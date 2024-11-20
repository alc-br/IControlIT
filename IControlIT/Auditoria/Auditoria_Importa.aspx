<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Auditoria_Importa.aspx.vb" Inherits="IControlIT.Auditoria_Importa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-5">
                            <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Importação de Contestação" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <asp:DropDownList ID="cboConglomerado" runat="server" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                        </div>
                        <div class="col-md-5">
                            <asp:Label ID="Label2" runat="server" CssClass="configlabel" Text="Data:" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <asp:DropDownList ID="cboDataLote" CssClass="configCombo" runat="server" Width="100%" TabIndex="2"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <div style="width: 100%; height: 100%; position: relative;">
                                <asp:Button ID="btExecutar" class="btn btn-success" runat="server" Style="position: absolute; right: 0; bottom: -5px" Text="Executar" CausesValidation="False" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblDescricaoArquivo" runat="server" CssClass="configlabel" Style="left: 9px; top: 13px; float: left;" ForeColor="Black"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div style="width: 100%; overflow: auto">
                                <asp:DataGrid ID="dtgAuditoria" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px" AllowPaging="True" PageSize="20">
                                    <Columns>
                                        <asp:BoundColumn DataField="Conta" HeaderText="Conta" Visible="True">
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="DB_Tipo_Parametro" HeaderText="Tipo" Visible="True">
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Valor" HeaderText="Valor" Visible="True" DataFormatString="{0:R$##########.#0}">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
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

                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="tbBotao" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="btVoltar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btVoltar_Click">
            <i class="fas fa-arrow-left"></i>
            <br />
            <span>Voltar</span>
        </asp:LinkButton>
    </div>

</asp:Content>


