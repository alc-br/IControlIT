<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Consulta_Contrato.aspx.vb" Inherits="IControlIT.Consulta_Contrato" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Msg *************************************************************************************** -->
    <div id="pnlMsg" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Contratos Vencidos" Style="float: none; left: 5px; position: relative; top: 0px;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div>&nbsp;</div>
            <div class="row">
                <div class="col-md-12">
                    <div id="DivCustoFixo" runat="server" style="overflow: auto; width: 100%;">
                        <asp:DataGrid ID="dtgLista" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                            <Columns>
                                <asp:BoundColumn DataField="Descricao" HeaderText="Descrição"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Qtd" HeaderText="Qtde">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:BoundColumn>
                            </Columns>

                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" />
                            <PagerStyle Mode="NumericPages" />

                        </asp:DataGrid>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col=md-12 text-right">
                    <asp:Button ID="btDFechar" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <asp:Button ID="btnHome" runat="server" class="btn-tab pull-left" Text="Capa" CausesValidation="False" OnClick="btnHome_Click" />
                    <asp:Button ID="btnProduto" runat="server" class="btn-tab-disable pull-left" Text="Serviço" CausesValidation="False" OnClick="btnProduto_Click" />
                    <asp:Button ID="btnSla" runat="server" class="btn-tab-disable pull-left" Text="SLA" CausesValidation="False" OnClick="btnSla_Click" />
                    <asp:Button ID="btnAditivo" runat="server" class="btn-tab-disable pull-left" Text="Aditivo" CausesValidation="False" OnClick="btnAditivo_Click" />
                    <asp:Button ID="btnContas" runat="server" class="btn-tab-disable pull-left" Text="Contas" CausesValidation="False" OnClick="btnContas_Click" />
                </div>
            </nav>
            <div class="card-body" style="background-color: #FFFFFF; border-radius: 6px; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px">

                <!--Capa-->
                <div id="divCapa" runat="server" visible="true" class="row">
                    <div class="col-md-12">
                        <div class="card-body" style="box-shadow: none;">
                            <div class="row">
                                <div class="col-md-12">
                                    <table style="width: 100%; height: 350px;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 24px">
                                                            <asp:LinkButton ID="btCapa" runat="server" CssClass="nav-link">
                                                                <i class="far fa-plus-square" style="font-size: 24pt"></i>
                                                            </asp:LinkButton>
                                                            <asp:HiddenField ID="hfdID" runat="server" />
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblCapaContrato" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Capa do Contrato" Style="float: none"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 680px">
                                                            <asp:Label ID="lblSServico" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="vertical-align: top">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        <asp:Label ID="lblContrato" runat="server" CssClass="configlabel" Text="Número.:" ForeColor="#9CCC2A"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtNumeroContrato" runat="server" CssClass="configlabel" Style="float: left;"></asp:Label>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        <asp:Label ID="lblContratada" runat="server" CssClass="configlabel" Text="Contratada.:" ForeColor="#9CCC2A"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtSEmpresa" runat="server" CssClass="configlabel" Style="float: left;" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        <asp:Label ID="lblContratante" runat="server" CssClass="configlabel" Text="Contratante.:" ForeColor="#9CCC2A"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtSContratante" runat="server" CssClass="configlabel" Style="float: left;" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        <asp:Label ID="lblFilial" runat="server" ForeColor="#9CCC2A" CssClass="configlabel" Text="Filial.:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtSFilial" runat="server" CssClass="configlabel" Style="float: left;" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: Left; height: 40px">
                                                            <asp:Label ID="lblInformacao" runat="server" Text="Informação" Font-Bold="False" Font-Names="Verdana" Font-Size="12pt" ForeColor="#666666"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 110px;">
                                                                        <asp:Label ID="lblStatus" runat="server" ForeColor="#9CCC2A" CssClass="configlabel" Text="Status.:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtSStatus" runat="server" CssClass="configlabel" Style="float: left;" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 110px;">
                                                                        <asp:Label ID="lblDataInicioVigencia" ForeColor="#9CCC2A" runat="server" CssClass="configlabel" Text="Data do inicio.:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtSInicioVigencia" runat="server" CssClass="configlabel" Style="float: left;" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 120px;">
                                                                        <asp:Label ID="lblDataFimVigencia" runat="server" ForeColor="#9CCC2A" CssClass="configlabel" Text="Data do Termino.:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtSFimVigencia" runat="server" CssClass="configlabel" Style="float: left;" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: Left; height: 40px">
                                                            <asp:Label ID="lblObjetoContrato" runat="server" Text="Objeto" Font-Bold="False" Font-Names="Verdana" Font-Size="12pt" ForeColor="#666666"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="txtDescricao" runat="server" CssClass="configlabel" Style="float: left;" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="txtsObjeto" runat="server" CssClass="configlabel" Style="float: left;" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--Serviço-->
                <div id="divServico" runat="server" visible="false" class="row">
                    <div class="col-md-12">
                        <div class="card-body" style="box-shadow: none;">
                            <div class="row">
                                <div class="col-md-12">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 24px">
                                                <asp:LinkButton ID="btProduto" runat="server" CssClass="nav-link">
                                                    <i class="far fa-plus-square" style="font-size: 24pt"></i>
                                                </asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblProduto" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Produtos e Serviços Contratados" Style="float: none"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 overflow-auto">
                                    <asp:DataGrid ID="dtgProduto" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px" AllowPaging="false">

                                        <Columns>

                                            <asp:BoundColumn DataField="Id_Contrato_SLA_Servico" Visible="False">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Descrição">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDescricaoProduto" runat="server" CssClass="configtext" Style="height: 15px" ReadOnly="True" Width="100%" Text="<%# Bind('Descricao') %>" BorderColor="Transparent" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Tipo_Servico" HeaderText="Tipo" Visible="True">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Valor" HeaderText="Valor" Visible="True">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle HorizontalAlign="left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Ação" ItemStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnRedirect" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_View.png" OnClick="btnRedirect_Click" CommandArgument='<%# Eval("Id_Contrato_SLA_Servico") %>' Height="25px" CausesValidation="False" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn> 

                                        </Columns>

                                        <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Left" />
                                        <%--<PagerStyle Mode="NumericPages" />--%>
                                        <AlternatingItemStyle BackColor="#E0E0E0" />

                                    </asp:DataGrid>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--SLA-->
                <div id="divSla" runat="server" visible="false" class="row">
                    <div class="col-md-12">
                        <div class="card-body" style="box-shadow: none;">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 24px">
                                                    <asp:LinkButton ID="btSLA" runat="server" CssClass="nav-link">
                                                    <i class="far fa-plus-square" style="font-size: 24pt"></i>
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSLA" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="SLA" Style="float: none"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 overflow-auto">
                                    <asp:DataGrid ID="dtgSLA" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px" AllowPaging="True">

                                        <Columns>
                                            <asp:BoundColumn DataField="Id_Contrato_SLA_Operacao" Visible="False">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Descrição">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDescricaoSLA" runat="server" CssClass="configtext" Height="32px" MaxLength="100" Text="<%# Bind('Descricao') %>" ReadOnly="True" TextMode="MultiLine" Width="300px" BorderColor="Transparent" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Prazo" HeaderText="Prazo" Visible="True">
                                                <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Valor" HeaderText="Valor" Visible="True">
                                                <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                        </Columns>

                                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Left" />
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

                <!--Aditivo-->
                <div id="divAditivo" runat="server" visible="false" class="row">
                    <div class="col-md-12">
                        <div class="card-body" style="box-shadow: none;">
                            <div class="row">
                                <div class="col-md-12">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 24px">
                                                <asp:LinkButton ID="btAditivo" runat="server" CssClass="nav-link">
                                                    <i class="far fa-plus-square" style="font-size: 24pt"></i>
                                                </asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAditivo" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Aditivos" Style="float: none"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:DataGrid ID="dtgAditivo" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px" AllowPaging="True">
                                        <Columns>
                                            <asp:BoundColumn DataField="Id_Contrato_Aditivo" Visible="False">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Descrição">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDescricaoAditivo" runat="server" CssClass="configtext" MaxLength="100" Text="<%# Bind('Descricao') %>" ReadOnly="True" TextMode="MultiLine" Width="100%" BorderColor="Transparent" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Dt_Vigencia" HeaderText="Vigência" Visible="True">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                        </Columns>

                                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Left" />
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

                <!--Contas-->
                <div id="divContas" runat="server" visible="false" class="row">
                    <div class="col-md-12">
                        <div class="card-body" style="box-shadow: none;">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblConta" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Conta(s) Vinculada(s) ao Contrato" Style="float: none"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:DataGrid ID="dtgConta" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px" AllowPaging="True">

                                        <Columns>

                                            <asp:BoundColumn DataField="Conta" HeaderText="Conta" Visible="True">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Tipo" HeaderText="Tipo" Visible="True">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="QTD" HeaderText="Qtde" Visible="True">
                                                <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Cancelamento" HeaderText="Cancelada" Visible="True">
                                                <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Conta" HeaderText="Nr_Conta" Visible="False">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Id_Ativo_Tipo" HeaderText="Id_Ativo_Tipo" Visible="False">
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            </asp:BoundColumn>

                                        </Columns>

                                        <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Left" />
                                        <PagerStyle Mode="NumericPages" />
                                        <AlternatingItemStyle BackColor="#E0E0E0" />

                                    </asp:DataGrid>
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
        <asp:LinkButton ID="btPDF" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span runat="server">Anexos</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAlerta" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAlerta_Click">
            <i class="fas fa-exclamation-triangle"></i>
            <br />
            <span>Alerta</span>
        </asp:LinkButton>
    </div>

</asp:Content>



