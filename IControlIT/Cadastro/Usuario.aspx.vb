
Public Class Usuario
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
                "Cadastro de Usuário ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtDescricao)
            Page.Form.DefaultButton = btPDF.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            oConfig.CarregaCombo(cboIdioma, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Idioma", Nothing))
            oConfig.CarregaCombo(cboUsuarioGrupo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Usuario_Grupo", Nothing))
            oConfig.CarregaCombo(cboUsuarioPerfil, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Usuario_Perfil", Nothing))

            If Not Request("Id_Consumidor") = Nothing And Not Request("Nm_Consumidor") = Nothing And Not Request("Nm_Login") = Nothing Then
                txtIdConsumidor.Text = Request("Id_Consumidor")
                txtNmConsumidor.Text = Request("Nm_Consumidor")
                txtDescricao.Text = Request("Nm_Login")
            End If

            If Not Request("ID") = Nothing Then
                vdataset = WS_Cadastro.Usuario(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing,
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                    Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)

                If vdataset.Tables(0).Rows.Count > 0 Then
                    txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Usuario")
                    txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Nm_Usuario")
                    txtIdConsumidor.Text = vdataset.Tables(0).Rows(0).Item("Id_Consumidor")
                    txtNmConsumidor.Text = vdataset.Tables(0).Rows(0).Item("Nm_Consumidor")
                    cboIdioma.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Idioma")
                    cboUsuarioGrupo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Usuario_Grupo")
                    cboUsuarioPerfil.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Usuario_Perfil")
                    optIncluir.SelectedValue = vdataset.Tables(0).Rows(0).Item("Incluir")
                    optAlterar.SelectedValue = vdataset.Tables(0).Rows(0).Item("Alterar")
                    optExcluir.SelectedValue = vdataset.Tables(0).Rows(0).Item("Excluir")
                    optDetalhamentoConta.SelectedValue = vdataset.Tables(0).Rows(0).Item("Detalhamento_Conta")
                    optDetalhamentoContato.SelectedValue = vdataset.Tables(0).Rows(0).Item("Detalhamento_Contato")
                    optStatusUsuario.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Fl_Desativado") = 2, 3, vdataset.Tables(0).Rows(0).Item("Fl_Desativado"))

                    '-----informa senha
                    If vdataset.Tables(0).Rows(0).Item("Id_Usuario_Grupo") = 1 Then
                        If Session("Nm_Usuario") = vdataset.Tables(0).Rows(0).Item("Nm_Usuario") Then
                            lblSenha.Text = "Senha(" & vdataset.Tables(0).Rows(0).Item("senha") & ")"
                        Else
                            lblSenha.Text = "Senha(**********)"
                        End If
                    Else
                        lblSenha.Text = "Senha(" & vdataset.Tables(0).Rows(0).Item("senha") & ")"
                    End If
                    '-----perfil de acesso
                    oConfig.CarregaCombo(cboUsuarioPerfilAcesso, WS_Cadastro.Usuario(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                                           cboUsuarioPerfil.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                                           Nothing, Nothing, Nothing, "sp_SL_Usuario_Perfil_Acesso", True))

                    cboUsuarioPerfilAcesso.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Usuario_Perfil_Acesso")
                    '-----carrega link para cadastro de perfil de usuario
                    btConfiguracao.PostBackUrl = "~/Cadastro/Usuario_Perfil.aspx?ID=" & txtIdentificacao.Text

                    If vdataset.Tables(0).Rows(0).Item("Fl_Desativado") = 1 Then
                        txtDescricao.BackColor = Drawing.Color.Red
                        txtIdConsumidor.BackColor = Drawing.Color.Red
                        txtNmConsumidor.BackColor = Drawing.Color.Red
                        cboIdioma.BackColor = Drawing.Color.Red
                        cboUsuarioGrupo.BackColor = Drawing.Color.Red
                        cboUsuarioPerfil.BackColor = Drawing.Color.Red
                        cboUsuarioPerfilAcesso.BackColor = Drawing.Color.Red
                        btRedefinirSenha.Enabled = False
                    End If

                    '-----monta cadastro de hierarquia
                    vdataset = WS_Cadastro.Relacionamento(Session("Conn_Banco"), "sp_Cadastro_Solicitacao_Permissao", txtIdentificacao.Text, Nothing)
                    oConfig.CarregaList(lstDestino, vdataset)
                End If
            End If
        End If
    End Sub

    Protected Sub cboUsuarioPerfil_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUsuarioPerfil.SelectedIndexChanged
        If cboUsuarioPerfil.SelectedValue = Nothing Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        oConfig.CarregaCombo(cboUsuarioPerfilAcesso, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Usuario_Perfil_Acesso_Id_Usuario_Perfil", cboUsuarioPerfil.SelectedValue, Nothing))
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboIdioma.SelectedValue = Nothing
        cboUsuarioGrupo.SelectedValue = Nothing
        cboUsuarioPerfil.SelectedValue = Nothing
        optIncluir.SelectedValue = 1
        optAlterar.SelectedValue = 1
        optExcluir.SelectedValue = 1
        optDetalhamentoConta.SelectedValue = 1
        optDetalhamentoContato.SelectedValue = 1
        optStatusUsuario.SelectedValue = 1
        cboUsuarioPerfilAcesso.Items.Clear()
        btSalvar.Enabled = True

        txtDescricao.BackColor = Drawing.Color.White
        txtIdConsumidor.BackColor = Drawing.Color.White
        txtNmConsumidor.BackColor = Drawing.Color.White
        cboIdioma.BackColor = Drawing.Color.White
        cboUsuarioGrupo.BackColor = Drawing.Color.White
        cboUsuarioPerfil.BackColor = Drawing.Color.White
        cboUsuarioPerfilAcesso.BackColor = Drawing.Color.White
        btRedefinirSenha.Enabled = True
    End Sub

    Protected Sub btRedefinirSenha_Click(sender As Object, e As System.EventArgs) Handles btRedefinirSenha.Click
        If txtIdentificacao.Text = "" Then Exit Sub
        '-----reseta senha
        WS_Cadastro.Usuario(Session("Conn_Banco"), txtIdentificacao.Text, Nothing, Nothing, Nothing, Nothing, Nothing,
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                            Nothing, Nothing, Nothing, "sp_Reiniciar_Senha", True)

        '-----lista senha
        vdataset = WS_Cadastro.Usuario(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing,
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                    Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)

        lblSenha.Text = "Senha(" & vdataset.Tables(0).Rows(0).Item("senha") & ")"
        Call Master.Registro_Salvo("Senha alterada para: " & vdataset.Tables(0).Rows(0).Item("senha"))
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btGrupo_Click(sender As Object, e As EventArgs) Handles btGrupo.Click
        '-----carrega consumidor
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        oConfig.CarregaList(lstOrigem, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Solicitacao_Permissao", txtGrupo.Text))
    End Sub

    Protected Sub btMoveSelecionado_Click(sender As Object, e As EventArgs) Handles btMoveSelecionado.Click
        Call move_Dados(lstOrigem, lstDestino)
    End Sub

    Protected Sub btMoveSelecao_Click(sender As Object, e As EventArgs) Handles btMoveSelecao.Click
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
        txtIdentificacao.Text = WS_Cadastro.Usuario(Session("Conn_Banco"),
                                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                                        oConfig.ValidaCampo(txtDescricao.Text),
                                                        Nothing,
                                                        oConfig.ValidaCampo(txtIdConsumidor.Text),
                                                        oConfig.ValidaCampo(cboIdioma.SelectedValue),
                                                        oConfig.ValidaCampo(cboUsuarioGrupo.SelectedValue),
                                                        oConfig.ValidaCampo(cboUsuarioPerfil.SelectedValue),
                                                        oConfig.ValidaCampo(cboUsuarioPerfilAcesso.SelectedValue),
                                                        optIncluir.SelectedValue,
                                                        optAlterar.SelectedValue,
                                                        optExcluir.SelectedValue,
                                                        optDetalhamentoConta.SelectedValue,
                                                        optDetalhamentoContato.SelectedValue,
                                                        optStatusUsuario.SelectedValue,
                                                        Session("Id_Usuario"),
                                                        "sp_SM",
                                                        False).Tables(0).Rows(0).Item(0)

        '-----grava relacionamento do perfil selecionado
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Relacionamento(Session("Conn_Banco"), "sp_Rl_Usuario_Solictacao_Permissao_Insere", txtIdentificacao.Text, oConfig.AgrupaDados(lstDestino))

        '-----carrega link para cadastro de perfil de usuario
        btConfiguracao.PostBackUrl = "~/Cadastro/Usuario_Perfil.aspx?ID=" & txtIdentificacao.Text

        '-----verifica se ususario e senha ja existem 
        If txtIdentificacao.Text = 999 Then
            Call Master.Registro_Salvo("* Usuário já está sendo utilizado, favor informar outro usuário!")
        End If
        ''-----registro salvo ok
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)

        If optStatusUsuario.SelectedValue = 1 Then
            txtDescricao.BackColor = Drawing.Color.Red
            txtIdConsumidor.BackColor = Drawing.Color.Red
            txtNmConsumidor.BackColor = Drawing.Color.Red
            cboIdioma.BackColor = Drawing.Color.Red
            cboUsuarioGrupo.BackColor = Drawing.Color.Red
            cboUsuarioPerfil.BackColor = Drawing.Color.Red
            cboUsuarioPerfilAcesso.BackColor = Drawing.Color.Red
            btRedefinirSenha.Enabled = False
        Else
            txtDescricao.BackColor = Nothing
            txtIdConsumidor.BackColor = Nothing
            txtNmConsumidor.BackColor = Nothing
            cboIdioma.BackColor = Nothing
            cboUsuarioGrupo.BackColor = Nothing
            cboUsuarioPerfil.BackColor = Nothing
            cboUsuarioPerfilAcesso.BackColor = Nothing
            btRedefinirSenha.Enabled = True
        End If
        'todos os procedimentos realizados voltar para  pagina de consumidor com o id do consumidor
        '---------txtIdConsumidor.Text
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "alert('Registro salvo com sucesso!'); window.location.href = '" & ResolveClientUrl("~/Cadastro/Consumidor.aspx?id=" & txtIdConsumidor.Text) & "';", True)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "alert('Registro salvo com sucesso!'); window.stop();", True)
        'System.Threading.Thread.Sleep(6000)
        'Response.Redirect("~/Cadastro/Consumidor.aspx?id=" & txtIdConsumidor.Text)

    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
