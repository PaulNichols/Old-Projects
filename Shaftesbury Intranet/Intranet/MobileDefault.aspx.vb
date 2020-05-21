Imports System.Web.UI.MobileControls

Namespace ASPNET.StarterKit.Portal

    Public Class MobileDefault
        Inherits System.Web.UI.MobileControls.MobilePage

        Protected Label1 As System.Web.UI.MobileControls.Label
        Protected WithEvents TabView As ASPNET.StarterKit.Portal.MobileControls.TabbedPanel
        Protected Form1 As System.Web.UI.MobileControls.Form
        Protected DeviceSpecific1 As System.Web.UI.MobileControls.DeviceSpecific

        Private authorizedTabs As New ArrayList()

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

#End Region


        '*********************************************************************
        '
        ' Page_Init Event Handler
        '
        ' The Page_Init event handler executes at the very beginning of each page
        ' request (immediately before Page_Load).
        '
        ' The Page_Init event handler calls the PopulateTabs utility method
        ' to insert empty tabs into the tab view. It then determines the tab
        ' index of the currently requested portal, and then calls the
        ' PopulateTabView utility method to dynamically populate the
        ' active portal view.
        '
        '*********************************************************************

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            Dim tabIndex As Integer = 0
            Dim tabID As Integer = 1

            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()

            ' Obtain current tab index and tab id settings
            Dim tabSetting As String = CType(HiddenVariables("ti"), String)

            If Not (tabSetting Is Nothing) Then

                Dim comma As Integer = tabSetting.IndexOf(","c)
                tabIndex = CInt(tabSetting.Substring(0, comma))
                tabID = CInt(tabSetting.Substring((comma + 1)))

            End If

            ' Obtain PortalSettings from Current Context
            LoadPortalSettings(tabIndex, tabID)

            ' Populate tab list with empty tabs
            PopulateTabStrip()

            ' Populate the current tab view
            PopulateTabView(tabIndex)

        End Sub


        '*********************************************************************
        '
        ' PopulateTabStrip method
        '
        ' The PopulateTabStrip method is used to dynamically create and add
        ' tabs for each tab view defined in the portal configuration.
        '
        '*********************************************************************

        Sub PopulateTabStrip()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim i As Integer
            For i = 0 To _portalSettings.MobileTabs.Count - 1

                ' Create a MobilePortalTab control for the tab,
                ' and add it to the tab view.

                Dim tab As TabStripDetails = CType(_portalSettings.MobileTabs(i), TabStripDetails)

                If PortalSecurity.IsInRoles(tab.AuthorizedRoles) Then

                    Dim tabPanel As New MobilePortalTab()
                    tabPanel.PaneTitle = tab.TabName

                    TabView.Panes.Add(tabPanel)

                End If

            Next i

        End Sub


        '*********************************************************************
        '
        ' PopulateTabView method
        '
        ' The PopulateTabView method dynamically populates a portal tab
        ' with each module defined in the portal configuration.
        '
        '*********************************************************************

        Sub PopulateTabView(ByVal tabIndex As Integer)

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Ensure that the visiting user has access to the current page
            If PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AuthorizedRoles) = False Then
                Response.Redirect("~/Admin/MobileAccessDenied.aspx")
            End If

            ' Obtain reference to container mobile tab
            Dim view As MobilePortalTab = CType(TabView.Panes(tabIndex), MobilePortalTab)

            ' Dynamically populate the view
            If _portalSettings.ActiveTab.Modules.Count > 0 Then

                ' Loop through each entry in the configuration system for this tab
                Dim _moduleSettings As ModuleSettings
                For Each _moduleSettings In _portalSettings.ActiveTab.Modules

                    ' Only add the module if it support Mobile devices
                    If _moduleSettings.ShowMobile Then

                        Dim moduleControl As MobilePortalModuleControl = CType(Page.LoadControl(_moduleSettings.MobileSrc), MobilePortalModuleControl)
                        moduleControl.ModuleConfiguration = _moduleSettings

                        view.Panes.Add(moduleControl)

                    End If

                Next _moduleSettings

            End If

        End Sub


        '*********************************************************************
        '
        ' TabView_OnActivate Event Handler
        '
        ' The TabView_OnActivate event handler executes when the user switches
        ' tabs in the tab view. It calls the PopulateTabView utility
        ' method to dynamically populate the newly activated view.
        '
        '*********************************************************************

        Private Sub TabView_OnTabActivate(ByVal sender As Object, ByVal e As EventArgs) Handles TabView.TabActivate

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim tabIndex As Integer = TabView.ActivePaneIndex
            Dim tabID As Integer = CType(_portalSettings.MobileTabs(tabIndex), TabStripDetails).TabId

            ' Store tabindex in a hidden variable to preserve accross round trips
            If tabIndex <> 0 Then
                HiddenVariables("ti") = String.Concat(tabIndex.ToString(), ",", tabID.ToString())
            Else
                HiddenVariables.Remove("ti")
            End If

            ' Check to see if portal settings need reloading
            LoadPortalSettings(tabIndex, tabID)

            ' Populate the newly active tab.
            PopulateTabView(tabIndex)

            ' Set the view to summary mode, where a summary of all the modules are shown.
            CType(TabView.ActivePane, MobilePortalTab).SummaryView = True

        End Sub


        '*********************************************************************
        '
        ' LoadPortalSettings method
        '
        ' LoadPortalSettings is a helper methods that loads portal settings for
        ' the selected tab.  It first verifies that the settings haven't already
        ' been set within the Global.asax file -- if they are different (in the
        ' case that a tab change is made) then the method reloads the appropriate
        ' tab data.
        '
        '*********************************************************************

        Sub LoadPortalSettings(ByVal tabIndex As Integer, ByVal tabId As Integer)

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If _portalSettings.ActiveTab.TabId <> tabId Or _portalSettings.ActiveTab.TabIndex <> tabIndex Then
                HttpContext.Current.Items("PortalSettings") = New PortalSettings(tabIndex, tabId)
            End If

        End Sub

    End Class

End Namespace