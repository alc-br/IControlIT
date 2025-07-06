
Public Class Rateio_Custo_Fixo
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Rateio As New WS_GUA_Rateio.WSRateio
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim oConfig As New cls_Config

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
                "Rateio de Custo Fixo ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboDataLote)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            oConfig.CarregaCombo(cboFaturaParametro, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Fatura_Parametro", Nothing))
            '-----gera data lote
            Dim vDataSet As New Data.DataSet
            vDataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            oConfig.CarregaCombo(cboDataLote, vDataSet)

            Session("DataSet") = Nothing
        End If
    End Sub

    Public Sub limpar()
        Page.SetFocus(cboDataLote)
        cboDataLote.Enabled = True
        cboFaturaParametro.Enabled = True
        cboDataLote.SelectedValue = Nothing
        cboFaturaParametro.SelectedValue = Nothing
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Rateio.Rateio_Custo_Fixo(Session("Conn_Banco"),
                            oConfig.ValidaCampo(cboDataLote.SelectedValue),
                            oConfig.ValidaCampo(cboFaturaParametro.SelectedValue),
                            "sp_Rateio_Custo_Fixo",
                            False)

        cboDataLote.Enabled = False
        cboFaturaParametro.Enabled = False
        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class

