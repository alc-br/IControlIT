<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Filial.aspx.vb" Inherits="IControlIT.Filial" %>

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
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" EnableTheming="True" SetFocusOnError="True" Style="font-size: 10pt; z-index: 130; left: 584px; color: red; font-family: Arial; top: 35px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblEmpresa" runat="server" CssClass="configlabel" Text="Empresa"></asp:Label>
                            <asp:DropDownList ID="cboEmpresa" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblCNPJ" runat="server" CssClass="configlabel" Text="* CNPJ"></asp:Label>
                            <asp:TextBox ID="txtCNPJ" runat="server" CssClass="configtext" MaxLength="20" Width="100%" TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCNPJ" runat="server" ControlToValidate="txtCNPJ" Display="None" EnableTheming="True" SetFocusOnError="True" Style="font-size: 10pt; z-index: 130; left: 419px; color: red; font-family: Arial; top: 61px; float: left;"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevCNPJ" runat="server" ControlExtender="meeCNPJ" ControlToValidate="txtCNPJ" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevCNPJ" InvalidValueBlurredMessage="*" Style="left: 364px; top: 62px; float: left;" ValidationGroup="MKE" Width="48px"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblEndereco" runat="server" CssClass="configlabel" Text="Endereço"></asp:Label>
                            <asp:TextBox ID="txtEndereco" runat="server" CssClass="configtext" MaxLength="300" TextMode="MultiLine" Width="100%" TabIndex="4"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblVisaoHierarquia" runat="server" CssClass="configlabel" Width="100%" Text="Visão Relatório"></asp:Label>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 120px;">
                                        <asp:CheckBox ID="chkDepartamento" runat="server" Style="float: left" CssClass="configchekbox" Text="Departamento" ForeColor="Black" />
                                    </td>

                                    <td style="width: 120px;">
                                        <asp:CheckBox ID="chkSetor" runat="server" Style="float: left" CssClass="configchekbox" Text="Setor" ForeColor="Black" />
                                    </td>

                                    <td style="width: 120px;">
                                        <asp:CheckBox ID="chkSecao" runat="server" Style="float: left" CssClass="configchekbox" Text="Seção" ForeColor="Black" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <!--Relacionamento-->
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-warning">
                                <div class="panel-heading row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblMontaHierarquia" runat="server" CssClass="configlabel" Text="Permissão para Requisição" Font-Bold="False"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <div style="width: 100%; border: 1px solid #818181; border-radius: 4px">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtGrupo" runat="server" CssClass="configtext" Style="width: 100%; border: none" BackColor="Transparent"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 30px; background-color: #09A8C5; border-radius: 0px 4px 4px 0px">
                                                        <asp:LinkButton ID="btGrupo" runat="server" CssClass="nav-link" OnClick="btGrupo_Click">
                                                            <i class="fas fa-search" style="color: #FFFFFF; font-size: 9pt"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div style="height: 5px"></div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="width: 100%; border: 1px solid #818181; border-radius: 4px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:ListBox ID="lstOrigem" runat="server" CssClass="configlistboxAbrir border-0" ForeColor="Black" Height="90px" Width="100%"></asp:ListBox>
                                                    </td>
                                                    <td style="width: 45px; background-color: #09A8C5; text-align: center; border-radius: 0px 4px 4px 0px">
                                                        <asp:LinkButton ID="btMoveSelecionado" runat="server" CssClass="nav-link" Style="position: relative; width: 100%; height: 90px;" OnClick="btGrupo_Click">
                                                            <i class="fas fa-caret-down" style="color: #FFFFFF; font-size: 14pt; position: absolute; top: 50%; transform: translateY(-50%); right: 35%"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="height: 5px"></div>
                                    <div class="col-md-12">
                                        <div style="width: 100%; border: 1px solid #818181; border-radius: 4px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:ListBox ID="lstDestino" runat="server" CssClass="configlistboxAbrir border-0" ForeColor="Black" Height="90px" Width="100%"></asp:ListBox>
                                                    </td>
                                                    <td style="width: 45px; background-color: #09A8C5; text-align: center; border-radius: 0px 4px 4px 0px">
                                                        <asp:LinkButton ID="btMoveSelecao" runat="server" CssClass="nav-link" Style="position: relative; width: 100%; height: 90px;" OnClick="btGrupo_Click">
                                                            <i class="fas fa-caret-up" style="color: #FFFFFF; font-size: 14pt; position: absolute; top: 50%; transform: translateY(-50%); right: 35%"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
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

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 104; left: 321px; position: absolute; top: 587px; height: 37px; width: 133px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceCNPJ" runat="server" TargetControlID="rfvCNPJ"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeCNPJ" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True" Mask="99,999,999/9999-99" TargetControlID="txtCNPJ"></cc1:MaskedEditExtender>
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
    </div>

</asp:Content>
