
Imports System.ComponentModel

Namespace Controls.Data

    <ToolboxBitmap(GetType(System.Web.UI.WebControls.DropDownList)), _
    DefaultProperty("SelectedValue"), _
    Description("Customized drop downlist whose Selected value property can be bound to a data member in addition to binding the list it displays."), _
    ToolboxData("<{0}:DataDropDownList runat='server' CssClass='smalltext' Width='232px'></{0}:DataDropDownList>")> _
    Public Class DataDropDownList
        Inherits System.Web.UI.WebControls.DropDownList
        Implements IDataControl, IChangableDataControl, IReadOnlyControl

        Public Sub ClearChanged() Implements IChangableDataControl.ClearChanged
            mChanged = False
        End Sub

        Public Sub BindData(ByVal WebForm As System.Web.UI.Control) Implements IDataControl.BindData
            CType(WebForm, IManagedControl).Manager.ManagerViewState.Add("ControlBeingBound", Parent.ID)
            DataHelper.ControlBindData(WebForm, Me)
            AddHandler Me.SelectedIndexChanged, AddressOf ValueChanged
        End Sub

        Public Sub ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IChangableDataControl.ValueChanged
            mChanged = True
        End Sub

        Public Property Changed() As Boolean Implements IChangableDataControl.Changed
            Get
                Return mChanged
            End Get
            Set(ByVal Value As Boolean)
                mChanged = Value
            End Set
        End Property
        Private mChanged As Boolean

        'Error message set when a data unbinding error occurs.
        'Optional - Default message: 'Invalid Data Format for 'control name'
        <Category("Data"), DefaultValue("")> _
       Public Property BindingErrorMessage() As String Implements IDataControl.BindingErrorMessage
            Get
                Return mBindingErrorMessage
            End Get
            Set(ByVal Value As String)
                mBindingErrorMessage = Value
            End Set
        End Property
        Private mBindingErrorMessage As String

        'The property that is to be bound to
        <Category("Data"), DefaultValue("SelectedValue")> _
        Public Property BindingProperty() As String Implements IDataControl.BindingProperty
            Get
                Return mBindingProperty
            End Get
            Set(ByVal Value As String)
                mBindingProperty = Value
            End Set
        End Property
        Private mBindingProperty As String

        <Category("Data"), DefaultValue("")> _
       Public Property BindingSourceObject() As String Implements IDataControl.BindingSourceObject
            Get
                Return mBindingSourceObject
            End Get
            Set(ByVal Value As String)
                mBindingSourceObject = Value
            End Set
        End Property
        Private mBindingSourceObject As String

        <Category("Data"), DefaultValue("")> _
        Public Property BindingSourceProperty() As String Implements IDataControl.BindingSourceProperty
            Get
                Return mBindingSourceProperty
            End Get
            Set(ByVal Value As String)
                mBindingSourceProperty = Value
            End Set
        End Property
        Private mBindingSourceProperty As String

        Public Property Text() As String Implements IDataControl.Text
            Get
                Return MyBase.SelectedValue
            End Get
            Set(ByVal Value As String)
                MyBase.SelectedValue = Value
            End Set
        End Property

        'Unbinds the control from the BindingSource by loading the BindingSource with the value in the control.
        Public Sub UnbindData(ByVal WebForm As System.Web.UI.Control) Implements IDataControl.UnbindData
            CType(WebForm, IManagedControl).Manager.ManagerViewState.Add("ControlBeingBound", Parent.ID)
            DataHelper.ControlUnbindData(WebForm, Me)
            RemoveHandler Me.SelectedIndexChanged, AddressOf ValueChanged
        End Sub

        <Category("Data"), _
            Description("The format string used to format this field when binding. Defaults to fieldname with txtStripped."), _
            DefaultValue("")> _
        Public Property UserFieldName() As String Implements IDataControl.UserFieldName
            Get
                If mUserFieldName = String.Empty Then
                    mUserFieldName = ID.Substring(3)
                End If
                Return mUserFieldName
            End Get
            Set(ByVal Value As String)
                mUserFieldName = Value
            End Set
        End Property
        Private mUserFieldName As String

        Public Overrides Property SelectedValue() As String
            Get
                Return Page.Request.Form(UniqueID)
            End Get
            Set(ByVal Value As String)
                MyBase.SelectedValue = Value
            End Set
        End Property

        ' Override this method to provide error information
        Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
            MyBase.Render(writer)

            If Not BindingErrorMessage Is Nothing AndAlso BindingErrorMessage <> "" Then
                '  writer.Write(" <img src='images/warning.gif' alt='" + BindingErrorMessage + "')'>")
            End If
        End Sub

        Public WriteOnly Property [ReadOnly]() As Boolean Implements IReadOnlyControl.ReadOnly
            Set(ByVal Value As Boolean)
                viewstate.Add("Readonly", Value)
            End Set
        End Property
        Private ReadOnly Property GetReadOnly() As Boolean
            Get
                Return CType(viewstate("Readonly"), Boolean)
            End Get
        End Property

        Public Property UseCustomDataSource() As Boolean
            Get
                If Not viewstate("UseCustomDataSource") Is Nothing Then
                    Return CType(viewstate("UseCustomDataSource"), Boolean)
                Else
                    Return False
                End If
            End Get
            Set(ByVal Value As Boolean)
                viewstate.Add("UseCustomDataSource", Value)
            End Set
        End Property

        Public Overrides Property DataSource() As Object
            Get
                If Not viewstate("DataSource") Is Nothing Then
                    Return viewstate("DataSource")
                Else
                    Return MyBase.DataSource
                End If
            End Get
            Set(ByVal Value As Object)
                Dim UseViewState As Boolean = True
                If UseCustomDataSource Then
                    Try
                        viewstate.Add("DataSource", Value)
                        UseViewState = False
                    Catch ex As System.Runtime.Serialization.SerializationException
                        MyBase.DataSource = Value
                    End Try
                Else
                    MyBase.DataSource = Value
                End If
                ' Me.EnableViewState = UseViewState
            End Set
        End Property

        Public Sub Clear()
            Me.DataSource = Nothing
            Me.DataBind()
            Me.Items.Clear()
        End Sub

        Private Sub DataDropDownList_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
            If UseCustomDataSource Then
                Dim Item As String = Me.SelectedValue
                Me.DataBind()
                'Me.SelectedValue = ""
                Me.SelectedValue = Item
            End If
        End Sub

        Public ReadOnly Property HasSelectedObject() As Boolean
            Get
                Return (UseCustomDataSource AndAlso Me.SelectedIndex > -1 AndAlso Not Me.DataSource Is Nothing AndAlso TypeOf Me.DataSource Is IList)
            End Get
        End Property

        Public ReadOnly Property SelectedObject() As Object
            Get
                If HasSelectedObject Then
                    Return CType(Me.DataSource, IList).Item(Me.SelectedIndex)
                ElseIf UseCustomDataSource Then
                    Throw New System.Exception("Please set the UseCustomDataSource property")
                End If
            End Get
        End Property
        'Public Overrides Property EnableViewState() As Boolean
        '    Get
        '        If Not viewstate("DataSource") Is Nothing Then
        '            Return False
        '        Else
        '            Return MyBase.EnableViewState
        '        End If
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        MyBase.EnableViewState = Value
        '    End Set
        'End Property
    End Class
End Namespace

