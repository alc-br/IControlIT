
Public Class Fatura
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Rateio As New WS_GUA_Rateio.WSRateio
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet
    Public vClientClick As String

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
                "Lançamento de Fatura ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)

            Page.SetFocus(txtNumeroFatura)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Fatura", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboFaturaParametro, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Fatura_Parametro", Nothing))
            '-----gera data lote
            Dim vDataSet As New Data.DataSet
            vDataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            oConfig.CarregaCombo(cboDataLote, vDataSet)

            If Not Request("ID") = Nothing Then
                vDataSet = WS_Rateio.Fatura(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                 Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                Dim Identificacao = vDataSet.Tables(0).Rows(0).Item("Id_Fatura")
                txtIdentificacao.Text = Identificacao
                vClientClick = Identificacao
                cboFaturaParametro.SelectedValue = vDataSet.Tables(0).Rows(0).Item("Id_Fatura_Parametro")
                txtNumeroFatura.Text = vDataSet.Tables(0).Rows(0).Item("Nr_Fatura")
                txtDescricao.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Fatura")
                cboDataLote.SelectedValue = vDataSet.Tables(0).Rows(0).Item("Dt_Lote")
                txtEmissao.Text = vDataSet.Tables(0).Rows(0).Item("Dt_Emissao")
                txtVencimento.Text = vDataSet.Tables(0).Rows(0).Item("Dt_Vencimento")
                txtValorFatura.Text = vDataSet.Tables(0).Rows(0).Item("Vr_Fatura")
                txtNotaFiscal.Text = vDataSet.Tables(0).Rows(0).Item("Nota_Fiscal")
                txtPedido.Text = vDataSet.Tables(0).Rows(0).Item("Pedido")
                txtRef.Text = vDataSet.Tables(0).Rows(0).Item("Ref")
                txtReq.Text = vDataSet.Tables(0).Rows(0).Item("Req")
                CkbProvisao.Checked = vDataSet.Tables(0).Rows(0).Item("Provisao")
                txtObs.Text = vDataSet.Tables(0).Rows(0).Item("Observacao")

                '-----monta complemento do ativo
                If cboFaturaParametro.SelectedValue = Nothing Then Exit Sub
                '-----monta complemento do ativo
                dtgDadoFatura.DataSource = WS_Rateio.Fatura(Session("Conn_Banco"), Request("ID"), cboFaturaParametro.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                                Nothing, Nothing, Nothing, "sp_SL_Dados_Fatura", True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                dtgDadoFatura.DataBind()

                '-----monta quando inserio novo campo depois de ter criado o registro
                If dtgDadoFatura.Items.Count = 0 Then
                    dtgDadoFatura.DataSource = WS_Rateio.Fatura(Session("Conn_Banco"), Nothing, cboFaturaParametro.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                Nothing, Nothing, Nothing, "sp_SL_Campo_Insert", True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    dtgDadoFatura.DataBind()
                End If
            End If
        End If
    End Sub

    Protected Sub cboFaturaParametro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFaturaParametro.SelectedIndexChanged
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials

        If cboFaturaParametro.SelectedValue = Nothing Then Exit Sub
        '-----monta complemento do ativo
        dtgDadoFatura.DataSource = WS_Rateio.Fatura(Session("Conn_Banco"), Nothing, cboFaturaParametro.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                        Nothing, Nothing, Nothing, "sp_SL_Campo_Insert", True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        dtgDadoFatura.DataBind()
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboFaturaParametro.SelectedValue = Nothing
        cboDataLote.SelectedValue = Nothing
        dtgDadoFatura.DataSource = Nothing
        dtgDadoFatura.DataBind()
        Page.SetFocus(cboFaturaParametro)
    End Sub

    Public Function montaComplemento(ByVal pGrid As DataGrid) As System.String
        Dim i As System.Int32
        Dim vLabel As Label
        Dim vText As TextBox
        Dim vRetorno As String = Nothing

        For i = 0 To pGrid.Items.Count - 1
            vLabel = pGrid.Items(i).Cells(0).Controls(1)
            vText = pGrid.Items(i).Cells(0).Controls(3)
            If Not Trim(vLabel.Text) = "" Then
                vText.Text = IIf(Trim(vText.Text) = "", "0", vText.Text)
                vRetorno = vRetorno & "<" & vLabel.Text & ">" &
                                    vText.Text &
                                    "</" & vLabel.Text & ">"
                'vRetorno = vRetorno & "<" & vLabel.Text & ">" &
                '                    IIf(IsNumeric(Replace(Replace(vText.Text, ".", ""), ",", ".")), Replace(Replace(vText.Text, ".", ""), ",", "."), "0") &
                '                    "</" & vLabel.Text & ">"
            End If
        Next
        vRetorno = IIf(vRetorno = Nothing, Nothing, "<dados>" & vRetorno & "</dados>")
        Return vRetorno
    End Function

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----nao insere registro quando descricao so for numerica
        If IsNumeric(txtDescricao.Text) Then Exit Sub
        WS_Rateio.Fatura(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                        oConfig.ValidaCampo(cboFaturaParametro.SelectedValue),
                                        oConfig.ValidaCampo(txtNumeroFatura.Text),
                                        oConfig.ValidaCampo(txtDescricao.Text),
                                        oConfig.ValidaCampo(cboDataLote.SelectedValue),
                                        IIf(Trim(txtEmissao.Text) = "", Nothing, txtEmissao.Text),
                                        IIf(Trim(txtVencimento.Text) = "", Nothing, txtVencimento.Text),
                                        oConfig.ValidaCampo(txtValorFatura.Text),
                                        montaComplemento(dtgDadoFatura),
                                        Nothing,
                                        Session("Id_Usuario"),
                                        "sp_SM",
                                        False,
                                        oConfig.ValidaCampo(txtNotaFiscal.Text),
                                        oConfig.ValidaCampo(txtPedido.Text),
                                        oConfig.ValidaCampo(txtRef.Text),
                                        oConfig.ValidaCampo(txtReq.Text),
                                        oConfig.ValidaCheckbox(CkbProvisao.Checked),
                                        oConfig.ValidaCampo(txtObs.Text)
                                        )
        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)

        '-----limp atela se for primeiro registro
        If Trim(txtIdentificacao.Text) = "" Then limpar()
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials

        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Rateio.Fatura(Session("Conn_Banco"), txtIdentificacao.Text,
                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                            Nothing, Nothing, Nothing, Session("Id_Usuario"), "sp_SE",
                            False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing
                            )
        Call limpar()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
