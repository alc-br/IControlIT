USE [Amil]
GO
/****** Object:  StoredProcedure [dbo].[cn_T_Rateio]    Script Date: 16/03/2024 15:20:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[cn_T_Rateio]
    (
    @pPakage					VARCHAR(200) = 'sp_Rateio',
    @pPakage_Filtro				VARCHAR(200)	= 'Consumidor',
    @pId_Filtro_1				VARCHAR(MAX)	= NULL,
    @pId_Filtro_Filial			VARCHAR(MAX)	= NULL,
    @pId_Filtro_Usuario			INT				= 4636,
    @p_Id_Filtro_Centro_Custo	VARCHAR(MAX)	= NULL,
    @p_Id_Filtro_Departamento	VARCHAR(MAX)	= NULL,
    @p_Id_Filtro_Setor			VARCHAR(MAX)	= NULL,
    @pDt_LoteDe					VARCHAR(6)		= '202312',
    @pDt_LoteAte				VARCHAR(6)		= NULL,
    @pAtivo_Tipo_Grupo			VARCHAR(50)		= 'Telefonia_Movel') AS 
	 
/*
  K2AICONTROLIT - 176
  Repetição de processo - Rateio
  JOAO CARLOS - 25/02/2024
*/

DECLARE @sql NVARCHAR(MAX)

IF @pPakage = 'sp_Rateio_ById' OR @pPakage = 'sp_Rateio_Export_ById' 
BEGIN
    
-- Construindo a consulta dinâmica
	SET @sql = '
		SELECT R.Id_Rateio,
				R.Nm_Rateio,
				eps.Nm_Empresa as Empresa,
				eps.CNPJ as CNPJ,
				AT.Nm_Ativo_Tipo AS Tipo,
				CO.Nm_Conglomerado AS Conglomerado,
				FA.Nr_Fatura AS Conta,
				A.Nr_Ativo_Tabela AS Telefone,
				C.Nm_Consumidor AS Usuario,
				CC.Cd_Centro_Custo AS [Centro de Custo],
				CC.Nm_Centro_Custo AS [Descricao Centro de Custo],
				C.Matricula AS Matricula,
				fpc.Dia_Vencimento as [Dia Vencimento Original],
				CONVERT(VARCHAR(10),FA.Dt_Vencimento,103) AS Vencimento,
				'' '' AS ''Referencia'',
				RI.Valor_Fatura AS [Seriços],
				CAST(RI.Valor_Rateado - RI.Valor_Fatura AS DECIMAL(10,2)) AS Encargos,
				CAST(RI.Valor_Rateado AS DECIMAL(10,2)) AS [Total],
				CAST((SELECT SUM(Valor_Rateado)
					  FROM Rateio_Item
					  WHERE Id_Rateio = RI.Id_Rateio) AS DECIMAL(10,2)) AS [Total Rateio],
				FA.Vr_Fatura AS [Valor Fatura],
				NFF.Nr_Nota_Fiscal AS [Número NF],
				NFF.Pct_Nota_Fiscal AS [Porcentagem NF],
				(CAST(RI.Valor_Rateado AS DECIMAL(10,2)) * NFF.Pct_Nota_Fiscal) / 100 AS [Valor NF]		
		FROM Rateio_Item RI 
		LEFT JOIN vw_Ativo A ON (RI.Id_Ativo = A.Id_Ativo)
		LEFT JOIN Conglomerado CO ON (CO.Id_Conglomerado = A.Id_Conglomerado)
		LEFT JOIN Ativo_Tipo AT ON (AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo)
		LEFT JOIN Ativo_Tipo_Grupo ATG ON (ATG.Id_Ativo_Tipo_Grupo = AT.Id_Ativo_Tipo_Grupo)
		LEFT JOIN vw_Consumidor C ON (RI.Id_Consumidor = C.Id_Consumidor)
		LEFT JOIN vw_Filial F ON (F.Id_Filial = RI.Id_Filial)
		LEFT JOIN vw_Usuario US ON C.Id_Consumidor = US.Id_Consumidor
		LEFT JOIN vw_Centro_Custo CC ON (RI.Id_Centro_Custo = CC.Id_Centro_Custo)
		LEFT JOIN Rl_Rateio_Fatura RF ON (RF.Id_Rateio = RI.Id_Rateio)
		LEFT JOIN Fatura FA ON (RF.Id_Fatura = FA.Id_Fatura)
		LEFT JOIN Rateio R ON R.Id_Rateio = RI.Id_Rateio
		LEFT JOIN Fatura_Plano_Conta FPC on fpc.Nr_Plano_Conta = FA.Nr_Fatura
		LEFT JOIN Empresa eps on eps.Id_Empresa = fpc.Id_Empresa
		LEFT JOIN Nota_Fiscal_Fatura NFF ON NFF.Id_Fatura = FA.Id_Fatura AND NFF.Id_Centro_Custo = CC.Id_Centro_Custo
		WHERE R.Id_Rateio = @pId_Filtro_1
		ORDER BY FA.Nr_Fatura, R.Id_Rateio '

		-- Executando a consulta dinâmica
		EXEC sp_executesql @sql, N'@pId_Filtro_1 VARCHAR(MAX)', @pId_Filtro_1
		
		
