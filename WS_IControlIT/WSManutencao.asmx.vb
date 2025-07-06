Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
Public Class WSManutencao
    Inherits WebService

    Dim oBanco As New cls_Banco
    Dim vParametro() As SqlClient.SqlParameter

    '-----alteracao e consulta (lote)
    <WebMethod()>
    Public Function Lote(ByVal pPConn_Banco As System.String,
                            ByVal pNm_Usuario As System.String,
                            ByVal pDt_Lote As System.String,
                            ByVal pId_Lote_Marcacao As System.Double,
                            ByVal pDt_Visita As System.Int32,
                            ByVal pDt_Fechamento As System.Int32,
                            ByVal pDt_Exportacao As System.Int32,
                            ByVal pId_Usuario_Permissao As System.Int32,
                            ByVal pPakage As System.String,
                            ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(7)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pNm_Usuario, "@pNm_Usuario", False)
        oBanco.monta_Parametro(vParametro, pDt_Lote, "@pDt_Lote", False)
        oBanco.monta_Parametro(vParametro, pId_Lote_Marcacao, "@pId_Lote_Marcacao", False)
        oBanco.monta_Parametro(vParametro, pDt_Visita, "@pDt_Visita", False)
        oBanco.monta_Parametro(vParametro, pDt_Fechamento, "@pDt_Fechamento", False)
        oBanco.monta_Parametro(vParametro, pDt_Exportacao, "@pDt_Exportacao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)


        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Manutencao_Lote", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Manutencao_Lote", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----alteracao e consulta (Script_Exportacao)
    <WebMethod()>
    Public Function Script_Exportacao(ByVal pPConn_Banco As System.String,
                                        ByVal pScript As System.String,
                                        ByVal pDt_Expotacao As System.DateTime,
                                        ByVal pId_Usuario_Permissao As System.Int32,
                                        ByVal pPakage As System.String,
                                        ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pScript, "@pScript", False)
        oBanco.monta_Parametro(vParametro, pDt_Expotacao, "@pDt_Expotacao", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Script_Exportacao", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Script_Exportacao", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----le e grava afquivo pdf
    <WebMethod()>
    Public Function ArquivoPDF(ByVal pPConn_Banco As System.String,
                                ByVal pId_Arquivo_PDF As System.Int32,
                                ByVal pNm_Arquivo_PDF As System.String,
                                ByVal pTabela_Registro As System.String,
                                ByVal pId_Registro_Tabela As System.Int32,
                                ByVal pTamanho As System.Double,
                                ByVal pArquivo As Byte(),
                                ByVal pId_Usuario_Permissao As System.Int32,
                                ByVal pPakage As System.String,
                                ByVal pRetorno As System.Boolean) As System.Data.DataSet

        ReDim vParametro(7)
        '-----monta parametro do tipo image
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Arquivo_PDF, "@pId_Arquivo_PDF", False)
        oBanco.monta_Parametro(vParametro, pNm_Arquivo_PDF, "@pNm_Arquivo_PDF", False)
        oBanco.monta_Parametro(vParametro, pTabela_Registro, "@pTabela_Registro", False)
        oBanco.monta_Parametro(vParametro, pId_Registro_Tabela, "@pId_Registro_Tabela", False)
        oBanco.monta_Parametro(vParametro, pTamanho, "@pTamanho", False)
        vParametro(6) = New SqlClient.SqlParameter("@pArquivo", SqlDbType.Image)
        vParametro(6).Value = pArquivo
        vParametro(6).Direction = ParameterDirection.Input
        oBanco.monta_Parametro(vParametro, pId_Usuario_Permissao, "@pId_Usuario_Permissao", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Arquivo_PDF", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Arquivo_PDF", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

    '-----caixa de entrada de email
    <WebMethod()>
    Public Function caixa_entrada(ByVal pPConn_Banco As System.String,
                                    ByVal pPakage As System.String,
                                    ByVal pId_Usuario As System.Int32,
                                    ByVal pTexto As System.String,
                                    ByVal pId_Mail_Caixa_Siada As System.Int32,
                                    ByVal pRetorno As System.Boolean) As System.Data.DataSet
        ReDim vParametro(3)
        oBanco.monta_Parametro(vParametro, pPakage, "@pPakage", False)
        oBanco.monta_Parametro(vParametro, pId_Usuario, "@pId_Usuario", False)
        oBanco.monta_Parametro(vParametro, pTexto, "@pTexto", False)
        oBanco.monta_Parametro(vParametro, pId_Mail_Caixa_Siada, "@pId_Mail_Caixa_Siada", False)

        If pRetorno = True Then
            Return oBanco.retorna_Query("dbo.pa_Mail_Sender", vParametro, pPConn_Banco)
        Else
            oBanco.manutencao_Dados("dbo.pa_Mail_Sender", vParametro, pPConn_Banco)
            Return Nothing
        End If
    End Function

End Class