  Use [Ativvus_Login]
  GO
  ALTER TABLE [Ativvus_Login].[dbo].[Usuario_Global] ADD Token varchar(250) NULL;
  GO
  ALTER TABLE [Ativvus_Login].[dbo].[Usuario_Global] ADD Token_Validade DateTime NULL;
  GO  
	CREATE NONCLUSTERED INDEX [idx_Usuario_Global_Token] ON [Ativvus_Login].[dbo].[Usuario_Global]
	(
		[Token] ASC,
		[Token_Validade] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)

	GO




