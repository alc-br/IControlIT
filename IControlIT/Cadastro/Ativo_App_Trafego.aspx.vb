
Public Class Ativo_App_Trafego
    Inherits System.Web.UI.Page

    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Configura Monitoramento de Dados",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtAtivoConta)
            Page.Form.DefaultButton = btPDF.UniqueID
            Call Master.Localizar("sp_Drop_App_Monitoramento", Page.AppRelativeVirtualPath.ToString)

            If Not Request("ID") = Nothing Then
                vdataset = WS_Modulo.Monitoramento_Dados(Session("Conn_Banco"), Nothing, Nothing, Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)
                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Ativo")
                txtAtivoConta.Text = vdataset.Tables(0).Rows(0).Item("Nr_Ativo")
                txtCorteDe.Text = vdataset.Tables(0).Rows(0).Item("Corte_De")
                txtCorteAte.Text = vdataset.Tables(0).Rows(0).Item("Corte_Ate")
                txtPacoteMB.Text = vdataset.Tables(0).Rows(0).Item("Pacote_MB")
                txtMes.Text = vdataset.Tables(0).Rows(0).Item("Qtd_Mes")
            End If

        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        Page.SetFocus(txtAtivoConta)
        btSalvar.Enabled = True
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        '-----registro salvo
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Monitoramento_Dados(Session("Conn_Banco"),
                                        Nothing,
                                        Nothing,
                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                        oConfig.ValidaCampo(txtAtivoConta.Text),
                                        oConfig.ValidaCampo(txtCorteDe.Text),
                                        oConfig.ValidaCampo(txtCorteAte.Text),
                                        oConfig.ValidaCampo(txtPacoteMB.Text),
                                        oConfig.ValidaCampo(txtMes.Text),
                                        Session("Id_Usuario"),
                                        "sp_SM",
                                        True)
        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Monitoramento_Dados(Session("Conn_Banco"),
                                        Nothing,
                                        Nothing,
                                        txtIdentificacao.Text,
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
