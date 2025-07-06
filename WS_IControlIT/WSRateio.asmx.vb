Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSRateio
    Inherits WebService

    Dim oBanco As New cls_Banco
    Dim vParametro() As SqlClient.SqlParameter

    '-----cadastro e consulta (Fatura)
    <WebMethod()>
    Public Function Fatura(ByVal pPConn_Banco As System.String,
                            ByVal pId_Fatura As System.Int32,
                            ByVal pId_Fatura_Parametro As System.Int32,
                            ByVal pNr_Fatura As System.String,
                            ByVal pNm_Fatura As System.String,
                            ByVal pDt_Lote As System.String,
                            ByVal pDt_Emissao As System.DateTime,
                            ByVal pDt_Vencimento As System.DateTime,
                            ByVal pVr_Fatura As System.Double,
                            ByVal pNm_Campo As System.String,
                            ByVal pId_Conglomerado As System.Int32,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean,
                            ByVal pNota_Fiscal As System.String,
                            ByVal pPedido As System.String,
                            ByVal pRef As System.String,
                            ByVal pReq As System.String,
                            ByVal pProvisao As System.Int16,
                            ByVal pObservacao As System.String
                            ) As System.Data.DataSet
        ReDim vParametro(17)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura, "@pId_Fatura", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura_Parametro, "@pId_Fatura_Parametro", False)
        oBanco.monta_Parametro(vParametro, pNr_Fatura, "@pNr_Fatura", False)
        oBanco.monta_Parametro(vParametro, pNm_Fatura, "@pNm_Fatura", False)
        oBanco.monta_Parametro(vParametro, pDt_Lote, "@pDt_Lote", False)
        oBanco.monta_Parametro(vParametro, pDt_Emissao, "@pDt_Emissao", False)
        oBanco.monta_Parametro(vParametro, pDt_Vencimento, "@pDt_Vencimento", False)
        oBanco.monta_Parametro(vParametro, pVr_Fatura, "@pVr_Fatura", False)
        oBanco.monta_Parametro(vParametro, pNm_Campo, "@pNm_Campo", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)
        oBanco.monta_Parametro(vParametro, pNota_Fiscal, "@pNota_Fiscal", False)
        oBanco.monta_Parametro(vParametro, pPedido, "@pPedido", False)
        oBanco.monta_Parametro(vParametro, pRef, "@pRef", False)
        oBanco.monta_Parametro(vParametro, pReq, "@pReq", False)
        oBanco.monta_Parametro(vParametro, pProvisao, "@pProvisao", False)
        oBanco.monta_Parametro(vParametro, pObservacao, "@pObservacao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Fatura", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Fatura", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (rateio)
    <WebMethod()>
    Public Function Rateio(ByVal pPConn_Banco As System.String,
                            ByVal pNm_Rateio As System.String,
                            ByVal pId_Fatura_Parametro As System.Int32,
                            ByVal pId_Array As System.String,
                            ByVal pData_Lote As System.String,
                            ByVal pId_Fatura_Tipo_Rateio As System.String,
                            ByVal pId_Fatura As System.String,
                            ByVal pGrava_Rateio As System.Int32,
                            ByVal pId_Rateio As System.Int32,
                            ByVal pObservacao As System.String,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(9)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pNm_Rateio, "@pNm_Rateio", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura_Parametro, "@pId_Fatura_Parametro", False)
        oBanco.monta_Parametro(vParametro, pId_Array, "@pId_Array", False)
        oBanco.monta_Parametro(vParametro, pData_Lote, "@pData_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura_Tipo_Rateio, "@pId_Fatura_Tipo_Rateio", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura, "@pId_Fatura", False)
        oBanco.monta_Parametro(vParametro, pObservacao, "@pObservacao", False)
        oBanco.monta_Parametro(vParametro, pGrava_Rateio, "@pGrava_Rateio", False)
        oBanco.monta_Parametro(vParametro, pId_Rateio, "@pId_Rateio", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Rateio", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Rateio", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Plano_Conta)
    <WebMethod()>
    Public Function Plano_Conta(ByVal pPConn_Banco As System.String,
                                ByVal pId_Fatura_Plano_Conta As System.Int32,
                                ByVal pNr_Plano_Conta As System.String,
                                ByVal pId_Conglomerado As System.Int32,
                                ByVal pId_Empresa As System.Int32,
                                ByVal pId_Contrato As System.Int32,
                                ByVal pDia_Vencimento As System.Int32,
                                ByVal pLote_Cancelamento As System.DateTime,
                                ByVal pDescricao As System.String,
                                ByVal pDt_Lote As System.String,
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pId_Ativo_Tipo_Grupo As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(11)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura_Plano_Conta, "@pId_Fatura_Plano_Conta", False)
        oBanco.monta_Parametro(vParametro, pNr_Plano_Conta, "@pNr_Plano_Conta", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pId_Empresa, "@pId_Empresa", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato, "@pId_Contrato", False)
        oBanco.monta_Parametro(vParametro, pDia_Vencimento, "@pDia_Vencimento", False)
        oBanco.monta_Parametro(vParametro, pLote_Cancelamento, "@pLote_Cancelamento", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)
        oBanco.monta_Parametro(vParametro, pDt_Lote, "@pDt_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo_Grupo, "@pId_Ativo_Tipo_Grupo", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Fatura_Plano_Conta", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Fatura_Plano_Conta", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----rateio de custo fixo
    <WebMethod()>
    Public Function Rateio_Custo_Fixo(ByVal pPConn_Banco As System.String,
                                        ByVal pDt_Lote As System.String,
                                        ByVal pId_Fatura_Parametro As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(2)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pDt_Lote, "@pDt_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura_Parametro, "@pId_Fatura_Parametro", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Rateio_Custo_Fixo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Rateio_Custo_Fixo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function
End Class
