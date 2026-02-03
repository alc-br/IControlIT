Imports System.Data
Imports System.Text.RegularExpressions
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Xml.Linq

Public Class Parametros_Cadastro
    Inherits System.Web.UI.Page

    Private _snapshotAtual As Dictionary(Of String, String)

    Private Shared ReadOnly CamposHistoricoOrdenados As IReadOnlyList(Of CampoHistoricoInfo) = New List(Of CampoHistoricoInfo) From {
        New CampoHistoricoInfo("Tipo", "Tipo"),
        New CampoHistoricoInfo("CNPJ_Anima", "CNPJ Anima"),
        New CampoHistoricoInfo("Conta", "Conta"),
        New CampoHistoricoInfo("DescricaoServico", "Descricao servico"),
        New CampoHistoricoInfo("MesEmissao", "Mes emissao"),
        New CampoHistoricoInfo("RequisitioningBUId", "Requisitioning BU id"),
        New CampoHistoricoInfo("RequisitioningBUName", "Requisitioning BU name"),
        New CampoHistoricoInfo("Description", "Descricao geral"),
        New CampoHistoricoInfo("Justification", "Justificativa"),
        New CampoHistoricoInfo("PreparerEmail", "Email preparer"),
        New CampoHistoricoInfo("ApproverEmail", "Email approver"),
        New CampoHistoricoInfo("DocumentStatusCode", "Status documento"),
        New CampoHistoricoInfo("RequisitionType", "Tipo requisicao"),
        New CampoHistoricoInfo("SourceUniqueId", "Source unique id"),
        New CampoHistoricoInfo("CategoryName", "Categoria"),
        New CampoHistoricoInfo("DeliverToLocationCode", "Deliver to location code"),
        New CampoHistoricoInfo("DeliverToOrganizationCode", "Deliver to organization code"),
        New CampoHistoricoInfo("ProcurementBUName", "Procurement BU name"),
        New CampoHistoricoInfo("ItemDescription", "Descricao item"),
        New CampoHistoricoInfo("ItemNumber", "Numero item"),
        New CampoHistoricoInfo("RequesterEmail", "Email requester"),
        New CampoHistoricoInfo("SupplierName", "Fornecedor"),
        New CampoHistoricoInfo("SupplierContactName", "Contato fornecedor"),
        New CampoHistoricoInfo("SupplierSiteName", "Site fornecedor (CNPJ)"),
        New CampoHistoricoInfo("CentroDeCusto", "Centro de custo"),
        New CampoHistoricoInfo("Estabelecimento", "Estabelecimento"),
        New CampoHistoricoInfo("UnidadeNegocio", "Unidade negocio"),
        New CampoHistoricoInfo("Finalidade", "Finalidade"),
        New CampoHistoricoInfo("Projeto", "Projeto"),
        New CampoHistoricoInfo("CodigoRequisicaoCompra", "Requisicao de Compra (Oracle)"),
        New CampoHistoricoInfo("CodigoOrdemCompra", "Ordem de Compra (Oracle)"),
        New CampoHistoricoInfo("CodigoInvoice", "ID Processo (V360)"),
        New CampoHistoricoInfo("Observacao", "Observacao"),
        New CampoHistoricoInfo("Fl_Ativo", "Status ativo")
    }

#Region "Controles da PÃ¡gina"
    ' DeclaraÃ§Ãµes de todos os 30+ controles da pagina .aspx
    Protected WithEvents hfIdParametro As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents hfCodigoReferencia As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents hfPreparerEmail As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents hfApproverEmail As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents hfRequesterEmail As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents hfChkAtivo As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents hfCustomDocumentStatusCode As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents hfCustomCategoryName As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents hfCustomSupplierName As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents lblTitulo As Global.System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipo As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCNPJ_Anima As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtConta As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescricaoServico As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMesEmissao As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRequisitioningBUId As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRequisitioningBUName As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJustification As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPreparerEmail As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtApproverEmail As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlDocumentStatusCode As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtRequisitionType As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSourceUniqueId As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlCategoryName As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDeliverToLocationCode As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDeliverToOrganizationCode As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProcurementBUName As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtItemDescription As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtItemNumber As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRequesterEmail As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlSupplierName As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSupplierContactName As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSupplierSiteName As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCentroDeCusto As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEstabelecimento As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUnidadeNegocio As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFinalidade As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProjeto As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtInterCompany As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodigoRequisicaoCompra As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodigoOrdemCompra As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodigoInvoice As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtObservacao As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents chkAtivo As Global.System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkProcessamentoManual As Global.System.Web.UI.WebControls.CheckBox
    Protected WithEvents hfChkProcessamentoManual As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnSalvar As Global.System.Web.UI.WebControls.Button
    Protected WithEvents btnApagar As Global.System.Web.UI.WebControls.Button
    Protected WithEvents divHistorico As Global.System.Web.UI.WebControls.Panel
    Protected WithEvents rptHistorico As Global.System.Web.UI.WebControls.Repeater
#End Region

#Region "Rotina de Log"
    Private Sub EscreveLog(ByVal mensagem As String)
        Try
            Dim logFilePath As String = "C:\Temp\Anima_Cadastro_Log.txt"
            Dim logDirectory As String = Path.GetDirectoryName(logFilePath)
            If Not Directory.Exists(logDirectory) Then Directory.CreateDirectory(logDirectory)
            Using sw As StreamWriter = New StreamWriter(logFilePath, True)
                sw.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss.fff} - {mensagem}")
            End Using
        Catch ex As Exception
        End Try
    End Sub
