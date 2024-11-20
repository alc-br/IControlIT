
Public Class Bilhete_Manual
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim oConfig As New cls_Config
    Dim vDataSet As New System.Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Inserir Bilhete Manualmente ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtAtivo)
            Page.Form.DefaultButton = BtSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Bilhete", Page.AppRelativeVirtualPath.ToString)

            hdfId_Bilhete.Value = Nothing
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
            oConfig.CarregaCombo(cboBilheteTipo, WS_Modulo.Bilhete_Manual(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", Nothing, True))

            If Not Request("ID") = Nothing Then
                vDataSet = WS_Modulo.Bilhete_Manual(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Request("ID"), "sp_Lista_Bilhete", Nothing, True)

                txtAtivo.Text = vDataSet.Tables(0).Rows(0).Item("Nr_Ativo")
                txtValorFatura.Text = vDataSet.Tables(0).Rows(0).Item("DB_Custo")
                txtDataLote.Text = vDataSet.Tables(0).Rows(0).Item("Dt_Lote")
                txtData.Text = vDataSet.Tables(0).Rows(0).Item("Data")
                txtNotaFiscal.Text = vDataSet.Tables(0).Rows(0).Item("DC_Nr_Nota_Fiscal")
                cboBilheteTipo.SelectedValue = vDataSet.Tables(0).Rows(0).Item("Id_Bilhete_Tipo")
                hdfId_Bilhete.Value = vDataSet.Tables(0).Rows(0).Item("Id_Bilhete")
                txtAtivo.ReadOnly = True
                txtDataLote.ReadOnly = True
                txtData.ReadOnly = True
            End If
        End If
    End Sub

    Public Sub Limpar()
        '-----limpa
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        Page.SetFocus(txtAtivo)
        cboBilheteTipo.SelectedValue = Nothing
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        hdfId_Bilhete.Value = Nothing
        txtAtivo.ReadOnly = False
        txtDataLote.ReadOnly = False
        txtData.ReadOnly = False
        Page.SetFocus(txtAtivo)
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call Limpar()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

        If hdfId_Bilhete.Value = Nothing Then
            vDataSet = WS_Modulo.Bilhete_Manual(Session("Conn_Banco"),
                                                    oConfig.ValidaCampo(txtAtivo.Text),
                                                    oConfig.ValidaCampo(txtValorFatura.Text),
                                                    oConfig.ValidaCampo(txtDataLote.Text),
                                                    oConfig.ValidaCampo(txtData.Text),
                                                    oConfig.ValidaCampo(txtNotaFiscal.Text),
                                                    oConfig.ValidaCampo(cboBilheteTipo.SelectedValue),
                                                    Nothing,
                                                    "sp_SM",
                                                    Session("Id_Usuario"),
                                                    True)

            If vDataSet.Tables(0).Rows(0).Item("Status_Carga") = "Validacao OK" Then
                Master.Registro_Salvo("Registro salvo com sucesso !")
            Else
                Master.Registro_Salvo(vDataSet.Tables(0).Rows(0).Item("Status_Carga"))
            End If
        Else
            vDataSet = WS_Modulo.Bilhete_Manual(Session("Conn_Banco"),
                                                Nothing,
                                                oConfig.ValidaCampo(txtValorFatura.Text),
                                                Nothing,
                                                Nothing,
                                                oConfig.ValidaCampo(txtNotaFiscal.Text),
                                                oConfig.ValidaCampo(cboBilheteTipo.SelectedValue),
                                                oConfig.ValidaCampo(hdfId_Bilhete.Value),
                                                "sp_SM_Alteracao",
                                                Session("Id_Usuario"),
                                                True)
        End If

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Not hdfId_Bilhete.Value = Nothing Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            vDataSet = WS_Modulo.Bilhete_Manual(Session("Conn_Banco"),
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                oConfig.ValidaCampo(hdfId_Bilhete.Value),
                                                "sp_Deletar_Bilhete",
                                                Session("Id_Usuario"),
                                                True)

            Master.Registro_Salvo("Registro Deletado.")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)

            Call Limpar()
        End If
    End Sub

    Protected Sub btConfiguracao_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Carga/Tipo_Bilhete_Manual.aspx")
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
