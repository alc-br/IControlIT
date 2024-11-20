'------------------------------------------------------------------------------
' <gerado automaticamente>
'     Esse código foi gerado por uma ferramenta.
'
'     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
'     o código for recriado
' </gerado automaticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class Consulta_Caixa_Entrada

    '''<summary>
    '''Controle pnlmsg.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlmsg As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblMsgLocaliza.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblMsgLocaliza As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblAssunto.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblAssunto As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblDescricaoAssunto.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricaoAssunto As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblEmailDestino.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblEmailDestino As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblDescricaoEmailDestino.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricaoEmailDestino As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblEmailCopia.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblEmailCopia As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblDescricaoEmailCopia.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricaoEmailCopia As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblTexto.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblTexto As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblDescricaoTexto1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricaoTexto1 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblDescricaoTexto2.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricaoTexto2 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblDescricaoTexto3.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricaoTexto3 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblDescricaoTexto4.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricaoTexto4 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblDescricaoTexto5.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricaoTexto5 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblDescricaoTextAdicional.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricaoTextAdicional As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle btFecharMsg.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btFecharMsg As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btReenviarEmail.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btReenviarEmail As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle hdfIdMail.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents hdfIdMail As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Controle lblDLinhaConta.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDLinhaConta As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle cboMenssagem.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents cboMenssagem As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle btExecutar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btExecutar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle dtgLocaliza.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents dtgLocaliza As Global.System.Web.UI.WebControls.DataGrid

    '''<summary>
    '''Controle Div2.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents Div2 As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle btVoltar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btVoltar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Propriedade Master.
    '''</summary>
    '''<remarks>
    '''Propriedade gerada automaticamente.
    '''</remarks>
    Public Shadows ReadOnly Property Master() As IControlIT.Principal
        Get
            Return CType(MyBase.Master, IControlIT.Principal)
        End Get
    End Property
End Class
