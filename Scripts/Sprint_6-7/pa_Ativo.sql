SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[pa_Ativo] ( @pPakage      VARCHAR(200),      
         @pId_Ativo      INT    OUT,      
         @pNr_Ativo      VARCHAR(50)  = NULL,      
         @pFinalidade     VARCHAR(50)  = NULL,      
         @pId_Ativo_Tipo     INT    = NULL,      
         @pId_Conglomerado    INT    = NULL,      
         @pId_Ativo_Modelo    INT    = NULL,      
         @pLocalidade     VARCHAR(10)  = NULL,      
         @pDt_Ativacao     DATETIME  = NULL,       
         @pObservacao     VARCHAR(8000) = NULL,      
         @pAtivo_Complemento    VARCHAR(MAX) = NULL,      
         @pId_Ativo_Status    INT    = NULL,      
         @pArray_Consumidor    VARCHAR(MAX) = NULL,      
         @pId_Usuario_Permissao   INT    = NUll,      
         @pEndereco      varchar(512) = Null,      
         @pNumero_Sim_Card    varchar(256) = Null,      
         @pValor_Contrato    varchar(256) = Null,      
         @pPlano_Contrato    varchar(256) = Null,      
         @pVelocidade     varchar(256) = Null) AS      
-----insere ou atualiza registro      
IF @pPAKAGE = 'sp_SM'      
BEGIN      
 DECLARE @Array VARCHAR(MAX)      
 DECLARE @S VARCHAR(10)      
 DECLARE @vSQL VARCHAR(8000)      
 DECLARE @Relacionamento INT      
 SET @Relacionamento = 0      
      
 IF EXISTS (SELECT * FROM Ativo WHERE Id_Ativo = @pId_Ativo)      
 BEGIN      
  IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Alterar') = 2      
  BEGIN      
   UPDATE Ativo       
   SET  Nr_Ativo      = @pNr_Ativo,      
     Finalidade      = @pFinalidade,      
     Id_Ativo_Tipo     = @pId_Ativo_Tipo,      
     Id_Conglomerado     = @pId_Conglomerado,      
     Id_Ativo_Modelo     = @pId_Ativo_Modelo,      
     Localidade      = @pLocalidade,      
     Dt_Ativacao      = @pDt_Ativacao,      
     Observacao =     '(Data Alteração - ' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + ' - ' + @pObservacao + ')  ' + ISNULL(Observacao,''),      
     Ativo_Complemento    = @pAtivo_Complemento,      
     Id_Ativo_Status     = @pId_Ativo_Status,      
     Fl_Desativado     = 0,      
     Endereco      = @pEndereco,      
     Numero_Sim_Card     = @pNumero_Sim_Card,      
     Valor_Contrato     = @pValor_Contrato,      
     Plano_Contrato     = @pPlano_Contrato,      
     Velocidade      = @pVelocidade      
      
   WHERE Id_Ativo = @pId_Ativo      
   SET @Relacionamento = 1      
  END      
 END      
 ELSE      
 BEGIN      
  IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Incluir') = 2 AND      
   NOT EXISTS (SELECT * FROM Ativo WHERE Nr_Ativo = @pNr_Ativo AND Id_Conglomerado = @pId_Conglomerado)      
  BEGIN      
   INSERT INTO Ativo ( Nr_Ativo,      
        Finalidade,      
        Id_Ativo_Tipo,      
        Id_Conglomerado,      
        Id_Ativo_Modelo,      
        Localidade,      
        Dt_Ativacao,      
        Observacao,      
        Ativo_Complemento,      
        Id_Ativo_Status,      
        Fl_Desativado,      
        Endereco,      
        Numero_Sim_Card,      
        Valor_Contrato,      
        Plano_Contrato,      
        Velocidade)      
   VALUES( @pNr_Ativo,      
     @pFinalidade,      
     @pId_Ativo_Tipo,      
     @pId_Conglomerado,      
     @pId_Ativo_Modelo,      
     @pLocalidade,      
     @pDt_Ativacao,      
     '(Data Criação - ' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + ' - ' + @pObservacao + ')  ',      
     @pAtivo_Complemento,      
     @pId_Ativo_Status,      
     0,      
     @pEndereco,      
     @pNumero_Sim_Card,      
     @pValor_Contrato,      
     @pPlano_Contrato,      
     @pVelocidade)      
   -----retorna id       
   SELECT @pId_Ativo = MAX(Id_Ativo) FROM Ativo      
   SET @Relacionamento = 1      
  END      
  ELSE      
  BEGIN      
   SELECT @pId_Ativo = 0      
  END      
 END      
      
 -----RODA RELACIONAMENTO DE ATIVO COM CONSUMIDOR      
 IF @Relacionamento = 1       
 BEGIN      
  -----limpa tabela de relaciomanto      
  CREATE TABLE #Tp_Retorno_Relacionamento (T_Qtde INT)      
  SET  @vSQL = 'DELETE FROM Rl_Consumidor_Ativo  WHERE Id_Consumidor IN (SELECT id_Consumidor FROM vw_Consumidor) AND Id_Ativo = ' + CONVERT(VARCHAR(10),@pId_Ativo)      
  EXEC (@vSQL)      
      
  SELECT @Array = @pArray_Consumidor      
  IF LEN(@Array) > 0 SET @Array = @Array + ','       
  WHILE LEN(@Array) > 0      
  BEGIN      
   SELECT @S = LTRIM(SUBSTRING(@Array, 1, CHARINDEX(',', @Array) - 1))      
   SET  @vSQL = 'SELECT COUNT(*) FROM Rl_Consumidor_Ativo WHERE Id_Consumidor = ' +  @S + ' AND Id_Ativo = ' + CONVERT(VARCHAR(10),@pId_Ativo)      
   INSERT INTO #Tp_Retorno_Relacionamento (T_Qtde)      
   EXEC(@vSQL)      
      
   IF (SELECT COUNT(T_Qtde) FROM #Tp_Retorno_Relacionamento) > 0      
   BEGIN      
    SET @vSQL = 'INSERT INTO Rl_Consumidor_Ativo(Id_Ativo,Id_Consumidor)' +       
       'VALUES (' + CONVERT(VARCHAR(10),@pId_Ativo) + ',' + @S + ')'      
    EXEC (@vSQL)      
   END      
   SELECT @Array = SUBSTRING(@Array, CHARINDEX(',', @Array) + 1, LEN(@Array))      
      
  END      
      
  -----limpa tabela de relacionamento de usurio com ativo      
  DECLARE @v_Id_Ativo AS INT      
  DECLARE @v_Id_Usuario AS INT      
      
  DECLARE CurItens CURSOR FOR      
  SELECT Id_Ativo, Id_Usuario FROM Rl_Perfil_Ativo_Usuario RAU       
  WHERE NOT EXISTS(SELECT * FROM Rl_Consumidor_Ativo RCA      
      WHERE RCA.Id_Ativo = RAU.Id_Ativo AND RCA.Id_Consumidor IN (SELECT Id_Consumidor FROM Usuario WHERE Id_Usuario = RAU.Id_Usuario))      
      
  OPEN CurItens      
  FETCH NEXT FROM CurItens INTO @v_Id_Ativo, @v_Id_Usuario      
  -----corre tabela      
  WHILE @@FETCH_STATUS = 0      
  BEGIN      
   DELETE FROM Rl_Perfil_Ativo_Usuario WHERE Id_Ativo = @v_Id_Ativo AND Id_Usuario = @v_Id_Usuario      
      
  FETCH NEXT FROM CurItens INTO @v_Id_Ativo, @v_Id_Usuario      
  END      
  CLOSE CurItens      
  DEALLOCATE CurItens      
      
  -----pede o de acordo novamente ao usuario      
  UPDATE Usuario      
  SET  Fl_Desativado = 3       
  FROM Usuario       
  WHERE Id_Usuario IN (SELECT Id_Usuario FROM Rl_Perfil_Ativo_Usuario WHERE Id_Ativo = @pId_Ativo)      
 END      
END      
      
-----ativa / desativa resgistro      
IF @pPAKAGE = 'sp_SE'      
BEGIN      
 IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Excluir') = 2      
 BEGIN      
  -----envia aparelho para devolucao      
  UPDATE Estoque_Aparelho      
  SET  Id_Aparelho_Tipo = 4      
  FROM Estoque_Aparelho       
  WHERE Id_Aparelho IN (SELECT Id_Aparelho  FROM Rl_Estoque_Aparelho_Ativo WHERE id_Ativo = @pId_Ativo)      
        
  -----desatuiva a linha      
  UPDATE Ativo       
  SET  Fl_Desativado = 1,      
    Dt_Desativacao = GETDATE(),      
    Observacao = '* Desativação | Data Alteração - ' + CONVERT(VARCHAR,GETDATE(),103) + @pObservacao + ' *' + Observacao      
  WHERE Id_Ativo = @pId_Ativo      
 END      
END      
      
-----consulta pelo ID do registro      
IF @pPAKAGE = 'sp_SL_ID'      
BEGIN      
 -----busca equipamento do ativo      
 DECLARE @IMEI VARCHAR(8000)      
 SET @IMEI = ''      
      
 IF EXISTS(SELECT * FROM Rl_Estoque_Aparelho_Ativo REA INNER JOIN Estoque_Aparelho EA ON REA.Id_Aparelho = EA.Id_Aparelho WHERE Id_Ativo = @pId_Ativo)      
 BEGIN      
  SELECT @IMEI = @IMEI + ISNULL(EA.Nr_Aparelho + CASE WHEN EA.Id_Aparelho_Tipo = 1 AND Fl_Desativado = 0 THEN ' (Em Uso)'       
                WHEN EA.Id_Aparelho_Tipo = 2 AND Fl_Desativado = 0 THEN ' (Assitencia)'      
                WHEN EA.Id_Aparelho_Tipo = 3 AND Fl_Desativado = 0 THEN ' (Solicitação)'      
                WHEN EA.Id_Aparelho_Tipo = 4 AND Fl_Desativado = 0 THEN ' (Devolução)'       
                WHEN EA.Id_Aparelho_Tipo = 4 AND Fl_Desativado = 1 THEN ' (Descarte)'      
               END, '')      
  FROM Rl_Estoque_Aparelho_Ativo REA INNER JOIN Estoque_Aparelho EA ON REA.Id_Aparelho = EA.Id_Aparelho      
  WHERE Id_Ativo = @pId_Ativo      
      
  --1 estoque      
  --2 assistencia      
  --3 solicitacao      
  --4 devolucao      
  --SELECT @IMEI = SUBSTRING(@IMEI,1,LEN(@IMEI) - 2)      
 END       
 ELSE      
 BEGIN      
  SET @IMEI = ''      
 END      
      
 SELECT ISNULL(AT.Id_Ativo,'')     AS Id_Ativo,      
   ISNULL(AT.Nr_Ativo,'')     AS Nr_Ativo,      
   ISNULL(AT.Nr_Ativo,'')           AS Nr_Ativo_Tabela,      
   ISNULL(AT.Finalidade,'')    AS Finalidade,      
   ISNULL(AT.Id_Ativo_Tipo,'')    AS Id_Ativo_Tipo,      
   ISNULL(AT.Id_Conglomerado,0)   AS Id_Conglomerado,      
   ISNULL(AT.Id_Ativo_Modelo,0)   AS Id_Ativo_Modelo,      
   ISNULL(AT.Localidade,'')    AS Localidade,      
   ISNULL(AT.Dt_Ativacao, '01/01/1900') AS Dt_Ativacao,      
   ISNULL(AT.Observacao,'')    AS Observacao,      
   ISNULL(AT.Id_Ativo_Status,'')   AS Id_Ativo_Status,      
   ISNULL(@IMEI,'')      AS Equipamento,      
   ISNULL(AT.Endereco, '')     as Endereco,      
   ISNULL(AT.Numero_Sim_Card, '')   as Numero_Sim_Card,      
   ISNULL(AT.Valor_Contrato, '')   as Valor_Contrato,      
   ISNULL(AT.Plano_Contrato, '')   as Plano_Contrato,      
   ISNULL(AT.Velocidade, '')    as Velocidade,   
            ISNULL(AT.Fl_Desativado, '')   as Fl_Desativado   
      
 FROM Ativo AT       
 WHERE AT.Id_Ativo = @pId_Ativo      
END      
      
-----consulta dados de complemto de ativo (insert)      
IF @pPAKAGE = 'sp_SL_Complemento_Insert'      
BEGIN      
 SELECT ISNULL(Nm_Ativo_Complemento,' ') AS Nm_Ativo_Complemento,      
   '' AS Descricao      
 FROM Ativo_Complemento      
 WHERE Id_Ativo_Tipo = @pId_Ativo_Tipo      
END      
      
-----consulta dados de complemto de ativo (update ou select)      
IF @pPAKAGE = 'sp_SL_Complemento_Update'      
BEGIN      
 DECLARE @vCOMPLEMENTO VARCHAR(MAX)      
 DECLARE @vDELIMITADOR_CAMPO VARCHAR(1)       
 DECLARE @vDELIMITADOR_DESCRICAO VARCHAR(1)      
 DECLARE @vCAMPO VARCHAR(50)      
 DECLARE @vDESCRICAO VARCHAR(50)      
      
 -----captura dado camplemento      
 SELECT @vCOMPLEMENTO = ISNULL(Ativo_Complemento,'')      
 FROM vw_Ativo      
 WHERE Id_Ativo = @pId_Ativo      
       
 -----set caracter delimitador      
 SELECT @vDELIMITADOR_CAMPO = '¶'      
 SELECT @vDELIMITADOR_DESCRICAO = '§'      
      
 IF LEN(@vCOMPLEMENTO) > 0 SET @vCOMPLEMENTO = @vCOMPLEMENTO + @vDELIMITADOR_DESCRICAO      
 CREATE TABLE #ARRAY(Campo VARCHAR(50), Descricao VARCHAR(50))      
      
 WHILE LEN(@vCOMPLEMENTO) > 0      
 BEGIN      
  SELECT @vCAMPO = LTRIM(SUBSTRING(@vCOMPLEMENTO, 1, CHARINDEX(@vDELIMITADOR_CAMPO, @vCOMPLEMENTO) - 1))      
  SELECT @vCOMPLEMENTO = SUBSTRING(@vCOMPLEMENTO, CHARINDEX(@vDELIMITADOR_CAMPO, @vCOMPLEMENTO) + 1, LEN(@vCOMPLEMENTO))      
          
  SELECT @vDESCRICAO = LTRIM(SUBSTRING(@vCOMPLEMENTO, 1, CHARINDEX(@vDELIMITADOR_DESCRICAO, @vCOMPLEMENTO) - 1))      
  SELECT @vCOMPLEMENTO = SUBSTRING(@vCOMPLEMENTO, CHARINDEX(@vDELIMITADOR_DESCRICAO, @vCOMPLEMENTO) + 1, LEN(@vCOMPLEMENTO))      
      
  INSERT INTO #ARRAY (Campo,Descricao) VALUES (@vCAMPO,@vDESCRICAO)      
 END      
       
 SELECT ATC.Nm_Ativo_Complemento,      
   ARR.Descricao       
 FROM Ativo_Complemento ATC LEFT JOIN #ARRAY  ARR      
         ON (ARR.Campo = ATC.Nm_Ativo_Complemento)       
 WHERE ATC.Id_Ativo_Tipo = @pId_Ativo_Tipo       
END      
      
-----retorna relacionamento de ativo com consumidor      
IF @pPAKAGE = 'sd_SL_RL_Ativo_Consumidor'      
BEGIN
	--insere data de desativacao para ultima linha
	--IF (SELECT	COUNT(*) 
	--	FROM	Rl_Perfil_Ativo_Usuario 
	--	WHERE	Id_Ativo = @pId_Ativo
	--			AND Dt_Hr_Desativacao IS NULL)	> 1
	--BEGIN
	--	DECLARE @vId_Usuario INT = 0

	--	SELECT	TOP 1 @vId_Usuario = Id_Usuario 
	--	FROM	Rl_Perfil_Ativo_Usuario
	--	WHERE	Id_Ativo = @pId_Ativo
	--			AND Dt_Hr_Desativacao IS NULL
	--	ORDER BY	Dt_Hr_Ativacao ASC

 --		UPDATE	Rl_Perfil_Ativo_Usuario
	--	SET		Dt_Hr_Desativacao = GETDATE()
	--	WHERE	Id_Ativo = @pId_Ativo
	--			AND  Id_Usuario = @vId_Usuario
	--END

	--lista linha vinculado ao ativo
	SELECT DISTINCT ISNULL(CON.Id_Consumidor,'') AS Id_Consumidor,      
		   ISNULL(CON.Nm_Consumidor,'')   AS Nm_Consumidor,      
		   Dt_Hr_Ativacao       AS Dt_Hr_Ativacao,      
		   Dt_Hr_Desativacao      AS Dt_Hr_Desativacao,      
		   'True'         AS Excluir,      
		   'False'         AS Lupa,      
		   'False'         AS Voltar,      
		   -----termo de ativacao      
		   'window.open(' + '''' + '../Termo/' + ISNULL(ATG.Termo_Ativacao,'') + '?Id_Ativo=' + CONVERT(VARCHAR,RCA.Id_Ativo) +        
		   '&Id_Consumidor=' + CONVERT(VARCHAR,RCA.Id_Consumidor) +       
		   '&Nm_Ativo_Tipo_Grupo=' + CONVERT(VARCHAR,ATG.Nm_Ativo_Tipo_Grupo) + '''' + ',' + '''' + '''' + ',' + '''' +      
		   'resizable=yes, menubar=no, scrollbars=yes,height=600, width=800, top=10, left=10' + '''' + ')'      
		   AS Termo_Ativacao,      
		   -----termo de desativacao      
		   'window.open(' + '''' + '../Termo/' + ISNULL(ATG.Termo_Desativacao,'') + '?Id_Ativo=' + CONVERT(VARCHAR,RCA.Id_Ativo) +        
		   '&Id_Consumidor=' + CONVERT(VARCHAR,RCA.Id_Consumidor) +       
		   '&Nm_Ativo_Tipo_Grupo=' + CONVERT(VARCHAR,ATG.Nm_Ativo_Tipo_Grupo) + '''' + ',' + '''' + '''' + ',' + '''' +      
		   'resizable=yes, menubar=no, scrollbars=yes,height=600, width=800, top=10, left=10' + '''' + ')'      
		   AS Termo_Devolucao,      
		   'True'         AS bt_Termo_Ativacao,      
		   'True'         AS bt_Termo_Devolucao
		 
	 FROM	vw_Consumidor CON	INNER JOIN Rl_Consumidor_Ativo RCA ON CON.Id_Consumidor = RCA.Id_Consumidor      
								INNER JOIN Ativo A ON A.Id_Ativo = RCA.Id_Ativo      
								INNER JOIN Ativo_Tipo AT ON A.Id_Ativo_Tipo = AT.Id_Ativo_Tipo       
								INNER JOIN Ativo_Tipo_Grupo ATG ON AT.Id_Ativo_Tipo_Grupo  = ATG.Id_Ativo_Tipo_Grupo       
								LEFT JOIN vw_Usuario US ON US.Id_Consumidor = CON.Id_Consumidor      
								LEFT JOIN Rl_Perfil_Ativo_Usuario RUA ON US.Id_Usuario = RUA.Id_Usuario AND RUA.Id_Ativo = RCA.Id_Ativo
			  
	 WHERE RCA.Id_Ativo = @pId_Ativo        
	 ORDER BY Nm_Consumidor
