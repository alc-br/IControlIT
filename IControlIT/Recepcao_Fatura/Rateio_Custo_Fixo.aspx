<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Rateio_Custo_Fixo.aspx.vb" Inherits="IControlIT.Rateio_Custo_Fixo" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblDataLote" runat="server" CssClass="configlabel" Text="* Mês de Vencimento"></asp:Label>
                            <asp:DropDownList ID="cboDataLote" runat="server" Width="100%" TabIndex="1" CssClass="configCombo"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDataLote" runat="server" ControlToValidate="cboDataLote" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblFaturaParametro" runat="server" CssClass="configlabel" Text="* Tipo da Fatura"></asp:Label>
                            <asp:DropDownList ID="cboFaturaParametro" runat="server" AutoPostBack="True" Width="100%" CssClass="configCombo" EnableTheming="True" TabIndex="2"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvFaturaParametro" runat="server" ControlToValidate="cboFaturaParametro" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 108; left: 155px; position: absolute; top: 273px; height: 41px; width: 136px; right: 619px;">
        <cc1:ValidatorCalloutExtender ID="vceDataLote" runat="server" TargetControlID="rfvDataLote"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceFaturaParametro" runat="server" TargetControlID="rfvFaturaParametro"></cc1:ValidatorCalloutExtender>
        <asp:Label ID="lblMessage" runat="server" Style="left: 13px; top: 87px"></asp:Label>
    </asp:Panel>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="tbBotao" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="btVoltar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btVoltar_Click">
            <i class="fas fa-arrow-left"></i>
            <br />
            <span>Voltar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" OnClick="btSalvar_Click">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
    </div>

</asp:Content>


