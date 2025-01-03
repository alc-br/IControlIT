<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Data_Parada.aspx.vb" Inherits="IControlIT.Data_Parada" %>

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
                            <asp:Label ID="lblData" runat="server" CssClass="configlabel" Text="* Data"></asp:Label>
                            <asp:TextBox ID="txtData" runat="server" MaxLength="19" Width="100%" CssClass="configtext" TabIndex="1"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevData" runat="server" ControlExtender="meeData" ControlToValidate="txtData" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataAtivacao" InvalidValueBlurredMessage="*" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="* Descri��o"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" Width="100%" AutoPostBack="True" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
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

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 289px; position: absolute; top: 300px; height: 38px; width: 130px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeData" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtData"></cc1:MaskedEditExtender>
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
        <asp:LinkButton ID="btDesativar" runat="server" CssClass="btn-menu-toolbar" OnClientClick="return confirm('Voc� deseja desativa o registro?');" OnClick="btDesativar_Click">
            <i class="fas fa-recycle"></i>
            <br />
            <span id="lblEncerrar" runat="server">Excluir</span>
        </asp:LinkButton>
        <asp:LinkButton ID="bt_Recalcular" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="bt_Recalcular_Click">
            <i class="fas fa-calculator"></i>
            <br />
            <span id="lblCalcular" runat="server">Recalcular</span>
        </asp:LinkButton>
    </div>

</asp:Content>
