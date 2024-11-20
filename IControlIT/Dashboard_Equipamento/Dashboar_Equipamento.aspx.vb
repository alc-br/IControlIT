Imports System.Web.UI.DataVisualization.Charting

Public Class Dashboar_Equipamento
    Inherits System.Web.UI.Page
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vDataSet As New Data.DataSet
    Dim vDataView As Data.DataView = Nothing
    Dim vDataTableFiltro As New System.Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                                "Equipamento", _
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

            Call Monta_Tela(Nothing, "Nm_Filial")
        End If
    End Sub

    Public Sub Monta_Tela(pFiltro As String, pHierarquia As String)
        Dim i As System.Int32
        Dim Cont As System.Int32
        Dim Dt_Lote(0) As System.String
        Dim fDt_Lote(0) As System.String
        Dim Qtd(0) As System.Double
        Dim fQtd(0) As System.Double
        Dim Custo(0) As System.Double
        Dim fCusto(0) As System.Double

        Dim vTipo_Area As System.String = ""
        Dim vCusto As System.Double = 0
        Dim vQTD As System.Double = 0

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
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
                vDataTableFiltro = vDataView.ToTable(True, "Id_Filial", "Nm_Filial")
                oConfig.FiltroCDC(lstFilial, vDataTableFiltro, "Filial")
            End If
        End If

        If lstDepartamento.SelectedValue = Nothing Then
            lstDepartamento.Items.Clear()
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Departamento' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
                oConfig.CarregaListDataView(lstDepartamento, vDataView)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
                vDataTableFiltro = vDataView.ToTable(True, "Id_Departamento", "Nm_Departamento")
                oConfig.FiltroCDC(lstDepartamento, vDataTableFiltro, "Departamento")
            End If
        End If

        If lstSetor.SelectedValue = Nothing Then
            lstSetor.Items.Clear()
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Setor' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
                oConfig.CarregaListDataView(lstSetor, vDataView)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
                vDataTableFiltro = vDataView.ToTable(True, "Id_Setor", "Nm_Setor")
                oConfig.FiltroCDC(lstSetor, vDataTableFiltro, "Setor")
            End If
        End If

        If lstSecao.SelectedValue = Nothing Then
            lstSecao.Items.Clear()
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Secao' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
                oConfig.CarregaListDataView(lstSecao, vDataView)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Filtro ASC", Data.DataViewRowState.OriginalRows)
                vDataTableFiltro = vDataView.ToTable(True, "Id_Secao", "Nm_Secao")
                oConfig.FiltroCDC(lstSecao, vDataTableFiltro, "Secao")
            End If
        End If

        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Centro_Custo' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
            oConfig.CarregaListDataView(lstCentro_Custo, vDataView)
        Else
            lstCentro_Custo.Items.Clear()
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Cd_Centro_Custo ASC", Data.DataViewRowState.OriginalRows)
            vDataTableFiltro = vDataView.ToTable(True, "Filtro", "Cd_Centro_Custo")
            oConfig.FiltroCDC(lstCentro_Custo, vDataTableFiltro, Nothing)
        End If

        '-----cria dataset
        Dim vLinha As Data.DataRow
        Dim vDataSetNow As New System.Data.DataSet
        '-----cria datatable
        Dim vDataTable As Data.DataTable = New Data.DataTable("vDataTableInclui")
        '-----cria colunas
        Dim vArea As Data.DataColumn = New Data.DataColumn("Area", GetType(System.String))
        Dim vQuantidade As Data.DataColumn = New Data.DataColumn("Quantidade", GetType(System.Double))
        Dim vValor As Data.DataColumn = New Data.DataColumn("Valor", GetType(System.Double))
        Dim v_QTD As System.Int32 = 0
        Dim v_Valor As System.Int32 = 0
        '-----cria dataset
        vDataSetNow.DataSetName = "vDataSet"
        '-----adiciona colunas na tabela
        vDataTable.Columns.Add(vArea)
        vDataTable.Columns.Add(vQuantidade)
        vDataTable.Columns.Add(vValor)

        vDataSetNow.Tables.Add(vDataTable)

        '-----detalhamento Area-----------------------------------------------------------------------------------------------------------------
        '***************************************************************************************************************************************
        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento ' AND Lote = '" & Session("Calendario") & "/01" & "'", pHierarquia & " ASC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento ' AND Lote = '" & Session("Calendario") & "/01" & "'" & pFiltro, pHierarquia & " ASC", Data.DataViewRowState.OriginalRows)
        End If

        If vDataView.Count > 0 Then
            For i = 0 To vDataView.Count - 1
                If vTipo_Area = vDataView.Item(i).Item(pHierarquia) Then
                    vCusto = vCusto + vDataView.Item(i).Item("Total")
                    vQTD = vQTD + 1
                Else
                    If Not vTipo_Area = "" Then
                        vLinha = vDataTable.NewRow
                        vLinha("Area") = vTipo_Area
                        vLinha("Quantidade") = vQTD
                        vLinha("Valor") = vCusto
                        vDataTable.Rows.Add(vLinha)
                    End If
                    vCusto = vDataView.Item(i).Item("Total")
                    vQTD = 1
                    vTipo_Area = vDataView.Item(i).Item(pHierarquia)
                End If
            Next i
            '-----grava ultimo registro no data set
            If vDataView.Count = i Then
                vLinha = vDataTable.NewRow
                vLinha("Area") = vTipo_Area
                vLinha("Quantidade") = vQTD
                vLinha("Valor") = vCusto
                vDataTable.Rows.Add(vLinha)
            End If
            vDataSetNow.AcceptChanges()
            dtgGrupo.DataSource = vDataSetNow
            dtgGrupo.DataBind()

            '----realiza soma dos valores
            v_QTD = 0
            v_Valor = 0
            For i = 0 To dtgGrupo.Items.Count - 1
                v_QTD = v_QTD + dtgGrupo.Items(i).Cells(1).Text
                v_Valor = v_Valor + dtgGrupo.Items(i).Cells(2).Text
            Next i
            txtQuantidade.Text = v_QTD
            txtTotal.Text = Format(v_Valor, "R$########,#0")

            trGrupo.Visible = True
        Else
            trGrupo.Visible = False
        End If

        '-----detalhamento tipo de ativo--------------------------------------------------------------------------------------------------------
        '***************************************************************************************************************************************
        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento ' AND Lote = '" & Session("Calendario") & "/01" & "'", "Nm_Ativo_Tipo ASC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento ' AND Lote = '" & Session("Calendario") & "/01" & "'" & pFiltro, "Nm_Ativo_Tipo ASC", Data.DataViewRowState.OriginalRows)
        End If

        '-----cria dataset
        vDataSetNow.Clear()
        vTipo_Area = ""
        vLinha = Nothing

        If vDataView.Count > 0 Then
            For i = 0 To vDataView.Count - 1
                If vTipo_Area = vDataView.Item(i).Item("Nm_Ativo_Tipo") Then
                    vCusto = vCusto + vDataView.Item(i).Item("Total")
                    vQTD = vQTD + 1
                Else
                    If Not vTipo_Area = "" Then
                        vLinha = vDataTable.NewRow
                        vLinha("Area") = vTipo_Area
                        vLinha("Quantidade") = vQTD
                        vLinha("Valor") = vCusto
                        vDataTable.Rows.Add(vLinha)
                    End If
                    vCusto = vDataView.Item(i).Item("Total")
                    vQTD = 1
                    vTipo_Area = vDataView.Item(i).Item("Nm_Ativo_Tipo")
                End If
            Next i
            '-----grava ultimo registro no data set
            If vDataView.Count = i Then
                vLinha = vDataTable.NewRow
                vLinha("Area") = vTipo_Area
                vLinha("Quantidade") = vQTD
                vLinha("Valor") = vCusto
                vDataTable.Rows.Add(vLinha)
            End If
            vDataSetNow.AcceptChanges()

            dtgTopGastoMes.DataSource = vDataSetNow
            dtgTopGastoMes.DataBind()

            trTopGastos.Visible = True
        Else
            trTopGastos.Visible = False
        End If

        '-----detalhamento Usuario--------------------------------------------------------------------------------------------------------------
        '***************************************************************************************************************************************

        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento ' AND Lote = '" & Session("Calendario") & "/01" & "'", "Total DESC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento ' AND Lote = '" & Session("Calendario") & "/01" & "'" & pFiltro, "Total DESC", Data.DataViewRowState.OriginalRows)
        End If

        Session("DataSet") = vDataView
        dtgDetalhamentoUsuario.CurrentPageIndex = 0
        dtgDetalhamentoUsuario.DataSource = vDataView
        dtgDetalhamentoUsuario.DataBind()

        If vDataView.Count > 0 Then
            trDetalhamentoUsuario.Visible = True
        Else
            trDetalhamentoUsuario.Visible = False
        End If

        '-----Utilizacao do Portal----------------------------------------------------
        '*****************************************************************************
        '-----Mes
        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'", "Lote  ASC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & pFiltro, "Lote ASC", Data.DataViewRowState.OriginalRows)
        End If


        txtUsuarioVisitaramNaoConcluiramMes.Text = 0
        txtUsuarioVisitaramNaoConcluiramAno.Text = 0

        If vDataView.Count > 0 Then
            Cont = 0
            For i = 0 To vDataView.Count - 1
                If vDataView.Item(i).Item(33) >= 1 Then
                    Cont = Cont + 1
                End If
            Next
            txtUsuarioVisitaramNaoConcluiramMes.Text = Cont

            '-----Ano
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' ", "Lote  ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' " & pFiltro, "Lote ASC", Data.DataViewRowState.OriginalRows)
            End If

            If vDataView.Count > 0 Then
                Cont = 0
                For i = 0 To vDataView.Count - 1
                    If vDataView.Item(i).Item(33) >= 1 Then
                        Cont = Cont + 1
                    End If
                Next
                txtUsuarioVisitaramNaoConcluiramAno.Text = Cont
            End If
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
            chtCurvaGasto.Series("A1").BackGradientStyle = GradientStyle.TopBottom
            chtCurvaGasto.Series("A1").Color = Drawing.ColorTranslator.FromHtml("#f953c6")
            chtCurvaGasto.Series("A1").BackSecondaryColor = Drawing.ColorTranslator.FromHtml("#b91d73")

            trCurvaGasto.Visible = True
        Else
            trCurvaGasto.Visible = False
            chtCurvaGasto.Series("A1").Points.Clear()
        End If

        divFiltro.Visible = False
    End Sub

    Protected Sub lstFilial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFilial.SelectedIndexChanged
        lstCentro_Custo.SelectedValue = Nothing
        If lstFilial.SelectedValue = "Todos" Then
            lstDepartamento.SelectedValue = Nothing
            lstSetor.SelectedValue = Nothing
            lstSecao.SelectedValue = Nothing
            Call Monta_Tela(Nothing, "Nm_Filial") '---volta para filial default
        Else
            hfvFiltro.Value = " AND Nm_Filial = *-*" & lstFilial.SelectedItem.Text & "*-*"
            Call Monta_Tela(" AND Nm_Filial = '" & lstFilial.SelectedItem.Text & "'", "Nm_Filial")
        End If
    End Sub

    Protected Sub lstSecao_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSecao.SelectedIndexChanged
        lstCentro_Custo.SelectedValue = Nothing
        If lstSecao.SelectedValue = "Todos" Then
            lstFilial.SelectedValue = Nothing
            lstDepartamento.SelectedValue = Nothing
            lstSetor.SelectedValue = Nothing
            Call Monta_Tela(Nothing, "Nm_Filial") '---volta para filial default
        Else
            hfvFiltro.Value = " AND Nm_Secao = *-*" & lstSecao.SelectedItem.Text & "*-*"
            Call Monta_Tela(" AND Nm_Secao = '" & lstSecao.SelectedItem.Text & "'", "Nm_Secao")
        End If
    End Sub

    Protected Sub lstSetor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSetor.SelectedIndexChanged
        lstCentro_Custo.SelectedValue = Nothing
        If lstSetor.SelectedValue = "Todos" Then
            lstFilial.SelectedValue = Nothing
            lstDepartamento.SelectedValue = Nothing
            lstSecao.SelectedValue = Nothing
            Call Monta_Tela(Nothing, "Nm_Filial") '---volta para filial default
        Else
            hfvFiltro.Value = " AND Nm_Setor = *-*" & lstSetor.SelectedItem.Text & "*-*"
            Call Monta_Tela(" AND Nm_Setor = '" & lstSetor.SelectedItem.Text & "'", "Nm_Setor")
        End If
    End Sub

    Protected Sub lstDepartamento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDepartamento.SelectedIndexChanged
        lstCentro_Custo.SelectedValue = Nothing
        If lstDepartamento.SelectedValue = "Todos" Then
            lstFilial.SelectedValue = Nothing
            lstSetor.SelectedValue = Nothing
            lstSecao.SelectedValue = Nothing
            Call Monta_Tela(Nothing, "Nm_Filial") '---volta para filial default
        Else
            hfvFiltro.Value = " AND Nm_Departamento = *-*" & lstDepartamento.SelectedItem.Text & "*-*"
            Call Monta_Tela(" AND Nm_Departamento = '" & lstDepartamento.SelectedItem.Text & "'", "Nm_Departamento")
        End If
    End Sub

    Protected Sub lstCentro_Custo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCentro_Custo.SelectedIndexChanged
        If lstCentro_Custo.SelectedValue = "Todos" Then
            lstFilial.SelectedValue = Nothing
            lstDepartamento.SelectedValue = Nothing
            lstSetor.SelectedValue = Nothing
            lstSecao.SelectedValue = Nothing
            Call Monta_Tela(Nothing, "Nm_Filial") '---volta para filial default
        Else
            hfvFiltro.Value = " AND Cd_Centro_Custo = *-*" & lstCentro_Custo.SelectedItem.Text & "*-*"
            Call Monta_Tela(" AND Cd_Centro_Custo = '" & lstCentro_Custo.SelectedItem.Text & "'", "Cd_Centro_Custo")
        End If
    End Sub

    Protected Sub dtgDetalhamentoUsuario_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgDetalhamentoUsuario.PageIndexChanged
        dtgDetalhamentoUsuario.CurrentPageIndex = e.NewPageIndex
        dtgDetalhamentoUsuario.DataSource = Session("DataSet")
        dtgDetalhamentoUsuario.DataBind()
    End Sub

    Protected Sub btNaoConcluiramMes_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNaoConcluiramMes.Click

        divFiltro.Visible = True
        divCockpit.Visible = True
        CarregaCockpitDetalhe("Ativo sem validação", hfvFiltro.Value)

        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Dashboard_Equipamento/CockPit_Detalhe.aspx" & "?Descricao=Ativo sem validação" & "&Filtro=" & hfvFiltro.Value & "','','resizable=yes, menubar=yes, scrollbars=yes, height=768px, width=1024px, top=10, left=10');", True)
    End Sub

    Protected Sub btNaoConcluiramAno_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNaoConcluiramAno.Click

        divFiltro.Visible = True
        divCockpit.Visible = True
        CarregaCockpitDetalhe("Ativo sem validação ((FY) | Ano Fiscal)", hfvFiltro.Value)

        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Dashboard_Equipamento/CockPit_Detalhe.aspx" & "?Descricao=Ativo sem validação ((FY) | Ano Fiscal)" & "&Filtro=" & hfvFiltro.Value & "','','resizable=yes, menubar=yes, scrollbars=yes, height=768px, width=1024px, top=10, left=10');", True)
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
        '-----limpa tela
        dtgDetalhamentoUsuario.DataSource = Nothing
        dtgDetalhamentoUsuario.DataBind()
        dtgGrupo.DataSource = Nothing
        dtgGrupo.DataBind()
        dtgTopGastoMes.DataSource = Nothing
        dtgTopGastoMes.DataBind()
        txtTotal.Text = 0
        txtQuantidade.Text = 0
        txtUsuarioVisitaramNaoConcluiramAno.Text = 0
        txtUsuarioVisitaramNaoConcluiramMes.Text = 0

        '-----monta tela
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

        Call Monta_Tela(Nothing, "Nm_Filial")
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
        dtgDetalhamentoUsuario.CurrentPageIndex = 0
        dtgDetalhamentoUsuario.DataSource = vDataView
        dtgDetalhamentoUsuario.DataBind()

        Dim v_Valida As System.Int32 = 0
        For i = 0 To vDataView.Count - 1
            v_Valida = v_Valida + vDataView.Item(i).Row(15)
        Next i
        txtTotal.Text = Format(v_Valida, "########,#0")
    End Sub

    Protected Sub CarregaCockpitDetalhe(ByVal descricao As String, ByVal BI As String)

        Dim vDataSet As New Data.DataSet
        Dim vDataView As Data.DataView = Nothing
        Dim i As System.Int32 = 0
        Dim Mes As System.Double = 0
        Dim Ano As System.Double = 0

        hdfBI.Value = BI

        '-----gera dados 
        '---------------------------------------------------------------------------------------
        vDataSet = Session("CockPit")
        lblDescricao.Text = Trim(descricao)

        '--------------------------------
        If lblDescricao.Text = "Ativo sem validação" Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", "'"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            dtgGrupoDetalhe.DataSource = vDataView
            dtgGrupoDetalhe.Columns(0).Visible = False
            dtgGrupoDetalhe.Columns(3).Visible = False
            dtgGrupoDetalhe.Columns(4).Visible = False
            dtgGrupoDetalhe.Columns(7).Visible = True
            dtgGrupoDetalhe.DataBind()

            txtTotal.Text = Format(0, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Ativo sem validação ((FY) | Ano Fiscal)" Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' " & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", """"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            dtgGrupoDetalhe.DataSource = vDataView
            dtgGrupoDetalhe.Columns(0).Visible = False
            dtgGrupoDetalhe.Columns(3).Visible = False
            dtgGrupoDetalhe.Columns(4).Visible = False
            dtgGrupoDetalhe.Columns(7).Visible = True
            dtgGrupoDetalhe.DataBind()

            txtTotal.Text = Format(0, "R$##########,###########0")
        End If
    End Sub

    Protected Sub BtOk_Click(sender As Object, e As ImageClickEventArgs) Handles BtOk.Click
        Dim vDataSet As New Data.DataSet
        Dim vDataView As Data.DataView = Nothing
        Dim Mes As System.Double = 0
        Dim Ano As System.Double = 0

        vDataSet = Session("CockPit")

        '--------------------------------
        If lblDescricao.Text = "Ativo sem validação" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", """"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Nm_Consumidor Like '%" & txtOrdenacao.Text & "%' AND Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", """"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            End If
            dtgGrupoDetalhe.DataSource = vDataView
            dtgGrupoDetalhe.Columns(0).Visible = False
            dtgGrupoDetalhe.Columns(3).Visible = False
            dtgGrupoDetalhe.Columns(4).Visible = False
            dtgGrupoDetalhe.Columns(7).Visible = True
            dtgGrupoDetalhe.DataBind()

            txtTotal.Text = Format(0, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Ativo sem validação ((FY) | Ano Fiscal)" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' " & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", """"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Nm_Consumidor Like '%" & txtOrdenacao.Text & "%' AND Grupo = 'Apontamento' " & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", """"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            End If
            dtgGrupoDetalhe.DataSource = vDataView
            dtgGrupoDetalhe.Columns(0).Visible = False
            dtgGrupoDetalhe.Columns(3).Visible = False
            dtgGrupoDetalhe.Columns(4).Visible = False
            dtgGrupoDetalhe.Columns(7).Visible = True
            dtgGrupoDetalhe.DataBind()

            txtTotal.Text = Format(0, "R$##########,###########0")
        End If
    End Sub

    Protected Sub btnFiltro_Click(sender As Object, e As ImageClickEventArgs)
        AbreFiltro()
    End Sub

    Protected Sub AbreFiltro()

        If divFiltro.Visible = False Then
            divFiltro.Visible = True
        Else
            divFiltro.Visible = False
            divCockpit.Visible = False
        End If

    End Sub

    Protected Sub btnConfirmarFiltro_Click(sender As Object, e As EventArgs)
        AbreFiltro()
    End Sub

    Protected Sub btnFiltro_Click1(sender As Object, e As EventArgs)
        AbreFiltro()
    End Sub

    Protected Sub btnFecharFiltro_Click(sender As Object, e As EventArgs)
        AbreFiltro()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
