Imports AjaxControlToolkit.HTMLEditor.ToolbarButton
Imports ClosedXML.Excel
Imports Microsoft.VisualBasic.Logging

Public Class Auditoria_Contestacao
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim WS_Auditoria As New WS_GUA_Auditoria.WSAuditoria
    Dim WS_Rateio As New WS_GUA_Rateio.WSRateio
    Dim vDataSet As New System.Data.DataSet


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            '-----verifica permissao de acesso a tela
            Dim vTraduzir As New cls_Traducao
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Auditoria e Contestação ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            oConfig.CarregaCombo(cboOperadora, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Fatura_Parametro", Nothing))
            vDataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            oConfig.CarregaCombo(cboDataLote, vDataSet)

            Call Master.Localizar("sp_Auditoria_Contestacao", Page.AppRelativeVirtualPath.ToString)

            If Not Request("ID") = Nothing Then
                vDataSet = Nothing

                vDataSet = WS_Auditoria.Auditoria_Contestacao(Session("Conn_Banco"),
                                                   Request("ID"),
                                                   Nothing,
                                                   Nothing,
                                                   Nothing,
                                                   Nothing,
                                                   "sp_Auditoria_Contestacao_Detalhe",
                                                   True)

                txtTotalCobrado.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Total_Cobrado") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Total_Cobrado"), "")
                txtTotalAuditado.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Total_Auditado") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Total_Auditado"), "")
                txtTotalDiferenca.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Total_Diferenca") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Total_Diferenca"), "")
                txtErro.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Erro") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Erro"), "")
                txtTotalDiferencaPositivo.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Total_Diferenca_Positiva") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Total_Diferenca_Positiva"), "")
                txtTotalDiferencaNegativo.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Total_Diferenca_Negativa") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Total_Diferenca_Negativa"), "")
                txtErroPositivo.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Erro_Positivo") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Erro_Positivo"), "")
                txtErroNegativo.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Erro_Negativo") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Erro_Negativo"), "")

                divDados.Visible = False
                divResumo.Visible = True
            End If

        End If
    End Sub

    Public Sub chkSelecTodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelecTodos.CheckedChanged
        If chkSelecTodos.Checked = True Then
            For i = 0 To optFatura.Items.Count - 1
                optFatura.Items(i).Selected = True
            Next
        Else
            For i = 0 To optFatura.Items.Count - 1
                optFatura.Items(i).Selected = False
            Next
        End If
    End Sub

    Protected Sub cboDataLote_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDataLote.SelectedIndexChanged
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

        If cboDataLote.SelectedValue = Nothing Then Exit Sub
        If cboOperadora.SelectedValue = Nothing Then Exit Sub

        dtgAtivo_Critica.DataSource = Nothing
        dtgAtivo_Critica.DataBind()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))

        '-----carreta fatura
        oConfig.CarregaCheckBoxList(optFatura, WS_Auditoria.Auditoria_Contestacao(Session("Conn_Banco"),
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                cboDataLote.SelectedValue,
                                                cboOperadora.SelectedValue,
                                                "sp_Retorna_Fatura",
                                                True))
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As EventArgs)


        For i = 0 To optFatura.Items.Count - 1
            If optFatura.Items(i).Selected Then
                vDataSet = WS_Auditoria.Auditoria_Contestacao(Session("Conn_Banco"),
                                                   Nothing,
                                                   optFatura.Items(i).Value,
                                                   Nothing,
                                                   cboDataLote.SelectedValue,
                                                   cboOperadora.SelectedValue,
                                                   "sp_Auditoria_Contestacao_V2",
                                                   True)

                txtTotalCobrado.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Total_Cobrado") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Total_Cobrado"), "")
                txtTotalAuditado.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Total_Auditado") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Total_Auditado"), "")
                txtTotalDiferenca.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Total_Diferenca") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Total_Diferenca"), "")
                txtErro.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Erro") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Erro"), "")
                'txtTotalDiferencaPositivo.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Total_Diferenca_Positiva") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Total_Diferenca_Positiva"), "")
                'txtTotalDiferencaNegativo.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Total_Diferenca_Negativa") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Total_Diferenca_Negativa"), "")
                'txtErroPositivo.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Erro_Positivo") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Erro_Positivo"), "")
                'txtErroNegativo.Text = IIf(vDataSet.Tables(0).Rows(0).Item("Erro_Positivo") IsNot DBNull.Value, vDataSet.Tables(0).Rows(0).Item("Erro_Negativo"), "")
                ViewState("IdFatura") = optFatura.Items(i).Value
                ViewState("DataLote") = cboDataLote.SelectedValue
            End If
        Next
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub

    Protected Sub Valida_Tab(ByVal btn As Button)

        divDados.Visible = False
        divResumo.Visible = False
        btnDados.CssClass = "btn-tab-disable pull-left"
        btnResumo.CssClass = "btn-tab-disable pull-left"

        If btn.Text = "Dados" Then
            divDados.Visible = True
            btnDados.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Resumo" Then
            divResumo.Visible = True
            btnResumo.CssClass = "btn-tab pull-left"
        End If

    End Sub

    Public Sub limpar()
        cboOperadora.SelectedValue = Nothing
        cboDataLote.SelectedValue = Nothing
        chkSelecTodos.Checked = False
        optFatura.SelectedValue = Nothing
        optFatura.Items.Clear()
        txtDescricao.Text = Nothing
        txtObservacao.Text = Nothing
        txtTotalAuditado.Text = Nothing
        txtTotalCobrado.Text = Nothing
        txtTotalDiferenca.Text = Nothing
        txtTotalDiferencaPositivo.Text = Nothing
        txtTotalDiferencaNegativo.Text = Nothing
        txtErro.Text = Nothing
        txtErroNegativo.Text = Nothing
        txtErroPositivo.Text = Nothing
        txtContestacoesAnteriores.Text = Nothing
    End Sub

    Protected Sub btnConta_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnAcompanhamento_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnStatus_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btExportar_Click(sender As Object, e As EventArgs)

        If ViewState("IdFatura") IsNot Nothing Then
            Session("DataSet") = WS_Auditoria.Auditoria_Contestacao(Session("Conn_Banco"),
                                                   Nothing,
                                                   ViewState("IdFatura"),
                                                   Nothing,
                                                   ViewState("DataLote"),
                                                   Nothing,
                                                   "sp_Auditoria_Contestacao_Exporta",
                                                   True)

            '-----comentado = todos ou posso selecionar um tipo de modelo por vez
            Dim Tipo As System.String = Nothing
            '-----nome do arquivo a ser exportado
            Dim Descricao As System.String = "Auditoria_Contestacao"
            '-----campos a ser exportado modelo (xxxx;xxxxx;xxxx). quando null sistema gera com base no dataset
            Dim Campo As System.String = "Id_Auditoria_Item; Id_Auditoria_Resumo;Id_Bilhete;Id_Bilhete_Tipo;Id_Ativo;Id_Contrato;Id_Conglomerado;Unidade;DT_Lote;QTD_Consumo;Valor_Cobrado;Valor_Contrato;Valor_Correto;Valor_Cobrado_A_Mais;Total_Fatura;Fatura"

            '-----abre pnl de exportacao
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                            "Descricao=" & Descricao &
                                            "&Campo=" & Campo &
                                            "&Tipo=" & Tipo &
                                            "','','resizable=yes, menubar=yes, scrollbars=no," &
                                            "height=768px, width=1024px, top=10, left=10'" &
                                            ")", True)
        End If
    End Sub
End Class