USE [Fortlev]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JOAO CARLOS
-- Create date: 25/03/2024
-- Description:	PROCEDIMENTO EXECUTADO APOS INSERIR UM REGISTRO NA TABELA DE VINCULO DE ATIVO COM CONSUMIDOR.
--              QUE REALIZA O PROCEDIMENTO DE ENVIO DO TERMO PARA O CONSUMIDOR VINCULANDO NA AREA ANOMINA
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'pa_usuario_perfil_insert_termo_responsabilidade')
DROP PROCEDURE pa_usuario_perfil_insert_termo_responsabilidade
GO
CREATE PROCEDURE pa_usuario_perfil_insert_termo_responsabilidade
	-- Add the parameters for the stored procedure here
	@pId_usuario int,
	@pId_Ativo int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    /* K2AICONTROL 24 - ENVIO DE EMAIL DO TERMO DE RESPONSABILIDADE*/
			DECLARE @vId_Ativo_Tipo int
			DECLARE @v_Nm_Usuario varchar(100)
			DECLARE @v_Empresa varchar(100)
			DECLARE @v_Email VARCHAR(150)
			--DECLARE @vId_Ativo int = 1785
			--DECLARE @vId_Usuario int = 4811
			DECLARE @v_Token VARCHAR(150) DECLARE @v_SQL AS VARCHAR(MAX)
			DECLARE @v_tokenExists INT
			DECLARE @v_Termo_Ativacao varchar(150)
			DECLARE @v_Nm_Ativo_Tipo_Grupo VARCHAR(100)

			DECLARE @v_Id_Consumidor INT,
			        @v_Nm_Consumidor VARCHAR(100),
					@v_Matricula VARCHAR(100),
					@v_Endereco VARCHAR(100),
					@v_Nm_Filial VARCHAR(100),
					@v_Centro_Custo VARCHAR(100),
					@v_Nm_Departamento VARCHAR(100),
					@v_Linha VARCHAR(100),
					@v_Tipo_Ativo VARCHAR(100),
					@v_Nm_Conglomerado VARCHAR(100),
					@v_Nm_Modelo_Fabricante VARCHAR(100),
					@V_IMEI VARCHAR(100),
					@V_ACESSORIO VARCHAR(100),
					@v_CHIP VARCHAR(100),
					@V_Carregador INT,
					@V_Cabo_USB INT,
					@V_Fone INT,
					@V_Pelicula INT,
					@V_Capa_ProtecAo INT,
					@V_Nr_Aparelho VARCHAR(100),
					@V_NF VARCHAR(100),
					@v_Logo varchar(100)

			select @vId_Ativo_Tipo = Id_Ativo_Tipo from ativo where Id_Ativo = @pId_Ativo

			IF(@vId_Ativo_Tipo = 1)
				BEGIN
					if(not exists(select 1 from  [Ativvus_Login]..[Usuario_Envio_TermoResponsabilidade] 
								  where Id_Ativo = @pId_Ativo and Id_Usuario = @pId_Usuario and Token_Validade > GETDATE()
							   )
					  )
					begin
						select @v_Nm_Usuario = Nm_Usuario from Usuario where Id_usuario = @pId_Usuario

						select @v_Empresa = Empresa, @v_Email = EMail from Ativvus_Login..Usuario_Global where Nm_Usuario = @v_Nm_Usuario

						----gerar um token
					    set @v_Token = CONVERT(VARCHAR(50), NEWID())
					     ----Verificar se o token já existe na tabela
						SELECT @v_tokenExists = COUNT(*) FROM [Ativvus_Login]..[Usuario_Envio_TermoResponsabilidade] WHERE Token = @v_Token

						---- Se o token já existe, gere um novo até que seja único
						WHILE @v_tokenExists > 0
						BEGIN
							SET @v_Token = CONVERT(VARCHAR(50), NEWID())
							SELECT @v_tokenExists = COUNT(*) FROM [Ativvus_Login]..[Usuario_Envio_TermoResponsabilidade] WHERE Token = @v_Token
						END

						--recuperar o termo recuperação que sera usado para montar a tela 
						SELECT 
						  @v_Termo_Ativacao =  ISNULL(ATG.Termo_Ativacao, ''),			
			              @v_Nm_Ativo_Tipo_Grupo = CONVERT(VARCHAR, Nm_Ativo_Tipo_Grupo),
						  @v_Logo=(SELECT TOP 1 Logo FROM vw_Holding) 
						 FROM vw_Consumidor CON 
								INNER JOIN Rl_Consumidor_Ativo RCA ON CON.Id_Consumidor = RCA.Id_Consumidor      
								INNER JOIN Ativo A ON A.Id_Ativo = RCA.Id_Ativo      
								INNER JOIN Ativo_Tipo AT ON A.Id_Ativo_Tipo = AT.Id_Ativo_Tipo       
								INNER JOIN Ativo_Tipo_Grupo ATG ON AT.Id_Ativo_Tipo_Grupo  = ATG.Id_Ativo_Tipo_Grupo       
								LEFT JOIN vw_Usuario US ON US.Id_Consumidor = CON.Id_Consumidor      
								LEFT JOIN Rl_Perfil_Ativo_Usuario RUA ON US.Id_Usuario = RUA.Id_Usuario AND RUA.Id_Ativo = RCA.Id_Ativo
						 WHERE RCA.Id_Ativo = @pId_Ativo

                       ----recuperar os detalhes para montar o termo
					   SELECT DISTINCT
								--@pId_Ativo AS Id_Ativo,
								--US.Id_Usuario AS Id_Usuario,
								@v_Id_Consumidor=ISNULL(CON.Id_Consumidor,'') ,
								@v_Nm_Consumidor=ISNULL(CON.Nm_Consumidor,'') ,
								@v_Email = ISNULL(CON.Email, ''),
								--ISNULL(US.Nm_Usuario,'') AS Nm_Usuario,
								@v_Matricula=ISNULL(CON.Matricula,'') ,
								@v_Endereco = ISNULL(FI.Endereco,'') ,
								@v_Nm_Filial=ISNULL(FI.Nm_Filial,'') ,
								@v_Centro_Custo=ISNULL(CC.Cd_Centro_Custo,'') ,
								@v_Nm_Departamento=ISNULL(DP.Nm_Departamento,'') ,
								@v_Linha=ISNULL(A.Nr_Ativo,'') ,
								@v_Tipo_Ativo=ISNULL(AT.Nm_Ativo_Tipo,'') ,
								@v_Nm_Conglomerado=ISNULL(CG.Nm_Conglomerado,'') ,
								@v_Nm_Modelo_Fabricante=ISNULL(AMF.Descricao,'') ,
							-- Adicionando a parte dinâmica para IMEI
								@v_IMEI=ISNULL((SELECT 'C' + CONVERT(VARCHAR(2), REG)
										FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB
										WHERE NM_ATIVO_COMPLEMENTO = 'IMEI'), '') ,
                            -- Adicionando a parte dinâmica para CHIP
							   @v_CHIP = ISNULL((SELECT	'C'+ CONVERT(VARCHAR(2),REG) 
										   FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB       
										   WHERE NM_ATIVO_COMPLEMENTO = 'CHIP'), '') ,
							-- Adicionando a parte dinâmica para ACESSORIO
							   @v_ACESSORIO=ISNULL((SELECT 'C' + CONVERT(VARCHAR(2), REG)
										FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB
										WHERE NM_ATIVO_COMPLEMENTO = 'ACESSORIO'), '') ,
								@v_Carregador=ISNULL(EA.Ck_Carregador,'') ,
								@v_Cabo_USB=ISNULL(EA.Ck_Cabousb,''),
								@v_Fone=ISNULL(EA.Ck_Fone,'') ,
								@v_Pelicula=ISNULL(EA.Ck_Pelicula,''),
								@v_Capa_Protecao=ISNULL(EA.Ck_Capaprotecao,'') ,
								@v_Nr_Aparelho=ISNULL(ISNULL((SELECT TOP 1 Nr_Aparelho
												FROM Estoque_Aparelho
												WHERE Id_Aparelho = EAA.Id_Aparelho AND Id_Aparelho_Tipo = 1), (SELECT 'C' + CONVERT(VARCHAR(2), REG)
																													FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB
																													WHERE NM_ATIVO_COMPLEMENTO = 'NS')), '') ,
								@v_NF=ISNULL(ISNULL((SELECT TOP 1 SENF.Nr_Nota_Fiscal
												FROM Estoque_Aparelho SEA INNER JOIN Estoque_Nota_Fiscal SENF ON SENF.Id_Estoque_Nota_Fiscal = SEA.Id_Estoque_Nota_Fiscal
												WHERE Id_Aparelho = EAA.Id_Aparelho
												AND SEA.Id_Aparelho_Tipo = 1), (SELECT 'C' + CONVERT(VARCHAR(2), REG)
																				FROM (SELECT ROW_NUMBER() OVER (ORDER BY NM_ATIVO_COMPLEMENTO) AS REG, Nm_Ativo_Complemento FROM Ativo_Complemento WHERE Id_Ativo_tipo IN (SELECT Id_Ativo_tipo FROM Ativo WHERE Id_Ativo = @pId_Ativo)) AS SUB
																				WHERE NM_ATIVO_COMPLEMENTO = 'NF')), '') 
							FROM Fortlev..Consumidor CON
								INNER JOIN  Rl_Consumidor_Ativo RCA ON CON.Id_Consumidor = RCA.Id_Consumidor
								INNER JOIN  Filial FI ON FI.Id_Filial = CON.Id_Filial
								INNER JOIN  Centro_Custo CC ON CC.Id_Centro_Custo = CON.Id_Centro_Custo
								INNER JOIN  Ativo A ON A.Id_Ativo = RCA.Id_Ativo
								INNER JOIN  Ativo_Tipo AT ON A.Id_Ativo_Tipo = AT.Id_Ativo_Tipo
								INNER JOIN  Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
								INNER JOIN  Usuario US ON US.Id_Consumidor = CON.Id_Consumidor
								LEFT JOIN  Departamento DP ON DP.Id_Departamento = CON.Id_Departamento
								LEFT JOIN  vw_Ativo_Modelo_Fabricante AMF ON AMF.Id_Ativo_Modelo = A.Id_Ativo_Modelo
								LEFT JOIN  Rl_Estoque_Aparelho_Ativo EAA ON EAA.id_Ativo = A.Id_Ativo
								LEFT JOIN  Estoque_Aparelho EA ON EAA.Id_Aparelho = EA.Id_Aparelho
							WHERE RCA.Id_Ativo = @pId_Ativo
							
            
					   ----registra o token para o usuario
							INSERT INTO [Ativvus_Login].[dbo].[Usuario_Envio_TermoResponsabilidade]
								([Id_Usuario]
								,[Id_Ativo]
								,[Empresa]
								,[DataCadastro]
								,[QtdEnvios]
								,[DataUltEnvio]
								,[Url_Termo]
								,[Token]
								,[Token_Validade]
								,[Token_Confirmacao]
								,[Termo_Ativacao]
								,[Nm_Ativo_Tipo_Grupo]
								,[Id_Consumidor]
								,[Nm_Consumidor]
								,[Nm_Usuario]
								,[Matricula]
								,[Endereco]
								,[Nm_Filial]
								,[Centro_Custo]
								,[Departamento]
								,[Linha]
								,[Tipo_Ativo]
								,[Nm_Conglomerado]
								,[Nm_Modelo_Fabricante]
								,[IMEI]
								,[ACESSORIO]
								,[Carregador]
								,[Cabo_USB]
								,[Fone]
								,[Pelicula]
								,[Capa_Protecao]
								,[Nr_Aparelho]
								,[NF]
								,[Logo]
								,[CHIP])
							VALUES
								(@pId_usuario --<Id_Usuario, int,>
								,@pId_Ativo --<Id_Ativo, int,>
								,@v_Empresa --<Empresa, varchar(50),>
								,Getdate() --<DataCadastro, datetime,>
								,0 --<QtdEnvios, int,>
								,NULL --<DataUltEnvio, datetime,>
								,NULL --<Url_Termo, varchar(200),>
								,@v_Token --<Token, varchar(250),>
								,DATEADD(day, 1, GETDATE()) --<Token_Validade, datetime,>
								,NULL --<Token_Confirmacao, datetime,>
								,@v_Termo_Ativacao --<Termo_Ativacao, varchar(150),>
								,@v_Nm_Ativo_Tipo_Grupo--<Nm_Ativo_Tipo_Grupo, varchar(150),>
								,@v_Id_Consumidor --<Id_Consumidor, int,>
								,@v_Nm_Consumidor --<Nm_Consumidor, varchar(100),>
								,@v_Nm_Usuario --<Nm_Usuario, varchar(100),>
								,@v_Matricula --<Matricula, varchar(100),>
								,@v_Endereco --<Endereco, varchar(100),>
								,@v_Nm_Filial --<Nm_Filial, varchar(100),>
								,@v_Centro_Custo --<Centro_Custo, varchar(100),>
								,@v_Nm_Departamento --<Departamento, varchar(100),>
								,@v_Linha --<Linha, varchar(100),>
								,@v_Tipo_Ativo --<Tipo_Ativo, varchar(100),>
								,@v_Nm_Conglomerado --<Nm_Conglomerado, varchar(100),>
								,@v_Nm_Modelo_Fabricante --<Nm_Modelo_Fabricante, varchar(100),>
								,@V_IMEI --<IMEI, varchar(100),>
								,@V_ACESSORIO --<ACESSORIO, varchar(100),>
								,@V_Carregador --<Carregador, int,>
								,@V_Cabo_USB --<Cabo_USB, int,>
								,@V_Fone --<Fone, int,>
								,@V_Pelicula --<Pelicula, int,>
								,@V_Capa_ProtecAo --<Capa_Protecao, int,>
								,@V_Nr_Aparelho --<Nr_Aparelho, varchar(100),>
								,@V_NF --<NF, varchar(100),>
								,@v_Logo --<Logo, varchar(100),>
								,@v_CHIP --<CHIP, varchar(100),>
								)
													  
					   ---grava mensagem de e-mail na tabela de envio
						INSERT INTO [dbo].[Mail_Caixa_Saida]
								   ([E_Mail_Destino]
								   ,[E_Mail_Copia]
								   ,[Texto_Adicional]
								   ,[Dt_Programacao]
								   ,[Dt_Saida]
								   ,[Fl_Enviado]
								   ,[Id_Log_SQL]
								   ,[Fl_QTD_Tentativa_Envio])
							 VALUES
								   (@v_Email --<E_Mail_Destino, varchar(50),>
								   ,NULL --<E_Mail_Copia, varchar(50),>
								   ,'Usuário:' + @v_Nm_Usuario + ' - Clique no link para validar as informãções sobre seu ativo: <a href="https://icontrolit.homologa.bitgrow.net?hashtr=' + @v_Token + '" target="_blank">Validar</a>' --<Texto_Adicional, varchar(8000),>
								   ,GETDATE() --<Dt_Programacao, datetime,>
								   ,NULL --<Dt_Saida, datetime,>
								   ,0 --<Fl_Enviado, bit,>
								   ,NULL --<Id_Log_SQL, int,>
								   ,0 --<Fl_QTD_Tentativa_Envio, int,>
								   )




					   INSERT INTO Mail_Caixa_Saida 
					   VALUES (15,  @v_Email  , NULL,
					   'Usuário:' + @v_Nm_Usuario + ' - Clique no link para validar as informãções sobre seu ativo: <a href="https://icontrolit.homologa.bitgrow.net?hashtr=' + @v_Token + '" target="_blank">Validar</a>', GETDATE(), NULL, 0, 0, 0)  

					end
				END --./Id_ATivo_Tipo -1 (Movel)

		  /* ./K2AICONTROL 24 - ENVIO DE EMAIL DO TERMO DE RESPONSABILIDADE*/
END
GO
