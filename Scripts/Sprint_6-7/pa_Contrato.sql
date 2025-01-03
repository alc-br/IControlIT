SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pa_Contrato] ( @pPakage    VARCHAR(200),  
          @pId_Contrato   INT    = NULL,  
          @pNr_Contrato   VARCHAR(50)  = NULL,  
          @pId_Contrato_Status INT    = NULL,  
          @pDescricao    VARCHAR(100) = NULL,  
          @pId_Servico   INT    = NULL,  
          @pDt_Inicio_Vigencia DATETIME  = NULL,  
          @pDt_Fim_Vigencia  DATETIME  = NULL,  
          @pId_Filial    INT    = NULL,  
          @pId_Empresa_Contratada INT    = NULL,  
          @pObjeto    VARCHAR(8000) = NULL,  
		  @pId_Contrato_Indice    INT = NULL,  
          @pId_Usuario_Permissao INT    = NULL) AS  
-----insere ou atualiza registro  
IF @pPAKAGE = 'sp_SM'  
BEGIN  
 IF EXISTS (SELECT * FROM Contrato WHERE Id_Contrato = @pId_Contrato)  
 BEGIN  
  IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Alterar') = 2  
  BEGIN  
   UPDATE Contrato   
   SET  Nr_Contrato    = @pNr_Contrato,  
     Descricao    = @pDescricao,  
     Id_Contrato_Status  = @pId_Contrato_Status,  
     Id_Servico    = @pId_Servico,  
     Dt_Inicio_Vigencia  = @pDt_Inicio_Vigencia,  
     Dt_Fim_Vigencia   = @pDt_Fim_Vigencia,  
     Id_Filial    = @pId_Filial,  
     Id_Empresa_Contratada = @pId_Empresa_Contratada,  
     Objeto    = @pObjeto,
	 Id_Contrato_Indice = @pId_Contrato_Indice
   WHERE Id_Contrato = @pId_Contrato  
  END  
 END  
 ELSE  
 BEGIN  
  IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Incluir') = 2  
  BEGIN  
   INSERT INTO Contrato ( Nr_Contrato,  
         Descricao,  
         Id_Contrato_Status,  
         Id_Servico,  
         Dt_Inicio_Vigencia,  
         Dt_Fim_Vigencia,  
         Id_Filial,  
         Id_Empresa_Contratada,  
         Objeto,
		 Id_Contrato_Indice,
         Fl_Desativado)   
   VALUES( @pNr_Contrato,  
     @pDescricao,  
     @pId_Contrato_Status,  
     @pId_Servico,  
     @pDt_Inicio_Vigencia,  
     @pDt_Fim_Vigencia,  
     @pId_Filial,  
     @pId_Empresa_Contratada,  
     @pObjeto,
	 @pId_Contrato_Indice,
     0)  
  END  
 END  
END  
  
-----desativa resgistro  
IF @pPAKAGE = 'sp_SE'  
BEGIN  
 IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Excluir') = 2  
 BEGIN  
  BEGIN  
   UPDATE Contrato   
   SET  Fl_Desativado = 1
   WHERE Id_Contrato = @pId_Contrato  
  END  
 END  
END  
  
-----consulta pelo ID do registro  
IF @pPAKAGE = 'sp_SL_ID'  
BEGIN  
 SELECT ISNULL(Id_Contrato,'')     AS Id_Contrato,  
   ISNULL(Nr_Contrato,'')     AS Nr_Contrato,  
   ISNULL(Descricao,'')     AS Descricao,  
   ISNULL(Id_Contrato_Status,'')   AS Id_Contrato_Status,  
   ISNULL(Id_Servico,'')     AS Id_Servico,  
   ISNULL(Dt_Inicio_Vigencia,'1900-01-01') AS Dt_Inicio_Vigencia,  
   ISNULL(Dt_Fim_Vigencia,'1900-01-01') AS Dt_Fim_Vigencia,  
   ISNULL(Id_Filial,'')     AS Id_Filial,  
   ISNULL(Id_Empresa_Contratada,'')  AS Id_Empresa_Contratada,  
   ISNULL(Objeto,'')      AS Objeto,
   ISNULL(Id_Contrato_Indice,'')      AS Id_Contrato_Indice
 FROM Contrato  
 WHERE Id_Contrato = @pId_Contrato  
END  
  
-----consulta pelo ID do registro  
IF @pPAKAGE = 'sp_SL_View'  
BEGIN  
 SELECT ISNULL(Id_Contrato,0)        AS Id_Contrato,  
   ISNULL(Nr_Contrato,'')        AS Nr_Contrato,  
   ISNULL(Descricao,'')        AS Descricao,  
   ISNULL(CS.Nm_Contrato_Status,'')     AS Nm_Contrato_Status,  
   ISNULL(S.Nm_Servico,'')        AS Nm_Servico,  
   ISNULL(Dt_Inicio_Vigencia,'1900-01-01')    AS Dt_Inicio_Vigencia,  
   ISNULL(Dt_Fim_Vigencia,'1900-01-01')    AS Dt_Fim_Vigencia,  
   ISNULL(F.Nm_Filial,'Todas')       AS Nm_Filial,  
   ISNULL(EC.Nm_Empresa_Contratada,'')     AS Nm_Empresa_Contratada,  
   ISNULL(Objeto,'')         AS Objeto,  
   ISNULL((SELECT TOP 1 Nm_Holding FROM Holding),'') AS Empresa  
 FROM vw_Contrato C INNER JOIN Contrato_Status CS ON C.Id_Contrato_Status = CS.Id_Contrato_Status  
       INNER JOIN Servico S ON C.Id_Servico = S.Id_Servico  
       INNER JOIN Empresa_Contratada EC ON C.Id_Empresa_Contratada = EC.Id_Empresa_Contratada  
       LEFT JOIN Filial F ON C.Id_Filial = F.Id_Filial  
 WHERE Id_Contrato = @pId_Contrato  
