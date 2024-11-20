
Public Class Usuario_Perfil
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
                "Cadastro de Perfil de Usuário ", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboConfiguraAcesso)
            Page.Form.DefaultButton = btPDF.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            If Not Request("ID") = Nothing Then
                vdataset = WS_Cadastro.Usuario_Perfil(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, _
                                                            Nothing, Nothing, Nothing, Nothing, _
                                                            Nothing, "sp_SL_ID", True)
                txtIdUsuario.Text = vdataset.Tables(0).Rows(0).Item("Id_Usuario")
                txtNmUsuario.Text = vdataset.Tables(0).Rows(0).Item("Nm_Usuario")
                txtIdConsumidor.Text = vdataset.Tables(0).Rows(0).Item("Id_Consumidor")
                txtNmConsumidor.Text = vdataset.Tables(0).Rows(0).Item("Nm_Consumidor")
                txtIdUsuarioPerfil.Text = vdataset.Tables(0).Rows(0).Item("Id_Usuario_Perfil")
                txtNmUsuarioPerfil.Text = vdataset.Tables(0).Rows(0).Item("Nm_Usuario_Perfil")
                txtIdUsuarioPerfilAcesso.Text = vdataset.Tables(0).Rows(0).Item("Id_Usuario_Perfil_Acesso")
                txtNmUsuarioPerfilAcesso.Text = vdataset.Tables(0).Rows(0).Item("Nm_Usuario_Perfil_Acesso")
                '-----carrega ativo competencia para configuracao de acesso
                Session("DataSet") = WS_Cadastro.Usuario_Perfil(Session("Conn_Banco"), Nothing, txtIdConsumidor.Text, Nothing, Nothing, _
                                                                                Nothing, Nothing, Nothing, Nothing, "sp_SL_Ativo_Competencia_Consumidor", True)

                dtgAtivoCompetencia.DataSource = Session("DataSet")
                dtgAtivoCompetencia.DataBind()
                '-----monta combo de perfil de acesso
                oAcesso_Usuario.carregaAcesso(txtIdUsuarioPerfilAcesso.Text, cboConfiguraAcesso)

            End If
        End If
    End Sub

    Protected Sub cboConfiguraAcesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboConfiguraAcesso.SelectedIndexChanged
        pnlDetalhe.Visible = True
        lstDestino.Items.Clear()
        lstOrigem.Items.Clear()
        If Not cboConfiguraAcesso.SelectedValue = Nothing Then
            '-----retorna relacionamento
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            vdataset = WS_Cadastro.Usuario_Perfil(Session("Conn_Banco"), txtIdUsuario.Text, Nothing, Nothing,
                                                        Nothing, Nothing, Nothing, Nothing, Nothing,
                                                        cboConfiguraAcesso.SelectedValue & "_Retorno", True)
            oConfig.CarregaList(lstDestino, vdataset)
        End If
    End Sub

    Protected Sub btSalvarGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSalvarGrupo.Click
        '-----grava relacionamento do perfil selecionado 
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Relacionamento(Session("Conn_Banco"), cboConfiguraAcesso.SelectedValue & "_Insere", txtIdUsuario.Text, oConfig.AgrupaDados(lstDestino))
        cboConfiguraAcesso.SelectedValue = Nothing
        lstOrigem.Items.Clear()
        lstDestino.Items.Clear()
        txtGrupo.Text = ""
        pnlDetalhe.Visible = False
    End Sub

    Protected Sub btGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btGrupo.Click
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        oConfig.CarregaList(lstOrigem, WS_Cadastro.Usuario_Perfil(Session("Conn_Banco"), txtIdUsuario.Text, Nothing, txtGrupo.Text, Nothing, _
                                                                Nothing, Nothing, Nothing, Nothing, _
                                                                cboConfiguraAcesso.SelectedValue, True))
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

    Protected Sub dtgAtivoCompetencia_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAtivoCompetencia.PageIndexChanged
        dtgAtivoCompetencia.CurrentPageIndex = e.NewPageIndex
        dtgAtivoCompetencia.DataSource = Session("DataSet")
        dtgAtivoCompetencia.DataBind()
    End Sub

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        cboConfiguraAcesso.SelectedValue = Nothing
        pnlDetalhe.Visible = False
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        '-----grava relacionamento do perfil selecionado 
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Relacionamento(Session("Conn_Banco"), cboConfiguraAcesso.SelectedValue & "_Insere", txtIdUsuario.Text, oConfig.AgrupaDados(lstDestino))
        cboConfiguraAcesso.SelectedValue = Nothing
        lstOrigem.Items.Clear()
        lstDestino.Items.Clear()
        txtGrupo.Text = ""
        pnlDetalhe.Visible = False
    End Sub
    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdUsuario.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Usuario_Perfil(Session("Conn_Banco"),
                                        txtIdUsuario.Text,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Session("Id_Usuario"),
                                        "sp_SE",
                                        False)

        cboConfiguraAcesso.SelectedValue = Nothing
        pnlDetalhe.Visible = False
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
