<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Estatistica_Solicitacao.aspx.vb" Inherits="IControlIT.Estatistica_Solicitacao" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Abrir-->
    <div id="pnlMsg" runat="server" class="bgModal" visible="True">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="Abrir Relatório" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="Label17" runat="server" CssClass="configlabel" Text="De:" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:DropDownList ID="cboDataDe" runat="server" Style="float: left" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label18" runat="server" CssClass="configlabel" Text="Até:" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:DropDownList ID="cboDataAte" runat="server" Style="float: left" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:RequiredFieldValidator ID="rfvDe" runat="server" ControlToValidate="cboDataDe" Font-Names="Arial" Font-Size="10pt">*</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvAte" runat="server" ControlToValidate="cboDataAte" Font-Names="Arial" Font-Size="10pt">*</asp:RequiredFieldValidator>
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

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblIncidentePeriodo" runat="server" CssClass="configlabel" Text="Total de Incidente"></asp:Label>
                            <asp:TextBox ID="txtIncidentePeriodo" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblAberto" runat="server" CssClass="configlabel" Text="Incidente Aberto"></asp:Label>
                            <div class="input-group no-border">
                                <asp:TextBox ID="txtAberto" runat="server" Style="display: inline-block; width: calc(100% - 25px)" CssClass="configtext" ReadOnly="True"></asp:TextBox>
                                <asp:ImageButton ID="btAberto" runat="server" Style="display: inline-block; float: right" Height="25px" ImageUrl="~/Img_Sistema/Botao/bt_Atalho.png" Width="25px" ReadOnly="True" ToolTip="Detalhamento de incidente abertos" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblEncerrado" runat="server" CssClass="configlabel" Text="Incidente Encerrado"></asp:Label>
                            <div class="input-group no-border">
                                <asp:TextBox ID="txtEncerrado" runat="server" Style="display: inline-block; width: calc(100% - 25px)" CssClass="configtext" ReadOnly="True"></asp:TextBox>
                                <asp:ImageButton ID="btEncerrado" runat="server" Style="display: inline-block; float: right" Height="25px" ImageUrl="~/Img_Sistema/Botao/bt_Atalho.png" Width="25px" ToolTip="Detalhamento de incidente encerrados" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblAbertoDSLA" runat="server" CssClass="configlabel" Text="Dentro do SLA ■" ForeColor="#6B96A6"></asp:Label>
                            <asp:TextBox ID="txtAbertoDSLA" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblEncerradoDSLA" runat="server" CssClass="configlabel" Text="Dentro do SLA ■" ForeColor="#6B96A6"></asp:Label>
                            <asp:TextBox ID="txtEncerradoDSLA" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblAbertoFSLA" runat="server" CssClass="configlabel" Text="Fora do SLA ■" ForeColor="#2978A1"></asp:Label>
                            <asp:TextBox ID="txtAberturaFSLA" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblEncerradoFSLA" runat="server" CssClass="configlabel" Text="Fora do SLA ■" ForeColor="#2978A1"></asp:Label>
                            <asp:TextBox ID="txtEncerradoFSLA" runat="server" CssClass="configtext" Width="100%" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div style="height: 10px"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="configlabel" Style="float: none;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt" Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div style="height: 5px"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="DivAtivo" runat="server" style="width: 100%; height: 280px; overflow: auto" title=" " visible="False">
                                <asp:DataGrid ID="dtgSolicitacao" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                    EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                    HorizontalAlign="Left" PageSize="8" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                                    <Columns>
                                        <asp:BoundColumn DataField="Id_Solicitacao" HeaderText="Solicitação"></asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nm_Solicitacao_Tipo" HeaderText="Tipo">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Dt_Solicitacao" HeaderText="Solicitação">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Dt_Vencimento" HeaderText="Vencimento">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Dt_Encerramento" HeaderText="Encerramento">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Fl_Status" HeaderText="Status">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:TemplateColumn HeaderText="SLA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVD" runat="server" CssClass="configlabel" Height="24px" Width="24px" Visible="<%# Bind('VD') %>" BackColor="Lime"></asp:Label>
                                                <asp:Label ID="lblAM" runat="server" CssClass="configlabel" Height="24px" Width="24px" Visible="<%# Bind('AM') %>" BackColor="Yellow"></asp:Label>
                                                <asp:Label ID="lblVM" runat="server" CssClass="configlabel" Height="24px" Width="24px" Visible="<%# Bind('VM') %>" BackColor="Red"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:ButtonColumn DataTextField="LINK" CommandName="Select" Text="LINK" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Botao/Grid/Grid_View.png&quot; border=&quot;0&quot; height=&quot;24px&quot; width=&quot;24px&quot;&gt;">
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <HeaderStyle Width="20px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:ButtonColumn>

                                        <asp:BoundColumn DataField="LINK" HeaderText="LINK" Visible="False"></asp:BoundColumn>

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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="tbBotao" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="btVoltar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btVoltar_Click">
            <i class="fas fa-arrow-left"></i>
            <br />
            <span>Voltar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAbrir_Click">
            <i class="fas fa-folder-open"></i>
            <br />
            <span>Dados</span>
        </asp:LinkButton>
    </div>

</asp:Content>
