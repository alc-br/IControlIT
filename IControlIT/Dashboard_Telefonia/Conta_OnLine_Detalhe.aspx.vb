
Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.IO

Public Class Conta_OnLine_Detalhe
    Inherits System.Web.UI.Page
    Dim WS_Consulta As New WS_GUA_Consulta.WSConsulta

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            WS_Consulta.Credentials = System.Net.CredentialCache.DefaultCredentials

            dtgDetalhamento.DataSource = Session("DataViewConta")
            dtgDetalhamento.DataBind()

            btVoltar.Visible = False

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "keyPrintPDF", "PrintPDF();", True)
            timerPDF.Enabled = True

        End If

    End Sub

    Protected Sub btVoltar_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Dashboard_Telefonia/Dashboar_Telefonia.aspx?larguraTela=" + Request.QueryString("larguraTela") + "")
    End Sub

    Protected Sub timerPDF_Tick(sender As Object, e As EventArgs)
        btVoltar.Visible = True
        timerPDF.Enabled = False
    End Sub
End Class
