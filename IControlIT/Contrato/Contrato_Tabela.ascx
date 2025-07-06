<%@ Control Language="VB" AutoEventWireup="false" CodeBehind="Contrato_Tabela.ascx.vb" Inherits="IControlIT.Contrato_Tabela" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div class="row">
    <div class="col-md-12">
        <div class="col-md-12 overflow-auto">
            <asp:DataGrid ID="dtgContrato_Tabela" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Font-Size="9pt" Width="100%"
                ForeColor="Black" AllowPaging="True" PageSize="12" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" AllowSorting="True">

                <Columns>
                    <asp:BoundColumn SortExpression="Id_Contrato" DataField="Id_Contrato" HeaderText="Id_Contrato" Visible="False">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:BoundColumn>

                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:ImageButton ID="btExcluir" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_View.png" OnClick="btExcluir_Click" Height="25px" CausesValidation="False" />
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:BoundColumn SortExpression="Nr_Contrato" DataField="Nr_Contrato" HeaderText="Número do Contrato" Visible="True">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:BoundColumn>

                    <asp:BoundColumn SortExpression="Nm_Empresa_Contratada" DataField="Nm_Empresa_Contratada" HeaderText="Contratada" Visible="True">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:BoundColumn>

                    <asp:BoundColumn SortExpression="Empresa" DataField="Empresa" HeaderText="Contratante" Visible="True">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:BoundColumn>

                    <asp:BoundColumn SortExpression="Nm_Filial" DataField="Nm_Filial" HeaderText="Filial" Visible="True">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:BoundColumn>

                    <asp:BoundColumn SortExpression="Nm_Contrato_Status" DataField="Nm_Contrato_Status" HeaderText="Status" Visible="True">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:BoundColumn>

                    <asp:BoundColumn SortExpression="Dt_Inicio_Vigencia" DataField="Dt_Inicio_Vigencia" HeaderText="Data Início" Visible="True">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>

                    <asp:BoundColumn SortExpression="Dt_Fim_Vigencia" DataField="Dt_Fim_Vigencia" HeaderText="Data Final" Visible="True">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>

                    <asp:BoundColumn SortExpression="Nm_Servico" DataField="Nm_Servico" HeaderText="Tipo" Visible="True">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:BoundColumn>

                    <asp:BoundColumn SortExpression="Objeto" DataField="Objeto" HeaderText="Objeto" Visible="True">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:BoundColumn>

                    <asp:BoundColumn SortExpression="Nm_Contrato_Indice" DataField="Nm_Contrato_Indice" HeaderText="Índice Reajuste" Visible="True">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:BoundColumn>

                </Columns>

                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Left" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <PagerStyle Mode="NumericPages" />
                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <AlternatingItemStyle BackColor="#E6E6E6" />
                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />

            </asp:DataGrid>
        </div>

        <asp:HiddenField ID="hdfPakage" runat="server" />
        <asp:HiddenField ID="hdfDescricao" runat="server" />
        <asp:HiddenField ID="hdfPagina" runat="server" />
    </div>
</div>

