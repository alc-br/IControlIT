-- =============================================================================
-- Patch: pa_SincronizarEstruturaOrganizacional
-- Data:  2026-04-07
-- Autor: Anderson Luiz Chipak - Agencia ALC
--
-- Correcoes (apenas no bloco MERGE_CONSUMIDOR):
--   1. Garante existencia de Cargo a partir do staging do Consumidor
--      (mesmo padrao ja usado para Centro_Custo), evitando que Id_Cargo
--      fique NULL e sobrescreva o cargo existente.
--   2. Remove "CS.Fl_Desativado = S.Fl_Desativado" do UPDATE que junta
--      com Consumidor_Status (estava sobrescrevendo o flag do consumidor
--      com o flag do proprio status).
--   3. No WHEN MATCHED do MERGE Consumidor, FKs e Status passam a usar
--      ISNULL(SRC.X, T.X) para preservar o valor antigo quando o novo
--      vier NULL (ex.: codigo nao localizado nas tabelas de dominio).
--
-- Os demais blocos (MERGE_EMPRESA, MERGE_FILIAL, MERGE_CARGO,
-- MERGE_DEPARTAMENTO, MERGE_CENTRO_CUSTO) NAO foram alterados.
-- =============================================================================

ALTER PROCEDURE [dbo].[pa_SincronizarEstruturaOrganizacional]
    @pAcao VARCHAR(200),
    @XmlData XML
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @pAcao = 'MERGE_EMPRESA'
        BEGIN
            TRUNCATE TABLE Empresa_Staging;

            INSERT INTO Empresa_Staging (Cd_Empresa, Id_Holding, Nm_Empresa, CNPJ, Fl_Desativado)
            SELECT
                C.value('(Cd_Empresa)[1]', 'varchar(20)'),
                C.value('(Id_Holding)[1]', 'int'),
                C.value('(Nm_Empresa)[1]', 'varchar(50)'),
                C.value('(CNPJ)[1]', 'varchar(50)'),
                C.value('(Fl_Desativado)[1]', 'int')
            FROM @XmlData.nodes('/Root/Empresa') AS T(C);

            UPDATE T
            SET T.Cd_Empresa = S.Cd_Empresa
            FROM Empresa T
            INNER JOIN Empresa_Staging S
                ON T.Id_Holding = S.Id_Holding
                AND T.Nm_Empresa = S.Nm_Empresa
                AND T.CNPJ = S.CNPJ
                AND T.Fl_Desativado = S.Fl_Desativado
                AND T.Cd_Empresa IS NULL;

            DECLARE @EmpresaOutput TABLE (Id_Empresa INT, Cd_Empresa VARCHAR(20));

            MERGE Empresa AS T
            USING (SELECT Cd_Empresa, Id_Holding, Nm_Empresa, CNPJ, Fl_Desativado FROM Empresa_Staging) AS S
            ON T.Cd_Empresa = S.Cd_Empresa
            WHEN MATCHED THEN
                UPDATE SET T.Nm_Empresa = S.Nm_Empresa,
                           T.Id_Holding = S.Id_Holding,
                           T.CNPJ = S.CNPJ,
                           T.Fl_Desativado = S.Fl_Desativado
            WHEN NOT MATCHED BY TARGET THEN
                INSERT (Cd_Empresa, Id_Holding, Nm_Empresa, CNPJ, Fl_Desativado)
                VALUES (S.Cd_Empresa, S.Id_Holding, S.Nm_Empresa, S.CNPJ, S.Fl_Desativado)
                OUTPUT INSERTED.Id_Empresa, S.Cd_Empresa INTO @EmpresaOutput(Id_Empresa, Cd_Empresa);

            INSERT INTO Empresa_Map (Id_Empresa, Cd_Empresa)
            SELECT O.Id_Empresa, O.Cd_Empresa
            FROM @EmpresaOutput O
            WHERE NOT EXISTS (SELECT 1 FROM Empresa_Map EM WHERE EM.Id_Empresa = O.Id_Empresa);
        END


        ELSE IF @pAcao = 'MERGE_FILIAL'
        BEGIN
            TRUNCATE TABLE Filial_Staging;

            INSERT INTO Filial_Staging (Cd_Filial, Nm_Filial, CNPJ, Endereco, Cd_Empresa, Fl_Desativado)
            SELECT
                C.value('(Cd_Filial)[1]', 'varchar(50)'),
                C.value('(Nm_Filial)[1]', 'varchar(50)'),
                C.value('(CNPJ)[1]', 'varchar(50)'),
                C.value('(Endereco)[1]', 'varchar(300)'),
                C.value('(Cd_Empresa)[1]', 'varchar(20)'),
                C.value('(Fl_Desativado)[1]', 'int')
            FROM @XmlData.nodes('/Root/Filial') AS T(C);

            -- Atualiza FK antes do MERGE
            UPDATE FS
            SET FS.Id_Empresa = M.Id_Empresa
            FROM Filial_Staging FS
            JOIN Empresa_Map M ON FS.Cd_Empresa = M.Cd_Empresa;

            UPDATE T
            SET T.Cd_Filial = S.Cd_Filial
            FROM Filial T
            INNER JOIN Filial_Staging S
                ON T.Nm_Filial = S.Nm_Filial
                AND T.CNPJ = S.CNPJ
                AND T.Endereco = S.Endereco
                AND T.Cd_Filial IS NULL;

            DECLARE @FilialOutput TABLE (Id_Filial INT, Cd_Filial VARCHAR(50));

            MERGE Filial AS T
            USING (
                SELECT FS.Cd_Filial, FS.Nm_Filial, FS.CNPJ, FS.Endereco, FS.Id_Empresa, FS.Fl_Desativado
                FROM Filial_Staging FS
            ) AS S
            ON T.Cd_Filial = S.Cd_Filial
            WHEN MATCHED THEN
                UPDATE SET T.Nm_Filial = S.Nm_Filial,
                           T.CNPJ = S.CNPJ,
                           T.Endereco = S.Endereco,
                           T.Id_Empresa = S.Id_Empresa,
                           T.Fl_Desativado = S.Fl_Desativado
            WHEN NOT MATCHED BY TARGET THEN
                INSERT (Cd_Filial, Nm_Filial, CNPJ, Endereco, Id_Empresa, Fl_Desativado)
                VALUES (S.Cd_Filial, S.Nm_Filial, S.CNPJ, S.Endereco, S.Id_Empresa, S.Fl_Desativado)
                OUTPUT INSERTED.Id_Filial, S.Cd_Filial INTO @FilialOutput(Id_Filial, Cd_Filial);

            INSERT INTO Filial_Map (Id_Filial, Cd_Filial)
            SELECT O.Id_Filial, O.Cd_Filial
            FROM @FilialOutput O
            WHERE NOT EXISTS (SELECT 1 FROM Filial_Map FM WHERE FM.Id_Filial = O.Id_Filial);
        END


        ELSE IF @pAcao = 'MERGE_CARGO'
        BEGIN
            TRUNCATE TABLE Cargo_Staging;

            INSERT INTO Cargo_Staging (Cd_Cargo, Nm_Cargo, Fl_Desativado)
            SELECT
                C.value('(Cd_Cargo)[1]', 'varchar(20)'),
                C.value('(Nm_Cargo)[1]', 'varchar(100)'),
                C.value('(Fl_Desativado)[1]', 'int')
            FROM @XmlData.nodes('/Root/Cargo') AS T(C);

            UPDATE T
            SET T.Cd_Cargo = S.Cd_Cargo
            FROM Cargo T
            INNER JOIN Cargo_Staging S
                ON T.Nm_Cargo = S.Nm_Cargo
                AND T.Cd_Cargo IS NULL;

            DECLARE @CargoOutput TABLE (Id_Cargo INT, Cd_Cargo VARCHAR(20));

            MERGE Cargo AS T
            USING (SELECT Cd_Cargo, Nm_Cargo, Fl_Desativado FROM Cargo_Staging) AS S
            ON T.Cd_Cargo = S.Cd_Cargo
            WHEN MATCHED THEN
                UPDATE SET T.Nm_Cargo = S.Nm_Cargo,
                           T.Fl_Desativado = S.Fl_Desativado
            WHEN NOT MATCHED BY TARGET THEN
                INSERT (Cd_Cargo, Nm_Cargo, Fl_Desativado)
                VALUES (S.Cd_Cargo, S.Nm_Cargo, S.Fl_Desativado)
                OUTPUT INSERTED.Id_Cargo, S.Cd_Cargo INTO @CargoOutput(Id_Cargo, Cd_Cargo);

            INSERT INTO Cargo_Map (Id_Cargo, Cd_Cargo)
            SELECT O.Id_Cargo, O.Cd_Cargo
            FROM @CargoOutput O
            WHERE NOT EXISTS (SELECT 1 FROM Cargo_Map CM WHERE CM.Id_Cargo = O.Id_Cargo);
        END


        ELSE IF @pAcao = 'MERGE_DEPARTAMENTO'
        BEGIN
            TRUNCATE TABLE Departamento_Staging;

            INSERT INTO Departamento_Staging (Cd_Departamento, Nm_Departamento, Fl_Desativado)
            SELECT
                C.value('(Cd_Departamento)[1]', 'varchar(20)'),
                C.value('(Nm_Departamento)[1]', 'varchar(80)'),
                C.value('(Fl_Desativado)[1]', 'int')
            FROM @XmlData.nodes('/Root/Departamento') AS T(C);

            UPDATE T
            SET T.Cd_Departamento = S.Cd_Departamento
            FROM Departamento T
            INNER JOIN Departamento_Staging S
                ON T.Nm_Departamento = S.Nm_Departamento
                AND T.Cd_Departamento IS NULL;

            DECLARE @DepartamentoOutput TABLE (Id_Departamento INT, Cd_Departamento VARCHAR(20));

            MERGE Departamento AS T
            USING (SELECT Cd_Departamento, Nm_Departamento, Fl_Desativado FROM Departamento_Staging) AS S
            ON T.Cd_Departamento = S.Cd_Departamento
            WHEN MATCHED THEN
                UPDATE SET T.Nm_Departamento = S.Nm_Departamento,
                           T.Fl_Desativado = S.Fl_Desativado
            WHEN NOT MATCHED BY TARGET THEN
                INSERT (Cd_Departamento, Nm_Departamento, Fl_Desativado)
                VALUES (S.Cd_Departamento, S.Nm_Departamento, S.Fl_Desativado)
                OUTPUT INSERTED.Id_Departamento, S.Cd_Departamento INTO @DepartamentoOutput(Id_Departamento, Cd_Departamento);

            INSERT INTO Departamento_Map (Id_Departamento, Cd_Departamento)
            SELECT O.Id_Departamento, O.Cd_Departamento
            FROM @DepartamentoOutput O
            WHERE NOT EXISTS (SELECT 1 FROM Departamento_Map DM WHERE DM.Id_Departamento = O.Id_Departamento);
        END


       ELSE IF @pAcao = 'MERGE_CENTRO_CUSTO'
        BEGIN
            TRUNCATE TABLE Centro_Custo_Staging;

            INSERT INTO Centro_Custo_Staging (Cd_Centro_Custo, Nm_Centro_Custo, Fl_Desativado)
            SELECT
                C.value('(Cd_Centro_Custo)[1]', 'varchar(50)'),
                C.value('(Nm_Centro_Custo)[1]', 'varchar(70)'),
                C.value('(Fl_Desativado)[1]', 'int')
            FROM @XmlData.nodes('/Root/Centro_Custo') AS T(C);

            UPDATE T
            SET T.Cd_Centro_Custo = S.Cd_Centro_Custo
            FROM Centro_Custo T
            INNER JOIN Centro_Custo_Staging S
                ON T.Nm_Centro_Custo = S.Nm_Centro_Custo
                AND T.Cd_Centro_Custo IS NULL;

            DECLARE @CentroCustoOutput TABLE (Id_Centro_Custo INT, Cd_Centro_Custo VARCHAR(50));

            MERGE Centro_Custo AS T
            USING (SELECT Cd_Centro_Custo, Nm_Centro_Custo, Fl_Desativado FROM Centro_Custo_Staging) AS S
            ON T.Cd_Centro_Custo = S.Cd_Centro_Custo
            WHEN MATCHED THEN
                UPDATE SET T.Nm_Centro_Custo = S.Nm_Centro_Custo,
                           T.Fl_Desativado = S.Fl_Desativado
            WHEN NOT MATCHED BY TARGET THEN
                INSERT (Cd_Centro_Custo, Nm_Centro_Custo, Fl_Desativado)
                VALUES (S.Cd_Centro_Custo, S.Nm_Centro_Custo, S.Fl_Desativado)
                OUTPUT INSERTED.Id_Centro_Custo, S.Cd_Centro_Custo INTO @CentroCustoOutput(Id_Centro_Custo, Cd_Centro_Custo);

            INSERT INTO Centro_Custo_Map (Id_Centro_Custo, Cd_Centro_Custo)
            SELECT O.Id_Centro_Custo, O.Cd_Centro_Custo
            FROM @CentroCustoOutput O
            WHERE NOT EXISTS (SELECT 1 FROM Centro_Custo_Map CCM WHERE CCM.Id_Centro_Custo = O.Id_Centro_Custo);
        END


        ELSE IF @pAcao = 'MERGE_CONSUMIDOR'
        BEGIN
            -- TRUNCAR TABELAS DE STAGING
            TRUNCATE TABLE Consumidor_Staging;

            INSERT INTO Consumidor_Staging (
                Cd_Consumidor,
                Nm_Consumidor,
                Matricula,
                EMail,
                Cd_Consumidor_Status,
                Matricula_Chefia,
                Fl_Desativado,
                Nm_Cidade,
                Nm_Estado,
                Dt_Ativacao_Consumidor,
                Dt_Desativacao_Consumidor,
                Cd_Centro_Custo,
                Cd_Departamento,
                Cd_Filial,
                Cd_Cargo, -- [INICIO - ICTRL-FX-202508-001]
                Id_Consumidor_Tipo
            )
            SELECT
                C.value('(Cd_Consumidor)[1]', 'varchar(20)'),
                C.value('(Nm_Consumidor)[1]', 'varchar(200)'),
                C.value('(Matricula)[1]', 'varchar(120)'),
                C.value('(EMail)[1]', 'varchar(200)'),
                C.value('(Cd_Consumidor_Status)[1]', 'varchar(10)'),
                ISNULL(C.value('(Matricula_Chefia)[1]', 'varchar(20)'), ''),
                ISNULL(C.value('(Fl_Desativado)[1]', 'int'), 0),
                ISNULL(C.value('(Nm_Cidade)[1]', 'varchar(200)'), ''),
                ISNULL(C.value('(Nm_Estado)[1]', 'varchar(200)'), ''),
                ISNULL(C.value('(Dt_Ativacao_Consumidor)[1]', 'varchar(200)'), NULL),
                ISNULL(C.value('(Dt_Desativacao_Consumidor)[1]', 'datetime'), NULL),
                C.value('(Cd_Centro_Custo)[1]', 'varchar(50)'),
                C.value('(Cd_Departamento)[1]', 'varchar(20)'),
                C.value('(Cd_Filial)[1]', 'varchar(50)'),
                C.value('(Cd_Cargo)[1]', 'varchar(50)'), -- [INICIO - ICTRL-FX-202508-001]
                1
            FROM @XmlData.nodes('/Root/Consumidor') AS T(C);


            -- TRATAR A TABELA DE CENTRO_CUSTO ANTES DE INSERIR OS DADOS DO CONSUMIDOR

            -- MERGE NA TABELA Centro_Custo A PARTIR DA Centro_Custo_Staging
            MERGE Centro_Custo AS T
            USING (
                SELECT DISTINCT
                    Cd_Centro_Custo,
                    Nm_Centro_Custo,
                    ISNULL(Fl_Desativado, 0) AS Fl_Desativado -- Garantir valor padrao para Fl_Desativado
                FROM Centro_Custo_Staging
            ) AS SRC
            ON T.Cd_Centro_Custo = SRC.Cd_Centro_Custo
            WHEN MATCHED THEN
                UPDATE SET
                    T.Nm_Centro_Custo = SRC.Nm_Centro_Custo,
                    T.Fl_Desativado = SRC.Fl_Desativado
            WHEN NOT MATCHED BY TARGET THEN
                INSERT (
                    Cd_Centro_Custo,
                    Nm_Centro_Custo,
                    Fl_Desativado
                )
                VALUES (
                    SRC.Cd_Centro_Custo,
                    SRC.Nm_Centro_Custo,
                    SRC.Fl_Desativado
                );


            -- ASSOCIAR OS IDS GERADOS NO MAP Centro_Custo_Map
            INSERT INTO Centro_Custo_Map (Id_Centro_Custo, Cd_Centro_Custo)
            SELECT DISTINCT T.Id_Centro_Custo, T.Cd_Centro_Custo
            FROM Centro_Custo T
            WHERE NOT EXISTS (
                SELECT 1 FROM Centro_Custo_Map M
                WHERE M.Cd_Centro_Custo = T.Cd_Centro_Custo
            );

            -- ATUALIZAR OS IDS DE Centro_Custo EM Consumidor_Staging (direto da tabela principal)
            UPDATE CS
            SET CS.Id_Centro_Custo = CC.Id_Centro_Custo
            FROM Consumidor_Staging CS
            JOIN dbo.Centro_Custo CC ON CS.Cd_Centro_Custo = CC.Cd_Centro_Custo;


            -- Garante que todos os Centros de Custo da carga de Consumidores existam na tabela principal.
            MERGE dbo.Centro_Custo AS T
            USING (
                SELECT DISTINCT Cd_Centro_Custo
                FROM dbo.Consumidor_Staging
                WHERE Cd_Centro_Custo IS NOT NULL AND LTRIM(RTRIM(Cd_Centro_Custo)) <> ''
            ) AS S
            ON T.Cd_Centro_Custo = S.Cd_Centro_Custo
            WHEN NOT MATCHED BY TARGET THEN
                INSERT (Cd_Centro_Custo, Nm_Centro_Custo, Fl_Desativado)
                VALUES (S.Cd_Centro_Custo, S.Cd_Centro_Custo, 0);

            -- [FIX 2026-04-07] Garante que todos os Cargos da carga de Consumidores existam na tabela principal.
            MERGE dbo.Cargo AS T
            USING (
                SELECT DISTINCT Cd_Cargo
                FROM dbo.Consumidor_Staging
                WHERE Cd_Cargo IS NOT NULL AND LTRIM(RTRIM(Cd_Cargo)) <> ''
            ) AS S
            ON T.Cd_Cargo = S.Cd_Cargo
            WHEN NOT MATCHED BY TARGET THEN
                INSERT (Cd_Cargo, Nm_Cargo, Fl_Desativado)
                VALUES (S.Cd_Cargo, S.Cd_Cargo, 0);

            -- Atualiza os IDs na tabela de Staging, buscando diretamente das tabelas principais para maior seguranca.
            UPDATE CS SET CS.Id_Centro_Custo = CC.Id_Centro_Custo FROM dbo.Consumidor_Staging CS JOIN dbo.Centro_Custo CC ON CS.Cd_Centro_Custo = CC.Cd_Centro_Custo;
            UPDATE CS SET CS.Id_Filial = F.Id_Filial FROM dbo.Consumidor_Staging CS JOIN dbo.Filial F ON CS.Cd_Filial = F.Cd_Filial;
            UPDATE CS SET CS.Id_Cargo = C.Id_Cargo FROM dbo.Consumidor_Staging CS JOIN dbo.Cargo C ON CS.Cd_Cargo = C.Cd_Cargo;
            UPDATE CS SET CS.Id_Departamento = D.Id_Departamento FROM dbo.Consumidor_Staging CS JOIN dbo.Departamento D ON CS.Cd_Departamento = D.Cd_Departamento;

            -- Resolve Id_Consumidor_Status a partir do codigo (A, T, S, D, F, O, P, R, U)
            -- recebido do SF via Cd_Consumidor_Status.
            UPDATE CS
            SET CS.Id_Consumidor_Status = S.Id_Consumidor_Status
            FROM dbo.Consumidor_Staging AS CS
            JOIN dbo.Consumidor_Status AS S ON CS.Cd_Consumidor_Status = S.cd_Status;

            -- [FIX 2026-04-07] Removido o fallback "Id_Consumidor_Status = 6 WHERE IS NULL".
            -- Deixar NULL aqui permite que o MERGE abaixo preserve o status antigo do
            -- consumidor existente via ISNULL(SRC.Id_Consumidor_Status, T.Id_Consumidor_Status)
            -- e use 6 (Invalido) apenas no INSERT de consumidores novos.

            -- ATUALIZAR Cd_Consumidor BASEADO EM Matricula SE NECESSARIO
            UPDATE T
            SET T.Cd_Consumidor = S.Cd_Consumidor
            FROM Consumidor T
            INNER JOIN Consumidor_Staging S
                 ON T.Matricula = S.Matricula
                 AND T.Cd_Consumidor IS NULL;

            DECLARE @MaxIdConsumidor INT;

            -- Obter o maior Id_Consumidor ja existente na tabela Consumidor
            SELECT @MaxIdConsumidor = ISNULL(MAX(Id_Consumidor), 0) FROM Consumidor;

            -- Atribuir novos Id_Consumidor incrementais para os registros que nao possuem
            ;WITH CTE AS (
                SELECT CS.*,
                    ROW_NUMBER() OVER (ORDER BY Matricula) AS RN
                FROM Consumidor_Staging CS
                WHERE Id_Consumidor IS NULL
            )
            UPDATE CTE
            SET Id_Consumidor = @MaxIdConsumidor + RN;


            DECLARE @ConsumidorOutput TABLE (
                Id_Consumidor INT,
                Cd_Consumidor VARCHAR(50)
            );

            MERGE Consumidor AS T
            USING (
                SELECT
                    Cd_Consumidor,
                    Nm_Consumidor,
                    Matricula,
                    EMail,
                    Id_Consumidor_Status,
                    Matricula_Chefia,
                    Fl_Desativado,
                    Nm_Cidade,
                    Nm_Estado,
                    Dt_Ativacao_Consumidor,
                    Dt_Desativacao_Consumidor,
                    Id_Cargo,
                    Id_Departamento,
                    Id_Centro_Custo,
                    Id_Filial,
                    Id_Consumidor_Tipo
                FROM (
                    SELECT
                        Cd_Consumidor,
                        Nm_Consumidor,
                        Matricula,
                        EMail,
                        Id_Consumidor_Status,
                        Matricula_Chefia,
                        Fl_Desativado,
                        Nm_Cidade,
                        Nm_Estado,
                        Dt_Ativacao_Consumidor,
                        Dt_Desativacao_Consumidor,
                        Id_Cargo,
                        Id_Departamento,
                        Id_Centro_Custo,
                        Id_Filial,
                        Id_Consumidor_Tipo,
                        ROW_NUMBER() OVER (PARTITION BY Matricula ORDER BY Dt_Ativacao_Consumidor DESC) AS RowNum
                    FROM Consumidor_Staging
                ) AS Deduplicated
                WHERE RowNum = 1
            ) AS SRC
            ON T.Matricula = SRC.Matricula
            WHEN MATCHED THEN
                -- [FIX 2026-04-07] FKs e Status passam a usar ISNULL(SRC.X, T.X)
                -- para nao zerar o valor existente quando o codigo recebido nao
                -- for localizado nas tabelas de dominio.
                UPDATE SET
                    T.Cd_Consumidor          = SRC.Cd_Consumidor,
                    T.Nm_Consumidor          = SRC.Nm_Consumidor,
                    T.Matricula              = SRC.Matricula,
                    T.EMail                  = SRC.EMail,
                    T.Id_Consumidor_Status   = ISNULL(SRC.Id_Consumidor_Status, T.Id_Consumidor_Status),
                    T.Matricula_Chefia       = SRC.Matricula_Chefia,
                    T.Fl_Desativado          = SRC.Fl_Desativado,
                    T.Nm_Cidade              = SRC.Nm_Cidade,
                    T.Nm_Estado              = SRC.Nm_Estado,
                    T.Dt_Ativacao_Consumidor = SRC.Dt_Ativacao_Consumidor,
                    T.Dt_Desativacao_Consumidor = SRC.Dt_Desativacao_Consumidor,
                    T.Id_Cargo               = ISNULL(SRC.Id_Cargo, T.Id_Cargo),
                    T.Id_Departamento        = ISNULL(SRC.Id_Departamento, T.Id_Departamento),
                    T.Id_Centro_Custo        = ISNULL(SRC.Id_Centro_Custo, T.Id_Centro_Custo),
                    T.Id_Filial              = ISNULL(SRC.Id_Filial, T.Id_Filial)
            WHEN NOT MATCHED BY TARGET THEN
                INSERT (
                    Cd_Consumidor,
                    Nm_Consumidor,
                    Matricula,
                    EMail,
                    Id_Consumidor_Status,
                    Matricula_Chefia,
                    Fl_Desativado,
                    Nm_Cidade,
                    Nm_Estado,
                    Dt_Ativacao_Consumidor,
                    Dt_Desativacao_Consumidor,
                    Id_Cargo,
                    Id_Departamento,
                    Id_Centro_Custo,
                    Id_Filial,
                    Id_Consumidor_Tipo
                )
                VALUES (
                    ISNULL(SRC.Cd_Consumidor, ''),
                    ISNULL(SRC.Nm_Consumidor, ''),
                    ISNULL(SRC.Matricula, ''),
                    ISNULL(SRC.EMail, ''),
                    ISNULL(SRC.Id_Consumidor_Status, 6),
                    ISNULL(SRC.Matricula_Chefia, ''),
                    ISNULL(SRC.Fl_Desativado, 0),
                    ISNULL(SRC.Nm_Cidade, ''),
                    ISNULL(SRC.Nm_Estado, ''),
                    SRC.Dt_Ativacao_Consumidor,
                    SRC.Dt_Desativacao_Consumidor,
                    SRC.Id_Cargo,
                    SRC.Id_Departamento,
                    SRC.Id_Centro_Custo,
                    SRC.Id_Filial,
                    ISNULL(SRC.Id_Consumidor_Tipo, 1)
                )
                OUTPUT INSERTED.Id_Consumidor, SRC.Cd_Consumidor INTO @ConsumidorOutput(Id_Consumidor, Cd_Consumidor);


            -- RELACIONAMENTOS NAS TABELAS INTERMEDIARIAS
            -- Relacionamento entre Centro_Custo e Departamento
            INSERT INTO Rl_Hi_Centro_Custo_Departamento (Id_Centro_Custo, Id_Departamento)
            SELECT DISTINCT CS.Id_Centro_Custo, CS.Id_Departamento
            FROM Consumidor_Staging CS
            WHERE CS.Id_Centro_Custo IS NOT NULL
              AND CS.Id_Departamento IS NOT NULL
              AND NOT EXISTS (
                  SELECT 1
                  FROM Rl_Hi_Centro_Custo_Departamento R
                  WHERE R.Id_Centro_Custo = CS.Id_Centro_Custo
                    AND R.Id_Departamento = CS.Id_Departamento
              );

            -- Relacionamento entre Filial e Centro_Custo
            INSERT INTO Rl_Hi_Filial_Centro_Custo (Id_Filial, Id_Centro_Custo)
            SELECT DISTINCT CS.Id_Filial, CS.Id_Centro_Custo
            FROM Consumidor_Staging CS
            WHERE CS.Id_Filial IS NOT NULL
              AND CS.Id_Centro_Custo IS NOT NULL
              AND NOT EXISTS (
                  SELECT 1
                  FROM Rl_Hi_Filial_Centro_Custo R
                  WHERE R.Id_Filial = CS.Id_Filial
                    AND R.Id_Centro_Custo = CS.Id_Centro_Custo
              );
        END

        ELSE
        BEGIN
            RAISERROR('Acao invalida: %s', 16, 1, @pAcao);
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000),
                @ErrorSeverity INT,
                @ErrorState INT,
                @ErrorLine INT,
                @ProcedureName NVARCHAR(128),
                @ErrorDetails NVARCHAR(MAX);

        SET @ErrorMessage = ERROR_MESSAGE();
        SET @ErrorSeverity = ERROR_SEVERITY();
        SET @ErrorState = ERROR_STATE();
        SET @ErrorLine = ERROR_LINE();
        SET @ProcedureName = OBJECT_NAME(@@PROCID);

        SET @ErrorDetails = N'Erro na execucao. Detalhes: ' +
                            N'Procedimento: ' + ISNULL(@ProcedureName, 'Desconhecido') + CHAR(13) +
                            N'Linha: ' + CAST(@ErrorLine AS NVARCHAR(10)) + CHAR(13) +
                            N'Mensagem: ' + @ErrorMessage + CHAR(13) +
                            N'Gravidade: ' + CAST(@ErrorSeverity AS NVARCHAR(10)) + CHAR(13) +
                            N'Estado: ' + CAST(@ErrorState AS NVARCHAR(10));

        RAISERROR(@ErrorDetails, @ErrorSeverity, @ErrorState);
    END CATCH

END;
