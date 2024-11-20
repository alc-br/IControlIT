<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Contrato_SLA_Operacao.aspx.vb" Inherits="IControlIT.Contrato_SLA_Operacao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblContrato" runat="server" CssClass="configlabel" Text="* Contrato"></asp:Label>
                            <asp:DropDownList ID="cboContrato" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="1" Width="100%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvContrato" runat="server" ControlToValidate="cboContrato" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lbDescricao" runat="server" CssClass="configlabel" Text="* Descrição"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" Height="50px" MaxLength="100" TextMode="MultiLine" Width="100%" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblPrazoDias" runat="server" CssClass="configlabel" Text="Prazo em Dias"></asp:Label>
                            <asp:TextBox ID="txtPrazoDias" runat="server" CssClass="configtext" Width="100%" MaxLength="4" TabIndex="3"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevPrazoDias" runat="server" ControlExtender="meePrazoDias" ControlToValidate="txtPrazoDias" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevPrazoDias" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblMulta" runat="server" CssClass="configlabel">Valor</asp:Label>
                            <asp:TextBox ID="txtValorSLA" runat="server" CssClass="configtext" MaxLength="10" Width="100%" TabIndex="4"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevValorSLA" runat="server" ControlExtender="meeValorSLA" ControlToValidate="txtValorSLA" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevValorSLA" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"> </cc1:MaskedEditValidator>
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

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 208px; position: absolute; top: 382px; height: 115px; margin-bottom: 0px;">
        <cc1:ValidatorCalloutExtender ID="vceContrato" runat="server" TargetControlID="rfvContrato"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meePrazoDias" runat="server" AcceptNegative="Left" Mask="9999" MaskType="Number" TargetControlID="txtPrazoDias"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeValorSLA" runat="server" AcceptNegative="Left" DisplayMoney="Right" Mask="99999.99" MaskType="Number" TargetControlID="txtValorSLA"></cc1:MaskedEditExtender>
        <asp:Label ID="lblMessage" runat="server" Style="left: 13px; top: 87px"> </asp:Label>
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

