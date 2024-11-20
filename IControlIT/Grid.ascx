<%@ Control Language="VB" AutoEventWireup="false" CodeBehind="Grid.ascx.vb" Inherits="IControlIT.Grid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<link href="CSSConfigObj.CSS" rel="Stylesheet" />
<link href="CSSEstruturalMaster.css" rel="Stylesheet" />
<link rel="Shortcut Icon" href="Img_Sistema/logo.ico" />

<!--filtro ********************************************************************************** -->
<div id="pnlMsg" runat="server" class="bgModal" visible="false">
    <div class="modalPopup">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblCLassificar" runat="server" CssClass="configlabel" Text="Classificar" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
            </div>
        </div>
        <div style="height: 5px"></div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblClassificarPor" runat="server" Text="Classificar Por" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt" ForeColor="#818181"></asp:Label>
                <asp:DropDownList ID="cboColuna" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%"></asp:DropDownList>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblOrderm" runat="server" Text="Ordem" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt" ForeColor="#818181"></asp:Label>
                <asp:DropDownList ID="cboOrdernacao" runat="server" CssClass="configCombo" EnableTheming="True" Style="width: 100%;">
                    <asp:ListItem Value="1">De A a Z</asp:ListItem>
                    <asp:ListItem Value="2">De Z a A</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTipo1" runat="server" Text="Filtro de Texto" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt" ForeColor="#818181"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvColuna" runat="server" ControlToValidate="cboColuna" Display="None" Enabled="False" Font-Names="Arial" Font-Size="8pt" SetFocusOnError="True" ForeColor="Red" Style="float: left;">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtOrdenacao" runat="server" CssClass="configtext" BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Width="100%"></asp:TextBox>
            </div>
        </div>
        <div style="height: 5px"></div>
        <div class="row">
            <div class="col-md-12 text-right">
                <asp:Button ID="btFechar" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                <asp:Button ID="BtOk" class="btn btn-success" runat="server" Text="Mostrar" CausesValidation="False" />
            </div>
        </div>
    </div>
</div>

<div id="DivMatrix" runat="server" class="row">
    <div id="DivGrid" runat="server" class="col-md-12">
        <table>
            <tr>
                <td style="padding: 2px">
                    <asp:LinkButton ID="BtExportar" runat="server" aria-label="Exportar" data-balloon-pos="up" OnClick="BtExportar_Click">
                            <i class="fas fa-download" style="font-size: 16pt"></i>
                    </asp:LinkButton>
                </td>
                <td style="padding: 2px">
                    <asp:LinkButton ID="BtOrdenar" runat="server" aria-label="Filtro" data-balloon-pos="up" OnClick="BtOrdenar_Click">
                            <i class="fas fa-filter" style="font-size: 16pt"></i>
                    </asp:LinkButton>
                </td>
                <td style="padding: 2px">
                    <asp:LinkButton ID="BtRefresh" runat="server" aria-label="Atualizar" data-balloon-pos="up" OnClick="BtRefresh_Click">
                            <i class="fas fa-retweet" style="font-size: 16pt"></i>
                    </asp:LinkButton>
                </td>
                <td style="padding: 2px">
                    <asp:DropDownList ID="cboColuna_Calculo" runat="server" CssClass="configCombo" EnableTheming="True" Style="width: 140px; float: none" AutoPostBack="True"></asp:DropDownList>
                </td>
                <td style="padding: 2px">
                    <asp:LinkButton ID="btSomar" runat="server" aria-label="Auto Soma" data-balloon-pos="up" OnClick="btSomar_Click">
                            <i class="fas fa-plus" style="font-size: 16pt"></i>
                    </asp:LinkButton>
                </td>
                <td style="padding: 2px">
                    <asp:LinkButton ID="BtSubTotal" runat="server" aria-label="Sub Total" data-balloon-pos="up" OnClick="BtSubTotal_Click">
                            <i class="fas fa-equals" style="font-size: 16pt"></i>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</div>
<div style="height: 15px"></div>
<div class="row">
    <div class="col-md-12">
        <div id="DivCorpo" runat="server" style="border: 1px solid #FFFFFF; width: 100%; max-height: 450px; overflow: auto;">
            <asp:DataGrid ID="dtgMatrix" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="9pt" PageSize="50" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" CellPadding="5" CellSpacing="5" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                <PagerStyle Visible="False" />
                <AlternatingItemStyle BackColor="#E0E0E0" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="Black" />
            </asp:DataGrid>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12 text-right">
        <table style="width: 100%">
            <tr>
                <td style="width: 100%"></td>
                <td style="padding: 2px">
                    <asp:DropDownList ID="cboMostraLinha" runat="server" CssClass="configCombo" EnableTheming="True" Style="width: 60px; float: none" AutoPostBack="True"></asp:DropDownList>
                </td>
                <td style="padding: 2px">
                    <asp:Label ID="lblPagCont" runat="server" Font-Names="Arial" Font-Size="9pt" Height="15px" Style="text-align: center; height: 17px;" Width="25px" ForeColor="#333333">0</asp:Label>
                </td>
                <td style="padding: 2px">
                    <asp:Label ID="lblDe" runat="server" Font-Names="Arial" Font-Size="9pt" Height="15px" Style="text-align: center; height: 17px;" Width="25px" ForeColor="#333333">de</asp:Label>
                </td>
                <td style="padding: 2px">
                    <asp:Label ID="lblPag" runat="server" Font-Names="Arial" Font-Size="9pt" Height="15px" Style="text-align: center; height: 17px;" Width="25px" ForeColor="#333333">0</asp:Label>
                </td>
                <td style="padding: 2px">
                    <asp:LinkButton ID="btPrimeiro" runat="server" aria-label="Primeira" data-balloon-pos="up" OnClick="btPrimeiro_Click">
                            <i class="fas fa-step-backward" style="font-size: 16pt"></i>
                    </asp:LinkButton>
                </td>
                <td style="padding: 2px">
                    <asp:LinkButton ID="btAnterior" runat="server" aria-label="Anterior" data-balloon-pos="up" OnClick="btAnterior_Click">
                            <i class="fas fa-backward" style="font-size: 16pt"></i>
                    </asp:LinkButton>
                </td>
                <td style="padding: 2px">
                    <asp:LinkButton ID="btProximo" runat="server" aria-label="Próxima" data-balloon-pos="up" OnClick="btProximo_Click">
                            <i class="fas fa-forward" style="font-size: 16pt"></i>
                    </asp:LinkButton>
                </td>
                <td style="padding: 2px">
                    <asp:LinkButton ID="btUltimo" runat="server" aria-label="Última" data-balloon-pos="up" OnClick="btUltimo_Click">
                            <i class="fas fa-step-forward" style="font-size: 16pt"></i>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</div>


<asp:HiddenField ID="hfdDescricao" runat="server" />
<asp:HiddenField ID="hfdCampo" runat="server" />

<asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 29px; position: absolute; top: 272px; height: 38px;">
    <cc1:ValidatorCalloutExtender ID="vceColuna" runat="server" TargetControlID="rfvColuna">
    </cc1:ValidatorCalloutExtender>
    <asp:Label ID="lblMessage" runat="server" Style="left: 13px; top: 87px" Font-Names="Arial" Font-Size="9pt"></asp:Label>
</asp:Panel>

