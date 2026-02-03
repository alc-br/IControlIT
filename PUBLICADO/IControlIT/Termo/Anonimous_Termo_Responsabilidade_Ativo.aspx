<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Anonimous_Termo_Responsabilidade_Ativo.aspx.vb" Inherits="IControlIT.Anonimous_Termo_Responsabilidade_Ativo" %>

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
        <!--Msg -->
        <asp:Panel ID="pnlMSG" runat="server" Style="top: 0px; left: 0px; position: fixed; height: 100%; width: 100%; z-index: 120; background-color: transparent;" Visible="false">
            <table style="width: 100%; height: 100%; background-color: #FFFFFF;">
                <tr>
                    <td></td>
                    <td style="width: 290px;">
                        <asp:Label ID="lblMsgImei" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" Text="Favor cadastrar o IMEI e tentar novamente."></asp:Label>
                    </td>
                    <td></td>
                </tr>
            </table>
        </asp:Panel>

        <div>
            <table style="width: 100%">
                <tr>
                    <td runat="server" align="center">
                        <table style="width: 80%;">
                            <tr>
                                <td>
                                    <asp:Button ID="btImprimir" class="btn btn-info" Width="100px" runat="server" Text="Imprimir" CausesValidation="False" />
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
                                    <asp:Label ID="lbl_titulo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"></asp:Label>
                                    <asp:Label ID="lbl_SubTitulo" runat="server" Font-Names="Arial" Font-Size="14pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtDadosUsuario" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="border: 1px solid #000000; width: 100%">
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtMatricula" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_matricula" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtCentroCusto" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_centroCusto" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid #000000; width: 170px;">
                                                <asp:Label ID="lbl_txtLocalidade" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_localidade" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtDepartamento" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_departamento" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtCiente" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtDadosAparelho" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="border: 1px solid #000000; width: 100%">
                                        <tr>
                                            <td style="width: 170px; border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtLinha" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_linha" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtImeiAparelho" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_ImeiAparelho" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 170px; border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtOperadora" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_operadora" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtChipAparelho" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_ChipAparelho" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 170px; border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtMarcaModeloAparelho" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_MarcaModeloAparelho" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtAcessorio" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_Acessorio" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 170px; border: 1px solid #000000;">
                                                <asp:Label ID="lbl_txtTipoAtivo" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="border: 1px solid #000000;">
                                                <asp:Label ID="lbl_TipoAtivo" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto1" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                    <asp:Label ID="lbl_txtColaborador" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                    <asp:Label ID="lbl_txtTexto2" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_1" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_2" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_3" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="llbl_txtTexto3_4" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_5" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_6" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_7" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_8" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_9" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_10" runat="server" Font-Names="Arial" Font-Size="13pt"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <table style="width: 500px; height: 90px">
                                        <tr>
                                            <td style="width: 200px;">
                                                <asp:Label ID="lbl_txtData1" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td style="width: 300px;">
                                                <asp:Label ID="lbl_Data1" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
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
                                                <asp:Image ID="imgAss" runat="server" Visible="False" ImageUrl="~/Img_Sistema/Ass_Termo/Assinatura_Sodexo_Telefonia.png" />
                                                <asp:Label ID="lblLinha_Ass1" runat="server" Font-Names="Arial" Font-Size="11pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLinha_Ass2" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLinha_Ass3" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_Ass_1" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Ass_2" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Ass_3" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <table style="width: 500px; height: 100px">
                                        <tr>
                                            <td style="width: 200px;">
                                                <asp:Label ID="lbl_txtData2" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                            <td style="width: 300px;">
                                                <asp:Label ID="lbl_Data2" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
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
                                                <asp:Label ID="lblLinha_Ass4" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLinha_Ass5" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLinha_Ass6" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_Ass_4" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Ass_5" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Ass_6" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td style="height: 40px"></td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto9" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto10" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto11" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto12" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto13" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto14" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto15" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto16" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto17" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto18" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto19" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto22" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto23" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto24" runat="server" Font-Names="Arial" Font-Size="12pt"></asp:Label>
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
