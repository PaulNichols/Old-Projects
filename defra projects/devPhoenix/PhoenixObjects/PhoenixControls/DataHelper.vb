Imports System.Reflection
Imports System.Globalization

Public Class DataHelper
    Const FieldLengthPropName As String = "FieldLength"

    ' Binds all databound controls on a WebForm page by calling their BindData methods.
    Public Shared Sub FormBindData(ByVal Container As Web.UI.Control, ByVal WebForm As Web.UI.Control)
        '  *** Drill through each control on the form
        For Each Control As Web.UI.Control In Container.Controls
            ' ** Recursively call down into any containers
            'Try                                                    'MLD 22/12/4 Try/Catch commented out...
                If Control.Controls.Count > 0 Then
                    FormBindData(Control, WebForm)
                End If
            'Catch ex As Exception                                  'MLD 22/12/4 ... as it swallows important errors. If there is a good reason to ignore
            'End Try                                                'a particular error, please add a catch that is specific to that error and/or rethrow other errors


            ' only work on those that support interface
            If Not Control.GetType.GetInterface(GetType(IDataControl).ToString) Is Nothing And Control.Visible Then

                Try
                    '*** Call the BindData method on the control
                    'Control.GetType.GetMethod("BindData", Utils.MemberAccess).Invoke(Control, New Object() {WebForm})
                    CType(Control, IDataControl).BindData(WebForm)
                Catch ex As Exception
                    ' *** Display Error info
                    Try
                        'TODO: This must be adjusted for specific types of controls
                        CType(Control, IDataControl).Text = ex.Message
                    Catch
                    End Try
                End Try
            End If
        Next Control
    End Sub

    Public Shared Function FormUnbindData(ByVal WebForm As Web.UI.Control) As BindingError()
        Dim Errors As BindingError()
        FormUnbindData(WebForm, WebForm, Errors)
        Return Errors
    End Function

    Shared Function FormUnbindData(ByVal Container As Web.UI.Control, ByVal WebForm As Web.UI.Control, ByRef Errors As BindingError()) As BindingError()
        ' *** Drill through each of the controls
        For Each aControl As Web.UI.Control In Container.Controls
            ' ** Recursively call down into containers
            If aControl.Controls.Count > 0 Then
                FormUnbindData(aControl, WebForm, Errors)
            End If
            If Not aControl.GetType.GetInterface(GetType(IDataControl).ToString) Is Nothing And aControl.Visible Then

                Try
                    ' *** Call the UnbindData method on the control
                    CType(aControl, IDataControl).UnbindData(WebForm)
                Catch ex As System.Exception
                    ' *** Display Error info
                    Try
                        Dim BindError As New BindingError
                        If Utils.Empty(CType(aControl, IDataControl).BindingErrorMessage) Then
                            If CType(aControl, IDataControl).UserFieldName <> "" Then
                                BindError.Message = "Invalid format for " & CType(aControl, IDataControl).UserFieldName
                            Else
                                BindError.Message = "Invalid format for " & aControl.ID.Replace("txt", "")
                            End If
                        Else
                            BindError.Message = CType(aControl, IDataControl).BindingErrorMessage
                        End If

                        CType(aControl, IDataControl).BindingErrorMessage = BindError.Message

                        With BindError
                            .ErrorMsg = ex.Message
                            .Source = ex.Source
                            .StackTrace = ex.StackTrace
                            .ObjectName = aControl.ID
                        End With

                        If Errors Is Nothing Then
                            ReDim Errors(0)
                            Errors(0) = BindError
                        Else
                            ' *** Resize the array and assign Error
                            Dim lnSize As Int32 = Errors.GetLength(0)
                            Dim Temp As Array = Array.CreateInstance(GetType(BindingError), lnSize)
                            Errors.CopyTo(Temp, 0)
                            Temp.SetValue(BindError, lnSize - 1)

                            Errors = CType(Temp, BindingError())
                        End If
                    Catch ' ignore additional exceptions
                    End Try
                End Try
            End If

        Next
        Return Errors
    End Function

    ' Formats an Binding Errors collection as a string in HTML format
    Public Shared Function BindingErrorsToHtml(ByVal Errors As BindingError()) As String
        Return BindingErrorsToString(Errors).Replace("\r", "<br>")
    End Function

    ' Formats an Binding Errors collection as a string.
    Public Shared Function BindingErrorsToString(ByVal Errors As BindingError()) As String
        ' *** Optional Error Parsing
        If Not Errors Is Nothing Then
            Dim ErrorsString As String
            For x As Int32 = 0 To Errors.Length - 1
                ErrorsString = ErrorsString + Errors(x).Message + "<br>"
            Next
            If Not Utils.Empty(ErrorsString) Then
                Return ErrorsString
            End If
        End If
        Return ""
    End Function


    Public Shared Sub ControlBindData(ByVal WebPage As Web.UI.Control, ByVal ActiveControl As PhoenixControls.IDataControl)
        Dim BindingSourceObject As String = ActiveControl.BindingSourceObject
        Dim BindingSourceProperty As String = ActiveControl.BindingSourceProperty
        Dim BindingProperty As String = ActiveControl.BindingProperty

        Try
            If BindingSourceObject Is Nothing OrElse BindingSourceObject.Length = 0 OrElse _
                    BindingSourceProperty Is Nothing OrElse BindingSourceProperty.Length = 0 Then

                Return
            End If

            ' *** Get a reference to the actual control source object
            Dim BindingSource As Object

            ' *** Allow this or me to be bound to the page
            If BindingSourceObject = "this" OrElse BindingSourceObject.ToLower = "me" Then
                BindingSource = WebPage
            Else
                BindingSource = Utils.GetPropertyEx(WebPage, BindingSourceObject)
            End If

            If BindingSource Is Nothing Then Return


            ' *** Retrieve the control source value
            Dim Value As Object

            If BindingSource.GetType.IsSubclassOf(GetType(System.Data.DataSet)) OrElse BindingSource.GetType.Equals(GetType(System.Data.DataSet)) Then
                Dim Table As String = BindingSourceProperty.Substring(0, BindingSourceProperty.IndexOf("."))
                Dim Column As String = BindingSourceProperty.Substring(BindingSourceProperty.IndexOf(".") + 1)
                Dim Ds As DataSet = CType(BindingSource, DataSet)
                Value = Ds.Tables(Table).Rows(0)(Column)
            ElseIf BindingSource.GetType.IsSubclassOf(GetType(System.Data.DataRow)) OrElse BindingSource.GetType.Equals(GetType(System.Data.DataRow)) Then
                Dim Dr As DataRow = CType(BindingSource, DataRow)
                Value = Dr(BindingSourceProperty)
            ElseIf BindingSource.GetType.IsSubclassOf(GetType(System.Data.DataTable)) OrElse BindingSource.GetType.Equals(GetType(System.Data.DataTable)) Then
                Dim Dt As DataTable = CType(BindingSource, DataTable)
                Value = Dt.Rows(0)(BindingSourceProperty)
            ElseIf BindingSource.GetType.IsSubclassOf(GetType(System.Data.DataView)) OrElse BindingSource.GetType.Equals(GetType(System.Data.DataTable)) Then
                Dim Dv As DataView = CType(BindingSource, DataView)
                Value = Dv.Table.Rows(0)(BindingSourceProperty)
            Else
                Value = Utils.GetPropertyEx(BindingSource, BindingSourceProperty)
                'If TypeOf Value Is Date Then
                '    With CType(Value, Date)
                '        If .Hour = 0 AndAlso .Minute = 0 AndAlso .Second = 0 Then
                '            Value = .ToShortDateString
                '        End If
                '    End With
                'End If
            End If


            ' *** Figure out the type of the control we're binding to
            Dim BindValue As PropertyInfo = CType(ActiveControl, System.Web.UI.Control).GetType.GetProperty(BindingProperty, Utils.MemberAccess)
            Dim BindingSourceType As Type = BindValue.PropertyType

            If Value Is Nothing Then
                Try
                    Utils.SetProperty(ActiveControl, BindingProperty, GetDefault(BindingSourceType))
                Catch
                End Try
            Else
                'TODO: Fix Formatting here					
                If BindingSourceType.Equals(GetType(Boolean)) Then
                    Utils.SetProperty(ActiveControl, BindingProperty, Value)
                Else
                    If BindingSourceType.Equals(GetType(String)) Then
                        If TypeOf Value Is Boolean Then Value = CType(Value, Int32) * -1
                        Value = DecodeHTML(Value.ToString)
                    End If
                End If

                Dim ActiveFormatControl As IFormatable
                If Not CType(ActiveControl, Object).GetType.GetInterface(GetType(IFormatable).ToString) Is Nothing Then
                    ActiveFormatControl = CType(ActiveControl, IFormatable)
                End If

                If ActiveFormatControl Is Nothing OrElse _
                Utils.Empty(CType(ActiveControl, IFormatable).Format) Then
                    Utils.SetProperty(ActiveControl, BindingProperty, Value)
                Else
                    Utils.SetProperty(ActiveFormatControl, BindingProperty, GetFormattedText(ActiveFormatControl.Format, BindingSourceType, Value))
                    'Utils.SetProperty(ActiveFormatControl, BindingProperty, String.Format(ActiveControl.DisplayFormat, Value))
                End If
            End If
            Try
                Dim ChangeableControl As Web.UI.WebControls.WebControl = Nothing
                ChangeableControl = CType(ActiveControl, Web.UI.WebControls.WebControl)
                If Not ChangeableControl.GetType.GetInterface(GetType(IChangableDataControl).ToString) Is Nothing Then
                    CType(ChangeableControl, IChangableDataControl).ClearChanged()
                End If
            Catch ex As Exception
            End Try



            ' Dim ControlType As Type = CType(ActiveControl, System.Web.UI.WebControls.WebControl).getType()
            If TypeOf ActiveControl Is Controls.Data.DataTextBox Then
                SetMaxLength(CType(ActiveControl, Controls.Data.DataTextBox), BindingSource, BindingSourceProperty)
            End If
            '    CType(ActiveControl, System.Web.UI.WebControls.TextBox).AutoPostBack = True
            'ElseIf ControlType.Equals(GetType(System.Web.UI.WebControls.CheckBox)) OrElse ControlType.IsSubclassOf(GetType(System.Web.UI.WebControls.CheckBox)) Then
            '    CType(ActiveControl, System.Web.UI.WebControls.CheckBox).AutoPostBack = True
            'ElseIf ControlType.Equals(GetType(System.Web.UI.WebControls.ListControl)) OrElse ControlType.IsSubclassOf(GetType(System.Web.UI.WebControls.ListControl)) Then
            '    CType(ActiveControl, System.Web.UI.WebControls.ListControl).AutoPostBack = True
            'End If


        Catch ex As System.Exception    'MLD 6/4/5 modified as it was swallowing errors
            Throw New Exception("Can't bind " & CType(ActiveControl, Web.ui.Control).ID & " to " & _
                BindingSourceObject & "." & _
                BindingSourceProperty)
        End Try
    End Sub

    Private Shared Sub SetMaxLength(ByVal textBox As Controls.Data.DataTextBox, ByVal bindingSource As Object, ByVal bindingSourceProperty As String)
        Dim SourceProperty As PropertyInfo = bindingSource.getType.GetProperty(bindingSourceProperty)
        If Not SourceProperty Is Nothing Then
            Dim Attributes As Object() = SourceProperty.GetCustomAttributes(True)
            'get the current properties attribute collection
            'as there needs to be a definaition of the DatabaseInformation attribute class in each proxy generated file
            'it makes it hard to get the actual attribute by name, so we just iterate through the collection until
            'we find the field length property as defined in a constant

            If Not Attributes Is Nothing AndAlso Attributes.Length > 0 Then
                For Each Attribute As System.Attribute In Attributes
                    Dim FieldLengthProp As PropertyInfo = Attribute.getType.GetProperty(FieldLengthPropName)
                    If Not FieldLengthProp Is Nothing Then
                        'if we find the field length property then get it's value
                        Dim FieldLength As Object = FieldLengthProp.GetValue(Attribute, Nothing)
                        If Not FieldLength Is Nothing Then
                            Try
                                Int32.Parse(FieldLength.ToString)
                                'if there is an integer value then set the max length to it
                                textBox.MaxLength = CType(FieldLength, Int32)
                            Catch ex As FormatException

                            End Try
                        End If
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Shared Function GetFormattedText(ByVal format As String, ByVal bindingType As Type, ByVal value As Object) As String
        If Not Utils.Empty(format) AndAlso _
           Not value Is Nothing AndAlso _
           bindingType.Equals(GetType(String)) Then
            Try
                Select Case format
                    Case "Boolean"
                        If Boolean.Parse(value.ToString) Then
                            Return "Yes"
                        Else
                            Return "No"
                        End If
                    Case "ShortDate"
                        'ensure that is it a valid date
                        Return Date.Parse(value.ToString).ToShortDateString()
                    Case "LongDate"
                        'ensure that is it a valid date
                        Return Date.Parse(value.ToString).ToLongDateString
                    Case "ShortTime"
                        'ensure that is it a valid date
                        Return Date.Parse(value.ToString).ToShortTimeString
                    Case "LongTime"
                        'ensure that is it a valid date
                        Return Date.Parse(value.ToString).ToLongTimeString
                    Case Else
                        'do nothing
                End Select
            Catch ex As Exception
                Return Nothing
            End Try
        End If
        Try
            Return GetDefault(bindingType).ToString
        Catch
            Return Nothing
        End Try
    End Function

    Private Shared Function GetDefault(ByVal propertyType As Type) As Object
        If propertyType.Equals(GetType(Boolean)) Then
            Return False
        ElseIf propertyType.Equals(GetType(String)) Then
            Return String.Empty
        Else
            Return String.Empty
        End If
    End Function
    '       ' Unbinds control properties back into the control source
    Public Shared Sub ControlUnbindData(ByVal WebPage As Web.UI.Control, ByVal ActiveControl As IDataControl)
        Dim BindingSourceObject As String = ActiveControl.BindingSourceObject
        Dim BindingSourceProperty As String = ActiveControl.BindingSourceProperty
        Dim BindingProperty As String = ActiveControl.BindingProperty

        If BindingSourceObject Is Nothing OrElse BindingSourceObject.Length = 0 OrElse _
            BindingSourceProperty Is Nothing OrElse BindingSourceProperty.Length = 0 Then

            Return
        End If

        Dim BindingSource As Object
        If BindingSourceObject = "this" OrElse BindingSourceObject.ToLower = "me" Then
            BindingSource = WebPage
        Else
            BindingSource = Utils.GetPropertyEx(WebPage, BindingSourceObject)
        End If

        If BindingSource Is Nothing Then
            Throw (New Exception("Invalid BindingSource"))
        End If

        'Retrieve the new value from the control
        Dim Value As Object = Utils.GetPropertyEx(ActiveControl, BindingProperty)

        ' Try to retrieve the type of the BindingSourceProperty
        Dim BindingSourceType As String
        Dim DataColumn As String
        Dim DataTable As String

        If BindingSource.getType.Equals(GetType(System.Data.DataSet)) Then
            ' *** Split out the datatable and column names
            Dim lnAt As Int32 = BindingSourceProperty.IndexOf(".")
            DataTable = BindingSourceProperty.Substring(0, lnAt)
            DataColumn = BindingSourceProperty.Substring(lnAt + 1)
            Dim Ds As DataSet = CType(BindingSource, DataSet)
            BindingSourceType = Ds.Tables(DataTable).Columns(DataColumn).DataType.Name
        ElseIf BindingSource.getType.Equals(GetType(System.Data.DataRow)) Then
            Dim Dr As DataRow = CType(BindingSource, DataRow)
            BindingSourceType = Dr.Table.Columns(BindingSourceProperty).DataType.Name
        ElseIf BindingSource.getType.Equals(GetType(System.Data.DataTable)) Then
            Dim dt As DataTable = CType(BindingSource, DataTable)
            BindingSourceType = dt.Columns(BindingSourceProperty).DataType.Name
        Else
            ' *** It's an object property or field - get it
            If BindingSource.GetType.BaseType.Equals(GetType(System.Enum)) Then
                BindingSourceType = GetType(System.Enum).Name
            Else
                Dim Info As MemberInfo() = BindingSource.GetType.GetMember(BindingSourceProperty, Utils.MemberAccess)
                If Info(0).MemberType = MemberTypes.Field Then
                    Dim Field As FieldInfo = CType(Info(0), FieldInfo)
                    BindingSourceType = Field.FieldType.Name
                    Try

                        If Field.FieldType.basetype.name = "Enum" Then
                            BindingSourceType = GetType(System.Enum).Name
                        End If
                    Catch
                    End Try
                Else
                    Dim Field As PropertyInfo = CType(Info(0), PropertyInfo)
                    BindingSourceType = Field.PropertyType.Name
                    Try
                        If Field.PropertyType.basetype.name = "Enum" Then
                            BindingSourceType = GetType(System.Enum).Name
                        End If
                    Catch
                    End Try

                End If
            End If

        End If

        ' Retrieve the value
        Dim AssignedValue As Object

        If BindingSourceType = "String" Then
            'this should perhaps pop nothing into assignedvalue if value is an empty string?
            AssignedValue = EncodeHTML(Value.ToString)
        ElseIf BindingSourceType = "Int16" Then
            AssignedValue = Int16.Parse(Value.ToString, NumberStyles.Integer)   ' Convert.ToInt16(loValue);				
        ElseIf BindingSourceType = "Int32" Then
            If Not Value Is Nothing Then AssignedValue = Int32.Parse(Value.ToString, NumberStyles.Integer) ' Convert.ToInt32(loValue);				
        ElseIf BindingSourceType = "Int64" Then
            AssignedValue = Int32.Parse(Value.ToString, NumberStyles.Integer)  '				
        ElseIf (BindingSourceType = "Byte") Then
            AssignedValue = Convert.ToByte(Value)
        ElseIf (BindingSourceType = "Decimal") Then
            AssignedValue = Decimal.Parse(Value.ToString, NumberStyles.Any)
        ElseIf (BindingSourceType = "Double") Then
            AssignedValue = Double.Parse(Value.ToString, NumberStyles.Any)
        ElseIf (BindingSourceType = "Boolean") Then
            If TypeOf ActiveControl Is System.Web.UI.WebControls.RadioButtonList Then
                AssignedValue = CType(Value, Boolean)
            Else
                AssignedValue = Value
            End If
        ElseIf BindingSourceType = "DateTime" Then
            AssignedValue = Convert.ToDateTime(Value)
        ElseIf BindingSourceType = "Object" Then
            AssignedValue = Value
        ElseIf BindingSourceType = "Enum" Then
            AssignedValue = Value
        Else  ' Not HANDLED!!!
            Throw New Exception("Field Type not Handled by Data unbinding")
        End If

        Dim HasChanged As Boolean = False
        Try
            Dim ChangeableControl As Web.UI.WebControls.WebControl = Nothing
            ChangeableControl = CType(ActiveControl, Web.UI.WebControls.WebControl)
            If Not ChangeableControl.GetType.GetInterface(GetType(IChangableDataControl).ToString) Is Nothing Then
                HasChanged = CType(ChangeableControl, IChangableDataControl).Changed
            End If
        Catch ex As Exception
        End Try
        'If HasChanged AndAlso Not AssignedValue Is Nothing Then
        ' Write the value back to the underlying object/data item
        If Not AssignedValue Is Nothing Then


            If BindingSource.GetType.Equals(GetType(System.Data.DataSet)) Then
                CType(BindingSource, DataSet).Tables(DataTable).Rows(0)(DataColumn) = AssignedValue
            ElseIf BindingSource.GetType.Equals(GetType(System.Data.DataRow)) Then
                CType(BindingSource, DataRow)(BindingSourceProperty) = AssignedValue
            ElseIf BindingSource.GetType.Equals(GetType(System.Data.DataTable)) Then
                CType(BindingSource, DataTable).Rows(0)(BindingSourceProperty) = AssignedValue
            ElseIf BindingSource.GetType.Equals(GetType(System.Data.DataView)) Then
                CType(BindingSource, DataView)(0)(BindingSourceProperty) = AssignedValue
            ElseIf BindingSource.GetType.BaseType.Equals(GetType(System.Enum)) Then
                'Stop
            Else
                Utils.SetPropertyEx(BindingSource, BindingSourceProperty, AssignedValue)
            End If
        End If
        'End If
        ' *** Clear the error message - no error
        ActiveControl.BindingErrorMessage = ""
    End Sub

    Public Shared Function DecodeHTML(ByVal value As String) As String
        Return System.Web.HttpUtility.HtmlDecode(value)
    End Function

    Public Shared Function EncodeHTML(ByVal value As String) As String
        Return System.Web.HttpUtility.HtmlEncode(value)
    End Function
