Imports System.ComponentModel

Namespace Controls.Data
    <ToolboxBitmap(GetType(System.Web.UI.WebControls.ListBox)), _
        DefaultProperty("SelectedValue"), _
        ToolboxData("<{0}:DataListBox runat='server' Width='232px'></{0}:DataListBox>")> _
   Public Class DataListBox
        Inherits System.Web.UI.WebControls.ListBox
        Implements IDataControl, IChangableDataControl, IReadOnlyControl

        Public Sub ClearChanged() Implements IChangableDataControl.ClearChanged
            mChanged = False
        End Sub

        Public Sub BindData(ByVal WebForm As System.Web.UI.Control) Implements IDataControl.BindData
            DataHelper.ControlBindData(WebForm, Me)
            AddHandler Me.SelectedIndexChanged, AddressOf ValueChanged
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
        <Category("Data"), DefaultValue("")> _
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

        Public Sub ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IChangableDataControl.ValueChanged
            mChanged = True
        End Sub
    End Class
End Namespace
