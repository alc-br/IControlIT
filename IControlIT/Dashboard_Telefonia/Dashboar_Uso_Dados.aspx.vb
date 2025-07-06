Imports System.Web.UI.DataVisualization.Charting

Public Class Dashboar_Uso_Dados
    Inherits System.Web.UI.Page
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vDataSet As New Data.DataSet
    Dim vDataView As Data.DataView = Nothing
    Dim vDataTableTop As New System.Data.DataTable
    Dim vDataTable As New System.Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            'Pega a largura da tela
            Dim tamTela As String = Request.QueryString("larguraTela")

            If tamTela <> Nothing Then
                If Convert.ToInt32(tamTela) >= 600 Then
                    chtCurvaHora.Width = 730
                    chtAplicativo.Width = 730
                    'chtPark.Width = 730
                    'chtFH_FS.Width = 730
                Else
                    chtCurvaHora.Width = 450
                    chtAplicativo.Width = 450
                    'chtPark.Width = 450
                    'chtFH_FS.Width = 450
                End If
            End If

            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                                "Tráfego de Dados",
                                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Session("BI") = Nothing
            Session("DataSet") = Nothing

            '-----home
            Call Master.home("usuario")
            Dim v_dataSet As New Data.DataSet

            '-----lista descricao dos campos de hierarquia
            vDataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Texto_Hierarquia", Nothing, Nothing, Nothing, Nothing, Nothing)
            If vDataSet.Tables(0).Rows.Count > 0 Then
                lblFilial.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Filial")
                lblCentro_Custo.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Centro_Custo")
                lblDepartamento.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Departamento")
                lblSetor.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Setor")
                lblSecao.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Secao")
            End If

            '-----busca calendario
            Call Valida_Botao_Calendario(v_dataSet)

            Call Monta_Tela(Nothing)

        End If
    End Sub

    Protected Sub lstFilial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFilial.SelectedIndexChanged
        lstCentro_Custo.SelectedValue = Nothing
        If lstFilial.SelectedValue = "Todos" Then
            lstDepartamento.SelectedValue = Nothing
            lstSetor.SelectedValue = Nothing
            lstSecao.SelectedValue = Nothing
            Call Monta_Tela(Nothing)
        Else
            hfvFiltro.Value = " AND Nm_Filial = '" & lstFilial.SelectedItem.Text & "'"
            Call Monta_Tela(" AND Nm_Filial = '" & lstFilial.SelectedItem.Text & "'")
        End If

    End Sub

    Protected Sub lstSecao_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSecao.SelectedIndexChanged
        lstCentro_Custo.SelectedValue = Nothing
        If lstSecao.SelectedValue = "Todos" Then
            lstFilial.SelectedValue = Nothing
            lstDepartamento.SelectedValue = Nothing
            lstSetor.SelectedValue = Nothing
            Call Monta_Tela(Nothing)
        Else
            hfvFiltro.Value = " AND Nm_Secao = '" & lstSecao.SelectedItem.Text & "'"
            Call Monta_Tela(" AND Nm_Secao = '" & lstSecao.SelectedItem.Text & "'")
        End If
    End Sub

    Protected Sub lstSetor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSetor.SelectedIndexChanged
        lstCentro_Custo.SelectedValue = Nothing
        If lstSetor.SelectedValue = "Todos" Then
            lstFilial.SelectedValue = Nothing
            lstDepartamento.SelectedValue = Nothing
            lstSecao.SelectedValue = Nothing
            Call Monta_Tela(Nothing)
        Else
            hfvFiltro.Value = " AND Nm_Setor = '" & lstSetor.SelectedItem.Text & "'"
            Call Monta_Tela(" AND Nm_Setor = '" & lstSetor.SelectedItem.Text & "'")
        End If
    End Sub

    Protected Sub lstDepartamento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDepartamento.SelectedIndexChanged
        lstCentro_Custo.SelectedValue = Nothing
        If lstDepartamento.SelectedValue = "Todos" Then
            lstFilial.SelectedValue = Nothing
            lstSetor.SelectedValue = Nothing
            lstSecao.SelectedValue = Nothing
            Call Monta_Tela(Nothing)
        Else
            hfvFiltro.Value = " AND Nm_Departamento = '" & lstDepartamento.SelectedItem.Text & "'"
            Call Monta_Tela(" AND Nm_Departamento = '" & lstDepartamento.SelectedItem.Text & "'")
        End If
    End Sub

    Protected Sub lstCentro_Custo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCentro_Custo.SelectedIndexChanged
        If lstCentro_Custo.SelectedValue = "Todos" Then
            lstFilial.SelectedValue = Nothing
            lstDepartamento.SelectedValue = Nothing
            lstSetor.SelectedValue = Nothing
            lstSecao.SelectedValue = Nothing
            Call Monta_Tela(Nothing)
        Else
            hfvFiltro.Value = " AND Cd_Centro_Custo = '" & lstCentro_Custo.SelectedItem.Text & "'"
            Call Monta_Tela(" AND Cd_Centro_Custo = '" & lstCentro_Custo.SelectedItem.Text & "'")
        End If
    End Sub

    Protected Sub dtgTopGastoMes_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgTopGastoMes.PageIndexChanged
        dtgTopGastoMes.CurrentPageIndex = e.NewPageIndex
        dtgTopGastoMes.DataSource = Session("DataSet")
        dtgTopGastoMes.DataBind()
    End Sub

    Protected Sub dtgTopGastoMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgTopGastoMes.SelectedIndexChanged
        pnlListaAplicativo.Visible = True
        lblListaAplicativo.Text = "Aplicativos Utilizados"
        vDataSet = Session("CockPit")
        vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' AND Id_Ativo = '" & dtgTopGastoMes.SelectedItem.Cells(6).Text & "'", "Trafego_Usuario_Aplicativo DESC", Data.DataViewRowState.OriginalRows)
        vDataTableTop = vDataView.ToTable(True, "Nm_Aplicativo", "Trafego_Usuario_Aplicativo")
        dtgListaAplicativo.DataSource = vDataTableTop
        dtgListaAplicativo.DataBind()
    End Sub

    Protected Sub bt01_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt01.Click
        data_selecionada(sender, hfvMes01.Value)
    End Sub

    Protected Sub bt02_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt02.Click
        data_selecionada(sender, hfvMes02.Value)
    End Sub

    Protected Sub bt03_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt03.Click
        data_selecionada(sender, hfvMes03.Value)
    End Sub

    Protected Sub bt04_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt04.Click
        data_selecionada(sender, hfvMes04.Value)
    End Sub

    Protected Sub bt05_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt05.Click
        data_selecionada(sender, hfvMes05.Value)
    End Sub

    Protected Sub bt06_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt06.Click
        data_selecionada(sender, hfvMes06.Value)
    End Sub

    Protected Sub bt07_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt07.Click
        data_selecionada(sender, hfvMes07.Value)
    End Sub

    Protected Sub bt08_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt08.Click
        data_selecionada(sender, hfvMes08.Value)
    End Sub

    Protected Sub bt09_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt09.Click
        data_selecionada(sender, hfvMes09.Value)
    End Sub

    Protected Sub bt10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt10.Click
        data_selecionada(sender, hfvMes10.Value)
    End Sub

    Protected Sub bt11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt11.Click
        data_selecionada(sender, hfvMes11.Value)
    End Sub

    Protected Sub bt12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt12.Click
        data_selecionada(sender, hfvMes12.Value)
    End Sub

    Public Sub Valida_Botao_Calendario(ByVal v_dataSet As Data.DataSet)

        '-----busca calendario
        v_dataSet = WS_Modulo.Deskboard(Session("Conn_Banco"), "sp_Ano_Trafego_Dados", Nothing, Nothing, Nothing)
        If Session("Calendario") = Nothing Then
            Dim Linha As Data.DataRow
            For Each Linha In v_dataSet.Tables(0).Rows
                If Linha.Item(1) = "TRUE" Then
                    Session("Calendario") = Linha.Item(0)
                End If
            Next
        End If

        If v_dataSet.Tables(0).Rows.Count > 0 Then

            hfvMes01.Value = v_dataSet.Tables(0).Rows(0).Item("Mes")
            Monta_Mes(bt01, Right(v_dataSet.Tables(0).Rows(0).Item("Mes"), 2))
            bt01.Enabled = v_dataSet.Tables(0).Rows(0).Item("Mostrar")
            If bt01.Enabled = False Then
                bt01.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(0).Item("Mes") Then
                bt01.CssClass = "labelCalendarioSelected"
            End If

            hfvMes02.Value = v_dataSet.Tables(0).Rows(1).Item("Mes")
            Monta_Mes(bt02, Right(v_dataSet.Tables(0).Rows(1).Item("Mes"), 2))
            bt02.Enabled = v_dataSet.Tables(0).Rows(1).Item("Mostrar")
            If bt02.Enabled = False Then
                bt02.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(1).Item("Mes") Then
                bt02.CssClass = "labelCalendarioSelected"
            End If

            hfvMes03.Value = v_dataSet.Tables(0).Rows(2).Item("Mes")
            Monta_Mes(bt03, Right(v_dataSet.Tables(0).Rows(2).Item("Mes"), 2))
            bt03.Enabled = v_dataSet.Tables(0).Rows(2).Item("Mostrar")
            If bt03.Enabled = False Then
                bt03.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(2).Item("Mes") Then
                bt03.CssClass = "labelCalendarioSelected"
            End If

            hfvMes04.Value = v_dataSet.Tables(0).Rows(3).Item("Mes")
            Monta_Mes(bt04, Right(v_dataSet.Tables(0).Rows(3).Item("Mes"), 2))
            bt04.Enabled = v_dataSet.Tables(0).Rows(3).Item("Mostrar")
            If bt04.Enabled = False Then
                bt04.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(3).Item("Mes") Then
                bt04.CssClass = "labelCalendarioSelected"
            End If

            hfvMes05.Value = v_dataSet.Tables(0).Rows(4).Item("Mes")
            Monta_Mes(bt05, Right(v_dataSet.Tables(0).Rows(4).Item("Mes"), 2))
            bt05.Enabled = v_dataSet.Tables(0).Rows(4).Item("Mostrar")
            If bt05.Enabled = False Then
                bt05.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(4).Item("Mes") Then
                bt05.CssClass = "labelCalendarioSelected"
            End If

            hfvMes06.Value = v_dataSet.Tables(0).Rows(5).Item("Mes")
            Monta_Mes(bt06, Right(v_dataSet.Tables(0).Rows(5).Item("Mes"), 2))
            bt06.Enabled = v_dataSet.Tables(0).Rows(5).Item("Mostrar")
            If bt06.Enabled = False Then
                bt06.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(5).Item("Mes") Then
                bt06.CssClass = "labelCalendarioSelected"
            End If

            hfvMes07.Value = v_dataSet.Tables(0).Rows(6).Item("Mes")
            Monta_Mes(bt07, Right(v_dataSet.Tables(0).Rows(6).Item("Mes"), 2))
            bt07.Enabled = v_dataSet.Tables(0).Rows(6).Item("Mostrar")
            If bt07.Enabled = False Then
                bt07.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(6).Item("Mes") Then
                bt07.CssClass = "labelCalendarioSelected"
            End If

            hfvMes08.Value = v_dataSet.Tables(0).Rows(7).Item("Mes")
            Monta_Mes(bt08, Right(v_dataSet.Tables(0).Rows(7).Item("Mes"), 2))
            bt08.Enabled = v_dataSet.Tables(0).Rows(7).Item("Mostrar")
            If bt08.Enabled = False Then
                bt08.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(7).Item("Mes") Then
                bt08.CssClass = "labelCalendarioSelected"
            End If

            hfvMes09.Value = v_dataSet.Tables(0).Rows(8).Item("Mes")
            Monta_Mes(bt09, Right(v_dataSet.Tables(0).Rows(8).Item("Mes"), 2))
            bt09.Enabled = v_dataSet.Tables(0).Rows(8).Item("Mostrar")
            If bt09.Enabled = False Then
                bt09.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(8).Item("Mes") Then
                bt09.CssClass = "labelCalendarioSelected"
            End If

            hfvMes10.Value = v_dataSet.Tables(0).Rows(9).Item("Mes")
            Monta_Mes(bt10, Right(v_dataSet.Tables(0).Rows(9).Item("Mes"), 2))
            bt10.Enabled = v_dataSet.Tables(0).Rows(9).Item("Mostrar")
            If bt10.Enabled = False Then
                bt10.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(9).Item("Mes") Then
                bt10.CssClass = "labelCalendarioSelected"
            End If

            hfvMes11.Value = v_dataSet.Tables(0).Rows(10).Item("Mes")
            Monta_Mes(bt11, Right(v_dataSet.Tables(0).Rows(10).Item("Mes"), 2))
            bt11.Enabled = v_dataSet.Tables(0).Rows(10).Item("Mostrar")
            If bt11.Enabled = False Then
                bt11.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(10).Item("Mes") Then
                bt11.CssClass = "labelCalendarioSelected"
            End If

            hfvMes12.Value = v_dataSet.Tables(0).Rows(11).Item("Mes")
            Monta_Mes(bt12, Right(v_dataSet.Tables(0).Rows(11).Item("Mes"), 2))
            bt12.Enabled = v_dataSet.Tables(0).Rows(11).Item("Mostrar")
            If bt12.Enabled = False Then
                bt12.CssClass = "labelCalendarioDisabled"
            End If
            If Session("Calendario") = v_dataSet.Tables(0).Rows(11).Item("Mes") Then
                bt12.CssClass = "labelCalendarioSelected"
            End If
        End If

    End Sub

    Public Sub Monta_Mes(ByVal btn As Button, ByVal mes As String)

        If mes = "01" Then
            btn.Text = "Jan"
        ElseIf mes = "02" Then
            btn.Text = "Fev"
        ElseIf mes = "03" Then
            btn.Text = "Mar"
        ElseIf mes = "04" Then
            btn.Text = "Abr"
        ElseIf mes = "05" Then
            btn.Text = "Mai"
        ElseIf mes = "06" Then
            btn.Text = "Jun"
        ElseIf mes = "07" Then
            btn.Text = "Jul"
        ElseIf mes = "08" Then
            btn.Text = "Ago"
        ElseIf mes = "09" Then
            btn.Text = "Set"
        ElseIf mes = "10" Then
            btn.Text = "Out"
        ElseIf mes = "11" Then
            btn.Text = "Nov"
        ElseIf mes = "12" Then
            btn.Text = "Dez"
        End If

    End Sub

    Public Sub data_selecionada(ByVal botao As Button, ByVal mes As String)
        If bt01.Enabled = True Then bt01.CssClass = "labelCalendario"
        If bt02.Enabled = True Then bt02.CssClass = "labelCalendario"
        If bt03.Enabled = True Then bt03.CssClass = "labelCalendario"
        If bt04.Enabled = True Then bt04.CssClass = "labelCalendario"
        If bt05.Enabled = True Then bt05.CssClass = "labelCalendario"
        If bt06.Enabled = True Then bt06.CssClass = "labelCalendario"
        If bt07.Enabled = True Then bt07.CssClass = "labelCalendario"
        If bt08.Enabled = True Then bt08.CssClass = "labelCalendario"
        If bt09.Enabled = True Then bt09.CssClass = "labelCalendario"
        If bt10.Enabled = True Then bt10.CssClass = "labelCalendario"
        If bt11.Enabled = True Then bt11.CssClass = "labelCalendario"
        If bt12.Enabled = True Then bt12.CssClass = "labelCalendario"

        Session("CockPit") = WS_Modulo.Deskboard(Session("Conn_Banco"), "Dados_Moveis", IIf(Session("KPI") = "Telefonia_Movel_Trafego", "Telefonia_Movel", Session("KPI")), Session("Id_Usuario"), mes)
        Session("Calendario") = mes

        Dim vCalendario As String = "bt_Calendario_" & Right(Session("Calendario"), 2)
        Dim vCalendario_Select As String = "bt_Calendario_Select_" & Right(Session("Calendario"), 2)

        If Session("Calendario") = mes Then botao.CssClass = "labelCalendarioSelected"

        Call Monta_Tela(Nothing)
    End Sub

    Protected Sub btLocalizarOp_Click(sender As Object, e As ImageClickEventArgs) Handles btLocalizarOP.Click
        vDataSet = Session("CockPit")

        If txtLocalizarOP.Text = "" Then
            If hdfFiltro.Value = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Voz_Dados' ", IIf(hdfFiltroVoz.Value = "1", "Voz DESC", "Dados DESC"), Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Voz_Dados' " & hdfFiltro.Value, IIf(hdfFiltroVoz.Value = "1", "Voz DESC", "Dados DESC"), Data.DataViewRowState.OriginalRows)
            End If
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Voz_Dados' " & " AND Nm_Consumidor LIKE '%" & txtLocalizarOP.Text & "%'" & hdfFiltro.Value, IIf(hdfFiltroVoz.Value = "1", "Voz DESC", "Dados DESC"), Data.DataViewRowState.OriginalRows)
        End If

        vDataTableTop = vDataView.ToTable(True, "Id_Ativo", "Nr_Ativo", "Nm_Consumidor", "Nm_Setor", "Voz", "Dados")

        Session("DataSet_Operadora") = vDataTableTop
        dtgDadosUsuario.CurrentPageIndex = 0
        dtgDadosUsuario.DataSource = Session("DataSet_Operadora")
        dtgDadosUsuario.DataBind()

        For i = 0 To dtgDadosUsuario.Items.Count - 1

            Dim lbl As Label

            lbl = dtgDadosUsuario.Items(i).Cells(0).Controls(1)

            If lbl.Text.Length > 15 Then
                lbl.Text = lbl.Text.Substring(0, 15) + "..."
            End If

        Next

        Dim v_ConsumoVozOP As System.Int32 = 0
        Dim v_ConsumoDadosOP As System.Int32 = 0
        For i = 0 To vDataTableTop.Rows.Count - 1
            v_ConsumoVozOP = v_ConsumoVozOP + vDataTableTop.Rows(i).Item(4)
            v_ConsumoDadosOP = v_ConsumoDadosOP + vDataTableTop.Rows(i).Item(5)
        Next i
        txtConsumoVozOP.Text = Format(v_ConsumoVozOP, "########,#0")
        txtConsumoDadosOP.Text = Format(v_ConsumoDadosOP, "########,#0")
    End Sub

    Protected Sub dtgDadosUsuario_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgDadosUsuario.PageIndexChanged
        dtgDadosUsuario.CurrentPageIndex = e.NewPageIndex
        dtgDadosUsuario.DataSource = Session("DataSet_Operadora")
        dtgDadosUsuario.DataBind()

        For i = 0 To dtgDadosUsuario.Items.Count - 1

            Dim lbl As Label

            lbl = dtgDadosUsuario.Items(i).Cells(0).Controls(1)

            If lbl.Text.Length > 15 Then
                lbl.Text = lbl.Text.Substring(0, 15) + "..."
            End If

        Next
    End Sub

    Protected Sub btLocalizar_Click(sender As Object, e As ImageClickEventArgs) Handles btLocalizar.Click
        vDataSet = Session("CockPit")

        If txtLocalizar.Text = "" Then
            If hdfFiltro.Value = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' ", "Trafego_Usuario DESC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' " & hdfFiltro.Value, "Trafego_Usuario DESC", Data.DataViewRowState.OriginalRows)
            End If
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' " & " AND Nm_Consumidor LIKE '%" & txtLocalizar.Text & "%'" & hdfFiltro.Value, "Trafego_Usuario DESC", Data.DataViewRowState.OriginalRows)
        End If

        vDataTableTop = vDataView.ToTable(True, "Id_Ativo", "Nr_Ativo", "Nm_Conglomerado", "Nm_Ativo_Tipo", "Nm_Consumidor", "Pacote_MB")

        Session("DataSet") = vDataTableTop
        dtgTopGastoMes.CurrentPageIndex = 0
        dtgTopGastoMes.DataSource = vDataTableTop
        dtgTopGastoMes.DataBind()
    End Sub

    Protected Sub btFecharLista_Click(sender As Object, e As EventArgs) Handles btFecharLista.Click
        pnlListaAplicativo.Visible = False
    End Sub

    Protected Sub btVoz_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btVoz_") + 7, 8), System.Int32)

        Call monta_Detalhamento(hfvFiltro.Value, dtgDadosUsuario.Items(i).Cells(1).Text, "Voz_Operadora")

        '-----curva de utilizacao por hora--------------------------------------
        '***********************************************************************
        Call monta_Grafico_Hora()
    End Sub

    Protected Sub btOrdernarVoz_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        hdfFiltroVoz.Value = "1"
        hdfFiltroDados.Value = "0"

        '-----consumo por usuario-----------------------------------------------
        '***********************************************************************
        Call utilizacao_usuario()

        '-----detalhamento consumo total----------------------------------------
        '***********************************************************************
        Call monta_Detalhamento(hfvFiltro.Value, Nothing, "Voz_Operadora")

        '-----curva de utilizacao por hora--------------------------------------
        '***********************************************************************
        Call monta_Grafico_Hora()
    End Sub

    Protected Sub btDados_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btDados_") + 9, 8), System.Int32)

        Call monta_Detalhamento(hfvFiltro.Value, dtgDadosUsuario.Items(i).Cells(1).Text, "Dados_Operadora")

        '-----curva de utilizacao por hora--------------------------------------
        '***********************************************************************
        Call monta_Grafico_Hora()
    End Sub

    Protected Sub btOrdernarDados_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        hdfFiltroVoz.Value = "0"
        hdfFiltroDados.Value = "1"

        '-----consumo por usuario-----------------------------------------------
        '***********************************************************************
        Call utilizacao_usuario()

        '-----detalhamento consumo total----------------------------------------
        '***********************************************************************
        Call monta_Detalhamento(hfvFiltro.Value, Nothing, "Dados_Operadora")

        '-----curva de utilizacao por hora--------------------------------------
        '***********************************************************************
        Call monta_Grafico_Hora()
    End Sub

    Public Sub ZeraText(ByVal me_Form As ContentPlaceHolder)
        Dim vObjeto As Label
        Dim vControleContent As Control
        Dim X As System.Int32 = 0

        For Each vControleContent In me_Form.Controls
            If vControleContent.Controls.Count > 0 Then
                For X = 0 To vControleContent.Controls.Count - 1
                    If vControleContent.Controls.Item(X).ToString = "System.Web.UI.WebControls.Label" Then
                        vObjeto = vControleContent.Controls.Item(X)
                        If vObjeto.ID.IndexOf("_L") > 0 Then
                            vObjeto.Text = "0"
                        End If
                    End If
                Next
            Else
                If vControleContent.ToString = "System.Web.UI.WebControls.Label" Then
                    vObjeto = vControleContent
                    If vObjeto.Text.IndexOf("L_") > 0 Then
                        vObjeto.Text = "0"
                    End If
                End If
            End If
        Next

        '-----volta cor do cabecario
        lblDia_Cab_1.ForeColor = Drawing.Color.White
        lblDia_Cab_2.ForeColor = Drawing.Color.White
        lblDia_Cab_3.ForeColor = Drawing.Color.White
        lblDia_Cab_4.ForeColor = Drawing.Color.White
        lblDia_Cab_5.ForeColor = Drawing.Color.White
        lblDia_Cab_6.ForeColor = Drawing.Color.White
        lblDia_Cab_7.ForeColor = Drawing.Color.White
        lblDia_Cab_8.ForeColor = Drawing.Color.White
        lblDia_Cab_9.ForeColor = Drawing.Color.White
        lblDia_Cab_10.ForeColor = Drawing.Color.White
        lblDia_Cab_11.ForeColor = Drawing.Color.White
        lblDia_Cab_12.ForeColor = Drawing.Color.White
        lblDia_Cab_13.ForeColor = Drawing.Color.White
        lblDia_Cab_14.ForeColor = Drawing.Color.White
        lblDia_Cab_15.ForeColor = Drawing.Color.White
        lblDia_Cab_16.ForeColor = Drawing.Color.White
        lblDia_Cab_17.ForeColor = Drawing.Color.White
        lblDia_Cab_18.ForeColor = Drawing.Color.White
        lblDia_Cab_19.ForeColor = Drawing.Color.White
        lblDia_Cab_20.ForeColor = Drawing.Color.White
        lblDia_Cab_21.ForeColor = Drawing.Color.White
        lblDia_Cab_22.ForeColor = Drawing.Color.White
        lblDia_Cab_23.ForeColor = Drawing.Color.White
        lblDia_Cab_24.ForeColor = Drawing.Color.White
        lblDia_Cab_25.ForeColor = Drawing.Color.White
        lblDia_Cab_26.ForeColor = Drawing.Color.White
        lblDia_Cab_27.ForeColor = Drawing.Color.White
        lblDia_Cab_28.ForeColor = Drawing.Color.White
        lblDia_Cab_29.ForeColor = Drawing.Color.White
        lblDia_Cab_30.ForeColor = Drawing.Color.White
        lblDia_Cab_31.ForeColor = Drawing.Color.White
    End Sub

    Public Sub utilizacao_usuario()
        vDataSet = Session("CockPit")

        '-----consumo por usuario-----------------------------------------------
        '****************************************************************************
        If hdfFiltro.Value = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Voz_Dados' ", IIf(hdfFiltroVoz.Value = "1", "Voz DESC", "Dados DESC"), Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Voz_Dados' " & hdfFiltro.Value, IIf(hdfFiltroVoz.Value = "1", "Voz DESC", "Dados DESC"), Data.DataViewRowState.OriginalRows)
        End If

        vDataTableTop = vDataView.ToTable(True, "Id_Ativo", "Nr_Ativo", "Nm_Consumidor", "Nm_Setor", "Voz", "Dados")

        Session("DataSet_Operadora") = vDataTableTop
        dtgDadosUsuario.CurrentPageIndex = 0
        dtgDadosUsuario.DataSource = Session("DataSet_Operadora")
        dtgDadosUsuario.DataBind()

        For i = 0 To dtgDadosUsuario.Items.Count - 1

            Dim lbl As Label

            lbl = dtgDadosUsuario.Items(i).Cells(0).Controls(1)

            If lbl.Text.Length > 15 Then
                lbl.Text = lbl.Text.Substring(0, 15) + "..."
            End If

        Next

        Dim v_ConsumoVozOP As System.Int32 = 0
        Dim v_ConsumoDadosOP As System.Int32 = 0
        For i = 0 To vDataTableTop.Rows.Count - 1
            v_ConsumoVozOP = v_ConsumoVozOP + vDataTableTop.Rows(i).Item(4)
            v_ConsumoDadosOP = v_ConsumoDadosOP + vDataTableTop.Rows(i).Item(5)
        Next i
        txtConsumoVozOP.Text = Format(v_ConsumoVozOP, "########,#0")
        txtConsumoDadosOP.Text = Format(v_ConsumoDadosOP, "########,#0")
    End Sub

    Public Sub Monta_Tela(pFiltro As String)
        Dim i As System.Int32
        Dim Cont As System.Int32
        Dim Ativo As System.String = ""

        Dim Dt_Lote(0) As System.String
        Dim Custo(0) As System.Double

        vDataSet = Session("CockPit")
        hdfFiltro.Value = pFiltro
        Call Master.Localizar(Nothing, Nothing)

        '-----Filtro-----------------------------------------------------------------------
        '**********************************************************************************
        If lstFilial.SelectedValue = Nothing Then
            lstFilial.Items.Clear()
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Filial' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
                oConfig.CarregaListDataView(lstFilial, vDataView)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
                vDataTable = vDataView.ToTable(True, "Id_Filial", "Nm_Filial")
                oConfig.FiltroCDC(lstFilial, vDataTable, "Filial")
            End If
        End If

        If lstDepartamento.SelectedValue = Nothing Then
            lstDepartamento.Items.Clear()
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Departamento' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
                oConfig.CarregaListDataView(lstDepartamento, vDataView)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
                vDataTable = vDataView.ToTable(True, "Id_Departamento", "Nm_Departamento")
                oConfig.FiltroCDC(lstDepartamento, vDataTable, "Departamento")
            End If
        End If

        If lstSetor.SelectedValue = Nothing Then
            lstSetor.Items.Clear()
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Setor' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
                oConfig.CarregaListDataView(lstSetor, vDataView)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
                vDataTable = vDataView.ToTable(True, "Id_Setor", "Nm_Setor")
                oConfig.FiltroCDC(lstSetor, vDataTable, "Setor")
            End If
        End If

        If lstSecao.SelectedValue = Nothing Then
            lstSecao.Items.Clear()
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Secao' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
                oConfig.CarregaListDataView(lstSecao, vDataView)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
                vDataTable = vDataView.ToTable(True, "Id_Secao", "Nm_Secao")
                oConfig.FiltroCDC(lstSecao, vDataTable, "Secao")
            End If
        End If

        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Centro_Custo' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
            oConfig.CarregaListDataView(lstCentro_Custo, vDataView)
        Else
            lstCentro_Custo.Items.Clear()
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' " & pFiltro, "Cd_Centro_Custo ASC", Data.DataViewRowState.OriginalRows)
            vDataTable = vDataView.ToTable(True, "Filtro", "Cd_Centro_Custo")
            oConfig.FiltroCDC(lstCentro_Custo, vDataTable, Nothing)
        End If

        '-----consumo por usuario-----------------------------------------------
        '***********************************************************************
        Call utilizacao_usuario()

        '-----detalhamento consumo total----------------------------------------
        '***********************************************************************
        Call monta_Detalhamento(hfvFiltro.Value, Nothing, "Voz_Operadora")

        '-----curva de utilizacao por hora--------------------------------------
        '***********************************************************************
        Call monta_Grafico_Hora()

        '-----grafico consumo por tipo-----------------------------------------------
        '****************************************************************************
        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' ", "Nm_Aplicativo ASC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' " & pFiltro, "Nm_Aplicativo ASC", Data.DataViewRowState.OriginalRows)
        End If

        i = 0
        Cont = 0
        ReDim Dt_Lote(1000)
        ReDim Custo(1000)

        '-----cria dataset
        Dim vLinha As Data.DataRow
        Dim vDataSetNow As New System.Data.DataSet
        '-----cria datatable
        Dim vaDataTable As Data.DataTable = New Data.DataTable("vDataTableInclui")
        '-----cria colunas
        Dim vAplicativo As Data.DataColumn = New Data.DataColumn("Aplicativo", GetType(System.String))
        Dim vConsumo As Data.DataColumn = New Data.DataColumn("Consumo", GetType(System.Double))
        '-----cria dataset
        vDataSetNow.DataSetName = "vDataSet"
        '-----adiciona colunas na tabela
        vaDataTable.Columns.Add(vAplicativo)
        vaDataTable.Columns.Add(vConsumo)

        vDataSetNow.Tables.Add(vaDataTable)

        If vDataView.Count > 0 Then
            For i = 0 To vDataView.Count - 1
                If vDataView.Item(i).Item(29) <> Dt_Lote(Cont) Then
                    If i > 0 Then Cont = Cont + 1
                    Dt_Lote(Cont) = vDataView.Item(i).Item(29)
                    Custo(Cont) = vDataView.Item(i).Item(33)
                End If
            Next

            '-----popula tabela com soma das informacoes
            For i = 0 To Cont
                vLinha = vaDataTable.NewRow
                vLinha("Aplicativo") = Dt_Lote(i)
                vLinha("Consumo") = Custo(i)

                vaDataTable.Rows.Add(vLinha)
            Next i
            vDataSetNow.AcceptChanges()

            vDataView = New Data.DataView(vDataSetNow.Tables(0), Nothing, "Consumo DESC", Data.DataViewRowState.OriginalRows)

            Dim vaAplicativo(9) As System.String
            Dim vaConsumo(9) As System.Double

            If Cont > 0 Then
                vaAplicativo(0) = vDataView.Item(0).Item(0)
                vaConsumo(0) = vDataView.Item(0).Item(1)
            End If
            If Cont > 1 Then
                vaAplicativo(1) = vDataView.Item(1).Item(0)
                vaConsumo(1) = vDataView.Item(1).Item(1)
            End If
            If Cont > 2 Then
                vaAplicativo(2) = vDataView.Item(2).Item(0)
                vaConsumo(2) = vDataView.Item(2).Item(1)
            End If
            If Cont > 3 Then
                vaAplicativo(3) = vDataView.Item(3).Item(0)
                vaConsumo(3) = vDataView.Item(3).Item(1)
            End If
            If Cont > 4 Then
                vaAplicativo(4) = vDataView.Item(4).Item(0)
                vaConsumo(4) = vDataView.Item(4).Item(1)
            End If
            If Cont > 5 Then
                vaAplicativo(5) = vDataView.Item(5).Item(0)
                vaConsumo(5) = vDataView.Item(5).Item(1)
            End If
            If Cont > 6 Then
                vaAplicativo(6) = vDataView.Item(6).Item(0)
                vaConsumo(6) = vDataView.Item(6).Item(1)
            End If
            If Cont > 7 Then
                vaAplicativo(7) = vDataView.Item(7).Item(0)
                vaConsumo(7) = vDataView.Item(7).Item(1)
            End If
            If Cont > 8 Then
                vaAplicativo(8) = vDataView.Item(8).Item(0)
                vaConsumo(8) = vDataView.Item(8).Item(1)
            End If
            If Cont > 9 Then
                vaAplicativo(9) = vDataView.Item(9).Item(0)
                vaConsumo(9) = vDataView.Item(9).Item(1)
            End If

            chtAplicativo.Series(0).Points.DataBindXY(vaAplicativo, vaConsumo)
            chtAplicativo.Series(0).BackGradientStyle = GradientStyle.TopBottom
            chtAplicativo.Series(0).Color = Drawing.ColorTranslator.FromHtml("#3d72b4")
            chtAplicativo.Series(0).BackSecondaryColor = Drawing.ColorTranslator.FromHtml("#525252")
        Else
            chtAplicativo.Series(0).Points.Clear()
        End If

        '-----detalhamento Usuario----------------------------------------------------------
        '***********************************************************************************
        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' ", "Trafego_Usuario DESC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Dados_App' " & pFiltro, "Trafego_Usuario DESC", Data.DataViewRowState.OriginalRows)
        End If

        vDataTableTop = vDataView.ToTable(True, "Id_Ativo", "Nr_Ativo", "Nm_Conglomerado", "Nm_Ativo_Tipo", "Nm_Consumidor", "Pacote_MB", "Trafego_Usuario")

        Session("DataSet") = vDataTableTop
        dtgTopGastoMes.CurrentPageIndex = 0
        dtgTopGastoMes.DataSource = Session("DataSet")
        dtgTopGastoMes.DataBind()

        divFiltro.Visible = False
    End Sub

    Public Sub monta_Detalhamento(pFiltro As String, pAtivo As String, pTipo As String)
        vDataSet = Session("CockPit")
        ZeraText(Master.FindControl("ContentPlaceHolder1"))

        '-----detalhamento por usuario-----------------------------------------------
        '****************************************************************************
        If pTipo = "Voz_Operadora" Then
            lblTipoFiltroConsumo.Text = "Voz | " & IIf(pAtivo = Nothing, " Sem Seleção de Linha | ", "Linha - " + pAtivo + " | ") & IIf(pFiltro = Nothing, " Sem Seleção de Área ", " - " & pFiltro.Replace(" AND Nm_Filial = ", "").Replace(" AND Cd_Centro_Custo = ", "").Replace(" AND Nm_Secao = ", "").Replace(" AND Nm_Setor = ", "").Replace(" AND Nm_Departamento = ", ""))
        Else
            lblTipoFiltroConsumo.Text = "Dados | " & IIf(pAtivo = Nothing, " Sem Seleção de Linha | ", "Linha - " + pAtivo + " | ") & IIf(pFiltro = Nothing, " Sem Seleção de Área ", " - " & pFiltro.Replace(" AND Nm_Filial = ", "").Replace(" AND Cd_Centro_Custo = ", "").Replace(" AND Nm_Secao = ", "").Replace(" AND Nm_Setor = ", "").Replace(" AND Nm_Departamento = ", ""))
        End If

        If pAtivo = Nothing Then
            '------por area
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = '" & pTipo & "'", "Voz, Data", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = '" & pTipo & "' " & pFiltro, "Voz, Data", Data.DataViewRowState.OriginalRows)
            End If
        Else
            '-----por usuario
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = '" & pTipo & "' and Nr_Ativo = '" & pAtivo & "' ", "Voz, Data", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = '" & pTipo & "' and Nr_Ativo = '" & pAtivo & "' " & pFiltro, "Voz, Data", Data.DataViewRowState.OriginalRows)
            End If
        End If

        For i = 0 To vDataView.Count - 1
            If vDataView.Item(i).Item(41) = 31 Then
                '-----dia
                lblDia_Cab_1.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_1.ForeColor = Drawing.Color.Yellow
                '-----hora
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C1.Text = Convert.ToDouble(lblDia_L1_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C1.Text = Convert.ToDouble(lblDia_L2_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C1.Text = Convert.ToDouble(lblDia_L3_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C1.Text = Convert.ToDouble(lblDia_L4_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C1.Text = Convert.ToDouble(lblDia_L5_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C1.Text = Convert.ToDouble(lblDia_L6_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C1.Text = Convert.ToDouble(lblDia_L7_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C1.Text = Convert.ToDouble(lblDia_L8_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C1.Text = Convert.ToDouble(lblDia_L9_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C1.Text = Convert.ToDouble(lblDia_L10_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C1.Text = Convert.ToDouble(lblDia_L11_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C1.Text = Convert.ToDouble(lblDia_L12_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C1.Text = Convert.ToDouble(lblDia_L13_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C1.Text = Convert.ToDouble(lblDia_L14_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C1.Text = Convert.ToDouble(lblDia_L15_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C1.Text = Convert.ToDouble(lblDia_L16_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C1.Text = Convert.ToDouble(lblDia_L17_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C1.Text = Convert.ToDouble(lblDia_L18_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C1.Text = Convert.ToDouble(lblDia_L19_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C1.Text = Convert.ToDouble(lblDia_L20_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C1.Text = Convert.ToDouble(lblDia_L21_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C1.Text = Convert.ToDouble(lblDia_L22_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C1.Text = Convert.ToDouble(lblDia_L23_C1.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C1.Text = Convert.ToDouble(lblDia_L24_C1.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 30 Then
                '-----dia
                lblDia_Cab_2.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_2.ForeColor = Drawing.Color.Yellow
                '-----hora
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C2.Text = Convert.ToDouble(lblDia_L1_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C2.Text = Convert.ToDouble(lblDia_L2_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C2.Text = Convert.ToDouble(lblDia_L3_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C2.Text = Convert.ToDouble(lblDia_L4_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C2.Text = Convert.ToDouble(lblDia_L5_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C2.Text = Convert.ToDouble(lblDia_L6_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C2.Text = Convert.ToDouble(lblDia_L7_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C2.Text = Convert.ToDouble(lblDia_L8_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C2.Text = Convert.ToDouble(lblDia_L9_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C2.Text = Convert.ToDouble(lblDia_L10_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C2.Text = Convert.ToDouble(lblDia_L11_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C2.Text = Convert.ToDouble(lblDia_L12_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C2.Text = Convert.ToDouble(lblDia_L13_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C2.Text = Convert.ToDouble(lblDia_L14_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C2.Text = Convert.ToDouble(lblDia_L15_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C2.Text = Convert.ToDouble(lblDia_L16_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C2.Text = Convert.ToDouble(lblDia_L17_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C2.Text = Convert.ToDouble(lblDia_L18_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C2.Text = Convert.ToDouble(lblDia_L19_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C2.Text = Convert.ToDouble(lblDia_L20_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C2.Text = Convert.ToDouble(lblDia_L21_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C2.Text = Convert.ToDouble(lblDia_L22_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C2.Text = Convert.ToDouble(lblDia_L23_C2.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C2.Text = Convert.ToDouble(lblDia_L24_C2.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 29 Then
                '-----dia
                lblDia_Cab_3.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_3.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C3.Text = Convert.ToDouble(lblDia_L1_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C3.Text = Convert.ToDouble(lblDia_L2_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C3.Text = Convert.ToDouble(lblDia_L3_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C3.Text = Convert.ToDouble(lblDia_L4_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C3.Text = Convert.ToDouble(lblDia_L5_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C3.Text = Convert.ToDouble(lblDia_L6_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C3.Text = Convert.ToDouble(lblDia_L7_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C3.Text = Convert.ToDouble(lblDia_L8_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C3.Text = Convert.ToDouble(lblDia_L9_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C3.Text = Convert.ToDouble(lblDia_L10_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C3.Text = Convert.ToDouble(lblDia_L11_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C3.Text = Convert.ToDouble(lblDia_L12_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C3.Text = Convert.ToDouble(lblDia_L13_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C3.Text = Convert.ToDouble(lblDia_L14_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C3.Text = Convert.ToDouble(lblDia_L15_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C3.Text = Convert.ToDouble(lblDia_L16_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C3.Text = Convert.ToDouble(lblDia_L17_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C3.Text = Convert.ToDouble(lblDia_L18_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C3.Text = Convert.ToDouble(lblDia_L19_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C3.Text = Convert.ToDouble(lblDia_L20_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C3.Text = Convert.ToDouble(lblDia_L21_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C3.Text = Convert.ToDouble(lblDia_L22_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C3.Text = Convert.ToDouble(lblDia_L23_C3.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C3.Text = Convert.ToDouble(lblDia_L24_C3.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 28 Then
                '-----dia
                lblDia_Cab_4.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_4.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C4.Text = Convert.ToDouble(lblDia_L1_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C4.Text = Convert.ToDouble(lblDia_L2_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C4.Text = Convert.ToDouble(lblDia_L3_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C4.Text = Convert.ToDouble(lblDia_L4_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C4.Text = Convert.ToDouble(lblDia_L5_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C4.Text = Convert.ToDouble(lblDia_L6_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C4.Text = Convert.ToDouble(lblDia_L7_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C4.Text = Convert.ToDouble(lblDia_L8_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C4.Text = Convert.ToDouble(lblDia_L9_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C4.Text = Convert.ToDouble(lblDia_L10_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C4.Text = Convert.ToDouble(lblDia_L11_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C4.Text = Convert.ToDouble(lblDia_L12_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C4.Text = Convert.ToDouble(lblDia_L13_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C4.Text = Convert.ToDouble(lblDia_L14_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C4.Text = Convert.ToDouble(lblDia_L15_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C4.Text = Convert.ToDouble(lblDia_L16_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C4.Text = Convert.ToDouble(lblDia_L17_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C4.Text = Convert.ToDouble(lblDia_L18_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C4.Text = Convert.ToDouble(lblDia_L19_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C4.Text = Convert.ToDouble(lblDia_L20_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C4.Text = Convert.ToDouble(lblDia_L21_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C4.Text = Convert.ToDouble(lblDia_L22_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C4.Text = Convert.ToDouble(lblDia_L23_C4.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C4.Text = Convert.ToDouble(lblDia_L24_C4.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 27 Then
                '-----dia
                lblDia_Cab_5.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_5.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C5.Text = Convert.ToDouble(lblDia_L1_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C5.Text = Convert.ToDouble(lblDia_L2_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C5.Text = Convert.ToDouble(lblDia_L3_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C5.Text = Convert.ToDouble(lblDia_L4_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C5.Text = Convert.ToDouble(lblDia_L5_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C5.Text = Convert.ToDouble(lblDia_L6_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C5.Text = Convert.ToDouble(lblDia_L7_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C5.Text = Convert.ToDouble(lblDia_L8_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C5.Text = Convert.ToDouble(lblDia_L9_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C5.Text = Convert.ToDouble(lblDia_L10_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C5.Text = Convert.ToDouble(lblDia_L11_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C5.Text = Convert.ToDouble(lblDia_L12_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C5.Text = Convert.ToDouble(lblDia_L13_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C5.Text = Convert.ToDouble(lblDia_L14_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C5.Text = Convert.ToDouble(lblDia_L15_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C5.Text = Convert.ToDouble(lblDia_L16_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C5.Text = Convert.ToDouble(lblDia_L17_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C5.Text = Convert.ToDouble(lblDia_L18_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C5.Text = Convert.ToDouble(lblDia_L19_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C5.Text = Convert.ToDouble(lblDia_L20_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C5.Text = Convert.ToDouble(lblDia_L21_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C5.Text = Convert.ToDouble(lblDia_L22_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C5.Text = Convert.ToDouble(lblDia_L23_C5.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C5.Text = Convert.ToDouble(lblDia_L24_C5.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 26 Then
                '-----dia
                lblDia_Cab_6.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_6.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C6.Text = Convert.ToDouble(lblDia_L1_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C6.Text = Convert.ToDouble(lblDia_L2_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C6.Text = Convert.ToDouble(lblDia_L3_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C6.Text = Convert.ToDouble(lblDia_L4_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C6.Text = Convert.ToDouble(lblDia_L5_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C6.Text = Convert.ToDouble(lblDia_L6_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C6.Text = Convert.ToDouble(lblDia_L7_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C6.Text = Convert.ToDouble(lblDia_L8_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C6.Text = Convert.ToDouble(lblDia_L9_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C6.Text = Convert.ToDouble(lblDia_L10_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C6.Text = Convert.ToDouble(lblDia_L11_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C6.Text = Convert.ToDouble(lblDia_L12_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C6.Text = Convert.ToDouble(lblDia_L13_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C6.Text = Convert.ToDouble(lblDia_L14_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C6.Text = Convert.ToDouble(lblDia_L15_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C6.Text = Convert.ToDouble(lblDia_L16_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C6.Text = Convert.ToDouble(lblDia_L17_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C6.Text = Convert.ToDouble(lblDia_L18_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C6.Text = Convert.ToDouble(lblDia_L19_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C6.Text = Convert.ToDouble(lblDia_L20_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C6.Text = Convert.ToDouble(lblDia_L21_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C6.Text = Convert.ToDouble(lblDia_L22_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C6.Text = Convert.ToDouble(lblDia_L23_C6.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C6.Text = Convert.ToDouble(lblDia_L24_C6.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 25 Then
                '-----dia
                lblDia_Cab_7.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_7.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C7.Text = Convert.ToDouble(lblDia_L1_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C7.Text = Convert.ToDouble(lblDia_L2_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C7.Text = Convert.ToDouble(lblDia_L3_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C7.Text = Convert.ToDouble(lblDia_L4_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C7.Text = Convert.ToDouble(lblDia_L5_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C7.Text = Convert.ToDouble(lblDia_L6_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C7.Text = Convert.ToDouble(lblDia_L7_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C7.Text = Convert.ToDouble(lblDia_L8_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C7.Text = Convert.ToDouble(lblDia_L9_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C7.Text = Convert.ToDouble(lblDia_L10_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C7.Text = Convert.ToDouble(lblDia_L11_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C7.Text = Convert.ToDouble(lblDia_L12_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C7.Text = Convert.ToDouble(lblDia_L13_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C7.Text = Convert.ToDouble(lblDia_L14_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C7.Text = Convert.ToDouble(lblDia_L15_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C7.Text = Convert.ToDouble(lblDia_L16_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C7.Text = Convert.ToDouble(lblDia_L17_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C7.Text = Convert.ToDouble(lblDia_L18_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C7.Text = Convert.ToDouble(lblDia_L19_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C7.Text = Convert.ToDouble(lblDia_L20_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C7.Text = Convert.ToDouble(lblDia_L21_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C7.Text = Convert.ToDouble(lblDia_L22_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C7.Text = Convert.ToDouble(lblDia_L23_C7.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C7.Text = Convert.ToDouble(lblDia_L24_C7.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 24 Then
                '-----dia
                lblDia_Cab_8.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_8.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C8.Text = Convert.ToDouble(lblDia_L1_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C8.Text = Convert.ToDouble(lblDia_L2_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C8.Text = Convert.ToDouble(lblDia_L3_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C8.Text = Convert.ToDouble(lblDia_L4_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C8.Text = Convert.ToDouble(lblDia_L5_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C8.Text = Convert.ToDouble(lblDia_L6_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C8.Text = Convert.ToDouble(lblDia_L7_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C8.Text = Convert.ToDouble(lblDia_L8_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C8.Text = Convert.ToDouble(lblDia_L9_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C8.Text = Convert.ToDouble(lblDia_L10_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C8.Text = Convert.ToDouble(lblDia_L11_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C8.Text = Convert.ToDouble(lblDia_L12_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C8.Text = Convert.ToDouble(lblDia_L13_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C8.Text = Convert.ToDouble(lblDia_L14_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C8.Text = Convert.ToDouble(lblDia_L15_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C8.Text = Convert.ToDouble(lblDia_L16_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C8.Text = Convert.ToDouble(lblDia_L17_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C8.Text = Convert.ToDouble(lblDia_L18_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C8.Text = Convert.ToDouble(lblDia_L19_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C8.Text = Convert.ToDouble(lblDia_L20_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C8.Text = Convert.ToDouble(lblDia_L21_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C8.Text = Convert.ToDouble(lblDia_L22_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C8.Text = Convert.ToDouble(lblDia_L23_C8.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C8.Text = Convert.ToDouble(lblDia_L24_C8.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 23 Then
                '-----dia
                lblDia_Cab_9.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_9.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C9.Text = Convert.ToDouble(lblDia_L1_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C9.Text = Convert.ToDouble(lblDia_L2_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C9.Text = Convert.ToDouble(lblDia_L3_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C9.Text = Convert.ToDouble(lblDia_L4_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C9.Text = Convert.ToDouble(lblDia_L5_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C9.Text = Convert.ToDouble(lblDia_L6_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C9.Text = Convert.ToDouble(lblDia_L7_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C9.Text = Convert.ToDouble(lblDia_L8_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C9.Text = Convert.ToDouble(lblDia_L9_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C9.Text = Convert.ToDouble(lblDia_L10_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C9.Text = Convert.ToDouble(lblDia_L11_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C9.Text = Convert.ToDouble(lblDia_L12_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C9.Text = Convert.ToDouble(lblDia_L13_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C9.Text = Convert.ToDouble(lblDia_L14_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C9.Text = Convert.ToDouble(lblDia_L15_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C9.Text = Convert.ToDouble(lblDia_L16_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C9.Text = Convert.ToDouble(lblDia_L17_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C9.Text = Convert.ToDouble(lblDia_L18_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C9.Text = Convert.ToDouble(lblDia_L19_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C9.Text = Convert.ToDouble(lblDia_L20_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C9.Text = Convert.ToDouble(lblDia_L21_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C9.Text = Convert.ToDouble(lblDia_L22_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C9.Text = Convert.ToDouble(lblDia_L23_C9.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C9.Text = Convert.ToDouble(lblDia_L24_C9.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 22 Then
                '-----dia
                lblDia_Cab_10.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_10.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C10.Text = Convert.ToDouble(lblDia_L1_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C10.Text = Convert.ToDouble(lblDia_L2_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C10.Text = Convert.ToDouble(lblDia_L3_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C10.Text = Convert.ToDouble(lblDia_L4_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C10.Text = Convert.ToDouble(lblDia_L5_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C10.Text = Convert.ToDouble(lblDia_L6_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C10.Text = Convert.ToDouble(lblDia_L7_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C10.Text = Convert.ToDouble(lblDia_L8_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C10.Text = Convert.ToDouble(lblDia_L9_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C10.Text = Convert.ToDouble(lblDia_L10_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C10.Text = Convert.ToDouble(lblDia_L11_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C10.Text = Convert.ToDouble(lblDia_L12_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C10.Text = Convert.ToDouble(lblDia_L13_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C10.Text = Convert.ToDouble(lblDia_L14_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C10.Text = Convert.ToDouble(lblDia_L15_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C10.Text = Convert.ToDouble(lblDia_L16_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C10.Text = Convert.ToDouble(lblDia_L17_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C10.Text = Convert.ToDouble(lblDia_L18_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C10.Text = Convert.ToDouble(lblDia_L19_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C10.Text = Convert.ToDouble(lblDia_L20_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C10.Text = Convert.ToDouble(lblDia_L21_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C10.Text = Convert.ToDouble(lblDia_L22_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C10.Text = Convert.ToDouble(lblDia_L23_C10.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C10.Text = Convert.ToDouble(lblDia_L24_C10.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 21 Then
                '-----dia
                lblDia_Cab_11.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_11.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C11.Text = Convert.ToDouble(lblDia_L1_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C11.Text = Convert.ToDouble(lblDia_L2_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C11.Text = Convert.ToDouble(lblDia_L3_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C11.Text = Convert.ToDouble(lblDia_L4_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C11.Text = Convert.ToDouble(lblDia_L5_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C11.Text = Convert.ToDouble(lblDia_L6_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C11.Text = Convert.ToDouble(lblDia_L7_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C11.Text = Convert.ToDouble(lblDia_L8_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C11.Text = Convert.ToDouble(lblDia_L9_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C11.Text = Convert.ToDouble(lblDia_L10_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C11.Text = Convert.ToDouble(lblDia_L11_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C11.Text = Convert.ToDouble(lblDia_L12_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C11.Text = Convert.ToDouble(lblDia_L13_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C11.Text = Convert.ToDouble(lblDia_L14_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C11.Text = Convert.ToDouble(lblDia_L15_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C11.Text = Convert.ToDouble(lblDia_L16_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C11.Text = Convert.ToDouble(lblDia_L17_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C11.Text = Convert.ToDouble(lblDia_L18_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C11.Text = Convert.ToDouble(lblDia_L19_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C11.Text = Convert.ToDouble(lblDia_L20_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C11.Text = Convert.ToDouble(lblDia_L21_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C11.Text = Convert.ToDouble(lblDia_L22_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C11.Text = Convert.ToDouble(lblDia_L23_C11.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C11.Text = Convert.ToDouble(lblDia_L24_C11.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 20 Then
                '-----dia
                lblDia_Cab_12.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_12.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C12.Text = Convert.ToDouble(lblDia_L1_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C12.Text = Convert.ToDouble(lblDia_L2_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C12.Text = Convert.ToDouble(lblDia_L3_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C12.Text = Convert.ToDouble(lblDia_L4_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C12.Text = Convert.ToDouble(lblDia_L5_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C12.Text = Convert.ToDouble(lblDia_L6_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C12.Text = Convert.ToDouble(lblDia_L7_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C12.Text = Convert.ToDouble(lblDia_L8_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C12.Text = Convert.ToDouble(lblDia_L9_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C12.Text = Convert.ToDouble(lblDia_L10_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C12.Text = Convert.ToDouble(lblDia_L11_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C12.Text = Convert.ToDouble(lblDia_L12_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C12.Text = Convert.ToDouble(lblDia_L13_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C12.Text = Convert.ToDouble(lblDia_L14_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C12.Text = Convert.ToDouble(lblDia_L15_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C12.Text = Convert.ToDouble(lblDia_L16_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C12.Text = Convert.ToDouble(lblDia_L17_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C12.Text = Convert.ToDouble(lblDia_L18_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C12.Text = Convert.ToDouble(lblDia_L19_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C12.Text = Convert.ToDouble(lblDia_L20_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C12.Text = Convert.ToDouble(lblDia_L21_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C12.Text = Convert.ToDouble(lblDia_L22_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C12.Text = Convert.ToDouble(lblDia_L23_C12.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C12.Text = Convert.ToDouble(lblDia_L24_C12.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 19 Then
                '-----dia
                lblDia_Cab_13.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_13.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C13.Text = Convert.ToDouble(lblDia_L1_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C13.Text = Convert.ToDouble(lblDia_L2_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C13.Text = Convert.ToDouble(lblDia_L3_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C13.Text = Convert.ToDouble(lblDia_L4_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C13.Text = Convert.ToDouble(lblDia_L5_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C13.Text = Convert.ToDouble(lblDia_L6_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C13.Text = Convert.ToDouble(lblDia_L7_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C13.Text = Convert.ToDouble(lblDia_L8_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C13.Text = Convert.ToDouble(lblDia_L9_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C13.Text = Convert.ToDouble(lblDia_L10_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C13.Text = Convert.ToDouble(lblDia_L11_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C13.Text = Convert.ToDouble(lblDia_L12_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C13.Text = Convert.ToDouble(lblDia_L13_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C13.Text = Convert.ToDouble(lblDia_L14_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C13.Text = Convert.ToDouble(lblDia_L15_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C13.Text = Convert.ToDouble(lblDia_L16_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C13.Text = Convert.ToDouble(lblDia_L17_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C13.Text = Convert.ToDouble(lblDia_L18_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C13.Text = Convert.ToDouble(lblDia_L19_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C13.Text = Convert.ToDouble(lblDia_L20_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C13.Text = Convert.ToDouble(lblDia_L21_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C13.Text = Convert.ToDouble(lblDia_L22_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C13.Text = Convert.ToDouble(lblDia_L23_C13.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C13.Text = Convert.ToDouble(lblDia_L24_C13.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 18 Then
                '-----dia
                lblDia_Cab_14.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_14.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C14.Text = Convert.ToDouble(lblDia_L1_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C14.Text = Convert.ToDouble(lblDia_L2_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C14.Text = Convert.ToDouble(lblDia_L3_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C14.Text = Convert.ToDouble(lblDia_L4_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C14.Text = Convert.ToDouble(lblDia_L5_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C14.Text = Convert.ToDouble(lblDia_L6_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C14.Text = Convert.ToDouble(lblDia_L7_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C14.Text = Convert.ToDouble(lblDia_L8_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C14.Text = Convert.ToDouble(lblDia_L9_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C14.Text = Convert.ToDouble(lblDia_L10_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C14.Text = Convert.ToDouble(lblDia_L11_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C14.Text = Convert.ToDouble(lblDia_L12_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C14.Text = Convert.ToDouble(lblDia_L13_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C14.Text = Convert.ToDouble(lblDia_L14_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C14.Text = Convert.ToDouble(lblDia_L15_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C14.Text = Convert.ToDouble(lblDia_L16_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C14.Text = Convert.ToDouble(lblDia_L17_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C14.Text = Convert.ToDouble(lblDia_L18_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C14.Text = Convert.ToDouble(lblDia_L19_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C14.Text = Convert.ToDouble(lblDia_L20_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C14.Text = Convert.ToDouble(lblDia_L21_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C14.Text = Convert.ToDouble(lblDia_L22_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C14.Text = Convert.ToDouble(lblDia_L23_C14.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C14.Text = Convert.ToDouble(lblDia_L24_C14.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 17 Then
                '-----dia
                lblDia_Cab_15.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_15.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C15.Text = Convert.ToDouble(lblDia_L1_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C15.Text = Convert.ToDouble(lblDia_L2_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C15.Text = Convert.ToDouble(lblDia_L3_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C15.Text = Convert.ToDouble(lblDia_L4_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C15.Text = Convert.ToDouble(lblDia_L5_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C15.Text = Convert.ToDouble(lblDia_L6_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C15.Text = Convert.ToDouble(lblDia_L7_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C15.Text = Convert.ToDouble(lblDia_L8_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C15.Text = Convert.ToDouble(lblDia_L9_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C15.Text = Convert.ToDouble(lblDia_L10_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C15.Text = Convert.ToDouble(lblDia_L11_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C15.Text = Convert.ToDouble(lblDia_L12_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C15.Text = Convert.ToDouble(lblDia_L13_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C15.Text = Convert.ToDouble(lblDia_L14_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C15.Text = Convert.ToDouble(lblDia_L15_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C15.Text = Convert.ToDouble(lblDia_L16_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C15.Text = Convert.ToDouble(lblDia_L17_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C15.Text = Convert.ToDouble(lblDia_L18_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C15.Text = Convert.ToDouble(lblDia_L19_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C15.Text = Convert.ToDouble(lblDia_L20_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C15.Text = Convert.ToDouble(lblDia_L21_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C15.Text = Convert.ToDouble(lblDia_L22_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C15.Text = Convert.ToDouble(lblDia_L23_C15.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C15.Text = Convert.ToDouble(lblDia_L24_C15.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 16 Then
                '-----dia
                lblDia_Cab_16.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_16.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C16.Text = Convert.ToDouble(lblDia_L1_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C16.Text = Convert.ToDouble(lblDia_L2_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C16.Text = Convert.ToDouble(lblDia_L3_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C16.Text = Convert.ToDouble(lblDia_L4_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C16.Text = Convert.ToDouble(lblDia_L5_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C16.Text = Convert.ToDouble(lblDia_L6_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C16.Text = Convert.ToDouble(lblDia_L7_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C16.Text = Convert.ToDouble(lblDia_L8_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C16.Text = Convert.ToDouble(lblDia_L9_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C16.Text = Convert.ToDouble(lblDia_L10_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C16.Text = Convert.ToDouble(lblDia_L11_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C16.Text = Convert.ToDouble(lblDia_L12_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C16.Text = Convert.ToDouble(lblDia_L13_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C16.Text = Convert.ToDouble(lblDia_L14_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C16.Text = Convert.ToDouble(lblDia_L15_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C16.Text = Convert.ToDouble(lblDia_L16_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C16.Text = Convert.ToDouble(lblDia_L17_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C16.Text = Convert.ToDouble(lblDia_L18_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C16.Text = Convert.ToDouble(lblDia_L19_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C16.Text = Convert.ToDouble(lblDia_L20_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C16.Text = Convert.ToDouble(lblDia_L21_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C16.Text = Convert.ToDouble(lblDia_L22_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C16.Text = Convert.ToDouble(lblDia_L23_C16.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C16.Text = Convert.ToDouble(lblDia_L24_C16.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 15 Then
                '-----dia
                lblDia_Cab_17.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_17.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C17.Text = Convert.ToDouble(lblDia_L1_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C17.Text = Convert.ToDouble(lblDia_L2_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C17.Text = Convert.ToDouble(lblDia_L3_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C17.Text = Convert.ToDouble(lblDia_L4_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C17.Text = Convert.ToDouble(lblDia_L5_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C17.Text = Convert.ToDouble(lblDia_L6_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C17.Text = Convert.ToDouble(lblDia_L7_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C17.Text = Convert.ToDouble(lblDia_L8_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C17.Text = Convert.ToDouble(lblDia_L9_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C17.Text = Convert.ToDouble(lblDia_L10_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C17.Text = Convert.ToDouble(lblDia_L11_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C17.Text = Convert.ToDouble(lblDia_L12_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C17.Text = Convert.ToDouble(lblDia_L13_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C17.Text = Convert.ToDouble(lblDia_L14_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C17.Text = Convert.ToDouble(lblDia_L15_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C17.Text = Convert.ToDouble(lblDia_L16_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C17.Text = Convert.ToDouble(lblDia_L17_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C17.Text = Convert.ToDouble(lblDia_L18_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C17.Text = Convert.ToDouble(lblDia_L19_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C17.Text = Convert.ToDouble(lblDia_L20_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C17.Text = Convert.ToDouble(lblDia_L21_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C17.Text = Convert.ToDouble(lblDia_L22_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C17.Text = Convert.ToDouble(lblDia_L23_C17.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C17.Text = Convert.ToDouble(lblDia_L24_C17.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 14 Then
                '-----dia
                lblDia_Cab_18.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_18.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C18.Text = Convert.ToDouble(lblDia_L1_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C18.Text = Convert.ToDouble(lblDia_L2_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C18.Text = Convert.ToDouble(lblDia_L3_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C18.Text = Convert.ToDouble(lblDia_L4_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C18.Text = Convert.ToDouble(lblDia_L5_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C18.Text = Convert.ToDouble(lblDia_L6_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C18.Text = Convert.ToDouble(lblDia_L7_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C18.Text = Convert.ToDouble(lblDia_L8_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C18.Text = Convert.ToDouble(lblDia_L9_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C18.Text = Convert.ToDouble(lblDia_L10_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C18.Text = Convert.ToDouble(lblDia_L11_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C18.Text = Convert.ToDouble(lblDia_L12_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C18.Text = Convert.ToDouble(lblDia_L13_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C18.Text = Convert.ToDouble(lblDia_L14_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C18.Text = Convert.ToDouble(lblDia_L15_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C18.Text = Convert.ToDouble(lblDia_L16_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C18.Text = Convert.ToDouble(lblDia_L17_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C18.Text = Convert.ToDouble(lblDia_L18_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C18.Text = Convert.ToDouble(lblDia_L19_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C18.Text = Convert.ToDouble(lblDia_L20_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C18.Text = Convert.ToDouble(lblDia_L21_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C18.Text = Convert.ToDouble(lblDia_L22_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C18.Text = Convert.ToDouble(lblDia_L23_C18.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C18.Text = Convert.ToDouble(lblDia_L24_C18.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 13 Then
                '-----dia
                lblDia_Cab_19.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_19.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C19.Text = Convert.ToDouble(lblDia_L1_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C19.Text = Convert.ToDouble(lblDia_L2_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C19.Text = Convert.ToDouble(lblDia_L3_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C19.Text = Convert.ToDouble(lblDia_L4_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C19.Text = Convert.ToDouble(lblDia_L5_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C19.Text = Convert.ToDouble(lblDia_L6_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C19.Text = Convert.ToDouble(lblDia_L7_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C19.Text = Convert.ToDouble(lblDia_L8_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C19.Text = Convert.ToDouble(lblDia_L9_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C19.Text = Convert.ToDouble(lblDia_L10_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C19.Text = Convert.ToDouble(lblDia_L11_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C19.Text = Convert.ToDouble(lblDia_L12_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C19.Text = Convert.ToDouble(lblDia_L13_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C19.Text = Convert.ToDouble(lblDia_L14_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C19.Text = Convert.ToDouble(lblDia_L15_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C19.Text = Convert.ToDouble(lblDia_L16_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C19.Text = Convert.ToDouble(lblDia_L17_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C19.Text = Convert.ToDouble(lblDia_L18_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C19.Text = Convert.ToDouble(lblDia_L19_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C19.Text = Convert.ToDouble(lblDia_L20_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C19.Text = Convert.ToDouble(lblDia_L21_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C19.Text = Convert.ToDouble(lblDia_L22_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C19.Text = Convert.ToDouble(lblDia_L23_C19.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C19.Text = Convert.ToDouble(lblDia_L24_C19.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 12 Then
                '-----dia
                lblDia_Cab_20.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_20.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C20.Text = Convert.ToDouble(lblDia_L1_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C20.Text = Convert.ToDouble(lblDia_L2_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C20.Text = Convert.ToDouble(lblDia_L3_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C20.Text = Convert.ToDouble(lblDia_L4_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C20.Text = Convert.ToDouble(lblDia_L5_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C20.Text = Convert.ToDouble(lblDia_L6_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C20.Text = Convert.ToDouble(lblDia_L7_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C20.Text = Convert.ToDouble(lblDia_L8_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C20.Text = Convert.ToDouble(lblDia_L9_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C20.Text = Convert.ToDouble(lblDia_L10_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C20.Text = Convert.ToDouble(lblDia_L11_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C20.Text = Convert.ToDouble(lblDia_L12_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C20.Text = Convert.ToDouble(lblDia_L13_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C20.Text = Convert.ToDouble(lblDia_L14_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C20.Text = Convert.ToDouble(lblDia_L15_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C20.Text = Convert.ToDouble(lblDia_L16_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C20.Text = Convert.ToDouble(lblDia_L17_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C20.Text = Convert.ToDouble(lblDia_L18_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C20.Text = Convert.ToDouble(lblDia_L19_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C20.Text = Convert.ToDouble(lblDia_L20_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C20.Text = Convert.ToDouble(lblDia_L21_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C20.Text = Convert.ToDouble(lblDia_L22_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C20.Text = Convert.ToDouble(lblDia_L23_C20.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C20.Text = Convert.ToDouble(lblDia_L24_C20.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 11 Then
                '-----dia
                lblDia_Cab_21.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_21.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C21.Text = Convert.ToDouble(lblDia_L1_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C21.Text = Convert.ToDouble(lblDia_L2_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C21.Text = Convert.ToDouble(lblDia_L3_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C21.Text = Convert.ToDouble(lblDia_L4_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C21.Text = Convert.ToDouble(lblDia_L5_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C21.Text = Convert.ToDouble(lblDia_L6_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C21.Text = Convert.ToDouble(lblDia_L7_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C21.Text = Convert.ToDouble(lblDia_L8_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C21.Text = Convert.ToDouble(lblDia_L9_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C21.Text = Convert.ToDouble(lblDia_L10_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C21.Text = Convert.ToDouble(lblDia_L11_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C21.Text = Convert.ToDouble(lblDia_L12_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C21.Text = Convert.ToDouble(lblDia_L13_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C21.Text = Convert.ToDouble(lblDia_L14_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C21.Text = Convert.ToDouble(lblDia_L15_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C21.Text = Convert.ToDouble(lblDia_L16_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C21.Text = Convert.ToDouble(lblDia_L17_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C21.Text = Convert.ToDouble(lblDia_L18_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C21.Text = Convert.ToDouble(lblDia_L19_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C21.Text = Convert.ToDouble(lblDia_L20_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C21.Text = Convert.ToDouble(lblDia_L21_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C21.Text = Convert.ToDouble(lblDia_L22_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C21.Text = Convert.ToDouble(lblDia_L23_C21.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C21.Text = Convert.ToDouble(lblDia_L24_C21.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 10 Then
                '-----dia
                lblDia_Cab_22.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_22.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C22.Text = Convert.ToDouble(lblDia_L1_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C22.Text = Convert.ToDouble(lblDia_L2_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C22.Text = Convert.ToDouble(lblDia_L3_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C22.Text = Convert.ToDouble(lblDia_L4_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C22.Text = Convert.ToDouble(lblDia_L5_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C22.Text = Convert.ToDouble(lblDia_L6_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C22.Text = Convert.ToDouble(lblDia_L7_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C22.Text = Convert.ToDouble(lblDia_L8_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C22.Text = Convert.ToDouble(lblDia_L9_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C22.Text = Convert.ToDouble(lblDia_L10_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C22.Text = Convert.ToDouble(lblDia_L11_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C22.Text = Convert.ToDouble(lblDia_L12_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C22.Text = Convert.ToDouble(lblDia_L13_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C22.Text = Convert.ToDouble(lblDia_L14_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C22.Text = Convert.ToDouble(lblDia_L15_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C22.Text = Convert.ToDouble(lblDia_L16_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C22.Text = Convert.ToDouble(lblDia_L17_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C22.Text = Convert.ToDouble(lblDia_L18_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C22.Text = Convert.ToDouble(lblDia_L19_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C22.Text = Convert.ToDouble(lblDia_L20_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C22.Text = Convert.ToDouble(lblDia_L21_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C22.Text = Convert.ToDouble(lblDia_L22_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C22.Text = Convert.ToDouble(lblDia_L23_C22.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C22.Text = Convert.ToDouble(lblDia_L24_C22.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 9 Then
                '-----dia
                lblDia_Cab_23.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_23.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C23.Text = Convert.ToDouble(lblDia_L1_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C23.Text = Convert.ToDouble(lblDia_L2_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C23.Text = Convert.ToDouble(lblDia_L3_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C23.Text = Convert.ToDouble(lblDia_L4_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C23.Text = Convert.ToDouble(lblDia_L5_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C23.Text = Convert.ToDouble(lblDia_L6_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C23.Text = Convert.ToDouble(lblDia_L7_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C23.Text = Convert.ToDouble(lblDia_L8_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C23.Text = Convert.ToDouble(lblDia_L9_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C23.Text = Convert.ToDouble(lblDia_L10_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C23.Text = Convert.ToDouble(lblDia_L11_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C23.Text = Convert.ToDouble(lblDia_L12_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C23.Text = Convert.ToDouble(lblDia_L13_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C23.Text = Convert.ToDouble(lblDia_L14_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C23.Text = Convert.ToDouble(lblDia_L15_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C23.Text = Convert.ToDouble(lblDia_L16_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C23.Text = Convert.ToDouble(lblDia_L17_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C23.Text = Convert.ToDouble(lblDia_L18_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C23.Text = Convert.ToDouble(lblDia_L19_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C23.Text = Convert.ToDouble(lblDia_L20_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C23.Text = Convert.ToDouble(lblDia_L21_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C23.Text = Convert.ToDouble(lblDia_L22_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C23.Text = Convert.ToDouble(lblDia_L23_C23.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C23.Text = Convert.ToDouble(lblDia_L24_C23.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 8 Then
                '-----dia
                lblDia_Cab_24.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_24.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C24.Text = Convert.ToDouble(lblDia_L1_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C24.Text = Convert.ToDouble(lblDia_L2_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C24.Text = Convert.ToDouble(lblDia_L3_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C24.Text = Convert.ToDouble(lblDia_L4_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C24.Text = Convert.ToDouble(lblDia_L5_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C24.Text = Convert.ToDouble(lblDia_L6_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C24.Text = Convert.ToDouble(lblDia_L7_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C24.Text = Convert.ToDouble(lblDia_L8_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C24.Text = Convert.ToDouble(lblDia_L9_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C24.Text = Convert.ToDouble(lblDia_L10_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C24.Text = Convert.ToDouble(lblDia_L11_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C24.Text = Convert.ToDouble(lblDia_L12_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C24.Text = Convert.ToDouble(lblDia_L13_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C24.Text = Convert.ToDouble(lblDia_L14_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C24.Text = Convert.ToDouble(lblDia_L15_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C24.Text = Convert.ToDouble(lblDia_L16_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C24.Text = Convert.ToDouble(lblDia_L17_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C24.Text = Convert.ToDouble(lblDia_L18_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C24.Text = Convert.ToDouble(lblDia_L19_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C24.Text = Convert.ToDouble(lblDia_L20_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C24.Text = Convert.ToDouble(lblDia_L21_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C24.Text = Convert.ToDouble(lblDia_L22_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C24.Text = Convert.ToDouble(lblDia_L23_C24.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C24.Text = Convert.ToDouble(lblDia_L24_C24.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 7 Then
                '-----dia
                lblDia_Cab_25.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_25.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C25.Text = Convert.ToDouble(lblDia_L1_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C25.Text = Convert.ToDouble(lblDia_L2_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C25.Text = Convert.ToDouble(lblDia_L3_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C25.Text = Convert.ToDouble(lblDia_L4_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C25.Text = Convert.ToDouble(lblDia_L5_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C25.Text = Convert.ToDouble(lblDia_L6_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C25.Text = Convert.ToDouble(lblDia_L7_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C25.Text = Convert.ToDouble(lblDia_L8_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C25.Text = Convert.ToDouble(lblDia_L9_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C25.Text = Convert.ToDouble(lblDia_L10_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C25.Text = Convert.ToDouble(lblDia_L11_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C25.Text = Convert.ToDouble(lblDia_L12_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C25.Text = Convert.ToDouble(lblDia_L13_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C25.Text = Convert.ToDouble(lblDia_L14_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C25.Text = Convert.ToDouble(lblDia_L15_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C25.Text = Convert.ToDouble(lblDia_L16_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C25.Text = Convert.ToDouble(lblDia_L17_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C25.Text = Convert.ToDouble(lblDia_L18_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C25.Text = Convert.ToDouble(lblDia_L19_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C25.Text = Convert.ToDouble(lblDia_L20_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C25.Text = Convert.ToDouble(lblDia_L21_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C25.Text = Convert.ToDouble(lblDia_L22_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C25.Text = Convert.ToDouble(lblDia_L23_C25.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C25.Text = Convert.ToDouble(lblDia_L24_C25.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 6 Then
                '-----dia
                lblDia_Cab_26.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_26.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C26.Text = Convert.ToDouble(lblDia_L1_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C26.Text = Convert.ToDouble(lblDia_L2_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C26.Text = Convert.ToDouble(lblDia_L3_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C26.Text = Convert.ToDouble(lblDia_L4_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C26.Text = Convert.ToDouble(lblDia_L5_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C26.Text = Convert.ToDouble(lblDia_L6_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C26.Text = Convert.ToDouble(lblDia_L7_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C26.Text = Convert.ToDouble(lblDia_L8_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C26.Text = Convert.ToDouble(lblDia_L9_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C26.Text = Convert.ToDouble(lblDia_L10_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C26.Text = Convert.ToDouble(lblDia_L11_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C26.Text = Convert.ToDouble(lblDia_L12_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C26.Text = Convert.ToDouble(lblDia_L13_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C26.Text = Convert.ToDouble(lblDia_L14_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C26.Text = Convert.ToDouble(lblDia_L15_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C26.Text = Convert.ToDouble(lblDia_L16_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C26.Text = Convert.ToDouble(lblDia_L17_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C26.Text = Convert.ToDouble(lblDia_L18_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C26.Text = Convert.ToDouble(lblDia_L19_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C26.Text = Convert.ToDouble(lblDia_L20_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C26.Text = Convert.ToDouble(lblDia_L21_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C26.Text = Convert.ToDouble(lblDia_L22_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C26.Text = Convert.ToDouble(lblDia_L23_C26.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C26.Text = Convert.ToDouble(lblDia_L24_C26.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 5 Then
                '-----dia
                lblDia_Cab_27.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_27.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C27.Text = Convert.ToDouble(lblDia_L1_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C27.Text = Convert.ToDouble(lblDia_L2_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C27.Text = Convert.ToDouble(lblDia_L3_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C27.Text = Convert.ToDouble(lblDia_L4_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C27.Text = Convert.ToDouble(lblDia_L5_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C27.Text = Convert.ToDouble(lblDia_L6_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C27.Text = Convert.ToDouble(lblDia_L7_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C27.Text = Convert.ToDouble(lblDia_L8_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C27.Text = Convert.ToDouble(lblDia_L9_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C27.Text = Convert.ToDouble(lblDia_L10_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C27.Text = Convert.ToDouble(lblDia_L11_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C27.Text = Convert.ToDouble(lblDia_L12_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C27.Text = Convert.ToDouble(lblDia_L13_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C27.Text = Convert.ToDouble(lblDia_L14_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C27.Text = Convert.ToDouble(lblDia_L15_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C27.Text = Convert.ToDouble(lblDia_L16_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C27.Text = Convert.ToDouble(lblDia_L17_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C27.Text = Convert.ToDouble(lblDia_L18_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C27.Text = Convert.ToDouble(lblDia_L19_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C27.Text = Convert.ToDouble(lblDia_L20_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C27.Text = Convert.ToDouble(lblDia_L21_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C27.Text = Convert.ToDouble(lblDia_L22_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C27.Text = Convert.ToDouble(lblDia_L23_C27.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C27.Text = Convert.ToDouble(lblDia_L24_C27.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 4 Then
                '-----dia
                lblDia_Cab_28.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_28.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C28.Text = Convert.ToDouble(lblDia_L1_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C28.Text = Convert.ToDouble(lblDia_L2_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C28.Text = Convert.ToDouble(lblDia_L3_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C28.Text = Convert.ToDouble(lblDia_L4_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C28.Text = Convert.ToDouble(lblDia_L5_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C28.Text = Convert.ToDouble(lblDia_L6_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C28.Text = Convert.ToDouble(lblDia_L7_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C28.Text = Convert.ToDouble(lblDia_L8_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C28.Text = Convert.ToDouble(lblDia_L9_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C28.Text = Convert.ToDouble(lblDia_L10_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C28.Text = Convert.ToDouble(lblDia_L11_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C28.Text = Convert.ToDouble(lblDia_L12_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C28.Text = Convert.ToDouble(lblDia_L13_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C28.Text = Convert.ToDouble(lblDia_L14_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C28.Text = Convert.ToDouble(lblDia_L15_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C28.Text = Convert.ToDouble(lblDia_L16_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C28.Text = Convert.ToDouble(lblDia_L17_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C28.Text = Convert.ToDouble(lblDia_L18_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C28.Text = Convert.ToDouble(lblDia_L19_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C28.Text = Convert.ToDouble(lblDia_L20_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C28.Text = Convert.ToDouble(lblDia_L21_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C28.Text = Convert.ToDouble(lblDia_L22_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C28.Text = Convert.ToDouble(lblDia_L23_C28.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C28.Text = Convert.ToDouble(lblDia_L24_C28.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 3 Then
                '-----dia
                lblDia_Cab_29.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_29.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C29.Text = Convert.ToDouble(lblDia_L1_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C29.Text = Convert.ToDouble(lblDia_L2_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C29.Text = Convert.ToDouble(lblDia_L3_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C29.Text = Convert.ToDouble(lblDia_L4_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C29.Text = Convert.ToDouble(lblDia_L5_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C29.Text = Convert.ToDouble(lblDia_L6_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C29.Text = Convert.ToDouble(lblDia_L7_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C29.Text = Convert.ToDouble(lblDia_L8_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C29.Text = Convert.ToDouble(lblDia_L9_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C29.Text = Convert.ToDouble(lblDia_L10_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C29.Text = Convert.ToDouble(lblDia_L11_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C29.Text = Convert.ToDouble(lblDia_L12_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C29.Text = Convert.ToDouble(lblDia_L13_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C29.Text = Convert.ToDouble(lblDia_L14_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C29.Text = Convert.ToDouble(lblDia_L15_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C29.Text = Convert.ToDouble(lblDia_L16_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C29.Text = Convert.ToDouble(lblDia_L17_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C29.Text = Convert.ToDouble(lblDia_L18_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C29.Text = Convert.ToDouble(lblDia_L19_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C29.Text = Convert.ToDouble(lblDia_L20_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C29.Text = Convert.ToDouble(lblDia_L21_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C29.Text = Convert.ToDouble(lblDia_L22_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C29.Text = Convert.ToDouble(lblDia_L23_C29.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C29.Text = Convert.ToDouble(lblDia_L24_C29.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 2 Then
                '-----dia
                lblDia_Cab_30.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_30.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C30.Text = Convert.ToDouble(lblDia_L1_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C30.Text = Convert.ToDouble(lblDia_L2_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C30.Text = Convert.ToDouble(lblDia_L3_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C30.Text = Convert.ToDouble(lblDia_L4_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C30.Text = Convert.ToDouble(lblDia_L5_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C30.Text = Convert.ToDouble(lblDia_L6_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C30.Text = Convert.ToDouble(lblDia_L7_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C30.Text = Convert.ToDouble(lblDia_L8_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C30.Text = Convert.ToDouble(lblDia_L9_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C30.Text = Convert.ToDouble(lblDia_L10_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C30.Text = Convert.ToDouble(lblDia_L11_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C30.Text = Convert.ToDouble(lblDia_L12_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C30.Text = Convert.ToDouble(lblDia_L13_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C30.Text = Convert.ToDouble(lblDia_L14_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C30.Text = Convert.ToDouble(lblDia_L15_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C30.Text = Convert.ToDouble(lblDia_L16_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C30.Text = Convert.ToDouble(lblDia_L17_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C30.Text = Convert.ToDouble(lblDia_L18_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C30.Text = Convert.ToDouble(lblDia_L19_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C30.Text = Convert.ToDouble(lblDia_L20_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C30.Text = Convert.ToDouble(lblDia_L21_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C30.Text = Convert.ToDouble(lblDia_L22_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C30.Text = Convert.ToDouble(lblDia_L23_C30.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C30.Text = Convert.ToDouble(lblDia_L24_C30.Text) + vDataView.Item(i).Item(40)
            End If

            If vDataView.Item(i).Item(41) = 1 Then
                '-----dia
                lblDia_Cab_31.Text = vDataView.Item(i).Item(38)
                If Right(vDataView.Item(i).Item(38), 3) = "Sab" Or Right(vDataView.Item(i).Item(38), 3) = "Dom" Then lblDia_Cab_31.ForeColor = Drawing.Color.Yellow
                '-----hora                
                If vDataView.Item(i).Item(39) = 1 Then lblDia_L1_C31.Text = Convert.ToDouble(lblDia_L1_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 2 Then lblDia_L2_C31.Text = Convert.ToDouble(lblDia_L2_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 3 Then lblDia_L3_C31.Text = Convert.ToDouble(lblDia_L3_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 4 Then lblDia_L4_C31.Text = Convert.ToDouble(lblDia_L4_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 5 Then lblDia_L5_C31.Text = Convert.ToDouble(lblDia_L5_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 6 Then lblDia_L6_C31.Text = Convert.ToDouble(lblDia_L6_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 7 Then lblDia_L7_C31.Text = Convert.ToDouble(lblDia_L7_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 8 Then lblDia_L8_C31.Text = Convert.ToDouble(lblDia_L8_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 9 Then lblDia_L9_C31.Text = Convert.ToDouble(lblDia_L9_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 10 Then lblDia_L10_C31.Text = Convert.ToDouble(lblDia_L10_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 11 Then lblDia_L11_C31.Text = Convert.ToDouble(lblDia_L11_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 12 Then lblDia_L12_C31.Text = Convert.ToDouble(lblDia_L12_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 13 Then lblDia_L13_C31.Text = Convert.ToDouble(lblDia_L13_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 14 Then lblDia_L14_C31.Text = Convert.ToDouble(lblDia_L14_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 15 Then lblDia_L15_C31.Text = Convert.ToDouble(lblDia_L15_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 16 Then lblDia_L16_C31.Text = Convert.ToDouble(lblDia_L16_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 17 Then lblDia_L17_C31.Text = Convert.ToDouble(lblDia_L17_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 18 Then lblDia_L18_C31.Text = Convert.ToDouble(lblDia_L18_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 19 Then lblDia_L19_C31.Text = Convert.ToDouble(lblDia_L19_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 20 Then lblDia_L20_C31.Text = Convert.ToDouble(lblDia_L20_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 21 Then lblDia_L21_C31.Text = Convert.ToDouble(lblDia_L21_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 22 Then lblDia_L22_C31.Text = Convert.ToDouble(lblDia_L22_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 23 Then lblDia_L23_C31.Text = Convert.ToDouble(lblDia_L23_C31.Text) + vDataView.Item(i).Item(40)
                If vDataView.Item(i).Item(39) = 0 Then lblDia_L24_C31.Text = Convert.ToDouble(lblDia_L24_C31.Text) + vDataView.Item(i).Item(40)
            End If
        Next

        '-----lista totais por linha
        lblTotal_Dia_1.Text = Convert.ToDouble(lblDia_L1_C1.Text) + Convert.ToDouble(lblDia_L2_C1.Text) + Convert.ToDouble(lblDia_L3_C1.Text) + Convert.ToDouble(lblDia_L4_C1.Text) + Convert.ToDouble(lblDia_L5_C1.Text) + Convert.ToDouble(lblDia_L6_C1.Text) + Convert.ToDouble(lblDia_L7_C1.Text) + Convert.ToDouble(lblDia_L8_C1.Text) + Convert.ToDouble(lblDia_L9_C1.Text) + Convert.ToDouble(lblDia_L10_C1.Text) + Convert.ToDouble(lblDia_L11_C1.Text) + Convert.ToDouble(lblDia_L12_C1.Text) + Convert.ToDouble(lblDia_L13_C1.Text) + Convert.ToDouble(lblDia_L14_C1.Text) + Convert.ToDouble(lblDia_L15_C1.Text) + Convert.ToDouble(lblDia_L16_C1.Text) + Convert.ToDouble(lblDia_L17_C1.Text) + Convert.ToDouble(lblDia_L18_C1.Text) + Convert.ToDouble(lblDia_L19_C1.Text) + Convert.ToDouble(lblDia_L20_C1.Text) + Convert.ToDouble(lblDia_L21_C1.Text) + Convert.ToDouble(lblDia_L22_C1.Text) + Convert.ToDouble(lblDia_L23_C1.Text) + Convert.ToDouble(lblDia_L24_C1.Text)
        lblTotal_Dia_2.Text = Convert.ToDouble(lblDia_L1_C2.Text) + Convert.ToDouble(lblDia_L2_C2.Text) + Convert.ToDouble(lblDia_L3_C2.Text) + Convert.ToDouble(lblDia_L4_C2.Text) + Convert.ToDouble(lblDia_L5_C2.Text) + Convert.ToDouble(lblDia_L6_C2.Text) + Convert.ToDouble(lblDia_L7_C2.Text) + Convert.ToDouble(lblDia_L8_C2.Text) + Convert.ToDouble(lblDia_L9_C2.Text) + Convert.ToDouble(lblDia_L10_C2.Text) + Convert.ToDouble(lblDia_L11_C2.Text) + Convert.ToDouble(lblDia_L12_C2.Text) + Convert.ToDouble(lblDia_L13_C2.Text) + Convert.ToDouble(lblDia_L14_C2.Text) + Convert.ToDouble(lblDia_L15_C2.Text) + Convert.ToDouble(lblDia_L16_C2.Text) + Convert.ToDouble(lblDia_L17_C2.Text) + Convert.ToDouble(lblDia_L18_C2.Text) + Convert.ToDouble(lblDia_L19_C2.Text) + Convert.ToDouble(lblDia_L20_C2.Text) + Convert.ToDouble(lblDia_L21_C2.Text) + Convert.ToDouble(lblDia_L22_C2.Text) + Convert.ToDouble(lblDia_L23_C2.Text) + Convert.ToDouble(lblDia_L24_C2.Text)
        lblTotal_Dia_3.Text = Convert.ToDouble(lblDia_L1_C3.Text) + Convert.ToDouble(lblDia_L2_C3.Text) + Convert.ToDouble(lblDia_L3_C3.Text) + Convert.ToDouble(lblDia_L4_C3.Text) + Convert.ToDouble(lblDia_L5_C3.Text) + Convert.ToDouble(lblDia_L6_C3.Text) + Convert.ToDouble(lblDia_L7_C3.Text) + Convert.ToDouble(lblDia_L8_C3.Text) + Convert.ToDouble(lblDia_L9_C3.Text) + Convert.ToDouble(lblDia_L10_C3.Text) + Convert.ToDouble(lblDia_L11_C3.Text) + Convert.ToDouble(lblDia_L12_C3.Text) + Convert.ToDouble(lblDia_L13_C3.Text) + Convert.ToDouble(lblDia_L14_C3.Text) + Convert.ToDouble(lblDia_L15_C3.Text) + Convert.ToDouble(lblDia_L16_C3.Text) + Convert.ToDouble(lblDia_L17_C3.Text) + Convert.ToDouble(lblDia_L18_C3.Text) + Convert.ToDouble(lblDia_L19_C3.Text) + Convert.ToDouble(lblDia_L20_C3.Text) + Convert.ToDouble(lblDia_L21_C3.Text) + Convert.ToDouble(lblDia_L22_C3.Text) + Convert.ToDouble(lblDia_L23_C3.Text) + Convert.ToDouble(lblDia_L24_C3.Text)
        lblTotal_Dia_4.Text = Convert.ToDouble(lblDia_L1_C4.Text) + Convert.ToDouble(lblDia_L2_C4.Text) + Convert.ToDouble(lblDia_L3_C4.Text) + Convert.ToDouble(lblDia_L4_C4.Text) + Convert.ToDouble(lblDia_L5_C4.Text) + Convert.ToDouble(lblDia_L6_C4.Text) + Convert.ToDouble(lblDia_L7_C4.Text) + Convert.ToDouble(lblDia_L8_C4.Text) + Convert.ToDouble(lblDia_L9_C4.Text) + Convert.ToDouble(lblDia_L10_C4.Text) + Convert.ToDouble(lblDia_L11_C4.Text) + Convert.ToDouble(lblDia_L12_C4.Text) + Convert.ToDouble(lblDia_L13_C4.Text) + Convert.ToDouble(lblDia_L14_C4.Text) + Convert.ToDouble(lblDia_L15_C4.Text) + Convert.ToDouble(lblDia_L16_C4.Text) + Convert.ToDouble(lblDia_L17_C4.Text) + Convert.ToDouble(lblDia_L18_C4.Text) + Convert.ToDouble(lblDia_L19_C4.Text) + Convert.ToDouble(lblDia_L20_C4.Text) + Convert.ToDouble(lblDia_L21_C4.Text) + Convert.ToDouble(lblDia_L22_C4.Text) + Convert.ToDouble(lblDia_L23_C4.Text) + Convert.ToDouble(lblDia_L24_C4.Text)
        lblTotal_Dia_5.Text = Convert.ToDouble(lblDia_L1_C5.Text) + Convert.ToDouble(lblDia_L2_C5.Text) + Convert.ToDouble(lblDia_L3_C5.Text) + Convert.ToDouble(lblDia_L4_C5.Text) + Convert.ToDouble(lblDia_L5_C5.Text) + Convert.ToDouble(lblDia_L6_C5.Text) + Convert.ToDouble(lblDia_L7_C5.Text) + Convert.ToDouble(lblDia_L8_C5.Text) + Convert.ToDouble(lblDia_L9_C5.Text) + Convert.ToDouble(lblDia_L10_C5.Text) + Convert.ToDouble(lblDia_L11_C5.Text) + Convert.ToDouble(lblDia_L12_C5.Text) + Convert.ToDouble(lblDia_L13_C5.Text) + Convert.ToDouble(lblDia_L14_C5.Text) + Convert.ToDouble(lblDia_L15_C5.Text) + Convert.ToDouble(lblDia_L16_C5.Text) + Convert.ToDouble(lblDia_L17_C5.Text) + Convert.ToDouble(lblDia_L18_C5.Text) + Convert.ToDouble(lblDia_L19_C5.Text) + Convert.ToDouble(lblDia_L20_C5.Text) + Convert.ToDouble(lblDia_L21_C5.Text) + Convert.ToDouble(lblDia_L22_C5.Text) + Convert.ToDouble(lblDia_L23_C5.Text) + Convert.ToDouble(lblDia_L24_C5.Text)
        lblTotal_Dia_6.Text = Convert.ToDouble(lblDia_L1_C6.Text) + Convert.ToDouble(lblDia_L2_C6.Text) + Convert.ToDouble(lblDia_L3_C6.Text) + Convert.ToDouble(lblDia_L4_C6.Text) + Convert.ToDouble(lblDia_L5_C6.Text) + Convert.ToDouble(lblDia_L6_C6.Text) + Convert.ToDouble(lblDia_L7_C6.Text) + Convert.ToDouble(lblDia_L8_C6.Text) + Convert.ToDouble(lblDia_L9_C6.Text) + Convert.ToDouble(lblDia_L10_C6.Text) + Convert.ToDouble(lblDia_L11_C6.Text) + Convert.ToDouble(lblDia_L12_C6.Text) + Convert.ToDouble(lblDia_L13_C6.Text) + Convert.ToDouble(lblDia_L14_C6.Text) + Convert.ToDouble(lblDia_L15_C6.Text) + Convert.ToDouble(lblDia_L16_C6.Text) + Convert.ToDouble(lblDia_L17_C6.Text) + Convert.ToDouble(lblDia_L18_C6.Text) + Convert.ToDouble(lblDia_L19_C6.Text) + Convert.ToDouble(lblDia_L20_C6.Text) + Convert.ToDouble(lblDia_L21_C6.Text) + Convert.ToDouble(lblDia_L22_C6.Text) + Convert.ToDouble(lblDia_L23_C6.Text) + Convert.ToDouble(lblDia_L24_C6.Text)
        lblTotal_Dia_7.Text = Convert.ToDouble(lblDia_L1_C7.Text) + Convert.ToDouble(lblDia_L2_C7.Text) + Convert.ToDouble(lblDia_L3_C7.Text) + Convert.ToDouble(lblDia_L4_C7.Text) + Convert.ToDouble(lblDia_L5_C7.Text) + Convert.ToDouble(lblDia_L6_C7.Text) + Convert.ToDouble(lblDia_L7_C7.Text) + Convert.ToDouble(lblDia_L8_C7.Text) + Convert.ToDouble(lblDia_L9_C7.Text) + Convert.ToDouble(lblDia_L10_C7.Text) + Convert.ToDouble(lblDia_L11_C7.Text) + Convert.ToDouble(lblDia_L12_C7.Text) + Convert.ToDouble(lblDia_L13_C7.Text) + Convert.ToDouble(lblDia_L14_C7.Text) + Convert.ToDouble(lblDia_L15_C7.Text) + Convert.ToDouble(lblDia_L16_C7.Text) + Convert.ToDouble(lblDia_L17_C7.Text) + Convert.ToDouble(lblDia_L18_C7.Text) + Convert.ToDouble(lblDia_L19_C7.Text) + Convert.ToDouble(lblDia_L20_C7.Text) + Convert.ToDouble(lblDia_L21_C7.Text) + Convert.ToDouble(lblDia_L22_C7.Text) + Convert.ToDouble(lblDia_L23_C7.Text) + Convert.ToDouble(lblDia_L24_C7.Text)
        lblTotal_Dia_8.Text = Convert.ToDouble(lblDia_L1_C8.Text) + Convert.ToDouble(lblDia_L2_C8.Text) + Convert.ToDouble(lblDia_L3_C8.Text) + Convert.ToDouble(lblDia_L4_C8.Text) + Convert.ToDouble(lblDia_L5_C8.Text) + Convert.ToDouble(lblDia_L6_C8.Text) + Convert.ToDouble(lblDia_L7_C8.Text) + Convert.ToDouble(lblDia_L8_C8.Text) + Convert.ToDouble(lblDia_L9_C8.Text) + Convert.ToDouble(lblDia_L10_C8.Text) + Convert.ToDouble(lblDia_L11_C8.Text) + Convert.ToDouble(lblDia_L12_C8.Text) + Convert.ToDouble(lblDia_L13_C8.Text) + Convert.ToDouble(lblDia_L14_C8.Text) + Convert.ToDouble(lblDia_L15_C8.Text) + Convert.ToDouble(lblDia_L16_C8.Text) + Convert.ToDouble(lblDia_L17_C8.Text) + Convert.ToDouble(lblDia_L18_C8.Text) + Convert.ToDouble(lblDia_L19_C8.Text) + Convert.ToDouble(lblDia_L20_C8.Text) + Convert.ToDouble(lblDia_L21_C8.Text) + Convert.ToDouble(lblDia_L22_C8.Text) + Convert.ToDouble(lblDia_L23_C8.Text) + Convert.ToDouble(lblDia_L24_C8.Text)
        lblTotal_Dia_9.Text = Convert.ToDouble(lblDia_L1_C9.Text) + Convert.ToDouble(lblDia_L2_C9.Text) + Convert.ToDouble(lblDia_L3_C9.Text) + Convert.ToDouble(lblDia_L4_C9.Text) + Convert.ToDouble(lblDia_L5_C9.Text) + Convert.ToDouble(lblDia_L6_C9.Text) + Convert.ToDouble(lblDia_L7_C9.Text) + Convert.ToDouble(lblDia_L8_C9.Text) + Convert.ToDouble(lblDia_L9_C9.Text) + Convert.ToDouble(lblDia_L10_C9.Text) + Convert.ToDouble(lblDia_L11_C9.Text) + Convert.ToDouble(lblDia_L12_C9.Text) + Convert.ToDouble(lblDia_L13_C9.Text) + Convert.ToDouble(lblDia_L14_C9.Text) + Convert.ToDouble(lblDia_L15_C9.Text) + Convert.ToDouble(lblDia_L16_C9.Text) + Convert.ToDouble(lblDia_L17_C9.Text) + Convert.ToDouble(lblDia_L18_C9.Text) + Convert.ToDouble(lblDia_L19_C9.Text) + Convert.ToDouble(lblDia_L20_C9.Text) + Convert.ToDouble(lblDia_L21_C9.Text) + Convert.ToDouble(lblDia_L22_C9.Text) + Convert.ToDouble(lblDia_L23_C9.Text) + Convert.ToDouble(lblDia_L24_C9.Text)
        lblTotal_Dia_10.Text = Convert.ToDouble(lblDia_L1_C10.Text) + Convert.ToDouble(lblDia_L2_C10.Text) + Convert.ToDouble(lblDia_L3_C10.Text) + Convert.ToDouble(lblDia_L4_C10.Text) + Convert.ToDouble(lblDia_L5_C10.Text) + Convert.ToDouble(lblDia_L6_C10.Text) + Convert.ToDouble(lblDia_L7_C10.Text) + Convert.ToDouble(lblDia_L8_C10.Text) + Convert.ToDouble(lblDia_L9_C10.Text) + Convert.ToDouble(lblDia_L10_C10.Text) + Convert.ToDouble(lblDia_L11_C10.Text) + Convert.ToDouble(lblDia_L12_C10.Text) + Convert.ToDouble(lblDia_L13_C10.Text) + Convert.ToDouble(lblDia_L14_C10.Text) + Convert.ToDouble(lblDia_L15_C10.Text) + Convert.ToDouble(lblDia_L16_C10.Text) + Convert.ToDouble(lblDia_L17_C10.Text) + Convert.ToDouble(lblDia_L18_C10.Text) + Convert.ToDouble(lblDia_L19_C10.Text) + Convert.ToDouble(lblDia_L20_C10.Text) + Convert.ToDouble(lblDia_L21_C10.Text) + Convert.ToDouble(lblDia_L22_C10.Text) + Convert.ToDouble(lblDia_L23_C10.Text) + Convert.ToDouble(lblDia_L24_C10.Text)
        lblTotal_Dia_11.Text = Convert.ToDouble(lblDia_L1_C11.Text) + Convert.ToDouble(lblDia_L2_C11.Text) + Convert.ToDouble(lblDia_L3_C11.Text) + Convert.ToDouble(lblDia_L4_C11.Text) + Convert.ToDouble(lblDia_L5_C11.Text) + Convert.ToDouble(lblDia_L6_C11.Text) + Convert.ToDouble(lblDia_L7_C11.Text) + Convert.ToDouble(lblDia_L8_C11.Text) + Convert.ToDouble(lblDia_L9_C11.Text) + Convert.ToDouble(lblDia_L10_C11.Text) + Convert.ToDouble(lblDia_L11_C11.Text) + Convert.ToDouble(lblDia_L12_C11.Text) + Convert.ToDouble(lblDia_L13_C11.Text) + Convert.ToDouble(lblDia_L14_C11.Text) + Convert.ToDouble(lblDia_L15_C11.Text) + Convert.ToDouble(lblDia_L16_C11.Text) + Convert.ToDouble(lblDia_L17_C11.Text) + Convert.ToDouble(lblDia_L18_C11.Text) + Convert.ToDouble(lblDia_L19_C11.Text) + Convert.ToDouble(lblDia_L20_C11.Text) + Convert.ToDouble(lblDia_L21_C11.Text) + Convert.ToDouble(lblDia_L22_C11.Text) + Convert.ToDouble(lblDia_L23_C11.Text) + Convert.ToDouble(lblDia_L24_C11.Text)
        lblTotal_Dia_12.Text = Convert.ToDouble(lblDia_L1_C12.Text) + Convert.ToDouble(lblDia_L2_C12.Text) + Convert.ToDouble(lblDia_L3_C12.Text) + Convert.ToDouble(lblDia_L4_C12.Text) + Convert.ToDouble(lblDia_L5_C12.Text) + Convert.ToDouble(lblDia_L6_C12.Text) + Convert.ToDouble(lblDia_L7_C12.Text) + Convert.ToDouble(lblDia_L8_C12.Text) + Convert.ToDouble(lblDia_L9_C12.Text) + Convert.ToDouble(lblDia_L10_C12.Text) + Convert.ToDouble(lblDia_L11_C12.Text) + Convert.ToDouble(lblDia_L12_C12.Text) + Convert.ToDouble(lblDia_L13_C12.Text) + Convert.ToDouble(lblDia_L14_C12.Text) + Convert.ToDouble(lblDia_L15_C12.Text) + Convert.ToDouble(lblDia_L16_C12.Text) + Convert.ToDouble(lblDia_L17_C12.Text) + Convert.ToDouble(lblDia_L18_C12.Text) + Convert.ToDouble(lblDia_L19_C12.Text) + Convert.ToDouble(lblDia_L20_C12.Text) + Convert.ToDouble(lblDia_L21_C12.Text) + Convert.ToDouble(lblDia_L22_C12.Text) + Convert.ToDouble(lblDia_L23_C12.Text) + Convert.ToDouble(lblDia_L24_C12.Text)
        lblTotal_Dia_13.Text = Convert.ToDouble(lblDia_L1_C13.Text) + Convert.ToDouble(lblDia_L2_C13.Text) + Convert.ToDouble(lblDia_L3_C13.Text) + Convert.ToDouble(lblDia_L4_C13.Text) + Convert.ToDouble(lblDia_L5_C13.Text) + Convert.ToDouble(lblDia_L6_C13.Text) + Convert.ToDouble(lblDia_L7_C13.Text) + Convert.ToDouble(lblDia_L8_C13.Text) + Convert.ToDouble(lblDia_L9_C13.Text) + Convert.ToDouble(lblDia_L10_C13.Text) + Convert.ToDouble(lblDia_L11_C13.Text) + Convert.ToDouble(lblDia_L12_C13.Text) + Convert.ToDouble(lblDia_L13_C13.Text) + Convert.ToDouble(lblDia_L14_C13.Text) + Convert.ToDouble(lblDia_L15_C13.Text) + Convert.ToDouble(lblDia_L16_C13.Text) + Convert.ToDouble(lblDia_L17_C13.Text) + Convert.ToDouble(lblDia_L18_C13.Text) + Convert.ToDouble(lblDia_L19_C13.Text) + Convert.ToDouble(lblDia_L20_C13.Text) + Convert.ToDouble(lblDia_L21_C13.Text) + Convert.ToDouble(lblDia_L22_C13.Text) + Convert.ToDouble(lblDia_L23_C13.Text) + Convert.ToDouble(lblDia_L24_C13.Text)
        lblTotal_Dia_14.Text = Convert.ToDouble(lblDia_L1_C14.Text) + Convert.ToDouble(lblDia_L2_C14.Text) + Convert.ToDouble(lblDia_L3_C14.Text) + Convert.ToDouble(lblDia_L4_C14.Text) + Convert.ToDouble(lblDia_L5_C14.Text) + Convert.ToDouble(lblDia_L6_C14.Text) + Convert.ToDouble(lblDia_L7_C14.Text) + Convert.ToDouble(lblDia_L8_C14.Text) + Convert.ToDouble(lblDia_L9_C14.Text) + Convert.ToDouble(lblDia_L10_C14.Text) + Convert.ToDouble(lblDia_L11_C14.Text) + Convert.ToDouble(lblDia_L12_C14.Text) + Convert.ToDouble(lblDia_L13_C14.Text) + Convert.ToDouble(lblDia_L14_C14.Text) + Convert.ToDouble(lblDia_L15_C14.Text) + Convert.ToDouble(lblDia_L16_C14.Text) + Convert.ToDouble(lblDia_L17_C14.Text) + Convert.ToDouble(lblDia_L18_C14.Text) + Convert.ToDouble(lblDia_L19_C14.Text) + Convert.ToDouble(lblDia_L20_C14.Text) + Convert.ToDouble(lblDia_L21_C14.Text) + Convert.ToDouble(lblDia_L22_C14.Text) + Convert.ToDouble(lblDia_L23_C14.Text) + Convert.ToDouble(lblDia_L24_C14.Text)
        lblTotal_Dia_15.Text = Convert.ToDouble(lblDia_L1_C15.Text) + Convert.ToDouble(lblDia_L2_C15.Text) + Convert.ToDouble(lblDia_L3_C15.Text) + Convert.ToDouble(lblDia_L4_C15.Text) + Convert.ToDouble(lblDia_L5_C15.Text) + Convert.ToDouble(lblDia_L6_C15.Text) + Convert.ToDouble(lblDia_L7_C15.Text) + Convert.ToDouble(lblDia_L8_C15.Text) + Convert.ToDouble(lblDia_L9_C15.Text) + Convert.ToDouble(lblDia_L10_C15.Text) + Convert.ToDouble(lblDia_L11_C15.Text) + Convert.ToDouble(lblDia_L12_C15.Text) + Convert.ToDouble(lblDia_L13_C15.Text) + Convert.ToDouble(lblDia_L14_C15.Text) + Convert.ToDouble(lblDia_L15_C15.Text) + Convert.ToDouble(lblDia_L16_C15.Text) + Convert.ToDouble(lblDia_L17_C15.Text) + Convert.ToDouble(lblDia_L18_C15.Text) + Convert.ToDouble(lblDia_L19_C15.Text) + Convert.ToDouble(lblDia_L20_C15.Text) + Convert.ToDouble(lblDia_L21_C15.Text) + Convert.ToDouble(lblDia_L22_C15.Text) + Convert.ToDouble(lblDia_L23_C15.Text) + Convert.ToDouble(lblDia_L24_C15.Text)
        lblTotal_Dia_16.Text = Convert.ToDouble(lblDia_L1_C16.Text) + Convert.ToDouble(lblDia_L2_C16.Text) + Convert.ToDouble(lblDia_L3_C16.Text) + Convert.ToDouble(lblDia_L4_C16.Text) + Convert.ToDouble(lblDia_L5_C16.Text) + Convert.ToDouble(lblDia_L6_C16.Text) + Convert.ToDouble(lblDia_L7_C16.Text) + Convert.ToDouble(lblDia_L8_C16.Text) + Convert.ToDouble(lblDia_L9_C16.Text) + Convert.ToDouble(lblDia_L10_C16.Text) + Convert.ToDouble(lblDia_L11_C16.Text) + Convert.ToDouble(lblDia_L12_C16.Text) + Convert.ToDouble(lblDia_L13_C16.Text) + Convert.ToDouble(lblDia_L14_C16.Text) + Convert.ToDouble(lblDia_L15_C16.Text) + Convert.ToDouble(lblDia_L16_C16.Text) + Convert.ToDouble(lblDia_L17_C16.Text) + Convert.ToDouble(lblDia_L18_C16.Text) + Convert.ToDouble(lblDia_L19_C16.Text) + Convert.ToDouble(lblDia_L20_C16.Text) + Convert.ToDouble(lblDia_L21_C16.Text) + Convert.ToDouble(lblDia_L22_C16.Text) + Convert.ToDouble(lblDia_L23_C16.Text) + Convert.ToDouble(lblDia_L24_C16.Text)
        lblTotal_Dia_17.Text = Convert.ToDouble(lblDia_L1_C17.Text) + Convert.ToDouble(lblDia_L2_C17.Text) + Convert.ToDouble(lblDia_L3_C17.Text) + Convert.ToDouble(lblDia_L4_C17.Text) + Convert.ToDouble(lblDia_L5_C17.Text) + Convert.ToDouble(lblDia_L6_C17.Text) + Convert.ToDouble(lblDia_L7_C17.Text) + Convert.ToDouble(lblDia_L8_C17.Text) + Convert.ToDouble(lblDia_L9_C17.Text) + Convert.ToDouble(lblDia_L10_C17.Text) + Convert.ToDouble(lblDia_L11_C17.Text) + Convert.ToDouble(lblDia_L12_C17.Text) + Convert.ToDouble(lblDia_L13_C17.Text) + Convert.ToDouble(lblDia_L14_C17.Text) + Convert.ToDouble(lblDia_L15_C17.Text) + Convert.ToDouble(lblDia_L16_C17.Text) + Convert.ToDouble(lblDia_L17_C17.Text) + Convert.ToDouble(lblDia_L18_C17.Text) + Convert.ToDouble(lblDia_L19_C17.Text) + Convert.ToDouble(lblDia_L20_C17.Text) + Convert.ToDouble(lblDia_L21_C17.Text) + Convert.ToDouble(lblDia_L22_C17.Text) + Convert.ToDouble(lblDia_L23_C17.Text) + Convert.ToDouble(lblDia_L24_C17.Text)
        lblTotal_Dia_18.Text = Convert.ToDouble(lblDia_L1_C18.Text) + Convert.ToDouble(lblDia_L2_C18.Text) + Convert.ToDouble(lblDia_L3_C18.Text) + Convert.ToDouble(lblDia_L4_C18.Text) + Convert.ToDouble(lblDia_L5_C18.Text) + Convert.ToDouble(lblDia_L6_C18.Text) + Convert.ToDouble(lblDia_L7_C18.Text) + Convert.ToDouble(lblDia_L8_C18.Text) + Convert.ToDouble(lblDia_L9_C18.Text) + Convert.ToDouble(lblDia_L10_C18.Text) + Convert.ToDouble(lblDia_L11_C18.Text) + Convert.ToDouble(lblDia_L12_C18.Text) + Convert.ToDouble(lblDia_L13_C18.Text) + Convert.ToDouble(lblDia_L14_C18.Text) + Convert.ToDouble(lblDia_L15_C18.Text) + Convert.ToDouble(lblDia_L16_C18.Text) + Convert.ToDouble(lblDia_L17_C18.Text) + Convert.ToDouble(lblDia_L18_C18.Text) + Convert.ToDouble(lblDia_L19_C18.Text) + Convert.ToDouble(lblDia_L20_C18.Text) + Convert.ToDouble(lblDia_L21_C18.Text) + Convert.ToDouble(lblDia_L22_C18.Text) + Convert.ToDouble(lblDia_L23_C18.Text) + Convert.ToDouble(lblDia_L24_C18.Text)
        lblTotal_Dia_19.Text = Convert.ToDouble(lblDia_L1_C19.Text) + Convert.ToDouble(lblDia_L2_C19.Text) + Convert.ToDouble(lblDia_L3_C19.Text) + Convert.ToDouble(lblDia_L4_C19.Text) + Convert.ToDouble(lblDia_L5_C19.Text) + Convert.ToDouble(lblDia_L6_C19.Text) + Convert.ToDouble(lblDia_L7_C19.Text) + Convert.ToDouble(lblDia_L8_C19.Text) + Convert.ToDouble(lblDia_L9_C19.Text) + Convert.ToDouble(lblDia_L10_C19.Text) + Convert.ToDouble(lblDia_L11_C19.Text) + Convert.ToDouble(lblDia_L12_C19.Text) + Convert.ToDouble(lblDia_L13_C19.Text) + Convert.ToDouble(lblDia_L14_C19.Text) + Convert.ToDouble(lblDia_L15_C19.Text) + Convert.ToDouble(lblDia_L16_C19.Text) + Convert.ToDouble(lblDia_L17_C19.Text) + Convert.ToDouble(lblDia_L18_C19.Text) + Convert.ToDouble(lblDia_L19_C19.Text) + Convert.ToDouble(lblDia_L20_C19.Text) + Convert.ToDouble(lblDia_L21_C19.Text) + Convert.ToDouble(lblDia_L22_C19.Text) + Convert.ToDouble(lblDia_L23_C19.Text) + Convert.ToDouble(lblDia_L24_C19.Text)
        lblTotal_Dia_20.Text = Convert.ToDouble(lblDia_L1_C20.Text) + Convert.ToDouble(lblDia_L2_C20.Text) + Convert.ToDouble(lblDia_L3_C20.Text) + Convert.ToDouble(lblDia_L4_C20.Text) + Convert.ToDouble(lblDia_L5_C20.Text) + Convert.ToDouble(lblDia_L6_C20.Text) + Convert.ToDouble(lblDia_L7_C20.Text) + Convert.ToDouble(lblDia_L8_C20.Text) + Convert.ToDouble(lblDia_L9_C20.Text) + Convert.ToDouble(lblDia_L10_C20.Text) + Convert.ToDouble(lblDia_L11_C20.Text) + Convert.ToDouble(lblDia_L12_C20.Text) + Convert.ToDouble(lblDia_L13_C20.Text) + Convert.ToDouble(lblDia_L14_C20.Text) + Convert.ToDouble(lblDia_L15_C20.Text) + Convert.ToDouble(lblDia_L16_C20.Text) + Convert.ToDouble(lblDia_L17_C20.Text) + Convert.ToDouble(lblDia_L18_C20.Text) + Convert.ToDouble(lblDia_L19_C20.Text) + Convert.ToDouble(lblDia_L20_C20.Text) + Convert.ToDouble(lblDia_L21_C20.Text) + Convert.ToDouble(lblDia_L22_C20.Text) + Convert.ToDouble(lblDia_L23_C20.Text) + Convert.ToDouble(lblDia_L24_C20.Text)
        lblTotal_Dia_21.Text = Convert.ToDouble(lblDia_L1_C21.Text) + Convert.ToDouble(lblDia_L2_C21.Text) + Convert.ToDouble(lblDia_L3_C21.Text) + Convert.ToDouble(lblDia_L4_C21.Text) + Convert.ToDouble(lblDia_L5_C21.Text) + Convert.ToDouble(lblDia_L6_C21.Text) + Convert.ToDouble(lblDia_L7_C21.Text) + Convert.ToDouble(lblDia_L8_C21.Text) + Convert.ToDouble(lblDia_L9_C21.Text) + Convert.ToDouble(lblDia_L10_C21.Text) + Convert.ToDouble(lblDia_L11_C21.Text) + Convert.ToDouble(lblDia_L12_C21.Text) + Convert.ToDouble(lblDia_L13_C21.Text) + Convert.ToDouble(lblDia_L14_C21.Text) + Convert.ToDouble(lblDia_L15_C21.Text) + Convert.ToDouble(lblDia_L16_C21.Text) + Convert.ToDouble(lblDia_L17_C21.Text) + Convert.ToDouble(lblDia_L18_C21.Text) + Convert.ToDouble(lblDia_L19_C21.Text) + Convert.ToDouble(lblDia_L20_C21.Text) + Convert.ToDouble(lblDia_L21_C21.Text) + Convert.ToDouble(lblDia_L22_C21.Text) + Convert.ToDouble(lblDia_L23_C21.Text) + Convert.ToDouble(lblDia_L24_C21.Text)
        lblTotal_Dia_22.Text = Convert.ToDouble(lblDia_L1_C22.Text) + Convert.ToDouble(lblDia_L2_C22.Text) + Convert.ToDouble(lblDia_L3_C22.Text) + Convert.ToDouble(lblDia_L4_C22.Text) + Convert.ToDouble(lblDia_L5_C22.Text) + Convert.ToDouble(lblDia_L6_C22.Text) + Convert.ToDouble(lblDia_L7_C22.Text) + Convert.ToDouble(lblDia_L8_C22.Text) + Convert.ToDouble(lblDia_L9_C22.Text) + Convert.ToDouble(lblDia_L10_C22.Text) + Convert.ToDouble(lblDia_L11_C22.Text) + Convert.ToDouble(lblDia_L12_C22.Text) + Convert.ToDouble(lblDia_L13_C22.Text) + Convert.ToDouble(lblDia_L14_C22.Text) + Convert.ToDouble(lblDia_L15_C22.Text) + Convert.ToDouble(lblDia_L16_C22.Text) + Convert.ToDouble(lblDia_L17_C22.Text) + Convert.ToDouble(lblDia_L18_C22.Text) + Convert.ToDouble(lblDia_L19_C22.Text) + Convert.ToDouble(lblDia_L20_C22.Text) + Convert.ToDouble(lblDia_L21_C22.Text) + Convert.ToDouble(lblDia_L22_C22.Text) + Convert.ToDouble(lblDia_L23_C22.Text) + Convert.ToDouble(lblDia_L24_C22.Text)
        lblTotal_Dia_23.Text = Convert.ToDouble(lblDia_L1_C23.Text) + Convert.ToDouble(lblDia_L2_C23.Text) + Convert.ToDouble(lblDia_L3_C23.Text) + Convert.ToDouble(lblDia_L4_C23.Text) + Convert.ToDouble(lblDia_L5_C23.Text) + Convert.ToDouble(lblDia_L6_C23.Text) + Convert.ToDouble(lblDia_L7_C23.Text) + Convert.ToDouble(lblDia_L8_C23.Text) + Convert.ToDouble(lblDia_L9_C23.Text) + Convert.ToDouble(lblDia_L10_C23.Text) + Convert.ToDouble(lblDia_L11_C23.Text) + Convert.ToDouble(lblDia_L12_C23.Text) + Convert.ToDouble(lblDia_L13_C23.Text) + Convert.ToDouble(lblDia_L14_C23.Text) + Convert.ToDouble(lblDia_L15_C23.Text) + Convert.ToDouble(lblDia_L16_C23.Text) + Convert.ToDouble(lblDia_L17_C23.Text) + Convert.ToDouble(lblDia_L18_C23.Text) + Convert.ToDouble(lblDia_L19_C23.Text) + Convert.ToDouble(lblDia_L20_C23.Text) + Convert.ToDouble(lblDia_L21_C23.Text) + Convert.ToDouble(lblDia_L22_C23.Text) + Convert.ToDouble(lblDia_L23_C23.Text) + Convert.ToDouble(lblDia_L24_C23.Text)
        lblTotal_Dia_24.Text = Convert.ToDouble(lblDia_L1_C24.Text) + Convert.ToDouble(lblDia_L2_C24.Text) + Convert.ToDouble(lblDia_L3_C24.Text) + Convert.ToDouble(lblDia_L4_C24.Text) + Convert.ToDouble(lblDia_L5_C24.Text) + Convert.ToDouble(lblDia_L6_C24.Text) + Convert.ToDouble(lblDia_L7_C24.Text) + Convert.ToDouble(lblDia_L8_C24.Text) + Convert.ToDouble(lblDia_L9_C24.Text) + Convert.ToDouble(lblDia_L10_C24.Text) + Convert.ToDouble(lblDia_L11_C24.Text) + Convert.ToDouble(lblDia_L12_C24.Text) + Convert.ToDouble(lblDia_L13_C24.Text) + Convert.ToDouble(lblDia_L14_C24.Text) + Convert.ToDouble(lblDia_L15_C24.Text) + Convert.ToDouble(lblDia_L16_C24.Text) + Convert.ToDouble(lblDia_L17_C24.Text) + Convert.ToDouble(lblDia_L18_C24.Text) + Convert.ToDouble(lblDia_L19_C24.Text) + Convert.ToDouble(lblDia_L20_C24.Text) + Convert.ToDouble(lblDia_L21_C24.Text) + Convert.ToDouble(lblDia_L22_C24.Text) + Convert.ToDouble(lblDia_L23_C24.Text) + Convert.ToDouble(lblDia_L24_C24.Text)
        lblTotal_Dia_25.Text = Convert.ToDouble(lblDia_L1_C25.Text) + Convert.ToDouble(lblDia_L2_C25.Text) + Convert.ToDouble(lblDia_L3_C25.Text) + Convert.ToDouble(lblDia_L4_C25.Text) + Convert.ToDouble(lblDia_L5_C25.Text) + Convert.ToDouble(lblDia_L6_C25.Text) + Convert.ToDouble(lblDia_L7_C25.Text) + Convert.ToDouble(lblDia_L8_C25.Text) + Convert.ToDouble(lblDia_L9_C25.Text) + Convert.ToDouble(lblDia_L10_C25.Text) + Convert.ToDouble(lblDia_L11_C25.Text) + Convert.ToDouble(lblDia_L12_C25.Text) + Convert.ToDouble(lblDia_L13_C25.Text) + Convert.ToDouble(lblDia_L14_C25.Text) + Convert.ToDouble(lblDia_L15_C25.Text) + Convert.ToDouble(lblDia_L16_C25.Text) + Convert.ToDouble(lblDia_L17_C25.Text) + Convert.ToDouble(lblDia_L18_C25.Text) + Convert.ToDouble(lblDia_L19_C25.Text) + Convert.ToDouble(lblDia_L20_C25.Text) + Convert.ToDouble(lblDia_L21_C25.Text) + Convert.ToDouble(lblDia_L22_C25.Text) + Convert.ToDouble(lblDia_L23_C25.Text) + Convert.ToDouble(lblDia_L24_C25.Text)
        lblTotal_Dia_26.Text = Convert.ToDouble(lblDia_L1_C26.Text) + Convert.ToDouble(lblDia_L2_C26.Text) + Convert.ToDouble(lblDia_L3_C26.Text) + Convert.ToDouble(lblDia_L4_C26.Text) + Convert.ToDouble(lblDia_L5_C26.Text) + Convert.ToDouble(lblDia_L6_C26.Text) + Convert.ToDouble(lblDia_L7_C26.Text) + Convert.ToDouble(lblDia_L8_C26.Text) + Convert.ToDouble(lblDia_L9_C26.Text) + Convert.ToDouble(lblDia_L10_C26.Text) + Convert.ToDouble(lblDia_L11_C26.Text) + Convert.ToDouble(lblDia_L12_C26.Text) + Convert.ToDouble(lblDia_L13_C26.Text) + Convert.ToDouble(lblDia_L14_C26.Text) + Convert.ToDouble(lblDia_L15_C26.Text) + Convert.ToDouble(lblDia_L16_C26.Text) + Convert.ToDouble(lblDia_L17_C26.Text) + Convert.ToDouble(lblDia_L18_C26.Text) + Convert.ToDouble(lblDia_L19_C26.Text) + Convert.ToDouble(lblDia_L20_C26.Text) + Convert.ToDouble(lblDia_L21_C26.Text) + Convert.ToDouble(lblDia_L22_C26.Text) + Convert.ToDouble(lblDia_L23_C26.Text) + Convert.ToDouble(lblDia_L24_C26.Text)
        lblTotal_Dia_27.Text = Convert.ToDouble(lblDia_L1_C27.Text) + Convert.ToDouble(lblDia_L2_C27.Text) + Convert.ToDouble(lblDia_L3_C27.Text) + Convert.ToDouble(lblDia_L4_C27.Text) + Convert.ToDouble(lblDia_L5_C27.Text) + Convert.ToDouble(lblDia_L6_C27.Text) + Convert.ToDouble(lblDia_L7_C27.Text) + Convert.ToDouble(lblDia_L8_C27.Text) + Convert.ToDouble(lblDia_L9_C27.Text) + Convert.ToDouble(lblDia_L10_C27.Text) + Convert.ToDouble(lblDia_L11_C27.Text) + Convert.ToDouble(lblDia_L12_C27.Text) + Convert.ToDouble(lblDia_L13_C27.Text) + Convert.ToDouble(lblDia_L14_C27.Text) + Convert.ToDouble(lblDia_L15_C27.Text) + Convert.ToDouble(lblDia_L16_C27.Text) + Convert.ToDouble(lblDia_L17_C27.Text) + Convert.ToDouble(lblDia_L18_C27.Text) + Convert.ToDouble(lblDia_L19_C27.Text) + Convert.ToDouble(lblDia_L20_C27.Text) + Convert.ToDouble(lblDia_L21_C27.Text) + Convert.ToDouble(lblDia_L22_C27.Text) + Convert.ToDouble(lblDia_L23_C27.Text) + Convert.ToDouble(lblDia_L24_C27.Text)
        lblTotal_Dia_28.Text = Convert.ToDouble(lblDia_L1_C28.Text) + Convert.ToDouble(lblDia_L2_C28.Text) + Convert.ToDouble(lblDia_L3_C28.Text) + Convert.ToDouble(lblDia_L4_C28.Text) + Convert.ToDouble(lblDia_L5_C28.Text) + Convert.ToDouble(lblDia_L6_C28.Text) + Convert.ToDouble(lblDia_L7_C28.Text) + Convert.ToDouble(lblDia_L8_C28.Text) + Convert.ToDouble(lblDia_L9_C28.Text) + Convert.ToDouble(lblDia_L10_C28.Text) + Convert.ToDouble(lblDia_L11_C28.Text) + Convert.ToDouble(lblDia_L12_C28.Text) + Convert.ToDouble(lblDia_L13_C28.Text) + Convert.ToDouble(lblDia_L14_C28.Text) + Convert.ToDouble(lblDia_L15_C28.Text) + Convert.ToDouble(lblDia_L16_C28.Text) + Convert.ToDouble(lblDia_L17_C28.Text) + Convert.ToDouble(lblDia_L18_C28.Text) + Convert.ToDouble(lblDia_L19_C28.Text) + Convert.ToDouble(lblDia_L20_C28.Text) + Convert.ToDouble(lblDia_L21_C28.Text) + Convert.ToDouble(lblDia_L22_C28.Text) + Convert.ToDouble(lblDia_L23_C28.Text) + Convert.ToDouble(lblDia_L24_C28.Text)
        lblTotal_Dia_29.Text = Convert.ToDouble(lblDia_L1_C29.Text) + Convert.ToDouble(lblDia_L2_C29.Text) + Convert.ToDouble(lblDia_L3_C29.Text) + Convert.ToDouble(lblDia_L4_C29.Text) + Convert.ToDouble(lblDia_L5_C29.Text) + Convert.ToDouble(lblDia_L6_C29.Text) + Convert.ToDouble(lblDia_L7_C29.Text) + Convert.ToDouble(lblDia_L8_C29.Text) + Convert.ToDouble(lblDia_L9_C29.Text) + Convert.ToDouble(lblDia_L10_C29.Text) + Convert.ToDouble(lblDia_L11_C29.Text) + Convert.ToDouble(lblDia_L12_C29.Text) + Convert.ToDouble(lblDia_L13_C29.Text) + Convert.ToDouble(lblDia_L14_C29.Text) + Convert.ToDouble(lblDia_L15_C29.Text) + Convert.ToDouble(lblDia_L16_C29.Text) + Convert.ToDouble(lblDia_L17_C29.Text) + Convert.ToDouble(lblDia_L18_C29.Text) + Convert.ToDouble(lblDia_L19_C29.Text) + Convert.ToDouble(lblDia_L20_C29.Text) + Convert.ToDouble(lblDia_L21_C29.Text) + Convert.ToDouble(lblDia_L22_C29.Text) + Convert.ToDouble(lblDia_L23_C29.Text) + Convert.ToDouble(lblDia_L24_C29.Text)
        lblTotal_Dia_30.Text = Convert.ToDouble(lblDia_L1_C30.Text) + Convert.ToDouble(lblDia_L2_C30.Text) + Convert.ToDouble(lblDia_L3_C30.Text) + Convert.ToDouble(lblDia_L4_C30.Text) + Convert.ToDouble(lblDia_L5_C30.Text) + Convert.ToDouble(lblDia_L6_C30.Text) + Convert.ToDouble(lblDia_L7_C30.Text) + Convert.ToDouble(lblDia_L8_C30.Text) + Convert.ToDouble(lblDia_L9_C30.Text) + Convert.ToDouble(lblDia_L10_C30.Text) + Convert.ToDouble(lblDia_L11_C30.Text) + Convert.ToDouble(lblDia_L12_C30.Text) + Convert.ToDouble(lblDia_L13_C30.Text) + Convert.ToDouble(lblDia_L14_C30.Text) + Convert.ToDouble(lblDia_L15_C30.Text) + Convert.ToDouble(lblDia_L16_C30.Text) + Convert.ToDouble(lblDia_L17_C30.Text) + Convert.ToDouble(lblDia_L18_C30.Text) + Convert.ToDouble(lblDia_L19_C30.Text) + Convert.ToDouble(lblDia_L20_C30.Text) + Convert.ToDouble(lblDia_L21_C30.Text) + Convert.ToDouble(lblDia_L22_C30.Text) + Convert.ToDouble(lblDia_L23_C30.Text) + Convert.ToDouble(lblDia_L24_C30.Text)
        lblTotal_Dia_31.Text = Convert.ToDouble(lblDia_L1_C31.Text) + Convert.ToDouble(lblDia_L2_C31.Text) + Convert.ToDouble(lblDia_L3_C31.Text) + Convert.ToDouble(lblDia_L4_C31.Text) + Convert.ToDouble(lblDia_L5_C31.Text) + Convert.ToDouble(lblDia_L6_C31.Text) + Convert.ToDouble(lblDia_L7_C31.Text) + Convert.ToDouble(lblDia_L8_C31.Text) + Convert.ToDouble(lblDia_L9_C31.Text) + Convert.ToDouble(lblDia_L10_C31.Text) + Convert.ToDouble(lblDia_L11_C31.Text) + Convert.ToDouble(lblDia_L12_C31.Text) + Convert.ToDouble(lblDia_L13_C31.Text) + Convert.ToDouble(lblDia_L14_C31.Text) + Convert.ToDouble(lblDia_L15_C31.Text) + Convert.ToDouble(lblDia_L16_C31.Text) + Convert.ToDouble(lblDia_L17_C31.Text) + Convert.ToDouble(lblDia_L18_C31.Text) + Convert.ToDouble(lblDia_L19_C31.Text) + Convert.ToDouble(lblDia_L20_C31.Text) + Convert.ToDouble(lblDia_L21_C31.Text) + Convert.ToDouble(lblDia_L22_C31.Text) + Convert.ToDouble(lblDia_L23_C31.Text) + Convert.ToDouble(lblDia_L24_C31.Text)

        '-----lista totais por coluna
        lblTotal_Hora_1.Text = Convert.ToDouble(lblDia_L1_C1.Text) + Convert.ToDouble(lblDia_L1_C2.Text) + Convert.ToDouble(lblDia_L1_C3.Text) + Convert.ToDouble(lblDia_L1_C4.Text) + Convert.ToDouble(lblDia_L1_C5.Text) + Convert.ToDouble(lblDia_L1_C6.Text) + Convert.ToDouble(lblDia_L1_C7.Text) + Convert.ToDouble(lblDia_L1_C8.Text) + Convert.ToDouble(lblDia_L1_C9.Text) + Convert.ToDouble(lblDia_L1_C10.Text) + Convert.ToDouble(lblDia_L1_C11.Text) + Convert.ToDouble(lblDia_L1_C12.Text) + Convert.ToDouble(lblDia_L1_C13.Text) + Convert.ToDouble(lblDia_L1_C14.Text) + Convert.ToDouble(lblDia_L1_C15.Text) + Convert.ToDouble(lblDia_L1_C16.Text) + Convert.ToDouble(lblDia_L1_C17.Text) + Convert.ToDouble(lblDia_L1_C18.Text) + Convert.ToDouble(lblDia_L1_C19.Text) + Convert.ToDouble(lblDia_L1_C20.Text) + Convert.ToDouble(lblDia_L1_C21.Text) + Convert.ToDouble(lblDia_L1_C22.Text) + Convert.ToDouble(lblDia_L1_C23.Text) + Convert.ToDouble(lblDia_L1_C24.Text) + Convert.ToDouble(lblDia_L1_C25.Text) + Convert.ToDouble(lblDia_L1_C26.Text) + Convert.ToDouble(lblDia_L1_C27.Text) + Convert.ToDouble(lblDia_L1_C28.Text) + Convert.ToDouble(lblDia_L1_C29.Text) + Convert.ToDouble(lblDia_L1_C30.Text) + Convert.ToDouble(lblDia_L1_C31.Text)
        lblTotal_Hora_2.Text = Convert.ToDouble(lblDia_L2_C1.Text) + Convert.ToDouble(lblDia_L2_C2.Text) + Convert.ToDouble(lblDia_L2_C3.Text) + Convert.ToDouble(lblDia_L2_C4.Text) + Convert.ToDouble(lblDia_L2_C5.Text) + Convert.ToDouble(lblDia_L2_C6.Text) + Convert.ToDouble(lblDia_L2_C7.Text) + Convert.ToDouble(lblDia_L2_C8.Text) + Convert.ToDouble(lblDia_L2_C9.Text) + Convert.ToDouble(lblDia_L2_C10.Text) + Convert.ToDouble(lblDia_L2_C11.Text) + Convert.ToDouble(lblDia_L2_C12.Text) + Convert.ToDouble(lblDia_L2_C13.Text) + Convert.ToDouble(lblDia_L2_C14.Text) + Convert.ToDouble(lblDia_L2_C15.Text) + Convert.ToDouble(lblDia_L2_C16.Text) + Convert.ToDouble(lblDia_L2_C17.Text) + Convert.ToDouble(lblDia_L2_C18.Text) + Convert.ToDouble(lblDia_L2_C19.Text) + Convert.ToDouble(lblDia_L2_C20.Text) + Convert.ToDouble(lblDia_L2_C21.Text) + Convert.ToDouble(lblDia_L2_C22.Text) + Convert.ToDouble(lblDia_L2_C23.Text) + Convert.ToDouble(lblDia_L2_C24.Text) + Convert.ToDouble(lblDia_L2_C25.Text) + Convert.ToDouble(lblDia_L2_C26.Text) + Convert.ToDouble(lblDia_L2_C27.Text) + Convert.ToDouble(lblDia_L2_C28.Text) + Convert.ToDouble(lblDia_L2_C29.Text) + Convert.ToDouble(lblDia_L2_C30.Text) + Convert.ToDouble(lblDia_L2_C31.Text)
        lblTotal_Hora_3.Text = Convert.ToDouble(lblDia_L3_C1.Text) + Convert.ToDouble(lblDia_L3_C2.Text) + Convert.ToDouble(lblDia_L3_C3.Text) + Convert.ToDouble(lblDia_L3_C4.Text) + Convert.ToDouble(lblDia_L3_C5.Text) + Convert.ToDouble(lblDia_L3_C6.Text) + Convert.ToDouble(lblDia_L3_C7.Text) + Convert.ToDouble(lblDia_L3_C8.Text) + Convert.ToDouble(lblDia_L3_C9.Text) + Convert.ToDouble(lblDia_L3_C10.Text) + Convert.ToDouble(lblDia_L3_C11.Text) + Convert.ToDouble(lblDia_L3_C12.Text) + Convert.ToDouble(lblDia_L3_C13.Text) + Convert.ToDouble(lblDia_L3_C14.Text) + Convert.ToDouble(lblDia_L3_C15.Text) + Convert.ToDouble(lblDia_L3_C16.Text) + Convert.ToDouble(lblDia_L3_C17.Text) + Convert.ToDouble(lblDia_L3_C18.Text) + Convert.ToDouble(lblDia_L3_C19.Text) + Convert.ToDouble(lblDia_L3_C20.Text) + Convert.ToDouble(lblDia_L3_C21.Text) + Convert.ToDouble(lblDia_L3_C22.Text) + Convert.ToDouble(lblDia_L3_C23.Text) + Convert.ToDouble(lblDia_L3_C24.Text) + Convert.ToDouble(lblDia_L3_C25.Text) + Convert.ToDouble(lblDia_L3_C26.Text) + Convert.ToDouble(lblDia_L3_C27.Text) + Convert.ToDouble(lblDia_L3_C28.Text) + Convert.ToDouble(lblDia_L3_C29.Text) + Convert.ToDouble(lblDia_L3_C30.Text) + Convert.ToDouble(lblDia_L3_C31.Text)
        lblTotal_Hora_4.Text = Convert.ToDouble(lblDia_L4_C1.Text) + Convert.ToDouble(lblDia_L4_C2.Text) + Convert.ToDouble(lblDia_L4_C3.Text) + Convert.ToDouble(lblDia_L4_C4.Text) + Convert.ToDouble(lblDia_L4_C5.Text) + Convert.ToDouble(lblDia_L4_C6.Text) + Convert.ToDouble(lblDia_L4_C7.Text) + Convert.ToDouble(lblDia_L4_C8.Text) + Convert.ToDouble(lblDia_L4_C9.Text) + Convert.ToDouble(lblDia_L4_C10.Text) + Convert.ToDouble(lblDia_L4_C11.Text) + Convert.ToDouble(lblDia_L4_C12.Text) + Convert.ToDouble(lblDia_L4_C13.Text) + Convert.ToDouble(lblDia_L4_C14.Text) + Convert.ToDouble(lblDia_L4_C15.Text) + Convert.ToDouble(lblDia_L4_C16.Text) + Convert.ToDouble(lblDia_L4_C17.Text) + Convert.ToDouble(lblDia_L4_C18.Text) + Convert.ToDouble(lblDia_L4_C19.Text) + Convert.ToDouble(lblDia_L4_C20.Text) + Convert.ToDouble(lblDia_L4_C21.Text) + Convert.ToDouble(lblDia_L4_C22.Text) + Convert.ToDouble(lblDia_L4_C23.Text) + Convert.ToDouble(lblDia_L4_C24.Text) + Convert.ToDouble(lblDia_L4_C25.Text) + Convert.ToDouble(lblDia_L4_C26.Text) + Convert.ToDouble(lblDia_L4_C27.Text) + Convert.ToDouble(lblDia_L4_C28.Text) + Convert.ToDouble(lblDia_L4_C29.Text) + Convert.ToDouble(lblDia_L4_C30.Text) + Convert.ToDouble(lblDia_L4_C31.Text)
        lblTotal_Hora_5.Text = Convert.ToDouble(lblDia_L5_C1.Text) + Convert.ToDouble(lblDia_L5_C2.Text) + Convert.ToDouble(lblDia_L5_C3.Text) + Convert.ToDouble(lblDia_L5_C4.Text) + Convert.ToDouble(lblDia_L5_C5.Text) + Convert.ToDouble(lblDia_L5_C6.Text) + Convert.ToDouble(lblDia_L5_C7.Text) + Convert.ToDouble(lblDia_L5_C8.Text) + Convert.ToDouble(lblDia_L5_C9.Text) + Convert.ToDouble(lblDia_L5_C10.Text) + Convert.ToDouble(lblDia_L5_C11.Text) + Convert.ToDouble(lblDia_L5_C12.Text) + Convert.ToDouble(lblDia_L5_C13.Text) + Convert.ToDouble(lblDia_L5_C14.Text) + Convert.ToDouble(lblDia_L5_C15.Text) + Convert.ToDouble(lblDia_L5_C16.Text) + Convert.ToDouble(lblDia_L5_C17.Text) + Convert.ToDouble(lblDia_L5_C18.Text) + Convert.ToDouble(lblDia_L5_C19.Text) + Convert.ToDouble(lblDia_L5_C20.Text) + Convert.ToDouble(lblDia_L5_C21.Text) + Convert.ToDouble(lblDia_L5_C22.Text) + Convert.ToDouble(lblDia_L5_C23.Text) + Convert.ToDouble(lblDia_L5_C24.Text) + Convert.ToDouble(lblDia_L5_C25.Text) + Convert.ToDouble(lblDia_L5_C26.Text) + Convert.ToDouble(lblDia_L5_C27.Text) + Convert.ToDouble(lblDia_L5_C28.Text) + Convert.ToDouble(lblDia_L5_C29.Text) + Convert.ToDouble(lblDia_L5_C30.Text) + Convert.ToDouble(lblDia_L5_C31.Text)
        lblTotal_Hora_6.Text = Convert.ToDouble(lblDia_L6_C1.Text) + Convert.ToDouble(lblDia_L6_C2.Text) + Convert.ToDouble(lblDia_L6_C3.Text) + Convert.ToDouble(lblDia_L6_C4.Text) + Convert.ToDouble(lblDia_L6_C5.Text) + Convert.ToDouble(lblDia_L6_C6.Text) + Convert.ToDouble(lblDia_L6_C7.Text) + Convert.ToDouble(lblDia_L6_C8.Text) + Convert.ToDouble(lblDia_L6_C9.Text) + Convert.ToDouble(lblDia_L6_C10.Text) + Convert.ToDouble(lblDia_L6_C11.Text) + Convert.ToDouble(lblDia_L6_C12.Text) + Convert.ToDouble(lblDia_L6_C13.Text) + Convert.ToDouble(lblDia_L6_C14.Text) + Convert.ToDouble(lblDia_L6_C15.Text) + Convert.ToDouble(lblDia_L6_C16.Text) + Convert.ToDouble(lblDia_L6_C17.Text) + Convert.ToDouble(lblDia_L6_C18.Text) + Convert.ToDouble(lblDia_L6_C19.Text) + Convert.ToDouble(lblDia_L6_C20.Text) + Convert.ToDouble(lblDia_L6_C21.Text) + Convert.ToDouble(lblDia_L6_C22.Text) + Convert.ToDouble(lblDia_L6_C23.Text) + Convert.ToDouble(lblDia_L6_C24.Text) + Convert.ToDouble(lblDia_L6_C25.Text) + Convert.ToDouble(lblDia_L6_C26.Text) + Convert.ToDouble(lblDia_L6_C27.Text) + Convert.ToDouble(lblDia_L6_C28.Text) + Convert.ToDouble(lblDia_L6_C29.Text) + Convert.ToDouble(lblDia_L6_C30.Text) + Convert.ToDouble(lblDia_L6_C31.Text)
        lblTotal_Hora_7.Text = Convert.ToDouble(lblDia_L7_C1.Text) + Convert.ToDouble(lblDia_L7_C2.Text) + Convert.ToDouble(lblDia_L7_C3.Text) + Convert.ToDouble(lblDia_L7_C4.Text) + Convert.ToDouble(lblDia_L7_C5.Text) + Convert.ToDouble(lblDia_L7_C6.Text) + Convert.ToDouble(lblDia_L7_C7.Text) + Convert.ToDouble(lblDia_L7_C8.Text) + Convert.ToDouble(lblDia_L7_C9.Text) + Convert.ToDouble(lblDia_L7_C10.Text) + Convert.ToDouble(lblDia_L7_C11.Text) + Convert.ToDouble(lblDia_L7_C12.Text) + Convert.ToDouble(lblDia_L7_C13.Text) + Convert.ToDouble(lblDia_L7_C14.Text) + Convert.ToDouble(lblDia_L7_C15.Text) + Convert.ToDouble(lblDia_L7_C16.Text) + Convert.ToDouble(lblDia_L7_C17.Text) + Convert.ToDouble(lblDia_L7_C18.Text) + Convert.ToDouble(lblDia_L7_C19.Text) + Convert.ToDouble(lblDia_L7_C20.Text) + Convert.ToDouble(lblDia_L7_C21.Text) + Convert.ToDouble(lblDia_L7_C22.Text) + Convert.ToDouble(lblDia_L7_C23.Text) + Convert.ToDouble(lblDia_L7_C24.Text) + Convert.ToDouble(lblDia_L7_C25.Text) + Convert.ToDouble(lblDia_L7_C26.Text) + Convert.ToDouble(lblDia_L7_C27.Text) + Convert.ToDouble(lblDia_L7_C28.Text) + Convert.ToDouble(lblDia_L7_C29.Text) + Convert.ToDouble(lblDia_L7_C30.Text) + Convert.ToDouble(lblDia_L7_C31.Text)
        lblTotal_Hora_8.Text = Convert.ToDouble(lblDia_L8_C1.Text) + Convert.ToDouble(lblDia_L8_C2.Text) + Convert.ToDouble(lblDia_L8_C3.Text) + Convert.ToDouble(lblDia_L8_C4.Text) + Convert.ToDouble(lblDia_L8_C5.Text) + Convert.ToDouble(lblDia_L8_C6.Text) + Convert.ToDouble(lblDia_L8_C7.Text) + Convert.ToDouble(lblDia_L8_C8.Text) + Convert.ToDouble(lblDia_L8_C9.Text) + Convert.ToDouble(lblDia_L8_C10.Text) + Convert.ToDouble(lblDia_L8_C11.Text) + Convert.ToDouble(lblDia_L8_C12.Text) + Convert.ToDouble(lblDia_L8_C13.Text) + Convert.ToDouble(lblDia_L8_C14.Text) + Convert.ToDouble(lblDia_L8_C15.Text) + Convert.ToDouble(lblDia_L8_C16.Text) + Convert.ToDouble(lblDia_L8_C17.Text) + Convert.ToDouble(lblDia_L8_C18.Text) + Convert.ToDouble(lblDia_L8_C19.Text) + Convert.ToDouble(lblDia_L8_C20.Text) + Convert.ToDouble(lblDia_L8_C21.Text) + Convert.ToDouble(lblDia_L8_C22.Text) + Convert.ToDouble(lblDia_L8_C23.Text) + Convert.ToDouble(lblDia_L8_C24.Text) + Convert.ToDouble(lblDia_L8_C25.Text) + Convert.ToDouble(lblDia_L8_C26.Text) + Convert.ToDouble(lblDia_L8_C27.Text) + Convert.ToDouble(lblDia_L8_C28.Text) + Convert.ToDouble(lblDia_L8_C29.Text) + Convert.ToDouble(lblDia_L8_C30.Text) + Convert.ToDouble(lblDia_L8_C31.Text)
        lblTotal_Hora_9.Text = Convert.ToDouble(lblDia_L9_C1.Text) + Convert.ToDouble(lblDia_L9_C2.Text) + Convert.ToDouble(lblDia_L9_C3.Text) + Convert.ToDouble(lblDia_L9_C4.Text) + Convert.ToDouble(lblDia_L9_C5.Text) + Convert.ToDouble(lblDia_L9_C6.Text) + Convert.ToDouble(lblDia_L9_C7.Text) + Convert.ToDouble(lblDia_L9_C8.Text) + Convert.ToDouble(lblDia_L9_C9.Text) + Convert.ToDouble(lblDia_L9_C10.Text) + Convert.ToDouble(lblDia_L9_C11.Text) + Convert.ToDouble(lblDia_L9_C12.Text) + Convert.ToDouble(lblDia_L9_C13.Text) + Convert.ToDouble(lblDia_L9_C14.Text) + Convert.ToDouble(lblDia_L9_C15.Text) + Convert.ToDouble(lblDia_L9_C16.Text) + Convert.ToDouble(lblDia_L9_C17.Text) + Convert.ToDouble(lblDia_L9_C18.Text) + Convert.ToDouble(lblDia_L9_C19.Text) + Convert.ToDouble(lblDia_L9_C20.Text) + Convert.ToDouble(lblDia_L9_C21.Text) + Convert.ToDouble(lblDia_L9_C22.Text) + Convert.ToDouble(lblDia_L9_C23.Text) + Convert.ToDouble(lblDia_L9_C24.Text) + Convert.ToDouble(lblDia_L9_C25.Text) + Convert.ToDouble(lblDia_L9_C26.Text) + Convert.ToDouble(lblDia_L9_C27.Text) + Convert.ToDouble(lblDia_L9_C28.Text) + Convert.ToDouble(lblDia_L9_C29.Text) + Convert.ToDouble(lblDia_L9_C30.Text) + Convert.ToDouble(lblDia_L9_C31.Text)
        lblTotal_Hora_10.Text = Convert.ToDouble(lblDia_L10_C1.Text) + Convert.ToDouble(lblDia_L10_C2.Text) + Convert.ToDouble(lblDia_L10_C3.Text) + Convert.ToDouble(lblDia_L10_C4.Text) + Convert.ToDouble(lblDia_L10_C5.Text) + Convert.ToDouble(lblDia_L10_C6.Text) + Convert.ToDouble(lblDia_L10_C7.Text) + Convert.ToDouble(lblDia_L10_C8.Text) + Convert.ToDouble(lblDia_L10_C9.Text) + Convert.ToDouble(lblDia_L10_C10.Text) + Convert.ToDouble(lblDia_L10_C11.Text) + Convert.ToDouble(lblDia_L10_C12.Text) + Convert.ToDouble(lblDia_L10_C13.Text) + Convert.ToDouble(lblDia_L10_C14.Text) + Convert.ToDouble(lblDia_L10_C15.Text) + Convert.ToDouble(lblDia_L10_C16.Text) + Convert.ToDouble(lblDia_L10_C17.Text) + Convert.ToDouble(lblDia_L10_C18.Text) + Convert.ToDouble(lblDia_L10_C19.Text) + Convert.ToDouble(lblDia_L10_C20.Text) + Convert.ToDouble(lblDia_L10_C21.Text) + Convert.ToDouble(lblDia_L10_C22.Text) + Convert.ToDouble(lblDia_L10_C23.Text) + Convert.ToDouble(lblDia_L10_C24.Text) + Convert.ToDouble(lblDia_L10_C25.Text) + Convert.ToDouble(lblDia_L10_C26.Text) + Convert.ToDouble(lblDia_L10_C27.Text) + Convert.ToDouble(lblDia_L10_C28.Text) + Convert.ToDouble(lblDia_L10_C29.Text) + Convert.ToDouble(lblDia_L10_C30.Text) + Convert.ToDouble(lblDia_L10_C31.Text)
        lblTotal_Hora_11.Text = Convert.ToDouble(lblDia_L11_C1.Text) + Convert.ToDouble(lblDia_L11_C2.Text) + Convert.ToDouble(lblDia_L11_C3.Text) + Convert.ToDouble(lblDia_L11_C4.Text) + Convert.ToDouble(lblDia_L11_C5.Text) + Convert.ToDouble(lblDia_L11_C6.Text) + Convert.ToDouble(lblDia_L11_C7.Text) + Convert.ToDouble(lblDia_L11_C8.Text) + Convert.ToDouble(lblDia_L11_C9.Text) + Convert.ToDouble(lblDia_L11_C10.Text) + Convert.ToDouble(lblDia_L11_C11.Text) + Convert.ToDouble(lblDia_L11_C12.Text) + Convert.ToDouble(lblDia_L11_C13.Text) + Convert.ToDouble(lblDia_L11_C14.Text) + Convert.ToDouble(lblDia_L11_C15.Text) + Convert.ToDouble(lblDia_L11_C16.Text) + Convert.ToDouble(lblDia_L11_C17.Text) + Convert.ToDouble(lblDia_L11_C18.Text) + Convert.ToDouble(lblDia_L11_C19.Text) + Convert.ToDouble(lblDia_L11_C20.Text) + Convert.ToDouble(lblDia_L11_C21.Text) + Convert.ToDouble(lblDia_L11_C22.Text) + Convert.ToDouble(lblDia_L11_C23.Text) + Convert.ToDouble(lblDia_L11_C24.Text) + Convert.ToDouble(lblDia_L11_C25.Text) + Convert.ToDouble(lblDia_L11_C26.Text) + Convert.ToDouble(lblDia_L11_C27.Text) + Convert.ToDouble(lblDia_L11_C28.Text) + Convert.ToDouble(lblDia_L11_C29.Text) + Convert.ToDouble(lblDia_L11_C30.Text) + Convert.ToDouble(lblDia_L11_C31.Text)
        lblTotal_Hora_12.Text = Convert.ToDouble(lblDia_L12_C1.Text) + Convert.ToDouble(lblDia_L12_C2.Text) + Convert.ToDouble(lblDia_L12_C3.Text) + Convert.ToDouble(lblDia_L12_C4.Text) + Convert.ToDouble(lblDia_L12_C5.Text) + Convert.ToDouble(lblDia_L12_C6.Text) + Convert.ToDouble(lblDia_L12_C7.Text) + Convert.ToDouble(lblDia_L12_C8.Text) + Convert.ToDouble(lblDia_L12_C9.Text) + Convert.ToDouble(lblDia_L12_C10.Text) + Convert.ToDouble(lblDia_L12_C11.Text) + Convert.ToDouble(lblDia_L12_C12.Text) + Convert.ToDouble(lblDia_L12_C13.Text) + Convert.ToDouble(lblDia_L12_C14.Text) + Convert.ToDouble(lblDia_L12_C15.Text) + Convert.ToDouble(lblDia_L12_C16.Text) + Convert.ToDouble(lblDia_L12_C17.Text) + Convert.ToDouble(lblDia_L12_C18.Text) + Convert.ToDouble(lblDia_L12_C19.Text) + Convert.ToDouble(lblDia_L12_C20.Text) + Convert.ToDouble(lblDia_L12_C21.Text) + Convert.ToDouble(lblDia_L12_C22.Text) + Convert.ToDouble(lblDia_L12_C23.Text) + Convert.ToDouble(lblDia_L12_C24.Text) + Convert.ToDouble(lblDia_L12_C25.Text) + Convert.ToDouble(lblDia_L12_C26.Text) + Convert.ToDouble(lblDia_L12_C27.Text) + Convert.ToDouble(lblDia_L12_C28.Text) + Convert.ToDouble(lblDia_L12_C29.Text) + Convert.ToDouble(lblDia_L12_C30.Text) + Convert.ToDouble(lblDia_L12_C31.Text)
        lblTotal_Hora_13.Text = Convert.ToDouble(lblDia_L13_C1.Text) + Convert.ToDouble(lblDia_L13_C2.Text) + Convert.ToDouble(lblDia_L13_C3.Text) + Convert.ToDouble(lblDia_L13_C4.Text) + Convert.ToDouble(lblDia_L13_C5.Text) + Convert.ToDouble(lblDia_L13_C6.Text) + Convert.ToDouble(lblDia_L13_C7.Text) + Convert.ToDouble(lblDia_L13_C8.Text) + Convert.ToDouble(lblDia_L13_C9.Text) + Convert.ToDouble(lblDia_L13_C10.Text) + Convert.ToDouble(lblDia_L13_C11.Text) + Convert.ToDouble(lblDia_L13_C12.Text) + Convert.ToDouble(lblDia_L13_C13.Text) + Convert.ToDouble(lblDia_L13_C14.Text) + Convert.ToDouble(lblDia_L13_C15.Text) + Convert.ToDouble(lblDia_L13_C16.Text) + Convert.ToDouble(lblDia_L13_C17.Text) + Convert.ToDouble(lblDia_L13_C18.Text) + Convert.ToDouble(lblDia_L13_C19.Text) + Convert.ToDouble(lblDia_L13_C20.Text) + Convert.ToDouble(lblDia_L13_C21.Text) + Convert.ToDouble(lblDia_L13_C22.Text) + Convert.ToDouble(lblDia_L13_C23.Text) + Convert.ToDouble(lblDia_L13_C24.Text) + Convert.ToDouble(lblDia_L13_C25.Text) + Convert.ToDouble(lblDia_L13_C26.Text) + Convert.ToDouble(lblDia_L13_C27.Text) + Convert.ToDouble(lblDia_L13_C28.Text) + Convert.ToDouble(lblDia_L13_C29.Text) + Convert.ToDouble(lblDia_L13_C30.Text) + Convert.ToDouble(lblDia_L13_C31.Text)
        lblTotal_Hora_14.Text = Convert.ToDouble(lblDia_L14_C1.Text) + Convert.ToDouble(lblDia_L14_C2.Text) + Convert.ToDouble(lblDia_L14_C3.Text) + Convert.ToDouble(lblDia_L14_C4.Text) + Convert.ToDouble(lblDia_L14_C5.Text) + Convert.ToDouble(lblDia_L14_C6.Text) + Convert.ToDouble(lblDia_L14_C7.Text) + Convert.ToDouble(lblDia_L14_C8.Text) + Convert.ToDouble(lblDia_L14_C9.Text) + Convert.ToDouble(lblDia_L14_C10.Text) + Convert.ToDouble(lblDia_L14_C11.Text) + Convert.ToDouble(lblDia_L14_C12.Text) + Convert.ToDouble(lblDia_L14_C13.Text) + Convert.ToDouble(lblDia_L14_C14.Text) + Convert.ToDouble(lblDia_L14_C15.Text) + Convert.ToDouble(lblDia_L14_C16.Text) + Convert.ToDouble(lblDia_L14_C17.Text) + Convert.ToDouble(lblDia_L14_C18.Text) + Convert.ToDouble(lblDia_L14_C19.Text) + Convert.ToDouble(lblDia_L14_C20.Text) + Convert.ToDouble(lblDia_L14_C21.Text) + Convert.ToDouble(lblDia_L14_C22.Text) + Convert.ToDouble(lblDia_L14_C23.Text) + Convert.ToDouble(lblDia_L14_C24.Text) + Convert.ToDouble(lblDia_L14_C25.Text) + Convert.ToDouble(lblDia_L14_C26.Text) + Convert.ToDouble(lblDia_L14_C27.Text) + Convert.ToDouble(lblDia_L14_C28.Text) + Convert.ToDouble(lblDia_L14_C29.Text) + Convert.ToDouble(lblDia_L14_C30.Text) + Convert.ToDouble(lblDia_L14_C31.Text)
        lblTotal_Hora_15.Text = Convert.ToDouble(lblDia_L15_C1.Text) + Convert.ToDouble(lblDia_L15_C2.Text) + Convert.ToDouble(lblDia_L15_C3.Text) + Convert.ToDouble(lblDia_L15_C4.Text) + Convert.ToDouble(lblDia_L15_C5.Text) + Convert.ToDouble(lblDia_L15_C6.Text) + Convert.ToDouble(lblDia_L15_C7.Text) + Convert.ToDouble(lblDia_L15_C8.Text) + Convert.ToDouble(lblDia_L15_C9.Text) + Convert.ToDouble(lblDia_L15_C10.Text) + Convert.ToDouble(lblDia_L15_C11.Text) + Convert.ToDouble(lblDia_L15_C12.Text) + Convert.ToDouble(lblDia_L15_C13.Text) + Convert.ToDouble(lblDia_L15_C14.Text) + Convert.ToDouble(lblDia_L15_C15.Text) + Convert.ToDouble(lblDia_L15_C16.Text) + Convert.ToDouble(lblDia_L15_C17.Text) + Convert.ToDouble(lblDia_L15_C18.Text) + Convert.ToDouble(lblDia_L15_C19.Text) + Convert.ToDouble(lblDia_L15_C20.Text) + Convert.ToDouble(lblDia_L15_C21.Text) + Convert.ToDouble(lblDia_L15_C22.Text) + Convert.ToDouble(lblDia_L15_C23.Text) + Convert.ToDouble(lblDia_L15_C24.Text) + Convert.ToDouble(lblDia_L15_C25.Text) + Convert.ToDouble(lblDia_L15_C26.Text) + Convert.ToDouble(lblDia_L15_C27.Text) + Convert.ToDouble(lblDia_L15_C28.Text) + Convert.ToDouble(lblDia_L15_C29.Text) + Convert.ToDouble(lblDia_L15_C30.Text) + Convert.ToDouble(lblDia_L15_C31.Text)
        lblTotal_Hora_16.Text = Convert.ToDouble(lblDia_L16_C1.Text) + Convert.ToDouble(lblDia_L16_C2.Text) + Convert.ToDouble(lblDia_L16_C3.Text) + Convert.ToDouble(lblDia_L16_C4.Text) + Convert.ToDouble(lblDia_L16_C5.Text) + Convert.ToDouble(lblDia_L16_C6.Text) + Convert.ToDouble(lblDia_L16_C7.Text) + Convert.ToDouble(lblDia_L16_C8.Text) + Convert.ToDouble(lblDia_L16_C9.Text) + Convert.ToDouble(lblDia_L16_C10.Text) + Convert.ToDouble(lblDia_L16_C11.Text) + Convert.ToDouble(lblDia_L16_C12.Text) + Convert.ToDouble(lblDia_L16_C13.Text) + Convert.ToDouble(lblDia_L16_C14.Text) + Convert.ToDouble(lblDia_L16_C15.Text) + Convert.ToDouble(lblDia_L16_C16.Text) + Convert.ToDouble(lblDia_L16_C17.Text) + Convert.ToDouble(lblDia_L16_C18.Text) + Convert.ToDouble(lblDia_L16_C19.Text) + Convert.ToDouble(lblDia_L16_C20.Text) + Convert.ToDouble(lblDia_L16_C21.Text) + Convert.ToDouble(lblDia_L16_C22.Text) + Convert.ToDouble(lblDia_L16_C23.Text) + Convert.ToDouble(lblDia_L16_C24.Text) + Convert.ToDouble(lblDia_L16_C25.Text) + Convert.ToDouble(lblDia_L16_C26.Text) + Convert.ToDouble(lblDia_L16_C27.Text) + Convert.ToDouble(lblDia_L16_C28.Text) + Convert.ToDouble(lblDia_L16_C29.Text) + Convert.ToDouble(lblDia_L16_C30.Text) + Convert.ToDouble(lblDia_L16_C31.Text)
        lblTotal_Hora_17.Text = Convert.ToDouble(lblDia_L17_C1.Text) + Convert.ToDouble(lblDia_L17_C2.Text) + Convert.ToDouble(lblDia_L17_C3.Text) + Convert.ToDouble(lblDia_L17_C4.Text) + Convert.ToDouble(lblDia_L17_C5.Text) + Convert.ToDouble(lblDia_L17_C6.Text) + Convert.ToDouble(lblDia_L17_C7.Text) + Convert.ToDouble(lblDia_L17_C8.Text) + Convert.ToDouble(lblDia_L17_C9.Text) + Convert.ToDouble(lblDia_L17_C10.Text) + Convert.ToDouble(lblDia_L17_C11.Text) + Convert.ToDouble(lblDia_L17_C12.Text) + Convert.ToDouble(lblDia_L17_C13.Text) + Convert.ToDouble(lblDia_L17_C14.Text) + Convert.ToDouble(lblDia_L17_C15.Text) + Convert.ToDouble(lblDia_L17_C16.Text) + Convert.ToDouble(lblDia_L17_C17.Text) + Convert.ToDouble(lblDia_L17_C18.Text) + Convert.ToDouble(lblDia_L17_C19.Text) + Convert.ToDouble(lblDia_L17_C20.Text) + Convert.ToDouble(lblDia_L17_C21.Text) + Convert.ToDouble(lblDia_L17_C22.Text) + Convert.ToDouble(lblDia_L17_C23.Text) + Convert.ToDouble(lblDia_L17_C24.Text) + Convert.ToDouble(lblDia_L17_C25.Text) + Convert.ToDouble(lblDia_L17_C26.Text) + Convert.ToDouble(lblDia_L17_C27.Text) + Convert.ToDouble(lblDia_L17_C28.Text) + Convert.ToDouble(lblDia_L17_C29.Text) + Convert.ToDouble(lblDia_L17_C30.Text) + Convert.ToDouble(lblDia_L17_C31.Text)
        lblTotal_Hora_18.Text = Convert.ToDouble(lblDia_L18_C1.Text) + Convert.ToDouble(lblDia_L18_C2.Text) + Convert.ToDouble(lblDia_L18_C3.Text) + Convert.ToDouble(lblDia_L18_C4.Text) + Convert.ToDouble(lblDia_L18_C5.Text) + Convert.ToDouble(lblDia_L18_C6.Text) + Convert.ToDouble(lblDia_L18_C7.Text) + Convert.ToDouble(lblDia_L18_C8.Text) + Convert.ToDouble(lblDia_L18_C9.Text) + Convert.ToDouble(lblDia_L18_C10.Text) + Convert.ToDouble(lblDia_L18_C11.Text) + Convert.ToDouble(lblDia_L18_C12.Text) + Convert.ToDouble(lblDia_L18_C13.Text) + Convert.ToDouble(lblDia_L18_C14.Text) + Convert.ToDouble(lblDia_L18_C15.Text) + Convert.ToDouble(lblDia_L18_C16.Text) + Convert.ToDouble(lblDia_L18_C17.Text) + Convert.ToDouble(lblDia_L18_C18.Text) + Convert.ToDouble(lblDia_L18_C19.Text) + Convert.ToDouble(lblDia_L18_C20.Text) + Convert.ToDouble(lblDia_L18_C21.Text) + Convert.ToDouble(lblDia_L18_C22.Text) + Convert.ToDouble(lblDia_L18_C23.Text) + Convert.ToDouble(lblDia_L18_C24.Text) + Convert.ToDouble(lblDia_L18_C25.Text) + Convert.ToDouble(lblDia_L18_C26.Text) + Convert.ToDouble(lblDia_L18_C27.Text) + Convert.ToDouble(lblDia_L18_C28.Text) + Convert.ToDouble(lblDia_L18_C29.Text) + Convert.ToDouble(lblDia_L18_C30.Text) + Convert.ToDouble(lblDia_L18_C31.Text)
        lblTotal_Hora_19.Text = Convert.ToDouble(lblDia_L19_C1.Text) + Convert.ToDouble(lblDia_L19_C2.Text) + Convert.ToDouble(lblDia_L19_C3.Text) + Convert.ToDouble(lblDia_L19_C4.Text) + Convert.ToDouble(lblDia_L19_C5.Text) + Convert.ToDouble(lblDia_L19_C6.Text) + Convert.ToDouble(lblDia_L19_C7.Text) + Convert.ToDouble(lblDia_L19_C8.Text) + Convert.ToDouble(lblDia_L19_C9.Text) + Convert.ToDouble(lblDia_L19_C10.Text) + Convert.ToDouble(lblDia_L19_C11.Text) + Convert.ToDouble(lblDia_L19_C12.Text) + Convert.ToDouble(lblDia_L19_C13.Text) + Convert.ToDouble(lblDia_L19_C14.Text) + Convert.ToDouble(lblDia_L19_C15.Text) + Convert.ToDouble(lblDia_L19_C16.Text) + Convert.ToDouble(lblDia_L19_C17.Text) + Convert.ToDouble(lblDia_L19_C18.Text) + Convert.ToDouble(lblDia_L19_C19.Text) + Convert.ToDouble(lblDia_L19_C20.Text) + Convert.ToDouble(lblDia_L19_C21.Text) + Convert.ToDouble(lblDia_L19_C22.Text) + Convert.ToDouble(lblDia_L19_C23.Text) + Convert.ToDouble(lblDia_L19_C24.Text) + Convert.ToDouble(lblDia_L19_C25.Text) + Convert.ToDouble(lblDia_L19_C26.Text) + Convert.ToDouble(lblDia_L19_C27.Text) + Convert.ToDouble(lblDia_L19_C28.Text) + Convert.ToDouble(lblDia_L19_C29.Text) + Convert.ToDouble(lblDia_L19_C30.Text) + Convert.ToDouble(lblDia_L19_C31.Text)
        lblTotal_Hora_20.Text = Convert.ToDouble(lblDia_L20_C1.Text) + Convert.ToDouble(lblDia_L20_C2.Text) + Convert.ToDouble(lblDia_L20_C3.Text) + Convert.ToDouble(lblDia_L20_C4.Text) + Convert.ToDouble(lblDia_L20_C5.Text) + Convert.ToDouble(lblDia_L20_C6.Text) + Convert.ToDouble(lblDia_L20_C7.Text) + Convert.ToDouble(lblDia_L20_C8.Text) + Convert.ToDouble(lblDia_L20_C9.Text) + Convert.ToDouble(lblDia_L20_C10.Text) + Convert.ToDouble(lblDia_L20_C11.Text) + Convert.ToDouble(lblDia_L20_C12.Text) + Convert.ToDouble(lblDia_L20_C13.Text) + Convert.ToDouble(lblDia_L20_C14.Text) + Convert.ToDouble(lblDia_L20_C15.Text) + Convert.ToDouble(lblDia_L20_C16.Text) + Convert.ToDouble(lblDia_L20_C17.Text) + Convert.ToDouble(lblDia_L20_C18.Text) + Convert.ToDouble(lblDia_L20_C19.Text) + Convert.ToDouble(lblDia_L20_C20.Text) + Convert.ToDouble(lblDia_L20_C21.Text) + Convert.ToDouble(lblDia_L20_C22.Text) + Convert.ToDouble(lblDia_L20_C23.Text) + Convert.ToDouble(lblDia_L20_C24.Text) + Convert.ToDouble(lblDia_L20_C25.Text) + Convert.ToDouble(lblDia_L20_C26.Text) + Convert.ToDouble(lblDia_L20_C27.Text) + Convert.ToDouble(lblDia_L20_C28.Text) + Convert.ToDouble(lblDia_L20_C29.Text) + Convert.ToDouble(lblDia_L20_C30.Text) + Convert.ToDouble(lblDia_L20_C31.Text)
        lblTotal_Hora_21.Text = Convert.ToDouble(lblDia_L21_C1.Text) + Convert.ToDouble(lblDia_L21_C2.Text) + Convert.ToDouble(lblDia_L21_C3.Text) + Convert.ToDouble(lblDia_L21_C4.Text) + Convert.ToDouble(lblDia_L21_C5.Text) + Convert.ToDouble(lblDia_L21_C6.Text) + Convert.ToDouble(lblDia_L21_C7.Text) + Convert.ToDouble(lblDia_L21_C8.Text) + Convert.ToDouble(lblDia_L21_C9.Text) + Convert.ToDouble(lblDia_L21_C10.Text) + Convert.ToDouble(lblDia_L21_C11.Text) + Convert.ToDouble(lblDia_L21_C12.Text) + Convert.ToDouble(lblDia_L21_C13.Text) + Convert.ToDouble(lblDia_L21_C14.Text) + Convert.ToDouble(lblDia_L21_C15.Text) + Convert.ToDouble(lblDia_L21_C16.Text) + Convert.ToDouble(lblDia_L21_C17.Text) + Convert.ToDouble(lblDia_L21_C18.Text) + Convert.ToDouble(lblDia_L21_C19.Text) + Convert.ToDouble(lblDia_L21_C20.Text) + Convert.ToDouble(lblDia_L21_C21.Text) + Convert.ToDouble(lblDia_L21_C22.Text) + Convert.ToDouble(lblDia_L21_C23.Text) + Convert.ToDouble(lblDia_L21_C24.Text) + Convert.ToDouble(lblDia_L21_C25.Text) + Convert.ToDouble(lblDia_L21_C26.Text) + Convert.ToDouble(lblDia_L21_C27.Text) + Convert.ToDouble(lblDia_L21_C28.Text) + Convert.ToDouble(lblDia_L21_C29.Text) + Convert.ToDouble(lblDia_L21_C30.Text) + Convert.ToDouble(lblDia_L21_C31.Text)
        lblTotal_Hora_22.Text = Convert.ToDouble(lblDia_L22_C1.Text) + Convert.ToDouble(lblDia_L22_C2.Text) + Convert.ToDouble(lblDia_L22_C3.Text) + Convert.ToDouble(lblDia_L22_C4.Text) + Convert.ToDouble(lblDia_L22_C5.Text) + Convert.ToDouble(lblDia_L22_C6.Text) + Convert.ToDouble(lblDia_L22_C7.Text) + Convert.ToDouble(lblDia_L22_C8.Text) + Convert.ToDouble(lblDia_L22_C9.Text) + Convert.ToDouble(lblDia_L22_C10.Text) + Convert.ToDouble(lblDia_L22_C11.Text) + Convert.ToDouble(lblDia_L22_C12.Text) + Convert.ToDouble(lblDia_L22_C13.Text) + Convert.ToDouble(lblDia_L22_C14.Text) + Convert.ToDouble(lblDia_L22_C15.Text) + Convert.ToDouble(lblDia_L22_C16.Text) + Convert.ToDouble(lblDia_L22_C17.Text) + Convert.ToDouble(lblDia_L22_C18.Text) + Convert.ToDouble(lblDia_L22_C19.Text) + Convert.ToDouble(lblDia_L22_C20.Text) + Convert.ToDouble(lblDia_L22_C21.Text) + Convert.ToDouble(lblDia_L22_C22.Text) + Convert.ToDouble(lblDia_L22_C23.Text) + Convert.ToDouble(lblDia_L22_C24.Text) + Convert.ToDouble(lblDia_L22_C25.Text) + Convert.ToDouble(lblDia_L22_C26.Text) + Convert.ToDouble(lblDia_L22_C27.Text) + Convert.ToDouble(lblDia_L22_C28.Text) + Convert.ToDouble(lblDia_L22_C29.Text) + Convert.ToDouble(lblDia_L22_C30.Text) + Convert.ToDouble(lblDia_L22_C31.Text)
        lblTotal_Hora_23.Text = Convert.ToDouble(lblDia_L23_C1.Text) + Convert.ToDouble(lblDia_L23_C2.Text) + Convert.ToDouble(lblDia_L23_C3.Text) + Convert.ToDouble(lblDia_L23_C4.Text) + Convert.ToDouble(lblDia_L23_C5.Text) + Convert.ToDouble(lblDia_L23_C6.Text) + Convert.ToDouble(lblDia_L23_C7.Text) + Convert.ToDouble(lblDia_L23_C8.Text) + Convert.ToDouble(lblDia_L23_C9.Text) + Convert.ToDouble(lblDia_L23_C10.Text) + Convert.ToDouble(lblDia_L23_C11.Text) + Convert.ToDouble(lblDia_L23_C12.Text) + Convert.ToDouble(lblDia_L23_C13.Text) + Convert.ToDouble(lblDia_L23_C14.Text) + Convert.ToDouble(lblDia_L23_C15.Text) + Convert.ToDouble(lblDia_L23_C16.Text) + Convert.ToDouble(lblDia_L23_C17.Text) + Convert.ToDouble(lblDia_L23_C18.Text) + Convert.ToDouble(lblDia_L23_C19.Text) + Convert.ToDouble(lblDia_L23_C20.Text) + Convert.ToDouble(lblDia_L23_C21.Text) + Convert.ToDouble(lblDia_L23_C22.Text) + Convert.ToDouble(lblDia_L23_C23.Text) + Convert.ToDouble(lblDia_L23_C24.Text) + Convert.ToDouble(lblDia_L23_C25.Text) + Convert.ToDouble(lblDia_L23_C26.Text) + Convert.ToDouble(lblDia_L23_C27.Text) + Convert.ToDouble(lblDia_L23_C28.Text) + Convert.ToDouble(lblDia_L23_C29.Text) + Convert.ToDouble(lblDia_L23_C30.Text) + Convert.ToDouble(lblDia_L23_C31.Text)
        lblTotal_Hora_24.Text = Convert.ToDouble(lblDia_L24_C1.Text) + Convert.ToDouble(lblDia_L24_C2.Text) + Convert.ToDouble(lblDia_L24_C3.Text) + Convert.ToDouble(lblDia_L24_C4.Text) + Convert.ToDouble(lblDia_L24_C5.Text) + Convert.ToDouble(lblDia_L24_C6.Text) + Convert.ToDouble(lblDia_L24_C7.Text) + Convert.ToDouble(lblDia_L24_C8.Text) + Convert.ToDouble(lblDia_L24_C9.Text) + Convert.ToDouble(lblDia_L24_C10.Text) + Convert.ToDouble(lblDia_L24_C11.Text) + Convert.ToDouble(lblDia_L24_C12.Text) + Convert.ToDouble(lblDia_L24_C13.Text) + Convert.ToDouble(lblDia_L24_C14.Text) + Convert.ToDouble(lblDia_L24_C15.Text) + Convert.ToDouble(lblDia_L24_C16.Text) + Convert.ToDouble(lblDia_L24_C17.Text) + Convert.ToDouble(lblDia_L24_C18.Text) + Convert.ToDouble(lblDia_L24_C19.Text) + Convert.ToDouble(lblDia_L24_C20.Text) + Convert.ToDouble(lblDia_L24_C21.Text) + Convert.ToDouble(lblDia_L24_C22.Text) + Convert.ToDouble(lblDia_L24_C23.Text) + Convert.ToDouble(lblDia_L24_C24.Text) + Convert.ToDouble(lblDia_L24_C25.Text) + Convert.ToDouble(lblDia_L24_C26.Text) + Convert.ToDouble(lblDia_L24_C27.Text) + Convert.ToDouble(lblDia_L24_C28.Text) + Convert.ToDouble(lblDia_L24_C29.Text) + Convert.ToDouble(lblDia_L24_C30.Text) + Convert.ToDouble(lblDia_L24_C31.Text)
    End Sub

    Public Sub monta_Grafico_Hora()
        Dim fDt_Lote(23)
        Dim fCusto(23)

        fDt_Lote(0) = lblHora_1.Text
        fCusto(0) = lblTotal_Hora_1.Text

        fDt_Lote(1) = lblHora_2.Text
        fCusto(1) = lblTotal_Hora_2.Text

        fDt_Lote(2) = lblHora_3.Text
        fCusto(2) = lblTotal_Hora_3.Text

        fDt_Lote(3) = lblHora_4.Text
        fCusto(3) = lblTotal_Hora_4.Text

        fDt_Lote(4) = lblHora_5.Text
        fCusto(4) = lblTotal_Hora_5.Text

        fDt_Lote(5) = lblHora_6.Text
        fCusto(5) = lblTotal_Hora_6.Text

        fDt_Lote(6) = lblHora_7.Text
        fCusto(6) = lblTotal_Hora_7.Text

        fDt_Lote(7) = lblHora_8.Text
        fCusto(7) = lblTotal_Hora_8.Text

        fDt_Lote(8) = lblHora_9.Text
        fCusto(8) = lblTotal_Hora_9.Text

        fDt_Lote(9) = lblHora_10.Text
        fCusto(9) = lblTotal_Hora_10.Text

        fDt_Lote(10) = lblHora_11.Text
        fCusto(10) = lblTotal_Hora_11.Text

        fDt_Lote(11) = lblHora_12.Text
        fCusto(11) = lblTotal_Hora_12.Text

        fDt_Lote(12) = lblHora_13.Text
        fCusto(12) = lblTotal_Hora_13.Text

        fDt_Lote(13) = lblHora_14.Text
        fCusto(13) = lblTotal_Hora_14.Text

        fDt_Lote(14) = lblHora_15.Text
        fCusto(14) = lblTotal_Hora_15.Text

        fDt_Lote(15) = lblHora_16.Text
        fCusto(15) = lblTotal_Hora_16.Text

        fDt_Lote(16) = lblHora_17.Text
        fCusto(16) = lblTotal_Hora_17.Text

        fDt_Lote(17) = lblHora_18.Text
        fCusto(17) = lblTotal_Hora_18.Text

        fDt_Lote(18) = lblHora_19.Text
        fCusto(18) = lblTotal_Hora_19.Text

        fDt_Lote(19) = lblHora_20.Text
        fCusto(19) = lblTotal_Hora_20.Text

        fDt_Lote(20) = lblHora_21.Text
        fCusto(20) = lblTotal_Hora_21.Text

        fDt_Lote(21) = lblHora_22.Text
        fCusto(21) = lblTotal_Hora_22.Text

        fDt_Lote(22) = lblHora_23.Text
        fCusto(22) = lblTotal_Hora_23.Text

        fDt_Lote(23) = lblHora_24.Text
        fCusto(23) = lblTotal_Hora_24.Text

        chtCurvaHora.Series("A1").Points.DataBindXY(fDt_Lote, fCusto)
        chtCurvaHora.Series("A1").BorderWidth = 1
        chtCurvaHora.Series("A1").BorderColor = Drawing.ColorTranslator.FromHtml("#36D1DC")
        chtCurvaHora.Series("A1").Color = Drawing.ColorTranslator.FromHtml("#8C36D1DC")
    End Sub

    Protected Sub AbreFiltro()

        If divFiltro.Visible = False Then
            divFiltro.Visible = True
        Else
            divFiltro.Visible = False
        End If

    End Sub

    Protected Sub btnConfirmarFiltro_Click(sender As Object, e As EventArgs)
        AbreFiltro()
    End Sub

    Protected Sub btnFiltro_Click(sender As Object, e As EventArgs)
        AbreFiltro()
    End Sub

    Protected Sub btnFecharFiltro_Click(sender As Object, e As EventArgs)
        AbreFiltro()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
