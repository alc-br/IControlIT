<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Nota_Fiscal.aspx.vb" Inherits="IControlIT.Nota_Fiscal" %>

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
                            <asp:Label ID="lblNotaFiscal" runat="server" CssClass="configlabel" Text="* Número da NF"></asp:Label>
                            <asp:TextBox ID="txtNotaFiscal" runat="server" CssClass="configtext" Width="100%" MaxLength="50" TabIndex="9"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNotaFiscal" runat="server" ControlToValidate="txtNotaFiscal" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblData" runat="server" CssClass="configlabel" Text="* Data Nota Fiscal"></asp:Label>
                            <asp:TextBox ID="txtData" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="10" AutoPostBack="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvData" runat="server" ControlToValidate="txtData" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevData" runat="server" ControlExtender="meeData" ControlToValidate="txtData" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevData" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblObservacao" runat="server" CssClass="configlabel" Text="Observação"></asp:Label>
                            <asp:TextBox ID="txtObservacao" runat="server" CssClass="configtext" Height="60px" MaxLength="300" TextMode="MultiLine" Width="100%" TabIndex="11"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblFormaAquisicao" runat="server" CssClass="configlabel" Text="* Forma de aquisição"></asp:Label>
                            <asp:DropDownList ID="cboFormaAquisicao" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="13"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvFormaAquisicao" runat="server" ControlToValidate="cboFormaAquisicao" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDataInicioFormaAquisicao" runat="server" CssClass="configlabel" Text="Data de inicio"></asp:Label>
                            <asp:TextBox ID="txtDataInicioFormaAquisicao" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="14"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevDataInicioFormaAquisicao" runat="server" ControlExtender="meeDataInicioFormaAquisicao" ControlToValidate="txtDataInicioFormaAquisicao" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataInicioFormaAquisicao" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblValorFormaAquisicao" runat="server" CssClass="configlabel" Text="Valor Unitário"></asp:Label>
                            <asp:TextBox ID="txtValorFormaAquisicao" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="15"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevValorFormaAquisicao" runat="server" ControlExtender="meeValorFormaAquisicao" ControlToValidate="txtValorFormaAquisicao" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevValorFormaAquisicao" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblMesFormaAquisicao" runat="server" CssClass="configlabel" Text="Quantidade de meses residuais"></asp:Label>
                            <asp:TextBox ID="txtMesFormaAquisicao" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="16"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevMesFormaAquisicao" runat="server" ControlExtender="meeMesFormaAquisicao" ControlToValidate="txtMesFormaAquisicao" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevMesFormaAquisicao" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco"></asp:Label>
                            <asp:TextBox ID="txtIdentificacao" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador2" runat="server" Style="z-index: 107; left: 636px; position: absolute; top: 594px; height: 56px; width: 176px;">
        <cc1:ValidatorCalloutExtender ID="vceNotaFiscal" runat="server" TargetControlID="rfvNotaFiscal"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceData" runat="server" TargetControlID="rfvData"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceFormaAquisicao" runat="server" TargetControlID="rfvFormaAquisicao"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeData" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtData"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeDataInicioFormaAquisicao" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataInicioFormaAquisicao"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeValorFormaAquisicao" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" ErrorTooltipEnabled="True" Mask="99999.99" MaskType="Number" TargetControlID="txtValorFormaAquisicao"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeMesFormaAquisicao" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" ErrorTooltipEnabled="True" Mask="99" TargetControlID="txtMesFormaAquisicao"></cc1:MaskedEditExtender>
        <asp:Label ID="lblMessage" runat="server" Style="left: 13px; top: 87px">
        </asp:Label>
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
        <asp:LinkButton ID="btPDF" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span id="lblPdf" runat="server">PDF</span>
        </asp:LinkButton>
    </div>

</asp:Content>
