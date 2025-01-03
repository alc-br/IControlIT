USE [Amil]
GO
/****** Object:  StoredProcedure [dbo].[pa_si_Send_Mail]    Script Date: 23/02/2024 13:54:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pa_si_Send_Mail]  AS    
DECLARE @pProfile_Name VARCHAR(50)    
DECLARE @pEmail VARCHAR(50)    
DECLARE @pCopia VARCHAR(50)    
DECLARE @pAssunto VARCHAR(50)    
DECLARE @pTexto VARCHAR(8000)    
DECLARE @v_Id_Mail_Caixa_Siada INT    
DECLARE @v_Nm_Usuario VARCHAR(50)    
DECLARE @v_Nm_Empresa VARCHAR(50)    
DECLARE @v_mailitem_id INT     
DECLARE @vCMD VARCHAR(100)    
    
DECLARE ICursor CURSOR FOR    
SELECT MC.Id_Mail_Caixa_Siada    
FROM Mail_Sender MS INNER JOIN Mail_Caixa_Saida MC ON MS.Id_Mail_Sender = MC.Id_Mail_Sender    
WHERE Fl_Enviado = 0 AND Fl_QTD_Tentativa_Envio <= 2    
  AND Status_Mail = 1    
  AND MC.Dt_Programacao < GETDATE()    
ORDER BY E_MAIL_DESTINO    
    
OPEN ICursor    
FETCH NEXT FROM ICursor INTO @v_Id_Mail_Caixa_Siada    
         
WHILE @@FETCH_STATUS = 0    
BEGIN       
 SELECT @pProfile_Name = Profile_Name,    
   @pEmail = E_Mail_Destino,    
   @pCopia = E_Mail_Copia,    
   @pAssunto = Assunto,    
   @pTexto = CASE WHEN NOT Texto_1 IS NULL THEN '<p>  <font size="4"> ' + Texto_1 + ' </font>  </p>' ELSE '' END +    
      CASE WHEN NOT Texto_2 IS NULL THEN '<p>  <font size="4"> ' + Texto_2 + '</font>  </p> ' ELSE '' END +    
      CASE WHEN NOT Texto_3 IS NULL THEN '<p>  <font size="4"> ' + Texto_3 + '</font>  </p> ' ELSE '' END +    
      CASE WHEN NOT Texto_4 IS NULL THEN '<p>  <font size="4"> ' + Texto_4 + '</font>  </p> ' ELSE '' END +    
      ISNULL( '<p>  <font size="4"> ' +Texto_Adicional, ' ' + '</font>  </p>' ) +    
      CASE WHEN NOT Texto_5 IS NULL THEN '<p>  <font size="4"> ' + Texto_5 + '</font>  </p>' ELSE '' END +    
      '<body>    
      <table>    
      <tr><td><img src="https://www.icontrolit.com.br/Img_Sistema/logo/logo_k2a.png" width="267" height="97"></td></tr>    
      </table>    
      </body>'    
    
 FROM Mail_Sender MS INNER JOIN Mail_Caixa_Saida MC ON MS.Id_Mail_Sender = MC.Id_Mail_Sender    
 WHERE MC.Id_Mail_Caixa_Siada = @v_Id_Mail_Caixa_Siada    
    
 EXEC msdb..sp_send_dbmail @profile_name = @pProfile_Name,    
        @recipients = @pEmail,    
        @copy_recipients = @pCopia,    
        @subject = @pAssunto,    
        @body = @pTexto,    
        @body_format = 'HTML',    
        --@file_attachments = 'D:\Portal_Ativvus\Img_Sistema\img_email.png',    
        @mailitem_id = @v_mailitem_id OUTPUT    
    
 ------encerra o chamado quando for uma solicitação de roaming    
 UPDATE Solicitacao    
 SET  Fl_Status = 2,    
   Dt_Encerramento = GETDATE()    
 FROM Solicitacao     
 WHERE Id_Mail_Caixa_Saida_Operadora = @v_Id_Mail_Caixa_Siada    
    
 -----envia msg APP (somente dos subitens conta e chamado)    
 DECLARE SICursor CURSOR FOR    
 SELECT SUS.Nm_Usuario,    
   SEP.Nm_Empresa,    
   SMS.Assunto    
 FROM Mail_Sender SMS INNER JOIN Mail_Caixa_Saida SMC ON SMS.Id_Mail_Sender = SMC.Id_Mail_Sender    
       INNER JOIN Consumidor SCO ON SMC.E_Mail_Destino = SCO.EMail    
       INNER JOIN Usuario SUS ON SUS.Id_Consumidor = SCO.Id_Consumidor    
       INNER JOIN Filial SFI ON SFI.Id_Filial = SCO.Id_Filial     
       INNER JOIN Empresa SEP ON SEP.Id_Empresa = SFI.Id_Empresa     
 WHERE SMC.Id_Mail_Caixa_Siada = @v_Id_Mail_Caixa_Siada    
    
 OPEN SICursor    
 FETCH NEXT FROM SICursor INTO @v_Nm_Usuario, @v_Nm_Empresa, @pAssunto    
 WHILE @@FETCH_STATUS = 0    
 BEGIN    
  IF @pAssunto LIKE '%(Conta)%' OR  @pAssunto LIKE '%(Chamado)%'    
  BEGIN    
   SET @vCMD = 'start c:\app\messageSender.exe ' + @v_Nm_Empresa + '!' + @v_Nm_Usuario + '?' + Replace(@pAssunto, ' ', '_')    
   EXEC master..xp_cmdshell @vCMD    
  END    
  FETCH NEXT FROM SICursor INTO @v_Nm_Usuario, @v_Nm_Empresa, @pAssunto    
 END    
 CLOSE SICursor    
 DEALLOCATE SICursor    
    
 ---muda status da msg para enviado    
 UPDATE Mail_Caixa_Saida    
 SET  Dt_Saida = GETDATE(),    
   Id_Log_SQL = @v_mailitem_id,    
   Fl_Enviado = 1,    
   Fl_QTD_Tentativa_Envio = Fl_QTD_Tentativa_Envio + 1    
 WHERE Id_Mail_Caixa_Siada = @v_Id_Mail_Caixa_Siada    
    
 FETCH NEXT FROM ICursor INTO @v_Id_Mail_Caixa_Siada    
END    
CLOSE ICursor    
DEALLOCATE ICursor
