
Public Class Custo_Fixo_Item
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
                "Item de Custo Fixo ", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboDescricao)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Custo_Fixo_Item", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboDescricao, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Custo_Fixo", Nothing))
            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))
            oConfig.CarregaCombo(cboAtivoTipo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo", Nothing))

            If Not Request("ID") = Nothing Then
                vdataset = WS_Cadastro.Custo_Fixo_Item(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)
                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Custo_Fixo_Item")
                cboDescricao.Text = vdataset.Tables(0).Rows(0).Item("Id_Custo_Fixo")
                cboAtivoTipo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo")
                cboConglomerado.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Conglomerado")
                txtValor.Text = vdataset.Tables(0).Rows(0).Item("Custo")
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboDescricao.SelectedValue = Nothing
        cboAtivoTipo.SelectedValue = Nothing
        cboConglomerado.SelectedValue = Nothing
        Page.SetFocus(cboDescricao)
        btSalvar.Enabled = True
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Custo_Fixo_Item(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                        cboDescricao.SelectedValue,
                                        cboAtivoTipo.SelectedValue,
                                        cboConglomerado.SelectedValue,
                                        oConfig.ValidaCampo(txtValor.Text),
                                        Nothing,
                                        Session("Id_Usuario"),
                                        "sp_SM",
                                        False)
        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub
    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Custo_Fixo_Item(Session("Conn_Banco"),
                                        txtIdentificacao.Text,
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
