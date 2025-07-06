<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Consulta_Conta.aspx.vb" Inherits="IControlIT.Consulta_Conta" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-5">
                            <asp:Label ID="lblServico" runat="server" CssClass="configlabel" Text="Serviço:" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <asp:DropDownList ID="cboServico" runat="server" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                        </div>
                        <div class="col-md-5">
                            <asp:Label ID="Label1" runat="server" CssClass="configlabel" Text="Data:" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <asp:DropDownList ID="cboDataLote" runat="server" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
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
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblDescricaoArquivo" runat="server" CssClass="configlabel" Style="left: 9px; top: 13px; float: left;" ForeColor="#C1C1C1"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 mb-2">
                            <asp:TextBox ID="txtPesquisa" runat="server" CssClass="configtext" Visible="false" AutoPostBack="true" Width="25%" placeholder="Filtro ..."></asp:TextBox>
                            <div style="width: 75%; height: 100%; position: relative; display: flex; flex-direction: row-reverse;">                            
                                <asp:Button ID="btUploadLote" class="btn btn-info" runat="server" Visible="false" Style="bottom: -5px" Text="Importar arquivos em lote" CausesValidation="False" />
                                <asp:Button ID="btDwZip" class="btn btn-info" runat="server" Visible="false" Style="bottom: -5px" Text="Baixar todas faturas" CausesValidation="False" />
                            </div>
                        </div>
                        <%--<div class="col-md-12 mb-4">
                            <div style="width: 100%; height: 100%; position: relative; display: flex; flex-direction: row-reverse;">                            
                                <asp:Button ID="btUploadLote" class="btn btn-info" runat="server" Visible="false" Style="bottom: -5px" Text="Importar faturas em lote" CausesValidation="False" />
                                <asp:Button ID="btDwZip" class="btn btn-info" runat="server" Visible="false" Style="bottom: -5px" Text="Baixar todas faturas" CausesValidation="False" />
                            </div>
                        </div>--%>
                        <%--<div class="col-md-6 mb-5">
                            <div style="width: 100%; height: 100%; position: relative;">
                                <asp:Button ID="btDwZip" class="btn btn-info" runat="server" Visible="false" Style="position: absolute; right: 0; bottom: -5px" Text="Preparar baixa de todas faturas" CausesValidation="False" />--%>
                                <%--<asp:Label ID="blBtDwZip" runat="server" CssClass="configlabel" Visible="false" Text="Arquivos disponíveis em [ C:\tempIControlIt ]" Style="left: 9px; top: 13px; float: left;" ForeColor="#C1C1C1"></asp:Label>--%>
                            <%--</div>--%>
                        </div>	
                    </div>
                    <div class="row">
                        <div class="col-md-12 overflow-auto">
                            <asp:DataGrid ID="dtgConsultaConta" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Left" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" AllowSorting="True">
                                
                                <Columns>

                                    <asp:BoundColumn SortExpression="Operadora" DataField="Operadora" HeaderText="Operadora" Visible="True">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Fatura" DataField="Fatura" HeaderText="Conta" Visible="True">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Data_Cancelamento" DataField="Data_Cancelamento" HeaderText="Dt. Canc." Visible="True" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Red" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Data_Vencimento" DataField="Data_Vencimento" HeaderText="Dia Venc." Visible="True">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Red" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Cadastrada" DataField="Cadastrada" HeaderText="Cad." DataFormatString="&lt;img src=&quot;../Img_Sistema/Botao/Grid/{0}&quot; border=&quot;0&quot; height=&quot;18px&quot;&gt;" Visible="true">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Conta_Carregada" DataField="Conta_Carregada" HeaderText="Car." DataFormatString="&lt;img src=&quot;../Img_Sistema/Botao/Grid/{0}&quot; border=&quot;0&quot; height=&quot;18px&quot;&gt;" Visible="true">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Dt_Emissao" DataField="Dt_Emissao" HeaderText="Dt. Emissão" Visible="True" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Dt_Vencimento" DataField="Dt_Vencimento" HeaderText="Dt. Venc." Visible="True">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    </asp:BoundColumn>

                                    <%--<asp:BoundColumn SortExpression="Qtd_Linha" DataField="Qtd_Linha" HeaderText="Qtd Linha" Visible="True">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>--%>

                                    <%--<asp:BoundColumn SortExpression="Total" DataField="Total" HeaderText="Total Sistema" Visible="True" DataFormatString="{0:R$##########,###########0}">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>--%>

                                    <asp:BoundColumn SortExpression="Total_Carregado" DataField="Total_Carregado" HeaderText="Total Bilhete" Visible="True" DataFormatString="{0:R$##########,###########0}">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Total_Fatura" DataField="Total_Fatura" HeaderText="Total/Fatura" Visible="True" DataFormatString="{0:R$##########,###########0}">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <%--<asp:BoundColumn SortExpression="Total_Rateado" DataField="Total_Rateado" HeaderText="Total/Rateio" Visible="True" DataFormatString="{0:R$##########,###########0}">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>--%>

                                    <asp:BoundColumn SortExpression="NF" DataField="NF" HeaderText="N.F." Visible="True">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Pedido" DataField="Pedido" HeaderText="Pedido" Visible="True">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />                                        
                                    </asp:BoundColumn>


                                    <%--<asp:BoundColumn SortExpression="Observação" DataField="Observacao" HeaderText="Observação" Visible="True">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>--%>
                                    <asp:TemplateColumn HeaderText="Observação">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObservacao" runat="server"
                                                Text='<%# Left(Eval("Observacao").ToString(), 20) & IIf(Len(Eval("Observacao").ToString()) > 20, "...", "") %>'
                                                ToolTip='<%# Eval("Observacao").ToString() %>' />
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>

                                    <%--<asp:BoundColumn SortExpression="Provisão" DataField="Provisao" HeaderText="Provisão" Visible="True">--%>
                                    <asp:TemplateColumn HeaderText="Provisão">
                                        <%--<ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />--%>
                                         <ItemTemplate>
                                             <div  style="display: flex;justify-content: center;align-items: center;">
                                                <asp:Image ID="imgProvisao" runat="server"  style="width: 30px;"
                                                    ImageUrl='<%# IIf(Convert.ToInt32(Eval("Provisao")) = 1, "~/Img_Sistema/Botao/Grid/Provisaocheck.png", "~/Img_Sistema/Botao/Grid/ProvisaoNaoCheck-box.png") %>' 
                                                    AlternateText='<%# IIf(Convert.ToInt32(Eval("Provisao")) = 1, "Com previsão", "Sem previsão") %>' />
                                              </div>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <%--</asp:BoundColumn>--%>
                                     </asp:TemplateColumn>

                                    <asp:BoundColumn SortExpression="Req" DataField="Req" HeaderText="Req." Visible="True">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Flag_Conta_Cadastrada" DataField="Flag_Conta_Cadastrada" HeaderText="Flag_Conta_Cadastrada" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Flag_Conta_Carregada" DataField="Flag_Conta_Carregada" HeaderText="Flag_Conta_Carregada" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn SortExpression="Id_Fatura" DataField="Id_Fatura" HeaderText="Id_Fatura" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:TemplateColumn HeaderText="Obs">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btObservacao" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Check.png" CssClass="<%# Bind('Download_Observacao')%>" CausesValidation="False" OnClick="btObservacao_Click" CommandArgument="<%# Bind('Fatura')%>" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="B">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btBoleto" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Check.png" CssClass="<%# Bind('Download_Boleto')%>" CausesValidation="False" OnClick="btBoleto_Click" CommandArgument="<%# Bind('Fatura')%>" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText="Nf.">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btNf" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Check.png" CssClass="<%# Bind('Download_NF')%>" CausesValidation="False" OnClick="btNf_Click" CommandArgument="<%# Bind('Fatura')%>" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Rat">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btRateio" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Check.png" CssClass="<%# Bind('Download_Rateio')%>" CausesValidation="False" CommandArgument="<%# Bind('Fatura')%>" OnClick="btRateio_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Fatura">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btDownload" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Check.png" CssClass="<%# Bind('Download_Fatura')%>" CausesValidation="False" OnClick="btDownload_Click" CommandArgument="<%# Bind('Fatura')%>" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btFatura" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_View.png" Style="height: 25px" CausesValidation="False" OnClick="btFatura_Click" Visible="<%# Bind('Visible_LINK') %>" CommandArgument="<%# Bind('Id_Fatura')%>" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>

                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <PagerStyle Mode="NumericPages" />
                                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <AlternatingItemStyle BackColor="#E6E6E6" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />

                            </asp:DataGrid>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">&nbsp</div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 text-left">
                            <asp:TextBox ID="txtFaturaCadastrada" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" Font-Size="18pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="Transparent" Visible="false"></asp:TextBox>
                            <asp:Label ID="lblFaturaCadastrada" runat="server" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Faturas Cadastradas" ForeColor="White" BackColor="Silver" Visible="false" Style="padding: 5px 20px 5px 20px; border-radius: 2px"></asp:Label>
                        </div>
                        <div class="col-md-4 text-left">
                            <asp:TextBox ID="txtFaturaCarregada" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" Font-Size="18pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="Transparent" Visible="false"></asp:TextBox>
                            <asp:Label ID="lblFaturaCarregada" runat="server" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Faturas Carregadas" BackColor="Silver" ForeColor="White" Visible="false" Style="padding: 5px 20px 5px 20px; border-radius: 2px"></asp:Label>
                        </div>
                        <div class="col-md-4 text-left">
                            <asp:TextBox ID="txtTotalCarregado" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" Font-Size="18pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="Transparent" ForeColor="#00CC00" Visible="false"></asp:TextBox>
                            <asp:Label ID="lblTotalCarregado" runat="server" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Operadora" BackColor="Silver" ForeColor="White" Visible="false" Style="padding: 5px 20px 5px 20px; border-radius: 2px"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="pnlObservacao" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Download" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                    <asp:Label ID="Label2" runat="server" CssClass="configlabel" Text="Todas faturas estão disponíveis e preparadas para baixar!" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                    <asp:Label ID="lbPainelObservacaoDownload" runat="server" CssClass="configlabel" Text="" Visible="false" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                    
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-12">
                    <a id="A2" runat="server" class="nav-link" href="~/PDF/View_PDF.aspx?Manual=2">
                        <i class="fas fa-file-pdf"></i>
                        <p>Clique aqui e Baixe suas Faturas</p>
                    </a>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btCancelaModalObs" class="btn btn-success" runat="server" Text="Fechar" CausesValidation="False" UseSubmitBehavior="False" />                    
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
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" OnClick="btExportar_Click">
            <i class="fas fa-file-upload font-size-responsive"></i>
            <br />
            <span>Export</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btConfiguracao" CssClass="btn-menu-toolbar" runat="server" CausesValidation="false">
            <i class="fas fa-cog font-size-responsive"></i>
            <br />
            <span>Config</span>
        </asp:LinkButton>
    </div>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .GREEN {
            height: 25px;
            -webkit-filter: opacity(.5) drop-shadow(0 0 0 green);
            filter: opacity(.5) drop-shadow(0 0 0 green);
        }

        .YELLOW {
            height: 25px;
            -webkit-filter: opacity(.5) drop-shadow(0 0 0 yellow);
            filter: opacity(.5) drop-shadow(0 0 0 yellow);
        }

        .RED {
            height: 25px;
            -webkit-filter: opacity(.5) drop-shadow(0 0 0 red);
            filter: opacity(.5) drop-shadow(0 0 0 red);
        }

        .RED_BLOCK {
            height: 25px;
            -webkit-filter: opacity(.5) drop-shadow(0 0 0 red);
            filter: opacity(.5) drop-shadow(0 0 0 red);
            pointer-events: none;
        }
    </style>

</asp:Content>

