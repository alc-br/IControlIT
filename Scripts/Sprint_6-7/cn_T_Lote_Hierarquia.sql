SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[cn_T_Lote_Hierarquia] (	@pPakage					VARCHAR(200),
												@pPakage_Filtro				VARCHAR(200)	= NULL,
												@pId_Filtro_1				VARCHAR(MAX)	= NULL,
												@pId_Filtro_Filial			VARCHAR(MAX)	= NULL,
												@pId_Filtro_Usuario			INT				= NULL,
												@p_Id_Filtro_Centro_Custo	VARCHAR(MAX)	= NULL,
												@p_Id_Filtro_Departamento	VARCHAR(MAX)	= NULL,
												@p_Id_Filtro_Setor			VARCHAR(MAX)	= NULL,
												@pDt_LoteDe					VARCHAR(6)		= NULL,
												@pDt_LoteAte				VARCHAR(6)		= NULL,
												@pAtivo_Tipo_Grupo			VARCHAR(50)		= NULL) AS
DECLARE @SQL AS VARCHAR(8000)
	DECLARE @CALC_DATA AS INT 
	DECLARE @ATE AS INT
	DECLARE @Filtro_Pivot AS VARCHAR(8000)

	-----monta intervalo de data
	SET @ATE = CONVERT(INT,@pDt_LoteAte)
	SET @CALC_DATA = CONVERT(INT,@pDt_LoteDe)

	SET @Filtro_Pivot = '[' + @pDt_LoteDe + '],'

	WHILE @CALC_DATA < @ATE
	BEGIN
		
		IF SUBSTRING(CONVERT(VARCHAR,@CALC_DATA),5,2) = '12'
		SET @CALC_DATA = @CALC_DATA + 88
	
		SET @CALC_DATA = @CALC_DATA + 1
		SET @Filtro_Pivot = @Filtro_Pivot + '[' + CONVERT(VARCHAR(6), @CALC_DATA) + '],'
	END	
	SET @Filtro_Pivot = SUBSTRING(@Filtro_Pivot, 1, LEN(@Filtro_Pivot) - 1)

	-----filtra dados
	SELECT	LT.DC_Nr_Nota_Fiscal				AS Conta, 
			AT.Nr_Ativo							AS Ativo,
			CG.Nm_Conglomerado					AS Conglomerado,
			(SELECT COUNT(*) FROM Rl_Consumidor_Ativo SRL INNER JOIN vw_Consumidor SC ON SRL.Id_Consumidor = SC.Id_Consumidor WHERE SRL.Id_Ativo = AT.Id_Ativo) AS Compartilhado,
			CASE WHEN AT.Fl_Desativado = 0 THEN 'Ativo' ELSE 'Desativado' END AS [Status_Cadastro],
			ATP.Nm_Ativo_Tipo					AS Tipo,
			ISNULL(CO.Nm_Consumidor, 'Vazia')	AS Colaborador,
			ISNULL(CO.Matricula, 'Vazia')		AS Matricula,
			ISNULL(US.Nm_Usuario, 'Vazia')		AS Usuario,
			ISNULL(FI.Nm_Filial, 'Vazia')		AS Filial,
			ISNULL(CC.Cd_Centro_Custo, 'Vazia') AS CDC,
			ISNULL(DP.Nm_Departamento, 'Vazia') AS Departamento,
			ISNULL(ST.Nm_Setor, 'Vazia')		AS Setor,
			(select rlpa.Dt_Hr_Ativacao from  Rl_Perfil_Ativo_Usuario rlpa where rlpa.id_usuario=US.Id_Usuario and  rlpa.id_ativo=AT.Id_Ativo)		AS [Data/Hora Ativação],
			(select rlpa.Dt_Hr_Desativacao from  Rl_Perfil_Ativo_Usuario rlpa where rlpa.id_usuario=US.Id_Usuario and  rlpa.id_ativo=AT.Id_Ativo)	AS [Data/Hora Desativação],
			isnull (at.Valor_Contrato ,'Vazia')	AS  [Valor Contrato],
			isnull (at.Plano_Contrato ,'Vazia')	AS  [Plano Contrato],
			isnull (at.Velocidade ,'Vazia')	as  Velocidade,
		--	ISNULL(CAST(LT.Total_Lote AS DECIMAL(10,2)),0) AS Total_Conta,
			SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) AS Lote,
			ISNULL(CAST(LT.Total_Consumo AS DECIMAL(10,2)),0) AS Total
			INTO #Temp_Export
	FROM	vw_Custo_Usuario LT INNER JOIN Ativo AT ON (AT.Id_Ativo = LT.Id_Ativo)
								INNER JOIN Ativo_Tipo ATP ON (ATP.Id_Ativo_Tipo = AT.Id_Ativo_Tipo)
								INNER JOIN Ativo_Tipo_Grupo ATG ON (ATG.Id_Ativo_Tipo_Grupo = ATP.Id_Ativo_Tipo_Grupo) 
								INNER JOIN Conglomerado CG ON (CG.Id_Conglomerado = AT.Id_Conglomerado)
								LEFT JOIN Usuario US ON (LT.Id_Usuario = US.Id_Usuario)
								LEFT JOIN Consumidor CO ON (CO.Id_Consumidor = LT.Id_Consumidor)
								LEFT JOIN Filial FI ON (FI.Id_Filial = CO.Id_Filial) 
								LEFT JOIN Centro_Custo CC ON (CC.Id_Centro_Custo = CO.Id_Centro_Custo)
								LEFT JOIN Departamento DP ON (DP.Id_Departamento = CO.Id_Departamento)
								LEFT JOIN Setor ST ON (ST.Id_Setor = CO.Id_Setor)
	WHERE	SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) >= @pDt_LoteDe 
			AND SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) <= @pDt_LoteAte
			AND (ATG.Nm_Ativo_Tipo_Grupo = @pAtivo_Tipo_Grupo OR @pAtivo_Tipo_Grupo IS NULL)

