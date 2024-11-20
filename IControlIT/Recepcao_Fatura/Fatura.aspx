<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Fatura.aspx.vb" Inherits="IControlIT.Fatura" %>

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
                        <div class="col-md-6">
                            <asp:Label ID="lblNumeroFatura" runat="server" CssClass="configlabel" Text="Número da Fatura"></asp:Label>
                            <asp:TextBox ID="txtNumeroFatura" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNumeroFatura" runat="server" ControlToValidate="txtNumeroFatura" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblFaturaParametro" runat="server" CssClass="configlabel" Text="Tipo da Fatura"></asp:Label>
                            <asp:DropDownList ID="cboFaturaParametro" runat="server" AutoPostBack="True" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="2"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvFaturaParametro" runat="server" ControlToValidate="cboFaturaParametro" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="Descrição"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="3" placeholder="Exemplo: Nome Empresa / Mês Vencimento/ Número da Fatura"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDataLote" runat="server" CssClass="configlabel" Text="Mês Vencimento"></asp:Label>
                            <asp:DropDownList ID="cboDataLote" runat="server" Width="100%" TabIndex="4" CssClass="configCombo"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDataEmissao" runat="server" CssClass="configlabel" Text="Data de Emissão"></asp:Label>
                            <asp:TextBox ID="txtEmissao" runat="server" CssClass="configtext" MaxLength="10" Width="100%" TabIndex="5"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevEmissao" runat="server" ControlExtender="meeEmissao" ControlToValidate="txtEmissao" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevEmissao" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDataVencimento" runat="server" CssClass="configlabel" Text="Data de Vencimento"></asp:Label>
                            <asp:TextBox ID="txtVencimento" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="6"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevVencimento" runat="server" ControlExtender="meeVencimento" ControlToValidate="txtVencimento" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevVencimento" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblValorFatura" runat="server" CssClass="configlabel" Text="Valor da Fatura"></asp:Label>
                            <asp:TextBox ID="txtValorFatura" runat="server" CssClass="configtext" Width="100%" MaxLength="13" TabIndex="7"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvValorFatura" runat="server" ControlToValidate="txtValorFatura" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevValorFatura" runat="server" ControlExtender="meeValorFatura" ControlToValidate="txtValorFatura" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevValorFatura" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"> </cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco"></asp:Label>
                            <asp:TextBox ID="txtIdentificacao" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblNotaFiscal" runat="server" CssClass="configlabel" Text="Nota Fiscal"></asp:Label>
                            <asp:TextBox ID="txtNotaFiscal" runat="server" CssClass="configtext"  Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblPedido" runat="server" CssClass="configlabel" Text="Pedido"></asp:Label>
                            <asp:TextBox ID="txtPedido" runat="server" CssClass="configtext" Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblRef" runat="server" CssClass="configlabel" Text="Ref."></asp:Label>
                            <asp:TextBox ID="txtRef" runat="server" CssClass="configtext" Width="100%" ></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblReq" runat="server" CssClass="configlabel" Text="Req."></asp:Label>
                            <asp:TextBox ID="txtReq" runat="server" CssClass="configtext"  Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label1" runat="server" CssClass="configlabel" Text="Observação"></asp:Label>
                            <asp:TextBox ID="txtObs" runat="server" CssClass="configtext" Width="100%" MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <div id="divProvisaoCkb" class="configchekbox checkboxFatura">
                                <asp:CheckBox ID="CkbProvisao" runat="server" />
                                <asp:Label AssociatedControlID="CkbProvisao" runat="server">Provisão</asp:Label>
                             </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="DivAtivo" runat="server" style="overflow: auto; width: 100%; height: 125px;">
                                <asp:DataGrid ID="dtgDadoFatura" runat="server" AutoGenerateColumns="False" CellPadding="0" EnableTheming="True" HorizontalAlign="Left" PageSize="1" ShowHeader="False" Width="100%" GridLines="None">
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblBindComplemento" runat="server" Text="<%# Bind('Nm_Fatura_Parametro_Campo') %>" Width="100%" Style="float: left; text-align: left" CssClass="configlabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtBindComplemento" Text="<%# Bind('Descricao') %>" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Width="100%" CssClass="configtext"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 33px; position: absolute; top: 675px; height: 54px; width: 157px;">
        <cc1:ValidatorCalloutExtender ID="vceFaturaParametro" runat="server" TargetControlID="rfvFaturaParametro"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceNumeroFatura" runat="server" TargetControlID="rfvNumeroFatura"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceValorFatura" runat="server" TargetControlID="rfvValorFatura"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeEmissao" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEmissao"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeVencimento" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtVencimento"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeValorFatura" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" ErrorTooltipEnabled="True" Mask="99999999.99" MaskType="Number" TargetControlID="txtValorFatura"></cc1:MaskedEditExtender>
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
        <asp:LinkButton ID="btLimpar" runat="server" CssClass="btn-menu-toolbar" OnClick="btLimpar_Click">
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

    <script type="text/javascript">
        async function openPdfView() {
            window.open('../PDF/Lista_PDF.aspx?pRegistro=<%=vClientClick %>&pTabela=Fatura', '', 'resizable=yes, menubar=yes, scrollbars=no,height=768px, width=1024px, top=10, left=10')
        }

        var btnPdf = document.getElementById('ContentPlaceHolder2_btPDF');
        btnPdf.addEventListener("click", function (e) {
            openPdfView()
            e.preventDefault();
        });
    </script>
</asp:Content>