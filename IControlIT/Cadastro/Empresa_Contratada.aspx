<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Empresa_Contratada.aspx.vb" Inherits="IControlIT.Empresa_Contratada" %>

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
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="* Descricao"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCNPJ" runat="server" CssClass="configlabel" Text="* CPNJ"></asp:Label>
                            <asp:TextBox ID="txtCNPJ" runat="server" CssClass="configtext" Width="100%" MaxLength="20" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCNPJ" runat="server" ControlToValidate="txtCNPJ" Display="None" Height="16px" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;" Width="47px"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevCNPJ" runat="server" ControlToValidate="txtCNPJ" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevCNPJ" InvalidValueBlurredMessage="*" ValidationGroup="MKE" ControlExtender="meeCNPJ"></cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblContato" runat="server" CssClass="configlabel" Text="Contato"></asp:Label>
                            <asp:TextBox ID="txtContato" runat="server" CssClass="configtext" Width="100%" MaxLength="50" TabIndex="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblTelefone" runat="server" CssClass="configlabel" Text="Observa��o"></asp:Label>
                            <asp:TextBox ID="txtTelefone" runat="server" CssClass="configtext" Width="100%" Height="250px" MaxLength="200" TabIndex="4" TextMode="MultiLine"></asp:TextBox>
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

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 172px; position: absolute; top: 334px; height: 37px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceCNPJ" runat="server" TargetControlID="rfvCNPJ"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeCNPJ" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True" Mask="99,999,999/9999-99" TargetControlID="txtCNPJ"></cc1:MaskedEditExtender>
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
        <asp:LinkButton ID="btDesativar" runat="server" CssClass="btn-menu-toolbar" OnClientClick="return confirm('Voc� deseja desativa o registro?');" OnClick="btDesativar_Click">
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
