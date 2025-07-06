
Public Class Fatura_Parametro_Campo
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
                "Campo da Fatura", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))
            '-----voltar

            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtDescricao)
            Page.Form.DefaultButton = btPDF.UniqueID
            Call Master.Localizar("sp_Drop_Fatura_Parametro_Campo", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboFaturaParametro, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Fatura_Parametro", Nothing))
            If Not Request("ID") = Nothing Then
                vdataset = WS_Cadastro.Fatura_Parametro_Campo(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)
                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Fatura_Parametro_Campo")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Nm_Fatura_Parametro_Campo")
                cboFaturaParametro.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Fatura_Parametro") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Fatura_Parametro"))
                txtObservacao.Text = vdataset.Tables(0).Rows(0).Item("Observacao")
                optSinal.SelectedValue = vdataset.Tables(0).Rows(0).Item("Sinal")
                txtIdentificacaoModal.Text = vdataset.Tables(0).Rows(0).Item("Id_Fatura_Parametro_Campo")
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboFaturaParametro.SelectedValue = Nothing
        optSinal.SelectedValue = 1
        btSalvar.Enabled = True
    End Sub
    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        '-----verifica se colocou observacao
        If Trim(txtObservacaoObrigatoria.Text) = "" Then Exit Sub

        '-----nao insere registro quando descricao so for numerica
        If IsNumeric(txtDescricao.Text) Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Fatura_Parametro_Campo(Session("Conn_Banco"),
                                                oConfig.ValidaCampo(txtIdentificacao.Text),
                                                oConfig.ValidaCampo(Replace(txtDescricao.Text, " ", "")),
                                                cboFaturaParametro.SelectedValue,
                                                oConfig.ValidaCampo(Mid(txtObservacaoObrigatoria.Text, 1, 1000)),
                                                optSinal.SelectedValue,
                                                Session("Id_Usuario"),
                                                "sp_SM",
                                                False)
        vdataset = WS_Cadastro.Fatura_Parametro_Campo(Session("Conn_Banco"),
                                                      Request("ID"), Nothing, Nothing, Nothing,
                                                      Nothing, Nothing, "sp_SL_ID", True)

        txtObservacao.Text = vdataset.Tables(0).Rows(0).Item("Observacao")
        txtIdentificacaoModal.Text = vdataset.Tables(0).Rows(0).Item("Id_Fatura_Parametro_Campo")

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
        '-----registro salvo ok
        pnlObservacao.Visible = False
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Fatura_Parametro_Campo(Session("Conn_Banco"),
                                                txtIdentificacao.Text,
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

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btSalvar",
                                                        False)
        pnlRegistro.Visible = False
        pnlObservacao.Visible = True
        txtObservacaoObrigatoria.Text = ""
    End Sub

    Protected Sub btFechar_Registro_Click(sender As Object, e As EventArgs) Handles btFechar_Registro.Click
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btFechar_Registro",
                                                        False)
        pnlRegistro.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
    End Sub

    Protected Sub btCancela_Click(sender As Object, e As EventArgs) Handles btCancela.Click
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btCancela",
                                                        False)
        pnlObservacao.Visible = False
    End Sub

    Protected Sub btAbrir_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btAbrir",
                                                        False)
        pnlRegistro.Visible = True
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "disableScrolling();", True)
    End Sub
End Class
