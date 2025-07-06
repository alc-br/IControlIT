Public Class CockPit_Menu
    Inherits System.Web.UI.Page

    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vdataSet As System.Data.DataSet
    Dim vdataSetTemp As System.Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            Page.Form.DefaultButton = btPesquisar.UniqueID

            Session("CockPit") = Nothing
            Session("Calendario") = Nothing

            '-----cria menu de usuario
            If Not Session("Nm_Usuario") Is Nothing Then
                '-----identifica usuario logado
                'vdataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Valida_Usuario", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)
                'lblNome.Text = vdataSet.Tables(0).Rows(0).Item("Nm_Consumidor")
                'imgLogoCliente.ImageUrl = vdataSet.Tables(0).Rows(0).Item("Logo")
                'PageBody.Style.Item("background-image") = Replace("url('" + vdataSet.Tables(0).Rows(0).Item("Logo").ToString.Replace(".png", "_Site.png") + "')", "~/", "")

                '-----busca dados menu
                '-----session para criacao do menu 
                vdataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Valida_Ativo_Grupo", Nothing, Nothing, Nothing, Nothing, Nothing)
                Dim Linha As System.Data.DataRow

                '------monta dashboard so para usuario gestor
                If Session("Id_Usuario_Perfil_Acesso") = 4 Or
                    Session("Id_Usuario_Perfil_Acesso") = 5 Or
                    Session("Id_Usuario_Perfil_Acesso") = 6 Or
                    Session("Id_Usuario_Perfil_Acesso") = 7 Or
                    Session("Id_Usuario_Perfil_Acesso") = 8 Then

                    For Each Linha In vdataSet.Tables(0).Rows
                        If Linha.Item(0) = "Telefonia_Movel" Then
                            btTelefoniaMovel.CommandName = Linha.Item(1)
                            If Not Linha.Item(1) = "" Then btTelefoniaMovel.Enabled = True

                            'btDadosMaveis.CommandName = "Dados_Moveis"
                            'If Not Linha.Item(1) = "" Then btDadosMaveis.Enabled = True
                        End If

                        If Linha.Item(0) = "Telefonia_Fixa" Then
                            btTelefoniaFixa.CommandName = Linha.Item(1)
                            If Not Linha.Item(1) = "" Then btTelefoniaFixa.Enabled = True
                        End If

                        If Linha.Item(0) = "Link_Dados" Then
                            btLink.CommandName = Linha.Item(1)
                            If Not Linha.Item(1) = "" Then btLink.Enabled = True
                        End If

                        If Linha.Item(0) = "Equipamento" Then
                            btDeskTop.CommandName = Linha.Item(1)
                            If Not Linha.Item(1) = "" Then btDeskTop.Enabled = True
                        End If

                        If Linha.Item(0) = "Impressora" Then
                            btImpressao.CommandName = Linha.Item(1)
                            If Not Linha.Item(1) = "" Then btImpressao.Enabled = True
                        End If

                        If Linha.Item(0) = "Cloud" Then
                            btCloud.CommandName = Linha.Item(1)
                            btCloud.Enabled = True
                        End If
                    Next
                End If

                If btTelefoniaMovel.Enabled = False Then
                    btTelefoniaMovel.Style.Add("Opacity", "0.4")
                    lblDMovel.Style.Add("Opacity", "0.2")
                    tdTM.Style.Add("Opacity", "0.4")

                    'btDadosMaveis.Style.Add("Opacity", "0.4")
                    'lblDadosMoveis.Style.Add("Opacity", "0.4")
                    'tdDM.Style.Add("Opacity", "0.4")
                End If

                If btTelefoniaFixa.Enabled = False Then
                    btTelefoniaFixa.Style.Add("Opacity", "0.4")
                    lblDFixa.Style.Add("Opacity", "0.2")
                    tdTF.Style.Add("Opacity", "0.4")
                End If

                If btLink.Enabled = False Then
                    btLink.Style.Add("Opacity", "0.4")
                    lblDLink.Style.Add("Opacity", "0.2")
                    tdLK.Style.Add("Opacity", "0.4")
                End If

                If btDeskTop.Enabled = False Then
                    btDeskTop.Style.Add("Opacity", "0.4")
                    lblDDesktop.Style.Add("Opacity", "0.2")
                    tdDK.Style.Add("Opacity", "0.4")
                End If

                If btImpressao.Enabled = False Then
                    btImpressao.Style.Add("Opacity", "0.4")
                    lblDImpressao.Style.Add("Opacity", "0.2")
                    tdIM.Style.Add("Opacity", "0.4")
                End If

                If btCloud.Enabled = False Then
                    btCloud.Style.Add("Opacity", "0.4")
                    lblDCloud.Style.Add("Opacity", "0.2")
                    tdCL.Style.Add("Opacity", "0.4")
                End If
            End If

            '-----verifica a opcao voltar
            If Request("Postback") = 1 Then
                DivConta.Visible = True
                lblObservacaoConta.Visible = False
                Call Monta_Dados()
                pnlMinhasContas.Visible = True
                pnlMeusAtivos.Visible = False
                'pnlFundo.Visible = True
            Else
                Session("KPI") = Nothing
                DivConta.Visible = True
                lblObservacaoConta.Visible = False
                Call Monta_Dados()
            End If

            '-----------------------------------------------------------------------------------------------------------
            '-----botoes relatorio
            '-----------------------------------------------------------------------------------------------------------
            lblObservacao.Visible = False
            lblObservacao_Det.Visible = False
            'lblGrupo.Text = "Movél"


            'btDadosMaveis.Attributes.Add("onclick", "alert('Essa operação pode levar alguns minutos.');timedCount();")

            Call Filtro(1)
            Call ValidaBotaoSub(btRMovel)

            Dim acesso As String

            acesso = Session("Id_Usuario_Perfil_Acesso").ToString()

            If Request("Pagina") <> Nothing Then
                Call ValidaPagina(Request("Pagina"))
            End If
        End If
    End Sub

    Private Sub Monta_Dados()
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        dtgLote.CurrentPageIndex = 0
        dtgAtivo.CurrentPageIndex = 0

        '-----monta ativo sob responsabilidade do colaborador
        Session("DataSet_1") = Nothing
        Session("DataSet_1") = WS_Consulta.Lote(Session("Conn_Banco"),
                                            "sp_Ativo_Responsabilidade",
                                            "Usuario",
                                            Session("Id_Usuario"),
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing)
        dtgAtivo.DataSource = Session("DataSet_1")
        dtgAtivo.DataBind()

        '-----minha equipe

        If WS_Consulta.Lote(Session("Conn_Banco"), "sp_Minha_Equipe", "Usuario", Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing).Tables.Count > 0 Then
            dtgMinhaEquipe.DataSource = WS_Consulta.Lote(Session("Conn_Banco"),
                                                            "sp_Minha_Equipe",
                                                            "Usuario",
                                                            Session("Id_Usuario"),
                                                            Nothing,
                                                            Nothing,
                                                            Nothing,
                                                            Nothing,
                                                            Nothing,
                                                            Nothing,
                                                            Nothing,
                                                            Nothing,
                                                            Nothing)
            dtgMinhaEquipe.DataBind()
        End If

        '----monta conta marcacao
        Session("DataSet") = Nothing
        Session("DataSet") = WS_Consulta.Lote(Session("Conn_Banco"),
                                                "sp_Lote_Marcacao",
                                                "Usuario",
                                                Session("Id_Usuario"),
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Session("KPI"),
                                                Nothing)
        dtgLote.DataSource = Session("DataSet")
        dtgLote.DataBind()

        '------lista meus ativos
        vdataSet = WS_Cadastro.Ativo(Session("Conn_Banco"), Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing,
                                 Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                 Nothing, "sd_SL_Modelo", True, Nothing, Nothing, Nothing, Nothing, Nothing)

        dtgModeloAtivo.DataSource = vdataSet
        dtgModeloAtivo.DataBind()

        If dtgLote.Items.Count = 0 Then
            DivConta.Visible = False
            lblObservacaoConta.Visible = True
        End If
    End Sub

    Protected Sub lblTipo1_Click(sender As Object, e As System.EventArgs)
        Dim v_btSalvar As Button = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgAtivo") + 19, 4), System.Int32)

        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim v_Ativo_Grupo As HiddenField
        v_Ativo_Grupo = dtgAtivo.Items(i).Cells(0).Controls(3)

        DivConta.Visible = True
        lblObservacaoConta.Visible = False
        Session("KPI") = v_Ativo_Grupo.Value
        Call Monta_Dados()

        pnlMinhasContas.Visible = True
        pnlMeusAtivos.Visible = False
        'pnlFundo.Visible = True
    End Sub

    Protected Sub btAtivo1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgAtivo") + 19, 4), System.Int32)

        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim v_Ativo_Grupo As HiddenField
        v_Ativo_Grupo = dtgAtivo.Items(i).Cells(0).Controls(3)

        DivConta.Visible = True
        lblObservacaoConta.Visible = False
        Session("KPI") = v_Ativo_Grupo.Value
        Call Monta_Dados()

        pnlMinhasContas.Visible = True
        pnlMeusAtivos.Visible = False
        'pnlFundo.Visible = True

        'If Convert.ToInt32(Hdf_LarguraTela.Value) < 600 Then

        '    Dim btnConta1, btnConta1Mobile, btnConta2, btnConta2Mobile As ImageButton

        '    For i = 0 To dtgLote.Items.Count - 1
        '        btnConta1 = dtgLote.Items(i).Cells(0).Controls(1).Controls(3)
        '        btnConta1Mobile = dtgLote.Items(i).Cells(0).Controls(1).Controls(5)

        '        btnConta2 = dtgLote.Items(i).Cells(1).Controls(1).Controls(3)
        '        btnConta2Mobile = dtgLote.Items(i).Cells(1).Controls(1).Controls(5)

        '        btnConta1.Visible = False
        '        btnConta1Mobile.Visible = True

        '        btnConta2.Visible = False
        '        btnConta2Mobile.Visible = True
        '    Next

        'End If
    End Sub

    Protected Sub lblTipo2_Click(sender As Object, e As System.EventArgs)
        Dim v_btSalvar As Button = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgAtivo") + 19, 4), System.Int32)

        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim v_Ativo_Grupo As HiddenField
        v_Ativo_Grupo = dtgAtivo.Items(i).Cells(1).Controls(3)

        DivConta.Visible = True
        lblObservacaoConta.Visible = False
        Session("KPI") = v_Ativo_Grupo.Value
        Call Monta_Dados()

        pnlMinhasContas.Visible = True
        pnlMeusAtivos.Visible = False
        'pnlFundo.Visible = True
    End Sub

    Protected Sub btAtivo2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgAtivo") + 19, 4), System.Int32)

        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim v_Ativo_Grupo As HiddenField
        v_Ativo_Grupo = dtgAtivo.Items(i).Cells(1).Controls(3)

        DivConta.Visible = True
        lblObservacaoConta.Visible = False
        Session("KPI") = v_Ativo_Grupo.Value
        Call Monta_Dados()

        pnlMinhasContas.Visible = True
        pnlMeusAtivos.Visible = False
        'pnlFundo.Visible = True

        'If Convert.ToInt32(Hdf_LarguraTela.Value) < 600 Then

        '    Dim btnConta1, btnConta1Mobile, btnConta2, btnConta2Mobile As ImageButton

        '    For i = 0 To dtgLote.Items.Count - 1
        '        btnConta1 = dtgLote.Items(i).Cells(0).Controls(1).Controls(3)
        '        btnConta1Mobile = dtgLote.Items(i).Cells(0).Controls(1).Controls(5)

        '        btnConta2 = dtgLote.Items(i).Cells(1).Controls(1).Controls(3)
        '        btnConta2Mobile = dtgLote.Items(i).Cells(1).Controls(1).Controls(5)

        '        btnConta1.Visible = False
        '        btnConta1Mobile.Visible = True

        '        btnConta2.Visible = False
        '        btnConta2Mobile.Visible = True
        '    Next

        'End If
    End Sub

    Protected Sub lblTipo3_Click(sender As Object, e As System.EventArgs)
        Dim v_btSalvar As Button = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgAtivo") + 19, 4), System.Int32)

        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim v_Ativo_Grupo As HiddenField
        v_Ativo_Grupo = dtgAtivo.Items(i).Cells(2).Controls(3)

        DivConta.Visible = True
        lblObservacaoConta.Visible = False
        Session("KPI") = v_Ativo_Grupo.Value
        Call Monta_Dados()

        pnlMinhasContas.Visible = True
        pnlMeusAtivos.Visible = False
        'pnlFundo.Visible = True
    End Sub

    Protected Sub btAtivo3_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgAtivo") + 19, 4), System.Int32)

        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim v_Ativo_Grupo As HiddenField
        v_Ativo_Grupo = dtgAtivo.Items(i).Cells(2).Controls(3)

        DivConta.Visible = True
        lblObservacaoConta.Visible = False
        Session("KPI") = v_Ativo_Grupo.Value
        Call Monta_Dados()

        pnlMinhasContas.Visible = True
        pnlMeusAtivos.Visible = False
        'pnlFundo.Visible = True

        '-----remonta as contas quando estivermos e uma tela mobile
        'If Convert.ToInt32(Hdf_LarguraTela.Value) < 600 Then

        '    Dim btnConta1, btnConta1Mobile, btnConta2, btnConta2Mobile As ImageButton

        '    For i = 0 To dtgLote.Items.Count - 1
        '        btnConta1 = dtgLote.Items(i).Cells(0).Controls(1).Controls(3)
        '        btnConta1Mobile = dtgLote.Items(i).Cells(0).Controls(1).Controls(5)

        '        btnConta2 = dtgLote.Items(i).Cells(1).Controls(1).Controls(3)
        '        btnConta2Mobile = dtgLote.Items(i).Cells(1).Controls(1).Controls(5)

        '        btnConta1.Visible = False
        '        btnConta1Mobile.Visible = True

        '        btnConta2.Visible = False
        '        btnConta2Mobile.Visible = True
        '    Next

        'End If
    End Sub

    Protected Sub dtgAtivo_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAtivo.PageIndexChanged
        dtgAtivo.CurrentPageIndex = e.NewPageIndex
        dtgAtivo.DataSource = Session("DataSet_1")
        dtgAtivo.DataBind()
        If dtgLote.Items.Count = 0 Then
            DivConta.Visible = False
            lblObservacaoConta.Visible = True
        End If
    End Sub

    Protected Sub dtgLote_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgLote.PageIndexChanged
        dtgLote.CurrentPageIndex = e.NewPageIndex
        dtgLote.DataSource = Session("DataSet")
        dtgLote.DataBind()
        If dtgLote.Items.Count = 0 Then
            DivConta.Visible = False
            lblObservacaoConta.Visible = True
        End If
    End Sub

    Protected Sub btTelefoniaMovel_Click(sender As Object, e As System.EventArgs) Handles btTelefoniaMovel.Click
        Session("KPI") = "Telefonia_Movel"
        Call Aguarde()
        Response.Redirect("~/Dashboard_Telefonia/Dashboar_Telefonia.aspx?larguraTela=" + Hdf_LarguraTela.Value + "")
    End Sub

    'Protected Sub btDadosMaveis_Click(sender As Object, e As System.EventArgs) Handles btDadosMaveis.Click
    '    Session("KPI") = "Telefonia_Movel_Trafego"
    '    Call Aguarde()
    '    Response.Redirect("~/Dashboard_Telefonia/Dashboar_Uso_Dados.aspx?larguraTela=" + Hdf_LarguraTela.Value + "")
    'End Sub

    Protected Sub btTelefoniaFixa_Click(sender As Object, e As System.EventArgs) Handles btTelefoniaFixa.Click
        Session("KPI") = "Telefonia_Fixa"
        Call Aguarde()
        Response.Redirect("~/Dashboard_Telefonia/Dashboar_Telefonia.aspx?larguraTela=" + Hdf_LarguraTela.Value + "")
    End Sub

    Protected Sub btDeskTop_Click(sender As Object, e As System.EventArgs) Handles btDeskTop.Click
        Session("KPI") = "Equipamento"
        Call Aguarde()
        Response.Redirect("~/Dashboard_Equipamento/Dashboar_Equipamento.aspx?larguraTela=" + Hdf_LarguraTela.Value + "")
    End Sub

    Protected Sub btImpressao_Click(sender As Object, e As System.EventArgs) Handles btImpressao.Click
        Session("KPI") = "Impressora"
        Call Aguarde()
        Response.Redirect("~/Dashboard_Impressao/Dashboar_Impressao.aspx?larguraTela=" + Hdf_LarguraTela.Value + "")
    End Sub

    Protected Sub btCloud_Click(sender As Object, e As System.EventArgs) Handles btCloud.Click
        'Session("KPI") = "Cloud"
        'Call Aguarde()
    End Sub

    Protected Sub btLink_Click(sender As Object, e As System.EventArgs) Handles btLink.Click
        Session("KPI") = "Link_Dados"
        Call Aguarde()
        Response.Redirect("~/Dashboard_Link/Dashboar_Link.aspx?larguraTela=" + Hdf_LarguraTela.Value + "")
    End Sub

    Public Sub Aguarde()
        '-----gera dados 
        '---------------------------------------------------------------------------------------
        If Session("Id_Usuario_Perfil_Acesso") = 4 _
                Or Session("Id_Usuario_Perfil_Acesso") = 5 _
                Or Session("Id_Usuario_Perfil_Acesso") = 6 _
                Or Session("Id_Usuario_Perfil_Acesso") = 7 _
                Or Session("Id_Usuario_Perfil_Acesso") = 8 Then

            WS_Modulo.Timeout = 3600000
            Dim vPakage As String = Nothing

            If Session("KPI") = "Telefonia_Movel_Trafego" Then
                'vPakage = btDadosMaveis.CommandName
            End If

            If Session("KPI") = "Telefonia_Movel" Then
                vPakage = btTelefoniaMovel.CommandName
            End If

            If Session("KPI") = "Telefonia_Fixa" Then
                vPakage = btTelefoniaFixa.CommandName
            End If

            If Session("KPI") = "Link_Dados" Then
                vPakage = btLink.CommandName
            End If

            If Session("KPI") = "Equipamento" Then
                vPakage = btDeskTop.CommandName
            End If

            If Session("KPI") = "Impressora" Then
                vPakage = btImpressao.CommandName
            End If

            If Session("KPI") = "Cloud" Then
                vPakage = btCloud.CommandName
            End If

            '-----desabilita botoes quando usuario nao tiver cadastro de hierarquia
            vdataSetTemp = WS_Modulo.Deskboard(Session("Conn_Banco"), vPakage, IIf(Session("KPI") = "Telefonia_Movel_Trafego", "Telefonia_Movel", Session("KPI")), Session("Id_Usuario"), Nothing)
            Session("CockPit") = vdataSetTemp
        End If
    End Sub

    Protected Sub btRMovel_Click(sender As Object, e As System.EventArgs) Handles btRMovel.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        'lblGrupo.Text = "Telefonia Movél"
        Session("KPI") = "Telefonia_Movel"
        Call Filtro(1)
    End Sub

    Protected Sub btRFixa_Click(sender As Object, e As System.EventArgs) Handles btRFixa.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        'lblGrupo.Text = "Telefonia Fixa"
        Session("KPI") = "Telefonia_Fixa"
        Call Filtro(2)
    End Sub

    Protected Sub btRCloud_Click(sender As Object, e As System.EventArgs) Handles btRCloud.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        'lblGrupo.Text = "Cloud"
        Session("KPI") = "Cloud"
        Call Filtro(99)
    End Sub

    Protected Sub btRLink_Click(sender As Object, e As System.EventArgs) Handles btRLink.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        'lblGrupo.Text = "Link de Dados"
        Session("KPI") = "Link_Dados"
        Call Filtro(3)
    End Sub

    Protected Sub btRImpressao_Click(sender As Object, e As System.EventArgs) Handles btRImpressao.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        'lblGrupo.Text = "Impressão"
        Session("KPI") = "Impressora"
        Call Filtro(5)
    End Sub

    Protected Sub btREquipamento_Click(sender As Object, e As System.EventArgs) Handles btREquipamento.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        'lblGrupo.Text = "Equipamento/Licença de SW"
        Session("KPI") = "Equipamento"
        Call Filtro(4)
    End Sub

    Protected Sub LimpaBgBotaoSub()

        btRMovel.CssClass = "btn-tab-menu-disable pull-left"
        btRFixa.CssClass = "btn-tab-menu-disable pull-left"
        btRCloud.CssClass = "btn-tab-menu-disable pull-left"
        btRLink.CssClass = "btn-tab-menu-disable pull-left"
        btREquipamento.CssClass = "btn-tab-menu-disable pull-left"
        btRImpressao.CssClass = "btn-tab-menu-disable pull-left"

    End Sub

    Protected Sub ValidaBotaoSub(ByVal btn As LinkButton)

        Call LimpaBgBotaoSub()
        btn.CssClass = "btn-tab-menu pull-left"

    End Sub

    Public Sub Filtro(pId_Ativo_Tipo_Grupo As System.Int32)
        Session("DataSet") = Nothing
        '-----monta consulta
        dtgLocalizaRelatorio.CurrentPageIndex = Nothing
        '-----localiza
        Session("DataSet") = WS_Modulo.Validacao_Relatorio(Session("Conn_Banco"), "sp_Lista_Relario_liberado", Nothing, Session("Id_Usuario"), pId_Ativo_Tipo_Grupo)
        dtgLocalizaRelatorio.DataSource = Session("DataSet")
        dtgLocalizaRelatorio.DataBind()

        '-----verifica botao sem link
        Dim vbt01, vbt02 As LinkButton
        Dim vlblBt01, vlblBt02 As Label

        For linha = 0 To dtgLocalizaRelatorio.Items.Count - 1

            vbt01 = dtgLocalizaRelatorio.Items(linha).Cells(0).Controls(1)
            vlblBt01 = dtgLocalizaRelatorio.Items(linha).Cells(0).Controls(1).Controls(1)
            vbt02 = dtgLocalizaRelatorio.Items(linha).Cells(1).Controls(1)
            vlblBt02 = dtgLocalizaRelatorio.Items(linha).Cells(1).Controls(1).Controls(1)

            If vlblBt01.Text = "" Then vbt01.Visible = False
            If vlblBt02.Text = "" Then vbt02.Visible = False
        Next

        If dtgLocalizaRelatorio.Items.Count = 0 Then
            lblObservacao_Det.Visible = True
        End If
    End Sub

    Protected Sub DesabilitaPanels()
        pnlMinhasAreas.Visible = False
        pnlSubMenuRelatorio.Visible = False
        pnlMinhasContas.Visible = False
        pnlModelo_Ativo.Visible = False
        pnlMeusAtivos.Visible = False
        'divPesquisar.Visible = False
        pnlListaTelefonica.Visible = False
    End Sub

    Protected Sub DesativaCssBotao()
        Dim navHome As HtmlGenericControl = Master.FindControl("navHome")
        Dim navMeusAtivos As HtmlGenericControl = Master.FindControl("navMeusAtivos")
        Dim navMinhasAreas As HtmlGenericControl = Master.FindControl("navMinhasAreas")
        Dim navChamado As HtmlGenericControl = Master.FindControl("navChamado")
        Dim navFerramenta As HtmlGenericControl = Master.FindControl("navFerramenta")
        Dim navSair As HtmlGenericControl = Master.FindControl("navSair")
        Dim liRelatorio As HtmlGenericControl = Master.FindControl("liRelatorio")
        Dim liImgAtivos As HtmlGenericControl = Master.FindControl("liImgAtivos")

        navHome.Attributes("class") = "nav-item"
        navMeusAtivos.Attributes("class") = "nav-item"
        navMinhasAreas.Attributes("class") = "nav-item"
        navChamado.Attributes("class") = "nav-item"
        navFerramenta.Attributes("class") = "nav-item"
        navSair.Attributes("class") = "nav-item"
        liRelatorio.Attributes("class") = "nav-item"
        liImgAtivos.Attributes("class") = "nav-item"
    End Sub

    Protected Sub btACFechar_Click(sender As Object, e As ImageClickEventArgs)
        pnlModelo_Ativo.Visible = False
    End Sub
    Protected Sub btConta1Mobile_Click(sender As Object, e As ImageClickEventArgs)

        Dim btn As ImageButton
        btn = sender

        Response.Redirect("~/Marcacao/Marcacao_Celular.aspx" + btn.AlternateText.Substring(22, btn.AlternateText.Length - 22))

    End Sub
    Protected Sub btConta2Mobile_Click(sender As Object, e As ImageClickEventArgs)

        Dim btn As ImageButton
        btn = sender

        Response.Redirect("~/Marcacao/Marcacao_Celular.aspx" + btn.AlternateText.Substring(22, btn.AlternateText.Length - 22))

    End Sub

    Protected Sub ValidaPagina(ByVal page As String)

        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key02", "closeSidebar();", True)

        Call DesabilitaPanels()
        Call DesativaCssBotao()

        Dim navHome As HtmlGenericControl = Master.FindControl("navHome")
        Dim navMeusAtivos As HtmlGenericControl = Master.FindControl("navMeusAtivos")
        Dim navMinhasAreas As HtmlGenericControl = Master.FindControl("navMinhasAreas")
        Dim navChamado As HtmlGenericControl = Master.FindControl("navChamado")
        Dim navFerramenta As HtmlGenericControl = Master.FindControl("navFerramenta")
        Dim navSair As HtmlGenericControl = Master.FindControl("navSair")
        Dim liRelatorio As HtmlGenericControl = Master.FindControl("liRelatorio")
        Dim liImgAtivos As HtmlGenericControl = Master.FindControl("liImgAtivos")

        If page = "conta" Then
            navMeusAtivos.Attributes("class") = "nav-item active"
            pnlMeusAtivos.Visible = True

        ElseIf page = "indicadores" Then
            navMinhasAreas.Attributes("class") = "nav-item active"
            pnlMinhasAreas.Visible = True

        ElseIf page = "Relatorio" Or page = "facilidades" Then
            liRelatorio.Attributes("class") = "nav-item active"
            pnlSubMenuRelatorio.Visible = True

        ElseIf page = "MeusAtivos" Then
            liImgAtivos.Attributes("class") = "nav-item active"
            pnlModelo_Ativo.Visible = True

        End If

    End Sub

    ''Protected Sub btRelatorio_Click(sender As Object, e As System.EventArgs) Handles btRelatorio.Click

    ''    ValidaPagina("Relatorio")

    ''    'pnlModelo_Ativo.Visible = False
    ''    'divPesquisar.Visible = False
    ''    'pnlSubMenuRelatorio.Visible = True
    ''    'navHome.Attributes("class") = "nav-item"
    ''    'liRelatorio.Attributes("class") = "nav-item active"
    ''    'liImgAtivos.Attributes("class") = "nav-item"
    ''End Sub

    ''Protected Sub imgAtivos_Click(sender As Object, e As System.EventArgs) Handles imgAtivos.Click
    ''    ValidaPagina("Meus Ativos")
    ''End Sub

    ''Protected Sub btMeusAtivos_Click(sender As Object, e As EventArgs)
    ''    ValidaPagina("conta")
    ''End Sub

    ''Protected Sub btMinhaAreas_Click(sender As Object, e As EventArgs)
    ''    ValidaPagina("indicadores")
    ''End Sub

    ''Protected Sub btChamado_Click(sender As Object, e As EventArgs)
    ''    Call DesabilitaPanels()
    ''    Call DesativaCssBotao()
    ''    navChamado.Attributes("class") = "nav-item active"
    ''    Response.Redirect("~/Chamado/Consulta_Chamado.aspx")
    ''End Sub

    Protected Sub btPesquisar_Click(sender As Object, e As EventArgs)

        If txtPesquisaContato.Text <> "" Then

            vdataSet = WS_Cadastro.Consumidor(Session("Conn_Banco"), Nothing, oConfig.ValidaCampo(txtPesquisaContato.Text), Nothing, Nothing,
                              Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                              Nothing, Nothing, Nothing, Nothing, Nothing,
                              Nothing, "sp_SL_Lista_Telefonica_IControlIT", True)
            dtgListaTelefonica.DataSource = vdataSet
            dtgListaTelefonica.DataBind()
            pnlListaTelefonica.Visible = True
        Else

            pnlListaTelefonica.Visible = False

        End If
    End Sub

    'Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
    '    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key02", "openNav();", True)
    'End Sub

    Protected Sub btnFechaPesquisa_Click(sender As Object, e As EventArgs)
        txtPesquisaContato.Text = ""
        pnlListaTelefonica.Visible = False
    End Sub
End Class