END  
  
-----consulta sla servico  
IF @pPAKAGE = 'sp_SL_Consulta_Produto'  
BEGIN  
 SELECT ISNULL(Id_Contrato_SLA_Servico, '') AS Id_Contrato_SLA_Servico,  
   ISNULL(Descricao,'')    AS Descricao,  
   ISNULL(Vr_SLA_Servico,0)   AS Valor,  
   ISNULL(Tipo_Servico,'')    AS Tipo_Servico  
 FROM vw_Contrato_SLA_Servico  
 WHERE Id_Contrato = @pId_Contrato  
END  
  
-----consulta sla operacao  
IF @pPAKAGE = 'sp_SL_Consulta_SLA'  
BEGIN  
 SELECT ISNULL(Id_Contrato_SLA_Operacao, '') AS Id_Contrato_SLA_Operacao,  
   ISNULL(Descricao,'')     AS Descricao,  
   ISNULL(Prazo_Dias,'')     AS Prazo,  
   ISNULL(Vr_SLA_Operacao,0)    AS Valor  
 FROM vw_Contrato_SLA_Operacao  
 WHERE Id_Contrato = @pId_Contrato  
END  
  
-----consulta aditivo  
IF @pPAKAGE = 'sp_SL_Consulta_Aditivo'  
BEGIN  
 SELECT ISNULL(Id_Contrato_Aditivo, '')     AS Id_Contrato_Aditivo,  
   ISNULL(CONVERT(VARCHAR(10),Dt_Vigencia,103),'') AS Dt_Vigencia,  
   ISNULL(Descricao,'')       AS Descricao  
 FROM vw_Contrato_Aditivo  
 WHERE Id_Contrato = @pId_Contrato  
END  
  
-----consulta conta  
IF @pPAKAGE = 'sp_SL_Consulta_Conta'  
BEGIN  
 SELECT FC.NR_Plano_Conta AS Conta,  
   AT.Nm_Ativo_Tipo AS Tipo,  
   CONVERT(VARCHAR,FC.Lote_Cancelamento, 103) AS Cancelamento,  
   (SELECT COUNT(DISTINCT Nr_Ativo) FROM Ativo   
    WHERE Id_Ativo_Tipo = AT.Id_Ativo_Tipo  
     AND Id_Ativo IN (SELECT Id_Ativo FROM Lote WHERE DC_Nr_Nota_Fiscal = FC.NR_Plano_Conta )) AS QTD,  
   AT.Id_Ativo_Tipo  
 FROM Fatura_Plano_Conta FC INNER JOIN vw_Contrato CO ON (FC.Id_Contrato = CO.Id_Contrato)  
         INNER JOIN Lote LT ON (LT.DC_Nr_Nota_Fiscal = FC.NR_Plano_Conta)  
         INNER JOIN Ativo A ON (A.Id_Ativo = LT.ID_Ativo)  
         INNER JOIN Ativo_Tipo AT ON (A.ID_Ativo_Tipo = AT.Id_Ativo_Tipo)  
 WHERE FC.Id_Contrato = @pId_Contrato  
 GROUP BY FC.NR_Plano_Conta, FC.Lote_Cancelamento, AT.Id_Ativo_Tipo, AT.Nm_Ativo_Tipo  
 ORDER BY NR_Plano_Conta DESC  
END  
  
-----consulta conta  
IF @pPAKAGE = 'sp_SL_Ativo_Conta'  
BEGIN  
 SELECT L.DC_Nr_Nota_Fiscal,  
   AT.Nm_Ativo_Tipo,  
   AM.Descricao,  
   (SELECT COUNT(DISTINCT Nr_Ativo) FROM Ativo   
    WHERE (Id_Ativo_Modelo = AM.Id_Ativo_Modelo OR AM.Id_Ativo_Modelo IS NULL)  
     AND Id_Ativo IN (SELECT Id_Ativo FROM Lote WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal )) AS QTD  
       
 FROM Lote L INNER JOIN Ativo A ON A.Id_Ativo = L.Id_Ativo  
     INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo  
     LEFT JOIN vw_Ativo_Modelo_Fabricante AM ON AM.Id_Ativo_Modelo = A.Id_Ativo_Modelo  
 WHERE L.DC_Nr_Nota_Fiscal = @pNr_Contrato  
   AND AT.Id_Ativo_Tipo = @pId_Contrato  
 GROUP BY L.DC_Nr_Nota_Fiscal, AT.Nm_Ativo_Tipo, AM.Id_Ativo_Modelo, AM.Descricao  
 ORDER BY QTD DESC  
END