-- ============================================================================
-- MIGRATIONS SQL - CONTRATOS A VENCER (FONTE_CORRETA_PRD)
-- Funcionalidade: KPI Contratos a Vencer (ICTRL-NF-202509-002)
-- Data: 2025-02-09
-- ============================================================================
-- IMPORTANTE: Execute este script no banco de dados do cliente para habilitar
-- a funcionalidade de filtro de contratos por vencimento.
-- ============================================================================

-- ============================================================================
-- 1. STORED PROCEDURE: sp_Drop_Contrato_Status
-- ============================================================================

IF EXISTS (SELECT 1 FROM sys.procedures WHERE name = 'sp_Drop_Contrato_Status')
BEGIN
    DROP PROCEDURE dbo.sp_Drop_Contrato_Status;
    PRINT 'Procedure sp_Drop_Contrato_Status removida para recriação';
END
GO

CREATE PROCEDURE dbo.sp_Drop_Contrato_Status
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id_Contrato_Status AS ID,
        Nm_Contrato_Status AS Descricao
    FROM dbo.Contrato_Status
    ORDER BY Nm_Contrato_Status;
END
GO

PRINT 'Procedure sp_Drop_Contrato_Status criada com sucesso';
GO

-- ============================================================================
-- 2. STORED PROCEDURE: sp_Drop_Contrato_Vencimento
-- ============================================================================

IF EXISTS (SELECT 1 FROM sys.procedures WHERE name = 'sp_Drop_Contrato_Vencimento')
BEGIN
    DROP PROCEDURE dbo.sp_Drop_Contrato_Vencimento;
    PRINT 'Procedure sp_Drop_Contrato_Vencimento removida para recriação';
END
GO

CREATE PROCEDURE dbo.sp_Drop_Contrato_Vencimento
AS
BEGIN
    SET NOCOUNT ON;

    -- Retorna intervalos de dias pré-definidos
    SELECT Dias, Descricao, Fl_Principal
    FROM (
        SELECT 5 AS Dias, '5 dias' AS Descricao, 0 AS Fl_Principal UNION ALL
        SELECT 10, '10 dias', 0 UNION ALL
        SELECT 15, '15 dias', 0 UNION ALL
        SELECT 30, '30 dias', 0 UNION ALL
        SELECT 45, '45 dias', 1 UNION ALL  -- Padrão
        SELECT 60, '60 dias', 0 UNION ALL
        SELECT 90, '90 dias', 0
    ) AS Vencimentos
    ORDER BY Dias;
END
GO

PRINT 'Procedure sp_Drop_Contrato_Vencimento criada com sucesso';
GO

-- ============================================================================
-- 3. STORED PROCEDURE: sp_SL_Consulta_Contrato_Filtro
-- Esta é a procedure principal que faz o filtro de contratos por vencimento
-- ============================================================================

IF EXISTS (SELECT 1 FROM sys.procedures WHERE name = 'sp_SL_Consulta_Contrato_Filtro')
BEGIN
    DROP PROCEDURE dbo.sp_SL_Consulta_Contrato_Filtro;
    PRINT 'Procedure sp_SL_Consulta_Contrato_Filtro removida para recriação';
END
GO