END      
      
-----retorna dados pra termo      
IF @pPAKAGE = 'sd_SL_Termo'      
BEGIN      
 -----monta ativo complemento      
 DECLARE @sql VARCHAR(8000)      
 DECLARE @T_Ativo_Complemento TABLE (Id_Ativo INT, IMEI VARCHAR(300), CHIP VARCHAR(300), ACESSORIO VARCHAR(300), NF VARCHAR(300), NS VARCHAR(300))      
 SET @sql = 'SELECT Id_Ativo,'      
       
 SELECT       
 -----IMEI      
 @sql = @sql +      
 CASE WHEN NOT EXISTS( SELECT *      
       FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB       
       WHERE NM_ATIVO_COMPLEMENTO = 'IMEI') THEN 'NULL AS IMEI,'      
 ELSE (SELECT 'C'+ CONVERT(VARCHAR(2),REG) + ' AS IMEI,'      
   FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB       
   WHERE NM_ATIVO_COMPLEMENTO = 'IMEI')      
 END,      
       
 -----CHIP      
 @sql = @sql +      
 CASE WHEN NOT EXISTS( SELECT *      
       FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB       
       WHERE NM_ATIVO_COMPLEMENTO = 'CHIP') THEN 'NULL AS CHIP,'      
 ELSE (SELECT 'C'+ CONVERT(VARCHAR(2),REG) + ' AS CHIP,'      
   FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB       
   WHERE NM_ATIVO_COMPLEMENTO = 'CHIP')      
 END,      
      
 -----ACESSORIO      
 @sql = @sql +      
 CASE WHEN NOT EXISTS( SELECT *      
       FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB       
       WHERE NM_ATIVO_COMPLEMENTO = 'ACESSORIO') THEN 'NULL AS ACESSORIO,'      
 ELSE (SELECT 'C'+ CONVERT(VARCHAR(2),REG) + ' AS ACESSORIO,'      
   FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB       
   WHERE NM_ATIVO_COMPLEMENTO = 'ACESSORIO')      
 END,      
      
 -----NF      
 @sql = @sql +      
 CASE WHEN NOT EXISTS( SELECT *      
       FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB       
       WHERE NM_ATIVO_COMPLEMENTO = 'NF') THEN 'NULL AS NF,'      
 ELSE (SELECT 'C'+ CONVERT(VARCHAR(2),REG) + ' AS NF,'      
   FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB       
   WHERE NM_ATIVO_COMPLEMENTO = 'NF')      
 END,      
      
 -----NS      
 @sql = @sql +      
 CASE WHEN NOT EXISTS( SELECT *      
       FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB       
       WHERE NM_ATIVO_COMPLEMENTO = 'NS') THEN 'NULL AS NS,'      
 ELSE (SELECT 'C'+ CONVERT(VARCHAR(2),REG) + ' AS NS,'      
   FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB       
   WHERE NM_ATIVO_COMPLEMENTO = 'NS')      
 END       
      
 SELECT @sql = SUBSTRING(@sql,1, LEN(@sql) - 1) + ' FROM vw_Ativo_Complemento'      
 INSERT INTO @T_Ativo_Complemento EXEC(@sql)      
      
 -----executa sqlserver      
 SELECT DISTINCT       
   ISNULL(CON.Id_Consumidor,'') AS Id_Consumidor,      
   ISNULL(CON.Nm_Consumidor,'') AS Nm_Consumidor,      
   ISNULL(US.Nm_Usuario,'')  AS Nm_Usuario,      
   ISNULL(CON.Matricula,'')  AS Matricula,      
   ISNULL(FI.Endereco,'')   AS Endereco,      
   ISNULL(FI.Nm_Filial,'')   AS Nm_Filial,      
   ISNULL(CC.Cd_Centro_Custo,'') AS Centro_Custo,      
   ISNULL(DP.Nm_Departamento,'') AS Departamento,      
   ISNULL(A.Nr_Ativo,'')   AS Linha,      
   ISNULL(AT.Nm_Ativo_Tipo,'')  AS Tipo_Ativo,      
   ISNULL(CG.Nm_Conglomerado,'') AS Nm_Conglomerado,      
   ISNULL(AMF.Descricao,'')  AS Nm_Modelo_Fabricante,      
   ISNULL(TAC.IMEI,'')    AS IMEI,      
   ISNULL(A.Numero_Sim_Card,'')    AS CHIP,      
   ISNULL(TAC.ACESSORIO,'')  AS ACESSORIO,      
   ISNULL(ISNULL((SELECT TOP 1  Nr_Aparelho       
       FROM Estoque_Aparelho       
       WHERE Id_Aparelho = EAA.Id_Aparelho AND Id_Aparelho_Tipo = 1), TAC.NS), '') AS Nr_Aparelho,      
   ISNULL(ISNULL((SELECT TOP 1 SENF.Nr_Nota_Fiscal       
       FROM Estoque_Aparelho SEA INNER JOIN Estoque_Nota_Fiscal SENF ON SENF.Id_Estoque_Nota_Fiscal = SEA.Id_Estoque_Nota_Fiscal       
       WHERE Id_Aparelho = EAA.Id_Aparelho      
       AND SEA.Id_Aparelho_Tipo = 1), TAC.NF), '') AS NF      
 FROM Consumidor CON  INNER JOIN Rl_Consumidor_Ativo RCA ON CON.Id_Consumidor = RCA.Id_Consumidor      
        INNER JOIN Filial FI ON FI.Id_Filial = CON.Id_Filial      
        INNER JOIN Centro_Custo CC ON CC.Id_Centro_Custo = CON.Id_Centro_Custo      
        INNER JOIN Ativo A ON A.Id_Ativo = RCA.Id_Ativo      
        INNER JOIN Ativo_Tipo AT ON A.Id_Ativo_Tipo = AT.Id_Ativo_Tipo      
        INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado       
        INNER JOIN Usuario US ON US.Id_Consumidor = CON.Id_Consumidor      
        LEFT JOIN Departamento DP ON DP.Id_Departamento = CON.Id_Departamento      
        LEFT JOIN vw_Ativo_Modelo_Fabricante AMF ON AMF.Id_Ativo_Modelo = A.Id_Ativo_Modelo      
        LEFT JOIN Rl_Estoque_Aparelho_Ativo EAA ON EAA.id_Ativo = A.Id_Ativo      
        LEFT JOIN @T_Ativo_Complemento TAC ON TAC.Id_Ativo = A.Id_Ativo      
 WHERE RCA.Id_Ativo = @pId_Ativo      
 ORDER BY Nr_Aparelho DESC      
