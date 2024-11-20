<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Politica_Consumidor.aspx.vb" Inherits="IControlIT.Politica_Consumidor" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--ativo-->
    <div id="pnlMsg" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescricaoAtivoCota" runat="server" CssClass="configlabel" Text="Ativos sem Cota" Style="float: none; left: 5px; position: relative; top: 0px;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12">
                    <div id="Div1" runat="server" style="border: 1px solid #CCCCCC; width: 100%; height: 187px; overflow: auto" title=" ">
                        <asp:DataGrid ID="dtgLista" runat="server" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" Width="535px" BorderWidth="1px" GridLines="Horizontal">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />

                            <Columns>
                                <asp:ButtonColumn CommandName="Select" DataTextField="Lista" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Botao/Grid/{0}&quot; border=&quot;0&quot; height=&quot;18px&quot;&gt;" Text="Ativa/Desativa">
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <HeaderStyle Width="17px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="Descricao" HeaderText="Descrição"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Qtd" HeaderText="Quantidade"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Id_Ativo_Tipo" HeaderText="Id_Ativo_Tipo"
                                    Visible="False"></asp:BoundColumn>
                            </Columns>

                            <AlternatingItemStyle BackColor="#E6E6E6" />
                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#EEEEEE" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12">
                    <div id="Div2" runat="server" style="border: 1px solid #CCCCCC; width: 100%; height: 187px; overflow: auto" title=" ">
                        <asp:DataGrid ID="dtgDetalhe" runat="server" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" Width="535px" BorderWidth="1px" GridLines="Horizontal">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />

                            <Columns>
                                <asp:ButtonColumn CommandName="Select" DataTextField="Lista" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Botao/Grid/{0}&quot; border=&quot;0&quot; height=&quot;18px&quot;&gt;" Text="Ativa/Desativa">
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <HeaderStyle Width="17px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="Ativo" HeaderText="Ativo"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Consumidor" HeaderText="Usuário"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Id_Consumidor" HeaderText="Id_Consumidor" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Id_Ativo_Tipo" HeaderText="Id_Ativo_Tipo" Visible="False"></asp:BoundColumn>
                            </Columns>

                            <AlternatingItemStyle BackColor="#E6E6E6" />
                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#EEEEEE" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFecharCota" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
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
                        <div class="col-md-10">
                            <asp:Label ID="lblTipoFatura" runat="server" CssClass="configlabel" Text="* Login ou Nome do Usuário"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" Width="100%"></asp:TextBox>
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
                            <div style="width: 100%; overflow: auto">
                                <asp:DataGrid ID="dtgPolitica" runat="server" AllowPaging="True" AutoGenerateColumns="False" EnableTheming="True" HorizontalAlign="Left"
                                    Style="font-size: 8pt; color: Black; font-family: Arial" Width="100%" CellPadding="5" ForeColor="Black" PageSize="17" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" GridLines="None">

                                    <Columns>
                                        <asp:BoundColumn DataField="Nm_Consumidor" HeaderText="Usuário">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Liberar pra Marcação" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkMarcar" runat="server" Checked="<%# Bind('Marca_Ligacao') %>" Font-Names="Arial" Font-Size="9pt" />
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Valor da Politica" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="50px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValor" runat="server" CssClass="configtext" Text='<%# Bind("Valor_Politica") %>'></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="meeValorPolitica" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" ErrorTooltipEnabled="True" Mask="99999999.99" MaskType="Number" TargetControlID="txtValor"></cc1:MaskedEditExtender>
                                                <cc1:MaskedEditValidator ID="mevValor" runat="server" ControlExtender="meeValorPolitica" ControlToValidate="txtValor" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="*" InvalidValueBlurredMessage="*" ValidationGroup="MKE"> </cc1:MaskedEditValidator>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tipo do Ativo" ItemStyle-Height="50px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cboTipo_Ativo" runat="server" CssClass="configCombo" Enabled="<%# Bind('Enabled_Tipo') %>">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Id_Consumidor" HeaderText="Id_Consumidor" Visible="False">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Id_Ativo_Tipo" HeaderText="Id_Ativo_Tipo" Visible="False">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn ItemStyle-Height="30px" ItemStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btInsere" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Add.png" OnClick="btInsere_Click" Height="28px" />
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-Height="30px" ItemStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btDesativa" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Deletar.png" OnClientClick="return confirm('Desativa Registro?');" OnClick="btDesativa_Click" Height="28px" />
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateColumn>
                                    </Columns>

                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Left" Wrap="true" />
                                    <PagerStyle Mode="NumericPages" />
                                    <AlternatingItemStyle BackColor="#E0E0E0" />

                                </asp:DataGrid>
                            </div>
                            <asp:HiddenField ID="hdfAtivo_Tipo" runat="server" />
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
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" OnClick="btSalvar_Click">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" OnClick="btExportar_Click">
            <i class="fas fa-file-upload"></i>
            <br />
            <span>Export</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAlerta" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAlerta_Click">
            <i class="fas fa-exclamation-triangle"></i>
            <br />
            <span>Alerta</span>
        </asp:LinkButton>
    </div>

</asp:Content>
