Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web.UI.DataVisualization.Charting
'Imports iTextSharp.text
'Imports iTextSharp.text.html.simpleparser
'Imports iTextSharp.text.pdf

Public Class Dashboar_Telefonia
    Inherits System.Web.UI.Page

    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vDataSet As New Data.DataSet
    Dim vDataView As Data.DataView = Nothing
    Dim vDataTable As New System.Data.DataTable
    Private dtCDC As New DataTable()
    Dim listaCDC As New SortedDictionary(Of String, String)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            'Pega a largura da tela
            Dim tamTela As String = Request.QueryString("larguraTela")

            If tamTela <> Nothing Then
                If Convert.ToInt32(tamTela) >= 600 Then
                    chtCurvaGasto.Width = 380
                    chtCurvaAtivo.Width = 380
                    chtPark.Width = 380
                    chtFH_FS.Width = 380
                Else
                    chtCurvaGasto.Width = 350
                    chtCurvaAtivo.Width = 350
                    chtPark.Width = 350
                    chtFH_FS.Width = 350
                End If
            End If

            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                                IIf(Session("KPI") = "Telefonia_Movel", "Telefonia Móvel", "Telefonia Fixa"),
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

        If lstCentro_Custo.SelectedValue = Nothing Or lstCentro_Custo.SelectedValue = "Todos" Then
            lstCentro_Custo.Items.Clear()
            If pFiltro = Nothing Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Centro_Custo' ", "Filtro ASC", Data.DataViewRowState.OriginalRows)
                oConfig.CarregaListDataView(lstCentro_Custo, vDataView)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Cd_Centro_Custo ASC", Data.DataViewRowState.OriginalRows)
                vDataTable = vDataView.ToTable(True, "Filtro", "Cd_Centro_Custo")
                oConfig.FiltroCDC(lstCentro_Custo, vDataTable, Nothing)
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
            chtCurvaGasto.Series("A1").BorderWidth = 1
            chtCurvaGasto.Series("A1").BorderColor = Drawing.ColorTranslator.FromHtml("#36D1DC")
            chtCurvaGasto.Series("A1").Color = Drawing.ColorTranslator.FromHtml("#8C36D1DC")

            ''---- monta labels para o grafico
            'Dim x As Integer = 0
            'Dim lote As String = ""
            'For x = 0 To fDt_Lote.Length - 1
            '    lote = lote & fDt_Lote(x) & "!"
            'Next
            'lote = lote.Substring(0, lote.Length - 1)

            ''---- monta dados para o grafico
            'Dim y As Integer = 0
            'Dim valor As String = ""
            'For y = 0 To fCusto.Length - 1
            '    valor = valor & fCusto(y) & "!"
            'Next
            'valor = valor.Substring(0, valor.Length - 1)

            ''---- carrega o grafico
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "keyBarra01", "GraficoBarra01('" & lote & "','" & valor.ToString().Replace(",", ".") & "','" & "canvasCusto" & "');", True)

            trGraficoCusto.Visible = True
        Else
            trGraficoCusto.Visible = False
            chtCurvaGasto.Series("A1").Points.Clear()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "keyBarra01", "GraficoBarra01('" & "" & "','" & "" & "','" & "" & "');", True)
        End If

        '-----detalhamento Usuario----------------------------------------------------------
        '***********************************************************************************
        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' AND Lote = '" & Session("Calendario") & "/01" & "'", "Total DESC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & pFiltro, "Total DESC", Data.DataViewRowState.OriginalRows)
        End If

        Session("DataSet") = vDataView
        dtgTopGastoMes.CurrentPageIndex = 0
        dtgTopGastoMes.DataSource = vDataView
        dtgTopGastoMes.DataBind()

        If vDataView.Count > 0 Then
            trGraficoDetalhamento.Visible = True
        Else
            trGraficoDetalhamento.Visible = False
        End If

        '----limita tamanho dos nomes para não estourar a grid
        LimitaTamanhoLabel(dtgTopGastoMes)

        '----realiza soma dos valores
        Dim v_Valida As System.Double = 0
        For i = 0 To vDataView.Count - 1
            v_Valida = v_Valida + vDataView.Item(i).Row(19)
        Next i
        txtTotal.Text = Format(v_Valida, "R$ ##########,###########0")

        '-----inventario-------------------------------------------------------------------
        '**********************************************************************************
        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' ", "Lote, Nr_Ativo ASC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento' " & pFiltro, "Lote, Nr_Ativo ASC", Data.DataViewRowState.OriginalRows)
        End If

        i = 0
        Cont = 0
        ReDim Dt_Lote(1000)
        ReDim Qtd(1000)

        If vDataView.Count > 0 Then
            For i = 0 To vDataView.Count - 1
                If vDataView.Item(i).Item(3) = Dt_Lote(Cont) Then
                    If Ativo <> vDataView.Item(i).Item(4) Then
                        Qtd(Cont) = Qtd(Cont) + 1
                        Ativo = vDataView.Item(i).Item(4)
                    End If
                Else
                    If i > 0 Then Cont = Cont + 1
                    Dt_Lote(Cont) = vDataView.Item(i).Item(3)
                    Qtd(Cont) = Qtd(Cont) + 1
                    Ativo = vDataView.Item(i).Item(4)
                End If
            Next

            ReDim fDt_Lote(Cont)
            ReDim fQtd(Cont)
            For i = 0 To Cont
                fDt_Lote(i) = Dt_Lote(i)
                fQtd(i) = Qtd(i)
            Next i
            chtCurvaAtivo.Series("A1").Points.DataBindXY(fDt_Lote, fQtd)
            chtCurvaAtivo.Series("A1").BorderWidth = 1
            chtCurvaAtivo.Series("A1").BorderColor = Drawing.ColorTranslator.FromHtml("#ec008c")
            chtCurvaAtivo.Series("A1").Color = Drawing.ColorTranslator.FromHtml("#8Cec008c")

            trGraficoInventario.Visible = True
        Else
            trGraficoInventario.Visible = False
            chtCurvaAtivo.Series("A1").Points.Clear()
        End If

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
        ReDim Qtd(1000)

        If vDataView.Count > 0 Then
            For i = 0 To vDataView.Count - 1
                If vDataView.Item(i).Item(6) = Tipo_Ativo(Cont) Then
                    If Ativo <> vDataView.Item(i).Item(4) Then
                        Qtd(Cont) = Qtd(Cont) + 1
                        Ativo = vDataView.Item(i).Item(4)
                    End If
                Else
                    If i > 0 Then Cont = Cont + 1
                    Tipo_Ativo(Cont) = vDataView.Item(i).Item(6)
                    Qtd(Cont) = Qtd(Cont) + 1
                    Ativo = vDataView.Item(i).Item(4)
                End If
            Next

            ReDim fTipo_Ativo(Cont)
            ReDim fQtd(Cont)
            For i = 0 To Cont
                fTipo_Ativo(i) = Tipo_Ativo(i)
                fQtd(i) = Qtd(i)
            Next i
            chtPark.Series("A1").Points.DataBindXY(fTipo_Ativo, fQtd)
            chtPark.Series(0).BorderWidth = 1
            chtPark.Series(0).BorderColor = Drawing.ColorTranslator.FromHtml("#ad5389")
            chtPark.Series(0).Color = Drawing.ColorTranslator.FromHtml("#8Cad5389")

            trGraficoInventarioTipo.Visible = True
        Else
            trGraficoInventarioTipo.Visible = False
            chtPark.Series("A1").Points.Clear()
        End If

        '-----Custo por Tipo de Horario----------------------------------------------------
        '**********************************************************************************
        If pFiltro = Nothing Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento ' AND Lote = '" & Session("Calendario") & "/01" & "'", "Final_Semana, Fora_Horario, Dentro_Horario  ASC", Data.DataViewRowState.OriginalRows)
        Else
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Detalhamento ' AND Lote = '" & Session("Calendario") & "/01" & "'" & pFiltro, "Final_Semana, Fora_Horario, Dentro_Horario ASC", Data.DataViewRowState.OriginalRows)
        End If

        i = 0
        Cont = 0
        Dim vDescricao(2) As System.String
        vDescricao(0) = "Final de Semana"
        vDescricao(1) = "Fora do Horário"
        vDescricao(2) = "Dentro do Horário"
        ReDim fCusto(2)
        fCusto(0) = Format(0, "R$##########,###########0")
        fCusto(1) = Format(0, "R$##########,###########0")
        fCusto(2) = Format(0, "R$##########,###########0")

        If vDataView.Count > 0 Then
            For i = 0 To vDataView.Count - 1
                '----fs
                If vDataView.Item(i).Item(16) > 0 Then
                    fCusto(0) = fCusto(0) + Format(vDataView.Item(i).Item(16), "R$##########,###########0")
                End If
                '----fr
                If vDataView.Item(i).Item(17) > 0 Then
                    fCusto(1) = fCusto(1) + Format(vDataView.Item(i).Item(17), "R$##########,###########0")
                End If
                '----dh
                If vDataView.Item(i).Item(18) > 0 Then
                    fCusto(2) = fCusto(2) + Format(vDataView.Item(i).Item(18), "R$##########,###########0")
                End If
            Next i
            chtFH_FS.Series(0).Points.DataBindXY(vDescricao, fCusto)
            chtFH_FS.Series(0).BorderWidth = 1
            chtFH_FS.Series(0).BorderColor = Drawing.ColorTranslator.FromHtml("#f67d70")
            chtFH_FS.Series(0).Color = Drawing.ColorTranslator.FromHtml("#f67d70")

            trGraficoCustoHora.Visible = True
        Else
            trGraficoCustoHora.Visible = False
            chtFH_FS.Series(0).Points.Clear()
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
        txtValorTotalRetornoMarcacaoMes.Text = 0
        txtUsuarioVisitaramNaoConcluiramAno.Text = 0
        txtValorTotalRetornoMarcacaoAno.Text = 0

        If vDataView.Count > 0 Then
            Cont = 0
            For i = 0 To vDataView.Count - 1
                If vDataView.Item(i).Item(33) >= 1 Then
                    Cont = Cont + 1
                End If
            Next
            txtUsuarioVisitaramNaoConcluiramMes.Text = Cont

            Cont = 0
            For i = 0 To vDataView.Count - 1
                Cont = Cont + vDataView.Item(i).Item(34)
            Next
            txtValorTotalRetornoMarcacaoMes.Text = Format(Cont, "R$##########,###########0")

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

                Cont = 0
                For i = 0 To vDataView.Count - 1
                    Cont = Cont + vDataView.Item(i).Item(34)
                Next
                txtValorTotalRetornoMarcacaoAno.Text = Format(Cont, "R$##########,###########0")
            End If
        End If

        divFiltro.Visible = False
        'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_filtro.png"
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

    Protected Sub dtgTopGastoMes_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgTopGastoMes.PageIndexChanged
        dtgTopGastoMes.CurrentPageIndex = e.NewPageIndex
        dtgTopGastoMes.DataSource = Session("DataSet")
        dtgTopGastoMes.DataBind()

        '----limita tamanho dos nomes para não estourar a grid
        LimitaTamanhoLabel(dtgTopGastoMes)
    End Sub

    Protected Sub dtgTopGastoMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgTopGastoMes.SelectedIndexChanged

        divContaOnline.Visible = True
        divFiltro.Visible = True
        'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_close.png"
        CarregaContaOnline("", "Detalhamento da Conta do Colaborador" & dtgTopGastoMes.SelectedItem.Cells(1).Text, dtgTopGastoMes.SelectedItem.Cells(7).Text, dtgTopGastoMes.SelectedItem.Cells(6).Text, hfvFiltro.Value)

    End Sub

    Protected Sub btForaHorarioMes_Click(sender As Object, e As EventArgs) Handles btForaHorarioMes.Click

        divFiltro.Visible = True
        divCockpit.Visible = True
        'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_close.png"
        CarregaCockpitDetalhe("Trafego de Dados Fora do Horario", hfvFiltro.Value)

    End Sub

    Protected Sub btFinalSemanaMes_Click(sender As Object, e As EventArgs) Handles btFinalSemanaMes.Click

        divFiltro.Visible = True
        divCockpit.Visible = True
        'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_close.png"
        CarregaCockpitDetalhe("Trafego de Dados de Final de Semana", hfvFiltro.Value)

    End Sub

    Protected Sub btNaoConcluiramMes_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNaoConcluiramMes.Click

        divFiltro.Visible = True
        divCockpit.Visible = True
        'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_close.png"
        CarregaCockpitDetalhe("Colaborador com Conta em Aberto", hfvFiltro.Value)

    End Sub

    Protected Sub btNaoConcluiramAno_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNaoConcluiramAno.Click

        divFiltro.Visible = True
        divCockpit.Visible = True
        'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_close.png"
        CarregaCockpitDetalhe("Colaborador com Cota em Aberto ((FY) | Ano Fiscal)", hfvFiltro.Value)

    End Sub

    Protected Sub btRetornoMarcacaoMes_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btRetornoMarcacaoMes.Click

        divFiltro.Visible = True
        divCockpit.Visible = True
        'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_close.png"
        CarregaCockpitDetalhe("Valor Apontado", hfvFiltro.Value)

    End Sub

    Protected Sub btRetornoMarcacaoAno_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btRetornoMarcacaoAno.Click

        divFiltro.Visible = True
        divCockpit.Visible = True
        'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_close.png"
        CarregaCockpitDetalhe("Valor Apontado ((FY) | Ano Fiscal)", hfvFiltro.Value)

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

        Dim vCalendario As String = "bt_Calendario_" & Right(Session("Calendario"), 2)
        Dim vCalendario_Select As String = "bt_Calendario_Select_" & Right(Session("Calendario"), 2)

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
        dtgTopGastoMes.CurrentPageIndex = 0
        dtgTopGastoMes.DataSource = vDataView
        dtgTopGastoMes.DataBind()

        '----limita tamanho dos nomes para não estourar a grid
        LimitaTamanhoLabel(dtgTopGastoMes)

        Dim v_Valida As System.Int32 = 0
        For i = 0 To vDataView.Count - 1
            v_Valida = v_Valida + vDataView.Item(i).Row(15)
        Next i
        txtTotal.Text = Format(v_Valida, "R$ ##########,###########0")
    End Sub

    Protected Sub AbreFiltro()

        If divFiltro.Visible = False Then
            divFiltro.Visible = True
            'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_close.png"
        Else
            divFiltro.Visible = False
            divContaOnline.Visible = False
            divCockpit.Visible = False
            'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_filtro.png"
        End If

    End Sub

    Protected Sub LimitaTamanhoLabelDtgGrupo(ByVal dtg As DataGrid)

        '----limita tamanho dos nomes para não estourar a grid
        Dim label As Label
        Dim vCont As Integer = 0
        'Pega a largura da tela
        Dim tamTela As String = Request.QueryString("larguraTela")

        For vCont = 0 To dtg.Items.Count - 1
            label = dtg.Items(vCont).Cells(1).Controls(1)
            If label.Text.Length > 15 And tamTela < 600 Then
                label.Text = label.Text.Substring(0, 15) & "..."
            End If
        Next vCont

    End Sub

    Protected Sub LimitaTamanhoLabel(ByVal dtg As DataGrid)

        '----limita tamanho dos nomes para não estourar a grid
        Dim label As Label
        Dim vCont As Integer = 0
        'Pega a largura da tela
        Dim tamTela As String = Request.QueryString("larguraTela")

        For vCont = 0 To dtg.Items.Count - 1
            label = dtg.Items(vCont).Cells(1).Controls(1)
            If label.Text.Length > 15 And tamTela < 600 Then
                label.Text = label.Text.Substring(0, 15) & "..."
            End If
        Next vCont

    End Sub

    Protected Sub btnConfirmarFiltro_Click(sender As Object, e As EventArgs)
        AbreFiltro()
    End Sub

    Protected Sub CarregaContaOnline(ByVal bilhete As String, ByVal descricao As String, ByVal filtro As String, ByVal total As String, ByVal bi As String)
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----monta detalhamento da conta
        Dim vDataSetConta As New System.Data.DataSet
        Dim vDataViewConta As System.Data.DataView
        Dim vFiltroConta As System.String = Nothing
        Session("DataViewConta") = ""

        If Request("viewBilhete") = "Final_Semana" Then
            vFiltroConta = "Final_Semana=1"
        End If

        If Request("viewBilhete") = "Fora_Horario" Then
            vFiltroConta = "Fora_Horario=1"
        End If

        vDataViewConta = New Data.DataView(WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Detalhamento_Bilhete", filtro, Session("Id_Usuario"), Nothing, Nothing).Tables(0),
                                                            vFiltroConta,
                                                            "Data, Destino ASC",
                                                            Data.DataViewRowState.OriginalRows)

        Session("DataViewConta") = vDataViewConta

        dtgDetalhamento.DataSource = Session("DataViewConta")
        dtgDetalhamento.DataBind()

        '-----trata se usuario tem permissao de detalhar a conta
        If dtgDetalhamento.Items(0).Cells(0).Text = "&nbsp;" Then
            pnlDetalhe.Visible = True
            dtgDetalhamento.Visible = False
            Exit Sub
        End If

        '-----busca numeros mais discados
        If Request("viewBilhete") = "Area" Or Request("viewBilhete") = "Tipo" Or Request("viewBilhete") = Nothing Then
            dtgMaisDiscado.DataSource = WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Mais_Discado", filtro, Session("Id_Usuario"), Nothing, Nothing)
            dtgMaisDiscado.DataBind()
        End If

        If Request("viewBilhete") = "Final_Semana" Then
            dtgMaisDiscado.DataSource = WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Mais_Discado_Final_Semana", filtro, Session("Id_Usuario"), Nothing, Nothing)
            dtgMaisDiscado.DataBind()
        End If

        If Request("viewBilhete") = "Fora_Horario" Then
            dtgMaisDiscado.DataSource = WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Mais_Discado_Fora_Horario", filtro, Session("Id_Usuario"), Nothing, Nothing)
            dtgMaisDiscado.DataBind()
        End If

        ''-----busca volume
        If Request("viewBilhete") = "Area" Or Request("viewBilhete") = "Tipo" Or Request("viewBilhete") = Nothing Then
            dtgVolume.DataSource = WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Volume", filtro, Session("Id_Usuario"), Nothing, Nothing)
            dtgVolume.DataBind()
        End If

        If Request("viewBilhete") = "Final_Semana" Then
            dtgVolume.DataSource = WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Volume_Final_Semana", filtro, Session("Id_Usuario"), Nothing, Nothing)
            dtgVolume.DataBind()
        End If

        If Request("viewBilhete") = "Fora_Horario" Then
            dtgVolume.DataSource = WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Volume_Fora_Horario", filtro, Session("Id_Usuario"), Nothing, Nothing)
            dtgVolume.DataBind()
        End If

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
        If lblDescricao.Text = "Trafego de Dados de Final de Semana" Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Final_Semana > '0' AND Lote = '" & Session("Calendario") & "'" & Replace(BI, "*-*", "'"), "Final_Semana DESC", Data.DataViewRowState.OriginalRows)
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = True
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            'For i = 0 To dtgGrupo.Items.Count - 1
            '    Mes = Mes + dtgGrupo.Items(i).Cells(4).Text
            'Next
            txtTotalCockpit.Text = Format(Mes, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Trafego de Dados Fora do Horario" Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Fora_Horario > '0' AND Lote = '" & Session("Calendario") & "'" & Replace(BI, "*-*", "'"), "Fora_Horario DESC", Data.DataViewRowState.OriginalRows)
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(3).Visible = True
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            For i = 0 To dtgGrupo.Items.Count - 1
                Mes = Mes + dtgGrupo.Items(i).Cells(3).Text
            Next
            txtTotalCockpit.Text = Format(Mes, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Colaborador com Conta em Aberto" Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(BI, "*-*", "'"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.Columns(7).Visible = True
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            txtTotalCockpit.Text = Format(0, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Colaborador com Cota em Aberto ((FY) | Ano Fiscal)" Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' " & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(BI, "*-*", "'"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.Columns(7).Visible = True
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            txtTotalCockpit.Text = Format(0, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Valor Apontado" Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND ValorTotalRetornoMarcacaoMes > 0 " & Replace(BI, "*-*", "'"), "Nm_Consumidor, ValorTotalRetornoMarcacaoMes ASC", Data.DataViewRowState.OriginalRows)
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.Columns(7).Visible = True
            dtgGrupo.Columns(8).Visible = True
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            For i = 0 To dtgGrupo.Items.Count - 1
                Mes = Mes + dtgGrupo.Items(i).Cells(8).Text
            Next
            txtTotalCockpit.Text = Format(Mes, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Valor Apontado ((FY) | Ano Fiscal)" Then
            vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' " & " AND ValorTotalRetornoMarcacaoMes > 0 " & Replace(BI, "*-*", "'"), "Nm_Consumidor, ValorTotalRetornoMarcacaoMes ASC", Data.DataViewRowState.OriginalRows)
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.Columns(7).Visible = True
            dtgGrupo.Columns(8).Visible = True
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            For i = 0 To dtgGrupo.Items.Count - 1
                Mes = Mes + dtgGrupo.Items(i).Cells(8).Text
            Next
            txtTotalCockpit.Text = Format(Mes, "R$##########,###########0")
        End If
    End Sub

    Protected Sub dtgGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgGrupo.SelectedIndexChanged
        If lblDescricao.Text = "Trafego de Dados de Final de Semana" Then

            divCockpit.Visible = False
            divContaOnline.Visible = True
            divFiltro.Visible = True
            'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_close.png"
            CarregaContaOnline("Final_Semana", dtgGrupo.SelectedItem.Cells(1).Text & " - " & dtgGrupo.SelectedItem.Cells(2).Text, dtgGrupo.SelectedItem.Cells(5).Text, dtgGrupo.SelectedItem.Cells(4).Text, "")

            'Response.Redirect("../Dashboard_Telefonia/Conta_OnLine_Detalhe.aspx?viewBilhete=Final_Semana&Descricao=" & dtgGrupo.SelectedItem.Cells(1).Text & " - " & dtgGrupo.SelectedItem.Cells(2).Text & " &Filtro=" & dtgGrupo.SelectedItem.Cells(5).Text & " &Total=" & dtgGrupo.SelectedItem.Cells(4).Text)
        End If

        If lblDescricao.Text = "Trafego de Dados Fora do Horario" Then

            divCockpit.Visible = False
            divContaOnline.Visible = True
            divFiltro.Visible = True
            'btnFiltro.ImageUrl = "~/Img_Sistema/Botao/ic_close.png"
            CarregaContaOnline("Fora_Horario", dtgGrupo.SelectedItem.Cells(1).Text & " - " & dtgGrupo.SelectedItem.Cells(2).Text, dtgGrupo.SelectedItem.Cells(3).Text, dtgGrupo.SelectedItem.Cells(3).Text, "")

            'Response.Redirect("../Dashboard_Telefonia/Conta_OnLine_Detalhe.aspx?viewBilhete=Fora_Horario&Descricao=" & dtgGrupo.SelectedItem.Cells(1).Text & " - " & dtgGrupo.SelectedItem.Cells(2).Text & " &Filtro=" & dtgGrupo.SelectedItem.Cells(3).Text & " &Total=" & dtgGrupo.SelectedItem.Cells(3).Text)
        End If
    End Sub

    Protected Sub BtOk_Click(sender As Object, e As ImageClickEventArgs) Handles BtOk.Click
        Dim vDataSet As New Data.DataSet
        Dim vDataView As Data.DataView = Nothing
        Dim Mes As System.Double = 0
        Dim Ano As System.Double = 0

        vDataSet = Session("CockPit")

        '--------------------------------
        If lblDescricao.Text = "Trafego de Dados de Final de Semana" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Final_Semana > '0' AND Lote = '" & Session("Calendario") & "'" & Replace(hdfBI.Value, "*-*", "'"), "Final_Semana DESC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Nm_Consumidor Like '%" & txtOrdenacao.Text & "%' and Final_Semana > '0' AND Lote = '" & Session("Calendario") & "'" & Replace(hdfBI.Value, "*-*", "'"), "Nm_Consumidor ASC", Data.DataViewRowState.OriginalRows)
            End If

            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(2).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = True
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            For i = 0 To dtgGrupo.Items.Count - 1
                Mes = Mes + dtgGrupo.Items(i).Cells(4).Text
            Next
            txtTotalCockpit.Text = Format(Mes, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Trafego de Dados Fora do Horario" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Fora_Horario > '0' AND Lote = '" & Session("Calendario") & "'" & Replace(hdfBI.Value, "*-*", "'"), "Fora_Horario DESC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Nm_Consumidor Like '%" & txtOrdenacao.Text & "%' and Fora_Horario > '0' AND Lote = '" & Session("Calendario") & "'" & Replace(hdfBI.Value, "*-*", "'"), "Nm_Consumidor ASC", Data.DataViewRowState.OriginalRows)
            End If

            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(2).Visible = False
            dtgGrupo.Columns(3).Visible = True
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            For i = 0 To dtgGrupo.Items.Count - 1
                Mes = Mes + dtgGrupo.Items(i).Cells(3).Text
            Next
            txtTotalCockpit.Text = Format(Mes, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Colaborador com Conta em Aberto" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(hdfBI.Value, "*-*", "'"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Nm_Consumidor Like '%" & txtOrdenacao.Text & "%' AND Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(hdfBI.Value, "*-*", "'"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            End If
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.Columns(7).Visible = True
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            txtTotalCockpit.Text = Format(0, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Colaborador com Cota em Aberto ((FY) | Ano Fiscal)" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' " & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(hdfBI.Value, "*-*", "'"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Nm_Consumidor Like '%" & txtOrdenacao.Text & "%' AND Grupo = 'Apontamento' " & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(hdfBI.Value, "*-*", "'"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            End If
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.Columns(7).Visible = True
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            txtTotalCockpit.Text = Format(0, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Valor Apontado" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND ValorTotalRetornoMarcacaoMes > 0 " & Replace(hdfBI.Value, "*-*", "'"), "Nm_Consumidor, ValorTotalRetornoMarcacaoMes ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Nm_Consumidor Like '%" & txtOrdenacao.Text & "%' AND Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND ValorTotalRetornoMarcacaoMes > 0 " & Replace(hdfBI.Value, "*-*", "'"), "Nm_Consumidor, ValorTotalRetornoMarcacaoMes ASC", Data.DataViewRowState.OriginalRows)
            End If
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.Columns(7).Visible = True
            dtgGrupo.Columns(8).Visible = True
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            For i = 0 To dtgGrupo.Items.Count - 1
                Mes = Mes + dtgGrupo.Items(i).Cells(8).Text
            Next
            txtTotalCockpit.Text = Format(Mes, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Valor Apontado ((FY) | Ano Fiscal)" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' " & " AND ValorTotalRetornoMarcacaoMes > 0 " & Replace(hdfBI.Value, "*-*", "'"), "Nm_Consumidor, ValorTotalRetornoMarcacaoMes ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Nm_Consumidor Like '%" & txtOrdenacao.Text & "%' AND Grupo = 'Apontamento' " & " AND ValorTotalRetornoMarcacaoMes > 0 " & Replace(hdfBI.Value, "*-*", "'"), "Nm_Consumidor, ValorTotalRetornoMarcacaoMes ASC", Data.DataViewRowState.OriginalRows)
            End If
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.Columns(7).Visible = True
            dtgGrupo.Columns(8).Visible = True
            dtgGrupo.DataBind()

            LimitaTamanhoLabelDtgGrupo(dtgGrupo)

            For i = 0 To dtgGrupo.Items.Count - 1
                Mes = Mes + dtgGrupo.Items(i).Cells(8).Text
            Next
            txtTotalCockpit.Text = Format(Mes, "R$##########,###########0")
        End If
    End Sub

    Protected Sub btImprimir_Click(sender As Object, e As EventArgs) Handles btImprimir.Click

        Response.Redirect("../Dashboard_Telefonia/Conta_OnLine_Detalhe.aspx" & "?Descricao=Detalhamento da Conta do Colaborador" & dtgTopGastoMes.SelectedItem.Cells(1).Text + "&Filtro=" & dtgTopGastoMes.SelectedItem.Cells(7).Text & "&larguraTela=" & Request.QueryString("larguraTela") & "&Total=" & dtgTopGastoMes.SelectedItem.Cells(6).Text & "&BI=" & hfvFiltro.Value & "")

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

    Protected Sub btPesquisarCdc_Click(sender As Object, e As ImageClickEventArgs)

        If txtPesquisaCdc.Text <> "" Then
            If txtPesquisaCdc.Text.ToLower() = "todos" Then
                lstFilial.SelectedValue = Nothing
                lstDepartamento.SelectedValue = Nothing
                lstSetor.SelectedValue = Nothing
                lstSecao.SelectedValue = Nothing
                lblErroCdc.Visible = False
                Call Monta_Tela(Nothing)
            Else
                For i = 0 To lstCentro_Custo.Items.Count - 1
                    If txtPesquisaCdc.Text.ToUpper() = lstCentro_Custo.Items(i).Text.ToUpper() Then
                        lblErroCdc.Visible = False
                        lstCentro_Custo.SelectedValue = lstCentro_Custo.Items(i).Value
                        hfvFiltro.Value = " AND Cd_Centro_Custo = *-*" & lstCentro_Custo.Items(i).Text & "*-*"
                        Call Monta_Tela(" AND Cd_Centro_Custo = '" & lstCentro_Custo.Items(i).Text & "'")
                        txtPesquisaCdc.Text = ""
                        Return
                    End If
                Next
                lblErroCdc.Visible = True
            End If
        End If

    End Sub

End Class
