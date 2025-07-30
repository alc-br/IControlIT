' -----------------------------------------------------------------------
' Consulta_Chamado.aspx.vb
' Autor: Anderson Luiz Chipak
' Data: 05/09/2024
' Descrição:Code Behind para Consulta de Chamados
' -----------------------------------------------------------------------
' /*
' * HISTÓRICO DE MODIFICAÇÕES
' * [ICTRL-NF-202506-006 | 2025-06-22 | ANDERSON LUIZ CHIPAK]
' * [ICTRL-NF-202506-012 | 2025-06-22 | ANDERSON LUIZ CHIPAK]
' * [ICTRL-NF-202506-013 | 2025-06-22 | ANDERSON LUIZ CHIPAK]
' * [ICTRL-NF-202506-001 | 2025-06-21 | ANDERSON LUIZ CHIPAK]
' * [ICTRL-NF-202506-008 | 2025-06-24 | ANDERSON LUIZ CHIPAK]
' * [ICTRL-NF-202506-017 | 2025-06-24 | ANDERSON LUIZ CHIPAK]
' * [ICTRL-NF-202506-007 | 2025-07-04 | ANDERSON LUIZ CHIPAK]
' * [ICTRL-NF-202506-004 | 2025-07-08 | ANDERSON LUIZ CHIPAK]
' */
Imports System.IO
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Web.Script.Serialization
Imports System.Web.Services
Imports System.Text
Imports iTextSharp.text.pdf.codec.wmf
Imports Microsoft.Extensions.Logging
Imports System.Threading.Tasks
Imports System.Net
Imports System.Drawing
Imports System.Windows.Interop
Imports HtmlAgilityPack
Imports System.Text.RegularExpressions
Public Class AnexoModel
    Public Property NomeArquivo As String
    Public Property CaminhoArquivo As String
