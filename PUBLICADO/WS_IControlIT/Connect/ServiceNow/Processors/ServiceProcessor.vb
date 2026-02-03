' -----------------------------------------------------------------------
' ServiceProcessor.vb
' Autor: Anderson Luiz Chipak
' Data: 05/09/2024
' Descrição:Faz o processamento dos dados
' -----------------------------------------------------------------------
' Histórico de Modificações
' TICKET - DATA - NOME COMPLETO
' [ICTRL-NF-202506-012 | 2025-06-22 | Anderson Chipak]
' [ICTRL-NF-202506-014 | 2025-07-04 | Anderson Chipak]
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
        Public Function ProcessMobileActions(ByVal request As MobileActionsRequestModel) As Integer
            Try
                ' Log da inicialização do processo
                EscreveLog($"(ServiceProcessor)Processando MobileActions para RequestNumber: {request.RequestNumber}")

                ' Prepara a conexão com o banco de dados
                Dim pPConn_Banco As String = ConfigurationManager.AppSettings("VALE")

                ' Parâmetros da chamada para o WSChamado (insere ou atualiza chamado)
                Dim pPakage As String = "insere_chamado"
                Dim pId_Chamado As Integer = 0 ' ID atual ou 0 caso seja inserção
                Dim pRetorno As Boolean = True

                ' Chama o método Chamado do WSChamado
                EscreveLog("(ServiceProcessor)Chamando WSChamado.Chamado para MobileActions com os seguintes parâmetros:" &
                   $" RequestNumber: {request.RequestNumber}, WorkOrderNumber: {request.WorkOrderNumber}, Action: {request.Action}")

                Dim cleanedAdditionalInfo As String = request.AdditionalInformation.Replace("description:", "").Trim()


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

                ' [INÍCIO - ICTRL-NF-202506-014]
                ' Extrair o Id_Chamado do DataSet retornado
                If result IsNot Nothing AndAlso result.Tables(0).Rows.Count > 0 Then
                    Dim idChamado As Integer = Convert.ToInt32(result.Tables(0).Rows(0)("Id_Chamado"))
                    EscreveLog($"(ServiceProcessor) Id_Chamado criado: {idChamado}")

                    ' Se a ação for "ALTERAR PROPRIETARIO", executa o chamado automaticamente
                    If request.Action.ToUpper() = "ALTERAR PROPRIETARIO" Then
                        Try
                            EscreveLog($"(ServiceProcessor) Executando automaticamente o chamado {idChamado} de Alteração de Proprietário.")
                            Dim comentariosExecucao As String = "Execução automática via integração ServiceNow."

                            Dim resultadoExecucao As String = WSChamado.ExecutarAcaoAtivo(pPConn_Banco,
                                                                              "dbo.pa_Ativo_Chamado",
                                                                              "alterar-proprietario",
                                                                              idChamado,
                                                                              comentariosExecucao,
                                                                              Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                              True)

                            EscreveLog($"(ServiceProcessor) Resultado da execução automática para o chamado {idChamado}: {resultadoExecucao}")

                        Catch exExecucao As Exception
                            ' Loga o erro mas não impede o fluxo de retorno do ID do chamado.
                            ' O chamado foi criado, mas a execução automática falhou.
                            EscreveLog($"(ServiceProcessor) ERRO na execução automática do chamado {idChamado}: {exExecucao.Message}")
                        End Try
                    End If

                    Return idChamado
                Else
                    Throw New Exception("Erro: Nenhum dado retornado da chamada WSChamado.Chamado.")
                End If
                ' [FIM - ICTRL-NF-202506-014]

            Catch ex As SoapException
                ' Log de exceção SOAP
                EscreveLog($"(ServiceProcessor) Erro SOAP ao processar MobileActions: {ex.Message}")
                Throw New Exception(ex.Message)
            Catch ex As Exception
                ' Log de exceção genérica
                EscreveLog($"(ServiceProcessor) Erro ao processar MobileActions: {ex.Message}")
                Throw New Exception(ex.Message)
            End Try
        End Function


        ' Método para processar ProfileActions (similar ao MobileActions)
        Public Function ProcessProfileActions(ByVal request As ProfileActionsRequestModel) As Integer
            Try
                ' Log da inicialização do processo
                EscreveLog($"(ServiceProcessor)Processando ProfileActions para RequestNumber: {request.RequestNumber}")

                ' Prepara a conexão com o banco de dados
                Dim pPConn_Banco As String = ConfigurationManager.AppSettings("VALE")

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

                ' Extrair o Id_Chamado do DataSet retornado
                If result IsNot Nothing AndAlso result.Tables(0).Rows.Count > 0 Then
                    Dim idChamado As Integer = Convert.ToInt32(result.Tables(0).Rows(0)("Id_Chamado"))
                    EscreveLog($"(ServiceProcessor) Id_Chamado retornado: {idChamado}")
                    Return idChamado
                Else
                    Throw New Exception("Erro: Nenhum dado retornado da chamada WSChamado.Chamado.")
                End If

            Catch ex As SoapException
                ' Log de exceção SOAP
                EscreveLog($"(ServiceProcessor) Erro SOAP ao processar ProfileActions: {ex.Message}")
                Throw New Exception(ex.Message)
            Catch ex As Exception
                ' Log de exceção genérica
                EscreveLog($"(ServiceProcessor) Erro ao processar ProfileActions: {ex.Message}")
                Throw New Exception(ex.Message)
            End Try
        End Function





    End Class
End Namespace
