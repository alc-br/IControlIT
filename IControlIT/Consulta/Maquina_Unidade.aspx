<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Maquina_Unidade.aspx.vb" Inherits="IControlIT.Consulta_Maquina_Unidade" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Src="../Filtro_Acesso.ascx" TagName="Filtro_Acesso" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Abrir-->
    <div id="pnlAbir" runat="server" class="bgModal" visible="True">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Maquinas" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label1" runat="server" CssClass="configlabel" Text="Fornecedor" Style="float: left" Font-Names="Segoe UI" Font-Size="14pt"></asp:Label>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12">
                    <asp:ListBox ID="cboConglomerado" runat="server" CssClass="configlistboxAbrir" Height="76px" Width="100%"></asp:ListBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFechar" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btExecutar" class="btn btn-success" runat="server" Text="Abrir" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!-- Tela ****************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblDescricaoArquivo" runat="server" CssClass="configlabel" Style="left: 9px; top: 13px; float: left;" ForeColor="Black"></asp:Label>
                            <div id="divGrid" runat="server" style="overflow: auto; width: 100%; height: 470px;" title=" ">
                                <asp:DataGrid ID="dtgDatagrid" runat="server" AllowPaging="True" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" PageSize="17" Style="font-size: 9pt; font-family: Arial; float: none;"
                                    Width="100%" BorderWidth="1px" BorderColor="Silver" Font-Names="CordiaUPC" Font-Size="17pt" ForeColor="Black">

                                    <Columns>
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
        <asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAbrir_Click">
            <i class="fas fa-folder-open"></i>
            <br />
            <span id="lblDados" runat="server">Dados</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btExportar_Click">
            <i class="fas fa-download"></i>
            <br />
            <span id="lblExportar" runat="server">Exportar</span>
        </asp:LinkButton>
    </div>

</asp:Content>

