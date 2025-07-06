
Partial Public Class Correio
    Inherits System.Web.UI.Page
    Dim WS_Estoque As New WS_GUA_Estoque.WSEstoque
    Dim vDataSet As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials

            vDataSet = WS_Estoque.Estoque(Session("Conn_Banco"), _
                                            Nothing, Nothing, Nothing, Nothing, Request("Id_Aparelho"), Nothing, Nothing, Nothing, _
                                            "sd_SL_Correio", _
                                            True)

            lbl_Colaborador.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Consumidor")
            lbl_txtDestinatario.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Estoque_Endereco_Entrega")

        End If

    End Sub

    Protected Sub btImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btImprimir.Click
        Page.ClientScript.RegisterStartupScript(Me.GetType, "ClientScript", "<script>window.print();</script>")
    End Sub

End Class

