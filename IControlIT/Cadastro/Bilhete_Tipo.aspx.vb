
Public Class Bilhete_Tipo
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
                "Tipo e Descrição do Bilhete", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtTipo)
            Page.Form.DefaultButton = btPDF.UniqueID
            Call Master.Localizar("sp_Drop_Bilhete_Tipo", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))
            If Not Request("ID") = Nothing Then
                vdataset = WS_Cadastro.Bilhete_Tipo(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, _
                                                        Nothing, Nothing, "sp_SL_ID", True)

                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Bilhete_Tipo")
                txtTipo.Text = vdataset.Tables(0).Rows(0).Item("Nm_Bilhete_Tipo")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Nm_Bilhete_Descricao")
                optUnidade.SelectedValue = vdataset.Tables(0).Rows(0).Item("Unidade")
                cboConglomerado.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Conglomerado") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Conglomerado"))
                optTipoDescricao.SelectedValue = vdataset.Tables(0).Rows(0).Item("Tipo_Descricao")
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboConglomerado.SelectedValue = Nothing
        optUnidade.SelectedValue = 1
        optTipoDescricao.SelectedValue = 1
        Page.SetFocus(txtTipo)
        btSalvar.Enabled = True
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        '-----nao insere registro quando descricao so for numerica
        If IsNumeric(txtTipo.Text) Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Bilhete_Tipo(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                        cboConglomerado.SelectedValue,
                                        oConfig.ValidaCampo(txtTipo.Text),
                                        oConfig.ValidaCampo(txtDescricao.Text),
                                        optUnidade.SelectedValue,
                                        optTipoDescricao.SelectedValue,
                                        Session("Id_Usuario"),
                                        "sp_SM",
                                        False)
        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Bilhete_Tipo(Session("Conn_Banco"), txtIdentificacao.Text,
                                        Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Session("Id_Usuario"), "sp_SE", False)
        Call limpar()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
