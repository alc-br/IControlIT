Imports Microsoft.VisualBasic
Imports System.Data

Public Class cls_Config
    Public Function ValidaCampo(ByVal pCampo As String) As System.String
        Dim vRetorno As System.String = RTrim(LTrim(pCampo))
        vRetorno = IIf(Trim(vRetorno) = "", Nothing, vRetorno)
        Return vRetorno
    End Function
    Public Function ValidaCheckbox(ByVal pCampo As Boolean) As System.Int32
        Dim vRetorno As System.Int32
        vRetorno = IIf(pCampo, 1, 0)
        Return vRetorno
    End Function

    Public Function AgrupaDados(ByVal pList As WebControls.ListBox) As System.String
        '-----retorna dados agrupado em uma variavel string
        Dim vArray As System.String = Nothing

        If pList.Items.Count > 0 Then
            Dim i As System.Int32 = 0
            For i = 0 To pList.Items.Count - 1
                If Not pList.Items(i).Value = "" Then
                    vArray = vArray + pList.Items(i).Value & IIf(pList.Items.Count - 1 = i, "", ",")
                End If
            Next i
        End If

        Return vArray
    End Function

    Public Function AgrupaDadosAtivos(ByVal pDataSet As System.Data.DataSet) As System.String
        '-----retorna dados agrupado em uma variavel string
        Dim vArray As System.String = Nothing
        If Not pDataSet Is Nothing Then
            Dim i As System.Int32 = 0
            For i = 0 To pDataSet.Tables(0).Rows.Count - 1
                If Not pDataSet.Tables(0).Rows(i).Item(0) = "" Then
                    vArray = vArray + pDataSet.Tables(0).Rows(i).Item(0) & IIf(pDataSet.Tables(0).Rows.Count - 1 = i, "", ",")
                End If
            Next i
        End If
        Return vArray
    End Function

    Public Sub LimpaText(ByVal me_Form As ContentPlaceHolder)
        Dim vObjeto As TextBox
        Dim vControleContent As Control
        Dim X As System.Int32 = 0

        For Each vControleContent In me_Form.Controls
            If vControleContent.Controls.Count > 0 Then
                For X = 0 To vControleContent.Controls.Count - 1
                    If vControleContent.Controls.Item(X).ToString = "System.Web.UI.WebControls.TextBox" Then
                        vObjeto = vControleContent.Controls.Item(X)
                        vObjeto.Text = ""
                    End If
                Next
            Else
                If vControleContent.ToString = "System.Web.UI.WebControls.TextBox" Then
                    vObjeto = vControleContent
                    vObjeto.Text = ""
                End If
            End If
        Next
    End Sub

    Public Sub CarregaCombo(ByVal pCombo As WebControls.DropDownList, _
                            ByVal pDataSet As System.Data.DataSet)

        If pDataSet.Tables.Count = 0 Then Exit Sub

        Dim Linha As DataRow
        Dim Item As ListItem
        pCombo.Items.Clear()

        Item = New ListItem
        Item.Text = ""
        pCombo.Items.Add(Item)

        For Each Linha In pDataSet.Tables(0).Rows
            Item = New ListItem
            Item.Text = Linha.Item(1)
            Item.Value = Linha.Item(0)
            pCombo.Items.Add(Item)
        Next
    End Sub

    Public Sub CarregaList(ByVal pList As WebControls.ListBox, _
                            ByVal pDataSet As System.Data.DataSet)

        If pDataSet.Tables.Count = 0 Then Exit Sub

        Dim Linha As DataRow
        Dim Item As ListItem
        pList.Items.Clear()

        Item = New ListItem
        Item.Text = ""
        pList.Items.Add(Item)

        For Each Linha In pDataSet.Tables(0).Rows
            Item = New ListItem
            Item.Text = Linha.Item(1)
            Item.Value = Linha.Item(0)
            pList.Items.Add(Item)
        Next
    End Sub

    Public Sub CarregaListDataView(ByVal pList As WebControls.ListBox,
                                    ByVal pDataView As System.Data.DataView)

        If pDataView.Count = 0 Then Exit Sub

        If pDataView.Count = 1 Then
            pList.Enabled = False
        Else
            pList.Enabled = True
        End If

        Dim i As Int32 = 0
        Dim Item As ListItem

        pList.Items.Clear()
        Item = New ListItem
        Item.Text = "Todos"
        pList.Items.Add(Item)

        For i = 0 To pDataView.Count - 1
            If Not pDataView.Item(i).Item(2).ToString = "" Then
                Item = New ListItem
                Item.Text = pDataView.Item(i).Item(2)
                Item.Value = pDataView.Item(i).Item(1)
                pList.Items.Add(Item)
            End If
        Next
    End Sub

    Public Sub FiltroCDC(ByVal pList As WebControls.ListBox,
                        ByVal pDataTable As System.Data.DataTable,
                        ByVal pTipo_Dados As String)

        If pDataTable.Rows.Count = 0 Then Exit Sub

        Dim i As Int32 = 0
        Dim Item As ListItem

        pList.Items.Clear()
        Item = New ListItem
        Item.Text = "Todos"
        pList.Items.Add(Item)

        For i = 0 To pDataTable.Rows.Count - 1
            Item = New ListItem
            If pTipo_Dados = Nothing Then
                If pDataTable.Rows(i).Item(0) Is Nothing Then
                    Item.Text = pDataTable.Rows(i).Item(1)
                    Item.Value = pDataTable.Rows(i).Item(0)
                End If
            End If

            If pTipo_Dados = "Filial" Then
                If pDataTable.Rows(i).Item(0) Is Nothing Then
                    Item.Text = pDataTable.Rows(i).Item(1)
                    Item.Value = pDataTable.Rows(i).Item(0)
                End If
            End If

            If pTipo_Dados = "CDC" Then
                If pDataTable.Rows(i).Item(0) Is Nothing Then
                    Item.Text = pDataTable.Rows(i).Item(1)
                    Item.Value = pDataTable.Rows(i).Item(0)
                End If
            End If

            If pTipo_Dados = "Departamento" Then
                If pDataTable.Rows(i).Item(0) Is Nothing Then
                    Item.Text = pDataTable.Rows(i).Item(1)
                    Item.Value = pDataTable.Rows(i).Item(0)
                End If
            End If

            If pTipo_Dados = "Setor" Then
                If pDataTable.Rows(i).Item(0) Is Nothing Then
                    Item.Text = pDataTable.Rows(i).Item(1)
                    Item.Value = pDataTable.Rows(i).Item(0)
                End If
            End If

            If pTipo_Dados = "Secao" Then
                If pDataTable.Rows(i).Item(0) Is Nothing Then
                    Item.Text = pDataTable.Rows(i).Item(1)
                    Item.Value = pDataTable.Rows(i).Item(0)
                End If
            End If
            pList.Items.Add(Item)
        Next
    End Sub

    Public Function CarregaDragDrop(ByVal pDataSeteOrigem As System.Data.DataSet) As System.Data.DataSet
        '-----cria dataset para armazenar dados drag drop
        Dim vDtDragDrop As New System.Data.DataSet
        vDtDragDrop.DataSetName = "vDataSetDragDrop"
        '-----cria datatable
        Dim vDataTable As Data.DataTable = New Data.DataTable("vDataTableInclui")
        '-----cria colunas
        Dim vCodigo As Data.DataColumn = New Data.DataColumn("Codigo", GetType(System.String))
        Dim vDescricao As Data.DataColumn = New Data.DataColumn("Descricao", GetType(System.String))
        '-----adiciona colunas na tabela
        vDataTable.Columns.Add(vCodigo)
        vDataTable.Columns.Add(vDescricao)

        '-----adiciona tabela no dataset
        vDtDragDrop.Tables.Add(vDataTable)

        '-----carrega dataset
        Dim vLinha As Data.DataRow
        Dim vLDataSet As Data.DataRow

        For Each vLDataSet In pDataSeteOrigem.Tables(0).Rows
            vLinha = vDataTable.NewRow
            vLinha("Codigo") = vLDataSet.Item(0)
            vLinha("Descricao") = vLDataSet.Item(1)
            vDataTable.Rows.Add(vLinha)
        Next
        vDtDragDrop.AcceptChanges()
        '-----cria session
        Return vDtDragDrop
    End Function

    Public Sub CarregaCheckBoxList(ByVal pDropDownList As WebControls.CheckBoxList, _
                                    ByVal pDataSet As System.Data.DataSet)

        If pDataSet.Tables.Count = 0 Then Exit Sub

        Dim Linha As Data.DataRow
        Dim Item As ListItem
        pDropDownList.Items.Clear()

        For Each Linha In pDataSet.Tables(0).Rows
            Item = New ListItem
            Item.Text = Linha.Item(1)
            Item.Value = Linha.Item(0)
            If Linha.Table.Columns.Count = 3 Then
                Item.Selected = IIf(Linha.Item(2) = 2, True, False)
            Else
                Item.Selected = False
            End If
            pDropDownList.Items.Add(Item)
        Next
    End Sub

    Public Sub CarregaRadioButtonList(ByVal pDropDownList As WebControls.RadioButtonList, _
                                        ByVal pDataSet As System.Data.DataSet)

        If pDataSet.Tables.Count = 0 Then Exit Sub

        Dim Linha As Data.DataRow
        Dim Item As ListItem
        pDropDownList.Items.Clear()

        For Each Linha In pDataSet.Tables(0).Rows
            Item = New ListItem
            Item.Text = Linha.Item(1)
            Item.Value = Linha.Item(0)
            If Linha.Table.Columns.Count = 3 Then
                Item.Selected = IIf(Linha.Item(2) = 2, True, False)
            Else
                Item.Selected = False
            End If
            pDropDownList.Items.Add(Item)
        Next
    End Sub

End Class

