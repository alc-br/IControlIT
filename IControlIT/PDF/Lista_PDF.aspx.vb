
'/*
'* HISTÓRICO DE MODIFICAÇÕES
'* [ICTRL-NF-202509-001 | 2025-09-30 | Anderson Chipak]
'* [ICTRL-NF-202512-002 | 2025-12-04 | Parceiro IControlIT] - Botão "Marcar como Pago" e observação obrigatória
'*/
Public Class Lista_PDF
    Inherits System.Web.UI.Page
    Dim WS_Manutencao As New WS_GUA_Manutencao.WSManutencao

    ' [INÍCIO - ICTRL-NF-202509-001]
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    ' [FIM - ICTRL-NF-202509-001]

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            ' [INÍCIO - ICTRL-NF-202512-002]
            ' Exibe o botão "Marcar como pago" apenas se for um anexo de comprovante de pagamento
            If Request("pTabela") = "Fatura_Comprovante" Then
                pnlMarcarPago.Visible = True
            End If
            ' [FIM - ICTRL-NF-202512-002]

            '-----lista arquivo pdf
            Call listar()
        End If
    End Sub

    Protected Sub btInserir_Click(sender As Object, e As System.EventArgs) Handles btInserir.Click
        Response.Redirect("../PDF/importa_PDF.aspx?pRegistro=" & Request("pRegistro") & "&pTabela=" & Request("pTabela"))
    End Sub

    Protected Sub btExcluir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)

        Dim v_btExcluir As ImageButton = sender
        Dim vText As System.String = v_btExcluir.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btExcluir_") + 11, 8), System.Int32)

        dtgListaPDF.DataSource = WS_Manutencao.ArquivoPDF(Session("Conn_Banco"), _
                                                        dtgListaPDF.Items(i).Cells(3).Text, _
                                                        Nothing, _
                                                        dtgListaPDF.Items(i).Cells(4).Text, _
                                                        dtgListaPDF.Items(i).Cells(5).Text, _
                                                        Nothing, _
                                                        Nothing, _
                                                        Session("Id_Usuario"), _
                                                        "sp_SE", _
                                                        False)
        '-----lista arquivo pdf
        Call listar()
    End Sub

    Public Sub listar()
        '-----lista arquivo pdf
        dtgListaPDF.DataSource = WS_Manutencao.ArquivoPDF(Session("Conn_Banco"), _
                                                            Nothing, _
                                                            Nothing, _
                                                            Request("pTabela"), _
                                                            Request("pRegistro"), _
                                                            Nothing, _
                                                            Nothing, _
                                                            Nothing, _
                                                            "sp_SL_ID", _
                                                            True)
        dtgListaPDF.DataBind()
    End Sub

    Protected Sub btLink_Click(sender As Object, e As ImageClickEventArgs)
        Dim v_btExcluir As ImageButton = sender
        Dim vText As System.String = v_btExcluir.ClientID.ToString
        Dim i As System.Int32 = vText.Split("_").Last()

        Response.Redirect($"../PDF/View_PDF.aspx?pTabela={Request("pTabela")}&pRegistro={Request("pRegistro")}&pId_Arquivo_PDF={dtgListaPDF.Items(i).Cells(3).Text}")
    End Sub

    ' [INÍCIO - ICTRL-NF-202512-002] - Marcar fatura como paga
    Protected Sub btnMarcarComoPago_Click(sender As Object, e As EventArgs) Handles btnMarcarComoPago.Click
        ' Apenas exibe o modal de confirmação
        pnlConfirmacaoPago.Visible = True
    End Sub

    Protected Sub btnCancelarConfirmacao_Click(sender As Object, e As EventArgs) Handles btnCancelarConfirmacao.Click
        ' Esconde o modal e limpa o campo de texto
        pnlConfirmacaoPago.Visible = False
        txtObservacao.Text = ""
    End Sub

    Protected Sub btnConfirmarPago_Click(sender As Object, e As EventArgs) Handles btnConfirmarPago.Click
        Try
            ' Valida se a observação foi preenchida
            If Page.IsValid AndAlso Not String.IsNullOrWhiteSpace(txtObservacao.Text) Then
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

                ' Esconde o modal
                pnlConfirmacaoPago.Visible = False
                txtObservacao.Text = ""

                ' Registra um script para fechar esta janela pop-up e atualizar a página que a abriu
                Dim script As String = "alert('Fatura marcada como PAGA com sucesso!'); window.opener.location.reload(); window.close();"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closeAndRefresh", script, True)
            End If
        Catch ex As Exception
            ' Em caso de erro, exibe mensagem
            Dim scriptErro As String = "alert('Erro ao marcar fatura como paga: " & ex.Message.Replace("'", "\'") & "');"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "erro", scriptErro, True)
        End Try
    End Sub
    ' [FIM - ICTRL-NF-202512-002]
End Class
