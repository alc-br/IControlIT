<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Aparelho.aspx.vb" Inherits="IControlIT.Aparelho" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Observacao-->
    <div id="pnlObservacao" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Observação" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                    <asp:TextBox ID="txtObservacaoObrigatoria" runat="server" CssClass="configtext" MaxLength="300" Style="float: left; border-radius: 6px 6px;" TextMode="MultiLine" Width="100%" Height="350px" TabIndex="7"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvObservacao" runat="server" ControlToValidate="txtObservacaoObrigatoria" Font-Names="Arial" Font-Size="10pt" Style="left: 445px; top: 38px; float: left;" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btCancela" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btOk" class="btn btn-success" runat="server" Text="Confirmar" CausesValidation="False" />
                    <asp:HiddenField ID="hfdId_Aparelho" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <!--Registro Adicionais-->
    <div id="pnlRegistro" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescRegistro" runat="server" CssClass="configlabel" Text="Dados Adicionais" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div style="height: 5px"></div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblObservacao" runat="server" CssClass="configlabel" Text="Observação" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:TextBox ID="txtObservacao" runat="server" Style="width: 100%; min-height: 150px; font-size: 9pt" TextMode="MultiLine" TabIndex="8" ForeColor="#FF9900" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblFinalidade" runat="server" CssClass="configlabel" Text="Finalidade" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:TextBox ID="txtFinalidade" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="100%" CssClass="configtext"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblIdentificacaoModal" runat="server" CssClass="configlabel" Text="Chave do banco" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:TextBox ID="txtIndentificacaoModal" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFechar_Registro" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
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
                            <asp:Label ID="lblAparelhoTipo" runat="server" CssClass="configlabel" Text="* Posição do equipamento"></asp:Label>
                            <asp:DropDownList ID="cboAparelhoTipo" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="1">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="3">Solicitação</asp:ListItem>
                                <asp:ListItem Value="4">Devolução</asp:ListItem>
                                <asp:ListItem Value="1">Estoque</asp:ListItem>
                                <asp:ListItem Value="2">Assistência</asp:ListItem>
                                <asp:ListItem Value="5">Descarte</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAparelhoTipo" runat="server" ControlToValidate="cboAparelhoTipo" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblNumeroAparelho" runat="server" CssClass="configlabel" Text="* Número do Aparelho"></asp:Label>
                            <asp:TextBox ID="txtNumeroAparelho" runat="server" CssClass="configtext" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNumeroAparelho" runat="server" ControlToValidate="txtNumeroAparelho" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblLinhaSolicitacao" runat="server" CssClass="configlabel" Text="Linha Solicitação"></asp:Label>
                            <asp:TextBox ID="txtLinhaSolicitacao" runat="server" CssClass="configtext" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblNumeroChamado" runat="server" CssClass="configlabel" Text="* Número do Chamado"></asp:Label>
                            <asp:TextBox ID="txtNumeroChamado" runat="server" CssClass="configtext" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNumeroChamado" runat="server" ControlToValidate="txtNumeroChamado" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblValorImobilizado" runat="server" CssClass="configlabel" Text="Status"></asp:Label>
                            <asp:DropDownList ID="cboEstoqueAparelhoStatus" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="3"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblEnderecoEntrega" runat="server" CssClass="configlabel" Text="Endereço de entrega"></asp:Label>
                            <asp:DropDownList ID="cboEnderecoEntrega" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="4"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btImprimir" class="btn btn-warning" runat="server" Text="Etiqueta" CausesValidation="False" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblNotaFiscal" runat="server" CssClass="configlabel" Text="* Nota Fiscal"></asp:Label>
                            <asp:DropDownList ID="cboNotaFiscal" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="5"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvNotaFiscal" runat="server" ControlToValidate="cboNotaFiscal" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblGestorEstoque" runat="server" CssClass="configlabel" Text="* Gestor do Estoque"></asp:Label>
                            <asp:DropDownList ID="cboUsuarioEstoque" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="5"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvUsuarioEstoque" runat="server" ControlToValidate="cboUsuarioEstoque" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblConglomerado" runat="server" CssClass="configlabel" Text="* Fornecedor"></asp:Label>
                            <asp:DropDownList ID="cboConglomerado" runat="server" Width="100%" CssClass="configCombo" EnableTheming="True" AutoPostBack="True" TabIndex="7"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvConglomerado" runat="server" ControlToValidate="cboConglomerado" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblAtivoTipo" runat="server" CssClass="configlabel" Text="* Tipo da linha"></asp:Label>
                            <asp:DropDownList ID="cboAtivoTipo" runat="server" Width="100%" CssClass="configCombo" EnableTheming="True" AutoPostBack="True" TabIndex="7"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAtivoTipo" runat="server" ControlToValidate="cboAtivoTipo" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblAtivoModelo" runat="server" CssClass="configlabel" Text="Modelo do equipamento"></asp:Label>
                            <asp:DropDownList ID="cboAtivoModelo" runat="server" Width="100%" CssClass="configCombo" EnableTheming="True" TabIndex="8"></asp:DropDownList>

                            <asp:Label ID="lblNumeroAparelho2" runat="server" CssClass="configlabel" Text="2º Número do Aparelho"></asp:Label>
                            <asp:TextBox ID="txtNumeroAparelho2" runat="server" CssClass="configtext" Width="100%" MaxLength="50" TabIndex="9"></asp:TextBox>
                        </div>
                         <div class="col-md-6">
                            <div class="row">
                                <div id="divAtivoAcessorios" class="col-md-12">
                                    <asp:Label ID="lbAtivoAcessorios" runat="server" CssClass="configlabel" Text="* Acessórios"></asp:Label> 
                                </div> 
                                <div id="divAtivoAcessoriosCkb" class="col-md-11 offset-md-1">
                                    <asp:CheckBox ID="CkbCarregador" runat="server" />
                                    <asp:Label AssociatedControlID="CkbCarregador" runat="server">Carregador</asp:Label><br />
                                    <asp:CheckBox ID="CkbCabo" runat="server" />
                                    <asp:Label AssociatedControlID="CkbCabo" runat="server">Cabo USB</asp:Label><br />
                                    <asp:CheckBox ID="CkbFone" runat="server" />
                                    <asp:Label AssociatedControlID="CkbFone" runat="server">Fone</asp:Label><br />
                                    <asp:CheckBox ID="CkbPelicula" runat="server" />
                                    <asp:Label AssociatedControlID="CkbPelicula" runat="server">Película</asp:Label><br />
                                    <asp:CheckBox ID="CkbCapa" runat="server" />
                                    <asp:Label AssociatedControlID="CkbCapa" runat="server">Capa de proteção do aparelho</asp:Label><br />
                                    <asp:Label ID="lbSelecioneAcessorio" runat="server" Visible="false" CssClass="configlabel" Text="Selecione um acessório"></asp:Label>
                                </div>
                            </div>
                        </div>
