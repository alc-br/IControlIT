<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Atualizar.aspx.vb" Inherits="IControlIT.Atualizar" %>

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
    <form id="form2" runat="server" defaultbutton="btEnviar">
        <h1>Atualizar usuarios em lote</h1>
        <div>
            <asp:Button ID="btDownload" class="btn btn-success" Width="80px" runat="server" Text="Download template" OnClick="btDownload_Click" />
            <asp:FileUpload ID="File2" runat="server" TabIndex="1" Width="400px" dir="ltr" Height="35px" BorderColor="White" BorderWidth="1px" />
        </div>
        <div>
            <asp:Button ID="btLimpar" class="btn btn-default" Width="80px" runat="server" Text="Limpar" />
            <asp:Button ID="btEnviar" class="btn btn-success" Width="80px" runat="server" Text="Enviar" OnClick="btEnviar_Click" disabled="true" />
        </div>
        <div>
            <asp:Label ID="lblNotImportedLinesInfo" class="text-bold" runat="server" ></asp:Label>
            <br />
            <asp:Label ID="lblNotImportedLines" class="text-bold" runat="server" ></asp:Label>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">
    var uploadFile = document.getElementById("File2");
    var infoLabel = document.getElementById("lblNotImportedLinesInfo");
    var btEnviar = document.getElementById("btEnviar");
    var btLimpar = document.getElementById("btLimpar");

    uploadFile.addEventListener("change", function (e) {
        var size = uploadFile.files[0].size;
        if (size > 36700160) {      
            infoLabel.innerHTML = "Tamanho do arquivo excede o valor máximo de 35MB.";
            uploadFile.value = "";
            btEnviar.disabled = true;
        } else {
            infoLabel.innerHTML = "";
            btEnviar.disabled = false;
        }
        e.preventDefault();
    });

    btLimpar.addEventListener("click", function (e) {
        infoLabel.innerHTML = "";
        e.preventDefault();
    });
</script>
