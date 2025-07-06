
Public Class Aproveitamento_Pacote
    Inherits System.Web.UI.Page
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Private vNumeroAtivo As System.Int32
    Dim vDataSet As System.Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                "Aproveitamento de Serviço", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboGrupoAtivo)
            Call Master.Localizar(Nothing, Nothing)

            Dim vdataSet As New System.Data.DataSet
            vdataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            oConfig.CarregaCombo(cboDataDe, vdataSet)

        End If
    End Sub

    Protected Sub dtgDataGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDataGrid.PageIndexChanged
        dtgDataGrid.CurrentPageIndex = e.NewPageIndex
        dtgDataGrid.DataSource = Session("DataSet")
        dtgDataGrid.DataBind()
    End Sub

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        pnlAbir.Visible = False
        lblDescricaoArquivo.Text = ""
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As EventArgs) Handles btExecutar.Click
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        dtgDataGrid.CurrentPageIndex = Nothing
        lblDescricaoArquivo.Text = ""

        If cboGrupoAtivo.SelectedValue = Nothing Then Exit Sub
        If cboDataDe.SelectedValue = Nothing Then Exit Sub

        WS_Consulta.Timeout = 3600000

        Session("DataSet") = WS_Consulta.Aproveitamento_Pacote(Session("Conn_Banco"),
                                                                oConfig.ValidaCampo(cboGrupoAtivo.SelectedValue),
                                                                oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                                "sp_Aproveitamento")
        dtgDataGrid.DataSource = Session("DataSet")
        dtgDataGrid.DataBind()

        lblDescricaoArquivo.Text = ""
        pnlAbir.Visible = False
        lblDescricaoArquivo.Text = "Arquivo | Grupo de Ativo: " & cboGrupoAtivo.SelectedValue & " | Lote: " & cboDataDe.SelectedValue
    End Sub

    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String = Nothing
        '-----nome do arquivo a ser exportado
        Dim Descricao As System.String = "Aproveitamento_Pacote"
        '-----campos a ser exportado modelo (xxxx;xxxxx;xxxx). quando null sistema gera com base no dataset
        Dim Campo As System.String = Nothing
        '-----abre pnl de exportacao
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                            "Descricao=" & Descricao &
                                            "&Campo=" & Campo &
                                            "&Tipo=" & Tipo &
                                            "','','resizable=yes, menubar=yes, scrollbars=no," &
                                            "height=768px, width=1024px, top=10, left=10'" &
                                            ")", True)
    End Sub

    Protected Sub btAbrir_Click(sender As Object, e As EventArgs)
        lblDescricaoArquivo.Text = ""
        pnlAbir.Visible = True
        cboGrupoAtivo.SelectedValue = Nothing
        cboDataDe.SelectedValue = Nothing
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
