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

    <style type="text/css">
        .espacamento{
            line-height: 1.6rem;
        }		
		
		@page {
  margin: 27mm 23mm 27mm 23mm; 
}

td {
line-height:19px;
		
     </style>
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
            <table style="width: 100%;">
                <tr>
                    <td runat="server" align="center">
                        <table style="width: 60%;">
                            <tr>
                                <td>
                                    <asp:Button ID="btImprimir" class="btn btn-info" Width="100px" runat="server" Text="Imprimir" CausesValidation="False" />
                                </td>
                            </tr>

							<tr>
															<td>
								<asp:Image ID="imgLogo1" runat="server" Visible="True" ImageUrl="~/Img_Sistema/Ass_Termo/Fortlev-cabecalho.png" />
								</td>
								</tr>

                            <tr>
                                <td style="text-align: left;visibility:hidden;">
                                    <asp:Image ID="imgLogo" Width="200px" runat="server" />
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: center;">
                                    <asp:Label ID="lbl_titulo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                    <asp:Label ID="lbl_SubTitulo" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>
							
                            <tr>
																	 
                                <td>
								<div style="height: 3px;">&nbsp;</div>
                                    <asp:Label ID="lbl_txtDadosUsuario" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>
                            <tr style="visibility:hidden;">
                                <td>
                                    <table style="border: none; width: 100%;opacity(100%);">
                                        <tr>

                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: none;">
                                                <asp:Label ID="lbl_txtCentroCusto" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                            <td style="border: none;">
                                                <asp:Label ID="lbl_centroCusto" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 16px;">&nbsp;</td>
                                            <td style="border: none;">
                                                <asp:Label ID="lbl_txtDepartamento" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                            <td style="border: none;">
                                                <asp:Label ID="lbl_departamento" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtCiente" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtDadosAparelho" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>
                            
                                </td>
                            </tr>
							
							<div style="height: 100px;">&nbsp;</div>
							
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto1" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
									
									                                            <span style="border: none;visibility:hidden">
                                                <asp:Label ID="lbl_txtMatricula" runat="server" Font-Names="Arial" Font-Size="1pt"></asp:Label></span>
												

									
                                    <span style="font-weight:bold;Font-Size=16px;font-family:arial;"><asp:Label ID="lbl_txtColaborador" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label></span>
                                    <asp:Label ID="lbl_txtTexto2" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
									<asp:Label ID="lbl_matricula" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label><span>
									<asp:Label ID="lbl_txtTexto3" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                </span>
                                </td>

							</tr>

							<tr>
                                            <td style="width: 16px;height:40px">&nbsp;</td>
							</tr>
							
							<table style="width:1100px">
							
                            <tr>
                                <td>
								
                                    <asp:Label ID="lbl_txtTexto3_1" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                </td>
                            </tr>
							<tr>
                                            <td style="width: 16px;">&nbsp;</td>
							</tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_2" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                </td>
                            </tr>
							
							<tr>
                                            <td style="width: 16px;">&nbsp;</td>
							</tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_3" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                </td>
                            </tr>
							
							<tr>
                                            <td style="width: 16px;">&nbsp;</td>
							</tr>
                            <tr>
                                <td>
                                    <asp:Label ID="llbl_txtTexto3_4" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                </td>
                            </tr>
							
							<tr>
                                            <td style="width: 16px;">&nbsp;</td>
							</tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_5" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                </td>
                            </tr>
							
							<tr>
                                            <td style="width: 16px;">&nbsp;</td>
							</tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_6" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                </td>
                            </tr>
							
							<tr>
                                            <td style="width: 16px;">&nbsp;</td>
							</tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_7" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                </td>
                            </tr>
							
							<tr>
                                            <td style="width: 16px;">&nbsp;</td>
							</tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_8" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                </td>
                            </tr>
							
							<tr>
                                            <td style="width: 16px;">&nbsp;</td>
							</tr>
                            <tr>
                                <td style="font-weight:bold;text-decoration:underline;">
                                    <asp:Label ID="lbl_txtTexto3_9" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                </td>
                            </tr>
                            <tr style=visibility:hidden;>
                                <td>
                                    <asp:Label ID="lbl_txtTexto3_10" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                </td>
                            </tr>

                                <td>
                                    <table style="border: none;">
                                        <tr>
                                            <td style="width: 170px; border: none;height:30px;font-weight:bold">
                                                <asp:Label ID="lbl_txtLinha" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                            <td style="border: none;">
                                                <asp:Label ID="lbl_linha" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
											</tr>
											
										<tr>
                                            <td style="width: 170px; border: none;height:30px;font-weight:bold">
                                                <asp:Label ID="lbl_txtChipAparelho" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                            <td style="border: none;">
                                                <asp:Label ID="lbl_ChipAparelho" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
											</tr>
									
                                        <tr>
                                            <td style="width: 170px; border: none;height:30px;font-weight:bold">
                                                <asp:Label ID="lbl_txtMarcaModeloAparelho" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                            <td style="border: none;">
                                                <asp:Label ID="lbl_MarcaModeloAparelho" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
										</tr>
										
											<tr>
                                            <td style="border: none;padding-bottom:6px;font-weight:bold">
                                                <asp:Label ID="lbl_txtImeiAparelho" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>

                                            </td>
                                            <td style="border: none;">
                                                <asp:Label ID="lbl_ImeiAparelho" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
											</tr>
											
																	
											<div style="font-size:1px;line-height:10px">&nbsp;</td>

                                            <div style="border: none;visibility:hidden;height:1px">
                                                <asp:Label ID="lbl_txtOperadora" runat="server" Font-Names="Arial" Font-Size="18pt" style="width: 170px; border: none;height:30px;visibility:hidden;height:1px"></asp:Label>
                                                <asp:Label ID="lbl_operadora" runat="server" Font-Names="Arial" Font-Size="18pt" style="border: none;visibility:hidden;height:1px"></asp:Label>
											</div>
																												
											<div style="font-size:1px;line-height:10px">&nbsp;</td>

											
										<tr>
                                            <td style="border: none;font-weight:bold;padding-bottom:6px">
                                                <asp:Label ID="lbl_txtAcessorio" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                            <td style="border: none;">
                                                <asp:Label ID="lbl_Acessorio" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                        </tr>
										                                        
											<td style="font-size:3px">&nbsp;</td>

                                            <div style="border: none;visibility:hidden;height:1px">
                                                <asp:Label ID="lbl_txtTipoAtivo" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                                <asp:Label ID="lbl_TipoAtivo" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </div>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>

</table> 


                            <tr>
                                <td>
                                    <table style="width: 575px; height: 90px">
                                        <tr>
                                            <td style="width: 275px;font-weight:bold">
                                                <asp:Label ID="lbl_txtData1" runat="server" Font-Names="Arial" Font-Size="18pt" ></asp:Label>
                                            </td>
                                            <td style="width: 300px;visibility:none">
											                                            <span style="border: none; width: 170px;display:none">
                                                <asp:Label ID="lbl_txtLocalidade" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </span>
                                            <span style="border: none;">
                                                <asp:Label ID="lbl_localidade" runat="server" Font-Names="Arial" Font-Size="16pt"></asp:Label>
                                            </span>
					<span id="dataExibida" style="Font-Size:21px"></span>
						<script>
							var varData = new Date();
							var elementoHTML = document.getElementById("dataExibida");
							elementoHTML.textContent = varData.getDate() + "/" + (varData.getMonth() + 1) + "/" + varData.getFullYear() + " - " + varData.getHours() + ":" + varData.getMinutes();

						</script>
                                                <span style="color:white"><asp:Label ID="lbl_Data1" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label></span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <table style="width: 500px; height: 1px">
                                        <tr>
                                            <td style="width: 200px;">
                                                <asp:Label ID="lbl_txtData2" runat="server" Font-Names="Arial" Font-Size="1pt"></asp:Label>
                                            </td>
                                            <td style="width: 300px;">
                                                <asp:Label ID="lbl_Data2" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                                <asp:Label ID="lblLinha_Ass4" runat="server" Font-Names="Arial" Font-Size="18pt" ></asp:Label>
											
                                            <td>
                                                <asp:Label ID="lblLinha_Ass5" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_Ass_4" runat="server" Font-Names="Arial" Font-Size="18pt" style="font-weight:bold"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Ass_5" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                            <td style="height:30px;">
                                                <asp:Label ID="lbl_Ass_6" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>

                                        </tr>
										
                           
										
										                                            <td style="height:50px;">
                                                <asp:Label ID="lblLinha_Ass6" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
											
                                    </table>
                                </td>
								
										
                            </tr>

							 <tr>

                                <td>
                                    <table>
																 							<div style="height: 30px;">&nbsp;</div>
                                        <tr style="width: 275px;">
										    <td style="width:155px;">

                                                <asp:Label ID="lbl_Ass_3" runat="server" Font-Names="Arial" Font-Size="18pt"  style="font-weight:bold"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLinha_Ass2" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLinha_Ass3" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_Ass_1" runat="server" Font-Names="Arial" Font-Size="18pt" CssClass="espacamento"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Ass_2" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
											</tr>
									</table>
                                </td>
                            </tr>

<table>

										 <div style="height: 280px;">&nbsp;</div>

  <tr>
											  <td>
                                                <asp:Image ID="imgAss" runat="server" Visible="False" ImageUrl="~/Img_Sistema/Ass_Termo/Fortlev-rodape.png" />
                                                <asp:Label ID="lblLinha_Ass1" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                            </td>
											
                                        </tr>
										</table>
<table style="visibility:hidden";>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto9" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto10" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto11" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto12" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto13" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto14" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto15" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto16" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto17" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto18" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto19" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto22" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto23" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lbl_txtTexto24" runat="server" Font-Names="Arial" Font-Size="18pt"></asp:Label>
                                </td>
                            </tr>
</table>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
