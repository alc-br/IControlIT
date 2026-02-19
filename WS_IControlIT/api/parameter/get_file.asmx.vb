' ANIMA
Imports System.IO
Imports System.Web.Services
Imports System.Configuration
Imports System.Text
Imports System.Data
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Imports System.Web
Imports System.Data.SqlClient

Namespace Api
    <WebService(Namespace:="https://www.icontrolit.com.br/api/parameter/get_file.asmx")>
    <WebServiceBinding()>
    <ScriptService()>
    Public Class GetFile
        Inherits WebService

        Private ReadOnly _oBanco As New cls_Banco()

        Public Class FileResponse
            Public Property nome_arquivo As String
            Public Property conteudo_arquivo As String
            Public Property observacao As String
        End Class

        Private Function AutenticarRequisicao() As Boolean
            Dim authorizationHeader As String = Context?.Request?.Headers("Authorization")

            If String.IsNullOrEmpty(authorizationHeader) Then
                authorizationHeader = Context?.Request?.ServerVariables("HTTP_AUTHORIZATION")
            End If

            Dim isBasic As Boolean = Not String.IsNullOrEmpty(authorizationHeader) AndAlso authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase)

            If Not isBasic Then
                Return False
            End If

            Dim encodedCredentials As String = authorizationHeader.Substring(6)
            Dim decoded As String = Nothing

            Try
                decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials))
            Catch
                Return False
            End Try

            If String.IsNullOrEmpty(decoded) Then
                Return False
            End If

            Dim partes As String() = decoded.Split(":"c)
            If partes.Length <> 2 Then
                Return False
            End If

            Dim expectedUser As String = ConfigurationManager.AppSettings("AuthUserApiParametros")
            Dim expectedPass As String = ConfigurationManager.AppSettings("AuthPassApiParametros")

            If String.IsNullOrEmpty(expectedUser) OrElse String.IsNullOrEmpty(expectedPass) Then
                Return False
            End If

            Return String.Equals(partes(0), expectedUser, StringComparison.Ordinal) AndAlso
                   String.Equals(partes(1), expectedPass, StringComparison.Ordinal)
        End Function

        ''' <summary>
        ''' Endpoint para obter arquivos PDF pelo numero da fatura (Nr_Fatura).
        ''' Parametros via QueryString:
        '''   - conta: Numero da fatura (obrigatorio) - corresponde ao Nr_Fatura na tabela Fatura
        '''   - dt_emissao: Periodo no formato AAAAMM (opcional) - filtra Fatura.Dt_Emissao pelo range do mes
        ''' Retorno JSON (array):
        '''   Cada item contem:
        '''   - nome_arquivo: Tabela_Registro + '-' + Nr_Fatura + '_' + LEFT(Dt_Lote,4) + '-' + RIGHT(Dt_Lote,2)
        '''   - conteudo_arquivo: conteudo do PDF em base64
        '''   - observacao: descricao de erro, se houver
        ''' </summary>
        <WebMethod(BufferResponse:=True)>
        <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
        Public Sub GetJSON()
            If Not AutenticarRequisicao() Then
                Context.Response.StatusCode = 401
                Context.Response.StatusDescription = "Unauthorized"
                Context.Response.AddHeader("WWW-Authenticate", "Basic realm=" & ChrW(34) & "base" & ChrW(34))
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                Return
            End If

            Dim conta As String = Context.Request.QueryString("conta")
            Dim dtEmissaoParam As String = Context.Request.QueryString("dt_emissao")

            ' Validar parametro obrigatorio
            If String.IsNullOrWhiteSpace(conta) Then
                ResponderErro(400, "Parametro 'conta' e obrigatorio.")
                Return
            End If

            ' Validar formato dt_emissao se informado (AAAAMM)
            If Not String.IsNullOrWhiteSpace(dtEmissaoParam) Then
                If dtEmissaoParam.Trim().Length <> 6 OrElse Not Integer.TryParse(dtEmissaoParam.Trim(), Nothing) Then
                    ResponderErro(400, "Parametro 'dt_emissao' deve estar no formato AAAAMM (ex: 202510).")
                    Return
                End If
            End If

            Try
                Dim connConfigurada As String = ConfigurationManager.AppSettings("ANIMA_EDUCACAO")
                If String.IsNullOrWhiteSpace(connConfigurada) Then
                    ResponderErro(500, "Chave ANIMA_EDUCACAO nao configurada no Web.config.")
                    Return
                End If

                Dim connectionString As String = _oBanco.Descriptografar(connConfigurada)

                Using conn As New SqlConnection(connectionString)
                    conn.Open()

                    ' 1. Buscar a Fatura pelo Nr_Fatura (e opcionalmente por Dt_Emissao)
                    Dim idFatura As Integer = 0
                    Dim nrFatura As String = Nothing
                    Dim dtLote As String = Nothing

                    Dim queryFatura As String = "SELECT TOP 1 Id_Fatura, Nr_Fatura, Dt_Lote FROM dbo.Fatura WHERE Nr_Fatura = @NrFatura"

                    ' Adicionar filtro de dt_emissao se informado
                    If Not String.IsNullOrWhiteSpace(dtEmissaoParam) Then
                        queryFatura &= " AND Dt_Emissao >= @DtEmissaoDe AND Dt_Emissao < @DtEmissaoAte"
                    End If

                    queryFatura &= " ORDER BY Id_Fatura DESC"

                    Using cmd As New SqlCommand(queryFatura, conn)
                        cmd.Parameters.AddWithValue("@NrFatura", conta)

                        If Not String.IsNullOrWhiteSpace(dtEmissaoParam) Then
                            Dim ano As Integer = Integer.Parse(dtEmissaoParam.Trim().Substring(0, 4))
                            Dim mes As Integer = Integer.Parse(dtEmissaoParam.Trim().Substring(4, 2))
                            Dim dtDe As New DateTime(ano, mes, 1)
                            Dim dtAte As DateTime = dtDe.AddMonths(1)
                            cmd.Parameters.AddWithValue("@DtEmissaoDe", dtDe)
                            cmd.Parameters.AddWithValue("@DtEmissaoAte", dtAte)
                        End If

                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                idFatura = reader.GetInt32(0)
                                nrFatura = If(reader.IsDBNull(1), "", reader.GetString(1).Trim())
                                dtLote = If(reader.IsDBNull(2), "", reader.GetString(2).Trim())
                            End If
                        End Using
                    End Using

                    If idFatura = 0 Then
                        Dim msgErro As String = "Fatura nao encontrada para a conta informada: " & conta
                        If Not String.IsNullOrWhiteSpace(dtEmissaoParam) Then
                            msgErro &= " (dt_emissao: " & dtEmissaoParam.Trim() & ")"
                        End If
                        ResponderErro(404, msgErro)
                        Return
                    End If

                    ' 2. Buscar TODOS os Arquivo_PDF associados a esta fatura
                    '    - Se Tabela_Registro = 'Boleto' ou 'Fatura' => Id_Registro_Tabela = Id_Fatura
                    '    - Se Tabela_Registro = 'Nota_Fiscal_Fatura' => Id_Registro_Tabela = Nota_Fiscal_Fatura.Id_Nota_Fiscal onde Id_Fatura = @IdFatura
                    Dim queryArquivo As String = "
                        SELECT
                            ap.Tabela_Registro,
                            ap.Arquivo
                        FROM dbo.Arquivo_PDF ap
                        WHERE
                            (
                                ap.Tabela_Registro IN ('Boleto', 'Fatura')
                                AND ap.Id_Registro_Tabela = @IdFatura
                            )
                            OR
                            (
                                ap.Tabela_Registro = 'Nota_Fiscal_Fatura'
                                AND ap.Id_Registro_Tabela IN (
                                    SELECT nf.Id_Nota_Fiscal
                                    FROM dbo.Nota_Fiscal_Fatura nf
                                    WHERE nf.Id_Fatura = @IdFatura
                                )
                            )
                        ORDER BY ap.Id_Arquivo_PDF ASC"

                    ' Sufixo do nome: _YYYY-MM
                    Dim sufixoDtLote As String = ""
                    If Not String.IsNullOrWhiteSpace(dtLote) AndAlso dtLote.Length >= 6 Then
                        sufixoDtLote = "_" & dtLote.Substring(0, 4) & "-" & dtLote.Substring(4, 2)
                    End If

                    Dim arquivos As New List(Of Dictionary(Of String, Object))()

                    Using cmd As New SqlCommand(queryArquivo, conn)
                        cmd.Parameters.AddWithValue("@IdFatura", idFatura)
                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim tabelaRegistro As String = If(reader.IsDBNull(0), "", reader.GetString(0).Trim())
                                Dim arquivoBytes As Byte() = Nothing

                                If Not reader.IsDBNull(1) Then
                                    Dim tamanho As Long = reader.GetBytes(1, 0, Nothing, 0, 0)
                                    arquivoBytes = New Byte(CInt(tamanho) - 1) {}
                                    reader.GetBytes(1, 0, arquivoBytes, 0, CInt(tamanho))
                                End If

                                Dim item As New Dictionary(Of String, Object)()

                                If arquivoBytes IsNot Nothing AndAlso arquivoBytes.Length > 0 Then
                                    item("nome_arquivo") = tabelaRegistro & "-" & nrFatura & sufixoDtLote
                                    item("conteudo_arquivo") = Convert.ToBase64String(arquivoBytes)
                                    item("observacao") = Nothing
                                Else
                                    item("nome_arquivo") = tabelaRegistro & "-" & nrFatura & sufixoDtLote
                                    item("conteudo_arquivo") = Nothing
                                    item("observacao") = "Arquivo com conteudo vazio para Tabela_Registro: " & tabelaRegistro
                                End If

                                arquivos.Add(item)
                            End While
                        End Using
                    End Using

                    If arquivos.Count = 0 Then
                        ResponderErro(404, "Arquivo PDF nao encontrado para a fatura: " & conta)
                        Return
                    End If

                    ResponderJsonArray(200, arquivos)
                End Using

            Catch ex As Exception
                ResponderErro(500, "Erro interno: " & ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Envia resposta JSON de erro (array com um unico item contendo a observacao)
        ''' </summary>
        Private Sub ResponderErro(statusCode As Integer, observacao As String)
            Dim item As New Dictionary(Of String, Object)()
            item("nome_arquivo") = Nothing
            item("conteudo_arquivo") = Nothing
            item("observacao") = observacao

            Dim lista As New List(Of Dictionary(Of String, Object))()
            lista.Add(item)

            ResponderJsonArray(statusCode, lista)
        End Sub

        ''' <summary>
        ''' Monta e envia a resposta JSON como array
        ''' </summary>
        Private Sub ResponderJsonArray(statusCode As Integer, itens As List(Of Dictionary(Of String, Object)))
            Context.Response.StatusCode = statusCode
            Context.Response.ContentType = "application/json; charset=utf-8"

            Dim serializer As New JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(serializer.Serialize(itens))
            Context.Response.Flush()
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Sub
    End Class
End Namespace
