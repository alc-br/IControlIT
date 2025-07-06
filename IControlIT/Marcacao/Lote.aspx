<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Lote.aspx.vb" Inherits="IControlIT.Lote" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Filtro_Acesso.ascx" TagName="Filtro_Acesso" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <!-- menssagem obervacao -->
    <div id="pnlMenssagem" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblObservacao" runat="server" Style="float: left" ForeColor="White" Text="É necessário que você selecione um filtro."></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFecharMsg" class="btn btn-primary" runat="server" Text="OK" CausesValidation="False" />
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
                        <div class="col-md-12">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="Relatório" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5">
                            <asp:Label ID="Label17" runat="server" CssClass="configlabel" Text="De:" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                            <asp:DropDownList ID="cboDataDe" runat="server" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                        </div>
                        <div class="col-md-5">
                            <asp:Label ID="Label18" runat="server" CssClass="configlabel" Text="Até:" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                            <asp:DropDownList ID="cboDataAte" runat="server" Width="100%" TabIndex="3" CssClass="configCombo"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <div style="width: 100%; height: 100%; position: relative;">
                                <asp:Button ID="btExecutar" class="btn btn-success" runat="server" Style="position: absolute; right: 0; bottom: -5px" Text="Executar" CausesValidation="False" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="height: 5px"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <uc1:Filtro_Acesso ID="Filtro_Acesso" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:RequiredFieldValidator ID="rfvDe" runat="server" ControlToValidate="cboDataDe" Display="None" Font-Names="Segoe UI" Font-Size="8pt" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvAte" runat="server" ControlToValidate="cboDataAte" Display="None" Font-Names="Segoe UI" Font-Size="8pt" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="hdvTipo_Grafico" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr />
                        </div>
                    </div>
                    <div style="width: 100%; overflow: auto">
                        <asp:DataGrid ID="dtgLote" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                            EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                            HorizontalAlign="Left" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btView" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Check_Pesquisa.png" Style="height: 28px; width: 28px" CausesValidation="False" OnClick="btView_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="16px" />
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="Dt_Lote" HeaderText="Vencimento">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Nm_Ativo_Tipo" HeaderText="Tipo">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Nm_Consumidor" HeaderText="Colaborador">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Dt_Fechamento" HeaderText="Fechado">
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Dt_Exportacao" HeaderText="Exportado">
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Link" HeaderText="Link" Visible="False"></asp:BoundColumn>
                            </Columns>

                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <PagerStyle Mode="NumericPages" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <AlternatingItemStyle BackColor="#E0E0E0" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid>
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
        <asp:LinkButton ID="btLimpar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-file"></i>
            <br />
            <span>Novo</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btDesativar" runat="server" CssClass="btn-menu-toolbar" OnClientClick="return confirm('Você deseja desativa o registro?');" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-save"></i>
            <br />
            <span>Excluir</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btConfiguracao" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-cogs"></i>
            <br />
            <span>Configurações</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btPDF" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span>Configurações</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAdicionar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-plus-square"></i>
            <br />
            <span>Adicionar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="true" Style="opacity: 1;" Enabled="true" OnClick="btExportar_Click">
            <i class="fas fa-file-upload"></i>
            <br />
            <span>Exportar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSolicitação" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-laptop"></i>
            <br />
            <span>Equipamento</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btNotaFiscal" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-receipt"></i>
            <br />
            <span>Nota Fiscal</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btEmail" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClientClick="return confirm('Enviar e-mail notificando da devolução?');" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-envelope-open-text"></i>
            <br />
            <span>E-Mail</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAlerta" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-exclamation-triangle"></i>
            <br />
            <span>Alerta</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSincronizaCont" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-sync-alt"></i>
            <br />
            <span>Sinc/Agenda</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btUpload" runat="server" CssClass="btn-menu-toolbar" Visible="false" CausesValidation="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-file-upload"></i>
            <br />
            <span>Upload</span>
        </asp:LinkButton>
        <asp:LinkButton ID="ImageButton1" runat="server" CssClass="btn-menu-toolbar" Visible="false" CausesValidation="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-play-circle"></i>
            <br />
            <span>Executar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="bt_Recalcular" runat="server" CssClass="btn-menu-toolbar" Visible="false" CausesValidation="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-calculator"></i>
            <br />
            <span>Recalcular</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAprovar_Barra" runat="server" CssClass="btn-menu-toolbar" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-thumbs-up"></i>
            <br />
            <span>Aprovar</span>
        </asp:LinkButton>
    </div>

</asp:Content>
