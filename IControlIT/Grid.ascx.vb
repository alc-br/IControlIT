
Public Class Grid
    Inherits System.Web.UI.UserControl

    Private vintPagina As System.Int32

    Public Property Descricao() As String
        Get
            Return hfdDescricao.Value
        End Get
        Set(ByVal Value As String)
            hfdDescricao.Value = Value
        End Set
    End Property

    Public Property Campo() As String
        Get
            Return hfdCampo.Value
        End Get
        Set(ByVal Value As String)
            hfdCampo.Value = Value
        End Set
    End Property

    Public Sub montaPaginacao()
        cboMostraLinha.Items.Clear()
        '-----monta combo de mostra linha
        Dim Item As ListItem
        Dim i As System.Int32
        For i = 1 To dtgMatrix.PageCount
            Item = New ListItem
            Item.Text = i
            Item.Value = i - 1
            cboMostraLinha.Items.Add(Item)
        Next
    End Sub

    Public Sub Dados(ByVal pValue As System.Data.DataSet)
        Session("GridDataView") = Nothing
        Session("GridDataSet") = pValue
        Session("GridDataSetOriginal") = pValue
        dtgMatrix.CurrentPageIndex = 0
        dtgMatrix.DataSource = Session("GridDataSet")
        dtgMatrix.DataBind()

        lblPag.Text = dtgMatrix.PageCount
        lblPagCont.Text = dtgMatrix.CurrentPageIndex + 1
        '-----monta paginacao
        montaPaginacao()

        If dtgMatrix.CurrentPageIndex = 0 Then
            btAnterior.Enabled = False
        Else
            btAnterior.Enabled = True
        End If

        If dtgMatrix.CurrentPageIndex = dtgMatrix.PageCount - 1 Then
            btProximo.Enabled = False
        Else
            btProximo.Enabled = True
        End If
    End Sub

    Public Sub Colunas(ByVal pHeader() As System.String,
                        ByVal pField() As System.String,
                        ByVal pSort() As System.String)
        Dim i As System.Int32
        Dim Coluna As BoundColumn
        Dim vdataSet As New System.Data.DataSet
        '-----limpa campo
        txtOrdenacao.Text = ""
        '-----prepara combo de ordenacao
        Dim Item As ListItem
        cboColuna.Items.Clear()
        cboColuna_Calculo.Items.Clear()
        Item = New ListItem
        Item.Text = ""
        cboColuna.Items.Add(Item)
        cboColuna_Calculo.Items.Add(Item)
        '-----cria dataset para armazenar dados drag drop
        Dim vTempDataSet As New System.Data.DataSet
        vTempDataSet.DataSetName = "vTempDataSet"
        '-----cria datatable
        Dim vDataTable As Data.DataTable = New Data.DataTable("vTempDataTable")
        '-----cria colunas
        Dim vHeader As Data.DataColumn = New Data.DataColumn("Header", GetType(System.String))
        Dim vField As Data.DataColumn = New Data.DataColumn("Field", GetType(System.String))
        Dim vSort As Data.DataColumn = New Data.DataColumn("Sort", GetType(System.String))
        '-----adiciona colunas na tabela
        vDataTable.Columns.Add(vHeader)
        vDataTable.Columns.Add(vField)
        vDataTable.Columns.Add(vSort)
        '-----adiciona tabela no dataset
        vTempDataSet.Tables.Add(vDataTable)

        '-----carrega dataset
        Dim vLinha As Data.DataRow

        For i = 0 To UBound(pHeader)
            '-----grava dataset 
            vLinha = vDataTable.NewRow
            vLinha("Header") = pHeader(i)
            vLinha("Field") = pField(i)
            If Not pSort Is Nothing Then vLinha("Sort") = pSort(i)
            vDataTable.Rows.Add(vLinha)
            '-----monta coluna
            Coluna = New BoundColumn
            Coluna.DataField = pField(i)
            Coluna.HeaderText = pHeader(i)

            'TODO: achar outros relatórios com campo currency ou outra forma de formatar essas colunas com R$
            If pHeader(i).StartsWith("20") Then
                Coluna.DataFormatString = "{0:c}"
            End If

            If Not pSort Is Nothing Then Coluna.SortExpression = pSort(i)
            dtgMatrix.Columns.Add(Coluna)
            '-----monta combo de 
            Item = New ListItem
            Item.Text = pHeader(i)
            Item.Value = pField(i)
            cboColuna.Items.Add(Item)
            cboColuna_Calculo.Items.Add(Item)
        Next
        vTempDataSet.AcceptChanges()

        '-----cria session
        Session("vTempDataSet") = vTempDataSet
    End Sub

    Protected Sub Monta_Coluna()
        If Session("vTempDataSet") Is Nothing Then Exit Sub

        '-----carrega dataset
        Dim Coluna As BoundColumn
        Dim vTempDataSet As New System.Data.DataSet
        vTempDataSet = Session("vTempDataSet")

        Dim vLinha As Data.DataRow

        For Each vLinha In vTempDataSet.Tables(0).Rows
            Coluna = New BoundColumn
            Coluna.DataField = vLinha("Field")
            Coluna.HeaderText = vLinha("Header")

            'TODO: achar outros relatórios com campo currency ou outra forma de formatar essas colunas com R$
            If vLinha("Header").StartsWith("20") Then
                Coluna.DataFormatString = "{0:c}"
            End If

            If Not vLinha("Sort") Is System.DBNull.Value Then Coluna.SortExpression = vLinha("Sort")
            dtgMatrix.Columns.Add(Coluna)
        Next
    End Sub

    Protected Sub Atualiza_Grid()
        Call Monta_Coluna()
        dtgMatrix.DataSource = IIf(Session("GridDataView") Is Nothing, Session("GridDataSet"), Session("GridDataView"))
        dtgMatrix.DataBind()

        lblPag.Text = dtgMatrix.PageCount
        lblPagCont.Text = dtgMatrix.CurrentPageIndex + 1
        '-----monta paginacao
        montaPaginacao()

        If dtgMatrix.CurrentPageIndex = 0 Then
            btAnterior.Enabled = False
        Else
            btAnterior.Enabled = True
        End If

        If dtgMatrix.CurrentPageIndex = dtgMatrix.PageCount - 1 Then
            btProximo.Enabled = False
        Else
            btProximo.Enabled = True
        End If
    End Sub

    Protected Sub cboMostraLinha_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMostraLinha.SelectedIndexChanged
        dtgMatrix.CurrentPageIndex = cboMostraLinha.SelectedValue
        vintPagina = dtgMatrix.CurrentPageIndex
        Call Atualiza_Grid()
    End Sub

    Protected Sub BtOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtOk.Click
        Dim vDataSet As New Data.DataSet
        Dim vDataView As Data.DataView = Nothing

        pnlMsg.Visible = False
        '-----filtra
        If Not cboColuna.SelectedValue = Nothing And Not Trim(txtOrdenacao.Text) = Nothing Then
            Call Monta_Coluna()
            vDataSet = Session("GridDataSetOriginal") 'Session("GridDataSet")
            If vDataSet.Tables(0).Columns(cboColuna.SelectedValue).DataType.ToString = "System.String" Then
                vDataView = New Data.DataView(vDataSet.Tables(0),
                                              IIf(Trim(txtOrdenacao.Text) = "", Nothing, cboColuna.SelectedValue & " Like '%" & txtOrdenacao.Text & "%'"),
                                              cboColuna.SelectedValue & IIf(cboOrdernacao.SelectedValue = 1, " ASC", " DESC"),
                                              Data.DataViewRowState.OriginalRows)
            Else
                If IsNumeric(txtOrdenacao.Text) = True Or Trim(txtOrdenacao.Text) = "" Then
                    vDataView = New Data.DataView(vDataSet.Tables(0),
                                                  IIf(Trim(txtOrdenacao.Text) = "", Nothing, cboColuna.SelectedValue & " = " & txtOrdenacao.Text & " "),
                                                  cboColuna.SelectedValue & IIf(cboOrdernacao.SelectedValue = 1, " ASC", " DESC"),
                                                  Data.DataViewRowState.OriginalRows)
                End If
            End If
            If Not vDataView Is Nothing Then
                dtgMatrix.CurrentPageIndex = 0
                dtgMatrix.DataSource = vDataView
                dtgMatrix.DataBind()
                Session("GridDataView") = vDataView
            End If
            dtgMatrix.CurrentPageIndex = 0
            lblPag.Text = dtgMatrix.PageCount
            lblPagCont.Text = dtgMatrix.CurrentPageIndex + 1
            '-----monta paginacao
            montaPaginacao()
            Exit Sub
        End If

        '-----ordernar
        If Not cboColuna.SelectedValue = Nothing Then
            Call Monta_Coluna()
            vDataSet = Session("GridDataSetOriginal")
            vDataView = New Data.DataView(vDataSet.Tables(0),
                                          Nothing,
                                          cboColuna.SelectedValue & IIf(cboOrdernacao.SelectedValue = 1, " ASC", " DESC"),
                                          Data.DataViewRowState.OriginalRows)
            If Not vDataView Is Nothing Then
                dtgMatrix.CurrentPageIndex = 0
                dtgMatrix.DataSource = vDataView
                dtgMatrix.DataBind()
                Session("GridDataView") = vDataView
            End If
            dtgMatrix.CurrentPageIndex = 0
            lblPag.Text = dtgMatrix.PageCount
            lblPagCont.Text = dtgMatrix.CurrentPageIndex + 1
            '-----monta paginacao
            montaPaginacao()
        End If
    End Sub

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        pnlMsg.Visible = False
    End Sub

    Protected Sub BtExportar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
        "window.open('../Exportacao/Exporta.aspx?Descricao=" & Descricao & "&Campo=" & Campo &
        "','','resizable=yes, menubar=yes, scrollbars=no," &
        "height=768px, width=1024px, top=10, left=10'" &
        ")", True)
    End Sub

    Protected Sub BtOrdenar_Click(sender As Object, e As EventArgs)
        pnlMsg.Visible = True
    End Sub

    Protected Sub BtRefresh_Click(sender As Object, e As EventArgs)
        Dim vDataSet As New Data.DataSet
        Dim vDataView As Data.DataView = Nothing

        pnlMsg.Visible = False
        cboColuna.SelectedValue = Nothing

        Call Monta_Coluna()
        vDataSet = Session("GridDataSetOriginal")

        If vDataSet.Tables.Count = 0 Then Exit Sub

        vDataView = New Data.DataView(vDataSet.Tables(0),
                                      Nothing,
                                      Nothing,
                                      Data.DataViewRowState.OriginalRows)
        If Not vDataView Is Nothing Then
            dtgMatrix.CurrentPageIndex = 0
            dtgMatrix.DataSource = vDataView
            dtgMatrix.DataBind()
            Session("GridDataView") = vDataView
        End If
        dtgMatrix.CurrentPageIndex = 0
        lblPag.Text = dtgMatrix.PageCount
        lblPagCont.Text = dtgMatrix.CurrentPageIndex + 1
        '-----monta paginacao
        montaPaginacao()
    End Sub

    Protected Sub btSomar_Click(sender As Object, e As EventArgs)
        '-----soma valores
        Dim vDataSet As New Data.DataSet
        Dim Linha As Data.DataRow
        Dim vSoma As System.Double = 0
        vDataSet = Nothing
        vDataSet = Session("GridDataSetOriginal") 'Session("GridDataSet")

        If cboColuna_Calculo.SelectedValue = Nothing Then Exit Sub
        If vDataSet.Tables(0).Columns(cboColuna_Calculo.SelectedValue).DataType.ToString = "System.String" Then Exit Sub

        For Each Linha In vDataSet.Tables(0).Rows
            vSoma = vSoma + IIf(Linha.Item(cboColuna_Calculo.SelectedValue).ToString = "", 0, Linha.Item(cboColuna_Calculo.SelectedValue))
        Next

        Call Atualiza_Grid()

        Dim vDataTable As New Data.DataTable
        vDataTable = Nothing
        vDataTable = vDataSet.Tables(0)

        Dim vRow As Data.DataRow
        vRow = Nothing

        vRow = vDataTable.NewRow
        vRow(cboColuna_Calculo.SelectedValue) = vSoma

        vDataTable.Rows.Add(vRow)

        dtgMatrix.DataSource = vDataTable
        dtgMatrix.DataBind()

        vDataTable.Rows.Remove(vRow)
    End Sub

    Protected Sub BtSubTotal_Click(sender As Object, e As EventArgs)
        If cboColuna_Calculo.SelectedValue = Nothing Then Exit Sub
        Session("GridDataView") = Nothing

        '-----cria dataset de agrupamento
        Dim vColuna_Agrupa As System.String = Nothing
        Dim vSoma() As System.Double
        Dim i As System.Int32
        Dim x As System.Int32
        Dim vNomeColuna() As System.String
        Dim vdataSet As New System.Data.DataSet
        Dim vColunaDataSet As New System.Data.DataSet
        Dim vDadosDataSet As New System.Data.DataSet
        Dim vDataView As Data.DataView = Nothing
        vColunaDataSet = Session("vTempDataSet")
        vDadosDataSet = Session("GridDataSetOriginal") 'Session("GridDataSet")

        '-----inicia agrupamento
        '-----cria dataset para armazenar dados drag drop
        Dim vTempDataSet As New System.Data.DataSet
        vTempDataSet.DataSetName = "vTempDataSet"
        '-----cria datatable
        Dim vDataTable As Data.DataTable = New Data.DataTable("vTempDataTable")
        ReDim vNomeColuna(vColunaDataSet.Tables(0).Rows.Count)
        '-----cria colunas
        For i = 0 To vColunaDataSet.Tables(0).Rows.Count - 1
            Dim vColuna As Data.DataColumn = New Data.DataColumn(vColunaDataSet.Tables(0).Rows(i).Item("Field"), GetType(System.String))
            vNomeColuna(i) = vColunaDataSet.Tables(0).Rows(i).Item("Field")
            vDataTable.Columns.Add(vColuna)
        Next
        '-----adiciona tabela no dataset
        vTempDataSet.Tables.Add(vDataTable)
        Dim vLinhaInsert As Data.DataRow
        ReDim vSoma(vColunaDataSet.Tables(0).Rows.Count - 1)

        '-----ordena dado para agrupamento
        vDataView = New Data.DataView(vDadosDataSet.Tables(0),
                                      Nothing,
                                      cboColuna_Calculo.SelectedValue & " ASC",
                                      Data.DataViewRowState.OriginalRows)

        '-----verifica se campo selecionado nao e numerico
        If Not vDataView.Table.Columns(cboColuna_Calculo.SelectedValue).DataType.ToString = "System.String" Then Exit Sub

        '-----muda de linha
        For i = 0 To vDataView.Table.Rows.Count - 1
            vLinhaInsert = vDataTable.NewRow
            vColuna_Agrupa = vDataView.Item(i).Item(cboColuna_Calculo.SelectedValue)

            '-----muda coluna
            For x = 0 To UBound(vNomeColuna) - 1
                If Not vDataView.Table.Columns(x).DataType.ToString = "System.String" Then
                    vSoma(x) = vSoma(x) + IIf(vDataView.Item(i).Item(x).ToString = "", 0, vDataView.Item(i).Item(x))
                End If
            Next

            '-----grava agrupamento
            If vDataView.Table.Rows.Count - 1 = i Or Not vColuna_Agrupa = vDataView.Item(IIf(i + 1 > vDataView.Table.Rows.Count - 1, i, i + 1)).Item(cboColuna_Calculo.SelectedValue) Then
                For x = 0 To UBound(vNomeColuna) - 1
                    If Not vDataView.Table.Columns(x).DataType.ToString = "System.String" Then
                        vLinhaInsert(vNomeColuna(x)) = vSoma(x)
                    Else
                        vLinhaInsert(vNomeColuna(x)) = IIf(cboColuna_Calculo.SelectedValue = vNomeColuna(x), vDataView.Item(i).Item(x), "SUM")
                    End If
                    vSoma(x) = 0
                Next
                vDataTable.Rows.Add(vLinhaInsert)
            End If
        Next

        vTempDataSet.AcceptChanges()

        '-----cria session
        If Not vTempDataSet Is Nothing Then
            Call Monta_Coluna()
            dtgMatrix.CurrentPageIndex = 0
            dtgMatrix.DataSource = vTempDataSet
            dtgMatrix.DataBind()
            Session("GridDataSet") = vTempDataSet
        End If
        dtgMatrix.CurrentPageIndex = 0
        lblPag.Text = dtgMatrix.PageCount
        lblPagCont.Text = dtgMatrix.CurrentPageIndex + 1
        '-----monta paginacao
        montaPaginacao()
    End Sub

    Protected Sub btPrimeiro_Click(sender As Object, e As EventArgs)
        dtgMatrix.CurrentPageIndex = 0
        vintPagina = dtgMatrix.CurrentPageIndex
        Call Atualiza_Grid()
    End Sub

    Protected Sub btAnterior_Click(sender As Object, e As EventArgs)
        If dtgMatrix.CurrentPageIndex > 0 Then
            dtgMatrix.CurrentPageIndex = dtgMatrix.CurrentPageIndex - 1
            vintPagina = dtgMatrix.CurrentPageIndex
            Call Atualiza_Grid()
        End If
    End Sub
    Protected Sub btProximo_Click(sender As Object, e As EventArgs)
        If dtgMatrix.CurrentPageIndex < dtgMatrix.PageCount - 1 Then
            dtgMatrix.CurrentPageIndex = dtgMatrix.CurrentPageIndex + 1
            vintPagina = dtgMatrix.CurrentPageIndex
            Call Atualiza_Grid()
        End If
    End Sub

    Protected Sub btUltimo_Click(sender As Object, e As EventArgs)
        dtgMatrix.CurrentPageIndex = dtgMatrix.PageCount - 1
        vintPagina = dtgMatrix.CurrentPageIndex
        Call Atualiza_Grid()
    End Sub
End Class
