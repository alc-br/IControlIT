SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[cg_bi_Carga_Bilhete] (	@pId_Arquivo			INT,
												@pDt_Lote				VARCHAR(6),
												@pDt_Liberacao_Lote		DATETIME,
												@pNumero_Nota_Fiscal	VARCHAR(50) = NULL) AS
------declara variavel para uso no while (loop)
DECLARE @v_CG_DB_Data_Hora		DATETIME
DECLARE @v_CG_DB_Destino		VARCHAR(50)
DECLARE @v_CG_DB_Consumo		FLOAT
DECLARE @v_CG_DB_Custo			NUMERIC(13,2)
DECLARE @v_CG_Id_Tronco			VARCHAR(50)
DECLARE @v_CG_Nota_Fiscal		VARCHAR(50)
DECLARE @v_CG_Id_Ativo			VARCHAR(50)
DECLARE @v_CG_Bilhete_Tipo		VARCHAR(50)
DECLARE @v_CG_Bilhete_Descricao	VARCHAR(50)
DECLARE @v_CG_DB_Origem			VARCHAR(50)
DECLARE @v_DB_Tipo_Parametro	VARCHAR(50)
DECLARE @v_DB_Localidade		VARCHAR(50)

-----declara variavel para auxiliar no while
DECLARE @v_Id_Ativo_Lote AS INT
DECLARE @v_Id_Lote AS FLOAT
DECLARE @v_Id_Lote_Log_Criacao AS FLOAT
DECLARE @v_Id_Bilhete AS FLOAT

-----declara varivel
DECLARE @v_SQL VARCHAR(MAX)
DECLARE @v_Campo VARCHAR(MAX)

-----inicia variavel
SELECT @v_SQL = ''
SELECT @v_Campo = ''
SELECT @v_Id_Lote = 0
SELECT @v_Id_Lote_Log_Criacao = 0
SELECT @v_Id_Bilhete = 0

----**********************************************************************************
-------convert consumo do formato de hora para formato numerico 
----**********************************************************************************
-----DB_Consumo-FLOAT
SELECT	@v_SQL = 	'UPDATE	Cg_Bi_Si_Arquivo_Formatado ' +
					'SET ' + AC.Nm_Campo + ' = ' + 'dbo.Fu_Convert_Hora_Decimal (' + AC.Nm_Campo + ') ' +
					'WHERE ISNUMERIC(' + AC.Nm_Campo + ') = 0'
FROM	Cg_Bi_Arquivo_Campo AC INNER JOIN Cg_Bi_Si_Campo_Bilhete CB ON (AC.Id_Campo_Bilhete = CB.Id_Campo_Bilhete)
WHERE	AC.Id_Arquivo = @pId_Arquivo
		AND CB.Nm_Campo_Tabela  = 'DB_Consumo'
PRINT(@v_SQL)
EXEC (@v_SQL)

------**********************************************************************************
-------valida campo para importacao dos dados para tabela bilhete
----**********************************************************************************
CREATE TABLE #Temp_Validacao (Retorno INT)
-----campo DB_Data_Hor-DATETIME
SELECT	@v_SQL = 'SELECT ISDATE(' + AC.Nm_Campo + ') FROM Cg_Bi_Si_Arquivo_Formatado'
FROM	Cg_Bi_Arquivo_Campo AC INNER JOIN Cg_Bi_Si_Campo_Bilhete CB ON (AC.Id_Campo_Bilhete = CB.Id_Campo_Bilhete)
WHERE	AC.Id_Arquivo = @pId_Arquivo
		AND CB.Nm_Campo_Tabela  = 'DB_Data_Hora'
-----campo DB_Consumo-FLOAT
SELECT	@v_SQL = @v_SQL + ' UNION SELECT ISNUMERIC(' + AC.Nm_Campo + ') FROM Cg_Bi_Si_Arquivo_Formatado'
FROM	Cg_Bi_Arquivo_Campo AC INNER JOIN Cg_Bi_Si_Campo_Bilhete CB ON (AC.Id_Campo_Bilhete = CB.Id_Campo_Bilhete)
WHERE	AC.Id_Arquivo = @pId_Arquivo
		AND CB.Nm_Campo_Tabela  = 'DB_Consumo'
-----campo DB_Custo-NUMERIC(13,2)
SELECT	@v_SQL = @v_SQL + ' UNION SELECT ISNUMERIC(' + AC.Nm_Campo + ') FROM Cg_Bi_Si_Arquivo_Formatado'
FROM	Cg_Bi_Arquivo_Campo AC INNER JOIN Cg_Bi_Si_Campo_Bilhete CB ON (AC.Id_Campo_Bilhete = CB.Id_Campo_Bilhete)
WHERE	AC.Id_Arquivo = @pId_Arquivo
		AND CB.Nm_Campo_Tabela  = 'DB_Custo'
