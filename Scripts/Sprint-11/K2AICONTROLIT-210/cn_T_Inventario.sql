SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[cn_T_Inventario] (
    @pPakage                  VARCHAR(200),
    @pPakage_Filtro           VARCHAR(200) = NULL,
    @pId_Filtro_1             VARCHAR(MAX) = NULL,
    @pId_Filtro_Filial        VARCHAR(MAX) = NULL,
    @pId_Filtro_Usuario       INT = NULL,
    @p_Id_Filtro_Centro_Custo VARCHAR(MAX) = NULL,
    @p_Id_Filtro_Departamento VARCHAR(MAX) = NULL,
    @p_Id_Filtro_Setor        VARCHAR(MAX) = NULL,
    @pDt_LoteDe               VARCHAR(6) = NULL,
    @pDt_LoteAte              VARCHAR(6) = NULL,
    @pAtivo_Tipo_Grupo        VARCHAR(50) = NULL
) AS

SELECT
    (SELECT MAX(DC_Nr_Nota_Fiscal) FROM Lote WHERE Id_Ativo = AT.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = AT.Id_Ativo)) AS Conta,
    (SELECT SUBSTRING(CONVERT(VARCHAR(10), MAX(Dt_Lote), 103), 4, 7) FROM Lote WHERE Id_Ativo = AT.Id_Ativo) AS [Ultima_Conta],
    AT.Nr_Ativo AS Ativo_Virtual,
    AT.Nr_Ativo_Tabela AS Ativo,
    Nm_Ativo_Fabricante AS Fabricante,
    Nm_Ativo_Modelo AS Modelo,
    AST.Nm_Ativo_Status AS [Status_Ativo],
    CO.Nm_Conglomerado AS Conglomerado,
    ATP.Nm_Ativo_Tipo AS Tipo,
    CASE
        WHEN Nm_Ativo_Tipo = 'MOVEL DADOS' THEN 'DADOS'
        WHEN Nm_Ativo_Tipo = 'MOVEL VOZ' THEN 'VOZ'
        ELSE NULL
    END AS [Tipo de linha],

    --K2AICONTROLIT-210: Informação quebrada de SIMCARD e IMEI em Inventário de Ativo
	'''' + at.numero_Sim_card AS CHIP,
    
	at.Plano_Contrato AS PLANO,
    at.valor_contrato AS [Valor_contrato],
	(SELECT TOP 1 ENF.Nr_Nota_Fiscal
     FROM Rl_Estoque_Aparelho_Ativo SREA 
     INNER JOIN Estoque_Aparelho SEA ON SREA.Id_Aparelho = SEA.Id_Aparelho
     INNER JOIN Estoque_Nota_Fiscal ENF ON ENF.Id_Estoque_Nota_Fiscal = SEA.Id_Estoque_Nota_Fiscal
     WHERE SREA.Id_Ativo = AT.Id_Ativo AND SEA.Id_Aparelho_Tipo = 1) AS Nota_Fiscal_Apa,

	--K2AICONTROLIT-210: Informação quebrada de SIMCARD e IMEI em Inventário de Ativo
    '''' + (SELECT TOP 1 SEA.Nr_Aparelho
     FROM Rl_Estoque_Aparelho_Ativo SREA 
     INNER JOIN Estoque_Aparelho SEA ON SREA.Id_Aparelho = SEA.Id_Aparelho
    WHERE SREA.Id_Ativo = AT.Id_Ativo AND SEA.Id_Aparelho_Tipo = 1) AS Aparelho,
    
	(SELECT TOP 1 c.Nm_Consumidor
     FROM Rl_Perfil_Ativo_Usuario RLAU
     INNER JOIN usuario U ON u.Id_Usuario = RLAU.Id_Usuario
     INNER JOIN Consumidor C ON c.Id_Consumidor = u.Id_Consumidor
     WHERE Dt_Hr_Desativacao IS NULL AND RLAU.Id_Ativo = AT.Id_Ativo
     ORDER BY Dt_Hr_Ativacao DESC) AS Consumidor,
    (SELECT TOP 1 c.Matricula
     FROM Rl_Perfil_Ativo_Usuario RLAU
     INNER JOIN usuario U ON u.Id_Usuario = RLAU.Id_Usuario
     INNER JOIN Consumidor C ON c.Id_Consumidor = u.Id_Consumidor
     WHERE Dt_Hr_Desativacao IS NULL AND RLAU.Id_Ativo = AT.Id_Ativo
     ORDER BY Dt_Hr_Ativacao DESC) AS Matricula,
    (SELECT TOP 1 u.Nm_Usuario
     FROM Rl_Perfil_Ativo_Usuario RLAU
     INNER JOIN usuario U ON u.Id_Usuario = RLAU.Id_Usuario
     INNER JOIN Consumidor C ON c.Id_Consumidor = u.Id_Consumidor
     WHERE Dt_Hr_Desativacao IS NULL AND RLAU.Id_Ativo = AT.Id_Ativo
     ORDER BY Dt_Hr_Ativacao DESC) AS Usuario,
    (SELECT TOP 1 c.EMail
     FROM Rl_Perfil_Ativo_Usuario RLAU
     INNER JOIN usuario U ON u.Id_Usuario = RLAU.Id_Usuario
     INNER JOIN Consumidor C ON c.Id_Consumidor = u.Id_Consumidor
     WHERE Dt_Hr_Desativacao IS NULL AND RLAU.Id_Ativo = AT.Id_Ativo
     ORDER BY Dt_Hr_Ativacao DESC) AS Email,
    (SELECT TOP 1 cc.Nm_Centro_Custo
     FROM Rl_Perfil_Ativo_Usuario RLAU
     INNER JOIN usuario U ON u.Id_Usuario = RLAU.Id_Usuario
     INNER JOIN Consumidor C ON c.Id_Consumidor = u.Id_Consumidor
     INNER JOIN Centro_Custo cc ON cc.Id_Centro_Custo = c.Id_Centro_Custo
     WHERE Dt_Hr_Desativacao IS NULL AND RLAU.Id_Ativo = AT.Id_Ativo
     ORDER BY Dt_Hr_Ativacao DESC) AS CDC,
    AT.Localidade AS [Data_Suspensao],
    AT.Observacao AS Observacao
