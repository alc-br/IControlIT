
Public Class Template_Consulta
    Inherits System.Web.UI.Page
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim oConfig As New cls_Config

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)

            Session("DataSet") = Nothing
            Session("DataSet_1") = Nothing

            If Session("KPI") = Nothing Then Session("KPI") = "Telefonia_Movel"

            '-----busca relatorio
            Dim vDataSet As Data.DataSet
            vDataSet = WS_Modulo.Validacao_Relatorio(Session("Conn_Banco"), "sp_SL_ID", Request("ID"), Nothing, Nothing)

            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Relatório - " & vDataSet.Tables(0).Rows(0).Item("Nm_Template"),
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----configura tela
            Grid1.Descricao = vDataSet.Tables(0).Rows(0).Item("Nm_Template")

            '-----trata o botao de home e voltar
            If Not Session("Id_Usuario_Perfil_Acesso") = 1 Then
                '-----usuario gestor
                If Request("Visualiza") = "0" Then
                    Call Master.Voltar("", "~/CockPit_Menu.aspx?Postback=1")
                Else
                    Call Master.Voltar("", "~/Marcacao/lote.aspx?Postback=1")
                End If
                Call Master.home("usuario")
            Else
                '-----usuario administrador
                Call Master.Voltar("", "~/Marcacao/lote.aspx?Postback=1")
            End If

            '----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(cboDataDe)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            '-----valida data lote
            Dim vValida_Filtro As System.Int32 = vDataSet.Tables(0).Rows(0).Item("Dt_Lote")
            hdvTipo_Grafico.Value = vDataSet.Tables(0).Rows(0).Item("Tipo_Grafico")

            '-----desabilita data
            If vValida_Filtro = 1 Then
                lnDe.Visible = False
                cboDataDe.Visible = False
                lnAte.Visible = False
                cboDataAte.Visible = False
            End If

            '-----libera todos
            If vValida_Filtro = 2 Then
                vDataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                oConfig.CarregaCombo(cboDataDe, vDataSet)
                oConfig.CarregaCombo(cboDataAte, vDataSet)

                cboDataDe.SelectedIndex = cboDataDe.Items.Count - 1
                cboDataAte.SelectedIndex = cboDataAte.Items.Count - 1
            End If

            '-----desabilita filtro
            If vValida_Filtro = 3 Then
                lnFiltro.Visible = False
                Filtro_Acesso.Visible = False
                vDataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                oConfig.CarregaCombo(cboDataDe, vDataSet)
                oConfig.CarregaCombo(cboDataAte, vDataSet)

                cboDataDe.SelectedIndex = cboDataDe.Items.Count - 1
                cboDataAte.SelectedIndex = cboDataAte.Items.Count - 1
            End If

            '-----desabilita todos
            If vValida_Filtro = 4 Then
                lnDe.Visible = False
                cboDataDe.Visible = False
                lnAte.Visible = False
                cboDataAte.Visible = False
                lnFiltro.Visible = False
                Filtro_Acesso.Visible = False
            End If

            '-----data (de) e filtro
            If vValida_Filtro = 5 Then
                vDataSet = WS_Consulta.Lote(Session("Conn_Banco"), "sp_Data_Lote", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                oConfig.CarregaCombo(cboDataDe, vDataSet)

                lnDe.Visible = True
                cboDataDe.Visible = True
                lnAte.Visible = False
                cboDataAte.Visible = False
                lnFiltro.Visible = False
                Filtro_Acesso.Visible = False
            End If
        End If

        ' Verifica se há parâmetros na URL
        If Request.QueryString.HasKeys() AndAlso Request.QueryString("Id_Rateio") IsNot Nothing Then
            ' Obtém o valor do parâmetro 'parametro' passado via GET na URL
            Dim valorParametro As String = Request.QueryString("Id_Rateio")

            Call executarPorIdRateio()

        End If

    End Sub

    Protected Sub btExecutar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExecutar.Click
        lblDescricaoArquivo.Text = ""
        Call executar()
        If cboDataDe.Visible = True Then
            If cboDataAte.Visible = False Then lblDescricaoArquivo.Text = "Relatório | Lote de: " & cboDataDe.SelectedItem.Text
            If cboDataAte.Visible = True Then lblDescricaoArquivo.Text = "Relatório | Lote de: " & cboDataDe.SelectedItem.Text & "Lote até: " & cboDataAte.SelectedItem.Text
        End If
    End Sub

    Protected Sub cboDataDe_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDataDe.SelectedIndexChanged
        btExecutar.Enabled = True
    End Sub

    Protected Sub cboDataAte_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDataAte.SelectedIndexChanged
        btExecutar.Enabled = True
    End Sub

    Public Sub executarPorIdRateio()
        Dim vDataSet As New Data.DataSet
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Timeout = 3600000
        WS_Consulta.Timeout = 3600000

        'cboDataDe.Text = DateTime.Now().ToString("yyyyMM")
        If cboDataDe.Items.Count > 0 Then
            ' Seleciona o último item
            cboDataDe.SelectedIndex = cboDataDe.Items.Count - 1
        End If


        'btExecutar.Enabled = False
        pnlMenssagem.Visible = False

        '-----busca dados de configuracao do template
        Dim vProcedure As System.String = Nothing
        Dim vConsulta As System.String = Nothing
        Dim vExportacao As System.String = Nothing
        Dim vGrafico As System.String = Nothing
        Dim vCampo As System.String = Nothing
        Dim vIdRateio As System.String = Nothing

        vDataSet = WS_Modulo.Validacao_Relatorio(Session("Conn_Banco"), "sp_SL_ID", Request("ID"), Nothing, Nothing)
        vProcedure = vDataSet.Tables(0).Rows(0).Item("Nm_Procedure")
        vConsulta = "sp_Rateio_ById" 'vDataSet.Tables(0).Rows(0).Item("Nm_Pakage_Consulta")
        vExportacao = "sp_Rateio_Export_ById" 'vDataSet.Tables(0).Rows(0).Item("Nm_Pakage_Export")
        vGrafico = "sp_Rateio_ById" 'vDataSet.Tables(0).Rows(0).Item("Nm_Pakage_Grafico")
        vIdRateio = Request.QueryString("Id_Rateio").ToString()


        '-----monta grid
        Session("DataSet_1") = WS_Consulta.Template(Session("Conn_Banco"),
                                                        vProcedure,
                                                        vConsulta,
                                                        Filtro_Acesso.pPakage,
                                                        vIdRateio, 'Filtro_Acesso.pParametro1,
                                                        Filtro_Acesso.pParametro_Filial,
                                                        Filtro_Acesso.pParametro_Usuario,
                                                        Filtro_Acesso.pParametro_Centro_Custo,
                                                        Filtro_Acesso.pParametro_Departamento,
                                                        Filtro_Acesso.pParametro_Setor,
                                                        oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                        oConfig.ValidaCampo(cboDataAte.SelectedValue),
                                                        Session("KPI"))

        If vDataSet.Tables.Count > 0 Then
            vDataSet = Session("DataSet_1")
            Dim i As System.Int32

            Dim header(vDataSet.Tables(0).Columns.Count - 1) As String
            Dim field(vDataSet.Tables(0).Columns.Count - 1) As String
            Dim sort(vDataSet.Tables(0).Columns.Count - 1) As String

            For i = 0 To vDataSet.Tables(0).Columns.Count - 1
                header(i) = vDataSet.Tables(0).Columns(i).ColumnName
                field(i) = vDataSet.Tables(0).Columns(i).ColumnName
                sort(i) = vDataSet.Tables(0).Columns(i).ColumnName
            Next

            Call Grid1.Colunas(header, field, sort)
            Call Grid1.Dados(vDataSet)

            conteudo.Visible = True
        End If

        '-----carrega dados para exportacao
        If vDataSet.Tables.Count > 0 Then
            '-----monta exportacao
            Session("DataSet") = WS_Consulta.Template(Session("Conn_Banco"),
                                                        vProcedure,
                                                        vExportacao,
                                                        Filtro_Acesso.pPakage,
                                                        vIdRateio,'Filtro_Acesso.pParametro1,
                                                        Filtro_Acesso.pParametro_Filial,
                                                        Filtro_Acesso.pParametro_Usuario,
                                                        Filtro_Acesso.pParametro_Centro_Custo,
                                                        Filtro_Acesso.pParametro_Departamento,
                                                        Filtro_Acesso.pParametro_Setor,
                                                        oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                        oConfig.ValidaCampo(cboDataAte.SelectedValue),
                                                        Session("KPI"))

            vDataSet = Session("DataSet")

            Dim i As System.Int32
            For i = 0 To vDataSet.Tables(0).Columns.Count - 1
                vCampo = vCampo & vDataSet.Tables(0).Columns(i).ColumnName & ";"
            Next
            Grid1.Campo = Mid(vCampo, 1, Len(vCampo) - 1)

            '-----monta grafico (verifica se grafico esta ativo)
            If Not vGrafico = Nothing Then
                vDataSet = WS_Consulta.Template(Session("Conn_Banco"),
                                                    vProcedure,
                                                    vGrafico,
                                                    Filtro_Acesso.pPakage,
                                                    vIdRateio, 'Filtro_Acesso.pParametro1,
                                                    Filtro_Acesso.pParametro_Filial,
                                                    Filtro_Acesso.pParametro_Usuario,
                                                    Filtro_Acesso.pParametro_Centro_Custo,
                                                    Filtro_Acesso.pParametro_Departamento,
                                                    Filtro_Acesso.pParametro_Setor,
                                                    oConfig.ValidaCampo(cboDataDe.SelectedValue),
                                                    oConfig.ValidaCampo(cboDataAte.SelectedValue),
                                                    Session("KPI"))
            End If
        End If
    End Sub

    Public Sub executar()
        Dim vDataSet As New Data.DataSet
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Timeout = 3600000
        WS_Consulta.Timeout = 3600000

        'btExecutar.Enabled = False
        pnlMenssagem.Visible = False

        '-----busca dados de configuracao do template
        Dim vProcedure As System.String = Nothing
        Dim vConsulta As System.String = Nothing
        Dim vExportacao As System.String = Nothing
        Dim vGrafico As System.String = Nothing
        Dim vCampo As System.String = Nothing

        vDataSet = WS_Modulo.Validacao_Relatorio(Session("Conn_Banco"), "sp_SL_ID", Request("ID"), Nothing, Nothing)
        vProcedure = vDataSet.Tables(0).Rows(0).Item("Nm_Procedure")
        vConsulta = vDataSet.Tables(0).Rows(0).Item("Nm_Pakage_Consulta")
        vExportacao = vDataSet.Tables(0).Rows(0).Item("Nm_Pakage_Export")
        vGrafico = vDataSet.Tables(0).Rows(0).Item("Nm_Pakage_Grafico")

        '-----monta grid
        Session("DataSet_1") = WS_Consulta.Template(Session("Conn_Banco"), _
                                                        vProcedure, _
                                                        vConsulta, _
                                                        Filtro_Acesso.pPakage, _
                                                        Filtro_Acesso.pParametro1, _
                                                        Filtro_Acesso.pParametro_Filial, _
                                                        Filtro_Acesso.pParametro_Usuario, _
                                                        Filtro_Acesso.pParametro_Centro_Custo, _
                                                        Filtro_Acesso.pParametro_Departamento, _
                                                        Filtro_Acesso.pParametro_Setor, _
                                                        oConfig.ValidaCampo(cboDataDe.SelectedValue), _
                                                        oConfig.ValidaCampo(cboDataAte.SelectedValue), _
                                                        Session("KPI"))

        If vDataSet.Tables.Count > 0 Then
            vDataSet = Session("DataSet_1")
            Dim i As System.Int32

            Dim header(vDataSet.Tables(0).Columns.Count - 1) As String
            Dim field(vDataSet.Tables(0).Columns.Count - 1) As String
            Dim sort(vDataSet.Tables(0).Columns.Count - 1) As String

            For i = 0 To vDataSet.Tables(0).Columns.Count - 1
                header(i) = vDataSet.Tables(0).Columns(i).ColumnName
                field(i) = vDataSet.Tables(0).Columns(i).ColumnName
                sort(i) = vDataSet.Tables(0).Columns(i).ColumnName
            Next

            Call Grid1.Colunas(header, field, sort)
            Call Grid1.Dados(vDataSet)

            conteudo.Visible = True
        End If

        '-----carrega dados para exportacao
        If vDataSet.Tables.Count > 0 Then
            '-----monta exportacao
            Session("DataSet") = WS_Consulta.Template(Session("Conn_Banco"), _
                                                        vProcedure, _
                                                        vExportacao, _
                                                        Filtro_Acesso.pPakage, _
                                                        Filtro_Acesso.pParametro1, _
                                                        Filtro_Acesso.pParametro_Filial, _
                                                        Filtro_Acesso.pParametro_Usuario, _
                                                        Filtro_Acesso.pParametro_Centro_Custo, _
                                                        Filtro_Acesso.pParametro_Departamento, _
                                                        Filtro_Acesso.pParametro_Setor, _
                                                        oConfig.ValidaCampo(cboDataDe.SelectedValue), _
                                                        oConfig.ValidaCampo(cboDataAte.SelectedValue), _
                                                        Session("KPI"))

            vDataSet = Session("DataSet")

            Dim i As System.Int32
            For i = 0 To vDataSet.Tables(0).Columns.Count - 1
                vCampo = vCampo & vDataSet.Tables(0).Columns(i).ColumnName & ";"
            Next
            Grid1.Campo = Mid(vCampo, 1, Len(vCampo) - 1)

            '-----monta grafico (verifica se grafico esta ativo)
            If Not vGrafico = Nothing Then
                vDataSet = WS_Consulta.Template(Session("Conn_Banco"), _
                                                    vProcedure, _
                                                    vGrafico, _
                                                    Filtro_Acesso.pPakage, _
                                                    Filtro_Acesso.pParametro1, _
                                                    Filtro_Acesso.pParametro_Filial, _
                                                    Filtro_Acesso.pParametro_Usuario, _
                                                    Filtro_Acesso.pParametro_Centro_Custo, _
                                                    Filtro_Acesso.pParametro_Departamento, _
                                                    Filtro_Acesso.pParametro_Setor, _
                                                    oConfig.ValidaCampo(cboDataDe.SelectedValue), _
                                                    oConfig.ValidaCampo(cboDataAte.SelectedValue), _
                                                    Session("KPI"))
            End If
        End If
    End Sub

    Protected Sub BtOk_Click(sender As Object, e As EventArgs) Handles BtOk.Click
        pnlMenssagem.Visible = False
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
