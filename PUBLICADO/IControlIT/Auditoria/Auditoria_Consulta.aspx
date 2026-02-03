<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Auditoria_Consulta.aspx.vb" Inherits="IControlIT.Auditoria_Consulta" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--tela ********************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-10">
                            <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Operadora" Style="float: left;" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <asp:DropDownList ID="cboConglomerado" runat="server" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <div style="width: 100%; height: 100%; position: relative;">
                                <asp:Button ID="btExecutar" class="btn btn-success" runat="server" Style="position: absolute; right: 0; bottom: -5px" Text="Executar" CausesValidation="False" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr />
                        </div>
                    </div>
                    <div id="conteudo" runat="server" visible="false" class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <div class="form-group text-center">
                                        <asp:Label ID="Label17" runat="server" Style="font: 20pt Segoe UI Semibold; color: #09AF00; padding-bottom: 10px" Text="R$ " Font-Bold="True"></asp:Label>
                                        <asp:Label ID="txtTotalRecuperado" runat="server" Style="font: 40pt Segoe UI Semibold; color: #09AF00;" Visible="False" ReadOnly="True"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div style="background-color: #09AF00; padding: 5px; border-radius: 4px; text-align: center">
                                        <asp:Label ID="lblTotalRecuperado" runat="server" Style="font: 12pt Segoe UI; color: #FFFFFF;" Text="Total Recuperado" Visible="False"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" style="height: 50px; position: relative; padding-left: 15px; padding-right: 15px">
                                    <div style="height: 2px; width: calc(100% - 30px); position: absolute; left: 15px; top: calc(50% - 1px); background-color: #09AF00"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblDescricaoArquivo" runat="server" Font-Bold="False" Font-Names="Segoe UI" Font-Size="Larger" ForeColor="#262627" Style="float: none"></asp:Label>
                                    <div id="DivCorpo" runat="server" style="border: 1px solid #C0C0C0; width: 100%; top: 103px; height: 100%; overflow: auto" title=" " visible="False">
                                        <asp:DataGrid ID="dtgConsultaConta" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px">

                                            <Columns>
                                                <asp:ButtonColumn DataTextField="Id_Auditoria_Lote" CommandName="Select" Text="Id_Auditoria_Lote" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Botao/Grid/Grid_View.png&quot; border=&quot;0&quot; height=&quot;30px&quot; width=&quot;30px&quot;&gt;">
                                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:ButtonColumn>
                                            </Columns>

                                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#FFFFFF" BackColor="Black" BorderColor="Black" Font-Names="Segoe UI" Font-Size="12pt" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <PagerStyle Mode="NumericPages" />
                                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <AlternatingItemStyle BackColor="#E0E0E0" />

                                        </asp:DataGrid>
                                    </div>
                                </div>
                            </div>
                            <div style="height: 10px"></div>
                            <div id="divPopupAuditoria" runat="server" visible="false" class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblMsgLocaliza" runat="server" CssClass="configlabel" Text="Status de Auditoria" Style="float: left;" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="height: 5px"></div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div id="Div1" runat="server" style="border: 1px solid #CCCCCC; width: 100%; overflow: auto" title=" ">
                                                <asp:DataGrid ID="dtgAcompanhamento_Status" runat="server" AutoGenerateColumns="False" BorderColor="#818181" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Segoe UI"
                                                    Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" Width="100%" ForeColor="Black" BorderStyle="Solid" BorderWidth="1px">
                                                    <PagerStyle Mode="NumericPages" />

                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Descrição" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" Style="border: 0px; line-height: 15px" Width="100%" Height="100px" Text='<%# Bind("Descricao") %>' TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                        </asp:TemplateColumn>

                                                        <asp:BoundColumn DataField="Data" HeaderText="Data" Visible="True" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                        </asp:BoundColumn>

                                                        <asp:BoundColumn DataField="Valor" DataFormatString="{0:R$ ##########,###########0}" HeaderText="Valor" Visible="True" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                        </asp:BoundColumn>
                                                    </Columns>

                                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#FFFFFF" BackColor="#818181" BorderColor="#818181" Font-Names="Segoe UI" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                    <PagerStyle Mode="NumericPages" />
                                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                    <AlternatingItemStyle BackColor="#E0E0E0" />

                                                </asp:DataGrid>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="height: 5px"></div>
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                            <asp:Button ID="btFecharMsg" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- Retirado por pedido da K2A --%>
                            <%--<div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="lblGrafico" runat="server" Font-Bold="False" Visible="false" Font-Names="Segoe UI" Text="Ofensores (Acumulado)" Font-Size="Larger" ForeColor="#262627" Style="float:none"></asp:Label>
                                    <div id="DivLista" runat="server" style="border: 1px solid #C0C0C0; width: 100%; height: 145px; overflow:auto" title=" " visible="false">
                                        <asp:DataGrid ID="dtgMatrix" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial" 
                                                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px">
                                            <Columns>
                                                <asp:BoundColumn DataField="Descricao" HeaderText="Descrição" Visible="True">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Valor" HeaderText="Valor" Visible="True" DataFormatString="{0:R$ ##########,###########0}">
                                                    <HeaderStyle Width="150px" HorizontalAlign="Right"/>
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"/>
                                                </asp:BoundColumn>
                                            </Columns> 

                                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#FFFFFF" BackColor="Black" BorderColor="Black" Font-Names="Segoe UI" Font-Size="12pt" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <PagerStyle Mode="NumericPages" />
                                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <AlternatingItemStyle BackColor="#E0E0E0" />
                            
                                        </asp:DataGrid>                
                                    </div>
                                    <asp:HiddenField ID="hfdX" runat="server" />
                                    <asp:HiddenField ID="hfdY" runat="server" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Chart ID="Grafico" runat="server" AntiAliasing="All" backcolor="Transparent" BorderDashStyle="Solid" BorderlineColor="Transparent" BorderWidth="1" EnableViewState="true" imagetype="Png" Palette="SeaGreen" TextAntiAliasingQuality="Normal" Height="175px">
                                        <Series>
                                            <asp:Series Name="A1" XValueType="Double" IsValueShownAsLabel="False" Color="#9E9D24" LabelForeColor="#333333" > </asp:Series>
                                        </Series>

                                        <chartareas>
                                            <asp:ChartArea Area3DStyle-Enable3D="false" BackColor="Transparent" BackSecondaryColor="Transparent" BorderColor="Transparent" Name="chartArea" ShadowColor="Transparent">

                                                <area3dstyle Inclination="10" IsClustered="False" IsRightAngleAxes="False" LightStyle="Realistic" Perspective="5" PointDepth="50" Rotation="5" WallWidth="0" />
                                    
                                                <axisy LineColor="0, 0, 0, 0" TitleFont="Segoe UI, 8pt">
                                                    <LabelStyle Font="Segoe UI, 8pt" Enabled="False" />
                                                    <MajorGrid LineColor="0, 0, 0, 0" />
                                                </axisy>

                                                <axisx LineColor="0, 0, 0, 0" TitleAlignment="Center" TitleFont="Segoe UI, 8pt">
                                                    <LabelStyle Font="Segoe UI, 8pt" Enabled="False" />
                                                    <MajorGrid LineColor="0, 0, 0, 0" />
                                                </axisx>

                                            </asp:ChartArea>
                                        </chartareas>
                                    </asp:Chart>
                                </div>
                            </div>--%>
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
        <asp:LinkButton ID="btStatus" CssClass="btn-menu-toolbar" runat="server" CausesValidation="false">
            <i class="fas fa-cog"></i>
            <br />
            <span>Config</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btExportar_Click">
            <i class="fas fa-file-upload"></i>
            <br />
            <span>Export</span>
        </asp:LinkButton>
    </div>

</asp:Content>
