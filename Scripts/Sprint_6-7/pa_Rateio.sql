/****** Object:  StoredProcedure [dbo].[pa_Rateio]   
        Script Date: 20/12/2023 07:22:58 
		Alterado por : João Carlos
		Info         : incluir funcionalidade que testa se a fatura possui Ativo, Consumidor
														Centro de Custo, Colaborador
		Task         : K2AICONTROIT-11 [Aviso de erro na execução do Rateio]
		@pPAKAGE = 'sp_Retorna_Fatura_Ativo_Consumidor'
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pa_Rateio]   
    (   
    @pPakage				VARCHAR(200),   
    @pId_Array				VARCHAR(MAX)	= NULL,   
    @pId_Fatura_Tipo_Rateio	VARCHAR(MAX)	= NULL,   
    --Array de tipo de rateio conglomerado, filial ou Ativo     
    @pId_Fatura				VARCHAR(MAX)	= NULL,   
    --Array de fatura para rateio     
    @pNm_Rateio				VARCHAR(50)		= NULL,   
    --Descricao para rateio     
    @pId_Fatura_Parametro	INT				= NULL,   
    --Parametro de fatura     
    @pGrava_Rateio			INT				= NULL,   
    @pData_Lote				VARCHAR(10)		= NULL,   
    --Data do Lote para Rateio     
    @pId_Rateio				INT,   
    @pObservacao			VARCHAR(300)	= NULL)   
AS   
-----variavel de array     
DECLARE @Array VARCHAR(MAX)   
DECLARE @S VARCHAR(20)   
DECLARE @pTArray TABLE (T_Id FLOAT)   
   
-----#############################################################################################################     
-----#############################################################################################################     
-----#############################################################################################################     
-----EXECUCAO RATEIO     
-----#############################################################################################################     
-----#############################################################################################################     
-----#############################################################################################################     
   
-----rateio       
IF @pPAKAGE = 'sp_Rateio'     
BEGIN   
    -------------------------------------------------------------------------------------------------     
    -----declara parametro     
    -------------------------------------------------------------------------------------------------     
    -----cria tabela temporaria para rateio     
    CREATE TABLE #TRateio   
    (   
        Id_Ativo INT,   
        Id_Conglomerado INT,   
        Id_Consumidor INT,   
        Id_Filial INT,   
        Id_Centro_Custo INT,   
        Porcentagem NUMERIC(13,8),   
        Valor_Rateado FLOAT,   
        Valor_Fatura FLOAT,   
        Valor_NF FLOAT   
    )   
   
    CREATE TABLE #TBase_Rateio   
    (   
        Id_Ativo INT,   
        Id_Filial INT,   
        Id_Centro_Custo INT,   
        Id_Conglomerado INT,   
        Id_Consumidor INT   
    )   
   
    -----tabela de fatura     
    DECLARE @pTFatura TABLE (T_Id INT)   
    -----tabela de fatura_tipo_rateio     
    DECLARE @pTFatura_Tipo_Rateio TABLE (T_Id INT)   
    -----tabela de bilhete para desativacao de ativo padraok2     
    DECLARE @pTBilhete_Ativo_Padrao TABLE (T_Id FLOAT)   
    -----variaveis     
    -----variavel armazena ID do ultimo registro salvo na tabela rateio     
    DECLARE @vId_Rateio INT   
    -----variavel de totalizacao de fatura e tarifado     
    DECLARE @vVr_Total_Tarifado NUMERIC(13,7)   
    DECLARE @vVr_Total_Fatura NUMERIC(13,7)   
    -----variavel para armazenar totalizacao original de fatura e tarifado     
    DECLARE @vVr_Original_Total_Tarifado NUMERIC(13,7)   
    DECLARE @vVr_Original_Total_Fatura NUMERIC(13,7)   
    -----variavel para armazenar centro de custo pra onde vao as criticas     
    DECLARE @vId_Centro_Custo_Critica INT   
    -----variavel para armazenar parametro de rateio por nota     
    DECLARE @vVerifica_Nota	INT   
    -----variavel para armazenar parametro de rateio do valor do ativo padrao     
    DECLARE @vRateio_Ativo_Padrao INT   
    -----variavel para armazenar parametro de rateio do valor do ativo padrao     
    DECLARE @vValida_Configuracao INT   
    -----variavel para armazenar valor tarifado do ativo padrao     
    DECLARE @vVr_Total_Tarifado_Ativo_Padrao NUMERIC(13,7)   
    -----variavel para armazenar diferenca do valor que nao tem usuario vinculado     
    DECLARE @vVr_Total_Sem_Ativo NUMERIC(13,7)   
   
    -----inicia variaveis     
    SET @vId_Rateio = 0       SET @vVr_Total_Tarifado = 0   
    SET @vVr_Total_Fatura = 0   
    SET @vId_Centro_Custo_Critica = NULL   
    SET @vVr_Original_Total_Tarifado = 0   
    SET @vVr_Original_Total_Fatura = 0   
    SET @vVerifica_Nota	= 0   
    SET @vRateio_Ativo_Padrao = 0   
    SET @vValida_Configuracao = 0   
    SET @vVr_Total_Tarifado_Ativo_Padrao = 0   
    SET @vVr_Total_Sem_Ativo = 0   
   
    -------------------------------------------------------------------------------------------------     
    -----busca parametro para rateio da tabela fatura parametro     
    -------------------------------------------------------------------------------------------------     
    -----parametro que indica se ira ratear os valore do ativo padrao     
    -----parametro que indica se rateio sera feito por nota     
    SELECT @vRateio_Ativo_Padrao = Rateia_Ativo_Padrao,   
        @vVerifica_Nota = Rateio_Nota   
    FROM Fatura_Parametro   
    WHERE	Id_Fatura_Parametro = @pId_Fatura_Parametro   
   
    -----busca centro custo de critica onde vao entrar os ativos sem usuario     
    SELECT @vId_Centro_Custo_Critica = Id_Centro_Custo   
    FROM Centro_Custo   
    WHERE Cd_Centro_custo = (SELECT Cd_Centro_Custo   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro)   
   
    -----monta array de fatura     
    SELECT @Array = @pId_Fatura   
    IF LEN(@Array) > 0 SET @Array = @Array + ','   
    WHILE LEN(@Array) > 0     
	BEGIN   
        SELECT @S = LTRIM(SUBSTRING(@Array, 1, CHARINDEX(',', @Array) - 1))   
        INSERT INTO @pTFatura   
        VALUES   
            (@S)   
        SELECT @Array = SUBSTRING(@Array, CHARINDEX(',', @Array) + 1, LEN(@Array))   
    END   
   
    -----monta array de fatura_tipo_rateio     
    SELECT @Array = @pId_Fatura_Tipo_Rateio   
    IF LEN(@Array) > 0 SET @Array = @Array + ','   
    WHILE LEN(@Array) > 0     
	BEGIN   
        SELECT @S = LTRIM(SUBSTRING(@Array, 1, CHARINDEX(',', @Array) - 1))   
        INSERT INTO @pTFatura_Tipo_Rateio   
        VALUES   
            (@S)   
        SELECT @Array = SUBSTRING(@Array, CHARINDEX(',', @Array) + 1, LEN(@Array))   
    END   
   
   
   
    -------------------------------------------------------------------------------------------------     
    -----busca valor total das faturas para rateio     
    -------------------------------------------------------------------------------------------------     
    SELECT @vVr_Total_Fatura = ISNULL(SUM(Vr_Fatura),0)   
    FROM Fatura   
    WHERE	Id_Fatura IN (SELECT *   
        FROM @pTFatura)   
        AND NOT Id_Fatura IN (SELECT Id_Fatura   
        FROM Rl_Rateio_Fatura)   
   
    -------------------------------------------------------------------------------------------------     
    -----monta base ok     
    -------------------------------------------------------------------------------------------------     
    -----busca valor total para rateio     
    -----nao pode constar no rateio o ativo de critica da tabela fatura_parametro e conglomerado     
    IF 'sp_Fatura_Tipo_Rateio_Conglomerado' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        -----busca valor total para rateio     
        SELECT @vVr_Total_Tarifado = SUM(VWCU.Total_Consumo)   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND AT.Id_Conglomerado IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura))))   
            AND ((@vId_Centro_Custo_Critica IS NULL AND NOT VWCU.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica IS NULL)   
            AND NOT VWCU.Total_Consumo <= 0   
   
        -----valida e agrupa por centro de custo os ativos para rateio     
        INSERT INTO #TBase_Rateio   
        SELECT AT.Id_Ativo,   
            VWCU.Id_Filial,   
            ISNULL(VWCU.Id_Centro_Custo,@vId_Centro_Custo_Critica) AS Id_Centro_Custo,   
            AT.Id_Conglomerado,   
            VWCU.Id_Consumidor   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (VWCU.Id_Ativo = AT.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND AT.Id_Conglomerado IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura))))   
            AND NOT VWCU.Total_Consumo <= 0   
        GROUP BY	AT.Id_Ativo,VWCU.Id_Filial,VWCU.Id_Centro_Custo,AT.Id_Conglomerado,VWCU.Id_Consumidor   
    END   
   
    IF 'sp_Fatura_Tipo_Rateio_Filial' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        -----busca valor total para rateio     
        SELECT @vVr_Total_Tarifado = SUM(VWCU.Total_Consumo)   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND VWCU.Id_Filial IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND ((@vId_Centro_Custo_Critica IS NULL AND NOT VWCU.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica IS NULL)   
            AND NOT VWCU.Total_Consumo <= 0   
   
        ---valida e agrupa por centro de custo os ativos para rateio     
        INSERT INTO #TBase_Rateio   
        SELECT AT.Id_Ativo,   
            VWCU.Id_Filial,   
            ISNULL(VWCU.Id_Centro_Custo,@vId_Centro_Custo_Critica) AS Id_Centro_Custo,   
            AT.Id_Conglomerado,   
            VWCU.Id_Consumidor   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (VWCU.Id_Ativo = AT.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND VWCU.Id_Filial IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND NOT VWCU.Total_Consumo <= 0   
        GROUP BY AT.Id_Ativo,VWCU.Id_Filial,VWCU.Id_Centro_Custo,AT.Id_Conglomerado,VWCU.Id_Consumidor   
    END   
    IF 'sp_Fatura_Tipo_Rateio_Ativo' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        -----busca valor total para rateio     
        SELECT @vVr_Total_Tarifado = SUM(VWCU.Total_Consumo)   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND AT.Id_Ativo IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
  FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
         FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura))))   
            AND ((@vId_Centro_Custo_Critica IS NULL AND NOT VWCU.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica IS NULL)   
  AND NOT VWCU.Total_Consumo <= 0   
   
        ---valida e agrupa por centro de custo os ativos para rateio     
        INSERT INTO #TBase_Rateio   
        SELECT AT.Id_Ativo,   
            AAC.Id_Filial,   
            ISNULL(AAC.Id_Centro_Custo,@vId_Centro_Custo_Critica) AS Id_Centro_Custo,   
            AT.Id_Conglomerado,   
            AAC.Id_Consumidor   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (VWCU.Id_Ativo  = AT.Id_Ativo)   
            LEFT JOIN vw_Alocacao_Ativo_Consumidor AAC ON (AAC.Id_Ativo = AT.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND AT.Id_Ativo IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura))))   
            AND NOT VWCU.Total_Consumo <= 0   
        GROUP BY	AT.Id_Ativo,AAC.Id_Filial,AAC.Id_Centro_Custo,AT.Id_Conglomerado,AAC.Id_Consumidor   
    END   
    IF 'sp_Fatura_Tipo_Rateio_Tronco_Grupo' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        -----valida e agrupa por centro de custo os ativos para rateio do outro conglomerado     
        SELECT AT.Id_Ativo,   
            BI.Id_Filial,   
            ISNULL(BI.Id_Centro_Custo,@vId_Centro_Custo_Critica) AS Id_Centro_Custo,   
            AT.Id_Conglomerado ,   
            BI.Id_Consumidor,   
            CAST(SUM(BI.Total_Consumo) AS DECIMAL(13,8)) AS Total   
        INTO #TBase_Tronco   
        FROM vw_Custo_Tronco BI INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = BI.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR,BI.Dt_Lote,112),1,6)	= @pData_Lote   
            AND BI.Id_Tronco IN (SELECT Id_Tronco   
            FROM Tronco   
            WHERE Id_Tronco_Grupo IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio))   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT BI.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND ((@vId_Centro_Custo_Critica IS NULL AND NOT BI.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica IS NULL)   
        GROUP BY AT.Id_Ativo,BI.Id_Filial,BI.Id_Centro_Custo,AT.Id_Conglomerado,BI.Id_Consumidor   
   
        -----busca valor total tarifado dos ativos do outro grupo de tronco     
        SELECT @vVr_Total_Tarifado = SUM(AT.Total)   
        FROM #TBase_Tronco AT   
        WHERE	((@vId_Centro_Custo_Critica IS NULL AND NOT AT.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica IS NULL)   
            AND NOT AT.Total <= 0   
    END   
   
    -------------------------------------------------------------------------------------------------     
    -----verificar se valor do ativo padrao sera rateado ou sera colocado em um unico centro de custo     
    -------------------------------------------------------------------------------------------------     
    -----armazena valor original do faturado e tarifado     
    SET @vVr_Original_Total_Tarifado = @vVr_Total_Tarifado   
    SET @vVr_Original_Total_Fatura = @vVr_Total_Fatura   
   
    -----verifica se ira ratear ou nao os valores que entrarao como ativo padrao     
    IF @vRateio_Ativo_Padrao = 2     
	BEGIN   
        -----marca fatura igual ao rateado     
        SET @vVr_Total_Fatura = @vVr_Total_Tarifado   
   
        -----busca valor total tarifado do ativo padrao     
        IF 'sp_Fatura_Tipo_Rateio_Conglomerado' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
		BEGIN   
            SELECT @vVr_Total_Tarifado_Ativo_Padrao = SUM(VWCU.Total_Consumo)   
  FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR,VWCU.Dt_Lote,112),1,6) = @pData_Lote   
                AND AT.Id_Conglomerado IN (SELECT *   
                FROM @pTFatura_Tipo_Rateio)   
                AND AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND (AT.Id_Ativo IN (SELECT ISNULL(Id_Ativo,0)   
                FROM dbo.Rl_Consumidor_Ativo   
                WHERE Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
                AND VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
   
                AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
                FROM Fatura   
                WHERE Id_Fatura IN (SELECT T_Id   
                FROM @pTFatura))))   
        END   
        IF 'sp_Fatura_Tipo_Rateio_Filial' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
		BEGIN   
            SELECT @vVr_Total_Tarifado_Ativo_Padrao = SUM(VWCU.Total_Consumo)   
            FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR,VWCU.Dt_Lote,112),1,6) = @pData_Lote   
                AND VWCU.Id_Filial IN (SELECT *   
                FROM @pTFatura_Tipo_Rateio)   
                AND AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND (AT.Id_Ativo IN (SELECT ISNULL(Id_Ativo,0)   
                FROM dbo.Rl_Consumidor_Ativo   
                WHERE Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
                AND VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
        END   
        IF 'sp_Fatura_Tipo_Rateio_Ativo' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
		BEGIN   
            SELECT @vVr_Total_Tarifado_Ativo_Padrao = SUM(VWCU.Total_Consumo)   
            FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR,VWCU.Dt_Lote,112),1,6) = @pData_Lote   
                AND AT.Id_Ativo IN (SELECT *   
                FROM @pTFatura_Tipo_Rateio)   
                AND AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND (AT.Id_Ativo IN (SELECT ISNULL(Id_Ativo,0)   
                FROM dbo.Rl_Consumidor_Ativo   
                WHERE Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
                AND VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
   
                AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
                FROM Fatura   
                WHERE Id_Fatura IN (SELECT T_Id   
                FROM @pTFatura))))   
        END   
        IF 'sp_Fatura_Tipo_Rateio_Tronco_Grupo' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
		BEGIN   
            SELECT @vVr_Total_Tarifado = SUM(Total_Consumo)   
            FROM vw_Custo_Tronco BI   
            WHERE	SUBSTRING(CONVERT(VARCHAR(6),BI.Dt_Lote,112),1,6) = @pData_Lote   
                AND BI.Id_Tronco IN (SELECT Id_Tronco   
                FROM Tronco   
                WHERE Id_Tronco_Grupo IN (SELECT *   
                FROM @pTFatura_Tipo_Rateio))   
                AND BI.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND BI.Id_Ativo IN (SELECT ISNULL(Id_Ativo,0)   
                FROM dbo.Rl_Consumidor_Ativo   
                WHERE Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
        END   
        -----cria um registro com a diferenca do fatura para o tarifado     
        INSERT INTO #TRateio   
        VALUES( NULL,   
                NULL,   
                NULL,   
                NULL,   
                @vId_Centro_Custo_Critica,   
                0,   
                CONVERT(FLOAT,(@vVr_Original_Total_Fatura - @vVr_Original_Total_Tarifado)),   
                CONVERT(FLOAT,@vVr_Total_Tarifado_Ativo_Padrao),   
                CONVERT(FLOAT,@vVr_Original_Total_Fatura))   
    END   
   
    -------------------------------------------------------------------------------------------------     
    -----realisa rateio com base ok     
    -------------------------------------------------------------------------------------------------     
    IF NOT 'sp_Fatura_Tipo_Rateio_Tronco_Grupo' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        -----não tronco     
        INSERT INTO #TRateio   
        SELECT AT.Id_Ativo,   
            AT.Id_Conglomerado,   
            AT.Id_Consumidor,   
            AT.Id_Filial,   
            AT.Id_Centro_Custo,   
   
            ((VWCU.Total_Consumo / @vVr_Total_Tarifado) * 100) AS Porcentagem,   
            ((@vVr_Total_Fatura * ((VWCU.Total_Consumo / @vVr_Total_Tarifado) * 100)) / 100) AS Valor_Rateado,   
            VWCU.Total_Consumo AS Total_Ativo,   
   
            CONVERT(FLOAT,@vVr_Original_Total_Fatura) AS Valor_Fatura   
        FROM #TBase_Rateio AT INNER JOIN vw_Custo_Usuario VWCU ON (VWCU.Id_Ativo = AT.Id_Ativo   
                AND (VWCU.Id_Consumidor = AT.Id_Consumidor   
                OR AT.Id_Consumidor IS NULL))   
   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND AT.Id_Conglomerado IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura))))   
            AND ((@vId_Centro_Custo_Critica IS NULL AND NOT VWCU.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica IS NULL)   
            AND NOT VWCU.Total_Consumo <= 0   
    END     
	ELSE     
	BEGIN   
        -----tronco     
        INSERT INTO #TRateio   
        SELECT AT.Id_Ativo,   
            AT.Id_Conglomerado,   
            AT.Id_Consumidor,   
            AT.Id_Filial,   
            AT.Id_Centro_Custo,   
            (((AT.Total) / @vVr_Total_Tarifado) * 100) AS Porcentagem,   
            ((@vVr_Total_Fatura * (((AT.Total) / @vVr_Total_Tarifado) * 100)) / 100) AS Valor_Rateado,   
            (AT.Total) AS Total_Ativo,   
            @vVr_Total_Fatura   
        FROM #TBase_Tronco AT   
        WHERE	((@vId_Centro_Custo_Critica IS NULL AND NOT AT.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica IS NULL)   
            AND NOT AT.Total <= 0   
    END   
       -----*************************************************************************************************************     
    -----*************************************************************************************************************	     
    -----realisa 1 configuracao (se existe alguma ativo que seu valor sera rateado para todo conglomerado)     
    IF @vValida_Configuracao = 0     
	BEGIN   
        SELECT AT.Id_Ativo, AT.Id_Conglomerado   
        INTO #TConfig_Rateio_Conglomerado   
        FROM vw_Ativo AT INNER JOIN Ativo_Parametro AP ON (AT.Id_Ativo = AP.Id_Ativo)   
        WHERE	AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM #TBase_Rateio)   
            AND AP.Rateio_Conglomerado = 2   
   
        IF EXISTS (SELECT *   
        FROM #TConfig_Rateio_Conglomerado)     
		BEGIN   
            ---soma ativo fora da opca de confgiuracao     
            SELECT @vVr_Total_Tarifado = SUM(Valor_Rateado)   
            FROM #TRateio   
            WHERE	NOT Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Conglomerado)   
   
            ---soma critica de ativo sem centro de custo para rateio     
            SELECT @vVr_Total_Fatura = SUM(Valor_Rateado)   
            FROM #TRateio   
            WHERE	Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Conglomerado)   
   
            -----delete critica     
            DELETE FROM #TRateio WHERE Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Conglomerado)   
   
            -----realisa rateio com base ok de outro conglomerado     
            INSERT INTO #TRateio   
            SELECT AT.Id_Ativo,   
                AT.Id_Conglomerado,   
                AT.Id_Consumidor,   
                AT.Id_Filial,   
                AT.Id_Centro_Custo,   
                ((AT.Valor_Rateado / @vVr_Total_Tarifado) * 100) AS Porcentagem,   
                ((@vVr_Total_Fatura * ((AT.Valor_Rateado / @vVr_Total_Tarifado) * 100)) / 100) AS Valor_Rateado,   
                AT.Valor_Rateado AS Total_Ativo,   
                @vVr_Total_Fatura   
            FROM #TRateio AT   
   
            SET @vValida_Configuracao = 1   
        END   
    END   
   
    -----*************************************************************************************************************     
    -----*************************************************************************************************************	     
    -----realisa 2 configuracao (se existe alguma ativo que seu valor sera rateado para outros centros de custo)     
    IF @vValida_Configuracao = 0     
	BEGIN   
        SELECT RA.Id_Ativo, RCC.Id_Centro_Custo   
        INTO #TConfig_Rateio_Centro_Custo   
        FROM #TRateio RA INNER JOIN Rl_Ativo_Centro_Custo RCC ON (RA.Id_Ativo = RCC.Id_Ativo)   
        GROUP BY RA.Id_Ativo, RCC.Id_Centro_Custo   
        IF EXISTS (SELECT *   
        FROM #TConfig_Rateio_Centro_Custo)     
		BEGIN   
            ---soma ativo fora da opca de confgiuracao     
            SELECT RCC.Id_Ativo,   
                RA.Id_Conglomerado,   
                RA.Id_Consumidor,   
                RA.Id_Filial,   
                RCC.Id_Centro_Custo,   
                RCC.Porcentagem   
            INTO #TCentro_Custo_Rateio   
            FROM #TRateio RA INNER JOIN Rl_Ativo_Centro_Custo RCC ON (RA.Id_Ativo = RCC.Id_Ativo)   
            GROUP BY	RCC.Id_Ativo,     
						RA.Id_Conglomerado,     
						RA.Id_Consumidor,      
						RA.Id_Filial,     
						RCC.Id_Centro_Custo,      
						RCC.Porcentagem   
   
            ---soma critica de ativo sem centro de custo para rateio     
            SELECT RA.Id_Ativo, SUM(RA.Valor_Rateado) AS Total   
            INTO #TTotal_Ativo   
            FROM #TRateio RA   
            WHERE	Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Centro_Custo)   
            GROUP BY RA.Id_Ativo   
   
            -----delete critica     
            DELETE FROM #TRateio WHERE Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Centro_Custo)   
   
            -----insere rateio de configuracao     
            INSERT INTO #TRateio   
            SELECT CCR.Id_Ativo,   
                CCR.Id_Conglomerado,   
                CCR.Id_Consumidor,   
                CCR.Id_Filial,   
                CCR.Id_Centro_Custo,   
                ISNULL(CCR.Porcentagem,0) AS Porcentagem,   
                CASE WHEN CCR.Porcentagem IS NULL THEN      
						((	SELECT Total   
                FROM #TTotal_Ativo   
                WHERE	Id_Ativo  = CCR.Id_Ativo)     
						/      
						(SELECT COUNT(*)   
                FROM #TCentro_Custo_Rateio   
                WHERE Id_Ativo = CCR.Id_Ativo))     
						ELSE      
						(((	SELECT Total   
                FROM #TTotal_Ativo   
                WHERE	Id_Ativo = CCR.Id_Ativo)     
						*      
						CCR.Porcentagem) / 100) END AS Valor_Rateado,   
                (	SELECT Total   
                FROM #TTotal_Ativo   
                WHERE	Id_Ativo  = CCR.Id_Ativo) AS Total_Ativo,   
                (	SELECT Total   
                FROM #TTotal_Ativo   
                WHERE	Id_Ativo  = CCR.Id_Ativo)   
            FROM #TCentro_Custo_Rateio CCR   
        END   
    END   
   
    -----*************************************************************************************************************     
    -----*************************************************************************************************************	     
    -----realisa 3 configuracao (se existe alguma ativo que seu valor sera rateado para outros conglomerado)     
    ----verifica se outra configuracao ja foi efetuada     
    IF @vValida_Configuracao = 0     
	BEGIN   
        SELECT AP.Id_Ativo, AP.Id_Conglomerado   
        INTO #TConfig_Rateio_Outro_Conglomerado   
        FROM vw_Ativo AT INNER JOIN Ativo_Parametro AP ON (AT.Id_Ativo = AP.Id_Ativo)   
        WHERE	AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM #TBase_Rateio)   
            AND NOT AP.Id_Conglomerado IS NULL   
        IF EXISTS (SELECT *   
        FROM #TConfig_Rateio_Outro_Conglomerado)     
		BEGIN   
            -----busca valor total tarifado dos ativos do outro conglomerado     
            SELECT @vVr_Total_Tarifado = SUM(VWCU.Total_Consumo)   
            FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
                AND AT.Id_Conglomerado IN (SELECT DISTINCT Id_Conglomerado   
                FROM #TConfig_Rateio_Outro_Conglomerado)   
                AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor)--     
                AND ((@vId_Centro_Custo_Critica IS NULL AND NOT VWCU.Id_Centro_Custo IS NULL)   
                OR NOT @vId_Centro_Custo_Critica IS NULL)   
   
            -----valida e agrupa por centro de custo os ativos para rateio do outro conglomerado     
            SELECT AT.Id_Ativo,   
                AAC.Id_Filial,   
                ISNULL(AAC.Id_Centro_Custo,@vId_Centro_Custo_Critica) AS Id_Centro_Custo,   
                AT.Id_Conglomerado,   
                AAC.Id_Consumidor   
            INTO #TBase_Rateio_Outro_Conglomerado   
            FROM vw_Ativo AT LEFT JOIN vw_Alocacao_Ativo_Consumidor AAC ON (AT.Id_Ativo = AAC.Id_Ativo)   
                LEFT JOIN Ativo_Parametro AP ON (AP.Id_Ativo = AT.Id_Ativo)   
            WHERE	AT.Id_Conglomerado IN (SELECT Id_Conglomerado   
                FROM #TConfig_Rateio_Outro_Conglomerado)   
                AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND NOT AAC.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor)--     
            GROUP BY AT.Id_Ativo,AAC.Id_Filial,AAC.Id_Centro_Custo,AT.Id_Conglomerado,AP.Id_Conglomerado, AAC.Id_Consumidor   
   
            -----busca valor total do ativo para para rateio em outro conglomerado     
            SELECT @vVr_Total_Fatura = SUM(Valor_Rateado)   
            FROM #TRateio   
            WHERE	Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Outro_Conglomerado)   
   
            -----delete critica     
            DELETE FROM #TRateio WHERE Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Outro_Conglomerado)   
   
            -----realisa rateio com base ok de outro conglomerado     
            INSERT INTO #TRateio   
            SELECT AT.Id_Ativo,   
                AT.Id_Conglomerado,   
                AT.Id_Consumidor,   
                AT.Id_Filial,   
                AT.Id_Centro_Custo,   
                (((LT.Total /ISNULL(ATCOMP.QTDCOMP,1)) / @vVr_Total_Tarifado) * 100) AS Porcentagem,   
                ((@vVr_Total_Fatura * (((LT.Total /ISNULL(ATCOMP.QTDCOMP,1)) / @vVr_Total_Tarifado) * 100)) / 100) AS Valor_Rateado,   
                (LT.Total /ISNULL(ATCOMP.QTDCOMP,1)) AS Total_Ativo,   
                @vVr_Total_Fatura   
            FROM #TBase_Rateio_Outro_Conglomerado AT LEFT JOIN (SELECT BROC.Id_Ativo, COUNT(*) AS QTDCOMP   
                FROM #TBase_Rateio_Outro_Conglomerado BROC   
                GROUP BY BROC.Id_Ativo   
                HAVING COUNT(*) > 1     
																	) ATCOMP ON (ATCOMP.Id_Ativo = AT.Id_Ativo)   
                INNER JOIN Lote LT ON (AT.Id_Ativo = LT.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR,LT.Dt_Lote,112),1,6) = @pData_Lote   
                AND ((@vId_Centro_Custo_Critica IS NULL AND NOT AT.Id_Centro_Custo IS NULL)   
                OR NOT @vId_Centro_Custo_Critica IS NULL)   
                AND NOT LT.Total <= 0   
   
            SET @vValida_Configuracao = 1   
        END   
    END   
   
    -----*************************************************************************************************************     
    -----*************************************************************************************************************	     
    -----realisa 4 configuracao (se existe alguma ativo que seu valor sera rateado para outros grupos de troncos)     
    ----verifica se outra configuracao ja foi efetuada     
    IF @vValida_Configuracao = 0     
	BEGIN   
        SELECT AT.Id_Ativo, TR.Id_Tronco   
        INTO #TConfig_Rateio_Outro_Tronco_Grupo   
        FROM vw_Ativo AT INNER JOIN Ativo_Parametro AP ON (AT.Id_Ativo = AP.Id_Ativo)   
            INNER JOIN Tronco TR ON (TR.Id_Tronco_Grupo = AP.Id_Tronco_Grupo)   
        WHERE	AT.Id_Ativo IN (SELECT Id_Ativo   
        FROM #TBase_Rateio)   
   
        IF EXISTS (SELECT *   
        FROM #TConfig_Rateio_Outro_Tronco_Grupo)     
		BEGIN   
            -----valida e agrupa por centro de custo os ativos para rateio do outro conglomerado     
            SELECT AT.Id_Ativo,   
                BI.Id_Filial,   
                ISNULL(BI.Id_Centro_Custo,@vId_Centro_Custo_Critica) AS Id_Centro_Custo,   
                AT.Id_Conglomerado,   
                BI.Id_Consumidor,   
                CAST(SUM(BI.Total_Consumo) AS DECIMAL(13,8)) AS Total   
            INTO #TBase_Rateio_Outro_Grupo_Tronco   
            FROM vw_Custo_Tronco BI INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = BI.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR,BI.Dt_Lote,112),1,6)	= @pData_Lote   
                AND BI.Id_Tronco IN (SELECT Id_Tronco   
                FROM #TConfig_Rateio_Outro_Tronco_Grupo)   
                AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND NOT BI.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor)   
                AND ((@vId_Centro_Custo_Critica IS NULL AND NOT BI.Id_Centro_Custo IS NULL)   
                OR NOT @vId_Centro_Custo_Critica IS NULL)   
            GROUP BY AT.Id_Ativo,BI.Id_Filial,BI.Id_Centro_Custo,AT.Id_Conglomerado,BI.Id_Consumidor   
   
            -----busca valor total tarifado dos ativos do outro grupo de tronco     
            SELECT @vVr_Total_Tarifado = SUM(AT.Total)   
            FROM #TBase_Rateio_Outro_Grupo_Tronco AT   
            WHERE	((@vId_Centro_Custo_Critica IS NULL AND NOT AT.Id_Centro_Custo IS NULL)   
                OR NOT @vId_Centro_Custo_Critica IS NULL)   
                AND NOT AT.Total <= 0   
   
            -----busca valor total do ativo para para rateio em outro conglomerado     
            SELECT @vVr_Total_Fatura = SUM(Valor_Rateado)   
            FROM #TRateio   
            WHERE	Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Outro_Tronco_Grupo)   
   
            -----delete critica     
            DELETE FROM #TRateio WHERE Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Outro_Tronco_Grupo)   
   
            -----realisa rateio com base ok de outro conglomerado     
            INSERT INTO #TRateio   
            SELECT AT.Id_Ativo,   
                AT.Id_Conglomerado,   
                AT.Id_Consumidor,   
                AT.Id_Filial,   
                AT.Id_Centro_Custo,   
                ((AT.Total / @vVr_Total_Tarifado) * 100) AS Porcentagem,   
                ((@vVr_Total_Fatura * ((AT.Total / @vVr_Total_Tarifado) * 100)) / 100) AS Valor_Rateado,   
                AT.Total AS Total_Ativo,   
                @vVr_Total_Fatura   
FROM #TBase_Rateio_Outro_Grupo_Tronco AT   
            WHERE	((@vId_Centro_Custo_Critica IS NULL AND NOT AT.Id_Centro_Custo IS NULL)   
                OR NOT @vId_Centro_Custo_Critica IS NULL)   
                AND NOT AT.Total <= 0   
   
            SET @vValida_Configuracao = 1   
        END   
    END   
   
    -----*****************************************************************************************     
    -----retorna rateio     
   
    SELECT ISNULL(CC.Cd_Centro_Custo,'')	AS Cd_Centro_Custo,   
        ISNULL(AT.Nr_Ativo, '')			AS Nr_Ativo,   
        ISNULL(RI.Porcentagem,0)		AS Porcentagem,   
        ISNULL(RI.Valor_Rateado,0)		AS Valor_Rateado,   
        ISNULL(RI.Valor_Fatura,0)		AS Valor_Base,   
        @vId_Rateio						AS Id_Rateio,   
   
        (ISNULL(RI.Valor_Rateado,0) - ISNULL(RI.Valor_Fatura,0)) AS Diferenca,   
   
        ISNULL((SELECT Vr_Fatura   
        FROM Fatura   
        WHERE Id_Fatura = @pId_Fatura),0) AS Total_Fatura,   
   
        ISNULL((SELECT SUM(Valor_Fatura)   
        FROM #TRateio),0) AS Total_Bilhete,   
   
        0 AS Valor_Bilhete,   
   
        ISNULL((SELECT SUM(AC.Valor)   
        FROM Auditoria_Conta AC INNER JOIN Auditoria_Lote AL ON AC.Id_Auditoria_Lote = AL.Id_Auditoria_Lote   
        WHERE AL.Dt_Lote = @pData_Lote   
            AND (@vVerifica_Nota = 1   
            OR (@vVerifica_Nota = 2 AND AC.Conta IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura = @pId_Fatura)))),0)     
			AS Valor_Auditado   
   
    INTO #Temp_Rateio   
   
    FROM #TRateio RI INNER JOIN Centro_Custo CC ON (RI.Id_Centro_Custo = CC.Id_Centro_Custo)   
        INNER JOIN vw_Ativo AT ON (RI.Id_Ativo = AT.Id_Ativo)   
    WHERE						     
		('sp_Fatura_Tipo_Rateio_Conglomerado' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )   
        AND AT.Id_Conglomerado IN (SELECT *   
        FROM @pTFatura_Tipo_Rateio)   
        OR   
        (NOT 'sp_Fatura_Tipo_Rateio_Conglomerado' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro ))     
		)   
   
   
    -----monta observacao do Rateio     
    -----Valor das linhas vagas:0 / Valor de estoque: 0 / Valor de desconto: 0 / Valor da fatura: 0 / Valor contabilizado: 0 / Valor carregado de bilhete: 0 / Valor da auditoria: 0     
    IF @pGrava_Rateio = 2 AND LEN(@pObservacao) < 100     
	BEGIN   
        DECLARE @vObservacao VARCHAR(300)   
        SELECT @vObservacao = @pObservacao +      
				' / Valor da fatura: ' + CONVERT(VARCHAR,CAST(Total_Fatura AS DECIMAL(10,2))) +     
				' / Valor contabilizado: ' + CONVERT(VARCHAR,CAST((SELECT SUM(Valor_Rateado)   
            FROM #Temp_Rateio) AS DECIMAL(10,2))) +     
				' / Valor carregado de bilhete: ' + CONVERT(VARCHAR,CAST(Total_Bilhete AS DECIMAL(10,2))) +     
				' / Valor da auditoria: ' + CONVERT(VARCHAR,CAST(Valor_Auditado AS DECIMAL(10,2)))   
        FROM #Temp_Rateio   
        ORDER BY Diferenca   
    END	     
	ELSE     
	BEGIN   
        SELECT @vObservacao = @pObservacao   
    END   
   
    ---grava rateio historico de rateio se for selecionado     
    IF @pGrava_Rateio = 2     
	BEGIN   
        IF EXISTS(SELECT *   
        FROM #TRateio)     
		BEGIN   
       INSERT INTO Rateio   
            SELECT @pNm_Rateio,   
                GETDATE(),   
                (SELECT SUBSTRING(@pData_Lote,1,4) + '/' + SUBSTRING(@pData_Lote,5,2) + '/01'),   
                @vVr_Original_Total_Fatura,   
                @vVr_Original_Total_Tarifado,   
                @pId_Fatura_Parametro,   
                @vObservacao   
   
            ---------busca id do historico do rateio     
            SELECT @vId_Rateio = MAX(Id_Rateio)   
            FROM Rateio   
   
            -----marca id do rateio nas faturas     
            INSERT INTO	Rl_Rateio_Fatura   
            SELECT @vId_Rateio, T_Id   
            FROM @pTFatura   
   
            -----rateia valor por centro de custo     
            INSERT INTO Rateio_Item   
            SELECT @vId_Rateio, *   
            FROM #TRateio   
      WHERE NOT Valor_Rateado <= 0   
            --Valor_Rateado > 0     
   
            -----grava ativo critica do rateio      
            UPDATE dbo.Rateio_Ativo_Critica SET Id_Rateio = @vId_Rateio WHERE Id_Rateio IS NULL   
        END   
    END   
   
    -----retorno do rateio     
    SELECT *,   
        (SELECT SUM(Valor_Rateado)   
        FROM #Temp_Rateio) AS Total_Rateado   
    FROM #Temp_Rateio   
    WHERE NOT Valor_Rateado <= 0   
    ORDER BY Diferenca   
   
    -----Dispara Email Rateio Realizado     
   
    INSERT INTO Mail_Caixa_Saida   
    VALUES   
        (9,   
            (select email   
            from Consumidor   
            where Nm_Consumidor = 'K2A'),   
            --'teste@icontrolit.com.br',     
            NULL,   
            'Rateio realizada da Operadora ' + (select Nm_fatura   
            from fatura   
            where Id_Fatura = @pId_Fatura)+ ' Lote Mes ' + @pData_Lote,   
            --'teste',     
            --@pId_Rateio,     
            --@pId_Fatura,     
            --'Rateio realizada da Operadora ' + @pTFatura + ' Lote Mes ' + @pData_Lote,     
            --'Rateio realizada da Operadora ' + (select Nm_Rateio   from Rateio where Id_Rateio = @pId_Rateio) + ' Lote Mes ' + @pData_Lote,     
            '2021-01-01',   
            NULL,   
            0,   
            0,   
            0)   
END   
   
   
-----#############################################################################################################     
-----#############################################################################################################     
-----#############################################################################################################     
-----EXECUCAO RATEIO V2    
-----Este pakage é uma versão do sp_Rateio, porém sem validação de @pData_Lote.     
-----O Comportamento esperado é a realização do rateio independente das datas futuras ou passadas.    
-----#############################################################################################################     
-----#############################################################################################################     
-----#############################################################################################################     
   
   
IF @pPAKAGE = 'sp_Rateio_v2'     
BEGIN   
    -------------------------------------------------------------------------------------------------     
    -----declara parametro     
    -------------------------------------------------------------------------------------------------     
    -----cria tabela temporaria para rateio     
    CREATE TABLE #TRateio_   
    (   
        Id_Ativo INT,   
        Id_Conglomerado INT,   
        Id_Consumidor INT,   
        Id_Filial INT,   
        Id_Centro_Custo INT,   
        Porcentagem NUMERIC(13,8),   
        Valor_Rateado FLOAT,   
        Valor_Fatura FLOAT,   
        Valor_NF FLOAT   
    )   
   
    CREATE TABLE #TBase_Rateio_   
    (   
        Id_Ativo INT,   
        Id_Filial INT,   
        Id_Centro_Custo INT,   
        Id_Conglomerado INT,   
        Id_Consumidor INT   
    )   
   
    -----tabela de fatura     
    DECLARE @pTFatura_ TABLE (T_Id INT)   
    -----tabela de fatura_tipo_rateio     
    DECLARE @pTFatura_Tipo_Rateio_ TABLE (T_Id INT)   
    -----tabela de bilhete para desativacao de ativo padraok2     
    DECLARE @pTBilhete_Ativo_Padrao_ TABLE (T_Id FLOAT)   
    -----variaveis     
    -----variavel armazena ID do ultimo registro salvo na tabela rateio     
    DECLARE @vId_Rateio_ INT   
    -----variavel de totalizacao de fatura e tarifado     
    DECLARE @vVr_Total_Tarifado_ NUMERIC(13,7)   
    DECLARE @vVr_Total_Fatura_ NUMERIC(13,7)   
    -----variavel para armazenar totalizacao original de fatura e tarifado     
    DECLARE @vVr_Original_Total_Tarifado_ NUMERIC(13,7)   
    DECLARE @vVr_Original_Total_Fatura_ NUMERIC(13,7)   
    -----variavel para armazenar centro de custo pra onde vao as criticas     
    DECLARE @vId_Centro_Custo_Critica_ INT   
    -----variavel para armazenar parametro de rateio por nota     
    DECLARE @vVerifica_Nota_	INT   
    -----variavel para armazenar parametro de rateio do valor do ativo padrao     
    DECLARE @vRateio_Ativo_Padrao_ INT   
    -----variavel para armazenar parametro de rateio do valor do ativo padrao     
    DECLARE @vValida_Configuracao_ INT   
    -----variavel para armazenar valor tarifado do ativo padrao     
    DECLARE @vVr_Total_Tarifado_Ativo_Padrao_ NUMERIC(13,7)   
    -----variavel para armazenar diferenca do valor que nao tem usuario vinculado     
    DECLARE @vVr_Total_Sem_Ativo_ NUMERIC(13,7)   
   
    -----inicia variaveis     
    SET @vId_Rateio_ = 0   
    SET @vVr_Total_Tarifado_ = 0   
    SET @vVr_Total_Fatura_ = 0   
    SET @vId_Centro_Custo_Critica_ = NULL   
    SET @vVr_Original_Total_Tarifado_ = 0   
    SET @vVr_Original_Total_Fatura_ = 0   
    SET @vVerifica_Nota_	= 0   
    SET @vRateio_Ativo_Padrao_ = 0   
    SET @vValida_Configuracao_ = 0   
    SET @vVr_Total_Tarifado_Ativo_Padrao_ = 0   
    SET @vVr_Total_Sem_Ativo_ = 0   
   
    -------------------------------------------------------------------------------------------------     
    -----busca parametro para rateio da tabela fatura parametro     
    -------------------------------------------------------------------------------------------------     
    -----parametro que indica se ira ratear os valore do ativo padrao     
    -----parametro que indica se rateio sera feito por nota     
    SELECT @vRateio_Ativo_Padrao_ = Rateia_Ativo_Padrao,   
        @vVerifica_Nota_ = Rateio_Nota   
    FROM Fatura_Parametro   
    WHERE	Id_Fatura_Parametro = @pId_Fatura_Parametro   
   
    -----busca centro custo de critica onde vao entrar os ativos sem usuario     
    SELECT @vId_Centro_Custo_Critica_ = Id_Centro_Custo   
    FROM Centro_Custo   
    WHERE Cd_Centro_custo = (SELECT Cd_Centro_Custo   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro)   
   
    -----monta array de fatura     
    SELECT @Array = @pId_Fatura   
    IF LEN(@Array) > 0 SET @Array = @Array + ','   
    WHILE LEN(@Array) > 0     
	BEGIN   
        SELECT @S = LTRIM(SUBSTRING(@Array, 1, CHARINDEX(',', @Array) - 1))   
        INSERT INTO @pTFatura_   
        VALUES   
            (@S)   
        SELECT @Array = SUBSTRING(@Array, CHARINDEX(',', @Array) + 1, LEN(@Array))   
    END   
   
    -----monta array de fatura_tipo_rateio     
    SELECT @Array = @pId_Fatura_Tipo_Rateio   
    IF LEN(@Array) > 0 SET @Array = @Array + ','   
    WHILE LEN(@Array) > 0     
	BEGIN   
        SELECT @S = LTRIM(SUBSTRING(@Array, 1, CHARINDEX(',', @Array) - 1))   
        INSERT INTO @pTFatura_Tipo_Rateio_   
        VALUES   
            (@S)   
        SELECT @Array = SUBSTRING(@Array, CHARINDEX(',', @Array) + 1, LEN(@Array))   
    END   
   
   
   
    -------------------------------------------------------------------------------------------------     
    -----busca valor total das faturas para rateio     
    -------------------------------------------------------------------------------------------------     
    SELECT @vVr_Total_Fatura_ = ISNULL(SUM(Vr_Fatura),0)   
    FROM Fatura   
    WHERE	Id_Fatura IN (SELECT *   
        FROM @pTFatura_)   
        AND NOT Id_Fatura IN (SELECT Id_Fatura   
        FROM Rl_Rateio_Fatura)   
   
    -------------------------------------------------------------------------------------------------     
    -----monta base ok     
    -------------------------------------------------------------------------------------------------     
    -----busca valor total para rateio     
    -----nao pode constar no rateio o ativo de critica da tabela fatura_parametro e conglomerado     
    IF 'sp_Fatura_Tipo_Rateio_Conglomerado' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        -----busca valor total para rateio     
        SELECT @vVr_Total_Tarifado_ = SUM(VWCU.Total_Consumo)   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
        WHERE	/* SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote    
            AND */ AT.Id_Conglomerado IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio_)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota_ = 1 OR ( @vVerifica_Nota_ = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura_))))   
            AND ((@vId_Centro_Custo_Critica_ IS NULL AND NOT VWCU.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
            AND NOT VWCU.Total_Consumo <= 0   
   
        -----valida e agrupa por centro de custo os ativos para rateio     
        INSERT INTO #TBase_Rateio_   
        SELECT AT.Id_Ativo,   
            VWCU.Id_Filial,   
            ISNULL(VWCU.Id_Centro_Custo,@vId_Centro_Custo_Critica_) AS Id_Centro_Custo,   
            AT.Id_Conglomerado,   
            VWCU.Id_Consumidor   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (VWCU.Id_Ativo = AT.Id_Ativo)   
        WHERE	/* SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote    
            AND */ AT.Id_Conglomerado IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio_)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota_ = 1 OR ( @vVerifica_Nota_ = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura_))))   
            AND NOT VWCU.Total_Consumo <= 0   
        GROUP BY	AT.Id_Ativo,VWCU.Id_Filial,VWCU.Id_Centro_Custo,AT.Id_Conglomerado,VWCU.Id_Consumidor   
    END   
   
    IF 'sp_Fatura_Tipo_Rateio_Filial' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        -----busca valor total para rateio     
        SELECT @vVr_Total_Tarifado_ = SUM(VWCU.Total_Consumo)   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND VWCU.Id_Filial IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio_)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND ((@vId_Centro_Custo_Critica_ IS NULL AND NOT VWCU.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
            AND NOT VWCU.Total_Consumo <= 0   
   
        ---valida e agrupa por centro de custo os ativos para rateio     
        INSERT INTO #TBase_Rateio_   
        SELECT AT.Id_Ativo,   
            VWCU.Id_Filial,   
            ISNULL(VWCU.Id_Centro_Custo,@vId_Centro_Custo_Critica_) AS Id_Centro_Custo,   
            AT.Id_Conglomerado,   
            VWCU.Id_Consumidor   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (VWCU.Id_Ativo = AT.Id_Ativo)   
        WHERE	/* SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote    
            AND */ VWCU.Id_Filial IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio_)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND NOT VWCU.Total_Consumo <= 0   
        GROUP BY AT.Id_Ativo,VWCU.Id_Filial,VWCU.Id_Centro_Custo,AT.Id_Conglomerado,VWCU.Id_Consumidor   
    END   
    IF 'sp_Fatura_Tipo_Rateio_Ativo' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        -----busca valor total para rateio     
        SELECT @vVr_Total_Tarifado_ = SUM(VWCU.Total_Consumo)   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND AT.Id_Ativo IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio_)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota_ = 1 OR ( @vVerifica_Nota_ = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura_))))   
            AND ((@vId_Centro_Custo_Critica_ IS NULL AND NOT VWCU.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
            AND NOT VWCU.Total_Consumo <= 0   
   
        ---valida e agrupa por centro de custo os ativos para rateio     
        INSERT INTO #TBase_Rateio_   
        SELECT AT.Id_Ativo,   
            AAC.Id_Filial,   
            ISNULL(AAC.Id_Centro_Custo,@vId_Centro_Custo_Critica_) AS Id_Centro_Custo,   
            AT.Id_Conglomerado,   
            AAC.Id_Consumidor   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (VWCU.Id_Ativo  = AT.Id_Ativo)   
            LEFT JOIN vw_Alocacao_Ativo_Consumidor AAC ON (AAC.Id_Ativo = AT.Id_Ativo)   
        WHERE	/* SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote    
            AND */ AT.Id_Ativo IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio_)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota_ = 1 OR ( @vVerifica_Nota_ = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura_))))   
            AND NOT VWCU.Total_Consumo <= 0   
        GROUP BY	AT.Id_Ativo,AAC.Id_Filial,AAC.Id_Centro_Custo,AT.Id_Conglomerado,AAC.Id_Consumidor   
    END   
    IF 'sp_Fatura_Tipo_Rateio_Tronco_Grupo' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        -----valida e agrupa por centro de custo os ativos para rateio do outro conglomerado     
        SELECT AT.Id_Ativo,   
            BI.Id_Filial,   
            ISNULL(BI.Id_Centro_Custo,@vId_Centro_Custo_Critica_) AS Id_Centro_Custo,   
            AT.Id_Conglomerado ,   
            BI.Id_Consumidor,   
            CAST(SUM(BI.Total_Consumo) AS DECIMAL(13,8)) AS Total   
        INTO #TBase_Tronco_   
        FROM vw_Custo_Tronco BI INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = BI.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR,BI.Dt_Lote,112),1,6)	= @pData_Lote   
            AND BI.Id_Tronco IN (SELECT Id_Tronco   
            FROM Tronco   
            WHERE Id_Tronco_Grupo IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio_))   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT BI.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND ((@vId_Centro_Custo_Critica_ IS NULL AND NOT BI.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
        GROUP BY AT.Id_Ativo,BI.Id_Filial,BI.Id_Centro_Custo,AT.Id_Conglomerado,BI.Id_Consumidor   
   
        -----busca valor total tarifado dos ativos do outro grupo de tronco     
        SELECT @vVr_Total_Tarifado_ = SUM(AT.Total)   
        FROM #TBase_Tronco_ AT   
        WHERE	((@vId_Centro_Custo_Critica_ IS NULL AND NOT AT.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
            AND NOT AT.Total <= 0   
    END   
   
    -------------------------------------------------------------------------------------------------     
    -----verificar se valor do ativo padrao sera rateado ou sera colocado em um unico centro de custo     
 -------------------------------------------------------------------------------------------------     
    -----armazena valor original do faturado e tarifado     
    SET @vVr_Original_Total_Tarifado_ = @vVr_Total_Tarifado_   
    SET @vVr_Original_Total_Fatura_ = @vVr_Total_Fatura_   
   
    -----verifica se ira ratear ou nao os valores que entrarao como ativo padrao     
    IF @vRateio_Ativo_Padrao_ = 2     
	BEGIN   
        -----marca fatura igual ao rateado     
        SET @vVr_Total_Fatura_ = @vVr_Total_Tarifado_   
   
        -----busca valor total tarifado do ativo padrao     
        IF 'sp_Fatura_Tipo_Rateio_Conglomerado' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
		BEGIN   
            SELECT @vVr_Total_Tarifado_Ativo_Padrao_ = SUM(VWCU.Total_Consumo)   
            FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR,VWCU.Dt_Lote,112),1,6) = @pData_Lote   
                AND AT.Id_Conglomerado IN (SELECT *   
                FROM @pTFatura_Tipo_Rateio_)   
                AND AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND (AT.Id_Ativo IN (SELECT ISNULL(Id_Ativo,0)   
                FROM dbo.Rl_Consumidor_Ativo   
                WHERE Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
                AND VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
   
                AND (@vVerifica_Nota_ = 1 OR ( @vVerifica_Nota_ = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
                FROM Fatura   
                WHERE Id_Fatura IN (SELECT T_Id   
                FROM @pTFatura_))))   
        END   
        IF 'sp_Fatura_Tipo_Rateio_Filial' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
		BEGIN   
            SELECT @vVr_Total_Tarifado_Ativo_Padrao_ = SUM(VWCU.Total_Consumo)   
            FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR,VWCU.Dt_Lote,112),1,6) = @pData_Lote   
                AND VWCU.Id_Filial IN (SELECT *   
                FROM @pTFatura_Tipo_Rateio_)   
                AND AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND (AT.Id_Ativo IN (SELECT ISNULL(Id_Ativo,0)   
                FROM dbo.Rl_Consumidor_Ativo   
                WHERE Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
                AND VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
        END   
        IF 'sp_Fatura_Tipo_Rateio_Ativo' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
		BEGIN   
            SELECT @vVr_Total_Tarifado_Ativo_Padrao_ = SUM(VWCU.Total_Consumo)   
            FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR,VWCU.Dt_Lote,112),1,6) = @pData_Lote   
                AND AT.Id_Ativo IN (SELECT *   
                FROM @pTFatura_Tipo_Rateio_)   
                AND AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND (AT.Id_Ativo IN (SELECT ISNULL(Id_Ativo,0)   
                FROM dbo.Rl_Consumidor_Ativo   
                WHERE Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
                AND VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
   
                AND (@vVerifica_Nota_ = 1 OR ( @vVerifica_Nota_ = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
                FROM Fatura   
                WHERE Id_Fatura IN (SELECT T_Id   
                FROM @pTFatura_))))   
        END   
        IF 'sp_Fatura_Tipo_Rateio_Tronco_Grupo' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
		BEGIN   
            SELECT @vVr_Total_Tarifado_ = SUM(Total_Consumo)   
            FROM vw_Custo_Tronco BI   
            WHERE	SUBSTRING(CONVERT(VARCHAR(6),BI.Dt_Lote,112),1,6) = @pData_Lote   
                AND BI.Id_Tronco IN (SELECT Id_Tronco   
                FROM Tronco   
                WHERE Id_Tronco_Grupo IN (SELECT *   
                FROM @pTFatura_Tipo_Rateio_))   
                AND BI.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND BI.Id_Ativo IN (SELECT ISNULL(Id_Ativo,0)   
                FROM dbo.Rl_Consumidor_Ativo   
                WHERE Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor))   
        END   
        -----cria um registro com a diferenca do fatura para o tarifado     
        INSERT INTO #TRateio_   
        VALUES( NULL,   
                NULL,   
                NULL,   
                NULL,   
                @vId_Centro_Custo_Critica_,   
                0,   
                CONVERT(FLOAT,(@vVr_Original_Total_Fatura_ - @vVr_Original_Total_Tarifado_)),   
                CONVERT(FLOAT,@vVr_Total_Tarifado_Ativo_Padrao_),   
                CONVERT(FLOAT,@vVr_Original_Total_Fatura_))   
    END   
   
    -------------------------------------------------------------------------------------------------     
    -----realisa rateio com base ok     
   -------------------------------------------------------------------------------------------------     
    IF NOT 'sp_Fatura_Tipo_Rateio_Tronco_Grupo' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        -----não tronco     
        INSERT INTO #TRateio_   
        SELECT AT.Id_Ativo,   
            AT.Id_Conglomerado,   
            AT.Id_Consumidor,   
            AT.Id_Filial,   
            AT.Id_Centro_Custo,   
   
            ((VWCU.Total_Consumo / @vVr_Total_Tarifado_) * 100) AS Porcentagem,   
            ((@vVr_Total_Fatura_ * ((VWCU.Total_Consumo / @vVr_Total_Tarifado_) * 100)) / 100) AS Valor_Rateado,   
            VWCU.Total_Consumo AS Total_Ativo,   
   
            CONVERT(FLOAT,@vVr_Original_Total_Fatura_) AS Valor_Fatura   
        FROM #TBase_Rateio_ AT INNER JOIN vw_Custo_Usuario VWCU ON (VWCU.Id_Ativo = AT.Id_Ativo   
                AND (VWCU.Id_Consumidor = AT.Id_Consumidor   
                OR AT.Id_Consumidor IS NULL))   
   
        WHERE	/* SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote    
            AND */ AT.Id_Conglomerado IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio_)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota_ = 1 OR ( @vVerifica_Nota_ = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura_))))   
            AND ((@vId_Centro_Custo_Critica_ IS NULL AND NOT VWCU.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
            AND NOT VWCU.Total_Consumo <= 0   
    END     
	ELSE     
	BEGIN   
        -----tronco     
        INSERT INTO #TRateio_   
        SELECT AT.Id_Ativo,   
            AT.Id_Conglomerado,   
            AT.Id_Consumidor,   
            AT.Id_Filial,   
            AT.Id_Centro_Custo,   
            (((AT.Total) / @vVr_Total_Tarifado_) * 100) AS Porcentagem,   
            ((@vVr_Total_Fatura_ * (((AT.Total) / @vVr_Total_Tarifado_) * 100)) / 100) AS Valor_Rateado,   
            (AT.Total) AS Total_Ativo,   
            @vVr_Total_Fatura_   
        FROM #TBase_Tronco_ AT   
        WHERE	((@vId_Centro_Custo_Critica_ IS NULL AND NOT AT.Id_Centro_Custo IS NULL)   
            OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
            AND NOT AT.Total <= 0   
    END   
   
    -----*************************************************************************************************************     
    -----*************************************************************************************************************	     
    -----realisa 1 configuracao (se existe alguma ativo que seu valor sera rateado para todo conglomerado)     
    IF @vValida_Configuracao_ = 0     
	BEGIN   
        SELECT AT.Id_Ativo, AT.Id_Conglomerado   
        INTO #TConfig_Rateio_Conglomerado_   
        FROM vw_Ativo AT INNER JOIN Ativo_Parametro AP ON (AT.Id_Ativo = AP.Id_Ativo)   
        WHERE	AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM #TBase_Rateio_)   
            AND AP.Rateio_Conglomerado = 2   
   
        IF EXISTS (SELECT *   
        FROM #TConfig_Rateio_Conglomerado_)     
		BEGIN   
            ---soma ativo fora da opca de confgiuracao     
            SELECT @vVr_Total_Tarifado_ = SUM(Valor_Rateado)   
            FROM #TRateio_   
            WHERE	NOT Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Conglomerado_)   
   
            ---soma critica de ativo sem centro de custo para rateio     
            SELECT @vVr_Total_Fatura_ = SUM(Valor_Rateado)   
            FROM #TRateio_   
            WHERE	Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Conglomerado_)   
   
            -----delete critica     
            DELETE FROM #TRateio_ WHERE Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Conglomerado_)   
   
            -----realisa rateio com base ok de outro conglomerado     
            INSERT INTO #TRateio_   
            SELECT AT.Id_Ativo,   
                AT.Id_Conglomerado,   
                AT.Id_Consumidor,   
                AT.Id_Filial,   
                AT.Id_Centro_Custo,   
                ((AT.Valor_Rateado / @vVr_Total_Tarifado_) * 100) AS Porcentagem,   
                ((@vVr_Total_Fatura_ * ((AT.Valor_Rateado / @vVr_Total_Tarifado_) * 100)) / 100) AS Valor_Rateado,   
                AT.Valor_Rateado AS Total_Ativo,   
                @vVr_Total_Fatura_   
            FROM #TRateio_ AT   
   
            SET @vValida_Configuracao_ = 1   
        END   
    END   
   
    -----*************************************************************************************************************     
    -----*************************************************************************************************************	     
    -----realisa 2 configuracao (se existe alguma ativo que seu valor sera rateado para outros centros de custo)     
    IF @vValida_Configuracao_ = 0     
	BEGIN   
        SELECT RA.Id_Ativo, RCC.Id_Centro_Custo   
        INTO #TConfig_Rateio_Centro_Custo_   
        FROM #TRateio_ RA INNER JOIN Rl_Ativo_Centro_Custo RCC ON (RA.Id_Ativo = RCC.Id_Ativo)   
        GROUP BY RA.Id_Ativo, RCC.Id_Centro_Custo   
        IF EXISTS (SELECT *   
        FROM #TConfig_Rateio_Centro_Custo_)     
		BEGIN   
            ---soma ativo fora da opca de confgiuracao     
            SELECT RCC.Id_Ativo,   
                RA.Id_Conglomerado,   
                RA.Id_Consumidor,   
                RA.Id_Filial,   
                RCC.Id_Centro_Custo,   
                RCC.Porcentagem   
            INTO #TCentro_Custo_Rateio_   
            FROM #TRateio_ RA INNER JOIN Rl_Ativo_Centro_Custo RCC ON (RA.Id_Ativo = RCC.Id_Ativo)   
            GROUP BY	RCC.Id_Ativo,     
						RA.Id_Conglomerado,     
						RA.Id_Consumidor,      
						RA.Id_Filial,     
						RCC.Id_Centro_Custo,      
						RCC.Porcentagem   
   
            ---soma critica de ativo sem centro de custo para rateio     
            SELECT RA.Id_Ativo, SUM(RA.Valor_Rateado) AS Total   
            INTO #TTotal_Ativo_   
            FROM #TRateio_ RA   
            WHERE	Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Centro_Custo_)   
            GROUP BY RA.Id_Ativo   
   
            -----delete critica     
            DELETE FROM #TRateio_ WHERE Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Centro_Custo_)   
   
            -----insere rateio de configuracao     
            INSERT INTO #TRateio_   
            SELECT CCR.Id_Ativo,   
  CCR.Id_Conglomerado,   
                CCR.Id_Consumidor,   
                CCR.Id_Filial,   
                CCR.Id_Centro_Custo,   
                ISNULL(CCR.Porcentagem,0) AS Porcentagem,   
                CASE WHEN CCR.Porcentagem IS NULL THEN      
						((	SELECT Total   
                FROM #TTotal_Ativo_   
                WHERE	Id_Ativo  = CCR.Id_Ativo)     
						/      
						(SELECT COUNT(*)   
                FROM #TCentro_Custo_Rateio_   
                WHERE Id_Ativo = CCR.Id_Ativo))     
						ELSE      
						(((	SELECT Total   
                FROM #TTotal_Ativo_   
                WHERE	Id_Ativo = CCR.Id_Ativo)     
						*      
						CCR.Porcentagem) / 100) END AS Valor_Rateado,   
                (	SELECT Total   
                FROM #TTotal_Ativo_   
                WHERE	Id_Ativo  = CCR.Id_Ativo) AS Total_Ativo,   
                (	SELECT Total   
                FROM #TTotal_Ativo_   
                WHERE	Id_Ativo  = CCR.Id_Ativo)   
            FROM #TCentro_Custo_Rateio_ CCR   
        END   
    END   
   
    -----*************************************************************************************************************     
    -----*************************************************************************************************************	     
    -----realisa 3 configuracao (se existe alguma ativo que seu valor sera rateado para outros conglomerado)     
    ----verifica se outra configuracao ja foi efetuada     
    IF @vValida_Configuracao_ = 0     
	BEGIN   
        SELECT AP.Id_Ativo, AP.Id_Conglomerado   
        INTO #TConfig_Rateio_Outro_Conglomerado_   
        FROM vw_Ativo AT INNER JOIN Ativo_Parametro AP ON (AT.Id_Ativo = AP.Id_Ativo)   
        WHERE	AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM #TBase_Rateio_)   
            AND NOT AP.Id_Conglomerado IS NULL   
        IF EXISTS (SELECT *   
        FROM #TConfig_Rateio_Outro_Conglomerado_)     
		BEGIN   
            -----busca valor total tarifado dos ativos do outro conglomerado     
            SELECT @vVr_Total_Tarifado_ = SUM(VWCU.Total_Consumo)   
            FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = VWCU.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
                AND AT.Id_Conglomerado IN (SELECT DISTINCT Id_Conglomerado   
                FROM #TConfig_Rateio_Outro_Conglomerado_)   
                AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor)--     
          AND ((@vId_Centro_Custo_Critica_ IS NULL AND NOT VWCU.Id_Centro_Custo IS NULL)   
                OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
   
            -----valida e agrupa por centro de custo os ativos para rateio do outro conglomerado     
            SELECT AT.Id_Ativo,   
                AAC.Id_Filial,   
                ISNULL(AAC.Id_Centro_Custo,@vId_Centro_Custo_Critica_) AS Id_Centro_Custo,   
                AT.Id_Conglomerado,   
                AAC.Id_Consumidor   
            INTO #TBase_Rateio_Outro_Conglomerado_   
            FROM vw_Ativo AT LEFT JOIN vw_Alocacao_Ativo_Consumidor AAC ON (AT.Id_Ativo = AAC.Id_Ativo)   
                LEFT JOIN Ativo_Parametro AP ON (AP.Id_Ativo = AT.Id_Ativo)   
            WHERE	AT.Id_Conglomerado IN (SELECT Id_Conglomerado   
                FROM #TConfig_Rateio_Outro_Conglomerado_)   
                AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND NOT AAC.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor)--     
            GROUP BY AT.Id_Ativo,AAC.Id_Filial,AAC.Id_Centro_Custo,AT.Id_Conglomerado,AP.Id_Conglomerado, AAC.Id_Consumidor   
   
            -----busca valor total do ativo para para rateio em outro conglomerado     
            SELECT @vVr_Total_Fatura_ = SUM(Valor_Rateado)   
            FROM #TRateio_   
            WHERE	Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Outro_Conglomerado_)   
   
            -----delete critica     
            DELETE FROM #TRateio_ WHERE Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Outro_Conglomerado_)   
   
            -----realisa rateio com base ok de outro conglomerado     
            INSERT INTO #TRateio_   
            SELECT AT.Id_Ativo,   
                AT.Id_Conglomerado,   
                AT.Id_Consumidor,   
                AT.Id_Filial,   
                AT.Id_Centro_Custo,   
                (((LT.Total /ISNULL(ATCOMP.QTDCOMP,1)) / @vVr_Total_Tarifado_) * 100) AS Porcentagem,   
                ((@vVr_Total_Fatura_ * (((LT.Total /ISNULL(ATCOMP.QTDCOMP,1)) / @vVr_Total_Tarifado_) * 100)) / 100) AS Valor_Rateado,   
                (LT.Total /ISNULL(ATCOMP.QTDCOMP,1)) AS Total_Ativo,   
                @vVr_Total_Fatura_   
            FROM #TBase_Rateio_Outro_Conglomerado_ AT LEFT JOIN (SELECT BROC.Id_Ativo, COUNT(*) AS QTDCOMP   
                FROM #TBase_Rateio_Outro_Conglomerado_ BROC   
   GROUP BY BROC.Id_Ativo   
                HAVING COUNT(*) > 1     
																	) ATCOMP ON (ATCOMP.Id_Ativo = AT.Id_Ativo)   
                INNER JOIN Lote LT ON (AT.Id_Ativo = LT.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR,LT.Dt_Lote,112),1,6) = @pData_Lote   
                AND ((@vId_Centro_Custo_Critica_ IS NULL AND NOT AT.Id_Centro_Custo IS NULL)   
                OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
                AND NOT LT.Total <= 0   
   
            SET @vValida_Configuracao_ = 1   
        END   
    END   
   
    -----*************************************************************************************************************     
    -----*************************************************************************************************************	     
    -----realisa 4 configuracao (se existe alguma ativo que seu valor sera rateado para outros grupos de troncos)     
    ----verifica se outra configuracao ja foi efetuada     
    IF @vValida_Configuracao_ = 0     
	BEGIN   
        SELECT AT.Id_Ativo, TR.Id_Tronco   
        INTO #TConfig_Rateio_Outro_Tronco_Grupo_   
        FROM vw_Ativo AT INNER JOIN Ativo_Parametro AP ON (AT.Id_Ativo = AP.Id_Ativo)   
            INNER JOIN Tronco TR ON (TR.Id_Tronco_Grupo = AP.Id_Tronco_Grupo)   
        WHERE	AT.Id_Ativo IN (SELECT Id_Ativo   
        FROM #TBase_Rateio_)   
   
        IF EXISTS (SELECT *   
        FROM #TConfig_Rateio_Outro_Tronco_Grupo_)     
		BEGIN   
            -----valida e agrupa por centro de custo os ativos para rateio do outro conglomerado     
            SELECT AT.Id_Ativo,   
                BI.Id_Filial,   
                ISNULL(BI.Id_Centro_Custo,@vId_Centro_Custo_Critica_) AS Id_Centro_Custo,   
                AT.Id_Conglomerado,   
                BI.Id_Consumidor,   
                CAST(SUM(BI.Total_Consumo) AS DECIMAL(13,8)) AS Total   
            INTO #TBase_Rateio_Outro_Grupo_Tronco_   
            FROM vw_Custo_Tronco BI INNER JOIN vw_Ativo AT ON (AT.Id_Ativo = BI.Id_Ativo)   
            WHERE	SUBSTRING(CONVERT(VARCHAR,BI.Dt_Lote,112),1,6)	= @pData_Lote   
                AND BI.Id_Tronco IN (SELECT Id_Tronco   
                FROM #TConfig_Rateio_Outro_Tronco_Grupo_)   
                AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
                FROM Fatura_Parametro)   
                AND NOT BI.Id_Consumidor IN (SELECT Id_Consumidor   
                FROM Estoque_Consumidor)   
                AND ((@vId_Centro_Custo_Critica_ IS NULL AND NOT BI.Id_Centro_Custo IS NULL)   
                OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
            GROUP BY AT.Id_Ativo,BI.Id_Filial,BI.Id_Centro_Custo,AT.Id_Conglomerado,BI.Id_Consumidor   
   
            -----busca valor total tarifado dos ativos do outro grupo de tronco     
            SELECT @vVr_Total_Tarifado_ = SUM(AT.Total)   
            FROM #TBase_Rateio_Outro_Grupo_Tronco_ AT   
            WHERE	((@vId_Centro_Custo_Critica_ IS NULL AND NOT AT.Id_Centro_Custo IS NULL)   
                OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
                AND NOT AT.Total <= 0   
   
 -----busca valor total do ativo para para rateio em outro conglomerado     
            SELECT @vVr_Total_Fatura_ = SUM(Valor_Rateado)   
            FROM #TRateio_   
            WHERE	Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Outro_Tronco_Grupo_)   
   
            -----delete critica     
            DELETE FROM #TRateio_ WHERE Id_Ativo IN (SELECT Id_Ativo   
            FROM #TConfig_Rateio_Outro_Tronco_Grupo_)   
   
            -----realisa rateio com base ok de outro conglomerado     
            INSERT INTO #TRateio_   
            SELECT AT.Id_Ativo,   
                AT.Id_Conglomerado,   
                AT.Id_Consumidor,   
                AT.Id_Filial,   
                AT.Id_Centro_Custo,   
                ((AT.Total / @vVr_Total_Tarifado_) * 100) AS Porcentagem,   
                ((@vVr_Total_Fatura_ * ((AT.Total / @vVr_Total_Tarifado_) * 100)) / 100) AS Valor_Rateado,   
                AT.Total AS Total_Ativo,   
                @vVr_Total_Fatura_   
            FROM #TBase_Rateio_Outro_Grupo_Tronco_ AT   
            WHERE	((@vId_Centro_Custo_Critica_ IS NULL AND NOT AT.Id_Centro_Custo IS NULL)   
                OR NOT @vId_Centro_Custo_Critica_ IS NULL)   
                AND NOT AT.Total <= 0   
   
            SET @vValida_Configuracao_ = 1   
        END   
    END   
   
    -----*****************************************************************************************     
    -----retorna rateio     
   
    SELECT ISNULL(CC.Cd_Centro_Custo,'')	AS Cd_Centro_Custo,   
        ISNULL(AT.Nr_Ativo, '')			AS Nr_Ativo,   
        ISNULL(RI.Porcentagem,0)		AS Porcentagem,   
        ISNULL(RI.Valor_Rateado,0)		AS Valor_Rateado,   
        ISNULL(RI.Valor_Fatura,0)		AS Valor_Base,   
        @vId_Rateio_						AS Id_Rateio,   
   
        (ISNULL(RI.Valor_Rateado,0) - ISNULL(RI.Valor_Fatura,0)) AS Diferenca,   
   
        ISNULL((SELECT Vr_Fatura   
        FROM Fatura   
        WHERE Id_Fatura = @pId_Fatura),0) AS Total_Fatura,   
   
        ISNULL((SELECT SUM(Valor_Fatura)   
        FROM #TRateio_),0) AS Total_Bilhete,   
   
        0 AS Valor_Bilhete,   
   
        ISNULL((SELECT SUM(AC.Valor)   
        FROM Auditoria_Conta AC INNER JOIN Auditoria_Lote AL ON AC.Id_Auditoria_Lote = AL.Id_Auditoria_Lote   
        WHERE /* AL.Dt_Lote = @pData_Lote    
            AND */ (@vVerifica_Nota_ = 1   
            OR (@vVerifica_Nota_ = 2 AND AC.Conta IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura = @pId_Fatura)))),0)     
			AS Valor_Auditado   
   
    INTO #Temp_Rateio_   
   
    FROM #TRateio_ RI INNER JOIN Centro_Custo CC ON (RI.Id_Centro_Custo = CC.Id_Centro_Custo)   
        INNER JOIN vw_Ativo AT ON (RI.Id_Ativo = AT.Id_Ativo)   
    WHERE						     
		('sp_Fatura_Tipo_Rateio_Conglomerado' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )   
        AND AT.Id_Conglomerado IN (SELECT *   
        FROM @pTFatura_Tipo_Rateio_)   
        OR   
        (NOT 'sp_Fatura_Tipo_Rateio_Conglomerado' = (SELECT Id_Fatura_Tipo_Rateio   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro ))     
		)   
   
   
    -----monta observacao do Rateio  
    -----Valor das linhas vagas:0 / Valor de estoque: 0 / Valor de desconto: 0 / Valor da fatura: 0 / Valor contabilizado: 0 / Valor carregado de bilhete: 0 / Valor da auditoria: 0  
    IF @pGrava_Rateio = 2 AND LEN(@pObservacao) < 100  
	BEGIN 
        DECLARE @vObservacao_ VARCHAR(300) 
        SELECT @vObservacao_ = @pObservacao +   
				' / Valor da fatura: ' + CONVERT(VARCHAR,CAST(Total_Fatura AS DECIMAL(10,2))) +  
				' / Valor contabilizado: ' + CONVERT(VARCHAR,CAST((SELECT SUM(Valor_Rateado) 
            FROM #Temp_Rateio_) AS DECIMAL(10,2))) +  
				' / Valor carregado de bilhete: ' + CONVERT(VARCHAR,CAST(Total_Bilhete AS DECIMAL(10,2))) +  
				' / Valor da auditoria: ' + CONVERT(VARCHAR,CAST(Valor_Auditado AS DECIMAL(10,2))) 
        FROM #Temp_Rateio_ 
        ORDER BY Diferenca 
    END	  
	ELSE  
	BEGIN 
        SELECT @vObservacao_ = @pObservacao 
    END 
   
    ---grava rateio historico de rateio se for selecionado     
    IF @pGrava_Rateio = 2     
	BEGIN   
        IF EXISTS(SELECT *   
        FROM #TRateio_)     
		BEGIN   
            INSERT INTO Rateio   
            SELECT @pNm_Rateio,   
                GETDATE(),   
                (SELECT SUBSTRING(@pData_Lote,1,4) + '/' + SUBSTRING(@pData_Lote,5,2) + '/01'),   
                @vVr_Original_Total_Fatura_,   
                @vVr_Original_Total_Tarifado_,   
                @pId_Fatura_Parametro,   
                @vObservacao_   
   
            ---------busca id do historico do rateio     
            SELECT @vId_Rateio_ = MAX(Id_Rateio)   
            FROM Rateio   
   
            -----marca id do rateio nas faturas     
            INSERT INTO	Rl_Rateio_Fatura   
            SELECT @vId_Rateio_, T_Id   
            FROM @pTFatura_   
   
            -----rateia valor por centro de custo     
            -- INSERT INTO Rateio_Item   
            -- SELECT @vId_Rateio_, *   
            -- FROM #TRateio_   
            -- WHERE NOT Valor_Rateado <= 0   
            --Valor_Rateado > 0     
   
            SELECT   
                f.Id_Fatura,   
                f.Nr_Fatura,   
                f.Dt_Lote,   
                f.Vr_Fatura,   
                r.Id_Rateio   
            INTO #fatura   
            FROM Fatura AS F   
                INNER JOIN Rl_Rateio_Fatura rl ON rl.Id_Fatura = f.Id_Fatura   
                INNER JOIN rateio R ON r.Id_Rateio = rl.Id_Rateio   
            WHERE R.Id_Rateio IN (SELECT Id_Rateio   
            FROM Rateio   
   
            WHERE SUBSTRING(CONVERT(VARCHAR(6),Dt_lote,112),1,6) = @pData_Lote )   
   
--------------------------------------------------------------------------------------------------------------------   
--------------------------------------------------------------------------------------------------------------------   
   
            DELETE Rateio_Item WHERE Id_Rateio IN (SELECT id_rateio FROM #fatura)   
   
--------------------------------------------------------------------------------------------------------------------   
--------------------------------------------------------------------------------------------------------------------   
   
            SELECT   
                f.Id_Rateio											AS Id_Rateio,   
                AT.Id_Ativo											AS ID_Ativo,   
                CG.Id_Conglomerado									AS id_Conglomerado,   
                CO.id_Consumidor									AS Id_Consumidor,   
                FI.id_Filial										AS Id_Filial,   
                CC.id_Centro_Custo									AS Id_Centro_Custo,   
                ISNULL(CAST(LT.Total_Lote AS DECIMAL(10,2)),0)		AS Valor_Fatura,   
                f.Vr_Fatura											AS Total_Conta   
            INTO #Temp_Export   
            FROM vw_Custo_Usuario LT INNER JOIN Ativo AT ON (AT.Id_Ativo = LT.Id_Ativo)   
                INNER JOIN Conglomerado CG ON (CG.Id_Conglomerado = AT.Id_Conglomerado)   
                LEFT JOIN Consumidor CO ON (CO.Id_Consumidor = LT.Id_Consumidor)   
                LEFT JOIN Filial FI ON (FI.Id_Filial = CO.Id_Filial)   
                LEFT JOIN Centro_Custo CC ON (CC.Id_Centro_Custo = CO.Id_Centro_Custo)   
                INNER JOIN #fatura F ON f.Nr_Fatura = LT.DC_Nr_Nota_Fiscal   
            --WHERE SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) = @pData_Lote   
            --    AND AT.Id_Ativo NOT IN (SELECT Id_Ativo FROM Fatura_Parametro)   
            --    AND CO.Id_Consumidor NOT IN (SELECT Id_Consumidor FROM Estoque_Consumidor)   
            --    AND LT.Total_Lote <> 0 AND LT.Total_Lote <> 0.01   
			where SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) = @pData_Lote    
				and AT.Id_Ativo not in (select Id_Ativo from Fatura_Parametro)   
				and CO.Id_Consumidor  not in (select Id_Consumidor from Estoque_Consumidor)    
				and LT.Total_Lote <> 0     
				and LT.Total_Lote <> 0.01   
				and co.Id_Consumidor is not null   
   
--------------------------------------------------------------------------------------------------------------------   
--------------------------------------------------------------------------------------------------------------------   
   
   
            SELECT   
                Id_Rateio,   
                sum(Valor_Fatura) AS Total_conta_2   
            INTO #soma_idrateio   
            FROM #Temp_Export   
            WHERE Valor_Fatura <> 0   
            GROUP BY Id_Rateio   
   
         insert into Rateio_Item   
            SELECT   
                ex.Id_Rateio,   
                ex.ID_Ativo,   
                ex.id_Conglomerado,   
                ex.Id_Consumidor,   
                ex.Id_Filial,   
                ex.Id_Centro_Custo,   
                cast((ex.Valor_Fatura / s.Total_conta_2) AS decimal (18,8))						    AS Porcentagem,   
                (cast((ex.Valor_Fatura / s.Total_conta_2) AS decimal (18,8)))	* ex.Total_Conta	AS Valor_Rateado,   
                ex.Valor_Fatura,   
                ex.Total_Conta   
            --INTO #Rateio_Item   
            FROM #Temp_Export AS ex   
                INNER JOIN #soma_idrateio AS S ON s.Id_Rateio = ex.Id_Rateio   
--------------------------------------------------------------------------------------------------------------------   
--------------------------------------------------------------------------------------------------------------------   
   
           -- INSERT INTO Rateio_Item   
            --SELECT   
            --    id_rateio,   
            --    ID_Ativo,   
            --    id_Conglomerado,   
            --    Id_Consumidor,   
            --    Id_Filial,   
            --    Id_Centro_Custo,   
            --    Porcentagem,   
            --    0 AS Valor_Rateado,   
            --    Valor_Fatura,   
            --    Total_Conta   
            --FROM #Rateio_Item   
   
            DROP TABLE #Temp_Export   
            DROP TABLE #soma_idrateio   
            DROP TABLE #fatura   
            --DROP TABLE #Rateio_Item   
   
            -----grava ativo critica do rateio      
            UPDATE dbo.Rateio_Ativo_Critica SET Id_Rateio = @vId_Rateio_ WHERE Id_Rateio IS NULL   
        END   
    END   
   
    -----retorno do rateio     
    SELECT *,   
        (SELECT SUM(Valor_Rateado)   
        FROM #Temp_Rateio_) AS Total_Rateado   
    FROM #Temp_Rateio_   
    WHERE NOT Valor_Rateado <= 0   
    ORDER BY Diferenca   
   
    -----Dispara Email Rateio Realizado     
   
    INSERT INTO Mail_Caixa_Saida   
    VALUES   
        (9,   
            (select email   
            from Consumidor   
            where Nm_Consumidor = 'K2A'),   
            --'teste@icontrolit.com.br',     
            NULL,   
            'Rateio realizada da Operadora ' + (select Nm_fatura   
            from fatura   
            where Id_Fatura = @pId_Fatura)+ ' Lote Mes ' + @pData_Lote,   
            --'teste',     
            --@pId_Rateio,     
            --@pId_Fatura,     
            --'Rateio realizada da Operadora ' + @pTFatura + ' Lote Mes ' + @pData_Lote,     
            --'Rateio realizada da Operadora ' + (select Nm_Rateio   from Rateio where Id_Rateio = @pId_Rateio) + ' Lote Mes ' + @pData_Lote,     
            '2021-01-01',   
            NULL,   
            0,   
            0,   
            0)   
END   
   
   
-----#############################################################################################################     
-----#############################################################################################################     
-----#############################################################################################################     
-----CONSULTA DO RATEIO     
-----#############################################################################################################     
-----#############################################################################################################     
-----#############################################################################################################     
   
   
-----retorna fatura      
IF @pPAKAGE = 'sp_Retorna_Fatura'     
BEGIN   
    SELECT ISNULL(Id_Fatura ,'')	AS Id_Fatura,   
        ISNULL(Nm_Fatura,'')	AS Nm_Fatura   
    FROM Fatura   
    WHERE	Id_Fatura_Parametro	= @pId_Fatura_Parametro   
        AND NOT Id_Fatura IN (SELECT Id_Fatura   
        FROM Rl_Rateio_Fatura)   
        AND Dt_Lote = @pData_Lote   
END   
   
-----****************************************************************************************************************     
-----****************************************************************************************************************     
-----retorna tipo rateio      
-----****************************************************************************************************************     
-----****************************************************************************************************************     
IF @pPAKAGE = 'sp_Retorna_Tipo_Rateio'     
BEGIN   
    IF EXISTS(SELECT Id_Fatura_Parametro   
    FROM Rl_Ra_Fatura_Parametro_Ativo   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro)     
	BEGIN   
        SELECT ATV.Id_Ativo,   
            ATV.Nr_Ativo   
        FROM Rl_Ra_Fatura_Parametro_Ativo RFA INNER JOIN vw_Ativo ATV ON (RFA.Id_Ativo = ATV.Id_Ativo)   
        WHERE	Id_Fatura_Parametro = @pId_Fatura_Parametro   
    END   
   
    IF EXISTS(SELECT Id_Fatura_Parametro   
    FROM Rl_Ra_Fatura_Parametro_Conglomerado   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro)     
	BEGIN   
        SELECT CON.Id_Conglomerado,   
            CON.Nm_Conglomerado   
        FROM Rl_Ra_Fatura_Parametro_Conglomerado RFG INNER JOIN vw_Conglomerado CON ON (RFG.Id_Conglomerado = CON.Id_Conglomerado)   
        WHERE	Id_Fatura_Parametro = @pId_Fatura_Parametro   
    END   
   
    IF EXISTS(SELECT Id_Fatura_Parametro   
    FROM Rl_Ra_Fatura_Parametro_Filial   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro)     
	BEGIN   
        SELECT FIL.Id_Filial,   
            FIL.Nm_Filial   
        FROM Rl_Ra_Fatura_Parametro_Filial RFF INNER JOIN vw_Filial FIL ON (RFF.Id_Filial = FIL.Id_Filial)   
        WHERE	Id_Fatura_Parametro = @pId_Fatura_Parametro   
    END   
   
    IF EXISTS(SELECT Id_Fatura_Parametro   
    FROM Rl_Ra_Fatura_Parametro_Tronco_Grupo   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro)     
	BEGIN   
        SELECT TG.Id_Tronco_Grupo,   
            TG.Nm_Tronco_Grupo   
        FROM Rl_Ra_Fatura_Parametro_Tronco_Grupo RTG INNER JOIN vw_Tronco_Grupo TG ON (RTG.Id_Tronco_Grupo = TG.Id_Tronco_Grupo)   
        WHERE	Id_Fatura_Parametro = @pId_Fatura_Parametro   
    END   
END   
   
-----****************************************************************************************************************     
-----****************************************************************************************************************     
-----retorna bilhete alocados em ativo padrao       
-----****************************************************************************************************************     
-----****************************************************************************************************************     
IF @pPAKAGE = 'sp_Retorna_Bilhete_Ativo_Padrao'     
BEGIN   
    -----monta array de fatura     
    SELECT @Array = @pId_Array   
    IF LEN(@Array) > 0 SET @Array = @Array + ','   
    WHILE LEN(@Array) > 0     
	BEGIN   
        SELECT @S = LTRIM(SUBSTRING(@Array, 1, CHARINDEX(',', @Array) - 1))   
        INSERT INTO @pTArray   
        VALUES   
            (@S)   
        SELECT @Array = SUBSTRING(@Array, CHARINDEX(',', @Array) + 1, LEN(@Array))   
    END   
   
    -------------------------------------------------------------------------------------------------     
    -----busca parametro para rateio da tabela fatura parametro     
    -------------------------------------------------------------------------------------------------     
    -----parametro que indica se ira ratear os valore do ativo padrao     
    -----parametro que indica se rateio sera feito por nota     
    SELECT @vRateio_Ativo_Padrao = Rateia_Ativo_Padrao,   
        @vVerifica_Nota = Rateio_Nota   
    FROM Fatura_Parametro   
    WHERE	Id_Fatura_Parametro = @pId_Fatura_Parametro   
   
    -----retorna bilhete de ativo padrao relacionada a fatura     
    SELECT BI.Id_Bilhete,   
        BI.Nm_Bilhete_Tipo,   
        BI.Nm_Bilhete_Descricao,   
        BI.DB_Custo   
    FROM vw_Bilhete BI INNER JOIN Lote LT ON (BI.Id_Lote = LT.Id_Lote)   
    WHERE	BI.Id_Ativo IN (SELECT Id_Ativo   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro)   
        AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND BI.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
        FROM Fatura   
        WHERE Id_Fatura IN (SELECT T_Id   
        FROM @pTArray))))   
        AND SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) = @pData_Lote   
        AND BI.DB_Custo < 0   
END   
   
-----****************************************************************************************************************     
-----****************************************************************************************************************     
-----salva bilhete retirando bilhete do ativo padrao       
-----****************************************************************************************************************     
-----****************************************************************************************************************     
IF @pPAKAGE = 'sp_Salva_Bilhete_Ativo_Padrao'     
BEGIN   
    -----salva bilhete     
    IF EXISTS(SELECT Id_Lote   
    FROM Lote   
    WHERE Dt_Lote IN (SELECT Dt_Lote   
        FROM Lote   
        WHERE Id_Lote IN (SELECT Id_Lote   
        FROM Bilhete   
        WHERE Id_Bilhete = @pId_Rateio))   
        AND Id_Ativo IN ((SELECT TOP 1   
            Id_Ativo   
        FROM Ativo   
        WHERE Nr_Ativo = @pNm_Rateio)))     
	BEGIN   
        UPDATE	Bilhete     
		SET		Id_Ativo = (SELECT TOP 1   
            Id_Ativo   
        FROM Ativo   
        WHERE Nr_Ativo = @pNm_Rateio),     
				Id_Lote = (SELECT Id_Lote   
        FROM Lote   
        WHERE Dt_Lote IN (SELECT Dt_Lote   
            FROM Lote   
            WHERE Id_Lote IN (SELECT Id_Lote   
            FROM Bilhete   
            WHERE Id_Bilhete = @pId_Rateio))   
            AND Id_Ativo IN ((SELECT TOP 1   
                Id_Ativo   
            FROM Ativo   
            WHERE Nr_Ativo = @pNm_Rateio)))     
		WHERE	Id_Bilhete = @pId_Rateio   
    END   
   
    -----monta array de fatura     
    SELECT @Array = @pId_Array   
    IF LEN(@Array) > 0 SET @Array = @Array + ','   
    WHILE LEN(@Array) > 0     
	BEGIN   
        SELECT @S = LTRIM(SUBSTRING(@Array, 1, CHARINDEX(',', @Array) - 1))   
        INSERT INTO @pTArray   
        VALUES   
            (@S)   
        SELECT @Array = SUBSTRING(@Array, CHARINDEX(',', @Array) + 1, LEN(@Array))   
    END   
   
    -------------------------------------------------------------------------------------------------     
    -----busca parametro para rateio da tabela fatura parametro     
    -------------------------------------------------------------------------------------------------     
    -----parametro que indica se ira ratear os valore do ativo padrao     
    -----parametro que indica se rateio sera feito por nota     
    SELECT @vRateio_Ativo_Padrao = Rateia_Ativo_Padrao,   
        @vVerifica_Nota = Rateio_Nota   
    FROM Fatura_Parametro   
    WHERE	Id_Fatura_Parametro = @pId_Fatura_Parametro   
   
    -----retorna bilhete de ativo padrao relacionada a fatura     
    SELECT BI.Id_Bilhete,   
        BI.Nm_Bilhete_Tipo,   
        BI.Nm_Bilhete_Descricao,   
        BI.DB_Custo   
    FROM vw_Bilhete BI INNER JOIN Lote LT ON (BI.Id_Lote = LT.Id_Lote)   
    WHERE	BI.Id_Ativo IN (SELECT Id_Ativo   
        FROM Fatura_Parametro   
        WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro)   
        AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND BI.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
        FROM Fatura   
        WHERE Id_Fatura IN (SELECT T_Id   
        FROM @pTArray))))   
        AND SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) = @pData_Lote   
END   
   
-----****************************************************************************************************************     
-----****************************************************************************************************************     
-----retorna ativo vago     
-----****************************************************************************************************************     
-----****************************************************************************************************************     
IF @pPAKAGE = 'sp_Retorna_Ativo_Vago'     
BEGIN   
    -----limpa tabela de rateio ativo critica de tudo que ainda nao foi salvo     
    DELETE FROM Rateio_Ativo_Critica WHERE Id_Rateio IS NULL   
   
    -------------------------------------------------------------------------------------------------     
    -----busca parametro para rateio da tabela fatura parametro     
    -------------------------------------------------------------------------------------------------     
    -----parametro que indica se ira ratear os valore do ativo padrao     
    -----parametro que indica se rateio sera feito por nota     
    SELECT @vRateio_Ativo_Padrao = Rateia_Ativo_Padrao,   
        @vVerifica_Nota = Rateio_Nota   
    FROM Fatura_Parametro   
    WHERE	Id_Fatura_Parametro = @pId_Fatura_Parametro   
   
    -----monta array de fatura     
    SELECT @Array = @pId_Fatura   
    IF LEN(@Array) > 0 SET @Array = @Array + ','   
    WHILE LEN(@Array) > 0     
	BEGIN   
        SELECT @S = LTRIM(SUBSTRING(@Array, 1, CHARINDEX(',', @Array) - 1))   
        INSERT INTO @pTFatura   
        VALUES   
            (@S)   
        SELECT @Array = SUBSTRING(@Array, CHARINDEX(',', @Array) + 1, LEN(@Array))   
    END   
   
    -----monta array de fatura_tipo_rateio     
    SELECT @Array = @pId_Fatura_Tipo_Rateio   
    IF LEN(@Array) > 0 SET @Array = @Array + ','   
    WHILE LEN(@Array) > 0     
	BEGIN   
        SELECT @S = LTRIM(SUBSTRING(@Array, 1, CHARINDEX(',', @Array) - 1))   
        INSERT INTO @pTFatura_Tipo_Rateio   
        VALUES   
            (@S)   
        SELECT @Array = SUBSTRING(@Array, CHARINDEX(',', @Array) + 1, LEN(@Array))   
    END   
   
    -----cria tabela temporaria     
    CREATE TABLE #T_Rateio_Ativo_Critica   
    (   
        Id_Rateio INT,   
        Id_Ativo INT,   
        Nr_Ativo VARCHAR(50),   
        Total DECIMAL(13,2)   
    )   
   
   
    IF 'sp_Fatura_Tipo_Rateio_Conglomerado' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        INSERT INTO #T_Rateio_Ativo_Critica   
        SELECT NULL					AS Id_Rateio,   
            AT.Id_Ativo				AS Id_Ativo,   
            AT.Nr_Ativo				AS Nr_Ativo,   
            SUM(VWCU.Total_Lote)	AS Total   
        FROM vw_Custo_Usuario VWCU INNER JOIN Ativo AT ON (VWCU.Id_Ativo = AT.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND AT.Id_Conglomerado IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura))))   
            AND VWCU.Id_Centro_Custo IS NULL   
        GROUP BY	AT.Id_Ativo,AT.Nr_Ativo   
    END   
    IF 'sp_Fatura_Tipo_Rateio_Filial' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        INSERT INTO #T_Rateio_Ativo_Critica   
        SELECT NULL					AS Id_Rateio,   
            AT.Id_Ativo				AS Id_Ativo,   
            AT.Nr_Ativo				AS Nr_Ativo,   
            SUM(VWCU.Total_Lote)	AS Total   
        FROM vw_Custo_Usuario VWCU INNER JOIN Ativo AT ON (VWCU.Id_Ativo = AT.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND VWCU.Id_Filial IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND VWCU.Id_Centro_Custo IS NULL   
        GROUP BY	AT.Id_Ativo,AT.Nr_Ativo   
    END   
    IF 'sp_Fatura_Tipo_Rateio_Ativo' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        INSERT INTO #T_Rateio_Ativo_Critica   
        SELECT NULL					AS Id_Rateio,   
            AT.Id_Ativo				AS Id_Ativo,   
            AT.Nr_Ativo				AS Nr_Ativo,   
            SUM(VWCU.Total_Lote)	AS Total   
        FROM vw_Custo_Usuario VWCU INNER JOIN Ativo AT ON (VWCU.Id_Ativo = AT.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND AT.Id_Ativo IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND NOT VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor)--     
            AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura))))   
            AND VWCU.Id_Centro_Custo IS NULL   
        GROUP BY	AT.Id_Ativo,AT.Nr_Ativo   
    END   
   
    IF @pGrava_Rateio = 2     
	BEGIN   
        INSERT INTO Rateio_Ativo_Critica   
        SELECT (SELECT MAX(Id_Rateio)   
            FROM Rateio) AS Id_Rateio,   
            Id_Ativo,   
            Nr_Ativo,   
            Total   
        FROM #T_Rateio_Ativo_Critica TRAC   
        WHERE	NOT EXISTS(SELECT *   
        FROM Rateio_Ativo_Critica   
        WHERE Id_Rateio = TRAC.Id_Rateio   
            AND Id_Ativo = TRAC.Id_Ativo)  
    END   
   
    -----lista ativo critica     
    SELECT ISNULL(SUM(Total),0) AS Total   
    FROM #T_Rateio_Ativo_Critica   
