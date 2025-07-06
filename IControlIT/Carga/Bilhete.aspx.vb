
Public Class Bilhete
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim vDataSet As New Data.DataSet
    Dim oConfig As New cls_Config

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Importação de Bilhete ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboArquivo)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            oConfig.CarregaCombo(cboArquivo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Arquivo_Carga", Nothing))
            oConfig.CarregaCombo(cboAtivoTipo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo", Nothing))
            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))

            txtDataLiberacao.Text = System.DateTime.Now.ToString
            hdfMenssagem.Value = 0
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboArquivo.SelectedValue = Nothing
        cboAtivoTipo.SelectedValue = Nothing
        cboConglomerado.SelectedValue = Nothing
        dtgCargaCompletada.DataSource = Nothing
        dtgCargaCompletada.DataBind()
        txtDataLiberacao.Text = System.DateTime.Now.ToString
        hdfMenssagem.Value = 0
        Page.SetFocus(cboArquivo)
    End Sub

    Protected Sub btCarga_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCarga.Click
        If Trim(txtDataLote.Text) = "" Then
            Exit Sub
            DivAtivo.Visible = False
        End If
        DivAtivo.Visible = True
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vDataSet = WS_Modulo.Carga_Bilhete(Session("Conn_Banco"),
                                            "sp_Carga_Completada",
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            oConfig.ValidaCampo(txtDataLote.Text),
                                            Nothing,
                                            Nothing,
                                            True)
        dtgCargaCompletada.DataSource = vDataSet
        dtgCargaCompletada.DataBind()
        Page.SetFocus("txtDataLiberacao")
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs) Handles btVoltar.Click
        pnlCarga.Visible = False
    End Sub

    Protected Sub BtContinuar_Click(sender As Object, e As EventArgs) Handles BtContinuar.Click
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Timeout = 3600000
        WS_Modulo.Carga_Bilhete(Session("Conn_Banco"),
                                    "sp_Carga_Bilhete",
                                    oConfig.ValidaCampo(cboArquivo.SelectedValue),
                                    oConfig.ValidaCampo(cboAtivoTipo.SelectedValue),
                                    oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                    oConfig.ValidaCampo(txtDataLote.Text),
                                    oConfig.ValidaCampo(txtDataLiberacao.Text),
                                    Nothing,
                                    False)
        Call limpar()
        pnlCarga.Visible = False
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As EventArgs)
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Timeout = 3600000
        vDataSet = WS_Modulo.Valida_Bilhete(Session("Conn_Banco"),
                                            "sp_Valida_Bilhete",
                                            oConfig.ValidaCampo(cboArquivo.SelectedValue),
                                            oConfig.ValidaCampo(cboAtivoTipo.SelectedValue),
                                            oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                            oConfig.ValidaCampo(txtDataLote.Text),
                                            oConfig.ValidaCampo(txtDataLiberacao.Text),
                                            Nothing,
                                            True)

        If vDataSet.Tables(0).Rows(0).Item("Validacao") = 0 And hdfMenssagem.Value = 0 Then
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Timeout = 3600000
            WS_Modulo.Carga_Bilhete(Session("Conn_Banco"),
                                    "sp_Carga_Bilhete",
                                    oConfig.ValidaCampo(cboArquivo.SelectedValue),
                                    oConfig.ValidaCampo(cboAtivoTipo.SelectedValue),
                                    oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                    oConfig.ValidaCampo(txtDataLote.Text),
                                    oConfig.ValidaCampo(txtDataLiberacao.Text),
                                    Nothing,
                                    False)
            Call limpar()
        Else
            hdfMenssagem.Value = 1
            '-----menssagem
            pnlCarga.Visible = True
            btExecutar.Visible = False
        End If
    End Sub
    Protected Sub btUpload_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
                                                    "window.open('../Carga/Upload.aspx" &
                                                    "','','resizable=yes, menubar=yes, scrollbars=no," &
                                                    "height=768px, width=1024px, top=10, left=10'" &
                                                    ")", True)
    End Sub

    Protected Sub tVoltarMenu_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
