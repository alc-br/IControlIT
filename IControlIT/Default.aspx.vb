Imports System.IO
'Imports ASPSnippets.FaceBookAPI
Imports System.Web.Script.Serialization
Imports System.Web.Mail
Imports System.Security.Claims
Imports System.IdentityModel.Tokens.Jwt
Imports Microsoft.IdentityModel.Tokens
Imports System.Net
Imports System.Security.Cryptography

' 21032025 - CHIPAK - REDBELT - Implementacao da protecao de forca bruta

Public Class _Default
    Inherits System.Web.UI.Page

    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim oTradutor As New cls_Traducao
    Dim vdataSet As Data.DataSet

    ' 21032025 - INICIO
    ' Configurações de segurança (força bruta)
    Dim MaxTentativasUsuario As Integer = Integer.Parse(ConfigurationManager.AppSettings("MaxTentativasUsuario"))
    Dim MaxTentativasIP As Integer = Integer.Parse(ConfigurationManager.AppSettings("MaxTentativasIP"))
    Dim TempoBloqueio As Integer = Integer.Parse(ConfigurationManager.AppSettings("TempoBloqueioMinutos"))
    ' 21032025 - FIM

    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    'Dim FaceBookUser As cls_Facebook
    Dim v_Valida_Usuario As System.Int16
    Dim Linha As System.Data.DataRow

    '----- C2AICONTROLIT-116
    '----- site:https://www.javainuse.com/jwtgenerator
    '----- sessão : Create JSON Web Token Using Secret Key 
    '----- algorithm: HS256
    '----- key : projetoicontrolit
    Private Shared ReadOnly chave As String = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTcwODY5NjUzNSwiaWF0IjoxNzA4Njk2NTM1fQ.yoilXbc-hpHRMFSGHTan7vBLebtLbiicV1N-j1PFX0o"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

        '-----verifica e valida conexao
        If Not Page.IsPostBack Then
            Session("Id_Usuario") = Nothing
            Session("Id_Usuario_Perfil_Acesso") = Nothing
            Session("Nm_Usuario") = Nothing
            Session("Conn_Banco") = Nothing
            Session("Id_Idioma") = Nothing
            Session("KPI") = Nothing
            Session("Empresa") = Nothing
            Session("DataSet") = Nothing
            Session("Menu") = Nothing
            v_Valida_Usuario = 0
            lblMsgTokenSenha.Visible = False

            '-----realisa chamada do banco (IControlIT)
            For Each Linha In WS_Modulo.Conn_Banco.Tables(0).Rows
                If "ATIVVUS_LOGIN" = Linha.Item(1) Then
                    Session("Conn_Banco") = Linha.Item(0)
                End If
            Next

            'conexao com facebook
            '--------------------
            'FaceBookConnect.API_Key = ConfigurationManager.AppSettings("Face.AppId")
            'FaceBookConnect.API_Secret = ConfigurationManager.AppSettings("Face.Secret")

            '-----validacao pelo AD (local ou redirecionado)
            If Not Request.ServerVariables("LOGON_USER").Substring(Request.ServerVariables("LOGON_USER").LastIndexOf("\") + 1) = "" Or Not Request("redirect") = "" Then

                '-----validacao pelo AD
                If Not Request.ServerVariables("LOGON_USER").Substring(Request.ServerVariables("LOGON_USER").LastIndexOf("\") + 1) = "" Then
                    vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                                        "Sd_Valida_Usuario_AD_Local",
                                                        Request.ServerVariables("LOGON_USER").Substring(Request.ServerVariables("LOGON_USER").LastIndexOf("\") + 1),
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing)
                End If

                '-----validacao pelo ridecionamento
                If Not Request("redirect") = "" Then
                    Dim vRedirect() As String = oTradutor.Descriptografar(Request("redirect")).Split(";")

                    vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                                        "Sd_Valida_Usuario_Redirecionado",
                                                        vRedirect(0),
                                                        vRedirect(1),
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing,
                                                        Nothing)
                End If

                If vdataSet.Tables(0).Rows.Count > 0 Then
                    Session("Id_Usuario") = vdataSet.Tables(0).Rows(0).Item("Id_Usuario")
                    Session("Id_Usuario_Perfil_Acesso") = vdataSet.Tables(0).Rows(0).Item("Id_Usuario_Perfil_Acesso")
                    Session("Nm_Usuario") = vdataSet.Tables(0).Rows(0).Item("Nm_Usuario")
                    Session("Id_Idioma") = vdataSet.Tables(0).Rows(0).Item("Id_Idioma")
                    Session("Empresa") = vdataSet.Tables(0).Rows(0).Item("Empresa")
                    txtUsuario.Text = vdataSet.Tables(0).Rows(0).Item("Nm_Usuario")
                    txtSenha.Text = ""

                    v_Valida_Usuario = 1
                End If
            End If

            '-----validacao pelo facebook
            '-----------------------------------------------------------------
            '    If Not Request.QueryString("error") = "access_denied" Then
            '    Dim code As String = Request.QueryString("code")
            '    If Not String.IsNullOrEmpty(code) Then
            '        Dim data As String = FaceBookConnect.Fetch(code, "me")
            '        Dim faceBookUser As cls_Facebook.FaceBookUser = New JavaScriptSerializer().Deserialize(Of cls_Facebook.FaceBookUser)(data)

            '        vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"), _
            '                                               "Sd_Valida_Usuario_Facebook", _
            '                                               Nothing, _
            '                                               Nothing, _
            '                                               Nothing, _
            '                                               Nothing, _
            '                                               faceBookUser.Id, _
            '                                               Nothing, _
            '                                               Nothing)
            '        If vdataSet.Tables(0).Rows.Count = 0 Then
            '            '-----vincula e libera usuario do facebook no IControlIT
            '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalSubFacebook');", True)
            '        Else
            '            '-----usuario liberado pelo facebook 
            '            Session("Id_Usuario") = vdataSet.Tables(0).Rows(0).Item("Id_Usuario")
            '            Session("Id_Usuario_Perfil_Acesso") = vdataSet.Tables(0).Rows(0).Item("Id_Usuario_Perfil_Acesso")
            '            Session("Nm_Usuario") = vdataSet.Tables(0).Rows(0).Item("Nm_Usuario")
            '            Session("Id_Idioma") = vdataSet.Tables(0).Rows(0).Item("Id_Idioma")
            '            Session("Empresa") = vdataSet.Tables(0).Rows(0).Item("Empresa")
            '            txtUsuario.Text = vdataSet.Tables(0).Rows(0).Item("Nm_Usuario")
            '            txtSenha.Text = " "

            '            v_Valida_Usuario = 1
            '        End If
            '    End If
            'End If



            '-----usuario validado
            If v_Valida_Usuario = 0 Then
                txtUsuario.ReadOnly = False
                txtSenha.ReadOnly = False
                btLogin.Enabled = True
                Page.SetFocus(txtUsuario)
                Page.ClientScript.RegisterStartupScript(GetType(Type), "ClientScript", "<script> document.forms[0].txtUsuario.focus();</script>")
            Else
                '-----monta texto do termo
                For Each Linha In WS_Modulo.Monta_Texto_Termo(Session("Conn_Banco"), "sp_SL_ID", Session("Empresa")).Tables(0).Rows
                    If Linha.Item("Caixa") = "lblTexto1" Then lblTexto1.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto2" Then lblTexto2.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto3" Then lblTexto3.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto4" Then lblTexto4.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto5" Then lblTexto5.Text = Linha.Item("Texto")
                Next

                '-----carrega banco da empresa validada
                For Each Linha In WS_Modulo.Conn_Banco.Tables(0).Rows
                    If Session("Empresa").ToUpper = Linha.Item(1) Then
                        Session("Conn_Banco") = Linha.Item(0)
                    End If
                Next

                If vdataSet.Tables(0).Rows(0).Item("Fl_Desativado") = 3 Then
                    lblTextoNome.Text = "Olá, " + vdataSet.Tables(0).Rows(0).Item("Nm_Consumidor") + " bem vindo ao portal iControlIT."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalSubTermo');", True)
                    btLogin.Enabled = False
                    'brFacebook.Enabled = False
                    txtUsuario.Enabled = False
                    txtSenha.Enabled = False

                    vdataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Linha_De_Acordo", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)

                    dtgAtivo.DataSource = vdataSet
                    dtgAtivo.DataBind()
                Else
                    If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                        Response.Redirect("~/Home.aspx") 'IIf(Request("Logoff") = Nothing, "~/IControlIT.aspx", "~/Home.aspx"))
                    Else
                        Response.Redirect("~/CockPit_Menu.aspx")
                    End If
                End If
            End If

            '----- iniciou a tela com post do e-mail
            If Request.QueryString("hashts") IsNot Nothing Then
                Dim token As String = Request.QueryString("hashts")
                Dim email As String = Nothing
                Dim nmUsuario As String = Nothing
                Dim resultado As (email As String, nmUsuario As String) = DecodificarTokenJwt(token)

                If (resultado.email.IsNullOrEmpty And resultado.nmUsuario.IsNullOrEmpty) Then

                    lblMsgTokenInvalido.Visible = True
                Else

                    '----- Exibir o campo de confirmação de senha
                    txtNovaSenha.Visible = True
                    txtEmail.Text = resultado.email
                    txtUsuario.Text = resultado.nmUsuario
                    btTrocarSenha.Visible = False

                    lblAcesso.Text = "Alterar Senha"

                    btLogin.Text = "Alterar"

                    msgSenhaForte1.Visible = True

                End If

            End If

            '------- K2AICONTROLIT - 24
            If Request.QueryString("hashtr") IsNot Nothing Then
                Dim token As String = Request.QueryString("hashtr")

                '-----selecionar o método que valida o token
                Call ProcessarTokenTermoResponsabilidade(token)
            End If
            '------- K2AICONTROLIT - 24
        End If

    End Sub
    '------ K2AICONTROLIT- 24
    Protected Sub ProcessarTokenTermoResponsabilidade(ByVal token As String)
        Dim resultado As (IdAtivo As String, IdUsuario As String, Empresa As String) = ValidarTokenTermoResponsabilidade(token)

        If (resultado.IdAtivo.IsNullOrEmpty And resultado.IdUsuario.IsNullOrEmpty) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "Modal('#ModalTermoRespon');", True)
        Else
            Dim resultUrlTermo = MontaUrlTermoResponsabilidade(token)

            If String.IsNullOrEmpty(resultUrlTermo) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "Modal('#ModalTermoRespon');", True)
            Else
                '----- iniciar a montagem da modal de exibição do termo
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key",
                                                   resultUrlTermo, True)
                '----- registrar que o termo já foi visto
            End If

        End If

    End Sub

    Protected Function MontaUrlTermoResponsabilidade(ByVal token As String)
        '---- Sd_TR_MontarTokenTermoResponsabilidade
        vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                        "Sd_TR_MontarTokenTermoResponsabilidade",
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        token,
                                        Nothing)
        Dim NmConsumidor As String = ""
        Dim DtHrAtivacao As String = ""
        Dim DtHrDesativacao As String = ""
        Dim TermoAtivacao As String = ""
        Dim IdAtivo As String = ""
        Dim IdConsumidor As String = ""
        Dim NmAtivoTipoGrupo As String = ""
        Dim Email As String = ""
        Dim NmUsuario As String = ""
        Dim url As String = ""

        If vdataSet.Tables.Count > 0 Then
            TermoAtivacao = vdataSet.Tables(0).Rows(0).Item("Termo_Ativacao")
            IdAtivo = vdataSet.Tables(0).Rows(0).Item("Id_Ativo")
            IdConsumidor = vdataSet.Tables(0).Rows(0).Item("Id_Consumidor")
            NmAtivoTipoGrupo = vdataSet.Tables(0).Rows(0).Item("Nm_Ativo_Tipo_Grupo")
            'window.open('../Termo/Termo_Responsabilidade_Fortlev.aspx?Id_Ativo=1785&Id_Consumidor=2687&Nm_Ativo_Tipo_Grupo=Telefonia_Movel','','resizable=yes, menubar=no, scrollbars=yes,height=600, width=800, top=10, left=10')

            url = "window.open('../Termo/Anonimous_" & TermoAtivacao & "?token=" & token & "&Id_Ativo=" & IdAtivo & "&Id_Consumidor=" & IdConsumidor & "&Nm_Ativo_Tipo_Grupo=" & NmAtivoTipoGrupo & "','','resizable=yes, menubar=no, scrollbars=yes,height=600, width=800, top=10, left=10')"
            'url = "window.open('../Termo/" & TermoAtivacao & "?Id_Ativo=" & IdAtivo & "&Id_Consumidor=" & IdConsumidor & "&Nm_Ativo_Tipo_Grupo=" & NmAtivoTipoGrupo & " , , resizable=yes, menubar=no, scrollbars=yes,height=600, width=800, top=10, left=10 ')"

        End If

        Return url

    End Function

    Protected Function ValidarTokenTermoResponsabilidade(token As String) As (IdAtivo As String, IdUsuario As String, Empresa As String)

        '---- Sd_Valida_Token_Valido
        vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                        "Sd_TR_ValidarTokenTermoResponsabilidade",
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        token,
                                        Nothing)


        Dim IdAtivo As String = ""
        Dim IdUsuario As String = ""
        Dim Empresa As String = ""
        If vdataSet.Tables.Count > 0 Then
            IdAtivo = vdataSet.Tables(0).Rows(0).Item("Id_Ativo")
            IdUsuario = vdataSet.Tables(0).Rows(0).Item("Id_Usuario")
            Empresa = vdataSet.Tables(0).Rows(0).Item("Empresa")
        End If
        Return (IdAtivo, IdUsuario, Empresa)
    End Function

    '------ K2AICONTROLIT- 24


    'Protected Sub brFacebook_Click(sender As Object, e As ImageClickEventArgs) Handles brFacebook.Click
    '    FaceBookConnect.Authorize(Nothing, Request.Url.AbsoluteUri.Split("?"c)(0))
    'End Sub

    '-----Métodos para manipular o token

    Public Shared Function GerarHash32Caracteres(ByVal input As String) As String
        Using sha256Hash As SHA256 = SHA256.Create()
            ' Converte a entrada string em um array de bytes e computa o hash
            Dim bytes As Byte() = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input))

            ' Converte o array de bytes em uma string hexadecimal
            Dim stringBuilder As New StringBuilder()
            For i As Integer = 0 To bytes.Length - 1
                stringBuilder.Append(bytes(i).ToString("x2"))
            Next

            ' Retorna os primeiros 32 caracteres da hash
            Return stringBuilder.ToString().Substring(0, 32)
        End Using
    End Function

    Public Shared Function GerarTokenJwt(email As String, nmUsuario As String) As String
        Dim tokenHandler As New JwtSecurityTokenHandler()
        Dim chaveBytes As Byte() = Encoding.ASCII.GetBytes(chave)
        Dim tokenDescriptor As New SecurityTokenDescriptor() With {
            .Subject = New ClaimsIdentity(New Claim() {
                New Claim(ClaimTypes.Email, email),
                New Claim(ClaimTypes.Name, nmUsuario)
            }),
            .Expires = DateTime.UtcNow.AddHours(1),
            .SigningCredentials = New SigningCredentials(New SymmetricSecurityKey(chaveBytes), SecurityAlgorithms.HmacSha256Signature)
        }
        Dim token As SecurityToken = tokenHandler.CreateToken(tokenDescriptor)
        Return tokenHandler.WriteToken(token)
    End Function

    Protected Function DecodificarTokenJwt(token As String) As (email As String, nmUsuario As String)

        '---- Sd_Valida_Token_Valido
        vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                        "Sd_Valida_Token_Valido",
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        token,
                                        Nothing)


        Dim email As String = ""
        Dim nmUsuario As String = ""
        If vdataSet.Tables.Count > 0 Then
            email = vdataSet.Tables(0).Rows(0).Item("Email")
            nmUsuario = vdataSet.Tables(0).Rows(0).Item("Nm_Usuario")
        End If
        Return (email, nmUsuario)
    End Function

    '----- ./Métodos para manipular o token

    Protected Sub btEmailCorporativo_Click(sender As Object, e As EventArgs) Handles btEmailCorporativo.Click
        If Trim(txtEmailCorporativo.Text) = "" Then Exit Sub

        vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                        "Sd_Viculacao_Email_Facebook",
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        txtEmailCorporativo.Text,
                                        Nothing,
                                        Nothing,
                                        Nothing)

        lblTexto_Email.Visible = True
        lblTexto_Email.Text = vdataSet.Tables(0).Rows(0).Item("Texto_Email")

        If vdataSet.Tables(0).Rows(0).Item("Texto_Email") = "Seu ID foi encaminhado por e-mail." Then
            lblSegundoPasso.Visible = True
            lblChaveSeguranca.Visible = True
            lblTexto_Email.Visible = True
            txtChaveSeguranca.Visible = True
            btChaveSeguranca.Visible = True
        End If

    End Sub

    Protected Sub btChaveSeguranca_Click(sender As Object, e As EventArgs) Handles btChaveSeguranca.Click
        If Trim(txtChaveSeguranca.Text) = "" Then Exit Sub

        Dim code As String = Request.QueryString("code")
        If Not String.IsNullOrEmpty(code) Then
            'Dim data As String = FaceBookConnect.Fetch(code, "me")
            'Dim faceBookUser As cls_Facebook.FaceBookUser = New JavaScriptSerializer().Deserialize(Of cls_Facebook.FaceBookUser)(data)

            'vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
            '                    "Sd_Valida_Chave_Id_Facebook",
            '                    Nothing,
            '                    Nothing,
            '                    Nothing,
            '                    Nothing,
            '                    faceBookUser.Id,
            '                    txtChaveSeguranca.Text,
            '                    Nothing)

            'lblTexto_Email.Visible = True
            'lblTexto_Email.Text = vdataSet.Tables(0).Rows(0).Item("Texto_Validacao")

            'vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
            '                                    "Sd_Valida_Usuario_Facebook",
            '                                    Nothing,
            '                                    Nothing,
            '                                    Nothing,
            '                                    Nothing,
            '                                    faceBookUser.Id,
            '                                    Nothing,
            '                                    Nothing)

            If vdataSet.Tables(0).Rows.Count > 0 Then
                '-----usuario liberado pelo facebook
                Session("Id_Usuario") = vdataSet.Tables(0).Rows(0).Item("Id_Usuario")
                Session("Id_Usuario_Perfil_Acesso") = vdataSet.Tables(0).Rows(0).Item("Id_Usuario_Perfil_Acesso")
                Session("Nm_Usuario") = vdataSet.Tables(0).Rows(0).Item("Nm_Usuario")
                Session("Id_Idioma") = vdataSet.Tables(0).Rows(0).Item("Id_Idioma")
                Session("Empresa") = vdataSet.Tables(0).Rows(0).Item("Empresa")
                txtUsuario.Text = vdataSet.Tables(0).Rows(0).Item("Nm_Usuario")
                txtSenha.Text = ""

                '-----monta texto do termo
                For Each Linha In WS_Modulo.Monta_Texto_Termo(Session("Conn_Banco"), "sp_SL_ID", Session("Empresa")).Tables(0).Rows
                    If Linha.Item("Caixa") = "lblTexto1" Then lblTexto1.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto2" Then lblTexto2.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto3" Then lblTexto3.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto4" Then lblTexto4.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto5" Then lblTexto5.Text = Linha.Item("Texto")
                Next

                '-----carrega banco da empresa validada
                For Each Linha In WS_Modulo.Conn_Banco.Tables(0).Rows
                    If Session("Empresa").ToUpper = Linha.Item(1) Then
                        Session("Conn_Banco") = Linha.Item(0)
                    End If
                Next

                If vdataSet.Tables(0).Rows(0).Item("Fl_Desativado") = 3 Then
                    ''lblTextoNome.Text = "Olá, " + vdataSet.Tables(0).Rows(0).Item("Nm_Consumidor") + " bem vindo ao portal iControlIT."
                    ''ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalSubTermo');", True)
                    ''btLogin.Enabled = False
                    '''brFacebook.Enabled = False
                    ''txtUsuario.Enabled = False
                    ''txtSenha.Enabled = False

                    ''vdataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Linha_De_Acordo", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)

                    ''dtgAtivo.DataSource = vdataSet
                    ''dtgAtivo.DataBind()

                    ' RETIRADO O POPUP DE VALIDAÇÃO DO USUÁRIO APÓS FLAG DESATIVADO = 3
                    ' ESSE FLAG ESTÁ SENDO ALTERADO PARA 3 A CADA VEZ QUE O USUÁRIO É ATUALIZADO NO SISTEMA
                    ' AGORA, AO FAZER LOGIN, O USUÁRIO FLAG 3 É AUTOMATICAMENTE DESFLAGUEADO E REDIRECIONADO COMO LOGIN NORMAL
                    ' DEIXAR O CÓDIGO ACIMA PARA CASO DE FUTURO ARREPENDIMENTO E PEDIDO DE POPUP DOS TERMOS DO ACORDO DE VOLTA
                    WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Satus_Acordo", Session("Nm_Usuario"), Nothing, Nothing, Nothing, Nothing)
                    If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                        Response.Redirect("~/Home.aspx") 'IIf(Request("Logoff") = Nothing, "~/IControlIT.aspx", "~/Home.aspx"))
                    Else
                        Response.Redirect("~/CockPit_Menu.aspx")
                    End If
                Else
                    If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                        Response.Redirect("~/Home.aspx") 'IIf(Request("Logoff") = Nothing, "~/IControlIT.aspx", "~/Home.aspx"))
                    Else
                        Response.Redirect("~/CockPit_Menu.aspx")
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub btTrocarSenha_Click(sender As Object, e As EventArgs) Handles btTrocarSenha.Click
        'txtConfirmaSenhaForte.Visible = False
        'txtSenhaForte.Visible = False
        msgSenhaForteErroRequisitos.Visible = False
        msgSenhaForteDadosObrigatorios.Visible = False
        msgSenhaForte1.Visible = False
        'msgSenhaForte2.Visible = False
        'msgSenhaForte3.Visible = False
        'msgSenhaForte4.Visible = False
        'msgSenhaForte5.Visible = False

        If txtEmail.Visible = True Then
            'If txtNovaSenha.Visible = True Then
            'txtNovaSenha.Visible = False
            'rfvNovaSenha.Visible = False
            rfvEmail.Visible = False
            txtEmail.Visible = False
            '
            rfvSenha.Visible = True
            txtSenha.Visible = True
            btLogin.Text = "Conectar"

            lblAcesso.Text = "Login"


            btTrocarSenha.Text = "Trocar Senha"
        Else
            'txtNovaSenha.Visible = True
            'rfvNovaSenha.Visible = True
            lblMsgTokenInvalido.Visible = False
            txtEmail.Visible = True
            rfvEmail.Visible = True '
            rfvSenha.Visible = False
            txtSenha.Visible = False
            btLogin.Text = "Solicitar"
            btTrocarSenha.Text = "Voltar"
            lblAcesso.Text = "Alterar Senha"
        End If
    End Sub

    Protected Sub btAcordo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btAcordo.Click
        '-----atualiza status do usuario
        WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Satus_Acordo", Session("Nm_Usuario"), Nothing, Nothing, Nothing, Nothing)

        '-----acessa sistema
        If Session("Id_Usuario_Perfil_Acesso") = 1 Then
            Response.Redirect("~/Home.aspx") 'IIf(Request("Logoff") = Nothing, "~/IControlIT.aspx", "~/Home.aspx"))
        Else
            Response.Redirect("~/CockPit_Menu.aspx")
        End If
    End Sub

    'Protected Sub btFecharEmailEnviara_Click(sender As Object, e As EventArgs) Handles btEsqueciSenha.Click
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalEnviaSenhaEmail');", True)
    '    lblMsgEnvioEmail.Text = ""
    'End Sub

    Protected Sub btEmailEnvio_Click1(sender As Object, e As EventArgs) Handles btEmailEnvio.Click
        If Trim(txtEmailEnvio.Text) = "" Then Exit Sub

        vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                        "Sd_Envia_Senha_Email",
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        txtEmailEnvio.Text,
                                        Nothing,
                                        Nothing,
                                        Nothing)

        lblMsgEnvioEmail.Text = vdataSet.Tables(0).Rows(0).Item("Texto_Email")
    End Sub

    '----- Função que valida se o usuario possui e-mail cadastrado para enviar a mensagem com o token
    Protected Function trocaSenhaPossuiEmailValido(txtEmail As String, txtUsuario As String) As String

        '----- Confirmar se o email existe atraves da consulta no banco de dados.
        vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                        "Sd_Valida_Usuario_Com_Email",
                                        txtUsuario,
                                        Nothing,
                                        Nothing,
                                        txtEmail,
                                        Nothing,
                                        Nothing, 'Token
                                        Nothing)

        If vdataSet.Tables(0).Rows.Count > 0 Then
            Return vdataSet.Tables(0).Rows(0).Item("Empresa")
        Else
            Return ""
        End If

    End Function

    '----- função que grava o token para o usuario
    Protected Function trocaSenhaPossuiEmailValidoGravaToken(txtEmail As String, txtUsuario As String, token As String) As Boolean

        '----- Confirmar se o email existe atraves da consulta no banco de dados.
        vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                        "Sd_Valida_Usuario_Grava_Token",
                                        txtUsuario,
                                        Nothing,
                                        Nothing,
                                        txtEmail,
                                        Nothing,
                                        token,
                                        Nothing)

        If vdataSet.Tables(0).Rows.Count <= 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    '----- Função que grava na caixa de saida o token para o usuario
    Protected Function trocaSenhaGravaTokenCaixaSaida(txtEmail As String) As String

        vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                        "Sd_Valida_Usuario_Grava_CaixaSaidaEmail",
                                        Nothing,
                                        Nothing,
                                        Nothing,
                                        txtEmail,
                                        Nothing,
                                        Nothing,
                                        Nothing)

        lblMsgTokenSenha.Visible = True
        Return vdataSet.Tables(0).Rows(0).Item("Texto_Email")

    End Function

    '----- Função de grava a alteração da nova senha
    Protected Function trocaSenhaSalvarAlteracaoSenha(email As String, nmUsuario As String, txtSenha As String)
        vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                        "Sd_Valida_Usuario_Token_Troca_Senha",
                                        nmUsuario,
                                        txtSenha,
                                        txtSenha,
                                        email,
                                        Nothing,
                                        Nothing,
                                        Nothing)

        Return vdataSet.Tables(0).Rows(0).Item("Texto_Senha")
    End Function

    '----- Função que valida o token para permitir troca de senha do usuario
    Protected Function trocaSenhaConfirmaValidadeTokenCaixaSaida(txtEmail As String, txtUsuario As String) As String

        vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                        "Sd_Valida_Usuario_Com_Token_Valido",
                                        txtUsuario,
                                        Nothing,
                                        Nothing,
                                        txtEmail,
                                        Nothing,
                                        Nothing,
                                        Nothing)

        Return vdataSet.Tables(0).Rows(0).Item("Token_Validade")
    End Function

    '' CHIPAK - INICIO

    Protected Sub btLogin_Click(sender As Object, e As EventArgs) Handles btLogin.Click
        Try

            ' 21032025 - INICIO
            Dim ipUsuario As String = Request.UserHostAddress
            Dim nomeUsuario As String = txtUsuario.Text.Trim()

            If VerificarBloqueio(ipUsuario, nomeUsuario) Then
                lblMsgTokenSenha.Text = "Acesso temporariamente bloqueado por muitas tentativas inválidas. Tente novamente mais tarde."
                lblMsgTokenSenha.Visible = True
                Exit Sub
            End If
            ' 21032025 - FIM

            '-----valida usuario nao autenticado pelo IIS
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            ' Verifica se a Session("Conn_Banco") está definida
            If Session("Conn_Banco") Is Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Session Conn_Banco is Nothing');", True)
                Exit Sub
            End If


            '----- testa se é o primeiro acesso do usuário
            vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                              "Sd_SF_ValidaPrimeiroAcesso",
                                              txtUsuario.Text,
                                              txtSenha.Text,
                                              "", 'txtNovaSenha.Text,
                                              Nothing,
                                              Nothing,
                                              Nothing,
                                              Nothing)

            If vdataSet IsNot Nothing AndAlso vdataSet.Tables.Count > 0 AndAlso vdataSet.Tables(0).Rows.Count > 0 Then
                Dim vNm_Usuario = vdataSet.Tables(0).Rows(0).Item("Nm_Usuario")
                Dim vEmpresa = vdataSet.Tables(0).Rows(0).Item("Empresa")
                Dim vSenhaSegura = vdataSet.Tables(0).Rows(0).Item("Senha_Segura")
                Dim vEmail = vdataSet.Tables(0).Rows(0).Item("EMail")

                If vSenhaSegura = 0 Then
                    Dim token As String = GerarHash32Caracteres(vEmail + DateTime.Now().ToString("yyyyMMddHHmmss"))
                    trocaSenhaPossuiEmailValidoGravaToken(vEmail, txtUsuario.Text, token)

                    'redirecionar para tela de troca de senha
                    Dim url As String = String.Format("Default.aspx?hashts={0}", token)
                    Response.Redirect(url)
                End If
            End If

            '----- Procedimento para envio de token para mudar a senha
            If txtSenha.Visible = False And txtEmail.Visible = True Then
                If txtUsuario.Text = "" Or txtEmail.Text = "" Then
                    lblMsgTokenSenha.Text = "* Preenchimento obrigatório dos campos [Usuário]/[E-Mail]"
                    lblMsgTokenSenha.Visible = True
                    Exit Sub
                Else
                    '----- validar se o usuário possui e-mail
                    Dim possuiEmailValido = trocaSenhaPossuiEmailValido(txtEmail.Text, txtUsuario.Text)

                    If Not possuiEmailValido.IsNullOrEmpty Then
                        Dim token As String = GerarHash32Caracteres(txtEmail.Text + DateTime.Now().ToString("yyyyMMddHHmmss"))
                        trocaSenhaPossuiEmailValidoGravaToken(txtEmail.Text, txtUsuario.Text, token)

                        Dim retornoEnvioToken As String = trocaSenhaGravaTokenCaixaSaida(txtEmail.Text)
                        lblMsgTokenSenha.Text = "* Solicitação de Troca de Senha foi encaminhada por E-Mail"
                        lblMsgTokenSenha.Visible = True

                        rfvEmail.Visible = False
                        txtEmail.Visible = False
                        rfvSenha.Visible = True
                        txtSenha.Visible = True
                        btLogin.Text = "Conectar"
                        btTrocarSenha.Text = "Trocar Senha"
                    Else
                        lblMsgTokenSenha.Text = "* Seu e-mail não foi identificado em nosso cadastro, favor entrar em contato com a área de TI."
                        lblMsgTokenSenha.Visible = True
                    End If

                    Exit Sub
                End If
            End If

            '----- Enviou o token e está válido
            If Request.QueryString("hashts") IsNot Nothing And txtNovaSenha.Visible Then
                If txtUsuario.Text = "" Or txtSenha.Text = "" Or txtNovaSenha.Text = "" Then
                    lblMsgTokenSenha.Text = "Preencha todos os campos para trocar a senha."
                    lblMsgTokenSenha.Visible = True
                    Exit Sub
                End If

                If txtSenha.Text <> txtNovaSenha.Text Then
                    lblMsgTokenSenha.Text = "Os campos [Senha] e [Confirmação de Senha] não são iguais."
                    lblMsgTokenSenha.Visible = True
                    Exit Sub
                End If

                If IsValidPassword(txtSenha.Text) = False Then
                    msgSenhaForteErroRequisitos.Visible = True
                    Exit Sub
                End If

                Dim token As String = Request.QueryString("hashts")
                Dim resultado As (email As String, nmUsuario As String) = DecodificarTokenJwt(token)
                Dim email As String = resultado.email
                Dim nmUsuario As String = resultado.nmUsuario

                If txtSenha.Text = txtNovaSenha.Text Then
                    Dim Texto_Senha = trocaSenhaSalvarAlteracaoSenha(email, nmUsuario, txtSenha.Text)
                    lblMsgTokenSenha.Text = Texto_Senha
                    lblMsgTokenSenha.Visible = True

                    Dim script As String = "alert('Sua senha foi alterada. Estamos redirecionando você, confirme seu acesso.');"
                    btTrocarSenha.Visible = True
                    lblAcesso.Text = "Login"

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ExibirAlerta", script, True)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "LimparParametrosEAtualizar", "window.location.href = window.location.pathname;", True)
                End If

                Exit Sub
            End If

            '----- Troca senha quando a mesma for padrão
            If txtSenha.Text.ToUpper = "ICONTROLIT*" Then
                lblMsgSenha.Visible = True
                Exit Sub
            End If

            '----- Valida usuário e senha 
            vdataSet = WS_Modulo.Validacao_Global(Session("Conn_Banco"),
                                              "Sd_Valida_Usuario",
                                              txtUsuario.Text,
                                              txtSenha.Text,
                                              "", 'txtNovaSenha.Text,
                                              Nothing,
                                              Nothing,
                                              Nothing,
                                              Nothing)

            If vdataSet IsNot Nothing AndAlso vdataSet.Tables.Count > 0 AndAlso vdataSet.Tables(0).Rows.Count > 0 Then
                Session("Id_Usuario") = vdataSet.Tables(0).Rows(0).Item("Id_Usuario")
                Session("Id_Usuario_Perfil_Acesso") = vdataSet.Tables(0).Rows(0).Item("Id_Usuario_Perfil_Acesso")
                Session("Nm_Usuario") = vdataSet.Tables(0).Rows(0).Item("Nm_Usuario")
                Session("Id_Idioma") = vdataSet.Tables(0).Rows(0).Item("Id_Idioma")
                Session("Empresa") = vdataSet.Tables(0).Rows(0).Item("Empresa")
                txtUsuario.Text = vdataSet.Tables(0).Rows(0).Item("Nm_Usuario")
                txtSenha.Text = ""

                ' 21032025 - INICIO
                RegistrarTentativa(ipUsuario, nomeUsuario, True)
                ' 21032025 - FIM


                '----- Monta texto do termo
                For Each Linha In WS_Modulo.Monta_Texto_Termo(Session("Conn_Banco"), "sp_SL_ID", Session("Empresa")).Tables(0).Rows
                    If Linha.Item("Caixa") = "lblTexto1" Then lblTexto1.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto2" Then lblTexto2.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto3" Then lblTexto3.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto4" Then lblTexto4.Text = Linha.Item("Texto")
                    If Linha.Item("Caixa") = "lblTexto5" Then lblTexto5.Text = Linha.Item("Texto")
                Next

                '----- Carrega banco da empresa validada
                For Each Linha In WS_Modulo.Conn_Banco.Tables(0).Rows
                    If Session("Empresa").ToUpper = Linha.Item(1) Then
                        Session("Conn_Banco") = Linha.Item(0)
                    End If
                Next

                If vdataSet.Tables(0).Rows(0).Item("Fl_Desativado") = 3 And Request("Logoff") Is Nothing Then
                    lblTextoNome.Text = "Olá, " + vdataSet.Tables(0).Rows(0).Item("Nm_Consumidor") + " bem vindo ao portal iControlIT."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "Modal('#myModalSubTermo');", True)
                    btLogin.Enabled = False
                    txtUsuario.Enabled = False
                    txtSenha.Enabled = False

                    vdataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Linha_De_Acordo", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)

                    dtgAtivo.DataSource = vdataSet
                    dtgAtivo.DataBind()
                Else
                    If Session("Id_Usuario_Perfil_Acesso") = 1 Then
                        Dim aut = WS_Cadastro.Envia_Log(Session("Conn_Banco"),
                                                    Session("Id_Usuario"),
                                                    DateTime.Now,
                                                    "Usuário logado e autenticado",
                                                    False)
                        Response.Redirect("~/Home.aspx")
                    Else
                        Response.Redirect("~/CockPit_Menu.aspx")
                    End If
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "Modal('#myModal');", True)
                lblMsgTokenSenha.Text = "Usuário ou senha inválidos."
                lblMsgTokenSenha.Visible = True

                ' 21032025 - INICIO
                RegistrarTentativa(ipUsuario, nomeUsuario, False)
                ' 21032025 - INFIMICIO

                Session("Id_Usuario") = Nothing
                Session("Id_Usuario_Perfil_Acesso") = Nothing
                Session("Nm_Usuario") = Nothing
                Session("Id_Idioma") = Nothing
                Page.ClientScript.RegisterStartupScript(GetType(Type), "ClientScript", "<script> document.forms[0].txtUsuario.focus();</script>")
            End If

        Catch ex As Exception
            ' Log do erro e exibição de mensagem amigável ao usuário
            lblMsgTokenSenha.Text = "Ocorreu um erro durante o processo de login. Por favor, tente novamente mais tarde."
            lblMsgTokenSenha.Visible = True
            ' Aqui você pode incluir uma chamada para logar o erro em um banco de dados ou arquivo de log
            ' Por exemplo: LogError(ex)
        End Try
    End Sub


    '' CHIPAK - FIM

    'Protected Sub btnSite_Click(sender As Object, e As EventArgs) Handles btnSite.Click
    '    Response.Redirect("http://IControlIT.com.br/")
    'End Sub

    Function IsValidPassword(password As String) As Boolean
        ' Verificar se a senha tem pelo menos 8 caracteres
        If password.Length < 12 Then
            Return False
        End If

        ' Verificar se a senha contém pelo menos um número
        Dim hasNumber As Boolean = Regex.IsMatch(password, "\d")
        If Not hasNumber Then
            Return False
        End If

        ' Verificar se a senha contém pelo menos um caractere especial
        Dim hasSpecialChar As Boolean = Regex.IsMatch(password, "[^a-zA-Z0-9]")
        If Not hasSpecialChar Then
            Return False
        End If

        ' Verificar se a senha contém pelo menos uma letra maiúscula
        Dim hasUpperCase As Boolean = Regex.IsMatch(password, "[A-Z]")
        If Not hasUpperCase Then
            Return False
        End If

        ' Verificar se a senha contém pelo menos uma letra minúscula
        Dim hasLowerCase As Boolean = Regex.IsMatch(password, "[a-z]")
        If Not hasLowerCase Then
            Return False
        End If

        ' Se todas as verificações passaram, a senha é válida
        Return True
    End Function

    ' 21032025 - INICIO
    ' ----- Proteção força bruta -----
    Sub RegistrarTentativa(ip As String, usuario As String, sucesso As Boolean)
        Dim cache As System.Web.Caching.Cache = HttpContext.Current.Cache
        Dim chaveUsuario As String = "TentativasUsuario_" & usuario
        Dim chaveIP As String = "TentativasIP_" & ip

        If Not sucesso Then
            Dim tentativasUsuario As Integer = 0
            Dim tentativasIP As Integer = 0

            If cache(chaveUsuario) IsNot Nothing Then
                tentativasUsuario = Convert.ToInt32(cache(chaveUsuario))
            End If
            If cache(chaveIP) IsNot Nothing Then
                tentativasIP = Convert.ToInt32(cache(chaveIP))
            End If

            tentativasUsuario += 1
            tentativasIP += 1

            cache.Insert(chaveUsuario, tentativasUsuario, Nothing, DateTime.Now.AddMinutes(TempoBloqueio), TimeSpan.Zero)
            cache.Insert(chaveIP, tentativasIP, Nothing, DateTime.Now.AddMinutes(TempoBloqueio), TimeSpan.Zero)
        Else
            cache.Remove(chaveUsuario)
            cache.Remove(chaveIP)
        End If


    End Sub

    Function VerificarBloqueio(ip As String, usuario As String) As Boolean
        Dim cache As System.Web.Caching.Cache = HttpContext.Current.Cache
        Dim chaveUsuario As String = "TentativasUsuario_" & usuario
        Dim chaveIP As String = "TentativasIP_" & ip

        Dim tentativasUsuario As Integer = If(cache(chaveUsuario) IsNot Nothing, Convert.ToInt32(cache(chaveUsuario)), 0)
        Dim tentativasIP As Integer = If(cache(chaveIP) IsNot Nothing, Convert.ToInt32(cache(chaveIP)), 0)

        If tentativasUsuario >= MaxTentativasUsuario OrElse tentativasIP >= MaxTentativasIP Then
            Return True
        End If

        Return False
    End Function


    ' 21032025 - FIM



End Class
