﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
'
Namespace WS_GUA_Chamado
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="WSChamadoSoap", [Namespace]:="WSChamado")>  _
    Partial Public Class WSChamado
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private ChamadoOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ExecutarAcaoAtivoOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ChamadoAuxiliarOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ChamadoAuxiliarComAnexosOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.IControlIT.My.MySettings.Default.IControlIT_localhost_WSChamado
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event ChamadoCompleted As ChamadoCompletedEventHandler
        
        '''<remarks/>
        Public Event ExecutarAcaoAtivoCompleted As ExecutarAcaoAtivoCompletedEventHandler
        
        '''<remarks/>
        Public Event ChamadoAuxiliarCompleted As ChamadoAuxiliarCompletedEventHandler
        
        '''<remarks/>
        Public Event ChamadoAuxiliarComAnexosCompleted As ChamadoAuxiliarComAnexosCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("WSChamado/Chamado", RequestNamespace:="WSChamado", ResponseNamespace:="WSChamado", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Chamado( _
                    ByVal pPConn_Banco As String,  _
                    ByVal pPakage As String,  _
                    ByVal pageNumber As Integer,  _
                    ByVal pageSize As Integer,  _
                    ByVal idChamado As Integer,  _
                    ByVal requestNumber As String,  _
                    ByVal workOrderNumber As String,  _
                    ByVal estado As String,  _
                    ByVal comentarios As String,  _
                    ByVal atribuidoPara As String,  _
                    ByVal tipoSolicitacao As String,  _
                    ByVal transactionID As String,  _
                    ByVal idConsumidor As Integer,  _
                    ByVal idAtivo As Integer,  _
                    ByVal idConglomerado As Integer,  _
                    ByVal userName As String,  _
                    ByVal userNumber As String,  _
                    ByVal designationProduct As String,  _
                    ByVal telecomProvider As String,  _
                    ByVal framingPlan As String,  _
                    ByVal migrationDevice As String,  _
                    ByVal servicePack As String,  _
                    ByVal newAreaCode As String,  _
                    ByVal newUserNumber As String,  _
                    ByVal newTelecomProvider As String,  _
                    ByVal countryDateForRoaming As String,  _
                    ByVal managerOrAdm As String,  _
                    ByVal viewProfile As String,  _
                    ByVal managerNumber As String,  _
                    ByVal additionalInformation As String,  _
                    ByVal name As String,  _
                    ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("Chamado", New Object() {pPConn_Banco, pPakage, pageNumber, pageSize, idChamado, requestNumber, workOrderNumber, estado, comentarios, atribuidoPara, tipoSolicitacao, transactionID, idConsumidor, idAtivo, idConglomerado, userName, userNumber, designationProduct, telecomProvider, framingPlan, migrationDevice, servicePack, newAreaCode, newUserNumber, newTelecomProvider, countryDateForRoaming, managerOrAdm, viewProfile, managerNumber, additionalInformation, name, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ChamadoAsync( _
                    ByVal pPConn_Banco As String,  _
                    ByVal pPakage As String,  _
                    ByVal pageNumber As Integer,  _
                    ByVal pageSize As Integer,  _
                    ByVal idChamado As Integer,  _
                    ByVal requestNumber As String,  _
                    ByVal workOrderNumber As String,  _
                    ByVal estado As String,  _
                    ByVal comentarios As String,  _
                    ByVal atribuidoPara As String,  _
                    ByVal tipoSolicitacao As String,  _
                    ByVal transactionID As String,  _
                    ByVal idConsumidor As Integer,  _
                    ByVal idAtivo As Integer,  _
                    ByVal idConglomerado As Integer,  _
                    ByVal userName As String,  _
                    ByVal userNumber As String,  _
                    ByVal designationProduct As String,  _
                    ByVal telecomProvider As String,  _
                    ByVal framingPlan As String,  _
                    ByVal migrationDevice As String,  _
                    ByVal servicePack As String,  _
                    ByVal newAreaCode As String,  _
                    ByVal newUserNumber As String,  _
                    ByVal newTelecomProvider As String,  _
                    ByVal countryDateForRoaming As String,  _
                    ByVal managerOrAdm As String,  _
                    ByVal viewProfile As String,  _
                    ByVal managerNumber As String,  _
                    ByVal additionalInformation As String,  _
                    ByVal name As String,  _
                    ByVal pRetorno As Boolean)
            Me.ChamadoAsync(pPConn_Banco, pPakage, pageNumber, pageSize, idChamado, requestNumber, workOrderNumber, estado, comentarios, atribuidoPara, tipoSolicitacao, transactionID, idConsumidor, idAtivo, idConglomerado, userName, userNumber, designationProduct, telecomProvider, framingPlan, migrationDevice, servicePack, newAreaCode, newUserNumber, newTelecomProvider, countryDateForRoaming, managerOrAdm, viewProfile, managerNumber, additionalInformation, name, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ChamadoAsync( _
                    ByVal pPConn_Banco As String,  _
                    ByVal pPakage As String,  _
                    ByVal pageNumber As Integer,  _
                    ByVal pageSize As Integer,  _
                    ByVal idChamado As Integer,  _
                    ByVal requestNumber As String,  _
                    ByVal workOrderNumber As String,  _
                    ByVal estado As String,  _
                    ByVal comentarios As String,  _
                    ByVal atribuidoPara As String,  _
                    ByVal tipoSolicitacao As String,  _
                    ByVal transactionID As String,  _
                    ByVal idConsumidor As Integer,  _
                    ByVal idAtivo As Integer,  _
                    ByVal idConglomerado As Integer,  _
                    ByVal userName As String,  _
                    ByVal userNumber As String,  _
                    ByVal designationProduct As String,  _
                    ByVal telecomProvider As String,  _
                    ByVal framingPlan As String,  _
                    ByVal migrationDevice As String,  _
                    ByVal servicePack As String,  _
                    ByVal newAreaCode As String,  _
                    ByVal newUserNumber As String,  _
                    ByVal newTelecomProvider As String,  _
                    ByVal countryDateForRoaming As String,  _
                    ByVal managerOrAdm As String,  _
                    ByVal viewProfile As String,  _
                    ByVal managerNumber As String,  _
                    ByVal additionalInformation As String,  _
                    ByVal name As String,  _
                    ByVal pRetorno As Boolean,  _
                    ByVal userState As Object)
            If (Me.ChamadoOperationCompleted Is Nothing) Then
                Me.ChamadoOperationCompleted = AddressOf Me.OnChamadoOperationCompleted
            End If
            Me.InvokeAsync("Chamado", New Object() {pPConn_Banco, pPakage, pageNumber, pageSize, idChamado, requestNumber, workOrderNumber, estado, comentarios, atribuidoPara, tipoSolicitacao, transactionID, idConsumidor, idAtivo, idConglomerado, userName, userNumber, designationProduct, telecomProvider, framingPlan, migrationDevice, servicePack, newAreaCode, newUserNumber, newTelecomProvider, countryDateForRoaming, managerOrAdm, viewProfile, managerNumber, additionalInformation, name, pRetorno}, Me.ChamadoOperationCompleted, userState)
        End Sub
        
        Private Sub OnChamadoOperationCompleted(ByVal arg As Object)
            If (Not (Me.ChamadoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ChamadoCompleted(Me, New ChamadoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("WSChamado/ExecutarAcaoAtivo", RequestNamespace:="WSChamado", ResponseNamespace:="WSChamado", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ExecutarAcaoAtivo(ByVal pPConn_Banco As String, ByVal procedureName As String, ByVal pPakage As String, ByVal pId_Chamado As Integer, ByVal pComentariosAtivo As String, ByVal pCampo1 As String, ByVal pCampo2 As String, ByVal pCampo3 As String, ByVal pCampo4 As String, ByVal pCampo5 As String, ByVal pCampo6 As String, ByVal pRetorno As Boolean) As String
            Dim results() As Object = Me.Invoke("ExecutarAcaoAtivo", New Object() {pPConn_Banco, procedureName, pPakage, pId_Chamado, pComentariosAtivo, pCampo1, pCampo2, pCampo3, pCampo4, pCampo5, pCampo6, pRetorno})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ExecutarAcaoAtivoAsync(ByVal pPConn_Banco As String, ByVal procedureName As String, ByVal pPakage As String, ByVal pId_Chamado As Integer, ByVal pComentariosAtivo As String, ByVal pCampo1 As String, ByVal pCampo2 As String, ByVal pCampo3 As String, ByVal pCampo4 As String, ByVal pCampo5 As String, ByVal pCampo6 As String, ByVal pRetorno As Boolean)
            Me.ExecutarAcaoAtivoAsync(pPConn_Banco, procedureName, pPakage, pId_Chamado, pComentariosAtivo, pCampo1, pCampo2, pCampo3, pCampo4, pCampo5, pCampo6, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ExecutarAcaoAtivoAsync(ByVal pPConn_Banco As String, ByVal procedureName As String, ByVal pPakage As String, ByVal pId_Chamado As Integer, ByVal pComentariosAtivo As String, ByVal pCampo1 As String, ByVal pCampo2 As String, ByVal pCampo3 As String, ByVal pCampo4 As String, ByVal pCampo5 As String, ByVal pCampo6 As String, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.ExecutarAcaoAtivoOperationCompleted Is Nothing) Then
                Me.ExecutarAcaoAtivoOperationCompleted = AddressOf Me.OnExecutarAcaoAtivoOperationCompleted
            End If
            Me.InvokeAsync("ExecutarAcaoAtivo", New Object() {pPConn_Banco, procedureName, pPakage, pId_Chamado, pComentariosAtivo, pCampo1, pCampo2, pCampo3, pCampo4, pCampo5, pCampo6, pRetorno}, Me.ExecutarAcaoAtivoOperationCompleted, userState)
        End Sub
        
        Private Sub OnExecutarAcaoAtivoOperationCompleted(ByVal arg As Object)
            If (Not (Me.ExecutarAcaoAtivoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ExecutarAcaoAtivoCompleted(Me, New ExecutarAcaoAtivoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("WSChamado/ChamadoAuxiliar", RequestNamespace:="WSChamado", ResponseNamespace:="WSChamado", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ChamadoAuxiliar(ByVal pPConn_Banco As String, ByVal pAcao As String, ByVal id_Conglomerado As Integer, ByVal id_Ativo As Integer, ByVal pEmailDestino As String, ByVal pEmailCopia As String, ByVal id_Mail_Sender As Integer, ByVal pTextoAdicional As String, ByVal pAssuntoEmail As String, ByVal pNmUsuarioAtual As String, ByVal pId_Chamado As String, ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("ChamadoAuxiliar", New Object() {pPConn_Banco, pAcao, id_Conglomerado, id_Ativo, pEmailDestino, pEmailCopia, id_Mail_Sender, pTextoAdicional, pAssuntoEmail, pNmUsuarioAtual, pId_Chamado, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ChamadoAuxiliarAsync(ByVal pPConn_Banco As String, ByVal pAcao As String, ByVal id_Conglomerado As Integer, ByVal id_Ativo As Integer, ByVal pEmailDestino As String, ByVal pEmailCopia As String, ByVal id_Mail_Sender As Integer, ByVal pTextoAdicional As String, ByVal pAssuntoEmail As String, ByVal pNmUsuarioAtual As String, ByVal pId_Chamado As String, ByVal pRetorno As Boolean)
            Me.ChamadoAuxiliarAsync(pPConn_Banco, pAcao, id_Conglomerado, id_Ativo, pEmailDestino, pEmailCopia, id_Mail_Sender, pTextoAdicional, pAssuntoEmail, pNmUsuarioAtual, pId_Chamado, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ChamadoAuxiliarAsync(ByVal pPConn_Banco As String, ByVal pAcao As String, ByVal id_Conglomerado As Integer, ByVal id_Ativo As Integer, ByVal pEmailDestino As String, ByVal pEmailCopia As String, ByVal id_Mail_Sender As Integer, ByVal pTextoAdicional As String, ByVal pAssuntoEmail As String, ByVal pNmUsuarioAtual As String, ByVal pId_Chamado As String, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.ChamadoAuxiliarOperationCompleted Is Nothing) Then
                Me.ChamadoAuxiliarOperationCompleted = AddressOf Me.OnChamadoAuxiliarOperationCompleted
            End If
            Me.InvokeAsync("ChamadoAuxiliar", New Object() {pPConn_Banco, pAcao, id_Conglomerado, id_Ativo, pEmailDestino, pEmailCopia, id_Mail_Sender, pTextoAdicional, pAssuntoEmail, pNmUsuarioAtual, pId_Chamado, pRetorno}, Me.ChamadoAuxiliarOperationCompleted, userState)
        End Sub
        
        Private Sub OnChamadoAuxiliarOperationCompleted(ByVal arg As Object)
            If (Not (Me.ChamadoAuxiliarCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ChamadoAuxiliarCompleted(Me, New ChamadoAuxiliarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("WSChamado/ChamadoAuxiliarComAnexos", RequestNamespace:="WSChamado", ResponseNamespace:="WSChamado", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ChamadoAuxiliarComAnexos(ByVal pPConn_Banco As String, ByVal pAcao As String, ByVal id_Conglomerado As Integer, ByVal id_Ativo As Integer, ByVal pEmailDestino As String, ByVal pEmailCopia As String, ByVal id_Mail_Sender As Integer, ByVal pTextoAdicional As String, ByVal pAssuntoEmail As String, ByVal pEnderecoArquivo As String, ByVal pNmUsuarioAtual As String, ByVal pId_Chamado As String, ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("ChamadoAuxiliarComAnexos", New Object() {pPConn_Banco, pAcao, id_Conglomerado, id_Ativo, pEmailDestino, pEmailCopia, id_Mail_Sender, pTextoAdicional, pAssuntoEmail, pEnderecoArquivo, pNmUsuarioAtual, pId_Chamado, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ChamadoAuxiliarComAnexosAsync(ByVal pPConn_Banco As String, ByVal pAcao As String, ByVal id_Conglomerado As Integer, ByVal id_Ativo As Integer, ByVal pEmailDestino As String, ByVal pEmailCopia As String, ByVal id_Mail_Sender As Integer, ByVal pTextoAdicional As String, ByVal pAssuntoEmail As String, ByVal pEnderecoArquivo As String, ByVal pNmUsuarioAtual As String, ByVal pId_Chamado As String, ByVal pRetorno As Boolean)
            Me.ChamadoAuxiliarComAnexosAsync(pPConn_Banco, pAcao, id_Conglomerado, id_Ativo, pEmailDestino, pEmailCopia, id_Mail_Sender, pTextoAdicional, pAssuntoEmail, pEnderecoArquivo, pNmUsuarioAtual, pId_Chamado, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ChamadoAuxiliarComAnexosAsync(ByVal pPConn_Banco As String, ByVal pAcao As String, ByVal id_Conglomerado As Integer, ByVal id_Ativo As Integer, ByVal pEmailDestino As String, ByVal pEmailCopia As String, ByVal id_Mail_Sender As Integer, ByVal pTextoAdicional As String, ByVal pAssuntoEmail As String, ByVal pEnderecoArquivo As String, ByVal pNmUsuarioAtual As String, ByVal pId_Chamado As String, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.ChamadoAuxiliarComAnexosOperationCompleted Is Nothing) Then
                Me.ChamadoAuxiliarComAnexosOperationCompleted = AddressOf Me.OnChamadoAuxiliarComAnexosOperationCompleted
            End If
            Me.InvokeAsync("ChamadoAuxiliarComAnexos", New Object() {pPConn_Banco, pAcao, id_Conglomerado, id_Ativo, pEmailDestino, pEmailCopia, id_Mail_Sender, pTextoAdicional, pAssuntoEmail, pEnderecoArquivo, pNmUsuarioAtual, pId_Chamado, pRetorno}, Me.ChamadoAuxiliarComAnexosOperationCompleted, userState)
        End Sub
        
        Private Sub OnChamadoAuxiliarComAnexosOperationCompleted(ByVal arg As Object)
            If (Not (Me.ChamadoAuxiliarComAnexosCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ChamadoAuxiliarComAnexosCompleted(Me, New ChamadoAuxiliarComAnexosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")>  _
    Public Delegate Sub ChamadoCompletedEventHandler(ByVal sender As Object, ByVal e As ChamadoCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ChamadoCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As System.Data.DataSet
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),System.Data.DataSet)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")>  _
    Public Delegate Sub ExecutarAcaoAtivoCompletedEventHandler(ByVal sender As Object, ByVal e As ExecutarAcaoAtivoCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ExecutarAcaoAtivoCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")>  _
    Public Delegate Sub ChamadoAuxiliarCompletedEventHandler(ByVal sender As Object, ByVal e As ChamadoAuxiliarCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ChamadoAuxiliarCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As System.Data.DataSet
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),System.Data.DataSet)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")>  _
    Public Delegate Sub ChamadoAuxiliarComAnexosCompletedEventHandler(ByVal sender As Object, ByVal e As ChamadoAuxiliarComAnexosCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ChamadoAuxiliarComAnexosCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As System.Data.DataSet
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),System.Data.DataSet)
            End Get
        End Property
    End Class
End Namespace