
Public Class Conferencia_Fatura
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Rateio As New WS_GUA_Rateio.WSRateio
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim oConfig As New cls_Config
    Protected WithEvents txtValor As System.Web.UI.WebControls.TextBox

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Recepção de Fatura ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            btConfiguracao.PostBackUrl = "~/Recepcao_Fatura/Fatura.aspx"

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboFaturaParametro)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))
            oConfig.CarregaCombo(cboFaturaParametro, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Fatura_Parametro", Nothing))

            '-----gera data lote
            Dim vDataSet As New Data.DataSet
            vDataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            oConfig.CarregaCombo(cboDataLote, vDataSet)

            Session("DataSet") = Nothing
        End If
    End Sub

    Protected Sub btExecutar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExecutar.Click
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials

        If Not cboFaturaParametro.SelectedValue = Nothing And Not cboConglomerado.SelectedValue = Nothing And Not cboDataLote.SelectedValue = Nothing Then
            Session("DataSet") = WS_Rateio.Fatura(Session("Conn_Banco"), Nothing, cboFaturaParametro.SelectedValue, Nothing, Nothing,
                                                        cboDataLote.SelectedValue,
                                                        Nothing, Nothing, Nothing, Nothing,
                                                        oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                                        Session("Id_Usuario"), "sp_SL_Conferencia_Recepcao", True,
                                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            dtgFatura.DataSource = Session("DataSet")
            dtgFatura.DataBind()
            Call soma_Fatura()

            lblDescricaoArquivo.Text = "Arquivo | Tipo da Fatura: " & cboFaturaParametro.SelectedItem.Text & " - Conglomerado: " & cboConglomerado.SelectedItem.Text & " - Lote: " & cboDataLote.SelectedItem.Text

            '-----mostra caixa de texto
            lblTotalFatura.Visible = True
            txtTotalFatura.Visible = True
            bt_Recalcular.Visible = True
            DivAtivo.Visible = True

            '-----msg quando fatura ja foi criada 
            If dtgFatura.Items.Count = 0 Then
                Call Master.Registro_Salvo("* Fatura importada ou carga não realizada")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
            End If
        End If
    End Sub

    Public Sub soma_Fatura()
        If dtgFatura.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32 = 0
        Dim vValorFaura As System.Double = 0
        Dim v_txtValor As TextBox

        For i = 0 To dtgFatura.Items.Count - 1
            v_txtValor = dtgFatura.Items(i).Cells(1).Controls(3)
            If IsNumeric(v_txtValor.Text) = True Then
                vValorFaura = vValorFaura + v_txtValor.Text
            End If
        Next i

        txtTotalFatura.Text = Format(vValorFaura, "##,###.#0")
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials

        If dtgFatura.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_txtValor As TextBox
        Dim v_Valida As System.Int32 = 0

        '----validade valor
        For i = 0 To dtgFatura.Items.Count - 1
            v_txtValor = dtgFatura.Items(i).Cells(1).Controls(3)
            If IsNumeric(v_txtValor.Text) = False Then
                v_Valida = 1
            End If
            If v_Valida = 0 Then
                If CType(v_txtValor.Text, Double) = 0 Then
                    v_Valida = 1
                End If
            End If
        Next i
        If v_Valida = 1 Then Exit Sub

        '-----grava nota
        For i = 0 To dtgFatura.Items.Count - 1
            v_txtValor = dtgFatura.Items(i).Cells(1).Controls(3)
            WS_Rateio.Fatura(Session("Conn_Banco"),
                                 Nothing,
                                 cboFaturaParametro.SelectedValue,
                                 dtgFatura.Items(i).Cells(0).Text,
                                 Nothing,
                                 cboDataLote.SelectedValue,
                                 Nothing,
                                 Nothing,
                                 IIf(Trim(v_txtValor.Text) = "", Nothing, v_txtValor.Text),
                                 Nothing,
                                 Nothing,
                                 Session("Id_Usuario"),
                                 "sp_SL_Insert_Fatura_Conferencia",
                                 False,
                                 Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Next i

        '-----registro salvo ok
        cboDataLote.Enabled = False
        cboConglomerado.Enabled = False
        cboFaturaParametro.Enabled = False
        dtgFatura.Enabled = False

        dtgFatura.DataSource = Nothing
        dtgFatura.DataBind()
        txtTotalFatura.Text = ""

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String = Nothing
        '-----nome do arquivo a ser exportado
        Dim Descricao As System.String = "Planta_Pagamento"
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

    Protected Sub bt_Recalcular_Click(sender As Object, e As EventArgs)
        Call soma_Fatura()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class

