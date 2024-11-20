
Public Class Contrato_SLA_Operacao
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Contrato As New WS_GUA_Contrato.WSContrato
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                        IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                        "Cadastro de S.L.A. ", _
                        vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboContrato)
            Page.Form.DefaultButton = BtSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Contrato_SLA_Operacao", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboContrato, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Contrato", Nothing))

            If Not Request("ID") = Nothing Then
                vdataset = WS_Contrato.Contrato_SLA_Operacao(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)
                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Contrato_SLA_Operacao")
                cboContrato.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Contrato")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Descricao")
                txtPrazoDias.Text = vdataset.Tables(0).Rows(0).Item("Prazo_Dias")
                txtValorSLA.Text = vdataset.Tables(0).Rows(0).Item("Vr_SLA_Operacao")
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboContrato.SelectedValue = Nothing
        btSalvar.Enabled = True
        Page.SetFocus(cboContrato)
    End Sub
    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Contrato.Contrato_SLA_Operacao(Session("Conn_Banco"),
                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                        oConfig.ValidaCampo(cboContrato.SelectedValue),
                                        oConfig.ValidaCampo(Mid(txtDescricao.Text, 1, 8000)),
                                        oConfig.ValidaCampo(txtPrazoDias.Text),
                                        oConfig.ValidaCampo(txtValorSLA.Text),
                                        Session("Id_Usuario"),
                                        "sp_SM",
                                        False)
        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Contrato.Contrato_SLA_Operacao(Session("Conn_Banco"),
                                                txtIdentificacao.Text,
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
End Class
