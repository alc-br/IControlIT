<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="CockPit_Detalhe_Impressao.aspx.vb" Inherits="IControlIT.CockPit_Detalhe_Impressao" EnableEventValidation="true" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>K2A - IControlIT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <script src='../JScript.js' type="text/javascript"></script>
    <link href="../CSSConfigObj.css" rel="Stylesheet" />
    <link href="../CSSEstruturalMaster.css" rel="Stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="Stylesheet" />
    <link rel="Shortcut Icon" href="Img_Sistema/logo.ico" />

    <style type="text/css">
        .pagina #login {
            margin: 1% auto;
            padding: 0px;
        }

        .configlabel {
            font-size: 9pt;
            color: black;
            font-family: Arial;
            background-color: transparent;
        }

        .configlabelcabGraf {
            font-size: 9pt;
            font-family: Arial;
            background-color: transparent;
            left: 20px;
            position: relative;
            float: left;
            height: 15px;
            top: 3px;
        }

        .configtextPesquisa {
            font-size: 8pt;
            color: White;
            font-family: Arial;
            border-width: 1px;
            border-style: solid;
            border-color: transparent;
            height: 20px;
            background-color: transparent
        }
    </style>
</head>
<body class="pagina" runat="server" bgcolor="#EEEEEE">
    <form id="form1" runat="server">
        <!--detalhamento -->
        <asp:Panel ID="pnlDetalhe" runat="server" Style="top: 0px; left: 0px; position: fixed; height: 100%; width: 100%; z-index: 120; background-color: rgba(0,0,0,0.9);" Visible="false">
            <table style="border: 1px ridge #CCCCCC; width: 400px; height: 277px; background-color: #FFFFFF; position: fixed; top: 96px; left: 284px; border-radius: 6px 6px;" align="center">
                <tr>
                    <td>
                        <table style="width: 100%; height: 30px">
                            <tr>
                                <td>
                                    <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Detalhamento" Style="float: none; left: 5px; position: relative; top: 0px;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <div id="DivTopGastoMes" runat="server" style="border: 1px solid #CCCCCC; width: 610px; height: 187px; overflow: auto" title=" ">
                                                    <asp:DataGrid ID="dtgTopGastoMes" runat="server" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="580px" BorderWidth="1px" GridLines="Horizontal">
                                                        <PagerStyle Mode="NumericPages" />
                                                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />

                                                        <Columns>
                                                            <asp:ButtonColumn DataTextField="Descricao" CommandName="Select" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Indicadores/add_Black.png&quot; border=&quot;0&quot; height=&quot;20px&quot;width=&quot;20px&quot;&gt;">
                                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                <HeaderStyle Width="18px" />
                                                                <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                            </asp:ButtonColumn>
                                                            <asp:BoundColumn DataField="Descricao" HeaderText="Usuário">
                                                                <HeaderStyle Width="200px" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Mes" HeaderText="R$|Mês" DataFormatString="{0:R$##########0.#0}">
                                                                <HeaderStyle HorizontalAlign="Right" Width="150px" />
                                                                <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                                <HeaderStyle Width="200px" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                            </asp:BoundColumn>
                                                        </Columns>

                                                        <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#FFFFFF" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#333333" />
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td></td>

                                <td style="width: 84px">
                                    <asp:Button ID="btFecharAbrir" class="btn btn-default" Width="80px" runat="server" Text="Fechar" CausesValidation="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <div id="login" style="top: 0px; width: 100%; height: 100%;" align="center">
            <table style="border: 1px solid #CCCCCC; width: 888px; background-color: #FFFFFF;">
                <tr>
                    <td style="height: 33px; text-align: center">
                        <asp:Label ID="lblDescricao" runat="server" Font-Names="Calibri Light" Font-Size="Larger"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">
                        <table style="width: 100%; height: 100%">
                            <tr>
                                <td style="height: 15px;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtOrdenacao" runat="server" MaxLength="50" Style="float: left;" placeholder="Pesquisa Descrição" Width="150px" CssClass="configtext"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 25px">
                                                            <asp:ImageButton ID="BtOk" runat="server" Style="float: right" ImageUrl="~/Img_Sistema/Geral/Localiza_Pesquisa_Home.png" CausesValidation="False" Height="20px" Width="20px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 80px;">
                                                <asp:Label ID="lblLinha" runat="server" Text="Quantidade" CssClass="configlabel" Font-Bold="True" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px;">
                                                <asp:Label ID="lblQTDLinha" runat="server" Text="0" CssClass="configlabel" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px;">
                                                <asp:Label ID="lblMes" runat="server" Text="Total Mês" CssClass="configlabel" Visible="False" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 100px;">
                                                <asp:Label ID="lblCustoMes" runat="server" LabelFormat="R${0:0,00}" Text="0" CssClass="configlabel" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 150px;">
                                                <asp:Label ID="lblAno" runat="server" Text="Total (FY)|Ano Fiscal" CssClass="configlabel" Visible="False" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 100px;">
                                                <asp:Label ID="lblCustoAno" runat="server" Text="0" LabelFormat="R${0:0,00}" CssClass="configlabel" Visible="False"></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top">
                                    <div id="DivAtivo" runat="server" style="border: 1px solid #C0C0C0; width: 877px; top: 103px; height: 412px; overflow: auto" title=" ">
                                        <asp:DataGrid ID="dtgGrupo" runat="server" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Center" Width="850px" AutoGenerateColumns="False" BorderColor="#CCCCCC" CellPadding="5" CellSpacing="5" BorderWidth="1px" GridLines="Horizontal">
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <PagerStyle Mode="NumericPages" />
                                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" ForeColor="Black" Font-Strikeout="False" Font-Underline="False" />

                                            <Columns>
                                                <asp:ButtonColumn DataTextField="Descricao" CommandName="Select" Text="Descricao" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Indicadores/add_Black.png&quot; border=&quot;0&quot; height=&quot;20px&quot; width=&quot;20px&quot;&gt;">
                                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="18px" />
                                                    <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:ButtonColumn>

                                                <asp:BoundColumn DataField="Descricao" HeaderText="Descrição">
                                                    <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="QTD" HeaderText="Quantidade">
                                                    <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Mes" HeaderText="R$|Mês" DataFormatString="{0:R$##########0.#0}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Ano" HeaderText="R$|FY" DataFormatString="{0:R$##########0.#0}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                            </Columns>

                                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#FFFFFF" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#333333" />

                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
