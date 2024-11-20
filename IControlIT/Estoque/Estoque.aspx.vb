
Public Class Estoque
    Inherits System.Web.UI.Page
    Dim WS_Estoque As New WS_GUA_Estoque.WSEstoque
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim oConfig As New cls_Config
    Dim vdataset As Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Estoque de Equipamento ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            btSolicitação.PostBackUrl = "~/Estoque/Aparelho.aspx"
            btNotaFiscal.PostBackUrl = "~/Estoque/Nota_Fiscal.aspx"

            '-----monta detalhamento do botao dinamico
            dtgLista.DataSource = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Botoes_Dinamicos_Estoque", Nothing, Nothing, Nothing, Nothing, Nothing)
            dtgLista.DataBind()

            If dtgLista.Items.Count = 0 Then
                btAlerta.Enabled = True
            End If

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboConsumidor)
            Page.Form.DefaultButton = btSolicitação.UniqueID
            Call Master.Localizar("sp_Drop_Estoque", "~/Estoque/Aparelho.aspx")

            oConfig.CarregaCombo(cboTipo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo", Nothing))
            '-----carrega combo do usuario do estoque
            oConfig.CarregaCombo(cboConsumidor, WS_Estoque.Estoque(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Consumidor", True))
        End If
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As System.EventArgs) Handles btExecutar.Click
        WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials
        hdfTab.Value = "Todos"
        If cboConsumidor.SelectedValue = Nothing Then Exit Sub
        Call Lista_Estoque(Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue))
        Dim aut = WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Clicou no botao abrir na em estoque",
                                                        False)
    End Sub

    Protected Sub btDesativa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        pnlDetalhe.Visible = True
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgEstoque") + 23, 4), System.Int32)

        hfdId_Aparelho.Value = dtgEstoque.Items(i).Cells(6).Text
    End Sub

    Protected Sub btAssistencia_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgEstoque") + 26, 4), System.Int32)

        WS_Estoque.Estoque(Session("Conn_Banco"),
                        Nothing,
                        Nothing,
                        Nothing,
                        Nothing,
                        dtgEstoque.Items(i).Cells(6).Text,
                        Nothing,
                        Nothing,
                        Session("Id_Usuario"),
                        "sp_Manutencao_Estoque_Assistencia",
                        False)

        '-----zera paginacao
        If cboConsumidor.SelectedValue = Nothing Then Exit Sub
        Call Lista_Estoque(Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue))
    End Sub

    Protected Sub btDevolucao_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgEstoque") + 24, 4), System.Int32)

        WS_Estoque.Estoque(Session("Conn_Banco"),
                        Nothing,
                        Nothing,
                        Nothing,
                        Nothing,
                        dtgEstoque.Items(i).Cells(6).Text,
                        Nothing,
                        Nothing,
                        Session("Id_Usuario"),
                        "sp_Manutencao_Estoque_Devolucao",
                        False)

        '-----zera paginacao
        If cboConsumidor.SelectedValue = Nothing Then Exit Sub
        Call Lista_Estoque(Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue))
    End Sub

    Protected Sub btDesativa_Usuario_Ativo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgAtivo") + 35, 4), System.Int32)

        '-----deleta registro
        WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Estoque.Estoque(Session("Conn_Banco"),
                                cboConsumidor.SelectedValue,
                                Nothing,
                                Nothing,
                                Nothing,
                                Nothing,
                                dtgAtivo.Items(i).Cells(4).Text,
                                Nothing,
                                Session("Id_Usuario"),
                                "sp_Retira_Usuario_Estoque",
                                False)

        '-----zera paginacao
        If cboConsumidor.SelectedValue = Nothing Then Exit Sub
        Call Lista_Estoque(Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue))
    End Sub

    Protected Sub btOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btOk.Click
        If Trim(txtObservacao.Text) = "" Then Exit Sub
        pnlDetalhe.Visible = False

        '-----deleta registro
        WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials

        WS_Estoque.Estoque(Session("Conn_Banco"),
                                Nothing,
                                Nothing,
                                Nothing,
                                Nothing,
                                hfdId_Aparelho.Value,
                                Nothing,
                                txtObservacao.Text,
                                Session("Id_Usuario"),
                                "sp_Desativa_Aparelho",
                                False)

        If cboConsumidor.SelectedValue = Nothing Then Exit Sub
        Call Lista_Estoque(Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue))
    End Sub

    Private Sub Lista_Estoque(ByVal vDescricao As System.String, ByVal vTipo As System.Int32)
        '-----Estoque--------------------------------------------------------------------------------------------
        vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                            cboConsumidor.SelectedValue,
                            oConfig.ValidaCampo(vDescricao),
                            Nothing, Nothing, oConfig.ValidaCampo(vTipo), Nothing, Nothing, Nothing,
                            "sp_Estoque",
                            True)
        dtgEstoque.DataSource = vdataset
        dtgEstoque.DataBind()
        txtQuantidadeEstoque.Text = dtgEstoque.Items.Count

        '-----Solicitacao--------------------------------------------------------------------------------------------
        If hdfTab.Value = "Solicitacao" Or hdfTab.Value = "Todos" Then
            vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                                    cboConsumidor.SelectedValue,
                                    oConfig.ValidaCampo(vDescricao),
                                    Nothing, Nothing, oConfig.ValidaCampo(vTipo), Nothing, Nothing, Nothing,
                                    "sp_Solicitacao",
                                    True)
            dtgSolicitacao.DataSource = vdataset
            dtgSolicitacao.DataBind()
            txtQuantidadeSolicitacao.Text = dtgSolicitacao.Items.Count
        End If

        '-----Devolucao--------------------------------------------------------------------------------------------
        If hdfTab.Value = "Devolucao" Or hdfTab.Value = "Todos" Then
            vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                                    cboConsumidor.SelectedValue,
                                    oConfig.ValidaCampo(vDescricao),
                                    Nothing, Nothing, oConfig.ValidaCampo(vTipo), Nothing, Nothing, Nothing,
                                    "sp_Devolucao",
                                    True)
            dtgDevolucao.DataSource = vdataset
            dtgDevolucao.DataBind()
            txtQuantidadeDevolucao.Text = dtgDevolucao.Items.Count
        End If

        '-----Assistencia--------------------------------------------------------------------------------------------
        If hdfTab.Value = "Assistencia" Or hdfTab.Value = "Todos" Then
            vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                                    cboConsumidor.SelectedValue,
                                    oConfig.ValidaCampo(vDescricao),
                                    Nothing, Nothing, oConfig.ValidaCampo(vTipo), Nothing, Nothing, Nothing,
                                    "sp_Assistencia",
                                    True)
            dtgAssistencia.DataSource = vdataset
            dtgAssistencia.DataBind()
            txtQuantidadeAssistencia.Text = dtgAssistencia.Items.Count
        End If
        '-----Ativo--------------------------------------------------------------------------------------------
        If hdfTab.Value = "Ativo" Or hdfTab.Value = "Todos" Then
            vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                                    cboConsumidor.SelectedValue,
                                    oConfig.ValidaCampo(vDescricao),
                                    Nothing, Nothing, oConfig.ValidaCampo(vTipo), Nothing, Nothing, Nothing,
                                    "sp_Ativo",
                                    True)
            dtgAtivo.DataSource = vdataset
            dtgAtivo.DataBind()
            txtQuantidadeAtivo.Text = dtgAtivo.Items.Count
        End If
        '-----Desativado--------------------------------------------------------------------------------------------
        If hdfTab.Value = "Descarte" Or hdfTab.Value = "Todos" Then
            vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                                    cboConsumidor.SelectedValue,
                                    oConfig.ValidaCampo(vDescricao),
                                    Nothing, Nothing, oConfig.ValidaCampo(vTipo), Nothing, Nothing, Nothing,
                                    "sp_Desativado",
                                    True)
            dtgDesativado.DataSource = vdataset
            dtgDesativado.DataBind()
            txtQuantidadeDesativado.Text = dtgDesativado.Items.Count
        End If
        Call formataGrid()
    End Sub

    Public Sub Manutencao_Estoque()
        If dtgEstoque.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_txtNr_Ativo As TextBox

        For i = 0 To dtgEstoque.Items.Count - 1
            v_txtNr_Ativo = dtgEstoque.Items(i).Cells(1).Controls(1)
            If Not v_txtNr_Ativo.Text = "" Or (v_txtNr_Ativo.Text = "" And Not dtgEstoque.Items(i).Cells(10).Text = "&nbsp;") Then
                vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                                        Nothing,
                                        oConfig.ValidaCampo(v_txtNr_Ativo.Text),
                                        Nothing,
                                        Nothing,
                                        dtgEstoque.Items(i).Cells(6).Text,
                                        IIf(Trim(dtgEstoque.Items(i).Cells(7).Text) = "&nbsp;", Nothing, dtgEstoque.Items(i).Cells(7).Text),
                                        Nothing,
                                        Session("Id_Usuario"),
                                        "sp_Manutencao_Estoque",
                                        True)
            End If
        Next i
    End Sub

    Public Sub Manutencao_Solicitacao()
        If dtgSolicitacao.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_txtNr_Aparelho As TextBox
        Dim v_txtNr_Pedido As TextBox
        Dim v_txtNota_Fiscal As TextBox

        For i = 0 To dtgSolicitacao.Items.Count - 1
            v_txtNr_Aparelho = dtgSolicitacao.Items(i).Cells(0).Controls(1)
            v_txtNr_Pedido = dtgSolicitacao.Items(i).Cells(4).Controls(1)
            v_txtNota_Fiscal = dtgSolicitacao.Items(i).Cells(6).Controls(1)
            If Not v_txtNr_Aparelho.Text = "" And Not v_txtNr_Pedido.Text = "" And Not v_txtNota_Fiscal.Text = "" Then
                WS_Estoque.Estoque(Session("Conn_Banco"),
                                            Nothing,
                                            oConfig.ValidaCampo(v_txtNr_Aparelho.Text),
                                            oConfig.ValidaCampo(v_txtNr_Pedido.Text),
                                            oConfig.ValidaCampo(v_txtNota_Fiscal.Text),
                                            dtgSolicitacao.Items(i).Cells(5).Text,
                                            Nothing,
                                            Nothing,
                                            Session("Id_Usuario"),
                                            "sp_Manutencao_Solicitacao_Pedido",
                                            False)
            End If
        Next i
    End Sub

    Public Sub Manutencao_Devolucao()
        If dtgDevolucao.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_chkEstoque As CheckBox

        For i = 0 To dtgDevolucao.Items.Count - 1
            v_chkEstoque = dtgDevolucao.Items(i).Cells(4).Controls(1)
            If v_chkEstoque.Checked = True Then
                WS_Estoque.Estoque(Session("Conn_Banco"),
                                    cboConsumidor.SelectedValue,
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    dtgDevolucao.Items(i).Cells(5).Text,
                                    Nothing,
                                    Nothing,
                                    Session("Id_Usuario"),
                                    "sp_Manutencao_Devolucao_Estoque",
                                    False)
            End If
        Next i
    End Sub

    Public Sub Manutencao_Assistencia()
        If dtgAssistencia.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_chkEstoque As CheckBox

        For i = 0 To dtgAssistencia.Items.Count - 1
            v_chkEstoque = dtgAssistencia.Items(i).Cells(4).Controls(1)
            If v_chkEstoque.Checked = True Then
                WS_Estoque.Estoque(Session("Conn_Banco"),
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        dtgAssistencia.Items(i).Cells(5).Text,
                                        Nothing,
                                        Nothing,
                                        Session("Id_Usuario"),
                                        "sp_Manutencao_Assistencia_Estoque",
                                        False)
            End If
        Next i
    End Sub

    Public Sub Manutencao_Dasativado()
        If dtgDesativado.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_chkAtivar As CheckBox

        For i = 0 To dtgDesativado.Items.Count - 1
            v_chkAtivar = dtgDesativado.Items(i).Cells(4).Controls(1)
            If v_chkAtivar.Checked = True Then
                WS_Estoque.Estoque(Session("Conn_Banco"),
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        dtgDesativado.Items(i).Cells(5).Text,
                                        Nothing,
                                        Nothing,
                                        Session("Id_Usuario"),
                                        "sp_Reativa_Aparelho",
                                        False)
            End If
        Next i
    End Sub

    Private Sub formataGrid()
        Dim linha As System.Int32
        Dim coluna As System.Int32

        lblDescQuantidadeSolicitacao.BackColor = Drawing.Color.Silver
        lblDescQuantidadeEstoque.BackColor = Drawing.Color.Silver
        lblDescQuantidadeAssistencia.BackColor = Drawing.Color.Silver
        lblDescQuantidadeDevolucao.BackColor = Drawing.Color.Silver
        lblDescQuantidadeDesativado.BackColor = Drawing.Color.Silver

        '-----estoque
        For linha = 0 To dtgEstoque.Items.Count - 1
            If dtgEstoque.Items(linha).Cells(8).Text = 2 Then
                For coluna = 0 To dtgEstoque.Items(linha).Cells.Count - 1
                    dtgEstoque.Items(linha).Cells(coluna).BackColor = Drawing.ColorTranslator.FromHtml("#0E84FF")
                    dtgEstoque.Items(linha).Cells(coluna).ForeColor = Drawing.ColorTranslator.FromHtml("#FFFFFF")
                Next coluna
            End If
        Next linha

        '-----ativo
        If hdfTab.Value = "Ativo" Or hdfTab.Value = "Todos" Then
            For linha = 0 To dtgAtivo.Items.Count - 1
                If dtgAtivo.Items(linha).Cells(5).Text = 2 Then
                    For coluna = 0 To dtgAtivo.Items(linha).Cells.Count - 1
                        dtgAtivo.Items(linha).Cells(coluna).BackColor = Drawing.ColorTranslator.FromHtml("#0E84FF")
                        dtgAtivo.Items(linha).Cells(coluna).ForeColor = Drawing.ColorTranslator.FromHtml("#FFFFFF")

                    Next coluna
                End If
            Next linha
        End If

        '-----assistencia
        If hdfTab.Value = "Assistencia" Or hdfTab.Value = "Todos" Then
            For linha = 0 To dtgAssistencia.Items.Count - 1
                If dtgAssistencia.Items(linha).Cells(7).Text = 2 Then
                    For coluna = 0 To dtgAssistencia.Items(linha).Cells.Count - 1
                        dtgAssistencia.Items(linha).Cells(coluna).BackColor = Drawing.ColorTranslator.FromHtml("#0E84FF")
                        dtgAssistencia.Items(linha).Cells(coluna).ForeColor = Drawing.ColorTranslator.FromHtml("#FFFFFF")
                    Next coluna
                End If
            Next linha
        End If
    End Sub

    Protected Sub btFecharRegulador_Click(sender As Object, e As EventArgs) Handles btFecharRegulador.Click
        pnlMsg.Visible = False
    End Sub

    Protected Sub btCancela_Click(sender As Object, e As EventArgs) Handles btCancela.Click
        pnlDetalhe.Visible = False
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----manutencao nos dados do solicitacao
        If hdfTab.Value = "Solicitacao" Then Call Manutencao_Solicitacao()

        '-----manutencao nos dados do estoque
        If hdfTab.Value = "Estoque" Then Call Manutencao_Estoque()

        '-----manutencao nos dados do assistencia
        If hdfTab.Value = "Assistencia" Then Call Manutencao_Assistencia()

        '-----manutencao nos dados de dvolucao
        If hdfTab.Value = "Devolucao" Then Call Manutencao_Devolucao()

        '-----manutencao nos dados do destivado
        If hdfTab.Value = "Descarte" Then Call Manutencao_Dasativado()

        If cboConsumidor.SelectedValue = Nothing Then Exit Sub
        Call Lista_Estoque(Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue))

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub


    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String
        '-----nome do arquivo a ser exportado
        Dim Descricao As System.String
        '-----campos a ser exportado modelo (xxxx;xxxxx;xxxx). quando null sistema gera com base no dataset
        Dim Campo As System.String

        If hdfTab.Value = "Solicitacao" Then
            Session("DataSet") = WS_Estoque.Estoque(Session("Conn_Banco"),
                                                    cboConsumidor.SelectedValue,
                                                    Nothing,
                                                    Nothing, Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue), Nothing, Nothing, Nothing,
                                                    "sp_Solicitacao",
                                                    True)
            Tipo = Nothing
            Descricao = "Solicitacao"
            Campo = "Nr_Aparelho;Nm_Aparelho_Tipo;Nm_Ativo_Modelo;Nm_Conglomerado;Nr_Pedido;Nr_Nota_Fiscal;Nr_Ativo;DC_Nr_Nota_Fiscal;Nr_Chamado;Dt_Chamado;Nm_Estoque_Aparelho_Status"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                                "Descricao=" & Descricao &
                                                "&Campo=" & Campo &
                                                "&Tipo=" & Tipo &
                                                "','','resizable=yes, menubar=yes, scrollbars=no," &
                                                "height=768px, width=1024px, top=10, left=10'" &
                                                ")", True)
        End If

        If hdfTab.Value = "Estoque" Then
            Session("DataSet") = WS_Estoque.Estoque(Session("Conn_Banco"),
                                                    cboConsumidor.SelectedValue,
                                                    oConfig.ValidaCampo(txtPesquisaEquipamento.Text),
                                                    Nothing, Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue), Nothing, Nothing, Nothing,
                                                    "sp_Estoque",
                                                    True)

            Tipo = Nothing
            Descricao = "Estoque"
            Campo = "Nr_Ativo;Nr_Aparelho;Nm_Aparelho_Tipo;Nm_Ativo_Modelo;Nm_Conglomerado;Nm_Estoque_Aparelho_Status"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                                "Descricao=" & Descricao &
                                                "&Campo=" & Campo &
                                                "&Tipo=" & Tipo &
                                                "','','resizable=yes, menubar=yes, scrollbars=no," &
                                                "height=768px, width=1024px, top=10, left=10'" &
                                                ")", True)
        End If

        If hdfTab.Value = "Ativo" Then
            Session("DataSet") = WS_Estoque.Estoque(Session("Conn_Banco"),
                                                    cboConsumidor.SelectedValue,
                                                    Nothing,
                                                    Nothing, Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue), Nothing, Nothing, Nothing,
                                                    "sp_Ativo",
                                                    True)

            Tipo = Nothing
            Descricao = "Ativo"
            Campo = "Nr_Ativo;Nm_Aparelho_Tipo;Nm_Conglomerado;Status_Aparelho;Dt_Suspensao;Tempo_Parado"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                                "Descricao=" & Descricao &
                                                "&Campo=" & Campo &
                                                "&Tipo=" & Tipo &
                                                "','','resizable=yes, menubar=yes, scrollbars=no," &
                                                "height=768px, width=1024px, top=10, left=10'" &
                                                ")", True)
        End If

        If hdfTab.Value = "Assistencia" Then
            Session("DataSet") = WS_Estoque.Estoque(Session("Conn_Banco"),
                                                    cboConsumidor.SelectedValue,
                                                    oConfig.ValidaCampo(txtPesquisaEquipamento.Text),
                                                    Nothing, Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue), Nothing, Nothing, Nothing,
                                                    "sp_Assistencia",
                                                    True)

            Tipo = Nothing
            Descricao = "Assistencia"
            Campo = "Nr_Aparelho;Nm_Aparelho_Tipo;Nm_Ativo_Modelo;Nm_Conglomerado;Nr_Ativo;Status_Aparelho"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                                "Descricao=" & Descricao &
                                                "&Campo=" & Campo &
                                                "&Tipo=" & Tipo &
                                                "','','resizable=yes, menubar=yes, scrollbars=no," &
                                                "height=768px, width=1024px, top=10, left=10'" &
                                                ")", True)
        End If

        If hdfTab.Value = "Devolucao" Then
            Session("DataSet") = WS_Estoque.Estoque(Session("Conn_Banco"),
                                                    cboConsumidor.SelectedValue,
                                                    oConfig.ValidaCampo(txtPesquisaEquipamento.Text),
                                                    Nothing, Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue), Nothing, Nothing, Nothing,
                                                    "sp_Devolucao",
                                                    True)
            Tipo = Nothing
            Descricao = "Devolucao"
            Campo = "Nr_Aparelho;Nm_Aparelho_Tipo;Nm_Ativo_Modelo;Nm_Conglomerado;Nr_Ativo;Consumidor;Usuario;Status_Usuario;EMail_Consumidor;Matricula_Chefia"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                                "Descricao=" & Descricao &
                                                "&Campo=" & Campo &
                                                "&Tipo=" & Tipo &
                                                "','','resizable=yes, menubar=yes, scrollbars=no," &
                                                "height=768px, width=1024px, top=10, left=10'" &
                                                ")", True)
        End If

        If hdfTab.Value = "Descarte" Then
            Session("DataSet") = WS_Estoque.Estoque(Session("Conn_Banco"),
                                                    cboConsumidor.SelectedValue,
                                                    oConfig.ValidaCampo(txtPesquisaEquipamento.Text),
                                                    Nothing, Nothing, IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue), Nothing, Nothing, Nothing,
                                                    "sp_Desativado",
                                                    True)

            Tipo = Nothing
            Descricao = "Descarte"
            Campo = "Nr_Aparelho;Nm_Aparelho_Tipo;Nm_Ativo_Modelo;Nm_Conglomerado;Status_Aparelho;Observacao"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                                "Descricao=" & Descricao &
                                                "&Campo=" & Campo &
                                                "&Tipo=" & Tipo &
                                                "','','resizable=yes, menubar=yes, scrollbars=no," &
                                                "height=768px, width=1024px, top=10, left=10'" &
                                                ")", True)
        End If
    End Sub

    Protected Sub btEmail_Click(sender As Object, e As EventArgs)
        If cboConsumidor.SelectedValue = Nothing Then Exit Sub

        WS_Estoque.Estoque(Session("Conn_Banco"),
                            cboConsumidor.SelectedValue,
                            Nothing,
                            Nothing,
                            Nothing,
                            IIf(cboTipo.SelectedValue = "", 0, cboTipo.SelectedValue),
                            Nothing,
                            Nothing,
                            Session("Id_Usuario"),
                            "sp_Email_Notificacao_Devolucao",
                            False)

        '-----envio de email
        Call Master.Registro_Salvo("E-mail enviado com sucesso.")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btAlerta_Click(sender As Object, e As EventArgs)
        pnlMsg.Visible = True
    End Sub

    Protected Sub btnSolicitacao_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnEstoque_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnAtivo_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnAssistencia_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnDevolucao_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnDescarte_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub Valida_Tab(ByVal btn As Button)

        divSolicitacaoMenu.Visible = False
        divEstoqueMenu.Visible = False
        divAtivoMenu.Visible = False
        divAssistenciaMenu.Visible = False
        divDevolucaoMenu.Visible = False
        divDescarteMenu.Visible = False
        btnSolicitacao.CssClass = "btn-tab-disable pull-left"
        btnEstoque.CssClass = "btn-tab-disable pull-left"
        btnAtivo.CssClass = "btn-tab-disable pull-left"
        btnAssistencia.CssClass = "btn-tab-disable pull-left"
        btnDevolucao.CssClass = "btn-tab-disable pull-left"
        btnDescarte.CssClass = "btn-tab-disable pull-left"

        If btn.Text = "Solicitação" Then
            divSolicitacaoMenu.Visible = True
            btnSolicitacao.CssClass = "btn-tab pull-left"
            hdfTab.Value = "Solicitacao"
        ElseIf btn.Text = "Estoque" Then
            divEstoqueMenu.Visible = True
            btnEstoque.CssClass = "btn-tab pull-left"
            hdfTab.Value = "Estoque"
        ElseIf btn.Text = "Ativo" Then
            divAtivoMenu.Visible = True
            btnAtivo.CssClass = "btn-tab pull-left"
            hdfTab.Value = "Ativo"
        ElseIf btn.Text = "Assistência" Then
            divAssistenciaMenu.Visible = True
            btnAssistencia.CssClass = "btn-tab pull-left"
            hdfTab.Value = "Assistencia"
        ElseIf btn.Text = "Devolução" Then
            divDevolucaoMenu.Visible = True
            btnDevolucao.CssClass = "btn-tab pull-left"
            hdfTab.Value = "Devolucao"
        ElseIf btn.Text = "Descarte" Then
            divDescarteMenu.Visible = True
            btnDescarte.CssClass = "btn-tab pull-left"
            hdfTab.Value = "Descarte"
        End If

    End Sub

    Protected Sub btPesquisar_Click(sender As Object, e As EventArgs)
        '-----manutencao nos dados do solicitacao
        vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                                    cboConsumidor.SelectedValue,
                                    oConfig.ValidaCampo(txtPesquisaEquipamento.Text),
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    "sp_Solicitacao",
                                    True)
        dtgSolicitacao.DataSource = vdataset
        dtgSolicitacao.DataBind()
        txtQuantidadeSolicitacao.Text = dtgSolicitacao.Items.Count
        If dtgSolicitacao.Items.Count > 0 Then
            lblDescQuantidadeSolicitacao.BackColor = Drawing.Color.Lime
        End If

        '-----manutencao nos dados do estoque
        vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                    cboConsumidor.SelectedValue,
                    oConfig.ValidaCampo(txtPesquisaEquipamento.Text),
                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                    "sp_Estoque",
                    True)
        dtgEstoque.DataSource = vdataset
        dtgEstoque.DataBind()
        txtQuantidadeEstoque.Text = dtgEstoque.Items.Count
        If dtgEstoque.Items.Count > 0 Then
            lblDescQuantidadeEstoque.BackColor = Drawing.Color.Lime
        End If

        '-----manutencao nos dados do assistencia
        vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                                    cboConsumidor.SelectedValue,
                                    oConfig.ValidaCampo(txtPesquisaEquipamento.Text),
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    "sp_Assistencia",
                                    True)
        dtgAssistencia.DataSource = vdataset
        dtgAssistencia.DataBind()
        txtQuantidadeAssistencia.Text = dtgAssistencia.Items.Count
        If dtgAssistencia.Items.Count > 0 Then
            lblDescQuantidadeAssistencia.BackColor = Drawing.Color.Lime
        End If

        '-----manutencao nos dados de dvolucao
        vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                                    cboConsumidor.SelectedValue,
                                    oConfig.ValidaCampo(txtPesquisaEquipamento.Text),
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    "sp_Devolucao",
                                    True)
        dtgDevolucao.DataSource = vdataset
        dtgDevolucao.DataBind()
        txtQuantidadeDevolucao.Text = dtgDevolucao.Items.Count
        If dtgDevolucao.Items.Count > 0 Then
            lblDescQuantidadeDevolucao.BackColor = Drawing.Color.Lime
        End If

        '-----manutencao nos dados do destivado
        vdataset = WS_Estoque.Estoque(Session("Conn_Banco"),
                                    cboConsumidor.SelectedValue,
                                    oConfig.ValidaCampo(txtPesquisaEquipamento.Text),
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    "sp_Desativado",
                                    True)
        dtgDesativado.DataSource = vdataset
        dtgDesativado.DataBind()
        txtQuantidadeDesativado.Text = dtgDesativado.Items.Count
        If dtgDesativado.Items.Count > 0 Then
            lblDescQuantidadeDesativado.BackColor = Drawing.Color.Lime
        End If
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class

