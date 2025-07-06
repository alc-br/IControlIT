
Public Class Solicitacao_Roaming
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
            Page.SetFocus(cboAtivoTipo)
            Page.Form.DefaultButton = btSalvar.UniqueID

            '-----valida botão menu
            ''Call Master.SelecionaBotao("roaming")

            '-----habilita campo usuario para administrador
            If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                txtUsuario.ReadOnly = False
            End If

            '-----monta modelo do ativo
            oConfig.CarregaCombo(cboAtivoTipo, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo_Roaming", Session("Id_Usuario"), Nothing))
            txtUsuario.Text = Session("Nm_Usuario")
            txtStatus.Text = "Aberto"
            imgVD.Visible = True
            Session("DataSet") = Nothing

            If Not Request("ID") = Nothing Then
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

                cboAtivoTipo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo")
                oConfig.CarregaCombo(cboSolicitacao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Solicitacao_Tipo_Id_Ativo_Tipo_Roaming", cboAtivoTipo.SelectedValue, Nothing))
                oConfig.CarregaCombo(cboSolucao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Solucao_Incidente", cboAtivoTipo.SelectedValue, Nothing))
                oConfig.CarregaCombo(cboSubSolucao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Solucao_Incidente", cboAtivoTipo.SelectedValue, Nothing))

                cboSolicitacao.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Tipo")
                cboSolucao.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Solucao") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Solucao"))

                hfdId_Solicitacao_Avaliacao.Value = vdataset.Tables(0).Rows(0).Item("Id_Solicitacao_Avaliacao")

                Call Executar()
                dtgSolicitacaoItem.DataSource = Session("DataSet")
                vDataSetChat = Session("DataSet")
                dtgSolicitacaoItem.DataBind()

                Dim vImage As Image
                Dim vImageUs As Image

                '----Monta imagem da grid no chat
                For i = 0 To dtgSolicitacaoItem.Items.Count - 1

                    If vDataSetChat.Tables(0).Rows(i).Item("Adm") = "True" Then

                        vImage = dtgSolicitacaoItem.Items(i).Cells(3).Controls(1).Controls(0).Controls(3)
                        vImage.ImageUrl = "../Img_Sistema/Botao/Grid/" & vDataSetChat.Tables(0).Rows(i).Item("Publicacao")
                    Else
                        vImageUs = dtgSolicitacaoItem.Items(i).Cells(3).Controls(3).Controls(0).Controls(7)
                        vImageUs.ImageUrl = "../Img_Sistema/Botao/Grid/" & vDataSetChat.Tables(0).Rows(i).Item("Publicacao")
                    End If

                Next

                If dtgSolicitacaoItem.Items.Count > 0 Then
                    tdChat.Visible = True
                End If

                If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                    btInformacoes.Enabled = True
                    btInformacoes.Style.Add("Opacity", "1")
                End If

                If txtStatus.Text = "Aberto" Then
                    txtUsuario.ReadOnly = True
                    If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then lblEncerrar.InnerText = "Cancelar"

                    btEncerrar.Enabled = True
                    btEncerrar.Style.Add("Opacity", "1")

                    cboSolucao.Enabled = True
                    dtgSolicitacaoItem.Enabled = True
                    txtDetalhamento.Enabled = False
                    cboAtivoTipo.Enabled = False
                    cboSolicitacao.Enabled = False
                End If
            End If

            If txtStatus.Text = "Encerrado" Then

                txtChat.ReadOnly = True
                btInsere.Enabled = False

                imgVD.Visible = False
                imgAM.Visible = False
                imgVM.Visible = False
                If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then lblEncerrar.InnerText = "Cancelar"
                btAvaliacao.Enabled = True
                btAvaliacao.Style.Add("Opacity", "1")

                cboSolucao.Enabled = False

                btSalvar.Enabled = False
                btSalvar.Style.Add("Opacity", "0.3")

                btEncerrar.Enabled = False
                btEncerrar.Style.Add("Opacity", "0.3")

                cboAtivoTipo.Enabled = False
                cboSolicitacao.Enabled = False
                dtgSolicitacaoItem.Enabled = False
                txtDetalhamento.Enabled = False
                txtUsuario.ReadOnly = True
                btSalvarAvaliacao.Visible = False
            End If
        End If
    End Sub

    Protected Sub cboAtivoTipo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboAtivoTipo.SelectedIndexChanged
        If cboAtivoTipo.SelectedValue = Nothing Then Exit Sub

        '-----monta modelo do ativo
        oConfig.CarregaCombo(cboSolicitacao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Solicitacao_Tipo_Id_Ativo_Tipo_Roaming", cboAtivoTipo.SelectedValue, Nothing))
        '-----monta modelo do ativo
        oConfig.CarregaCombo(cboSolucao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Solucao_Incidente", cboAtivoTipo.SelectedValue, Nothing))
        oConfig.CarregaCombo(cboSubSolucao, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Solucao_Incidente", cboAtivoTipo.SelectedValue, Nothing))
    End Sub

    Protected Sub btInsere_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        If txtUsuario.Text = Session("Nm_Usuario") Then
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
            T = WS_Modulo.Solicitacao(Session("Conn_Banco"), _
                                        oConfig.ValidaCampo(txtIncidente.Text), _
                                        Nothing, _
                                        Session("Id_Usuario"), _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        22, _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        Session("Id_Usuario"), _
                                        "sp_Grava_Item_Solicitacao", _
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
    End Sub

    Protected Sub BtPublicaSim_Click(sender As Object, e As System.EventArgs) Handles BtPublicaSim.Click
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        T = WS_Modulo.Solicitacao(Session("Conn_Banco"), _
                                    oConfig.ValidaCampo(txtIncidente.Text), _
                                    Nothing, _
                                    Session("Id_Usuario"), _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    22, _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    Session("Id_Usuario"), _
                                    "sp_Grava_Item_Solicitacao", _
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
        T = WS_Modulo.Solicitacao(Session("Conn_Banco"), _
                                    oConfig.ValidaCampo(txtIncidente.Text), _
                                    Nothing, _
                                    Session("Id_Usuario"), _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    1, _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    Nothing, _
                                    Session("Id_Usuario"), _
                                    "sp_Grava_Item_Solicitacao", _
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
        Session("DataSet") = WS_Modulo.Solicitacao(Session("Conn_Banco"), _
                                                    oConfig.ValidaCampo(txtIncidente.Text), _
                                                    Nothing, _
                                                    Session("Id_Usuario"), _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    "sp_Consulta_Solicitacao_Item", _
                                                    True)
    End Sub

    Protected Sub btOk_Click(sender As Object, e As System.EventArgs) Handles btOk.Click
        pnlDetalhe.Visible = False
        Call Encerrar("sp_Encerra_Solicitacao")
        '-----envio de email de encerramento
        T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                    oConfig.ValidaCampo(txtIncidente.Text),
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    "sd_Email_Enc_Chamado_Roaming",
                                    False).Tables(0).Rows(0).Item(0)
        pnlDetalhe.Visible = False
        pnlRegistroSalvo.Visible = True
        lblRegistroSalvo.Visible = False
        lblMsg.Visible = True
        lblMsg.Text = "Chamado Encerrado"

        '-----limpa tela
        cboAtivoTipo.SelectedValue = Nothing
        cboSolicitacao.SelectedValue = Nothing
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
    End Sub

    Public Sub Encerrar(Encerra_Cancela As System.String)

        Dim v_lblDetalhamento As Label
        vDataSetChat = Session("DataSet")

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

        If Not txtIncidente.Text = "" Then
            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                    oConfig.ValidaCampo(txtIncidente.Text),
                    Nothing,
                    Nothing,
                    Nothing,
                    Nothing,
                    Nothing,
                    Nothing,
                    Nothing,
                    Nothing,
                    oConfig.ValidaCampo(txtObservacao.Text),
                    Nothing,
                    Nothing,
                    Nothing,
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

        '-----valida data menor que a data atual

        If txtDetalhamento.Enabled = True Then
            If txtDetalhamento.Text < Date.Today Then
                lblMsg.Text = "Data inválida."
                lblMsg.Visible = True
                Exit Sub
            End If

            '-----valida data maior que 30 dias
            If txtDetalhamento.Text > Date.Now.AddDays(30) Then
                lblMsg.Text = "Solicitação só pode ser feita com no máximo 30 dias."
                lblMsg.Visible = True
                Exit Sub
            End If
        End If

        If txtIncidente.Text = "" Then
            txtIncidente.Text = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                                        Nothing,
                                                        oConfig.ValidaCampo(txtDetalhamento.Text),
                                                        Session("Id_Usuario"),
                                                        oConfig.ValidaCampo(txtUsuario.Text),
                                                        oConfig.ValidaCampo(cboAtivoTipo.SelectedValue),
                                                        oConfig.ValidaCampo(cboSolicitacao.SelectedValue),
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Session("Id_Usuario"),
                                                        "sp_Grava_Solicitacao",
                                                        False).Tables(0).Rows(0).Item(0)

            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIncidente.Text),
                                        Nothing,
                                        Session("Id_Usuario"),
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        oConfig.ValidaCampo(txtDetalhamento.Text),
                                        22,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
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
            Dim v_lblDetalhamento As TextBox

            For i = 0 To dtgSolicitacaoItem.Items.Count - 1

                If vDataSetChat.Tables(0).Rows(i).Item("Adm") = "True" Then
                    v_lblDetalhamento = dtgSolicitacaoItem.Items(i).Cells(2).Controls(1).Controls(0).Controls(1)
                Else
                    v_lblDetalhamento = dtgSolicitacaoItem.Items(i).Cells(2).Controls(3).Controls(0).Controls(1)
                End If

                T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                            oConfig.ValidaCampo(txtIncidente.Text),
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            dtgSolicitacaoItem.Items(i).Cells(1).Text,
                                            oConfig.ValidaCampo(IIf(v_lblDetalhamento.Text = "", IIf(txtChat.Text = "", txtDetalhamento.Text, txtChat.Text), v_lblDetalhamento.Text)),
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
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
        cboAtivoTipo.Enabled = False
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
                                    "sd_Email_Enc_Chamado_Roaming",
                                    False).Tables(0).Rows(0).Item(0)
        pnlSolucao.Visible = False
        pnlRegistroSalvo.Visible = True
        lblRegistroSalvo.Visible = False
        lblMsg.Visible = True
        lblMsg.Text = "Chamado Encerrado"

        '-----limpa tela
        cboAtivoTipo.SelectedValue = Nothing
        cboSolicitacao.SelectedValue = Nothing
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
    End Sub

    Protected Sub btSalvarAvaliacao_Click(sender As Object, e As EventArgs) Handles btSalvarAvaliacao.Click
        If Not cboAvaliacao.SelectedValue = Nothing Then
            vdataset = WS_Modulo.Solicitacao_Avaliacao(Session("Conn_Banco"), _
                                                       oConfig.ValidaCampo(txtIncidente.Text), _
                                                       Nothing, _
                                                       Nothing, _
                                                       oConfig.ValidaCampo(cboAvaliacao.SelectedValue), _
                                                       oConfig.ValidaCampo(txtComentario.Text), _
                                                       Nothing, _
                                                       "sp_SM", _
                                                       True)
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
        pnlAvaliacao.Visible = False
    End Sub

    Protected Sub btFechar_Registro_Click(sender As Object, e As EventArgs) Handles btFechar_Registro.Click
        pnlRegistroSalvo.Visible = False
    End Sub

    Protected Sub btFechar_Informacoes_Click(sender As Object, e As EventArgs) Handles btFechar_Informacoes.Click
        pnlInformacao.Visible = False
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        '-----registro salvo
        lblRegistroSalvo.Visible = False
        lblMsg.Visible = False
        pnlRegistroSalvo.Visible = True

        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----valida data em branco
        If Trim(txtDetalhamento.Text) = "" Then Exit Sub

        '-----valida data menor que a data atual

        If txtDetalhamento.Enabled = True Then
            If txtDetalhamento.Text < Date.Today Then
                lblMsg.Text = "Data inválida."
                lblMsg.Visible = True
                Exit Sub
            End If

            '-----valida data maior que 30 dias
            If txtDetalhamento.Text > Date.Now.AddDays(30) Then
                lblMsg.Text = "Solicitação só pode ser feita com no máximo 30 dias."
                lblMsg.Visible = True
                Exit Sub
            End If
        End If

        If txtIncidente.Text = "" Then
            txtIncidente.Text = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                                        Nothing,
                                                        oConfig.ValidaCampo(txtDetalhamento.Text),
                                                        Session("Id_Usuario"),
                                                        oConfig.ValidaCampo(txtUsuario.Text),
                                                        oConfig.ValidaCampo(cboAtivoTipo.SelectedValue),
                                                        oConfig.ValidaCampo(cboSolicitacao.SelectedValue),
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Session("Id_Usuario"),
                                                        "sp_Grava_Solicitacao",
                                                        False).Tables(0).Rows(0).Item(0)

            T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIncidente.Text),
                                        Nothing,
                                        Session("Id_Usuario"),
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        oConfig.ValidaCampo(txtDetalhamento.Text),
                                        22,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
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
            Dim v_lblDetalhamento As TextBox

            For i = 0 To dtgSolicitacaoItem.Items.Count - 1

                If vDataSetChat.Tables(0).Rows(i).Item("Adm") = "True" Then
                    v_lblDetalhamento = dtgSolicitacaoItem.Items(i).Cells(2).Controls(1).Controls(0).Controls(1)
                Else
                    v_lblDetalhamento = dtgSolicitacaoItem.Items(i).Cells(2).Controls(3).Controls(0).Controls(1)
                End If

                T = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                            oConfig.ValidaCampo(txtIncidente.Text),
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            dtgSolicitacaoItem.Items(i).Cells(1).Text,
                                            oConfig.ValidaCampo(IIf(v_lblDetalhamento.Text = "", IIf(txtChat.Text = "", txtDetalhamento.Text, txtChat.Text), v_lblDetalhamento.Text)),
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
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
        cboAtivoTipo.Enabled = False
        cboSolicitacao.Enabled = False
        txtChat.Text = ""
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
            cboAtivoTipo.SelectedValue = Nothing
            cboSolicitacao.SelectedValue = Nothing
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
                                            "sd_Email_Enc_Chamado_Roaming",
                                            False).Tables(0).Rows(0).Item(0)
                pnlSolucao.Visible = False
                pnlRegistroSalvo.Visible = True
                lblRegistroSalvo.Visible = False
                lblMsg.Visible = True
                lblMsg.Text = "Chamado Encerrado"

                '-----limpa tela
                cboAtivoTipo.SelectedValue = Nothing
                cboSolicitacao.SelectedValue = Nothing
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

    Protected Sub btInformacoes_Click(sender As Object, e As EventArgs)
        pnlInformacao.Visible = True
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

    Protected Sub btAvaliacao_Click(sender As Object, e As EventArgs)
        pnlAvaliacao.Visible = True

        If Not hfdId_Solicitacao_Avaliacao.Value = 0 Then
            vdataset = WS_Modulo.Solicitacao_Avaliacao(Session("Conn_Banco"),
                                                       Nothing,
                                                       hfdId_Solicitacao_Avaliacao.Value,
                                                       Nothing,
                                                       Nothing,
                                                       Nothing,
                                                       Nothing,
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