END      
      
-----retorna dados pra conta com o modelo do ativo      
IF @pPAKAGE = 'sd_SL_Modelo'      
BEGIN      
 SELECT ISNULL(A.Nr_Ativo,'') AS Nr_Ativo,      
   ISNULL(C.Nm_Conglomerado,'') AS Nm_Conglomerado,      
   ISNULL(AT.Nm_Ativo_Tipo,'') AS Nm_Ativo_Tipo,      
   ISNULL(AM.Descricao,'') AS Nm_Ativo_Modelo,      
   ISNULL(EA.Nr_Aparelho,'') AS Nr_Aparelho,      
   ISNULL(A.Finalidade,'') AS Finalidade      
 FROM Ativo A INNER JOIN Rl_Consumidor_Ativo RCA ON A.Id_Ativo = RCA.Id_Ativo       
     LEFT JOIN Conglomerado C ON A.Id_Conglomerado = C.Id_Conglomerado      
     LEFT JOIN Ativo_Tipo AT ON A.Id_Ativo_Tipo = AT.Id_Ativo_Tipo      
     LEFT JOIN vw_Ativo_Modelo_Fabricante AM ON A.Id_Ativo_Modelo = AM.Id_Ativo_Modelo      
     LEFT JOIN Rl_Estoque_Aparelho_Ativo REA ON REA.id_Ativo = A.Id_Ativo       
     LEFT JOIN Estoque_Aparelho EA ON EA.Id_Aparelho = REA.Id_Aparelho      
 WHERE RCA.Id_Consumidor IN (SELECT Id_Consumidor FROM Usuario WHERE Id_Usuario = @pId_Ativo)      
