
'/*
'* HISTÓRICO DE MODIFICAÇÕES
'* [ICTRL-NF-202512-002 | 2025-12-04 | Parceiro IControlIT] - Observação obrigatória e marcar como pago ao anexar comprovante
'*/
Public Class Importa_PDF
    Inherits System.Web.UI.Page
    Dim WS_Manutencao As New WS_GUA_Manutencao.WSManutencao
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo

    Dim oConfig As New cls_Config

    ' [INÍCIO - ICTRL-NF-202512-002]
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Exibe campo de observação sempre que for comprovante (inclusive no PostBack,
        ' para que o txtObservacao participe do ViewState e o valor digitado seja preservado)
        If Request("pTabela") = "Fatura_Comprovante" Then
            trObservacao.Visible = True
        End If
    End Sub
    ' [FIM - ICTRL-NF-202512-002]

    Protected Sub btIncluir_PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btIncluir.Click
        Try
            Dim strTipo As String = inputPDF.PostedFile.ContentType
            Dim intTamanho As Int64 = System.Convert.ToInt32(inputPDF.PostedFile.InputStream.Length)
            Dim nomeArquivo As String = inputPDF.PostedFile.FileName

            '-----limita tamanho do arquivo
            If intTamanho > 9999999999 Or intTamanho = 0 Then Exit Sub
            Dim byteImagem As Byte() = New Byte(intTamanho) {}
            inputPDF.PostedFile.InputStream.Read(byteImagem, 0, intTamanho)

            '-----grava arquivo
            Dim id_retorno As Data.DataSet
            id_retorno = WS_Manutencao.ArquivoPDF(Session("Conn_Banco"),
                                                    Nothing,
                                                    nomeArquivo,
                                                    Request("pTabela"),
                                                    Request("pRegistro"),
                                                    intTamanho,
                                                    byteImagem,
                                                    Session("Id_Usuario"),
                                                    "sp_SM",
                                                    False)

            ' [INÍCIO - ICTRL-NF-202512-002] - Marcar fatura como paga ao anexar comprovante
            ' Ao anexar comprovante, observação é obrigatória e marca como pago
            If Request("pTabela") = "Fatura_Comprovante" Then
                Dim idFatura As Integer = Convert.ToInt32(Request("pRegistro"))
                Dim observacao As String = txtObservacao.Text.Trim()

                ' Chama a stored procedure sp_Marcar_Fatura_Paga via WebService
                WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
                WS_Modulo.Marcar_Fatura_Paga(
                    Session("Conn_Banco"),
                    idFatura,           ' @Id_Fatura
                    observacao,         ' @Observacao
                    1                   ' @Fl_Pago = 1 (marcar como pago)
                )
            End If
            ' [FIM - ICTRL-NF-202512-002]

            ' Redireciona de volta para Lista_PDF
            Response.Redirect("../PDF/Lista_PDF.aspx?pRegistro=" & Request("pRegistro") & "&pTabela=" & Request("pTabela"))

        Catch ex As Exception
            lblInfo.Text = "Erro ao salvar arquivo: " & ex.Message
            lblInfo.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs) Handles btVoltar.Click
        Response.Redirect("../PDF/Lista_PDF.aspx?pRegistro=" & Request("pRegistro") & "&pTabela=" & Request("pTabela"))
    End Sub

End Class
