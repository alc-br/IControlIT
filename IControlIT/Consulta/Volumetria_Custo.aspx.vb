
Public Class Volumetria_Custo
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
                "Volumetria - Custo", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboGrupoAtivo)
            Call Master.Localizar(Nothing, Nothing)

            oConfig.CarregaList(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))

            Dim vdataSet As New System.Data.DataSet
            vdataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            oConfig.CarregaCombo(cboDataDe, vdataSet)
            oConfig.CarregaCombo(cboDataAte, vdataSet)

        End If
    End Sub

    Protected Sub btExecutar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExecutar.Click
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        dtgDataGrid.CurrentPageIndex = Nothing

        WS_Consulta.Timeout = 3600000

        If cboGrupoAtivo.SelectedValue = Nothing Then Exit Sub
        If cboConglomerado.SelectedValue = Nothing Then Exit Sub

        Session("DataSet") = WS_Consulta.Volumetria_Custo(Session("Conn_Banco"),
                                                                oConfig.ValidaCampo(cboGrupoAtivo.SelectedValue),
                                                                oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                                                oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                                oConfig.ValidaCampo(cboDataAte.SelectedValue),
                                                                "sp_Volumetria")
        dtgDatagrid.DataSource = Session("DataSet")
        dtgDatagrid.DataBind()


        Dim vdataSet As New System.Data.DataSet
        vdataSet = Session("DataSet")

        Dim vSoma As System.Decimal
        Dim X(vdataSet.Tables(0).Columns.Count - 15) As System.String
        Dim Y(vdataSet.Tables(0).Columns.Count - 15) As System.Decimal

        '-----eixo X
        For i As Int16 = 0 To vdataSet.Tables(0).Columns.Count - 15
            X(i) = vdataSet.Tables(0).Columns.Item(i + 14).ToString
        Next

        '-----eixo Y
        For i As Int16 = 0 To vdataSet.Tables(0).Columns.Count - 15
            For ii As Int16 = 0 To vdataSet.Tables(0).Rows.Count - 1
                vSoma = vSoma + IIf(IsDBNull(vdataSet.Tables(0).Rows(ii).Item(i + 14)), 0, vdataSet.Tables(0).Rows(ii).Item(i + 14))
            Next
            Y(i) = String.Format("{0:N}", vSoma)
            vSoma = 0
        Next

        lblDescricaoArquivo.Text = ""
        pnlAbir.Visible = False
        lblDescricaoArquivo.Text = "Arquivo | Grupo de Ativo: " & cboGrupoAtivo.SelectedValue & " | Conglomerado: " & cboConglomerado.SelectedValue & " | Lote (De): " & cboDataDe.SelectedValue & " | Lote (Ate): " & cboDataAte.SelectedValue
    End Sub

    Protected Sub dtgDataGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDataGrid.PageIndexChanged
        dtgDataGrid.CurrentPageIndex = e.NewPageIndex
        dtgDatagrid.DataSource = Session("DataSet")
        dtgDataGrid.DataBind()
    End Sub

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        pnlAbir.Visible = False
        lblDescricaoArquivo.Text = ""
    End Sub

    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String = Nothing
        '-----nome do arquivo a ser exportado
        Dim Descricao As System.String = "Volumetria_Custo"
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
