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
    <WebService(Namespace:="https://www.icontrolit.com.br/api/parameter/get.asmx")>
    <WebServiceBinding()>
    <ScriptService()>
    Public Class [Base]
        Inherits WebService

        Private ReadOnly _wsCadastro As New WSCadastro()
        Private ReadOnly _oBanco As New cls_Banco()

        Public Class UpdateRequestModel
            Public Property Id_Parametro As Integer
            Public Property Requisicao_Compra As String
            Public Property Ordem_Compra As String
            Public Property Id_Processo As String
            Public Property Observacoes As String
        End Class

        Public Class ApiResponse
            Public Property Success As Boolean
            Public Property Message As String
        End Class

        Private Function TryReadPayloadFromBody() As UpdateRequestModel
            Dim context = HttpContext.Current
            Dim request = context?.Request
            If request Is Nothing Then
                Return Nothing
            End If

            Dim stream = request.InputStream
            If stream Is Nothing Then
                Return Nothing
            End If

            Dim originalPosition As Long = 0
            If stream.CanSeek Then
                originalPosition = stream.Position
                stream.Position = 0
            End If

            Dim encoding As Encoding = If(request.ContentEncoding, Encoding.UTF8)
            Dim body As String

            Using reader As New StreamReader(stream, encoding, detectEncodingFromByteOrderMarks:=True, bufferSize:=1024, leaveOpen:=True)
                body = reader.ReadToEnd()
            End Using

            If stream.CanSeek Then
                stream.Position = originalPosition
            End If

            If String.IsNullOrWhiteSpace(body) Then
                Return Nothing
            End If

            Dim serializer As New JavaScriptSerializer()
            Try
                Return serializer.Deserialize(Of UpdateRequestModel)(body)
            Catch
                Try
                    Dim wrapper = serializer.Deserialize(Of Dictionary(Of String, Object))(body)
                    If wrapper IsNot Nothing Then
                        For Each item In wrapper
                            If String.Equals(item.Key, "payload", StringComparison.OrdinalIgnoreCase) Then
                                Return serializer.ConvertToType(Of UpdateRequestModel)(item.Value)
                            End If
                        Next
                    End If
                Catch
                End Try
            End Try

            Return Nothing
        End Function

        Private Function AutenticarRequisicao() As Boolean
            Const logPath As String = "C:\Temp\ApiParametrosAuth.log"

            Dim builder As New StringBuilder()
            builder.AppendLine("---- " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " ----")

            Dim authorizationHeader As String = Context?.Request?.Headers("Authorization")
            builder.AppendLine("AuthorizationHeaderPresent=" & (Not String.IsNullOrEmpty(authorizationHeader)).ToString())

            If String.IsNullOrEmpty(authorizationHeader) Then
                authorizationHeader = Context?.Request?.ServerVariables("HTTP_AUTHORIZATION")
                builder.AppendLine("ServerVariableAuthPresent=" & (Not String.IsNullOrEmpty(authorizationHeader)).ToString())
            End If

            Dim headerApiUser As String = Context?.Request?.Headers("X-Api-User")
            Dim headerApiKey As String = Context?.Request?.Headers("X-Api-Key")
            builder.AppendLine("HeaderXApiUserPresent=" & (Not String.IsNullOrEmpty(headerApiUser)).ToString())
            builder.AppendLine("HeaderXApiKeyPresent=" & (Not String.IsNullOrEmpty(headerApiKey)).ToString())


            Dim isBasic As Boolean = Not String.IsNullOrEmpty(authorizationHeader) AndAlso authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase)
            builder.AppendLine("IsBasic=" & isBasic.ToString())

            Dim autenticado As Boolean = False

            If isBasic Then
                Dim encodedCredentials As String = authorizationHeader.Substring(6)
                Dim decoded As String = Nothing

                Try
                    decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials))
                Catch
                    builder.AppendLine("DecodeFailed=True")
                End Try

                If Not String.IsNullOrEmpty(decoded) Then
                    Dim partes As String() = decoded.Split(":"c)
                    If partes.Length = 2 Then
                        Dim expectedUser As String = ConfigurationManager.AppSettings("AuthUserApiParametros")
                        Dim expectedPass As String = ConfigurationManager.AppSettings("AuthPassApiParametros")
                        If Not String.IsNullOrEmpty(expectedUser) AndAlso Not String.IsNullOrEmpty(expectedPass) Then
                            autenticado = String.Equals(partes(0), expectedUser, StringComparison.Ordinal) AndAlso String.Equals(partes(1), expectedPass, StringComparison.Ordinal)
                        Else
                            builder.AppendLine("CredenciaisConfiguradas=False")
                        End If
                    Else
                        builder.AppendLine("SplitFailed=True")
                    End If
                End If
            End If

            builder.AppendLine("AuthSuccess=" & autenticado.ToString())

            Try
                Dim dir = Path.GetDirectoryName(logPath)
                If Not String.IsNullOrEmpty(dir) AndAlso Not Directory.Exists(dir) Then
                    Directory.CreateDirectory(dir)
                End If
                File.AppendAllText(logPath, builder.ToString())
            Catch
            End Try

            Return autenticado
        End Function

        Private Function ObterParametrosDataSet() As DataTable
            Dim connConfigurada As String = ConfigurationManager.AppSettings("ANIMA_EDUCACAO")
            If String.IsNullOrWhiteSpace(connConfigurada) Then
                Throw New ApplicationException("Chave Conn_Banco_ApiParametros nao configurada no Web.config.")
            End If

            ' NOVA LÓGICA: Verificar se existe sequência preparada na tabela Anima_Parametros_Preparado
            Dim connectionString As String = _oBanco.Descriptografar(connConfigurada)
            Dim temSequenciaPreparada As Boolean = False

            Try
                Using conn As New SqlConnection(connectionString)
                    conn.Open()
                    Dim queryCheck As String = "SELECT COUNT(*) FROM Anima_Parametros_Preparado"
                    Using cmd As New SqlCommand(queryCheck, conn)
                        Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                        temSequenciaPreparada = (count > 0)
                    End Using
                End Using
            Catch
                ' Em caso de erro, retorna tabela vazia (não há sequência preparada)
            End Try

            ' Se tem sequência preparada, retornar da tabela Anima_Parametros_Preparado
            If temSequenciaPreparada Then
                Return ObterParametrosComSequencia(connectionString)
            End If

            ' Caso contrário, retorna tabela vazia (robô só processa quando há sequência preparada)
            Dim tabelaVazia As New DataTable("Parametros")
            Return tabelaVazia
        End Function

        ''' <summary>
        ''' Obtém os parâmetros preparados da tabela Anima_Parametros_Preparado, ordenados por Ordem
        ''' IMPORTANTE: Retorna o Id_Parametro ORIGINAL da tabela Anima_Parametros (não o ID da tabela preparada)
        ''' </summary>
        Private Function ObterParametrosComSequencia_OLD_BACKUP(connectionString As String) As DataTable
            ' BACKUP DA IMPLEMENTAÇÃO ANTIGA - NÃO USAR
            Dim resultado As DataSet = _wsCadastro.Parametros_AnimaLegacy(
                pPConn_Banco:=connectionString,
                pAcao:="SELECT_ALL",
                pId_Parametro:=0,
                pCodigo_Referencia:=Nothing,
                pTipo:=Nothing,
                pCNPJ_Anima:=Nothing,
                pConta:=Nothing,
                pDescricaoServico:=Nothing,
                pMesEmissao:=Nothing,
                pRequisitioningBUId:=Nothing,
                pRequisitioningBUName:=Nothing,
                pDescription:=Nothing,
                pJustification:=Nothing,
                pPreparerEmail:=Nothing,
                pApproverEmail:=Nothing,
                pDocumentStatusCode:=Nothing,
                pRequisitionType:=Nothing,
                pSourceUniqueId:=Nothing,
                pCategoryName:=Nothing,
                pDeliverToLocationCode:=Nothing,
                pDeliverToOrganizationCode:=Nothing,
                pProcurementBUName:=Nothing,
                pItemDescription:=Nothing,
                pItemNumber:=Nothing,
                pRequesterEmail:=Nothing,
                pSupplierName:=Nothing,
                pSupplierContactName:=Nothing,
                pSupplierSiteName:=Nothing,
                pCentroDeCusto:=Nothing,
                pEstabelecimento:=Nothing,
                pUnidadeNegocio:=Nothing,
                pFinalidade:=Nothing,
                pProjeto:=Nothing,
                pInterCompany:=Nothing,
                pCodigoRequisicaoCompra:=Nothing,
                pCodigoOrdemCompra:=Nothing,
                pCodigoInvoice:=Nothing,
                pObservacao:=Nothing,
                pUsuario:=HttpContext.Current?.User?.Identity?.Name,
                pFl_Ativo:=True
            )

            If resultado Is Nothing OrElse resultado.Tables.Count = 0 Then
                Dim tabelaVazia As New DataTable("Parametros")
                Return tabelaVazia
            End If

            Return resultado.Tables(0)
        End Function

        ''' <summary>
        ''' Obtém os parâmetros preparados da tabela Anima_Parametros_Preparado, ordenados por Ordem
        ''' IMPORTANTE: Retorna o Id_Parametro ORIGINAL da tabela Anima_Parametros (não o ID da tabela preparada)
        ''' </summary>
        Private Function ObterParametrosComSequencia(connectionString As String) As DataTable
            Dim tabela As New DataTable("Parametros")

            Try
                Using conn As New SqlConnection(connectionString)
                    conn.Open()

                    ' Buscar da tabela Anima_Parametros_Preparado ordenado por Ordem
                    ' IMPORTANTE: Id_Parametro aqui é o ID ORIGINAL da tabela Anima_Parametros
                    Dim query As String = "
                        SELECT
                            Id_Parametro,
                            Codigo_Referencia,
                            Tipo,
                            CNPJ_Anima,
                            Conta,
                            DescricaoServico,
                            MesEmissao,
                            RequisitioningBUId,
                            RequisitioningBUName,
                            [Description],
                            Justification,
                            PreparerEmail,
                            ApproverEmail,
                            DocumentStatusCode,
                            RequisitionType,
                            SourceUniqueId,
                            CategoryName,
                            DeliverToLocationCode,
                            DeliverToOrganizationCode,
                            ProcurementBUName,
                            ItemDescription,
                            ItemNumber,
                            RequesterEmail,
                            SupplierName,
                            SupplierContactName,
                            SupplierSiteName,
                            CentroDeCusto,
                            Estabelecimento,
                            UnidadeNegocio,
                            Finalidade,
                            Projeto,
                            InterCompany,
                            CodigoRequisicaoCompra,
                            CodigoOrdemCompra,
                            CodigoInvoice,
                            Observacao,
                            Processamento_Manual,
                            Status_Robo,
                            Sequencia_Execucao,
                            Status,
                            Descricao,
                            Data_Modificacao,
                            Usuario_Modificacao,
                            Ordem
                        FROM Anima_Parametros_Preparado
                        ORDER BY Ordem ASC"

                    Using cmd As New SqlCommand(query, conn)
                        Using adapter As New SqlDataAdapter(cmd)
                            adapter.Fill(tabela)
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                Throw New ApplicationException("Erro ao buscar parametros preparados: " & ex.Message)
            End Try

            Return tabela
        End Function

        Private Function ObterPrnFatura(idParametro As Integer) As String
            Dim connConfigurada As String = ConfigurationManager.AppSettings("ANIMA_EDUCACAO")
            If String.IsNullOrWhiteSpace(connConfigurada) Then
                Return Nothing
            End If

            Dim connectionString As String = _oBanco.Descriptografar(connConfigurada)
            Dim prnFatura As String = Nothing

            Try
                Using conn As New SqlConnection(connectionString)
                    conn.Open()

                    ' Primeiro busca a Conta do Anima_Parametros
                    Dim conta As String = Nothing
                    Dim queryParametro As String = "SELECT Conta FROM dbo.Anima_Parametros WHERE Id_Parametro = @IdParametro"
                    Using cmd As New SqlCommand(queryParametro, conn)
                        cmd.Parameters.AddWithValue("@IdParametro", idParametro)
                        Dim result As Object = cmd.ExecuteScalar()
                        If result IsNot Nothing Then
                            conta = result.ToString()
                        End If
                    End Using

                    ' Se encontrou a conta, busca a fatura
                    If Not String.IsNullOrWhiteSpace(conta) Then
                        Dim queryFatura As String = "SELECT TOP 1 Observacao, Nota_Fiscal FROM dbo.Fatura WHERE Nr_Fatura = @Conta ORDER BY Id_Fatura DESC"
                        Using cmd As New SqlCommand(queryFatura, conn)
                            cmd.Parameters.AddWithValue("@Conta", conta)
                            Using reader As SqlDataReader = cmd.ExecuteReader()
                                If reader.Read() Then
                                    Dim observacao As String = If(reader.IsDBNull(0), "", reader.GetString(0).Trim())
                                    Dim notaFiscal As String = If(reader.IsDBNull(1), "", reader.GetString(1).Trim())

                                    ' Concatena apenas se ambos tiverem valor
                                    If Not String.IsNullOrWhiteSpace(observacao) AndAlso Not String.IsNullOrWhiteSpace(notaFiscal) Then
                                        prnFatura = observacao & "_" & notaFiscal
                                    ElseIf Not String.IsNullOrWhiteSpace(observacao) Then
                                        prnFatura = observacao
                                    ElseIf Not String.IsNullOrWhiteSpace(notaFiscal) Then
                                        prnFatura = notaFiscal
                                    End If
                                End If
                            End Using
                        End Using
                    End If
                End Using
            Catch
                ' Em caso de erro, retorna Nothing
            End Try

            Return prnFatura
        End Function

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

            Dim tabela As DataTable = ObterParametrosDataSet()
            Dim serializer As New JavaScriptSerializer()
            
            ' AUMENTO DO LIMITE DE JSON PARA O MÁXIMO PERMITIDO (CORREÇÃO)
            serializer.MaxJsonLength = Int32.MaxValue

            Dim linhas As New List(Of Dictionary(Of String, Object))()

            For Each row As DataRow In tabela.Rows
                Dim dict As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
                For Each col As DataColumn In tabela.Columns
                    Dim valor As Object = If(row.IsNull(col), Nothing, row(col))
                    ' Corrigir encoding UTF-8 mal formado em campos de texto
                    If valor IsNot Nothing AndAlso TypeOf valor Is String Then
                        valor = CorrigirEncoding(valor)
                    End If
                    dict(col.ColumnName) = valor
                Next

                ' Normalizar campo Tipo (remover acentos, converter para minúsculas)
                ' Retorno padronizado: telecom-hibrida, servico-hibrida, servico, telecom
                If dict.ContainsKey("Tipo") Then
                    dict("Tipo") = NormalizarTipo(dict("Tipo"))
                End If

                ' Converter Data_Modificacao para formato ISO 8601
                If dict.ContainsKey("Data_Modificacao") AndAlso dict("Data_Modificacao") IsNot Nothing Then
                    If TypeOf dict("Data_Modificacao") Is DateTime Then
                        Dim dataModificacao As DateTime = CType(dict("Data_Modificacao"), DateTime)
                        dict("Data_Modificacao") = dataModificacao.ToString("yyyy-MM-ddTHH:mm:ss")
                    End If
                End If

                ' Adicionar campo prn_fatura (Observacao_Nota_Fiscal da tabela Fatura)
                Dim idParametro As Integer = 0
                If dict.ContainsKey("Id_Parametro") AndAlso dict("Id_Parametro") IsNot Nothing Then
                    Integer.TryParse(dict("Id_Parametro").ToString(), idParametro)
                End If
                If idParametro > 0 Then
                    dict("prn_fatura") = ObterPrnFatura(idParametro)
                Else
                    dict("prn_fatura") = Nothing
                End If

                linhas.Add(dict)
            Next

            Dim json As String = serializer.Serialize(linhas)
            Dim response = Context.Response
            response.ContentType = "application/json; charset=utf-8"
            response.Write(json)
            response.Flush()
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Sub

        ''' <summary>
        ''' Endpoint para obter detalhamento de fatura (invoice) pelo número da conta
        ''' Parâmetros via QueryString:
        ''' - conta: Número da conta (obrigatório) - corresponde ao DC_Nr_Nota_Fiscal
        ''' - dt_lote: Período no formato YYYYMM (opcional)
        ''' - conceito: Filtro por tipo de conceito (opcional) - "s" para servico, "a" para assinatura
        ''' </summary>
        <WebMethod(BufferResponse:=True)>
        <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
        Public Sub GetInvoice()
            If Not AutenticarRequisicao() Then
                Context.Response.StatusCode = 401
                Context.Response.StatusDescription = "Unauthorized"
                Context.Response.AddHeader("WWW-Authenticate", "Basic realm=" & ChrW(34) & "base" & ChrW(34))
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                Return
            End If

            ' Obter parâmetros da QueryString
            Dim conta As String = Context.Request.QueryString("conta")
            Dim dtLote As String = Context.Request.QueryString("dt_lote")
            Dim conceito As String = Context.Request.QueryString("conceito")

            ' Validar parâmetro obrigatório
            If String.IsNullOrWhiteSpace(conta) Then
                Context.Response.StatusCode = 400
                Context.Response.ContentType = "application/json; charset=utf-8"
                Dim errorResponse As New Dictionary(Of String, Object)()
                errorResponse("success") = False
                errorResponse("message") = "Parametro 'conta' e obrigatorio."
                Dim serializer As New JavaScriptSerializer()
                Context.Response.Write(serializer.Serialize(errorResponse))
                Context.Response.Flush()
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                Return
            End If

            Try
                Dim tabela As DataTable = ObterDetalhamentoFatura(conta, dtLote)

                ' Aplicar filtro por conceito se informado
                If Not String.IsNullOrWhiteSpace(conceito) Then
                    tabela = FiltrarPorConceito(tabela, conceito)
                End If

                Dim serializer As New JavaScriptSerializer()

                ' AUMENTO DO LIMITE DE JSON PARA O MÁXIMO PERMITIDO (CORREÇÃO)
                serializer.MaxJsonLength = Int32.MaxValue

                Dim linhas As New List(Of Dictionary(Of String, Object))()

                For Each row As DataRow In tabela.Rows
                    Dim dict As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
                    For Each col As DataColumn In tabela.Columns
                        Dim nomeApi As String = MapearNomeColunaParaApi(col.ColumnName)
                        Dim valor As Object = If(row.IsNull(col), Nothing, row(col))
                        ' Corrigir encoding UTF-8 mal formado em campos de texto
                        If valor IsNot Nothing AndAlso TypeOf valor Is String Then
                            valor = CorrigirEncoding(valor)
                        End If
                        ' Converter datas para ISO8601
                        valor = FormatarValorParaApi(nomeApi, valor)
                        dict(nomeApi) = valor
                    Next
                    linhas.Add(dict)
                Next

                Dim json As String = serializer.Serialize(linhas)
                Dim response = Context.Response
                response.ContentType = "application/json; charset=utf-8"
                response.Write(json)
                response.Flush()
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            Catch ex As Exception
                Context.Response.StatusCode = 500
                Context.Response.ContentType = "application/json; charset=utf-8"
                Dim errorResponse As New Dictionary(Of String, Object)()
                errorResponse("success") = False
                errorResponse("message") = "Erro ao consultar detalhamento: " & ex.Message
                Dim serializer As New JavaScriptSerializer()
                Context.Response.Write(serializer.Serialize(errorResponse))
                Context.Response.Flush()
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End Try
        End Sub

        ''' <summary>
        ''' Corrige strings com encoding UTF-8 mal formado (double-encoded)
        ''' Isso ocorre quando dados UTF-8 sao gravados em colunas VARCHAR Latin1
        ''' Usa ChrW() para evitar problemas de encoding no codigo fonte
        ''' </summary>
        Private Function CorrigirEncoding(valor As Object) As String
            If valor Is Nothing OrElse valor Is DBNull.Value Then
                Return Nothing
            End If

            Dim texto As String = valor.ToString()
            If String.IsNullOrEmpty(texto) Then
                Return texto
            End If

            ' Mapeamento de sequencias UTF-8 mal formadas para caracteres corretos
            ' Quando UTF-8 eh interpretado como Latin1, cada byte vira um caractere
            ' ChrW(195) = "Ã" (primeiro byte de UTF-8 para caracteres acentuados)
            Dim correcoes As New Dictionary(Of String, String)()

            ' Minusculas acentuadas (a)
            correcoes.Add(ChrW(195) & ChrW(161), ChrW(225))  ' a com acento agudo
            correcoes.Add(ChrW(195) & ChrW(160), ChrW(224))  ' a com acento grave
            correcoes.Add(ChrW(195) & ChrW(163), ChrW(227))  ' a com til
            correcoes.Add(ChrW(195) & ChrW(162), ChrW(226))  ' a com circunflexo

            ' Minusculas acentuadas (e)
            correcoes.Add(ChrW(195) & ChrW(169), ChrW(233))  ' e com acento agudo
            correcoes.Add(ChrW(195) & ChrW(168), ChrW(232))  ' e com acento grave
            correcoes.Add(ChrW(195) & ChrW(170), ChrW(234))  ' e com circunflexo

            ' Minusculas acentuadas (i)
            correcoes.Add(ChrW(195) & ChrW(173), ChrW(237))  ' i com acento agudo
            correcoes.Add(ChrW(195) & ChrW(172), ChrW(236))  ' i com acento grave
            correcoes.Add(ChrW(195) & ChrW(174), ChrW(238))  ' i com circunflexo

            ' Minusculas acentuadas (o)
            correcoes.Add(ChrW(195) & ChrW(179), ChrW(243))  ' o com acento agudo
            correcoes.Add(ChrW(195) & ChrW(178), ChrW(242))  ' o com acento grave
            correcoes.Add(ChrW(195) & ChrW(181), ChrW(245))  ' o com til
            correcoes.Add(ChrW(195) & ChrW(180), ChrW(244))  ' o com circunflexo

            ' Minusculas acentuadas (u)
            correcoes.Add(ChrW(195) & ChrW(186), ChrW(250))  ' u com acento agudo
            correcoes.Add(ChrW(195) & ChrW(185), ChrW(249))  ' u com acento grave
            correcoes.Add(ChrW(195) & ChrW(187), ChrW(251))  ' u com circunflexo

            ' Cedilha
            correcoes.Add(ChrW(195) & ChrW(167), ChrW(231))  ' c cedilha minusculo
            correcoes.Add(ChrW(195) & ChrW(135), ChrW(199))  ' C cedilha maiusculo

            ' Outros caracteres especiais
            correcoes.Add(ChrW(195) & ChrW(177), ChrW(241))  ' n com til minusculo
            correcoes.Add(ChrW(195) & ChrW(145), ChrW(209))  ' N com til maiusculo
            correcoes.Add(ChrW(195) & ChrW(188), ChrW(252))  ' u com trema minusculo
            correcoes.Add(ChrW(195) & ChrW(156), ChrW(220))  ' U com trema maiusculo

            ' Maiusculas acentuadas (A)
            correcoes.Add(ChrW(195) & ChrW(129), ChrW(193))  ' A com acento agudo
            correcoes.Add(ChrW(195) & ChrW(128), ChrW(192))  ' A com acento grave
            correcoes.Add(ChrW(195) & ChrW(131), ChrW(195))  ' A com til
            correcoes.Add(ChrW(195) & ChrW(130), ChrW(194))  ' A com circunflexo

            ' Maiusculas acentuadas (E)
            correcoes.Add(ChrW(195) & ChrW(137), ChrW(201))  ' E com acento agudo
            correcoes.Add(ChrW(195) & ChrW(136), ChrW(200))  ' E com acento grave
            correcoes.Add(ChrW(195) & ChrW(138), ChrW(202))  ' E com circunflexo

            ' Maiusculas acentuadas (I)
            correcoes.Add(ChrW(195) & ChrW(141), ChrW(205))  ' I com acento agudo
            correcoes.Add(ChrW(195) & ChrW(140), ChrW(204))  ' I com acento grave
            correcoes.Add(ChrW(195) & ChrW(142), ChrW(206))  ' I com circunflexo

            ' Maiusculas acentuadas (O)
            correcoes.Add(ChrW(195) & ChrW(147), ChrW(211))  ' O com acento agudo
            correcoes.Add(ChrW(195) & ChrW(146), ChrW(210))  ' O com acento grave
            correcoes.Add(ChrW(195) & ChrW(149), ChrW(213))  ' O com til
            correcoes.Add(ChrW(195) & ChrW(148), ChrW(212))  ' O com circunflexo

            ' Maiusculas acentuadas (U)
            correcoes.Add(ChrW(195) & ChrW(154), ChrW(218))  ' U com acento agudo
            correcoes.Add(ChrW(195) & ChrW(153), ChrW(217))  ' U com acento grave
            correcoes.Add(ChrW(195) & ChrW(155), ChrW(219))  ' U com circunflexo

            For Each correcao In correcoes
                texto = texto.Replace(correcao.Key, correcao.Value)
            Next

            Return texto
        End Function

        ''' <summary>
        ''' Normaliza o campo Tipo para retorno padronizado:
        ''' - Converte para minúsculas
        ''' - Remove acentos
        ''' Ex: "Telecom-Híbrida" -> "telecom-hibrida", "Serviço" -> "servico"
        ''' </summary>
        Private Function NormalizarTipo(valor As Object) As String
            If valor Is Nothing OrElse valor Is DBNull.Value Then
                Return Nothing
            End If

            ' Primeiro corrigir encoding, depois normalizar
            Dim tipo As String = CorrigirEncoding(valor).Trim().ToLower()

            ' Remover acentos
            tipo = tipo.Replace("á", "a").Replace("à", "a").Replace("ã", "a").Replace("â", "a")
            tipo = tipo.Replace("é", "e").Replace("è", "e").Replace("ê", "e")
            tipo = tipo.Replace("í", "i").Replace("ì", "i").Replace("î", "i")
            tipo = tipo.Replace("ó", "o").Replace("ò", "o").Replace("õ", "o").Replace("ô", "o")
            tipo = tipo.Replace("ú", "u").Replace("ù", "u").Replace("û", "u")
            tipo = tipo.Replace("ç", "c")

            Return tipo
        End Function

        ''' <summary>
        ''' Mapeia o nome da coluna do banco para o nome da API em snake_case
        ''' </summary>
        Private Function MapearNomeColunaParaApi(nomeColuna As String) As String
            Dim mapeamento As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) From {
                {"Periodo", "periodo"},
                {"Razao Social", "razao_social"},
                {"Operadora Fatura", "operadora_fatura"},
                {"Conta", "conta"},
                {"Fatura", "fatura"},
                {"Data da emissao", "data_da_emissao"},
                {"Data Vencimento", "data_vencimento"},
                {"Operadora servico", "operadora_servico"},
                {"Servico", "servico"},
                {"Servicos original", "servicos_original"},
                {"Tecnologia do Servico", "tecnologia_do_servico"},
                {"Tipo de Servico", "tipo_de_servico"},
                {"Tecnologia conceito", "tecnologia_conceito"},
                {"Conceito", "conceito"},
                {"Conceito Original", "conceito_original"},
                {"Es tributacao", "es_tributacao"},
                {"Moeda", "moeda"},
                {"Taxa de Cambio", "taxa_de_cambio"},
                {"Quant. de Chamadas", "quant_de_chamadas"},
                {"Unidades", "unidades"},
                {"Tipo de Unidades", "tipo_de_unidades"},
                {"Custo Total Original", "custo_total_original"},
                {"Custo Total s/I", "custo_total_si"},
                {"Custo Total C/I", "custo_total_ci"},
                {"Tipo de Encargo Fixo", "tipo_de_encargo_fixo"},
                {"Data De E.Fixos", "data_de_e_fixos"},
                {"Data Ate E.Fixos", "data_ate_e_fixos"},
                {"Data Até E.Fixos", "data_ate_e_fixos"},
                {"Data De E.Variaveis", "data_de_e_variaveis"},
                {"Data De E.Variáveis", "data_de_e_variaveis"},
                {"Data Ate E.Variaveis", "data_ate_e_variaveis"},
                {"Data Até E.Variáveis", "data_ate_e_variaveis"},
                {"Plano", "plano"}
            }

            If mapeamento.ContainsKey(nomeColuna) Then
                Return mapeamento(nomeColuna)
            End If

            ' Fallback: converter para snake_case e normalizar acentos
            Dim resultado As String = nomeColuna.ToLower().Replace(" ", "_").Replace(".", "_")
            ' Remover acentos comuns
            resultado = resultado.Replace("á", "a").Replace("à", "a").Replace("ã", "a").Replace("â", "a")
            resultado = resultado.Replace("é", "e").Replace("è", "e").Replace("ê", "e")
            resultado = resultado.Replace("í", "i").Replace("ì", "i").Replace("î", "i")
            resultado = resultado.Replace("ó", "o").Replace("ò", "o").Replace("õ", "o").Replace("ô", "o")
            resultado = resultado.Replace("ú", "u").Replace("ù", "u").Replace("û", "u")
            resultado = resultado.Replace("ç", "c")
            Return resultado
        End Function

        ''' <summary>
        ''' Formata valores para a API, convertendo datas para ISO8601 e valores monetários para formato brasileiro
        ''' </summary>
        Private Function FormatarValorParaApi(nomeApi As String, valor As Object) As Object
            If valor Is Nothing Then
                Return Nothing
            End If

            ' Campos monetários que devem ser formatados como "R$ 1,73"
            Dim camposMonetarios As String() = {
                "custo_total_original",
                "custo_total_si",
                "custo_total_ci",
                "valor",
                "valor_total",
                "custo",
                "preco",
                "taxa_de_cambio"
            }

            ' Se for campo monetário, formatar como moeda brasileira
            If camposMonetarios.Contains(nomeApi) Then
                Dim valorNumerico As Decimal = 0

                ' Tentar converter para decimal
                If TypeOf valor Is Decimal Then
                    valorNumerico = CType(valor, Decimal)
                ElseIf TypeOf valor Is Double Then
                    valorNumerico = Convert.ToDecimal(CType(valor, Double))
                ElseIf TypeOf valor Is Single Then
                    valorNumerico = Convert.ToDecimal(CType(valor, Single))
                ElseIf TypeOf valor Is Integer Then
                    valorNumerico = Convert.ToDecimal(CType(valor, Integer))
                ElseIf TypeOf valor Is Long Then
                    valorNumerico = Convert.ToDecimal(CType(valor, Long))
                ElseIf TypeOf valor Is String Then
                    Decimal.TryParse(CType(valor, String).Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, valorNumerico)
                End If

                ' Formatar como moeda brasileira: R$ 1.234,56
                Return "R$ " & valorNumerico.ToString("N2", New System.Globalization.CultureInfo("pt-BR"))
            End If

            ' Se for DateTime, converter para ISO8601 (aplica para qualquer campo DateTime)
            If TypeOf valor Is DateTime Then
                Dim dt As DateTime = CType(valor, DateTime)
                ' Para o campo periodo, usar formato MM/YYYY
                If nomeApi = "periodo" Then
                    Return dt.ToString("MM/yyyy")
                End If
                Return dt.ToString("yyyy-MM-dd")
            End If

            ' Campos de data que precisam ser convertidos para ISO8601 (quando vem como string)
            Dim camposData As String() = {
                "data_da_emissao",
                "data_vencimento",
                "data_de_e_fixos",
                "data_ate_e_fixos",
                "data_de_e_variaveis",
                "data_ate_e_variaveis"
            }

            If camposData.Contains(nomeApi) Then
                ' Se for string com data no formato DD/MM/YYYY, converter para ISO8601
                If TypeOf valor Is String Then
                    Dim strValor As String = CType(valor, String)
                    If Not String.IsNullOrWhiteSpace(strValor) Then
                        Dim dt As DateTime
                        ' Tentar parse no formato brasileiro DD/MM/YYYY
                        If DateTime.TryParseExact(strValor, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
                            Return dt.ToString("yyyy-MM-dd")
                        End If
                        ' Tentar parse no formato MM/YYYY (periodo)
                        If DateTime.TryParseExact(strValor, "MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
                            Return dt.ToString("yyyy-MM")
                        End If
                    End If
                End If
            End If

            Return valor
        End Function

        ''' <summary>
        ''' Filtra o DataTable pelo campo Conceito
        ''' - "s" ou "servico": retorna registros onde Conceito comeca com "servico"
        ''' - "a" ou "assinatura": retorna registros onde Conceito comeca com "assinatura"
        ''' </summary>
        Private Function FiltrarPorConceito(tabela As DataTable, filtroConceito As String) As DataTable
            If tabela Is Nothing OrElse tabela.Rows.Count = 0 Then
                Return tabela
            End If

            ' Verificar se a coluna Conceito existe
            If Not tabela.Columns.Contains("Conceito") Then
                Return tabela
            End If

            ' Normalizar o filtro
            Dim filtro As String = filtroConceito.Trim().ToLower()

            ' Determinar o prefixo a buscar
            ' Usa "servi" para evitar problemas com encoding do cedilha
            Dim prefixoBusca As String = Nothing
            If filtro = "s" OrElse filtro.StartsWith("servi") Then
                prefixoBusca = "servi"
            ElseIf filtro = "a" OrElse filtro.StartsWith("assin") Then
                prefixoBusca = "assin"
            Else
                ' Filtro nao reconhecido, retornar sem filtrar
                Return tabela
            End If

            ' Criar tabela filtrada com mesma estrutura
            Dim tabelaFiltrada As DataTable = tabela.Clone()

            For Each row As DataRow In tabela.Rows
                If Not row.IsNull("Conceito") Then
                    ' Corrigir encoding e normalizar para comparacao
                    Dim conceito As String = CorrigirEncoding(row("Conceito")).Trim().ToLower()
                    ' Remover acentos para comparacao
                    conceito = conceito.Replace(ChrW(231), "c").Replace(ChrW(199), "c")  ' cedilha
                    conceito = conceito.Replace(ChrW(225), "a").Replace(ChrW(224), "a").Replace(ChrW(227), "a").Replace(ChrW(226), "a")
                    conceito = conceito.Replace(ChrW(233), "e").Replace(ChrW(232), "e").Replace(ChrW(234), "e")
                    conceito = conceito.Replace(ChrW(237), "i").Replace(ChrW(236), "i").Replace(ChrW(238), "i")
                    conceito = conceito.Replace(ChrW(243), "o").Replace(ChrW(242), "o").Replace(ChrW(245), "o").Replace(ChrW(244), "o")
                    conceito = conceito.Replace(ChrW(250), "u").Replace(ChrW(249), "u").Replace(ChrW(251), "u")

                    If conceito.StartsWith(prefixoBusca) Then
                        tabelaFiltrada.ImportRow(row)
                    End If
                End If
            Next

            Return tabelaFiltrada
        End Function

        ''' <summary>
        ''' Obtém o detalhamento da fatura chamando a stored procedure cn_Detalhamento_Bilhete_API
        ''' </summary>
        Private Function ObterDetalhamentoFatura(conta As String, dtLote As String) As DataTable
            Dim connConfigurada As String = ConfigurationManager.AppSettings("ANIMA_EDUCACAO")
            If String.IsNullOrWhiteSpace(connConfigurada) Then
                Throw New ApplicationException("Chave ANIMA_EDUCACAO nao configurada no Web.config.")
            End If

            Dim connectionString As String = _oBanco.Descriptografar(connConfigurada)
            Dim tabela As New DataTable("DetalhamentoFatura")

            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Using cmd As New SqlCommand("dbo.cn_Detalhamento_Bilhete_API", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 300

                    ' Parâmetros da stored procedure
                    cmd.Parameters.AddWithValue("@pPakage", "sp_Detalhamento")
                    cmd.Parameters.AddWithValue("@pNr_Fatura", conta)

                    ' Parâmetro opcional de período
                    If Not String.IsNullOrWhiteSpace(dtLote) Then
                        cmd.Parameters.AddWithValue("@pDt_LoteDe", dtLote)
                    Else
                        cmd.Parameters.AddWithValue("@pDt_LoteDe", DBNull.Value)
                    End If

                    ' Parâmetros opcionais (NULL)
                    cmd.Parameters.AddWithValue("@pAtivo_Tipo_Grupo", DBNull.Value)
                    cmd.Parameters.AddWithValue("@pId_Conglomerado", DBNull.Value)
                    cmd.Parameters.AddWithValue("@pDt_LoteAte", DBNull.Value)

                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(tabela)
                    End Using
                End Using
            End Using

            Return tabela
        End Function
    End Class
End Namespace