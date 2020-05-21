Namespace ASPNET.StarterKit.Portal

    Public MustInherit Class Document
        Inherits ASPNET.StarterKit.Portal.PortalModuleControl
        Protected WithEvents lblAreaOrder As System.Web.UI.WebControls.Label

        Protected WithEvents myDataGrid As System.Web.UI.WebControls.DataGrid

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        '*******************************************************
        '
        ' The Page_Load event handler on this User Control is used to
        ' obtain a SqlDataReader of document information from the 
        ' Documents table, and then databind the results to a DataGrid
        ' server control.  It uses the ASPNET.StarterKit.PortalDocumentDB()
        ' data component to encapsulate all data functionality.
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Obtain Document Data from Documents table
            ' and bind to the datalist control
            DataGridBind()
            'If Not IsPostBack Then


            '    lblAreaOrder.Text = "Desc"
            'End If
        End Sub


        '*******************************************************
        '
        ' GetBrowsePath() is a helper method used to create the url   
        ' to the document.  If the size of the content stored in the   
        ' database is non-zero, it creates a path to browse that.   
        ' Otherwise, the FileNameUrl value is used.
        '
        ' This method is used in the databinding expression for
        ' the browse Hyperlink within the DataGrid, and is called 
        ' for each row when DataGrid.DataBind() is called.  It is 
        ' defined as a helper method here (as opposed to inline 
        ' within the template) to improve code organization and
        ' avoid embedding logic within the content template.
        '
        '*******************************************************'

        Private Sub DataGridBind()
            Dim documents As New ASPNET.StarterKit.Portal.DocumentDB
            '            Dim dr As SqlDataReader
            '            Dim t As New DataTable(
            '            Dim row As New DataRow
            '            t.ImportRow(
            'dim dv as new DataView(dr.GetValues(

            Try
                myDataGrid.DataSource = documents.GetDocuments(ModuleId, CType(Session.Item("User"), ASPNET.StarterKit.Portal.UserDetails).Id)
                myDataGrid.DataBind()
            Catch
            End Try
        End Sub

        Function GetBrowsePath(ByVal url As String, ByVal size As Object, ByVal documentId As Integer) As String

            Dim hasSize As Boolean = True

            If Size Is DBNull.Value Then

                hasSize = False

            ElseIf CInt(Size) < 1 Then

                hasSize = False

            End If

            If hasSize = False Then

                ' return the FileNameUrl
                Return url

            End If

            ' if there is content in the database, create an 
            ' url to browse it
            Return "../DesktopModules/ViewDocument.aspx?DocumentID=" & documentId.ToString()

        End Function

        Private Sub grdContacts_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles myDataGrid.SortCommand
            If lblAreaOrder.Text = "Desc" Then
                lblAreaOrder.Text = "Asc"
            Else
                lblAreaOrder.Text = "Desc"
            End If
        End Sub

    End Class

End Namespace