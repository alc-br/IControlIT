
Public Class Arquivo_Usuario
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim vDataSet As New Data.DataSet
    Dim oConfig As New cls_Config

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Carga de usuário - Visualização",
                                                        False)
        Try
            If Not Page.IsPostBack Then
                WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

                Dim vTraduzir As New cls_Traducao
                '-----verifica permissao de acesso a tela
                Call vTraduzir.Permissao_Tela(Page.Request.Path)
                '-----traduz e passa titulo para master page
                Call Master.Titulo(
                    IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                    "Importar dados de Usuário",
                    vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

                '-----voltar
                Call Master.Voltar("javascript:history.go(-1);", Nothing)
                Call Master.Localizar(Nothing, Nothing)
            End If
        Catch my_e As Exception
            Console.WriteLine("Third handler")
        End Try
    End Sub

    Public Sub Campo_Usuario()
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vDataSet = WS_Modulo.Carga_Usuario(Session("Conn_Banco"),
                                    "sp_Drop_Campo_Usuario",
                                    Nothing,
                                    Nothing,
                                    Nothing,
                                    "usuario",
                                    True)

        If dtgDado.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_cbo_Campo_Usuario_Grid As DropDownList

        For i = 0 To dtgDado.Items.Count - 1
            v_cbo_Campo_Usuario_Grid = dtgDado.Items(i).Cells(1).Controls(1)
            oConfig.CarregaCombo(v_cbo_Campo_Usuario_Grid, vDataSet)
            v_cbo_Campo_Usuario_Grid.SelectedValue = dtgDado.Items(i).Cells(2).Text
        Next i
    End Sub

    Protected Sub btOK_Arquivo_Click(sender As Object, e As EventArgs) Handles btOK_Arquivo.Click
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Carga de usuário - Click btOK_Arquivo",
                                                        False)
        pnlVerifica_Arquivo.Visible = False
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Carga de usuário - Click btSalvar",
                                                        False)

        Dim i As System.Int32
        Dim v_txtCampoFixo As TextBox
        Dim v_cboCampousuario As DropDownList

        '-----grava compo fixo de texto
        For i = 0 To dtgCampo.Items.Count - 1
            v_txtCampoFixo = dtgCampo.Items(i).Cells(3).Controls(1)

            vDataSet = WS_Modulo.Carga_Usuario(Session("Conn_Banco"),
                                                "sp_SM_Campo_Fixo",
                                                dtgCampo.Items(i).Cells(0).Text,
                                                Nothing,
                                                oConfig.ValidaCampo(v_txtCampoFixo.Text),
                                                "usuario",
                                                False)
        Next i

        '-----grava campo para importacao
        If dtgDado.Items.Count > 0 Then
            vDataSet = WS_Modulo.Carga_Usuario(Session("Conn_Banco"),
                                                "sp_SM_Campo_Usuario_Limpar",
                                                Nothing, Nothing, Nothing, "usuario",
                                                False)
        End If
        For i = 0 To dtgDado.Items.Count - 1
            v_cboCampousuario = dtgDado.Items(i).Cells(1).Controls(1)

            vDataSet = WS_Modulo.Carga_Usuario(Session("Conn_Banco"),
                                                "sp_SM_Campo_Usuario",
                                                oConfig.ValidaCampo(v_cboCampousuario.SelectedItem.Value),
                                                dtgDado.Items(i).Cells(0).Text,
                                                Nothing,
                                                "usuario",
                                                False)
        Next i

        dtgCampo.DataSource = WS_Modulo.Carga_Usuario(Session("Conn_Banco"),
                                                "sp_Monta_Campo",
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                "usuario",
                                                True)
        dtgCampo.DataBind()

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btConfiguracao_Click(sender As Object, e As EventArgs)

        'WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        'WS_Modulo.Timeout = 3600000

        ''-----valida informações e carrega passando o parametro @pCampo_Arquivo = carregar
        'dtgCampo.DataSource = WS_Modulo.Carga_Usuario(Session("Conn_Banco"),
        '                                                "sp_Valida_Importar",
        '                                                Nothing,
        '                                                "carregar",
        '                                                Nothing,
        '                                                "usuario",
        '                                                True)
    End Sub
    Protected Sub btUpload_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Carga de usuário - Click btUpload",
                                                        False)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
                                                    "window.open('../Carga/Upload.aspx" &
                                                    "','','resizable=yes, menubar=yes, scrollbars=no," &
                                                    "height=768px, width=1024px, top=10, left=10'" &
                                                    ")", True)
    End Sub

    Protected Sub btImportar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Carga de usuário - btImportar",
                                                        False)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
                                                    "window.open('../Carga/Importar.aspx" &
                                                    "','','resizable=yes, menubar=yes, scrollbars=no," &
                                                    "height=768px, width=1024px, top=10, left=10'" &
                                                    ")", True)
    End Sub
    Protected Sub btAtualizar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Carga de usuário - btAtualizar",
                                                        False)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
                                                    "window.open('../Carga/Atualizar.aspx" &
                                                    "','','resizable=yes, menubar=yes, scrollbars=no," &
                                                    "height=768px, width=1024px, top=10, left=10'" &
                                                    ")", True)
    End Sub
    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Carga de usuário - btExportar",
                                                        False)
        '-----monta exportacao
        Session("DataSet") = WS_Modulo.Carga_Usuario(Session("Conn_Banco"),
                                                    "sp_Arquivo_RH_Sistema",
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    Nothing,
                                                    True)

        Dim Descricao As System.String = "Template_RH"
        Dim Campo As System.String = "Nome;Tipo;Matricula;E_Mail;Empresa_Contratada;Cod_Cargo;Cargo;Cod_Empresa;Empresa;Cod_Filial;Filial;Cod_Centro_Custo;Centro_Custo;Cod_Departamento;Departamento;Cod_Setor;Setor;Cod_Secao;Secao;Status_User;Login;Data_Admissao;Data_Demissao;Tipo_Gestor;Matricula_Chefia_Imediata;Valor_Politica;Marca_Particular_Politica;Tipo_Ativo_Politica"

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
                                            "window.open('../Exportacao/Exporta.aspx?Descricao=" & Descricao & "&Campo=" & Campo &
                                            "','','resizable=yes, menubar=yes, scrollbars=no," &
                                            "height=768px, width=1024px, top=10, left=10'" &
                                            ")", True)
    End Sub
    Protected Sub btAbrir_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Carga de usuário - Click btAbrir",
                                                        False)
        'Try
        '-----lista padrao do arquivo
        dtgCampo.DataSource = WS_Modulo.Carga_Usuario(Session("Conn_Banco"),
                                                            "sp_Monta_Campo",
                                                            Nothing,
                                                            Nothing,
                                                            Nothing,
                                                            "usuario",
                                                            True)
        dtgCampo.DataBind()

        '-----lista configuracao do arquivo
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Modulo.Timeout = 3600000

        dtgDado.DataSource = WS_Modulo.Carga_Usuario(Session("Conn_Banco"),
                                                        "sp_Coluna_Arquivo",
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        "usuario",
                                                        True)
        dtgDado.DataBind()
        Campo_Usuario()

        lblMatrix.Visible = True
        lblArquivo.Visible = True

        'Catch my_e As Exception
        '    Console.WriteLine("Third handler")
        'End Try
    End Sub
    Protected Sub btSincronizaCont_Click(sender As Object, e As EventArgs)
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Carga de usuário - Click btSincronizaCont",
                                                        False)
        WS_Modulo.Timeout = 3600000

        '-----valida informações
        dtgCampo.DataSource = WS_Modulo.Carga_Usuario(Session("Conn_Banco"),
                                                        "sp_Valida_Importar",
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        "usuario",
                                                        True)
        dtgCampo.DataBind()

        '-----lista os 10 primeiros registro para importacao
        dtgDetalhe.DataSource = WS_Modulo.Carga_Usuario(Session("Conn_Banco"),
                                                "sp_Lista_10_Registros",
                                                Nothing,
                                                Nothing,
                                                Nothing,
                                                "usuario",
                                                True)
        dtgDetalhe.DataBind()

        lblTeste.Visible = True
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        $"Tela Carga de usuário - Click btVoltar",
                                                        False)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class
