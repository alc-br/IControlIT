<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Plano_Conta.aspx.vb" Inherits="IControlIT.Plano_Conta" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-10">
                            <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Abrir Centralização de Contas" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <%--<asp:ListBox ID="lstConglomerado" runat="server" CssClass="configlistboxAbrir" Width="100%" Style="max-height:200px" Font-Overline="False"></asp:ListBox>--%>
                            <asp:DropDownList ID="cboConglomerado" runat="server" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <div style="width: 100%; height: 100%; position: relative;">
                                <asp:Button ID="btExecutar" class="btn btn-success" runat="server" Style="position: absolute; height: 35px; right: 0; bottom: -5px" Text="Executar" CausesValidation="False" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">&nbsp</div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="DivAtivo" runat="server" style="z-index: 103; left: 5px; overflow: auto; width: 100%;" title=" ">
                                <asp:Label ID="lblMsg" runat="server" Font-Names="Microsoft JhengHei Light" Font-Size="14pt" ForeColor="#262627" Visible="false" Text="* Não foi encontrada nenhuma conta."></asp:Label>
                                <asp:DataGrid ID="dtgPlanoConta" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                    EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                    HorizontalAlign="Left" PageSize="6" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                                    <Columns>
                                        <asp:BoundColumn DataField="Id_Fatura_Plano_Conta" HeaderText="Id_Fatura_Plano_Conta" Visible="False">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Id_Empresa" HeaderText="Id_Empresa" Visible="False">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Id_Contrato" HeaderText="Id_Contrato" Visible="False">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:TemplateColumn HeaderText="Conta">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNumeroFatura" runat="server" MaxLength="50" Text="<%# Bind('Nr_Plano_Conta') %>" CssClass="configtext"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Empresa">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cboEmpresa_Grid" runat="server" CssClass="configCombo" EnableTheming="True"></asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Contrato">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cboContrato_Grid" runat="server" CssClass="configCombo" Style="width: 100%" EnableTheming="True"></asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle Width="160px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Venc">
                                            <ItemTemplate>
                                                <cc1:MaskedEditExtender ID="meeDia_Vencimento" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99" TargetControlID="txtDia_Vencimento"></cc1:MaskedEditExtender>
                                                <asp:TextBox ID="txtDia_Vencimento" runat="server" Width="40px" MaxLength="10" Text="<%# Bind('Dia_Vencimento') %>" CssClass="configtext"></asp:TextBox>
                                                <cc1:MaskedEditValidator ID="mevDia_Vencimento" runat="server" ControlExtender="meeDia_Vencimento" ControlToValidate="txtDia_Vencimento" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDia_Vencimento" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE">*</cc1:MaskedEditValidator>
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Canc.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLote_Cancelamento" runat="server" Width="100%" MaxLength="10" Text="<%# Bind('Lote_Cancelamento') %>" CssClass="configtext"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="meeLote_Cancelamento" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtLote_Cancelamento"></cc1:MaskedEditExtender>
                                                <cc1:MaskedEditValidator ID="mevLote_Cancelamento" runat="server" ControlExtender="meeLote_Cancelamento" ControlToValidate="txtLote_Cancelamento" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevLote_Cancelamento" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE">*</cc1:MaskedEditValidator>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Descrição">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" Height="50px" MaxLength="100" TextMode="MultiLine" Width="100%" Text="<%# Bind('Descricao') %>"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btDesativa" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Deletar.png" OnClientClick="return confirm('Desativa Registro?');" OnClick="btDesativa_Click" Height="25px" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

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
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" OnClick="btSalvar_Click">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAdicionar" runat="server" CssClass="btn-menu-toolbar" OnClick="btAdicionar_Click">
            <i class="fas fa-plus-circle"></i>
            <br />
            <span>Add</span>
        </asp:LinkButton>
    </div>

</asp:Content>
