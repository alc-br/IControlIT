
Public Class Consulta_Contrato
    Inherits System.Web.UI.Page
    Dim WS_Contrato As New WS_GUA_Contrato.WSContrato
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo

    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Contrato.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Contrato ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Call Master.Localizar("sp_Drop_Contrato_Detalhe", Page.AppRelativeVirtualPath.ToString)
            Call Master.Pesquisar_Contrato_Tabela(True, False)
            Call Master.Localizar("sp_Drop_Contrato", Page.AppRelativeVirtualPath.ToString)

            Session("DataSet_Produto") = Nothing
            Session("DataSet_SLA") = Nothing
            Session("DataSet_Aditivo") = Nothing
            Session("DataSet_Conta") = Nothing

            '-----monta detalhamento do botao dinamico
            dtgLista.DataSource = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Botoes_Dinamicos_Contrato", Nothing, Nothing, Nothing, Nothing, Nothing)
            dtgLista.DataBind()

            If dtgLista.Items.Count = 0 Then
                btAlerta.Enabled = True
                btAlerta.Style.Add("Opacity", "0.1")
            End If

            If Not Request("ID") = Nothing Then
                '-----contrato
                hfdID.Value = Request("ID")

                vdataset = WS_Contrato.Contrato(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing,
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                    Nothing, Nothing, "sp_SL_View", True)


                btPDF.OnClientClick = "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & vdataset.Tables(0).Rows(0).Item("Id_Contrato") & "&pTabela=Contrato','','resizable=yes, menubar=yes, scrollbars=no,height=768px, width=1024px, top=10, left=10');return false;"
                txtNumeroContrato.Text = vdataset.Tables(0).Rows(0).Item("Nr_Contrato")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Descricao")

                txtSContratante.Text = vdataset.Tables(0).Rows(0).Item("Empresa")
                txtSEmpresa.Text = vdataset.Tables(0).Rows(0).Item("Nm_Empresa_Contratada")
                txtSFilial.Text = vdataset.Tables(0).Rows(0).Item("Nm_Filial")
                txtSFimVigencia.Text = vdataset.Tables(0).Rows(0).Item("Dt_Fim_Vigencia")
                txtSInicioVigencia.Text = vdataset.Tables(0).Rows(0).Item("Dt_Inicio_Vigencia")
                txtsObjeto.Text = vdataset.Tables(0).Rows(0).Item("Objeto")
                lblSServico.Text = vdataset.Tables(0).Rows(0).Item("Nm_Servico")
                txtSStatus.Text = vdataset.Tables(0).Rows(0).Item("Nm_Contrato_Status")

                '-----produto
                Session("DataSet_Produto") = WS_Contrato.Contrato(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing,
                                                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                        Nothing, Nothing, "sp_SL_Consulta_Produto", True)
                dtgProduto.DataSource = Session("DataSet_Produto")
                dtgProduto.DataBind()

                '-----SLA
                Session("DataSet_SLA") = WS_Contrato.Contrato(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing,
                                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                    Nothing, Nothing, "sp_SL_Consulta_SLA", True)
                dtgSLA.DataSource = Session("DataSet_SLA")
                dtgSLA.DataBind()

                '-----Aditivo
                Session("DataSet_Aditivo") = WS_Contrato.Contrato(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing,
                                                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                        Nothing, Nothing, "sp_SL_Consulta_Aditivo", True)
                dtgAditivo.DataSource = Session("DataSet_Aditivo")
                dtgAditivo.DataBind()

                '-----Conta
                Session("DataSet_Conta") = WS_Contrato.Contrato(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing,
                                                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                        Nothing, Nothing, "sp_SL_Consulta_Conta", True)
                dtgConta.DataSource = Session("DataSet_Conta")
                dtgConta.DataBind()
            Else
                ' [INÍCIO - ICTRL-NF-202509-002]
                ' Popula os dropdowns de filtro
                Call PopularFiltros()

                ' Quando não tem ID, verifica se veio com filtro de vencimento
                If Not String.IsNullOrEmpty(Request.QueryString("vencimento")) Then
                    ' O dropdown já foi selecionado em PopularFiltros()
                    ' Agora busca os contratos com o filtro
                    Call BuscarContratos()
                End If
                ' [FIM - ICTRL-NF-202509-002]
            End If
        End If
    End Sub

    Protected Sub dtgProduto_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgProduto.PageIndexChanged
        dtgProduto.CurrentPageIndex = e.NewPageIndex
        dtgProduto.DataSource = Session("DataSet_Produto")
        dtgProduto.DataBind()
    End Sub

    Protected Sub dtgSLA_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSLA.PageIndexChanged
        dtgSLA.CurrentPageIndex = e.NewPageIndex
        dtgSLA.DataSource = Session("DataSet_SLA")
        dtgSLA.DataBind()
    End Sub

    Protected Sub dtgAditivo_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAditivo.PageIndexChanged
        dtgAditivo.CurrentPageIndex = e.NewPageIndex
        dtgAditivo.DataSource = Session("DataSet_Aditivo")
        dtgAditivo.DataBind()
    End Sub

    Protected Sub dtgConta_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgConta.PageIndexChanged
        dtgConta.CurrentPageIndex = e.NewPageIndex
        dtgConta.DataSource = Session("DataSet_Conta")
        dtgConta.DataBind()
    End Sub

    Protected Sub btCapa_Click(sender As Object, e As EventArgs) Handles btCapa.Click
        Response.Redirect("~/Contrato/Contrato.aspx?ID=" + hfdID.Value)
    End Sub

    Protected Sub btProduto_Click(sender As Object, e As EventArgs) Handles btProduto.Click
        Response.Redirect("~/Contrato/Contrato_SLA_Servico.aspx")
    End Sub

    Protected Sub btSLA_Click(sender As Object, e As EventArgs) Handles btSLA.Click
        Response.Redirect("~/Contrato/Contrato_SLA_Operacao.aspx")
    End Sub

    Protected Sub btAditivo_Click(sender As Object, e As EventArgs) Handles btAditivo.Click
        Response.Redirect("~/Contrato/Contrato_Aditivo.aspx")
    End Sub

    Protected Sub btDFechar_Click(sender As Object, e As EventArgs) Handles btDFechar.Click
        pnlMsg.Visible = False
    End Sub
    Protected Sub btnHome_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub
    Protected Sub btnProduto_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub
    Protected Sub btnSla_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub
    Protected Sub btnAditivo_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub
    Protected Sub btnContas_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub
    Protected Sub btAlerta_Click(sender As Object, e As EventArgs)
        pnlMsg.Visible = True
    End Sub

    Protected Sub Valida_Tab(ByVal btn As Button)

        divCapa.Visible = False
        divServico.Visible = False
        divSla.Visible = False
        divAditivo.Visible = False
        divContas.Visible = False
        btnHome.CssClass = "btn-tab-disable pull-left"
        btnProduto.CssClass = "btn-tab-disable pull-left"
        btnSla.CssClass = "btn-tab-disable pull-left"
        btnAditivo.CssClass = "btn-tab-disable pull-left"
        btnContas.CssClass = "btn-tab-disable pull-left"

        If btn.Text = "Capa" Then
            divCapa.Visible = True
            btnHome.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Serviço" Then
            divServico.Visible = True
            btnProduto.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "SLA" Then
            divSla.Visible = True
            btnSla.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Aditivo" Then
            divAditivo.Visible = True
            btnAditivo.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Contas" Then
            divContas.Visible = True
            btnContas.CssClass = "btn-tab pull-left"
        End If

    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub

    Protected Sub btnRedirect_Click(Sender As Object, e As EventArgs)
        ' Obtém o botão clicado
        'Dim btnRedirect As Button = DirectCast(Sender, Button)

        ' Obtém o valor do campo Id_Contrato_SLA_Servico do botão clicado
        'Dim id As String = btnRedirect.CommandArgument
        If TypeOf Sender Is ImageButton Then
            Dim imgBtnRedirect As ImageButton = DirectCast(Sender, ImageButton)
            Dim id As String = imgBtnRedirect.CommandArgument

            ' Redireciona para outra página com base no valor do campo Id_Contrato_SLA_Servico
            Response.Redirect("~/Contrato/Contrato_SLA_Servico.aspx?id=" & id)
        End If



    End Sub


    ' [INÍCIO - ICTRL-NF-202509-002]
    Private Sub PopularFiltros()
        Try
            ' Popula dropdown de Status do Contrato
            Dim dsStatus As DataSet = WS_Modulo.Exec_Procedure_Simple(Session("Conn_Banco"), "sp_Drop_Contrato_Status")
            If dsStatus IsNot Nothing AndAlso dsStatus.Tables.Count > 0 Then
                ddlStatusContrato.DataSource = dsStatus
                ddlStatusContrato.DataTextField = "Descricao"
                ddlStatusContrato.DataValueField = "ID"
                ddlStatusContrato.DataBind()
                ddlStatusContrato.Items.Insert(0, New ListItem("Todos", "0"))
            End If

            ' Popula dropdown de Vencimento
            Dim dsVencimento As DataSet = WS_Contrato.Contrato(Session("Conn_Banco"), 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Drop_Contrato_Vencimento", True)
            If dsVencimento IsNot Nothing AndAlso dsVencimento.Tables.Count > 0 Then
                ddlVenceEm.DataSource = dsVencimento
                ddlVenceEm.DataTextField = "Descricao"
                ddlVenceEm.DataValueField = "Dias"
                ddlVenceEm.DataBind()
                ddlVenceEm.Items.Insert(0, New ListItem("Qualquer Data", "0"))
            End If

            ' Se veio da Home com parâmetro vencimento, seleciona o valor
            If Not String.IsNullOrEmpty(Request.QueryString("vencimento")) Then
                If ddlVenceEm.Items.FindByValue(Request.QueryString("vencimento")) IsNot Nothing Then
                    ddlVenceEm.SelectedValue = Request.QueryString("vencimento")
                End If
            End If
        Catch ex As Exception
            ' Tratar erro se a carga dos filtros falhar
        End Try
    End Sub

    Protected Sub Filtro_Changed(sender As Object, e As EventArgs)
        Call BuscarContratos()
    End Sub

    Private Sub BuscarContratos()
        Dim idStatus As Integer = 0
        Dim diasVencimento As Integer = 0

        ' Credentials para PostBack (quando altera dropdowns)
        WS_Contrato.Credentials = System.Net.CredentialCache.DefaultCredentials

        ' Ler valores dos dropdowns
        If ddlStatusContrato.Items.Count > 0 AndAlso Not String.IsNullOrEmpty(ddlStatusContrato.SelectedValue) Then
            Integer.TryParse(ddlStatusContrato.SelectedValue, idStatus)
        End If

        If ddlVenceEm.Items.Count > 0 AndAlso Not String.IsNullOrEmpty(ddlVenceEm.SelectedValue) Then
            Integer.TryParse(ddlVenceEm.SelectedValue, diasVencimento)
        End If

        ' Chamando o novo método Contrato_Filtro
        vdataset = WS_Contrato.Contrato_Filtro(Session("Conn_Banco"), idStatus, diasVencimento)

        ' Armazena em Session("DataSet") para que o UserControl Contrato_Tabela possa usar
        Session("DataSet") = vdataset

        ' Rebind na grid do Contrato_Tabela (UserControl na Master Page)
        Dim contratoTabela As Contrato_Tabela = CType(Master.FindControl("pnlContrato_Tabela").FindControl("Contrato_Tabela1"), Contrato_Tabela)
        If contratoTabela IsNot Nothing Then
            contratoTabela.RebindGrid()
        End If
    End Sub

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs)
        If Session("DataSet") IsNot Nothing Then
            Session("DataSet_Exportacao") = CType(Session("DataSet"), DataSet)

            Dim script As String = "window.open('../Exportacao/Exporta.aspx?Descricao=Relatorio_de_Contratos','','resizable=yes, menubar=yes, scrollbars=no,height=768px, width=1024px, top=10, left=10');"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "exportWindow", script, True)
        End If
    End Sub
    ' [FIM - ICTRL-NF-202509-002]

End Class
