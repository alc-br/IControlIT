<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Home.aspx.vb" Inherits="IControlIT.Home" EnableEventValidation="true" %>

<%@ MasterType VirtualPath="~/Principal.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content runat="server" ID="Head" ContentPlaceHolderID="head">

    <style type="text/css">
        .liMenu {
            display: inline-block;
            vertical-align: middle;
            padding: 2px;
        }

        .btn-Selected {
            background-color: transparent;
        }

            .btn-Selected:hover {
                background-color: #EEEEEE;
            }

            .btn-Selected:active {
                background-color: #B2DFDB;
            }
    </style>

</asp:Content>

<asp:Content runat="Server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">

    <!--Confirmação-->
    <div id="pnlConfirmacao" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="configlabel" Text="Lixeira" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblMenssagem" runat="server" CssClass="configlabel" Text="Você deseja restaurar este registro?" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btCancela" class="btn btn-default" runat="server" Text="Não" CausesValidation="False" />
                    <asp:Button ID="btOk" class="btn btn-success" runat="server" Text="Sim" CausesValidation="False" />
                    <asp:HiddenField ID="hfdId_Aparelho" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div class="content">
        <div class="container-fluid">
            <!-- your content here -->
            <div id="divConteudo" runat="server" visible="true">
                <div id="divPesquisar" runat="server" class="row" visible="true">
                    <div class="col-md-12 text-center">
                        <h2 style="font-size: 20pt">Bem vindo ao nosso portal</h2>
                    </div>
                    <div class="col-md-12">
                        <div style="height: 40px; padding: 2px 10px 2px 20px; background-color: #FFF; border: 1px solid #E0E0E0; border-radius: 25px">
                            <div class="input-group no-border">
                                <asp:TextBox ID="txtPesquisar" type="text" placeholder="Basta Digitar para Localizar." runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:LinkButton ID="btPesquisar" runat="server" CssClass="nav-link" OnClick="btPesquisar_Click"><i class="material-icons">search</i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" style="height: 5px"></div>
                </div>

                <!--Pesquisa-->
                <div class="row">
                    <div class="col-md-12">
                        <div id="DivPesquisa" runat="server" visible="false" style="overflow: auto; width: 100%; max-height: 450px; padding: 15px; background-color: #FFFFFF; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px; border-radius: 4px">
                            <asp:DataGrid ID="dtgLocaliza" runat="server" AutoGenerateColumns="False" CellPadding="0" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Segoe UI" Font-Overline="False"
                                Font-Size="10pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" HorizontalAlign="Center" ShowHeader="False" Width="100%" AllowPaging="True" GridLines="None">

                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <PagerStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="15pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />
                                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <Columns>
                                    <asp:ButtonColumn CommandName="Select" DataTextField="Lixeira" ItemStyle-Width="30px" DataTextFormatString="&lt;img src=&quot;Img_Sistema/Botao/Grid/{0}&quot; border=&quot;0&quot; height=&quot;30px&quot; width=&quot;30px&quot;" Text="Lixeira">
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:ButtonColumn>

                                    <asp:TemplateColumn HeaderText="Descrição">
                                        <ItemTemplate>
                                            <table style="width: 100%; float: right">
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblDescricao" runat="server" Style="float: left" Text='<%# Bind("Pesquisa") %>' Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlkNm_Consumidor" runat="server" ForeColor="Blue" Style="float: left;" NavigateUrl='<%# Bind("Link_2") %>' Text='<%# Bind("Nm_Consumidor") %>'></asp:HyperLink>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlkNr_Ativo" runat="server" ForeColor="#1E783B" Style="float: left;" NavigateUrl='<%# Bind("Link_1") %>' Text='<%# Bind("Nr_Ativo") %>'></asp:HyperLink>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblValor" runat="server" Font-Names="Arial" Font-Size="9pt" Style="float: left;" Text='<%# Bind("Lote") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbllblTipo" runat="server" Font-Names="Arial" Font-Size="9pt" Style="float: left;" Text='<%# Bind("Tipo") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="height: 16px;"></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Tabela" HeaderText="Tabela" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Link_1" HeaderText="Link_1" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Link_2" HeaderText="Link_2" Visible="False"></asp:BoundColumn>
                                </Columns>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Font-Names="CordiaUPC" Font-Size="13pt" HorizontalAlign="Center" />
                            </asp:DataGrid>

                            <asp:HiddenField ID="hdfVoltar" runat="server" />
                            <asp:Label ID="lblMsg" runat="server" ForeColor="#333333" Width="100%" Style="text-align: center" Font-Names="Microsoft YaHei Light" Font-Size="Medium" Text="Não há dados para sua pesquisa." Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>
                <div>&nbsp;</div>

                <!--Indicadores-->
                <div id="divMenuPesquisa" runat="server" visible="false">
                    <div class="row text-center">
                        <div class="col-md-3 col-sm-6">
                            <div id="divContestacao" runat="server" class="card" style="background-color: #00CC00; cursor: pointer" onclick="imgContestacao_Click">
                                <div class="card-body">
                                    <asp:Button ID="imgContestacaoFull" runat="server" CssClass="bt_cockpit_menu" OnClick="imgContestacao_Click" />
                                    <asp:LinkButton ID="imgContestacao" runat="server" CssClass="nav-link">
                                        <i id="iconeContestacao" runat="server" class="fas fa-thumbs-up" style="font-size: 22pt; color: #FFFFFF"></i>
                                    </asp:LinkButton>
                                    <br />
                                    <asp:Label ID="lblDescricaoContestacao" runat="server" Style="opacity: 0.2" Text="Indicador" Font-Bold="True" Font-Names="Segoe UI" Font-Size="18pt" ForeColor="#FFFFFF"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDescricaoContestacaoSub" runat="server" Style="opacity: 0.2; line-height: 45px" Text="Indicador" Font-Bold="False" Font-Names="Segoe UI" Font-Size="14pt" ForeColor="#FFFFFF"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-6">
                            <div id="divGestao" runat="server" class="card" style="background-color: #00CC00; cursor: pointer" onclick="Gestao">
                                <div class="card-body">
                                    <asp:Button ID="btGestaoFUll" runat="server" CssClass="bt_cockpit_menu" OnClick="btGestao_Click" />
                                    <asp:LinkButton ID="btGestao" runat="server" CssClass="nav-link" OnClick="btGestao_Click">
                                        <i id="iconeGestao" runat="server" class="fas fa-thumbs-up" style="font-size: 22pt; color: #48cae4"></i>
                                    </asp:LinkButton>
                                    <br />
                                    <asp:Label ID="lblDescricaoGestao" runat="server" Style="opacity: 0.2" Text="Indicador" Font-Bold="True" Font-Names="Segoe UI" Font-Size="18pt" ForeColor="#FFFFFF"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDescricaoGestaoSub" runat="server" Style="opacity: 0.2; line-height: 45px" Text="Indicador" Font-Bold="False" Font-Names="Segoe UI" Font-Size="14pt" ForeColor="#FFFFFF"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-6">
                            <div id="divRH" runat="server" class="card" style="background-color: #00CC00">
                                <div class="card-body">
                                    <asp:Button ID="imgRHFull" runat="server" CssClass="bt_cockpit_menu" OnClick="imgRH_Click" />
                                    <asp:LinkButton ID="imgRH" runat="server" CssClass="nav-link" OnClick="imgRH_Click">
                                        <i id="iconeRH" runat="server" class="fas fa-thumbs-up" style="font-size: 22pt; color: #48cae4"></i>
                                    </asp:LinkButton>
                                    <br />
                                    <asp:Label ID="lblDescricaoRH" runat="server" Style="opacity: 0.2" Text="Indicador" Font-Bold="True" Font-Names="Segoe UI" Font-Size="16pt" ForeColor="#FFFFFF"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDescricaoRHSub" runat="server" Style="opacity: 0.2; line-height: 45px" Text="Indicador" Font-Bold="False" Font-Names="Segoe UI" Font-Size="14pt" ForeColor="#FFFFFF"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-6">
                            <div id="divFatura" runat="server" class="card" style="background-color: #00CC00">
                                <div class="card-body">
                                    <asp:Button ID="imgFaturaFull" runat="server" CssClass="bt_cockpit_menu" OnClick="imgFatura_Click" />
                                    <asp:LinkButton ID="imgFatura" runat="server" CssClass="nav-link" OnClick="imgFatura_Click">
                                        <i id="iconeFatura" runat="server" class="fas fa-thumbs-up" style="font-size: 22pt; color: #48cae4"></i>
                                    </asp:LinkButton>
                                    <br />
                                    <asp:Label ID="lblDescricaoCarga" runat="server" Style="opacity: 0.2" Text="Indicador" Font-Bold="True" Font-Names="Segoe UI" Font-Size="16pt" ForeColor="#FFFFFF"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDescricaoCargaSub" runat="server" Style="opacity: 0.2; line-height: 45px" Text="Indicador" Font-Bold="False" Font-Names="Segoe UI" Font-Size="14pt" ForeColor="#FFFFFF"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row text-center">
                        <div class="col-md-3 col-sm-6">
                            <div id="divCota" runat="server" class="card" style="background-color: #00CC00">
                                <div class="card-body">
                                    <asp:Button ID="imgCotaFull" runat="server" CssClass="bt_cockpit_menu" OnClick="imgCota_Click" />
                                    <asp:LinkButton ID="imgCota" runat="server" CssClass="nav-link">
                                        <i id="iconeCota" runat="server" class="fas fa-thumbs-up" style="font-size: 22pt; color: #FFFFFF"></i>
                                    </asp:LinkButton>
                                    <br />
                                    <asp:Label ID="lblDescricaoCota" runat="server" Style="opacity: 0.2" Text="Indicador" Font-Bold="True" Font-Names="Segoe UI" Font-Size="16pt" ForeColor="#FFFFFF"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDescricaoCotaSub" runat="server" Style="opacity: 0.2; line-height: 45px" Text="Indicador" Font-Bold="False" Font-Names="Segoe UI" Font-Size="14pt" ForeColor="#FFFFFF"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-6">
                            <div id="divContrato" runat="server" class="card" style="background-color: #00CC00">
                                <div class="card-body">
                                    <asp:Button ID="imgContratoFull" runat="server" CssClass="bt_cockpit_menu" OnClick="imgContrato_Click" />
                                    <asp:LinkButton ID="imgContrato" runat="server" CssClass="nav-link">
                                        <i id="iconeContrato" runat="server" class="fas fa-thumbs-up" style="font-size: 22pt; color: #FFFFFF"></i>
                                    </asp:LinkButton>
                                    <br />
                                    <asp:Label ID="lblDescricaoContrato" runat="server" Style="opacity: 0.2" Text="Indicador" Font-Bold="True" Font-Names="Segoe UI" Font-Size="16pt" ForeColor="#FFFFFF"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDescricaoContratoSub" runat="server" Style="opacity: 0.2; line-height: 45px" Text="Indicador" Font-Bold="False" Font-Names="Segoe UI" Font-Size="14pt" ForeColor="#FFFFFF"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-6">
                            <div id="divEstoque" runat="server" class="card" style="background-color: #00CC00">
                                <div class="card-body">
                                    <asp:Button ID="imgEstoqueFull" runat="server" CssClass="bt_cockpit_menu" OnClick="imgEstoque_Click" />
                                    <asp:LinkButton ID="imgEstoque" runat="server" CssClass="nav-link" OnClick="imgEstoque_Click">
                                        <i id="iconeEstoque" runat="server" class="fas fa-thumbs-up" style="font-size: 22pt; color: #48cae4"></i>
                                    </asp:LinkButton>
                                    <br />
                                    <asp:Label ID="lblDescricaoEstoque" runat="server" Style="opacity: 0.2" Text="Indicador" Font-Bold="True" Font-Names="Segoe UI" Font-Size="16pt" ForeColor="#FFFFFF"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDescricaoEstoqueSub" runat="server" Style="opacity: 0.2; line-height: 45px" Text="Indicador" Font-Bold="False" Font-Names="Segoe UI" Font-Size="14pt" ForeColor="#FFFFFF"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-6">
                            <div id="divRateio" runat="server" class="card" style="background-color: #00CC00">
                                <div class="card-body">
                                    <asp:Button ID="imgRateioFull" runat="server" CssClass="bt_cockpit_menu" OnClick="imgRateio_Click" />
                                    <asp:LinkButton ID="imgRateio" aria-label="Rateio" runat="server" CssClass="nav-link" OnClick="imgRateio_Click">
                                        <i id="iconeRateio" runat="server" class="fas fa-thumbs-up" style="font-size: 22pt; color: #48cae4"></i>
                                    </asp:LinkButton>
                                    <br />
                                    <asp:Label ID="lblDescricaoRateio" runat="server" Style="opacity: 0.2" Text="Indicador" Font-Bold="True" Font-Names="Segoe UI" Font-Size="16pt" ForeColor="#FFFFFF"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDescricaoRateioSub" runat="server" Style="opacity: 0.2; line-height: 45px" Text="Indicador" Font-Bold="False" Font-Names="Segoe UI" Font-Size="14pt" ForeColor="#FFFFFF"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <!--submenurelatorio-->
                    <asp:Panel ID="pnlSubMenuRelatorio" runat="server" Visible="False">
                        <nav>
                            <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                <!--K2AICONTROLIT-192-->
                                <asp:LinkButton ID="btRelatorioGeral" runat="server" aria-label="Geral" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgRelatorio" runat="server" class="fas fa-chart-bar" style="font-size: 26pt"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btMovel" runat="server" aria-label="Telefonia Móvel" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgMovel" runat="server" class="fas fa-mobile-alt" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btFixa" runat="server" aria-label="Telefonia Fixa" data-balloon-pos="up" CssClass="btn-tab-menu-disable pull-left">
                                    <i id="imgFixa" runat="server" class="fas fa-phone-alt" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btDados" runat="server" aria-label="Link de Dados" data-balloon-pos="up" CssClass="btn-tab-menu-disable pull-left">
                                    <i id="imgDados" runat="server" class="fas fa-wifi" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btCloud" runat="server" aria-label="Cloud" data-balloon-pos="up" CssClass="btn-tab-menu-disable pull-left">
                                    <i id="imgCloud" runat="server" class="fas fa-cloud" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btDesktop" runat="server" aria-label="Equipamento" data-balloon-pos="up" CssClass="btn-tab-menu-disable pull-left">
                                    <i id="imgDesktop" runat="server" class="fas fa-server" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btImpressao" runat="server" aria-label="Impressão" data-balloon-pos="up" CssClass="btn-tab-menu-disable pull-left">
                                    <i id="imgImpressao" runat="server" class="fas fa-print" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                            </div>
                        </nav>
                        <div class="card-body" style="background-color: #FFFFFF; border-radius: 6px; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px">
                            <!--Cabecario-->
                            <div class="row text-center">
                                <div class="col-md-12">
                                    <asp:Label ID="lblDescricaoRelatorio" runat="server" ForeColor="#818181" CssClass="configlabel" Font-Size="16pt" Style="float: none;" Font-Names="Segoe UI Semibold" Text="Extração de Dados: "></asp:Label>
                                    <asp:Label ID="lblGrupo" runat="server" Font-Names="Segoe UI Semibold" ForeColor="#818181" CssClass="configlabel" Font-Size="16pt" Style="float: none;"></asp:Label>
                                </div>
                            </div>
                            <div>&nbsp;</div>
                            <!--Sub Botões-->
                            <div class="row">
                                <div class="col-md-12 text-left">
                                    <ul style="list-style-type: none; margin: 0; padding: 0">
                                        <li class="liMenu">
                                            <asp:Button ID="btVolumeCusto" runat="server" CssClass="btn btn-primary" CausesValidation="False" Font-Size="9pt" Font-Names="Segoe UI" Text="Volume ($)" Enabled="False" Style="cursor: pointer; text-align: center;" />
                                        </li>
                                        <li class="liMenu">
                                            <asp:Button ID="btVolumeQuantidade" runat="server" CssClass="btn btn-primary" CausesValidation="False" Font-Size="9pt" Font-Names="Segoe UI" Text="Volume" Enabled="False" Style="cursor: pointer; text-align: center;" />
                                        </li>
                                        <li class="liMenu">
                                            <asp:Button ID="btAproveitamento" runat="server" CssClass="btn btn-primary" CausesValidation="False" Font-Size="9pt" Font-Names="Segoe UI" Text="Aproveitamento" Enabled="False" Style="cursor: pointer; text-align: center;" />
                                        </li>
                                        <li class="liMenu">
                                            <asp:Button ID="btDetalhamento" runat="server" CssClass="btn btn-primary" CausesValidation="False" Font-Size="9pt" Font-Names="Segoe UI" Text="Detalhamento" Enabled="False" Style="cursor: pointer; text-align: center;" />
                                        </li>
                                        <li class="liMenu">
                                            <asp:Button ID="btEstatisticaChamado" runat="server" CssClass="btn btn-primary" CausesValidation="False" Font-Size="9pt" Font-Names="Segoe UI" Text="Suporte" Enabled="False" Style="cursor: pointer; text-align: center;" />
                                        </li>
                                        <li class="liMenu">
                                            <asp:Button ID="btMonitoramentoDados" runat="server" CssClass="btn btn-primary" CausesValidation="False" Font-Size="9pt" Font-Names="Segoe UI" Text="Monitora/Mb" Enabled="False" Style="cursor: pointer; text-align: center;" />
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div>&nbsp;</div>
                            <!--Corpo-->
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="DivRelatorio" runat="server" style="width: 100%; z-index: 103; overflow: auto; max-height: 100%;">
                                        <asp:DataGrid ID="dtgLocalizaRelatorio" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="0" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" PageSize="5" Style="font-size: 8pt; color: black; font-family: Arial" GridLines="None" ShowHeader="False">
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <PagerStyle Mode="NumericPages" />
                                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />

                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <table style="width: 100%; float: left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="btn-Selected" style="height: 40px; text-align: left; padding-left: 10px; padding-top: 8px">
                                                                    <asp:LinkButton ID="bt01" runat="server" ForeColor="#4FC3F7" PostBackUrl='<%# Bind("Link_1") %>'>
                                                                        <i id="i20" runat="server" class="fas fa-search-plus" style="font-size: 18pt"></i>
                                                                        <asp:Label ID="lblBt01" runat="server" Font-Names="Segoe UI Semibold" ForeColor="#4FC3F7" CssClass="configlabel" Font-Size="10pt" Style="float: none;" Text='<%# Bind("Nm_Template_1") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <table style="width: 100%; float: left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="btn-Selected" style="height: 40px; text-align: left; padding-left: 10px; padding-top: 8px">
                                                                    <asp:LinkButton ID="bt02" runat="server" ForeColor="#4FC3F7" PostBackUrl='<%# Bind("Link_2") %>'>
                                                                        <i id="i20" runat="server" class="fas fa-search-plus" style="font-size: 18pt"></i>
                                                                        <asp:Label ID="lblBt02" runat="server" Font-Names="Segoe UI Semibold" ForeColor="#4FC3F7" CssClass="configlabel" Font-Size="10pt" Style="float: none;" Text='<%# Bind("Nm_Template_2") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:DataGrid>
                                    </div>
                                </div>
                            </div>
                            <!--Msg-->
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblObservacao" runat="server" CssClass="configlabel" ForeColor="#818181" Style="left: 9px; height: 13px; float: none;" Text="* Você não possui permissão de acesso aos relatórios de indicadores desse tipo de ativo." Visible="False"></asp:Label>
                                    <asp:Label ID="lblObservacao_Det" runat="server" CssClass="configlabel" ForeColor="#818181" Style="left: 9px; height: 13px; float: none;" Text="* Você não possui permissão de acesso aos relatórios customizados desse tipo de ativo." Visible="False"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!--submenu-->
                    <div id="pnlSubMenu" runat="server" visible="False">
                        <!--Botões-->
                        <nav>
                            <div class="nav nav-tabs" id="nav-tab-02" role="tablist">
                                <asp:LinkButton ID="bt01" runat="server" aria-label="Menu de Ativo" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgBt01" runat="server" class="fas fa-grip-horizontal" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="bt02" runat="server" aria-label="Menu de Usuário" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgBt02" runat="server" class="fas fa-users" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="bt03" runat="server" aria-label="Menu de Contrato" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgBt03" runat="server" class="fas fa-sticky-note" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="bt04" runat="server" aria-label="Menu de Contestação" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgBt04" runat="server" class="fas fa-search-dollar" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="bt05" runat="server" aria-label="Menu de Importar e Exportar Dados" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgBt05" runat="server" class="fas fa-file-download" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="bt06" runat="server" aria-label="Menu de Estoque" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgBt06" runat="server" class="fas fa-archive" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="bt07" runat="server" aria-label="Menu de Fatura" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgBt07" runat="server" class="fas fa-file-invoice-dollar" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="bt08" runat="server" aria-label="Menu de Repasse de Custo Fixo" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgBt08" runat="server" class="fas fa-chart-area" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="bt09" runat="server" aria-label="Menu de Ferramenta" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgBt09" runat="server" class="fas fa-cogs" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="bt10" runat="server" aria-label="Menu de Chamado" data-balloon-pos="up" CssClass="btn-tab-menu pull-left text-center">
                                    <i id="imgBt10" runat="server" class="fas fa-headset" style="font-size: 26pt"></i>
                                </asp:LinkButton>
                            </div>
                        </nav>
                        <div class="card-body" style="background-color: #FFFFFF; border-radius: 6px; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px">
                            <div class="row">
                                <div class="col-md-12">
                                    <!--Cabeçalho-->
                                    <div class="row text-center">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblDefinicoes" runat="server" ForeColor="#818181" CssClass="configlabel" Font-Names="Segoe UI Semibold" Font-Size="18pt" Style="float: none;" Text="Cadastro"></asp:Label>
                                        </div>
                                    </div>
                                    <div>&nbsp</div>
                                    <div style="width: 100%; height: 1px; background-color: #E0E0E0"></div>
                                    <div>&nbsp</div>
                                    <!--Corpo-->
                                    <div class="row">
                                        <div class="col-md-6">
                                            <ul style="list-style-type: none; margin: 0; padding: 5px; text-align: left">
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS01" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i1" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS01" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS03" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i2" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS03" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS05" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i3" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS05" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS07" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i4" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS07" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS09" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i5" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS09" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS11" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i6" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS11" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS13" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i7" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS13" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS15" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i8" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS15" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS17" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i9" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS17" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS19" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i10" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS19" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-md-6">
                                            <ul style="list-style-type: none; margin: 5px; padding: 5px; text-align: left">
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS02" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i11" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS02" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS04" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i12" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS04" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS06" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i13" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS06" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS08" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i14" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS08" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS10" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i15" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS10" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS12" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i16" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS12" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS14" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i17" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS14" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS16" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i18" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS16" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS18" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i19" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS18" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                                <li class="btn-Selected">
                                                    <asp:LinkButton ID="btS20" runat="server" ForeColor="#4FC3F7">
                                                        <i id="i20" runat="server" class="fas fa-file-import" style="font-size: 18pt"></i>
                                                        <span id="lblBtS20" runat="server" style="font-size: 12pt"></span>
                                                    </asp:LinkButton>
                                                </li>
                                            </ul>
                                        </div>
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
