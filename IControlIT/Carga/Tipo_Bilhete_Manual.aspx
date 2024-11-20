<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Tipo_Bilhete_Manual.aspx.vb" Inherits="IControlIT.Tipo_Bilhete_Manual" %>

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
                            <div class="panel panel-warning">
                                <div class="panel-heading row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblMontaHierarquia" runat="server" CssClass="configlabel" Text="Relacionamento (Filial)" Font-Bold="False"></asp:Label>
                                        <div class="input-group no-border">
                                            <asp:TextBox ID="txtGrupo" runat="server" CssClass="configtext" Style="width: calc(100% - 36px)" BackColor="Transparent"></asp:TextBox>
                                            <asp:ImageButton ID="btGrupo" runat="server" ImageUrl="~/Img_Sistema/Botao/bt_Combo.png" Style="border-style: none; font-size: 10pt; font-family: Arial; background-color: transparent; z-index: 108;" ToolTip="Pesquisar" Width="30px" Height="30px" CausesValidation="False" OnClick="btGrupo_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div style="height: 5px"></div>
                                <div class="row">
                                    <div class="col-md-5">
                                        <asp:ListBox ID="lstOrigem" runat="server" CssClass="configlistboxAbrir" ForeColor="Black" Height="90px" Width="100%"></asp:ListBox>
                                    </div>
                                    <div class="col-md-2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td runat="server" align="center">
                                                    <table>
                                                        <tr>
                                                            <td style="height: 12px;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btMoveSelecionado" runat="server" ImageUrl="~/Img_Sistema/Botao/bt_Combo_Direita.png" Style="border-style: none; font-size: 8pt; font-family: Arial; top: auto; background-color: transparent; z-index: 108;" ToolTip="Mover" Width="24px" CausesValidation="False" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btMoveSelecao" runat="server" ImageUrl="~/Img_Sistema/Botao/bt_Combo_Esquerda.png" Style="border-style: none; font-size: 8pt; font-family: Arial; top: auto; background-color: transparent; z-index: 108;" ToolTip="Mover" Width="24px" CausesValidation="False" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 12px;"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:ListBox ID="lstDestino" runat="server" CssClass="configlistboxAbrir" ForeColor="Black" Height="90px" Width="100%"></asp:ListBox>
                                    </div>
                                </div>
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
    </div>

</asp:Content>
