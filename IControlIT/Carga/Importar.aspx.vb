Imports System.IO
Imports ClosedXML.Excel

Public Class Importar
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
                                                        $"Tela Carga Importar - Visualização",
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

    Protected Sub btEnviar_Click(sender As Object, e As EventArgs)
        Dim fileStream As Stream = File1.PostedFile.InputStream
        Dim workBook As XLWorkbook = New XLWorkbook(fileStream)
        Dim workSheet As IXLWorksheet = workBook.Worksheet(1)
        Dim rows As Integer = workSheet.FirstColumn().CellsUsed().Count()

        Dim notInserted = New List(Of Integer)
        For row As Integer = 2 To rows
            Dim user As New User()
            user.Nm_Consumimdor = workSheet.Cell(row, 1).Value.ToString()
            user.Id_Consumidor_Tipo = HandleNumericCells(workSheet.Cell(row, 2).Value)
            user.Matricula = workSheet.Cell(row, 3).Value.ToString()
            user.Email = workSheet.Cell(row, 4).Value.ToString()
            user.Email_Copia = workSheet.Cell(row, 5).Value.ToString()
            user.Fl_Nao_Envia_Email = HandleNumericCells(workSheet.Cell(row, 6).Value)
            user.Id_Empresa_Contratada = HandleNumericCells(workSheet.Cell(row, 7).Value)
            user.Id_Cargo = HandleNumericCells(workSheet.Cell(row, 8).Value)
            user.Id_Filial = HandleNumericCells(workSheet.Cell(row, 9).Value)
            user.Id_Centro_Custo = HandleNumericCells(workSheet.Cell(row, 10).Value)
            user.Id_Departamento = HandleNumericCells(workSheet.Cell(row, 11).Value)
            user.Id_Setor = HandleNumericCells(workSheet.Cell(row, 12).Value)
            user.Id_Secao = HandleNumericCells(workSheet.Cell(row, 13).Value)
            user.Id_Consumidor_Status = HandleNumericCells(workSheet.Cell(row, 14).Value)
            user.Matricula_Chefia = workSheet.Cell(row, 15).Value.ToString()
            user.Id_Usuario_Permissao = HandleNumericCells(workSheet.Cell(row, 16).Value)
            user.Nm_Usuario = workSheet.Cell(row, 17).Value.ToString()
            user.senha = workSheet.Cell(row, 18).Value.ToString()
            user.id_Consumidor = HandleNumericCells(workSheet.Cell(row, 19).Value)
            user.Id_Idioma = HandleNumericCells(workSheet.Cell(row, 20).Value)
            user.Id_Usuario_Grupo = HandleNumericCells(workSheet.Cell(row, 21).Value)
            user.Id_Usuario_Perfil = HandleNumericCells(workSheet.Cell(row, 22).Value)
            user.Id_Usuario_Perfil_Acesso = HandleNumericCells(workSheet.Cell(row, 23).Value)
            user.Incluir = HandleNumericCells(workSheet.Cell(row, 24).Value)
            user.Alterar = HandleNumericCells(workSheet.Cell(row, 25).Value)
            user.Excluir = HandleNumericCells(workSheet.Cell(row, 26).Value)
            user.Detalhamento_Conta = HandleNumericCells(workSheet.Cell(row, 27).Value)
            user.Detalhamento_Contato = HandleNumericCells(workSheet.Cell(row, 28).Value)
            user.Fl_Desativado = HandleNumericCells(workSheet.Cell(row, 29).Value)

            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Try
                Dim SalvarConsumidor = WS_Cadastro.InventarioLote(Session("Conn_Banco"),
                                                          Nothing,
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
            lblNotImportedLinesInfo.Text = "Os usuários contidos nas linhas abaixo não foram importados. Verifique os dados contidos no arquivo e tente novamente."
            lblNotImportedLines.Text = $"Linha: {String.Join(",", notInserted)}"
        Else
            lblNotImportedLinesInfo.Text = String.Empty
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