<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="CockPit_Detalhe.aspx.vb" Inherits="IControlIT.CockPit_Detalhe" EnableEventValidation="true" %>

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

        <div id="login" style="top: 0px; width: 100%; height: 100%; background-color: #EEEEEE;" align="center">
            <table style="width: 888px;">
                <tr>
                    <td style="height: 33px; text-align: center">
                        <asp:Label ID="lblDescricao" runat="server" Font-Names="Calibri Light" Font-Size="Larger"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">
                        <table style="border: 1px solid #CCCCCC; width: 100%; height: 100%; background-color: #FFFFFF;">
                            <tr>
                                <td style="height: 15px;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtOrdenacao" runat="server" MaxLength="50" Style="float: left;" placeholder="Pesquisa Descrição" Width="830px" CssClass="configtext"></asp:TextBox>
                                            </td>
                                            <td style="width: 32px">
                                                <asp:ImageButton ID="BtOk" runat="server" ImageUrl="~/Img_Sistema/Geral/Localiza_Pesquisa_Home.png" CausesValidation="False" Height="20px" Width="20px" />
                                            </td>
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
                                                <asp:ButtonColumn DataTextField="Nm_Consumidor" CommandName="Select" Text="Descricao" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Indicadores/add_Black.png&quot; border=&quot;0&quot; height=&quot;20px&quot; width=&quot;20px&quot;&gt;">
                                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="18px" />
                                                    <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:ButtonColumn>

                                                <asp:BoundColumn DataField="Nm_Consumidor" HeaderText="Descrição">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Fora_Horario" HeaderText="R$|Fora Horário" DataFormatString="{0:R$##########0.#0}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Final_Semana" HeaderText="R$|Final Semana" DataFormatString="{0:R$##########0.#0}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Id_Filtro" HeaderText="Id_Filtro" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Id_Consumidor" HeaderText="Id_Consumidor" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Dt_Lote" HeaderText="Data da Conta" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="ValorTotalRetornoMarcacaoMes" HeaderText="Marcado" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                            </Columns>

                                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#FFFFFF" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#333333" />

                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td></td>
                                            <td style="width: 100px;">
                                                <asp:Label ID="lblTotal" runat="server" CssClass="configlabel" Style="float: left;" Text="Custo Total"></asp:Label>
                                            </td>
                                            <td style="width: 120px;">
                                                <asp:TextBox ID="txtTotal" runat="server" CssClass="configtext" Style="float: left; text-align: right;" Width="115px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
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
