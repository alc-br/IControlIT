'------------------------------------------------------------------------------
' <gerado automaticamente>
'     Este código foi gerado por uma ferramenta.
'
'     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
'     o código for recriado
' </gerado automaticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class Estoque

    '''<summary>
    '''Controle pnlDetalhe.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlDetalhe As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblDesativacaoAparelho.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDesativacaoAparelho As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblObservacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblObservacao As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle rfvAte.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents rfvAte As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Controle txtObservacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtObservacao As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle btCancela.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btCancela As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btOk.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btOk As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle pnlMsg.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlMsg As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblDescricaoRegulador.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescricaoRegulador As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle DivCustoFixo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents DivCustoFixo As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle dtgLista.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dtgLista As Global.System.Web.UI.WebControls.DataGrid

    '''<summary>
    '''Controle btFecharRegulador.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btFecharRegulador As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle Label1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents Label1 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle cboConsumidor.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents cboConsumidor As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle Label2.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents Label2 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle cboTipo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents cboTipo As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle btExecutar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btExecutar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnSolicitacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnSolicitacao As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnEstoque.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnEstoque As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnAtivo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnAtivo As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnAssistencia.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnAssistencia As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnDevolucao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnDevolucao As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnDescarte.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnDescarte As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle hdfTab.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents hdfTab As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Controle hfdId_Aparelho.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents hfdId_Aparelho As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Controle divSolicitacaoMenu.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divSolicitacaoMenu As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle divSolicitacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divSolicitacao As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle dtgSolicitacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dtgSolicitacao As Global.System.Web.UI.WebControls.DataGrid

    '''<summary>
    '''Controle divEstoqueMenu.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divEstoqueMenu As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle divEstoque.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divEstoque As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle dtgEstoque.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dtgEstoque As Global.System.Web.UI.WebControls.DataGrid

    '''<summary>
    '''Controle divAtivoMenu.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divAtivoMenu As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle divAtivo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divAtivo As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle dtgAtivo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dtgAtivo As Global.System.Web.UI.WebControls.DataGrid

    '''<summary>
    '''Controle divAssistenciaMenu.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divAssistenciaMenu As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle divAssistencia.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divAssistencia As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle dtgAssistencia.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dtgAssistencia As Global.System.Web.UI.WebControls.DataGrid

    '''<summary>
    '''Controle divDevolucaoMenu.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divDevolucaoMenu As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle divDevolucao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divDevolucao As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle dtgDevolucao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dtgDevolucao As Global.System.Web.UI.WebControls.DataGrid

    '''<summary>
    '''Controle divDescarteMenu.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divDescarteMenu As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle divDesativacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divDesativacao As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle dtgDesativado.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dtgDesativado As Global.System.Web.UI.WebControls.DataGrid

    '''<summary>
    '''Controle txtPesquisaEquipamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtPesquisaEquipamento As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle btPesquisar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btPesquisar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle txtQuantidadeSolicitacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtQuantidadeSolicitacao As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle lblDescQuantidadeSolicitacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescQuantidadeSolicitacao As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtQuantidadeEstoque.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtQuantidadeEstoque As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle lblDescQuantidadeEstoque.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescQuantidadeEstoque As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtQuantidadeAtivo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtQuantidadeAtivo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle lblDescQuantidadeAtivo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescQuantidadeAtivo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtQuantidadeAssistencia.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtQuantidadeAssistencia As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle lblDescQuantidadeAssistencia.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescQuantidadeAssistencia As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtQuantidadeDevolucao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtQuantidadeDevolucao As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle lblDescQuantidadeDevolucao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescQuantidadeDevolucao As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtQuantidadeDesativado.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtQuantidadeDesativado As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle lblDescQuantidadeDesativado.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDescQuantidadeDesativado As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle tbBotao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents tbBotao As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle btVoltar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btVoltar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btSalvar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btSalvar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btExportar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btExportar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btSolicitação.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btSolicitação As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btNotaFiscal.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btNotaFiscal As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btEmail.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btEmail As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btAlerta.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btAlerta As Global.System.Web.UI.WebControls.LinkButton

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
