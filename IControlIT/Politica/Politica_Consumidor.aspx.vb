
Public Class Politica_Consumidor
    Inherits System.Web.UI.Page
    Dim WS_Politica As New WS_GUA_Politica.WSPolitica
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo( _
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing, _
                "Orçamento", _
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))

            '-----voltar
            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Page.SetFocus(txtDescricao)
            Page.Form.DefaultButton = btExecutar.UniqueID
            Call Master.Localizar(Nothing, Nothing)

            '-----monta detalhamento do botao dinamico
            dtgLista.DataSource = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Botoes_Dinamicos_Cota", Nothing, Nothing, Nothing, Nothing, Nothing)
            dtgLista.DataBind()

            If dtgLista.Items.Count > 0 Then
                btAlerta.Visible = True
            End If
        End If
    End Sub

    Protected Sub btExecutar_Click(sender As Object, e As EventArgs) Handles btExecutar.Click
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        Session("DataSet") = WS_Politica.Politica_Consumidor(Session("Conn_Banco"), Nothing, Nothing, oConfig.ValidaCampo(txtDescricao.Text), Nothing, Nothing, Nothing, Nothing, "sp_Consulta_Politica", True)
        dtgPolitica.DataSource = Session("DataSet")
        dtgPolitica.DataBind()
        Call monta_Ativo_Tipo()

        If Session("DataSet").tables(0).rows.count = 0 Then
            Call Master.Registro_Salvo("* É necessário informar um valor inicial para cota de consumo no cadastro de consumidor.")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
        End If
    End Sub

    Protected Sub dtgPolitica_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPolitica.PageIndexChanged
        dtgPolitica.CurrentPageIndex = e.NewPageIndex
        dtgPolitica.DataSource = Session("DataSet")
        dtgPolitica.DataBind()
        Call monta_Ativo_Tipo()
    End Sub

    Protected Sub btDesativa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgPolitica") + 24, 2), System.Int32)

        Dim v_cboAtivo_Tipo As DropDownList
        v_cboAtivo_Tipo = dtgPolitica.Items(i).Cells(3).Controls(1)

        '-----deleta registro
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        WS_Politica.Politica_Consumidor(Session("Conn_Banco"),
                                            dtgPolitica.Items(i).Cells(4).Text,
                                            IIf(v_cboAtivo_Tipo.SelectedValue = "", Nothing, v_cboAtivo_Tipo.SelectedValue),
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Nothing,
                                            Session("Id_Usuario"),
                                            "sp_SE",
                                            False)
        '-----zera paginacao
        dtgPolitica.CurrentPageIndex = Nothing

        '-----refresh de consulta
        Session("DataSet") = WS_Politica.Politica_Consumidor(Session("Conn_Banco"), Nothing, Nothing, oConfig.ValidaCampo(dtgPolitica.Items(i).Cells(0).Text), Nothing, Nothing, Nothing, Nothing, "sp_Consulta_Politica", True)
        dtgPolitica.DataSource = Session("DataSet")
        dtgPolitica.DataBind()
        Call monta_Ativo_Tipo()

        '-----recarrega lista agrupada de tipo de ativo sem cota
        dtgLista.DataSource = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Botoes_Dinamicos_Cota", Nothing, Nothing, Nothing, Nothing, Nothing)
        dtgLista.DataBind()
    End Sub

    Protected Sub btInsere_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim v_btSalvar As ImageButton = sender
        Dim vText As System.String = v_btSalvar.ClientID.ToString
        Dim i As System.Int32 = CType(Mid(vText, vText.IndexOf("dtgPolitica") + 22, 2), System.Int32)

        WS_Politica.Politica_Consumidor(Session("Conn_Banco"), _
                                            dtgPolitica.Items(i).Cells(4).Text, _
                                            Nothing, _
                                            Nothing, _
                                            0, _
                                            Nothing, _
                                            Nothing, _
                                            Session("Id_Usuario"), _
                                            "sp_Insere_Politica_Ativo_Tipo", _
                                            False)
        '-----zera paginacao
        dtgPolitica.CurrentPageIndex = Nothing

        '-----refresh de consulta
        Session("DataSet") = WS_Politica.Politica_Consumidor(Session("Conn_Banco"), Nothing, Nothing, oConfig.ValidaCampo(dtgPolitica.Items(i).Cells(0).Text), Nothing, Nothing, Nothing, Nothing, "sp_Consulta_Politica", True)
        dtgPolitica.DataSource = Session("DataSet")
        dtgPolitica.DataBind()
        Call monta_Ativo_Tipo()

        '-----recarrega lista agrupada de tipo de ativo sem cota
        dtgLista.DataSource = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Botoes_Dinamicos_Cota", Nothing, Nothing, Nothing, Nothing, Nothing)
        dtgLista.DataBind()
    End Sub

    Public Sub monta_Ativo_Tipo()
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        If dtgPolitica.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_cbo_Tipo_Ativo As DropDownList

        For i = 0 To dtgPolitica.Items.Count - 1
            v_cbo_Tipo_Ativo = dtgPolitica.Items(i).Cells(3).Controls(1)
            oConfig.CarregaCombo(v_cbo_Tipo_Ativo, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Ativo_Tipo", Nothing))
            v_cbo_Tipo_Ativo.SelectedValue = dtgPolitica.Items(i).Cells(5).Text
        Next i
    End Sub

    Protected Sub dtgLista_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dtgLista.SelectedIndexChanged
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        '-----lista ativo sem cota
        hdfAtivo_Tipo.Value = dtgLista.SelectedItem.Cells(3).Text
        vdataset = WS_Politica.Politica_Consumidor(Session("Conn_Banco"), _
                                                    Nothing, _
                                                    hdfAtivo_Tipo.Value, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    Nothing, _
                                                    "sp_Lista_Ativo_Sem_Politica", True)
        dtgDetalhe.DataSource = vdataset
        dtgDetalhe.DataBind()
    End Sub

    Protected Sub dtgDetalhe_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dtgDetalhe.SelectedIndexChanged
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        '-----grava politica inicial
        WS_Politica.Politica_Consumidor(Session("Conn_Banco"), _
                                        dtgDetalhe.SelectedItem.Cells(3).Text, _
                                        dtgDetalhe.SelectedItem.Cells(4).Text, _
                                        Nothing, _
                                        0, _
                                        Nothing, _
                                        Nothing, _
                                        Session("Id_Usuario"), _
                                        "sp_Insere_Politica_Ativo_Tipo", _
                                        False)

        '-----lista ativo para cadastrar cota
        Session("DataSet") = WS_Politica.Politica_Consumidor(Session("Conn_Banco"), _
                                                               Nothing, _
                                                               Nothing, _
                                                               oConfig.ValidaCampo(dtgDetalhe.SelectedItem.Cells(2).Text), _
                                                               Nothing, _
                                                               Nothing, _
                                                               Nothing, _
                                                               Nothing, _
                                                               "sp_Consulta_Politica", _
                                                               True)
        dtgPolitica.DataSource = Session("DataSet")
        dtgPolitica.DataBind()
        Call monta_Ativo_Tipo()

        '-----recarrega lista de ativo sem cota
        vdataset = WS_Politica.Politica_Consumidor(Session("Conn_Banco"), _
                                            Nothing, _
                                            hdfAtivo_Tipo.Value, _
                                            Nothing, _
                                            Nothing, _
                                            Nothing, _
                                            Nothing, _
                                            Nothing, _
                                            "sp_Lista_Ativo_Sem_Politica", True)
        dtgDetalhe.DataSource = vdataset
        dtgDetalhe.DataBind()

        '-----recarrega lista agrupada de tipo de ativo sem cota
        dtgLista.DataSource = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Botoes_Dinamicos_Cota", Nothing, Nothing, Nothing, Nothing, Nothing)
        dtgLista.DataBind()

        pnlMsg.Visible = False
    End Sub

    Protected Sub dtgPolitica_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dtgPolitica.SelectedIndexChanged
        pnlMsg.Visible = False
    End Sub

    Protected Sub btFecharCota_Click(sender As Object, e As EventArgs) Handles btFecharCota.Click
        pnlMsg.Visible = False
    End Sub

    Protected Sub btSalvar_Click(sender As Object, e As EventArgs)
        WS_Politica.Credentials = System.Net.CredentialCache.DefaultCredentials
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        If dtgPolitica.Items.Count = 0 Then Exit Sub
        Dim i As System.Int32
        Dim v_txtValor As TextBox
        Dim v_chkMarcar As CheckBox
        Dim v_cboAtivo_Tipo As DropDownList
        Dim v_Valida As System.Int32 = 0

        '----validade valor
        For i = 0 To dtgPolitica.Items.Count - 1
            v_txtValor = dtgPolitica.Items(i).Cells(2).Controls(1)
            If IsNumeric(v_txtValor.Text) = False Then
                v_Valida = 1
            End If
            If v_Valida = 0 Then
                If CType(v_txtValor.Text, Double) = 0 Then
                    v_Valida = 1
                End If
            End If
        Next i
        If v_Valida = 1 Then
            Call Master.Registro_Salvo("Valor da cota não pode ser (0).")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
            Exit Sub
        End If

        '-----grava cota
        For i = 0 To dtgPolitica.Items.Count - 1
            v_txtValor = dtgPolitica.Items(i).Cells(2).Controls(1)
            v_chkMarcar = dtgPolitica.Items(i).Cells(1).Controls(1)
            v_cboAtivo_Tipo = dtgPolitica.Items(i).Cells(3).Controls(1)

            If v_cboAtivo_Tipo.SelectedValue = "" Then Exit Sub

            WS_Politica.Politica_Consumidor(Session("Conn_Banco"),
                                                dtgPolitica.Items(i).Cells(4).Text,
                                                v_cboAtivo_Tipo.SelectedValue,
                                                Nothing,
                                                IIf(Trim(v_txtValor.Text) = "", Nothing, v_txtValor.Text),
                                                IIf(v_chkMarcar.Checked = True, 2, 1),
                                                Nothing,
                                                Session("Id_Usuario"),
                                                "sp_SM",
                                                False)
        Next i

        '-----recarrega lista agrupada de tipo de ativo sem cota
        dtgLista.DataSource = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Detalha_Botoes_Dinamicos_Cota", Nothing, Nothing, Nothing, Nothing, Nothing)
        dtgLista.DataBind()

        '-----registro salvo ok
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalRegistroSalvo');", True)
    End Sub

    Protected Sub btExportar_Click(sender As Object, e As EventArgs)
        '-----comentado = todos ou posso selecionar um tipo de modelo por vez
        Dim Tipo As System.String = Nothing
        '-----nome do arquivo a ser exportado
        Dim Descricao As System.String = "Cota_Consumo"
        '-----campos a ser exportado modelo (xxxx;xxxxx;xxxx). quando null sistema gera com base no dataset
        Dim Campo As System.String = "Nm_Consumidor;Valor_Politica;Marca_Ligacao;Nm_Ativo_Tipo"
        '-----abre pnl de exportacao
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "window.open('../Exportacao/Exporta.aspx?" &
                                            "Descricao=" & Descricao &
                                            "&Campo=" & Campo &
                                            "&Tipo=" & Tipo &
                                            "','','resizable=yes, menubar=yes, scrollbars=no," &
                                            "height=768px, width=1024px, top=10, left=10'" &
                                            ")", True)
    End Sub

    Protected Sub btAlerta_Click(sender As Object, e As EventArgs)
        pnlMsg.Visible = True
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub
End Class

