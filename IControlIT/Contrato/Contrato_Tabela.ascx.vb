
Imports System.Data.SqlClient

Public Class Contrato_Tabela
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
        dtgContrato_Tabela.CurrentPageIndex = Nothing
        '-----localiza
        Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Session("DataSet") = WS_Cadastro.DropList(Session("Conn_Banco"), hdfPakage.Value, hdfDescricao.Value)
        dtgContrato_Tabela.DataSource = Session("DataSet")
        dtgContrato_Tabela.DataBind()
    End Sub

    Protected Sub dtgContrato_Tabela_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgContrato_Tabela.PageIndexChanged
        dtgContrato_Tabela.CurrentPageIndex = e.NewPageIndex
        dtgContrato_Tabela.DataSource = Session("DataSet")
        dtgContrato_Tabela.DataBind()
    End Sub

    Protected Sub btExcluir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btExcluir_") + 11, 8), System.Int32)

        Response.Redirect(hdfPagina.Value & "?id=" & dtgContrato_Tabela.Items(i).Cells(0).Text)

        Dim v_lblDescricao As Label

        For Linha = 0 To dtgContrato_Tabela.Items.Count - 1
            For coluna = 0 To dtgContrato_Tabela.Items(Linha).Cells.Count - 1
                dtgContrato_Tabela.Items(Linha).Cells(coluna).BackColor = Nothing
                v_lblDescricao = dtgContrato_Tabela.Items(Linha).Cells(0).Controls(3)
                v_lblDescricao.ForeColor = Drawing.Color.Black
            Next coluna
        Next

        For coluna = 0 To dtgContrato_Tabela.Items(i).Cells.Count - 1
            dtgContrato_Tabela.Items(i).Cells(coluna).BackColor = Drawing.ColorTranslator.FromHtml("#4988DB")
            v_lblDescricao = dtgContrato_Tabela.Items(i).Cells(0).Controls(3)
            v_lblDescricao.ForeColor = Drawing.Color.White
        Next coluna
    End Sub

    Protected Sub dtgdtgContrato_Tabela_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgContrato_Tabela.SortCommand
        Dim dt As Data.DataSet = Session("DataSet")
        dt.Tables(0).DefaultView.Sort = e.SortExpression & " " & SortDir(e.SortExpression)
        dtgContrato_Tabela.DataSource = dt.Tables(0).DefaultView
        dtgContrato_Tabela.DataBind()
    End Sub

    Private Function SortDir(ByVal sColumn As String) As String
        Dim sDir As String = "asc"
        Dim sPreviousColumnSorted As String = If(ViewState("SortColumn") IsNot Nothing, ViewState("SortColumn").ToString(), "")

        If sPreviousColumnSorted = sColumn Then
            sDir = If(ViewState("SortDir").ToString() = "asc", "desc", "asc")
        Else
            ViewState("SortColumn") = sColumn
        End If

        ViewState("SortDir") = sDir
        Return sDir
    End Function
End Class
