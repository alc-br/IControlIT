
Public Class Consulta_Solicitacao
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim vdataset As New Data.DataSet
    Dim oConfig As New cls_Config

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Suporte ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----home
            If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then
                Call Master.home("usuario")
            End If

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtSolicitacao)

            '-----valida botão menu
            Call Master.SelecionaBotao("suporte")

            ''-----verifica permissao para abertura de chamado
            ''Dim vDataSet As System.Data.DataSet
            ''Dim i As System.Int16
            ''vDataSet = WS_Modulo.Solicitacao(Session("Conn_Banco"),
            ''                                Nothing, Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, Nothing, Nothing,
            ''                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Consulta_Permissao_Abertura", True)

            ''For i = 0 To vDataSet.Tables(0).Rows.Count - 1
            ''    '-----usuario
            ''    If vDataSet.Tables(0).Rows(i).Item("Id_Solicitacao_Permissao") = 1 Then

            ''        Call Master.ValidaBotaoSuporte("1")
            ''    End If
            ''    '-----unidade
            ''    If vDataSet.Tables(0).Rows(i).Item("Id_Solicitacao_Permissao") = 2 Then

            ''        Call Master.ValidaBotaoSuporte("2")
            ''    End If
            ''    '-----roaming
            ''    If vDataSet.Tables(0).Rows(i).Item("Id_Solicitacao_Permissao") = 3 Then

            ''        Call Master.ValidaBotaoSuporte("3")
            ''    End If
            ''Next

            '-----retira botoes quando todos nao estao sendo usados
            'If btSolicitacao.Enabled = False And btRequisicaoUnidade.Enabled = False And btRequisicaoUsuario.Enabled = False Then
            '    tbBotoesSolicitacao.Visible = False
            '    divAxia.Visible = False
            'End If

            vdataset = WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Conglomerado_Solicitacao", Session("Id_Usuario"), Nothing)

            If vDataSet.Tables(0).Rows.Count > 0 Then
                btRelatorio.Style.Add("Opacity", "1")
                btRelatorio.Enabled = True
            End If

            Session("DataSet") = Nothing
            Call Executar()
        End If
    End Sub

    Protected Sub dtgSolicitacao_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgSolicitacao.PageIndexChanged
        Call Executar()
        dtgSolicitacao.CurrentPageIndex = e.NewPageIndex
        dtgSolicitacao.DataSource = Session("DataSet")
        dtgSolicitacao.DataBind()
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As System.EventArgs) Handles btExecutar.Click
        Call Executar()
    End Sub

    Public Sub Executar()
        Session("DataSet") = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                                    Nothing,
                                                    oConfig.ValidaCampo(txtSolicitacao.Text),
                                                    Nothing,
                                                    IIf(Session("Id_Usuario_Perfil_Acesso") = 1, oConfig.ValidaCampo(txtUsuario.Text), Session("Nm_Usuario")),
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    oConfig.ValidaCampo(cboStatus.SelectedValue),
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Session("Id_Usuario"),
                                                    "sp_Consulta_Solicitacao",
                                                    True)
        dtgSolicitacao.DataSource = Session("DataSet")
        dtgSolicitacao.DataBind()
        pnlAbir.Visible = False

        If Session("DataSet").Tables(0).Rows.Count = 0 Then
            lblMsg.Visible = True
            dtgSolicitacao.Visible = False
            lblTitulo.Visible = False
        Else
            lblMsg.Visible = False
            dtgSolicitacao.Visible = True
            lblTitulo.Visible = True
        End If

    End Sub


    Protected Sub dtgSolicitacao_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dtgSolicitacao.SelectedIndexChanged
        'Response.Redirect(dtgSolicitacao.SelectedItem.Cells(8).Text)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('" & dtgSolicitacao.SelectedItem.Cells(3).Text & "','_blank','resizable=yes, menubar=yes, scrollbars=no, height=700, width=1200, top=0, left=0');", True)
        Response.Redirect(dtgSolicitacao.SelectedItem.Cells(3).Text)
    End Sub

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        pnlAbir.Visible = False
    End Sub

    Protected Sub btRequisicaoUsuario_Click(sender As Object, e As EventArgs)
        Response.Redirect("../Solicitacao/Solicitacao.aspx")
    End Sub

    Protected Sub btRequisicaoUnidade_Click(sender As Object, e As EventArgs)
        Response.Redirect("../Solicitacao/Solicitacao_Unidade.aspx")
    End Sub
    Protected Sub btSolicitacao_Click(sender As Object, e As EventArgs)
        Response.Redirect("../Solicitacao/Solicitacao_Roaming.aspx")
    End Sub

    Protected Sub btRelatorio_Click(sender As Object, e As ImageClickEventArgs)
        Response.Redirect("../Consulta/Maquina_Unidade.aspx")
    End Sub

    'Protected Sub btAssistente_Click1(sender As Object, e As ImageClickEventArgs) Handles btAssistente.Click
    '    'https://localhost:44333/Default.aspx?Bot=Comodato_Sodexo&Run=JQJKJODQLMLMJITKBKDGNQLQHQLKRQHMLOPMLORQJMLINONOTQBMTQHKTGNMPONMPKRMNQFIRITKFKFKDKLKTOTONML
    '    '$USU:SAMANTA.LOPES;$CLC:BR014437;OLA

    '    vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"), Nothing, Nothing,
    '                                    Session("Id_Usuario"),
    '                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
    '                                    "sp_Acesso_Axia",
    '                                    True)

    '    Response.Redirect(vdataset.Tables(0).Rows(0).Item("LINK"))
    'End Sub
    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String = Nothing
        '-----nome do arquivo a ser exportado
        Dim Descricao As System.String = "Chamado"
        '-----campos a ser exportado modelo (xxxx;xxxxx;xxxx). quando null sistema gera com base no dataset
        Dim Campo As System.String = "Nm_Solicitacao;Nm_Solicitacao_Tipo;Excedente_SLA;Dt_Solicitacao;Dt_Vencimento;Dt_Encerramento;Fl_Status;Nm_Usuario;Nm_Consumidor"
        '-----abre pnl de exportacao
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                            "Descricao=" & Descricao &
                                            "&Campo=" & Campo &
                                            "&Tipo=" & Tipo &
                                            "','','resizable=yes, menubar=yes, scrollbars=no," &
                                            "height=768px, width=1024px, top=10, left=10'" &
                                            ")", True)
    End Sub
    Protected Sub btAbrir_Click(sender As Object, e As EventArgs)
        pnlAbir.Visible = True
        txtSolicitacao.Text = ""
        txtUsuario.Text = ""
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class

