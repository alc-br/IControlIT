<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Termo_Devolucao_Ativo.aspx.vb" Inherits="IControlIT.Termo_Devolucao_Ativo" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
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
        <div>
            <table style="width: 100%">
                <tr>
                    <td runat="server" align="center">
                        <table style="width: 700px;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btImprimir" class="btn btn-info" Width="100px" runat="server" Text="Imprimir" CausesValidation="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Image ID="imgLogo" Width="200px" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: Left;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:Label ID="lbl_titulo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_Eu" runat="server" Font-Names="Arial" Font-Size="10pt">Eu,</asp:Label>
                                    <asp:Label ID="lbl_Colaborador" runat="server" Font-Bold="True"
                                        Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                    <asp:Label ID="lbl_Virgula" runat="server">,</asp:Label>
                                    <asp:Label ID="lbl_txtTexto1" runat="server" Font-Names="Arial"
                                        Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtDadosUsuario" runat="server" Font-Bold="True"
                                        Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="border: 1px solid #000000; width: 687px;">
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtMatricula" runat="server" Font-Names="Arial"
                                                    Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_matricula" runat="server" Font-Names="Arial"
                                                    Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtCentroCusto" runat="server" Font-Names="Arial"
                                                    Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_centroCusto" runat="server" Font-Names="Arial"
                                                    Font-Size="10pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtLocalidade" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_localidade" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtDepartamento" runat="server" Font-Names="Arial"
                                                    Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_departamento" runat="server" Font-Names="Arial"
                                                    Font-Size="10pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtCiente" runat="server" Font-Bold="True"
                                        Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtDadosAparelho" runat="server" Font-Bold="True"
                                        Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="border: 1px solid #000000; width: 687px">
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtLinha" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_linha" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtImei" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_Imei" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtOperadora" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_operadora" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtChipAparelho" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_ChipAparelho" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtAtivo_Tipo" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_Ativo_Tipo" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtAcessorios" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_acessorios" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtMarcaModelo" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_marcaModelo" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtObs" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 500px; height: 100px">
                                        <tr>
                                            <td style="width: 200px;">
                                                <asp:Label ID="lbl_txtData1" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="width: 300px;">
                                                <asp:Label ID="lbl_Data1" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_Linha_Ass_1" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Linha_Ass_2" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Linha_Ass_3" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_Ass_1" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Ass_2" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Ass_3" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
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
