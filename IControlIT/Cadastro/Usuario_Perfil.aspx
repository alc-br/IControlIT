<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Usuario_Perfil.aspx.vb" Inherits="IControlIT.Usuario_Perfil" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--perfil-->
    <div id="pnlDetalhe" runat="server" class="row" visible="false">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <!--Relacionamento-->
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-warning">
                                <div class="panel-heading row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblMontaHierarquia" runat="server" CssClass="configlabel" Text="Permissão para Requisição" Font-Bold="False"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <div style="width: 100%; border: 1px solid #818181; border-radius: 4px">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtGrupo" runat="server" CssClass="configtext" Style="width: 100%; border: none" BackColor="Transparent"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 30px; background-color: #09A8C5; border-radius: 0px 4px 4px 0px">
                                                        <asp:LinkButton ID="btGrupo" runat="server" CssClass="nav-link" OnClick="btGrupo_Click">
                                                            <i class="fas fa-search" style="color: #FFFFFF; font-size: 9pt"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div style="height: 5px"></div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="width: 100%; border: 1px solid #818181; border-radius: 4px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:ListBox ID="lstOrigem" runat="server" CssClass="configlistboxAbrir border-0" ForeColor="Black" Height="90px" Width="100%"></asp:ListBox>
                                                    </td>
                                                    <td style="width: 45px; background-color: #09A8C5; text-align: center; border-radius: 0px 4px 4px 0px">
                                                        <asp:LinkButton ID="btMoveSelecionado" runat="server" CssClass="nav-link" Style="position: relative; width: 100%; height: 90px;" OnClick="btGrupo_Click">
                                                            <i class="fas fa-caret-down" style="color: #FFFFFF; font-size: 14pt; position: absolute; top: 50%; transform: translateY(-50%); right: 35%"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="height: 5px"></div>
                                    <div class="col-md-12">
                                        <div style="width: 100%; border: 1px solid #818181; border-radius: 4px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:ListBox ID="lstDestino" runat="server" CssClass="configlistboxAbrir border-0" ForeColor="Black" Height="90px" Width="100%"></asp:ListBox>
                                                    </td>
                                                    <td style="width: 45px; background-color: #09A8C5; text-align: center; border-radius: 0px 4px 4px 0px">
                                                        <asp:LinkButton ID="btMoveSelecao" runat="server" CssClass="nav-link" Style="position: relative; width: 100%; height: 90px;" OnClick="btGrupo_Click">
                                                            <i class="fas fa-caret-up" style="color: #FFFFFF; font-size: 14pt; position: absolute; top: 50%; transform: translateY(-50%); right: 35%"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-right">
                            <asp:Button ID="btFechar" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                            <asp:Button ID="btSalvarGrupo" class="btn btn-success" runat="server" Text="Salvar" CausesValidation="False" />
                        </div>
                    </div>
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
                        <div class="col-md-6">
                            <asp:Label ID="lblNivel" runat="server" CssClass="configlabel" Text="Nível"></asp:Label>
                            <asp:DropDownList ID="cboConfiguraAcesso" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" AutoPostBack="True" TabIndex="1"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblAtivoCompetencia" runat="server" CssClass="configlabel" Text="Ativo vinculado"></asp:Label>
                            <div id="DivAtivo" runat="server" style="border: 1px solid #CCCCCC; overflow: auto; width: 100%; height: 102px;" title=" ">

                                <asp:DataGrid ID="dtgAtivoCompetencia" runat="server" AutoGenerateColumns="False" EnableTheming="True" HorizontalAlign="Left" PageSize="4"
                                    Style="font-size: 8pt; font-family: Arial" Width="100%" AllowPaging="True" CellPadding="5" CellSpacing="5" GridLines="None" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">

                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <PagerStyle Mode="NumericPages" />
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />

                                    <Columns>

                                        <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo" Visible="True">
                                            <ItemStyle Font-Bold="False" Height="20px" Font-Italic="False" Font-Size="10pt" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Dt_Hr_Ativacao" HeaderText="Ativação" Visible="True">
                                            <ItemStyle Font-Bold="False" Height="20px" Font-Italic="False" Font-Size="10pt" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Dt_Hr_Desativacao" HeaderText="Desativação" Visible="True">
                                            <ItemStyle Font-Bold="False" Height="20px" Font-Italic="False" Font-Size="10pt" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Id_Ativo" HeaderText="Id_Ativo" Visible="False">
                                            <ItemStyle Font-Bold="False" Height="20px" Font-Italic="False" Font-Size="10pt" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                    </Columns>
                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#EEEEEE" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblUsuario" runat="server" CssClass="configlabel" Text="Usuário"></asp:Label>
                            <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="configtext" Visible="False" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                            <asp:TextBox ID="txtNmUsuario" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblConsumidor" runat="server" CssClass="configlabel" Text="Colaborador"></asp:Label>
                            <asp:TextBox ID="txtIdConsumidor" runat="server" CssClass="configtext" Visible="False" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                            <asp:TextBox ID="txtNmConsumidor" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblUsuarioPerfil" runat="server" CssClass="configlabel" Text="Perfil"></asp:Label>
                            <asp:TextBox ID="txtIdUsuarioPerfil" runat="server" CssClass="configtext" Visible="False" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                            <asp:TextBox ID="txtNmUsuarioPerfil" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblUsuarioPerfilAcesso" runat="server" CssClass="configlabel" Text="Acesso"></asp:Label>
                            <asp:TextBox ID="txtIdUsuarioPerfilAcesso" runat="server" CssClass="configtext" Visible="False" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                            <asp:TextBox ID="txtNmUsuarioPerfilAcesso" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
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
        <asp:LinkButton ID="btDesativar" runat="server" CssClass="btn-menu-toolbar" OnClientClick="return confirm('Você deseja desativa o registro?');" OnClick="btDesativar_Click">
            <i class="fas fa-recycle"></i>
            <br />
            <span id="lblEncerrar" runat="server">Excluir</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btPDF" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span id="lblPdf" runat="server">PDF</span>
        </asp:LinkButton>
    </div>

</asp:Content>
