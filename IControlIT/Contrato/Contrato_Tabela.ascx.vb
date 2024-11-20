
Imports System.Data.SqlClient
Imports IControlIT.WS_GUA_Contrato
Imports IControlIT.WS_GUA_Manutencao
Imports iTextSharp.text

Public Class Contrato_Tabela
    Inherits System.Web.UI.UserControl
    Dim oConfig As New cls_Config
    Dim WS_Manutencao As New WS_GUA_Manutencao.WSManutencao

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

    Protected Sub TxtPesquisa_Changed(ByVal sender As Object, ByVal e As EventArgs) Handles txtPesquisa.TextChanged
        Dim dt As Data.DataSet = Session("DataSet")

        If String.IsNullOrEmpty(Trim(txtPesquisa.Text)) Then
            dtgContrato_Tabela.DataSource = Session("DataSet")
            dtgContrato_Tabela.CurrentPageIndex = 0
            dtgContrato_Tabela.DataBind()
        Else
            dt.Tables(0).DefaultView.RowFilter = $"[Nr_Contrato] LIKE '%{txtPesquisa.Text}%' OR 
                                                   [Nm_Empresa_Contratada] LIKE '%{txtPesquisa.Text}%'OR 
                                                   [Objeto] LIKE '%{txtPesquisa.Text}%'"
            'dt.Tables(0).DefaultView.RowFilter = $"[Nr_Contrato] LIKE '%{txtPesquisa.Text}%' OR 
            '                                       [Nm_Empresa_Contratada] LIKE '%{txtPesquisa.Text}%' OR
            '                                       [Pedido] LIKE '%{txtPesquisa.Text}%' OR
            '                                       [Req] LIKE '%{txtPesquisa.Text}%'"

            dtgContrato_Tabela.DataSource = dt.Tables(0).DefaultView
            dtgContrato_Tabela.CurrentPageIndex = 0
            dtgContrato_Tabela.DataBind()

        End If
    End Sub


    Protected Sub btExibirAnexo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)

        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btExibirAnexo_") + 15, 8), System.Int32)
        Dim id = dtgContrato_Tabela.Items(i).Cells(0).Text

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & id & "&pTabela=Contrato','','resizable=yes, menubar=yes, scrollbars=no,height=768px, width=1024px, top=10, left=10')", True)

        v_btSalvar.CssClass = "GREEN"

    End Sub

    Public Function countAnexosContrato(ByVal idContrato As Integer) As Integer
        Dim data As DataSet = WS_Manutencao.ArquivoPDF(Session("Conn_Banco"),
                                                  Nothing,
                                                  Nothing,
                                                  "Contrato", _ ' Substitua "NomeDaTabela" pelo nome correto da tabela
                                                  idContrato,
                                                  Nothing,
                                                  Nothing,
                                                  Nothing,
                                                  "sp_SL_ID",
                                                  True)

        Dim numeroDeAnexos As Integer = 0

        If data IsNot Nothing AndAlso data.Tables.Count > 0 AndAlso data.Tables(0).Rows.Count > 0 Then
            numeroDeAnexos = data.Tables(0).Rows.Count
        End If

        Return numeroDeAnexos
    End Function

    Protected Sub dtgContrato_Tabela_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dtgContrato_Tabela.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim idContrato As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Id_Contrato"))

            Dim qtdeAnexos As Integer = countAnexosContrato(idContrato)

            Dim btExibirAnexo As ImageButton = CType(e.Item.FindControl("btExibirAnexo"), ImageButton)

            If qtdeAnexos > 0 Then
                ' btExibirAnexo.Style("background-color") = "green"  ' Define a cor de fundo para verde
                btExibirAnexo.CssClass = "YELLOW"
            Else
                'btExibirAnexo.Style("background-color") = "red"  ' Define a cor de fundo para vermelho
                btExibirAnexo.CssClass = "RED"
            End If
        End If
    End Sub


End Class


