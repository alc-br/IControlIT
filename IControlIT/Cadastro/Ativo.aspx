<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Ativo.aspx.vb" Inherits="IControlIT.Ativo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--Confirmação-->
    <div id="pnlConfirmacao" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="configlabel" Text="Lixeira" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblMenssagem" runat="server" CssClass="configlabel" Text="Este Ativo está desativado. Deseja restaurar antes de continuar?" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btContinuar" class="btn btn-default" runat="server" Text="Não" CausesValidation="False" />
                    <asp:Button ID="btRestaurar" class="btn btn-success" runat="server" Text="Sim" CausesValidation="False" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <!--observacao-->
    <div id="pnlObservacao" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDLinhaConta" runat="server" CssClass="configlabel" Text="Observação" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                    <asp:TextBox ID="txtObservacaoObrigarotia" runat="server" CssClass="configtext" MaxLength="300" Style="float: left; border-radius: 6px 6px;" TextMode="MultiLine" Width="100%" Height="350px" TabIndex="7"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvObservacao" runat="server" ControlToValidate="txtObservacaoObrigarotia" Font-Names="Arial" Font-Size="10pt" Style="left: 445px; top: 38px; float: left;" ForeColor="Red">*</asp:RequiredFieldValidator>
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

    <!--registro adicionais-->
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
                    <asp:Label ID="lblIdentificacao" runat="server" CssClass="configlabel" Text="Chave do banco" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:TextBox ID="txtIdentificacao" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
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
    <div id="divContent" runat="server" class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblNumeroAtivo" runat="server" CssClass="configlabel" Text="* Número do ativo"></asp:Label>
                            <asp:TextBox ID="txtNumeroAtivo" runat="server" CssClass="configtext" MaxLength="50" Width="100%" TabIndex="1" ForeColor="#006600"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNumeroAtivo" runat="server" ControlToValidate="txtNumeroAtivo" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblAtivoTipo" runat="server" CssClass="configlabel" Text="* Tipo do ativo"></asp:Label>
                            <asp:DropDownList ID="cboAtivoTipo" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="3" AutoPostBack="True" Width="100%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAtivoTipo" runat="server" ControlToValidate="cboAtivoTipo" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblConglomerado" runat="server" CssClass="configlabel" Text="* Fornecedor"></asp:Label>
                            <asp:DropDownList ID="cboConglomerado" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="4" Width="100%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvConglomerado" runat="server" ControlToValidate="cboConglomerado" Display="None" SetFocusOnError="True" Style="left: 445px; top: 38px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblAtivoModelo" runat="server" CssClass="configlabel" Text="Modelo do ativo"></asp:Label>
                            <asp:DropDownList ID="cboAtivoModelo" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="5" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblAtivoStatus" runat="server" CssClass="configlabel" Text="Status do ativo"></asp:Label>
                            <asp:DropDownList ID="cboAtivoStatus" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="6" Width="100%"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDataAtivacao" runat="server" CssClass="configlabel" Text="Entrega do termo"></asp:Label>
                            <asp:TextBox ID="txtDataAtivacao" runat="server" MaxLength="19" Width="100%" CssClass="configtext" TabIndex="7"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevDataAtivacao" runat="server" ControlExtender="meeDataAtivacao" ControlToValidate="txtDataAtivacao" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevDataAtivacao" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblSuspenssao" runat="server" CssClass="configlabel" Text="Retorno Suspensão"></asp:Label>
                            <asp:TextBox ID="txtSuspenssao" runat="server" MaxLength="19" Width="100%" CssClass="configtext" TabIndex="7"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevSuspenssao" runat="server" ControlExtender="meeSuspenssao" ControlToValidate="txtSuspenssao" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevSuspenssao" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"></cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblEquipamento" runat="server" CssClass="configlabel" Text="Equipamento"></asp:Label>
                            <asp:TextBox ID="txtEquipamento" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="5" Width="100%" CssClass="configtext" ReadOnly="True" ForeColor="#006600" Font-Bold="False" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblEndereco" runat="server" CssClass="configlabel" Text="Endereço"></asp:Label>
                            <asp:TextBox ID="txtEndereco" runat="server" BorderStyle="Solid" BorderWidth="1px"  Width="100%" CssClass="configtext"  ForeColor="#006600" Font-Bold="False"></asp:TextBox>
                        </div>      
                        <div class="col-md-6">
                            <asp:Label ID="lblSimCard" runat="server" CssClass="configlabel" Text="Numero Sim Card"></asp:Label>
                            <asp:TextBox ID="txtSimCard" runat="server" BorderStyle="Solid" BorderWidth="1px"  Width="100%" CssClass="configtext"  ForeColor="#006600" Font-Bold="False"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblVlrContrato" runat="server" CssClass="configlabel" Text="Valor do contrato"></asp:Label>
                            <asp:TextBox ID="txtVlrContrato" runat="server" BorderStyle="Solid" BorderWidth="1px"  Width="100%" CssClass="configtext"  ForeColor="#006600" Font-Bold="False"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblPlanoContrato" runat="server" CssClass="configlabel" Text="Plano do contrato"></asp:Label>
                            <asp:TextBox ID="txtPlanoContrato" runat="server" BorderStyle="Solid" BorderWidth="1px"  Width="100%" CssClass="configtext"  ForeColor="#006600" Font-Bold="False"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lvlVelocidade" runat="server" CssClass="configlabel" Text="Velocidade"></asp:Label>
                            <asp:TextBox ID="txtVelocidade" runat="server" BorderStyle="Solid" BorderWidth="1px"  Width="100%" CssClass="configtext"  ForeColor="#006600" Font-Bold="False"></asp:TextBox>
                        </div>
                    </div>
                    <div style="height: 5px"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="divGrupo" runat="server" style="width: 100%; height: 105px; overflow: auto" title=" ">
                                <asp:DataGrid ID="dtgUsuario" runat="server" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" HorizontalAlign="Center" Width="100%" AutoGenerateColumns="False" BorderColor="#CCCCCC" CellPadding="5" CellSpacing="5" BorderWidth="1px" GridLines="Horizontal">
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <PagerStyle Mode="NumericPages" />
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />

                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <asp:ImageButton ID="btInserir" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Add.png" CausesValidation="False" OnClick="btInserir_Click" Height="22px" Width="22px" />
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:ImageButton ID="btExcluir" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Deletar.png" OnClientClick="return confirm('Desativa Registro?');" Visible="<%# Bind('Excluir') %>" CausesValidation="False" OnClick="btExcluir_Click" Height="22px" Width="22px" />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Colaborador ou Login">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cboConsumidor" runat="server" Visible="false" CssClass="configCombo" EnableTheming="True" Style="width: 100%; height: 30px" TabIndex="2" OnSelectedIndexChanged="cboConsumidor_SelectedIndexChanged1" AutoPostBack="True"></asp:DropDownList>
                                                <asp:TextBox ID="txtDescricao" Text="<%# Bind('Nm_Consumidor') %>" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="50" Width="100%" CssClass="configtext"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Ativação">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLote_Ativacao" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="19" Text='<%# Bind("Dt_Hr_Ativacao") %>' Width="100%" CssClass="configtext"></asp:TextBox>
                                                <cc1:CalendarExtender ID="ceLote_Ativacao" runat="server" Format="dd/MM/yyyy HH:mm:ss" TargetControlID="txtLote_Ativacao"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btTermo" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Word.png" CausesValidation="False" OnClientClick="<%# Bind('Termo_Ativacao') %>" Enabled="<%# Bind('bt_Termo_Ativacao') %>" Height="22px" Width="22px" />
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Devolução">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLote_Devolucao" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="30px" MaxLength="19" Text='<%# Bind("Dt_Hr_Desativacao") %>' Width="100%" CssClass="configtext"></asp:TextBox>
                                                <cc1:CalendarExtender ID="ceLote_Devolucao" runat="server" Format="dd/MM/yyyy HH:mm:ss" TargetControlID="txtLote_Devolucao"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btDevolucao" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Word.png" CausesValidation="False" OnClientClick="<%# Bind('Termo_Devolucao') %>" Enabled="<%# Bind('bt_Termo_Devolucao') %>" Height="22px" Width="22px" />
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>

                                        <asp:BoundColumn DataField="Id_Consumidor" HeaderText="Id_Consumidor" Visible="False">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btLupa" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Lupa.png" Visible="<%# Bind('Lupa') %>" OnClick="btLupa_Click" CausesValidation="False" Height="22px" Width="22px" />
                                                <asp:ImageButton ID="btVoltar" runat="server" ImageUrl="~/Img_Sistema/Botao/Grid/Grid_Editar.png" Visible="<%# Bind('Voltar') %>" CausesValidation="False" OnClick="btVoltar_Click" Height="22px" Width="22px" />
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>
                                    </Columns>

                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="False" BackColor="#EEEEEE" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="DivAtivo" runat="server" style="overflow: auto; width: 100%; height: 140px;">
                                <asp:DataGrid ID="dtgDadosComplemento" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="5" EnableTheming="True" HorizontalAlign="Left" PageSize="1" ShowHeader="False" Width="100%" GridLines="None">
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblBindComplemento" runat="server" Text="<%# Bind('Nm_Ativo_Complemento') %>" Width="100%" Style="float: left; text-align: left" CssClass="configlabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtBindComplemento" Text="<%# Bind('Descricao') %>" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Width="100%" CssClass="configtext"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 642px; position: absolute; top: 685px; height: 41px; width: 133px;">
        <cc1:ValidatorCalloutExtender ID="vceNumeroAtivo" runat="server" TargetControlID="rfvNumeroAtivo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceAtivoTipo" runat="server" TargetControlID="rfvAtivoTipo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceConglomerado" runat="server" TargetControlID="rfvConglomerado"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeDataAtivacao" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataAtivacao"></cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="meeSuspenssao" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSuspenssao"></cc1:MaskedEditExtender>
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
        <asp:LinkButton ID="btDesativar" runat="server" CssClass="btn-menu-toolbar" OnClientClick="return confirm('Você deseja desativa o registro?');" OnClick="btDesativar_Click">
            <i class="fas fa-recycle"></i>
            <br />
            <span id="lblEncerrar" runat="server">Excluir</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btConfiguracao" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false">
            <i class="fas fa-cog"></i>
            <br />
            <span>Config</span>
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

