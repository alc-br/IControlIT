-- =============================================
-- Author     :	JOAO CARLOS
-- Create date: 12/01/2024
-- Description:	CRIAÇÃO DE NOVOS CAMPOS NA TABELA estoque_aparelho, 
--   referente a demanda https://bitgrow.atlassian.net/browse/K2AICONTROLIT-59
--   Inclução de checkbox para informar acessórios no cadastro do aparelho
--   O campo deve ser no formato checkbox com as seguintes opções:  
--   Carregador, Cabo USB, Fone, Película e Capa de proteção do aparelho.
--   A label do campo deve ser: Acessórios
--   O formulário deve realizar a validação para que seja selecionado no mínimo 1 acessório.
--   Pode ser selecionado vários acessórios.
-- =============================================
-- alteração da pa_Estoque_Aparelho , para cuidar dos processos de CRUD
-- =============================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pa_Estoque_Aparelho]
    (@pPakage                      VARCHAR(200),
    @pId_Aparelho                  INT    OUT,
    @pNr_Aparelho                  VARCHAR(50)  = NULL,
    @pNr_Linha_Solicitacao         VARCHAR(50)  = NULL,
    @pNr_Chamado                   VARCHAR(50)  = NULL,
    @pDt_Chamado                   DATETIME  = NULL,
    @pNr_Pedido                    VARCHAR(50)  = NULL,
    @pDt_Pedido                    DATETIME  = NULL,
    @pId_Estoque_Nota_Fiscal       INT    = NULL,
    @pId_Conglomerado              INT    = NULL,
    @pId_Aparelho_Tipo             INT    = NULL,
    @pId_Ativo_Tipo                INT    = NULL,
    @pId_Ativo_Modelo              INT    = NULL,
    @pId_Estoque_Aparelho_Status   INT    = NULL,
    @pObservacao                   VARCHAR(300) = NULL,
    @pJustificativa_Desativacao    VARCHAR(300) = NULL,
    @pId_Estoque_Endereco_Entrega  INT    = NULL,
    @pId_Consumidor                INT    = NULL,
    @pFl_Desativado                INT    = NULL,
    @pId_Usuario_Permissao         INT    = NUll,
    @pNr_Aparelho_2                VARCHAR(50)  = NULL,
	@pCk_Carregador                INT  = NULL,
	@pCk_Cabousb                   INT  = NULL,
	@pCk_Fone                      INT  = NULL,
	@pCk_Pelicula                  INT  = NULL,
	@pCk_Capaprotecao              INT  = NULL)
AS

print @pPAKAGE
-----insere ou atualiza registro      
IF @pPAKAGE = 'sp_SM'      
BEGIN  
    IF EXISTS (SELECT 1
    FROM Estoque_Aparelho
    WHERE Id_Aparelho = @pId_Aparelho)      
    BEGIN
        IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Alterar') = 2 AND
            NOT EXISTS (SELECT Id_Aparelho
            FROM Estoque_Aparelho
            WHERE Nr_Aparelho = @pNr_Aparelho_2 OR
                (Nr_Aparelho_2 = @pNr_Aparelho_2 AND Nr_Aparelho_2 IS NOT NULL))    
        BEGIN
            DECLARE @variavelConsumidor VARCHAR(100)

            SELECT @variavelConsumidor=c.Nm_Consumidor
            FROM Usuario AS U
                INNER JOIN Consumidor AS C ON U.Id_Consumidor = C.Id_Consumidor
            WHERE U.Id_Usuario = @pId_Usuario_Permissao
            UPDATE Estoque_Aparelho     

            SET Nr_Linha_Solicitacao        = @pNr_Linha_Solicitacao,      
                Nr_Chamado                  = @pNr_Chamado,    
                Nr_Aparelho_2               = @pNr_Aparelho_2, 
                Nr_Pedido                   = @pNr_Pedido,      
                Id_Estoque_Nota_Fiscal      = @pId_Estoque_Nota_Fiscal,       
                Id_Conglomerado             = @pId_Conglomerado,      
                Id_Aparelho_Tipo            = @pId_Aparelho_Tipo,       
                Id_Ativo_Tipo               = @pId_Ativo_Tipo,       
                Id_Ativo_Modelo             = @pId_Ativo_Modelo,       
                Id_Estoque_Aparelho_Status  = @pId_Estoque_Aparelho_Status,       
                Observacao                  = '(Data Alteração - ' + FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm') + ' - ' + @pObservacao + ' | ' +'Consmidor - ' + @variavelConsumidor +  ')  ' + ISNULL(Observacao,''),       
                Justificativa_Desativacao   = @pJustificativa_Desativacao,       
                Id_Estoque_Endereco_Entrega = @pId_Estoque_Endereco_Entrega,      
                Id_Consumidor               = @pId_Consumidor,       
                Fl_Desativado               = 0,   
				Ck_Carregador               = @pCk_Carregador,
				Ck_Cabousb                  = @pCk_Cabousb,
				Ck_Fone                     = @pCk_Fone,
				Ck_Pelicula                 = @pCk_Pelicula,
				Ck_Capaprotecao             = @pCk_Capaprotecao
            WHERE Id_Aparelho = @pId_Aparelho
        END
        ELSE      
        BEGIN
            SELECT @pId_Aparelho = 0
        END
    END      
    ELSE      
    BEGIN
        IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Incluir') = 2 AND
            NOT EXISTS (SELECT Id_Aparelho
            FROM Estoque_Aparelho
            WHERE Nr_Aparelho = @pNr_Aparelho OR
                Nr_Aparelho = @pNr_Aparelho_2 OR
                Nr_Aparelho_2 = @pNr_Aparelho OR
                (Nr_Aparelho_2 = @pNr_Aparelho_2 AND Nr_Aparelho_2 IS NOT NULL))   
        BEGIN
            INSERT INTO Estoque_Aparelho(
                Nr_Aparelho,
                Nr_Linha_Solicitacao,
                Nr_Chamado,
                Dt_Chamado,
                Nr_Pedido,
                Dt_Pedido,
                Id_Estoque_Nota_Fiscal,
                Id_Conglomerado,
                Id_Aparelho_Tipo,
                Id_Ativo_Tipo,
                Id_Ativo_Modelo,
                Id_Estoque_Aparelho_Status,
                Observacao,
                Justificativa_Desativacao,
                Id_Estoque_Endereco_Entrega,
                Id_Consumidor,
                Fl_Desativado,
                Nr_Aparelho_2,
				Ck_Carregador,
				Ck_Cabousb,
				Ck_Fone,
				Ck_Pelicula,
				Ck_Capaprotecao)
            VALUES(
                @pNr_Aparelho,
                @pNr_Linha_Solicitacao,
                @pNr_Chamado,
                GETDATE(),
                @pNr_Pedido,
                @pDt_Pedido,
                @pId_Estoque_Nota_Fiscal,
                @pId_Conglomerado,
                @pId_Aparelho_Tipo,
                @pId_Ativo_Tipo,
                @pId_Ativo_Modelo,
                @pId_Estoque_Aparelho_Status,
                @pObservacao,
                @pJustificativa_Desativacao,
                @pId_Estoque_Endereco_Entrega,
                @pId_Consumidor,
                0,
                @pNr_Aparelho_2,
				@pCk_Carregador,
				@pCk_Cabousb,
				@pCk_Fone,
				@pCk_Pelicula,
				@pCk_Capaprotecao)
            -----retorna id       
            SELECT @pId_Aparelho = MAX(Id_Aparelho)
            FROM Estoque_Aparelho
        END      
        ELSE      
        BEGIN
            SELECT @pId_Aparelho = 0
        END
    END
