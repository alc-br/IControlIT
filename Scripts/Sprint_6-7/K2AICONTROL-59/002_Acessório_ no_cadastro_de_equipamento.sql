-- =============================================
-- Author     :	JOAO CARLOS
-- Create date: 12/01/2024
-- Description:	CRIA��O DE NOVOS CAMPOS NA TABELA estoque_aparelho, 
--   referente a demanda https://bitgrow.atlassian.net/browse/K2AICONTROLIT-59
--   Inclu��o de checkbox para informar acess�rios no cadastro do aparelho
--   O campo deve ser no formato checkbox com as seguintes op��es:  
--   Carregador, Cabo USB, Fone, Pel�cula e Capa de prote��o do aparelho.
--   A label do campo deve ser: Acess�rios
--   O formul�rio deve realizar a valida��o para que seja selecionado no m�nimo 1 acess�rio.
--   Pode ser selecionado v�rios acess�rios.
-- =============================================
-- altera��o da TRIGGER para realizar o processo de log
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