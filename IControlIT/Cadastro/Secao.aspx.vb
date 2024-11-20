
Public Class Secao
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
                "Cadastro de Seção ", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtDescricao)
            Page.Form.DefaultButton = btPDF.UniqueID
            Call Master.Localizar("sp_Drop_Secao", Page.AppRelativeVirtualPath.ToString)

            If Not Request("ID") = Nothing Then
                vdataset = WS_Cadastro.Secao(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, "sp_SL_ID", True)
                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Secao")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Nm_Secao")

                '-----monta cadastro de hierarquia
                vdataset = WS_Cadastro.Relacionamento(Session("Conn_Banco"), "sp_Cadastro_Hierarquia", txtIdentificacao.Text, "SECAO")
                oConfig.CarregaList(lstDestino, vdataset)
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        lstOrigem.Items.Clear()
        lstDestino.Items.Clear()
        txtGrupo.Text = ""
        Page.SetFocus(txtDescricao)
        btSalvar.Enabled = True
    End Sub

    Protected Sub btGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btGrupo.Click
        '-----carrega consumidor
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        oConfig.CarregaList(lstOrigem, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Setor", txtGrupo.Text))
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
        txtIdentificacao.Text = WS_Cadastro.Secao(Session("Conn_Banco"),
                                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                                        oConfig.ValidaCampo(txtDescricao.Text),
                                                        Session("Id_Usuario"),
                                                        "sp_SM",
                                                        False).Tables(0).Rows(0).Item(0)

        '-----grava relacionamento do perfil selecionado
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Relacionamento(Session("Conn_Banco"), "sp_Rl_Hi_Setor_Secao_Insere", txtIdentificacao.Text, oConfig.AgrupaDados(lstDestino))

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtDescricao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Secao(Session("Conn_Banco"),
                                txtIdentificacao.Text,
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
