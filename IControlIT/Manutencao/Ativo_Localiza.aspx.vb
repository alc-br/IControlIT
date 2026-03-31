'/*
'* HISTÓRICO DE MODIFICAÇÕES
'* [ICTRL-NF-202509-001 | 2025-09-30 | Parceiro IControlIT]
'* [ICTRL-NF-202512-002 | 2025-12-03 | Parceiro IControlIT] - KPI Contas Não Pagas: 10 colunas, botões Comprovante e Fatura, lógica de cores
'*/
Public Class Ativo_Localiza
    Inherits System.Web.UI.Page
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo

    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' [INÍCIO - ICTRL-NF-202509-001] - Validar Session
        If Session("Conn_Banco") Is Nothing OrElse Session("Nm_Usuario") Is Nothing Then
            Response.Redirect("~/Default.aspx")
            Exit Sub
        End If
        ' [FIM - ICTRL-NF-202509-001]

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

            ' [INÍCIO - ICTRL-NF-202509-001] Contas Não Pagas (KPI Home)
            If Request("ID") = "ContasNaoPagas" Then
                Session("DataSet") = WS_Modulo.Exec_Procedure_Simple(Session("Conn_Banco"), "sp_SL_ContasNaoPagas")

                dtgLocaliza.Visible = False
                dtgContasNaoPagas.Visible = True

                If Session("DataSet") IsNot Nothing AndAlso _
                   CType(Session("DataSet"), DataSet).Tables.Count > 0 AndAlso _
                   CType(Session("DataSet"), DataSet).Tables(0).Rows.Count > 0 Then
                    dtgContasNaoPagas.DataSource = Session("DataSet")
                    dtgContasNaoPagas.DataBind()
                Else
                    dtgContasNaoPagas.DataSource = Nothing
                    dtgContasNaoPagas.DataBind()
                End If

                lblDescricaoVagos.Text = "Contas com Pagamento Pendente (Mês Vigente)"
                Call Master.Titulo("Relatório de Contas Não Pagas")
            End If
            ' [FIM - ICTRL-NF-202509-001]

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

    ' [INÍCIO - ICTRL-NF-202512-002 - KPI Contas Não Pagas]
    Protected Sub dtgContasNaoPagas_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' A coluna Status (Fl_Pago) é a 7ª (índice 6)
            Dim statusCell As TableCell = e.Item.Cells(6)
            Dim statusValue As String = ""

            ' A stored procedure retorna Fl_Pago como string ('Pago' ou 'Pendente')
            If Not IsDBNull(DataBinder.Eval(e.Item.DataItem, "Fl_Pago")) Then
                statusValue = DataBinder.Eval(e.Item.DataItem, "Fl_Pago").ToString().Trim()
            End If

            If statusValue = "Pendente" OrElse statusValue = "" Then
                ' Fatura NÃO PAGA - Células vermelhas
                e.Item.BackColor = System.Drawing.Color.FromArgb(255, 235, 238) ' Vermelho claro (#FFEBEE)
                statusCell.ForeColor = System.Drawing.Color.DarkRed
                statusCell.Font.Bold = True
            Else
                ' Fatura PAGA - Células verdes
                e.Item.BackColor = System.Drawing.Color.FromArgb(232, 245, 233) ' Verde claro (#E8F5E9)
                statusCell.ForeColor = System.Drawing.Color.DarkGreen
                statusCell.Font.Bold = True
            End If
        End If
    End Sub

    Protected Sub dtgContasNaoPagas_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dtgContasNaoPagas.CurrentPageIndex = e.NewPageIndex
        dtgContasNaoPagas.DataSource = Session("DataSet")
        dtgContasNaoPagas.DataBind()
    End Sub

    Protected Sub btComprovante_Click(sender As Object, e As ImageClickEventArgs)
        Dim v_bt As ImageButton = sender
        Dim idFatura As String = v_bt.CommandArgument

        ' Abrir popup com a lista de anexos (comprovantes de pagamento)
        ' Usar a mesma tela de Lista_PDF.aspx mas com tabela "Fatura_Comprovante"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
            "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & idFatura & "&pTabela=Fatura_Comprovante','_blank','resizable=yes, menubar=yes, scrollbars=no, height=700, width=1200, top=0, left=0');",
            True)
    End Sub

    Protected Sub btFatura_Click(sender As Object, e As ImageClickEventArgs)
        Dim v_bt As ImageButton = sender
        Dim idFatura As String = v_bt.CommandArgument

        ' Redirecionar para a tela de Cadastro de Fatura
        Response.Redirect("~/Recepcao_Fatura/Fatura.aspx?ID=" & idFatura)
    End Sub
    ' [FIM - ICTRL-NF-202512-002]

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        pnlMsg.Visible = False
    End Sub
    Protected Sub btAlerta_Click(sender As Object, e As EventArgs)
        pnlMsg.Visible = True
    End Sub
    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String = Nothing
        '-----nome do arquivo a ser exportado (dinâmico baseado no ID)
        Dim Descricao As System.String = "Detalhamento_" & Request("ID")
        '-----campos a ser exportado modelo (xxxx;xxxxx;xxxx). quando null sistema gera com base no dataset
        Dim Campo As System.String = Nothing

        ' [INÍCIO - ICTRL-NF-202512-002] - Preparar DataSet para exportação
        Session("DataSet_Exportacao") = CType(Session("DataSet"), DataSet)
        ' [FIM - ICTRL-NF-202512-002]

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
