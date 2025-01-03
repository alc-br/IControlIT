USE [Ativvus_Login]
GO
/****** Object:  StoredProcedure [dbo].[pa_si_Validacao_Global]    Script Date: 23/03/2024 08:09:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pa_si_Validacao_Global] (@pPAKAGE   VARCHAR(200),  
            @pNm_Usuario  VARCHAR(50) = NULL,  
            @pSenha    VARCHAR(50) = NULL,  
            @pNova_Senha  VARCHAR(50) = NULL,  
            @pEmail_Corporativo VARCHAR(50) = NULL,  
            @pId_Facebook  VARCHAR(50) = NULL,  
            @pChave_Validacao VARCHAR(250) = NULL,  
            @pFl_Automatico  VARCHAR(50) = NULL) AS  
  
--declare @pPAKAGE   VARCHAR(200)  
--declare @pNm_Usuario  VARCHAR(50)   
--declare @pSenha    VARCHAR(50)  
--declare @pNova_Senha  VARCHAR(50)   
--declare @pEmail_Corporativo VARCHAR(50)  
--declare @pId_Facebook  VARCHAR(50)   
--declare @pChave_Validacao VARCHAR(50)   
--declare @pFl_Automatico  VARCHAR(50)   
  
-- set @pPakage=N'Sd_Viculacao_Email_App'  
-- set @pNm_Usuario=NULL  
-- set @pSenha=N'11986897381'  
-- set @pNova_Senha=NULL  
-- set @pEmail_Corporativo=N'alexandre.teixeira@sodexo.com.us'  
-- set @pId_Facebook=NULL  
-- set @pChave_Validacao=NULL  
-- set @pFl_Automatico=NULL  
   
             
DECLARE @v_SQL AS VARCHAR(MAX)  
DECLARE @v_Nm_Usuario AS VARCHAR(50)  
DECLARE @v_Senha AS VARCHAR(50)  
DECLARE @v_Empresa AS VARCHAR(50)  
DECLARE @v_Email AS VARCHAR(100)
DECLARE @v_Token AS VARCHAR(250)
DECLARE @v_Id_Usuario AS VARCHAR(50)
DECLARE @v_Id_Ativo AS VARCHAR(50)
DECLARE @vNm_Consumidor varchar(100)
DECLARE @vDt_Hr_Ativacao Datetime
DECLARE @vDt_Hr_Desativacao Datetime
DECLARE @vTermo_Ativacao varchar(8000)
DECLARE @vId_Ativo varchar(100)
DECLARE @vId_Consumidor varchar(100)
DECLARE @vNm_Ativo_Tipo_Grupo varchar(100)
DECLARE @vEmail varchar(100)
DECLARE @vNm_Usuario varchar(100)


-----K2AICONTROLIT-24 - Enviar mensagem de termo de responsabilidade

IF @pPAKAGE = 'Sd_TR_ValidarTokenTermoResponsabilidade'  
BEGIN 

	SELECT @v_Id_Usuario = Id_Usuario, 
           @v_Empresa = Empresa,
	 	   @v_Id_Ativo = Id_Ativo
	 FROM  Usuario_Envio_TermoResponsabilidade
	 WHERE Token_Validade >= GETDATE()
	   AND Token =  @pChave_Validacao
	   AND Token_Confirmacao IS NULL
    SELECT ISNULL(@v_Id_Usuario, '') AS Id_Usuario, ISNULL(@v_Id_Ativo, '') AS Id_Ativo, ISNULL(@v_Empresa, '') AS Empresa
    

END

----- Método que retorna os delhates do hold para elaborar o termo
IF @pPAKAGE = 'Sd_TR_RetornarHoldTermoResponsabilidade'
BEGIN
    SELECT @v_Id_Usuario = Id_Usuario, 
				   @v_Empresa = Empresa,
	 			   @v_Id_Ativo = Id_Ativo
			 FROM  Usuario_Envio_TermoResponsabilidade
			 WHERE Token_Validade >= GETDATE()
			   AND Token =  @pChave_Validacao
			   AND Token_Confirmacao IS NULL
    IF(@v_Id_Usuario IS NOT NULL AND @v_Empresa IS NOT NULL)
	BEGIN   
		SELECT Id_Consumidor,Nm_Consumidor,Nm_Usuario,Matricula,Endereco,Nm_Filial,
		       Centro_Custo,Departamento,Linha,Tipo_Ativo,Nm_Conglomerado,Nm_Modelo_Fabricante,
			   IMEI,ACESSORIO,Carregador,Cabo_USB,Fone,Pelicula,Capa_Protecao,Nr_Aparelho,NF, CHIP,
			   Logo, Termo_Ativacao,  Empresa, 
			  (SELECT CASE WHEN DATEPART(DW,GETDATE()) = 1 THEN 'Domingo'
							WHEN DATEPART(DW,GETDATE()) = 2 THEN 'Segunda'
							WHEN DATEPART(DW,GETDATE()) = 3 THEN 'Terça'
							WHEN DATEPART(DW,GETDATE()) = 4 THEN 'Quarta'
							WHEN DATEPART(DW,GETDATE()) = 5 THEN 'Quinta'
							WHEN DATEPART(DW,GETDATE()) = 6 THEN 'Sexta'
							WHEN DATEPART(DW,GETDATE()) = 7 THEN 'Sábado' END) AS Dia_Semana,
				(SELECT CASE	WHEN MONTH(GETDATE()) = 1 THEN 'Janeiro - '
							WHEN MONTH(GETDATE()) = 2 THEN 'Fevereiro - '
							WHEN MONTH(GETDATE()) = 3 THEN 'Março - '
							WHEN MONTH(GETDATE()) = 4 THEN 'Abril - '
							WHEN MONTH(GETDATE()) = 5 THEN 'Maio - '
							WHEN MONTH(GETDATE()) = 6 THEN 'Junho - '
							WHEN MONTH(GETDATE()) = 7 THEN 'Julho - '
							WHEN MONTH(GETDATE()) = 8 THEN 'Agosto - '
							WHEN MONTH(GETDATE()) = 9 THEN 'Setembro - '
							WHEN MONTH(GETDATE()) = 10 THEN 'Outubro - '
							WHEN MONTH(GETDATE()) = 11 THEN 'Novembro - '
							WHEN MONTH(GETDATE()) = 12 THEN 'Dezembro - ' END + CONVERT(VARCHAR,DAY(GETDATE()))) AS Data
		FROM Usuario_Envio_TermoResponsabilidade
		WHERE Token_Validade >= GETDATE()
			   AND Token =  @pChave_Validacao
			   AND Token_Confirmacao IS NULL
         --se o token for valido então registrar a data de leitura do termo

		 UPDATE  Usuario_Envio_TermoResponsabilidade
		   SET Token_Confirmacao = GETDATE()
		 WHERE Token_Validade >= GETDATE()
		   AND Token =  @pChave_Validacao
		   AND Token_Confirmacao IS NULL
    END
    ELSE BEGIN
	   SELECT '' AS Id_Consumidor,'' AS Nm_Consumidor,'' AS Nm_Usuario,'' AS Matricula,'' AS Endereco,
	           '' AS Nm_Filial, '' AS Centro_Custo,'' AS Departamento,'' AS Linha,'' AS Tipo_Ativo,'' AS Nm_Conglomerado,
			   '' AS Nm_Modelo_Fabricante, '' AS IMEI, '' AS ACESSORIO,0 AS Carregador,0 AS Cabo_USB,0 AS Fone,
			    0 AS Pelicula, 0 AS Capa_Protecao, '' AS Nr_Aparelho, '' AS NF,	'' AS CHIP,   
	          '~/Img_Sistema/Logo/logo_k2a.png' AS Termo_Ativacao, 'K2AIcontrolIT' AS Logo, 'K2AIcontrolIT' AS Empresa, 
			  (SELECT CASE WHEN DATEPART(DW,GETDATE()) = 1 THEN 'Domingo'
							WHEN DATEPART(DW,GETDATE()) = 2 THEN 'Segunda'
							WHEN DATEPART(DW,GETDATE()) = 3 THEN 'Terça'
							WHEN DATEPART(DW,GETDATE()) = 4 THEN 'Quarta'
							WHEN DATEPART(DW,GETDATE()) = 5 THEN 'Quinta'
							WHEN DATEPART(DW,GETDATE()) = 6 THEN 'Sexta'
							WHEN DATEPART(DW,GETDATE()) = 7 THEN 'Sábado' END) AS Dia_Semana,
				(SELECT CASE	WHEN MONTH(GETDATE()) = 1 THEN 'Janeiro - '
							WHEN MONTH(GETDATE()) = 2 THEN 'Fevereiro - '
							WHEN MONTH(GETDATE()) = 3 THEN 'Março - '
							WHEN MONTH(GETDATE()) = 4 THEN 'Abril - '
							WHEN MONTH(GETDATE()) = 5 THEN 'Maio - '
							WHEN MONTH(GETDATE()) = 6 THEN 'Junho - '
							WHEN MONTH(GETDATE()) = 7 THEN 'Julho - '
							WHEN MONTH(GETDATE()) = 8 THEN 'Agosto - '
							WHEN MONTH(GETDATE()) = 9 THEN 'Setembro - '
							WHEN MONTH(GETDATE()) = 10 THEN 'Outubro - '
							WHEN MONTH(GETDATE()) = 11 THEN 'Novembro - '
							WHEN MONTH(GETDATE()) = 12 THEN 'Dezembro - ' END + CONVERT(VARCHAR,DAY(GETDATE()))) AS Data
	END
END

----- Método que retorna os detalhes para preenchimento do termo
--IF @pPAKAGE = 'Sd_TR_RetornarDetalhesAtivoTermoResponsabilidade'  
--BEGIN 
   
   
--	SELECT @v_Id_Usuario = Id_Usuario, 
--           @v_Empresa = Empresa,
--	 	   @v_Id_Ativo = Id_Ativo
--	 FROM  Usuario_Envio_TermoResponsabilidade
--	 WHERE Token_Validade >= GETDATE()
--	   AND Token =  @pChave_Validacao
--	   AND Token_Confirmacao IS NULL

--    --se o token for valido então registrar a data de leitura do termo

--	 UPDATE  Usuario_Envio_TermoResponsabilidade
--	   SET Token_Confirmacao = GETDATE()
--	 WHERE Token_Validade >= GETDATE()
--	   AND Token =  @pChave_Validacao
--	   AND Token_Confirmacao IS NULL

    
--	IF(@v_Empresa IS NULL AND @v_Id_Ativo IS NULL AND @v_Id_Usuario IS NULL)
--	BEGIN
--		SET @v_Id_Ativo = 0
--		SELECT top 1 @v_Id_Usuario = Id_Usuario, 
--				@v_Empresa = Empresa
--			FROM  Usuario_Envio_TermoResponsabilidade
--	END

--    SET @v_SQL = '
--		SELECT DISTINCT
--			ISNULL(CON.Id_Consumidor,'''') AS Id_Consumidor,
--			ISNULL(CON.Nm_Consumidor,'''') AS Nm_Consumidor,
--			ISNULL(US.Nm_Usuario,'''') AS Nm_Usuario,
--			ISNULL(CON.Matricula,'''') AS Matricula,
--			ISNULL(FI.Endereco,'''') AS Endereco,
--			ISNULL(FI.Nm_Filial,'''') AS Nm_Filial,
--			ISNULL(CC.Cd_Centro_Custo,'''') AS Centro_Custo,
--			ISNULL(DP.Nm_Departamento,'''') AS Departamento,
--			ISNULL(A.Nr_Ativo,'''') AS Linha,
--			ISNULL(AT.Nm_Ativo_Tipo,'''') AS Tipo_Ativo,
--			ISNULL(CG.Nm_Conglomerado,'''') AS Nm_Conglomerado,
--			ISNULL(AMF.Descricao,'''') AS Nm_Modelo_Fabricante,';

--		-- Adicionando a parte dinâmica para IMEI
--		SET @v_SQL = @v_SQL + '
--			ISNULL((SELECT ''C'' + CONVERT(VARCHAR(2), REG)
--					FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM ' + @v_Empresa + '..Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM ' + @v_Empresa + '..Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB
--					WHERE NM_ATIVO_COMPLEMENTO = ''IMEI''), '''') AS IMEI,';

--		-- Adicionando a parte dinâmica para ACESSORIO
--		SET @v_SQL = @v_SQL + '
--			ISNULL((SELECT ''C'' + CONVERT(VARCHAR(2), REG)
--					FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM ' + @v_Empresa + '..Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM ' + @v_Empresa + '..Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB
--					WHERE NM_ATIVO_COMPLEMENTO = ''ACESSORIO''), '''') AS ACESSORIO,';

--		SET @v_SQL = @v_SQL + '
--			ISNULL(EA.Ck_Carregador,'''') AS Carregador,
--			ISNULL(EA.Ck_Cabousb,'''') AS [Cabo USB],
--			ISNULL(EA.Ck_Fone,'''') AS Fone,
--			ISNULL(EA.Ck_Pelicula,'''') AS Pelicula,
--			ISNULL(EA.Ck_Capaprotecao,'''') AS [Capa Proteção],
--			ISNULL(ISNULL((SELECT TOP 1 Nr_Aparelho
--							FROM ' + @v_Empresa + '..Estoque_Aparelho
--							WHERE Id_Aparelho = EAA.Id_Aparelho AND Id_Aparelho_Tipo = 1), (SELECT ''C'' + CONVERT(VARCHAR(2), REG)
--																								FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM ' + @v_Empresa + '..Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM ' + @v_Empresa + '..Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB
--																								WHERE NM_ATIVO_COMPLEMENTO = ''NS'')), '''') AS Nr_Aparelho,
--			ISNULL(ISNULL((SELECT TOP 1 SENF.Nr_Nota_Fiscal
--							FROM ' + @v_Empresa + '..Estoque_Aparelho SEA INNER JOIN ' + @v_Empresa + '..Estoque_Nota_Fiscal SENF ON SENF.Id_Estoque_Nota_Fiscal = SEA.Id_Estoque_Nota_Fiscal
--							WHERE Id_Aparelho = EAA.Id_Aparelho
--							AND SEA.Id_Aparelho_Tipo = 1), (SELECT ''C'' + CONVERT(VARCHAR(2), REG)
--															FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM ' + @v_Empresa + '..Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM ' + @v_Empresa + '..Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB
--															WHERE NM_ATIVO_COMPLEMENTO = ''NF'')), '''') AS NF
--		FROM Fortlev..Consumidor CON
--			INNER JOIN  ' + @v_Empresa + '..Rl_Consumidor_Ativo RCA ON CON.Id_Consumidor = RCA.Id_Consumidor
--			INNER JOIN  ' + @v_Empresa + '..Filial FI ON FI.Id_Filial = CON.Id_Filial
--			INNER JOIN  ' + @v_Empresa + '..Centro_Custo CC ON CC.Id_Centro_Custo = CON.Id_Centro_Custo
--			INNER JOIN  ' + @v_Empresa + '..Ativo A ON A.Id_Ativo = RCA.Id_Ativo
--			INNER JOIN  ' + @v_Empresa + '..Ativo_Tipo AT ON A.Id_Ativo_Tipo = AT.Id_Ativo_Tipo
--			INNER JOIN  ' + @v_Empresa + '..Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
--			INNER JOIN  ' + @v_Empresa + '..Usuario US ON US.Id_Consumidor = CON.Id_Consumidor
--			LEFT JOIN  ' + @v_Empresa + '..Departamento DP ON DP.Id_Departamento = CON.Id_Departamento
--			LEFT JOIN  ' + @v_Empresa + '..vw_Ativo_Modelo_Fabricante AMF ON AMF.Id_Ativo_Modelo = A.Id_Ativo_Modelo
--			LEFT JOIN  ' + @v_Empresa + '..Rl_Estoque_Aparelho_Ativo EAA ON EAA.id_Ativo = A.Id_Ativo
--			LEFT JOIN  ' + @v_Empresa + '..Estoque_Aparelho EA ON EAA.Id_Aparelho = EA.Id_Aparelho
--		WHERE RCA.Id_Ativo = @pId_Ativo
--		ORDER BY Nr_Aparelho DESC';

--		-- Executando a consulta dinâmica
--		EXEC sp_executesql @v_SQL, N'@pId_Ativo INT', @v_Id_Ativo;
--END

----- Método que retorna os detalhes para abertura do Modal e exibir o termo
IF @pPAKAGE = 'Sd_TR_MontarTokenTermoResponsabilidade'  
BEGIN 

    --CREATE TABLE #Temp_termoglobal (Termo Nvarchar(100), Id_Ativo int, Id_Consumidor int, Nm_Ativo_Tipo_Grupo Nvarchar(100)) 
   
    SELECT @v_Id_Usuario = Id_Usuario, 
           @v_Empresa = Empresa,
	 	   @v_Id_Ativo = Id_Ativo
	 FROM  Ativvus_Login..Usuario_Envio_TermoResponsabilidade
	 WHERE Token_Validade >= GETDATE()
	   AND Token =  @pChave_Validacao;

		SELECT 
			ISNULL(Termo_Ativacao, '') AS Termo_Ativacao,
			CONVERT(VARCHAR, Id_Ativo) AS Id_Ativo,
			CONVERT(VARCHAR, Id_Consumidor) AS Id_Consumidor,
			CONVERT(VARCHAR, Nm_Ativo_Tipo_Grupo) AS Nm_Ativo_Tipo_Grupo
		FROM Ativvus_Login..Usuario_Envio_TermoResponsabilidade
	 WHERE Token_Validade >= GETDATE()
	   AND Token =  @pChave_Validacao;

   -- SET @v_SQL = '
			--SELECT
			--   ISNULL(ATG.Termo_Ativacao, '''') Termo_Ativacao,
			--   CONVERT(VARCHAR, RCA.Id_Ativo) Id_Ativo,
			--   CONVERT(VARCHAR, RCA.Id_Consumidor) Id_Consumidor,
			--   CONVERT(VARCHAR, ATG.Nm_Ativo_Tipo_Grupo) Nm_Ativo_Tipo_Grupo
			-- FROM '+ @v_Empresa +'..vw_Consumidor CON 
			--		INNER JOIN '+ @v_Empresa +'..Rl_Consumidor_Ativo RCA ON CON.Id_Consumidor = RCA.Id_Consumidor      
			--		INNER JOIN '+ @v_Empresa +'..Ativo A ON A.Id_Ativo = RCA.Id_Ativo      
			--		INNER JOIN '+ @v_Empresa +'..Ativo_Tipo AT ON A.Id_Ativo_Tipo = AT.Id_Ativo_Tipo       
			--		INNER JOIN '+ @v_Empresa +'..Ativo_Tipo_Grupo ATG ON AT.Id_Ativo_Tipo_Grupo  = ATG.Id_Ativo_Tipo_Grupo       
			--		LEFT JOIN '+ @v_Empresa +'..vw_Usuario US ON US.Id_Consumidor = CON.Id_Consumidor      
			--		LEFT JOIN '+ @v_Empresa +'..Rl_Perfil_Ativo_Usuario RUA ON US.Id_Usuario = RUA.Id_Usuario AND RUA.Id_Ativo = RCA.Id_Ativo
			-- WHERE RCA.Id_Ativo = @pId_Ativo
			-- ORDER BY Nm_Consumidor'
   --   INSERT INTO  #Temp_termoglobal(Termo, Id_Ativo, Id_Consumidor, Nm_Ativo_Tipo_Grupo)
   --   EXEC sp_executesql @v_SQL,N'@pId_Ativo VARCHAR(10)' , @v_Id_Ativo

	  --SELECT Termo, Id_Ativo, Id_Consumidor, Nm_Ativo_Tipo_Grupo FROM #Temp_termoglobal

	  -- DROP TABLE #Temp_termoglobal

	   /*
	SET @v_SQL = '
			SELECT       
			   @vNm_Consumidor = ISNULL(CON.Nm_Consumidor, ''''),      
			   @vDt_Hr_Ativacao = Dt_Hr_Ativacao,
			   @vDt_Hr_Desativacao = Dt_Hr_Desativacao,
			   @vTermo_Ativacao = ISNULL(ATG.Termo_Ativacao, ''''),
			   @vId_Ativo = CONVERT(VARCHAR, RCA.Id_Ativo),
			   @vId_Consumidor = CONVERT(VARCHAR, RCA.Id_Consumidor),
			   @vNm_Ativo_Tipo_Grupo = CONVERT(VARCHAR, ATG.Nm_Ativo_Tipo_Grupo),
			   @vEmail = CON.Email,
			   @vNm_Usuario = US.Nm_Usuario
			 FROM '+ @v_Empresa +'..vw_Consumidor CON 
					INNER JOIN '+ @v_Empresa +'..Rl_Consumidor_Ativo RCA ON CON.Id_Consumidor = RCA.Id_Consumidor      
					INNER JOIN '+ @v_Empresa +'..Ativo A ON A.Id_Ativo = RCA.Id_Ativo      
					INNER JOIN '+ @v_Empresa +'..Ativo_Tipo AT ON A.Id_Ativo_Tipo = AT.Id_Ativo_Tipo       
					INNER JOIN '+ @v_Empresa +'..Ativo_Tipo_Grupo ATG ON AT.Id_Ativo_Tipo_Grupo  = ATG.Id_Ativo_Tipo_Grupo       
					LEFT JOIN '+ @v_Empresa +'..vw_Usuario US ON US.Id_Consumidor = CON.Id_Consumidor      
					LEFT JOIN '+ @v_Empresa +'..Rl_Perfil_Ativo_Usuario RUA ON US.Id_Usuario = RUA.Id_Usuario AND RUA.Id_Ativo = RCA.Id_Ativo
			 WHERE RCA.Id_Ativo = @pId_Ativo
			 ORDER BY Nm_Consumidor'*/

			/*EXEC  @v_SQL --,
							--   N'@pId_Ativo NVARCHAR(10)' --, @vNm_Consumidor NVARCHAR(100) OUTPUT, @vDt_Hr_Ativacao NVARCHAR(100) OUTPUT, @vDt_Hr_Desativacao NVARCHAR(100) OUTPUT, @vTermo_Ativacao NVARCHAR(8000) OUTPUT, @vId_Ativo VARCHAR(100) OUTPUT, @vId_Consumidor NVARCHAR(100) OUTPUT, @vNm_Ativo_Tipo_Grupo NVARCHAR(100) OUTPUT, @vEmail NVARCHAR(100) OUTPUT, @vNm_Usuario NVARCHAR(100) OUTPUT',
							  , @v_Id_Ativo ,
							   @vNm_Consumidor OUTPUT,
							   @vDt_Hr_Ativacao OUTPUT,
							   @vDt_Hr_Desativacao OUTPUT,
							   @vTermo_Ativacao OUTPUT,
							   @vId_Ativo OUTPUT,
							   @vId_Consumidor OUTPUT,
							   @vNm_Ativo_Tipo_Grupo OUTPUT,
							   @vEmail OUTPUT,
							   @vNm_Usuario OUTPUT*/
							   /*
			SELECT @vNm_Consumidor AS Nm_Consumidor,
				   @vDt_Hr_Ativacao AS Dt_Hr_Ativacao,
				   @vDt_Hr_Desativacao AS Dt_Hr_Desativacao,
				   ISNULL(@vTermo_Ativacao, '') AS Termo_Ativacao,
				   @vId_Ativo AS Id_Ativo,
				   @vId_Consumidor AS Id_Consumidor,
				   @vNm_Ativo_Tipo_Grupo AS Nm_Ativo_Tipo_Grupo,
				   @vEmail AS Email,
				   @vNm_Usuario AS Nm_Usuario;*/
	   
