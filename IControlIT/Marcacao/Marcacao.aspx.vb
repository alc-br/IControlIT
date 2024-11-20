Public Class Marcacao
    Inherits System.Web.UI.Page
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim WS_Politica As New WS_GUA_Politica.WSPolitica

    Dim oConfig As New cls_Config
    Dim vdataset As New System.Data.DataSet
    Private vchkMarcar As CheckBox
    Private vintPagina As System.Int32

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                "Conta", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            Page.Form.DefaultButton = btAprovar_Barra.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            '-----trata o botao de home e voltar
            If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then
                '-----usuario gestor
                If Request("Visualiza") = "0" Then
                    Call Master.Voltar("", "~/CockPit_Menu.aspx?Postback=1")
                Else
                    Call Master.Voltar("", "~/Marcacao/lote.aspx?Postback=1")
                End If
                Call Master.home("usuario")
            Else
                '-----usuario administrador
                Call Master.Voltar("", "~/Marcacao/lote.aspx?Postback=1")
            End If

            Session("DataView") = Nothing
            'imgConta.ImageUrl = Request("pImagem")

            If Not Request("Lote") = Nothing Then
                '-----mostra valor da politica de uso
                '-----a opcao de request nao e valida para concluir, termo, sincronizacao dos contatos
                vdataset = WS_Politica.Politica_Consumidor(Session("Conn_Banco"), IIf(Request("Id_Usuario") = Nothing, Session("Id_Usuario"), Request("Id_Usuario")), _
                                                               Nothing, Nothing, Nothing, Nothing, Request("Lote"), Nothing, "sp_SL_Marcacao", True)
                If vdataset.Tables(0).Rows.Count > 0 Then
                    txtValorPolitica.Text = Format(vdataset.Tables(0).Rows(0).Item("Valor_Politica"), "R$#########0.00")
                    hfdMarcaLigacao.Value = Format(vdataset.Tables(0).Rows(0).Item("Marca_Ligacao"), "R$#########0.00")
                    txtValorPolitica.Visible = True
                    lblValorPolitica.Visible = True
                Else
                    hfdMarcaLigacao.Value = 2
                End If

                '-----monta dados de custo fixo
                vdataset = WS_Cadastro.Custo_Fixo_Item(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, Nothing, Request("Lote"), Nothing, "sp_Custo_Fixo", True)
                Dim vCustoFixo As System.Double = vdataset.Tables(0).Rows(0).Item("Custo")
                If vdataset.Tables(0).Rows.Count > 0 Then
                    lblCustoFixo.Text = Format(vCustoFixo, "R$#########0.00")
                End If

                '-----monta detalhamento do custo fixo
                vdataset = WS_Cadastro.Custo_Fixo_Item(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, Nothing, Request("Lote"), Nothing, "sp_Detalhamento_Custo_Fixo", True)
                If vdataset.Tables(0).Rows.Count > 0 Then
                    dtgCustoFixo.DataSource = vdataset
                    dtgCustoFixo.DataBind()
                End If

                '-----monta dados na tela
                If Mid(Request("DataLote"), 5, 2) = "01" Then txtLote.Text = "Jan/" & Mid(Request("DataLote"), 1, 4)
                If Mid(Request("DataLote"), 5, 2) = "02" Then txtLote.Text = "Fev/" & Mid(Request("DataLote"), 1, 4)
                If Mid(Request("DataLote"), 5, 2) = "03" Then txtLote.Text = "Mar/" & Mid(Request("DataLote"), 1, 4)
                If Mid(Request("DataLote"), 5, 2) = "04" Then txtLote.Text = "Abr/" & Mid(Request("DataLote"), 1, 4)
                If Mid(Request("DataLote"), 5, 2) = "05" Then txtLote.Text = "Mai/" & Mid(Request("DataLote"), 1, 4)
                If Mid(Request("DataLote"), 5, 2) = "06" Then txtLote.Text = "Jun/" & Mid(Request("DataLote"), 1, 4)
                If Mid(Request("DataLote"), 5, 2) = "07" Then txtLote.Text = "Jul/" & Mid(Request("DataLote"), 1, 4)
                If Mid(Request("DataLote"), 5, 2) = "08" Then txtLote.Text = "Ago/" & Mid(Request("DataLote"), 1, 4)
                If Mid(Request("DataLote"), 5, 2) = "09" Then txtLote.Text = "Set/" & Mid(Request("DataLote"), 1, 4)
                If Mid(Request("DataLote"), 5, 2) = "10" Then txtLote.Text = "Out/" & Mid(Request("DataLote"), 1, 4)
                If Mid(Request("DataLote"), 5, 2) = "11" Then txtLote.Text = "Nov/" & Mid(Request("DataLote"), 1, 4)
                If Mid(Request("DataLote"), 5, 2) = "12" Then txtLote.Text = "Dez/" & Mid(Request("DataLote"), 1, 4)

                txtAtivo.Text = Request("Ativo")
                Dim vPakage As System.String = Nothing

                If Request("Visualiza") = "0" Then
                    vPakage = "sp_Bilhete_Marcacao"
                    btAprovar_Barra.Visible = True
                    btSincronizaCont.Visible = True

                    '-----marca flag de lote visitado
                    WS_Cadastro.Marcacao(Session("Conn_Banco"), Request("Lote"), Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, "sp_Cria_Lote_Marcacao", False)

                    '-----marca ligacao particular
                    WS_Cadastro.Marcacao(Session("Conn_Banco"), Request("Lote"), Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, "sp_Marca_Agenda_Marcacao_Particular", False)
                    Call Refresh_Marcacao()
                Else
                    vPakage = "sp_Bilhete_Consulta"
                    btAprovar_Barra.Visible = False
                    btSincronizaCont.Visible = False

                    '-----cadastro do numera agenda para marcacao
                    txtContato.Enabled = False
                    chkParticularContato.Enabled = False
                    btGravaAgenda.Visible = False
                End If

                '-----cria session para armazenamento de dados para paginacao
                Session("DataView") = New Data.DataView(WS_Consulta.Bilhete(Session("Conn_Banco"), vPakage, Request("Lote"), _
                                                                                IIf(Request("Id_Usuario") = Nothing, Session("Id_Usuario"), Request("Id_Usuario")), _
                                                                                Nothing, Request("Id_Usuario_Marcacao")).Tables(0), _
                                                                                IIf(chkCustoZero.Checked = False, "Custo <> 0", Nothing), _
                                                                                "Data, Destino ASC", _
                                                                                Data.DataViewRowState.OriginalRows)

                dtgBilhete.DataSource = Session("DataView")
                dtgBilhete.DataBind()

                '-----cria grid com dados volume e custo por tipo
                dtgVolume.DataSource = WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Volume", Request("Lote"), _
                                                            IIf(Request("Id_Usuario") = Nothing, Session("Id_Usuario"), Request("Id_Usuario")), _
                                                            Nothing, Request("Id_Usuario_Marcacao"))
                dtgVolume.DataBind()

                '-----trata se usuario tem permissao de detalhar a conta
                If dtgBilhete.Items.Count = 0 Then
                    pnlDetalhe.Visible = True
                    Exit Sub
                End If

                Call formataGrid()

                '-----cria uma session com valor original do dataset
                Session("DataSetOriginal") = Session("DataView")

                '-----totaliza valores
                Dim vCont As System.Int32
                Dim vdataSetBilhete As New System.Data.DataView
                vdataSetBilhete = Session("DataView")
                Dim vtotalLigacao As System.Double = 0
                Dim vtotalMarcado As System.Double = 0
                Dim vtotalNaoMarcado As System.Double = 0

                '-----monta valor de servico e valor marcado
                For vCont = 0 To vdataSetBilhete.Table.Rows.Count - 1
                    vtotalLigacao = vtotalLigacao + vdataSetBilhete.Table.Rows(vCont).Item("Custo")
                    If CType(vdataSetBilhete.Table.Rows(vCont).Item("Marcar"), System.Boolean) = True Then
                        If vdataSetBilhete.Table.Rows(vCont).Item("Usuario") = IIf(Request("Nm_Usuario") = Nothing, Session("Nm_Usuario"), Request("Nm_Usuario")) Then
                            vtotalMarcado = vtotalMarcado + vdataSetBilhete.Table.Rows(vCont).Item("Custo")
                        Else
                            vtotalNaoMarcado = vtotalNaoMarcado + vdataSetBilhete.Table.Rows(vCont).Item("Custo")
                        End If
                    Else
                        vtotalNaoMarcado = vtotalNaoMarcado + vdataSetBilhete.Table.Rows(vCont).Item("Custo")
                    End If
                Next vCont

                '-----carrega valor totalizado no menu
                txtLigacaoTotal.Text = Format(vtotalLigacao, "R$#########0.00")
                txtMarcado.Text = Format(vtotalMarcado, "R$#########0.00")
                lblTotalGeral.Text = Format(vtotalLigacao + vCustoFixo, "R$#########0.00")

                '-----trata paginacao
                lblPagTotal.Text = dtgBilhete.PageCount
                txtPagCont.Text = dtgBilhete.CurrentPageIndex + 1

                If dtgBilhete.CurrentPageIndex = 0 Then
                    btPagAnterior.Enabled = False
                Else
                    btPagAnterior.Enabled = True
                End If

                If dtgBilhete.CurrentPageIndex = dtgBilhete.PageCount - 1 Then
                    btPagProximo.Enabled = False
                Else
                    btPagProximo.Enabled = True
                End If

                '-----alerta nao entrega do termo
                If 1 = WS_Modulo.Alerta_Sistema(Session("Conn_Banco"), txtAtivo.Text, "sp_Termo").Tables(0).Rows(0).Item("Flag_Alerta") Then
                    lblTermo.Visible = True
                Else
                    lblTermo.Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub atualizaGridMarc(ByVal pid_Bilhete_Split As System.String)
        Dim v_bilhete_marcado As System.String()
        If Not pid_Bilhete_Split = Nothing Then v_bilhete_marcado = pid_Bilhete_Split.Split(",")

        '-----cria dataset
        Dim vDataSet As New System.Data.DataSet
        vDataSet.DataSetName = "vDataSetBilhete"
        '-----cria datatable
        Dim vDataTable As Data.DataTable = New Data.DataTable("vDataTableInclui")
        '-----cria colunas
        Dim vId_Bilhete As Data.DataColumn = New Data.DataColumn("Id_Bilhete", GetType(System.Int64))
        Dim vDestino As Data.DataColumn = New Data.DataColumn("Destino", GetType(System.String))
        Dim vLocalidade As Data.DataColumn = New Data.DataColumn("Tipo", GetType(System.String))
        Dim vData_Hora As Data.DataColumn = New Data.DataColumn("Data", GetType(System.String))
        Dim vConsumo As Data.DataColumn = New Data.DataColumn("Consumo", GetType(System.String))
        Dim vCusto As Data.DataColumn = New Data.DataColumn("Custo", GetType(System.Double))
        Dim vUsuario As Data.DataColumn = New Data.DataColumn("Usuario", GetType(System.String))
        Dim vMarcar As Data.DataColumn = New Data.DataColumn("Marcar", GetType(System.String))
        Dim vReload As Data.DataColumn = New Data.DataColumn("Reload", GetType(System.String))
        Dim vId_Bilhete_Tipo As Data.DataColumn = New Data.DataColumn("Id_Bilhete_Tipo", GetType(System.String))
        Dim vAgenda As Data.DataColumn = New Data.DataColumn("Agenda", GetType(System.String))
        Dim vGrupo As Data.DataColumn = New Data.DataColumn("Grupo", GetType(System.String))
        Dim vDia_Semana As Data.DataColumn = New Data.DataColumn("Dia_Semana", GetType(System.String))
        Dim vDesmarcar As Data.DataColumn = New Data.DataColumn("Desmarcar", GetType(System.String))
        Dim vOP_Correta As Data.DataColumn = New Data.DataColumn("OP_Correta", GetType(System.String))
        Dim vDB_Operadora_Sainte As Data.DataColumn = New Data.DataColumn("DB_Operadora_Sainte", GetType(System.String))
        Dim vNm_Ativo_Tipo_Grupo As Data.DataColumn = New Data.DataColumn("Nm_Ativo_Tipo_Grupo", GetType(System.String))

        '-----adiciona colunas na tabela
        vDataTable.Columns.Add(vId_Bilhete)
        vDataTable.Columns.Add(vDestino)
        vDataTable.Columns.Add(vLocalidade)
        vDataTable.Columns.Add(vData_Hora)
        vDataTable.Columns.Add(vConsumo)
        vDataTable.Columns.Add(vCusto)
        vDataTable.Columns.Add(vUsuario)
        vDataTable.Columns.Add(vMarcar)
        vDataTable.Columns.Add(vReload)
        vDataTable.Columns.Add(vId_Bilhete_Tipo)
        vDataTable.Columns.Add(vAgenda)
        vDataTable.Columns.Add(vGrupo)
        vDataTable.Columns.Add(vDesmarcar)
        vDataTable.Columns.Add(vDia_Semana)
        vDataTable.Columns.Add(vOP_Correta)
        vDataTable.Columns.Add(vDB_Operadora_Sainte)
        vDataTable.Columns.Add(vNm_Ativo_Tipo_Grupo)

        '-----adiciona tabela no dataset
        vDataSet.Tables.Add(vDataTable)

        '-----carrega dataset
        Dim vCont As System.Int32
        Dim i As System.Int32
        Dim vLinha As Data.DataRow
        Dim vtotalLigacao As Double = 0
        Dim vtotalMarcado As Double = 0
        Dim vtotalNaoMarcado As Double = 0
        Dim v_lblDestino As Label = Nothing

        Dim vdataViewBilhete As New System.Data.DataView
        vdataViewBilhete = Session("DataView")

        For vCont = 0 To vdataViewBilhete.Table.Rows.Count - 1
            vLinha = vDataTable.NewRow
            vLinha("Id_Bilhete") = vdataViewBilhete.Table.Rows(vCont).Item("Id_Bilhete")
            vLinha("Destino") = vdataViewBilhete.Table.Rows(vCont).Item("Destino")
            vLinha("Tipo") = vdataViewBilhete.Table.Rows(vCont).Item("Tipo")
            vLinha("Data") = vdataViewBilhete.Table.Rows(vCont).Item("Data")
            vLinha("Consumo") = vdataViewBilhete.Table.Rows(vCont).Item("Consumo")
            vLinha("Custo") = vdataViewBilhete.Table.Rows(vCont).Item("Custo")
            vLinha("Usuario") = vdataViewBilhete.Table.Rows(vCont).Item("Usuario")
            vLinha("Marcar") = vdataViewBilhete.Table.Rows(vCont).Item("Marcar")
            vLinha("Id_Bilhete_Tipo") = vdataViewBilhete.Table.Rows(vCont).Item("Id_Bilhete_Tipo")
            vLinha("Agenda") = vdataViewBilhete.Table.Rows(vCont).Item("Agenda")
            vLinha("Grupo") = vdataViewBilhete.Table.Rows(vCont).Item("Grupo")
            vLinha("Desmarcar") = vdataViewBilhete.Table.Rows(vCont).Item("Desmarcar")
            vLinha("Dia_Semana") = vdataViewBilhete.Table.Rows(vCont).Item("Dia_Semana")
            vLinha("OP_Correta") = vdataViewBilhete.Table.Rows(vCont).Item("OP_Correta")
            vLinha("DB_Operadora_Sainte") = vdataViewBilhete.Table.Rows(vCont).Item("DB_Operadora_Sainte")
            vLinha("Nm_Ativo_Tipo_Grupo") = vdataViewBilhete.Table.Rows(vCont).Item("Nm_Ativo_Tipo_Grupo")

            If dtgBilhete.Items(vchkMarcar.ToolTip).Cells(13).Text = vdataViewBilhete.Table.Rows(vCont).Item("Destino").ToString Then
                If vLinha("Desmarcar") = 2 Then
                    If vchkMarcar.Checked = True Then
                        If vLinha("Custo") > 0 Then
                            If Trim(vLinha("Usuario")) = "" Then
                                vLinha("Usuario") = Session("Nm_Usuario")
                                vLinha("Marcar") = True
                            End If
                        Else
                            If vLinha("usuario") = Session("Nm_Usuario") Then
                                vLinha("Usuario") = " "
                                vLinha("Marcar") = False
                            End If
                        End If
                    Else
                        If dtgBilhete.Items(vchkMarcar.ToolTip).Cells(8).Text = vdataViewBilhete.Table.Rows(vCont).Item("Id_Bilhete") Then
                            If vLinha("usuario") = Session("Nm_Usuario") Then
                                vLinha("Usuario") = " "
                                vLinha("Marcar") = False
                            End If
                        End If
                    End If
                End If
            End If

            vLinha("Reload") = vdataViewBilhete.Table.Rows(vCont).Item("Reload")
            '-----totaliza valores
            vtotalLigacao = vtotalLigacao + vdataViewBilhete.Table.Rows(vCont).Item("Custo")
            vDataTable.Rows.Add(vLinha)
        Next vCont
        vDataSet.AcceptChanges()

        '-----totaliza valores marcado e nao marcado
        For vCont = 0 To vDataSet.Tables(0).Rows.Count - 1
            If CType(vDataSet.Tables(0).Rows(vCont).Item("Marcar"), System.Boolean) = False Then
                vtotalNaoMarcado = vtotalNaoMarcado + vDataSet.Tables(0).Rows(vCont).Item("Custo")
            Else
                If vDataSet.Tables(0).Rows(vCont).Item("Usuario") = Session("Nm_Usuario") Then
                    vtotalMarcado = vtotalMarcado + vDataSet.Tables(0).Rows(vCont).Item("Custo")
                Else
                    vtotalNaoMarcado = vtotalNaoMarcado + vDataSet.Tables(0).Rows(vCont).Item("Custo")
                End If
            End If
        Next vCont

        '-----carrega valor totalizado no menu 
        txtLigacaoTotal.Text = Format(vtotalLigacao, "R$#########0.00")
        txtMarcado.Text = Format(vtotalMarcado, "R$#########0.00")

        '-----carrega datagrid de saida
        Session("DataView") = New Data.DataView(vDataSet.Tables(0), _
                                          IIf(chkCustoZero.Checked = False, "Custo <> 0", Nothing), _
                                          IIf(cboColuna.SelectedValue = "", "Data, Destino ASC", cboColuna.SelectedValue & " ASC"), _
                                          Data.DataViewRowState.OriginalRows)


        dtgBilhete.DataSource = Session("DataView")
        dtgBilhete.DataBind()
        Call formataGrid()

        '-----cria uma session com valor original do dataset
        Session("DataSetOriginal") = Session("DataView")

        lblPagTotal.Text = dtgBilhete.PageCount
        txtPagCont.Text = dtgBilhete.CurrentPageIndex + 1

        If dtgBilhete.CurrentPageIndex = 0 Then
            btPagAnterior.Enabled = False
        Else
            btPagAnterior.Enabled = True
        End If

        If dtgBilhete.CurrentPageIndex = dtgBilhete.PageCount - 1 Then
            btPagProximo.Enabled = False
        Else
            btPagProximo.Enabled = True
        End If
    End Sub

    Private Sub atualizaGrid()
        '-----carrega datagrid de saida
        dtgBilhete.DataSource = Session("DataView")
        dtgBilhete.DataBind()
        Call formataGrid()

        lblPagTotal.Text = dtgBilhete.PageCount
        txtPagCont.Text = dtgBilhete.CurrentPageIndex + 1

        If dtgBilhete.CurrentPageIndex = 0 Then
            btPagAnterior.Enabled = False
        Else
            btPagAnterior.Enabled = True
        End If

        If dtgBilhete.CurrentPageIndex = dtgBilhete.PageCount - 1 Then
            btPagProximo.Enabled = False
        Else
            btPagProximo.Enabled = True
        End If
    End Sub
    Private Sub formataGrid()
        Dim linha As System.Int32
        Dim v_lbtDestino As LinkButton
        Dim v_txtDestino As TextBox
        'Dim coluna As System.Int32

        For linha = 0 To dtgBilhete.Items.Count - 1
            vchkMarcar = dtgBilhete.Items(linha).Cells(0).Controls(1)
            vchkMarcar.ToolTip = linha
            Dim vDataHoraLigacao As System.DateTime = dtgBilhete.Items(linha).Cells(4).Text

            '----para grupo
            If dtgBilhete.Items(linha).Cells(10).Text = 1 Then
                'For coluna = 0 To dtgBilhete.Items(linha).Cells.Count - 1
                dtgBilhete.Items(linha).Cells(0).BackColor = Drawing.ColorTranslator.FromHtml("#64BE00")
                dtgBilhete.Items(linha).Cells(0).ForeColor = Drawing.ColorTranslator.FromHtml("#FFFFFF")
                'Next coluna
            End If

            '----fora do horario comercial
            If CType(vDataHoraLigacao.ToShortTimeString.Replace(":", ""), System.Double) <= 600 Or CType(vDataHoraLigacao.ToShortTimeString.Replace(":", ""), System.Double) >= 1800 Then
                'For coluna = 0 To dtgBilhete.Items(linha).Cells.Count - 1
                dtgBilhete.Items(linha).Cells(0).BackColor = Drawing.ColorTranslator.FromHtml("#FBAA19")
                dtgBilhete.Items(linha).Cells(0).ForeColor = Drawing.ColorTranslator.FromHtml("#FFFFFF")
                ' Next coluna
            End If

            '----final semana
            If vDataHoraLigacao.DayOfWeek = 0 Or vDataHoraLigacao.DayOfWeek = 6 Then
                ' For coluna = 0 To dtgBilhete.Items(linha).Cells.Count - 1
                dtgBilhete.Items(linha).Cells(0).BackColor = Drawing.ColorTranslator.FromHtml("#B4009E")
                dtgBilhete.Items(linha).Cells(0).ForeColor = Drawing.ColorTranslator.FromHtml("#FFFFFF")
                'Next coluna
            End If

            '----contratado
            If dtgBilhete.Items(linha).Cells(11).Text = 1 Then
                'For coluna = 0 To dtgBilhete.Items(linha).Cells.Count - 1
                dtgBilhete.Items(linha).Cells(0).BackColor = Drawing.ColorTranslator.FromHtml("#EF3B28")
                dtgBilhete.Items(linha).Cells(0).ForeColor = Drawing.ColorTranslator.FromHtml("#FFFFFF")
                'Next coluna
            End If

            '----contratado
            If dtgBilhete.Items(linha).Cells(14).Text <> dtgBilhete.Items(linha).Cells(15).Text Then
                dtgBilhete.Items(linha).Cells(15).ForeColor = Drawing.ColorTranslator.FromHtml("#FF0000")
            End If

            '-----libera agenda 
            If dtgBilhete.Items(linha).Cells(16).Text = "Impressora" Then
                v_txtDestino = dtgBilhete.Items(linha).Cells(1).Controls(3)
                v_txtDestino.Visible = True
            Else
                v_lbtDestino = dtgBilhete.Items(linha).Cells(1).Controls(1)
                v_lbtDestino.Visible = True
            End If

        Next linha
    End Sub

    Protected Sub txtPagCont_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPagCont.TextChanged
        If IsNumeric(txtPagCont.Text) Then
            If txtPagCont.Text > dtgBilhete.PageCount - 1 Then
                dtgBilhete.CurrentPageIndex = dtgBilhete.PageCount - 1
            ElseIf txtPagCont.Text < 1 Then
                dtgBilhete.CurrentPageIndex = 0
            Else
                dtgBilhete.CurrentPageIndex = txtPagCont.Text - 1
            End If

            vintPagina = dtgBilhete.CurrentPageIndex
            atualizaGrid()
        Else
            txtPagCont.Text = dtgBilhete.CurrentPageIndex + 1
        End If
    End Sub

    Protected Sub chkMarcar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vchkMarcar = sender

        If dtgBilhete.Items(vchkMarcar.ToolTip).Cells(11).Text = 2 Then
            WS_Cadastro.Marcacao(Session("Conn_Banco"), Request("Lote"), dtgBilhete.Items(vchkMarcar.ToolTip).Cells(13).Text, Session("Id_Usuario"), dtgBilhete.Items(vchkMarcar.ToolTip).Cells(8).Text, Nothing, Nothing, "sp_Marca_Desmarca_Bilhete", False)
            Call atualizaGridMarc(Nothing)
        Else
            vchkMarcar.Checked = False
        End If
    End Sub

    Protected Sub btExportar_Click(sender As Object, e As System.EventArgs) Handles btExportar.Click
        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String = Nothing
        '-----nome do arquivo a ser exportado
        Dim Descricao As System.String = "Conta"
        '-----campos a ser exportado modelo (xxxx;xxxxx;xxxx). quando null sistema gera com base no dataset
        Dim Campo As System.String = "Destino;Tipo;Descricao;Data;Consumo;Custo;Usuario;Agenda;DB_Operadora_Sainte"
        '-----------------------------------------------------------------------------------------------------------------------------------
        Dim vPakage As System.String = Nothing
        If Request("Visualiza") = "0" Then
            vPakage = "sp_Bilhete_Marcacao"
        Else
            vPakage = "sp_Bilhete_Consulta"
        End If
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        Session("DataSet") = WS_Consulta.Bilhete(Session("Conn_Banco"), vPakage, Request("Lote"),
                                                     IIf(Request("Id_Usuario") = Nothing, Session("Id_Usuario"), Request("Id_Usuario")),
                                                     Nothing, Nothing)
        '-----------------------------------------------------------------------------------------------------------------------------------

        '-----abre pnl de exportacao
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                            "Descricao=" & Descricao &
                                            "&Campo=" & Campo &
                                            "&Tipo=" & Tipo &
                                            "','','resizable=yes, menubar=yes, scrollbars=no," &
                                            "height=768px, width=1024px, top=10, left=10'" &
                                            ")", True)
    End Sub

    Protected Sub BtOrdenar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtOrdenar.Click
        Dim vDataView_1 As New Data.DataView
        Dim vDataView As Data.DataView = Nothing

        '-----volta original
        If cboColuna.SelectedValue = Nothing And Trim(txtOrdenacao.Text) = Nothing Then
            '-----restaura dataset original
            Session("DataView") = Session("DataSetOriginal")

            Session("DataSetOriginal") = Session("DataView")
            dtgBilhete.CurrentPageIndex = 0
            dtgBilhete.DataSource = Session("DataView")
            dtgBilhete.DataBind()
            Call formataGrid()

            dtgBilhete.CurrentPageIndex = 0
            lblPagTotal.Text = dtgBilhete.PageCount
            txtPagCont.Text = dtgBilhete.CurrentPageIndex + 1
        End If

        '-----filtra
        If Not cboColuna.SelectedValue = Nothing And Not Trim(txtOrdenacao.Text) = Nothing Then
            vDataView_1 = Session("DataSetOriginal")
            If vDataView_1.Table.Columns(cboColuna.SelectedValue).DataType.ToString = "System.String" Then
                vDataView = New Data.DataView(vDataView_1.Table, _
                                              IIf(Trim(txtOrdenacao.Text) = "", Nothing, cboColuna.SelectedValue & " Like '%" & txtOrdenacao.Text & "%'") & IIf(chkCustoZero.Checked = False = Nothing, IIf(chkCustoZero.Checked = False, " AND Custo <> 0", Nothing), Nothing), _
                                              cboColuna.SelectedValue & " ASC", _
                                              Data.DataViewRowState.OriginalRows)
            Else
                If IsNumeric(txtOrdenacao.Text) = True Or Trim(txtOrdenacao.Text) = "" Then
                    vDataView = New Data.DataView(vDataView_1.Table, _
                                                  IIf(Trim(txtOrdenacao.Text) = "", Nothing, cboColuna.SelectedValue & " = " & txtOrdenacao.Text & " ") & IIf(chkCustoZero.Checked = False = Nothing, IIf(chkCustoZero.Checked = False, " AND Custo <> 0", Nothing), Nothing), _
                                                  cboColuna.SelectedValue & " ASC", _
                                                  Data.DataViewRowState.OriginalRows)
                End If
            End If
            If Not vDataView Is Nothing Then
                dtgBilhete.CurrentPageIndex = 0
                Session("DataView") = vDataView
                dtgBilhete.DataSource = Session("DataView")
                dtgBilhete.DataBind()
                Call formataGrid()
            End If
            dtgBilhete.CurrentPageIndex = 0
            lblPagTotal.Text = dtgBilhete.PageCount
            txtPagCont.Text = dtgBilhete.CurrentPageIndex + 1
            Exit Sub
        End If

        '-----ordernar
        If Not cboColuna.SelectedValue = Nothing Then
            vDataView_1 = Session("DataSetOriginal")
            vDataView = New Data.DataView(vDataView_1.Table, _
                                          IIf(chkCustoZero.Checked = False, "Custo <> 0", Nothing), _
                                          cboColuna.SelectedValue & " ASC", _
                                          Data.DataViewRowState.OriginalRows)
            If Not vDataView Is Nothing Then
                Session("DataView") = vDataView
                dtgBilhete.CurrentPageIndex = 0
                dtgBilhete.DataSource = Session("DataView")
                dtgBilhete.DataBind()
                Call formataGrid()
            End If
            dtgBilhete.CurrentPageIndex = 0
            lblPagTotal.Text = dtgBilhete.PageCount
            txtPagCont.Text = dtgBilhete.CurrentPageIndex + 1
        End If
        pnlFiltro.Visible = False
    End Sub

    Protected Sub dtgBilhete_DeleteCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgBilhete.DeleteCommand
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim vdataSet As New System.Data.DataSet
        vdataSet = WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Retorna_Bilente_Tipo", Nothing, Nothing, e.Item.Cells(9).Text, Nothing)

        lblTextoTipo.Text = ""
        lblTextoDescricao.Text = ""
        txtDiaSemana.Text = ""
        txtOperadoraSainte.Text = ""

        lblTextoTipo.Text = Mid(vdataSet.Tables(0).Rows(0).Item("Nm_Bilhete_Tipo"), 1, 45)
        lblTextoDescricao.Text = Mid(vdataSet.Tables(0).Rows(0).Item("Nm_Bilhete_Descricao"), 1, 45)
        txtDiaSemana.Text = e.Item.Cells(5).Text
        txtOperadoraSainte.Text = e.Item.Cells(15).Text

        pnlMsg.Visible = True
    End Sub

    Public Sub Refresh_Marcacao()
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim vPagina As System.Int32

        vPagina = dtgBilhete.CurrentPageIndex
        dtgBilhete.CurrentPageIndex = 0
        lblPagTotal.Text = dtgBilhete.PageCount
        txtPagCont.Text = dtgBilhete.CurrentPageIndex + 1

        Dim vPakage As System.String = Nothing
        If Request("Visualiza") = "0" Then
            vPakage = "sp_Bilhete_Marcacao"
        Else
            vPakage = "sp_Bilhete_Consulta"
        End If

        Session("DataView") = New Data.DataView(WS_Consulta.Bilhete(Session("Conn_Banco"), vPakage, Request("Lote"), _
                                                                        IIf(Request("Id_Usuario") = Nothing, Session("Id_Usuario"), Request("Id_Usuario")), _
                                                                        Nothing, Nothing).Tables(0), _
                                                                        IIf(chkCustoZero.Checked = False, "Custo <> 0", Nothing), _
                                                                        "Data, Destino ASC", _
                                                                        Data.DataViewRowState.OriginalRows)

        dtgBilhete.DataSource = Session("DataView")
        dtgBilhete.DataBind()

        '-----totaliza valores
        Dim vCont As System.Int32
        Dim vdataSetBilhete As New System.Data.DataView
        vdataSetBilhete = Session("DataView")
        Dim vtotalLigacao As System.Double = 0
        Dim vtotalMarcado As System.Double = 0
        Dim vtotalNaoMarcado As System.Double = 0

        For vCont = 0 To vdataSetBilhete.Table.Rows.Count - 1
            vtotalLigacao = vtotalLigacao + vdataSetBilhete.Table.Rows(vCont).Item("Custo")
            If CType(vdataSetBilhete.Table.Rows(vCont).Item("Marcar"), System.Boolean) = True Then
                If vdataSetBilhete.Table.Rows(vCont).Item("Usuario") = Session("Nm_Usuario") Then
                    vtotalMarcado = vtotalMarcado + vdataSetBilhete.Table.Rows(vCont).Item("Custo")
                Else
                    vtotalNaoMarcado = vtotalNaoMarcado + vdataSetBilhete.Table.Rows(vCont).Item("Custo")
                End If
            Else
                vtotalNaoMarcado = vtotalNaoMarcado + vdataSetBilhete.Table.Rows(vCont).Item("Custo")
            End If
        Next vCont

        '-----carrega valor totalizado no menu
        txtLigacaoTotal.Text = Format(vtotalLigacao, "R$#########0.00")
        txtMarcado.Text = Format(vtotalMarcado, "R$#########0.00")

        '-----cria uma session com valor orifinal do dataset
        Session("DataSetOriginal") = Session("DataView")

        '-----voltar a pagina que estava
        dtgBilhete.CurrentPageIndex = vPagina
        dtgBilhete.DataBind()
        Call formataGrid()
        lblPagTotal.Text = dtgBilhete.PageCount
        txtPagCont.Text = vPagina + 1
    End Sub

    Protected Sub chkCustoZero_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCustoZero.CheckedChanged
        Call Refresh_Marcacao()
    End Sub

    Protected Sub btSincronizaCont_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSincronizaCont.Click
        '-----sincroniza agenda
        Call Sincroniza_Agenda()
    End Sub

    Public Sub Sincroniza_Agenda()
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----monta agenda
        WS_Cadastro.Agenda_Marcacao_Particular(Session("Conn_Banco"), Session("Id_Usuario"), Nothing, Nothing, Nothing, "sp_Cria_Agenda", False)

        '-----sincronixa agenda
        WS_Cadastro.Marcacao(Session("Conn_Banco"), Request("Lote"), Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, "sp_Refresh_Agenda_Marcacao_Particular", False)

        Call Refresh_Marcacao()
    End Sub

    Protected Sub btFecharmsg_Click(sender As Object, e As EventArgs) Handles btFecharmsg.Click
        pnlMsg.Visible = False
    End Sub

    Protected Sub btFecharCustoFixo_Click(sender As Object, e As EventArgs) Handles btFecharCustoFixo.Click
        pnlCustoFixo.Visible = False
    End Sub

    Protected Sub btACFechar_Click(sender As Object, e As EventArgs) Handles btACFechar.Click
        pnlAgenda.Visible = False
    End Sub

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        pnlFiltro.Visible = False
    End Sub

    Protected Sub btGravaAgenda_Click(sender As Object, e As EventArgs) Handles btGravaAgenda.Click
        If lblNumero.Text = "" Or txtContato.Text = "" Then Exit Sub

        WS_Cadastro.Agenda_Marcacao_Particular(Session("Conn_Banco"), _
                                            Session("Id_Usuario"), _
                                            oConfig.ValidaCampo(lblNumero.Text), _
                                            oConfig.ValidaCampo(txtContato.Text), _
                                            IIf(chkParticularContato.Checked = False, 2, 1), _
                                            "sp_SM", _
                                            False)

        '-----sincroniza agenda
        Call Sincroniza_Agenda()

        pnlAgenda.Visible = False
    End Sub

    Protected Sub btFecharExcedente_Click(sender As Object, e As EventArgs) Handles btFecharExcedente.Click
        pnlExcedente.Visible = False
    End Sub

    Protected Sub btEnviar_Click(sender As Object, e As EventArgs) Handles btEnviar.Click
        WS_Cadastro.Excedente_Politica(Session("Conn_Banco"), Request("Lote"), Session("Id_Usuario"), txtJustificativa.Text, "sp_Grava_Justificativa", False)
        '-----fecha lote
        WS_Cadastro.Marcacao(Session("Conn_Banco"), Request("Lote"), Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, "sp_Fecha_Lote_Marcacao", False)
        '-----monta historico do resumo da marcacao
        WS_Cadastro.Bilhete_Historico_Resumo(Session("Conn_Banco"), _
                                                    Request("Lote"), _
                                                    Session("Id_Usuario"), _
                                                    oConfig.ValidaCampo(txtLigacaoTotal.Text), _
                                                    oConfig.ValidaCampo(txtValorPolitica.Text), _
                                                    oConfig.ValidaCampo(txtMarcado.Text), _
                                                    oConfig.ValidaCampo(txtMarcado.Text), _
                                                    "sp_Grava_Historico")

        Response.Redirect("Conta.aspx")
    End Sub

    Protected Sub lbtDestino_Click(sender As Object, e As EventArgs)
        Dim v_btSalvar As LinkButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("lbtDestino_") + 12, 8), System.Int32)

        If Not dtgBilhete.Items(i).Cells(2).Text = "&nbsp;" Then Exit Sub
        If dtgBilhete.Items(i).Cells(11).Text = 1 Then Exit Sub

        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim vdataSet As New System.Data.DataSet
        vdataSet = WS_Consulta.Bilhete(Session("Conn_Banco"), "sp_Retorna_Bilente_Tipo", Nothing, Nothing, dtgBilhete.Items(i).Cells(9).Text, Nothing)

        lblNumero.Text = ""
        txtContato.Text = ""
        chkParticularContato.Checked = False

        lblNumero.Text = dtgBilhete.Items(i).Cells(13).Text
        txtContato.Text = IIf(dtgBilhete.Items(i).Cells(2).Text = "&nbsp;", "", dtgBilhete.Items(i).Cells(2).Text)

        pnlAgenda.Visible = True
    End Sub

    Protected Sub btAprovar_Barra_Click(sender As Object, e As EventArgs) Handles btAprovar_Barra.Click
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----verifica excedente
        If 1 = WS_Cadastro.Excedente_Politica(Session("Conn_Banco"), Request("Lote"), Session("Id_Usuario"), Nothing, "sp_Valida_Excedente", True).Tables(0).Rows(0).Item("Validacao") Then
            pnlExcedente.Visible = True
            vdataset = WS_Cadastro.Excedente_Politica(Session("Conn_Banco"), Request("Lote"), Session("Id_Usuario"), Nothing, "sp_Lista_Excedente", True)

            lblSubValorTotal.Text = vdataset.Tables(0).Rows(0).Item("Total")
            lblSubValorParticular.Text = vdataset.Tables(0).Rows(0).Item("Particular")
            lblSubValorServico.Text = vdataset.Tables(0).Rows(0).Item("Servico")
            lblSubValorPolitica.Text = vdataset.Tables(0).Rows(0).Item("Valor_Politica")
            lblSubValorTrafego.Text = vdataset.Tables(0).Rows(0).Item("Trafego")
            lblSubValorExcedente.Text = vdataset.Tables(0).Rows(0).Item("Excedente")
        Else
            '-----fecha lote
            WS_Cadastro.Marcacao(Session("Conn_Banco"), Request("Lote"), Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, "sp_Fecha_Lote_Marcacao", False)

            '-----envia por email protocolo de desconto
            WS_Cadastro.Marcacao(Session("Conn_Banco"), Request("Lote"), Nothing, Session("Id_Usuario"), Nothing, Nothing, Nothing, "sp_E_Mail_Marcacao_Particular", False)

            '-----monta historico do resumo da marcacao
            WS_Cadastro.Bilhete_Historico_Resumo(Session("Conn_Banco"),
                                                        Request("Lote"),
                                                        Session("Id_Usuario"),
                                                        oConfig.ValidaCampo(txtLigacaoTotal.Text),
                                                        oConfig.ValidaCampo(txtValorPolitica.Text),
                                                        oConfig.ValidaCampo(txtMarcado.Text),
                                                        oConfig.ValidaCampo(txtMarcado.Text),
                                                        "sp_Grava_Historico")

            Response.Redirect("~/CockPit_Menu.aspx?Postback=1")
        End If
    End Sub

    Protected Sub btFiltro_Click(sender As Object, e As EventArgs)
        pnlFiltro.Visible = True
    End Sub

    Protected Sub btPagAnterior_Click(sender As Object, e As EventArgs)
        dtgBilhete.CurrentPageIndex = 0
        vintPagina = dtgBilhete.CurrentPageIndex
        Call atualizaGrid()
    End Sub

    Protected Sub btPagPrimerio_Click(sender As Object, e As EventArgs)
        If dtgBilhete.CurrentPageIndex > 0 Then
            dtgBilhete.CurrentPageIndex = Me.dtgBilhete.CurrentPageIndex - 1
            vintPagina = dtgBilhete.CurrentPageIndex
            Call atualizaGrid()
        End If
    End Sub

    Protected Sub btPagUltimo_Click(sender As Object, e As EventArgs)
        If dtgBilhete.CurrentPageIndex < dtgBilhete.PageCount - 1 Then
            dtgBilhete.CurrentPageIndex = dtgBilhete.CurrentPageIndex + 1
            vintPagina = dtgBilhete.CurrentPageIndex
            Call atualizaGrid()
        End If
    End Sub

    Protected Sub btPagProximo_Click(sender As Object, e As EventArgs)
        dtgBilhete.CurrentPageIndex = dtgBilhete.PageCount - 1
        vintPagina = dtgBilhete.CurrentPageIndex
        Call atualizaGrid()
    End Sub

    Protected Sub btUsuarioGrupo_Click(sender As Object, e As EventArgs)
        pnlCustoFixo.Visible = True
    End Sub

    Protected Sub btFecharMsgPopup_Click(sender As Object, e As EventArgs)
        pnlDetalhe.Visible = False
    End Sub
    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
