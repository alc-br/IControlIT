<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Ativo_Tipo.aspx.vb" Inherits="IControlIT.Ativo_Tipo" %>

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
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="* Descrição"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblGrupoAtivoTipo" runat="server" CssClass="configlabel" Text="* Grupo"></asp:Label>
                            <asp:DropDownList ID="cboGrupoAtivoTipo" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="2"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvGrupoAtivoTipo" runat="server" ControlToValidate="cboGrupoAtivoTipo" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblImagem" runat="server" CssClass="configlabel" Text="* Imagem"></asp:Label>
                            <asp:TextBox ID="txtImagem" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvImagem" runat="server" ControlToValidate="txtImagem" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblPhoto" runat="server" CssClass="configlabel" Text="* Photo"></asp:Label>
                            <asp:TextBox ID="txtPhoto" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPhoto" runat="server" ControlToValidate="txtPhoto" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblEstoqueRegulador" runat="server" CssClass="configlabel" Text="Estoque Regulador"></asp:Label>
                            <asp:TextBox ID="txtEstoqueRegulador" runat="server" CssClass="configtext" Width="100%" TabIndex="5" MaxLength="5"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevEstoqueRegulador" runat="server" ControlExtender="meeEstoqueRegulador" ControlToValidate="txtEstoqueRegulador" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevEstoqueRegulador" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblTipoGrupo" runat="server" CssClass="configlabel" Text="Tipo Grupo"></asp:Label>
                            <asp:DropDownList ID="cboTipoGrupo" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="6"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblSubGrupo" runat="server" CssClass="configlabel" Text="Sub Grupo"></asp:Label>
                            <asp:DropDownList ID="cboSubGrupo" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="7"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblConglomerado" runat="server" CssClass="configlabel" Text="Fornecedor"></asp:Label>
                            <asp:DropDownList ID="cboConglomerado" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="7"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco"></asp:Label>
                            <asp:TextBox ID="txtIdentificacao" runat="server" CssClass="configtext" ReadOnly="True" Style="float: left;" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 411px; position: absolute; top: 337px; height: 34px; width: 122px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vcevGrupoAtivoTipo" runat="server" TargetControlID="rfvGrupoAtivoTipo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceImagem" runat="server" TargetControlID="rfvImagem"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vcePhoto" runat="server" TargetControlID="rfvPhoto"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeEstoqueRegulador" runat="server" AcceptNegative="Left" Mask="999" MaskType="Number" TargetControlID="txtEstoqueRegulador"></cc1:MaskedEditExtender>
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
        <asp:LinkButton ID="btPDF" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span id="lblPdf" runat="server">PDF</span>
        </asp:LinkButton>
    </div>

</asp:Content>
