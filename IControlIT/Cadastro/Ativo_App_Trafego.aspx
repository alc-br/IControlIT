<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Ativo_App_Trafego.aspx.vb" Inherits="IControlIT.Ativo_App_Trafego" %>

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
                            <asp:Label ID="lblAtivoConta" runat="server" CssClass="configlabel" Text="* Ativo"></asp:Label>
                            <asp:TextBox ID="txtAtivoConta" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAtivoConta" runat="server" ControlToValidate="txtAtivoConta" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCorteDe" runat="server" CssClass="configlabel" Text="* Corte De"></asp:Label>
                            <asp:TextBox ID="txtCorteDe" runat="server" CssClass="configtext" MaxLength="2" Width="100%" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCorteDe" runat="server" ControlToValidate="txtCorteDe" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevCorteDe" runat="server" ControlExtender="meeCorteDe" ControlToValidate="txtCorteDe" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevEstoqueRegulador" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblCorteAte" runat="server" CssClass="configlabel" Text="* Corte Até"></asp:Label>
                            <asp:TextBox ID="txtCorteAte" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCorteAte" runat="server" ControlToValidate="txtCorteAte" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevCorteAte" runat="server" ControlExtender="meeCorteAte" ControlToValidate="txtCorteAte" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevEstoqueRegulador" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblPacoteMB" runat="server" CssClass="configlabel" Text="* Pacote Mb"></asp:Label>
                            <asp:TextBox ID="txtPacoteMB" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPacoteMb" runat="server" ControlToValidate="txtPacoteMB" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevPacoteMb" runat="server" ControlExtender="meePacoteMb" ControlToValidate="txtPacoteMB" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevEstoqueRegulador" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblMes" runat="server" CssClass="configlabel" Text="* Soma/Diminí Mês Vencimento "></asp:Label>
                            <asp:TextBox ID="txtMes" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="5"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMes" runat="server" ControlToValidate="txtMes" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevMes" runat="server" ControlExtender="meeMes" ControlToValidate="txtMes" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevEstoqueRegulador" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco"></asp:Label>
                            <asp:TextBox ID="txtIdentificacao" runat="server" CssClass="configtext" ReadOnly="True" Style="float: left;" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 411px; position: absolute; top: 337px; height: 34px; width: 122px;">
        <cc1:ValidatorCalloutExtender ID="vceAtivoConta" runat="server" TargetControlID="rfvAtivoConta"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceCorteDe" runat="server" TargetControlID="rfvCorteDe"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceCorteAte" runat="server" TargetControlID="rfvCorteAte"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vcePacoteMb" runat="server" TargetControlID="rfvPacoteMb"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeCorteDe" runat="server" AcceptNegative="Left" Mask="99" MaskType="Number" TargetControlID="txtCorteDe"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeCorteAte" runat="server" AcceptNegative="Left" Mask="99" MaskType="Number" TargetControlID="txtCorteAte"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meePacoteMb" runat="server" AcceptNegative="Left" Mask="999999" MaskType="Number" TargetControlID="txtPacoteMB"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeMes" runat="server" AcceptNegative="Left" Mask="9" MaskType="Number" TargetControlID="txtMes"></cc1:MaskedEditExtender>
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
        <asp:LinkButton ID="btPdf" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span id="lblInformacoes" runat="server">PDF</span>
        </asp:LinkButton>
    </div>

</asp:Content>
