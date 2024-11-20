<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Auditoria.aspx.vb" Inherits="IControlIT.Auditoria" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Abrir-->
    <div id="pnlAbir" runat="server" class="bgModal" visible="True">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Abrir Status de Contestação" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12">
                    <asp:ListBox ID="lstConglomerado" runat="server" CssClass="configlistboxAbrir" Width="100%" Style="max-height: 200px" Font-Overline="False"></asp:ListBox>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFechar" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btExecutar" class="btn btn-success" runat="server" Text="Executar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--lanca status -->
    <div id="pnlmsg" runat="server" class="bgModal" visible="False">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblContestar" runat="server" CssClass="configlabel" Text="Status" Style="float: none;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="*Descrição"></asp:Label>
                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="800" TextMode="MultiLine" Width="100%" Height="80px" TabIndex="4"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDataPrevista" runat="server" CssClass="configlabel" Text="* Data Prevista"></asp:Label>
                    <asp:TextBox ID="txtDataPrevista" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="5"></asp:TextBox>
                    <cc1:MaskedEditValidator ID="mevDataPrevista" runat="server" ControlExtender="meeDataPrevista" ControlToValidate="txtDataPrevista" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataPrevista" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                    <asp:RequiredFieldValidator ID="rfvDataPrevista" runat="server" ControlToValidate="txtDataPrevista" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblValorPrevisto" runat="server" CssClass="configlabel" Text="* Valor Previsto"></asp:Label>
                    <asp:TextBox ID="txtValorPrevisto" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="6"></asp:TextBox>
                    <cc1:MaskedEditValidator ID="mevValorPrevisto" runat="server" ControlExtender="meeValorPrevisto" ControlToValidate="txtValorPrevisto" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevValorPrevisto" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                    <asp:RequiredFieldValidator ID="rfvValorPrevisto" runat="server" ControlToValidate="txtValorPrevisto" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFechar_Status" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btSalvar" class="btn btn-success" runat="server" Text="Salvar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--lanca acompanhamento -->
    <div id="pnlAcompanhamento" runat="server" class="bgModal" visible="False">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblACTitulo" runat="server" CssClass="configlabel" Text="Acompanhamento" Style="float: none;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblACDescricao" runat="server" CssClass="configlabel" Text="* Descrição"></asp:Label>
                    <asp:TextBox ID="txtACDescricao" runat="server" CssClass="configtext" MaxLength="800" TextMode="MultiLine" Width="100%" Height="117px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvACDescricao" runat="server" ControlToValidate="txtACDescricao" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblACDataResposta" runat="server" CssClass="configlabel" Text="* Data Resposta"></asp:Label>
                    <asp:TextBox ID="txtACDataResposta" runat="server" CssClass="configtext" Width="100%" MaxLength="10"></asp:TextBox>
                    <cc1:MaskedEditValidator ID="mkeACDataResposta" runat="server" ControlExtender="meeACDataResposta" ControlToValidate="txtACDataResposta" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevACDataResposta" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                    <asp:RequiredFieldValidator ID="rfvACDataResposta" runat="server" ControlToValidate="txtACDataResposta" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btACFechar" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btACSalvar" class="btn btn-success" runat="server" Text="Salvar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--Tela ********************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <asp:Button ID="btnConta" runat="server" class="btn-tab pull-left" Text="Conta" CausesValidation="False" OnClick="btnConta_Click" />
                    <asp:Button ID="btnAcompanhamento" runat="server" class="btn-tab-disable pull-left" Text="Acompanhamento" CausesValidation="False" OnClick="btnAcompanhamento_Click" />
                    <asp:Button ID="btnStatus" runat="server" class="btn-tab-disable pull-left" Text="Status" CausesValidation="False" OnClick="btnStatus_Click" />
                </div>
            </nav>
            <div class="card-body" style="background-color: #FFFFFF; border-radius: 6px; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px">

                <!--Conta-->
                <div id="divConta" runat="server" class="row" visible="true">
                    <div class="col-md-12">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="height: 15px">
                                                <asp:Label ID="lblLote" runat="server" Font-Bold="False" Visible="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Auditoria por Mês" Style="float: none"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="width: 100%; overflow: auto">
                                                    <asp:DataGrid ID="dtgLote" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                                        Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" PageSize="5">

                                                        <Columns>
                                                            <asp:BoundColumn DataField="Id_Auditoria_Lote" HeaderText="Id_Auditoria_Lote" Visible="False">
                                                                <HeaderStyle Width="16px" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                            </asp:BoundColumn>

                                                            <asp:BoundColumn DataField="Id_Auditoria_Status" HeaderText="Id_Auditoria_Status" Visible="False"></asp:BoundColumn>

                                                            <asp:BoundColumn DataField="Nm_Auditoria_Status" HeaderText="Nm_Auditoria_Status" Visible="False"></asp:BoundColumn>

                                                            <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btAcompanhamento" runat="server" Style="float: none;" Height="30px" ImageUrl="<%# Bind('Acompanhamento') %>" OnClick="btAcompanhamento_Click" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="30px" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                            </asp:TemplateColumn>

                                                            <asp:BoundColumn DataField="Descricao" HeaderText="Descrição" Visible="True"></asp:BoundColumn>

                                                            <asp:BoundColumn DataField="Dt_Lote" HeaderText="Lote" Visible="True"></asp:BoundColumn>

                                                            <asp:BoundColumn DataField="Valor" HeaderText="Valor" Visible="True" DataFormatString="{0:R$##########,###########0}"></asp:BoundColumn>

                                                        </Columns>

                                                        <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                                        <PagerStyle Mode="NumericPages" />
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--Acompanhamento-->
                <div id="divAcompanhamento" runat="server" class="row" visible="false">
                    <div class="col-md-12">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="height: 15px">
                                                <asp:Label ID="lblConta" runat="server" Font-Bold="False" Visible="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Acompanhamento" Style="float: none"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="width: 100%; overflow: auto">
                                                    <asp:DataGrid ID="dtgAuditoriaTexto" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                                        Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" PageSize="5">

                                                        <Columns>

                                                            <asp:TemplateColumn>
                                                                <HeaderTemplate>
                                                                    <asp:ImageButton ID="btTexto" runat="server" Height="28px" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Add.png" OnClick="btTexto_Click" Style="float: none;" ToolTip="Acompanhamento" />
                                                                </HeaderTemplate>
                                                                <HeaderStyle Width="15px" />
                                                            </asp:TemplateColumn>

                                                            <asp:BoundColumn DataField="Data" HeaderText="Data" Visible="True"></asp:BoundColumn>

                                                            <asp:BoundColumn DataField="Dt_Resposta" HeaderText="Data para Resposta" Visible="True"></asp:BoundColumn>

                                                            <asp:TemplateColumn HeaderText="Descrição">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" ReadOnly="True" Style="float: none;" Text="<%# Bind('Descricao') %>" TextMode="MultiLine" Height="100px" Width="100%"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100%" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                            </asp:TemplateColumn>
                                                        </Columns>

                                                        <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                                        <PagerStyle Mode="NumericPages" />
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--Status-->
                <div id="divStatus" runat="server" class="row" visible="false">
                    <div class="col-md-12">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="height: 23px">
                                                <asp:Label ID="lblAcompanhamento" runat="server" Font-Bold="False" Visible="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Status da Contestação" Style="float: none"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="width: 100%; overflow: auto">
                                                    <asp:DataGrid ID="dtgAcompanhamento" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                                        Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" PageSize="5">

                                                        <Columns>

                                                            <asp:BoundColumn DataField="Id_Auditoria_Acompanhamento" HeaderText="Id_Auditoria_Acompanhamento" Visible="False">
                                                                <HeaderStyle Width="16px" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                            </asp:BoundColumn>

                                                            <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btDesativa" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Deletar.png" Height="32px" OnClick="btDesativa_Click" OnClientClick="return confirm('Desativa Registro?')" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="32px" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                            </asp:TemplateColumn>

                                                            <asp:BoundColumn DataField="Data_Prevista" HeaderText="Data" Visible="True"></asp:BoundColumn>

                                                            <asp:BoundColumn DataField="Nm_Auditoria_Status" HeaderText="Status" Visible="True"></asp:BoundColumn>

                                                            <asp:BoundColumn DataField="Valor_Previsto" HeaderText="Valor" Visible="True" DataFormatString="{0:R$##########,###########0}"></asp:BoundColumn>

                                                            <asp:TemplateColumn HeaderText="Descrição">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" Text="<%# Bind('Descricao') %>" TextMode="MultiLine" Width="100%" Height="100px" ReadOnly="True"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100%" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                            </asp:TemplateColumn>
                                                        </Columns>

                                                        <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                                        <PagerStyle Mode="NumericPages" />
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 113; left: 489px; position: absolute; top: 900px; height: 188px; width: 284px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceDataPrevista" runat="server" TargetControlID="rfvDataPrevista"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceValorPrevisto" runat="server" TargetControlID="rfvValorPrevisto"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceACDescricao" runat="server" TargetControlID="rfvACDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeACDataResposta" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtACDataResposta"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeDataPrevista" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataPrevista"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeValorPrevisto" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" ErrorTooltipEnabled="True" Mask="999999.99" MaskType="Number" TargetControlID="txtValorPrevisto">
        </cc1:MaskedEditExtender>
        <asp:Label ID="lblMessage" runat="server" Style="left: 13px; top: 87px"></asp:Label>

        <asp:HiddenField ID="hfdId_Auditoria_Lote_Select" runat="server" />
        <asp:HiddenField ID="hfdId_Auditoria_Lote" runat="server" />
        <asp:HiddenField ID="hfdId_Auditoria_Conta" runat="server" />
        <asp:HiddenField ID="hfdId_Auditoria_Status" runat="server" />
        <asp:HiddenField ID="hfdDt_Lote" runat="server" />
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
        <asp:LinkButton ID="btPDF" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span>PDF</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAdicionar" runat="server" CssClass="btn-menu-toolbar" OnClick="btAdicionar_Click">
            <i class="fas fa-plus-square"></i>
            <br />
            <span>Add</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" OnClick="btExportar_Click">
            <i class="fas fa-file-upload"></i>
            <br />
            <span>Export</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar" OnClick="btAbrir_Click">
            <i class="fas fa-folder-open"></i>
            <br />
            <span>Dados</span>
        </asp:LinkButton>
    </div>

</asp:Content>




