
Partial Public Class Anonimous_Termo_Responsabilidade_Ativo
    Inherits System.Web.UI.Page
    'Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim vDataSet As New Data.DataSet
    Dim vDataSetXML As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            '-----lista holding
            '-----lista holding
            '-----------Sd_TR_RetornarHoldTermoResponsabilidade

            ''----- Confirmar se o token existe e está disponivel para exibir o termo.
            vDataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                            "Sd_TR_RetornarHoldTermoResponsabilidade",
                                            Nothing, 'txtUsuario,
                                            Nothing,
                                            Nothing,
                                            Nothing, 'txtEmail,
                                            Nothing,
                                            Request("Token"), 'Token
                                            Nothing)



            Dim vEmpresa As String
            'vDataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Valida_Usuario", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)
            vEmpresa = vDataSet.Tables(0).Rows(0).Item("Empresa")
            imgLogo.ImageUrl = vDataSet.Tables(0).Rows(0).Item("Logo")
            If vEmpresa = "Fortlev" Then
                imgLogo1.ImageUrl = vDataSet.Tables(0).Rows(0).Item("Logo")
            End If


            '-----lista xml
            vDataSetXML.ReadXml(App_Path() & "Termo/Base/" & "Dados_Default.xml".Replace("Default", vEmpresa))

                lbl_titulo.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Titulo").ToString
                lbl_SubTitulo.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("subtitulo").ToString
                lbl_txtLocalidade.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Filial").ToString
                lbl_txtLinha.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha").ToString
                lbl_txtOperadora.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Operadora").ToString
                lbl_txtMatricula.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Matricula").ToString
                lbl_txtCentroCusto.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("CentroCusto").ToString
                lbl_txtDepartamento.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Departamento").ToString
                lbl_txtData1.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Data_1").ToString
                lbl_txtData2.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Data_2").ToString
                lbl_Data1.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Data_Texto_1").ToString
                lbl_Data2.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Data_Texto_2").ToString
                lbl_txtTexto1.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto1").ToString
                lbl_txtTexto2.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto2").ToString
                lbl_txtTexto3.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3").ToString

                lbl_txtTexto3_1.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_1").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto3_2.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_2").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto3_3.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_3").ToString.Replace("salto_Linha", "<br>")
                llbl_txtTexto3_4.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_4").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto3_5.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_5").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto3_6.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_6").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto3_7.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_7").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto3_8.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_8").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto3_9.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_9").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto3_10.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto3_10").ToString.Replace("salto_Linha", "<br>")

                '------rodape
                lbl_txtTexto9.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto9").ToString.Replace("salto_Linha", "<br>")

                lbl_txtTexto10.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto10").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto11.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto11").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto12.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto12").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto13.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto13").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto14.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto14").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto15.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto15").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto16.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto16").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto17.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto17").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto18.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto18").ToString.Replace("salto_Linha", "<br>")
                lbl_txtTexto19.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto19").ToString.Replace("salto_Linha", "<br>")

                lbl_txtMarcaModeloAparelho.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("MarcaModeloAparelho").ToString
                lbl_txtTipoAtivo.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("TipoAtivo").ToString
                lbl_txtImeiAparelho.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("ImeiAparelho").ToString
                lbl_txtChipAparelho.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("ChipAparelho").ToString
                lbl_txtAcessorio.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Acessorio").ToString

                lbl_txtTexto22.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto22").ToString
                lbl_txtTexto23.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto23").ToString
                lbl_txtTexto24.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Texto24").ToString

                lbl_Ass_1.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_1").ToString
                lbl_Ass_2.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_2").ToString
                lbl_Ass_3.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_3").ToString
                lbl_Ass_4.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_4").ToString
                lbl_Ass_5.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_5").ToString
                lbl_Ass_6.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Assinatura_6").ToString

                If vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha_Assinatura_1").ToString = "______________________________" Then
                    lblLinha_Ass1.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha_Assinatura_1").ToString
                Else
                    imgAss.Visible = True
                    lblLinha_Ass1.Visible = False
                End If

                lblLinha_Ass2.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha_Assinatura_2").ToString
                lblLinha_Ass3.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha_Assinatura_3").ToString
                lblLinha_Ass4.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha_Assinatura_4").ToString
                lblLinha_Ass5.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha_Assinatura_5").ToString
                lblLinha_Ass6.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Linha_Assinatura_6").ToString

                lbl_txtDadosUsuario.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("DadosUsuario").ToString
                lbl_txtCiente.Text = vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("Ciente").ToString & " " & vDataSetXML.Tables(Request("Nm_Ativo_Tipo_Grupo")).Rows(0)("DadosAparelho").ToString

                'vDataSet = WS_Cadastro.Ativo(Session("Conn_Banco"), Request("Id_Ativo"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sd_SL_Termo", True)
                Dim vDataView As System.Data.DataView
                vDataView = New Data.DataView(vDataSet.Tables(0), "Id_Consumidor = " & Request("Id_Consumidor"), Nothing, Data.DataViewRowState.OriginalRows)
                lbl_linha.Text = vDataView.Item(0).Item("Linha")
                lbl_operadora.Text = vDataView.Item(0).Item("Nm_Conglomerado")
                lbl_localidade.Text = vDataView.Item(0).Item("Nm_Filial")
                lbl_matricula.Text = vDataView.Item(0).Item("Matricula")
                lbl_centroCusto.Text = vDataView.Item(0).Item("Centro_Custo")
                lbl_departamento.Text = vDataView.Item(0).Item("Departamento")

                lbl_txtColaborador.Text = vDataView.Item(0).Item("Nm_Consumidor")

                lbl_TipoAtivo.Text = vDataView.Item(0).Item("Tipo_Ativo")
                lbl_MarcaModeloAparelho.Text = vDataView.Item(0).Item("Nm_Modelo_Fabricante")

                lbl_ImeiAparelho.Text = IIf(vDataView.Item(0).Item("Nr_Aparelho") = "", vDataView.Item(0).Item("IMEI"), vDataView.Item(0).Item("Nr_Aparelho"))
                lbl_ChipAparelho.Text = vDataView.Item(0).Item("CHIP")
                lbl_Acessorio.Text = vDataView.Item(0).Item("Acessorio")


                '-----não deixa imprimir termo sem numero do imei
                'If vDataView.Item(0).Item("Nr_Aparelho") = "" And vDataView.Item(0).Item("IMEI") = "" Then
                '    pnlMSG.Visible = True
                'End If
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