END      
      
-----lista para alerta de ativo      
IF @pPAKAGE = 'sd_SL_Ativo_Alerta'      
BEGIN      
 SELECT ISNULL(A.Id_Ativo,'') AS Id_Ativo,      
   ISNULL(A.Nr_Ativo,'') AS Nr_Ativo      
 FROM Ativo A       
 WHERE A.Nr_Ativo LIKE '%' + @pNr_Ativo + '%' OR (@pNr_Ativo IS NULL AND A.Id_Conglomerado = @pId_Conglomerado)      
END      
      
---alerta ativo      
IF @pPAKAGE = 'sd_SM_Ativo_Alerta'      
BEGIN      
 IF @pDt_Ativacao > GETDATE()      
 BEGIN      
  -----insere a mesma data de suspenssao para o ativo      
  UPDATE Ativo       
  SET  Localidade = CONVERT(VARCHAR, @pDt_Ativacao, 103)      
  WHERE Id_Ativo = @pId_Ativo      
       
  -----insere a msg de retorno de       
  INSERT INTO Mail_Caixa_Saida      
  VALUES (5,      
    ISNULL((SELECT TOP 1 EMail FROM Consumidor WHERE Id_Consumidor IN (SELECT Id_Consumidor FROM Usuario WHERE Id_Usuario = @pId_Usuario_Permissao)),''),      
    ISNULL((SELECT TOP 1 EMail_Copia FROM Consumidor WHERE Id_Consumidor IN (SELECT Id_Consumidor FROM Usuario WHERE Id_Usuario = @pId_Usuario_Permissao)),''),      
    'Ativo(' + ISNULL(@pNr_Ativo,'') + ') ' + ISNULL(@pObservacao,''),      
    @pDt_Ativacao,      
    NULL,      
    0,      
    0,      
    0)      
 END      
