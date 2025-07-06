
Public Class Auditoria_Acompanhamento
    Inherits System.Web.UI.Page

    Dim WS_Auditoria As New WS_GUA_Auditoria.WSAuditoria
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Status da Contestação ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboLote)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Auditoria_Acompanhamento", Page.AppRelativeVirtualPath.ToString)

            Page.SetFocus(cboLote)
            oConfig.CarregaCombo(cboLote, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Auditoria_Lote", Nothing))
            oConfig.CarregaCombo(cboStatus, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Auditoria_Status", Nothing))

            If Not Request("ID") = Nothing Then
                vdataset = WS_Auditoria.Auditoria_Acompanhamento(Session("Conn_Banco"),
                                                                        Request("ID"),
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        "sp_SL_ID",
                                                                        True)

                If Not vdataset.Tables(0).Rows.Count = 0 Then
                    txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Auditoria_Acompanhamento")
                    cboLote.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Auditoria_Lote")
                    cboStatus.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Auditoria_Status")
                    txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Descricao")
                    txtDataPrevista.Text = vdataset.Tables(0).Rows(0).Item("Data_Prevista")
                    txtValorPrevisto.Text = vdataset.Tables(0).Rows(0).Item("Valor_Previsto")

                    oConfig.CarregaCombo(cboConta, WS_Auditoria.Auditoria_Acompanhamento(Session("Conn_Banco"), Nothing, cboLote.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Drop_Auditoria_Detalhe", True))
                    cboConta.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Auditoria_Conta")
                End If
            End If
        End If
    End Sub

    Protected Sub cboLote_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLote.SelectedIndexChanged
        If cboLote.SelectedValue = Nothing Then Exit Sub

        oConfig.CarregaCombo(cboConta, WS_Auditoria.Auditoria_Acompanhamento(Session("Conn_Banco"), Nothing, cboLote.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Drop_Auditoria_Detalhe", True))
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboLote.SelectedValue = Nothing
        cboConta.SelectedValue = Nothing
        cboStatus.SelectedValue = Nothing
    End Sub
    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        If txtValorPrevisto.Text = 0 Then Exit Sub
        WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
        vdataset = WS_Auditoria.Auditoria_Acompanhamento(Session("Conn_Banco"),
                                                                oConfig.ValidaCampo(txtIdentificacao.Text),
                                                                oConfig.ValidaCampo(cboLote.SelectedValue),
                                                                oConfig.ValidaCampo(cboConta.SelectedValue),
                                                                oConfig.ValidaCampo(cboStatus.SelectedValue),
                                                                oConfig.ValidaCampo(txtDescricao.Text),
                                                                oConfig.ValidaCampo(txtDataPrevista.Text),
                                                                oConfig.ValidaCampo(txtValorPrevisto.Text),
                                                                Session("Id_Usuario"),
                                                                "sp_SM",
                                                                True)

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
        vdataset = WS_Auditoria.Auditoria_Acompanhamento(Session("Conn_Banco"),
                                                                txtIdentificacao.Text,
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                Session("Id_Usuario"),
                                                                "sp_SE",
                                                                True)
        Call limpar()
    End Sub
    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
