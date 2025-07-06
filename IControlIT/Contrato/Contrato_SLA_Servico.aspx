<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Contrato_SLA_Servico.aspx.vb" Inherits="IControlIT.Contrato_SLA_Servico" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--registro adicionais-->
    <div id="pnlRegistro" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescRegistro" runat="server" CssClass="configlabel" Text="Dados Adicionais" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
                <div style="height: 5px"></div>
                <div class="col-md-12">
                    <asp:Label ID="lblHistContrato" runat="server" CssClass="configlabel" Text="Contrato"></asp:Label>
                    <asp:TextBox ID="txtHistContrato" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" EnableTheming="True" TabIndex="1"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lbHistDescricao" runat="server" CssClass="configlabel" Text="Descrição"></asp:Label>
                    <asp:TextBox ID="txtHistDescricao" runat="server" CssClass="configtext" ReadOnly="True" EnableTheming="True" Width="100%" TabIndex="1"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblHistOperadora" runat="server" CssClass="configlabel" Text="Operadora"></asp:Label>
                    <asp:TextBox ID="txtHistOperadora" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" EnableTheming="True" TabIndex="1"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblHistTipoServico" runat="server" CssClass="configlabel" Text="Tipo do serviço"></asp:Label>
                    <asp:TextBox ID="txtHistTipoServico" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" EnableTheming="True" TabIndex="1"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblHistValor" runat="server" CssClass="configlabel" Text="Valor"></asp:Label>
                    <asp:TextBox ID="txtHistValor" runat="server" CssClass="configtext" ReadOnly="True" MaxLength="10" Width="100%" TabIndex="1"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblHistIndice" runat="server" CssClass="configlabel" Text="Índice"></asp:Label>
                    <asp:TextBox ID="txtHistIndice" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" EnableTheming="True" TabIndex="1"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblDtAlteracao" runat="server" CssClass="configlabel" Text="Índice"></asp:Label>
                    <asp:TextBox ID="txtDtAlteracao" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" EnableTheming="True" TabIndex="1"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblHistIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco"></asp:Label>
                    <asp:TextBox ID="txtHistIdentificacao" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900" EnableTheming="True" TabIndex="1"></asp:TextBox>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFechar_Registro" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblContrato" runat="server" CssClass="configlabel" Text="* Contrato"></asp:Label>
                            <asp:DropDownList ID="cboContrato" runat="server" CssClass="configCombo" Width="100%" EnableTheming="True" TabIndex="1"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvContrato" runat="server" ControlToValidate="cboContrato" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="* Descrição"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" Height="50px" MaxLength="100" TextMode="MultiLine" Width="100%" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblOperadora" runat="server" CssClass="configlabel" Text="* Operadora"></asp:Label>
                            <asp:DropDownList ID="cboOperadora" runat="server" CssClass="configCombo" AutoPostBack="True" Width="100%" EnableTheming="True" TabIndex="1" OnSelectedIndexChanged="cboOperadora_Selected"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvOperadora" runat="server" ControlToValidate="cboOperadora" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-6">
                            <asp:Label ID="lblTipoServico" runat="server" CssClass="configlabel" Text="Tipo do serviço"></asp:Label>
                            <asp:DropDownList ID="cboTipoServico" runat="server" CssClass="configCombo" Width="100%" EnableTheming="True" TabIndex="1"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblValor" runat="server" CssClass="configlabel" Text="* Valor"></asp:Label>
                            <asp:TextBox ID="txtValorSLA" runat="server" CssClass="configtext" MaxLength="10" Width="100%" TabIndex="4"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevValorSLA" runat="server" ControlExtender="meeValorSLA" ControlToValidate="txtValorSLA" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevValorSLA" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                            <asp:RequiredFieldValidator ID="rfvValorSLA" runat="server" ControlToValidate="txtValorSLA" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblIndice" runat="server" CssClass="configlabel" Text="* Índice"></asp:Label>
                            <asp:DropDownList ID="cboIndice" runat="server" CssClass="configCombo" Width="100%" EnableTheming="True" TabIndex="1"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblPorcentagem" runat="server" CssClass="configlabel" Text="%"></asp:Label>
                            <asp:TextBox ID="txtPorcentagem" runat="server" CssClass="configCombo" Width="100%" EnableTheming="True" TabIndex="1"></asp:TextBox>
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

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 126px; position: absolute; top: 425px; height: 75px;">
        <cc1:ValidatorCalloutExtender ID="vceContrato" runat="server" TargetControlID="rfvContrato"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceOperadora" runat="server" TargetControlID="rfvOperadora"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceValorSLA" runat="server" TargetControlID="rfvValorSLA"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeValorSLA" runat="server" AcceptNegative="Left" DisplayMoney="Right" Mask="99999.9999" MaskType="Number" TargetControlID="txtValorSLA"></cc1:MaskedEditExtender>
        <asp:Label ID="lblMessage" runat="server" Style="left: 13px; top: 87px">
        </asp:Label>
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
        <asp:LinkButton ID="btRecalc" runat="server" CssClass="btn-menu-toolbar" OnClick="btRecalc_Click">
            <i class="fa fa-calculator"></i>
            <br />
            <span id="Span1" runat="server">Recalc</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAbrir_Click">
            <i class="fas fa-folder-open"></i>
            <br />
            <span>Dados</span>
        </asp:LinkButton>
    </div>

</asp:Content>
