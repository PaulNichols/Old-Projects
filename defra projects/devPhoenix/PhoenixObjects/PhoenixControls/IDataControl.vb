
Public Interface IDataControl

    ' Summary description for IwwData.

    ' The object that the control is bound to. This is the base object
    ' and you are still required to set the BindingSource property with
    ' the property to bind to
    Property BindingSourceObject() As String

    Property Text() As String

    ' The BindingSource property to bind to set as a string. Uses Reflection to retrieve and set 
    ' content of the object in question
    Property BindingSourceProperty() As String

    'Field or type that this control is bound to
    Property BindingProperty() As String

    ' The fieldname the user sees when an error occurs.
    Property UserFieldName() As String

    ' Error message that is set if unbinding fails.
    Property BindingErrorMessage() As String

    ' Hooks up databinding for the control.
    Sub BindData(ByVal WebForm As Web.UI.Control)

    ' Unbinds data back into its  BindingSource.
    Sub UnbindData(ByVal WebForm As Web.UI.Control)


End Interface