END

----- ./ K2AICONTROLIT-24 - Enviar mensagem de termo de responsabilidade

-----K2AICONTROLIT-116
-------Procedimento para confirmação do cadastro de email do usuario para enviar token de mudança de senha
IF @pPAKAGE = 'Sd_Valida_Usuario_Com_Email'  
BEGIN 
   SELECT Nm_Usuario,  Empresa, Email	   
	 FROM Usuario_Global   
	 WHERE Email = @pEmail_Corporativo  
	   AND Nm_Usuario = @pNm_Usuario
END

-------Procedimento para confirmação do cadastro de email do usuario para validar se token está ativo
IF @pPAKAGE = 'Sd_Valida_Usuario_Com_Token_Valido'  
BEGIN 
   SELECT Nm_Usuario,  Empresa, Email,
     CASE WHEN [Token_Validade] >= GETDATE() THEN 1 ELSE 0 END AS Token_Validade
	 FROM Usuario_Global   
	 WHERE Email = @pEmail_Corporativo  
	   AND Nm_Usuario = @pNm_Usuario
END

-------Procedimento para confirmação do cadastro se token está ativo
IF @pPAKAGE = 'Sd_Valida_Token_Valido'  
BEGIN 


   SELECT @v_Nm_Usuario = Nm_Usuario, 
          @v_Empresa = Empresa,
		  @v_Email = Email    
	 FROM Usuario_Global   
	 WHERE Token_Validade >= GETDATE()
	   AND Token =  @pChave_Validacao
    SELECT ISNULL(@v_Nm_Usuario, '') AS Nm_Usuario, ISNULL(@v_Email, '') AS Email, ISNULL(@v_Empresa, '') AS Empresa
