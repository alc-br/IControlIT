
Public Class Ativo_Localiza
    Inherits System.Web.UI.Page
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo

    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Call Master.Localizar(Nothing, Nothing)

            If Request("ID") = "Inventario" Then
                Session("DataSet") = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Inventario", Nothing, Nothing, Nothing, Nothing, Nothing)
                dtgLocaliza.DataSource = Session("DataSet")
                dtgLocaliza.DataBind()
                '-----traduz e passa titulo para master page
                Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Ativos sem Usuário ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))
            End If

            If Request("ID") = "Custo_Cancelada" Then
                Session("DataSet") = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Custo_Cancelada", Nothing, Nothing, Nothing, Nothing, Nothing)
                dtgLocaliza.DataSource = Session("DataSet")
                dtgLocaliza.DataBind()
                '-----traduz e passa titulo para master page
                Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Custo de Ativos Cancelados ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))
            End If

            If Request("ID") = "Sem_lote" Then
                Session("DataSet") = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Linha_Sem_Lote", Nothing, Nothing, Nothing, Nothing, Nothing)
                dtgLocaliza.DataSource = Session("DataSet")
                dtgLocaliza.DataBind()
                '-----traduz e passa titulo para master page
                Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Ativos sem Conta",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))
            End If

            If Request("ID") = "Custo_Estoque" Then
                Session("DataSet") = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Custo_Estoque", Nothing, Nothing, Nothing, Nothing, Nothing)
                dtgLocaliza.DataSource = Session("DataSet")
                dtgLocaliza.DataBind()
                '-----traduz e passa titulo para master page
                Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Custo de Ativos em Estoque ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))
            End If

            If Request("ID") = "Linha_Sem_Uso" Then
                Session("DataSet") = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Linha_Sem_Uso", Nothing, Nothing, Nothing, Nothing, Nothing)
                dtgLocaliza.DataSource = Session("DataSet")
                dtgLocaliza.DataBind()
                '-----traduz e passa titulo para master page
                Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Ativos sem uso no último Mês ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))
            End If

            If dtgLista.Items.Count = 0 Then
                btAlerta.Enabled = True
                btAlerta.Style.Add("Opacity", "0.1")
            End If
        End If
    End Sub

    Protected Sub dtgLocaliza_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgLocaliza.PageIndexChanged
        dtgLocaliza.CurrentPageIndex = e.NewPageIndex
        dtgLocaliza.DataSource = Session("DataSet")
        dtgLocaliza.DataBind()
    End Sub

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        pnlMsg.Visible = False
    End Sub
    Protected Sub btAlerta_Click(sender As Object, e As EventArgs)
        pnlMsg.Visible = True
    End Sub
    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String = Nothing
        '-----nome do arquivo a ser exportado
        Dim Descricao As System.String = "Detalhamento"
        '-----campos a ser exportado modelo (xxxx;xxxxx;xxxx). quando null sistema gera com base no dataset
        Dim Campo As System.String = Nothing
        '-----abre pnl de exportacao
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                            "Descricao=" & Descricao &
                                            "&Campo=" & Campo &
                                            "&Tipo=" & Tipo &
                                            "','','resizable=yes, menubar=yes, scrollbars=no," &
                                            "height=768px, width=1024px, top=10, left=10'" &
                                            ")", True)
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
