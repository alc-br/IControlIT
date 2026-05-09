' -----------------------------------------------------------------------
' Consulta_Chamado.aspx.vb
' Autor: Anderson Luiz Chipak
' Data: 05/09/2024
' Descri��o:Code Behind para Consulta de Chamados
' -----------------------------------------------------------------------
' /*
' * HIST�RICO DE MODIFICA��ES
' * [ICTRL-NF-202506-006 | 2025-06-22 | ANDERSON LUIZ CHIPAK]
' * [ICTRL-NF-202506-012 | 2025-06-22 | ANDERSON LUIZ CHIPAK]
' * [ICTRL-NF-202506-013 | 2025-06-22 | ANDERSON LUIZ CHIPAK]
' * [ICTRL-NF-202506-001 | 2025-06-21 | ANDERSON LUIZ CHIPAK]
' * [ICTRL-NF-202506-008 | 2025-06-24 | ANDERSON LUIZ CHIPAK]
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
    Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
    Dim vdataset As Data.DataSet
    Private logFilePath As String = "C:\Temp\Log.txt"

    ' M�todo para garantir que o arquivo de log pode ser criado ou acessado
    Private Sub InicializaLog()
        Try
            ' Verifica se a pasta existe, sen�o cria
            Dim logDirectory As String = Path.GetDirectoryName(logFilePath)
            If Not Directory.Exists(logDirectory) Then
                Directory.CreateDirectory(logDirectory)
            End If
            ' Verifica se o arquivo existe, sen�o cria
            If Not File.Exists(logFilePath) Then
                File.Create(logFilePath).Dispose()
            End If
        Catch ex As Exception
            ' Caso ocorra um erro ao criar diret�rio ou arquivo de log
            Throw New Exception("Erro ao inicializar o arquivo de log: " & ex.Message)
        End Try
    End Sub
    ' M�todo para escrever log em arquivo de texto
    Private Sub EscreveLog(ByVal mensagem As String)
        Try
            ' Inicializa o log (cria pasta/arquivo se necess�rio)
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
            ' Chama o m�todo ass�ncrono BtnExecutar_Click usando um handler ass�ncrono
            Dim task As Task = BtnExecutar_Click(Nothing, EventArgs.Empty)
            task.Wait() ' Aguarda a conclus�o da tarefa ass�ncrona
            Return ' Evita a execu��o do restante do Page_Load
        End If
        ' Executa o carregamento inicial de dados se n�o for um postback
        If Not IsPostBack Then
            Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)
            Dim connBancoValue = Session("Conn_Banco")
            If String.IsNullOrEmpty(connBancoValue) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('A string de conex�o est� vazia.');", True)
            Else
                BindChamados(1, itemsPerPage)
                BindEmpresaContratante()
            End If
        End If
    End Sub
    Private Sub BindChamados(ByVal pageNumber As Integer, ByVal pageSize As Integer)
        Try
            ' Inicializa��o da conex�o com o banco de dados
            Dim pPConn_Banco As String = Session("Conn_Banco")
            If String.IsNullOrEmpty(pPConn_Banco) Then
                Throw New InvalidOperationException("A conex�o com o banco de dados n�o foi encontrada.")
            End If
            EscreveLog("pRetorno: True")
            ' Chamada inicial para buscar dados do Chamado
            Dim vdataset As DataSet = WS_Chamado.Chamado(pPConn_Banco, "busca_todos_dados", pageNumber, pageSize, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Me.TermoBusca, True)

            ' Verifica se o DataSet cont�m informa��es
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
                ' Esse trecho indica que o erro pode estar vindo do SQL Server com o conte�do de @MsgErro
                EscreveLog("===========================================================================================")
                EscreveLog("(Consulta_Chamado.aspx.vb) Erro do SQL Server: " & mensagemErro & vbCrLf & ex.StackTrace)
            Else
                ' Captura qualquer outra exce��o
                EscreveLog("===========================================================================================")
                EscreveLog("(Consulta_Chamado.aspx.vb) Erro Gen�rico: " & mensagemErro & vbCrLf & ex.StackTrace)
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
        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)
        Try
            ' Chame seu m�todo de a��o do chamado e capture a mensagem de retorno
            Dim mensagem As String = ExecutarAcaoChamado()
            Dim mensagemEscapada As String = mensagem.Replace("'", "\'")
            Try
                ' Aguarde a chamada da API, passando a mensagem de sucesso para DispararRequisicaoSimples
                DispararRequisicaoSimples("ENCERRADA", mensagem)
            Catch ex As Exception
                mensagemEscapada = mensagemEscapada & $" ATEN��O: Foi relatado um erro no retorno da requisi��o para o ServiceNow: {ex.Message}"
            End Try
            ' Atualize os dados dos chamados
            BindChamados(CurrentPage, itemsPerPage)
            ' Exiba a mensagem de sucesso retornada pelo m�todo
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('Chamado processado. {mensagemEscapada}');", True)
        Catch ex As Exception
            ' Atualize os dados dos chamados
            BindChamados(CurrentPage, itemsPerPage)
            EscreveLog("(Consulta_Chamado.BtnExecutar_Click) Erro ao executar a a��o: " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{ex.Message}');", True)
        End Try
    End Function
    Public Sub DispararRequisicaoSimples(estadoChamado As String, mensagem As String)
        Try
            ' For�ar o uso do protocolo TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls
            ' Configurar os dados de autentica��o e URL
            Dim authSistel As String = ConfigurationManager.AppSettings("auth_sistel")
            Dim apiUrl As String = ConfigurationManager.AppSettings("url_api_sistel")
            Dim assignedTo As String = ConfigurationManager.AppSettings("assigned_to_sistel")
            ' Montar os dados do corpo da requisi��o
            ' [IN�CIO - ICTRL-NF-202506-008]
            ' Montar os dados do corpo da requisi��o
            Dim registrationValue As String

            ' Se a solicita��o for de uma nova linha e o campo da nova linha estiver preenchido,
            ' usamos o novo n�mero. Caso contr�rio, mant�m-se o valor original.
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
            ' Gravar no log tudo que est� sendo enviado
            EscreveLog("======= ENVIO DE REQUISI��O SIMPLES =======")
            EscreveLog($"API URL: {apiUrl}")
            EscreveLog($"Authorization: {authSistel}")
            EscreveLog($"Request Content: {jsonRequestBody}")
            EscreveLog("===========================================")
            ' Configurar a requisi��o usando HttpWebRequest
            Dim request As HttpWebRequest = CType(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "PUT"
            request.ContentType = "application/json"
            request.Headers.Add("Authorization", "Basic " & authSistel)
            ' Enviar os dados no corpo da requisi��o
            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(jsonRequestBody)
                streamWriter.Flush()
            End Using
            ' Disparar a requisi��o sem esperar resposta
            request.GetResponse().Close()
            EscreveLog("Requisi��o PUT disparada com sucesso (simples).")
        Catch ex As Exception
            ' Log de erros
            EscreveLog("======= ERRO AO ENVIAR REQUISI��O SIMPLES =======")
            EscreveLog($"Erro durante a requisi��o PUT: {ex.Message}")
            If ex.InnerException IsNot Nothing Then
                EscreveLog($"Inner Exception: {ex.InnerException.Message}")
            End If
            EscreveLog("=================================================")
            Throw
        End Try
    End Sub
    Public Async Function RetornoAPISemEspera(estadoChamado As String) As Task
        ' For�ar o uso do protocolo TLS 1.2
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ' Criar um objeto HttpClient
        Using client As New HttpClient()
            Try
                ' Lendo usu�rio e senha do web.config
                Dim authSistel As String = ConfigurationManager.AppSettings("auth_sistel")
                Dim apiUrl As String = ConfigurationManager.AppSettings("url_api_sistel")
                Dim assignedTo As String = ConfigurationManager.AppSettings("assigned_to_sistel")
                ' Adicionar cabe�alho de autoriza��o
                client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Basic", authSistel)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                ' Montar os dados do corpo da requisi��o
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
                ' Gravar no log tudo que est� sendo enviado
                EscreveLog("======= ENVIO DE REQUISI��O =======")
                EscreveLog($"API URL: {apiUrl}")
                EscreveLog($"Authorization: {authSistel}")
                EscreveLog($"Request Content: {jsonRequestBody}")
                EscreveLog("===================================")
                ' Tentar fazer a requisi��o PUT sem aguardar uma resposta completa
                Try
                    ' Apenas dispara a requisi��o
                    Await client.PutAsync(apiUrl, content).ConfigureAwait(False)
                    EscreveLog("Requisi��o PUT enviada com sucesso.")
                Catch ex As Exception
                    ' Logar qualquer erro durante o envio
                    EscreveLog("======= ERRO AO ENVIAR REQUISI��O =======")
                    EscreveLog($"Erro durante a requisi��o PUT: {ex.Message}")
                    If ex.InnerException IsNot Nothing Then
                        EscreveLog($"Inner Exception: {ex.InnerException.Message}")
                    End If
                    EscreveLog("=======================================")
                    Throw
                End Try
            Catch ex As Exception
                ' Capturar outras exce��es
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
        ' For�ar o uso do protocolo TLS 1.2
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ' Criar um objeto HttpClient
        Using client As New HttpClient()
            Try
                ' Lendo usu�rio e senha do web.config
                Dim authSistel As String = ConfigurationManager.AppSettings("auth_sistel")
                Dim apiUrl As String = ConfigurationManager.AppSettings("url_api_sistel")
                Dim assignedTo As String = ConfigurationManager.AppSettings("assigned_to_sistel")
                ' Adicionar cabe�alho de autoriza��o
                client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Basic", authSistel)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                ' Montar os dados do corpo da requisi��o
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
                ' Gravar no log tudo que est� sendo enviado
                EscreveLog("======= ENVIO DE REQUISI��O =======")
                EscreveLog($"API URL: {apiUrl}")
                EscreveLog($"Authorization: {authSistel}")
                EscreveLog($"Request Content: {jsonRequestBody}")
                EscreveLog("===================================")
                ' Tentar fazer a requisi��o PUT
                Try
                    Dim apiResponse As HttpResponseMessage = Await client.PutAsync(apiUrl, content)
                    ' Gravar no log o c�digo de status da resposta
                    EscreveLog($"C�digo de Status HTTP: {apiResponse.StatusCode}")
                    ' Tentar ler o conte�do da resposta
                    Dim responseData As String = Await apiResponse.Content.ReadAsStringAsync()
                    ' Registrar resposta completa, independentemente de sucesso ou falha
                    If apiResponse.IsSuccessStatusCode Then
                        EscreveLog("======= SUCESSO =======")
                        EscreveLog("Requisi��o bem-sucedida:")
                        EscreveLog($"Resposta: {responseData}")
                        EscreveLog("========================")
                    Else
                        EscreveLog("======= ERRO =======")
                        EscreveLog($"Erro na requisi��o: {apiResponse.StatusCode} - {responseData}")
                        EscreveLog("====================")
                        ' Exibir um alerta com o erro usando ScriptManager
                        Dim errorMessage As String = $"Erro na atualiza��o do Service Now. C�digo de status: {apiResponse.StatusCode}. Detalhes: {responseData}"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{errorMessage}');", True)
                    End If
                Catch ex As Exception
                    ' Capturar exce��es relacionadas � requisi��o
                    EscreveLog("======= ERRO DURANTE A REQUISI��O =======")
                    EscreveLog($"Erro durante a requisi��o PUT: {ex.Message}")
                    If ex.InnerException IsNot Nothing Then
                        EscreveLog($"Inner Exception: {ex.InnerException.Message}")
                    End If
                    EscreveLog("=========================================")
                    Throw
                End Try
            Catch ex As Exception
                ' Capturar outras exce��es
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
    ' Fun��o que executa a a��o baseada no tipo de solicita��o
    Private Function ExecutarAcaoChamado() As String
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
        Try
            ' Coletando valores dos campos necess�rios
            Dim tipoSolicitacao As String = hfTipoSolicitacao.Value.ToLower().Replace(" ", "-").Replace("/", "-")
            Dim idChamado As Integer = If(Not String.IsNullOrEmpty(hfIdChamado.Value), Convert.ToInt32(hfIdChamado.Value), 0)
            Dim servicePack As String = hfServicePack.Value
            Dim comentariosAtivo As String = ""
            ' Ajusta o campo "comentarios" dependendo do tipo de solicita��o
            Select Case tipoSolicitacao
                ' [IN�CIO - ICTRL-NF-202506-002]
                Case "simcard-m2m---nova-linha"
                    comentariosAtivo = "Linha: " & hfNovaLinha.Value & " | Plano: " & hfnomePlanoMigracaoNL.Value
                    pCampo1 = hfNovaLinha.Value
                    pCampo2 = hfnomePlanoMigracaoNL.Value
                ' [FIM - ICTRL-NF-202506-002]
                Case c_acao_pacote_roaming
                    If Not String.IsNullOrEmpty(servicePack) Then
                        comentariosAtivo = "Pacote contratado: " & servicePack
                        pCampo1 = servicePack
                    End If
                Case c_acao_migracao_plano
                    comentariosAtivo = hfMigrationDevice.Value
                    pCampo1 = hfMigrationDevice.Value
                Case c_acao_nova_linha
                    comentariosAtivo = "Linha: " & hfNovaLinha.Value & " | Plano: " & hfnomePlanoMigracaoNL.Value
                    pCampo1 = hfNovaLinha.Value
                    pCampo2 = hfnomePlanoMigracaoNL.Value
                Case c_acao_alterar_ddd
                    comentariosAtivo = "Linha: (" & hfNewAreaCode.Value & ") " & hfNovaLinha.Value
                    pCampo1 = hfNovaLinha.Value
                Case c_acao_alterar_numero
                    comentariosAtivo = "Linha: " & hfAlterarLinha.Value
                    pCampo1 = hfNovaLinha.Value
                Case c_acao_portabilidade
                    comentariosAtivo = "Nome do plano: " & hfNomePlanoPortabilidade.Value & " | Data de recebimento do chip: " & hfDataRecebimentoChip.Value & " | Data de efetiva��o da portabilidade: " & hfDataEfetivacaoPortabilidade.Value
                    pCampo1 = hfNomePlanoPortabilidade.Value
                    pCampo2 = hfDataRecebimentoChip.Value
                    pCampo3 = hfDataEfetivacaoPortabilidade.Value
            End Select

            ' [IN�CIO - ICTRL-NF-202506-001 | 2025-06-21 | Parceiro IControlIT]
            Select Case tipoSolicitacao
                ' ... outros cases ...
                Case "e-sim-troca-de-chip-virtual"
                    comentariosAtivo = "Novo SIM Card: " & hfNovoSimCard.Value
                    pCampo1 = hfNovoSimCard.Value
            End Select
            ' [FIM - ICTRL-NF-202506-001]

            ' Chama a fun��o que controla o tipo de solicita��o, exceto para "alterar-proprietario"
            Return WS_Chamado.ExecutarAcaoAtivo(Session("Conn_Banco"), "dbo.pa_Ativo_Chamado", tipoSolicitacao, idChamado, comentariosAtivo, pCampo1, pCampo2, pCampo3, pCampo4, pCampo5, pCampo6, True)
        Catch ex As SqlClient.SqlException
            Throw
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function EnviarEmailChamado()
        Dim emailsSelecionados As String = hfEmailsSelecionados.Value ' Emails separados por v�rgula
        Dim emailRespRegional As String = hfEmailResponsavelRegional.Value
        ' Verificar e substituir a v�rgula por ponto e v�rgula
        emailRespRegional = emailRespRegional.Replace(" ", "")
        If emailRespRegional.Contains(",") Then
            emailRespRegional = emailRespRegional.Replace(",", ";")
        End If
        ' Recuperando os valores da p�gina
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
            pTextoAdicional &= "<strong>Nome Do usu�rio:</strong> " & nomeUsuario & "<br />"
        End If
        If Not String.IsNullOrEmpty(idTransacao) Then
            pTextoAdicional &= "<strong>ID da transa��o:</strong> " & idTransacao & "<br />"
        End If
        If Not String.IsNullOrEmpty(tipoSolicitacao) Then
            pTextoAdicional &= "<strong>Tipo de Solicita��o:</strong> " & tipoSolicitacao & "<br />"
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
                    ' Converter o conte�do base64 de volta para um byte array
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
                    ' Qualquer outro erro gen�rico
                    EscreveLog($"Erro ao processar arquivo '{arquivoBase64("nome").ToString()}': {ex.Message}")
                End Try
            Next
        End If
        ' Chamar a fun��o para enviar o e-mail com o HTML gerado e anexos
        Dim msg As String = AgendarEnvioEmail(emailsSelecionados, emailRespRegional, pTextoAdicional, pAssuntoEmail, arquivosAnexos)
        Return msg
    End Function
    Public Function GetBadgeClass(estado As Object) As String
        If estado IsNot Nothing Then
            If estado.ToString() = "Pendente" Then
                Return "azul"
            ElseIf estado.ToString() = "Conclu�do" Then
                Return "verde"
            ElseIf estado.ToString() = "Cancelado" Then
                Return "vermelho"
            End If
        End If
        Return "azul" ' Classe padr�o para outros estados ou nulo
    End Function
    ' Fun��o para buscar e-mails da operadora por id_Conglomerado
    <WebMethod()>
    Public Shared Function BuscarEmailsOperadora(ByVal idConglomerado As Integer) As String
        Try
            ' Chamar o m�todo Operadora do WebService
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_emails_operadora", idConglomerado, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
            ' Verifica se o DataSet cont�m resultados
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
            ' Configura��o da conex�o com o banco de dados
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
    ' Fun��o para buscar e-mails da operadora por id_Conglomerado
    <WebMethod()>
    Public Shared Function BuscarFaturaAgrupadora(ByVal idConglomerado As Integer) As String
        Try
            ' Chamar o m�todo Operadora do WebService
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_fatura_agrupadora", idConglomerado, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
            ' Verifica se o DataSet cont�m resultados
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
            ' Chamar o m�todo Operadora do WebService
            Dim WS_Chamado As New WS_GUA_Chamado.WSChamado
            Dim pPConn_Banco As String = HttpContext.Current.Session("Conn_Banco")
            Dim ds As DataSet = WS_Chamado.ChamadoAuxiliar(pPConn_Banco, "buscar_nomes_filiais", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
            ' Verifica se o DataSet cont�m resultados
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ' Limpa as op��es existentes no DropDownList (empresaContratante)
                empresaContratante.Items.Clear()
                ' Adiciona a op��o padr�o
                empresaContratante.Items.Add(New ListItem("Selecione a Empresa", ""))
                ' Preenche o DropDownList com os dados do DataSet
                For Each row As DataRow In ds.Tables(0).Rows
                    Dim nomeFilial As String = row("Nm_Filial").ToString()
                    empresaContratante.Items.Add(New ListItem(nomeFilial, nomeFilial))
                Next
            Else
                ' Adiciona uma mensagem padr�o se n�o houver resultados
                empresaContratante.Items.Clear()
                empresaContratante.Items.Add(New ListItem("Nenhuma empresa dispon�vel", ""))
            End If
        Catch ex As Exception
            ' Tratamento de exce��es
            empresaContratante.Items.Clear()
            empresaContratante.Items.Add(New ListItem("Erro ao carregar as empresas", ""))
        End Try
    End Sub
    Protected Sub ddlItemsPerPage_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' Recupera o valor selecionado no dropdown
        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)
        ' Atualiza a p�gina��o e a exibi��o dos chamados com o novo n�mero de itens por p�gina
        ' Aqui, voc� deve chamar seu m�todo de exibi��o com o novo valor de itemsPerPage
        BindChamados(CurrentPage, itemsPerPage)
    End Sub
    ' Fun��o para agendar envio de email
    Private Function AgendarEnvioEmail(ByVal pEmailRespRegional As String, ByVal pEmailOperadora As String, ByVal pTextoAdicional As String, ByVal pAssuntoEmail As String, ByVal anexos As List(Of AnexoModel)) As String
        Try
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
                    ' Concatenar o nome do arquivo e o caminho f�sico
                    If Not String.IsNullOrEmpty(caminhosArquivosConcatenados) Then
                        caminhosArquivosConcatenados &= delimitador
                    End If
                    caminhosArquivosConcatenados &= anexo.CaminhoArquivo
                Next
                ' Chama o servi�o web para agendar o disparo com os anexos fora do loop
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
            ' Verifica se h� anexos
            If anexos IsNot Nothing AndAlso anexos.Count > 0 Then
                Dim caminhosArquivosConcatenados As String = ""
                Dim delimitador As String = "|"
                For Each anexo In anexos
                    ' Concatenar o nome do arquivo e o caminho f�sico
                    If Not String.IsNullOrEmpty(caminhosArquivosConcatenados) Then
                        caminhosArquivosConcatenados &= delimitador
                    End If
                    caminhosArquivosConcatenados &= anexo.CaminhoArquivo
                Next
                ' Chama o servi�o web para agendar o disparo com os anexos fora do loop
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

            ' Escapa caracteres especiais para que a mensagem seja exibida corretamente no JavaScript
            Dim msgEscapada As String = msg.Replace("'", "\'").Replace(vbCrLf, "\n")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{msgEscapada}');", True)

            ' Apenas atualiza a lista de chamados se o envio de e-mail n�o retornou um erro
            If Not msg.ToLower().Contains("erro") Then
                BindChamados(CurrentPage, itemsPerPage)
            End If

        Catch ex As Exception
            ' Se ocorrer qualquer erro inesperado durante o processo, ele ser� capturado aqui.
            ' Isso evita que o modal feche silenciosamente.
            Dim errorMsg As String = "Ocorreu um erro inesperado ao tentar enviar o e-mail: " & ex.Message.Replace("'", "\'").Replace(vbCrLf, " ")
            EscreveLog("(Consulta_Chamado.btnEnviarEmail_Click) Erro Detalhado: " & ex.ToString()) ' Grava o erro completo no log para depura��o

            ' Exibe o alerta de erro para o usu�rio.
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{errorMsg}');", True)
        End Try
    End Sub

    ' [IN�CIO - ICTRL-NF-202506-012]

    ' Propriedade para armazenar o termo de busca entre postbacks
    Private Property TermoBusca As String
        Get
            Return If(ViewState("TermoBusca") Is Nothing, String.Empty, ViewState("TermoBusca").ToString())
        End Get
        Set(ByVal value As String)
            ViewState("TermoBusca") = value
        End Set
    End Property

    ' Evento do bot�o de busca
    Protected Sub btnBusca_Click(sender As Object, e As EventArgs)
        Me.TermoBusca = txtBusca.Text.Trim()
        Me.CurrentPage = 1 ' Reseta para a primeira p�gina ao buscar
        BindChamados(Me.CurrentPage, Convert.ToInt32(ddlItemsPerPage.SelectedValue))
    End Sub

    ' Evento do bot�o para limpar a busca
    Protected Sub btnLimparBusca_Click(sender As Object, e As EventArgs)
        Me.TermoBusca = String.Empty
        txtBusca.Text = String.Empty
        Me.CurrentPage = 1
        BindChamados(Me.CurrentPage, Convert.ToInt32(ddlItemsPerPage.SelectedValue))
    End Sub

    ' [FIM - ICTRL-NF-202506-012]


    ' [IN�CIO - ICTRL-NF-202506-006]
    Protected Sub btnConfirmarCancelamento_Click(sender As Object, e As EventArgs)
        Dim idChamado As Integer = Convert.ToInt32(hfIdChamado.Value)
        Dim motivoCancelamento As String = hfCancellationComment.Value
        Dim itemsPerPage As Integer = Convert.ToInt32(ddlItemsPerPage.SelectedValue)
        Dim mensagemRetorno As String = ""

        Try
            ' PASSO 1: Tenta notificar o ServiceNow PRIMEIRO.
            DispararRequisicaoSimples("CANCELADO", motivoCancelamento)

            ' PASSO 2: Se a notifica��o acima n�o falhar, executa a a��o no banco de dados.
            mensagemRetorno = WS_Chamado.ExecutarAcaoAtivo(Session("Conn_Banco"),
                                                    "dbo.pa_Ativo_Chamado",
                                                    "cancelar-manualmente",
                                                    idChamado,
                                                    motivoCancelamento,
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{mensagemRetorno.Replace("'", "\'")}');", True)

        Catch ex As Exception
            ' Se QUALQUER um dos passos falhar (principalmente a notifica��o),
            ' o erro � capturado aqui e NADA � gravado no banco.
            Dim msgErro As String = $"Erro ao cancelar o chamado: {ex.Message}".Replace("'", "\'")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('{msgErro}');", True)
        End Try

        BindChamados(CurrentPage, itemsPerPage)
    End Sub
    ' [FIM - ICTRL-NF-202506-006]

End Class
