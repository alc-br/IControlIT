Imports System.Web.Services
Imports System.Configuration
Imports System.Text
Imports System.Data
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization

Namespace Api
    <WebService(Namespace:="https://www.icontrolit.com.br/api/base.asmx")>
    <WebServiceBinding()>
    <ScriptService()>
    Public Class [Base]
        Inherits WebService

        Private ReadOnly _wsCadastro As New WSCadastro()

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
                            autenticado = String.Equals(partes(0), expectedUser, StringComparison.Ordinal) AndAlso
                                           String.Equals(partes(1), expectedPass, StringComparison.Ordinal)
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
                Dim dir = System.IO.Path.GetDirectoryName(logPath)
                If Not String.IsNullOrEmpty(dir) AndAlso Not System.IO.Directory.Exists(dir) Then
                    System.IO.Directory.CreateDirectory(dir)
                End If
                System.IO.File.AppendAllText(logPath, builder.ToString())
            Catch
            End Try

            Return autenticado
        End Function

        Private Function ObterParametrosDataSet() As DataTable
            Dim connConfigurada As String = ConfigurationManager.AppSettings("ANIMA")
            If String.IsNullOrWhiteSpace(connConfigurada) Then
                Throw New ApplicationException("Chave Conn_Banco_ApiParametros nÃ£o configurada no Web.config.")
            End If

            Dim resultado As DataSet = _wsCadastro.Parametros_Anima(
                pPConn_Banco:=connConfigurada,
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

            If resultado.Tables.Count = 0 Then
                Dim tabelaVazia As New DataTable("Parametros")
                Return tabelaVazia
            End If

            Return resultado.Tables(0)
        End Function

        <WebMethod(BufferResponse:=True)>
        <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
        Public Sub ListarJSON()
            If Not AutenticarRequisicao() Then
                Context.Response.StatusCode = 401
                Context.Response.StatusDescription = "Unauthorized"
                Context.Response.AddHeader("WWW-Authenticate", "Basic realm=""base""")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                Return
            End If

            Dim tabela As DataTable = ObterParametrosDataSet()
            Dim serializer As New JavaScriptSerializer()
            Dim linhas As New List(Of Dictionary(Of String, Object))()

            For Each row As DataRow In tabela.Rows
                Dim dict As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
                For Each col As DataColumn In tabela.Columns
                    dict(col.ColumnName) = If(row.IsNull(col), Nothing, row(col))
                Next
                linhas.Add(dict)
            Next
            Dim json As String = serializer.Serialize(linhas)
            Dim response = Context.Response
            response.ContentType = "application/json; charset=utf-8"
            response.Write(json)
            response.Flush()
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Sub
    End Class
End Namespace




