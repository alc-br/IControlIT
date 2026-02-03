' -----------------------------------------------------------------------
' MobileActionsRequestModel.vb
' Autor: Anderson Luiz Chipak
' Data: 05/09/2024
' Descrição: Modelo de dados para MobileActions
' -----------------------------------------------------------------------
' Histórico de Modificações
' TICKET - DATA - NOME COMPLETO
' -----------------------------------------------------------------------
Namespace Connect.ServiceNow.Models
    Public Class MobileActionsRequestModel
        Public Property Action As String = String.Empty ' Permite valor vazio
        Public Property RequestNumber As String = String.Empty
        Public Property WorkOrderNumber As String = String.Empty
        Public Property UserName As String = String.Empty
        Public Property UserNumber As String = String.Empty
        Public Property DesignationProduct As String = String.Empty
        Public Property TelecomProvider As String = String.Empty
        Public Property FramingPlan As String = String.Empty
        Public Property MigrationDevice As String = String.Empty
        Public Property ServicePack As String = String.Empty
        Public Property NewAreaCode As String = String.Empty
        Public Property NewUserNumber As String = String.Empty
        Public Property NewTelecomProvider As String = String.Empty
        Public Property CountryDateForRoaming As String = String.Empty
        Public Property AdditionalInformation As String = String.Empty
        Public Property TransactionID As String = String.Empty
    End Class
End Namespace
