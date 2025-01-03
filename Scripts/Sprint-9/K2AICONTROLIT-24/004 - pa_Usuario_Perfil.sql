USE [Fortlev]
GO
/****** Object:  StoredProcedure [dbo].[pa_Usuario_Perfil]    Script Date: 23/03/2024 10:40:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[pa_Usuario_Perfil] (	@pPakage				VARCHAR(200),
											@pId_Usuario			INT				= NULL,
											@pId_Consumidor			INT				= NULL,
											@pDescricao				VARCHAR(50)		= NULL,
											@pRelacao				VARCHAR(MAX)	= NULL,
											@pId_Ativo				INT				= NULL,
											@pDt_Ativacao			DATETIME		= NULL,
											@pDt_Desativacao		DATETIME		= NULL,
											@pId_Usuario_Permissao	INT				= NULL) AS
DECLARE @Array VARCHAR(MAX)
DECLARE @S VARCHAR(10)

-----desativa resgistro
IF @pPAKAGE = 'sp_SE'
BEGIN
	IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Excluir') = 2
	BEGIN
		DELETE FROM Rl_Perfil_Centro_Custo_Usuario WHERE Id_Usuario = @pId_Usuario
		DELETE FROM Rl_Perfil_Departamento_Usuario WHERE Id_Usuario = @pId_Usuario
		DELETE FROM Rl_Perfil_Conglomerado_Usuario WHERE Id_Usuario = @pId_Usuario
		DELETE FROM Rl_Perfil_Filial_Usuario WHERE Id_Usuario = @pId_Usuario
		DELETE FROM Rl_Perfil_Secao_Usuario WHERE Id_Usuario = @pId_Usuario
		DELETE FROM Rl_Perfil_Setor_Usuario WHERE Id_Usuario = @pId_Usuario
		DELETE FROM dbo.Rl_Perfil_Consumidor_Usuario WHERE Id_Usuario = @pId_Usuario
	END
END
-----consulta pelo ID do registro
IF @pPAKAGE = 'sp_SL_ID'
BEGIN
	SELECT	ISNULL(USU.Id_Usuario,'')				AS Id_Usuario,
			ISNULL(USU.Nm_Usuario,'')				AS Nm_Usuario,
			ISNULL(USU.Id_Consumidor,'')			AS Id_Consumidor,
			ISNULL(CON.Nm_Consumidor,'')			AS Nm_Consumidor,
			ISNULL(USU.Id_Usuario_Perfil,'')		AS Id_Usuario_Perfil,
			ISNULL(UPF.Nm_Usuario_Perfil,'')		AS Nm_Usuario_Perfil,
			ISNULL(USU.Id_Usuario_Perfil_Acesso,'')	AS Id_Usuario_Perfil_Acesso,
			ISNULL(UPA.Nm_Usuario_Perfil_Acesso,'')	AS Nm_Usuario_Perfil_Acesso
	FROM	Usuario USU		INNER JOIN	vw_Consumidor				CON	ON (USU.Id_Consumidor = CON.Id_Consumidor)
							INNER JOIN 	Si_Usuario_Perfil			UPF	ON (USU.Id_Usuario_Perfil = UPF.Id_Usuario_Perfil)
							INNER JOIN 	Si_Usuario_Perfil_Acesso	UPA	ON (USU.Id_Usuario_Perfil_Acesso = UPA.Id_Usuario_Perfil_Acesso)
	WHERE	Id_Usuario = @pId_Usuario
END

---retorna ativos de competencia do consumidor 
IF @pPAKAGE = 'sp_SL_Ativo_Competencia_Consumidor'
BEGIN
	SELECT	RCA.Id_Ativo,
			ATV.Nr_Ativo,
			RPAU.Dt_Hr_Ativacao,
			RPAU.Dt_Hr_Desativacao
	FROM	Usuario	USU	INNER JOIN Rl_Consumidor_Ativo RCA ON (USU.Id_Consumidor = RCA.Id_Consumidor)
						LEFT JOIN Rl_Perfil_Ativo_Usuario RPAU ON (RCA.Id_Ativo = RPAU.Id_Ativo AND USU.Id_Usuario = RPAU.Id_Usuario)
						INNER JOIN vw_ATIVO ATV ON (ATV.Id_Ativo = RCA.Id_Ativo)
	WHERE	RCA.Id_Consumidor =	@pId_Consumidor
END

-----filtra consumidor para selecao do acesso do perfil
IF @pPAKAGE = 'sp_Perfil_Acesso_Consumidor'
BEGIN
	SELECT	DISTINCT VWCON.Id_Consumidor,
			Nm_Consumidor
	FROM	vw_Consumidor VWCON LEFT JOIN Usuario VWUSU	ON (VWUSU.Id_Consumidor = VWCON.Id_Consumidor)
	WHERE	((Nm_Consumidor LIKE '%'+@pDescricao+'%' OR Nm_Usuario LIKE '%'+@pDescricao+'%'))
	ORDER BY VWCON.Nm_Consumidor
END

-----filtra empresa servico para selecao  do acesso do perfil
IF @pPAKAGE = 'sp_Perfil_Acesso_Conglomerado'
BEGIN
	SELECT	DISTINCT Id_Conglomerado,
			Nm_Conglomerado
	FROM	vw_Conglomerado
	WHERE	(Nm_Conglomerado LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)
	ORDER BY Nm_Conglomerado
END

-----filtra filial para selecao  do acesso do perfil
IF @pPAKAGE = 'sp_Perfil_Acesso_Filial'
BEGIN
	SELECT	DISTINCT Id_Filial,
			Nm_Filial
	FROM	vw_Filial
	WHERE	(Nm_Filial LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)
	ORDER BY Nm_Filial
END

-----filtra centro de custo para selecao  do acesso do perfil
IF @pPAKAGE = 'sp_Perfil_Acesso_Centro_Custo'
BEGIN
	SELECT	DISTINCT CC.Id_Centro_Custo,
			CC.Cd_Centro_Custo
	FROM	Rl_Hi_Filial_Centro_Custo RHFC INNER JOIN vw_Centro_Custo CC ON (RHFC.Id_Centro_Custo = CC.Id_Centro_Custo)
	WHERE	RHFC.Id_Filial IN (SELECT Id_Filial FROM Rl_Perfil_Filial_Usuario WHERE Id_Usuario = @pId_Usuario)
			AND (CC.Cd_Centro_Custo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)
	ORDER BY CC.Cd_Centro_Custo
END

-----filtra departamento para selecao do acesso do perfil
IF @pPAKAGE = 'sp_Perfil_Acesso_Departamento'
BEGIN
	SELECT	DISTINCT DEP.Id_Departamento,
			DEP.Nm_Departamento
	FROM	Rl_Hi_Filial_Centro_Custo RHFC	INNER JOIN Rl_Hi_Centro_Custo_Departamento RHCD ON (RHFC.Id_Centro_Custo = RHCD.Id_Centro_Custo)
											INNER JOIN vw_Departamento DEP ON (RHCD.Id_Departamento = DEP.Id_Departamento)
	WHERE	RHFC.Id_Filial IN (SELECT Id_Filial FROM Rl_Perfil_Filial_Usuario WHERE Id_Usuario = @pId_Usuario)
			AND (DEP.Nm_Departamento LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)
	ORDER BY DEP.Nm_Departamento
END

-----filtra setor para selecao  do acesso do perfil
IF @pPAKAGE = 'sp_Perfil_Acesso_Setor'
BEGIN
	SELECT	DISTINCT ST.Id_Setor,
			ST.Nm_Setor
	FROM	Rl_Hi_Filial_Centro_Custo RHFC	INNER JOIN Rl_Hi_Centro_Custo_Departamento RHCD ON (RHFC.Id_Centro_Custo = RHCD.Id_Centro_Custo)
											INNER JOIN Rl_Hi_Departamento_Setor RHDS ON (RHCD.Id_Departamento = RHDS.Id_Departamento)
											INNER JOIN vw_Setor ST ON (RHDS.Id_Setor = ST.Id_Setor)
	WHERE	RHFC.Id_Filial IN (SELECT Id_Filial FROM Rl_Perfil_Filial_Usuario WHERE Id_Usuario = @pId_Usuario)
			AND (ST.Nm_Setor LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)
	ORDER BY ST.Nm_Setor
END

-----filtra secao para selecao do acesso do perfil
IF @pPAKAGE = 'sp_Perfil_Acesso_Secao'
BEGIN
	SELECT	DISTINCT SC.Id_Secao,
			SC.Nm_Secao
	FROM	Rl_Hi_Filial_Centro_Custo RHFC	INNER JOIN Rl_Hi_Centro_Custo_Departamento RHCD ON (RHFC.Id_Centro_Custo = RHCD.Id_Centro_Custo)
											INNER JOIN Rl_Hi_Departamento_Setor RHDS ON (RHCD.Id_Departamento = RHDS.Id_Departamento)
											INNER JOIN Rl_Hi_Setor_Secao RHSS ON(RHDS.Id_Setor = RHSS.Id_Setor)
											INNER JOIN vw_Secao SC ON (RHSS.Id_Secao = SC.Id_Secao)
	WHERE	RHFC.Id_Filial IN (SELECT Id_Filial FROM Rl_Perfil_Filial_Usuario WHERE Id_Usuario = @pId_Usuario)
			AND (SC.Nm_Secao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)
	ORDER BY SC.Nm_Secao
END

-----retorna perfil de acesso (consumidor)
IF @pPAKAGE = 'sp_Perfil_Acesso_Consumidor_Retorno'
BEGIN
	SELECT	CON.Id_Consumidor,
			CON.Nm_Consumidor
	FROM	Rl_Perfil_Consumidor_Usuario RPCU INNER JOIN vw_Consumidor CON ON (RPCU.Id_Consumidor = CON.Id_Consumidor)
	WHERE	RPCU.Id_Usuario = @pId_Usuario
	ORDER BY CON.Nm_Consumidor
END

-----retorna perfil de acesso (Conglomerado)
IF @pPAKAGE = 'sp_Perfil_Acesso_Conglomerado_Retorno'
BEGIN
	SELECT	EMS.Id_Conglomerado,
			EMS.Nm_Conglomerado
	FROM	Rl_Perfil_Conglomerado_Usuario RPEU INNER JOIN vw_Conglomerado EMS ON (RPEU.Id_Conglomerado = EMS.Id_Conglomerado)
	WHERE	RPEU.Id_Usuario = @pId_Usuario
	ORDER BY EMS.Nm_Conglomerado
END

-----retorna perfil de acesso (filial)
IF @pPAKAGE = 'sp_Perfil_Acesso_Filial_Retorno'
BEGIN
	SELECT	FIL.Id_Filial,
			FIL.Nm_Filial
	FROM	Rl_Perfil_Filial_Usuario RPFU INNER JOIN vw_Filial FIL ON (RPFU.Id_Filial = FIL.Id_Filial)
	WHERE	RPFU.Id_Usuario = @pId_Usuario
	ORDER BY FIL.Nm_Filial
END

-----retorna perfil de acesso (centro_Custo)
IF @pPAKAGE = 'sp_Perfil_Acesso_Centro_Custo_Retorno'
BEGIN
	SELECT	CC.Id_Centro_Custo,
			CC.Cd_Centro_Custo
	FROM	Rl_Perfil_Centro_Custo_Usuario RPCU INNER JOIN vw_Centro_Custo CC ON (RPCU.Id_Centro_Custo = CC.Id_Centro_Custo)
	WHERE	RPCU.Id_Usuario = @pId_Usuario
	ORDER BY CC.Nm_Centro_Custo
END

-----retorna perfil de acesso (deparatamento)
IF @pPAKAGE = 'sp_Perfil_Acesso_Departamento_Retorno'
BEGIN
	SELECT	DEP.Id_Departamento,
			DEP.Nm_Departamento
	FROM	Rl_Perfil_Departamento_Usuario RPDU INNER JOIN vw_Departamento DEP ON (RPDU.Id_Departamento = DEP.Id_Departamento)
	WHERE	RPDU.Id_Usuario = @pId_Usuario
	ORDER BY DEP.Nm_Departamento
END

-----retorna perfil de acesso (setor)
IF @pPAKAGE = 'sp_Perfil_Acesso_Setor_Retorno'
BEGIN
	SELECT	SE.Id_Setor,
			SE.Nm_Setor
	FROM	Rl_Perfil_Setor_Usuario RPSU INNER JOIN vw_Setor SE ON (RPSU.Id_Setor = SE.Id_Setor)
	WHERE	RPSU.Id_Usuario = @pId_Usuario
	ORDER BY SE.Nm_Setor
END

-----retorna perfil de acesso (secao)
IF @pPAKAGE = 'sp_Perfil_Acesso_Secao_Retorno'
BEGIN
	SELECT	SC.Id_Secao,
			SC.Nm_Secao
	FROM	Rl_Perfil_Secao_Usuario RPSU INNER JOIN vw_Secao SC ON (RPSU.Id_Secao = SC.Id_Secao)
	WHERE	RPSU.Id_Usuario = @pId_Usuario
	ORDER BY SC.Nm_Secao
END

-----insere data de ativacao e desativacao para ativo de competencia
IF @pPAKAGE = 'sp_Perfil_Acesso_Intervalo_Data_Ativo'
BEGIN
	IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Alterar') = 2
	BEGIN
		IF 0 = (	SELECT	COUNT(*) 
					FROM	Rl_Perfil_Ativo_Usuario 
					WHERE	Id_Usuario		= @pId_Usuario
							AND	Id_Ativo	= @pId_Ativo)

		BEGIN
			UPDATE	Rl_Perfil_Ativo_Usuario
			SET		Dt_Hr_Desativacao = GETDATE()
			WHERE	Id_Ativo = @pId_Ativo
					AND Dt_Hr_Desativacao IS NULL

			INSERT INTO Rl_Perfil_Ativo_Usuario
			VALUES(	@pId_Ativo,
					CASE WHEN @pDt_Ativacao = (SELECT LEFT(CONVERT(VARCHAR, GETDATE(), 120),10) + ' 00:00:00') THEN GETDATE()
						ELSE @pDt_Ativacao END,
					@pDt_Desativacao,
					@pId_Usuario)	 
          
		END
		ELSE
		BEGIN
			UPDATE	Rl_Perfil_Ativo_Usuario
			SET		Dt_Hr_Desativacao = GETDATE()
			WHERE	Id_Ativo = @pId_Ativo
					AND NOT Id_Usuario = @pId_Usuario
					AND Dt_Hr_Desativacao IS NULL

			UPDATE	Rl_Perfil_Ativo_Usuario
			SET		Dt_Hr_Ativacao		= CASE WHEN @pDt_Ativacao = (SELECT LEFT(CONVERT(VARCHAR, GETDATE(), 120),10) + ' 00:00:00') THEN GETDATE()
												ELSE @pDt_Ativacao END,
					Dt_Hr_Desativacao	= @pDt_Desativacao
			WHERE	Id_Usuario		= @pId_Usuario
					AND	Id_Ativo	= @pId_Ativo
		END
	END
END

-----insere data de ativacao e desativacao para ativo de competencia usando consumidor
IF @pPAKAGE = 'sp_Perfil_Acesso_Intervalo_Data_Ativo_Consumidor'
BEGIN
	IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Alterar') = 2
	BEGIN
		DECLARE @vId_Usuario INT
		SELECT @vId_Usuario = Id_Usuario FROM Usuario WHERE Id_Consumidor = @pId_Usuario
		
		IF NOT @vId_Usuario IS NULL
		BEGIN
			IF 0 = (	SELECT	COUNT(*) 
						FROM	Rl_Perfil_Ativo_Usuario 
						WHERE	Id_Usuario		= @vId_Usuario
								AND	Id_Ativo	= @pId_Ativo)

			BEGIN
				UPDATE	Rl_Perfil_Ativo_Usuario
				SET		Dt_Hr_Desativacao = GETDATE()
				WHERE	Id_Ativo = @pId_Ativo
						AND Dt_Hr_Desativacao IS NULL

				INSERT INTO Rl_Perfil_Ativo_Usuario
				VALUES(	@pId_Ativo,
						CASE WHEN @pDt_Ativacao = (SELECT LEFT(CONVERT(VARCHAR, GETDATE(), 120),10) + ' 00:00:00') THEN GETDATE()
							ELSE @pDt_Ativacao END,
						@pDt_Desativacao,
						@vId_Usuario)	
			END
			ELSE
			BEGIN

		
				--UPDATE	Rl_Perfil_Ativo_Usuario
				--SET		Dt_Hr_Desativacao = GETDATE()
				--WHERE	Id_Ativo = @pId_Ativo
				--		AND Dt_Hr_Ativacao IS NULL
				--		AND Dt_Hr_Desativacao IS NULL

				UPDATE	Rl_Perfil_Ativo_Usuario
				SET		Dt_Hr_Ativacao		= CASE WHEN @pDt_Ativacao = (SELECT LEFT(CONVERT(VARCHAR, GETDATE(), 120),10) + ' 00:00:00') THEN GETDATE()
													ELSE @pDt_Ativacao END,
						Dt_Hr_Desativacao = @pDt_Desativacao
				WHERE	Id_Usuario = @vId_Usuario
						AND	Id_Ativo= @pId_Ativo


			END
		END			
	END
END