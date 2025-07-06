Public Class Ativo
    Inherits System.Web.UI.Page

    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Ativo - Visualização {Request("ID")}",
                                                        False)
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Cadastro de Ativo",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.Form.DefaultButton = btPDF.UniqueID
            Page.SetFocus(txtNumeroAtivo)
            Call Master.Localizar("sp_Drop_Ativo", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboAtivoTipo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo", Nothing))
            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))
            oConfig.CarregaCombo(cboAtivoStatus, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Ativo_Status", Nothing))

            If Request("ID") = Nothing Then
                Call Monta_Relacionamento_Default(dtgUsuario, -1)

                If dtgUsuario.Items.Count = 0 Then
                    dtgUsuario.DataSource = Session("vDtDragDrop")
                    dtgUsuario.DataBind()
                End If
            Else
                btConfiguracao.PostBackUrl = "~/Cadastro/Ativo_Parametro.aspx?ID=" & Request("ID")
                vdataset = WS_Cadastro.Ativo(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True, Nothing, Nothing, Nothing, Nothing, Nothing)
                If vdataset.Tables(0).Rows.Count = 0 Then Exit Sub

                Dim fl_Desativado As Int16 = vdataset.Tables(0).Rows(0).Item("Fl_Desativado")

                btPDF.OnClientClick = "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & vdataset.Tables(0).Rows(0).Item("Id_Ativo") & "&pTabela=Ativo','','resizable=yes, menubar=yes, scrollbars=no,height=768px, width=1024px, top=10, left=10')"

                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Ativo")
                txtNumeroAtivo.Text = vdataset.Tables(0).Rows(0).Item("Nr_Ativo_Tabela")
                txtFinalidade.Text = vdataset.Tables(0).Rows(0).Item("Finalidade")
                cboAtivoTipo.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo"))
                cboConglomerado.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Conglomerado") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Conglomerado"))
                txtDataAtivacao.Text = vdataset.Tables(0).Rows(0).Item("Dt_Ativacao")
                txtObservacao.Text = vdataset.Tables(0).Rows(0).Item("Observacao")
                cboAtivoStatus.SelectedValue = IIf(vdataset.Tables(0).Rows(0).Item("Id_Ativo_Status") = 0, "", vdataset.Tables(0).Rows(0).Item("Id_Ativo_Status"))

                txtEquipamento.Text = vdataset.Tables(0).Rows(0).Item("Equipamento")

                txtSuspenssao.Text = vdataset.Tables(0).Rows(0).Item("Localidade")
                txtSimCard.Text = vdataset.Tables(0).Rows(0).Item("Numero_Sim_Card")
                txtEndereco.Text = vdataset.Tables(0).Rows(0).Item("Endereco")
                txtVlrContrato.Text = vdataset.Tables(0).Rows(0).Item("Valor_Contrato")
                txtPlanoContrato.Text = vdataset.Tables(0).Rows(0).Item("Plano_Contrato")
                txtVelocidade.Text = vdataset.Tables(0).Rows(0).Item("Velocidade")

                '-----monta complemento do ativo
                dtgDadosComplemento.DataSource = WS_Cadastro.Ativo(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, cboAtivoTipo.SelectedValue, Nothing,
                                                                       Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                       Nothing, "sp_SL_Complemento_Update", True, Nothing, Nothing, Nothing, Nothing, Nothing)
                dtgDadosComplemento.DataBind()

                '-----monta modelo do ativo
                oConfig.CarregaCombo(cboAtivoModelo, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Ativo_Modelo_Id_Ativo_Tipo", cboAtivoTipo.SelectedValue, Nothing))
                cboAtivoModelo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Modelo")

                '-----retorna relacionamento de ativo comconsumidor
                vdataset = WS_Cadastro.Ativo(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, cboAtivoTipo.SelectedValue, Nothing, Nothing, Nothing, Nothing,
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, "sd_SL_RL_Ativo_Consumidor", True, Nothing, Nothing, Nothing, Nothing, Nothing)

                dtgUsuario.DataSource = vdataset
                dtgUsuario.DataBind()

                Call Monta_Relacionamento_Default(dtgUsuario, -1)

                If dtgUsuario.Items.Count = 0 Then
                    dtgUsuario.DataSource = Session("vDtDragDrop")
                    dtgUsuario.DataBind()
                End If

                If fl_Desativado = 1 Then
                    pnlConfirmacao.Visible = True
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "disableScrolling();", True)
                End If
            End If
        End If
    End Sub

    Protected Sub btExcluir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Excluindo dados cadastro de ativos",
                                                        False)
        Dim v_Objeto_Select As ImageButton = sender
        Dim vText As System.String = v_Objeto_Select.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgUsuario") + 22, 4), System.Int32)

        Call Monta_Relacionamento_Default(dtgUsuario, i)
        dtgUsuario.DataSource = Session("vDtDragDrop")
        dtgUsuario.DataBind()
    End Sub

    Protected Sub btInserir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btInserir",
                                                        False)
        If dtgUsuario.Items.Count = 0 Then
            Call Monta_Relacionamento_Default(dtgUsuario, -1)

            If dtgUsuario.Items.Count = 0 Then
                dtgUsuario.DataSource = Session("vDtDragDrop")
                dtgUsuario.DataBind()
            End If
        Else
            Dim v_cbo_Consumidor As DropDownList = dtgUsuario.Items(0).Cells(1).Controls(1)
        End If

        If Not dtgUsuario.Items(0).Cells(6).Text = "&nbsp;" Then
            Call Monta_Relacionamento_Default(dtgUsuario, -1)
            dtgUsuario.DataSource = Session("vDtDragDrop")
            dtgUsuario.DataBind()
        End If
    End Sub

    Protected Sub btLupa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btLupa",
                                                        False)
        monta_Consumidor(sender)
    End Sub

    Public Sub monta_Consumidor(ByVal pObjeto As Object)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim v_Objeto_Select As ImageButton = pObjeto
        Dim vText As System.String = v_Objeto_Select.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgUsuario") + 19, 4), System.Int32)

        Dim v_txt_Consumidor As TextBox = dtgUsuario.Items(i).Cells(1).Controls(3)
        Dim v_cbo_Consumidor As DropDownList = dtgUsuario.Items(i).Cells(1).Controls(1)
        Dim v_bt_Lupa As ImageButton = dtgUsuario.Items(i).Cells(7).Controls(1)
        Dim v_bt_Voltar As ImageButton = dtgUsuario.Items(i).Cells(7).Controls(3)

        If dtgUsuario.Items.Count = 0 Then Exit Sub

        oConfig.CarregaCombo(v_cbo_Consumidor, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Consumidor_Ativo", v_txt_Consumidor.Text))
        v_cbo_Consumidor.Visible = True
        v_cbo_Consumidor.BackColor = Drawing.Color.Silver
        Page.SetFocus(v_cbo_Consumidor)

        v_bt_Voltar.Visible = True
        v_txt_Consumidor.Visible = False
        v_bt_Lupa.Visible = False
    End Sub

    Protected Sub btVoltar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btVoltar",
                                                        False)
        Dim v_Objeto_Select As ImageButton = sender
        Dim vText As System.String = v_Objeto_Select.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgUsuario") + 21, 4), System.Int32)

        Dim v_txt_Consumidor As TextBox = dtgUsuario.Items(i).Cells(1).Controls(3)
        Dim v_cbo_Consumidor As DropDownList = dtgUsuario.Items(i).Cells(1).Controls(1)
        Dim v_bt_Lupa As ImageButton = dtgUsuario.Items(i).Cells(7).Controls(1)
        Dim v_bt_Voltar As ImageButton = dtgUsuario.Items(i).Cells(7).Controls(3)

        v_cbo_Consumidor.Visible = False
        v_cbo_Consumidor.BackColor = Drawing.Color.White
        v_bt_Voltar.Visible = False
        v_txt_Consumidor.Visible = True
        v_bt_Lupa.Visible = True
        v_txt_Consumidor.Text = ""
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboAtivoModelo.SelectedValue = Nothing
        cboAtivoTipo.SelectedValue = Nothing
        cboAtivoStatus.SelectedValue = Nothing
        cboConglomerado.SelectedValue = Nothing
        '-----usuario
        dtgUsuario.DataSource = Nothing
        dtgUsuario.DataBind()
        Call Monta_Relacionamento_Default(dtgUsuario, -1)
        If dtgUsuario.Items.Count = 0 Then
            dtgUsuario.DataSource = Session("vDtDragDrop")
            dtgUsuario.DataBind()
        End If

        dtgDadosComplemento.DataSource = Nothing
        dtgDadosComplemento.DataBind()
        btConfiguracao.PostBackUrl = Nothing
        Session("dtDragDrop") = Nothing
    End Sub

    Protected Sub cboAtivoTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAtivoTipo.SelectedIndexChanged
        If cboAtivoTipo.SelectedValue = Nothing Then Exit Sub
        '-----monta complemento do ativo
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        dtgDadosComplemento.DataSource = WS_Cadastro.Ativo(Session("Conn_Banco"), Nothing, Nothing, Nothing, cboAtivoTipo.SelectedValue, Nothing, Nothing, Nothing, Nothing,
                                                                Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_Complemento_Insert", True, Nothing, Nothing, Nothing, Nothing, Nothing)
        dtgDadosComplemento.DataBind()
        '-----monta modelo do ativo
        oConfig.CarregaCombo(cboAtivoModelo, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Ativo_Modelo_Id_Ativo_Tipo", cboAtivoTipo.SelectedValue, Nothing))
    End Sub

    Protected Sub cboConsumidor_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim v_bt_Excluir As ImageButton = dtgUsuario.Items(0).Cells(0).Controls(1)
        Dim v_txt_Consumidor As TextBox = dtgUsuario.Items(0).Cells(1).Controls(3)
        Dim v_cbo_Consumidor As DropDownList = dtgUsuario.Items(0).Cells(1).Controls(1)
        Dim v_bt_Lupa As ImageButton = dtgUsuario.Items(0).Cells(7).Controls(1)
        Dim v_bt_Voltar As ImageButton = dtgUsuario.Items(0).Cells(7).Controls(3)
        Dim v_Dt_Hr_Ativacao As TextBox = dtgUsuario.Items(0).Cells(2).Controls(1)

        dtgUsuario.Items(0).Cells(6).Text = v_cbo_Consumidor.SelectedValue
        v_txt_Consumidor.Text = v_cbo_Consumidor.SelectedItem.Text
        v_Dt_Hr_Ativacao.Text = DateTime.Today.ToString

        v_cbo_Consumidor.Visible = False
        v_cbo_Consumidor.BackColor = Drawing.Color.White
        v_bt_Voltar.Visible = False
        v_txt_Consumidor.Visible = True
        v_bt_Lupa.Visible = False
        v_bt_Excluir.Visible = True

        Call Monta_Relacionamento_Default(dtgUsuario, -2) '-2 nao insere um novo registro em branco
        dtgUsuario.DataSource = Session("vDtDragDrop")
        dtgUsuario.DataBind()

        Page.SetFocus(v_Dt_Hr_Ativacao)
    End Sub

    Public Function montaComplemento(ByVal pGrid As DataGrid) As System.String
        Dim i As System.Int32
        Dim vLabel As Label
        Dim vText As TextBox
        Dim vRetorno As String = Nothing

        For i = 0 To pGrid.Items.Count - 1
            vLabel = pGrid.Items(i).Cells(0).Controls(1)
            vText = pGrid.Items(i).Cells(0).Controls(3)
            vRetorno = vRetorno & IIf(Trim(vLabel.Text) = "", " ", vLabel.Text) & "¶" & IIf(Trim(vText.Text) = "", " ", vText.Text) & IIf(pGrid.Items.Count - 1 = i, "", "§")
        Next
        Return vRetorno
    End Function

    Public Sub Monta_Relacionamento_Default(ByVal vDataGrid As DataGrid,
                                            ByVal vi As System.Int32)

        Dim vDtDragDrop As New System.Data.DataSet
        vDtDragDrop.DataSetName = "vDataSetDragDrop"
        Dim vDataTable As Data.DataTable = New Data.DataTable("vDataTableInclui")
        '-----cria colunas
        Dim vId_Consumidor As Data.DataColumn = New Data.DataColumn("Id_Consumidor", GetType(System.String))
        Dim vNm_Consumidor As Data.DataColumn = New Data.DataColumn("Nm_Consumidor", GetType(System.String))
        Dim vDt_Hr_Ativacao As Data.DataColumn = New Data.DataColumn("Dt_Hr_Ativacao", GetType(System.String))
        Dim vDt_Hr_Desativacao As Data.DataColumn = New Data.DataColumn("Dt_Hr_Desativacao", GetType(System.String))
        Dim vTermo_Ativacao As Data.DataColumn = New Data.DataColumn("Termo_Ativacao", GetType(System.String))
        Dim vTermo_Devolucao As Data.DataColumn = New Data.DataColumn("Termo_Devolucao", GetType(System.String))
        Dim vExcluir As Data.DataColumn = New Data.DataColumn("Excluir", GetType(System.String))
        Dim vLupa As Data.DataColumn = New Data.DataColumn("Lupa", GetType(System.String))
        Dim vVoltar As Data.DataColumn = New Data.DataColumn("Voltar", GetType(System.String))
        Dim vbt_Termo_Ativacao As Data.DataColumn = New Data.DataColumn("bt_Termo_Ativacao", GetType(System.String))
        Dim vbt_Termo_Devolucao As Data.DataColumn = New Data.DataColumn("bt_Termo_Devolucao", GetType(System.String))

        vDataTable.Columns.Add(vId_Consumidor)
        vDataTable.Columns.Add(vNm_Consumidor)
        vDataTable.Columns.Add(vDt_Hr_Ativacao)
        vDataTable.Columns.Add(vDt_Hr_Desativacao)
        vDataTable.Columns.Add(vTermo_Ativacao)
        vDataTable.Columns.Add(vTermo_Devolucao)
        vDataTable.Columns.Add(vExcluir)
        vDataTable.Columns.Add(vLupa)
        vDataTable.Columns.Add(vVoltar)
        vDataTable.Columns.Add(vbt_Termo_Ativacao)
        vDataTable.Columns.Add(vbt_Termo_Devolucao)

        '-----adiciona tabela no dataset
        vDtDragDrop.Tables.Add(vDataTable)

        Dim vLinha As Data.DataRow
        '-----carrega dataset
        If vi = -1 Then
            vLinha = vDataTable.NewRow
            vLinha("Id_Consumidor") = ""
            vLinha("Nm_Consumidor") = ""
            vLinha("Dt_Hr_Ativacao") = ""
            vLinha("Dt_Hr_Desativacao") = ""
            vLinha("Termo_Ativacao") = ""
            vLinha("Termo_Devolucao") = ""
            vLinha("Excluir") = "False"
            vLinha("Lupa") = "True"
            vLinha("Voltar") = "False"
            vLinha("bt_Termo_Ativacao") = "False"
            vLinha("bt_Termo_Devolucao") = "False"

            vDataTable.Rows.Add(vLinha)
        End If

        Dim i As System.Int32
        Dim v_txt_Consumidor As TextBox
        Dim v_Dt_Hr_Ativacao As TextBox
        Dim v_Dt_Hr_Desativacao As TextBox
        Dim v_Termo_Ativacao As ImageButton
        Dim v_Termo_Devolucao As ImageButton

        For i = 0 To dtgUsuario.Items.Count - 1
            If i <> vi Then
                vLinha = vDataTable.NewRow
                v_txt_Consumidor = dtgUsuario.Items(i).Cells(1).Controls(3)
                v_Dt_Hr_Ativacao = dtgUsuario.Items(i).Cells(2).Controls(1)
                v_Termo_Ativacao = dtgUsuario.Items(i).Cells(3).Controls(1)
                v_Dt_Hr_Desativacao = dtgUsuario.Items(i).Cells(4).Controls(1)
                v_Termo_Devolucao = dtgUsuario.Items(i).Cells(5).Controls(1)

                vLinha("Id_Consumidor") = dtgUsuario.Items(i).Cells(6).Text
                vLinha("Nm_Consumidor") = v_txt_Consumidor.Text
                vLinha("Dt_Hr_Ativacao") = v_Dt_Hr_Ativacao.Text
                vLinha("Dt_Hr_Desativacao") = v_Dt_Hr_Desativacao.Text

                vLinha("Termo_Ativacao") = v_Termo_Ativacao.PostBackUrl
                vLinha("Termo_Devolucao") = v_Termo_Devolucao.PostBackUrl

                vLinha("Excluir") = "True"
                vLinha("Lupa") = "False"
                vLinha("Voltar") = "False"

                vLinha("bt_Termo_Ativacao") = IIf(v_Termo_Ativacao.PostBackUrl = "", "False", "True")
                vLinha("bt_Termo_Devolucao") = IIf(v_Termo_Devolucao.PostBackUrl = "", "False", "True")

                vDataTable.Rows.Add(vLinha)
            End If
        Next

        vDtDragDrop.AcceptChanges()
        Session("vDtDragDrop") = vDtDragDrop
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        '-----verifica se colocou observacao
        If Trim(txtObservacaoObrigarotia.Text) = "" Then Exit Sub
        txtObservacao.Text = txtObservacaoObrigarotia.Text

        '-----nao insere registro quando descricao so for numerica
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        txtIdentificacao.Text = WS_Cadastro.Ativo(Session("Conn_Banco"),
                                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                                        oConfig.ValidaCampo(txtNumeroAtivo.Text),
                                                        oConfig.ValidaCampo(txtFinalidade.Text),
                                                        oConfig.ValidaCampo(cboAtivoTipo.SelectedValue),
                                                        oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                                        oConfig.ValidaCampo(cboAtivoModelo.SelectedValue),
                                                        IIf(Not IsDate(txtSuspenssao.Text), Nothing, oConfig.ValidaCampo(txtSuspenssao.Text)),
                                                        IIf(Trim(txtDataAtivacao.Text) = "", Nothing, txtDataAtivacao.Text),
                                                        oConfig.ValidaCampo(Mid(txtObservacao.Text, 1, 8000) + " | Usuário - " + Session("Nm_Usuario")),
                                                        montaComplemento(dtgDadosComplemento),
                                                        oConfig.ValidaCampo(cboAtivoStatus.SelectedValue),
                                                        oConfig.AgrupaDadosAtivos(Session("vDtDragDrop")),
                                                        Session("Id_Usuario"),
                                                        "sp_SM",
                                                        False,
                                                        oConfig.ValidaCampo(txtEndereco.Text),
                                                        oConfig.ValidaCampo(txtSimCard.Text),
                                                        oConfig.ValidaCampo(txtVlrContrato.Text),
                                                        oConfig.ValidaCampo(txtPlanoContrato.Text),
                                                        oConfig.ValidaCampo(txtVelocidade.Text)).Tables(0).Rows(0).Item(0)

        Dim aut = WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Ativo - Visualização {Request("ID")}",
                                                        False)

        'btConfiguracao.PostBackUrl = "~/Cadastro/Ativo_Parametro.aspx?ID=" & Request("ID")
        vdataset = WS_Cadastro.Ativo(Session("Conn_Banco"),
                                     IIf(Request("ID") Is Nothing, txtIdentificacao.Text, Request("ID")),
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     "sp_SL_ID",
                                     True,
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     Nothing,
                                     Nothing)

        txtObservacao.Text = vdataset.Tables(0).Rows(0).Item("Observacao")

        '-----configura link para acesso ao rateio
        btConfiguracao.PostBackUrl = "~/Cadastro/Ativo_Parametro.aspx?ID=" & txtIdentificacao.Text

        '-----envio de msg de alerta de data de suspensao
        If Not IIf(Not IsDate(txtSuspenssao.Text), Nothing, oConfig.ValidaCampo(txtSuspenssao.Text)) = Nothing Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            txtIdentificacao.Text = WS_Cadastro.Ativo(Session("Conn_Banco"),
                                                oConfig.ValidaCampo(txtIdentificacao.Text),
                                                oConfig.ValidaCampo(txtNumeroAtivo.Text),
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                IIf(Not IsDate(txtSuspenssao.Text), Nothing, oConfig.ValidaCampo(txtSuspenssao.Text)),
                                                "Suspensão de linha",
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Session("Id_Usuario"),
                                                "sd_SM_Ativo_Alerta",
                                                False, Nothing, Nothing, Nothing, Nothing, Nothing).Tables(0).Rows(0).Item(0)

        End If

        '-----salva datas de ativacao e desativacao
        If Not dtgUsuario.Items.Count = 0 Or Not txtIdentificacao.Text = 0 Then
            Dim v_txtDataAtivacao As TextBox
            Dim v_txtDataDesativacao As TextBox
            For i = 0 To dtgUsuario.Items.Count - 1
                v_txtDataAtivacao = dtgUsuario.Items(i).Cells(2).Controls(1)
                v_txtDataDesativacao = dtgUsuario.Items(i).Cells(4).Controls(1)

                If Not Trim(v_txtDataAtivacao.Text) = Nothing Then
                    WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
                    WS_Cadastro.Usuario_Perfil(Session("Conn_Banco"),
                                                    dtgUsuario.Items(i).Cells(6).Text,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    txtIdentificacao.Text,
                                                    IIf(Trim(v_txtDataAtivacao.Text) = "", Nothing, v_txtDataAtivacao.Text),
                                                    IIf(Trim(v_txtDataDesativacao.Text) = "", Nothing, v_txtDataDesativacao.Text),
                                                    Session("Id_Usuario"),
                                                    "sp_Perfil_Acesso_Intervalo_Data_Ativo_Consumidor", False)
                End If
            Next
        End If

        '-----monta relacionamento de ativo comconsumidor para print do termo
        vdataset = WS_Cadastro.Ativo(Session("Conn_Banco"), txtIdentificacao.Text, Nothing, Nothing, cboAtivoTipo.SelectedValue, Nothing, Nothing, Nothing, Nothing,
                                            Nothing, Nothing, Nothing, Nothing, Nothing, "sd_SL_RL_Ativo_Consumidor", True, Nothing, Nothing, Nothing, Nothing, Nothing)

        dtgUsuario.DataSource = vdataset
        dtgUsuario.DataBind()

        Call Monta_Relacionamento_Default(dtgUsuario, -1)

        If dtgUsuario.Items.Count = 0 Then
            dtgUsuario.DataSource = Session("vDtDragDrop")
            dtgUsuario.DataBind()
        End If

        '-----registro salvo ok
        pnlObservacao.Visible = False

        If txtIdentificacao.Text = -1 Then
            Call Master.Registro_Salvo("Verifique se o login do usuário está cadastrado para funcionar as datas de desativação e ativação.")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
        End If

    End Sub

    Protected Sub btCancela_Click(sender As Object, e As EventArgs) Handles btCancela.Click
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btCancela",
                                                        False)
        pnlObservacao.Visible = False
    End Sub

    Protected Sub btFechar_Registro_Click(sender As Object, e As EventArgs) Handles btFechar_Registro.Click
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btFechar_Registro",
                                                        False)
        pnlRegistro.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btLimpar",
                                                        False)
        Call limpar()
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btSalvar",
                                                        False)
        pnlRegistro.Visible = False
        pnlObservacao.Visible = True
        txtObservacaoObrigarotia.Text = ""
    End Sub
    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Desativando ativo " + txtIdentificacao.Text,
                                                        False)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Ativo(Session("Conn_Banco"), txtIdentificacao.Text,
                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                Nothing, " | Usuário - " + Session("Nm_Usuario"), Nothing, Nothing,
                                Nothing, Session("Id_Usuario"), "sp_SE", False, Nothing, Nothing, Nothing, Nothing, Nothing)
        Call limpar()
    End Sub

    Protected Sub btAbrir_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btAbrir",
                                                        False)
        pnlRegistro.Visible = True
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "disableScrolling();", True)
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btVoltar",
                                                        False)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "disableScrolling();", True)
    End Sub

    Protected Sub btContinuar_Click(sender As Object, e As EventArgs) Handles btContinuar.Click
        pnlConfirmacao.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
    End Sub

    Protected Sub btRestaurar_Click(sender As Object, e As EventArgs) Handles btRestaurar.Click
        '-----desativa/ativa registro
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Lixeira(Session("Conn_Banco"), txtIdentificacao.Text, Session("Id_Usuario"), "Ativo")
        pnlConfirmacao.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
        Response.Redirect("Ativo.aspx" & "?id=" & Request("ID"))
    End Sub
End Class
