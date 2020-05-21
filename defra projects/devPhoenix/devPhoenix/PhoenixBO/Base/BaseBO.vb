Imports System
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Web.Services.Description

<Serializable()> _
Public MustInherit Class BaseBO
    Implements IPersist, IUpdatable, ICloneable

    Protected Function GetDatabaseDate(ByVal aDate As Date) As Object
        If aDate.Ticks > 0 Then
            Return aDate
        Else
            Return Nothing
        End If
    End Function

    Public Overridable Function Save() As BaseBO Implements IPersist.Save
        DataObjects.Sprocs.LastError = Nothing
        ValidationErrors = Nothing
        Return Nothing
    End Function


    Public Overridable Function Delete() As Object
        Return Nothing
    End Function

    Public Property Created() As Boolean Implements IUpdatable.Created
        Get
            Return mCreated
        End Get
        Set(ByVal Value As Boolean)
            mCreated = Value
        End Set
    End Property
    Private mCreated As Boolean

    <DOtoBOMapping("CheckSum")> _
       Public Property CheckSum() As Int32 Implements IUpdatable.CheckSum
        Get
            Return mCheckSum
        End Get
        Set(ByVal Value As Int32)
            mCheckSum = Value
        End Set
    End Property
    Protected mCheckSum As Int32

    Public Property ValidationErrors() As ValidationManager
        Get
            Return mValidationErrors
        End Get
        Set(ByVal Value As ValidationManager)
            mValidationErrors = Value
        End Set
    End Property
    Private mValidationErrors As ValidationManager

    'Private Overloads Function Clone(ByVal objectToClone As BaseBO) As BaseBO
    '    Dim NewObject As Object = System.Activator.CreateInstance(objectToClone.GetType)
    '    Dim Props As Reflection.PropertyInfo() = objectToClone.GetType.GetProperties()
    '    Dim Prop As Reflection.PropertyInfo
    '    Dim NewProp As Reflection.PropertyInfo

    '    For Each Prop In Props
    '        Try
    '            If String.Compare(Prop.Name, "checksum") <> 0 Then
    '                Dim NewValue As Object = Prop.GetValue(objectToClone, Nothing)
    '                If TypeOf Prop.GetValue(objectToClone, Nothing) Is BaseBO Then
    '                    NewObject.GetType.GetProperty(Prop.Name).SetValue(NewObject, Clone(CType(Prop.GetValue(objectToClone, Nothing), BaseBO)), Nothing)
    '                Else
    '                    If Prop.CanWrite AndAlso Not NewValue Is Nothing Then
    '                        NewProp = NewObject.GetType.GetProperty(Prop.Name, Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
    '                        If Not NewProp Is Nothing Then
    '                            NewProp.SetValue(NewObject, Prop.GetValue(objectToClone, Nothing), Nothing)
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        Catch
    '        End Try

    '    Next
    '    Return CType(NewObject, BaseBO)
    'End Function

    Public Overridable Overloads Function Clone() As Object Implements System.ICloneable.Clone
        Dim ClonedObject As BaseBO = Me
        Me.CheckSum = 0
        Return ClonedObject
    End Function

    Protected Shared Function GetSchemaDataSet(ByVal schema As String) As DataSet 'MLD 24/11/4 moved here from BOCITESImportExportPermit
        'get the DS ready
        Dim result As New DataSet
        'create a stream to put the info into
        Dim io As New io.StringReader(schema)
        'set-up the DS schema
        result.ReadXmlSchema(io)
        'tidy ...
        io.Close()
        io = Nothing
        Return result
    End Function

    Protected Shared Function GetProp(ByVal item As Object, ByVal name As String) As Object    'MLD 2/12/4 moved here from BOCITESImportExportPermit
        Return item.getType().GetProperty(name).GetValue(item, Nothing)
    End Function

    Protected Shared Function Resolve(ByVal item As String) As String    'MLD 2/12/4 moved here from BOCITESImportExportPermit
        If item Is Nothing Then Return ""
        Return item
    End Function

    Protected Shared Function Resolve(ByVal item As Object, ByVal prop As String) As String    'MLD 2/12/4 moved here from BOCITESImportExportPermit
        If item Is Nothing Then Return ""
        Return CType(GetProp(item, prop), String)
    End Function

    Protected Shared Function Resolve(ByVal item As Boolean) As String    'MLD 2/12/4 moved here from BOCITESImportExportPermit
        If item Then Return "X"
        Return ""
    End Function

    'Compresses a string containing "Environment.Newline-separated" lines into a set number of lines
    Protected Shared Function CompressLines(ByVal text As String, ByVal maxLines As Int32) As String    'MLD 30/11/4
        Dim newlines() As Char = Environment.NewLine.ToCharArray()
        Dim linefeed() As Char = Environment.NewLine.ToCharArray(1, 1)
        Dim trimmed As String = text.Trim(newlines)
        Dim squashed As String = trimmed.Replace(Environment.NewLine, "")
        Dim lines() As String = trimmed.Replace(Environment.NewLine, linefeed).Split(linefeed)
        Dim rough As Int32 = squashed.Length \ maxLines  'rough guess at desired line length
        Dim work() As String

        If lines.Length <= maxLines Then
            Return trimmed                              'no need to do anything
        End If
        Do
            work = TryCompression(lines, rough)         'try compression
            rough += 5
        Loop While work.Length > maxLines               'retry if not compressed enough

        text = ""
        For Each line As String In work                 'build resulting string
            text += line + Environment.NewLine
        Next
        Return text.TrimEnd(newlines)
    End Function

    Private Shared Function TryCompression(ByVal lines() As String, ByVal rough As Int32) As String()   'MLD 30/11/4
        Dim index1 As Int32 = 0
        Dim index2 As Int32 = 0
        Dim space() As Char = " ".ToCharArray()
        Dim work(lines.Length) As String

        While index2 < lines.Length
            work(index1) = ""
            While work(index1).Length < rough AndAlso index2 < lines.Length
                work(index1) += lines(index2).TrimEnd(space) + " "  'one and only one space
                index2 += 1
            End While
            work(index1).TrimEnd(space)
            index1 += 1
        End While
        ReDim Preserve work(index1 - 1)
        Return work
    End Function
End Class
