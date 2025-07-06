Public Class Rateio
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Rateio As New WS_GUA_Rateio.WSRateio
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim oConfig As New cls_Config
    Dim vDataSet As New System.Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

            WS_Cadastro.Timeout = 3600000
            WS_Consulta.Timeout = 3600000
            WS_Rateio.Timeout = 3600000

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                        IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                        "Rateio Contabil ", _
                        vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)

            Page.SetFocus(cboFaturaTipo)
            Page.Form.DefaultButton = btExecutar.UniqueID
            btConfiguracao.PostBackUrl = "~/Recepcao_Fatura/Rateio_Custo_Fixo.aspx"
            Call Master.Localizar("sp_Drop_Rateio", Page.AppRelativeVirtualPath.ToString)

            '-----gera combo
            oConfig.CarregaCombo(cboFaturaTipo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Fatura_Parametro", Nothing))
            vDataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            oConfig.CarregaCombo(cboDataLote, vDataSet)

            Session("DataView") = Nothing

            If Not Request("ID") = Nothing Then
                Dim vLinha As Data.DataRow
                vDataSet = Nothing
                '-----monta dados rateio
                chkGravaRateio.Checked = True
                BtExecutar.Enabled = False
                chkGravaRateio.Enabled = False
                dtgAtivo_Critica.Enabled = False

                vDataSet = WS_Consulta.Rateio_Lista(Session("Conn_Banco"), Request("ID"), Nothing, "sp_Rateio")

                hdfId_Rateio.Value = Request("ID")

                txtDescricao.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Rateio")
                cboDataLote.SelectedValue = vDataSet.Tables(0).Rows(0).Item("Dt_Lote")
                txtObservacao.Text = vDataSet.Tables(0).Rows(0).Item("Observacao")
                cboFaturaTipo.SelectedValue = vDataSet.Tables(0).Rows(0).Item("Id_Fatura_Parametro")

                '-----monta dados ativo critica
                vDataSet = Nothing
                vDataSet = WS_Consulta.Rateio_Lista(Session("Conn_Banco"), Request("ID"), Nothing, "sp_Ativo_Critica")
                If vDataSet.Tables(0).Rows.Count > 0 Then
                    txtSCritica.Text = Format(vDataSet.Tables(0).Rows(0).Item("Total"), "##########,###########0")
                    txtSCritica.Text = IIf(Trim(txtSCritica.Text) = "", 0, txtSCritica.Text)
                End If

                '-----carega bilhete vinculados ao ativo padrao
                Dim vDataSetFatura As New System.Data.DataSet
                vDataSetFatura = Nothing
                vDataSetFatura = WS_Consulta.Rateio_Lista(Session("Conn_Banco"), Request("ID"), Nothing, "sp_Fatura_Rateio")
                Dim pId_Check As System.String = Nothing

                For Each vLinha In vDataSetFatura.Tables(0).Rows
                    pId_Check = pId_Check + vLinha.Item(0)
                Next

                '-----carrega ativo critica
                vDataSet = Nothing
                vDataSet = WS_Rateio.Rateio(Session("Conn_Banco"), _
                                            Nothing, _
                                            cboFaturaTipo.SelectedValue, _
                                            pId_Check, _
                                            oConfig.ValidaCampo(cboDataLote.SelectedValue), _
                                            Nothing, _
                                            Nothing, _
                                            Nothing, _
                                            Nothing, _
                                            Nothing, _
                                            "sp_Retorna_Bilhete_Ativo_Padrao", _
                                            True)
                dtgAtivo_Critica.DataSource = vDataSet
                dtgAtivo_Critica.DataBind()

                '-----soma
                Dim vCont As System.Int32
                Dim vTotalCredito As System.Double = 0
                Dim vTotalDebito As System.Double = 0

                For vCont = 0 To vDataSet.Tables(0).Rows.Count - 1
                    If vDataSet.Tables(0).Rows(vCont).Item("DB_Custo") > 0 Then
                        vTotalCredito = vTotalCredito + vDataSet.Tables(0).Rows(vCont).Item("DB_Custo")
                    Else
                        vTotalDebito = vTotalDebito + vDataSet.Tables(0).Rows(vCont).Item("DB_Custo")
                    End If
                Next vCont
                txtSPacote.Text = Format(vTotalCredito, "##########,###########0")
                txtSDesconto.Text = Format(vTotalDebito, "##########,###########0")

                '-----monta dados do rateio item

                vDataSet = WS_Consulta.Rateio_Lista(Session("Conn_Banco"), Request("ID"), cboDataLote.SelectedValue, "sp_Rateio_Item")
                dtgConsulta.DataSource = vDataSet
                dtgConsulta.DataBind()
                Session("DataView") = vDataSet

                txtSFatura.Text = Format(vDataSet.Tables(0).Rows(0).Item("Total_Fatura"), "##########,###########0")
                txtSAuditado.Text = Format(vDataSet.Tables(0).Rows(0).Item("Valor_Auditado"), "##########,###########0")
                txtSRateado.Text = Format(vDataSet.Tables(0).Rows(0).Item("Total_Rateado"), "##########,###########0")
                txtSBilhete.Text = Format(vDataSet.Tables(0).Rows(0).Item("Total_Bilhete"), "##########,###########0")

                txtSVago.Text = Mid(txtObservacao.Text, (txtObservacao.Text.IndexOf("Valor das linhas vagas: ") + 25), IIf((txtObservacao.Text.IndexOf("/ Valor de estoque:") - 24) < 0, 2, (txtObservacao.Text.IndexOf("/ Valor de estoque:") - 24)))
                txtSEstoque.Text = Mid(txtObservacao.Text, (txtObservacao.Text.IndexOf("/ Valor de estoque: ") + 20), IIf((txtObservacao.Text.IndexOf(" / Valor de desconto:") - 50) < 0, 2, (txtObservacao.Text.IndexOf(" / Valor de desconto:") - 50)))

                txtSCritica.Text = Format((CType(txtSVago.Text, Double) + CType(txtSEstoque.Text, Double) + (CType(txtSFatura.Text, Double) - CType(txtSBilhete.Text, Double))), "##########,###########0")
                txtSPorcentagemIndice.Text = Format((CType(txtSCritica.Text, Double) / CType(txtSRateado.Text, Double)) * 100, "###") & "%"
            End If
        End If
    End Sub


    Public Sub chkSelecTodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelecTodos.CheckedChanged
        If chkSelecTodos.Checked = True Then
            For i = 0 To optFatura.Items.Count - 1
                optFatura.Items(i).Selected = True
            Next
        Else
            For i = 0 To optFatura.Items.Count - 1
                optFatura.Items(i).Selected = False
            Next
        End If
    End Sub


    Protected Sub cboDataLote_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDataLote.SelectedIndexChanged
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

        If cboDataLote.SelectedValue = Nothing Then Exit Sub
        If cboFaturaTipo.SelectedValue = Nothing Then Exit Sub

        chkRateio.Items.Clear()
        dtgAtivo_Critica.DataSource = Nothing
        dtgAtivo_Critica.DataBind()
        dtgConsulta.DataSource = Nothing
        dtgConsulta.DataBind()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        txtSVago.Text = 0

        '-----carreta fatura
        oConfig.CarregaCheckBoxList(optFatura, WS_Rateio.Rateio(Session("Conn_Banco"),
                                                                        Nothing,
                                                                        cboFaturaTipo.SelectedValue,
                                                                        Nothing,
                                                                        oConfig.ValidaCampo(cboDataLote.SelectedValue),
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        Nothing,
                                                                        "sp_Retorna_Fatura",
                                                                        True))
        '-----carega tipo de rateio
        oConfig.CarregaCheckBoxList(chkRateio, WS_Rateio.Rateio(Session("Conn_Banco"), _
                                                                    Nothing, _
                                                                    cboFaturaTipo.SelectedValue, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    Nothing, _
                                                                    "sp_Retorna_Tipo_Rateio", _
                                                                    True))

        Dim vCont As Int32
        For vCont = 0 To chkRateio.Items.Count - 1
            chkRateio.Items.Item(0).Selected = True
        Next vCont
    End Sub

    Protected Sub optFatura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optFatura.SelectedIndexChanged
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

        If cboFaturaTipo.SelectedValue = Nothing Then Exit Sub
        '-----carrega ativo critica
        Dim vdataSet As New Data.DataSet
        vdataSet = WS_Rateio.Rateio(Session("Conn_Banco"), _
                                        Nothing, _
                                        cboFaturaTipo.SelectedValue, _
                                        optFatura.SelectedValue, _
                                        oConfig.ValidaCampo(cboDataLote.SelectedValue), _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        "sp_Retorna_Bilhete_Ativo_Padrao", _
                                        True)

        If vdataSet.Tables(0).Rows.Count < 1000 Then
            dtgAtivo_Critica.DataSource = vdataSet
            dtgAtivo_Critica.DataBind()
        End If

        '-----soma
        Dim vCont As System.Int32
        Dim vTotal As System.Double = 0
        Dim vTotalCredito As System.Double = 0
        Dim vTotalDebito As System.Double = 0

        For vCont = 0 To vdataSet.Tables(0).Rows.Count - 1
            If vdataSet.Tables(0).Rows(vCont).Item("DB_Custo") > 0 Then
                vTotalCredito = vTotalCredito + vdataSet.Tables(0).Rows(vCont).Item("DB_Custo")
            Else
                vTotalDebito = vTotalDebito + vdataSet.Tables(0).Rows(vCont).Item("DB_Custo")
            End If
        Next vCont
        txtSPacote.Text = Format(vTotalCredito, "##########,###########0")
        txtSDesconto.Text = Format(vTotalDebito, "##########,###########0")

        '-----monta lista de arquivo vago
        vdataSet = WS_Rateio.Rateio(Session("Conn_Banco"), _
                                        oConfig.ValidaCampo(txtDescricao.Text), _
                                        cboFaturaTipo.SelectedValue, _
                                        Nothing, _
                                        oConfig.ValidaCampo(cboDataLote.SelectedValue), _
                                        Retorna_Id_Check(chkRateio), _
                                        optFatura.SelectedValue, _
                                        IIf(chkGravaRateio.Checked = True, 2, 1), _
                                        Nothing, _
                                        Nothing, _
                                        "sp_Retorna_Ativo_Vago", _
                                        True)

        If vdataSet.Tables(0).Rows.Count > 0 Then
            txtSVago.Text = Format(vdataSet.Tables(0).Rows(0).Item("Total"), "##########,###########0")
            txtSVago.Text = IIf(Trim(txtSVago.Text) = "", 0, txtSVago.Text)
        End If

        '-----monta lista de arquivo estoque
        vdataSet = WS_Rateio.Rateio(Session("Conn_Banco"), _
                                        oConfig.ValidaCampo(txtDescricao.Text), _
                                        cboFaturaTipo.SelectedValue, _
                                        Nothing, _
                                        oConfig.ValidaCampo(cboDataLote.SelectedValue), _
                                        Retorna_Id_Check(chkRateio), _
                                        optFatura.SelectedValue, _
                                        IIf(chkGravaRateio.Checked = True, 2, 1), _
                                        Nothing, _
                                        Nothing, _
                                        "sp_Retorna_Ativo_Estoque", _
                                        True)

        If vdataSet.Tables(0).Rows.Count > 0 Then
            txtSEstoque.Text = Format(vdataSet.Tables(0).Rows(0).Item("Total"), "##########,###########0")
            txtSEstoque.Text = IIf(Trim(txtSEstoque.Text) = "", 0, txtSEstoque.Text)
        End If
    End Sub

    Protected Sub dtgConsulta_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgConsulta.PageIndexChanged
        dtgConsulta.CurrentPageIndex = e.NewPageIndex
        dtgConsulta.DataSource = Session("DataView")
        dtgConsulta.DataBind()
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboFaturaTipo.SelectedValue = Nothing
        cboDataLote.SelectedValue = Nothing
        chkSelecTodos.Checked = False
        optFatura.Items.Clear()
        chkRateio.Items.Clear()
        dtgAtivo_Critica.DataSource = Nothing
        dtgAtivo_Critica.DataBind()
        chkGravaRateio.Checked = False
        dtgConsulta.DataSource = Nothing
        dtgConsulta.DataBind()
        Session("DataView") = Nothing
        BtExecutar.Enabled = True
        btExecutar.Enabled = True
        chkGravaRateio.Enabled = True
        dtgAtivo_Critica.Enabled = True
        Page.SetFocus(txtDescricao)
    End Sub

    Function Retorna_Id_Check(ByVal pCheckBox As CheckBoxList) As System.String
        Dim i As System.Int32
        Dim pId_Check As System.String = Nothing
        For i = 0 To pCheckBox.Items.Count - 1
            If pCheckBox.Items(i).Selected Then
                pId_Check = pId_Check & pCheckBox.Items(i).Value & ","
            End If
        Next
        If Not pId_Check = Nothing Then pId_Check = Mid(pId_Check, 1, Len(pId_Check) - 1)
        Return pId_Check
    End Function

    Protected Sub btInsere_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim v_txt_Nr_Ativo As TextBox
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btInsere") + 10, 2), System.Int32)

        v_txt_Nr_Ativo = dtgAtivo_Critica.Items(i).Cells(1).Controls(1)

        If cboFaturaTipo.SelectedValue = Nothing Then Exit Sub
        '-----carrega ativo critica
        Dim vdataSet As New Data.DataSet
        vdataSet = WS_Rateio.Rateio(Session("Conn_Banco"),
                                        v_txt_Nr_Ativo.Text,
                                        cboFaturaTipo.SelectedValue,
                                        optFatura.SelectedValue,
                                        oConfig.ValidaCampo(cboDataLote.SelectedValue),
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        dtgAtivo_Critica.Items(i).Cells(0).Text,
                                        Nothing,
                                        "sp_Salva_Bilhete_Ativo_Padrao",
                                        True)
        dtgAtivo_Critica.DataSource = vdataSet
        dtgAtivo_Critica.DataBind()
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

        If Not hdfId_Rateio.Value = Nothing Then
            WS_Rateio.Rateio(Session("Conn_Banco"),
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    oConfig.ValidaCampo(hdfId_Rateio.Value),
                                    Nothing,
                                    "sp_Deleta_Rateio",
                                    False)
            Call limpar()
        End If
    End Sub

    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

        If dtgConsulta.Items.Count = 0 Then Exit Sub

        If Not dtgConsulta.Items(0).Cells(5).Text = 0 Then
            Session("DataSet") = WS_Consulta.Rateio_Lista(Session("Conn_Banco"), dtgConsulta.Items(0).Cells(5).Text, cboDataLote.SelectedValue, "sp_Rateio_Export")

            '-----comentado = todos ou posso selecionar um tipo de modelo por vez
            Dim Tipo As System.String = Nothing
            '-----nome do arquivo a ser exportado
            Dim Descricao As System.String = "Rateio"
            '-----campos a ser exportado modelo (xxxx;xxxxx;xxxx). quando null sistema gera com base no dataset
            Dim Campo As System.String = "Virtual;Ativo;Conglomerado;Tipo;Usuario;Matricula;Filial;CDC;Sistema;Rateado;Fatura;Total_Fatura;Total_Rateado"

            '-----abre pnl de exportacao
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                                "Descricao=" & Descricao &
                                                "&Campo=" & Campo &
                                                "&Tipo=" & Tipo &
                                                "','','resizable=yes, menubar=yes, scrollbars=no," &
                                                "height=768px, width=1024px, top=10, left=10'" &
                                                ")", True)
        End If
    End Sub


    Protected Sub btExecutar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim txtErros As String
        Dim vdatasetmodal As New Data.DataSet
        Dim possuiErros As Boolean = False

        For i = 0 To optFatura.Items.Count - 1
            If optFatura.Items(i).Selected Then

                '-----executa rateio - funcionalidade de teste de fatura
                vdatasetmodal = WS_Rateio.Rateio(Session("Conn_Banco"),
                                                    optFatura.Items(i).Text,
                                                    Nothing,
                                                    Nothing,
                                                    oConfig.ValidaCampo(cboDataLote.SelectedValue),
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    "sp_Retorna_Fatura_Ativo_Consumidor",
                                                    True)
                If (vdatasetmodal.Tables(0).Rows(0).Item("ativo") = 0 Or
                    vdatasetmodal.Tables(0).Rows(0).Item("consumidor") = 0 Or
                    vdatasetmodal.Tables(0).Rows(0).Item("centrocusto") = 0 Or
                    vdatasetmodal.Tables(0).Rows(0).Item("colaborador") = 0
                    ) Then
                    possuiErros = True
                    txtErros += "(" & cboDataLote.SelectedValue & ") - " & optFatura.Items(i).Text & vbCrLf
                End If
            End If
        Next

        If (possuiErros) Then
            txtObservacaoObrigatoria.Text = txtErros
            pnlObservacao.Visible = True
        Else
            btExecutar.Enabled = False
            chkGravaRateio.Enabled = False
            dtgAtivo_Critica.Enabled = False

            '-----inicia time out para rateio
            WS_Rateio.Timeout = 3600000

            dtgConsulta.CurrentPageIndex = Nothing

            If optFatura.SelectedValue = Nothing Then Exit Sub
            If chkRateio.SelectedValue = Nothing Then Exit Sub
            Dim vDataSet As New Data.DataSet

            For i = 0 To optFatura.Items.Count - 1
                If optFatura.Items(i).Selected Then

                    '-----monta reteio

                    If Not optFatura.Items(i).Value = Nothing Then
                        txtDescricao.Text = "Rateio - " & optFatura.Items(i).Text
                    End If

                    If Trim(txtObservacao.Text) = "" Then
                        txtObservacao.Text = "Valor das linhas vagas:" & Format(CType(txtSVago.Text, System.Double), "##########0.#0") &
                                            " / Valor de estoque: " & Format(CType(txtSEstoque.Text, System.Double), "##########0.#0") &
                                            " / Valor de desconto: " & Format(CType(txtSDesconto.Text, System.Double), "##########0.#0")
                    End If

                    '-----executa rateio
                    vDataSet = WS_Rateio.Rateio(Session("Conn_Banco"),
                                            oConfig.ValidaCampo(txtDescricao.Text),
                                            cboFaturaTipo.SelectedValue,
                                            Nothing,
                                            oConfig.ValidaCampo(cboDataLote.SelectedValue),
                                            Retorna_Id_Check(chkRateio),
                                            optFatura.Items(i).Value,
                                            IIf(chkGravaRateio.Checked = True, 2, 1),
                                            Nothing,
                                            oConfig.ValidaCampo(txtObservacao.Text),
                                            "sp_Rateio_v2",
                                            True)

                    dtgConsulta.DataSource = vDataSet
                    dtgConsulta.DataBind()
                    Session("DataView") = vDataSet

                    If vDataSet.Tables(0).Rows.Count > 0 Then
                        txtSFatura.Text = Format(vDataSet.Tables(0).Rows(0).Item("Total_Fatura"), "##########,###########0")
                        txtSAuditado.Text = Format(vDataSet.Tables(0).Rows(0).Item("Valor_Auditado"), "##########,###########0")
                        txtSRateado.Text = Format(vDataSet.Tables(0).Rows(0).Item("Total_Rateado"), "##########,###########0")
                        txtSBilhete.Text = Format(vDataSet.Tables(0).Rows(0).Item("Total_Bilhete"), "##########,###########0")
                        txtSCritica.Text = Format(((CType(txtSFatura.Text, Double) - CType(txtSBilhete.Text, Double))), "##########,###########0")
                        txtSPorcentagemIndice.Text = Format((CType(txtSCritica.Text, Double) / CType(txtSRateado.Text, Double)) * 100, "###") & "%"

                        '"Valor das linhas vagas:0 / Valor de estoque: 0 / Valor de desconto: 0 / Valor da fatura: 0 / Valor contabilizado: 0 / Valor carregado de bilhete: 0 / Valor da auditoria: 0"
                        txtObservacao.Text = "Valor das linhas vagas:" & Format(CType(txtSVago.Text, System.Double), "##########0.#0") &
                                            " / Valor de estoque: " & Format(CType(txtSEstoque.Text, System.Double), "##########0.#0") &
                                            " / Valor de desconto: " & Format(CType(txtSDesconto.Text, System.Double), "##########0.#0") &
                                            " / Valor da fatura: " & Format(CType(txtSFatura.Text, System.Double), "##########0.#0") &
                                            " / Valor contabilizado: " & Format(CType(txtSRateado.Text, System.Double), "##########0.#0") &
                                            " / Valor carregado de bilhete: " & Format(CType(txtSBilhete.Text, System.Double), "##########0.#0") &
                                            " / Valor da auditoria: " & Format(CType(txtSAuditado.Text, System.Double), "##########0.#0")
                    End If

                    '-----grava vago
                    If chkGravaRateio.Checked = True Then
                        vDataSet = WS_Rateio.Rateio(Session("Conn_Banco"),
                                            oConfig.ValidaCampo(txtDescricao.Text),
                                            cboFaturaTipo.SelectedValue,
                                            Nothing,
                                            oConfig.ValidaCampo(cboDataLote.SelectedValue),
                                            Retorna_Id_Check(chkRateio),
                                            optFatura.Items(i).Value,
                                            2,
                                            Nothing,
                                            Nothing,
                                            "sp_Retorna_Ativo_Vago",
                                            True)
                    End If

                End If
            Next

            If chkGravaRateio.Checked = True Then
                btExecutar.Enabled = False
                '-----registro salvo ok
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
            End If
        End If
    End Sub

    Protected Sub btnConta_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnAcompanhamento_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnStatus_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub Valida_Tab(ByVal btn As Button)

        divDados.Visible = False
        divResumo.Visible = False
        divSemUsuario.Visible = False
        btnDados.CssClass = "btn-tab-disable pull-left"
        btnResumo.CssClass = "btn-tab-disable pull-left"
        btnSemUsuario.CssClass = "btn-tab-disable pull-left"

        If btn.Text = "Dados" Then
            divDados.Visible = True
            btnDados.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Resumo" Then
            divResumo.Visible = True
            btnResumo.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Sem Usuário" Then
            divSemUsuario.Visible = True
            btnSemUsuario.CssClass = "btn-tab pull-left"
        End If

    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub

    Protected Sub btCancelaModalObs_Click(sender As Object, e As EventArgs) Handles btCancelaModalObs.Click
        pnlObservacao.Visible = False
    End Sub
End Class

