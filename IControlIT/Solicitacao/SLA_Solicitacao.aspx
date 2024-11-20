<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="SLA_Solicitacao.aspx.vb" Inherits="IControlIT.SLA_Solicitacao" %>

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
                        <div class="col-md-12">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="* Descrição"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" Width="100%" AutoPostBack="True" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblQTDHoras" runat="server" CssClass="configlabel" Text="* Prazo de atendimento"></asp:Label>
                            <asp:DropDownList ID="cboQTDHoras" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="4" Width="100%" ViewStateMode="Inherit">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="2">2 Horas</asp:ListItem>
                                <asp:ListItem Value="4">4 Horas</asp:ListItem>
                                <asp:ListItem Value="6">6 Horas</asp:ListItem>
                                <asp:ListItem Value="8">8 Horas</asp:ListItem>
                                <asp:ListItem Value="10">10 Horas</asp:ListItem>
                                <asp:ListItem Value="12">12 Horas</asp:ListItem>
                                <asp:ListItem Value="14">14 Horas</asp:ListItem>
                                <asp:ListItem Value="16">16 Horas</asp:ListItem>
                                <asp:ListItem Value="18">18 Horas</asp:ListItem>
                                <asp:ListItem Value="20">20 Horas</asp:ListItem>
                                <asp:ListItem Value="22">22 Horas</asp:ListItem>
                                <asp:ListItem Value="24">1 Dia</asp:ListItem>
                                <asp:ListItem Value="48">2 Dias</asp:ListItem>
                                <asp:ListItem Value="72">3 Dias</asp:ListItem>
                                <asp:ListItem Value="96">4 Dias</asp:ListItem>
                                <asp:ListItem Value="120">5 Dias</asp:ListItem>
                                <asp:ListItem Value="144">6 Dias</asp:ListItem>
                                <asp:ListItem Value="168">7 Dias</asp:ListItem>
                                <asp:ListItem Value="192">8 Dias</asp:ListItem>
                                <asp:ListItem Value="216">9 Dias</asp:ListItem>
                                <asp:ListItem Value="240">10 Dias</asp:ListItem>
                                <asp:ListItem Value="264">11 Dias</asp:ListItem>
                                <asp:ListItem Value="288">12 Dias</asp:ListItem>
                                <asp:ListItem Value="312">13 Dias</asp:ListItem>
                                <asp:ListItem Value="336">14 Dias</asp:ListItem>
                                <asp:ListItem Value="360">15 Dias</asp:ListItem>
                                <asp:ListItem Value="384">16 Dias</asp:ListItem>
                                <asp:ListItem Value="408">17 Dias</asp:ListItem>
                                <asp:ListItem Value="432">18 Dias</asp:ListItem>
                                <asp:ListItem Value="456">19 Dias</asp:ListItem>
                                <asp:ListItem Value="480">20 Dias</asp:ListItem>
                                <asp:ListItem Value="504">21 Dias</asp:ListItem>
                                <asp:ListItem Value="528">22 Dias</asp:ListItem>
                                <asp:ListItem Value="552">23 Dias</asp:ListItem>
                                <asp:ListItem Value="576">24 Dias</asp:ListItem>
                                <asp:ListItem Value="600">25 Dias</asp:ListItem>
                                <asp:ListItem Value="624">26 Dias</asp:ListItem>
                                <asp:ListItem Value="648">27 Dias</asp:ListItem>
                                <asp:ListItem Value="672">28 Dias</asp:ListItem>
                                <asp:ListItem Value="696">29 Dias</asp:ListItem>
                                <asp:ListItem Value="720">30 Dias</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvQTDHoras" runat="server" ControlToValidate="cboQTDHoras" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblEmail" runat="server" CssClass="configlabel" Text="* Email"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="configtext" MaxLength="50" Width="100%" AutoPostBack="True" TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
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

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 289px; position: absolute; top: 300px; height: 38px; width: 130px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceQTDHoras" runat="server" TargetControlID="rfvQTDHoras"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceEmail" runat="server" TargetControlID="rfvEmail"></cc1:ValidatorCalloutExtender>
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
