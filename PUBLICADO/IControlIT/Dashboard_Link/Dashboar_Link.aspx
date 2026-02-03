<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Dashboar_Link.aspx.vb" Inherits="IControlIT.Dashboar_Link" %>

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

    <!-- Graficos -->
    <div id="trMsg" runat="server" class="row" visible="false">
        <div class="col-md-12 text-center">
            <h1 class="Dash" style="line-height: 80px">Você não tem informações!</h1>
        </div>
    </div>

    <div class="row">
        <!-- GRAFICO EVOLUCAO DE CUSTO ********************************* -->
        <div id="trGraficoCusto" runat="server" class="col-md-6">
            <div class="row">
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
        </div>
        <!-- GRAFICO POR TIPO *************-->
        <div id="trGraficoInventarioTipo" runat="server" class="col-md-6">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body text-center">
                            <div class="row">
                                <div class="col-md-12">
                                    <h1 class="Dash">Custo por Tipo</h1>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div style="overflow: auto; padding: 15px">
                                        <asp:Chart ID="chtPark" runat="server" CanResize="true" Height="230px" BorderDashStyle="Solid"
                                            ImageType="Png" BorderWidth="1" BackColor="Transparent" BorderColor="Transparent" TextAntiAliasingQuality="Normal"
                                            Palette="SeaGreen" AntiAliasing="All" EnableViewState="true" BorderlineColor="#E0E0E0">

                                            <Series>
                                                <asp:Series Name="A1" XValueType="Double" LabelFormat="R$ {0:#####,0}" IsValueShownAsLabel="true" Color="#E0E0E0" LabelForeColor="#818181" Font="Segoe UI Semibold, 10pt" Legend="Legend1"></asp:Series>
                                            </Series>

                                            <ChartAreas>
                                                <asp:ChartArea Name="chartArea" BorderColor="Transparent" BackSecondaryColor="Transparent" BackColor="Transparent" Area3DStyle-Enable3D="false" ShadowColor="Transparent">

                                                    <Area3DStyle Rotation="5" Perspective="5" LightStyle="Realistic" Inclination="10" PointDepth="50"
                                                        IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />

                                                    <AxisY LineColor="Transparent" TitleFont="Segoe UI Semibold, 2pt" IsMarksNextToAxis="False" LineDashStyle="NotSet" LineWidth="0">
                                                        <LabelStyle Font="Segoe UI Semibold, 2pt" ForeColor="#5B86E5" />
                                                        <MajorGrid LineColor="#E0E0E0" />
                                                    </AxisY>

                                                    <AxisX LineColor="Transparent" TitleAlignment="Center" TitleFont="Segoe UI Semibold, 2pt">
                                                        <LabelStyle Font="Segoe UI Semibold, 2pt" ForeColor="#5B86E5" />
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
        </div>
    </div>

    <!-- TOP DE CUSTO ********************************* -->
    <div id="trGraficoDetalhamento" runat="server" class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body text-center">
                    <div class="row">
                        <div class="col-md-12">
                            <h1 class="Dash">Detalhamento por Área</h1>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <table style="width: 100%; height: 30px;">
                                <tr>
                                    <td style="width: 150px; height: 40px; background-color: #E0E0E0; border: 1px solid #F0F0F0; border-radius: 20px; padding-left: 5px; padding-right: 5px" align="center">
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
                            <div id="divGrupo" runat="server" style="left: 5px; overflow: auto; width: 100%; border-radius: 6px; border: 1px solid #B06AB3; background-color: #cfa5d1; padding: 5px 1px 5px 1px" title=" ">
                                <asp:DataGrid ID="dtgGrupo" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" BackColor="#FFFFFF"
                                    EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                    HorizontalAlign="Left" Font-Size="10pt" Width="100%" ForeColor="#818181" AllowPaging="true" BorderColor="#f7f0f7">

                                    <Columns>
                                        <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                            <ItemStyle Wrap="false" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nm_Conglomerado" HeaderText="Fornecedor" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                            <ItemStyle Wrap="false" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nm_Ativo_Tipo" HeaderText="Tipo" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                            <ItemStyle Wrap="false" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nm_Consumidor" HeaderText="Projeto" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
                                            <ItemStyle Wrap="false" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Total" DataFormatString="{0:R$##########,#0}" HeaderText="R$|Mês" HeaderStyle-ForeColor="#FFFFFF" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Names="Segoe UI Semibold" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle BackColor="#cfa5d1" Height="40px" BorderColor="#cfa5d1" HorizontalAlign="Left" Font-Size="10pt" />
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
                        <div class="col-md-12 text-right">
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="configtextDash" Width="100%" ReadOnly="True" Font-Size="20pt" Font-Names="Segoe UI Semibold" ForeColor="#818181"></asp:TextBox>
                            <asp:Label ID="lblTotal" runat="server" Style="background-color: #00CC00; padding: 5px 15px 5px 15px; border-radius: 4px" Text="Custo Total (Mês)" Font-Size="12pt" ForeColor="#FFFFFF" Font-Names="Segoe UI"></asp:Label>
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
        <asp:LinkButton ID="btnFiltro" runat="server" CssClass="btn-menu-toolbar" Visible="true" Style="opacity: 1;" Enabled="true" OnClick="btnFiltro_Click">
            <i class="fas fa-sliders-h"></i>
            <br />
            <span>Filtro</span>
        </asp:LinkButton>
    </div>

</asp:Content>
