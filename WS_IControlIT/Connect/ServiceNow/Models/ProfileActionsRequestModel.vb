' -----------------------------------------------------------------------
' ProfileActionsRequestModel.vb
' Autor: Anderson Luiz Chipak
' Data: 05/09/2024
' Descrição: Modelo de dados para ProfileActions
' -----------------------------------------------------------------------
' Histórico de Modificações
' TICKET - DATA - NOME COMPLETO
' TICKET - DATA - NOME COMPLETO
' -----------------------------------------------------------------------
Namespace Connect.ServiceNow.Models
    Public Class ProfileActionsRequestModel
        Public Property Action As String = String.Empty ' Permite valor vazio
        Public Property RequestNumber As String = String.Empty
        Public Property WorkOrderNumber As String = String.Empty
        Public Property UserName As String = String.Empty
        Public Property UserNumber As String = String.Empty
        Public Property ManagerOrAdm As String = String.Empty
        Public Property ViewProfile As String = String.Empty
        Public Property ManagerNumber As String = String.Empty
        Public Property TransactionID As String = String.Empty
    End Class
End Namespace
