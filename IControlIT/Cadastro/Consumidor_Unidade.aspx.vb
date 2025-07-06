
Public Class Consumidor_Unidade
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Dados Adicionais",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtNmUnidade)
            Page.Form.DefaultButton = btPDF.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            txtIdConsumidor.Text = Request("Id_Consumidor")
            txtNmConsumidor.Text = Request("Nm_Consumidor")
            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))

            btPDF.Enabled = True
            btPDF.Style.Add("Opacity", "1")

            '----lista dados da unidade
            If Not Request("Id_Consumidor") = Nothing Then
                vdataset = WS_Cadastro.Consumidor_Unidade(Session("Conn_Banco"), Nothing, Request("Id_Consumidor"), Nothing, Nothing,
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                            Nothing, Nothing, Nothing, "sp_SL_ID", True)

                If vdataset.Tables(0).Rows.Count = 0 Then Exit Sub

                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Consumidor_Unidade")
                txtNmUnidade.Text = vdataset.Tables(0).Rows(0).Item("Nm_Unidade")
                txtCNPJ.Text = vdataset.Tables(0).Rows(0).Item("CNPJ")
                txtIE.Text = vdataset.Tables(0).Rows(0).Item("IE")
                txtDataAtivacao.Text = vdataset.Tables(0).Rows(0).Item("Data_Ativacao")
                txtObservacao.Text = vdataset.Tables(0).Rows(0).Item("Observacao")
                txtEntregaContato.Text = vdataset.Tables(0).Rows(0).Item("Entrega_Contato")
                txtEntregaEndereco.Text = vdataset.Tables(0).Rows(0).Item("Entrega_Endereco")
                txtEntregaTelefone.Text = vdataset.Tables(0).Rows(0).Item("Entrega_Telefone")
                txtFaturamentoContato.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Contato")
                txtFaturamentoEndereco.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Endereco")
                txtFaturamentoCNPJ.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_CNPJ")
                txtFaturamentoIE.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_IE")
                txtFaturamentoEmail.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Email")
                txtFaturamentoTelefone.Text = vdataset.Tables(0).Rows(0).Item("Faturamento_Telefone")
                txtMatricula.Text = vdataset.Tables(0).Rows(0).Item("Matricula")
                cboConglomerado.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Conglomerado")

                '----fornecedor que atente a unidade
                oConfig.CarregaList(lstDestino, WS_Cadastro.Consumidor_Unidade(Session("Conn_Banco"), vdataset.Tables(0).Rows(0).Item("Id_Consumidor_Unidade"), Nothing, Nothing, Nothing,
                                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                            Nothing, Nothing, Nothing, "sp_SL_Unidade_Fornecedor", True))

            End If
        End If
    End Sub

    Protected Sub Limpar()

        txtIdentificacao.Text = ""
        txtNmUnidade.Text = ""
        txtCNPJ.Text = ""
        txtIE.Text = ""
        txtDataAtivacao.Text = ""
        txtObservacao.Text = ""
        txtEntregaContato.Text = ""
        txtEntregaEndereco.Text = ""
        txtEntregaTelefone.Text = ""
        txtFaturamentoContato.Text = ""
        txtFaturamentoEndereco.Text = ""
        txtFaturamentoCNPJ.Text = ""
        txtFaturamentoIE.Text = ""
        txtFaturamentoEmail.Text = ""
        txtFaturamentoTelefone.Text = ""
        txtMatricula.Text = ""
        cboConglomerado.SelectedValue = Nothing

    End Sub

    Protected Sub btMoveSelecionado_Click(sender As Object, e As System.EventArgs) Handles btMoveSelecionado.Click
        Call move_Dados(lstOrigem, lstDestino)
    End Sub

    Protected Sub btMoveSelecao_Click(sender As Object, e As System.EventArgs) Handles btMoveSelecao.Click
        Call move_Dados(lstDestino, lstOrigem)
    End Sub

    Public Sub move_Dados(pOrigem As ListBox, pDestino As ListBox)
        If pOrigem.SelectedValue = Nothing Then Exit Sub

        Dim vOrigen As ListItem
        '-----adiciona
        vOrigen = New ListItem
        vOrigen.Value = pOrigem.SelectedItem.Value
        vOrigen.Text = pOrigem.SelectedItem.Text
        pDestino.Items.Add(vOrigen)
        '-----remove
        pOrigem.Items.Remove(vOrigen)
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----salva dados adicionais de usuario e de forncedor, fornecedor que atende a unidade
        WS_Cadastro.Consumidor_Unidade(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                        oConfig.ValidaCampo(txtIdConsumidor.Text),
                                        oConfig.ValidaCampo(txtNmUnidade.Text),
                                        oConfig.ValidaCampo(txtCNPJ.Text),
                                        oConfig.ValidaCampo(txtIE.Text),
                                        IIf(Trim(txtDataAtivacao.Text) = "", Nothing, txtDataAtivacao.Text),
                                        oConfig.ValidaCampo(txtObservacao.Text),
                                        oConfig.ValidaCampo(txtEntregaContato.Text),
                                        oConfig.ValidaCampo(txtEntregaEndereco.Text),
                                        oConfig.ValidaCampo(txtEntregaTelefone.Text),
                                        oConfig.ValidaCampo(txtFaturamentoContato.Text),
                                        oConfig.ValidaCampo(txtFaturamentoEndereco.Text),
                                        oConfig.ValidaCampo(txtFaturamentoCNPJ.Text),
                                        oConfig.ValidaCampo(txtFaturamentoIE.Text),
                                        oConfig.ValidaCampo(txtFaturamentoEmail.Text),
                                        oConfig.ValidaCampo(txtFaturamentoTelefone.Text),
                                        oConfig.ValidaCampo(txtMatricula.Text),
                                        IIf(cboConglomerado.SelectedValue = "", 0, cboConglomerado.SelectedValue),
                                        oConfig.AgrupaDados(lstDestino),
                                        Session("Id_Usuario"),
                                        "sp_SM",
                                        False)

        '-----limpa e esconde lista de selecao
        txtGrupo.Text = ""
        lstOrigem.Items.Clear()
        lstDestino.Items.Clear()

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
        Call Limpar()
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Consumidor_Unidade(Session("Conn_Banco"),
                                        txtIdentificacao.Text,
                                        oConfig.ValidaCampo(txtIdConsumidor.Text),
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Session("Id_Usuario"),
                                        "sp_SE",
                                        False)

        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        Page.SetFocus(txtNmUnidade)
        btSalvar.Enabled = True
        Call Limpar()
    End Sub
    Protected Sub btPDF_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & txtIdentificacao.Text & "&pTabela=Consumidor_Unidade','_blank','resizable=yes, menubar=yes, scrollbars=no, height=700, width=1200, top=0, left=0');", True)
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call Limpar()
    End Sub
    Protected Sub btnFornecedor_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnDadosUnidade_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnDadosFornecedor_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub Valida_Tab(ByVal btn As Button)

        divFornecedor.Visible = False
        divDadosUnidade.Visible = False
        divDadosFornecedor.Visible = False
        btnFornecedor.CssClass = "btn-tab-disable pull-left"
        btnDadosUnidade.CssClass = "btn-tab-disable pull-left"
        btnDadosFornecedor.CssClass = "btn-tab-disable pull-left"

        If btn.Text = "Forncedor que Atende a Unidade" Then
            divFornecedor.Visible = True
            btnFornecedor.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Dados da Unidade" Then
            divDadosUnidade.Visible = True
            btnDadosUnidade.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Dados do Fornecedor" Then
            divDadosFornecedor.Visible = True
            btnDadosFornecedor.CssClass = "btn-tab pull-left"
        End If

    End Sub
    Protected Sub btGrupo_Click(sender As Object, e As ImageClickEventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        If Trim(txtGrupo.Text) = "" Then Exit Sub
        oConfig.CarregaList(lstOrigem, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Consumidor_Ativo", txtGrupo.Text))
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
