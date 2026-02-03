<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Consulta_Caixa_Entrada.aspx.vb" Inherits="IControlIT.Consulta_Caixa_Entrada" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--detalhamento -->
    <div id="pnlmsg" runat="server" class="bgModal" visible="False">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblMsgLocaliza" runat="server" CssClass="configlabel" Text="Mensagem" Style="float: none" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div>&nbsp;</div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblAssunto" runat="server" CssClass="configlabel" Text="Assunto:" ForeColor="#1E88E5" Font-Names="Segoe UI Semibold"></asp:Label>
                    <asp:Label ID="lblDescricaoAssunto" runat="server" Style="float: left; width: 100%" CssClass="configlabel" Font-Names="Segoe UI"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblEmailDestino" runat="server" CssClass="configlabel" Text="Destino:" ForeColor="#1E88E5" Font-Names="Segoe UI Semibold"></asp:Label>
                    <asp:Label ID="lblDescricaoEmailDestino" runat="server" Style="float: left; width: 100%" CssClass="configlabel" Font-Names="Segoe UI"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblEmailCopia" runat="server" CssClass="configlabel" Text="Cópia:" ForeColor="#1E88E5" Font-Names="Segoe UI Semibold"></asp:Label>
                    <asp:Label ID="lblDescricaoEmailCopia" runat="server" Style="float: left; width: 100%" CssClass="configlabel" Font-Names="Segoe UI"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblTexto" runat="server" CssClass="configlabel" Text="Texto:" ForeColor="#1E88E5" Font-Names="Segoe UI Semibold"></asp:Label>
                    <asp:Label ID="lblDescricaoTexto1" runat="server" Style="float: left; width: 100%; line-height: 20px; white-space: pre-wrap; text-align: justify;" CssClass="configlabel" Font-Names="Segoe UI"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescricaoTexto2" runat="server" Style="float: left; line-height: 20px; white-space: pre-wrap; text-align: justify;" Font-Names="Segoe UI Semibold" ForeColor="White"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescricaoTexto3" runat="server" Style="float: left; line-height: 20px; white-space: pre-wrap; text-align: justify;" Font-Names="Segoe UI" ForeColor="#818181"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescricaoTexto4" runat="server" Style="float: left; line-height: 20px; white-space: pre-wrap; text-align: justify;" Font-Names="Segoe UI" ForeColor="#818181"> </asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescricaoTexto5" runat="server" Style="float: left; line-height: 20px; white-space: pre-wrap; text-align: justify;" Font-Names="Segoe UI" ForeColor="#818181"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescricaoTextAdicional" runat="server" Style="float: left; line-height: 20px; white-space: pre-wrap; text-align: justify;" Font-Names="Segoe UI" ForeColor="#818181"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFecharMsg" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btReenviarEmail" class="btn btn-primary" runat="server" Text="Reenviar" CausesValidation="False" />
                    <asp:HiddenField ID="hdfIdMail" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <!--tela ********************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-10">
                            <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Contestação" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <asp:DropDownList ID="cboMenssagem" runat="server" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
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
                    <div style="width: 100%; overflow: auto">
                        <asp:DataGrid ID="dtgLocaliza" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px" AllowPaging="True" PageSize="11">

                            <Columns>
                                <asp:ButtonColumn DataTextField="Id_Mail_Caixa_Siada" CommandName="Select" Text="Id_Mail_Caixa_Siada" ItemStyle-HorizontalAlign="Center" DataTextFormatString="&lt;img src=&quot;../Img_Sistema/Botao/Grid/Grid_Check_Pesquisa.png&quot; border=&quot;0&quot; height=&quot;28px&quot; width=&quot;28px&quot;&gt;">
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:ButtonColumn>

                                <asp:BoundColumn DataField="Assunto" HeaderText="Assunto">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Texto" HeaderText="Texto">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Dt_Programacao" HeaderText="Programado">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Dt_Saida" HeaderText="Enviado">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Id_Mail_Caixa_Siada" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>
                            </Columns>

                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <PagerStyle Mode="NumericPages" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <AlternatingItemStyle BackColor="#E0E0E0" />

                        </asp:DataGrid>
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
    </div>

</asp:Content>



