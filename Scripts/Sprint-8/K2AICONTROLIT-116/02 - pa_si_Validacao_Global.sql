USE [Ativvus_Login]
GO
/****** Object:  StoredProcedure [dbo].[pa_si_Validacao_Global]    Script Date: 20/02/2024 20:39:42 ******/
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
       '''' + 'Usuário:' + @v_Nm_Usuario + ' - Clique no link: <a href="https://www.icontrolit.com.br?hashts=' + @v_Token + '" target="_blank">Trocar Senha</a>''' + ', GETDATE(), NULL, 0, 0, 0)'  
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
  