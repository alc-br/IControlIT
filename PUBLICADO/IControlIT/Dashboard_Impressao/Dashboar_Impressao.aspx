<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Dashboar_Impressao.aspx.vb" Inherits="IControlIT.Dashboar_Impressao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div style="width: 100%; height: 100%;">
        <table style="width: 100%">
            <tr>
                <td>
                    <!--menu-->
                    <table style="border: 1px solid #484848; width: 220px; border-radius: 10px 10px;">
                        <tr>
                            <td style="text-align: center; height: 40px">
                                <asp:Label ID="lblGovernanca" runat="server" CssClass="configlabel" ForeColor="#333333" Text="Governança" Style="float: none;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 220px;">
                                    <tr>
                                        <td style="width: 142px;">
                                            <asp:Label ID="lblGrupoUsuario" runat="server" CssClass="configlabel" Text="Áreas" Font-Bold="False" ForeColor="#333333"></asp:Label>
                                        </td>
                                        <td style="width: 122px;">
                                            <asp:TextBox ID="txtUnidade" runat="server" CssClass="configtext" Style="left: 118px; top: 10px; bottom: 200px; right: 768px; float: left; text-align: right;" Width="40px" ReadOnly="True" BorderColor="Transparent" ForeColor="#333333" Font-Bold="True" Font-Size="10pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btUnidade" runat="server" ImageUrl="~/Img_Sistema/Indicadores/add_Black.png" Height="25px" Width="25px" ToolTip="Detalhamento" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 142px;">
                                            <asp:Label ID="lblColaborador" runat="server" CssClass="configlabel" Text="Colaboradores" Font-Bold="False" ForeColor="#333333"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtColaborador" runat="server" CssClass="configtext" Style="left: 118px; top: 10px; bottom: 200px; right: 768px; float: left; text-align: right;" Width="40px" ReadOnly="True" BorderColor="Transparent" ForeColor="#333333" Font-Bold="True" Font-Size="10pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btColaborador" runat="server" ImageUrl="~/Img_Sistema/Indicadores/add_Black.png" Height="25px" Width="25px" ToolTip="Detalhamento" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 142px;">
                                            <asp:Label ID="lblImediatos" runat="server" CssClass="configlabel" Font-Bold="False" ForeColor="#333333" Text="Imediatos"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtImediato" runat="server" CssClass="configtext" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ReadOnly="True" Style="left: 118px; top: 10px; bottom: 200px; right: 784px; float: left; text-align: right;" Width="40px" BorderColor="Transparent" ForeColor="#333333" Font-Bold="True" Font-Size="10pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btImediatos" runat="server" ImageUrl="~/Img_Sistema/Indicadores/add_Black.png" Height="25px" Width="25px" ToolTip="Detalhamento" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 142px;">
                                            <asp:Label ID="lblInventario" runat="server" CssClass="configlabel" Font-Bold="False" ForeColor="#333333" Text="Inventário atual"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtInventario" runat="server" CssClass="configtext" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ReadOnly="True" Style="left: 118px; top: 10px; bottom: 200px; right: 784px; float: left; text-align: right;" Width="40px" BorderColor="Transparent" ForeColor="#333333" Font-Bold="True" Font-Size="10pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btInventario" runat="server" ImageUrl="~/Img_Sistema/Indicadores/add_Black.png" Height="25px" Width="25px" ToolTip="Detalhamento" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 40px">
                                <asp:Label ID="lblCustoTotal" runat="server" CssClass="configlabel" ForeColor="#333333" Text="Custos Totais" Style="float: none;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 220px;">
                                    <tr>
                                        <td style="width: 162px;">
                                            <asp:Label ID="lblConsumoMes" runat="server" CssClass="configlabel" Font-Bold="False" ForeColor="#333333" Text="Mês "></asp:Label>
                                        </td>
                                        <td style="width: 122px;">
                                            <asp:TextBox ID="txtTotalGastoMes" runat="server" CssClass="configtext" Style="left: 118px; top: 10px; bottom: 200px; right: 784px; float: left; text-align: right;" Width="86px" ReadOnly="True" Font-Bold="False" Font-Size="10pt" ForeColor="#333333" BorderColor="Transparent"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 162px;">
                                            <asp:Label ID="lblConsumoAno" runat="server" CssClass="configlabel" Font-Bold="False" ForeColor="#333333" Text="(FY) | Ano Fiscal"></asp:Label>
                                        </td>
                                        <td style="width: 122px;">
                                            <asp:TextBox ID="txtTotalGastoAno" runat="server" CssClass="configtext" Style="left: 118px; top: 10px; bottom: 200px; right: 784px; float: left; text-align: right;" Width="86px" ReadOnly="True" BorderColor="Transparent" Font-Bold="False" Font-Size="10pt" ForeColor="#333333"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 162px;">
                                            <asp:Label ID="lblCustoFixo" runat="server" CssClass="configlabel" ForeColor="#333333" Text="Custo Fixo"></asp:Label>
                                        </td>
                                        <td style="width: 122px;">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 162px;">
                                            <asp:Label ID="lblFixoMes" runat="server" CssClass="configlabel" Font-Bold="False" ForeColor="#333333" Text="Mês "></asp:Label>
                                        </td>
                                        <td style="width: 122px;">
                                            <asp:TextBox ID="txtTotalFixoMes" runat="server" CssClass="configtext" Style="left: 118px; top: 10px; bottom: 200px; right: 784px; float: left; text-align: right;" Width="86px" ReadOnly="True" BorderColor="Transparent" Font-Bold="False" Font-Size="10pt" ForeColor="#333333"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 162px;">
                                            <asp:Label ID="lblFixoAno" runat="server" CssClass="configlabel" Font-Bold="False" ForeColor="#333333" Text="(FY) | Ano Fiscal"></asp:Label>
                                        </td>
                                        <td style="width: 122px;">
                                            <asp:TextBox ID="txtTotalFixoAno" runat="server" CssClass="configtext" Style="left: 118px; top: 10px; bottom: 200px; right: 784px; float: left; text-align: right;" Width="86px" ReadOnly="True" BorderColor="Transparent" Font-Bold="False" Font-Size="10pt" ForeColor="#333333"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 40px">
                                <asp:Label ID="lblCalendario" runat="server" CssClass="configlabel" ForeColor="#333333" Text="Calendário" Style="float: none;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <div style="width: 172px; margin: 1% auto; padding: 0px;">
                                    <table style="width: 160px;">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="bt01" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_01.png" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="bt02" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_02.png" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="bt03" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_03.png" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="bt04" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_04.png" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="bt05" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_05.png" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="bt06" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_06.png" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="bt07" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_07.png" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="bt08" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_08.png" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="bt09" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_09.png" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="bt10" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_10.png" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="bt11" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_11.png" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="bt12" runat="server" Height="53px" Width="53px" ImageUrl="~/Img_Sistema/Indicadores/bt_Calendario_12.png" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>

                <td>
                    <!--grafico-->
                    <div style="width: 895px; height: 770px; overflow: auto; margin: 1% auto; padding: 0px;">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr style="vertical-align: top">
                                            <td>
                                                <table style="width: 100%; height: 550px">

                                                    <!-- GRAFICO EVOLUCAO DE CUSTO ********************************* -->
                                                    <tr>
                                                        <td style="text-align: center; height: 50px">
                                                            <h1 class="Dash">Custo</h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="overflow: auto; padding: 15px">
                                                                <asp:Chart ID="chtCurvaGasto" runat="server" Height="220px" Width="850px" BorderDashStyle="Solid"
                                                                    ImageType="Png" BorderWidth="1" BackColor="Transparent" BorderColor="Transparent" TextAntiAliasingQuality="Normal"
                                                                    Palette="SeaGreen" AntiAliasing="All" EnableViewState="true" BorderlineColor="White">

                                                                    <Series>
                                                                        <asp:Series Name="A1" XValueType="Double" LabelFormat="R${0:######,0}" IsValueShownAsLabel="true" Color="#ac9207" LabelForeColor="Black" Font="Microsoft Sans Serif, 10pt" Legend="Legend1"></asp:Series>
                                                                    </Series>

                                                                    <ChartAreas>
                                                                        <asp:ChartArea Name="chartArea" BorderColor="Transparent" BackSecondaryColor="Transparent" BackColor="Transparent" Area3DStyle-Enable3D="false" ShadowColor="Transparent">

                                                                            <Area3DStyle Rotation="5" Perspective="5" LightStyle="Realistic" Inclination="10" PointDepth="50" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />

                                                                            <AxisY LineColor="Transparent" TitleFont="Arial, 1pt">
                                                                                <LabelStyle Font="Arial, 1pt" ForeColor="Black" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisY>

                                                                            <AxisX LineColor="Transparent" TitleAlignment="Center" TitleFont="Arial, 1pt">
                                                                                <LabelStyle Font="Arial, 1pt" ForeColor="Black" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisX>
                                                                        </asp:ChartArea>
                                                                    </ChartAreas>
                                                                </asp:Chart>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <!-- TOP DE CUSTO ********************************* -->
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="text-align: center; height: 50px">
                                                                                    <h1 class="Dash">Detalhamento por Área</h1>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="border: 1px solid #9E9E9E; width: 390px; height: 125px; border-radius: 10px 10px; background-color: #FFFFFF;">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div id="divGrupo" runat="server" style="left: 5px; overflow: auto; width: 410px; top: 103px; height: 150px;" title=" ">
                                                                                                    <asp:DataGrid ID="dtgGrupo" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                                                                                        EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                                                                        HorizontalAlign="Left" Font-Size="9pt" Width="389px" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                                                                                                        <Columns>
                                                                                                            <asp:BoundColumn DataField="Descricao" HeaderText="Descrição">
                                                                                                                <ItemStyle Wrap="false" />
                                                                                                            </asp:BoundColumn>

                                                                                                            <asp:BoundColumn DataField="Mes" DataFormatString="{0:R$##########,#0}" HeaderText="R$|Mês">
                                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                                                <ItemStyle HorizontalAlign="Right" Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                                                            </asp:BoundColumn>

                                                                                                            <asp:BoundColumn DataField="Ano" DataFormatString="{0:R$##########,#0}" HeaderText="R$|FY">
                                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                                                <ItemStyle HorizontalAlign="Right" Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                                                            </asp:BoundColumn>

                                                                                                            <asp:ButtonColumn CommandName="Select" DataTextField="Descricao" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Indicadores/add_Black.png&quot; border=&quot;0&quot; height=&quot;20px&quot; width=&quot;20px&quot;&gt;">
                                                                                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                                                                                <HeaderStyle Width="20px" />
                                                                                                                <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                                                            </asp:ButtonColumn>

                                                                                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                                                                                <HeaderStyle HorizontalAlign="Right" Width="20px" />
                                                                                                                <ItemStyle HorizontalAlign="Right" Wrap="false" />
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
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="text-align: center; height: 50px">
                                                                                    <h1 class="Dash">Detalhamento por Usuário</h1>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="border: 1px solid #9E9E9E; width: 390px; height: 125px; border-radius: 10px 10px; background-color: #FFFFFF;">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div id="divAtivo" runat="server" style="left: 5px; overflow: auto; width: 410px; top: 103px; height: 150px;" title=" ">
                                                                                                    <asp:DataGrid ID="dtgAtivo" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                                                                                        EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                                                                        HorizontalAlign="Left" Font-Size="9pt" Width="389px" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                                                                                                        <Columns>
                                                                                                            <asp:BoundColumn DataField="Descricao" HeaderText="Descrição">
                                                                                                                <ItemStyle Wrap="false" />
                                                                                                            </asp:BoundColumn>

                                                                                                            <asp:BoundColumn DataField="Mes" DataFormatString="{0:R$##########,#0}" HeaderText="R$|Mês">
                                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                                                <ItemStyle HorizontalAlign="Right" Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                                                            </asp:BoundColumn>

                                                                                                            <asp:BoundColumn DataField="Ano" DataFormatString="{0:R$##########,#0}" HeaderText="R$|FY">
                                                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                                                <ItemStyle HorizontalAlign="Right" Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                                                            </asp:BoundColumn>

                                                                                                            <asp:ButtonColumn CommandName="Select" DataTextField="Descricao" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Indicadores/add_Black.png&quot; border=&quot;0&quot; height=&quot;20px&quot; width=&quot;20px&quot;&gt;">
                                                                                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                                                                                                <HeaderStyle Width="20px" />
                                                                                                                <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                                                                            </asp:ButtonColumn>

                                                                                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                                                                                <HeaderStyle HorizontalAlign="Right" Width="20px" />
                                                                                                                <ItemStyle HorizontalAlign="Right" Wrap="false" />
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
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>

                                                    <!-- GRAFICO CUSTO POR AREA POR TIPO ********************************* -->
                                                    <tr>
                                                        <td style="text-align: center; height: 50px">
                                                            <h1 class="Dash">Custo por Área por Tipo</h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="overflow: auto; padding: 15px">
                                                                <asp:Chart ID="chtCurvaTipo" runat="server" Height="320px" Width="850px" BorderDashStyle="Solid" ImageType="Png" BorderWidth="1" BorderColor="Transparent" TextAntiAliasingQuality="Normal"
                                                                    Palette="None" BorderlineColor="Black" BackColor="Transparent" AntiAliasing="All" EnableViewState="true"
                                                                    PaletteCustomColors="#fff176;#ffb74d">
                                                                    <Legends>
                                                                        <asp:Legend ForeColor="Black" TitleFont="Arial, 8pt, style=Bold" BackColor="Transparent" Font="Arial, 8pt, style=Bold" Name="Default"></asp:Legend>
                                                                    </Legends>

                                                                    <Series>
                                                                        <asp:Series Name="A1" XValueType="Double" ChartType="StackedBar" IsVisibleInLegend="false" Font="Arial, 8pt" LabelForeColor="Black"></asp:Series>
                                                                        <asp:Series Name="A2" XValueType="Double" ChartType="StackedBar" IsVisibleInLegend="false" Font="Arial, 8pt" LabelForeColor="Black"></asp:Series>
                                                                    </Series>

                                                                    <ChartAreas>
                                                                        <asp:ChartArea Name="chartArea" BorderColor="Transparent" BackSecondaryColor="Transparent" BackColor="Transparent" Area3DStyle-Enable3D="true" ShadowColor="Transparent">
                                                                            <Area3DStyle Rotation="5" Perspective="5" LightStyle="Realistic" Inclination="10" PointDepth="50"
                                                                                IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />

                                                                            <AxisY LineColor="Transparent" TitleAlignment="Center" TitleFont="Arial, 8pt">
                                                                                <LabelStyle Font="Arial, 8pt" ForeColor="Transparent" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisY>

                                                                            <AxisX LineColor="Transparent" TitleAlignment="Center" TitleFont="Arial, 8pt">
                                                                                <LabelStyle Font="Arial, 8pt" ForeColor="Black" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisX>
                                                                        </asp:ChartArea>
                                                                    </ChartAreas>
                                                                </asp:Chart>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <!-- GRAFICO CUSTO POR AREA POR TIPO DE PAPEL ********************************* -->
                                                    <tr>
                                                        <td style="text-align: center; height: 50px">
                                                            <h1 class="Dash">Custo por Área por Tipo de Papel</h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="overflow: auto; padding: 15px">
                                                                <asp:Chart ID="chtCurvaTipoPapel" runat="server" Height="320px" Width="850px" BorderDashStyle="Solid" ImageType="Png" BorderWidth="1" BorderColor="Transparent" TextAntiAliasingQuality="Normal" Palette="None" BorderlineColor="Black"
                                                                    BackColor="Transparent" AntiAliasing="All" EnableViewState="true"
                                                                    PaletteCustomColors="#546e7a;#4e342e;#616161;#4527a0;#2e7d32">

                                                                    <Legends>
                                                                        <asp:Legend ForeColor="Black" TitleFont="Arial, 8pt, style=Bold" BackColor="Transparent" Font="Arial, 8pt, style=Bold" Name="Default"></asp:Legend>
                                                                    </Legends>

                                                                    <Series>
                                                                        <asp:Series Name="A1" XValueType="Double" ChartType="StackedBar" IsVisibleInLegend="false" Font="Arial, 8pt"></asp:Series>
                                                                        <asp:Series Name="A2" XValueType="Double" ChartType="StackedBar" IsVisibleInLegend="false" Font="Arial, 8pt"></asp:Series>
                                                                        <asp:Series Name="A3" XValueType="Double" ChartType="StackedBar" IsVisibleInLegend="false" Font="Arial, 8pt"></asp:Series>
                                                                        <asp:Series Name="A4" XValueType="Double" ChartType="StackedBar" IsVisibleInLegend="false" Font="Arial, 8pt"></asp:Series>
                                                                        <asp:Series Name="A5" XValueType="Double" ChartType="StackedBar" IsVisibleInLegend="false" Font="Arial, 8pt"></asp:Series>
                                                                    </Series>

                                                                    <ChartAreas>
                                                                        <asp:ChartArea Name="chartArea" BorderColor="Transparent" BackSecondaryColor="Transparent" BackColor="Transparent" Area3DStyle-Enable3D="true" ShadowColor="Transparent">
                                                                            <Area3DStyle Rotation="5" Perspective="5" LightStyle="Realistic" Inclination="10" PointDepth="50"
                                                                                IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />

                                                                            <AxisY LineColor="Transparent" TitleAlignment="Center" TitleFont="Arial, 8pt">
                                                                                <LabelStyle Font="Arial, 8pt" ForeColor="Transparent" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisY>

                                                                            <AxisX LineColor="Transparent" TitleAlignment="Center" TitleFont="Arial, 5pt">
                                                                                <LabelStyle Font="Arial, 5pt" ForeColor="Black" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisX>
                                                                        </asp:ChartArea>
                                                                    </ChartAreas>
                                                                </asp:Chart>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <!-- Custo por tipo de documento ******************** -->
                                                    <tr>
                                                        <td style="text-align: center; height: 50px">
                                                            <h1 class="Dash">Custo por Tipo de Documento</h1>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <div style="overflow: auto; padding: 15px">
                                                                <asp:Chart ID="chtTipoDocumento" runat="server" Height="320px" Width="850px" BorderDashStyle="Solid"
                                                                    ImageType="Png" BorderWidth="1" BorderColor="Transparent" TextAntiAliasingQuality="Normal"
                                                                    Palette="None" BorderlineColor="Black" BackColor="Transparent" AntiAliasing="All" EnableViewState="true" PaletteCustomColors="153, 182, 75">

                                                                    <Series>
                                                                        <asp:Series Name="A1" XValueType="Double" LabelFormat="R${0:#####,0}" IsValueShownAsLabel="true" Color="#99B64B" LabelForeColor="Black" IsXValueIndexed="True" ChartType="Bar">
                                                                            <EmptyPointStyle IsValueShownAsLabel="True" />
                                                                        </asp:Series>
                                                                    </Series>

                                                                    <ChartAreas>
                                                                        <asp:ChartArea Name="chartArea" BorderColor="Transparent" BackSecondaryColor="Transparent" BackColor="Transparent" Area3DStyle-Enable3D="true" ShadowColor="Transparent">
                                                                            <Area3DStyle Rotation="5" Perspective="5" LightStyle="Realistic" Inclination="10" PointDepth="50" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                                                                            <AxisY LineColor="Transparent" TitleAlignment="Center" TitleFont="Arial, 8pt">
                                                                                <LabelStyle Font="Arial, 8pt" ForeColor="Transparent" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisY>

                                                                            <AxisX LineColor="Transparent" TitleAlignment="Center" TitleFont="Arial, 8pt">
                                                                                <LabelStyle Font="Arial, 8pt" ForeColor="Black" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisX>
                                                                        </asp:ChartArea>
                                                                    </ChartAreas>
                                                                </asp:Chart>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <!-- GRAFICO EVOLUCAO DP PARK DE ATIVO *************-->
                                                    <tr>
                                                        <td style="text-align: center; height: 50px">
                                                            <h1 class="Dash">Impressões</h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="overflow: auto; padding: 15px">
                                                                <asp:Chart ID="chtCurvaAtivo" runat="server" Height="220px" Width="850px" BorderDashStyle="Solid"
                                                                    ImageType="Png" BorderWidth="1" BackColor="Transparent" BorderColor="Transparent" TextAntiAliasingQuality="Normal"
                                                                    Palette="SeaGreen" AntiAliasing="All" EnableViewState="true" BorderlineColor="Black">

                                                                    <Series>
                                                                        <asp:Series Name="A1" XValueType="Double" IsValueShownAsLabel="true" Color="#9E9D24" LabelForeColor="#000000" Font="Microsoft Sans Serif, 10pt"></asp:Series>
                                                                    </Series>

                                                                    <ChartAreas>
                                                                        <asp:ChartArea Name="chartArea" BorderColor="Transparent" BackSecondaryColor="Transparent" BackColor="Transparent" Area3DStyle-Enable3D="false" ShadowColor="Transparent">

                                                                            <Area3DStyle Rotation="5" Perspective="5" LightStyle="Realistic" Inclination="10" PointDepth="50"
                                                                                IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />

                                                                            <AxisY LineColor="Transparent" TitleFont="Arial, 1pt">
                                                                                <LabelStyle Font="Arial, 1pt" ForeColor="Black" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisY>

                                                                            <AxisX LineColor="Transparent" TitleAlignment="Center" TitleFont="Arial, 1pt">
                                                                                <LabelStyle Font="Arial, 1pt" ForeColor="Black" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisX>
                                                                        </asp:ChartArea>
                                                                    </ChartAreas>
                                                                </asp:Chart>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <!-- GRAFICO POR TIPO *************-->
                                                    <tr>
                                                        <td style="text-align: center; height: 50px">
                                                            <h1 class="Dash">Impressões por Área</h1>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <div style="overflow: auto; padding: 15px">
                                                                <asp:Chart ID="chtCurvaUnidade" runat="server" Width="850px" Height="320px" BorderDashStyle="Solid" ImageType="Png" BorderWidth="1" BorderColor="Transparent" TextAntiAliasingQuality="Normal"
                                                                    Palette="None" BorderlineColor="Black" BackColor="Transparent" AntiAliasing="All" EnableViewState="true"
                                                                    PaletteCustomColors="51,105,30;85,139,47">

                                                                    <Legends>
                                                                        <asp:Legend ForeColor="Black" TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" Font="Trebuchet MS, 8pt, style=Bold" Name="Default" ItemColumnSeparatorColor="Black"></asp:Legend>
                                                                    </Legends>

                                                                    <Series>
                                                                        <asp:Series Name="A1" XValueType="Double" ChartType="StackedBar" IsVisibleInLegend="false" Font="Arial, 8pt" LabelForeColor="Black"></asp:Series>
                                                                        <asp:Series Name="A2" XValueType="Double" ChartType="StackedBar" IsVisibleInLegend="false" Font="Arial, 8pt" LabelForeColor="Black"></asp:Series>
                                                                    </Series>

                                                                    <ChartAreas>
                                                                        <asp:ChartArea Name="chartArea" BorderColor="Transparent"
                                                                            BackSecondaryColor="Transparent" BackColor="Transparent" Area3DStyle-Enable3D="true"
                                                                            ShadowColor="Transparent">

                                                                            <Area3DStyle Rotation="5" Perspective="5" LightStyle="Realistic" Inclination="10" PointDepth="50"
                                                                                IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />

                                                                            <AxisY LineColor="Transparent" TitleAlignment="Center" TitleFont="Arial, 8pt">
                                                                                <LabelStyle Font="Arial, 8pt" ForeColor="Transparent" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisY>

                                                                            <AxisX LineColor="Transparent" TitleAlignment="Center" TitleFont="Arial, 8pt">
                                                                                <LabelStyle Font="Arial, 8pt" ForeColor="Black" />
                                                                                <MajorGrid LineColor="Transparent" />
                                                                            </AxisX>
                                                                        </asp:ChartArea>
                                                                    </ChartAreas>
                                                                </asp:Chart>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <!-- GRAFICO POR AREA *************-->
                                                    <tr>
                                                        <td style="text-align: center; height: 50px">
                                                            <h1 class="Dash">Impressões por Tipo</h1>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <div style="overflow: auto; padding: 15px">
                                                                <asp:Chart ID="chtPark" runat="server" Width="550px" Height="270px" BorderDashStyle="Solid" ImageType="Png" BorderWidth="1" BorderColor="Transparent" TextAntiAliasingQuality="Normal" Palette="None" BackColor="Transparent" AntiAliasing="All" EnableViewState="true"
                                                                    BorderlineColor="Black" PaletteCustomColors="51,105,30;85,139,47">

                                                                    <Series>
                                                                        <asp:Series ChartArea="MainChartArea" ChartType="Pie" Legend="Default" Name="Series1" CustomProperties="PieLabelStyle=Outside"
                                                                            YValuesPerPoint="6" Font="Trebuchet MS, 8.25pt, style=Bold" LabelForeColor="Black" BorderColor="#333333">
                                                                            <SmartLabelStyle AllowOutsidePlotArea="No" MaxMovingDistance="100" />
                                                                        </asp:Series>
                                                                    </Series>

                                                                    <ChartAreas>
                                                                        <asp:ChartArea BackSecondaryColor="Transparent" BackColor="Transparent" Name="MainChartArea" ShadowColor="Transparent">
                                                                            <Area3DStyle IsRightAngleAxes="False" />
                                                                        </asp:ChartArea>
                                                                    </ChartAreas>
                                                                </asp:Chart>
                                                            </div>
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
        <asp:LinkButton ID="btnFiltro" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="true" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-sliders-h"></i>
            <br />
            <span>Filtro</span>
        </asp:LinkButton>
    </div>

</asp:Content>
