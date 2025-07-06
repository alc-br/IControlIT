
Public Class Consumidor_Estoque
    Inherits System.Web.UI.Page
    Dim WS_Estoque As New WS_GUA_Estoque.WSEstoque
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro

    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Identificar Usuário de Estoque ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtGrupo)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            vdataset = WS_Estoque.Estoque_Consumidor(Session("Conn_Banco"), Nothing, Nothing, "sp_SL_ID", True)
            oConfig.CarregaList(lstDestino, vdataset)
        End If
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

    Protected Sub btGrupo_Click(sender As Object, e As System.EventArgs) Handles btGrupo.Click
        WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        If Trim(txtGrupo.Text) = "" Then Exit Sub
        oConfig.CarregaList(lstOrigem, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Consumidor_Ativo", txtGrupo.Text))
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----grava relacionamento do perfil selecionado
        WS_Estoque.Estoque_Consumidor(Session("Conn_Banco"), oConfig.AgrupaDados(lstDestino), Session("Id_Usuario"), "sp_Grava_Registro", False)
        '-----limpa e esconde lista de selecao
        txtGrupo.Text = ""
        lstOrigem.Items.Clear()
        lstDestino.Items.Clear()
        oConfig.CarregaList(lstDestino, WS_Estoque.Estoque_Consumidor(Session("Conn_Banco"), Nothing, Nothing, "sp_SL_ID", True))

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
