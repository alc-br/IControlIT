<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Marcacao.aspx.vb" Inherits="IControlIT.Marcacao" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--filtro ********************************************************************************** -->
    <div id="pnlFiltro" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblCLassificar" runat="server" CssClass="configlabel" Text="Classificar" Style="float: none; left: 5px; position: relative; top: 0px;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblClassificarPor" runat="server" Text="Classificar Por"></asp:Label>
                    <asp:DropDownList ID="cboColuna" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" AutoPostBack="True">
                        <asp:ListItem> </asp:ListItem>
                        <asp:ListItem Value="Destino">Identificação</asp:ListItem>
                        <asp:ListItem Value="Tipo">Descrição</asp:ListItem>
                        <asp:ListItem Value="Data">Data Hora</asp:ListItem>
                        <asp:ListItem Value="Consumo">Consumo</asp:ListItem>
                        <asp:ListItem Value="Custo">Valor</asp:ListItem>
                        <asp:ListItem Value="Usuario">Usuários</asp:ListItem>
                        <asp:ListItem Value="Agenda">Contato</asp:ListItem>
                        <asp:ListItem Value="Dia_Semana">Dia</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblOrderm" runat="server" Text="Ordem"></asp:Label>
                    <asp:DropDownList ID="cboOrdernacao" runat="server" CssClass="configCombo" EnableTheming="True" Style="width: 100%">
                        <asp:ListItem Value="1">De A a Z</asp:ListItem>
                        <asp:ListItem Value="2">De Z a A</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblTipo1" runat="server" Text="Filtro de Texto"></asp:Label>
                    <asp:TextBox ID="txtOrdenacao" runat="server" CssClass="configtext" BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Width="100%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvColuna" runat="server" ControlToValidate="cboColuna" Display="None" Enabled="False" Font-Names="Arial" Font-Size="8pt" SetFocusOnError="True" ForeColor="Red" Style="float: left;">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFechar" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="BtOrdenar" class="btn btn-primary" runat="server" Text="Mostrar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--### Msg para tela fazia ****************************************************************** -->
    <div id="pnlDetalhe" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label1" runat="server" Style="float: left" CssClass="configlabel" Text="* Não existe informações para detalhamento!"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFecharMsgPopup" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" OnClick="btFecharMsgPopup_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- ### Tipo de Bilhete ********************************************************************* -->
    <div id="pnlMsg" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblDesc_Tipo" runat="server" CssClass="configlabel" Text="Dados do Bilhete" Style="float: none; left: 5px; position: relative; top: 0px;" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblTipo" runat="server" CssClass="configlabel" Text="Tipo:"></asp:Label>
                    <asp:TextBox ID="lblTextoTipo" runat="server" CssClass="configtext" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="Descrição:"></asp:Label>
                    <asp:TextBox ID="lblTextoDescricao" runat="server" CssClass="configtext" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblDiaSemana" runat="server" CssClass="configlabel" Text="Dia da Semana"></asp:Label>
                    <asp:TextBox ID="txtDiaSemana" runat="server" CssClass="configtext" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblOperadoraSainte" runat="server" CssClass="configlabel" Text="Operadora Sainte"></asp:Label>
                    <asp:TextBox ID="txtOperadoraSainte" runat="server" CssClass="configtext" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFecharmsg" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!-- ### Custo Fixo ************************************************************************** -->
    <div id="pnlCustoFixo" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescCustoFixo" runat="server" CssClass="configlabel" Text="Custo Fixo" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div id="Div4" runat="server" style="overflow: auto; width: 100%;" title=" ">
                        <asp:DataGrid ID="dtgCustoFixo" runat="server" BorderColor="#0062B1" AutoGenerateColumns="False" EnableTheming="True" HorizontalAlign="Left" PageSize="1" Style="font-size: 8pt; color: black; font-family: Arial" Width="100%" GridLines="None">
                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="Nm_Custo_Fixo" HeaderText="Descrição"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Custo" HeaderText="Valor"></asp:BoundColumn>
                            </Columns>
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#000000" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="#000000" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFecharCustoFixo" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!-- Cadastro Agenda ********************************************************************* -->
    <div id="pnlAgenda" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblACTitulo" runat="server" CssClass="configlabel" Text="Cadastrar número na agenda" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblDestino" runat="server" CssClass="configlabel" Text="ID:"></asp:Label>
                    <asp:TextBox ID="lblNumero" runat="server" CssClass="configtext" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblContato" runat="server" CssClass="configlabel" Text="Contato:"></asp:Label>
                    <asp:TextBox ID="txtContato" runat="server" CssClass="configtext" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div>&nbsp;</div>
            <div class="row">
                <div class="col-md-12">
                    <asp:CheckBox ID="chkParticularContato" runat="server" Style="float: left" Text="Particular" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btACFechar" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btGravaAgenda" class="btn btn-primary" runat="server" Text="Salvar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!-- Excedente *************************************************************************** -->
    <div id="pnlExcedente" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblDescExcedente" runat="server" CssClass="configlabel float-none" Text="Excedente de Cota" Font-Names="Microsoft JhengHei Light" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblSubDescTotal" runat="server" CssClass="configlabel" Text="Total da Conta:"></asp:Label>
                    <asp:TextBox ID="lblSubValorTotal" runat="server" CssClass="configtext" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblSubDescParticular" runat="server" CssClass="configlabel" Text="Particular:"></asp:Label>
                    <asp:TextBox ID="lblSubValorParticular" runat="server" CssClass="configtext" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblSubDescServico" runat="server" CssClass="configlabel" Text="Serviço:"></asp:Label>
                    <asp:TextBox ID="lblSubValorServico" runat="server" CssClass="configtext" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblSubDescTrafego" runat="server" CssClass="configlabel" Text="Utilização:"></asp:Label>
                    <asp:TextBox ID="lblSubValorTrafego" runat="server" CssClass="configtext" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblSubDescPolitica" runat="server" CssClass="configlabel" Text="Orçamento/Cota:"></asp:Label>
                    <asp:TextBox ID="lblSubValorPolitica" runat="server" CssClass="configtext" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblSubDescExcedente" runat="server" CssClass="configlabel" Text="Excedente:"></asp:Label>
                    <asp:TextBox ID="lblSubValorExcedente" runat="server" CssClass="configtext" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lbSubDescJustificativa" runat="server" CssClass="configlabel" Text="Justificativa:"></asp:Label>
                    <asp:TextBox ID="txtJustificativa" runat="server" CssClass="configtext" MaxLength="300" Width="100%" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtJustificativa" ErrorMessage="*" Height="16px" Style="font-size: 10pt; z-index: 132; color: red; font-family: Arial; width: 10px; float: Left;" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFecharExcedente" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                    <asp:Button ID="btEnviar" class="btn btn-success" runat="server" Text="Enviar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!-- Pagina ****************************************************************************** -->
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblVolume" runat="server" Font-Names="Microsoft JhengHei Light" Font-Size="14pt" ForeColor="#818181" Text="Consumo por Tipo"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 text-left">
                            <asp:Label ID="lblAtivo" runat="server" CssClass="configlabel">Ativo: </asp:Label>
                            <asp:Label ID="txtAtivo" runat="server" CssClass="configlabel" Font-Bold="True"></asp:Label>
                        </div>
                        <div class="col-md-6 text-right">
                            <asp:Label ID="lbl1" runat="server" CssClass="configlabel" Style="float: none">Mês: </asp:Label>
                            <asp:Label ID="txtLote" runat="server" CssClass="configlabel" Font-Bold="True" Style="float: none"></asp:Label>
                        </div>
                    </div>
                    <!-- volume e custo por tipo -->
                    <div class="row">
                        <div class="col-md-12">
                            <div id="DivVolume" runat="server" style="overflow: auto">
                                <asp:DataGrid ID="dtgVolume" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" BorderStyle="Solid" BorderWidth="1px">
                                    <Columns>
                                        <asp:BoundColumn DataField="Tipo" HeaderText="Tipo">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Volume" HeaderText="Consumo" DataFormatString="{0:##########0}">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Valor" HeaderText="Valor" DataFormatString="{0:R$##########0.#0}">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                    </Columns>

                                    <PagerStyle Mode="NumericPages" />
                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <AlternatingItemStyle BackColor="#E0E0E0" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-right">
                            <asp:Label ID="lblTotal" runat="server" CssClass="configlabel" Font-Bold="False" Style="float: none;" Text="Total da Conta: "></asp:Label>
                            <asp:Label ID="txtLigacaoTotal" runat="server" CssClass="configlabel" Style="float: right;" Font-Bold="True">0</asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <!-- Detalhamento da Conta -->
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblDetalhamento" runat="server" Font-Names="Microsoft JhengHei Light" Font-Size="14pt" ForeColor="#333333" Text="Detalhamento"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:CheckBox ID="chkCustoZero" runat="server" AutoPostBack="True" Text="Mostrar trafego sem custo" ForeColor="Black" Style="float: Left" Width="180px" Font-Bold="False" Font-Names="Calibri Light" Font-Size="10pt" />
                            <asp:Label ID="lblTermo" runat="server" Font-Names="Calibri Light" Font-Size="10pt" ForeColor="Red" Text="* Falta Termo de Entrega." Visible="True"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="Div1" runat="server" style="overflow: auto">
                                <asp:DataGrid ID="dtgBilhete" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px" PageSize="6" AllowPaging="True">
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkMarcar" runat="server" AutoPostBack="True" OnCheckedChanged="chkMarcar_CheckedChanged" Checked='<%# Bind("Marcar") %>' Enabled='<%# Bind("Reload") %>' Font-Names="Arial" Font-Size="8pt" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Identificação">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtDestino" runat="server" OnClick="lbtDestino_Click" Text="<%# Bind('Destino') %> " TextMode="MultiLine" Width="150px" Visible="false">LinkButton</asp:LinkButton>
                                                <asp:TextBox ID="txtDestino" runat="server" TextMode="MultiLine" CssClass="configtext" Text="<%# Bind('Destino') %> " MaxLength="50" Width="150px" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Agenda" HeaderText="Contato"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Usuario" Visible="False" HeaderText="Usuários"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Data" Visible="True" HeaderText="Data/Hora"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Dia_Semana" Visible="False" HeaderText="Dia"></asp:BoundColumn>
                                        <asp:ButtonColumn DataTextField="Tipo" HeaderText="Descrição" Text="Delete" CommandName="Delete">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#003399" />
                                        </asp:ButtonColumn>
                                        <asp:BoundColumn DataField="Custo" Visible="True" HeaderText="Valor" DataFormatString="{0:R$##########0.#0}">
                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_Bilhete" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Id_Bilhete_Tipo" HeaderText="Id_Bilhete_Tipo" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Grupo" HeaderText="Grupo" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Desmarcar" HeaderText="Desmarcar" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consumo" Visible="True" HeaderText="Consumo">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Destino" HeaderText="Destino" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="OP_Correta" HeaderText="OP_Correta" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DB_Operadora_Sainte" Visible="False" HeaderText="Sainte">
                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Nm_Ativo_Tipo_Grupo" Visible="False" HeaderText="Nm_Ativo_Tipo_Grupo">
                                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>

                                    <PagerStyle Visible="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <AlternatingItemStyle BackColor="#E0E0E0" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />

                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">&nbsp;</div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-right">
                            <ul style="list-style-type: none">
                                <li style="display: inline-block">
                                    <asp:LinkButton ID="btFiltro" runat="server" OnClick="btFiltro_Click">
                                        <i class="fas fa-filter" style="font-size: 18pt"></i>
                                    </asp:LinkButton>
                                </li>
                                <li style="display: inline-block">
                                    <asp:LinkButton ID="btPagAnterior" runat="server" OnClick="btPagAnterior_Click">
                                        <i class="fas fa-step-backward" style="font-size: 20pt"></i>
                                    </asp:LinkButton>
                                </li>
                                <li style="display: inline-block">
                                    <asp:LinkButton ID="btPagPrimerio" runat="server" OnClick="btPagPrimerio_Click">
                                        <i class="fas fa-backward" style="font-size: 20pt"></i>
                                    </asp:LinkButton>
                                </li>
                                <li style="display: inline-block">
                                    <asp:TextBox ID="txtPagCont" runat="server" AutoPostBack="True" Text="1" Style="width: 36px; padding: 5px; text-align: center; border: 1px solid #818181; border-radius: 4px" Font-Names="Arial" Font-Size="14pt" ForeColor="#818181" MaxLength="3" OnTextChanged="txtPagCont_TextChanged"></asp:TextBox>
                                </li>
                                <li style="display: inline-block">
                                    <asp:TextBox ID="lblPagTotal" runat="server" AutoPostBack="True" Text="1" Style="width: 36px; text-align: center" Font-Names="Arial" Font-Size="14pt" ForeColor="#818181" ReadOnly="true" MaxLength="3" OnTextChanged="txtPagCont_TextChanged"></asp:TextBox>
                                </li>
                                <li style="display: inline-block">
                                    <asp:LinkButton ID="btPagUltimo" runat="server" OnClick="btPagUltimo_Click">
                                        <i class="fas fa-forward" style="font-size: 20pt"></i>
                                    </asp:LinkButton>
                                </li>
                                <li style="display: inline-block">
                                    <asp:LinkButton ID="btPagProximo" runat="server" OnClick="btPagProximo_Click">
                                        <i class="fas fa-step-forward" style="font-size: 20pt"></i>
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">&nbsp;</div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <ul style="list-style-type: none">
                                <li style="display: inline-block">
                                    <asp:Label ID="lblLegFinalSemana" runat="server" BackColor="#B4009E" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Width="15px" Height="15px"></asp:Label>
                                    <asp:Label ID="lblFinalSemana" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: Left" Text="Final de semana"></asp:Label>
                                </li>
                                <li style="display: inline-block">
                                    <asp:Label ID="lblLegForaComercial" runat="server" BackColor="#FBAA19" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Width="15px" Height="15px"></asp:Label>
                                    <asp:Label ID="lblForaComercial" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: Left" Text="Fora do horário"></asp:Label>
                                </li>
                                <li style="display: inline-block">
                                    <asp:Label ID="lblLegParaGrupo" runat="server" BackColor="#64BE00" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Width="15px" Height="15px"></asp:Label>
                                    <asp:Label ID="lblParaGrupo" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: left" Text="Para o grupo"></asp:Label>
                                </li>
                                <li style="display: inline-block">
                                    <asp:Label ID="lblLegPacote" runat="server" BackColor="#EF3B28" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="15px" Height="15px"></asp:Label>
                                    <asp:Label ID="lblPacote" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: left" Text="Não descontado"></asp:Label>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:HiddenField ID="hfdMarcaLigacao" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <div class="card">
                <div class="card-body">
                    <asp:Label ID="lblValorPolitica" runat="server" CssClass="configlabel" Font-Bold="False" Style="float: left;" Text="ORÇADO: "></asp:Label>
                    <asp:Label ID="txtValorPolitica" runat="server" CssClass="configlabel" Style="float: right;" Font-Bold="True">R$0,00</asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card">
                <div class="card-body">
                    <asp:Label ID="lblLigacaoParticular" runat="server" CssClass="configlabel" Font-Bold="False" Style="float: left;" Text="PARTICULAR: "></asp:Label>
                    <asp:Label ID="txtMarcado" runat="server" CssClass="configlabel" Style="float: right;" Font-Bold="True" BackColor="#000066" ForeColor="White">R$0,00</asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card">
                <div class="card-body">
                    <asp:LinkButton ID="btUsuarioGrupo" runat="server" Style="float: left" OnClick="btUsuarioGrupo_Click">
                        <i class="fas fa-eye" style="font-size: 14pt"></i>
                    </asp:LinkButton>
                    <asp:Label ID="lblFixo" runat="server" CssClass="configlabel" Font-Bold="False" Text="CUSTO FIXO: "></asp:Label>
                    <asp:Label ID="lblCustoFixo" runat="server" CssClass="configlabel" Style="float: right;" Font-Bold="True">R$0,00</asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card">
                <div class="card-body">
                    <asp:Label ID="lblGeral" runat="server" CssClass="configlabel" Font-Bold="False" Style="float: left;" Text="TOTAL GERAL: "></asp:Label>
                    <asp:Label ID="lblTotalGeral" runat="server" CssClass="configlabel" Style="float: right;" Font-Bold="True">R$0,00</asp:Label>
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
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="true" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-download"></i>
            <br />
            <span>Exportar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btSincronizaCont" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="true" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-sync-alt"></i>
            <br />
            <span>Sinc/Agenda</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAprovar_Barra" runat="server" CssClass="btn-menu-toolbar" Visible="true" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-thumbs-up"></i>
            <br />
            <span>Aprovar</span>
        </asp:LinkButton>
    </div>

</asp:Content>


