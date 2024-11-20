Imports System.IO
Imports ClosedXML.Excel

Public Class Atualizar
    Inherits System.Web.UI.Page
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim oTraducao As New cls_Traducao
    Dim oConfig As New cls_Config
    Dim vDataSet As System.Data.DataSet
    Dim vBanco As String
    Dim vPasta As String
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Carga Atualizar - Visualização {Request("ID")}",
                                                        False)
        '-----criar pasta com o texto 'convert_' + o nome do banco (ex. convert_sms)
        vBanco = oTraducao.Descriptografar(Session("Conn_Banco"))
        vPasta = "Convert_" & Trim(Mid(vBanco, (vBanco.IndexOf("CATALOG=") + 9), (vBanco.IndexOf("USER") - (vBanco.IndexOf("CATALOG=") + 9))))

        '-----lista arquivos
        vDataSet = WS_Modulo.Arquivo_Pasta(Session("Conn_Banco"), "sp_Lista_Pasta", vPasta)
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As System.EventArgs) Handles btLimpar.Click
        '-----limpa a pasta
        vBanco = oTraducao.Descriptografar(Session("Conn_Banco"))
        vPasta = "Convert_" & Trim(Mid(vBanco, (vBanco.IndexOf("CATALOG=") + 9), (vBanco.IndexOf("USER") - (vBanco.IndexOf("CATALOG=") + 9))))

        vDataSet = WS_Modulo.Arquivo_Pasta(Session("Conn_Banco"), "sp_Limpar_Pasta", vPasta)
    End Sub



    Public Class User
        Public Nm_Consumimdor As String
        Public Id_Consumidor_Tipo As Integer
        Public Matricula As String
        Public Email As String
        Public Email_Copia As String
        Public Fl_Nao_Envia_Email As Integer
        Public Id_Empresa_Contratada As Integer
        Public Id_Cargo As Integer
        Public Id_Filial As Integer
        Public Id_Centro_Custo As Integer
        Public Id_Departamento As Integer
        Public Id_Setor As Integer
        Public Id_Secao As Integer
        Public Id_Consumidor_Status As Integer
        Public Matricula_Chefia As String
        Public Id_Usuario_Permissao As Integer
        Public Nm_Usuario As String
        Public senha As String
        Public id_Consumidor As Integer
        Public Id_Idioma As Integer
        Public Id_Usuario_Grupo As Integer
        Public Id_Usuario_Perfil As Integer
        Public Id_Usuario_Perfil_Acesso As Integer
        Public Incluir As Integer
        Public Alterar As Integer
        Public Excluir As Integer
        Public Detalhamento_Conta As Integer
        Public Detalhamento_Contato As Integer
        Public Fl_Desativado As Integer
    End Class

    Protected Sub btDownload_Click(sender As Object, e As EventArgs)

        btDownload.Attributes.Add("onclick", "alert('Aguarde o download do arquivo. Essa operação pode levar alguns minutos.');")

        Dim xlsxWorkbook As XLWorkbook = New XLWorkbook()

        Dim xlsSheet As IXLWorksheet = xlsxWorkbook.AddWorksheet("Usuarios")
        xlsSheet.Cell("A1").Value = "Id_Consumidor"
        xlsSheet.Cell("B1").Value = "Nm_Consumidor"
        xlsSheet.Cell("C1").Value = "Id_Consumidor_Tipo"
        xlsSheet.Cell("D1").Value = "Matricula"
        xlsSheet.Cell("E1").Value = "Email"
        xlsSheet.Cell("F1").Value = "Email_Copia"
        xlsSheet.Cell("G1").Value = "Fl_Nao_Envia_Email"
        xlsSheet.Cell("H1").Value = "Id_Empresa_Contratada"
        xlsSheet.Cell("I1").Value = "Id_Cargo"
        xlsSheet.Cell("J1").Value = "Id_Filial"
        xlsSheet.Cell("K1").Value = "Id_Centro_Custo"
        xlsSheet.Cell("L1").Value = "Id_Departamento"
        xlsSheet.Cell("M1").Value = "Id_Setor"
        xlsSheet.Cell("N1").Value = "Id_Secao"
        xlsSheet.Cell("O1").Value = "Id_Consumidor_Status"
        xlsSheet.Cell("P1").Value = "Matricula_Chefia"
        xlsSheet.Cell("Q1").Value = "Id_Usuario_Permissao"
        xlsSheet.Cell("R1").Value = "Nm_Usuario"
        xlsSheet.Cell("S1").Value = "senha"
        xlsSheet.Cell("T1").Value = "id_Consumidor"
        xlsSheet.Cell("U1").Value = "Id_Idioma"
        xlsSheet.Cell("V1").Value = "Id_Usuario_Grupo"
        xlsSheet.Cell("W1").Value = "Id_Usuario_Perfil"
        xlsSheet.Cell("X1").Value = "Id_Usuario_Perfil_Acesso"
        xlsSheet.Cell("Y1").Value = "Incluir"
        xlsSheet.Cell("Z1").Value = "Alterar"
        xlsSheet.Cell("AA1").Value = "Excluir"
        xlsSheet.Cell("AB1").Value = "Detalhamento_Conta"
        xlsSheet.Cell("AC1").Value = "Detalhamento_Contato"
        xlsSheet.Cell("AD1").Value = "Fl_Desativado"

        Dim dataset = WS_Modulo.ConsumidorUsuario(Session("Conn_Banco"),
                                                          "sp_Usuario_Consumidor",
                                                            False)
        For row As Integer = 0 To dataset.Tables(0).Rows.Count - 1

            xlsSheet.Cell($"A{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Consumidor")
            xlsSheet.Cell($"B{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Nm_Consumidor")
            xlsSheet.Cell($"C{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Consumidor_Tipo")
            xlsSheet.Cell($"D{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Matricula")
            xlsSheet.Cell($"E{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Email")
            xlsSheet.Cell($"F{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Email_Copia")
            xlsSheet.Cell($"G{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Fl_Nao_Envia_Email")
            xlsSheet.Cell($"H{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Empresa_Contratada")
            xlsSheet.Cell($"I{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Cargo")
            xlsSheet.Cell($"J{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Filial")
            xlsSheet.Cell($"K{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Centro_Custo")
            xlsSheet.Cell($"L{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Departamento")
            xlsSheet.Cell($"M{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Setor")
            xlsSheet.Cell($"N{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Secao")
            xlsSheet.Cell($"O{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Consumidor_Status")
            xlsSheet.Cell($"P{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Matricula_Chefia")
            xlsSheet.Cell($"Q{row + 2}").Value = 0
            xlsSheet.Cell($"R{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Nm_Usuario")
            xlsSheet.Cell($"S{row + 2}").Value = dataset.Tables(0).Rows(row).Item("senha")
            xlsSheet.Cell($"T{row + 2}").Value = dataset.Tables(0).Rows(row).Item("id_Consumidor")
            xlsSheet.Cell($"U{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Idioma")
            xlsSheet.Cell($"V{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Usuario_Grupo")
            xlsSheet.Cell($"W{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Usuario_Perfil")
            xlsSheet.Cell($"X{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Id_Usuario_Perfil_Acesso")
            xlsSheet.Cell($"Y{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Incluir")
            xlsSheet.Cell($"Z{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Alterar")
            xlsSheet.Cell($"AA{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Excluir")
            xlsSheet.Cell($"AB{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Detalhamento_Conta")
            xlsSheet.Cell($"AC{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Detalhamento_Contato")
            xlsSheet.Cell($"AD{row + 2}").Value = dataset.Tables(0).Rows(row).Item("Fl_Desativado")
        Next
        Dim fileName As String = $"UpdateUsuarios.xlsx"
        Dim path = Server.MapPath(String.Format("~/InsertTemplate1.xlsx"))
        xlsxWorkbook.SaveAs($"{path.Replace("InsertTemplate1.xlsx", "")}/{fileName}")
        Dim filePath As String = Server.MapPath(String.Format($"~/{fileName}"))

        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("Content-Disposition", ($"attachment; filename={fileName}"))

        Response.WriteFile(filePath)

        Response.Flush()
        Response.End()

        Kill(fileName)

    End Sub

    Protected Sub btEnviar_Click(sender As Object, e As EventArgs)
        Dim fileStream As Stream = File2.PostedFile.InputStream
        Dim workBook As XLWorkbook = New XLWorkbook(fileStream)
        Dim workSheet As IXLWorksheet = workBook.Worksheet(1)
        Dim rows As Integer = workSheet.FirstColumn().CellsUsed().Count()

        Dim notInserted = New List(Of Integer)
        For row As Integer = 2 To rows
            Dim user As New User()
            user.id_Consumidor = HandleNumericCells(workSheet.Cell(row, 1).Value)
            user.Nm_Consumimdor = workSheet.Cell(row, 2).Value.ToString()
            user.Id_Consumidor_Tipo = HandleNumericCells(workSheet.Cell(row, 3).Value)
            user.Matricula = workSheet.Cell(row, 4).Value.ToString()
            user.Email = workSheet.Cell(row, 5).Value.ToString()
            user.Email_Copia = workSheet.Cell(row, 6).Value.ToString()
            user.Fl_Nao_Envia_Email = HandleNumericCells(workSheet.Cell(row, 7).Value)
            user.Id_Empresa_Contratada = HandleNumericCells(workSheet.Cell(row, 8).Value)
            user.Id_Cargo = HandleNumericCells(workSheet.Cell(row, 9).Value)
            user.Id_Filial = HandleNumericCells(workSheet.Cell(row, 10).Value)
            user.Id_Centro_Custo = HandleNumericCells(workSheet.Cell(row, 11).Value)
            user.Id_Departamento = HandleNumericCells(workSheet.Cell(row, 12).Value)
            user.Id_Setor = HandleNumericCells(workSheet.Cell(row, 13).Value)
            user.Id_Secao = HandleNumericCells(workSheet.Cell(row, 14).Value)
            user.Id_Consumidor_Status = HandleNumericCells(workSheet.Cell(row, 15).Value)
            user.Matricula_Chefia = workSheet.Cell(row, 16).Value.ToString()
            user.Id_Usuario_Permissao = HandleNumericCells(workSheet.Cell(row, 17).Value)
            user.Nm_Usuario = workSheet.Cell(row, 18).Value.ToString()
            user.senha = workSheet.Cell(row, 19).Value.ToString()
            user.Id_Idioma = HandleNumericCells(workSheet.Cell(row, 21).Value)
            user.Id_Usuario_Grupo = HandleNumericCells(workSheet.Cell(row, 22).Value)
            user.Id_Usuario_Perfil = HandleNumericCells(workSheet.Cell(row, 23).Value)
            user.Id_Usuario_Perfil_Acesso = HandleNumericCells(workSheet.Cell(row, 24).Value)
            user.Incluir = HandleNumericCells(workSheet.Cell(row, 25).Value)
            user.Alterar = HandleNumericCells(workSheet.Cell(row, 26).Value)
            user.Excluir = HandleNumericCells(workSheet.Cell(row, 27).Value)
            user.Detalhamento_Conta = HandleNumericCells(workSheet.Cell(row, 28).Value)
            user.Detalhamento_Contato = HandleNumericCells(workSheet.Cell(row, 29).Value)
            user.Fl_Desativado = HandleNumericCells(workSheet.Cell(row, 30).Value)

            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Try
                Dim SalvarConsumidor = WS_Cadastro.InventarioLote(Session("Conn_Banco"),
                                                          user.id_Consumidor,
                                                          user.Nm_Consumimdor,
                                                          user.Id_Consumidor_Tipo,
                                                          user.Matricula,
                                                          user.Email,
                                                          user.Email_Copia,
                                                          user.Fl_Nao_Envia_Email,
                                                          user.Id_Empresa_Contratada,
                                                          user.Id_Cargo,
                                                          user.Id_Filial,
                                                          user.Id_Centro_Custo,
                                                          user.Id_Departamento,
                                                          user.Id_Setor,
                                                          user.Id_Secao,
                                                          user.Id_Consumidor_Status,
                                                          user.Matricula_Chefia,
                                                          user.Id_Usuario_Permissao,
                                                          Nothing,
                                                          user.Nm_Usuario,
                                                          Nothing,
                                                          user.Id_Idioma,
                                                          user.Id_Usuario_Grupo,
                                                          user.Id_Usuario_Perfil,
                                                          user.Id_Usuario_Perfil_Acesso,
                                                          user.Incluir,
                                                          user.Alterar,
                                                          user.Excluir,
                                                          user.Detalhamento_Conta,
                                                          user.Detalhamento_Contato,
                                                          user.Fl_Desativado,
                                                          "sp_SM",
                                                          False)
            Catch ex As Exception
                notInserted.Add(row)
                Continue For
            End Try
        Next

        If notInserted.Count > 0 Then
            lblNotImportedLinesInfo.Text = "Os usuários contidos nas linhas abaixo não foram atualizados. Verifique os dados contidos no arquivo e tente novamente."
            lblNotImportedLines.Text = $"Linha: {String.Join(",", notInserted)}"
        Else
            lblNotImportedLinesInfo.Text = "Lote de usuários atualizado com sucesso."
            lblNotImportedLines.Text = String.Empty
        End If
    End Sub

    Function HandleNumericCells(ByVal input As String)
        Dim output As Integer = Nothing
        If Not String.IsNullOrEmpty(input) Then
            Integer.TryParse(input, output)
        End If
        Return output
    End Function
End Class