<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="IControlIT._Default" EnableEventValidation="true" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>K2A - IControlIT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />

    <link rel="stylesheet" href="CSSConfigObj.CSS" />
    <link rel="stylesheet" href="CSSEstruturalMaster.css" />
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <link rel="Shortcut Icon" href="Img_Sistema/logo.ico" />

    <script src='JScript.js' type="text/javascript"></script>
    <script src="Scripts/jquery-3.5.1.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>

    <%-- trava o zoom quando visualizado no mobile e almenta a scala dos objetos na tela deixando responsivo --%>
    <meta name="viewport" content="width=device-width, initial-scale=0.7, maximum-scale=0.7, user-scalable=no" />

    <script type="text/javascript">

        var submit = 0;
        var dataInicial = new Date();
        function CheckDouble() {
            if (++submit > 1) {
                submit = 0;
                var dataFinal = new Date();
                var segundos = (dataFinal - dataInicial) / 1000;
                if (segundos < 2) {
                    dataInicial = new Date();
                    alert('Processando o login. Aguarde alguns segundos...');
                    return false;
                }
            }
        }

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
        function Modal(idModal) {
            $(idModal).modal();
        }
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

        .btnLinkSite {
            font-family: 'Microsoft JhengHei';
            color: #fffc00;
            font-size: 16pt;
            background-color: transparent;
            height: 35px;
            text-decoration: underline;
        }

            .btnLinkSite:hover {
                font-family: 'Microsoft JhengHei';
                color: #56ab2f;
                font-size: 16pt;
                background-color: transparent;
                height: 35px;
                text-decoration: underline;
            }
    </style>
</head>

