
Public Class Contrato
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
                "Capa do Contrato ", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtNumeroContrato)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Contrato_Corpo", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboStatus, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Contrato_Status", Nothing))
            oConfig.CarregaCombo(cboServico, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Servico", Nothing))
            oConfig.CarregaCombo(cboFilial, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Filial", Nothing))
            oConfig.CarregaCombo(cboIndice, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Contrato_Indice", Nothing))
            oConfig.CarregaCombo(cboEmpresaContratada, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Empresa_Contratada", Nothing))

            If Not Request("ID") = Nothing Then
                vdataset = WS_Contrato.Contrato(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing,
                                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                                    Nothing, Nothing, "sp_SL_ID", True)
                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Contrato")
                txtNumeroContrato.Text = vdataset.Tables(0).Rows(0).Item("Nr_Contrato")
                txtDescricao.Text = vdataset.Tables(0).Rows(0).Item("Descricao")
                txtDataInicioVigencia.Text = vdataset.Tables(0).Rows(0).Item("Dt_Inicio_Vigencia")
                txtDataFimVigencia.Text = vdataset.Tables(0).Rows(0).Item("Dt_Fim_Vigencia")
                txtObjeto.Text = vdataset.Tables(0).Rows(0).Item("Objeto")
                cboStatus.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Contrato_Status")
                cboServico.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Servico")
                cboFilial.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Filial")
                cboIndice.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Contrato_Indice")
                cboEmpresaContratada.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Empresa_Contratada")
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboStatus.SelectedValue = Nothing
        cboServico.SelectedValue = Nothing
        cboFilial.SelectedValue = Nothing
        cboEmpresaContratada.SelectedValue = Nothing
        btSalvar.Enabled = True
        Page.SetFocus(txtNumeroContrato)
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)

        WS_Contrato.Contrato(Session("Conn_Banco"),
                                    oConfig.ValidaCampo(txtIdentificacao.Text),
                                    oConfig.ValidaCampo(txtNumeroContrato.Text),
                                    oConfig.ValidaCampo(Mid(txtDescricao.Text, 1, 100)),
                                    oConfig.ValidaCampo(cboStatus.SelectedValue),
                                    oConfig.ValidaCampo(cboServico.SelectedValue),
                                    oConfig.ValidaCampo(txtDataInicioVigencia.Text),
                                    IIf(Trim(txtDataFimVigencia.Text) = "", Nothing, txtDataFimVigencia.Text),
                                    oConfig.ValidaCampo(cboFilial.SelectedValue),
                                    oConfig.ValidaCampo(cboEmpresaContratada.SelectedValue),
                                    oConfig.ValidaCampo(Mid(txtObjeto.Text, 1, 8000)),
                                    oConfig.ValidaCampo(cboIndice.SelectedValue),
                                    Session("Id_Usuario"),
                                    "sp_SM",
                                    False)
        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub
        WS_Contrato.Contrato(Session("Conn_Banco"),
                                    txtIdentificacao.Text,
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
                                    Session("Id_Usuario"),
                                    "sp_SE",
                                    False)
        Call limpar()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
