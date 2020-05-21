Imports System
Imports System.Data
Imports System.Drawing
Imports System.Collections
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.MobileControls
Imports System.Web.UI.MobileControls.Adapters
Imports ASPNET.StarterKit.Portal.MobileControls



<Assembly: TagPrefix("ASPNET.StarterKit.Portal.MobileControls", "portal")> 


Namespace ASPNET.StarterKit.Portal

    '*********************************************************************
    '
    ' MobilePortalModuleControl
    '
    ' The MobilePortalModuleControl class is the base class used for
    ' each module user control in the mobile portal. Since it implements
    ' the IContentsPane interface, any control inheriting from this class
    ' can be used as a module in a portal tab.
    '
    '*********************************************************************

    Public Class MobilePortalModuleControl
        Inherits UserControl
        Implements IContentsPane

        Private _moduleConfiguration As ModuleSettings
        Private _summaryControl As Control

        '*********************************************************************
        '
        ' MobilePortalModuleControl.ModuleConfiguration Property
        '
        ' Returns the configuration information for this module.
        '
        '*********************************************************************

        Public Property ModuleConfiguration() As ModuleSettings

            Get
                Return _moduleConfiguration
            End Get
            Set(ByVal Value As ModuleSettings)
                _moduleConfiguration = Value
            End Set

        End Property


        '*********************************************************************
        '
        ' MobilePortalModuleControl.Tab Property
        '
        ' Returns the parent portal tab.
        '
        '*********************************************************************

        Public ReadOnly Property Tab() As MobilePortalTab

            Get
                Return Parent
            End Get

        End Property


        '*********************************************************************
        '
        ' MobilePortalModuleControl.ModuleTitle Property
        '
        ' Returns the name of this module.
        '
        '*********************************************************************

        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property ModuleTitle() As String

            Get
                Return _moduleConfiguration.ModuleTitle
            End Get

        End Property


        '*********************************************************************
        '
        ' MobilePortalModuleControl.ModuleId Property
        '
        ' Returns the unique ID of this module.
        '
        '*********************************************************************

        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property ModuleId() As Integer

            Get
                Return _moduleConfiguration.ModuleId
            End Get

        End Property

        '*********************************************************************
        '
        ' IContentsPane.Title Property
        '
        ' Returns the name of the module, to be used as the pane title
        ' when used inside a tab.
        '
        '*********************************************************************

        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        ReadOnly Property Title() As String Implements IContentsPane.Title

            Get
                Return _moduleConfiguration.ModuleTitle
            End Get

        End Property


        '*********************************************************************
        '
        ' IContentsPane.OnSetSummaryMode Method
        '
        ' OnSetSummaryMode is called on each child pane when the parent tab
        ' changes from showing summaries to individual details or vice versa.
        ' This method calls the UpdateVisibility utility method to 
        ' update the visibility of child controls.
        '
        '*********************************************************************

        Sub OnSetSummaryMode() Implements IContentsPane.OnSetSummaryMode

            UpdateVisibility()

        End Sub


        '*********************************************************************
        '
        ' MobilePortalModuleControl.OnInit Method
        '
        ' OnInit is called when the control is created and added to the 
        ' control tree. OnInit looks for a child control that renders the
        ' summary view of the module, and creates a default one (with a
        ' simple LinkCommand control) if no summary is found.
        '
        '*********************************************************************

        Protected Overrides Sub OnInit(ByVal e As EventArgs)

            MyBase.OnInit(e)

            ' Look for a control that renders the summary.
            _summaryControl = FindControl("summary")

            ' There could be no summary control, or the summary control may be
            ' an empty panel. If there's no summary UI, automatically generate one.
            If _summaryControl Is Nothing Or (TypeOf _summaryControl Is Panel And Not _summaryControl.HasControls()) Then

                ' Create and initialize a new LinkCommand control
                Dim command = New LinkCommand()
                command.Text = Me.ModuleTitle

                ' Set the command name to the details command, so that
                ' event bubbling can recognize it as a command to go to
                ' details view.
                command.CommandName = ContentsPanel.DetailsCommand

                ' Add it to the appropriate place.
                If Not (_summaryControl Is Nothing) Then

                    _summaryControl.Controls.Add(command)

                Else

                    Controls.Add(command)
                    _summaryControl = command

                End If

            End If

        End Sub


        '*********************************************************************
        '
        ' MobilePortalModuleControl.OnLoad Method
        '
        ' OnLoad is called when the control is created and added to the 
        ' control tree, after OnInit. OnLoad calls the UpdateVisibility
        ' utility method to update the visibility of child controls.
        '
        '*********************************************************************

        Protected Overrides Sub OnLoad(ByVal e As EventArgs)

            MyBase.OnLoad(e)
            UpdateVisibility()

        End Sub


        '*********************************************************************
        '
        ' MobilePortalModuleControl.UpdateVisibility Method
        '
        ' UpdateVisibility updates the visibility of child controls
        ' depending on the current setting. If the module is currently
        ' being shown in summary mode, all children except the summary
        ' control are hidden. If the module is currently being shown
        ' in details mode, only the summary control is hidden.
        '
        '*********************************************************************

        Private Sub UpdateVisibility()

            Dim summary As Boolean = Not (Tab Is Nothing) And Tab.SummaryView

            Dim child As Control
            For Each child In Controls
                child.Visible = Not summary
            Next child

            If Not (_summaryControl Is Nothing) Then
                _summaryControl.Visible = summary
            End If

        End Sub

    End Class


    '*********************************************************************
    '
    ' MobilePortalTab Class
    '
    ' The MobilePortalTab class is used for each tab of the mobile 
    ' portal.
    '
    '*********************************************************************

    Public Class MobilePortalTab
        Inherits ContentsPanel

    End Class

