<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Arquivo_Usuario.aspx.vb" Inherits="IControlIT.Arquivo_Usuario" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Verifica Arquivo-->
    <div id="pnlVerifica_Arquivo" runat="server" class="bgModal" visible="true">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblVerifica_Arquivo" runat="server" CssClass="configlabel" Style="float: none" Text="* Verifique se o arquivo para carga é o correto."></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btOK_Arquivo" class="btn btn-success" runat="server" Text="OK" CausesValidation="False" />
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
                            <asp:Label ID="lblMatrix" runat="server" Text="Matrix de Importação" ForeColor="#333333" Font-Names="Calibri Light" Font-Size="20pt" Visible="False"></asp:Label>
                            <div id="divCampos" runat="server" style="border: 1px solid #FFFFFF; overflow: auto; width: 100%; height: 190px;" title=" ">
                                <asp:DataGrid ID="dtgCampo" runat="server" AutoGenerateColumns="False" BackColor="#FFFFFF" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="CordiaUPC" Font-Overline="False" Font-Size="17pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Left" Style="font-size: 8pt; font-family: Arial; float: none;" GridLines="Horizontal" Width="752px" BorderColor="Silver" BorderWidth="1px">
                                    <Columns>
                                        <asp:BoundColumn DataField="Id_Campo" HeaderText="Id_Campo" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Campo" HeaderText="Campo"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Obrigatório">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVazio" runat="server" CssClass="configlabel" Style="float: left;" ForeColor="Black" Text="<%# Bind('Vazio') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Descrição Repetida">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" TabIndex="1" Width="200px" ForeColor="Black" Text="<%# Bind('Coluna_Fixo') %>"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn HeaderText="Quantidade" DataField="Quantidade"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Observação" DataField="Observacao"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Tamanho" HeaderText="Tamanho"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Descricao" HeaderText="Descrição"></asp:BoundColumn>
                                    </Columns>

                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <PagerStyle Mode="NumericPages" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Names="Calibri Light" Font-Size="10pt" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblArquivo" runat="server" Text="Matrix de Arquivo" ForeColor="#333333" Font-Names="Calibri Light" Font-Size="20pt" Visible="False"></asp:Label>
                            <div id="divDados" runat="server" style="border: 1px solid #FFFFFF; overflow: auto; width: 100%; height: 190px;" title=" ">
                                <asp:DataGrid ID="dtgDado" runat="server" AutoGenerateColumns="False" BackColor="#FFFFFF" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="CordiaUPC" Font-Overline="False" Font-Size="17pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Left" Style="font-size: 8pt; font-family: Arial; float: none;" Width="752px" BorderColor="Silver" BorderWidth="1px" GridLines="Horizontal">
                                    <Columns>
                                        <asp:BoundColumn DataField="Campo" HeaderText="Cabeçalho">
                                            <HeaderStyle Width="500px" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Campo">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cboCampoUsuario" runat="server" CssClass="configCombo" EnableTheming="True" ForeColor="Black" TabIndex="3" Width="200px">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Campo_Arquivo" HeaderText="Campo_Arquivo" Visible="False"></asp:BoundColumn>
                                    </Columns>

                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <PagerStyle Mode="NumericPages" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Names="Calibri Light" Font-Size="10pt" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblTeste" runat="server" Text="Teste da Carga" ForeColor="#333333" Font-Names="Calibri Light" Font-Size="20pt" Visible="False"></asp:Label>
                            <div id="diDetalhe" runat="server" style="border: 1px solid #FFFFFF; overflow: auto; width: 100%; height: 132px;" title=" ">
                                <asp:DataGrid ID="dtgDetalhe" runat="server" BackColor="#FFFFFF" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="CordiaUPC" Font-Overline="False" Font-Size="12pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Left" Style="font-size: 8pt; font-family: Arial; float: none;" Width="752px" BorderColor="Silver" BorderWidth="1px" GridLines="Horizontal">

                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <PagerStyle Mode="NumericPages" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Names="Calibri Light" Font-Size="10pt" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--MSG-->
    <div id="pnlValidador2" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
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
        <asp:LinkButton ID="btConfiguracao" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btConfiguracao_Click">
            <i class="fas fa-cog"></i>
            <br />
            <span id="Config" runat="server">Config</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btUpload" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btUpload_Click">
            <i class="fas fa-upload"></i>
            <br />
            <span id="Upload" runat="server">Upload</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btExportar_Click">
            <i class="fas fa-download"></i>
            <br />
            <span id="Span1" runat="server">Exportar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAbrir_Click">
            <i class="fas fa-folder-open"></i>
            <br />
            <span id="Span2" runat="server">Dados</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSincronizaCont" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btSincronizaCont_Click">
            <i class="fas fa-sync"></i>
            <br />
            <span id="Span3" runat="server">Sync</span>
        </asp:LinkButton>
         <asp:LinkButton ID="btImportar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btImportar_Click">
            <i class="fas fa-upload"></i>
            <br />
            <span id="Span4" runat="server">Importar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAtualizar_Click">
            <i class="fas fa-sync"></i>
            <br />
            <span id="Span5" runat="server">Atualizar</span>
        </asp:LinkButton>
    </div>

</asp:Content>