END

-------Procedimento para gravar no cadastro de email do usuario o token e deixar ativo nos proximos 30 minutos
IF @pPAKAGE = 'Sd_Valida_Usuario_Grava_Token'  
BEGIN 
  UPDATE Usuario_Global   
     SET Token = @pChave_Validacao,
	     Token_Validade = DATEADD(MINUTE, 30, GETDATE())
	 WHERE Email      = @pEmail_Corporativo  
	   AND Nm_Usuario = @pNm_Usuario

   SELECT Nm_Usuario,  Empresa, Email,
     CASE WHEN [Token_Validade] >= GETDATE() THEN 1 ELSE 0 END AS Token_Validade
	 FROM Usuario_Global   
	 WHERE Email = @pEmail_Corporativo  
	   AND Nm_Usuario = @pNm_Usuario

END

-------Procedimento para gravar a solicitação de token na caixa de saida de email do usuario
IF @pPAKAGE = 'Sd_Valida_Usuario_Grava_CaixaSaidaEmail'   
BEGIN  
 SELECT @v_Empresa = '', @v_Token = '', @v_Nm_Usuario = ''  
  
 SELECT @v_Nm_Usuario = Nm_Usuario,  
   @v_Empresa = Empresa,  
   @v_Token = Token  
 FROM Usuario_Global   
 WHERE Email = @pEmail_Corporativo   
   
 IF NOT EXISTS(SELECT EMail FROM Usuario_Global WHERE EMail = @pEmail_Corporativo GROUP BY EMail HAVING COUNT(*) > 1)  
 BEGIN  
  IF NOT @v_Empresa = '' AND NOT @v_Nm_Usuario = '' AND NOT @v_Token = ''  
  BEGIN  
   SET @v_SQL = 'INSERT INTO ' + @v_Empresa + '..Mail_Caixa_Saida ' +  
       'VALUES (8, ' + '''' + @pEmail_Corporativo + '''' + ', NULL,' +   
       '''' + 'Usuário:' + @v_Nm_Usuario + ' - Clique no link: <a href="https://icontrolit.homologa.bitgrow.net?hashts=' + @v_Token + '" target="_blank">Trocar Senha</a>''' + ', GETDATE(), NULL, 0, 0, 0)'  
   EXEC(@v_SQL)  
   SELECT 'Sua senha foi encaminhada por e-mail' AS Texto_Email  
  END  
  ELSE  
  BEGIN  
   SELECT 'Seu e-mail não foi identificado em nosso cadastro, favor entrar em contato com a área de TI.' AS Texto_Email  
  END  
 END  
 ELSE  
 BEGIN  
  SELECT 'Seu e-mail está duplicado, favor entrar em contato com a área de TI.' AS Texto_Email  
 END  