-----exportacao
IF @pPakage = 'sp_Lote_Mes_Export'
BEGIN
	-----monta consulta
	SET @SQL = 	'SELECT	*	
				FROM #Temp_Export
				PIVOT (SUM(Total) FOR Lote IN (' + @Filtro_Pivot +
				')) Retorno'

	EXEC (@SQL)
END

-----grid
IF @pPakage = 'sp_Lote_Mes'
BEGIN
	-----monta consulta
	SET @SQL = 	'SELECT *
				FROM #Temp_Export
				PIVOT (SUM(Total) FOR Lote IN (' + @Filtro_Pivot +
				')) Retorno'

	EXEC (@SQL)
END

-----grafrico
IF @pPakage = 'sp_Lote_Mes_Grafico'
BEGIN
	-----monta intervalo de data
	SET @ATE = CONVERT(INT,@pDt_LoteAte)
	SET @CALC_DATA = CONVERT(INT,@pDt_LoteDe)

	SET @Filtro_Pivot = '[' + @pDt_LoteDe + '],'

	WHILE @CALC_DATA < @ATE
	BEGIN
		SET @CALC_DATA = @CALC_DATA + 1
		SET @Filtro_Pivot = @Filtro_Pivot + '[' + CONVERT(VARCHAR(6), @CALC_DATA) + '],'

		IF SUBSTRING(CONVERT(VARCHAR,@CALC_DATA),4,2) = '12'
		SET @CALC_DATA = @CALC_DATA + 88
	END	
	SET @Filtro_Pivot = SUBSTRING(@Filtro_Pivot, 1, LEN(@Filtro_Pivot) - 1)

	-----filtra dados
	SELECT	SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) AS X,					
			CAST(SUM(LT.Total_Lote) AS DECIMAL(10,2)) AS Y --INTO #Temp_Grafico
	FROM	vw_Custo_Usuario LT INNER JOIN Ativo AT ON (AT.Id_Ativo = LT.Id_Ativo)
								INNER JOIN Ativo_Tipo ATP ON (ATP.Id_Ativo_Tipo = AT.Id_Ativo_Tipo)
								INNER JOIN Ativo_Tipo_Grupo ATG ON (ATG.Id_Ativo_Tipo_Grupo = ATP.Id_Ativo_Tipo_Grupo) 
	WHERE	SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) >= @pDt_LoteDe 
			AND	SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) <= @pDt_LoteAte
			AND (ATG.Nm_Ativo_Tipo_Grupo = @pAtivo_Tipo_Grupo OR @pAtivo_Tipo_Grupo IS NULL)
	GROUP BY SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6)
	ORDER BY X
END
