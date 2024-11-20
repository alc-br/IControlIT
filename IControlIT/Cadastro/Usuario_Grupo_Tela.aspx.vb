
Public Class Usuario_Grupo_Tela
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim oAcesso_Usuario As New cls_Relacionamento
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
                "Permissão de Acesso a Tela", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            oConfig.CarregaCombo(cboUsuarioGrupo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Usuario_Grupo", Nothing))
            Page.SetFocus(cboUsuarioGrupo)
            Page.Form.DefaultButton = btPDF.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            If Not Request("ID") = Nothing Then
                Call Carrega_Dados(Request("ID"))
                cboUsuarioGrupo.SelectedValue = Request("ID")
            End If
        End If
    End Sub

    Protected Sub cboUsuarioGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUsuarioGrupo.SelectedIndexChanged
        If cboUsuarioGrupo.SelectedValue = Nothing Then Exit Sub
        Carrega_Dados(cboUsuarioGrupo.SelectedValue)
    End Sub

    Public Sub Limpar()
        txtGrupo.Text = ""
        lstOrigem.Items.Clear()
        lstDestino.Items.Clear()
        cboUsuarioGrupo.SelectedValue = Nothing
        btSalvar.Enabled = True
    End Sub

    Public Sub Carrega_Dados(ByVal pUsuarioGrupo As System.Int32)
        '-----gretorna relacionamento de usuario grupo com tela do sistema
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        oConfig.CarregaList(lstDestino, oConfig.CarregaDragDrop(WS_Cadastro.Usuario_Grupo(Session("Conn_Banco"), pUsuarioGrupo, Nothing, Nothing, "sd_SL_Rl_Usuario_Grupo_Si_Tela", True)))
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
        '-----carrega lista de origem com politica ativo 
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        oConfig.CarregaList(lstOrigem, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Si_Tela", txtGrupo.Text))
    End Sub
    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call Limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        '-----grava relacionamento de usuario grupo com tela do sistema
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Relacionamento(Session("Conn_Banco"), "sp_SM_RL_Ususario_Grupo_Si_Tela", cboUsuarioGrupo.SelectedValue, oConfig.AgrupaDados(lstDestino))

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If cboUsuarioGrupo.SelectedValue = Nothing Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Usuario_Grupo(Session("Conn_Banco"),
                                        cboUsuarioGrupo.SelectedValue,
                                        Nothing,
                                        Session("Id_Usuario"),
                                        "sp_SE_RL",
                                        False)
        Call Limpar()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
