<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Fatura_Nota_Fiscal.aspx.vb" Inherits="IControlIT.Fatura_Nota_Fiscal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Confirmação-->
    <div id="pnlConfirmacao" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="configlabel" Text="Confirmação" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblMenssagem" runat="server" CssClass="configlabel" Text="Nota fiscal criada com sucesso. Deseja deseja criar uma nova para a mesma fatura?" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btNao" class="btn btn-default" runat="server" Text="Não" CausesValidation="False" />
                    <asp:Button ID="btSim" class="btn btn-success" runat="server" Text="Sim" CausesValidation="False" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
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
                            <asp:Label ID="lblFatura" runat="server" CssClass="configlabel" Text="* Fatura"></asp:Label>
                            <asp:DropDownList ID="cboFatura" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:DropDownList>
                        </div>
                        <div class="col-md-5">
                            <asp:Label ID="lblCentroCusto" runat="server" CssClass="configlabel" Text="Centro de Custo"></asp:Label>
                            <asp:DropDownList ID="cboCentroCusto" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <div style="width: 100%; height: 100%; position: relative;">
                                <asp:Button ID="btBuscar" class="btn btn-success" runat="server" Style="position: absolute; right: 0; bottom: -5px" Text="Buscar" CausesValidation="False" />
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="dvDetalhes" visible="false">
                        <div class="col-md-12">
                            <hr />
                            <h4>Detalhes da fatura</h4>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblNrFatura" runat="server" Text="Número da Fatura" Style="float: left; text-align: left" CssClass="configlabel"></asp:Label>
                            <asp:TextBox ID="txtNrFatura" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="100" Width="100%" CssClass="configtext" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblDtLote" runat="server" Text="Data Lote" Style="float: left; text-align: left" CssClass="configlabel"></asp:Label>
                            <asp:TextBox ID="txtDtLote" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="100" Width="100%" CssClass="configtext" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblVrFatura" runat="server" Text="Valor da Fatura" Style="float: left; text-align: left" CssClass="configlabel"></asp:Label>
                            <asp:TextBox ID="txtVrFatura" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="100" Width="100%" CssClass="configtext" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblNmFaturaParametro" runat="server" Text="Conglomerado" Style="float: left; text-align: left" CssClass="configlabel"></asp:Label>
                            <asp:TextBox ID="txtNmFaturaParametro" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="100" Width="100%" CssClass="configtext" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <hr />
                            <h4>Detalhes do centro de custo</h4>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblNmCentroCusto" runat="server" Text="Nome" Style="float: left; text-align: left" CssClass="configlabel"></asp:Label>
                            <asp:TextBox ID="txtNmCentroCusto" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="100" Width="100%" CssClass="configtext" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblCdCentroCusto" runat="server" Text="CD" Style="float: left; text-align: left" CssClass="configlabel"></asp:Label>
                            <asp:TextBox ID="txtCdCentroCusto" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="100" Width="100%" CssClass="configtext" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div>
                            <asp:HiddenField ID="txtIdFatura" runat="server" />
                            <asp:HiddenField ID="txtIdCentroCusto" runat="server" />
                        </div>
                    </div>
                    <div class="row" runat="server" id="dvCadastro" visible="false">
                        <div class="col-md-12">
                            <hr />
                            <h4>Cadastro da nota fiscal</h4>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblNrNotaFiscal" runat="server" CssClass="configlabel" Text="Número de Nota Fiscal"></asp:Label>
                            <asp:TextBox ID="txtNrNotaFiscal" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="100" Width="100%" CssClass="configtext"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblPctNotaFiscal" runat="server" CssClass="configlabel" Text="Porcentagem/Valor"></asp:Label>
                            <asp:TextBox ID="txtPctNotaFiscal" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="100" Width="100%" CssClass="configtext"></asp:TextBox>
                        </div>
                        <%--<div class="col-md-4">
                            <asp:Label ID="lblVrNotaFiscal" runat="server" CssClass="configlabel" Text="Valor"></asp:Label>
                            <asp:TextBox ID="txtVrNotaFiscal" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="100" Width="100%" CssClass="configtext" ReadOnly="true"></asp:TextBox>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div id="tbBotao" runat="server" class="scrollmenu">
        <div class="btn-menu-toolbar divEspaco"></div>
        <asp:LinkButton ID="btVoltar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-arrow-left"></i>
            <br />
            <span>Voltar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btLimpar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file"></i>
            <br />
            <span>Novo</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
        <%--<asp:LinkButton ID="btDesativar" runat="server" CssClass="btn-menu-toolbar" OnClientClick="return confirm('Você deseja desativa o registro?');">
            <i class="fas fa-recycle"></i>
            <br />
            <span id="lblEncerrar" runat="server">Excluir</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btPDF" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-file-pdf"></i>
            <br />
            <span id="lblPdf" runat="server">PDF</span>
        </asp:LinkButton>--%>
    </div>

</asp:Content>




