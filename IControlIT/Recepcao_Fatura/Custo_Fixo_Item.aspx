<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Custo_Fixo_Item.aspx.vb" Inherits="IControlIT.Custo_Fixo_Item" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="* Descrição"></asp:Label>
                            <asp:DropDownList ID="cboDescricao" runat="server" Width="100%" CssClass="configCombo" EnableTheming="True" TabIndex="1"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="cboDescricao" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"> </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblAtivoTipo" runat="server" CssClass="configlabel" Text="* Tipo do Ativo"></asp:Label>
                            <asp:DropDownList ID="cboAtivoTipo" runat="server" Width="100%" CssClass="configCombo" EnableTheming="True" TabIndex="2"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAtivoTipo" runat="server" ControlToValidate="cboAtivoTipo" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblConglomerado" runat="server" CssClass="configlabel" Text="* Fornecedor"></asp:Label>
                            <asp:DropDownList ID="cboConglomerado" runat="server" Width="100%" CssClass="configCombo" EnableTheming="True" TabIndex="3"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvConglomerado" runat="server" ControlToValidate="cboConglomerado" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblValor" runat="server" CssClass="configlabel" Text="* Valor"></asp:Label>
                            <asp:TextBox ID="txtValor" runat="server" CssClass="configtext" Width="100%" MaxLength="13" TabIndex="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvValor" runat="server" ControlToValidate="txtValor" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevValor" runat="server" ControlExtender="meeValor" ControlToValidate="txtValor" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevValor" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"> </cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco"></asp:Label>
                            <asp:TextBox ID="txtIdentificacao" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 221px; position: absolute; top: 379px; height: 51px; width: 130px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceAtivoTipo" runat="server" TargetControlID="rfvAtivoTipo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceConglomerado" runat="server" TargetControlID="rfvConglomerado"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceValor" runat="server" TargetControlID="rfvValor"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeValor" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" ErrorTooltipEnabled="True" Mask="99999999.99" MaskType="Number" TargetControlID="txtValor"></cc1:MaskedEditExtender>
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
        <asp:LinkButton ID="btLimpar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btLimpar_Click">
            <i class="fas fa-file"></i>
            <br />
            <span>Novo</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" OnClick="btSalvar_Click">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btDesativar" runat="server" CssClass="btn-menu-toolbar" OnClientClick="return confirm('Você deseja desativa o registro?');" OnClick="btDesativar_Click">
            <i class="fas fa-recycle"></i>
            <br />
            <span id="lblEncerrar" runat="server">Excluir</span>
        </asp:LinkButton>
    </div>

</asp:Content>

