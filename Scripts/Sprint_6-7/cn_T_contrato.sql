SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[cn_T_contrato] (@pPakage					VARCHAR(200),
										@pPakage_Filtro				VARCHAR(200)	= NULL,
										@pId_Filtro_1				VARCHAR(MAX)	= NULL,
										@pId_Filtro_Filial			VARCHAR(MAX)	= NULL,
										@pId_Filtro_Usuario			INT				= NULL,
										@p_Id_Filtro_Centro_Custo	VARCHAR(MAX)	= NULL,
										@p_Id_Filtro_Departamento	VARCHAR(MAX)	= NULL,
										@p_Id_Filtro_Setor			VARCHAR(MAX)	= NULL,
										@pDt_LoteDe					VARCHAR(6)		= NULL,
										@pDt_LoteAte				VARCHAR(6)		= NULL,
										@pAtivo_Tipo_Grupo			VARCHAR(50)		= NULL) AS
	
				select
				c.Id_Contrato as [ID_Contrato],
				c.Nr_Contrato as [Contrato],
				c.Descricao as [Descricao contrato],
				cst.Nm_Contrato_Status as [status],
				s.Nm_Servico as [Servico],
				c.Dt_Inicio_Vigencia as [Data Inicio contrato],
				c.Dt_Fim_Vigencia as [Data Fim contrato],
				E.Nm_Empresa_Contratada as [Empresa Contratada],
				f.Nm_Filial as [Filial],
				c.Objeto as [Objeto],
				C.Fl_Desativado as [Fl_Desativado]
				INTO #Temp_Export

				from contrato C

				inner join Contrato_Status cst on cst.Id_Contrato_Status = c.Id_Contrato_Status
				inner join Empresa_Contratada  e ON E.Id_Empresa_Contratada = C.Id_Empresa_Contratada
				left join Filial f on f.Id_Filial = c.Id_Filial
				inner join Servico S on s.Id_Servico = c.Id_Servico
	
-----exportacao
IF @pPakage = 'sp_contrato_Export'
BEGIN
	SELECT * FROM #Temp_Export
END

-----grid
IF @pPakage = 'sp_Contrato'
BEGIN
	SELECT * FROM #Temp_Export
END