-----insere validacao na tabela temporaria
PRINT(@v_SQL)
INSERT INTO #Temp_Validacao (Retorno)
EXEC(@v_SQL)

--**********************************************************************************
-----aborta se existir lote duplicados (o ativo para critica e retirado desse select)
--**********************************************************************************
--CREATE TABLE #Temp_Validacao_Complemento_Lote (Retorno INT)
--SELECT @v_SQL = 'SELECT  COUNT(*) ' +
--				'FROM	dbo.Cg_Bi_Si_Arquivo_Formatado TB ' +
--				'WHERE	EXISTS (SELECT * FROM LOTE WHERE Dt_Lote = ' + 'CONVERT(DATETIME, ' + '''' + @pDt_Lote + '''' + '+' + '''' + '01' + '''' + ') ' +
--										'AND Id_Ativo = TB.' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 7) + ')'

-------insere validacao na tabela temporaria
--PRINT(@v_SQL)
--INSERT INTO #Temp_Validacao_Complemento_Lote (Retorno)
--EXEC(@v_SQL)


--**********************************************************************************
-----inicia carga
--**********************************************************************************
---------aborta se nao validar
IF NOT EXISTS(SELECT * FROM #Temp_Validacao WHERE Retorno = 0)
BEGIN
	-----aborta se existir lote duplicados (o ativo para critica e retirado desse select)
	--IF	EXISTS(SELECT * FROM #Temp_Validacao_Complemento_Lote WHERE Retorno = 0)
	--BEGIN
		--**********************************************************************************
		-----CARRREGA DADOS NA TABELA LOTE
		--**********************************************************************************
		SELECT @v_SQL = 'SELECT	(SELECT ISNULL(MAX(Id_Lote),0) FROM LOTE) + ROW_NUMBER() OVER(ORDER BY ' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 7) + ' ASC), ' +
						(SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 7) + ', ' +
						'CONVERT(DATETIME, ' + '''' + @pDt_Lote + '''' + '+' + '''' + '01' + '''' + '), ' +
						'CONVERT(DATETIME,' + '''' + CONVERT(VARCHAR,@pDt_Liberacao_Lote, 110) + '''' + '), ' +
						'0,' +
						'0,' +
						'0,' +
						'0,' +
						'0,' +
						'0,' +
						'0,' +
						'0' +
						ISNULL(@pNumero_Nota_Fiscal, CASE WHEN NOT EXISTS(SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 6) THEN ',NULL ' ELSE ', ' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 6) END)  + ' ' +
						'FROM 	dbo.Cg_Bi_Si_Arquivo_Formatado ' +
						'GROUP BY ' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 7) + 
						CASE WHEN NOT EXISTS(SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 6) THEN ' ' ELSE ', ' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 6) END
		PRINT(@v_SQL)
		INSERT INTO Lote
		EXEC(@v_SQL)

		--**********************************************************************************
		-----CARRREGA DADOS NA LOTE LOG
		--**********************************************************************************
		SELECT @v_SQL = 'SELECT (SELECT ISNULL(MAX(Id_Lote_Log_Criacao),0) FROM Lote_Log_Criacao) + ROW_NUMBER() OVER(ORDER BY Id_Consumidor ASC), ' +
								'(SELECT TOP 1 Id_Lote FROM Lote WHERE Id_Ativo = SQUERY.Id_Ativo AND Dt_Lote = ' + 'CONVERT(DATETIME, ' + '''' + @pDt_Lote + '''' + '+' + '''' + '01' + '''' + ')' + '), ' +
								'SQUERY.Id_Consumidor, ' +
								'SQUERY.Id_Filial, ' +
								'SQUERY.Id_Centro_Custo, ' +
								'SQUERY.Id_Departamento, ' +
								'SQUERY.Id_Setor, ' +
								'SQUERY.Id_Secao ' +
						'FROM (SELECT	DISTINCT	AT.Id_Ativo, ' +
													'CO.Id_Consumidor, ' +
													'CO.Id_Filial, ' +
													'CO.Id_Centro_Custo, ' +
													'CO.Id_Departamento, ' +
													'CO.Id_Setor, ' +
													'CO.Id_Secao ' +
								'FROM 	dbo.Cg_Bi_Si_Arquivo_Formatado AF INNER JOIN vw_Ativo AT ON AT.Id_Ativo = AF.' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 7) + ' ' + 
											'INNER JOIN Rl_Consumidor_Ativo RL ON RL.Id_Ativo = AT.Id_Ativo ' +
											'INNER JOIN vw_Consumidor CO ON CO.Id_Consumidor = RL.Id_Consumidor) SQUERY '
		PRINT(@v_SQL)
		INSERT INTO Lote_Log_Criacao
		EXEC(@v_SQL)		

		--**********************************************************************************
		-----CARRREGA DADOS NA TABELA BILHETE
		--**********************************************************************************
		SELECT @v_SQL = 'SELECT (SELECT ISNULL(MAX(Id_Bilhete),0) FROM Bilhete) + ROW_NUMBER() OVER(ORDER BY ' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 7) + ' ASC), ' +
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 7) THEN 'NULL' ELSE (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 7) END + ', ' +
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 1) THEN 'NULL' ELSE 
								--'CASE WHEN ' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 1) + ' = ' + '''' + '1900/01/01' + '''' + ' THEN ' + 'CONVERT(DATETIME, ' + '''' + '202201' + '''' + '+' + '''' + '01' + '''' + ') ELSE ' +
							'CASE WHEN ' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 1) + ' = ' + '''' + '1900/01/01' + '''' + ' THEN ' + 'getdate() ELSE ' +
								'CONVERT(DATETIME,' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 1) + ') END' END + ', ' +
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 2) THEN 'NULL' ELSE (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 2) END + ', ' +
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 3) THEN 'NULL' ELSE (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 3) END + ', ' +
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 4) THEN '0' ELSE (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 4) END + ', ' +
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 8) THEN 'NULL' ELSE (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 8) END + ', ' +
						'(SELECT TOP 1 Id_Lote FROM Lote WHERE Id_Ativo = AF.' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = 2 AND Id_Campo_Bilhete = 7) +
							CASE WHEN NOT EXISTS(SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 6) THEN ' AND DT_LOTE = ' + 'CONVERT(DATETIME, ' + '''' + @pDt_Lote + '''' + '+' + '''' + '01' + '''' + ')'  + '), ' 
							ELSE ' AND DC_Nr_Nota_Fiscal = AF.' + (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 6) + ' AND DT_LOTE = ' + 'CONVERT(DATETIME, ' + '''' + @pDt_Lote + '''' + '+' + '''' + '01' + '''' + ')'  + '), ' END +
						'NULL, ' + 
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 10) THEN '0' ELSE (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 10) END + ', ' +
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 5) THEN 'NULL' ELSE (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 5) END + ', ' +
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 6) THEN 'NULL' ELSE (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 6) END + ', ' +
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 13) THEN 'NULL' ELSE (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 13) END + ', ' +
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 11) THEN 'NULL' ELSE (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 11) END + ', ' +
						CASE WHEN NOT EXISTS (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 12) THEN '0' ELSE (SELECT Nm_Campo FROM Cg_Bi_Arquivo_Campo WHERE Id_Arquivo = @pId_Arquivo AND Id_Campo_Bilhete = 12) END + ' ' +
						'FROM	Cg_Bi_Si_Arquivo_Formatado AF '
		PRINT(@v_SQL)
		INSERT INTO Bilhete
		EXEC(@v_SQL)

		--**********************************************************************************
		-----CARRREGA DADOS DO INTERVALO DE DATAS PARA CONSULTA AO DESHBOARD
		--**********************************************************************************
		INSERT INTO Bilhete_Data
		SELECT	L.Id_Lote,
				MIN(B.DB_Data_Hora) AS Menor,
				MAX(B.DB_Data_Hora) AS Maior
		FROM	Bilhete B INNER JOIN Lote L ON B.Id_Lote = L.Id_Lote
		WHERE	 B.DB_Custo > 0
				and not exists(select * from Bilhete_Data where Id_Lote = l.Id_Lote)
		GROUP BY L.Id_Lote
		
		--**********************************************************************************
		-----VERIFICA SE EXISTE UM USUARIO E UM LOTE CARREGADO PARA VINCULAR TODAS AS LINHA
		-----NESSE USUARIO (CARGA INICIAL)
		--**********************************************************************************
		IF (SELECT COUNT(*) FROM Consumidor) = 1 AND (SELECT COUNT(DISTINCT Dt_Lote) FROM Lote) = 1
		BEGIN
			-----monta inventario ativo consumidor
			SELECT	DISTINCT	(SELECT TOP 1 Id_Consumidor FROM Consumidor) AS Id_Consumidor,
								Id_Ativo INTO #Inventario_Unico
			FROM	Ativo

			INSERT INTO Rl_Consumidor_Ativo
			SELECT * FROM #Inventario_Unico I
			WHERE	NOT EXISTS(SELECT * FROM Rl_Consumidor_Ativo WHERE Id_Consumidor = I.Id_Consumidor AND Id_Ativo = I.Id_Ativo)
			
			-----monta inventario ativo usuario
			INSERT INTO Rl_Perfil_Ativo_Usuario
			SELECT	RCA.Id_Ativo,
					CONVERT(DATETIME, '2000/01/01'),
					NULL,
					USU.Id_Usuario 
			FROM	Rl_Consumidor_Ativo RCA INNER JOIN Usuario USU ON (RCA.Id_Consumidor = USU.Id_Consumidor)
			WHERE	NOT EXISTS(SELECT * FROM Rl_Perfil_Ativo_Usuario WHERE Id_Ativo = RCA.Id_Ativo AND Id_Usuario = USU.Id_Usuario)
		END
	END