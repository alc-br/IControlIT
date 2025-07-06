
Public Class Solicitacao_Unidade
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet
    Dim vDataSetChat As New Data.DataSet
    Dim T As System.Int32

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            '-----home
            If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then
                Call Master.home("usuario")
            End If

            '----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboUnidade)
            Page.Form.DefaultButton = btSalvar.UniqueID

            '-----habilita campo usuario para administrador
            If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                txtUsuario.ReadOnly = False
            Else
                txtNumeroAtivoUnidade.ReadOnly = False
                btSalvarAtivo.Text = "Salvar"
            End If

            '-----valida botão menu
            ''Call Master.SelecionaBotao("unidade")

            '-----monta modelo do ativo
            oConfig.CarregaCombo(cboUnidade, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Acesso_Consumidor_Unidade", Session("Id_Usuario"), Nothing))
            oConfig.CarregaCombo(cboTipoGrupo, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo_Grupo_Tipo", Session("Id_Usuario"), Nothing))

            '-----monta combo da criacao de ativo
            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Conglomerado_Solicitacao", Session("Id_Usuario"), Nothing))
            oConfig.CarregaCombo(cboUnidadeAtivo, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Acesso_Consumidor_Unidade", Session("Id_Usuario"), Nothing))

            txtUsuario.Text = Session("Nm_Usuario")
            txtStatus.Text = "Aberto"
            imgVD.Visible = True
            Session("DataSet") = Nothing

            If Not Request("ID") = Nothing Then
                '-----lista dados da solicitacao
                vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                                Request("ID"),
                                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                "sp_Retorno_Solicitacao",
                                                True)

                txtIncidente.Text = vdataset.Tables(0).Rows(0).Item("Id_Solicitacao")
                txtDetalhamento.Text = vdataset.Tables(0).Rows(0).Item("Nm_Solicitacao")
                txtUsuario.Text = vdataset.Tables(0).Rows(0).Item("Nm_Usuario")
                txtDataHora.Text = vdataset.Tables(0).Rows(0).Item("Dt_Solicitacao")
                txtDataVencimento.Text = vdataset.Tables(0).Rows(0).Item("Dt_Vencimento")
                txtEnecerramento.Text = vdataset.Tables(0).Rows(0).Item("Dt_Encerramento")
                txtStatus.Text = vdataset.Tables(0).Rows(0).Item("Fl_Status")
                imgVD.Visible = vdataset.Tables(0).Rows(0).Item("VD")
                imgAM.Visible = vdataset.Tables(0).Rows(0).Item("AM")
                imgVM.Visible = vdataset.Tables(0).Rows(0).Item("VM")
                lnImg.Visible = True
                imgEquipamento.ImageUrl = vdataset.Tables(0).Rows(0).Item("Photo")

                oConfig.CarregaCombo(cboTipoSubGrupo, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo_Sub_Grupo", vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo_Grupo_Tipo"), Nothing))
                oConfig.CarregaCombo(cboSolicitacao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Solicitacao_Tipo_Id_Ativo_Tipo_Unidade", vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo"), Nothing))
                oConfig.CarregaCombo(cboSolucao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Solucao_Incidente", vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo"), Nothing))
                oConfig.CarregaCombo(cboSubSolucao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Solucao_Incidente", vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo"), Nothing))

                cboSolicitacao.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Tipo")
                cboSolucao.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Solucao") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Solucao"))
                cboUnidade.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Consumidor_Unidade")
                hfdId_Solicitacao_Avaliacao.Value = vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Avaliacao")
                hdfIdSolicitacaoUnidadeProcesso.Value = vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Unidade_Processo")

                '-----combo criacao do ativo
                Dim vTipoAtivo As System.Int32 = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo")
                Dim vSolucaoAtivoTipo As System.Int32 = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo")

                cboUnidadeAtivo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Consumidor_Unidade")
                cboTipoGrupo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo_Grupo_Tipo")
                cboTipoSubGrupo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo_Sub_Grupo")

                Call Executar()
                dtgSolicitacaoItem.DataSource = Session("DataSet")
                vDataSetChat = Session("DataSet")
                dtgSolicitacaoItem.DataBind()

                Dim vImage As Image
                Dim vImageUs As Image

                '----Monta imagem da grid no chat
                For i = 0 To dtgSolicitacaoItem.Items.Count - 1
                    If vDataSetChat.Tables(0).Rows(i).Item("Adm") = "True" Then
                        vImage = dtgSolicitacaoItem.Items(i).Cells(2).Controls(1).Controls(0).Controls(3)
                        vImage.ImageUrl = "../Img_Sistema/Botao/Grid/" & vDataSetChat.Tables(0).Rows(i).Item("Publicacao")
                    Else
                        vImageUs = dtgSolicitacaoItem.Items(i).Cells(2).Controls(3).Controls(0).Controls(7)
                        vImageUs.ImageUrl = "../Img_Sistema/Botao/Grid/" & vDataSetChat.Tables(0).Rows(i).Item("Publicacao")
                    End If
                Next

                If dtgSolicitacaoItem.Items.Count > 0 Then
                    'tdChatCenter.Visible = True
                    tdChat.Visible = True
                End If

                If Session("Id_Usuario_Perfil_Acesso") = 1 Then btInformacoes.Visible = True

                If txtStatus.Text = "Aberto" Then
                    txtUsuario.ReadOnly = True
                    If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then
                        lblEncerrar.InnerText = "Cancelar"

                        If hdfIdSolicitacaoUnidadeProcesso.Value = 1 Then
                            lblFornecedor.InnerText = "Mátricula"
                        Else
                            lblFornecedor.InnerText = "N° Série"
                        End If
                    End If

                    '-----administrador cria um ativo com fornecedor e amarrado com a unidade
                    btFornecedor.Enabled = True
                    cboSolucao.Enabled = True
                    dtgSolicitacaoItem.Enabled = True
                    txtDetalhamento.Enabled = False
                    cboSolucaoAtivoTipo.Enabled = False
                    cboTipoGrupo.Enabled = False
                    cboTipoSubGrupo.Enabled = False
                    cboSolicitacao.Enabled = False
                    cboUnidade.Enabled = False
                End If

                '-----monta dados de unidade
                If cboUnidade.SelectedValue = Nothing Then Exit Sub

                vdataset = WS_Cadastro.Consumidor_Unidade(Session("Conn_Banco"), oConfig.ValidaCampo(cboUnidade.SelectedValue), Nothing, Nothing, Nothing,
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                            Nothing, Nothing, Nothing, "sp_SL_ID_Solicitacao_Unidade", True)

                If vdataset.Tables(0).Rows.Count = 0 Then Exit Sub

                hdfId_Consumidor_Unidade.Value = vdataset.Tables(0).Rows(0).Item("Id_Consumidor_Unidade")
                txtNmUnidade.Text = vdataset.Tables(0).Rows(0).Item("Nm_Unidade")
                txtCNPJ.Text = vdataset.Tables(0).Rows(0).Item("CNPJ")
                txtIE.Text = vdataset.Tables(0).Rows(0).Item("IE")
                txtDataAtivacao.Text = vdataset.Tables(0).Rows(0).Item("Data_Ativacao")
                txtObservacaoUnidade.Text = vdataset.Tables(0).Rows(0).Item("Observacao")
                txtEntregaContato.Text = vdataset.Tables(0).Rows(0).Item("Entrega_Contato")
                txtEntregaEndereco.Text = vdataset.Tables(0).Rows(0).Item("Entrega_Endereco")
                txtEntregaTelefone.Text = vdataset.Tables(0).Rows(0).Item("Entrega_Telefone")
                txtFaturamentoContato.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Contato")
                txtFaturamentoEndereco.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Endereco")
                txtFaturamentoCNPJ.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_CNPJ")
                txtFaturamentoIE.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_IE")
                txtFaturamentoEmail.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Email")
                txtFaturamentoTelefone.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Telefone")
                txtMatricula.Text = vdataset.Tables(0).Rows(0).Item("Matricula")

                '-----monta configura do texto da caixa descricao ****************************************************************
                vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                            Nothing, Nothing, Nothing, Nothing, Nothing, cboTipoGrupo.SelectedValue, cboTipoSubGrupo.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, hdfId_Consumidor_Unidade.Value, Nothing,
                                            "sp_Drop_Ativo_Tipo_Unidade",
                                            True)

                oConfig.CarregaCombo(cboSolucaoAtivoTipo, vdataset)
                oConfig.CarregaCombo(cboTipoAtivo, vdataset)
                cboTipoAtivo.SelectedValue = vTipoAtivo
                cboSolucaoAtivoTipo.SelectedValue = vSolucaoAtivoTipo

                '-----monta configura do texto da caixa descricao
                WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
                vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                                Nothing, Nothing, Nothing, Nothing, Nothing, cboSolicitacao.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                "sp_Config_Caixa_Texto",
                                                True)
                lblDetalhamento.Text = vdataset.Tables(0).Rows(0).Item("Fl_Config_Caixa_Texto")

                '-----lista dados do ativo criado pela solicitacao
                vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"), Request("ID"),
                                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                "sp_Consulta_Solicitacao_Ativo",
                                                True)

                If vdataset.Tables(0).Rows.Count > 0 Then
                    hdfIdAtivoUnidade.Value = vdataset.Tables(0).Rows(0).Item("Id_Ativo")
                    cboTipoAtivo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo")
                    cboConglomerado.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Conglomerado")
                    cboUnidadeAtivo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Consumidor_Unidade")
                    txtObservacaoAtivo.Text = vdataset.Tables(0).Rows(0).Item("Observacao")

                    If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                        btSalvarAtivo.Visible = False
                        txtNumeroAtivoUnidade.Text = vdataset.Tables(0).Rows(0).Item("Nr_Ativo")
                        txtNumeroAtivoUnidade.ReadOnly = True
                        btSalvarDadosUnidade.Visible = False
                    Else
                        oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Conglomerado_Solicitacao", Nothing, Nothing))

                        cboTipoAtivo.Enabled = False
                        cboConglomerado.Enabled = False
                        cboUnidadeAtivo.Enabled = False
                        txtObservacaoAtivo.ReadOnly = True

                        If Mid(vdataset.Tables(0).Rows(0).Item("Nr_Ativo"), 1, 2) = "RQ" Then
                            lblNumeroSerie.ForeColor = Drawing.Color.Orange
                            btSalvarAtivo.Visible = True
                            txtNumeroAtivoUnidade.ReadOnly = False
                        Else
                            btSalvarAtivo.Visible = False
                            txtNumeroAtivoUnidade.ReadOnly = True
                        End If
                    End If
                Else
                    If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                        txtNumeroAtivoUnidade.ReadOnly = True
                        cboTipoAtivo.Enabled = False
                        cboUnidadeAtivo.Enabled = False
                        cboConglomerado.Enabled = True
                        txtObservacaoAtivo.ReadOnly = False
                        lblObservacaoAtivo.ForeColor = Drawing.Color.Orange
                        lblConglomerado.ForeColor = Drawing.Color.Orange
                        btSalvarAtivo.Visible = True
                        If cboConglomerado.Items.Count > 1 Then
                            cboConglomerado.SelectedIndex = 1
                        End If
                    Else
                        txtNumeroAtivoUnidade.ReadOnly = True
                        cboTipoAtivo.Enabled = False
                        cboConglomerado.Enabled = False
                        cboUnidadeAtivo.Enabled = False
                        txtObservacaoAtivo.ReadOnly = True
                    End If
                End If
            End If

            If txtStatus.Text = "Encerrado" Then
                If txtUsuario.Text = Session("Nm_Usuario") Then
                    btAvaliacao.Visible = True
                End If

                txtChat.ReadOnly = True
                btInsere.Enabled = False

                imgVD.Visible = False
                imgAM.Visible = False
                imgVM.Visible = False
                If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then
                    lblEncerrar.InnerText = "Cancelar"
                End If
                btAvaliacao.Enabled = True
                btAvaliacao.Style.Add("Opacity", "1")

                btSalvar.Enabled = False
                btSalvar.Style.Add("Opacity", "0.3")

                btEncerrar.Enabled = False
                btEncerrar.Style.Add("Opacity", "0.3")

                cboUnidade.Enabled = False
                cboSolucao.Enabled = False
                cboSolucaoAtivoTipo.Enabled = False
                cboTipoGrupo.Enabled = False
                cboTipoSubGrupo.Enabled = False
                cboSolicitacao.Enabled = False
                dtgSolicitacaoItem.Enabled = False
                txtDetalhamento.Enabled = False
                txtUsuario.ReadOnly = True
                btSalvarAvaliacao.Visible = False
            End If
        End If
    End Sub

    Protected Sub cboAvaliacao_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboAvaliacao.SelectedIndexChanged

        If cboAvaliacao.SelectedValue = 1 Then
            ValidaEstrelas(1)
        ElseIf cboAvaliacao.SelectedValue = 2 Then
            ValidaEstrelas(2)
        ElseIf cboAvaliacao.SelectedValue = 3 Then
            ValidaEstrelas(3)
        ElseIf cboAvaliacao.SelectedValue = 4 Then
            ValidaEstrelas(4)
        ElseIf cboAvaliacao.SelectedValue = 5 Then
            ValidaEstrelas(5)
        End If

    End Sub

    Protected Sub cboUnidade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUnidade.SelectedIndexChanged
        '-----lista dados da unidade
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        If cboUnidade.SelectedValue = Nothing Then Exit Sub

        vdataset = WS_Cadastro.Consumidor_Unidade(Session("Conn_Banco"), oConfig.ValidaCampo(cboUnidade.SelectedValue), Nothing, Nothing, Nothing,
                                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                        Nothing, Nothing, Nothing, "sp_SL_ID_Solicitacao_Unidade", True)

        If vdataset.Tables(0).Rows.Count = 0 Then Exit Sub

        hdfId_Consumidor_Unidade.Value = vdataset.Tables(0).Rows(0).Item("Id_Consumidor_Unidade")
        txtNmUnidade.Text = vdataset.Tables(0).Rows(0).Item("Nm_Unidade")
        txtCNPJ.Text = vdataset.Tables(0).Rows(0).Item("CNPJ")
        txtIE.Text = vdataset.Tables(0).Rows(0).Item("IE")
        txtDataAtivacao.Text = vdataset.Tables(0).Rows(0).Item("Data_Ativacao")
        txtObservacaoUnidade.Text = vdataset.Tables(0).Rows(0).Item("Observacao")
        txtEntregaContato.Text = vdataset.Tables(0).Rows(0).Item("Entrega_Contato")
        txtEntregaEndereco.Text = vdataset.Tables(0).Rows(0).Item("Entrega_Endereco")
        txtEntregaTelefone.Text = vdataset.Tables(0).Rows(0).Item("Entrega_Telefone")
        txtFaturamentoContato.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Contato")
        txtFaturamentoEndereco.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Endereco")
        txtFaturamentoCNPJ.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_CNPJ")
        txtFaturamentoIE.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_IE")
        txtFaturamentoEmail.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Email")
        txtFaturamentoTelefone.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Telefone")
        txtMatricula.Text = vdataset.Tables(0).Rows(0).Item("Matricula")
    End Sub

    Protected Sub cboTipoGrupo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoGrupo.SelectedIndexChanged
        If cboTipoGrupo.SelectedValue = Nothing Then Exit Sub

        oConfig.CarregaCombo(cboTipoSubGrupo, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo_Sub_Grupo", cboTipoGrupo.SelectedValue, Nothing))
    End Sub

    Protected Sub cboTipoSubGrupo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoSubGrupo.SelectedIndexChanged
        If cboTipoSubGrupo.SelectedValue = Nothing Then Exit Sub

        vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                            Nothing, Nothing, Nothing, Nothing, Nothing, cboTipoGrupo.SelectedValue, cboTipoSubGrupo.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, hdfId_Consumidor_Unidade.Value, Nothing,
                                            "sp_Drop_Ativo_Tipo_Unidade",
                                            True)

        oConfig.CarregaCombo(cboSolucaoAtivoTipo, vdataset)
    End Sub

    Protected Sub cboSolucaoAtivoTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSolucaoAtivoTipo.SelectedIndexChanged
        If cboSolucaoAtivoTipo.SelectedValue = Nothing Then Exit Sub

        '-----monta modelo do ativo
        oConfig.CarregaCombo(cboSolicitacao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Solicitacao_Tipo_Id_Ativo_Tipo_Unidade", cboSolucaoAtivoTipo.SelectedValue, Nothing))

        imgEquipamento.ImageUrl = WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Iamgem_Ativo_Tipo", cboSolucaoAtivoTipo.SelectedValue, Nothing).Tables(0).Rows(0).Item("Descricao")

        '-----trocar retorno da imagem por null
        If imgEquipamento.ImageUrl <> "~/Img_Sistema/imgDefault.png" Then
            lnImg.Visible = True
        Else
            lnImg.Visible = False
        End If
    End Sub

    Protected Sub cboSolicitacao_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSolicitacao.SelectedIndexChanged
        '-----monta configura do texto da caixa descricao
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                        Nothing, Nothing, Nothing, Nothing, Nothing, cboSolicitacao.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        "sp_Config_Caixa_Texto",
                                        True)
        lblDetalhamento.Text = vdataset.Tables(0).Rows(0).Item("Fl_Config_Caixa_Texto")
    End Sub

    Protected Sub BtPublicaSim_Click(sender As Object, e As System.EventArgs) Handles BtPublicaSim.Click
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                    oConfig.ValidaCampo(txtIncidente.Text),
                                    Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing, Nothing,
                                    22, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Session("Id_Usuario"),
                                    "sp_Grava_Item_Solicitacao",
                                    False).Tables(0).Rows(0).Item(0)
        '-----refresh de consulta
        Call Executar()
        dtgSolicitacaoItem.DataSource = Session("DataSet")
        vDataSetChat = Session("DataSet")
        dtgSolicitacaoItem.DataBind()

        Call SalvarPublicacao()
        pnlPublica.Visible = False
    End Sub

    Protected Sub BtPublicaNao_Click(sender As Object, e As System.EventArgs) Handles BtPublicaNao.Click
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                    oConfig.ValidaCampo(txtIncidente.Text),
                                    Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing, Nothing,
                                    1, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Session("Id_Usuario"),
                                    "sp_Grava_Item_Solicitacao",
                                    False).Tables(0).Rows(0).Item(0)
        '-----refresh de consulta
        Call Executar()
        dtgSolicitacaoItem.DataSource = Session("DataSet")
        vDataSetChat = Session("DataSet")
        dtgSolicitacaoItem.DataBind()

        Call SalvarPublicacao()
        pnlPublica.Visible = False
    End Sub

    Public Sub Executar()
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        Session("DataSet") = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                                    oConfig.ValidaCampo(txtIncidente.Text),
                                                    Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing,
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                    "sp_Consulta_Solicitacao_Item",
                                                    True)
    End Sub

    Protected Sub btOk_Click(sender As Object, e As System.EventArgs) Handles btOk.Click
        pnlDetalhe.Visible = False
        Call Encerrar("sp_Encerra_Solicitacao")
        '-----envio de email de encerramento
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                    oConfig.ValidaCampo(txtIncidente.Text),
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    "sd_Email_Encerramento_Chamado",
                                    False).Tables(0).Rows(0).Item(0)
        pnlDetalhe.Visible = False
        pnlRegistroSalvo.Visible = True
        lblRegistroSalvo.Visible = False
        lblMsg.Visible = True
        lblMsg.Text = "Chamado Encerrado"

        '-----limpa tela
        Call Limpar()

        '-----reinicia tela
        txtUsuario.Text = Session("Nm_Usuario")
        txtStatus.Text = "Aberto"
        imgVD.Visible = True
        imgAM.Visible = False
        imgVM.Visible = False
        Session("DataSet") = Nothing
    End Sub

    Public Sub Encerrar(Encerra_Cancela As System.String)
        Dim v_lblDetalhamento As Label


        If vDataSetChat.Tables.Count > 0 Then

            For i = 0 To dtgSolicitacaoItem.Items.Count - 1

                If vDataSetChat.Tables(0).Rows(i).Item("Adm") = "True" Then
                    v_lblDetalhamento = dtgSolicitacaoItem.Items(i).Cells(2).Controls(1).Controls(0).Controls(1)
                Else
                    v_lblDetalhamento = dtgSolicitacaoItem.Items(i).Cells(2).Controls(3).Controls(0).Controls(1)
                End If

                If v_lblDetalhamento.Text = "" Then
                    pnlRegistroSalvo.Visible = True
                    lblRegistroSalvo.Visible = False
                    lblMsg.Visible = True
                    lblMsg.Text = "*"
                    Exit Sub
                End If
            Next i

        End If

        If Not txtIncidente.Text = "" Then
            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIncidente.Text),
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        oConfig.ValidaCampo(txtObservacao.Text),
                                        Nothing, Nothing, Nothing,
                                        IIf(cboSolucao.SelectedValue = Nothing, oConfig.ValidaCampo(cboSubSolucao.SelectedValue), oConfig.ValidaCampo(cboSolucao.SelectedValue)),
                                        Session("Id_Usuario"),
                                        Encerra_Cancela,
                                        False).Tables(0).Rows(0).Item(0)
        End If
    End Sub

    Protected Sub SalvarPublicacao()
        '-----registro salvo
        lblRegistroSalvo.Visible = False
        lblMsg.Visible = False
        pnlRegistroSalvo.Visible = True

        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----valida data em branco
        If Trim(txtDetalhamento.Text) = "" Then Exit Sub

        If txtIncidente.Text = "" Then
            txtIncidente.Text = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                                        Nothing,
                                                        oConfig.ValidaCampo(txtDetalhamento.Text),
                                                        Session("Id_Usuario"),
                                                        oConfig.ValidaCampo(txtUsuario.Text),
                                                        oConfig.ValidaCampo(cboSolucaoAtivoTipo.SelectedValue),
                                                        oConfig.ValidaCampo(cboSolicitacao.SelectedValue),
                                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                        oConfig.ValidaCampo(cboUnidade.SelectedValue),
                                                        Session("Id_Usuario"),
                                                        "sp_Grava_Solicitacao",
                                                        False).Tables(0).Rows(0).Item(0)

            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIncidente.Text),
                                        Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing,
                                        oConfig.ValidaCampo(txtDetalhamento.Text),
                                        22, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Session("Id_Usuario"),
                                        "sp_Grava_Item_Solicitacao",
                                        False).Tables(0).Rows(0).Item(0)
            '-----refresh de consulta
            Call Executar()
            txtDataHora.Text = Date.Today.Date

            '-----envio de email de tratativa
            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIncidente.Text),
                                        Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        "sd_Email_Chamado",
                                        False).Tables(0).Rows(0).Item(0)

            Call Executar()
            dtgSolicitacaoItem.DataSource = Session("DataSet")
            vDataSetChat = Session("DataSet")
            dtgSolicitacaoItem.DataBind()
        Else '--------------------------------------------------------------------------------------------------------
            Dim v_lblDetalhamento As Label

            For i = 0 To dtgSolicitacaoItem.Items.Count - 1

                If vDataSetChat.Tables(0).Rows(i).Item("Adm") = "True" Then
                    v_lblDetalhamento = dtgSolicitacaoItem.Items(i).Cells(2).Controls(1).Controls(0).Controls(1)
                Else
                    v_lblDetalhamento = dtgSolicitacaoItem.Items(i).Cells(2).Controls(3).Controls(0).Controls(1)
                End If

                T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                            oConfig.ValidaCampo(txtIncidente.Text),
                                            Nothing, Nothing, Nothing, Nothing, Nothing,
                                            dtgSolicitacaoItem.Items(i).Cells(1).Text,
                                            oConfig.ValidaCampo(IIf(v_lblDetalhamento.Text = "", IIf(txtChat.Text = "", txtDetalhamento.Text, txtChat.Text), v_lblDetalhamento.Text)),
                                            Nothing, Nothing, Nothing, Nothing,
                                            oConfig.ValidaCampo(cboSolucao.SelectedValue),
                                            Nothing,
                                            Session("Id_Usuario"),
                                            "sp_Atualiza_Item_Solicitacao",
                                            False).Tables(0).Rows(0).Item(0)
            Next i

            '-----envio de email de tratativa
            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIncidente.Text),
                                        Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        "sd_Email_Chamado",
                                        False).Tables(0).Rows(0).Item(0)

            Call Executar()
            dtgSolicitacaoItem.DataSource = Session("DataSet")
            vDataSetChat = Session("DataSet")
            dtgSolicitacaoItem.DataBind()
        End If

        '-----salva politica de uso
        pnlRegistroSalvo.Visible = True
        lblRegistroSalvo.Visible = True
        txtUsuario.ReadOnly = True
        If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then lblEncerrar.InnerText = "Cancelar"
        cboSolucao.Enabled = True
        dtgSolicitacaoItem.Enabled = False
        txtDetalhamento.Enabled = False
        cboSolucaoAtivoTipo.Enabled = False
        cboTipoGrupo.Enabled = False
        cboTipoSubGrupo.Enabled = False
        cboSolicitacao.Enabled = False
        txtChat.Text = ""
    End Sub

    Protected Sub btEnceraSolucao_Click(sender As Object, e As EventArgs) Handles btEnceraSolucao.Click
        If imgVM.Visible = True Then
            pnlSolucao.Visible = False
            pnlDetalhe.Visible = True
            Exit Sub
        End If

        Call Encerrar("sp_Encerra_Solicitacao")
        '-----envio de email de encerramento
        T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                    oConfig.ValidaCampo(txtIncidente.Text),
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    "sd_Email_Encerramento_Chamado",
                                    False).Tables(0).Rows(0).Item(0)
        pnlSolucao.Visible = False
        pnlRegistroSalvo.Visible = True
        lblRegistroSalvo.Visible = False
        lblMsg.Visible = True
        lblMsg.Text = "Chamado Encerrado"

        '-----limpa tela
        Call Limpar()

        '-----reinicia tela
        txtUsuario.Text = Session("Nm_Usuario")
        txtStatus.Text = "Aberto"
        imgVD.Visible = True
        imgAM.Visible = False
        imgVM.Visible = False
        Session("DataSet") = Nothing
    End Sub

    Protected Sub btSalvarAvaliacao_Click(sender As Object, e As EventArgs) Handles btSalvarAvaliacao.Click
        If Not cboAvaliacao.SelectedValue = Nothing Then
            vdataset = WS_Modulo.Solicitacao_Avaliacao(Session("Conn_Banco"),
                                                       oConfig.ValidaCampo(txtIncidente.Text),
                                                       Nothing,
                                                       Nothing,
                                                       oConfig.ValidaCampo(cboAvaliacao.SelectedValue),
                                                       oConfig.ValidaCampo(txtComentario.Text),
                                                       Nothing,
                                                       "sp_SM",
                                                       True)
            ValidaEstrelas(0)
            txtComentario.Text = ""
            pnlAvaliacao.Visible = False
        End If
    End Sub

    Protected Sub btFecharSolucao_Click(sender As Object, e As EventArgs) Handles btFecharSolucao.Click
        pnlSolucao.Visible = False
    End Sub

    Protected Sub btCancela_Click(sender As Object, e As EventArgs) Handles btCancela.Click
        pnlDetalhe.Visible = False
    End Sub

    Protected Sub btFecharAvaliacao_Click(sender As Object, e As EventArgs) Handles btFecharAvaliacao.Click
        ValidaEstrelas(0)
        txtComentario.Text = ""
        pnlAvaliacao.Visible = False
    End Sub

    Protected Sub btFechar_Registro_Click(sender As Object, e As EventArgs) Handles btFechar_Registro.Click
        pnlRegistroSalvo.Visible = False
    End Sub

    Protected Sub btFechar_Informacoes_Click(sender As Object, e As EventArgs) Handles btFechar_Informacoes.Click
        pnlInformacao.Visible = False
    End Sub

    Protected Sub btSairDadosUnidade_Click(sender As Object, e As EventArgs) Handles btSairDadosUnidade.Click
        pnlDadosUnidade.Visible = False
        divTela.Visible = True
        tbBotao.Visible = True
    End Sub

    Protected Sub Fornecedor()
        vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                              Request("ID"),
                              Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                              "sp_Consulta_Solicitacao_Ativo",
                              True)

        '-----trata solicitacao de matricula
        '-------------------------------------------------------------------------------------------
        If hdfIdSolicitacaoUnidadeProcesso.Value = 1 Then
            If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                lblNumeroSerie.ForeColor = Drawing.Color.Orange
                txtNumeroAtivoUnidade.ReadOnly = False
            End If
            lblNumeroSerie.Text = "* Matrícula"
            lblAtivoSolicitacao.Text = "Criação da Matrícula"
        End If

        '-----insere ativo
        '-------------------------------------------------------------------------------------------
        If hdfIdSolicitacaoUnidadeProcesso.Value = 0 Then
            lblNumeroSerie.Text = "* Número de Série"
            lblAtivoSolicitacao.Text = "Criação do Ativo"
        End If

        '-----trata solcitacao de devolucao
        '-------------------------------------------------------------------------------------------
        If hdfIdSolicitacaoUnidadeProcesso.Value = 2 Then
            lblNumeroSerie.Text = "* Número de Série"
            lblAtivoSolicitacao.Text = "Devolução de Ativo"
            lblNumeroSerie.ForeColor = Drawing.Color.Orange
            txtNumeroAtivoUnidade.ReadOnly = False
            btSalvarAtivo.Text = "Salvar"

            lblTipoAtivo.Visible = False
            cboTipoAtivo.Visible = False
            lblConglomerado.Visible = True
            cboConglomerado.Visible = True
            lblUnidadeAtivo.Visible = False
            cboUnidadeAtivo.Visible = False
            lblObservacaoAtivo.Visible = False
            txtObservacaoAtivo.Visible = False
        End If

        '-------------------------------------------------------------------------------------------

        If vdataset.Tables(0).Rows.Count > 0 Then
            hdfIdAtivoUnidade.Value = vdataset.Tables(0).Rows(0).Item("Id_Ativo")
            cboTipoAtivo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo")
            cboConglomerado.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Conglomerado")
            cboUnidadeAtivo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Consumidor_Unidade")
            txtObservacaoAtivo.Text = vdataset.Tables(0).Rows(0).Item("Observacao")

            '-----abre pdf para o usuario vincular a nf
            If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then
                tbPDF.Visible = True
                btPDF.OnClientClick = "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & hdfIdAtivoUnidade.Value & "&pTabela=Ativo','','resizable=yes, menubar=yes, scrollbars=no,height=768px, width=1024px, top=10, left=10')"
            Else
                txtNumeroAtivoUnidade.Text = vdataset.Tables(0).Rows(0).Item("Nr_Ativo")
                txtNumeroAtivoUnidade.ReadOnly = True
            End If
        End If

        pnlCriaAtivo.Visible = True
    End Sub

    Protected Sub BtFecharAtivo_Click(sender As Object, e As EventArgs) Handles btFecharAtivo.Click
        pnlCriaAtivo.Visible = False
    End Sub

    Protected Sub BtSalvarAtivo_Click(sender As Object, e As EventArgs) Handles btSalvarAtivo.Click
        '-----verifica se colocou observacao
        If cboTipoAtivo.SelectedValue = Nothing Then Exit Sub
        If cboUnidadeAtivo.SelectedValue = Nothing Then Exit Sub

        Dim vMSGSolicitcao_Item As System.String = ""

        If Session("Id_Usuario_Perfil_Acesso") = 1 Then
            '-----nao insere registro quando descricao so for numerica
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            '-----trata matricula da coca cola
            '-------------------------------------------------------------------------------------------
            If hdfIdSolicitacaoUnidadeProcesso.Value = 1 Then
                WS_Cadastro.Consumidor_Unidade(Session("Conn_Banco"),
                                        hdfId_Consumidor_Unidade.Value,
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        oConfig.ValidaCampo(txtNumeroAtivoUnidade.Text),
                                        Nothing, Nothing,
                                        Session("Id_Usuario"),
                                        "sp_SM_Solicitacao_Unidade_Matricula",
                                        False)
                vMSGSolicitcao_Item = "Matrícula inseriada com sucesso"

                '-----encerra solicitacao
                Call Encerrar("sp_Encerra_Solicitacao_NS_Unidade")
                '-----envio de email de encerramento
                T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                oConfig.ValidaCampo(txtIncidente.Text),
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    "sd_Email_Encerramento_Chamado",
                                    False).Tables(0).Rows(0).Item(0)
            End If

            '-----insere ativo
            '-------------------------------------------------------------------------------------------
            If hdfIdSolicitacaoUnidadeProcesso.Value = 0 Then
                If cboConglomerado.SelectedValue = Nothing Then Exit Sub

                hdfIdAtivoUnidade.Value = WS_Cadastro.Ativo(Session("Conn_Banco"),
                                        Nothing, oConfig.ValidaCampo(txtIncidente.Text), Nothing,
                                        oConfig.ValidaCampo(cboTipoAtivo.SelectedValue),
                                        oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                        Nothing, Nothing, Nothing,
                                        oConfig.ValidaCampo(Mid(txtObservacaoAtivo.Text, 1, 8000) + " | Usuário - " + Session("Nm_Usuario")),
                                        Nothing, Nothing,
                                        oConfig.ValidaCampo(cboUnidadeAtivo.SelectedValue),
                                        Session("Id_Usuario"),
                                        "sd_Cria_Ativo_Solicitacao",
                                        False, Nothing, Nothing, Nothing, Nothing, Nothing).Tables(0).Rows(0).Item(0)

                vMSGSolicitcao_Item = "Equipamento encaminhado para entrega pelo forncedor"

                WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                    Nothing, Nothing, Nothing, Nothing,
                                    hdfIdAtivoUnidade.Value, Nothing,
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    oConfig.ValidaCampo(cboConglomerado.SelectedValue), Nothing,
                                    "sp_Lote_Solicitacao_Unidade",
                                    True)
            End If

            '-----trata devolucao
            '-------------------------------------------------------------------------------------------
            If hdfIdSolicitacaoUnidadeProcesso.Value = 2 Then

                Dim vDataSetCancelamento As System.Data.DataSet
                vDataSetCancelamento = WS_Cadastro.Ativo(Session("Conn_Banco"), Nothing,
                                                        oConfig.ValidaCampo(txtNumeroAtivoUnidade.Text),
                                                        Nothing, Nothing, oConfig.ValidaCampo(cboConglomerado.SelectedValue), hdfId_Consumidor_Unidade.Value, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                        "sd_Cancela_Numero_Serie",
                                                        True, Nothing, Nothing, Nothing, Nothing, Nothing)

                If vDataSetCancelamento.Tables(0).Rows(0).Item("Validacao") = 1 Then
                    vMSGSolicitcao_Item = vDataSetCancelamento.Tables(0).Rows(0).Item("Msg")
                Else
                    vMSGSolicitcao_Item = vDataSetCancelamento.Tables(0).Rows(0).Item("Msg")
                End If

                '***********
                txtNumeroAtivoUnidade.Text = vDataSetCancelamento.Tables(0).Rows(0).Item("Msg")
                '***********

                '-----encerra solicitacao
                If vDataSetCancelamento.Tables(0).Rows(0).Item("Validacao") = 1 Then
                    Call Encerrar("sp_Encerra_Solicitacao_NS_Unidade")
                    '-----envio de email de encerramento
                    T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                        oConfig.ValidaCampo(txtIncidente.Text),
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        "sd_Email_Encerramento_Chamado",
                                        False).Tables(0).Rows(0).Item(0)
                End If
            End If

            '-------------------------------------------------------------------------------------------

            If hdfIdAtivoUnidade.Value = "0" Then
                pnlCriaAtivo.Visible = False
                pnlRegistroSalvo.Visible = True
                lblRegistroSalvo.Visible = False
                lblMsg.Visible = True
                lblMsg.Text = "Ativo já existe, verificar se o mesmo está na lixeira."
            End If

            txtNumeroAtivoUnidade.ReadOnly = True
            cboTipoAtivo.Enabled = False
            cboConglomerado.Enabled = False
            cboUnidadeAtivo.Enabled = False
            txtObservacaoAtivo.ReadOnly = True

            lblObservacaoAtivo.ForeColor = Drawing.Color.Gray
            lblConglomerado.ForeColor = Drawing.Color.Gray

            '-----cria masg e envio de email para o usuario
            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                    oConfig.ValidaCampo(txtIncidente.Text), Nothing,
                                    Session("Id_Usuario"),
                                    Nothing, Nothing, Nothing, Nothing, Nothing, 22, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Session("Id_Usuario"),
                                    "sp_Grava_Item_Solicitacao",
                                    False).Tables(0).Rows(0).Item(0)

            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                    oConfig.ValidaCampo(txtIncidente.Text),
                                    Nothing, Nothing, Nothing, Nothing, Nothing, T,
                                    vMSGSolicitcao_Item,
                                    Nothing, Nothing, Nothing, Nothing, Nothing,
                                    oConfig.ValidaCampo(cboSolucao.SelectedValue),
                                    Session("Id_Usuario"),
                                    "sp_Atualiza_Item_Solicitacao",
                                    False).Tables(0).Rows(0).Item(0)

            '-----envio de email de tratativa
            T = WS_Modulo.Solicitacao(Session("Conn_Banco"), oConfig.ValidaCampo(txtIncidente.Text),
                                    Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    "sd_Email_Chamado",
                                    False).Tables(0).Rows(0).Item(0)

            Call Executar()
            dtgSolicitacaoItem.DataSource = Session("DataSet")
            vDataSetChat = Session("DataSet")
            dtgSolicitacaoItem.DataBind()

            '-----trata processo da unidade
            If hdfIdSolicitacaoUnidadeProcesso.Value = 1 Or hdfIdSolicitacaoUnidadeProcesso.Value = 2 Then
                pnlSolucao.Visible = False
                pnlRegistroSalvo.Visible = True
                lblRegistroSalvo.Visible = False
                lblMsg.Visible = True
                lblMsg.Text = vMSGSolicitcao_Item

                '-----limpa tela
                cboSolucaoAtivoTipo.SelectedValue = Nothing
                cboTipoGrupo.SelectedValue = Nothing
                cboTipoSubGrupo.SelectedValue = Nothing
                cboSolicitacao.SelectedValue = Nothing
                cboUnidade.SelectedValue = Nothing
                txtDetalhamento.Text = ""
                cboSolucao.SelectedValue = Nothing
                txtUsuario.Text = ""
                txtDataHora.Text = ""
                txtDataVencimento.Text = ""
                txtEnecerramento.Text = ""
                txtStatus.Text = ""
                txtIncidente.Text = ""
                dtgSolicitacaoItem.DataSource = Nothing
                vDataSetChat.Clear()
                dtgSolicitacaoItem.DataBind()

                '-----reinicia tela
                txtUsuario.Text = Session("Nm_Usuario")
                txtStatus.Text = "Aberto"
                imgVD.Visible = True
                imgAM.Visible = False
                imgVM.Visible = False
                Session("DataSet") = Nothing
            End If
        Else
            hdfIdAtivoUnidade.Value = WS_Cadastro.Ativo(Session("Conn_Banco"),
                                                        hdfIdAtivoUnidade.Value,
                                                        oConfig.ValidaCampo(txtNumeroAtivoUnidade.Text),
                                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                        Session("Id_Usuario"),
                                                        "sd_Altera_Numero_Serie",
                                                        False, Nothing, Nothing, Nothing, Nothing, Nothing).Tables(0).Rows(0).Item(0)

            If Mid(txtNumeroAtivoUnidade.Text, 1, 2) = "RQ" Then
                btSalvarAtivo.Visible = True
                txtNumeroAtivoUnidade.ReadOnly = False
            Else
                btSalvarAtivo.Visible = False
                txtNumeroAtivoUnidade.ReadOnly = True
            End If

            '-----encerra solicitacao
            '------------------------------------------------------------------------------------------------------
            Call Encerrar("sp_Encerra_Solicitacao_NS_Unidade")
            '-----envio de email de encerramento
            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
            oConfig.ValidaCampo(txtIncidente.Text),
                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                "sd_Email_Encerramento_Chamado",
                                False).Tables(0).Rows(0).Item(0)

            pnlSolucao.Visible = False
            pnlRegistroSalvo.Visible = False
            lblRegistroSalvo.Visible = False
            lblMsg.Visible = False
            'lblMsg.Text = "Chamado Encerrado"
            pnlAvaliacao.Visible = True
            cboAvaliacao.SelectedIndex = 3

            '-----limpa tela
            cboSolucaoAtivoTipo.SelectedValue = Nothing
            cboTipoGrupo.SelectedValue = Nothing
            cboTipoSubGrupo.SelectedValue = Nothing
            cboSolicitacao.SelectedValue = Nothing
            cboUnidade.SelectedValue = Nothing
            txtDetalhamento.Text = ""
            cboSolucao.SelectedValue = Nothing
            txtUsuario.Text = ""
            txtDataHora.Text = ""
            txtDataVencimento.Text = ""
            txtEnecerramento.Text = ""
            txtStatus.Text = ""
            txtIncidente.Text = ""
            dtgSolicitacaoItem.DataSource = Nothing
            vDataSetChat.Clear()
            dtgSolicitacaoItem.DataBind()

            '-----reinicia tela
            txtUsuario.Text = Session("Nm_Usuario")
            txtStatus.Text = "Aberto"
            imgVD.Visible = True
            imgAM.Visible = False
            imgVM.Visible = False
            Session("DataSet") = Nothing
        End If
        pnlCriaAtivo.Visible = False
        btSalvarAtivo.Visible = False
    End Sub

    Public Sub Limpar()
        txtIncidente.Text = ""
        txtDetalhamento.Text = ""
        txtUsuario.Text = ""
        txtDataHora.Text = ""
        txtDataVencimento.Text = ""
        txtEnecerramento.Text = ""
        txtStatus.Text = ""
        cboSolucaoAtivoTipo.SelectedValue = Nothing
        cboTipoGrupo.SelectedValue = Nothing
        cboTipoSubGrupo.SelectedValue = Nothing
        cboTipoGrupo.Items.Clear()
        cboTipoSubGrupo.Items.Clear()
        cboSolicitacao.SelectedValue = Nothing
        cboSolucao.SelectedValue = Nothing
        cboUnidade.SelectedValue = Nothing
        hfdId_Solicitacao_Avaliacao.Value = Nothing
        cboTipoAtivo.SelectedValue = Nothing
        cboUnidadeAtivo.SelectedValue = Nothing
        dtgSolicitacaoItem.DataSource = Nothing
        vDataSetChat.Clear()
        dtgSolicitacaoItem.DataBind()
        txtNmUnidade.Text = ""
        txtCNPJ.Text = ""
        txtIE.Text = ""
        txtDataAtivacao.Text = ""
        txtObservacao.Text = ""
        txtEntregaContato.Text = ""
        txtEntregaEndereco.Text = ""
        txtEntregaTelefone.Text = ""
        txtFaturamentoContato.Text = ""
        txtFaturamentoEndereco.Text = ""
        txtFaturamentoCNPJ.Text = ""
        txtFaturamentoIE.Text = ""
        txtFaturamentoEmail.Text = ""
        txtFaturamentoTelefone.Text = ""
        txtMatricula.Text = ""
        hdfIdAtivoUnidade.Value = ""
        txtNumeroAtivoUnidade.Text = ""
        cboTipoAtivo.SelectedValue = ""
        cboConglomerado.SelectedValue = ""
        cboUnidadeAtivo.SelectedValue = ""
        txtObservacaoAtivo.Text = ""
        lblObservacaoAtivo.ForeColor = Drawing.Color.Gray
        lblConglomerado.ForeColor = Drawing.Color.Gray
    End Sub

    Protected Sub btSalvarDadosUnidade_Click(sender As Object, e As EventArgs) Handles btSalvarDadosUnidade.Click
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Consumidor_Unidade(Session("Conn_Banco"),
                                        hdfId_Consumidor_Unidade.Value,
                                        Nothing,
                                        oConfig.ValidaCampo(txtNmUnidade.Text),
                                        oConfig.ValidaCampo(txtCNPJ.Text),
                                        oConfig.ValidaCampo(txtIE.Text),
                                        Nothing,
                                        oConfig.ValidaCampo(txtObservacaoUnidade.Text),
                                        oConfig.ValidaCampo(txtEntregaContato.Text),
                                        oConfig.ValidaCampo(txtEntregaEndereco.Text),
                                        oConfig.ValidaCampo(txtEntregaTelefone.Text),
                                        oConfig.ValidaCampo(txtFaturamentoContato.Text),
                                        oConfig.ValidaCampo(txtFaturamentoEndereco.Text),
                                        oConfig.ValidaCampo(txtFaturamentoCNPJ.Text),
                                        oConfig.ValidaCampo(txtFaturamentoIE.Text),
                                        oConfig.ValidaCampo(txtFaturamentoEmail.Text),
                                        oConfig.ValidaCampo(txtFaturamentoTelefone.Text),
                                        oConfig.ValidaCampo(txtMatricula.Text),
                                        Nothing, Nothing,
                                        Session("Id_Usuario"),
                                        "sp_SM_Solicitacao_Unidade",
                                        False)
        '-----registro salvo
        pnlDadosUnidade.Visible = False
    End Sub

    Protected Sub btnFecharMatricula_Click(sender As Object, e As EventArgs)
        pnlSolicitacaoMatricula.Visible = False
    End Sub

    Protected Sub btnFecharDevolucao_Click(sender As Object, e As EventArgs)
        pnlSolicitacaoDevolucao.Visible = False
    End Sub

    Protected Sub btPdfDadosUnidade_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & hdfId_Consumidor_Unidade.Value & "&pTabela=Consumidor_Unidade','_blank','resizable=yes, menubar=yes, scrollbars=no, height=700, width=1200, top=0, left=0');", True)
    End Sub

    Protected Sub btExportarTela_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "keyPrintPDF", "window.print();", True)
    End Sub

    Protected Sub btRelatorio_Click(sender As Object, e As ImageClickEventArgs) Handles btRelatorio.Click
        tbBotao.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "keyPrintPDF", "window.print();", True)
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        '-----registro salvo
        lblRegistroSalvo.Visible = False
        lblMsg.Visible = False
        pnlRegistroSalvo.Visible = True

        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----valida data em branco
        If Trim(txtDetalhamento.Text) = "" Then Exit Sub

        If txtIncidente.Text = "" Then
            txtIncidente.Text = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                                        Nothing,
                                                        oConfig.ValidaCampo(txtDetalhamento.Text),
                                                        Session("Id_Usuario"),
                                                        oConfig.ValidaCampo(txtUsuario.Text),
                                                        oConfig.ValidaCampo(cboSolucaoAtivoTipo.SelectedValue),
                                                        oConfig.ValidaCampo(cboSolicitacao.SelectedValue),
                                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                        oConfig.ValidaCampo(cboUnidade.SelectedValue),
                                                        Session("Id_Usuario"),
                                                        "sp_Grava_Solicitacao",
                                                        False).Tables(0).Rows(0).Item(0)

            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIncidente.Text),
                                        Nothing,
                                        Session("Id_Usuario"),
                                        Nothing, Nothing, Nothing, Nothing,
                                        oConfig.ValidaCampo(txtDetalhamento.Text),
                                        22,
                                        Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Session("Id_Usuario"),
                                        "sp_Grava_Item_Solicitacao",
                                        False).Tables(0).Rows(0).Item(0)
            '-----refresh de consulta
            Call Executar()
            txtDataHora.Text = Date.Today.Date

            '-----envio de email de tratativa
            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIncidente.Text),
                                        Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        "sd_Email_Chamado",
                                        False).Tables(0).Rows(0).Item(0)

            Call Executar()
            dtgSolicitacaoItem.DataSource = Session("DataSet")
            vDataSetChat = Session("DataSet")
            dtgSolicitacaoItem.DataBind()
        Else '--------------------------------------------------------------------------------------------------------
            Dim v_lblDetalhamento As Label

            For i = 0 To dtgSolicitacaoItem.Items.Count - 1

                If vDataSetChat.Tables(0).Rows(i).Item("Adm") = "True" Then
                    v_lblDetalhamento = dtgSolicitacaoItem.Items(i).Cells(2).Controls(1).Controls(0).Controls(1)
                Else
                    v_lblDetalhamento = dtgSolicitacaoItem.Items(i).Cells(2).Controls(3).Controls(0).Controls(1)
                End If

                T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                            oConfig.ValidaCampo(txtIncidente.Text),
                                            Nothing, Nothing, Nothing, Nothing, Nothing,
                                            dtgSolicitacaoItem.Items(i).Cells(1).Text,
                                            oConfig.ValidaCampo(IIf(v_lblDetalhamento.Text = "", IIf(txtChat.Text = "", txtDetalhamento.Text, txtChat.Text), v_lblDetalhamento.Text)),
                                            Nothing, Nothing, Nothing, Nothing,
                                            oConfig.ValidaCampo(cboSolucao.SelectedValue),
                                            Nothing,
                                            Session("Id_Usuario"),
                                            "sp_Atualiza_Item_Solicitacao",
                                            False).Tables(0).Rows(0).Item(0)
            Next i

            '-----envio de email de tratativa
            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIncidente.Text),
                                        Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        "sd_Email_Chamado",
                                        False).Tables(0).Rows(0).Item(0)

            Call Executar()
            dtgSolicitacaoItem.DataSource = Session("DataSet")
            vDataSetChat = Session("DataSet")
            dtgSolicitacaoItem.DataBind()
        End If

        '-----salva politica de uso
        pnlRegistroSalvo.Visible = True
        lblRegistroSalvo.Visible = True
        txtUsuario.ReadOnly = True
        If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then lblEncerrar.InnerText = "Cancelar"
        cboSolucao.Enabled = True
        dtgSolicitacaoItem.Enabled = False
        txtDetalhamento.Enabled = False
        cboSolucaoAtivoTipo.Enabled = False
        cboTipoGrupo.Enabled = False
        cboTipoSubGrupo.Enabled = False
        cboSolicitacao.Enabled = False
    End Sub
    Protected Sub btEncerrar_Click(sender As Object, e As EventArgs)
        If lblEncerrar.InnerText = "Cancelar" Then
            Call Encerrar("sp_Encerra_Solicitacao_Pelo_Usuario")

            pnlSolucao.Visible = False
            pnlRegistroSalvo.Visible = True
            lblRegistroSalvo.Visible = False
            lblMsg.Visible = True
            lblMsg.Text = "Chamado Cancelado"

            '-----limpa tela
            Call Limpar()

            '-----reinicia tela
            btEncerrar.Enabled = False
            btEncerrar.Style.Add("Opacity", "0.3")
            txtUsuario.Text = Session("Nm_Usuario")
            txtStatus.Text = "Aberto"
            imgVD.Visible = True
            imgAM.Visible = False
            imgVM.Visible = False
            Session("DataSet") = Nothing
        Else
            If cboSolucao.SelectedValue = Nothing Then
                pnlSolucao.Visible = True
            Else
                If imgVM.Visible = True Then
                    pnlDetalhe.Visible = True
                    Exit Sub
                End If

                Call Encerrar("sp_Encerra_Solicitacao")
                '-----envio de email de encerramento
                T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                            oConfig.ValidaCampo(txtIncidente.Text),
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                            "sd_Email_Encerramento_Chamado",
                                            False).Tables(0).Rows(0).Item(0)
                pnlSolucao.Visible = False
                pnlRegistroSalvo.Visible = True
                lblRegistroSalvo.Visible = False
                lblMsg.Visible = True
                lblMsg.Text = "Chamado Encerrado"

                '-----limpa tela
                Call Limpar()

                '-----reinicia tela
                btEncerrar.Enabled = False
                btEncerrar.Style.Add("Opacity", "0.3")
                txtUsuario.Text = Session("Nm_Usuario")
                txtStatus.Text = "Aberto"
                imgVD.Visible = True
                imgAM.Visible = False
                imgVM.Visible = False
                Session("DataSet") = Nothing
            End If
        End If
    End Sub

    Protected Sub btnInformacao_Click(sender As Object, e As EventArgs)
        divInformacao.Visible = True
        divChat.Visible = False
        btnInformacao.CssClass = "btn-tab pull-left"
        btnChat.CssClass = "btn-tab-disable pull-left"
    End Sub

    Protected Sub btnChat_Click(sender As Object, e As EventArgs)
        divInformacao.Visible = False
        divChat.Visible = True
        btnInformacao.CssClass = "btn-tab-disable pull-left"
        btnChat.CssClass = "btn-tab pull-left"
    End Sub

    Protected Sub btInsere_Click(sender As Object, e As EventArgs)
        If txtChat.Text <> "" Then
            If txtUsuario.Text = Session("Nm_Usuario") Then
                WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
                T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                            oConfig.ValidaCampo(txtIncidente.Text),
                                            Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing, Nothing,
                                            22, Nothing, Nothing, Nothing, Nothing, Nothing,
                                            Session("Id_Usuario"),
                                            "sp_Grava_Item_Solicitacao",
                                            False).Tables(0).Rows(0).Item(0)
                '-----refresh de consulta
                Call Executar()
                dtgSolicitacaoItem.DataSource = Session("DataSet")
                vDataSetChat = Session("DataSet")
                dtgSolicitacaoItem.DataBind()

                Call SalvarPublicacao()
                pnlPublica.Visible = False
            Else
                pnlPublica.Visible = True
            End If
        End If
    End Sub

    Protected Sub btInformacoes_Click(sender As Object, e As EventArgs)
        pnlDadosUnidade.Visible = True
        divTela.Visible = False
        tbBotao.Visible = False
    End Sub

    Protected Sub btFornecedor_Click(sender As Object, e As EventArgs)
        Fornecedor()
    End Sub
    Protected Sub btAvaliacao_Click(sender As Object, e As EventArgs)
        pnlAvaliacao.Visible = True

        If Not oConfig.ValidaCampo(hfdId_Solicitacao_Avaliacao.Value) = 0 Then
            vdataset = WS_Modulo.Solicitacao_Avaliacao(Session("Conn_Banco"),
                                                       Nothing,
                                                       hfdId_Solicitacao_Avaliacao.Value,
                                                       Nothing,
                                                       Nothing,
                                                       Nothing,
                                                       Session("Id_Usuario"),
                                                       "sp_SL_ID",
                                                       True)
            txtComentario.ReadOnly = True
            'cboAvaliacao.Visible = False
            cboAvaliacao.Enabled = False
            cboAvaliacao.SelectedValue = vdataset.Tables(0).Rows(0).Item("Avaliacao")
            txtComentario.Text = vdataset.Tables(0).Rows(0).Item("Descricao")

            If vdataset.Tables(0).Rows(0).Item("Avaliacao") = 1 Then ValidaEstrelas(1)
            If vdataset.Tables(0).Rows(0).Item("Avaliacao") = 2 Then ValidaEstrelas(2)
            If vdataset.Tables(0).Rows(0).Item("Avaliacao") = 3 Then ValidaEstrelas(3)
            If vdataset.Tables(0).Rows(0).Item("Avaliacao") = 4 Then ValidaEstrelas(4)
            If vdataset.Tables(0).Rows(0).Item("Avaliacao") = 5 Then ValidaEstrelas(5)
        Else
            btSalvarAvaliacao.Visible = True
        End If
    End Sub

    Protected Sub ValidaEstrelas(ByVal btn As Integer)

        cboAvaliacao.SelectedIndex = 0
        star01.Attributes("class") = "fas fa-star starEmpty"
        star02.Attributes("class") = "fas fa-star starEmpty"
        star03.Attributes("class") = "fas fa-star starEmpty"
        star04.Attributes("class") = "fas fa-star starEmpty"
        star05.Attributes("class") = "fas fa-star starEmpty"

        If btn = 1 Then
            cboAvaliacao.SelectedIndex = 1
            star01.Attributes("class") = "fas fa-star"
        ElseIf btn = 2 Then
            cboAvaliacao.SelectedIndex = 2
            star01.Attributes("class") = "fas fa-star"
            star02.Attributes("class") = "fas fa-star"
        ElseIf btn = 3 Then
            cboAvaliacao.SelectedIndex = 3
            star01.Attributes("class") = "fas fa-star"
            star02.Attributes("class") = "fas fa-star"
            star03.Attributes("class") = "fas fa-star"
        ElseIf btn = 4 Then
            cboAvaliacao.SelectedIndex = 4
            star01.Attributes("class") = "fas fa-star"
            star02.Attributes("class") = "fas fa-star"
            star03.Attributes("class") = "fas fa-star"
            star04.Attributes("class") = "fas fa-star"
        ElseIf btn = 5 Then
            cboAvaliacao.SelectedIndex = 5
            star01.Attributes("class") = "fas fa-star"
            star02.Attributes("class") = "fas fa-star"
            star03.Attributes("class") = "fas fa-star"
            star04.Attributes("class") = "fas fa-star"
            star05.Attributes("class") = "fas fa-star"
        End If

    End Sub

    Protected Sub btStar01_Click(sender As Object, e As EventArgs)
        ValidaEstrelas(1)
    End Sub

    Protected Sub btStar02_Click(sender As Object, e As EventArgs)
        ValidaEstrelas(2)
    End Sub

    Protected Sub btStar03_Click(sender As Object, e As EventArgs)
        ValidaEstrelas(3)
    End Sub

    Protected Sub btStar04_Click(sender As Object, e As EventArgs)
        ValidaEstrelas(4)
    End Sub

    Protected Sub btStar05_Click(sender As Object, e As EventArgs)
        ValidaEstrelas(5)
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class

