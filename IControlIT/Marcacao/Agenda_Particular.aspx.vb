Public Class Agenda_Particular
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vDataSet As System.Data.DataSet
    Dim vDataView As Data.DataView
    Dim v_optTipo As CheckBox
    Dim v_objTable As HtmlTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Timeout = 3600000

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                            IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                            "Agenda de Contatos ",
                            vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----home
            Call Master.home("usuario")

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Call Master.Localizar(Nothing, Nothing)

            '-----monta agenda verificando se exite novo numero discado com base na carga
            WS_Cadastro.Agenda_Marcacao_Particular(Session("Conn_Banco"),
                                                        Session("Id_Usuario"),
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        "sp_Cria_Agenda",
                                                        False)

            '-----voltar
            If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Else
                Call Master.Voltar(Nothing, "~/CockPit_Menu.aspx")
            End If

            Page.Form.DefaultButton = btSalvar.UniqueID

            '-----monta grid com numero discado
            vDataSet = WS_Cadastro.Agenda_Marcacao_Particular(Session("Conn_Banco"), Session("Id_Usuario"), Nothing, Nothing, Nothing, "sp_SL_ID_Discado_Page_Load", True)
            Session("DataSet") = vDataSet
            dtgDiscado.DataSource = Session("DataSet")
            dtgDiscado.DataBind()

            Call ValidaCheckbox()

        End If
    End Sub

    Protected Sub ValidaCheckbox()

        For i = 0 To dtgDiscado.Items.Count - 1
            v_objTable = dtgDiscado.Items(i).Cells(0).Controls(9)
            v_optTipo = dtgDiscado.Items(i).Cells(0).Controls(9).Controls(0).Controls(0).Controls(1)

            If v_optTipo.Checked = True Then
                v_objTable.Style.Add("Background-position", "-22px 3px")
            Else
                v_objTable.Style.Add("Background-position", "0px 3px")
            End If
        Next

    End Sub

    Protected Sub btLocalizar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btLocalizar.Click
        dtgDiscado.CurrentPageIndex = 0

        '-----localiza discado
        vDataSet = WS_Cadastro.Agenda_Marcacao_Particular(Session("Conn_Banco"), Session("Id_Usuario"), Nothing, txtLocalizar.Text, Nothing, "sp_SL_ID_Discado", True)

        dtgDiscado.DataSource = vDataSet
        dtgDiscado.DataBind()

        Call ValidaCheckbox()
    End Sub

    Protected Sub dtgDiscado_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgDiscado.PageIndexChanged
        dtgDiscado.CurrentPageIndex = e.NewPageIndex
        dtgDiscado.DataSource = Session("DataSet")
        dtgDiscado.DataBind()

        Call ValidaCheckbox()

    End Sub

    Protected Sub chkParticularDiscado_CheckedChanged(sender As Object, e As EventArgs)
        v_optTipo = sender
        Call ValidaCheckbox()
    End Sub
    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim v_txtDescricao As TextBox
        Dim v_txtNumero As Label

        '-----grava discados
        If dtgDiscado.Items.Count > 0 Then
            For i = 0 To dtgDiscado.Items.Count - 1
                v_txtNumero = dtgDiscado.Items(i).Cells(0).Controls(7)
                v_txtDescricao = dtgDiscado.Items(i).Cells(0).Controls(3)
                v_optTipo = dtgDiscado.Items(i).Cells(0).Controls(9).Controls(0).Controls(0).Controls(1)

                WS_Cadastro.Agenda_Marcacao_Particular(Session("Conn_Banco"),
                                                            Session("Id_Usuario"),
                                                            oConfig.ValidaCampo(v_txtNumero.Text),
                                                            oConfig.ValidaCampo(v_txtDescricao.Text),
                                                            IIf(v_optTipo.Checked = False, 2, 1),
                                                            "sp_SM",
                                                            False)
            Next i
        End If

        Session("DataSet") = Nothing
        '-----monta grid com numero discado
        vDataSet = WS_Cadastro.Agenda_Marcacao_Particular(Session("Conn_Banco"), Session("Id_Usuario"), Nothing, Nothing, Nothing, "sp_SL_ID_Discado_Page_Load", True)
        dtgDiscado.DataSource = vDataSet
        dtgDiscado.DataBind()

        Call ValidaCheckbox()

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class

