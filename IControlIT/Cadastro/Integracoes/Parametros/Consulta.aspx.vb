Imports System.Data
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Web.UI.WebControls
Imports Newtonsoft.Json

Public Class Parametros_Consulta
    Inherits System.Web.UI.Page

    Private ReadOnly wsCadastro As New WS_GUA_Cadastro.WSCadastro()

    Private Const SessionDataKey As String = "ParametrosAnimaConsulta"
    Private Const DefaultPageSize As Integer = 10

#Region "Declaracoes de Controles"
    Protected WithEvents gvParametros As GridView
    Protected WithEvents gvDetalheFatura As GridView
    Protected WithEvents ddlFiltroStatus As DropDownList
    Protected WithEvents ddlFiltroTipo As DropDownList
    Protected WithEvents txtFiltroCNPJ As TextBox
    Protected WithEvents txtFiltroConta As TextBox
    Protected WithEvents txtFiltroFornecedor As TextBox
    Protected WithEvents txtFiltroMesEmissao As TextBox
    Protected WithEvents btnPesquisar As Button
    Protected WithEvents btnLimpar As Button
    Protected WithEvents btnPreparar As Button
    Protected WithEvents ddlPageSize As DropDownList
    Protected WithEvents btnPaginaAnterior As Button
    Protected WithEvents btnPaginaProxima As Button
    Protected WithEvents lblPaginaAtual As Label
    Protected WithEvents lblTotalRegistros As Label
    Protected WithEvents hdnItensSelecionados As HiddenField
    Protected WithEvents hdnContaSelecionada As HiddenField
    Protected WithEvents hdnTotalRegistrosDetalhe As HiddenField
    Protected WithEvents hdnRegistrosExibidos As HiddenField
    Protected WithEvents ddlLinhasPorVez As DropDownList
    Protected WithEvents ddlFiltroConceito As DropDownList
    Protected WithEvents btnCarregarMais As Button
    Protected WithEvents btnMostrarTudo As Button
    Protected WithEvents btnExportarCSV As Button
    Protected WithEvents pnlAlert As Panel
    Protected WithEvents lblAlertMessage As Label
    Protected WithEvents gvPreparacao As GridView
    Protected WithEvents btnVerPreparacao As Button
