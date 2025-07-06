Imports System.Runtime.Remoting

Public Class Fatura_Nota_Fiscal
    Inherits System.Web.UI.Page
    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim oConfig As New cls_Config
    Dim vdataset As New Data.DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vTraduzir As New cls_Traducao
            '-----verifica permissao de acesso a tela
            Call vTraduzir.Permissao_Tela(Page.Request.Path)
            '-----traduz e passa titulo para master page
            Call Master.Titulo(
                IIf(vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma")) = Nothing,
                "Cadastro de Notas Fiscais",
                vTraduzir.Traduzir(Session("Conn_Banco"), Master.FindControl("ContentPlaceHolder1"), Page.Request.Path, Session("Id_Idioma"))))
            '-----voltar

            Call Master.Voltar("javascript:history.go(-1);", Nothing)
            Call Master.Localizar("sp_Drop_Fatura", Page.AppRelativeVirtualPath.ToString)

            oConfig.CarregaCombo(cboFatura, WS_Cadastro.Fatura_Nota_Fiscal(Session("Conn_Banco"), "sp_Drop_Fatura", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True))
            oConfig.CarregaCombo(cboCentroCusto, WS_Cadastro.Fatura_Nota_Fiscal(Session("Conn_Banco"), "sp_Drop_Centro_Custo", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True))

        End If
    End Sub

    Protected Sub btBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBuscar.Click
        If cboFatura Is Nothing Or cboCentroCusto Is Nothing Then
            Exit Sub
        End If

        vdataset = WS_Cadastro.Fatura_Nota_Fiscal(Session("Conn_Banco"), "sp_Fatura", cboFatura.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, True)
        txtIdFatura.Value = vdataset.Tables(0).Rows(0).Item("Id_Fatura")
        txtNrFatura.Text = vdataset.Tables(0).Rows(0).Item("Nr_Fatura")
        txtDtLote.Text = vdataset.Tables(0).Rows(0).Item("Dt_Lote")
        txtVrFatura.Text = vdataset.Tables(0).Rows(0).Item("Vr_Fatura")
        txtNmFaturaParametro.Text = vdataset.Tables(0).Rows(0).Item("Nm_Fatura_Parametro")

        ' Verificar se cboCentroCusto foi selecionado antes de buscar os detalhes
        If Not String.IsNullOrEmpty(cboCentroCusto.SelectedValue) Then
            vdataset = WS_Cadastro.Fatura_Nota_Fiscal(Session("Conn_Banco"), "sp_Centro_custo", Nothing, cboCentroCusto.SelectedValue, Nothing, Nothing, Nothing, Nothing, True)
            txtIdCentroCusto.Value = vdataset.Tables(0).Rows(0).Item("Id_Centro_Custo")
            txtNmCentroCusto.Text = vdataset.Tables(0).Rows(0).Item("Nm_Centro_Custo")
            txtCdCentroCusto.Text = vdataset.Tables(0).Rows(0).Item("Cd_Centro_Custo")
        Else
            txtIdCentroCusto.Value = ""
            txtNmCentroCusto.Text = ""
            txtCdCentroCusto.Text = ""
        End If

        dvDetalhes.Visible = True
        dvCadastro.Visible = True
    End Sub


    'Protected Sub pct_Calc(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPctNotaFiscal.TextChanged
    '    txtVrNotaFiscal.Text = (txtVrFatura.Text * txtPctNotaFiscal.Text) / 100
    'End Sub

    Protected Sub btSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSalvar.Click
        If String.IsNullOrEmpty(txtNrNotaFiscal.Text) Or String.IsNullOrEmpty(txtPctNotaFiscal.Text) Then
            Exit Sub
        End If

        Dim idCentroCusto As Integer
        If Not String.IsNullOrEmpty(txtIdCentroCusto.Value) Then
            idCentroCusto = Integer.Parse(txtIdCentroCusto.Value)
        Else
            idCentroCusto = Nothing
        End If

        WS_Cadastro.Fatura_Nota_Fiscal(Session("Conn_Banco"),
                                       "sp_SM",
                                       txtIdFatura.Value,
                                       idCentroCusto,
                                       txtNrNotaFiscal.Text,
                                       Nothing,
                                       Convert.ToSingle(txtPctNotaFiscal.Text),
                                       Session("Id_Usuario"),
                                       False)

        pnlConfirmacao.Visible = True
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "disableScrolling();", True)
    End Sub

    Protected Sub pnlConfirmacao_Resposta_Sim(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSim.Click
        txtNrNotaFiscal.Text = ""
        txtPctNotaFiscal.Text = ""
        'txtVrNotaFiscal.Text = ""

        pnlConfirmacao.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
    End Sub

    Protected Sub pnlConfirmacao_Resposta_Nao(ByVal sender As Object, ByVal e As System.EventArgs) Handles btNao.Click
        Call limpar()

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "enableScrolling();", True)
    End Sub

    Private Sub limpar()
        pnlConfirmacao.Visible = False
        dvDetalhes.Visible = False
        dvCadastro.Visible = False
        cboFatura.SelectedValue = Nothing
        cboCentroCusto.SelectedValue = Nothing
        txtNrFatura.Text = ""
        txtDtLote.Text = ""
        txtVrFatura.Text = ""
        txtNmFaturaParametro.Text = ""
        txtNrNotaFiscal.Text = ""
        txtPctNotaFiscal.Text = ""
        txtNmCentroCusto.Text = ""
        txtCdCentroCusto.Text = ""
        txtIdCentroCusto.Value = Nothing
        txtIdFatura.Value = Nothing
    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs) Handles btVoltar.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "javascript:history.go(-1);", True)
    End Sub

    Protected Sub btLimpar_Click(sender As Object, e As EventArgs) Handles btLimpar.Click
        Call limpar()
    End Sub
End Class