
Public Class Exportacao_RH
    Inherits System.Web.UI.Page
    Dim WS_Manutencao As New WS_GUA_Manutencao.WSManutencao
    Dim oConfig As New cls_Config
    Dim vDataSet As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Exportação do Arquivo de Desconto de Usuário ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Call Master.Localizar(Nothing, Nothing)

            Page.SetFocus(txtScript)
            Page.Form.DefaultButton = btExecutar.UniqueID

            '-----monstra dado
            WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
            txtScript.Text = WS_Manutencao.Script_Exportacao(Session("Conn_Banco"), Nothing, Nothing, Session("Id_Usuario"), "sp_SL_ID", True).Tables(0).Rows(0).Item("Script")
            Session("DataSet") = Nothing
            oConfig.CarregaList(lstViewArquivoConsulta, WS_Manutencao.Script_Exportacao(Session("Conn_Banco"), Nothing, Nothing, Session("Id_Usuario"), "sp_Data_Export", True))
        End If
    End Sub

    Protected Sub btFechaConfig_Click(sender As Object, e As EventArgs) Handles btFechaConfig.Click
        pnlConfiguracao.Visible = False
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        If Trim(txtScript.Text) = "" Then Exit Sub
        WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Manutencao.Script_Exportacao(Session("Conn_Banco"),
                                            oConfig.ValidaCampo(txtScript.Text),
                                            Nothing,
                                            Session("Id_Usuario"),
                                            "sp_SM",
                                            False)

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btConfiguracao_Click(sender As Object, e As EventArgs)
        pnlConfiguracao.Visible = True
    End Sub

    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim vPakage As System.String = Nothing

        If lstViewArquivoConsulta.SelectedValue = Nothing Then
            If optOpcao.SelectedValue = 2 Then
                vPakage = "sp_Arquivo_Exporta_Politica"
            End If
            If Not vPakage = "sp_Arquivo_Exporta_Politica" Then vPakage = "sp_Arquivo_Export"
            Session("DataSet") = WS_Manutencao.Script_Exportacao(Session("Conn_Banco"),
                                                                        Nothing,
                                                                        Nothing,
                                                                        Session("Id_Usuario"),
                                                                        vPakage,
                                                                        True)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
                                 "window.open('../Exportacao/Exporta.aspx?Descricao=" & "Exporta_RH" & "&Tipo=2" & "&Campo=Descricao" &
                                 "','','resizable=yes, menubar=yes, scrollbars=no," &
                                 "height=768px, width=1024px, top=10, left=10'" &
                                 ")", True)

            '-----monstra dado
            txtScript.Text = WS_Manutencao.Script_Exportacao(Session("Conn_Banco"), Nothing, Nothing, Session("Id_Usuario"), "sp_SL_ID", True).Tables(0).Rows(0).Item("Script")
            oConfig.CarregaList(lstViewArquivoConsulta, WS_Manutencao.Script_Exportacao(Session("Conn_Banco"), Nothing, Nothing, Session("Id_Usuario"), "sp_Data_Export", True))
        Else
            WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials

            If optOpcao.SelectedValue = 2 Then
                vPakage = "sp_Arquivo_Wiew_Politica"
            End If
            If Not vPakage = "sp_Arquivo_Wiew_Politica" Then vPakage = "sp_Arquivo_View"

            Session("DataSet") = WS_Manutencao.Script_Exportacao(Session("Conn_Banco"),
                                                                        Nothing,
                                                                        lstViewArquivoConsulta.SelectedItem.Text,
                                                                        Session("Id_Usuario"),
                                                                        vPakage,
                                                                        True)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
                             "window.open('../Exportacao/Exporta.aspx?Descricao=" & "Exporta_RH" & "&Tipo=2" & "&Campo=Descricao" &
                             "','','resizable=yes, menubar=yes, scrollbars=no," &
                             "height=768px, width=1024px, top=10, left=10'" &
                             ")", True)
        End If
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As EventArgs)
        WS_Manutencao.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim vPakage As String
        If optOpcao.SelectedValue = 2 Then
            vPakage = "sp_Arquivo_Wiew_Politica"
        Else
            vPakage = "sp_Arquivo_View"
        End If

        If lstViewArquivoConsulta.SelectedValue = Nothing Then
            vDataSet = WS_Manutencao.Script_Exportacao(Session("Conn_Banco"),
                                                            Nothing,
                                                            Nothing,
                                                            Session("Id_Usuario"),
                                                            vPakage,
                                                            True)
        Else
            vDataSet = WS_Manutencao.Script_Exportacao(Session("Conn_Banco"),
                                                            Nothing,
                                                            IIf(lstViewArquivoConsulta.SelectedValue = "", Nothing, lstViewArquivoConsulta.SelectedItem.Text),
                                                            Session("Id_Usuario"),
                                                            vPakage,
                                                            True)

        End If

        dtgArquivo.DataSource = vDataSet
        dtgArquivo.DataBind()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
