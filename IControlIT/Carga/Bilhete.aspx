<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Bilhete.aspx.vb" Inherits="IControlIT.Bilhete" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--filtro ********************************************************************************** -->
    <div id="pnlCarga" runat="server" visible="False">
        <div class="bgModal">
            <div class="modalPopup">
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblCLassificar" runat="server" CssClass="configlabel" Text="Mensagem" Style="float: none; left: 5px; position: relative; top: 0px;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblObservacao" runat="server" Style="float: left" CssClass="configlabel" Text="O Tipo do Ativo e Fornecedor estão corretos para carga?"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-right">
                        <asp:Button ID="btVoltar" class="btn btn-default" runat="server" Text="Voltar" CausesValidation="False" />
                        <asp:Button ID="BtContinuar" class="btn btn-success" runat="server" Text="Continuar" CausesValidation="False" />
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
                            <asp:Label ID="lblArquivo" runat="server" CssClass="configlabel" Text="* Tipo do Importação"></asp:Label>
                            <asp:DropDownList ID="cboArquivo" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="1"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="cboArquivo" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblAtivoTipo" runat="server" CssClass="configlabel" Text="* Tipo do Ativo"></asp:Label>
                            <asp:DropDownList ID="cboAtivoTipo" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="2"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAtivoTipo" runat="server" ControlToValidate="cboAtivoTipo" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblConglomerado" runat="server" CssClass="configlabel" Text="* Fornecedor"></asp:Label>
                            <asp:DropDownList ID="cboConglomerado" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="3"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvConglomerado" runat="server" ControlToValidate="cboConglomerado" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDataLiberacaoLote" runat="server" CssClass="configlabel" Text="* Data de Liberação  do Lote"></asp:Label>
                            <asp:TextBox ID="txtDataLiberacao" runat="server" CssClass="configtext" Width="100%" MaxLength="10" TabIndex="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDataLiberacao" runat="server" ControlToValidate="txtDataLiberacao" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <cc1:MaskedEditValidator ID="mevDataLiberacao" runat="server" ControlExtender="meeDataLiberacao" ControlToValidate="txtDataLiberacao" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataLiberacao" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblDataLote" runat="server" CssClass="configlabel" Text="* Mês Lote"></asp:Label>
                            <div class="input-group no-border">
                                <asp:TextBox ID="txtDataLote" runat="server" CssClass="configtext" Style="width: calc(100% - 30px)" MaxLength="10" TabIndex="5"></asp:TextBox>
                                <asp:ImageButton ID="btCarga" runat="server" ImageUrl="~/Img_Sistema/Geral/Localiza_Pesquisa_Home.png" Width="26px" Height="26px" Style="float: left" CausesValidation="False" />
                                <asp:RequiredFieldValidator ID="rfvDataLote" runat="server" ControlToValidate="txtDataLote" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <cc1:MaskedEditValidator ID="mevDataLote" runat="server" ControlExtender="meeDataLote" ControlToValidate="txtDataLote" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataLote" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="DivAtivo" runat="server" style="overflow: auto; width: 100%; height: 200px" title=" " visible="False">
                                <asp:DataGrid ID="dtgCargaCompletada" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="400px" BorderStyle="Solid" BorderWidth="1px">

                                    <Columns>
                                        <asp:BoundColumn DataField="Nm_Conglomerado" HeaderText="Fornecedor">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="QTD" HeaderText="Qtde">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Valor" HeaderText="Valor" DataFormatString="{0:R$##########0.#0}">
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
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />

                                </asp:DataGrid>
                            </div>
                            <asp:HiddenField ID="hdfMenssagem" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador2" runat="server" Style="z-index: 107; left: 170px; position: absolute; top: 576px; height: 45px; width: 176px;">
        <cc1:ValidatorCalloutExtender ID="vceDataLote" runat="server" TargetControlID="rfvDataLote"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeDataLote" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="9999/99" TargetControlID="txtDataLote"></cc1:MaskedEditExtender>
        <cc1:ValidatorCalloutExtender ID="vceDataLiberacao" runat="server" TargetControlID="rfvDataLiberacao"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeDataLiberacao" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataLiberacao"></cc1:MaskedEditExtender>
        <cc1:ValidatorCalloutExtender ID="vceArquivo" runat="server" TargetControlID="rfvArquivo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceAtivoTipo" runat="server" TargetControlID="rfvAtivoTipo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceConglomerado" runat="server" TargetControlID="rfvConglomerado"></cc1:ValidatorCalloutExtender>
        <asp:Label ID="lblMessage" runat="server" Style="left: 13px; top: 87px"> </asp:Label>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="tbBotao" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="tVoltarMenu" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="tVoltarMenu_Click">
            <i class="fas fa-arrow-left"></i>
            <br />
            <span>Voltar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btUpload" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btUpload_Click">
            <i class="fas fa-upload"></i>
            <br />
            <span>Upload</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExecutar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btExecutar_Click">
            <i class="fas fa-play-circle"></i>
            <br />
            <span>Executar</span>
        </asp:LinkButton>
    </div>

</asp:Content>
