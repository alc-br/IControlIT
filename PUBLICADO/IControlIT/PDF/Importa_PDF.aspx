<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Importa_PDF.aspx.vb" Inherits="IControlIT.Importa_PDF" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

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

<body>
    <form id="form1" runat="server" defaultbutton="btIncluir">
        <table style="width: 100%;">
            <tr>
                <td>
                    <div style="width: 560px; margin: 1% auto; padding: 0px;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Label ID="lblImportaPDF" runat="server" Font-Bold="False" Font-Names="Microsoft JhengHei Light" Font-Size="X-Large" ForeColor="#333333" Text="Importar Arquivo" Style="float: none"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 82px;">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 82px;">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 82px;">
                                                <asp:Label ID="lblArquivo" runat="server" Font-Size="9pt" Style="float: right" Font-Bold="False" ForeColor="#333333">Arquivo</asp:Label>
                                            </td>
                                            <td>
                                                <input id="inputPDF" onchange="changeFile(this)" style="width: 460px; height: 35px" type="file" size="250" runat="server" dir="ltr">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 82px;">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 82px;">&nbsp;</td>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td></td>

                                                        <td style="width: 84px">
                                                            <asp:Button ID="btIncluir" class="btn btn-success" Width="80px" runat="server" Text="Salvar" CausesValidation="False" disabled ="true" />
                                                        </td>

                                                        <td style="width: 84px">
                                                            <asp:Button ID="btVoltar" class="btn btn-default" Width="80px" runat="server" Text="Voltar" CausesValidation="False" />
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
    <div class="col-12" style="display:flex;justify-content:center">
        <asp:Label ID="lblInfo" class="text-bold" runat="server" ></asp:Label>
    </div>
</body>
</html>

<script type="text/javascript">
    var infoLabel = document.getElementById("lblInfo");
    var btEnviar = document.getElementById("btIncluir");
    var uploadFile = document.getElementById("inputPDF");

    uploadFile.addEventListener("change", function (e) {
        var size = uploadFile.files[0].size;
        if (size > 36700160) {
            infoLabel.innerHTML = "Tamanho do arquivo excede o valor máximo de 35MB.";
            uploadFile.value = "";
            btEnviar.disabled = true;
        } else {
            infoLabel.innerHTML = "";
            btEnviar.disabled = false;
            document.getElementById('txtdescricao').value = input.files[0].name
        }
        e.preventDefault();
    });
</script>