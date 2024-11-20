<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Ativo_Tipo_Grupo_Tipo.aspx.vb" Inherits="IControlIT.Ativo_Tipo_Grupo_Tipo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register src="../Localizar.ascx" tagname="Localizar" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="* Descrição"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" Width="100%" AutoPostBack="True" TabIndex="1" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
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

    <asp:Panel id="pnlValidador" runat="server" style="z-index: 107; left: 289px; position: absolute; top: 300px; height: 38px; width: 130px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <asp:Label id="lblMessage" runat="server" style="left: 13px; top: 87px"></asp:Label>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    
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