END

IF @pPakage = 'sp_Rateio' OR @pPakage = 'sp_Rateio_Export'
BEGIN

-- Construindo a consulta dinâmica
		SET @sql = '
		SELECT R.Id_Rateio,
				R.Nm_Rateio,
				eps.Nm_Empresa as Empresa,
				eps.CNPJ as CNPJ,
				AT.Nm_Ativo_Tipo AS Tipo,
				CO.Nm_Conglomerado AS Conglomerado,
				FA.Nr_Fatura AS Conta,
				A.Nr_Ativo_Tabela AS Telefone,
				C.Nm_Consumidor AS Usuario,
				CC.Cd_Centro_Custo AS [Centro de Custo],
				CC.Nm_Centro_Custo AS [Descricao Centro de Custo],
				C.Matricula AS Matricula,
				fpc.Dia_Vencimento as [Dia Vencimento Original],
				CONVERT(VARCHAR(10),FA.Dt_Vencimento,103) AS Vencimento,
				'' '' AS ''Referencia'',
				RI.Valor_Fatura AS [Seriços],
				CAST(RI.Valor_Rateado - RI.Valor_Fatura AS DECIMAL(10,2)) AS Encargos,
				CAST(RI.Valor_Rateado AS DECIMAL(10,2)) AS [Total],
				CAST((SELECT SUM(Valor_Rateado)
					  FROM Rateio_Item
					  WHERE Id_Rateio = RI.Id_Rateio) AS DECIMAL(10,2)) AS [Total Rateio],
				FA.Vr_Fatura AS [Valor Fatura],
				NFF.Nr_Nota_Fiscal AS [Número NF],
				NFF.Pct_Nota_Fiscal AS [Porcentagem NF],
				(CAST(RI.Valor_Rateado AS DECIMAL(10,2)) * NFF.Pct_Nota_Fiscal) / 100 AS [Valor NF]
		FROM Rateio_Item RI 
		LEFT JOIN vw_Ativo A ON (RI.Id_Ativo = A.Id_Ativo)
		LEFT JOIN Conglomerado CO ON (CO.Id_Conglomerado = A.Id_Conglomerado)
		LEFT JOIN Ativo_Tipo AT ON (AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo)
		LEFT JOIN Ativo_Tipo_Grupo ATG ON (ATG.Id_Ativo_Tipo_Grupo = AT.Id_Ativo_Tipo_Grupo)
		LEFT JOIN vw_Consumidor C ON (RI.Id_Consumidor = C.Id_Consumidor)
		LEFT JOIN vw_Filial F ON (F.Id_Filial = RI.Id_Filial)
		LEFT JOIN vw_Usuario US ON C.Id_Consumidor = US.Id_Consumidor
		LEFT JOIN vw_Centro_Custo CC ON (RI.Id_Centro_Custo = CC.Id_Centro_Custo)
		LEFT JOIN Rl_Rateio_Fatura RF ON (RF.Id_Rateio = RI.Id_Rateio)
		LEFT JOIN Fatura FA ON (RF.Id_Fatura = FA.Id_Fatura)
		LEFT JOIN Rateio R ON R.Id_Rateio = RI.Id_Rateio
		LEFT JOIN Fatura_Plano_Conta FPC on fpc.Nr_Plano_Conta = FA.Nr_Fatura
		LEFT JOIN Empresa eps on eps.Id_Empresa = fpc.Id_Empresa
		LEFT JOIN Nota_Fiscal_Fatura NFF ON NFF.Id_Fatura = FA.Id_Fatura AND NFF.Id_Centro_Custo = CC.Id_Centro_Custo
		WHERE SUBSTRING(CONVERT(VARCHAR(6),FA.Dt_Lote,112),1,6) = @pDt_LoteDe
			AND (ATG.Nm_Ativo_Tipo_Grupo = @pAtivo_Tipo_Grupo OR @pAtivo_Tipo_Grupo IS NULL)
		ORDER BY FA.Nr_Fatura, R.Id_Rateio'

		-- Executando a consulta dinâmica
		EXEC sp_executesql @sql, N'@pDt_LoteDe NVARCHAR(6), @pAtivo_Tipo_Grupo NVARCHAR(50)', @pDt_LoteDe, @pAtivo_Tipo_Grupo    

END
	 
------Exportacao by id
-------exportacao 
--IF @pPakage = 'sp_Rateio_Export_ById' 
--BEGIN
--    SELECT *
--    FROM #Temp_Export
--END 
 
-------grid 
--IF @pPakage = 'sp_Rateio_ById' 
--BEGIN
--    SELECT *
--    FROM #Temp_Export
--END

-----exportacao 
--IF @pPakage = 'sp_Rateio_Export' 
--BEGIN
--    SELECT *
--    FROM #Temp_Export
--END 
 
-------grid 
--IF @pPakage = 'sp_Rateio' 
--BEGIN
--    SELECT *
--    FROM #Temp_Export
--END