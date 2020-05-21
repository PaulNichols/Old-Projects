Public Class PhoenixSystem
    Private Const LOG_ON As Boolean = False

    Public Shared Function GetAttributes(ByVal className As String, ByVal propertyName As String) As String
        'temp************
        className = "uk.gov.defra.Phoenix.BO.ReferenceData.BOCountry"
        propertyName = "ISO2CountryCode"
        '****************

        'Get the Bo Assembly based on the BaseBo class which should normally exsist
        Dim BoClassType As Type = GetType(BaseBO).Assembly.GetType(className)

        'Get the EntityMapping attribut which will tell us which DO this BO relates to
        Dim BoEntityMappingAttribute As Object() = BoClassType.GetCustomAttributes(GetType(uk.gov.defra.Phoenix.BO.EntityMapping), True)

        If Not BoEntityMappingAttribute Is Nothing AndAlso BoEntityMappingAttribute.Length > 1 Then
            'If the BO was decorated with the EntityMapping attribute then find the required property 
            Dim DoProp As Reflection.PropertyInfo = CType(BoEntityMappingAttribute(0), uk.gov.defra.Phoenix.BO.EntityMapping).Entity.GetProperty(propertyName)
            If Not DoProp Is Nothing Then
                'if the Property was found then get the FieldSize attribute
                Dim DoFieldSizeAttribute As Object() = DoProp.GetCustomAttributes(GetType(uk.gov.defra.EnterpriseObjects.Attributes.FieldSize), True)

                If Not DoFieldSizeAttribute Is Nothing AndAlso DoFieldSizeAttribute.Length > 0 Then
                    'If the DO Property was decorated with the FieldSize attribute then find the field length (size)
                    Dim FieldSize As Int32 = CType(DoFieldSizeAttribute(0), uk.gov.defra.EnterpriseObjects.Attributes.FieldSize).Size
                    'create an XML String for the proxy builder giving it all the details 
                    'to decorate the generarted proxy code with the DatabaseInformation attribute
                    Dim ReturnXML As String = String.Format("<DatabaseInformation FieldLength=(0) FieldDescription=(1) AllowNulls=(2)></DatabaseInformation>", FieldSize)
                    Return ReturnXML
                End If
            End If
        End If
    End Function

    'TS replaced this with version that works below. 26/4/05
    'Public Shared Function GetEnumValues(ByVal enumName As String, ByVal element As String, ByVal ignoreAttributeName As String) As Object
    '    Dim Result As Object
    '    Dim BOAssembly As Reflection.Assembly = Reflection.Assembly.Load("uk.gov.defra.Phoenix.BO")
    '    For Each EnumType As Type In BOAssembly.GetTypes()
    '        If EnumType.IsEnum AndAlso EnumType.Name = enumName AndAlso _
    '            (EnumType.Attributes.GetNames(EnumType).IndexOf(EnumType.Attributes.GetNames(EnumType), ignoreAttributeName) = -1) Then

    '            Result = CType([Enum].Parse(EnumType, element, True), Int32)
    '            Exit For
    '        End If
    '    Next EnumType
    '    BOAssembly = Nothing


    '    'Select Case enumName
    '    '    Case GetType(uk.gov.defra.Phoenix.BO.Common).Name
    '    '        Result = CType(CType([Enum].Parse(GetType(uk.gov.defra.Phoenix.BO.Common.UserPermissions), element, True), uk.gov.defra.Phoenix.BO.Common.UserPermissions), Int32)
    '    '    Case GetType(uk.gov.defra.Phoenix.BO.Common.AssignedToList).Name
    '    '        Result = CType(CType([Enum].Parse(GetType(uk.gov.defra.Phoenix.BO.Common.AssignedToList), element, True), uk.gov.defra.Phoenix.BO.Common.AssignedToList), Int32)
    '    'End Select
    '    Return Result
    'End Function

    Public Function GetEnumValues(ByVal enumName As String, ByVal element As String, ByVal elementArr As String) As Object
        Dim Result As Object
        Dim BOAssembly As Reflection.Assembly = Reflection.Assembly.Load("uk.gov.defra.Phoenix.BO")
        For Each EnumType As Type In BOAssembly.GetTypes()

            'Check to see if it is an enum and is called the same
            If EnumType.IsEnum AndAlso EnumType.Name = enumName Then
                Dim EnumAttributesLocal As String() = EnumType.Attributes.GetNames(EnumType)
                'Return NamesArr
                Dim EnumAttributes As String() = elementArr.Split(":"c)
                If CheckEnum(EnumAttributesLocal, EnumAttributes) Then
                    Result = CType([Enum].Parse(EnumType, element, True), Int32)
                    Exit For
                End If
            End If
        Next EnumType
        BOAssembly = Nothing

        'Reuturn a dummy string so the developer can see
        'somthing is wrong by looking at the proxy file.
        If Result Is Nothing Then
            Result = "ProxyGeneratorError"
        End If

        Return Result
    End Function

    Private Function CheckEnum(ByVal enumAttributesLocal As String(), ByVal enumAttributes As String()) As Boolean
        Dim Result As Boolean = True

        If enumAttributesLocal.Length = enumAttributes.Length Then
            Dim i As Int32
            For i = 0 To (enumAttributesLocal.Length - 1)
                If Not (enumAttributes(i) = enumAttributesLocal(i)) Then
                    Result = False 'this is not the enum we're interested in
                    Exit For
                End If
            Next i
        Else
            Result = False
        End If

        Return Result
    End Function

    Public Shared Function GetDefaults(ByVal className As String, ByVal propertyName As String) As Object
        'NOTE: If you return a string, remember to put quotes around the return value.
        LogOutput(className)
        LogOutput(GetType(BO.Party.Note).ToString())
        Select Case className
            Case GetClassName(GetType(BO.Application.Search.ApplicationSearchCriteriaCommon_Customer))
                Select Case propertyName
                    Case "IDMarkType"
                        Return "new BOIDMarkType"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Application.cites.BONotificationSpecie))
                Select Case propertyName
                    Case "Derivative"
                        Return "New BOCITESDerivative"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Application.CITES.Applications.BOCITESArticle10Permit))
                Select Case propertyName
                    Case "MemberStateOfImport"
                        Return "New BOCountry"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
                'Case GetClassName(GetType(BO.Application.Search.ApplicationSearchCriteriaDetailed))
                '    Select Case propertyName
                '        Case "DateApplicationReceived"
                '            Return "new DateRange"
                '        Case Else
                '            ThrowNoPropertyError(propertyName)
                '    End Select
                'Case GetClassName(GetType(BO.Application.Search.ApplicationSearchCriteriaDetailed_Customer))
                '    Select Case propertyName
                '        Case "IDMarkType"
                '            Return "new BOIDMarkType"
                '        Case Else
                '            ThrowNoPropertyError(propertyName)
                '    End Select
            Case GetClassName(GetType(BO.Application.CITES.ImportNotification.BOImportSpecie))
                Select Case propertyName
                    Case "ExportNumberDateOfIssue"
                        Return "Date.Today"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Application.CITES.Applications.BOImportApplication))
                Select Case propertyName
                    Case "CountryOfExport"
                        Return "New BOCountry"
                    Case "CountryOfImport"
                        Return "New BOCountry"
                    Case "LocationAddress"
                        Return "New BOReadOnlyAddress"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Application.BOSpecimen))
                Select Case propertyName
                    Case "UOM"
                        Return "New BOMeasurement"
                    Case "Fate"
                        Return "New BOSpecimenFate"
                    Case "ExactDOB"
                        Return "True"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Application.BOSpecimenMark))
                Select Case propertyName
                    Case "IdMarkType"
                        Return "New BOIDMarkType"
                    Case "Location"
                        Return "New BOIDMarkLocation"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Application.CITES.BOCITESPermit))
                Select Case propertyName
                    Case "AuthorityLocation"
                        Return "New BOCountry"
                    Case "Derivative"
                        Return "New BOCITESDerivative"
                    Case "Derivative"
                        Return "New BOCITESDerivative"
                    Case "Purpose"
                        Return "New BOCITESPurpose"
                    Case "Source1"
                        Return "New BOCITESSource"
                    Case "Source2"
                        Return "New BOCITESSource"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Application.BOPermit))
                Select Case propertyName
                    Case "CountryOfOrigin"
                        Return "New BOCountry"
                    Case "Specie"
                        Return "New BOSpecie"
                    Case "NumberOfCopies"
                        Return "1"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Application.BOApplicationPartyDetails))
                Select Case propertyName
                    Case "Address"
                        Return "New BOReadOnlyAddress"
                    Case "Party"
                        Return "New BOParty"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(Stamp))
                Select Case propertyName
                    Case "User"
                        Return "new BOAuthorisedUser"
                End Select

            Case GetClassName(GetType(BO.Application.BOApplication))
                Select Case propertyName
                    Case "CreatedInfo"
                        Return "new Stamp"
                End Select
            Case GetClassName(GetType(BO.Application.CITES.BOCITESNotification))
                Select Case propertyName
                    Case "NotificationDate"
                        Return "Date.Today"
                    Case "Active"
                        Return "True"
                    Case "CountryOfExport"
                        Return "New BOCountry"
                    Case "CountryOfImport"
                        Return "New BOCountry"
                    Case "CountryOfOrigin"
                        Return "New BOCountry"
                    Case "Party"
                        Return "New BOApplicationPartyDetails"
                    Case "Specie"
                        Return "New BONotificationSpecie() {}"
                    Case "UOM"
                        Return "New BOMeasurement"
                    Case "Derivative"
                        Return "New BOCITESDerivative"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Application.CITES.Seizurenotification.BOSeizureNotification))
                Select Case propertyName
                    'Case "SingleSpecie"
                    '    Return "New BONotificationSpecie"
                Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.BOMeasurement))
                Select Case propertyName
                    Case "UOM"
                        Return "New BOUnitOfMeasurement"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Party.BOPerson))
                Select Case propertyName
                    Case "IsBusiness"
                        Return True
                    Case "BusinessTypeId"
                        Return GetDefaultBusinessType(PropertyTypeEnum.BusinessTypeId)
                    Case "BusinessType"
                        Return GetDefaultBusinessType(PropertyTypeEnum.BusinessType)
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Party.BOPerson))
                Select Case propertyName
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.application.BOApplication))
                Select Case propertyName
                    Case "DateOfApplication"
                        Return "Date.Today"
                End Select
            Case GetClassName(GetType(BO.Party.BOPartyIndividual))
                Select Case propertyName
                    Case "IsBusiness"
                        Return False
                    Case "DOB"
                        'Return "New DateTime(1970, 1, 1)"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(Party.Note))
                Select Case propertyName
                    'Case "CreatedInfo"
                    '    Return "New PartyNotes.Stamp : MyBase.CreatedInfo.Date = Date.Now"
                    'Case "ModifiedInfo"
                    '    Return "New PartyNotes.Stamp"
                Case "CreatedDate"
                        Return "Date.Now"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Party.BOContact))
                Select Case propertyName
                    Case "Active"
                        Return True
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(BO.Party.BOAddress))
                Select Case propertyName
                    Case "Active"
                        Return "True"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(Application.CITES.Applications.BOCITESApplication))
                Select Case propertyName
                    Case "CreatedInfo"
                        Return "new Stamp"
                    Case "AdditionalDeclaration"
                        Return "New BOAdditionalDeclaration"
                    Case "CountryOfImport"
                        Return "New BOCountry"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(Application.CITES.Applications.BOImportExportApplication))
                Select Case propertyName
                    Case "RegionOfImport", "RegionOfExport"
                        Return "new BOUKCountry"
                    Case "CountryOfImport", "CountryOfExport"
                        Return "new BOCountry"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case GetClassName(GetType(Application.CITES.Applications.BOCITESImportExportPermit))
                Select Case propertyName
                    Case "CofLExport"
                        Return "new BOCountry"
                    Case Else
                        ThrowNoPropertyError(propertyName)
                End Select
            Case Else
                Throw New NotImplementedException("Class: " & className)
        End Select
        Return Nothing
    End Function


    Public Shared Function GetClassHeader(ByVal className As String) As Object
        Dim RC As String = Nothing
        Select Case className
            Case GetClassName(GetType(BO.Application.BOApplication))
                RC = LoadText("BOCITESApplication.txt")
            Case GetClassName(GetType(BO.Application.CITES.Applications.BOCITESArticle10))
                RC = LoadText("BOCITESArticle10.txt")
                'Case GetClassName(GetType(BO.Application.CITES.Applications.BOCITESArticle30))
                '    RC = LoadText("BOCITESArticle30.txt")
            Case GetClassName(GetType(BO.Application.Bird.Registration.SpecimenType))
                RC = LoadText("SpecimenType.txt")
        End Select
        Return RC
    End Function

    Private Shared Function LoadText(ByVal fileName As String) As String
        Dim FullFileName As String = "C:\Web\devPhoenixWebService\ClassHeader\" & fileName
        If IO.File.Exists(FullFileName) Then
            Dim sr As New IO.StreamReader(FullFileName)
            Dim Contents As String = sr.ReadToEnd()
            sr.Close()
            sr = Nothing
            Return Contents
        Else
            Return Nothing
        End If
    End Function

    Private Shared Sub ThrowNoPropertyError(ByVal propertyName As String)
        Throw New NotImplementedException("Property: " & propertyName)
    End Sub

    Private Shared Function GetClassName(ByVal [class] As Type) As String
        Try
            Dim Elements As String() = [class].ToString.Split("."c)
            Return Elements(Elements.GetUpperBound(0))
        Catch
            Return String.Empty
        End Try
    End Function

    Private Enum PropertyTypeEnum
        BusinessTypeId
        BusinessType
    End Enum

    Private Shared Function GetDefaultBusinessType(ByVal propertyType As PropertyTypeEnum) As Object
        Dim Types As DataObjects.Collection.BusinessTypeBoundCollection = ReferenceData.Lists.GetBusinessTypes(False)
        For Each busType As DataObjects.Entity.BusinessType In Types
            If busType.Default Then
                If propertyType = PropertyTypeEnum.BusinessTypeId Then
                    Return busType.Id
                ElseIf propertyType = PropertyTypeEnum.BusinessType Then
                    Return String.Concat("""", busType.Description, """")
                Else
                    Throw New System.Exception
                End If
            End If
        Next busType
    End Function

    Private Shared Sub LogOutput(ByVal output As String)
        If LOG_ON Then
            Dim FileName As String = "c:\temp\_SystemPhoenixLog.txt"
            Dim io As New io.StreamWriter(FileName, True)
            io.WriteLine(Date.Today.ToString & ": " & output)
            io.Close()
            io = Nothing
        End If
    End Sub

End Class
