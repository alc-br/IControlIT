<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Consumidor.aspx.vb" Inherits="IControlIT.Consumidor" %>

<%@ MasterType VirtualPath="~/Principal.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Localizar.ascx" TagName="Localizar" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />

    <!--Confirmação-->
    <div id="pnlConfirmacao" runat="server" class="bgModal" visible="false">
        <div class="modalPopup">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="configlabel" Text="Lixeira" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblMenssagem" runat="server" CssClass="configlabel" Text="Este Consumidor está desativado. Deseja restaurar antes de continuar?" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="12pt"></asp:Label>
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
                    <asp:Label ID="Label1" runat="server" CssClass="configlabel" Text="Chave do banco" Style="float: left" Font-Names="Segoe UI" Font-Size="12pt"></asp:Label>
                    <asp:TextBox ID="txtIndentificacao" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
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
                            <asp:Label ID="lblDescricao" runat="server" CssClass="configlabel" Text="* Nome"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="120" Width="100%" CssClass="configtext" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblMatricula" runat="server" CssClass="configlabel" Text="* Registro"></asp:Label>
                            <asp:TextBox ID="txtMatricula" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="120" Width="100%" CssClass="configtext" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMatricula" runat="server" ControlToValidate="txtMatricula" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblEmail" runat="server" CssClass="configlabel" Text="* e-mail"></asp:Label>
                            <asp:TextBox ID="txtEMail" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Width="100%" CssClass="configtext" TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEMail" runat="server" ControlToValidate="txtEMail" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblEmailCopia" runat="server" CssClass="configlabel" Text="e-mail Cópia"></asp:Label>
                            <asp:TextBox ID="txtEmailCopia" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Width="100%" CssClass="configtext" TabIndex="4"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblPoliticaUso" runat="server" CssClass="configlabel" Text="Cota de consumo"></asp:Label>
                            <asp:TextBox ID="txtValorPolitica" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="19" Width="100%" CssClass="configtext" TabIndex="5"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="mevValorPolitica" runat="server" ControlExtender="meeValorPolitica" ControlToValidate="txtValorPolitica" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="mevValorPolitica" InvalidValueBlurredMessage="*" Style="left: 211px; top: 199px; z-index: 117; float: left;" ValidationGroup="MKE"> </cc1:MaskedEditValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblMatricula_Chefia" runat="server" CssClass="configlabel" Text="Matrícula chefia"></asp:Label>
                            <asp:TextBox ID="txtMatricula_Chefia" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="120" Width="100%" CssClass="configtext" TabIndex="6"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblConsumidorTipo" runat="server" CssClass="configlabel" Text="* Tipo"></asp:Label>
                            <asp:DropDownList ID="cboConsumidorTipo" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="7" AppendDataBoundItems="True" Width="100%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvConsumidorTipo" runat="server" ControlToValidate="cboConsumidorTipo" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblEmpresaContratada" runat="server" CssClass="configlabel" Text="Empresa terceira"></asp:Label>
                            <asp:DropDownList ID="cboEmpresaContratada" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="8" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblCargo" runat="server" CssClass="configlabel" Text="Cargo"></asp:Label>
                            <asp:DropDownList ID="cboCargo" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="9" Width="100%"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblFilial" runat="server" CssClass="configlabel" Text="* Filial"></asp:Label>
                            <asp:DropDownList ID="cboFilial" AutoPostBack="True" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="10" Width="100%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvFilial" runat="server" ControlToValidate="cboFilial" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblCentroCusto" runat="server" CssClass="configlabel" Text="* Centro de custo"></asp:Label>
                            <asp:DropDownList ID="cboCentroCusto" AutoPostBack="True" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="11" Width="100%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCentroCusto" runat="server" ControlToValidate="cboCentroCusto" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDepartamento" runat="server" CssClass="configlabel" Text="Departamento"></asp:Label>
                            <asp:DropDownList ID="cboDepartamento" AutoPostBack="True" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="12" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblSetor" runat="server" CssClass="configlabel" Text="Setor"></asp:Label>
                            <asp:DropDownList ID="cboSetor" AutoPostBack="True" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="13" Width="100%"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblSecao" runat="server" CssClass="configlabel" Text="Seção"></asp:Label>
                            <asp:DropDownList ID="cboSecao" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="14" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblStatus" runat="server" CssClass="configlabel" Text="Situação cadastral"></asp:Label>
                            <asp:DropDownList ID="cboConsumidorStatus" runat="server" CssClass="configCombo" EnableTheming="True" TabIndex="15" Width="100%"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:CheckBox ID="chkFl_Nao_Enviar_Email" runat="server" Style="float: right" Font-Names="Arial" Font-Size="8pt" Text="Não divulgar telefone" ForeColor="Black" />
                            <div id="DivAtivo" runat="server" style="border: 1px solid #CCCCCC; overflow: auto; width: 100%; height: 81px" title=" ">
                                <asp:DataGrid ID="dtgAtivo" runat="server" AutoGenerateColumns="False" BorderColor="Black" CellPadding="5" CellSpacing="5" Font-Bold="False" HorizontalAlign="Center" Font-Italic="False" BackColor="Transparent" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Width="100%" BorderStyle="Solid" BorderWidth="1px">
                                    <Columns>
                                        <asp:BoundColumn DataField="Id_Ativo" HeaderText="Id_Ativo" Visible="False"></asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nr_Ativo" HeaderText="Linha">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Height="20px" Font-Size="10pt" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nm_Conglomerado" HeaderText="Operadora">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Height="20px" Font-Size="10pt" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Nm_Ativo_Tipo" HeaderText="Tipo">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Height="20px" Font-Size="10pt" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>

                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <div style="display: flex; align-items: center; justify-content: center;">
                                                    <a href="javascript:void(0);" onclick="baixarTodosTermos()" title="Baixar todos os termos">
                                                        <span style="margin-right: 5px;color: #ccc;font-size: 15px;">Termo</span>
                                                        <i class="fas fa-download" style="color: white; font-size: 10px;"></i>
                                                    </a>
                                                </div>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <a href='<%# "/Termo/Termo_Responsabilidade_Fortlev.aspx?Id_Ativo=" & Eval("Id_Ativo") & "&Id_Consumidor=" & Request("ID") & "&Nm_Ativo_Tipo_Grupo=" & Eval("Nm_Ativo_Tipo_Grupo") %>'
                                                   class="termo-download"
                                                   data-idativo='<%# Eval("Id_Ativo") %>'
                                                   data-nmgrupo='<%# Eval("Nm_Ativo_Tipo_Grupo") %>'
                                                   target="_blank"
                                                   title="Abrir Termo">
                                                    <i class="fas fa-download" style="color: #444; font-size: 14px;"></i>
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>




                                    </Columns>

                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Height="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#CCCCCC" BackColor="Black" BorderColor="Black" Font-Names="Calibri Light" Font-Size="12pt" HorizontalAlign="Center" Wrap="true" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <PagerStyle Mode="NumericPages" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />

                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblUsuario" runat="server" CssClass="configlabel" Text="Usuário"></asp:Label>
                            <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="configtext" Width="100%" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtNmUsuario" runat="server" CssClass="configtext" MaxLength="50" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
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

    <asp:Panel ID="pnlValidador" runat="server" Style="z-index: 107; left: 677px; position: absolute; top: 489px; height: 63px; width: 132px;">
        <cc1:ValidatorCalloutExtender ID="vceDescricao" runat="server" TargetControlID="rfvDescricao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceMatricula" runat="server" TargetControlID="rfvMatricula"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceEMail" runat="server" TargetControlID="rfvEMail"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceFilial" runat="server" TargetControlID="rfvFilial"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceConsumidorTipo" runat="server" TargetControlID="rfvConsumidorTipo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceCentroCusto" runat="server" TargetControlID="rfvCentroCusto"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeValorPolitica" runat="server" AcceptNegative="Right" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" ErrorTooltipEnabled="True" Mask="99999999.99" MaskType="Number" TargetControlID="txtValorPolitica"></cc1:MaskedEditExtender>
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
        <%--<asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar">
            <i class="fas fa-folder-open"></i>
            <br />
            <span id="Span1" runat="server">Dados</span>
        </asp:LinkButton>--%>
        <asp:LinkButton ID="btConfiguracao" runat="server" CausesValidation="false" CssClass="btn-menu-toolbar">
            <i class="fas fa-cog"></i>
            <br />
            <span id="Span2" runat="server">Config</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" OnClick="btAbrir_Click">
            <i class="fas fa-folder-open"></i>
            <br />
            <span>Dados</span>
        </asp:LinkButton>
    </div>

    <script>
    function baixarTodosTermos() {
        const termos = document.querySelectorAll(".termo-download");

        let idAtivo = [];
        let idConsumidor = []; // repetiremos o mesmo valor
        let nmGrupo = [];

        const consumidorId = new URLSearchParams(window.location.search).get("ID");

        termos.forEach(link => {
            idAtivo.push(link.getAttribute("data-idativo"));
            nmGrupo.push(link.getAttribute("data-nmgrupo"));
            idConsumidor.push(consumidorId);
        });

        const url = `/Termo/Termo_Responsabilidade_Fortlev.aspx?` +
            `Id_Ativo=${idAtivo.join(",")}` +
            `&Id_Consumidor=${idConsumidor.join(",")}` +
            `&Nm_Ativo_Tipo_Grupo=${nmGrupo.join(",")}`;

        window.open(url, "_blank");
    }
   </script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const links = document.querySelectorAll("a.termo-download");

            links.forEach(link => {
                const href = link.getAttribute("href");
                const checkUrl = href + "&check=1";

                fetch(checkUrl, { method: "GET" })
                    .then(response => {
                        if (response.status === 204) {
                            link.style.display = "none";
                        }
                    })
                    .catch(error => {
                        console.error("Erro ao verificar termo:", error);
                        link.style.display = "none";
                    });
            });
        });
    </script>



</asp:Content>
