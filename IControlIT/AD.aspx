<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AD.aspx.vb" Inherits="IControlIT.AD" EnableEventValidation="true" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Sistema de gestão de telefonia</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <script src='JScript.js' type="text/javascript"></script>
    <link href="CSSConfigObj.CSS" rel="Stylesheet" />
    <link rel="Shortcut Icon" href="Img_Sistema/logo.ico" />

    <script src="Scripts/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <link href="CSSEstruturalMaster.css" rel="Stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript">
        //monta tamanho da tela
        window.onload = maxWindow;
        function maxWindow() {
            window.moveTo(0, 0);

            if (document.all) { top.window.resizeTo((screen.availWidth - 1), screen.availHeight); }

            else if (document.layers || document.getElementById) {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = (screen.availWidth - 1);
                }
            }
        }

        //controle do modal
        var idModal;
        function Modal(idModal) { $(idModal).modal(); }
        function ModalClose(idModal) { $(idModal).modal("hide"); }

    </script>

    <style type="text/css">
        INPUT {
            border: 0px none transparent;
            padding: 0px;
            background-position: left top;
            background-repeat: no-repeat;
            font-family: Tahoma,Verdana,Segoe,sans-serif;
            float: left;
            margin-left: 0px;
            margin-right: 0px;
            margin-top: 0px;
        }

        .configCombo {
            font-size: 8pt;
            color: black;
            font-family: Arial;
        }

        .configlabel {
            font-size: 9pt;
            color: black;
            font-family: Arial;
            background-color: transparent;
        }

        .modalBackground {
            background-color: Aqua;
            top: 0px !important;
            left: 0px !important;
            position: absolute !important;
            z-index: 1 !important;
        }

        .modalPopup {
            background-color: #fffddd;
            padding: 3px;
            z-index: 10001;
        }
    </style>
</head>

<body bgcolor="White">
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <!--termo de uso-->
                <div>
                    <div class="modal fade" id="myModalSubTermo" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button id="Button3" runat="server" type="button" class="close" data-dismiss="modal" causesvalidation="False">&times;</button>
                                    <h4 class="modal-title">Termo de Uso de Ativo</h4>
                                </div>
                                <div class="modal-body">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlMsgTermo" runat="server" Height="269px" Width="560px">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTextoNome" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTexto1" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="DivAtivo" runat="server" style="left: 5px; overflow: auto; width: 560px; top: 103px; height: 84px" title=" ">
                                                                    <asp:DataGrid ID="dtgAtivo" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                                                        EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                                        HorizontalAlign="Left" Font-Size="9pt" Width="540px" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                                                                        <Columns>
                                                                            <asp:BoundColumn DataField="Imagem" DataFormatString="&lt;img src=&quot;{0}&quot; border=&quot;0&quot; height=&quot;20px&quot; width=&quot;20px&quot;&gt;">
                                                                                <HeaderStyle Width="20px" />
                                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                            </asp:BoundColumn>

                                                                            <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Linha">
                                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="Nm_Ativo_Tipo" HeaderText="Tipo">
                                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="Dt_Hr_Ativacao" HeaderText="Ativação">
                                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="Dt_Hr_Desativacao" HeaderText="Desativação">
                                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                            </asp:BoundColumn>
                                                                        </Columns>

                                                                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                                        <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                                        <PagerStyle Mode="NumericPages" />
                                                                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                    </asp:DataGrid>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTexto2" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTexto3" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTexto4" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTexto5" runat="server" CssClass="configlabel" Style="float: left;" Font-Bold="True" Font-Italic="True" Font-Underline="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td></td>
                                            <td style="width: 110px;">
                                                <button type="button" class="btn btn-default" style="width: 100px" data-dismiss="modal" causesvalidation="False">Fechar</button>
                                            </td>
                                            <td style="width: 110px;">
                                                <asp:Button ID="btAcordo" class="btn btn-success" Width="100px" runat="server" Text="De Acordo" Style="float: right" CausesValidation="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="DivHome" runat="server" style="width: 100%; height: 100vh; overflow: auto;">
                    <table style="width: 100%; height: 100%">
                        <tr align="center">
                            <td valign="middle">
                                <table>
                                    <!--Cabecario-->
                                    <tr>
                                        <td>
                                            <table style="width: 100%; height: 100px; background-color: transparent">
                                                <tr>
                                                    <td></td>

                                                    <td style="width: 26px;">
                                                        <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Img_Sistema/Default/logo_preto.png" Height="40px" ToolTip="Menu" CausesValidation="False" />
                                                    </td>

                                                    <!--meio-->
                                                    <td style="width: 80px;">
                                                        <asp:Timer ID="Timer1" runat="server" Interval="1100" />
                                                    </td>

                                                    <td style="width: 600px; text-align: center;">
                                                        <asp:Label ID="lblTitulo" runat="server" Font-Names="Microsoft JhengHei Light" Font-Size="18pt" ForeColor="#CCCCCC" Style="float: none"></asp:Label>
                                                    </td>

                                                    <td style="width: 80px;"></td>
                                                    <!--meio-->

                                                    <td style="width: 35px;">
                                                        <asp:Image ID="Image2" runat="server" Width="32px" ImageUrl="~/Img_Sistema/Geral/ic_android.png" />
                                                    </td>

                                                    <td style="width: 35px;">
                                                        <asp:Image ID="Image3" runat="server" Width="32px" ImageUrl="~/Img_Sistema/Geral/ic_apple.png" />
                                                    </td>

                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <!--Titulo-->
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="text-align: center; height: 40px">
                                                        <asp:Label ID="lblAcesso" runat="server" Font-Names="Microsoft JhengHei Light" Font-Size="32pt" ForeColor="#333333" Text="Aguarde o Acesso ao Portal" Style="float: none;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblSite" runat="server" CssClass="configlabel" ForeColor="#333333" Text="www.IControlIT.com.br" Font-Names="Microsoft JhengHei Light" Font-Size="16pt" Style="float: none;"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <!--Espaco de linha-->
                                    <tr>
                                        <td style="height: 30px"></td>
                                    </tr>

                                    <!--Rodape-->
                                    <tr>
                                        <td style="text-align: center" align="center">
                                            <table style="width: 100%; float: none">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDadosTermo" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Silver" Width="350px">Acesse o site da IControlIT www.IControlIT.com.br e baixe o nosso APP na loja de aplicativos do seu smartphone. copyright © 2018 IControlIT.</asp:Label>
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

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>