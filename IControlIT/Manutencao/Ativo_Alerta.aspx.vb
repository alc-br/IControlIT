
Public Class Ativo_Alerta
    Inherits System.Web.UI.Page
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro

    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Alerta de Ativo ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            Session("DataSet") = Nothing

            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))

            '-----monta lista de alestas programados
            vdataset = WS_Modulo.Alerta_Sistema(Session("Conn_Banco"), Session("Id_Usuario"), "sp_Alerta_Programado")
            dtgAlertaProgramado.DataSource = vdataset
            dtgAlertaProgramado.DataBind()
            Call Master.Localizar(Nothing, Nothing)
            Call Master.Localizar("sp_Drop_Ativo_Tipo", Page.AppRelativeVirtualPath.ToString)

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
        End If
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As System.EventArgs) Handles btExecutar.Click
        Session("DataSet") = WS_Cadastro.Ativo(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, oConfig.ValidaCampo(cboConglomerado.SelectedValue), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sd_SL_Ativo_Alerta", True, Nothing, Nothing, Nothing, Nothing, Nothing)

        dtgLocaliza.CurrentPageIndex = 0
        dtgLocaliza.DataSource = Session("DataSet")
        dtgLocaliza.DataBind()

        '-----monta lista de alestas programados
        vdataset = WS_Modulo.Alerta_Sistema(Session("Conn_Banco"), Session("Id_Usuario"), "sp_Alerta_Programado")
        dtgAlertaProgramado.DataSource = vdataset
        dtgAlertaProgramado.DataBind()


        '-----registro ok
        lblProgramarAlerta.Visible = True
    End Sub

    Protected Sub btLocalizar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btLocalizar.Click
        Session("DataSet") = WS_Cadastro.Ativo(Session("Conn_Banco"), Nothing, txtLocalizar.Text, Nothing, Nothing, oConfig.ValidaCampo(cboConglomerado.SelectedValue), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sd_SL_Ativo_Alerta", True, Nothing, Nothing, Nothing, Nothing, Nothing)

        dtgLocaliza.CurrentPageIndex = 0
        dtgLocaliza.DataSource = Session("DataSet")
        dtgLocaliza.DataBind()

        '-----registro ok
        lblProgramarAlerta.Visible = True
    End Sub

    Protected Sub dtgLocaliza_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgLocaliza.PageIndexChanged
        dtgLocaliza.CurrentPageIndex = e.NewPageIndex
        dtgLocaliza.DataSource = Session("DataSet")
        dtgLocaliza.DataBind()
    End Sub

    Protected Sub btAlerta_Click1(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("btAlerta_") + 10, 8), System.Int32)

        Dim v_txtObservacao As TextBox
        Dim v_txtDataAlerta As TextBox
        v_txtObservacao = dtgLocaliza.Items(i).Cells(2).Controls(1)
        v_txtDataAlerta = dtgLocaliza.Items(i).Cells(3).Controls(1)

        If v_txtDataAlerta.Text = "" Then Exit Sub

        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim vtemp As Int16 = WS_Cadastro.Ativo(Session("Conn_Banco"),
                                                dtgLocaliza.Items(i).Cells(1).Text,
                                                dtgLocaliza.Items(i).Cells(0).Text,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                oConfig.ValidaCampo(v_txtDataAlerta.Text),
                                                oConfig.ValidaCampo(Mid(v_txtObservacao.Text, 1, 8000)),
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                Session("Id_Usuario"),
                                                "sd_SM_Ativo_Alerta",
                                                False, Nothing, Nothing, Nothing, Nothing, Nothing).Tables(0).Rows(0).Item(0)

        '-----lista base
        dtgLocaliza.DataSource = Nothing
        dtgLocaliza.DataBind()
        lblProgramarAlerta.Visible = False

        '-----monta lista de alestas programados
        vdataset = WS_Modulo.Alerta_Sistema(Session("Conn_Banco"), Session("Id_Usuario"), "sp_Alerta_Programado")
        dtgAlertaProgramado.DataSource = vdataset
        dtgAlertaProgramado.DataBind()
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
