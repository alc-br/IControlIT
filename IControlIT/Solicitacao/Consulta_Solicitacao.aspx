<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Consulta_Solicitacao.aspx.vb" Inherits="IControlIT.Consulta_Solicitacao" %>

<%@ MasterType VirtualPath="~/Principal.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Abrir-->
    <div class="row">
        <div class="col-md-12">
            <div id="pnlAbir" runat="server" class="card" visible="false">
                <div class="card-body text-left">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Abrir" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lblSolicitacao" runat="server" CssClass="configlabel" Text="Descrição ou Número"></asp:Label>
                                <asp:TextBox ID="txtSolicitacao" runat="server" AutoPostBack="True" CssClass="configtext" MaxLength="50" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lblUsuario" runat="server" CssClass="configlabel" Text="Usuário ou Colaborador"></asp:Label>
                                <asp:TextBox ID="txtUsuario" runat="server" AutoPostBack="True" CssClass="configtext" MaxLength="50" Width="100%" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblStatus" runat="server" CssClass="configlabel" Text="* Status"></asp:Label>
                            <asp:DropDownList ID="cboStatus" runat="server" Width="100%" TabIndex="3" CssClass="configCombo">
                                <asp:ListItem Value="1">Aberto</asp:ListItem>
                                <asp:ListItem Value="2">Encerrado</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div>&nbsp;</div>
                    <asp:Button ID="btExecutar" class="btn btn-success float-right" runat="server" Text="Executar" CausesValidation="False" />
                    <asp:Button ID="btFechar" class="btn btn-default float-right" runat="server" Text="Fechar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--Content-->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="configlabel" Text="Solicitações" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                        </div>
                    </div>
                    <div>&nbsp;</div>
                    <asp:Label ID="lblMsg" runat="server" Font-Names="Microsoft JhengHei Light" Font-Size="14pt" ForeColor="#262627" Visible="false" Text="*Nenhuma solicitação em aberto."></asp:Label>
                    <asp:DataGrid ID="dtgSolicitacao" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" ShowHeader="false"
                        EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Left" Font-Size="10pt" Width="100%" Height="" ForeColor="#818181" AllowPaging="True" BorderColor="Transparent">

                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="width: 100%; border-bottom: 1px solid #e0e0e0; padding-bottom: 20px">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblRegistroTitulo" runat="server" Text="Registro" ForeColor="#818181" Font-Size="12pt" Font-Names="Segoe UI Semibold"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 15px"></td>
                                                                    <td>
                                                                        <asp:Label ID="lblDispositivoTitulo" runat="server" Text="Dispositivo" ForeColor="#818181" Font-Size="12pt" Font-Names="Segoe UI Semibold"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblIdRegistro" runat="server" Text="<%# Bind('Id_Solicitacao') %>" Style="line-height: 11px; color: #262627"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 15px"></td>
                                                                    <td>
                                                                        <asp:Label ID="lblDispositivo" runat="server" Text="<%# Bind('Nm_Solicitacao_Tipo') %>" Style="line-height: 11px; color: #262627"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-bottom: 5px; padding-top: 5px">
                                                                        <asp:Label ID="lblDescricaoTitulo" runat="server" Text="Descrição" ForeColor="#818181" Font-Size="12pt" Font-Names="Segoe UI Semibold"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDescricao" runat="server" Text="<%# Bind('Matricula') %>" Style="line-height: 11px; color: #262627"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Image ID="imgVD" runat="server" ImageUrl="~/Img_Sistema/Menu/vd.png" Height="18px" Visible="<%# Bind('VD') %>" />
                                                                        <asp:Image ID="imgAM" runat="server" ImageUrl="~/Img_Sistema/Menu/am.png" Height="18px" Visible="<%# Bind('AM') %>" />
                                                                        <asp:Image ID="imgVM" runat="server" ImageUrl="~/Img_Sistema/Menu/vm.png" Height="18px" Visible="<%# Bind('VM') %>" />
                                                                        <asp:Label ID="lblVencimento" runat="server" Text="<%# Bind('Vencimento') %>" Font-Size="9pt" Font-Names="Arial" ForeColor="#000000"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 10px"></td>
                                                                    <td>
                                                                        <asp:Image ID="imgAvaliacao" runat="server" ImageUrl="<%# Bind('Avaliacao') %>" Height="15px" />
                                                                    </td>
                                                                    <td style="width: 10px"></td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmail" runat="server" Text="<%# Bind('Nm_Usuario') %>" Style="line-height: 11px; color: #262627"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <%--<td style="background-color:blueviolet">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Chat.png" Height="36px" Width="36px" ImageAlign="Middle" />
                                            </td>--%>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <%--<asp:ButtonColumn DataTextField="LINK" CommandName="Select" Text="LINK" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Botao/Grid/Grid_Deletar.png&quot; border=&quot;0&quot; height=&quot;42px&quot; " Visible="true">
                                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                        </asp:ButtonColumn>--%>

                            <asp:BoundColumn DataField="Fl_Status" HeaderText="Status" Visible="false">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <HeaderStyle BackColor="#56CCF2" Height="40px" BorderColor="#56CCF2" />
                            </asp:BoundColumn>

                            <asp:ButtonColumn DataTextField="LINK" Visible="true" CommandName="Select" Text="LINK" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Botao/Grid/Grid_Chat.png&quot; border=&quot;0&quot; height=&quot;36px&quot; width=&quot;36px&quot;&gt;">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:ButtonColumn>

                            <asp:BoundColumn DataField="LINK" HeaderText="LINK" Visible="false"></asp:BoundColumn>
                        </Columns>

                        <HeaderStyle Font-Bold="False" Height="40px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#FFFFFF" Font-Names="Segoe UI" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                        <PagerStyle Mode="NumericPages" HorizontalAlign="Center" Font-Size="Medium" ForeColor="#818181" Font-Names="Segoe UI Semibold" />
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <%--<div id="divAxia" runat="server" class="divAxia" style="position: fixed; top: 0px; right: 0px">
        <asp:ImageButton ID="btAssistente" runat="server" CssClass="btnAxia" ImageUrl="~/Img_Sistema/Alicia.png" />
    </div>--%>

    <div id="tbBotao" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="btVoltar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btVoltar_Click">
                <i class="fas fa-arrow-left"></i>
                <br />
                <span>Voltar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" Visible="false" Style="opacity: 0.1;" Enabled="true">
                <i class="fas fa-save"></i>
                <br />
                <span>Salvar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btEncerrar" runat="server" CssClass="btn-menu-toolbar" OnClientClick="return confirm('Você deseja desativar o registro?');" Visible="true" Style="opacity: 0.1;" Enabled="false">
                <i class="fas fa-recycle"></i>
                <br />
                <span>Encerrar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btUnidade" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="true" Style="opacity: 0.1;" Enabled="false">
                <i class="fas fa-cog"></i>
                <br />
                <span>Unidade</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" Visible="true" Style="opacity: 1;" Enabled="true" OnClick="btExportar_Click">
                <i class="fas fa-file-upload"></i>
                <br />
                <span>Exportar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="true" Style="opacity: 1" Enabled="true" OnClick="btAbrir_Click">
                <i class="fas fa-folder-open"></i>
                <br />
                <span>Abrir</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btFornecedor" runat="server" CssClass="btn-menu-toolbar" Visible="true" Style="opacity: 0.1;" Enabled="false">
                <i class="fas fa-file-alt"></i>
                <br />
                <span>Fornecedor</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAvaliacao" runat="server" CssClass="btn-menu-toolbar" Visible="false" Style="opacity: 0.1;" Enabled="false">
                <i class="fas fa-star"></i>
                <br />
                <span>Fornecedor</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btRelatorio" runat="server" CssClass="btn-menu-toolbar" Visible="false" Style="opacity: 0.1;" Enabled="false">
                <i class="fas fa-chart-bar"></i>
                <br />
                <span>Relatório</span>
        </asp:LinkButton>
    </div>

</asp:Content>


