' -----------------------------------------------------------------------
' SuccessFactors.asmx
' Autor: Anderson Luiz Chipak - Agencia ALC
' Data: 05/12/2024
' Descrição:
'   Web Service responsável por acessar as APIs do SAP SuccessFactors,
'   aplicar regras de negócio e chamar o WSSincronizacao.asmx em lote,
'   enviando XML e acionando MERGE na procedure.
' -----------------------------------------------------------------------

Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization
Imports System.Collections
Imports System.Configuration
Imports System.Globalization
Imports System.Xml
Imports System.Reflection
Imports System.Diagnostics.Eventing

Namespace Sync
    <WebService(Namespace:="https://www.icontrolit.com.br/Sync/SuccessFactors.asmx")>
    <WebServiceBinding()>
    Public Class SuccessFactors
        Inherits WebService

        Private WSSincronizacao As New WSSincronizacao()
        Private logFilePath As String = "C:\Temp\LogSuccessFactors.txt"

        Private Sub InicializaLog()
            Try
                Dim logDirectory As String = Path.GetDirectoryName(logFilePath)
                If Not Directory.Exists(logDirectory) Then
                    Directory.CreateDirectory(logDirectory)
                End If

                If Not File.Exists(logFilePath) Then
                    File.Create(logFilePath).Dispose()
                End If
            Catch ex As Exception
                Throw New Exception("Erro ao inicializar o arquivo de log: " & ex.Message)
            End Try
        End Sub

        Private Sub EscreveLog(ByVal mensagem As String)
            Try
                InicializaLog()
                Using sw As StreamWriter = New StreamWriter(logFilePath, True)
                    sw.WriteLine($"{DateTime.Now}: {mensagem}")
                End Using
            Catch ex As Exception
                Throw New Exception("Erro ao gravar log: " & ex.Message)
            End Try
        End Sub

        Private Function ObterTokenAcesso() As String
            Try
                Dim clientId As String = ConfigurationManager.AppSettings("SF_client_id")
                Dim userId As String = ConfigurationManager.AppSettings("SF_user_id")
                Dim tokenUrl As String = ConfigurationManager.AppSettings("SF_token_url")
                Dim privateKey As String = ConfigurationManager.AppSettings("SF_private_key")
                Dim idpUrl As String = ConfigurationManager.AppSettings("SF_idp_url")
                Dim apiKey As String = ConfigurationManager.AppSettings("SF_api_key")
                Dim companyId As String = ConfigurationManager.AppSettings("SF_company_id")


                If String.IsNullOrEmpty(clientId) OrElse String.IsNullOrEmpty(userId) OrElse String.IsNullOrEmpty(tokenUrl) OrElse String.IsNullOrEmpty(privateKey) Then
                    EscreveLog("Configurações para token no web.config estão incompletas.")
                    Throw New Exception("Configurações para token estão incompletas.")
                End If

                ' 1) Obter a assertion do IDP
                Dim postDataIdp As String = "client_id=" & Uri.EscapeDataString(clientId) &
                                            "&user_id=" & Uri.EscapeDataString(userId) &
                                            "&token_url=" & Uri.EscapeDataString(tokenUrl) &
                                            "&private_key=" & Uri.EscapeDataString(privateKey)

                Dim idpRequest As HttpWebRequest = CType(WebRequest.Create(idpUrl), HttpWebRequest)
                idpRequest.Method = "POST"
                idpRequest.Headers.Add("APIKey", apiKey)
                idpRequest.ContentType = "application/x-www-form-urlencoded"
                Dim idpData As Byte() = Encoding.UTF8.GetBytes(postDataIdp)
                idpRequest.ContentLength = idpData.Length
                Using stream As Stream = idpRequest.GetRequestStream()
                    stream.Write(idpData, 0, idpData.Length)
                End Using

                Dim assertion As String = ""
                Using idpResponse As HttpWebResponse = CType(idpRequest.GetResponse(), HttpWebResponse)
                    Using reader As New StreamReader(idpResponse.GetResponseStream(), Encoding.UTF8)
                        assertion = reader.ReadToEnd().Trim()
                    End Using
                End Using

                If String.IsNullOrEmpty(assertion) Then
                    EscreveLog("Não foi possível obter a assertion do IDP.")
                    Throw New Exception("Assertion vazia do IDP.")
                End If

                ' 2) Obter o access_token usando a assertion
                Dim oauthUrl As String = tokenUrl
                Dim grantType As String = "urn:ietf:params:oauth:grant-type:saml2-bearer"

                Dim postDataToken As String = "client_id=" & Uri.EscapeDataString(clientId) &
                                              "&grant_type=" & Uri.EscapeDataString(grantType) &
                                              "&company_id=" & Uri.EscapeDataString(companyId) &
                                              "&assertion=" & Uri.EscapeDataString(assertion)

                Dim tokenRequest As HttpWebRequest = CType(WebRequest.Create(oauthUrl), HttpWebRequest)
                tokenRequest.Method = "POST"
                tokenRequest.Headers.Add("APIKey", apiKey)
                tokenRequest.ContentType = "application/x-www-form-urlencoded"
                Dim tokenData As Byte() = Encoding.UTF8.GetBytes(postDataToken)
                tokenRequest.ContentLength = tokenData.Length
                Using stream As Stream = tokenRequest.GetRequestStream()
                    stream.Write(tokenData, 0, tokenData.Length)
                End Using

                Dim accessToken As String = ""
                EscreveLog("1")

                Using tokenResponse As HttpWebResponse = CType(tokenRequest.GetResponse(), HttpWebResponse)
                    Using reader As New StreamReader(tokenResponse.GetResponseStream(), Encoding.UTF8)
                        Dim jsonResp As String = reader.ReadToEnd()
                        Dim serializer As New JavaScriptSerializer()
                        Dim jsonDict As Dictionary(Of String, Object) = serializer.Deserialize(Of Dictionary(Of String, Object))(jsonResp)
                        If jsonDict.ContainsKey("access_token") Then
                            accessToken = jsonDict("access_token").ToString()
                        End If
                    End Using
                End Using

                If String.IsNullOrEmpty(accessToken) Then
                    EscreveLog("Não foi possível obter o access_token.")
                    Throw New Exception("Access_token não retornado.")
                End If

                Return accessToken

            Catch ex As Exception
                EscreveLog("Erro ao obter token de acesso: " & ex.Message)
                Throw
            End Try
        End Function

        Public Function EncodeXml(value As String) As String
            If String.IsNullOrWhiteSpace(value) Then
                Return value
            End If
            Return value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("""", "&quot;").Replace("'", "&apos;")
        End Function


        Private Function ChamarApiGETRaw(ByVal urlApi As String, ByVal token As String) As String
            Dim request As HttpWebRequest = CType(WebRequest.Create(urlApi), HttpWebRequest)
            Dim apiKey As String = ConfigurationManager.AppSettings("SF_api_key")
            request.Method = "GET"
            request.Accept = "application/json"
            request.Headers("Authorization") = "Bearer " & token
            request.Headers.Add("APIKey", apiKey)

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                    Return reader.ReadToEnd()
                End Using
            End Using
        End Function

        Private Function ExtrairResultados(jsonDict As Dictionary(Of String, Object)) As Object()
            Dim results As Object() = {}
            If jsonDict.ContainsKey("d") Then
                Dim data As Dictionary(Of String, Object) = CType(jsonDict("d"), Dictionary(Of String, Object))
                If data.ContainsKey("results") Then
                    Dim resultsList As ArrayList = CType(data("results"), ArrayList)
                    results = resultsList.ToArray()
                End If
            End If
            Return results
        End Function

        ' Você pode adaptar para cada Entidade (FOCompany, FOJobCode etc),
        ' mas aqui fica genérico para qualquer uma (entidadeNome).
        Private Function SincronizarEntidadePaginado(entidadeNome As String,
                                             urlApi As String,
                                             gerarXml As Func(Of Object(), String),
                                             sincronizarEmLote As Func(Of String, String)) As String
            Try
                EscreveLog("Iniciando Sincronizar" & entidadeNome)

                ' 1) Obter Token
                Dim token As String = ObterTokenAcesso()

                ' 2) Pegar TODOS os registros de todas as páginas
                Dim todosRegistros As New List(Of Dictionary(Of String, Object))()

                ' Padrão: se não tiver "?", adiciona ?$format=json, senão adiciona &format=json
                If urlApi.Contains("?") Then
                    urlApi &= "&$format=json"
                Else
                    urlApi &= "?$format=json"
                End If

                Dim proximaUrl As String = urlApi

                ' Enquanto existir proximaUrl, chama ChamarApiGETRaw
                Dim serializer As New JavaScriptSerializer()
                serializer.MaxJsonLength = Integer.MaxValue

                Do While Not String.IsNullOrEmpty(proximaUrl)
                    EscreveLog($"Chamada da URL: {proximaUrl}")

                    Dim responseString As String = ChamarApiGETRaw(proximaUrl, token)
                    EscreveLog("Resposta da API obtida com sucesso.")

                    ' Desserializa
                    Dim jsonDict As Dictionary(Of String, Object) = serializer.Deserialize(Of Dictionary(Of String, Object))(responseString)

                    ' Extrair 'results'
                    Dim resultsArray As Object() = ExtrairResultados(jsonDict)
                    EscreveLog($"Página com {resultsArray.Length} registros.")

                    ' Salva no todosRegistros
                    For Each itemObj In resultsArray
                        ' Converte itemObj (que é Object) para Dictionary(Of String, Object)
                        Dim item As Dictionary(Of String, Object) = TryCast(itemObj, Dictionary(Of String, Object))
                        If item IsNot Nothing Then
                            todosRegistros.Add(item)
                        End If
                    Next

                    ' Tenta extrair a __next, se existir
                    ' Ex.: jsonDict("d").__next
                    Dim dObj As Dictionary(Of String, Object) = Nothing
                    proximaUrl = Nothing ' assume que não tem próxima por padrão

                    If jsonDict.ContainsKey("d") Then
                        dObj = TryCast(jsonDict("d"), Dictionary(Of String, Object))
                        If dObj IsNot Nothing AndAlso dObj.ContainsKey("__next") Then
                            Dim prox As String = dObj("__next").ToString()
                            If Not String.IsNullOrEmpty(prox) Then
                                proximaUrl = prox
                            End If
                        End If
                    End If

                    EscreveLog(If(String.IsNullOrEmpty(proximaUrl),
                          "Não há próxima página (__next). Finalizando coleta.",
                          $"__next detectado. Próxima página: {proximaUrl}"))
                Loop

                ' Agora que todos foram coletados, gera o XML (uma vez só)
                EscreveLog($"Total de registros coletados em todas as páginas: {todosRegistros.Count}.")

                Dim xml As String = gerarXml(todosRegistros.ToArray())
                EscreveLog("XML gerado com sucesso: " & xml)

                ' Sincronizar em lote
                If sincronizarEmLote IsNot Nothing Then
                    Dim resp As String = sincronizarEmLote(xml)
                    EscreveLog("Sincronizar" & entidadeNome & "EmLote retorno: " & resp)
                End If

                Return "Sincronização " & entidadeNome & " concluída com sucesso. XML: " & xml

            Catch ex As Exception
                EscreveLog("Erro em Sincronizar" & entidadeNome & ": " & ex.Message)
                Throw
            End Try
        End Function


        ' Função genérica para sincronizar qualquer entidade
        Private Function SincronizarEntidade(entidadeNome As String,
                                             urlApi As String,
                                             gerarXml As Func(Of Object(), String),
                                             sincronizarEmLote As Func(Of String, String)) As String
            Try
                EscreveLog("Iniciando Sincronizar" & entidadeNome)

                Dim token As String = ObterTokenAcesso()

                ' Adicionar $format=json se não estiver presente
                If urlApi.Contains("?") Then
                    urlApi &= "&$format=json"
                Else
                    urlApi &= "?$format=json"
                End If

                Dim responseString As String = ChamarApiGETRaw(urlApi, token)
                EscreveLog("Resposta da API obtida com sucesso.")

                Dim serializer As New JavaScriptSerializer()
                serializer.MaxJsonLength = Integer.MaxValue
                Dim jsonDict As Dictionary(Of String, Object) = serializer.Deserialize(Of Dictionary(Of String, Object))(responseString)

                Dim results As Object() = ExtrairResultados(jsonDict)
                EscreveLog("Resultados extraídos com sucesso.")

                Dim xml As String = gerarXml(results)
                EscreveLog("XML gerado com sucesso: " & xml)

                If sincronizarEmLote IsNot Nothing Then
                    ' Dim resp As String = sincronizarEmLote(SanitizeXmlString(xml))
                    Dim resp As String = sincronizarEmLote(xml)
                    EscreveLog("Sincronizar" & entidadeNome & "EmLote retorno: " & resp)
                End If

                Return "Sincronização " & entidadeNome & " concluída com sucesso. XML: " & xml
            Catch ex As Exception
                EscreveLog("Erro em Sincronizar" & entidadeNome & ": " & ex.Message)
                Throw
            End Try
        End Function

        Private Function SafeGetValue(ByVal item As Dictionary(Of String, Object),
                              ByVal key As String,
                              ByVal defaultValue As String) As String
            If item Is Nothing Then
                Return defaultValue
            End If
            If Not item.ContainsKey(key) OrElse item(key) Is Nothing Then
                Return defaultValue
            End If
            Return item(key).ToString()
        End Function


        ' Funções auxiliares para gerar XML
        Private Function GerarXmlCargos(ByVal results As Object()) As String
            Dim sb As New StringBuilder()
            sb.AppendLine("<Root>")

            For Each obj As Object In results
                Dim item = TryCast(obj, Dictionary(Of String, Object))
                If item Is Nothing Then
                    Continue For
                End If

                ' Use SafeGetValue para cada campo
                Dim code As String = EncodeXml(SafeGetValue(item, "externalCode", "SEM_CODE"))
                Dim status As String = SafeGetValue(item, "status", "A")
                Dim description As String = EncodeXml(SafeGetValue(item, "name_defaultValue", "SEM_DESCRICAO"))
                Dim flDesativado As Integer = If(status = "A", 0, 1)

                sb.AppendLine("  <Cargo>")
                sb.AppendLine($"    <Cd_Cargo>{code}</Cd_Cargo>")
                sb.AppendLine($"    <Nm_Cargo>{description}</Nm_Cargo>")
                sb.AppendLine($"    <Fl_Desativado>{flDesativado}</Fl_Desativado>")
                sb.AppendLine("  </Cargo>")
            Next

            sb.AppendLine("</Root>")
            Return sb.ToString()
        End Function


        Private Function GerarXmlEmpresas(ByVal results As Object()) As String
            Dim sb As New StringBuilder()
            sb.AppendLine("<Root>")

            For Each obj As Object In results
                Dim item = TryCast(obj, Dictionary(Of String, Object))
                If item Is Nothing Then
                    Continue For
                End If

                Dim companyCode As String = EncodeXml(SafeGetValue(item, "externalCode", "SEM_CODE"))
                Dim status As String = SafeGetValue(item, "status", "A")
                Dim description As String = EncodeXml(SafeGetValue(item, "name", "SEM_NOME"))
                Dim flDesativado As Integer = If(status = "A", 0, 1)

                sb.AppendLine("  <Empresa>")
                sb.AppendLine($"    <Cd_Empresa>{companyCode}</Cd_Empresa>")
                sb.AppendLine($"    <Id_Holding>1</Id_Holding>")
                sb.AppendLine($"    <Nm_Empresa>{description}</Nm_Empresa>")
                sb.AppendLine($"    <CNPJ>{companyCode}</CNPJ>")
                sb.AppendLine($"    <Fl_Desativado>{flDesativado}</Fl_Desativado>")
                sb.AppendLine("  </Empresa>")
            Next

            sb.AppendLine("</Root>")
            Return sb.ToString()
        End Function


        Private Function GerarXmlDepartamentos(ByVal results As Object()) As String
            Dim sb As New StringBuilder()
            sb.AppendLine("<Root>")

            For Each obj As Object In results
                Dim item = TryCast(obj, Dictionary(Of String, Object))
                If item Is Nothing Then
                    Continue For
                End If

                Dim departmentCode As String = EncodeXml(SafeGetValue(item, "externalCode", "SEM_CODE"))
                Dim status As String = SafeGetValue(item, "status", "A")
                Dim departmentName As String = EncodeXml(SafeGetValue(item, "name_defaultValue", "SEM_NOME"))
                Dim flDesativado As Integer = If(status = "A", 0, 1)

                sb.AppendLine("  <Departamento>")
                sb.AppendLine($"    <Cd_Departamento>{departmentCode}</Cd_Departamento>")
                sb.AppendLine($"    <Nm_Departamento>{departmentName}</Nm_Departamento>")
                sb.AppendLine($"    <Fl_Desativado>{flDesativado}</Fl_Desativado>")
                sb.AppendLine("  </Departamento>")
            Next

            sb.AppendLine("</Root>")
            Return sb.ToString()
        End Function


        Private Function GerarXmlFiliais(ByVal results As Object()) As String
            Dim sb As New StringBuilder()
            sb.AppendLine("<Root>")

            For Each obj As Object In results
                Dim item = TryCast(obj, Dictionary(Of String, Object))
                If item Is Nothing Then
                    Continue For
                End If

                Dim locationCode As String = EncodeXml(SafeGetValue(item, "externalCode", "SEM_CODE"))
                Dim locationName As String = EncodeXml(SafeGetValue(item, "name", "SEM_NOME"))
                Dim streetAddress As String = EncodeXml(SafeGetValue(item, "customString5", ""))

                sb.AppendLine("  <Filial>")
                sb.AppendLine($"    <Cd_Filial>{locationCode}</Cd_Filial>")
                sb.AppendLine($"    <Nm_Filial>{locationName}</Nm_Filial>")
                sb.AppendLine($"    <CNPJ>{locationCode}</CNPJ>")
                sb.AppendLine($"    <Endereco>{streetAddress}</Endereco>")
                sb.AppendLine($"    <Fl_Desativado>0</Fl_Desativado>")
                sb.AppendLine("  </Filial>")
            Next

            sb.AppendLine("</Root>")
            Return sb.ToString()
        End Function


        Private Function GerarXmlSetores(ByVal results As Object()) As String
            Dim sb As New StringBuilder()
            sb.AppendLine("<Root>")

            For Each obj As Object In results
                Dim item = TryCast(obj, Dictionary(Of String, Object))
                If item Is Nothing Then
                    Continue For
                End If

                Dim level6Description As String = EncodeXml(SafeGetValue(item, "level6Description", "SEM_DESCRICAO"))
                Dim desc1 As String = EncodeXml(SafeGetValue(item, "level1Description", "SEM_COD"))

                sb.AppendLine("  <Setor>")
                sb.AppendLine($"    <Id_Setor>1</Id_Setor>")
                sb.AppendLine($"    <Cd_Setor>{desc1}</Cd_Setor>")
                sb.AppendLine($"    <Nm_Setor>{level6Description}</Nm_Setor>")
                sb.AppendLine($"    <Fl_Desativado>0</Fl_Desativado>")
                sb.AppendLine("  </Setor>")
            Next

            sb.AppendLine("</Root>")
            Return sb.ToString()
        End Function


        ' Gera XML para consumidores a partir de dados extraídos do CompoundEmployee (ajuste conforme sua necessidade)
        Private Function GerarXmlConsumidores(consumersData As List(Of Dictionary(Of String, Object))) As String
            Dim sb As New StringBuilder()
            sb.AppendLine("<Root>")
            For Each item In consumersData
                ' Função auxiliar para acessar valores de forma segura
                Dim GetValue = Function(key As String, defaultValue As String) As String
                                   If item.ContainsKey(key) AndAlso item(key) IsNot Nothing Then
                                       Return item(key).ToString()
                                   End If
                                   Return defaultValue
                               End Function



                ' **Novo: Logar todas as chaves e seus valores**
                Dim registroDetalhado As New StringBuilder()
                registroDetalhado.Append("REGISTRO DETALHADO: ")
                For Each key As String In item.Keys
                    registroDetalhado.Append($"{key}={item(key).ToString()}, ")
                Next
                ' Remover a última vírgula e espaço
                If registroDetalhado.Length > 0 Then
                    registroDetalhado.Length -= 2
                End If
                ' EscreveLog(registroDetalhado.ToString())



                Dim employeeIdentifier As String = EncodeXml(GetValue("person_id_external", "PENDENTE"))
                Dim nmConsumidor As String = EncodeXml(GetValue("formal_name", "PENDENTE"))
                Dim matricula As String = EncodeXml(GetValue("person_id_external", "PENDENTE"))
                Dim email As String = EncodeXml(GetValue("email_address", "sem-email@dominio.com"))
                Dim statusConsumidor As String = EncodeXml(GetValue("logon_user_is_active", "false")) ' true = ativo; false = inativo
                Dim Cd_Filial As String = EncodeXml(GetValue("company", "PENDENTE"))
                Dim nmCidade As String = EncodeXml(GetValue("custom_string4", "PENDENTE"))
                Dim nmEstado As String = EncodeXml(GetValue("custom_string3", "PENDENTE"))
                Dim Cd_Centro_Custo As String = EncodeXml(GetValue("cost_center", "PENDENTE"))
                Dim matriculaChefia As String = EncodeXml(GetValue("manager_id", "PENDENTE"))
                Dim Cd_Departamento As String = EncodeXml(GetValue("department", "PENDENTE"))
                Dim Cd_Setor As String = "PENDENTE"
                Dim flDesativado As Integer = If(statusConsumidor = "false", 1, 0)
                Dim idConsumidorStatus As Integer = If(statusConsumidor = "true", 1, 2)

                Dim dtDesativacao As String = ""
                Dim end_date As String = GetValue("end_date", "")

                If Not String.IsNullOrWhiteSpace(end_date) Then
                    Try
                        Dim parsedDate As Date = Date.ParseExact(end_date, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        dtDesativacao = parsedDate.ToString("yyyy-MM-ddTHH:mm:ss")
                    Catch ex As Exception
                        EscreveLog($"Erro ao converter end_date: {end_date}. Exceção: {ex.Message}")
                    End Try
                End If

                Dim dtAtivacao As String = ""
                Dim hireDate As String = GetValue("hireDate", "")

                If Not String.IsNullOrWhiteSpace(hireDate) Then
                    Try
                        Dim parsedDate As Date = Date.ParseExact(hireDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        dtAtivacao = parsedDate.ToString("yyyy-MM-ddTHH:mm:ss")
                    Catch ex As Exception
                        EscreveLog($"Erro ao converter hireDate: {hireDate}. Exceção: {ex.Message}")
                    End Try
                End If

                sb.AppendLine("  <Consumidor>")
                sb.AppendLine($"    <Cd_Consumidor>{employeeIdentifier}</Cd_Consumidor>")
                sb.AppendLine($"    <Nm_Consumidor>{nmConsumidor}</Nm_Consumidor>")
                sb.AppendLine($"    <Matricula>{matricula}</Matricula>")
                sb.AppendLine($"    <EMail>{email}</EMail>")
                sb.AppendLine($"    <Id_Consumidor_Status>{idConsumidorStatus}</Id_Consumidor_Status>")
                sb.AppendLine($"    <Matricula_Chefia>{matriculaChefia}</Matricula_Chefia>")
                sb.AppendLine($"    <Fl_Desativado>{flDesativado}</Fl_Desativado>")
                sb.AppendLine($"    <Nm_Cidade>{nmCidade}</Nm_Cidade>")
                sb.AppendLine($"    <Nm_Estado>{nmEstado}</Nm_Estado>")
                sb.AppendLine($"    <Dt_Ativacao_Consumidor>{dtAtivacao}</Dt_Ativacao_Consumidor>")
                sb.AppendLine($"    <Dt_Desativacao_Consumidor>{dtDesativacao}</Dt_Desativacao_Consumidor>")
                sb.AppendLine($"    <Cd_Centro_Custo>{Cd_Centro_Custo}</Cd_Centro_Custo>")
                sb.AppendLine($"    <Cd_Departamento>{Cd_Departamento}</Cd_Departamento>")
                sb.AppendLine($"    <Cd_Filial>{Cd_Filial}</Cd_Filial>")
                sb.AppendLine("  </Consumidor>")
            Next
            sb.AppendLine("</Root>")
            Return sb.ToString()
        End Function




        ' Métodos Web simplificados com SincronizarEntidade:

        ' 1 ######## MERGE_EMPRESA
        <WebMethod()>
        Public Function SincronizarFOCompany_1(pPConn_Banco As String) As String
            Dim apiUrl As String = ConfigurationManager.AppSettings("SF_url_api_FOCompany")
            Return SincronizarEntidadePaginado("FOCompany",
                                       apiUrl,
                                       AddressOf GerarXmlEmpresas,
                                       Function(xml) WSSincronizacao.SincronizarEmpresasEmLote(xml, pPConn_Banco))
        End Function

        ' 2 ######## MERGE_CARGO
        <WebMethod()>
        Public Function SincronizarFOJobCode_2(pPConn_Banco As String) As String
            Dim apiUrl As String = ConfigurationManager.AppSettings("SF_url_api_FOJobCode")
            Return SincronizarEntidadePaginado("FOJobCode",
                                       apiUrl,
                                       AddressOf GerarXmlCargos,
                                       Function(xml) WSSincronizacao.SincronizarCargosEmLote(xml, pPConn_Banco))

        End Function

        ' 3 ######## MERGE_DEPARTAMENTO
        <WebMethod()>
        Public Function SincronizarFODepartment_3(pPConn_Banco As String) As String
            Dim apiUrl As String = ConfigurationManager.AppSettings("SF_url_api_FODepartment")
            Return SincronizarEntidadePaginado("FODepartment",
                                       apiUrl,
                                       AddressOf GerarXmlDepartamentos,
                                       Function(xml) WSSincronizacao.SincronizarDepartamentosEmLote(xml, pPConn_Banco))
        End Function


        ' 4 ######## MERGE_FILIAL
        <WebMethod()>
        Public Function SincronizarFOLocation_4(pPConn_Banco As String) As String
            Dim apiUrl As String = ConfigurationManager.AppSettings("SF_url_api_FOLocation")
            Return SincronizarEntidadePaginado("FOLocation",
                                       apiUrl,
                                       AddressOf GerarXmlFiliais,
                                       Function(xml) WSSincronizacao.SincronizarFiliaisEmLote(xml, pPConn_Banco))
        End Function

        '<WebMethod()>
        'Public Function SincronizarAPIHierarquia_5(pPConn_Banco As String) As String
        'Dim apiUrl As String = ConfigurationManager.AppSettings("SF_url_api_XXXXXXXXXXXX")
        'Return SincronizarEntidade("APIHierarquia",
        '                               apiUrl,
        '                               AddressOf GerarXmlSetores,
        'Function(xml) WSSincronizacao.SincronizarSetoresEmLote(xml, pPConn_Banco))
        'End Function




        Private Function SfapiLogin(ByVal accessToken As String) As String
            ' Faz a chamada SOAP de login usando Bearer Token
            Dim loginUrl As String = ConfigurationManager.AppSettings("SF_url_api_login")
            Dim apiKey As String = ConfigurationManager.AppSettings("SF_api_key")

            Dim soapBody As String = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:urn='urn:sfobject.sfapi.successfactors.com'>" &
                                     "<soapenv:Header/><soapenv:Body>" &
                                     "<urn:login><urn:credential>" &
                                     "<urn:companyId></urn:companyId>" &
                                     "<urn:username></urn:username>" &
                                     "<urn:password></urn:password>" &
                                     "</urn:credential></urn:login>" &
                                     "</soapenv:Body></soapenv:Envelope>"

            Dim request As HttpWebRequest = CType(WebRequest.Create(loginUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "text/xml; charset=UTF-8"
            request.Headers("Authorization") = "Bearer " & accessToken
            request.Headers.Add("APIKey", apiKey)

            Dim bytes = Encoding.UTF8.GetBytes(soapBody)
            request.ContentLength = bytes.Length
            Using stream = request.GetRequestStream()
                stream.Write(bytes, 0, bytes.Length)
            End Using

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                    Dim soapResponse = reader.ReadToEnd()

                    ' Parsear o XML para extrair sessionId
                    Dim xmlDoc As New XmlDocument()
                    xmlDoc.LoadXml(soapResponse)
                    Dim nsMgr As New XmlNamespaceManager(xmlDoc.NameTable)
                    nsMgr.AddNamespace("sf", "urn:sfobject.sfapi.successfactors.com")

                    Dim sessionIdNode As XmlNode = xmlDoc.SelectSingleNode("//sf:loginResponse/sf:result/sf:sessionId", nsMgr)
                    If sessionIdNode IsNot Nothing Then
                        Return sessionIdNode.InnerText
                    Else
                        EscreveLog("Não foi possível obter sessionId do login SOAP.")
                        Throw New Exception("SessionId não encontrado no retorno do login SOAP.")
                    End If
                End Using
            End Using
        End Function

        Private Function SfapiQuery(ByVal sessionId As String) As String
            Dim queryUrl As String = ConfigurationManager.AppSettings("SF_url_api_login")
            Dim apiKey As String = ConfigurationManager.AppSettings("SF_api_key")
            Dim c_filtro_company As String = ConfigurationManager.AppSettings("C_Filtro_Company")
            Dim soapBody As String =
                "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:urn='urn:sfobject.sfapi.successfactors.com'>" &
                "<soapenv:Header/><soapenv:Body>" &
                "<urn:query>" &
                "<urn:queryString>" &
                "SELECT person, personal_information, email_information, national_id_card, phone_information, employment_information, personal_documents_information, job_information FROM CompoundEmployee WHERE COMPANY_TERRITORY_CODE = '" & c_filtro_company & "'" &
                "</urn:queryString>" &
                "<urn:param><urn:name>maxRows</urn:name><urn:value>800</urn:value></urn:param>" &
                "</urn:query>" &
                "</soapenv:Body></soapenv:Envelope>"

            Dim request As HttpWebRequest = CType(WebRequest.Create(queryUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "text/xml; charset=UTF-8"
            request.Headers("SOAPAction") = "#query"
            ' Basic Auth: 
            Dim authInfo As String = Convert.ToBase64String(Encoding.UTF8.GetBytes("SFAPI@valeD:Welcome123"))
            request.Headers("Authorization") = "Basic " & authInfo
            ' Cookie com sessionId
            request.Headers("cookie") = "JSESSIONID=" & sessionId
            request.Headers.Add("APIKey", apiKey)


            Dim bytes = Encoding.UTF8.GetBytes(soapBody)
            request.ContentLength = bytes.Length
            Using stream = request.GetRequestStream()
                stream.Write(bytes, 0, bytes.Length)
            End Using

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                    Return reader.ReadToEnd()
                End Using
            End Using
        End Function

        Private Function SfapiQueryMore(sessionId As String, querySessionId As String) As String
            Try
                Dim queryUrl As String = ConfigurationManager.AppSettings("SF_url_api_login")
                Dim apiKey As String = ConfigurationManager.AppSettings("SF_api_key")

                Dim soapBody As String =
            "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:urn='urn:sfobject.sfapi.successfactors.com'>" &
            "<soapenv:Header/><soapenv:Body>" &
            "<urn:queryMore>" &
            $"<urn:querySessionId>{querySessionId}</urn:querySessionId>" &
            "</urn:queryMore>" &
            "</soapenv:Body></soapenv:Envelope>"

                Dim request As HttpWebRequest = CType(WebRequest.Create(queryUrl), HttpWebRequest)
                request.Method = "POST"
                request.ContentType = "text/xml; charset=UTF-8"
                request.Headers("SOAPAction") = "#queryMore"

                ' Autenticação e Cookie
                Dim authInfo As String = Convert.ToBase64String(Encoding.UTF8.GetBytes("SFAPI@valeD:Welcome123"))
                request.Headers("Authorization") = "Basic " & authInfo
                request.Headers("cookie") = "JSESSIONID=" & sessionId
                request.Headers.Add("APIKey", apiKey)

                Dim bytes = Encoding.UTF8.GetBytes(soapBody)
                request.ContentLength = bytes.Length
                Using stream = request.GetRequestStream()
                    stream.Write(bytes, 0, bytes.Length)
                End Using

                Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                    Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                        Return reader.ReadToEnd()
                    End Using
                End Using
            Catch ex As Exception
                EscreveLog($"Erro em SfapiQueryMore: {ex.Message}")
                Throw
            End Try
        End Function

        ' Retorna a lista de todos os registros do CompoundEmployee
        Private Function SfapiQueryAll(sessionId As String) As List(Of Dictionary(Of String, Object))
            Dim todosRegistros As New List(Of Dictionary(Of String, Object))()

            ' 1) Chama SfapiQuery (primeira página)
            Dim primeiraResposta As String = SfapiQuery(sessionId)

            todosRegistros.AddRange(ParseCompoundEmployeeXml(primeiraResposta))

            ' 2) Se hasMore=true, chama queryMore
            Dim hasMore As Boolean = ExtrairHasMore(primeiraResposta)
            Dim querySessionId As String = ExtrairQuerySessionId(primeiraResposta)

            While hasMore

                Dim proximaResposta As String = SfapiQueryMore(sessionId, querySessionId)

                todosRegistros.AddRange(ParseCompoundEmployeeXml(proximaResposta))

                hasMore = ExtrairHasMore(proximaResposta)
                querySessionId = ExtrairQuerySessionId(proximaResposta)

            End While

            Return todosRegistros
        End Function


        Private Function ExtrairHasMore(xml As String) As Boolean
            Dim doc As New XmlDocument()
            doc.LoadXml(xml)

            Dim nsMgr As New XmlNamespaceManager(doc.NameTable)
            nsMgr.AddNamespace("sf", "urn:sfobject.sfapi.successfactors.com")

            ' Tenta no queryResponse
            Dim node As XmlNode = doc.SelectSingleNode("//sf:queryResponse/sf:result/sf:hasMore", nsMgr)
            If node Is Nothing Then
                ' Se não encontrou nada, tenta no queryMoreResponse
                node = doc.SelectSingleNode("//sf:queryMoreResponse/sf:result/sf:hasMore", nsMgr)
            End If

            Return (node IsNot Nothing AndAlso node.InnerText = "true")
        End Function

        Private Function ExtrairQuerySessionId(xml As String) As String
            Dim doc As New XmlDocument()
            doc.LoadXml(xml)

            Dim nsMgr As New XmlNamespaceManager(doc.NameTable)
            nsMgr.AddNamespace("sf", "urn:sfobject.sfapi.successfactors.com")

            ' Tenta no queryResponse
            Dim node As XmlNode = doc.SelectSingleNode("//sf:queryResponse/sf:result/sf:querySessionId", nsMgr)
            If node Is Nothing Then
                ' Se não encontrou nada, tenta no queryMoreResponse
                node = doc.SelectSingleNode("//sf:queryMoreResponse/sf:result/sf:querySessionId", nsMgr)
            End If

            If node IsNot Nothing Then
                Return node.InnerText
            End If
            Return ""
        End Function






        ' Parse da resposta da query CompoundEmployee (XML)
        Private Function ParseCompoundEmployeeXml(soapResponse As String) As List(Of Dictionary(Of String, Object))
            Dim consumers As New List(Of Dictionary(Of String, Object))()

            ' Logar o conteúdo recebido para diagnóstico
            EscreveLog($"Iniciando ParseCompoundEmployeeXml. Comprimento da resposta: {soapResponse.Length}")

            ' Verificar se a resposta está vazia
            If String.IsNullOrWhiteSpace(soapResponse) Then
                EscreveLog("Erro: A resposta SOAP está vazia.")
                Return consumers
            End If

            Try
                Dim xmlDoc As New XmlDocument()
                xmlDoc.LoadXml(soapResponse)

                ' Definir o Namespace Manager com os namespaces corretos
                Dim nsMgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsMgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/")
                nsMgr.AddNamespace("sf", "urn:sfobject.sfapi.successfactors.com")

                ' Selecionar todos os nós <sfobject> dentro de <queryResponse>/<result>
                Dim sfobjectNodes = xmlDoc.SelectNodes("//sf:queryResponse/sf:result/sf:sfobject", nsMgr)
                If sfobjectNodes.Count = 0 Then
                    ' Se não achar, tenta no queryMoreResponse
                    sfobjectNodes = xmlDoc.SelectNodes("//sf:queryMoreResponse/sf:result/sf:sfobject", nsMgr)

                    If sfobjectNodes.Count = 0 Then
                        EscreveLog("*********** XML Retornado (caso zero) ***********")
                        EscreveLog(soapResponse)
                        EscreveLog("*********** Fim do XML Retornado ***********")
                    End If

                End If

                EscreveLog($"Número de <sfobject> encontrados: {sfobjectNodes.Count}")

                For Each sfobjNode As XmlNode In sfobjectNodes
                    Dim empData As New Dictionary(Of String, Object)()

                    ' 1. Extrair <person_id_external>
                    Dim personIdExternalNode = sfobjNode.SelectSingleNode("sf:person/sf:person_id_external", nsMgr)
                    If personIdExternalNode IsNot Nothing Then
                        empData("person_id_external") = personIdExternalNode.InnerText
                    Else
                        empData("person_id_external") = "PENDENTE"
                    End If

                    ' 2. Extrair <formal_name>
                    Dim formalNameNode = sfobjNode.SelectSingleNode("sf:person/sf:personal_information/sf:formal_name", nsMgr)
                    If formalNameNode IsNot Nothing Then
                        empData("formal_name") = formalNameNode.InnerText
                    Else
                        empData("formal_name") = "PENDENTE"
                    End If

                    ' 3. Extrair <email_address>
                    Dim emailAddressNode = sfobjNode.SelectSingleNode("sf:person/sf:email_information/sf:email_address", nsMgr)
                    If emailAddressNode IsNot Nothing Then
                        empData("email_address") = emailAddressNode.InnerText
                    Else
                        empData("email_address") = "sem-email@dominio.com"
                    End If

                    ' 4. Extrair <logon_user_is_active>
                    Dim logonUserIsActiveNode = sfobjNode.SelectSingleNode("sf:person/sf:logon_user_is_active", nsMgr)
                    If logonUserIsActiveNode IsNot Nothing Then
                        empData("logon_user_is_active") = logonUserIsActiveNode.InnerText
                    Else
                        empData("logon_user_is_active") = "false"
                    End If

                    ' 5. Extrair <company>
                    Dim companyNode = sfobjNode.SelectSingleNode("sf:person/sf:employment_information/sf:job_information/sf:company", nsMgr)
                    If companyNode IsNot Nothing Then
                        empData("company") = companyNode.InnerText
                    Else
                        empData("company") = "PENDENTE"
                    End If

                    ' 6. Extrair <custom_string4>
                    Dim customString4Node = sfobjNode.SelectSingleNode("sf:person/sf:personal_information/sf:custom_string4", nsMgr)
                    If customString4Node IsNot Nothing Then
                        empData("custom_string4") = customString4Node.InnerText
                    Else
                        empData("custom_string4") = "PENDENTE"
                    End If

                    ' 7. Extrair <custom_string3>
                    Dim customString3Node = sfobjNode.SelectSingleNode("sf:person/sf:personal_information/sf:custom_string3", nsMgr)
                    If customString3Node IsNot Nothing Then
                        empData("custom_string3") = customString3Node.InnerText
                    Else
                        empData("custom_string3") = "PENDENTE"
                    End If

                    ' 8. Extrair <cost_center>
                    Dim costCenterNode = sfobjNode.SelectSingleNode("sf:person/sf:employment_information/sf:job_information/sf:cost_center", nsMgr)
                    If costCenterNode IsNot Nothing Then
                        empData("cost_center") = costCenterNode.InnerText
                    Else
                        empData("cost_center") = "PENDENTE"
                    End If

                    ' 9. Extrair <manager_id>
                    Dim managerIdNode = sfobjNode.SelectSingleNode("sf:person/sf:employment_information/sf:job_information/sf:manager_id", nsMgr)
                    If managerIdNode IsNot Nothing Then
                        empData("manager_id") = managerIdNode.InnerText
                    Else
                        empData("manager_id") = "PENDENTE"
                    End If

                    ' 10. Extrair <department>
                    Dim departmentNode = sfobjNode.SelectSingleNode("sf:person/sf:employment_information/sf:job_information/sf:department", nsMgr)
                    If departmentNode IsNot Nothing Then
                        empData("department") = departmentNode.InnerText
                    Else
                        empData("department") = "PENDENTE"
                    End If

                    ' Opcional: Extrair <id> como employeeIdentifier (se ainda necessário)
                    Dim idNode = sfobjNode.SelectSingleNode("sf:id", nsMgr)
                    If idNode IsNot Nothing Then
                        empData("employeeIdentifier") = idNode.InnerText
                    Else
                        empData("employeeIdentifier") = "PENDENTE"
                    End If

                    ' Adicionar empData ao consumers
                    consumers.Add(empData)
                Next

                EscreveLog($"Total de registros parseados: {consumers.Count}")
                Return consumers

            Catch ex As Exception
                ' Tratar exceção e registrar log
                EscreveLog($"Erro ao processar o XML: {ex.Message}")
                Return New List(Of Dictionary(Of String, Object))() ' Retorna lista vazia em caso de erro

            Finally
                EscreveLog("Finalizando ParseCompoundEmployeeXml.")
            End Try

        End Function


        ' 6 ######## MERGE_CONSUMIDOR
        <WebMethod()>
        Public Function SincronizarCompoundEmployee_6(pPConn_Banco As String) As String
            Try
                EscreveLog("Iniciando SincronizarCompoundEmployee")

                Dim accessToken = ObterTokenAcesso()
                EscreveLog("AccessToken obtido.")

                ' 1) Fazer login SOAP com Bearer token para obter sessionId
                Dim sessionId = SfapiLogin(accessToken)
                EscreveLog("SessionId obtido: " & sessionId)

                ' Buscar TODAS as páginas
                Dim consumersData = SfapiQueryAll(sessionId)

                EscreveLog("Dados do CompoundEmployee parseados, total: " & consumersData.Count)
                EscreveLog("INICIO DADOS")
                ' **Logar os Dados Extraídos**
                For Each consumer In consumersData
                    Dim logEntry As New StringBuilder()
                    For Each kvp In consumer
                        logEntry.Append($"{kvp.Key}={kvp.Value}, ")
                    Next
                    If logEntry.Length > 0 Then
                        logEntry.Length -= 2 ' Remove trailing comma and space
                    End If
                    ' EscreveLog($"Consumidor: {logEntry.ToString()}")
                Next
                EscreveLog("FIM DADOS")

                ' 4) Gerar XML para sincronização
                Dim xmlConsumidores As String = GerarXmlConsumidores(consumersData)
                EscreveLog("XML gerado para consumidores." & xmlConsumidores)

                ' 5) Sincronizar em lote (chamar WSSincronizacao)
                Dim resp As String = WSSincronizacao.SincronizarConsumidoresEmLote(xmlConsumidores, pPConn_Banco)
                EscreveLog("SincronizarConsumidoresEmLote retorno: " & resp)

                Return "Sincronização CompoundEmployee concluída com sucesso. XML: " & xmlConsumidores
            Catch ex As Exception
                EscreveLog("Erro em SincronizarCompoundEmployee: " & ex.Message)
                Throw
            End Try
        End Function

        <WebMethod()>
        Public Function SincronizarTodasAsAPIs() As String
            Try
                Dim sb As New StringBuilder()

                Dim pPConn_Banco As String = ConfigurationManager.AppSettings("VALE")

                ' 1 ######## MERGE_EMPRESA
                sb.AppendLine(SincronizarFOCompany_1(pPConn_Banco))

                ' 2 ######## MERGE_CARGO
                sb.AppendLine(SincronizarFOJobCode_2(pPConn_Banco))

                ' 3 ######## MERGE_DEPARTAMENTO
                sb.AppendLine(SincronizarFODepartment_3(pPConn_Banco))

                ' 4 ######## MERGE_FILIAL
                sb.AppendLine(SincronizarFOLocation_4(pPConn_Banco))

                ' 5 ######## MERGE_HIERARQUIA
                'sb.AppendLine(SincronizarAPIHierarquia_5(pPConn_Banco))

                ' 6 ######## MERGE_CONSUMIDOR
                sb.AppendLine(SincronizarCompoundEmployee_6(pPConn_Banco))

                Return sb.ToString()
            Catch ex As Exception
                EscreveLog("Erro em SincronizarTodasAsAPIs: " & ex.Message)
                Throw
            End Try
        End Function

    End Class
End Namespace