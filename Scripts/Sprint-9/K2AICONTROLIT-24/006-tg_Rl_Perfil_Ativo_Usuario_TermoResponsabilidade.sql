USE [Fortlev]
GO
/****** Object:  Trigger [dbo].[tg_Rl_Perfil_Ativo_Usuario_log]    Script Date: 29/03/2024 16:40:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER          TRIGGER [dbo].[tg_Rl_Perfil_Ativo_Usuario_log] on  [dbo].[Rl_Perfil_Ativo_Usuario] FOR DELETE, UPDATE , insert AS
begin

IF EXISTS(SELECT * FROM deleted)
begin

select  
Id_Ativo,
Dt_Hr_Ativacao,
Dt_Hr_Desativacao,
Id_Usuario,
'Atualizado De',
substring (convert(varchar,(Dt_Hr_Ativacao),102),1,4) + '-' + substring (convert(varchar,(Dt_Hr_Ativacao),102),6,2) + '-' + substring (convert(varchar,(Dt_Hr_Ativacao),102),9,2),

getdate()

from deleted

end

IF EXISTS(SELECT * FROM inserted)
begin




		--insert  into Rl_Perfil_Ativo_Usuario_log
		insert into Rl_Perfil_Ativo_Usuario_log

		select  
		Id_Ativo,
		Dt_Hr_Ativacao,
		Dt_Hr_Desativacao,
		Id_Usuario,
		'Atualizado para',
		substring (convert(varchar,(Dt_Hr_Ativacao),102),1,4) + '-' + substring (convert(varchar,(Dt_Hr_Ativacao),102),6,2) + '-' + substring (convert(varchar,(Dt_Hr_Ativacao),102),9,2),

		getdate()



		--convert(varchar, Dt_Hr_Ativacao,   102) ' 1: mm/dd/aa'
		--SUBSTRING([Dt_Hr_Ativacao], 1, 4)
		--[Dt_Hr_Ativacao]
		from inserted

		--update ativo set Dt_Ativacao = (replace(convert(varchar, Dt_Hr_Ativacao,   102),',','.')  where Id_Ativo = (select top (1) Id_Ativo from inserted )
		 --update ativo set Dt_Ativacao = '2023-02-01' where Id_Ativo = 1477

    --k2aIcontrolIt-24

	declare @vIdAtivo int, @vIdUsuario int
	select  
		@vIdAtivo =Id_Ativo,		
		@vIdUsuario = Id_Usuario
		from inserted

	exec pa_usuario_perfil_insert_termo_responsabilidade @vIdUsuario, @vIdAtivo
	

End




End
