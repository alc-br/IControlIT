<%@ Control Language="VB" AutoEventWireup="false" CodeBehind="Localizar.ascx.vb" Inherits="IControlIT.Localizar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div class="row">
    <div class="col-md-12">
        <div id="Div1" runat="server" style="width: 100%; overflow: auto;">
            <asp:DataGrid ID="dtgLocaliza" runat="server" AutoGenerateColumns="False" CellPadding="2" CellSpacing="2" EnableTheming="True" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Font-Size="9pt" Width="100%"
                ForeColor="Black" GridLines="None" ShowHeader="False" AllowPaging="true" PageSize="12">

                <PagerStyle Mode="NumericPages" Font-Names="Segoe UI Semibold" HorizontalAlign="Center" Font-Size="Large" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Names="Arial" Font-Size="9pt" />

                <Columns>
                    <asp:TemplateColumn HeaderText="Descrição">
                        <ItemTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 25px;">
                                        <asp:ImageButton ID="btExcluir" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_View.png" OnClick="btExcluir_Click" Height="25px" CausesValidation="False" />
                                    </td>
                                    <td style="width: 100%; text-align: Left;">
                                        <asp:Label ID="lblDescricao" runat="server" Text='<%# Bind("Descricao") %>' Font-Bold="False" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Width="100%" Style="line-height: 18px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:TemplateColumn>

                    <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </div>

        <asp:HiddenField ID="hdfPakage" runat="server" />
        <asp:HiddenField ID="hdfDescricao" runat="server" />
        <asp:HiddenField ID="hdfPagina" runat="server" />
    </div>
</div>

