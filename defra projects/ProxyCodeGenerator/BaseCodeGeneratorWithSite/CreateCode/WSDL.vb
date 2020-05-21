Imports System.Xml 'MLD added 29/10/4

Public Class WSDL
    Private mFile As Xml.XmlDocument
    Private mManager As XmlNamespaceManager 'MLD added 29/10/4
    Public ServiceName As String
    Public BindingName As String
    Public [Namespace] As String
    Public RootNamespace As String
    Public URL As String
    Public Operations As ArrayList
    Public ComplexTypes As ArrayList
    Public SimpleTypes As ArrayList
    ' Public Defaults As ArrayList

    Public Sub New(ByVal tempFileName As String, ByVal url As String, ByVal filename As String, ByVal logMessage As Delegates.LogMessageDelegate)
        MyClass.New(tempFileName, url, filename, True, logMessage)
    End Sub

    Public Sub New(ByVal tempFileName As String, ByVal url As String, ByVal filename As String, ByVal deleteOnLoad As Boolean, ByVal logMessage As Delegates.LogMessageDelegate)
        _logMessage = logMessage
        mFile = New Xml.XmlDocument
        logMessage("Loading XML...")
        mFile.Load(tempFileName)
        mManager = New XmlNamespaceManager(mFile.NameTable)                 'MLD added 29/10/4
        mManager.AddNamespace("wsdl", "http://schemas.xmlsoap.org/wsdl/")   'MLD added 29/10/4
        mManager.AddNamespace("s", "http://www.w3.org/2001/XMLSchema")      'MLD added 29/10/4
        Dim ds As New DataSet
        If deleteOnLoad Then
            IO.File.Delete(tempFileName)
        End If
        If Not filename Is Nothing AndAlso filename.Length > 0 Then
            Dim Path As String = IO.Directory.GetParent(filename).ToString
            logMessage("Reading Root Namespace")
            RootNamespace = Path.Substring(Path.LastIndexOf(IO.Path.DirectorySeparatorChar) + 1)
        End If
        'Stop
        Me.URL = url
        ReadInfo()
    End Sub
    Private _logMessage As Delegates.LogMessageDelegate

    Private Function GetXMLElementByPath(ByVal element As XmlElement, ByVal path As String) As XmlElement 'MLD added 29/10/4
        Dim node As XmlNode = element.SelectSingleNode(path, mManager)
        Return CType(node, XmlElement)
    End Function

    Private Function GetXMLElement(ByVal element As Xml.XmlElement, ByVal elementName As String) As Xml.XmlElement
        Dim xmlElement As Xml.XmlElement = element.Item(elementName)
        If xmlElement Is Nothing Then
            xmlElement = element.Item("wsdl:" + elementName)
        End If
        Return xmlElement
    End Function

    Private Function GetFirstXMLElementByTagName(ByVal doc As Xml.XmlDocument, ByVal tagName As String) As Xml.XmlElement
        Dim xmlElement As Xml.XmlElement = doc.GetElementsByTagName(tagName)(0)
        If xmlElement Is Nothing Then
            xmlElement = doc.GetElementsByTagName("wsdl:" + tagName)(0)
        End If
        Return xmlElement
    End Function

    Private Function GetXMLElementByTagName(ByVal element As Xml.XmlElement, ByVal tagName As String) As Xml.XmlNodeList
        Dim xmlNodes As Xml.XmlNodeList = element.GetElementsByTagName(tagName)
        If xmlNodes Is Nothing OrElse xmlNodes.Count = 0 Then
            xmlNodes = element.GetElementsByTagName("wsdl:" + tagName)
        End If
        Return xmlNodes
    End Function

    Private Sub ReadInfo()
        _logMessage("Reading Definitions")
        Dim Definitions As Xml.XmlElement = GetFirstXMLElementByTagName(mFile, "definitions")

        _logMessage("Reading Services")
        Dim xmlService As Xml.XmlElement = GetXMLElement(Definitions, "service")
        ServiceName = xmlService.GetAttribute("name")

        _logMessage("Reading Bindings")
        Dim xmlBinding As Xml.XmlElement = GetXMLElement(Definitions, "binding")
        BindingName = xmlBinding.GetAttribute("name")

        [Namespace] = Definitions.GetAttribute("targetNamespace")

        'Defaults = Nothing

        _logMessage("Iterating Operations...")
        Operations = New ArrayList
        For Each Operation As Xml.XmlElement In GetXMLElementByTagName(GetXMLElement(Definitions, "portType"), "operation")
            Dim OperationName As String = Operation.GetAttribute("name")

            If GetXMLElementByTagName(Operation, "input").Count > 0 AndAlso _
               Not GetXMLElementByTagName(Operation, "input")(0).Attributes("name") Is Nothing Then
                OperationName = GetXMLElementByTagName(Operation, "input")(0).Attributes("name").Value
            End If

            Dim SoapAction As String = GetSoapAction(OperationName, xmlBinding)

            Dim Params As OperationArgument() = GetParams(Definitions, OperationName)
            'If Not Params Is Nothing Then
            '    For Each arg As OperationArgument In Params
            '        If arg.XMLType.StartsWith("s0:") Then
            '            'a complex type
            '            arg.Type()
            '            ComplexTypes.Add(New ComplexType(ComplexTypeName, Base, Members))
            '        End If
            '    Next
            'End If
            Operations.Add(New Operation(OperationName, Params, SoapAction, GetReturn(Definitions, OperationName)))
        Next Operation

        _logMessage("Iterating Types...")
        ComplexTypes = New ArrayList
        SimpleTypes = New ArrayList
        For Each schema As Xml.XmlElement In GetXMLElement(Definitions, "types").GetElementsByTagName("s:schema")
            'Try
            '    Dim DefaultString As String = _
            '        schema.GetAttribute("targetNamespace")
            '    If DefaultString.IndexOf("#"c) <> -1 Then
            '        PopulateDefaults(DefaultString.Substring(DefaultString.IndexOf("#"c) + 1))
            '    End If
            'Catch
            'End Try
            For Each SimpleType As Xml.XmlElement In schema.GetElementsByTagName("s:simpleType")
                Dim SimpleTypeName As String = SimpleType.GetAttribute("name")
                _logMessage("Simple Type: " & SimpleTypeName)

                Dim EnumList As New ArrayList
                For Each EnumerationNode As Xml.XmlElement In SimpleType.Item("s:restriction").GetElementsByTagName("s:enumeration")
                    EnumList.Add(EnumerationNode.GetAttribute("value"))
                Next EnumerationNode
                SimpleTypes.Add(New SimpleType(SimpleTypeName, EnumList, SimpleTypeEnum.Enum))
            Next SimpleType

            For Each ComplexType As Xml.XmlElement In schema.GetElementsByTagName("s:complexType")
                Dim ComplexTypeName As String = ComplexType.GetAttribute("name")
                If Not ComplexTypeName Is Nothing AndAlso ComplexTypeName.Length > 0 Then
                    _logMessage("Complex Type: " & ComplexTypeName)
                    Dim Base As String
                    Try
                        Dim BaseName As String = ComplexType.Item("s:complexContent").Item("s:extension").GetAttribute("base")
                        Base = GetDataType(Nothing, BaseName)
                    Catch
                        Base = Nothing
                    End Try

                    Dim Members() As OperationArgument
                    Try
                        Dim xmlElement As Xml.XmlElement
                        If Not ComplexType.Item("s:complexContent") Is Nothing AndAlso _
                           Not ComplexType.Item("s:complexContent").Item("s:extension") Is Nothing AndAlso _
                           Not ComplexType.Item("s:complexContent").Item("s:extension").Item("s:sequence") Is Nothing Then
                            xmlElement = ComplexType.Item("s:complexContent").Item("s:extension").Item("s:sequence")
                        Else
                            xmlElement = ComplexType.Item("s:sequence")
                        End If
                        Members = GetMembers(xmlElement)
                    Catch
                        Members = Nothing
                    End Try
                    Try
                        If ComplexType.Item("s:sequence").Item("s:element").GetAttribute("maxOccurs").ToLower = "unbounded" Then
                            ComplexTypeName = ComplexType.Item("s:sequence").Item("s:element").GetAttribute("name") & "()"
                        End If
                    Catch ex As Exception
                    End Try

                    ComplexTypes.Add(New ComplexType(ComplexTypeName, Base, Members))
                End If
            Next ComplexType
        Next schema
    End Sub

    Private Function GetSoapAction(ByVal operationName As String, ByVal binding As Xml.XmlElement)
        For Each soapAction As Xml.XmlElement In GetXMLElementByTagName(binding, "operation")
            If soapAction.GetAttribute("name") = operationName Then
                Return soapAction.Item("soap:operation").GetAttribute("soapAction")
            End If
        Next soapAction
        Return Nothing
    End Function

    Private Function GetParams(ByVal startElement As Xml.XmlElement, ByVal operationName As String) As OperationArgument()
        Try
            For Each element As Xml.XmlElement In GetXMLElementByTagName(startElement, "types")(0).Item("s:schema").GetElementsByTagName("s:element")
                If element.GetAttribute("name") = operationName Then
                    Dim ParamCount As Int32 = element.GetElementsByTagName("s:complexType").Item(0).Item("s:sequence").GetElementsByTagName("s:element").Count
                    If ParamCount = 0 Then
                        Return Nothing
                    Else
                        Dim ReturnVal As New ArrayList
                        For Each paramElement As Xml.XmlElement In element.GetElementsByTagName("s:complexType").Item(0).Item("s:sequence").GetElementsByTagName("s:element")
                            Dim Type As String = GetTypeFromAttribute(paramElement)
                            If paramElement.HasAttribute("name") Then
                                ReturnVal.Add(New OperationArgument(paramElement.GetAttribute("name"), Type, paramElement.GetAttribute("type")))
                            End If
                        Next paramElement
                        Return ReturnVal.ToArray(GetType(OperationArgument))
                    End If
                End If
            Next element
        Catch
            Return Nothing
        End Try
    End Function

    Private Function TryDotNetType(ByVal typeName As String) As String  'MLD 1/11/4 common code moved to method
        Try
            Return DataTypeConversion.GetDotNetTypeFromXML(typeName).ToString
        Catch ex As Exception
            Return typeName
        End Try
    End Function

    Private Function TrimOffNamespace(ByVal name As String) As String   'MLD 1/11/4 added
        Return name.Split(New Char() {":"c})(1)
    End Function

    Private Function GetTypeFromAttribute(ByVal element As XmlElement) As String    'MLD 1/11/4 code moved here from GetParams and GetMembers
        Dim attribute As String = element.GetAttribute("type")
        Try
            Return DataTypeConversion.GetDotNetTypeFromXML(attribute).ToString
        Catch
            'it is probably an object so use it directly
            If attribute.IndexOf(":"c) >= 0 Then
                attribute = TrimOffNamespace(attribute)
                If attribute.StartsWith("ArrayOf") Then
                    attribute = attribute.Substring(7)
                    If String.Compare(attribute, "anytype", True) = 0 Then
                        Return GetType(Object).ToString + "()"
                    End If
                    Return TryDotNetType(attribute) + "()"
                End If
                Return TryDotNetType(attribute)
            End If
            Return GetType(Object).ToString
        End Try
    End Function

    Private Function GetMembers(ByVal startElement As Xml.XmlElement) As OperationArgument()    'MLD 1/11/4 refactored
        Try
            Dim elementList As XmlNodeList = startElement.GetElementsByTagName("s:element")
            Dim ReturnVal(elementList.Count - 1) As OperationArgument
            Dim Index As Int32 = 0
            For Each element As Xml.XmlElement In elementList
                If Not element.GetAttribute("name") Is Nothing AndAlso _
                    element.GetAttribute("name").Length > 0 Then
                    ReturnVal(Index) = New OperationArgument(element.GetAttribute("name"), GetTypeFromAttribute(element))
                    Index += 1
                End If
            Next element
            ReDim Preserve ReturnVal(Index - 1)
            Return ReturnVal
        Catch
            Return Nothing
        End Try
    End Function

    Private Function GetDataType(ByVal startElement As Xml.XmlElement, ByVal type As String) As String
        Try
            type = DataTypeConversion.GetDotNetTypeFromXML(type).ToString
        Catch
            'it is probably an object so use it directly
            If type.StartsWith("s") AndAlso type.Substring(2).ToLower = "schema" Then
                Return GetType(DataSet).ToString()
            End If
            If type.IndexOf(":"c) >= 0 Then
                type = TrimOffNamespace(type)
                If String.Compare(type, "ArrayOfAnyType", True) = 0 Then
                    Return "Object()"
                End If
                'ensure that it is not an array
                If Not startElement Is Nothing Then
                    Try
                        Dim element1 As XmlElement = GetXMLElementByPath(startElement, "s:complexType[@name='" + type + "']")
                        Dim element2 As XmlElement = element1.Item("s:sequence").Item("s:element")
                        If element2.GetAttribute("maxOccurs").ToLower = "unbounded" Then
                            If type.StartsWith("ArrayOf") Then
                                Return TryDotNetType(type.Substring(7)) & "()"
                            End If
                            Return type & "()"
                        End If
                    Catch
                    End Try
                End If
            Else
                type = Nothing
            End If
        End Try
        Return type
    End Function

    Private Function GetReturn(ByVal startElement As Xml.XmlElement, ByVal operationName As String) As String 'MLD modified 29/10/4
        Try
            Dim element As XmlElement = GetXMLElementByPath(startElement, "wsdl:types/s:schema/s:element[@name='" + operationName + "Response']")
            Dim paramElement As XmlElement = GetXMLElementByPath(element, "s:complexType/s:sequence/s:element[1]")
            Dim attribute As String = paramElement.GetAttribute("type")
            If attribute.Length = 0 Then
                'must be a complex type
                paramElement = paramElement.Item("s:complexType").Item("s:sequence").Item("s:element")
                Return GetDataType(Nothing, paramElement.GetAttribute("ref"))
            End If
            Return GetDataType(GetXMLElementByPath(startElement, "wsdl:types/s:schema"), attribute)
        Catch
            Return Nothing
        End Try
    End Function

    'Private Function PopulateDefaults(ByVal defaults As String)
    '    Dim DefaultList As String() = defaults.Split(";"c)
    '    For Each def As String In DefaultList
    '        Dim DefLine As String() = def.Split("="c)
    '        If DefLine.Length = 3 Then
    '            If Me.Defaults Is Nothing Then
    '                Me.Defaults = New ArrayList
    '            End If
    '            'pop in different chars
    '            DefLine(2) = DefLine(2).Replace("'''", "|||")
    '            DefLine(2) = DefLine(2).Replace("'", """")
    '            DefLine(2) = DefLine(2).Replace("|||", "'")
    '            Me.Defaults.Add(New DefaultStruct(DefLine(0), DefLine(1), DefLine(2)))
    '        End If
    '    Next def
    'End Function

    Private Function IsNumeric(ByVal check As String) As Boolean
        Try
            Dim Test As Int32 = CType(check, Int32)
            Return True
        Catch
            Return False
        End Try
    End Function
End Class

Public Structure Operation
    Public Sub New(ByVal name As String, ByVal arguments As OperationArgument(), ByVal soapAction As String, ByVal returnType As String)
        Me.Name = name
        Me.Arguments = arguments
        Me.ReturnType = returnType
        Me.SoapAction = soapAction
    End Sub
    Public Sub New(ByVal name As String, ByVal arguments As OperationArgument(), ByVal soapAction As String)
        MyClass.New(name, arguments, soapAction, Nothing)
    End Sub
    Public Name As String
    Public Arguments As OperationArgument()
    Public ReturnType As String
    Public SoapAction As String
End Structure

Public Structure OperationArgument
    Public Sub New(ByVal name As String, ByVal type As String, ByVal xmlType As String)
        Me.Name = name
        Me.Type = type
        Me.XMLType = xmlType
    End Sub

    Public Sub New(ByVal name As String, ByVal type As String)
        MyClass.New(name, type, Nothing)
    End Sub
    Public Name As String
    Public Type As String
    Public XMLType As String
End Structure

Public Structure ComplexType
    Public Sub New(ByVal name As String, ByVal members As OperationArgument())
        MyClass.New(name, Nothing, members)
    End Sub

    Public Sub New(ByVal name As String, ByVal base As String, ByVal members As OperationArgument())
        Me.Name = name
        Me.Members = members
        Me.Base = base
    End Sub
    Public Name As String
    Public Base As String
    Public Members As OperationArgument()
End Structure

Public Enum SimpleTypeEnum
    [Enum]
End Enum

Public Structure SimpleType
    Public Sub New(ByVal name As String, ByVal enumNameList As ArrayList, ByVal type As SimpleTypeEnum)
        Me.Name = name
        Me.EnumNameList = enumNameList
        Me.Type = type
    End Sub

    Public Name As String
    Public EnumNameList As ArrayList
    Public Type As SimpleTypeEnum
End Structure

Public Structure DefaultStruct
    Public Sub New(ByVal [class] As String, ByVal propertyName As String, ByVal value As String)
        Me.Class = [class]
        Me.PropertyName = propertyName
        Me.Value = value
    End Sub
    Public PropertyName As String
    Public Value As String
    Public [Class] As String
End Structure
