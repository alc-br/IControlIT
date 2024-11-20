Imports System.Web.UI.DataVisualization.Charting

Public Class Dashboar_Link
    Inherits System.Web.UI.Page
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vDataSet As New Data.DataSet
    Dim vDataView As Data.DataView
    Dim vDataTable As New System.Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            'Pega a largura da tela
            Dim tamTela As String = Request.QueryString("larguraTela")

            If tamTela <> Nothing Then
                If Convert.ToInt32(tamTela) >= 600 Then
                    chtCurvaGasto.Width = 730
                    chtPark.Width = 730
                Else
                    chtCurvaGasto.Width = 450
                    chtPark.Width = 450
                End If
            End If

            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                                " Link de Dados", _
                                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Session("BI") = Nothing
            Session("DataSet") = Nothing
            Call Master.Localizar(Nothing, Nothing)

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
            v_dataSet = WS_Modulo.Deskboard(Session("Conn_Banco"), "sp_Ano_Fiscal", Nothing, Nothing, Nothing)
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

            Call Monta_Tela(Nothing)

        End If
    End Sub

    Public Sub Monta_Tela(pFiltro As String)
        Dim i As System.Int32
        Dim Cont As System.Int32
        Dim Ativo As System.String = ""

        Dim Dt_Lote(0) As System.String
        Dim fDt_Lote(0) As System.String

        Dim Qtd(0) As System.Double
        Dim fQtd(0) As System.Double

        Dim Custo(0) As System.Double
        Dim fCusto(0) As System.Double

        Dim Tipo_Ativo(0) As System.String
        Dim fTipo_Ativo(0) As System.String

        Dim ContGraficos As Int32 = 0

        vDataView = Nothing
        vDataSet = Session("CockPit")

        hdfFiltro.Value = pFiltro

        '-----Filtro-----------------------------------------------------------------------
        '**********************************************************************************
        If lstFilial.SelectedValue = Nothing Then
            lstFilial.Items.Clear()
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Filial' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
                oConfig.CarregaListDataView(lstFilial, vDataView)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
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
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
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
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
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
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
                vDataTable = vDataView.ToTable(True, "Id_Secao", "Nm_Secao")
                oConfig.FiltroCDC(lstSecao, vDataTable, "Secao")
            End If
        End If

        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Centro_Custo' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
            oConfig.CarregaListDataView(lstCentro_Custo, vDataView)
        Else
            lstCentro_Custo.Items.Clear()
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Cd_Centro_Custo ASC", Data.DataViewRowState.OriginalRows)
            vDataTable = vDataView.ToTable(True, "Filtro", "Cd_Centro_Custo")
            oConfig.FiltroCDC(lstCentro_Custo, vDataTable, Nothing)
        End If

        '-----grafico Custo----------------------------------------------------------
        '****************************************************************************
        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' ", "Lote ASC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Lote ASC", Data.DataViewRowState.OriginalRows)
        End If

        i = 0
        Cont = 0
        ReDim Dt_Lote(1000)
        ReDim Custo(1000)

        If vDataView.Count > 0 Then
            For i = 0 To vDataView.Count - 1
                If vDataView.Item(i).Item(3) = Dt_Lote(Cont) Then
                    Custo(Cont) = Custo(Cont) + vDataView.Item(i).Item(19)
                Else
                    If i > 0 Then Cont = Cont + 1
                    Dt_Lote(Cont) = vDataView.Item(i).Item(3)
                    Custo(Cont) = Custo(Cont) + vDataView.Item(i).Item(19)
                End If
            Next

            ReDim fDt_Lote(Cont)
            ReDim fCusto(Cont)
            For i = 0 To Cont
                fDt_Lote(i) = Dt_Lote(i)
                fCusto(i) = Custo(i)
            Next i
            chtCurvaGasto.Series("A1").Points.DataBindXY(fDt_Lote, fCusto)
            chtCurvaGasto.Series("A1").BorderWidth = 1
            chtCurvaGasto.Series("A1").BorderColor = Drawing.ColorTranslator.FromHtml("#36D1DC")
            chtCurvaGasto.Series("A1").Color = Drawing.ColorTranslator.FromHtml("#8C36D1DC")

            trGraficoCusto.Visible = True
        Else
            ContGraficos = ContGraficos + 1
            trGraficoCusto.Visible = False
            chtCurvaGasto.Series("A1").Points.Clear()
        End If

        '-----detalhamento area----------------------------------------------------------
        '***********************************************************************************
        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' AND Lote = '" & Session("Calendario") & "/01" & "'", "Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & pFiltro, "Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
        End If

        Session("DataSet") = vDataView
        dtgGrupo.CurrentPageIndex = 0
        dtgGrupo.DataSource = vDataView
        dtgGrupo.DataBind()

        If vDataView.Count > 0 Then
            trGraficoDetalhamento.Visible = True
        Else
            ContGraficos = ContGraficos + 1
            trGraficoDetalhamento.Visible = False
        End If

        '----realiza soma dos valores
        Dim v_Valida As System.Int32 = 0
        For i = 0 To vDataView.Count - 1
            v_Valida = v_Valida + vDataView.Item(i).Row(15)      'dtgGrupo.Items(i).Cells(4).Text
        Next i
        txtTotal.Text = Format(v_Valida, "R$########,#0")

        '-----inventario por Tipo----------------------------------------------------------
        '**********************************************************************************
        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento ' AND Lote = '" & Session("Calendario") & "/01" & "'", "Nm_Ativo_Tipo ASC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento ' AND Lote = '" & Session("Calendario") & "/01" & "'" & pFiltro, "Nm_Ativo_Tipo ASC", Data.DataViewRowState.OriginalRows)
        End If

        i = 0
        Cont = 0
        ReDim Tipo_Ativo(1000)
        ReDim Custo(1000)

        If vDataView.Count > 0 Then
            For i = 0 To vDataView.Count - 1
                If vDataView.Item(i).Item(6) = Tipo_Ativo(Cont) Then
                    Custo(Cont) = Custo(Cont) + vDataView.Item(i).Item(19)
                Else
                    If i > 0 Then Cont = Cont + 1
                    Tipo_Ativo(Cont) = vDataView.Item(i).Item(6)
                    Custo(Cont) = Custo(Cont) + vDataView.Item(i).Item(19)
                End If
            Next

            ReDim fTipo_Ativo(Cont)
            ReDim fCusto(Cont)
            For i = 0 To Cont
                fTipo_Ativo(i) = Tipo_Ativo(i)
                fCusto(i) = Custo(i)
            Next i
            chtPark.Series("A1").Points.DataBindXY(fTipo_Ativo, fCusto)
            chtPark.Series("A1").BorderWidth = 1
            chtPark.Series("A1").BorderColor = Drawing.ColorTranslator.FromHtml("#ec008c")
            chtPark.Series("A1").Color = Drawing.ColorTranslator.FromHtml("#8Cec008c")

            trGraficoInventarioTipo.Visible = True
        Else
            ContGraficos = ContGraficos + 1
            trGraficoInventarioTipo.Visible = False
            chtPark.Series("A1").Points.Clear()
        End If

        If ContGraficos = 3 Then
            trMsg.Visible = True
        End If

        If trGraficoCusto.Visible = True And trGraficoInventarioTipo.Visible = False Then
            trGraficoCusto.Attributes.Add("class", "col-md-12")
        ElseIf trGraficoCusto.Visible = False And trGraficoInventarioTipo.Visible = True Then
            trGraficoInventarioTipo.Attributes.Add("class", "col-md-12")
        Else
            trGraficoCusto.Attributes.Add("class", "col-md-6")
            trGraficoInventarioTipo.Attributes.Add("class", "col-md-6")
        End If

        divFiltro.Visible = False

    End Sub

    Protected Sub lstFilial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFilial.SelectedIndexChanged
        lstCentro_Custo.SelectedValue = Nothing
        If lstFilial.SelectedValue = "Todos" Then
            lstDepartamento.SelectedValue = Nothing
            lstSetor.SelectedValue = Nothing
            lstSecao.SelectedValue = Nothing
            Call Monta_Tela(Nothing)
        Else
            hfvFiltro.Value = " AND Nm_Filial = *-*" & lstFilial.SelectedItem.Text & "*-*"
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
            hfvFiltro.Value = " AND Nm_Secao = *-*" & lstSecao.SelectedItem.Text & "*-*"
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
            hfvFiltro.Value = " AND Nm_Setor = *-*" & lstSetor.SelectedItem.Text & "*-*"
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
            hfvFiltro.Value = " AND Nm_Departamento = *-*" & lstDepartamento.SelectedItem.Text & "*-*"
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
            hfvFiltro.Value = " AND Cd_Centro_Custo = *-*" & lstCentro_Custo.SelectedItem.Text & "*-*"
            Call Monta_Tela(" AND Cd_Centro_Custo = '" & lstCentro_Custo.SelectedItem.Text & "'")
        End If
    End Sub

    Protected Sub dtgGrupo_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgGrupo.PageIndexChanged
        dtgGrupo.CurrentPageIndex = e.NewPageIndex
        dtgGrupo.DataSource = Session("DataSet")
        dtgGrupo.DataBind()
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

        Session("CockPit") = WS_Modulo.Deskboard(Session("Conn_Banco"), "cockpit", Session("KPI"), Session("Id_Usuario"), mes)
        Session("Calendario") = mes

        If Session("Calendario") = mes Then botao.CssClass = "labelCalendarioSelected"

        Call Monta_Tela(Nothing)
    End Sub

    Protected Sub btLocalizar_Click(sender As Object, e As ImageClickEventArgs) Handles btLocalizar.Click
        vDataSet = Session("CockPit")

        If txtLocalizar.Text = "" Then
            If hdfFiltro.Value = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' AND Lote = '" & Session("Calendario") & "/01" & "'", "Total DESC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & hdfFiltro.Value, "Total DESC", Data.DataViewRowState.OriginalRows)
            End If
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND Nm_Consumidor LIKE '%" & txtLocalizar.Text & "%'" & hdfFiltro.Value, "Total DESC", Data.DataViewRowState.OriginalRows)
        End If

        Session("DataSet") = vDataView
        dtgGrupo.CurrentPageIndex = 0
        dtgGrupo.DataSource = vDataView
        dtgGrupo.DataBind()

        Dim v_Valida As System.Int32 = 0
        For i = 0 To vDataView.Count - 1
            v_Valida = v_Valida + vDataView.Item(i).Row(15)
        Next i
        txtTotal.Text = Format(v_Valida, "########,#0")
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
