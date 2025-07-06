' -----------------------------------------------------------------------
' WSChamado.asmx
' Autor: Anderson Luiz Chipak
' Data: 05/09/2024
' Descrição: Web Service para os Chamados, com funcionalidade para buscar
' e atualizar chamados e suas relações.
' -----------------------------------------------------------------------
' Histórico de Modificações
' ICTRL-NF-202506-012 - 2025-06-22 - Anderson Chipak
' -----------------------------------------------------------------------

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.IO ' Para o log em arquivo de texto

<WebService(Namespace:="WSChamado", Name:="WSChamado")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSChamado
    Inherits WebService

    ' Instância da classe oBanco
    Dim oBanco As cls_Banco

    ' Diretório e arquivo de log
    Private logFilePath As String = "C:\Temp\Log.txt"

    ' Construtor para inicializar o banco de dados
    Public Sub New()
        Try
            oBanco = New cls_Banco()
            If oBanco Is Nothing Then
                Throw New Exception("Erro ao inicializar o objeto oBanco.")
            End If
        Catch ex As Exception
            ' Log de erro ao inicializar o banco
            EscreveLog("(WSChamado) Erro ao inicializar o banco: " & ex.Message)
            Throw New Exception("Erro ao inicializar o banco: " & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

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

    ' Verifica se o oBanco foi inicializado corretamente
    Private Sub VerificaOBanco()
        If oBanco Is Nothing Then
            Dim mensagemErro As String = "Erro: oBanco não foi inicializado."
            EscreveLog(mensagemErro)
            Throw New Exception(mensagemErro)
        End If
    End Sub








    ' Função para inserir, atualizar ou consultar chamados
    <WebMethod()>
    Public Function Chamado(ByVal pPConn_Banco As String,
                            ByVal pPakage As String,
                            ByVal pageNumber As Integer,
                            ByVal pageSize As Integer,
                            ByVal idChamado As Integer,
                            ByVal requestNumber As String,
                            ByVal workOrderNumber As String,
                            ByVal estado As String,
                            ByVal comentarios As String,
                            ByVal atribuidoPara As String,
                            ByVal tipoSolicitacao As String,
                            ByVal transactionID As String,
                            ByVal idConsumidor As Integer,
                            ByVal idAtivo As Integer,
                            ByVal idConglomerado As Integer,
                            ByVal userName As String,
                            ByVal userNumber As String,
                            ByVal designationProduct As String,
                            ByVal telecomProvider As String,
                            ByVal framingPlan As String,
                            ByVal migrationDevice As String,
                            ByVal servicePack As String,
                            ByVal newAreaCode As String,
                            ByVal newUserNumber As String,
                            ByVal newTelecomProvider As String,
                            ByVal countryDateForRoaming As String,
                            ByVal managerOrAdm As String,
                            ByVal viewProfile As String,
                            ByVal managerNumber As String,
                            ByVal additionalInformation As String,
                            ByVal name As String,
                            ByVal pTermoBusca As String, ' [ICTRL-NF-202506-012]
                            ByVal pRetorno As Boolean) As DataSet
        Try

            VerificaOBanco()

            ' Log de início da operação
            EscreveLog($"(WSChamado) Action: {tipoSolicitacao}, Chamado ID: {idChamado}, pPakage: {pPakage}")

            If String.IsNullOrEmpty(pPConn_Banco) Then Throw New Exception("pPConn_Banco está vazio.")
            ' Carregar a lista de ações permitidas a partir do web.config
            Dim acoesPermitidasConfig As String = ConfigurationManager.AppSettings("AcoesPermitidas")
            Dim acoesValidas As List(Of String) = acoesPermitidasConfig.Split(","c).Select(Function(a) a.Trim().ToUpper()).ToList()
            ' Verificações de parâmetros
            If pPakage.Trim().ToLower() <> "busca_todos_dados" Then
                If String.IsNullOrEmpty(tipoSolicitacao) Then Throw New Exception("O action está vazio. O campo action deve, obrigatoriamente, estar preenchido com a ação a ser realizada.")

                ' Validação do tipoSolicitacao
                If Not acoesValidas.Contains(tipoSolicitacao.ToUpper()) Then
                    Dim acoesPermitidasMensagem As String = String.Join(", ", acoesValidas)
                    Throw New Exception($"O action é inválido. O campo action deve conter uma das ações permitidas. Atualmente, as ações permitidas são: {acoesPermitidasMensagem}.")
                End If

                If String.IsNullOrEmpty(requestNumber) Then Throw New Exception("requestNumber está vazio.")
                If String.IsNullOrEmpty(workOrderNumber) Then Throw New Exception("workOrderNumber está vazio.")

            End If

            ' Inicializa a lista dinâmica para os parâmetros
            Dim parametros As New List(Of SqlClient.SqlParameter)()
            ' Adiciona os parâmetros à lista diretamente
            parametros.Add(New SqlClient.SqlParameter("@pPakage", pPakage))
            parametros.Add(New SqlClient.SqlParameter("@pageNumber", pageNumber))
            parametros.Add(New SqlClient.SqlParameter("@pageSize", pageSize))
            parametros.Add(New SqlClient.SqlParameter("@pId_Chamado", idChamado))
            parametros.Add(New SqlClient.SqlParameter("@requestNumber", requestNumber))
            parametros.Add(New SqlClient.SqlParameter("@workOrderNumber", workOrderNumber))
            parametros.Add(New SqlClient.SqlParameter("@estado", estado))
            parametros.Add(New SqlClient.SqlParameter("@comentarios", comentarios))
            parametros.Add(New SqlClient.SqlParameter("@atribuidoPara", atribuidoPara))
            parametros.Add(New SqlClient.SqlParameter("@tipoSolicitacao", tipoSolicitacao))
            parametros.Add(New SqlClient.SqlParameter("@transactionID", transactionID))
            parametros.Add(New SqlClient.SqlParameter("@idConsumidor", idConsumidor))
            parametros.Add(New SqlClient.SqlParameter("@idAtivo", idAtivo))
            parametros.Add(New SqlClient.SqlParameter("@idConglomerado", idConglomerado))



            ' Dados auxiliares
            parametros.Add(New SqlClient.SqlParameter("@userName", userName))
            parametros.Add(New SqlClient.SqlParameter("@userNumber", userNumber))
            parametros.Add(New SqlClient.SqlParameter("@designationProduct", If(designationProduct, "").Replace("-", String.Empty)))
            parametros.Add(New SqlClient.SqlParameter("@telecomProvider", telecomProvider))
            parametros.Add(New SqlClient.SqlParameter("@framingPlan", framingPlan))
            parametros.Add(New SqlClient.SqlParameter("@migrationDevice", migrationDevice))
            parametros.Add(New SqlClient.SqlParameter("@servicePack", servicePack))
            parametros.Add(New SqlClient.SqlParameter("@newAreaCode", newAreaCode))
            parametros.Add(New SqlClient.SqlParameter("@newUserNumber", newUserNumber))
            parametros.Add(New SqlClient.SqlParameter("@newTelecomProvider", newTelecomProvider))
            parametros.Add(New SqlClient.SqlParameter("@countryDateForRoaming", countryDateForRoaming))
            parametros.Add(New SqlClient.SqlParameter("@additionalInformation", additionalInformation))
            parametros.Add(New SqlClient.SqlParameter("@name", name))

            ' Verificações para ProfileActions
            If Not String.IsNullOrEmpty(managerOrAdm) Then
                parametros.Add(New SqlClient.SqlParameter("@managerOrAdm", managerOrAdm))
            End If
            If Not String.IsNullOrEmpty(viewProfile) Then
                parametros.Add(New SqlClient.SqlParameter("@viewProfile", viewProfile))
            End If
            If Not String.IsNullOrEmpty(managerNumber) Then
                parametros.Add(New SqlClient.SqlParameter("@managerNumber", managerNumber))
            End If

            ' [INÍCIO - ICTRL-NF-202506-012]
            parametros.Add(New SqlClient.SqlParameter("@pTermoBusca", pTermoBusca))
            ' [FIM - ICTRL-NF-202506-012]

            ' Converte a lista para array e retorna o resultado da query
            Return oBanco.retorna_Query("dbo.pa_Chamado", parametros.ToArray(), pPConn_Banco)

        Catch ex As SqlClient.SqlException
            EscreveLog(ex.Message)
            Throw New Exception(ex.Message)

        Catch ex As Exception
            ' Log do erro detalhado
            EscreveLog("(WSChamado) Erro em Chamado: " & ex.Message & vbCrLf & ex.StackTrace)
            Throw New Exception(ex.Message)
        End Try
    End Function







    <WebMethod()>
    Public Function ExecutarAcaoAtivo(ByVal pPConn_Banco As String,
                                       ByVal procedureName As String,
                                       ByVal pPakage As String,
                                       ByVal pId_Chamado As Integer,
                                       ByVal pComentariosAtivo As String,
                                       ByVal pCampo1 As String,
                                       ByVal pCampo2 As String,
                                       ByVal pCampo3 As String,
                                       ByVal pCampo4 As String,
                                       ByVal pCampo5 As String,
                                       ByVal pCampo6 As String,
                                       ByVal pRetorno As Boolean) As String
        Try
            ' Verificação de parâmetros
            If String.IsNullOrEmpty(pPConn_Banco) Then Throw New Exception("pPConn_Banco está vazio.")
            If String.IsNullOrEmpty(pPakage) Then Throw New Exception("pPakage está vazio.")
            If String.IsNullOrEmpty(procedureName) Then Throw New Exception("Nome da procedure está vazio.")

            ' Parâmetros a serem passados para a procedure
            Dim parametros As New List(Of SqlClient.SqlParameter)()
            parametros.Add(New SqlClient.SqlParameter("@pPAKAGE", pPakage))
            parametros.Add(New SqlClient.SqlParameter("@pId_Chamado", pId_Chamado))
            parametros.Add(New SqlClient.SqlParameter("@pComentariosAtivo", pComentariosAtivo))
            parametros.Add(New SqlClient.SqlParameter("@pCampo1", pCampo1))
            parametros.Add(New SqlClient.SqlParameter("@pCampo2", pCampo2))
            parametros.Add(New SqlClient.SqlParameter("@pCampo3", pCampo3))
            parametros.Add(New SqlClient.SqlParameter("@pCampo4", pCampo4))
            parametros.Add(New SqlClient.SqlParameter("@pCampo5", pCampo5))
            parametros.Add(New SqlClient.SqlParameter("@pCampo6", pCampo6))

            ' Parâmetro de saída para mensagem de sucesso ou erro
            Dim mensagemRetorno As New SqlClient.SqlParameter("@MensagemRetorno", SqlDbType.NVarChar, 4000)
            mensagemRetorno.Direction = ParameterDirection.Output
            parametros.Add(mensagemRetorno)

            ' Chamada à procedure especificada e obtenção do resultado
            oBanco.retorna_Query(procedureName, parametros.ToArray(), pPConn_Banco)

            ' Capturar a mensagem de retorno e registrar no log
            Dim mensagem As String = mensagemRetorno.Value.ToString()

            ' Retornar a mensagem de sucesso ou erro para o chamador
            Return mensagem

        Catch ex As SqlClient.SqlException
            EscreveLog(ex.Message)
            Return $"Erro SQL: {ex.Message}"
        Catch ex As Exception
            EscreveLog($"(WSChamado) Erro em ExecutarProcedureAtivo: {ex.Message}")
            Return $"Erro Geral: {ex.Message}"
        End Try
    End Function








    'Essa função vai chamar a procedure pa_Operadores_Email para realizar operações de CRUD na tabela Operadores_Email.
    <WebMethod()>
    Public Function ChamadoAuxiliar(ByVal pPConn_Banco As String,
                                ByVal pAcao As String,
                                ByVal id_Conglomerado As Integer,
                                ByVal id_Ativo As Integer,
                                ByVal pEmailDestino As String,
                                ByVal pEmailCopia As String,
                                ByVal id_Mail_Sender As Integer,
                                ByVal pTextoAdicional As String,
                                ByVal pAssuntoEmail As String,
                                ByVal pNmUsuarioAtual As String,
                                ByVal pId_Chamado As String,
                                ByVal pRetorno As Boolean) As DataSet
        Try
            If String.IsNullOrEmpty(pPConn_Banco) Then Throw New Exception("pPConn_Banco está vazio.")
            If String.IsNullOrEmpty(pAcao) Then Throw New Exception("pAcao está vazio.")

            Dim parametros As New List(Of SqlClient.SqlParameter)()
            parametros.Add(New SqlClient.SqlParameter("@pAcao", pAcao))
            parametros.Add(New SqlClient.SqlParameter("@id_Conglomerado", id_Conglomerado))
            parametros.Add(New SqlClient.SqlParameter("@id_Ativo", id_Ativo))
            parametros.Add(New SqlClient.SqlParameter("@pEmailDestino", pEmailDestino))
            parametros.Add(New SqlClient.SqlParameter("@pEmailCopia", pEmailCopia))
            parametros.Add(New SqlClient.SqlParameter("@id_Mail_Sender", id_Mail_Sender))
            parametros.Add(New SqlClient.SqlParameter("@pTextoAdicional", pTextoAdicional))
            parametros.Add(New SqlClient.SqlParameter("@pNmUsuarioAtual", pNmUsuarioAtual))
            parametros.Add(New SqlClient.SqlParameter("@pId_Chamado", pId_Chamado))
            parametros.Add(New SqlClient.SqlParameter("@pAssuntoEmail", pAssuntoEmail))


            Return oBanco.retorna_Query("dbo.pa_ChamadoAuxiliar", parametros.ToArray(), pPConn_Banco)

        Catch ex As SqlClient.SqlException
            EscreveLog(ex.Message)
            Throw New Exception(ex.Message)

        Catch ex As Exception
            EscreveLog($"(WSChamado.ChamadoAuxiliar) Erro em OperadoresEmail: {ex.Message}")
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod()>
    Public Function ChamadoAuxiliarComAnexos(ByVal pPConn_Banco As String, ByVal pAcao As String, ByVal id_Conglomerado As Integer, ByVal id_Ativo As Integer, ByVal pEmailDestino As String, ByVal pEmailCopia As String, ByVal id_Mail_Sender As Integer, ByVal pTextoAdicional As String, ByVal pAssuntoEmail As String, ByVal pEnderecoArquivo As String, ByVal pNmUsuarioAtual As String, ByVal pId_Chamado As String, ByVal pRetorno As Boolean) As DataSet
        Try
            Dim parametros As New List(Of SqlClient.SqlParameter)()
            parametros.Add(New SqlClient.SqlParameter("@pAcao", pAcao))
            parametros.Add(New SqlClient.SqlParameter("@id_Conglomerado", id_Conglomerado))
            parametros.Add(New SqlClient.SqlParameter("@id_Ativo", id_Ativo))
            parametros.Add(New SqlClient.SqlParameter("@pEmailDestino", pEmailDestino))
            parametros.Add(New SqlClient.SqlParameter("@pEmailCopia", pEmailCopia))
            parametros.Add(New SqlClient.SqlParameter("@id_Mail_Sender", id_Mail_Sender))
            parametros.Add(New SqlClient.SqlParameter("@pTextoAdicional", pTextoAdicional))
            parametros.Add(New SqlClient.SqlParameter("@pAssuntoEmail", pAssuntoEmail))
            parametros.Add(New SqlClient.SqlParameter("@pEnderecoArquivo", pEnderecoArquivo))
            parametros.Add(New SqlClient.SqlParameter("@pNmUsuarioAtual", pNmUsuarioAtual))
            parametros.Add(New SqlClient.SqlParameter("@pId_Chamado", pId_Chamado))

            Return oBanco.retorna_Query("dbo.pa_ChamadoAuxiliar", parametros.ToArray(), pPConn_Banco)

        Catch ex As SqlClient.SqlException
            EscreveLog($"============================================================================================")
            EscreveLog($"(WSChamado.ChamadoAuxiliarComAnexos) Erro ao chamar a procedure: {ex.Message}")
            Throw New Exception(ex.Message)

        Catch ex As Exception
            EscreveLog($"============================================================================================")
            EscreveLog($"(WSChamado.ChamadoAuxiliarComAnexos) Erro ao chamar a procedure: {ex.Message}")
            Throw New Exception($"Erro ao chamar a procedure: {ex.Message}")

        End Try

    End Function

    ' [INÍCIO - ICTRL-NF-202506-017]
    <WebMethod()>
    Public Function AtualizarFlagManualChamado(ByVal pPConn_Banco As String, ByVal pId_Chamado As Integer, ByVal pFl_Manual As Boolean) As String
        Try
            If String.IsNullOrEmpty(pPConn_Banco) Then Throw New Exception("pPConn_Banco está vazio.")

            Dim parametros As New List(Of SqlClient.SqlParameter)()
            parametros.Add(New SqlClient.SqlParameter("@pAcao", "atualizar_flag_manual"))
            parametros.Add(New SqlClient.SqlParameter("@pId_Chamado", pId_Chamado))
            parametros.Add(New SqlClient.SqlParameter("@pFl_Manual", pFl_Manual))

            ' Chama a procedure auxiliar com a nova ação
            Dim ds As DataSet = oBanco.retorna_Query("dbo.pa_ChamadoAuxiliar", parametros.ToArray(), pPConn_Banco)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0)("Mensagem").ToString()
            Else
                Return "Operação concluída, mas sem retorno específico do banco de dados."
            End If

        Catch ex As Exception
            EscreveLog($"(WSChamado.AtualizarFlagManualChamado) Erro: {ex.Message}")
            Throw New Exception(ex.Message)
        End Try
    End Function
    ' [FIM - ICTRL-NF-202506-017]

End Class
