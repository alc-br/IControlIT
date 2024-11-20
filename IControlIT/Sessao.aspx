<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Sessao.aspx.vb" Inherits="IControlIT.Sessao" EnableEventValidation="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>K2A - IControlIT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
</head>
<body>
    <form id="form1" runat="server">

        <div style="width: 100%; height: 100vh; text-align: center; position: relative">
            <div style="position: absolute; left: 50%; top: 50%; transform: translate(-50%, -50%)">
                <asp:Label ID="Label1" runat="server" EnableTheming="True"
                    Font-Names="Arial" Font-Overline="False" Font-Size="14pt"
                    ForeColor="#333333" Width="528px">
				    <h3>Sessão Encerrada.</h3> Por gentileza, tente o acesso novamente. Caso o problema persista, contate a área responsável.
				    <br /> <p>Obrigado.</p>
                </asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
