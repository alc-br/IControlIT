' [INÍCIO - ICTRL-NF-202506-026]
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Data

Public Class Termo_Aceite
    Inherits System.Web.UI.Page

    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro()
    Dim vDataSetXML As New Data.DataSet()
    Private vHash As String
    Private vClientKey As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        vHash = Request.QueryString("hash")
        vClientKey = Request.QueryString("c")

        If Not IsPostBack Then
            ValidarEPreencherTermo()
        End If
    End Sub

    Private Sub ValidarEPreencherTermo()
        If String.IsNullOrEmpty(vHash) OrElse String.IsNullOrEmpty(vClientKey) Then
            ExibirMensagem("Link de aceite inválido ou incompleto.", True)
            Return
        End If

        Dim pPConn_Banco As String = GetConnectionStringFromAppSettings(vClientKey)
        If String.IsNullOrEmpty(pPConn_Banco) Then
            ExibirMensagem("Erro de configuração do servidor. Cliente inválido ou não configurado.", True)
            Return
        End If

        Dim dsAceite As DataSet = WS_Cadastro.Termo_Aceite_Validar(pPConn_Banco, "validar_hash", 0, 0, vHash, Nothing)
        If dsAceite Is Nothing OrElse dsAceite.Tables.Count = 0 OrElse dsAceite.Tables(0).Rows.Count = 0 Then
            ExibirMensagem("Link de aceite inválido ou expirado.", True)
            Return
        End If

        Dim aceiteRow = dsAceite.Tables(0).Rows(0)
        Dim idConsumidor As Integer = CInt(aceiteRow("Id_Consumidor"))
        Dim idAtivo As Integer = CInt(aceiteRow("Id_Ativo"))

        If Not IsDBNull(aceiteRow("Flg_Aceite")) AndAlso CBool(aceiteRow("Flg_Aceite")) Then
            Dim dtAceite As DateTime = CDate(aceiteRow("Dt_Aceite"))
            ExibirMensagem($"Este termo já foi aceito em {dtAceite:dd/MM/yyyy 'às' HH:mm:ss}.", False)
            Return
        End If

        Dim dsDadosTermo As DataSet = WS_Cadastro.Ativo(pPConn_Banco, idAtivo, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sd_SL_Termo", True, Nothing, Nothing, Nothing, Nothing, Nothing)
        If dsDadosTermo Is Nothing OrElse dsDadosTermo.Tables.Count = 0 OrElse dsDadosTermo.Tables(0).Rows.Count = 0 Then
            ExibirMensagem("Erro: Não foi possível carregar os dados do termo.", True)
            Return
        End If

        Dim dv As New DataView(dsDadosTermo.Tables(0), "Id_Consumidor = " & idConsumidor, Nothing, DataViewRowState.OriginalRows)
        If dv.Count = 0 Then
            ExibirMensagem("Erro crítico: A combinação de Ativo e Consumidor para este aceite é inválida.", True)
            Return
        End If

        Dim nmConsumidor As String = dv(0)("Nm_Consumidor").ToString()
        btnAceitar.Text = $"Eu, {nmConsumidor}, aceito os termos"
        ltTermoHtml.Text = GerarConteudoTermoComoHtml(dv, vClientKey)
    End Sub

    Protected Sub btnAceitar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim pPConn_Banco As String = GetConnectionStringFromAppSettings(vClientKey)
        If String.IsNullOrEmpty(pPConn_Banco) Then
            ExibirMensagem("Erro de configuração do servidor.", True)
            Return
        End If

        Dim dsConfirmacao As DataSet = WS_Cadastro.Termo_Aceite_Validar(pPConn_Banco, "confirmar_aceite", 0, 0, vHash, Nothing)

        If dsConfirmacao IsNot Nothing AndAlso dsConfirmacao.Tables.Count > 0 AndAlso dsConfirmacao.Tables(0).Rows.Count > 0 AndAlso CInt(dsConfirmacao.Tables(0).Rows(0)("RowsAffected")) > 0 Then
            ExibirMensagem("Obrigado! Seu aceite foi registrado com sucesso.", False)
        Else
            ExibirMensagem("Não foi possível registrar seu aceite. O link pode ter expirado ou já ter sido utilizado.", True)
        End If
    End Sub

    Private Function GetConnectionStringFromAppSettings(ByVal clientKey As String) As String
        Return ConfigurationManager.AppSettings(clientKey.ToUpper())
    End Function

    Private Sub ExibirMensagem(ByVal mensagem As String, ByVal isError As Boolean)
        pnlPrincipal.Visible = False
        pnlMensagem.Visible = True
        Dim cssClass As String = If(isError, "message message-error", "message message-success")
        ltMensagem.Text = $"<div class='{cssClass}'>{mensagem}</div>"
    End Sub

    Private Function App_Path() As String
        Return System.AppDomain.CurrentDomain.BaseDirectory()
    End Function

    Private Function GerarConteudoTermoComoHtml(ByVal dv As DataView, ByVal clientKey As String) As String
        ' --- INÍCIO DA LÓGICA COMPLETA ---
        Try
            vDataSetXML.ReadXml(App_Path() & "Termo/Base/Dados_Default.xml".Replace("Default", clientKey))
        Catch
            ' Fallback para o XML padrão caso o do cliente não seja encontrado
            vDataSetXML.ReadXml(App_Path() & "Termo/Base/Dados_Default.xml")
        End Try

        Dim tipoGrupo As String = "Telefonia_Movel" ' Padrão
        If dv(0).Row.Table.Columns.Contains("Nm_Ativo_Tipo_Grupo") AndAlso dv(0)("Nm_Ativo_Tipo_Grupo") IsNot DBNull.Value Then
            tipoGrupo = dv(0)("Nm_Ativo_Tipo_Grupo").ToString()
        End If

        If Not vDataSetXML.Tables.Contains(tipoGrupo) Then Return "Erro: Template do termo (XML) não encontrado."
        Dim xmlRow = vDataSetXML.Tables(tipoGrupo).Rows(0)

        Dim sb As New StringBuilder()

        ' Título e Subtítulo
        sb.AppendLine($"<h3 style='text-align:center;'>{xmlRow("Titulo")}</h3>")
        If Not String.IsNullOrEmpty(xmlRow("Subtitulo").ToString()) Then
            sb.AppendLine($"<h4 style='text-align:center; font-weight:normal;'>{xmlRow("Subtitulo")}</h4>")
        End If
        sb.AppendLine("<br/>")

        ' Corpo do Texto
        sb.AppendLine($"<p>{xmlRow("DadosUsuario")} {xmlRow("Ciente")} {xmlRow("DadosAparelho")} {xmlRow("Texto1")}</p>")

        Dim nome = dv(0)("Nm_Consumidor").ToString().Trim()
        Dim matricula = dv(0)("Matricula").ToString().Trim()
        sb.AppendLine($"<p>{nome} {xmlRow("Texto2")} {matricula} {xmlRow("Texto3")}</p>")

        ' Cláusulas numeradas
        For i = 1 To 8
            Dim col = $"Texto3_{i}"
            If xmlRow.Table.Columns.Contains(col) AndAlso Not String.IsNullOrWhiteSpace(xmlRow(col).ToString()) Then
                sb.AppendLine($"<p style='padding-left: 20px;'>{xmlRow(col).ToString().Replace("salto_Linha", "<br/>")}</p>")
            End If
        Next

        ' Cláusula de destaque
        If xmlRow.Table.Columns.Contains("Texto3_9") Then
            sb.AppendLine($"<p style='padding-left: 20px; margin-top:15px; margin-bottom:15px;'><strong><u>{xmlRow("Texto3_9")}</u></strong></p>")
        End If

        ' Detalhes do aparelho
        sb.AppendLine("<div style='padding-left: 35px;'>")
        sb.AppendLine($"<p><strong>{xmlRow("Linha")}</strong> {dv(0)("Linha")}</p>")
        sb.AppendLine($"<p><strong>{xmlRow("ChipAparelho")}</strong> {dv(0)("CHIP")}</p>")
        sb.AppendLine($"<p><strong>{xmlRow("MarcaModeloAparelho")}</strong> {dv(0)("Nm_Modelo_Fabricante")}</p>")
        sb.AppendLine($"<p><strong>{xmlRow("ImeiAparelho")}</strong> {dv(0)("Nr_Aparelho")}</p>")
        sb.AppendLine($"<p><strong>{xmlRow("Acessorio")}</strong> {dv(0)("Acessorio")}</p>")
        sb.AppendLine("</div>")

        Return sb.ToString()
        ' --- FIM DA LÓGICA COMPLETA ---
    End Function

End Class
' [FIM - ICTRL-NF-202506-026]