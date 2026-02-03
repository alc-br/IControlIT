
<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Cadastro.aspx.vb" Inherits="IControlIT.Parametros_Cadastro" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>

    <script type="text/javascript">
        (function () {
            function tornarOnLoadSeguro() {
                var handlerAtual = window.onload;
                if (typeof handlerAtual === 'function' && handlerAtual !== carregamentoSeguro) {
                    carregamentoSeguro.handlerOriginal = handlerAtual;
                    window.onload = carregamentoSeguro;
                }
            }

            function carregamentoSeguro(evento) {
                if (typeof carregamentoSeguro.handlerOriginal === 'function') {
                    if (document.getElementById('Hdf_LarguraTela')) {
                        carregamentoSeguro.handlerOriginal.call(window, evento);
                    } else if (window.console && typeof window.console.warn === 'function') {
                        console.warn('DEBUG: Ignorando execucao de TamanhoTela porque "Hdf_LarguraTela" nao esta presente nesta pagina.');
                    }
                }
            }

            tornarOnLoadSeguro();
            setTimeout(tornarOnLoadSeguro, 0);
        })();

        $(function () {
            try {
                $('#<%= txtMesEmissao.ClientID %>').mask('00/0000');
                $('#<%= txtCNPJ_Anima.ClientID %>').mask('00.000.000/0000-00', { reverse: true });
                $('#<%= txtSupplierSiteName.ClientID %>').mask('00.000.000/0000-00', { reverse: true });
            } catch (e) {
                console.error('DEBUG: Erro ao aplicar mascaras jQuery.', e);
            }

            // Validacao de CNPJ no blur
            $('#<%= txtCNPJ_Anima.ClientID %>').on('blur', function () {
                var cnpj = $(this).val();
                var feedbackElement = $('#cnpjFeedback');
                if (cnpj.length > 0 && !validarCNPJ(cnpj)) {
                    $(this).css('border-color', 'red');
                    feedbackElement.text('CNPJ invalido.').css('color', 'red');
                } else {
                    $(this).css('border-color', '#ced4da');
                    feedbackElement.text('');
                }
            });

            // Validacao de email no blur
            var emailFields = [
                '#<%= txtPreparerEmail.ClientID %>',
                '#<%= txtApproverEmail.ClientID %>',
                '#<%= txtRequesterEmail.ClientID %>'
            ];

            emailFields.forEach(function(fieldId) {
                $(fieldId).on('blur', function() {
                    var email = $(this).val().trim();
                    if (email.length > 0 && !validarEmail(email)) {
                        $(this).css('border-color', 'red');
                    } else {
                        $(this).css('border-color', '#ced4da');
                    }
                });
            });

            // Sync checkbox ativo
            var chkAtivo = document.getElementById('<%= chkAtivo.ClientID %>');
            var hiddenAtivo = document.getElementById('<%= hfChkAtivo.ClientID %>');
            if (chkAtivo && hiddenAtivo) {
                var syncHidden = function () {
                    hiddenAtivo.value = chkAtivo.checked ? 'true' : 'false';
                };
                chkAtivo.addEventListener('change', syncHidden);
                syncHidden();
            }
        });

        function validarEmail(email) {
            var regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return regex.test(email);
        }

        function validarCNPJ(cnpj) {
            cnpj = cnpj.replace(/[^\d]+/g, '');
            if (cnpj === '' || cnpj.length !== 14 || /(\d)\1{13}/.test(cnpj)) {
                return false;
            }

            let tamanho = cnpj.length - 2;
            let numeros = cnpj.substring(0, tamanho);
            let digitos = cnpj.substring(tamanho);
            let soma = 0;
            let pos = tamanho - 7;

            for (let i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2) pos = 9;
            }

            let resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != digitos.charAt(0)) {
                return false;
            }

            tamanho = tamanho + 1;
            numeros = cnpj.substring(0, tamanho);
            soma = 0;
            pos = tamanho - 7;

            for (let i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2) pos = 9;
            }

            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            return resultado == digitos.charAt(1);
        }

        function handleDropdownOutro(ddl) {
            if (ddl.value === 'Outro') {
                var novoValor = prompt('Por favor, informe o novo valor:');
                if (novoValor && novoValor.trim() !== '') {
                    novoValor = novoValor.trim();
                    var opt = new Option(novoValor, novoValor, true, true);
                    ddl.add(opt);
                    // Salvar o valor customizado no hidden field correspondente
                    var ddlId = ddl.id;
                    if (ddlId.indexOf('ddlDocumentStatusCode') >= 0) {
                        document.getElementById('<%= hfCustomDocumentStatusCode.ClientID %>').value = novoValor;
                    } else if (ddlId.indexOf('ddlCategoryName') >= 0) {
                        document.getElementById('<%= hfCustomCategoryName.ClientID %>').value = novoValor;
                    } else if (ddlId.indexOf('ddlSupplierName') >= 0) {
                        document.getElementById('<%= hfCustomSupplierName.ClientID %>').value = novoValor;
                    }
                } else {
                    ddl.selectedIndex = 0;
                }
            }
        }

        function validarFormulario() {
            console.log('validarFormulario() iniciada');
            try {
                // Validar CNPJ
                const cnpjInput = document.getElementById('<%= txtCNPJ_Anima.ClientID %>');
                if (!cnpjInput) {
                    console.error('Campo CNPJ nao encontrado');
                    return false;
                }
                if (cnpjInput.value.trim() !== '' && !validarCNPJ(cnpjInput.value)) {
                    alert('O CNPJ da Anima informado e invalido. Por favor, verifique.');
                    cnpjInput.focus();
                    return false;
                }

                // Validar emails
                var emailFields = [
                    { id: '<%= txtPreparerEmail.ClientID %>', name: 'Email preparer' },
                    { id: '<%= txtApproverEmail.ClientID %>', name: 'Email approver' },
                    { id: '<%= txtRequesterEmail.ClientID %>', name: 'Email requester' }
                ];

                for (var i = 0; i < emailFields.length; i++) {
                    var emailField = document.getElementById(emailFields[i].id);
                    if (emailField && emailField.value.trim() !== '' && !validarEmail(emailField.value.trim())) {
                        alert('O campo "' + emailFields[i].name + '" contem um email invalido.');
                        emailField.focus();
                        return false;
                    }
                }

                // Validar campos obrigatorios
                const camposObrigatorios = [
                    '<%= ddlTipo.ClientID %>', '<%= txtMesEmissao.ClientID %>', '<%= ddlDocumentStatusCode.ClientID %>',
                    '<%= txtDescription.ClientID %>', '<%= txtJustification.ClientID %>', '<%= txtConta.ClientID %>',
                    '<%= txtDescricaoServico.ClientID %>', '<%= ddlCategoryName.ClientID %>', '<%= txtItemNumber.ClientID %>',
                    '<%= txtItemDescription.ClientID %>', '<%= txtCNPJ_Anima.ClientID %>', '<%= ddlSupplierName.ClientID %>',
                    '<%= txtSupplierSiteName.ClientID %>', '<%= txtPreparerEmail.ClientID %>', '<%= txtApproverEmail.ClientID %>',
                    '<%= txtRequesterEmail.ClientID %>', '<%= txtRequisitioningBUName.ClientID %>', '<%= txtRequisitioningBUId.ClientID %>',
                    '<%= txtProcurementBUName.ClientID %>', '<%= txtDeliverToLocationCode.ClientID %>', '<%= txtCentroDeCusto.ClientID %>',
                    '<%= txtEstabelecimento.ClientID %>', '<%= txtUnidadeNegocio.ClientID %>'
                ];

                for (let i = 0; i < camposObrigatorios.length; i++) {
                    const campoId = camposObrigatorios[i];
                    const campo = document.getElementById(campoId);
                    if (!campo) {
                        console.error('Campo obrigatorio nao encontrado: ' + campoId);
                        alert('Erro de configuracao: Campo obrigatorio nao encontrado. Contate o suporte.');
                        return false;
                    }
                    if (!campo.value || campo.value.trim() === '') {
                        let labelText = 'Campo desconhecido';
                        const label = document.querySelector('label[for="' + campo.id + '"]');
                        if (label) {
                            labelText = label.innerText.replace('*', '').trim();
                        }
                        console.log('Campo vazio: ' + campoId + ' (' + labelText + ')');
                        alert('O campo "' + labelText + '" e obrigatorio.');
                        campo.focus();
                        return false;
                    }
                }

                // Validar que nenhum dropdown esta com "Outro" selecionado
                var dropdownsComOutro = [
                    { id: '<%= ddlDocumentStatusCode.ClientID %>', name: 'Status' },
                    { id: '<%= ddlCategoryName.ClientID %>', name: 'Categoria' },
                    { id: '<%= ddlSupplierName.ClientID %>', name: 'Fornecedor' }
                ];
                for (var d = 0; d < dropdownsComOutro.length; d++) {
                    var ddl = document.getElementById(dropdownsComOutro[d].id);
                    if (ddl && ddl.value === 'Outro') {
                        alert('Por favor, informe um valor para o campo "' + dropdownsComOutro[d].name + '" ou selecione uma opcao valida.');
                        ddl.focus();
                        return false;
                    }
                }

                // Remover mascara dos CNPJs antes de enviar
                cnpjInput.value = cnpjInput.value.replace(/[^\d]+/g, '');
                var supplierSiteField = document.getElementById('<%= txtSupplierSiteName.ClientID %>');
                if (supplierSiteField) {
                    supplierSiteField.value = supplierSiteField.value.replace(/[^\d]+/g, '');
                }
                console.log('validarFormulario() - validacao OK, retornando true');
                return true;
            } catch (err) {
                console.error('DEBUG: ERRO CRITICO DENTRO DE validarFormulario(): ', err);
                alert('Ocorreu um erro de script. Verifique o console (F12).');
                return false;
            }
        }
    </script>

    <style>
        #divTitulo,
        #divLocalizar {
            display: none !important;
        }

        .cadastro-wrapper {
            padding: 0;
            background-color: transparent;
            min-height: calc(100vh - 120px);
        }

        .cadastro-card,
        .historico-panel {
            width: 100%;
            margin: 0;
            margin-bottom: 24px;
            background: #fff;
            border-radius: 10px;
            border: 1px solid #e6ebf1;
            box-shadow: 0 8px 18px rgba(15, 34, 58, 0.08);
        }

        .cadastro-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            gap: 16px;
            padding: 24px;
            border-bottom: 1px solid #edf0f5;
        }

        .cadastro-title {
            display: block;
            font-size: 22px;
            font-weight: 600;
            color: #1f2d3d;
            margin: 0;
        }

        .cadastro-subtitle {
            margin: 6px 0 0;
            font-size: 13px;
            color: #6c7a91;
        }

        .link-voltar {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            padding: 10px 18px;
            border-radius: 6px;
            background: #f1f5f9;
            color: #1f2937 !important;
            font-size: 13px;
            font-weight: 600;
            text-decoration: none;
            transition: background 0.2s ease;
        }

        .link-voltar:hover {
            background: #e2e8f0;
        }

        .cadastro-body {
            padding: 24px;
        }

        .section-title {
            margin: 0 0 16px;
            font-size: 16px;
            font-weight: 600;
            color: #1f2d3d;
        }

        .form-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(260px, 1fr));
            gap: 18px;
            margin-bottom: 24px;
        }

        .form-grid .full-width {
            grid-column: 1 / -1;
        }

        .form-group label {
            display: block;
            margin-bottom: 6px;
            font-size: 12px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.04em;
            color: #6c7a91;
        }

        .form-control,
        .form-select {
            width: 100%;
            border: 1px solid #d8dee9;
            border-radius: 6px;
            padding: 10px 12px;
            font-size: 14px;
            color: #1f2d3d;
            background-color: #fff;
            transition: border-color 0.2s ease, box-shadow 0.2s ease;
            height: 46px;
            line-height: 24px;
        }

        .form-select {
            padding-right: 40px;
            background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 4 5'%3E%3Cpath fill='%236c7a91' d='M2 0L0 2h4zM2 5L0 3h4z'/%3E%3C/svg%3E");
            background-repeat: no-repeat;
            background-position: right 12px center;
            background-size: 12px 12px;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
        }

        textarea.form-control {
            height: auto;
            line-height: normal;
        }

        .form-control:focus,
        .form-select:focus {
            outline: none;
            border-color: #2563eb;
            box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.12);
        }

        .checkbox-row {
            display: flex;
            align-items: center;
            gap: 10px;
            margin-top: 8px;
        }

        .form-actions {
            display: flex;
            justify-content: flex-end;
            gap: 12px;
            padding-top: 16px;
            border-top: 1px solid #edf0f5;
        }

        .btn-primary {
            border: none;
            border-radius: 6px;
            padding: 10px 20px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            background: linear-gradient(90deg, #2563eb 0%, #1d4ed8 100%);
            color: #fff;
            box-shadow: 0 4px 12px rgba(37, 99, 235, 0.25);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .btn-primary:hover {
            transform: translateY(-1px);
            box-shadow: 0 6px 16px rgba(37, 99, 235, 0.3);
        }

        .btn-danger {
            border: none;
            border-radius: 6px;
            padding: 10px 20px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            background: linear-gradient(90deg, #dc2626 0%, #b91c1c 100%);
            color: #fff;
            box-shadow: 0 4px 12px rgba(220, 38, 38, 0.25);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .btn-danger:hover {
            transform: translateY(-1px);
            box-shadow: 0 6px 16px rgba(220, 38, 38, 0.35);
        }

        .btn-secondary {
            border: 1px solid #d1d5db;
            border-radius: 6px;
            padding: 10px 20px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            background: #fff;
            color: #374151;
            transition: background 0.2s ease, border-color 0.2s ease;
        }

        .btn-secondary:hover {
            background: #f3f4f6;
            border-color: #9ca3af;
        }

        /* Modal de Historico */
        .modal-overlay {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.5);
            z-index: 9999;
            justify-content: center;
            align-items: flex-start;
            padding: 40px 20px;
            overflow-y: auto;
        }

        .modal-overlay.active {
            display: flex;
        }

        .modal-content {
            background: #fff;
            border-radius: 12px;
            width: 100%;
            max-width: 900px;
            max-height: calc(100vh - 80px);
            overflow-y: auto;
            box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
        }

        .modal-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 20px 24px;
            border-bottom: 1px solid #e5e7eb;
            position: sticky;
            top: 0;
            background: #fff;
            z-index: 1;
        }

        .modal-header h2 {
            margin: 0;
            font-size: 18px;
            font-weight: 600;
            color: #1f2d3d;
        }

        .modal-close {
            background: none;
            border: none;
            font-size: 24px;
            color: #6b7280;
            cursor: pointer;
            padding: 0;
            line-height: 1;
        }

        .modal-close:hover {
            color: #1f2d3d;
        }

        .modal-body {
            padding: 24px;
        }

        .modal-tip {
            margin: 0 0 20px;
            font-size: 13px;
            color: #64748b;
        }

        .historico-panel h2 {
            margin: 0;
            padding: 24px;
            border-bottom: 1px solid #edf0f5;
            font-size: 18px;
            color: #1f2d3d;
        }

        .historico-tip {
            margin: 0;
            padding: 12px 24px 0;
            font-size: 13px;
            color: #64748b;
        }

        .historico-list {
            padding: 20px 24px 24px;
            display: flex;
            flex-direction: column;
            gap: 18px;
        }

        .historico-item {
            border: 1px solid #e2e8f0;
            border-radius: 10px;
            background: #fff;
            overflow: hidden;
            box-shadow: 0 6px 16px rgba(15, 34, 58, 0.06);
        }

        .historico-headline {
            display: flex;
            flex-wrap: wrap;
            gap: 12px;
            align-items: center;
            padding: 16px 20px;
            background: #f8fafc;
            border-bottom: 1px solid #edf0f5;
        }

        .historico-headline span {
            font-size: 13px;
            color: #475569;
        }

        .historico-versao {
            font-weight: 700;
            color: #1f2937;
            letter-spacing: 0.02em;
        }

        .historico-acao {
            font-weight: 600;
            color: #2563eb;
            text-transform: uppercase;
            letter-spacing: 0.04em;
        }

        .historico-data {
            color: #0f172a;
        }

        .historico-table {
            width: 100%;
            border-collapse: collapse;
        }

        .historico-table th {
            width: 260px;
            padding: 10px 16px;
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.05em;
            color: #64748b;
            background: #f9fafb;
            border-bottom: 1px solid #edf0f5;
            text-align: left;
            vertical-align: top;
        }

        .historico-table td {
            padding: 10px 16px;
            font-size: 13px;
            color: #1f2937;
            border-bottom: 1px solid #edf0f5;
            white-space: pre-line;
        }

        .historico-table tr:last-child th,
        .historico-table tr:last-child td {
            border-bottom: none;
        }

        .historico-table tr.campo-alterado th,
        .historico-table tr.campo-alterado td {
            background: #fff7ed;
            color: #9a3412;
            font-weight: 600;
        }

        .cnpj-feedback {
            font-size: 12px;
            margin-top: 4px;
            display: block;
        }

        @media (max-width: 768px) {
            .cadastro-header {
                flex-direction: column;
                align-items: flex-start;
            }

            .form-actions {
                flex-direction: column;
                align-items: stretch;
            }

            .historico-headline {
                flex-direction: column;
                align-items: flex-start;
                gap: 6px;
            }

            .historico-table th,
            .historico-table td {
                display: block;
                width: 100%;
            }

            .historico-table th {
                border-bottom: none;
                padding-bottom: 4px;
            }

            .historico-table td {
                border-top: 1px solid #edf0f5;
            }
        }
    </style>

    <div class="cadastro-wrapper">
        <asp:HiddenField ID="hfIdParametro" runat="server" />
        <asp:HiddenField ID="hfCodigoReferencia" runat="server" />
        <asp:HiddenField ID="hfPreparerEmail" runat="server" />
        <asp:HiddenField ID="hfApproverEmail" runat="server" />
        <asp:HiddenField ID="hfRequesterEmail" runat="server" />
        <asp:HiddenField ID="hfChkAtivo" runat="server" />
        <asp:HiddenField ID="hfCustomDocumentStatusCode" runat="server" />
        <asp:HiddenField ID="hfCustomCategoryName" runat="server" />
        <asp:HiddenField ID="hfCustomSupplierName" runat="server" />

        <div class="cadastro-card">
            <div class="cadastro-header">
                <div>
                    <asp:Label ID="lblTitulo" runat="server" Text="Novo Parametro Anima" CssClass="cadastro-title"></asp:Label>
                    <p class="cadastro-subtitle">Preencha os campos obrigatorios (*) para salvar o parametro.</p>
                </div>
                <a class="link-voltar" href="Consulta.aspx">Voltar para consulta</a>
            </div>

            <div class="cadastro-body">
                <h2 class="section-title">Dados do parametro</h2>
                <div class="form-grid">
                    <div class="form-group">
                        <label for="<%= ddlTipo.ClientID %>">Tipo *</label>
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtCNPJ_Anima.ClientID %>">CNPJ Anima *</label>
                        <asp:TextBox ID="txtCNPJ_Anima" runat="server" CssClass="form-control" MaxLength="18" placeholder="00.000.000/0000-00"></asp:TextBox>
                        <span id="cnpjFeedback" class="cnpj-feedback"></span>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtConta.ClientID %>">Conta *</label>
                        <asp:TextBox ID="txtConta" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="form-group full-width">
                        <label for="<%= txtDescricaoServico.ClientID %>">Descricao servico *</label>
                        <asp:TextBox ID="txtDescricaoServico" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtMesEmissao.ClientID %>">Mes emissao *</label>
                        <asp:TextBox ID="txtMesEmissao" runat="server" CssClass="form-control" MaxLength="7" placeholder="MM/AAAA"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtRequisitioningBUId.ClientID %>">Requisitioning BU id *</label>
                        <asp:TextBox ID="txtRequisitioningBUId" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtRequisitioningBUName.ClientID %>">Requisitioning BU name *</label>
                        <asp:TextBox ID="txtRequisitioningBUName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group full-width">
                        <label for="<%= txtDescription.ClientID %>">Descricao geral *</label>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                    <div class="form-group full-width">
                        <label for="<%= txtJustification.ClientID %>">Justificativa *</label>
                        <asp:TextBox ID="txtJustification" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtPreparerEmail.ClientID %>">Email preparer *</label>
                        <asp:TextBox ID="txtPreparerEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtApproverEmail.ClientID %>">Email approver *</label>
                        <asp:TextBox ID="txtApproverEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= ddlDocumentStatusCode.ClientID %>">Status documento *</label>
                        <asp:DropDownList ID="ddlDocumentStatusCode" runat="server" CssClass="form-select" onchange="handleDropdownOutro(this)"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtRequisitionType.ClientID %>">Tipo requisicao</label>
                        <asp:TextBox ID="txtRequisitionType" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtSourceUniqueId.ClientID %>">Source unique id</label>
                        <asp:TextBox ID="txtSourceUniqueId" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= ddlCategoryName.ClientID %>">Categoria *</label>
                        <asp:DropDownList ID="ddlCategoryName" runat="server" CssClass="form-select" onchange="handleDropdownOutro(this)"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtDeliverToLocationCode.ClientID %>">Deliver to location code *</label>
                        <asp:TextBox ID="txtDeliverToLocationCode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtDeliverToOrganizationCode.ClientID %>">Deliver to organization code</label>
                        <asp:TextBox ID="txtDeliverToOrganizationCode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtProcurementBUName.ClientID %>">Procurement BU name *</label>
                        <asp:TextBox ID="txtProcurementBUName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group full-width">
                        <label for="<%= txtItemDescription.ClientID %>">Descricao item *</label>
                        <asp:TextBox ID="txtItemDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtItemNumber.ClientID %>">Numero item *</label>
                        <asp:TextBox ID="txtItemNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtRequesterEmail.ClientID %>">Email requester *</label>
                        <asp:TextBox ID="txtRequesterEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= ddlSupplierName.ClientID %>">Fornecedor *</label>
                        <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-select" onchange="handleDropdownOutro(this)"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtSupplierContactName.ClientID %>">Contato fornecedor</label>
                        <asp:TextBox ID="txtSupplierContactName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtSupplierSiteName.ClientID %>">Site fornecedor (CNPJ) *</label>
                        <asp:TextBox ID="txtSupplierSiteName" runat="server" CssClass="form-control" MaxLength="18" placeholder="00.000.000/0000-00"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtCentroDeCusto.ClientID %>">Centro de custo *</label>
                        <asp:TextBox ID="txtCentroDeCusto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtEstabelecimento.ClientID %>">Estabelecimento *</label>
                        <asp:TextBox ID="txtEstabelecimento" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtUnidadeNegocio.ClientID %>">Unidade negocio *</label>
                        <asp:TextBox ID="txtUnidadeNegocio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtFinalidade.ClientID %>">Finalidade</label>
                        <asp:TextBox ID="txtFinalidade" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtProjeto.ClientID %>">Projeto</label>
                        <asp:TextBox ID="txtProjeto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtInterCompany.ClientID %>">Inter Company</label>
                        <asp:TextBox ID="txtInterCompany" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <h2 class="section-title" style="margin-top: 24px;">Retorno do robo</h2>
                <div class="form-grid">
                    <div class="form-group">
                        <label for="<%= txtCodigoRequisicaoCompra.ClientID %>">Requisicao de Compra (Oracle)</label>
                        <asp:TextBox ID="txtCodigoRequisicaoCompra" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtCodigoOrdemCompra.ClientID %>">Ordem de Compra (Oracle)</label>
                        <asp:TextBox ID="txtCodigoOrdemCompra" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtCodigoInvoice.ClientID %>">ID Processo (V360)</label>
                        <asp:TextBox ID="txtCodigoInvoice" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group full-width">
                        <label for="<%= txtObservacao.ClientID %>">Observacao</label>
                        <asp:TextBox ID="txtObservacao" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                </div>

                <%-- Checkbox "Parametro ativo" removido - parametro sempre ativo por padrao --%>
                <asp:CheckBox ID="chkAtivo" runat="server" Visible="false" />
                <div class="checkbox-row">
                    <asp:CheckBox ID="chkProcessamentoManual" runat="server" Text="Processamento Manual" />
                    <asp:HiddenField ID="hfChkProcessamentoManual" runat="server" />
                </div>

                <div class="form-actions">
                    <asp:Button ID="btnApagar" runat="server" Text="Apagar parametro" CssClass="btn-danger" OnClick="BtnApagar_Click" Visible="false" OnClientClick="return confirm('Tem certeza que deseja apagar este parametro? Esta acao removera tambÃ©m o historico.');" />
                    <button type="button" id="btnHistorico" class="btn-secondary" onclick="abrirModalHistorico()" style="display:none;">Historico</button>
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar parametro" CssClass="btn-primary" OnClick="BtnSalvar_Click" OnClientClick="return validarFormulario();" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal de Historico -->
    <div id="modalHistorico" class="modal-overlay">
        <div class="modal-content">
            <div class="modal-header">
                <h2>Historico de alteracoes</h2>
                <button type="button" class="modal-close" onclick="fecharModalHistorico()">&times;</button>
            </div>
            <div class="modal-body">
                <p class="modal-tip">Versao 1 exibe o registro original; as versoes seguintes listam apenas os campos alterados na respectiva atualizacao. Linhas em destaque indicam onde ocorreu a mudanca.</p>
                <asp:Panel ID="divHistorico" runat="server" Visible="false">
                    <div class="historico-list">
                        <asp:Repeater ID="rptHistorico" runat="server" OnItemDataBound="rptHistorico_ItemDataBound">
                            <ItemTemplate>
                                <div class="historico-item">
                                    <div class="historico-headline">
                                        <span class="historico-versao"><%# Server.HtmlEncode(Convert.ToString(Eval("NumeroVersaoFormatado"))) %></span>
                                        <span class="historico-acao"><%# Server.HtmlEncode(Convert.ToString(Eval("AcaoDescricao"))) %></span>
                                        <span class="historico-data"><%# Server.HtmlEncode(Convert.ToString(Eval("DataFormatada"))) %></span>
                                        <span class="historico-usuario"><%# Server.HtmlEncode(Convert.ToString(Eval("Usuario"))) %></span>
                                    </div>
                                    <table class="historico-table">
                                        <tbody>
                                            <asp:Repeater ID="rptCampos" runat="server">
                                                <ItemTemplate>
                                                    <tr class="<%# Server.HtmlEncode(Convert.ToString(Eval("CssClass"))) %>">
                                                        <th><%# Server.HtmlEncode(Convert.ToString(Eval("Nome"))) %></th>
                                                        <td><%# Server.HtmlEncode(Convert.ToString(Eval("Valor"))) %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function abrirModalHistorico() {
            document.getElementById('modalHistorico').classList.add('active');
            document.body.style.overflow = 'hidden';
        }

        function fecharModalHistorico() {
            document.getElementById('modalHistorico').classList.remove('active');
            document.body.style.overflow = '';
        }

        // Fechar modal ao clicar fora
        document.getElementById('modalHistorico').addEventListener('click', function(e) {
            if (e.target === this) {
                fecharModalHistorico();
            }
        });

        // Fechar modal com ESC
        document.addEventListener('keydown', function(e) {
            if (e.key === 'Escape') {
                fecharModalHistorico();
            }
        });
    </script>
</asp:Content>

