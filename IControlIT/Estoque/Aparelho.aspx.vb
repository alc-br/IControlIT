
Imports iTextSharp.text

Public Class Aparelho
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Estoque As New WS_GUA_Estoque.WSEstoque

    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Cadastro de Aparelho ",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtNumeroAparelho)
            Page.Form.DefaultButton = btSalvar.UniqueID
            Call Master.Localizar("sp_Drop_Estoque", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboEnderecoEntrega, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Estoque_Endereco_Entrega", Nothing))
            oConfig.CarregaCombo(cboAtivoTipo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo", Nothing))
            oConfig.CarregaCombo(cboEstoqueAparelhoStatus, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Estoque_Aparelho_Status", Nothing))
            oConfig.CarregaCombo(cboUsuarioEstoque, WS_Estoque.Estoque(Session("Conn_Banco"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_Consumidor", True))
            oConfig.CarregaCombo(cboConglomerado, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Conglomerado", Nothing))
            oConfig.CarregaCombo(cboNotaFiscal, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Estoque_Nota_Fiscal", Nothing))

            If Not Request("ID") = Nothing Then
                vdataset = WS_Estoque.Aparelho(Session("Conn_Banco"), Request("ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sp_SL_ID", True)

                btPDF.OnClientClick = "window.open('../PDF/Lista_PDF.aspx?pRegistro=" & vdataset.Tables(0).Rows(0).Item("Id_Aparelho") & "&pTabela=Estoque_Aparelho','','resizable=yes, menubar=yes, scrollbars=no,height=768px, width=1024px, top=10, left=10')"

                txtIdentificacao.Text = vdataset.Tables(0).Rows(0).Item("Id_Aparelho")
                txtNumeroAparelho.Text = vdataset.Tables(0).Rows(0).Item("Nr_Aparelho")
                txtNumeroAparelho2.Text = vdataset.Tables(0).Rows(0).Item("Nr_Aparelho_2")
                txtLinhaSolicitacao.Text = vdataset.Tables(0).Rows(0).Item("Nr_Linha_Solicitacao")
                txtNumeroChamado.Text = vdataset.Tables(0).Rows(0).Item("Nr_Chamado")
                cboAparelhoTipo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Aparelho_Tipo")
                cboAtivoTipo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Tipo")
                cboEstoqueAparelhoStatus.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Estoque_Aparelho_Status")
                txtObservacao.Text = vdataset.Tables(0).Rows(0).Item("Observacao")
                cboEnderecoEntrega.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Estoque_Endereco_Entrega")
                cboNotaFiscal.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Estoque_Nota_Fiscal")
                cboUsuarioEstoque.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Consumidor")
                cboConglomerado.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Conglomerado")
                txtDescricaoLinha.Text = vdataset.Tables(0).Rows(0).Item("Nr_Ativo")
                txtIndentificacaoModal.Text = vdataset.Tables(0).Rows(0).Item("Id_Aparelho")

                CkbCarregador.Checked = vdataset.Tables(0).Rows(0).Item("Ck_Carregador")
                CkbCabo.Checked = vdataset.Tables(0).Rows(0).Item("Ck_Cabousb")
                CkbFone.Checked = vdataset.Tables(0).Rows(0).Item("Ck_Fone")
                CkbPelicula.Checked = vdataset.Tables(0).Rows(0).Item("Ck_Pelicula")
                CkbCapa.Checked = vdataset.Tables(0).Rows(0).Item("Ck_Capaprotecao")

                btImprimir.Visible = IIf(cboEnderecoEntrega.SelectedValue = Nothing, False, True)
                txtNumeroAparelho.Enabled = False
                cboAparelhoTipo.Enabled = True

                '-----monta modelo do ativo
                If Not cboAtivoTipo.SelectedValue = Nothing Then
                    oConfig.CarregaCombo(cboAtivoModelo, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Ativo_Modelo_Id_Ativo_Tipo", cboAtivoTipo.SelectedValue, Nothing))
                    cboAtivoModelo.SelectedValue = vdataset.Tables(0).Rows(0).Item("Id_Ativo_Modelo")
                End If
            End If
        End If
    End Sub

    Public Sub limpar()
        oConfig.LimpaText(Master.FindControl("ContentPlaceHolder1"))
        cboAparelhoTipo.SelectedValue = Nothing
        cboAtivoTipo.SelectedValue = Nothing
        cboEstoqueAparelhoStatus.SelectedValue = Nothing
        cboAtivoModelo.SelectedValue = Nothing
        cboUsuarioEstoque.SelectedValue = Nothing
        cboConglomerado.SelectedValue = Nothing
        cboEnderecoEntrega.SelectedValue = Nothing
        cboNotaFiscal.SelectedValue = Nothing

        txtNumeroAparelho.Enabled = True
        cboAparelhoTipo.Enabled = True
        CkbCabo.Checked = False
        CkbCapa.Checked = False
        CkbCarregador.Checked = False
        CkbFone.Checked = False
        CkbPelicula.Checked = False
        lbSelecioneAcessorio.Visible = False

    End Sub

    Protected Sub cboAtivoTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAtivoTipo.SelectedIndexChanged
        If cboAtivoTipo.SelectedValue = Nothing Then Exit Sub
        '-----monta modelo do ativo
        oConfig.CarregaCombo(cboAtivoModelo, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Ativo_Modelo_Id_Ativo_Tipo", cboAtivoTipo.SelectedValue, Nothing))
    End Sub

    Protected Sub btImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btImprimir.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
                                             "window.open('../Termo/Correio.aspx?Id_Aparelho=" & txtIdentificacao.Text &
                                             "','','resizable=yes, menubar=yes, scrollbars=no," &
                                             "height=768px, width=1024px, top=10, left=10'" &
                                             ")", True)
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs)
        Call limpar()
    End Sub
    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        '-----verifica se colocou observacao
        If Trim(txtObservacaoObrigatoria.Text) = "" Then Exit Sub
        'txtObservacao.Text = txtObservacaoObrigatoria.Text

        '-----nao insere registro quando descricao so for numerica
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim script As String = "
    console.log('TAMANHOS DOS CAMPOS:');
    console.log('txtNumeroAparelho: ' + '" & txtNumeroAparelho.Text.Trim().Length & "');
    console.log('txtNumeroAparelho2: ' + '" & txtNumeroAparelho2.Text.Trim().Length & "');
    console.log('txtLinhaSolicitacao: ' + '" & txtLinhaSolicitacao.Text.Trim().Length & "');
    console.log('txtNumeroChamado: ' + '" & txtNumeroChamado.Text.Trim().Length & "');
    console.log('ObservacaoObrigatoria: ' + '" & txtObservacaoObrigatoria.Text.Trim().Length & "');
"

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "logCampos", script, True)



        If txtNumeroAparelho.Text = txtNumeroAparelho2.Text Then
            Call Master.Registro_Salvo("* Os dois números do aparelho não podem ser iguais.")
        Else
            txtIdentificacao.Text = WS_Estoque.Aparelho(Session("Conn_Banco"),
                                                        oConfig.ValidaCampo(txtIdentificacao.Text),
                                                        oConfig.ValidaCampo(Left(txtNumeroAparelho.Text.Trim(), 50)),
                                                        oConfig.ValidaCampo(Left(txtNumeroAparelho2.Text.Trim(), 50)),
                                                        oConfig.ValidaCampo(Left(txtLinhaSolicitacao.Text.Trim(), 50)),
                                                        oConfig.ValidaCampo(Left(txtNumeroChamado.Text.Trim(), 50)),
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        oConfig.ValidaCampo(cboNotaFiscal.SelectedValue),
                                                        oConfig.ValidaCampo(cboConglomerado.SelectedValue),
                                                        oConfig.ValidaCampo(cboAparelhoTipo.SelectedValue),
                                                        oConfig.ValidaCampo(cboAtivoTipo.SelectedValue),
                                                        oConfig.ValidaCampo(cboAtivoModelo.SelectedValue),
                                                        oConfig.ValidaCampo(cboEstoqueAparelhoStatus.SelectedValue),
                                                        oConfig.ValidaCampo(Left(txtObservacaoObrigatoria.Text.Trim(), 300)),
                                                        Nothing,
                                                        oConfig.ValidaCampo(cboEnderecoEntrega.SelectedValue),
                                                        oConfig.ValidaCampo(cboUsuarioEstoque.SelectedValue),
                                                        oConfig.ValidaCheckbox(CkbCarregador.Checked),
                                                        oConfig.ValidaCheckbox(CkbCabo.Checked),
                                                        oConfig.ValidaCheckbox(CkbFone.Checked),
                                                        oConfig.ValidaCheckbox(CkbPelicula.Checked),
                                                        oConfig.ValidaCheckbox(CkbCapa.Checked),
                                                        Session("Id_Usuario"),
                                                        "sp_SM",
                                                        False).Tables(0).Rows(0).Item(0)

            '-----registro salvo ok
            If txtIdentificacao.Text = 0 Then
                '-----registro salvo ok
                If String.IsNullOrEmpty(txtNumeroAparelho2.Text) Then
                    Call Master.Registro_Salvo(lblNumeroAparelho.Text & " - " & txtNumeroAparelho.Text & " já cadastrado.")
                Else
                    Call Master.Registro_Salvo(lblNumeroAparelho.Text & " - " & txtNumeroAparelho.Text & " ou " & txtNumeroAparelho2.Text & " já cadastrado.")
                End If
            Else
                vdataset = WS_Estoque.Aparelho(Session("Conn_Banco"),
                                           IIf(Request("ID") Is Nothing, txtIdentificacao.Text, Request("ID")),
                                           Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                           Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                           Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                           Nothing, Nothing, Nothing, Nothing, Nothing,
                                           "sp_SL_ID", True)

                txtObservacao.Text = vdataset.Tables(0).Rows(0).Item("Observacao")
                txtIndentificacaoModal.Text = vdataset.Tables(0).Rows(0).Item("Id_Aparelho")

                Call Master.Registro_Salvo("Registro salvo com sucesso !")
            End If
        End If

        '-----registro salvo ok
        pnlObservacao.Visible = False
        lbSelecioneAcessorio.Visible = False

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btDesativar_Click(sender As Object, e As EventArgs)
        If Trim(txtIdentificacao.Text) = "" Then Exit Sub

        If Trim(txtObservacao.Text) = "" Then
            Call Master.Registro_Salvo("É necessário informar o motivo da desativação no campo Observação.")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
        Else
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Estoque.Credentials = System.Net.CredentialCache.DefaultCredentials

            WS_Estoque.Aparelho(Session("Conn_Banco"), txtIdentificacao.Text, Nothing, Nothing, Nothing, Nothing, Nothing,
                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, txtObservacao.Text, Nothing, Nothing,
                                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Session("Id_Usuario"), "sp_SE", False)
            Call limpar()
        End If
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btSalvar",
                                                        False)
        pnlRegistro.Visible = False
        pnlObservacao.Visible = True
        txtObservacao.Text = ""
    End Sub

    Protected Sub btCancela_Click(sender As Object, e As EventArgs) Handles btCancela.Click
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btCancela",
                                                        False)
        pnlObservacao.Visible = False
    End Sub

    Protected Sub btFechar_Registro_Click(sender As Object, e As EventArgs) Handles btFechar_Registro.Click
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btFechar_Registro",
                                                        False)
        pnlRegistro.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
    End Sub

    Protected Sub btAbrir_Click(sender As Object, e As EventArgs)
        WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                      Session("Id_Usuario"),
                                                      DateTime.Now,
                                                        "Tela Ativo - Click btAbrir",
                                                        False)
        pnlRegistro.Visible = True
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "disableScrolling();", True)
    End Sub
End Class
