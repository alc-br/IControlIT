<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Ativo_Parametro.aspx.vb" Inherits="IControlIT.Ativo_Parametro" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Centro de Custo-->
    <div id="pnlCentro_Custo" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblPorcentualCDC" runat="server" CssClass="configlabel" Text="Percentual por Centro Custo" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12">
                    <div id="DivAtivo" runat="server" style="overflow: auto; width: 100%; max-height: 250px" title=" ">
                        <asp:DataGrid ID="dtgPorcentagem" runat="server" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderWidth="1px" GridLines="Horizontal">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#000000" />

                            <Columns>
                                <asp:TemplateColumn HeaderText="Centro de Custo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCdCentroCusto" runat="server" Text="<%# Bind('Cd_Centro_Custo') %>" Width="100%" ForeColor="Black" CssClass="configlabel"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="% Rateio">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPorcentagem" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="19" Text='<%# Bind("Porcentagem") %>' Width="35px" ForeColor="Black" CssClass="configtext"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="Id_Centro_Custo" HeaderText="Id_Centro_Custo" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>
                            </Columns>

                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#EEEEEE" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFechar" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btMostar" class="btn btn-success" runat="server" Text="Salvar" CausesValidation="False" />
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
                            <asp:Label ID="lblDataTerminoGarantia" runat="server" CssClass="configlabel" Text="Data do termino da Garantia"></asp:Label>
                            <asp:TextBox ID="txtDataTerminoGarantia" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="1"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevDataTerminoGarantia" runat="server" ControlExtender="meeDataTerminoGarantia" ControlToValidate="txtDataTerminoGarantia" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataTerminoGarantia" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblFormaAquisicao" runat="server" CssClass="configlabel" Text="* Forma"></asp:Label>
                            <asp:DropDownList ID="cboFormaAquisicao" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="2"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvFormaAquisicao" runat="server" ControlToValidate="cboFormaAquisicao" Display="None" SetFocusOnError="True" Style="float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDataInicioFormaAquisicao" runat="server" CssClass="configlabel" Text="Data de inicio"></asp:Label>
                            <asp:TextBox ID="txtDataInicioFormaAquisicao" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="3"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevDataInicioFormaAquisicao" runat="server" ControlExtender="meeDataInicioFormaAquisicao" ControlToValidate="txtDataInicioFormaAquisicao" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataInicioFormaAquisicao" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblValorFormaAquisicao" runat="server" CssClass="configlabel" Text="Valor"></asp:Label>
                            <asp:TextBox ID="txtValorFormaAquisicao" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="4"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevValorFormaAquisicao" runat="server" ControlExtender="meeValorFormaAquisicao" ControlToValidate="txtValorFormaAquisicao" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevValorFormaAquisicao" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblMesFormaAquisicao" runat="server" CssClass="configlabel" Text="Quantidade de meses residuais"></asp:Label>
                            <asp:TextBox ID="txtMesFormaAquisicao" runat="server" CssClass="configtext" Width="100%" MaxLength="2" TabIndex="5"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevMesFormaAquisicao" runat="server" ControlExtender="meeMesFormaAquisicao" ControlToValidate="txtMesFormaAquisicao" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevMesFormaAquisicao" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"> </cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco"></asp:Label>
                            <asp:TextBox ID="txtIdentificacao" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                    </div>
                    <div style="height: 10px"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblDadosRateio" runat="server" Text="Configuração do Rateio" ForeColor="#999999" Font-Names="Calibri Light" Font-Size="20pt"></asp:Label>
                        </div>
                    </div>
                    <div style="height: 10px"></div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:CheckBox ID="chkRateioConglomerado" AutoPostBack="true" runat="server" Text="Mesmo fornecedor do ativo" />
                        </div>
                        <div class="col-md-6">
                            <asp:CheckBox ID="chkCentroCusto" runat="server" AutoPostBack="True" Style="float: right" Font-Names="Arial" Font-Size="8pt" Text="Subescrevendo o CDC do usuário" ForeColor="Black" />
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
                                                        <asp:TextBox ID="txtLocalizaCentroCusto" runat="server" CssClass="configtext" Style="width: 100%; border: none" BackColor="Transparent"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 30px; background-color: #09A8C5; border-radius: 0px 4px 4px 0px">
                                                        <asp:LinkButton ID="btLocalizaCentroCusto" runat="server" CssClass="nav-link" OnClick="btLocalizaCentroCusto_Click">
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
                                                        <asp:ListBox ID="lstCentroCusto" runat="server" CssClass="configlistboxAbrir border-0" ForeColor="Black" Height="90px" Width="100%"></asp:ListBox>
                                                    </td>
                                                    <td style="width: 45px; background-color: #09A8C5; text-align: center; border-radius: 0px 4px 4px 0px">
                                                        <asp:LinkButton ID="btMoveSelecionado" runat="server" CssClass="nav-link" Style="position: relative; width: 100%; height: 90px;" OnClick="btMoveSelecionado_Click">
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
                                                        <asp:ListBox ID="lstListaCentroCusto" runat="server" CssClass="configlistboxAbrir border-0" ForeColor="Black" Height="90px" Width="100%"></asp:ListBox>
                                                    </td>
                                                    <td style="width: 45px; background-color: #09A8C5; text-align: center; border-radius: 0px 4px 4px 0px">
                                                        <asp:LinkButton ID="btMoveSelecao" runat="server" CssClass="nav-link" Style="position: relative; width: 100%; height: 90px;" OnClick="btMoveSelecao_Click">
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
                        <div class="col-md-6">
                            <asp:Label ID="lblRateioTroncoGrupo" runat="server" CssClass="configlabel" Text="Para grupo de rota"></asp:Label>
                            <asp:DropDownList ID="cboRateioTroncoGrupo" runat="server" AutoPostBack="True" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="6"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblRateioOutroConglomerado" runat="server" CssClass="configlabel" Text="Para outro fornecedor"></asp:Label>
                            <asp:DropDownList ID="cboRateioOutroConglomerado" runat="server" AutoPostBack="True" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="7"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblContrato" runat="server" CssClass="configlabel" Text="Contrato"></asp:Label>
                            <asp:DropDownList ID="cboContrato" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="8"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btContrato" runat="server" BackColor="Transparent" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="White" Height="25px" Text="Contrato" Style="cursor: pointer" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblConta" runat="server" CssClass="configlabel"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 208px; position: absolute; top: 647px; height: 48px; width: 187px;">
        <cc1:ValidatorCalloutExtender ID="vceFormaAquisicao" runat="server" TargetControlID="rfvFormaAquisicao"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeDataTerminoGarantia" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataTerminoGarantia" AcceptAMPM="True"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeDataInicioFormaAquisicao" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataInicioFormaAquisicao"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeValorFormaAquisicao" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" ErrorTooltipEnabled="True" Mask="99999.99" MaskType="Number" TargetControlID="txtValorFormaAquisicao"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeMesFormaAquisicao" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" Mask="99" MaskType="Number" TargetControlID="txtMesFormaAquisicao"></cc1:MaskedEditExtender>
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
        <asp:LinkButton ID="btConfig" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btConfig_Click">
            <i class="fas fa-cog"></i>
            <br />
            <span id="Span1" runat="server">Config</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btPDF" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span id="lblPdf" runat="server">PDF</span>
        </asp:LinkButton>
    </div>

</asp:Content>

