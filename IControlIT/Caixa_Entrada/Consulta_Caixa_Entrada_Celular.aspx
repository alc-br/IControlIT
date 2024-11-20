<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Consulta_Caixa_Entrada_Celular.aspx.vb" Inherits="IControlIT.Caixa_Entrada_Consulta_Caixa_Entrada_Celular" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>K2A - IControlIT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <script src='JScript.js' type="text/javascript"></script>
    <link href="CSSConfigObj.CSS" rel="Stylesheet" />
    <link rel="Shortcut Icon" href="Img_Sistema/logo.ico" />

    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>

    <%-- trava o zoom quando visualizado no mobile e almenta a scala dos objetos na tela deixando responsivo --%>
    <meta name="viewport" content="width=device-width, initial-scale=0.8, maximum-scale=0.8, user-scalable=no" />

</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server" AsyncPostBackTimeout="9000" CombineScripts="True"></cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <!--cabecalho-->
                <div style="position: fixed; top: 0px; left: 0px; width: 100%; background-color: #000000; z-index: 50;" align="center">
                    <div class="container" style="width: 100%; height: 45px; float: none;">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50px; height: 40px">
                                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Img_Sistema/Default/Logo_Menu_IControlIT_Branco.png" Style="position: relative; top: 1px; left: 5px; height: 25px;" ToolTip="Menu" />
                                </td>
                                <td style="width: 100%">&nbsp;</td>
                                <td style="width: 30px;" align="center">
                                    <asp:ImageButton ID="btVoltar" runat="server" Style="float: right; padding: 3px" ImageUrl="~/Img_Sistema/Geral/bt_Clase_Branco.png" Height="22px" CausesValidation="False" OnClick="btVoltar_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <!--cabecalho Titulo-->
                <div style="position: fixed; top: 45px; left: 0px; width: 100%; height: 80px; background-color: #323232; z-index: 50;" align="center">
                    <div style="width: 100%; height: 80px; float: none;">
                        <div class="container">
                            <table style="width: 100%; height: 80px; text-align: center">
                                <tr>
                                    <td style="width: 32px">
                                        <asp:Image ID="imgInbox" runat="server" ImageUrl="~/Img_Sistema/Botao/ic_inbox.png" Width="32px" />
                                    </td>
                                    <td style="text-align: left; padding-left: 15px">
                                        <asp:Label ID="Label3" runat="server" Text="Caixa de Entrada" ForeColor="#818181" Font-Names="Segoe UI Semibold" Font-Size="14pt"></asp:Label>
                                    </td>
                                    <td style="width: 42px">
                                        <asp:ImageButton ID="btAbrir" runat="server" ImageUrl="~/Img_Sistema/Botao/bt_Tools_Abrir.png" Width="42px" OnClick="btAbrir_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <!--Abrir-->
                <asp:Panel ID="pnlAbir" runat="server" Style="top: 0px; left: 0px; position: fixed; height: 100%; width: 100%; z-index: 120; background-color: #FFFFFF" Visible="False">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 100%; text-align: center">
                                                        <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Abrir Contestação" Style="float: none" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ListBox ID="lstMenssagem" runat="server" CssClass="configlistboxAbrir" Height="100px" Width="100%"></asp:ListBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
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
                                                        <asp:Button ID="btExecutar" class="btn btn-success" Width="80px" runat="server" Text="Abrir" CausesValidation="False" />
                                                    </td>

                                                    <td style="width: 84px">
                                                        <asp:Button ID="btFechar" class="btn btn-default" Width="80px" runat="server" Text="Fechar" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <!--detalhamento -->
                <asp:Panel ID="pnlmsg" runat="server" Style="top: 0px; left: 0px; position: fixed; height: 100%; width: 100%; z-index: 120; background-color: #FFFFFF" Visible="False">
                    <table style="width: 100%; height: 100%;" align="center">
                        <tr>
                            <td align="center" style="padding: 15px">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblMsgLocaliza" runat="server" CssClass="configlabel" Text="Mensagem" Style="float: none" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div id="DivAtivo" runat="server">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 60px;">
                                                                        <asp:Label ID="lblAssunto" runat="server" CssClass="configlabel" Text="Assunto:" ForeColor="#1E88E5" Font-Names="Segoe UI Semibold"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDescricaoAssunto" runat="server" Style="float: left" CssClass="configlabel" Font-Names="Segoe UI"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblEmailDestino" runat="server" CssClass="configlabel" Text="Destino:" ForeColor="#1E88E5" Font-Names="Segoe UI Semibold"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDescricaoEmailDestino" runat="server" Style="float: left" CssClass="configlabel" Font-Names="Segoe UI"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblEmailCopia" runat="server" CssClass="configlabel" Text="Cópia:" ForeColor="#1E88E5" Font-Names="Segoe UI Semibold"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDescricaoEmailCopia" runat="server" Style="float: left" CssClass="configlabel" Font-Names="Segoe UI"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblTexto" runat="server" CssClass="configlabel" Text="Texto:" ForeColor="#1E88E5" Font-Names="Segoe UI Semibold"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDescricaoTexto1" runat="server" Style="float: left" CssClass="configlabel" Font-Names="Segoe UI"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDescricaoTexto2" runat="server" Style="float: left; line-height: 20px; white-space: pre-wrap; text-align: justify;" Font-Names="Segoe UI Semibold" ForeColor="White"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDescricaoTexto3" runat="server" Style="float: left; line-height: 20px; white-space: pre-wrap; text-align: justify;" Font-Names="Segoe UI" ForeColor="#818181"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDescricaoTexto4" runat="server" Style="float: left; line-height: 20px; white-space: pre-wrap; text-align: justify;" Font-Names="Segoe UI" ForeColor="#818181"> </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDescricaoTexto5" runat="server" Style="float: left; line-height: 20px; white-space: pre-wrap; text-align: justify;" Font-Names="Segoe UI" ForeColor="#818181"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDescricaoTextAdicional" runat="server" Style="float: left; line-height: 20px; white-space: pre-wrap; text-align: justify;" Font-Names="Segoe UI" ForeColor="#818181"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td></td>

                                                    <td style="width: 84px">
                                                        <asp:Button ID="btReenviarEmail" class="btn btn-success" Width="80px" runat="server" Text="Reenviar" CausesValidation="False" />
                                                    </td>

                                                    <td style="width: 84px">
                                                        <asp:Button ID="btFecharMsg" class="btn btn-default" Width="80px" runat="server" Text="Fechar" CausesValidation="False" />
                                                    </td>
                                                    <asp:HiddenField ID="hdfIdMail" runat="server" />
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <!--corpo-->
                <div style="width: 100%">
                    <table style="width: 100%">
                        <tr style="height: 120px">
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="dtgLocaliza" runat="server" AutoGenerateColumns="False" EnableTheming="True" HorizontalAlign="Center" Style="font-size: 10pt; font-family: 'Segoe UI'" Width="100%"
                                    ShowHeader="False" BorderColor="#F0F0F0" BorderWidth="0px" CellPadding="5" CellSpacing="5" ForeColor="Black" GridLines="None">

                                    <Columns>
                                        <asp:ButtonColumn DataTextField="Id_Mail_Caixa_Siada" CommandName="Select" Text="Id_Mail_Caixa_Siada" ItemStyle-HorizontalAlign="Center" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Botao/Grid/Grid_Check_Pesquisa.png&quot; border=&quot;0&quot; height=&quot;28px&quot; width=&quot;28px&quot;&gt;">
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:ButtonColumn>

                                        <asp:TemplateColumn HeaderText="">
                                            <ItemTemplate>
                                                <table style="width: 100%; border-bottom: 1px solid #818181;">
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label3" runat="server" Text="<%# Bind('Assunto') %>" ForeColor="#323232" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label2" runat="server" Text="<%# Bind('Dt_Programacao') %>" ForeColor="#818181" Font-Names="Segoe UI Semibold" Font-Size="9pt"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Text="<%# Bind('Dt_Saida') %>" ForeColor="#818181" Font-Names="Segoe UI Semibold" Font-Size="9pt"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="<%# Bind('Texto') %>" ForeColor="#818181" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Id_Mail_Caixa_Siada" Visible="False"></asp:BoundColumn>
                                    </Columns>
                                    <PagerStyle Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
