Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSModulo
    Inherits WebService

    Dim oBanco As New cls_Banco
    Dim vParametro() As SqlClient.SqlParameter

    '-----busca banco
    <WebMethod()>
    Public Function Conn_Banco() As Data.DataSet
        Dim i As System.Int32 = 0

        '-----cria dataset para armazenar dados drag drop
        Dim vDS As New System.Data.DataSet
        vDS.DataSetName = "vDataSet"
        '-----cria datatable
        Dim vDataTable As Data.DataTable = New Data.DataTable("vDataTableInclui")
        '-----cria colunas
        Dim vCodigo As Data.DataColumn = New Data.DataColumn("Codigo", GetType(System.String))
        Dim vDescricao As Data.DataColumn = New Data.DataColumn("Descricao", GetType(System.String))
        '-----adiciona colunas na tabela
        vDataTable.Columns.Add(vCodigo)
        vDataTable.Columns.Add(vDescricao)
        '-----adiciona tabela no dataset
        vDS.Tables.Add(vDataTable)

        '-----carrega dataset
        Dim vLinha As Data.DataRow

        For i = 0 To System.Configuration.ConfigurationManager.AppSettings.Count - 1
            vLinha = vDataTable.NewRow
            vLinha("Codigo") = System.Configuration.ConfigurationManager.AppSettings.Item(i)
            vLinha("Descricao") = System.Configuration.ConfigurationManager.AppSettings.Keys.Item(i)
            vDataTable.Rows.Add(vLinha)
        Next

        vDS.AcceptChanges()
        Return vDS
    End Function

    '-----validacao global de usuario
    <WebMethod()>
    Public Function Validacao_Global(ByVal pPConn_Banco As System.String,
                                    ByVal pPakage As System.String,
                                    ByVal pNm_Usuario As System.String,
                                    ByVal pSenha As System.String,
                                    ByVal pNova_Senha As System.String,
                                    ByVal pEmail_Corporativo As System.String,
                                    ByVal pId_Facebook As System.String,
                                    ByVal pChave_Validacao As System.String,
                                    ByVal pFl_Automatico As System.String) As System.Data.DataSet

        ReDim vParametro(7)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pNm_Usuario, "@pNm_Usuario", False)
        oBanco.monta_Parametro(vParametro, pSenha, "@pSenha", False)
        oBanco.monta_Parametro(vParametro, pNova_Senha, "@pNova_Senha", False)
        oBanco.monta_Parametro(vParametro, pEmail_Corporativo, "@pEmail_Corporativo", False)
        oBanco.monta_Parametro(vParametro, pId_Facebook, "@pId_Facebook", False)
        oBanco.monta_Parametro(vParametro, pChave_Validacao, "@pChave_Validacao", False)
        oBanco.monta_Parametro(vParametro, pFl_Automatico, "@pFl_Automatico", False)

        Return oBanco.retorna_Query("dbo.pa_si_Validacao_Global", vParametro, pPConn_Banco)
    End Function

    '-----valida usuario,menu e traducao de campo
    <WebMethod()>
    Public Function Validacao(ByVal pPConn_Banco As System.String,
                                ByVal pPakage As System.String,
                                ByVal pNm_Usuario As System.String,
                                ByVal pId_Menu As System.Int32,
                                ByVal pSenha As System.String,
                                ByVal pLink As System.String,
                                ByVal pId_Idioma As System.Int32) As System.Data.DataSet
        ReDim vParametro(5)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pNm_Usuario, "@pNm_Usuario", False)
        oBanco.monta_Parametro(vParametro, pId_Menu, "@pId_Menu", False)
        oBanco.monta_Parametro(vParametro, pSenha, "@pSenha", False)
        oBanco.monta_Parametro(vParametro, pLink, "@pLink", False)
        oBanco.monta_Parametro(vParametro, pId_Idioma, "@pId_Idioma", False)

        Return oBanco.retorna_Query("dbo.pa_si_Validacao", vParametro, pPConn_Banco)
    End Function

    '-----atuliza de acordo do usuario
    <WebMethod()>
    Public Function Status_Acordo(ByVal pPConn_Banco As System.String,
                                    ByVal pPakage As System.String,
                                    ByVal pNm_Usuario As System.Int32) As System.Data.DataSet
        ReDim vParametro(1)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pNm_Usuario, "@pNm_Usuario", False)

        oBanco.manutencao_Dados("pa_si_Validacao", vParametro, pPConn_Banco)
        Return Nothing
    End Function

    '-----valida usuario para relatorio
    <WebMethod()>
    Public Function Validacao_Relatorio(ByVal pPConn_Banco As System.String,
                                        ByVal pPakage As System.String,
                                        ByVal pId_Template As System.Int32,
                                        ByVal pId_Usuario As System.Int32,
                                        ByVal pId_Ativo_Tipo_Grupo As System.Int32) As System.Data.DataSet

        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Template, "@pId_Template", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo_Grupo, "@pId_Ativo_Tipo_Grupo", False)

        Return oBanco.retorna_Query("dbo.pa_si_Template", vParametro, pPConn_Banco)
    End Function

    '-----monta dados para deskboard
    <WebMethod()>
    Public Function Deskboard(ByVal pPConn_Banco As System.String,
                                ByVal pPakage As System.String,
                                ByVal pGrupoTipoAtivo As System.String,
                                ByVal pId_Usuario As System.Int32,
                                ByVal pMes As System.String) As System.Data.DataSet

        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)
        oBanco.monta_Parametro(vParametro, pMes, "@pMes", False)
        oBanco.monta_Parametro(vParametro, pGrupoTipoAtivo, "@pGrupoTipoAtivo", False)

        Return oBanco.retorna_Query("dbo.pa_si_Dados_Tendencia_Geral", vParametro, pPConn_Banco)
    End Function

    '-----monta dados carga de bilhete
    <WebMethod()>
    Public Function Carga_Bilhete(ByVal pPConn_Banco As System.String,
                                    ByVal pPakage As System.String,
                                    ByVal pId_Arquivo As System.Int32,
                                    ByVal pId_Ativo_Tipo As System.Int32,
                                    ByVal pId_Conglomerado As System.Int32,
                                    ByVal pDt_Lote As System.String,
                                    ByVal pDt_Liberacao_Lote As System.DateTime,
                                    ByVal pNumero_Nota_Fiscal As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(6)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Arquivo, "@pId_Arquivo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pDt_Lote, "@pDt_Lote", False)
        oBanco.monta_Parametro(vParametro, pDt_Liberacao_Lote, "@pDt_Liberacao_Lote", False)
        oBanco.monta_Parametro(vParametro, pNumero_Nota_Fiscal, "@pNumero_Nota_Fiscal", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.cg_bi_Gera_Importacao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.cg_bi_Gera_Importacao", vParametro, pPConn_Banco)
            Return Nothing
        End If

    End Function

    '-----valida dados carga de bilhete
    <WebMethod()>
    Public Function Valida_Bilhete(ByVal pPConn_Banco As System.String,
                                    ByVal pPakage As System.String,
                                    ByVal pId_Arquivo As System.Int32,
                                    ByVal pId_Ativo_Tipo As System.Int32,
                                    ByVal pId_Conglomerado As System.Int32,
                                    ByVal pDt_Lote As System.String,
                                    ByVal pDt_Liberacao_Lote As System.DateTime,
                                    ByVal pNumero_Nota_Fiscal As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(6)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Arquivo, "@pId_Arquivo", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pDt_Lote, "@pDt_Lote", False)
        oBanco.monta_Parametro(vParametro, pDt_Liberacao_Lote, "@pDt_Liberacao_Lote", False)
        oBanco.monta_Parametro(vParametro, pNumero_Nota_Fiscal, "@pNumero_Nota_Fiscal", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.cg_bi_Valida_Importacao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.cg_bi_Valida_Importacao", vParametro, pPConn_Banco)
            Return Nothing
        End If

    End Function

    '-----monta dados carga de usuario
    <WebMethod()>
    Public Function Carga_Usuario(ByVal pPConn_Banco As System.String,
                                    ByVal pPakage As System.String,
                                    ByVal pId_Campo As System.Int32,
                                    ByVal pCampo_Arquivo As System.String,
                                    ByVal pColuna_Fixo As System.String,
                                    ByVal pImportacao As System.String,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Campo, "@pId_Campo", False)
        oBanco.monta_Parametro(vParametro, pCampo_Arquivo, "@pCampo_Arquivo", False)
        oBanco.monta_Parametro(vParametro, pColuna_Fixo, "@pColuna_Fixo", False)
        oBanco.monta_Parametro(vParametro, pImportacao, "@pImportacao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.cg_co_us_Gera_Importacao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.cg_co_us_Gera_Importacao", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function
    <WebMethod()>
    Public Function ConsumidorUsuario(ByVal pPconn_Banco As System.String,
                                      ByVal pPakage As System.String,
                                      ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(0)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)

        Return oBanco.retorna_Query("dbo.cn_Usuario_Consumidor", vParametro, pPconn_Banco)

    End Function

    '-----monta dados carga de inventario
    <WebMethod()>
    Public Function Carga_Inventario(ByVal pPConn_Banco As System.String,
                                    ByVal pPakage As System.String,
                                    ByVal pId_Arquivo As System.Int32,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(1)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Arquivo, "@pId_Arquivo", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.cg_inv_Gera_Importacao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.cg_inv_Gera_Importacao", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----monta dados carga de usuario
    <WebMethod()>
    Public Function Bilhete_Manual(ByVal pPConn_Banco As System.String,
                                    ByVal pNr_Ativo As System.String,
                                    ByVal pValor As System.Double,
                                    ByVal pDataLote As System.String,
                                    ByVal pData As System.DateTime,
                                    ByVal pNotaFiscal As System.String,
                                    ByVal pIdTipoBilhete As System.Int32,
                                    ByVal pIdBilhete As System.Int32,
                                    ByVal pPakage As System.String,
                                    ByVal pId_Usuario_Permissao As System.Int32,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(8)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pNr_Ativo, "@pNr_Ativo", False)
        oBanco.monta_Parametro(vParametro, pValor, "@pValor", False)
        oBanco.monta_Parametro(vParametro, pDataLote, "@pDataLote", False)
        oBanco.monta_Parametro(vParametro, pData, "@pData", False)
        oBanco.monta_Parametro(vParametro, pNotaFiscal, "@pNotaFiscal", False)
        oBanco.monta_Parametro(vParametro, pIdTipoBilhete, "@pIdTipoBilhete", False)
        oBanco.monta_Parametro(vParametro, pIdBilhete, "@pIdBilhete", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Bilhete_Manual", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Bilhete_Manual", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----cadastro e consulta (Tipo de Biilhete Manual)
    <WebMethod()>
    Public Function Bilhete_Tipo_Manual(ByVal pPConn_Banco As System.String,
                                        ByVal pId_Bilhete_Tipo As System.String,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(2)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Bilhete_Tipo, "@pId_Bilhete_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Bilhete_Tipo_Manual", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Bilhete_Tipo_Manual", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----verifica alerta do sistema
    <WebMethod()>
    Public Function Alerta_Sistema(ByVal pPConn_Banco As System.String,
                                    ByVal pFiltro_1 As System.String,
                                    ByVal pPakage As System.String) As System.Data.DataSet

        ReDim vParametro(1)
        oBanco.monta_Parametro(vParametro, pFiltro_1, "@pFiltro_1", False)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)

        Return oBanco.retorna_Query("dbo.pa_Alerta", vParametro, pPConn_Banco)
    End Function

    '-----lista arquivo da pasta
    <WebMethod()>
    Public Function Arquivo_Pasta(ByVal pPConn_Banco As System.String,
                                    ByVal pPakage As System.String,
                                    ByVal pPasta As System.String) As System.Data.DataSet

        ReDim vParametro(1)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pPasta, "@pPasta", False)

        Return oBanco.retorna_Query("dbo.cg_Pasta_Upload", vParametro, pPConn_Banco)
    End Function

    '-----monta carga de arquivo importado
    <WebMethod()>
    Public Function Monta_Carga(ByVal pPConn_Banco As System.String,
                                ByVal pPakage As System.String,
                                ByVal pPasta As System.String,
                                ByVal pOperadora As System.String) As System.Data.DataSet

        ReDim vParametro(1)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pPasta, "@pPasta", False)

        Return oBanco.convert_Arquivo("dbo.CONVERT_BILHETE", vParametro, pPConn_Banco, pOperadora)
    End Function

    '-----monta texto de termo
    <WebMethod()>
    Public Function Monta_Texto_Termo(ByVal pPConn_Banco As System.String,
                                        ByVal pPakage As System.String,
                                        ByVal pEmpresa As System.String) As System.Data.DataSet

        ReDim vParametro(1)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pEmpresa, "@pEmpresa", False)

        Return oBanco.retorna_Query("dbo.pa_si_Texto_Termo", vParametro, pPConn_Banco)
    End Function

    '-----cadastro e consulta (solicitacao)
    <WebMethod()>
    Public Function Solicitacao(ByVal pPConn_Banco As System.String,
                                ByVal pId_Solicitacao As System.Int32,
                                ByVal pNm_Solicitacao As System.String,
                                ByVal pId_Usuario As System.Int32,
                                ByVal pNm_Usuario As System.String,
                                ByVal pId_Ativo_Tipo As System.Int32,
                                ByVal pId_Solicitacao_Tipo As System.Int32,
                                ByVal pId_Solicitacao_Item As System.String,
                                ByVal pNm_Solicitacao_Item As System.String,
                                ByVal pFl_Status As System.Int32,
                                ByVal pExcedente_SLA As System.String,
                                ByVal pDt_LoteDe As System.String,
                                ByVal pDt_LoteAte As System.String,
                                ByVal pId_Solicitacao_Solucao As System.Int32,
                                ByVal pId_Consumidor_Unidade As System.Int32,
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(15)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao, "@pId_Solicitacao", True)
        oBanco.monta_Parametro(vParametro, pNm_Solicitacao, "@pNm_Solicitacao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)
        oBanco.monta_Parametro(vParametro, pNm_Usuario, "@pNm_Usuario", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo_Tipo, "@pId_Ativo_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao_Tipo, "@pId_Solicitacao_Tipo", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao_Item, "@pId_Solicitacao_Item", False)
        oBanco.monta_Parametro(vParametro, pNm_Solicitacao_Item, "@pNm_Solicitacao_Item", False)
        oBanco.monta_Parametro(vParametro, pFl_Status, "@pFl_Status", False)
        oBanco.monta_Parametro(vParametro, pExcedente_SLA, "@pExcedente_SLA", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteDe, "@pDt_LoteDe", False)
        oBanco.monta_Parametro(vParametro, pDt_LoteAte, "@pDt_LoteAte", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao_Solucao, "@pId_Solicitacao_Solucao", False)
        oBanco.monta_Parametro(vParametro, pId_Consumidor_Unidade, "@pId_Consumidor_Unidade", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Solicitacao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Solicitacao", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (avaliacao de solicitacao)
    <WebMethod()>
    Public Function Solicitacao_Avaliacao(ByVal pPConn_Banco As System.String,
                                            ByVal pId_Solicitacao As System.Int32,
                                            ByVal pId_Solicitacao_Avaliacao As System.Int32,
                                            ByVal pDt_Avaliacao As System.DateTime,
                                            ByVal pAvaliacao As System.Int32,
                                            ByVal pDescricao As System.String,
                                            ByVal pId_Usuario_Permissao As System.Int32,
                                            ByVal pPakage As System.String,
                                            ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(6)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao, "@pId_Solicitacao", False)
        oBanco.monta_Parametro(vParametro, pId_Solicitacao_Avaliacao, "@pId_Solicitacao_Avaliacao", False)
        oBanco.monta_Parametro(vParametro, pDt_Avaliacao, "@pDt_Avaliacao", False)
        oBanco.monta_Parametro(vParametro, pAvaliacao, "@pAvaliacao", False)
        oBanco.monta_Parametro(vParametro, pDescricao, "@pDescricao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Solicitacao_Avaliacao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Solicitacao_Avaliacao", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----cadastro e consulta (trafego aplicativo)
    <WebMethod()>
    Public Function Ativo_App_Trafego(ByVal pPConn_Banco As System.String,
                                        ByVal pNumero_Linha As System.String,
                                        ByVal pId_Ativo As System.Int32,
                                        ByVal pString_Dados As System.String,
                                        ByVal pMes As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(4)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pNumero_Linha, "@pNumero_Linha", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
        oBanco.monta_Parametro(vParametro, pString_Dados, "@pString_Dados", False)
        oBanco.monta_Parametro(vParametro, pMes, "@pMes", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Ativo_App_Trafego", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Ativo_App_Trafego", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function

    '-----consulta monitoramento de dados
    <WebMethod()>
    Public Function Monitoramento_Dados(ByVal pPConn_Banco As System.String,
                                        ByVal pMes As System.String,
                                        ByVal pId_Conglomerado As System.Int32,
                                        ByVal pId_Ativo As System.Int32,
                                        ByVal pNr_Ativo As System.String,
                                        ByVal pCorteDe As System.Int32,
                                        ByVal pCorteAte As System.Int32,
                                        ByVal pPacoteMB As System.Int32,
                                        ByVal pQtdMes As System.Int32,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(9)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pMes, "@pMes", False)
        oBanco.monta_Parametro(vParametro, pId_Conglomerado, "@pId_Conglomerado", False)
        oBanco.monta_Parametro(vParametro, pId_Ativo, "@pId_Ativo", False)
        oBanco.monta_Parametro(vParametro, pNr_Ativo, "@pNr_Ativo", False)
        oBanco.monta_Parametro(vParametro, pCorteDe, "@pCorteDe", False)
        oBanco.monta_Parametro(vParametro, pCorteAte, "@pCorteAte", False)
        oBanco.monta_Parametro(vParametro, pPacoteMB, "@pPacoteMB", False)
        oBanco.monta_Parametro(vParametro, pQtdMes, "@pQtdMes", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.cn_App_Monitora_Dados", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.cn_App_Monitora_Dados", vParametro, pPConn_Banco)
            Return oBanco.convertRetorno(vParametro(1).Value)
        End If
    End Function
End Class
