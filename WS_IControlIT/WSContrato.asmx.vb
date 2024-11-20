Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSContrato
    Inherits WebService

    Dim oBanco As New cls_Banco
    Dim vParametro() As SqlClient.SqlParameter

    '-----cadastro e consulta (Contrato)
    <WebMethod()>
    Public Function Contrato(ByVal pPConn_Banco As System.String,
                                ByVal pId_Contrato As System.Int32,
                                ByVal pNr_Contrato As System.String,
                                ByVal pDescricao As System.String,
                                ByVal pId_Contrato_Status As System.Int32,
                                ByVal pId_Servico As System.Int32,
                                ByVal pDt_Inicio_Vigencia As System.DateTime,
                                ByVal pDt_Fim_Vigencia As System.DateTime,
                                ByVal pId_Filial As System.String,
                                ByVal pId_Empresa_Contratada As System.String,
                                ByVal pObjeto As System.String,
                                ByVal pId_Contrato_Indice As System.Int32,
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(12)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato, "@pId_Contrato", False)
        oBanco.monta_Parametro(vParametro, pNr_Contrato, "@pNr_Contrato", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato_Status, "@pId_Contrato_Status", False)
        oBanco.monta_Parametro(vParametro, pId_Servico, "@pId_Servico", False)
        oBanco.monta_Parametro(vParametro, pDt_Inicio_Vigencia, "@pDt_Inicio_Vigencia", False)
        oBanco.monta_Parametro(vParametro, pDt_Fim_Vigencia, "@pDt_Fim_Vigencia", False)
        oBanco.monta_Parametro(vParametro, pId_Filial, "@pId_Filial", False)
        oBanco.monta_Parametro(vParametro, pId_Empresa_Contratada, "@pId_Empresa_Contratada", False)
        oBanco.monta_Parametro(vParametro, pObjeto, "@pObjeto", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato_Indice, "@pId_Contrato_Indice", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Contrato", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Contrato", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Contrato_SLA_Operacao)
    <WebMethod()>
    Public Function Contrato_SLA_Operacao(ByVal pPConn_Banco As System.String,
                                            ByVal pId_Contrato_SLA_Operacao As System.Int32,
                                            ByVal pId_Contrato As System.Int32,
                                            ByVal pDescricao As System.String,
                                            ByVal pPrazo_Dias As System.Int32,
                                            ByVal pVr_SLA_Operacao As System.Double,
                                            ByVal pId_Usuario_Permissao As System.Int32,
                                            ByVal pPakage As System.String,
                                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(6)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato_SLA_Operacao, "@pId_Contrato_SLA_Operacao", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato, "@pId_Contrato", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)
        oBanco.monta_Parametro(vParametro, pPrazo_Dias, "@pPrazo_Dias", False)
        oBanco.monta_Parametro(vParametro, pVr_SLA_Operacao, "@pVr_SLA_Operacao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Contrato_SLA_Operacao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Contrato_SLA_Operacao", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Contrato_SLA_Serivo)
    <WebMethod()>
    Public Function Contrato_SLA_Servico(ByVal pPConn_Banco As System.String,
                                            ByVal pId_Contrato_SLA_Servico As System.Int32,
                                            ByVal pId_Contrato As System.Int32,
                                            ByVal pDescricao As System.String,
                                            ByVal pId_Operadora As System.Int32,
                                            ByVal pTipo_Servico As System.String,
                                            ByVal pVr_SLA_Servico As System.Double,
                                            ByVal pId_Contrato_Indice As System.Int32,
                                            ByVal pId_Usuario_Permissao As System.Int32,
                                            ByVal pPakage As System.String,
                                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(8)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato_SLA_Servico, "@pId_Contrato_SLA_Servico", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato, "@pId_Contrato", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)
        oBanco.monta_Parametro(vParametro, pId_Operadora, "@pId_Operadora", False)
        oBanco.monta_Parametro(vParametro, pTipo_Servico, "@pTipo_Servico", False)
        oBanco.monta_Parametro(vParametro, pVr_SLA_Servico, "@pVr_SLA_Servico", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato_Indice, "@pId_Contrato_Indice", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Contrato_SLA_Servico", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Contrato_SLA_Servico", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Contrato_Aditivo)
    <WebMethod()>
    Public Function Contrato_Aditivo(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Contrato_Aditivo As System.Int32,
                                        ByVal pId_Contrato As System.Int32,
                                        ByVal pDt_Vigencia As System.DateTime,
                                        ByVal pDescricao As System.String,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(5)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato_Aditivo, "@pId_Contrato_Aditivo", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato, "@pId_Contrato", False)
        oBanco.monta_Parametro(vParametro, pDt_Vigencia, "@pDt_Vigencia", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Contrato_Aditivo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Contrato_Aditivo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function
End Class
