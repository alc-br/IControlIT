' -----------------------------------------------------------------------
' WSSincronizacao.asmx
' Autor: Anderson Luiz Chipak - Agencia ALC
' Data: 05/12/2024
' Descrição:
'   Web Service que recebe dados em XML (lotes) prontos para MERGE,
'   chamando a pa_SincronizarEstruturaOrganizacional com @pAcao e @XmlData.
' -----------------------------------------------------------------------

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.IO

<WebService(Namespace:="WSSincronizacao", Name:="WSSincronizacao")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSSincronizacao
    Inherits WebService

    Dim oBanco As cls_Banco
    Private logFilePath As String = "C:\Temp\LogWSSincronizacao.txt"

    Public Sub New()
        Try
            oBanco = New cls_Banco()
            If oBanco Is Nothing Then
                Throw New Exception("Erro ao inicializar o objeto oBanco.")
            End If
        Catch ex As Exception
            EscreveLog("(WSSincronizacao) Erro ao inicializar o banco: " & ex.Message)
            Throw New Exception("Erro ao inicializar o banco: " & ex.Message)
        End Try
    End Sub

    Private Sub InicializaLog()
        Try
            Dim logDirectory As String = Path.GetDirectoryName(logFilePath)
            If Not Directory.Exists(logDirectory) Then
                Directory.CreateDirectory(logDirectory)
            End If

            If Not File.Exists(logFilePath) Then
                File.Create(logFilePath).Dispose()
            End If
        Catch ex As Exception
            Throw New Exception("Erro ao inicializar o arquivo de log: " & ex.Message)
        End Try
    End Sub

    Private Sub EscreveLog(ByVal mensagem As String)
        Try
            InicializaLog()
            Using sw As StreamWriter = New StreamWriter(logFilePath, True)
                sw.WriteLine($"{DateTime.Now}: {mensagem}")
            End Using
        Catch ex As Exception
            Throw New Exception("Erro ao gravar log: " & ex.Message)
        End Try
    End Sub

    Private Sub VerificaOBanco()
        If oBanco Is Nothing Then
            Dim mensagemErro As String = "Erro: oBanco não foi inicializado."
            EscreveLog(mensagemErro)
            Throw New Exception(mensagemErro)
        End If
    End Sub

    Private Function ChamarProcedureEstrutura(ByVal pAcao As String, ByVal xmlData As String, pPConn_Banco As String) As String
        Try
            VerificaOBanco()
            Dim parametros As New List(Of SqlClient.SqlParameter)()
            parametros.Add(New SqlClient.SqlParameter("@pAcao", pAcao))
            parametros.Add(New SqlClient.SqlParameter("@XmlData", xmlData))
            oBanco.retorna_Query("dbo.pa_SincronizarEstruturaOrganizacional", parametros.ToArray(), pPConn_Banco)
            Return "OK"
        Catch ex As Exception
            EscreveLog("(WSSincronizacao) Erro em ChamarProcedureEstrutura: " & ex.Message)
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod()>
    Public Function SincronizarCargosEmLote(ByVal xmlData As String, pPConn_Banco As String) As String
        Return ChamarProcedureEstrutura("MERGE_CARGO", xmlData, pPConn_Banco)
    End Function

    <WebMethod()>
    Public Function SincronizarEmpresasEmLote(ByVal xmlData As String, pPConn_Banco As String) As String
        Return ChamarProcedureEstrutura("MERGE_EMPRESA", xmlData, pPConn_Banco)
    End Function

    <WebMethod()>
    Public Function SincronizarDepartamentosEmLote(ByVal xmlData As String, pPConn_Banco As String) As String
        Return ChamarProcedureEstrutura("MERGE_DEPARTAMENTO", xmlData, pPConn_Banco)
    End Function

    <WebMethod()>
    Public Function SincronizarCentroCustoEmLote(ByVal xmlData As String, pPConn_Banco As String) As String
        Return ChamarProcedureEstrutura("MERGE_CENTRO_CUSTO", xmlData, pPConn_Banco)
    End Function

    <WebMethod()>
    Public Function SincronizarFiliaisEmLote(ByVal xmlData As String, pPConn_Banco As String) As String
        Return ChamarProcedureEstrutura("MERGE_FILIAL", xmlData, pPConn_Banco)
    End Function

    <WebMethod()>
    Public Function SincronizarSetoresEmLote(ByVal xmlData As String, pPConn_Banco As String) As String
        Return ChamarProcedureEstrutura("MERGE_SETOR", xmlData, pPConn_Banco)
    End Function

    ' Caso necessário criar equivalentes para Consumidor e Usuario:
    <WebMethod()>
    Public Function SincronizarConsumidoresEmLote(ByVal xmlData As String, pPConn_Banco As String) As String
        Return ChamarProcedureEstrutura("MERGE_CONSUMIDOR", xmlData, pPConn_Banco)
    End Function

    <WebMethod()>
    Public Function SincronizarUsuariosEmLote(ByVal xmlData As String, pPConn_Banco As String) As String
        Return ChamarProcedureEstrutura("MERGE_USUARIO", xmlData, pPConn_Banco)
    End Function

End Class
