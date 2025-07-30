
Public Class Principal
    Inherits System.Web.UI.MasterPage
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim vdataSet As System.Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Nm_Usuario") Is Nothing Then
            Response.Redirect("DEFAULT.aspx")
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            '-----ativa tela de aguarde
            System.Threading.Thread.Sleep(1000)

            Dim v_dataSet As New System.Data.DataSet
            Dim vCont As System.Int32 = 0
            Dim vMnPai As System.Int32 = 0

            '-----identifica usuario logado
            v_dataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Valida_Usuario", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)
            lblNome.Text = v_dataSet.Tables(0).Rows(0).Item("Nm_Consumidor")
            imgLogoCliente.ImageUrl = v_dataSet.Tables(0).Rows(0).Item("Logo")

            '-----cria menu de usuario
            '-----session para criacao do menu 
            If Session("Menu") Is Nothing Then Session("Menu") = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Menu", Session("Nm_Usuario"), Nothing, Nothing, Nothing, Session("Id_Idioma"))
            v_dataSet = Session("Menu")

            ' [INÍCIO - ICTRL2025029] - Preenche o label de debug com as permissões da sessão
            ' Este bloco verifica se as variáveis de sessão existem antes de tentar lê-las.
            If Session("Modulos_Permitidos") IsNot Nothing Or Session("Torres_Permitidas") IsNot Nothing Then

                ' Lê o valor da sessão para Módulos. Se for nulo ou vazio no banco, exibe "Nenhuma".
                Dim modulos As String = If(Session("Modulos_Permitidos") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Session("Modulos_Permitidos").ToString()), Session("Modulos_Permitidos").ToString(), "Nenhuma")

                ' Lê o valor da sessão para Torres. Se for nulo ou vazio no banco, exibe "Nenhuma".
                Dim torres As String = If(Session("Torres_Permitidas") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Session("Torres_Permitidas").ToString()), Session("Torres_Permitidas").ToString(), "Nenhuma")

                ' Preenche o texto do label.
                lblDebugPermissions.Text = String.Format("DEBUG | Módulos: {0} | Torres: {1}", modulos, torres)

            End If
            ' [FIM - ICTRL2025029]


            If v_dataSet.Tables(0).Rows.Count > 0 Then
                '-----monta menu
                For vCont = 0 To v_dataSet.Tables(0).Rows.Count - 1
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 10 Then
                        btConfiguracao.Disabled = False
                        btConfiguracao.Style.Add("Opacity", "0.9")
                        'btSConfiguracao.Style.Add("Opacity", "0.9")
                    End If
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 5 Then
                        ''btFerramenta.Disabled = False
                        ''btFerramenta.Style.Add("Opacity", "0.9")
                        'btSFerramenta.Style.Add("Opacity", "0.9")
                    End If
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 6 Then
                        btEstoque.Disabled = False
                        btEstoque.Style.Add("Opacity", "0.9")
                        'btSEstoque.Style.Add("Opacity", "0.9")
                    End If
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 15 Then
                        btOrcamento.Disabled = False
                        btOrcamento.Style.Add("Opacity", "0.9")
                        'btSOrcamento.Style.Add("Opacity", "0.9")
                    End If
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 3 Then
                        btContrato.Disabled = False
                        btContrato.Style.Add("Opacity", "0.9")
                        'btSContrato.Style.Add("Opacity", "0.9")
                    End If
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 7 Then
                        btFatura.Disabled = False
                        btFatura.Style.Add("Opacity", "0.9")
                        'btSFatura.Style.Add("Opacity", "0.9")
                    End If
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 11 Then
                        btContabil.Disabled = False
                        btContabil.Style.Add("Opacity", "0.9")
                        'btSContabil.Style.Add("Opacity", "0.9")
                    End If
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 4 Then
                        btContestacao.Disabled = False
                        btContestacao.Style.Add("Opacity", "0.9")
                        'btSContestacao.Style.Add("Opacity", "0.9")
                    End If
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 8 Then
                        btCadastro.Disabled = False
                        btCadastro.Style.Add("Opacity", "0.9")
                        'btSCadastro.Style.Add("Opacity", "0.9")
                    End If
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 14 Then
                        btMarcacao.Disabled = False
                        btMarcacao.Style.Add("Opacity", "0.9")
                        'btSMarcacao.Style.Add("Opacity", "0.9")
                    End If
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 12 Then
                        btRelatorio.Disabled = False
                        btRelatorio.Style.Add("Opacity", "0.9")
                        'btSRelatorio.Style.Add("Opacity", "0.9")
                    End If
                    If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 18 Then
                        ''btIncidente.Disabled = False
                        ''btIncidente.Style.Add("Opacity", "0.9")
                        '''btSIncidente.Style.Add("Opacity", "0.9")
                    End If
                    'If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 17 Then
                    '    btCaixa_Entrada.Disabled = False
                    '    btCaixa_Entrada.Style.Add("Opacity", "0.9")
                    '    'btSCaixa_Entrada.Style.Add("Opacity", "0.9")
                    'End If
                Next vCont
            End If

            '-----verifica permissao para abertura de chamado
            'Dim vDataSet As System.Data.DataSet
            'Dim i As System.Int16
            'vDataSet = WS_Modulo.Solicitacao(Session("Conn_Banco"),
            '                                Nothing, Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing, Nothing,
            '                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Consulta_Permissao_Abertura", True)

            'For i = 0 To vDataSet.Tables(0).Rows.Count - 1
            '    '-----usuario
            '    If vDataSet.Tables(0).Rows(i).Item("Id_Solicitacao_Permissao") = 1 Then
            '        Call ValidaBotaoSuporte("1")
            '    End If

            '    '-----unidade
            '    If vDataSet.Tables(0).Rows(i).Item("Id_Solicitacao_Permissao") = 2 Then
            '        Call ValidaBotaoSuporte("2")
            '    End If

            '    '-----roaming
            '    If vDataSet.Tables(0).Rows(i).Item("Id_Solicitacao_Permissao") = 3 Then
            '        Call ValidaBotaoSuporte("3")
            '    End If
            'Next

            '-----valida sidebar para o tipo de usuario
            If Session("Id_Usuario_Perfil_Acesso") <> Nothing Then
                If Session("Id_Usuario_Perfil_Acesso") <> 1 Then
                    sidebarAdm.Visible = False
                    sidebarUsuario.Visible = True
                End If
            End If


            SelecionaBotao(Request("Pagina"))
        End If
    End Sub

    Public Sub Localizar(vPakage As System.String, vPagina As System.String)
        hdfPakage.Value = vPakage
        hdfPagina.Value = vPagina

        If vPakage = Nothing And vPagina = Nothing Then
            'btLocalizar.Visible = False
            divLocalizar.Visible = False
        End If

        If divLocalizar.Visible = True Then
            divPlaceholder.Attributes("class") = "placeholder-pesquisa"
        End If
    End Sub

    Public Sub Registro_Salvo(vRequest As System.String)
        lbl_Msg.Text = vRequest
    End Sub

    Public Sub Titulo(ByVal pTexto As System.String)
        lblTitulo.Text = pTexto
        lblTituloSub.Text = pTexto
    End Sub

    Public Sub home(vRequest As System.String)
        hdfHome.Value = vRequest
    End Sub

    Public Sub Voltar(ByVal pOnClick As System.String, vRequest As System.String)
        If Not pOnClick = Nothing Then
            'btVoltar.OnClientClick = pOnClick
            hdfVoltarOnClick.Value = pOnClick
        End If
        hdfVoltar.Value = vRequest
    End Sub

    'Protected Sub BtVoltar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btVoltar.Click
    '    If tbPesquisa.Visible = False Then
    '        If btVoltar.OnClientClick.ToString = Nothing Then
    '            Response.Redirect(hdfVoltar.Value)
    '        End If
    '    Else
    '        btVoltar.OnClientClick = hdfVoltarOnClick.Value
    '        btLocalizar.Visible = True
    '        tbPesquisa.Visible = False
    '        pnlFundoLocalizar.Visible = False
    '        pnlLocalizar.Visible = False
    '    End If
    'End Sub

    'Protected Sub btLocalizar_Click(sender As Object, e As ImageClickEventArgs) Handles btLocalizar.Click
    '    'btVoltar.OnClientClick = Nothing
    '    btLocalizar.Visible = False
    '    'tbPesquisa.Visible = True
    '    pnlFundoLocalizar.Visible = True
    '    Page.SetFocus(txtPesquisa)
    'End Sub

    'Protected Sub btIniciar_Click(sender As Object, e As ImageClickEventArgs) Handles btIniciar.Click
    '    If hdfHome.Value = "usuario" Then
    '        Response.Redirect("~/CockPit_Menu.aspx")
    '    Else
    '        Response.Redirect("~/Home.aspx")
    '    End If
    'End Sub

    Protected Sub btnDash_Click(sender As Object, e As EventArgs)
        If Session("Id_Usuario_Perfil_Acesso") <> 1 Then
            Response.Redirect("~/CockPit_Menu.aspx")
        Else
            Response.Redirect("~/Home.aspx")
        End If
    End Sub

    Protected Sub btPesquisar_Click(sender As Object, e As EventArgs)
        Call Pesquisar(True, True)
    End Sub

    Public Sub Pesquisar(ByVal visible As Boolean, ByVal btnCloseVisible As Boolean)

        If visible = True Then
            Localizar1.Pakage = hdfPakage.Value
            Localizar1.Descricao = txtPesquisa.Text
            Localizar1.Selecao = hdfPagina.Value
            Localizar1.DataBind()

            pnlLocalizar.Visible = visible
            divCloseLocalizar.Visible = btnCloseVisible

            If btnCloseVisible = False Then
                divTituloSub.Visible = True
                divTitulo.Visible = False
            End If
        End If

    End Sub

    Public Sub Pesquisar_Contrato_Tabela(ByVal visible As Boolean, ByVal btnCloseVisible As Boolean)

        If visible = True Then
            Contrato_Tabela1.Pakage = hdfPakage.Value
            Contrato_Tabela1.Descricao = txtPesquisa.Text
            Contrato_Tabela1.Selecao = hdfPagina.Value
            Contrato_Tabela1.DataBind()

            pnlContrato_Tabela.Visible = visible
            divCloseContrato_Tabela.Visible = btnCloseVisible

            If btnCloseVisible = False Then
                divTituloSub.Visible = True
                divTitulo.Visible = False
            End If
        End If

    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        pnlLocalizar.Visible = False
    End Sub

    Protected Sub btAgenda_Click(sender As Object, e As System.EventArgs) Handles btAgenda.Click
        Response.Redirect("~/Marcacao/Agenda_Particular.aspx")
    End Sub

    Protected Sub btContaAntiga_Click(sender As Object, e As System.EventArgs) Handles btContaAntiga.Click
        Response.Redirect("~/Marcacao/Lote.aspx")
    End Sub

    Protected Sub btCaixa_Entrada_Click(sender As Object, e As System.EventArgs) Handles btCaixa_Entrada.Click
        Response.Redirect("~/Caixa_Entrada/Consulta_Caixa_Entrada.aspx")
    End Sub

    Protected Sub btFerramenta_Click(sender As Object, e As EventArgs) Handles btFerramenta.Click
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PrincipalMaster|btFerramenta_Click", "openDropCadastro();", True)
    End Sub

    Public Sub SelecionaBotao(ByVal btn As String)
        navHomeMenu.Attributes("class") = "nav-item"

        If btn = "contrato" Then
            navContrato.Attributes("class") = "nav-item active"
        ElseIf btn = "cadastro" Then
            navConta.Attributes("class") = "nav-item active"
        ElseIf btn = "fatura" Then
            navFatura.Attributes("class") = "nav-item active"
        ElseIf btn = "contestacao" Then
            navContestacao.Attributes("class") = "nav-item active"
        ElseIf btn = "rateio" Then
            navRateio.Attributes("class") = "nav-item active"
        ElseIf btn = "estoque" Then
            navEstoque.Attributes("class") = "nav-item active"
        ElseIf btn = "orcamento" Then
            navOrcamento.Attributes("class") = "nav-item active"
        ElseIf btn = "contaUsuario" Then
            navMarcacao.Attributes("class") = "nav-item active"
        ElseIf btn = "suporte" Then
            ''navSuporte.Attributes("class") = "nav-item active"
        ElseIf btn = "relatorio" Then
            navRelatorio.Attributes("class") = "nav-item active"
        ElseIf btn = "config" Then
            navConfig.Attributes("class") = "nav-item active"
        ElseIf btn = "suporte" Then
            btChamado.Style.Add("background-color", "#0072B3")
        ElseIf btn = "unidade" Then
            btRequisicaoUnidade.Visible = True
            btRequisicaoUnidade.Style.Add("background-color", "#0072B3")
        ElseIf btn = "usuario" Then
            btRequisicaoUsuario.Visible = True
            btRequisicaoUsuario.Style.Add("background-color", "#0072B3")
        ElseIf btn = "roaming" Then
            btSolicitacao.Visible = True
            btSolicitacao.Style.Add("background-color", "#0072B3")
        End If

    End Sub

    Public Sub ValidaBotaoSuporte(ByVal vPermissao As String)

        'btRequisicaoUsuario.HRef = ""
        'btRequisicaoUnidade.HRef = ""
        'btSolicitacao.HRef = ""

        If vPermissao = "1" Then
            btRequisicaoUsuario.Visible = True
        ElseIf vPermissao = "2" Then
            btRequisicaoUnidade.Visible = True
        ElseIf vPermissao = "3" Then
            btSolicitacao.Visible = True
        End If

    End Sub

End Class