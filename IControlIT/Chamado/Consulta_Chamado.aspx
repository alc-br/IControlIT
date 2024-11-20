<%@ Page Language="VB" Async="true" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Consulta_Chamado.aspx.vb" Inherits="IControlIT.Consulta_Chamado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- INICIO PÁGINA -->

    <style type="text/css">

        /* Estilos para o contêiner da coluna esquerda */
        .left-column {
            background-color: #fefefe;
            border-radius: 8px;
            padding: 30px;
            color: #333;
            border: 1px solid #d8d8d8;
        }

        /* Espaçamento entre os elementos */
        .left-column p {
            margin: 0;
            display: block;
            flex-direction: column; /* Organiza rótulo e valor em coluna */
            line-height: 1.2;
        }

        /* Estilo para os rótulos (ex: Request Number) */
        .left-column p strong {
            color: #999;
            font-size: 10.5px;
            text-transform: uppercase!important;
            letter-spacing: 0.5px;
            margin-bottom: 2px; /* Espaço entre rótulo e valor */
        }

        /* Estilo para os valores (ex: RITM02037863) */
        .left-column p span {
            color: #222;
            font-size: 14px; /* Maior destaque para o valor */
            font-weight: 500;
        }

        /* Linha divisória para organizar os blocos */
        .left-column .content-top p + br,
        .left-column #camposCondicionaisContainer,
        .left-column #modalComentariosContainer {
            margin-top: 20px;
            padding-top: 20px;
            border-top: 1px solid #eaeaea;
        }

        /* Título para "Informações Adicionais" */
        #modalComentariosContainer p:first-child {
            font-size: 10px;
            font-weight: bold;
            color: #555;
            margin-bottom: 10px;
        }

        /* Estilo para o texto de "Informações Adicionais" */
        #modalComentariosContainer p span {
            color: #444;
            font-weight: normal;
        }





        .modal-custom {
            display: none;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background-color: #fafafa;
            border-radius: 8px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
            max-width: 1000px;
            width: 90%;
            padding: 20px;
            z-index: 1000;
        }

        .modal-header {
            border-bottom: 1px solid #ddd;
            padding-bottom: 10px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .modal-title {
            font-size: 1.5rem;
            font-weight: 600;
            margin: 0;
        }

        .btn-send-mail{
            margin-top: 20px !important;
            background-color: #eee !important;
            color: #777 !important;
            box-shadow: none !important;
            padding: 12px 20px !important;
            border-radius: 4px !important;
        }

        .close {
            background: none;
            border: none;
            font-size: 1.5rem;
            cursor: pointer;
        }

        .modal-body {
            padding-top: 20px;
            font-size: 14px;
        }

        .left-column {
            width: 50%;
            padding-right: 20px;
        }

        .right-column {
            width: 50%;
            padding-left: 20px;
        }

        .form-group-row {
            display: flex;
            gap: 20px;
            justify-content: space-between;
        }

        .form-group {
            flex: 1;
            min-width: 0;
        }

        .form-control:read-only {
            background-image: none;
        }

        .form-control {
            border-radius: 4px;
            border: 1px solid #ddd;
            padding: 10px;
            width: 100%;
            height: 40px;
            background: #fff;
        }

        .custom-file {
            position: relative;
            border: 1px dashed #ccc;
            border-radius: 4px;
            padding: 10px;
            text-align: center;
            cursor: pointer;
            height: 44px;
            margin-bottom: 10px;
        }

        div#ContentPlaceHolder2_camposCondicionaisContainer p {
            line-height: 18px;
        }

        .flex-fill.left-column p strong {
            font-weight: 400;
        }

        .flex-fill.left-column p {
            margin-bottom: 10px;
        }

        #loadingSpinner {
            display: none;
            margin-top: 10px;
            text-align: center;
        }

        .vertical-separator {
            border-left: 1px solid #eee;
            height: auto;
            margin: 0 20px;
        }

        .divider {
            border-bottom: 1px solid #ddd;
            margin: 10px 0 20px 0;
        }

        .modal-footer {
            border-top: 1px solid #ddd;
            padding-top: 10px;
            display: flex;
            justify-content: space-between;
        }

        .in-modal-footer {
            display: flex;
            justify-content: right;
        }

        .btn-secondary {
            background-color: #aaa;
            border: none;
            color: #fff;
            padding: 10px 20px;
            border-radius: 4px;
            cursor: pointer;
        }

        /* Manter o restante do seu CSS atual */
        .ajax__tab_xp .ajax__tab_body {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: transparent;
        }

        .justify-content-between {
            justify-content: space-between !important;
            margin: 20px 0;
            background: #fff;
            padding: 25px 20px 25px 25px;
            border-radius: 10px;
        }

        .justify-content-between p {
          margin-bottom: 0;
          margin-top: 10px;
          font-size: 15px;
        }

        #divTitulo {
            display: none;
        }

        .date-time {
            font-size: 12px;
            padding-right: 10px;
        }

        .modal-dialog {
            max-width: 60% !important;
        }

        .modal-backdrop-custom {
            background-color: rgba(0, 0, 0, 0.5);
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 1050 !important;
        }

        .flex-fill {
            display: flex;
            flex-direction: column;
        }

        .verde {
            background-color: #32B877 !important;
        }

        .azul {
            background-color: #538DD7 !important;
        }

        .vermelho {
            background-color: #db6363 !important;
        }

        .esconde {
            display: none!important;
        }

        .modal-custom{
            z-index: 1051 !important;
        }

        .dropdown-emails {
            display: none;
            border: 1px solid #ddd;
            border-radius: 4px;
            max-height: 150px;
            overflow-y: auto;
            padding: 10px;
            position: absolute;
            background-color: white;
            z-index: 1000;
            width: calc(80% - 20px);
        }

        .dropdown-emails.active {
            display: block;
        }

    div#modalComentariosContainer {
        margin-top: 10px;
    }
    #modalComentarios {
        line-height: 18px;
    }

    div#modalComentariosContainer p strong {
        font-size: 95%;
        font-weight: 500;
        color: #3d372a;
    }

    .form-control, label, input::placeholder {
        line-height: 1.1;
        color: #555 !important;
        font-weight: 400;
    }

    .form-check, label {
        font-size: 12px;
        line-height: 7px;
        color: #777;
        font-weight: 300;
        padding-left: 2px;
    }

    .pagination-container {
        text-align: center;
        margin: 20px 0;
    }

    .pagination {
        display: inline-block;
    }

    .pagination .page-buttons {
        display: inline-flex;
        align-items: center;
    }

    .pagination .page-buttons .asp-button {
        margin: 0 10px;
    }

    .items-per-page {
        margin-top: 10px;
    }

    .desativado{
        background-color: transparent!important;
        color: #777777!important;
        border: 1px solid #999999!important;
        box-shadow: none!important;
    }

</style>

<!-- Inclua o CSS do Flatpickr -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
<!-- Inclua o JavaScript do Flatpickr -->
<script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>


