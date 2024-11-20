<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Dashboar_Equipamento.aspx.vb" Inherits="IControlIT.Dashboar_Equipamento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">

    <%-- trava o zoom quando visualizado no mobile e almenta a scala dos objetos na tela deixando responsivo --%>
    <meta name="viewport" content="width=device-width, initial-scale=0.7, maximum-scale=0.7, user-scalable=no" />

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.js"></script>
    <script type="text/javascript" src="https://www.chartjs.org/samples/latest/utils.js"></script>

    <style type="text/css">
        .filtro {
            background-color: #EEEEEE;
            position: fixed;
            overflow: auto;
            padding-top: 64px;
            top: 0px;
            width: 100%;
            height: 100%;
            margin: 0 auto;
            text-align: center;
            z-index: 500;
            right: 0px;
            animation: rigth_popUp 0.2s ease forwards;
            animation-direction: alternate;
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

        @keyframes rigth_popUp {
            0% {
                /*opacity: 0*/
            }

            100% {
                transform: translateX(0px);
                /*opacity: 1*/
            }
        }

        #chtCurvaGasto {
            width: 700px;
        }

        .txtPesquisa {
            float: left;
            text-align: left;
            left: 10px;
            width: 100%;
            padding-left: 5px;
            padding-right: 5px;
        }

        /*/ Responsivo para tela mobile /*/
        @media only screen and (max-width: 600px) {

            .filtro {
                background-color: #EEEEEE;
                position: fixed;
                overflow: auto;
                padding-top: 64px;
                top: 0px;
                width: 100%;
                height: 100%;
                margin: 0 auto;
                text-align: center;
                z-index: 500;
                right: 0px;
                animation: rigth_popUp 0.2s ease forwards;
                animation-direction: alternate;
            }

            .bgGraficos {
                background-color: #EEEEEE;
                width: 100%;
                overflow: auto;
                padding: 60px 15px 15px 15px;
            }

            .tblResponsivo {
                width: 100%;
            }

            @keyframes rigth_popUp {
                0% {
                    /*opacity: 0*/
                }

                100% {
                    transform: translateX(0px);
                    /*opacity: 1*/
                }
            }

            #chtCurvaGasto {
                width: 350px;
            }
        }
    </style>

    <script type="text/javascript">

        function PrintPDF() {

            window.print();

        }

    </script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <!-- Filtro -->
    <div id="divFiltro" runat="server" class="filtro" visible="true">
        <table style="width: 100%">
            <tr>
                <td align="center" runat="server">
                    <table class="tblResponsivo">
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <table style="width: 100%; text-align: left">
                                    <tr>
                                        <td style="float: right">
                                            <asp:LinkButton ID="btnFecharFiltro" runat="server" Visible="true" Style="opacity: 1;" Enabled="true" OnClick="btnFecharFiltro_Click">
                                                <i class="fas fa-times" style="font-size: 20pt"></i>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblFiltros" runat="server" CssClass="configlabel" ForeColor="#323232" Text="FILTROS" Style="float: none;" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="height: 3px; background-color: #CCCCCC"></td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFilial" runat="server" CssClass="configlabel" ForeColor="#323232" Text="Filial" Style="float: none;" Font-Names="Segoe UI" Font-Size="16pt"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lstFilial" runat="server" CssClass="configlistboxAbrir" ForeColor="#818181" Height="80px" Width="100%" AutoPostBack="True"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDepartamento" runat="server" CssClass="configlabel" ForeColor="#323232" Text="Filial" Style="float: none;" Font-Names="Segoe UI" Font-Size="16pt"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lstDepartamento" runat="server" CssClass="configlistboxAbrir" ForeColor="#818181" Height="80px" Width="100%" AutoPostBack="True"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSetor" runat="server" CssClass="configlabel" ForeColor="#323232" Text="Filial" Style="float: none;" Font-Names="Segoe UI" Font-Size="16pt"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lstSetor" runat="server" CssClass="configlistboxAbrir" ForeColor="#818181" Height="80px" Width="100%" AutoPostBack="True"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSecao" runat="server" CssClass="configlabel" ForeColor="#323232" Text="Filial" Style="float: none;" Font-Names="Segoe UI" Font-Size="16pt"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lstSecao" runat="server" CssClass="configlistboxAbrir" ForeColor="#818181" Height="80px" Width="100%" AutoPostBack="True"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCentro_Custo" runat="server" CssClass="configlabel" ForeColor="#323232" Text="Filial" Style="float: none;" Font-Names="Segoe UI" Font-Size="16pt"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lstCentro_Custo" runat="server" CssClass="configlistboxAbrir" ForeColor="#818181" Height="80px" Width="100%" AutoPostBack="True"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 15px"></td>
                                    </tr>

                                </table>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="height: 3px; background-color: #2b262d"></td>
                            <td></td>
                        </tr>
                        <tr style="height: 25px">
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align: center; height: 30px">
                                <asp:Label ID="lblCalendario" runat="server" CssClass="configlabel" ForeColor="#323232" Text="Calendário" Style="float: none;" Font-Names="Segoe UI" Font-Size="18pt"></asp:Label>
                                <asp:HiddenField ID="hdfFiltro" runat="server" />
                                <asp:HiddenField ID="hdfFiltroVoz" Value="1" runat="server" />
                                <asp:HiddenField ID="hdfFiltroDados" Value="0" runat="server" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td align="center" style="text-align: center;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt01" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Jan" Font-Names="Segoe UI" Font-Size="14pt" />
                                        </td>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt02" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Fev" Font-Names="Segoe UI" Font-Size="14pt" />
                                        </td>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt03" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Mar" Font-Names="Segoe UI" Font-Size="14pt" />
                                        </td>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt04" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Abr" Font-Names="Segoe UI" Font-Size="14pt" Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt05" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Mai" Font-Names="Segoe UI" Font-Size="14pt" />
                                        </td>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt06" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Jun" Font-Names="Segoe UI" Font-Size="14pt" />
                                        </td>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt07" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Jul" Font-Names="Segoe UI" Font-Size="14pt" />
                                        </td>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt08" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Ago" Font-Names="Segoe UI" Font-Size="14pt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt09" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Set" Font-Names="Segoe UI" Font-Size="14pt" />
                                        </td>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt10" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Out" Font-Names="Segoe UI" Font-Size="14pt" />
                                        </td>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt11" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Nov" Font-Names="Segoe UI" Font-Size="14pt" />
                                        </td>
                                        <td style="width: 64px; height: 64px">
                                            <asp:Button ID="bt12" runat="server" CssClass="labelCalendario" Width="100%" Height="100%" Text="Dez" Font-Names="Segoe UI" Font-Size="14pt" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hfvMes01" runat="server" />
                                <asp:HiddenField ID="hfvMes02" runat="server" />
                                <asp:HiddenField ID="hfvMes03" runat="server" />
                                <asp:HiddenField ID="hfvMes04" runat="server" />
                                <asp:HiddenField ID="hfvMes05" runat="server" />
                                <asp:HiddenField ID="hfvMes06" runat="server" />
                                <asp:HiddenField ID="hfvMes07" runat="server" />
                                <asp:HiddenField ID="hfvMes08" runat="server" />
                                <asp:HiddenField ID="hfvMes09" runat="server" />
                                <asp:HiddenField ID="hfvMes10" runat="server" />
                                <asp:HiddenField ID="hfvMes11" runat="server" />
                                <asp:HiddenField ID="hfvMes12" runat="server" />
                                <asp:HiddenField ID="hfvFiltro" runat="server" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height: 100px">
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="btnConfirmarFiltro" runat="server" CssClass="btn btn-success" Text="Confirmar" Height="60px" Width="100%" Font-Size="14pt" OnClick="btnConfirmarFiltro_Click" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>

    <!-- Popup Cockpit detalhe -->
    <div id="divCockpit" runat="server" class="filtro" visible="false">
        <table style="width: 100%">
            <tr>
                <td align="center" runat="server">
                    <table class="tblResponsivo">
                        <tr>
                            <td style="float: right">
                                <asp:LinkButton ID="LinkButton1" runat="server" Visible="true" Style="opacity: 1;" Enabled="true" OnClick="btnFecharFiltro_Click">
                                    <i class="fas fa-times" style="font-size: 20pt"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="cardGrafico">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: center; height: 50px">
                                                <asp:Label ID="lblDescricao" runat="server" CssClass="Dash" Font-Size="X-Large"></asp:Label>
                                                <asp:HiddenField ID="hdfBI" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="center" runat="server">
                                                            <table style="width: 100%; height: 30px;">
                                                                <tr>
                                                                    <td style="width: 150px; height: 40px; background-color: #E0E0E0; border-radius: 20px; padding-left: 5px; padding-right: 5px" align="center">
                                                                        <asp:ImageButton ID="BtOk" runat="server" ImageUrl="~/Img_Sistema/Geral/Localiza_Pesquisa_Home.png" Width="22px" Height="22px" Style="position: relative; float: left; top: 2px; left: 5px;" />
                                                                        <asp:TextBox ID="txtOrdenacao" placeholder="Pesquisar" runat="server" Font-Names="Segoe UI" ForeColor="#000000" Height="25px" MaxLength="50" Style="float: left; text-align: left; left: 10px; position: relative; top: 0px; width: 300px; padding-left: 5px; padding-right: 5px" BackColor="Transparent" Font-Bold="False" Font-Strikeout="False" BorderColor="Transparent" Font-Size="13pt"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>

                                                            <table style="width: 100%; background-color: #FFFFFF;">
                                                                <tr>
                                                                    <td>
                                                                        <div id="div1" runat="server" style="left: 5px; overflow: auto; width: 100%; max-height: 500px; border-radius: 6px; border: 1px solid #B06AB3; background-color: #cfa5d1; padding: 5px 1px 5px 1px" title=" ">
                                                                            <asp:DataGrid ID="dtgGrupoDetalhe" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" BackColor="#FFFFFF"
                                                                                EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                                                HorizontalAlign="Left" Font-Size="10pt" Width="100%" ForeColor="#818181" AllowPaging="false" BorderColor="#f7f0f7">

                                                                                <Columns>
                                                                                    <asp:ButtonColumn HeaderStyle-BackColor="#cfa5d1" DataTextField="Nm_Consumidor" CommandName="Select" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Indicadores/add_Black.png&quot; border=&quot;0&quot; height=&quot;20px&quot; width=&quot;20px&quot;&gt;">
                                                                                        <HeaderStyle Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                                                                        <ItemStyle Wrap="false" />
                                                                                    </asp:ButtonColumn>

                                                                                    <asp:BoundColumn DataField="Nm_Consumidor" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderText="Descrição" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                                                                        <ItemStyle Wrap="False" />
                                                                                    </asp:BoundColumn>

                                                                                    <asp:BoundColumn DataField="Nr_Ativo" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderText="Ativo" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                                                                        <ItemStyle Wrap="False" />
                                                                                    </asp:BoundColumn>

                                                                                    <asp:BoundColumn DataField="Fora_Horario" HeaderText="R$|Fora Horário" DataFormatString="{0:R$##########.#0}" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                                                                        <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                                    </asp:BoundColumn>

                                                                                    <asp:BoundColumn DataField="Final_Semana" HeaderText="R$|Final Semana" DataFormatString="{0:R$##########.#0}" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="9pt" />
                                                                                        <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                                    </asp:BoundColumn>

                                                                                    <asp:BoundColumn DataField="Id_Filtro" HeaderText="Id_Filtro" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                                                                        <ItemStyle Wrap="False" />
                                                                                    </asp:BoundColumn>

                                                                                    <asp:BoundColumn DataField="Id_Consumidor" HeaderText="Id_Consumidor" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                                                                        <ItemStyle Wrap="False" />
                                                                                    </asp:BoundColumn>

                                                                                    <asp:BoundColumn DataField="Dt_Lote" HeaderText="Data da Conta" Visible="false" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                                                                        <ItemStyle Wrap="False" />
                                                                                    </asp:BoundColumn>

                                                                                    <asp:BoundColumn DataField="ValorTotalRetornoMarcacaoMes" HeaderText="Marcado" Visible="false" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                                                                        <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                                                                        <ItemStyle Wrap="False" />
                                                                                    </asp:BoundColumn>

                                                                                </Columns>

                                                                                <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Segoe UI" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                                                                <%--<PagerStyle Mode="NumericPages" BackColor="#cfa5d1" BorderColor="#cfa5d1" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" Font-Bold="true" ForeColor="#FFFFFF" Font-Names="Segoe UI Semibold" />--%>
                                                                                <AlternatingItemStyle BackColor="#efe1ef" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                            </asp:DataGrid>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td style="width: 230px"></td>
                                                                                <td align="right">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td style="width: 150px">
                                                                                                <asp:TextBox ID="txtTotalCockpit" runat="server" CssClass="configtextDash" Width="100%" ReadOnly="True" Font-Size="20pt" Font-Names="Segoe UI Semibold" ForeColor="#818181"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: right; padding-right: 10px; background-color: #00CC00; height: 30px; border-radius: 4px">
                                                                                                <asp:Label ID="lblTotalCockpit" runat="server" Width="100%" Text="Custo Total" Font-Size="12pt" ForeColor="#FFFFFF" Font-Names="Segoe UI"></asp:Label>
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
                                                </table>
                                            </td>
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

    <!-- Graficos -->
    <!-- GRAFICO EVOLUCAO DE CUSTO ********************************* -->
    <div id="trGrupo" runat="server" class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body text-center">
                    <div class="row">
                        <div class="col-md-12">
                            <h1 class="Dash">Área</h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="divGrupo" runat="server" style="left: 5px; overflow: auto; width: 100%; border-radius: 6px; border: 1px solid #B06AB3; background-color: #cfa5d1; padding: 5px 1px 5px 1px" title=" ">
                                <asp:DataGrid ID="dtgGrupo" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                    EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                    HorizontalAlign="Center" Font-Size="10pt" Width="100%" ForeColor="#818181" BorderColor="#EEEEEE" AllowPaging="True">

                                    <Columns>
                                        <asp:BoundColumn DataField="Area" HeaderText="Área" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Quantidade" HeaderText="Qtd" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Valor" HeaderText="R$|Mês" DataFormatString="{0:R$##########,#0}" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                            <ItemStyle Wrap="false" />
                                        </asp:BoundColumn>
                                    </Columns>

                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Segoe UI" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <PagerStyle Mode="NumericPages" BackColor="#cfa5d1" BorderColor="#cfa5d1" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" Font-Bold="true" ForeColor="#FFFFFF" Font-Names="Segoe UI Semibold" />
                                    <AlternatingItemStyle BackColor="#efe1ef" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <table>
                                <tr>
                                    <td style="width: 150px;">
                                        <asp:TextBox ID="txtQuantidade" runat="server" CssClass="configtextDash" Width="100%" ReadOnly="True" Font-Size="20pt" Font-Names="Segoe UI Semibold" ForeColor="#818181"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td style="width: 100px;">
                                        <asp:TextBox ID="txtTotal" runat="server" CssClass="configtextDash" Width="100%" ReadOnly="True" Font-Size="20pt" Font-Names="Segoe UI Semibold" ForeColor="#818181"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 10px; background-color: #00CC00; height: 30px; border-radius: 4px">
                                        <asp:Label ID="lblQuantidade" runat="server" Width="100%" Font-Size="12pt" ForeColor="#FFFFFF" Font-Names="Segoe UI" Text="Quantidade"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td style="text-align: right; padding-right: 10px; background-color: #00CC00; height: 30px; border-radius: 4px">
                                        <asp:Label ID="lblTotal" runat="server" Width="100%" Font-Size="12pt" ForeColor="#FFFFFF" Font-Names="Segoe UI" Text="Total"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- EQUIPAMENTO *************-->
    <div id="trTopGastos" runat="server" class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body text-center">
                    <div class="row">
                        <div class="col-md-12">
                            <h1 class="Dash">Tipo de Ativo</h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="divAtivo" runat="server" style="left: 5px; overflow: auto; width: 100%; border-radius: 6px; border: 1px solid #B06AB3; background-color: #cfa5d1; padding: 5px 1px 5px 1px" title=" ">
                                <asp:DataGrid ID="dtgTopGastoMes" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" BackColor="#FFFFFF"
                                    EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                    HorizontalAlign="Left" Font-Size="10pt" Width="100%" ForeColor="#818181" AllowPaging="True" BorderColor="#f7f0f7">

                                    <Columns>
                                        <asp:BoundColumn DataField="Area" HeaderText="Tipo de Ativo" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Quantidade" HeaderText="Qtd" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Valor" HeaderText="R$|Mês" DataFormatString="{0:R$##########,#0}" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                            <ItemStyle Wrap="false" />
                                        </asp:BoundColumn>
                                    </Columns>

                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Segoe UI" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <PagerStyle Mode="NumericPages" BackColor="#cfa5d1" BorderColor="#cfa5d1" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" Font-Bold="true" ForeColor="#FFFFFF" Font-Names="Segoe UI Semibold" />
                                    <AlternatingItemStyle BackColor="#efe1ef" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- CONSUMIDOR ********************************* -->
    <div id="trDetalhamentoUsuario" runat="server" class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body text-center">
                    <div class="row">
                        <div class="col-md-12">
                            <h1 class="Dash">Detalhamento por Consumidor</h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <table style="width: 100%; height: 30px;">
                                <tr>
                                    <td style="width: 150px; height: 40px; background-color: #E0E0E0; border-radius: 20px; padding-left: 5px; padding-right: 5px" align="center">
                                        <asp:ImageButton ID="btLocalizar" runat="server" ImageUrl="~/Img_Sistema/Geral/Localiza_Pesquisa_Home.png" Width="22px" Height="22px" Style="position: relative; float: left; top: 2px; left: 5px;" />
                                        <asp:TextBox ID="txtLocalizar" placeholder="Pesquisar" runat="server" Font-Names="Segoe UI" ForeColor="#000000" Height="25px" MaxLength="50" Style="float: left; text-align: left; left: 10px; position: relative; top: 0px; width: 300px; padding-left: 5px; padding-right: 5px" BackColor="Transparent" Font-Bold="False" Font-Strikeout="False" BorderColor="Transparent" Font-Size="13pt"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div>&nbsp;</div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="divDetalhamentoUsuario" runat="server" style="left: 5px; overflow: auto; width: 100%; border-radius: 6px; border: 1px solid #B06AB3; background-color: #cfa5d1; padding: 5px 1px 5px 1px" title=" ">
                                <asp:DataGrid ID="dtgDetalhamentoUsuario" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" BackColor="#FFFFFF"
                                    EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                    HorizontalAlign="Left" Font-Size="10pt" Width="100%" ForeColor="#818181" AllowPaging="True" BorderColor="#f7f0f7">

                                    <Columns>

                                        <asp:BoundColumn DataField="Nm_Consumidor" HeaderText="Consumidor" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nm_Ativo_Tipo" HeaderText="Tipo" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Total" HeaderText="R$|Mês" DataFormatString="{0:R$##########,#0}" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" />
                                            <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nm_Consumidor" HeaderText="Nm_Consumidor" Visible="False">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Total" HeaderText="Total" Visible="False">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_Filtro" HeaderText="ID_Filtro" Visible="False">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundColumn>
                                    </Columns>

                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Segoe UI" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <PagerStyle Mode="NumericPages" BackColor="#cfa5d1" BorderColor="#cfa5d1" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" Font-Bold="true" ForeColor="#FFFFFF" Font-Names="Segoe UI Semibold" />
                                    <AlternatingItemStyle BackColor="#efe1ef" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- MARCACAO E OU ACESSO E CONCLUSAO DA CONTA ******************** -->
    <div id="tr2" runat="server" class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body text-center">
                    <div class="row">
                        <div class="col-md-12">
                            <h1 class="Dash">Utilização do Portal</h1>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- RECUPERADOS ******************** -->
    <div id="Div5" runat="server" class="row">
        <div class="col-md-6">
            <div class="card" style="background-color: #FFCC00;">
                <div class="card-body text-center">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtUsuarioVisitaramNaoConcluiramMes" runat="server" CssClass="configtextDash" ReadOnly="True" Width="100%" Font-Size="26pt" Font-Names="Segoe UI Semibold" ForeColor="#FFFFFF" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblUsuarioVisitaramNaoConcluiram" runat="server" CssClass="configlabelDash" Text="Contas sem Visualização Mês"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btNaoConcluiramMes" runat="server" ImageUrl="~/Img_Sistema/Default/bt_visualiza.png" Width="32px" Height="32px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="card" style="background-color: #FFCC00;">
                <div class="card-body text-center">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtUsuarioVisitaramNaoConcluiramAno" runat="server" CssClass="configtextDash" ReadOnly="True" Width="100%" Font-Size="26pt" Font-Names="Segoe UI" ForeColor="#FFFFFF" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblUsuarioVisitaramNaoConcluiramAno" runat="server" CssClass="configlabelDash" Text="Contas sem Visualização FY"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btNaoConcluiramAno" runat="server" ImageUrl="~/Img_Sistema/Default/bt_visualiza.png" Height="32px" Width="32px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <!-- GRAFICO EVOLUCAO DE CUSTO ********************************* -->
    <div id="trCurvaGasto" runat="server" class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body text-center">
                    <div class="row">
                        <div class="col-md-12">
                            <h1 class="Dash">Custo</h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div style="overflow: auto; padding: 15px">
                                <asp:Chart ID="chtCurvaGasto" runat="server" CanResize="true" Height="230px" BorderDashStyle="Solid"
                                    ImageType="Png" BorderWidth="1" BackColor="Transparent" BorderColor="Transparent" TextAntiAliasingQuality="Normal"
                                    Palette="SeaGreen" AntiAliasing="All" EnableViewState="true" BorderlineColor="#E0E0E0">

                                    <Series>
                                        <asp:Series Name="A1" XValueType="Double" LabelFormat="R$ {0:#####,0}" IsValueShownAsLabel="true" Color="#E0E0E0" LabelForeColor="#818181" Font="Segoe UI Semibold, 10pt" Legend="Legend1"></asp:Series>
                                    </Series>

                                    <ChartAreas>
                                        <asp:ChartArea Name="chartArea" BorderColor="Transparent" BackSecondaryColor="Transparent" BackColor="Transparent" Area3DStyle-Enable3D="false" ShadowColor="Transparent">

                                            <Area3DStyle Rotation="5" Perspective="5" LightStyle="Realistic" Inclination="10" PointDepth="50" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />

                                            <AxisY LineColor="Transparent" TitleFont="Segoe UI Semibold, 2pt" IsMarksNextToAxis="False" LineDashStyle="NotSet" LineWidth="0">
                                                <LabelStyle Font="Segoe UI Semibold, 2pt" ForeColor="#b91d73" />
                                                <MajorGrid LineColor="#E0E0E0" />
                                            </AxisY>

                                            <AxisX LineColor="Transparent" TitleAlignment="Center" TitleFont="Segoe UI Semibold, 2pt">
                                                <LabelStyle Font="Segoe UI Semibold, 2pt" ForeColor="#b91d73" />
                                                <MajorGrid LineColor="#E0E0E0" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="Div2" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="btVoltar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btVoltar_Click">
            <i class="fas fa-arrow-left"></i>
            <br />
            <span>Voltar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btnFiltro" runat="server" CssClass="btn-menu-toolbar" Visible="true" Style="opacity: 1;" Enabled="true" CausesValidation="false" OnClick="btnFiltro_Click">
            <i class="fas fa-sliders-h"></i>
            <br />
            <span>Filtro</span>
        </asp:LinkButton>
    </div>

</asp:Content>
