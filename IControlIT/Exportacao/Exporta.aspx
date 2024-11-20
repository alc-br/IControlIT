<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Exporta.aspx.vb" Inherits="IControlIT.Exporta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>K2A - IControlIT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <script src='../JScript.js' type="text/javascript"></script>
    <link href="../CSSConfigObj.css" rel="Stylesheet" />
    <link href="../CSSEstruturalMaster.css" rel="Stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="Stylesheet" />
    <link rel="Shortcut Icon" href="Img_Sistema/logo.ico" />
    <style type="text/css">
        input[type="checkbox"] {
            margin-right: 5px;
        }
    </style>
</head>

<body class="fbody">
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
                    <div style="width: 600px; margin: 1% auto; padding: 0px;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="height: 40px; text-align: center">
                                    <asp:Label ID="lblDescricao" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="X-Large" ForeColor="#333333" Text="Exportação de Dados" Style="float: none"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="QuantidadeRegistrosLabel" /><br />
                                    <asp:Label runat="server" ID="QuantidadeArquivosLabel" /><br />
                                    <asp:Label runat="server" ID="TextoTempoProcessamentoLabel" /><br />
                                    <asp:Label runat="server" ID="PrevisaoTerminoLabel" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <asp:CheckBox runat="server" ID="pdfPaisagemCheckbox" Text="PDF em modo paisagem" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tamanho da fonte no PDF/HTML: <asp:TextBox runat="server" ID="TamanhoFonteTextBox" Columns="2" MaxLength="2" />px
                                    <asp:RangeValidator runat="server" ID="TamanhoFonteRangeValidator" ControlToValidate="TamanhoFonteTextBox" Type="Integer" EnableClientScript="true" ErrorMessage="O tamanho da fonte deve estar entre 8 e 16." MinimumValue="8" MaximumValue="16" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Quantidade de registros por arquivo: <asp:TextBox runat="server" ID="QuantidadeRegistrosTextBox" Columns="6" MaxLength="6" /><br /><br />
                                    <asp:RangeValidator runat="server" ID="QuantidadeRegistrosRangeValidator" ControlToValidate="QuantidadeRegistrosTextBox" Type="Integer" EnableClientScript="true" ErrorMessage="A quantidade de registros deve estar entre 50000 e 500000." MinimumValue="50000" MaximumValue="500000" />
                                </td>
                            </tr>
                            <tr>
                                <td>Campos:<br />
                                    <asp:CheckBoxList runat="server" ID="CamposCheckBoxList" CellSpacing="10" CellPadding="10" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 170px;">
                                                <asp:Label ID="lblExport" runat="server" Font-Size="9pt" Style="float: left;" Font-Bold="False" ForeColor="#333333" Width="165px">Exportar arquivo para formato</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cboExport" runat="server" EnableTheming="True" Style="width: 320px; float: Left;" AutoPostBack="True" CssClass="configCombo" Enabled="False" ForeColor="#333333">
                                                    <asp:ListItem Value="1">Excel</asp:ListItem>
                                                    <asp:ListItem Value="2">PDF</asp:ListItem>
                                                    <asp:ListItem Value="3">HTML</asp:ListItem>
                                                    <asp:ListItem Value="4">CSV</asp:ListItem>
                                                    <%--<asp:ListItem Value="2">Word</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--<asp:HiddenField ID="hdfCampo" runat="server" />--%>
                                    <%--<asp:DataGrid ID="dtg" runat="server" Visible="false"></asp:DataGrid>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btExecutar" class="btn btn-success" Style="float: right" Text="Download" CausesValidation="True" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
