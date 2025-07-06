
Imports System.Web.UI.DataVisualization.Charting

Public Class Auditoria_Consulta
    Inherits System.Web.UI.Page

    Dim WS_Auditoria As New WS_GUA_Auditoria.WSAuditoria
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vDataSet As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Acompanhamento de Contestação ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboConglomerado)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)
            btStatus.PostBackUrl = "~/Auditoria/Auditoria.aspx"
            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))
            Page.SetFocus(cboConglomerado)

            Session("DataSet") = Nothing
        End If
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As EventArgs) Handles btExecutar.Click
        WS_Auditoria.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        If cboConglomerado.SelectedValue = Nothing Then Exit Sub
        btStatus.PostBackUrl = "~/Auditoria/Auditoria.aspx?lstConglomerado=" & cboConglomerado.SelectedValue
        lblDescricaoArquivo.Text = ""
        Session("DataSet") = Nothing

        '-----monta colunas
        vDataSet = WS_Auditoria.Auditoria(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Coluna_Consulta", True)
        Dim i As System.Int32
        Dim header(vDataSet.Tables(0).Rows.Count - 1) As String
        Dim field(vDataSet.Tables(0).Rows.Count - 1) As String
        Dim sort(vDataSet.Tables(0).Rows.Count - 1) As String

        For i = 0 To vDataSet.Tables(0).Rows.Count - 1
            header(i) = vDataSet.Tables(0).Rows(i).Item("Descricao")
            field(i) = vDataSet.Tables(0).Rows(i).Item("Descricao")
            sort(i) = vDataSet.Tables(0).Rows(i).Item("ID")
        Next

        Call Colunas(header, field, sort)

        '-----monta dados
        Session("DataSet") = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        cboConglomerado.SelectedValue,
                                        Nothing,
                                        "sp_Auditoria_Consulta",
                                        True)

        If Session("DataSet").Tables.Count = 0 Then Exit Sub
        dtgConsultaConta.DataSource = Session("DataSet")
        dtgConsultaConta.DataBind()

        '-----soma total recuparedo
        Dim vValorFaura As System.Double = 0
        For i = 0 To dtgConsultaConta.Items.Count - 1
            vValorFaura = vValorFaura + IIf(dtgConsultaConta.Items(i).Cells(dtgConsultaConta.Columns.Count - 1).Text = "&nbsp;", 0, dtgConsultaConta.Items(i).Cells(dtgConsultaConta.Columns.Count - 1).Text)
        Next i
        txtTotalRecuperado.Text = Format(vValorFaura, "##,###.#0")

        '-----monta grafico
        'vDataSet = WS_Auditoria.Auditoria(Session("Conn_Banco"),
        '                            Nothing,
        '                            Nothing,
        '                            Nothing,
        '                            cboConglomerado.SelectedItem.Value,
        '                            Nothing,
        '                            "sp_Auditoria_Grafico_Conglomerado",
        '                            True)

        'dtgMatrix.DataSource = vDataSet
        'dtgMatrix.DataBind()

        'If Not vDataSet Is Nothing Then
        '    If vDataSet.Tables.Count > 0 Then
        '        If vDataSet.Tables(0).Rows.Count > 0 Then
        '            Dim Linha As Data.DataRow
        '            Dim vX As System.String = Nothing
        '            Dim vY As System.String = Nothing

        '            '-----monta grafico de qtd de ativos
        '            vX = Nothing
        '            vY = Nothing
        '            For Each Linha In vDataSet.Tables(0).Rows
        '                vX = vX + Linha.Item("Descricao") + ";"
        '                vY = vY + CType(Linha.Item("Valor"), System.String) + ";"
        '            Next
        '            vX = Mid(vX, 1, Len(vX) - 1)
        '            vY = Mid(vY, 1, Len(vY) - 1)

        '            hfdX.Value = vX
        '            hfdY.Value = vY
        '            Call GeraGrafico(hfdX.Value, hfdY.Value)
        '        End If
        '    End If
        'End If


        'lblGrafico.Visible = True
        lblDescricaoArquivo.Text = "Arquivo | Conglomerado: " & cboConglomerado.SelectedItem.Text

        DivCorpo.Visible = True
        'DivLista.Visible = True
        lblTotalRecuperado.Visible = True
        txtTotalRecuperado.Visible = True

        conteudo.Visible = True
    End Sub

    Public Sub Colunas(ByVal pHeader() As System.String,
                        ByVal pField() As System.String,
                        ByVal pSort() As System.String)

        Dim i As System.Int32
        Dim Coluna As BoundColumn

        For i = 0 To UBound(pHeader)
            '-----monta coluna
            Coluna = New BoundColumn
            If pHeader(i) = "Id_Auditoria_Lote" Then Coluna.Visible = False
            Coluna.DataField = pField(i)
            Coluna.HeaderText = pHeader(i)

            If pSort(i) > 2 Then
                If pSort(i) > 2 Then
                    Coluna.DataFormatString = "{0:R$##########,###########0}"
                End If
                Coluna.HeaderStyle.HorizontalAlign = HorizontalAlign.Right
                Coluna.ItemStyle.HorizontalAlign = HorizontalAlign.Right
            End If

            dtgConsultaConta.Columns.Add(Coluna)
        Next
    End Sub

    'Public Sub GeraGrafico(ByVal pX As System.String, ByVal pY As System.String)
    '    Dim i As System.Int32
    '    Dim rX() As System.String = Split(pX, ";")
    '    Dim rY() As System.String = Split(pY, ";")
    '    Dim X(UBound(rX)) As System.String
    '    Dim Y(UBound(rY)) As System.Double

    '    For i = 0 To UBound(rX)
    '        X(i) = rX(i)
    '        Y(i) = CType(rY(i), System.Double)
    '    Next

    '    Grafico.Series(0).Points.DataBindXY(X, Y)
    '    Grafico.Series(0).BackGradientStyle = GradientStyle.TopBottom
    '    Grafico.Series(0).Color = Drawing.ColorTranslator.FromHtml("#f953c6")
    '    Grafico.Series(0).BackSecondaryColor = Drawing.ColorTranslator.FromHtml("#b91d73")
    'End Sub

    Protected Sub btFecharMsg_Click(sender As Object, e As EventArgs) Handles btFecharMsg.Click
        '-----monta colunas
        vDataSet = WS_Auditoria.Auditoria(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Coluna_Consulta", True)
        Dim i As System.Int32
        Dim header(vDataSet.Tables(0).Rows.Count - 1) As String
        Dim field(vDataSet.Tables(0).Rows.Count - 1) As String
        Dim sort(vDataSet.Tables(0).Rows.Count - 1) As String

        For i = 0 To vDataSet.Tables(0).Rows.Count - 1
            header(i) = vDataSet.Tables(0).Rows(i).Item("Descricao")
            field(i) = vDataSet.Tables(0).Rows(i).Item("Descricao")
            sort(i) = vDataSet.Tables(0).Rows(i).Item("ID")
        Next

        Call Colunas(header, field, sort)
        '-----lista auditoria
        dtgConsultaConta.DataSource = Session("DataSet")
        dtgConsultaConta.DataBind()

        divPopupAuditoria.Visible = False

        For Linha = 0 To dtgConsultaConta.Items.Count - 1
            For coluna = 0 To dtgConsultaConta.Items(Linha).Cells.Count - 1
                dtgConsultaConta.Items(Linha).Cells(coluna).BackColor = Nothing
            Next coluna
        Next
    End Sub

    Protected Sub dtgConsultaConta_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dtgConsultaConta.SelectedIndexChanged

        '-----monta colunas
        vDataSet = WS_Auditoria.Auditoria(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Coluna_Consulta", True)
        Dim i As System.Int32
        Dim header(vDataSet.Tables(0).Rows.Count - 1) As String
        Dim field(vDataSet.Tables(0).Rows.Count - 1) As String
        Dim sort(vDataSet.Tables(0).Rows.Count - 1) As String

        For i = 0 To vDataSet.Tables(0).Rows.Count - 1
            header(i) = vDataSet.Tables(0).Rows(i).Item("Descricao")
            field(i) = vDataSet.Tables(0).Rows(i).Item("Descricao")
            sort(i) = vDataSet.Tables(0).Rows(i).Item("ID")
        Next

        Call Colunas(header, field, sort)
        '-----lista auditoria
        dtgConsultaConta.DataSource = Session("DataSet")
        dtgConsultaConta.DataBind()

        '-----lista acompanhamento da auditoria
        dtgAcompanhamento_Status.DataSource = WS_Auditoria.Auditoria(Session("Conn_Banco"),
                                                                    dtgConsultaConta.SelectedItem.Cells(1).Text,
                                                                    Nothing,
                                                                    Nothing,
                                                                    Nothing,
                                                                    Nothing,
                                                                    "sp_Acompanhamento_Status",
                                                                    True)

        dtgAcompanhamento_Status.DataBind()

        divPopupAuditoria.Visible = True

        '------seleciona 
        For Linha = 0 To dtgConsultaConta.Items.Count - 1
            For coluna = 0 To dtgConsultaConta.Items(Linha).Cells.Count - 1
                dtgConsultaConta.Items(Linha).Cells(coluna).BackColor = Nothing
            Next coluna
        Next

        For coluna = 0 To dtgConsultaConta.SelectedItem.Cells.Count - 1
            dtgConsultaConta.Items(dtgConsultaConta.SelectedItem.ItemIndex).Cells(coluna).BackColor = Drawing.ColorTranslator.FromHtml("#C0C0C0")
        Next coluna

    End Sub

    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String = Nothing
        '-----nome do arquivo a ser exportado
        Dim Descricao As System.String = "Resumo_Auditoria"
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