<div runat="server" title="CHAMADOS">
        
    <!-- Campos ocultos essenciais para as ações -->
    <input type="text" id="hfNewUserNumber" class="esconde" runat="server" />
    <input type="text" id="hfNewAreaCode" class="esconde" runat="server" />
    <input type="text" id="hfTelecomProvider" class="esconde" runat="server" />
    <input type="text" id="hfUserNumber" class="esconde" runat="server" />
    <input type="text" id="hfIdAtivo" class="esconde" runat="server" />
    <input type="text" id="hfIdConsumidor" class="esconde" runat="server" />
    <input type="text" id="hfNewPlanoContrato" class="esconde" runat="server" />
    <input type="text" id="hfIdConglomerado" class="esconde" runat="server" />
    <input type="text" id="hfIdChamado" class="esconde" runat="server" />
    <input type="text" id="hfTipoSolicitacao" class="esconde" runat="server" />
    <input type="text" id="hfComentarios" class="esconde" runat="server" />
    <input type="text" id="hfEstado" class="esconde" runat="server" />
    <input type="text" id="hfRequestNumber" class="esconde" runat="server" />
    <input type="text" id="hfWorkOrderNumber" class="esconde" runat="server" />
    <input type="text" id="hfUserName" class="esconde" runat="server" />
    <input type="text" id="hfNrAtivo" class="esconde" runat="server" />
    <input type="text" id="hfEmailsSelecionados" class="esconde" runat="server" />
    <input type="text" id="hfEmailResponsavelRegional" class="esconde" runat="server" />
    <input type="text" id="hfServicePack" class="esconde" runat="server" />
    <input type="text" id="hfNovaLinha" class="esconde" runat="server" />
    <input type="text" id="hfMigrationDevice" class="esconde" runat="server" />
    <input type="text" id="hfnomePlanoMigracaoNL" class="esconde" runat="server" />
    <input type="text" id="hfOriginalMigrationDevice" class="esconde" runat="server" />
    <input type="text" id="hfNomePlanoPortabilidade" class="esconde" runat="server" />
    <input type="text" id="hfDataEfetivacaoPortabilidade" class="esconde" runat="server" />
    <input type="text" id="hfDataRecebimentoChip" class="esconde" runat="server" />
    <input type="text" id="hfAlterarLinha" class="esconde" runat="server" />
    <input type="text" id="hfFaturaDropdown" class="esconde" runat="server" />
    <input type="text" id="hfDesignationProduct" class="esconde" runat="server" />
    
    
    


    <div>
        <div class="row">
            <div class="col-md-12">
                <div class="d-flex justify-content-between align-items-center activity">
                    <div>
                        <span class="ml-2" style="font-size: 24px; font-weight: bold;">Chamados Recentes</span>
                    </div>
                </div>
                <div class="mt-3">
                    <ul class="list list-inline">
                        <asp:Repeater ID="rptChamados" runat="server">
                            <ItemTemplate>
                                <li class="d-flex justify-content-between">
                                    <div class="d-flex flex-row align-items-center">
                                        <div class="ml-3">
                                            <div class="d-flex flex-row text-black-50 date-time" style="font-size: 13px; font-weight: 400;">
                                                <div class="d-flex align-items-center mr-3" style="font-size: 15px;">
                                                    <span class='badge badge-pill badge-primary <%# GetBadgeClass(Eval("Estado")) %>' style="padding: 6px; border-radius: 4px;margin-right: 20px">
                                                        <%# Eval("Estado") %>
                                                    </span>
                                                </div>
                                                <div class="d-flex flex-column">
                                                <!-- Linha 1 - Todos os campos lado a lado exceto Comentários -->
                                                <div class="d-flex align-items-center">
                                                    <div><span style="font-weight: 600;"><%# Eval("Tipo_Solicitacao") %></span></div>
                                                    <div class="ml-4">
                                                        <i class="fa fa-hashtag"></i>
                                                        <span class="ml-2"><%# Eval("RequestNumber") %></span>
                                                    </div>
                                                    <div class="ml-4">
                                                        <i class="fa fa-clipboard"></i>
                                                        <span class="ml-2"><%# Eval("WorkOrderNumber") %></span>
                                                    </div>
                                                    <div class="ml-4">
                                                        <i class="fa fa-user"></i>
                                                        <span class="ml-2"><%# HttpUtility.HtmlEncode(Eval("UserName")) %></span>
                                                    </div>
        
                                                    <%-- Verifica se NrAtivo não é nulo ou vazio e renderiza o bloco apenas se tiver valor --%>
                                                    <asp:Panel runat="server" Visible='<%# Not String.IsNullOrEmpty(Eval("DesignationProduct").ToString()) %>'>
                                                        <div class="ml-4">
                                                            <i class="fa fa-phone"></i>
                                                            <span class="ml-2"><%# Eval("DesignationProduct") %></span>
                                                        </div>
                                                    </asp:Panel>

                                                    <%-- Verifica se Email_Enviado = "True" e Estado não é "concluído" --%>
                                                    <asp:Panel runat="server" Visible='<%# Eval("Email_Enviado").ToString() = "True" And Eval("Estado").ToString().ToLower() <> "concluído" %>'>
                                                        <div class="ml-4">
                                                            <i class="fa fa-share" style="color:#538DD7"></i>
                                                            <span class="ml-2" style="color:#538DD7"><i>Email enviado</i></span>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
    
                                                <!-- Linha 2 - Comentários -->
                                                <p class='<%# If(String.IsNullOrEmpty(Eval("Comentarios").ToString()), "esconde", "") %>'>
                                                    <i><%# Eval("Comentarios") %></i>
                                                </p>
                                                <p class='<%# If(String.IsNullOrEmpty(Eval("ErroSistema").ToString()), "esconde", "") %>'>
                                                    <i style="color:red;font-size:12px;"><%# Eval("ErroSistema") %></i>
                                                </p>
                                                    
                                            </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="d-flex flex-row align-items-center">
                                        <div class="d-flex flex-column mr-2">
                                            <span class="date-time">Criado em: <%# Eval("Data_Criacao", "{0:dd/MM/yyyy HH:mm:ss}") %></span>
                                            <span class="date-time" runat="server" Visible='<%# Eval("Estado").ToString().ToLower() <> "cancelado" %>'><%# Eval("UltimaAcao")%> em: <%# Eval("Data_Atualizacao", "{0:dd/MM/yyyy HH:mm:ss}") %></span>
                                        </div>
                                        <span runat="server" Visible='<%# Eval("Estado").ToString().ToLower() <> "cancelado" %>'>
                                            <i class="fa fa-bars" style="cursor: pointer; font-size: 20px; margin-right: 10px; margin-left: 20px;" 
                                               onclick="abrirModalChamado(
                                                   '<%# Eval("Id_Chamado") %>',
                                                   '<%# Eval("RequestNumber") %>',
                                                   '<%# Eval("WorkOrderNumber") %>',
                                                   '<%# Eval("Estado") %>',
                                                   '<%# Eval("AdditionalInformation") %>',
                                                   '<%# Eval("UserName") %>',
                                                   '<%# Eval("TransactionID") %>',
                                                   '<%# Eval("Tipo_Solicitacao") %>',
                                                   '<%# Eval("UserNumber") %>',
                                                   '<%# Eval("TelecomProvider") %>',
                                                   '<%# Eval("NewAreaCode") %>',
                                                   '<%# Eval("NewUserNumber") %>',
                                                   '<%# Eval("FramingPlan") %>',
                                                   '<%# Eval("ServicePack") %>',
                                                   '<%# Eval("PlanoContratoAtual") %>',
                                                   '<%# Eval("Id_Ativo") %>',
                                                   '<%# Eval("Id_Conglomerado") %>',
                                                   '<%# Eval("NrAtivo") %>',
                                                   '<%# Eval("MigrationDevice") %>',
                                                   '<%# Eval("Email_Enviado") %>',
                                                   '<%# Eval("DesignationProduct") %>',
                                                   '<%# Eval("newTelecomProvider") %>',
                                                   '<%# Eval("Campo1") %>',
                                                   '<%# Eval("Campo2") %>',
                                                   '<%# Eval("Campo3") %>',
                                                   '<%# Eval("Campo4") %>',
                                                   '<%# Eval("Campo5") %>',
                                                   '<%# Eval("Campo6") %>',
                                                   '<%# Eval("ErroSistema") %>'
                                               )">
                                            </i>
                                        </span>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="pagination-container">
                    <div class="pagination">
                        <div class="page-buttons">
                            <asp:Button ID="btnPreviousPage" runat="server" Text="Anterior" OnClick="BtnPreviousPage_Click" CssClass="asp-button" />
                            <asp:Label ID="lblPageNumber" runat="server" Text="1"></asp:Label>
                            <asp:Button ID="btnNextPage" runat="server" Text="Próxima" OnClick="BtnNextPage_Click" CssClass="asp-button" />
                        </div>
                    </div>

                    <!-- Dropdown para selecionar itens por página -->
                    <div class="items-per-page">
                        <label for="ddlItemsPerPage">Itens por página:</label>
                        <asp:DropDownList ID="ddlItemsPerPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemsPerPage_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True"/>
                            <asp:ListItem Text="30" Value="30" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<script >


    let arquivosSelecionados = [];

    //Limpa campos da area de emails
    function resetCamposDireita() {
        // Empresa Contratante e Fatura Agrupadora (Dropdowns)
        document.getElementById('ContentPlaceHolder2_empresaContratante').selectedIndex = 0;
        document.getElementById('ContentPlaceHolder2_faturaDropdown').selectedIndex = 0;

        // Corpo do Email (Text Area)
        document.getElementById('ContentPlaceHolder2_corpoEmail').value = '';

        // Operadora Emails
        document.getElementById('operadoraEmails').value = '';
        document.querySelectorAll('#emailDropdown input[type="checkbox"]').forEach(function (checkbox) {
            checkbox.checked = false;
        });
        document.getElementById('ContentPlaceHolder2_selectedEmails').innerHTML = ''; // Limpa a lista de emails selecionados
        document.getElementById('ContentPlaceHolder1_hfEmailsSelecionados').value = '';


        // Anexos (limpar a lista e a variável de arquivos selecionados)
        arquivosSelecionados = []; // Reseta a lista de anexos selecionados
        document.getElementById('listaAnexos').innerHTML = ''; // Limpa a exibição dos anexos
        document.getElementById('<%= hfBase64Files.ClientID %>').value = '';

        // Email do Responsável na Regional
        document.getElementById('ContentPlaceHolder2_emailResponsavelRegional').value = '';
    }

    //Acoes do modal
    function abrirModalChamado(idChamado, requestNumber, workOrderNumber, estado, comentarios, userName, transactionID, tipoSolicitacao, userNumber, telecomProvider, newAreaCode, newUserNumber, framingPlan, servicePack, planoContratoAtual, idAtivo, idConglomerado, nrAtivo, migrationDevice, emailEnviado, designationProduct, newTelecomProvider, Campo1, Campo2, Campo3, Campo4, Campo5, Campo6, ErroSistema) {

        document.querySelector('#ContentPlaceHolder1_hfDesignationProduct').value = designationProduct;

        function loadFields() {

            // Funções de utilidade para configurar visibilidade e valores
            function setElementVisibility(elementId, value) {
                var element = document.getElementById(elementId);
                if (element) {
                    if (value && value.trim() !== "") {
                        element.style.display = "block";
                        element.querySelector('span').innerText = value;
                    } else {
                        element.style.display = "none";
                    }
                } else {
                    console.error("Elemento com ID " + elementId + " não encontrado.");
                }
            }

            function setInputValue(elementId, value) {
                var element = document.getElementById(elementId);
                if (element) {
                    element.value = value || '';
                } else {
                    console.error("Elemento com ID " + elementId + " não encontrado.");
                }
            }

            // Preencher os novos campos adicionais
            setInputValue('ContentPlaceHolder1_hfUserNumber', userNumber);
            setInputValue('ContentPlaceHolder1_hfTelecomProvider', telecomProvider);
            setInputValue('ContentPlaceHolder1_hfNewAreaCode', newAreaCode);
            setInputValue('ContentPlaceHolder1_hfNewUserNumber', newUserNumber);
            setInputValue('ContentPlaceHolder1_hfIdAtivo', idAtivo);
            setInputValue('ContentPlaceHolder1_hfIdConsumidor', transactionID);
            setInputValue('ContentPlaceHolder1_hfIdChamado', idChamado);
            setInputValue('ContentPlaceHolder1_hfIdConglomerado', idConglomerado);
            setInputValue('ContentPlaceHolder1_hfNewPlanoContrato', framingPlan);
            setInputValue('ContentPlaceHolder1_hfTipoSolicitacao', tipoSolicitacao);
            setInputValue('ContentPlaceHolder1_hfComentarios', comentarios);
            setInputValue('ContentPlaceHolder1_hfEstado', estado);
            setInputValue('ContentPlaceHolder1_hfRequestNumber', requestNumber);
            setInputValue('ContentPlaceHolder1_hfWorkOrderNumber', workOrderNumber);
            setInputValue('ContentPlaceHolder1_hfUserName', userName);
            setInputValue('ContentPlaceHolder1_hfNrAtivo', nrAtivo);
            setInputValue('ContentPlaceHolder1_hfServicePack', servicePack);
            setInputValue('ContentPlaceHolder1_hfMigrationDevice', migrationDevice);
            setInputValue('ContentPlaceHolder1_hfOriginalMigrationDevice', migrationDevice);


            if (emailEnviado == "False") {
                var btnExecutar = document.getElementById('ContentPlaceHolder2_btnExecutar');
                btnExecutar.classList.add('desativado');
                btnExecutar.disabled = true;
                btnExecutar.style.cursor = 'not-allowed';
            } else {
                var btnExecutar = document.getElementById('ContentPlaceHolder2_btnExecutar');
                btnExecutar.classList.remove('desativado');
                btnExecutar.classList.remove('esconde');
                // Converte o estado para minúsculas e verifica se é 'concluído'
                if (estado.toLowerCase() === 'concluído') {
                    // Adiciona a classe "desativado" e altera o texto do botão
                    var btnExecutar = document.getElementById('ContentPlaceHolder2_btnExecutar');
                    btnExecutar.classList.add('desativado');
                    btnExecutar.classList.add('esconde');
                    btnExecutar.value = 'Chamado Concluído';


                    // Impede que o botão seja clicável e altera o cursor para "não permitido"
                    btnExecutar.disabled = true;
                    btnExecutar.style.cursor = 'not-allowed';
                } else {
                    // Remove a classe "desativado", volta o texto original e permite o clique
                    var btnExecutar = document.getElementById('ContentPlaceHolder2_btnExecutar');
                    btnExecutar.classList.remove('desativado');
                    btnExecutar.value = 'Executar';

                    // Permite o clique novamente e altera o cursor para "pointer"
                    btnExecutar.disabled = false;
                    btnExecutar.style.cursor = 'pointer';
                }
            }

            if (tipoSolicitacao === "ALTERAR PROPRIETARIO") {
                // Remove a classe "desativado", volta o texto original e permite o clique
                var btnExecutar = document.getElementById('ContentPlaceHolder2_btnExecutar');
                btnExecutar.classList.remove('desativado');
                btnExecutar.value = 'Executar';

                // Permite o clique novamente e altera o cursor para "pointer"
                btnExecutar.disabled = false;
                btnExecutar.style.cursor = 'pointer';
            }

            // Exibir campos padrão
            setElementVisibility('ContentPlaceHolder2_modalIdChamadoContainer', idChamado);
            setElementVisibility('modalRequestNumberContainer', requestNumber);
            setElementVisibility('modalWorkOrderNumberContainer', workOrderNumber);
            setElementVisibility('modalEstadoContainer', estado);
            setElementVisibility('modalComentariosContainer', comentarios);
            setElementVisibility('modalUserNameContainer', userName);
            setElementVisibility('modalTransactionIDContainer', transactionID);
            setElementVisibility('modalTipoSolicitacaoContainer', tipoSolicitacao);
            setElementVisibility('modalPlanoContratoAtualContainer', planoContratoAtual);
            setElementVisibility('modalNumeroLinhaContainer', designationProduct);
            setElementVisibility('modalOperadoraContainer', telecomProvider);

            // Limpar campos condicionais
            var camposCondicionaisContainer = document.getElementById('ContentPlaceHolder2_camposCondicionaisContainer');
            camposCondicionaisContainer.innerHTML = "";

        }

        // Função para aplicar a máscara "dd/mm/aaaa" nos campos de data
        function mascaraData(campo) {
            var valor = campo.value.replace(/\D/g, ""); // Remove caracteres não numéricos
            if (valor.length >= 2) {
                valor = valor.slice(0, 2) + '/' + valor.slice(2);
            }
            if (valor.length >= 5) {
                valor = valor.slice(0, 5) + '/' + valor.slice(5, 9);
            }
            campo.value = valor;

            // Sincronizar o valor formatado com o campo oculto correspondente
            if (campo.id === "dataRecebimentoChip") {
                document.querySelector('#ContentPlaceHolder1_hfDataRecebimentoChip').value = campo.value;
            } else if (campo.id === "dataEfetivacaoPortabilidade") {
                document.querySelector('#ContentPlaceHolder1_hfDataEfetivacaoPortabilidade').value = campo.value;
            }
        }

        function showConditionalFields(solicitacao) {

            // Limpar campos condicionais
            var camposCondicionaisContainer = document.getElementById('ContentPlaceHolder2_camposCondicionaisContainer');

            // Exibir campos condicionais
            switch (solicitacao) {
                case 'ALTERAR DDD':
                    camposCondicionaisContainer.innerHTML = '<p><strong>Novo DDD solicitado:</strong> <span>' + newAreaCode + '</span></p>';
                    if (estado === 'Concluído') {
                        camposCondicionaisContainer.innerHTML += '<p><strong>Nova linha com esse DDD:</strong> <span>' + Campo1 + '</span></p>';
                    } else {
                        camposCondicionaisContainer.innerHTML += `
                            <p>
                                <input type="number" id="novaLinha" class="form-control" placeholder="Informe a nova linha"
                                maxlength="11" oninput="if(this.value.length > 11) this.value = this.value.slice(0, 11);" />
                            </p>
                        `;

                        var novaLinhaInput = document.querySelector('#novaLinha');
                        if (novaLinhaInput) {
                            novaLinhaInput.addEventListener('blur', function () {
                                document.querySelector('#ContentPlaceHolder1_hfNovaLinha').value = this.value;
                            });
                        }
                    }
                    break;
                case 'ALTERAR NUMERO':
                    camposCondicionaisContainer.innerHTML = '<p><strong>Antigo Número:</strong> <span>' + designationProduct + '</span></p>';
                    if (estado === 'Concluído') {
                        camposCondicionaisContainer.innerHTML += '<p><strong>Novo Número:</strong> <span>' + Campo1 + '</span></p>';
                    } else {
                        camposCondicionaisContainer.innerHTML += `
                            <p>
                                <input type="number" id="novaLinha" class="form-control" placeholder="Informe a nova linha"
                                maxlength="11" oninput="if(this.value.length > 11) this.value = this.value.slice(0, 11);" />
                            </p>
`;

                        var novaLinhaInput = document.querySelector('#novaLinha');
                        if (novaLinhaInput) {
                            novaLinhaInput.addEventListener('blur', function () {
                                document.querySelector('#ContentPlaceHolder1_hfNovaLinha').value = this.value;
                            });
                        }
                    }
                    break;
                case 'MIGRACAO DE PLANO':
                    if (estado === 'Concluído') {
                        camposCondicionaisContainer.innerHTML = '<p><strong>Tipo de migração:</strong> <span>' + migrationDevice + '</span></p>';
                        if (migrationDevice.toLowerCase() == 'smartphone para basico') {
                            camposCondicionaisContainer.innerHTML += '<p><strong>Novo Plano:</strong> <span>Básico</span></p>';
                        } else {
                            camposCondicionaisContainer.innerHTML += '<p><strong>Novo Plano:</strong> <span>' + Campo1 + '</span></p>';
                        }
                    } else {
                        if (migrationDevice.toLowerCase() == 'smartphone para basico') {
                            camposCondicionaisContainer.innerHTML = '<p><strong>Novo Plano:</strong> <span>Básico</span></p>';
                        } else {
                            camposCondicionaisContainer.innerHTML = '<p><strong>Tipo de migração:</strong> <span>' + migrationDevice + '</span></p>';
                            camposCondicionaisContainer.innerHTML += '<p><select id="nomePlanoMigracao" class="form-control"><option value="1">Selecione um plano</option></select></p>';
                        }

                        // Adiciona o evento de desfoque para atualizar hfMigrationDevice
                        var nomePlanoMigracao = document.querySelector('#nomePlanoMigracao');
                        if (nomePlanoMigracao) {
                            nomePlanoMigracao.addEventListener('change', function () {
                                var textoSelecionado = nomePlanoMigracao.options[nomePlanoMigracao.selectedIndex].text;
                                document.querySelector('#ContentPlaceHolder1_hfMigrationDevice').value = textoSelecionado;
                            });
                        }
                    }
                    break;
                case 'CONTRATAR PACOTE DE ROAMING INTERNACIONAL':
                    if (servicePack && servicePack.trim() !== '') {
                        camposCondicionaisContainer.innerHTML = '<p><strong>Dados do Pacote:</strong> <span>' + servicePack + '</span></p>';
                    }
                    break;
                case 'HABILITAR ACESSO':
                    camposCondicionaisContainer.innerHTML = '<p><strong>Ação:</strong> <span>Habilitar Acesso</span></p>';
                    break;
                case 'DESABILITAR ACESSO':
                    camposCondicionaisContainer.innerHTML = '<p><strong>Ação:</strong> <span>Desabilitar Acesso</span></p>';
                    break;
                case 'CANCELAR LINHA':
                case 'PERDA/ROUBO':
                    if (comentarios && comentarios.trim() !== '') {
                        camposCondicionaisContainer.innerHTML = '<p><strong>Motivo:</strong> <span>' + comentarios + '</span></p>';
                    }
                    break;
                case 'PORTABILIDADE DE LINHA':
                    camposCondicionaisContainer.innerHTML = '<p><strong>Operadora origem:</strong> <span>' + telecomProvider + '</span></p>';
                    camposCondicionaisContainer.innerHTML += '<p><strong>Operadora destino:</strong> <span>' + newTelecomProvider + '</span></p><br />';
                    if (estado === 'Concluído') {
                        camposCondicionaisContainer.innerHTML += '<p><strong>Plano:</strong> <span>' + Campo1 + '</span></p>';
                        camposCondicionaisContainer.innerHTML += '<p><strong>Data de recebimento do chip:</strong> <span>' + Campo2 + '</span></p>';
                        camposCondicionaisContainer.innerHTML += '<p><strong>Data de efetivação da portabilidade:</strong> <span>' + Campo3 + '</span></p>';
                    } else {
                        camposCondicionaisContainer.innerHTML += `
                            <p>
                                <label for="nomePlanoPortabilidade">Selecione um plano</label>
                                <select id="nomePlanoPortabilidade" class="form-control">
                                    <option value="1">Selecione um plano</option>
                                </select>
                            </p>
                            <p>
                                <label for="dataRecebimentoChip">Data de recebimento do chip</label>
                                <input type="text" id="dataRecebimentoChip" class="form-control" placeholder="Selecione a data" style="background:#fff;" />
                            </p>
                            <p>
                                <label for="dataEfetivacaoPortabilidade">Data de efetivação da portabilidade</label>
                                <input type="text" id="dataEfetivacaoPortabilidade" class="form-control" placeholder="Selecione a data" style="background:#fff;" />
                            </p>
                        `;

                        // Inicializa o Flatpickr nos campos de data
                        flatpickr("#dataRecebimentoChip", { dateFormat: "d/m/Y" });
                        flatpickr("#dataEfetivacaoPortabilidade", { dateFormat: "d/m/Y" });

                        // Sincronizar o valor formatado com o campo oculto correspondente
                        var dataRecebimentoChip = document.querySelector('#dataRecebimentoChip');
                        var dataEfetivacaoPortabilidade = document.querySelector('#dataEfetivacaoPortabilidade');

                        // Adiciona evento de mudança para dataRecebimentoChip
                        dataRecebimentoChip.addEventListener('change', function () {
                            document.querySelector('#ContentPlaceHolder1_hfDataRecebimentoChip').value = dataRecebimentoChip.value;
                        });

                        // Adiciona evento de mudança para dataEfetivacaoPortabilidade
                        dataEfetivacaoPortabilidade.addEventListener('change', function () {
                            document.querySelector('#ContentPlaceHolder1_hfDataEfetivacaoPortabilidade').value = dataEfetivacaoPortabilidade.value;
                        });

                        // Adiciona o evento de mudança para atualizar hfNomePlanoPortabilidade
                        var nomePlanoPortabilidade = document.querySelector('#nomePlanoPortabilidade');
                        if (nomePlanoPortabilidade) {
                            nomePlanoPortabilidade.addEventListener('change', function () {
                                var textoSelecionado = nomePlanoPortabilidade.options[nomePlanoPortabilidade.selectedIndex].text;
                                document.querySelector('#ContentPlaceHolder1_hfNomePlanoPortabilidade').value = textoSelecionado;
                            });
                        }
                    }
                    break;
                case 'ALTERAR PROPRIETARIO':
                    if (userNumber && userNumber.trim() !== '') {
                        camposCondicionaisContainer.innerHTML = '<p><strong>Novo Proprietário:</strong> <span>' + newUserNumber + '</span></p>';
                    }
                    break;
                case 'NOVA LINHA':
                    if (estado === 'Concluído') {
                        camposCondicionaisContainer.innerHTML = '<p><strong>Nova Linha:</strong> <span>' + Campo1 + '</span></p>';
                        camposCondicionaisContainer.innerHTML += '<p><strong>Plano:</strong> <span>' + Campo2 + '</span></p>';
                    } else {
                        camposCondicionaisContainer.innerHTML = '<p><strong>Plano de referência:</strong> <span>' + framingPlan + '</span></p>';
                        camposCondicionaisContainer.innerHTML += `
                                <p>
                                    <input type="number" id="novaLinha" class="form-control" placeholder="Informe a nova linha"
                                    maxlength="11" oninput="if(this.value.length > 11) this.value = this.value.slice(0, 11);" />
                                </p>
                                <p>
                                    <select id="nomePlanoMigracaoNL" class="form-control">
                                        <option value="1">Selecione um plano</option>
                                    </select>
                                </p>
                            `;

                        var nomePlanoMigracaoNL = document.querySelector('#nomePlanoMigracaoNL');
                        if (nomePlanoMigracaoNL) {
                            nomePlanoMigracaoNL.addEventListener('change', function () {
                                var textoSelecionado = nomePlanoMigracaoNL.options[nomePlanoMigracaoNL.selectedIndex].text;
                                document.querySelector('#ContentPlaceHolder1_hfnomePlanoMigracaoNL').value = textoSelecionado;
                            });
                        }


                        var novaLinhaInput = document.querySelector('#novaLinha');
                        if (novaLinhaInput) {
                            novaLinhaInput.addEventListener('blur', function () {
                                document.querySelector('#ContentPlaceHolder1_hfNovaLinha').value = this.value;
                            });
                        }
                    }
                    break;
                default:
                    console.error('Tipo de solicitação desconhecido.');
                    break;
            }


            const faturaDropdown = document.querySelector("#ContentPlaceHolder2_faturaDropdown");
            const hfFaturaDropdown = document.querySelector("#ContentPlaceHolder1_hfFaturaDropdown");

            if (faturaDropdown && hfFaturaDropdown) {
                // Adicionar evento ao dropdown
                faturaDropdown.addEventListener("change", function () {
                    // Atualizar o campo hidden com o valor selecionado
                    hfFaturaDropdown.value = this.value;
                });
            }



        }



        function carregarPlanos(idConglomerado) {
            $.ajax({
                type: "POST",
                url: "Consulta_Chamado.aspx/BuscarPlanosContrato",
                data: JSON.stringify({ idConglomerado: idConglomerado }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    try {
                        var planos = JSON.parse(response.d);  // Converte a resposta JSON para um array de strings
                        console.log("Planos carregados: ", planos); // Verifica os planos carregados
                    } catch (e) {
                        console.error("Erro ao fazer parse da resposta: ", e);
                        return;
                    }

                    // Obtém o elemento dropdown e o limpa antes de adicionar as novas opções
                    var planosDropdown = $('#nomePlanoMigracao');
                    var planosDropdownNP = $('#nomePlanoMigracaoNL');
                    var planosDropdownPortabilidade = $('#nomePlanoPortabilidade');

                    planosDropdown.empty();
                    planosDropdown.append('<option value="">Selecione um plano</option>'); // Placeholder

                    planosDropdownNP.empty();
                    planosDropdownNP.append('<option value="">Selecione um plano</option>'); // Placeholder

                    planosDropdownPortabilidade.empty();
                    planosDropdownPortabilidade.append('<option value="">Selecione um plano</option>'); // Placeholder

                    // Adiciona os planos ao dropdown
                    if (planos && planos.length > 0) {
                        planos.forEach(function (plano) {
                            planosDropdown.append('<option value="' + plano + '">' + plano + '</option>');
                            planosDropdownNP.append('<option value="' + plano + '">' + plano + '</option>');
                            planosDropdownPortabilidade.append('<option value="' + plano + '">' + plano + '</option>');
                        });
                    } else {
                        planosDropdown.append('<option value="">Nenhum plano encontrado</option>');
                        planosDropdownNP.append('<option value="">Nenhum plano encontrado</option>');
                        planosDropdownPortabilidade.append('<option value="">Nenhum plano encontrado</option>');
                    }
                },
                error: function (error) {
                    console.error('Erro ao carregar os planos de contrato: ', error);
                    $('#nomePlanoMigracao').empty().append('<option value="">Erro ao carregar os planos</option>');
                    $('#nomePlanoMigracaoNL').empty().append('<option value="">Erro ao carregar os planos</option>');
                    $('#nomePlanoPortabilidade').empty().append('<option value="">Erro ao carregar os planos</option>');
                }
            });
        }


        function carregarEmailsOperadora(idConglomerado) {
            $.ajax({
                type: "POST",
                url: "Consulta_Chamado.aspx/BuscarEmailsOperadora",
                data: JSON.stringify({ idConglomerado: idConglomerado }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    try {
                        var emails = JSON.parse(response.d);  // Faz o parse da string JSON em um array
                        console.log("Emails processados: ", emails); // Verifica o que foi extraído de 'response.d'
                    } catch (e) {
                        console.error("Erro ao fazer parse da resposta: ", e);
                        return;
                    }

                    var emailDropdown = $('#emailDropdown');
                    emailDropdown.empty(); // Limpa o dropdown antes de preencher

                    if (emails && emails.length > 0) {
                        // Adiciona os emails no dropdown com checkboxes
                        emails.forEach(function (email) {
                            emailDropdown.append('<label><input type="checkbox" value="' + email + '"> ' + email + '</label><br>');
                        });

                        // Adiciona dinamicamente o evento de change nos checkboxes
                        emailDropdown.find('input[type="checkbox"]').change(function () {
                            atualizarEmailsSelecionados();
                        });
                    } else {
                        emailDropdown.append('<span>Nenhum email encontrado.</span>');
                    }
                },
                error: function (error) {
                    console.error('Erro ao carregar os emails da operadora: ', error);
                    $('#emailDropdown').append('<span>Erro ao carregar os emails.</span>');
                }
            });
        }



        // Função para atualizar a exibição dos e-mails selecionados
        function atualizarEmailsSelecionados() {
            var selectedEmailsDiv = document.getElementById('ContentPlaceHolder2_selectedEmails');
            var selectedEmailsHf = document.getElementById('ContentPlaceHolder1_hfEmailsSelecionados');

            var selectedEmails = [];

            document.querySelectorAll('#emailDropdown input[type="checkbox"]:checked').forEach(function (checkedBox) {
                selectedEmails.push(checkedBox.value);
            });

            // Atualiza a div com os e-mails selecionados
            selectedEmailsDiv.innerHTML = selectedEmails.length > 0 ? selectedEmails.join(';') : 'Nenhum e-mail selecionado.';
            selectedEmailsHf.value = selectedEmails.length > 0 ? selectedEmails.join(';') : 'Nenhum e-mail selecionado.';
        }


        function carregarFaturaAgrupadora(idConglomerado) {
            $.ajax({
                type: "POST",
                url: "Consulta_Chamado.aspx/BuscarFaturaAgrupadora",
                data: JSON.stringify({ idConglomerado: idConglomerado }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    try {
                        var faturas = JSON.parse(response.d);  // Faz o parse da string JSON em um array
                        console.log("Faturas processadas: ", faturas); // Verifica o que foi extraído de 'response.d'
                    } catch (e) {
                        console.error("Erro ao fazer parse da resposta: ", e);
                        return;
                    }

                    var faturaDropdown = $('#ContentPlaceHolder2_faturaDropdown');
                    faturaDropdown.empty(); // Limpa o dropdown antes de preencher
                    faturaDropdown.append('<option value="">Selecione a Fatura</option>'); // Adiciona o placeholder

                    if (faturas && faturas.length > 0) {
                        // Adiciona as faturas no dropdown
                        faturas.forEach(function (fatura) {
                            faturaDropdown.append('<option value="' + fatura + '">' + fatura + '</option>');
                        });
                    } else {
                        faturaDropdown.append('<option value="">Nenhuma fatura agrupadora encontrada</option>');
                    }
                },
                error: function (error) {
                    console.error('Erro ao carregar as faturas agrupadoras: ', error);
                    $('#faturaDropdown').empty().append('<option value="">Erro ao carregar as faturas agrupadoras</option>');
                }
            });
        }

        // Resetar campos da direita (sempre que o modal for aberto)
        resetCamposDireita();

        // Carrega campos de acordo com o chamado selecionado
        loadFields();

        // Carrega campos condicionais
        showConditionalFields(tipoSolicitacao)

        // Chama a função para carregar os emails ao abrir o modal
        carregarFaturaAgrupadora(idConglomerado);

        carregarPlanos(idConglomerado);  // Substitua 'idConglomerado' pelo valor correto


        // Chama a função para carregar os emails ao abrir o modal
        if (!idConglomerado || idConglomerado === '0') {
            $('#emailDropdown').empty(); // Limpa o dropdown antes de exibir a mensagem
            $('#emailDropdown').append('<span>Emails não disponíveis para esse tipo de chamado.</span>');

            // Desabilitar o botão Enviar Email
            var btnEnviarEmail = document.getElementById('ContentPlaceHolder2_btnEnviarEmail');
            btnEnviarEmail.disabled = true; // Desativa o botão
            btnEnviarEmail.style.cursor = 'not-allowed'; // Altera o cursor para sinal de proibido
            btnEnviarEmail.title = "Emails não disponíveis para esse tipo de chamado."; // Exibe a mensagem ao passar o mouse

        } else {
            carregarEmailsOperadora(idConglomerado);

            // Habilitar o botão caso idConglomerado seja válido
            var btnEnviarEmail = document.getElementById('ContentPlaceHolder2_btnEnviarEmail');
            btnEnviarEmail.disabled = false; // Ativa o botão
            btnEnviarEmail.style.cursor = 'pointer'; // Volta o cursor ao normal
            btnEnviarEmail.title = ""; // Remove a mensagem
        }


        // Exibir o modal
        document.getElementById('backdropCustom').style.display = 'block';
        document.getElementById('chamadoModal').style.display = 'block';
    }

    //Fecha o modal
    function fecharModal() {
        document.getElementById('backdropCustom').style.display = 'none';
        document.getElementById('chamadoModal').style.display = 'none';
    }

    //Valida envio de emails
    function validarEnvioEmail() {
        // Obter a div com os e-mails selecionados
        var selectedEmailsDiv = document.getElementById('ContentPlaceHolder2_selectedEmails');
        var selectedEmails = selectedEmailsDiv ? selectedEmailsDiv.innerHTML.trim() : '';

        // Verificar se há um caractere '@' nos e-mails selecionados
        if (!selectedEmails.includes('@')) {
            alert('Por favor, selecione ao menos um e-mail válido antes de enviar.');
            return false;  // Impede o envio do formulário
        }

        // Caso os e-mails estejam preenchidos corretamente, permite o envio
        document.getElementById('ContentPlaceHolder1_hfEmailResponsavelRegional').value = document.getElementById('ContentPlaceHolder2_emailResponsavelRegional').value;

        return true;
    }

    //Abre dropdown dos emails
    function abrirDropdownEmails() {
        var dropdown = document.getElementById('emailDropdown');
        dropdown.style.display = 'block';
    }

    // Função para normalizar o nome do arquivo (remover caracteres especiais e acentos)
    function normalizarNomeArquivo(nome) {
        return nome.normalize("NFD").replace(/[\u0300-\u036f]/g, "").replace(/[^\w\s.-]/g, "");
    }

    // Adicionar novos arquivos e convertê-los em base64 para armazenar no campo oculto
    function adicionarArquivos() {
        var input = document.getElementById('anexos');
        var listaAnexos = document.getElementById('listaAnexos');
        var arquivos = input.files;

        // Recupera os arquivos existentes no campo oculto (se houver)
        var hfBase64FilesValue = document.getElementById('<%= hfBase64Files.ClientID %>').value;
    var base64Files = hfBase64FilesValue ? JSON.parse(hfBase64FilesValue) : [];

    // Exibe o loading spinner enquanto processa os arquivos
    document.getElementById('loadingSpinner').style.display = 'block';

    // Função para converter arquivo para base64
    function toBase64(file) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = (error) => reject(error);
        });
    }

    setTimeout(async function () {
        for (var i = 0; i < arquivos.length; i++) {
            var arquivo = arquivos[i];
            arquivosSelecionados.push(arquivo); // Adiciona à lista de arquivos selecionados
            var base64 = await toBase64(arquivo); // Converte o arquivo em base64
            var nomeNormalizado = normalizarNomeArquivo(arquivo.name);
            base64Files.push({ nome: nomeNormalizado, conteudo: base64 });

            var index = arquivosSelecionados.length - 1; // Índice do arquivo recém-adicionado
            var divArquivo = document.createElement('div');
            divArquivo.style.display = 'flex';
            divArquivo.style.alignItems = 'center';
            divArquivo.style.justifyContent = 'space-between';
            divArquivo.style.marginBottom = '5px';
            divArquivo.innerHTML = '<span>' + arquivo.name + '</span>' +
                '<button type="button" style="background: none; border: none; color: red; cursor: pointer;" onclick="removerArquivo(' + index + ')">X</button>';

            listaAnexos.appendChild(divArquivo);
        }

        // Armazena os arquivos base64 no campo oculto
        document.getElementById('<%= hfBase64Files.ClientID %>').value = JSON.stringify(base64Files);

        // Esconde o loading spinner
        document.getElementById('loadingSpinner').style.display = 'none';
    }, 500);
    }

    // Função para remover um arquivo da lista
    function removerArquivo(index) {
        // Remove o arquivo da lista de arquivos selecionados
        arquivosSelecionados.splice(index, 1);

        // Recupera os arquivos base64 do campo oculto
        var hfBase64FilesValue = document.getElementById('<%= hfBase64Files.ClientID %>').value;
    var base64Files = hfBase64FilesValue ? JSON.parse(hfBase64FilesValue) : [];

    // Remove o arquivo correspondente do campo oculto
    base64Files.splice(index, 1);

    // Atualiza o campo oculto com os arquivos restantes
    document.getElementById('<%= hfBase64Files.ClientID %>').value = JSON.stringify(base64Files);

        // Atualiza a lista exibida
        atualizarListaAnexos();
    }

    // Atualiza a exibição da lista de anexos após a remoção
    function atualizarListaAnexos() {
        var listaAnexos = document.getElementById('listaAnexos');
        listaAnexos.innerHTML = ''; // Limpa a lista antes de renderizar novamente
        arquivosSelecionados.forEach(function (arquivo, index) {
            var divArquivo = document.createElement('div');
            divArquivo.style.display = 'flex';
            divArquivo.style.alignItems = 'center';
            divArquivo.style.justifyContent = 'space-between';
            divArquivo.style.marginBottom = '5px';
            divArquivo.innerHTML = '<span>' + arquivo.name + '</span>' +
                '<button type="button" style="background: none; border: none; color: red; cursor: pointer;" onclick="removerArquivo(' + index + ')">X</button>';

            listaAnexos.appendChild(divArquivo);
        });
    }


    document.querySelectorAll('#emailDropdown input[type="checkbox"]').forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            var selectedEmailsDiv = document.getElementById('ContentPlaceHolder2_selectedEmails');
            var selectedEmailsHf = document.getElementById('ContentPlaceHolder1_hfEmailsSelecionados');
            var selectedEmails = [];
            document.querySelectorAll('#emailDropdown input[type="checkbox"]:checked').forEach(function (checkedBox) {
                selectedEmails.push(checkedBox.value);
            });
            selectedEmailsDiv.innerHTML = selectedEmails.join(', ');
            selectedEmailsHf.value = selectedEmails.join(', ');
        });
    });

    // Previne que o dropdown feche ao selecionar os emails
    document.getElementById('emailDropdown').addEventListener('click', function (e) {
        e.stopPropagation();
    });

    // Fecha o dropdown se clicar fora, mas não ao clicar dentro
    document.addEventListener('click', function (e) {
        if (!document.getElementById('operadoraEmails').contains(e.target) && !document.getElementById('emailDropdown').contains(e.target)) {
            document.getElementById('emailDropdown').style.display = 'none';
        }
    });