End Namespace

Namespace ASPNET.StarterKit.Portal.MobileControls

    '*********************************************************************
    '
    ' LinkCommand Class
    '
    ' The LinkCommand class is used for a simple custom version of the
    ' Command control. Although the class itself has no added or modified
    ' functionality, it allows a new adapter to be specified. On
    ' HTML devices, this control renders as a hyperlink rather than
    ' a button.
    '
    '*********************************************************************

    Public Class LinkCommand
        Inherits Command

    End Class


    '*********************************************************************
    '
    ' HtmlLinkCommandAdapter Class
    '
    ' The HtmlLinkCommandAdapter class is used to render the LinkCommand
    ' control on an HTML device. Unlike the Command control, which renders
    ' as a button, the HtmlLinkCommandAdapter renders a LinkCommand as
    ' a hyperlink. Only the Render method needs to be overriden.
    '
    '*********************************************************************

    Public Class HtmlLinkCommandAdapter
        Inherits HtmlCommandAdapter


        '*********************************************************************
        '
        ' HtmlLinkCommandAdapter.Render Method
        '
        ' The Render method performs rendering of the LinkCommand control.
        '
        '*********************************************************************
        Public Overloads Overrides Sub Render(ByVal writer As HtmlMobileTextWriter)
            ' Render a postback event as an anchor.
            RenderPostBackEventAsAnchor(writer, Nothing, Control.Text)

            ' Write a break, if necessary.
            writer.WriteBreak()

        End Sub

    End Class


    '*********************************************************************
    '
    ' Panels Package
    '
    ' The Panels Package is a set of bonus mobile controls used for
    ' the Mobile Portal. The package provides a new set of 
    ' control classes. All of these controls inherit from the 
    ' System.Web.UI.MobileControls.Panel class.
    '
    '      MultiPanel
    '          A base class capable of managing multiple child controls,
    '          called "panes". Each child pane must implement the 
    '          IPanelPane interface.
    '      ChildPanel
    '          A base class for panels that can be used as child panes
    '          of MultiPanel panels. MultiPanel itself inherits from
    '          ChildPanel, so you can nest one MultiPanel as a child
    '          pane of another.
    '      TabbedPanel
    '          A specialized type of MultiPanel that comes with 
    '          adapters for rendering the panel as a tab view where
    '          appropriate. On other devices, adapters render the
    '          TabbedPanel using a separate menu screen.
    '      ContentsPanel
    '          A specialized type of MultiPanel that can show either
    '          a summary view, where all child panes are shown
    '          simultaneously, or a details view that shows the
    '          active pane. Each child pane must implement the
    '          IContentsPane interface.
    '
    ' Although these controls are fairly advanced compared to the
    ' rest of the portal, full source code is provided.
    ' 
    '*********************************************************************

    '*********************************************************************
    '
    ' IPanelPane interface
    '
    ' The IPanelPane interface must be implemented by any control 
    ' that needs to be a child pane of a MultiPanel or derivative
    ' control. The interface has the following members:
    '
    '      Title property
    '          Returns the title of the pane.
    '
    '*********************************************************************
    Public Interface IPanelPane

        ReadOnly Property Title() As String

    End Interface


    '*********************************************************************
    '
    ' IContentsPane interface
    '
    ' The IContentsPane interface must be implemented by any control 
    ' that needs to be a child pane of a ContentsPanel control.
    ' The interface has the following members:
    '
    '      Title property
    '          Returns the title of the pane.
    '      OnSetSummaryMode method
    '          Called when the ContentsPane control switches
    '          from summary view to item details view.
    '
    '*********************************************************************
    Public Interface IContentsPane
        Inherits IPanelPane

        Sub OnSetSummaryMode()

    End Interface

    '*********************************************************************
    '
    ' ChildPanel Class
    '
    ' The ChildPanel Class is a control that inherits from 
    ' System.Web.UI.MobileControls.Panel, and can be placed inside
    ' a MultiPanel control. Even MultiPanel inherits from ChildPanel,
    ' allowing nesting of MultiPanel controls.
    '
    '*********************************************************************
    Public Class ChildPanel
        Inherits Panel
        Implements IPanelPane, INamingContainer

        '*********************************************************************
        '
        ' IPanelPane.Title Property
        '
        ' Returns the title of the pane.
        '
        '*********************************************************************

        ReadOnly Property Title() As String Implements IPanelPane.Title

            Get
                Return Me.PaneTitle
            End Get

        End Property

        '*********************************************************************
        '
        ' ChildPanel.PaneTitle Property
        '
        ' Returns the title of the pane.
        '
        '*********************************************************************

        Public Property PaneTitle() As String

            Get
                ' Load the title from the ViewState property bag, 
                ' defaulting to an empty String.
                Dim s As String = CStr(ViewState("Title"))

                If Not (s Is Nothing) Then
                    Return s
                Else
                    Return String.Empty
                End If

            End Get

            Set(ByVal Value As String)

                ' Save the title to the ViewState property bag.
                ViewState("Title") = Value

            End Set

        End Property

        '*********************************************************************
        '
        ' ChildPanel.PaginateChildren Property
        '
        ' The PaginateChildren property controls whether the form
        ' can paginate children of the panel individually. Overriden
        ' to allow contents to be paginated.
        '
        '*********************************************************************

        Protected Overrides ReadOnly Property PaginateChildren() As Boolean

            Get
                Return True
            End Get

        End Property

    End Class


    '*********************************************************************
    '
    ' MultiPanel Class
    '
    ' The MultiPanel Class is a control that inherits from 
    ' ChildPanel, and can manage one or more child controls or "panes".
    '
    '*********************************************************************

    Public Class MultiPanel
        Inherits ChildPanel

        ' Collection of panes.
        Private _panes As PanelPaneCollection

        '*********************************************************************
        '
        ' MultiPanel.Panes Property
        '
        ' Returns the collection of child panes.
        '
        '*********************************************************************

        Public ReadOnly Property Panes() As PanelPaneCollection
            Get
                ' If not yet created, create the collection.
                If _panes Is Nothing Then
                    _panes = New PanelPaneCollection(Me)
                End If
                Return _panes
            End Get
        End Property

        '*********************************************************************
        '
        ' MultiPanel.ActivePane Property
        '
        ' Get or set the currently active child pane.
        '
        '*********************************************************************

        Public Property ActivePane() As IPanelPane
            Get
                ' Get the index of the active pane, and use it to
                ' look up the active pane.
                Dim index As Integer = ActivePaneIndex

                If index <> -1 Then
                    Return Panes(index)
                Else
                    Return Nothing
                End If

            End Get

            Set(ByVal Value As IPanelPane)
                ' Find the index of the given pane, and use it to
                ' set the active pane index.
                Dim index As Integer = Panes.IndexOf(Value)

                If index = -1 Then
                    Throw New Exception("Pane not in Panes collection")
                End If

                ActivePaneIndex = index

            End Set

        End Property

        '*********************************************************************
        '
        ' MultiPanel.ActivePaneIndex Property
        '
        ' Get or set the index of the currently active child pane.
        '
        '*********************************************************************

        Public Property ActivePaneIndex() As Integer

            Get
                ' Get the index from the ViewState property bag, defaulting
                ' to the first pane if not found.
                Dim o As Object = ViewState("ActivePaneIndex")

                If Not (o Is Nothing) Then
                    Return CInt(o)
                Else
                    If Panes.Count > 0 Then
                        Return 0
                    Else
                        Return -1
                    End If
                End If

            End Get

            Set(ByVal Value As Integer)

                ' Make sure index is within range.
                If Value < 0 Or Value >= Panes.Count Then
                    Throw New Exception("Active pane index out of range")
                End If

                ' Set the index in the ViewState property bag.
                ViewState("ActivePaneIndex") = Value

            End Set

        End Property


        '*********************************************************************
        '
        ' MultiPanel.AddParsedSubObject Method
        '
        ' AddParsedSubObject is called by the framework when a child
        ' control is being added to the control from the persistence format.
        ' AddParsedSubObject below checks if the added control is a 
        ' child pane, and automatically adds it to the Panes collection.
        '
        '*********************************************************************

        Protected Overrides Sub AddParsedSubObject(ByVal obj As Object)

            Dim pane As IPanelPane = obj

            ' Only allow panes as children.
            If pane Is Nothing Then
                Throw New Exception("A MultiPanel control can only contain panes.")
            End If

            ' Add the pane to the Panes collection.
            Panes.AddInternal(pane)
            MyBase.AddParsedSubObject(obj)

        End Sub


        '*********************************************************************
        '
        ' MultiPanel.OnRender Method
        '
        ' OnRender is called by the framework to render the control.
        ' By default, OnRender of a MultiPanel only renders the active 
        ' child pane. Specialized versions of the control, such as
        ' TabbedPanel and ContentsPanel, have different behavior.
        '
        '*********************************************************************

        Protected Overrides Sub OnRender(ByVal writer As HtmlTextWriter)

            CType(ActivePane, Control).RenderControl(writer)

        End Sub


        '*********************************************************************
        '
        ' MultiPanel.PaginateRecursive Method
        '
        ' PaginateRecursive is called by the framework to recursively
        ' paginate children. For MultiPanel controls, PaginateRecursive
        ' only paginates the active child pane.
        '
        '*********************************************************************

        Public Overrides Sub PaginateRecursive(ByVal pager As ControlPager)

            Dim _activePane As Control = CType(ActivePane, Control)

            ' Active pane may not be a mobile control (e.g. it may be
            ' a user control).
            Dim mobileCtl As MobileControl = Nothing

            Try
                mobileCtl = CType(_activePane, MobileControl)
            Catch
                ' cast failed, not a mobile control
            End Try

            If Not (mobileCtl Is Nothing) Then

                ' Paginate the children.
                mobileCtl.PaginateRecursive(pager)

                ' Set own first and last page from results of child
                ' pagination.
                Me.FirstPage = mobileCtl.FirstPage
                Me.LastPage = pager.PageCount

            Else

                ' Call the DoPaginateChildren utility method to 
                ' paginate a non-mobile child.
                Dim firstAssignedPage As Integer = -1
                DoPaginateChildren(pager, _activePane, firstAssignedPage)

                ' Set own first and last page from results of child
                ' pagination.
                If firstAssignedPage <> -1 Then
                    Me.FirstPage = firstAssignedPage
                Else
                    Me.FirstPage = pager.GetPage(100)
                End If

                Me.LastPage = pager.PageCount

            End If

        End Sub


        '*********************************************************************
        '
        ' MultiPanel.DoPaginateChildren Static Method
        '
        ' The DoPaginateChildren method paginates non-mobile child
        ' controls, looking for mobile controls inside them.
        '
        '*********************************************************************

        Private Shared Sub DoPaginateChildren(ByVal pager As ControlPager, ByVal ctl As Control, ByRef firstAssignedPage As Integer)

            ' Search all children of the control.
            If ctl.HasControls() Then

                Dim child As Control

                For Each child In ctl.Controls

                    If child.Visible Then

                        ' Look for a visible mobile control.
                        Dim mobileCtl As MobileControl = Nothing
                        Try
                            mobileCtl = CType(child, MobileControl)
                        Catch
                            ' cast failed -- not a mobile control
                        End Try

                        If Not (mobileCtl Is Nothing) Then

                            ' Paginate the mobile control.
                            mobileCtl.PaginateRecursive(pager)

                            ' If this is the first control being paginated,
                            ' set the first assigned page.
                            If firstAssignedPage = -1 Then
                                firstAssignedPage = mobileCtl.FirstPage
                            End If

                        Else

                            If TypeOf child Is UserControl Then

                                ' Continue paginating user controls, which may contain
                                ' their own mobile children.
                                DoPaginateChildren(pager, child, firstAssignedPage)

                            End If

                        End If

                    End If

                Next child

            End If

        End Sub

    End Class


    '*********************************************************************
    '
    ' PanelPaneCollection Class
    '
    ' The PanelPaneCollection Class is used to keep a collection of
    ' child panes of a MultiPanel control. The class implements 
    ' ICollection, so it can be used as a general collection.
    '
    '*********************************************************************

    Public Class PanelPaneCollection
        Implements ICollection

        ' Private instance variables.
        Private _parent As MultiPanel
        Private _items As New ArrayList()


        ' Can only be instantiated by MultiPanel.
        Friend Sub New(ByVal parent As MultiPanel)

            ' Save off reference to parent control.
            _parent = parent

        End Sub


        '*********************************************************************
        '
        ' PanelPaneCollection.Add Method
        '
        ' Adds a pane to the collection.
        '
        '*********************************************************************

        Public Sub Add(ByVal pane As IPanelPane)

            ' Add the pane to the parent's child controls collection.
            _parent.Controls.Add(CType(pane, Control))
            _items.Add(pane)

        End Sub


        '*********************************************************************
        '
        ' PanelPaneCollection.AddInternal Method
        '
        ' Adds a pane to the collection, but does not add it to the parent's
        ' controls. This is called by the parent control itself to add 
        ' panes.
        '
        '*********************************************************************

        Friend Sub AddInternal(ByVal pane As IPanelPane)

            _items.Add(pane)

        End Sub


        '*********************************************************************
        '
        ' PanelPaneCollection.Remove Method
        '
        ' Removes a pane from the collection.
        '
        '*********************************************************************

        Public Sub Remove(ByVal pane As IPanelPane)

            ' Remove the pane from the parent's child controls collection.
            _parent.Controls.Remove(CType(pane, Control))
            _items.Remove(pane)

        End Sub


        '*********************************************************************
        '
        ' PanelPaneCollection.Clear Method
        '
        ' Removes all panes from the collection.
        '
        '*********************************************************************

        Public Sub Clear()

            ' Remove all child controls from the parent.
            Dim pane As Control

            For Each pane In _items
                _parent.Controls.Remove(pane)
            Next pane

            _items.Clear()

        End Sub


        '*********************************************************************
        '
        ' PanelPaneCollection.this[] Property
        '
        ' Returns a pane by index.
        '
        '*********************************************************************

        Default Public ReadOnly Property Item(ByVal index As Integer) As IPanelPane

            Get
                Return CType(_items(index), IPanelPane)
            End Get

        End Property


        '*********************************************************************
        '
        ' PanelPaneCollection.Count Property
        '
        ' Returns the number of panes in the collection.
        '
        '*********************************************************************

        Public ReadOnly Property Count() As Integer Implements ICollection.Count

            Get
                Return _items.Count
            End Get

        End Property


        '*********************************************************************
        '
        ' PanelPaneCollection.IndexOf Method
        '
        ' Returns the index of a given pane.
        '
        '*********************************************************************

        Public Function IndexOf(ByVal pane As IPanelPane) As Integer

            Return _items.IndexOf(pane)

        End Function


        '*********************************************************************
        '
        ' PanelPaneCollection.IsReadOnly Property
        '
        ' Returns whether the collection is read-only.
        '
        '*********************************************************************

        Public ReadOnly Property IsReadOnly() As Boolean

            Get
                Return _items.IsReadOnly
            End Get

        End Property


        '*********************************************************************
        '
        ' PanelPaneCollection.IsSynchronized Property
        '
        ' Returns whether the collection is synchronized.
        '
        '*********************************************************************

        Public ReadOnly Property IsSynchronized() As Boolean Implements ICollection.IsSynchronized

            Get
                Return False
            End Get

        End Property


        '*********************************************************************
        '
        ' PanelPaneCollection.SyncRoot Property
        '
        ' Returns the collection's synchronization root.
        '
        '*********************************************************************

        Public ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot

            Get
                Return Me
            End Get

        End Property


        '*********************************************************************
        '
        ' PanelPaneCollection.CopyTo Method
        '
        ' Copies the contents of the collection to an array.
        '
        '*********************************************************************

        Public Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo

            Dim item As Object
            For Each item In _items

                array.SetValue(item, index)
                index += 1

            Next item

        End Sub


        '*********************************************************************
        '
        ' PanelPaneCollection.GetEnumerator Method
        '
        ' Returns an object capable of enumerating the collection.
        '
        '*********************************************************************
        Public Overridable Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator

            Return _items.GetEnumerator()

        End Function

    End Class


    '*********************************************************************
    '
    ' TabbedPanel Class
    '
    ' The TabbedPanel Class is a control that inherits from MultiPanel,
    ' and provides the ability for the user to switch between panels.
    ' The TabbedPanel also has adapters defined for custom rendering.
    '
    '*********************************************************************

    Public Class TabbedPanel
        Inherits MultiPanel
        Implements IPostBackEventHandler

        '*********************************************************************
        '
        ' TabbedPanel.OnRender Method
        '
        ' OnRender is called by the framework to render the control.
        ' The TabbedPanel's OnRender method overrides the behavior
        ' of MultiPanel, and directly calls the adapter to do rendering.
        '
        '*********************************************************************

        Protected Overrides Sub OnRender(ByVal writer As HtmlTextWriter)

            Adapter.Render(writer)

        End Sub

        '*********************************************************************
        '
        ' TabbedPanel.TabColor Property
        '
        ' Gets or sets the background color used for each tab label, when
        ' tabbed rendering is used.
        '
        '*********************************************************************

        Public Property TabColor() As Color

            Get
                ' Get the color from the ViewState property bag, defaulting
                ' to an empty color.
                Dim o As Object = ViewState("TabColor")
                If Not (o Is Nothing) Then
                    Return CType(o, Color)
                Else
                    Return Color.Empty
                End If

            End Get

            Set(ByVal Value As Color)

                ' Save the color in the ViewState property bag.
                ViewState("TabColor") = Value

            End Set

        End Property


        '*********************************************************************
        '
        ' TabbedPanel.TabTextColor Property
        '
        ' Gets or sets the text color used for each tab label, when
        ' tabbed rendering is used.
        '
        '*********************************************************************

        Public Property TabTextColor() As Color

            Get
                ' Get the color from the ViewState property bag, defaulting
                ' to an empty color.
                Dim o As Object = ViewState("TabTextColor")
                If Not (o Is Nothing) Then
                    Return CType(o, Color)
                Else
                    Return Color.Empty
                End If

            End Get

            Set(ByVal Value As Color)

                ' Save the color in the ViewState property bag.
                ViewState("TabTextColor") = Value

            End Set

        End Property


        '*********************************************************************
        '
        ' TabbedPanel.ActiveTabColor Property
        '
        ' Gets or sets the background color used for the active tab label, when
        ' tabbed rendering is used.
        '
        '*********************************************************************

        Public Property ActiveTabColor() As Color

            Get
                ' Get the color from the ViewState property bag, defaulting
                ' to an empty color.
                Dim o As Object = ViewState("ActiveTabColor")
                If Not (o Is Nothing) Then
                    Return CType(o, Color)
                Else
                    Return Color.Empty
                End If

            End Get

            Set(ByVal Value As Color)

                ' Save the color in the ViewState property bag.
                ViewState("ActiveTabColor") = Value

            End Set

        End Property


        '*********************************************************************
        '
        ' TabbedPanel.ActiveTabTextColor Property
        '
        ' Gets or sets the text color used for the active tab label, when
        ' tabbed rendering is used.
        '
        '*********************************************************************

        Public Property ActiveTabTextColor() As Color

            Get
                ' Get the color from the ViewState property bag, defaulting
                ' to an empty color.
                Dim o As Object = ViewState("ActiveTabTextColor")
                If Not (o Is Nothing) Then
                    Return CType(o, Color)
                Else
                    Return Color.Empty
                End If

            End Get

            Set(ByVal Value As Color)

                ' Save the color in the ViewState property bag.
                ViewState("ActiveTabTextColor") = Value

            End Set

        End Property


        '*********************************************************************
        '
        ' TabbedPanel.TabsPerRow Property
        '
        ' Gets or sets the number of tabs to be displayed per row, when
        ' tabbed rendering is used.
        '
        '*********************************************************************

        Public Property TabsPerRow() As Integer

            Get
                ' Get the value from the ViewState property bag, defaulting
                ' to 4.
                Dim o As Object = ViewState("TabsPerRow")

                If Not (o Is Nothing) Then
                    Return CInt(o)
                Else
                    Return 4
                End If

            End Get

            Set(ByVal Value As Integer)

                ' Save the value in the ViewState property bag.
                ViewState("TabsPerRow") = Value

            End Set

        End Property


        '*********************************************************************
        '
        ' IPostBackEventHandler.RaisePostBackEvent Property
        '
        ' RaisePostBackEvent is called by the framework when the control
        ' is to receive a postback event. Responds to the event by 
        ' using the event information to switch to another active pane.
        '
        '*********************************************************************

        Public Overridable Sub RaisePostBackEvent(ByVal eventArgument As String) Implements IPostBackEventHandler.RaisePostBackEvent

            Dim e As New EventArgs()

            ' Call Deactivate event handler.
            OnTabDeactivate(e)

            ActivePaneIndex = CInt(eventArgument)

            ' Call Activate event handler.
            OnTabActivate(e)

        End Sub

        '*********************************************************************
        '
        ' Public events.
        '
        '*********************************************************************

        Public Event TabActivate As EventHandler
        Public Event TabDeactivate As EventHandler


        '*********************************************************************
        '
        ' IPostBackEventHandler.OnTabActivate Method
        '
        ' OnTabActivate is called when a child pane is newly activated
        ' as a result of user interaction, and raises the TabActivate event.
        '
        '*********************************************************************

        Protected Overridable Sub OnTabActivate(ByVal e As EventArgs)

            If Not (e Is Nothing) Then

                RaiseEvent TabActivate(Me, e)

            End If

        End Sub


        '*********************************************************************
        '
        ' IPostBackEventHandler.OnTabDeactivate Method
        '
        ' OnTabDeactivate is called when a child pane is deactivated
        ' as a result of user interaction, and raises the TabDeactivate event.
        '
        '*********************************************************************

        Protected Overridable Sub OnTabDeactivate(ByVal e As EventArgs)

            If Not (e Is Nothing) Then

                RaiseEvent TabDeactivate(Me, e)
            End If

        End Sub

    End Class


    '*********************************************************************
    '
    ' ContentsPanel Class
    '
    ' The ContentsPanel Class is a control that inherits from MultiPanel,
    ' and can render child panes in one of two views. In Summary View,
    ' the control renders each of its child panes (which, in turn, would
    ' probably show only summarized views of themselves) In Details View
    ' the control only renders the active pane.
    '
    '*********************************************************************
    Public Class ContentsPanel
        Inherits MultiPanel

        ' Constants for command names that can be used for
        ' event bubbling in custom UI.
        Public Shared DetailsCommand As String = "details"
        Public Shared SummaryCommand As String = "summary"

        '*********************************************************************
        '
        ' ContentsPanel.SummaryView Property
        '
        ' Get or set the view of the panel to either Summary (true) 
        ' or Details (false) view.
        '
        '*********************************************************************

        Public Property SummaryView() As Boolean

            Get
                ' Get the setting from the ViewState property bag, 
                ' defaulting to true.
                Dim o As Object = ViewState("SummaryView")
                If Not (o Is Nothing) Then
                    Return CBool(o)
                Else
                    Return True
                End If

            End Get

            Set(ByVal Value As Boolean)
                ' Save the setting in the ViewState property bag.
                ViewState("SummaryView") = Value

                ' Notify each child pane of the switched mode.
                Dim pane As IContentsPane
                For Each pane In Panes
                    pane.OnSetSummaryMode()
                Next pane

            End Set

        End Property


        '*********************************************************************
        '
        ' ContentsPanel.Render Method
        '
        ' Called by the framework to render the control. The behavior differs
        ' depending on whether Summary or Details view is showing.
        '
        '*********************************************************************

        Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)

            If SummaryView Then
                ' Render all panes in Summary view.
                RenderChildren(writer)

            Else
                ' Render only the active pane in Details view.
                CType(ActivePane, Control).RenderControl(writer)

            End If

        End Sub


        '*********************************************************************
        '
        ' ContentsPanel.OnBubbleEvent Method
        '
        ' Called by the framework when postback events are bubbled up 
        ' from a child control. If the event source uses the special
        ' command names listed above, this method automatically responds
        ' to the event to change modes. This allows the developer to 
        ' provide UI for showing item details by simply placing a 
        ' control with the appropriate command name in a child pane.
        '
        '*********************************************************************

        Protected Overrides Function OnBubbleEvent(ByVal sender As Object, ByVal e As EventArgs) As Boolean

            Dim handled As Boolean = False
            Dim commandArgs As System.Web.UI.WebControls.CommandEventArgs = e

            If Not (commandArgs Is Nothing) And Not (commandArgs.CommandName Is Nothing) Then

                Dim commandName As String = commandArgs.CommandName.ToLower()

                ' Look for recognized command names.
                If commandName = DetailsCommand Then

                    ' To show details, first find the child pane in which the
                    ' event source is located.
                    Dim ctl As Control = CType(sender, Control)
                    Dim pane As IPanelPane = Nothing

                    While Not (ctl Is Nothing) And Not ctl Is Me

                        Try
                            pane = CType(ctl, IPanelPane)
                        Catch
                            ' cast failed
                        End Try

                        If Not (pane Is Nothing) Then

                            ' Make the pane active, and switch into Details view.
                            ActivePane = pane
                            SummaryView = False
                            handled = True
                            Exit While

                        End If

                        ctl = ctl.Parent

                    End While

                Else

                    If commandName = SummaryCommand Then

                        ' Switch into Summary view.
                        SummaryView = True
                        handled = True

                    End If

                End If

            End If

            Return handled

        End Function


        '*********************************************************************
        '
        ' ContentsPanel.ShowDetails Method
        '
        ' The ShowDetails method switches the control into Details view,
        ' and makes the specified child pane active. Child panes can
        ' call this method to activate themselves.
        '
        '*********************************************************************

        Public Sub ShowDetails(ByVal pane As IPanelPane)

            SummaryView = False
            ActivePane = pane

        End Sub

    End Class


    '*********************************************************************
    '
    ' HtmlTabbedPanelAdapter Class
    '
    ' The HtmlTabbedPanelAdapter provides rendering for the TabbedPanel
    ' class on devices that support HTML and JScript.
    '
    '*********************************************************************

    Public Class HtmlTabbedPanelAdapter
        Inherits HtmlControlAdapter

        '*********************************************************************
        '
        ' HtmlTabbedPanelAdapter.Control Property
        '
        ' Returns the attached control, strongly typed as a TabbedPanel.
        '
        '*********************************************************************

        Protected Shadows ReadOnly Property Control() As TabbedPanel

            Get
                Return CType(MyBase.Control, TabbedPanel)
            End Get

        End Property


        '*********************************************************************
        '
        ' HtmlTabbedPanelAdapter.Render Method
        '
        ' Renders the control. The TabbedPanel is rendered as one or more
        ' rows of tabs that the user can click on to move between tabs.
        '
        '*********************************************************************

        Public Overloads Overrides Sub Render(ByVal writer As HtmlMobileTextWriter)

            Dim _activePane As IPanelPane = Control.ActivePane
            Dim tabsPerRow As Integer = Control.TabsPerRow
            Dim panes As PanelPaneCollection = Control.Panes
            Dim paneCount As Integer = panes.Count

            ' Figure out the number of visible panes.
            Dim visiblePanes(paneCount) As Integer
            Dim visiblePaneCount As Integer = 0
            Dim i As Integer

            For i = 0 To paneCount - 1

                If CType(panes(i), Control).Visible Then

                    visiblePanes(visiblePaneCount) = i
                    visiblePaneCount += 1

                End If

            Next i

            ' Calculate how many rows are necessary.
            Dim rows As Integer = (visiblePaneCount + tabsPerRow - 1) / tabsPerRow

            ' make sure tabsPerRow doesn't exceed the number of visible panes
            If Control.TabsPerRow > visiblePaneCount Then
                tabsPerRow = visiblePaneCount
            Else
                tabsPerRow = Control.TabsPerRow
            End If

            ' Open the table.
            writer.WriteBeginTag("table")
            writer.WriteAttribute("cellspacing", "0")
            writer.WriteAttribute("cellpadding", "2")
            writer.WriteAttribute("border", "0")
            writer.WriteLine(">")

            Dim row As Integer

            For row = rows - 1 To 0 Step -1

                writer.WriteFullBeginTag("tr")
                writer.WriteLine()

                Dim col As Integer

                For col = 0 To tabsPerRow - 1
                    writer.WriteBeginTag("td")
                    writer.WriteAttribute("width", "0")
                    writer.Write(">")
                    writer.WriteEndTag("td")

                    i = row * tabsPerRow + col
                    If row > 0 And i >= visiblePaneCount Then

                        writer.WriteFullBeginTag("td")
                        writer.WriteEndTag("td")
                        GoTo ContinueNextCol

                    End If

                    Dim index As Integer = visiblePanes(i)
                    Dim child As IPanelPane = panes(index)

                    If child Is _activePane Then

                        writer.WriteBeginTag("td")
                        writer.WriteAttribute("bgcolor", GetColorString(Control.ActiveTabColor, "#333333"))
                        writer.Write(">")

                        writer.WriteBeginTag("font")
                        writer.WriteAttribute("face", "Verdana")
                        writer.WriteAttribute("size", "-2")
                        writer.WriteAttribute("color", GetColorString(Control.ActiveTabTextColor, "#000000"))
                        writer.Write(">")

                        writer.WriteFullBeginTag("b")
                        writer.Write("&nbsp;")
                        writer.WriteText(child.Title, True)
                        writer.Write("&nbsp;")
                        writer.WriteEndTag("b")

                        writer.WriteEndTag("font")

                        writer.WriteEndTag("td")
                        writer.WriteLine()
                    Else
                        writer.WriteBeginTag("td")
                        writer.WriteAttribute("bgcolor", GetColorString(Control.TabColor, "#cccccc"))
                        writer.Write(">")

                        writer.WriteBeginTag("font")
                        writer.WriteAttribute("face", "Verdana")
                        writer.WriteAttribute("size", "-2")
                        writer.WriteAttribute("color", GetColorString(Control.TabTextColor, "#000000"))
                        writer.Write(">")

                        writer.Write("&nbsp;")
                        writer.WriteBeginTag("a")
                        RenderPostBackEventAsAttribute(writer, "href", index.ToString())
                        writer.Write(">")
                        writer.WriteText(child.Title, True)
                        writer.WriteEndTag("a")
                        writer.Write("&nbsp;")

                        writer.WriteEndTag("font")

                        writer.WriteEndTag("td")
                        writer.WriteLine()
                    End If
