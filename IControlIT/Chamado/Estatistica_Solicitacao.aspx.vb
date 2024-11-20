Imports System.Web.UI.DataVisualization.Charting

Public Class Estatistica_Solicitacao
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim oConfig As New cls_Config
    Dim vdataset As System.Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                "Estatística do Suporte ", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboDataDe)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            '-----periodo dos incidentes
            vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"), _
                                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                                "sp_Consulta_Solicitacao_Periodo", _
                                                True)

            oConfig.CarregaCombo(cboDataDe, vdataset)
            oConfig.CarregaCombo(cboDataAte, vdataset)
        End If
    End Sub

    Public Sub Executar()
        vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"), _
                                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                            oConfig.ValidaCampo(cboDataDe.SelectedValue), _
                                            oConfig.ValidaCampo(cboDataAte.SelectedValue), _
                                            Nothing, _
                                            Nothing, _
                                            Session("Id_Usuario"), _
                                            "sp_Consulta_Solicitacao_Status", _
                                            True)

        If vdataset.Tables(0).Rows(0).Item("Incidente") > 0 Then
            txtIncidentePeriodo.Text = vdataset.Tables(0).Rows(0).Item("Incidente")
            txtAberto.Text = vdataset.Tables(0).Rows(0).Item("Aberto")
            txtAbertoDSLA.Text = vdataset.Tables(0).Rows(0).Item("Aberto_FSLA")
            txtAberturaFSLA.Text = vdataset.Tables(0).Rows(0).Item("Aberto_DSLA")
            txtEncerrado.Text = vdataset.Tables(0).Rows(0).Item("Encerrado")
            txtEncerradoDSLA.Text = vdataset.Tables(0).Rows(0).Item("Encerrado_FSLA")
            txtEncerradoFSLA.Text = vdataset.Tables(0).Rows(0).Item("Encerrado_DSLA")

            '------lista grid dos incidentes abertos
            DivAtivo.Visible = True
            lblTitulo.Visible = True
            lblTitulo.Text = "Incidente Abertos"

            vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"),
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        1,
                                        Nothing, Nothing, Nothing, Nothing, Nothing,
                                        Session("Id_Usuario"),
                                        "sp_Consulta_Solicitacao",
                                        True)
            dtgSolicitacao.DataSource = vdataset
            dtgSolicitacao.DataBind()

        Else
            '-----fila de atendimento
            Call Master.Registro_Salvo("Você não está cadastrado em nenhuma fila de atendimento.")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
        End If
    End Sub

    Protected Sub btAberto_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btAberto.Click
        DivAtivo.Visible = True
        lblTitulo.Visible = True
        lblTitulo.Text = "Incidente Abertos"

        vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"), _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    1, _
                                    Nothing, Nothing, Nothing, Nothing, Nothing, _
                                    Session("Id_Usuario"), _
                                    "sp_Consulta_Solicitacao", _
                                    True)
        dtgSolicitacao.DataSource = vdataset
        dtgSolicitacao.DataBind()
    End Sub

    Protected Sub btEncerrado_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btEncerrado.Click
        DivAtivo.Visible = True
        lblTitulo.Visible = True
        lblTitulo.Text = "Incidente Encerados"

        vdataset = WS_Modulo.Solicitacao(Session("Conn_Banco"), _
                            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _
                            2, _
                            Nothing, Nothing, Nothing, Nothing, Nothing, _
                            Session("Id_Usuario"), _
                            "sp_Consulta_Solicitacao", _
                            True)
        dtgSolicitacao.DataSource = vdataset
        dtgSolicitacao.DataBind()
    End Sub

    Protected Sub dtgSolicitacao_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dtgSolicitacao.SelectedIndexChanged
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Chamado/Solicitacao.aspx" & "?ID=" & dtgSolicitacao.SelectedItem.Cells(0).Text & "','','resizable=yes, menubar=yes, scrollbars=no, height=768px, width=1024px, top=10, left=10');", True)
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As EventArgs) Handles btExecutar.Click
        Call Executar()
        pnlMsg.Visible = False
    End Sub

    Protected Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        pnlMsg.Visible = False
    End Sub

    Protected Sub btAbrir_Click(sender As Object, e As EventArgs)
        pnlMsg.Visible = True
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class

