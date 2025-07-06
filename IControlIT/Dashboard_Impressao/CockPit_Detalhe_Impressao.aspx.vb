Public Class CockPit_Detalhe_Impressao
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim vDataSet As New Data.DataSet
            Dim vDataView As Data.DataView = Nothing
            Dim i As System.Int32 = 0
            Dim Mes As System.Double = 0
            Dim Ano As System.Double = 0

            '-----gera dados 
            '---------------------------------------------------------------------------------------
            vDataSet = Session("CockPit")
            lblDescricao.Text = Trim(Request("Descricao"))

            '-------------------------
            If lblDescricao.Text = "Areas" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '3' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
                dtgGrupo.DataSource = vDataView
                dtgGrupo.Columns(0).Visible = False
                dtgGrupo.Columns(2).Visible = False
                dtgGrupo.Columns(3).Visible = False
                dtgGrupo.Columns(4).Visible = False
                dtgGrupo.DataBind()

                lblLinha.Visible = True
                lblQTDLinha.Visible = True
                lblQTDLinha.Text = dtgGrupo.Items.Count
            End If

            '-------------------------
            If lblDescricao.Text = "Imediatos" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '4' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
                dtgGrupo.DataSource = vDataView
                dtgGrupo.Columns(0).Visible = False
                dtgGrupo.Columns(2).Visible = False
                dtgGrupo.Columns(3).Visible = False
                dtgGrupo.Columns(4).Visible = False
                dtgGrupo.DataBind()

                lblLinha.Visible = True
                lblQTDLinha.Visible = True
                lblQTDLinha.Text = dtgGrupo.Items.Count
            End If

            '--------------------------------
            If lblDescricao.Text = "Colaboradores" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '2' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
                dtgGrupo.DataSource = vDataView
                dtgGrupo.Columns(0).Visible = False
                dtgGrupo.Columns(2).Visible = False
                dtgGrupo.Columns(3).Visible = False
                dtgGrupo.Columns(4).Visible = False
                dtgGrupo.DataBind()

                lblLinha.Visible = True
                lblQTDLinha.Visible = True
                lblQTDLinha.Text = dtgGrupo.Items.Count
            End If

            '--------------------------------
            If lblDescricao.Text = "Inventario" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '7' ", Nothing, Data.DataViewRowState.OriginalRows)
                dtgGrupo.DataSource = vDataView
                dtgGrupo.Columns(0).Visible = False
                dtgGrupo.Columns(1).HeaderText = "Ativo | Colaborador"
                dtgGrupo.Columns(2).Visible = False
                dtgGrupo.Columns(3).Visible = True
                dtgGrupo.Columns(3).HeaderText = "Cota"
                dtgGrupo.Columns(4).Visible = False
                dtgGrupo.Columns(5).Visible = True
                dtgGrupo.Columns(5).HeaderText = "Tipo"
                dtgGrupo.DataBind()

                For i = 0 To dtgGrupo.Items.Count - 1
                    Mes = Mes + IIf(dtgGrupo.Items(i).Cells(3).Text = "&nbsp;", 0, dtgGrupo.Items(i).Cells(3).Text)
                Next

            End If

            '--------------------------------
            If Mid(lblDescricao.Text, 1, 13) = "Ativo Tipo - " Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '12' AND ID = '" & Request("Filtro") + "'", "Ano DESC", Data.DataViewRowState.OriginalRows)
                dtgGrupo.DataSource = vDataView
                dtgGrupo.Columns(0).Visible = True
                dtgGrupo.Columns(2).Visible = False
                dtgGrupo.Columns(3).Visible = True
                dtgGrupo.Columns(4).Visible = True
                dtgGrupo.DataBind()


                For i = 0 To dtgGrupo.Items.Count - 1
                    Mes = Mes + IIf(dtgGrupo.Items(i).Cells(3).Text = "&nbsp;", 0, dtgGrupo.Items(i).Cells(3).Text)
                    Ano = Ano + IIf(dtgGrupo.Items(i).Cells(4).Text = "&nbsp;", 0, dtgGrupo.Items(i).Cells(4).Text)
                Next

                lblMes.Visible = True
                lblCustoMes.Visible = True
                lblCustoMes.Text = Format(Mes, "R$##########,###########0")
                lblAno.Visible = True
                lblCustoAno.Visible = True
                lblCustoAno.Text = Format(Ano, "R$##########,###########0")
            End If

            '--------------------------------
            If Mid(lblDescricao.Text, 1, 16) = "Colaboradores - " Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '13' AND ID = '" & Request("Filtro") + "'", "Ano DESC", Data.DataViewRowState.OriginalRows)
                dtgGrupo.DataSource = vDataView
                dtgGrupo.Columns(0).Visible = True
                dtgGrupo.Columns(2).Visible = False
                dtgGrupo.Columns(3).Visible = True
                dtgGrupo.Columns(4).Visible = True
                dtgGrupo.DataBind()

                For i = 0 To dtgGrupo.Items.Count - 1
                    Mes = Mes + IIf(dtgGrupo.Items(i).Cells(3).Text = "&nbsp;", 0, dtgGrupo.Items(i).Cells(3).Text)
                    Ano = Ano + IIf(dtgGrupo.Items(i).Cells(4).Text = "&nbsp;", 0, dtgGrupo.Items(i).Cells(4).Text)
                Next

                lblMes.Visible = True
                lblCustoMes.Visible = True
                lblCustoMes.Text = Format(Mes, "R$##########,###########0")
                lblAno.Visible = True
                lblCustoAno.Visible = True
                lblCustoAno.Text = Format(Ano, "R$##########,###########0")
            End If
        End If
    End Sub

    Protected Sub dtgGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgGrupo.SelectedIndexChanged
        Dim vDataSet As New Data.DataSet
        Dim vDataView As Data.DataView = Nothing

        '-----gera dados 
        '---------------------------------------------------------------------------------------
        If Mid(lblDescricao.Text, 1, 13) = "Ativo Tipo - " Then
            Response.Redirect("../Dashboard_Impressao/Conta_OnLine_Detalhe_Impressao.aspx?viewBilhete=Tipo&Descricao=" & dtgGrupo.SelectedItem.Cells(1).Text & " &Filtro=" & dtgGrupo.SelectedItem.Cells(2).Text & " &Total=" & dtgGrupo.SelectedItem.Cells(3).Text)
        End If

        If Mid(lblDescricao.Text, 1, 16) = "Colaboradores - " Then
            pnlDetalhe.Visible = True
            vDataSet = Session("CockPit")
            vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '14' AND QTD = " & CType(dtgGrupo.SelectedItem.Cells(2).Text, System.Int32), "Mes DESC", Data.DataViewRowState.OriginalRows)
            dtgTopGastoMes.DataSource = vDataView
            dtgTopGastoMes.DataBind()
        End If
    End Sub

    Protected Sub dtgTopGastoMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgTopGastoMes.SelectedIndexChanged
        If Mid(lblDescricao.Text, 1, 16) = "Colaboradores - " Then
            Response.Redirect("../Dashboard_Impressao/Conta_OnLine_Detalhe_Impressao.aspx?viewBilhete=Area&Descricao=" & dtgTopGastoMes.SelectedItem.Cells(1).Text & " &Filtro=" & dtgTopGastoMes.SelectedItem.Cells(3).Text & " &Total=" & dtgTopGastoMes.SelectedItem.Cells(2).Text)
        End If
    End Sub

    Protected Sub BtOk_Click(sender As Object, e As ImageClickEventArgs) Handles BtOk.Click
        Dim vDataSet As New Data.DataSet
        Dim vDataView As Data.DataView = Nothing
        Dim Mes As System.Double = 0
        Dim Ano As System.Double = 0

        vDataSet = Session("CockPit")

        If lblDescricao.Text = "Areas" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '3' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Descricao Like '%" & txtOrdenacao.Text & "%' and Filtro = '3' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
            End If

            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(2).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.DataBind()

            lblLinha.Visible = True
            lblQTDLinha.Visible = True
            lblQTDLinha.Text = dtgGrupo.Items.Count
        End If

        '-------------------------
        If lblDescricao.Text = "Imediatos" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '4' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Descricao Like '%" & txtOrdenacao.Text & "%' and Filtro = '4' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
            End If
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(2).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.DataBind()

            lblLinha.Visible = True
            lblQTDLinha.Visible = True
            lblQTDLinha.Text = dtgGrupo.Items.Count
        End If

        '--------------------------------
        If lblDescricao.Text = "Colaboradores" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '2' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Descricao Like '%" & txtOrdenacao.Text & "%' and Filtro = '2' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
            End If

            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(2).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.DataBind()

            lblLinha.Visible = True
            lblQTDLinha.Visible = True
            lblQTDLinha.Text = dtgGrupo.Items.Count
        End If

        '--------------------------------
        If lblDescricao.Text = "Inventario" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '7' ", Nothing, Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Descricao Like '%" & txtOrdenacao.Text & "%' and Filtro = '7' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
            End If

            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(1).HeaderText = "Ativo | Colaborador"
            dtgGrupo.Columns(2).Visible = False
            dtgGrupo.Columns(3).Visible = True
            dtgGrupo.Columns(3).HeaderText = "Cota"
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.Columns(5).Visible = True
            dtgGrupo.Columns(5).HeaderText = "Tipo"
            dtgGrupo.DataBind()

            For i = 0 To dtgGrupo.Items.Count - 1
                Mes = Mes + IIf(dtgGrupo.Items(i).Cells(3).Text = "&nbsp;", 0, dtgGrupo.Items(i).Cells(3).Text)
            Next
        End If

        '--------------------------------
        If Mid(lblDescricao.Text, 1, 13) = "Ativo Tipo - " Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '12' AND ID = '" & Request("Filtro") + "'", "Ano DESC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Descricao Like '%" & txtOrdenacao.Text & "%' and Filtro = '12' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
            End If

            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = True
            dtgGrupo.Columns(2).Visible = False
            dtgGrupo.Columns(3).Visible = True
            dtgGrupo.Columns(4).Visible = True
            dtgGrupo.DataBind()

            For i = 0 To dtgGrupo.Items.Count - 1
                Mes = Mes + IIf(dtgGrupo.Items(i).Cells(3).Text = "&nbsp;", 0, dtgGrupo.Items(i).Cells(3).Text)
                Ano = Ano + IIf(dtgGrupo.Items(i).Cells(4).Text = "&nbsp;", 0, dtgGrupo.Items(i).Cells(4).Text)
            Next

            lblMes.Visible = True
            lblCustoMes.Visible = True
            lblCustoMes.Text = Format(Mes, "R$##########,###########0")
            lblAno.Visible = True
            lblCustoAno.Visible = True
            lblCustoAno.Text = Format(Ano, "R$##########,###########0")
        End If

        '--------------------------------
        If Mid(lblDescricao.Text, 1, 24) = "Colaboradores - " Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Filtro = '13' AND ID = '" & Request("Filtro") + "'", "Ano DESC", Data.DataViewRowState.OriginalRows)
            Else

                vDataView = New Data.DataView(vDataSet.Tables(0), "Descricao Like '%" & txtOrdenacao.Text & "%' and Filtro = '13' ", "Descricao ASC", Data.DataViewRowState.OriginalRows)
            End If

            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = True
            dtgGrupo.Columns(2).Visible = False
            dtgGrupo.Columns(3).Visible = True
            dtgGrupo.Columns(4).Visible = True
            dtgGrupo.DataBind()

            For i = 0 To dtgGrupo.Items.Count - 1
                Mes = Mes + IIf(dtgGrupo.Items(i).Cells(3).Text = "&nbsp;", 0, dtgGrupo.Items(i).Cells(3).Text)
                Ano = Ano + IIf(dtgGrupo.Items(i).Cells(4).Text = "&nbsp;", 0, dtgGrupo.Items(i).Cells(4).Text)
            Next

            lblMes.Visible = True
            lblCustoMes.Visible = True
            lblCustoMes.Text = Format(Mes, "R$##########,###########0")
            lblAno.Visible = True
            lblCustoAno.Visible = True
            lblCustoAno.Text = Format(Ano, "R$##########,###########0")
        End If
    End Sub

    Protected Sub btFecharAbrir_Click(sender As Object, e As EventArgs) Handles btFecharAbrir.Click
        pnlDetalhe.Visible = False
    End Sub
End Class
