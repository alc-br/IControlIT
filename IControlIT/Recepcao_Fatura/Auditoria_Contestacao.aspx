<%@ Page Language="vb" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Auditoria_Contestacao.aspx.vb" Inherits="IControlIT.Auditoria_Contestacao" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <asp:Button ID="btnDados" runat="server" class="btn-tab pull-left" Text="Dados" CausesValidation="False" OnClick="btnConta_Click" />
                    <asp:Button ID="btnResumo" runat="server" class="btn-tab-disable pull-left" Text="Resumo" CausesValidation="False" OnClick="btnAcompanhamento_Click" />
                </div>
            </nav>
            <div class="card-body" style="background-color: #FFFFFF; border-radius: 6px; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px">

                <!--Dados-->
                <div id="divDados" runat="server" class="row" visible="true">
                    <div class="col-md-12">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblDados" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Dados para Processamento" Style="float: none"></asp:Label>
                                </div>
                            </div>
                            <div style="height: 5px"></div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblOperadora" runat="server" CssClass="configlabel" Text="*Operadora"></asp:Label>
                                    <asp:DropDownList ID="cboOperadora" runat="server" Width="100%" TabIndex="1" CssClass="configCombo"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblDataLote" runat="server" CssClass="configlabel" Text="*Mês Lote"></asp:Label>
                                    <asp:DropDownList ID="cboDataLote" runat="server" Width="100%" AutoPostBack="True" TabIndex="2" CssClass="configCombo"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblSelecTodos" runat="server" CssClass="configlabel" Text="Selecionar todos:  "></asp:Label>
                                    <asp:CheckBox ID="chkSelecTodos" runat="server" AutoPostBack="true" CssClass="configchekbox" Style="position: relative; top: 8px" Font-Bold="True" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblFatura" runat="server" CssClass="configlabel" Text="*Fatura"></asp:Label>
                                    <div id="DivFatura" runat="server" style="border: 1px solid #CCCCCC; z-index: 103; left: 5px; overflow: auto; width: 100%; height: 140px; border-radius: 6px 0px 0px 6px;" visible="true">
                                        <asp:CheckBoxList ID="optFatura" runat="server" CssClass="configchekbox" AutoPostBack="True" ForeColor="Black">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
<%--                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="Label1" runat="server" CssClass="configlabel" Text="Tipo do Rateio"></asp:Label>
                                    <div id="DivRateio" runat="server" style="border: 1px solid #CCCCCC; z-index: 103; left: 5px; overflow: auto; width: 100%; height: 40px; border-radius: 6px 0px 0px 6px;">
                                        <asp:CheckBoxList ID="chkRateio" runat="server" CssClass="configchekbox" ForeColor="Black" Height="24px" Enabled="False"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Style="float: left;" Text="Descrição" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="configtext" MaxLength="50" TabIndex="3" Width="100%" ReadOnly="True"></asp:TextBox>
                                    <asp:HiddenField ID="hdfId_Rateio" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblObservacao" runat="server" CssClass="configlabel" Style="left: 9px; top: 39px; float: left;" Text="Observação" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtObservacao" runat="server" CssClass="configtext" Height="65px" MaxLength="8000" TextMode="MultiLine" Width="100%" ReadOnly="True" ForeColor="White"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--Resumo-->
                <div id="divResumo" runat="server" class="row" visible="false">
                    <div class="col-md-12">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblResumo" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Resumo dos Calculos" Style="float: none"></asp:Label>
                                </div>
                            </div>
                            <div style="height: 5px"></div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="DivAtivoPadrao" runat="server" style="border: 1px solid #CCCCCC; width: 100%; max-height: 120px; overflow: auto" title=" ">
                                        <asp:DataGrid ID="dtgAtivo_Critica" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                            EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                            HorizontalAlign="Center" PageSize="5" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                                            <Columns>
                                                <asp:BoundColumn DataField="Id_Bilhete" HeaderText="Id_Bilhete" Visible="False"></asp:BoundColumn>

                                                <asp:TemplateColumn HeaderText="Número Ativo">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNrAtivo" runat="server" CssClass="configtext" Width="200px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btInsere" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Add.png" Height="18px" OnClientClick="return confirm('Desativa Registro?');" />
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:TemplateColumn>

                                                <asp:BoundColumn DataField="Nm_Bilhete_Tipo" HeaderText="Tipo" Visible="True"></asp:BoundColumn>

                                                <asp:BoundColumn DataField="Nm_Bilhete_Descricao" HeaderText="Descricao" Visible="True"></asp:BoundColumn>

                                                <asp:BoundColumn DataField="DB_Custo" DataFormatString="{0:R$##########0.#0}" HeaderText="Valor" Visible="True">
                                                    <HeaderStyle Width="100px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                            </Columns>

                                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                            <PagerStyle Mode="NumericPages" />

                                        </asp:DataGrid>
                                    </div>
                                </div>
                            </div>
                            <div class="row text-left">
                                <div class="col-md-3">
                                    <asp:Label ID="lblTotalCobrado" runat="server" CssClass="configlabel" Style="float: none;" Text="Total cobrado" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtTotalCobrado" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblTotalAuditado" runat="server" CssClass="configlabel" Style="float: none;" Text="Total auditado" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtTotalAuditado" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblTotalDiferenca" runat="server" CssClass="configlabel" Style="float: none;" Text="Total diferença" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtTotalDiferenca" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblErro" runat="server" CssClass="configlabel" Style="float: none;" Text="% erro" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtErro" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row text-left">
                                <div class="col-md-3">
                                    <asp:Label ID="lblTotalDiferencaPositivo" runat="server" CssClass="configlabel" Style="float: none;" Text="Total diferença positivo" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtTotalDiferencaPositivo" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblTotalDiferencaNegativo" runat="server" CssClass="configlabel" Style="float: none;" Text="Total diferença negativo" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtTotalDiferencaNegativo" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblErroPositivo" runat="server" CssClass="configlabel" Style="float: none;" Text="% erro positivo" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtErroPositivo" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblErroNegativo" runat="server" CssClass="configlabel" Style="float: none;" Text="% erro negativo" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtErroNegativo" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row text-left">
                                <div class="col-md-6">
                                    <asp:Label ID="lblContestacoesAnteriores" runat="server" CssClass="configlabel" Style="float: none;" Text="Total créditos contestações anteriores" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtContestacoesAnteriores" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
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
        <asp:LinkButton ID="btLimpar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btLimpar_Click">
            <i class="fas fa-file"></i>
            <br />
            <span>Novo</span>
        </asp:LinkButton>
        <%--<asp:LinkButton ID="btDesativar" runat="server" CssClass="btn-menu-toolbar" OnClientClick="return confirm('Você deseja desativa o registro?');" OnClick="btDesativar_Click">
            <i class="fas fa-trash"></i>
            <br />
            <span>Excluir</span>
        </asp:LinkButton>--%>
        <%--<asp:LinkButton ID="btConfiguracao" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-cog"></i>
            <br />
            <span>Config</span>
        </asp:LinkButton>--%>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" OnClick="btExportar_Click" CausesValidation="false">
            <i class="fas fa-upload"></i>
            <br />
            <span>Export</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExecutar" runat="server" CssClass="btn-menu-toolbar" OnClick="btExecutar_Click" CausesValidation="false">
            <i class="fas fa-play-circle"></i>
            <br />
            <span>Executar</span>
        </asp:LinkButton>
    </div>

</asp:Content>
