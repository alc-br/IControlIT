Imports System.Web.UI.DataVisualization.Charting

Public Class App_Dados
    Inherits System.Web.UI.Page
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vdataset As System.Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Monitoramento de Dados Móveis ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboDataDe)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            '-----carrega combo
            oConfig.CarregaCombo(cboDataDe, WS_Modulo.Monitoramento_Dados(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Lista_Mes", True))
            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))
        End If
    End Sub

    Public Sub Executar()

        Dim i As System.Int32
        Dim Cont As System.Int32
        Dim Dt_Lote(0) As System.String
        Dim fDt_Lote(0) As System.String
        Dim Custo(0) As System.Double
        Dim fCusto(0) As System.Double

        vdataset = WS_Modulo.Monitoramento_Dados(Session("Conn_Banco"),
                                                    oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                    oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    "Lista_Totais",
                                                    True)

        If vdataset.Tables(0).Rows.Count > 0 Then
            '-----lista detalahamento totais----------------------------------------------
            Dim vQTD_Ativo As System.Int32 = 0
            Dim vConsumo_App As System.Double = 0
            Dim vPacote_Contratado As System.Double = 0

            For i = 0 To vdataset.Tables(0).Rows.Count - 1
                vQTD_Ativo = vQTD_Ativo + vdataset.Tables(0).Rows(i).Item("QTD_Ativo")
                vConsumo_App = vConsumo_App + vdataset.Tables(0).Rows(i).Item("Consumo_App")
                vPacote_Contratado = vPacote_Contratado + vdataset.Tables(0).Rows(i).Item("Pacote_Contratado")
            Next

            txtMBOperadora.Text = vPacote_Contratado
            txtMBConsumo.Text = vConsumo_App
            txtPacoteLinha.Text = vPacote_Contratado / vQTD_Ativo
            txtQtdLinha.Text = vQTD_Ativo
            txtMBConsumo.ForeColor = IIf(vConsumo_App > vPacote_Contratado, Drawing.Color.Red, Drawing.Color.Black)
            lblMBConsumo.BackColor = IIf(vConsumo_App > vPacote_Contratado, Drawing.Color.Red, Drawing.Color.Silver)

            '-----lista curva de consumo----------------------------------------------
            vdataset = WS_Modulo.Monitoramento_Dados(Session("Conn_Banco"),
                                                    oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                    oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    "Lista_Consumo_Dia",
                                                    True)

            i = 0
            Cont = 0
            ReDim Dt_Lote(1000)
            ReDim Custo(1000)

            If vdataset.Tables(0).Rows.Count > 0 Then
                For i = 0 To vdataset.Tables(0).Rows.Count - 1
                    If vdataset.Tables(0).Rows(i).Item("Dt_Trafego") = Dt_Lote(Cont) Then
                        Custo(Cont) = Custo(Cont) + vdataset.Tables(0).Rows(i).Item("Consumo_App")
                    Else
                        If i > 0 Then Cont = Cont + 1
                        Dt_Lote(Cont) = vdataset.Tables(0).Rows(i).Item("Dt_Trafego")
                        Custo(Cont) = Custo(Cont) + vdataset.Tables(0).Rows(i).Item("Consumo_App")
                    End If
                Next

                ReDim fDt_Lote(Cont)
                ReDim fCusto(Cont)
                For i = 0 To Cont
                    fDt_Lote(i) = Dt_Lote(i)
                    fCusto(i) = Custo(i)
                Next i
                chtCurvaGasto.Series("A1").Points.DataBindXY(fDt_Lote, fCusto)
                chtCurvaGasto.Series("A1").BackGradientStyle = GradientStyle.TopBottom
                chtCurvaGasto.Series("A1").Color = Drawing.ColorTranslator.FromHtml("#3d72b4")
                chtCurvaGasto.Series("A1").BackSecondaryColor = Drawing.ColorTranslator.FromHtml("#525252")
            Else
                chtCurvaGasto.Series("A1").Points.Clear()
            End If

            '-----lista consumo por horario----------------------------------------------
            vdataset = WS_Modulo.Monitoramento_Dados(Session("Conn_Banco"),
                                                    oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                    oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    "Lista_Consumo_horario",
                                                    True)

            i = 0
            Cont = 0
            ReDim Dt_Lote(2)
            ReDim fCusto(2)
            Dt_Lote(0) = "Final de Semana"
            Dt_Lote(1) = "Fora do Horário"
            Dt_Lote(2) = "Dentro do Horário"

            If vdataset.Tables(0).Rows.Count > 0 Then
                fCusto(0) = Format(vdataset.Tables(0).Rows(0).Item("Consumo_App"), "##########,###########0")
                fCusto(1) = Format(vdataset.Tables(0).Rows(1).Item("Consumo_App"), "##########,###########0")
                fCusto(2) = Format(vdataset.Tables(0).Rows(2).Item("Consumo_App"), "##########,###########0")

                chtFH_FS.Series(0).Points.DataBindXY(Dt_Lote, fCusto)
                chtFH_FS.Series(0).BackGradientStyle = GradientStyle.TopBottom
                chtFH_FS.Series(0).Color = Drawing.ColorTranslator.FromHtml("#3d72b4")
                chtFH_FS.Series(0).BackSecondaryColor = Drawing.ColorTranslator.FromHtml("#525252")
            Else
                chtFH_FS.Series(0).Points.Clear()
            End If

            '-----lista consumo por aplicativo----------------------------------------------
            vdataset = WS_Modulo.Monitoramento_Dados(Session("Conn_Banco"),
                                                    oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                    oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    "Lista_Consumo_Aplicativo",
                                                    True)

            i = 0
            Cont = 0
            ReDim Dt_Lote(1000)
            ReDim Custo(1000)

            If vdataset.Tables(0).Rows.Count > 0 Then
                For i = 0 To vdataset.Tables(0).Rows.Count - 1
                    If vdataset.Tables(0).Rows(i).Item("Nm_Aplicativo") = Dt_Lote(Cont) Then
                        Custo(Cont) = Custo(Cont) + vdataset.Tables(0).Rows(i).Item("Consumo_App")
                    Else
                        If i > 0 Then Cont = Cont + 1
                        Dt_Lote(Cont) = vdataset.Tables(0).Rows(i).Item("Nm_Aplicativo")
                        Custo(Cont) = Custo(Cont) + vdataset.Tables(0).Rows(i).Item("Consumo_App")
                    End If
                Next

                ReDim fDt_Lote(Cont)
                ReDim fCusto(Cont)
                For i = 0 To Cont
                    fDt_Lote(i) = Dt_Lote(i)
                    fCusto(i) = Custo(i)
                Next i
                chtAplicativo.Series(0).Points.DataBindXY(fDt_Lote, fCusto)
                chtAplicativo.Series(0).BackGradientStyle = GradientStyle.TopBottom
                chtAplicativo.Series(0).Color = Drawing.ColorTranslator.FromHtml("#3d72b4")
                chtAplicativo.Series(0).BackSecondaryColor = Drawing.ColorTranslator.FromHtml("#525252")
            Else
                chtAplicativo.Series(0).Points.Clear()
            End If

            '-----lista consumo por usuario----------------------------------------------
            vdataset = WS_Modulo.Monitoramento_Dados(Session("Conn_Banco"),
                                                    oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                    oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    "Lista_Consumo_Usuario",
                                                    True)
            dtgConsumoUsuario.DataSource = vdataset
            dtgConsumoUsuario.DataBind()

            '-----lista consumo por usuario----------------------------------------------
            vdataset = WS_Modulo.Monitoramento_Dados(Session("Conn_Banco"),
                                                    oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                    oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    "Lista_Sem_Conexao_App",
                                                    True)
            dtgConexao.DataSource = vdataset
            dtgConexao.DataBind()
        Else
            '-----retorno vazio
            Call Master.Registro_Salvo("Não foram encontradas informações para o filtro selecionado.")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)

            txtMBOperadora.Text = ""
            txtMBConsumo.Text = ""
            txtPacoteLinha.Text = ""
            txtQtdLinha.Text = ""
            chtAplicativo.Series(0).Points.Clear()
            chtFH_FS.Series(0).Points.Clear()
            chtCurvaGasto.Series("A1").Points.Clear()
            dtgConsumoUsuario.DataSource = Nothing
            dtgConsumoUsuario.DataBind()
        End If
    End Sub

    Protected Sub dtgConsumoUsuario_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dtgConsumoUsuario.SelectedIndexChanged
        pnlListaAplicativo.Visible = True
        lblListaAplicativo.Text = "Aplicativos Utilizados"

        vdataset = WS_Modulo.Monitoramento_Dados(Session("Conn_Banco"),
                                                oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                                dtgConsumoUsuario.SelectedItem.Cells(0).Text,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                "Lista_Consumo_Det_Aplicativo",
                                                True)

        txtCorteDe.Text = vdataset.Tables(0).Rows(0).Item("Corte_De")
        txtCorteAte.Text = vdataset.Tables(0).Rows(0).Item("Corte_Ate")
        txtPacote.Text = vdataset.Tables(0).Rows(0).Item("Pacote_MB")
        txtChip.Text = vdataset.Tables(0).Rows(0).Item("CHIP")
        txtImei.Text = vdataset.Tables(0).Rows(0).Item("IMEI")
        txtNoFiscal.Text = vdataset.Tables(0).Rows(0).Item("Nr_Nota_Fiscal")
        txtDataNF.Text = vdataset.Tables(0).Rows(0).Item("Dt_Nota_Fiscal")
        txtContrato.Text = vdataset.Tables(0).Rows(0).Item("Qtd_Mes_Residuo_Fr_Aquisicao")
        txtModelo.Text = vdataset.Tables(0).Rows(0).Item("Nm_Ativo_Modelo")
        txtValor.Text = vdataset.Tables(0).Rows(0).Item("Vr_Fr_Aquisicao")

        dtgListaAplicativo.DataSource = vdataset
        dtgListaAplicativo.DataBind()
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As EventArgs) Handles btExecutar.Click
        Call Executar()
        pnlMsg.Visible = False
    End Sub

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        pnlMsg.Visible = False
    End Sub

    Protected Sub btFecharLista_Click(sender As Object, e As EventArgs) Handles btFecharLista.Click
        pnlListaAplicativo.Visible = False
    End Sub

    Protected Sub btAbrir_Click(sender As Object, e As EventArgs)
        pnlMsg.Visible = True
    End Sub

    Protected Sub btnHorario_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnAplicativo_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnTendencia_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub btnConexao_Click(sender As Object, e As EventArgs)
        Valida_Tab(sender)
    End Sub

    Protected Sub Valida_Tab(ByVal btn As Button)

        divHorario.Visible = False
        divAplicativo.Visible = False
        divTendencia.Visible = False
        divConexaoMenu.Visible = False
        btnHorario.CssClass = "btn-tab-disable pull-left"
        btnAplicativo.CssClass = "btn-tab-disable pull-left"
        btnTendencia.CssClass = "btn-tab-disable pull-left"
        btnConexao.CssClass = "btn-tab-disable pull-left"

        If btn.Text = "Horário" Then
            divHorario.Visible = True
            btnHorario.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Aplicativo" Then
            divAplicativo.Visible = True
            btnAplicativo.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Tendência" Then
            divTendencia.Visible = True
            btnTendencia.CssClass = "btn-tab pull-left"
        ElseIf btn.Text = "Sem Conexão" Then
            divConexaoMenu.Visible = True
            btnConexao.CssClass = "btn-tab pull-left"
        End If

    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class

