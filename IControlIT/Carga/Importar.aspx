<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Importar.aspx.vb" Inherits="IControlIT.Importar" %>

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
    <form id="form1" runat="server" defaultbutton="btEnviar">
        <h1>Importar aquivo</h1>
        <div>
            <a class="btn btn-primary" href ="../Arquivos_Templates/InsertTemplate1.xlsx">Arquivo Template</a>
            <asp:FileUpload ID="File1" runat="server" TabIndex="1" Width="400px" dir="ltr" Height="35px" BorderColor="White" BorderWidth="1px" />
        </div>
        <div>
            <asp:Button ID="btLimpar" class="btn btn-default" Width="80px" runat="server" Text="Limpar" />
            <asp:Button ID="btEnviar" class="btn btn-success" Width="80px" runat="server" Text="Enviar" OnClick="btEnviar_Click" />
        </div>
        <div>
            <asp:Label ID="lblNotImportedLinesInfo" class="text-bold" runat="server" ></asp:Label>
            <br />
            <asp:Label ID="lblNotImportedLines" class="text-bold" runat="server" ></asp:Label>
        </div>
    </form>
</body>
</html>
