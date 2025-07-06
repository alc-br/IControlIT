Imports System.Data
Public Class Filtro_Acesso
    Inherits System.Web.UI.UserControl

    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo

    Function pPakage() As System.String
        Dim vPakage As System.String = Nothing

        '-----consulta de filtro segue hierarquia do menor para o maior
        If Session("Id_Usuario_Perfil_Acesso") = 4 Or (Session("Id_Usuario_Perfil_Acesso") = 1 And Not cboFilial.SelectedValue = Nothing) Then vPakage = "Filial"
        If Session("Id_Usuario_Perfil_Acesso") = 5 Or (Session("Id_Usuario_Perfil_Acesso") = 1 And Not cboCentro_Custo.SelectedValue = Nothing) Then vPakage = "Centro_Custo"
        If Session("Id_Usuario_Perfil_Acesso") = 6 Or (Session("Id_Usuario_Perfil_Acesso") = 1 And Not cboDepartamento.SelectedValue = Nothing) Then vPakage = "Departamento"
        If Session("Id_Usuario_Perfil_Acesso") = 7 Or (Session("Id_Usuario_Perfil_Acesso") = 1 And Not cboSetor.SelectedValue = Nothing) Then vPakage = "Setor"
        If Session("Id_Usuario_Perfil_Acesso") = 8 Or (Session("Id_Usuario_Perfil_Acesso") = 1 And Not cboSecao.SelectedValue = Nothing) Then vPakage = "Secao"

        If Not cboConsumidor.SelectedValue = Nothing Or Not Trim(txtConsumidor.Text) = Nothing Then
            vPakage = "Consumidor"
            If Not Trim(txtConsumidor.Text) = Nothing Then vPakage = "Desc_Consumidor"
        End If

        If vPakage = Nothing Then vPakage = "Consumidor"

        Return vPakage
    End Function

    Function pParametro1() As System.String
        Dim pHidden As System.String = ""
        Dim vRetorno As System.String = Nothing

        '----filial
        If Session("Id_Usuario_Perfil_Acesso") = 4 Or (Session("Id_Usuario_Perfil_Acesso") = 1 And Not cboFilial.SelectedValue = Nothing) Then
            If cboFilial.SelectedValue = Nothing Then
                pHidden = convertCombo(cboFilial)
            Else
                pHidden = cboFilial.SelectedValue
            End If
        End If
        '----centro de custo
        If Session("Id_Usuario_Perfil_Acesso") = 5 Or (Session("Id_Usuario_Perfil_Acesso") = 1 And Not cboCentro_Custo.SelectedValue = Nothing) Then
            If cboCentro_Custo.SelectedValue = Nothing Then
                pHidden = convertCombo(cboCentro_Custo)
            Else
                pHidden = cboCentro_Custo.SelectedValue
            End If
        End If
        '----departamento
        If Session("Id_Usuario_Perfil_Acesso") = 6 Or (Session("Id_Usuario_Perfil_Acesso") = 1 And Not cboDepartamento.SelectedValue = Nothing) Then
            If cboDepartamento.SelectedValue = Nothing Then
                pHidden = convertCombo(cboDepartamento)
            Else
                pHidden = cboDepartamento.SelectedValue
            End If
        End If
        '----setor
        If Session("Id_Usuario_Perfil_Acesso") = 7 Or (Session("Id_Usuario_Perfil_Acesso") = 1 And Not cboSetor.SelectedValue = Nothing) Then
            If cboSetor.SelectedValue = Nothing Then
                pHidden = convertCombo(cboSetor)
            Else
                pHidden = cboSetor.SelectedValue
            End If
        End If
        '----secao
        If Session("Id_Usuario_Perfil_Acesso") = 8 Or (Session("Id_Usuario_Perfil_Acesso") = 1 And Not cboSecao.SelectedValue = Nothing) Then
            If cboSecao.SelectedValue = Nothing Then
                pHidden = convertCombo(cboSecao)
            Else
                pHidden = cboSecao.SelectedValue
            End If
        End If

        '----consumidor
        If Not cboConsumidor.SelectedValue = Nothing Then
            pHidden = cboConsumidor.SelectedValue
        End If

        If Not txtConsumidor.Text = "" Then
            pHidden = txtConsumidor.Text
        End If

        '-----monta retorno
        If Not Trim(pHidden) = "" Then
            vRetorno = pHidden
        End If

        Return vRetorno
    End Function

    Function pParametro_Filial() As System.String
        Dim vRetormo As System.String = Nothing
        vRetormo = IIf(cboFilial.SelectedValue = Nothing, 0, cboFilial.SelectedValue)

        '-----------------------------------------------------------------------------
        '-----quando usuario for gestor e nao selecionar nada o sistema seleciona tudo
        '-----------------------------------------------------------------------------
        If vRetormo = Nothing Then
            Return IIf(cboFilial.SelectedValue = Nothing, 0, convertCombo(cboFilial))
        End If

        Return vRetormo
    End Function

    Function pParametro_Usuario() As System.String
        Return Session("Id_Usuario")
    End Function

    Function pParametro_Centro_Custo() As System.String
        If Not cboCentro_Custo.SelectedValue = "" Then
            Return cboCentro_Custo.SelectedValue
        Else
            Return Nothing
        End If
    End Function

    Function pParametro_Departamento() As System.String
        If Not cboDepartamento.SelectedValue = "" Then
            Return cboDepartamento.SelectedValue
        Else
            Return Nothing
        End If
    End Function

    Function pParametro_Setor() As System.String
        If Not cboSetor.SelectedValue = "" Then
            Return cboSetor.SelectedValue
        Else
            Return Nothing
        End If
    End Function

    '------------------------------------------------------------------------------------
    '-----codigo
    '------------------------------------------------------------------------------------
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

            '-----monta ativo sob responsabilidade do colaborador
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim vDataSet As New System.Data.DataSet
            vDataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Valida_Ativo_Grupo", Nothing, Nothing, Nothing, Nothing, Nothing)
            Dim Linha As System.Data.DataRow
            For Each Linha In vDataSet.Tables(0).Rows
                If Linha.Item(0) = "Telefonia_Movel" Then btMovel.Enabled = True
                If Linha.Item(0) = "Telefonia_Fixa" Then btFixa.Enabled = True
                If Linha.Item(0) = "Link_Dados" Then btDados.Enabled = True
                If Linha.Item(0) = "Desktop" Then btDesktop.Enabled = True
                If Linha.Item(0) = "Impressora" Then btPrint.Enabled = True
            Next

            '-----nivel 1 master gerenciador do sistema
            If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                pnlFilial.Visible = True
                pnlCentroCusto.Visible = True
                lnCentroCusto.Visible = True
                pnlDepartamento.Visible = False
                pnlSetor.Visible = False
                pnlSecao.Visible = False
                pnlConsumidor.Visible = True
                lnConsumidor.Visible = True
                '-----filial
                CarregaCombo(cboFilial, WS_Cadastro.DropList(Session("Conn_Banco"), "sp_Drop_Filial", Nothing))
            End If

            '-----nivel 4 gestor de filial
            If Session("Id_Usuario_Perfil_Acesso") = 4 Then
                pnlFilial.Visible = True
                pnlCentroCusto.Visible = False
                pnlDepartamento.Visible = False
                pnlSetor.Visible = False
                pnlSecao.Visible = False
                pnlConsumidor.Visible = True
                lnConsumidor.Visible = True
                '-----filial
                CarregaCombo(cboFilial, WS_Cadastro.Hierarquia(Session("Conn_Banco"), "sp_SL_RL_Filial", Nothing, Session("Id_Usuario")))
                '-----consumidor
                CarregaCombo(cboConsumidor, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Consumidor", Session("Id_Usuario"), Nothing))
            End If

            '-----nivel 5 gestor de centro custo
            If Session("Id_Usuario_Perfil_Acesso") = 5 Then
                pnlFilial.Visible = True
                pnlCentroCusto.Visible = True
                lnCentroCusto.Visible = True
                pnlDepartamento.Visible = False
                pnlSetor.Visible = False
                pnlSecao.Visible = False
                pnlConsumidor.Visible = True
                lnConsumidor.Visible = True
                '-----filial
                CarregaCombo(cboFilial, WS_Cadastro.Hierarquia(Session("Conn_Banco"), "sp_SL_RL_Filial", Nothing, Session("Id_Usuario")))
                '-----consumidor
                CarregaCombo(cboConsumidor, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Consumidor", Session("Id_Usuario"), Nothing))
            End If

            '-----nivel 6 gestor de departamento
            If Session("Id_Usuario_Perfil_Acesso") = 6 Then
                pnlFilial.Visible = True
                pnlCentroCusto.Visible = False
                pnlDepartamento.Visible = True
                lnDepartamento.Visible = True
                pnlSetor.Visible = False
                pnlSecao.Visible = False
                pnlConsumidor.Visible = True
                lnConsumidor.Visible = True
                '-----filial
                CarregaCombo(cboFilial, WS_Cadastro.Hierarquia(Session("Conn_Banco"), "sp_SL_RL_Filial", Nothing, Session("Id_Usuario")))
                '-----consumidor
                CarregaCombo(cboConsumidor, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Consumidor", Session("Id_Usuario"), Nothing))
            End If

            '-----nivel 7 gestor de setor
            If Session("Id_Usuario_Perfil_Acesso") = 7 Then
                pnlFilial.Visible = True
                pnlCentroCusto.Visible = False
                pnlDepartamento.Visible = False
                pnlSetor.Visible = True
                lnSetor.Visible = True
                pnlSecao.Visible = False
                pnlConsumidor.Visible = True
                lnConsumidor.Visible = True
                '-----filial
                CarregaCombo(cboFilial, WS_Cadastro.Hierarquia(Session("Conn_Banco"), "sp_SL_RL_Filial", Nothing, Session("Id_Usuario")))
                '-----consumidor
                CarregaCombo(cboConsumidor, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Consumidor", Session("Id_Usuario"), Nothing))
            End If

            '-----nivel 8 gestor de Secao
            If Session("Id_Usuario_Perfil_Acesso") = 8 Then
                pnlFilial.Visible = True
                pnlCentroCusto.Visible = False
                pnlDepartamento.Visible = False
                pnlSetor.Visible = False
                pnlSecao.Visible = True
                lnSecao.Visible = True
                pnlConsumidor.Visible = True
                lnConsumidor.Visible = True
                '-----filial
                CarregaCombo(cboFilial, WS_Cadastro.Hierarquia(Session("Conn_Banco"), "sp_SL_RL_Filial", Nothing, Session("Id_Usuario")))
                '-----consumidor
                CarregaCombo(cboConsumidor, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Consumidor", Session("Id_Usuario"), Nothing))
            End If

            '-----nivel 3 gestor de consumidor
            If Session("Id_Usuario_Perfil_Acesso") = 3 Or Session("Id_Usuario_Perfil_Acesso") = 9 Then
                pnlFilial.Visible = True
                pnlCentroCusto.Visible = False
                pnlDepartamento.Visible = False
                pnlSetor.Visible = False
                pnlSecao.Visible = False
                pnlConsumidor.Visible = True
                lnConsumidor.Visible = True
                '-----filial
                CarregaCombo(cboFilial, WS_Cadastro.Hierarquia(Session("Conn_Banco"), "sp_SL_RL_Filial", Nothing, Session("Id_Usuario")))
                '-----consumidor
                CarregaCombo(cboConsumidor, WS_Cadastro.DropList_Filtro(Session("Conn_Banco"), "sp_Drop_Filtro_Hierarquia_Consumidor", Session("Id_Usuario"), Nothing))
            End If

            '-----valida visualizacao de departamento, setor e secao
            '-----monta parametro de hierarquia
            vDataSet = WS_Cadastro.Hierarquia(Session("Conn_Banco"), "sd_Valida_Nivel_Hierarquia", cboFilial.SelectedValue, Nothing)

            If vDataSet.Tables.Count = 0 Then Exit Sub
            If vDataSet.Tables(0).Rows.Count > 0 Then
                If Session("Id_Usuario_Perfil_Acesso") = 1 Or
                    Session("Id_Usuario_Perfil_Acesso") = 4 Or
                    Session("Id_Usuario_Perfil_Acesso") = 5 Or
                    Session("Id_Usuario_Perfil_Acesso") = 6 Then
                    pnlDepartamento.Visible = vDataSet.Tables(0).Rows(0).Item("Hi_Departamento")
                    lnDepartamento.Visible = vDataSet.Tables(0).Rows(0).Item("Hi_Departamento")
                End If

                If Session("Id_Usuario_Perfil_Acesso") = 1 Or
                    Session("Id_Usuario_Perfil_Acesso") = 4 Or
                    Session("Id_Usuario_Perfil_Acesso") = 5 Or
                    Session("Id_Usuario_Perfil_Acesso") = 6 Or
                    Session("Id_Usuario_Perfil_Acesso") = 7 Then
                    pnlSetor.Visible = vDataSet.Tables(0).Rows(0).Item("Hi_Setor")
                    lnSetor.Visible = vDataSet.Tables(0).Rows(0).Item("Hi_Setor")
                End If

                If Session("Id_Usuario_Perfil_Acesso") = 1 Or
                    Session("Id_Usuario_Perfil_Acesso") = 4 Or
                    Session("Id_Usuario_Perfil_Acesso") = 5 Or
                    Session("Id_Usuario_Perfil_Acesso") = 6 Or
                    Session("Id_Usuario_Perfil_Acesso") = 7 Or
                    Session("Id_Usuario_Perfil_Acesso") = 8 Then
                    pnlSecao.Visible = vDataSet.Tables(0).Rows(0).Item("Hi_Secao")
                    lnSecao.Visible = vDataSet.Tables(0).Rows(0).Item("Hi_Secao")
                End If
            End If
        End If
    End Sub

    Protected Sub cboFilial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFilial.SelectedIndexChanged
        WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----administrator getor filial getor centro de custo
        If (Session("Id_Usuario_Perfil_Acesso") = 1 Or Session("Id_Usuario_Perfil_Acesso") = 4 Or Session("Id_Usuario_Perfil_Acesso") = 5) And Not cboFilial.SelectedValue = Nothing Then
            CarregaCombo(cboCentro_Custo, WS_Cadastro.Hierarquia(Session("Conn_Banco"), "sd_SL_RL_G_Centro_Custo_Filial", cboFilial.SelectedValue, Session("Id_Usuario")))
        End If

        '-----getor departamento
        If Session("Id_Usuario_Perfil_Acesso") = 6 And Not cboFilial.SelectedValue = Nothing Then
            CarregaCombo(cboDepartamento, WS_Cadastro.Hierarquia(Session("Conn_Banco"), "sd_SL_RL_G_Departamento_Centro_Custo", cboFilial.SelectedValue, Session("Id_Usuario")))
        End If

        '-----gestor de setor
        If Session("Id_Usuario_Perfil_Acesso") = 7 And Not cboFilial.SelectedValue = Nothing Then
            CarregaCombo(cboSetor, WS_Cadastro.Hierarquia(Session("Conn_Banco"), "sd_SL_RL_G_Setor_Departamento", cboFilial.SelectedValue, Session("Id_Usuario")))
        End If

        '-----gestor de secao
        If Session("Id_Usuario_Perfil_Acesso") = 8 And Not cboFilial.SelectedValue = Nothing Then
            CarregaCombo(cboSecao, WS_Cadastro.Hierarquia(Session("Conn_Banco"), "sd_SL_RL_G_Secao_Setor", cboFilial.SelectedValue, Session("Id_Usuario")))
        End If
    End Sub

    Protected Sub Valida_Botao(ByVal sender As Object, ByVal tipo As String)
        Session("KPI") = tipo
        btMovel.Style.Add("Opacity", "10")
        btFixa.Style.Add("Opacity", "10")
        btDados.Style.Add("Opacity", "10")
        btDesktop.Style.Add("Opacity", "10")
        btPrint.Style.Add("Opacity", "10")
        btSistema.Style.Add("Opacity", "10")
        sender.Style.Add("Opacity", "0.4")
    End Sub

    Protected Sub btMovel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btMovel.Click
        Valida_Botao(sender, "Telefonia_Movel")
    End Sub

    Protected Sub btFixa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btFixa.Click
        Valida_Botao(sender, "Telefonia_Fixa")
    End Sub

    Protected Sub btDados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btDados.Click
        Valida_Botao(sender, "Link_Dados")
    End Sub

    Protected Sub btDesktop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btDesktop.Click
        Valida_Botao(sender, "Desktop")
    End Sub

    Protected Sub btPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btPrint.Click
        Valida_Botao(sender, "Impressora")
    End Sub

    Protected Sub btSistema_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSistema.Click
        Valida_Botao(sender, Nothing)
    End Sub

    Function convertCombo(ByVal pCombo As DropDownList) As System.String
        Dim vTextoRetorno() As System.String = Nothing
        Dim i As System.Int32
        Dim vCampoRetorno As System.String = Nothing
        '-----quebra campo em linha

        For i = 1 To pCombo.Items.Count - 1
            vCampoRetorno = vCampoRetorno & pCombo.Items(i).Value & ","
        Next

        If vCampoRetorno = Nothing Then
            Return Nothing
        Else
            Return Mid(vCampoRetorno, 1, Len(vCampoRetorno) - 1)
        End If
    End Function

    Public Sub CarregaCombo(ByVal pCombo As WebControls.DropDownList,
                        ByVal pDataSet As System.Data.DataSet)

        If pDataSet.Tables.Count = 0 Then Exit Sub

        Dim Linha As DataRow
        Dim Item As ListItem
        pCombo.Items.Clear()

        Item = New ListItem
        Item.Value = ""
        Item.Text = Replace(Replace(Replace(pCombo.ID, "cbo", ""), "_", " de "), "secao", "Seção")
        pCombo.Items.Add(Item)

        For Each Linha In pDataSet.Tables(0).Rows
            Item = New ListItem
            Item.Text = Linha.Item(1)
            Item.Value = Linha.Item(0)
            pCombo.Items.Add(Item)
        Next
    End Sub
End Class
