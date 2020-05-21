Imports System.Xml

Namespace Application
    Public Class ApplicationUtils
        Public Shared Function GetInfoFromBirdRegistrationXSD(ByVal infoType As BirdRegistrationXSDType) As Object
            Dim RetValue As Object = Nothing
            'work out where the information is stored
            Dim Storage As Collections.Queue = GetCollectionForBirdRegXSD(infoType)

            Dim StreamReader As System.IO.Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("uk.gov.defra.Phoenix.BO.BirdRegistration.xsd")
            Dim myXmlReader As New System.Xml.XmlTextReader(StreamReader, Xml.XmlNodeType.Document, Nothing)
            Dim vr As New System.Xml.XmlValidatingReader(myXmlReader)
            vr.Schemas.Add("", myXmlReader)
            Dim StartElement As Schema.XmlSchemaElement = CType(vr.Schemas.Item("").Items(0), Schema.XmlSchemaElement)
            Dim CurrentItem As Schema.XmlSchemaObject = StartElement
            Do
                If Storage.Count = 0 OrElse CurrentItem Is Nothing Then Exit Do

                Dim XMLItem As XMLInfo = CType(Storage.Dequeue(), XMLInfo)
                Select Case XMLItem.NodeType
                    Case NodeTypes.ComplexType
                        If TypeOf CurrentItem Is Schema.XmlSchemaElement Then
                            CurrentItem = CType(CType(CurrentItem, Schema.XmlSchemaElement).ElementType, Schema.XmlSchemaComplexType)
                        ElseIf TypeOf CurrentItem Is Schema.XmlSchemaChoice Then
                            Dim SC As Schema.XmlSchemaChoice = CType(CurrentItem, Schema.XmlSchemaChoice)
                            For Each Item As Schema.XmlSchemaObject In SC.Items
                                If TypeOf Item Is Schema.XmlSchemaComplexType AndAlso String.Compare(CType(item, Schema.XmlSchemaComplexType).Name, XMLItem.Name, True) = 0 Then
                                    CurrentItem = CType(Item, Schema.XmlSchemaComplexType)
                                    Exit For
                                End If
                            Next Item
                            CurrentItem = Nothing
                        End If
                    Case NodeTypes.Sequence
                        If TypeOf CurrentItem Is Schema.XmlSchemaComplexType Then
                            CurrentItem = CType(CType(CurrentItem, Schema.XmlSchemaComplexType).ContentTypeParticle, Schema.XmlSchemaGroupBase)
                        End If
                    Case NodeTypes.Choice
                        If TypeOf CurrentItem Is Schema.XmlSchemaGroupBase Then
                            Dim Seq As Schema.XmlSchemaGroupBase = CType(CurrentItem, Schema.XmlSchemaGroupBase)
                            For Each Item As Schema.XmlSchemaObject In Seq.Items
                                If TypeOf Item Is Schema.XmlSchemaChoice Then
                                    CurrentItem = CType(Item, Schema.XmlSchemaChoice)
                                    Exit For
                                End If
                                CurrentItem = Nothing
                            Next Item
                        ElseIf TypeOf CurrentItem Is Schema.XmlSchemaComplexType Then
                            CurrentItem = CType(CType(CurrentItem, Schema.XmlSchemaComplexType).ContentTypeParticle, Schema.XmlSchemaChoice)
                        End If
                    Case NodeTypes.Element
                        If TypeOf CurrentItem Is Schema.XmlSchemaGroupBase Then
                            Dim Seq As Schema.XmlSchemaGroupBase = CType(CurrentItem, Schema.XmlSchemaGroupBase)
                            For Each Item As Schema.XmlSchemaObject In Seq.Items
                                If TypeOf Item Is Schema.XmlSchemaElement AndAlso String.Compare(CType(item, Schema.XmlSchemaElement).Name, XMLItem.Name, True) = 0 Then
                                    CurrentItem = CType(Item, Schema.XmlSchemaElement)
                                    Exit For
                                End If
                                CurrentItem = Nothing
                            Next Item
                        End If
                End Select
                If Storage.Count = 0 AndAlso Not CurrentItem Is Nothing Then
                    If Not CurrentItem Is Nothing AndAlso XMLItem.RequiredValue <> RequiredValueTypes.none Then
                        Dim pi As Reflection.PropertyInfo = CurrentItem.GetType.GetProperty(XMLItem.RequiredValueString)
                        If XMLItem.RequiredValue = RequiredValueTypes.maxOccurs Then
                            Dim piString As Reflection.PropertyInfo = CurrentItem.GetType.GetProperty(XMLItem.RequiredValueString & "String")
                            If String.Compare(piString.GetValue(CurrentItem, Nothing).ToString, "unbounded", True) = 0 Then
                                RetValue = Int32.MaxValue
                                Exit Do
                            End If
                        End If
                        If Not pi Is Nothing Then
                            RetValue = pi.GetValue(CurrentItem, Nothing)
                            Exit Do
                        End If
                    End If
                    Exit Do
                End If
            Loop
            'close the streams
            vr.Close()
            myXmlReader.Close()
            StreamReader.Close()
            'no minOccurs is the same as 1, no maxOccurs is the same as unlimited
            Return RetValue
        End Function

        Public Enum BirdRegistrationXSDType
            EggRingMinOccurs
            EggRingMaxOccurs
            EggMinOccurs
            EggMaxOccurs

            AdultFoundSpecimenMinOccurs
            AdultFoundSpecimenMaxOccurs
            AdultFoundIdMarkMinOccurs
            AdultFoundIdMarkMaxOccurs
            AdultFoundRingsMinOccurs
            AdultFoundRingsMaxOccurs

            AdultImportedSpecimenMinOccurs
            AdultImportedSpecimenMaxOccurs
            AdultImportedIdMarkMinOccurs
            AdultImportedIdMarkMaxOccurs
            AdultImportedRingsMinOccurs
            AdultImportedRingsMaxOccurs

            AdultOtherSpecimenMinOccurs
            AdultOtherSpecimenMaxOccurs
            AdultOtherIdMarkMinOccurs
            AdultOtherIdMarkMaxOccurs
            AdultOtherRingsMinOccurs
            AdultOtherRingsMaxOccurs

            AdultBredSpecimenMinOccurs
            AdultBredSpecimenMaxOccurs
            AdultBredIdMarkMinOccurs
            AdultBredIdMarkMaxOccurs
            AdultBredRingsMinOccurs
            AdultBredRingsMaxOccurs

            AdultImportedAcquiredDateNillable
            LastLaidDateNillable

            MotherMinOccurs
            MotherMaxOccurs
            FatherMinOccurs
            FatherMaxOccurs

            MotherIdMarkMinOccurs
            MotherIdMarkMaxOccurs
            FatherIdMarkMinOccurs
            FatherIdMarkMaxOccurs
        End Enum

        Private Structure XMLInfo
            Friend Sub New(ByVal type As NodeTypes)
                NodeType = type
                RequiredValue = RequiredValueTypes.none
            End Sub

            Friend Sub New(ByVal type As NodeTypes, ByVal name As String)
                MyClass.New(type)
                Me.Name = name
            End Sub

            Friend Sub New(ByVal type As NodeTypes, ByVal name As String, ByVal requiredValue As RequiredValueTypes)
                MyClass.New(type, name)
                Me.RequiredValue = requiredValue
            End Sub

            Friend NodeType As NodeTypes
            Friend Name As String
            Friend RequiredValue As RequiredValueTypes

            Friend ReadOnly Property RequiredValueString() As String
                Get
                    If RequiredValue = RequiredValueTypes.none Then
                        Return String.Empty
                    Else
                        Select Case RequiredValue
                            Case RequiredValueTypes.maxOccurs
                                Return "MaxOccurs"
                            Case RequiredValueTypes.minOccurs
                                Return "MinOccurs"
                            Case RequiredValueTypes.nillable
                                Return "IsNillable"
                        End Select
                    End If
                End Get
            End Property
        End Structure

        Private Enum NodeTypes
            Element
            ComplexType
            Sequence
            Choice
        End Enum

        Private Enum RequiredValueTypes
            none
            minOccurs
            maxOccurs
            nillable
        End Enum

        Private Shared Function GetCollectionForBirdRegXSD(ByVal infoType As BirdRegistrationXSDType) As Collections.Queue
            Dim Storage As New Collections.Queue

            Select Case infoType
                Case BirdRegistrationXSDType.FatherIdMarkMaxOccurs, BirdRegistrationXSDType.FatherIdMarkMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Parents"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Father"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.FatherIdMarkMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "FatherIDMark", RequiredValue))
                Case BirdRegistrationXSDType.MotherIdMarkMaxOccurs, BirdRegistrationXSDType.MotherIdMarkMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Parents"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Mother"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.MotherIdMarkMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "MotherIDMark", RequiredValue))
                Case BirdRegistrationXSDType.MotherMaxOccurs, BirdRegistrationXSDType.MotherMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Parents"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.MotherMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Mother", RequiredValue))
                Case BirdRegistrationXSDType.FatherMaxOccurs, BirdRegistrationXSDType.FatherMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Parents"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.FatherMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Father", RequiredValue))
                Case BirdRegistrationXSDType.EggRingMaxOccurs, BirdRegistrationXSDType.EggRingMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Clutch"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Egg"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.EggRingMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "EggRing", RequiredValue))
                Case BirdRegistrationXSDType.EggMaxOccurs, BirdRegistrationXSDType.EggMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Clutch"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.EggMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Egg", RequiredValue))
                Case BirdRegistrationXSDType.AdultFoundSpecimenMaxOccurs, BirdRegistrationXSDType.AdultFoundSpecimenMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultFound"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultFoundSpecimenMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultFoundBird", RequiredValue))
                Case BirdRegistrationXSDType.AdultFoundIdMarkMaxOccurs, BirdRegistrationXSDType.AdultFoundIdMarkMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultFound"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultFoundBird"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultFoundIdMarkMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "FoundBirdIDMark", RequiredValue))
                Case BirdRegistrationXSDType.AdultFoundRingsMaxOccurs, BirdRegistrationXSDType.AdultFoundRingsMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultFound"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultFoundBird"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultFoundRingsMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "FoundRing", RequiredValue))
                Case BirdRegistrationXSDType.AdultImportedSpecimenMaxOccurs, BirdRegistrationXSDType.AdultImportedSpecimenMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultImported"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultImportedSpecimenMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultImportedBird", RequiredValue))
                Case BirdRegistrationXSDType.AdultImportedIdMarkMaxOccurs, BirdRegistrationXSDType.AdultImportedIdMarkMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultImported"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultImportedBird"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultImportedIdMarkMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "ImportedIDMarks", RequiredValue))
                Case BirdRegistrationXSDType.AdultImportedRingsMaxOccurs, BirdRegistrationXSDType.AdultImportedRingsMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultImported"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultImportedBird"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultImportedRingsMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "ImportedRing", RequiredValue))
                Case BirdRegistrationXSDType.AdultOtherSpecimenMaxOccurs, BirdRegistrationXSDType.AdultOtherSpecimenMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultOther"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultOtherSpecimenMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultOtherBird", RequiredValue))
                Case BirdRegistrationXSDType.AdultOtherIdMarkMaxOccurs, BirdRegistrationXSDType.AdultOtherIdMarkMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultOther"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultOtherBird"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultOtherIdMarkMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "OtherIDMarks", RequiredValue))
                Case BirdRegistrationXSDType.AdultOtherRingsMaxOccurs, BirdRegistrationXSDType.AdultOtherRingsMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultOther"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultOtherBird"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultOtherRingsMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "OtherRing", RequiredValue))
                Case BirdRegistrationXSDType.AdultImportedAcquiredDateNillable
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultImported"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultImportedBird"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "DateAcquired", RequiredValueTypes.nillable))
                Case BirdRegistrationXSDType.LastLaidDateNillable
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Clutch"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "LastLaidDate", RequiredValueTypes.nillable))
                Case BirdRegistrationXSDType.AdultBredSpecimenMaxOccurs, BirdRegistrationXSDType.AdultBredSpecimenMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultBred"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultBredSpecimenMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultBredBird", RequiredValue))
                Case BirdRegistrationXSDType.AdultBredIdMarkMaxOccurs, BirdRegistrationXSDType.AdultBredIdMarkMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultBred"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultBredBird"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultBredIdMarkMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "BredIDMarks", RequiredValue))
                Case BirdRegistrationXSDType.AdultBredRingsMaxOccurs, BirdRegistrationXSDType.AdultBredRingsMinOccurs
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "Adult"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Choice))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultBred"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "AdultBredBird"))
                    Storage.Enqueue(New XMLInfo(NodeTypes.ComplexType))
                    Storage.Enqueue(New XMLInfo(NodeTypes.Sequence))
                    Dim RequiredValue As RequiredValueTypes
                    If infoType = BirdRegistrationXSDType.AdultBredRingsMinOccurs Then
                        RequiredValue = RequiredValueTypes.minOccurs
                    Else
                        RequiredValue = RequiredValueTypes.maxOccurs
                    End If
                    Storage.Enqueue(New XMLInfo(NodeTypes.Element, "BredRing", RequiredValue))
            End Select

            Return Storage
        End Function
    End Class
End Namespace
