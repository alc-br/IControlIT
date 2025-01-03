USE [Amil]
GO
/****** Object:  Trigger [dbo].[tg_Usuario_Global]    Script Date: 27/02/2024 18:52:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER trigger [dbo].[tg_Usuario_Global] On [dbo].[Usuario] FOR INSERT, DELETE, UPDATE AS
BEGIN
	DECLARE @vNm_Empresa VARCHAR(50)

	DECLARE @v_Tp_Id_Usuario INT
	DECLARE @v_Tp_Nm_Usuario VARCHAR(50)
	DECLARE @v_Tp_Senha VARCHAR(50)
	DECLARE @v_Tp_Nm_Empresa VARCHAR(50)
	DECLARE @v_Tp_Email VARCHAR(50)
	
	SET @v_Tp_Id_Usuario = 0
	SET @v_Tp_Nm_Usuario = NULL
	SET @v_Tp_Senha = NULL
	SET @v_Tp_Nm_Empresa = NULL
	SET @v_Tp_Email = NULL	

	------------------------------------------------------------------------------------------------------------
	-----trata o delete na tabela
	------------------------------------------------------------------------------------------------------------
	IF EXISTS(SELECT * FROM DELETED)
	BEGIN
		-----captura nome da empresa
		SELECT	@vNm_Empresa = Nm_Empresa
		FROM DELETED D	INNER JOIN Consumidor C ON D.Id_Consumidor = C.Id_Consumidor
						INNER JOIN Filial F ON C.Id_Filial = F.Id_Filial
						INNER JOIN Empresa E ON E.Id_Empresa = F.Id_Empresa
						inner join holding H on h.Id_Holding = E.Id_Holding
		ORDER BY Nm_Usuario

		-----inicia cursor
		DECLARE ICursor CURSOR FOR
		SELECT	D.Id_Usuario,
				D.Nm_Usuario,
				D.Senha,
				Nm_Empresa,
				Email
		FROM DELETED D	INNER JOIN Consumidor C ON D.Id_Consumidor = C.Id_Consumidor
						INNER JOIN Filial F ON C.Id_Filial = F.Id_Filial
						INNER JOIN Empresa E ON E.Id_Empresa = F.Id_Empresa
						inner join holding H on h.Id_Holding = E.Id_Holding
		ORDER BY D.Nm_Usuario

		OPEN ICursor
		FETCH NEXT FROM ICursor INTO	@v_Tp_Id_Usuario,
										@v_Tp_Nm_Usuario,
										@v_Tp_Senha,
										@v_Tp_Nm_Empresa,
										@v_Tp_Email
					
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF NOT EXISTS(SELECT * FROM Ativvus_Login..Usuario_Global WHERE Nm_Usuario = @v_Tp_Nm_Usuario AND Empresa = @v_Tp_Nm_Empresa)
			BEGIN
				-----limpa todos os emails iguais para não existir duplicado			
				UPDATE	Ativvus_Login..Usuario_Global
				SET		EMail = NULL
				FROM 	Ativvus_Login..Usuario_Global
				WHERE	EMail = @v_Tp_Email
				-----altera registro
				INSERT INTO Ativvus_Login..Usuario_Global
				VALUES(@v_Tp_Nm_Usuario, @v_Tp_Senha, @v_Tp_Nm_Empresa, @v_Tp_Email, NULL, NULL, NULL, NULL, 2, NULL, NULL)
			END
			ELSE
			BEGIN
				-----limpa todos os emails iguais para não existir duplicado			
				UPDATE	Ativvus_Login..Usuario_Global
				SET		EMail = NULL
				FROM 	Ativvus_Login..Usuario_Global
				WHERE	EMail = @v_Tp_Email
				-----altera registro
				UPDATE	Ativvus_Login..Usuario_Global
				SET		Senha = @v_Tp_Senha,
						EMail = @v_Tp_Email
				WHERE	Nm_Usuario = @v_Tp_Nm_Usuario
						AND Empresa = @v_Tp_Nm_Empresa	
			END

			FETCH NEXT FROM ICursor INTO	@v_Tp_Id_Usuario,
											@v_Tp_Nm_Usuario,
											@v_Tp_Senha,
											@v_Tp_Nm_Empresa,
											@v_Tp_Email
		END
		CLOSE ICursor
		DEALLOCATE ICursor
		
		-----limpa usuario que nao sao mais utilizados
		DELETE 
		FROM	Ativvus_Login..Usuario_Global
		WHERE	Id_Usuario IN (	SELECT	Id_Usuario  
								FROM	Ativvus_Login..Usuario_Global UG
								WHERE	Empresa = @vNm_Empresa
										AND NOT EXISTS (SELECT Nm_Usuario FROM Usuario WHERE Nm_Usuario = UG.Nm_Usuario))
	END


	------------------------------------------------------------------------------------------------------------
	-----trata o insert na tabela
	------------------------------------------------------------------------------------------------------------
	IF EXISTS(SELECT * FROM INSERTED)
	BEGIN
		-----captura nome da empresa
		SELECT	@vNm_Empresa = Nm_Empresa
		FROM INSERTED D	INNER JOIN Consumidor C ON D.Id_Consumidor = C.Id_Consumidor
						INNER JOIN Filial F ON C.Id_Filial = F.Id_Filial
						INNER JOIN Empresa E ON E.Id_Empresa = F.Id_Empresa
						inner join holding H on h.Id_Holding = E.Id_Holding
		ORDER BY Nm_Usuario

		-----inicia cursor
		DECLARE ICursor CURSOR FOR
		SELECT	I.Id_Usuario,
				I.Nm_Usuario,
				I.Senha,
				Nm_Empresa,
				Email
		FROM INSERTED I	INNER JOIN Consumidor C ON I.Id_Consumidor = C.Id_Consumidor
						INNER JOIN Filial F ON C.Id_Filial = F.Id_Filial
						INNER JOIN Empresa E ON E.Id_Empresa = F.Id_Empresa
						inner join holding H on h.Id_Holding = E.Id_Holding
		ORDER BY I.Nm_Usuario

		OPEN ICursor
		FETCH NEXT FROM ICursor INTO	@v_Tp_Id_Usuario,
										@v_Tp_Nm_Usuario,
										@v_Tp_Senha,
										@v_Tp_Nm_Empresa,
										@v_Tp_Email
				
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF NOT EXISTS(SELECT * FROM Ativvus_Login..Usuario_Global WHERE Nm_Usuario = @v_Tp_Nm_Usuario AND Empresa = @v_Tp_Nm_Empresa)
			BEGIN
				-----limpa todos os emails iguais para não existir duplicado			
				UPDATE	Ativvus_Login..Usuario_Global
				SET		EMail = NULL
				FROM 	Ativvus_Login..Usuario_Global
				WHERE	EMail = @v_Tp_Email
				-----altera registro
				INSERT INTO Ativvus_Login..Usuario_Global
				VALUES(@v_Tp_Nm_Usuario, @v_Tp_Senha, @v_Tp_Nm_Empresa, @v_Tp_Email, NULL, NULL, NULL, NULL, 2, NULL, NULL)
			END
			ELSE
			BEGIN
				-----limpa todos os emails iguais para não existir duplicado			
				UPDATE	Ativvus_Login..Usuario_Global
				SET		EMail = NULL
				FROM 	Ativvus_Login..Usuario_Global
				WHERE	EMail = @v_Tp_Email
				-----altera registro
				UPDATE	Ativvus_Login..Usuario_Global
				SET		Senha = @v_Tp_Senha,
						EMail = @v_Tp_Email
				WHERE	Nm_Usuario = @v_Tp_Nm_Usuario
						AND Empresa = @v_Tp_Nm_Empresa	
			END

			FETCH NEXT FROM ICursor INTO	@v_Tp_Id_Usuario,
											@v_Tp_Nm_Usuario,
											@v_Tp_Senha,
											@v_Tp_Nm_Empresa,
											@v_Tp_Email
		END
		CLOSE ICursor
		DEALLOCATE ICursor

		-----limpa usuario que nao sao mais utilizados
		DELETE 
		FROM	Ativvus_Login..Usuario_Global
		WHERE	Id_Usuario IN (	SELECT	Id_Usuario  
								FROM	Ativvus_Login..Usuario_Global UG
								WHERE	Empresa = @vNm_Empresa
										AND NOT EXISTS (SELECT Nm_Usuario FROM Usuario WHERE Nm_Usuario = UG.Nm_Usuario))
	END
END