END   
   
-----****************************************************************************************************************     
-----****************************************************************************************************************     
-----deleta rateio     
-----****************************************************************************************************************     
-----****************************************************************************************************************     
   
IF @pPAKAGE = 'sp_Deleta_Rateio'     
BEGIN   
    IF EXISTS(SELECT *   
    FROM Rateio   
    WHERE Id_Rateio = @pId_Rateio)     
	BEGIN   
        DELETE FROM Rateio_Ativo_Critica WHERE Id_Rateio IS NULL   
        DELETE FROM Rateio_Ativo_Critica WHERE Id_Rateio = @pId_Rateio   
        DELETE FROM Rateio_Item WHERE Id_Rateio = @pId_Rateio   
        DELETE FROM Rl_Rateio_Fatura WHERE Id_Rateio = @pId_Rateio   
        DELETE FROM Rateio WHERE Id_Rateio = @pId_Rateio   
    END   
END   
   
-----****************************************************************************************************************     
-----****************************************************************************************************************     
-----retorna ativo estoque     
-----****************************************************************************************************************     
-----****************************************************************************************************************     
IF @pPAKAGE = 'sp_Retorna_Ativo_Estoque'     
BEGIN   
    -------------------------------------------------------------------------------------------------     
    -----busca parametro para rateio da tabela fatura parametro     
    -------------------------------------------------------------------------------------------------     
    -----parametro que indica se ira ratear os valore do ativo padrao     
    -----parametro que indica se rateio sera feito por nota     
    SELECT @vRateio_Ativo_Padrao = Rateia_Ativo_Padrao,   
        @vVerifica_Nota = Rateio_Nota   
    FROM Fatura_Parametro   
    WHERE	Id_Fatura_Parametro = @pId_Fatura_Parametro   
   
    -----monta array de fatura     
    SELECT @Array = @pId_Fatura   
    IF LEN(@Array) > 0 SET @Array = @Array + ','   
    WHILE LEN(@Array) > 0     
	BEGIN   
        SELECT @S = LTRIM(SUBSTRING(@Array, 1, CHARINDEX(',', @Array) - 1))   
        INSERT INTO @pTFatura   
        VALUES   
            (@S)   
        SELECT @Array = SUBSTRING(@Array, CHARINDEX(',', @Array) + 1, LEN(@Array))   
    END   
   
    -----monta array de fatura_tipo_rateio     
    SELECT @Array = @pId_Fatura_Tipo_Rateio   
    IF LEN(@Array) > 0 SET @Array = @Array + ','   
    WHILE LEN(@Array) > 0     
	BEGIN   
        SELECT @S = LTRIM(SUBSTRING(@Array, 1, CHARINDEX(',', @Array) - 1))   
        INSERT INTO @pTFatura_Tipo_Rateio   
        VALUES   
            (@S)   
        SELECT @Array = SUBSTRING(@Array, CHARINDEX(',', @Array) + 1, LEN(@Array))   
    END   
   
    -----cria tabela temporaria     
    CREATE TABLE #T_Rateio_Ativo_Estoque   
    (   
        Id_Rateio INT,   
        Id_Ativo INT,   
        Nr_Ativo VARCHAR(50),   
        Total DECIMAL(13,2)   
    )   
   
    IF 'sp_Fatura_Tipo_Rateio_Conglomerado' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        INSERT INTO #T_Rateio_Ativo_Estoque   
        SELECT NULL					AS Id_Rateio,   
            AT.Id_Ativo				AS Id_Ativo,   
            AT.Nr_Ativo				AS Nr_Ativo,   
            SUM(VWCU.Total_Lote)	AS Total   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (VWCU.Id_Ativo = AT.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND AT.Id_Conglomerado IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND (AT.Id_Ativo IN (SELECT ISNULL(Id_Ativo,0)   
            FROM dbo.Rl_Consumidor_Ativo   
            WHERE Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor))   
            AND VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor))   
   
            AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura))))   
        GROUP BY	AT.Id_Ativo,AT.Nr_Ativo   
    END   
    IF 'sp_Fatura_Tipo_Rateio_Filial' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        INSERT INTO #T_Rateio_Ativo_Estoque   
        SELECT NULL					AS Id_Rateio,   
            AT.Id_Ativo				AS Id_Ativo,   
            AT.Nr_Ativo				AS Nr_Ativo,   
            SUM(VWCU.Total_Lote)	AS Total   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (VWCU.Id_Ativo = AT.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND VWCU.Id_Filial IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND (AT.Id_Ativo IN (SELECT ISNULL(Id_Ativo,0)   
            FROM dbo.Rl_Consumidor_Ativo   
            WHERE Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor))   
            AND VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor))   
   
        GROUP BY	AT.Id_Ativo,AT.Nr_Ativo   
    END   
    IF 'sp_Fatura_Tipo_Rateio_Ativo' = (SELECT Id_Fatura_Tipo_Rateio   
    FROM Fatura_Parametro   
    WHERE Id_Fatura_Parametro = @pId_Fatura_Parametro )     
	BEGIN   
        INSERT INTO #T_Rateio_Ativo_Estoque   
        SELECT NULL					AS Id_Rateio,   
            AT.Id_Ativo				AS Id_Ativo,   
            AT.Nr_Ativo				AS Nr_Ativo,   
            SUM(VWCU.Total_Lote)	AS Total   
        FROM vw_Custo_Usuario VWCU INNER JOIN vw_Ativo AT ON (VWCU.Id_Ativo = AT.Id_Ativo)   
        WHERE	SUBSTRING(CONVERT(VARCHAR(6),VWCU.Dt_Lote,112),1,6) = @pData_Lote   
            AND AT.Id_Ativo IN (SELECT *   
            FROM @pTFatura_Tipo_Rateio)   
            AND NOT AT.Id_Ativo IN (SELECT Id_Ativo   
            FROM Fatura_Parametro)   
            AND (AT.Id_Ativo IN (SELECT ISNULL(Id_Ativo,0)   
            FROM dbo.Rl_Consumidor_Ativo   
            WHERE Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor))   
            AND VWCU.Id_Consumidor IN (SELECT Id_Consumidor   
            FROM Estoque_Consumidor))   
   
            AND (@vVerifica_Nota = 1 OR ( @vVerifica_Nota = 2 AND VWCU.DC_Nr_Nota_Fiscal IN (SELECT Nr_Fatura   
            FROM Fatura   
            WHERE Id_Fatura IN (SELECT T_Id   
            FROM @pTFatura))))   
        GROUP BY	AT.Id_Ativo,AT.Nr_Ativo   
   
   
   
   
    END   
    -----retorna ativo estoque     
    SELECT ISNULL(SUM(Total),0) AS Total   
    FROM #T_Rateio_Ativo_Estoque   
