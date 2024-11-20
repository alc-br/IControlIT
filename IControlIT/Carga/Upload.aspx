<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Upload.aspx.vb" Inherits="IControlIT.Upload" %>

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
</head>

<script type="text/javascript">
    var t = 1000;
    function timedCount() {
        if (t > 0) {
            document.getElementById("txt").innerText = "Faltam: " + t + " segundos, Aguarde.";
            setTimeout("timedCount()", 1000);
            t = t - 1;
        }
    }
</script>

<body bgcolor="#FFFFFF">
    <form id="form1" runat="server" defaultbutton="btCopiar">
        <table style="width: 100%;">
            <tr>
                <td>
                    <div style="width: 425px; margin: 1% auto; padding: 0px;">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="height: 30px; text-align: center">
                                                <asp:Label ID="lblDescricao" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="X-Large" ForeColor="#333333" Text="Upload de Arquivo" Style="float: none"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="height: 26px;">
                                                            <asp:FileUpload ID="File1" runat="server" TabIndex="1" Width="400px" dir="ltr" Height="35px" BorderColor="White" BorderWidth="1px" AllowMultiple="true" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="File1" Font-Names="Arial" Font-Size="10pt" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblExport" runat="server" Font-Names="Arial" Style="float: left" ForeColor="#333333" Font-Size="9pt">Arquivos carregados, o arquivo de possuir no máximo 7mb.</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ListBox ID="lstArquivo" runat="server" Width="408px" ForeColor="Black" CssClass="configlistboxAbrir"></asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 23px;">
                                                <asp:Label ID="lblMessage" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#FF3300"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 23px;">
                                                <input id="txt" type="text" style="color: #CCCCCC; width: 408px; background-color: transparent; height: 21px;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td></td>
                                                        <td style="width: 85px">
                                                            <asp:Button ID="btCopiar" class="btn btn-success" Width="80px" runat="server" Text="Cópiar" CausesValidation="False" />
                                                        </td>
                                                        <td style="width: 85px">
                                                            <asp:Button ID="btLimpar" class="btn btn-default" Width="80px" runat="server" Text="Limpar" CausesValidation="False" OnClientClick="return confirm('Você deseja limpar os registros da pasta?');" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
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
