Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml.Wordprocessing
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Ionic.Zip
Imports System.Collections.Generic
Imports System.Net
Imports System.Windows.Forms
Imports System.Web.UI.HtmlControls

Public Class FaturaInfo
    Public Property NomeDaFatura As String
    Public Property TipoDaFatura As String
End Class
Public Class Consulta_Conta
    Inherits System.Web.UI.Page
    Dim WS_Rateio As New WS_GUA_Rateio.WSRateio
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim oConfig As New cls_Config
    Dim WS_Manutencao As New WS_GUA_Manutencao.WSManutencao

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'FileUpload1.Attributes("multiple") = "multiple"
        If Not Page.IsPostBack Then
            WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

            btConfiguracao.PostBackUrl = "~/Recepcao_Fatura/Plano_Conta.aspx"

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Mapa de Conta",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboServico)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            oConfig.CarregaCombo(cboServico, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo_Grupo", Nothing))


            '-----gera data lote
            Dim vDataSet As New Data.DataSet
            vDataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            oConfig.CarregaCombo(cboDataLote, vDataSet)

        End If
    End Sub

    Protected Sub btExecutar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExecutar.Click
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----retira caixa de texto
        lblFaturaCadastrada.Visible = False
        lblFaturaCarregada.Visible = False
        lblTotalCarregado.Visible = False
        txtTotalCarregado.Visible = False
        txtFaturaCadastrada.Visible = False
        txtFaturaCarregada.Visible = False

        '-----limpa grid 
        Session("DataSet") = Nothing
        dtgConsultaConta.DataSource = Session("DataSet")
        dtgConsultaConta.DataBind()
        lblDescricaoArquivo.Text = ""

        If cboServico.SelectedValue = Nothing Then Exit Sub
        If Trim(cboDataLote.Text) = "" Then Exit Sub
        btConfiguracao.PostBackUrl = "~/Recepcao_Fatura/Plano_Conta.aspx?lstServico=" & cboServico.SelectedValue

        Session("DataSet") = WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        cboDataLote.SelectedValue,
                                                        Nothing,
                                                        cboServico.SelectedValue,
                                                        "sp_Confere_Plano_Conta_V3",
                                                        True)

        For Each Linha In Session("DataSet").Tables(0).Rows
            If Linha("Visible_LINK") = False Then
                If Linha("Download_Observacao") = "RED" Then Linha("Download_Observacao") = "RED_BLOCK"
                If Linha("Download_Fatura") = "RED" Then Linha("Download_Fatura") = "RED_BLOCK"
                If Linha("Download_Boleto") = "RED" Then Linha("Download_Boleto") = "RED_BLOCK"
                If Linha("Download_NF") = "RED" Then Linha("Download_NF") = "RED_BLOCK"
                If Linha("Download_Rateio") = "RED" Then Linha("Download_Rateio") = "RED_BLOCK"
            End If
        Next

        dtgConsultaConta.DataSource = Session("DataSet")
        dtgConsultaConta.CurrentPageIndex = 0 ' corrigido bug que não carregava caso número da página atual maior que o número de páginas ao trocar conta
        dtgConsultaConta.DataBind()

        '-----monta totalizador
        Dim vDataSet As New System.Data.DataSet
        Dim vCont As System.Int32
        Dim vFaturaCadastrada As System.Int32 = 0
        Dim vFaturaCarregada As System.Int32 = 0
        Dim vTotalCarregado As System.Double = 0

        vDataSet = Session("DataSet")

        For vCont = 0 To vDataSet.Tables(0).Rows.Count - 1
            vTotalCarregado = vTotalCarregado + vDataSet.Tables(0).Rows(vCont).Item("Total_Carregado")
            If vDataSet.Tables(0).Rows(vCont).Item("Flag_Conta_Cadastrada") = 2 Then vFaturaCadastrada = vFaturaCadastrada + 1
            If vDataSet.Tables(0).Rows(vCont).Item("Flag_Conta_Carregada") = 2 Then vFaturaCarregada = vFaturaCarregada + 1
        Next vCont

        txtTotalCarregado.Text = Strings.Format(vTotalCarregado, "R$##########,###########0")
        txtFaturaCadastrada.Text = vFaturaCadastrada
        txtFaturaCarregada.Text = vFaturaCarregada

        lblDescricaoArquivo.Text = "Arquivo | Serviço: " & cboServico.SelectedItem.Text & " - Lote: " & cboDataLote.SelectedItem.Text

        '-----mostra caixa de texto
        lblFaturaCadastrada.Visible = True
        lblFaturaCarregada.Visible = True
        lblTotalCarregado.Visible = True
        txtTotalCarregado.Visible = True
        txtFaturaCadastrada.Visible = True
        txtFaturaCarregada.Visible = True
        txtPesquisa.Visible = True
        ' exibe o botão de donwload de todas faturas quando houver arquivos para baixar
        validarBotaoDownloadZip()
        '----- k2aicontrolit-196
        btUploadLote.Visible = True

    End Sub

    Protected Sub validarBotaoDownloadZip()
        Dim dt As Data.DataSet = Session("DataSet")
        Dim existeArquivoDonwload As Boolean = False
        For Each table As DataTable In dt.Tables
            For Each row As DataRow In table.Rows
                If (row("Download_Fatura") = "GREEN" Or row("Download_Fatura") = "YELLOW") Then
                    existeArquivoDonwload = True
                End If
            Next
        Next
        If (existeArquivoDonwload = True) Then
            btDwZip.Visible = True
        Else
            btDwZip.Visible = False
        End If
    End Sub

    Protected Sub dtgConsultaConta_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgConsultaConta.PageIndexChanged
        Dim dt As Data.DataSet = Session("DataSet")

        dtgConsultaConta.CurrentPageIndex = e.NewPageIndex
        dtgConsultaConta.DataSource = dt.Tables(0).DefaultView
        dtgConsultaConta.DataBind()
    End Sub

    Protected Sub btFatura_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_bt As ImageButton = sender
        'Dim vText As System.String = v_bt.ClientID.ToString
        'Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btFatura_") + 10, 8), System.Int32)

        Response.Redirect("../Recepcao_Fatura/Fatura.aspx?ID=" & v_bt.CommandArgument) 'dtgConsultaConta.Items(i).Cells(11).Text) - correção bug sempre abre a fatura 2
    End Sub

    Protected Sub btDownload_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_bt As ImageButton = sender
        'Dim vText As System.String = v_bt.ClientID.ToString
        'Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btDownload_") + 12, 8), System.Int32)

        Dim vDataSet As New System.Data.DataSet
        vDataSet = WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                                        Nothing,
                                                        v_bt.CommandArgument,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        cboDataLote.SelectedValue,
                                                        Nothing,
                                                        cboServico.SelectedValue,
                                                        "sp_Retorna_Id_Dw",
                                                        True)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & vDataSet.Tables(0).Rows(0).Item("Id_Fatura") & "&pTabela=Fatura','_blank','resizable=yes, menubar=yes, scrollbars=no, height=700, width=1200, top=0, left=0');", True)


        If vDataSet.Tables(0).Rows(0).Item("Id_Fatura") <> 0 Then v_bt.CssClass = "GREEN"
    End Sub

    Protected Sub btObservacao_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_bt As ImageButton = sender
        'Dim vText As System.String = v_bt.ClientID.ToString
        'Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btBoleto_") + 10, 8), System.Int32)

        Dim vDataSet As New System.Data.DataSet
        vDataSet = WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                                        Nothing,
                                                        v_bt.CommandArgument,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        cboDataLote.SelectedValue,
                                                        Nothing,
                                                        cboServico.SelectedValue,
                                                        "sp_Retorna_Id_Dw",
                                                        True)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & vDataSet.Tables(0).Rows(0).Item("Id_Fatura") & "&pTabela=Observacao','_blank','resizable=yes, menubar=yes, scrollbars=no, height=700, width=1200, top=0, left=0');", True)

        If vDataSet.Tables(0).Rows(0).Item("Id_Bilhete") <> 0 Then v_bt.CssClass = "GREEN"
    End Sub

    Protected Sub btBoleto_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_bt As ImageButton = sender
        'Dim vText As System.String = v_bt.ClientID.ToString
        'Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btBoleto_") + 10, 8), System.Int32)

        Dim vDataSet As New System.Data.DataSet
        vDataSet = WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                                        Nothing,
                                                        v_bt.CommandArgument,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        cboDataLote.SelectedValue,
                                                        Nothing,
                                                        cboServico.SelectedValue,
                                                        "sp_Retorna_Id_Dw",
                                                        True)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & vDataSet.Tables(0).Rows(0).Item("Id_Fatura") & "&pTabela=Boleto','_blank','resizable=yes, menubar=yes, scrollbars=no, height=700, width=1200, top=0, left=0');", True)

        If vDataSet.Tables(0).Rows(0).Item("Id_Bilhete") <> 0 Then v_bt.CssClass = "GREEN"
    End Sub

    Protected Sub btNf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_bt As ImageButton = sender
        'Dim vText As System.String = v_bt.ClientID.ToString
        'Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btNf_") + 6, 8), System.Int32)

        Dim vDataSet As New System.Data.DataSet
        vDataSet = WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                                        Nothing,
                                                        v_bt.CommandArgument,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        cboDataLote.SelectedValue,
                                                        Nothing,
                                                        cboServico.SelectedValue,
                                                        "sp_Retorna_Id_Dw",
                                                        True)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & vDataSet.Tables(0).Rows(0).Item("Id_NF") & "&pTabela=Nota_Fiscal_Fatura','_blank','resizable=yes, menubar=yes, scrollbars=no, height=700, width=1200, top=0, left=0');", True)

        If vDataSet.Tables(0).Rows(0).Item("Id_NF") <> 0 Then v_bt.CssClass = "GREEN"
    End Sub

    Protected Sub btRateio_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim v_bt As ImageButton = sender
        'Dim vText As System.String = v_bt.ClientID.ToString
        'Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btRateio_") + 10, 8), System.Int32)

        Dim vDataSet As New System.Data.DataSet
        vDataSet = WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                                        Nothing,
                                                        v_bt.CommandArgument,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        cboDataLote.SelectedValue,
                                                        Nothing,
                                                        cboServico.SelectedValue,
                                                        "sp_Retorna_Id_Dw",
                                                        True)

        Response.Redirect("/Consulta/Template_Consulta.aspx?ID=55&pTela=true&Id_Rateio=" & vDataSet.Tables(0).Rows(0).Item("Id_Rateio"))

        If vDataSet.Tables(0).Rows(0).Item("Id_Rateio") <> 0 Then v_bt.CssClass = "GREEN"
    End Sub

    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String = Nothing
        '-----nome do arquivo a ser exportado
        Dim Descricao As System.String = "Mapa_Conta"
        '-----campos a ser exportado modelo (xxxx;xxxxx;xxxx). quando null sistema gera com base no dataset
        Dim Campo As System.String = "Fatura;Qtd_Linha;Total;Total_Carregado;Total_Fatura;Total_Rateado;Data_Cancelamento"

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

    Protected Sub dtgConsultaConta_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgConsultaConta.SortCommand
        Dim dt As Data.DataSet = Session("DataSet")

        dt.Tables(0).DefaultView.Sort = e.SortExpression & " " & SortDir(e.SortExpression)
        dtgConsultaConta.DataSource = dt.Tables(0).DefaultView
        dtgConsultaConta.DataBind()
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

    Protected Sub txtPesquisa_Changed(ByVal sender As Object, ByVal e As EventArgs) Handles txtPesquisa.TextChanged
        Dim dt As Data.DataSet = Session("DataSet")

        If String.IsNullOrEmpty(Trim(txtPesquisa.Text)) Then
            dtgConsultaConta.DataSource = Session("DataSet")
            dtgConsultaConta.CurrentPageIndex = 0
            dtgConsultaConta.DataBind()
        Else
            dt.Tables(0).DefaultView.RowFilter = $"[Operadora] LIKE '%{txtPesquisa.Text}%' OR 
                                                   [Fatura] LIKE '%{txtPesquisa.Text}%' OR
                                                   [Pedido] LIKE '%{txtPesquisa.Text}%' OR
                                                   [Req] LIKE '%{txtPesquisa.Text}%'"

            dtgConsultaConta.DataSource = dt.Tables(0).DefaultView
            dtgConsultaConta.CurrentPageIndex = 0
            dtgConsultaConta.DataBind()

        End If
    End Sub

    Protected Sub BtDwZip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btDwZip.Click
        Dim dt As Data.DataSet = Session("DataSet")
        Dim existeDownload As Boolean = False
        Dim dataatual As String
        dataatual = CDate(Now).ToString("ddMMyyyyHHmmss")
        btDwZip.Enabled = False
        btDwZip.Text = "Executando..."


        For Each table As DataTable In dt.Tables
            For Each row As DataRow In table.Rows
                If (row("Download_Fatura") = "GREEN" Or row("Download_Fatura") = "YELLOW") Then
                    RetornaIdFatura(row("Fatura"), dataatual)
                    existeDownload = True
                End If
            Next
        Next
        'btDwZip.Visible = False
        btDwZip.Enabled = True
        btDwZip.Text = "Baixar todas faturas"
        If (existeDownload = True) Then
            'abrir modal de mensagem

            If File.Exists(MapPath("~/Img_Sistema/Faturas_todas.zip")) Then
                File.Delete(MapPath("~/Img_Sistema/Faturas_todas.zip"))
            End If

            'compactar arquivos da pasta
            Using zip As ZipFile = New ZipFile()
                ' Adicione os arquivos ao arquivo ZIP
                For Each arquivo In Directory.GetFiles("C:\tempIControlIt\" + dataatual + "\")
                    zip.AddFile(arquivo, "")
                Next

                ' Salve o arquivo ZIP no disco
                zip.Save(MapPath("~/Img_Sistema/Faturas_todas.zip"))
            End Using
            lbPainelObservacaoDownload.Text = dataatual
            pnlObservacao.Visible = True
        End If


    End Sub

    Protected Sub btCancelaModalObs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancelaModalObs.Click
        'apagar o diretorio temporario
        Dim Path As String = "C:\tempIControlIt\" + lbPainelObservacaoDownload.Text
        System.IO.Directory.Delete(Path, True)

        pnlObservacao.Visible = False
    End Sub

    Protected Sub RetornaIdFatura(ByVal fatura As String, ByVal dataatual As String)
        Dim vDataSet As New System.Data.DataSet
        Dim vDataSet2 As New System.Data.DataSet
        Dim caminhoArquivoPDF As String
        Dim buffer As Byte()
        'Dim dataatual As String
        'dataatual = CDate(Now).ToString("ddMMyyyy")

        vDataSet = WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                                        Nothing,
                                                        fatura, '(valor fatura)
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        cboDataLote.SelectedValue,
                                                        Nothing,
                                                        cboServico.SelectedValue,
                                                        "sp_Retorna_Id_Dw",
                                                        True)
        '-----lista arquivo pdf
        If (vDataSet.Tables(0).Rows.Count > 0) Then
            vDataSet2 = WS_Manutencao.ArquivoPDF(Session("Conn_Banco"),
                                                                Nothing,
                                                                Nothing,
                                                                "Fatura",
                                                                vDataSet.Tables(0).Rows(0).Item("Id_Fatura"),
                                                                Nothing,
                                                                Nothing,
                                                                Nothing,
                                                                "sp_SL_ID",
                                                                True)
            For Each table As DataTable In vDataSet2.Tables
                For Each row As DataRow In table.Rows
                    Console.WriteLine(row("Link"))

                    buffer = Nothing

                    buffer = row("Arquivo")

                    If (System.IO.Directory.Exists("C:\tempIControlIt") = False) Then
                        System.IO.Directory.CreateDirectory("C:\tempIControlIt\")
                        System.IO.Directory.CreateDirectory("C:\tempIControlIt\" + dataatual + "\")
                    Else
                        If (System.IO.Directory.Exists("C:\tempIControlIt\" + dataatual + "\") = False) Then
                            System.IO.Directory.CreateDirectory("C:\tempIControlIt\" + dataatual + "\")
                        End If
                    End If
                    caminhoArquivoPDF = "C:\tempIControlIt\" + dataatual + "\" + row("Nm_Arquivo_PDF")

                    File.WriteAllBytes(caminhoArquivoPDF, buffer)

                Next
            Next
        End If

    End Sub
    '------ K2AICONTROLIT - 196
    Protected Sub BtUploadLote_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUploadLote.Click


        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../PDF/Importa_Multi.aspx?pServico=" & cboServico.SelectedValue & "&pDataLote=" & cboDataLote.SelectedValue & "','_blank','resizable=yes, menubar=yes, scrollbars=no, height=600, width=600, top=0, left=0');", True)

        'pnlUploadFaturaLote.Visible = True
        'btnSelectFileUploadFaturaLote.Enabled = True
        'ddlDrivers.Visible = True
        'lstDirectories.Visible = True
        'lstFiles.Visible = True

        '' Limpar qualquer conteúdo anterior no ComboBox
        'ddlDrivers.Items.Clear()
        '' Limpar qualquer conteúdo anterior dos diretorios
        'lstDirectories.Items.Clear()
        '' Limpar qualquer conteúdo anterior dos arquivos
        'lstFiles.Items.Clear()

        '' Obter as unidades disponíveis no sistema
        'Dim drives() As DriveInfo = DriveInfo.GetDrives()

        '' Iterar sobre as unidades e adicionar ao ComboBox
        'For Each drive As DriveInfo In drives
        '    ' Adicionar o nome da unidade ao ComboBox
        '    ddlDrivers.Items.Add(drive.Name)
        'Next

        'fnListDirectories()
    End Sub

    'Protected Sub btnClosepnlUploadFaturaLote_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClosepnlUploadFaturaLote.Click
    '    lblUpFatLoteSubTitulo.Visible = True
    '    pnlUploadFaturaLote.Visible = False
    'End Sub

    '----- K2AICONTROLIT-197

    ' Método para verificar se existe um fatura com o nome do arquivo
    'Private Function ExisteValorNoDataGrid(valor As String) As Integer
    '    Dim existeArquivo As Integer = 0
    '    Dim dt As Data.DataSet = Session("DataSet")
    '    For Each table As DataTable In dt.Tables
    '        For Each row As DataRow In table.Rows
    '            If (row("Fatura") = valor) Then
    '                existeArquivo = UpFaturaLote_Get_Id_Fatura(valor)
    '                Return existeArquivo
    '            End If
    '        Next
    '    Next
    '    Return existeArquivo
    'End Function

    ''----- Método que recupera o id da fatura
    'Private Function UpFaturaLote_Get_Id_Fatura(fatura As String) As Integer
    '    Dim vDataSet As New System.Data.DataSet
    '    Dim buffer As Byte()

    '    vDataSet = WS_Rateio.Plano_Conta(Session("Conn_Banco"),
    '                                                    Nothing,
    '                                                    fatura, '(valor fatura)
    '                                                    Nothing,
    '                                                    Nothing,
    '                                                    Nothing,
    '                                                    Nothing,
    '                                                    Nothing,
    '                                                    Nothing,
    '                                                    cboDataLote.SelectedValue,
    '                                                    Nothing,
    '                                                    cboServico.SelectedValue,
    '                                                    "sp_Retorna_Id_Dw",
    '                                                    True)
    '    If vDataSet.Tables.Count = 0 OrElse vDataSet.Tables(0).Rows.Count = 0 Then
    '        Return 0
    '    End If

    '    Return Convert.ToInt32(vDataSet.Tables(0).Rows(0)("Id_Fatura"))
    'End Function


    ''----- K2AICONTROLIT-196
    'Protected Sub btnSelectFileUploadFaturaLote_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectFileUploadFaturaLote.Click

    '    btnSelectFileUploadFaturaLote.Enabled = False
    '    ddlDrivers.Visible = False
    '    lstDirectories.Visible = False
    '    lstFiles.Visible = False

    '    Dim vNomeArquivo As FaturaInfo
    '    Dim vExisteArquivo As Integer = 0
    '    Dim vIdFatura As Integer = 0

    '    lblUpFatLoteSubTitulo.Visible = False

    '    For i As Integer = 0 To lstFiles.Items.Count - 1

    '        If lstFiles.Items(i).Selected Then

    '            Dim newRow As New HtmlTableRow()

    '            Dim textCell As New HtmlTableCell()
    '            Dim iconCell As New HtmlTableCell()

    '           vNomeArquivo = SepararFatura(lstFiles.Items(i).ToString())
    '            '----- recuperar o id da fatura para salvar o arquivo => vExisteArquivo
    '            vExisteArquivo = ExisteValorNoDataGrid(vNomeArquivo.NomeDaFatura)

    '            If vExisteArquivo > 0 Then
    '                '----- recuperar o caminho completo do arquivo
    '                Dim caminhoArquivo As String = lblCaminho.Text + "\" + lstFiles.Items(i).ToString()
    '                '----- converter o arquivo em base64
    '                If File.Exists(caminhoArquivo) Then
    '                    ' Lê o arquivo e converte seu conteúdo para Base64
    '                    Dim byteImagem As Byte() = File.ReadAllBytes(caminhoArquivo)
    '                    Dim nomeArquivoPdf = lstFiles.Items(i).ToString()
    '                    Dim tabelaRegistro = VerificarTipoTabelaRegistro(lstFiles.Items(i).ToString())
    '                    Dim idRegistroTabela = vExisteArquivo
    '                    Dim Tamanho = byteImagem.Count
    '                    Dim download = 0

    '                    '----- registra a insersão do arquivo no banco 
    '                    Dim id_retorno As Data.DataSet
    '                    id_retorno = WS_Manutencao.ArquivoPDF(Session("Conn_Banco"),
    '                                            Nothing,
    '                                            nomeArquivoPdf, 'nomeArquivo,
    '                                            tabelaRegistro, 'Request("pTabela"),
    '                                            idRegistroTabela, 'Request("pRegistro"),
    '                                            Tamanho, 'intTamanho,
    '                                            byteImagem, 'byteImagem,
    '                                            Session("Id_Usuario"),
    '                                            "sp_SM",
    '                                            False)

    '                    iconCell.InnerHtml = "<i class='fas fa-check text-success'></i>"
    '                Else
    '                    iconCell.InnerHtml = "<i class='fas fa-times text-danger'></i>"
    '                End If                
    '            Else
    '                iconCell.InnerHtml = "<i class='fas fa-times text-danger'></i>"
    '            End If

    '            textCell.InnerText = lstFiles.Items(i).ToString()

    '            newRow.Cells.Add(textCell)
    '            newRow.Cells.Add(iconCell)

    '            tblLoteFaturas.Rows.Add(newRow)

    '            If tblLoteFaturas.Rows.Count * 30 > 600 Then ' Supondo que cada linha tem uma altura média de 30px
    '                tblLoteFaturas.Attributes("style") = "max-height: 600px; overflow-y: auto;"
    '            End If
    '        End If
    '    Next
    'End Sub

    'Protected Sub fnListDirectories()
    '    ' Limpar qualquer conteúdo anterior na ListBox
    '    lstDirectories.Items.Clear()

    '    ' Obter o drive selecionado
    '    Dim selectedDrive As String = ddlDrivers.SelectedValue
    '    lblCaminho.Text = selectedDrive

    '    ' Verificar se o drive selecionado é válido
    '    If Directory.Exists(selectedDrive) Then
    '        ' Obter os diretórios no drive selecionado
    '        Dim directories() As String = Directory.GetDirectories(selectedDrive)

    '        ' Adicionar os diretórios à ListBox
    '        For Each directory As String In directories
    '            lstDirectories.Items.Add(Path.GetFileName(directory))
    '        Next
    '    Else
    '        ' Se o diretório não existir, exibir uma mensagem de erro
    '        lstDirectories.Items.Add("Diretório não encontrado.")
    '    End If
    'End Sub

    'Protected Sub lstDirectories_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    ' Limpar qualquer conteúdo anterior na ListBox de arquivos
    '    lstFiles.Items.Clear()
    '    Dim tiposPermitidos() As String = {".txt", ".pdf", ".docx", ".jpg", ".png", ".mp3", ".mp4", ".xlsx", ".pptx", ".html", ".odt", ".ods", ".odp", ".pdf", ".xlsx", ".csv", ".html", ".zip", ".rar", ".7z", ".txt", ".mdb"}

    '    ' Obter o diretório selecionado
    '    Dim selectedDirectory As String = lstDirectories.SelectedValue
    '    If selectedDirectory = "..." Then

    '        ' Dividir o caminho em partes usando o caractere de barra invertida como delimitador
    '        Dim meuArray() As String = lblCaminho.Text.Split("\"c)

    '        ' Recuperar os níveis abaixo do caminho
    '        Dim niveisAbaixo As String = ddlDrivers.Text

    '        For i As Integer = 1 To meuArray.Length - 2
    '            If Not niveisAbaixo.EndsWith("\") Then
    '                niveisAbaixo &= "\" & meuArray(i)
    '            Else
    '                niveisAbaixo &= meuArray(i)
    '            End If
    '        Next

    '        lblCaminho.Text = niveisAbaixo

    '        ' Verificar se o diretório selecionado é válido
    '        If Directory.Exists(niveisAbaixo) Then
    '            ' Obtém uma coleção de nomes de caminho para os subdiretórios no diretório
    '            Dim subdiretorios() As String = Directory.GetDirectories(niveisAbaixo, FileIO.SearchOption.SearchAllSubDirectories)

    '            lstDirectories.Items.Clear()
    '            If niveisAbaixo <> ddlDrivers.Text Then
    '                lstDirectories.Items.Add("...")
    '            End If

    '            Dim directories() As String = Directory.GetDirectories(niveisAbaixo)

    '            ' Adicionar os diretórios à ListBox
    '            For Each directory As String In directories
    '                lstDirectories.Items.Add(Path.GetFileName(directory))
    '            Next

    '            ' Obter os arquivos no diretório selecionado
    '            Dim files() As String = Directory.GetFiles(niveisAbaixo)

    '            ' Adicionar os arquivos à ListBox
    '            For Each file As String In files
    '                lstFiles.Items.Add(Path.GetFileName(file))
    '            Next
    '        Else
    '            ' Se o diretório não existir, exibir uma mensagem de erro
    '            lstFiles.Items.Add("Diretório não encontrado.")
    '        End If
    '    Else
    '        'verifica se o ultimo caracter é uma barra se não inclui uma barra no final do caminho antes de concatenar o próximo nível
    '        If Not lblCaminho.Text.EndsWith("\") Then
    '            lblCaminho.Text = lblCaminho.Text & "\"
    '        End If
    '        lblCaminho.Text = lblCaminho.Text & selectedDirectory

    '        selectedDirectory = lblCaminho.Text

    '        ' Verificar se o diretório selecionado é válido
    '        If Directory.Exists(selectedDirectory) Then
    '            ' Obtém uma coleção de nomes de caminho para os subdiretórios no diretório
    '            Dim subdiretorios() As String = Directory.GetDirectories(selectedDirectory, FileIO.SearchOption.SearchAllSubDirectories)

    '            lstDirectories.Items.Clear()
    '            lstDirectories.Items.Add("...")
    '            Dim directories() As String = Directory.GetDirectories(selectedDirectory)

    '            ' Adicionar os diretórios à ListBox
    '            For Each directory As String In directories
    '                lstDirectories.Items.Add(Path.GetFileName(directory))
    '            Next

    '            ' Obter os arquivos no diretório selecionado
    '            Dim files() As String = Directory.GetFiles(selectedDirectory)

    '            ' Adicionar os arquivos à ListBox
    '            For Each file As String In files
    '                lstFiles.Items.Add(Path.GetFileName(file))
    '            Next
    '        Else
    '            ' Se o diretório não existir, exibir uma mensagem de erro
    '            lstFiles.Items.Add("Diretório não encontrado.")
    '        End If

    '    End If
    'End Sub

    ''---- K2AICONTROIT-197 - JOÃO CARLOS 
    'Public Function SepararFatura(nomeDeArquivo As String) As FaturaInfo
    '    Dim nomeFatura As String = ""
    '    Dim tipoFatura As String = ""

    '    ' Verifica se o nome do arquivo segue o padrão esperado
    '    If nomeDeArquivo.Contains("_") AndAlso nomeDeArquivo.Contains(".") Then
    '        Dim partesNome As String() = nomeDeArquivo.Split("_"c)
    '        If partesNome.Length >= 2 Then
    '            nomeFatura = partesNome(0)

    '            Dim partesTipo As String() = partesNome(1).Split("."c)
    '            If partesTipo.Length >= 2 Then
    '                tipoFatura = partesTipo(0)
    '            End If
    '        End If
    '    End If

    '    ' Retorna as informações da fatura
    '    Return New FaturaInfo With {
    '    .NomeDaFatura = nomeFatura,
    '    .TipoDaFatura = tipoFatura
    '}
    'End Function

    'Public Function VerificarTipoTabelaRegistro(ByVal texto As String) As String
    '    ' Expressões regulares para verificar a presença das palavras-chave
    '    Dim regexBoleto As New Regex("\bBOL\b", RegexOptions.IgnoreCase)
    '    Dim regexNotaFiscal As New Regex("\bNF\b", RegexOptions.IgnoreCase)
    '    Dim regexFatura As New Regex("\bDET\b", RegexOptions.IgnoreCase)

    '    ' Verifica se a string contém alguma das palavras-chave
    '    If regexBoleto.IsMatch(texto) Then
    '        Return "Boleto"
    '    ElseIf regexFatura.IsMatch(texto) Then
    '        Return "Fatura"
    '        'ElseIf regexNotaFiscal.IsMatch(texto) Then
    '        '    Return "Nota_Fiscal"
    '    Else
    '        Return "Nota_Fiscal"
    '    End If
    'End Function

    'Protected Sub uploadFile_Click(sender As Object, e As EventArgs)
    '    If UploadImages.HasFiles Then
    '        For Each uploadedFile As HttpPostedFile In UploadImages.PostedFiles
    '            ' uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Images/"), uploadedFile.FileName))
    '            listofuploadedfiles.Text &= String.Format("{0}<br />", uploadedFile.FileName)
    '        Next
    '    End If
    'End Sub

    'Protected Sub UploadImages_PreRender(sender As Object, e As EventArgs) Handles UploadImages.PreRender
    '    If UploadImages.HasFiles Then
    '        For Each uploadedFile As HttpPostedFile In UploadImages.PostedFiles
    '            ' uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Images/"), uploadedFile.FileName))
    '            listofuploadedfiles.Text &= String.Format("{0}<br />", uploadedFile.FileName)
    '        Next
    '    End If
    'End Sub

    'Protected Sub Upload(sender As Object, e As EventArgs)
    '    For i As Integer = 0 To Request.Files.Count - 1
    '        Dim postedFile As HttpPostedFile = Request.Files(i)
    '        If postedFile.ContentLength > 0 Then
    '            Dim fileName As String = Path.GetFileName(postedFile.FileName)
    '            postedFile.SaveAs(Server.MapPath("~/Uploads/") & fileName)
    '            lblMessage.Text += String.Format("<b>{0}</b> uploaded.<br />", fileName)
    '        End If
    '    Next
    'End Sub
End Class

