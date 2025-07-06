
Partial Public Class Termo_Devolucao_Ativo
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
            imgLogo.ImageUrl = vDataSet.Tables(0).Rows(0).Item("Logo")
            vEmpresa = vDataSet.Tables(0).Rows(0).Item("Empresa")

            '-----lista xml
            vDataSetXML.ReadXml(App_Path() & "Termo/Base/" & "Dados_Default.xml".Replace("Default", vEmpresa))

            lbl_titulo.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Titulo").ToString
            lbl_txtTexto1.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto1").ToString & vDataSet.Tables(0).Rows(0).Item("Empresa") & vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto2").ToString
            lbl_txtLinha.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha").ToString
            lbl_txtOperadora.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Operadora").ToString
            lbl_txtLocalidade.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Filial").ToString
            lbl_txtDadosUsuario.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("DadosUsuario").ToString

            lbl_txtCentroCusto.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("CentroCusto").ToString
            lbl_txtMatricula.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Matricula").ToString
            lbl_txtDepartamento.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Departamento").ToString
            lbl_txtCiente.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Ciente").ToString
            lbl_txtDadosAparelho.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("DadosAparelho").ToString
            lbl_txtMarcaModelo.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("MarcaModelo").ToString
            lbl_txtImei.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("ImeiAparelho").ToString
            lbl_txtAcessorios.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Acessorios").ToString
            lbl_txtObs.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Obs").ToString
            lbl_txtAtivo_Tipo.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("AtivoTipo").ToString

            lbl_txtImei.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("ImeiAparelho").ToString
            lbl_txtChipAparelho.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("ChipAparelho").ToString
            lbl_txtAcessorios.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Acessorio").ToString

            lbl_txtData1.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Data_1").ToString
            lbl_Data1.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Data_Texto_1").ToString

            lbl_Ass_1.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_1").ToString
            lbl_Ass_2.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_2").ToString
            lbl_Ass_3.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_3").ToString

            lbl_Linha_Ass_1.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha_Assinatura_1").ToString
            lbl_Linha_Ass_2.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha_Assinatura_2").ToString
            lbl_Linha_Ass_3.Text = vDataSetXML.Tables("Devolucao_" & Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha_Assinatura_3").ToString

            vDataSet = WS_Cadastro.Ativo(Session("Conn_Banco"), Request("Id_Ativo"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                      Nothing, Nothing, Nothing, Nothing, Nothing,
                      Nothing, "sd_SL_Termo", True, Nothing, Nothing, Nothing, Nothing, Nothing)

            Dim vDataView As System.Data.DataView
            vDataView = New Data.DataView(vDataSet.Tables(0), "Id_Consumidor = " & Request("Id_Consumidor"), Nothing, Data.DataViewRowState.OriginalRows)

            lbl_linha.Text = vDataView.Item(0).Item("Linha")
            lbl_Ativo_Tipo.Text = vDataView.Item(0).Item("Tipo_Ativo")
            lbl_operadora.Text = vDataView.Item(0).Item("Nm_Conglomerado")
            lbl_localidade.Text = vDataView.Item(0).Item("Nm_Filial")
            lbl_Colaborador.Text = vDataView.Item(0).Item("Nm_Consumidor")
            lbl_matricula.Text = vDataView.Item(0).Item("Matricula")
            lbl_centroCusto.Text = vDataView.Item(0).Item("Centro_Custo")
            lbl_departamento.Text = vDataView.Item(0).Item("Departamento")

            lbl_marcaModelo.Text = vDataView.Item(0).Item("Nm_Modelo_Fabricante")
            lbl_Imei.Text = IIf(vDataView.Item(0).Item("Nr_Aparelho") = "", vDataView.Item(0).Item("IMEI"), vDataView.Item(0).Item("Nr_Aparelho"))

            lbl_ChipAparelho.Text = vDataView.Item(0).Item("CHIP")
            lbl_acessorios.Text = vDataView.Item(0).Item("Acessorio")

        End If
    End Sub

    Private Function App_Path() As String
        Dim sPath As System.String
        sPath = System.AppDomain.CurrentDomain.BaseDirectory()
        sPath = sPath & IIf(Right(sPath, 1) = "/", "", "/").ToString
        Return sPath
    End Function

    Protected Sub btImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btImprimir.Click
        btImprimir.Style.Add("Opacity", "0")
        Page.ClientScript.RegisterStartupScript(Me.GetType, "ClientScript", "<script>window.print();</script>")
    End Sub

End Class

