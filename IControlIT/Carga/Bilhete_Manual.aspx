<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Bilhete_Manual.aspx.vb" Inherits="IControlIT.Bilhete_Manual" %>

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
                            <asp:Label ID="lblAtivo" runat="server" CssClass="configlabel" Text="* Ativo"></asp:Label>
                            <asp:TextBox ID="txtAtivo" runat="server" CssClass="configtext" Width="100%" MaxLength="50" TabIndex="1"></asp:TextBox>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvAtivo" runat="server" ControlToValidate="txtAtivo" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblValor" runat="server" CssClass="configlabel" Text="* Valor"></asp:Label>
                            <asp:TextBox ID="txtValorFatura" runat="server" CssClass="configtext" Width="100%" MaxLength="13" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvValorFatura" runat="server" ControlToValidate="txtValorFatura" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevValorFatura" runat="server" ControlExtender="meeValorFatura" ControlToValidate="txtValorFatura" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevValorFatura" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE">
                            </cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDataLote" runat="server" CssClass="configlabel" Text="* Data do Lote"></asp:Label>
                            <asp:TextBox ID="txtDataLote" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDataLote" runat="server" ControlToValidate="txtDataLote" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevDataLote" runat="server" ControlExtender="meeDataLote" ControlToValidate="txtDataLote" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataLote" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblData" runat="server" CssClass="configlabel" Text="* Data "></asp:Label>
                            <asp:TextBox ID="txtData" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvData" runat="server" ControlToValidate="txtData" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevData" runat="server" ControlExtender="meeData" ControlToValidate="txtData" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevData" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblNotaFical" runat="server" CssClass="configlabel" Text="* Conta"></asp:Label>
                            <asp:TextBox ID="txtNotaFiscal" runat="server" CssClass="configtext" Width="100%" MaxLength="50" TabIndex="5"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNotaFiscal" runat="server" ControlToValidate="txtNotaFiscal" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblTipoBilhete" runat="server" CssClass="configlabel" Text="* Tipo de Bilhete"></asp:Label>
                            <asp:DropDownList ID="cboBilheteTipo" runat="server" Width="100%" CssClass="configCombo" EnableTheming="True" TabIndex="6"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvIdTipoBilhete" runat="server" ControlToValidate="cboBilheteTipo" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="hdfId_Bilhete" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador2" runat="server" Style="z-index: 107; left: 216px; position: absolute; top: 430px; height: 42px; width: 175px;">
        <cc1:ValidatorCalloutExtender ID="vceDataLote" runat="server" TargetControlID="rfvDataLote"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeDataLote" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="9999/99" TargetControlID="txtDataLote"></cc1:MaskedEditExtender>
        <cc1:ValidatorCalloutExtender ID="vceData" runat="server" TargetControlID="rfvData"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeData" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtData"></cc1:MaskedEditExtender>
        <cc1:ValidatorCalloutExtender ID="vceAtivo" runat="server" TargetControlID="rfvAtivo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceNotaFiscal" runat="server" TargetControlID="rfvNotaFiscal"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceValorFatura" runat="server" TargetControlID="rfvValorFatura"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeValorFatura" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" ErrorTooltipEnabled="True" Mask="99999999.99" MaskType="Number" TargetControlID="txtValorFatura"></cc1:MaskedEditExtender>
        <cc1:ValidatorCalloutExtender ID="vceIdTipoBilhete" runat="server" TargetControlID="rfvIdTipoBilhete"></cc1:ValidatorCalloutExtender>
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
        <asp:LinkButton ID="btConfiguracao" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btConfiguracao_Click">
            <i class="fas fa-cog"></i>
            <br />
            <span id="lblConfig" runat="server">Config</span>
        </asp:LinkButton>
    </div>

</asp:Content>
