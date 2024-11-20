Imports Microsoft.VisualBasic
Imports System.Data

Public Class cls_Relacionamento
    Public Sub carregaAcesso(ByVal pId_Usuario_Perfil_Acesso As System.Int32, _
                                    ByVal pCombo_Acesso As WebControls.DropDownList)
        Dim Item As ListItem
        pCombo_Acesso.Items.Clear()
        'adiciona uma linha em branco
        Item = New ListItem
        Item.Text = ""
        pCombo_Acesso.Items.Add(Item)

        'id_usuario_perfil_Acesso = 1 (Master)
        'acesso total ao sistema e aos dados do sistema
        If pId_Usuario_Perfil_Acesso = 1 Then
            pCombo_Acesso.Enabled = False
            Exit Sub
        End If


        'id_usuario_perfil_Acesso = 3 (Gestor Consumidor)
        'id_usuario_perfil_Acesso = 4 (Gestor Filial)
        'a confiuracao da filial e ativo e aberta para todos os gestore sendo obrigatorio
        'a selecao de uma filial para 
        Item = New ListItem
        Item.Value = "sp_Perfil_Acesso_Filial"
        Item.Text = "Filial"
        pCombo_Acesso.Items.Add(Item)

        Item = New ListItem
        Item.Value = "sp_Perfil_Acesso_Consumidor"
        Item.Text = "Consumidor"
        pCombo_Acesso.Items.Add(Item)

        'id_usuario_perfil_Acesso = 2 (Conglomerado)
        'id_usuario_perfil_Acesso = 5 (Centro de Custo)
        'id_usuario_perfil_Acesso = 6 (Departamento)
        'id_usuario_perfil_Acesso = 7 (Setor)
        'id_usuario_perfil_Acesso = 8 (Secao)
        Item = New ListItem

        If pId_Usuario_Perfil_Acesso = 2 Then
            Item.Value = "sp_Perfil_Acesso_Conglomerado"
            Item.Text = "Conglomerado"
        End If

        If pId_Usuario_Perfil_Acesso = 5 Then
            Item.Value = "sp_Perfil_Acesso_Centro_Custo"
            Item.Text = "Centro de Custo"
        End If

        If pId_Usuario_Perfil_Acesso = 6 Then
            Item.Value = "sp_Perfil_Acesso_Departamento"
            Item.Text = "Departamento"
        End If

        If pId_Usuario_Perfil_Acesso = 7 Then
            Item.Value = "sp_Perfil_Acesso_Setor"
            Item.Text = "Setor"
        End If

        If pId_Usuario_Perfil_Acesso = 8 Then
            Item.Value = "sp_Perfil_Acesso_Secao"
            Item.Text = "Secao"
        End If
        If Not Item.Value = Nothing Then pCombo_Acesso.Items.Add(Item)
    End Sub

End Class

