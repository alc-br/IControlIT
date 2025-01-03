<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Bilhete_Tipo.aspx.vb" Inherits="IControlIT.Bilhete_Tipo" %>

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
                            <asp:Label ID="lblTipo" runat="server" CssClass="configlabel" Text="* Tipo"></asp:Label>
                            <asp:TextBox ID="txtTipo" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTipo" runat="server" ControlToValidate="txtTipo" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="Descri��o"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblConglomerado" runat="server" CssClass="configlabel" Text="Fornecedor"></asp:Label>
                            <asp:DropDownList ID="cboConglomerado" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="4"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvConglomerado" runat="server" ControlToValidate="cboConglomerado" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDescricaoBilhete" runat="server" CssClass="configlabel" Text="View do bilhete"></asp:Label>
                            <asp:RadioButtonList ID="optTipoDescricao" runat="server" RepeatDirection="Horizontal" TabIndex="5" CssClass="configchekbox" ForeColor="Black">
                                <asp:ListItem Selected="True" Value="1">Tipo</asp:ListItem>
                                <asp:ListItem Value="2">Descricao</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblUnidade" runat="server" CssClass="configlabel" Text="View da unidade"></asp:Label>
                            <asp:RadioButtonList ID="optUnidade" runat="server" RepeatDirection="Horizontal" TabIndex="3" CssClass="configchekbox" ForeColor="Black">
                                <asp:ListItem Selected="True" Value="1">Date Time</asp:ListItem>
                                <asp:ListItem Value="2">Decimal</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco"></asp:Label>
                            <asp:TextBox ID="txtIdentificacao" runat="server" CssClass="configtext" ReadOnly="True" Width="320px" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 25px; position: absolute; top: 449px; height: 75px;">
        <cc1:ValidatorCalloutExtender ID="vceTipo" runat="server" TargetControlID="rfvTipo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceConglomerado" runat="server" TargetControlID="rfvConglomerado"></cc1:ValidatorCalloutExtender>
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
