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
-- alteração da TRIGGER para realizar o processo de log
-- =============================================
ALTER    TRIGGER [dbo].[tg_Estoque_Aparelho_LOG] on  [dbo].[Estoque_Aparelho] FOR INSERT, DELETE, UPDATE AS
begin 

insert into [Estoque_Aparelho_LOG]

SELECT  [Id_Aparelho]
      ,[Nr_Aparelho]
      ,[Nr_Linha_Solicitacao]
      ,[Nr_Chamado]
      ,[Dt_Chamado]
      ,[Nr_Pedido]
      ,[Dt_Pedido]
      ,[Id_Estoque_Nota_Fiscal]
      ,[Id_Conglomerado]
      ,[Id_Aparelho_Tipo]
      ,[Id_Ativo_Tipo]
      ,[Id_Ativo_Modelo]
      ,[Id_Estoque_Aparelho_Status]
      ,[Observacao]
      ,[Justificativa_Desativacao]
      ,[Id_Estoque_Endereco_Entrega]
      ,[Id_Consumidor]
      ,[Fl_Desativado]
      ,'Atualizado de'
	  , getdate()
	  ,[Ck_Carregador]
	  ,[Ck_Cabousb]
	  ,[Ck_Fone]
	  ,[Ck_Pelicula]
	  ,[Ck_Capaprotecao]
from  deleted


insert into [Estoque_Aparelho_LOG]

SELECT  [Id_Aparelho]
      ,[Nr_Aparelho]
      ,[Nr_Linha_Solicitacao]
      ,[Nr_Chamado]
      ,[Dt_Chamado]
      ,[Nr_Pedido]
      ,[Dt_Pedido]
      ,[Id_Estoque_Nota_Fiscal]
      ,[Id_Conglomerado]
      ,[Id_Aparelho_Tipo]
      ,[Id_Ativo_Tipo]
      ,[Id_Ativo_Modelo]
      ,[Id_Estoque_Aparelho_Status]
      ,[Observacao]
      ,[Justificativa_Desativacao]
      ,[Id_Estoque_Endereco_Entrega]
      ,[Id_Consumidor]
      ,[Fl_Desativado]
      ,'Atualizado para'
	  , getdate()
	  ,[Ck_Carregador]
	  ,[Ck_Cabousb]
	  ,[Ck_Fone]
	  ,[Ck_Pelicula]
	  ,[Ck_Capaprotecao]
from  inserted

end