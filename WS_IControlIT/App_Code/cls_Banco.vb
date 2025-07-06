Imports System.Data

Public Class cls_Banco
    Dim v_nVetor As System.Int32 = 0

    Function convert_Arquivo(ByVal p_nmProcedure As System.String,
                            ByVal p_Parametro() As SqlClient.SqlParameter,
                            ByVal pPConn_Banco As System.String,
                            ByVal pOperadora As System.String) As Data.DataSet

        Dim vBanco As String = Descriptografar(pPConn_Banco)
        Dim vConexao As String = Trim(Mid(vBanco, 1, (vBanco.IndexOf("CATALOG=") + 8))) &
                                    "SC_" & pOperadora & "_" & Trim(Mid(vBanco, (vBanco.IndexOf("CATALOG=") + 9), (vBanco.IndexOf("USER") - (vBanco.IndexOf("CATALOG=") + 8)))) &
                                    Trim(Mid(vBanco, (vBanco.IndexOf("USER")), vBanco.Length))

        Dim o_Conn As SqlClient.SqlConnection
        o_Conn = New SqlClient.SqlConnection(vConexao)
        o_Conn.Open()

        Dim o_adaptadorBanco As SqlClient.SqlDataAdapter

        o_adaptadorBanco = New SqlClient.SqlDataAdapter
        With o_adaptadorBanco
            .SelectCommand = New SqlClient.SqlCommand
            .SelectCommand.Connection = o_Conn
            .SelectCommand.CommandText = p_nmProcedure
            .SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
            .SelectCommand.CommandTimeout = 3600
            If Not p_Parametro Is Nothing Then
                For v_nVetor = 0 To UBound(p_Parametro)
                    .SelectCommand.Parameters.Add(p_Parametro(v_nVetor))
                Next v_nVetor
            End If

            convert_Arquivo = New Data.DataSet
            .Fill(convert_Arquivo, p_nmProcedure)
        End With
        o_adaptadorBanco = Nothing

        o_Conn.Close()
    End Function

    Function retorna_Query(ByVal p_nmProcedure As System.String,
                            ByVal p_Parametro() As SqlClient.SqlParameter,
                            ByVal pPConn_Banco As System.String) As Data.DataSet

        Dim o_Conn As SqlClient.SqlConnection
        o_Conn = New SqlClient.SqlConnection(Descriptografar(pPConn_Banco))
        o_Conn.Open()

        Dim o_adaptadorBanco As SqlClient.SqlDataAdapter

        o_adaptadorBanco = New SqlClient.SqlDataAdapter
        With o_adaptadorBanco
            .SelectCommand = New SqlClient.SqlCommand
            .SelectCommand.Connection = o_Conn
            .SelectCommand.CommandText = p_nmProcedure
            .SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
            .SelectCommand.CommandTimeout = 3600
            If Not p_Parametro Is Nothing Then
                For v_nVetor = 0 To UBound(p_Parametro)
                    .SelectCommand.Parameters.Add(p_Parametro(v_nVetor))
                Next v_nVetor
            End If

            retorna_Query = New Data.DataSet
            .Fill(retorna_Query, p_nmProcedure)
        End With
        o_adaptadorBanco = Nothing

        o_Conn.Close()
    End Function

    Function manutencao_Dados(ByVal p_nmProcedure As System.String,
                                ByVal p_Parametro() As SqlClient.SqlParameter,
                                ByVal pPConn_Banco As System.String) As String

        Dim o_Conn As SqlClient.SqlConnection
        o_Conn = New SqlClient.SqlConnection(Descriptografar(pPConn_Banco))
        o_Conn.Open()

        Dim o_adaptadorBanco As SqlClient.SqlCommand

        o_adaptadorBanco = New SqlClient.SqlCommand
        With o_adaptadorBanco
            .CommandTimeout = 3600
            .CommandText = p_nmProcedure
            .CommandType = CommandType.StoredProcedure
            .Connection = o_Conn

            If Not p_Parametro Is Nothing Then
                For v_nVetor = 0 To UBound(p_Parametro)
                    .Parameters.Add(p_Parametro(v_nVetor))
                Next v_nVetor
            End If

            If o_Conn.State = ConnectionState.Closed Then
                o_Conn.Open()
            End If
            manutencao_Dados = .ExecuteNonQuery().ToString
            o_Conn.Close()

        End With
        o_adaptadorBanco = Nothing

        o_Conn.Close()
    End Function

    Public Sub monta_Parametro(ByVal pParametro() As System.Data.SqlClient.SqlParameter,
                                ByVal pParam_Proc As System.Object,
                                ByVal pParam_Banco As System.String,
                                ByVal pInputOutput As System.Boolean)
        For v_nVetor = 0 To UBound(pParametro)
            If pParametro(v_nVetor) Is Nothing Then
                pParametro(v_nVetor) = New SqlClient.SqlParameter
                pParametro(v_nVetor).ParameterName = pParam_Banco
                If pParam_Proc Is Nothing Then
                    pParametro(v_nVetor).DbType = DbType.String
                Else
                    pParametro(v_nVetor).DbType = GetDBType(pParam_Proc.GetType)
                End If
                pParametro(v_nVetor).Value = IIf(pParam_Proc = Nothing, System.DBNull.Value, pParam_Proc)
                pParametro(v_nVetor).Direction = IIf(pInputOutput = True, ParameterDirection.InputOutput, ParameterDirection.Input)
                Exit For
            End If
        Next
    End Sub

    Private Function GetDBType(ByVal pType As System.Type) As SqlDbType
        Dim vParametro As SqlClient.SqlParameter
        Dim vConvert As System.ComponentModel.TypeConverter
        vParametro = New SqlClient.SqlParameter()
        vConvert = System.ComponentModel.TypeDescriptor.GetConverter(vParametro.DbType)
        vParametro.DbType = vConvert.ConvertFrom(pType.Name)
        'If vConvert.CanConvertFrom(pType) Then
        '    vParametro.DbType = vConvert.ConvertFrom(pType.Name)
        'End If
        Return vParametro.DbType
    End Function

    Public Function convertRetorno(ByVal pDadoOrigem As System.String) As System.Data.DataSet
        '-----cria dataset para armazenar dados drag drop
        Dim vDtDragDrop As New System.Data.DataSet
        vDtDragDrop.DataSetName = "vDataSetDragDrop"
        '-----cria datatable
        Dim vDataTable As Data.DataTable = New Data.DataTable("vDataTableInclui")
        '-----cria colunas
        Dim vID As Data.DataColumn = New Data.DataColumn("ID", GetType(System.String))
        '-----adiciona colunas na tabela
        vDataTable.Columns.Add(vID)
        '-----adiciona tabela no dataset
        vDtDragDrop.Tables.Add(vDataTable)

        '-----carrega dataset
        Dim vLinha As Data.DataRow

        vLinha = vDataTable.NewRow
        vLinha("ID") = pDadoOrigem
        vDataTable.Rows.Add(vLinha)

        vDtDragDrop.AcceptChanges()
        Return vDtDragDrop
    End Function

    Function Descriptografar(ByVal pString As System.String) As System.String
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
                Case "A"
                    vConvert_1 = "0"
                Case "B"
                    vConvert_1 = "0"
                Case "C"
                    vConvert_1 = "1"
                Case "D"
                    vConvert_1 = "1"
                Case "E"
                    vConvert_1 = "2"
                Case "F"
                    vConvert_1 = "2"
                Case "G"
                    vConvert_1 = "3"
                Case "H"
                    vConvert_1 = "3"
                Case "I"
                    vConvert_1 = "4"
                Case "J"
                    vConvert_1 = "4"
                Case "K"
                    vConvert_1 = "5"
                Case "L"
                    vConvert_1 = "5"
                Case "M"
                    vConvert_1 = "6"
                Case "N"
                    vConvert_1 = "6"
                Case "O"
                    vConvert_1 = "7"
                Case "P"
                    vConvert_1 = "7"
                Case "Q"
                    vConvert_1 = "8"
                Case "R"
                    vConvert_1 = "8"
                Case "S"
                    vConvert_1 = "9"
                Case "T"
                    vConvert_1 = "9"
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
End Class