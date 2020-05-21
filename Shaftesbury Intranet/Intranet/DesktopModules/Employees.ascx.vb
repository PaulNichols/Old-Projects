Namespace ASPNET.StarterKit.Portal

    Public MustInherit Class Employees
        Inherits ASPNET.StarterKit.Portal.PortalModuleControl
        Protected WithEvents grdEmployees As System.Web.UI.WebControls.DataGrid


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
            grdEmployees.PageSize = CType(System.Configuration.ConfigurationSettings.AppSettings("EmployeeGridPageSize"), Int32)
        End Sub

#End Region

        '*******************************************************
        '
        ' The Page_Load event handler on this User Control is used to
        ' obtain a DataReader of contact information from the Contacts
        ' table, and then databind the results to a DataGrid
        ' server control.  It uses the ASPNET.StarterKit.PortalContactsDB()
        ' data component to encapsulate all data functionality.
        '
        '*******************************************************'

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            GridBind()
        End Sub

        Private Sub GridBind()
            ' Obtain contact information from Contacts table
            ' and bind to the DataGrid Control

            Dim contacts As New ASPNET.StarterKit.Portal.ContactsDB
            With grdEmployees
                .DataSource = contacts.GetContacts(ModuleId, 1)
                .DataBind()

                .PagerStyle.Visible = (.PageCount > 1)
                .ShowFooter = (.PageCount > 1)
            End With
        End Sub

        Private Sub grdContacts_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdEmployees.PageIndexChanged
            grdEmployees.CurrentPageIndex = e.NewPageIndex
            GridBind()
        End Sub
    End Class

End Namespace