END 

-----****************************************************************************************************************     
-----****************************************************************************************************************     
-----retorna se as faturas selecionadas para o rateio possuem Centro de Custo, Colaborador,
----- Ativo e Consumidor     
-----****************************************************************************************************************     
-----**************************************************************************************************************** 
IF @pPAKAGE = 'sp_Retorna_Fatura_Ativo_Consumidor'     
BEGIN     

	DECLARE @conta varchar(100), @ativo VARCHAR(30), @conglomerado VARCHAR(60), 
			@compartilhado VARCHAR(90), @colaborador varchar(100), @lote varchar(100), @total varchar(100),
			@centrocusto int,
			@possuiativos bit = 1, @countConsumidor int, 
			@possuiconsumidor bit = 1,
			@possuicolaborador bit = 1,
			@possuicentrocusto bit = 1


	--Cursor para percorrer os registros
	DECLARE cursor1 CURSOR FOR
	  SELECT	LT.DC_Nr_Nota_Fiscal				AS Conta, 
				AT.Nr_Ativo							AS Ativo,
				CG.Nm_Conglomerado					AS Conglomerado,
				(SELECT COUNT(*) FROM Rl_Consumidor_Ativo SRL INNER JOIN vw_Consumidor SC ON SRL.Id_Consumidor = SC.Id_Consumidor WHERE SRL.Id_Ativo = AT.Id_Ativo) AS Compartilhado,
										ISNULL(CO.Nm_Consumidor, 'Vazia')	AS Colaborador,
						--	ISNULL(CAST(LT.Total_Lote AS DECIMAL(10,2)),0) AS Total_Conta,
				SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) AS Lote,
				ISNULL(CAST(LT.Total_Consumo AS DECIMAL(10,2)),0) AS Total,
				ISNULL(cc.Id_Centro_Custo, 0) AS CentroCusto
			--	INTO #Temp_Export
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
		WHERE	LT.DC_Nr_Nota_Fiscal in (
			SELECT TOP 1 Nr_Fatura
			FROM Fatura WHERE Nm_Fatura in (@pNm_Rateio) AND Dt_Lote = @pData_Lote
		)
		and SUBSTRING(CONVERT(VARCHAR(6),LT.Dt_Lote,112),1,6) >= @pData_Lote

	--Abrindo Cursor
	OPEN cursor1

	-- Lendo a próxima linha
	FETCH NEXT FROM cursor1 INTO @conta, @ativo, @conglomerado, @compartilhado, @colaborador, @lote, @total, @centrocusto
	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN

		-- Executando as rotinas desejadas manipulando o registro
		--SELECT @conta, @ativo, @conglomerado, @compartilhado, @colaborador, @lote, @total, @centrocusto

		--se não apresentou o colaborador já retorna impedimento
		IF(@colaborador = 'Vazia')
		BEGIN
		   SET @possuicolaborador = 0
		END

		IF(@centrocusto = 0)
		BEGIN
		   SET @possuicentrocusto = 0 
		END
				
		-- Determinando se existe ativo para cada loop
		--select * from ativo where Nr_Ativo=@Ativo -- 144
		IF( EXISTS(select * from ativo where Nr_Ativo=@Ativo))
		BEGIN
		   --SELECT 'EXISTE ATIVO PARA A CONTA', @conta, @ativo	
		   -- Determina se existe consumidor para o ativo
		   IF(NOT EXISTS(select * from Rl_Consumidor_Ativo where Id_Ativo IN (select Id_Ativo from ativo where Nr_Ativo=@Ativo)))
		   BEGIN
		      SET @possuiativos = 0
		   END
		END
		ELSE BEGIN
		  SET @possuiativos = 0
		END

		

	-- Lendo a próxima linha
	FETCH NEXT FROM cursor1 INTO @conta, @ativo, @conglomerado, @compartilhado, @colaborador, @lote, @total, @centrocusto
	END

	-- Fechando Cursor para leitura
	CLOSE cursor1

	-- Finalizado o cursor
	DEALLOCATE cursor1

	SELECT @possuiativos AS ativo
	     , @possuiconsumidor AS consumidor
		 , @possuicentrocusto AS centrocusto
		 , @possuicolaborador AS colaborador
END