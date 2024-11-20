
Public Class Upload
    Inherits System.Web.UI.Page
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim oTraducao As New cls_Traducao
    Dim oConfig As New cls_Config
    Dim vDataSet As System.Data.DataSet
    Dim vBanco As String
    Dim vPasta As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            btCopiar.Attributes.Add("onclick", "alert('Aguarde o upload do arquivo. Essa operação pode levar alguns minutos.');timedCount();")

            '-----criar pasta com o texto 'convert_' + o nome do banco (ex. convert_sms)
            vBanco = oTraducao.Descriptografar(Session("Conn_Banco"))
            vPasta = "Convert_" & Trim(Mid(vBanco, (vBanco.IndexOf("CATALOG=") + 9), (vBanco.IndexOf("USER") - (vBanco.IndexOf("CATALOG=") + 9))))

            '-----lista arquivos
            vDataSet = WS_Modulo.Arquivo_Pasta(Session("Conn_Banco"), "sp_Lista_Pasta", vPasta)
            oConfig.CarregaList(lstArquivo, vDataSet)
        End If
    End Sub

    Protected Sub btCopiar_Click(sender As Object, e As System.EventArgs) Handles btCopiar.Click
        Try
            vBanco = oTraducao.Descriptografar(Session("Conn_Banco"))
            vPasta = "Convert_" & Trim(Mid(vBanco, (vBanco.IndexOf("CATALOG=") + 9), (vBanco.IndexOf("USER") - (vBanco.IndexOf("CATALOG=") + 9))))

            Dim ExtencaoArquivo As String = System.IO.Path.GetExtension(File1.PostedFile.FileName).ToLower()

            If Trim(ExtencaoArquivo) = "" Then
                lblMessage.Text = "Arquivo não selecionado."
                Exit Sub
            End If

            If ExtencaoArquivo = ".gua" Or ExtencaoArquivo = ".zip" Then
                If File1.PostedFile.ContentLength <= 70000000 Then

                    '-----realiza upload do arquivo
                    For Each file In File1.PostedFiles
                        file.SaveAs("C:\\" + vPasta + "\\" + file.FileName)
                    Next

                    '-----decompacta arquivo zipado
                    WS_Modulo.Arquivo_Pasta(Session("Conn_Banco"), "sp_Descompacta_Arquivo", vPasta)

                    '-----verifica quantidade de arquivo
                    vDataSet = WS_Modulo.Arquivo_Pasta(Session("Conn_Banco"), "sp_Lista_Pasta", vPasta)
                    oConfig.CarregaList(lstArquivo, vDataSet)

                    lblMessage.Text = "Ação executada com sucesso."
                Else
                    lblMessage.Text = "Arquivo maior que o permitido de 7mb"
                End If
            Else
                lblMessage.Text = "Formato do arquivo inválida."
            End If
        Catch exc As Exception
            lblMessage.Text = "Erro com a trasmição do arquivo."
        End Try
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As System.EventArgs) Handles btLimpar.Click
        '-----limpa a pasta
        vBanco = oTraducao.Descriptografar(Session("Conn_Banco"))
        vPasta = "Convert_" & Trim(Mid(vBanco, (vBanco.IndexOf("CATALOG=") + 9), (vBanco.IndexOf("USER") - (vBanco.IndexOf("CATALOG=") + 9))))

        vDataSet = WS_Modulo.Arquivo_Pasta(Session("Conn_Banco"), "sp_Limpar_Pasta", vPasta)
        lstArquivo.Items.Clear()
    End Sub
End Class