<body runat="server">
    <form id="form1" runat="server" defaultbutton="btLogin">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>

                <div style="width: 100%; height: 100vh; background-size: cover; position: absolute; position: center; background-image: linear-gradient(to top, rgba(116,116,191,1), rgba(52,138,199,1))">

                    <!-- modal Termo Responsabilidade indisponivel -->
                    <!-- K2AIcontrolIt - 24 -->
                    <div>
                        <div class="modal fade" id="ModalTermoRespon" data-keyboard="false" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button id="Button4" runat="server" type="button" class="close" data-dismiss="modal" causesvalidation="False">&times;</button>
                                    </div>
                                    <div class="modal-body">
                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label CssClass="configlabel" ID="Label1" runat="server" ForeColor="#000000" Style="float: none" Text="Termo não disponível / Token inválido." Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal" causesvalidation="False">Fechar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- modal Senha Invalida -->
                    <div>
                        <div class="modal fade" id="myModal" data-keyboard="false" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button id="bt_close_popup" runat="server" type="button" class="close" data-dismiss="modal" causesvalidation="False">&times;</button>
                                    </div>
                                    <div class="modal-body">
                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label CssClass="configlabel" ID="lbl_Msg" runat="server" ForeColor="#000000" Style="float: none" Text="Usuário ou Senha inválido." Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal" causesvalidation="False">Fechar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--autenticacao facebook-->
                    <div>
                        <div class="modal fade" id="myModalSubFacebook" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button id="Button1" runat="server" type="button" class="close" data-dismiss="modal" causesvalidation="False">&times;</button>
                                        <h4 class="modal-title">Autenticação pelo Facebook</h4>
                                    </div>
                                    <div class="modal-body">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr style="height: 35px">
                                                            <td>
                                                                <asp:Label ID="lblPrimeiraPasso" runat="server" CssClass="configlabel" Style="float: left;" Font-Bold="True">1º - Passo</asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 179px;">
                                                                <asp:Label ID="lblEmailCorporativo" runat="server" CssClass="configlabel" Style="float: left;">Digite o seu e-mail corporativo</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEmailCorporativo" runat="server" CssClass="configtext" MaxLength="50" Width="320px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btEmailCorporativo" class="btn btn-success" Width="80px" runat="server" Text="Enviar" CausesValidation="False" />
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 35px">
                                                            <td>
                                                                <asp:Label ID="lblSegundoPasso" runat="server" CssClass="configlabel" Style="float: left;" Font-Bold="True" Visible="False">2º - Passo</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTexto_Email" runat="server" CssClass="configlabel" Style="float: left;" Visible="False"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 179px;">
                                                                <asp:Label ID="lblChaveSeguranca" runat="server" CssClass="configlabel" Style="float: left;" Visible="False">Digite a chave de segurança</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtChaveSeguranca" runat="server" CssClass="configtext" MaxLength="50" Width="320px" AutoPostBack="True" Visible="False"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btChaveSeguranca" class="btn btn-success" Width="80px" runat="server" Text="Confirmar" Visible="false" CausesValidation="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" style="width: 80px" data-dismiss="modal" causesvalidation="False">Fechar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--enviar senha por email-->
                    <div>
                        <div class="modal fade" id="myModalEnviaSenhaEmail" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button id="Button2" runat="server" type="button" class="close" style="position: absolute; right: 15px" data-dismiss="modal" causesvalidation="False">&times;</button>
                                        <h4 class="modal-title">Enviar Senha por E-mail</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblTextoPasso1EnviaSenha" runat="server" CssClass="configlabel" Style="float: left;" Font-Bold="True">1º - Passo</asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblMsgEnvioEmail" runat="server" CssClass="configlabel" Style="float: left;" ForeColor="#FF6600"></asp:Label>
                                                <asp:Label ID="lblTextoCoporativo" runat="server" Style="width: 100%" CssClass="configlabel">Digite o seu e-mail corporativo</asp:Label>
                                                <asp:TextBox ID="txtEmailEnvio" runat="server" CssClass="configtext" MaxLength="50" Width="100%" BorderStyle="Solid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <div class="row">
                                            <div class="col-md-12 left-right">
                                                <asp:Button ID="btEmailEnvio" class="btn btn-success" runat="server" Text="Enviar" CausesValidation="False" />
                                                <button type="button" class="btn btn-default" data-dismiss="modal" style="float: right; margin-left: 5px; margin-right: 5px" causesvalidation="False">Fechar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--termo de uso-->
                    <div>
                        <div class="modal fade" id="myModalSubTermo" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button id="Button3" runat="server" type="button" class="close" style="position: absolute; right: 15px" data-dismiss="modal" causesvalidation="False">&times;</button>
                                        <h4 class="modal-title">Termo de Uso de Ativo</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblTextoNome" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblTexto1" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                                <div id="DivAtivo" runat="server" style="left: 5px; overflow: auto; width: 100%; top: 103px; height: 84px" title=" ">
                                                    <asp:DataGrid ID="dtgAtivo" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                                        EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                        HorizontalAlign="Left" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

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
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblTexto2" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblTexto3" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblTexto4" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblTexto5" runat="server" CssClass="configlabel" Style="float: left;" Font-Bold="True" Font-Italic="True" Font-Underline="False"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Button ID="btAcordo" class="btn btn-success" runat="server" Text="De Acordo" Style="float: right" CausesValidation="False" />
                                                <button type="button" class="btn btn-default" data-dismiss="modal" causesvalidation="False">Fechar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--<div style="width:100%; height:60px; position:fixed; top:0px; background-image:linear-gradient(to bottom, rgba(0,0,0,0.8), rgba(0,0,0,0)">
                    <div class="container">
                        <!--Cabecario-->
                        <table style="width: 100%; height:60px;" >
                            <tr>
                                <td valign="middle" align="center" style="width: 26px;">
                                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Img_Sistema/Default/Logo_Menu_IControlIT_Branco.png" Height="40px" ToolTip="Menu" CausesValidation="False" />                        
                                </td>

                                <!--meio-->
                                <td style="width: 80px;"></td>
                       
                                <td style="width: 100%; text-align: center;">
                                    <asp:Label ID="lblTitulo" runat="server" Font-Names="Microsoft JhengHei Light" Font-Size="18pt" ForeColor="#FFFFFF" style="float:none" ></asp:Label>
                                </td>
                        
                                <td style="width: 80px;"></td>
                                <!--meio-->                                              

                                <td style="width: 35px;">
                                    <asp:Image ID="Image2" runat="server" Width="40px" ImageUrl="~/Img_Sistema/Geral/ic_android_branco.png" />
                                </td>

                                <td style="width: 35px;">
                                    <asp:Image ID="Image3" runat="server" Width="40px" ImageUrl="~/Img_Sistema/Geral/ic_apple_branco.png" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>--%>

                    <div id="DivHome" runat="server" style="width: 100%; height: 100vh; overflow: auto;">

                        <table runat="server" style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                            <tr runat="server" align="center">
                                <td runat="server" valign="middle">
                                    <div style="width: 500px; background-color: #FFFFFF; padding: 30px; border-radius: 10px; box-shadow: rgba(0,0,0,0.2) 0px 4px 8px 2px">

                                        <table style="width: 100%" runat="server" cellpadding="0" cellspacing="0">

                                            <!--Titulo-->
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center; height: 60px">
                                                                <asp:Label ID="lblAcesso" runat="server" Font-Names="Microsoft JhengHei Light" Font-Size="32pt" ForeColor="#979797" Text="Login" Style="float: none;"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <!--Caixa de texto-->
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <!--login-->
                                                            <td runat="server" align="center" style="width: 100%">
                                                                <table style="width: 100%;" cellpadding="0px" cellspacing="0px">
                                                                    <tr>
                                                                        <td runat="server" style="width: 100%; height: 42px;" align="center">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsuario" ErrorMessage="*" Height="16px" SetFocusOnError="True" Style="font-size: 10pt; z-index: 132; color: red; font-family: Arial; width: 10px; float: Left;"></asp:RequiredFieldValidator>
                                                                            <asp:TextBox ID="txtUsuario" placeholder="Usuário" runat="server" CssClass="textBoxLogin" Font-Names="Calibri Light" Font-Size="Larger" ForeColor="#979797" Height="45px" MaxLength="50" TabIndex="1" Width="100%" ToolTip="Usuário" BackColor="Transparent" Font-Bold="False" Font-Strikeout="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    
