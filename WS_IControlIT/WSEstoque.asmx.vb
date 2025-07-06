Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSEstoque
    Inherits WebService

    Dim oBanco As New cls_Banco
    Dim vParametro() As SqlClient.SqlParameter

    '-----cadastro e consulta (Estoque Aparelho)
    <WebMethod()>
    Public Function Aparelho(ByVal pPConn_Banco As System.String,
                            ByVal pId_Aparelho As System.Int32,
                            ByVal pNr_Aparelho As System.String,
                            ByVal pNr_Aparelho_2 As System.String,
                            ByVal pNr_Linha_Solicitacao As System.String,
                            ByVal pNr_Chamado As System.String,
                            ByVal pDt_Chamado As System.DateTime,
                            ByVal pNr_Pedido As System.String,
                            ByVal pDt_Pedido As System.DateTime,
                            ByVal pId_Estoque_Nota_Fiscal As System.Int32,
                            ByVal pId_Conglomerado As System.Int32,
                            ByVal pId_Aparelho_Tipo As System.Int32,
                            ByVal pId_Ativo_Tipo As System.Int32,
                            ByVal pId_Ativo_Modelo As System.Int32,
                            ByVal pId_Estoque_Aparelho_Status As System.Int32,
                            ByVal pObservacao As System.String,
                            ByVal pJustificativa_Desativacao As System.String,
                            ByVal pId_Estoque_Endereco_Entrega As System.Int32,
                            ByVal pId_Consumidor As System.Int32,
                            ByVal pCk_Carregador As System.Int32,
                            ByVal pCk_Cabousb As System.Int32,
                            ByVal pCk_Fone As System.Int32,
                            ByVal pCk_Pelicula As System.Int32,
                            ByVal pCk_Capaprotecao As System.Int32,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet


        ReDim vParametro(24)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Aparelho, "@pId_Aparelho", True)
        oBanco.monta_Parametro(vParametro, pNr_Aparelho, "@pNr_Aparelho", False)
        oBanco.monta_Parametro(vParametro, pNr_Aparelho_2, "@pNr_Aparelho_2", False)
        oBanco.monta_Parametro(vParametro, pNr_Linha_Solicitacao, "@pNr_Linha_Solicitacao", False)
        oBanco.monta_Parametro(vParametro, pNr_Chamado, "@pNr_Chamado", False)
        oBanco.monta_Parametro(vParametro, pDt_Chamado, "@pDt_Chamado", False)
        oBanco.monta_Parametro(vParametro, pNr_Pedido, "@pNr_Pedido", False)
        oBanco.monta_Parametro(vParametro, pDt_Pedido, "@pDt_Pedido", False)
        oBanco.monta_Parametro(vParametro, pId_Estoque_Nota_Fiscal, "@pId_Estoque_Nota_Fiscal", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pId_Aparelho_Tipo, "@pId_Aparelho_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Modelo, "@pId_Ativo_Modelo", False)
        oBanco.monta_Parametro(vParametro, pId_Estoque_Aparelho_Status, "@pId_Estoque_Aparelho_Status", False)
        oBanco.monta_Parametro(vParametro, pObservacao, "@pObservacao", False)
        oBanco.monta_Parametro(vParametro, pJustificativa_Desativacao, "@pJustificativa_Desativacao", False)
        oBanco.monta_Parametro(vParametro, pId_Estoque_Endereco_Entrega, "@pId_Estoque_Endereco_Entrega", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        oBanco.monta_Parametro(vParametro, pCk_Carregador, "@pCk_Carregador", False)
        oBanco.monta_Parametro(vParametro, pCk_Cabousb, "@pCk_Cabousb", False)
        oBanco.monta_Parametro(vParametro, pCk_Fone, "@pCk_Fone", False)
        oBanco.monta_Parametro(vParametro, pCk_Pelicula, "@pCk_Pelicula", False)
        oBanco.monta_Parametro(vParametro, pCk_Capaprotecao, "@pCk_Capaprotecao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Estoque_Aparelho", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Estoque_Aparelho", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (Estoque Nota Fiscal)
    <WebMethod()>
    Public Function Nota_Fiscal(ByVal pPConn_Banco As System.String,
                                ByVal pId_Estoque_Nota_Fiscal As System.Int32,
                                ByVal pNr_Nota_Fiscal As System.String,
                                ByVal pDt_Nota_Fiscal As System.DateTime,
                                ByVal pId_Ativo_Fr_Aquisicao As System.Int32,
                                ByVal pVr_Fr_Aquisicao As System.Double,
                                ByVal pDt_Inicio_Fr_Aquisicao As System.DateTime,
                                ByVal pQtd_Mes_Residuo_Fr_Aquisicao As System.Int32,
                                ByVal pObservacao As System.String,
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(9)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Estoque_Nota_Fiscal, "@pId_Estoque_Nota_Fiscal", True)
        oBanco.monta_Parametro(vParametro, pNr_Nota_Fiscal, "@pNr_Nota_Fiscal", False)
        oBanco.monta_Parametro(vParametro, pDt_Nota_Fiscal, "@pDt_Nota_Fiscal", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Fr_Aquisicao, "@pId_Ativo_Fr_Aquisicao", False)
        oBanco.monta_Parametro(vParametro, pVr_Fr_Aquisicao, "@pVr_Fr_Aquisicao", False)
        oBanco.monta_Parametro(vParametro, pDt_Inicio_Fr_Aquisicao, "@pDt_Inicio_Fr_Aquisicao", False)
        oBanco.monta_Parametro(vParametro, pQtd_Mes_Residuo_Fr_Aquisicao, "@pQtd_Mes_Residuo_Fr_Aquisicao", False)
        oBanco.monta_Parametro(vParametro, pObservacao, "@pObservacao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Estoque_Nota_Fiscal", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Estoque_Nota_Fiscal", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (Estoque Aparelho Tipo)
    <WebMethod()>
    Public Function Estoque_Endereco_Entrega(ByVal pPConn_Banco As System.String,
                                                ByVal pId_Estoque_Endereco_Entrega As System.Int32,
                                                ByVal pNm_Estoque_Endereco_Entrega As System.String,
                                                ByVal pId_Usuario_Permissao As System.Int32,
                                                ByVal pPakage As System.String,
                                                ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Estoque_Endereco_Entrega, "@pId_Estoque_Endereco_Entrega", False)
        oBanco.monta_Parametro(vParametro, pNm_Estoque_Endereco_Entrega, "@pNm_Estoque_Endereco_Entrega", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Estoque_Endereco_Entrega", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Estoque_Endereco_Entrega", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Estoque Consumidor)
    <WebMethod()>
    Public Function Estoque_Consumidor(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Consumidor As System.String,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(2)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Estoque_Consumidor", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Estoque_Consumidor", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----consulta (Estoque)
    <WebMethod()>
    Public Function Estoque(ByVal pPConn_Banco As System.String,
                            ByVal pId_Consumidor As System.String,
                            ByVal pNr_Ativo As System.String,
                            ByVal pNr_Pedido As System.String,
                            ByVal pNr_Nota_Fiscal As System.String,
                            ByVal pId_Aparelho As System.Int32,
                            ByVal pId_Ativo As System.Int32,
                            ByVal pObservacao As System.String,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(8)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pNr_Ativo, "@pNr_Ativo", False)
        oBanco.monta_Parametro(vParametro, pNr_Pedido, "@pNr_Pedido", False)
        oBanco.monta_Parametro(vParametro, pNr_Nota_Fiscal, "@pNr_Nota_Fiscal", False)
        oBanco.monta_Parametro(vParametro, pId_Aparelho, "@pId_Aparelho", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
        oBanco.monta_Parametro(vParametro, pObservacao, "@pObservacao", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Estoque", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Estoque", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function
End Class
