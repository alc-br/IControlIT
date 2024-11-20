
Public Class Localizar
    Inherits System.Web.UI.UserControl
    Dim oConfig As New cls_Config

    Public Property Pakage() As String
        Get
            Return hdfPakage.Value
        End Get
        Set(ByVal Value As String)
            hdfPakage.Value = Value
        End Set
    End Property

    Public Property Descricao() As String
        Get
            Return hdfDescricao.Value
        End Get
        Set(ByVal Value As String)
            hdfDescricao.Value = Value
            Call Executar()
        End Set
    End Property

    Public Property Selecao() As String
        Get
            Return hdfPagina.Value
        End Get
        Set(ByVal Value As String)
            hdfPagina.Value = Value
        End Set
    End Property

    Public Sub Executar()
        dtgLocaliza.CurrentPageIndex = Nothing
        '-----localiza
        Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Session("DataSet") = WS_Cadastro.DropList(Session("Conn_Banco"), hdfPakage.Value, hdfDescricao.Value)
        dtgLocaliza.DataSource = Session("DataSet")
        dtgLocaliza.DataBind()
    End Sub

    Protected Sub dtgLocaliza_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgLocaliza.PageIndexChanged
        dtgLocaliza.CurrentPageIndex = e.NewPageIndex
        dtgLocaliza.DataSource = Session("DataSet")
        dtgLocaliza.DataBind()
    End Sub

    Protected Sub btExcluir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btExcluir_") + 11, 8), System.Int32)

        Response.Redirect(hdfPagina.Value & "?id=" & dtgLocaliza.Items(i).Cells(1).Text)

        Dim v_lblDescricao As Label

        For Linha = 0 To dtgLocaliza.Items.Count - 1
            For coluna = 0 To dtgLocaliza.Items(Linha).Cells.Count - 1
                dtgLocaliza.Items(Linha).Cells(coluna).BackColor = Nothing
                v_lblDescricao = dtgLocaliza.Items(Linha).Cells(0).Controls(3)
                v_lblDescricao.ForeColor = Drawing.Color.Black
            Next coluna
        Next

        For coluna = 0 To dtgLocaliza.Items(i).Cells.Count - 1
            dtgLocaliza.Items(i).Cells(coluna).BackColor = Drawing.ColorTranslator.FromHtml("#4988DB")
            v_lblDescricao = dtgLocaliza.Items(i).Cells(0).Controls(3)
            v_lblDescricao.ForeColor = Drawing.Color.White
        Next coluna
    End Sub

End Class
