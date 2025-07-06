
Partial Public Class Termo_Responsabilidade_Desktop
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
            Dim vEmpresa As String
            vDataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Valida_Usuario", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)
            imgLogo.ImageUrl = vDataSet.Tables(0).Rows(0).Item("Logo").ToString.Replace(".png", "_color.png")
            vEmpresa = vDataSet.Tables(0).Rows(0).Item("Empresa")

            '-----lista xml
            vDataSetXML.ReadXml(App_Path() & "Termo/Default/Dados.xml")

            lbl_titulo.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Titulo").ToString
            lbl_SubTitulo.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("subtitulo").ToString
            lbl_txtLocalidade.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Filial").ToString
            lbl_txtEquipamento.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha").ToString
            lbl_txtMatricula.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Matricula").ToString
            lbl_txtCentroCusto.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("CentroCusto").ToString
            lbl_txtDepartamento.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Departamento").ToString
            lbl_txtData.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Data").ToString
            lbl_txtTexto1.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto1").ToString
            lbl_txtTexto2.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto2").ToString
            lbl_txtTexto3.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3").ToString

            lbl_txtTexto3_1.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_1").ToString
            lbl_txtTexto3_2.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_2").ToString
            lbl_txtTexto3_3.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_3").ToString
            llbl_txtTexto3_4.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_4").ToString
            lbl_txtTexto3_5.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_5").ToString
            lbl_txtTexto3_6.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_6").ToString
            lbl_txtTexto3_7.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_7").ToString
            lbl_txtTexto3_8.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_8").ToString
            lbl_txtTexto3_9.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_9").ToString
            lbl_txtTexto3_10.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_10").ToString

            lbl_txtTexto9.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto9").ToString
            lbl_txtTexto10.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto10").ToString
            lbl_txtTexto11.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto11").ToString
            lbl_txtTexto12.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto12").ToString
            lbl_txtTexto13.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto13").ToString
            lbl_txtTexto14.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto14").ToString
            lbl_txtTexto15.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto15").ToString
            lbl_txtTexto16.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto16").ToString
            lbl_txtTexto17.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto17").ToString
            lbl_txtTexto18.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto18").ToString
            lbl_txtTexto19.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto19").ToString

            lbl_txtEquipamento.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Equipamento").ToString
            lbl_txtCodigo_TI.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Codigo_TI").ToString
            lbl_txtNumero_Serie.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Numero_Serie").ToString
            lbl_txtModelo.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Modelo").ToString
            lbl_txtPatrimonio.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Patrimonio").ToString
            lbl_txtAcessorio.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Acessorio").ToString

            lbl_txtTexto22.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto22").ToString
            lbl_txtTexto23.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto23").ToString
            lbl_txtTexto24.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto24").ToString

            lbl_Ass_1.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_1").ToString
            lbl_Ass_2.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_2").ToString
            lbl_Ass_3.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_3").ToString

            lbl_txtDadosUsuario.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("DadosUsuario").ToString
            lbl_txtCiente.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Ciente").ToString
            lbl_txtDadosAparelho.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("DadosAparelho").ToString


            vDataSet = WS_Cadastro.Ativo(Session("Conn_Banco"), Request("Id_Ativo"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                 Nothing, Nothing, Nothing, Nothing, Nothing,
                                 Nothing, "sd_SL_Termo", True, Nothing, Nothing, Nothing, Nothing, Nothing)

            Dim vDataView As System.Data.DataView
            vDataView = New Data.DataView(vDataSet.Tables(0), "Id_Consumidor = " & Request("Id_Consumidor"), Nothing, Data.DataViewRowState.OriginalRows)

            lbl_Data.Text = DateTime.Now.ToShortDateString

            lbl_txtColaborador.Text = vDataView.Item(0).Item("Nm_Consumidor") & " empregada(o) regularmente contratada(o) pela " & vEmpresa


            lbl_localidade.Text = vDataView.Item(0).Item("Nm_Filial")
            lbl_matricula.Text = vDataView.Item(0).Item("Matricula")
            lbl_centroCusto.Text = vDataView.Item(0).Item("Centro_Custo")
            lbl_departamento.Text = vDataView.Item(0).Item("Departamento")

            lbl_Equipamento.Text = vDataView.Item(0).Item("Tipo_Ativo")
            lbl_CodigoTI.Text = vDataView.Item(0).Item("Linha")
            lbl_Numero_Serie.Text = vDataView.Item(0).Item("Nr_Aparelho")
            lbl_Modelo.Text = vDataView.Item(0).Item("Nm_Modelo_Fabricante")
            lbl_Patrimonio.Text = vDataView.Item(0).Item("CHIP")
            lbl_Acessorio.Text = vDataView.Item(0).Item("Acessorio")

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

