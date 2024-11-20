<%@ Page Language="vb" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="CockPit_Menu.aspx.vb" Inherits="IControlIT.CockPit_Menu" EnableEventValidation="true" %>

<%@ MasterType VirtualPath="~/Principal.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content runat="server" ID="Head" ContentPlaceHolderID="head">

    <style type="text/css">
        h1 {
            font-size: 35px;
            text-align: center;
            color: #af1e2d;
            font-family: 'Microsoft JhengHei Light';
            background: -webkit-linear-gradient(top, #e24f60, #af1e2d);
            background: linear-gradient(top, #e24f60, #af1e2d);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            white-space: nowrap
        }

        .bg-panel {
            z-index: 59;
            position: fixed;
            top: 0px;
            left: 0px;
            width: 100%;
            height: 100%;
            background-image: linear-gradient(to bottom, #eeedf0, #dcd8e3);
        }

        .btMenu {
            position: absolute;
            left: 50%;
            top: 50%;
            transform: translate(-50%, -50%);
        }

        .liMenu {
            display: inline-block;
            vertical-align: middle;
            padding: 2px;
        }

        .btn-ativos {
            position: relative;
            text-align: center;
            width: 100px;
            height: 100px;
            background-color: #FFFFFF;
            box-shadow: rgba(0,0,0,0.4) 0px 2px 10px 0px;
            -webkit-transition: 500ms;
            -moz-transition: 500ms;
            -ms-transition: 500ms;
            -o-transition: 500ms;
            transition: 500ms;
        }

            .btn-ativos:hover {
                box-shadow: rgba(0,0,0,0.4) 0px 16px 36px 0px;
                animation-name: cardUp;
                animation-fill-mode: forwards;
                -webkit-animation-duration: 500ms;
                -moz-animation-duration: 500ms;
                -ms-animation-duration: 500ms;
                -o-animation-duration: 500ms;
                animation-duration: 500ms;
            }

        .tblCabecalho {
            width: 100%;
            background-color: transparent;
            padding-left: 50px;
            padding-right: 50px;
        }

        #btFerramenta:hover {
            background-color: #4E4D4B;
        }

        /*animacao dos botoes*/
        @-webkit-keyframes cardUp {
            from {
                bottom: 0px;
            }

            to {
                bottom: 12px;
            }
        }

        @keyframes cardUp {
            from {
                bottom: 0px;
            }

            to {
                bottom: 12px;
            }
        }

        *:focus {
            outline: none;
        }

        .btn-Selected:hover {
            background-color: #EEEEEE;
        }

        .btn-Selected:active {
            background-color: #B2DFDB;
        }

        .btn-menu {
            display: inline-block;
            vertical-align: middle;
            -webkit-transform: perspective(1px) translateZ(0);
            transform: perspective(1px) translateZ(0);
            box-shadow: 0 0 1px rgba(0, 0, 0, 0);
            -webkit-transition-duration: 0.3s;
            transition-duration: 0.3s;
            -webkit-transition-property: transform;
            transition-property: transform;
            -webkit-transition-timing-function: ease-out;
            transition-timing-function: ease-out;
        }

            .btn-menu:hover, .btn-menu:focus, .btn-menu:active {
                -webkit-transform: translateY(-8px);
                transform: translateY(-8px);
            }

                .btn-menu:hover .speech-bubble {
                    visibility: visible;
                }

        /* Float Shadow */
        .hvr-float-shadow {
            display: inline-block;
            vertical-align: middle;
            -webkit-transform: perspective(1px) translateZ(0);
            transform: perspective(1px) translateZ(0);
            box-shadow: 0 0 1px rgba(0, 0, 0, 0);
            position: relative;
            -webkit-transition-duration: 0.3s;
            transition-duration: 0.3s;
            -webkit-transition-property: transform;
            transition-property: transform;
        }

            .hvr-float-shadow:before {
                pointer-events: none;
                position: absolute;
                z-index: -1;
                content: '';
                /*top: 100%;*/
                bottom: -6px;
                left: 10%;
                height: 7px;
                width: 90%;
                opacity: 1;
                background: -webkit-radial-gradient(center, ellipse, rgba(0, 0, 0, 0.35) 0%, rgba(0, 0, 0, 0) 80%);
                background: radial-gradient(ellipse at center, rgba(0, 0, 0, 0.35) 0%, rgba(0, 0, 0, 0) 80%);
                /* W3C */
                -webkit-transition-duration: 0.3s;
                transition-duration: 0.3s;
                -webkit-transition-property: transform, opacity, height;
                transition-property: transform, opacity, height;
            }

            .hvr-float-shadow:hover, .hvr-float-shadow:focus, .hvr-float-shadow:active {
                -webkit-transform: translateY(-5px);
                transform: translateY(-5px);
                /* move the element up by 5px */
            }

                .hvr-float-shadow:hover:before, .hvr-float-shadow:focus:before, .hvr-float-shadow:active:before {
                    height: 12px;
                    opacity: 1;
                    -webkit-transform: translateY(7px);
                    transform: translateY(7px);
                    /* move the element down by 5px (it will stay in place because it's attached to the element that also moves up 5px) */
                }

                .hvr-float-shadow:hover .speech-bubble {
                    visibility: visible;
                }

        /*tooltip dos botões do menu*/
        .speech-bubble {
            position: absolute;
            top: -35px;
            background: #4ca2cd;
            border-radius: 4px;
            padding: 7px 15px 7px 15px;
            visibility: hidden;
        }

            .speech-bubble:after {
                content: '';
                position: absolute;
                bottom: 0;
                left: 50%;
                width: 0;
                height: 0;
                border: 8px solid transparent;
                border-top-color: #4ca2cd;
                border-bottom: 0px;
                margin-left: -8px;
                margin-bottom: -8px;
            }

        .divCard {
            width: 550px;
            background-color: #FFFFFF;
            border-radius: 6px;
            box-shadow: rgba(0, 0, 0, 0.2) 0 2px 8px 2px;
            padding: 15px;
        }

        .imgLoading {
            width: 300px;
        }

        /*/ Responsivo para tela mobile /*/
        @media only screen and (max-width: 600px) {

            .tblCabecalho {
                width: 100%;
                background-color: transparent;
                padding-left: 15px;
                padding-right: 15px;
            }

            .liMenu {
                position: relative;
                display: inline-block;
                vertical-align: middle;
                padding: 2px 0px 2px 0px;
            }

            .btn-ativos {
                position: relative;
                text-align: center;
                width: 100px;
                height: 100px;
                background-color: #FFFFFF;
                box-shadow: rgba(0,0,0,0.4) 0px 2px 10px 0px;
                -webkit-transition: 500ms;
                -moz-transition: 500ms;
                -ms-transition: 500ms;
                -o-transition: 500ms;
                transition: 500ms;
            }

                .btn-ativos:hover {
                    width: 100px;
                    height: 100px;
                    box-shadow: rgba(0,0,0,0.4) 0px 16px 36px 0px;
                    animation-name: cardUp;
                    animation-fill-mode: forwards;
                    -webkit-animation-duration: 500ms;
                    -moz-animation-duration: 500ms;
                    -ms-animation-duration: 500ms;
                    -o-animation-duration: 500ms;
                    animation-duration: 500ms;
                }

            .divCard {
                width: 100%;
                background-color: #FFFFFF;
                border-radius: 6px;
                box-shadow: rgba(0, 0, 0, 0.2) 0 2px 8px 2px;
                padding: 15px;
            }

            /*animacao dos botoes*/
            @-webkit-keyframes cardUp {
                from {
                    bottom: 0px;
                }

                to {
                    bottom: 2px;
                }
            }

            @keyframes cardUp {
                from {
                    bottom: 0px;
                }

                to {
                    bottom: 2px;
                }
            }

            .imgLoading {
                width: 100%;
            }
        }
    </style>

</asp:Content>

<asp:Content runat="Server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">

    <div>

        <!-- Navbar -->
        <div style="width: 100%; background-color: #ffffff; box-shadow: rgba(0,0,0,0.2) 0px 4px 16px 2px; position: fixed; top: 0px; left: 0px; z-index: 1">
            <table style="width: calc(100% - 60px); height: 80px;">
                <tr>
                    <td class="divEspacoSidebar"></td>
                    <td>
                        <div style="padding-left: 15px;">
                            <asp:Image ID="imgLogoCliente" runat="server" Style="max-width: 350px; max-height: 50px;" />
                        </div>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td class="divEspacoSidebar"></td>
                    <td style="padding: 0px 15px 15px 15px">
                        <div style="height: 40px; padding: 2px 10px 2px 20px; background-color: #EEEEEE; border: 1px solid #E0E0E0; border-radius: 25px">
                            <div class="input-group no-border">
                                <asp:TextBox ID="txtPesquisaContato" placeholder="Agenda Corporativa" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:LinkButton ID="btPesquisar" runat="server" CssClass="nav-link" OnClick="btPesquisar_Click"><i class="material-icons">search</i></asp:LinkButton>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <!--lista telefonica ***************************************************************************** -->
                        <div class="row">
                            <asp:Panel ID="pnlListaTelefonica" runat="server" Style="width: 100%; z-index: 5; margin-bottom: 15px" Visible="False">
                                <table style="width: 100%;">
                                    <tr>
                                        <td runat="server" align="center" style="padding: 0px 35px 0px 35px">
                                            <div id="DivAtivo" runat="server" style="position: relative; overflow: auto; width: 100%; max-height: 450px; padding: 5px; background-color: #FFFFFF; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px; border-radius: 4px">
                                                <asp:LinkButton ID="btnFechaPesquisa" runat="server" Style="position: absolute; right: 15px; top: 15px" OnClick="btnFechaPesquisa_Click">
                                                                    <i class="fas fa-times" style="color: #4FC3F7"></i>
                                                </asp:LinkButton>
                                                <asp:DataGrid ID="dtgListaTelefonica" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" BackColor="#FFFFFF"
                                                    EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                    HorizontalAlign="Left" Font-Size="10pt" Width="100%" ForeColor="#818181" AllowPaging="false" BorderColor="#f7f0f7">
                                                    <PagerStyle Mode="NumericPages" />
                                                    <AlternatingItemStyle Font-Bold="False" />
                                                    <ItemStyle ForeColor="Black" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="Nm_Consumidor" HeaderText="Nome">
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Linha">
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Nm_Ativo_Tipo" HeaderText="Tipo">
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Nm_Conglomerado" HeaderText="Operadora">
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                    </Columns>

                                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Segoe UI" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                                    <AlternatingItemStyle BackColor="#e0e0e0" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />

                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
            <div style="position: fixed; top: 25px; right: 0px">
                <button class="navbar-toggler" type="button" data-toggle="collapse" aria-controls="navigation-index" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="navbar-toggler-icon icon-bar" style="margin-top: 4px; margin-right: 4px"></span>
                    <span class="navbar-toggler-icon icon-bar" style="margin-top: 4px; margin-right: 4px"></span>
                    <span class="navbar-toggler-icon icon-bar" style="margin-top: 4px; margin-right: 4px"></span>
                </button>
            </div>
        </div>
        <!-- End Navbar -->

        <!-- Content -->
        <div class="content">
            <div class="container-fluid">
                <!-- your content here -->

                <div style="height: 40px"></div>

                <!--minhas areas ***************************************************************************** -->
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlMinhasAreas" runat="server" Style="z-index: 60; width: 100%; background-color: transparent;" Visible="true">
                            <table style="width: 100%; height: 100%;">
                                <tr>
                                    <td>
                                        <div>&nbsp;</div>
                                    </td>
                                </tr>
                                <tr>
                                    <!--Cabecario-->
                                    <td style="text-align: center">
                                        <table style="width: 100%; text-align: center">
                                            <tr>
                                                <td>
                                                    <div style="width: 100%; height: 2px; background-color: #818181"></div>
                                                </td>
                                                <td style="width: 200px">
                                                    <asp:Label ID="lblDMinhasAreas" runat="server" ForeColor="#818181" CssClass="configlabel" Font-Names="Segoe UI Semibold" Font-Size="18pt" Style="float: none;" Text="Visão do Gestor"></asp:Label>
                                                </td>
                                                <td>
                                                    <div style="width: 100%; height: 2px; background-color: #818181"></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table>
                                            <tr>
                                                <td style="height: 30px"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!--Corpo-->
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td align="center">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td align="center" runat="server" id="tdTM">
                                                                            <div id="Div1" runat="server" class="btn-ativos" style="width: 120px; height: 120px">
                                                                                <asp:LinkButton ID="btTelefoniaMovel" runat="server" Enabled="false" CausesValidation="false" CssClass="btMenu" Style="width: 100%">
                                                                                    <i id="i1" runat="server" class="fas fa-mobile-alt" style="font-size: 28pt; color: #4FC3F7"></i>
                                                                                    <br />
                                                                                    <asp:Label ID="lblDMovel" runat="server" Style="line-height: 30px" Width="100%" ForeColor="#4FC3F7" Font-Names="Segoe UI Semibold" Font-Size="10pt" Text="Telefonia Móvel"></asp:Label>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </td>

                                                                        <td style="width: 10px"></td>

                                                                        <td runat="server" id="tdTF">
                                                                            <div id="Div2" runat="server" class="btn-ativos" style="width: 120px; height: 120px">
                                                                                <asp:LinkButton ID="btTelefoniaFixa" runat="server" Enabled="false" CausesValidation="false" CssClass="btMenu" Style="width: 100%">
                                                                                    <i id="i2" runat="server" class="fas fa-phone" style="font-size: 28pt; color: #4FC3F7"></i>
                                                                                    <br />
                                                                                    <asp:Label ID="lblDFixa" runat="server" Style="line-height: 30px" Width="100%" ForeColor="#4FC3F7" Font-Names="Segoe UI Semibold" Font-Size="10pt" Text="Telefonia Fixa"></asp:Label>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </td>

                                                                        <td style="width: 10px"></td>

                                                                        <td runat="server" id="tdIM">
                                                                            <div id="Div3" runat="server" class="btn-ativos" style="width: 120px; height: 120px; opacity: 0,2;">
                                                                                <asp:LinkButton ID="btImpressao" runat="server" Enabled="false" CausesValidation="false" CssClass="btMenu" Style="width: 100%">
                                                                                    <i id="i3" runat="server" class="fas fa-print" style="font-size: 28pt; color: #4FC3F7"></i>
                                                                                    <br />
                                                                                    <asp:Label ID="lblDImpressao" runat="server" Style="line-height: 30px" Width="100%" ForeColor="#4FC3F7" Font-Names="Segoe UI Semibold" Font-Size="10pt" Text="Impressão"></asp:Label>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </td>
                                                                        <td>&nbsp;</td>

                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td runat="server" id="tdCL">
                                                                            <div id="Div4" runat="server" class="btn-ativos" style="width: 120px; height: 120px">
                                                                                <asp:LinkButton ID="btCloud" runat="server" Enabled="false" CausesValidation="false" CssClass="btMenu" Style="width: 100%">
                                                                                    <i id="i4" runat="server" class="fas fa-cloud" style="font-size: 28pt; color: #4FC3F7"></i>
                                                                                    <br />
                                                                                    <asp:Label ID="lblDCloud" runat="server" Style="line-height: 30px" Width="100%" ForeColor="#4FC3F7" Font-Names="Segoe UI Semibold" Font-Size="10pt" Text="Cloud"></asp:Label>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </td>
                                                                        <td style="width: 10px"></td>
                                                                        <td runat="server" id="tdLK">
                                                                            <div id="Div5" runat="server" class="btn-ativos" style="width: 120px; height: 120px">
                                                                                <asp:LinkButton ID="btLink" runat="server" Enabled="false" CausesValidation="false" CssClass="btMenu" Style="width: 100%">
                                                                                    <i id="i5" runat="server" class="fas fa-wifi" style="font-size: 28pt; color: #4FC3F7"></i>
                                                                                    <br />
                                                                                    <asp:Label ID="lblDLink" runat="server" Style="line-height: 30px" Width="100%" ForeColor="#4FC3F7" Font-Names="Segoe UI Semibold" Font-Size="10pt" Text="Link Dados"></asp:Label>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </td>
                                                                        <td style="width: 10px"></td>
                                                                        <td runat="server" id="tdDK">
                                                                            <div id="Div6" runat="server" class="btn-ativos" style="width: 120px; height: 120px">
                                                                                <asp:LinkButton ID="btDeskTop" runat="server" Enabled="false" CausesValidation="false" CssClass="btMenu" Style="width: 100%">
                                                                                    <i id="i6" runat="server" class="fas fa-hdd" style="font-size: 28pt; color: #4FC3F7"></i>
                                                                                    <br />
                                                                                    <asp:Label ID="lblDDesktop" runat="server" Style="line-height: 30px" Width="100%" ForeColor="#4FC3F7" Font-Names="Segoe UI Semibold" Font-Size="10pt" Text="Outros Ativos"></asp:Label>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <%--<tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td runat="server" id="tdDM">
                                                                                            <div id="Div7" runat="server" class="btn-ativos" style="width: 120px; height: 120px">
                                                                                                <asp:LinkButton ID="btDadosMaveis" runat="server" Enabled="false" CausesValidation="false" CssClass="btMenu" style="width: 100%">
                                                                                                    <i id="i7" runat="server" class="fas fa-server" style="font-size: 28pt; color: #4FC3F7"></i>
                                                                                                    <br />
                                                                                                    <asp:Label ID="lblDadosMoveis" runat="server" Style="line-height: 30px" Width="100%" ForeColor="#4FC3F7" Font-Names="Segoe UI Semibold" Font-Size="10pt" Text="Trafego de Dados"></asp:Label>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>--%>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" Style="color: #818181">
                                                                    <ProgressTemplate>
                                                                        <asp:Image ID="imgTempIndicadores" runat="server" CssClass="imgLoading" Height="64px" Width="64px" ImageUrl="~/Img_Sistema/loading_spinner.gif" />
                                                                        <br />
                                                                        Carregando...
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <!--Timer-->
                                            <tr style="height: 30px;">
                                                <td align="center">
                                                    <input runat="server" id="txtTime" name="txtTime" type="text" style="color: #000080; width: 210px; background-color: transparent" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>

                <!--minhas contas ***************************************************************************** -->
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlMeusAtivos" runat="server" Style="z-index: 60; width: 100%" Visible="false">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <div>&nbsp;</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 80%">
                                        <asp:Panel ID="pnlMenu" runat="server">
                                            <!--Cabecario-->
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <div style="width: 100%; height: 2px; background-color: #818181"></div>
                                                    </td>
                                                    <td style="width: 200px; text-align: center">
                                                        <asp:Label ID="lblDMeusAtivos" runat="server" ForeColor="#818181" CssClass="configlabel" Font-Size="18pt" Style="float: none;" Text="Minhas Contas" Font-Names="Segoe UI Semibold"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <div style="width: 100%; height: 2px; background-color: #818181"></div>
                                                    </td>
                                                </tr>
                                            </table>

                                            <!--Corpo-->
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <div id="DivCorpo" runat="server" style="overflow: auto">
                                                            <asp:DataGrid ID="dtgAtivo" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                                                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" GridLines="None" HorizontalAlign="Center">

                                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                <PagerStyle Mode="NumericPages" PageButtonCount="3" />
                                                                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" HorizontalAlign="Center" Font-Underline="False" ForeColor="Black" />

                                                                <Columns>
                                                                    <asp:TemplateColumn>
                                                                        <ItemTemplate>
                                                                            <div runat="server" id="divAtivo1" class="btn-ativos" style="width: 110px; height: 110px" visible='<%# Bind("Visible_1") %>'>
                                                                                <div id="divQtdConta" runat="server" visible="false" style="width: 32px; height: 32px; box-shadow: rgba(0,0,0,0.4) 0px 2px 4px 0px; background-color: orangered; border-radius: 0px 0px 0px 30px; position: absolute; right: 0px; top: 0px">
                                                                                    <table style="width: 100%; height: 100%">
                                                                                        <tr>
                                                                                            <td align="center" style="padding: 0px 0px 5px 5px">
                                                                                                <asp:Label ID="lblQtdConta" runat="server" Text="1" ForeColor="White" Font-Size="14pt" Font-Names="Segoe UI Semibold" Font-Bold="True"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                                <table style="width: 100%; height: 100%;">
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: center">
                                                                                            <asp:ImageButton ID="btAtivo1" runat="server" ImageUrl='<%# Bind("Imagem_1") %>' Visible='<%# Bind("Visible_1") %>' Height="46px" Width="46px" OnClick="btAtivo1_Click" ToolTip="Click para visualizar a conta" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 10px"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="vertical-align: top; text-align: center">
                                                                                            <asp:Button ID="lblTipo1" ForeColor="#818181" Font-Names="Segoe UI" Height="20px" Font-Size="11pt" BackColor="Transparent" Style="cursor: pointer; float: none" runat="server" Text='<%# Bind("Ativo_1") %>' Font-Underline="True" OnClick="lblTipo1_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                            <asp:HiddenField ID="hdvId_Ativo1" runat="server" Value='<%# Bind("Id_Ativo_1") %>' />
                                                                            <asp:HiddenField ID="hdvCota1" runat="server" Value='<%# Bind("Valor_Politica_1") %>' />
                                                                            <asp:HiddenField ID="hdvId_Ativo1_1" runat="server" Value='<%# Bind("Ativo_Grupo_1") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>

                                                                    <asp:TemplateColumn>
                                                                        <ItemTemplate>
                                                                            <div runat="server" id="divAtivo2" class="btn-ativos" style="width: 110px; height: 110px" visible='<%# Bind("Visible_2") %>'>
                                                                                <div id="divQtdConta2" runat="server" visible="false" style="width: 32px; height: 32px; background-color: orangered; border-radius: 0px 0px 0px 30px; position: absolute; right: 0px; top: 0px">
                                                                                    <table style="width: 100%; height: 100%">
                                                                                        <tr>
                                                                                            <td align="center" style="padding: 0px 0px 5px 5px">
                                                                                                <asp:Label ID="lblQtdConta2" runat="server" Text="1" ForeColor="White" Font-Size="14pt" Font-Names="Segoe UI Semibold" Font-Bold="True"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                                <table style="width: 100%; height: 100%;">
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: center">
                                                                                            <asp:ImageButton ID="btAtivo2" runat="server" ImageUrl='<%# Bind("Imagem_2") %>' Visible='<%# Bind("Visible_2") %>' Height="46px" Width="46px" OnClick="btAtivo2_Click" ToolTip="Click para visualizar a conta" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 10px"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="vertical-align: top; text-align: center">
                                                                                            <asp:Button ID="lblTipo2" ForeColor="#818181" Font-Names="Segoe UI" Height="20px" Font-Size="11pt" BackColor="Transparent" Style="cursor: pointer; float: none" runat="server" Text='<%# Bind("Ativo_2") %>' Font-Underline="True" OnClick="lblTipo2_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                            <asp:HiddenField ID="hdvId_Ativo2" runat="server" Value="<%# Bind('Id_Ativo_2') %>" />
                                                                            <asp:HiddenField ID="hdvCota2" runat="server" Value="<%# Bind('Valor_Politica_2') %>" />
                                                                            <asp:HiddenField ID="hdvId_Ativo1_2" runat="server" Value="<%# Bind('Ativo_Grupo_2') %>" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>

                                                                    <asp:TemplateColumn>
                                                                        <ItemTemplate>
                                                                            <div runat="server" id="divAtivo3" class="btn-ativos" style="width: 110px; height: 110px" visible="<%# Bind('Visible_3') %>">
                                                                                <div id="divQtdConta3" runat="server" visible="false" style="width: 32px; height: 32px; background-color: orangered; border-radius: 0px 0px 0px 30px; position: absolute; right: 0px; top: 0px">
                                                                                    <table style="width: 100%; height: 100%">
                                                                                        <tr>
                                                                                            <td align="center" style="padding: 0px 0px 5px 5px">
                                                                                                <asp:Label ID="lblQtdConta3" runat="server" Text="1" ForeColor="White" Font-Size="14pt" Font-Names="Segoe UI Semibold" Font-Bold="True"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                                <table style="width: 100%; height: 100%;">
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: center">
                                                                                            <asp:ImageButton ID="btAtivo3" runat="server" ImageUrl="<%# Bind('Imagem_3') %>" Visible='<%# Bind("Visible_3") %>' Height="46px" Width="46px" OnClick="btAtivo3_Click" ToolTip="Click para visualizar a conta" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 10px"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="vertical-align: top; text-align: center">
                                                                                            <asp:Button ID="lblTipo3" ForeColor="#818181" Font-Names="Segoe UI" Height="20px" Font-Size="11pt" BackColor="Transparent" Style="cursor: pointer; float: none" runat="server" Text="<%# Bind('Ativo_3') %>" Font-Underline="True" OnClick="lblTipo3_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                            <asp:HiddenField ID="hdvId_Ativo3" runat="server" Value="<%# Bind('Id_Ativo_3') %>" />
                                                                            <asp:HiddenField ID="hdvCota3" runat="server" Value="<%# Bind('Valor_Politica_3') %>" />
                                                                            <asp:HiddenField ID="hdvId_Ativo1_3" runat="server" Value="<%# Bind('Ativo_Grupo_3') %>" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                </Columns>
                                                            </asp:DataGrid>
                                                        </div>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>

                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <!--Cabecario-->
                                                    <td style="text-align: center">
                                                        <table style="width: 100%; text-align: center">
                                                            <tr>
                                                                <td>
                                                                    <div style="width: 100%; height: 2px; background-color: #818181"></div>
                                                                </td>
                                                                <td style="width: 200px">
                                                                    <asp:Label ID="Label1" runat="server" ForeColor="#818181" CssClass="configlabel" Font-Names="Segoe UI Semibold" Font-Size="18pt" Style="float: none;" Text="Minha Equipe"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <div style="width: 100%; height: 2px; background-color: #818181"></div>
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
                                                        <!--minha equipe -->
                                                        <div class="row">
                                                            <asp:Panel ID="Panel2" runat="server" Style="width: 100%; z-index: 5;" Visible="true">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td runat="server" align="center" style="padding: 0px 35px 0px 35px">
                                                                            <div id="Div7" runat="server" style="position: relative; overflow: auto; width: 100%; max-height: 450px; padding: 5px; background-color: #FFFFFF; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px; border-radius: 4px">
                                                                                <asp:DataGrid ID="dtgMinhaEquipe" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" BackColor="#FFFFFF"
                                                                                    EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                                                    HorizontalAlign="Left" Font-Size="10pt" Width="100%" ForeColor="#818181" AllowPaging="false" BorderColor="#f7f0f7">
                                                                                    <PagerStyle Mode="NumericPages" />
                                                                                    <AlternatingItemStyle Font-Bold="False" />
                                                                                    <ItemStyle ForeColor="Black" />
                                                                                    <Columns>
                                                                                        <asp:BoundColumn DataField="Nm_Consumidor" HeaderText="Nome">
                                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Linha">
                                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="Nm_Ativo_Tipo" HeaderText="Tipo">
                                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                                                        </asp:BoundColumn>
                                                                                    </Columns>

                                                                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Segoe UI" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                                                                    <AlternatingItemStyle BackColor="#e0e0e0" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />

                                                                                </asp:DataGrid>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 50px">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>

                <!--contas em aberto ***************************************************************************** -->
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlMinhasContas" runat="server" Style="z-index: 60; width: 100%; background-color: transparent;" Visible="False">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <div>&nbsp;</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <asp:Panel ID="pnlSubMinhasContas" runat="server">
                                            <!--Cabecario-->
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <div style="width: 100%; height: 2px; background-color: #818181"></div>
                                                    </td>
                                                    <td style="width: 300px; text-align: center">
                                                        <asp:Label ID="lblSConta" runat="server" ForeColor="#818181" CssClass="configlabel" Font-Names="Segoe UI Semibold" Font-Size="18pt" Style="float: none;" Text="Minhas Contas Abertas"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <div style="width: 100%; height: 2px; background-color: #818181"></div>
                                                    </td>
                                                </tr>
                                            </table>

                                            <!--Corpo-->
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblObservacaoConta" runat="server" CssClass="configlabel" ForeColor="#818181" Text="* Não existem contas em aberto." Style="float: none; position: relative; top: 10px;" Font-Names="Segoe UI Semibold" Font-Size="12pt" Visible="false"></asp:Label>

                                                        <div id="DivConta" runat="server" style="overflow: auto; float: none">
                                                            <asp:DataGrid ID="dtgLote" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                                                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" GridLines="None" HorizontalAlign="Center">

                                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                <PagerStyle Mode="NumericPages" PageButtonCount="3" />
                                                                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" HorizontalAlign="Center" Font-Underline="False" ForeColor="Black" />
                                                                <Columns>
                                                                    <asp:TemplateColumn>
                                                                        <ItemTemplate>
                                                                            <div runat="server" id="divConta1" visible="<%# Bind('Visible_1') %>">
                                                                                <table class="btn-ativos" style="width: 110px; height: 110px">
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblAtivo1" runat="server" ForeColor="#818181" Font-Names="Segoe UI Semibold" Font-Size="12pt" Text="<%# Bind('Ativo_Tipo_1') %>"></asp:Label>
                                                                                        </td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td align="center">
                                                                                            <asp:ImageButton ID="btConta1" runat="server" Style="padding: 5px" Width="48px" Height="48px" ImageUrl="<%# Bind('Imagem_1') %>" PostBackUrl="<%# Bind('Link_1') %>" Visible="<%# Bind('Visible_1') %>" />
                                                                                            <asp:ImageButton ID="btConta1Mobile" runat="server" Style="padding: 5px" Width="60px" Height="60px" ImageUrl="<%# Bind('Imagem_1') %>" AlternateText="<%# Bind('Link_1') %>" OnClick="btConta1Mobile_Click" Visible="false" />
                                                                                        </td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblAtivo_Tipo_1" runat="server" ForeColor="#818181" Font-Names="Segoe UI" Font-Size="10pt" Text="<%# Bind('Ativo_1') %>"></asp:Label>
                                                                                        </td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblDt_Lote1" runat="server" ForeColor="#818181" Font-Names="Segoe UI" Font-Size="10pt" Text="<%# Bind('Dt_Lote_1') %>"></asp:Label>
                                                                                        </td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>

                                                                    <asp:TemplateColumn>
                                                                        <ItemTemplate>
                                                                            <div runat="server" id="divConta2" visible="<%# Bind('Visible_2') %>">
                                                                                <table class="btn-ativos" style="width: 110px; height: 110px">
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblAtivo2" runat="server" ForeColor="#818181" Font-Names="Segoe UI Semibold" Font-Size="12pt" Text="<%# Bind('Ativo_Tipo_2') %>"></asp:Label>
                                                                                        </td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td align="center">
                                                                                            <asp:ImageButton ID="btConta2" runat="server" Style="padding: 5px" Width="48px" Height="48px" ImageUrl="<%# Bind('Imagem_2') %>" PostBackUrl="<%# Bind('Link_2') %>" Visible="<%# Bind('Visible_2') %>" />
                                                                                            <asp:ImageButton ID="btConta2Mobile" runat="server" Style="padding: 5px" Width="60px" Height="60px" ImageUrl="<%# Bind('Imagem_2') %>" AlternateText="<%# Bind('Link_2') %>" OnClick="btConta2Mobile_Click" Visible="false" />
                                                                                        </td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblAtivo_Tipo_2" runat="server" ForeColor="#818181" Font-Names="Segoe UI" Font-Size="10pt" Text="<%# Bind('Ativo_2') %>"></asp:Label>
                                                                                        </td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblDt_Lote2" runat="server" ForeColor="#818181" Font-Names="Segoe UI" Font-Size="10pt" Text="<%# Bind('Dt_Lote_2') %>"></asp:Label>
                                                                                        </td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                        <td>&nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                </Columns>
                                                            </asp:DataGrid>
                                                        </div>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 50px">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>

                <!--submenurelatorio-->
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlSubMenuRelatorio" runat="server" Style="z-index: 60; width: 100%; background-color: transparent" Visible="False">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="height: 40px">&nbsp;</td>
                                </tr>
                            </table>
                            <nav>
                                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                    <asp:LinkButton ID="btRMovel" runat="server" CssClass="btn-tab-menu pull-left text-center">
                                        <i id="imgMovel" runat="server" class="fas fa-mobile-alt" style="font-size: 26pt"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btRFixa" runat="server" CssClass="btn-tab-menu-disable pull-left">
                                        <i id="imgFixa" runat="server" class="fas fa-phone-alt" style="font-size: 26pt"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btRLink" runat="server" CssClass="btn-tab-menu-disable">
                                        <i id="imgLink" runat="server" class="fas fa-cloud" style="font-size: 26pt"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btRCloud" runat="server" CssClass="btn-tab-menu-disable">
                                        <i id="imgCloud" runat="server" class="fas fa-wifi" style="font-size: 26pt"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btREquipamento" runat="server" CssClass="btn-tab-menu-disable">
                                        <i id="imgDesktop" runat="server" class="fas fa-server" style="font-size: 26pt"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btRImpressao" runat="server" CssClass="btn-tab-menu-disable">
                                        <i id="imgImpressao" runat="server" class="fas fa-print" style="font-size: 26pt"></i>
                                    </asp:LinkButton>
                                </div>
                            </nav>
                            <div class="card-body" style="background-color: #FFFFFF; border-radius: 6px; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="DivRelatorio" runat="server" style="width: 100%; z-index: 103; overflow: auto; max-height: 320px;">
                                            <asp:DataGrid ID="dtgLocalizaRelatorio" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="0" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" PageSize="5" Style="font-size: 8pt; color: black; font-family: Arial" GridLines="None" ShowHeader="False">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                <PagerStyle Mode="NumericPages" />
                                                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />

                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <table style="width: 100%; float: left" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="btn-Selected" style="height: 40px; text-align: left; padding-left: 10px; padding-top: 8px">
                                                                        <asp:LinkButton ID="bt01" runat="server" ForeColor="#4FC3F7" PostBackUrl="<%# Bind('Link_1') %>">
                                                                            <i id="i20" runat="server" class="fas fa-search-plus" style="font-size: 18pt"></i>
                                                                            <asp:Label ID="lblBt01" runat="server" Font-Names="Segoe UI Semibold" ForeColor="#4FC3F7" CssClass="configlabel" Font-Size="10pt" Style="float: none;" Text="<%# Bind('Nm_Template_1') %>"></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <table style="width: 100%; float: left" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="btn-Selected" style="height: 40px; text-align: left; padding-left: 10px; padding-top: 8px">
                                                                        <asp:LinkButton ID="bt02" runat="server" ForeColor="#4FC3F7" PostBackUrl="<%# Bind('Link_2') %>">
                                                                            <i id="i20" runat="server" class="fas fa-search-plus" style="font-size: 18pt"></i>
                                                                            <asp:Label ID="lblBt02" runat="server" Font-Names="Segoe UI Semibold" ForeColor="#4FC3F7" CssClass="configlabel" Font-Size="10pt" Style="float: none;" Text="<%# Bind('Nm_Template_2') %>"></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:DataGrid>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="padding: 15px">
                                        <asp:Label ID="lblObservacao" runat="server" CssClass="configlabel" ForeColor="#818181" Style="left: 9px; height: 13px; float: none;" Text="* Você não possui permissão de acesso aos relatórios de indicadores desse tipo de ativo." Visible="False"></asp:Label>
                                        <asp:Label ID="lblObservacao_Det" runat="server" CssClass="configlabel" ForeColor="#818181" Style="left: 9px; height: 13px; float: none;" Text="* Você não possui permissão de acesso aos relatórios customizados desse tipo de ativo." Visible="False"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>

                <!--modelo de ativo ***************************************************************************** -->
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlModelo_Ativo" runat="server" Style="width: 100%; z-index: 120; background-color: transparent; overflow: auto;" Visible="false">
                            <table style="width: 100%; height: 100%">
                                <tr>
                                    <td>
                                        <div>&nbsp;</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <!--Cabecario-->
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="text-align: center; height: 18px;">
                                                        <asp:Label ID="lblACTitulo" runat="server" ForeColor="#818181" CssClass="configlabel" Font-Names="Segoe UI Semibold" Font-Size="22pt" Style="float: none;" Text="Meus Ativos"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 30px"></td>
                                                </tr>
                                            </table>

                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="center">
                                                        <div id="DivAtivoModelo" runat="server" style="width: 100%; padding: 10px">
                                                            <div class="divCard">
                                                                <asp:DataGrid ID="dtgModeloAtivo" runat="server" AutoGenerateColumns="False" Width="100%" BorderColor="Transparent" BorderStyle="Solid" CellPadding="5" CellSpacing="5" ShowHeader="false"
                                                                    Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" BorderWidth="1px" GridLines="Horizontal">
                                                                    <PagerStyle Mode="NumericPages" />
                                                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                                                    <Columns>
                                                                        <asp:TemplateColumn>
                                                                            <ItemTemplate>
                                                                                <table style="width: 100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table style="width: 100%" runat="server" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td style="padding: 5px">
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTituloAtivo" runat="server" Text="Ativo" Font-Names="Segoe UI Semibold" ForeColor="#818181" Font-Size="Large"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblAtivo" runat="server" Text="<%# Bind('Nr_Ativo') %>" Font-Names="Segoe UI Light" ForeColor="#818181" Font-Size="Medium" Style="line-height: 30px"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                    <td style="padding: 5px">
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTituloTipo" runat="server" Text="Tipo" Font-Names="Segoe UI Semibold" ForeColor="#818181" Font-Size="Large"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTipo" runat="server" Text="<%# Bind('Nm_Ativo_Tipo') %>" Font-Names="Segoe UI Light" ForeColor="#818181" Font-Size="Medium" Style="line-height: 30px"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                    <td style="padding: 5px">
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTituloConglomerado" runat="server" Text="Fornecedor" Font-Names="Segoe UI Semibold" ForeColor="#818181" Font-Size="Large"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblConglomerado" runat="server" Text="<%# Bind('Nm_Conglomerado') %>" Font-Names="Segoe UI Light" ForeColor="#818181" Font-Size="Medium" Style="line-height: 30px"></asp:Label>
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
                                                                                            <table style="width: 100%" runat="server" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td style="padding: 5px">
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTituloModelo" runat="server" Text="Modelo" Font-Names="Segoe UI Semibold" ForeColor="#818181" Font-Size="Large"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblModelo" runat="server" Text="<%# Bind('Nm_Ativo_Modelo') %>" Font-Names="Segoe UI Light" ForeColor="#818181" Font-Size="Medium" Style="line-height: 30px"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                    <td style="padding: 5px">
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTituloEquip" runat="server" Text="Equipamento" Font-Names="Segoe UI Semibold" ForeColor="#818181" Font-Size="Large"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblEquip" runat="server" Text="<%# Bind('Nr_Aparelho') %>" Font-Names="Segoe UI Light" ForeColor="#818181" Font-Size="Medium" Style="line-height: 30px"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding: 5px">
                                                                                            <table style="width: 100%">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTituloDescricao" runat="server" Text="Descrição" Font-Names="Segoe UI Semibold" ForeColor="#818181" Font-Size="Large"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblDescricao" runat="server" Text="<%# Bind('Finalidade') %>" Font-Names="Segoe UI Light" ForeColor="#818181" Font-Size="Medium" Style="line-height: 30px"></asp:Label>
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
                                                                                            <div style="height: 1px; background-color: #CCCCCC"></div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>

                                                                    </Columns>
                                                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#FFFFFF" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#333333" />
                                                                </asp:DataGrid>
                                                            </div>
                                                            <div>&nbsp;</div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>

            </div>
        </div>

    </div>

    <div>
        <asp:HiddenField ID="Hdf_LarguraTela" runat="server" Value="" ClientIDMode="Static" />
    </div>

</asp:Content>
