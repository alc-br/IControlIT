<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Exportacao_RH.aspx.vb" Inherits="IControlIT.Exportacao_RH" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Configucao-->
    <div id="pnlConfiguracao" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <table style="width: 100%;" align="center">
                        <tr>
                            <td style="width: 100%;">
                                <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Configuração" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px"></td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%; height: 160px; border: 1px solid #818181; border-radius: 4px; padding: 5px;">
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblExemplo1" runat="server" Width="100%" Style="line-height: 17px;" Text="Formato 1 - Exemplo: (#000matricula$000valorFI000FilialCC000Centro_Custo) . 0 = Caractere com maximo de 50 casas onde nos campos “Matricula”  e “Valor” esse caractere fica a esquerda e nos campos “Filial” e “Centro_Custo” o caractere fica a direita. Onde pra iniciar o campo a necessário dos caracteres especiais “#,$,FI,CC”."></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblExemplo2" runat="server" Width="100%" Style="line-height: 17px" Text="Formato 2 - matricula;valor;filial;centro_custo (Formato CSV)"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td></td>

                                        <td align="right" runat="server">
                                            <asp:Button ID="btFechaConfig" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
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

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblScript" runat="server" CssClass="configlabel" Text="Script"></asp:Label>
                            <asp:TextBox ID="txtScript" runat="server" CssClass="configtext" MaxLength="200" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvScript" runat="server" ControlToValidate="txtScript" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-left">
                            <asp:Label ID="lblOpcao" runat="server" CssClass="configlabel" Text="Opção de Exportação "></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:RadioButtonList ID="optOpcao" runat="server" CssClass="configchekbox" Style="float: none" RepeatDirection="Horizontal" Font-Bold="false">
                                <asp:ListItem Value="1" Selected="True">Lote fechado pelo usuário</asp:ListItem>
                                <asp:ListItem Value="2">Excedente de política em lote aberto a mais de 3 meses</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblArquivosExportados" runat="server" CssClass="configlabel" Text="Arquivos Exportados"></asp:Label>
                            <asp:ListBox ID="lstViewArquivoConsulta" runat="server" Style="float: left" CssClass="configlistboxAbrir" Width="100%" Height="123px" AutoPostBack="True"></asp:ListBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblViewArquivoExportacao" runat="server" CssClass="configlabel" Text="Arquivo para Exportação"></asp:Label>
                            <div id="DivArquivo" runat="server" style="border: 1px solid #C0C0C0; z-index: 103; left: 5px; overflow: auto; width: 100%; top: 103px; height: 184px" title=" ">
                                <asp:DataGrid ID="dtgArquivo" runat="server" AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="471px" BorderWidth="1px" GridLines="Horizontal">
                                    <PagerStyle Mode="NumericPages" />
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" />

                                    <Columns>
                                        <asp:BoundColumn DataField="Descricao" HeaderText="Descrição" Visible="True">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                    </Columns>

                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#EEEEEE" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 928px; position: absolute; top: 688px;">
        <cc1:ValidatorCalloutExtender ID="vceScript" runat="server" TargetControlID="rfvScript"></cc1:ValidatorCalloutExtender>
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
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" OnClick="btSalvar_Click">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btConfiguracao" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btConfiguracao_Click">
            <i class="fas fa-cog"></i>
            <br />
            <span>Config</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btExportar_Click">
            <i class="fas fa-file-upload"></i>
            <br />
            <span>Export</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExecutar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btExecutar_Click">
            <i class="fas fa-play-circle"></i>
            <br />
            <span>Executar</span>
        </asp:LinkButton>
    </div>

</asp:Content>
