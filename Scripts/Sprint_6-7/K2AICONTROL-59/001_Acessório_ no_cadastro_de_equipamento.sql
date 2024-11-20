-- =============================================
-- Author     :	JOAO CARLOS
-- Create date: 12/01/2024
-- Description:	CRIAÇÃO DE NOVOS CAMPOS NA TABELA estoque_aparelho, 
--   referente a demanda https://INTgrow.atlassian.net/browse/K2AICONTROLIT-59
--   Inclução de checkbox para informar acessórios no cadastro do aparelho
--   O campo deve ser no formato checkbox com as seguintes opções:  
--   Carregador, Cabo USB, Fone, Película e Capa de proteção do aparelho.
--   A label do campo deve ser: Acessórios
--   O formulário deve realizar a validação para que seja selecionado no mínimo 1 acessório.
--   Pode ser selecionado vários acessórios.
-- =============================================
-- alteração da tabela , adicionando novos campos do tipo INT 
-- =============================================
ALTER TABLE [dbo].[Estoque_Aparelho] ADD Ck_Carregador INT NULL ;
GO
ALTER TABLE [dbo].[Estoque_Aparelho] ADD Ck_Cabousb INT NULL ;
GO
ALTER TABLE [dbo].[Estoque_Aparelho] ADD Ck_Fone INT NULL ;
GO
ALTER TABLE [dbo].[Estoque_Aparelho] ADD Ck_Pelicula INT NULL ;
GO
ALTER TABLE [dbo].[Estoque_Aparelho] ADD Ck_Capaprotecao INT NULL ;
GO
ALTER TABLE [dbo].[Estoque_Aparelho_LOG] ADD Ck_Carregador INT NULL ;
GO
ALTER TABLE [dbo].[Estoque_Aparelho_LOG] ADD Ck_Cabousb INT NULL ;
GO
ALTER TABLE [dbo].[Estoque_Aparelho_LOG] ADD Ck_Fone INT NULL ;
GO
ALTER TABLE [dbo].[Estoque_Aparelho_LOG] ADD Ck_Pelicula INT NULL ;
GO
ALTER TABLE [dbo].[Estoque_Aparelho_LOG] ADD Ck_Capaprotecao INT NULL ;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO