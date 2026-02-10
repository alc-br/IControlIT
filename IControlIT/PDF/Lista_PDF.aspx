<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Lista_PDF.aspx.vb" Inherits="IControlIT.Lista_PDF" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>K2A - IControlIT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <script src='../JScript.js' type="text/javascript"></script>
    <link href="../CSSConfigObj.css" rel="Stylesheet" />
    <link href="../CSSEstruturalMaster.css" rel="Stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="Stylesheet" />
    <link rel="Shortcut Icon" href="Img_Sistema/logo.ico" />
</head>

<body>
    <form id="form1" runat="server" defaultbutton="btInserir">
        <table style="width: 100%;">
            <tr>
                <td>
                    <div style="width: 560px; margin: 1% auto; padding: 0px;">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center; height: 40px">
                                    <asp:Label ID="lblImportaPDF" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="X-Large" ForeColor="#333333" Text="Arquivos Importados" Style="float: none"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 180px">
                                    <div id="DivAtivo_Complemento" runat="server" style="border: 1px solid #CCCCCC; width: 554px; height: 187px; overflow: auto" title=" ">
                                        <asp:DataGrid ID="dtgListaPDF" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="0" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                            Font-Size="9pt" Width="550px" ForeColor="Black" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GridLines="None">
                                            <Columns>
                                                <asp:TemplateColumn ItemStyle-Height="32px" ItemStyle-Width="32px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btLink" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_View.png" Height="26px" OnClick="btLink_Click"/>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="32px" />
                                                    <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn ItemStyle-Height="32px" ItemStyle-Width="32px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btExcluir" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Deletar.png" OnClick="btExcluir_Click" Height="26px" OnClientClick="return confirm('Você deseja desativa o registro?');" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="32px" />
                                                    <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:TemplateColumn>

                                                <asp:BoundColumn DataField="Nm_Arquivo_PDF" HeaderText="Descrição" Visible="True">
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Id_Arquivo_PDF" Visible="False">
                                                    <ItemStyle Font-Bold="False" Font-Italic="False"
                                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Tabela_Registro" Visible="False">
                                                    <ItemStyle Font-Bold="False" Font-Italic="False"
                                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Id_Registro_Tabela" Visible="False">
                                                    <ItemStyle Font-Bold="False" Font-Italic="False"
                                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
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
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp</td>
                            </tr>
                            <tr>
                                <td>
                                    <%-- [INÍCIO - ICTRL-NF-202509-001] --%>
                                    <!-- Painel de Confirmação para Marcar como Pago -->
                                    <div id="pnlConfirmacaoPago" runat="server" class="bgModal" visible="false">
                                        <div class="modalPopup" style="width: 400px;">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label runat="server" CssClass="configlabel" Text="Confirmar Pagamento" Font-Size="16pt" Style="float: left;"></asp:Label>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 15px;">
                                                    <asp:Label runat="server" CssClass="configlabel" Text="Por favor, insira uma observação:" Style="float: left;"></asp:Label>
                                                    <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvObservacao" runat="server" ControlToValidate="txtObservacao"
                                                        ErrorMessage="A observação é obrigatória." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 20px;">
                                                <div class="col-md-12 text-right">
                                                    <asp:Button ID="btnCancelarConfirmacao" class="btn btn-default" runat="server" Text="Cancelar" CausesValidation="False" OnClick="btnCancelarConfirmacao_Click" />
                                                    <asp:Button ID="btnConfirmarPago" class="btn btn-primary" runat="server" Text="Confirmar" OnClick="btnConfirmarPago_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <!-- Painel que controla a visibilidade do botão 'Marcar como pago' -->
                                                <asp:Panel ID="pnlMarcarPago" runat="server" Visible="false" Style="float: right; margin-right: 10px;">
                                                    <asp:Button ID="btnMarcarComoPago" class="btn btn-primary" runat="server" Text="Marcar como pago" CausesValidation="False" OnClick="btnMarcarComoPago_Click" />
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 164px">
                                                <asp:Button ID="btInserir" class="btn btn-success" runat="server" Text="Anexar novo arquivo" CausesValidation="False" />
                                            </td>
                                        </tr>
                                    </table>
                                    <%-- [FIM - ICTRL-NF-202509-001] --%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
