Imports Microsoft.VisualBasic
Imports System.Data
Imports System.IO
Imports System.Linq
Imports System.Collections.Generic

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

    ' [INICIO - ICTRL2025029] - Funcoes de Segregacao de Acesso

    Public Function FiltrarTorresPorPermissao(ByVal pDataSet As DataSet, ByVal pTorresPermitidas As Object) As DataSet
        Try
            WriteLog("FiltrarTorresPorPermissao INICIADO")

            If pDataSet Is Nothing Then
                WriteLog("AVISO: pDataSet e Nothing - retornando Nothing")
                Return Nothing
            End If

            If pDataSet.Tables.Count = 0 OrElse pDataSet.Tables(0).Rows.Count = 0 Then
                WriteLog("AVISO: DataSet vazio - retornando DataSet original")
                Return pDataSet
            End If

            If pTorresPermitidas Is Nothing OrElse _
               pTorresPermitidas Is DBNull.Value OrElse _
               String.IsNullOrWhiteSpace(CStr(pTorresPermitidas)) Then
                WriteLog("Sem restricao de torres (acesso total) - retornando DataSet completo")
                Return pDataSet
            End If

            Dim torresStr As String = CStr(pTorresPermitidas).Trim()
            Dim listaTorres As List(Of String) = torresStr.Split(","c) _
                .Select(Function(x) x.Trim()) _
                .Where(Function(x) Not String.IsNullOrEmpty(x)) _
                .ToList()

            WriteLog(String.Format("Torres permitidas (IDs): {0}", String.Join(", ", listaTorres)))

            Dim nomeColuna As String = If(pDataSet.Tables(0).Columns.Contains("ID"), "ID", "Id_Ativo_Tipo_Grupo")
            If Not pDataSet.Tables(0).Columns.Contains(nomeColuna) Then
                WriteLog(String.Format("ERRO: Coluna '{0}' nao encontrada no DataSet", nomeColuna))
                Return pDataSet
            End If

            Dim dtFiltrado As DataTable = pDataSet.Tables(0).Clone()

            For Each row As DataRow In pDataSet.Tables(0).Rows
                Dim idTorre As String = row(nomeColuna).ToString()
                If listaTorres.Contains(idTorre) Then
                    dtFiltrado.ImportRow(row)
                End If
            Next

            Dim dsFiltrado As New DataSet()
            dsFiltrado.Tables.Add(dtFiltrado)

            WriteLog(String.Format("Filtro aplicado: {0} torres totais -> {1} torres permitidas", pDataSet.Tables(0).Rows.Count, dtFiltrado.Rows.Count))
            Return dsFiltrado

        Catch ex As Exception
            WriteLog(String.Format("ERRO em FiltrarTorresPorPermissao: {0}", ex.Message))
            Return pDataSet
        End Try
    End Function

    Public Function FiltrarAtivosPorTorre(ByVal pDataSet As DataSet, ByVal pTorresPermitidas As Object, Optional ByVal pNomeColunaTorre As String = "Id_Ativo_Tipo_Grupo") As DataSet
        Try
            WriteLog("FiltrarAtivosPorTorre INICIADO")

            If pDataSet Is Nothing Then Return Nothing
            If pDataSet.Tables.Count = 0 OrElse pDataSet.Tables(0).Rows.Count = 0 Then Return pDataSet

            If pTorresPermitidas Is Nothing OrElse _
               pTorresPermitidas Is DBNull.Value OrElse _
               String.IsNullOrWhiteSpace(CStr(pTorresPermitidas)) Then
                WriteLog("Sem restricao de torres (acesso total)")
                Return pDataSet
            End If

            Dim torresStr As String = CStr(pTorresPermitidas).Trim()
            Dim listaTorres As List(Of String) = torresStr.Split(","c) _
                .Select(Function(x) x.Trim()) _
                .Where(Function(x) Not String.IsNullOrEmpty(x)) _
                .ToList()

            If Not pDataSet.Tables(0).Columns.Contains(pNomeColunaTorre) Then
                If pDataSet.Tables(0).Columns.Contains("ID_Ativo_Tipo_Grupo") Then
                    pNomeColunaTorre = "ID_Ativo_Tipo_Grupo"
                ElseIf pDataSet.Tables(0).Columns.Contains("Id_Torre") Then
                    pNomeColunaTorre = "Id_Torre"
                Else
                    Return pDataSet
                End If
            End If

            Dim dtFiltrado As DataTable = pDataSet.Tables(0).Clone()

            For Each row As DataRow In pDataSet.Tables(0).Rows
                If Not IsDBNull(row(pNomeColunaTorre)) Then
                    Dim idTorre As String = row(pNomeColunaTorre).ToString()
                    If listaTorres.Contains(idTorre) Then
                        dtFiltrado.ImportRow(row)
                    End If
                End If
            Next

            Dim dsFiltrado As New DataSet()
            dsFiltrado.Tables.Add(dtFiltrado)

            WriteLog(String.Format("FiltrarAtivosPorTorre: {0} ativos -> {1} permitidos", pDataSet.Tables(0).Rows.Count, dtFiltrado.Rows.Count))
            Return dsFiltrado

        Catch ex As Exception
            WriteLog(String.Format("ERRO em FiltrarAtivosPorTorre: {0}", ex.Message))
            Return pDataSet
        End Try
    End Function

    Public Function BuscarServicosPorTorres(pTorres_Permitidas As Object, Optional pConn_Banco As String = Nothing) As DataSet
        Try
            WriteLog("--- BuscarServicosPorTorres: Iniciando ---")

            If String.IsNullOrEmpty(pConn_Banco) Then
                WriteLog("ERRO: Connection string nao foi fornecida")
                Return New DataSet()
            End If

            Dim connectionString As String = Descriptografar(pConn_Banco)

            If pTorres_Permitidas Is Nothing OrElse String.IsNullOrEmpty(pTorres_Permitidas.ToString()) Then
                WriteLog("Sem restricao de torres - retornando TODOS os servicos")
                Using conn As New Data.SqlClient.SqlConnection(connectionString)
                    conn.Open()
                    Dim cmd As New Data.SqlClient.SqlCommand("SELECT DISTINCT Id_Servico FROM Rl_Servico_Ativo_Tipo_Grupo", conn)
                    Dim adapter As New Data.SqlClient.SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    adapter.Fill(ds)
                    Return ds
                End Using
            End If

            WriteLog(String.Format("Torres permitidas: {0}", pTorres_Permitidas.ToString()))

            Using connection As New Data.SqlClient.SqlConnection(connectionString)
                connection.Open()
                Dim query As String = String.Format( _
                    "SELECT DISTINCT Id_Servico " & _
                    "FROM Rl_Servico_Ativo_Tipo_Grupo " & _
                    "WHERE Id_Ativo_Tipo_Grupo IN ({0})", _
                    pTorres_Permitidas.ToString())

                Dim command As New Data.SqlClient.SqlCommand(query, connection)
                Dim dataAdapter As New Data.SqlClient.SqlDataAdapter(command)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet)

                WriteLog(String.Format("Servicos encontrados: {0}", dataSet.Tables(0).Rows.Count))
                Return dataSet
            End Using

        Catch ex As Exception
            WriteLog(String.Format("ERRO em BuscarServicosPorTorres: {0}", ex.Message))
            Return New DataSet()
        End Try
    End Function

    Public Function FiltrarContratosPorServicos(pDataSet As DataSet, pServicosPermitidos As DataSet) As DataSet
        Try
            WriteLog("--- FiltrarContratosPorServicos: Iniciando ---")

            If pDataSet Is Nothing OrElse pDataSet.Tables.Count = 0 OrElse pDataSet.Tables(0).Rows.Count = 0 Then
                Return New DataSet()
            End If

            If pServicosPermitidos Is Nothing OrElse pServicosPermitidos.Tables.Count = 0 Then
                Return pDataSet
            End If

            If pServicosPermitidos.Tables(0).Rows.Count = 0 Then
                Return pDataSet.Clone()
            End If

            Dim servicosPermitidosIds As New List(Of String)
            For Each row As DataRow In pServicosPermitidos.Tables(0).Rows
                servicosPermitidosIds.Add(row("Id_Servico").ToString())
            Next

            Dim colunaServico As String = Nothing
            For Each col As DataColumn In pDataSet.Tables(0).Columns
                Dim colName As String = col.ColumnName.ToLower()
                If colName.Contains("id_servico") OrElse colName.Contains("idservico") Then
                    colunaServico = col.ColumnName
                    Exit For
                End If
            Next

            If String.IsNullOrEmpty(colunaServico) Then colunaServico = "ID"

            If Not pDataSet.Tables(0).Columns.Contains(colunaServico) Then
                Return pDataSet
            End If

            Dim linhasFiltradas = From row As DataRow In pDataSet.Tables(0).AsEnumerable()
                                 Where servicosPermitidosIds.Contains(row(colunaServico).ToString())
                                 Select row

            Dim dataSetFiltrado As DataSet = pDataSet.Clone()

            If linhasFiltradas.Count() > 0 Then
                Dim tabelaFiltrada As DataTable = linhasFiltradas.CopyToDataTable()
                dataSetFiltrado.Tables.Clear()
                dataSetFiltrado.Tables.Add(tabelaFiltrada.Copy())
            End If

            WriteLog(String.Format("FiltrarContratosPorServicos: {0} -> {1}", pDataSet.Tables(0).Rows.Count, dataSetFiltrado.Tables(0).Rows.Count))
            Return dataSetFiltrado

        Catch ex As Exception
            WriteLog(String.Format("ERRO em FiltrarContratosPorServicos: {0}", ex.Message))
            Return pDataSet
        End Try
    End Function

    Public Function FiltrarServicosPorTorre(ByVal pDataSet As DataSet, ByVal pTorresPermitidas As Object, ByVal pConnBanco As String) As DataSet
        Try
            WriteLog("FiltrarServicosPorTorre INICIADO")

            If pDataSet Is Nothing Then Return Nothing
            If pDataSet.Tables.Count = 0 OrElse pDataSet.Tables(0).Rows.Count = 0 Then Return pDataSet

            If pTorresPermitidas Is Nothing OrElse _
               pTorresPermitidas Is DBNull.Value OrElse _
               String.IsNullOrWhiteSpace(CStr(pTorresPermitidas)) Then
                WriteLog("Sem restricao de torres (acesso total)")
                Return pDataSet
            End If

            Dim torresStr As String = CStr(pTorresPermitidas).Trim()
            Dim listaTorres As List(Of String) = torresStr.Split(","c) _
                .Select(Function(x) x.Trim()) _
                .Where(Function(x) Not String.IsNullOrEmpty(x)) _
                .ToList()

            Dim servicosPermitidos As New List(Of String)

            Try
                Dim connectionStringDescriptografada As String = Descriptografar(pConnBanco)

                Using conn As New SqlClient.SqlConnection(connectionStringDescriptografada)
                    conn.Open()

                    For Each idTorre As String In listaTorres
                        Dim cmd As New SqlClient.SqlCommand()
                        cmd.Connection = conn
                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = "SELECT Id_Servico FROM dbo.Rl_Servico_Ativo_Tipo_Grupo WHERE Id_Ativo_Tipo_Grupo = @IdTorre"
                        cmd.Parameters.AddWithValue("@IdTorre", Integer.Parse(idTorre))

                        Using reader As SqlClient.SqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim idServico As String = reader("Id_Servico").ToString()
                                If Not servicosPermitidos.Contains(idServico) Then
                                    servicosPermitidos.Add(idServico)
                                End If
                            End While
                        End Using
                    Next

                    conn.Close()
                End Using

            Catch ex As Exception
                WriteLog(String.Format("ERRO ao consultar Rl_Servico_Ativo_Tipo_Grupo: {0}", ex.Message))
                Return pDataSet
            End Try

            If servicosPermitidos.Count = 0 Then
                Dim dtVazio As DataTable = pDataSet.Tables(0).Clone()
                Dim dsVazio As New DataSet()
                dsVazio.Tables.Add(dtVazio)
                Return dsVazio
            End If

            Dim nomeColuna As String = "Id_Servico"
            If Not pDataSet.Tables(0).Columns.Contains(nomeColuna) Then
                If pDataSet.Tables(0).Columns.Contains("ID") Then
                    nomeColuna = "ID"
                ElseIf pDataSet.Tables(0).Columns.Contains("Id") Then
                    nomeColuna = "Id"
                Else
                    Return pDataSet
                End If
            End If

            Dim dtFiltrado As DataTable = pDataSet.Tables(0).Clone()

            For Each row As DataRow In pDataSet.Tables(0).Rows
                Dim idServico As String = row(nomeColuna).ToString()
                If servicosPermitidos.Contains(idServico) Then
                    dtFiltrado.ImportRow(row)
                End If
            Next

            Dim dsFiltrado As New DataSet()
            dsFiltrado.Tables.Add(dtFiltrado)

            WriteLog(String.Format("FiltrarServicosPorTorre: {0} servicos -> {1} permitidos", pDataSet.Tables(0).Rows.Count, dtFiltrado.Rows.Count))
            Return dsFiltrado

        Catch ex As Exception
            WriteLog(String.Format("ERRO em FiltrarServicosPorTorre: {0}", ex.Message))
            Return pDataSet
        End Try
    End Function

    Private Sub WriteLog(message As String)
        Dim logFilePath As String = "d:\logs\mds.txt"
        Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
        Dim logMessage As String = String.Format("{0} [SEGREGACAO_MDS] {1}{2}", timestamp, message, Environment.NewLine)

        Try
            Dim logDir As String = Path.GetDirectoryName(logFilePath)
            If Not Directory.Exists(logDir) Then
                Directory.CreateDirectory(logDir)
            End If

            File.AppendAllText(logFilePath, logMessage, System.Text.Encoding.UTF8)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(String.Format("[SEGREGACAO_MDS] ERRO AO GRAVAR LOG: {0}", ex.Message))
        End Try
    End Sub

    Private Function Descriptografar(ByVal pString As System.String) As System.String
        Dim pSenha As System.String
        pSenha = "GUA@123"

        Dim chavecript As String = ""
        Dim chavecript_crc As String = ""
        Dim chavecript_key As String = ""
        Dim chavecriptcompleta As String = ""
        Dim X As Int32 = 0
        Dim I As Int32 = 0
        Dim Y As Int32 = 0
        Dim Z As Int32 = 0
        Dim W As Int32 = 0
        Dim Validade As Int32 = -1

        Dim vConvert_1 As String = ""

        For X = 1 To Len(pString)
            Select Case Mid(pString, X, 1)
                Case "A" : vConvert_1 = "0"
                Case "B" : vConvert_1 = "0"
                Case "C" : vConvert_1 = "1"
                Case "D" : vConvert_1 = "1"
                Case "E" : vConvert_1 = "2"
                Case "F" : vConvert_1 = "2"
                Case "G" : vConvert_1 = "3"
                Case "H" : vConvert_1 = "3"
                Case "I" : vConvert_1 = "4"
                Case "J" : vConvert_1 = "4"
                Case "K" : vConvert_1 = "5"
                Case "L" : vConvert_1 = "5"
                Case "M" : vConvert_1 = "6"
                Case "N" : vConvert_1 = "6"
                Case "O" : vConvert_1 = "7"
                Case "P" : vConvert_1 = "7"
                Case "Q" : vConvert_1 = "8"
                Case "R" : vConvert_1 = "8"
                Case "S" : vConvert_1 = "9"
                Case "T" : vConvert_1 = "9"
            End Select
            chavecript = chavecript + vConvert_1
        Next

        chavecript_crc = Mid(chavecript, 1, 5)
        chavecript_key = Mid(chavecript, 6, Len(chavecript))
        pString = Nothing
        X = 1

        While X <= 5
            Z = Len(chavecript_key) + 2
            W = 0
            Y = 1

            While Y <= Len(chavecript_key)
                W = W + (Asc(Mid(chavecript_key, Y, 1)) * Z)
                Z = Z - 1
                Y = Y + 1
            End While

            W = CType(Math.Round(W / 9.0, 0), Int32) Mod 9
            chavecript_key = CType(W, String) + chavecript_key
            X = X + 1
        End While

        If chavecript_crc = Mid(chavecript_key, 1, 5) Then
            chavecript_key = Mid(chavecript_key, 6, Len(chavecript_key))

            For X = 1 To Len(chavecript_key)
                If X Mod 2 = 0 Then
                    chavecriptcompleta = chavecriptcompleta + Char.ConvertFromUtf32(CType(Mid(chavecript_key, X - 1, 1) + Mid(chavecript_key, X, 1), Int32))
                End If
            Next

            For I = 1 To 10
                If pSenha = Mid(chavecriptcompleta, 1, I) Then
                    Validade = I + 1
                End If
            Next

            If Validade = -1 Then
                chavecriptcompleta = Nothing
            Else
                chavecriptcompleta = Mid(chavecriptcompleta, Validade, Len(chavecriptcompleta))
            End If
        End If
        Return chavecriptcompleta.ToLower()
    End Function

    ' [FIM - ICTRL2025029]

End Class

