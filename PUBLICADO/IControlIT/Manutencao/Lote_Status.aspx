<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Lote_Status.aspx.vb" Inherits="IControlIT.Lote_Status" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblUsuario" runat="server" CssClass="configlabel" Style="left: 9px; top: 13px;" Text="Usuário"></asp:Label>
                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDataLote" runat="server" CssClass="configlabel" Style="left: 9px; top: 39px;" Text="Mês"></asp:Label>
                            <asp:DropDownList ID="cboDataLote" runat="server" CssClass="configCombo" Width="100%" AutoPostBack="True" CausesValidation="True" TabIndex="2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblAtivo" runat="server" CssClass="configlabel" Style="left: 9px; top: 39px;" Text="Ativo"></asp:Label>
                            <div id="DivCorpo" runat="server" style="border: 1px solid #C0C0C0; width: 100%; height: 235px; overflow: auto" title=" " visible="True">
                                <asp:DataGrid ID="dtgConsulta" runat="server" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False"
                                    Font-Underline="False" ForeColor="Black" HorizontalAlign="Center" Width="100%" AutoGenerateColumns="False" BorderStyle="Solid" BorderColor="Silver">

                                    <Columns>
                                        <asp:BoundColumn HeaderText="Id_Lote_Marcacao" Visible="False" DataField="Id_Lote_Marcacao"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Visitado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkVisitado" runat="server" Checked="<%# Bind('Dt_Visita') %>" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Fechado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFechado" runat="server" Checked="<%# Bind('Dt_Fechado') %>" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Exportado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkExportado" runat="server" Checked="<%# Bind('Dt_Exportado') %>" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
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

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 832px; position: absolute; top: 507px; height: 45px;">
        <cc1:ValidatorCalloutExtender ID="vceUsuario" runat="server" TargetControlID="rfvUsuario"></cc1:ValidatorCalloutExtender>
        <asp:Label ID="lblMessage" runat="server" Style="left: 13px; top: 87px"> </asp:Label>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="tbBotao" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="btVoltar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btVoltar_Click">
            <i class="fas fa-arrow-left"></i>
            <br />
            <span>Voltar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btLimpar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btLimpar_Click">
            <i class="fas fa-file"></i>
            <br />
            <span>Novo</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" OnClick="btSalvar_Click">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
    </div>

</asp:Content>