</script>

<!-- FIM PÁGINA -->
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="tbBotao" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="btVoltar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-arrow-left"></i>
            <br />
            <span>Voltar</span>
        </asp:LinkButton>
    </div>

    <!-- Modal ----------------------------------------------------------------------------------------------------->

    <!-- Backgrop do modal -->
    <div id="backdropCustom" class="modal-backdrop-custom" style="display: none;"></div>

    <!-- Modal -->
    <div id="chamadoModal" class="modal-custom" style="display: none;">
        <div class="modal-header">
            <div style="display:flex">
                <span class="modal-title" id="modalTipoSolicitacaoContainer">
                    <strong>Chamado:</strong> <span id="modalTipoSolicitacao" runat="server"></span>
                </span>
                <span id="modalEstadoContainer">
                    <span id="modalEstado" class='badge badge-pill badge-primary <%# GetBadgeClass(Eval("Estado")) %>' style="padding: 6px; border-radius: 6px;margin-left: 13px; margin-top: 4px;"></span>
                </span>
                <span id="modalIdChamadoContainer" runat="server" style="margin-top: 4px;margin-left: 5px;">
                    <i style="color:#777;font-size: 13px;">#<span id="modalIdChamado" runat="server"></span></i>
                </span>
            </div>
            <button type="button" class="close" onclick="fecharModal()" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>

        <div class="modal-body d-flex">
            <!-- Coluna Esquerda -->
            <div class="flex-fill left-column d-flex flex-column">

                <!-- Conteúdo Alinhado ao Topo -->
                <div class="content-top">
                    <p id="modalRequestNumberContainer">
                        <strong class="label-chamado">RequestNumber:</strong> <span id="modalRequestNumber"></span>
                    </p>
                    <p id="modalWorkOrderNumberContainer">
                        <strong class="label-chamado">Work Order Number:</strong> <span id="modalWorkOrderNumber"></span>
                    </p>
                    <p id="modalTransactionIDContainer">
                        <strong class="label-chamado">ID da transação:</strong> <span id="modalTransactionID"></span>
                    </p>
                    <br />
                    <p id="modalUserNameContainer">
                        <strong class="label-chamado">Usuário solicitante:</strong> <span id="modalUserName"></span>
                    </p>
                    <p id="modalNumeroLinhaContainer">
                        <strong class="label-chamado">Número da linha:</strong> <span id="modalNumeroLinha"></span>
                    </p>
                    <p id="modalOperadoraContainer">
                        <strong class="label-chamado">Operadora:</strong> <span id="modalOperadora"></span>
                    </p>
                    <p id="modalPlanoContratoAtualContainer">
                        <strong class="label-chamado">Plano atual:</strong> <span id="modalPlanoContratoAtual"></span>
                    </p>
                    <p id="modalComentariosContainer">
                        <strong class="label-chamado">Comentários:</strong> <span id="modalComentarios"></span>
                    </p>
                    <br />
                    <div id="camposCondicionaisContainer" runat="server"></div>
                </div>

                <!-- Botão Alinhado ao Final -->
                <div class="button-bottom mt-auto">
                    <asp:Button ID="btnExecutar" runat="server" CssClass="btn btn-primary verde" Text="Executar" OnClick="btnExecutar_Click" />
                </div>

            </div>


            <!-- Coluna Direita -->
            <div class="right-column" id="rightColumn" runat="server">
                <div class="form-group-row">
                    <div class="form-group">
                        <label for="empresaContratante">Empresa Contratante</label>
                        <asp:DropDownList ID="empresaContratante" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Selecione a Empresa" Value="" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="faturaDropdown">Fatura Agrupadora</label>
                        <select id="faturaDropdown" class="form-control" runat="server">
                            <option value="">Selecione a Fatura</option>
                        </select>
                    </div>
                </div>

                <div class="form-group">
                    <label for="corpoEmail">Corpo do Email</label>
                    <textarea id="corpoEmail" class="form-control" rows="2" runat="server"></textarea>
                </div>

                <div class="form-group">
                    <label for="operadoraEmails">Operadora Emails <span style="color:red;">*</span></label>
                    <input type="text" id="operadoraEmails" class="form-control" placeholder="Selecione emails" onclick="abrirDropdownEmails()" />
                    <div id="emailDropdown" class="dropdown-emails">
                        <!-- Os emails serão preenchidos aqui dinamicamente pelo JavaScript -->
                    </div>
                    <div id="selectedEmails" style="margin-top: 10px;" runat="server"></div>
                </div>

                <div class="form-group">
                    <label for="anexos">Anexos</label>
                    <div class="custom-file" onclick="document.getElementById('anexos').click()">
                        <span id="anexosText">Clique aqui para anexar arquivos</span>
                        <input type="file" id="anexos" class="form-control" multiple onchange="adicionarArquivos()" />
                    </div>
                    <div id="listaAnexos"></div>
                    <div id="loadingSpinner">
                        <img src="spinner.gif" alt="Carregando..." />
                    </div>
                </div>

                <!-- Campo oculto para armazenar os arquivos em base64 -->
                <asp:HiddenField ID="hfBase64Files" runat="server" />

                <!-- Grupo do campo de email e botão -->
                <div class="form-group d-flex align-items-center">
                    <div style="flex: 1; margin-right: 10px;">
                        <label for="emailResponsavelRegional">Email do Responsável na Regional</label>
                        <input type="text" id="emailResponsavelRegional" class="form-control" runat="server"/>
                    </div>
                    <asp:Button ID="btnEnviarEmail" runat="server" CssClass="btn btn-primary btn-send-mail" 
                        Text="Enviar Email" OnClick="btnEnviarEmail_Click" 
                        OnClientClick="return validarEnvioEmail();" />
                </div>
            </div>


        </div>


    </div>




    <!-- FIM CÓDIGOS ADICIONAIS -->
</asp:Content>