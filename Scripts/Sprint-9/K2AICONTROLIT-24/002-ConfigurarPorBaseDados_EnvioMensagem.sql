/*
* este script deve ser executado para cada base de dados, e a descrição de seu texto ajustado
* de acordo com as especificações do cliente.
*/
USE [Fortlev]
GO

INSERT INTO [dbo].[Mail_Sender]
           ([Id_Mail_Sender]
		   ,[Profile_Name]
           ,[Assunto]
           ,[Texto_1]
           ,[Texto_2]
           ,[Texto_3]
           ,[Texto_4]
           ,[Texto_5]
           ,[Status_Mail])
     VALUES
           (15
		   ,'email_K2A'  --<Profile_Name, varchar(50),>
           ,'[Astromaritima]-Termo Responsabilidade(iControlIT)' --<Assunto, varchar(50),>
           ,'Atenção Foi realizada a inclusão de um Ativo para a sua utilização. Favor Validar seu termo de Responsabilidade.' --<Texto_1, varchar(8000),>
           ,NULL --<Texto_2, varchar(8000),>
           ,NULL --<Texto_3, varchar(8000),>
           ,NULL --<Texto_4, varchar(8000),>
           ,NULL --<Texto_5, varchar(8000),>
           , 2 --<Status_Mail, int,>
		   )
GO

