
Partial Public Class Termo_Devolucao_Desktop
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim vDataSet As New Data.DataSet
    Dim vDataSetXML As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            '-----lista holding
            vDataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Valida_Usuario", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)
            imgLogo.ImageUrl = vDataSet.Tables(0).Rows(0).Item("Logo").ToString.Replace(".png", "_color.png")

            '-----lista xml
            vDataSetXML.ReadXml(App_Path() & "Termo/Default/Dados.xml")

            lbl_titulo.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Titulo").ToString
            lbl_txtTexto1.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto1").ToString

            lbl_txtEquipamento.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Equipamento").ToString
            lbl_txtNumero_Serie.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("NS").ToString
            lbl_txtModelo.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Modelo").ToString
            lbl_txtCodigo_TI.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Codigo").ToString
            lbl_txtAcessorio.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Acessorio").ToString

            lbl_txtDadosUsuario.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("DadosUsuario").ToString

            lbl_txtCentroCusto.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("CentroCusto").ToString
            lbl_txtMatricula.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Matricula").ToString
            lbl_txtDepartamento.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Departamento").ToString
            lbl_txtCiente.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Ciente").ToString
            lbl_txtDadosAparelho.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("DadosAparelho").ToString

            lbl_txtObs.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Obs").ToString

            lbl_Ass_1.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_1").ToString
            lbl_Ass_2.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_2").ToString
            lbl_Ass_3.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_3").ToString

            vDataSet = WS_Cadastro.Ativo(Session("Conn_Banco"), Request("Id_Ativo"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                          Nothing, Nothing, Nothing, Nothing, Nothing,
                                          Nothing, "sd_SL_Termo", True, Nothing, Nothing, Nothing, Nothing, Nothing)

            Dim vDataView As System.Data.DataView
            vDataView = New Data.DataView(vDataSet.Tables(0), "Id_Consumidor = " & Request("Id_Consumidor"), Nothing, Data.DataViewRowState.OriginalRows)

            lbl_localData.Text = DateTime.Now.ToShortDateString

            lbl_Colaborador.Text = vDataView.Item(0).Item("Nm_Consumidor")
            lbl_matricula.Text = vDataView.Item(0).Item("Matricula")
            lbl_centroCusto.Text = vDataView.Item(0).Item("Centro_Custo")
            lbl_departamento.Text = vDataView.Item(0).Item("Departamento")

            lbl_Codigo_TI.Text = vDataView.Item(0).Item("Linha")
            lbl_Acessorio.Text = vDataView.Item(0).Item("Acessorio")
            lbl_Equipamento.Text = vDataView.Item(0).Item("Tipo_Ativo")
            lbl_Modelo.Text = vDataView.Item(0).Item("Nm_Modelo_Fabricante")
            lbl_Numero_Serie.Text = vDataView.Item(0).Item("Nr_Aparelho")
        End If
    End Sub

    Private Function App_Path() As String
        Dim sPath As System.String
        sPath = System.AppDomain.CurrentDomain.BaseDirectory()
        sPath = sPath & IIf(Right(sPath, 1) = "/", "", "/").ToString
        Return sPath
    End Function

    Protected Sub btImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btImprimir.Click
        Page.ClientScript.RegisterStartupScript(Me.GetType, "ClientScript", "<script>window.print();</script>")
    End Sub

End Class

