'------------------------------------------------------------------------------
' <gerado automaticamente>
'     Esse c�digo foi gerado por uma ferramenta.
'
'     As altera��es ao arquivo poder�o causar comportamento incorreto e ser�o perdidas se
'     o c�digo for recriado
' </gerado automaticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class Setor

    '''<summary>
    '''Controle lblDescricao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricao As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtDescricao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtDescricao As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle rfvDescricao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents rfvDescricao As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Controle lblMontaHierarquia.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblMontaHierarquia As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtGrupo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtGrupo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle btGrupo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btGrupo As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lstOrigem.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lstOrigem As Global.System.Web.UI.WebControls.ListBox

    '''<summary>
    '''Controle btMoveSelecionado.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btMoveSelecionado As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lstDestino.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lstDestino As Global.System.Web.UI.WebControls.ListBox

    '''<summary>
    '''Controle btMoveSelecao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btMoveSelecao As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lblIdentificacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblIdentificacao As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtIdentificacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtIdentificacao As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle pnlValidador.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlValidador As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle vceDescricao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents vceDescricao As Global.AjaxControlToolkit.ValidatorCalloutExtender

    '''<summary>
    '''Controle lblMessage.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblMessage As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle tbBotao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents tbBotao As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle btVoltar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btVoltar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btLimpar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btLimpar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btSalvar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btSalvar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btDesativar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btDesativar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lblEncerrar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblEncerrar As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle btPDF.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btPDF As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lblPdf.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declara��o de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblPdf As Global.System.Web.UI.HtmlControls.HtmlGenericControl

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
