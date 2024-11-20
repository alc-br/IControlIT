Public Class Dashboar_Impressao
    Inherits System.Web.UI.Page
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vDataSet As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                                "Impressões ", _
                                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Call Master.Localizar(Nothing, Nothing)

            '-----home
            Call Master.home("usuario")
            Dim v_dataSet As New Data.DataSet

            '-----lista descricao dos campos de hierarquia
            'vDataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Texto_Hierarquia", Nothing, Nothing, Nothing, Nothing, Nothing)
            'If vDataSet.Tables(0).Rows.Count > 0 Then
            '    lblFilial.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Filial")
            '    lblCentro_Custo.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Centro_Custo")
            '    lblDepartamento.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Departamento")
            '    lblSetor.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Setor")
            '    lblSecao.Text = vDataSet.Tables(0).Rows(0).Item("Nm_Secao")
            'End If

            '-----busca calendario
            v_dataSet = WS_Modulo.Deskboard(Session("Conn_Banco"), "sp_Ano_Fiscal", Nothing, Nothing, Nothing)
            If Session("Calendario") = Nothing Then
                Dim Linha As Data.DataRow
                For Each Linha In v_dataSet.Tables(0).Rows
                    If Linha.Item(1) = "TRUE" Then
                        Session("Calendario") = Linha.Item(0)
                    End If
                Next
            End If

            If v_dataSet.Tables(0).Rows.Count > 0 Then
                bt01.AlternateText = v_dataSet.Tables(0).Rows(0).Item("Mes")
                bt01.Enabled = v_dataSet.Tables(0).Rows(0).Item("Mostrar")
                bt01.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(0).Item("Mes"), 2) + ".png"
                If bt01.Enabled = False Then bt01.ImageUrl = bt01.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(0).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(0).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt01.AlternateText Then bt01.ImageUrl = bt01.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(0).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(0).Item("Mes"), 2) + ".png")

                bt02.AlternateText = v_dataSet.Tables(0).Rows(1).Item("Mes")
                bt02.Enabled = v_dataSet.Tables(0).Rows(1).Item("Mostrar")
                bt02.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(1).Item("Mes"), 2) + ".png"
                If bt02.Enabled = False Then bt02.ImageUrl = bt02.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(1).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(1).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt02.AlternateText Then bt02.ImageUrl = bt02.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(1).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(1).Item("Mes"), 2) + ".png")

                bt03.AlternateText = v_dataSet.Tables(0).Rows(2).Item("Mes")
                bt03.Enabled = v_dataSet.Tables(0).Rows(2).Item("Mostrar")
                bt03.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(2).Item("Mes"), 2) + ".png"
                If bt03.Enabled = False Then bt03.ImageUrl = bt03.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(2).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(2).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt03.AlternateText Then bt03.ImageUrl = bt03.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(2).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(2).Item("Mes"), 2) + ".png")

                bt04.AlternateText = v_dataSet.Tables(0).Rows(3).Item("Mes")
                bt04.Enabled = v_dataSet.Tables(0).Rows(3).Item("Mostrar")
                bt04.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(3).Item("Mes"), 2) + ".png"
                If bt04.Enabled = False Then bt04.ImageUrl = bt04.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(3).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(3).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt04.AlternateText Then bt04.ImageUrl = bt04.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(3).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(3).Item("Mes"), 2) + ".png")

                bt05.AlternateText = v_dataSet.Tables(0).Rows(4).Item("Mes")
                bt05.Enabled = v_dataSet.Tables(0).Rows(4).Item("Mostrar")
                bt05.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(4).Item("Mes"), 2) + ".png"
                If bt05.Enabled = False Then bt05.ImageUrl = bt05.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(4).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(4).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt05.AlternateText Then bt05.ImageUrl = bt05.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(4).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(4).Item("Mes"), 2) + ".png")

                bt06.AlternateText = v_dataSet.Tables(0).Rows(5).Item("Mes")
                bt06.Enabled = v_dataSet.Tables(0).Rows(5).Item("Mostrar")
                bt06.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(5).Item("Mes"), 2) + ".png"
                If bt06.Enabled = False Then bt06.ImageUrl = bt06.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(5).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(5).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt06.AlternateText Then bt06.ImageUrl = bt06.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(5).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(5).Item("Mes"), 2) + ".png")

                bt07.AlternateText = v_dataSet.Tables(0).Rows(6).Item("Mes")
                bt07.Enabled = v_dataSet.Tables(0).Rows(6).Item("Mostrar")
                bt07.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(6).Item("Mes"), 2) + ".png"
                If bt07.Enabled = False Then bt07.ImageUrl = bt07.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(6).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(6).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt07.AlternateText Then bt07.ImageUrl = bt07.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(6).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(6).Item("Mes"), 2) + ".png")

                bt08.AlternateText = v_dataSet.Tables(0).Rows(7).Item("Mes")
                bt08.Enabled = v_dataSet.Tables(0).Rows(7).Item("Mostrar")
                bt08.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(7).Item("Mes"), 2) + ".png"
                If bt08.Enabled = False Then bt08.ImageUrl = bt08.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(7).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(7).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt08.AlternateText Then bt08.ImageUrl = bt08.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(7).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(7).Item("Mes"), 2) + ".png")

                bt09.AlternateText = v_dataSet.Tables(0).Rows(8).Item("Mes")
                bt09.Enabled = v_dataSet.Tables(0).Rows(8).Item("Mostrar")
                bt09.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(8).Item("Mes"), 2) + ".png"
                If bt09.Enabled = False Then bt09.ImageUrl = bt09.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(8).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(8).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt09.AlternateText Then bt09.ImageUrl = bt09.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(8).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(8).Item("Mes"), 2) + ".png")

                bt10.AlternateText = v_dataSet.Tables(0).Rows(9).Item("Mes")
                bt10.Enabled = v_dataSet.Tables(0).Rows(9).Item("Mostrar")
                bt10.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(9).Item("Mes"), 2) + ".png"
                If bt10.Enabled = False Then bt10.ImageUrl = bt10.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(9).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(9).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt10.AlternateText Then bt10.ImageUrl = bt10.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(9).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(9).Item("Mes"), 2) + ".png")

                bt11.AlternateText = v_dataSet.Tables(0).Rows(10).Item("Mes")
                bt11.Enabled = v_dataSet.Tables(0).Rows(10).Item("Mostrar")
                bt11.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(10).Item("Mes"), 2) + ".png"
                If bt11.Enabled = False Then bt11.ImageUrl = bt11.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(10).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(10).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt11.AlternateText Then bt11.ImageUrl = bt11.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(10).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(10).Item("Mes"), 2) + ".png")

                bt12.AlternateText = v_dataSet.Tables(0).Rows(11).Item("Mes")
                bt12.Enabled = v_dataSet.Tables(0).Rows(11).Item("Mostrar")
                bt12.ImageUrl = "~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(11).Item("Mes"), 2) + ".png"
                If bt12.Enabled = False Then bt12.ImageUrl = bt12.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(11).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Enabled_" + Right(v_dataSet.Tables(0).Rows(11).Item("Mes"), 2) + ".png")
                If Session("Calendario") = bt12.AlternateText Then bt12.ImageUrl = bt12.ImageUrl.Replace("~/Img_Sistema/Indicadores/bt_Calendario_" + Right(v_dataSet.Tables(0).Rows(11).Item("Mes"), 2) + ".png", "~/Img_Sistema/Indicadores/bt_Calendario_Select_" + Right(v_dataSet.Tables(0).Rows(11).Item("Mes"), 2) + ".png")
            End If

            Call Executa_Consulta()
            Call Grafico_Park()
            Call Grafico_Tipo()
            Call Grafico_Unidade()
            Call Grafico_Curva()
            Call Grafico_Tipo_Area()
            Call Grafico_Tipo_Papel()
            Call Grafico_Tipo_Documento()
        End If
    End Sub

    Protected Sub btUnidade_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUnidade.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Dashboard_Telefonia/CockPit_Detalhe.aspx" & "?Descricao=Areas" & "','','resizable=yes, menubar=yes, scrollbars=no, height=768px, width=1024px, top=10, left=10');", True)
    End Sub

    Protected Sub btColaborador_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btColaborador.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Dashboard_Telefonia/CockPit_Detalhe.aspx" & "?Descricao=Colaboradores" & "','','resizable=yes, menubar=yes, scrollbars=no, height=768px, width=1024px, top=10, left=10');", True)
    End Sub

    Protected Sub btImediatos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btImediatos.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Dashboard_Telefonia/CockPit_Detalhe.aspx" & "?Descricao=Imediatos" & "','','resizable=yes, menubar=yes, scrollbars=no, height=768px, width=1024px, top=10, left=10');", True)
    End Sub

    Protected Sub btInventario_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btInventario.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Dashboard_Telefonia/CockPit_Detalhe.aspx" & "?Descricao=Inventario" & "','','resizable=yes, menubar=yes, scrollbars=no, height=768px, width=1024px, top=10, left=10');", True)
    End Sub

    Protected Sub bt01_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt01.Click
        data_selecionada(sender)
    End Sub

    Protected Sub bt02_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt02.Click
        data_selecionada(sender)
    End Sub

    Protected Sub bt03_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt03.Click
        data_selecionada(sender)
    End Sub

    Protected Sub bt04_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt04.Click
        data_selecionada(sender)
    End Sub

    Protected Sub bt05_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt05.Click
        data_selecionada(sender)
    End Sub

    Protected Sub bt06_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt06.Click
        data_selecionada(sender)
    End Sub

    Protected Sub bt07_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt07.Click
        data_selecionada(sender)
    End Sub

    Protected Sub bt08_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt08.Click
        data_selecionada(sender)
    End Sub

    Protected Sub bt09_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt09.Click
        data_selecionada(sender)
    End Sub

    Protected Sub bt10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt10.Click
        data_selecionada(sender)
    End Sub

    Protected Sub bt11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt11.Click
        data_selecionada(sender)
    End Sub

    Protected Sub bt12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt12.Click
        data_selecionada(sender)
    End Sub

    Public Sub Executa_Consulta()
        Dim vDataSet As New Data.DataSet
        Dim vDataView As Data.DataView = Nothing

        vDataSet = Session("CockPit")

        '-----monta unidades do gestor
        vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '3' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
        txtUnidade.Text = Format(vDataView.Item(0).Item(2), "#########0")

        '-----monta colaboradoes do gestor
        vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '2' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
        txtColaborador.Text = Format(vDataView.Item(0).Item(2), "#########0")

        '-----monta imediatos do gestor
        vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '4' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
        If vDataView.Count = 0 Then
            txtImediato.Text = 0
        Else
            txtImediato.Text = Format(vDataView.Item(0).Item(2), "#########0")
        End If

        '-----monta total de gasto mes ano
        vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '8' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
        txtTotalGastoMes.Text = Format(vDataView.Item(0).Item(3), "R$##########,###########0")
        txtTotalGastoAno.Text = Format(vDataView.Item(0).Item(4), "R$##########,###########0")

        '-----monta total de custo fixo mes ano
        vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '27' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
        txtTotalFixoMes.Text = Format(vDataView.Item(0).Item(3), "R$##########,###########0")
        txtTotalFixoAno.Text = Format(vDataView.Item(0).Item(4), "R$##########,###########0")

        '-----monta inventario
        vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '7' ", Nothing, Data.DataViewRowState.OriginalRows)
        If vDataView.Count = 0 Then
            txtInventario.Text = 0
        Else
            txtInventario.Text = Format(vDataView.Item(0).Item(2), "#########0")
        End If

        '-----monta grip com detalhamento por unidade
        vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '11' ", "Ano DESC", Data.DataViewRowState.OriginalRows)
        dtgGrupo.DataSource = vDataView
        dtgGrupo.DataBind()

        '-----monta top de gasto de usuario mes
        vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '1' ", "Mes DESC", Data.DataViewRowState.OriginalRows)
        dtgAtivo.DataSource = vDataView
        dtgAtivo.DataBind()

    End Sub

    Public Sub Grafico_Park()
        Dim vDataSet As New Data.DataSet
        vDataSet = Session("CockPit")

        '-----variavel grafico----------------------------------------------------------------
        Dim Linha As Data.DataRow
        Dim i As System.Int32 = 0

        For Each Linha In vDataSet.Tables(0).Rows
            If Linha.Item("Filtro") = "30" Then
                i = i + 1
            End If
        Next
        Dim vTipo(i - 1) As System.String
        Dim vQTD(i - 1) As System.Double

        i = 0
        For Each Linha In vDataSet.Tables(0).Rows
            If Linha.Item("Filtro") = "30" Then
                vTipo(i) = Linha.Item("Descricao")
                vQTD(i) = Linha.Item("QTD")
                i = i + 1
            End If
        Next

        chtPark.Series(0).Points.DataBindXY(vTipo, vQTD)
    End Sub

    Public Sub Grafico_Tipo()
        Dim vDataSet As New Data.DataSet
        vDataSet = Session("CockPit")

        '-----variavel grafico----------------------------------------------------------------
        Dim Linha As Data.DataRow
        Dim i As System.Int32 = 0

        For Each Linha In vDataSet.Tables(0).Rows
            If Linha.Item("Filtro") = "5" Then
                i = i + 1
            End If
        Next
        Dim vTipo(i - 1) As System.String
        Dim vQTD(i - 1) As System.Double

        i = 0
        For Each Linha In vDataSet.Tables(0).Rows
            If Linha.Item("Filtro") = "5" Then
                vTipo(i) = Linha.Item("Descricao")
                vQTD(i) = Format(Linha.Item("QTD"), "#########0")
                i = i + 1
            End If
        Next

        chtCurvaAtivo.Series(0).Points.DataBindXY(vTipo, vQTD)
    End Sub

    Public Sub Grafico_Unidade()
        Dim vDataSet As New Data.DataSet
        vDataSet = Session("CockPit")

        Dim Linha As Data.DataRow
        Dim i As System.Int32 = 0
        Dim Grupo As System.String = ""

        For i = 0 To 1
            chtCurvaUnidade.Series(i).Points.Clear()
        Next i

        For Each Linha In vDataSet.Tables(0).Rows
            If Linha.Item("Filtro") = "6" Then
                If Linha.Item("ID") = Grupo Then
                    i = i + 1
                    chtCurvaUnidade.Series(i).Points.AddXY(Linha.Item("ID"), Linha.Item("Ano"))

                    chtCurvaUnidade.Series(i).Legend = "Default"
                    chtCurvaUnidade.Series(i).LegendText = Linha.Item("Descricao")
                    chtCurvaUnidade.Series(i).IsVisibleInLegend = True
                Else
                    Grupo = Linha.Item("ID")
                    i = 0
                    chtCurvaUnidade.Series(i).Points.AddXY(Linha.Item("ID"), Linha.Item("Ano"))

                    chtCurvaUnidade.Series(i).Legend = "Default"
                    chtCurvaUnidade.Series(i).LegendText = Linha.Item("Descricao")
                    chtCurvaUnidade.Series(i).IsVisibleInLegend = True
                End If
            End If
        Next
    End Sub

    Public Sub Grafico_Curva()
        Dim vDataSet As New Data.DataSet
        vDataSet = Session("CockPit")

        Dim Linha As Data.DataRow
        Dim i As System.Int32 = 0

        '-----variavel grafico----------------------------------------------------------------
        i = 0

        For Each Linha In vDataSet.Tables(0).Rows
            If Linha.Item("Filtro") = "29" Then
                i = i + 1
            End If
        Next
        Dim vLote(i - 1) As System.String
        Dim vTotal(i - 1) As System.Double

        i = 0
        For Each Linha In vDataSet.Tables(0).Rows
            If Linha.Item("Filtro") = "29" Then
                vLote(i) = Linha.Item("Descricao")
                vTotal(i) = Linha.Item("Mes")
                i = i + 1
            End If
        Next

        chtCurvaGasto.Series("A1").Points.DataBindXY(vLote, vTotal)
    End Sub

    Public Sub Grafico_Tipo_Area()
        Dim vDataSet As New Data.DataSet
        vDataSet = Session("CockPit")

        Dim Linha As Data.DataRow
        Dim i As System.Int32 = 0
        Dim Grupo As System.String = ""

        For i = 0 To 1
            chtCurvaTipo.Series(i).Points.Clear()
        Next i

        For Each Linha In vDataSet.Tables(0).Rows
            If Linha.Item("Filtro") = "31" Then
                If Linha.Item("ID") = Grupo Then
                    i = i + 1
                    chtCurvaTipo.Series(i).Points.AddXY(Linha.Item("ID"), Linha.Item("Mes"))

                    chtCurvaTipo.Series(i).Legend = "Default"
                    chtCurvaTipo.Series(i).LegendText = Linha.Item("Descricao")
                    chtCurvaTipo.Series(i).IsVisibleInLegend = True

                Else
                    Grupo = Linha.Item("ID")
                    i = 0
                    chtCurvaTipo.Series(i).Points.AddXY(Linha.Item("ID"), Linha.Item("Mes"))

                    chtCurvaTipo.Series(i).Legend = "Default"
                    chtCurvaTipo.Series(i).LegendText = Linha.Item("Descricao")
                    chtCurvaTipo.Series(i).IsVisibleInLegend = True
                End If
            End If
        Next
    End Sub

    Public Sub Grafico_Tipo_Papel()
        Dim vDataSet As New Data.DataSet
        vDataSet = Session("CockPit")

        Dim Linha As Data.DataRow
        Dim i As System.Int32 = 0
        Dim Grupo As System.String = ""

        For i = 0 To 4
            chtCurvaTipoPapel.Series(i).Points.Clear()
        Next i

        For Each Linha In vDataSet.Tables(0).Rows
            If Linha.Item("Filtro") = "15" Then
                If Linha.Item("ID") = Grupo Then
                    i = i + 1
                    chtCurvaTipoPapel.Series(i).Points.AddXY(Linha.Item("ID"), Linha.Item("Mes"))

                    chtCurvaTipoPapel.Series(i).Legend = "Default"
                    chtCurvaTipoPapel.Series(i).LegendText = Linha.Item("Descricao")
                    chtCurvaTipoPapel.Series(i).IsVisibleInLegend = True

                Else
                    Grupo = Linha.Item("ID")
                    i = 0
                    chtCurvaTipoPapel.Series(i).Points.AddXY(Linha.Item("ID"), Linha.Item("Mes"))

                    chtCurvaTipoPapel.Series(i).Legend = "Default"
                    chtCurvaTipoPapel.Series(i).LegendText = Linha.Item("Descricao")
                    chtCurvaTipoPapel.Series(i).IsVisibleInLegend = True
                End If
            End If
        Next
    End Sub

    Public Sub Grafico_Tipo_Documento()
        Dim vDataSet As New Data.DataSet
        vDataSet = Session("CockPit")

        '-----variavel grafico----------------------------------------------------------------
        Dim Linha As Data.DataRow
        Dim i As System.Int32 = 0

        For Each Linha In vDataSet.Tables(0).Rows
            If Linha.Item("Filtro") = "23" Then
                i = i + 1
            End If
        Next
        Dim vTipo(i - 1) As System.String
        Dim vMes(i - 1) As System.Double

        i = 0
        For Each Linha In vDataSet.Tables(0).Rows
            If Linha.Item("Filtro") = "23" Then
                vTipo(i) = Linha.Item("Descricao")
                vMes(i) = Linha.Item("Ano")
                i = i + 1
            End If
        Next

        chtTipoDocumento.Series(0).Points.DataBindXY(vTipo, vMes)
    End Sub

    Public Sub data_selecionada(ByVal botao As ImageButton)
        bt01.ImageUrl = bt01.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        bt02.ImageUrl = bt02.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        bt03.ImageUrl = bt03.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        bt04.ImageUrl = bt04.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        bt05.ImageUrl = bt05.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        bt06.ImageUrl = bt06.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        bt07.ImageUrl = bt07.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        bt08.ImageUrl = bt08.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        bt09.ImageUrl = bt09.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        bt10.ImageUrl = bt10.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        bt11.ImageUrl = bt11.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        bt12.ImageUrl = bt12.ImageUrl.Replace("bt_Calendario_Select", "bt_Calendario")
        Session("CockPit") = WS_Modulo.Deskboard(Session("Conn_Banco"), "cockpit", Session("KPI"), Session("Id_Usuario"), botao.AlternateText)
        Session("Calendario") = botao.AlternateText

        Dim vCalendario As String = "bt_Calendario_" & Right(Session("Calendario"), 2)
        Dim vCalendario_Select As String = "bt_Calendario_Select_" & Right(Session("Calendario"), 2)

        If Session("Calendario") = botao.AlternateText Then botao.ImageUrl = botao.ImageUrl.Replace(vCalendario, vCalendario_Select)

        Call Executa_Consulta()
        Call Grafico_Park()
        Call Grafico_Tipo()
        Call Grafico_Unidade()
        Call Grafico_Curva()
        Call Grafico_Tipo_Area()
        Call Grafico_Tipo_Papel()
        Call Grafico_Tipo_Documento()
    End Sub

    Protected Sub dtgAtivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgAtivo.SelectedIndexChanged
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Dashboard_Impressao/CockPit_Detalhe.aspx" & "?Descricao=Ativo Tipo - " & dtgAtivo.SelectedItem.Cells(0).Text + "&Filtro=" & dtgAtivo.SelectedItem.Cells(0).Text & "','','resizable=yes, menubar=yes, scrollbars=no, height=768px, width=1024px, top=10, left=10');", True)
    End Sub

    Protected Sub dtgGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgGrupo.SelectedIndexChanged
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Dashboard_Impressao/CockPit_Detalhe.aspx" & "?Descricao=Colaboradores - " & dtgGrupo.SelectedItem.Cells(0).Text + "&Filtro=" & dtgGrupo.SelectedItem.Cells(0).Text & "','','resizable=yes, menubar=yes, scrollbars=no, height=768px, width=1024px, top=10, left=10');", True)
    End Sub
    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub

End Class