INTO #Temp_Export
FROM
    vw_Ativo AT
    INNER JOIN vw_Conglomerado CO ON AT.Id_Conglomerado = CO.Id_Conglomerado
    INNER JOIN vw_Ativo_Tipo ATP ON ATP.Id_Ativo_Tipo = AT.Id_Ativo_Tipo
    INNER JOIN Ativo_Tipo_Grupo ATG ON (ATG.Id_Ativo_Tipo_Grupo = ATP.Id_Ativo_Tipo_Grupo)
    LEFT JOIN Ativo_Status AST ON (AT.Id_Ativo_Status = AST.Id_Ativo_Status)
    LEFT JOIN vw_Ativo_Complemento AC ON (AC.Id_Ativo = AT.Id_Ativo)
    LEFT JOIN vw_Ativo_Modelo AM ON AM.Id_Ativo_Modelo = AT.ID_Ativo_Modelo
    LEFT JOIN vw_Ativo_Fabricante AF ON (AM.Id_Ativo_Fabricante = AF.Id_Ativo_Fabricante)
WHERE
    (ATG.Nm_Ativo_Tipo_Grupo IN (@pAtivo_Tipo_Grupo, 'Fixo_Dados') OR @pAtivo_Tipo_Grupo IS NULL)
    AND NOT AT.Nr_Ativo LIKE '%(%'
ORDER BY Conglomerado

IF @pPakage = 'sp_Inventario_Export'
BEGIN
    SELECT * FROM #Temp_Export
END

IF @pPakage = 'sp_Inventario'
BEGIN
    SELECT * FROM #Temp_Export
END

IF @pPakage = 'sp_Inventario_Grafico'
BEGIN
    SELECT Tipo AS X, COUNT(*) AS Y 
    FROM #Temp_Export
    GROUP BY Tipo 
END
