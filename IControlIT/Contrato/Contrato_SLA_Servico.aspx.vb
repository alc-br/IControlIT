
Public Class Contrato_SLA_Servico
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Contrato As New WS_GUA_Contrato.WSContrato
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet
    Dim vdatasetHist As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Cadastro de Produto do Contrato",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboContrato)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Contrato_SLA_Servico", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboContrato, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Contrato", Nothing))
            oConfig.CarregaCombo(cboOperadora, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))
            oConfig.CarregaCombo(cboIndice, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Contrato_Indice", Nothing))

            If Not Request("ID") = Nothing Then
                vdataset = WS_Contrato.Contrato_SLA_Servico(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID_V2", True)
                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Contrato_SLA_Servico")
                cboContrato.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Contrato")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Descricao")
                cboOperadora.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Operadora")
                Call load_cboTipoServico()
                cboTipoServico.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Tipo_Servico")
                txtValorSLA.Text = vdataset.Tables(0).Rows(0).Item("Vr_SLA_Servico")
                cboIndice.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Contrato_Indice")

                vdatasetHist = WS_Contrato.Contrato_SLA_Servico(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_HIS_ID", True)
                If vdatasetHist.Tables(0).Rows.Count > 0 Then
                    txtHistContrato.Text = vdatasetHist.Tables(0).Rows(0).Item("Contrato")
                    txtHistDescricao.Text = vdatasetHist.Tables(0).Rows(0).Item("Descricao")
                    txtHistOperadora.Text = vdatasetHist.Tables(0).Rows(0).Item("Operadora")
                    txtHistTipoServico.Text = vdatasetHist.Tables(0).Rows(0).Item("Tipo_Servico")
                    txtHistValor.Text = vdatasetHist.Tables(0).Rows(0).Item("Vr_SLA_Servico")
                    txtHistIndice.Text = vdatasetHist.Tables(0).Rows(0).Item("Contrato_Indice")
                    txtHistIdentificacao.Text = vdatasetHist.Tables(0).Rows(0).Item("Id_Contrato_SLA_Servico")
                    txtDtAlteracao.Text = vdatasetHist.Tables(0).Rows(0).Item("Dt_Alteracao")
                End If
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboContrato.SelectedValue = Nothing
        cboOperadora.SelectedValue = Nothing
        cboIndice.SelectedValue = Nothing
        Call load_cboTipoServico()
        btSalvar.Enabled = True
        Page.SetFocus(cboContrato)
    End Sub
    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)

        WS_Contrato.Contrato_SLA_Servico(Session("Conn_Banco"),
                                                oConfig.ValidaCampo(txtIdentificacao.Text),
                                                oConfig.ValidaCampo(cboContrato.SelectedValue),
                                                oConfig.ValidaCampo(Mid(txtDescricao.Text, 1, 8000)),
                                                oConfig.ValidaCampo(cboOperadora.SelectedValue),
                                                oConfig.ValidaCampo(cboTipoServico.SelectedItem.Text),
                                                oConfig.ValidaCampo(txtValorSLA.Text),
                                                oConfig.ValidaCampo(cboIndice.SelectedValue),
                                                Session("Id_Usuario"),
                                                "sp_SM",
                                                False)

        vdatasetHist = WS_Contrato.Contrato_SLA_Servico(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_HIS_ID", True)
        If vdatasetHist.Tables(0).Rows.Count > 0 Then
            txtHistContrato.Text = vdatasetHist.Tables(0).Rows(0).Item("Contrato")
            txtHistDescricao.Text = vdatasetHist.Tables(0).Rows(0).Item("Descricao")
            txtHistOperadora.Text = vdatasetHist.Tables(0).Rows(0).Item("Operadora")
            txtHistTipoServico.Text = vdatasetHist.Tables(0).Rows(0).Item("Tipo_Servico")
            txtHistValor.Text = vdatasetHist.Tables(0).Rows(0).Item("Vr_SLA_Servico")
            txtHistIndice.Text = vdatasetHist.Tables(0).Rows(0).Item("Contrato_Indice")
            txtHistIdentificacao.Text = vdatasetHist.Tables(0).Rows(0).Item("Id_Contrato_SLA_Servico")
            txtDtAlteracao.Text = vdatasetHist.Tables(0).Rows(0).Item("Dt_Alteracao")
        End If

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Contrato.Contrato_SLA_Servico(Session("Conn_Banco"),
                                                txtIdentificacao.Text,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Session("Id_Usuario"),
                                                "sp_SE",
                                                False)
        Call limpar()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub

    Protected Sub load_cboTipoServico()
        If String.IsNullOrEmpty(cboOperadora.SelectedValue) Then
            cboTipoServico.Enabled = False
            cboTipoServico.SelectedValue = Nothing
        Else
            oConfig.CarregaCombo(cboTipoServico, WS_Cadastro.Bilhete_Tipo(Session("Conn_Banco"), Nothing, cboOperadora.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Drop_Bilhete", True))
            cboTipoServico.Enabled = True
        End If

    End Sub

    Protected Sub cboOperadora_Selected(sender As Object, e As EventArgs)
        Call load_cboTipoServico()
    End Sub

    Protected Sub btRecalc_Click(sender As Object, e As EventArgs)
        If Char.IsNumber(txtValorSLA.Text) Then
            If Char.IsNumber(txtPorcentagem.Text) Then
                txtPorcentagem.Text = Replace(txtPorcentagem.Text, ".", ",")
                Dim reCalc = Double.Parse(txtValorSLA.Text) * Double.Parse(txtPorcentagem.Text)
                txtValorSLA.Text = reCalc.ToString("n4")
            Else
                txtPorcentagem.Focus()
                txtPorcentagem.Text = Nothing
            End If
        Else
            txtValorSLA.Focus()
            txtPorcentagem.Text = Nothing
        End If
    End Sub

    Protected Sub btAbrir_Click(sender As Object, e As EventArgs)
        'WS_Cadastro.Envia_Log(Session("Conn_Banco"),
        '                                              Session("Id_Usuario"),
        '                                              DateTime.Now,
        '                                                "Tela Ativo - Click btAbrir",
        '                                                False)
        pnlRegistro.Visible = True
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "disableScrolling();", True)
    End Sub

    Protected Sub btFechar_Registro_Click(sender As Object, e As EventArgs) Handles btFechar_Registro.Click
        'WS_Cadastro.Envia_Log(Session("Conn_Banco"),
        '                                              Session("Id_Usuario"),
        '                                              DateTime.Now,
        '                                                "Tela Ativo - Click btFechar_Registro",
        '                                                False)
        pnlRegistro.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
    End Sub
End Class
