' ANIMA
Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports System.Text

Namespace Api

    <WebService(Namespace:="https://www.icontrolit.com.br/api/parameter")>
    <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
    <System.ComponentModel.ToolboxItem(False)>
    <ScriptService()>
    Public Class PutService
        Inherits WebService

        ' ---------- MODELOS ----------
        Public Class ApiResponse
            Public Property Success As Boolean
            Public Property Message As String
        End Class

        Private Function ValorOuNull(s As String) As String
            If String.IsNullOrWhiteSpace(s) Then Return Nothing
            Return s.Trim()
        End Function

        ' ---------- ENDPOINT GET (query string) ----------
        ' Ex.: /api/parameter/put.asmx/UpdateQS?Id_Parametro=6&Requisicao_Compra=REQ123&Ordem_Compra=OC456&Id_Processo=V360-789&Observacoes=Atualizado%20via%20API
        <WebMethod()>
        <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
        Public Function UpdateQS(
            ByVal Id_Parametro As Integer,
            ByVal Requisicao_Compra As String,
            ByVal Ordem_Compra As String,
            ByVal Id_Processo As String,
            ByVal Observacoes As String
        ) As ApiResponse

            If Not AutenticarRequisicao(Context) Then
                Context.Response.StatusCode = 401
                Return New ApiResponse With {.Success = False, .Message = "Unauthorized"}
            End If

            Return DoUpdate(Id_Parametro, Requisicao_Compra, Ordem_Compra, Id_Processo, Observacoes)
        End Function

        ' ---------- ENDPOINT POST (form-urlencoded) ----------
        ' Ex.: POST /api/parameter/put.asmx/UpdateForm  (Content-Type: application/x-www-form-urlencoded)
        '      Body: Id_Parametro=6&Requisicao_Compra=REQ123&Ordem_Compra=OC456&Id_Processo=V360-789&Observacoes=Atualizado+via+API
        <WebMethod()>
        <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)>
        Public Function UpdateForm() As ApiResponse
            If Not AutenticarRequisicao(Context) Then
                Context.Response.StatusCode = 401
                Return New ApiResponse With {.Success = False, .Message = "Unauthorized"}
            End If

            Dim req = Context.Request
            Dim idParam As Integer
            If Not Integer.TryParse(req.Params("Id_Parametro"), idParam) Then
                Context.Response.StatusCode = 400
                Return New ApiResponse With {.Success = False, .Message = "Id_Parametro inválido ou ausente."}
            End If

            Dim requisicao As String = req.Params("Requisicao_Compra")
            Dim ordem As String = req.Params("Ordem_Compra")
            Dim idProc As String = req.Params("Id_Processo")
            Dim obs As String = req.Params("Observacoes")

            Return DoUpdate(idParam, requisicao, ordem, idProc, obs)
        End Function

        ' ---------- ENDPOINT UPDATE STATUS ----------
        ' Ex.: /api/parameter/put.asmx/UpdateStatus?Id_Parametro=6&Status=Concluído&Descricao=Aqui vai a descricao do status
        <WebMethod()>
        <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
        Public Function UpdateStatus(
            ByVal Id_Parametro As Integer,
            ByVal Status As String,
            ByVal Descricao As String
        ) As ApiResponse

            If Not AutenticarRequisicao(Context) Then
                Context.Response.StatusCode = 401
                Return New ApiResponse With {.Success = False, .Message = "Unauthorized"}
            End If

            Return DoUpdateStatus(Id_Parametro, Status, Descricao)
        End Function

        ' ---------- LÓGICA COMUM ----------
        Private Function DoUpdate(
            ByVal Id_Parametro As Integer,
            ByVal Requisicao_Compra As String,
            ByVal Ordem_Compra As String,
            ByVal Id_Processo As String,
            ByVal Observacoes As String
        ) As ApiResponse

            If Id_Parametro <= 0 Then
                Context.Response.StatusCode = 400
                Return New ApiResponse With {.Success = False, .Message = "Id_Parametro inválido."}
            End If

            Dim conn As String = ConfigurationManager.AppSettings("ANIMA_EDUCACAO")
            If String.IsNullOrWhiteSpace(conn) Then
                Context.Response.StatusCode = 500
                Return New ApiResponse With {.Success = False, .Message = "Chave de conexão 'ANIMA_EDUCACAO' não configurada no Web.config."}
            End If

            Try
                Dim ws As New WSCadastro()
                Dim usuarioAtual = If(Context?.User?.Identity?.IsAuthenticated, Context.User.Identity.Name, "API_BASE")

                ws.AtualizarCamposCompras(
                    conn,
                    Id_Parametro,
                    ValorOuNull(Requisicao_Compra),
                    ValorOuNull(Ordem_Compra),
                    ValorOuNull(Id_Processo),
                    ValorOuNull(Observacoes),
                    usuarioAtual
                )

                Context.Response.StatusCode = 200
                Return New ApiResponse With {.Success = True, .Message = "Atualização realizada com sucesso."}
            Catch ex As Exception
                Context.Response.StatusCode = 500
                Return New ApiResponse With {.Success = False, .Message = "Falha ao atualizar: " & ex.Message}
            End Try
        End Function

        Private Function DoUpdateStatus(
            ByVal Id_Parametro As Integer,
            ByVal Status As String,
            ByVal Descricao As String
        ) As ApiResponse

            If Id_Parametro <= 0 Then
                Context.Response.StatusCode = 400
                Return New ApiResponse With {.Success = False, .Message = "Id_Parametro inválido."}
            End If

            Dim conn As String = ConfigurationManager.AppSettings("ANIMA_EDUCACAO")
            If String.IsNullOrWhiteSpace(conn) Then
                Context.Response.StatusCode = 500
                Return New ApiResponse With {.Success = False, .Message = "Chave de conexão 'ANIMA_EDUCACAO' não configurada no Web.config."}
            End If

            Try
                Dim ws As New WSCadastro()
                Dim usuarioAtual = If(Context?.User?.Identity?.IsAuthenticated, Context.User.Identity.Name, "API_UpdateStatus")

                ' NÃO usar ValorOuNull aqui - precisamos do Status exato para detectar "Concluído"
                ws.AtualizarStatusDescricao(
                    conn,
                    Id_Parametro,
                    Status,
                    Descricao,
                    usuarioAtual
                )

                Context.Response.StatusCode = 200
                Return New ApiResponse With {.Success = True, .Message = "Status e Descrição atualizados com sucesso."}
            Catch ex As Exception
                Context.Response.StatusCode = 500
                Return New ApiResponse With {.Success = False, .Message = "Falha ao atualizar status: " & ex.Message}
            End Try
        End Function

        ' ---------- AUTENTICAÇÃO ----------
        ' Usa appSettings: AuthUserApiParametros / AuthPassApiParametros
        Private Function AutenticarRequisicao(ctx As HttpContext) As Boolean
            Dim authorizationHeader As String = ctx.Request.Headers("Authorization")
            If String.IsNullOrEmpty(authorizationHeader) Then
                authorizationHeader = ctx.Request.ServerVariables("HTTP_AUTHORIZATION")
            End If

            If String.IsNullOrEmpty(authorizationHeader) OrElse Not authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase) Then
                Return False
            End If

            Try
                Dim encoded As String = authorizationHeader.Substring(6)
                Dim decoded As String = Encoding.UTF8.GetString(Convert.FromBase64String(encoded))
                Dim partes As String() = decoded.Split(":"c)
                If partes.Length <> 2 Then Return False

                Dim expectedUser As String = ConfigurationManager.AppSettings("AuthUserApiParametros")
                Dim expectedPass As String = ConfigurationManager.AppSettings("AuthPassApiParametros")
                If String.IsNullOrEmpty(expectedUser) OrElse String.IsNullOrEmpty(expectedPass) Then
                    Return False
                End If

                Return String.Equals(partes(0), expectedUser, StringComparison.Ordinal) AndAlso
                       String.Equals(partes(1), expectedPass, StringComparison.Ordinal)
            Catch
                Return False
            End Try
        End Function

    End Class
End Namespace
