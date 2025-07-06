
Public Class Auditoria
    Inherits System.Web.UI.Page

    Dim WS_Auditoria As New WS_GUA_Auditoria.WSAuditoria
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Session("DataSet") = Nothing

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Status de Contestação ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(lstConglomerado)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            oConfig.CarregaList(lstConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))

            Call Executar(Request("lstConglomerado"))
        End If
    End Sub

    Protected Sub dtgLote_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgLote.PageIndexChanged
        dtgLote.CurrentPageIndex = e.NewPageIndex
        dtgLote.DataSource = Session("DataSet_Lote")
        dtgLote.DataBind()
    End Sub

    Protected Sub dtgAuditoriaTexto_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAuditoriaTexto.PageIndexChanged
        dtgAuditoriaTexto.CurrentPageIndex = e.NewPageIndex
        dtgAuditoriaTexto.DataSource = Session("DataSet_Conta")
        dtgAuditoriaTexto.DataBind()
    End Sub

    Protected Sub dtgAcompanhamento_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAcompanhamento.PageIndexChanged
        dtgAcompanhamento.CurrentPageIndex = e.NewPageIndex
        dtgAcompanhamento.DataSource = Session("DataSet_Acompanhamento")
        dtgAcompanhamento.DataBind()
    End Sub

    Protected Sub btDesativa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btDesativa_") + 12, 8), System.Int32)

        WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
        vdataset = WS_Auditoria.Auditoria_Acompanhamento(Session("Conn_Banco"),
                                                                dtgAcompanhamento.Items(i).Cells(0).Text,
                                                                Nothing,
                                                                Nothing,
                                                                 Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                Session("Id_Usuario"),
                                                                "sp_SE",
                                                                True)

        '-----lista lote
        If lstConglomerado.SelectedValue = Nothing Then Exit Sub
        Session("DataSet_Lote") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                lstConglomerado.SelectedValue,
                                                                Nothing,
                                                                "sp_Auditoria_Lote",
                                                                True)
        dtgLote.DataSource = Session("DataSet_Lote")
        dtgLote.DataBind()

        '-----lista conta
        Session("DataSet_Conta") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                                hfdId_Auditoria_Lote_Select.Value,
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                "sp_Auditoria_Texto",
                                                                True)
        dtgAuditoriaTexto.DataSource = Session("DataSet_Conta")
        dtgAuditoriaTexto.DataBind()

        '-----lista acompanhamento
        Session("DataSet_Acompanhamento") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                                        hfdId_Auditoria_Lote_Select.Value,
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        "sp_Auditoria_Acompanhamento",
                                                                        True)
        dtgAcompanhamento.DataSource = Session("DataSet_Acompanhamento")
        dtgAcompanhamento.DataBind()

        btAdicionar.Enabled = False
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As System.EventArgs) Handles btExecutar.Click
        Executar(Nothing)
    End Sub

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        pnlAbir.Visible = False
        lstConglomerado.SelectedValue = Nothing
    End Sub

    Protected Sub btFechar_Status_Click(sender As Object, e As EventArgs) Handles btFechar_Status.Click
        '-----limpa registro
        txtDataPrevista.Text = ""
        txtDescricao.Text = ""
        txtValorPrevisto.Text = ""
        pnlmsg.Visible = False
    End Sub

    Protected Sub btACFechar_Click(sender As Object, e As EventArgs) Handles btACFechar.Click
        pnlAcompanhamento.Visible = False
    End Sub

    Protected Sub btTexto_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        If hfdId_Auditoria_Status.Value = "" Then Exit Sub
        pnlAcompanhamento.Visible = True
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs) Handles btSalvar.Click
        If txtValorPrevisto.Text = 0 Then Exit Sub
        WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
        vdataset = WS_Auditoria.Auditoria_Acompanhamento(Session("Conn_Banco"),
                                                                Nothing,
                                                                oConfig.ValidaCampo(hfdId_Auditoria_Lote.Value),
                                                                Nothing,
                                                                oConfig.ValidaCampo(hfdId_Auditoria_Status.Value),
                                                                oConfig.ValidaCampo(txtDescricao.Text),
                                                                oConfig.ValidaCampo(txtDataPrevista.Text),
                                                                oConfig.ValidaCampo(txtValorPrevisto.Text),
                                                                Session("Id_Usuario"),
                                                                "sp_SM",
                                                                True)

        '-----limpa registro
        txtDataPrevista.Text = ""
        txtDescricao.Text = ""
        txtValorPrevisto.Text = ""
        pnlmsg.Visible = False

        '-----lista lote
        If lstConglomerado.SelectedValue = Nothing Then Exit Sub
        Session("DataSet_Lote") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                lstConglomerado.SelectedValue,
                                                                Nothing,
                                                                "sp_Auditoria_Lote",
                                                                True)
        dtgLote.DataSource = Session("DataSet_Lote")
        dtgLote.DataBind()

        '-----lista acompanhamento
        Session("DataSet_Acompanhamento") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                                        hfdId_Auditoria_Lote_Select.Value,
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        "sp_Auditoria_Acompanhamento",
                                                                        True)
        dtgAcompanhamento.DataSource = Session("DataSet_Acompanhamento")
        dtgAcompanhamento.DataBind()

        btAdicionar.Enabled = False
    End Sub

    Protected Sub btAcompanhamento_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_bt As ImageButton = sender
        Dim vText As System.String = v_bt.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btAcompanhamento_") + 18, 8), System.Int32)

        WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
        '-----lista conta
        Session("DataSet_Conta") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                                dtgLote.Items(i).Cells(0).Text,
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                "sp_Auditoria_Texto",
                                                                True)
        dtgAuditoriaTexto.DataSource = Session("DataSet_Conta")
        dtgAuditoriaTexto.DataBind()

        hfdId_Auditoria_Lote_Select.Value = dtgLote.Items(i).Cells(0).Text

        '-----libera gravacao do pdf por lote
        btPDF.OnClientClick = "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & dtgLote.Items(i).Cells(0).Text & "&pTabela=Auditoria_Lote','','resizable=yes, menubar=yes, scrollbars=no,height=768px, width=1024px, top=10, left=10')"
        btPDF.Enabled = True

        '-----lista acompanhamento por lote
        Session("DataSet_Acompanhamento") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                                    dtgLote.Items(i).Cells(0).Text,
                                                                    Nothing,
                                                                    Nothing,
                                                                    Nothing,
                                                                    Nothing,
                                                                    "sp_Auditoria_Acompanhamento",
                                                                    True)

        dtgAcompanhamento.DataSource = Session("DataSet_Acompanhamento")
        dtgAcompanhamento.DataBind()

        lblConta.Visible = True
        lblAcompanhamento.Visible = True

        '-----armazena dados para gravar status
        hfdId_Auditoria_Lote.Value = dtgLote.Items(i).Cells(0).Text
        hfdId_Auditoria_Conta.Value = dtgLote.Items(i).Cells(2).Text
        hfdId_Auditoria_Status.Value = dtgLote.Items(i).Cells(1).Text
        hfdDt_Lote.Value = dtgLote.Items(i).Cells(5).Text

        lblLote.Text = "Auditoria por Mês - " & dtgLote.Items(i).Cells(4).Text
        btAdicionar.Enabled = True

        '-----muda cor da linha
        For Linha = 0 To dtgLote.Items.Count - 1
            For coluna = 0 To dtgLote.Items(Linha).Cells.Count - 1
                dtgLote.Items(Linha).Cells(coluna).BackColor = Nothing
            Next coluna
        Next

        For coluna = 0 To dtgLote.Items(i).Cells.Count - 1
            dtgLote.Items(i).Cells(coluna).BackColor = Drawing.ColorTranslator.FromHtml("#C0C0C0")
        Next coluna
    End Sub

    Protected Sub btACSalvar_Click(sender As Object, e As EventArgs) Handles btACSalvar.Click
        WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
        '-----salva texto de acompanhamento
        vdataset = WS_Auditoria.Auditoria_Acompanhamento(Session("Conn_Banco"),
                                                                Nothing,
                                                                oConfig.ValidaCampo(hfdId_Auditoria_Lote.Value),
                                                                Nothing,
                                                                Nothing,
                                                                oConfig.ValidaCampo(txtACDescricao.Text),
                                                                oConfig.ValidaCampo(txtACDataResposta.Text),
                                                                Nothing,
                                                                Session("Id_Usuario"),
                                                                "sp_Grava_Texto",
                                                                True)
        '-----lista texto de acompanhamento
        Session("DataSet_Conta") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                        hfdId_Auditoria_Lote_Select.Value,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        "sp_Auditoria_Texto",
                                                        True)
        dtgAuditoriaTexto.DataSource = Session("DataSet_Conta")
        dtgAuditoriaTexto.DataBind()

        pnlAcompanhamento.Visible = False
    End Sub

    Public Sub Executar(plista As String)
        WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
        '-----lista lote
        If lstConglomerado.SelectedValue = Nothing And plista = Nothing Then Exit Sub
        Session("DataSet_Lote") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                            Nothing,
                                                            Nothing,
                                                            Nothing,
                                                            IIf(plista = Nothing, lstConglomerado.SelectedValue, plista),
                                                            Nothing,
                                                            "sp_Auditoria_Lote",
                                                            True)
        dtgLote.DataSource = Session("DataSet_Lote")
        dtgLote.DataBind()

        pnlAbir.Visible = False
        lblLote.Visible = True
    End Sub

    Protected Sub btAdicionar_Click(sender As Object, e As EventArgs)
        lblContestar.Text = hfdId_Auditoria_Conta.Value & " - por Lote"
        If hfdId_Auditoria_Status.Value = "" Then Exit Sub
        pnlmsg.Visible = True
    End Sub

    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        If hfdId_Auditoria_Lote_Select.Value = Nothing Then Exit Sub

        WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
        Session("DataSet") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                    Nothing,
                                                    Nothing,
                                                    hfdDt_Lote.Value.Replace("/", ""),
                                                    lstConglomerado.SelectedValue,
                                                    Nothing,
                                                    "sp_Arquivo_Constestacao",
                                                    True)

        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String = Nothing
        '-----nome do arquivo a ser exportado
        Dim Descricao As System.String = "Contestacao"
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
        pnlAbir.Visible = True
        lstConglomerado.SelectedValue = Nothing

        lblLote.Visible = False
        dtgLote.DataSource = Nothing
        dtgLote.DataBind()

        lblConta.Visible = False
        dtgAuditoriaTexto.DataSource = Nothing
        dtgAuditoriaTexto.DataBind()

        lblAcompanhamento.Visible = False
        dtgAcompanhamento.DataSource = Nothing
        dtgAcompanhamento.DataBind()
    End Sub
    Protected Sub btnConta_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub
    Protected Sub btnAcompanhamento_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub
    Protected Sub btnStatus_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub Valida_Tab(ByVal btn As Button)

        divConta.Visible = False
        divAcompanhamento.Visible = False
        divStatus.Visible = False
        btnConta.CssClass = "btn-tab-disable pull-left"
        btnAcompanhamento.CssClass = "btn-tab-disable pull-left"
        btnStatus.CssClass = "btn-tab-disable pull-left"

        If btn.Text = "Conta" Then
            divConta.Visible = True
            btnConta.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Acompanhamento" Then
            divAcompanhamento.Visible = True
            btnAcompanhamento.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Status" Then
            divStatus.Visible = True
            btnStatus.CssClass = "btn-tab pull-left"
        End If

    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub

End Class

