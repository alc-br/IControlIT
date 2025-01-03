SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pa_DropList]  
    (  
    @pPakage VARCHAR(200),  
    @pDescricao VARCHAR(50) = NULL)  
AS            
-----filtra tipo de bilhete            
IF @pPAKAGE = 'sp_Drop_DB_Tipo_Parametro'            
BEGIN  
    SELECT DB_Tipo_Parametro AS ID,  
        DB_Tipo_Parametro AS Descricao  
    FROM Contrato_Bilhete  
END            
            
-----custo fixo            
IF @pPAKAGE = 'sp_Drop_Custo_Fixo'            
BEGIN  
    SELECT Id_Custo_Fixo AS ID,  
        Nm_Custo_Fixo AS Descricao,  
        dbo.Fu_Link_HTTP ('Custo_Fixo.aspx',Id_Custo_Fixo,'Raiz') AS Link  
    FROM Custo_Fixo  
    WHERE (Nm_Custo_Fixo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Custo_Fixo  
END            
            
-----custo fixo item            
IF @pPAKAGE = 'sp_Drop_Custo_Fixo_Item'            
BEGIN  
    SELECT CFI.Id_Custo_Fixo_Item AS ID,  
        Nm_Custo_Fixo + ' - ' + AT.Nm_Ativo_Tipo + ' - ' + C.Nm_Conglomerado AS Descricao,  
        dbo.Fu_Link_HTTP ('Custo_Fixo_Item.aspx',Id_Custo_Fixo_Item,'Raiz') AS Link  
    FROM Custo_Fixo_Item CFI INNER JOIN Custo_Fixo CF ON CFI.Id_Custo_Fixo =  CF.Id_Custo_Fixo  
        INNER JOIN Ativo_Tipo AT ON AT.Id_Ativo_Tipo = CFI.Id_Ativo_Tipo  
        INNER JOIN Conglomerado C ON C.Id_Conglomerado = CFI.Id_Conglomerado  
    WHERE (CF.Nm_Custo_Fixo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY CF.Nm_Custo_Fixo  
END            
            
-----estoque            
IF @pPAKAGE = 'sp_Drop_Estoque'            
BEGIN  
    SELECT Id_Aparelho AS ID,  
        Nr_Aparelho AS Descricao,  
        dbo.Fu_Link_HTTP ('Aparelho.aspx',Id_Aparelho,'Raiz') AS Link  
    FROM vw_Estoque_Aparelho  
    WHERE (Nr_Aparelho LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nr_Aparelho  
END            
            
-----estoque nota fiscal            
IF @pPAKAGE = 'sp_Drop_Estoque_Nota_Fiscal'            
BEGIN  
    SELECT Id_Estoque_Nota_Fiscal AS ID,  
        Nr_Nota_Fiscal AS Descricao,  
        dbo.Fu_Link_HTTP ('Nota_Fiscal.aspx',Id_Estoque_Nota_Fiscal,'Raiz') AS Link  
    FROM vw_Estoque_Nota_Fiscal  
    WHERE (Nr_Nota_Fiscal LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nr_Nota_Fiscal  
END            
            
-----arquivo carga            
IF @pPAKAGE = 'sp_Drop_Arquivo_Carga'            
BEGIN  
    SELECT Id_Arquivo AS ID,  
        Nm_Arquivo AS Descricao  
    FROM dbo.Cg_Bi_Arquivo  
    WHERE (Nm_Arquivo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Arquivo  
END            
            
-----rateio            
IF @pPAKAGE = 'sp_Drop_Rateio'            
BEGIN  
    SELECT Id_Rateio AS ID,  
        Nm_Rateio AS Descricao,  
        dbo.Fu_Link_HTTP ('Rateio.aspx',Id_Rateio,'Raiz') AS Link  
    FROM Rateio  
    WHERE (Nm_Rateio LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Rateio  
END            
            
-----fatura_tipo_rateio            
IF @pPAKAGE = 'sp_Drop_Fatura_Tipo_Rateio'            
BEGIN  
    SELECT Id_Fatura_Tipo_Rateio AS ID,  
        Nm_Fatura_Tipo_Rateio AS Descricao  
    FROM Fatura_Tipo_Rateio  
    WHERE (Nm_Fatura_Tipo_Rateio LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Fatura_Tipo_Rateio  
END            
            
-----fatura            
IF @pPAKAGE = 'sp_Drop_Fatura'            
BEGIN  
    SELECT Id_Fatura AS ID,  
        Nm_Fatura AS Descricao,  
        dbo.Fu_Link_HTTP ('Fatura.aspx',Id_Fatura,'Raiz') AS Link  
    FROM Fatura  
    WHERE (Nm_Fatura LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Fatura  
END            
            
-----fatura_parametro            
IF @pPAKAGE = 'sp_Drop_Fatura_Parametro'            
BEGIN  
    SELECT Id_Fatura_Parametro AS ID,  
        Nm_Fatura_Parametro AS Descricao,  
        dbo.Fu_Link_HTTP ('Fatura_Parametro.aspx',Id_Fatura_Parametro,'Raiz') AS Link  
    FROM Fatura_Parametro  
    WHERE (Nm_Fatura_Parametro LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Fatura_Parametro  
END            
            
-----fatura_parametro_campo            
IF @pPAKAGE = 'sp_Drop_Fatura_Parametro_Campo'            
BEGIN  
    SELECT FC.Id_Fatura_Parametro_Campo AS ID,  
        F.Nm_Fatura_Parametro + ' - ' + FC.Nm_Fatura_Parametro_Campo AS Descricao,  
        dbo.Fu_Link_HTTP ('Fatura_Parametro_Campo.aspx',Id_Fatura_Parametro_Campo,'Raiz') AS Link  
    FROM Fatura_Parametro_Campo FC INNER JOIN Fatura_Parametro F ON F.Id_Fatura_Parametro = FC.Id_Fatura_Parametro  
    WHERE (FC.Nm_Fatura_Parametro_Campo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Fatura_Parametro_Campo  
END            
            
-----tronco_grupo            
IF @pPAKAGE = 'sp_Drop_Tronco'            
BEGIN  
    SELECT Id_Tronco AS ID, Nm_Tronco + '-' + CO.Nm_Conglomerado AS Descricao,  
        dbo.Fu_Link_HTTP ('Tronco.aspx',Id_Tronco,'Raiz') AS Link  
    FROM vw_Tronco TR INNER JOIN vw_Conglomerado CO ON (TR.Id_Conglomerado = CO.Id_Conglomerado)  
    WHERE (Nm_Tronco LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Tronco  
END            
            
-----tronco_grupo            
IF @pPAKAGE = 'sp_Drop_Tronco_Grupo'            
BEGIN  
    SELECT Id_Tronco_Grupo AS ID,  
        Nm_Tronco_Grupo AS Descricao,  
        dbo.Fu_Link_HTTP ('Tronco_Grupo.aspx',Id_Tronco_Grupo,'Raiz') AS Link  
    FROM vw_Tronco_Grupo  
    WHERE (Nm_Tronco_Grupo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Tronco_Grupo  
END            
            
-----si_tela            
IF @pPAKAGE = 'sp_Drop_Si_Tela'            
BEGIN  
    SELECT Id_Tela AS ID,  
        MN.Nm_Menu + '-' + Nm_Tela AS Descricao  
    FROM Si_Tela TEL INNER JOIN dbo.Si_Menu MN ON (MN.Id_Menu = TEL.Id_Menu)  
    WHERE (Nm_Tela LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Tela  
END            
            
-----consumidor_tipo            
IF @pPAKAGE = 'sp_Drop_Consumidor_Tipo'            
BEGIN  
    SELECT Id_Consumidor_Tipo AS ID,  
        Nm_Consumidor_Tipo AS Descricao,  
        dbo.Fu_Link_HTTP ('Consumidor_Tipo.aspx',Id_Consumidor_Tipo,'Raiz') AS Link  
    FROM vw_Consumidor_Tipo  
    WHERE (Nm_Consumidor_Tipo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Consumidor_Tipo  
END            
            
-----consumidor_status            
IF @pPAKAGE = 'sp_Drop_Consumidor_Status'            
BEGIN  
    SELECT Id_Consumidor_Status AS ID,  
        Nm_Consumidor_Status AS Descricao,  
        dbo.Fu_Link_HTTP ('Consumidor_Status.aspx',Id_Consumidor_Status,'Raiz') AS Link  
    FROM vw_Consumidor_Status  
    WHERE (Nm_Consumidor_Status LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Consumidor_Status  
END            
            
-----ativo_status            
IF @pPAKAGE = 'sp_Drop_Ativo_Status'            
BEGIN  
    SELECT Id_Ativo_Status AS ID,  
        Nm_Ativo_Status AS Descricao,  
        dbo.Fu_Link_HTTP ('Ativo_Status.aspx',Id_Ativo_Status,'Raiz') AS Link  
    FROM vw_Ativo_Status  
    WHERE (Nm_Ativo_Status LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Ativo_Status  
END            
            
-----estoque_aparelho_status            
IF @pPAKAGE = 'sp_Drop_Estoque_Aparelho_Status'            
BEGIN  
    SELECT Id_Estoque_Aparelho_Status AS ID,  
        Nm_Estoque_Aparelho_Status AS Descricao,  
        dbo.Fu_Link_HTTP ('Estoque_Aparelho_Status.aspx',Id_Estoque_Aparelho_Status,'Raiz') AS Link  
    FROM vw_Estoque_Aparelho_Status  
    WHERE (Nm_Estoque_Aparelho_Status LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Estoque_Aparelho_Status  
END            
            
-----contrato_status            
IF @pPAKAGE = 'sp_Drop_Contrato_Status'            
BEGIN  
    SELECT Id_Contrato_Status AS ID,  
        Nm_Contrato_Status AS Descricao,  
        dbo.Fu_Link_HTTP ('Contrato_Status.aspx',Id_Contrato_Status,'Raiz') AS Link  
   FROM vw_Contrato_Status  
    WHERE (Nm_Contrato_Status LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Contrato_Status  
END            
            
-----contrato_ativo            
IF @pPAKAGE = 'sp_Drop_Contrato_Aditivo'            
BEGIN  
    SELECT Id_Contrato_Aditivo AS ID,  
        C.Descricao + ' - ' + A.Descricao AS Descricao,  
        dbo.Fu_Link_HTTP ('Contrato_Aditivo.aspx',Id_Contrato_Aditivo,'Raiz') AS Link  
    FROM vw_Contrato_Aditivo A INNER JOIN vw_Contrato C ON C.Id_Contrato = A.Id_Contrato  
    WHERE (A.Descricao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Descricao  
END            
            
-----contrato_sla_operacao            
IF @pPAKAGE = 'sp_Drop_Contrato_SLA_Operacao'            
BEGIN  
    SELECT CO.Id_Contrato_SLA_Operacao AS ID,  
        C.Descricao + ' - ' + CO.Descricao AS Descricao,  
        dbo.Fu_Link_HTTP ('Contrato_SLA_Operacao.aspx',Id_Contrato_SLA_Operacao,'Raiz') AS Link  
    FROM vw_Contrato_SLA_Operacao CO INNER JOIN vw_Contrato C ON C.Id_Contrato = CO.Id_Contrato  
    WHERE (CO.Descricao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Descricao  
END            
            
            
-----contrato_sla_servico            
IF @pPAKAGE = 'sp_Drop_Contrato_SLA_Servico'            
BEGIN  
    SELECT Id_Contrato_SLA_Servico  AS ID,  
        C.Descricao + ' - ' + CS.Descricao AS Descricao,  
        dbo.Fu_Link_HTTP ('Contrato_SLA_Servico.aspx',Id_Contrato_SLA_Servico,'Raiz') AS Link  
    FROM vw_Contrato_SLA_Servico CS INNER JOIN vw_Contrato C ON C.Id_Contrato = CS.Id_Contrato  
    WHERE (CS.Descricao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Descricao  
END            
            
-----usuario_perfil            
IF @pPAKAGE = 'sp_Drop_Usuario_Perfil'            
BEGIN  
    SELECT Id_Usuario_Perfil AS ID,  
        Nm_Usuario_Perfil AS Descricao  
    FROM Si_Usuario_Perfil  
    WHERE (Nm_Usuario_Perfil LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Usuario_Perfil  
END            
            
-----usuario_grupo            
IF @pPAKAGE = 'sp_Drop_Usuario_Grupo'            
BEGIN  
    SELECT Id_Usuario_Grupo AS ID,  
        Nm_Usuario_Grupo AS Descricao,  
        dbo.Fu_Link_HTTP ('Usuario_Grupo.aspx',Id_Usuario_Grupo,'Raiz') AS Link  
    FROM Usuario_Grupo  
    WHERE (Nm_Usuario_Grupo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Usuario_Grupo  
END            
            
-----idioma            
IF @pPAKAGE = 'sp_Drop_Idioma'            
BEGIN  
    SELECT Id_Idioma AS ID,  
        Nm_Idioma AS Descricao  
    FROM Si_Idioma  
    WHERE (Nm_Idioma LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Idioma  
END            
            
-----cargo            
IF @pPAKAGE = 'sp_Drop_Cargo'            
BEGIN  
    SELECT Id_Cargo AS ID,  
        Nm_Cargo AS Descricao,  
        dbo.Fu_Link_HTTP ('Cargo.aspx',Id_Cargo,'Raiz') AS Link  
    FROM vw_Cargo  
    WHERE (Nm_Cargo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Cargo  
END            
            
-----ativo fabricante            
IF @pPAKAGE = 'sp_Drop_Ativo_Fabricante'            
BEGIN  
    SELECT Id_Ativo_Fabricante AS ID,  
        Nm_Ativo_Fabricante AS Descricao,  
        dbo.Fu_Link_HTTP ('Ativo_Fabricante.aspx',Id_Ativo_Fabricante,'Raiz') AS Link  
    FROM vw_Ativo_Fabricante  
    WHERE (Nm_Ativo_Fabricante LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Ativo_Fabricante  
END            
            
-----ativo Modelo            
IF @pPAKAGE = 'sp_Drop_Ativo_Modelo'            
BEGIN  
    SELECT Id_Ativo_Modelo AS ID,  
        Nm_Ativo_Fabricante + Nm_Ativo_Modelo AS Descricao,  
        dbo.Fu_Link_HTTP ('Ativo_Modelo.aspx',Id_Ativo_Modelo,'Raiz') AS Link  
    FROM vw_Ativo_Modelo INNER JOIN vw_Ativo_Fabricante ON (vw_Ativo_Modelo.Id_Ativo_Fabricante = vw_Ativo_Fabricante.Id_Ativo_Fabricante)  
    WHERE (Nm_Ativo_Modelo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Ativo_Modelo  
END            
            
-----ativo tipo            
IF @pPAKAGE = 'sp_Drop_Ativo_Tipo'            
BEGIN  
    SELECT Id_Ativo_Tipo AS ID,  
        Nm_Ativo_Tipo AS Descricao,  
        dbo.Fu_Link_HTTP ('Ativo_Tipo.aspx',Id_Ativo_Tipo,'Raiz') AS Link  
    FROM vw_Ativo_Tipo  
    WHERE (Nm_Ativo_Tipo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Ativo_Tipo  
END            
            
-----sub grupo ativo tipo            
IF @pPAKAGE = 'sp_Drop_Sub_Grupo_Ativo_Tipo'            
BEGIN  
    SELECT Id_Ativo_Tipo_Sub_Grupo AS ID,  
        Nm_Ativo_Tipo_Sub_Grupo AS Descricao,  
        dbo.Fu_Link_HTTP ('Ativo_Tipo_Sub_Grupo.aspx',Id_Ativo_Tipo_Sub_Grupo,'Raiz') AS Link  
    FROM vw_Ativo_Tipo_Sub_Grupo  
    WHERE (Nm_Ativo_Tipo_Sub_Grupo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Ativo_Tipo_Sub_Grupo  
END            
            
-----tipo grupo ativo tipo            
IF @pPAKAGE = 'sp_Drop_Tipo_Grupo_Ativo_Tipo'            
BEGIN  
    SELECT Id_Ativo_Tipo_Grupo_Tipo AS ID,  
        Nm_Ativo_Tipo_Grupo_Tipo AS Descricao,  
        dbo.Fu_Link_HTTP ('Ativo_Tipo_Grupo_Tipo.aspx',Id_Ativo_Tipo_Grupo_Tipo,'Raiz') AS Link  
    FROM vw_Ativo_Tipo_Grupo_Tipo  
    WHERE (Nm_Ativo_Tipo_Grupo_Tipo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Ativo_Tipo_Grupo_Tipo  
END            
            
-----grupo ativo tipo            
IF @pPAKAGE = 'sp_Drop_Grupo_Ativo_Tipo'            
BEGIN  
    SELECT Id_Ativo_Tipo_Grupo AS ID,  
        Nm_Ativo_Tipo_Grupo AS Descricao,  
        dbo.Fu_Link_HTTP ('Ativo_Tipo.aspx',Id_Ativo_Tipo_Grupo,'Raiz') AS Link  
    FROM Ativo_Tipo_Grupo  
    WHERE (Nm_Ativo_Tipo_Grupo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
        AND Visivel = 2  
    ORDER BY Nm_Ativo_Tipo_Grupo  
END            
            
-----auditoria status            
IF @pPAKAGE = 'sp_Drop_Auditoria_Status'            
BEGIN  
    SELECT Id_Auditoria_Status AS ID,  
        Nm_Auditoria_Status AS Descricao,  
        dbo.Fu_Link_HTTP ('Auditoria_Status.aspx',Id_Auditoria_Status,'Raiz') AS Link  
    FROM vw_Auditoria_Status  
    WHERE (Nm_Auditoria_Status LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Auditoria_Status  
END            
            
-----estoque entrega aparelho            
IF @pPAKAGE = 'sp_Drop_Estoque_Endereco_Entrega'            
BEGIN  
    SELECT Id_Estoque_Endereco_Entrega AS ID,  
        Nm_Estoque_Endereco_Entrega AS Descricao,  
        dbo.Fu_Link_HTTP ('Endereco_Entrega.aspx',Id_Estoque_Endereco_Entrega,'Raiz') AS Link  
    FROM Estoque_Endereco_Entrega  
    WHERE (Nm_Estoque_Endereco_Entrega LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Estoque_Endereco_Entrega  
END            
            
-----holding            
IF @pPAKAGE = 'sp_Drop_Holding'            
BEGIN  
    SELECT Id_Holding AS ID,  
        Nm_Holding AS Descricao,  
        dbo.Fu_Link_HTTP ('Holding.aspx',Id_Holding,'Raiz') AS Link  
    FROM vw_Holding  
    WHERE (Nm_Holding LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Holding  
END            
            
-----empresa servico            
IF @pPAKAGE = 'sp_Drop_Conglomerado'            
BEGIN  
    SELECT Id_Conglomerado AS ID,  
        Nm_Conglomerado AS Descricao,  
        dbo.Fu_Link_HTTP ('Conglomerado.aspx',Id_Conglomerado,'Raiz') AS Link  
    FROM vw_Conglomerado  
    WHERE (Nm_Conglomerado LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Conglomerado  
END            
            
-----empresa servico            
IF @pPAKAGE = 'sp_Drop_Empresa_Contratada'            
BEGIN  
    SELECT Id_Empresa_Contratada AS ID,  
        Nm_Empresa_Contratada AS Descricao,  
        dbo.Fu_Link_HTTP ('Empresa_Contratada.aspx',Id_Empresa_Contratada,'Raiz') AS Link  
    FROM vw_Empresa_Contratada  
    WHERE (Nm_Empresa_Contratada LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Empresa_Contratada  
END            
            
-----empresa            
IF @pPAKAGE = 'sp_Drop_Empresa'            
BEGIN  
    SELECT Id_Empresa AS ID,  
        Nm_Empresa AS Descricao,  
        dbo.Fu_Link_HTTP ('Empresa.aspx',Id_Empresa,'Raiz') AS Link  
    FROM vw_Empresa  
    WHERE (Nm_Empresa LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Empresa  
END            
            
-----filial            
IF @pPAKAGE = 'sp_Drop_Filial'            
BEGIN  
    SELECT Id_Filial AS ID,  
        Nm_Filial AS Descricao,  
        dbo.Fu_Link_HTTP ('Filial.aspx',Id_Filial,'Raiz') AS Link  
    FROM vw_Filial  
    WHERE (Nm_Filial LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Filial  
END            
            
-----bilhete_tipo            
IF @pPAKAGE = 'sp_Drop_Bilhete_Tipo'            
BEGIN  
    SELECT Id_Bilhete_Tipo AS ID,  
        CASE WHEN LTRIM(RTRIM(Nm_Bilhete_Tipo)) = '' THEN Nm_Bilhete_Descricao ELSE Nm_Bilhete_Tipo END AS Descricao,  
        dbo.Fu_Link_HTTP ('Bilhete_Tipo.aspx',Id_Bilhete_Tipo,'Raiz') AS Link  
    FROM vw_Bilhete_Tipo  
    WHERE (Nm_Bilhete_Tipo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Bilhete_Tipo  
END            
            
-----centro_custo            
IF @pPAKAGE = 'sp_Drop_Centro_Custo'            
BEGIN  
    SELECT Id_Centro_Custo AS ID,  
        Cd_Centro_Custo AS Descricao,  
        dbo.Fu_Link_HTTP ('Centro_Custo.aspx',Id_Centro_Custo,'Raiz') AS Link  
    FROM vw_Centro_Custo  
    WHERE (Cd_Centro_Custo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Cd_Centro_Custo  
END            
            
-----departamento            
IF @pPAKAGE = 'sp_Drop_Departamento'            
BEGIN  
    SELECT Id_Departamento AS ID,  
        Nm_Departamento AS Descricao,  
        dbo.Fu_Link_HTTP ('Departamento.aspx',Id_Departamento,'Raiz') AS Link  
    FROM vw_Departamento  
    WHERE (Nm_Departamento LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Departamento  
END            
            
-----setor            
IF @pPAKAGE = 'sp_Drop_Setor'            
BEGIN  
    SELECT Id_Setor AS ID,  
        Nm_Setor AS Descricao,  
        dbo.Fu_Link_HTTP ('Setor.aspx',Id_Setor,'Raiz') AS Link  
    FROM vw_Setor  
    WHERE (Nm_Setor LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Setor  
END            
            
-----secao            
IF @pPAKAGE = 'sp_Drop_Secao'            
BEGIN  
    SELECT Id_Secao AS ID,  
        Nm_Secao AS Descricao,  
        dbo.Fu_Link_HTTP ('Secao.aspx',Id_Secao,'Raiz') AS Link  
    FROM vw_Secao  
    WHERE (Nm_Secao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Secao  
END            
            
-----ativo_fr_aquisicao            
IF @pPAKAGE = 'sp_Drop_Ativo_Fr_Aquisicao'            
BEGIN  
    SELECT Id_Ativo_Fr_Aquisicao AS ID,  
        Nm_Ativo_Fr_Aquisicao AS Descricao,  
        dbo.Fu_Link_HTTP ('Ativo_Fr_Aquisicao.aspx',Id_Ativo_Fr_Aquisicao,'Raiz') AS Link  
    FROM vw_Ativo_Fr_Aquisicao  
    WHERE (Nm_Ativo_Fr_Aquisicao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Ativo_Fr_Aquisicao  
END            
            
-----contrato            
IF @pPAKAGE = 'sp_Drop_Contrato_Corpo'            
BEGIN  
    SELECT Id_Contrato AS ID,  
        Nr_Contrato + ' - ' + Descricao AS Descricao,  
        dbo.Fu_Link_HTTP ('Contrato.aspx',Id_Contrato,'Raiz') AS Link  
    FROM vw_Contrato  
    WHERE (Descricao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Descricao  
END            
            
-----contrato            
IF @pPAKAGE = 'sp_Drop_Contrato'            
BEGIN  
    SELECT Id_Contrato AS ID,  
        Nr_Contrato + ' - ' + Descricao AS Descricao,  
        dbo.Fu_Link_HTTP ('Consulta_Contrato.aspx',Id_Contrato,'Raiz') AS Link  
    FROM vw_Contrato  
    WHERE (Descricao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Descricao  
END            
          
-----contrato            
IF @pPAKAGE = 'sp_Drop_Contrato_Detalhe'            
BEGIN  
    SELECT ISNULL(Id_Contrato,0)        AS Id_Contrato,  
        ISNULL(Nr_Contrato,'')        AS Nr_Contrato,  
        ISNULL(Descricao,'')        AS Descricao,  
        ISNULL(CS.Nm_Contrato_Status,'')     AS Nm_Contrato_Status,  
        ISNULL(S.Nm_Servico,'')        AS Nm_Servico,  
        ISNULL(Dt_Inicio_Vigencia,'1900-01-01')    AS Dt_Inicio_Vigencia,  
        ISNULL(Dt_Fim_Vigencia,'1900-01-01')    AS Dt_Fim_Vigencia,  
        ISNULL(F.Nm_Filial,'Todas')       AS Nm_Filial,  
        ISNULL(EC.Nm_Empresa_Contratada,'')     AS Nm_Empresa_Contratada,  
        ISNULL(Objeto,'')         AS Objeto,  
        ISNULL(I.Nm_Contrato_Indice,'')         AS Nm_Contrato_Indice,  
        ISNULL((SELECT TOP 1  
            Nm_Holding  
        FROM Holding),'') AS Empresa,  
        dbo.Fu_Link_HTTP ('Consulta_Contrato.aspx',Id_Contrato,'Raiz') AS Link  
    FROM Contrato C INNER JOIN Contrato_Status CS ON C.Id_Contrato_Status = CS.Id_Contrato_Status  
        INNER JOIN Servico S ON C.Id_Servico = S.Id_Servico  
        INNER JOIN Empresa_Contratada EC ON C.Id_Empresa_Contratada = EC.Id_Empresa_Contratada  
        LEFT JOIN Filial F ON C.Id_Filial = F.Id_Filial  
        LEFT JOIN Contrato_Indice I ON C.Id_Contrato_Indice = I.Id_Contrato_Indice  
    WHERE (Descricao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL) and C.Fl_Desativado = 0
    ORDER BY Descricao  
END            
            
-----consumidor            
IF @pPAKAGE = 'sp_Drop_Consumidor'            
BEGIN  
    SELECT Id_Consumidor AS ID,  
        Nm_Consumidor AS Descricao  
    FROM vw_Consumidor  
    WHERE (Nm_Consumidor LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Consumidor  
END            
            
-----consumidor X ativo            
IF @pPAKAGE = 'sp_Drop_Consumidor_Ativo'            
BEGIN  
    SELECT VWCON.Id_Consumidor AS ID,  
        VWCON.Nm_Consumidor AS Descricao  
    FROM vw_Consumidor VWCON LEFT JOIN Usuario VWUSU ON (VWUSU.Id_Consumidor = VWCON.Id_Consumidor)  
    WHERE ((Nm_Consumidor LIKE '%'+@pDescricao+'%' OR Nm_Usuario LIKE '%'+@pDescricao+'%'))  
    ORDER BY VWCON.Nm_Consumidor  
END            
            
-----ativo_complemento            
IF @pPAKAGE = 'sp_Drop_Ativo_Complemento'            
BEGIN  
    SELECT Id_Ativo_Complemento AS ID,  
        AT.Nm_Ativo_Tipo + ' / ' + Nm_Ativo_Complemento AS Descricao,  
        dbo.Fu_Link_HTTP ('Ativo_Complemento.aspx',Id_Ativo_Complemento,'Raiz') AS Link  
    FROM Ativo_Complemento AC INNER JOIN Ativo_Tipo AT ON AC.Id_Ativo_Tipo = AT.Id_Ativo_Tipo  
    WHERE (Nm_Ativo_Complemento LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Descricao  
END            
            
-----servico            
IF @pPAKAGE = 'sp_Drop_Servico'            
BEGIN  
    SELECT Id_Servico AS ID,  
        Nm_Servico AS Descricao,  
        dbo.Fu_Link_HTTP ('Servico.aspx',Id_Servico,'Raiz') AS Link  
    FROM vw_Servico  
    WHERE (Nm_Servico LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Servico  
END            
            
-----ativo            
IF @pPAKAGE = 'sp_Drop_Ativo'            
BEGIN  
    SELECT Id_Ativo AS ID,  
        Nr_Ativo AS Descricao  
    FROM vw_Ativo  
    WHERE (Nr_Ativo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nr_Ativo  
END            
            
-----ativo + conglomerado            
IF @pPAKAGE = 'sp_Drop_Ativo_Conglomerado'            
BEGIN  
    SELECT A.Id_Ativo AS ID,  
        A.Nr_Ativo + ' - ' + C.Nm_Conglomerado AS Descricao  
    FROM vw_Ativo A INNER JOIN Conglomerado C ON A.Id_Conglomerado = C.Id_Conglomerado  
    WHERE (Nr_Ativo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nr_Ativo  
END            
            
-----auditoria acompanhamento            
IF @pPAKAGE = 'sp_Drop_Auditoria_Acompanhamento'        
BEGIN  
    SELECT Id_Auditoria_Acompanhamento AS ID,  
        AC.Descricao + ' - ' + AL.Descricao AS Descricao,  
        dbo.Fu_Link_HTTP ('Auditoria_Acompanhamento.aspx',Id_Auditoria_Acompanhamento,'Raiz') AS Link  
    FROM Auditoria_Acompanhamento AC INNER JOIN Auditoria_Lote AL ON AC.Id_Auditoria_Lote = AL.Id_Auditoria_Lote  
    WHERE (AC.Descricao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Descricao  
END            
            
-----status auditoria            
IF @pPAKAGE = 'sp_Drop_Auditoria_Status'            
BEGIN  
    SELECT Id_Auditoria_Status AS ID,  
        Nm_Auditoria_Status AS Descricao  
    FROM dbo.Auditoria_Status  
    WHERE (Nm_Auditoria_Status LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Auditoria_Status  
END            
            
-----auditoria lote            
IF @pPAKAGE = 'sp_Drop_Auditoria_Lote'            
BEGIN  
    SELECT Id_Auditoria_Lote AS ID,  
        Descricao   AS Descricao  
    FROM Auditoria_Lote  
    WHERE (Descricao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Descricao  
END            
            
-----Solicitacao SLA            
IF @pPAKAGE = 'sp_Drop_Solicitacao_SLA'            
BEGIN  
    SELECT Id_Solicitacao_SLA AS ID,  
        Nm_Solicitacao_SLA AS Descricao,  
        dbo.Fu_Link_HTTP ('SLA_Solicitacao.aspx',Id_Solicitacao_SLA,'Raiz') AS Link  
    FROM vw_Solicitacao_SLA  
    WHERE (Nm_Solicitacao_SLA LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Solicitacao_SLA  
END            
            
-----Solicitacao Tipo            
IF @pPAKAGE = 'sp_Drop_Solicitacao_Tipo'            
BEGIN  
    SELECT Id_Solicitacao_Tipo AS ID,  
        Nm_Ativo_Tipo + '-' + Nm_Solicitacao_Tipo AS Descricao,  
        dbo.Fu_Link_HTTP ('Tipo_Solicitcao.aspx',Id_Solicitacao_Tipo,'Raiz') AS Link  
    FROM vw_Solicitacao_Tipo ST INNER JOIN Ativo_Tipo AT ON ST.Id_Ativo_Tipo = AT.Id_Ativo_Tipo  
    WHERE (Nm_Solicitacao_Tipo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Ativo_Tipo, Nm_Solicitacao_Tipo  
END            
            
-----solicitacao solucao            
IF @pPAKAGE = 'sp_Drop_Solicitacao_Solucao'            
BEGIN  
    SELECT Id_Solicitacao_Solucao AS ID,  
        AT.Nm_Ativo_Tipo + '-' + Nm_Solicitacao_Solucao AS Descricao,  
        dbo.Fu_Link_HTTP ('Solucao.aspx',Id_Solicitacao_Solucao,'Raiz') AS Link  
    FROM vw_Solicitacao_Solucao SS INNER JOIN Ativo_Tipo AT ON SS.Id_Ativo_Tipo = AT.Id_Ativo_Tipo  
    WHERE (Nm_Solicitacao_Solucao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Ativo_Tipo, Nm_Solicitacao_Solucao  
END            
            
-----solicitacao data parada            
IF @pPAKAGE = 'sp_Drop_Solicitacao_Data_Parada'            
BEGIN  
    SELECT Id_Data_Parada AS ID,  
        Descricao + ' (' + CONVERT(VARCHAR(10), Data, 103) + ')' AS Descricao,  
        dbo.Fu_Link_HTTP ('Data_Parada.aspx',Id_Data_Parada,'Raiz') AS Link  
    FROM Solicitacao_Data_Parada  
    WHERE (Descricao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Descricao  
END            
            
-----Solicitacao Fila de Atendimento            
IF @pPAKAGE = 'sp_Drop_Solicitacao_Fila_Atendimento'            
BEGIN  
    SELECT Id_Solicitacao_Fila_Atendimento AS ID,  
        Nm_Solicitacao_Fila_Atendimento AS Descricao,  
        dbo.Fu_Link_HTTP ('Fila_Atendimento.aspx',Id_Solicitacao_Fila_Atendimento,'Raiz') AS Link  
    FROM vw_Solicitacao_Fila_Atendimento  
    WHERE (Nm_Solicitacao_Fila_Atendimento LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Solicitacao_Fila_Atendimento  
END            
            
-----usuario            
IF @pPAKAGE = 'sp_Drop_Usuario'            
BEGIN  
    SELECT Id_Usuario AS ID,  
        Nm_Usuario AS Descricao,  
        dbo.Fu_Link_HTTP ('Usuario.aspx',Id_Usuario,'Raiz') AS Link  
    FROM Usuario  
    WHERE (Nm_Usuario LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Usuario  
END            
            
-----tipo de messagem de envio de email            
IF @pPAKAGE = 'sp_Drop_Mail_Sender'            
BEGIN  
    SELECT Id_Mail_Sender AS ID,  
        Assunto AS Descricao  
    FROM Mail_Sender  
    WHERE (Assunto LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Assunto  
END            
            
-----bilhete            
IF @pPAKAGE = 'sp_Drop_Bilhete'            
BEGIN  
    SELECT Id_Bilhete AS ID,  
        SUBSTRING(CONVERT(VARCHAR(6),L.Dt_Lote,112),1,6) + ' - ' + Nm_Bilhete_Tipo + ' | R$' + CONVERT(VARCHAR, DB_Custo) AS Descricao,  
        dbo.Fu_Link_HTTP ('Bilhete_Manual.aspx',CONVERT(VARCHAR,CAST(Id_Bilhete AS DECIMAL(15))),'Raiz') AS Link  
    FROM vw_Bilhete B inner join Lote L ON B.Id_Lote = L.Id_Lote  
    WHERE L.Id_Ativo IN (SELECT Id_Ativo  
        FROM Ativo  
        WHERE Nr_Ativo = @pDescricao)  
        AND Id_Bilhete_Tipo IN (SELECT Id_Bilhete_Tipo  
        FROM Bilhete_Manual_Tipo)  
    ORDER BY Descricao  
END            
            
-----ativo tipo solicitacao (solicitacao)            
IF @pPAKAGE = 'sp_Drop_Ativo_Tipo_Solicitacao'            
BEGIN  
    SELECT Id_Ativo_Tipo AS ID,  
        Nm_Ativo_Tipo AS Descricao,  
        dbo.Fu_Link_HTTP ('Ativo_Tipo.aspx',Id_Ativo_Tipo,'Raiz') AS Link  
    FROM vw_Ativo_Tipo AT  
    WHERE (Nm_Ativo_Tipo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
        AND EXISTS(SELECT *  
        FROM Solicitacao_Tipo ST  
        WHERE ST.Id_Ativo_Tipo = AT.Id_Ativo_Tipo)  
    ORDER BY Nm_Ativo_Tipo  
END            
            
-----solictacao_permissao            
IF @pPAKAGE = 'sp_Drop_Solicitacao_Permissao'            
BEGIN  
    SELECT Id_Solicitacao_Permissao AS ID,  
        Nm_Solicitacao_Permissao AS Descricao,  
        dbo.Fu_Link_HTTP ('Usuario.aspx',Id_Solicitacao_Permissao,'Raiz') AS Link  
    FROM Solicitacao_Permissao  
    WHERE (Nm_Solicitacao_Permissao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Solicitacao_Permissao  
END            
            
-----app monitora trafego            
IF @pPAKAGE = 'sp_Drop_App_Monitoramento'            
BEGIN  
    SELECT ISNULL(AT.Id_Ativo,'') AS ID,  
        ISNULL(AT.Nr_Ativo,'') AS Descricao,  
        dbo.Fu_Link_HTTP ('Ativo_App_Trafego.aspx',Id_Ativo_Tipo,'Raiz') AS Link  
    FROM Ativo_App_Trafego AAT INNER JOIN Ativo AT ON AAT.Id_Ativo = AT.Id_Ativo  
    WHERE (Nr_Ativo LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nr_Ativo  
END            
            
-----solictacao processo unidade            
IF @pPAKAGE = 'sp_Drop_Solicitacao_Processo_Unidade'            
BEGIN  
    SELECT ISNULL(SUP.Id_Processo_Unidade_Solicitacao,'') AS ID,  
        ISNULL(SUP.Nm_Processo_Unidade_Solicitacao,'') AS Descricao  
    FROM Solicitacao_Unidade_Processo SUP  
    WHERE (Nm_Processo_Unidade_Solicitacao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Processo_Unidade_Solicitacao  
END      
      
-----contrato_indice      
IF @pPAKAGE = 'sp_Drop_Contrato_Indice'            
BEGIN  
    SELECT  
        Id_Contrato_Indice AS ID,  
        Nm_Contrato_Indice AS Descricao,  
        dbo.Fu_Link_HTTP ('Contrato_Indice.aspx',Id_Contrato_Indice,'Raiz') AS Link  
    FROM Contrato_Indice  
    WHERE (Nm_Contrato_Indice LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Nm_Contrato_Indice  
END      
     
-----contrato_sla_servico para auditoria           
IF @pPAKAGE = 'sp_Drop_Contrato_SLA_Servico_Auditoria'             
BEGIN  
    SELECT Id_Contrato_SLA_Servico  AS ID,  
        C.Descricao + ' - ' + CS.Descricao AS Descricao  
    FROM Contrato_SLA_Servico CS  
        INNER JOIN Contrato C ON C.Id_Contrato = CS.Id_Contrato  
        INNER JOIN Bilhete_Tipo BT ON CS.Tipo_Servico LIKE BT.Nm_Bilhete_Tipo  
    WHERE (CS.Descricao LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Descricao  
END   
   
----- Auditoria Contestação            
IF @pPAKAGE = 'sp_Auditoria_Contestacao'            
BEGIN  
    SELECT  
        Id_Auditoria_Contestacao AS ID,  
        Nm_Fatura AS Descricao,  
        dbo.Fu_Link_HTTP ('Auditoria_Contestacao.aspx',Id_Auditoria_Contestacao,'Raiz') AS Link  
    FROM Auditoria_Contestacao AC  
        INNER JOIN Fatura F ON AC.Id_Fatura = f.Id_Fatura  
    WHERE (Nm_Fatura LIKE '%'+@pDescricao+'%' OR @pDescricao IS NULL)  
    ORDER BY Id_Auditoria_Contestacao DESC  
END  
  
-----consulta Ativo_Tipo_Grupo  
IF @pPAKAGE = 'sp_Drop_Ativo_Tipo_Grupo'            
BEGIN  
    SELECT Id_Ativo_Tipo_Grupo AS ID,  
        Nm_Ativo_Tipo_Grupo AS Descricao,  
        dbo.Fu_Link_HTTP ('Consulta_Conta.aspx',Id_Ativo_Tipo_Grupo,'Raiz') AS Link  
    FROM Ativo_Tipo_Grupo  
    WHERE Visivel = 2  
    ORDER BY Descricao  
END
