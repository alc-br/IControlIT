' ANIMA - API para atualizar Status do Robô
Imports System.IO
Imports System.Web.Services
Imports System.Configuration
Imports System.Text
Imports System.Data
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Imports System.Web
Imports System.Data.SqlClient

Namespace Api
    <WebService(Namespace:="https://www.icontrolit.com.br/api/parameter/updatestatusrobo.asmx")>
    <WebServiceBinding()>
    <ScriptService()>
    Public Class UpdateStatusRobo
        Inherits WebService

        Private ReadOnly _oBanco As New cls_Banco()

        ''' <summary>
        ''' Modelo para requisição de atualização de status
        ''' </summary>
        Public Class UpdateStatusRequest
            Public Property Id_Parametro As Integer
            Public Property Status_Robo As Integer
        End Class

        ''' <summary>
        ''' Modelo para resposta da API
        ''' </summary>
        Public Class ApiResponse
            Public Property Success As Boolean
            Public Property Message As String
            Public Property Data As Object
        End Class

        ''' <summary>
        ''' Lê o payload JSON do corpo da requisição
        ''' </summary>
        Private Function TryReadPayloadFromBody() As UpdateStatusRequest
            Dim context = HttpContext.Current
            Dim request = context?.Request
            If request Is Nothing Then
                Return Nothing
            End If

            Dim stream = request.InputStream
            If stream Is Nothing Then
                Return Nothing
            End If

            Dim originalPosition As Long = 0
            If stream.CanSeek Then
                originalPosition = stream.Position
                stream.Position = 0
            End If

            Dim encoding As Encoding = If(request.ContentEncoding, Encoding.UTF8)
            Dim body As String

            Using reader As New StreamReader(stream, encoding, detectEncodingFromByteOrderMarks:=True, bufferSize:=1024, leaveOpen:=True)
                body = reader.ReadToEnd()
            End Using

            If stream.CanSeek Then
                stream.Position = originalPosition
            End If

            If String.IsNullOrWhiteSpace(body) Then
                Return Nothing
            End If

            Dim serializer As New JavaScriptSerializer()
            Try
                Return serializer.Deserialize(Of UpdateStatusRequest)(body)
            Catch
                ' Tentar ler de wrapper "payload"
                Try
                    Dim wrapper = serializer.Deserialize(Of Dictionary(Of String, Object))(body)
                    If wrapper IsNot Nothing Then
                        For Each item In wrapper
                            If String.Equals(item.Key, "payload", StringComparison.OrdinalIgnoreCase) Then
                                Return serializer.ConvertToType(Of UpdateStatusRequest)(item.Value)
                            End If
                        Next
                    End If
                Catch
                End Try
            End Try

            Return Nothing
        End Function

        ''' <summary>
        ''' Autentica a requisição usando Basic Auth ou Headers X-Api-User/X-Api-Key
        ''' </summary>
        Private Function AutenticarRequisicao() As Boolean
            Const logPath As String = "C:\Temp\ApiUpdateStatusRoboAuth.log"

            Dim builder As New StringBuilder()
            builder.AppendLine("---- " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " ----")

            Dim authorizationHeader As String = Context?.Request?.Headers("Authorization")
            builder.AppendLine("AuthorizationHeaderPresent=" & (Not String.IsNullOrEmpty(authorizationHeader)).ToString())

            If String.IsNullOrEmpty(authorizationHeader) Then
                authorizationHeader = Context?.Request?.ServerVariables("HTTP_AUTHORIZATION")
                builder.AppendLine("ServerVariableAuthPresent=" & (Not String.IsNullOrEmpty(authorizationHeader)).ToString())
            End If

            Dim isBasic As Boolean = Not String.IsNullOrEmpty(authorizationHeader) AndAlso authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase)
            builder.AppendLine("IsBasic=" & isBasic.ToString())

            Dim autenticado As Boolean = False

            If isBasic Then
                Dim encodedCredentials As String = authorizationHeader.Substring(6)
                Dim decoded As String = Nothing

                Try
                    decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials))
                Catch
                    builder.AppendLine("DecodeFailed=True")
                End Try

                If Not String.IsNullOrEmpty(decoded) Then
                    Dim partes As String() = decoded.Split(":"c)
                    If partes.Length = 2 Then
                        Dim expectedUser As String = ConfigurationManager.AppSettings("AuthUserApiParametros")
                        Dim expectedPass As String = ConfigurationManager.AppSettings("AuthPassApiParametros")
                        If Not String.IsNullOrEmpty(expectedUser) AndAlso Not String.IsNullOrEmpty(expectedPass) Then
                            autenticado = String.Equals(partes(0), expectedUser, StringComparison.Ordinal) AndAlso String.Equals(partes(1), expectedPass, StringComparison.Ordinal)
                        Else
                            builder.AppendLine("CredenciaisConfiguradas=False")
                        End If
                    Else
                        builder.AppendLine("SplitFailed=True")
                    End If
                End If
            End If

            builder.AppendLine("AuthSuccess=" & autenticado.ToString())

            Try
                Dim dir = Path.GetDirectoryName(logPath)
                If Not String.IsNullOrEmpty(dir) AndAlso Not Directory.Exists(dir) Then
                    Directory.CreateDirectory(dir)
                End If
                File.AppendAllText(logPath, builder.ToString())
            Catch
            End Try

            Return autenticado
        End Function

        ''' <summary>
        ''' WebMethod para atualizar o Status_Robo de um parâmetro
        ''' Aceita requisições POST com JSON no body
        ''' Formato: { "Id_Parametro": 123, "Status_Robo": 200 }
        ''' Status_Robo possíveis:
        ''' - 100: Preparando
        ''' - 150: Executando
        ''' - 180: Finalizado, esperando verificação
        ''' - 200: Sucesso (limpa Sequencia_Execucao automaticamente)
        ''' - 250: Sucesso com pendências
        ''' - 300: Erro recuperável
        ''' - 350: Erro lógico
        ''' - 400: Falha operacional
        ''' - 500: Falha crítica
        ''' </summary>
        <WebMethod(BufferResponse:=True)>
        <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=False)>
        Public Sub UpdateStatus()
            ' Autenticar requisição
            If Not AutenticarRequisicao() Then
                Context.Response.StatusCode = 401
                Context.Response.StatusDescription = "Unauthorized"
                Context.Response.AddHeader("WWW-Authenticate", "Basic realm=" & ChrW(34) & "UpdateStatusRobo" & ChrW(34))
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                Return
            End If

            Dim response As ApiResponse = New ApiResponse()
            Dim serializer As New JavaScriptSerializer()

            Try
                ' Ler payload do body
                Dim payload As UpdateStatusRequest = TryReadPayloadFromBody()

                If payload Is Nothing Then
                    response.Success = False
                    response.Message = "Payload invalido ou ausente. Formato esperado: { ""Id_Parametro"": 123, ""Status_Robo"": 200 }"
                    Context.Response.StatusCode = 400
                    Context.Response.ContentType = "application/json; charset=utf-8"
                    Context.Response.Write(serializer.Serialize(response))
                    Context.Response.Flush()
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                    Return
                End If

                ' Validar parâmetros
                If payload.Id_Parametro <= 0 Then
                    response.Success = False
                    response.Message = "Id_Parametro deve ser maior que zero."
                    Context.Response.StatusCode = 400
                    Context.Response.ContentType = "application/json; charset=utf-8"
                    Context.Response.Write(serializer.Serialize(response))
                    Context.Response.Flush()
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                    Return
                End If

                ' Validar Status_Robo
                Dim statusValidos As Integer() = {100, 150, 180, 200, 250, 300, 350, 400, 500}
                If Not statusValidos.Contains(payload.Status_Robo) Then
                    response.Success = False
                    response.Message = "Status_Robo invalido. Valores aceitos: 100, 150, 180, 200, 250, 300, 350, 400, 500"
                    Context.Response.StatusCode = 400
                    Context.Response.ContentType = "application/json; charset=utf-8"
                    Context.Response.Write(serializer.Serialize(response))
                    Context.Response.Flush()
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                    Return
                End If

                ' Atualizar status no banco
                Dim sucesso As Boolean = AtualizarStatusRobo(payload.Id_Parametro, payload.Status_Robo)

                If sucesso Then
                    response.Success = True
                    response.Message = "Status atualizado com sucesso."
                    response.Data = New With {
                        .Id_Parametro = payload.Id_Parametro,
                        .Status_Robo = payload.Status_Robo,
                        .Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    }
                    Context.Response.StatusCode = 200
                Else
                    response.Success = False
                    response.Message = "Falha ao atualizar status. Verifique se o Id_Parametro existe."
                    Context.Response.StatusCode = 500
                End If

            Catch ex As Exception
                response.Success = False
                response.Message = "Erro ao processar requisicao: " & ex.Message
                Context.Response.StatusCode = 500
            End Try

            ' Retornar resposta JSON
            Context.Response.ContentType = "application/json; charset=utf-8"
            Context.Response.Write(serializer.Serialize(response))
            Context.Response.Flush()
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Sub

        ''' <summary>
        ''' Atualiza o Status_Robo no banco de dados
        ''' Se Status_Robo = 200, limpa automaticamente a Sequencia_Execucao
        ''' </summary>
        Private Function AtualizarStatusRobo(idParametro As Integer, statusRobo As Integer) As Boolean
            Dim connConfigurada As String = ConfigurationManager.AppSettings("ANIMA_EDUCACAO")
            If String.IsNullOrWhiteSpace(connConfigurada) Then
                Throw New ApplicationException("Chave ANIMA_EDUCACAO nao configurada no Web.config.")
            End If

            Dim connectionString As String = _oBanco.Descriptografar(connConfigurada)

            Try
                Using conn As New SqlConnection(connectionString)
                    conn.Open()

                    Dim query As String = "
                        UPDATE Anima_Parametros
                        SET
                            Status_Robo = @StatusRobo,
                            Sequencia_Execucao = CASE
                                WHEN @StatusRobo = 200 THEN NULL
                                ELSE Sequencia_Execucao
                            END,
                            Data_Modificacao = GETDATE(),
                            Usuario_Modificacao = 'API_UpdateStatusRobo'
                        WHERE Id_Parametro = @IdParametro"

                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@IdParametro", idParametro)
                        cmd.Parameters.AddWithValue("@StatusRobo", statusRobo)

                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        Return rowsAffected > 0
                    End Using
                End Using
            Catch ex As Exception
                Throw New ApplicationException("Erro ao atualizar Status_Robo: " & ex.Message)
            End Try
        End Function
    End Class
End Namespace