END      
      
---cria um ativo pela solicitacao de unidade      
IF @pPAKAGE = 'sd_Cria_Ativo_Solicitacao'      
BEGIN      
 IF NOT EXISTS (SELECT * FROM Ativo WHERE Nr_Ativo = @pNr_Ativo AND Id_Conglomerado = @pId_Conglomerado)      
 BEGIN      
  INSERT INTO Ativo ( Nr_Ativo,      
       Finalidade,      
       Id_Ativo_Tipo,      
       Id_Conglomerado,      
       Id_Ativo_Modelo,      
       Localidade,      
       Dt_Ativacao,      
       Observacao,      
       Ativo_Complemento,      
       Id_Ativo_Status,      
       Fl_Desativado,      
       Endereco,      
       Numero_Sim_Card,      
       Valor_Contrato,      
       Plano_Contrato,      
       Velocidade)      
  VALUES( 'RQ - ' + @pNr_Ativo,      
    'Criado na Requisição - ' + @pNr_Ativo,      
    @pId_Ativo_Tipo,      
    @pId_Conglomerado,      
    @pId_Ativo_Modelo,      
    @pLocalidade,      
    GETDATE(),      
    '(Data Alteração - ' + CONVERT(VARCHAR,GETDATE(),103) + ' - ' + @pObservacao + ')  ',      
    @pAtivo_Complemento,      
    @pId_Ativo_Status,      
    0,      
    @pEndereco,      
    @pNumero_Sim_Card,      
    @pValor_Contrato,      
    @pPlano_Contrato,      
    @pVelocidade)      
      
  -----retorna id       
  SELECT @pId_Ativo = MAX(Id_Ativo) FROM Ativo      
      
  -----relaciona ativo com a solicitacao      
  INSERT INTO Rl_Solicitacao_Ativo      
  VALUES(@pNr_Ativo, @pId_Ativo)      
      
  -----relaciona ativo com a unidade de negocio      
  INSERT INTO Rl_Consumidor_Ativo      
  VALUES ((SELECT Id_Consumidor FROM Consumidor_Unidade WHERE Id_Consumidor_Unidade = @pArray_Consumidor),       
    @pId_Ativo)      
      
  -----vicula ativo ao usuario      
  UPDATE Rl_Perfil_Ativo_Usuario      
  SET  Dt_Hr_Ativacao = CONVERT(DATETIME, '2000/01/01')      
  WHERE Id_Ativo = @pId_Ativo      
      
 END      
 ELSE      
 BEGIN     
  SELECT @pId_Ativo = 0      
 END      
