
Public Class Tipo_Solicitcao
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                "Cadastro de Tipo de Solicitação ", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtDescricao)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Solicitacao_Tipo", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboAtivoTipo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo", Nothing))
            oConfig.CarregaCombo(cboSLAAtendimeto, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Solicitacao_SLA", Nothing))
            oConfig.CarregaCombo(cboFilaAtendimento, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Solicitacao_Fila_Atendimento", Nothing))
            oConfig.CarregaCombo(cboGrupoSolicitacao, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Solicitacao_Permissao", Nothing))
            oConfig.CarregaCombo(cboSolicitacaoMatricula, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Solicitacao_Processo_Unidade", Nothing))

            If Not Request("ID") = Nothing Then
                'TODO: conferir os parâmetros da function do WS
                'vdataset = WS_Cadastro.Solicitacao_Tipo(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)
                vdataset = WS_Cadastro.Solicitacao_Tipo(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)

                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Tipo")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Nm_Solicitacao_Tipo")
                cboAtivoTipo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo")
                cboSLAAtendimeto.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_SLA")
                cboFilaAtendimento.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Solicitcao_Fila_Atendimento")
                cboGrupoSolicitacao.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Permissao")
                cboSolicitacaoMatricula.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Unidade_Processo")
                txtCaixaTexto.Text = vdataset.Tables(0).Rows(0).Item("Fl_Config_Caixa_Texto")

            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboAtivoTipo.SelectedValue = Nothing
        cboFilaAtendimento.SelectedValue = Nothing
        cboSLAAtendimeto.SelectedValue = Nothing
        cboGrupoSolicitacao.SelectedValue = Nothing
        Page.SetFocus(txtDescricao)
        btSalvar.Enabled = True
    End Sub
    Protected Sub chkSolicitarMatricula_CheckedChanged(sender As Object, e As EventArgs)

        'If chkSolicitarMatricula.Checked = True Then
        '    tblCheck.Style.Add("Background-position", "-22px 3px")
        'Else
        '    tblCheck.Style.Add("Background-position", "0px 3px")
        'End If

    End Sub
    Protected Sub cboAtivoTipo_SelectedIndexChanged(sender As Object, e As EventArgs)

        'If cboAtivoTipo.SelectedItem.Text = "Solicitação Matricula" Then
        '    lnCheckMatricula.Visible = True
        'Else
        '    lnCheckMatricula.Visible = False
        'End If

    End Sub
    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        If IsNumeric(txtDescricao.Text) Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        'TODO: verificar parâmetros da function da WS
        'WS_Cadastro.Solicitacao_Tipo(Session("Conn_Banco"),
        '                            oConfig.ValidaCampo(txtIdentificacao.Text),
        '                            oConfig.ValidaCampo(txtDescricao.Text),
        '                            cboAtivoTipo.SelectedValue,
        '                            cboSLAAtendimeto.SelectedValue,
        '                            cboFilaAtendimento.SelectedValue,
        '                            cboGrupoSolicitacao.SelectedValue,
        '                            oConfig.ValidaCampo(txtCaixaTexto.Text),
        '                            cboSolicitacaoMatricula.SelectedValue,
        '                            Session("Id_Usuario"),
        '                            "sp_SM",
        '                            False)

        WS_Cadastro.Solicitacao_Tipo(Session("Conn_Banco"),
                                    oConfig.ValidaCampo(txtIdentificacao.Text),
                                    oConfig.ValidaCampo(txtDescricao.Text),
                                    cboAtivoTipo.SelectedValue,
                                    cboSLAAtendimeto.SelectedValue,
                                    cboFilaAtendimento.SelectedValue,
                                    cboGrupoSolicitacao.SelectedValue,
                                    oConfig.ValidaCampo(txtCaixaTexto.Text),
                                    Session("Id_Usuario"),
                                    "sp_SM",
                                    False)

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub
    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        'TODO: verificar parâmetros da function da WS
        'WS_Cadastro.Solicitacao_Tipo(Session("Conn_Banco"),
        '                            txtIdentificacao.Text,
        '                            Nothing,
        '                            Nothing,
        '                            Nothing,
        '                            Nothing,
        '                            Nothing,
        '                            Nothing,
        '                            Nothing,
        '                            Session("Id_Usuario"),
        '                            "sp_SE",
        '                            False)

        WS_Cadastro.Solicitacao_Tipo(Session("Conn_Banco"),
                                    txtIdentificacao.Text,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Session("Id_Usuario"),
                                    "sp_SE",
                                    False)
        Call limpar()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
