
Public Class Auditoria_Importa
    Inherits System.Web.UI.Page

    Dim WS_Auditoria As New WS_GUA_Auditoria.WSAuditoria
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta

    Dim oConfig As New cls_Config

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Importação de Auditoria ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboConglomerado)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))
            '-----gera data lote
            Dim vDataSet As New Data.DataSet
            vDataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            oConfig.CarregaCombo(cboDataLote, vDataSet)

            Session("DataSet") = Nothing
        End If
    End Sub

    Protected Sub btExecutar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExecutar.Click
        WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        If cboConglomerado.SelectedValue = Nothing Then Exit Sub
        If cboDataLote.SelectedValue = Nothing Then Exit Sub

        Session("DataSet") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                        Nothing,
                                                        Nothing,
                                                        cboDataLote.SelectedValue,
                                                        cboConglomerado.SelectedValue,
                                                        Nothing,
                                                        "sp_Imp_Auditoria_Bilhete",
                                                        True)

        dtgAuditoria.DataSource = Session("DataSet")
        dtgAuditoria.DataBind()

        '-----registro salvo ok
        lblDescricaoArquivo.Text = "Arquivo | Conglomerado: " & cboConglomerado.SelectedItem.Text & " - Lote: " & cboDataLote.SelectedItem.Text
    End Sub

    Protected Sub dtgConsultaConta_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAuditoria.PageIndexChanged
        dtgAuditoria.CurrentPageIndex = e.NewPageIndex
        dtgAuditoria.DataSource = Session("DataSet")
        dtgAuditoria.DataBind()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class

