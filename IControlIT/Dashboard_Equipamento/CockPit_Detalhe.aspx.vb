Public Class CockPit_Detalhe
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

            '--------------------------------
            If lblDescricao.Text = "Ativo sem validação" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", "'"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
                dtgGrupo.DataSource = vDataView
                dtgGrupo.Columns(0).Visible = False
                dtgGrupo.Columns(3).Visible = False
                dtgGrupo.Columns(4).Visible = False
                dtgGrupo.Columns(7).Visible = True
                dtgGrupo.DataBind()

                txtTotal.Text = Format(0, "R$##########,###########0")
            End If

            '--------------------------------
            If lblDescricao.Text = "Ativo sem validação ((FY) | Ano Fiscal)" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' " & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", """"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
                dtgGrupo.DataSource = vDataView
                dtgGrupo.Columns(0).Visible = False
                dtgGrupo.Columns(3).Visible = False
                dtgGrupo.Columns(4).Visible = False
                dtgGrupo.Columns(7).Visible = True
                dtgGrupo.DataBind()

                txtTotal.Text = Format(0, "R$##########,###########0")
            End If
        End If
    End Sub

    Protected Sub BtOk_Click(sender As Object, e As ImageClickEventArgs) Handles BtOk.Click
        Dim vDataSet As New Data.DataSet
        Dim vDataView As Data.DataView = Nothing
        Dim Mes As System.Double = 0
        Dim Ano As System.Double = 0

        vDataSet = Session("CockPit")

        '--------------------------------
        If lblDescricao.Text = "Ativo sem validação" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", """"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Nm_Consumidor Like '%" & txtOrdenacao.Text & "%' AND Grupo = 'Apontamento' AND Lote = '" & Session("Calendario") & "/01" & "'" & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", """"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            End If
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.Columns(7).Visible = True
            dtgGrupo.DataBind()

            txtTotal.Text = Format(0, "R$##########,###########0")
        End If

        '--------------------------------
        If lblDescricao.Text = "Ativo sem validação ((FY) | Ano Fiscal)" Then
            If txtOrdenacao.Text = "" Then
                vDataView = New Data.DataView(vDataSet.Tables(0), "Grupo = 'Apontamento' " & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", """"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            Else
                vDataView = New Data.DataView(vDataSet.Tables(0), "Nm_Consumidor Like '%" & txtOrdenacao.Text & "%' AND Grupo = 'Apontamento' " & " AND UsuarioVisitaramNaoConcluiramMes = 1" & Replace(Request("Filtro"), "*-*", """"), "Nm_Consumidor, Dt_Lote ASC", Data.DataViewRowState.OriginalRows)
            End If
            dtgGrupo.DataSource = vDataView
            dtgGrupo.Columns(0).Visible = False
            dtgGrupo.Columns(3).Visible = False
            dtgGrupo.Columns(4).Visible = False
            dtgGrupo.Columns(7).Visible = True
            dtgGrupo.DataBind()

            txtTotal.Text = Format(0, "R$##########,###########0")
        End If
    End Sub

End Class