END      
      
---altera numero do ativo pelo numero de serie       
IF @pPAKAGE = 'sd_Altera_Numero_Serie'      
BEGIN      
 IF NOT EXISTS (SELECT * FROM Ativo WHERE Nr_Ativo = @pNr_Ativo AND Id_Conglomerado IN (SELECT Id_Conglomerado FROM Ativo WHERE Id_Ativo = @pId_Ativo))      
 BEGIN      
  UPDATE Ativo       
  SET  Nr_Ativo = @pNr_Ativo      
  WHERE Id_Ativo = @pId_Ativo      
      
  SELECT Id_Ativo FROM Ativo WHERE Id_Ativo = @pId_Ativo      
 END      
 ELSE      
 BEGIN      
  SELECT @pId_Ativo = -1      
 END      
END      
      
---verifica e cancela um ativo de devolucao de solicitacao       
IF @pPAKAGE = 'sd_Cancela_Numero_Serie'      
BEGIN      
 IF NOT EXISTS(SELECT * FROM Ativo WHERE Nr_Ativo = @pNr_Ativo)      
 BEGIN      
  SELECT 'Número de série não encontrado' AS Msg, '0' AS Validacao      
 END       
 ELSE      
 BEGIN      
  IF NOT EXISTS(SELECT *       
     FROM Rl_Consumidor_Ativo       
     WHERE Id_Ativo IN (SELECT Id_Ativo FROM Ativo WHERE Nr_Ativo = @pNr_Ativo AND Id_Conglomerado = @pId_Conglomerado)       
       AND Id_Consumidor IN (SELECT Id_Consumidor       
            FROM Consumidor_Unidade       
            WHERE Id_Consumidor_Unidade = @pId_Ativo_Modelo)) -----******* consumidor      
  BEGIN      
   SELECT 'Número de série não está cadastrado para unidade que solicitou a devolucao' AS Msg, '0' AS Validacao      
  END       
  BEGIN      
   UPDATE Ativo       
   SET  Fl_Desativado = 1,       
     Observacao = '(Data da Devolução - ' + CONVERT(VARCHAR(10), GETDATE(),103) + ' Equipamento recolhido pelo fornecedor) - ' + Observacao      
   WHERE Id_Ativo IN (SELECT Id_Ativo FROM Ativo WHERE Nr_Ativo = @pNr_Ativo AND Id_Conglomerado = @pId_Conglomerado)       
      
   SELECT 'Número de série cancelado' AS Msg, '1' AS Validacao      
  END       
 END       
END
GO


