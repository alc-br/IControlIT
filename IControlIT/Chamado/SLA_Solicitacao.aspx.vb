
Public Class SLA_Solicitacao
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
                "Cadastro de SLA de Atendimento ", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))
            '-----voltar

            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtDescricao)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Solicitacao_SLA", Page.AppRelativeVirtualPath.ToString)

            If Not Request("ID") = Nothing Then
                vdataset = WS_Cadastro.Solicitacao_SLA(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)
                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_SLA")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Nm_Solicitacao_SLA")
                cboQTDHoras.SelectedValue = vdataset.Tables(0).Rows(0).Item("Horas")
                txtEmail.Text = vdataset.Tables(0).Rows(0).Item("Email_2_Nivel")
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        Page.SetFocus(txtDescricao)
        cboQTDHoras.SelectedValue = Nothing
        btSalvar.Enabled = True
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        If IsNumeric(txtDescricao.Text) Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Solicitacao_SLA(Session("Conn_Banco"),
                                oConfig.ValidaCampo(txtIdentificacao.Text),
                                oConfig.ValidaCampo(txtDescricao.Text),
                                oConfig.ValidaCampo(cboQTDHoras.SelectedValue),
                                oConfig.ValidaCampo(txtEmail.Text),
                                Session("Id_Usuario"),
                                "sp_SM",
                                False)

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Solicitacao_SLA(Session("Conn_Banco"),
                                txtIdentificacao.Text,
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
