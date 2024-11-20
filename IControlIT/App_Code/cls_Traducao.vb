Imports IControlIT
Imports Microsoft.VisualBasic

Public Class cls_Traducao
    Dim v_dataSet As New Data.DataSet

    Public Sub Permissao_Tela(ByVal Pagina As System.String)
        'v_dataSet = HttpContext.Current.Session("SubMenu")
        'Dim vLDataSet As Data.DataRow
        'For Each vLDataSet In v_dataSet.Tables(0).Rows
        '    If vLDataSet.Item("Pagina") = Pagina.Substring(Pagina.LastIndexOf("/") + 1) Then Exit Sub
        'Next
        'HttpContext.Current.Response.Redirect("~/Main.aspx")
    End Sub

    Function Traduzir(ByVal pConn_Banco As System.String, _
                        ByVal me_Form As ContentPlaceHolder, _
                        ByVal Pagina As System.String, _
                        ByVal Id_Idioma As System.Int32) As System.String

        Traduzir = Nothing
        Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
        WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials
        v_dataSet = WS_Modulo.Validacao(pConn_Banco, "Sd_Traducao", Nothing, Nothing, Nothing, Pagina.Substring(Pagina.LastIndexOf("/") + 1), Id_Idioma)
        Dim vLDataSet As Data.DataRow

        For Each vLDataSet In v_dataSet.Tables(0).Rows
            If vLDataSet.Item("Nm_Campo") = "Title" Then
                Traduzir = vLDataSet.Item("Traducao")
            Else
                '-----tipo objeto (label)
                If me_Form.FindControl(vLDataSet.Item("Nm_Campo")).GetType.ToString = "System.Web.UI.WebControls.Label" Then
                    Dim v_Label As System.Web.UI.WebControls.Label
                    v_Label = me_Form.FindControl(vLDataSet.Item("Nm_Campo"))
                    If vLDataSet.Item("Propriedade") = "Text" Then v_Label.Text = vLDataSet.Item("Traducao")
                End If

                '-----tipo objeto (button)
                If me_Form.FindControl(vLDataSet.Item("Nm_Campo")).GetType.ToString = "System.Web.UI.WebControls.Button" Then
                    Dim v_Button As System.Web.UI.WebControls.Button
                    v_Button = me_Form.FindControl(vLDataSet.Item("Nm_Campo"))
                    If vLDataSet.Item("Propriedade") = "Text" Then v_Button.Text = vLDataSet.Item("Traducao")
                    If vLDataSet.Item("Propriedade") = "OnClientClick" Then v_Button.OnClientClick = vLDataSet.Item("Traducao")
                    If vLDataSet.Item("Propriedade") = "ToolTip" Then v_Button.ToolTip = vLDataSet.Item("Traducao")
                End If

                '-----tipo objeto (ImageButton)
                If me_Form.FindControl(vLDataSet.Item("Nm_Campo")).GetType.ToString = "System.Web.UI.WebControls.ImageButton" Then
                    Dim v_Button As System.Web.UI.WebControls.ImageButton
                    v_Button = me_Form.FindControl(vLDataSet.Item("Nm_Campo"))
                    If vLDataSet.Item("Propriedade") = "ToolTip" Then v_Button.ToolTip = vLDataSet.Item("Traducao")
                End If

                '-----tipo objeto (requiredfieldvalidator)
                If me_Form.FindControl(vLDataSet.Item("Nm_Campo")).GetType.ToString = "System.Web.UI.WebControls.RequiredFieldValidator" Then
                    Dim v_RequiredFieldValidator As System.Web.UI.WebControls.RequiredFieldValidator
                    v_RequiredFieldValidator = me_Form.FindControl(vLDataSet.Item("Nm_Campo"))
                    If vLDataSet.Item("Propriedade") = "ErrorMessage" Then v_RequiredFieldValidator.ErrorMessage = vLDataSet.Item("Traducao")
                End If

                '-----tipo objeto (HyperLink)
                If me_Form.FindControl(vLDataSet.Item("Nm_Campo")).GetType.ToString = "System.Web.UI.WebControls.HyperLink" Then
                    Dim v_HyperLink As System.Web.UI.WebControls.HyperLink
                    v_HyperLink = me_Form.FindControl(vLDataSet.Item("Nm_Campo"))
                    If vLDataSet.Item("Propriedade") = "Text" Then v_HyperLink.Text = vLDataSet.Item("Traducao")
                End If
            End If
        Next
    End Function

    Function Descriptografar(ByVal pString As System.String) As System.String
        Dim pSenha As System.String
        pSenha = "GUA@123"

        Dim chavecript As String = ""
        Dim chavecript_crc As String = ""
        Dim chavecript_key As String = ""
        Dim chavecriptcompleta As String = ""
        Dim X As Int32 = 0
        Dim I As Int32 = 0
        Dim Y As Int32 = 0
        Dim Z As Int32 = 0
        Dim W As Int32 = 0
        Dim Validade As Int32 = -1

        Dim vConvert_1 As String = ""

        For X = 1 To Len(pString)
            Select Case Mid(pString, X, 1)
                Case "A"
                    vConvert_1 = "0"
                Case "B"
                    vConvert_1 = "0"
                Case "C"
                    vConvert_1 = "1"
                Case "D"
                    vConvert_1 = "1"
                Case "E"
                    vConvert_1 = "2"
                Case "F"
                    vConvert_1 = "2"
                Case "G"
                    vConvert_1 = "3"
                Case "H"
                    vConvert_1 = "3"
                Case "I"
                    vConvert_1 = "4"
                Case "J"
                    vConvert_1 = "4"
                Case "K"
                    vConvert_1 = "5"
                Case "L"
                    vConvert_1 = "5"
                Case "M"
                    vConvert_1 = "6"
                Case "N"
                    vConvert_1 = "6"
                Case "O"
                    vConvert_1 = "7"
                Case "P"
                    vConvert_1 = "7"
                Case "Q"
                    vConvert_1 = "8"
                Case "R"
                    vConvert_1 = "8"
                Case "S"
                    vConvert_1 = "9"
                Case "T"
                    vConvert_1 = "9"
            End Select
            chavecript = chavecript + vConvert_1
        Next

        chavecript_crc = Mid(chavecript, 1, 5)
        chavecript_key = Mid(chavecript, 6, Len(chavecript))
        pString = Nothing
        X = 1

        While X <= 5
            Z = Len(chavecript_key) + 2
            W = 0
            Y = 1

            While Y <= Len(chavecript_key)
                W = W + (Asc(Mid(chavecript_key, Y, 1)) * Z)
                Z = Z - 1
                Y = Y + 1
            End While

            W = CType(Math.Round(W / 9.0, 0), Int32) Mod 9
            chavecript_key = CType(W, String) + chavecript_key
            X = X + 1
        End While

        If chavecript_crc = Mid(chavecript_key, 1, 5) Then
            chavecript_key = Mid(chavecript_key, 6, Len(chavecript_key))

            For X = 1 To Len(chavecript_key)
                If X Mod 2 = 0 Then
                    chavecriptcompleta = chavecriptcompleta + Char.ConvertFromUtf32(CType(Mid(chavecript_key, X - 1, 1) + Mid(chavecript_key, X, 1), Int32))
                End If
            Next

            For I = 1 To 10
                If pSenha = Mid(chavecriptcompleta, 1, I) Then
                    Validade = I + 1
                End If
            Next

            If Validade = -1 Then
                chavecriptcompleta = Nothing
            Else
                chavecriptcompleta = Mid(chavecriptcompleta, Validade, Len(chavecriptcompleta))
            End If
        End If
        Return chavecriptcompleta
    End Function

End Class
