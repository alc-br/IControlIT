<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Ativo_Localiza.aspx.vb" Inherits="IControlIT.Ativo_Localiza" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Abrir-->
    <div id="pnlMsg" runat="server" class="bgModal" visible="False">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Ativo sem Usu�rio" Style="float: none; left: 5px; position: relative; top: 0px;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div id="DivCustoFixo" runat="server" style="overflow: auto; width: 100%; height: 85px;" title=" ">
                        <asp:DataGrid ID="dtgLista" runat="server" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="535px" BorderWidth="1px" GridLines="Horizontal">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />

                            <Columns>
                                <asp:BoundColumn DataField="Descricao" HeaderText="Descri��o" Visible="True">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" ForeColor="Black" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Qtd" HeaderText="Qtde" Visible="True">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" ForeColor="Black" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>
                            </Columns>

                            <AlternatingItemStyle BackColor="#E6E6E6" />
                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#EEEEEE" />

                        </asp:DataGrid>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFechar" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
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
                        <div class="col-md-12">
                            <asp:Label ID="lblDescricaoVagos" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Lista" Style="float: none"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div style="width: 100%; overflow: auto">
                                <%-- Grid padrão (Inventario, Custo_Cancelada, Sem_lote, etc.) --%>
                                <asp:DataGrid ID="dtgLocaliza" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                    Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" PageSize="30">

                                    <Columns>
                                        <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Nm_Ativo_Tipo" HeaderText="Tipo"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Nm_Conglomerado" HeaderText="Fornecedor"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Demitido" HeaderText="Observa&#231;&#227;o"></asp:BoundColumn>
                                    </Columns>

                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <PagerStyle Mode="NumericPages" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <AlternatingItemStyle BackColor="#E6E6E6" />
                                </asp:DataGrid>

                                <%-- [INÍCIO - ICTRL-NF-202509-001] Grid específico para ContasNaoPagas --%>
                                <asp:DataGrid ID="dtgContasNaoPagas" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                    Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" PageSize="30" Visible="False" OnItemDataBound="dtgContasNaoPagas_ItemDataBound" OnPageIndexChanged="dtgContasNaoPagas_PageIndexChanged">

                                    <Columns>
                                        <asp:BoundColumn DataField="Nm_Operadora" HeaderText="Operadora">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nr_Conta" HeaderText="Nr. Conta">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Dt_Emissao" HeaderText="Dt. Emissão" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Dt_Vencimento" HeaderText="Dt. Vencimento" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Vr_Total" HeaderText="Valor Total" DataFormatString="{0:c}">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:TemplateColumn HeaderText="Observação">
                                            <ItemTemplate>
                                                <asp:Label ID="lblObservacao" runat="server"
                                                    Text='<%# IIf(Eval("Observacao") IsNot Nothing AndAlso Len(Eval("Observacao").ToString()) > 30, Left(Eval("Observacao").ToString(), 30) & "...", IIf(Eval("Observacao") IsNot Nothing, Eval("Observacao").ToString(), "")) %>'
                                                    ToolTip='<%# IIf(Eval("Observacao") IsNot Nothing, Eval("Observacao").ToString(), "") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:BoundColumn DataField="Fl_Pago" HeaderText="Status">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Id_Fatura" HeaderText="Id_Fatura" Visible="False">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:TemplateColumn HeaderText="Pag.">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btComprovante" runat="server"
                                                    ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Check.png"
                                                    Style="height: 25px"
                                                    CausesValidation="False"
                                                    OnClick="btComprovante_Click"
                                                    CommandArgument='<%# Eval("Id_Fatura") %>'
                                                    ToolTip="Comprovante de Pagamento" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Fatura">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btFatura" runat="server"
                                                    ImageUrl="~/Img_Sistema/Botao/Grid/Grid_View.png"
                                                    Style="height: 25px"
                                                    CausesValidation="False"
                                                    OnClick="btFatura_Click"
                                                    CommandArgument='<%# Eval("Id_Fatura") %>'
                                                    ToolTip="Visualizar Fatura" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>

                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <PagerStyle Mode="NumericPages" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <AlternatingItemStyle BackColor="#E6E6E6" />
                                </asp:DataGrid>
                                <%-- [FIM - ICTRL-NF-202509-001] --%>
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
        <asp:LinkButton ID="btAlerta" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAlerta_Click">
            <i class="fas fa-exclamation-triangle"></i>
            <br />
            <span>Alerta</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btExportar_Click">
            <i class="fas fa-download"></i>
            <br />
            <span>Exportar</span>
        </asp:LinkButton>
    </div>

</asp:Content>
