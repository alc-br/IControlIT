
Public Class Lote
    Inherits System.Web.UI.Page
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim oConfig As New cls_Config
    Private vNumeroAtivo As System.Int32

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page

            Call Master.Titulo( _
                  IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                  "Contas Antigas", _
                  vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            Page.SetFocus(cboDataDe)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            '-----home
            If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then
                Call Master.home("usuario")
                '-----voltar
                Call Master.Voltar("", "~/CockPit_Menu.aspx")
            Else
                '-----voltar
                Call Master.Voltar("", "~/Home.aspx")
            End If
            Session("DataSet") = Nothing

            If Not Request("Postback") = 1 Then Session("DataSetPostBack") = Nothing

            If Request("Postback") = 1 And Not Session("DataSetPostBack") Is Nothing Then
                dtgLote.DataSource = Session("DataSetPostBack")
                dtgLote.DataBind()
            End If

            Dim vdataSet As New System.Data.DataSet
            vdataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            oConfig.CarregaCombo(cboDataDe, vdataSet)
            oConfig.CarregaCombo(cboDataAte, vdataSet)

            cboDataDe.SelectedIndex = cboDataDe.Items.Count - 1
            cboDataAte.SelectedIndex = cboDataAte.Items.Count - 1

        End If
    End Sub

    Protected Sub btExecutar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExecutar.Click
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        dtgLote.CurrentPageIndex = Nothing

        If Filtro_Acesso.pPakage = Nothing Then Exit Sub
        Session("DataSetPostBack") = WS_Consulta.Lote(Session("Conn_Banco"),
                                                            "sp_Lote",
                                                            Filtro_Acesso.pPakage,
                                                            Filtro_Acesso.pParametro1,
                                                            Filtro_Acesso.pParametro_Filial,
                                                            Filtro_Acesso.pParametro_Usuario,
                                                            Filtro_Acesso.pParametro_Centro_Custo,
                                                            Filtro_Acesso.pParametro_Departamento,
                                                            Filtro_Acesso.pParametro_Setor,
                                                            oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                            oConfig.ValidaCampo(cboDataAte.SelectedValue),
                                                            Session("KPI"),
                                                            Session("Id_Usuario"))
        dtgLote.DataSource = Session("DataSetPostBack")
        dtgLote.DataBind()

        If Session("KPI") = "Telefonia_Movel" Then lblDescricao.Text = "Contas (Móvel)"
        If Session("KPI") = "Telefonia_Fixa" Then lblDescricao.Text = "Contas (Fixa)"
        If Session("KPI") = "Link_Dados" Then lblDescricao.Text = "Contas (Link de Dados)"
        If Session("KPI") = "Desktop" Then lblDescricao.Text = "Contas (Desktop)"
        If Session("KPI") = "Impressora" Then lblDescricao.Text = "Contas (Impressora)"
        If Session("KPI") = "Frota" Then lblDescricao.Text = "Contas (Frota)"
        If Session("KPI") = "Plano_Saude" Then lblDescricao.Text = "Contas (Plano de Saúde)"
        If Session("KPI") = Nothing Then lblDescricao.Text = "Contas"
    End Sub

    Protected Sub dtgLote_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgLote.PageIndexChanged
        dtgLote.CurrentPageIndex = e.NewPageIndex
        dtgLote.DataSource = Session("DataSetPostBack")
        dtgLote.DataBind()
    End Sub

    Protected Sub cboDataDe_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDataDe.SelectedIndexChanged
        btExecutar.Enabled = True
    End Sub

    Protected Sub cboDataAte_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDataAte.SelectedIndexChanged
        btExecutar.Enabled = True
    End Sub

    Protected Sub btFecharMsg_Click(sender As Object, e As EventArgs) Handles btFecharMsg.Click
        pnlMenssagem.Visible = False
    End Sub

    Protected Sub btView_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_bt As ImageButton = sender
        Dim vText As System.String = v_bt.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btView_") + 8, 8), System.Int32)

        Response.Redirect(dtgLote.Items(i).Cells(7).Text)
    End Sub
    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        '-----monta exportacao
        Session("DataSet") = Session("DataSetPostBack")
        Dim Descricao As System.String = "Lote"
        Dim Campo As System.String = "Nm_Ativo_Tipo;Nr_Ativo;Nm_Consumidor;Dt_Lote;Total;Marcado;Dt_Fechamento;Dt_Visita;Dt_Exportacao"

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
                                            "window.open('../Exportacao/Exporta.aspx?Descricao=" & Descricao & "&Campo=" & Campo &
                                            "','','resizable=yes, menubar=yes, scrollbars=no," &
                                            "height=768px, width=1024px, top=10, left=10'" &
                                            ")", True)
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
