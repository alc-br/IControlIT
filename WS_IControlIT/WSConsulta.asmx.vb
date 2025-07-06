Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSConsulta
    Inherits WebService

    Dim oBanco As New cls_Banco
    Dim vParametro() As SqlClient.SqlParameter

    '-----pesquisar
    <WebMethod()>
    Public Function Pesquisar(ByVal pPConn_Banco As System.String,
                                ByVal pPakage As System.String,
                                ByVal pDescricao As System.String) As System.Data.DataSet
        ReDim vParametro(1)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)

        Return oBanco.retorna_Query("dbo.pa_Pesquisar", vParametro, pPConn_Banco)
    End Function

    '-----consulta ativo
    <WebMethod()>
    Public Function Ativo(ByVal pPConn_Banco As System.String,
                            ByVal pPakage As System.String,
                            ByVal pDescricao As System.String) As System.Data.DataSet
        ReDim vParametro(1)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)

        Return oBanco.retorna_Query("dbo.cn_Ativo", vParametro, pPConn_Banco)
    End Function

    '-----consulta consumidor
    <WebMethod()>
    Public Function Consumidor(ByVal pPConn_Banco As System.String,
                                ByVal pPakage As System.String,
                                ByVal pDescricao As System.String) As System.Data.DataSet
        ReDim vParametro(1)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)

        Return oBanco.retorna_Query("dbo.cn_Consumidor", vParametro, pPConn_Banco)
    End Function

    '-----consulta lote
    <WebMethod()>
    Public Function Lote(ByVal pPConn_Banco As System.String,
                            ByVal pPakage As System.String,
                            ByVal pPakage_Filtro As System.String,
                            ByVal pId_Filtro_1 As System.String,
                            ByVal pId_Filtro_Filial As System.String,
                            ByVal pId_Filtro_Usuario As System.Int32,
                            ByVal p_Id_Filtro_Centro_Custo As System.String,
                            ByVal p_Id_Filtro_Departamento As System.String,
                            ByVal p_Id_Filtro_Setor As System.String,
                            ByVal pDt_LoteDe As System.String,
                            ByVal pDt_LoteAte As System.String,
                            ByVal pAtivo_Tipo_Grupo As System.String,
                            ByVal pId_Usuario As System.Int32) As System.Data.DataSet

        ReDim vParametro(11)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pPakage_Filtro, "@pPakage_Filtro", False)
        oBanco.monta_Parametro(vParametro, pId_Filtro_1, "@pId_Filtro_1", False)
        oBanco.monta_Parametro(vParametro, pId_Filtro_Filial, "@pId_Filtro_Filial", False)
        oBanco.monta_Parametro(vParametro, pId_Filtro_Usuario, "@pId_Filtro_Usuario", False)
        oBanco.monta_Parametro(vParametro, p_Id_Filtro_Centro_Custo, "@p_Id_Filtro_Centro_Custo", False)
        oBanco.monta_Parametro(vParametro, p_Id_Filtro_Departamento, "@p_Id_Filtro_Departamento", False)
        oBanco.monta_Parametro(vParametro, p_Id_Filtro_Setor, "@p_Id_Filtro_Setor", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteDe, "@pDt_LoteDe", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteAte, "@pDt_LoteAte", False)
        oBanco.monta_Parametro(vParametro, pAtivo_Tipo_Grupo, "@pAtivo_Tipo_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)

        Return oBanco.retorna_Query("dbo.cn_Lote", vParametro, pPConn_Banco)
    End Function

    '-----consulta bilhete
    <WebMethod()>
    Public Function Bilhete(ByVal pPConn_Banco As System.String,
                            ByVal pPakage As System.String,
                            ByVal pId_Lote As System.Double,
                            ByVal pId_Usuario As System.Int32,
                            ByVal pId_Bilhete_Tipo As System.Int32,
                            ByVal pId_Usuario_Marcacao As System.Int32) As System.Data.DataSet

        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Lote, "@pId_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)
        oBanco.monta_Parametro(vParametro, pId_Bilhete_Tipo, "@pId_Bilhete_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Marcacao, "@pId_Usuario_Marcacao", False)

        Return oBanco.retorna_Query("dbo.cn_Bilhete", vParametro, pPConn_Banco)
    End Function

    '-----consulta template
    <WebMethod()>
    Public Function Template(ByVal pPConn_Banco As System.String,
                                ByVal pProcedure As System.String,
                                ByVal pPakage As System.String,
                                ByVal pPakage_Filtro As System.String,
                                ByVal pId_Filtro_1 As System.String,
                                ByVal pId_Filtro_Filial As System.String,
                                ByVal pId_Filtro_Usuario As System.Int32,
                                ByVal p_Id_Filtro_Centro_Custo As System.String,
                                ByVal p_Id_Filtro_Departamento As System.String,
                                ByVal p_Id_Filtro_Setor As System.String,
                                ByVal pDt_LoteDe As System.String,
                                ByVal pDt_LoteAte As System.String,
                                ByVal pAtivo_Tipo_Grupo As System.String) As System.Data.DataSet
        ReDim vParametro(10)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pPakage_Filtro, "@pPakage_Filtro", False)
        oBanco.monta_Parametro(vParametro, pId_Filtro_1, "@pId_Filtro_1", False)
        oBanco.monta_Parametro(vParametro, pId_Filtro_Filial, "@pId_Filtro_Filial", False)
        oBanco.monta_Parametro(vParametro, pId_Filtro_Usuario, "@pId_Filtro_Usuario", False)
        oBanco.monta_Parametro(vParametro, p_Id_Filtro_Centro_Custo, "@p_Id_Filtro_Centro_Custo", False)
        oBanco.monta_Parametro(vParametro, p_Id_Filtro_Departamento, "@p_Id_Filtro_Departamento", False)
        oBanco.monta_Parametro(vParametro, p_Id_Filtro_Setor, "@p_Id_Filtro_Setor", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteDe, "@pDt_LoteDe", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteAte, "@pDt_LoteAte", False)
        oBanco.monta_Parametro(vParametro, pAtivo_Tipo_Grupo, "@pAtivo_Tipo_Grupo", False)

        Return oBanco.retorna_Query(pProcedure, vParametro, pPConn_Banco)
    End Function

    '-----consulta rateio
    <WebMethod()>
    Public Function Rateio_Lista(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Rateio As System.String,
                                    ByVal pDt_Lote As System.String,
                                    ByVal pPakage As System.String) As System.Data.DataSet

        ReDim vParametro(2)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pDt_Lote, "@pDt_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Rateio, "@pId_Rateio", False)

        Return oBanco.retorna_Query("dbo.cn_Rateio", vParametro, pPConn_Banco)
    End Function

    '-----consulta volumetria custo
    <WebMethod()>
    Public Function Volumetria_Custo(ByVal pPConn_Banco As System.String,
                                    ByVal pAtivo_Tipo_Grupo As System.String,
                                    ByVal pId_Conglomerado As System.Int32,
                                    ByVal pDt_LoteDe As System.String,
                                    ByVal pDt_LoteAte As System.String,
                                    ByVal pPakage As System.String) As System.Data.DataSet
        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pAtivo_Tipo_Grupo, "@pAtivo_Tipo_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteDe, "@pDt_LoteDe", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteAte, "@pDt_LoteAte", False)

        Return oBanco.retorna_Query("dbo.cn_Volumetria_Custo", vParametro, pPConn_Banco)
    End Function

    '-----consulta volumetria consumo
    <WebMethod()>
    Public Function Volumetria_Consumo(ByVal pPConn_Banco As System.String,
                                        ByVal pAtivo_Tipo_Grupo As System.String,
                                        ByVal pId_Conglomerado As System.Int32,
                                        ByVal pDt_LoteDe As System.String,
                                        ByVal pDt_LoteAte As System.String,
                                        ByVal pPakage As System.String) As System.Data.DataSet
        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pAtivo_Tipo_Grupo, "@pAtivo_Tipo_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteDe, "@pDt_LoteDe", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteAte, "@pDt_LoteAte", False)

        Return oBanco.retorna_Query("dbo.cn_Volumetria_Consumo", vParametro, pPConn_Banco)
    End Function

    '-----consulta aproveitamento pacote
    <WebMethod()>
    Public Function Aproveitamento_Pacote(ByVal pPConn_Banco As System.String,
                                            ByVal pAtivo_Tipo_Grupo As System.String,
                                            ByVal pDt_LoteDe As System.String,
                                            ByVal pPakage As System.String) As System.Data.DataSet
        ReDim vParametro(2)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pAtivo_Tipo_Grupo, "@pAtivo_Tipo_Grupo", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteDe, "@pDt_LoteDe", False)

        Return oBanco.retorna_Query("dbo.cn_Aproveitamento_Pacote", vParametro, pPConn_Banco)
    End Function

    '-----consulta detalahemnto bilhete
    <WebMethod()>
    Public Function Detalhamento_Bilhete(ByVal pPConn_Banco As System.String,
                                            ByVal pAtivo_Tipo_Grupo As System.String,
                                            ByVal pId_Conglomerado As System.Int32,
                                            ByVal pDt_LoteDe As System.String,
                                            ByVal pDt_LoteAte As System.String,
                                            ByVal pPakage As System.String) As System.Data.DataSet
        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pAtivo_Tipo_Grupo, "@pAtivo_Tipo_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteDe, "@pDt_LoteDe", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteAte, "@pDt_LoteAte", False)

        Return oBanco.retorna_Query("dbo.cn_Detalhamento_Bilhete", vParametro, pPConn_Banco)
    End Function

End Class