#End Region

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        ' Popular dropdowns no Init para que o ViewState funcione corretamente
        PopulateDropdowns()

        ' Adicionar valores customizados dos hidden fields aos dropdowns (se existirem no Request)
        ' Isso eh necessario no Init porque os valores precisam existir antes do ViewState ser restaurado
        If IsPostBack Then
            Dim customStatus As String = Request.Form(hfCustomDocumentStatusCode.UniqueID)
            If Not String.IsNullOrEmpty(customStatus) Then
                AddItemToDropdownIfNotExists(ddlDocumentStatusCode, customStatus)
            End If

            Dim customCategory As String = Request.Form(hfCustomCategoryName.UniqueID)
            If Not String.IsNullOrEmpty(customCategory) Then
                AddItemToDropdownIfNotExists(ddlCategoryName, customCategory)
            End If

            Dim customSupplier As String = Request.Form(hfCustomSupplierName.UniqueID)
            If Not String.IsNullOrEmpty(customSupplier) Then
                AddItemToDropdownIfNotExists(ddlSupplierName, customSupplier)
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            _snapshotAtual = Nothing
            EscreveLog("----------------------------------------------------")
            EscreveLog("PRIMEIRO LOAD DA PAGINA (Not IsPostBack).")
            btnApagar.Visible = False
            Dim id As Integer = 0
            Dim idQuery As String = Request.QueryString("id")
            Dim contaQuery As String = Request.QueryString("conta")
            If Not String.IsNullOrWhiteSpace(idQuery) Then
                Integer.TryParse(idQuery, id)
            End If
            If id > 0 Then
                EscreveLog($"Carregando dados para EDICAO. ID: {id}")
                lblTitulo.Text = "Editar Parametro Anima"
                Dim codigoReferencia = CarregarParametro(id)
                If Not String.IsNullOrEmpty(codigoReferencia) Then
                    CarregarHistorico(codigoReferencia)
                    ' btnApagar.Visible = True ' Desabilitado ate segunda ordem
                Else
                    divHistorico.Visible = False
                    rptHistorico.DataSource = Nothing
                    rptHistorico.DataBind()
                End If
            ElseIf Not String.IsNullOrWhiteSpace(contaQuery) Then
                ' Novo cadastro a partir de dados da Fatura
                Dim mesEmissaoQuery As String = Request.QueryString("mesEmissao")
                EscreveLog($"Carregando dados da Fatura para conta: {contaQuery}, mesEmissao: {mesEmissaoQuery}")
                lblTitulo.Text = "Novo Parametro Anima"
                hfCodigoReferencia.Value = Guid.NewGuid().ToString()
                PreencherDadosDaFatura(contaQuery, mesEmissaoQuery)
                divHistorico.Visible = False
                rptHistorico.DataSource = Nothing
                rptHistorico.DataBind()
                btnApagar.Visible = False
            Else
                EscreveLog("Carregando pagina para um NOVO cadastro.")
                lblTitulo.Text = "Novo Parametro Anima"
                hfCodigoReferencia.Value = Guid.NewGuid().ToString()
                PreencherValoresPadrao()
                btnApagar.Visible = False
            End If
        End If
    End Sub

    Private Sub PopulateDropdowns()
        ' Dropdown Tipo - SEM opcao "Outro" - so popular se estiver vazio
        If ddlTipo.Items.Count = 0 Then
            ddlTipo.Items.Add(New ListItem("Selecione...", ""))
            ddlTipo.Items.Add(New ListItem("Servico", "Servico"))
            ddlTipo.Items.Add(New ListItem("Servico-Hibrida", "Servico-Hibrida"))
            ddlTipo.Items.Add(New ListItem("Telecom", "Telecom"))
            ddlTipo.Items.Add(New ListItem("Telecom-Hibrida", "Telecom-Hibrida"))
        End If

        ' Dropdown Status - COM opcao "Outro" - so popular se estiver vazio
        If ddlDocumentStatusCode.Items.Count = 0 Then
            ddlDocumentStatusCode.Items.Add(New ListItem("Selecione...", ""))
            ddlDocumentStatusCode.Items.Add(New ListItem("Aprovado", "APPROVED"))
            ddlDocumentStatusCode.Items.Add(New ListItem("Pendente", "PENDING"))
            ddlDocumentStatusCode.Items.Add(New ListItem("Rejeitado", "REJECTED"))
            ddlDocumentStatusCode.Items.Add(New ListItem("Outro...", "Outro"))
        End If

        ' Dropdown Categoria - COM opcao "Outro" - so popular se estiver vazio
        If ddlCategoryName.Items.Count = 0 Then
            ddlCategoryName.Items.Add(New ListItem("Selecione...", ""))
            ddlCategoryName.Items.Add(New ListItem("TELEFONIA E OUTROS SERVICOS DE TELECOMUNICACOES", "TELEFONIA E OUTROS SERVICOS DE TELECOMUNICACOES"))
            ddlCategoryName.Items.Add(New ListItem("SERVICO DE INTERNET", "SERVICO DE INTERNET"))
            ddlCategoryName.Items.Add(New ListItem("Outro...", "Outro"))
        End If

        ' Dropdown Fornecedor - COM opcao "Outro" - so popular se estiver vazio
        If ddlSupplierName.Items.Count = 0 Then
            ddlSupplierName.Items.Add(New ListItem("Selecione...", ""))
            ddlSupplierName.Items.Add(New ListItem("CLARO SA", "CLARO SA"))
            ddlSupplierName.Items.Add(New ListItem("TELMEX DO BRASIL S/A", "TELMEX DO BRASIL S/A"))
            ddlSupplierName.Items.Add(New ListItem("PROVECOM TELECOMUNICACOES LTDA - EPP", "PROVECOM TELECOMUNICACOES LTDA - EPP"))
            ddlSupplierName.Items.Add(New ListItem("MUNDIAL NET TELECOM LTDA - TUC", "MUNDIAL NET TELECOM LTDA - TUC"))
            ddlSupplierName.Items.Add(New ListItem("Outro...", "Outro"))
        End If
    End Sub

    Private Sub AddItemToDropdownIfNotExists(ByVal ddl As DropDownList, ByVal itemValue As String)
        If ddl.Items.FindByValue(itemValue) Is Nothing Then
            ddl.Items.Add(New ListItem(itemValue, itemValue))
        End If
    End Sub

    Private Sub PreencherValoresPadrao()
        ' Para novo cadastro sem parametros, deixar dropdowns em "Selecione..."
        ' e campos vazios para que o usuario preencha
        ddlTipo.SelectedIndex = 0  ' Selecione...
        ddlDocumentStatusCode.SelectedIndex = 0  ' Selecione...
        ddlCategoryName.SelectedIndex = 0  ' Selecione...
        ddlSupplierName.SelectedIndex = 0  ' Selecione...
        txtMesEmissao.Text = ""
        chkAtivo.Checked = True
        hfChkAtivo.Value = chkAtivo.Checked.ToString().ToLowerInvariant()
    End Sub

    Private Sub PreencherDadosDaFatura(conta As String, mesEmissao As String)
        ' Buscar dados da Fatura usando SELECT_FATURA_BY_CONTA
        Dim wsCadastro As New WS_GUA_Cadastro.WSCadastro()
        wsCadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Try
            EscreveLog($"Buscando dados da Fatura para conta: {conta}, mesEmissao: {mesEmissao}")
            Dim ds As DataSet = wsCadastro.Parametros_Anima(
                pPConn_Banco:=Session("Conn_Banco").ToString(),
                pAcao:="SELECT_FATURA_BY_CONTA",
                pId_Parametro:=0,
                pCodigo_Referencia:=Nothing,
                pTipo:=Nothing,
                pCNPJ_Anima:=Nothing,
                pConta:=conta,
                pDescricaoServico:=Nothing,
                pMesEmissao:=mesEmissao,
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
                pUsuario:=Nothing,
                pFl_Ativo:=False,
                pProcessamento_Manual:=False
            )

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim row As DataRow = ds.Tables(0).Rows(0)
                EscreveLog($"Dados da Fatura encontrados para conta: {conta}")

                ' Preencher campos baseados no de/para da Fatura
                ' Fatura.Nr_Fatura -> Conta
                txtConta.Text = SafeGetString(row, "Conta")

                ' FORMAT(Dt_Lote, 'MM/yyyy') -> MesEmissao
                txtMesEmissao.Text = SafeGetString(row, "MesEmissao")

                ' Empresa.CNPJ -> CNPJ_Anima
                Dim cnpjAnima As String = SafeGetString(row, "CNPJ_Anima")
                If Not String.IsNullOrWhiteSpace(cnpjAnima) Then
                    txtCNPJ_Anima.Text = cnpjAnima
                End If

                ' Empresa.Cd_Empresa -> RequisitioningBUName
                Dim requisitioningBUName As String = SafeGetString(row, "RequisitioningBUName")
                If Not String.IsNullOrWhiteSpace(requisitioningBUName) Then
                    txtRequisitioningBUName.Text = requisitioningBUName
                End If

                ' Conglomerado.Nm_Conglomerado -> SupplierName
                Dim supplierName As String = SafeGetString(row, "SupplierName")
                If Not String.IsNullOrWhiteSpace(supplierName) Then
                    AddItemToDropdownIfNotExists(ddlSupplierName, supplierName)
                    ddlSupplierName.SelectedValue = supplierName
                Else
                    SelecionarOpcaoPadrao(ddlSupplierName, "CLARO SA")
                End If

                ' Conglomerado.Nm_Conglomerado -> SupplierContactName
                Dim supplierContactName As String = SafeGetString(row, "SupplierContactName")
                If Not String.IsNullOrWhiteSpace(supplierContactName) Then
                    txtSupplierContactName.Text = supplierContactName
                End If

                ' Ativo_Tipo_Grupo.Nm_Ativo_Tipo_Grupo -> Description
                Dim description As String = SafeGetString(row, "Description")
                If Not String.IsNullOrWhiteSpace(description) Then
                    txtDescription.Text = description
                End If

                ' Valores padrao para campos nao mapeados
                SelecionarOpcaoPadrao(ddlTipo, "Servico-Hibrida")
                SelecionarOpcaoPadrao(ddlDocumentStatusCode, "APPROVED")
                SelecionarOpcaoPadrao(ddlCategoryName, "TELEFONIA E OUTROS SERVICOS DE TELECOMUNICACOES")

                chkAtivo.Checked = True
                hfChkAtivo.Value = chkAtivo.Checked.ToString().ToLowerInvariant()

                EscreveLog("Campos preenchidos com sucesso a partir dos dados da Fatura.")
            Else
                ' Se nao encontrar dados da Fatura, preencher apenas a conta e valores padrao
                EscreveLog($"Nenhum dado encontrado para conta: {conta}. Usando valores padrao.")
                txtConta.Text = conta
                PreencherValoresPadrao()
            End If

        Catch ex As Exception
            EscreveLog($"ERRO ao buscar dados da Fatura: {ex.Message}")
            ' Em caso de erro, preencher apenas a conta
            txtConta.Text = conta
            PreencherValoresPadrao()
        End Try
    End Sub

    Private Function SafeGetString(row As DataRow, columnName As String) As String
        If row Is Nothing OrElse Not row.Table.Columns.Contains(columnName) Then
            Return String.Empty
        End If
        Dim value = row(columnName)
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return String.Empty
        End If
        Return value.ToString().Trim()
    End Function

    Private Sub SelecionarOpcaoPadrao(ByVal ddl As DropDownList, ByVal valorPreferencial As String)
        If ddl Is Nothing Then Return

        If Not String.IsNullOrEmpty(valorPreferencial) Then
            Dim itemPreferencial As System.Web.UI.WebControls.ListItem = ddl.Items.FindByValue(valorPreferencial)
            If itemPreferencial IsNot Nothing Then
                ddl.ClearSelection()
                itemPreferencial.Selected = True
                Return
            End If
        End If

        For Each item As System.Web.UI.WebControls.ListItem In ddl.Items
            If Not String.IsNullOrEmpty(item.Value) Then
                ddl.ClearSelection()
                item.Selected = True
                Exit For
            End If
        Next
    End Sub

    Private Function CarregarParametro(ByVal id As Integer) As String
        Dim wsCadastro As New WS_GUA_Cadastro.WSCadastro()
        wsCadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Try
            Dim ds As DataSet = wsCadastro.Parametros_Anima(
                pPConn_Banco:=Session("Conn_Banco").ToString(),
                pAcao:="SELECT_BY_ID",
                pId_Parametro:=id,
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
                pUsuario:=Nothing,
                pFl_Ativo:=False,
                pProcessamento_Manual:=False
            )
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim row As DataRow = ds.Tables(0).Rows(0)
                hfIdParametro.Value = row("Id_Parametro").ToString()
                Dim codigoReferenciaAtual As String = row("Codigo_Referencia").ToString().Trim()
                hfCodigoReferencia.Value = codigoReferenciaAtual
                AddItemToDropdownIfNotExists(ddlTipo, row("Tipo").ToString())
                ddlTipo.SelectedValue = row("Tipo").ToString()
                AddItemToDropdownIfNotExists(ddlDocumentStatusCode, row("DocumentStatusCode").ToString())
                ddlDocumentStatusCode.SelectedValue = row("DocumentStatusCode").ToString()
                AddItemToDropdownIfNotExists(ddlCategoryName, row("CategoryName").ToString())
                ddlCategoryName.SelectedValue = row("CategoryName").ToString()
                AddItemToDropdownIfNotExists(ddlSupplierName, row("SupplierName").ToString())
                ddlSupplierName.SelectedValue = row("SupplierName").ToString()
                txtCNPJ_Anima.Text = row("CNPJ_Anima").ToString()
                txtConta.Text = row("Conta").ToString()
                txtDescricaoServico.Text = row("DescricaoServico").ToString()
                txtMesEmissao.Text = row("MesEmissao").ToString()
                txtRequisitioningBUId.Text = row("RequisitioningBUId").ToString()
                txtRequisitioningBUName.Text = row("RequisitioningBUName").ToString()
                txtDescription.Text = row("Description").ToString()
                txtJustification.Text = row("Justification").ToString()
                txtPreparerEmail.Text = row("PreparerEmail").ToString()
                hfPreparerEmail.Value = txtPreparerEmail.Text
                txtApproverEmail.Text = row("ApproverEmail").ToString()
                hfApproverEmail.Value = txtApproverEmail.Text
                txtRequisitionType.Text = row("RequisitionType").ToString()
                txtSourceUniqueId.Text = row("SourceUniqueId").ToString()
                txtDeliverToLocationCode.Text = row("DeliverToLocationCode").ToString()
                txtDeliverToOrganizationCode.Text = row("DeliverToOrganizationCode").ToString()
                txtProcurementBUName.Text = row("ProcurementBUName").ToString()
                txtItemDescription.Text = row("ItemDescription").ToString()
                txtItemNumber.Text = row("ItemNumber").ToString()
                txtRequesterEmail.Text = row("RequesterEmail").ToString()
                hfRequesterEmail.Value = txtRequesterEmail.Text
                txtSupplierContactName.Text = row("SupplierContactName").ToString()
                txtSupplierSiteName.Text = row("SupplierSiteName").ToString()
                txtCentroDeCusto.Text = row("CentroDeCusto").ToString()
                txtEstabelecimento.Text = row("Estabelecimento").ToString()
                txtUnidadeNegocio.Text = row("UnidadeNegocio").ToString()
                txtFinalidade.Text = row("Finalidade").ToString()
                txtProjeto.Text = row("Projeto").ToString()
                txtInterCompany.Text = row("InterCompany").ToString()
                txtCodigoRequisicaoCompra.Text = row("CodigoRequisicaoCompra").ToString()
                txtCodigoOrdemCompra.Text = row("CodigoOrdemCompra").ToString()
                txtCodigoInvoice.Text = row("CodigoInvoice").ToString()
                txtObservacao.Text = row("Observacao").ToString()
                chkAtivo.Checked = Convert.ToBoolean(row("Fl_Ativo"))
                hfChkAtivo.Value = chkAtivo.Checked.ToString().ToLowerInvariant()
                ' Carregar Processamento_Manual
                If row.Table.Columns.Contains("Processamento_Manual") AndAlso Not row.IsNull("Processamento_Manual") Then
                    chkProcessamentoManual.Checked = Convert.ToBoolean(row("Processamento_Manual"))
                Else
                    chkProcessamentoManual.Checked = False
                End If
                hfChkProcessamentoManual.Value = chkProcessamentoManual.Checked.ToString().ToLowerInvariant()
                _snapshotAtual = ConverterDataRowParaSnapshot(row)
                Return codigoReferenciaAtual
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('Ocorreu um erro ao carregar os dados: {ex.Message.Replace("'", "\'")}');", True)
            Return Nothing
        End Try
        Return Nothing
    End Function

    Private Sub CarregarHistorico(ByVal codigoReferencia As String)
        If String.IsNullOrWhiteSpace(codigoReferencia) Then
            divHistorico.Visible = False
            rptHistorico.DataSource = Nothing
            rptHistorico.DataBind()
            Return
        End If
        Dim codigoNormalizado As String = codigoReferencia.Trim()
        Dim guidReferencia As Guid
        If Guid.TryParse(codigoNormalizado, guidReferencia) Then
            codigoNormalizado = guidReferencia.ToString()
        End If
        Dim wsCadastro As New WS_GUA_Cadastro.WSCadastro()
        wsCadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Try
            EscreveLog($"Carregando historico para codigo de referencia {codigoNormalizado}.")
            Dim ds As DataSet = wsCadastro.Parametros_Anima(
                pPConn_Banco:=Session("Conn_Banco").ToString(),
                pAcao:="SELECT_HISTORY",
                pId_Parametro:=0,
                pCodigo_Referencia:=codigoNormalizado,
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
                pUsuario:=Nothing,
                pFl_Ativo:=False,
                pProcessamento_Manual:=False
            )
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim historico = MontarHistorico(ds.Tables(0))
                EscreveLog($"Total de versoes encontradas: {historico.Count}.")
                If historico.Count > 0 Then
                    divHistorico.Visible = True
                    rptHistorico.DataSource = historico
                    rptHistorico.DataBind()
                    ' Mostrar botao de historico
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mostrarBtnHistorico", "document.getElementById('btnHistorico').style.display = 'inline-block';", True)
                Else
                    divHistorico.Visible = False
                    rptHistorico.DataSource = Nothing
                    rptHistorico.DataBind()
                End If
            Else
                divHistorico.Visible = False
                rptHistorico.DataSource = Nothing
                rptHistorico.DataBind()
            End If
        Catch ex As Exception
            divHistorico.Visible = False
            rptHistorico.DataSource = Nothing
            rptHistorico.DataBind()
            EscreveLog("ERRO AO CARREGAR HISTORICO: " & ex.Message)
        End Try
    End Sub

    Private Function MontarHistorico(table As DataTable) As List(Of HistoricoVersao)
        Dim versoes As New List(Of HistoricoVersao)()
        If table Is Nothing Then Return versoes

        Dim linhasOrdenadas = table.AsEnumerable().OrderBy(Function(r) Convert.ToDateTime(r("Data_Modificacao"))).ToList()
        If linhasOrdenadas.Count = 0 Then Return versoes

        Dim estadosTemporarios As New List(Of HistoricoVersaoTemp)()

        For i As Integer = 0 To linhasOrdenadas.Count - 1
            Dim linha = linhasOrdenadas(i)
            Dim proximaLinha As DataRow = If(i + 1 < linhasOrdenadas.Count, linhasOrdenadas(i + 1), Nothing)
            Dim estado = ObterEstadoParaLinhaHistorico(linha, proximaLinha)

            Dim acaoTexto As String = If(linha.IsNull("Acao_Realizada"), String.Empty, linha("Acao_Realizada").ToString())
            Dim usuarioTexto As String = If(linha.IsNull("Usuario_Modificacao"), String.Empty, linha("Usuario_Modificacao").ToString())
            If String.IsNullOrWhiteSpace(usuarioTexto) Then
                usuarioTexto = "Nao informado"
            End If
            Dim dataModificacao As DateTime = If(linha.IsNull("Data_Modificacao"), DateTime.MinValue, Convert.ToDateTime(linha("Data_Modificacao")))

            estadosTemporarios.Add(New HistoricoVersaoTemp With {
                .Acao = acaoTexto,
                .Usuario = usuarioTexto,
                .Data = dataModificacao,
                .Estado = estado
            })
        Next

        Dim numeroVersaoSequencial As Integer = 0

        For i As Integer = 0 To estadosTemporarios.Count - 1
            Dim estadoAtual = estadosTemporarios(i).Estado
            Dim estadoComparacao As Dictionary(Of String, String) = Nothing

            If i + 1 < estadosTemporarios.Count Then
                estadoComparacao = estadosTemporarios(i + 1).Estado
            ElseIf _snapshotAtual IsNot Nothing Then
                estadoComparacao = _snapshotAtual
            End If

            ' Para a primeira versao (INSERT), se o estado estiver vazio, usar o snapshot atual
            If i = 0 AndAlso (estadoAtual Is Nothing OrElse estadoAtual.Count = 0) AndAlso _snapshotAtual IsNot Nothing Then
                estadoAtual = _snapshotAtual
                estadosTemporarios(i).Estado = estadoAtual
                EscreveLog("Historico Versao 1: usando _snapshotAtual pois estado estava vazio.")
            End If

            Dim campos = MapearCampos(estadoAtual, estadoComparacao)
            Dim camposExibidos As List(Of HistoricoCampo)

            If i = 0 Then
                ' Primeira versao: mostra TODOS os campos (todos os dados cadastrados)
                camposExibidos = campos
            Else
                ' Versoes seguintes: mostra apenas campos alterados
                camposExibidos = campos.FindAll(Function(c) c.Alterado)
            End If

            If i > 0 AndAlso camposExibidos.Count = 0 Then
                Continue For
            End If

            ' Incrementa o numero da versao apenas quando uma versao e realmente adicionada
            numeroVersaoSequencial += 1

            versoes.Add(New HistoricoVersao With {
                .NumeroVersao = numeroVersaoSequencial,
                .Acao = estadosTemporarios(i).Acao,
                .Usuario = estadosTemporarios(i).Usuario,
                .Data = estadosTemporarios(i).Data,
                .Campos = camposExibidos
            })
        Next

        Return versoes
    End Function

    Private Function ObterEstadoParaLinhaHistorico(linha As DataRow, proximaLinha As DataRow) As Dictionary(Of String, String)
        If linha Is Nothing Then
            Return New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        End If

        Dim dadosXml As String = If(linha.IsNull("Dados_Anteriores"), String.Empty, linha("Dados_Anteriores").ToString())
        If Not String.IsNullOrWhiteSpace(dadosXml) Then
            Return ConverterXmlParaSnapshot(dadosXml)
        End If

        Dim acao As String = If(linha.IsNull("Acao_Realizada"), String.Empty, linha("Acao_Realizada").ToString()).Trim().ToUpperInvariant()
        If acao = "INSERT" Then
            ' Para o INSERT (primeira versao), tenta obter os dados de varias fontes:
            ' 1. Do proximo registro (Dados_Anteriores contem o estado antes da alteracao)
            If proximaLinha IsNot Nothing AndAlso Not proximaLinha.IsNull("Dados_Anteriores") Then
                Dim proximoXml As String = proximaLinha("Dados_Anteriores").ToString()
                If Not String.IsNullOrWhiteSpace(proximoXml) Then
                    Return ConverterXmlParaSnapshot(proximoXml)
                End If
            End If

            ' 2. Do snapshot atual (dados carregados da tela de edicao)
            If _snapshotAtual IsNot Nothing Then
                Return New Dictionary(Of String, String)(_snapshotAtual, StringComparer.OrdinalIgnoreCase)
            End If

            ' 3. Da propria linha do historico, tentando extrair campos diretamente
            Return ConverterDataRowParaSnapshot(linha)
        End If

        Return New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    End Function

    Private Shared Function ConverterDataRowParaSnapshot(row As DataRow) As Dictionary(Of String, String)
        Dim resultado As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        If row Is Nothing Then Return resultado

        For Each info In CamposHistoricoOrdenados
            If row.Table.Columns.Contains(info.NomeCampo) Then
                Dim valor = row(info.NomeCampo)
                resultado(info.NomeCampo) = If(valor Is Nothing OrElse valor Is DBNull.Value, String.Empty, valor.ToString())
            End If
        Next

        Return resultado
    End Function

    Private Shared Function ConverterXmlParaSnapshot(xml As String) As Dictionary(Of String, String)
        Dim resultado As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        If String.IsNullOrWhiteSpace(xml) Then Return resultado

        Try
            Dim conteudo As String = "<root>" & xml & "</root>"
            Dim documento = XDocument.Parse(conteudo)
            Dim registro = documento.Root.Elements().FirstOrDefault()
            If registro IsNot Nothing Then
                For Each atributo In registro.Attributes()
                    Dim chave = atributo.Name.LocalName
                    If Not resultado.ContainsKey(chave) Then
                        resultado(chave) = atributo.Value
                    End If
                Next

                For Each elemento In registro.Elements()
                    Dim chave = elemento.Name.LocalName
                    If Not resultado.ContainsKey(chave) Then
                        resultado(chave) = elemento.Value
                    End If
                Next
            End If
        Catch
            ' Ignora erros de parse e retorna colecao vazia
        End Try

        Return resultado
    End Function

    Private Function MapearCampos(snapshot As Dictionary(Of String, String), estadoComparacao As Dictionary(Of String, String)) As List(Of HistoricoCampo)
        Dim campos As New List(Of HistoricoCampo)()

        For Each info In CamposHistoricoOrdenados
            Dim valorBruto As String = String.Empty
            If snapshot IsNot Nothing Then
                snapshot.TryGetValue(info.NomeCampo, valorBruto)
            End If
            Dim valorFormatado As String = FormatarValorCampo(info.NomeCampo, valorBruto)

            Dim valorComparacao As String = String.Empty
            Dim possuiComparacao As Boolean = False
            If estadoComparacao IsNot Nothing Then
                possuiComparacao = estadoComparacao.TryGetValue(info.NomeCampo, valorComparacao)
                If possuiComparacao Then
                    valorComparacao = FormatarValorCampo(info.NomeCampo, valorComparacao)
                End If
            End If

            Dim alterado As Boolean
            If possuiComparacao Then
                alterado = Not String.Equals(valorFormatado, valorComparacao, StringComparison.OrdinalIgnoreCase)
            ElseIf estadoComparacao Is Nothing Then
                alterado = False
            Else
                alterado = Not String.IsNullOrEmpty(valorFormatado)
            End If

            Dim rotuloCampo As String = If(String.IsNullOrWhiteSpace(info.Rotulo), info.NomeCampo, info.Rotulo)

            campos.Add(New HistoricoCampo With {
                .Nome = rotuloCampo,
                .Valor = valorFormatado,
                .Alterado = alterado
            })
        Next

        Return campos
    End Function

    Private Function FormatarValorCampo(nomeCampo As String, valor As String) As String
        If String.IsNullOrWhiteSpace(valor) Then Return String.Empty

        Dim texto = valor.Trim()
        Select Case nomeCampo.ToUpperInvariant()
            Case "CNPJ_ANIMA", "SUPPLIERSITENAME"
                Return FormatarCnpj(texto)
            Case "MESEMISSAO"
                If texto.Contains("/") Then Return texto
                If texto.Length = 6 Then
                    Return texto.Substring(0, 2) & "/" & texto.Substring(2)
                End If
            Case "FL_ATIVO"
                If texto.Equals("1") OrElse texto.Equals("true", StringComparison.OrdinalIgnoreCase) Then
                    Return "Ativo"
                ElseIf texto.Equals("0") OrElse texto.Equals("false", StringComparison.OrdinalIgnoreCase) Then
                    Return "Inativo"
                End If
        End Select

        Return texto
    End Function

    Private Shared Function FormatarCnpj(cnpj As String) As String
        If String.IsNullOrWhiteSpace(cnpj) Then Return String.Empty
        Dim numeros = Regex.Replace(cnpj, "[^\d]", "")
        If numeros.Length <> 14 Then Return cnpj

        Dim valorNumerico As ULong
        If ULong.TryParse(numeros, valorNumerico) Then
            Return valorNumerico.ToString("00\.000\.000\/0000\-00")
        End If

        Return cnpj
    End Function

    Protected Sub rptHistorico_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptHistorico.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem Then
            Return
        End If

        Dim versao = TryCast(e.Item.DataItem, HistoricoVersao)
        If versao Is Nothing Then Return

        Dim repeaterCampos = TryCast(e.Item.FindControl("rptCampos"), Repeater)
        If repeaterCampos Is Nothing Then Return

        repeaterCampos.DataSource = versao.Campos
        repeaterCampos.DataBind()
    End Sub

    Private Function RemoverMascara(ByVal textoComMascara As String) As String
        Return Regex.Replace(textoComMascara, "[^\d]", "")
    End Function

    Protected Sub BtnSalvar_Click(sender As Object, e As EventArgs)
        EscreveLog("BtnSalvar_Click INICIADO.")
        Try
            EscreveLog("DEBUG FORM KEYS: " & String.Join(" | ", Request.Form.AllKeys))
            ' Parametro sempre ativo por padrao (checkbox removido da tela)
            Dim flAtivoAtual As Boolean = True
            Dim flProcessamentoManual As Boolean = ObterValorCheckBox(chkProcessamentoManual, hfChkProcessamentoManual)
            EscreveLog($"DEBUG CHECKBOX: chkAtivo sempre True, chkProcessamentoManual.Checked='{flProcessamentoManual}'")
            ' ValidaÃ§Ã£o server-side com leitura direta do formulÃ¡rio garantindo captura pÃ³s-postback.
            Dim camposObrigatorios As New Dictionary(Of Web.UI.Control, String) From {
                {ddlTipo, "Tipo"}, {txtMesEmissao, "MÃªs EmissÃ£o"}, {ddlDocumentStatusCode, "Status do Documento"},
                {txtDescription, "DescriÃ§Ã£o Geral"}, {txtJustification, "Justificativa"}, {txtConta, "Conta"},
                {txtDescricaoServico, "DescriÃ§Ã£o do ServiÃ§o"}, {ddlCategoryName, "Nome da Categoria"}, {txtItemNumber, "NÃºmero do Item"},
                {txtItemDescription, "DescriÃ§Ã£o do Item"}, {txtCNPJ_Anima, "CNPJ Anima"}, {ddlSupplierName, "Nome do Fornecedor"},
                {txtSupplierSiteName, "Site do Fornecedor (CNPJ)"}, {txtPreparerEmail, "E-mail do Preparador"}, {txtApproverEmail, "E-mail do Aprovador"},
                {txtRequesterEmail, "E-mail do Solicitante"}, {txtRequisitioningBUName, "Nome da BU Requisitante"}, {txtRequisitioningBUId, "ID da BU Requisitante"},
                {txtProcurementBUName, "Nome da BU de Compras"}, {txtDeliverToLocationCode, "CÃ³digo do Local de Entrega"}, {txtCentroDeCusto, "Centro de Custo"},
                {txtEstabelecimento, "Estabelecimento"}, {txtUnidadeNegocio, "Unidade de NegÃ³cio"}
            }

            For Each campo In camposObrigatorios
                Dim valor As String = ""
                If TypeOf campo.Key Is TextBox Then
                    Dim txtBox As TextBox = CType(campo.Key, TextBox)
                    Dim hiddenFallback As HiddenField = Nothing
                    If txtBox Is txtPreparerEmail Then hiddenFallback = hfPreparerEmail
                    If txtBox Is txtApproverEmail Then hiddenFallback = hfApproverEmail
                    If txtBox Is txtRequesterEmail Then hiddenFallback = hfRequesterEmail
                    valor = ObterValorDoFormulario(txtBox, hiddenFallback)
                ElseIf TypeOf campo.Key Is DropDownList Then
                    valor = CType(campo.Key, DropDownList).SelectedValue
                End If

                If String.IsNullOrWhiteSpace(valor) Then
                    EscreveLog($"FALHA NA VALIDACAO DO SERVIDOR: Campo '{campo.Value}' esta vazio.")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('O campo ""{campo.Value}"" e obrigatorio.');", True)
                    Return
                End If
            Next
            EscreveLog("Validacao do servidor (VB) passou com sucesso.")

            ' Captura os valores dos campos de e-mail usando fallback para garantir leitura correta
            Dim preparerEmail As String = ObterValorDoFormulario(txtPreparerEmail, hfPreparerEmail)
            Dim approverEmail As String = ObterValorDoFormulario(txtApproverEmail, hfApproverEmail)
            Dim requesterEmail As String = ObterValorDoFormulario(txtRequesterEmail, hfRequesterEmail)

            Dim wsCadastro As New WS_GUA_Cadastro.WSCadastro()
            wsCadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim id As Integer = If(String.IsNullOrEmpty(hfIdParametro.Value), 0, CInt(hfIdParametro.Value))
            Dim acao As String = If(id > 0, "UPDATE", "INSERT")
            EscreveLog($"Preparando para chamar o Web Service. AÃ§Ã£o: {acao}, ID: {id}")

            Try
                wsCadastro.Parametros_Anima(
                    pPConn_Banco:=Session("Conn_Banco").ToString(),
                    pAcao:=acao,
                    pId_Parametro:=id,
                    pCodigo_Referencia:=hfCodigoReferencia.Value,
                    pTipo:=ddlTipo.SelectedValue,
                    pCNPJ_Anima:=RemoverMascara(txtCNPJ_Anima.Text),
                    pConta:=txtConta.Text,
                    pDescricaoServico:=txtDescricaoServico.Text,
                    pMesEmissao:=txtMesEmissao.Text,
                    pRequisitioningBUId:=txtRequisitioningBUId.Text,
                    pRequisitioningBUName:=txtRequisitioningBUName.Text,
                    pDescription:=txtDescription.Text,
                    pJustification:=txtJustification.Text,
                    pPreparerEmail:=preparerEmail, ' Valor obtido do formulÃ¡rio (com fallback)
                    pApproverEmail:=approverEmail, ' Valor obtido do formulÃ¡rio (com fallback)
                    pDocumentStatusCode:=ddlDocumentStatusCode.SelectedValue,
                    pRequisitionType:=txtRequisitionType.Text,
                    pSourceUniqueId:=txtSourceUniqueId.Text,
                    pCategoryName:=ddlCategoryName.SelectedValue,
                    pDeliverToLocationCode:=txtDeliverToLocationCode.Text,
                    pDeliverToOrganizationCode:=txtDeliverToOrganizationCode.Text,
                    pProcurementBUName:=txtProcurementBUName.Text,
                    pItemDescription:=txtItemDescription.Text,
                    pItemNumber:=txtItemNumber.Text,
                    pRequesterEmail:=requesterEmail, ' Valor obtido do formulÃ¡rio (com fallback)
                    pSupplierName:=ddlSupplierName.SelectedValue,
                    pSupplierContactName:=txtSupplierContactName.Text,
                    pSupplierSiteName:=RemoverMascara(txtSupplierSiteName.Text),
                    pCentroDeCusto:=txtCentroDeCusto.Text,
                    pEstabelecimento:=txtEstabelecimento.Text,
                    pUnidadeNegocio:=txtUnidadeNegocio.Text,
                    pFinalidade:=txtFinalidade.Text,
                    pProjeto:=txtProjeto.Text,
                    pInterCompany:=txtInterCompany.Text,
                    pCodigoRequisicaoCompra:=txtCodigoRequisicaoCompra.Text,
                    pCodigoOrdemCompra:=txtCodigoOrdemCompra.Text,
                    pCodigoInvoice:=txtCodigoInvoice.Text,
                    pObservacao:=txtObservacao.Text,
                    pUsuario:=Session("Nm_Usuario").ToString(),
                    pFl_Ativo:=flAtivoAtual,
                    pProcessamento_Manual:=flProcessamentoManual
                )
                EscreveLog("Web Service chamado com SUCESSO.")
            Catch wsEx As Exception
                EscreveLog("!!!!! ERRO AO CHAMAR O WEB SERVICE !!!!!")
                EscreveLog("Erro: " & wsEx.Message)
                If wsEx.InnerException IsNot Nothing Then EscreveLog("Inner Exception: " & wsEx.InnerException.Message)
                EscreveLog("Stack Trace: " & wsEx.StackTrace)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('Ocorreu um erro GRAVE no servidor ao tentar salvar: {wsEx.Message.Replace("'", "\'")}');", True)
                Return
            End Try

            ' Exibir mensagem de sucesso e permanecer na mesma pagina
            EscreveLog("Registro salvo com sucesso. Permanecendo na pagina.")
            If acao = "INSERT" Then
                ' Para novos registros, redirecionar para a pagina de consulta
                EscreveLog("Novo registro inserido. Redirecionando para consulta.")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('Registro salvo com sucesso!'); window.location.href='Consulta.aspx';", True)
            Else
                ' Para atualizacoes, mostrar mensagem e recarregar os dados
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('Registro atualizado com sucesso!');", True)
                ' Recarregar o historico apos salvar
                If Not String.IsNullOrEmpty(hfCodigoReferencia.Value) Then
                    CarregarHistorico(hfCodigoReferencia.Value)
                End If
            End If
            EscreveLog("Mensagem de sucesso exibida. Fim do metodo.")
        Catch ex As Exception
            EscreveLog("!!!!! ERRO INESPERADO E CATASTROFICO EM BtnSalvar_Click !!!!!")
            EscreveLog("Erro: " & ex.Message)
            EscreveLog("Stack Trace: " & ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('Ocorreu um erro INESPERADO no servidor: {ex.Message.Replace("'", "\'")}');", True)
        End Try
    End Sub

    Protected Sub BtnApagar_Click(sender As Object, e As EventArgs)
        Try
            Dim idParametro As Integer
            If Not Integer.TryParse(hfIdParametro.Value, idParametro) OrElse idParametro <= 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Registro nao encontrado para exclusao.');", True)
                Return
            End If

            Dim codigoReferencia As String = hfCodigoReferencia.Value
            If String.IsNullOrWhiteSpace(codigoReferencia) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Codigo de referencia ausente. Nao foi possivel apagar o parametro.');", True)
                Return
            End If

            If Session("Conn_Banco") Is Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Sessao expirada. Faca login novamente antes de apagar o parametro.');", True)
                Return
            End If

            Dim usuarioExclusao As String = String.Empty
            If Session("Nm_Usuario") IsNot Nothing Then
                usuarioExclusao = Session("Nm_Usuario").ToString()
            End If

            EscreveLog("Solicitando exclusao permanente do parametro. ID=" & idParametro & ", CodigoReferencia=" & codigoReferencia & ".")
            Dim wsCadastro As New WS_GUA_Cadastro.WSCadastro()
            wsCadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            wsCadastro.Parametros_Anima(
                pPConn_Banco:=Session("Conn_Banco").ToString(),
                pAcao:="PURGE",
                pId_Parametro:=idParametro,
                pCodigo_Referencia:=codigoReferencia,
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
                pUsuario:=usuarioExclusao,
                pFl_Ativo:=False,
                pProcessamento_Manual:=False
            )

            EscreveLog("Exclusao permanente concluida com sucesso.")
            Session("MensagemSucesso") = "Parametro removido com sucesso."
            Response.Redirect("~/Cadastro/Integracoes/Parametros/Consulta.aspx", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        Catch ex As Exception
            EscreveLog("Erro ao realizar exclusao permanente.")
            EscreveLog("Erro: " & ex.Message)
            If ex.InnerException IsNot Nothing Then EscreveLog("Inner: " & ex.InnerException.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", $"alert('Nao foi possivel apagar o parametro: {ex.Message.Replace("'", "\'")}');", True)
        End Try
    End Sub

    Private Function ObterValorCheckBox(chk As CheckBox, hidden As HiddenField) As Boolean
        ' Em um postback, se o checkbox esta marcado, ele envia "on" no form
        ' Se o checkbox esta desmarcado, ele NAO envia nada (postedValue sera Nothing)
        ' Portanto, a ausencia do valor significa que foi desmarcado

        Dim postedValue As String = Request.Form(chk.UniqueID)

        ' Se temos um valor postado, o checkbox esta marcado
        If Not String.IsNullOrEmpty(postedValue) Then
            Dim normalized As String = postedValue.ToLowerInvariant()
            chk.Checked = normalized = "on" OrElse normalized = "true" OrElse normalized = "1"
        Else
            ' Checkbox desmarcado - nao foi enviado no form
            chk.Checked = False
        End If

        ' Atualiza o hidden field para manter sincronizado
        If hidden IsNot Nothing Then
            hidden.Value = chk.Checked.ToString().ToLowerInvariant()
        End If

        Return chk.Checked
    End Function

    Private Function ObterValorDoFormulario(textBox As TextBox, Optional hiddenFallback As HiddenField = Nothing) As String
        Dim formKey As String = textBox.UniqueID
        Dim valorRequest As String = Request.Form(formKey)
        Dim textoAtualAntes As String = textBox.Text
        Dim hiddenValor As String = If(hiddenFallback IsNot Nothing, hiddenFallback.Value, Nothing)

        EscreveLog($"DEBUG FORM CAPTURE: chave='{formKey}', Request='{If(valorRequest, "<null>")}', TextAntes='{textoAtualAntes}', HiddenAntes='{If(hiddenValor, "<null>")}'")

        If String.IsNullOrWhiteSpace(valorRequest) Then
            Dim possuiChave As Boolean = Array.IndexOf(Request.Form.AllKeys, formKey) >= 0
            EscreveLog($"DEBUG POSTBACK: Request.Form vazio para '{formKey}'. Chave existe: {possuiChave}. Text atual: '{textoAtualAntes}'. Hidden atual: '{If(hiddenValor, "<null>")}'.")
            If Not String.IsNullOrWhiteSpace(textoAtualAntes) Then
                valorRequest = textoAtualAntes
                EscreveLog($"DEBUG FALLBACK: Usando valor do controle para '{formKey}'.")
            ElseIf hiddenFallback IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(hiddenFallback.Value) Then
                valorRequest = hiddenFallback.Value
                EscreveLog($"DEBUG FALLBACK: Usando hidden '{hiddenFallback.ID}' para '{formKey}'.")
            End If
        End If

        If valorRequest Is Nothing Then
            valorRequest = String.Empty
        End If

        textBox.Text = valorRequest
        If hiddenFallback IsNot Nothing Then
            hiddenFallback.Value = valorRequest
        End If
        Return valorRequest
    End Function

    Private NotInheritable Class CampoHistoricoInfo
        Public Sub New(campo As String, rotulo As String)
            NomeCampo = campo
            Rotulo = rotulo
        End Sub

        Public ReadOnly Property NomeCampo As String
        Public ReadOnly Property Rotulo As String
    End Class

    Private Class HistoricoVersaoTemp
        Public Property Acao As String
        Public Property Usuario As String
        Public Property Data As DateTime
        Public Property Estado As Dictionary(Of String, String)
    End Class

    Private Class HistoricoCampo
        Public Property Nome As String
        Public Property Valor As String
        Public Property Alterado As Boolean

        Public ReadOnly Property CssClass As String
            Get
                Return If(Alterado, "campo-alterado", String.Empty)
            End Get
        End Property
    End Class

    Private Class HistoricoVersao
        Public Property NumeroVersao As Integer
        Public Property Acao As String
        Public Property Usuario As String
        Public Property Data As DateTime
        Public Property Campos As List(Of HistoricoCampo)

        Public ReadOnly Property NumeroVersaoFormatado As String
            Get
                Return $"Versao {NumeroVersao}"
            End Get
        End Property

        Public ReadOnly Property AcaoDescricao As String
            Get
                If String.IsNullOrWhiteSpace(Acao) Then Return "Acao desconhecida"
                Select Case Acao.Trim().ToUpperInvariant()
                    Case "INSERT"
                        Return "Criacao"
                    Case "UPDATE"
                        Return "Atualizacao"
                    Case "DELETE"
                        Return "Desativacao"
                    Case Else
                        Return Acao
                End Select
            End Get
        End Property

        Public ReadOnly Property DataFormatada As String
            Get
                If Data = DateTime.MinValue Then Return String.Empty
                Return Data.ToString("dd/MM/yyyy HH:mm")
            End Get
        End Property
    End Class
End Class








