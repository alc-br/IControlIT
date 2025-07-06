
Public Class Lista_PDF
    Inherits System.Web.UI.Page
    Dim WS_Manutencao As New WS_GUA_Manutencao.WSManutencao

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       If Not Page.IsPostBack Then
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
End Class