End Class


'/// <summary>
'/// Error object used to return error information during databinding.
'/// </summary>
Public Class BindingError
    ' Name of the object the error occurred on
    Public Property ObjectName() As String
        Get
            Return mObjectName
        End Get
        Set(ByVal Value As String)
            mObjectName = Value
        End Set
    End Property
    Private mObjectName As String


    '	/// The error message raised
    Public Property Message() As String
        Get
            Return mMessage
        End Get
        Set(ByVal Value As String)
            mMessage = Value
        End Set
    End Property
    Private mMessage As String

    ' The raw Exception error message
    Public Property ErrorMsg() As String
        Get
            Return mErrorMsg
        End Get
        Set(ByVal Value As String)
            mErrorMsg = Value
        End Set
    End Property
    Private mErrorMsg As String

    ' The code that caused the error
    Public Property Source() As String
        Get
            Return mSource
        End Get
        Set(ByVal Value As String)
            mSource = Value
        End Set
    End Property
    Private mSource As String

    'The call stack that led up to the error

    Public Property StackTrace() As String
        Get
            Return mStackTrace
        End Get
        Set(ByVal Value As String)
            mStackTrace = Value
        End Set
    End Property
    Private mStackTrace As String
End Class

Public Class Utils

    ' Binding Flags constant to be reused for all Reflection access methods.
    Public Const MemberAccess As BindingFlags = _
        BindingFlags.Public Or BindingFlags.NonPublic Or _
        BindingFlags.Static Or BindingFlags.Instance Or BindingFlags.IgnoreCase

    ' Determines whether a string is empty (null or zero length)
    Public Shared Function Empty(ByVal stringToCheck As String) As Boolean
        If stringToCheck Is Nothing OrElse stringToCheck.Trim.Length = 0 Then
            Return True
        End If
        Return False
    End Function

    '' Replaces and  and Quote characters to HTML safe equivalents.
    'Public Shared Function FixHTMLForDisplay(ByVal html As String) As String
    '    html = html.Replace("<", "&lt;")
    '    html = html.Replace(">", "&gt;")
    '      html = html.Replace("\"","&quote;")
    '    Return html
    'End Function

    ' Retrieve a dynamic 'non-typelib' property
    Public Shared Function GetProperty(ByVal aObject As Object, ByVal aProperty As String) As Object
        Return aObject.GetType.GetProperty(aProperty, MemberAccess).GetValue(aObject, Nothing)
    End Function

    ' Retrieve a dynamic 'non-typelib' field
    Public Shared Function GetField(ByVal aObject As Object, ByVal aProperty As String) As Object
        Return aObject.GetType.GetField(aProperty, MemberAccess).GetValue(aObject)
    End Function

    ' Returns a property or field value using a base object and sub members including . syntax.
    ' For example, you can access: this.oCustomer.oData.Company with (this,"oCustomer.oData.Company")
    Public Shared Function GetPropertyEx(ByVal Parent As Object, ByVal aProperty As String) As Object
        Dim Member As MemberInfo

        Dim aType As Type = Parent.GetType()

        Dim lnAt As Int32 = aProperty.IndexOf(".")
        If lnAt < 0 Then
            If aProperty.ToLower = "me" Then
                Return Parent
            End If

            ' *** Get the member
            Member = aType.GetMember(aProperty, MemberAccess)(0)
            Dim Result As Object
            Dim IsEnum As Boolean = False
            If Member.MemberType = MemberTypes.Property Then
                Result = CType(Member, PropertyInfo).GetValue(Parent, Nothing)
                IsEnum = CType(Member, PropertyInfo).PropertyType.IsEnum
            Else
                Result = CType(Member, FieldInfo).GetValue(Parent)
                IsEnum = CType(Member, FieldInfo).FieldType.IsEnum
            End If
            If IsEnum Then
                Result = CType(Result, Int32)
            End If
            Return Result
        End If

        ' *** Walk the . syntax - split into current object (lcMain) and further parsed objects (lcSubs)
        Dim Main As String = aProperty.Substring(0, lnAt)
        Dim Subs As String = aProperty.Substring(lnAt + 1)

        ' *** Retrieve the current property
        Member = aType.GetMember(Main, MemberAccess)(0)

        Dim aSub As Object
        If Member.MemberType = MemberTypes.Property Then
            ' *** Get its value
            aSub = CType(Member, PropertyInfo).GetValue(Parent, Nothing)
        Else
            aSub = CType(Member, FieldInfo).GetValue(Parent)
        End If

        ' *** Recurse further into the sub-properties (lcSubs)
        Return Utils.GetPropertyEx(aSub, Subs)
    End Function

    Public Shared Sub SetProperty(ByVal aObject As Object, ByVal aProperty As String, ByVal aValue As Object)
        If TypeOf aValue Is Date OrElse TypeOf aValue Is Boolean Then
            aObject.GetType.GetProperty(aProperty, MemberAccess).SetValue(aObject, aValue, Nothing)
        Else
            aObject.GetType.GetProperty(aProperty, MemberAccess).SetValue(aObject, aValue.ToString, Nothing)
        End If
    End Sub

    Public Shared Sub SetField(ByVal aObject As Object, ByVal aProperty As String, ByVal aValue As Object)
        aObject.GetType.GetField(aProperty, MemberAccess).SetValue(aObject, aValue)
    End Sub

    Public Shared Function SetPropertyEx(ByVal aParent As Object, ByVal aProperty As String, ByVal aValue As Object) As Object
        Dim aType As Type = aParent.GetType
        Dim Member As MemberInfo

        ' *** no more .s - we got our final object
        Dim lnAt As Int32 = aProperty.IndexOf(".")
        If lnAt < 0 Then
            Member = aType.GetMember(aProperty, MemberAccess)(0)
            If Member.MemberType = MemberTypes.Property Then
                If Not CType(Member, PropertyInfo).PropertyType.BaseType Is Nothing AndAlso CType(Member, PropertyInfo).PropertyType.BaseType.Equals(GetType(System.Enum)) Then
                    CType(Member, PropertyInfo).SetValue(aParent, CType(System.Enum.GetValues(CType(Member, PropertyInfo).PropertyType), System.Array).GetValue(CType(aValue, Int32)), Nothing)
                Else
                    CType(Member, PropertyInfo).SetValue(aParent, aValue, Nothing)
                End If
                Return Nothing
            Else
                CType(Member, FieldInfo).SetValue(aParent, aValue)
                Return Nothing
            End If
        End If

        ' *** Walk the . syntax
        Dim Main As String = aProperty.Substring(0, lnAt)
        Dim Subs As String = aProperty.Substring(lnAt + 1)
        Member = aType.GetMember(Main, MemberAccess)(0)

        Dim aSub As Object
        If (Member.MemberType = MemberTypes.Property) Then
            aSub = CType(Member, PropertyInfo).GetValue(aParent, Nothing)
        Else
            aSub = CType(Member, FieldInfo).GetValue(aParent)
        End If
        ' *** Recurse until we get the lowest ref
        SetPropertyEx(aSub, Subs, aValue)
    End Function

    ' Wrapper method to call a 'dynamic' (non-typelib) method
    ' on a COM object
    Public Shared Function CallMethod(ByVal aObject As Object, ByVal aMethod As String, ByVal Params As Object()) As Object
        Return aObject.GetType.GetMethod(aMethod, MemberAccess).Invoke(aObject, Params)
    End Function

End Class
