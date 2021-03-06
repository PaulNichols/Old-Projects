Namespace ASPNET.StarterKit.Portal

    Public MustInherit Class DesktopPortalBanner
        Inherits System.Web.UI.UserControl

        Protected WelcomeMessage As System.Web.UI.WebControls.Label
        Protected siteName As System.Web.UI.WebControls.Label
        Protected tabs As System.Web.UI.WebControls.DataList

        Public tabIndex As Integer
        Public ShowTabs As Boolean = True
        Protected LogoffLink As String = ""

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Dynamically Populate the Portal Site Name
            siteName.Text = _portalSettings.PortalName

            ' If user logged in, customize welcome message
            If Request.IsAuthenticated = True Then
                Dim x As New UsersDB

                WelcomeMessage.Text = "Welcome " & x.GetFullName(Context.User.Identity.Name) & " <" & "span class=Accent" & ">|<" & "/span" & ">"

                ' if authentication mode is Cookie, provide a logoff link
                If Context.User.Identity.AuthenticationType = "Forms" Then
                    LogoffLink = "<" & "span class=""Accent""></span>" & ControlChars.Cr & "<" & "a href=" & Global.GetApplicationPath(Request) & "/Admin/Logoff.aspx class=SiteLink> Logoff" & "<" & "/a>"
                End If

                Dim Security As New ASPNET.StarterKit.Portal.UsersDB
                Dim dr As SqlDataReader = Security.GetSingleUserbyUserName(context.User.Identity.Name)
                While dr.Read
                    Dim Details As New UserDetails
                    With Details
                        .FullName = CType(dr("FullName"), String)
                        .Ref = CType(dr("Ref"), String)
                        .AuthorName = CType(dr("AuthorName"), String)
                        .Id = CType(dr("UserId"), Int32)
                        .AuthorTitle = CType(dr("AuthorTitle"), String)
                        .AuthorInitials = CType(dr("AuthorInitials"), String)
                        .Password = CType(dr("password"), String)
                        .Email = CType(dr("email"), String)
                        .username = CType(dr("username"), String)
                    End With
                    Session.Add("User", Details)
                End While
            End If

            ' Dynamically render portal tab strip
            If ShowTabs = True Then

                tabIndex = _portalSettings.ActiveTab.TabIndex

                ' Build list of tabs to be shown to user
                Dim authorizedTabs As New ArrayList()
                Dim addedTabs As Integer = 0

                Dim i As Integer
                For i = 0 To _portalSettings.DesktopTabs.Count - 1

                    Dim tab As TabStripDetails = CType(_portalSettings.DesktopTabs(i), TabStripDetails)

                    If PortalSecurity.IsInRoles(tab.AuthorizedRoles) Then
                        authorizedTabs.Add(tab)
                    End If

                    If addedTabs = tabIndex Then
                        tabs.SelectedIndex = addedTabs
                    End If

                    addedTabs += 1

                Next i

                ' Populate Tab List at Top of the Page with authorized tabs
                tabs.DataSource = authorizedTabs
                tabs.DataBind()

            End If


        End Sub


    End Class

End Namespace