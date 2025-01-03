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
Namespace WS_GUA_Estoque
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="WSEstoqueSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class WSEstoque
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private AparelhoOperationCompleted As System.Threading.SendOrPostCallback
        
        Private Nota_FiscalOperationCompleted As System.Threading.SendOrPostCallback
        
        Private Estoque_Endereco_EntregaOperationCompleted As System.Threading.SendOrPostCallback
        
        Private Estoque_ConsumidorOperationCompleted As System.Threading.SendOrPostCallback
        
        Private EstoqueOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.IControlIT.My.MySettings.Default.Ativvus_WS_GUA_Estoque_WSEstoque
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
        Public Event AparelhoCompleted As AparelhoCompletedEventHandler
        
        '''<remarks/>
        Public Event Nota_FiscalCompleted As Nota_FiscalCompletedEventHandler
        
        '''<remarks/>
        Public Event Estoque_Endereco_EntregaCompleted As Estoque_Endereco_EntregaCompletedEventHandler
        
        '''<remarks/>
        Public Event Estoque_ConsumidorCompleted As Estoque_ConsumidorCompletedEventHandler
        
        '''<remarks/>
        Public Event EstoqueCompleted As EstoqueCompletedEventHandler

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Aparelho", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function Aparelho(
                    ByVal pPConn_Banco As String,
                    ByVal pId_Aparelho As Integer,
                    ByVal pNr_Aparelho As String,
                    ByVal pNr_Aparelho_2 As String,
                    ByVal pNr_Linha_Solicitacao As String,
                    ByVal pNr_Chamado As String,
                    ByVal pDt_Chamado As Date,
                    ByVal pNr_Pedido As String,
                    ByVal pDt_Pedido As Date,
                    ByVal pId_Estoque_Nota_Fiscal As Integer,
                    ByVal pId_Conglomerado As Integer,
                    ByVal pId_Aparelho_Tipo As Integer,
                    ByVal pId_Ativo_Tipo As Integer,
                    ByVal pId_Ativo_Modelo As Integer,
                    ByVal pId_Estoque_Aparelho_Status As Integer,
                    ByVal pObservacao As String,
                    ByVal pJustificativa_Desativacao As String,
                    ByVal pId_Estoque_Endereco_Entrega As Integer,
                    ByVal pId_Consumidor As Integer,
                    ByVal pCk_Carregador As Integer,
                    ByVal pCk_Cabousb As Integer,
                    ByVal pCk_Fone As Integer,
                    ByVal pCk_Pelicula As Integer,
                    ByVal pCk_Capaprotecao As Integer,
                    ByVal pId_Usuario_Permissao As Integer,
                    ByVal pPakage As String,
                    ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("Aparelho", New Object() {pPConn_Banco, pId_Aparelho, pNr_Aparelho, pNr_Aparelho_2, pNr_Linha_Solicitacao, pNr_Chamado, pDt_Chamado, pNr_Pedido, pDt_Pedido, pId_Estoque_Nota_Fiscal, pId_Conglomerado, pId_Aparelho_Tipo, pId_Ativo_Tipo, pId_Ativo_Modelo, pId_Estoque_Aparelho_Status, pObservacao, pJustificativa_Desativacao, pId_Estoque_Endereco_Entrega, pId_Consumidor, pCk_Carregador, pCk_Cabousb, pCk_Fone, pCk_Pelicula, pCk_Capaprotecao, pId_Usuario_Permissao, pPakage, pRetorno})
            Return CType(results(0), System.Data.DataSet)
        End Function

        '''<remarks/>
        Public Overloads Sub AparelhoAsync(
                    ByVal pPConn_Banco As String,
                    ByVal pId_Aparelho As Integer,
                    ByVal pNr_Aparelho As String,
                    ByVal pNr_Aparelho_2 As String,
                    ByVal pNr_Linha_Solicitacao As String,
                    ByVal pNr_Chamado As String,
                    ByVal pDt_Chamado As Date,
                    ByVal pNr_Pedido As String,
                    ByVal pDt_Pedido As Date,
                    ByVal pId_Estoque_Nota_Fiscal As Integer,
                    ByVal pId_Conglomerado As Integer,
                    ByVal pId_Aparelho_Tipo As Integer,
                    ByVal pId_Ativo_Tipo As Integer,
                    ByVal pId_Ativo_Modelo As Integer,
                    ByVal pId_Estoque_Aparelho_Status As Integer,
                    ByVal pObservacao As String,
                    ByVal pJustificativa_Desativacao As String,
                    ByVal pId_Estoque_Endereco_Entrega As Integer,
                    ByVal pId_Consumidor As Integer,
                    ByVal pCk_Carregador As Integer,
                    ByVal pCk_Cabousb As Integer,
                    ByVal pCk_Fone As Integer,
                    ByVal pCk_Pelicula As Integer,
                    ByVal pCk_Capaprotecao As Integer,
                    ByVal pId_Usuario_Permissao As Integer,
                    ByVal pPakage As String,
                    ByVal pRetorno As Boolean)
            Me.AparelhoAsync(pPConn_Banco, pId_Aparelho, pNr_Aparelho, pNr_Aparelho_2, pNr_Linha_Solicitacao, pNr_Chamado, pDt_Chamado, pNr_Pedido, pDt_Pedido, pId_Estoque_Nota_Fiscal, pId_Conglomerado, pId_Aparelho_Tipo, pId_Ativo_Tipo, pId_Ativo_Modelo, pId_Estoque_Aparelho_Status, pObservacao, pJustificativa_Desativacao, pId_Estoque_Endereco_Entrega, pId_Consumidor, pCk_Carregador, pCk_Cabousb, pCk_Fone, pCk_Pelicula, pCk_Capaprotecao, pId_Usuario_Permissao, pPakage, pRetorno, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub AparelhoAsync(
                    ByVal pPConn_Banco As String,
                    ByVal pId_Aparelho As Integer,
                    ByVal pNr_Aparelho As String,
                    ByVal pNr_Aparelho_2 As String,
                    ByVal pNr_Linha_Solicitacao As String,
                    ByVal pNr_Chamado As String,
                    ByVal pDt_Chamado As Date,
                    ByVal pNr_Pedido As String,
                    ByVal pDt_Pedido As Date,
                    ByVal pId_Estoque_Nota_Fiscal As Integer,
                    ByVal pId_Conglomerado As Integer,
                    ByVal pId_Aparelho_Tipo As Integer,
                    ByVal pId_Ativo_Tipo As Integer,
                    ByVal pId_Ativo_Modelo As Integer,
                    ByVal pId_Estoque_Aparelho_Status As Integer,
                    ByVal pObservacao As String,
                    ByVal pJustificativa_Desativacao As String,
                    ByVal pId_Estoque_Endereco_Entrega As Integer,
                    ByVal pId_Consumidor As Integer,
                    ByVal pCk_Carregador As Integer,
                    ByVal pCk_Cabousb As Integer,
                    ByVal pCk_Fone As Integer,
                    ByVal pCk_Pelicula As Integer,
                    ByVal pCk_Capaprotecao As Integer,
                    ByVal pId_Usuario_Permissao As Integer,
                    ByVal pPakage As String,
                    ByVal pRetorno As Boolean,
                    ByVal userState As Object)
            If (Me.AparelhoOperationCompleted Is Nothing) Then
                Me.AparelhoOperationCompleted = AddressOf Me.OnAparelhoOperationCompleted
            End If
            Me.InvokeAsync("Aparelho", New Object() {pPConn_Banco, pId_Aparelho, pNr_Aparelho, pNr_Aparelho_2, pNr_Linha_Solicitacao, pNr_Chamado, pDt_Chamado, pNr_Pedido, pDt_Pedido, pId_Estoque_Nota_Fiscal, pId_Conglomerado, pId_Aparelho_Tipo, pId_Ativo_Tipo, pId_Ativo_Modelo, pId_Estoque_Aparelho_Status, pObservacao, pJustificativa_Desativacao, pId_Estoque_Endereco_Entrega, pId_Consumidor, pCk_Carregador, pCk_Cabousb, pCk_Fone, pCk_Pelicula, pCk_Capaprotecao, pId_Usuario_Permissao, pPakage, pRetorno}, Me.AparelhoOperationCompleted, userState)
        End Sub

        Private Sub OnAparelhoOperationCompleted(ByVal arg As Object)
            If (Not (Me.AparelhoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent AparelhoCompleted(Me, New AparelhoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Nota_Fiscal", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Nota_Fiscal(ByVal pPConn_Banco As String, ByVal pId_Estoque_Nota_Fiscal As Integer, ByVal pNr_Nota_Fiscal As String, ByVal pDt_Nota_Fiscal As Date, ByVal pId_Ativo_Fr_Aquisicao As Integer, ByVal pVr_Fr_Aquisicao As Double, ByVal pDt_Inicio_Fr_Aquisicao As Date, ByVal pQtd_Mes_Residuo_Fr_Aquisicao As Integer, ByVal pObservacao As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("Nota_Fiscal", New Object() {pPConn_Banco, pId_Estoque_Nota_Fiscal, pNr_Nota_Fiscal, pDt_Nota_Fiscal, pId_Ativo_Fr_Aquisicao, pVr_Fr_Aquisicao, pDt_Inicio_Fr_Aquisicao, pQtd_Mes_Residuo_Fr_Aquisicao, pObservacao, pId_Usuario_Permissao, pPakage, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub Nota_FiscalAsync(ByVal pPConn_Banco As String, ByVal pId_Estoque_Nota_Fiscal As Integer, ByVal pNr_Nota_Fiscal As String, ByVal pDt_Nota_Fiscal As Date, ByVal pId_Ativo_Fr_Aquisicao As Integer, ByVal pVr_Fr_Aquisicao As Double, ByVal pDt_Inicio_Fr_Aquisicao As Date, ByVal pQtd_Mes_Residuo_Fr_Aquisicao As Integer, ByVal pObservacao As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean)
            Me.Nota_FiscalAsync(pPConn_Banco, pId_Estoque_Nota_Fiscal, pNr_Nota_Fiscal, pDt_Nota_Fiscal, pId_Ativo_Fr_Aquisicao, pVr_Fr_Aquisicao, pDt_Inicio_Fr_Aquisicao, pQtd_Mes_Residuo_Fr_Aquisicao, pObservacao, pId_Usuario_Permissao, pPakage, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub Nota_FiscalAsync(ByVal pPConn_Banco As String, ByVal pId_Estoque_Nota_Fiscal As Integer, ByVal pNr_Nota_Fiscal As String, ByVal pDt_Nota_Fiscal As Date, ByVal pId_Ativo_Fr_Aquisicao As Integer, ByVal pVr_Fr_Aquisicao As Double, ByVal pDt_Inicio_Fr_Aquisicao As Date, ByVal pQtd_Mes_Residuo_Fr_Aquisicao As Integer, ByVal pObservacao As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.Nota_FiscalOperationCompleted Is Nothing) Then
                Me.Nota_FiscalOperationCompleted = AddressOf Me.OnNota_FiscalOperationCompleted
            End If
            Me.InvokeAsync("Nota_Fiscal", New Object() {pPConn_Banco, pId_Estoque_Nota_Fiscal, pNr_Nota_Fiscal, pDt_Nota_Fiscal, pId_Ativo_Fr_Aquisicao, pVr_Fr_Aquisicao, pDt_Inicio_Fr_Aquisicao, pQtd_Mes_Residuo_Fr_Aquisicao, pObservacao, pId_Usuario_Permissao, pPakage, pRetorno}, Me.Nota_FiscalOperationCompleted, userState)
        End Sub
        
        Private Sub OnNota_FiscalOperationCompleted(ByVal arg As Object)
            If (Not (Me.Nota_FiscalCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent Nota_FiscalCompleted(Me, New Nota_FiscalCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Estoque_Endereco_Entrega", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Estoque_Endereco_Entrega(ByVal pPConn_Banco As String, ByVal pId_Estoque_Endereco_Entrega As Integer, ByVal pNm_Estoque_Endereco_Entrega As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("Estoque_Endereco_Entrega", New Object() {pPConn_Banco, pId_Estoque_Endereco_Entrega, pNm_Estoque_Endereco_Entrega, pId_Usuario_Permissao, pPakage, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub Estoque_Endereco_EntregaAsync(ByVal pPConn_Banco As String, ByVal pId_Estoque_Endereco_Entrega As Integer, ByVal pNm_Estoque_Endereco_Entrega As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean)
            Me.Estoque_Endereco_EntregaAsync(pPConn_Banco, pId_Estoque_Endereco_Entrega, pNm_Estoque_Endereco_Entrega, pId_Usuario_Permissao, pPakage, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub Estoque_Endereco_EntregaAsync(ByVal pPConn_Banco As String, ByVal pId_Estoque_Endereco_Entrega As Integer, ByVal pNm_Estoque_Endereco_Entrega As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.Estoque_Endereco_EntregaOperationCompleted Is Nothing) Then
                Me.Estoque_Endereco_EntregaOperationCompleted = AddressOf Me.OnEstoque_Endereco_EntregaOperationCompleted
            End If
            Me.InvokeAsync("Estoque_Endereco_Entrega", New Object() {pPConn_Banco, pId_Estoque_Endereco_Entrega, pNm_Estoque_Endereco_Entrega, pId_Usuario_Permissao, pPakage, pRetorno}, Me.Estoque_Endereco_EntregaOperationCompleted, userState)
        End Sub
        
        Private Sub OnEstoque_Endereco_EntregaOperationCompleted(ByVal arg As Object)
            If (Not (Me.Estoque_Endereco_EntregaCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent Estoque_Endereco_EntregaCompleted(Me, New Estoque_Endereco_EntregaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Estoque_Consumidor", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Estoque_Consumidor(ByVal pPConn_Banco As String, ByVal pId_Consumidor As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("Estoque_Consumidor", New Object() {pPConn_Banco, pId_Consumidor, pId_Usuario_Permissao, pPakage, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub Estoque_ConsumidorAsync(ByVal pPConn_Banco As String, ByVal pId_Consumidor As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean)
            Me.Estoque_ConsumidorAsync(pPConn_Banco, pId_Consumidor, pId_Usuario_Permissao, pPakage, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub Estoque_ConsumidorAsync(ByVal pPConn_Banco As String, ByVal pId_Consumidor As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.Estoque_ConsumidorOperationCompleted Is Nothing) Then
                Me.Estoque_ConsumidorOperationCompleted = AddressOf Me.OnEstoque_ConsumidorOperationCompleted
            End If
            Me.InvokeAsync("Estoque_Consumidor", New Object() {pPConn_Banco, pId_Consumidor, pId_Usuario_Permissao, pPakage, pRetorno}, Me.Estoque_ConsumidorOperationCompleted, userState)
        End Sub
        
        Private Sub OnEstoque_ConsumidorOperationCompleted(ByVal arg As Object)
            If (Not (Me.Estoque_ConsumidorCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent Estoque_ConsumidorCompleted(Me, New Estoque_ConsumidorCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Estoque", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Estoque(ByVal pPConn_Banco As String, ByVal pId_Consumidor As String, ByVal pNr_Ativo As String, ByVal pNr_Pedido As String, ByVal pNr_Nota_Fiscal As String, ByVal pId_Aparelho As Integer, ByVal pId_Ativo As Integer, ByVal pObservacao As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean) As System.Data.DataSet
            Dim results() As Object = Me.Invoke("Estoque", New Object() {pPConn_Banco, pId_Consumidor, pNr_Ativo, pNr_Pedido, pNr_Nota_Fiscal, pId_Aparelho, pId_Ativo, pObservacao, pId_Usuario_Permissao, pPakage, pRetorno})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub EstoqueAsync(ByVal pPConn_Banco As String, ByVal pId_Consumidor As String, ByVal pNr_Ativo As String, ByVal pNr_Pedido As String, ByVal pNr_Nota_Fiscal As String, ByVal pId_Aparelho As Integer, ByVal pId_Ativo As Integer, ByVal pObservacao As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean)
            Me.EstoqueAsync(pPConn_Banco, pId_Consumidor, pNr_Ativo, pNr_Pedido, pNr_Nota_Fiscal, pId_Aparelho, pId_Ativo, pObservacao, pId_Usuario_Permissao, pPakage, pRetorno, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub EstoqueAsync(ByVal pPConn_Banco As String, ByVal pId_Consumidor As String, ByVal pNr_Ativo As String, ByVal pNr_Pedido As String, ByVal pNr_Nota_Fiscal As String, ByVal pId_Aparelho As Integer, ByVal pId_Ativo As Integer, ByVal pObservacao As String, ByVal pId_Usuario_Permissao As Integer, ByVal pPakage As String, ByVal pRetorno As Boolean, ByVal userState As Object)
            If (Me.EstoqueOperationCompleted Is Nothing) Then
                Me.EstoqueOperationCompleted = AddressOf Me.OnEstoqueOperationCompleted
            End If
            Me.InvokeAsync("Estoque", New Object() {pPConn_Banco, pId_Consumidor, pNr_Ativo, pNr_Pedido, pNr_Nota_Fiscal, pId_Aparelho, pId_Ativo, pObservacao, pId_Usuario_Permissao, pPakage, pRetorno}, Me.EstoqueOperationCompleted, userState)
        End Sub
        
        Private Sub OnEstoqueOperationCompleted(ByVal arg As Object)
            If (Not (Me.EstoqueCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent EstoqueCompleted(Me, New EstoqueCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub AparelhoCompletedEventHandler(ByVal sender As Object, ByVal e As AparelhoCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class AparelhoCompletedEventArgs
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub Nota_FiscalCompletedEventHandler(ByVal sender As Object, ByVal e As Nota_FiscalCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class Nota_FiscalCompletedEventArgs
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub Estoque_Endereco_EntregaCompletedEventHandler(ByVal sender As Object, ByVal e As Estoque_Endereco_EntregaCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class Estoque_Endereco_EntregaCompletedEventArgs
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub Estoque_ConsumidorCompletedEventHandler(ByVal sender As Object, ByVal e As Estoque_ConsumidorCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class Estoque_ConsumidorCompletedEventArgs
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub EstoqueCompletedEventHandler(ByVal sender As Object, ByVal e As EstoqueCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class EstoqueCompletedEventArgs
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
