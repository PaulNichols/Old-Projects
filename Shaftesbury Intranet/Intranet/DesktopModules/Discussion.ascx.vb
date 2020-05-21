Namespace ASPNET.StarterKit.Portal

    Public MustInherit Class Discussion
        Inherits ASPNET.StarterKit.Portal.PortalModuleControl

        Protected WithEvents TopLevelList As System.Web.UI.WebControls.DataList

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
        ' The Page_Load server event handler on this User Control is used
        ' on the first visit of the page to obtain and databind a list of
        ' discussion messages.
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            If Page.IsPostBack = False Then
                BindList()
            End If

        End Sub


        '*******************************************************
        '
        ' The BindList method obtains the list of top-level messages
        ' from the Discussion table and then databinds them against
        ' the "TopLevelList" asp:datalist server control.  It uses
        ' the ASPNET.StarterKit.PortalDiscussionDB() data component to encapsulate
        ' all data access functionality.
        '
        '*******************************************************

        Sub BindList()

            ' Obtain a list of discussion messages for the module
            ' and bind to datalist
            Dim discuss As New ASPNET.StarterKit.Portal.DiscussionDB()

            TopLevelList.DataSource = discuss.GetTopLevelMessages(ModuleId)
            TopLevelList.DataBind()

        End Sub


        '*******************************************************
        '
        ' The GetThreadMessages method is used to obtain the list
        ' of messages contained within a sub-topic of the
        ' a top-level discussion message thread.  This method is
        ' used to populate the "DetailList" asp:datalist server control
        ' in the SelectedItemTemplate of "TopLevelList".
        '
        '*******************************************************

        Function GetThreadMessages() As SqlDataReader

            ' Obtain a list of discussion messages for the module
            Dim discuss As New ASPNET.StarterKit.Portal.DiscussionDB()
            Dim dr As SqlDataReader = discuss.GetThreadMessages(TopLevelList.DataKeys(TopLevelList.SelectedIndex).ToString())

            ' Return the filtered DataView
            Return dr

        End Function


        '*******************************************************
        '
        ' The TopLevelList_Select server event handler is used to
        ' expand/collapse a selected discussion topic within the
        ' hierarchical <asp:DataList> server control.
        '
        '*******************************************************

        Private Sub TopLevelList_Select(ByVal Sender As Object, ByVal e As DataListCommandEventArgs) Handles TopLevelList.ItemCommand

            ' Determine the command of the button (either "select" or "collapse")
            Dim command As String = CType(e.CommandSource, ImageButton).CommandName

            ' Update asp:datalist selection index depending upon the type of command
            ' and then rebind the asp:datalist with content
            If command = "collapse" Then
                TopLevelList.SelectedIndex = -1
            Else
                TopLevelList.SelectedIndex = e.Item.ItemIndex
            End If

            BindList()

        End Sub


        '*******************************************************
        '
        ' The FormatUrl method is a helper messages called by a
        ' databinding statement within the <asp:DataList> server
        ' control template.  It is defined as a helper method here
        ' (as opposed to inline within the template) to to improve
        ' code organization and avoid embedding logic within the
        ' content template.
        '
        '*******************************************************

        Function FormatUrl(ByVal item As Integer) As String

            Return "~/DesktopModules/DiscussDetails.aspx?ItemID=" & item & "&mid=" & ModuleId

        End Function


        '*******************************************************
        '
        ' The NodeImage method is a helper method called by a
        ' databinding statement within the <asp:datalist> server
        ' control template.  It controls whether or not an item
        ' in the list should be rendered as an expandable topic
        ' or just as a single node within the list.
        '
        '*******************************************************

        Function NodeImage(ByVal count As Integer) As String

            If count > 0 Then
                Return "~/images/plus.gif"
            Else
                Return "~/images/node.gif"
            End If

        End Function

    End Class

End Namespace