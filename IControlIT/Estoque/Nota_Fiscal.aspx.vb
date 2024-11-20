
Public Class Nota_Fiscal
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Estoque As New WS_GUA_Estoque.WSEstoque

    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                "Cadastro de Nota Fiscal ", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtNotaFiscal)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Estoque_Nota_Fiscal", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboFormaAquisicao, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Ativo_Fr_Aquisicao", Nothing))

            If Not Request("ID") = Nothing Then
                vdataset = WS_Estoque.Nota_Fiscal(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)

                btPDF.OnClientClick = "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & vdataset.Tables(0).Rows(0).Item("Id_Estoque_Nota_Fiscal") & "&pTabela=Estoque_Nota_Fiscal','','resizable=yes, menubar=yes, scrollbars=no,height=768px, width=1024px, top=10, left=10')"

                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Estoque_Nota_Fiscal")
                txtNotaFiscal.Text = vdataset.Tables(0).Rows(0).Item("Nr_Nota_Fiscal")
                txtData.Text = vdataset.Tables(0).Rows(0).Item("Dt_Nota_Fiscal")
                txtObservacao.Text = vdataset.Tables(0).Rows(0).Item("Observacao")
                cboFormaAquisicao.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Fr_Aquisicao")
                txtDataInicioFormaAquisicao.Text = vdataset.Tables(0).Rows(0).Item("Dt_Inicio_Fr_Aquisicao")
                txtValorFormaAquisicao.Text = vdataset.Tables(0).Rows(0).Item("Vr_Fr_Aquisicao")
                txtMesFormaAquisicao.Text = vdataset.Tables(0).Rows(0).Item("Qtd_Mes_Residuo_Fr_Aquisicao")
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboFormaAquisicao.SelectedValue = Nothing
        txtMesFormaAquisicao.Text = "24"
    End Sub

    Protected Sub txtData_TextChanged(sender As Object, e As System.EventArgs) Handles txtData.TextChanged
        txtDataInicioFormaAquisicao.Text = txtData.Text
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        '-----nao insere registro quando descricao so for numerica
        WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials

        txtIdentificacao.Text = WS_Estoque.Nota_Fiscal(Session("Conn_Banco"),
                                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                                        oConfig.ValidaCampo(txtNotaFiscal.Text),
                                                        oConfig.ValidaCampo(txtData.Text),
                                                        oConfig.ValidaCampo(cboFormaAquisicao.SelectedValue),
                                                        oConfig.ValidaCampo(txtValorFormaAquisicao.Text),
                                                        IIf(Trim(txtDataInicioFormaAquisicao.Text) = "", Nothing, txtDataInicioFormaAquisicao.Text),
                                                        oConfig.ValidaCampo(txtMesFormaAquisicao.Text),
                                                        oConfig.ValidaCampo(Mid(txtObservacao.Text, 1, 300)),
                                                        Session("Id_Usuario"),
                                                        "sp_SM",
                                                        False).Tables(0).Rows(0).Item(0)

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub

        WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials

        WS_Estoque.Nota_Fiscal(Session("Conn_Banco"), txtIdentificacao.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Session("Id_Usuario"), "sp_SE", False)
        Call limpar()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
