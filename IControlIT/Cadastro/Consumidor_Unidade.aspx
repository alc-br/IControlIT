<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Consumidor_Unidade.aspx.vb" Inherits="IControlIT.Consumidor_Unidade" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <asp:Button ID="btnFornecedor" runat="server" class="btn-tab pull-left" Text="Forncedor que Atende a Unidade" CausesValidation="False" OnClick="btnFornecedor_Click" />
                    <asp:Button ID="btnDadosUnidade" runat="server" class="btn-tab-disable pull-left" Text="Dados da Unidade" CausesValidation="False" OnClick="btnDadosUnidade_Click" />
                    <asp:Button ID="btnDadosFornecedor" runat="server" class="btn-tab-disable pull-left" Text="Dados do Fornecedor" CausesValidation="False" OnClick="btnDadosFornecedor_Click" />
                </div>
            </nav>
            <div class="card-body" style="background-color: #FFFFFF; border-radius: 6px; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px">

                <!-- Fornecedor -->
                <!--Relacionamento-->
                <div id="divFornecedor" runat="server" class="row" visible="true">
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

                <!-- Dados Unidade -->
                <div id="divDadosUnidade" runat="server" class="row" visible="false">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblNm_Unidade" runat="server" CssClass="configlabel" Text="* Unidade"></asp:Label>
                                <asp:TextBox ID="txtNmUnidade" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="100" Width="100%" CssClass="configtext" TabIndex="1"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblCNPJ" runat="server" CssClass="configlabel" Text="CNPJ"></asp:Label>
                                <asp:TextBox ID="txtCNPJ" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="20" Width="100%" CssClass="configtext" TabIndex="2"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblIE" runat="server" CssClass="configlabel" Text="IE"></asp:Label>
                                <asp:TextBox ID="txtIE" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="20" Width="100%" CssClass="configtext" TabIndex="3"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblDataAtivacao" runat="server" CssClass="configlabel" Text="Ativação"></asp:Label>
                                <asp:TextBox ID="txtDataAtivacao" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="10" Width="100%" CssClass="configtext" TabIndex="4"></asp:TextBox>
                                <cc1:MaskedEditValidator ID="mevDataAtivacao" runat="server" ControlExtender="meeDataAtivacao" ControlToValidate="txtDataAtivacao" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataAtivacao" InvalidValueBlurredMessage="*" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblObservacao" runat="server" CssClass="configlabel" Text="Observação"></asp:Label>
                                <asp:TextBox ID="txtObservacao" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="800" Width="100%" CssClass="configtext" TabIndex="5" Height="80px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblEntregaContato" runat="server" CssClass="configlabel" Text="Contato (Ent)"></asp:Label>
                                <asp:TextBox ID="txtEntregaContato" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="100" Width="100%" CssClass="configtext" TabIndex="6"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblEntregaEndereco" runat="server" CssClass="configlabel" Text="Endereço (Ent)"></asp:Label>
                                <asp:TextBox ID="txtEntregaEndereco" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="600" Width="100%" CssClass="configtext" TabIndex="7" Height="80px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblEntregaTelefone" runat="server" CssClass="configlabel" Text="Telefone (Ent)"></asp:Label>
                                <asp:TextBox ID="txtEntregaTelefone" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Width="100%" CssClass="configtext" TabIndex="8"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblFaturamentoContato" runat="server" CssClass="configlabel" Text="Contato (Fat)"></asp:Label>
                                <asp:TextBox ID="txtFaturamentoContato" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="100" Width="100%" CssClass="configtext" TabIndex="9"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblFaturamentoEndereco" runat="server" CssClass="configlabel" Text="Endereço (Fat)"></asp:Label>
                                <asp:TextBox ID="txtFaturamentoEndereco" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="600" Width="100%" CssClass="configtext" TabIndex="10" Height="80px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblFaturamentoCNPJ" runat="server" CssClass="configlabel" Text="CNPJ (Fat)"></asp:Label>
                                <asp:TextBox ID="txtFaturamentoCNPJ" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="20" Width="100%" CssClass="configtext" TabIndex="11"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblFaturamentoIE" runat="server" CssClass="configlabel" Text="IE (Fat)"></asp:Label>
                                <asp:TextBox ID="txtFaturamentoIE" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="20" Width="100%" CssClass="configtext" TabIndex="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblFaturamentoEmail" runat="server" CssClass="configlabel" Text="E-mail (Fat)"></asp:Label>
                                <asp:TextBox ID="txtFaturamentoEmail" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="100" Width="100%" CssClass="configtext" TabIndex="13"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblFaturamentoTelefone" runat="server" CssClass="configlabel" Text="Telefone (Fat)"></asp:Label>
                                <asp:TextBox ID="txtFaturamentoTelefone" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Width="100%" CssClass="configtext" TabIndex="14"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblMatricula" runat="server" CssClass="configlabel" Text="Matrícula"></asp:Label>
                                <asp:TextBox ID="txtMatricula" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="100" Width="100%" CssClass="configtext" TabIndex="15"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblUsuario" runat="server" CssClass="configlabel" Text="Usuário"></asp:Label>
                                <asp:TextBox ID="txtIdConsumidor" runat="server" CssClass="configtext" Width="320px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="txtNmConsumidor" runat="server" CssClass="configtext" MaxLength="50" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
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

                <!-- Dados Fornecedor -->
                <div id="divDadosFornecedor" runat="server" class="row" visible="false">
                    <div class="col-md-12">
                        <asp:Label ID="lblConglomerado" runat="server" CssClass="configlabel" Text="Informar caso esse usuário seja um forncedor"></asp:Label>
                        <asp:DropDownList ID="cboConglomerado" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%"></asp:DropDownList>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 677px; position: absolute; top: 489px; height: 63px; width: 132px;">
        <cc1:MaskedEditExtender ID="meeDataAtivacao" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataAtivacao"></cc1:MaskedEditExtender>
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
