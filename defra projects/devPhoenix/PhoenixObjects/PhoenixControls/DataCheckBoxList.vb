Imports System.ComponentModel

Namespace Controls.Data
    <ToolboxBitmap(GetType(System.Web.UI.WebControls.CheckBoxList)), _
        DefaultProperty("SelectedValue"), _
        ToolboxData("<{0}:DataCheckBoxList runat='server' Width='232px'></{0}:DataCheckBoxList>")> _
   Public Class DataCheckBoxList
        Inherits System.Web.UI.WebControls.CheckBoxList
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
        <Category("Data"), DefaultValue("Checked")> _
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

        Public Shadows Property SelectedValue() As Object()
            Get
                Dim DS As Object() = CType(DataSource, Object())
                Dim ReturnArray As New ArrayList
                For Each item As WebControls.ListItem In CType(Me, uk.gov.defra.PhoenixControls.Controls.Data.DataCheckBoxList).Items
                    If item.Selected Then

                    End If
                Next
                Return CType(ReturnArray.ToArray(GetType(Object)), Object())
            End Get
            Set(ByVal Value As Object())
                '  MyBase.SelectedValue = Value
            End Set
        End Property

        Public Sub ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IChangableDataControl.ValueChanged
            mChanged = True
        End Sub

        Public Overrides Property DataSource() As Object
            Get
                Return viewstate("DataSource")
            End Get
            Set(ByVal Value As Object)
                viewstate.Add("DataSource", Value)
                MyBase.DataSource = Value
            End Set
        End Property
    End Class
End Namespace
