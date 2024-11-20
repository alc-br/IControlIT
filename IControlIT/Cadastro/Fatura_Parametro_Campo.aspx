<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Fatura_Parametro_Campo.aspx.vb" Inherits="IControlIT.Fatura_Parametro_Campo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Observacao-->
    <div id="pnlObservacao" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Observação" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                    <asp:TextBox ID="txtObservacaoObrigatoria" runat="server" CssClass="configtext" MaxLength="300" Style="float: left; border-radius: 6px 6px;" TextMode="MultiLine" Width="100%" Height="350px" TabIndex="7"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvObservacao" runat="server" ControlToValidate="txtObservacaoObrigatoria" Font-Names="Arial" Font-Size="10pt" Style="left: 445px; top: 38px; float: left;" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btCancela" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btOk" class="btn btn-success" runat="server" Text="Confirmar" CausesValidation="False" />
                    <asp:HiddenField ID="hfdId_Aparelho" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <!--Registro Adicionais-->
    <div id="pnlRegistro" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescRegistro" runat="server" CssClass="configlabel" Text="Dados Adicionais" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblObservacao" runat="server" CssClass="configlabel" Text="Observação" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:TextBox ID="txtObservacao" runat="server" Style="width: 100%; min-height: 150px; font-size: 9pt" TextMode="MultiLine" TabIndex="8" ForeColor="#FF9900" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblFinalidade" runat="server" CssClass="configlabel" Text="Finalidade" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:TextBox ID="txtFinalidade" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="100%" CssClass="configtext"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblIdentificacaoModal" runat="server" CssClass="configlabel" Text="Chave do banco" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:TextBox ID="txtIdentificacaoModal" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                </div>
            </div>
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
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="* Descrição"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="25" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblFaturaParametro" runat="server" CssClass="configlabel" Text="* Tipo da fatura"></asp:Label>
                            <asp:DropDownList ID="cboFaturaParametro" runat="server" Width="100%" CssClass="configCombo" EnableTheming="True" TabIndex="2"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvFaturaParametro" runat="server" ControlToValidate="cboFaturaParametro" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-lleft">
                            <asp:Label ID="lblCriarBilhete" runat="server" CssClass="configlabel" Text="Criar Bilhete"></asp:Label>
                            <br />
                            <div style="position: relative; width: 100%">
                                <asp:RadioButtonList ID="optSinal" runat="server" Style="left: 0; position: absolute" ForeColor="Black" RepeatDirection="Horizontal" TabIndex="3">
                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                    <asp:ListItem Value="2">Nao</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div style="height: 15px"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco"></asp:Label>
                            <asp:TextBox ID="txtIdentificacao" runat="server" Width="100%" CssClass="configtext" ReadOnly="True" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 126px; position: absolute; top: 410px; height: 88px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceFaturaParametro" runat="server" TargetControlID="rfvFaturaParametro"></cc1:ValidatorCalloutExtender>
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
        <asp:LinkButton ID="btPDF" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span id="lblPdf" runat="server">PDF</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAbrir_Click">
            <i class="fas fa-folder-open"></i>
            <br />
            <span>Dados</span>
        </asp:LinkButton>
    </div>

</asp:Content>
