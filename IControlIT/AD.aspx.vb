Public Class AD
    Inherits System.Web.UI.Page

    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim vdataSet As Data.DataSet
    Dim vEmpresa_Redirect As System.String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
    End Sub

    Protected Sub btAcordo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btAcordo.Click
        '-----atualiza status do usuario
        WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Satus_Acordo", Session("Nm_Usuario"), Nothing, Nothing, Nothing, Nothing)
        '-----acessa sistema
        Response.Redirect("http://webapp.IControlIT.com.br?redirect=" & vdataSet.Tables(0).Rows(0).Item("Usuario_Autenticado"))
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----verifica e valida conexao
        Session("Conn_Banco") = Nothing
        Session("Nm_Usuario") = Nothing

        '-----realisa chamada do banco (IControlIT)
        vEmpresa_Redirect = ConfigurationManager.AppSettings("Empresa_Redirect")
        vdataSet = WS_Modulo.Conn_Banco()
        Dim i As System.Int32 = 0
        For i = 0 To vdataSet.Tables(0).Rows.Count - 1
            If vdataSet.Tables(0).Rows(i).Item(1) = "ATIVVUS_LOGIN" Then
                Session("Conn_Banco") = vdataSet.Tables(0).Rows(i).Item(0)
            End If
        Next i

        '-----validacao pelo AD (local)
        If Not Request.ServerVariables("LOGON_USER").Substring(Request.ServerVariables("LOGON_USER").LastIndexOf("\") + 1) = "" Then

            vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                                        "Sd_Valida_Usuario_AD_Local",
                                                        Request.ServerVariables("LOGON_USER").Substring(Request.ServerVariables("LOGON_USER").LastIndexOf("\") + 1),
                                                        vEmpresa_Redirect,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing)

            If vdataSet.Tables(0).Rows.Count > 0 Then
                Dim Linha As System.Data.DataRow
                Session("Empresa") = vdataSet.Tables(0).Rows(0).Item("Empresa")
                Session("Nm_Usuario") = vdataSet.Tables(0).Rows(0).Item("Nm_Usuario")

                For Each Linha In WS_Modulo.Monta_Texto_Termo(Session("Conn_Banco"), "sp_SL_ID", Session("Empresa")).Tables(0).Rows
                    If Linha.Item("Caixa") = "lblTexto1" Then lblTexto1.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto2" Then lblTexto2.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto3" Then lblTexto3.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto4" Then lblTexto4.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto5" Then lblTexto5.Text = Linha.Item("Texto")
                Next

                '-----carrega banco da empresa validada
                vdataSet = WS_Modulo.Conn_Banco()
                For i = 0 To vdataSet.Tables(0).Rows.Count - 1
                    If vdataSet.Tables(0).Rows(i).Item(1) = vEmpresa_Redirect Then
                        Session("Conn_Banco") = vdataSet.Tables(0).Rows(i).Item(0)
                    End If
                Next i

                If vdataSet.Tables(0).Rows(0).Item("Fl_Desativado") = 3 Then
                    lblTextoNome.Text = "Ola, " + vdataSet.Tables(0).Rows(0).Item("Nm_Consumidor") + " bem vindo ao portal IControlIT."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalSubTermo');", True)

                    vdataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Linha_De_Acordo", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)

                    dtgAtivo.DataSource = vdataSet
                    dtgAtivo.DataBind()
                Else
                    Response.Redirect("http://webapp.IControlIT.com.br?redirect=" & vdataSet.Tables(0).Rows(0).Item("Usuario_Autenticado"))
                    'Response.Redirect("~/default.aspx?redirect=" & vdataSet.Tables(0).Rows(0).Item("Usuario_Autenticado"))
                End If
            End If
        End If
    End Sub
End Class
