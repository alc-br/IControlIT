<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Tipo_Solicitcao.aspx.vb" Inherits="IControlIT.Tipo_Solicitcao" %>

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
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblGrupoAtivoTipo" runat="server" CssClass="configlabel" Text="* Tipo do Ativo"></asp:Label>
                            <asp:DropDownList ID="cboAtivoTipo" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="2" OnSelectedIndexChanged="cboAtivoTipo_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAtivoTipo" runat="server" ControlToValidate="cboAtivoTipo" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblSLAAtendimento" runat="server" CssClass="configlabel" Text="* SLA de Atendimento"></asp:Label>
                            <asp:DropDownList ID="cboSLAAtendimeto" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="3"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSLAAtendimento" runat="server" ControlToValidate="cboSLAAtendimeto" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblFilaAtendiemtno" runat="server" CssClass="configlabel" Text="* Fila de Atendimento"></asp:Label>
                            <asp:DropDownList ID="cboFilaAtendimento" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="4"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvFilaAtendimento" runat="server" ControlToValidate="cboFilaAtendimento" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblAprovacao" runat="server" CssClass="configlabel" Text="Caixa de Texto"></asp:Label>
                            <asp:TextBox ID="txtCaixaTexto" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="300" Width="100%" CssClass="configtext" TabIndex="5" Height="80px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblGrupoSolicitacao" runat="server" CssClass="configlabel" Text="* Grupo"></asp:Label>
                            <asp:DropDownList ID="cboGrupoSolicitacao" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="6"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvGrupoSolicitacao" runat="server" ControlToValidate="cboGrupoSolicitacao" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblSolicitarMatricula" runat="server" CssClass="configlabel" Text="* Ação Unidade"></asp:Label>
                            <asp:DropDownList ID="cboSolicitacaoMatricula" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="7"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSolicitacaoMatricula" runat="server" ControlToValidate="cboSolicitacaoMatricula" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
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

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 411px; position: absolute; top: 337px; height: 34px; width: 122px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceAtivoTipo" runat="server" TargetControlID="rfvAtivoTipo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceSLAAtendimento" runat="server" TargetControlID="rfvSLAAtendimento"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceFilaAtendimento" runat="server" TargetControlID="rfvFilaAtendimento"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceGrupoSolicitacao" runat="server" TargetControlID="rfvGrupoSolicitacao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceSolicitacaoMatricula" runat="server" TargetControlID="rfvSolicitacaoMatricula"></cc1:ValidatorCalloutExtender>

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
