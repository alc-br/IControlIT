USE [Amil]
GO
/****** Object:  View [dbo].[vw_Pesquisa_Consumidor_Ativo]    Script Date: 12/01/2024 08:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER VIEW [dbo].[vw_Pesquisa_Consumidor_Ativo]
AS
-----Ativo (Numero Ativo)
SELECT	A.Nr_Ativo AS Pesquisa,
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'ativo' AS Campo		
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Ativo (controle interno)
SELECT	A.Finalidade AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'controle interno' AS Campo		
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
WHERE	NOT Finalidade IS NULL OR LTRIM(RTRIM(Finalidade)) = ''
UNION ALL
-----Ativo (Observacao)
SELECT	A.Observacao AS Pesquisa,
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
         '<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'observacao' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
			        LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
				    LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
WHERE	NOT Observacao IS NULL OR LTRIM(RTRIM(Observacao)) = ''
UNION ALL
-----Ativo (Complemento)
SELECT	SUBSTRING(REPLACE(REPLACE(A.Ativo_Complemento,'§',' / '),'¶','-'),1,50) + '...' AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
        '<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		CASE WHEN A.Fl_Desativado IN (2, 0) THEN 'Grid_Check_Pesquisa.png' ELSE 'Grid_Add_Pesquisa.png' END AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'dados adicionais' AS Campo
FROM	Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
				INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
				INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
				INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
				LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
				LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
				LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
WHERE	NOT Ativo_Complemento IS NULL OR NOT LTRIM(RTRIM(Ativo_Complemento)) = ''
UNION ALL
-----Ativo (Forma Aquisicao)
SELECT	AF.Nm_Ativo_Fr_Aquisicao AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
        '<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'forma aquisicao' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN Ativo_Parametro AP ON AP.Id_Ativo = A.Id_Ativo
					INNER JOIN Ativo_Fr_Aquisicao AF ON AF.Id_Ativo_Fr_Aquisicao = AP.Id_Ativo_Fr_Aquisicao
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
				    LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Nome)
SELECT	C.Nm_Consumidor AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
        '<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'consumidor' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Usuario)
SELECT	U.Nm_Usuario AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'usuario' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN vw_Usuario U ON U.Id_Consumidor = C.Id_Consumidor
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Matricula)
SELECT	C.Matricula AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'matricula' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Email)
SELECT	C.EMail AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'email' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Cargo)
SELECT	CRG.Nm_Cargo AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'cargo' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN Cargo CRG ON CRG.Id_Cargo = C.Id_Cargo
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Tipo)
SELECT	CT.Nm_Consumidor_Tipo AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'tipo consumidor' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN Consumidor_Tipo CT ON CT.Id_Consumidor_Tipo = C.Id_Consumidor_Tipo
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Status)
SELECT	CS.Nm_Consumidor_Status AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
   	    '<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'status consumidor' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN Consumidor_Status CS ON CS.Id_Consumidor_Status = C.Id_Consumidor_Status
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Empresa)
SELECT	EC.Nm_Empresa_Contratada AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
	    '<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'empresa contratada' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN Empresa_Contratada EC ON EC.Id_Empresa_Contratada = C.Id_Empresa_Contratada
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Filial)
SELECT	FI.Nm_Filial AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'filial' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Centro Custo)
SELECT	CC.Cd_Centro_Custo AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'centro custo' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
UNION ALL
-----Consumidor (Departamento)
SELECT	DP.Nm_Departamento AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'departamento' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN Departamento DP ON DP.Id_Departamento = C.Id_Departamento
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Setor)
SELECT	ST.Nm_Setor AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'setor' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN Setor ST ON ST.Id_Setor = C.Id_Setor
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Secao)
SELECT	SC.Nm_Secao AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'secao' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN Secao SC ON SC.Id_Secao = C.Id_Secao
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----ativo (Tipo)
SELECT	AT.Nm_Ativo_Tipo AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'tipo ativo' AS Campo
FROM	vw_Ativo A	INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo  
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
UNION ALL
-----Consumidor (Conglomerado)
SELECT	CG.Nm_Conglomerado AS Pesquisa, 
		ISNULL(C.Nm_Consumidor,' ') AS Nm_Consumidor,
		A.Nr_Ativo,
		'<strong>FORNECEDOR: </strong>' + CG.Nm_Conglomerado + ' | <strong>TIPO DE ATIVO: </strong>' + AT.Nm_Ativo_Tipo + ' | <strong>STATUS:</strong> ' + ATS.Nm_Ativo_Status + ' | <strong>FILIAL:</strong> ' + FI.Nm_Filial + ' | <strong>CENTRO DE CUSTO:</strong> ' + CC.Cd_Centro_Custo AS Tipo,
		'Última Conta - ' + ISNULL((SELECT RIGHT(CONVERT(VARCHAR,MAX(Dt_Lote),106),8) FROM Lote WHERE Id_Ativo = A.Id_Ativo), 'N/D') + ' | ' +
		'Total Carregado - ' + ISNULL((SELECT TOP 1 CONVERT(VARCHAR,CAST((Total) AS DECIMAL(13,2))) FROM Lote WHERE Id_Ativo = A.Id_Ativo AND Dt_Lote = (SELECT MAX(Dt_Lote) FROM Lote WHERE Id_Ativo = A.Id_Ativo)), '0') AS Lote,
		dbo.Fu_Link_HTTP ('Ativo.aspx',A.Id_Ativo,'') AS Link_1,
		CASE WHEN C.Id_Consumidor IS NULL THEN ' ' ELSE dbo.Fu_Link_HTTP ('Consumidor.aspx',C.Id_Consumidor,'') END AS Link_2,
		'Grid_Check_Pesquisa.png' AS Lixeira,
		0 AS ID,
		'null' AS Tabela,
		'conglomerado' AS Campo
FROM	vw_Ativo A	INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = A.Id_Ativo_Tipo
					INNER JOIN Rl_Consumidor_Ativo RA ON A.Id_Ativo = RA.Id_Ativo
					INNER JOIN vw_Consumidor C ON RA.Id_Consumidor = C.Id_Consumidor
					INNER JOIN Conglomerado CG ON CG.Id_Conglomerado = A.Id_Conglomerado
					LEFT JOIN vw_Ativo_Status ATS ON ATS.Id_Ativo_Status = A.Id_Ativo_Status
					LEFT JOIN Filial FI ON FI.Id_Filial = C.Id_Filial
					LEFT JOIN Centro_Custo CC ON CC.Id_Centro_Custo = C.Id_Centro_Custo
GO