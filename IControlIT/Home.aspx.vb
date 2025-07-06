Public Class Home
    Inherits System.Web.UI.Page

    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim vdataSet As System.Data.DataSet
    Dim oConfig As New cls_Config

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Nm_Usuario") Is Nothing Then
            Response.Redirect("DEFAULT.aspx")
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            Page.Form.DefaultButton = btPesquisar.UniqueID
            Page.SetFocus(txtPesquisar)

            '-----cria menu de usuario
            Dim v_dataSet As New System.Data.DataSet
            Dim vCont As System.Int32 = 0
            Dim vMnPai As System.Int32 = 0

            Dim lblNome As Label = Master.FindControl("lblNome")
            Dim imgLogoCliente As Image = Master.FindControl("imgLogoCliente")
            Dim btConfiguracao As HtmlAnchor = Master.FindControl("btConfiguracao")
            Dim btEstoque As HtmlAnchor = Master.FindControl("btEstoque")
            Dim btOrcamento As HtmlAnchor = Master.FindControl("btOrcamento")
            Dim btContrato As HtmlAnchor = Master.FindControl("btContrato")
            Dim btFatura As HtmlAnchor = Master.FindControl("btFatura")
            Dim btContabil As HtmlAnchor = Master.FindControl("btContabil")
            Dim btContestacao As HtmlAnchor = Master.FindControl("btContestacao")
            Dim btCadastro As HtmlAnchor = Master.FindControl("btCadastro")
            Dim btMarcacao As HtmlAnchor = Master.FindControl("btMarcacao")
            Dim btRelatorio As HtmlAnchor = Master.FindControl("btRelatorio")
            Dim btIncidente As HtmlAnchor = Master.FindControl("btIncidente")

            '''-----identifica usuario logado
            ''v_dataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Valida_Usuario", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)

            ''lblNome.Text = v_dataSet.Tables(0).Rows(0).Item("Nm_Consumidor")
            ''imgLogoCliente.ImageUrl = v_dataSet.Tables(0).Rows(0).Item("Logo")
            '''PageBody.Style.Item("background-image") = Replace("url('" + v_dataSet.Tables(0).Rows(0).Item("Logo").ToString.Replace(".png", "_Site.png") + "')", "~/", "")

            '-----carrega sub menu
            Call Menu(1)
            Call ValidaBotao(bt01)
            Call ValidaBotaoSub(btRelatorioGeral)

            '-----busca dados menu
            '-----session para criacao do menu 
            If Session("Menu") Is Nothing Then Session("Menu") = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Menu", Session("Nm_Usuario"), Nothing, Nothing, Nothing, Session("Id_Idioma"))
            v_dataSet = Session("Menu")

            ''If v_dataSet.Tables(0).Rows.Count > 0 Then
            ''    '-----monta menu
            ''    For vCont = 0 To v_dataSet.Tables(0).Rows.Count - 1
            ''        If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 10 Then
            ''            btConfiguracao.Disabled = False
            ''            btConfiguracao.Style.Add("Opacity", "0.9")
            ''            'btSConfiguracao.Style.Add("Opacity", "0.9")
            ''        End If
            ''        'If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 5 Then
            ''        '    btFerramenta.Disabled = False
            ''        '    btFerramenta.Style.Add("Opacity", "0.9")
            ''        '    'btSFerramenta.Style.Add("Opacity", "0.9")
            ''        'End If
            ''        If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 6 Then
            ''            btEstoque.Disabled = False
            ''            btEstoque.Style.Add("Opacity", "0.9")
            ''            'btSEstoque.Style.Add("Opacity", "0.9")
            ''        End If
            ''        If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 15 Then
            ''            btOrcamento.Disabled = False
            ''            btOrcamento.Style.Add("Opacity", "0.9")
            ''            'btSOrcamento.Style.Add("Opacity", "0.9")
            ''        End If
            ''        If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 3 Then
            ''            btContrato.Disabled = False
            ''            btContrato.Style.Add("Opacity", "0.9")
            ''            'btSContrato.Style.Add("Opacity", "0.9")
            ''        End If
            ''        If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 7 Then
            ''            btFatura.Disabled = False
            ''            btFatura.Style.Add("Opacity", "0.9")
            ''            'btSFatura.Style.Add("Opacity", "0.9")
            ''        End If
            ''        If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 11 Then
            ''            btContabil.Disabled = False
            ''            btContabil.Style.Add("Opacity", "0.9")
            ''            'btSContabil.Style.Add("Opacity", "0.9")
            ''        End If
            ''        If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 4 Then
            ''            btContestacao.Disabled = False
            ''            btContestacao.Style.Add("Opacity", "0.9")
            ''            'btSContestacao.Style.Add("Opacity", "0.9")
            ''        End If
            ''        If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 8 Then
            ''            btCadastro.Disabled = False
            ''            btCadastro.Style.Add("Opacity", "0.9")
            ''            'btSCadastro.Style.Add("Opacity", "0.9")
            ''        End If
            ''        If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 14 Then
            ''            btMarcacao.Disabled = False
            ''            btMarcacao.Style.Add("Opacity", "0.9")
            ''            'btSMarcacao.Style.Add("Opacity", "0.9")
            ''        End If
            ''        If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 12 Then
            ''            btRelatorio.Disabled = False
            ''            btRelatorio.Style.Add("Opacity", "0.9")
            ''            'btSRelatorio.Style.Add("Opacity", "0.9")
            ''        End If
            ''        If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 18 Then
            ''            btIncidente.Disabled = False
            ''            btIncidente.Style.Add("Opacity", "0.9")
            ''            'btSIncidente.Style.Add("Opacity", "0.9")
            ''        End If
            ''        'If v_dataSet.Tables(0).Rows(vCont).Item("ID_Menu") = 17 Then
            ''        '    btCaixa_Entrada.Disabled = False
            ''        '    btCaixa_Entrada.Style.Add("Opacity", "0.9")
            ''        '    'btSCaixa_Entrada.Style.Add("Opacity", "0.9")
            ''        'End If
            ''    Next vCont
            ''End If

            '-----------------------------------------------------------------------------------------------------------
            '-----botoes dinamicos
            '-----------------------------------------------------------------------------------------------------------
            vdataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Valida_Botoes_Dinamicos", Nothing, Nothing, Nothing, Nothing, Nothing)

            '-----------------------------------------------------------------------------------------------------------
            '-----valida no caso dos fornecedores entrando no sistema
            '-----------------------------------------------------------------------------------------------------------
            If v_dataSet.Tables(0).Rows.Count > 0 Then
                If v_dataSet.Tables(0).Rows.Count = 1 And v_dataSet.Tables(0).Rows(0).Item("ID_Menu") = 18 Then
                    Response.Redirect("~/Chamado/Consulta_Chamado.aspx")
                End If
            End If

            Dim i As System.Int32 = 0
            For i = 0 To vdataSet.Tables(0).Rows.Count - 1
                If vdataSet.Tables(0).Rows(i).Item("Descricao") = "Gestao" Then
                    If btContabil.Disabled = False Then
                        If vdataSet.Tables(0).Rows(i).Item("QTD") = 0 Then lblDescricaoGestao.Text = "Ativo"
                        If vdataSet.Tables(0).Rows(i).Item("QTD") > 0 Then
                            lblDescricaoGestao.Text = vdataSet.Tables(0).Rows(i).Item("QTD")
                            lblDescricaoGestaoSub.Text = "Ativo vago"
                            'btGestao.ImageUrl = "~/Img_Sistema/Master/Alerta.png"
                            divGestao.Style.Add("background-color", "#FF4949")
                            iconeGestao.Attributes("class") = "fas fa-bell"
                        End If
                        btGestaoFUll.Enabled = True
                        btGestao.Enabled = True
                        btGestao.Style.Add("Opacity", "0.9")
                        lblDescricaoGestao.Style.Add("Opacity", "0.9")
                        lblDescricaoGestaoSub.Style.Add("Opacity", "0.9")
                        divMenuPesquisa.Visible = True
                        'tbCab.Visible = True
                    End If
                End If

                If vdataSet.Tables(0).Rows(i).Item("Descricao") = "Custo_Cancelada" Then
                    If vdataSet.Tables(0).Rows(i).Item("QTD") = 0 Then lblDescricaoRH.Text = "Cancelamento"
                    If vdataSet.Tables(0).Rows(i).Item("QTD") > 0 Then
                        lblDescricaoRH.Text = vdataSet.Tables(0).Rows(i).Item("QTD")
                        lblDescricaoRHSub.Text = "Ativos Canc. c/Custo"
                        divRH.Style.Add("background-color", "#FF4949")
                        iconeRH.Attributes("class") = "fas fa-bell"
                    End If
                    imgRHFull.Enabled = True
                    imgRH.Enabled = True
                    imgRH.Style.Add("Opacity", "0.9")
                    lblDescricaoRH.Style.Add("Opacity", "0.9")
                    lblDescricaoRHSub.Style.Add("Opacity", "0.9")
                End If

                If vdataSet.Tables(0).Rows(i).Item("Descricao") = "Custo_Estoque" Then
                    If btContabil.Disabled = False Then
                        If vdataSet.Tables(0).Rows(i).Item("QTD") = 0 Then lblDescricaoCarga.Text = "Estoque"
                        If vdataSet.Tables(0).Rows(i).Item("QTD") > 0 Then
                            lblDescricaoCarga.Text = vdataSet.Tables(0).Rows(i).Item("QTD")
                            lblDescricaoCargaSub.Text = "Estoque c/Custo"
                            'imgFatura.ImageUrl = "~/Img_Sistema/Master/Alerta.png"
                            divFatura.Style.Add("background-color", "#FF4949")
                            iconeFatura.Attributes("class") = "fas fa-bell"
                        End If
                        imgFaturaFull.Enabled = True
                        imgFatura.Enabled = True
                        imgFatura.Style.Add("Opacity", "0.9")
                        lblDescricaoCarga.Style.Add("Opacity", "0.9")
                        lblDescricaoCargaSub.Style.Add("Opacity", "0.9")
                    End If
                End If

                If vdataSet.Tables(0).Rows(i).Item("Descricao") = "Linha_Sem_Lote" Then
                    If btContabil.Disabled = False Then
                        If vdataSet.Tables(0).Rows(i).Item("QTD") = 0 Then lblDescricaoRateio.Text = "Lin/Conta"
                        If vdataSet.Tables(0).Rows(i).Item("QTD") > 0 Then
                            lblDescricaoRateio.Text = vdataSet.Tables(0).Rows(i).Item("QTD")
                            lblDescricaoRateioSub.Text = "Ativos s/Conta"
                            'imgRateio.ImageUrl = "~/Img_Sistema/Master/Alerta.png"
                            divRateio.Style.Add("background-color", "#FF4949")
                            iconeRateio.Attributes("class") = "fas fa-bell"
                        End If
                        imgRateioFull.Enabled = True
                        imgRateio.Enabled = True
                        imgRateio.Style.Add("Opacity", "0.9")
                        lblDescricaoRateio.Style.Add("Opacity", "0.9")
                        lblDescricaoRateioSub.Style.Add("Opacity", "0.9")
                    End If
                End If

                If vdataSet.Tables(0).Rows(i).Item("Descricao") = "Cota" Then
                    If btOrcamento.Disabled = False Then
                        If vdataSet.Tables(0).Rows(i).Item("QTD") = 0 Then lblDescricaoCota.Text = "Cotas"
                        If vdataSet.Tables(0).Rows(i).Item("QTD") > 0 Then
                            lblDescricaoCota.Text = vdataSet.Tables(0).Rows(i).Item("QTD")
                            lblDescricaoCotaSub.Text = "S/Orçamento"
                            'imgCota.ImageUrl = "~/Img_Sistema/Master/Alerta.png"
                            divCota.Style.Add("background-color", "#FF4949")
                            iconeCota.Attributes("class") = "fas fa-bell"
                        End If
                        imgCotaFull.Enabled = False
                        imgCota.Enabled = False
                        imgCota.Style.Add("Opacity", "0.9")
                        lblDescricaoCota.Style.Add("Opacity", "0.9")
                        lblDescricaoCotaSub.Style.Add("Opacity", "0.9")
                    End If
                End If

                If vdataSet.Tables(0).Rows(i).Item("Descricao") = "Contrato" Then
                    If btContrato.Disabled = False Then
                        If vdataSet.Tables(0).Rows(i).Item("QTD") = 0 Then lblDescricaoContrato.Text = "Contrato"
                        If vdataSet.Tables(0).Rows(i).Item("QTD") > 0 Then
                            lblDescricaoContrato.Text = vdataSet.Tables(0).Rows(i).Item("QTD")
                            lblDescricaoContratoSub.Text = "Contrato venc."
                            'imgContrato.ImageUrl = "~/Img_Sistema/Master/Alerta.png"
                            divContrato.Style.Add("background-color", "#FF4949")
                            iconeContrato.Attributes("class") = "fas fa-bell"
                        End If
                        imgContratoFull.Enabled = False
                        imgContrato.Enabled = False
                        imgContrato.Style.Add("Opacity", "0.9")
                        lblDescricaoContrato.Style.Add("Opacity", "0.9")
                        lblDescricaoContratoSub.Style.Add("Opacity", "0.9")
                    End If
                End If

                If vdataSet.Tables(0).Rows(i).Item("Descricao") = "Linha_Sem_Uso" Then
                    If btEstoque.Disabled = False Then
                        If vdataSet.Tables(0).Rows(i).Item("QTD") = 0 Then lblDescricaoEstoque.Text = "Sem Uso"
                        If vdataSet.Tables(0).Rows(i).Item("QTD") > 0 Then
                            lblDescricaoEstoque.Text = vdataSet.Tables(0).Rows(i).Item("QTD")
                            lblDescricaoEstoqueSub.Text = "Ativos s/Uso"
                            'imgEstoque.ImageUrl = "~/Img_Sistema/Master/Alerta.png"
                            divEstoque.Style.Add("background-color", "#FF4949")
                            iconeEstoque.Attributes("class") = "fas fa-bell"
                        End If
                        imgEstoqueFull.Enabled = True
                        imgEstoque.Enabled = True
                        imgEstoque.Style.Add("Opacity", "0.9")
                        lblDescricaoEstoque.Style.Add("Opacity", "0.9")
                        lblDescricaoEstoqueSub.Style.Add("Opacity", "0.9")
                    End If
                End If

                If vdataSet.Tables(0).Rows(i).Item("Descricao") = "Auditoria" Then
                    If btContestacao.Disabled = False Then
                        If vdataSet.Tables(0).Rows(i).Item("QTD") = 0 Then
                            lblDescricaoContestacao.Text = "S/Contestação"
                            'imgContestacao.ImageUrl = "~/Img_Sistema/Master/Alerta_Contestacao.png"
                            divContestacao.Style.Add("background-color", "#FF4949")
                            iconeContestacao.Attributes("class") = "fas fa-bell"
                        Else
                            lblDescricaoContestacao.Text = vdataSet.Tables(0).Rows(i).Item("QTD") & "K"
                            lblDescricaoContestacaoSub.Text = "Recuperado"
                        End If
                        imgContestacaoFull.Enabled = False
                        imgContestacao.Enabled = False
                        imgContestacao.Style.Add("Opacity", "0.9")
                        lblDescricaoContestacao.Style.Add("Opacity", "0.9")
                        lblDescricaoContestacaoSub.Style.Add("Opacity", "0.9")
                    End If
                End If
            Next i

            '-----------------------------------------------------------------------------------------------------------
            '-----botoes relatorio
            '-----------------------------------------------------------------------------------------------------------
            lblObservacao.Visible = False
            lblObservacao_Det.Visible = False
            lblGrupo.Text = "Relatório Geral"

            If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                btAproveitamento.Enabled = True
                btVolumeCusto.Enabled = True
                btVolumeQuantidade.Enabled = True
                btDetalhamento.Enabled = True
                btEstatisticaChamado.Enabled = True
                btMonitoramentoDados.Enabled = True
            End If

            Call Filtro(9)
        End If

        If Request("Pagina") <> Nothing Then
            Call ValidaPagina(Request("Pagina"))
        End If

        Session("KPI") = Nothing
    End Sub

    Private Sub Pesquisar()
        lblMsg.Visible = False
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        Session("DataSet") = WS_Consulta.Pesquisar(Session("Conn_Banco"), "sp_Pesquisar", oConfig.ValidaCampo(txtPesquisar.Text))

        dtgLocaliza.CurrentPageIndex = Nothing
        dtgLocaliza.DataSource = Session("DataSet")
        dtgLocaliza.DataBind()

        If dtgLocaliza.Items.Count = 0 Then
            lblMsg.Visible = True
            DivPesquisa.Visible = True
        Else
            lblMsg.Visible = False
        End If
    End Sub

    'Protected Sub btFecharPesquisa_Click(sender As Object, e As ImageClickEventArgs) Handles btFecharPesquisa.Click
    '    btFecharPesquisa.Visible = False
    '    DivPesquisa.Visible = False
    '    lblMsg.Visible = False
    '    txtPesquisar.Text = ""
    'End Sub

    Protected Sub dtgLocaliza_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgLocaliza.PageIndexChanged
        dtgLocaliza.CurrentPageIndex = e.NewPageIndex
        dtgLocaliza.DataSource = Session("DataSet")
        dtgLocaliza.DataBind()
    End Sub

    Protected Sub dtgLocaliza_SelectedIndexChanged(sender As Object, e As EventArgs) 'Handles dtgLocaliza.SelectedIndexChanged
        If Not dtgLocaliza.Items(dtgLocaliza.SelectedIndex).Cells(4).Text = "null" Then
            '-----desativa/ativa registro
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Lixeira(Session("Conn_Banco"), dtgLocaliza.Items(dtgLocaliza.SelectedIndex).Cells(2).Text,
                                Session("Id_Usuario"),
                                dtgLocaliza.Items(dtgLocaliza.SelectedIndex).Cells(3).Text)

            dtgLocaliza.CurrentPageIndex = Nothing
            Call Pesquisar()
        Else
            Response.Redirect("~/" & dtgLocaliza.Items(dtgLocaliza.SelectedIndex).Cells(5).Text)
        End If
    End Sub

    Protected Sub LimpaBgBotao()

        bt01.CssClass = "btn-tab-menu-disable pull-left"
        bt02.CssClass = "btn-tab-menu-disable pull-left"
        bt03.CssClass = "btn-tab-menu-disable pull-left"
        bt04.CssClass = "btn-tab-menu-disable pull-left"
        bt05.CssClass = "btn-tab-menu-disable pull-left"
        bt06.CssClass = "btn-tab-menu-disable pull-left"
        bt07.CssClass = "btn-tab-menu-disable pull-left"
        bt08.CssClass = "btn-tab-menu-disable pull-left"
        bt09.CssClass = "btn-tab-menu-disable pull-left"
        bt10.CssClass = "btn-tab-menu-disable pull-left"

    End Sub

    Protected Sub ValidaBotao(ByVal btn As LinkButton)

        Call LimpaBgBotao()
        btn.CssClass = "btn-tab-menu pull-left"

    End Sub

    Protected Sub bt01_Click(sender As Object, e As System.EventArgs) Handles bt01.Click
        Call Menu(1)
        Call ValidaBotao(sender)
    End Sub

    Protected Sub bt02_Click(sender As Object, e As System.EventArgs) Handles bt02.Click
        Call Menu(2)
        Call ValidaBotao(sender)
    End Sub

    Protected Sub bt03_Click(sender As Object, e As System.EventArgs) Handles bt03.Click
        Call Menu(3)
        Call ValidaBotao(sender)
    End Sub

    Protected Sub bt04_Click(sender As Object, e As System.EventArgs) Handles bt04.Click
        Call Menu(4)
        Call ValidaBotao(sender)
    End Sub

    Protected Sub bt05_Click(sender As Object, e As System.EventArgs) Handles bt05.Click
        Call Menu(5)
        Call ValidaBotao(sender)
    End Sub

    Protected Sub bt06_Click(sender As Object, e As System.EventArgs) Handles bt06.Click
        Call Menu(6)
        Call ValidaBotao(sender)
    End Sub

    Protected Sub bt07_Click(sender As Object, e As System.EventArgs) Handles bt07.Click
        Call Menu(7)
        Call ValidaBotao(sender)
    End Sub

    Protected Sub bt08_Click(sender As Object, e As System.EventArgs) Handles bt08.Click
        Call Menu(9)
        Call ValidaBotao(sender)
    End Sub

    Protected Sub bt09_Click(sender As Object, e As System.EventArgs) Handles bt09.Click
        Call Menu(13)
        Call ValidaBotao(sender)
    End Sub

    Protected Sub bt10_Click(sender As Object, e As System.EventArgs) Handles bt10.Click
        Call Menu(18)
        Call ValidaBotao(sender)
    End Sub

    Private Sub SubMenu(ByVal visibilidade As System.Int16,
                   ByVal Link As System.String,
                   ByVal Nome As System.String)

        If btS01.Visible = False And visibilidade = 1 Then
            'lnMenu01.Visible = True
            btS01.Visible = True
            lblBtS01.InnerText = "  " & Nome
            btS01.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS02.Visible = False And visibilidade = 2 Then
            btS02.Visible = True
            lblBtS02.InnerText = "  " & Nome
            btS02.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS03.Visible = False And visibilidade = 3 Then
            'lnMenu02.Visible = True
            btS03.Visible = True
            lblBtS03.InnerText = "  " & Nome
            btS03.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS04.Visible = False And visibilidade = 4 Then
            btS04.Visible = True
            lblBtS04.InnerText = "  " & Nome
            btS04.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS05.Visible = False And visibilidade = 5 Then
            'lnMenu03.Visible = True
            btS05.Visible = True
            lblBtS05.InnerText = "  " & Nome
            btS05.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS06.Visible = False And visibilidade = 6 Then
            btS06.Visible = True
            lblBtS06.InnerText = "  " & Nome
            btS06.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS07.Visible = False And visibilidade = 7 Then
            'lnMenu04.Visible = True
            btS07.Visible = True
            lblBtS07.InnerText = "  " & Nome
            btS07.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS08.Visible = False And visibilidade = 8 Then
            btS08.Visible = True
            lblBtS08.InnerText = "  " & Nome
            btS08.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS09.Visible = False And visibilidade = 9 Then
            'lnMenu05.Visible = True
            btS09.Visible = True
            lblBtS09.InnerText = "  " & Nome
            btS09.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS10.Visible = False And visibilidade = 10 Then
            btS10.Visible = True
            lblBtS10.InnerText = "  " & Nome
            btS10.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS11.Visible = False And visibilidade = 11 Then
            'lnMenu06.Visible = True
            btS11.Visible = True
            lblBtS11.InnerText = "  " & Nome
            btS11.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS12.Visible = False And visibilidade = 12 Then
            btS12.Visible = True
            lblBtS12.InnerText = "  " & Nome
            btS12.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS13.Visible = False And visibilidade = 13 Then
            'lnMenu07.Visible = True
            btS13.Visible = True
            lblBtS13.InnerText = "  " & Nome
            btS13.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS14.Visible = False And visibilidade = 14 Then
            btS14.Visible = True
            lblBtS14.InnerText = "  " & Nome
            btS14.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS15.Visible = False And visibilidade = 15 Then
            'lnMenu08.Visible = True
            btS15.Visible = True
            lblBtS15.InnerText = "  " & Nome
            btS15.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS16.Visible = False And visibilidade = 16 Then
            btS16.Visible = True
            lblBtS16.InnerText = "  " & Nome
            btS16.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS17.Visible = False And visibilidade = 17 Then
            'lnMenu09.Visible = True
            btS17.Visible = True
            lblBtS17.InnerText = "  " & Nome
            btS17.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS18.Visible = False And visibilidade = 18 Then
            btS18.Visible = True
            lblBtS18.InnerText = "  " & Nome
            btS18.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS19.Visible = False And visibilidade = 19 Then
            'lnMenu10.Visible = True
            btS19.Visible = True
            lblBtS19.InnerText = "  " & Nome
            btS19.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If

        If btS20.Visible = False And visibilidade = 20 Then
            btS20.Visible = True
            lblBtS20.InnerText = "  " & Nome
            btS20.PostBackUrl = "~/" & Link
            visibilidade = 1
        End If
    End Sub

    Private Sub Menu(pMenu As System.Int16)
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vdataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Sub_Menu", Session("Nm_Usuario"), pMenu, Nothing, Nothing, Session("Id_Idioma"))
        Dim visibilidade As System.Int16 = 0
        btS01.Visible = False
        btS02.Visible = False
        btS03.Visible = False
        btS04.Visible = False
        btS05.Visible = False
        btS06.Visible = False
        btS07.Visible = False
        btS08.Visible = False
        btS09.Visible = False
        btS10.Visible = False
        btS11.Visible = False
        btS12.Visible = False
        btS13.Visible = False
        btS14.Visible = False
        btS15.Visible = False
        btS16.Visible = False
        btS17.Visible = False
        btS18.Visible = False
        btS19.Visible = False
        btS20.Visible = False
        'lnMenu01.Visible = False
        'lnMenu02.Visible = False
        'lnMenu03.Visible = False
        'lnMenu04.Visible = False
        'lnMenu05.Visible = False
        'lnMenu06.Visible = False
        'lnMenu07.Visible = False
        'lnMenu08.Visible = False
        'lnMenu09.Visible = False
        'lnMenu10.Visible = False

        If pMenu = 1 Then lblDefinicoes.Text = "Menu de Ativo"
        If pMenu = 2 Then lblDefinicoes.Text = "Menu de Usuário"
        If pMenu = 3 Then lblDefinicoes.Text = "Menu de Contrato"
        If pMenu = 4 Then lblDefinicoes.Text = "Menu de Contestação"
        If pMenu = 5 Then lblDefinicoes.Text = "Menu de Importar e Exportar Dados"
        If pMenu = 6 Then lblDefinicoes.Text = "Menu de Estoque"
        If pMenu = 7 Then lblDefinicoes.Text = "Menu de Fatura"
        If pMenu = 9 Then lblDefinicoes.Text = "Menu de Repasse de Custo Fixo"
        If pMenu = 13 Then lblDefinicoes.Text = "Menu de Ferramenta"
        If pMenu = 18 Then lblDefinicoes.Text = "Menu de Chamado"

        If vdataSet.Tables(0).Rows.Count > 0 Then
            Dim vLDataSet As Data.DataRow
            '-----monta tree
            For Each vLDataSet In vdataSet.Tables(0).Rows
                If vLDataSet.Item("Visivel") = 1 Then
                    If Not vLDataSet.Item("Pagina") = "" Then
                        visibilidade = visibilidade + 1
                        Call SubMenu(visibilidade, vLDataSet.Item("Link"), vLDataSet.Item("Nm_Tela"))
                    End If
                End If
            Next
        End If
    End Sub

    Protected Sub btVolumeCusto_Click(sender As Object, e As System.EventArgs) Handles btVolumeCusto.Click
        Response.Redirect("~/Consulta/Volumetria_Custo.aspx")
    End Sub

    Protected Sub btVolumeQuantidade_Click(sender As Object, e As System.EventArgs) Handles btVolumeQuantidade.Click
        Response.Redirect("~/Consulta/Volumetria_Consumo.aspx")
    End Sub

    Protected Sub btAproveitamento_Click(sender As Object, e As System.EventArgs) Handles btAproveitamento.Click
        Response.Redirect("~/Consulta/Aproveitamento_Pacote.aspx")
    End Sub

    Protected Sub btDetalhamento_Click(sender As Object, e As System.EventArgs) Handles btDetalhamento.Click
        Response.Redirect("~/Consulta/Detalhamento_Bilhete.aspx")
    End Sub

    Protected Sub btEstatisticaChamado_Click(sender As Object, e As EventArgs) Handles btEstatisticaChamado.Click
        Response.Redirect("~/Chamado/Estatistica_Solicitacao.aspx")
    End Sub

    Protected Sub btMonitoramentoDados_Click(sender As Object, e As EventArgs) Handles btMonitoramentoDados.Click
        Response.Redirect("~/Consulta/App_Dados.aspx")
    End Sub

    Protected Sub LimpaBgBotaoSub()

        btRelatorioGeral.CssClass = "btn-tab-menu-disable pull-left"
        btMovel.CssClass = "btn-tab-menu-disable pull-left"
        btFixa.CssClass = "btn-tab-menu-disable pull-left"
        btCloud.CssClass = "btn-tab-menu-disable pull-left"
        btDados.CssClass = "btn-tab-menu-disable pull-left"
        btDesktop.CssClass = "btn-tab-menu-disable pull-left"
        btImpressao.CssClass = "btn-tab-menu-disable pull-left"

    End Sub

    Protected Sub ValidaBotaoSub(ByVal btn As LinkButton)

        Call LimpaBgBotaoSub()
        btn.CssClass = "btn-tab-menu pull-left"

    End Sub

    Protected Sub btRelatorioGeral_Click(sender As Object, e As System.EventArgs) Handles btRelatorioGeral.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        If Session("Id_Usuario_Perfil_Acesso") = 1 Then
            btAproveitamento.Enabled = True
            btVolumeCusto.Enabled = True
            btVolumeQuantidade.Enabled = True
        End If
        lblGrupo.Text = "Relatório Geral"
        Session("KPI") = "Geral"
        Call Filtro(9)
    End Sub

    Protected Sub btMovel_Click(sender As Object, e As System.EventArgs) Handles btMovel.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        If Session("Id_Usuario_Perfil_Acesso") = 1 Then
            btAproveitamento.Enabled = True
            btVolumeCusto.Enabled = True
            btVolumeQuantidade.Enabled = True
        End If
        lblGrupo.Text = "Telefonia Movél"
        Session("KPI") = "Telefonia_Movel"
        Call Filtro(1)
    End Sub

    Protected Sub btFixa_Click(sender As Object, e As System.EventArgs) Handles btFixa.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        If Session("Id_Usuario_Perfil_Acesso") = 1 Then
            btAproveitamento.Enabled = True
            btVolumeCusto.Enabled = True
            btVolumeQuantidade.Enabled = True
        End If
        lblGrupo.Text = "Telefonia Fixa"
        Session("KPI") = "Telefonia_Fixa"
        Call Filtro(2)
    End Sub

    Protected Sub btDados_Click(sender As Object, e As System.EventArgs) Handles btDados.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        If Session("Id_Usuario_Perfil_Acesso") = 1 Then
            btAproveitamento.Enabled = True
            btVolumeCusto.Enabled = True
            btVolumeQuantidade.Enabled = True
        End If
        lblGrupo.Text = "Link de Dados"
        Session("KPI") = "Link_Dados"
        Call Filtro(3)
    End Sub

    Protected Sub btCloud_Click(sender As Object, e As System.EventArgs) Handles btCloud.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        If Session("Id_Usuario_Perfil_Acesso") = 1 Then
            btAproveitamento.Enabled = True
            btVolumeCusto.Enabled = True
            btVolumeQuantidade.Enabled = True
        End If
        lblGrupo.Text = "Cloud"
        Session("KPI") = "Equipamento"
        Call Filtro(99)
    End Sub

    Protected Sub btImpressao_Click(sender As Object, e As System.EventArgs) Handles btImpressao.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        If Session("Id_Usuario_Perfil_Acesso") = 1 Then
            btAproveitamento.Visible = True
            btVolumeCusto.Visible = True
            btVolumeQuantidade.Visible = True
        End If
        lblGrupo.Text = "Impressão"
        Session("KPI") = "Impressora"
        Call Filtro(5)
    End Sub

    Protected Sub btDesktop_Click(sender As Object, e As System.EventArgs) Handles btDesktop.Click
        ValidaBotaoSub(sender)
        lblObservacao_Det.Visible = False
        If Session("Id_Usuario_Perfil_Acesso") = 1 Then
            btAproveitamento.Enabled = True
            btVolumeCusto.Enabled = True
            btVolumeQuantidade.Enabled = True
        End If

        lblGrupo.Text = "Equipamento"
        Session("KPI") = "Equipamento"
        Call Filtro(4)
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

    Protected Sub btConfiguracao_Click(sender As Object, e As EventArgs)
        ValidaPagina("config")
    End Sub

    Protected Sub btRelatorio_Click(sender As Object, e As EventArgs)
        ValidaPagina("relatorio")
    End Sub

    Protected Sub btCaixa_Entrada_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Caixa_Entrada/Consulta_Caixa_Entrada.aspx")
    End Sub

    Protected Sub btIncidente_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Chamado/Consulta_Chamado.aspx")
    End Sub

    Protected Sub btMarcacao_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Marcacao/Lote.aspx")
    End Sub

    Protected Sub btFerramenta_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Manutencao/Exportacao_RH.aspx")
    End Sub

    Protected Sub btOrcamento_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Politica/Politica_Consumidor.aspx")
    End Sub

    Protected Sub btEstoque_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Estoque/Estoque.aspx")
    End Sub
    Protected Sub btContabil_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Recepcao_Fatura/Rateio.aspx")
    End Sub

    Protected Sub btContestacao_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Auditoria/Auditoria_Consulta.aspx")
    End Sub

    Protected Sub btFatura_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Recepcao_Fatura/Conferencia_Fatura.aspx")
    End Sub

    Protected Sub btCadastro_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Recepcao_Fatura/Consulta_Conta.aspx")
    End Sub

    Protected Sub btContrato_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Contrato/Consulta_Contrato.aspx")
    End Sub

    Protected Sub btPesquisar_Click(sender As Object, e As EventArgs)
        If txtPesquisar.Text = "" Then
            DivPesquisa.Visible = False
            lblMsg.Visible = False
        Else
            'btFecharPesquisa.Visible = True
            DivPesquisa.Visible = True
            Call Pesquisar()
        End If
    End Sub

    Protected Sub imgContestacao_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Auditoria/Auditoria_Consulta.aspx")
    End Sub

    Protected Sub imgRH_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Manutencao/Ativo_Localiza.aspx?ID=Custo_Cancelada")
    End Sub

    Protected Sub imgFatura_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Manutencao/Ativo_Localiza.aspx?ID=Custo_Estoque")
    End Sub

    Protected Sub imgCota_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Politica/Politica_Consumidor.aspx")
    End Sub

    Protected Sub imgContrato_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Contrato/Consulta_Contrato.aspx")
    End Sub

    Protected Sub imgEstoque_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Manutencao/Ativo_Localiza.aspx?ID=Linha_Sem_Uso")
    End Sub

    Protected Sub imgRateio_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Manutencao/Ativo_Localiza.aspx?ID=Sem_lote")
    End Sub

    Protected Sub btGestao_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Manutencao/Ativo_Localiza.aspx?ID=Inventario")
    End Sub

    Protected Sub DesabilitaPanels()
        pnlSubMenu.Visible = False
        pnlSubMenuRelatorio.Visible = False
        divConteudo.Visible = False
    End Sub

    Protected Sub DesativaCssBotao()
        Dim navHome As HtmlGenericControl = Master.FindControl("navHome")
        Dim navRelatorio As HtmlGenericControl = Master.FindControl("navRelatorio")
        Dim navConfig As HtmlGenericControl = Master.FindControl("navConfig")

        navHome.Attributes("class") = "nav-item"
        navRelatorio.Attributes("class") = "nav-item"
        navConfig.Attributes("class") = "nav-item"
    End Sub

    Protected Sub ValidaPagina(ByVal page As String)

        Dim navHome As HtmlGenericControl = Master.FindControl("navHome")
        Dim navRelatorio As HtmlGenericControl = Master.FindControl("navRelatorio")
        Dim navConfig As HtmlGenericControl = Master.FindControl("navConfig")

        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key02", "closeSidebar();", True)

        Call DesabilitaPanels()
        Call DesativaCssBotao()

        If page = "home" Then
            navHome.Attributes("class") = "nav-item active"
            divConteudo.Visible = True

        ElseIf page = "relatorio" Then
            navRelatorio.Attributes("class") = "nav-item active"
            pnlSubMenuRelatorio.Visible = True

        ElseIf page = "config" Then
            navConfig.Attributes("class") = "nav-item active"
            pnlSubMenu.Visible = True

        End If

    End Sub

    Protected Sub btAbrir_Click(sender As Object, e As EventArgs) Handles dtgLocaliza.SelectedIndexChanged
        Dim Tabela = dtgLocaliza.Items(dtgLocaliza.SelectedIndex).Cells(3).Text

        Select Case Tabela
            Case "Ativo"
                vdataSet = WS_Cadastro.Ativo(Session("Conn_Banco"), dtgLocaliza.Items(dtgLocaliza.SelectedIndex).Cells(2).Text,
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                            Nothing, Nothing, Nothing, "sp_SL_ID", True, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "Consumidor"
                vdataSet = WS_Cadastro.Consumidor(Session("Conn_Banco"), dtgLocaliza.Items(dtgLocaliza.SelectedIndex).Cells(2).Text,
                                                  Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                  Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)
            Case "Centro_Custo"
                vdataSet = WS_Cadastro.Centro_Custo(Session("Conn_Banco"), dtgLocaliza.Items(dtgLocaliza.SelectedIndex).Cells(2).Text,
                                                    Nothing, Nothing, Nothing, "sp_SL_ID", True)
            Case Else
                Call dtgLocaliza_SelectedIndexChanged(sender, e)
                Exit Sub
        End Select


        If vdataSet.Tables(0).Rows(0).Item("Fl_Desativado") = 1 Then
            pnlConfirmacao.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "disableScrolling();", True)
        End If
    End Sub

    Protected Sub btCancela_Click(sender As Object, e As EventArgs) Handles btCancela.Click
        pnlConfirmacao.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Call dtgLocaliza_SelectedIndexChanged(sender, e)
        pnlConfirmacao.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
    End Sub
End Class