END  

 ----- Procedimento que realiza a alteração da senha depois de validado pelo token
 IF @pPAKAGE = 'Sd_Valida_Usuario_Token_Troca_Senha'
 BEGIN

     SELECT @v_Empresa = '', @v_Senha = '', @v_Nm_Usuario = ''  
  
	 SELECT @v_Nm_Usuario = Nm_Usuario,  
	   @v_Empresa = Empresa,  
	   @v_Senha = senha  
	 FROM Usuario_Global   
	 WHERE Email = @pEmail_Corporativo 
    
     SET @v_SQL = 'UPDATE Usuario_Global ' +  
       'SET Senha = ' + '''' + @pNova_Senha + '''' +  
       ' , Token_Validade = GETDATE() ' +
       ' WHERE Nm_Usuario = ' + '''' + @pNm_Usuario + ''''  
         + ' AND Senha = ' + '''' + @v_Senha + ''''  
    EXEC(@v_SQL) 

	--select @v_Nm_Usuario, @v_Empresa, @pNova_Senha, @v_Senha

      SET @v_SQL = 'UPDATE ' + @v_Empresa + '..Usuario ' +  
        'SET Senha = ' + '''' + @pNova_Senha + '''' +
        ' WHERE Nm_Usuario = ' + '''' + @pNm_Usuario + ''''  
          + ' AND Senha = ' + '''' + @v_Senha + ''''  
    EXEC(@v_SQL)  

	SELECT 'Senha alterada com sucesso.' AS Texto_Senha

 END

