Imports System.IO
Imports DocumentFormat.OpenXml.Math
Imports IControlIT.WS_GUA_Manutencao
Imports IControlIT.WS_GUA_Rateio
Public Class Importa_Multi
    Inherits System.Web.UI.Page
    Dim WS_Rateio As New WS_GUA_Rateio.WSRateio
    Dim WS_Manutencao As New WS_GUA_Manutencao.WSManutencao

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FileUpload1.Attributes("multiple") = "multiple"
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
    End Sub

    Protected Sub Upload(sender As Object, e As EventArgs)
        Dim vNomeArquivo As FaturaInfo
        Dim vExisteArquivo As Integer = 0
        Dim vIdFatura As Integer = 0
        btnUpload.Enabled = False

        '----- importar lista de contas

        'Dim vDataSet As New System.Data.DataSet
        'vDataSet = WS_Rateio.Plano_Conta(Session("Conn_Banco"),
        '                                                Nothing,
        '                                                Nothing,
        '                                                Nothing,
        '                                                Nothing,
        '                                                Nothing,
        '                                                Nothing,
        '                                                Nothing,
        '                                                Nothing,
        '                                                Request("pDataLote"), 'cboDataLote.SelectedValue,
        '                                                Nothing,
        '                                                Request("pServico"),'cboServico.SelectedValue,
        '                                                "sp_Confere_Plano_Conta_V3",
        '                                                True)

        '----- processo para importar dados
        For i As Integer = 0 To Request.Files.Count - 1
            Dim postedFile As HttpPostedFile = Request.Files(i)
            If postedFile.ContentLength > 0 Then
                Dim fileName As String = Path.GetFileName(postedFile.FileName)
                '-----postedFile.SaveAs(Server.MapPath("~/Uploads/") & fileName)

                Dim newRow As New HtmlTableRow()
                Dim textCell As New HtmlTableCell()
                Dim iconCell As New HtmlTableCell()

                vNomeArquivo = SepararFatura(fileName)
                Dim tabelaRegistro = VerificarTipoTabelaRegistro(fileName)
                '----- recuperar o id da fatura para salvar o arquivo => vExisteArquivo
                vExisteArquivo = ExisteValorNoDataGrid(vNomeArquivo.NomeDaFatura)

                If vExisteArquivo > 0 AndAlso tabelaRegistro.Length > 0 Then
                    '----- recuperar o caminho completo do arquivo
                    'Dim caminhoArquivo As String = lblCaminho.Text + "\" + lstFiles.Items(i).ToString()
                    '----- converter o arquivo em base64
                    'If File.Exists(caminhoArquivo) Then
                    ' Lê o arquivo e converte seu conteúdo para Base64
                    'Dim byteImagem As Byte() = File.ReadAllBytes(caminhoArquivo)
                    'Dim nomeArquivoPdf = lstFiles.Items(i).ToString()
                    'Dim byteImagem As Byte() = New Byte(intTamanho) {}
                    'postedFile.InputStream.Read(byteImagem, 0, intTamanho)

                    'Dim byteImagem() As Byte = File.ReadAllBytes(postedFile.FileName)
                    Dim intTamanho As Int64 = System.Convert.ToInt32(postedFile.InputStream.Length)
                    postedFile.SaveAs(Server.MapPath("~/Uploads/") & fileName)
                    Dim byteImagem As Byte() = File.ReadAllBytes(Server.MapPath("~/Uploads/") & fileName)


                    Dim idRegistroTabela = vExisteArquivo
                    Dim Tamanho = byteImagem.Count
                    Dim download = 0

                    '----- registra a insersão do arquivo no banco 
                    Dim id_retorno As Data.DataSet
                    id_retorno = WS_Manutencao.ArquivoPDF(Session("Conn_Banco"),
                                                Nothing,
                                                fileName, 'nomeArquivo,
                                                tabelaRegistro, 'Request("pTabela"),
                                                idRegistroTabela, 'Request("pRegistro"),
                                                Tamanho, 'intTamanho,
                                                byteImagem, 'byteImagem,
                                                Session("Id_Usuario"),
                                                "sp_SM",
                                                False)

                    iconCell.InnerHtml = "<i class='fas fa-check text-success'></i>"
                    'Else
                    '    iconCell.InnerHtml = "<i class='fas fa-times text-danger'></i>"
                    'End If
                Else
                    iconCell.InnerHtml = "<i class='fas fa-times text-danger'></i>"
                End If

                textCell.InnerText = fileName

                newRow.Cells.Add(textCell)
                newRow.Cells.Add(iconCell)

                tblLoteFaturas.Rows.Add(newRow)

                If tblLoteFaturas.Rows.Count * 30 > 600 Then ' Supondo que cada linha tem uma altura média de 30px
                    tblLoteFaturas.Attributes("style") = "max-height: 600px; overflow-y: auto;"
                End If
                RemoveUploadedFile(fileName)
                ' lblMessage.Text += String.Format("<b>{0}</b> Processado.<br />", fileName)
            End If
        Next
    End Sub

    'Método que remove o arquivo usado para converter o byteImage
    Public Sub RemoveUploadedFile(fileName As String)
        ' Mapeia o caminho completo do arquivo
        Dim filePath As String = Server.MapPath("~/Uploads/") & fileName

        ' Verifica se o arquivo existe
        If File.Exists(filePath) Then
            ' Remove o arquivo
            File.Delete(filePath)
            ' Opcional: exibe uma mensagem de confirmação
            ' Response.Write("Arquivo removido com sucesso.")
        Else
            ' Opcional: exibe uma mensagem caso o arquivo não seja encontrado
            ' Response.Write("Arquivo não encontrado.")
        End If
    End Sub

    ' Método para verificar se existe um fatura com o nome do arquivo
    Private Function ExisteValorNoDataGrid(valor As String) As Integer
        Dim existeArquivo As Integer = 0
        Dim dt As Data.DataSet = Session("DataSet")
        For Each table As DataTable In dt.Tables
            For Each row As DataRow In table.Rows
                If (row("Fatura") = valor) Then
                    existeArquivo = UpFaturaLote_Get_Id_Fatura(valor)
                    Return existeArquivo
                End If
            Next
        Next
        Return existeArquivo
    End Function

    '----- Método que recupera o id da fatura
    Private Function UpFaturaLote_Get_Id_Fatura(fatura As String) As Integer
        Dim vDataSet As New System.Data.DataSet
        Dim buffer As Byte()

        vDataSet = WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                                        Nothing,
                                                        fatura, '(valor fatura)
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Request("pDataLote"), 'cboDataLote.SelectedValue,
                                                        Nothing,
                                                         Request("pServico"),'cboServico.SelectedValue,
                                                        "sp_Retorna_Id_Dw",
                                                        True)
        If vDataSet.Tables.Count = 0 OrElse vDataSet.Tables(0).Rows.Count = 0 Then
            Return 0
        End If

        Return Convert.ToInt32(vDataSet.Tables(0).Rows(0)("Id_Fatura"))
    End Function
    '----- Método que separa a nomenclatura do nome do arquivo
    Public Function SepararFatura(nomeDeArquivo As String) As FaturaInfo
        Dim nomeFatura As String = ""
        Dim tipoFatura As String = ""

        ' Verifica se o nome do arquivo segue o padrão esperado
        If nomeDeArquivo.Contains("_") AndAlso nomeDeArquivo.Contains(".") Then
            Dim partesNome As String() = nomeDeArquivo.Split("_"c)
            If partesNome.Length >= 2 Then
                nomeFatura = partesNome(0)

                Dim partesTipo As String() = partesNome(1).Split("."c)
                If partesTipo.Length >= 2 Then
                    tipoFatura = partesTipo(0)
                End If
            End If
        End If

        ' Retorna as informações da fatura
        Return New FaturaInfo With {
        .NomeDaFatura = nomeFatura,
        .TipoDaFatura = tipoFatura
    }
    End Function
    '----- Método que separa o tipo de arquivo
    Function VerificarTipoTabelaRegistro(ByVal input As String) As String
        ' Remove everything after the first dot (.)
        Dim dotIndex As Integer = input.IndexOf("."c)
        If dotIndex >= 0 Then
            input = input.Substring(0, dotIndex)
        End If

        ' Remove everything after the first space
        Dim spaceIndex As Integer = input.IndexOf(" "c)
        If spaceIndex >= 0 Then
            input = input.Substring(0, spaceIndex)
        End If

        ' Split the string using the underline (_) as the delimiter
        Dim result As String() = input.Split("_"c)
        If result.Length > 1 AndAlso result(1) = "NF" Then
            Return "Nota_Fiscal"
        ElseIf result.Length > 1 AndAlso result(1) = "BOL" Then
            Return "Boleto"
        ElseIf result.Length > 1 AndAlso result(1) = "DET" Then
            Return "Fatura"
        Else
            Return ""
        End If
    End Function
    'Public Function VerificarTipoTabelaRegistro(ByVal texto As String) As String

    '    Dim result As String() = texto.Split(" ", StringSplitOptions.None)

    '    ' Expressões regulares para verificar a presença das palavras-chave
    '    Dim regexBoleto As New Regex("\bBOL\b", RegexOptions.IgnoreCase)
    '    'Dim regexNotaFiscal As New Regex("\bNF\b", RegexOptions.IgnoreCase)
    '    Dim regexFatura As New Regex("\bDET\b", RegexOptions.IgnoreCase)

    '    ' Verifica se a string contém alguma das palavras-chave
    '    If regexBoleto.IsMatch(texto) Then
    '        Return "Boleto"
    '    ElseIf regexFatura.IsMatch(texto) Then
    '        Return "Fatura"
    '        'ElseIf regexNotaFiscal.IsMatch(texto) Then
    '        '    Return "Nota_Fiscal"
    '    Else
    '        Return "Nota_Fiscal"
    '    End If
    'End Function

End Class