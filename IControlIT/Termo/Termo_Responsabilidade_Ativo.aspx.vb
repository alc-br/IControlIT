
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.tool.xml
Imports System.Web.UI.WebControls

Partial Public Class Termo_Responsabilidade_Ativo
    Inherits System.Web.UI.Page

    Dim WS_Cadastro As New WS_GUA_Cadastro.WSCadastro
    Dim WS_Modulo As New WS_GUA_Modulo.WSModulo
    Dim vDataSet As New Data.DataSet
    Dim vDataSetXML As New Data.DataSet
    Private vLogoUrl As String

    Private Class TermoData
        Public Property IdAtivo As String
        Public Property IdConsumidor As String
        Public Property NmAtivoTipoGrupo As String
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Conn_Banco") Is Nothing OrElse Session("Nm_Usuario") Is Nothing Then
            Response.Write("Sessão expirada. Faça login novamente.")
            Response.End()
            Return
        End If

        ' [INÍCIO - ICTRL-NF-202506-026]
        ' Novo fluxo para gerar link de aceite
        If Not String.IsNullOrEmpty(Request("action")) AndAlso Request("action") = "send_link" Then
            ' O nome da empresa/cliente já é carregado na variável vEmpresa mais abaixo
            vDataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Valida_Usuario", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)
            Dim vEmpresa As String = vDataSet.Tables(0).Rows(0).Item("Empresa").ToString

            Dim idAtivo As Integer = CInt(Request("Id_Ativo"))
            Dim idConsumidor As Integer = CInt(Request("Id_Consumidor"))
            EnviarLinkAceite(idAtivo, idConsumidor, vEmpresa)
            Return
        End If
        ' [FIM - ICTRL-NF-202506-026]

        If Not Page.IsPostBack Then
            ' --- CREDENCIAIS DOS WEBSERVICES ---
            WS_Cadastro.Credentials = System.Net.CredentialCache.DefaultCredentials
            WS_Modulo.Credentials = System.Net.CredentialCache.DefaultCredentials

            ' --- VALIDAÇÃO DO USUÁRIO ---
            vDataSet = WS_Modulo.Validacao(Session("Conn_Banco"), "Sd_Valida_Usuario", Session("Nm_Usuario"), Nothing, "Dia_Semana", Nothing, Nothing)

            If vDataSet.Tables(0).Rows.Count > 0 AndAlso vDataSet.Tables(0).Columns.Contains("Logo") Then
                vLogoUrl = vDataSet.Tables(0).Rows(0)("Logo").ToString()
            Else
                vLogoUrl = ""
            End If

            ' Validação
            Dim vEmpresa As String = vDataSet.Tables(0).Rows(0).Item("Empresa").ToString
            vDataSetXML.ReadXml(App_Path() & "Termo/Base/Dados_Default.xml".Replace("Default", vEmpresa))

            ' Parâmetros da URL
            Dim ativos = Request("Id_Ativo").Split(","c)
            Dim consumidores = Request("Id_Consumidor").Split(","c)
            Dim grupos = Request("Nm_Ativo_Tipo_Grupo").Split(","c)

            If ativos.Length <> consumidores.Length OrElse ativos.Length <> grupos.Length Then
                Response.Write("Parâmetros inconsistentes. Verifique se todos os arrays possuem a mesma quantidade de elementos.")
                Response.End()
                Return
            End If

            ' ✅ Se for verificação simples (check=1), testamos e retornamos status
            If Not String.IsNullOrEmpty(Request("check")) AndAlso Request("check") = "1" Then
                Dim termosValidos As Integer = 0

                For i = 0 To ativos.Length - 1
                    Dim dsAtivo = WS_Cadastro.Ativo(
                    Session("Conn_Banco"),
                    ativos(i),
                    Nothing,             ' pNr_Ativo
                    Nothing,             ' pFinalidade
                    Nothing,             ' pId_Ativo_Tipo
                    Nothing,             ' pId_Conglomerado
                    Nothing,             ' pId_Ativo_Modelo
                    Nothing,             ' pLocalidade
                    Nothing,             ' pDt_Ativacao
                    Nothing,             ' pObservacao
                    Nothing,             ' pAtivo_Complemento
                    Nothing,             ' pId_Ativo_Status
                    Nothing,             ' pArray_Consumidor
                    Nothing,             ' pId_Usuario_Permissao
                    "sd_SL_Termo",       ' pPakage
                    True,                ' pRetorno
                    Nothing,             ' pEndereco
                    Nothing,             ' pNumero_Sim_Card
                    Nothing,             ' pValor_Contrato
                    Nothing,             ' pPlano_Contrato
                    Nothing              ' pVelocidade
                )

                    Dim dv As New DataView(dsAtivo.Tables(0), "Id_Consumidor = " & consumidores(i), Nothing, DataViewRowState.OriginalRows)

                    If dv.Count > 0 AndAlso vDataSetXML.Tables.Contains(grupos(i)) Then
                        termosValidos += 1
                    End If
                Next

                If termosValidos = 0 Then
                    Response.StatusCode = 204 ' No Content
                    Response.End()
                Else
                    Response.StatusCode = 200
                    Response.Write("ok")
                    Response.End()
                End If
            End If

            ' Criação da lista de objetos para geração do PDF
            Dim termos As New List(Of TermoData)
            For i = 0 To ativos.Length - 1
                termos.Add(New TermoData With {
                .IdAtivo = ativos(i),
                .IdConsumidor = consumidores(i),
                .NmAtivoTipoGrupo = grupos(i)
            })
            Next

            GerarPdf(termos)
        End If
    End Sub



    Private Function App_Path() As String
        Dim sPath As String = System.AppDomain.CurrentDomain.BaseDirectory()
        Return sPath & If(Right(sPath, 1) = "/", "", "/")
    End Function


    Private Sub GerarPdf(termos As List(Of TermoData))
        Dim doc As New Document(PageSize.A4, 50, 50, 50, 50)
        Dim output As New MemoryStream()
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, output)

        doc.Open()

        Dim termosGerados As Integer = 0

        For Each termo In termos

            ' WS Ativo
            Dim dsAtivo = WS_Cadastro.Ativo(
            Session("Conn_Banco"),
            termo.IdAtivo,       ' ou ativos(i) ou idAtivo, dependendo da localização no código
            Nothing,             ' pNr_Ativo
            Nothing,             ' pFinalidade
            Nothing,             ' pId_Ativo_Tipo
            Nothing,             ' pId_Conglomerado
            Nothing,             ' pId_Ativo_Modelo
            Nothing,             ' pLocalidade
            Nothing,             ' pDt_Ativacao
            Nothing,             ' pObservacao
            Nothing,             ' pAtivo_Complemento
            Nothing,             ' pId_Ativo_Status
            Nothing,             ' pArray_Consumidor
            Nothing,             ' pId_Usuario_Permissao
            "sd_SL_Termo",       ' pPakage
            True,                ' pRetorno
            Nothing,             ' pEndereco
            Nothing,             ' pNumero_Sim_Card
            Nothing,             ' pValor_Contrato
            Nothing,             ' pPlano_Contrato
            Nothing              ' pVelocidade
        )

            Dim dv As New Data.DataView(dsAtivo.Tables(0), "Id_Consumidor = " & termo.IdConsumidor, Nothing, DataViewRowState.OriginalRows)

            If dv.Count = 0 Then Continue For

            ' XML
            If Not vDataSetXML.Tables.Contains(termo.NmAtivoTipoGrupo) Then Continue For
            Dim xmlRow = vDataSetXML.Tables(termo.NmAtivoTipoGrupo).Rows(0)

            ' Fontes
            Dim fonteNormal = FontFactory.GetFont("Arial", 11)
            Dim fonteNegrito = FontFactory.GetFont("Arial", 11, Font.BOLD)
            Dim fonteTitulo = FontFactory.GetFont("Arial", 15, Font.BOLD)
            Dim fonteSubTitulo = FontFactory.GetFont("Arial", 13)

            ' Cabeçalho com imagem
            Try
                Dim imgHeader As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Img_Sistema/Ass_Termo/k2a.png"))

                imgHeader.ScaleToFit(doc.PageSize.Width / 1.5, 30.0F) ' altura ajustável
                imgHeader.SetAbsolutePosition(50, doc.PageSize.Height - imgHeader.ScaledHeight - 40) ' posiciona no topo

                writer.DirectContent.AddImage(imgHeader)

                ' Ajusta o topo do conteúdo textual para ficar abaixo da imagem
                doc.SetMargins(doc.LeftMargin, doc.RightMargin, imgHeader.ScaledHeight + 40, doc.BottomMargin)

            Catch ex As Exception
                ' erro silencioso
            End Try


            doc.Add(New Paragraph(" ") With {.SpacingBefore = 60})

            ' Cabeçalho
            doc.Add(ParagrafoTitulo(xmlRow("Titulo").ToString(), fonteTitulo, 20, 0))
            Dim subtitulo = xmlRow("Subtitulo").ToString().Trim()
            If Not String.IsNullOrEmpty(subtitulo) Then
                Dim pSubTitulo As New Paragraph(subtitulo, fonteSubTitulo)
                pSubTitulo.Alignment = Element.ALIGN_CENTER
                pSubTitulo.SpacingBefore = 0
                pSubTitulo.SpacingAfter = 0
                doc.Add(pSubTitulo)
            End If


            ' Corpo
            doc.Add(ParagrafoFormatadoPrimeiro(xmlRow("DadosUsuario").ToString(), fonteNormal))
            doc.Add(ParagrafoFormatadoPrimeiro($"{xmlRow("Ciente")} {xmlRow("DadosAparelho")}", fonteNormal))
            doc.Add(ParagrafoFormatadoPrimeiro(xmlRow("Texto1").ToString(), fonteNormal))

            ' Nome e matrícula
            Dim nome = dv(0)("Nm_Consumidor").ToString().Trim()
            Dim texto2 = xmlRow("Texto2").ToString().Trim()
            Dim matricula = dv(0)("Matricula").ToString().Trim()
            Dim texto3 = xmlRow("Texto3").ToString().Trim()

            doc.Add(ParagrafoFormatado($"{nome} {texto2} {matricula} {texto3}", fonteNormal))

            ' Tópicos 1 a 8
            For i = 1 To 8
                Dim col = $"Texto3_{i}"
                If xmlRow.Table.Columns.Contains(col) Then
                    doc.Add(ParagrafoFormatadoEdentado(xmlRow(col).ToString().Replace("salto_Linha", vbNewLine), fonteNormal))
                End If
            Next

            ' Texto3_9 (mantemos estilo especial: negrito + sublinhado)
            If xmlRow.Table.Columns.Contains("Texto3_9") Then
                Dim pDestaque As New Paragraph(xmlRow("Texto3_9").ToString(), FontFactory.GetFont("Arial", 11, Font.BOLD Or Font.UNDERLINE))
                pDestaque.SpacingAfter = 15
                pDestaque.IndentationLeft = 22
                doc.Add(pDestaque)
            End If

            ' Descrição do aparelho
            doc.Add(ParagrafoAparelhoComLabelNegrito(xmlRow("Linha"), dv(0)("Linha"), fonteNormal, fonteNegrito))
            doc.Add(ParagrafoAparelhoComLabelNegrito(xmlRow("ChipAparelho"), dv(0)("CHIP"), fonteNormal, fonteNegrito))
            doc.Add(ParagrafoAparelhoComLabelNegrito(xmlRow("MarcaModeloAparelho"), dv(0)("Nm_Modelo_Fabricante"), fonteNormal, fonteNegrito))
            doc.Add(ParagrafoAparelhoComLabelNegrito(xmlRow("ImeiAparelho"), dv(0)("Nr_Aparelho"), fonteNormal, fonteNegrito))
            doc.Add(ParagrafoAparelhoComLabelNegrito(xmlRow("Acessorio"), dv(0)("Acessorio"), fonteNormal, fonteNegrito))



            ' Data
            doc.Add(ParagrafoFormatadoSpaceBefore($"{xmlRow("Data_1")} {dv(0)("Nm_Filial")} {Now.ToString("dd/MM/yyyy - HH:mm")}", fonteNormal))
            Dim data2Texto = xmlRow("Data_Texto_2").ToString().Trim()
            If Not String.IsNullOrEmpty(data2Texto) Then
                Dim data2Label = xmlRow("Data_2").ToString().Trim()
                Dim textoFinal As String

                If Not String.IsNullOrEmpty(data2Label) Then
                    textoFinal = $"{data2Label}: {data2Texto}"
                Else
                    textoFinal = data2Texto
                End If

                doc.Add(ParagrafoFormatadoSpaceBefore(textoFinal, fonteNormal))
            End If



            ' Assinaturas com linha
            For Each num In {"4", "5", "6"}
                Dim assCol = $"Assinatura_{num}"
                If xmlRow.Table.Columns.Contains(assCol) Then
                    Dim texto = xmlRow(assCol).ToString()
                    If Not String.IsNullOrWhiteSpace(texto) Then
                        doc.Add(ParagrafoFormatado(texto, fonteNegrito))
                        doc.Add(ParagrafoFormatado("_______________________________________________________", fonteNormal))
                    End If
                End If
            Next

            ' [INÍCIO - ICTRL-NF-202506-026]
            ' Verifica se existe um aceite registrado para este termo
            Dim dtAceite As Object = Nothing
            ' Chama o WebService para verificar o status do aceite no banco de dados
            Dim dsVerifica As DataSet = WS_Cadastro.Termo_Aceite_Validar(Session("Conn_Banco"), "verificar_aceite", termo.IdConsumidor, termo.IdAtivo, Nothing, Nothing)
            Dim hashAceite As String = String.Empty

            ' Se a consulta retornou um aceite válido, captura a data e o hash
            If dsVerifica IsNot Nothing AndAlso dsVerifica.Tables.Count > 0 AndAlso dsVerifica.Tables(0).Rows.Count > 0 Then
                dtAceite = dsVerifica.Tables(0).Rows(0)("Dt_Aceite")
                hashAceite = dsVerifica.Tables(0).Rows(0)("Hash_Acesso").ToString()
            End If

            ' Se a data do aceite foi encontrada, adiciona o parágrafo de confirmação ao PDF
            If dtAceite IsNot Nothing AndAlso Not IsDBNull(dtAceite) Then
                Dim textoAceiteXml = xmlRow("TextoAceite").ToString().Trim()
                Dim nmConsumidor = dv(0)("Nm_Consumidor").ToString().Trim()
                Dim dataFormatada = CDate(dtAceite).ToString("dd/MM/yyyy 'às' HH:mm:ss")

                Dim pAceite As New Paragraph()
                pAceite.SpacingBefore = 30
                pAceite.Alignment = Element.ALIGN_CENTER

                ' Fontes específicas para a linha de aceite, com nomes únicos
                Dim fonteAceiteNormal = FontFactory.GetFont("Arial", 9, Font.ITALIC, BaseColor.GRAY)
                Dim fonteAceiteBold = FontFactory.GetFont("Arial", 9, Font.BOLDITALIC, BaseColor.GRAY)

                pAceite.Add(New Chunk("Assinado digitalmente por ", fonteAceiteNormal))
                pAceite.Add(New Chunk(nmConsumidor, fonteAceiteBold))
                pAceite.Add(New Chunk($" em {dataFormatada} ", fonteAceiteNormal))
                pAceite.Add(New Chunk($"({hashAceite})", fonteAceiteNormal)) ' Adiciona o hash

                doc.Add(pAceite)
            End If
            ' [FIM - ICTRL-NF-202506-026]

            ' Assinaturas ao lado
            For Each num In {"3", "2", "1"}
                Dim assCol = $"Assinatura_{num}"
                If xmlRow.Table.Columns.Contains(assCol) Then
                    Dim texto = xmlRow(assCol).ToString().Trim()

                    If Not String.IsNullOrWhiteSpace(texto) Then
                        ' Remove ":" se já existir no final
                        If texto.EndsWith(":") Then
                            texto = texto.Substring(0, texto.Length - 1).Trim()
                        End If

                        ' Cria o parágrafo com partes diferentes
                        Dim p As New Paragraph()
                        p.SpacingBefore = 20
                        p.SpacingAfter = 10

                        p.Add(New Chunk($"{texto}: ", fonteNegrito))
                        p.Add(New Chunk("__________________________________________________", fonteNormal))

                        doc.Add(p)
                    End If
                End If
            Next

            ' Rodapé com imagem
            Try
                Dim imgFooter As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Img_Sistema/Ass_Termo/k2a-rodape.png"))

                imgFooter.ScaleToFit(doc.PageSize.Width, 60.0F) ' altura ajustável
                imgFooter.SetAbsolutePosition(0, 60) '

                writer.DirectContent.AddImage(imgFooter)

            Catch ex As Exception
                ' erro silencioso
            End Try



            doc.NewPage()
            termosGerados += 1
        Next

        ' 🛡️ Se nenhum termo foi gerado, exibe mensagem
        If termosGerados = 0 Then
            Response.Clear()
            Response.ContentType = "text/html"
            Response.Write("<html><body style='font-family: Arial; font-size: 14px; color: red; text-align: center; padding: 50px;'>Nenhum termo foi encontrado com os parâmetros informados.</body></html>")
            Response.End()
            Return
        End If

        doc.Close()

        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-Disposition", "inline; filename=Termo.pdf")
        Response.OutputStream.Write(output.ToArray(), 0, output.ToArray().Length)
        Response.Flush()
        Response.End()
    End Sub

    Private Function ParagrafoFormatadoPrimeiro(texto As String, fonte As Font) As Paragraph
        Dim p As New Paragraph(texto, fonte)
        p.SpacingAfter = 10
        p.SpacingBefore = -10
        Return p
    End Function

    Private Function ParagrafoFormatado(texto As String, fonte As Font) As Paragraph
        Dim p As New Paragraph(texto, fonte)
        p.SpacingAfter = 10
        Return p
    End Function

    Private Function ParagrafoFormatadoSpaceBefore(texto As String, fonte As Font) As Paragraph
        Dim p As New Paragraph(texto, fonte)
        p.SpacingBefore = 20
        p.SpacingAfter = 30
        Return p
    End Function

    Private Function ParagrafoFormatadoEdentado(texto As String, fonte As Font) As Paragraph
        Dim p As New Paragraph(texto, fonte)
        p.SpacingAfter = 10
        p.IndentationLeft = 22
        Return p
    End Function

    Private Function ParagrafoAparelhoComLabelNegrito(label As String, valor As String, fonteNormal As Font, fonteNegrito As Font) As Paragraph
        Dim p As New Paragraph()
        p.IndentationLeft = 35
        p.SpacingAfter = 0

        If Not String.IsNullOrWhiteSpace(label) Then
            p.Add(New Chunk(label.Trim() & " ", fonteNegrito))
        End If

        If Not String.IsNullOrWhiteSpace(valor) Then
            p.Add(New Chunk(valor.Trim(), fonteNormal))
        End If

        Return p
    End Function

    Private Function ParagrafoTitulo(texto As String, fonte As Font, Optional spaceBefore As Single = 0, Optional spaceAfter As Single = 0) As Paragraph
        Dim p As New Paragraph(texto, fonte)
        p.Alignment = Element.ALIGN_CENTER
        p.SpacingBefore = spaceBefore
        p.SpacingAfter = spaceAfter
        Return p
    End Function



    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Necessário para permitir renderização de controles do ASP.NET
    End Sub


    ' [INÍCIO - ICTRL-NF-202506-026]
    Private Sub EnviarLinkAceite(ByVal idAtivo As Integer, ByVal idConsumidor As Integer, ByVal nomeCliente As String)
        Dim hash As String = Guid.NewGuid().ToString("N")
        Dim pPConn_Banco As String = Session("Conn_Banco")

        ' --- Busca de Dados Unificada ---
        Dim dsDadosTermo = WS_Cadastro.Ativo(pPConn_Banco, idAtivo, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "sd_SL_Termo", True, Nothing, Nothing, Nothing, Nothing, Nothing)
        If dsDadosTermo Is Nothing OrElse dsDadosTermo.Tables.Count = 0 OrElse dsDadosTermo.Tables(0).Rows.Count = 0 Then
            Response.Write("Erro: Não foi possível obter os dados do ativo para gerar o link.")
            Response.End()
            Return
        End If

        Dim dv As New Data.DataView(dsDadosTermo.Tables(0), "Id_Consumidor = " & idConsumidor, Nothing, DataViewRowState.OriginalRows)
        If dv.Count = 0 Then
            Response.Write("Erro: A combinação de Ativo e Consumidor informada não foi encontrada.")
            Response.End()
            Return
        End If

        ' Dim emailPara As String = dv(0)("EMail").ToString()
        Dim emailPara As String = "calegarichipak@gmail.com"

        ' Validação original comentada, pois estamos usando um valor fixo.
        ' If String.IsNullOrEmpty(emailPara) Then
        '     Response.Write("Erro: O consumidor encontrado não possui um e-mail cadastrado no sistema.")
        '     Response.End()
        '     Return
        ' End If

        ' --- Lógica de Geração de Link e Agendamento de E-mail (Chamada Única) ---
        Dim baseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority)
        Dim urlAceite As String = $"{baseUrl}/Termo/Aceite.aspx?hash={hash}&c={nomeCliente}"
        Dim assunto As String = "Ação necessária: Aceite do Termo de Responsabilidade"
        Dim corpo As String = $"<p>Olá,</p><p>Por favor, acesse o link a seguir para revisar e aceitar o seu termo de responsabilidade de ativo: <a href='{urlAceite}'>{urlAceite}</a></p><p>Obrigado.</p>"
        Dim nmUsuarioAtual As String = Session("Nm_Usuario")

        Try
            Dim dsResultado As DataSet = WS_Cadastro.Termo_Aceite_AgendarComEmail(pPConn_Banco, "agendar_aceite_e_email", idConsumidor, idAtivo, hash, emailPara, assunto, corpo, nmUsuarioAtual)

            ' Verifica se o retorno foi bem-sucedido (se retornou o novo Id_Aceite)
            If dsResultado IsNot Nothing AndAlso dsResultado.Tables.Count > 0 AndAlso dsResultado.Tables(0).Rows.Count > 0 Then
                Response.Write($"<html><body>Link de aceite gerado e e-mail agendado para envio para <b>{emailPara}</b> com sucesso.</body></html>")
            Else
                Response.Write($"<html><body style='color:red;'>Erro: Não foi possível registrar a solicitação de aceite no banco de dados.</body></html>")
            End If

        Catch ex As Exception
            Response.Write($"<html><body style='color:red;'>Erro inesperado: {ex.Message}</body></html>")
        End Try

        Response.End()
    End Sub
    ' [FIM - ICTRL-NF-202506-026]

End Class