----- ./K2AICONTROLIT-116
  
-----valida dados do usuario do facebook  
IF @pPAKAGE = 'Sd_Valida_Usuario_Facebook'  
BEGIN  
 SELECT @v_Empresa = '', @v_Nm_Usuario = ''  
  
 SELECT @v_Nm_Usuario = Nm_Usuario,  
   @v_Empresa = Empresa   
 FROM Usuario_Global   
 WHERE Id_Facebook = @pId_Facebook  
   
 IF NOT @v_Empresa = '' AND NOT @v_Nm_Usuario = ''  
 BEGIN  
  SET @v_SQL = 'SELECT USU.Id_Usuario,  
        CON.Nm_Consumidor,  
        USU.Nm_Usuario,  
        USU.Senha,  
        CON.Matricula,  
        USU.Id_Usuario_Perfil_Acesso,  
        USU.Id_Idioma,  
        USU.Fl_Desativado, ' +   
        '''' + @v_Empresa + '''' + ' AS Empresa ' +  
      'FROM ' + @v_Empresa + '..vw_Usuario USU INNER JOIN ' + @v_Empresa + '..vw_Consumidor CON ON (USU.Id_Consumidor = CON.Id_Consumidor) ' +  
      'WHERE USU.Nm_Usuario = ' + '''' + @v_Nm_Usuario + ''''  
  EXEC(@v_SQL)  
 END  
 ELSE  
 BEGIN  
  -----retona 0 para validacao  
  SELECT TOP 0 * FROM Usuario_Global  
 END   
END  
  