END

-----ativa / desativa resgistro      
IF @pPAKAGE = 'sp_SE'      
BEGIN
    IF dbo.Fu_Permissao(@pId_Usuario_Permissao,'Excluir') = 2      
    BEGIN
        UPDATE Estoque_Aparelho       
        SET Id_Aparelho_Tipo = 5,      
            Fl_Desativado = 1,      
            Observacao = @pObservacao      
        WHERE Id_Aparelho = @pId_Aparelho
    END
END

-----consulta pelo ID do registro      
IF @pPAKAGE = 'sp_SL_ID'      
BEGIN
    SELECT ISNULL(Id_Aparelho,0)     AS Id_Aparelho,
        ISNULL(Nr_Aparelho,0)     AS Nr_Aparelho,
        ISNULL(Nr_Aparelho_2,'')     AS Nr_Aparelho_2,
        ISNULL(Nr_Linha_Solicitacao,0)   AS Nr_Linha_Solicitacao,
        ISNULL(Nr_Chamado,0)     AS Nr_Chamado,
        ISNULL(Dt_Chamado,'01/01/1900')   AS Dt_Chamado,
        ISNULL(Nr_Pedido,0)      AS Nr_Pedido,
        ISNULL(Dt_Pedido,'01/01/1900')   AS Dt_Pedido,
        ISNULL(Id_Estoque_Nota_Fiscal,0)  AS Id_Estoque_Nota_Fiscal,
        ISNULL(Id_Conglomerado,0)    AS Id_Conglomerado,
        ISNULL(CASE WHEN Fl_Desativado = 1 THEN 5 ELSE Id_Aparelho_Tipo END ,0) AS Id_Aparelho_Tipo,
        ISNULL(Id_Ativo_Tipo,0)     AS Id_Ativo_Tipo,
        ISNULL(Id_Ativo_Modelo,0)    AS Id_Ativo_Modelo,
        ISNULL(Id_Estoque_Aparelho_Status,0) AS Id_Estoque_Aparelho_Status,
        ISNULL(Observacao,0)     AS Observacao,
        ISNULL(Justificativa_Desativacao,0)  AS Justificativa_Desativacao,
        ISNULL(Id_Estoque_Endereco_Entrega,0) AS Id_Estoque_Endereco_Entrega,
        ISNULL(Id_Consumidor,0)     AS Id_Consumidor,
        Fl_Desativado,
		ISNULL(Ck_Carregador, 0) AS Ck_Carregador,  
		ISNULL(Ck_Cabousb, 0) AS Ck_Cabousb,     
		ISNULL(Ck_Fone, 0) AS Ck_Fone,        
		ISNULL(Ck_Pelicula, 0) AS Ck_Pelicula,    
		ISNULL(Ck_Capaprotecao, 0) AS Ck_Capaprotecao,
        ISNULL((SELECT ISNULL(SAT.Nr_Ativo,'')
        FROM Rl_Estoque_Aparelho_Ativo SEA INNER JOIN Ativo SAT ON SEA.Id_Ativo = SAT.Id_Ativo
        WHERE Id_Aparelho = EA.Id_Aparelho),'') AS Nr_Ativo,
        ISNULL((SELECT ISNULL('../Cadastro/Ativo.aspx?ID=' + CONVERT(VARCHAR(50),Id_Ativo),'')
        FROM Rl_Estoque_Aparelho_Ativo
        WHERE Id_Aparelho = EA.Id_Aparelho),'') AS Link_Ativo
    FROM Estoque_Aparelho EA
    WHERE EA.Id_Aparelho = @pId_Aparelho
END