<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Agenda_Particular.aspx.vb" Inherits="IControlIT.Agenda_Particular" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">

    <%-- trava o zoom quando visualizado no mobile e almenta a scala dos objetos na tela deixando responsivo --%>
    <meta name="viewport" content="width=device-width, initial-scale=0.7, maximum-scale=0.7, user-scalable=no" />

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.js"></script>
    <script type="text/javascript" src="https://www.chartjs.org/samples/latest/utils.js"></script>

    <style type="text/css">
        .bgGraficos {
            background-color: #EEEEEE;
            width: 100%;
            overflow: auto;
            padding: 60px 15px 15px 15px;
        }

        .cardGrafico {
            width: 100%;
            background-color: #FFFFFF;
            box-shadow: rgba(0,0,0,0.2) 0px 2px 8px 0px;
            border-radius: 10px;
            padding: 15px;
        }

        .tblResponsivo {
            width: 720px;
        }

        .txtPesquisa {
            float: left;
            text-align: left;
            left: 10px;
            width: 100%;
            padding-left: 5px;
            padding-right: 5px;
        }

        /*/ Responsivo para tela mobile /*/
        @media (min-width: 468px) and (max-width: 1024px) {

            .bgGraficos {
                background-color: #EEEEEE;
                width: 100%;
                overflow: auto;
                padding: 60px 15px 15px 15px;
            }

            .tblResponsivo {
                width: 100%;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Cabecario -->
    <div>&nbsp;</div>
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <table style="width: 100%; height: 30px;">
                                <tr>
                                    <td style="height: 40px; background-color: #EEEEEE; border-radius: 20px; padding-left: 5px; padding-right: 5px" align="center">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 22px">
                                                    <asp:ImageButton ID="btLocalizar" runat="server" ImageUrl="~/Img_Sistema/Geral/Localiza_Pesquisa_Home.png" Width="22px" Height="22px" TabIndex="1" Style="position: relative; float: left; top: 2px; left: 5px;" />
                                                </td>
                                                <td class="tblResponsivo">
                                                    <asp:TextBox ID="txtLocalizar" CssClass="txtPesquisa" placeholder="Pesquisar" runat="server" Font-Names="Calibri Light" ForeColor="Gray" Height="25px" MaxLength="50" TabIndex="1" BackColor="Transparent" Font-Bold="False" Font-Strikeout="False" BorderColor="Transparent" Font-Size="18pt"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div>&nbsp;</div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:DataGrid ID="dtgDiscado" runat="server" AutoGenerateColumns="False" EnableTheming="True" HorizontalAlign="Center" Style="font-size: 10pt; font-family: 'Segoe UI'" Width="100%"
                                ShowHeader="False" BorderColor="#F0F0F0" BorderWidth="0px" CellPadding="5" CellSpacing="5" AllowPaging="true" ForeColor="Black" PageSize="10" GridLines="None">

                                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Apontar">
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="height: 64px; width: 64px">
                                                                    <asp:Image ID="imgUser" runat="server" ImageUrl="~/Img_Sistema/Botao/ic_user.png" Height="56px" Style="border-radius: 50%; background-color: #E0E0E0; padding: 5px" />
                                                                </td>
                                                                <td style="width: 5px"></td>
                                                                <td>
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDescricaoDiscado" runat="server" CssClass="configtext" Style="font: 12pt Segoe UI Semibold; color: #818181" MaxLength="50" Width="100%" Text="<%# Bind('Nm_Destino') %>"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Agenda_Numero.png" Height="16px" ImageAlign="AbsMiddle" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label17" runat="server" Text="<%# Bind('Nr_Destino') %>" ForeColor="#818181" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center" style="width: 64px; padding-left: 10px">
                                                        <table style="width: 100%; height: 64px; padding-left: 10px">
                                                            <tr>
                                                                <td align="center">
                                                                    <table id="tblCheck" runat="server" style="background-image: url('../Content/skins/flat/blue.png'); background-repeat: no-repeat; background-position: 0px 3px; width: 22px; height: 22px">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:CheckBox ID="chkParticularDiscado" Style="opacity: 0" runat="server" Checked="<%# Bind('Tipo') %>" OnCheckedChanged="chkParticularDiscado_CheckedChanged" AutoPostBack="True" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label18" runat="server" Text="Particular" ForeColor="#818181" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>
                                </Columns>

                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" />
                                <PagerStyle Mode="NumericPages" Font-Names="Segoe UI Semibold" HorizontalAlign="Center" Font-Size="Large" />
                                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:DataGrid>
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
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" Visible="true" Style="opacity: 1;" Enabled="true" OnClick="btSalvar_Click">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
    </div>

</asp:Content>
