USE [Ativvus_Login]
GO

/****** 
-- =============================================
-- Author:		João Carlos
-- Create date: 21/03/2024 20:03:44 
-- Description:	K2AICONTROLIT-24 - Envio do link para Termo de Responsabilidade por Ativo
--              Este script deve ser executado apenas na base de autenticação [Ativvus_Login]
--              será usado para controle de exibição do termo sem autenticar no sistema 
-- =============================================
******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Usuario_Envio_TermoResponsabilidade](
	[Id_Usuario] [int] NOT NULL,
	[Id_Ativo] [int] NOT NULL,
	[Empresa] [varchar](50) NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
	[QtdEnvios] [int] NULL,
	[DataUltEnvio] [datetime] NULL,
	[Url_Termo] [varchar](200) NULL,
	[Token] [varchar](250) NULL,
	[Token_Validade] [datetime] NULL,
	[Token_Confirmacao] [datetime] NULL,
	[Termo_Ativacao] [varchar](150) NULL,
 CONSTRAINT [PK_Usuario_EnvioTermoResponsabilidade] PRIMARY KEY CLUSTERED 
(
	[Id_Usuario] ASC,
	[Id_Ativo] ASC,
	[Empresa] ASC,
	[DataCadastro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Usuario_Envio_TermoResponsabilidade] ON [dbo].[Usuario_Envio_TermoResponsabilidade]
(
	[Token] ASC,
	[Token_Validade] ASC,
	[Token_Confirmacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Nm_Ativo_Tipo_Grupo VARCHAR(150) NULL

GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Id_Consumidor INT NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Nm_Consumidor VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Nm_Usuario VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Matricula VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Endereco VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Nm_Filial VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Centro_Custo VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Departamento VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Linha VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Tipo_Ativo VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Nm_Conglomerado VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Nm_Modelo_Fabricante VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD IMEI VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD ACESSORIO VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Carregador INT NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Cabo_USB INT NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Fone INT NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Pelicula INT NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Capa_Protecao INT NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Nr_Aparelho VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD NF VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD Logo VARCHAR(100) NULL
GO
ALTER TABLE  [dbo].[Usuario_Envio_TermoResponsabilidade] ADD CHIP VARCHAR(100) NULL
GO