Namespace ASPNET.StarterKit.Portal

    Public MustInherit Class Links
        Inherits ASPNET.StarterKit.Portal.PortalModuleControl

        Protected WithEvents myDataList As System.Web.UI.WebControls.DataList

		Protected linkImage As String = ""

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
        ' obtain a DataReader of link information from the Links
        ' table, and then databind the results to a templated DataList
        ' server control.  It uses the ASPNET.StarterKit.PortalLinkDB()
        ' data component to encapsulate all data functionality.
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Set the link image type
            If IsEditable Then
                linkImage = "~/images/edit.gif"
            Else
                linkImage = "~/images/navlink.gif"
            End If

            ' Obtain links information from the Links table
            ' and bind to the datalist control
            Dim links As New ASPNET.StarterKit.Portal.LinkDB()

            myDataList.DataSource = links.GetLinks(ModuleId)
            myDataList.DataBind()

		End Sub

		Protected Function ChooseURL(ByVal itemID As String, ByVal modID As String, ByVal URL As String) As String
			If IsEditable Then
				Return "~/DesktopModules/EditLinks.aspx?ItemID=" & CStr(itemID) & "&mid=" & modID
			Else
				Return URL
			End If
		End Function

		Protected Function ChooseTarget()
			If IsEditable Then
				Return "_self"
			Else
				Return "_new"
			End If
		End Function

		Protected Function ChooseTip(ByVal desc As String)
			If IsEditable Then
				Return "Edit"
			Else
				Return desc
			End If
		End Function
	End Class

End Namespace
