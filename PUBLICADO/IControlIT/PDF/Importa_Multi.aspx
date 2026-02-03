<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Importa_Multi.aspx.vb" Inherits="IControlIT.Importa_Multi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>K2A - IControlIT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <script src='../JScript.js' type="text/javascript"></script>
    <link href="../CSSConfigObj.css" rel="Stylesheet" />
    <link href="../CSSEstruturalMaster.css" rel="Stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="Stylesheet" />
    <link rel="stylesheet" href="../Content/fontawesome.min.css" type="text/css">
    <link rel="stylesheet" href="../Content/fontawesome-all.min.css">
    <link rel="Shortcut Icon" href="Img_Sistema/logo.ico" />
</head>
<body>
  <div class="container text-center">
    <div class="row">
        <div class="col-md-12" >
            <asp:Label ID="lblUpFatLoteTitulo" runat="server" CssClass="configlabel" Text="Upload em lote de arquivos" Style="margin-left: 30%;" Font-Names="Segoe UI Semibold" Font-Size="20pt" ForeColor="Blue"></asp:Label>
        </div>
         <div class="col-md-12">
            <asp:Label ID="lblUpFatLoteSubTitulo" runat="server" CssClass="configlabel" Text="Selecionar arquivos" Style="float: left" Font-Names="Segoe UI Semibold" Font-Size="18pt"></asp:Label>
         </div>  
        
    </div><br /><br /><br />
    <div class="row">
        <div class="col-md-12">
            <form id="form1" runat="server">
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btnUpload" Text="Importar" class="btn btn-success" runat="server" OnClick="Upload" />
                <br />
                <br />
                <asp:Label ID="lblMessage" runat="server" ForeColor="DarkGreen" />
            </form>
        </div>
    </div>
    <div class="row">
          <div class="col-md-12">
                <div id="divTabela" style="overflow-y: auto; max-height: 300px;">
                    <!-- Tabela -->
                    <table ID="tblLoteFaturas" class="table" runat="server" style="max-height: 600px; overflow-y: auto;">
                    <thead>
                        <tr>
                            <th><b>Arquivo</b></th>
                            <th><b>Status</b></th>
                        </tr>
                    </thead>
                    <tbody>
                        <%--<tr>
                            <td>Item 1</td>
                            <td>
                                <i class="fas fa-check text-success"></i>
                            </td>
                        </tr>
                        <tr>
                            <td>Item 2</td>
                            <td><i class="fas fa-times text-danger"></i></td>
                        </tr>--%>
                        <!-- Adicione mais linhas conforme necessário -->
                    </tbody>
                </table>
                </div>
          </div>  
    </div>
  </div>
</body>
</html>
