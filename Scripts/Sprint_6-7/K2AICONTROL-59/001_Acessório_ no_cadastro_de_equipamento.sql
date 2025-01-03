-- =============================================
-- Author     :	JOAO CARLOS
-- Create date: 12/01/2024
-- Description:	CRIA��O DE NOVOS CAMPOS NA TABELA estoque_aparelho, 
--   referente a demanda https://INTgrow.atlassian.net/browse/K2AICONTROLIT-59
--   Inclu��o de checkbox para informar acess�rios no cadastro do aparelho
--   O campo deve ser no formato checkbox com as seguintes op��es:  
--   Carregador, Cabo USB, Fone, Pel�cula e Capa de prote��o do aparelho.
--   A label do campo deve ser: Acess�rios
--   O formul�rio deve realizar a valida��o para que seja selecionado no m�nimo 1 acess�rio.
--   Pode ser selecionado v�rios acess�rios.
-- =============================================
-- altera��o da tabela , adicionando novos campos do tipo INT 
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