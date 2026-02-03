<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Ativo_Alerta.aspx.vb" Inherits="IControlIT.Ativo_Alerta" %>

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
                            <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Ativo para Suspenssão" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
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
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblProgramarAlerta" runat="server" Font-Bold="False" Visible="false" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Programar Alerta" Style="float: none"></asp:Label>
                            <div class="input-group no-border">
                                <asp:TextBox ID="txtLocalizar" placeholder="Pesquise Contato" runat="server" Font-Names="Calibri Light" ForeColor="Gray" Height="25px" MaxLength="50" Style="float: left; text-align: left; left: 10px; position: relative; top: 0px; width: calc(100% - 30px);" TabIndex="1" BackColor="Transparent" Font-Bold="False" Font-Strikeout="False" BorderColor="Transparent" Font-Size="18pt"></asp:TextBox>
                                <asp:ImageButton ID="btLocalizar" runat="server" ImageUrl="~/Img_Sistema/Geral/Localiza_Pesquisa_Home.png" Width="26px" Height="26px" TabIndex="1" Style="position: relative; float: left; top: 2px; left: 5px;" />
                            </div>
                        </div>
                    </div>
                    <div style="height: 5px"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <div style="width: 100%; max-height: 214px; overflow: auto">
                                <asp:DataGrid ID="dtgLocaliza" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                    EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                    HorizontalAlign="Left" PageSize="3" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                                    <Columns>
                                        <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo"></asp:BoundColumn>

                                        <asp:BoundColumn DataField="Id_Ativo" HeaderText="Id_Ativo" Visible="False"></asp:BoundColumn>

                                        <asp:TemplateColumn HeaderText="Observação">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" TextMode="MultiLine" Height="32px" Width="340px" MaxLength="8000"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Data Alerta">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAlerta" runat="server" MaxLength="10" CssClass="configtext" Style="background-color: transparent" Width="100px"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="meeAlerta" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtAlerta"></cc1:MaskedEditExtender>
                                                <cc1:MaskedEditValidator ID="mevAlerta" runat="server" ControlExtender="meeAlerta" ControlToValidate="txtAlerta" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevAlerta" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE">*</cc1:MaskedEditValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BackColor="White" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btAlerta" runat="server" Height="25px" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Add.png" Style="float: none;" TabIndex="2" OnClick="btAlerta_Click1" ToolTip="Criar Alerta" />
                                            </ItemTemplate>
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
                    <div style="height: 5px"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblAlertaProgramado" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Alerta Programado" Style="float: none"></asp:Label>
                            <div id="divInventario" runat="server" style="overflow: auto; width: 100%; height: 214px; background-color: #FFFFFF; border: 1px solid #999999;" title=" ">
                                <asp:DataGrid ID="dtgAlertaProgramado" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                                    <Columns>
                                        <asp:BoundColumn DataField="Observacao" HeaderText="Observação"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Data_Alerta" HeaderText="Data Alerta"></asp:BoundColumn>
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
    </div>

</asp:Content>
