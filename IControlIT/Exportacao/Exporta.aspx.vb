Imports ClosedXML.Extensions.ResponseExtensions
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports ClosedXML.Excel
Imports System.IO.Compression

Public Class Exporta
    Inherits Page

    'Dim vDataSet As DataSet
    Dim QuantidadeRegistrosPaginacao As Integer = 100000
    Dim TamanhoFonte As Integer = 11

    Public Function GetFieldValue(ByVal Row As System.Data.DataRow, ByVal FieldName As String) As String
        If Row.IsNull(FieldName) Then
            Return String.Empty
        Else
            Return CStr(Row(FieldName))
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim dataSet As DataSet = Session("DataSet")
        Dim quantidadeRegistros As Integer = dataSet.Tables(0).Rows.Count

        If Not Page.IsPostBack Then
            btExecutar.Attributes.Add("onclick", "alert('Aguarde o download do arquivo. Essa operação pode levar alguns minutos.');")

            'Dim minutosProcessamento As Integer = quantidadeRegistros / QuantidadeRegistrosPaginacao

            TamanhoFonteTextBox.Text = TamanhoFonte.ToString().Trim()
            QuantidadeRegistrosTextBox.Text = QuantidadeRegistrosPaginacao.ToString().Trim()
            btExecutar.Enabled = True
            cboExport.Enabled = True

            lblDescricao.Text = "Exportar: " & Request("Descricao")
            QuantidadeRegistrosLabel.Text = quantidadeRegistros & " registros retornados."
            If quantidadeRegistros > QuantidadeRegistrosPaginacao Then
                QuantidadeArquivosLabel.Text = "Será gerado um arquivo ZIP com " & (Math.Truncate(quantidadeRegistros / QuantidadeRegistrosPaginacao) + IIf(quantidadeRegistros Mod QuantidadeRegistrosPaginacao = 0, 0, 1)) & " arquivo(s)."
            End If

            ''If quantidadeRegistros > 1000 Then
            ''    TextoTempoProcessamentoLabel.Text = "Serão processados " + QuantidadeRegistrosPaginacao.ToString() + " registros por minuto."
            ''    PrevisaoTerminoLabel.Text = "Previsão para término: " & DateAdd(DateInterval.Minute, minutosProcessamento + 1, DateTime.Now)
            ''End If

            If Not Request("Tipo") = Nothing Then
                If Request("Tipo") <> 1 Then cboExport.Items.Item(0).Enabled = False
                If Request("Tipo") <> 4 Then cboExport.Items.Item(1).Enabled = False
                If Request("Tipo") <> 5 Then cboExport.Items.Item(2).Enabled = False
                ''If Request("Tipo") <> 4 Then cboExport.Items.Item(3).Enabled = False
                ''If Request("Tipo") <> 5 Then cboExport.Items.Item(4).Enabled = False
            End If

            For i = 0 To dataSet.Tables(0).Columns.Count - 1
                CamposCheckBoxList.Items.Add(dataSet.Tables(0).Columns(i).ColumnName)
            Next

            For Each item In CamposCheckBoxList.Items
                item.Selected = True
            Next

            '-----quando vazio captura cabecario do dataset
            'hdfCampo.Value = Request("Campo")

            'If hdfCampo.Value = Nothing Then
            '    Dim vCampo As System.String = Nothing
            '    Dim i As System.Int32
            '    For i = 0 To dataSet.Tables(0).Columns.Count - 1
            '        vCampo = vCampo & dataSet.Tables(0).Columns(i).ColumnName & ";"
            '    Next
            '    Dim Campo As System.String = Mid(vCampo, 1, Len(vCampo) - 1)

            '    hdfCampo.Value = Campo
            'End If

            'dtg.DataSource = Session("DataSet")
            'dtg.DataBind()

        Else
            Dim paginacao As Integer = QuantidadeRegistrosPaginacao
            Dim fonte As Integer = TamanhoFonte

            If Integer.TryParse(TamanhoFonteTextBox.Text, fonte) Then
                If fonte < 8 Or fonte > 16 Then
                    fonte = TamanhoFonte
                Else
                    TamanhoFonte = fonte
                End If
            End If

            If Integer.TryParse(QuantidadeRegistrosTextBox.Text, paginacao) Then
                If paginacao < 5000 Or paginacao > 500000 Then
                    paginacao = QuantidadeRegistrosPaginacao
                Else
                    QuantidadeRegistrosPaginacao = paginacao
                End If
            End If

            If quantidadeRegistros > QuantidadeRegistrosPaginacao Then
                QuantidadeArquivosLabel.Text = "Aguarde... Gerando um arquivo ZIP com " & (Math.Truncate(quantidadeRegistros / QuantidadeRegistrosPaginacao) + IIf(quantidadeRegistros Mod QuantidadeRegistrosPaginacao = 0, 0, 1)) & " arquivo(s)."
            End If
        End If

    End Sub

    Public Sub ExportarHTML(ByRef vfile As String)
        Dim stringBuilder As New StringBuilder
        Dim valorCampo As String
        Dim dataSet As DataSet = Session("DataSet")
        Dim gerarCabecalho As Boolean = True
        Dim quantidadeRegistros As Integer = dataSet.Tables(0).Rows.Count
        Dim multiplosArquivos As Boolean = quantidadeRegistros > QuantidadeRegistrosPaginacao
        Dim proximoArquivo As Integer = 1
        Dim quantidadeArquivos As Integer = Math.Truncate(quantidadeRegistros / QuantidadeRegistrosPaginacao) + IIf(quantidadeRegistros Mod QuantidadeRegistrosPaginacao = 0, 0, 1)
        Dim proximaPagina As Integer = QuantidadeRegistrosPaginacao
        Dim guidNovaPasta As Guid = Guid.NewGuid()

        For i = 0 To quantidadeRegistros - 1

            If gerarCabecalho Then
                stringBuilder.Append("<HTML><HEAD><meta http-equiv='Content-Type' content='text/html; charset=utf-8'>")
                stringBuilder.Append("<STYLE type='text/css'> table, th, td {border: 1px solid black; border-collapse: collapse;}</STYLE></HEAD><BODY>")
                stringBuilder.Append("<TABLE style='font-size: " & TamanhoFonte & "px;'>")
                stringBuilder.Append("<TR>")
                For Each item In CamposCheckBoxList.Items
                    If item.Selected Then
                        stringBuilder.Append("<TD bgcolor='Silver'><b>")
                        stringBuilder.Append(item.Text)
                        stringBuilder.Append("</b></TD>")
                    End If
                Next
                stringBuilder.Append("</TR>")
                gerarCabecalho = False
            End If

            stringBuilder.Append("<TR>")
            For Each item In CamposCheckBoxList.Items
                If item.Selected Then
                    valorCampo = GetFieldValue(dataSet.Tables(0).Rows(i), item.Text)
                    stringBuilder.Append("<TD valign='top'>" & valorCampo & "</TD>")
                End If
            Next
            stringBuilder.Append("</TR>")

            If multiplosArquivos AndAlso i > proximaPagina Then
                stringBuilder.Append("</TABLE></BODY></HTML>")

                Dim caminho As String = "~\App_Data\Relatorios\" & guidNovaPasta.ToString()
                Dim nomeArquivo As String = proximoArquivo.ToString("D3") & "-" & quantidadeArquivos.ToString("D3") & "_" & vfile

                Directory.CreateDirectory(Server.MapPath(caminho))
                File.WriteAllText(Server.MapPath(caminho & "\" & nomeArquivo), stringBuilder.ToString())

                proximoArquivo += 1
                proximaPagina += QuantidadeRegistrosPaginacao
                gerarCabecalho = True
                stringBuilder.Clear()
            End If

        Next

        stringBuilder.Append("</TABLE>")
        stringBuilder.Append("</BODY></HTML>")

        If multiplosArquivos Then
            Dim caminho As String = "~\App_Data\Relatorios\" & guidNovaPasta.ToString()
            Dim nomeArquivo As String = proximoArquivo.ToString("D3") & "-" & quantidadeArquivos.ToString("D3") & "_" & vfile

            Directory.CreateDirectory(Server.MapPath(caminho))
            File.WriteAllText(Server.MapPath(caminho & "\" & nomeArquivo), stringBuilder.ToString(), Encoding.UTF8)

            stringBuilder.Clear()

            ZipFile.CreateFromDirectory(Server.MapPath(caminho), Server.MapPath(caminho & ".zip"))

            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/x-zip-compressedd"
            HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" & vfile.Replace(".html", ".zip"))
            HttpContext.Current.Response.WriteFile(Server.MapPath(caminho & ".zip"))
            HttpContext.Current.Response.End()
        Else
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/vnd.ms-notepad"
            HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" & vfile & "")
            HttpContext.Current.Response.Charset = ""
            HttpContext.Current.Response.Write(stringBuilder.ToString)
            HttpContext.Current.Response.End()
        End If

    End Sub

    Public Sub ExportarCSV(ByRef vfile As String)
        Dim stringBuilder As New StringBuilder
        Dim valorCampo As String
        Dim dataSet As DataSet = Session("DataSet")
        Dim gerarCabecalho As Boolean = True
        Dim quantidadeRegistros As Integer = dataSet.Tables(0).Rows.Count
        Dim multiplosArquivos As Boolean = quantidadeRegistros > QuantidadeRegistrosPaginacao
        Dim proximoArquivo As Integer = 1
        Dim quantidadeArquivos As Integer = Math.Truncate(quantidadeRegistros / QuantidadeRegistrosPaginacao) + IIf(quantidadeRegistros Mod QuantidadeRegistrosPaginacao = 0, 0, 1)
        Dim proximaPagina As Integer = QuantidadeRegistrosPaginacao
        Dim guidNovaPasta As Guid = Guid.NewGuid()

        For i = 0 To quantidadeRegistros - 1

            If gerarCabecalho Then
                For Each item In CamposCheckBoxList.Items
                    If item.Selected Then
                        stringBuilder.Append(item.Text & ";")
                    End If
                Next
                stringBuilder.AppendLine()
                gerarCabecalho = False
            End If

            For Each item In CamposCheckBoxList.Items
                If item.Selected Then
                    valorCampo = GetFieldValue(dataSet.Tables(0).Rows(i), item.Text)
                    stringBuilder.Append(valorCampo & ";")
                End If
            Next
            stringBuilder.AppendLine()

            If multiplosArquivos AndAlso i > proximaPagina Then
                Dim caminho As String = "~\App_Data\Relatorios\" & guidNovaPasta.ToString()
                Dim nomeArquivo As String = proximoArquivo.ToString("D3") & "-" & quantidadeArquivos.ToString("D3") & "_" & vfile

                Directory.CreateDirectory(Server.MapPath(caminho))
                File.WriteAllText(Server.MapPath(caminho & "\" & nomeArquivo), stringBuilder.ToString(), Encoding.UTF8)

                proximoArquivo += 1
                proximaPagina += QuantidadeRegistrosPaginacao
                gerarCabecalho = True
                stringBuilder.Clear()
            End If
        Next

        If multiplosArquivos Then
            Dim caminho As String = "~\App_Data\Relatorios\" & guidNovaPasta.ToString()
            Dim nomeArquivo As String = proximoArquivo.ToString("D3") & "-" & quantidadeArquivos.ToString("D3") & "_" & vfile

            Directory.CreateDirectory(Server.MapPath(caminho))
            File.WriteAllText(Server.MapPath(caminho & "\" & nomeArquivo), stringBuilder.ToString(), Encoding.UTF8)

            stringBuilder.Clear()

            ZipFile.CreateFromDirectory(Server.MapPath(caminho), Server.MapPath(caminho & ".zip"))

            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/x-zip-compressedd"
            HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" & vfile.Replace(".csv", ".zip"))
            HttpContext.Current.Response.WriteFile(Server.MapPath(caminho & ".zip"))
            HttpContext.Current.Response.End()
        Else
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/vnd.ms-notepad"
            HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" & vfile & "")
            HttpContext.Current.Response.Charset = Encoding.UTF8.WebName
            HttpContext.Current.Response.Write(stringBuilder.ToString)
            HttpContext.Current.Response.End()
        End If

    End Sub

    Public Sub ExportarXLSX(ByRef vfile As String)
        Dim valorCampo As String
        Dim dataSet As DataSet = Session("DataSet")
        Dim gerarCabecalho As Boolean = True
        Dim quantidadeRegistros As Integer = dataSet.Tables(0).Rows.Count
        Dim multiplosArquivos As Boolean = quantidadeRegistros > QuantidadeRegistrosPaginacao
        Dim proximoArquivo As Integer = 1
        Dim quantidadeArquivos As Integer = Math.Truncate(quantidadeRegistros / QuantidadeRegistrosPaginacao) + IIf(quantidadeRegistros Mod QuantidadeRegistrosPaginacao = 0, 0, 1)
        Dim proximaPagina As Integer = QuantidadeRegistrosPaginacao
        Dim guidNovaPasta As Guid = Guid.NewGuid()

        Dim workbook As New XLWorkbook(XLEventTracking.Disabled)
        Dim planilha = workbook.Worksheets.Add("Planilha Exportada")

        Dim linha As Integer = 1
        For i = 0 To quantidadeRegistros - 1

            Dim letraColuna As Integer
            Dim repeticoesLetras As Integer

            If gerarCabecalho Then
                letraColuna = 65
                repeticoesLetras = 1
                linha = 1
                For Each item In CamposCheckBoxList.Items
                    If item.Selected Then
                        planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & "1").Value = item.Text
                        letraColuna += 1
                        If letraColuna > 90 Then
                            letraColuna = 65
                            repeticoesLetras += 1
                        End If
                    End If
                Next
                gerarCabecalho = False
            End If

            letraColuna = 65
            repeticoesLetras = 1
            linha += 1
            For Each item In CamposCheckBoxList.Items
                If item.Selected Then
                    valorCampo = GetFieldValue(dataSet.Tables(0).Rows(i), item.Text)
                    Dim campo As DataColumn = dataSet.Tables(0).Columns(item.Text)

                    Dim tipoDadosTexto As Boolean = True

                    If campo.DataType = Type.GetType("System.Decimal") Then
                        Dim valorDecimal As Decimal
                        If Decimal.TryParse(valorCampo, valorDecimal) Then
                            planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & linha).DataType = XLDataType.Number
                            planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & linha).Value = valorDecimal
                            tipoDadosTexto = False
                        End If
                    End If

                    If campo.DataType = Type.GetType("System.Int32") Then
                        Dim valorInteiro As Integer
                        If Integer.TryParse(valorCampo, valorInteiro) Then
                            planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & linha).DataType = XLDataType.Number
                            planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & linha).Value = valorInteiro
                            tipoDadosTexto = False
                        End If
                    End If

                    If campo.DataType = Type.GetType("System.DateTime") Then
                        Dim valorData As DateTime
                        If DateTime.TryParse(valorCampo, valorData) Then
                            planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & linha).DataType = XLDataType.DateTime
                            planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & linha).Value = valorData
                            tipoDadosTexto = False
                        End If
                    End If

                    If campo.DataType = Type.GetType("System.TimeSpan") Then
                        Dim valorHora As TimeSpan
                        If TimeSpan.TryParse(valorCampo, valorHora) Then
                            planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & linha).DataType = XLDataType.TimeSpan
                            planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & linha).Value = valorHora
                            tipoDadosTexto = False
                        End If
                    End If

                    If campo.DataType = Type.GetType("System.Boolean") Then
                        Dim valorLogico As Boolean
                        If Boolean.TryParse(valorCampo, valorLogico) Then
                            planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & linha).DataType = XLDataType.Boolean
                            planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & linha).Value = valorLogico
                            tipoDadosTexto = False
                        End If
                    End If

                    If tipoDadosTexto Then
                        planilha.Cell(StrDup(repeticoesLetras, Chr(letraColuna)) & linha).Value = valorCampo
                    End If

                    letraColuna += 1
                    If letraColuna > 90 Then
                        letraColuna = 65
                        repeticoesLetras += 1
                    End If
                End If
            Next

            If multiplosArquivos AndAlso i > proximaPagina Then
                Dim caminho As String = "~\App_Data\Relatorios\" & guidNovaPasta.ToString()
                Dim nomeArquivo As String = proximoArquivo.ToString("D3") & "-" & quantidadeArquivos.ToString("D3") & "_" & vfile

                Directory.CreateDirectory(Server.MapPath(caminho))

                workbook.SaveAs(Server.MapPath(caminho & "\" & nomeArquivo))
                planilha = Nothing
                workbook = New XLWorkbook(XLEventTracking.Disabled)
                planilha = workbook.Worksheets.Add("Planilha Exportada")

                proximoArquivo += 1
                proximaPagina += QuantidadeRegistrosPaginacao
                gerarCabecalho = True
            End If
        Next

        If multiplosArquivos Then
            Dim caminho As String = "~\App_Data\Relatorios\" & guidNovaPasta.ToString()
            Dim nomeArquivo As String = proximoArquivo.ToString("D3") & "-" & quantidadeArquivos.ToString("D3") & "_" & vfile

            Directory.CreateDirectory(Server.MapPath(caminho))
            workbook.SaveAs(Server.MapPath(caminho & "\" & nomeArquivo))

            ZipFile.CreateFromDirectory(Server.MapPath(caminho), Server.MapPath(caminho & ".zip"))

            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/x-zip-compressedd"
            HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" & vfile.Replace(".xlsx", ".zip"))
            HttpContext.Current.Response.WriteFile(Server.MapPath(caminho & ".zip"))
            HttpContext.Current.Response.End()
        Else
            HttpContext.Current.Response.Clear()
            'HttpContext.Current.Response.ContentType = "application/vnd.ms-notepad"
            'HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" & vfile & "")
            'HttpContext.Current.Response.Charset = ""
            'HttpContext.Current.Response.Write(result.ToString)
            HttpContext.Current.Response.DeliverWorkbook(workbook, vfile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            HttpContext.Current.Response.End()
        End If

        workbook.Dispose()

    End Sub

    Public Sub ExportarPDF(ByRef vfile As String)
        Dim stringBuilder As New StringBuilder
        Dim valorCampo As String
        Dim dataSet As DataSet = Session("DataSet")
        Dim novoArquivo As Boolean = True
        Dim quantidadeRegistros As Integer = dataSet.Tables(0).Rows.Count
        Dim multiplosArquivos As Boolean = quantidadeRegistros > QuantidadeRegistrosPaginacao
        Dim proximoArquivo As Integer = 1
        Dim quantidadeArquivos As Integer = Math.Truncate(quantidadeRegistros / QuantidadeRegistrosPaginacao) + IIf(quantidadeRegistros Mod QuantidadeRegistrosPaginacao = 0, 0, 1)
        Dim proximaPagina As Integer = QuantidadeRegistrosPaginacao
        Dim guidNovaPasta As Guid = Guid.NewGuid()

        Dim orientacaoPaisagem = pdfPaisagemCheckbox.Checked

        For i = 0 To quantidadeRegistros - 1

            If novoArquivo Then
                stringBuilder.Append("<HTML><HEAD><meta http-equiv='Content-Type' content='text/html; charset=utf-8'>")
                stringBuilder.Append("</HEAD><BODY>")
                stringBuilder.Append("<TABLE border='1' style='font-size: " & TamanhoFonte & "px;'>")
                stringBuilder.Append("<TR>")
                For Each item In CamposCheckBoxList.Items
                    If item.Selected Then
                        stringBuilder.Append("<TD bgcolor='Silver'><b>")
                        stringBuilder.Append(item.Text)
                        stringBuilder.Append("</b></TD>")
                    End If
                Next
                stringBuilder.Append("</TR>")
                novoArquivo = False
            End If

            stringBuilder.Append("<TR>")
            For Each item In CamposCheckBoxList.Items
                If item.Selected Then
                    valorCampo = GetFieldValue(dataSet.Tables(0).Rows(i), item.Text)
                    stringBuilder.Append("<TD valign='top' style='border: 1px solid black; border-collapse: collapse;'>" & valorCampo & "</TD>")
                End If
            Next
            stringBuilder.Append("</TR>")

            If multiplosArquivos AndAlso i > proximaPagina Then
                stringBuilder.Append("</TABLE></BODY></HTML>")

                Dim caminho As String = "~\App_Data\Relatorios\" & guidNovaPasta.ToString()
                Dim nomeArquivo As String = proximoArquivo.ToString("D3") & "-" & quantidadeArquivos.ToString("D3") & "_" & vfile

                Directory.CreateDirectory(Server.MapPath(caminho))

                Dim stringReader As New StringReader(stringBuilder.ToString())
                Dim pdfDocument As New Document(IIf(orientacaoPaisagem, PageSize.A4.Rotate(), PageSize.A4), 10.0F, 10.0F, 10.0F, 0F)
                Dim htmlWorker As New HTMLWorker(pdfDocument)
                Using memoryStream As New MemoryStream()
                    Dim writer = PdfWriter.GetInstance(pdfDocument, memoryStream)
                    pdfDocument.Open()
                    htmlWorker.Parse(stringReader)
                    pdfDocument.Close()

                    Dim bytes = memoryStream.ToArray()
                    memoryStream.Close()

                    File.WriteAllBytes(Server.MapPath(caminho & "\" & nomeArquivo), bytes)
                End Using

                proximoArquivo += 1
                proximaPagina += QuantidadeRegistrosPaginacao
                novoArquivo = True
                stringBuilder.Clear()
            End If

        Next

        stringBuilder.Append("</TABLE>")
        stringBuilder.Append("</BODY></HTML>")

        If multiplosArquivos Then
            Dim caminho As String = "~\App_Data\Relatorios\" & guidNovaPasta.ToString()
            Dim nomeArquivo As String = proximoArquivo.ToString("D3") & "-" & quantidadeArquivos.ToString("D3") & "_" & vfile

            Directory.CreateDirectory(Server.MapPath(caminho))

            Dim stringReader As New StringReader(stringBuilder.ToString())
            Dim pdfDocument As New Document(IIf(orientacaoPaisagem, PageSize.A4.Rotate(), PageSize.A4), 10.0F, 10.0F, 10.0F, 0F)
            Dim htmlWorker As New HTMLWorker(pdfDocument)
            Using memoryStream As New MemoryStream()
                Dim writer = PdfWriter.GetInstance(pdfDocument, memoryStream)
                pdfDocument.Open()
                htmlWorker.Parse(stringReader)
                pdfDocument.Close()

                Dim bytes = memoryStream.ToArray()
                memoryStream.Close()

                File.WriteAllBytes(Server.MapPath(caminho & "\" & nomeArquivo), bytes)
            End Using
            stringBuilder.Clear()

            ZipFile.CreateFromDirectory(Server.MapPath(caminho), Server.MapPath(caminho & ".zip"))

            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/x-zip-compressedd"
            HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" & vfile.Replace(".pdf", ".zip"))
            HttpContext.Current.Response.WriteFile(Server.MapPath(caminho & ".zip"))
            HttpContext.Current.Response.End()
        Else
            Dim stringReader As New StringReader(stringBuilder.ToString())
            Dim pdfDocument As New Document(IIf(orientacaoPaisagem, PageSize.A4.Rotate(), PageSize.A4), 10.0F, 10.0F, 10.0F, 0F)
            Dim htmlWorker As New HTMLWorker(pdfDocument)
            Using memoryStream As New MemoryStream()
                Dim writer = PdfWriter.GetInstance(pdfDocument, memoryStream)
                pdfDocument.Open()
                htmlWorker.Parse(stringReader)
                pdfDocument.Close()

                Dim bytes = memoryStream.ToArray()
                memoryStream.Close()

                HttpContext.Current.Response.Clear()
                HttpContext.Current.Response.ContentType = "application/pdf"
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" & vfile)
                HttpContext.Current.Response.Buffer = True
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
                HttpContext.Current.Response.BinaryWrite(bytes)
                HttpContext.Current.Response.End()
                HttpContext.Current.Response.Close()
            End Using
            stringBuilder.Clear()
        End If

    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As EventArgs) Handles btExecutar.Click
        If Session("DataSet") Is Nothing Then Exit Sub
        If cboExport.SelectedValue = 0 Then Exit Sub

        Dim selecionado As Boolean = False
        For Each item In CamposCheckBoxList.Items
            If item.Selected Then selecionado = True
        Next
        If Not selecionado Then Exit Sub

        'dtg.DataSource = Session("DataSet")
        'dtg.DataBind()

        If cboExport.SelectedValue = 1 Then Call ExportarXLSX(IIf(Request("Descricao") = Nothing, "Export.xlsx", Request("Descricao") & ".xlsx"))
        If cboExport.SelectedValue = 2 Then Call ExportarPDF(IIf(Request("Descricao") = Nothing, "Export.pdf", Request("Descricao") & ".pdf"))
        If cboExport.SelectedValue = 3 Then Call ExportarHTML(IIf(Request("Descricao") = Nothing, "Export.html", Request("Descricao") & ".html"))
        If cboExport.SelectedValue = 4 Then Call ExportarCSV(IIf(Request("Descricao") = Nothing, "Export.csv", Request("Descricao") & ".csv"))
        'If cboExport.SelectedValue = 2 Then Call ExportarText(IIf(Request("Descricao") = Nothing, "Export.doc", Request("Descricao") & ".doc"))
    End Sub
End Class