<%--                        <div class="col-md-6">
                            <asp:Label ID="lblObservacao" runat="server" CssClass="configlabel" Text="Observação"></asp:Label>
                            <asp:TextBox ID="txtObservacao" runat="server" CssClass="configtext" Height="100%" MaxLength="300" TextMode="MultiLine" Width="100%" TabIndex="11"></asp:TextBox>
                        </div>--%>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblNumeroAtivo" runat="server" CssClass="configlabel" Text="Número do ativo vinculado"></asp:Label>
                            <asp:TextBox ID="txtDescricaoLinha" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
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

    <asp:Panel ID="pnlValidador2" runat="server" Style="z-index: 107; left: 636px; position: absolute; top: 594px; height: 56px; width: 176px;">
        <cc1:ValidatorCalloutExtender ID="vceNumeroAparelho" runat="server" TargetControlID="rfvNumeroAparelho"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceNotaFiscal" runat="server" TargetControlID="rfvNotaFiscal"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceNumeroChamado" runat="server" TargetControlID="rfvNumeroChamado"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceUsuarioEstoque" runat="server" TargetControlID="rfvUsuarioEstoque"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceAtivoTipo" runat="server" TargetControlID="rfvAtivoTipo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceAparelhoTipo" runat="server" TargetControlID="rfvAparelhoTipo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceConglomerado" runat="server" TargetControlID="rfvConglomerado"></cc1:ValidatorCalloutExtender>
        <asp:Label ID="lblMessage" runat="server" Style="left: 13px; top: 87px"></asp:Label>
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
        <asp:LinkButton ID="btDesativar" runat="server" CssClass="btn-menu-toolbar" OnClientClick="return confirm('Você deseja desativa o registro?');" OnClick="btDesativar_Click">
            <i class="fas fa-trash"></i>
            <br />
            <span>Excluir</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btPDF" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span>PDF</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAbrir_Click">
            <i class="fas fa-folder-open"></i>
            <br />
            <span>Dados</span>
        </asp:LinkButton>
    </div>

</asp:Content>
