Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Runtime.CompilerServices

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSCadastro
    Inherits WebService

    Dim oBanco As New cls_Banco
    Dim vParametro() As SqlClient.SqlParameter

    '-----lixeira
    <WebMethod()>
    Public Function Lixeira(ByVal pPConn_Banco As System.String,
                            ByVal pId As System.Int32,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String) As System.Data.DataSet

        ReDim vParametro(2)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId, "@pId", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        oBanco.manutencao_Dados("dbo.pa_Lixeira", vParametro, pPConn_Banco)
        Return Nothing
    End Function

    '-----consulta para carregar os objetos de hierarquia
    <WebMethod()>
    Public Function Hierarquia(ByVal pPConn_Banco As System.String,
                                ByVal pPakage As System.String,
                                ByVal pChave As System.String,
                                ByVal pId_Usuario As System.String) As System.Data.DataSet
        ReDim vParametro(2)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pChave, "@pChave", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)

        Return oBanco.retorna_Query("dbo.pa_Hierarquia", vParametro, pPConn_Banco)
    End Function

    '-----consulta para carregar os objetos droplist com paramento de descricao
    <WebMethod()>
    Public Function DropList(ByVal pPConn_Banco As System.String,
                                ByVal pPakage As System.String,
                                ByVal pDescricao As System.String) As System.Data.DataSet
        ReDim vParametro(1)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)

        Return oBanco.retorna_Query("dbo.pa_DropList", vParametro, pPConn_Banco)
    End Function

    '-----consulta para carregar os objetos droplist com paramento de filtro
    <WebMethod()>
    Public Function DropList_Filtro(ByVal pPConn_Banco As System.String,
                                    ByVal pPakage As System.String,
                                    ByVal pFiltro_1 As System.Int32,
                                    ByVal pFiltro_2 As System.Int32) As System.Data.DataSet
        ReDim vParametro(2)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pFiltro_1, "@pFiltro_1", False)
        oBanco.monta_Parametro(vParametro, pFiltro_2, "@pFiltro_2", False)

        Return oBanco.retorna_Query("dbo.pa_DropList_Filtro", vParametro, pPConn_Banco)
    End Function

    '-----cadastro e consulta (relacinamento)
    <WebMethod()>
    Public Function Relacionamento(ByVal pPConn_Banco As System.String,
                                    ByVal pPakage As System.String,
                                    ByVal pChave As System.Int32,
                                    ByVal pRelacao As System.String) As System.Data.DataSet
        ReDim vParametro(2)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pChave, "@pChave", False)
        oBanco.monta_Parametro(vParametro, pRelacao, "@pRelacao", False)

        Return oBanco.retorna_Query("dbo.pa_Relacionamento", vParametro, pPConn_Banco)
    End Function

    '-----cadastro e consulta (centro_custo)
    <WebMethod()>
    Public Function Centro_Custo(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Centro_Custo As System.Int32,
                                    ByVal pCd_Centro_Custo As System.String,
                                    ByVal pNm_Centro_Custo As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Centro_Custo, "@pId_Centro_Custo", True)
        oBanco.monta_Parametro(vParametro, pCd_Centro_Custo, "@pCd_Centro_Custo", False)
        oBanco.monta_Parametro(vParametro, pNm_Centro_Custo, "@pNm_Centro_Custo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)


        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Centro_Custo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Centro_Custo", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (departamento)
    <WebMethod()>
    Public Function Departamento(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Departamento As System.Int32,
                                    ByVal pNm_Departamento As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Departamento, "@pId_Departamento", True)
        oBanco.monta_Parametro(vParametro, pNm_Departamento, "@pNm_Departamento", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Departamento", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Departamento", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (Setor)
    <WebMethod()>
    Public Function Setor(ByVal pPConn_Banco As System.String,
                            ByVal pId_Setor As System.Int32,
                            ByVal pNm_Setor As System.String,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Setor, "@pId_Setor", True)
        oBanco.monta_Parametro(vParametro, pNm_Setor, "@pNm_Setor", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Setor", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Setor", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If

    End Function

    '-----cadastro e consulta (secao)
    <WebMethod()>
    Public Function Secao(ByVal pPConn_Banco As System.String,
                            ByVal pId_Secao As System.Int32,
                            ByVal pNm_Secao As System.String,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Secao, "@pId_Secao", True)
        oBanco.monta_Parametro(vParametro, pNm_Secao, "@pNm_Secao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Secao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Secao", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (cargo)
    <WebMethod()>
    Public Function Cargo(ByVal pPConn_Banco As System.String,
                            ByVal pId_Cargo As System.Int32,
                            ByVal pNm_Cargo As System.String,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Cargo, "@pId_Cargo", False)
        oBanco.monta_Parametro(vParametro, pNm_Cargo, "@pNm_Cargo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Cargo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Cargo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Ativo_Tipo_Sub_Grupo)
    <WebMethod()>
    Public Function Ativo_Tipo_Sub_Grupo(ByVal pPConn_Banco As System.String,
                                            ByVal pId_Ativo_Tipo_Sub_Grupo As System.Int32,
                                            ByVal pNm_Ativo_Tipo_Sub_Grupo As System.String,
                                            ByVal pId_Usuario_Permissao As System.Int32,
                                            ByVal pPakage As System.String,
                                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo_Sub_Grupo, "@pId_Ativo_Tipo_Sub_Grupo", False)
        oBanco.monta_Parametro(vParametro, pNm_Ativo_Tipo_Sub_Grupo, "@pNm_Ativo_Tipo_Sub_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Tipo_Sub_Grupo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Tipo_Sub_Grupo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Ativo_Tipo_Grupo_Tipo)
    <WebMethod()>
    Public Function Ativo_Tipo_Grupo_Tipo(ByVal pPConn_Banco As System.String,
                                            ByVal pId_Ativo_Tipo_Grupo_Tipo As System.Int32,
                                            ByVal pNm_Ativo_Tipo_Grupo_Tipo As System.String,
                                            ByVal pId_Usuario_Permissao As System.Int32,
                                            ByVal pPakage As System.String,
                                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo_Grupo_Tipo, "@pId_Ativo_Tipo_Grupo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pNm_Ativo_Tipo_Grupo_Tipo, "@pNm_Ativo_Tipo_Grupo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Tipo_Grupo_Tipo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Tipo_Grupo_Tipo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (fabricante)
    <WebMethod()>
    Public Function Ativo_Fabricante(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Ativo_Fabricante As System.Int32,
                                        ByVal pNm_Ativo_Fabricante As System.String,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Fabricante, "@pId_Ativo_Fabricante", False)
        oBanco.monta_Parametro(vParametro, pNm_Ativo_Fabricante, "@pNm_Ativo_Fabricante", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Fabricante", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Fabricante", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (bilhete_tipo)
    <WebMethod()>
    Public Function Bilhete_Tipo(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Bilhete_Tipo As System.Int32,
                                    ByVal pId_Conglomerado As System.Int32,
                                    ByVal pNm_Bilhete_Tipo As System.String,
                                    ByVal pNm_Bilhete_Descricao As System.String,
                                    ByVal pUnidade As System.Int32,
                                    ByVal pTipo_Descricao As System.Int32,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(7)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Bilhete_Tipo, "@pId_Bilhete_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pNm_Bilhete_Tipo, "@pNm_Bilhete_Tipo", False)
        oBanco.monta_Parametro(vParametro, pNm_Bilhete_Descricao, "@pNm_Bilhete_Descricao", False)
        oBanco.monta_Parametro(vParametro, pUnidade, "@pUnidade", False)
        oBanco.monta_Parametro(vParametro, pTipo_Descricao, "@pTipo_Descricao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Bilhete_Tipo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Bilhete_Tipo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (ativo_Tipo)
    <WebMethod()>
    Public Function Ativo_Tipo(ByVal pPConn_Banco As System.String,
                                ByVal pId_Ativo_Tipo As System.Int32,
                                ByVal pNm_Ativo_Tipo As System.String,
                                ByVal pId_Ativo_Tipo_Grupo As System.String,
                                ByVal pEstoque_Regulador As System.String,
                                ByVal pImagem As System.String,
                                ByVal pPhoto As System.String,
                                ByVal pId_Ativo_Tipo_Sub_Grupo As System.Int32,
                                ByVal pId_Ativo_Tipo_Grupo_Tipo As System.Int32,
                                ByVal pId_Conglomerado As System.Int32,
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(10)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pNm_Ativo_Tipo, "@pNm_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo_Grupo, "@pId_Ativo_Tipo_Grupo", False)
        oBanco.monta_Parametro(vParametro, pEstoque_Regulador, "@pEstoque_Regulador", False)
        oBanco.monta_Parametro(vParametro, pImagem, "@pImagem", False)
        oBanco.monta_Parametro(vParametro, pPhoto, "@pPhoto", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo_Sub_Grupo, "@pId_Ativo_Tipo_Sub_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo_Grupo_Tipo, "@pId_Ativo_Tipo_Grupo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Tipo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Tipo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Auditoria_Status)
    <WebMethod()>
    Public Function Auditoria_Status(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Auditoria_Status As System.Int32,
                                        ByVal pNm_Auditoria_Status As System.String,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Auditoria_Status, "@pId_Auditoria_Status", False)
        oBanco.monta_Parametro(vParametro, pNm_Auditoria_Status, "@pNm_Auditoria_Status", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Auditoria_Status", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Auditoria_Status", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Holding)
    <WebMethod()>
    Public Function Holding(ByVal pPConn_Banco As System.String,
                                ByVal pId_Holding As System.Int32,
                                ByVal pNm_Holding As System.String,
                                ByVal pLogo As System.String,
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Holding, "@pId_Holding", False)
        oBanco.monta_Parametro(vParametro, pNm_Holding, "@pNm_Holding", False)
        oBanco.monta_Parametro(vParametro, pLogo, "@pLogo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Holding", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Holding", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (ativo_modelo)
    <WebMethod()>
    Public Function Ativo_Modelo(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Ativo_Modelo As System.Int32,
                                    ByVal pNm_Ativo_Modelo As System.String,
                                    ByVal pId_Ativo_Tipo As System.Int32,
                                    ByVal pId_Ativo_Fabricante As System.Int32,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(5)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Modelo, "@pId_Ativo_Modelo", False)
        oBanco.monta_Parametro(vParametro, pNm_Ativo_Modelo, "@pNm_Ativo_Modelo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Fabricante, "@pId_Ativo_Fabricante", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Modelo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Modelo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (empresa)
    <WebMethod()>
    Public Function Empresa(ByVal pPConn_Banco As System.String,
                            ByVal pId_Empresa As System.Int32,
                            ByVal pNm_Empresa As System.String,
                            ByVal pId_Holding As System.Int32,
                            ByVal pCNPJ As System.String,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(5)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pNm_Empresa, "@pNm_Empresa", False)
        oBanco.monta_Parametro(vParametro, pId_Holding, "@pId_Holding", False)
        oBanco.monta_Parametro(vParametro, pCNPJ, "@pCNPJ", False)
        oBanco.monta_Parametro(vParametro, pId_Empresa, "@pId_Empresa", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Empresa", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Empresa", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (empresa_contratada)
    <WebMethod()>
    Public Function Empresa_Contratada(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Empresa_Contratada As System.Int32,
                                        ByVal pNm_Empresa_Contratada As System.String,
                                        ByVal pCNPJ As System.String,
                                        ByVal pContato As System.String,
                                        ByVal pTelefone As System.String,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(6)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pNm_Empresa_Contratada, "@pNm_Empresa_Contratada", False)
        oBanco.monta_Parametro(vParametro, pCNPJ, "@pCNPJ", False)
        oBanco.monta_Parametro(vParametro, pContato, "@pContato", False)
        oBanco.monta_Parametro(vParametro, pTelefone, "@pTelefone", False)
        oBanco.monta_Parametro(vParametro, pId_Empresa_Contratada, "@pId_Empresa_Contratada", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Empresa_Contratada", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Empresa_Contratada", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Conglomerado)
    <WebMethod()>
    Public Function Conglomerado(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Conglomerado As System.Int32,
                                    ByVal pNm_Conglomerado As System.String,
                                    ByVal pContato As System.String,
                                    ByVal pTelefone As System.String,
                                    ByVal pCodigo_Operadora As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(6)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pNm_Conglomerado, "@pNm_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pContato, "@pContato", False)
        oBanco.monta_Parametro(vParametro, pTelefone, "@pTelefone", False)
        oBanco.monta_Parametro(vParametro, pCodigo_Operadora, "@pCodigo_Operadora", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Conglomerado", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Conglomerado", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (filial)
    <WebMethod()>
    Public Function Filial(ByVal pPConn_Banco As System.String,
                            ByVal pId_Filial As System.Int32,
                            ByVal pNm_Filial As System.String,
                            ByVal pCNPJ As System.String,
                            ByVal pEndereco As System.String,
                            ByVal pId_Empresa As System.Int32,
                            ByVal pHi_Departamento As System.Int32,
                            ByVal pHi_Setor As System.Int32,
                            ByVal pHi_Secao As System.Int32,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(9)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Filial, "@pId_Filial", True)
        oBanco.monta_Parametro(vParametro, pNm_Filial, "@pNm_Filial", False)
        oBanco.monta_Parametro(vParametro, pId_Empresa, "@pId_Empresa", False)
        oBanco.monta_Parametro(vParametro, pCNPJ, "@pCNPJ", False)
        oBanco.monta_Parametro(vParametro, pEndereco, "@pEndereco", False)
        oBanco.monta_Parametro(vParametro, pHi_Departamento, "@pHi_Departamento", False)
        oBanco.monta_Parametro(vParametro, pHi_Setor, "@pHi_Setor", False)
        oBanco.monta_Parametro(vParametro, pHi_Secao, "@pHi_Secao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Filial", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Filial", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If

    End Function

    '-----cadastro e consulta (Ativo_Fr_Aquisicao)
    <WebMethod()>
    Public Function Ativo_Fr_Aquisicao(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Ativo_Fr_Aquisicao As System.Int32,
                                        ByVal pNm_Ativo_Fr_Aquisicao As System.String,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Fr_Aquisicao, "@pId_Ativo_Fr_Aquisicao", False)
        oBanco.monta_Parametro(vParametro, pNm_Ativo_Fr_Aquisicao, "@pNm_Ativo_Fr_Aquisicao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Fr_Aquisicao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Fr_Aquisicao", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Ativo_Complemento)
    <WebMethod()>
    Public Function Ativo_Complemento(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Ativo_Complemento As System.Int32,
                                        ByVal pNm_Ativo_Complemento As System.String,
                                        ByVal pId_Ativo_Tipo As System.Int32,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Complemento, "@pId_Ativo_Complemento", False)
        oBanco.monta_Parametro(vParametro, pNm_Ativo_Complemento, "@pNm_Ativo_Complemento", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Complemento", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Complemento", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Servico)
    <WebMethod()>
    Public Function Servico(ByVal pPConn_Banco As System.String,
                            ByVal pId_Servico As System.Int32,
                            ByVal pNm_Servico As System.String,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Servico, "@pId_Servico", False)
        oBanco.monta_Parametro(vParametro, pNm_Servico, "@pNm_Servico", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Servico", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Servico", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Ativo)
    <WebMethod()>
    Public Function Ativo(ByVal pPConn_Banco As System.String,
                            ByVal pId_Ativo As System.Int32,
                            ByVal pNr_Ativo As System.String,
                            ByVal pFinalidade As System.String,
                            ByVal pId_Ativo_Tipo As System.Int32,
                            ByVal pId_Conglomerado As System.Int32,
                            ByVal pId_Ativo_Modelo As System.Int32,
                            ByVal pLocalidade As System.String,
                            ByVal pDt_Ativacao As System.DateTime,
                            ByVal pObservacao As System.String,
                            ByVal pAtivo_Complemento As System.String,
                            ByVal pId_Ativo_Status As System.Int32,
                            ByVal pArray_Consumidor As System.String,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean,
                            ByVal pEndereco As String,
                            pNumero_Sim_Card As String,
                            pValor_Contrato As String,
                            pPlano_Contrato As String,
                            pVelocidade As String
                            ) As System.Data.DataSet


        ReDim vParametro(18)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", True)
        oBanco.monta_Parametro(vParametro, pNr_Ativo, "@pNr_Ativo", False)
        oBanco.monta_Parametro(vParametro, pFinalidade, "@pFinalidade", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Modelo, "@pId_Ativo_Modelo", False)
        oBanco.monta_Parametro(vParametro, pLocalidade, "@pLocalidade", False)
        oBanco.monta_Parametro(vParametro, pDt_Ativacao, "@pDt_Ativacao", False)
        oBanco.monta_Parametro(vParametro, pObservacao, "@pObservacao", False)
        oBanco.monta_Parametro(vParametro, pAtivo_Complemento, "@pAtivo_Complemento", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Status, "@pId_Ativo_Status", False)
        oBanco.monta_Parametro(vParametro, pArray_Consumidor, "@pArray_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)
        oBanco.monta_Parametro(vParametro, pEndereco, "@pEndereco", False)
        oBanco.monta_Parametro(vParametro, pNumero_Sim_Card, "@pNumero_Sim_Card", False)
        oBanco.monta_Parametro(vParametro, pValor_Contrato, "@pValor_Contrato", False)
        oBanco.monta_Parametro(vParametro, pVelocidade, "@pVelocidade", False)
        oBanco.monta_Parametro(vParametro, pPlano_Contrato, "@pPlano_Contrato", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (Ativo_Parametro)
    <WebMethod()>
    Public Function Ativo_Parametro(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Ativo As System.Int32,
                                    ByVal pDt_Termino_Garantia As System.DateTime,
                                    ByVal pId_Ativo_Fr_Aquisicao As System.Int32,
                                    ByVal pDt_Ini_Fr_Aquisicao As System.DateTime,
                                    ByVal pVr_Fr_Aquisicao As System.Double,
                                    ByVal pQtd_Mes_Residuo_Fr_Aquisicao As System.Int32,
                                    ByVal pRateio_Conglomerado As System.Int32,
                                    ByVal pId_Tronco_Grupo As System.Int32,
                                    ByVal pId_Conglomerado As System.Int32,
                                    ByVal pId_Contrato As System.Int32,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(11)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
        oBanco.monta_Parametro(vParametro, pDt_Termino_Garantia, "@pDt_Termino_Garantia", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Fr_Aquisicao, "@pId_Ativo_Fr_Aquisicao", False)
        oBanco.monta_Parametro(vParametro, pDt_Ini_Fr_Aquisicao, "@pDt_Ini_Fr_Aquisicao", False)
        oBanco.monta_Parametro(vParametro, pVr_Fr_Aquisicao, "@pVr_Fr_Aquisicao", False)
        oBanco.monta_Parametro(vParametro, pQtd_Mes_Residuo_Fr_Aquisicao, "@pQtd_Mes_Residuo_Fr_Aquisicao", False)
        oBanco.monta_Parametro(vParametro, pRateio_Conglomerado, "@pRateio_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pId_Tronco_Grupo, "@pId_Tronco_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato, "@pId_Contrato", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Parametro", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Parametro", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Ativo_Porcentagem_Rateio)
    <WebMethod()>
    Public Function Ativo_Porcentagem_Rateio(ByVal pPConn_Banco As System.String,
                                                ByVal pId_Ativo As System.Int32,
                                                ByVal pId_Centro_Custo As System.Int32,
                                                ByVal pPorcentagem As System.Int32,
                                                ByVal pId_Usuario_Permissao As System.Int32,
                                                ByVal pPakage As System.String,
                                                ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
        oBanco.monta_Parametro(vParametro, pId_Centro_Custo, "@pId_Centro_Custo", False)
        oBanco.monta_Parametro(vParametro, pPorcentagem, "@pPorcentagem", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Porcentagem_Rateio", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Porcentagem_Rateio", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function


    <WebMethod>
    Public Function InventarioLote(ByVal pPConn_Banco As System.String,
                                ByVal pId_Consumidor As System.Int32,
                                ByVal pNm_Consumidor As System.String,
                                ByVal pId_Consumidor_Tipo As System.Int32,
                                ByVal pMatricula As System.String,
                                ByVal pEMail As System.String,
                                ByVal pEMail_Copia As System.String,
                                ByVal pFl_Nao_Envia_EMail As System.Int32,
                                ByVal pId_Empresa_Contratada As System.Int32,
                                ByVal pId_Cargo As System.Int32,
                                ByVal pId_Filial As System.Int32,
                                ByVal pId_Centro_Custo As System.Int32,
                                ByVal pId_Departamento As System.Int32,
                                ByVal pId_Setor As System.Int32,
                                ByVal pId_Secao As System.Int32,
                                ByVal pId_Consumidor_Status As System.Int32,
                                ByVal pMatricula_Chefia As System.String,
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pId_Usuario As System.Int32,
                                ByVal pNm_Usuario As System.String,
                                ByVal pSenha As System.String,
                                ByVal pId_Idioma As System.Int32,
                                ByVal pId_Usuario_Grupo As System.Int32,
                                ByVal pId_Usuario_Perfil As System.Int32,
                                ByVal pId_Usuario_Perfil_Acesso As System.Int32,
                                ByVal pIncluir As System.Int32,
                                ByVal pAlterar As System.Int32,
                                ByVal pExcluir As System.Int32,
                                ByVal pDetalhamento_Conta As System.Int32,
                                ByVal pDetalhamento_Contato As System.Int32,
                                ByVal pFl_Desativado As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(30)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", True)
        oBanco.monta_Parametro(vParametro, pNm_Consumidor, "@pNm_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor_Tipo, "@pId_Consumidor_Tipo", False)
        oBanco.monta_Parametro(vParametro, pMatricula, "@pMatricula", False)
        oBanco.monta_Parametro(vParametro, pEMail, "@pEMail", False)
        oBanco.monta_Parametro(vParametro, pEMail_Copia, "@pEMail_Copia", False)
        oBanco.monta_Parametro(vParametro, pFl_Nao_Envia_EMail, "@pFl_Nao_Envia_EMail", False)
        oBanco.monta_Parametro(vParametro, pId_Empresa_Contratada, "@pId_Empresa_Contratada", False)
        oBanco.monta_Parametro(vParametro, pId_Cargo, "@pId_Cargo", False)
        oBanco.monta_Parametro(vParametro, pId_Filial, "@pId_Filial", False)
        oBanco.monta_Parametro(vParametro, pId_Centro_Custo, "@pId_Centro_Custo", False)
        oBanco.monta_Parametro(vParametro, pId_Departamento, "@pId_Departamento", False)
        oBanco.monta_Parametro(vParametro, pId_Setor, "@pId_Setor", False)
        oBanco.monta_Parametro(vParametro, pId_Secao, "@pId_Secao", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor_Status, "@pId_Consumidor_Status", False)
        oBanco.monta_Parametro(vParametro, pMatricula_Chefia, "@pMatricula_Chefia", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", True)
        oBanco.monta_Parametro(vParametro, pNm_Usuario, "@pNm_Usuario", False)
        oBanco.monta_Parametro(vParametro, pSenha, "@pSenha", False)
        oBanco.monta_Parametro(vParametro, pId_Idioma, "@pId_Idioma", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Grupo, "@pId_Usuario_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Perfil, "@pId_Usuario_Perfil", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Perfil_Acesso, "@pId_Usuario_Perfil_Acesso", False)
        oBanco.monta_Parametro(vParametro, pIncluir, "@pIncluir", False)
        oBanco.monta_Parametro(vParametro, pAlterar, "@pAlterar", False)
        oBanco.monta_Parametro(vParametro, pExcluir, "@pExcluir", False)
        oBanco.monta_Parametro(vParametro, pDetalhamento_Conta, "@pDetalhamento_Conta", False)
        oBanco.monta_Parametro(vParametro, pDetalhamento_Contato, "@pDetalhamento_Contato", False)
        oBanco.monta_Parametro(vParametro, pFl_Desativado, "@pFl_Desativado", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_InventarioLote", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_InventarioLote", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If


    End Function

    '-----cadastro e consulta (Consumidor)
    <WebMethod()>
    Public Function Consumidor(ByVal pPConn_Banco As System.String,
                                ByVal pId_Consumidor As System.Int32,
                                ByVal pNm_Consumidor As System.String,
                                ByVal pId_Consumidor_Tipo As System.Int32,
                                ByVal pMatricula As System.String,
                                ByVal pEMail As System.String,
                                ByVal pEMail_Copia As System.String,
                                ByVal pFl_Nao_Envia_EMail As System.Int32,
                                ByVal pObservacao As System.String,
                                ByVal pId_Empresa_Contratada As System.Int32,
                                ByVal pId_Cargo As System.Int32,
                                ByVal pId_Filial As System.Int32,
                                ByVal pId_Centro_Custo As System.Int32,
                                ByVal pId_Departamento As System.Int32,
                                ByVal pId_Setor As System.Int32,
                                ByVal pId_Secao As System.Int32,
                                ByVal pId_Consumidor_Status As System.Int32,
                                ByVal pMatricula_Chefia As System.String,
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(18)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", True)
        oBanco.monta_Parametro(vParametro, pNm_Consumidor, "@pNm_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor_Tipo, "@pId_Consumidor_Tipo", False)
        oBanco.monta_Parametro(vParametro, pMatricula, "@pMatricula", False)
        oBanco.monta_Parametro(vParametro, pEMail, "@pEMail", False)
        oBanco.monta_Parametro(vParametro, pEMail_Copia, "@pEMail_Copia", False)
        oBanco.monta_Parametro(vParametro, pFl_Nao_Envia_EMail, "@pFl_Nao_Envia_EMail", False)
        oBanco.monta_Parametro(vParametro, pObservacao, "@pObservacao", False)
        oBanco.monta_Parametro(vParametro, pId_Empresa_Contratada, "@pId_Empresa_Contratada", False)
        oBanco.monta_Parametro(vParametro, pId_Cargo, "@pId_Cargo", False)
        oBanco.monta_Parametro(vParametro, pId_Filial, "@pId_Filial", False)
        oBanco.monta_Parametro(vParametro, pId_Centro_Custo, "@pId_Centro_Custo", False)
        oBanco.monta_Parametro(vParametro, pId_Departamento, "@pId_Departamento", False)
        oBanco.monta_Parametro(vParametro, pId_Setor, "@pId_Setor", False)
        oBanco.monta_Parametro(vParametro, pId_Secao, "@pId_Secao", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor_Status, "@pId_Consumidor_Status", False)
        oBanco.monta_Parametro(vParametro, pMatricula_Chefia, "@pMatricula_Chefia", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Consumidor", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Consumidor", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (consumidor_tipo)
    <WebMethod()>
    Public Function Consumidor_Tipo(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Consumidor_Tipo As System.Int32,
                                    ByVal pNm_Consumidor_Tipo As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor_Tipo, "@pId_Consumidor_Tipo", False)
        oBanco.monta_Parametro(vParametro, pNm_Consumidor_Tipo, "@pNm_Consumidor_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Consumidor_Tipo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Consumidor_Tipo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Consumidor_Unidade)
    <WebMethod()>
    Public Function Consumidor_Unidade(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Consumidor_Unidade As System.Int32,
                                        ByVal pId_Consumidor As System.Int32,
                                        ByVal pNm_Unidade As System.String,
                                        ByVal pCNPJ As System.String,
                                        ByVal pIE As System.String,
                                        ByVal pData_Ativacao As System.DateTime,
                                        ByVal pObservacao As System.String,
                                        ByVal pEntrega_Contato As System.String,
                                        ByVal pEntrega_Endereco As System.String,
                                        ByVal pEntrega_Telefone As System.String,
                                        ByVal pFaturamento_Contato As System.String,
                                        ByVal pFaturamento_Endereco As System.String,
                                        ByVal pFaturamento_CNPJ As System.String,
                                        ByVal pFaturamento_IE As System.String,
                                        ByVal pFaturamento_Email As System.String,
                                        ByVal pFaturamento_Telefone As System.String,
                                        ByVal pMatricula As System.String,
                                        ByVal pId_Conglomerado As System.Int32,
                                        ByVal pArray As System.String,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(20)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor_Unidade, "@pId_Consumidor_Unidade", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pNm_Unidade, "@pNm_Unidade", False)
        oBanco.monta_Parametro(vParametro, pCNPJ, "@pCNPJ", False)
        oBanco.monta_Parametro(vParametro, pIE, "@pIE", False)
        oBanco.monta_Parametro(vParametro, pData_Ativacao, "@pData_Ativacao", False)
        oBanco.monta_Parametro(vParametro, pObservacao, "@pObservacao", False)
        oBanco.monta_Parametro(vParametro, pEntrega_Contato, "@pEntrega_Contato", False)
        oBanco.monta_Parametro(vParametro, pEntrega_Endereco, "@pEntrega_Endereco", False)
        oBanco.monta_Parametro(vParametro, pEntrega_Telefone, "@pEntrega_Telefone", False)
        oBanco.monta_Parametro(vParametro, pFaturamento_Contato, "@pFaturamento_Contato", False)
        oBanco.monta_Parametro(vParametro, pFaturamento_Endereco, "@pFaturamento_Endereco", False)
        oBanco.monta_Parametro(vParametro, pFaturamento_CNPJ, "@pFaturamento_CNPJ", False)
        oBanco.monta_Parametro(vParametro, pFaturamento_IE, "@pFaturamento_IE", False)
        oBanco.monta_Parametro(vParametro, pFaturamento_Email, "@pFaturamento_Email", False)
        oBanco.monta_Parametro(vParametro, pFaturamento_Telefone, "@pFaturamento_Telefone", False)
        oBanco.monta_Parametro(vParametro, pMatricula, "@pMatricula", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pArray, "@pArray", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Consumidor_Unidade", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Consumidor_Unidade", vParametro, pPConn_Banco)
            Return Nothing
        End If

    End Function

    '-----cadastro e consulta (Usuario)
    <WebMethod()>
    Public Function Usuario(ByVal pPConn_Banco As System.String,
                            ByVal pId_Usuario As System.Int32,
                            ByVal pNm_Usuario As System.String,
                            ByVal pSenha As System.String,
                            ByVal pId_Consumidor As System.Int32,
                            ByVal pId_Idioma As System.Int32,
                            ByVal pId_Usuario_Grupo As System.Int32,
                            ByVal pId_Usuario_Perfil As System.Int32,
                            ByVal pId_Usuario_Perfil_Acesso As System.Int32,
                            ByVal pIncluir As System.Int32,
                            ByVal pAlterar As System.Int32,
                            ByVal pExcluir As System.Int32,
                            ByVal pDetalhamento_Conta As System.Int32,
                            ByVal pDetalhamento_Contato As System.Int32,
                            ByVal pFl_Desativado As System.Int32,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(15)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", True)
        oBanco.monta_Parametro(vParametro, pNm_Usuario, "@pNm_Usuario", False)
        oBanco.monta_Parametro(vParametro, pSenha, "@pSenha", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pId_Idioma, "@pId_Idioma", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Grupo, "@pId_Usuario_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Perfil, "@pId_Usuario_Perfil", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Perfil_Acesso, "@pId_Usuario_Perfil_Acesso", False)
        oBanco.monta_Parametro(vParametro, pIncluir, "@pIncluir", False)
        oBanco.monta_Parametro(vParametro, pAlterar, "@pAlterar", False)
        oBanco.monta_Parametro(vParametro, pExcluir, "@pExcluir", False)
        oBanco.monta_Parametro(vParametro, pDetalhamento_Conta, "@pDetalhamento_Conta", False)
        oBanco.monta_Parametro(vParametro, pDetalhamento_Contato, "@pDetalhamento_Contato", False)
        oBanco.monta_Parametro(vParametro, pFl_Desativado, "@pFl_Desativado", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Usuario", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Usuario", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (Usuario_Perfil)
    <WebMethod()>
    Public Function Usuario_Perfil(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Usuario As System.Int32,
                                    ByVal pId_Consumidor As System.Int32,
                                    ByVal pDescricao As System.String,
                                    ByVal pRelacao As System.String,
                                    ByVal pId_Ativo As System.Int32,
                                    ByVal pDt_Ativacao As System.DateTime,
                                    ByVal pDt_Desativacao As System.DateTime,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(8)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)
        oBanco.monta_Parametro(vParametro, pRelacao, "@pRelacao", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
        oBanco.monta_Parametro(vParametro, pDt_Ativacao, "@pDt_Ativacao", False)
        oBanco.monta_Parametro(vParametro, pDt_Desativacao, "@pDt_Desativacao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Usuario_Perfil", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Usuario_Perfil", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (usuario_grupo)
    <WebMethod()>
    Public Function Usuario_Grupo(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Usuario_Grupo As System.Int32,
                                    ByVal pNm_Usuario_Grupo As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Grupo, "@pId_Usuario_Grupo", False)
        oBanco.monta_Parametro(vParametro, pNm_Usuario_Grupo, "@pNm_Usuario_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Usuario_Grupo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Usuario_Grupo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----manutencao de marcacao (Bilhete, Lote_Marcacao)
    <WebMethod()>
    Public Function Marcacao(ByVal pPConn_Banco As System.String,
                                ByVal pId_Lote As System.Double,
                                ByVal pDB_Destino As System.String,
                                ByVal pId_Usuario As System.Int32,
                                ByVal pId_Bilhete As System.Double,
                                ByVal pDt_Lote As System.String,
                                ByVal pId_Bilhete_Split As System.String,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(6)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Lote, "@pId_Lote", False)
        oBanco.monta_Parametro(vParametro, pDB_Destino, "@pDB_Destino", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)
        oBanco.monta_Parametro(vParametro, pId_Bilhete, "@pId_Bilhete", False)
        oBanco.monta_Parametro(vParametro, pDt_Lote, "@pDt_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Bilhete_Split, "@pId_Bilhete_Split", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Bilhete", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Bilhete", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cria resumo da marcacao
    <WebMethod()>
    Public Sub Bilhete_Historico_Resumo(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Lote As System.Double,
                                        ByVal pId_Usuario As System.Int32,
                                        ByVal pTotal_Ligacao As System.Double,
                                        ByVal pValor_Politica As System.Double,
                                        ByVal pValor_Marcado As System.Double,
                                        ByVal pValor_Particular As System.Double,
                                        ByVal pPakage As System.String)

        ReDim vParametro(6)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Lote, "@pId_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)
        oBanco.monta_Parametro(vParametro, pTotal_Ligacao, "@pTotal_Ligacao", False)
        oBanco.monta_Parametro(vParametro, pValor_Politica, "@pValor_Politica", False)
        oBanco.monta_Parametro(vParametro, pValor_Marcado, "@pValor_Marcado", False)
        oBanco.monta_Parametro(vParametro, pValor_Particular, "@pValor_Particular", False)

        oBanco.manutencao_Dados("dbo.pa_Bilhete_Historico_Resumo", vParametro, pPConn_Banco)
    End Sub

    '-----cadastro e consulta (tronco_grupo)
    <WebMethod()>
    Public Function Tronco_Grupo(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Tronco_Grupo As System.Int32,
                                    ByVal pNm_Tronco_Grupo As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Tronco_Grupo, "@pId_Tronco_Grupo", False)
        oBanco.monta_Parametro(vParametro, pNm_Tronco_Grupo, "@pNm_Tronco_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Tronco_Grupo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Tronco_Grupo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (tronco)
    <WebMethod()>
    Public Function Tronco(ByVal pPConn_Banco As System.String,
                            ByVal pId_Tronco As System.Int32,
                            ByVal pNm_Tronco As System.String,
                            ByVal pId_Conglomerado As System.Int32,
                            ByVal pId_Tronco_Grupo As System.Int32,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(5)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Tronco, "@pId_Tronco", False)
        oBanco.monta_Parametro(vParametro, pNm_Tronco, "@pNm_Tronco", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pId_Tronco_Grupo, "@pId_Tronco_Grupo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Tronco", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Tronco", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (ativo_replace)
    <WebMethod()>
    Public Function Ativo_Replace(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Ativo_Tipo As System.Int32,
                                    ByVal pId_Ativo_Complemento As System.Int32,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Complemento, "@pId_Ativo_Complemento", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Replace", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Replace", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Fatura_Parametro_Campo)
    <WebMethod()>
    Public Function Fatura_Parametro_Campo(ByVal pPConn_Banco As System.String,
                                            ByVal pId_Fatura_Parametro_Campo As System.Int32,
                                            ByVal pNm_Fatura_Parametro_Campo As System.String,
                                            ByVal pId_Fatura_Parametro As System.Int32,
                                            ByVal pObservacao As System.String,
                                            ByVal pSinal As System.Int32,
                                            ByVal pId_Usuario_Permissao As System.Int32,
                                            ByVal pPakage As System.String,
                                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(6)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura_Parametro_Campo, "@pId_Fatura_Parametro_Campo", False)
        oBanco.monta_Parametro(vParametro, pNm_Fatura_Parametro_Campo, "@pNm_Fatura_Parametro_Campo", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura_Parametro, "@pId_Fatura_Parametro", False)
        oBanco.monta_Parametro(vParametro, pObservacao, "@pObservacao", False)
        oBanco.monta_Parametro(vParametro, pSinal, "@pSinal", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Fatura_Parametro_Campo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Fatura_Parametro_Campo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Fatura_Parametro)
    <WebMethod()>
    Public Function Fatura_Parametro(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Fatura_Parametro As System.Int32,
                                        ByVal pNm_Fatura_Parametro As System.String,
                                        ByVal pId_Contrato As System.Int32,
                                        ByVal pConta_Contabil As System.String,
                                        ByVal pId_Ativo As System.Int32,
                                        ByVal pCd_Centro_Custo As System.String,
                                        ByVal pId_Fatura_Tipo_Rateio As System.String,
                                        ByVal pRateia_Ativo_Padrao As System.Int32,
                                        ByVal pRateio_Nota As System.Int32,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(10)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura_Parametro, "@pId_Fatura_Parametro", True)
        oBanco.monta_Parametro(vParametro, pNm_Fatura_Parametro, "@pNm_Fatura_Parametro", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato, "@pId_Contrato", False)
        oBanco.monta_Parametro(vParametro, pConta_Contabil, "@pConta_Contabil", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
        oBanco.monta_Parametro(vParametro, pCd_Centro_Custo, "@pCd_Centro_Custo", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura_Tipo_Rateio, "@pId_Fatura_Tipo_Rateio", False)
        oBanco.monta_Parametro(vParametro, pRateia_Ativo_Padrao, "@pRateia_Ativo_Padrao", False)
        oBanco.monta_Parametro(vParametro, pRateio_Nota, "@pRateio_Nota", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Fatura_Parametro", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Fatura_Parametro", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (consumidor status)
    <WebMethod()>
    Public Function Consumidor_Status(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Consumidor_Status As System.Int32,
                                        ByVal pNm_Consumidor_Status As System.String,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor_Status, "@pId_Consumidor_Status", False)
        oBanco.monta_Parametro(vParametro, pNm_Consumidor_Status, "@pNm_Consumidor_Status", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Consumidor_Status", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Consumidor_Status", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (ativo status)
    <WebMethod()>
    Public Function Ativo_Status(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Ativo_Status As System.Int32,
                                    ByVal pNm_Ativo_Status As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Status, "@pId_Ativo_Status", False)
        oBanco.monta_Parametro(vParametro, pNm_Ativo_Status, "@pNm_Ativo_Status", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Status", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Status", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (agenda marcacao particular)
    <WebMethod()>
    Public Function Agenda_Marcacao_Particular(ByVal pPConn_Banco As System.String,
                                                ByVal pId_Usuario As System.Int32,
                                                ByVal pNr_Destino As System.String,
                                                ByVal pNm_Destino As System.String,
                                                ByVal pTipo As System.Int32,
                                                ByVal pPakage As System.String,
                                                ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)
        oBanco.monta_Parametro(vParametro, pNr_Destino, "@pNr_Destino", False)
        oBanco.monta_Parametro(vParametro, pNm_Destino, "@pNm_Destino", False)
        oBanco.monta_Parametro(vParametro, pTipo, "@pTipo", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Agenda_Marcacao_Particular", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Agenda_Marcacao_Particular", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (ativo status)
    <WebMethod()>
    Public Function Contrato_Status(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Contrato_Status As System.Int32,
                                    ByVal pNm_Contrato_Status As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato_Status, "@pId_Contrato_Status", False)
        oBanco.monta_Parametro(vParametro, pNm_Contrato_Status, "@pNm_Contrato_Status", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Contrato_Status", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Contrato_Status", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (custo_fixo)
    <WebMethod()>
    Public Function Custo_Fixo(ByVal pPConn_Banco As System.String,
                                ByVal pId_Custo_Fixo As System.Int32,
                                ByVal pNm_Custo_Fixo As System.String,
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Custo_Fixo, "@pId_Custo_Fixo", False)
        oBanco.monta_Parametro(vParametro, pNm_Custo_Fixo, "@pNm_Custo_Fixo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Custo_Fixo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Custo_Fixo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function


    '-----cadastro e consulta (custo_fixo_item)
    <WebMethod()>
    Public Function Custo_Fixo_Item(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Custo_Fixo_Item As System.Int32,
                                    ByVal pId_Custo_Fixo As System.Int32,
                                    ByVal pId_Ativo_Tipo As System.Int32,
                                    ByVal pId_Conglomerado As System.Int32,
                                    ByVal pValor As System.Double,
                                    ByVal pId_Lote As System.Double,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(7)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Custo_Fixo_Item, "@pId_Custo_Fixo_Item", False)
        oBanco.monta_Parametro(vParametro, pId_Custo_Fixo, "@pId_Custo_Fixo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pValor, "@pValor", False)
        oBanco.monta_Parametro(vParametro, pId_Lote, "@pId_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Custo_Fixo_Item", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Custo_Fixo_Item", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Politica_Consumidor)
    <WebMethod()>
    Public Function Ativo_Vago(ByVal pPConn_Banco As System.String,
                                ByVal pId_Ativo As System.Int32,
                                ByVal pId_Consumidor As System.Int32,
                                ByVal pNr_Ativo As System.String,
                                ByVal pDescricao As System.String,
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(5)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pNr_Ativo, "@pNr_Ativo", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_Vago", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_Vago", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '--Insere log
    <WebMethod>
    Public Function Envia_Log(ByVal pPConn_Banco As System.String,
                              ByVal pId_Usuario As System.String,
                              ByVal pData_Hora As DateTime,
                              ByVal pAcao_Executada As System.String,
                              ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(2)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)
        oBanco.monta_Parametro(vParametro, pData_Hora, "@pData_Hora", False)
        oBanco.monta_Parametro(vParametro, pAcao_Executada, "@pAcao_Executada", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Log_Geral", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Log_Geral", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function


    '-----excedente politica
    <WebMethod()>
    Public Function Excedente_Politica(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Lote As System.Double,
                                        ByVal pId_Usuario As System.Int32,
                                        ByVal pJustificativa As System.String,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Lote, "@pId_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)
        oBanco.monta_Parametro(vParametro, pJustificativa, "@pJustificativa", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Excedente_Cota", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Excedente_Cota", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (estoque aparelho status)
    <WebMethod()>
    Public Function Estoque_Aparelho_Status(ByVal pPConn_Banco As System.String,
                                            ByVal pId_Estoque_Aparelho_Status As System.Int32,
                                            ByVal pNm_Estoque_Aparelho_Status As System.String,
                                            ByVal pId_Usuario_Permissao As System.Int32,
                                            ByVal pPakage As System.String,
                                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Estoque_Aparelho_Status, "@pId_Estoque_Aparelho_Status", False)
        oBanco.monta_Parametro(vParametro, pNm_Estoque_Aparelho_Status, "@pNm_Estoque_Aparelho_Status", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Estoque_Aparelho_Status", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Estoque_Aparelho_Status", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (sla solicitacao)
    <WebMethod()>
    Public Function Solicitacao_SLA(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Solicitacao_SLA As System.Int32,
                                    ByVal pNm_Solicitacao_SLA As System.String,
                                    ByVal pQTDHoras As System.String,
                                    ByVal pEMail As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(5)

        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao_SLA, "@pId_Solicitacao_SLA", False)
        oBanco.monta_Parametro(vParametro, pNm_Solicitacao_SLA, "@pNm_Solicitacao_SLA", False)
        oBanco.monta_Parametro(vParametro, pQTDHoras, "@pQTDHoras", False)
        oBanco.monta_Parametro(vParametro, pEMail, "@pEMail", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Solicitacao_SLA", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Solicitacao_SLA", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (tipo solicitacao)
    <WebMethod()>
    Public Function Solicitacao_Tipo(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Solicitacao_Tipo As System.Int32,
                                    ByVal pNm_Solicitacao_Tipo As System.String,
                                    ByVal pId_Ativo_Tipo As System.Int32,
                                    ByVal pId_Solicitacao_SLA As System.Int32,
                                    ByVal pId_Solicitcao_Fila_Atendimento As System.Int32,
                                    ByVal pId_Solicitacao_Permissao As System.Int32,
                                    ByVal pFl_Config_Caixa_Texto As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(8)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao_Tipo, "@pId_Solicitacao_Tipo", False)
        oBanco.monta_Parametro(vParametro, pNm_Solicitacao_Tipo, "@pNm_Solicitacao_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao_SLA, "@pId_Solicitacao_SLA", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitcao_Fila_Atendimento, "@pId_Solicitcao_Fila_Atendimento", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao_Permissao, "@pId_Solicitacao_Permissao", False)
        oBanco.monta_Parametro(vParametro, pFl_Config_Caixa_Texto, "@pFl_Config_Caixa_Texto", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Solicitacao_Tipo", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Solicitacao_Tipo", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (solicitacao solucao)
    <WebMethod()>
    Public Function Solicitacao_Solucao(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Solicitacao_Solucao As System.Int32,
                                        ByVal pNm_Solicitacao_Solucao As System.String,
                                        ByVal pId_Ativo_Tipo As System.Int32,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao_Solucao, "@pId_Solicitacao_Solucao", False)
        oBanco.monta_Parametro(vParametro, pNm_Solicitacao_Solucao, "@pNm_Solicitacao_Solucao", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Solicitacao_Solucao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Solicitacao_Solucao", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (solicitacao data parada)
    <WebMethod()>
    Public Function Solicitacao_Data_Parada(ByVal pPConn_Banco As System.String,
                                            ByVal pId_Data_Parada As System.Int32,
                                            ByVal pData As System.DateTime,
                                            ByVal pDescricao As System.String,
                                            ByVal pId_Usuario_Permissao As System.Int32,
                                            ByVal pPakage As System.String,
                                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Data_Parada, "@pId_Data_Parada", False)
        oBanco.monta_Parametro(vParametro, pData, "@pData", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Solicitacao_Data_Parada", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Solicitacao_Data_Parada", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (fila de atendimento)
    <WebMethod()>
    Public Function Solicitacao_Fila_Atendimento(ByVal pPConn_Banco As System.String,
                                                ByVal pId_Solicitacao_Fila_Atendimento As System.Int32,
                                                ByVal pNm_Solicitacao_Fila_Atendimento As System.String,
                                                ByVal pId_Usuario_Permissao As System.Int32,
                                                ByVal pPakage As System.String,
                                                ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao_Fila_Atendimento, "@pId_Solicitacao_Fila_Atendimento", True)
        oBanco.monta_Parametro(vParametro, pNm_Solicitacao_Fila_Atendimento, "@pNm_Solicitacao_Fila_Atendimento", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Solicitacao_Fila_Atendimento", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Solicitacao_Fila_Atendimento", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (índice de reajuste)
    <WebMethod()>
    Public Function Contrato_Indice(ByVal pPConn_Banco As System.String,
                                    ByVal pId_Contrato_Indice As System.Int32,
                                    ByVal pNm_Contrato_Indice As System.String,
                                    ByVal pObs_Contrato_Indice As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Contrato_Indice, "@pId_Contrato_Indice", False)
        oBanco.monta_Parametro(vParametro, pNm_Contrato_Indice, "@pNm_Contrato_Indice", False)
        oBanco.monta_Parametro(vParametro, pObs_Contrato_Indice, "@pObs_Contrato_Indice", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Contrato_Indice", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Contrato_Indice", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (fatura nota fiscal)
    <WebMethod()>
    Public Function Fatura_Nota_Fiscal(ByVal pPConn_Banco As System.String,
                                       ByVal pPakage As System.String,
                                       ByVal pId_Fatura As System.Int32,
                                       ByVal pId_Centro_Custo As System.Int32,
                                       ByVal pNr_Nota_Fiscal As System.String,
                                       ByVal pVr_Nota_Fiscal As System.Single,
                                       ByVal pPct_Nota_Fiscal As System.Single,
                                       ByVal pId_Usuario_Permissao As System.Int32,
                                       ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(6)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Fatura, "@pId_Fatura", False)
        oBanco.monta_Parametro(vParametro, pNr_Nota_Fiscal, "@pNr_Nota_Fiscal", False)
        oBanco.monta_Parametro(vParametro, pVr_Nota_Fiscal, "@pVr_Nota_Fiscal", False)
        oBanco.monta_Parametro(vParametro, pPct_Nota_Fiscal, "@pPct_Nota_Fiscal", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)
        oBanco.monta_Parametro(vParametro, pId_Centro_Custo, "@pId_Centro_Custo", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Fatura_Nota_Fiscal", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Fatura_Nota_Fiscal", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    ' [INÍCIO - ICTRL-NF-202506-026]

    ' Novo método com nome único para agendar aceite E e-mail (9 parâmetros)
    <WebMethod()>
    Public Function Termo_Aceite_AgendarComEmail(ByVal pPConn_Banco As String,
                                               ByVal pAcao As String,
                                               ByVal pId_Consumidor As Integer,
                                               ByVal pId_Ativo As Integer,
                                               ByVal pHash_Acesso As String,
                                               ByVal pEmail_EnviadoPara As String,
                                               ByVal pAssuntoEmail As String,
                                               ByVal pTextoAdicional As String,
                                               ByVal pNmUsuarioAtual As String) As DataSet
        ReDim vParametro(7)
        oBanco.monta_Parametro(vParametro, pAcao, "@pAcao", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor, "@pId_Consumidor", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
        oBanco.monta_Parametro(vParametro, pHash_Acesso, "@pHash_Acesso", False)
        oBanco.monta_Parametro(vParametro, pEmail_EnviadoPara, "@pEmail_EnviadoPara", False)
        oBanco.monta_Parametro(vParametro, pAssuntoEmail, "@pAssuntoEmail", False)
        oBanco.monta_Parametro(vParametro, pTextoAdicional, "@pTextoAdicional", False)
        oBanco.monta_Parametro(vParametro, pNmUsuarioAtual, "@pNmUsuarioAtual", False)
        Return oBanco.retorna_Query("dbo.pa_Termo_Aceite", vParametro, pPConn_Banco)
    End Function

    ' Novo método com nome único para ações simples como validar e confirmar (6 parâmetros)
    <WebMethod()>
    Public Function Termo_Aceite_Validar(ByVal pPConn_Banco As String,
                                       ByVal pAcao As String,
                                       ByVal pId_Consumidor As Integer,
                                       ByVal pId_Ativo As Integer,
                                       ByVal pHash_Acesso As String,
                                       ByVal pEmail_EnviadoPara As String) As DataSet
        ' Chama a mesma função anterior, passando Nothing para os parâmetros extras.
        Return Me.Termo_Aceite_AgendarComEmail(pPConn_Banco, pAcao, pId_Consumidor, pId_Ativo, pHash_Acesso, pEmail_EnviadoPara, Nothing, Nothing, Nothing)
    End Function
    ' [FIM - ICTRL-NF-202506-026]
End Class
