<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Fatura_Parametro.aspx.vb" Inherits="IControlIT.Fatura_Parametro" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--tipo rateio-->
    <div id="pnlRateio" runat="server" class="row" visible="false">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblConfiguracao" runat="server" CssClass="configlabel" Text="Configurações de Tipo de Rateio" Style="float: none; left: 5px; position: relative; top: 0px;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
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
                        <div class="col-md-12 text-right">
                            <asp:Button ID="btFecharGrupo" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                            <asp:Button ID="btSalvarGrupo" class="btn btn-success" runat="server" Text="Salvar" CausesValidation="False" />
                        </div>
                    </div>
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
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblContrato" runat="server" CssClass="configlabel" Text="* Contrato"></asp:Label>
                            <asp:DropDownList ID="cboContrato" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="2"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvContrato" runat="server" ControlToValidate="cboContrato" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblContaContabil" runat="server" CssClass="configlabel" Text="Conta contabil"></asp:Label>
                            <asp:TextBox ID="txtContaContabil" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="3"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCentroCustoVago" runat="server" CssClass="configlabel" Text="Centro de custo para crítica"></asp:Label>
                            <asp:TextBox ID="txtCentroCustoCritica" runat="server" CssClass="configtext" MaxLength="20" Width="100%" TabIndex="4"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblTipoRateio" runat="server" CssClass="configlabel" Text="* Tipo de rateio"></asp:Label>
                            <div class="input-group no-border">
                                <asp:DropDownList ID="cboRateio" runat="server" AutoPostBack="True" CssClass="configCombo" Style="width: calc(100% - 36px)" EnableTheming="True" TabIndex="5"></asp:DropDownList>
                                <asp:ImageButton ID="btConfiguraRateio" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Add.png" Width="30px" Height="30px" Style="float: left" CausesValidation="False" />
                            </div>
                            <asp:RequiredFieldValidator ID="rfvRateio" runat="server" ControlToValidate="cboRateio" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblAtivoCritica" runat="server" CssClass="configlabel" Text="* Ativo da conta virtual"></asp:Label>
                            <table style="border: 1px solid #CCCCCC; width: 100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <div id="DivGrupo" runat="server" style="border-bottom: 1px solid #CCCCCC; width: 100%;">
                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 100%;">
                                                        <asp:TextBox ID="txtLocalizaAtivo" runat="server" Style="width: calc(100% - 20px)" ForeColor="Black" Height="22px" BackColor="Transparent"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btLocalizaAtivo" runat="server" ImageUrl="~/Img_Sistema/Botao/bt_Combo.png" Style="border-style: none; font-size: 10pt; font-family: Arial; background-color: transparent; z-index: 108;" ToolTip="Pesquisar" Width="20px" Height="20px" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:ListBox ID="lstListaAtivo" runat="server" CssClass="configlistboxpesquisa" Height="60px" Width="100%"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfvListaAtivo" runat="server" ControlToValidate="lstListaAtivo" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="height: 10px"></div>
                    <div class="row">
                        <div class="col-md-6">
                            <%--<asp:Label ID="lblRateiaDiferenca" runat="server"  CssClass="configlabel" Text="Não ratear a diferença para o valor pago"></asp:Label>--%>
                            <asp:CheckBox ID="chkRateiaAtivoPadrao" Style="float: left" runat="server" TabIndex="6" Text=" Não ratear a diferença para o valor pago" />
                        </div>
                        <div class="col-md-6">
                            <%--<asp:Label ID="lblRateioporNota" runat="server"  CssClass="configlabel" Text="Ratear com base na nota da op."></asp:Label>--%>
                            <asp:CheckBox ID="chkRateioNota" Style="float: left" runat="server" TabIndex="7" Text=" Ratear com base na nota da op." />
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

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 449px; position: absolute; top: 682px; height: 44px; width: 128px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceContrato" runat="server" TargetControlID="rfvContrato"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceRateio" runat="server" TargetControlID="rfvRateio"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceListaAtivo" runat="server" TargetControlID="rfvListaAtivo"></cc1:ValidatorCalloutExtender>
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