<%--                                                                        <td runat="server" style="width: 100%; height: 42px;" align="center">
                                                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtUsuario" ErrorMessage="*" Height="16px" SetFocusOnError="True" Style="font-size: 10pt; z-index: 132; color: red; font-family: Arial; width: 10px; float: Left;" Visible="false"></asp:RequiredFieldValidator>
                                                                            <asp:TextBox ID="txtEmail" placeholder="E-Mail" runat="server" CssClass="textBoxLogin" Font-Names="Calibri Light" Font-Size="Larger" ForeColor="#979797" Height="45px" MaxLength="50" TabIndex="1" Width="100%" ToolTip="E-mail do Usuário" BackColor="Transparent" Font-Bold="False" Font-Strikeout="False" Visible="false"></asp:TextBox>
                                                                        </td>--%>
                                                                    
                                                                    <tr>
                                                                        <td style="height: 10px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100%; height: 44px;" align="center">
                                                                            <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha" ErrorMessage="*" Height="16px" SetFocusOnError="True" Style="font-size: 10pt; z-index: 132; color: red; font-family: Arial; width: 10px; float: left;"></asp:RequiredFieldValidator>
                                                                            <asp:TextBox ID="txtSenha" placeholder="Senha" runat="server" CssClass="textBoxLogin" Font-Names="Calibri Light" Font-Size="Larger" ForeColor="#979797" Height="45px" MaxLength="50" TabIndex="2" TextMode="Password" Width="100%" ToolTip="Senha" BackColor="Transparent"></asp:TextBox>
                                                                            <%-- Bloco para fornecer o email para enviar o token --%>
                                                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="*" Height="16px" SetFocusOnError="True" Style="font-size: 10pt; z-index: 132; color: red; font-family: Arial; width: 10px; float: Left;" Visible="false"></asp:RequiredFieldValidator>
                                                                            <asp:TextBox ID="txtEmail" placeholder="E-Mail" runat="server" CssClass="textBoxLogin" Font-Names="Calibri Light" Font-Size="Larger" ForeColor="#979797" Height="45px" MaxLength="50" TabIndex="1" Width="100%" ToolTip="E-mail do Usuário" BackColor="Transparent" Font-Bold="False" Font-Strikeout="False" Visible="false"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <%--<tr>
                                                                        <td style="height: 10px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="msgSenhaForte1" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">* Este é seu primeiro acesso. Forneça uma senha que atenda os requisitos:  </asp:Label></br></br>
                                                                            <asp:Label ID="msgSenhaForte2" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">** Ter pelo menos 12 caracteres.</asp:Label></br>
                                                                            <asp:Label ID="msgSenhaForte3" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">** Ter um número (0-9).</asp:Label>                                                                        
                                                                            <asp:Label ID="msgSenhaForte4" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">** Ter um caractere especial (@ # $ etc.)</asp:Label></br>
                                                                            <asp:Label ID="msgSenhaForte5" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">** Ter pelo menos uma letra maiúscula (A-Z). </br>** Ter uma letra minúscula (a-z).</asp:Label></br>
                                                                            <asp:Label ID="msgSenhaForteDadosObrigatorios" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#FF4500" Visible="False">** Preencha os campos de [Atualizar Senha] e [Confirmação]</asp:Label></br>
                                                                            <asp:Label ID="msgSenhaForteErroRequisitos" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#FF4500" Visible="False">** A senha informada no campo [Atualizar Senha] e [Confirmação] não atende os requisitos mínimos.</asp:Label>
                                                                            <asp:Label ID="msgSenhaForteJaCadastrada" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#FF4500" Visible="False">** A nova senha deve ser diferente da senha atual.</asp:Label>
                                                                         </td>                                                                        
                                                                    </tr>
                                                                    <tr align="right">
                                                                        <td style="width: 100%; height: 0px;" align="center">
                                                                            <asp:RequiredFieldValidator ID="rfvSenhaForte" runat="server" ControlToValidate="txtSenhaForte" ErrorMessage="*" Height="16px" SetFocusOnError="True" Style="font-size: 10pt; z-index: 132; color: red; font-family: Arial; width: 10px; float: left;" Visible="false"></asp:RequiredFieldValidator>
                                                                            <asp:TextBox ID="txtSenhaForte" placeholder="Atualizar Senha" runat="server" CssClass="textBoxLogin" Font-Names="Calibri Light" Font-Size="Larger" ForeColor="#979797" Height="45px" MaxLength="50" TabIndex="2" TextMode="Password" Width="100%" ToolTip="Senha" BackColor="Transparent" Visible="false" autocomplete="off"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px"></td>
                                                                    </tr>
                                                                    <tr align="right">
                                                                        <td style="width: 100%; height: 0px;" align="center">
                                                                            <asp:RequiredFieldValidator ID="rfvConfirmaSenhaForte" runat="server" ControlToValidate="txtConfirmaSenhaForte" ErrorMessage="*" Height="16px" SetFocusOnError="True" Style="font-size: 10pt; z-index: 132; color: red; font-family: Arial; width: 10px; float: left;" Visible="false"></asp:RequiredFieldValidator>
                                                                            <asp:TextBox ID="txtConfirmaSenhaForte" placeholder="Confirmar Senha" runat="server" CssClass="textBoxLogin" Font-Names="Calibri Light" Font-Size="Larger" ForeColor="#979797" Height="45px" MaxLength="50" TabIndex="2" TextMode="Password" Width="100%" ToolTip="Confirmar Senha" BackColor="Transparent" Visible="false" autocomplete="off"></asp:TextBox>
                                                                        </td>
                                                                    </tr>--%>
                                                                    <tr align="right">
                                                                        <td style="width: 100%; height: 0px;" align="center">
                                                                            <asp:RequiredFieldValidator ID="rfvNovaSenha" runat="server" ControlToValidate="txtNovaSenha" ErrorMessage="*" Height="16px" SetFocusOnError="True" Style="font-size: 10pt; z-index: 132; color: red; font-family: Arial; width: 10px; float: left;" Visible="false"></asp:RequiredFieldValidator>
                                                                            <asp:TextBox ID="txtNovaSenha" placeholder="Confirmar Senha" runat="server" CssClass="textBoxLogin" Font-Names="Calibri Light" Font-Size="Larger" ForeColor="#979797" Height="45px" MaxLength="50" TabIndex="2" TextMode="Password" Width="100%" ToolTip="Nova Senha" BackColor="Transparent" Visible="false"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="padding-left: 10px">
                                                                                        <asp:Button ID="btTrocarSenha" runat="server" BackColor="Transparent" CausesValidation="False" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#008bed" Height="20px" Style="float: left; cursor: pointer" Text="Trocar Senha" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <%--<asp:Button ID="btEsqueciSenha" runat="server" BackColor="Transparent" CausesValidation="False" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#008bed" Height="20px" Style="float: right; cursor: pointer" Text="Esqueci minha Senha" Width="150px" />--%>
                                                                                    </td>
                                                                                    <td></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 15px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="btLogin" class="btn btn-primary" Width="100%" Style="border-radius: 20px 20px;" runat="server" Text="Conectar" CausesValidation="False" data-keyboard="false" Height="45px" Enabled="True" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lblMsgSenha" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">* É necessário trocar a senha padrão</asp:Label>
                                                                            <asp:Label ID="msgSenhaForte1" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">
                                                                                        </br></br></br>
                                                                                          Forneça uma senha que atenda os requisitos:</br>
                                                                                        ** Ter pelo menos 12 caracteres.</br>
                                                                                        ** Ter um número (0-9).</br> 
                                                                                        ** Ter um caractere especial (@ # $ etc.)</br>
                                                                                        ** Ter pelo menos uma letra maiúscula (A-Z). </br>
                                                                                        ** Ter uma letra minúscula (a-z).</br></br>
                                                                            </asp:Label>
                                                                            <%--<asp:Label ID="msgSenhaForte2" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">** Ter pelo menos 12 caracteres.</asp:Label></br>
                                                                            <asp:Label ID="msgSenhaForte3" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">** Ter um número (0-9).</asp:Label>                                                                        
                                                                            <asp:Label ID="msgSenhaForte4" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">** Ter um caractere especial (@ # $ etc.)</asp:Label></br>
                                                                            <asp:Label ID="msgSenhaForte5" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">** Ter pelo menos uma letra maiúscula (A-Z). </br>** Ter uma letra minúscula (a-z).</asp:Label></br>--%>
                                                                            <asp:Label ID="msgSenhaForteDadosObrigatorios" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#FF4500" Visible="False">** Preencha os campos de [Atualizar Senha] e [Confirmação]</br></asp:Label>
                                                                            <asp:Label ID="msgSenhaForteErroRequisitos" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#FF4500" Visible="False">** A senha informada não atende os requisitos mínimos.</asp:Label>
                                                                            <asp:Label ID="msgSenhaForteJaCadastrada" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#FF4500" Visible="False">** A nova senha deve ser diferente da senha atual.</asp:Label>
                                                                            <asp:Label ID="lblMsgTokenSenha" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="True">* Sua solicitação de troca de Senha foi encaminhada por E-Mail</asp:Label>
                                                                            <asp:Label ID="lblMsgTokenInvalido" runat="server" Font-Names="Calibri Light" Font-Size="Medium" ForeColor="#979797" Visible="False">*Seu Token não foi identificado em nosso cadastro, favor solicite novamente a troca de senha.</asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="height: 30px"></td>
                                            </tr>

                                            <!--Rodape-->
                                            <%--<tr>
                                                <td align="center">
                                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <asp:Label ID="lblMensagem" runat="server" CssClass="configlabel" ForeColor="#979797" Text="Desenvolvendo Soluções:" Font-Names="Microsoft JhengHei Light" Font-Size="18pt" Style="float: none"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <asp:Image ID="imgLogoIControlIT" runat="server" Height="80px" Width="80px" Style="padding: 4px" ImageAlign="Middle" ImageUrl="~/Img_Sistema/Master/Mn_Cadastro.png" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <asp:Label ID="Label5" runat="server" ForeColor="#979797" Text="A IControlIT pode construir as melhores ferramentas, combinando as maiores tecnologia do mercado." Font-Names="Microsoft JhengHei" Font-Size="12pt" Width="100%" Style="line-height: 20px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <asp:Label ID="Label1" runat="server" ForeColor="#979797" Text="Saiba mais em:" Font-Names="Microsoft JhengHei Light" Font-Size="11pt"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <table>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Button ID="btnSite" runat="server" CssClass="btnLinkSite" Font-Names="Microsoft JhengHei Light" ForeColor="#008bed" Text="IControlIT.com.br" CausesValidation="False" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
