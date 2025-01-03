USE [Amil]
GO
/****** Object:  StoredProcedure [dbo].[pa_Fatura_Plano_Conta]    Script Date: 18/03/2024 08:19:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pa_Fatura_Plano_Conta]   
    (@pPAKAGE VARCHAR(200),   
    @pId_Fatura_Plano_Conta INT   = NULL,   
    @pNr_Plano_Conta  VARCHAR(50) = NULL,   
    @pId_Conglomerado  INT   = NULL,   
    @pId_Empresa   INT   = NULL,   
    @pId_Contrato   INT   = NULL,   
    @pDia_Vencimento  INT   = NULL,   
    @pLote_Cancelamento  DATETIME = NULL,   
    @pDescricao    VARCHAR(100)= NULL,   
    @pDt_Lote    VARCHAR(6) = NULL,   
    @pId_Usuario_Permissao INT   = NULL,   
    @pId_Ativo_Tipo_Grupo INT   = NULL)   
AS   
-----consulta plano de conta                  
IF @pPAKAGE = 'sp_Consulta_Plano_Conta'                  
BEGIN   
    -----caso nao encontre registro para conglomerado deve ser criado um                   
    IF NOT EXISTS(SELECT Id_Fatura_Plano_Conta   
    FROM Fatura_Plano_Conta FPC   
        INNER JOIN Ativo A ON A.Id_Conglomerado = FPC.Id_Conglomerado   
        INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = a.Id_Ativo_Tipo   
        INNER JOIN Ativo_Tipo_Grupo ATG ON ATG.Id_Ativo_Tipo_Grupo = AT.Id_Ativo_Tipo_Grupo   
    WHERE ATG.Id_Ativo_Tipo_Grupo = @pId_Ativo_Tipo_Grupo   
        OR FPC.Id_Conglomerado = @pId_Conglomerado)                  
    BEGIN   
        INSERT INTO Fatura_Plano_Conta   
            (Nr_Plano_Conta,   
            Id_Conglomerado,   
            Id_Empresa,   
            Id_Contrato,   
            Dia_Vencimento,   
            Lote_Cancelamento,   
            Descricao)   
        VALUES( NULL,   
                @pId_Conglomerado,   
                @pId_Empresa,   
                @pId_Contrato,   
                NULL,   
                NULL,   
                @pDescricao)   
    END   
   
            SELECT Id_Fatura_Plano_Conta AS Id_Fatura_Plano_Conta,   
            Nr_Plano_Conta AS Nr_Plano_Conta,   
            Id_Conglomerado AS Id_Conglomerado,   
            Id_Empresa AS Id_Empresa,   
            Id_Contrato AS Id_Contrato,   
            Dia_Vencimento AS Dia_Vencimento,   
            Lote_Cancelamento AS Lote_Cancelamento,   
            Descricao AS Descricao   
        FROM Fatura_Plano_Conta   
        WHERE Id_Conglomerado = @pId_Conglomerado   
    UNION   
        SELECT Id_Fatura_Plano_Conta AS Id_Fatura_Plano_Conta,   
            Nr_Plano_Conta AS Nr_Plano_Conta,   
            FPC.Id_Conglomerado AS Id_Conglomerado,   
            Id_Empresa AS Id_Empresa,   
            Id_Contrato AS Id_Contrato,   
            Dia_Vencimento AS Dia_Vencimento,   
            Lote_Cancelamento AS Lote_Cancelamento,   
            Descricao AS Descricao   
        FROM Fatura_Plano_Conta FPC   
            INNER JOIN Ativo A ON A.Id_Conglomerado = FPC.Id_Conglomerado   
            INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo   
            INNER JOIN Ativo_Tipo_Grupo ATG ON ATG.Id_Ativo_Tipo_Grupo = AT.Id_Ativo_Tipo_Grupo   
        WHERE ATG.Id_Ativo_Tipo_Grupo = @pId_Ativo_Tipo_Grupo   
   
    ORDER BY Id_Conglomerado, Nr_Plano_Conta   
END   
   
-----atualiza registro                  
IF @pPAKAGE = 'sp_Atualiza_Plano_Conta'                  
BEGIN   
    IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Alterar') = 2                  
 BEGIN   
        UPDATE Fatura_Plano_Conta                  
  SET  Nr_Plano_Conta  = @pNr_Plano_Conta,                  
    Id_Conglomerado  = @pId_Conglomerado,                  
    Id_Empresa   = @pId_Empresa,                  
    Id_Contrato   = @pId_Contrato,                  
    Dia_Vencimento  = @pDia_Vencimento,                  
    Lote_Cancelamento = @pLote_Cancelamento,                  
    Descricao   = @pDescricao                  
  WHERE Id_Fatura_Plano_Conta = @pId_Fatura_Plano_Conta   
    END   
END   
   
-----insere registro                  
IF @pPAKAGE = 'sp_Insere_Plano_Conta'                  
BEGIN   
    IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Incluir') = 2                  
 BEGIN   
        INSERT INTO Fatura_Plano_Conta   
            (Nr_Plano_Conta,   
            Id_Conglomerado,   
            Id_Empresa,   
            Id_Contrato,   
      Dia_Vencimento,   
            Lote_Cancelamento,   
            Descricao)   
        VALUES( @pNr_Plano_Conta,   
                @pId_Conglomerado,   
                @pId_Empresa,   
                @pId_Contrato,   
                @pDia_Vencimento,   
                @pLote_Cancelamento,   
                @pDescricao)   
    END   
END   
   
-----desativa resgistro                  
IF @pPAKAGE = 'sp_SE'                  
BEGIN   
    IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Excluir') = 2                  
 BEGIN   
        DELETE Fatura_Plano_Conta WHERE Id_Fatura_Plano_Conta = @pId_Fatura_Plano_Conta   
    END   
END   
   
IF @pPAKAGE = 'sp_Retorna_Id_Dw'                 
BEGIN 
    SELECT 
        ISNULL((SELECT TOP 1 Id_Fatura  
        FROM Fatura 
        WHERE (Nr_Fatura = FPC.Nr_Plano_Conta) 
            AND (Id_Fatura IS NOT NULL)
            AND (SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)), 0) AS Id_Fatura, 
 
        ISNULL((SELECT TOP 1 
            Id_Bilhete 
        FROM Bilhete 
        WHERE DC_Nr_Nota_Fiscal LIKE FPC.Nr_Plano_Conta), 0) AS Id_Bilhete, 
 
        ISNULL((SELECT TOP 1 
            Id_Nota_Fiscal 
        FROM Nota_Fiscal_Fatura 
        WHERE (Id_Nota_Fiscal IS NOT NULL) 
        AND Id_Fatura IN (SELECT Id_Fatura 
        FROM Fatura 
        WHERE (Nr_Fatura = FPC.Nr_Plano_Conta) 
            AND (Id_Fatura IS NOT NULL)
            AND (SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote))),0) AS Id_NF, 
 
        ISNULL((SELECT TOP 1 Id_Rateio 
        FROM Rateio 
        WHERE Nm_Rateio LIKE CONCAT('%',FPC.Nr_Plano_Conta,'%')),0) AS Id_Rateio 
 
    FROM Fatura_Plano_Conta FPC 
    WHERE Nr_Plano_Conta LIKE @pNr_Plano_Conta 
END 
  
-----consulta plano de conta                  
IF @pPAKAGE = 'sp_Confere_Plano_Conta_V3'                  
BEGIN  
    ---plano de conta                  
    SELECT Operadora, Fatura, Conta_Carregada, Qtd_Linha, Total, Total_Carregado, Total_Fatura, Id_Fatura, Total_Rateado,
			   Cadastrada, Data_Vencimento, Data_Cancelamento, Flag_Conta_Cadastrada, Flag_Conta_Carregada, Visible_LINK,
			   Download_Fatura, Download_Boleto, Download_NF, --Download_Rateio, 
			   ISNULL((SELECT TOP 1 CASE WHEN Id_Rateio IS NULL THEN 'RED' ELSE 'GREEN' END AS Dw 
				FROM Rateio 
				WHERE Nm_Rateio LIKE CONCAT('%',Fatura,'%')), 'RED') AS Download_Rateio
			   , Dt_Emissao, Dt_Vencimento,
			   NF, Pedido, Req
		FROM(    
		SELECT  
            (SELECT Nm_Conglomerado  
            FROM Conglomerado  
            WHERE Id_Conglomerado = FPC.Id_Conglomerado) AS Operadora,  
  
            FPC.Nr_Plano_Conta AS Fatura,  
  
            CASE WHEN EXISTS(SELECT Id_Lote  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta AND (Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = FPC.Id_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)) THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            ISNULL(CAST((SELECT Vr_Fatura  
            FROM Fatura  
            WHERE Nr_Fatura = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Fatura,  
  
            ISNULL((SELECT Id_Fatura  
            FROM Fatura  
            WHERE Nr_Fatura = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Id_Fatura,  
  
  
            ISNULL(CAST((SELECT SUM(Valor_Rateado)  
            FROM Rateio_Item  
            WHERE Id_Rateio IN( SELECT RF.Id_Rateio  
            FROM Fatura FA INNER JOIN Rl_Rateio_Fatura RF ON (RF.Id_Fatura = FA.Id_Fatura)  
            WHERE FA.Nr_Fatura =  FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)) AS DECIMAL(10,2)), 0) AS Total_Rateado,  
  
            'Grid_Check.png' AS Cadastrada,  
  
            FPC.Dia_Vencimento AS Data_Vencimento,  
  
            CONVERT(VARCHAR(10), FPC.Lote_Cancelamento, 103) AS Data_Cancelamento,  
  
            2 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN EXISTS(SELECT Id_Lote  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = FPC.Id_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            CASE WHEN ISNULL((SELECT Id_Fatura  
            FROM Fatura  
            WHERE (Nr_Fatura = FPC.Nr_Plano_Conta)  
                AND (SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)), 0) = 0 THEN 'FALSE' ELSE 'TRUE' END AS Visible_LINK,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE (Tabela_Registro LIKE 'Fatura')  
                AND (Id_Registro_Tabela IN (SELECT Id_Fatura  
                FROM Fatura  
                WHERE Nr_Fatura LIKE FPC.Nr_Plano_Conta
                AND (SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)))), 'RED') AS Download_Fatura,  
  
            ISNULL((SELECT TOP 1
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw
            FROM Arquivo_PDF
            WHERE (Tabela_Registro LIKE 'Boleto')
                AND (Id_Registro_Tabela IN (SELECT Id_Fatura
                FROM Fatura
                WHERE Nr_Fatura LIKE FPC.Nr_Plano_Conta
                AND (SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)))), 'RED') AS Download_Boleto, 
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE (Tabela_Registro LIKE 'Nota_Fiscal_Fatura')  
                AND (Id_Registro_Tabela IN (SELECT Id_Nota_Fiscal  
                FROM Nota_Fiscal_Fatura  
                WHERE Id_Fatura IN (SELECT Id_Fatura  
                FROM Fatura  
                WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)
                AND (SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote))))), 'RED') AS Download_NF,  
  
            --ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            --FROM Arquivo_PDF  
            --WHERE (Tabela_Registro LIKE 'Rateio') AND (Id_Registro_Tabela IN (SELECT Id_Rateio  
            --    FROM Rateio  
            --    WHERE Nm_Rateio LIKE CONCAT('%',FPC.Nr_Plano_Conta,'%')))), 'RED') AS Download_Rateio,   

   --         ISNULL((SELECT TOP 1 (CASE WHEN Id_Rateio IS NOT NULL THEN 'GREEN' ELSE 'RED' END) Dw 
			--FROM Rateio 
			--WHERE Nm_Rateio LIKE CONCAT('%',FPC.Nr_Plano_Conta,'%')), 'RED') Download_Rateio, 
  
            (SELECT CONVERT(VARCHAR, Dt_Emissao, 103)  
            FROM Fatura  
            WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)  
                AND (Dt_Lote LIKE @pDt_Lote)) AS Dt_Emissao,  
  
            (SELECT CONVERT(VARCHAR, Dt_Vencimento, 103)  
            FROM Fatura  
            WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)  
                AND (Dt_Lote LIKE @pDt_Lote)) AS Dt_Vencimento,  
  
            (SELECT Nota_Fiscal  
            FROM Fatura  
            WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)  
                AND (Dt_Lote LIKE @pDt_Lote)) AS NF,  
  
            (SELECT Pedido  
            FROM Fatura  
            WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)  
                AND (Dt_Lote LIKE @pDt_Lote)) AS Pedido,  
  
            (SELECT Req  
            FROM Fatura  
            WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)  
                AND (Dt_Lote LIKE @pDt_Lote)) AS Req  
  
        FROM Fatura_Plano_Conta FPC  
        WHERE FPC.Id_Conglomerado IN (SELECT Id_Conglomerado  
        FROM Ativo  
        WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo IN (SELECT Id_Ativo_Tipo  
            FROM Ativo_Tipo  
            WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo_Grupo = @pId_Ativo_Tipo_Grupo))  
  
    UNION  
        -----fatura recepcionada e nao cadastrada no plano de conta                  
        SELECT  
            (SELECT Nm_Conglomerado  
            FROM Conglomerado  
            WHERE Id_Conglomerado = (SELECT Id_Conglomerado  
            FROM Fatura_Plano_Conta  
            WHERE Nr_Plano_Conta LIKE F.Nr_Fatura)) AS Operadora,  
  
            F.Nr_Fatura AS Fatura,  
  
            CASE WHEN EXISTS(SELECT Id_Lote  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado IN (SELECT Id_Conglomerado  
                FROM Fatura_Plano_Conta  
                WHERE Nr_Plano_Conta LIKE F.Nr_Fatura))  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            NULL AS Total_Fatura,  
  
            NULL AS Id_Fatura,  
  
            NULL AS Total_Rateado,  
  
            'Grid_Cancel.png' AS Cadastrada,  
  
            NULL AS Data_Vencimento,  
  
            NULL AS Data_Cancelamento,  
  
            1 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN EXISTS(SELECT Id_Lote  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado  IN (SELECT Id_Conglomerado  
                FROM Fatura_Plano_Conta  
                WHERE Nr_Plano_Conta LIKE F.Nr_Fatura))  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            'FALSE' AS Visible_LINK,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Fatura' 
                AND Id_Registro_Tabela = F.Id_Fatura), 'RED') AS Download_Fatura,  
  
            ISNULL((SELECT TOP 1
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw
            FROM Arquivo_PDF
            WHERE (Tabela_Registro LIKE 'Boleto') 
                AND (Id_Registro_Tabela = F.Id_Fatura)), 'RED') AS Download_Boleto,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Nota_Fiscal_Fatura'  
                AND Id_Registro_Tabela IN (SELECT Id_Nota_Fiscal  
                FROM Nota_Fiscal_Fatura  
                WHERE Id_Fatura = F.Id_Fatura)), 'RED') AS Download_NF,  
  
            --ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            --FROM Arquivo_PDF  
            --WHERE Tabela_Registro LIKE 'Rateio' AND Id_Registro_Tabela IN (SELECT  
            --        Id_Rateio  
            --    FROM Rateio  
            --    WHERE Nm_Rateio LIKE CONCAT('%',F.Nr_Fatura,'%'))), 'RED') AS Download_Rateio, 

   --         ISNULL((SELECT TOP 1 (CASE WHEN Id_Rateio IS NOT NULL THEN 'GREEN' ELSE 'RED' END) Dw  
			--FROM Rateio 
			--WHERE Nm_Rateio LIKE CONCAT('%',F.Nr_Fatura,'%')), 'RED') Download_Rateio,  
  
            CONVERT(VARCHAR, F.Dt_Emissao, 103) AS Dt_Emissao,  
  
            CONVERT(VARCHAR, F.Dt_Vencimento, 103) AS Dt_Vencimento,  
  
            F.Nota_Fiscal AS NF,  
  
            F.Pedido AS Pedido,  
  
            F.Req AS Req  
  
        FROM Fatura F  
        WHERE F.Id_Fatura_Parametro IN (SELECT Id_Fatura_Parametro  
            FROM Fatura_Parametro  
            WHERE Id_Ativo IN ((SELECT Id_Ativo  
            FROM Ativo  
            WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo IN (SELECT Id_Ativo_Tipo  
                FROM Ativo_Tipo  
                WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo_Grupo = @pId_Ativo_Tipo_Grupo))))  
            AND NOT EXISTS(SELECT Id_Fatura_Plano_Conta  
            FROM Fatura_Plano_Conta  
            WHERE Nr_Plano_Conta = F.Nr_Fatura)  
            AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote  
  
    UNION  
        ---fatura carregada e nao cadastrada no plano de mconta                  
        SELECT  
            (SELECT Nm_Conglomerado  
            FROM Conglomerado  
            WHERE Id_Conglomerado IN (SELECT Id_Conglomerado  
            FROM Ativo  
            WHERE Id_Ativo = L.Id_Ativo AND Fl_Desativado = 0) ) AS Operadora,  
  
            L.DC_Nr_Nota_Fiscal AS Fatura,  
  
            CASE WHEN L.DC_Nr_Nota_Fiscal IS NOT NULL THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            NULL AS Total_Fatura,  
  
            NULL AS Id_Fatura,  
  
            NULL AS Total_Rateado,  
  
            'Grid_Cancel.png' AS Cadastrada,  
  
            NULL AS Data_Vencimento,  
  
            NULL AS Data_Cancelamento,  
  
            1 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN L.DC_Nr_Nota_Fiscal IS NOT NULL THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            'FALSE' AS Visible_LINK,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Fatura' AND Id_Registro_Tabela IN (SELECT Id_Fatura  
                FROM Fatura  
                WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal)), 'RED') AS Download_Fatura,  
  
            ISNULL((SELECT TOP 1
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw
            FROM Arquivo_PDF
            WHERE Tabela_Registro LIKE 'Boleto' 
                AND Id_Registro_Tabela IN (SELECT Id_Fatura
                FROM Fatura
                WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal)), 'RED') AS Download_Boleto,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Nota_Fiscal_Fatura'  
                AND Id_Registro_Tabela IN (SELECT  
                    Id_Nota_Fiscal  
                FROM Nota_Fiscal_Fatura  
                WHERE Id_Fatura IN (SELECT Id_Fatura  
                FROM Fatura  
                WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal))), 'RED') AS Download_NF,  
  
            --ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            --FROM Arquivo_PDF  
            --WHERE Tabela_Registro LIKE 'Rateio'  
            --    AND Id_Registro_Tabela IN (SELECT  
            --        Id_Rateio  
            --    FROM Rateio  
            --    WHERE Nm_Rateio LIKE CONCAT('%',L.DC_Nr_Nota_Fiscal,'%'))), 'RED') AS Download_Rateio, 

   --         ISNULL((SELECT TOP 1 (CASE WHEN Id_Rateio IS NOT NULL THEN 'GREEN' ELSE 'RED' END) Dw  
			--FROM Rateio 
			--WHERE Nm_Rateio LIKE CONCAT('%',L.DC_Nr_Nota_Fiscal,'%')), 'RED') Download_Rateio,  
  
            CONVERT(VARCHAR, (SELECT Dt_Emissao  
            FROM Fatura  
            WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 103) AS Dt_Emissao,  
  
            CONVERT(VARCHAR, (SELECT Dt_Vencimento  
            FROM Fatura  
            WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 103) AS Dt_Vencimento,  
  
            (SELECT Nota_Fiscal  
            FROM Fatura  
            WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS NF,  
  
            (SELECT Pedido  
            FROM Fatura  
            WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS Pedido,  
  
            (SELECT Req  
            FROM Fatura  
            WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS Req  
  
        FROM Lote L  
        WHERE L.Id_Ativo IN ((SELECT Id_Ativo  
            FROM Ativo  
            WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo IN (SELECT Id_Ativo_Tipo  
                FROM Ativo_Tipo  
                WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo_Grupo = @pId_Ativo_Tipo_Grupo)))  
            AND NOT EXISTS(SELECT Id_Ativo  
            FROM Fatura_Plano_Conta  
            WHERE Nr_Plano_Conta = L.DC_Nr_Nota_Fiscal)  
            AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote  
    ) A
    ORDER BY Fatura  
END  
  
-----consulta plano de conta                  
IF @pPAKAGE = 'sp_Confere_Plano_Conta_V2'                  
BEGIN  
    -----plano de conta                  
                SELECT  
            C.Nm_Conglomerado AS Operadora,  
  
            FPC.Nr_Plano_Conta AS Fatura,  
  
            CASE WHEN L.DC_Nr_Nota_Fiscal IS NOT NULL THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            ISNULL(CAST((SELECT Vr_Fatura  
            FROM Fatura  
            WHERE Nr_Fatura = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Fatura,  
  
            F.Id_Fatura AS Id_Fatura,  
  
            ISNULL(CAST((SELECT SUM(Valor_Rateado)  
            FROM Rateio_Item  
            WHERE Id_Rateio IN( SELECT RF.Id_Rateio  
            FROM Fatura FA INNER JOIN Rl_Rateio_Fatura RF ON (RF.Id_Fatura = FA.Id_Fatura)  
            WHERE FA.Nr_Fatura =  FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)) AS DECIMAL(10,2)), 0) AS Total_Rateado,  
  
            'Grid_Check.png' AS Cadastrada,  
  
            FPC.Dia_Vencimento AS Data_Vencimento,  
  
            CONVERT(VARCHAR(10), FPC.Lote_Cancelamento, 103) AS Data_Cancelamento,  
  
            2 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN L.DC_Nr_Nota_Fiscal IS NOT NULL THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            CASE WHEN F.Id_Fatura IS NOT NULL THEN 'TRUE' ELSE 'FALSE' END AS Visible_LINK,  
  
            ISNULL((SELECT (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Fatura' AND Id_Registro_Tabela = F.Id_Fatura), 'RED') AS Download_Fatura,  
  
            ISNULL((SELECT TOP 1  
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Bilhete'  
                AND Id_Registro_Tabela = (SELECT TOP 1  
                    Id_Bilhete  
                FROM Bilhete  
                WHERE Id_Ativo = A.Id_Ativo  
                    AND Id_Bilhete_Tipo = (SELECT TOP 1  
                        Id_Bilhete_Tipo  
                    FROM Bilhete_Tipo  
                    WHERE Id_Conglomerado = C.Id_Conglomerado)  
                    AND Id_Lote = L.Id_Lote)), 'RED') AS Download_Boleto,  
  
            ISNULL((SELECT TOP 1  
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Nota_Fiscal_Fatura'  
                AND Id_Registro_Tabela = (SELECT TOP 1  
                    Id_Nota_Fiscal  
                FROM Nota_Fiscal_Fatura  
                WHERE Id_Fatura = F.Id_Fatura)), 'RED') AS Download_NF,  
  
            ISNULL((SELECT TOP 1  
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Rateio'  
                AND Id_Registro_Tabela = (SELECT TOP 1  
                    Id_Rateio  
                FROM Rateio  
                WHERE Nm_Rateio LIKE CONCAT('%',F.Nr_Fatura,'%'))), 'RED') AS Download_Rateio,  
  
            CASE WHEN F.Dt_Lote LIKE @pDt_Lote THEN CONVERT(VARCHAR, F.Dt_Emissao, 103) ELSE '' END AS Dt_Emissao,  
  
            CASE WHEN F.Dt_Lote LIKE @pDt_Lote THEN CONVERT(VARCHAR, F.Dt_Vencimento, 103) ELSE '' END AS Dt_Vencimento,  
  
            CASE WHEN F.Dt_Lote LIKE @pDt_Lote THEN F.Nota_Fiscal ELSE '' END AS NF,  
  
            CASE WHEN F.Dt_Lote LIKE @pDt_Lote THEN F.Pedido ELSE '' END AS Pedido,  
  
            CASE WHEN F.Dt_Lote LIKE @pDt_Lote THEN F.Req ELSE '' END AS Req,  
  
            F.Dt_Lote AS Dt_Lote  
  
        FROM Fatura_Plano_Conta FPC  
            INNER JOIN Ativo A ON A.Id_Conglomerado = FPC.Id_Conglomerado  
            INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = a.Id_Ativo_Tipo  
            INNER JOIN Ativo_Tipo_Grupo ATG ON ATG.Id_Ativo_Tipo_Grupo = AT.Id_Ativo_Tipo_Grupo  
            INNER JOIN Fatura F ON FPC.Nr_Plano_Conta = F.Nr_Fatura  
            LEFT JOIN Lote L ON (L.DC_Nr_Nota_Fiscal LIKE FPC.Nr_Plano_Conta) AND (SUBSTRING(CONVERT(VARCHAR(6),L.Dt_Lote,112),1,6) = @pDt_Lote)  
            INNER JOIN Conglomerado C ON c.Id_Conglomerado = FPC.Id_Conglomerado  
        WHERE ATG.Id_Ativo_Tipo_Grupo = @pId_Ativo_Tipo_Grupo  
        GROUP BY FPC.Nr_Plano_Conta, L.DC_Nr_Nota_Fiscal, F.Id_Fatura, FPC.Dia_Vencimento,            
            FPC.Lote_Cancelamento, C.Nm_Conglomerado, F.Dt_Emissao, F.Dt_Vencimento,     
            L.Id_Lote, A.Id_Ativo, C.Id_Conglomerado, F.Nota_Fiscal, F.Pedido,F.Dt_Lote,    
            F.Req, F.Nr_Fatura  
  
    UNION  
        -----fatura recepcionada e nao cadastrada no plano de conta                  
        SELECT  
            C.Nm_Conglomerado AS Operadora,  
  
            F.Nr_Fatura AS Fatura,  
  
            CASE WHEN L.DC_Nr_Nota_Fiscal IS NOT NULL THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            NULL AS Total_Fatura,  
  
            NULL AS Id_Fatura,  
  
            NULL AS Total_Rateado,  
  
            'Grid_Cancel.png' AS Cadastrada,  
  
            NULL AS Data_Vencimento,  
  
            NULL AS Data_Cancelamento,  
  
            1 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN L.DC_Nr_Nota_Fiscal IS NOT NULL THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            'FALSE' AS Visible_LINK,  
  
            ISNULL((SELECT (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Fatura' AND Id_Registro_Tabela = F.Id_Fatura), 'RED') AS Download_Fatura,  
  
            ISNULL((SELECT TOP 1  
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Bilhete'  
                AND Id_Registro_Tabela = (SELECT TOP 1  
                    Id_Bilhete  
                FROM Bilhete  
                WHERE Id_Ativo = A.Id_Ativo  
                    AND Id_Bilhete_Tipo = (SELECT TOP 1  
                        Id_Bilhete_Tipo  
                    FROM Bilhete_Tipo  
                    WHERE Id_Conglomerado = C.Id_Conglomerado)  
                    AND Id_Lote = L.Id_Lote)), 'RED') AS Download_Boleto,  
  
            ISNULL((SELECT TOP 1  
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Nota_Fiscal_Fatura'  
                AND Id_Registro_Tabela = (SELECT TOP 1  
                    Id_Nota_Fiscal  
                FROM Nota_Fiscal_Fatura  
                WHERE Id_Fatura = F.Id_Fatura)), 'RED') AS Download_NF,  
  
            ISNULL((SELECT TOP 1  
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Rateio'  
                AND Id_Registro_Tabela = (SELECT TOP 1  
                    Id_Rateio  
                FROM Rateio  
                WHERE Nm_Rateio LIKE CONCAT('%',F.Nr_Fatura,'%'))), 'RED') AS Download_Rateio,  
  
            CONVERT(VARCHAR, F.Dt_Emissao, 103) AS Dt_Emissao,  
  
            CONVERT(VARCHAR, F.Dt_Vencimento, 103) AS Dt_Vencimento,  
  
            F.Nota_Fiscal AS NF,  
  
            F.Pedido AS Pedido,  
  
            F.Req AS Req,  
  
            F.Dt_Lote AS Dt_Lote  
  
        FROM Fatura F  
            INNER JOIN Fatura_Parametro FP ON FP.Id_Fatura_Parametro = F.Id_Fatura_Parametro  
            INNER JOIN Ativo A ON A.Id_Ativo = FP.Id_Ativo  
            INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = a.Id_Ativo_Tipo  
            INNER JOIN Ativo_Tipo_Grupo ATG ON ATG.Id_Ativo_Tipo_Grupo = AT.Id_Ativo_Tipo_Grupo  
            LEFT JOIN Lote L ON (L.DC_Nr_Nota_Fiscal LIKE F.Nr_Fatura) AND (SUBSTRING(CONVERT(VARCHAR(6),L.Dt_Lote,112),1,6) = @pDt_Lote)  
            INNER JOIN Conglomerado C ON c.Id_Conglomerado = A.Id_Conglomerado  
        WHERE F.Nr_Fatura NOT IN (SELECT Nr_Plano_Conta  
            FROM Fatura_Plano_Conta  
            WHERE Nr_Plano_Conta LIKE F.Nr_Fatura)  
            AND F.Dt_Lote LIKE @pDt_Lote  
            AND ATG.Id_Ativo_Tipo_Grupo = @pId_Ativo_Tipo_Grupo  
    UNION  
        -----fatura carregada e nao cadastrada no plano de mconta                  
        SELECT  
            C.Nm_Conglomerado AS Operadora,  
  
            L.DC_Nr_Nota_Fiscal AS Fatura,  
  
            CASE WHEN L.DC_Nr_Nota_Fiscal IS NOT NULL THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            NULL AS Total_Fatura,  
  
            NULL AS Id_Fatura,  
  
            NULL AS Total_Rateado,  
  
            'Grid_Cancel.png' AS Cadastrada,  
  
            NULL AS Data_Vencimento,  
  
            NULL AS Data_Cancelamento,  
  
            1 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN L.DC_Nr_Nota_Fiscal IS NOT NULL THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            'FALSE' AS Visible_LINK,  
  
            ISNULL((SELECT (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Fatura' AND Id_Registro_Tabela = F.Id_Fatura), 'RED') AS Download_Fatura,  
  
            ISNULL((SELECT TOP 1  
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Bilhete'  
                AND Id_Registro_Tabela = (SELECT TOP 1  
                    Id_Bilhete  
                FROM Bilhete  
                WHERE Id_Ativo = A.Id_Ativo  
                    AND Id_Bilhete_Tipo = (SELECT TOP 1  
                        Id_Bilhete_Tipo  
      FROM Bilhete_Tipo  
                    WHERE Id_Conglomerado = C.Id_Conglomerado)  
    AND Id_Lote = L.Id_Lote)), 'RED') AS Download_Boleto,  
  
            ISNULL((SELECT TOP 1  
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Nota_Fiscal_Fatura'  
                AND Id_Registro_Tabela = (SELECT TOP 1  
                    Id_Nota_Fiscal  
                FROM Nota_Fiscal_Fatura  
                WHERE Id_Fatura = F.Id_Fatura)), 'RED') AS Download_NF,  
  
            ISNULL((SELECT TOP 1  
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Rateio'  
                AND Id_Registro_Tabela = (SELECT TOP 1  
                    Id_Rateio  
                FROM Rateio  
                WHERE Nm_Rateio LIKE CONCAT('%',F.Nr_Fatura,'%'))), 'RED') AS Download_Rateio,  
  
            CONVERT(VARCHAR, F.Dt_Emissao, 103) AS Dt_Emissao,  
  
            CONVERT(VARCHAR, F.Dt_Vencimento, 103) AS Dt_Vencimento,  
  
            F.Nota_Fiscal AS NF,  
  
            F.Pedido AS Pedido,  
  
            F.Req AS Req,  
  
            F.Dt_Lote AS Dt_Lote  
  
        FROM Lote L  
            INNER JOIN Ativo A ON A.Id_Ativo = L.Id_Ativo  
            INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = a.Id_Ativo_Tipo  
            INNER JOIN Ativo_Tipo_Grupo ATG ON ATG.Id_Ativo_Tipo_Grupo = AT.Id_Ativo_Tipo_Grupo  
            INNER JOIN Conglomerado C ON c.Id_Conglomerado = A.Id_Conglomerado  
            INNER JOIN Fatura F ON L.DC_Nr_Nota_Fiscal = F.Nr_Fatura  
        WHERE L.Id_Ativo IN (SELECT Id_Ativo  
            FROM Ativo)  
            AND NOT EXISTS(SELECT *  
            FROM Fatura_Plano_Conta  
            WHERE Nr_Plano_Conta = L.DC_Nr_Nota_Fiscal)  
            AND SUBSTRING(CONVERT(VARCHAR(6),L.Dt_Lote,112),1,6) = @pDt_Lote  
            AND ATG.Id_Ativo_Tipo_Grupo = @pId_Ativo_Tipo_Grupo  
        GROUP BY L.DC_Nr_Nota_Fiscal, L.Dt_Liberacao_Lote, C.Nm_Conglomerado,            
            F.Dt_Emissao, F.Dt_Vencimento, F.Nota_Fiscal, F.Pedido, F.Req, F.Id_Fatura,    
            L.DC_Nr_Nota_Fiscal, F.Id_Fatura, C.Nm_Conglomerado, F.Dt_Emissao, F.Dt_Vencimento,     
            L.Id_Lote, A.Id_Ativo, C.Id_Conglomerado, F.Nota_Fiscal, F.Pedido,F.Dt_Lote,    
            F.Req, F.Nr_Fatura  
  
    ORDER BY Fatura  
END  
  
-----consulta plano de conta     
IF @pPAKAGE = 'sp_Confere_Plano_Conta_V1'     
BEGIN  
    -----plano de conta     
                SELECT FPC.Nr_Plano_Conta AS Fatura,  
            CASE WHEN EXISTS(SELECT *  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            ISNULL(CAST((SELECT Vr_Fatura  
            FROM Fatura  
            WHERE Nr_Fatura = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Fatura,  
  
            ISNULL((SELECT Id_Fatura  
            FROM Fatura  
            WHERE Nr_Fatura = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Id_Fatura,  
  
            ISNULL(CAST((SELECT SUM(Valor_Rateado)  
            FROM Rateio_Item  
            WHERE Id_Rateio IN( SELECT RF.Id_Rateio  
            FROM Fatura FA INNER JOIN Rl_Rateio_Fatura RF ON (RF.Id_Fatura = FA.Id_Fatura)  
            WHERE FA.Nr_Fatura =  FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)) AS DECIMAL(10,2)), 0) AS Total_Rateado,  
  
            'Grid_Check.png' AS Cadastrada,  
  
            FPC.Dia_Vencimento AS Data_Vencimento, --dbo.Fu_Caracteres_Iniciais('00', CONVERT(VARCHAR(10), FPC.Dia_Vencimento, 103)) FPC.Dia_Vencimento AS Data_Vencimento,      
  
            FPC.Lote_Cancelamento AS Data_Cancelamento, -- CONVERT(VARCHAR(10), FPC.Lote_Cancelamento, 103) AS Data_Cancelamento,     
  
            2 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN EXISTS(SELECT *  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            CASE WHEN ISNULL((SELECT Id_Fatura  
            FROM Fatura  
            WHERE Nr_Fatura = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) = 0 THEN 'FALSE' ELSE 'TRUE' END AS Visible_LINK  
  
        FROM Fatura_Plano_Conta FPC  
        WHERE   FPC.Id_Conglomerado = @pId_Conglomerado  
  
    UNION  
        -----fatura recepcionada e nao cadastrada no plano de conta     
        SELECT F.Nr_Fatura,  
  
            CASE WHEN EXISTS(SELECT *  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            NULL,  
  
            NULL,  
  
            NULL,  
  
            'Grid_Cancel.png',  
  
            NULL AS Data_Vencimento,  
  
            NULL AS Data_Cancelamento,  
  
            1 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN EXISTS(SELECT *  
            FROM LOTE  
      WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            'FALSE' AS Visible_LINK  
  
        FROM Fatura F  
        WHERE F.Id_Fatura_Parametro IN ( SELECT Id_Fatura_Parametro  
            FROM Rl_Ra_Fatura_Parametro_Conglomerado  
            WHERE   Id_Conglomerado = @pId_Conglomerado)  
            AND NOT EXISTS(SELECT *  
            FROM Fatura_Plano_Conta  
            WHERE Nr_Plano_Conta = F.Nr_Fatura)  
            AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote  
    UNION  
        -----fatura carregada e nao cadastrada no plano de mconta     
        SELECT L.DC_Nr_Nota_Fiscal,  
  
            CASE WHEN EXISTS(SELECT *  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            NULL,  
  
            NULL,  
  
            NULL,  
  
            'Grid_Cancel.png',  
  
            NULL AS Data_Vencimento,  
  
            NULL AS Data_Cancelamento,  
  
            1 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN EXISTS(SELECT *  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = @pId_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            'FALSE' AS Visible_LINK  
  
        FROM Lote L  
        WHERE L.Id_Ativo IN (SELECT Id_Ativo  
            FROM Ativo  
            WHERE Id_Conglomerado = @pId_Conglomerado)  
            AND NOT EXISTS(SELECT *  
            FROM Fatura_Plano_Conta  
            WHERE Nr_Plano_Conta = L.DC_Nr_Nota_Fiscal)  
            AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote  
        GROUP BY L.DC_Nr_Nota_Fiscal, L.Dt_Liberacao_Lote  
    ORDER BY Fatura  
END 


-----consulta plano de conta   - Download todas faturas               
IF @pPAKAGE = 'sp_Confere_Plano_Conta_V4_DwZip'                  
BEGIN  
    ---plano de conta                  
        SELECT  
		     (SELECT TOP 1 Id_Arquivo_PDF AS Dw  
							FROM Arquivo_PDF  
							WHERE (Tabela_Registro LIKE 'Fatura')  
								AND (Id_Registro_Tabela IN (SELECT Id_Fatura  
								FROM Fatura  
								WHERE Nr_Fatura LIKE FPC.Nr_Plano_Conta
								AND (SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)))) AS ArquivoDw,
            (SELECT Nm_Conglomerado  
            FROM Conglomerado  
            WHERE Id_Conglomerado = FPC.Id_Conglomerado) AS Operadora,  
  
            FPC.Nr_Plano_Conta AS Fatura,  
  
            CASE WHEN EXISTS(SELECT Id_Lote  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta AND (Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = FPC.Id_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)) THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            ISNULL(CAST((SELECT Vr_Fatura  
            FROM Fatura  
            WHERE Nr_Fatura = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Fatura,  
  
            ISNULL((SELECT Id_Fatura  
            FROM Fatura  
            WHERE Nr_Fatura = FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Id_Fatura,  
  
  
            ISNULL(CAST((SELECT SUM(Valor_Rateado)  
            FROM Rateio_Item  
            WHERE Id_Rateio IN( SELECT RF.Id_Rateio  
            FROM Fatura FA INNER JOIN Rl_Rateio_Fatura RF ON (RF.Id_Fatura = FA.Id_Fatura)  
            WHERE FA.Nr_Fatura =  FPC.Nr_Plano_Conta  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)) AS DECIMAL(10,2)), 0) AS Total_Rateado,  
  
            'Grid_Check.png' AS Cadastrada,  
  
            FPC.Dia_Vencimento AS Data_Vencimento,  
  
            CONVERT(VARCHAR(10), FPC.Lote_Cancelamento, 103) AS Data_Cancelamento,  
  
            2 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN EXISTS(SELECT Id_Lote  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = FPC.Nr_Plano_Conta AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado = FPC.Id_Conglomerado)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            CASE WHEN ISNULL((SELECT Id_Fatura  
            FROM Fatura  
            WHERE (Nr_Fatura = FPC.Nr_Plano_Conta)  
                AND (SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)), 0) = 0 THEN 'FALSE' ELSE 'TRUE' END AS Visible_LINK,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE (Tabela_Registro LIKE 'Fatura')  
                AND (Id_Registro_Tabela IN (SELECT Id_Fatura  
                FROM Fatura  
                WHERE Nr_Fatura LIKE FPC.Nr_Plano_Conta
                AND (SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)))), 'RED') AS Download_Fatura,  
  
            ISNULL((SELECT TOP 1
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw
            FROM Arquivo_PDF
            WHERE (Tabela_Registro LIKE 'Boleto')
                AND (Id_Registro_Tabela IN (SELECT Id_Fatura
                FROM Fatura
                WHERE Nr_Fatura LIKE FPC.Nr_Plano_Conta
                AND (SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote)))), 'RED') AS Download_Boleto, 
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE (Tabela_Registro LIKE 'Nota_Fiscal_Fatura')  
                AND (Id_Registro_Tabela IN (SELECT Id_Nota_Fiscal  
                FROM Nota_Fiscal_Fatura  
                WHERE Id_Fatura IN (SELECT Id_Fatura  
                FROM Fatura  
                WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)
                AND (SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote))))), 'RED') AS Download_NF,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE (Tabela_Registro LIKE 'Rateio') AND (Id_Registro_Tabela IN (SELECT Id_Rateio  
                FROM Rateio  
                WHERE Nm_Rateio LIKE CONCAT('%',FPC.Nr_Plano_Conta,'%')))), 'RED') AS Download_Rateio,  
  
            (SELECT CONVERT(VARCHAR, Dt_Emissao, 103)  
            FROM Fatura  
            WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)  
                AND (Dt_Lote LIKE @pDt_Lote)) AS Dt_Emissao,  
  
            (SELECT CONVERT(VARCHAR, Dt_Vencimento, 103)  
            FROM Fatura  
            WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)  
                AND (Dt_Lote LIKE @pDt_Lote)) AS Dt_Vencimento,  
  
            (SELECT Nota_Fiscal  
            FROM Fatura  
            WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)  
                AND (Dt_Lote LIKE @pDt_Lote)) AS NF,  
  
            (SELECT Pedido  
            FROM Fatura  
            WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)  
                AND (Dt_Lote LIKE @pDt_Lote)) AS Pedido,  
  
            (SELECT Req  
            FROM Fatura  
            WHERE (Nr_Fatura LIKE FPC.Nr_Plano_Conta)  
                AND (Dt_Lote LIKE @pDt_Lote)) AS Req  
  
        FROM Fatura_Plano_Conta FPC  
        WHERE FPC.Id_Conglomerado IN (SELECT Id_Conglomerado  
        FROM Ativo  
        WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo IN (SELECT Id_Ativo_Tipo  
            FROM Ativo_Tipo  
            WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo_Grupo = @pId_Ativo_Tipo_Grupo))  
  
    UNION  
        -----fatura recepcionada e nao cadastrada no plano de conta                  
        SELECT  
		 (SELECT TOP 1 Id_Arquivo_PDF AS Dw  
				 FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Fatura' 
                AND Id_Registro_Tabela = F.Id_Fatura) AS ArquivoDw,
            (SELECT Nm_Conglomerado  
            FROM Conglomerado  
            WHERE Id_Conglomerado = (SELECT Id_Conglomerado  
            FROM Fatura_Plano_Conta  
            WHERE Nr_Plano_Conta LIKE F.Nr_Fatura)) AS Operadora,  
  
            F.Nr_Fatura AS Fatura,  
  
            CASE WHEN EXISTS(SELECT Id_Lote  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado IN (SELECT Id_Conglomerado  
                FROM Fatura_Plano_Conta  
                WHERE Nr_Plano_Conta LIKE F.Nr_Fatura))  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            NULL AS Total_Fatura,  
  
            NULL AS Id_Fatura,  
  
            NULL AS Total_Rateado,  
  
            'Grid_Cancel.png' AS Cadastrada,  
  
            NULL AS Data_Vencimento,  
  
            NULL AS Data_Cancelamento,  
  
            1 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN EXISTS(SELECT Id_Lote  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = F.Nr_Fatura AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo  
                WHERE Id_Conglomerado  IN (SELECT Id_Conglomerado  
                FROM Fatura_Plano_Conta  
                WHERE Nr_Plano_Conta LIKE F.Nr_Fatura))  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            'FALSE' AS Visible_LINK,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Fatura' 
                AND Id_Registro_Tabela = F.Id_Fatura), 'RED') AS Download_Fatura,  
  
            ISNULL((SELECT TOP 1
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw
            FROM Arquivo_PDF
            WHERE (Tabela_Registro LIKE 'Boleto') 
                AND (Id_Registro_Tabela = F.Id_Fatura)), 'RED') AS Download_Boleto,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Nota_Fiscal_Fatura'  
                AND Id_Registro_Tabela IN (SELECT Id_Nota_Fiscal  
                FROM Nota_Fiscal_Fatura  
                WHERE Id_Fatura = F.Id_Fatura)), 'RED') AS Download_NF,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Rateio' AND Id_Registro_Tabela IN (SELECT  
                    Id_Rateio  
                FROM Rateio  
                WHERE Nm_Rateio LIKE CONCAT('%',F.Nr_Fatura,'%'))), 'RED') AS Download_Rateio,  
  
            CONVERT(VARCHAR, F.Dt_Emissao, 103) AS Dt_Emissao,  
  
            CONVERT(VARCHAR, F.Dt_Vencimento, 103) AS Dt_Vencimento,  
  
            F.Nota_Fiscal AS NF,  
  
            F.Pedido AS Pedido,  
  
            F.Req AS Req  
  
        FROM Fatura F  
        WHERE F.Id_Fatura_Parametro IN (SELECT Id_Fatura_Parametro  
            FROM Fatura_Parametro  
            WHERE Id_Ativo IN ((SELECT Id_Ativo  
            FROM Ativo  
            WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo IN (SELECT Id_Ativo_Tipo  
                FROM Ativo_Tipo  
                WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo_Grupo = @pId_Ativo_Tipo_Grupo))))  
            AND NOT EXISTS(SELECT Id_Fatura_Plano_Conta  
            FROM Fatura_Plano_Conta  
            WHERE Nr_Plano_Conta = F.Nr_Fatura)  
            AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote  
  
    UNION  
        ---fatura carregada e nao cadastrada no plano de mconta                  
        SELECT  
		     (SELECT TOP 1 Id_Arquivo_PDF AS Dw  
				 FROM Arquivo_PDF  
                WHERE Tabela_Registro LIKE 'Fatura' AND Id_Registro_Tabela IN (SELECT Id_Fatura  
                FROM Fatura  
                WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal)) AS ArquivoDw,
            (SELECT Nm_Conglomerado  
            FROM Conglomerado  
            WHERE Id_Conglomerado IN (SELECT Id_Conglomerado  
            FROM Ativo  
            WHERE Id_Ativo = L.Id_Ativo AND Fl_Desativado = 0) ) AS Operadora,  
  
            L.DC_Nr_Nota_Fiscal AS Fatura,  
  
            CASE WHEN L.DC_Nr_Nota_Fiscal IS NOT NULL THEN 'Grid_Check.png' ELSE 'Grid_Cancel.png' END AS Conta_Carregada,  
  
            ISNULL((SELECT COUNT(Id_Ativo)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 0) AS Qtd_Linha,  
  
            ISNULL(CAST((SELECT SUM(Total)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total,  
  
            ISNULL(CAST((SELECT SUM(Total_Cobrado)  
            FROM LOTE  
            WHERE DC_Nr_Nota_Fiscal = L.DC_Nr_Nota_Fiscal AND Id_Ativo IN (SELECT Id_Ativo  
                FROM Ativo)  
                AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS DECIMAL(10,2)), 0) AS Total_Carregado,  
  
            NULL AS Total_Fatura,  
  
            NULL AS Id_Fatura,  
  
            NULL AS Total_Rateado,  
  
            'Grid_Cancel.png' AS Cadastrada,  
  
            NULL AS Data_Vencimento,  
  
            NULL AS Data_Cancelamento,  
  
            1 AS Flag_Conta_Cadastrada,  
  
            CASE WHEN L.DC_Nr_Nota_Fiscal IS NOT NULL THEN 2 ELSE 1 END AS Flag_Conta_Carregada,  
  
            'FALSE' AS Visible_LINK,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Fatura' AND Id_Registro_Tabela IN (SELECT Id_Fatura  
                FROM Fatura  
                WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal)), 'RED') AS Download_Fatura,  
  
            ISNULL((SELECT TOP 1
                (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw
            FROM Arquivo_PDF
            WHERE Tabela_Registro LIKE 'Boleto' 
                AND Id_Registro_Tabela IN (SELECT Id_Fatura
                FROM Fatura
                WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal)), 'RED') AS Download_Boleto,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Nota_Fiscal_Fatura'  
                AND Id_Registro_Tabela IN (SELECT  
                    Id_Nota_Fiscal  
                FROM Nota_Fiscal_Fatura  
                WHERE Id_Fatura IN (SELECT Id_Fatura  
                FROM Fatura  
                WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal))), 'RED') AS Download_NF,  
  
            ISNULL((SELECT TOP 1 (CASE WHEN Download = 1 THEN 'GREEN' ELSE 'YELLOW' END) AS Dw  
            FROM Arquivo_PDF  
            WHERE Tabela_Registro LIKE 'Rateio'  
                AND Id_Registro_Tabela IN (SELECT  
                    Id_Rateio  
                FROM Rateio  
                WHERE Nm_Rateio LIKE CONCAT('%',L.DC_Nr_Nota_Fiscal,'%'))), 'RED') AS Download_Rateio,  
  
            CONVERT(VARCHAR, (SELECT Dt_Emissao  
            FROM Fatura  
            WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 103) AS Dt_Emissao,  
  
            CONVERT(VARCHAR, (SELECT Dt_Vencimento  
            FROM Fatura  
            WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote), 103) AS Dt_Vencimento,  
  
            (SELECT Nota_Fiscal  
            FROM Fatura  
            WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS NF,  
  
            (SELECT Pedido  
            FROM Fatura  
            WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS Pedido,  
  
            (SELECT Req  
            FROM Fatura  
            WHERE Nr_Fatura LIKE L.DC_Nr_Nota_Fiscal AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote) AS Req  
  
        FROM Lote L  
        WHERE L.Id_Ativo IN ((SELECT Id_Ativo  
            FROM Ativo  
            WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo IN (SELECT Id_Ativo_Tipo  
                FROM Ativo_Tipo  
                WHERE Fl_Desativado = 0 AND Id_Ativo_Tipo_Grupo = @pId_Ativo_Tipo_Grupo)))  
            AND NOT EXISTS(SELECT Id_Ativo  
            FROM Fatura_Plano_Conta  
            WHERE Nr_Plano_Conta = L.DC_Nr_Nota_Fiscal)  
            AND SUBSTRING(CONVERT(VARCHAR(6),Dt_Lote,112),1,6) = @pDt_Lote  
  
    ORDER BY Fatura  
END  
  