Public Class Conta_OnLine_Detalhe_Impressao
    Inherits System.Web.UI.Page
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call lista_Todos()
        End If
    End Sub

    Public Sub lista_Todos()
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----monta detalhamento da conta
        lblDescricao.Text = Trim(Request("Descricao"))
        Dim vDataSet As New System.Data.DataSet
        Dim vDataView As System.Data.DataView
        Dim vFiltro As System.String = Nothing

        vDataView = New Data.DataView(WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Detalhamento_Bilhete", Request("Filtro"), Session("Id_Usuario"), Nothing, Nothing).Tables(0), _
                                                              vFiltro, _
                                                              "Data, Destino ASC", _
                                                              Data.DataViewRowState.OriginalRows)

        dtgDetalhamento.DataSource = vDataView
        dtgDetalhamento.DataBind()

        '-----trata se usuario tem permissao de detalhar a conta
        If dtgDetalhamento.Items(0).Cells(0).Text = "&nbsp;" Then
            pnlDetalhe.Visible = True
            dtgDetalhamento.Visible = False
            Exit Sub
        End If

        '-----busca volume
        dtgVolume.DataSource = WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Volume_Impressao_Folha", Request("Filtro"), Nothing, Nothing, Nothing)
        dtgVolume.DataBind()
    End Sub
End Class
