Partial Public Class KeepSessionAlive
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("RefreshTime") = DateTime.UtcNow
    End Sub
End Class
