<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Conta_OnLine_Detalhe_Impressao.aspx.vb" Inherits="IControlIT.Conta_OnLine_Detalhe_Impressao" EnableEventValidation="true" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
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
            left: 1px;
            position: relative;
            float: left;
            top: 0px;
            height: 15px;
        }

        .configlabelcabGraf {
            font-size: 9pt;
            font-family: Arial;
            background-color: transparent;
            position: relative;
            float: left;
            height: 15px;
        }

        SPAN {
            font-size: 12pt;
        }
    </style>

</head>
<body class="pagina" runat="server" bgcolor="#EEEEEE">
    <form id="form1" runat="server">

        <!-- Msg para tela fazia ****************************************************************************** -->
        <asp:Panel ID="pnlDetalhe" runat="server" Style="top: 0px; left: 0px; position: fixed; height: 100%; width: 100%; z-index: 120; background-color: rgba(0,0,0,0.9);" Visible="false">
            <table style="border: 1px ridge #CCCCCC; width: 400px; height: 90px; background-color: #FFFFFF; position: fixed; top: 195px; left: 374px; border-radius: 6px 6px;" align="center">
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblObservacao" runat="server" CssClass="configlabel" Text="Você não tem permissão para detalhar a conta de outro usuário." Style="z-index: 113; float: none;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <!-- tela  ****************************************************************************** -->
        <div id="login" style="vertical-align: top; height: 100%; width: 100%" align="center">
            <table style="width: 860px; height: 530px;">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 150px">
                                    <input type="button" value="Voltar" class="btn btn-default" onclick="history.go(-1)" style="width: 110px; cursor: pointer">
                                </td>
                                <td>
                                    <asp:Label ID="lblDescricao" Width="600px" runat="server" Font-Names="Calibri Light"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>
                        <!-- Bloco 02 ****************************************************************************** -->
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 820px;">
                            <tr>
                                <td style="width: 100%; height: 25px; text-align: center">
                                    <h1 class="Dash">Detalhamento da Conta</h1>
                                </td>
                            </tr>
                        </table>

                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <div id="divVolume" runat="server" style="border: 1px solid #CCCCCC; width: 818px; top: 103px; height: 120px; background-color: #FFFFFF; overflow: auto" title=" ">
                                        <asp:DataGrid ID="dtgVolume" runat="server" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Center" Width="780px" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderWidth="1px" GridLines="Horizontal">
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <PagerStyle Mode="NumericPages" />
                                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" ForeColor="Black" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />

                                            <Columns>
                                                <asp:BoundColumn DataField="Descricao" HeaderText="Descrição">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Valor" HeaderText="Valor" DataFormatString="{0:R$##########0.#0}">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                            </Columns>

                                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#FFFFFF" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#333333" />

                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <!-- Bloco 03 ****************************************************************************** -->
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 820px;">
                            <tr>
                                <td style="width: 100%; height: 25px; text-align: center">
                                    <h1 class="Dash">Detalhamento da Conta</h1>
                                </td>
                            </tr>
                        </table>

                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <div id="DivAtivo" runat="server" style="border: 1px solid #CCCCCC; width: 818px; top: 103px; height: 235px; background-color: #FFFFFF; overflow: auto" title=" ">
                                        <asp:DataGrid ID="dtgDetalhamento" runat="server" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Center" Width="780px" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderWidth="1px" GridLines="Horizontal">
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <PagerStyle Mode="NumericPages" />
                                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" ForeColor="Black" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:BoundColumn DataField="Destino" HeaderText="Documento">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Tipo" HeaderText="Tipo">
                                                    <HeaderStyle Width="200px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Data" HeaderText="Data">
                                                    <HeaderStyle Width="80px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Consumo" HeaderText="Consumo">
                                                    <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Custo" HeaderText="Custo" DataFormatString="{0:R$##########0.#0}">
                                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Grupo" HeaderText="Grupo" Visible="False">
                                                    <HeaderStyle Width="10px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Fora_Horario" HeaderText="Fora_Horario" Visible="False">
                                                    <HeaderStyle Width="10px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Final_Semana" HeaderText="Final_Semana" Visible="False">
                                                    <HeaderStyle Width="10px" />
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
        </div>
    </form>
</body>
</html>
