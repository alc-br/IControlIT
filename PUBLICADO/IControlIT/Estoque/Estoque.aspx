<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Estoque.aspx.vb" Inherits="IControlIT.Estoque" EnableEventValidation="true" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div runat="server" title=" ">
        <style type="text/css">
            .ajax__tab_xp .ajax__tab_body {
                font-family: Arial;
                font-size: 10pt;
                border-top: 0;
                border: 1px solid #999999;
                padding: 8px;
                background-color: transparent;
            }
        </style>
    </div>

    <!--desativa aparelho-->
    <div id="pnlDetalhe" runat="server" class="bgModal" visible="False">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDesativacaoAparelho" runat="server" CssClass="configlabel" ForeColor="#999999" Text="Desativação de Equipamento" Style="float: none; left: 5px; position: relative; top: 0px;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblObservacao" runat="server" CssClass="configlabel" ForeColor="White" Style="z-index: 113;" Text="Observação"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvAte" runat="server" ControlToValidate="txtObservacao" Font-Names="Arial" Font-Size="10pt" Style="left: 445px; top: 38px; float: left;" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtObservacao" runat="server" CssClass="configtext" Height="63px" MaxLength="300" TextMode="MultiLine" Width="100%" TabIndex="7"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="btCancela" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btOk" class="btn btn-success" runat="server" Text="Desativar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--estoque regulador-->
    <div id="pnlMsg" runat="server" class="bgModal" visible="False">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescricaoRegulador" runat="server" CssClass="configlabel" ForeColor="#999999" Text="Estoque Regulador" Style="float: none; left: 5px; position: relative; top: 0px;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12">
                    <div id="DivCustoFixo" runat="server" style="overflow: auto; width: 100%; height: 188px;" title=" ">
                        <asp:DataGrid ID="dtgLista" runat="server" AutoGenerateColumns="False" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Calibri"
                            Font-Overline="False" Font-Size="10pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC"
                            HorizontalAlign="Left" PageSize="1" Width="100%" CellPadding="0" BorderColor="Silver" BorderWidth="1px" GridLines="Horizontal">

                            <Columns>
                                <asp:BoundColumn DataField="Descricao" HeaderText="Descrição"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Qtd" HeaderText="Qtde">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                            </Columns>

                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#EEEEEE" />

                        </asp:DataGrid>
                    </div>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFecharRegulador" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
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
                        <div class="col-md-5">
                            <asp:Label ID="Label1" runat="server" CssClass="configlabel" Text="Estoque" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <asp:DropDownList ID="cboConsumidor" runat="server" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                        </div>
                        <div class="col-md-5">
                            <asp:Label ID="Label2" runat="server" CssClass="configlabel" Text="Tipo" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                            <asp:DropDownList ID="cboTipo" runat="server" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <div style="width: 100%; height: 100%; position: relative;">
                                <asp:Button ID="btExecutar" class="btn btn-success" runat="server" Style="position: absolute; right: 0; bottom: -5px" Text="Abrir" CausesValidation="False" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <asp:Button ID="btnSolicitacao" runat="server" class="btn-tab pull-left" Text="Solicitação" CausesValidation="False" OnClick="btnSolicitacao_Click" />
                    <asp:Button ID="btnEstoque" runat="server" class="btn-tab-disable pull-left" Text="Estoque" CausesValidation="False" OnClick="btnEstoque_Click" />
                    <asp:Button ID="btnAtivo" runat="server" class="btn-tab-disable pull-left" Text="Ativo" CausesValidation="False" OnClick="btnAtivo_Click" />
                    <asp:Button ID="btnAssistencia" runat="server" class="btn-tab-disable pull-left" Text="Assistência" CausesValidation="False" OnClick="btnAssistencia_Click" />
                    <asp:Button ID="btnDevolucao" runat="server" class="btn-tab-disable pull-left" Text="Devolução" CausesValidation="False" OnClick="btnDevolucao_Click" />
                    <asp:Button ID="btnDescarte" runat="server" class="btn-tab-disable pull-left" Text="Descarte" CausesValidation="False" OnClick="btnDescarte_Click" />
                    <asp:HiddenField ID="hdfTab" runat="server" />
                    <asp:HiddenField ID="hfdId_Aparelho" runat="server" />
                </div>
            </nav>
            <div class="card-body" style="background-color: #FFFFFF; border-radius: 6px; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px">

                <!--Solicitacao-->
                <div id="divSolicitacaoMenu" runat="server" class="row" visible="true">
                    <div class="col-md-12">
                        <div id="divSolicitacao" runat="server" style="overflow: auto; width: 100%; max-height: 420px;">
                            <asp:DataGrid ID="dtgSolicitacao" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px">

                                <Columns>
                                    <asp:TemplateColumn HeaderText="Equipamento">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNumeroAparelho" runat="server" MaxLength="50" Text="<%# Bind('Nr_Aparelho') %>" CssClass="configtext"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="Nm_Aparelho_Tipo" HeaderText="Tipo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Ativo_Modelo" HeaderText="Modelo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Conglomerado" HeaderText="Fornecedor">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:TemplateColumn HeaderText="Pedido">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPedido" runat="server" CssClass="configtext" MaxLength="50" Text="<%# Bind('Nr_Pedido') %>"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="Id_Aparelho" HeaderText="Id_Aparelho" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:TemplateColumn HeaderText="Nota Fiscal">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNotaFiscal" runat="server" MaxLength="50" CssClass="configtext" Text="<%# Bind('Nr_Nota_Fiscal') %>"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="DC_Nr_Nota_Fiscal" HeaderText="Conta">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nr_Chamado" HeaderText="Chamado">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Dt_Chamado" HeaderText="Data">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Estoque_Aparelho_Status" HeaderText="Status">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>
                                </Columns>

                                <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="#E0E0E0" />
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>

                <!--Estoque-->
                <div id="divEstoqueMenu" runat="server" class="row" visible="false">
                    <div class="col-md-12">
                        <div id="divEstoque" runat="server" style="overflow: auto; width: 100%; max-height: 420px;">
                            <asp:DataGrid ID="dtgEstoque" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px">

                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btDevolucao" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Devolucao.png" OnClick="btDevolucao_Click" OnClientClick="return confirm('Envia equipamento para controle de devolução?');" Height="30px" ToolTip="Devolução" />
                                            <asp:ImageButton ID="btAssistencia" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Ferramenta.png" OnClick="btAssistencia_Click" OnClientClick="return confirm('Envia equipamento para assistência?');" Height="30px" ToolTip="Assistência Técnica" />
                                            <asp:ImageButton ID="btDesativa" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Deletar.png" OnClick="btDesativa_Click" OnClientClick="return confirm('Desativar Registro?');" Height="30px" ToolTip="Descarte" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="48px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Ativo">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNumeroFatura" runat="server" MaxLength="50" Text="<%# Bind('Nr_Ativo') %>" CssClass="configtext"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="Nr_Aparelho" HeaderText="Equipamento">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Aparelho_Tipo" HeaderText="Tipo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Ativo_Modelo" HeaderText="Modelo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Conglomerado" HeaderText="Fornecedor">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Id_Aparelho" HeaderText="Id_Aparelho" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Id_Ativo" HeaderText="Id_Ativo" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Usuario" HeaderText="Usuario" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Estoque_Aparelho_Status" HeaderText="Status">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Nr_Ativo" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>
                                </Columns>

                                <PagerStyle Mode="NumericPages" />
                                <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                <AlternatingItemStyle BackColor="#E0E0E0" />

                            </asp:DataGrid>
                        </div>
                    </div>
                </div>

                <!--Ativo-->
                <div id="divAtivoMenu" runat="server" class="row" visible="false">
                    <div class="col-md-12">
                        <div id="divAtivo" runat="server" style="overflow: auto; width: 100%; max-height: 420px;">
                            <asp:DataGrid ID="dtgAtivo" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px">

                                <Columns>
                                    <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Aparelho_Tipo" HeaderText="Tipo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Conglomerado" HeaderText="Conglomerado">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Id_Aparelho" HeaderText="Id_Aparelho" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Id_Ativo" HeaderText="Id_Ativo" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Usuario" HeaderText="Usuario" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btDesativa_Usuario_Ativo" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Estoque.png" OnClick="btDesativa_Usuario_Ativo_Click" OnClientClick="return confirm('Desativar Usuário Gestor do Estoque?');" Height="32px" ToolTip="Desativar Gestor de Estoque" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="Status_Aparelho" HeaderText="Status">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Dt_Suspensao" HeaderText="Suspensão">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Tempo_Parado" HeaderText="Parado no Estoque">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                </Columns>

                                <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="#E0E0E0" />
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>

                <!--Assistência-->
                <div id="divAssistenciaMenu" runat="server" class="row" visible="false">
                    <div class="col-md-12">
                        <div id="divAssistencia" runat="server" style="overflow: auto; width: 100%; max-height: 420px;">
                            <asp:DataGrid ID="dtgAssistencia" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px">

                                <Columns>
                                    <asp:BoundColumn DataField="Nr_Aparelho" HeaderText="Equipamento">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Aparelho_Tipo" HeaderText="Tipo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Ativo_Modelo" HeaderText="Modelo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Conglomerado" HeaderText="Conglomerado">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEstoque" CssClass="configchekbox" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="Id_Aparelho" HeaderText="Id_Aparelho" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn
                                        DataField="Id_Ativo" HeaderText="Id_Ativo" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Usuario" HeaderText="Usuario" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Status_Aparelho" HeaderText="Status">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>
                                </Columns>

                                <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="#E0E0E0" />
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>

                <!--Assistência-->
                <div id="divDevolucaoMenu" runat="server" class="row" visible="false">
                    <div class="col-md-12">
                        <div id="divDevolucao" runat="server" style="overflow: auto; width: 100%; max-height: 420px;">
                            <asp:DataGrid ID="dtgDevolucao" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px">

                                <Columns>
                                    <asp:BoundColumn DataField="Nr_Aparelho" HeaderText="Equipamento">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Aparelho_Tipo" HeaderText="Tipo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Ativo_Modelo" HeaderText="Modelo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Conglomerado" HeaderText="Conglomerado">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEstoque" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="Id_Aparelho"
                                        HeaderText="Id_Aparelho" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Id_Ativo" HeaderText="Id_Ativo" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Consumidor" HeaderText="Colaborador">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Usuario" HeaderText="Usuário">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Status_Usuario" HeaderText="Status">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="EMail_Consumidor" HeaderText="E-Mail">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Matricula_Chefia" HeaderText="Matrícula Chefia">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btTermo" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Word.png" OnClientClick="<%# Bind('Termo_Devolucao') %>" ToolTip="Anexar PDF" Width="32px" Height="32px" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>
                                </Columns>

                                <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="#E0E0E0" />

                            </asp:DataGrid>
                        </div>
                    </div>
                </div>

                <!--Descarte-->
                <div id="divDescarteMenu" runat="server" class="row" visible="false">
                    <div class="col-md-12">
                        <div id="divDesativacao" runat="server" style="overflow: auto; width: 100%; max-height: 420px;">
                            <asp:DataGrid ID="dtgDesativado" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px">

                                <Columns>
                                    <asp:BoundColumn DataField="Nr_Aparelho" HeaderText="Equipamento">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Aparelho_Tipo" HeaderText="Tipo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Ativo_Modelo" HeaderText="Modelo">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Nm_Conglomerado" HeaderText="Conglomerado">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAtivar" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="Id_Aparelho" HeaderText="Id_Aparelho" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Id_Ativo" HeaderText="Id_Ativo" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Status_Aparelho" HeaderText="Status">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="Observacao" HeaderText="Observação">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>

                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btPDF_Ativo" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/pdf_Grid.png" OnClientClick="<%# Bind('PDF_Ativo') %>" ToolTip="Anexar PDF" Width="32px" Height="32px" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>

                                </Columns>

                                <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="#E0E0E0" />

                            </asp:DataGrid>
                        </div>
                    </div>
                </div>

                <!---->
                <div>&nbsp;</div>
                <div style="width: 100%; height: 1px; background-color: #818181"></div>
                <div>&nbsp;</div>
                <div class="row">
                    <div class="col-md-12">
                        <div style="height: 40px; padding: 2px 10px 2px 20px; background-color: #E0E0E0; border: 1px solid #E0E0E0; border-radius: 25px">
                            <div class="input-group no-border">
                                <asp:TextBox ID="txtPesquisaEquipamento" placeholder="Pesquisar..." runat="server" MaxLength="50" CssClass="form-control" ForeColor="#818181"></asp:TextBox>
                                <asp:LinkButton ID="btPesquisar" runat="server" CssClass="nav-link" OnClick="btPesquisar_Click"><i class="material-icons">search</i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div>&nbsp;</div>
                <div class="row">
                    <div class="col-md-4">
                        <asp:TextBox ID="txtQuantidadeSolicitacao" runat="server" CssClass="configtext" Style="text-align: right" Width="100%" Height="25px" ReadOnly="True" Font-Size="12pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="White"></asp:TextBox>
                        <asp:Label ID="lblDescQuantidadeSolicitacao" runat="server" Style="text-align: right; vertical-align: middle; border-radius: 2px; padding: 5px" Width="100%" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Solicitação" BackColor="Silver" ForeColor="White"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtQuantidadeEstoque" runat="server" CssClass="configtext" Style="text-align: right" Width="100%" Height="25px" ReadOnly="True" Font-Size="12pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="White"></asp:TextBox>
                        <asp:Label ID="lblDescQuantidadeEstoque" runat="server" Style="text-align: right; vertical-align: middle; border-radius: 2px; padding: 5px" Width="100%" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Estoque" BackColor="Silver" ForeColor="White"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtQuantidadeAtivo" runat="server" CssClass="configtext" Style="text-align: right" Width="100%" ReadOnly="True" Height="25px" Font-Size="12pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="White"></asp:TextBox>
                        <asp:Label ID="lblDescQuantidadeAtivo" runat="server" Style="text-align: right; vertical-align: middle; border-radius: 2px; padding: 5px" Width="100%" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Ativo" BackColor="Silver" ForeColor="White"></asp:Label>
                    </div>
                </div>
                <div style="height: 5px"></div>
                <div class="row">
                    <div class="col-md-4">
                        <asp:TextBox ID="txtQuantidadeAssistencia" runat="server" CssClass="configtext" Style="text-align: right" Width="100%" ReadOnly="True" Height="25px" Font-Size="12pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="White"></asp:TextBox>
                        <asp:Label ID="lblDescQuantidadeAssistencia" runat="server" Style="text-align: right; vertical-align: middle; border-radius: 2px; padding: 5px" Width="100%" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Assistência" BackColor="Silver" ForeColor="White"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtQuantidadeDevolucao" runat="server" CssClass="configtext" Style="text-align: right" Width="100%" ReadOnly="True" Height="25px" Font-Size="12pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="White"></asp:TextBox>
                        <asp:Label ID="lblDescQuantidadeDevolucao" runat="server" Style="text-align: right; vertical-align: middle; border-radius: 2px; padding: 5px" Width="100%" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Devolução" BackColor="Silver" ForeColor="White"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtQuantidadeDesativado" runat="server" CssClass="configtext" Style="text-align: right" Width="100%" ReadOnly="True" Height="25px" Font-Size="12pt" Font-Names="Microsoft JhengHei Light" BorderStyle="Solid" BorderWidth="1px" BorderColor="White"></asp:TextBox>
                        <asp:Label ID="lblDescQuantidadeDesativado" runat="server" Style="text-align: right; vertical-align: middle; border-radius: 2px; padding: 5px" Width="100%" Font-Names="Microsoft JhengHei Light" Font-Size="11pt" Text="Descarte" BackColor="Silver" ForeColor="White"></asp:Label>
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
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" OnClick="btExportar_Click">
            <i class="fas fa-upload"></i>
            <br />
            <span>Export</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSolicitação" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-mouse"></i>
            <br />
            <span>Equip</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btNotaFiscal" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-invoice-dollar"></i>
            <br />
            <span>Nota</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btEmail" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClientClick="return confirm('Enviar e-mail notificando da devolução?');" OnClick="btEmail_Click">
            <i class="fas fa-envelope"></i>
            <br />
            <span>Email</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAlerta" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAlerta_Click">
            <i class="fas fa-exclamation-triangle"></i>
            <br />
            <span>Alerta</span>
        </asp:LinkButton>
    </div>

</asp:Content>


