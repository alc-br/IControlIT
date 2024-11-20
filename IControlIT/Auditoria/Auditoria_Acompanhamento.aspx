<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Auditoria_Acompanhamento.aspx.vb" Inherits="IControlIT.Auditoria_Acompanhamento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblLote" runat="server" CssClass="configlabel" Text="Lote"></asp:Label>
                            <asp:DropDownList ID="cboLote" runat="server" CssClass="configCombo" EnableTheming="True" AutoPostBack="True" Enabled="False" TabIndex="1" Width="100%"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblConta" runat="server" CssClass="configlabel" Text="Conta"></asp:Label>
                            <asp:DropDownList ID="cboConta" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" Enabled="False" TabIndex="2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblStatus" runat="server" CssClass="configlabel" Text="Status"></asp:Label>
                            <asp:DropDownList ID="cboStatus" runat="server" CssClass="configCombo" Enabled="False" EnableTheming="True" Width="100%" TabIndex="3"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="* Descrição" ForeColor="#009900"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" Height="80px" MaxLength="100" TextMode="MultiLine" Width="100%" TabIndex="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDataInicioFormaAquisicao" runat="server" CssClass="configlabel" Text="Data prevista"></asp:Label>
                            <asp:TextBox ID="txtDataPrevista" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="5"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevDataPrevista" runat="server" ControlExtender="meeDataPrevista" ControlToValidate="txtDataPrevista" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataPrevista" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                            <asp:RequiredFieldValidator ID="rfvDataPrevista" runat="server" ControlToValidate="txtDataPrevista" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblValorPrevisto" runat="server" CssClass="configlabel" Text="Valor previsto"></asp:Label>
                            <asp:TextBox ID="txtValorPrevisto" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="6"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevValorPrevisto" runat="server" ControlExtender="meeValorPrevisto" ControlToValidate="txtValorPrevisto" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevValorPrevisto" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                            <asp:RequiredFieldValidator ID="rfvValorPrevisto" runat="server" ControlToValidate="txtValorPrevisto" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
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

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 146px; position: absolute; top: 443px; height: 70px; width: 144px;">
        <cc1:ValidatorCalloutExtender ID="vceDeacricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceDataPrevista" runat="server" TargetControlID="rfvDataPrevista"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceValorPrevisto" runat="server" TargetControlID="rfvValorPrevisto"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeDataPrevista" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataPrevista"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeValorPrevisto" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" ErrorTooltipEnabled="True" Mask="999999.99" MaskType="Number" TargetControlID="txtValorPrevisto"></cc1:MaskedEditExtender>
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
