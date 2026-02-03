<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Correio.aspx.vb" Inherits="IControlIT.Correio" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>K2A - IControlIT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <script src='../JScript.js' type="text/javascript"></script>
    <link href="../CSSConfigObj.css" rel="Stylesheet" />
    <link href="../CSSEstruturalMaster.css" rel="Stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="Stylesheet" />
    <link rel="Shortcut Icon" href="Img_Sistema/logo.ico" />
</head>

<body>
    <form id="form1" runat="server" defaultbutton="btImprimir">
        <div class="textoTit">
            <table style="width: 400px">
                <tr>
                    <td colspan="2" style="width: 400px">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 400px;">
                                    <asp:Label ID="lblEtiqueta" runat="server" EnableTheming="True" Font-Names="Arial" Font-Overline="False" Font-Size="20pt" ForeColor="#333333" Height="25px">Etiqueta Correio</asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btImprimir" class="btn btn-info" Width="100px" runat="server" Text="Imprimir" CausesValidation="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">&nbsp;</td>
                </tr>

                <tr>
                    <td style="width: 34px;"></td>
                    <td style="width: 365px;"></td>
                </tr>


                <tr>
                    <td style="width: 34px;">
                        <asp:Label ID="lbl_txtAC" runat="server" CssClass="textoExp">A/C</asp:Label>
                    </td>
                    <td style="width: 365px;">
                        <asp:Label ID="lbl_Colaborador" runat="server" CssClass="textoNeg"></asp:Label>
                    </td>
                </tr>


                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lbl_Destinatario" runat="server" CssClass="textoNeg2">Destinatario</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lbl_txtDestinatario" runat="server" CssClass="textoExp"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
