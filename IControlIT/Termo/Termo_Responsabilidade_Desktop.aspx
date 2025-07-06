<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Termo_Responsabilidade_Desktop.aspx.vb" Inherits="IControlIT.Termo_Responsabilidade_Desktop" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>K2A - IControlIT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <script src='../JScript.js' type="text/javascript"></script>
    <link href="../CSSConfigObj.css" rel="Stylesheet" />
    <link href="../CSSEstruturalMaster.css" rel="Stylesheet" />
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
                                    <asp:Button ID="btImprimir" runat="server" Style="background-image: url('Image/Botao/bt_Tools_Salvar.png'); float: right;" Height="18px" Width="70px" Text="Imprimir" BorderColor="#3BB4FF" BorderStyle="Solid" BorderWidth="1px" BackColor="White" ForeColor="#3BB4FF" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Image ID="imgLogo" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: Left;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: Left;">
                                    <asp:Label ID="lbl_titulo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                    <asp:Label ID="lbl_SubTitulo" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtDadosUsuario" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="border: 1px solid #000000; width: 686px">
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtMatricula" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_matricula" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtCentroCusto" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_centroCusto" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtLocalidade" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_localidade" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtDepartamento" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_departamento" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtCiente" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtDadosAparelho" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="border: 1px solid #000000; width: 686px">
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtEquipamento" runat="server" Font-Names="Arial"
                                                    Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_Equipamento" runat="server" Font-Names="Arial"
                                                    Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtModelo" runat="server" Font-Names="Arial"
                                                    Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_Modelo" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtCodigo_TI" runat="server" Font-Names="Arial"
                                                    Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_CodigoTI" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtPatrimonio" runat="server" Font-Names="Arial"
                                                    Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_Patrimonio" runat="server" Font-Names="Arial"
                                                    Font-Size="9pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtNumero_Serie" runat="server" Font-Names="Arial"
                                                    Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_Numero_Serie" runat="server" Font-Names="Arial"
                                                    Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtAcessorio" runat="server" Font-Names="Arial"
                                                    Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_Acessorio" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto1" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                    <asp:Label ID="lbl_txtColaborador" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                    <asp:Label ID="lbl_txtTexto2" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_1" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_2" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_3" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="llbl_txtTexto3_4" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_5" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_6" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_7" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_8" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_9" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_10" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <table style="width: 500px">
                                        <tr>
                                            <td style="width: 72px;">
                                                <asp:Label ID="lbl_txtData" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                            <td style="width: 167px;">
                                                <asp:Label ID="lbl_Data" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <p>____________________</p>
                                            </td>
                                            <td>
                                                <p>____________________</p>
                                            </td>
                                            <td>
                                                <p>____________________</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_Ass_1" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Ass_2" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Ass_3" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto9" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto10" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto11" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto12" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto13" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto14" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto15" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto16" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto17" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto18" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto19" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto22" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto23" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto24" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
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
