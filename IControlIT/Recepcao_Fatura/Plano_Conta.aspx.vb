
Public Class Plano_Conta
    Inherits System.Web.UI.Page
    Dim WS_Rateio As New WS_GUA_Rateio.WSRateio
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                "Centralização de Conta ", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboConglomerado)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)
            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))
            Session("DataSet") = Nothing

            If Request("lstConglomerado") IsNot Nothing Then
                Call Executar(Request("lstConglomerado"))
            End If

            If Request("lstServico") IsNot Nothing Then
                Call Executar(Request("lstServico"))
            End If
        End If
    End Sub

    Protected Sub dtgPolitica_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPlanoConta.PageIndexChanged
        dtgPlanoConta.CurrentPageIndex = e.NewPageIndex
        dtgPlanoConta.DataSource = Session("DataSet")
        dtgPlanoConta.DataBind()
        Call monta_Empresa()
        Call monta_Contrato()
    End Sub

    Protected Sub btDesativa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btDesativa") + 12, 2), System.Int32)

        '-----deleta registro
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                    dtgPlanoConta.Items(i).Cells(0).Text,
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Session("Id_Usuario"),
                                    Nothing,
                                    "sp_SE",
                                    False)
        '-----zera paginacao
        dtgPlanoConta.CurrentPageIndex = Nothing
        '-----refresh de consulta
        Call Executar(Nothing)
    End Sub

    Public Sub monta_Empresa()
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        If dtgPlanoConta.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_cbo_Empresa_Grid As DropDownList

        For i = 0 To dtgPlanoConta.Items.Count - 1
            v_cbo_Empresa_Grid = dtgPlanoConta.Items(i).Cells(4).Controls(1)
            oConfig.CarregaCombo(v_cbo_Empresa_Grid, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Empresa", Nothing))
            v_cbo_Empresa_Grid.SelectedValue = dtgPlanoConta.Items(i).Cells(1).Text
        Next i
    End Sub

    Public Sub monta_Contrato()
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        If dtgPlanoConta.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_cbo_Contrato_Grid As DropDownList

        For i = 0 To dtgPlanoConta.Items.Count - 1
            v_cbo_Contrato_Grid = dtgPlanoConta.Items(i).Cells(5).Controls(1)
            oConfig.CarregaCombo(v_cbo_Contrato_Grid, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Contrato", Nothing))
            v_cbo_Contrato_Grid.SelectedValue = dtgPlanoConta.Items(i).Cells(2).Text
        Next i
    End Sub

    Protected Sub btExecutar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExecutar.Click
        Call Executar(Nothing)
    End Sub

    Public Sub Executar(plista As String)
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        If cboConglomerado.SelectedValue = Nothing And plista = Nothing Then Exit Sub
        Session("DataSet") = WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                                        Nothing, Nothing,
                                                        IIf(plista = Nothing, cboConglomerado.SelectedValue, plista),
                                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, plista,
                                                        "sp_Consulta_Plano_Conta",
                                                        True)

        If Session("DataSet").Tables(0).Rows.Count = 0 Then
            lblMsg.Visible = True
            dtgPlanoConta.Visible = False
        Else
            lblMsg.Visible = False
            dtgPlanoConta.Visible = True
            dtgPlanoConta.CurrentPageIndex = 0
            dtgPlanoConta.DataSource = Session("DataSet")
            dtgPlanoConta.DataBind()
            Call monta_Empresa()
            Call monta_Contrato()
        End If

    End Sub
    Protected Sub btAdicionar_Click(sender As Object, e As EventArgs)

        Call Salvar()
        If cboConglomerado.SelectedValue = "" Then Exit Sub

        If Not Session("DataSet") Is Nothing Then
            WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                        Nothing, Nothing,
                                        cboConglomerado.SelectedValue,
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Session("Id_Usuario"),
                                        Nothing,
                                        "sp_Insere_Plano_Conta",
                                        False)
            '-----zera paginacao
            dtgPlanoConta.CurrentPageIndex = Nothing
            '-----refresh de consulta
            Call Executar(Nothing)
        End If
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)

        '-----chama função salvar
        Call Salvar()

        '-----registro salvo ok
        'dtgPlanoConta.Enabled = False

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)

        Call Executar(Nothing)
    End Sub

    Protected Sub Salvar()
        WS_Rateio.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        If Trim(cboConglomerado.SelectedValue) = "" Then Exit Sub

        If dtgPlanoConta.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_txtNr_Fatura As TextBox
        Dim v_cboEmpresa As DropDownList
        Dim v_cboContrato As DropDownList
        Dim v_txtDia_Vencimento As TextBox
        Dim v_txtLote_Cancelamento As TextBox
        Dim v_txtDescricao As TextBox

        For i = 0 To dtgPlanoConta.Items.Count - 1
            v_txtNr_Fatura = dtgPlanoConta.Items(i).Cells(3).Controls(1)
            v_cboEmpresa = dtgPlanoConta.Items(i).Cells(4).Controls(1)
            v_cboContrato = dtgPlanoConta.Items(i).Cells(5).Controls(1)
            v_txtDia_Vencimento = dtgPlanoConta.Items(i).Cells(6).Controls(3)
            v_txtLote_Cancelamento = dtgPlanoConta.Items(i).Cells(7).Controls(1)
            v_txtDescricao = dtgPlanoConta.Items(i).Cells(8).Controls(1)

            WS_Rateio.Plano_Conta(Session("Conn_Banco"),
                                                dtgPlanoConta.Items(i).Cells(0).Text,
                                                IIf(Trim(v_txtNr_Fatura.Text) = "", Nothing, v_txtNr_Fatura.Text),
                                                IIf(Trim(cboConglomerado.SelectedValue) = "", Nothing, cboConglomerado.SelectedValue),
                                                IIf(Trim(v_cboEmpresa.SelectedValue) = "", Nothing, v_cboEmpresa.SelectedValue),
                                                IIf(Trim(v_cboContrato.SelectedValue) = "", Nothing, v_cboContrato.SelectedValue),
                                                IIf(Trim(v_txtDia_Vencimento.Text) = "", Nothing, v_txtDia_Vencimento.Text),
                                                IIf(Trim(v_txtLote_Cancelamento.Text) = "", Nothing, v_txtLote_Cancelamento.Text),
                                                IIf(Trim(v_txtDescricao.Text) = "", Nothing, v_txtDescricao.Text),
                                                Nothing,
                                                Session("Id_Usuario"),
                                                Nothing,
                                                "sp_Atualiza_Plano_Conta",
                                                False)
        Next i
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class

