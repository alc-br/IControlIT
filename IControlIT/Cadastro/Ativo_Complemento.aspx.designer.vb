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


Partial Public Class Ativo_Complemento

    '''<summary>
    '''Controle lblDescricao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricao As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtDescricao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtDescricao As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle rfvDescricao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents rfvDescricao As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Controle lblAtivoTipo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblAtivoTipo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle cboAtivoTipo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents cboAtivoTipo As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle rfvAtivoTipo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents rfvAtivoTipo As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Controle lblIdentificacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblIdentificacao As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtIdentificacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtIdentificacao As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle pnlValidador.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlValidador As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle vceDescricao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents vceDescricao As Global.AjaxControlToolkit.ValidatorCalloutExtender

    '''<summary>
    '''Controle Label2.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents Label2 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle tbBotao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents tbBotao As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle btVoltar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btVoltar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btLimpar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btLimpar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btSalvar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btSalvar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btDesativar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btDesativar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btPDF.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btPDF As Global.System.Web.UI.WebControls.LinkButton

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
