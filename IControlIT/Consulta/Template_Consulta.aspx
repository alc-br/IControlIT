<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Template_Consulta.aspx.vb" Inherits="IControlIT.Template_Consulta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Filtro_Acesso.ascx" TagName="Filtro_Acesso" TagPrefix="uc1" %>
<%@ Register Src="../Grid.ascx" TagName="Grid" TagPrefix="uc3" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- menssagem obervacao -->
    <div id="pnlMenssagem" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblObservacao" runat="server" Style="float: left" CssClass="configlabel" Text="É necessário que você selecione um filtro."></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btOk" class="btn btn-success" Width="80px" runat="server" Text="OK" CausesValidation="False" />
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
                            <table style="width: 100%">
                                <tr>
                                    <td id="lnDe" runat="server">
                                        <asp:Label ID="Label17" runat="server" CssClass="configlabel" Text="De:" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                                        <asp:DropDownList ID="cboDataDe" runat="server" Style="float: left" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDe" runat="server" ControlToValidate="cboDataDe" Display="None" Font-Names="Arial" Font-Size="8pt" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td id="lnAte" runat="server">
                                        <asp:Label ID="Label18" runat="server" CssClass="configlabel" Text="Até:" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                                        <asp:DropDownList ID="cboDataAte" runat="server" Style="float: left" Width="100%" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAte" runat="server" ControlToValidate="cboDataAte" Display="None" Font-Names="Arial" Font-Size="8pt" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td align="right" style="width: 130px; padding-top: 20px;">
                                        <asp:Button ID="btExecutar" class="btn btn-success" runat="server" Text="Executar" CausesValidation="False" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="height: 5px">
                        <asp:HiddenField ID="hdvTipo_Grafico" runat="server" />
                    </div>
                    <div id="lnFiltro" runat="server" class="row">
                        <div class="col-md-12">
                            <uc1:Filtro_Acesso ID="Filtro_Acesso" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr />
                        </div>
                    </div>
                    <div id="conteudo" runat="server" visible="false" class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblDescricaoArquivo" runat="server" CssClass="configlabel" Style="left: 9px; top: 13px; float: left;" ForeColor="#818181"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <uc3:Grid ID="Grid1" runat="server" />
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
    </div>

</asp:Content>
