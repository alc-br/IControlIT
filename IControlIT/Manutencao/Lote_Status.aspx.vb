
Public Class Lote_Status
    Inherits System.Web.UI.Page
    Dim WS_Manutencao As New WS_GUA_Manutencao.WSManutencao
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim oConfig As New cls_Config
    Dim vDataSet As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Liberar Status do Lote ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Call Master.Localizar(Nothing, Nothing)

            Page.SetFocus(txtUsuario)
            Page.Form.DefaultButton = BtSalvar.UniqueID

            '-----gera data lote
            oConfig.CarregaCombo(cboDataLote, WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboDataLote.SelectedValue = Nothing
        dtgConsulta.DataSource = Nothing
        dtgConsulta.DataBind()
        Page.SetFocus(txtUsuario)
    End Sub

    Protected Sub cboDataLote_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDataLote.SelectedIndexChanged
        WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

        If cboDataLote.SelectedValue = Nothing Then
            Call limpar()
            Exit Sub
        End If

        Dim vDataSet As System.Data.DataSet = WS_Manutencao.Lote(Session("Conn_Banco"),
                                                                oConfig.ValidaCampo(txtUsuario.Text),
                                                                oConfig.ValidaCampo(cboDataLote.Text),
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                "sp_SL_Lote_Marcacao",
                                                                True)

        dtgConsulta.DataSource = vDataSet
        dtgConsulta.DataBind()
    End Sub
    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim linha As System.Int32
        Dim vchkVisitado As CheckBox
        Dim vchkFechado As CheckBox
        Dim vchkExportado As CheckBox

        For linha = 0 To dtgConsulta.Items.Count - 1
            vchkVisitado = dtgConsulta.Items(linha).Cells(2).Controls(1)
            vchkFechado = dtgConsulta.Items(linha).Cells(3).Controls(1)
            vchkExportado = dtgConsulta.Items(linha).Cells(4).Controls(1)

            WS_Manutencao.Lote(Session("Conn_Banco"),
                                    Nothing,
                                    Nothing,
                                    dtgConsulta.Items(linha).Cells(0).Text,
                                    vchkVisitado.Checked,
                                    vchkFechado.Checked,
                                    vchkExportado.Checked,
                                    Session("Id_Usuario"),
                                    "sp_SM_Lote_Marcacao",
                                    False)
        Next linha

        '-----limpa tela
        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
