<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="App_Dados.aspx.vb" Inherits="IControlIT.App_Dados" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Abrir-->
    <div id="pnlMsg" runat="server" class="bgModal" visible="True">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="Abrir Relatório" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="Label17" runat="server" CssClass="configlabel" Text="De:" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:DropDownList ID="cboDataDe" runat="server" Style="float: left" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label18" runat="server" CssClass="configlabel" Text="Fornecedor:" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:DropDownList ID="cboConglomerado" runat="server" Style="float: left" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:RequiredFieldValidator ID="rfvDe" runat="server" ControlToValidate="cboDataDe" Font-Names="Arial" Font-Size="10pt">*</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvConglomerado" runat="server" ControlToValidate="cboConglomerado" Font-Names="Arial" Font-Size="10pt">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFechar" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btExecutar" class="btn btn-success" runat="server" Text="Abrir" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--lista de aplicativo ************************************************************************ -->
    <div id="pnlListaAplicativo" runat="server" class="bgModal" visible="False">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblListaAplicativo" runat="server" CssClass="configlabel" ForeColor="#333333" Text="Aplicativos Utilizados" Style="float: none; position: relative; top: 10px;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblCorteDe" runat="server" CssClass="configlabel" Text="Corte De"></asp:Label>
                    <asp:TextBox ID="txtCorteDe" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblNoFiscal" runat="server" CssClass="configlabel" Text="Nota Fiscal"></asp:Label>
                    <asp:TextBox ID="txtNoFiscal" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblCorteAte" runat="server" CssClass="configlabel" Text="Corte Ate"></asp:Label>
                    <asp:TextBox ID="txtCorteAte" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblDataNF" runat="server" CssClass="configlabel" Text="Data NF"></asp:Label>
                    <asp:TextBox ID="txtDataNF" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblPacote" runat="server" CssClass="configlabel" Text="Pacote Mb"></asp:Label>
                    <asp:TextBox ID="txtPacote" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblContrato" runat="server" CssClass="configlabel" Text="Contrato"></asp:Label>
                    <asp:TextBox ID="txtContrato" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblChip" runat="server" CssClass="configlabel" Text="Chip"></asp:Label>
                    <asp:TextBox ID="txtChip" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblModelo" runat="server" CssClass="configlabel" Text="Modelo"></asp:Label>
                    <asp:TextBox ID="txtModelo" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblImei" runat="server" CssClass="configlabel" Text="Imei"></asp:Label>
                    <asp:TextBox ID="txtImei" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblValor" runat="server" CssClass="configlabel" Text="Valor"></asp:Label>
                    <asp:TextBox ID="txtValor" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div id="Div1" runat="server" style="border: 1px solid silver; overflow: auto; width: 100%; height: 230px; border-radius: 10px 10px;">
                        <!-- detalhamento ********************************* -->
                        <div id="Div2" runat="server" style="left: 5px; overflow: auto; width: 100%; height: 220px;">
                            <asp:DataGrid ID="dtgListaAplicativo" runat="server" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="600px" BorderWidth="1px" GridLines="Horizontal">
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                <Columns>
                                    <asp:BoundColumn DataField="Nm_Aplicativo" HeaderText="Aplicativo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Consumo_App" HeaderText="Consumo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                </Columns>
                                <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#FFFFFF" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#333333" />
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFecharLista" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:TextBox ID="txtMBOperadora" runat="server" CssClass="configtext" Width="100%" ReadOnly="True" Font-Size="20pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="#818181"></asp:TextBox>
                            <asp:Label ID="lblMBOperadora" runat="server" Style="vertical-align: middle; border-radius: 2px; padding: 5px" Width="100%" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Mb Operadora" BackColor="Silver" ForeColor="White"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtMBConsumo" runat="server" CssClass="configtext" Width="100%" ReadOnly="True" Font-Size="18pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="#818181"></asp:TextBox>
                            <asp:Label ID="lblMBConsumo" runat="server" Style="vertical-align: middle; border-radius: 2px; padding: 5px" Width="100%" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Mb Consumido" BackColor="Silver" ForeColor="White"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtPacoteLinha" runat="server" CssClass="configtext" Width="100%" ReadOnly="True" Font-Size="18pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="#818181" ForeColor="#00CC00"></asp:TextBox>
                            <asp:Label ID="lblPacoteLinha" runat="server" Style="vertical-align: middle; border-radius: 2px; padding: 5px" Width="100%" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Pacote Linha" BackColor="Silver" ForeColor="White"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtQtdLinha" runat="server" CssClass="configtext" Width="100%" ReadOnly="True" Font-Size="18pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="#818181" ForeColor="#00CC00"></asp:TextBox>
                            <asp:Label ID="lblQtdLinha" runat="server" Style="vertical-align: middle; border-radius: 2px; padding: 5px" Width="100%" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Qtd Linha" BackColor="#00CC00" ForeColor="White"></asp:Label>
                        </div>
                    </div>
                    <div style="height: 5px"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="DivAtivo" runat="server" style="border: 1px solid #C0C0C0; width: 100%; height: 180px; overflow: auto" title=" " visible="true">
                                <asp:DataGrid ID="dtgConsumoUsuario" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                    EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                    HorizontalAlign="Left" PageSize="5" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                                    <Columns>
                                        <asp:BoundColumn DataField="Id_Ativo" HeaderText="Id_Ativo" Visible="False"></asp:BoundColumn>

                                        <asp:ButtonColumn DataTextField="Nr_Ativo" CommandName="Select" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Indicadores/add_Black.png&quot; border=&quot;0&quot; height=&quot;24px&quot; width=&quot;24px&quot;&gt;">
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" />
                                            <HeaderStyle Width="20px" />
                                            <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                        </asp:ButtonColumn>

                                        <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Linha">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nm_Ativo_Tipo" HeaderText="Tipo">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Consumidor" HeaderText="Usuário">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Matricula" HeaderText="Matricula">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Matricula_Chefia" HeaderText="Chefia">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Cargo" HeaderText="Cargo">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="CDC" HeaderText="CDC">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Consumo_App" HeaderText="App">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Trafego_Operadora" HeaderText="Operadora">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
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
                </div>
            </div>
        </div>
    </div>

    <!-- graficos ********************************* -->
    <div class="row">
        <div class="col-md-12">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <asp:Button ID="btnHorario" runat="server" class="btn-tab pull-left" Text="Horário" CausesValidation="False" OnClick="btnHorario_Click" />
                    <asp:Button ID="btnAplicativo" runat="server" class="btn-tab-disable pull-left" Text="Aplicativo" CausesValidation="False" OnClick="btnAplicativo_Click" />
                    <asp:Button ID="btnTendencia" runat="server" class="btn-tab-disable pull-left" Text="Tendência" CausesValidation="False" OnClick="btnTendencia_Click" />
                    <asp:Button ID="btnConexao" runat="server" class="btn-tab-disable pull-left" Text="Sem Conexão" CausesValidation="False" OnClick="btnConexao_Click" />
                </div>
            </nav>
            <div class="card-body" style="background-color: #FFFFFF; border-radius: 6px; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px">

                <!-- horario ********************* -->
                <div id="divHorario" runat="server" visible="true" class="row">
                    <div class="col-md-12">
                        <table style="width: 100%;">
                            <tr>
                                <td></td>
                                <td style="width: 425px;">
                                    <asp:Chart ID="chtFH_FS" runat="server" Width="425px" BorderDashStyle="Solid" ImageType="Png" BorderWidth="1" BorderColor="Transparent" TextAntiAliasingQuality="Normal" Palette="None" BackColor="Transparent" AntiAliasing="All" EnableViewState="true" BorderlineColor="Black"
                                        PaletteCustomColors="56,169,54;99,167,234;99,234,217">
                                        <Series>
                                            <asp:Series ChartArea="MainChartArea" ChartType="Doughnut" Legend="Default" Name="Series1" CustomProperties="PieLabelStyle=Outside"
                                                YValuesPerPoint="1" Font="Trebuchet MS, 9pt, style=Bold" LabelForeColor="Black" BorderColor="#333333" XValueType="Auto">
                                                <SmartLabelStyle AllowOutsidePlotArea="No" MaxMovingDistance="100" />
                                            </asp:Series>
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea BackSecondaryColor="Transparent" BackColor="Transparent" Name="MainChartArea" ShadowColor="Transparent">
                                                <Area3DStyle IsRightAngleAxes="False" />
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>

                <!-- aplicativo ************************ -->
                <div id="divAplicativo" runat="server" visible="false" class="row">
                    <div class="col-md-12">
                        <table style="width: 100%;">
                            <tr>
                                <td></td>
                                <td style="width: 425px;">
                                    <asp:Chart ID="chtAplicativo" runat="server" Width="425px" BorderDashStyle="Solid" ImageType="Png" BorderWidth="1" BorderColor="Transparent" TextAntiAliasingQuality="Normal" Palette="None" BackColor="Transparent" AntiAliasing="All" EnableViewState="true" BorderlineColor="Black"
                                        PaletteCustomColors="56,169,54;99,167,234;99,234,217;247,78,35;245,115,69;220,190,110;104,164,104;54,131,113;63,150,255;122,107,190">
                                        <Series>
                                            <asp:Series ChartArea="MainChartArea" ChartType="Doughnut" Legend="Default" Name="Series1" CustomProperties="PieLabelStyle=Outside"
                                                YValuesPerPoint="1" Font="Trebuchet MS, 9pt, style=Bold" LabelForeColor="Black" BorderColor="#333333" XValueType="Auto">
                                                <SmartLabelStyle AllowOutsidePlotArea="No" MaxMovingDistance="100" />
                                            </asp:Series>
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea BackSecondaryColor="Transparent" BackColor="Transparent" Name="MainChartArea" ShadowColor="Transparent">
                                                <Area3DStyle IsRightAngleAxes="False" />
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>

                <!-- tendencia ************************ -->
                <div id="divTendencia" runat="server" visible="true" class="row">
                    <div class="col-md-12">
                        <table style="width: 100%;">
                            <tr>
                                <td></td>
                                <td style="width: 425px;">
                                    <asp:Chart ID="chtCurvaGasto" runat="server" Width="425px" BorderDashStyle="Solid"
                                        ImageType="Png" BorderWidth="1" BackColor="Transparent" BorderColor="Transparent" TextAntiAliasingQuality="Normal"
                                        Palette="SeaGreen" AntiAliasing="All" EnableViewState="true" BorderlineColor="Black">
                                        <Series>
                                            <asp:Series Name="A1" XValueType="Double" LabelFormat="{0:#####,0}" IsValueShownAsLabel="true" Color="#E78D05" LabelForeColor="Black" Font="Microsoft Sans Serif, 10pt" Legend="Legend1" ChartType="Line"></asp:Series>
                                        </Series>

                                        <ChartAreas>
                                            <asp:ChartArea Name="chartArea" BorderColor="Transparent" BackSecondaryColor="Transparent" BackColor="Transparent" Area3DStyle-Enable3D="false" ShadowColor="Transparent">

                                                <Area3DStyle Rotation="5" Perspective="5" LightStyle="Realistic" Inclination="10" PointDepth="50" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />

                                                <AxisY LineColor="Transparent" TitleFont="Arial, 1pt" IsMarksNextToAxis="False" LineDashStyle="NotSet" LineWidth="0">
                                                    <LabelStyle Font="Arial, 1pt" ForeColor="White" />
                                                    <MajorGrid LineColor="Transparent" />
                                                </AxisY>

                                                <AxisX LineColor="Transparent" TitleAlignment="Center" TitleFont="Arial, 1pt">
                                                    <LabelStyle Font="Arial, 1pt" ForeColor="Black" />
                                                    <MajorGrid LineColor="Transparent" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>

                <!-- conexão ************************ -->
                <div id="divConexaoMenu" runat="server" visible="true" class="row">
                    <div class="col-md-12">
                        <div id="divConexao" runat="server" style="border: 1px solid #C0C0C0; width: 100%; height: 180px; overflow: auto" title=" " visible="true">
                            <asp:DataGrid ID="dtgConexao" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Left" PageSize="5" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                                <Columns>
                                    <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Linha">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Ativo_Tipo" HeaderText="Tipo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Consumo_App" HeaderText="App">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Trafego_Operadora" HeaderText="Operadora">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
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

            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="tbBotao" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="btVoltar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btVoltar_Click">
            <i class="fas fa-arrow-left"></i>
            <br />
            <span>Voltar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-upload"></i>
            <br />
            <span>Export</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAbrir_Click">
            <i class="fas fa-folder-open"></i>
            <br />
            <span>Dados</span>
        </asp:LinkButton>
    </div>

</asp:Content>
