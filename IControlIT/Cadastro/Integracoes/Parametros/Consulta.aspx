<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Consulta.aspx.vb" Inherits="IControlIT.Parametros_Consulta" ResponseEncoding="UTF-8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <meta charset="UTF-8" />
    <style>
        /*TESTE - INICIO
        .main-panel{width:100%}
        #sidebar{display:none}
        /*TESTE - FIM*/

        #divTitulo,
        #divLocalizar {
            display: none !important;
        }

        .consulta-wrapper {
            padding: 0;
            background-color: transparent;
            min-height: calc(100vh - 120px);
        }

        .consulta-container {
            margin: 0 auto;
        }

        .card {
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 8px 18px rgba(15, 34, 58, 0.08);
            border: 1px solid #e6ebf1;
            margin-bottom: 20px;
        }

        .card-header {
            padding: 20px 24px;
            border-bottom: 1px solid #edf0f5;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .card-header h1 {
            font-size: 22px;
            font-weight: 600;
            color: #1f2d3d;
            margin: 0;
        }

        .card-header p {
            margin: 4px 0 0;
            font-size: 13px;
            color: #6c7a91;
        }

        .btn-novo {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            padding: 8px 16px;
            background: linear-gradient(90deg, #2563eb 0%, #1d4ed8 100%);
            color: #fff !important;
            border-radius: 6px;
            font-size: 13px;
            font-weight: 600;
            box-shadow: 0 4px 12px rgba(37, 99, 235, 0.25);
            text-decoration: none;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .btn-novo svg {
            width: 16px;
            height: 16px;
        }

        .btn-novo:hover {
            transform: translateY(-1px);
            box-shadow: 0 6px 16px rgba(37, 99, 235, 0.3);
        }

        .search-card .card-body {
            padding: 20px 24px;
        }


        .main-panel > .content{
            margin-top: 0px!important;
        }
        .main-panel > div:first-of-type{
            display: none;
        }

        .filter-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
            gap: 16px;
        }

        .form-control-custom,
        .form-select-custom {
            width: 100%;
            border: 1px solid #d8dee9;
            border-radius: 6px;
            padding: 10px 12px;
            font-size: 14px;
            color: #1f2d3d;
            background-color: #fff;
            transition: border-color 0.2s ease;
        }

        .form-control-custom:focus,
        .form-select-custom:focus {
            outline: none;
            border-color: #2563eb;
            box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.12);
        }

        .form-label {
            display: block;
            margin-bottom: 6px;
            font-size: 12px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.04em;
            color: #6c7a91;
        }

        .search-actions {
            margin-top: 20px;
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            justify-content: flex-end;
        }

        .btn-primary-custom,
        .btn-outline {
            border: none;
            border-radius: 6px;
            padding: 10px 18px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.2s ease;
        }

        .btn-primary-custom {
            background: linear-gradient(90deg, #2563eb 0%, #1d4ed8 100%);
            color: #fff;
        }

        .btn-primary-custom:hover {
            box-shadow: 0 6px 16px rgba(37, 99, 235, 0.3);
            transform: translateY(-1px);
        }

        .btn-outline {
            background: #fff;
            color: #1f2d3d;
            border: 1px solid #cbd5e1;
        }

        .btn-outline:hover {
            background: #f1f5f9;
        }

        .grid-container {
            overflow: hidden;
        }

        .grid-table-wrapper {
            overflow-x: auto;
        }

        .gridview-modern {
            width: 100%;
            border-collapse: collapse;
        }

        .gridview-modern thead tr {
            background: #f8fafc;
        }

        .gridview-modern th,
        .gridview-modern td {
            padding: 14px 18px;
            font-size: 13px;
            color: #1f2d3d;
            border-bottom: 1px solid #edf1f7;
            white-space: nowrap;
        }

        .gridview-modern th {
            text-transform: uppercase;
            font-weight: 600;
            letter-spacing: 0.03em;
            color: #64748b;
            text-align: left;
        }

        .gridview-modern th.sortable {
            cursor: pointer;
            user-select: none;
            transition: background 0.2s ease;
        }

        .gridview-modern th.sortable:hover {
            background: #e2e8f0;
        }

        .gridview-modern th .sort-icon {
            display: inline-block;
            margin-left: 6px;
            font-size: 10px;
            color: #94a3b8;
        }

        .gridview-modern th.sort-asc .sort-icon::after {
            content: "▲";
            color: #2563eb;
        }

        .gridview-modern th.sort-desc .sort-icon::after {
            content: "▼";
            color: #2563eb;
        }

        .gridview-modern th:not(.sort-asc):not(.sort-desc) .sort-icon::after {
            content: "⇅";
        }

        .sort-header {
            color: #64748b;
            text-decoration: none;
            display: flex;
            align-items: center;
            gap: 4px;
        }

        .sort-header:hover {
            color: #2563eb;
            text-decoration: none;
        }

        .gridview-modern tr:hover td {
            background: #f8fbff;
        }

        .grid-actions-column {
            text-align: right;
            width: 80px;
        }

        .grid-actions-header {
            text-align: right;
        }

        .grid-action-icon {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            width: 32px;
            height: 32px;
            border-radius: 6px;
            background: #f1f5f9;
            color: #1d4ed8;
            text-decoration: none;
            transition: background 0.2s ease, color 0.2s ease;
        }

        .grid-action-icon:hover {
            background: #1d4ed8;
            color: #fff;
        }

        .grid-action-icon svg {
            width: 16px;
            height: 16px;
            fill: currentColor;
        }

        .grid-action-icon-view {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            width: 32px;
            height: 32px;
            border-radius: 6px;
            background: #f1f5f9;
            color: #64748b;
            text-decoration: none;
            transition: background 0.2s ease, color 0.2s ease;
            margin-right: 6px;
            cursor: pointer;
            border: none;
        }

        .grid-action-icon-view:hover {
            background: #64748b;
            color: #fff;
        }

        .grid-action-icon-view svg {
            width: 16px;
            height: 16px;
            fill: currentColor;
        }

        /* Modal Styles */
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
            align-items: center;
        }

        .modal-overlay.active {
            display: flex;
        }

        .modal-content {
            background: #fff;
            border-radius: 12px;
            width: 95%;
            max-width: 1200px;
            max-height: 90vh;
            overflow: hidden;
            box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
        }

        .modal-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 20px 24px;
            border-bottom: 1px solid #e5e7eb;
            background: #f8fafc;
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
            cursor: pointer;
            color: #64748b;
            padding: 4px 8px;
            border-radius: 4px;
            transition: background 0.2s ease;
        }

        .modal-close:hover {
            background: #e5e7eb;
            color: #1f2d3d;
        }

        .modal-body {
            padding: 24px;
            overflow-y: auto;
            max-height: calc(90vh - 260px);
        }

        .detail-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
        }

        .detail-table thead tr {
            background: #f1f5f9;
        }

        .detail-table th,
        .detail-table td {
            padding: 10px 12px;
            border: 1px solid #e5e7eb;
            text-align: left;
            white-space: nowrap;
        }

        .detail-table th {
            font-weight: 600;
            color: #475569;
            text-transform: uppercase;
            font-size: 11px;
            letter-spacing: 0.03em;
        }

        .detail-table tbody tr:nth-child(even) {
            background: #f8fafc;
        }

        .detail-table tbody tr:hover {
            background: #e0f2fe;
        }

        .detail-table tfoot tr {
            background: #1e3a5f;
            color: #fff;
            font-weight: 600;
        }

        .detail-table tfoot td {
            border-color: #1e3a5f;
        }

        .detail-table-wrapper {
            overflow-x: auto;
        }

        .loading-spinner {
            text-align: center;
            padding: 40px;
            color: #64748b;
        }

        .no-data-message {
            text-align: center;
            padding: 40px;
            color: #64748b;
            font-size: 14px;
        }

        /* Modal Header Actions */
        .modal-header-actions {
            display: flex;
            align-items: center;
            gap: 12px;
        }

        .btn-exportar-csv {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            padding: 8px 16px;
            background: linear-gradient(90deg, #059669 0%, #047857 100%);
            color: #fff;
            border: none;
            border-radius: 6px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            box-shadow: 0 4px 12px rgba(5, 150, 105, 0.25);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .btn-exportar-csv:hover {
            transform: translateY(-1px);
            box-shadow: 0 6px 16px rgba(5, 150, 105, 0.3);
        }

        /* Modal Toolbar */
        .modal-toolbar {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 12px 24px;
            background: #f1f5f9;
            border-bottom: 1px solid #e5e7eb;
        }

        .modal-toolbar-left {
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .modal-toolbar-right {
            display: flex;
            align-items: center;
        }

        .toolbar-label {
            font-size: 13px;
            font-weight: 500;
            color: #475569;
        }

        .linhas-select {
            width: 90px;
            border: 1px solid #cbd5e1;
            border-radius: 6px;
            padding: 6px 10px;
            font-size: 13px;
            color: #1f2937;
            background: #fff;
        }

        .registros-info {
            font-size: 13px;
            font-weight: 500;
            color: #475569;
        }

        /* Carregar Mais */
        .carregar-mais-wrapper {
            display: flex;
            flex-direction: column;
            align-items: center;
            padding: 20px;
            background: #f8fafc;
            border-top: 1px solid #e5e7eb;
            gap: 12px;
        }

        .carregar-mais-buttons {
            display: flex;
            gap: 12px;
            align-items: center;
        }

        .btn-carregar-mais {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
            padding: 12px 32px;
            background: linear-gradient(90deg, #2563eb 0%, #1d4ed8 100%);
            color: #fff;
            border: none;
            border-radius: 8px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            box-shadow: 0 4px 12px rgba(37, 99, 235, 0.25);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .btn-carregar-mais:hover {
            transform: translateY(-1px);
            box-shadow: 0 6px 16px rgba(37, 99, 235, 0.3);
        }

        .btn-carregar-mais:disabled {
            opacity: 0.5;
            cursor: not-allowed;
            transform: none;
            box-shadow: none;
        }

        .btn-mostrar-tudo {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
            padding: 12px 32px;
            background: linear-gradient(90deg, #7c3aed 0%, #6d28d9 100%);
            color: #fff;
            border: none;
            border-radius: 8px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            box-shadow: 0 4px 12px rgba(124, 58, 237, 0.25);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .btn-mostrar-tudo:hover {
            transform: translateY(-1px);
            box-shadow: 0 6px 16px rgba(124, 58, 237, 0.3);
        }

        .registros-restantes {
            font-size: 12px;
            color: #64748b;
        }

        /* Navegação horizontal da tabela */
        .scroll-nav-container {
            display: flex;
            align-items: center;
            gap: 8px;
            margin-bottom: 12px;
        }

        .scroll-nav-btn {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            width: 36px;
            height: 36px;
            background: #fff;
            border: 1px solid #cbd5e1;
            border-radius: 6px;
            cursor: pointer;
            color: #475569;
            transition: all 0.2s ease;
        }

        .scroll-nav-btn:hover {
            background: #f1f5f9;
            border-color: #94a3b8;
        }

        .scroll-nav-btn:active {
            background: #e2e8f0;
        }

        .scroll-nav-btn svg {
            width: 20px;
            height: 20px;
            fill: currentColor;
        }

        .scroll-indicator {
            flex: 1;
            height: 6px;
            background: #e2e8f0;
            border-radius: 3px;
            overflow: hidden;
        }

        .scroll-indicator-bar {
            height: 100%;
            background: linear-gradient(90deg, #2563eb 0%, #1d4ed8 100%);
            border-radius: 3px;
            transition: width 0.2s ease, left 0.2s ease;
            position: relative;
        }

        /* Cores de status aplicadas via código VB no RowDataBound */
        /* Linhas mantêm cor de hover suave */
        tr:hover td {
            filter: brightness(0.95);
        }

        /* Status truncado com tooltip */
        .status-text {
            display: inline-block;
            max-width: 230px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            vertical-align: middle;
        }
        .status-text[title]:hover {
            cursor: help;
        }

        /* Checkbox de seleção com numeração */
        .checkbox-selection {
            display: flex;
            align-items: center;
            gap: 8px;
        }

        .selection-number {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            min-width: 24px;
            height: 24px;
            padding: 0 6px;
            background: linear-gradient(135deg, #3b82f6 0%, #2563eb 100%);
            color: #fff;
            border-radius: 12px;
            font-size: 11px;
            font-weight: 700;
            box-shadow: 0 2px 4px rgba(37, 99, 235, 0.3);
        }

        .checkbox-selection input[type="checkbox"] {
            width: 18px;
            height: 18px;
            cursor: pointer;
        }

        /* Status do Robô - Badges */
        .status-robo {
            display: inline-block;
            padding: 4px 10px;
            font-size: 11px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.03em;
            border-radius: 4px;
            white-space: nowrap;
        }

        .status-robo-100 { background-color: #dbeafe; color: #1e40af; } /* Preparando */
        .status-robo-150 { background-color: #fef3c7; color: #92400e; } /* Executando */
        .status-robo-180 { background-color: #e0e7ff; color: #3730a3; } /* Finalizado */
        .status-robo-200 { background-color: #d1fae5; color: #047857; } /* Sucesso */
        .status-robo-250 { background-color: #fed7aa; color: #9a3412; } /* Sucesso c/ Pendências */
        .status-robo-300 { background-color: #fecaca; color: #991b1b; } /* Erro Recuperável */
        .status-robo-350 { background-color: #fecaca; color: #7f1d1d; } /* Erro Lógico */
        .status-robo-400 { background-color: #fca5a5; color: #7f1d1d; } /* Falha Operacional */
        .status-robo-500 { background-color: #f87171; color: #450a0a; } /* Falha Crítica */

        /* Botão Preparar */
        .btn-preparar {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            padding: 8px 16px;
            background: linear-gradient(90deg, #059669 0%, #047857 100%);
            color: #fff !important;
            border-radius: 6px;
            border: none;
            font-size: 13px;
            font-weight: 600;
            box-shadow: 0 4px 12px rgba(5, 150, 105, 0.25);
            text-decoration: none;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
            cursor: pointer;
        }

        .btn-preparar:hover {
            transform: translateY(-1px);
            box-shadow: 0 6px 16px rgba(5, 150, 105, 0.3);
        }

        .btn-preparar:disabled {
            opacity: 0.5;
            cursor: not-allowed;
            transform: none;
        }

        .btn-preparar svg {
            width: 16px;
            height: 16px;
        }

        .sr-only {
            position: absolute;
            width: 1px;
            height: 1px;
            padding: 0;
            margin: -1px;
            overflow: hidden;
            clip: rect(0, 0, 0, 0);
            white-space: nowrap;
            border: 0;
        }

        .pagination-bar {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            align-items: center;
            padding: 16px 24px;
            background: #f8fafc;
            border-top: 1px solid #edf0f5;
        }

        .pagination-summary {
            font-size: 13px;
            color: #475569;
        }

        .pagination-controls {
            display: flex;
            align-items: center;
            gap: 12px;
        }

        .pagination-controls .btn-nav {
            border: 1px solid #cbd5e1;
            background: #fff;
            color: #334155;
            border-radius: 6px;
            padding: 8px 14px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: background 0.2s ease;
        }

        .pagination-controls .btn-nav:disabled {
            opacity: 0.45;
            cursor: not-allowed;
        }

        .pagination-controls .btn-nav:not(:disabled):hover {
            background: #f1f5f9;
        }

        .pagination-controls .page-info {
            font-size: 13px;
            font-weight: 600;
            color: #1f2937;
        }

        .pagination-controls .page-size-select {
            width: 70px;
            border: 1px solid #cbd5e1;
            border-radius: 6px;
            padding: 6px 10px;
            font-size: 13px;
            color: #1f2937;
            background: #fff;
        }

        .alert {
            padding: 15px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .alert-danger {
            color: #a94442;
            background-color: #f2dede;
            border-color: #ebccd1;
        }

        @media (max-width: 768px) {
            .consulta-wrapper {
                padding: 16px;
            }

            .card-header {
                flex-direction: column;
                align-items: flex-start;
                gap: 12px;
            }

            .search-actions {
                justify-content: stretch;
            }

            .pagination-bar {
                flex-direction: column;
                gap: 12px;
                align-items: flex-start;
            }
        }
    </style>

    <script type="text/javascript">
        // Controle de seleção de parâmetros para execução
        var itensSelecionados = {};
        var proximaSequencia = 1;

        function atualizarSelecao(checkbox, idParametro) {
            if (checkbox.checked) {
                // Adicionar à seleção
                itensSelecionados[idParametro] = proximaSequencia;
                proximaSequencia++;
            } else {
                // Remover da seleção
                delete itensSelecionados[idParametro];
                // Renumerar itens restantes
                renumerarSelecao();
            }

            // Atualizar visualização
            atualizarVisualizacaoSelecao();
            atualizarEstadoBotaoPreparar();
        }

        function renumerarSelecao() {
            var itensOrdenados = Object.keys(itensSelecionados).sort(function(a, b) {
                return itensSelecionados[a] - itensSelecionados[b];
            });

            proximaSequencia = 1;
            itensOrdenados.forEach(function(id) {
                itensSelecionados[id] = proximaSequencia;
                proximaSequencia++;
            });
        }

        function atualizarVisualizacaoSelecao() {
            // Atualizar todos os checkboxes e numerações
            var gridView = document.getElementById('<%= gvParametros.ClientID %>');
            if (!gridView) return;

            var checkboxes = gridView.querySelectorAll('input[type="checkbox"][data-id-parametro]');
            checkboxes.forEach(function(cb) {
                var idParametro = cb.getAttribute('data-id-parametro');
                var numberSpan = cb.nextElementSibling;

                if (itensSelecionados[idParametro]) {
                    // Item selecionado - mostrar número
                    if (numberSpan && numberSpan.classList.contains('selection-number')) {
                        numberSpan.textContent = itensSelecionados[idParametro];
                        numberSpan.style.display = 'inline-flex';
                    } else {
                        // Criar span de número se não existir
                        var span = document.createElement('span');
                        span.className = 'selection-number';
                        span.textContent = itensSelecionados[idParametro];
                        cb.parentNode.insertBefore(span, cb.nextSibling);
                    }
                } else {
                    // Item não selecionado - esconder número
                    if (numberSpan && numberSpan.classList.contains('selection-number')) {
                        numberSpan.style.display = 'none';
                    }
                }
            });

            // Atualizar campo hidden com a seleção
            atualizarCampoHiddenSelecao();
        }

        function atualizarCampoHiddenSelecao() {
            var hdnSelecao = document.getElementById('<%= hdnItensSelecionados.ClientID %>');
            if (hdnSelecao) {
                hdnSelecao.value = JSON.stringify(itensSelecionados);
            }
        }

        function atualizarEstadoBotaoPreparar() {
            var btnPreparar = document.getElementById('<%= btnPreparar.ClientID %>');
            if (btnPreparar) {
                var temSelecao = Object.keys(itensSelecionados).length > 0;
                btnPreparar.disabled = !temSelecao;
            }
        }

        function limparSelecao() {
            itensSelecionados = {};
            proximaSequencia = 1;
            atualizarVisualizacaoSelecao();
            atualizarEstadoBotaoPreparar();

            // Desmarcar todos os checkboxes
            var gridView = document.getElementById('<%= gvParametros.ClientID %>');
            if (gridView) {
                var checkboxes = gridView.querySelectorAll('input[type="checkbox"][data-id-parametro]');
                checkboxes.forEach(function(cb) {
                    cb.checked = false;
                });
            }
        }

        // Resetar apenas a numeração (manter checkboxes marcados, mas limpar numeros e contagem)
        function resetarNumeracao() {
            itensSelecionados = {};
            proximaSequencia = 1;

            // Esconder todos os números de sequência
            var gridView = document.getElementById('<%= gvParametros.ClientID %>');
            if (gridView) {
                var numberSpans = gridView.querySelectorAll('.selection-number');
                numberSpans.forEach(function(span) {
                    span.style.display = 'none';
                });
            }

            // Limpar campo hidden
            atualizarCampoHiddenSelecao();
            atualizarEstadoBotaoPreparar();
        }

        // Inicializar após carregamento da página
        document.addEventListener('DOMContentLoaded', function() {
            atualizarEstadoBotaoPreparar();
        });
    </script>

    <!-- Campos Hidden para controlar seleção -->
    <asp:HiddenField ID="hdnItensSelecionados" runat="server" />

    <div class="consulta-wrapper">
        <!-- ANIMA -->
        <div class="consulta-container">
            <div class="card">
                <div class="card-header">
                    <div>
                        <h1>Parametros de Integracao - Anima</h1>
                        <p>Gerencie, filtre e pesquise os parametros disponiveis para a integracao.</p>
                    </div>
                    <div style="display: flex; gap: 10px; align-items: center;">
                        <asp:Button ID="btnPreparar" runat="server" Text="Preparar Sequencia" CssClass="btn-preparar"
                            OnClick="BtnPreparar_Click" />
                        <a href="javascript:void(0);" onclick="abrirModalPreparacao(); return false;" style="font-size: 13px; color: #2563eb; text-decoration: none; margin-left: 10px;">Ver Preparacao</a>
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlAlert" runat="server" Visible="false" role="alert">
                <asp:Label ID="lblAlertMessage" runat="server"></asp:Label>
            </asp:Panel>

            <div class="card search-card">
                <div class="card-body">
                    <div class="filter-grid">
                        <div>
                            <label for="<%= ddlFiltroStatus.ClientID %>" class="form-label">Status</label>
                            <asp:DropDownList ID="ddlFiltroStatus" runat="server" CssClass="form-select-custom">
                                <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Pendente" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Completo" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <label for="<%= ddlFiltroTipo.ClientID %>" class="form-label">Tipo</label>
                            <asp:DropDownList ID="ddlFiltroTipo" runat="server" CssClass="form-control-custom">
                                <asp:ListItem Text="-- Todos --" Value=""></asp:ListItem>
                                <asp:ListItem Text="Desktop" Value="Desktop"></asp:ListItem>
                                <asp:ListItem Text="Geral" Value="Geral"></asp:ListItem>
                                <asp:ListItem Text="Impressora" Value="Impressora"></asp:ListItem>
                                <asp:ListItem Text="Link Dados" Value="Link_Dados"></asp:ListItem>
                                <asp:ListItem Text="Plano Saude" Value="Plano_Saude"></asp:ListItem>
                                <asp:ListItem Text="Servico" Value="Servico"></asp:ListItem>
                                <asp:ListItem Text="Telefonia Fixa" Value="Telefonia_Fixa"></asp:ListItem>
                                <asp:ListItem Text="Telefonia Movel" Value="Telefonia_Movel"></asp:ListItem>
                                <asp:ListItem Text="TV Assinatura" Value="TV Assinatura"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <label for="<%= txtFiltroCNPJ.ClientID %>" class="form-label">CNPJ Anima</label>
                            <asp:TextBox ID="txtFiltroCNPJ" runat="server" CssClass="form-control-custom" MaxLength="18" placeholder="Digite o CNPJ"></asp:TextBox>
                        </div>
                        <div>
                            <label for="<%= txtFiltroConta.ClientID %>" class="form-label">Conta</label>
                            <asp:TextBox ID="txtFiltroConta" runat="server" CssClass="form-control-custom" MaxLength="100" placeholder="Digite a conta"></asp:TextBox>
                        </div>
                        <div>
                            <label for="<%= txtFiltroFornecedor.ClientID %>" class="form-label">Fornecedor</label>
                            <asp:TextBox ID="txtFiltroFornecedor" runat="server" CssClass="form-control-custom" MaxLength="100" placeholder="Digite o fornecedor"></asp:TextBox>
                        </div>
                        <div>
                            <label for="<%= txtFiltroMesEmissao.ClientID %>" class="form-label">Mes Emissao</label>
                            <asp:TextBox ID="txtFiltroMesEmissao" runat="server" CssClass="form-control-custom mes-emissao-mask" MaxLength="7" placeholder="MM/AAAA"></asp:TextBox>
                        </div>
                    </div>
                    <div class="search-actions">
                        <asp:Button ID="btnLimpar" runat="server" Text="Limpar filtros" CssClass="btn-outline" OnClick="btnLimpar_Click" />
                        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="btn-primary-custom" OnClick="btnPesquisar_Click" />
                    </div>
                </div>
            </div>

            <div class="card grid-container">
                <div class="grid-table-wrapper">
                    <asp:GridView ID="gvParametros" runat="server" AutoGenerateColumns="False"
                        OnRowCommand="gvParametros_RowCommand"
                        OnRowDataBound="gvParametros_RowDataBound"
                        CssClass="gridview-modern"
                        GridLines="None"
                        DataKeyNames="Id_Parametro,Conta"
                        AllowPaging="False"
                        AllowSorting="True"
                        OnSorting="gvParametros_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="Sel" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <div class="checkbox-selection">
                                        <asp:Literal ID="litCheckbox" runat="server"></asp:Literal>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo" SortExpression="Tipo">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lnkSortTipo" runat="server" CommandName="Sort" CommandArgument="Tipo" CssClass="sort-header">
                                        Tipo<span class="sort-icon"></span>
                                    </asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("Tipo") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="Status" ItemStyle-Width="250px">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lnkSortStatus" runat="server" CommandName="Sort" CommandArgument="Status" CssClass="sort-header">
                                        Status<span class="sort-icon"></span>
                                    </asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descricao" ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Literal ID="litDescricao" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mes Emissao" SortExpression="MesEmissao">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lnkSortMesEmissao" runat="server" CommandName="Sort" CommandArgument="MesEmissao" CssClass="sort-header">
                                        Mes Emissao<span class="sort-icon"></span>
                                    </asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("MesEmissao") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Conta" SortExpression="Conta">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lnkSortConta" runat="server" CommandName="Sort" CommandArgument="Conta" CssClass="sort-header">
                                        Conta<span class="sort-icon"></span>
                                    </asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("Conta") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SupplierName" HeaderText="Fornecedor" />
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="grid-actions-header" ItemStyle-CssClass="grid-actions-column">
                                <ItemTemplate>
                                    <%-- Ícone de flag manual - preenchido dinamicamente no RowDataBound --%>
                                    <asp:Literal ID="litManualFlag" runat="server"></asp:Literal>
                                    <asp:LinkButton ID="btnVerDetalhe" runat="server" CssClass="grid-action-icon-view"
                                        CommandName="VerDetalhe" CommandArgument='<%# Eval("Conta") & "|" & Eval("MesEmissao") & "|" & Eval("Tipo") %>'
                                        title="Ver detalhes da fatura" ToolTip="Ver detalhes da fatura">
                                        <span class="sr-only">Ver detalhes</span>
                                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" aria-hidden="true" focusable="false">
                                            <path d="M12 4.5C7 4.5 2.73 7.61 1 12c1.73 4.39 6 7.5 11 7.5s9.27-3.11 11-7.5c-1.73-4.39-6-7.5-11-7.5zM12 17c-2.76 0-5-2.24-5-5s2.24-5 5-5 5 2.24 5 5-2.24 5-5 5zm0-8c-1.66 0-3 1.34-3 3s1.34 3 3 3 3-1.34 3-3-1.34-3-3-3z"/>
                                        </svg>
                                    </asp:LinkButton>
                                    <a class="grid-action-icon" href='<%# ObterLinkEdicao(Eval("Id_Parametro"), Eval("Conta"), Eval("MesEmissao")) %>' title="Editar parametro">
                                        <span class="sr-only">Editar</span>
                                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" aria-hidden="true" focusable="false">
                                            <path d="M4 17.25V20h2.75l8.086-8.086-2.75-2.75L4 17.25zm13.71-7.21a1.004 1.004 0 000-1.42l-2.34-2.34a1.004 1.004 0 00-1.42 0l-1.83 1.83 3.76 3.76 1.83-1.83z"/>
                                        </svg>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="pagination-bar">
                    <div class="pagination-summary">
                        <asp:Label ID="lblTotalRegistros" runat="server" Text="0 registros encontrados"></asp:Label>
                    </div>
                    <div class="pagination-controls">
                        <span>Itens por pagina</span>
                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="page-size-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="20" Value="20" />
                            <asp:ListItem Text="50" Value="50" />
                        </asp:DropDownList>
                        <asp:Button ID="btnPaginaAnterior" runat="server" Text="Anterior" CssClass="btn-nav" OnClick="btnPaginaAnterior_Click" />
                        <span class="page-info">
                            <asp:Label ID="lblPaginaAtual" runat="server" Text="1 / 1"></asp:Label>
                        </span>
                        <asp:Button ID="btnPaginaProxima" runat="server" Text="Proxima" CssClass="btn-nav" OnClick="btnPaginaProxima_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal de Detalhes da Fatura -->
    <div id="modalDetalhe" class="modal-overlay">
        <div class="modal-content">
            <div class="modal-header">
                <h2>Detalhes da Fatura - Conta: <span id="modalContaLabel"></span></h2>
                <div class="modal-header-actions">
                    <asp:Button ID="btnExportarCSV" runat="server" Text="Exportar CSV" CssClass="btn-exportar-csv" OnClick="btnExportarCSV_Click" />
                    <button type="button" class="modal-close" onclick="fecharModal()">&times;</button>
                </div>
            </div>
            <div class="modal-toolbar">
                <div class="modal-toolbar-left">
                    <span class="toolbar-label">Linhas por vez:</span>
                    <asp:DropDownList ID="ddlLinhasPorVez" runat="server" CssClass="linhas-select">
                        <asp:ListItem Text="100" Value="100" Selected="True" />
                        <asp:ListItem Text="500" Value="500" />
                        <asp:ListItem Text="1000" Value="1000" />
                        <asp:ListItem Text="5000" Value="5000" />
                    </asp:DropDownList>
                    <span class="toolbar-label" style="margin-left: 15px;">Conceito:</span>
                    <asp:DropDownList ID="ddlFiltroConceito" runat="server" CssClass="linhas-select" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltroConceito_SelectedIndexChanged">
                        <asp:ListItem Text="Todos" Value="" Selected="True" />
                        <asp:ListItem Text="Servico" Value="s" />
                        <asp:ListItem Text="Assinatura" Value="a" />
                    </asp:DropDownList>
                </div>
                <div class="modal-toolbar-right">
                    <span id="lblRegistrosExibidos" class="registros-info"></span>
                </div>
            </div>
            <div class="modal-body">
                <div id="modalLoading" class="loading-spinner">
                    Carregando dados...
                </div>
                <div id="modalNoData" class="no-data-message" style="display:none;">
                    Nenhum registro encontrado para esta conta.
                </div>
                <div id="modalTableWrapper" class="detail-table-wrapper" style="display:none;">
                    <!-- Navegação horizontal -->
                    <div class="scroll-nav-container">
                        <button type="button" class="scroll-nav-btn" id="btnScrollLeft" title="Rolar para esquerda">
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M15.41 7.41L14 6l-6 6 6 6 1.41-1.41L10.83 12z"/></svg>
                        </button>
                        <div class="scroll-indicator">
                            <div class="scroll-indicator-bar" id="scrollIndicatorBar" style="width: 20%;"></div>
                        </div>
                        <button type="button" class="scroll-nav-btn" id="btnScrollRight" title="Rolar para direita">
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M8.59 16.59L10 18l6-6-6-6-1.41 1.41L13.17 12z"/></svg>
                        </button>
                    </div>
                    <div id="tableScrollContainer" style="overflow-x: auto;">
                        <asp:GridView ID="gvDetalheFatura" runat="server" AutoGenerateColumns="True"
                            CssClass="detail-table" GridLines="None">
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div id="modalCarregarMais" class="carregar-mais-wrapper" style="display:none;">
                <div class="carregar-mais-buttons">
                    <asp:Button ID="btnCarregarMais" runat="server" Text="Carregar Mais" CssClass="btn-carregar-mais" OnClick="btnCarregarMais_Click" />
                    <asp:Button ID="btnMostrarTudo" runat="server" Text="Mostrar Tudo" CssClass="btn-mostrar-tudo" OnClick="btnMostrarTudo_Click" />
                </div>
                <span id="lblRestantes" class="registros-restantes"></span>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdnContaSelecionada" runat="server" />
    <asp:HiddenField ID="hdnTotalRegistrosDetalhe" runat="server" Value="0" />
    <asp:HiddenField ID="hdnRegistrosExibidos" runat="server" Value="0" />

    <!-- Botao hidden para carregar dados da preparacao -->
    <asp:Button ID="btnVerPreparacao" runat="server" style="display:none;" OnClick="btnVerPreparacao_Click" />

    <!-- Modal de Preparacao -->
    <div id="modalPreparacao" class="modal-overlay">
        <div class="modal-content" style="max-width: 900px;">
            <div class="modal-header">
                <h2>Sequencia Preparada</h2>
                <button type="button" class="modal-close" onclick="fecharModalPreparacao()">&times;</button>
            </div>
            <div class="modal-body">
                <div id="modalPreparacaoLoading" class="loading-spinner">
                    Carregando dados...
                </div>
                <div id="modalPreparacaoNoData" class="no-data-message" style="display:none;">
                    Nenhum registro na sequencia preparada.
                </div>
                <div id="modalPreparacaoTableWrapper" class="detail-table-wrapper" style="display:none;">
                    <asp:GridView ID="gvPreparacao" runat="server" AutoGenerateColumns="False"
                        CssClass="detail-table" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="Ordem" HeaderText="Ordem" ItemStyle-Width="60px" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                            <asp:BoundField DataField="Conta" HeaderText="Conta" />
                            <asp:BoundField DataField="MesEmissao" HeaderText="Mes Emissao" />
                            <asp:BoundField DataField="SupplierName" HeaderText="Fornecedor" />
                            <asp:BoundField DataField="Data_Preparacao" HeaderText="Data Preparacao" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function abrirModal(conta) {
            document.getElementById('modalContaLabel').innerText = conta;
            document.getElementById('modalDetalhe').classList.add('active');
            document.getElementById('modalLoading').style.display = 'block';
            document.getElementById('modalNoData').style.display = 'none';
            document.getElementById('modalTableWrapper').style.display = 'none';
            document.getElementById('modalCarregarMais').style.display = 'none';
        }

        function fecharModal() {
            document.getElementById('modalDetalhe').classList.remove('active');
        }

        function mostrarTabela(exibidos, total) {
            document.getElementById('modalLoading').style.display = 'none';
            document.getElementById('modalTableWrapper').style.display = 'block';

            // Atualiza contador de registros exibidos
            var lblExibidos = document.getElementById('lblRegistrosExibidos');
            if (lblExibidos) {
                lblExibidos.innerText = 'Exibindo ' + exibidos + ' de ' + total + ' registros';
            }

            // Mostra ou esconde os botões Carregar Mais / Mostrar Tudo
            var carregarMaisWrapper = document.getElementById('modalCarregarMais');
            var lblRestantes = document.getElementById('lblRestantes');
            if (exibidos < total) {
                carregarMaisWrapper.style.display = 'flex';
                if (lblRestantes) {
                    var restantes = total - exibidos;
                    lblRestantes.innerText = restantes + ' registro(s) restante(s)';
                }
            } else {
                // Todos os registros exibidos - esconde os botões
                carregarMaisWrapper.style.display = 'none';
            }

            // Inicializa navegação horizontal
            setTimeout(initScrollNav, 100);
        }

        function mostrarSemDados() {
            document.getElementById('modalLoading').style.display = 'none';
            document.getElementById('modalNoData').style.display = 'block';
            document.getElementById('modalCarregarMais').style.display = 'none';
        }

        // Navegação horizontal da tabela
        function initScrollNav() {
            var container = document.getElementById('tableScrollContainer');
            var btnLeft = document.getElementById('btnScrollLeft');
            var btnRight = document.getElementById('btnScrollRight');
            var indicatorBar = document.getElementById('scrollIndicatorBar');

            if (!container || !btnLeft || !btnRight || !indicatorBar) return;

            var scrollStep = 300; // pixels por clique

            function updateIndicator() {
                var scrollWidth = container.scrollWidth - container.clientWidth;
                if (scrollWidth <= 0) {
                    indicatorBar.style.width = '100%';
                    indicatorBar.style.marginLeft = '0';
                    return;
                }
                var scrollPercent = container.scrollLeft / scrollWidth;
                var barWidth = (container.clientWidth / container.scrollWidth) * 100;
                var barPosition = scrollPercent * (100 - barWidth);
                indicatorBar.style.width = barWidth + '%';
                indicatorBar.style.marginLeft = barPosition + '%';
            }

            btnLeft.onclick = function() {
                container.scrollBy({ left: -scrollStep, behavior: 'smooth' });
            };

            btnRight.onclick = function() {
                container.scrollBy({ left: scrollStep, behavior: 'smooth' });
            };

            container.addEventListener('scroll', updateIndicator);
            updateIndicator();
        }

        // Fechar modal ao clicar fora
        document.getElementById('modalDetalhe').addEventListener('click', function(e) {
            if (e.target === this) {
                fecharModal();
            }
        });

        // Modal de Preparação
        function abrirModalPreparacao() {
            // Mostra o modal com loading (o servidor vai atualizar depois do postback)
            document.getElementById('modalPreparacao').classList.add('active');
            document.getElementById('modalPreparacaoLoading').style.display = 'block';
            document.getElementById('modalPreparacaoNoData').style.display = 'none';
            document.getElementById('modalPreparacaoTableWrapper').style.display = 'none';

            // Dispara postback para carregar dados
            // O servidor vai chamar mostrarTabelaPreparacao() via RegisterStartupScript
            __doPostBack('<%= btnVerPreparacao.UniqueID %>', '');
        }

        function fecharModalPreparacao() {
            document.getElementById('modalPreparacao').classList.remove('active');
        }

        function mostrarTabelaPreparacao(total) {
            document.getElementById('modalPreparacaoLoading').style.display = 'none';
            if (total > 0) {
                document.getElementById('modalPreparacaoTableWrapper').style.display = 'block';
            } else {
                document.getElementById('modalPreparacaoNoData').style.display = 'block';
            }
        }

        // Fechar modal de preparação ao clicar fora
        document.getElementById('modalPreparacao').addEventListener('click', function(e) {
            if (e.target === this) {
                fecharModalPreparacao();
            }
        });

        // Fechar modal com ESC
        document.addEventListener('keydown', function(e) {
            if (e.key === 'Escape') {
                fecharModal();
                fecharModalPreparacao();
            }
        });

        // Mascara para o campo Mês Emissão (MM/AAAA)
        document.addEventListener('DOMContentLoaded', function() {
            var mesEmissaoInputs = document.querySelectorAll('.mes-emissao-mask');
            mesEmissaoInputs.forEach(function(input) {
                input.addEventListener('input', function(e) {
                    var value = e.target.value.replace(/\D/g, '');
                    if (value.length > 6) {
                        value = value.substring(0, 6);
                    }
                    if (value.length >= 2) {
                        var mes = value.substring(0, 2);
                        if (parseInt(mes, 10) > 12) {
                            mes = '12';
                        }
                        if (parseInt(mes, 10) < 1 && mes.length === 2) {
                            mes = '01';
                        }
                        value = mes + '/' + value.substring(2);
                    }
                    e.target.value = value;
                });

                input.addEventListener('keydown', function(e) {
                    // Permite: backspace, delete, tab, escape, enter, home, end, setas
                    if ([8, 9, 13, 27, 35, 36, 37, 38, 39, 40, 46].indexOf(e.keyCode) !== -1 ||
                        // Permite Ctrl+A, Ctrl+C, Ctrl+V, Ctrl+X
                        (e.keyCode >= 65 && e.keyCode <= 90 && (e.ctrlKey === true || e.metaKey === true))) {
                        return;
                    }
                    // Permite apenas numeros
                    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                        e.preventDefault();
                    }
                });
            });
        });
    </script>
</asp:Content>