CREATE PROCEDURE dbo.sp_SL_Consulta_Contrato_Filtro
    @pId_Contrato_Status INT = 0,
    @pDias_Vencimento INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DataLimite DATE;

    -- Calcular data limite baseada nos dias de vencimento
    IF @pDias_Vencimento > 0
        SET @DataLimite = DATEADD(DAY, @pDias_Vencimento, GETDATE());

    SELECT
        c.Id_Contrato,
        c.Nr_Contrato,
        c.Descricao,
        cs.Nm_Contrato_Status,
        c.Dt_Inicio_Vigencia,
        c.Dt_Fim_Vigencia,
        DATEDIFF(DAY, GETDATE(), c.Dt_Fim_Vigencia) AS DiasParaVencer,
        ec.Nm_Empresa AS Nm_Empresa_Contratada,
        '' AS Empresa,
        f.Nm_Filial,
        s.Nm_Servico,
        c.Objeto,
        '' AS Nm_Contrato_Indice
    FROM dbo.Contrato c
    INNER JOIN dbo.Contrato_Status cs ON c.Id_Contrato_Status = cs.Id_Contrato_Status
    LEFT JOIN dbo.Empresa ec ON c.Id_Empresa_Contratada = ec.Id_Empresa
    LEFT JOIN dbo.Filial f ON c.Id_Filial = f.Id_Filial
    LEFT JOIN dbo.Servico s ON c.Id_Servico = s.Id_Servico
    WHERE
        (c.Fl_Desativado = 0 OR c.Fl_Desativado IS NULL)
        AND (@pId_Contrato_Status = 0 OR c.Id_Contrato_Status = @pId_Contrato_Status)
        AND (
            @pDias_Vencimento = 0
            OR (c.Dt_Fim_Vigencia <= @DataLimite AND c.Dt_Fim_Vigencia >= GETDATE())
        )
    ORDER BY c.Dt_Fim_Vigencia ASC;
END
GO

PRINT 'Procedure sp_SL_Consulta_Contrato_Filtro criada com sucesso';
GO

-- ============================================================================
-- 4. ADICIONAR PACKAGES NA STORED PROCEDURE pa_Contrato
-- ============================================================================
-- NOTA: Estes são os blocos que precisam ser adicionados DENTRO da procedure
-- pa_Contrato do cliente, logo antes do END final.
-- ============================================================================

PRINT '';
PRINT '============================================================================';
PRINT 'IMPORTANTE: Adicione os seguintes blocos na stored procedure pa_Contrato';
PRINT 'do cliente, logo antes do END final:';
PRINT '============================================================================';
PRINT '';
PRINT '------contrato_status';
PRINT 'IF @pPAKAGE = ''sp_Drop_Contrato_Status''';
PRINT 'BEGIN';
PRINT '    SELECT Id_Contrato_Status AS ID,';
PRINT '        Nm_Contrato_Status AS Descricao';
PRINT '    FROM dbo.Contrato_Status';
PRINT '    ORDER BY Nm_Contrato_Status;';
PRINT 'END';
PRINT '';
PRINT '------contrato_vencimento (intervalos de dias)';
PRINT 'IF @pPAKAGE = ''sp_Drop_Contrato_Vencimento''';
PRINT 'BEGIN';
PRINT '    SELECT Dias, Descricao';
PRINT '    FROM (';
PRINT '        SELECT 5 AS Dias, ''5 dias'' AS Descricao UNION ALL';
PRINT '        SELECT 10, ''10 dias'' UNION ALL';
PRINT '        SELECT 15, ''15 dias'' UNION ALL';
PRINT '        SELECT 30, ''30 dias'' UNION ALL';
PRINT '        SELECT 45, ''45 dias'' UNION ALL';
PRINT '        SELECT 60, ''60 dias'' UNION ALL';
PRINT '        SELECT 90, ''90 dias''';
PRINT '    ) AS Vencimentos';
PRINT '    ORDER BY Dias;';
PRINT 'END';
PRINT '';
GO

-- ============================================================================
-- 5. VERIFICAÇÃO
-- ============================================================================

PRINT '';
PRINT '============================================================================';
PRINT 'VERIFICAÇÃO DAS PROCEDURES CRIADAS';
PRINT '============================================================================';

SELECT
    name AS ProcedureName,
    create_date AS DataCriacao,
    modify_date AS DataModificacao
FROM sys.procedures
WHERE name IN (
    'sp_Drop_Contrato_Status',
    'sp_Drop_Contrato_Vencimento',
    'sp_SL_Consulta_Contrato_Filtro'
)
ORDER BY name;

PRINT '';
PRINT '============================================================================';
PRINT 'MIGRATIONS CONCLUÍDAS - FILTRO DE CONTRATOS';
PRINT '============================================================================';
PRINT '';
PRINT 'Agora o filtro de contratos por vencimento deve funcionar corretamente.';
PRINT 'Teste acessando: /Contrato/Consulta_Contrato.aspx?vencimento=60';
GO