End Class
Public Class Consulta_Chamado
    Inherits System.Web.UI.Page
    Public WithEvents hfFlManual As System.Web.UI.HtmlControls.HtmlInputText ' [ICTRL-NF-202506-017]
    Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
    Dim vdataset As Data.DataSet
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' Verifica se o postback foi acionado pelo BtnExecutar
        If IsPostBack AndAlso Request("__EVENTTARGET") = "BtnExecutar" Then
            ' Chama o método assíncrono BtnExecutar_Click usando um handler assíncrono
            Dim task As Task = BtnExecutar_Click(Nothing, EventArgs.Empty)
            task.Wait() ' Aguarda a conclusão da tarefa assíncrona
            Return ' Evita a execução do restante do Page_Load
        End If
        ' Executa o carregamento inicial de dados se não for um postback
        If Not IsPostBack Then
            Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)
            Dim connBancoValue = Session("Conn_Banco")
            If String.IsNullOrEmpty(connBancoValue) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('A string de conexão está vazia.');", True)
            Else
                BindChamados(1, itemsPerPage)
                BindEmpresaContratante()
            End If
        End If
    End Sub
    Private Sub BindChamados(ByVal pageNumber As Integer, ByVal pageSize As Integer)
        Try
            ' Inicialização da conexão com o banco de dados
            Dim pPConn_Banco As String = Session("Conn_Banco")
            If String.IsNullOrEmpty(pPConn_Banco) Then
                Throw New InvalidOperationException("A conexão com o banco de dados não foi encontrada.")
            End If
            EscreveLog("pRetorno: True")
            ' Chamada inicial para buscar dados do Chamado
            Dim vdataset As DataSet = WS_Chamado.Chamado(pPConn_Banco, "busca_todos_dados", pageNumber, pageSize, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Me.TermoBusca, True)

            ' Verifica se o DataSet contém informações
            If vdataset IsNot Nothing AndAlso vdataset.Tables.Count > 0 Then
                ' Log para exibir nomes de colunas e dados do DataSet
                For Each table As DataTable In vdataset.Tables
                    EscreveLog("Tabela: " & table.TableName)
                    Dim columnNames As String = String.Join(", ", table.Columns.Cast(Of DataColumn).Select(Function(col) col.ColumnName))
                    EscreveLog("Colunas: " & columnNames)
                    For Each row As DataRow In table.Rows
                        Dim rowData As String = String.Join(", ", row.ItemArray)
                        EscreveLog("Dados: " & rowData)
                    Next
                Next
                ' Atribui o DataSet diretamente ao rptChamados
                rptChamados.DataSource = vdataset.Tables(0)
                rptChamados.DataBind()
            Else
                rptChamados.DataSource = Nothing
                rptChamados.DataBind()
            End If
        Catch ex As Exception
            Dim mensagemErro As String = ex.Message
            If mensagemErro.Contains("( ") AndAlso mensagemErro.Contains(" )") Then
                ' Esse trecho indica que o erro pode estar vindo do SQL Server com o conteúdo de @MsgErro
                EscreveLog("===========================================================================================")
                EscreveLog("(Consulta_Chamado.aspx.vb) Erro do SQL Server: " & mensagemErro & vbCrLf & ex.StackTrace)
            Else
                ' Captura qualquer outra exceção
                EscreveLog("===========================================================================================")
                EscreveLog("(Consulta_Chamado.aspx.vb) Erro Genérico: " & mensagemErro & vbCrLf & ex.StackTrace)
            End If
        End Try
    End Sub
    Private Property CurrentPage As Integer
        Get
            Return If(ViewState("CurrentPage") Is Nothing, 1, Convert.ToInt32(ViewState("CurrentPage")))
        End Get
        Set(ByVal value As Integer)
            ViewState("CurrentPage") = value
        End Set
    End Property
    Protected Sub BtnPreviousPage_Click(sender As Object, e As EventArgs)
        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)
        If CurrentPage > 1 Then
            CurrentPage -= 1
            BindChamados(CurrentPage, itemsPerPage)
            lblPageNumber.Text = CurrentPage.ToString()
        End If
    End Sub
    Protected Sub BtnNextPage_Click(sender As Object, e As EventArgs)
        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)
        CurrentPage += 1
        BindChamados(CurrentPage, itemsPerPage)
        lblPageNumber.Text = CurrentPage.ToString()
    End Sub
    Protected Async Function BtnExecutar_Click(sender As Object, e As EventArgs) As Task
        ' [INÍCIO - ICTRL-NF-202506-017]
        ' Esta nova versão verifica o texto do botão para decidir qual ação tomar.
        Dim buttonValue As String = Request.Form(btnExecutar.UniqueID)
        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)

        If buttonValue = "Salvar" Then
            ' AÇÃO: Apenas salvar o estado da flag "Manual".
            Dim isManual As Boolean = If(hfFlManual.Value = "1", True, False)
            Try
                Dim idChamado As Integer = Convert.ToInt32(hfIdChamado.Value)
                ' A chamada ao Web Service para salvar a flag.
                WS_Chamado.Credentials = System.Net.CredentialCache.DefaultCredentials
                Dim msg As String = WS_Chamado.AtualizarFlagManualChamado(Session("Conn_Banco"), idChamado, isManual)

                ' Recarrega a lista e mostra a mensagem de sucesso.
                BindChamados(CurrentPage, itemsPerPage)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{msg.Replace("'", "\'")}');", True)
            Catch ex As Exception
                EscreveLog("(Consulta_Chamado.BtnExecutar_Click) Erro ao salvar flag manual: " & ex.Message)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('Erro ao salvar: {ex.Message.Replace("'", "\'")}');", True)
            End Try
        Else
            ' AÇÃO: Executar o chamado normalmente (lógica original).
            Try
                Dim mensagem As String = ExecutarAcaoChamado()
                Try
                    DispararRequisicaoSimples("ENCERRADA", mensagem)
                Catch exAsApi As Exception
                    mensagem = mensagem & $" ATENÇÃO: Erro no retorno ao ServiceNow: {exAsApi.Message}"
                End Try

                Dim mensagemJs As String = HttpUtility.JavaScriptStringEncode($"Chamado processado. {mensagem}")
                BindChamados(CurrentPage, itemsPerPage)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{mensagemJs}');", True)

            Catch ex As Exception
                BindChamados(CurrentPage, itemsPerPage)
                EscreveLog("(Consulta_Chamado.BtnExecutar_Click) Erro ao executar a ação: " & ex.Message)

                Dim erroJs As String = HttpUtility.JavaScriptStringEncode(ex.Message)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{erroJs}');", True)
            End Try
        End If
        ' [FIM - ICTRL-NF-202506-017]
    End Function
    Public Sub DispararRequisicaoSimples(estadoChamado As String, mensagem As String)
        Try
            ' Forçar o uso do protocolo TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls
            ' Configurar os dados de autenticação e URL
            Dim authSistel As String = ConfigurationManager.AppSettings("auth_sistel")
            Dim apiUrl As String = ConfigurationManager.AppSettings("url_api_sistel")
            Dim assignedTo As String = ConfigurationManager.AppSettings("assigned_to_sistel")
            ' Montar os dados do corpo da requisição
            ' [INÍCIO - ICTRL-NF-202506-008]
            ' Montar os dados do corpo da requisição
            Dim registrationValue As String

            ' Se a solicitação for de uma nova linha e o campo da nova linha estiver preenchido,
            ' usamos o novo número. Caso contrário, mantém-se o valor original.
            If hfTipoSolicitacao.Value.ToUpper() = "NOVA LINHA" AndAlso Not String.IsNullOrEmpty(hfNovaLinha.Value) Then
                registrationValue = hfNovaLinha.Value
            Else
                registrationValue = hfDesignationProduct.Value
            End If

            Dim requestData As New With {
            .Action = hfTipoSolicitacao.Value,
            .RequestNumber = hfRequestNumber.Value,
            .WorkOrderNumber = hfWorkOrderNumber.Value,
            .Registration = registrationValue, ' Utiliza o valor definido dinamicamente
            .Status = estadoChamado,
            .Description = mensagem,
            .assigned_to = assignedTo,
            .iControlNumber = hfIdChamado.Value
            }
            ' [FIM - ICTRL-NF-202506-008]
            ' Serializar os dados para JSON
            Dim jsonRequestBody As String = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            ' Gravar no log tudo que está sendo enviado
            EscreveLog("======= ENVIO DE REQUISIÇÃO SIMPLES =======")
            EscreveLog($"API URL: {apiUrl}")
            EscreveLog($"Authorization: {authSistel}")
            EscreveLog($"Request Content: {jsonRequestBody}")
            EscreveLog("===========================================")
            ' Configurar a requisição usando HttpWebRequest
            Dim request As HttpWebRequest = CType(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "PUT"
            request.ContentType = "application/json"
            request.Headers.Add("Authorization", "Basic " & authSistel)
            ' Enviar os dados no corpo da requisição
            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(jsonRequestBody)
                streamWriter.Flush()
            End Using
            ' Disparar a requisição sem esperar resposta
            request.GetResponse().Close()
            EscreveLog("Requisição PUT disparada com sucesso (simples).")
        Catch ex As Exception
            ' Log de erros
            EscreveLog("======= ERRO AO ENVIAR REQUISIÇÃO SIMPLES =======")
            EscreveLog($"Erro durante a requisição PUT: {ex.Message}")
            If ex.InnerException IsNot Nothing Then
                EscreveLog($"Inner Exception: {ex.InnerException.Message}")
            End If
            EscreveLog("=================================================")
            Throw
        End Try
    End Sub
    Public Async Function RetornoAPISemEspera(estadoChamado As String) As Task
        ' Forçar o uso do protocolo TLS 1.2
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ' Criar um objeto HttpClient
        Using client As New HttpClient()
            Try
                ' Lendo usuário e senha do web.config
                Dim authSistel As String = ConfigurationManager.AppSettings("auth_sistel")
                Dim apiUrl As String = ConfigurationManager.AppSettings("url_api_sistel")
                Dim assignedTo As String = ConfigurationManager.AppSettings("assigned_to_sistel")
                ' Adicionar cabeçalho de autorização
                client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Basic", authSistel)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                ' Montar os dados do corpo da requisição
                Dim userNumber As String = hfUserNumber.Value
                Dim requestData As New With {
                .Action = hfTipoSolicitacao.Value,
                .RequestNumber = hfRequestNumber.Value,
                .WorkOrderNumber = hfWorkOrderNumber.Value,
                .Registration = userNumber,
                .Status = estadoChamado,
                .Description = "Chamado encerrado com sucesso!",
                .assigned_to = assignedTo,
                .iControlNumber = hfIdChamado.Value
                }
                ' Serializar os dados para JSON
                Dim jsonRequestBody As String = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
                Dim content As New StringContent(jsonRequestBody, Encoding.UTF8, "application/json")
                ' Gravar no log tudo que está sendo enviado
                EscreveLog("======= ENVIO DE REQUISIÇÃO =======")
                EscreveLog($"API URL: {apiUrl}")
                EscreveLog($"Authorization: {authSistel}")
                EscreveLog($"Request Content: {jsonRequestBody}")
                EscreveLog("===================================")
                ' Tentar fazer a requisição PUT sem aguardar uma resposta completa
                Try
                    ' Apenas dispara a requisição
                    Await client.PutAsync(apiUrl, content).ConfigureAwait(False)
                    EscreveLog("Requisição PUT enviada com sucesso.")
                Catch ex As Exception
                    ' Logar qualquer erro durante o envio
                    EscreveLog("======= ERRO AO ENVIAR REQUISIÇÃO =======")
                    EscreveLog($"Erro durante a requisição PUT: {ex.Message}")
                    If ex.InnerException IsNot Nothing Then
                        EscreveLog($"Inner Exception: {ex.InnerException.Message}")
                    End If
                    EscreveLog("=======================================")
                    Throw
                End Try
            Catch ex As Exception
                ' Capturar outras exceções
                EscreveLog("======= ERRO GERAL =======")
                EscreveLog($"Erro ao chamar a API: {ex.Message}")
                If ex.InnerException IsNot Nothing Then
                    EscreveLog($"Inner Exception: {ex.InnerException.Message}")
                End If
                EscreveLog("==========================")
                Throw
            End Try
        End Using
    End Function
    Private Async Function RetornoAPI(estadoChamado As String) As Task
        ' Forçar o uso do protocolo TLS 1.2
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ' Criar um objeto HttpClient
        Using client As New HttpClient()
            Try
                ' Lendo usuário e senha do web.config
                Dim authSistel As String = ConfigurationManager.AppSettings("auth_sistel")
                Dim apiUrl As String = ConfigurationManager.AppSettings("url_api_sistel")
                Dim assignedTo As String = ConfigurationManager.AppSettings("assigned_to_sistel")
                ' Adicionar cabeçalho de autorização
                client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Basic", authSistel)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                ' Montar os dados do corpo da requisição
                Dim userNumber As String = hfUserNumber.Value
                Dim requestData As New With {
                .Action = hfTipoSolicitacao.Value,
                .RequestNumber = hfRequestNumber.Value,
                .WorkOrderNumber = hfWorkOrderNumber.Value,
                .Registration = userNumber,
                .Status = estadoChamado,
                .Description = "Chamado encerrado com sucesso!",
                .assigned_to = assignedTo,
                .iControlNumber = hfIdChamado.Value
                }
                ' Serializar os dados para JSON
                Dim jsonRequestBody As String = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
                Dim content As New StringContent(jsonRequestBody, Encoding.UTF8, "application/json")
                ' Gravar no log tudo que está sendo enviado
                EscreveLog("======= ENVIO DE REQUISIÇÃO =======")
                EscreveLog($"API URL: {apiUrl}")
                EscreveLog($"Authorization: {authSistel}")
                EscreveLog($"Request Content: {jsonRequestBody}")
                EscreveLog("===================================")
                ' Tentar fazer a requisição PUT
                Try
                    Dim apiResponse As HttpResponseMessage = Await client.PutAsync(apiUrl, content)
                    ' Gravar no log o código de status da resposta
                    EscreveLog($"Código de Status HTTP: {apiResponse.StatusCode}")
                    ' Tentar ler o conteúdo da resposta
                    Dim responseData As String = Await apiResponse.Content.ReadAsStringAsync()
                    ' Registrar resposta completa, independentemente de sucesso ou falha
                    If apiResponse.IsSuccessStatusCode Then
                        EscreveLog("======= SUCESSO =======")
                        EscreveLog("Requisição bem-sucedida:")
                        EscreveLog($"Resposta: {responseData}")
                        EscreveLog("========================")
                    Else
                        EscreveLog("======= ERRO =======")
                        EscreveLog($"Erro na requisição: {apiResponse.StatusCode} - {responseData}")
                        EscreveLog("====================")
                        ' Exibir um alerta com o erro usando ScriptManager
                        Dim errorMessage As String = $"Erro na atualização do Service Now. Código de status: {apiResponse.StatusCode}. Detalhes: {responseData}"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{errorMessage}');", True)
                    End If
                Catch ex As Exception
                    ' Capturar exceções relacionadas à requisição
                    EscreveLog("======= ERRO DURANTE A REQUISIÇÃO =======")
                    EscreveLog($"Erro durante a requisição PUT: {ex.Message}")
                    If ex.InnerException IsNot Nothing Then
                        EscreveLog($"Inner Exception: {ex.InnerException.Message}")
                    End If
                    EscreveLog("=========================================")
                    Throw
                End Try
            Catch ex As Exception
                ' Capturar outras exceções
                EscreveLog("======= ERRO GERAL =======")
                EscreveLog($"Erro ao chamar a API: {ex.Message}")
                If ex.InnerException IsNot Nothing Then
                    EscreveLog($"Inner Exception: {ex.InnerException.Message}")
                End If
                EscreveLog("==========================")
                Throw
            End Try
        End Using
    End Function
    ' Função que executa a ação baseada no tipo de solicitação
    Private Function ExecutarAcaoChamado() As String

        ' [INÍCIO - ICTRL-NF-202506-004]
        Dim tipoSolicitacao As String = hfTipoSolicitacao.Value.ToLower().Replace(" ", "-").Replace("/", "-")
        Dim idChamado As Integer = If(Not String.IsNullOrEmpty(hfIdChamado.Value), Convert.ToInt32(hfIdChamado.Value), 0)
        Dim observacaoChamado As String = SanitizeAndEncodeForJs(hfObservacaoCompleta.Value) ' Assumindo que você terá um hf para a observação

        ' Verifica se a solicitação é de Habilitar ou Desabilitar Acesso
        If hfTipoSolicitacao.Value.ToUpper().Contains("HABILITAR ACESSO") Then
            HabilitarAcesso(idChamado, observacaoChamado)
            Return "Processo de Habilitação de Acesso iniciado."
        ElseIf hfTipoSolicitacao.Value.ToUpper().Contains("DESABILITAR ACESSO") Then
            DesabilitarAcesso(idChamado, observacaoChamado)
            Return "Processo de Desabilitação de Acesso iniciado."
        End If
        ' [FIM - ICTRL-NF-202506-004]

        Dim pCampo1 As String = ""
        Dim pCampo2 As String = ""
        Dim pCampo3 As String = ""
        Dim pCampo4 As String = ""
        Dim pCampo5 As String = ""
        Dim pCampo6 As String = ""
        Const c_acao_pacote_roaming As String = "contratar-pacote-de-roaming-internacional"
        Const c_acao_nova_linha As String = "nova-linha"
        Const c_acao_alterar_numero As String = "alterar-numero"
        Const c_acao_migracao_plano As String = "migracao-de-plano"
        Const c_acao_portabilidade As String = "portabilidade-de-linha"
        Const c_acao_alterar_ddd As String = "alterar-ddd"
        Const c_simcard_m2m_nova_linha As String = "simcard-m2m---nova-linha"
        Const c_telefone_via_satelite_nova_linha As String = "telefone-via-satélite---nova-linha"
        Const c_e_sim_troca_de_chip_virtual As String = "e-sim-troca-de-chip-virtual"
        Try
            ' Coletando valores dos campos necessários
            Dim servicePack As String = hfServicePack.Value
            Dim comentariosAtivo As String = ""
            ' Ajusta o campo "comentarios" dependendo do tipo de solicitação
            Select Case tipoSolicitacao
                ' [INÍCIO - ICTRL-NF-202506-023]
                Case c_acao_nova_linha, c_simcard_m2m_nova_linha, c_telefone_via_satelite_nova_linha
                    comentariosAtivo = "Novas Linhas: " & hfMultiplosAtivos.Value & " | Plano: " & hfnomePlanoMigracaoNL.Value
                    pCampo1 = hfMultiplosAtivos.Value
                    pCampo2 = hfnomePlanoMigracaoNL.Value
                ' [FIM - ICTRL-NF-202506-023]
                Case c_acao_pacote_roaming
                    If Not String.IsNullOrEmpty(servicePack) Then
                        comentariosAtivo = "Pacote contratado: " & servicePack
                        pCampo1 = servicePack
                    End If
                ' [INÍCIO - ICTRL-NF-202506-009]
                Case c_acao_migracao_plano
                    Dim novoPlano As String = hfNovoPlanoMigracao.Value
                    Dim tipoMigracao As String = hfOriginalMigrationDevice.Value

                    pCampo1 = novoPlano
                    pCampo2 = tipoMigracao
                    comentariosAtivo = "Migração de '" & tipoMigracao & "' para o plano: " & novoPlano
                ' [FIM - ICTRL-NF-202506-009]
                ' [INÍCIO - ICTRL-NF-202506-001 | 2025-06-21 | Parceiro IControlIT]
                Case c_e_sim_troca_de_chip_virtual
                    comentariosAtivo = "Novo SIM Card: " & hfNovoSimCard.Value
                    pCampo1 = hfNovoSimCard.Value
                ' [FIM - ICTRL-NF-202506-001]
                Case c_acao_alterar_ddd
                    comentariosAtivo = "Linha: (" & hfNewAreaCode.Value & ") " & hfNovaLinha.Value
                    pCampo1 = hfNovaLinha.Value
                Case c_acao_alterar_numero
                    comentariosAtivo = "Linha: " & hfAlterarLinha.Value
                    pCampo1 = hfNovaLinha.Value
                Case c_acao_portabilidade
                    comentariosAtivo = "Nome do plano: " & hfNomePlanoPortabilidade.Value & " | Data de recebimento do chip: " & hfDataRecebimentoChip.Value & " | Data de efetivação da portabilidade: " & hfDataEfetivacaoPortabilidade.Value
                    pCampo1 = hfNomePlanoPortabilidade.Value
                    pCampo2 = hfDataRecebimentoChip.Value
                    pCampo3 = hfDataEfetivacaoPortabilidade.Value
            End Select


            ' Chama a função que controla o tipo de solicitação, exceto para "alterar-proprietario"
            Return WS_Chamado.ExecutarAcaoAtivo(Session("Conn_Banco"), "dbo.pa_Ativo_Chamado", tipoSolicitacao, idChamado, comentariosAtivo, pCampo1, pCampo2, pCampo3, pCampo4, pCampo5, pCampo6, True)
        Catch ex As SqlClient.SqlException
            Throw
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function EnviarEmailChamado()
        Dim emailsSelecionados As String = hfEmailsSelecionados.Value ' Emails separados por vírgula
        Dim emailRespRegional As String = hfEmailResponsavelRegional.Value
        ' Verificar e substituir a vírgula por ponto e vírgula
        emailRespRegional = emailRespRegional.Replace(" ", "")
        If emailRespRegional.Contains(",") Then
            emailRespRegional = emailRespRegional.Replace(",", ";")
        End If
        ' Recuperando os valores da página
        Dim idChamado As String = hfIdChamado.Value
        Dim requestNumber As String = hfRequestNumber.Value
        Dim workOrderNumber As String = hfWorkOrderNumber.Value
        Dim estado As String = hfEstado.Value
        Dim nomeUsuario As String = hfUserName.Value
        Dim idTransacao As String = hfIdConsumidor.Value
        Dim tipoSolicitacao As String = hfTipoSolicitacao.Value
        Dim telecomProvider As String = hfTelecomProvider.Value ' ICTRL-NF-202506-013
        Dim planoAtual As String = hfNewPlanoContrato.Value
        Dim comentarios As String = hfComentarios.Value
        Dim camposCondicionais As String = camposCondicionaisContainer.InnerHtml
        Dim newAreaCode As String = hfNewAreaCode.Value
        Dim migrationDevice As String = hfMigrationDevice.Value
        Dim empresaContratanteSelecionada As String = empresaContratante.SelectedValue
        Dim faturaAgrupadoraSelecionada As String = hfFaturaDropdown.Value
        Dim corpoDoEmail As String = corpoEmail.Value
        Dim pTextoAdicional As String
        Dim linha As String
        If Not String.IsNullOrEmpty(hfNovaLinha.Value) Then
            linha = hfNovaLinha.Value
        ElseIf Not String.IsNullOrEmpty(hfDesignationProduct.Value) Then
            linha = hfDesignationProduct.Value
        Else
            linha = String.Empty ' Caso ambos estejam vazios
        End If
        If String.IsNullOrEmpty(idChamado) Then idChamado = "N/A"
        If String.IsNullOrEmpty(requestNumber) Then requestNumber = "N/A"
        '=============== INICIO DO CONTEUDO
        pTextoAdicional = "<p><strong>Dados do Chamado</strong></p>"
        If Not String.IsNullOrEmpty(corpoDoEmail) Then
            pTextoAdicional = $"<p>{corpoDoEmail}</p><br />" & "<p><strong>Dados do Chamado</strong></p>"
        End If
        pTextoAdicional &= "<p>"
        If Not String.IsNullOrEmpty(idChamado) Then
            pTextoAdicional &= "<strong>Id Chamado:</strong> " & idChamado & "<br />"
        End If
        If Not String.IsNullOrEmpty(requestNumber) Then
            pTextoAdicional &= "<strong>Request Number:</strong> " & requestNumber & "<br />"
        End If
        If Not String.IsNullOrEmpty(workOrderNumber) Then
            pTextoAdicional &= "<strong>Work Order Number:</strong> " & workOrderNumber & "<br />"
        End If
        If Not String.IsNullOrEmpty(nomeUsuario) Then
            pTextoAdicional &= "<strong>Nome Do usuário:</strong> " & nomeUsuario & "<br />"
        End If
        If Not String.IsNullOrEmpty(idTransacao) Then
            pTextoAdicional &= "<strong>ID da transação:</strong> " & idTransacao & "<br />"
        End If
        If Not String.IsNullOrEmpty(tipoSolicitacao) Then
            pTextoAdicional &= "<strong>Tipo de Solicitação:</strong> " & tipoSolicitacao & "<br />"
        End If
        ' ICTRL-NF-202506-013: Adiciona a operadora de origem no e-mail de portabilidade.
        If tipoSolicitacao.ToUpper() = "PORTABILIDADE DE LINHA" Then
            If Not String.IsNullOrEmpty(telecomProvider) Then
                pTextoAdicional &= "<strong>Operadora Origem:</strong> " & telecomProvider & "<br />"
            End If
        End If
        If Not String.IsNullOrEmpty(planoAtual) Then
            pTextoAdicional &= "<strong>Plano atual:</strong> " & planoAtual & "<br />"
        End If
        If Not String.IsNullOrEmpty(linha) Then
            pTextoAdicional &= "<strong>Linha:</strong> " & linha & "<br />"
        End If
        If Not String.IsNullOrEmpty(empresaContratanteSelecionada) Then
            pTextoAdicional &= "<strong>Empresa Contratante:</strong> " & empresaContratanteSelecionada & "<br />"
        End If
        If Not String.IsNullOrEmpty(faturaAgrupadoraSelecionada) Then
            pTextoAdicional &= "<strong>Fatura Agrupadora:</strong> " & faturaAgrupadoraSelecionada & "<br />"
        End If
        If Not String.IsNullOrEmpty(migrationDevice) Then
            pTextoAdicional &= "<strong>Tipo de Migração:</strong> " & migrationDevice & "<br />"
        End If

        ' ICTRL-NF-202506-013: Adiciona os campos condicionais (como Operadora Origem, etc.) ao corpo do email.
        If Not String.IsNullOrEmpty(camposCondicionais) Then
            pTextoAdicional &= "<br />" & camposCondicionais
        End If
        If Not String.IsNullOrEmpty(newAreaCode) Then
            pTextoAdicional &= "<strong>Novo DDD:</strong> " & newAreaCode & "<br />"
        End If
        pTextoAdicional &= "</p>"
        '=============== FIM DO CONTEUDO
        Dim pAssuntoEmail As String = "[VALE] " & tipoSolicitacao & " - " & workOrderNumber
        ' Gerando o caminho da pasta com base na data e hora
        Dim caminhoBase As String = ConfigurationManager.AppSettings("pasta-anexos")
        Dim pastaAnexos As String = caminhoBase & DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") & "\"
        ' Criar a pasta para os anexos
        If Not Directory.Exists(pastaAnexos) Then
            Directory.CreateDirectory(pastaAnexos)
        End If
        Dim base64FilesJson As String = hfBase64Files.Value
        Dim arquivosAnexos As New List(Of AnexoModel)
        If Not String.IsNullOrEmpty(base64FilesJson) Then
            Dim arquivosBase64 = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of Object))(base64FilesJson)
            For Each arquivoBase64 In arquivosBase64
                Try
                    Dim nomeArquivo As String = arquivoBase64("nome").ToString()
                    Dim conteudoBase64 As String = arquivoBase64("conteudo").ToString()
                    EscreveLog($"Convertendo o arquivo {nomeArquivo}")
                    ' Remover o prefixo "data:*;base64," caso ele exista
                    If conteudoBase64.Contains(",") Then
                        conteudoBase64 = conteudoBase64.Substring(conteudoBase64.IndexOf(",") + 1)
                    End If
                    ' Converter o conteúdo base64 de volta para um byte array
                    Dim bytes() As Byte = Convert.FromBase64String(conteudoBase64)
                    ' Salvar o arquivo na pasta criada
                    Dim caminhoArquivo As String = Path.Combine(pastaAnexos, nomeArquivo)
                    File.WriteAllBytes(caminhoArquivo, bytes)
                    Dim anexo As New AnexoModel With {
                    .NomeArquivo = nomeArquivo,
                    .CaminhoArquivo = caminhoArquivo
                    }
                    arquivosAnexos.Add(anexo)
                Catch ex As FormatException
                    ' Caso haja erro de formato base64
                    EscreveLog($"Erro ao converter base64 para bytes: {ex.Message}")
                Catch ex As Exception
                    ' Qualquer outro erro genérico
                    EscreveLog($"Erro ao processar arquivo '{arquivoBase64("nome").ToString()}': {ex.Message}")
                End Try
            Next
        End If
        ' Chamar a função para enviar o e-mail com o HTML gerado e anexos
        Dim msg As String = AgendarEnvioEmail(emailsSelecionados, emailRespRegional, pTextoAdicional, pAssuntoEmail, arquivosAnexos)
        Return msg
    End Function
    Public Function GetBadgeClass(estado As Object) As String
        If estado IsNot Nothing Then
            If estado.ToString() = "Pendente" Then
                Return "azul"
            ElseIf estado.ToString() = "Concluído" Then
                Return "verde"
            ElseIf estado.ToString() = "Cancelado" Then
                Return "vermelho"
            End If
        End If
        Return "azul" ' Classe padrão para outros estados ou nulo
    End Function
    ' Função para buscar e-mails da operadora por id_Conglomerado
    <WebMethod()>
    Public Shared Function BuscarEmailsOperadora(ByVal idConglomerado As Integer) As String
        Try
            ' Chamar o método Operadora do WebService
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_emails_operadora", idConglomerado, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
            ' Verifica se o DataSet contém resultados
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ' Serializa a lista de e-mails para o formato JSON
                Dim emailList As New List(Of String)
                For Each row As DataRow In ds.Tables(0).Rows
                    emailList.Add(row("nm_Email").ToString())
                Next
                Dim jsSerializer As New JavaScriptSerializer()
                Return jsSerializer.Serialize(emailList)
            Else
                Return "[]"
            End If
        Catch ex As Exception
            Return "[]"
        End Try
    End Function
    <WebMethod()>
    Public Shared Function BuscarPlanosContrato(ByVal idConglomerado As Integer) As String
        Try
            ' Configuração da conexão com o banco de dados
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            ' Chama a procedure para buscar planos de contrato
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_plano_contrato", idConglomerado, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
            ' Verifica se o DataSet tem resultados
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ' Serializa os planos para JSON
                Dim planoList As New List(Of String)
                For Each row As DataRow In ds.Tables(0).Rows
                    planoList.Add(row("Nm_Plano_Contrato").ToString())
                Next
                Dim jsSerializer As New JavaScriptSerializer()
                Return jsSerializer.Serialize(planoList)
            Else
                Return "[]"
            End If
        Catch ex As Exception
            Return "[]"
        End Try
    End Function
    ' Função para buscar e-mails da operadora por id_Conglomerado
    <WebMethod()>
    Public Shared Function BuscarFaturaAgrupadora(ByVal idConglomerado As Integer) As String
        Try
            ' Chamar o método Operadora do WebService
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_fatura_agrupadora", idConglomerado, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
            ' Verifica se o DataSet contém resultados
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ' Serializa a lista de e-mails para o formato JSON
                Dim emailList As New List(Of String)
                For Each row As DataRow In ds.Tables(0).Rows
                    emailList.Add(row("Nr_Plano_Conta").ToString())
                Next
                Dim jsSerializer As New JavaScriptSerializer()
                Return jsSerializer.Serialize(emailList)
            Else
                Return "[]"
            End If
        Catch ex As Exception
            Return "[]"
        End Try
    End Function
    Protected Sub BindEmpresaContratante()
        Try
            ' Chamar o método Operadora do WebService
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_nomes_filiais", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
            ' Verifica se o DataSet contém resultados
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ' Limpa as opções existentes no DropDownList (empresaContratante)
                empresaContratante.Items.Clear()
                ' Adiciona a opção padrão
                empresaContratante.Items.Add(New ListItem("Selecione a Empresa", ""))
                ' Preenche o DropDownList com os dados do DataSet
                For Each row As DataRow In ds.Tables(0).Rows
                    Dim nomeFilial As String = row("Nm_Filial").ToString()
                    empresaContratante.Items.Add(New ListItem(nomeFilial, nomeFilial))
                Next
            Else
                ' Adiciona uma mensagem padrão se não houver resultados
                empresaContratante.Items.Clear()
                empresaContratante.Items.Add(New ListItem("Nenhuma empresa disponível", ""))
            End If
        Catch ex As Exception
            ' Tratamento de exceções
            empresaContratante.Items.Clear()
            empresaContratante.Items.Add(New ListItem("Erro ao carregar as empresas", ""))
        End Try
    End Sub
    Protected Sub ddlItemsPerPage_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' Recupera o valor selecionado no dropdown
        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)
        ' Atualiza a páginação e a exibição dos chamados com o novo número de itens por página
        ' Aqui, você deve chamar seu método de exibição com o novo valor de itemsPerPage
        BindChamados(CurrentPage, itemsPerPage)
    End Sub
    ' Função para agendar envio de email
    Private Function AgendarEnvioEmail(ByVal pEmailRespRegional As String, ByVal pEmailOperadora As String, ByVal pTextoAdicional As String, ByVal pAssuntoEmail As String, ByVal anexos As List(Of AnexoModel)) As String
        Try

            ' [INÍCIO - ICTRL-NF-202506-004]
            Dim idUsuarioLogado As Integer = Convert.ToInt32(Session("Id_Usuario")) ' Assumindo que o Id_Usuario está na sessão
            Dim profileName As String = ObterEmailProfileDoUsuario(idUsuarioLogado)

            If String.IsNullOrEmpty(profileName) Then
                Return "Erro: Você não tem permissão para enviar e-mails a partir de chamados. Contate o administrador."
            ElseIf profileName = "ERRO" Then
                Return "Erro: Ocorreu um problema ao verificar suas permissões de envio. Tente novamente."
            End If
            ' [FIM - ICTRL-NF-202506-004]

            Dim id_Mail_Sender As Integer
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            Dim nmUsuario As String = Session("Nm_Usuario")
            Dim id_Chamado As Integer = hfIdChamado.Value
            ' Busca o id_Mail_Sender baseado no assunto
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_mail_sender", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, pAssuntoEmail, Nothing, id_Chamado, True)
            id_Mail_Sender = Convert.ToInt32(ds.Tables(0).Rows(0)("Id_Mail_Sender"))
            ' Primeiro email: Para pEmailOperadora, sem anexos
            WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "agendar_disparo_email", Nothing, Nothing, pEmailOperadora, Nothing, id_Mail_Sender, pTextoAdicional, Nothing, nmUsuario, id_Chamado, True)
            ' Segundo email: Para pEmailRespRegional, com ou sem anexos
            If anexos IsNot Nothing AndAlso anexos.Count > 0 Then
                Dim caminhosArquivosConcatenados As String = ""
                Dim delimitador As String = "|"
                For Each anexo In anexos
                    ' Concatenar o nome do arquivo e o caminho físico
                    If Not String.IsNullOrEmpty(caminhosArquivosConcatenados) Then
                        caminhosArquivosConcatenados &= delimitador
                    End If
                    caminhosArquivosConcatenados &= anexo.CaminhoArquivo
                Next
                ' Chama o serviço web para agendar o disparo com os anexos fora do loop
                WS_Chamado.ChamadoAuxiliarComAnexos(pPConn_Banco, "agendar_disparo_email", Nothing, Nothing, pEmailRespRegional, Nothing, id_Mail_Sender, pTextoAdicional, Nothing, caminhosArquivosConcatenados, nmUsuario, id_Chamado, True)
            Else
                ' Sem anexos
                WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "agendar_disparo_email", Nothing, Nothing, pEmailOperadora, Nothing, id_Mail_Sender, pTextoAdicional, Nothing, nmUsuario, id_Chamado, True)
            End If
            Return "Emails enviados com sucesso."
        Catch ex As Exception
            Return "Erro ao agendar os emails: " & ex.Message
        End Try
    End Function
    Private Function AgendarEnvioEmailOLD(ByVal pEmailDestino As String, ByVal pEmailCopia As String, ByVal pTextoAdicional As String, ByVal pAssuntoEmail As String, ByVal anexos As List(Of AnexoModel)) As String
        Try
            Dim id_Mail_Sender As Integer
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            Dim nmUsuario As String = Session("Nm_Usuario")
            Dim id_Chamado As Integer = hfIdChamado.Value
            ' Busca o id_Mail_Sender baseado no assunto
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_mail_sender", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, pAssuntoEmail, Nothing, id_Chamado, True)
            id_Mail_Sender = Convert.ToInt32(ds.Tables(0).Rows(0)("Id_Mail_Sender"))
            ' Verifica se há anexos
            If anexos IsNot Nothing AndAlso anexos.Count > 0 Then
                Dim caminhosArquivosConcatenados As String = ""
                Dim delimitador As String = "|"
                For Each anexo In anexos
                    ' Concatenar o nome do arquivo e o caminho físico
                    If Not String.IsNullOrEmpty(caminhosArquivosConcatenados) Then
                        caminhosArquivosConcatenados &= delimitador
                    End If
                    caminhosArquivosConcatenados &= anexo.CaminhoArquivo
                Next
                ' Chama o serviço web para agendar o disparo com os anexos fora do loop
                WS_Chamado.ChamadoAuxiliarComAnexos(pPConn_Banco, "agendar_disparo_email", Nothing, Nothing, pEmailDestino, pEmailCopia, id_Mail_Sender, pTextoAdicional, Nothing, caminhosArquivosConcatenados, nmUsuario, id_Chamado, True)
            Else
                ' Sem anexos
                WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "agendar_disparo_email", Nothing, Nothing, pEmailDestino, pEmailCopia, id_Mail_Sender, pTextoAdicional, Nothing, nmUsuario, id_Chamado, True)
            End If
            Return "Email enviado com sucesso."
        Catch ex As Exception
            Return "Erro ao agendar o email: " & ex.Message
        End Try
    End Function
    Protected Sub btnEnviarEmail_Click(sender As Object, e As EventArgs)
        Try
            Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)
            Dim msg As String = EnviarEmailChamado()

            ' Codifica a mensagem de forma segura para JavaScript
            Dim msgJs As String = HttpUtility.JavaScriptStringEncode(msg)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{msgJs}');", True)

            ' Apenas atualiza a lista de chamados se o envio de e-mail não retornou um erro
            If Not msg.ToLower().Contains("erro") Then
                BindChamados(CurrentPage, itemsPerPage)
            End If

        Catch ex As Exception
            ' Se ocorrer qualquer erro inesperado durante o processo, ele será capturado aqui.
            ' Isso evita que o modal feche silenciosamente.
            Dim errorMsg As String = "Ocorreu um erro inesperado ao tentar enviar o e-mail: " & ex.Message.Replace("'", "\'").Replace(vbCrLf, " ")
            EscreveLog("(Consulta_Chamado.btnEnviarEmail_Click) Erro Detalhado: " & ex.ToString()) ' Grava o erro completo no log para depuração

            ' Exibe o alerta de erro para o usuário.
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{errorMsg}');", True)
        End Try
    End Sub

    ' [INÍCIO - ICTRL-NF-202506-012]

    ' Propriedade para armazenar o termo de busca entre postbacks
    Private Property TermoBusca As String
        Get
            Return If(ViewState("TermoBusca") Is Nothing, String.Empty, ViewState("TermoBusca").ToString())
        End Get
        Set(ByVal value As String)
            ViewState("TermoBusca") = value
        End Set
    End Property

    ' Evento do botão de busca
    Protected Sub btnBusca_Click(sender As Object, e As EventArgs)
        Me.TermoBusca = txtBusca.Text.Trim()
        Me.CurrentPage = 1 ' Reseta para a primeira página ao buscar
        BindChamados(Me.CurrentPage, Convert.ToInt32(ddlItemsPerPage.SelectedValue))
    End Sub

    ' Evento do botão para limpar a busca
    Protected Sub btnLimparBusca_Click(sender As Object, e As EventArgs)
        Me.TermoBusca = String.Empty
        txtBusca.Text = String.Empty
        Me.CurrentPage = 1
        BindChamados(Me.CurrentPage, Convert.ToInt32(ddlItemsPerPage.SelectedValue))
    End Sub

    ' [FIM - ICTRL-NF-202506-012]


    ' [INÍCIO - ICTRL-NF-202506-006]
    Protected Sub btnConfirmarCancelamento_Click(sender As Object, e As EventArgs)
        Dim idChamado As Integer = Convert.ToInt32(hfIdChamado.Value)
        Dim motivoCancelamento As String = hfCancellationComment.Value
        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)
        Dim mensagemRetorno As String = ""

        Try
            ' PASSO 1: Tenta notificar o ServiceNow PRIMEIRO.
            DispararRequisicaoSimples("CANCELADO", motivoCancelamento)

            ' PASSO 2: Se a notificação acima não falhar, executa a ação no banco de dados.
            mensagemRetorno = WS_Chamado.ExecutarAcaoAtivo(Session("Conn_Banco"),
                                                    "dbo.pa_Ativo_Chamado",
                                                    "cancelar-manualmente",
                                                    idChamado,
                                                    motivoCancelamento,
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{mensagemRetorno.Replace("'", "\'")}');", True)

        Catch ex As Exception
            ' Se QUALQUER um dos passos falhar (principalmente a notificação),
            ' o erro é capturado aqui e NADA é gravado no banco.
            Dim msgErro As String = $"Erro ao cancelar o chamado: {ex.Message}".Replace("'", "\'")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{msgErro}');", True)
        End Try

        BindChamados(CurrentPage, itemsPerPage)
    End Sub
    ' [FIM - ICTRL-NF-202506-006]

    ' [INÍCIO - ICTRL-NF-202506-007]
    ''' <summary>
    ''' Remove quebras de linha e tags HTML de uma string, e a codifica para ser usada de forma segura em JavaScript.
    ''' </summary>
    ''' <param name="input">O objeto a ser sanitizado (será convertido para string).</param>
    ''' <returns>Uma string segura para JavaScript, em uma única linha.</returns>
    Public Function SanitizeAndEncodeForJs(ByVal input As Object) As String
        If input Is Nothing OrElse Convert.IsDBNull(input) Then
            Return ""
        End If

        Dim text As String = input.ToString()

        ' 1. Substitui todas as formas de quebra de linha por um espaço.
        text = text.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")

        ' 2. Remove todas as tags HTML usando Regex.
        text = Regex.Replace(text, "<.*?>", String.Empty)

        ' 3. Converte aspas duplas para sua entidade HTML. ESTA É A CORREÇÃO.
        text = text.Replace("""", "&quot;")

        ' 4. Codifica o restante da string para que ela não quebre literais de string em JavaScript (trata aspas simples, etc.).
        Return HttpUtility.JavaScriptStringEncode(text)
    End Function
    ' [FIM - ICTRL-NF-202506-007]




    ' [INÍCIO - ICTRL-NF-202506-004]

    Private Sub HabilitarAcesso(pIdChamado As Integer, pObservacao As String)
        Dim matricula As String = ExtrairValorDaObservacao(pObservacao, "Matricula")
        Dim tipoVisao As String = ExtrairValorDaObservacao(pObservacao, "Visao")
        Dim mdmVisao As String = ExtrairValorDaObservacao(pObservacao, "MDM_Visao") ' Extrai o novo campo

        If String.IsNullOrEmpty(matricula) Or String.IsNullOrEmpty(tipoVisao) Or String.IsNullOrEmpty(mdmVisao) Then
            Throw New Exception("Não foi possível encontrar a Matrícula, Tipo de Visão ou MDM_Visao na observação do chamado.")
        End If

        ' Usa o WebService para executar a lógica no banco de dados, passando o mdmVisao no pCampo3
        WS_Chamado.ExecutarAcaoAtivo(Session("Conn_Banco"), "dbo.pa_Ativo_Chamado", "habilitar-acesso", pIdChamado, Nothing, matricula, tipoVisao, mdmVisao, Nothing, Nothing, Nothing, True)
    End Sub

    Private Sub DesabilitarAcesso(pIdChamado As Integer, pObservacao As String)
        Dim matricula As String = ExtrairValorDaObservacao(pObservacao, "Matricula")
        If String.IsNullOrEmpty(matricula) Then
            Throw New Exception("Não foi possível encontrar a Matrícula na observação do chamado.")
        End If

        ' Usa o WebService para executar a lógica no banco de dados
        WS_Chamado.ExecutarAcaoAtivo(Session("Conn_Banco"), "dbo.pa_Ativo_Chamado", "desabilitar-acesso", pIdChamado, Nothing, matricula, Nothing, Nothing, Nothing, Nothing, Nothing, True)
    End Sub

    Private Function ExtrairValorDaObservacao(observacao As String, chave As String) As String
        Try
            ' Procura pela chave (ex: "Matricula:") na observação, ignorando maiúsculas/minúsculas
            Dim match As Match = Regex.Match(observacao, chave & ":\s*([^;]*)", RegexOptions.IgnoreCase)
            If match.Success Then
                Return match.Groups(1).Value.Trim()
            End If
            Return String.Empty
        Catch
            Return String.Empty
        End Try
    End Function

    Private Function ObterEmailProfileDoUsuario(pIdUsuario As Integer) As String
        Try
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(Session("Conn_Banco"), "buscar_email_profile", pIdUsuario, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0)("Profile_Name").ToString()
            End If
            Return String.Empty ' Retorna vazio se não encontrar perfil
        Catch ex As Exception
            EscreveLog("Erro ao buscar perfil de e-mail do usuário: " & ex.Message)
            Return "ERRO" ' Retorna "ERRO" para ser tratado na chamada da função
        End Try
    End Function

    ' [FIM - ICTRL-NF-202506-004]

End Class
