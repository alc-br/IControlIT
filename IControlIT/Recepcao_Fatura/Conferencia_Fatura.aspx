<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Conferencia_Fatura.aspx.vb" Inherits="IControlIT.Conferencia_Fatura" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Abrir Recepção de Fatura" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <%--<asp:ListBox ID="lstFaturaParametro" runat="server" CssClass="configlistboxAbrir" Height="91px" Width="100%"></asp:ListBox>--%>
                            <asp:DropDownList ID="cboFaturaParametro" runat="server" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvFaturaParametro" runat="server" ControlToValidate="cboFaturaParametro" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceFaturaParametro" runat="server" TargetControlID="rfvFaturaParametro"></cc1:ValidatorCalloutExtender>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="Label2" runat="server" CssClass="configlabel" Text="Fornecedor" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <%--<asp:ListBox ID="lstConglomerado" runat="server" CssClass="configlistboxAbrir" Height="91px" Width="100%"></asp:ListBox>--%>
                            <asp:DropDownList ID="cboConglomerado" runat="server" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvConglomerado" runat="server" ControlToValidate="cboConglomerado" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceConglomerado" runat="server" TargetControlID="rfvConglomerado"></cc1:ValidatorCalloutExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" CssClass="configlabel" Text="Data:" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <asp:DropDownList ID="cboDataLote" runat="server" TabIndex="3" Width="100%" CssClass="configCombo"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDataLote" runat="server" ControlToValidate="cboDataLote" Display="None" SetFocusOnError="True" Style="float: left;"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceDataLote" runat="server" TargetControlID="rfvDataLote"></cc1:ValidatorCalloutExtender>
                        </div>
                        <div class="col-md-2">
                            <div style="width: 100%; height: 100%; position: relative;">
                                <asp:Button ID="btExecutar" class="btn btn-success" runat="server" Style="position: absolute; right: 0; bottom: -5px" Text="Executar" CausesValidation="False" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">&nbsp</div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblDescricaoArquivo" runat="server" CssClass="configlabel" Style="left: 9px; top: 13px; float: left;" ForeColor="#C1C1C1"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="DivAtivo" runat="server" style="overflow: auto; width: 100%; max-height: 470px" title="" visible="false">
                                <asp:DataGrid ID="dtgFatura" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                    EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                    HorizontalAlign="Left" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                                    <Columns>
                                        <asp:BoundColumn DataField="Fatura" HeaderText="Fatura">
                                            <HeaderStyle Width="425px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:BoundColumn>

                                        <asp:TemplateColumn HeaderText="Valor da Fatura">
                                            <ItemTemplate>
                                                <cc1:MaskedEditExtender ID="meeValorFatura" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" ErrorTooltipEnabled="True" Mask="99999999.99" MaskType="Number" TargetControlID="txtValor">
                                                </cc1:MaskedEditExtender>
                                                <asp:TextBox ID="txtValor" runat="server" CssClass="configtext" Style="float: right" MaxLength="11" Text='<%# Bind("Valor") %>' Width="107px" AutoPostBack="True"></asp:TextBox>
                                                <cc1:MaskedEditValidator ID="mevValor" runat="server" ControlExtender="meeValorFatura" ControlToValidate="txtValor" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="*" InvalidValueBlurredMessage="*" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BackColor="White" />
                                        </asp:TemplateColumn>

                                        <asp:BoundColumn DataField="Valor" HeaderText="Valor de Bilhete" DataFormatString="{0:R$##########.#0}">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                    </Columns>

                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <PagerStyle Mode="NumericPages" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <AlternatingItemStyle BackColor="#E0E0E0" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />

                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                    <div style="height: 5px"></div>
                    <div class="row">
                        <div class="col-md-12 text-right">
                            <asp:TextBox ID="txtTotalFatura" runat="server" CssClass="configtext" Style="float: right; text-align: right;" Width="115px" Visible="False" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="lblTotalFatura" runat="server" CssClass="configlabel" Style="float: right;" Text="Total das Faturas: " Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 108; left: 137px; position: absolute; top: 495px; height: 49px; width: 128px;">
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
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" OnClick="btSalvar_Click">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btConfiguracao" CssClass="btn-menu-toolbar" CausesValidation="false" runat="server">
            <i class="fas fa-cog"></i>
            <br />
            <span>Config</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" OnClick="btExportar_Click">
            <i class="fas fa-file-upload"></i>
            <br />
            <span>Export</span>
        </asp:LinkButton>
        <asp:LinkButton ID="bt_Recalcular" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="bt_Recalcular_Click">
            <i class="fas fa-calculator"></i>
            <br />
            <span>Recalc</span>
        </asp:LinkButton>
    </div>

</asp:Content>

