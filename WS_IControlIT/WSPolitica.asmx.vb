Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSPolitica
    Inherits WebService

    Dim oBanco As New cls_Banco
    Dim vParametro() As SqlClient.SqlParameter

    '-----cadastro e consulta (Politica_Consumidor)
    <WebMethod()>
    Public Function Politica_Consumidor(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Consumidor As System.Int32,
                                        ByVal pId_Ativo_Tipo As System.Int32,
                                        ByVal pNm_Consumidor As System.String,
                                        ByVal pValor_Politica As System.Double,
                                        ByVal pMarca_Ligacao As System.Int32,
                                        ByVal pId_Lote As System.Double,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(7)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pNm_Consumidor, "@pNm_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pValor_Politica, "@pValor_Politica", False)
        oBanco.monta_Parametro(vParametro, pMarca_Ligacao, "@pMarca_Ligacao", False)
        oBanco.monta_Parametro(vParametro, pId_Lote, "@pId_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Politica_Consumidor", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Politica_Consumidor", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Troca_Hierarquia)
    <WebMethod()>
    Public Function Troca_Hierarquia(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Filial As System.Int32,
                                    ByVal pId_Centro_Custo As System.Int32,
                                    ByVal pId_Departamento As System.Int32,
                                    ByVal pId_Setor As System.Int32,
                                    ByVal pId_Secao As System.Int32,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String) As System.Data.DataSet

        ReDim vParametro(6)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Filial, "@pId_Filial", False)
        oBanco.monta_Parametro(vParametro, pId_Centro_Custo, "@pId_Centro_Custo", False)
        oBanco.monta_Parametro(vParametro, pId_Departamento, "@pId_Departamento", False)
        oBanco.monta_Parametro(vParametro, pId_Setor, "@pId_Setor", False)
        oBanco.monta_Parametro(vParametro, pId_Secao, "@pId_Secao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        oBanco.manutencao_Dados("dbo.pa_Troca_Hierarquia", vParametro, pPConn_Banco)
        Return Nothing
    End Function
End Class
