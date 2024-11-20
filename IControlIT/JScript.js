    //-----JavaScript

    //-----variaveis globais
    //-----captura nome da list pra ordenacao
    var listOrigemOrdenado;
    var hiddenHierarquia;
    var hdfSelect;

    //-----convert objeto
    function reconfigura(pTexto) {
        if ($get(pTexto) == null) { var objFiltro = pTexto.replace(/\$/g, '_'); }
        if ($get(objFiltro) == null) {
            var objFiltro_1 = pTexto.replace(/\$/g, '_');
            var objFiltro = objFiltro_1.replace(/ctl00_/g, '');
        }
	    return objFiltro;
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------MOVE REGISTRO DA LISTBOX------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    //-----monta dados seleciconados  do user control filtro_acesso-------------------------------------------------------------------
    function montaSelectChek(pSelecao) {
        if (pSelecao == 'filial') {
            var pHiddenSelect = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectFilial'));
            var vCheckBoxList = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkFilial'));
            var vDivCentroCusto = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$DivComboCentroCusto'));
            var vDivDepartamento = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$DivComboDepartamento'));
            var vDivSetor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$DivComboSetor'));
            var vDivSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$DivComboSecao'));
            var vhdfSelectCentroCusto = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectCentroCusto'));
            var vhdfSelectDepartamento = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectDepartamento'));
            var vhdfSelectSetor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectSetor'));
            var vhdfSelectSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectSecao'));
            var vhdfSelectConsumidor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectConsumidor'));
            var chkCentroCusto = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkCentroCusto'));
            var chkDepartamento = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkDepartamento'));
            var chkSetor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkSetor'));
            var chkSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkSecao'));

            if (vhdfSelectCentroCusto != null) {
                vhdfSelectCentroCusto.value = '';
                vDivCentroCusto.style.height = '0px';
                for (var i = 0; i < chkCentroCusto.rows.length; i++) { chkCentroCusto.deleteRow(i); i--; }
            }
            if (vhdfSelectDepartamento != null) {
                vhdfSelectDepartamento.value = '';
                vDivDepartamento.style.height = '0px';
                for (var i = 0; i < chkDepartamento.rows.length; i++) { chkDepartamento.deleteRow(i); i--; }
            }
            if (vhdfSelectSetor != null) {
                vhdfSelectSetor.value = '';
                vDivSetor.style.height = '0px';
                for (var i = 0; i < chkSetor.rows.length; i++) { chkSetor.deleteRow(i); i--; }
            }
            if (vhdfSelectSecao != null) {
                vDivSecao.style.height = '0px';
                vhdfSelectSecao.value = '';
                for (var i = 0; i < chkSecao.rows.length; i++) { chkSecao.deleteRow(i); i--; }
            }

            if (vhdfSelectConsumidor != null) { vhdfSelectConsumidor.value = ''; }
        }
        if (pSelecao == 'centrocusto') {
            var pHiddenSelect = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectCentroCusto'));
            var vCheckBoxList = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkCentroCusto'));
            var vDivDepartamento = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$DivComboDepartamento'));
            var vDivSetor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$DivComboSetor'));
            var vDivSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$DivComboSecao'));
            var vhdfSelectDepartamento = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectDepartamento'));
            var vhdfSelectSetor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectSetor'));
            var vhdfSelectSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectSecao'));
            var vhdfSelectConsumidor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectConsumidor'));
            var chkDepartamento = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkDepartamento'));
            var chkSetor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkSetor'));
            var chkSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkSecao'));

            if (vhdfSelectDepartamento != null) {
                vhdfSelectDepartamento.value = '';
                vDivDepartamento.style.height = '0px';
                for (var i = 0; i < chkDepartamento.rows.length; i++) { chkDepartamento.deleteRow(i); i--; }
            }
            if (vhdfSelectSetor != null) {
                vhdfSelectSetor.value = '';
                vDivSetor.style.height = '0px';
                for (var i = 0; i < chkSetor.rows.length; i++) { chkSetor.deleteRow(i); i--; }
            }
            if (vhdfSelectSecao != null) {
                vhdfSelectSecao.value = '';
                vDivSecao.style.height = '0px';
                for (var i = 0; i < chkSecao.rows.length; i++) { chkSecao.deleteRow(i); i--; }
            }

            if (vhdfSelectConsumidor != null) { vhdfSelectConsumidor.value = ''; }
        }
        if (pSelecao == 'departamento') {
            var pHiddenSelect = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectDepartamento'));
            var vCheckBoxList = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkDepartamento'));
            var vDivSetor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$DivComboSetor'));
            var vDivSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$DivComboSecao'));
            var vhdfSelectSetor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectSetor'));
            var vhdfSelectSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectSecao'));
            var vhdfSelectConsumidor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectConsumidor'));
            var chkSetor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkSetor'));
            var chkSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkSecao'));

            if (vhdfSelectSetor != null) {
                vhdfSelectSetor.value = '';
                vDivSetor.style.height = '0px';
                for (var i = 0; i < chkSetor.rows.length; i++) { chkSetor.deleteRow(i); i--; }
            }
            if (vhdfSelectSecao != null) {
                vhdfSelectSecao.value = '';
                vDivSecao.style.height = '0px';
                for (var i = 0; i < chkSecao.rows.length; i++) { chkSecao.deleteRow(i); i--; }
            }

            if (vhdfSelectConsumidor != null) { vhdfSelectConsumidor.value = ''; }
        }
        if (pSelecao == 'setor') {
            var pHiddenSelect = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectSetor'));
            var vCheckBoxList = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkSetor'));
            var vDivSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$DivComboSecao'));
            var vhdfSelectSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectSetor'));
            var vhdfSelectConsumidor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectConsumidor'));
            var chkSecao = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkSecao'));

            if (vhdfSelectSecao != null) {
                vhdfSelectSecao.value = '';
                vDivSecao.style.height = '0px';
                for (var i = 0; i < chkSecao.rows.length; i++) { chkSecao.deleteRow(i); i--; }
            }

            if (vhdfSelectConsumidor != null) { vhdfSelectConsumidor.value = ''; }
        }
        if (pSelecao == 'secao') {
            var vhdfSelectConsumidor = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectConsumidor'));
            var pHiddenSelect = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectSecao'));
            var vCheckBoxList = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkSecao'));
            if (vhdfSelectConsumidor != null) { vhdfSelectConsumidor.value = ''; }
        }

        if (pSelecao == 'consumidor') {
            var pHiddenSelect = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$hdfSelectConsumidor'));
            var vCheckBoxList = $get(reconfigura('ctl00$ContentPlaceHolder1$Filtro_Acesso$chkConsumidor'));
        }

        var ArraySelecao = pHiddenSelect.value.split(',');
        pHiddenSelect.value = '';
        var vSTR = '';

        if (vCheckBoxList.cells[0].childNodes[0].checked == true) {
            for (var i = 0; i < vCheckBoxList.cells.length; i++) {
                vCheckBoxList.cells[i].childNodes[0].checked = true;
            }
        }
        else {
            var vCont = 0;
            for (var i = 0; i < vCheckBoxList.cells.length; i++) {
                if (vCheckBoxList.cells[i].childNodes[0].checked == true)
                    vCont = vCont + 1;
            }
            if (ArraySelecao.length == vCont && vCont == (vCheckBoxList.cells.length - 1) && (vCheckBoxList.cells.length - 1) > 1) {
                for (var i = 0; i < vCheckBoxList.cells.length; i++) {
                    vCheckBoxList.cells[i].childNodes[0].checked = false;
                }
            }
        }
        //grava selecao
        for (var i = 0; i < vCheckBoxList.cells.length; i++) {
            if (vCheckBoxList.cells[i].childNodes[0].checked == true && vCheckBoxList.cells[i].childNodes[0].value != -1) {
                vSTR = vSTR + vCheckBoxList.cells[i].childNodes[0].value + ',';
            }
        }
        pHiddenSelect.value = vSTR.substring(0, (vSTR.length - 1))
    }