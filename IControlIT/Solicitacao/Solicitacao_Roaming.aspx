<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeBehind="Solicitacao_Roaming.aspx.vb" Inherits="IControlIT.Solicitacao_Roaming" EnableEventValidation="true" %>

<%@ MasterType VirtualPath="~/Principal.master" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">

    <style type="text/css">
        .speech-bubble {
            position: relative;
            background: #3168d2;
            border-radius: 0px 15px 15px 15px;
        }

            .speech-bubble:after {
                content: '';
                position: absolute;
                left: 0;
                top: 0;
                width: 0;
                height: 0;
                border: 20px solid transparent;
                border-right-color: #3168d2;
                border-left: 0;
                border-top: 0;
                margin-top: 0px;
                margin-left: -15px;
            }

        .speech-bubble-02 {
            position: relative;
            background: #4a525a;
            border-radius: 15px 0px 15px 15px;
        }

            .speech-bubble-02:after {
                content: '';
                position: absolute;
                right: 0;
                top: 0;
                width: 0;
                height: 0;
                border: 20px solid transparent;
                border-left-color: #4a525a;
                border-right: 0;
                border-top: 0;
                margin-top: 0px;
                margin-right: -15px;
            }

        .textBoxChat {
            color: #FFFFFF;
            font-size: 12pt;
            font-family: 'Segoe UI';
            /*background-color:transparent;
            border: 0px solid transparent;*/
            line-height: 20px;
            white-space: pre-wrap;
            /*height:110px;*/
            width: 350px;
            /*vertical-align: middle;*/
            /*overflow: hidden;*/
            text-align: justify;
        }

        /* Float Shadow */
        .hvr-float-shadow {
            display: inline-block;
            vertical-align: middle;
            -webkit-transform: perspective(1px) translateZ(0);
            transform: perspective(1px) translateZ(0);
            box-shadow: 0 0 1px rgba(0, 0, 0, 0);
            position: relative;
            -webkit-transition-duration: 0.3s;
            transition-duration: 0.3s;
            -webkit-transition-property: transform;
            transition-property: transform;
        }

            .hvr-float-shadow:before {
                pointer-events: none;
                position: absolute;
                z-index: -1;
                content: '';
                /*top: 100%;*/
                bottom: -6px;
                left: 15%;
                height: 7px;
                width: 70%;
                opacity: 1;
                background: -webkit-radial-gradient(center, ellipse, rgba(0, 0, 0, 0.35) 0%, rgba(0, 0, 0, 0) 80%);
                background: radial-gradient(ellipse at center, rgba(0, 0, 0, 0.35) 0%, rgba(0, 0, 0, 0) 80%);
                /* W3C */
                -webkit-transition-duration: 0.3s;
                transition-duration: 0.3s;
                -webkit-transition-property: transform, opacity, height;
                transition-property: transform, opacity, height;
            }

            .hvr-float-shadow:hover, .hvr-float-shadow:focus, .hvr-float-shadow:active {
                -webkit-transform: translateY(-5px);
                transform: translateY(-5px);
                /* move the element up by 5px */
            }

                .hvr-float-shadow:hover:before, .hvr-float-shadow:focus:before, .hvr-float-shadow:active:before {
                    height: 12px;
                    opacity: 1;
                    -webkit-transform: translateY(7px);
                    transform: translateY(7px);
                    /* move the element down by 5px (it will stay in place because it's attached to the element that also moves up 5px) */
                }

                .hvr-float-shadow:hover .speech-bubble-menu {
                    visibility: visible;
                }

        /*tooltip dos botões do menu*/
        .speech-bubble-menu {
            position: absolute;
            top: -35px;
            background: #4ca2cd;
            border-radius: 4px;
            padding: 7px 15px 7px 15px;
            visibility: hidden;
        }

            .speech-bubble-menu:after {
                content: '';
                position: absolute;
                bottom: 0;
                left: 50%;
                width: 0;
                height: 0;
                border: 8px solid transparent;
                border-top-color: #4ca2cd;
                border-bottom: 0;
                margin-left: -8px;
                margin-bottom: -8px;
            }
        /* width */
        #DivAtivo::-webkit-scrollbar {
            width: 10px;
        }

        /* Track */
        #DivAtivo::-webkit-scrollbar-track {
            background: #f1f1f1;
        }

        /* Handle */
        #DivAtivo::-webkit-scrollbar-thumb {
            background: #888;
        }

            /* Handle on hover */
            #DivAtivo::-webkit-scrollbar-thumb:hover {
                background: #555;
            }

        .center {
            display: block;
            margin-left: auto;
            margin-right: auto;
            width: 50%;
        }

        .tblInfo {
            width: 450px;
            height: 550px;
        }

        .tblChat {
            width: 450px;
            height: 550px;
            background-color: #33383d;
            padding: 10px;
            top: 125px;
        }

        .lblTituloChat {
            font-size: 22pt;
        }

        .tdPopup {
            width: 500px;
        }

        .imgAvaliacao {
            width: 200px;
        }

        .bgModal {
            position: fixed;
            z-index: 120;
            left: 0;
            top: 0;
            width: 100%;
            height: 100vh;
            background: rgba(0,0,0,0.8);
        }

        .modalAvaliacao {
            position: absolute;
            z-index: 120;
            background: #FFFFFF;
            float: left;
            left: 50%;
            top: 50%;
            transform: translate(-50%, -50%);
            padding: 20px;
            border-radius: 6px;
            box-shadow: rgba(0,0,0,0.4) 0px 4px 8px 2px;
            width: 500px;
        }

        .modalUnidade {
            position: absolute;
            z-index: 120;
            background: #FFFFFF;
            float: left;
            left: 50%;
            top: 50%;
            transform: translate(-50%, -50%);
            padding: 20px;
            border-radius: 6px;
            box-shadow: rgba(0,0,0,0.4) 0px 4px 8px 2px;
            width: 650px;
        }

        .fade-in {
            animation: fadeIn ease 0.2s;
            -webkit-animation: fadeIn ease 0.2s;
            -moz-animation: fadeIn ease 0.2s;
            -o-animation: fadeIn ease 0.2s;
            -ms-animation: fadeIn ease 0.2s;
        }

        .starEmpty {
            color: #757575;
        }

        .starEmpty {
            color: #757575;
        }

            .starEmpty:hover {
                color: #616161;
            }

        .grow {
            transition: all .2s ease-in-out;
        }

            .grow:hover {
                transform: scale(1.2);
            }

        /*/ Responsivo para tela mobile /*/
        @media (min-width: 320px) and (max-width: 1024px) {

            .tblInfo {
                width: 100%;
                /*height: 100vh;*/
            }

            .tblChat {
                width: 100%;
                background-color: #33383d;
                padding: 10px;
                top: 125px;
            }

            .lblTituloChat {
                font-size: 12pt;
            }

            .tdPopup {
                width: 100%;
            }

            .bgModal {
                position: fixed;
                z-index: 120;
                left: 0;
                top: 0;
                width: 100%;
                height: 100vh;
                background: rgba(0,0,0,0.8);
            }

            .modalAvaliacao {
                position: absolute;
                z-index: 120;
                background: #FFFFFF;
                float: left;
                left: 50%;
                top: 50%;
                transform: translate(-50%, -50%);
                padding: 35px;
                border-radius: 0px;
                box-shadow: rgba(0,0,0,0.4) 0px 4px 8px 2px;
                width: 100%;
            }

            .modalUnidade {
                position: absolute;
                z-index: 120;
                background: #FFFFFF;
                float: left;
                left: 50%;
                top: 50%;
                transform: translate(-50%, -50%);
                padding: 35px;
                border-radius: 0px;
                box-shadow: rgba(0,0,0,0.4) 0px 4px 8px 2px;
                width: 100%;
                height: 100vh;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--registro Salvo-->
    <div id="pnlRegistroSalvo" runat="server" class="bgModal" visible="false">
        <div class="modalAvaliacao text-center">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblMsg" runat="server" Style="float: none" CssClass="configlabel" Text=""></asp:Label>
                    <asp:Label ID="lblRegistroSalvo" runat="server" Style="float: none" CssClass="configlabel" Text="* Registro salvo com sucesso !"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="btFechar_Registro" class="btn btn-success" Width="80px" runat="server" Text="OK" CausesValidation="False" />
                    <asp:HiddenField ID="hfdId_Solicitacao_Avaliacao" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <!--Infomacoes-->
    <div id="pnlInformacao" runat="server" class="bgModal" visible="false">
        <div class="modalAvaliacao">
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblDescricao_Informacao" runat="server" CssClass="configlabel" Text="Informações Adicionais" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Label ID="lblSolucao" runat="server" CssClass="configlabel" Text="Solução" Style="float: none"></asp:Label>
                        <asp:DropDownList ID="cboSolucao" runat="server" Width="100%" CssClass="configCombo" EnableTheming="True" TabIndex="4" Enabled="False"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Label ID="lblUsuario" runat="server" CssClass="configlabel" Text="Usuário" Style="float: none"></asp:Label>
                        <asp:TextBox ID="txtUsuario" runat="server" MaxLength="50" Width="100%" CssClass="configtext" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblData" runat="server" CssClass="configlabel" Text="Data/Hora" Style="float: none"></asp:Label>
                        <asp:TextBox ID="txtDataHora" runat="server" MaxLength="50" Width="100%" CssClass="configtext" ReadOnly="True" ForeColor="#FF9900"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblDataVencimento" runat="server" CssClass="configlabel" Text="Vencimento" Style="float: none"></asp:Label>
                        <asp:TextBox ID="txtDataVencimento" runat="server" MaxLength="50" Width="100%" CssClass="configtext" ReadOnly="True" ForeColor="#FF9900"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblEncerramento" runat="server" CssClass="configlabel" Text="Encerramento" Style="float: none"></asp:Label>
                        <asp:TextBox ID="txtEnecerramento" runat="server" MaxLength="50" Width="100%" CssClass="configtext" ReadOnly="True" ForeColor="#FF9900"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblSinal" runat="server" CssClass="configlabel" Text="SLA" Style="float: none"></asp:Label>
                        <div style="width: 100%">
                            <asp:Image ID="imgVD" runat="server" ImageUrl="~/Img_Sistema/Menu/vd.png" Height="16px" Visible="False" />
                            <asp:Image ID="imgAM" runat="server" ImageUrl="~/Img_Sistema/Menu/am.png" Height="16px" Visible="False" />
                            <asp:Image ID="imgVM" runat="server" ImageUrl="~/Img_Sistema/Menu/vm.png" Height="16px" Visible="False" />
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblStatus" runat="server" CssClass="configlabel" Text="Status" Style="float: none"></asp:Label>
                        <asp:TextBox ID="txtStatus" runat="server" MaxLength="300" Width="100%" CssClass="configtext" ReadOnly="True" ForeColor="#FF9900"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblIncidente" runat="server" CssClass="configlabel" Text="Incidente" Style="float: none"></asp:Label>
                        <asp:TextBox ID="txtIncidente" runat="server" CssClass="configtext" ReadOnly="True" Width="100%" ForeColor="#FF9900"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div>&nbsp;</div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btFechar_Informacoes" class="btn btn-success" runat="server" Text="Fechar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--avaliacao-->
    <div id="pnlAvaliacao" runat="server" class="bgModal" visible="false">
        <div class="modalAvaliacao">
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblDescricaoAvaliacao" runat="server" CssClass="lblTituloChat" Text="Avaliação do Chamado" Style="float: none" Font-Names="Segoe UI Light" Font-Size="22pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div style="position: relative; text-align: center; border-radius: 30px; background-color: #BDBDBD; border: 2px solid #9E9E9E; padding: 5px">
                        <ul style="list-style-type: none; margin: 0; padding: 0">
                            <li class="grow" style="display: inline-block">
                                <asp:LinkButton ID="btStar01" runat="server" OnClick="btStar01_Click">
                                    <i id="star01" runat="server" class="fas fa-star starEmpty" style="font-size: 25pt"></i>
                                </asp:LinkButton>
                            </li>
                            <li class="grow" style="display: inline-block">
                                <asp:LinkButton ID="btStar02" runat="server" OnClick="btStar02_Click">
                                    <i id="star02" runat="server" class="fas fa-star starEmpty" style="font-size: 25pt"></i>
                                </asp:LinkButton>
                            </li>
                            <li class="grow" style="display: inline-block">
                                <asp:LinkButton ID="btStar03" runat="server" OnClick="btStar03_Click">
                                    <i id="star03" runat="server" class="fas fa-star starEmpty" style="font-size: 25pt"></i>
                                </asp:LinkButton>
                            </li>
                            <li class="grow" style="display: inline-block">
                                <asp:LinkButton ID="btStar04" runat="server" OnClick="btStar04_Click">
                                    <i id="star04" runat="server" class="fas fa-star starEmpty" style="font-size: 25pt"></i>
                                </asp:LinkButton>
                            </li>
                            <li class="grow" style="display: inline-block">
                                <asp:LinkButton ID="btStar05" runat="server" OnClick="btStar05_Click">
                                    <i id="star05" runat="server" class="fas fa-star starEmpty" style="font-size: 25pt"></i>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                    <asp:DropDownList ID="cboAvaliacao" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" Visible="false">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="1">Ruim</asp:ListItem>
                        <asp:ListItem Value="2">Regular</asp:ListItem>
                        <asp:ListItem Value="3">Bom</asp:ListItem>
                        <asp:ListItem Value="4">Ótimo</asp:ListItem>
                        <asp:ListItem Value="5">Excelente</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Label ID="lblComentario" runat="server" CssClass="configlabel" Text="Comentário" Style="float: none"></asp:Label>
                        <%--<asp:RequiredFieldValidator ID="rfvComentario" runat="server" ControlToValidate="txtComentario" Font-Names="Arial" Font-Size="10pt" Style="left: 445px; top: 38px; float: left;" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        <asp:TextBox ID="txtComentario" runat="server" AutoPostBack="True" CssClass="configtext" MaxLength="50" Width="100%" Height="63px" TextMode="MultiLine"></asp:TextBox>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12 text-center">
                    <asp:Button ID="btSalvarAvaliacao" class="btn btn-primary float-right" runat="server" Text="Salvar" CausesValidation="False" />
                    <asp:Button ID="btFecharAvaliacao" class="btn btn-default float-right" runat="server" Text="Fechar" CausesValidation="False" />
                </div>
            </div>
        </div>
    </div>

    <!--excedente de sla-->
    <div id="pnlDetalhe" runat="server" class="bgModal" visible="false">
        <div class="modalAvaliacao">
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblDesativacaoAparelho" runat="server" CssClass="configlabel" Text="Excedente de S.L.A." Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Label ID="lblObservacao" runat="server" CssClass="configlabel" Text="Observação" Style="float: none"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvAte" runat="server" ControlToValidate="txtObservacao" Font-Names="Arial" Font-Size="10pt" Style="left: 445px; top: 38px; float: left;" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtObservacao" runat="server" AutoPostBack="True" CssClass="configtext" MaxLength="50" Width="100%" Height="63px" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <ul style="list-style-type: none">
                        <li style="display: inline-block">
                            <asp:Button ID="btCancela" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                        </li>
                        <li style="display: inline-block">
                            <asp:Button ID="btOk" class="btn btn-success" runat="server" Text="Salvar" CausesValidation="False" />
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <!--solucao-->
    <div id="pnlSolucao" runat="server" class="bgModal" visible="false">
        <div class="modalAvaliacao">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDescricaoSolucao" runat="server" CssClass="configlabel" Text="Solução do Chamado" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Label ID="lblSubSolucao" runat="server" CssClass="configlabel" Text="Solucão" Style="float: none"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvSolucao" runat="server" ControlToValidate="cboSubSolucao" Font-Names="Arial" Font-Size="10pt" Style="left: 445px; top: 38px; float: left;" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:DropDownList ID="cboSubSolucao" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%" TabIndex="2"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <ul style="list-style-type: none; margin: 0; padding: 0">
                        <li style="display: inline-block">
                            <asp:Button ID="btFecharSolucao" class="btn btn-default" runat="server" Text="Fechar" CausesValidation="False" />
                        </li>
                        <li style="display: inline-block">
                            <asp:Button ID="btEnceraSolucao" class="btn btn-success" runat="server" Text="Salvar" CausesValidation="False" />
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <!--publica chamado para usuario-->
    <div id="pnlPublica" runat="server" class="bgModal" visible="false">
        <div class="modalAvaliacao">
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblDescPublica" runat="server" CssClass="configlabel" Style="float: none" Text="Publicar tratativa para usuário?" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
                </div>
            </div>
            <div>&nbsp;</div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <ul style="list-style-type: none; margin: 0; padding: 0">
                        <li style="display: inline-block">
                            <asp:Button ID="BtPublicaSim" class="btn btn-success" runat="server" Text="Sim" CausesValidation="False" />
                        </li>
                        <li style="display: inline-block">
                            <asp:Button ID="BtPublicaNao" class="btn btn-default" runat="server" Text="Não" CausesValidation="False" />
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <!--Tela *************************************************************************************** -->

    <!--Corpo-->
    <div id="divTela" runat="server" class="row">
        <div class="col-md-12">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <asp:Button ID="btnInformacao" runat="server" class="btn-tab pull-left" Text="Informações" CausesValidation="False" OnClick="btnInformacao_Click" />
                    <asp:Button ID="btnChat" runat="server" class="btn-tab-disable pull-left" Text="Chat" CausesValidation="False" OnClick="btnChat_Click" />
                </div>
            </nav>
            <div class="card-body" style="background-color: #FFFFFF; border-radius: 6px; box-shadow: rgba(0,0,0,0.2) 0px 2px 4px 2px">

                <!--Informações-->
                <div id="divInformacao" runat="server" visible="true" class="row">
                    <div class="col-md-12">
                        <div class="card-body" style="box-shadow: none;">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblTitulo" runat="server" ForeColor="#818181" CssClass="lblTituloChat" Font-Names="Segoe UI Light" Font-Size="22pt" Text="Solicitação"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblTipo_Ativo" runat="server" Style="float: left" CssClass="configlabel" Text="* Dispositivo"></asp:Label>
                                        <asp:DropDownList ID="cboAtivoTipo" runat="server" AutoPostBack="True" CssClass="configCombo" EnableTheming="True" Width="100%"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTipoAtivo" runat="server" ControlToValidate="cboAtivoTipo" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblSolicitacao" runat="server" Style="float: left" CssClass="configlabel" Text="* País de Destino"></asp:Label>
                                        <asp:DropDownList ID="cboSolicitacao" runat="server" CssClass="configCombo" EnableTheming="True" Width="100%"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSolicitacao" runat="server" ControlToValidate="cboSolicitacao" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblDetalhamento" runat="server" Style="float: left" CssClass="configlabel" Text="* Data da Viagem"></asp:Label>
                                        <asp:TextBox ID="txtDetalhamento" runat="server" CssClass="configtext" MaxLength="10" Width="100%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDetalhamento" runat="server" ControlToValidate="txtDetalhamento" Display="None" SetFocusOnError="True" Style="left: 534px; top: 37px; float: left;"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--Chat-->
                <div id="divChat" runat="server" visible="false" class="row">
                    <div class="col-md-12">
                        <div class="card-body" style="box-shadow: none">
                            <div class="row">
                                <div class="col-md-12">
                                    <div style="width: 100%; border: 1px solid #E0E0E0; background-color: #818181; padding: 15px">
                                        <asp:Label CssClass="lblTituloChat" ID="lblTituloChat" runat="server" ForeColor="#FFFFFF" Font-Names="Segoe UI Light" Text="Converse com o atendimento"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div id="tdChat" runat="server" visible="false" class="row">
                                <div class="col-md-12">
                                    <div id="DivAtivo" runat="server" class="tblChat">
                                        <asp:DataGrid ID="dtgSolicitacaoItem" runat="server" AutoGenerateColumns="False" BorderColor="Transparent" BorderWidth="1px" CellPadding="5" CellSpacing="5" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" ForeColor="Black" GridLines="Horizontal" HorizontalAlign="Center" Style="font-size: 8pt; font-family: 'Segoe UI'" BackColor="Transparent">

                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <PagerStyle Mode="NumericPages" />
                                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <Columns>
                                                <asp:BoundColumn DataField="Id_Solicitacao" HeaderText="Id_Solicitacao" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Id_Solicitacao_Item" HeaderText="Id_Solicitacao_Item" Visible="False"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="" HeaderStyle-Width="100%">
                                                    <ItemTemplate>
                                                        <table style="width: 100%;">
                                                            <tr id="trAdm" runat="server" visible="<%# Bind('Adm') %>">
                                                                <td id="td1" runat="server" align="right">
                                                                    <table>
                                                                        <tr>
                                                                            <td>&nbsp;</td>
                                                                            <td>&nbsp;</td>
                                                                            <td>
                                                                                <div class="speech-bubble-02" style="padding: 15px">
                                                                                    <asp:Label ID="Label2" runat="server" CssClass="textBoxChat" Text="<%# Bind('Nm_Solicitacao_Item') %>"></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td></td>
                                                                            <td>&nbsp;</td>
                                                                            <td style="text-align: left; padding-left: 20px">
                                                                                <asp:Image ID="imgPublicacaoAdm" runat="server" Width="16px" Height="16px" Style="filter: invert(70%)" />
                                                                                &nbsp;
                                                                                <asp:Label ID="Label4" runat="server" ForeColor="#818181" Text="<%# Bind('Dt_Hr_Solicitacao_Item') %>"></asp:Label>
                                                                                &nbsp;
                                                                                <asp:Label ID="Label3" runat="server" ForeColor="#818181" Text="<%# Bind('Nm_Usuario') %>"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr id="trUs" runat="server" visible="<%# Bind('Us') %>">
                                                                <td id="tdChat" runat="server" align="left">
                                                                    <table>
                                                                        <tr>
                                                                            <td>&nbsp;</td>
                                                                            <td>&nbsp;</td>
                                                                            <td>
                                                                                <div class="speech-bubble" style="padding: 15px">
                                                                                    <asp:Label ID="lblDescricao" runat="server" CssClass="textBoxChat" Style="color: #818181" Text="<%# Bind('Nm_Solicitacao_Item') %>"></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td></td>
                                                                            <td>&nbsp;</td>
                                                                            <td style="text-align: right; padding-right: 20px">
                                                                                <asp:Label ID="lblNome" runat="server" ForeColor="#818181" Text="<%# Bind('Nm_Usuario') %>"></asp:Label>
                                                                                &nbsp;
                                                                                <asp:Label ID="lblDataHora" runat="server" ForeColor="#818181" Text="<%# Bind('Dt_Hr_Solicitacao_Item') %>"></asp:Label>
                                                                                &nbsp;
                                                                                <asp:Image ID="imgPublicacaoUs" runat="server" Width="16px" Height="16px" Style="filter: invert(70%)" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="Publicacao" DataFormatString="&lt;img height=&quot;24px&quot;&gt;" Visible="true">
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle BackColor="Transparent" Font-Bold="False" Font-Italic="False" Font-Names="Calibri Light" Font-Overline="False" Font-Size="1pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#FFFFFF" Height="1px" HorizontalAlign="Center" Wrap="False" />
                                        </asp:DataGrid>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div style="width: 100%; border: 1px solid #E0E0E0; padding-top: 15px">
                                        <asp:TextBox ID="txtChat" runat="server" Style="width: calc(100% - 50px); height: 40px; padding: 12px 10px 10px 10px; background-color: transparent; vertical-align: middle; border: 0px solid #FFFFFF" ForeColor="#818181" placeholder="Escreva sua mensagem aqui..." TextMode="MultiLine"></asp:TextBox>
                                        <asp:LinkButton ID="btInsere" runat="server" Visible="true" Style="opacity: 1;" Enabled="true" OnClick="btInsere_Click">
                                            <i class="fas fa-paper-plane" style="font-size: 20pt"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <asp:Panel ID="pnlValidador" runat="server">
        <cc1:ValidatorCalloutExtender ID="vceTipoAtivo" runat="server" TargetControlID="rfvTipoAtivo"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceSolicitacao" runat="server" TargetControlID="rfvSolicitacao"></cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="vceDetalhamento" runat="server" TargetControlID="rfvDetalhamento"></cc1:ValidatorCalloutExtender>
        <cc1:MaskedEditExtender ID="meeDetalhamento" runat="server" AcceptNegative="Left" DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDetalhamento"></cc1:MaskedEditExtender>
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
        <asp:LinkButton ID="btSalvar" runat="server" CssClass="btn-menu-toolbar" Visible="true" Style="opacity: 1;" Enabled="true" OnClick="btSalvar_Click">
            <i class="fas fa-save"></i>
            <br />
            <span>Salvar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btEncerrar" runat="server" CssClass="btn-menu-toolbar" Visible="true" Style="opacity: 0.3;" Enabled="true" OnClick="btEncerrar_Click">
            <i class="fas fa-recycle"></i>
            <br />
            <span id="lblEncerrar" runat="server">Encerrar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btInformacoes" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="true" Style="opacity: 0.3;" Enabled="true" OnClick="btInformacoes_Click">
            <i class="fas fa-cog"></i>
            <br />
            <span id="lblInformacoes" runat="server">Unidade</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAbrir" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="false" Style="opacity: 1" Enabled="false">
            <i class="fas fa-folder-open"></i>
            <br />
            <span>Abrir</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btFornecedor" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-file-alt"></i>
            <br />
            <span id="lblFornecedor" runat="server">Fornecedor</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btAvaliacao" runat="server" CssClass="btn-menu-toolbar" CausesValidation="false" Visible="true" Style="opacity: 0.3;" Enabled="true" OnClick="btAvaliacao_Click">
            <i class="fas fa-star"></i>
            <br />
            <span>Avaliação</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btExportar" runat="server" CssClass="btn-menu-toolbar" Visible="false" Style="opacity: 1;" Enabled="true">
            <i class="fas fa-file-upload"></i>
            <br />
            <span>Exportar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btRelatorio" runat="server" CssClass="btn-menu-toolbar" Visible="false" Style="opacity: 0.1;" Enabled="false">
            <i class="fas fa-chart-bar"></i>
            <br />
            <span>Relatório</span>
        </asp:LinkButton>
    </div>

</asp:Content>
