Imports System.Data
Imports System.IO
Imports System.Diagnostics
Imports System.Text
Imports System.Threading
Imports System.Web

Public Class cls_Banco
    Dim v_nVetor As System.Int32 = 0

    ' [INICIO - DIAG-042026] Log diagnostico unificado
    Private diagLogPath As String = "C:\Temp\Log042026.txt"

    Private Sub LogDiag(ByVal etapa As String, ByVal mensagem As String)
        Try
            Dim dir As String = Path.GetDirectoryName(diagLogPath)
            If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
            Dim linha As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | [cls_Banco] | {etapa} | {mensagem}"
            Using sw As StreamWriter = New StreamWriter(diagLogPath, True)
                sw.WriteLine(linha)
            End Using
        Catch
            ' Engole - log diagnostico nunca pode lancar
        End Try
    End Sub
    ' [FIM - DIAG-042026]

    ' [INICIO - DIAG-042026 BANCO] Log universal de chamadas ao banco
    ' Arquivo separado com rotacao diaria: Log042026_Banco_yyyyMMdd.txt
    Private Shared diagBancoLock As New Object()
    Private Const DIAG_BANCO_DIR As String = "C:\Temp\"

    Private Function DiagBancoEnabled() As Boolean
        Try
            Dim flag As String = ConfigurationManager.AppSettings("DiagBancoEnabled")
            Return Not String.Equals(flag, "false", StringComparison.OrdinalIgnoreCase)
        Catch
            Return True
        End Try
    End Function

    Private Function DiagBancoFilePath() As String
        Return Path.Combine(DIAG_BANCO_DIR, $"Log042026_Banco_{DateTime.Now:yyyyMMdd}.txt")
    End Function

    Private Function FormatarParametros(ByVal parametros() As SqlClient.SqlParameter) As String
        If parametros Is Nothing OrElse parametros.Length = 0 Then Return "(sem parametros)"
        Dim sb As New StringBuilder()
        For i As Integer = 0 To parametros.Length - 1
            Dim p As SqlClient.SqlParameter = parametros(i)
            If p Is Nothing Then
                sb.Append("[NULL_PARAM] ; ")
                Continue For
            End If
            Dim valor As String
            If p.Value Is Nothing OrElse p.Value Is DBNull.Value Then
                valor = "<DBNull>"
            Else
                Try
                    valor = Convert.ToString(p.Value)
                    If valor Is Nothing Then valor = "<Nothing>"
                    If valor.Length > 200 Then valor = valor.Substring(0, 200) & "...(trunc)"
                Catch
                    valor = "<erro_to_string>"
                End Try
            End If
            ' Mascara campos sensiveis
            Dim nomeUpper As String = If(p.ParameterName, "").ToUpper()
            If nomeUpper.Contains("SENHA") OrElse nomeUpper.Contains("PASSWORD") OrElse nomeUpper.Contains("TOKEN") OrElse nomeUpper.Contains("KEY") Then
                valor = "***MASKED***"
            End If
            ' Marca campos numericos com valor 0 ou negativo (para detectar FK violation tipo Id_Consumidor=0)
            Dim flag0 As String = ""
            If p.SqlDbType = SqlDbType.Int OrElse p.SqlDbType = SqlDbType.BigInt OrElse p.SqlDbType = SqlDbType.SmallInt Then
                If valor = "0" Then flag0 = " [ZERO!]"
            End If
            sb.Append($"{p.ParameterName}={valor}{flag0} (Type={p.SqlDbType}); ")
        Next
        Return sb.ToString()
    End Function

    Private Function CapturarStackChamador() As String
        Try
            Dim stack As New StackTrace(2, False) ' pula este metodo e o chamador imediato (LogBanco)
            Dim sb As New StringBuilder()
            Dim count As Integer = 0
            For i As Integer = 0 To stack.FrameCount - 1
                If count >= 5 Then Exit For
                Dim frame As StackFrame = stack.GetFrame(i)
                Dim m As Reflection.MethodBase = frame.GetMethod()
                If m Is Nothing Then Continue For
                Dim tipo As String = If(m.DeclaringType IsNot Nothing, m.DeclaringType.Name, "?")
                ' Pula frames internos do proprio cls_Banco
                If tipo = "cls_Banco" Then Continue For
                sb.Append($"{tipo}.{m.Name} <- ")
                count += 1
            Next
            Return sb.ToString().TrimEnd(" "c, "<"c, "-"c)
        Catch
            Return "(stack indisponivel)"
        End Try
    End Function

    Private Function CapturarHttpContext() As String
        Try
            Dim ctx As HttpContext = HttpContext.Current
            If ctx Is Nothing OrElse ctx.Request Is Nothing Then Return "HTTP=N/A"
            Dim req As HttpRequest = ctx.Request
            Dim soapAction As String = req.Headers("SOAPAction")
            Return $"URL={req.RawUrl} | IP={req.UserHostAddress} | UA={If(req.UserAgent, "-")} | SOAPAction={If(soapAction, "-")}"
        Catch
            Return "HTTP=erro"
        End Try
    End Function

    Private Sub LogBanco(ByVal etapa As String, ByVal procedure As String, ByVal mensagem As String)
        If Not DiagBancoEnabled() Then Return
        Try
            Dim arquivo As String = DiagBancoFilePath()
            Dim threadId As Integer = Thread.CurrentThread.ManagedThreadId
            Dim linha As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | TID={threadId} | {etapa} | Proc={procedure} | {mensagem}"
            SyncLock diagBancoLock
                Dim dir As String = Path.GetDirectoryName(arquivo)
                If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
                Using sw As StreamWriter = New StreamWriter(arquivo, True, Encoding.UTF8)
                    sw.WriteLine(linha)
                End Using
            End SyncLock
        Catch
            ' Engole
        End Try
    End Sub

    ' Loga erro SQL com destaque para FK violation (547) e outros erros criticos
    Private Sub LogBancoErroSql(ByVal procedure As String, ByVal ex As SqlClient.SqlException, ByVal parametros() As SqlClient.SqlParameter)
        If Not DiagBancoEnabled() Then Return
        Dim destaque As String = ""
        Select Case ex.Number
            Case 547 : destaque = "*** FK_VIOLATION ***"
            Case 2627, 2601 : destaque = "*** UNIQUE_VIOLATION ***"
            Case 8152, 2628 : destaque = "*** STRING_TRUNCATION ***"
            Case 515 : destaque = "*** NOT_NULL_VIOLATION ***"
            Case -2 : destaque = "*** TIMEOUT ***"
            Case 1205 : destaque = "*** DEADLOCK ***"
        End Select
        Dim msg As String = $"{destaque} ErroNumero={ex.Number} | State={ex.State} | Class={ex.Class} | LineNumber={ex.LineNumber} | ProcedureSql={ex.Procedure} | Server={ex.Server} | Msg={ex.Message} | Params={FormatarParametros(parametros)} | Stack={CapturarStackChamador()} | {CapturarHttpContext()}"
        LogBanco("ERRO_SQL", procedure, msg)
    End Sub
    ' [FIM - DIAG-042026 BANCO]

    Function convert_Arquivo(ByVal p_nmProcedure As System.String,
                            ByVal p_Parametro() As SqlClient.SqlParameter,
                            ByVal pPConn_Banco As System.String,
                            ByVal pOperadora As System.String) As Data.DataSet

        ' [DIAG-042026 BANCO]
        LogBanco("INICIO_CONV", p_nmProcedure, $"Operadora={pOperadora} | Params={FormatarParametros(p_Parametro)} | Stack={CapturarStackChamador()} | {CapturarHttpContext()}")
        Dim swDiag As Stopwatch = Stopwatch.StartNew()

        Dim vBanco As String = Descriptografar(pPConn_Banco)
        Dim vConexao As String = Trim(Mid(vBanco, 1, (vBanco.IndexOf("CATALOG=") + 8))) &
                                    "SC_" & pOperadora & "_" & Trim(Mid(vBanco, (vBanco.IndexOf("CATALOG=") + 9), (vBanco.IndexOf("USER") - (vBanco.IndexOf("CATALOG=") + 8)))) &
                                    Trim(Mid(vBanco, (vBanco.IndexOf("USER")), vBanco.Length))

        Dim o_Conn As SqlClient.SqlConnection
        o_Conn = New SqlClient.SqlConnection(vConexao)

        Try
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

            swDiag.Stop()
            LogBanco("OK_CONV", p_nmProcedure, $"DuracaoMs={swDiag.ElapsedMilliseconds}")

        Catch sqlEx As SqlClient.SqlException
            swDiag.Stop()
            LogBancoErroSql(p_nmProcedure, sqlEx, p_Parametro)
            Throw
        Catch ex As Exception
            swDiag.Stop()
            LogBanco("ERRO_CONV", p_nmProcedure, $"Tipo={ex.GetType().FullName} | Msg={ex.Message} | DuracaoMs={swDiag.ElapsedMilliseconds}")
            Throw
        Finally
            If o_Conn IsNot Nothing AndAlso o_Conn.State <> ConnectionState.Closed Then
                o_Conn.Close()
            End If
        End Try
    End Function

    Function retorna_Query(ByVal p_nmProcedure As System.String,
                            ByVal p_Parametro() As SqlClient.SqlParameter,
                            ByVal pPConn_Banco As System.String) As Data.DataSet

        ' [DIAG-042026 BANCO] Log universal de toda chamada
        LogBanco("INICIO", p_nmProcedure, $"Params={FormatarParametros(p_Parametro)} | Stack={CapturarStackChamador()} | {CapturarHttpContext()}")
        Dim swDiag As Stopwatch = Stopwatch.StartNew()

        ' [DIAG-042026] Hipotese C: Descriptografar pode retornar Nothing/string vazia silenciosamente
        Dim connString As String = Descriptografar(pPConn_Banco)
        LogDiag("retorna_Query.DESCRIPTOGRAFAR", $"Procedure={p_nmProcedure} | ConnString_IsNothing={connString Is Nothing} | ConnString_Vazio={String.IsNullOrEmpty(connString)} | Tamanho={If(connString, "").Length}")
        If String.IsNullOrEmpty(connString) Then
            LogDiag("retorna_Query.ERRO_DESCRIPT", $"Procedure={p_nmProcedure} | Falha ao descriptografar connection string (entrada de tamanho {If(pPConn_Banco, "").Length})")
            LogBanco("ERRO_DESCRIPT", p_nmProcedure, "Falha ao descriptografar connection string")
            Throw New Exception("Falha ao descriptografar a connection string. Verifique a chave criptografada no Web.config.")
        End If

        Dim o_Conn As SqlClient.SqlConnection
        o_Conn = New SqlClient.SqlConnection(connString)
        Try
            o_Conn.Open()
            LogDiag("retorna_Query.CONN_ABERTA", $"Procedure={p_nmProcedure} | Database={o_Conn.Database} | DataSource={o_Conn.DataSource}")
        Catch openEx As Exception
            LogDiag("retorna_Query.ERRO_OPEN", $"Procedure={p_nmProcedure} | Tipo={openEx.GetType().FullName} | Msg={openEx.Message}")
            LogBanco("ERRO_OPEN", p_nmProcedure, $"Tipo={openEx.GetType().FullName} | Msg={openEx.Message}")
            Throw
        End Try

        Try
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

            swDiag.Stop()
            Dim rowCount As Integer = -1
            Try
                If retorna_Query IsNot Nothing AndAlso retorna_Query.Tables.Count > 0 Then rowCount = retorna_Query.Tables(0).Rows.Count
            Catch
            End Try
            LogBanco("OK", p_nmProcedure, $"DuracaoMs={swDiag.ElapsedMilliseconds} | RowCount={rowCount}")

        Catch sqlEx As SqlClient.SqlException
            swDiag.Stop()
            LogDiag("retorna_Query.ERRO_FILL", $"Procedure={p_nmProcedure} | Tipo=SqlException | Number={sqlEx.Number} | Msg={sqlEx.Message}")
            LogBancoErroSql(p_nmProcedure, sqlEx, p_Parametro)
            Throw
        Catch fillEx As Exception
            swDiag.Stop()
            LogDiag("retorna_Query.ERRO_FILL", $"Procedure={p_nmProcedure} | Tipo={fillEx.GetType().FullName} | Msg={fillEx.Message}")
            LogBanco("ERRO_FILL", p_nmProcedure, $"Tipo={fillEx.GetType().FullName} | Msg={fillEx.Message} | DuracaoMs={swDiag.ElapsedMilliseconds} | Stack={CapturarStackChamador()}")
            Throw
        Finally
            ' [DIAG-042026] Hipotese F: garante fechamento da conexao mesmo em caso de erro (evita vazamento de pool)
            If o_Conn IsNot Nothing AndAlso o_Conn.State <> ConnectionState.Closed Then
                o_Conn.Close()
            End If
        End Try
    End Function

    Function manutencao_Dados(ByVal p_nmProcedure As System.String,
                                ByVal p_Parametro() As SqlClient.SqlParameter,
                                ByVal pPConn_Banco As System.String) As String

        ' [DIAG-042026 BANCO] Log universal de toda chamada de INSERT/UPDATE/DELETE
        LogBanco("INICIO_MANUT", p_nmProcedure, $"Params={FormatarParametros(p_Parametro)} | Stack={CapturarStackChamador()} | {CapturarHttpContext()}")
        Dim swDiag As Stopwatch = Stopwatch.StartNew()

        Dim o_Conn As SqlClient.SqlConnection
        Dim connString As String = Descriptografar(pPConn_Banco)
        If String.IsNullOrEmpty(connString) Then
            LogBanco("ERRO_DESCRIPT_MANUT", p_nmProcedure, "Falha ao descriptografar")
            Throw New Exception("Falha ao descriptografar a connection string.")
        End If
        o_Conn = New SqlClient.SqlConnection(connString)

        Try
            o_Conn.Open()
        Catch openEx As Exception
            LogBanco("ERRO_OPEN_MANUT", p_nmProcedure, $"Tipo={openEx.GetType().FullName} | Msg={openEx.Message}")
            Throw
        End Try

        Try
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
            End With
            o_adaptadorBanco = Nothing

            swDiag.Stop()
            LogBanco("OK_MANUT", p_nmProcedure, $"DuracaoMs={swDiag.ElapsedMilliseconds} | LinhasAfetadas={manutencao_Dados}")

        Catch sqlEx As SqlClient.SqlException
            swDiag.Stop()
            LogBancoErroSql(p_nmProcedure, sqlEx, p_Parametro)
            Throw
        Catch ex As Exception
            swDiag.Stop()
            LogBanco("ERRO_MANUT", p_nmProcedure, $"Tipo={ex.GetType().FullName} | Msg={ex.Message} | DuracaoMs={swDiag.ElapsedMilliseconds} | Stack={CapturarStackChamador()}")
            Throw
        Finally
            If o_Conn IsNot Nothing AndAlso o_Conn.State <> ConnectionState.Closed Then
                o_Conn.Close()
            End If
        End Try
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