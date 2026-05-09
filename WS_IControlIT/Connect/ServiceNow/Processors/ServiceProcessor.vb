' -----------------------------------------------------------------------
' ServiceProcessor.vb
' Autor: Anderson Luiz Chipak
' Data: 05/09/2024
' Descrição:Faz o processamento dos dados
' -----------------------------------------------------------------------
Imports WS_IControlIT.Connect.ServiceNow.Models
Imports System.Web.Services.Protocols
Imports System.IO

Namespace Connect.ServiceNow.Processors
    Public Class ServiceProcessor
        ' Instância do WSChamado para acessar as funções do WebService
        Private WSChamado As New WSChamado()

        ' Diretório e arquivo de log
        Private logFilePath As String = "C:\Temp\Log.txt"

        ' [INICIO - DIAG-042026] Log diagnostico unificado
        Private diagLogPath As String = "C:\Temp\Log042026.txt"

        Private Sub LogDiag(ByVal etapa As String, ByVal correlationId As String, ByVal mensagem As String)
            Try
                Dim dir As String = Path.GetDirectoryName(diagLogPath)
                If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
                Dim linha As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | [ServiceProcessor] | {etapa} | CID={If(correlationId, "-")} | {mensagem}"
                Using sw As StreamWriter = New StreamWriter(diagLogPath, True)
                    sw.WriteLine(linha)
                End Using
            Catch
                ' Engole - log diagnostico nunca pode lancar
            End Try
        End Sub
        ' [FIM - DIAG-042026]

        ' Método para garantir que o arquivo de log pode ser criado ou acessado
        Private Sub InicializaLog()
            Try
                ' Verifica se a pasta existe, senão cria
                Dim logDirectory As String = Path.GetDirectoryName(logFilePath)
                If Not Directory.Exists(logDirectory) Then
                    Directory.CreateDirectory(logDirectory)
                End If

                ' Verifica se o arquivo existe, senão cria
                If Not File.Exists(logFilePath) Then
                    File.Create(logFilePath).Dispose()
                End If
            Catch ex As Exception
                ' Caso ocorra um erro ao criar diretório ou arquivo de log
                Throw New Exception("Erro ao inicializar o arquivo de log: " & ex.Message)
            End Try
        End Sub

        ' Método para escrever log em arquivo de texto
        Private Sub EscreveLog(ByVal mensagem As String)
            Try
                ' Inicializa o log (cria pasta/arquivo se necessário)
                InicializaLog()

                ' Escreve a mensagem no arquivo de log
                Using sw As StreamWriter = New StreamWriter(logFilePath, True)
                    sw.WriteLine($"{DateTime.Now}: {mensagem}")
                End Using
            Catch ex As Exception
                ' Se falhar ao escrever o log, lida com o erro
                Throw New Exception("Erro ao gravar log: " & ex.Message)
            End Try
        End Sub

        ' Método para processar MobileActions
        Public Function ProcessMobileActions(ByVal request As MobileActionsRequestModel, ByVal cid As String) As Integer
            Try
                ' Log da inicialização do processo
                EscreveLog($"(ServiceProcessor)Processando MobileActions para RequestNumber: {request.RequestNumber}")
                LogDiag("ProcessMobileActions.INICIO", cid, $"RequestNumber={request.RequestNumber}")

                ' Prepara a conexão com o banco de dados
                Dim pPConn_Banco As String = ConfigurationManager.AppSettings("VALE")
                LogDiag("ProcessMobileActions.CONFIG", cid, $"VALE_ConfigPresente={Not String.IsNullOrEmpty(pPConn_Banco)} | VALE_Tamanho={If(pPConn_Banco, "").Length}")

                ' Parâmetros da chamada para o WSChamado (insere ou atualiza chamado)
                Dim pPakage As String = "insere_chamado"
                Dim pId_Chamado As Integer = 0 ' ID atual ou 0 caso seja inserção
                Dim pRetorno As Boolean = True

                ' Chama o método Chamado do WSChamado
                EscreveLog("(ServiceProcessor)Chamando WSChamado.Chamado para MobileActions com os seguintes parâmetros:" &
                   $" RequestNumber: {request.RequestNumber}, WorkOrderNumber: {request.WorkOrderNumber}, Action: {request.Action}")

                ' [DIAG-042026] Hipotese B: NRE quando AdditionalInformation eh Nothing
                LogDiag("ProcessMobileActions.PRE_CLEAN", cid, $"AdditionalInformation_IsNothing={request.AdditionalInformation Is Nothing}")
                Dim cleanedAdditionalInfo As String = If(request.AdditionalInformation, "").Replace("description:", "").Trim()
                LogDiag("ProcessMobileActions.POS_CLEAN", cid, $"cleanedAdditionalInfo_Tamanho={cleanedAdditionalInfo.Length}")


                ' Executar o método WSChamado.Chamado e obter o Id_Chamado retornado
                Dim result As DataSet = WSChamado.Chamado(pPConn_Banco,
                                                  pPakage,
                                                  Nothing,
                                                  Nothing,
                                                  pId_Chamado,
                                                  request.RequestNumber,
                                                  request.WorkOrderNumber,
                                                  "Pendente", ' Estado inicial
                                                  Nothing, ' Comentários podem ser nulos
                                                  "MobileActions", ' Atribuído para
                                                  request.Action,
                                                  request.TransactionID,
                                                  Nothing, ' Id_Consumidor
                                                  Nothing, ' Id_Ativo
                                                  Nothing, ' Id_Conglomerado
                                                  request.UserName,
                                                  request.UserNumber,
                                                  request.DesignationProduct,
                                                  request.TelecomProvider,
                                                  request.FramingPlan,
                                                  request.MigrationDevice,
                                                  request.ServicePack,
                                                  request.NewAreaCode,
                                                  request.NewUserNumber,
                                                  request.NewTelecomProvider,
                                                  request.CountryDateForRoaming,
                                                  Nothing, ' ManagerOrAdm
                                                  Nothing, ' ViewProfile
                                                  Nothing, ' ManagerNumber
                                                  cleanedAdditionalInfo,
                                                  Nothing, ' Name
                                                  Nothing, ' pTermoBusca
                                                  pRetorno)

                ' [DIAG-042026] Resultado do WSChamado
                LogDiag("ProcessMobileActions.RESULT", cid,
                    $"ResultIsNothing={result Is Nothing} | " &
                    $"TableCount={If(result Is Nothing, -1, result.Tables.Count)} | " &
                    $"RowCount={If(result Is Nothing OrElse result.Tables.Count = 0, -1, result.Tables(0).Rows.Count)}")

                ' Extrair o Id_Chamado do DataSet retornado
                If result IsNot Nothing AndAlso result.Tables(0).Rows.Count > 0 Then
                    Dim idChamado As Integer = Convert.ToInt32(result.Tables(0).Rows(0)("Id_Chamado"))
                    EscreveLog($"(ServiceProcessor) Id_Chamado retornado: {idChamado}")
                    LogDiag("ProcessMobileActions.OK", cid, $"Id_Chamado={idChamado}")
                    Return idChamado
                Else
                    LogDiag("ProcessMobileActions.SEM_DADOS", cid, "WSChamado.Chamado retornou DataSet vazio ou nulo")
                    Throw New Exception("Erro: Nenhum dado retornado da chamada WSChamado.Chamado.")
                End If

            Catch ex As SoapException
                ' Log de exceção SOAP
                EscreveLog($"(ServiceProcessor) Erro SOAP ao processar MobileActions: {ex.Message}")
                LogDiag("ProcessMobileActions.ERRO_SOAP", cid, $"Code={ex.Code} | Actor={ex.Actor} | Msg={ex.Message} | Detail={If(ex.Detail Is Nothing, "-", ex.Detail.OuterXml)} | Stack={ex.StackTrace}")
                Throw New Exception(ex.Message)
            Catch ex As Exception
                ' Log de exceção genérica
                EscreveLog($"(ServiceProcessor) Erro ao processar MobileActions: {ex.Message}")
                LogDiag("ProcessMobileActions.ERRO", cid, $"Tipo={ex.GetType().FullName} | Msg={ex.Message} | InnerType={If(ex.InnerException Is Nothing, "-", ex.InnerException.GetType().FullName)} | InnerMsg={If(ex.InnerException Is Nothing, "-", ex.InnerException.Message)} | Stack={ex.StackTrace}")
                Throw New Exception(ex.Message)
            End Try
        End Function


        ' Método para processar ProfileActions (similar ao MobileActions)
        Public Function ProcessProfileActions(ByVal request As ProfileActionsRequestModel, ByVal cid As String) As Integer
            Try
                ' Log da inicialização do processo
                EscreveLog($"(ServiceProcessor)Processando ProfileActions para RequestNumber: {request.RequestNumber}")
                LogDiag("ProcessProfileActions.INICIO", cid, $"RequestNumber={request.RequestNumber}")

                ' Prepara a conexão com o banco de dados
                Dim pPConn_Banco As String = ConfigurationManager.AppSettings("VALE")
                LogDiag("ProcessProfileActions.CONFIG", cid, $"VALE_ConfigPresente={Not String.IsNullOrEmpty(pPConn_Banco)} | VALE_Tamanho={If(pPConn_Banco, "").Length}")

                ' Parâmetros da chamada para o WSChamado (insere ou atualiza chamado)
                Dim pPakage As String = "insere_chamado"
                Dim pId_Chamado As Integer = 0 ' ID atual ou 0 caso seja inserção
                Dim pRetorno As Boolean = True

                ' Chama o método Chamado do WSChamado
                EscreveLog("(ServiceProcessor)Chamando WSChamado.Chamado para ProfileActions com os seguintes parâmetros:" &
                   $" RequestNumber: {request.RequestNumber}, WorkOrderNumber: {request.WorkOrderNumber}, Action: {request.Action}")

                ' Executar o método WSChamado.Chamado e obter o Id_Chamado retornado
                Dim result As DataSet = WSChamado.Chamado(pPConn_Banco,
                                                  pPakage,
                                                  Nothing,
                                                  Nothing,
                                                  pId_Chamado,
                                                  request.RequestNumber,
                                                  request.WorkOrderNumber,
                                                  "Pendente", ' Estado inicial
                                                  Nothing, ' Comentários podem ser nulos
                                                  "ProfileActions", ' Atribuído para
                                                  request.Action,
                                                  request.TransactionID,
                                                  Nothing, ' Id_Consumidor
                                                  Nothing, ' Id_Ativo
                                                  Nothing, ' Id_Conglomerado
                                                  request.UserName,
                                                  request.UserNumber,
                                                  Nothing, ' DesignationProduct
                                                  Nothing, ' TelecomProvider
                                                  Nothing, ' FramingPlan
                                                  Nothing, ' MigrationDevice
                                                  Nothing, ' ServicePack
                                                  Nothing, ' NewAreaCode
                                                  Nothing, ' NewUserNumber
                                                  Nothing, ' NewTelecomProvider
                                                  Nothing, ' CountryDateForRoaming
                                                  request.ManagerOrAdm,
                                                  request.ViewProfile,
                                                  request.ManagerNumber,
                                                  Nothing, ' AdditionalInformation
                                                  Nothing, ' Name,
                                                  Nothing, ' pTermoBusca
                                                  pRetorno)

                ' [DIAG-042026] Resultado do WSChamado
                LogDiag("ProcessProfileActions.RESULT", cid,
                    $"ResultIsNothing={result Is Nothing} | " &
                    $"TableCount={If(result Is Nothing, -1, result.Tables.Count)} | " &
                    $"RowCount={If(result Is Nothing OrElse result.Tables.Count = 0, -1, result.Tables(0).Rows.Count)}")

                ' Extrair o Id_Chamado do DataSet retornado
                If result IsNot Nothing AndAlso result.Tables(0).Rows.Count > 0 Then
                    Dim idChamado As Integer = Convert.ToInt32(result.Tables(0).Rows(0)("Id_Chamado"))
                    EscreveLog($"(ServiceProcessor) Id_Chamado retornado: {idChamado}")
                    LogDiag("ProcessProfileActions.OK", cid, $"Id_Chamado={idChamado}")
                    Return idChamado
                Else
                    LogDiag("ProcessProfileActions.SEM_DADOS", cid, "WSChamado.Chamado retornou DataSet vazio ou nulo")
                    Throw New Exception("Erro: Nenhum dado retornado da chamada WSChamado.Chamado.")
                End If

            Catch ex As SoapException
                ' Log de exceção SOAP
                EscreveLog($"(ServiceProcessor) Erro SOAP ao processar ProfileActions: {ex.Message}")
                LogDiag("ProcessProfileActions.ERRO_SOAP", cid, $"Code={ex.Code} | Actor={ex.Actor} | Msg={ex.Message} | Detail={If(ex.Detail Is Nothing, "-", ex.Detail.OuterXml)} | Stack={ex.StackTrace}")
                Throw New Exception(ex.Message)
            Catch ex As Exception
                ' Log de exceção genérica
                EscreveLog($"(ServiceProcessor) Erro ao processar ProfileActions: {ex.Message}")
                LogDiag("ProcessProfileActions.ERRO", cid, $"Tipo={ex.GetType().FullName} | Msg={ex.Message} | InnerType={If(ex.InnerException Is Nothing, "-", ex.InnerException.GetType().FullName)} | InnerMsg={If(ex.InnerException Is Nothing, "-", ex.InnerException.Message)} | Stack={ex.StackTrace}")
                Throw New Exception(ex.Message)
            End Try
        End Function





    End Class
End Namespace
