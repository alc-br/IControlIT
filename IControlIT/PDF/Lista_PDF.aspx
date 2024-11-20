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
                                    <table style="width: 100%">
                                        <tr>
                                            <td></td>

                                            <td style="width: 164px">
                                                <asp:Button ID="btInserir" class="btn btn-success" runat="server" Text="Anexar novo arquivo" CausesValidation="False" />
                                            </td>
                                        </tr>
                                    </table>
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
