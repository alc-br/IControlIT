<%@ Control Language="VB" AutoEventWireup="false" CodeBehind="Filtro_Acesso.ascx.vb" Inherits="IControlIT.Filtro_Acesso" %>

<style type="text/css">
    INPUT {
        border: 0px none transparent;
        background-repeat: no-repeat;
    }
</style>

<script type="text/javascript" src="JScript.js"></script>

<div id="DivFiltro" runat="server" style="width: 100%; overflow: auto;" title=" ">
    <table style="width: 100%">
        <tr>
            <td>
                <table style="width: 100%;" border="0" cellspacing="0" cellpadding="0">
                    <tr id="pnlFilial" runat="server">
                        <td>
                            <asp:DropDownList ID="cboFilial" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" Height="35px" TabIndex="1" AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="lnCentroCusto" runat="server" visible="false">
                        <td style="height: 5px"></td>
                    </tr>
                    <tr id="pnlCentroCusto" runat="server">
                        <td>
                            <asp:DropDownList ID="cboCentro_Custo" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" Height="35px" TabIndex="2" AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="lnDepartamento" runat="server" visible="false">
                        <td style="height: 5px"></td>
                    </tr>
                    <tr id="pnlDepartamento" runat="server">
                        <td>
                            <asp:DropDownList ID="cboDepartamento" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" Height="35px" TabIndex="3" AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="lnSetor" runat="server" visible="false">
                        <td style="height: 5px"></td>
                    </tr>
                    <tr id="pnlSetor" runat="server">
                        <td>
                            <asp:DropDownList ID="cboSetor" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" Height="35px" TabIndex="4" AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="lnSecao" runat="server" visible="false">
                        <td style="height: 5px"></td>
                    </tr>
                    <tr id="pnlSecao" runat="server">
                        <td>
                            <asp:DropDownList ID="cboSecao" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" Height="35px" TabIndex="5" AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="lnConsumidor" runat="server" visible="false">
                        <td style="height: 5px"></td>
                    </tr>
                    <tr id="pnlConsumidor" runat="server">
                        <td>
                            <table style="width: 100%;" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="cboConsumidor" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" Height="35px" TabIndex="6" AutoPostBack="True"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtConsumidor" runat="server" placeholder="Pesquisar pelo nome do colaborador" CssClass="configtext" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td>
                <table style="width: 100%; height: 32px">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btSistema" runat="server" CssClass="nav-link" ToolTip="Refresh">
                                <i class="fas fa-redo-alt" style="font-size: 24pt; color: #4FC3F7"></i>
                            </asp:LinkButton>
                        </td>

                        <td>
                            <asp:LinkButton ID="btMovel" runat="server" CssClass="nav-link" ToolTip="Filtro Móvel">
                                <i class="fas fa-mobile-alt" style="font-size: 24pt; color: #4FC3F7"></i>
                            </asp:LinkButton>
                        </td>

                        <td>
                            <asp:LinkButton ID="btFixa" runat="server" CssClass="nav-link" ToolTip="Filtro Fixa">
                                <i class="fas fa-phone-alt" style="font-size: 24pt; color: #4FC3F7"></i>
                            </asp:LinkButton>
                        </td>

                        <td>
                            <asp:LinkButton ID="btDados" runat="server" CssClass="nav-link" ToolTip="Filtro Dados">
                                <i class="fas fa-cloud" style="font-size: 24pt; color: #4FC3F7"></i>
                            </asp:LinkButton>
                        </td>

                        <td>
                            <asp:LinkButton ID="btDesktop" runat="server" CssClass="nav-link" ToolTip="Filtro Desktop">
                                <i class="fas fa-server" style="font-size: 24pt; color: #4FC3F7"></i>
                            </asp:LinkButton>
                        </td>

                        <td>
                            <asp:LinkButton ID="btPrint" runat="server" CssClass="nav-link" ToolTip="Filtro Impressão">
                                <i class="fas fa-print" style="font-size: 24pt; color: #4FC3F7"></i>
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
