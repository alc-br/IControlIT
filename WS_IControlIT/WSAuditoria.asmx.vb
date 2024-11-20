Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/", Name:="WSAuditoria")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSAuditoria
    Inherits WebService

    Dim oBanco As New cls_Banco
    Dim vParametro() As SqlClient.SqlParameter

    '-----cadastro e consulta (Acompanhamento de Auditoria)
    <WebMethod()>
    Public Function Auditoria_Acompanhamento(ByVal pPConn_Banco As System.String,
                                                ByVal pId_Auditoria_Acompanhamento As System.Int32,
                                                ByVal pId_Auditoria_Lote As System.Int32,
                                                ByVal pId_Auditoria_Conta As System.Int32,
                                                ByVal pId_Auditoria_Status As System.Int32,
                                                ByVal pDescricao As System.String,
                                                ByVal pData_Prevista As System.DateTime,
                                                ByVal pValor_Previsto As System.Double,
                                                ByVal pId_Usuario_Permissao As System.Int32,
                                                ByVal pPakage As System.String,
                                                ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(8)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Auditoria_Acompanhamento, "@pId_Auditoria_Acompanhamento", False)
        oBanco.monta_Parametro(vParametro, pId_Auditoria_Lote, "@pId_Auditoria_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Auditoria_Conta, "@pId_Auditoria_Conta", False)
        oBanco.monta_Parametro(vParametro, pId_Auditoria_Status, "@pId_Auditoria_Status", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)
        oBanco.monta_Parametro(vParametro, pData_Prevista, "@pData_Prevista", False)
        oBanco.monta_Parametro(vParametro, pValor_Previsto, "@pValor_Previsto", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Auditoria_Acompanhamento", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Auditoria_Acompanhamento", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Auditoria)
    <WebMethod()>
    Public Function Auditoria(ByVal pPConn_Banco As System.String,
                                ByVal pId_Auditoria_Lote As System.Int32,
                                ByVal pId_Auditoria_Conta As System.Int32,
                                ByVal pDt_Lote As System.String,
                                ByVal pId_Conglomerado As System.Int32,
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(5)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Auditoria_Lote, "@pId_Auditoria_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Auditoria_Conta, "@pId_Auditoria_Conta", False)
        oBanco.monta_Parametro(vParametro, pDt_Lote, "@pDt_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Auditoria", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Auditoria", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    <WebMethod()>
    Public Function Auditoria_Contestacao(ByVal pPConn_Banco As System.String,
                                ByVal pId_Fatura As System.Int32,
                                ByVal pId_Auditoria_Contestacao As System.Int32,
                                ByVal pDescricao As System.String,
                                ByVal pDt_Lote As System.String,
                                ByVal pId_Fatura_Parametro As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(5)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPAKAGE", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura, "@pId_Fatura", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)
        oBanco.monta_Parametro(vParametro, pDt_Lote, "@pDt_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Auditoria_Contestacao, "@pId_Auditoria_Contestacao", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura_Parametro, "@pId_Fatura_Parametro", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Auditoria_Contestacao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Auditoria_Contestacao", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function
End Class