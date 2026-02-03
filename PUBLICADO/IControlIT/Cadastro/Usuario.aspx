<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Usuario.aspx.vb" Inherits="IControlIT.Usuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="* Login"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblSenha" runat="server" CssClass="configlabel" Text="Senha"></asp:Label>
                            <asp:Button ID="btRedefinirSenha" class="btn btn-warning" Width="100%" runat="server" Style="float: left" Text="Reiniciar Senha" CausesValidation="False" OnClientClick="return confirm('Reiniciar Senha do Usuário ?');" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblIdioma" runat="server" CssClass="configlabel" Text="* Idioma"></asp:Label>
                            <asp:DropDownList ID="cboIdioma" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="2" Width="100%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvIdioma" runat="server" ControlToValidate="cboIdioma" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblUsuarioGrupo" runat="server" CssClass="configlabel" Text="* Grupo"></asp:Label>
                            <asp:DropDownList ID="cboUsuarioGrupo" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="3" Width="100%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvUsuarioGrupo" runat="server" ControlToValidate="cboUsuarioGrupo" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;" TabIndex="4"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblUsuarioPerfil" runat="server" CssClass="configlabel" Text="* Perfil"></asp:Label>
                            <asp:DropDownList ID="cboUsuarioPerfil" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="5" AutoPostBack="True"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvUsuarioPerfil" runat="server" ControlToValidate="cboUsuarioPerfil" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblUsuarioPerfilAcesso" runat="server" CssClass="configlabel" Text="* Acesso"></asp:Label>
                            <asp:DropDownList ID="cboUsuarioPerfilAcesso" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="6"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvUsuarioPerfilAcesso" runat="server" ControlToValidate="cboUsuarioPerfilAcesso" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblIncluir" runat="server" CssClass="configlabel" Width="100%" Text="Permissão de incluir"></asp:Label>
                            <asp:RadioButtonList ID="optIncluir" runat="server" RepeatDirection="Horizontal" CellPadding="5" Style="font-size: 8pt; font-family: Arial;" TabIndex="7" ForeColor="Black">
                                <asp:ListItem Selected="True" Value="1">Revogar</asp:ListItem>
                                <asp:ListItem Value="2">Permitir</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblAlterar" runat="server" CssClass="configlabel" Width="100%" Text="Permissão de alterar"></asp:Label>
                            <asp:RadioButtonList ID="optAlterar" runat="server" RepeatDirection="Horizontal" CellPadding="5" Style="font-size: 8pt; font-family: Arial;" TabIndex="8" ForeColor="Black">
                                <asp:ListItem Selected="True" Value="1">Revogar</asp:ListItem>
                                <asp:ListItem Value="2">Permitir</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblExcluir" runat="server" CssClass="configlabel" Width="100%" Text="Permissão de excluir"></asp:Label>
                            <asp:RadioButtonList ID="optExcluir" runat="server" RepeatDirection="Horizontal" CellPadding="5" Style="font-size: 8pt; font-family: Arial;" TabIndex="9" ForeColor="Black">
                                <asp:ListItem Selected="True" Value="1">Revogar</asp:ListItem>
                                <asp:ListItem Value="2">Permitir</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDetalhamentoConta" runat="server" CssClass="configlabel" Width="100%" Text="Conta de outro usuário"></asp:Label>
                            <asp:RadioButtonList ID="optDetalhamentoConta" runat="server" RepeatDirection="Horizontal" CellPadding="5" Style="font-size: 8pt; font-family: Arial;" TabIndex="10" ForeColor="Black">
                                <asp:ListItem Selected="True" Value="1">Revogar</asp:ListItem>
                                <asp:ListItem Value="2">Permitir</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDetalhamentoContato" runat="server" CssClass="configlabel" Width="100%" Text="Contatos de outro usuário"></asp:Label>
                            <asp:RadioButtonList ID="optDetalhamentoContato" runat="server" RepeatDirection="Horizontal" CellPadding="5" Style="font-size: 8pt; font-family: Arial;" TabIndex="11" ForeColor="Black">
                                <asp:ListItem Selected="True" Value="1">Revogar</asp:ListItem>
                                <asp:ListItem Value="2">Permitir</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblStatusUsuario" runat="server" CssClass="configlabel" Width="100%" Text="Status para acesso "></asp:Label>
                            <asp:RadioButtonList ID="optStatusUsuario" runat="server" RepeatDirection="Horizontal" CellPadding="5" Style="font-size: 8pt; font-family: Arial;" TabIndex="12" ForeColor="Black">
                                <asp:ListItem Selected="True" Value="1">Revogar</asp:ListItem>
                                <asp:ListItem Value="3">Permitir</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
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
                        <div class="col-md-6">
                            <asp:Label ID="lblConsumidor" runat="server" CssClass="configlabel" Text="Nome"></asp:Label>
                            <asp:TextBox ID="txtIdConsumidor" runat="server" CssClass="configtext" Width="100%" Visible="False" ForeColor="#FF9900"></asp:TextBox>
                            <asp:TextBox ID="txtNmConsumidor" runat="server" CssClass="configtext" Width="100%" ReadOnly="True" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco"></asp:Label>
                            <asp:TextBox ID="txtIdentificacao" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 561px; position: absolute; top: 559px; height: 48px; width: 124px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceIdioma" runat="server" TargetControlID="rfvIdioma"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceUsuarioGrupo" runat="server" TargetControlID="rfvUsuarioGrupo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceUsuarioPerfil" runat="server" TargetControlID="rfvUsuarioPerfil"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceUsuarioPerfilAcesso" runat="server" TargetControlID="rfvUsuarioPerfilAcesso"></cc1:ValidatorCalloutExtender>
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
        <asp:LinkButton ID="btLimpar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btLimpar_Click">
            <i class="fas fa-file"></i>
            <br />
            <span>Novo</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" OnClick="btSalvar_Click">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btConfiguracao" runat="server" CssClass="btn-menu-toolbar">
            <i class="fas fa-cog" style="font-size: 16pt"></i>
            <br />
            <span id="lblEncerrar" runat="server">Config</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btPDF" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span id="lblPdf" runat="server">PDF</span>
        </asp:LinkButton>
    </div>

</asp:Content>