ContinueNextCol:
                Next col
                writer.WriteEndTag("tr")
                writer.WriteLine()

                If row > 0 Then
                    writer.WriteFullBeginTag("tr")
                    writer.WriteBeginTag("td")
                    writer.WriteAttribute("height", "1")
                    writer.Write(">")
                    writer.WriteEndTag("td")
                    writer.WriteEndTag("tr")
                    writer.WriteLine()
                End If
            Next row

            writer.WriteEndTag("table")
            writer.WriteLine()

            writer.WriteBeginTag("table")
            writer.WriteAttribute("width", "100%")
            writer.WriteAttribute("height", "2")
            writer.WriteAttribute("border", "0")
            writer.WriteAttribute("cellspacing", "0")
            writer.WriteAttribute("bgcolor", "#000000")
            writer.Write(">")
            writer.WriteFullBeginTag("tr")
            writer.WriteFullBeginTag("td")
            writer.WriteEndTag("td")
            writer.WriteEndTag("tr")
            writer.WriteEndTag("table")
            writer.WriteBreak()

            CType(_activePane, Control).RenderControl(writer)

        End Sub


        Private Shared Function GetColorString(ByVal _color As Color, ByVal defaultColor As String) As String

            If Not Color.Equals(_color, Color.Empty) Then
                Return ColorTranslator.ToHtml(_color)
            Else
                Return defaultColor
            End If

        End Function

    End Class


    Public Class WmlTabbedPanelAdapter
        Inherits WmlControlAdapter

        Private _menu As List

        Protected Shadows ReadOnly Property Control() As TabbedPanel

            Get
                Return CType(MyBase.Control, TabbedPanel)
            End Get

        End Property


        Public Overrides Sub OnInit(ByVal e As EventArgs)

            _menu = New List()
            AddHandler _menu.ItemCommand, AddressOf OnListItemCommand
            Control.Controls.AddAt(0, _menu)

        End Sub


        Public Overrides Sub OnLoad(ByVal e As EventArgs)

            _menu.Items.Clear()
            Dim index As Integer = 0
            Dim child As IPanelPane

            For Each child In Control.Panes

                If CType(child, Control).Visible Then
                    _menu.Items.Add(New MobileListItem(child.Title, index.ToString()))
                End If
                index += 1

            Next child

        End Sub


        Public Overloads Overrides Sub Render(ByVal writer As WmlMobileTextWriter)

            Dim st As Style = New Style()
            st.Wrapping = CType(Style(Style.WrappingKey, True), Wrapping)
            st.Alignment = CType(Style(Style.AlignmentKey, True), Alignment)
            writer.EnterLayout(st)

            If _menu.Visible Then
                _menu.RenderControl(writer)
            Else
                CType(Control.ActivePane, Control).RenderControl(writer)
            End If

            writer.ExitLayout(st)

        End Sub


        Private Sub OnListItemCommand(ByVal sender As Object, ByVal e As ListCommandEventArgs)
            _menu.Visible = False
            Control.RaisePostBackEvent(e.ListItem.Value)

        End Sub

    End Class


    Public Class ChtmlTabbedPanelAdapter
        Inherits HtmlControlAdapter

        Protected Shadows ReadOnly Property Control() As TabbedPanel

            Get
                Return CType(MyBase.Control, TabbedPanel)
            End Get

        End Property


        Public Overloads Overrides Sub Render(ByVal writer As HtmlMobileTextWriter)

            writer.EnterStyle(Style)

            Dim _activePane As IPanelPane = Control.ActivePane
            writer.Write("[ ")
            Dim index As Integer = 0
            Dim child As IPanelPane

            For Each child In Control.Controls

                If Not CType(child, Control).Visible Then

                    index += 1
                    GoTo ContinueNextChild

                End If

                If index > 0 Then
                    writer.Write(" | ")
                End If

                If child Is _activePane Then

                    writer.Write("<b>")
                    writer.WriteText(child.Title, True)
                    writer.Write("</b>")

                Else

                    writer.WriteBeginTag("a")
                    RenderPostBackEventAsAttribute(writer, "href", index.ToString())
                    writer.Write(">")
                    writer.WriteText(child.Title, True)
                    writer.WriteEndTag("a")

                End If

                index += 1

ContinueNextChild:
            Next child
            writer.Write(" ]")
            writer.WriteBreak()
            CType(_activePane, Control).RenderControl(writer)

            writer.ExitStyle(Style)

        End Sub

    End Class

End Namespace
