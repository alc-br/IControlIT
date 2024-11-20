
Public Class Ativo_Parametro
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
                "Cadastro de Aquisição e Rateio",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtDataTerminoGarantia)
            Page.Form.DefaultButton = btPDF.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            oConfig.CarregaCombo(cboFormaAquisicao, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Ativo_Fr_Aquisicao", Nothing))
            oConfig.CarregaCombo(cboRateioTroncoGrupo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Tronco_Grupo", Nothing))
            oConfig.CarregaCombo(cboRateioOutroConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))
            oConfig.CarregaCombo(cboContrato, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Contrato", Nothing))

            txtIdentificacao.Text = Request("ID")

            If Not Request("ID") = Nothing Then
                vdataset = WS_Cadastro.Ativo_Parametro(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing,
                                                            Nothing, Nothing, Nothing, Nothing, Nothing,
                                                            Nothing, Nothing, "sp_SL_ID", True)

                If Not vdataset.Tables(0).Rows.Count = 0 Then
                    txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Ativo")
                    txtDataTerminoGarantia.Text = vdataset.Tables(0).Rows(0).Item("Dt_Termino_Garantia")
                    cboFormaAquisicao.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Fr_Aquisicao")
                    txtDataInicioFormaAquisicao.Text = vdataset.Tables(0).Rows(0).Item("Dt_Ini_Fr_Aquisicao")
                    txtValorFormaAquisicao.Text = vdataset.Tables(0).Rows(0).Item("Vr_Fr_Aquisicao")
                    txtMesFormaAquisicao.Text = vdataset.Tables(0).Rows(0).Item("Qtd_Mes_Residuo_Fr_Aquisicao")
                    chkRateioConglomerado.Checked = IIf(vdataset.Tables(0).Rows(0).Item("Rateio_Conglomerado") = 2, True, False)
                    cboRateioTroncoGrupo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Tronco_Grupo")
                    cboRateioOutroConglomerado.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Conglomerado")
                    cboContrato.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Contrato")
                    '-----verifica se existe conta cadastrada
                    If Not vdataset.Tables(0).Rows(0).Item("Nr_Plano_Conta") = Nothing Then
                        lblConta.Text = "Conta - " & vdataset.Tables(0).Rows(0).Item("Nr_Plano_Conta") & " "
                        cboContrato.Enabled = False
                    End If
                    '-----verifica se existe contrato cadastrado
                    If Not vdataset.Tables(0).Rows(0).Item("Id_Contrato") = Nothing Then
                        btContrato.Visible = True
                        btContrato.PostBackUrl = "../Contrato/Consulta_Contrato.aspx?Voltar=Ativo&ID=" & vdataset.Tables(0).Rows(0).Item("Id_Contrato")
                    End If

                    '-----retorna relacionamento de ativo com centro de custo
                    vdataset = WS_Cadastro.Ativo_Parametro(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing,
                                                                Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                Nothing, Nothing, "sd_SL_RL_Ativo_Centro_Custo", True)
                    oConfig.CarregaList(lstCentroCusto, vdataset)

                    '-----trata campo de rateio
                    If chkRateioConglomerado.Checked = True Or Not cboRateioOutroConglomerado.SelectedValue = Nothing Or Not cboRateioTroncoGrupo.SelectedValue = Nothing Or lstCentroCusto.Items.Count > 1 Then
                        chkRateioConglomerado.Enabled = IIf(chkRateioConglomerado.Checked = True, True, False)
                        cboRateioOutroConglomerado.Enabled = IIf(cboRateioOutroConglomerado.SelectedValue = Nothing, False, True)
                        cboRateioTroncoGrupo.Enabled = IIf(cboRateioTroncoGrupo.SelectedValue = Nothing, False, True)
                        If lstCentroCusto.Items.Count > 1 Then
                            txtLocalizaCentroCusto.Enabled = True
                            lstListaCentroCusto.Enabled = True
                            btLocalizaCentroCusto.Enabled = True
                            chkCentroCusto.Enabled = True
                            chkCentroCusto.Checked = True
                        Else
                            txtLocalizaCentroCusto.Enabled = False
                            lstListaCentroCusto.Enabled = False
                            btLocalizaCentroCusto.Enabled = False
                            chkCentroCusto.Enabled = False
                            chkCentroCusto.Checked = False
                        End If
                    End If
                End If

                '-----retorna relacionamento de ativo com centro de custo e porcentagem 
                dtgPorcentagem.DataSource = WS_Cadastro.Ativo_Parametro(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                        Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                        "sd_SL_RL_Ativo_Centro_Custo_Porcentagem", True)
                dtgPorcentagem.DataBind()
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboFormaAquisicao.SelectedValue = Nothing
        cboContrato.SelectedValue = Nothing
        cboRateioOutroConglomerado.SelectedValue = Nothing
        cboRateioTroncoGrupo.SelectedValue = Nothing
        lstCentroCusto.Items.Clear()
        lstListaCentroCusto.Items.Clear()
        chkRateioConglomerado.Checked = False
        btSalvar.Enabled = True
    End Sub

    Protected Sub btLocalizaCentroCusto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btLocalizaCentroCusto.Click
        '-----carrega centro de custo
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        oConfig.CarregaList(lstListaCentroCusto, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Centro_Custo", txtLocalizaCentroCusto.Text))
    End Sub

    Protected Sub chkRateioConglomerado_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRateioConglomerado.CheckedChanged
        If chkRateioConglomerado.Checked = True Then
            cboRateioTroncoGrupo.Enabled = False
            txtLocalizaCentroCusto.Enabled = False
            cboRateioOutroConglomerado.Enabled = False
            lstListaCentroCusto.Enabled = False
            chkCentroCusto.Enabled = False
        Else
            cboRateioTroncoGrupo.Enabled = True
            txtLocalizaCentroCusto.Enabled = True
            cboRateioOutroConglomerado.Enabled = True
            lstListaCentroCusto.Enabled = True
            chkCentroCusto.Enabled = True
        End If
    End Sub

    Protected Sub cboRateioTroncoGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRateioTroncoGrupo.SelectedIndexChanged
        If Not cboRateioTroncoGrupo.SelectedValue = Nothing Then
            chkRateioConglomerado.Enabled = False
            txtLocalizaCentroCusto.Enabled = False
            cboRateioOutroConglomerado.Enabled = False
            lstListaCentroCusto.Enabled = False
            chkCentroCusto.Enabled = False
        Else
            chkRateioConglomerado.Enabled = True
            txtLocalizaCentroCusto.Enabled = True
            cboRateioOutroConglomerado.Enabled = True
            lstListaCentroCusto.Enabled = True
            chkCentroCusto.Enabled = True
        End If
    End Sub

    Protected Sub cboRateioOutroConglomerado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRateioOutroConglomerado.SelectedIndexChanged
        If Not cboRateioOutroConglomerado.SelectedValue = Nothing Then
            chkRateioConglomerado.Enabled = False
            cboRateioTroncoGrupo.Enabled = False
            txtLocalizaCentroCusto.Enabled = False
            lstListaCentroCusto.Enabled = False
            btLocalizaCentroCusto.Enabled = False
            chkCentroCusto.Enabled = False
        Else
            chkRateioConglomerado.Enabled = True
            cboRateioTroncoGrupo.Enabled = True
            txtLocalizaCentroCusto.Enabled = True
            lstListaCentroCusto.Enabled = True
            btLocalizaCentroCusto.Enabled = True
            chkCentroCusto.Enabled = True
        End If
    End Sub

    Protected Sub chkCentroCusto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCentroCusto.CheckedChanged
        If chkCentroCusto.Checked = True Then
            txtLocalizaCentroCusto.Enabled = True
            lstListaCentroCusto.Enabled = True
            btLocalizaCentroCusto.Enabled = True
            '---
            chkRateioConglomerado.Enabled = False
            cboRateioTroncoGrupo.Enabled = False
            cboRateioOutroConglomerado.Enabled = False
        Else
            txtLocalizaCentroCusto.Enabled = False
            lstListaCentroCusto.Enabled = False
            btLocalizaCentroCusto.Enabled = False

            txtLocalizaCentroCusto.Text = ""
            lstListaCentroCusto.Items.Clear()
            lstCentroCusto.Items.Clear()

            '---
            chkRateioConglomerado.Enabled = True
            cboRateioTroncoGrupo.Enabled = True
            cboRateioOutroConglomerado.Enabled = True
        End If
    End Sub

    Protected Sub btMoveSelecionado_Click(sender As Object, e As System.EventArgs) Handles btMoveSelecionado.Click
        Call move_Dados(lstCentroCusto, lstListaCentroCusto)
    End Sub

    Protected Sub btMoveSelecao_Click(sender As Object, e As System.EventArgs) Handles btMoveSelecao.Click
        Call move_Dados(lstListaCentroCusto, lstCentroCusto)
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

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        pnlCentro_Custo.Visible = False
    End Sub

    Protected Sub btMostar_Click(sender As Object, e As EventArgs) Handles btMostar.Click
        '-----grava percentual por centro de custo
        If dtgPorcentagem.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32 = 0
        Dim vPorcentagem As System.Int32 = 0
        Dim v_txtPorcentagem As TextBox = Nothing

        '-----totaliza porcentagem 
        For i = 0 To dtgPorcentagem.Items.Count - 1
            v_txtPorcentagem = dtgPorcentagem.Items(i).Cells(1).Controls(1)
            If Not Trim(v_txtPorcentagem.Text) = Nothing And IsNumeric(v_txtPorcentagem.Text) = True Then
                vPorcentagem = vPorcentagem + v_txtPorcentagem.Text
            End If
        Next

        '-----valida porcentagem para gravacao
        If Not vPorcentagem = 100 Then Exit Sub

        '-----grava porcentagem por centro de custo de rateio
        For i = 0 To dtgPorcentagem.Items.Count - 1
            v_txtPorcentagem = dtgPorcentagem.Items(i).Cells(1).Controls(1)
            If Not Trim(v_txtPorcentagem.Text) = Nothing Then
                WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
                WS_Cadastro.Ativo_Porcentagem_Rateio(Session("Conn_Banco"),
                                                            Request("ID"),
                                                            dtgPorcentagem.Items(i).Cells(2).Text,
                                                            IIf(Trim(v_txtPorcentagem.Text) = "", Nothing, v_txtPorcentagem.Text),
                                                            Session("Id_Usuario"),
                                                            "sp_Porcentagem_Rateio_Centro_Custo", False)
            End If
        Next

        '-----retorna relacionamento de ativo com centro de custo e porcentagem 
        dtgPorcentagem.DataSource = WS_Cadastro.Ativo_Parametro(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                "sd_SL_RL_Ativo_Centro_Custo_Porcentagem", True)
        dtgPorcentagem.DataBind()

        '-----fecha tela de cdc
        pnlCentro_Custo.Visible = False
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Ativo_Parametro(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                        IIf(Trim(txtDataTerminoGarantia.Text) = "", Nothing, txtDataTerminoGarantia.Text),
                                        oConfig.ValidaCampo(cboFormaAquisicao.SelectedValue),
                                        IIf(Trim(txtDataInicioFormaAquisicao.Text) = "", Nothing, txtDataInicioFormaAquisicao.Text),
                                        oConfig.ValidaCampo(txtValorFormaAquisicao.Text),
                                        oConfig.ValidaCampo(txtMesFormaAquisicao.Text),
                                        IIf(chkRateioConglomerado.Checked = True, 2, 1),
                                        oConfig.ValidaCampo(cboRateioTroncoGrupo.SelectedValue),
                                        oConfig.ValidaCampo(cboRateioOutroConglomerado.SelectedValue),
                                        oConfig.ValidaCampo(cboContrato.SelectedValue),
                                        Session("Id_Usuario"),
                                        "sp_SM",
                                        False)

        '-----grava relacionamento de ativo com centro de custo
        WS_Cadastro.Relacionamento(Session("Conn_Banco"), "sp_SM_RL_Ativo_Centro_Custo", txtIdentificacao.Text, oConfig.AgrupaDados(lstCentroCusto))

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Ativo_Parametro(Session("Conn_Banco"), txtIdentificacao.Text, Nothing, Nothing,
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Nothing, Session("Id_Usuario"), "sp_SE", False)
        Call limpar()
    End Sub
    Protected Sub btConfig_Click(sender As Object, e As EventArgs)
        pnlCentro_Custo.Visible = True
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
