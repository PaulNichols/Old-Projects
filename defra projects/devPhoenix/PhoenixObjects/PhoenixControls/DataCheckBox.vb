Imports System.ComponentModel


Namespace Controls.Data

    <ToolboxBitmap(GetType(Web.UI.WebControls.CheckBox)), _
        DefaultProperty("Checked"), _
        ToolboxData("<{0}:DataCheckBox runat='server' CssClass='smalltext' size='20'></{0}:DataCheckBox>")> _
   Public Class DataCheckBox
        Inherits System.Web.UI.WebControls.CheckBox
        Implements IDataControl, IChangableDataControl, IReadOnlyControl

        'Unbinds the control from the BindingSource by loading the BindingSource with the value in the control.
        Public Sub UnbindData(ByVal WebForm As System.Web.UI.Control) Implements IDataControl.UnbindData
            DataHelper.ControlUnbindData(WebForm, Me)
            RemoveHandler Me.CheckedChanged, AddressOf ValueChanged
        End Sub

        'Binds the control to the control BindingSource.
        Public Sub BindData(ByVal WebForm As System.Web.UI.Control) Implements IDataControl.BindData
            DataHelper.ControlBindData(WebForm, Me)
            AddHandler Me.CheckedChanged, AddressOf ValueChanged
        End Sub

        Public Property Changed() As Boolean Implements IChangableDataControl.Changed
            Get
                If Not viewstate("Changed") Is Nothing Then
                    Return CType(viewstate("Changed"), Boolean)
                End If
            End Get
            Set(ByVal Value As Boolean)
                viewstate.Add("Changed", Value)
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

        'The property that is set by the databinding.
        <Category("Data"), DefaultValue("Checked")> _
        Public Property BindingProperty() As String Implements IDataControl.BindingProperty
            Get
                If mBindingProperty Is Nothing Then
                    mBindingProperty = "Checked"
                End If
                Return mBindingProperty
            End Get
            Set(ByVal Value As String)
                mBindingProperty = Value
            End Set
        End Property
        Private mBindingProperty As String

        ' The object that is to be bound to
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

        'The property that is to be bound to
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

        Public Shadows Property Text() As String Implements IDataControl.Text
            Get
                Return MyBase.Text
            End Get
            Set(ByVal Value As String)
                MyBase.Text = Value
            End Set
        End Property

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

        'The format string used to format this field when binding. Defaults to fieldname with txtStripped.
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

        'Override this method to provide error information
        Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

            MyBase.Render(writer)

            If BindingErrorMessage Is Nothing AndAlso BindingErrorMessage <> "" Then
                '   writer.Write(" <img src='images/warning.gif' alt='" & BindingErrorMessage & "' onClick='alert(\" & BindingErrorMessage & "\"") '>")
            End If

        End Sub

        Public Sub ClearChanged() Implements IChangableDataControl.ClearChanged
            mChanged = False
        End Sub

        Public Sub ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IChangableDataControl.ValueChanged

        End Sub

        Protected Overrides Sub OnCheckedChanged(ByVal e As System.EventArgs)
            If GetReadOnly Then Checked = Not Checked
            MyBase.OnCheckedChanged(e)
        End Sub
    End Class
End Namespace