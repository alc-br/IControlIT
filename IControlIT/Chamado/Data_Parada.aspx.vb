
Public Class Data_Parada
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
                "Data Parada", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtData)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Solicitacao_Data_Parada", Page.AppRelativeVirtualPath.ToString)

            If Not Request("ID") = Nothing Then
                vdataset = WS_Cadastro.Solicitacao_Data_Parada(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, "sp_SL_ID", True)
                txtData.Text = vdataset.Tables(0).Rows(0).Item("Data")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Descricao")
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        Page.SetFocus(txtDescricao)
        btSalvar.Enabled = True
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Solicitacao_Data_Parada(Session("Conn_Banco"),
                                            oConfig.ValidaCampo(txtIdentificacao.Text),
                                            oConfig.ValidaCampo(txtData.Text),
                                            oConfig.ValidaCampo(txtDescricao.Text),
                                            Session("Id_Usuario"),
                                            "sp_SM",
                                            False)
        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtData.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Solicitacao_Data_Parada(Session("Conn_Banco"),
                                           oConfig.ValidaCampo(txtIdentificacao.Text),
                                            Nothing,
                                            Nothing,
                                            Session("Id_Usuario"),
                                            "sp_SE",
                                            False)
        Call limpar()
    End Sub

    Protected Sub bt_Recalcular_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Solicitacao_Data_Parada(Session("Conn_Banco"),
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Session("Id_Usuario"),
                                            "sp_Recalcula",
                                            False)
        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
