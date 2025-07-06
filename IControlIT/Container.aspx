<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Container.aspx.vb" Inherits="IControlIT.Container" EnableEventValidation="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>K2A - IControlIT</title>

    <%-- trava o zoom quando visualizado no mobile e almenta a scala dos objetos na tela deixando responsivo --%>
    <meta name="viewport" content="width=device-width, initial-scale=0.8, maximum-scale=0.8, user-scalable=no" />

    <link rel="Shortcut Icon" href="Img_Sistema/logo.ico" />

    <%--<style type="text/css">
        
        .btn-solicitacao{
            border: 1px solid #d6d6d6;
            border-radius: 4px;
            text-align: center;
        }
        .btn-solicitacao:hover{
            border: 1px solid #158cfb;
        }

        .divSolicitacao{
            width: 550px;
        }

        /*/ Responsivo para tela mobile /*/
        @media (min-width: 320px) and (max-width: 1024px) {
            
            .divSolicitacao{
                width: 100%;
                padding-right: 15px;
                padding-left: 15px;
            }
        }

    </style>--%>
</head>

<frameset border="0" framespacing="0" frameborder="0">
    <frame src="Home.aspx">
</frameset>

</html>