#End Region

    Private Property CurrentPageIndex As Integer
        Get
            Return If(ViewState("CurrentPageIndex"), 0)
        End Get
        Set(value As Integer)
            ViewState("CurrentPageIndex") = value
        End Set
    End Property

    Private Property SortExpression As String
        Get
            Return If(ViewState("SortExpression"), String.Empty)
        End Get
        Set(value As String)
            ViewState("SortExpression") = value
        End Set
    End Property

    Private Property SortDirection As SortDirection
        Get
            Return If(ViewState("SortDirection"), WebControls.SortDirection.Ascending)
        End Get
        Set(value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property

    ''' <summary>
    ''' IDs dos parametros que foram preparados (para manter checkboxes marcados apos postback)
    ''' </summary>
    Private Property IdsSelecionadosPreparados As List(Of String)
        Get
            Return If(TryCast(ViewState("IdsSelecionadosPreparados"), List(Of String)), New List(Of String)())
        End Get
        Set(value As List(Of String))
            ViewState("IdsSelecionadosPreparados") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Conn_Banco") Is Nothing OrElse String.IsNullOrEmpty(Session("Conn_Banco").ToString()) Then
            Response.Redirect("~/DEFAULT.aspx", False)
            Return
        End If

        wsCadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        ' Registrar botao de exportar CSV para fazer postback completo (nao AJAX)
        ' Isso e necessario porque o UpdatePanel do Master nao suporta download de arquivos
        Dim sm As ScriptManager = ScriptManager.GetCurrent(Me)
        If sm IsNot Nothing AndAlso btnExportarCSV IsNot Nothing Then
            sm.RegisterPostBackControl(btnExportarCSV)
        End If

        If Not IsPostBack Then
            Session.Remove(SessionDataKey)
            If ddlPageSize IsNot Nothing Then
                Dim item = ddlPageSize.Items.FindByValue(DefaultPageSize.ToString())
                If item IsNot Nothing Then
                    ddlPageSize.ClearSelection()
                    item.Selected = True
                End If
            End If
            CurrentPageIndex = 0
            BindGrid(True)
        End If
    End Sub

    Protected Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        CurrentPageIndex = 0
        BindGrid(True)
    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        If ddlFiltroStatus IsNot Nothing Then ddlFiltroStatus.ClearSelection()
        If ddlFiltroTipo IsNot Nothing Then ddlFiltroTipo.ClearSelection()
        If txtFiltroCNPJ IsNot Nothing Then txtFiltroCNPJ.Text = String.Empty
        If txtFiltroConta IsNot Nothing Then txtFiltroConta.Text = String.Empty
        If txtFiltroFornecedor IsNot Nothing Then txtFiltroFornecedor.Text = String.Empty
        If txtFiltroMesEmissao IsNot Nothing Then txtFiltroMesEmissao.Text = String.Empty
        Session.Remove(SessionDataKey)
        CurrentPageIndex = 0
        BindGrid(True)
    End Sub

    Protected Sub ddlPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPageSize.SelectedIndexChanged
        CurrentPageIndex = 0
        BindGrid(False)
    End Sub

    Protected Sub btnPaginaAnterior_Click(sender As Object, e As EventArgs) Handles btnPaginaAnterior.Click
        If CurrentPageIndex > 0 Then
            CurrentPageIndex -= 1
            BindGrid(False)
        End If
    End Sub

    Protected Sub btnPaginaProxima_Click(sender As Object, e As EventArgs) Handles btnPaginaProxima.Click
        CurrentPageIndex += 1
        BindGrid(False)
    End Sub

    Protected Sub gvParametros_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvParametros.RowCommand
        If e.CommandName = "Excluir" Then
            Dim args() As String = e.CommandArgument.ToString().Split(";"c)
            Dim idParametro As Integer = Convert.ToInt32(args(0))
            Dim codigoReferencia As String = If(args.Length > 1, args(1), Nothing)

            ' Exclui o parametro (PURGE = exclusao permanente) - 40 parametros
            wsCadastro.Parametros_Anima(
                pPConn_Banco:=Session("Conn_Banco").ToString(),
                pAcao:="PURGE",
                pId_Parametro:=idParametro,
                pCodigo_Referencia:=codigoReferencia,
                pTipo:=Nothing,
                pCNPJ_Anima:=Nothing,
                pConta:=Nothing,
                pDescricaoServico:=Nothing,
                pMesEmissao:=Nothing,
                pRequisitioningBUId:=Nothing,
                pRequisitioningBUName:=Nothing,
                pDescription:=Nothing,
                pJustification:=Nothing,
                pPreparerEmail:=Nothing,
                pApproverEmail:=Nothing,
                pDocumentStatusCode:=Nothing,
                pRequisitionType:=Nothing,
                pSourceUniqueId:=Nothing,
                pCategoryName:=Nothing,
                pDeliverToLocationCode:=Nothing,
                pDeliverToOrganizationCode:=Nothing,
                pProcurementBUName:=Nothing,
                pItemDescription:=Nothing,
                pItemNumber:=Nothing,
                pRequesterEmail:=Nothing,
                pSupplierName:=Nothing,
                pSupplierContactName:=Nothing,
                pSupplierSiteName:=Nothing,
                pCentroDeCusto:=Nothing,
                pEstabelecimento:=Nothing,
                pUnidadeNegocio:=Nothing,
                pFinalidade:=Nothing,
                pProjeto:=Nothing,
                pInterCompany:=Nothing,
                pCodigoRequisicaoCompra:=Nothing,
                pCodigoOrdemCompra:=Nothing,
                pCodigoInvoice:=Nothing,
                pObservacao:=Nothing,
                pUsuario:=Session("Nm_Usuario").ToString(),
                pFl_Ativo:=False,
                pProcessamento_Manual:=False
            )
            Session.Remove(SessionDataKey)
            CurrentPageIndex = 0
            BindGrid(True)
        ElseIf e.CommandName = "VerDetalhe" Then
            ' Argumento agora e Conta|MesEmissao|Tipo
            Dim args() As String = e.CommandArgument.ToString().Split("|"c)
            Dim conta As String = args(0)
            Dim mesEmissao As String = If(args.Length > 1, args(1), "")
            Dim tipo As String = If(args.Length > 2, args(2), "")
            hdnContaSelecionada.Value = conta & "|" & mesEmissao & "|" & tipo

            ' Determinar filtro de conceito baseado no Tipo do registro
            ' Se ja existe registro em Anima_Parametros, usar o Tipo para filtrar
            Dim filtroConceito As String = DeterminarFiltroConceito(tipo)

            ' Atualizar dropdown para refletir o filtro aplicado
            If Not String.IsNullOrWhiteSpace(filtroConceito) Then
                ddlFiltroConceito.SelectedValue = filtroConceito
            Else
                ddlFiltroConceito.SelectedValue = ""
            End If

            ' Carregar detalhes da fatura com filtro baseado no Tipo
            CarregarDetalhesFatura(conta, mesEmissao, True, filtroConceito)
        End If
    End Sub

    ''' <summary>
    ''' Determina o filtro de conceito baseado no Tipo do registro em Anima_Parametros
    ''' - Servico-Hibrida ou Servico -> filtra por conceito iniciando com "servi"
    ''' - Telecom-Hibrida ou Telecom -> filtra por conceito iniciando com "assin"
    ''' - Tipo vazio ou nao reconhecido -> sem filtro (mostra todos)
    ''' </summary>
    Private Function DeterminarFiltroConceito(tipo As String) As String
        If String.IsNullOrWhiteSpace(tipo) Then
            Return Nothing
        End If

        ' Normalizar o tipo para comparacao (lowercase, sem acentos)
        Dim tipoNormalizado As String = tipo.Trim().ToLower()
        tipoNormalizado = tipoNormalizado.Replace(ChrW(231), "c").Replace(ChrW(199), "c")  ' cedilha
        tipoNormalizado = tipoNormalizado.Replace(ChrW(237), "i").Replace(ChrW(236), "i").Replace(ChrW(238), "i")  ' i acentuado

        ' Servico-Hibrida ou Servico -> conceito iniciando com "servi"
        If tipoNormalizado.StartsWith("servico") OrElse tipoNormalizado.StartsWith("servi") Then
            Return "s"
        End If

        ' Telecom-Hibrida ou Telecom -> conceito iniciando com "assin" (assinatura)
        If tipoNormalizado.StartsWith("telecom") Then
            Return "a"
        End If

        ' Tipo nao reconhecido -> sem filtro
        Return Nothing
    End Function

    Protected Sub ddlFiltroConceito_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' Recarregar detalhes da fatura com o filtro de conceito selecionado
        Dim contaSelecionada As String = hdnContaSelecionada.Value
        If Not String.IsNullOrWhiteSpace(contaSelecionada) Then
            Dim args() As String = contaSelecionada.Split("|"c)
            Dim conta As String = args(0)
            Dim mesEmissao As String = If(args.Length > 1, args(1), "")
            Dim filtroConceito As String = ddlFiltroConceito.SelectedValue
            CarregarDetalhesFatura(conta, mesEmissao, True, filtroConceito)
        End If
    End Sub

    Protected Sub gvParametros_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvParametros.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = TryCast(e.Row.DataItem, DataRowView)
            If drv IsNot Nothing Then
                Dim idParametro As String = If(drv("Id_Parametro") IsNot Nothing AndAlso Not Convert.IsDBNull(drv("Id_Parametro")), drv("Id_Parametro").ToString(), "0")
                Dim isFaturaOnly As Boolean = idParametro = "0" OrElse String.IsNullOrEmpty(idParametro)

                ' Obter status para definir cor de fundo
                Dim status As String = String.Empty
                If drv.Row.Table.Columns.Contains("Status") Then
                    status = If(drv("Status"), String.Empty).ToString().Trim()
                End If

                ' Preencher litStatus com texto truncado
                Dim litStatus As Literal = TryCast(e.Row.FindControl("litStatus"), Literal)
                If litStatus IsNot Nothing Then
                    Dim statusTruncado As String = TruncarTexto(status, 60)
                    If statusTruncado <> status AndAlso Not String.IsNullOrEmpty(status) Then
                        ' Texto foi truncado - mostrar tooltip com texto completo
                        litStatus.Text = $"<span class=""status-text"" title=""{Server.HtmlEncode(status)}"">{Server.HtmlEncode(statusTruncado)}</span>"
                    Else
                        litStatus.Text = $"<span class=""status-text"">{Server.HtmlEncode(status)}</span>"
                    End If
                End If

                ' Preencher litDescricao com texto truncado
                Dim descricao As String = String.Empty
                If drv.Row.Table.Columns.Contains("Descricao") Then
                    descricao = If(drv("Descricao"), String.Empty).ToString().Trim()
                End If
                Dim litDescricao As Literal = TryCast(e.Row.FindControl("litDescricao"), Literal)
                If litDescricao IsNot Nothing Then
                    Dim descricaoTruncada As String = TruncarTexto(descricao, 60)
                    If descricaoTruncada <> descricao AndAlso Not String.IsNullOrEmpty(descricao) Then
                        ' Texto foi truncado - mostrar tooltip com texto completo
                        litDescricao.Text = $"<span class=""status-text"" title=""{Server.HtmlEncode(descricao)}"">{Server.HtmlEncode(descricaoTruncada)}</span>"
                    Else
                        litDescricao.Text = $"<span class=""status-text"">{Server.HtmlEncode(descricao)}</span>"
                    End If
                End If

                ' Aplicar cor de fundo baseada no status (cores suaves)
                If status.IndexOf("Concluido", StringComparison.OrdinalIgnoreCase) >= 0 OrElse status.IndexOf("Concluído", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    ' Verde suave para concluido
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#e8f5e9")
                ElseIf status.IndexOf("Erro", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    ' Vermelho suave para erro
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffebee")
                ElseIf status.IndexOf("Nao iniciado", StringComparison.OrdinalIgnoreCase) >= 0 OrElse status.IndexOf("Não iniciado", StringComparison.OrdinalIgnoreCase) >= 0 OrElse isFaturaOnly Then
                    ' Amarelo suave para nao iniciado ou apenas na Fatura
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffde7")
                End If

                ' Configurar checkbox com data-id-parametro (apenas se tiver Id_Parametro valido)
                Dim litCheckbox As Literal = TryCast(e.Row.FindControl("litCheckbox"), Literal)
                If litCheckbox IsNot Nothing Then
                    If Not isFaturaOnly Then
                        ' Verificar se este item estava selecionado antes (para manter marcado apos Preparar)
                        Dim isChecked As String = ""
                        If IdsSelecionadosPreparados IsNot Nothing AndAlso IdsSelecionadosPreparados.Contains(idParametro) Then
                            isChecked = " checked=""checked"""
                        End If
                        litCheckbox.Text = $"<input type=""checkbox"" data-id-parametro=""{idParametro}"" onclick=""atualizarSelecao(this, '{idParametro}')""{isChecked} /><span class=""selection-number"" style=""display:none;""></span>"
                    Else
                        ' Sem checkbox para itens apenas na Fatura
                        litCheckbox.Text = ""
                    End If
                End If

                ' Configurar icone de mao para Processamento_Manual = 1
                Dim litManualFlag As Literal = TryCast(e.Row.FindControl("litManualFlag"), Literal)
                If litManualFlag IsNot Nothing Then
                    litManualFlag.Text = ""
                    If drv.Row.Table.Columns.Contains("Processamento_Manual") Then
                        Dim flManual As Object = drv("Processamento_Manual")
                        If flManual IsNot Nothing AndAlso Not Convert.IsDBNull(flManual) Then
                            Dim isManual As Boolean = False
                            If TypeOf flManual Is Boolean Then
                                isManual = Convert.ToBoolean(flManual)
                            ElseIf TypeOf flManual Is Integer OrElse TypeOf flManual Is Short OrElse TypeOf flManual Is Long Then
                                isManual = Convert.ToInt32(flManual) = 1
                            ElseIf TypeOf flManual Is String Then
                                isManual = flManual.ToString() = "1" OrElse flManual.ToString().ToLower() = "true"
                            End If
                            If isManual Then
                                ' Icone de mao (hand pointer)
                                litManualFlag.Text = "<span title=""Processamento Manual"" style=""color: #6c757d; font-size: 16px; margin-right: 6px; cursor: help;"">&#9995;</span>"
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub gvParametros_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvParametros.Sorting
        If SortExpression = e.SortExpression Then
            SortDirection = If(SortDirection = WebControls.SortDirection.Ascending, WebControls.SortDirection.Descending, WebControls.SortDirection.Ascending)
        Else
            SortExpression = e.SortExpression
            SortDirection = WebControls.SortDirection.Ascending
        End If
        BindGrid(False)
    End Sub

    Protected Sub BtnPreparar_Click(sender As Object, e As EventArgs) Handles btnPreparar.Click
        If hdnItensSelecionados Is Nothing OrElse String.IsNullOrWhiteSpace(hdnItensSelecionados.Value) Then
            MostrarAlerta("Selecione pelo menos um parametro para preparar!", "danger")
            Return
        End If

        Try
            Dim selecao As Dictionary(Of String, Integer) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Integer))(hdnItensSelecionados.Value)
            If selecao Is Nothing OrElse selecao.Count = 0 Then
                MostrarAlerta("Selecione pelo menos um parametro para preparar!", "danger")
                Return
            End If

            ' Primeiro, limpar a tabela Anima_Parametros_Preparado
            LimparTabelaPreparado()

            ' Inserir cada item selecionado na tabela Anima_Parametros_Preparado
            Dim ordem As Integer = 1
            For Each item In selecao.OrderBy(Function(x) x.Value)
                Dim idParametro As Integer = Convert.ToInt32(item.Key)
                InserirNaFilaPreparada(idParametro, ordem)
                ordem += 1
            Next

            ' Limpar selecao apos preparar
            IdsSelecionadosPreparados = New List(Of String)()

            MostrarAlerta($"Sequência preparada com sucesso! {selecao.Count} parâmetro(s) adicionado(s) à sequência.", "success")
            Session.Remove(SessionDataKey)
            BindGrid(True)

            ' Registrar script para limpar selecao completamente
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "limparSelecao", "limparSelecao();", True)

        Catch ex As Exception
            MostrarAlerta($"Erro ao preparar sequencia: {ex.Message}", "danger")
        End Try
    End Sub

    Private Sub LimparTabelaPreparado()
        ' Chamar stored procedure para limpar a tabela Anima_Parametros_Preparado
        wsCadastro.Parametros_Anima(
            pPConn_Banco:=Session("Conn_Banco").ToString(),
            pAcao:="CLEAR_PREPARADO",
            pId_Parametro:=0,
            pCodigo_Referencia:=Nothing,
            pTipo:=Nothing,
            pCNPJ_Anima:=Nothing,
            pConta:=Nothing,
            pDescricaoServico:=Nothing,
            pMesEmissao:=Nothing,
            pRequisitioningBUId:=Nothing,
            pRequisitioningBUName:=Nothing,
            pDescription:=Nothing,
            pJustification:=Nothing,
            pPreparerEmail:=Nothing,
            pApproverEmail:=Nothing,
            pDocumentStatusCode:=Nothing,
            pRequisitionType:=Nothing,
            pSourceUniqueId:=Nothing,
            pCategoryName:=Nothing,
            pDeliverToLocationCode:=Nothing,
            pDeliverToOrganizationCode:=Nothing,
            pProcurementBUName:=Nothing,
            pItemDescription:=Nothing,
            pItemNumber:=Nothing,
            pRequesterEmail:=Nothing,
            pSupplierName:=Nothing,
            pSupplierContactName:=Nothing,
            pSupplierSiteName:=Nothing,
            pCentroDeCusto:=Nothing,
            pEstabelecimento:=Nothing,
            pUnidadeNegocio:=Nothing,
            pFinalidade:=Nothing,
            pProjeto:=Nothing,
            pInterCompany:=Nothing,
            pCodigoRequisicaoCompra:=Nothing,
            pCodigoOrdemCompra:=Nothing,
            pCodigoInvoice:=Nothing,
            pObservacao:=Nothing,
            pUsuario:=Session("Nm_Usuario").ToString(),
            pFl_Ativo:=False,
            pProcessamento_Manual:=False
        )
    End Sub

    Private Sub InserirNaFilaPreparada(idParametro As Integer, ordem As Integer)
        ' Inserir na tabela Anima_Parametros_Preparado com o mesmo Id_Parametro
        wsCadastro.Parametros_Anima(
            pPConn_Banco:=Session("Conn_Banco").ToString(),
            pAcao:="INSERT_PREPARADO",
            pId_Parametro:=idParametro,
            pCodigo_Referencia:=Nothing,
            pTipo:=Nothing,
            pCNPJ_Anima:=Nothing,
            pConta:=Nothing,
            pDescricaoServico:=Nothing,
            pMesEmissao:=Nothing,
            pRequisitioningBUId:=Nothing,
            pRequisitioningBUName:=Nothing,
            pDescription:=Nothing,
            pJustification:=Nothing,
            pPreparerEmail:=Nothing,
            pApproverEmail:=Nothing,
            pDocumentStatusCode:=Nothing,
            pRequisitionType:=Nothing,
            pSourceUniqueId:=Nothing,
            pCategoryName:=Nothing,
            pDeliverToLocationCode:=Nothing,
            pDeliverToOrganizationCode:=Nothing,
            pProcurementBUName:=Nothing,
            pItemDescription:=Nothing,
            pItemNumber:=Nothing,
            pRequesterEmail:=Nothing,
            pSupplierName:=Nothing,
            pSupplierContactName:=Nothing,
            pSupplierSiteName:=Nothing,
            pCentroDeCusto:=Nothing,
            pEstabelecimento:=Nothing,
            pUnidadeNegocio:=Nothing,
            pFinalidade:=Nothing,
            pProjeto:=Nothing,
            pInterCompany:=Nothing,
            pCodigoRequisicaoCompra:=Nothing,
            pCodigoOrdemCompra:=Nothing,
            pCodigoInvoice:=Nothing,
            pObservacao:=Nothing,
            pUsuario:=Session("Nm_Usuario").ToString(),
            pFl_Ativo:=True,
            pProcessamento_Manual:=False
        )
    End Sub

    Private Sub MostrarAlerta(mensagem As String, tipo As String)
        If pnlAlert IsNot Nothing AndAlso lblAlertMessage IsNot Nothing Then
            pnlAlert.Visible = True
            pnlAlert.CssClass = $"alert alert-{tipo}"
            lblAlertMessage.Text = mensagem
        End If
    End Sub

    Private Sub BindGrid(Optional reloadData As Boolean = False)
        Dim baseTable As DataTable = GetAllParametros(reloadData)

        If ddlFiltroTipo IsNot Nothing AndAlso (reloadData OrElse ddlFiltroTipo.Items.Count <= 1) Then
            BindFiltroTipo(baseTable)
        End If

        Dim filteredTable As DataTable = AplicarFiltros(baseTable)

        ' Aplicar ordenacao
        If Not String.IsNullOrEmpty(SortExpression) AndAlso filteredTable.Rows.Count > 0 Then
            Dim sortDir As String = If(SortDirection = WebControls.SortDirection.Ascending, "ASC", "DESC")
            filteredTable.DefaultView.Sort = $"{SortExpression} {sortDir}"
            filteredTable = filteredTable.DefaultView.ToTable()
        End If

        Dim totalRegistros As Integer = filteredTable.Rows.Count

        Dim pageSize As Integer = GetPageSize()
        Dim totalPaginas As Integer = If(totalRegistros = 0, 1, CInt(Math.Ceiling(totalRegistros / CDbl(pageSize))))

        If CurrentPageIndex >= totalPaginas Then
            CurrentPageIndex = Math.Max(0, totalPaginas - 1)
        End If

        Dim pagedSource As New PagedDataSource With {
            .AllowPaging = True,
            .PageSize = pageSize,
            .CurrentPageIndex = CurrentPageIndex,
            .DataSource = filteredTable.DefaultView
        }

        gvParametros.DataSource = pagedSource
        gvParametros.DataBind()

        If lblTotalRegistros IsNot Nothing Then
            lblTotalRegistros.Text = $"{totalRegistros} registro(s) encontrado(s)"
        End If

        If lblPaginaAtual IsNot Nothing Then
            Dim paginaExibida As Integer = If(totalRegistros = 0, 0, CurrentPageIndex + 1)
            lblPaginaAtual.Text = $"{paginaExibida} / {totalPaginas}"
        End If

        If btnPaginaAnterior IsNot Nothing Then
            btnPaginaAnterior.Enabled = CurrentPageIndex > 0
        End If

        If btnPaginaProxima IsNot Nothing Then
            btnPaginaProxima.Enabled = CurrentPageIndex < totalPaginas - 1 AndAlso totalRegistros > 0
        End If
    End Sub

    Private Function GetAllParametros(Optional reload As Boolean = False) As DataTable
        Dim table As DataTable = TryCast(Session(SessionDataKey), DataTable)

        If table Is Nothing OrElse reload Then
            ' Buscar dados combinados de Fatura e Anima_Parametros usando SELECT_WITH_FATURA
            Using ds As DataSet = wsCadastro.Parametros_Anima(
                pPConn_Banco:=Session("Conn_Banco").ToString(),
                pAcao:="SELECT_WITH_FATURA",
                pId_Parametro:=0,
                pCodigo_Referencia:=Nothing,
                pTipo:=Nothing,
                pCNPJ_Anima:=Nothing,
                pConta:=Nothing,
                pDescricaoServico:=Nothing,
                pMesEmissao:=Nothing,
                pRequisitioningBUId:=Nothing,
                pRequisitioningBUName:=Nothing,
                pDescription:=Nothing,
                pJustification:=Nothing,
                pPreparerEmail:=Nothing,
                pApproverEmail:=Nothing,
                pDocumentStatusCode:=Nothing,
                pRequisitionType:=Nothing,
                pSourceUniqueId:=Nothing,
                pCategoryName:=Nothing,
                pDeliverToLocationCode:=Nothing,
                pDeliverToOrganizationCode:=Nothing,
                pProcurementBUName:=Nothing,
                pItemDescription:=Nothing,
                pItemNumber:=Nothing,
                pRequesterEmail:=Nothing,
                pSupplierName:=Nothing,
                pSupplierContactName:=Nothing,
                pSupplierSiteName:=Nothing,
                pCentroDeCusto:=Nothing,
                pEstabelecimento:=Nothing,
                pUnidadeNegocio:=Nothing,
                pFinalidade:=Nothing,
                pProjeto:=Nothing,
                pInterCompany:=Nothing,
                pCodigoRequisicaoCompra:=Nothing,
                pCodigoOrdemCompra:=Nothing,
                pCodigoInvoice:=Nothing,
                pObservacao:=Nothing,
                pUsuario:=Nothing,
                pFl_Ativo:=False,
                pProcessamento_Manual:=False
            )

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                    table = ds.Tables(0).Copy()
                Else
                    table = New DataTable()
                End If
            End Using

            Session(SessionDataKey) = table
        End If

        Return table
    End Function

    Private Sub BindFiltroTipo(dados As DataTable)
        If ddlFiltroTipo Is Nothing Then
            Return
        End If

        Dim valorSelecionado As String = ddlFiltroTipo.SelectedValue

        ddlFiltroTipo.Items.Clear()
        ddlFiltroTipo.Items.Add(New ListItem("-- Todos --", ""))

        If dados IsNot Nothing AndAlso dados.Rows.Count > 0 AndAlso dados.Columns.Contains("Tipo") Then
            Dim tipos = dados.AsEnumerable().
                Select(Function(r) If(r.Field(Of String)("Tipo"), String.Empty).Trim()).
                Where(Function(v) Not String.IsNullOrWhiteSpace(v)).
                Distinct(StringComparer.OrdinalIgnoreCase).
                OrderBy(Function(v) v, StringComparer.OrdinalIgnoreCase)

            For Each tipo In tipos
                ddlFiltroTipo.Items.Add(New ListItem(tipo, tipo))
            Next
        End If

        Dim item = ddlFiltroTipo.Items.FindByValue(valorSelecionado)
        If item IsNot Nothing Then
            ddlFiltroTipo.ClearSelection()
            item.Selected = True
        End If
    End Sub

    Private Function AplicarFiltros(dados As DataTable) As DataTable
        If dados Is Nothing OrElse dados.Rows.Count = 0 Then
            Return If(dados IsNot Nothing, dados.Clone(), New DataTable())
        End If

        Dim registros = dados.AsEnumerable()

        ' Filtro por Status (texto)
        If ddlFiltroStatus IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(ddlFiltroStatus.SelectedValue) Then
            Dim statusFiltro = ddlFiltroStatus.SelectedValue
            If dados.Columns.Contains("Status") Then
                If statusFiltro = "0" Then
                    ' Pendente = nao concluido
                    registros = registros.Where(Function(r)
                                                    Dim st = If(r("Status"), String.Empty).ToString()
                                                    Return st.IndexOf("Concluido", StringComparison.OrdinalIgnoreCase) < 0 AndAlso st.IndexOf("Concluído", StringComparison.OrdinalIgnoreCase) < 0
                                                End Function)
                ElseIf statusFiltro = "1" Then
                    ' Completo = concluido
                    registros = registros.Where(Function(r)
                                                    Dim st = If(r("Status"), String.Empty).ToString()
                                                    Return st.IndexOf("Concluido", StringComparison.OrdinalIgnoreCase) >= 0 OrElse st.IndexOf("Concluído", StringComparison.OrdinalIgnoreCase) >= 0
                                                End Function)
                End If
            End If
        End If

        ' Filtro por Tipo
        If ddlFiltroTipo IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(ddlFiltroTipo.SelectedValue) Then
            Dim tipoFiltro = ddlFiltroTipo.SelectedValue.Trim()
            If dados.Columns.Contains("Tipo") Then
                registros = registros.Where(Function(r) String.Equals(If(r.Field(Of String)("Tipo"), String.Empty).Trim(), tipoFiltro, StringComparison.OrdinalIgnoreCase))
            End If
        End If

        ' Filtro por CNPJ
        If txtFiltroCNPJ IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(txtFiltroCNPJ.Text) Then
            Dim cnpjFiltro = Regex.Replace(txtFiltroCNPJ.Text, "[^\d]", String.Empty)
            If cnpjFiltro.Length > 0 AndAlso dados.Columns.Contains("CNPJ_Anima") Then
                registros = registros.Where(Function(r) Regex.Replace(If(r.Field(Of String)("CNPJ_Anima"), String.Empty), "[^\d]", String.Empty).Contains(cnpjFiltro))
            End If
        End If

        ' Filtro por Conta
        Dim contaFiltro = If(txtFiltroConta IsNot Nothing, txtFiltroConta.Text, String.Empty).Trim()
        If Not String.IsNullOrWhiteSpace(contaFiltro) AndAlso dados.Columns.Contains("Conta") Then
            registros = registros.Where(Function(r) If(r.Field(Of String)("Conta"), String.Empty).IndexOf(contaFiltro, StringComparison.OrdinalIgnoreCase) >= 0)
        End If

        ' Filtro por Fornecedor
        Dim fornecedorFiltro = If(txtFiltroFornecedor IsNot Nothing, txtFiltroFornecedor.Text, String.Empty).Trim()
        If Not String.IsNullOrWhiteSpace(fornecedorFiltro) AndAlso dados.Columns.Contains("SupplierName") Then
            registros = registros.Where(Function(r) If(r.Field(Of String)("SupplierName"), String.Empty).IndexOf(fornecedorFiltro, StringComparison.OrdinalIgnoreCase) >= 0)
        End If

        ' Filtro por Mes Emissao
        Dim mesEmissaoFiltro = If(txtFiltroMesEmissao IsNot Nothing, txtFiltroMesEmissao.Text, String.Empty).Trim()
        If Not String.IsNullOrWhiteSpace(mesEmissaoFiltro) AndAlso dados.Columns.Contains("MesEmissao") Then
            registros = registros.Where(Function(r) If(r.Field(Of String)("MesEmissao"), String.Empty).IndexOf(mesEmissaoFiltro, StringComparison.OrdinalIgnoreCase) >= 0)
        End If

        Dim lista = registros.ToList()
        If lista.Count = 0 Then
            Return dados.Clone()
        End If

        Return lista.CopyToDataTable()
    End Function

    Private Function GetPageSize() As Integer
        Dim tamanho As Integer = DefaultPageSize
        If ddlPageSize IsNot Nothing AndAlso Integer.TryParse(ddlPageSize.SelectedValue, tamanho) Then
            If tamanho <= 0 Then tamanho = DefaultPageSize
        End If
        Return tamanho
    End Function

    ''' <summary>
    ''' Trunca o texto para o tamanho maximo especificado, mantendo a ultima palavra inteira e adicionando "..."
    ''' </summary>
    Private Function TruncarTexto(texto As String, tamanhoMaximo As Integer) As String
        If String.IsNullOrEmpty(texto) OrElse texto.Length <= tamanhoMaximo Then
            Return texto
        End If

        ' Encontrar a posicao para cortar (manter ultima palavra inteira)
        Dim textoTruncado As String = texto.Substring(0, tamanhoMaximo)
        Dim ultimoEspaco As Integer = textoTruncado.LastIndexOf(" "c)

        If ultimoEspaco > 0 Then
            ' Cortar na ultima palavra completa
            textoTruncado = textoTruncado.Substring(0, ultimoEspaco)
        End If

        Return textoTruncado.TrimEnd() & "..."
    End Function

    ''' <summary>
    ''' Gera o link de edicao/cadastro considerando Id_Parametro, Conta e MesEmissao
    ''' </summary>
    Protected Function ObterLinkEdicao(idParametro As Object, conta As Object, mesEmissao As Object) As String
        If idParametro IsNot Nothing AndAlso Not Convert.IsDBNull(idParametro) AndAlso Convert.ToInt32(idParametro) > 0 Then
            Return $"Cadastro.aspx?id={idParametro}"
        Else
            Dim contaStr = Server.UrlEncode(If(conta, String.Empty).ToString())
            Dim mesStr = Server.UrlEncode(If(mesEmissao, String.Empty).ToString())
            Return $"Cadastro.aspx?conta={contaStr}&mesEmissao={mesStr}"
        End If
    End Function

'===============================================================================
'  ANIMA - INICIO DO CODIGO ESPECIFICO
'  Detalhamento de Fatura - Consulta cn_Detalhamento_Bilhete_API via WSCadastro
'===============================================================================
#Region "Detalhes da Fatura - ANIMA"
    Private Sub CarregarDetalhesFatura(conta As String, mesEmissao As String, novaConsulta As Boolean, Optional filtroConceito As String = Nothing)
        Try
            ' Armazenar a conta e mes selecionados
            hdnContaSelecionada.Value = conta & "|" & mesEmissao

            ' Converter mesEmissao para YYYYMM
            ' Pode vir no formato MM/AAAA (12/2025) ou YYYYMM (202512)
            Dim dtLote As String = Nothing
            If Not String.IsNullOrWhiteSpace(mesEmissao) Then
                If mesEmissao.Contains("/") Then
                    ' Formato MM/AAAA (ex: 12/2025)
                    Dim partes = mesEmissao.Split("/"c)
                    If partes.Length = 2 Then
                        Dim mes = partes(0).Trim()
                        Dim ano = partes(1).Trim()
                        dtLote = ano & mes ' YYYYMM (ex: 202512)
                    End If
                ElseIf mesEmissao.Length = 6 AndAlso IsNumeric(mesEmissao) Then
                    ' Ja esta no formato YYYYMM (ex: 202512)
                    dtLote = mesEmissao
                End If
            End If

            ' DEBUG: Log para verificar os valores (REMOVER APOS TESTE)
            System.Diagnostics.Debug.WriteLine($"[ANIMA DEBUG] CarregarDetalhesFatura - conta={conta}, mesEmissao={mesEmissao}, dtLote={dtLote}, conceito={filtroConceito}")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "debugLog", $"console.log('[ANIMA DEBUG] conta={conta}, mesEmissao={mesEmissao}, dtLote={dtLote}, conceito={filtroConceito}');", True)

            ' Chamar o web service para obter detalhamento da fatura com filtro de periodo
            Dim ds As DataSet = wsCadastro.Anima_Detalhamento_Fatura(
                pPConn_Banco:=Session("Conn_Banco").ToString(),
                pNr_Fatura:=conta,
                pDt_Lote:=dtLote
            )

            Dim tabela As DataTable = Nothing
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                tabela = ds.Tables(0)
                ' Corrigir encoding UTF-8 mal formado nos dados
                CorrigirEncodingDataTable(tabela)
                ' Aplicar filtro por conceito se informado
                If Not String.IsNullOrWhiteSpace(filtroConceito) Then
                    tabela = FiltrarPorConceito(tabela, filtroConceito)
                End If
            End If

            Dim totalRegistros As Integer = If(tabela IsNot Nothing, tabela.Rows.Count, 0)
            hdnTotalRegistrosDetalhe.Value = totalRegistros.ToString()

            If totalRegistros > 0 Then
                ' Limitar linhas inicialmente
                Dim linhasPorVez As Integer = Integer.Parse(ddlLinhasPorVez.SelectedValue)
                Dim registrosExibir As Integer = Math.Min(linhasPorVez, totalRegistros)

                ' Armazenar dados na sessao para paginacao
                Session("DetalheFaturaData") = tabela

                ' Criar view com as primeiras linhas
                Dim tabelaExibir As DataTable = tabela.Clone()
                For i As Integer = 0 To registrosExibir - 1
                    tabelaExibir.ImportRow(tabela.Rows(i))
                Next

                gvDetalheFatura.DataSource = tabelaExibir
                gvDetalheFatura.DataBind()

                hdnRegistrosExibidos.Value = registrosExibir.ToString()

                ' Script para mostrar tabela com dados
                Dim script As String = $"abrirModal('{Server.HtmlEncode(conta)}'); mostrarTabela({registrosExibir}, {totalRegistros});"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal", script, True)
            Else
                gvDetalheFatura.DataSource = Nothing
                gvDetalheFatura.DataBind()
                hdnRegistrosExibidos.Value = "0"

                ' Script para mostrar mensagem de sem dados
                Dim script As String = $"abrirModal('{Server.HtmlEncode(conta)}'); mostrarSemDados();"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal", script, True)
            End If

        Catch ex As Exception
            ' Em caso de erro, mostrar modal com mensagem
            Dim script As String = $"abrirModal('{Server.HtmlEncode(conta)}'); mostrarSemDados(); console.error('Erro ao carregar detalhes: {ex.Message.Replace("'", "\'")}');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal", script, True)
        End Try
    End Sub

    Protected Sub btnCarregarMais_Click(sender As Object, e As EventArgs) Handles btnCarregarMais.Click
        Try
            Dim tabela As DataTable = TryCast(Session("DetalheFaturaData"), DataTable)
            If tabela Is Nothing Then Return

            Dim totalRegistros As Integer = tabela.Rows.Count
            Dim registrosExibidos As Integer = Integer.Parse(hdnRegistrosExibidos.Value)
            Dim linhasPorVez As Integer = Integer.Parse(ddlLinhasPorVez.SelectedValue)

            Dim novosRegistros As Integer = Math.Min(registrosExibidos + linhasPorVez, totalRegistros)

            ' Criar view com mais linhas
            Dim tabelaExibir As DataTable = tabela.Clone()
            For i As Integer = 0 To novosRegistros - 1
                tabelaExibir.ImportRow(tabela.Rows(i))
            Next

            gvDetalheFatura.DataSource = tabelaExibir
            gvDetalheFatura.DataBind()

            hdnRegistrosExibidos.Value = novosRegistros.ToString()

            ' Script para atualizar contadores
            Dim script As String = $"mostrarTabela({novosRegistros}, {totalRegistros});"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "atualizarModal", script, True)
        Catch ex As Exception
            ' Ignorar erros
        End Try
    End Sub

    Protected Sub btnMostrarTudo_Click(sender As Object, e As EventArgs) Handles btnMostrarTudo.Click
        Try
            Dim tabela As DataTable = TryCast(Session("DetalheFaturaData"), DataTable)
            If tabela Is Nothing Then Return

            Dim totalRegistros As Integer = tabela.Rows.Count

            gvDetalheFatura.DataSource = tabela
            gvDetalheFatura.DataBind()

            hdnRegistrosExibidos.Value = totalRegistros.ToString()

            ' Script para atualizar contadores
            Dim script As String = $"mostrarTabela({totalRegistros}, {totalRegistros});"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "atualizarModal", script, True)
        Catch ex As Exception
            ' Ignorar erros
        End Try
    End Sub

    Protected Sub btnExportarCSV_Click(sender As Object, e As EventArgs) Handles btnExportarCSV.Click
        Try
            Dim tabela As DataTable = TryCast(Session("DetalheFaturaData"), DataTable)
            If tabela Is Nothing OrElse tabela.Rows.Count = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertCSV", "alert('Nenhum dado disponivel para exportar. Por favor, abra os detalhes da fatura primeiro.');", True)
                Return
            End If

            Dim conta As String = hdnContaSelecionada.Value
            Dim sb As New System.Text.StringBuilder()

            ' Cabecalho
            Dim headers As New List(Of String)()
            For Each col As DataColumn In tabela.Columns
                headers.Add("""" & col.ColumnName.Replace("""", """""") & """")
            Next
            sb.AppendLine(String.Join(";", headers))

            ' Dados
            For Each row As DataRow In tabela.Rows
                Dim values As New List(Of String)()
                For Each col As DataColumn In tabela.Columns
                    Dim valor As String = If(row(col) Is DBNull.Value, "", row(col).ToString())
                    values.Add("""" & valor.Replace("""", """""") & """")
                Next
                sb.AppendLine(String.Join(";", values))
            Next

            ' Enviar arquivo - usando Buffer e Flush para garantir download
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "text/csv; charset=utf-8"
            Response.Charset = "utf-8"
            Response.ContentEncoding = System.Text.Encoding.UTF8
            ' Adicionar BOM para Excel reconhecer UTF-8
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble())
            Response.AddHeader("Content-Disposition", $"attachment; filename=Detalhamento_Fatura_{conta}_{DateTime.Now:yyyyMMdd_HHmmss}.csv")
            Response.Write(sb.ToString())
            Response.Flush()
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        Catch ex As Threading.ThreadAbortException
            ' Ignorar ThreadAbortException (esperado com Response.End)
        Catch ex As Exception
            ' Log erro se necessario
        End Try
    End Sub

    Protected Sub btnVerPreparacao_Click(sender As Object, e As EventArgs) Handles btnVerPreparacao.Click
        CarregarDadosPreparacao()
    End Sub

    Private Sub CarregarDadosPreparacao()
        Try
            ' Buscar dados da tabela Anima_Parametros_Preparado usando SELECT_PREPARADO
            Using ds As DataSet = wsCadastro.Parametros_Anima(
                pPConn_Banco:=Session("Conn_Banco").ToString(),
                pAcao:="SELECT_PREPARADO",
                pId_Parametro:=0,
                pCodigo_Referencia:=Nothing,
                pTipo:=Nothing,
                pCNPJ_Anima:=Nothing,
                pConta:=Nothing,
                pDescricaoServico:=Nothing,
                pMesEmissao:=Nothing,
                pRequisitioningBUId:=Nothing,
                pRequisitioningBUName:=Nothing,
                pDescription:=Nothing,
                pJustification:=Nothing,
                pPreparerEmail:=Nothing,
                pApproverEmail:=Nothing,
                pDocumentStatusCode:=Nothing,
                pRequisitionType:=Nothing,
                pSourceUniqueId:=Nothing,
                pCategoryName:=Nothing,
                pDeliverToLocationCode:=Nothing,
                pDeliverToOrganizationCode:=Nothing,
                pProcurementBUName:=Nothing,
                pItemDescription:=Nothing,
                pItemNumber:=Nothing,
                pRequesterEmail:=Nothing,
                pSupplierName:=Nothing,
                pSupplierContactName:=Nothing,
                pSupplierSiteName:=Nothing,
                pCentroDeCusto:=Nothing,
                pEstabelecimento:=Nothing,
                pUnidadeNegocio:=Nothing,
                pFinalidade:=Nothing,
                pProjeto:=Nothing,
                pInterCompany:=Nothing,
                pCodigoRequisicaoCompra:=Nothing,
                pCodigoOrdemCompra:=Nothing,
                pCodigoInvoice:=Nothing,
                pObservacao:=Nothing,
                pUsuario:=Nothing,
                pFl_Ativo:=False,
                pProcessamento_Manual:=False
            )

                Dim totalRegistros As Integer = 0
                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                    gvPreparacao.DataSource = ds.Tables(0)
                    gvPreparacao.DataBind()
                    totalRegistros = ds.Tables(0).Rows.Count
                Else
                    gvPreparacao.DataSource = Nothing
                    gvPreparacao.DataBind()
                End If

                ' Script para abrir o modal e mostrar a tabela ou mensagem de sem dados
                ' E garantir que o botao Preparar continua desabilitado
                Dim script As String = $"document.getElementById('modalPreparacao').classList.add('active'); mostrarTabelaPreparacao({totalRegistros}); atualizarEstadoBotaoPreparar();"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mostrarPreparacao", script, True)
            End Using
        Catch ex As Exception
            Dim script As String = "document.getElementById('modalPreparacao').classList.add('active'); mostrarTabelaPreparacao(0); atualizarEstadoBotaoPreparar();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mostrarPreparacao", script, True)
        End Try
    End Sub

    ''' <summary>
    ''' Filtra o DataTable pelo campo Conceito
    ''' - "s" ou "servico": retorna registros onde Conceito comeca com "servico"
    ''' - "a" ou "assinatura": retorna registros onde Conceito comeca com "assinatura"
    ''' </summary>
    Private Function FiltrarPorConceito(tabela As DataTable, filtroConceito As String) As DataTable
        If tabela Is Nothing OrElse tabela.Rows.Count = 0 Then
            Return tabela
        End If

        ' Verificar se a coluna Conceito existe
        If Not tabela.Columns.Contains("Conceito") Then
            Return tabela
        End If

        ' Normalizar o filtro
        Dim filtro As String = filtroConceito.Trim().ToLower()

        ' Determinar o prefixo a buscar
        ' Usa "servi" para evitar problemas com encoding do cedilha
        Dim prefixoBusca As String = Nothing
        If filtro = "s" OrElse filtro.StartsWith("servi") Then
            prefixoBusca = "servi"
        ElseIf filtro = "a" OrElse filtro.StartsWith("assin") Then
            prefixoBusca = "assin"
        Else
            ' Filtro nao reconhecido, retornar sem filtrar
            Return tabela
        End If

        ' Criar tabela filtrada com mesma estrutura
        Dim tabelaFiltrada As DataTable = tabela.Clone()

        For Each row As DataRow In tabela.Rows
            If Not row.IsNull("Conceito") Then
                ' Normalizar para comparacao (ja foi corrigido encoding antes)
                Dim conceito As String = row("Conceito").ToString().Trim().ToLower()
                ' Remover acentos para comparacao
                conceito = conceito.Replace(ChrW(231), "c").Replace(ChrW(199), "c")  ' cedilha
                conceito = conceito.Replace(ChrW(225), "a").Replace(ChrW(224), "a").Replace(ChrW(227), "a").Replace(ChrW(226), "a")
                conceito = conceito.Replace(ChrW(233), "e").Replace(ChrW(232), "e").Replace(ChrW(234), "e")
                conceito = conceito.Replace(ChrW(237), "i").Replace(ChrW(236), "i").Replace(ChrW(238), "i")
                conceito = conceito.Replace(ChrW(243), "o").Replace(ChrW(242), "o").Replace(ChrW(245), "o").Replace(ChrW(244), "o")
                conceito = conceito.Replace(ChrW(250), "u").Replace(ChrW(249), "u").Replace(ChrW(251), "u")

                If conceito.StartsWith(prefixoBusca) Then
                    tabelaFiltrada.ImportRow(row)
                End If
            End If
        Next

        Return tabelaFiltrada
    End Function

    ''' <summary>
    ''' Corrige strings com encoding UTF-8 mal formado (double-encoded)
    ''' Isso ocorre quando dados UTF-8 sao gravados em colunas VARCHAR Latin1
    ''' Usa ChrW() para evitar problemas de encoding no codigo fonte
    ''' </summary>
    Private Function CorrigirEncoding(valor As Object) As String
        If valor Is Nothing OrElse valor Is DBNull.Value Then
            Return Nothing
        End If

        Dim texto As String = valor.ToString()
        If String.IsNullOrEmpty(texto) Then
            Return texto
        End If

        ' Mapeamento de sequencias UTF-8 mal formadas para caracteres corretos
        ' ChrW(195) = "Ã" (primeiro byte de UTF-8 para caracteres acentuados)
        Dim correcoes As New Dictionary(Of String, String)()

        ' Minusculas acentuadas (a)
        correcoes.Add(ChrW(195) & ChrW(161), ChrW(225))  ' a com acento agudo
        correcoes.Add(ChrW(195) & ChrW(160), ChrW(224))  ' a com acento grave
        correcoes.Add(ChrW(195) & ChrW(163), ChrW(227))  ' a com til
        correcoes.Add(ChrW(195) & ChrW(162), ChrW(226))  ' a com circunflexo

        ' Minusculas acentuadas (e)
        correcoes.Add(ChrW(195) & ChrW(169), ChrW(233))  ' e com acento agudo
        correcoes.Add(ChrW(195) & ChrW(168), ChrW(232))  ' e com acento grave
        correcoes.Add(ChrW(195) & ChrW(170), ChrW(234))  ' e com circunflexo

        ' Minusculas acentuadas (i)
        correcoes.Add(ChrW(195) & ChrW(173), ChrW(237))  ' i com acento agudo
        correcoes.Add(ChrW(195) & ChrW(172), ChrW(236))  ' i com acento grave
        correcoes.Add(ChrW(195) & ChrW(174), ChrW(238))  ' i com circunflexo

        ' Minusculas acentuadas (o)
        correcoes.Add(ChrW(195) & ChrW(179), ChrW(243))  ' o com acento agudo
        correcoes.Add(ChrW(195) & ChrW(178), ChrW(242))  ' o com acento grave
        correcoes.Add(ChrW(195) & ChrW(181), ChrW(245))  ' o com til
        correcoes.Add(ChrW(195) & ChrW(180), ChrW(244))  ' o com circunflexo

        ' Minusculas acentuadas (u)
        correcoes.Add(ChrW(195) & ChrW(186), ChrW(250))  ' u com acento agudo
        correcoes.Add(ChrW(195) & ChrW(185), ChrW(249))  ' u com acento grave
        correcoes.Add(ChrW(195) & ChrW(187), ChrW(251))  ' u com circunflexo

        ' Cedilha
        correcoes.Add(ChrW(195) & ChrW(167), ChrW(231))  ' c cedilha minusculo
        correcoes.Add(ChrW(195) & ChrW(135), ChrW(199))  ' C cedilha maiusculo

        ' Outros caracteres especiais
        correcoes.Add(ChrW(195) & ChrW(177), ChrW(241))  ' n com til minusculo
        correcoes.Add(ChrW(195) & ChrW(145), ChrW(209))  ' N com til maiusculo
        correcoes.Add(ChrW(195) & ChrW(188), ChrW(252))  ' u com trema minusculo
        correcoes.Add(ChrW(195) & ChrW(156), ChrW(220))  ' U com trema maiusculo

        ' Maiusculas acentuadas (A)
        correcoes.Add(ChrW(195) & ChrW(129), ChrW(193))  ' A com acento agudo
        correcoes.Add(ChrW(195) & ChrW(128), ChrW(192))  ' A com acento grave
        correcoes.Add(ChrW(195) & ChrW(131), ChrW(195))  ' A com til
        correcoes.Add(ChrW(195) & ChrW(130), ChrW(194))  ' A com circunflexo

        ' Maiusculas acentuadas (E)
        correcoes.Add(ChrW(195) & ChrW(137), ChrW(201))  ' E com acento agudo
        correcoes.Add(ChrW(195) & ChrW(136), ChrW(200))  ' E com acento grave
        correcoes.Add(ChrW(195) & ChrW(138), ChrW(202))  ' E com circunflexo

        ' Maiusculas acentuadas (I)
        correcoes.Add(ChrW(195) & ChrW(141), ChrW(205))  ' I com acento agudo
        correcoes.Add(ChrW(195) & ChrW(140), ChrW(204))  ' I com acento grave
        correcoes.Add(ChrW(195) & ChrW(142), ChrW(206))  ' I com circunflexo

        ' Maiusculas acentuadas (O)
        correcoes.Add(ChrW(195) & ChrW(147), ChrW(211))  ' O com acento agudo
        correcoes.Add(ChrW(195) & ChrW(146), ChrW(210))  ' O com acento grave
        correcoes.Add(ChrW(195) & ChrW(149), ChrW(213))  ' O com til
        correcoes.Add(ChrW(195) & ChrW(148), ChrW(212))  ' O com circunflexo

        ' Maiusculas acentuadas (U)
        correcoes.Add(ChrW(195) & ChrW(154), ChrW(218))  ' U com acento agudo
        correcoes.Add(ChrW(195) & ChrW(153), ChrW(217))  ' U com acento grave
        correcoes.Add(ChrW(195) & ChrW(155), ChrW(219))  ' U com circunflexo

        For Each correcao In correcoes
            texto = texto.Replace(correcao.Key, correcao.Value)
        Next

        Return texto
    End Function

    ''' <summary>
    ''' Aplica correção de encoding em todas as colunas string de um DataTable
    ''' </summary>
    Private Sub CorrigirEncodingDataTable(tabela As DataTable)
        If tabela Is Nothing Then Return

        For Each row As DataRow In tabela.Rows
            For Each col As DataColumn In tabela.Columns
                If col.DataType Is GetType(String) AndAlso Not row.IsNull(col) Then
                    row(col) = CorrigirEncoding(row(col))
                End If
            Next
        Next
    End Sub
#End Region
'===============================================================================
'  ANIMA - FIM DO CODIGO ESPECIFICO
'===============================================================================

End Class
