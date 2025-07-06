
Imports System.IO

Public Class View_PDF
    Inherits System.Web.UI.Page
    Dim WS_Manutencao As New WS_GUA_Manutencao.WSManutencao

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request("Manual") = 1 Then
                If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                    Response.AddHeader("Content-Disposition", "attachment; filename=Guia_Admin.pdf")
                    Response.WriteFile(MapPath("~/Img_Sistema/Guia_Admin.pdf"))
                Else
                    Response.AddHeader("Content-Disposition", "attachment; filename=Guia_Usuario.pdf")
                    Response.WriteFile(MapPath("~/Img_Sistema/Guia_Usuario.pdf"))
                End If
            ElseIf Request("Manual") = 2 Then
                Response.AddHeader("Content-Disposition", "attachment; filename=Faturas_todas.zip")
                Response.WriteFile(MapPath("~/Img_Sistema/Faturas_todas.zip"))
            Else
                Dim vDataSet As New Data.DataSet
                vDataSet = WS_Manutencao.ArquivoPDF(Session("Conn_Banco"),
                                                    Request("pId_Arquivo_PDF"),
                                                    Nothing,
                                                    Request("pTabela"),
                                                    Request("pRegistro"),
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    "sp_View_PDF",
                                                    True)

                Dim byteImagem() As Byte = vDataSet.Tables(0).Rows(0).Item("Arquivo")
                Dim nomeArquivo As String = vDataSet.Tables(0).Rows(0).Item("Nm_Arquivo_PDF")
                Dim extensaoArquivo As String = nomeArquivo.Trim().ToLower()
                If extensaoArquivo.EndsWith(".txt") Then
                    Response.ContentType = "text/plain"
                ElseIf extensaoArquivo.EndsWith(".htm") OrElse extensaoArquivo.EndsWith(".html") Then
                    Response.ContentType = "text/HTML"
                ElseIf extensaoArquivo.EndsWith(".gif") Then
                    Response.ContentType = "image/GIF"
                ElseIf extensaoArquivo.EndsWith(".jpeg") OrElse extensaoArquivo.EndsWith(".jpg") Then
                    Response.ContentType = "image/JPEG"
                ElseIf extensaoArquivo.EndsWith(".zip") Then
                    Response.ContentType = "application/x-zip-compressed"
                ElseIf extensaoArquivo.EndsWith(".pdf") Then
                    Response.ContentType = "application/pdf"
                ElseIf extensaoArquivo.EndsWith(".doc") OrElse extensaoArquivo.EndsWith(".docx") OrElse extensaoArquivo.EndsWith(".rtf") Then
                    Response.ContentType = "application/msword"
                ElseIf extensaoArquivo.EndsWith(".xls") OrElse extensaoArquivo.EndsWith(".xlsx") Then
                    Response.ContentType = "application/x-msexcel"
                Else
                    Response.ContentType = "application/octet-stream"
                End If

                Response.AddHeader("Content-Disposition", $"inline; filename={nomeArquivo}")
                Response.OutputStream.Write(byteImagem, 0, vDataSet.Tables(0).Rows(0).Item("Tamanho"))
                Response.End()
            End If
        End If
    End Sub
End Class