-----vincula dados do usuario para o facebook  
IF @pPAKAGE = 'Sd_Viculacao_Email_Facebook'  
BEGIN  
 DECLARE @v_Validacao AS TABLE(ID INT)  
 DECLARE @v_Chave_Validacao AS VARCHAR(100)  
 SELECT @v_Empresa = '', @v_Nm_Usuario = ''  
  
 SELECT @v_Nm_Usuario = Nm_Usuario,  
   @v_Empresa = Empresa   
 FROM Usuario_Global   
 WHERE Email = @pEmail_Corporativo  
   
 IF NOT EXISTS(SELECT EMail FROM Usuario_Global WHERE EMail = @pEmail_Corporativo GROUP BY EMail HAVING COUNT(*) > 1)  
 BEGIN  
  IF NOT @v_Empresa = '' AND NOT @v_Nm_Usuario = ''  
  BEGIN  
   SET @v_SQL = 'SELECT USU.Id_Usuario ' +  
       'FROM ' + @v_Empresa + '..vw_Usuario USU INNER JOIN ' + @v_Empresa + '..vw_Consumidor CON ON (USU.Id_Consumidor = CON.Id_Consumidor) ' +  
       'WHERE USU.Nm_Usuario = ' + '''' + @v_Nm_Usuario + '''' +  
         ' AND CON.EMail = ' + '''' + @pEmail_Corporativo + ''''  
    
   INSERT INTO @v_Validacao  
   EXEC(@v_SQL)  
     
   IF (SELECT ID FROM @v_Validacao) > 0  
   BEGIN  
    SELECT @v_Chave_Validacao = @v_Empresa + REPLACE(LEFT(RAND((DATEPART(mm, GETDATE()) * 100000 ) + (DATEPART(ss, GETDATE()) * 1000 ) + DATEPART(ms, GETDATE())),6), '0.', 'AT')  
    UPDATE Usuario_Global   
    SET  Chave_Validacao = @v_Chave_Validacao,  
      Fl_Validacao = 2,  
      Id_Facebook = NULL  
    WHERE Email = @pEmail_Corporativo   
      AND Nm_Usuario = @v_Nm_Usuario AND Empresa = @v_Empresa  
     
    SET @v_SQL = 'INSERT INTO ' + @v_Empresa + '..Mail_Caixa_Saida ' +  
        'VALUES (7, ' + '''' + @pEmail_Corporativo + '''' + ', NULL,' +   
        '''' + @v_Chave_Validacao + '''' + ', GETDATE(), NULL, 0, 0, 0)'  
    EXEC(@v_SQL)  
         
    SELECT 'Seu ID foi encaminhado por e-mail.' AS Texto_Email  
   END  
   ELSE  
   BEGIN  
    SELECT 'Seu e-mail não foi identificado em nosso cadastro, favor entrar em contato com a área de TI.' AS Texto_Email  
   END  
   END  
  ELSE  
  BEGIN  
   SELECT 'Seu e-mail não foi identificado em nosso cadastro, favor entrar em contato com a área de TI.' AS Texto_Email  
  END  
 END  
 ELSE  
 BEGIN  
  SELECT 'Seu e-mail está duplicado, favor entrar em contato com a área de TI.' AS Texto_Email  
 END  
END  
  
-----grava chave de id para validacao do facebook  
IF @pPAKAGE = 'Sd_Valida_Chave_Id_Facebook'  
BEGIN  
 SELECT @v_Empresa = '', @v_Nm_Usuario = ''  
  
 SELECT @v_Nm_Usuario = Nm_Usuario,  
   @v_Empresa = Empresa   
 FROM Usuario_Global   
 WHERE Chave_Validacao = @pChave_Validacao   
  
 IF NOT @v_Empresa = '' AND NOT @v_Nm_Usuario = ''  
 BEGIN  
  UPDATE Usuario_Global   
  SET  Fl_Validacao = 1,  
    Id_Facebook = @pId_Facebook  
  WHERE Chave_Validacao = @pChave_Validacao  
  SELECT 'Validação OK.' AS Texto_Validacao  
 END  
 ELSE  
 BEGIN  
  SELECT 'Id Inválido.' AS Texto_Validacao  
 END  
END  
  
-------valida dados do usuario  
IF @pPAKAGE = 'Sd_Valida_Usuario'  
BEGIN  
 SELECT @v_Empresa = ''  
   
 SELECT @v_Empresa = Empresa   
 FROM Usuario_Global   
 WHERE Nm_Usuario = @pNm_Usuario  
   AND Senha = @pSenha  
  
 IF NOT @v_Empresa = ''  
 BEGIN  
  -----valida usuario e não troca senha troca senha  
  IF  @pNova_Senha IS NULL  
  BEGIN  
   SET @v_SQL = 'SELECT USU.Id_Usuario,  
         CON.Nm_Consumidor,  
         USU.Nm_Usuario,  
         USU.Senha,  
         CON.Matricula,  
         USU.Id_Usuario_Perfil_Acesso,  
         USU.Id_Idioma,  
         USU.Fl_Desativado, ' +  
         '''' + @v_Empresa + '''' + ' AS Empresa ' +  
       'FROM ' + @v_Empresa + '..vw_Usuario USU INNER JOIN ' + @v_Empresa + '..vw_Consumidor CON ON (USU.Id_Consumidor = CON.Id_Consumidor) ' +  
       'WHERE USU.Nm_Usuario = ' + '''' + @pNm_Usuario + ''''  
         + 'AND USU.Senha = ' + '''' + @pSenha + ''''  
   EXEC(@v_SQL)  
  END  
  
  -----troca senha  
  IF NOT @pNova_Senha IS NULL  
  BEGIN  
   IF EXISTS(SELECT * FROM Usuario_Global WHERE Nm_Usuario = @pNm_Usuario AND Senha = @pNova_Senha)  
   BEGIN  
    SELECT 'Senha inválida.' AS Nm_Usuario  
   END   
   ELSE  
   BEGIN  
    SET @v_SQL = 'SELECT USU.Id_Usuario,  
          CON.Nm_Consumidor,  
          USU.Nm_Usuario,  
          USU.Senha,  
          CON.Matricula,  
          USU.Id_Usuario_Perfil_Acesso,  
          USU.Id_Idioma,  
          USU.Fl_Desativado, ' +  
          '''' + @v_Empresa + '''' + ' AS Empresa ' +  
        'FROM ' + @v_Empresa + '..vw_Usuario USU INNER JOIN ' + @v_Empresa + '..vw_Consumidor CON ON (USU.Id_Consumidor = CON.Id_Consumidor) ' +  
        'WHERE USU.Nm_Usuario = ' + '''' + @pNm_Usuario + ''''  
          + 'AND USU.Senha = ' + '''' + @pSenha + ''''  
    EXEC(@v_SQL)  
  
    SET @v_SQL = 'UPDATE ' + @v_Empresa + '..Usuario ' +  
        'SET Senha = ' + '''' + @pNova_Senha + '''' +  
        ' WHERE Nm_Usuario = ' + '''' + @pNm_Usuario + ''''  
          + ' AND Senha = ' + '''' + @pSenha + ''''  
    EXEC(@v_SQL)  
   END  
  END   
 END  
 ELSE  
 BEGIN  
  -----retona 0 para validacao  
  SELECT TOP 0 * FROM Usuario_Global  
 END   
END   
  
