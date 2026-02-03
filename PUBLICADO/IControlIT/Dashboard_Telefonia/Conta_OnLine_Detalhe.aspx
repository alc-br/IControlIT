<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Conta_OnLine_Detalhe.aspx.vb" Inherits="IControlIT.Conta_OnLine_Detalhe" EnableEventValidation="true" %>

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

    <%-- trava o zoom quando visualizado no mobile e almenta a scala dos objetos na tela deixando responsivo --%>
    <meta name="viewport" content="width=device-width, initial-scale=0.7, maximum-scale=0.7, user-scalable=no" />

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

        .bgGraficos {
            background-color: #EEEEEE;
            width: 100%;
            overflow: auto;
            padding: 60px 15px 15px 15px;
        }

        .cardGrafico {
            width: 100%;
            background-color: #FFFFFF;
            box-shadow: rgba(0,0,0,0.2) 0px 2px 8px 0px;
            border-radius: 10px;
            padding: 15px;
        }

        .tblResponsivo {
            width: 800px;
        }

        /*/ Responsivo para tela mobile /*/
        @media only screen and (max-width: 600px) {

            .bgGraficos {
                background-color: #EEEEEE;
                width: 100%;
                overflow: auto;
                padding: 60px 15px 15px 15px;
            }

            .tblResponsivo {
                width: 100%;
            }
        }
    </style>

    <script type="text/javascript">

        function PrintPDF() {
            //document.getElementById("btImprimir").style.visibility = "hidden";
            //document.getElementById("btVoltar").style.visibility = "hidden";
            window.print();
            //document.getElementById("btImprimir").style.visibility = "visible";
            //document.getElementById("btVoltar").style.visibility = "visible";
        }

    </script>
</head>
<body class="pagina" runat="server" bgcolor="#EEEEEE">
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server" AsyncPostBackTimeout="9000" CombineScripts="True"></cc1:ToolkitScriptManager>

        <!-- Popup Conta Online Detalhe -->
        <div id="divContaOnline" runat="server" class="filtro" visible="true">

            <table style="width: 100%">
                <tr>
                    <td align="center" runat="server">
                        <!-- dados agrupados ********************************************************************* -->
                        <table id="tbBotao" runat="server" class="tblResponsivo">
                            <tr>
                                <td>
                                    <!-- detalhamento ************************************************************************ -->
                                    <div id="printable" runat="server" class="cardGrafico">
                                        <table id="tbPdf" runat="server" style="width: 100%; height: 100%;">
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Timer ID="timerPDF" runat="server" Interval="2000" OnTick="timerPDF_Tick"></asp:Timer>
                                                    <asp:Button ID="btVoltar" class="btn btn-warning" Width="100px" runat="server" Text="VOLTAR" CausesValidation="False" OnClick="btVoltar_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    <h1 id="hrDetalhamento" runat="server">Detalhamento da Conta</h1>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="divGrid" runat="server" style="left: 5px; overflow: auto; width: 100%; border-radius: 6px; border: 1px solid #B06AB3; background-color: #cfa5d1; padding: 5px 1px 5px 1px" title=" ">
                                                        <asp:DataGrid ID="dtgDetalhamento" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" BackColor="#FFFFFF"
                                                            EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                            HorizontalAlign="Left" Font-Size="9pt" Width="100%" ForeColor="#818181" AllowPaging="false" BorderColor="#f7f0f7">

                                                            <Columns>

                                                                <asp:BoundColumn DataField="Destino" HeaderText="ID" HeaderStyle-BackColor="#cfa5d1" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundColumn>

                                                                <asp:BoundColumn DataField="Tipo" HeaderText="Tipo" HeaderStyle-BackColor="#cfa5d1" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundColumn>

                                                                <asp:BoundColumn DataField="Data" HeaderText="Data" HeaderStyle-BackColor="#cfa5d1" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundColumn>

                                                                <asp:BoundColumn DataField="Consumo" HeaderText="Consumo" HeaderStyle-BackColor="#cfa5d1" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundColumn>

                                                                <asp:BoundColumn DataField="Custo" HeaderText="Custo" DataFormatString="{0:R$##########0.#0}" HeaderStyle-BackColor="#cfa5d1" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundColumn>

                                                                <asp:BoundColumn DataField="Grupo" HeaderText="Grupo" Visible="False">
                                                                    <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundColumn>

                                                                <asp:BoundColumn DataField="Fora_Horario" HeaderText="Fora_Horario" Visible="False">
                                                                    <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundColumn>

                                                                <asp:BoundColumn DataField="Final_Semana" HeaderText="Final_Semana" Visible="False">
                                                                    <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundColumn>

                                                            </Columns>

                                                            <PagerStyle Mode="NumericPages" BackColor="#cfa5d1" BorderColor="#cfa5d1" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" Font-Bold="true" ForeColor="#FFFFFF" Font-Names="Segoe UI Semibold" />
                                                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Segoe UI" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                                            <AlternatingItemStyle BackColor="#efe1ef" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                        </asp:DataGrid>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
