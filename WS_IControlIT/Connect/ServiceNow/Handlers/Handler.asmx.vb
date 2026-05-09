' -----------------------------------------------------------------------
' Handler.asmx.vb
' Autor: Anderson Luiz Chipak
' Data: 05/09/2024
' Descrição: Tratamento de acoes
' -----------------------------------------------------------------------
' Histórico de Modificações
' TICKET - DATA - NOME COMPLETO
' -----------------------------------------------------------------------
Imports System.Web.Services
Imports System.Xml
Imports System.IO
Imports System.Web.Services.Description
Imports System.Web.Services.Protocols
Imports WS_IControlIT.Connect.ServiceNow.Models
Imports System.Text
Imports WS_IControlIT.Connect.ServiceNow.Processors

Namespace Connect.ServiceNow.Handlers

    <WebService(Namespace:="https://www.icontrolit.com.br/Connect/ServiceNow/Handlers/Handler.asmx")>
    <WebServiceBinding()>
    Public Class Handler
        Inherits WebService

        ' Caminho para o arquivo de log
        Private logFilePath As String = "C:\Temp\LogActions.txt"

        ' [INICIO - DIAG-042026] Log diagnostico para investigacao de erros 500 / respostas nulas
        Private diagLogPath As String = "C:\Temp\Log042026.txt"

        Private Sub InicializaDiagLog()
            Try
                Dim dir As String = Path.GetDirectoryName(diagLogPath)
                If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
                If Not File.Exists(diagLogPath) Then File.Create(diagLogPath).Dispose()
            Catch
                ' Engole - nao podemos deixar falha de log mascarar erro real
            End Try
        End Sub

        Private Sub LogDiag(ByVal etapa As String, ByVal correlationId As String, ByVal mensagem As String)
            Try
                InicializaDiagLog()
                Dim linha As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | [Handler] | {etapa} | CID={If(correlationId, "-")} | {mensagem}"
                Using sw As StreamWriter = New StreamWriter(diagLogPath, True)
                    sw.WriteLine(linha)
                End Using
            Catch
                ' Engole - log diagnostico nunca pode lancar
            End Try
        End Sub
        ' [FIM - DIAG-042026]

        ' Método para escrever o log
        Private Sub EscreveLog(ByVal mensagem As String)
            Try
                Using sw As StreamWriter = New StreamWriter(logFilePath, True)
                    sw.WriteLine($"{DateTime.Now}: {mensagem}")
                End Using
            Catch
                ' [DIAG-042026] Nao relanca - falha de log nao pode mascarar exceca real
            End Try
        End Sub

        ' Método para decodificar credenciais Basic Auth
        Private Function DecodificarCredenciais(ByVal credenciaisBase64 As String) As String()
            ' Decodificar as credenciais base64
            Dim credenciais As String = Encoding.UTF8.GetString(Convert.FromBase64String(credenciaisBase64))
            ' Retornar o usuário e a senha
            Return credenciais.Split(":"c)
        End Function

        ' Método principal para processar as requisições
        <WebMethod()>
        Public Function createInstance() As String
            ' [DIAG-042026] CID inicia generico, sera substituido pelo TransactionID/RequestNumber quando disponivel
            Dim cid As String = Guid.NewGuid().ToString("N").Substring(0, 8)
            LogDiag("createInstance.INICIO", cid, $"RemoteIP={Context.Request.UserHostAddress}")

            Try
                ' Verificação do cabeçalho Authorization
                Dim authorizationHeader As String = Context.Request.Headers("Authorization")
                If String.IsNullOrEmpty(authorizationHeader) OrElse Not authorizationHeader.StartsWith("Basic ") Then
                    LogDiag("createInstance.AUTH_FALTANDO", cid, $"HeaderPresente={Not String.IsNullOrEmpty(authorizationHeader)}")
                    Context.Response.StatusCode = 401 ' Unauthorized
                    Return "Erro: Cabeçalho Authorization ausente ou inválido."
                End If

                ' Decodificação das credenciais
                Dim credenciaisBase64 As String = authorizationHeader.Substring(6) ' Remover "Basic "
                Dim credenciais() As String = DecodificarCredenciais(credenciaisBase64)

                ' Verificação das credenciais
                Dim authUser As String = ConfigurationManager.AppSettings("AuthUserSoap")
                Dim authPass As String = ConfigurationManager.AppSettings("AuthPassSoap")
                If credenciais.Length <> 2 OrElse credenciais(0) <> authUser OrElse credenciais(1) <> authPass Then
                    LogDiag("createInstance.AUTH_INVALIDA", cid, $"UsuarioRecebido={If(credenciais.Length > 0, credenciais(0), "-")}")
                    Context.Response.StatusCode = 401 ' Unauthorized
                    Return "Erro: Credenciais inválidas."
                End If

                LogDiag("createInstance.AUTH_OK", cid, $"Usuario={authUser}")

                ' Obtenção do conteúdo do XML da requisição
                Dim httpRequest As HttpRequest = Context.Request
                Dim inputStream As Stream = httpRequest.InputStream
                inputStream.Position = 0 ' Reseta a posição do stream
                Dim xmlData As String = New StreamReader(inputStream).ReadToEnd()

                LogDiag("createInstance.PAYLOAD_RECEBIDO", cid, $"Tamanho={If(xmlData, "").Length} | Payload={xmlData}")

                If String.IsNullOrEmpty(xmlData) Then
                    LogDiag("createInstance.PAYLOAD_VAZIO", cid, "InputStream sem conteudo")
                    Context.Response.StatusCode = 400
                    Return "Erro: O conteúdo do XML está vazio."
                End If

                ' Carregar o XML
                Dim xmlDoc As New XmlDocument()
                Try
                    xmlDoc.LoadXml(xmlData)
                Catch xmlEx As XmlException
                    LogDiag("createInstance.XML_INVALIDO", cid, $"LineNumber={xmlEx.LineNumber} | LinePosition={xmlEx.LinePosition} | Mensagem={xmlEx.Message}")
                    Context.Response.StatusCode = 400
                    Return "Erro: XML mal formado - " & xmlEx.Message
                End Try

                ' Extração do conteúdo de xmlString
                Dim xmlStringNode As XmlNode = xmlDoc.SelectSingleNode("//xmlString")
                If xmlStringNode Is Nothing OrElse String.IsNullOrWhiteSpace(xmlStringNode.InnerText) Then
                    LogDiag("createInstance.XMLSTRING_VAZIO", cid, $"NodeEncontrado={xmlStringNode IsNot Nothing}")
                    Context.Response.StatusCode = 400
                    Return "Erro: O conteúdo do xmlString está vazio ou nulo."
                End If

                Dim xmlString As String = xmlStringNode.InnerText
                LogDiag("createInstance.XMLSTRING_EXTRAIDO", cid, $"Conteudo={xmlString}")

                ' Processar com base no tipo de requisição
                If xmlString.Contains("Profile_request") Then
                    LogDiag("createInstance.ROTA", cid, "Profile_request")
                    Return ProcessProfileActions(CType(ParseProfileXml(xmlString, cid), ProfileActionsRequestModel), cid)

                ElseIf xmlString.Contains("Mobile_request") Then
                    LogDiag("createInstance.ROTA", cid, "Mobile_request")
                    Return ProcessMobileActions(CType(ParseMobileXml(xmlString, cid), MobileActionsRequestModel), cid)
                Else
                    LogDiag("createInstance.TIPO_DESCONHECIDO", cid, $"Trecho={xmlString.Substring(0, Math.Min(200, xmlString.Length))}")
                    Context.Response.StatusCode = 400
                    Return "Erro: Tipo de requisição não reconhecido."
                End If

            Catch ex As Exception
                LogDiag("createInstance.ERRO_GERAL", cid, $"Tipo={ex.GetType().FullName} | Msg={ex.Message} | Stack={ex.StackTrace}")
                Context.Response.StatusCode = 500
                Return ex.Message
            End Try
        End Function



        ' Método para processar ações de perfil (Profile)
        Private Function ProcessProfileActions(ByVal request As ProfileActionsRequestModel, ByVal cid As String) As String
            Try
                EscreveLog("(Handler) Iniciando ProcessProfileActions.")
                LogDiag("ProcessProfileActions.INICIO", cid, $"Action={request.Action} | RequestNumber={request.RequestNumber} | WorkOrderNumber={request.WorkOrderNumber} | UserName={request.UserName}")

                ' Processa a ação de Profile
                Dim processor As New ServiceProcessor()
                Dim idChamado As Integer = processor.ProcessProfileActions(request, cid)

                LogDiag("ProcessProfileActions.SUCESSO", cid, $"Id_Chamado={idChamado}")

                ' Retorna o ID do chamado recém-criado
                Context.Response.StatusCode = 200
                Return idChamado

            Catch ex As Exception
                EscreveLog($"(Handler) Erro em ProcessProfileActions: {ex.Message}")
                LogDiag("ProcessProfileActions.ERRO", cid, $"Tipo={ex.GetType().FullName} | Msg={ex.Message} | Stack={ex.StackTrace}")
                Context.Response.StatusCode = 400
                Return ex.Message
            End Try
        End Function


        ' Método para processar ações móveis (Mobile)
        Private Function ProcessMobileActions(ByVal request As MobileActionsRequestModel, ByVal cid As String) As String
            Try
                EscreveLog("(Handler) Iniciando ProcessMobileActions.")
                LogDiag("ProcessMobileActions.INICIO", cid, $"Action={request.Action} | RequestNumber={request.RequestNumber} | WorkOrderNumber={request.WorkOrderNumber} | UserName={request.UserName} | AdditionalInfoNull={request.AdditionalInformation Is Nothing}")

                ' Processa a ação de Mobile
                Dim processor As New ServiceProcessor()
                Dim idChamado As Integer = processor.ProcessMobileActions(request, cid)

                LogDiag("ProcessMobileActions.SUCESSO", cid, $"Id_Chamado={idChamado}")

                ' Retorna o ID do chamado recém-criado
                Context.Response.StatusCode = 200
                Return idChamado

            Catch ex As Exception
                EscreveLog($"(Handler) Erro em ProcessMobileActions: {ex.Message}")
                LogDiag("ProcessMobileActions.ERRO", cid, $"Tipo={ex.GetType().FullName} | Msg={ex.Message} | Stack={ex.StackTrace}")
                Context.Response.StatusCode = 400
                Return ex.Message
            End Try
        End Function


        ' Método para processar o XML e extrair os dados do Profile
        Private Function ParseProfileXml(ByVal xmlData As String, ByVal cid As String) As ProfileActionsRequestModel
            Dim request As New ProfileActionsRequestModel()
            Try
                EscreveLog("(Handler) Iniciando ParseProfileXml.")
                LogDiag("ParseProfileXml.INICIO", cid, "")
                Dim xmlDoc As New XmlDocument()
                xmlDoc.LoadXml(xmlData)

                ' Gerenciando namespaces
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("ns0", "http://xmlns.oracle.com/Profile_request")

                ' Extrair valores do XML
                request.Action = xmlDoc.SelectSingleNode("//ns0:action", nsmgr)?.InnerText
                request.RequestNumber = xmlDoc.SelectSingleNode("//ns0:requestNumber", nsmgr)?.InnerText
                request.WorkOrderNumber = xmlDoc.SelectSingleNode("//ns0:workOrderNumber", nsmgr)?.InnerText
                request.UserName = xmlDoc.SelectSingleNode("//ns0:userName", nsmgr)?.InnerText
                request.UserNumber = xmlDoc.SelectSingleNode("//ns0:userNumber", nsmgr)?.InnerText
                request.ManagerOrAdm = xmlDoc.SelectSingleNode("//ns0:managerOrAdm", nsmgr)?.InnerText
                request.ViewProfile = xmlDoc.SelectSingleNode("//ns0:viewProfile", nsmgr)?.InnerText
                request.ManagerNumber = xmlDoc.SelectSingleNode("//ns0:managerNumberList", nsmgr)?.InnerText
                request.TransactionID = xmlDoc.SelectSingleNode("//ns0:transactionID", nsmgr)?.InnerText

                LogDiag("ParseProfileXml.CAMPOS", cid,
                    $"Action_Null={request.Action Is Nothing} | RequestNumber_Null={request.RequestNumber Is Nothing} | " &
                    $"WorkOrderNumber_Null={request.WorkOrderNumber Is Nothing} | UserName_Null={request.UserName Is Nothing} | " &
                    $"UserNumber_Null={request.UserNumber Is Nothing} | ManagerOrAdm_Null={request.ManagerOrAdm Is Nothing} | " &
                    $"ViewProfile_Null={request.ViewProfile Is Nothing} | ManagerNumber_Null={request.ManagerNumber Is Nothing} | " &
                    $"TransactionID_Null={request.TransactionID Is Nothing}")

                EscreveLog("(Handler) ParseProfileXml concluído com sucesso.")
                LogDiag("ParseProfileXml.OK", cid, "")
                Return request
            Catch ex As Exception
                EscreveLog($"(Handler) Erro ao processar XML de Profile: {ex.Message}")
                LogDiag("ParseProfileXml.ERRO", cid, $"Tipo={ex.GetType().FullName} | Msg={ex.Message} | Stack={ex.StackTrace}")
                Context.Response.StatusCode = 500
                Throw New Exception(ex.Message)
            End Try
        End Function

        ' Método para processar o XML e extrair os dados do Mobile
        Private Function ParseMobileXml(ByVal xmlData As String, ByVal cid As String) As MobileActionsRequestModel
            Dim request As New MobileActionsRequestModel()
            Try
                EscreveLog("(Handler) Iniciando ParseMobileXml.")
                LogDiag("ParseMobileXml.INICIO", cid, "")
                Dim xmlDoc As New XmlDocument()
                xmlDoc.LoadXml(xmlData)

                ' Gerenciando namespaces
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("ns0", "http://xmlns.oracle.com/Mobile_request")

                ' Extrair valores do XML
                request.Action = xmlDoc.SelectSingleNode("//ns0:action", nsmgr)?.InnerText
                request.RequestNumber = xmlDoc.SelectSingleNode("//ns0:requestNumber", nsmgr)?.InnerText
                request.WorkOrderNumber = xmlDoc.SelectSingleNode("//ns0:workOrderNumber", nsmgr)?.InnerText
                request.UserName = xmlDoc.SelectSingleNode("//ns0:userName", nsmgr)?.InnerText
                request.UserNumber = xmlDoc.SelectSingleNode("//ns0:userNumber", nsmgr)?.InnerText
                request.DesignationProduct = xmlDoc.SelectSingleNode("//ns0:designationProduct", nsmgr)?.InnerText
                request.TelecomProvider = xmlDoc.SelectSingleNode("//ns0:telecomProvider", nsmgr)?.InnerText
                request.FramingPlan = xmlDoc.SelectSingleNode("//ns0:framingPlan", nsmgr)?.InnerText
                request.MigrationDevice = xmlDoc.SelectSingleNode("//ns0:migrationDevice", nsmgr)?.InnerText
                request.ServicePack = xmlDoc.SelectSingleNode("//ns0:servicePack", nsmgr)?.InnerText
                request.NewAreaCode = xmlDoc.SelectSingleNode("//ns0:newAreaCode", nsmgr)?.InnerText
                request.NewUserNumber = xmlDoc.SelectSingleNode("//ns0:newUserNumber", nsmgr)?.InnerText
                request.NewTelecomProvider = xmlDoc.SelectSingleNode("//ns0:newTelecomProvider", nsmgr)?.InnerText
                request.CountryDateForRoaming = xmlDoc.SelectSingleNode("//ns0:countryDateForRoaming", nsmgr)?.InnerText
                request.AdditionalInformation = xmlDoc.SelectSingleNode("//ns0:additionalInformation", nsmgr)?.InnerText
                request.TransactionID = xmlDoc.SelectSingleNode("//ns0:transactionID", nsmgr)?.InnerText

                ' [DIAG-042026] Hipotese B: AdditionalInformation Nothing causa NRE em ServiceProcessor.vb:72
                LogDiag("ParseMobileXml.CAMPOS", cid,
                    $"Action_Null={request.Action Is Nothing} | RequestNumber_Null={request.RequestNumber Is Nothing} | " &
                    $"WorkOrderNumber_Null={request.WorkOrderNumber Is Nothing} | UserName_Null={request.UserName Is Nothing} | " &
                    $"UserNumber_Null={request.UserNumber Is Nothing} | DesignationProduct_Null={request.DesignationProduct Is Nothing} | " &
                    $"AdditionalInformation_Null={request.AdditionalInformation Is Nothing} | " &
                    $"AdditionalInformation_Vazio={String.IsNullOrEmpty(request.AdditionalInformation)} | " &
                    $"TransactionID_Null={request.TransactionID Is Nothing}")

                EscreveLog("(Handler) ParseMobileXml concluído com sucesso.")
                LogDiag("ParseMobileXml.OK", cid, "")
                Return request
            Catch ex As Exception
                EscreveLog($"(Handler) Erro ao processar XML de Mobile: {ex.Message}")
                LogDiag("ParseMobileXml.ERRO", cid, $"Tipo={ex.GetType().FullName} | Msg={ex.Message} | Stack={ex.StackTrace}")
                Context.Response.StatusCode = 500
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace
