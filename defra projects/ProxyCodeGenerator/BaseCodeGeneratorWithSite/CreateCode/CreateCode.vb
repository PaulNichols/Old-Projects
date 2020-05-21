Imports System.CodeDom.Compiler
Imports Microsoft.VisualBasic
Imports Microsoft.CSharp
Imports System.CodeDom
Imports System.io

Public Module Delegates
    Public Delegate Sub LogMessageDelegate(ByVal message As String)
End Module

Public Class CreateCode
    Private Const SOAPSUDS_LOCATION As String = """C:\Program Files\Microsoft Visual Studio .NET 2003\SDK\v1.1\Bin\SoapSuds"""
    Private Const CREATE_ASYNC As Boolean = False
    Private Const CREATE_WSE As Boolean = True
    Private Const CREATE_MODIFIEDEVENT As Boolean = False
    Private Const PROXYIGNORECLASSNAME As String = "ProxyIgnore"

    Private mProvider As CodeDomProvider
    'Private mType As CodeTypeDeclaration
    Private mInfo As String
    Private mWSDL As WSDL
    Private mFileName As String
    Protected RootNamespace As String
    Public DefaultProxy As Proxy

    Public Sub New()
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://mydevphoenixwebservice:10080/ListService.asmx?disco"" filename=""ListService.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://mydevphoenixwebservice:10080/ListService.asmx?wsdl"" filename=""ListService.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '            "C:\Web\devPhoenix\devPhoenixCommonCode\Web References\Lists\Reference.map")
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://mydevphoenixwebservice:10080/CreatePartyService.asmx?disco"" filename=""CreatePartyService.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://mydevphoenixwebservice:10080/CreatePartyService.asmx?wsdl"" filename=""CreatePartyService.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '"C:\Web\devPhoenix\devPhoenixCommonCode\Web References\CreateParty\Reference.map", Nothing)
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://mydevphoenixwebservice:10080/MaintGWDRefData.asmx?disco"" filename=""MaintGWDRefData.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://mydevphoenixwebservice:10080/MaintGWDRefData.asmx?wsdl"" filename=""MaintGWDRefData.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '            "C:\Web\devPhoenix\devPhoenixCommonCode\Web References\GWDRefData\Reference.map")
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://itd6687wa:5302/WebService.asmx?disco"" filename=""WebService.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://itd6687wa:5302/WebService.asmx?wsdl"" filename=""WebService.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '            "C:\Web\Reference.map")
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://mydevphoenixwebservice:10080/CITESService.asmx?disco"" filename=""CITESService.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://mydevphoenixwebservice:10080/CITESService.asmx?wsdl"" filename=""CITESService.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '            "c:\web\devPhoenixSecure\Web References\CITESService\Reference.map", Nothing)
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://mydevphoenixwebservice:10080/CommonService.asmx?disco"" filename=""CommonService.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://mydevphoenixwebservice:10080/CommonService.asmx?wsdl"" filename=""CommonService.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '            "C:\Web\devPhoenix\devPhoenixCommonCode\Web References\CommonService\Reference.map", Nothing)
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://mydevphoenixwebservice:10080/ReportService.asmx?disco"" filename=""ReportService.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://mydevphoenixwebservice:10080/ReportService.asmx?wsdl"" filename=""ReportService.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '            "C:\Web\devPhoenixSecure\Web References\ReportService\Reference.map")
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://mydevphoenixwebservice:10080/NotesService.asmx?disco"" filename=""NotesService.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://mydevphoenixwebservice:10080/NotesService.asmx?wsdl"" filename=""NotesService.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '"C:\Web\devPhoenix\devPhoenixCommonCode\Web References\PartyNotes\Reference.map")
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://mydevphoenixwebservice:10080/ReferenceData.asmx?disco"" filename=""ReferenceData.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://mydevphoenixwebservice:10080/ReferenceData.asmx?wsdl"" filename=""ReferenceData.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '            "c:\web\devPhoenixSecure\Web References\ReferenceData\Reference.map")
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://devcommoncodewebservice:10080/notification.asmx?disco"" filename=""notification.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://devcommoncodewebservice:10080/notification.asmx?wsdl"" filename=""notification.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '"C:\Web\devPhoenix\devPhoenixCommonCode\Web References\NotificationWebService\Reference.map", Nothing)
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://mydevphoenixwebservice:10080/ApplicationProgressionService.asmx?disco"" filename=""ApplicationProgressionService.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://mydevphoenixwebservice:10080/ApplicationProgressionService.asmx?wsdl"" filename=""ApplicationProgressionService.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '            "c:\web\devPhoenixSecure\Web References\ApplicationProgressionService\Reference.map", Nothing)
        MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
                    "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
                    "  <Results>" & Environment.NewLine & _
                    "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://mydevphoenixwebservice:10080/BirdService.asmx?disco"" filename=""BirdService.disco"" />" & Environment.NewLine & _
                    "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://mydevphoenixwebservice:10080/BirdService.asmx?wsdl"" filename=""BirdService.wsdl"" />" & Environment.NewLine & _
                    "  </Results>" & Environment.NewLine & _
                    "</DiscoveryClientResultsFile>", _
                    "C:\Web\devPhoenixSecure\Web References\BirdService\Reference.map", Nothing)
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://devcommoncodewebservice:10080/notification.asmx?disco"" filename=""notification.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://devcommoncodewebservice:10080/notification.asmx?wsdl"" filename=""notification.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '            "C:\Web\devPhoenix\devPhoenixCommonCode\Web References\NotificationWebService\Reference.map", Nothing)
        'MyClass.New("<?xml version=""1.0"" encoding=""utf-8""?>" & Environment.NewLine & _
        '            "<DiscoveryClientResultsFile xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Environment.NewLine & _
        '            "  <Results>" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.DiscoveryDocumentReference"" url=""http://mydevphoenixwebservice:10080/WebService.asmx?disco"" filename=""WebService.disco"" />" & Environment.NewLine & _
        '            "    <DiscoveryClientResult referenceType=""System.Web.Services.Discovery.ContractReference"" url=""http://mydevphoenixwebservice:10080/WebService.asmx?wsdl"" filename=""WebService.wsdl"" />" & Environment.NewLine & _
        '            "  </Results>" & Environment.NewLine & _
        '            "</DiscoveryClientResultsFile>", _
        '            "C:\Web\devPhoenix\devPhoenixCommonCode\Web References\PhoenixWebService\Reference.map", Nothing)
    End Sub

    Public Sub New(ByVal info As String, ByVal fileName As String, ByVal logMessage As Delegates.LogMessageDelegate)
        MyBase.New()
        GC.Collect()
        mWSDL = Nothing
        _logMessage = logMessage
        'Me.LogMessage("Executing Version: " & System.Windows.Forms.Application.ProductVersion)
        mFileName = fileName
        mInfo = info
        ParseInfo()
        SetVBProvider()
    End Sub
    Private _logMessage As Delegates.LogMessageDelegate

    Private Sub ParseInfo()
        If Not mInfo Is Nothing Then
            DefaultProxy = New Proxy
            Dim xml As New Xml.XmlDocument
            LogMessage("Loading XML Document...")
            xml.LoadXml(mInfo)
            LogMessage("XML Document Loaded...")
            LogMessage("Getting XML Elements...")
            Dim DiscoveryClientElement As Xml.XmlNode = xml.GetElementsByTagName("DiscoveryClientResultsFile").Item(0)
            Dim ResultsElement As Xml.XmlNode = DiscoveryClientElement.Item("Results")
            For Each node As Xml.XmlNode In ResultsElement.ChildNodes
                If node.Name = "DiscoveryClientResult" AndAlso _
                   node.Attributes("referenceType").Value.ToLower.EndsWith("contractreference") Then
                    LogMessage("Executing SOAP...the slow bit, sorry!")
                    Dim TempFile As String = ExecuteSoap(node.Attributes("url").Value)
                    LogMessage("SOAP executed ok...")
                    Dim URL As String = node.Attributes("url").Value
                    URL = URL.Substring(0, URL.LastIndexOf("?"))
                    mWSDL = New WSDL(TempFile, URL, mFileName, AddressOf LogMessage)
                End If
            Next node
        End If
    End Sub

    Private Sub LogMessage(ByVal message As String)
        If Not _logMessage Is Nothing Then
            _logMessage(message)
        End If
    End Sub

    Private Function ExecuteSoap(ByVal url As String) As String
        Dim TempFile As String = Path.GetTempFileName
        'Dim TempFile As String = Path.Combine(TempDirectory, "SoapSuds")

        'Dim fs As FileStream = System.IO.File.OpenWrite(TempFile)
        Dim md As System.Runtime.Remoting.MetadataServices.MetaData
        md = New System.Runtime.Remoting.MetadataServices.MetaData
        md.RetrieveSchemaFromUrlToFile(url, TempFile)
        md = Nothing
        ' force GC
        GC.Collect()
        'fs.Close()
        'fs = Nothing

        'Dim Exec As String = String.Concat( _
        '                     "", _
        '                     "-url:", url, _
        '                     " -wsdl -gc -os:", TempFile)

        'Dim pInfo As New ProcessStartInfo
        'With pInfo
        '    .FileName = SOAPSUDS_LOCATION
        '    .Arguments = Exec
        '    .UseShellExecute = False
        '    .RedirectStandardOutput = True
        '    .CreateNoWindow = True
        'End With
        'Dim p As Process = Process.Start(pInfo)
        'Do Until p.HasExited
        'Loop
        'p.Close()
        'p = Nothing
        Return TempFile
    End Function

    Private Sub AddProxyIgnoreClass(ByVal ns As CodeNamespace)
        Dim AttributeType As New CodeTypeDeclaration(PROXYIGNORECLASSNAME)
        AttributeType.BaseTypes.Add(New CodeTypeReference(GetType(Attribute).ToString))

        ns.Types.Add(AttributeType)
    End Sub

    ReadOnly Property GetCode() As String
        Get
            RootNamespace = mWSDL.RootNamespace

            Dim ns As New CodeNamespace(RootNamespace)

            Dim unit As New CodeCompileUnit
            unit.Namespaces.Add(ns)

            With ns.Imports
                .Add(New CodeNamespaceImport("System"))
                .Add(New CodeNamespaceImport("System.ComponentModel"))
                .Add(New CodeNamespaceImport("System.Diagnostics"))
                .Add(New CodeNamespaceImport("System.Web.Services"))
                .Add(New CodeNamespaceImport("System.Web.Services.Protocols"))
                .Add(New CodeNamespaceImport("System.Xml.Serialization"))
            End With

            If CREATE_WSE Then ns.Types.Add(CreateMainClass("Wse"))
            ns.Types.Add(CreateMainClass(""))

            AddProxyIgnoreClass(ns)

            AddedTypes = New Hashtable
            For Each ComplexType As ComplexType In mWSDL.ComplexTypes
                'check to see if it is used as either an arg or a return from my operations
                If Not ComplexType.Name.EndsWith("()") Then
                    Dim ArrayTypeName As String = ComplexType.Name & "()"
                    'If ComplexType.Name.EndsWith("()") Then
                    'ArrayTypeName = ComplexType.Name
                    'Else
                    'End If
                    Dim ProcessAdd As Boolean = False
                    For Each op As Operation In mWSDL.Operations
                        If Not op.ReturnType Is Nothing AndAlso _
                           (op.ReturnType = ComplexType.Name Or op.ReturnType = ArrayTypeName) Then
                            ProcessAdd = True
                            Exit For
                        End If
                        If ProcessAdd Then Exit For

                        If Not op.Arguments Is Nothing Then
                            For Each arg As OperationArgument In op.Arguments
                                If (arg.Type = ComplexType.Name Or arg.Type = ArrayTypeName) Then
                                    ProcessAdd = True
                                    Exit For
                                End If
                            Next arg
                        End If
                        If ProcessAdd Then Exit For

                        'check to see if its a base type of another class
                        For Each ct As ComplexType In mWSDL.ComplexTypes
                            If Not ct.Base Is Nothing AndAlso _
                               (ct.Base = ComplexType.Name Or ct.Base = ArrayTypeName) Then
                                ProcessAdd = True
                                Exit For
                            End If
                            If Not ComplexType.Base Is Nothing AndAlso _
                               (ComplexType.Base = ct.Name) Then ' OrElse ct.Name = ArrayTypeName) Then
                                ProcessAdd = True
                                Exit For
                            End If
                            If ComplexType.Base Is Nothing AndAlso _
                               ct.Name = ArrayTypeName Then
                                ProcessAdd = True
                                Exit For
                            End If
                            If Not ct.Members Is Nothing AndAlso _
                               ct.Members.Length > 0 Then
                                For Each mem As OperationArgument In ct.Members
                                    If (mem.Type = ComplexType.Name Or mem.Type = ArrayTypeName) Then
                                        ProcessAdd = True
                                        Exit For
                                    End If
                                Next mem
                            End If
                            If ProcessAdd Then Exit For
                        Next ct
                        If ProcessAdd Then Exit For

                    Next op
                    If ProcessAdd Then
                        If Not AddedTypes.Contains(ComplexType.Name) Then
                            AddedTypes.Add(ComplexType.Name, Nothing)
                            ns.Types.Add(CreateComplexTypeClass(ComplexType))
                        End If
                    End If
                End If
            Next ComplexType

            For Each SimpleType As SimpleType In mWSDL.SimpleTypes
                ns.Types.Add(CreateSimpleType(SimpleType))
            Next SimpleType

            AddedTypes = Nothing

            Return Generate(unit)
        End Get
    End Property
    Private AddedTypes As Hashtable

    Private Sub AddAttributes(ByRef type As CodeTypeDeclaration, ByVal name As String)
        For Each CT As ComplexType In Me.mWSDL.ComplexTypes
            If name = CT.Base Then
                type.CustomAttributes.Add(AttributeXMLInclude(CT.Name))
                AddAttributes(type, CT.Name)
            End If
            '    ns.Types.Add(CreateComplexTypeClass(ComplexType))
        Next CT
    End Sub

    Private Function CreateSimpleType(ByVal simpleType As SimpleType) As CodeTypeDeclaration
        Dim type As New CodeTypeDeclaration(simpleType.Name)
        With type
            .TypeAttributes = Reflection.TypeAttributes.Public
            Dim AttributeDeclaration As New CodeDom.CodeAttributeDeclaration
            AttributeDeclaration.Name = "ProxyIgnore"
            .CustomAttributes.Add(AttributeDeclaration)
            Select Case simpleType.Type
                Case SimpleTypeEnum.Enum
                    Dim [Enum] As New CodeVariableDeclarationStatement
                    .IsEnum = True
                    For Each name As String In simpleType.EnumNameList
                        Dim value As New CodeMemberField(GetType(Int32), name)

                        Dim Result As Object = DefaultProxy.GetEnumValues(simpleType.Name, name, PROXYIGNORECLASSNAME)
                        If Not Result Is Nothing Then
                            'used to set the value if required
                            value.InitExpression = New CodeDom.CodePrimitiveExpression(Result)
                        End If
                        .Members.Add(value)
                    Next name
            End Select
        End With
        Return type
    End Function

    Enum Test
        x
    End Enum

    Private Function CreateComplexTypeClass(ByVal complexType As ComplexType) As CodeTypeDeclaration
        Dim type As New CodeTypeDeclaration(complexType.Name)
        With type
            .CustomAttributes.Add(AttributeXMLType)
            .CustomAttributes.Add(AttributeSerializable)

            'check to see if anyone inherits from me
            AddAttributes(type, complexType.Name)

            .TypeAttributes = Reflection.TypeAttributes.Public
            If Not complexType.Base Is Nothing Then
                .BaseTypes.Add(New CodeTypeReference(complexType.Base))
                Dim DefaultConstructor As New CodeConstructor
                DefaultConstructor.Attributes = DefaultConstructor.Attributes Or MemberAttributes.Public
                GetDefaultsFrombase(DefaultConstructor, complexType.Base, complexType.Name)
                'Do
                '    For Each BaseComplexType As ComplexType In mWSDL.ComplexTypes
                '        If BaseComplexType.Name = complexType.Base Then
                '            For Each member As OperationArgument In BaseComplexType.Members
                '                Try
                '                    Dim InvokeMember As New CodeMemberField(member.Type, "m" & member.Name)
                '                    Dim DefValue As Object = DefaultProxy.GetDefaults(complexType.Name, member.Name)
                '                    If Not DefValue Is Nothing Then
                '                        InvokeMember.InitExpression = New CodeSnippetExpression(DefValue)
                '                        DefaultConstructor.Statements.Add(New CodeAssignStatement(New CodePropertyReferenceExpression(New CodeBaseReferenceExpression, member.Name), New CodeSnippetExpression(DefValue)))
                '                    End If
                '                Catch
                '                End Try
                '            Next member
                '        End If
                '    Next BaseComplexType
                'Loop
                If DefaultConstructor.Statements.Count > 0 Then
                    .Members.Add(DefaultConstructor)
                End If
                '                            DefaultConstructor.Statements.Add(InvokeMember)
            End If

            If Not complexType.Members Is Nothing Then
                Dim CreateEventArgs As New CodeObjectCreateExpression
                CreateEventArgs.CreateType = New CodeTypeReference(GetType(EventArgs))

                If CREATE_MODIFIEDEVENT And .BaseTypes.Count = 0 Then
                    'Dim ModifiedDelegate As New CodeTypeDelegate("ModifiedDelegate")
                    'With ModifiedDelegate
                    '    .Parameters.Add(New CodeParameterDeclarationExpression(New CodeTypeReference(GetType(Object)), "Sender"))
                    '    .Parameters.Add(New CodeParameterDeclarationExpression(New CodeTypeReference(GetType(EventArgs)), "e"))
                    'End With
                    '.Members.Add(ModifiedDelegate)
                    Dim OnChanged As New CodeMemberMethod
                    With OnChanged
                        .Name = "OnChanged"
                        .Parameters.Add(New CodeParameterDeclarationExpression(New CodeTypeReference(GetType(Object)), "Sender"))
                        .Parameters.Add(New CodeParameterDeclarationExpression(New CodeTypeReference(GetType(EventArgs)), "e"))
                        Dim t As New EventArgs
                        .Statements.Add(New CodeDelegateInvokeExpression(New CodeEventReferenceExpression(New CodeThisReferenceExpression, "Changed"), New CodeExpression() {New CodeThisReferenceExpression, CreateEventArgs}))
                        .Attributes = MemberAttributes.Final Or MemberAttributes.Public
                    End With
                    .Members.Add(OnChanged)

                    Dim ModifiedEvent As New CodeMemberEvent
                    With ModifiedEvent
                        .Name = "Changed"
                        .Type = New CodeTypeReference(GetType(System.EventHandler)) 'ModifiedDelegate.Name)
                        .Attributes = MemberAttributes.Public
                    End With
                    .Members.Add(ModifiedEvent)
                End If


                For Each member As OperationArgument In complexType.Members
                    Dim InvokeProperty As New CodeMemberProperty
                    InvokeProperty.Attributes = MemberAttributes.Public
                    InvokeProperty.Name = member.Name
                    InvokeProperty.HasGet = True
                    InvokeProperty.HasSet = True

                    InvokeProperty.Type = New CodeTypeReference(member.Type)
                    InvokeProperty.GetStatements.Add(New CodeMethodReturnStatement(New CodeVariableReferenceExpression("m" & member.Name)))
                    Dim CodeAssign As New CodeAssignStatement(New CodeVariableReferenceExpression("m" & member.Name), New CodeVariableReferenceExpression("value"))
                    If CREATE_MODIFIEDEVENT Then
                        Dim IfState As New CodeConditionStatement
                        IfState.Condition = New CodeBinaryOperatorExpression(New CodeVariableReferenceExpression("m" & member.Name), CodeBinaryOperatorType.IdentityInequality, New CodeVariableReferenceExpression("value"))
                        IfState.TrueStatements.Add(New CodeMethodInvokeExpression(New CodeMethodReferenceExpression(New CodeThisReferenceExpression, "OnChanged"), New CodeExpression() {New CodeThisReferenceExpression, CreateEventArgs}))
                        IfState.TrueStatements.Add(CodeAssign)
                        InvokeProperty.SetStatements.Add(IfState)
                    Else
                        InvokeProperty.SetStatements.Add(CodeAssign)
                    End If

                    .Members.Add(InvokeProperty)

                    Dim InvokeMember As New CodeMemberField(member.Type, "m" & member.Name)
                    Try
                        Dim DefValue As Object = DefaultProxy.GetDefaults(complexType.Name, member.Name)
                        If Not DefValue Is Nothing Then
                            InvokeMember.InitExpression = New CodeSnippetExpression(DefValue)
                        End If
                    Catch
                    End Try
                    'If Not mWSDL.Defaults Is Nothing Then
                    '    For Each def As DefaultStruct In mWSDL.Defaults
                    '        If def.Class = .Name AndAlso _
                    '           def.PropertyName = member.Name Then
                    '            InvokeMember.InitExpression = New CodeSnippetExpression(def.Value)
                    '        End If
                    '    Next def
                    'End If

                    .Members.Add(InvokeMember)
                    'If complexType.Item("s:sequence").Item("s:element").GetAttribute("maxOccurs").ToLower = "unbounded" Then
                    '    ComplexTypeName = complexType.Item("s:sequence").Item("s:element").GetAttribute("name") & "()"
                    'End If

                Next member
            End If
        End With
        Dim ClassHeaderInfo As String = DefaultProxy.GetClassHeader(complexType.Name)
        If Not ClassHeaderInfo Is Nothing AndAlso ClassHeaderInfo.Length > 0 Then
            type.Members.Add(New CodeSnippetTypeMember(ClassHeaderInfo))
        End If
        Return type
    End Function

    Private Function CreateMainClass() As CodeTypeDeclaration
        Return CreateMainClass("")
    End Function

    Private Function CreateMainClass(ByVal suffix As String) As CodeTypeDeclaration
        Dim type As New CodeTypeDeclaration(mWSDL.ServiceName & suffix)
        With type
            .TypeAttributes = Reflection.TypeAttributes.Public
            .BaseTypes.Add(New CodeTypeReference("Microsoft.Web.Services.WebServicesClientProtocol"))

            .CustomAttributes.AddRange(New CodeAttributeDeclaration() {AttributeDebugger, AttributeDesigner, AttributeWSBinding})
            '????? Unsure if this needs to be done.  Some have it, some don't, but I cannot work
            'out what determines it????
            'TODO: System.Xml.Serialization.XmlIncludeAttribute(GetType(BaseBO))>  _

            .Members.Add(BaseConstructor)

            'add operations
            For Each op As Operation In mWSDL.Operations
                .Members.Add(ProxyMethods(op.Name, op.Arguments, op.ReturnType, op.SoapAction))
                If CREATE_ASYNC Then
                    .Members.Add(ProxyMethodsBegin(op.Name, op.Arguments))
                    .Members.Add(ProxyMethodsEnd(op.Name, op.ReturnType))
                End If
            Next op
        End With
        Return type
    End Function

    Private ReadOnly Property ProxyMethods(ByVal name As String, ByVal arguments As OperationArgument(), ByVal returnType As String, ByVal soapAction As String) As CodeMemberMethod
        Get
            Dim proxyMethod As New CodeMemberMethod
            With proxyMethod
                .Name = name
                .Attributes = MemberAttributes.Public
                .CustomAttributes.Add(AttributeSoapDocumentMethod(soapAction))

                Dim invoke As New CodeMethodInvokeExpression
                invoke.Method = New CodeMethodReferenceExpression(New CodeThisReferenceExpression, "Invoke")
                invoke.Parameters.Add(New CodeSnippetExpression("""" & name & """"))

                Dim ObjectDeclare As New CodeVariableDeclarationStatement(GetType(Object()), "results")

                If arguments Is Nothing Then
                    invoke.Parameters.Add(New CodeSnippetExpression("New Object(-1) {}"))
                Else
                    Dim ArgList As String = ""
                    For Each arg As OperationArgument In arguments
                        Dim param As New CodeParameterDeclarationExpression(arg.Type, arg.Name)
                        .Parameters.Add(param)
                        ArgList &= arg.Name.Trim & ","
                    Next arg
                    If ArgList.Length > 0 Then
                        ArgList = String.Concat("New Object() {", ArgList.Substring(0, ArgList.Length - 1), "}")
                        invoke.Parameters.Add(New CodeSnippetExpression(ArgList))
                    End If
                End If

                Dim resultsVar As New CodeVariableDeclarationStatement(GetType(Object()), "results", invoke)
                .Statements.Add(resultsVar)

                If Not returnType Is Nothing Then
                    .ReturnType = New CodeTypeReference(returnType)

                    Dim returnStatement As New CodeCastExpression(.ReturnType, New CodeSnippetExpression("results(0)"))
                    .Statements.Add(New CodeMethodReturnStatement(returnStatement))
                End If
            End With
            Return proxyMethod
        End Get
    End Property

    Private ReadOnly Property ProxyMethodsBegin(ByVal name As String, ByVal arguments As OperationArgument()) As CodeMemberMethod
        Get
            Dim proxyMethod As New CodeMemberMethod
            With proxyMethod
                .Name = "Begin" & name
                .Attributes = MemberAttributes.Public

                Dim invoke As New CodeMethodInvokeExpression
                invoke.Method = New CodeMethodReferenceExpression(New CodeThisReferenceExpression, "BeginInvoke")
                invoke.Parameters.Add(New CodeSnippetExpression("""" & name & """"))

                Dim ObjectDeclare As New CodeVariableDeclarationStatement(GetType(Object()), "results")

                If arguments Is Nothing Then
                    invoke.Parameters.Add(New CodeSnippetExpression("New Object(-1) {}"))
                Else
                    Dim ArgList As String = ""
                    For Each arg As OperationArgument In arguments
                        Dim param As New CodeParameterDeclarationExpression(arg.Type, arg.Name)
                        .Parameters.Add(param)
                        ArgList &= arg.Name.Trim & ","
                    Next arg
                    If ArgList.Length > 0 Then
                        ArgList = String.Concat("New Object() {", ArgList.Substring(0, ArgList.Length - 1), "}")
                        invoke.Parameters.Add(New CodeSnippetExpression(ArgList))
                    End If
                End If

                .Parameters.Add(New CodeParameterDeclarationExpression(GetType(System.AsyncCallback), "callback"))
                .Parameters.Add(New CodeParameterDeclarationExpression(GetType(Object), "asyncState"))

                invoke.Parameters.Add(New CodeSnippetExpression("callback"))
                invoke.Parameters.Add(New CodeSnippetExpression("asyncState"))

                .ReturnType = New CodeTypeReference(GetType(System.IAsyncResult))
                .Statements.Add(New CodeMethodReturnStatement(invoke))
            End With
            Return proxyMethod
        End Get
    End Property

    Private ReadOnly Property ProxyMethodsEnd(ByVal name As String, ByVal returnType As String) As CodeMemberMethod
        Get
            Dim proxyMethod As New CodeMemberMethod
            With proxyMethod
                .Name = "End" & name
                .Attributes = MemberAttributes.Public

                Dim invoke As New CodeMethodInvokeExpression
                invoke.Method = New CodeMethodReferenceExpression(New CodeThisReferenceExpression, "EndInvoke")
                invoke.Parameters.Add(New CodeSnippetExpression("""" & name & """"))

                Dim ObjectDeclare As New CodeVariableDeclarationStatement(GetType(Object()), "results")

                .Parameters.Add(New CodeParameterDeclarationExpression(GetType(System.IAsyncResult), "asyncResult"))

                Dim resultsVar As New CodeVariableDeclarationStatement(GetType(Object()), "results", invoke)
                .Statements.Add(resultsVar)

                .ReturnType = New CodeTypeReference(returnType)

                Dim returnStatement As New CodeCastExpression(.ReturnType, New CodeSnippetExpression("results(0)"))
                .Statements.Add(New CodeMethodReturnStatement(returnStatement))
            End With
            Return proxyMethod
        End Get
    End Property

    Private ReadOnly Property BaseConstructor() As CodeConstructor
        Get
            Dim left As CodeExpression = New CodeMethodReferenceExpression(New CodeThisReferenceExpression, "Url")
            Dim right As CodeExpression = New CodeSnippetExpression("""" & mWSDL.URL & """")
            Dim urlAssign As New CodeAssignStatement(left, right)

            Dim baseCons As New CodeConstructor
            With baseCons
                .Attributes = MemberAttributes.Public
                .Statements.Add(urlAssign)
            End With
            Return baseCons
        End Get
    End Property

    Private ReadOnly Property AttributeSoapDocumentMethod(ByVal soapAction As String) As CodeAttributeDeclaration
        Get
            Return New CodeAttributeDeclaration("System.Web.Services.Protocols.SoapDocumentMethodAttribute", _
                   New CodeAttributeArgument(New CodeSnippetExpression("""" & soapAction & """")), _
                   New CodeAttributeArgument("RequestNamespace", New CodeSnippetExpression("""" & mWSDL.Namespace & """")), _
                   New CodeAttributeArgument("ResponseNamespace", New CodeSnippetExpression("""" & mWSDL.Namespace & """")), _
                   New CodeAttributeArgument("Use", New CodeSnippetExpression("System.Web.Services.Description.SoapBindingUse.Literal")), _
                   New CodeAttributeArgument("ParameterStyle", New CodeSnippetExpression("System.Web.Services.Protocols.SoapParameterStyle.Wrapped")))
        End Get
    End Property

    Private ReadOnly Property AttributeXMLInclude(ByVal includeType As String) As CodeAttributeDeclaration
        Get
            Return New CodeAttributeDeclaration("System.Xml.Serialization.XmlIncludeAttribute", New CodeAttributeArgument(New CodeSnippetExpression("GetType(" & includeType & ")")))
        End Get
    End Property

    Private ReadOnly Property AttributeSerializable() As CodeAttributeDeclaration
        Get
            Return New CodeAttributeDeclaration("System.SerializableAttribute")
        End Get
    End Property

    Private ReadOnly Property AttributeXMLType() As CodeAttributeDeclaration
        Get
            Return New CodeAttributeDeclaration("System.Xml.Serialization.XmlTypeAttribute", New CodeAttributeArgument("Namespace", New CodeSnippetExpression("""" & mWSDL.Namespace & """")))
        End Get
    End Property

    Private ReadOnly Property AttributeDebugger() As CodeAttributeDeclaration
        Get
            Return New CodeAttributeDeclaration("System.Diagnostics.DebuggerStepThroughAttribute")
        End Get
    End Property

    Private ReadOnly Property AttributeDesigner() As CodeAttributeDeclaration
        Get
            Return New CodeAttributeDeclaration("System.ComponentModel.DesignerCategoryAttribute", New CodeAttributeArgument(New CodeSnippetExpression("""code""")))
        End Get
    End Property

    Private ReadOnly Property AttributeWSBinding() As CodeAttributeDeclaration
        Get
            Return New CodeAttributeDeclaration("System.Web.Services.WebServiceBindingAttribute", New CodeAttributeArgument("Name", New CodeSnippetExpression("""" & mWSDL.BindingName & """")), New CodeAttributeArgument("Namespace", New CodeSnippetExpression("""" & mWSDL.Namespace & """")))
        End Get
    End Property

    Private Sub SetVBProvider()
        mProvider = New VBCodeProvider
    End Sub

    ' Generate - generate some code...
    Protected Function Generate(ByVal unit As CodeCompileUnit) As String

        ' create...
        Dim generator As ICodeGenerator = mProvider.CreateGenerator()

        ' run...
        Dim codeString As String = Generate(generator, unit)

        ' return...
        Return codeString

    End Function

    Protected Function Generate(ByVal generator As ICodeGenerator, ByVal unit As CodeCompileUnit) As String

        ' stream...
        Dim stream As New MemoryStream
        Dim writer As New StreamWriter(stream)

        ' compile...
        'Dim unit As CodeCompileUnit = CreateCompileUnit()

        ' run it...
        generator.GenerateCodeFromCompileUnit(unit, writer, CreateOptions())

        ' reset...
        writer.Flush()
        stream.Seek(0, SeekOrigin.Begin)
        Dim reader As New StreamReader(stream)
        Dim codeString As String = reader.ReadToEnd()
        stream.Close()

        ' return...
        Return codeString

    End Function

    ' CreateCompileUnit - create a compile unit from the code...
    'Public Function CreateCompileUnit() As CodeCompileUnit

    '    ' create...
    '    Dim unit As New CodeCompileUnit

    '    ' namespace...
    '    unit.Namespaces.Add(CreateNamespace())

    '    ' return...
    '    Return unit

    'End Function

    '' CreateNamespace - create a namespace...
    'Public Function CreateNamespace() As CodeNamespace
    '    Dim nsName As String = RootNamespace
    '    If nsName Is Nothing Then nsName = String.Empty

    '    ' create...
    '    Dim ns As New CodeNamespace(nsName)

    '    ' add...
    '    ns.Types.Add(mType)
    '    ' return...
    '    Return ns

    'End Function

    ' CreateOptions - create compile options...
    Protected Function CreateOptions() As CodeGeneratorOptions

        ' create...
        Dim options As New CodeGeneratorOptions

        ' normal..
        options.BracingStyle = "C"

        options.BlankLinesBetweenMembers = True

        'options.

        ' return...
        Return options

    End Function

    Private Sub GetDefaultsFrombase(ByRef Constructor As CodeConstructor, ByVal baseName As String, ByVal complexName As String)
        For Each BaseComplexType As ComplexType In mWSDL.ComplexTypes
            If BaseComplexType.Name = baseName Then
                If Not BaseComplexType.Members Is Nothing Then
                    For Each member As OperationArgument In BaseComplexType.Members
                        Try
                            Dim InvokeMember As New CodeMemberField(member.Type, "m" & member.Name)
                            Dim DefValue As Object = DefaultProxy.GetDefaults(complexName, member.Name)
                            If Not DefValue Is Nothing Then
                                InvokeMember.InitExpression = New CodeSnippetExpression(DefValue)
                                Constructor.Statements.Add(New CodeAssignStatement(New CodePropertyReferenceExpression(New CodeBaseReferenceExpression, member.Name), New CodeSnippetExpression(DefValue)))
                            End If
                        Catch
                        End Try
                    Next member
                End If
                If Not BaseComplexType.Base Is Nothing AndAlso _
                   BaseComplexType.Base.ToString.Length > 0 Then
                    GetDefaultsFrombase(Constructor, BaseComplexType.Base, complexName)
                End If
                Exit For
            End If
        Next BaseComplexType
    End Sub
End Class

<System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Web.Services.WebServiceBindingAttribute(Name:="__SystemSoap", Namespace:="http://www.defra.gov.uk")> _
Public Class Proxy
    Inherits System.Web.Services.Protocols.SoapHttpClientProtocol

    Public Sub New(ByVal url As String)
        Me.Url = url
    End Sub

    Public Sub New()
        MyBase.Url = "http://mydevphoenixwebservice:10080/_System.asmx"
    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.defra.gov.uk/GetDefaults")> _
    Public Function GetDefaults(ByVal className As String, ByVal propertyName As String) As Object
        Dim results() As Object = Me.Invoke("GetDefaults", New Object() {className, propertyName})
        Return results(0)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.defra.gov.uk/GetClassHeader")> _
    Public Function GetClassHeader(ByVal className As String) As Object
        Dim results() As Object = Me.Invoke("GetClassHeader", New Object() {className})
        Return results(0)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.defra.gov.uk/GetEnumValues")> _
    Public Function GetEnumValues(ByVal enumName As String, ByVal element As String, ByVal ignoreClassName As String) As Object
        Dim results() As Object = Me.Invoke("GetEnumValues", New Object() {enumName, element, ignoreClassName})
        Return results(0)
    End Function
End Class
