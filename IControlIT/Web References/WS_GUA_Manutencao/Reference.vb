﻿'------------------------------------------------------------------------------
' <auto-generated>
'     O código foi gerado por uma ferramenta.
'     Versão de Tempo de Execução:4.0.30319.42000
'
'     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
'     o código for gerado novamente.
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
'Este código-fonte foi gerado automaticamente por Microsoft.VSDesigner, Versão 4.0.30319.42000.
'
Namespace WS_GUA_Manutencao
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="WSManutencaoSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class WSManutencao
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private LoteOperationCompleted As System.Threading.SendOrPostCallback
        
        Private Script_ExportacaoOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ArquivoPDFOperationCompleted As System.Threading.SendOrPostCallback
        
        Private caixa_entradaOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.IControlIT.My.MySettings.Default.IControlIT_WS_GUA_Manutencao_WSManutencao
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
        Public Event LoteCompleted As LoteCompletedEventHandler
        
        '''<remarks/>
        Public Event Script_ExportacaoCompleted As Script_ExportacaoCompletedEventHandler
        
        '''<remarks/>
        Public Event ArquivoPDFCompleted As ArquivoPDFCompletedEventHandler
        
        '''<remarks/>
        Public Event caixa_entradaCompleted As caixa_entradaCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Lote", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Lote(ByVal pPConn_Banco As String, ByVal pNm_Usuario As String, ByVal pDt_Lote As String, ByVal pId_Lote_Marcacao As Double, ByVal pDt_Visita As Integer, ByVal pDt_Fechamento As Integer, ByVal pDt_Exportacao As Integer, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("Lote", New Object() {pPConn_Banco, pNm_Usuario, pDt_Lote, pId_Lote_Marcacao, pDt_Visita, pDt_Fechamento, pDt_Exportacao, pId_Usuario_Permissao, pPakage, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub LoteAsync(ByVal pPConn_Banco As String, ByVal pNm_Usuario As String, ByVal pDt_Lote As String, ByVal pId_Lote_Marcacao As Double, ByVal pDt_Visita As Integer, ByVal pDt_Fechamento As Integer, ByVal pDt_Exportacao As Integer, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean)
            Me.LoteAsync(pPConn_Banco, pNm_Usuario, pDt_Lote, pId_Lote_Marcacao, pDt_Visita, pDt_Fechamento, pDt_Exportacao, pId_Usuario_Permissao, pPakage, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub LoteAsync(ByVal pPConn_Banco As String, ByVal pNm_Usuario As String, ByVal pDt_Lote As String, ByVal pId_Lote_Marcacao As Double, ByVal pDt_Visita As Integer, ByVal pDt_Fechamento As Integer, ByVal pDt_Exportacao As Integer, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.LoteOperationCompleted Is Nothing) Then
                Me.LoteOperationCompleted = AddressOf Me.OnLoteOperationCompleted
            End If
            Me.InvokeAsync("Lote", New Object() {pPConn_Banco, pNm_Usuario, pDt_Lote, pId_Lote_Marcacao, pDt_Visita, pDt_Fechamento, pDt_Exportacao, pId_Usuario_Permissao, pPakage, pRetorno}, Me.LoteOperationCompleted, userState)
        End Sub
        
        Private Sub OnLoteOperationCompleted(ByVal arg As Object)
            If (Not (Me.LoteCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent LoteCompleted(Me, New LoteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Script_Exportacao", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Script_Exportacao(ByVal pPConn_Banco As String, ByVal pScript As String, ByVal pDt_Expotacao As Date, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("Script_Exportacao", New Object() {pPConn_Banco, pScript, pDt_Expotacao, pId_Usuario_Permissao, pPakage, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub Script_ExportacaoAsync(ByVal pPConn_Banco As String, ByVal pScript As String, ByVal pDt_Expotacao As Date, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean)
            Me.Script_ExportacaoAsync(pPConn_Banco, pScript, pDt_Expotacao, pId_Usuario_Permissao, pPakage, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub Script_ExportacaoAsync(ByVal pPConn_Banco As String, ByVal pScript As String, ByVal pDt_Expotacao As Date, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.Script_ExportacaoOperationCompleted Is Nothing) Then
                Me.Script_ExportacaoOperationCompleted = AddressOf Me.OnScript_ExportacaoOperationCompleted
            End If
            Me.InvokeAsync("Script_Exportacao", New Object() {pPConn_Banco, pScript, pDt_Expotacao, pId_Usuario_Permissao, pPakage, pRetorno}, Me.Script_ExportacaoOperationCompleted, userState)
        End Sub
        
        Private Sub OnScript_ExportacaoOperationCompleted(ByVal arg As Object)
            If (Not (Me.Script_ExportacaoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent Script_ExportacaoCompleted(Me, New Script_ExportacaoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ArquivoPDF", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ArquivoPDF(ByVal pPConn_Banco As String, ByVal pId_Arquivo_PDF As Integer, ByVal pNm_Arquivo_PDF As String, ByVal pTabela_Registro As String, ByVal pId_Registro_Tabela As Integer, ByVal pTamanho As Double, <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> ByVal pArquivo() As Byte, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("ArquivoPDF", New Object() {pPConn_Banco, pId_Arquivo_PDF, pNm_Arquivo_PDF, pTabela_Registro, pId_Registro_Tabela, pTamanho, pArquivo, pId_Usuario_Permissao, pPakage, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ArquivoPDFAsync(ByVal pPConn_Banco As String, ByVal pId_Arquivo_PDF As Integer, ByVal pNm_Arquivo_PDF As String, ByVal pTabela_Registro As String, ByVal pId_Registro_Tabela As Integer, ByVal pTamanho As Double, ByVal pArquivo() As Byte, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean)
            Me.ArquivoPDFAsync(pPConn_Banco, pId_Arquivo_PDF, pNm_Arquivo_PDF, pTabela_Registro, pId_Registro_Tabela, pTamanho, pArquivo, pId_Usuario_Permissao, pPakage, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ArquivoPDFAsync(ByVal pPConn_Banco As String, ByVal pId_Arquivo_PDF As Integer, ByVal pNm_Arquivo_PDF As String, ByVal pTabela_Registro As String, ByVal pId_Registro_Tabela As Integer, ByVal pTamanho As Double, ByVal pArquivo() As Byte, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.ArquivoPDFOperationCompleted Is Nothing) Then
                Me.ArquivoPDFOperationCompleted = AddressOf Me.OnArquivoPDFOperationCompleted
            End If
            Me.InvokeAsync("ArquivoPDF", New Object() {pPConn_Banco, pId_Arquivo_PDF, pNm_Arquivo_PDF, pTabela_Registro, pId_Registro_Tabela, pTamanho, pArquivo, pId_Usuario_Permissao, pPakage, pRetorno}, Me.ArquivoPDFOperationCompleted, userState)
        End Sub
        
        Private Sub OnArquivoPDFOperationCompleted(ByVal arg As Object)
            If (Not (Me.ArquivoPDFCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ArquivoPDFCompleted(Me, New ArquivoPDFCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/caixa_entrada", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function caixa_entrada(ByVal pPConn_Banco As String, ByVal pPakage As String, ByVal pId_Usuario As Integer, ByVal pTexto As String, ByVal pId_Mail_Caixa_Siada As Integer, ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("caixa_entrada", New Object() {pPConn_Banco, pPakage, pId_Usuario, pTexto, pId_Mail_Caixa_Siada, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub caixa_entradaAsync(ByVal pPConn_Banco As String, ByVal pPakage As String, ByVal pId_Usuario As Integer, ByVal pTexto As String, ByVal pId_Mail_Caixa_Siada As Integer, ByVal pRetorno As Boolean)
            Me.caixa_entradaAsync(pPConn_Banco, pPakage, pId_Usuario, pTexto, pId_Mail_Caixa_Siada, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub caixa_entradaAsync(ByVal pPConn_Banco As String, ByVal pPakage As String, ByVal pId_Usuario As Integer, ByVal pTexto As String, ByVal pId_Mail_Caixa_Siada As Integer, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.caixa_entradaOperationCompleted Is Nothing) Then
                Me.caixa_entradaOperationCompleted = AddressOf Me.Oncaixa_entradaOperationCompleted
            End If
            Me.InvokeAsync("caixa_entrada", New Object() {pPConn_Banco, pPakage, pId_Usuario, pTexto, pId_Mail_Caixa_Siada, pRetorno}, Me.caixa_entradaOperationCompleted, userState)
        End Sub
        
        Private Sub Oncaixa_entradaOperationCompleted(ByVal arg As Object)
            If (Not (Me.caixa_entradaCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent caixa_entradaCompleted(Me, New caixa_entradaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub LoteCompletedEventHandler(ByVal sender As Object, ByVal e As LoteCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class LoteCompletedEventArgs
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub Script_ExportacaoCompletedEventHandler(ByVal sender As Object, ByVal e As Script_ExportacaoCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class Script_ExportacaoCompletedEventArgs
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub ArquivoPDFCompletedEventHandler(ByVal sender As Object, ByVal e As ArquivoPDFCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ArquivoPDFCompletedEventArgs
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub caixa_entradaCompletedEventHandler(ByVal sender As Object, ByVal e As caixa_entradaCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class caixa_entradaCompletedEventArgs
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
