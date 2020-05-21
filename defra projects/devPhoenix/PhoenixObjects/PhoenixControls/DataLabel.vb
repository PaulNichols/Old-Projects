Imports System.ComponentModel

Namespace Controls.Data
    <ToolboxBitmap(GetType(Web.UI.WebControls.Label)), _
    DefaultProperty("Text"), _
    System.Web.UI.ToolboxDataAttribute("<{0}:DataLabel runat='server' CssClass='smalltext' Width='232px'></{0}:DataLabel>")> _
    Public Class DataLabel
        Inherits System.Web.UI.WebControls.Label
        Implements IDataControl, IFormatable

#Region "IwwWebDataControl Property Implementation"


        <Category("Data"), _
        Description("The object that the control is bound to."), _
        DefaultValue("")> _
        Public Property BindingSourceObject() As String Implements IDataControl.BindingSourceObject
            Get
                Return mBindingSourceObject
            End Get
            Set(ByVal Value As String)
                mBindingSourceObject = Value
            End Set
        End Property
        Private mBindingSourceObject As String

        <Category("Data"), _
        Description("The property of the object that the control is bound to."), _
        DefaultValue("")> _
        Public Property BindingSourceProperty() As String Implements IDataControl.BindingSourceProperty
            Get
                Return mBindingSourceProperty
            End Get
            Set(ByVal Value As String)
                mBindingSourceProperty = Value
            End Set
        End Property
        Private mBindingSourceProperty As String

        ' The property that is set by the databinding.
        <Category("Data"), _
        DefaultValue("Text")> _
        Public Property BindingProperty() As String Implements IDataControl.BindingProperty
            Get
                Return mBindingProperty
            End Get
            Set(ByVal Value As String)
                mBindingProperty = Value
            End Set
        End Property
        Private mBindingProperty As String = "Text"

        'Error message set when a data unbinding error occurs.
        'Optional - Default message: 'Invalid Data Format for 'control name'
        <Category("Data"), _
        DefaultValue("")> _
              Public Property BindingErrorMessage() As String Implements IDataControl.BindingErrorMessage
            Get
                Return mBindingErrorMessage
            End Get
            Set(ByVal Value As String)
                mBindingErrorMessage = Value
            End Set
        End Property
        Private mBindingErrorMessage As String

        <Category("Data"), _
        Description("The format string used to format this field when binding. Defaults to fieldname with txt stripped."), _
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

#End Region

#Region "IwwWebDataControl Methods Implementation"

        'Binds the control to the control BindingSource.
        Public Sub BindData(ByVal WebForm As Web.UI.Control) Implements IDataControl.BindData
            DataHelper.ControlBindData(WebForm, Me)
        End Sub

        ' <summary>
        ' Unbinds the control from the BindingSource by loading the BindingSource with the value in the control.
        ' </summary>
        '<param name="WebForm">The WebPage that contains the control source</param>
        Public Sub UnbindData(ByVal WebForm As Web.UI.Control) Implements IDataControl.UnbindData
            DataHelper.ControlUnbindData(WebForm, Me)
        End Sub

#End Region

        ' Override this method to provide error information
        'Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        '    MyBase.Render(writer)

        '    If Not BindingErrorMessage Is Nothing AndAlso BindingErrorMessage <> "" Then
        '        ' writer.Write(" <img src='images/warning.gif' alt='" + BindingErrorMessage + "')'>")
        '    End If
        'End Sub


        Public Shadows Property Text() As String Implements IDataControl.Text
            Get
                Return MyBase.Text
            End Get
            Set(ByVal Value As String)
                MyBase.Text = Value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
            EnableViewState = True
        End Sub

        <Category("Data"), DefaultValue("")> _
        Public Property Format() As String Implements IFormatable.Format
            Get
                Return mFormat
            End Get
            Set(ByVal Value As String)
                mFormat = Value
            End Set
        End Property
        Private mFormat As String = Nothing
    End Class
End Namespace
