
Public Class Importa_PDF
    Inherits System.Web.UI.Page
    Dim WS_Manutencao As New WS_GUA_Manutencao.WSManutencao

    Dim oConfig As New cls_Config

    Protected Sub btIncluir_PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btIncluir.Click
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
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs) Handles btVoltar.Click
        Response.Redirect("../PDF/Lista_PDF.aspx?pRegistro=" & Request("pRegistro") & "&pTabela=" & Request("pTabela"))
    End Sub

End Class
