
Public Class Filial
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
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Cadastro de Filial ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtDescricao)
            Page.Form.DefaultButton = btPDF.UniqueID
            Call Master.Localizar("sp_Drop_Filial", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboEmpresa, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Empresa", Nothing))

            If Not Request("ID") = Nothing Then
                vdataset = WS_Cadastro.Filial(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)
                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Filial")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Nm_Filial")
                txtCNPJ.Text = vdataset.Tables(0).Rows(0).Item("CNPJ")
                txtEndereco.Text = vdataset.Tables(0).Rows(0).Item("Endereco")
                cboEmpresa.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Empresa") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Empresa"))
                chkDepartamento.Checked = IIf(vdataset.Tables(0).Rows(0).Item("Hi_Departamento") = 0, False, True)
                chkSetor.Checked = IIf(vdataset.Tables(0).Rows(0).Item("Hi_Setor") = 0, False, True)
                chkSecao.Checked = IIf(vdataset.Tables(0).Rows(0).Item("Hi_Secao") = 0, False, True)

                '-----retorna relacionamento de filial co bilhete tipo
                vdataset = WS_Cadastro.Filial(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sd_SL_RL_Filial_Bilhete_Tipo", True)
                oConfig.CarregaList(lstDestino, vdataset)
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboEmpresa.SelectedValue = Nothing
        lstOrigem.Items.Clear()
        lstDestino.Items.Clear()
        chkDepartamento.Checked = False
        chkSetor.Checked = False
        btSalvar.Enabled = True
    End Sub

    Protected Sub btGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btGrupo.Click
        '-----carrega tipo de bilhete
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        oConfig.CarregaList(lstOrigem, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Bilhete_Tipo", txtGrupo.Text))
    End Sub

    Protected Sub btMoveSelecionado_Click(sender As Object, e As System.EventArgs) Handles btMoveSelecionado.Click
        Call move_Dados(lstOrigem, lstDestino)
    End Sub

    Protected Sub btMoveSelecao_Click(sender As Object, e As System.EventArgs) Handles btMoveSelecao.Click
        Call move_Dados(lstDestino, lstOrigem)
    End Sub

    Public Sub move_Dados(pOrigem As ListBox, pDestino As ListBox)
        If pOrigem.SelectedValue = Nothing Then Exit Sub

        Dim vOrigen As ListItem
        '-----adiciona
        vOrigen = New ListItem
        vOrigen.Value = pOrigem.SelectedItem.Value
        vOrigen.Text = pOrigem.SelectedItem.Text
        pDestino.Items.Add(vOrigen)
        '-----remove
        pOrigem.Items.Remove(vOrigen)
    End Sub
    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        txtIdentificacao.Text = WS_Cadastro.Filial(Session("Conn_Banco"),
                                                    oConfig.ValidaCampo(txtIdentificacao.Text),
                                                    oConfig.ValidaCampo(txtDescricao.Text),
                                                    oConfig.ValidaCampo(txtCNPJ.Text),
                                                    oConfig.ValidaCampo(txtEndereco.Text),
                                                    oConfig.ValidaCampo(cboEmpresa.SelectedValue),
                                                    IIf(chkDepartamento.Checked = True, 1, 0),
                                                    IIf(chkSetor.Checked = True, 1, 0),
                                                    IIf(chkSecao.Checked = True, 1, 0),
                                                    Session("Id_Usuario"),
                                                    "sp_SM",
                                                    False).Tables(0).Rows(0).Item(0)

        '-----grava relacionamento de filial com bilhete tipo
        WS_Cadastro.Relacionamento(Session("Conn_Banco"), "sp_SM_RL_Filial_Bilhete_Tipo", txtIdentificacao.Text, oConfig.AgrupaDados(lstDestino))

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Filial(Session("Conn_Banco"),
                            txtIdentificacao.Text,
                            Nothing,
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
