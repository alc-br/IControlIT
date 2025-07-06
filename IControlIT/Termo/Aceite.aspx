<%-- 
* HISTÓRICO DE MODIFICAÇÕES
* [ICTRL-NF-202506-003 | 2025-06-27 | Parceiro IControlIT]
--%>
<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Aceite.aspx.vb" Inherits="IControlIT.Termo_Aceite" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Confirmação de Termo de Responsabilidade</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 0; padding: 20px; background-color: #f4f4f4; }
        .container { max-width: 800px; margin: auto; background-color: #fff; padding: 30px; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
        .termo-content { border: 1px solid #ddd; padding: 20px; margin-top: 20px; max-height: 500px; overflow-y: auto; }
        .btn { padding: 12px 25px; font-size: 16px; cursor: pointer; border-radius: 5px; border: none; }
        .btn-success { background-color: #28a745; color: white; }
        .btn-disabled { background-color: #ccc; color: #666; cursor: not-allowed; }
        .message { padding: 15px; border-radius: 5px; margin-top: 20px; }
        .message-success { background-color: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .message-error { background-color: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Termo de Responsabilidade</h2>
            <asp:Panel ID="pnlPrincipal" runat="server">
                <p>Por favor, leia o termo de responsabilidade abaixo e clique no botão para confirmar o seu aceite.</p>
                <div class="termo-content">
                    <asp:Literal ID="ltTermoHtml" runat="server"></asp:Literal>
                </div>
                <br />
                <asp:Button ID="btnAceitar" runat="server" Text="" OnClick="btnAceitar_Click" CssClass="btn btn-success" />
            </asp:Panel>
            
            <asp:Panel ID="pnlMensagem" runat="server" Visible="false">
                 <asp:Literal ID="ltMensagem" runat="server"></asp:Literal>
            </asp:Panel>
        </div>
    </form>
</body>
</html>