-------valida dados do usuario redirecionado  
IF @pPAKAGE = 'Sd_Valida_Usuario_Redirecionado'  
BEGIN  
 SELECT @v_Empresa = ''  
 SELECT @v_Empresa = Empresa   
 FROM Usuario_Global   
 WHERE Nm_Usuario = @pNm_Usuario  
   AND Senha = @pSenha  
  
 IF NOT @v_Empresa = ''  
 BEGIN  
  -----valida usuario e não troca senha troca senha  
  IF  @pNova_Senha IS NULL  
  BEGIN  
   SET @v_SQL = 'SELECT USU.Id_Usuario,  
         CON.Nm_Consumidor,  
         USU.Nm_Usuario,  
         USU.Senha,  
         CON.Matricula,  
         USU.Id_Usuario_Perfil_Acesso,  
         USU.Id_Idioma,  
         USU.Fl_Desativado, ' +  
         '''' + @v_Empresa + '''' + ' AS Empresa ' +  
       'FROM ' + @v_Empresa + '..vw_Usuario USU INNER JOIN ' + @v_Empresa + '..vw_Consumidor CON ON (USU.Id_Consumidor = CON.Id_Consumidor) ' +  
       'WHERE USU.Nm_Usuario = ' + '''' + @pNm_Usuario + ''''  
         + 'AND USU.Senha = ' + '''' + @pSenha + ''''  
   EXEC(@v_SQL)  
  END  
 END  
 ELSE  
 BEGIN  
  -----retona 0 para validacao  
  SELECT TOP 0 * FROM Usuario_Global  
 END   END   
  
-------valida dados do usuario pelo AD  
IF @pPAKAGE = 'Sd_Valida_Usuario_AD_Local'  
BEGIN  
 SELECT @v_Empresa = ''  
 SELECT @v_Empresa = Empresa   
 FROM Usuario_Global   
 WHERE Nm_Usuario = @pNm_Usuario  
   AND Empresa = @pSenha  
  
 IF NOT @v_Empresa = ''  
 BEGIN  
  SET @v_SQL = 'SELECT USU.Id_Usuario,  
        CON.Nm_Consumidor,  
        USU.Nm_Usuario,  
        USU.Senha,  
        CON.Matricula,  
        USU.Id_Usuario_Perfil_Acesso,  
        USU.Id_Idioma,  
        USU.Fl_Desativado, ' +  
        '''' + @v_Empresa + '''' + ' AS Empresa, ' +  
        'dbo.Fu_Criptografa(Nm_Usuario +' + '''' + ';' + '''' + '+ USU.Senha) AS Usuario_Autenticado ' +  
      'FROM ' + @v_Empresa + '..vw_Usuario USU INNER JOIN ' + @v_Empresa + '..vw_Consumidor CON ON (USU.Id_Consumidor = CON.Id_Consumidor) ' +  
      'WHERE USU.Nm_Usuario = ' + '''' + @pNm_Usuario + ''''  
  EXEC(@v_SQL)  
 END  
 ELSE  
 BEGIN  
  -----retona 0 para validacao  
  SELECT TOP 0 * FROM Usuario_Global  
 END   
END   
  
-------envia senha por email para quando usuario esquecer a mesma  
IF @pPAKAGE = 'Sd_Envia_Senha_Email'  
BEGIN  
 SELECT @v_Empresa = '', @v_Senha = '', @v_Nm_Usuario = ''  
  
 SELECT @v_Nm_Usuario = Nm_Usuario,  
   @v_Empresa = Empresa,  
   @v_Senha = Senha  
 FROM Usuario_Global   
 WHERE Email = @pEmail_Corporativo   
   
 IF NOT EXISTS(SELECT EMail FROM Usuario_Global WHERE EMail = @pEmail_Corporativo GROUP BY EMail HAVING COUNT(*) > 1)  
 BEGIN  
  IF NOT @v_Empresa = '' AND NOT @v_Nm_Usuario = '' AND NOT @v_Senha = ''  
  BEGIN  
   SET @v_SQL = 'INSERT INTO ' + @v_Empresa + '..Mail_Caixa_Saida ' +  
       'VALUES (8, ' + '''' + @pEmail_Corporativo + '''' + ', NULL,' +   
       '''' + 'Usuário:' + @v_Nm_Usuario + ' - Senha:' + @v_Senha + '''' + ', GETDATE(), NULL, 0, 0, 0)'  
   EXEC(@v_SQL)  
   SELECT 'Sua senha foi encaminhada por e-mail' AS Texto_Email  
  END  
  ELSE  
  BEGIN  
   SELECT 'Seu e-mail não foi identificado em nosso cadastro, favor entrar em contato com a área de TI.' AS Texto_Email  
  END  
 END  
 ELSE  
 BEGIN  
  SELECT 'Seu e-mail está duplicado, favor entrar em contato com a área de TI.' AS Texto_Email  
 END  
END  
  
