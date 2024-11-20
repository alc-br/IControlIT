<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Rateio.aspx.vb" Inherits="IControlIT.Rateio" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Observacao-->
    <div id="pnlObservacao" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Fatura sem Ativo ou Consumidor" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                    <asp:TextBox ID="txtObservacaoObrigatoria" runat="server" CssClass="configtext" MaxLength="30000" Style="float: left; border-radius: 6px 6px;" TextMode="MultiLine" Width="100%" Height="350px" TabIndex="7"></asp:TextBox>                    
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btCancelaModalObs" class="btn btn-success" runat="server" Text="Fechar" CausesValidation="False" UseSubmitBehavior="False" />                    
                </div>
            </div>
        </div>
    </div>

    <!--Tela *************************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <asp:Button ID="btnDados" runat="server" class="btn-tab pull-left" Text="Dados" CausesValidation="False" OnClick="btnConta_Click" />
                    <asp:Button ID="btnResumo" runat="server" class="btn-tab-disable pull-left" Text="Resumo" CausesValidation="False" OnClick="btnAcompanhamento_Click" />
                    <asp:Button ID="btnSemUsuario" runat="server" class="btn-tab-disable pull-left" Text="Rateio" CausesValidation="False" OnClick="btnStatus_Click" />
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
                                    <asp:Label ID="lblFaturaTipo" runat="server" CssClass="configlabel" Text="*Tipo"></asp:Label>
                                    <asp:DropDownList ID="cboFaturaTipo" runat="server" Width="100%" TabIndex="1" CssClass="configCombo"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblDataLote" runat="server" CssClass="configlabel" Text="*Vencimento"></asp:Label>
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
                                    <asp:Label ID="Label2" runat="server" CssClass="configlabel" Text="Salvar Rateio: "></asp:Label>
                                    <asp:CheckBox ID="chkGravaRateio" runat="server" CssClass="configchekbox" Style="position: relative; top: 8px" Font-Bold="True" ToolTip="Salvar Rateio" />
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
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="Label1" runat="server" CssClass="configlabel" Text="Tipo do Rateio"></asp:Label>
                                    <div id="DivRateio" runat="server" style="border: 1px solid #CCCCCC; z-index: 103; left: 5px; overflow: auto; width: 100%; height: 40px; border-radius: 6px 0px 0px 6px;">
                                        <asp:CheckBoxList ID="chkRateio" runat="server" CssClass="configchekbox" ForeColor="Black" Height="24px" Enabled="False"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
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
                                                        <asp:ImageButton ID="btInsere" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Add.png" Height="18px" OnClientClick="return confirm('Desativa Registro?');" OnClick="btInsere_Click" />
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
                                    <asp:Label ID="lblSPorcentagemIndice" runat="server" CssClass="configlabel" Style="float: none;" Text="% Indice de Rateio" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtSPorcentagemIndice" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSVago" runat="server" CssClass="configlabel" Style="float: none;" Text="Custo de Ativo Desativado" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtSVago" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSCarregado" runat="server" CssClass="configlabel" Style="float: none;" Text="Total Base com Usuário" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtSBilhete" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSContabilizado" runat="server" CssClass="configlabel" Style="float: none;" Text="Contabilizado" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtSRateado" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row text-left">
                                <div class="col-md-3">
                                    <asp:Label ID="lblSPacote" runat="server" CssClass="configlabel" Style="float: none;" Text="Total de Pacote" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtSPacote" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSCritica" runat="server" CssClass="configlabel" Style="float: none;" Text="Indice de Rateio" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtSCritica" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSEstoque" runat="server" CssClass="configlabel" Style="float: none;" Text="Custo do Estoque" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtSEstoque" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSAuditado" runat="server" CssClass="configlabel" Style="float: none;" Text="Total Auditado" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtSAuditado" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row text-left">
                                <div class="col-md-6">
                                    <asp:Label ID="lblSFatura" runat="server" CssClass="configlabel" Style="float: none;" Text="Total Faturado" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtSFatura" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblSDesconto" runat="server" CssClass="configlabel" Style="float: none;" Text="Total de Desconto" ForeColor="#333333"></asp:Label>
                                    <asp:TextBox ID="txtSDesconto" runat="server" CssClass="configtext" ReadOnly="True" Style="float: right; text-align: right;" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--Sem Usuário-->
                <div id="divSemUsuario" runat="server" class="row" visible="false">
                    <div class="col-md-12">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblDetalhamento" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="Larger" ForeColor="#333333" Text="Detalhamento por Ativo" Style="float: none"></asp:Label>
                                </div>
                            </div>
                            <div style="height: 5px"></div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div style="width: 100%; overflow: auto">
                                        <asp:DataGrid ID="dtgConsulta" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5"
                                            EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                            HorizontalAlign="Left" PageSize="12" Font-Size="9pt" Width="100%" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">

                                            <Columns>
                                                <asp:BoundColumn DataField="Cd_Centro_Custo" HeaderText="Centro de Custo">
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Ativo">
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Porcentagem" HeaderText="% Rateio">
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Valor_Rateado" HeaderText="Rateado" DataFormatString="{0:R$##########.#0}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" HorizontalAlign="Right" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Valor_Base" HeaderText="Custo do Usuário" DataFormatString="{0:R$##########.#0}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Id_Rateio" HeaderText="Id_Rateio" Visible="False">
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="Valor_Bilhete" HeaderText="Valor Cobrado" DataFormatString="{0:R$##########.#0}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>

                                            </Columns>

                                            <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                            <PagerStyle Mode="NumericPages" />

                                        </asp:DataGrid>
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
        <asp:LinkButton ID="btDesativar" runat="server" CssClass="btn-menu-toolbar" OnClientClick="return confirm('Você deseja desativa o registro?');" OnClick="btDesativar_Click">
            <i class="fas fa-trash"></i>
            <br />
            <span>Excluir</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btConfiguracao" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-cog"></i>
            <br />
            <span>Config</span>
        </asp:LinkButton>
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





