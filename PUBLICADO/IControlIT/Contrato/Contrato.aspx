<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Contrato.aspx.vb" Inherits="IControlIT.Contrato" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblNumeroContrato" runat="server" CssClass="configlabel" Style="left: 9px; top: 39px;" Text="* Número do contrato"></asp:Label>
                            <asp:TextBox ID="txtNumeroContrato" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNumeroContrato" runat="server" ControlToValidate="txtNumeroContrato" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lbDescricao" runat="server" CssClass="configlabel" Text="* Breve descrição"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" Height="36px" MaxLength="100" TextMode="MultiLine" Width="100%" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblStatus" runat="server" CssClass="configlabel" Text="* Status"></asp:Label>
                            <asp:DropDownList ID="cboStatus" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="3"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="cboStatus" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblServico" runat="server" CssClass="configlabel" Text="* Serviço "></asp:Label>
                            <asp:DropDownList ID="cboServico" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="4"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvServico" runat="server" ControlToValidate="cboServico" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDataInicioVigencia" runat="server" CssClass="configlabel" Text="* Data do inicio da vigência"></asp:Label>
                            <asp:TextBox ID="txtDataInicioVigencia" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="5"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDataInicioVigencia" runat="server" ControlToValidate="txtDataInicioVigencia" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevDataInicioVigencia" runat="server" ControlExtender="meeDataInicioVigencia" ControlToValidate="txtDataInicioVigencia" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataInicioVigencia" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDataFimVigencia" runat="server" CssClass="configlabel" Text="Data do fim da vigência"></asp:Label>
                            <asp:TextBox ID="txtDataFimVigencia" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="6"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevDataFimVigencia" runat="server" ControlExtender="meeDataFimVigencia" ControlToValidate="txtDataFimVigencia" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataFimVigencia" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblFilial" runat="server" CssClass="configlabel" Text="Filial contratante"></asp:Label>
                            <asp:DropDownList ID="cboFilial" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="7"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblEmpresaContratada" runat="server" CssClass="configlabel" Text="* Empresa contratada"></asp:Label>
                            <asp:DropDownList ID="cboEmpresaContratada" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="8"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEmpresaContratada" runat="server" ControlToValidate="cboEmpresaContratada" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblObjeto" runat="server" CssClass="configlabel" Text="* Objeto"></asp:Label>
                            <asp:TextBox ID="txtObjeto" runat="server" CssClass="configtext" Height="81px" MaxLength="318" TextMode="MultiLine" Width="100%" TabIndex="9"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvObjeto" runat="server" ControlToValidate="txtObjeto" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco"></asp:Label>
                            <asp:TextBox ID="txtIdentificacao" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblIndice" runat="server" CssClass="configlabel" Text="Índice Reajuste"></asp:Label>
                            <asp:DropDownList ID="cboIndice" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="7"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 315px; position: absolute; top: 616px; height: 59px;">
        <cc1:ValidatorCalloutExtender ID="vceNumeroContrato" runat="server" TargetControlID="rfvNumeroContrato"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceStatus" runat="server" TargetControlID="rfvStatus"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceServico" runat="server" TargetControlID="rfvServico"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceDataInicioVigencia" runat="server" TargetControlID="rfvDataInicioVigencia"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceEmpresaContratada" runat="server" TargetControlID="rfvEmpresaContratada"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeDataInicioVigencia" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataInicioVigencia"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeDataFimVigencia" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataFimVigencia"></cc1:MaskedEditExtender>
        <cc1:ValidatorCalloutExtender ID="vceObjeto" runat="server" TargetControlID="rfvObjeto"></cc1:ValidatorCalloutExtender>
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
