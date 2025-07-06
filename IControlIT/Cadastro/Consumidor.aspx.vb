
Public Class Consumidor
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Politica As New WS_GUA_Politica.WSPolitica
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page 
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Cadastro de Consumidor",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtDescricao)
            'Page.Form.DefaultButton = btPDF.UniqueID
            Call Master.Localizar("sp_Drop_Consumidor", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboEmpresaContratada, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Empresa_Contratada", Nothing))
            oConfig.CarregaCombo(cboCargo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Cargo", Nothing))
            oConfig.CarregaCombo(cboFilial, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Filial", Nothing))
            oConfig.CarregaCombo(cboConsumidorTipo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Consumidor_Tipo", Nothing))
            oConfig.CarregaCombo(cboConsumidorStatus, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Consumidor_Status", Nothing))

            If Not Request("ID") = Nothing Then
                vdataset = WS_Cadastro.Consumidor(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing,
                                                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                      Nothing, Nothing, Nothing, Nothing, Nothing,
                                                      Nothing, "sp_SL_ID", True)

                If vdataset.Tables(0).Rows.Count = 0 Then Exit Sub

                Dim fl_Desativado As Int16 = vdataset.Tables(0).Rows(0).Item("Fl_Desativado")

                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Consumidor")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Nm_Consumidor")
                cboConsumidorTipo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Consumidor_Tipo")
                txtMatricula.Text = vdataset.Tables(0).Rows(0).Item("Matricula")
                txtEMail.Text = vdataset.Tables(0).Rows(0).Item("EMail")
                txtEmailCopia.Text = vdataset.Tables(0).Rows(0).Item("EMail_Copia")
                chkFl_Nao_Enviar_Email.Checked = IIf(vdataset.Tables(0).Rows(0).Item("Fl_Nao_Envia_EMail") = 2, True, False)
                cboEmpresaContratada.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Empresa_Contratada") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Empresa_Contratada"))
                cboCargo.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Cargo") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Cargo"))
                txtIdUsuario.Text = vdataset.Tables(0).Rows(0).Item("Id_Usuario")
                txtNmUsuario.Text = vdataset.Tables(0).Rows(0).Item("Nm_Usuario")
                cboConsumidorStatus.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Consumidor_Status") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Consumidor_Status"))
                cboFilial.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Filial") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Filial"))
                txtMatricula_Chefia.Text = vdataset.Tables(0).Rows(0).Item("Matricula_Chefia")
                txtObservacao.Text = vdataset.Tables(0).Rows(0).Item("Observacao")
                txtIndentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Consumidor")

                '-----carrega hierarquia
                If Not cboFilial.SelectedValue = Nothing Then oConfig.CarregaCombo(cboCentroCusto, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Centro_Custo", cboFilial.SelectedValue, Nothing))
                cboCentroCusto.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Centro_Custo")
                If Not cboCentroCusto.SelectedValue = Nothing Then oConfig.CarregaCombo(cboDepartamento, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Departamento", cboCentroCusto.SelectedValue, Nothing))
                cboDepartamento.Text = vdataset.Tables(0).Rows(0).Item("Id_Departamento")
                If Not cboDepartamento.SelectedValue = Nothing Then oConfig.CarregaCombo(cboSetor, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Setor", cboDepartamento.SelectedValue, Nothing))
                cboSetor.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Setor")
                If Not cboSetor.SelectedValue = Nothing Then oConfig.CarregaCombo(cboSecao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Secao", cboSetor.SelectedValue, Nothing))
                cboSecao.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Secao")

                '-----carrega ativo de consumidor
                dtgAtivo.DataSource = WS_Cadastro.Consumidor(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing,
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                  Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                  Nothing, "sp_SL_Ativo_Consumidor", True)
                dtgAtivo.DataBind()




                '-----carrega politica de uso
                vdataset = WS_Politica.Politica_Consumidor(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)
                If vdataset.Tables.Count > 0 Then
                    If vdataset.Tables(0).Rows.Count > 0 Then
                        txtValorPolitica.Text = IIf(vdataset.Tables(0).Rows(0).Item("Valor_Politica") = 0, "", vdataset.Tables(0).Rows(0).Item("Valor_Politica"))
                    End If
                End If

                '-----carrega link para cadastro de usuario
                'btConfiguracao.PostBackUrl = "~/Cadastro/Usuario.aspx?Id_Consumidor=" & txtIdentificacao.Text & "&Nm_Consumidor=" & txtDescricao.Text & IIf(Trim(txtIdUsuario.Text) = Nothing, "", "&ID=" & txtIdUsuario.Text)

                '-----carrega link para cadastro de usuario
                If Trim(txtIdUsuario.Text) = "0" Then
                    btConfiguracao.PostBackUrl = "~/Cadastro/Usuario.aspx?Id_Consumidor=" & txtIdentificacao.Text & "&Nm_Consumidor=" & txtDescricao.Text & "&Nm_Login=" & txtMatricula.Text & "@" & Session("Empresa")
                Else
                    btConfiguracao.PostBackUrl = "~/Cadastro/Usuario.aspx?Id_Consumidor=" & txtIdentificacao.Text & "&Nm_Consumidor=" & txtDescricao.Text & IIf(Trim(txtIdUsuario.Text) = Nothing, "", "&ID=" & txtIdUsuario.Text)
                End If

                '-----carrega link para cadastro de unidade
                'btAbrir.PostBackUrl = "~/Cadastro/Consumidor_Unidade.aspx?Id_Consumidor=" & txtIdentificacao.Text & "&Nm_Consumidor=" & txtDescricao.Text & IIf(Trim(txtIdUsuario.Text) = Nothing, "", "&ID=" & txtIdUsuario.Text)

                If fl_Desativado = 1 Then
                    pnlConfirmacao.Visible = True
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "disableScrolling();", True)
                End If
            End If
        End If
    End Sub

    Protected Sub cboFilial_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFilial.SelectedIndexChanged
        cboCentroCusto.Items.Clear()
        cboDepartamento.Items.Clear()
        cboSetor.Items.Clear()
        cboSecao.Items.Clear()
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        If Not cboFilial.SelectedValue = Nothing Then oConfig.CarregaCombo(cboCentroCusto, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Centro_Custo", cboFilial.SelectedValue, Nothing))
    End Sub

    Protected Sub cboCentroCusto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCentroCusto.SelectedIndexChanged
        cboDepartamento.Items.Clear()
        cboSetor.Items.Clear()
        cboSecao.Items.Clear()
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        If Not cboCentroCusto.SelectedValue = Nothing Then oConfig.CarregaCombo(cboDepartamento, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Departamento", cboCentroCusto.SelectedValue, Nothing))
    End Sub

    Protected Sub cboDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDepartamento.SelectedIndexChanged
        cboSetor.Items.Clear()
        cboSecao.Items.Clear()
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        If Not cboDepartamento.SelectedValue = Nothing Then oConfig.CarregaCombo(cboSetor, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Setor", cboDepartamento.SelectedValue, Nothing))
    End Sub

    Protected Sub cboSetor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSetor.SelectedIndexChanged
        cboSecao.Items.Clear()
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        If Not cboSetor.SelectedValue = Nothing Then oConfig.CarregaCombo(cboSecao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Secao", cboSetor.SelectedValue, Nothing))
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        chkFl_Nao_Enviar_Email.Checked = False
        cboConsumidorTipo.SelectedValue = Nothing
        cboConsumidorStatus.SelectedValue = Nothing
        cboCargo.SelectedValue = Nothing
        cboEmpresaContratada.SelectedValue = Nothing
        cboCentroCusto.Items.Clear()
        cboDepartamento.Items.Clear()
        cboFilial.SelectedValue = Nothing
        cboSecao.Items.Clear()
        cboSetor.Items.Clear()
        btConfiguracao.PostBackUrl = Nothing
        'btAbrir.PostBackUrl = Nothing
        dtgAtivo.DataSource = Nothing
        dtgAtivo.DataBind()
        btSalvar.Enabled = True
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        '-----verifica se colocou observacao
        If Trim(txtObservacaoObrigatoria.Text) = "" Then Exit Sub

        If IsNumeric(txtDescricao.Text) Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim novoConsumidor As String = txtIdentificacao.Text

        vdataset = WS_Cadastro.Consumidor(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                        oConfig.ValidaCampo(txtDescricao.Text),
                                        cboConsumidorTipo.SelectedValue,
                                        oConfig.ValidaCampo(txtMatricula.Text),
                                        oConfig.ValidaCampo(txtEMail.Text),
                                        oConfig.ValidaCampo(txtEmailCopia.Text()),
                                        IIf(chkFl_Nao_Enviar_Email.Checked = True, 2, 1),
                                        oConfig.ValidaCampo(Mid(txtObservacaoObrigatoria.Text, 1, 8000)),
                                        oConfig.ValidaCampo(cboEmpresaContratada.Text),
                                        oConfig.ValidaCampo(cboCargo.Text),
                                        oConfig.ValidaCampo(cboFilial.Text),
                                        oConfig.ValidaCampo(cboCentroCusto.Text),
                                        oConfig.ValidaCampo(cboDepartamento.Text),
                                        oConfig.ValidaCampo(cboSetor.Text),
                                        oConfig.ValidaCampo(cboSecao.Text),
                                        oConfig.ValidaCampo(cboConsumidorStatus.SelectedValue),
                                        oConfig.ValidaCampo(txtMatricula_Chefia.Text),
                                        Session("Id_Usuario"),
                                        "sp_SM",
                                        False)

        txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item(0)

        vdataset = WS_Cadastro.Consumidor(Session("Conn_Banco"), txtIdentificacao.Text, Nothing, Nothing, Nothing,
                                                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                      Nothing, Nothing, Nothing, Nothing, Nothing,
                                                      Nothing, "sp_SL_ID", True)

        txtObservacao.Text = vdataset.Tables(0).Rows(0).Item("Observacao")
        txtIndentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Consumidor")

        '-----registro salvo ok
        If txtIdentificacao.Text = 0 Then
            '-----registro salvo ok
            Call Master.Registro_Salvo(lblMatricula.Text & " - " & txtMatricula.Text & " já cadastrado.")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Cadastro_Consumidor_btSalvar_Click", "Modal('#myModalRegistroSalvo');", True)
        Else
            If Not Trim(txtValorPolitica.Text) = "" Then
                WS_Politica.Politica_Consumidor(Session("Conn_Banco"),
                                                    oConfig.ValidaCampo(txtIdentificacao.Text),
                                                    Nothing,
                                                    Nothing,
                                                    oConfig.ValidaCampo(txtValorPolitica.Text),
                                                    1,
                                                    Nothing,
                                                    Session("Id_Usuario"),
                                                    "sp_Insere_Politica",
                                                    False)
            End If

            If String.IsNullOrEmpty(novoConsumidor) Then
                Dim login As String
                If txtMatricula.Text.Contains("@") Then
                    login = txtMatricula.Text
                Else
                    login = txtMatricula.Text & "@" & Session("Empresa")
                End If
                Response.Redirect("~/Cadastro/Usuario.aspx?Id_Consumidor=" & txtIdentificacao.Text & "&Nm_Consumidor=" & txtDescricao.Text & "&Nm_Login=" & login & IIf(Trim(txtIdUsuario.Text) = Nothing, "", "&ID=" & txtIdUsuario.Text))
                btConfiguracao.PostBackUrl = "~/Cadastro/Usuario.aspx?Id_Consumidor=" & txtIdentificacao.Text & "&Nm_Consumidor=" & txtDescricao.Text & "&Nm_Login=" & login & IIf(Trim(txtIdUsuario.Text) = Nothing, "", "&ID=" & txtIdUsuario.Text)
            End If


            '-----carrega link para cadastro de usuario
            'btConfiguracao.PostBackUrl = "~/Cadastro/Usuario.aspx?Id_Consumidor=" & txtIdentificacao.Text & "&Nm_Consumidor=" & txtDescricao.Text & IIf(Trim(txtIdUsuario.Text) = Nothing, "", "&ID=" & txtIdUsuario.Text)

            '-----carrega link para cadastro de unidade
            'btAbrir.PostBackUrl = "~/Cadastro/Consumidor_Unidade.aspx?Id_Consumidor=" & txtIdentificacao.Text & "&Nm_Consumidor=" & txtDescricao.Text & IIf(Trim(txtIdUsuario.Text) = Nothing, "", "&ID=" & txtIdUsuario.Text)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Cadastro_Consumidor_btSalvar_Click", "Modal('#myModalRegistroSalvo');", True)
        End If
        '-----registro salvo ok
        pnlObservacao.Visible = False
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Consumidor(Session("Conn_Banco"),
                                    txtIdentificacao.Text,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
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

    Protected Sub btContinuar_Click(sender As Object, e As EventArgs) Handles btContinuar.Click
        pnlConfirmacao.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
    End Sub

    Protected Sub btRestaurar_Click(sender As Object, e As EventArgs) Handles btRestaurar.Click
        '-----desativa/ativa registro
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Lixeira(Session("Conn_Banco"), txtIdentificacao.Text, Session("Id_Usuario"), "Consumidor")
        pnlConfirmacao.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
        Response.Redirect("Consumidor.aspx" & "?id=" & Request("ID"))
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btSalvar",
                                                        False)
        pnlRegistro.Visible = False
        pnlObservacao.Visible = True
        txtObservacao.Text = ""
    End Sub

    Protected Sub btFechar_Registro_Click(sender As Object, e As EventArgs) Handles btFechar_Registro.Click
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btFechar_Registro",
                                                        False)
        pnlRegistro.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
    End Sub

    Protected Sub btCancela_Click(sender As Object, e As EventArgs) Handles btCancela.Click
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btCancela",
                                                        False)
        pnlObservacao.Visible = False
    End Sub

    Protected Sub btAbrir_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btAbrir",
                                                        False)
        pnlRegistro.Visible = True
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "disableScrolling();", True)
    End Sub

    Protected Sub btnAbrirTermo_Click(sender As Object, e As ImageClickEventArgs)
        Dim botao As ImageButton = CType(sender, ImageButton)
        Dim argumentos = botao.CommandArgument.Split("|"c)

        Dim idAtivo = argumentos(0)
        Dim idConsumidor = argumentos(1)
        Dim tipoGrupo = argumentos(2)

        ' Monta URL
        Dim url = $"http://localhost/Termo/Termo_Responsabilidade_Fortlev.aspx?Id_Ativo={idAtivo}%2c{idAtivo}&Id_Consumidor={idConsumidor}%2c{idConsumidor}&Nm_Ativo_Tipo_Grupo={tipoGrupo}%2c{tipoGrupo}"

        ' Abre nova aba
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirTermo", $"window.open('{url}', '_blank');", True)
    End Sub

End Class