-------valida dados do usuario pelo App  
IF @pPAKAGE = 'Sd_Valida_Numero_App'  
BEGIN  
 -----BAIN  
 SELECT DISTINCT  
   U.Id_Usuario,  
   C.Id_Consumidor,  
   C.Nm_Consumidor,  
   U.Nm_Usuario,  
   C.Matricula,  
   U.Id_Usuario_Perfil_Acesso,  
   U.Id_Idioma,  
   U.Fl_Desativado,  
   (SELECT TOP 1 Logo FROM Bain..vw_Holding) AS Logo,  
   (SELECT TOP 1 Nm_Holding FROM Bain..vw_Holding) AS Empresa  
 FROM Bain..Rl_Consumidor_Ativo RAC INNER JOIN Bain..vw_Ativo A ON A.Id_Ativo = RAC.Id_Ativo  
           INNER JOIN Bain..vw_Consumidor C ON C.Id_Consumidor = RAC.Id_Consumidor  
           INNER JOIN Bain..vw_Usuario U ON C.Id_Consumidor = U.Id_Consumidor  
           INNER JOIN Usuario_Global UG ON U.Nm_Usuario =  UG.Nm_Usuario AND U.Senha = UG.Senha  
 WHERE UG.Nm_Usuario = @pNm_Usuario   
   AND UG.Senha  = @pSenha  
 UNION ALL  
 -----AMIL  
 SELECT DISTINCT  
   U.Id_Usuario,  
   C.Id_Consumidor,  
   C.Nm_Consumidor,  
   U.Nm_Usuario,  
   C.Matricula,  
   U.Id_Usuario_Perfil_Acesso,  
   U.Id_Idioma,  
   U.Fl_Desativado,  
   (SELECT TOP 1 Logo FROM Amil..vw_Holding) AS Logo,  
   (SELECT TOP 1 Nm_Holding FROM Amil..vw_Holding) AS Empresa  
 FROM Amil..Rl_Consumidor_Ativo RAC INNER JOIN Amil..vw_Ativo A ON A.Id_Ativo = RAC.Id_Ativo  
           INNER JOIN Amil..vw_Consumidor C ON C.Id_Consumidor = RAC.Id_Consumidor  
           INNER JOIN Amil..vw_Usuario U ON C.Id_Consumidor = U.Id_Consumidor  
           INNER JOIN Usuario_Global UG ON U.Nm_Usuario =  UG.Nm_Usuario AND U.Senha = UG.Senha  
 WHERE UG.Nm_Usuario = @pNm_Usuario   
   AND UG.Senha  = @pSenha  
 UNION ALL  
 -----MODELO  
 SELECT DISTINCT  
   U.Id_Usuario,  
   C.Id_Consumidor,  
   C.Nm_Consumidor,  
   U.Nm_Usuario,  
   C.Matricula,  
   U.Id_Usuario_Perfil_Acesso,  
   U.Id_Idioma,  
   U.Fl_Desativado,  
   (SELECT TOP 1 Logo FROM Modelo..vw_Holding) AS Logo,  
   (SELECT TOP 1 Nm_Holding FROM Modelo..vw_Holding) AS Empresa  
 FROM Modelo..Rl_Consumidor_Ativo RAC INNER JOIN Modelo..vw_Ativo A ON A.Id_Ativo = RAC.Id_Ativo  
           INNER JOIN Modelo..vw_Consumidor C ON C.Id_Consumidor = RAC.Id_Consumidor  
           INNER JOIN Modelo..vw_Usuario U ON C.Id_Consumidor = U.Id_Consumidor  
           INNER JOIN Usuario_Global UG ON U.Nm_Usuario =  UG.Nm_Usuario AND U.Senha = UG.Senha  
 WHERE UG.Nm_Usuario = @pNm_Usuario   
   AND UG.Senha  = @pSenha  
END  
  
-----grava chave de id do App  
IF @pPAKAGE = 'Sd_Valida_Chave_App'  
BEGIN  
 SELECT @v_Empresa = '', @v_Nm_Usuario = ''  
  
 SELECT @v_Nm_Usuario = Nm_Usuario,  
   @v_Empresa = Empresa   
 FROM Usuario_Global   
 WHERE Chave_Validacao_App = @pChave_Validacao   
  
 IF NOT @v_Empresa = '' AND NOT @v_Nm_Usuario = ''  
 BEGIN  
  UPDATE Usuario_Global   
  SET  Fl_Validacao_App = 1  
  WHERE Chave_Validacao_App = @pChave_Validacao  
  SELECT 'Validação OK.' AS Texto_Validacao  
 END  
 ELSE  
 BEGIN  
  SELECT 'Id Inválido.' AS Texto_Validacao  
 END  
END  
  
-----vincula dados do usuario para o App  
IF @pPAKAGE = 'Sd_Viculacao_Email_App'  
BEGIN  
 DECLARE @v_Validacao_App AS TABLE(ID INT)  
 DECLARE @v_Chave_Validacao_App AS VARCHAR(100)  
 SELECT @v_Empresa = '', @v_Nm_Usuario = ''  
  
 SELECT @v_Nm_Usuario = Nm_Usuario,  
   @v_Empresa = Empresa   
 FROM Usuario_Global   
 WHERE Email = @pEmail_Corporativo  
   
 IF NOT EXISTS(SELECT EMail FROM Usuario_Global WHERE EMail = @pEmail_Corporativo GROUP BY EMail HAVING COUNT(*) > 1)  
 BEGIN  
  IF NOT @v_Empresa = '' AND NOT @v_Nm_Usuario = ''  
  BEGIN  
   SET @v_SQL = 'SELECT USU.Id_Usuario ' +  
       'FROM ' + @v_Empresa + '..vw_Usuario USU INNER JOIN ' + @v_Empresa + '..vw_Consumidor CON ON (USU.Id_Consumidor = CON.Id_Consumidor) ' +  
         ' INNER JOIN ' + @v_Empresa + '..Rl_Consumidor_Ativo RCA ON RCA.Id_Consumidor = CON.Id_Consumidor ' +  
         ' INNER JOIN ' + @v_Empresa + '..vw_Ativo AT ON RCA.Id_Ativo = AT.Id_Ativo ' +   
       'WHERE USU.Nm_Usuario = ' + '''' + @v_Nm_Usuario + '''' +  
         ' AND CON.EMail = ' + '''' + @pEmail_Corporativo + '''' +  
         ' AND AT.Nr_Ativo = ' + '''' + @pSenha + '''' ----utilizando senha como numero da linha   
   INSERT INTO @v_Validacao_App  
   EXEC(@v_SQL)  
  
   IF (SELECT ID FROM @v_Validacao_App) > 0  
   BEGIN  
    SELECT @v_Chave_Validacao_App = @v_Empresa + REPLACE(LEFT(RAND((DATEPART(mm, GETDATE()) * 100000 ) + (DATEPART(ss, GETDATE()) * 1000 ) + DATEPART(ms, GETDATE())),6), '0.', 'AT')  
    UPDATE Usuario_Global   
    SET  Chave_Validacao_App = @v_Chave_Validacao_App,  
      Fl_Validacao_App = 2  
    WHERE Email = @pEmail_Corporativo   
      AND Nm_Usuario = @v_Nm_Usuario AND Empresa = @v_Empresa  
     
    SET @v_SQL = 'INSERT INTO ' + @v_Empresa + '..Mail_Caixa_Saida ' +  
        'VALUES (7, ' + '''' + @pEmail_Corporativo + '''' + ', NULL,' +   
        '''' + @v_Chave_Validacao_App + '''' + ', GETDATE(), NULL, 0, 0, 0)'  
    EXEC(@v_SQL)  
         
    SELECT 'Seu ID foi encaminhado por e-mail.' AS Texto_Email  
   END  
   ELSE  
   BEGIN  
    SELECT 'Seu e-mail ou linha não foram identificados em nosso cadastro, favor entrar em contato com a área de TI.' AS Texto_Email  
   END  
   END  
  ELSE  
  BEGIN  
   SELECT 'Seu e-mail ou linha não foram identificados em nosso cadastro, favor entrar em contato com a área de TI.' AS Texto_Email  
  END  
 END  
 ELSE  
 BEGIN  
  SELECT 'Seu e-mail está duplicado, favor entrar em contato com a área de TI.' AS Texto_Email  
 END  
END  
  