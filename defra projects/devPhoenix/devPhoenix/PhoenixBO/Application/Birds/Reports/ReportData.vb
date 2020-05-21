Imports System.xml
Imports uk.gov.defra.Phoenix.BO.Party
Imports uk.gov.defra.Phoenix.BO.Application.Bird.Registration

Namespace Application.Bird.Reports

    <Serializable()> _
    Public Class ReportData
        Inherits BaseBO

        Public Sub New()
        End Sub

        Public Shared Function GetBirdRegDocReportData(ByVal applicationId As Int32, ByVal specimenId As Int32, ByVal schema As String) As ReportDataResults

            Dim nl As String = nl

            ' Get Business Objects
            Dim birdReg As BirdRegistration = New BirdRegistration(applicationId)
            Dim party As BOParty = BOParty.PolymorphicCreate(birdReg.PartyId)
            Dim defraId As String = party.AuthorisedPartyId.ToString()
            Dim address As BOAddress = party.GetMailingAddress(Nothing)            
            Dim adultSpecimenType As adultSpecimenType = birdReg.GetSpecimen(specimenId).SpecimenType
            Dim specimenInfo As SpecimenType = adultSpecimenType.SpecimenType

            birdReg.Parents.Father(0).GetMostPermanentMark()

            ' Get Licence Number
            Dim licenceNumber As String = birdReg.GetSpecimen(specimenId).EULicenseNumber

            'Get Specimen Info
            Dim dateHatched As String = ""
            If Not specimenInfo.HatchDate Is Nothing Then
                dateHatched = CType(specimenInfo.HatchDate, Date).ToString("dd/MM/yyyy")
            End If
            Dim pchSpecies As String = specimenInfo.ScientificName
            Dim commonName As String = specimenInfo.CommonName
            Dim sex As String = specimenInfo.GetGender

            ' Get Perents Id Mark Type
            Dim maleParentIdMarkType As String = birdReg.GetSpecimen(specimenId).MostPermFatherMark
            Dim femaleParentIdMarkType As String = birdReg.GetSpecimen(specimenId).MostPermMotherMark

            ' Get KeeperDetails
            Dim keeperDetails As String = "Defra Id: " & defraId.ToString & nl & party.DisplayName & nl & address.ReportAddress

            ' Get KeptAddress if different from Keeper Address
            Dim keptAddress As String = birdReg.KeptAddress(specimenId)
            If keptAddress = address.ReportAddress Then
                keptAddress = ""
            Else
                keptAddress = "Defra Id: " & defraId.ToString & nl & party.DisplayName & nl & keptAddress
            End If

            ' Get IdMarks & Type in permanence order
            Dim idMarksTypes As String = ""
            Dim newLine As String = ""
            Dim idMarks() As idMark = adultSpecimenType.AllMarks
            Dim pchIdMarkType As String = ""
            If idMarks.Length > 0 Then pchIdMarkType = idMarks(0).GetReportDescription
            For Each idMark As idMark In idMarks
                idMarksTypes = idMarksTypes & newLine & idMark.GetReportDescription
                newLine = nl
            Next

            Dim documentNo As String = specimenInfo.RegistrationDocumentReference 'birdReg.ApplicationId.ToString & "/" & birdReg.GetSpecimen(specimenId).SpecimenNumber.ToString("00")

            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim searchReference As String = ""

            ' BOBirdRegDoc_Main - Row 0
            Dim newRow As DataRow = returnDS.Tables("BOBirdRegDoc_Main").NewRow()
            With newRow
                .Item("ApplicationId") = 1 ' Hard Coded
                .Item("Barcode") = GetBarcode()
                .Item("IssueDate") = Date.Now.ToString("dd/MM/yyyy")
                .Item("DocumentNo") = documentNo
                .Item("PreviousDocumentNo") = ""

                .Item("KeeperDetails") = keeperDetails
                '.Item("KeeperDetails") = "Defra Id: 12345" & nl & _
                '"Mr G Figoni" & nl & _
                '"Government Buildings" & nl & _
                '"Epsom road" & nl & _
                '"Guildford" & nl & _
                '"Surrey" & nl & _
                '"GU1 2LD"

                .Item("KeptAddress") = keptAddress
                '.Item("KeptAddress") = "Defra Id: 12345" & nl & _
                '"Mr G Figoni" & nl & _
                '"Government Buildings" & nl & _
                '"Epsom road" & nl & _
                '"Guildford" & nl & _
                '"Surrey" & nl & _
                '"GU1 2LD"

                .Item("IdMarksTypes") = idMarksTypes
                '.Item("IdMarksTypes") = "ID Mark & Type: DEFRA CLOSED RING 9876A" & nl & _
                '"ID Mark & Type: SPLIT RING B3535" & nl & _
                '"ID Mark & Type: SWISS RING 1F45450"

                .Item("Species") = pchSpecies '"AVICEDA MADAGASCARIENSIS"
                .Item("CommonName") = commonName '"MADAGASCAR CUCKOO-FALCON"
                .Item("Origin") = "IMPORTED"
                .Item("DateHatched") = dateHatched  '"01/07/1999"
                .Item("DateAcquired") = "03/04/2004"
                .Item("Sex") = sex '"M"
                .Item("LicenceNumber") = licenceNumber
                .Item("MaleParentIdMarkType") = maleParentIdMarkType '"CLOSED RING 1F5569"
                .Item("FemaleParentIdMarkType") = femaleParentIdMarkType '"SPLIT RING CD1296"
                .Item("ShowRegulation8") = True
                .Item("PchIdMarkType") = pchIdMarkType '"DEFRA CLOSED RING 9876A"
                .Item("pchSpecies") = pchSpecies '"AVICEDA MADAGASCARIENSIS"
                .Item("PchCommonName") = commonName '"MADAGASCAR CUCKOO-FALCON"
                .Item("PchPresentKeeper") = "Mr G. Figoni"
                .Item("PchIdNo") = "12345"
            End With
            returnDS.Tables("BOBirdRegDoc_Main").Rows.Add(newRow)
            returnDS.AcceptChanges()

            Return New ReportDataResults(returnDS, searchReference)


        End Function

        Public Shared Function GetTestBirdRegDocReportData(ByVal applicationId As Int32, ByVal specimenId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim searchReference As String = ""

            ' BOBirdRegDoc_Main - Row 0
            Dim newRow As DataRow = returnDS.Tables("BOBirdRegDoc_Main").NewRow()
            With newRow
                .Item("ApplicationId") = 1
                .Item("Barcode") = "Barcode"
                .Item("IssueDate") = "12/04/2004"
                .Item("DocumentNo") = "123456/00"
                .Item("PreviousDocumentNo") = ""

                .Item("KeeperDetails") = "Defra Id: 12345" & Environment.NewLine & _
                "Mr G Figoni" & Environment.NewLine & _
                "Government Buildings" & Environment.NewLine & _
                "Epsom road" & Environment.NewLine & _
                "Guildford" & Environment.NewLine & _
                "Surrey" & Environment.NewLine & _
                "GU1 2LD"

                .Item("KeptAddress") = "Defra Id: 12345" & Environment.NewLine & _
                "Mr G Figoni" & Environment.NewLine & _
                "Government Buildings" & Environment.NewLine & _
                "Epsom road" & Environment.NewLine & _
                "Guildford" & Environment.NewLine & _
                "Surrey" & Environment.NewLine & _
                "GU1 2LD"

                .Item("IdMarksTypes") = "ID Mark & Type: DEFRA CLOSED RING 9876A" & Environment.NewLine & _
                "ID Mark & Type: SPLIT RING B3535" & Environment.NewLine & _
                "ID Mark & Type: SWISS RING 1F45450"

                .Item("Species") = "AVICEDA MADAGASCARIENSIS"
                .Item("CommonName") = "MADAGASCAR CUCKOO-FALCON"
                .Item("Origin") = "IMPORTED"
                .Item("DateHatched") = "01/07/1999"
                .Item("DateAcquired") = "03/04/2004"
                .Item("Sex") = "MALE"
                .Item("LicenceNumber") = ""
                .Item("MaleParentIdMarkType") = "CLOSED RING 1F5569"
                .Item("FemaleParentIdMarkType") = "SPLIT RING CD1296"
                .Item("ShowRegulation8") = True
                .Item("PchIdMarkType") = "DEFRA CLOSED RING 9876A"
                .Item("pchSpecies") = "AVICEDA MADAGASCARIENSIS"
                .Item("PchCommonName") = "MADAGASCAR CUCKOO-FALCON"
                .Item("PchPresentKeeper") = "Mr G. Figoni"
                .Item("PchIdNo") = "12345"
            End With
            returnDS.Tables("BOBirdRegDoc_Main").Rows.Add(newRow)
            returnDS.AcceptChanges()

            Return New ReportDataResults(returnDS, searchReference)


        End Function


        Public Shared Function GetSchedule4ReplacementRingsReportData(ByVal ApplicationId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim searchReference As String = ""

            ' BOSchedule4ReplacementRings_MainRow - Row 0
            Dim newRow As DataRow = returnDS.Tables("BOSchedule4ReplacementRings_Main").NewRow()
            With newRow
                .Item("ApplicationId") = 1
                .Item("Barcode") = "Barcode"
                .Item("InspectionSection") = True
                .Item("KeeperDetails") = "Defra Id: 12345" & Environment.NewLine & _
                "Mr G Figoni" & Environment.NewLine & _
                "Government Buildings" & Environment.NewLine & _
                "Epsom road" & Environment.NewLine & _
                "Guildford" & Environment.NewLine & _
                "Surrey" & Environment.NewLine & _
                "GU1 2LD"
                .Item("ApplicationRef") = "100014"
                .Item("MaxSignatures") = 2
            End With
            returnDS.Tables("BOSchedule4ReplacementRings_Main").Rows.Add(newRow)
            returnDS.AcceptChanges()

            ' boSchedule4ReplacementRings_Sub1Row - Bird 1
            newRow = returnDS.Tables("BOSchedule4ReplacementRings_Sub1").NewRow()
            With newRow
                .Item("ApplicationId") = 1
                .Item("BirdNo") = "Bird 1"
                .Item("Species") = "AVICEDA MADAGASCARIENSIS"
                .Item("CommonName") = "MADAGASCAR CUCKOO-FALCON"

                .Item("KeptAddress") = ""
                .Item("DateHatched") = "01/07/1998"
                .Item("Sex") = "FEMALE"
                .Item("Origin") = "IMPORTED"
                .Item("DateAcquired") = "01/05/2003"

                .Item("RingNo1") = "0987B"
                .Item("RingNo2") = "C4646"
                .Item("RingNo3") = ""
                .Item("RingNo4") = ""
                .Item("RingNo5") = ""
                .Item("RingNo6") = ""
                .Item("RingNo7") = ""
                .Item("RingNo8") = ""
                .Item("RingNo9") = ""
                .Item("RingNo10") = ""

                .Item("IdMarkType1") = "DEFRA CLOSED RING 3515X"
                .Item("IdMarkType2") = "SWISS RING 1F89691"
                .Item("IdMarkType3") = "SWISS RING 1F35170"
                .Item("IdMarkType4") = "SPLIT RING E3219"
                .Item("IdMarkType5") = ""
                .Item("IdMarkType6") = ""
                .Item("IdMarkType7") = ""
                .Item("IdMarkType8") = ""
                .Item("IdMarkType9") = ""
                .Item("IdMarkType10") = ""

                .Item("IdMarkTypeFitted1X") = ""
                .Item("IdMarkTypeFitted2X") = "X"
                .Item("IdMarkTypeFitted3X") = "X"
                .Item("IdMarkTypeFitted4X") = "X"
                .Item("IdMarkTypeFitted5X") = ""
                .Item("IdMarkTypeFitted6X") = ""
                .Item("IdMarkTypeFitted7X") = ""
                .Item("IdMarkTypeFitted8X") = ""
                .Item("IdMarkTypeFitted9X") = ""
                .Item("IdMarkTypeFitted10X") = ""

                .Item("IdMarkTypeRemoved1X") = "X"
                .Item("IdMarkTypeRemoved2X") = ""
                .Item("IdMarkTypeRemoved3X") = ""
                .Item("IdMarkTypeRemoved4X") = ""
                .Item("IdMarkTypeRemoved5X") = ""
                .Item("IdMarkTypeRemoved6X") = ""
                .Item("IdMarkTypeRemoved7X") = ""
                .Item("IdMarkTypeRemoved8X") = ""
                .Item("IdMarkTypeRemoved9X") = ""
                .Item("IdMarkTypeRemoved10X") = ""
            End With
            returnDS.Tables("BOSchedule4ReplacementRings_Sub1").Rows.Add(newRow)
            returnDS.AcceptChanges()

            ' boSchedule4ReplacementRings_Sub1Row - Bird 2
            newRow = returnDS.Tables("BOSchedule4ReplacementRings_Sub1").NewRow()
            With newRow
                .Item("ApplicationId") = 1
                .Item("BirdNo") = "Bird 2"
                .Item("Species") = "FALCO PEREGRINUS"
                .Item("CommonName") = "PEREGRINE FALCON"

                .Item("KeptAddress") = ""
                .Item("DateHatched") = "09/02/2001"
                .Item("Sex") = "MALE"
                .Item("Origin") = "UNKNOWN"
                .Item("DateAcquired") = "15/04/2003"

                .Item("RingNo1") = "1952B"
                .Item("RingNo2") = "C3257"
                .Item("RingNo3") = ""
                .Item("RingNo4") = ""
                .Item("RingNo5") = ""
                .Item("RingNo6") = ""
                .Item("RingNo7") = ""
                .Item("RingNo8") = ""
                .Item("RingNo9") = ""
                .Item("RingNo10") = ""

                .Item("IdMarkType1") = "DEFRA CLOSED RING 4406X"
                .Item("IdMarkType2") = "SPLIT RING G4110"
                .Item("IdMarkType3") = ""
                .Item("IdMarkType4") = ""
                .Item("IdMarkType5") = ""
                .Item("IdMarkType6") = ""
                .Item("IdMarkType7") = ""
                .Item("IdMarkType8") = ""
                .Item("IdMarkType9") = ""
                .Item("IdMarkType10") = ""

                .Item("IdMarkTypeFitted1X") = ""
                .Item("IdMarkTypeFitted2X") = "X"
                .Item("IdMarkTypeFitted3X") = ""
                .Item("IdMarkTypeFitted4X") = ""
                .Item("IdMarkTypeFitted5X") = ""
                .Item("IdMarkTypeFitted6X") = ""
                .Item("IdMarkTypeFitted7X") = ""
                .Item("IdMarkTypeFitted8X") = ""
                .Item("IdMarkTypeFitted9X") = ""
                .Item("IdMarkTypeFitted10X") = ""

                .Item("IdMarkTypeRemoved1X") = "X"
                .Item("IdMarkTypeRemoved2X") = ""
                .Item("IdMarkTypeRemoved3X") = ""
                .Item("IdMarkTypeRemoved4X") = ""
                .Item("IdMarkTypeRemoved5X") = ""
                .Item("IdMarkTypeRemoved6X") = ""
                .Item("IdMarkTypeRemoved7X") = ""
                .Item("IdMarkTypeRemoved8X") = ""
                .Item("IdMarkTypeRemoved9X") = ""
                .Item("IdMarkTypeRemoved10X") = ""

            End With
            returnDS.Tables("BOSchedule4ReplacementRings_Sub1").Rows.Add(newRow)
            returnDS.AcceptChanges()

            ' boSchedule4InspectorSigRow - Birds 1 to 5
            newRow = returnDS.Tables("BOSchedule4InspectorSig").NewRow()
            With newRow
                .Item("ApplicationId") = 1
                .Item("BirdNo1") = "Bird 1"
                .Item("BirdNo2") = "Bird 2"
                .Item("BirdNo3") = ""
                .Item("BirdNo4") = ""
                .Item("BirdNo5") = ""
            End With
            returnDS.Tables("BOSchedule4InspectorSig").Rows.Add(newRow)
            returnDS.AcceptChanges()

            Return New ReportDataResults(returnDS, searchReference)


        End Function

        'something like
        Public Shared Function GetRegistrationRefusalData(ByVal permitIds() As Int32, ByVal schema As String) As ReportDataResults
            'Dim returnDS As DataSet = GetSchemaDataSet(schema)
            'Dim refusal As Refusal = New RegistrationRefusal(permitIds)
            'returnDS = refusal.GetReportData(returnDS) 
            'Return New ReportDataResults(returnDS, "whatever")
        End Function

        Public Shared Function GetSchedule4ImportedBirdsReportData(ByVal ApplicationId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim birdreg As New BirdRegistration(ApplicationId)
            Dim reference As String = birdreg.ApplicationId.ToString()
            Dim specimens() As AdultSpecimenType = GetSpecimens(birdreg)
            Dim index As Int32 = 0

            ' BOSchedule4ImportedBirds_Main - Row 0
            Dim newRow As DataRow = returnDS.Tables("BOSchedule4ImportedBirds_Main").NewRow()
            FillBasics(newRow, birdreg, specimens.Length)
            newRow.Item("ApplicationRef") = reference
            returnDS.Tables("BOSchedule4ImportedBirds_Main").Rows.Add(newRow)
            returnDS.AcceptChanges()

            Dim birdNos(specimens.Length - 1) As String
            For Each item As AdultSpecimenType In specimens
                Dim specimen As AdultImportedSpecimen = CType(item, AdultImportedSpecimen)
                Dim info As SpecimenType = specimen.SpecimenType
                Dim fromUk As Boolean = False
                If TypeOf specimen.IsFromUK Is Boolean Then
                    fromUk = CType(specimen.IsFromUK, Boolean)
                End If

                index += 1
                newRow = returnDS.Tables("BOSchedule4ImportedBirds_Sub1").NewRow()
                With newRow
                    .Item("ApplicationId") = 1
                    .Item("BirdNo") = "Bird " + index.ToString()
                    birdNos(index - 1) = "Bird " & index.ToString
                    .Item("Species") = info.ScientificName
                    .Item("CommonName") = info.CommonName
                    .Item("LicenceNumber") = specimen.EULicenseNumber
                    .Item("Article10CertNo") = info.Article10Reference
                    .Item("Sex") = info.Gender.ToString()
                    .Item("KeptAddress") = specimen.KeptAddress_Helper
                    .Item("ImportedOutsideEuYesX") = Resolve(Not (fromUk OrElse specimen.IsWithinEU))
                    .Item("ImportedOutsideEuNoX") = Resolve(fromUk OrElse specimen.IsWithinEU)
                    .Item("ImportedWithinEuYesX") = Resolve(specimen.IsWithinEU)
                    .Item("ImportedWithinEuNoX") = Resolve(Not specimen.IsWithinEU)
                    .Item("QuarantineAddress") = specimen.Address
                    .Item("QuarantineKeeper") = specimen.KeeperName
                    If specimen.EndDate.Ticks > 0 Then            'EndDate is a Date...
                        Dim quarantine As Date = CType(specimen.EndDate, Date)
                        .Item("QuarantineDay") = quarantine.Day.ToString()
                        .Item("QuarantineMonth") = quarantine.Month.ToString()
                        .Item("QuarantineYear") = quarantine.Year.ToString()
                    End If
                    If specimen.DateAcquired.Ticks > 0 Then  'but DateAcquired is an Object (?)
                        .Item("DayAcquired") = specimen.DateAcquired.Day.ToString()
                        .Item("MonthAcquired") = specimen.DateAcquired.Month.ToString()
                        .Item("YearAcquired") = specimen.DateAcquired.Year.ToString()
                    End If
                    If Not info.HatchDate Is Nothing Then           'and HatchDate is an Object (?)
                        Dim hatched As Date = CType(info.HatchDate, Date)
                        .Item("HatchDay") = hatched.Day.ToString()
                        .Item("HatchMonth") = hatched.Month.ToString()
                        .Item("HatchYear") = hatched.Year.ToString()
                    End If
                End With
                FillMicrochipEtc(newRow, specimen, "ClosedRing")
                FillRings(newRow, specimen)
                returnDS.Tables("BOSchedule4ImportedBirds_Sub1").Rows.Add(newRow)
                returnDS.AcceptChanges()
            Next
            AddSignatures(returnDS, specimens.Length, "BOSchedule4InspectorSig", birdNos)

            Return New ReportDataResults(returnDS, reference)
        End Function

        Private Shared Sub AddParents(ByRef returnDS As DataSet, ByVal specimens() As Parent, ByVal table As String, ByVal gender As String, ByVal colony As Boolean)
            If Not specimens Is Nothing Then
                Dim index As Int32 = 0
                For Each specimen As Parent In specimens
                    Dim newRow As DataRow = returnDS.Tables(table).NewRow()
                    index += 1
                    With newRow
                        .Item("ApplicationId") = 1
                        .Item("Gender") = gender + " " + index.ToString()
                        .Item("IdMarkType") = specimen.GetMostPermanentMark()
                        .Item("RegKeeperYes") = Resolve(specimen.OwnedByKeeper)
                        .Item("RegKeeperNo") = Resolve(Not specimen.OwnedByKeeper)
                        If colony Then
                            .Item("Species") = "COLONY"
                            .Item("CommonName") = ""
                        Else
                            .Item("Species") = specimen.ScientificName
                            .Item("CommonName") = specimen.CommonName
                        End If
                    End With
                    returnDS.Tables(table).Rows.Add(newRow)
                    returnDS.AcceptChanges()
                Next
            End If
        End Sub

        Private Shared Sub AddSignatures(ByRef returnDS As DataSet, ByVal count As Int32, ByVal table As String, ByVal birdNos() As String)
            For sigRow As Int32 = 0 To (count - 1) \ 5
                AddSignatures(returnDS, sigRow, count, "BOSchedule4InspectorSig", birdNos)
            Next
        End Sub

        Private Shared Sub AddSignatures(ByRef returnDS As DataSet, ByVal sigRow As Int32, ByVal count As Int32, ByVal table As String, ByVal birdNos() As String)
            Dim newRow As DataRow = returnDS.Tables(table).NewRow()
            newRow.Item("ApplicationId") = 1

            For i As Int32 = 1 To 5
                Dim itemNo As String = "BirdNo" + i.ToString()
                Dim birdNo As Int32 = sigRow * 5 + i
                If birdNo <= count Then
                    newRow.Item(itemNo) = birdNos(birdNo - 1)
                Else
                    newRow.Item(itemNo) = ""
                End If
            Next
            returnDS.Tables(table).Rows.Add(newRow)
            returnDS.AcceptChanges()
        End Sub

        Private Shared Sub AddEgg(ByRef returnDS As DataSet, ByVal egg As ClutchEgg, ByVal table As String, ByVal index As Int32, ByVal scientific As String, ByVal common As String)
            Dim newRow As DataRow = returnDS.Tables(table).NewRow()
            Dim numRings As Int32 = egg.Rings.Length
            With newRow
                .Item("ApplicationId") = 1
                .Item("BirdNo") = "Bird " + index.ToString()
                .Item("ExpectedSpecies") = scientific
                .Item("CommonName") = common
                .Item("KeptAddress") = ""
                For count As Int32 = 1 To 10
                    Dim ringNo As String = "RingNo" + count.ToString()
                    If count <= numRings Then
                        .Item(ringNo) = egg.Rings(count - 1).Mark
                    Else
                        .Item(ringNo) = ""
                    End If
                Next
            End With
            returnDS.Tables(table).Rows.Add(newRow)
            returnDS.AcceptChanges()
        End Sub

        Public Shared Function GetSchedule4ApplicantBredReportData(ByVal ApplicationId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim birdreg As New BirdRegistration(ApplicationId)
            Dim reference As String = birdreg.ApplicationId.ToString()
            Dim specimens() As AdultSpecimenType = GetSpecimens(birdreg)
            Dim index As Int32 = 0

            ' BOSchedule4ApplicantBred_MainRow - Row 0
            Dim newRow As DataRow = returnDS.Tables("BOSchedule4ApplicantBred_Main").NewRow()
            FillBasics(newRow, birdreg, specimens.Length)
            With newRow
                .Item("ApplicationRef") = reference
                .Item("NumOfEggs") = "4"
                .Item("LastEggLaid") = "07/04/2002"
            End With
            returnDS.Tables("BOSchedule4ApplicantBred_Main").Rows.Add(newRow)
            returnDS.AcceptChanges()

            AddParents(returnDS, birdreg.Parents.Father, "BOSchedule4ApplicantBred_Sub1", "Male", False)
            AddParents(returnDS, birdreg.Parents.Mother, "BOSchedule4ApplicantBred_Sub1", "Female", False)

            Dim birdNos(specimens.Length - 1) As String
            For Each item As AdultSpecimenType In specimens
                Dim specimen As AdultBredSpecimen = CType(item, AdultBredSpecimen)
                Dim info As SpecimenType = specimen.SpecimenType

                index += 1
                newRow = returnDS.Tables("BOSchedule4ApplicantBred_Sub2").NewRow()
                With newRow
                    .Item("ApplicationId") = 1
                    .Item("BirdNo") = "Bird " + index.ToString()
                    birdNos(index - 1) = "Bird " & index.ToString
                    .Item("Species") = info.ScientificName
                    .Item("CommonName") = info.CommonName
                    .Item("Sex") = info.Gender.ToString()
                    .Item("KeptAddress") = ""
                    If Not info.HatchDate Is Nothing Then           'HatchDate is an Object (?)
                        Dim hatched As Date = CType(info.HatchDate, Date)
                        .Item("HatchDay") = hatched.Day.ToString()
                        .Item("HatchMonth") = hatched.Month.ToString()
                        .Item("HatchYear") = hatched.Year.ToString()
                    End If
                End With
                FillMicrochipEtc(newRow, specimen, "ClosedRing")
                FillRings(newRow, specimen)
                returnDS.Tables("BOSchedule4ApplicantBred_Sub2").Rows.Add(newRow)
                returnDS.AcceptChanges()
            Next
            AddSignatures(returnDS, specimens.Length, "BOSchedule4InspectorSig", birdNos)

            Return New ReportDataResults(returnDS, reference)
        End Function

        Public Shared Function GetSchedule4FoundReportData(ByVal ApplicationId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim birdreg As New BirdRegistration(ApplicationId)
            Dim applic As AdultFound = CType(birdreg.RegistrationApplication, AdultFound)
            Dim reference As String = birdreg.ApplicationId.ToString()
            Dim specimens() As AdultSpecimenType = GetSpecimens(birdreg)
            Dim index As Int32 = 0

            ' BOSchedule4Found_Main - Row 0
            Dim newRow As DataRow = returnDS.Tables("BOSchedule4Found_Main").NewRow()
            FillBasics(newRow, birdreg, specimens.Length)
            newRow.Item("ApplicationRef") = reference
            returnDS.Tables("BOSchedule4Found_Main").Rows.Add(newRow)
            returnDS.AcceptChanges()

            Dim birdNos(specimens.Length - 1) As String
            For Each item As AdultSpecimenType In specimens
                Dim specimen As AdultFoundSpecimen = CType(item, AdultFoundSpecimen)
                Dim info As SpecimenType = specimen.SpecimenType

                index += 1
                newRow = returnDS.Tables("BOSchedule4Found_Sub1").NewRow()
                With newRow
                    ' BOSchedule4Birds_Sub1
                    .Item("ApplicationId") = 1
                    .Item("BirdNo") = "Bird " + index.ToString()
                    birdNos(index - 1) = "Bird " & index.ToString
                    .Item("Species") = info.ScientificName
                    .Item("CommonName") = info.CommonName
                    .Item("Age") = info.AgeStatus_Helper
                    .Item("Sex") = info.Gender.ToString()
                    .Item("KeptAddress") = specimen.KeptAddress_Helper
                    .Item("WildDisabledYes") = ""
                    .Item("WildDisabledNo") = ""
                    .Item("Possession") = specimen.AcquisitionDetails
                    .Item("Injuries") = specimen.InjuryDetails
                    .Item("RspcaYes") = Resolve(applic.IsRSPCAInspector)
                    .Item("RspcaNo") = Resolve(Not applic.IsRSPCAInspector)
                    .Item("VetYes") = Resolve(applic.IsVet)
                    .Item("VetNo") = Resolve(Not applic.IsVet)
                    .Item("NotifiedYes") = Resolve(applic.IsKeeperOfThree)
                    .Item("NotifiedNo") = Resolve(Not applic.IsKeeperOfThree)
                    If specimen.DateFound.Ticks > 0 Then            'DateFound is a Date...
                        Dim found As Date = CType(specimen.DateFound, Date)
                        .Item("DayIntoCare") = found.Day.ToString()
                        .Item("MonthIntoCare") = found.Month.ToString()
                        .Item("YearIntoCare") = found.Year.ToString()
                    End If
                    If specimen.DateAcquired.Ticks > 0 Then 'but DateAcquired is an Object (?)
                        .Item("DayAcquired") = specimen.DateAcquired.Day.ToString()
                        .Item("MonthAcquired") = specimen.DateAcquired.Month.ToString()
                        .Item("YearAcquired") = specimen.DateAcquired.Year.ToString()
                    End If
                End With
                FillMicrochipEtc(newRow, specimen, "CloseRingNo")
                FillRings(newRow, specimen)
                returnDS.Tables("BOSchedule4Found_Sub1").Rows.Add(newRow)
                returnDS.AcceptChanges()
            Next
            AddSignatures(returnDS, specimens.Length, "BOSchedule4InspectorSig", birdNos)

            Return New ReportDataResults(returnDS, reference)
        End Function

        Private Shared Function GetBarcode() As String 'to do
            Return "00"
        End Function

        Private Shared Function GetKeeperDetails(ByVal birdreg As BirdRegistration, ByRef partyId As String) As String
            Dim party As BOParty = BOParty.PolymorphicCreate(birdreg.PartyId)
            partyId = party.AuthorisedPartyId.ToString()
            Dim primaryAddress As New BOAddress(CType(party.MailingAddressId, Int32), Nothing)
            Return party.DisplayName & Environment.NewLine & primaryAddress.ReportAddress
        End Function

        Private Shared Function GetSpecimens(ByVal birdreg As BirdRegistration) As AdultSpecimenType()
            Dim specimens() As AdultSpecimenType = birdreg.RegistrationApplication.Specimens
            If specimens.Length = 0 Then
                Throw New Exception("No specimens found in Registration data")
            End If
            If specimens.Length > 15 Then
                Throw New Exception("More than 15 specimens found in Registration data")
            End If
            Return specimens
        End Function

        Private Shared Sub FillBasics(ByVal newRow As DataRow, ByVal birdreg As BirdRegistration, ByVal count As Int32)
            newRow.Item("MaxSignatures") = count
            FillBasics(newRow, birdreg)
        End Sub

        Private Shared Sub FillBasics(ByVal newRow As DataRow, ByVal birdreg As BirdRegistration)
            Dim partyId As String
            With newRow
                .Item("ApplicationId") = 1
                .Item("Barcode") = GetBarcode()
                .Item("InspectionSection") = birdreg.IsInspectionRequired
                .Item("KeeperDetails") = GetKeeperDetails(birdreg, partyId)
                .Item("PartyId") = partyId
                .Item("Convict5YearsYesX") = "" ' Resolve(birdreg.SpecialPenalty) ' - Leave Blank - User fills this in
                .Item("Convict5YearsNoX") = "" ' Resolve(Not birdreg.SpecialPenalty) ' - Leave Blank - User fills this in
                .Item("Convict3YearsYesX") = "" ' Resolve(birdreg.OtherAnimalOffence) ' - Leave Blank - User fills this in
                .Item("Convict3YearsNoX") = "" 'Resolve(Not birdreg.OtherAnimalOffence) ' - Leave Blank - User fills this in
            End With
        End Sub

        Private Shared Sub FillRings(ByRef newRow As DataRow, ByVal specimen As AdultSpecimenType)
            Dim i As Int32 = 1
            For Each ring As IDMark In specimen.Rings
                newRow.Item("RingNo" + i.ToString()) = ring.Mark
                i += 1
            Next
            While i <= 10
                newRow.Item("RingNo" + i.ToString()) = ""
                i += 1
            End While
        End Sub

        Private Shared Sub FillMicrochipEtc(ByRef newRow As DataRow, ByVal specimen As AdultSpecimenType, ByVal closedRing As String)
            Dim otherRing() As String = OtherRingData(specimen)
            With newRow
                .Item(closedRing) = ClosedRingNumber(specimen)
                .Item("OtherRingNo") = otherRing(0)
                .Item("OtherRingType") = otherRing(1)
                .Item("Microchip") = MicrochipNumber(specimen)
            End With
        End Sub

        Private Shared Function MicrochipNumber(ByVal specimen As AdultSpecimenType) As String
            For Each mark As IDMark In specimen.IDMarks
                If mark.MarkType_Helper.IndexOf("Microchip") >= 0 Then
                    Return mark.Mark
                End If
            Next
            Return ""
        End Function

        Private Shared Function ClosedRingNumber(ByVal specimen As AdultSpecimenType) As String
            Dim permanence As String = "Z1"     'higher than Z
            Dim ringNumber As String = ""
            For Each mark As IDMark In specimen.IDMarks
                If mark.MarkType_Helper.IndexOf("Closed") >= 0 Then
                    If mark.MarkTypePermanence < permanence Then
                        permanence = mark.MarkTypePermanence
                        ringNumber = mark.Mark
                    End If
                End If
            Next
            Return ringNumber
        End Function

        Private Shared Function OtherRingData(ByVal specimen As AdultSpecimenType) As String()  'OtherRing(0) is ringnum, OtherRing(1) is type
            Dim permanence As String = "Z1"     'higher than Z
            Dim result(1) As String

            result(0) = ""
            result(1) = ""
            For Each mark As IDMark In specimen.IDMarks
                If mark.MarkType_Helper.IndexOf("Ring") >= 0 AndAlso mark.MarkType_Helper.IndexOf("Closed") = 0 Then
                    If mark.MarkTypePermanence < permanence Then
                        permanence = mark.MarkTypePermanence
                        result(0) = mark.Mark
                        result(1) = mark.MarkType_Helper
                    End If
                End If
            Next
            Return result
        End Function

        Public Shared Function GetSchedule4BirdsReportData(ByVal applicationId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim birdreg As New BirdRegistration(applicationId)
            Dim details As AdultOther = CType(birdreg.RegistrationApplication, AdultOther)
            Dim reference As String = birdreg.ApplicationId.ToString()
            Dim specimens() As AdultSpecimenType = GetSpecimens(birdreg)
            Dim index As Int32 = 0

            ' BOSchedule4Birds_MainRow - Row 0
            Dim newRow As DataRow = returnDS.Tables("BOSchedule4Birds_Main").NewRow()
            FillBasics(newRow, birdreg, specimens.Length)
            newRow.Item("ApplicationRef") = reference
            returnDS.Tables("BOSchedule4Birds_Main").Rows.Add(newRow)
            returnDS.AcceptChanges()

            Dim birdNos(specimens.Length - 1) As String
            For Each item As AdultSpecimenType In specimens
                Dim specimen As AdultOtherSpecimen = CType(item, AdultOtherSpecimen)
                Dim info As SpecimenType = specimen.SpecimenType
                Dim method As String = specimen.AcquisitionMethod_Helper

                index += 1
                newRow = returnDS.Tables("BOSchedule4Birds_Sub1").NewRow()
                With newRow
                    ' BOSchedule4Birds_Sub1
                    .Item("ApplicationId") = 1
                    .Item("BirdNo") = "Bird " + index.ToString()
                    birdNos(index - 1) = "Bird " & index.ToString
                    .Item("Species") = info.ScientificName
                    .Item("CommonName") = info.CommonName
                    .Item("Sex") = info.Gender.ToString()
                    .Item("Article10CertNo") = info.Article10Reference
                    .Item("KeptAddress") = specimen.KeptAddress_Helper
                    .Item("SaleX") = Resolve(method = "Sale")
                    .Item("HireX") = Resolve(method = "Hire")
                    .Item("ExchangeX") = Resolve(method = "Exchange")
                    .Item("BarterX") = Resolve(method = "Barter")
                    .Item("LoanX") = Resolve(method = "Loan")
                    .Item("GiftX") = Resolve(method = "Gift")
                    .Item("ReturnX") = Resolve(method = "Return")
                    .Item("PreviousKeeperAddress") = specimen.PreviousKeeper
                    .Item("Possession") = specimen.AcquisitionDetails
                    If specimen.DateAcquired.Ticks > 0 Then  'DateAcquired is an Object (?)
                        .Item("DayAcquired") = specimen.DateAcquired.Day.ToString()
                        .Item("MonthAcquired") = specimen.DateAcquired.Month.ToString()
                        .Item("YearAcquired") = specimen.DateAcquired.Year.ToString()
                    End If
                    If Not info.HatchDate Is Nothing Then           'HatchDate is an Object (?)
                        Dim hatched As Date = CType(info.HatchDate, Date)
                        .Item("HatchDay") = hatched.Day.ToString()
                        .Item("HatchMonth") = hatched.Month.ToString()
                        .Item("HatchYear") = hatched.Year.ToString()
                    End If
                End With
                FillMicrochipEtc(newRow, specimen, "ClosedRing")
                FillRings(newRow, specimen)
                returnDS.Tables("BOSchedule4Birds_Sub1").Rows.Add(newRow)
                returnDS.AcceptChanges()
            Next
            AddSignatures(returnDS, specimens.Length, "BOSchedule4InspectorSig", birdNos)

            Return New ReportDataResults(returnDS, reference)
        End Function

        Private Shared Function AdditionalEggs(ByVal birdreg As BirdRegistration) As Int32
            Dim clutch As clutch = CType(birdreg.RegistrationApplication, clutch)
            Dim eggs() As ClutchEgg = clutch.Eggs

            Dim nonClonedEggs As Int32 = 0
            For Each egg As ClutchEgg In eggs
                If Not egg.Cloned Then nonClonedEggs += 1
            Next

            Return nonClonedEggs

        End Function

        Public Shared Function GetSchedule4ChicksReportData(ByVal ApplicationId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim birdreg As New BirdRegistration(ApplicationId)
            Dim reference As String = birdreg.ApplicationId.ToString()
            Dim clutch As clutch = CType(birdreg.RegistrationApplication, clutch)
            Dim eggs() As ClutchEgg = clutch.Eggs
            Dim index As Int32 = 0
            Dim colony As Boolean = birdreg.Parents.Mother.Length > 1
            Dim scientificName As String = ""
            Dim commonName As String = ""

            ' BOSchedule4Chicks_MainRow - Row 0
            Dim newRow As DataRow = returnDS.Tables("BOSchedule4Chicks_Main").NewRow()
            FillBasics(newRow, birdreg, eggs.Length)
            With newRow
                ' BOSchedule4Chicks_Main
                .Item("RingRequestRef") = reference
                .Item("NumOfEggs") = eggs.Length
                .Item("LastEggLaid") = ""
                Dim relatedRingApplicationId As Int32 = birdreg.RelatedRingApplicationId
                .Item("AdditionalRingRequestNo") = ""
                .Item("AdditionalEggs") = ""
                If relatedRingApplicationId <> 0 Then
                    .Item("AdditionalRingRequestNo") = relatedRingApplicationId.ToString
                    .Item("AdditionalEggs") = AdditionalEggs(birdreg)
                End If
                If Not clutch.LastLaidDate Is Nothing Then
                    .Item("LastEggLaid") = CType(clutch.LastLaidDate, Date).ToString("dd/MM/yyyy")
                End If
            End With
            returnDS.Tables("BOSchedule4Chicks_Main").Rows.Add(newRow)
            returnDS.AcceptChanges()

            AddParents(returnDS, birdreg.Parents.Father, "BOSchedule4Chicks_Sub1", "Male", colony)
            AddParents(returnDS, birdreg.Parents.Mother, "BOSchedule4Chicks_Sub1", "Female", colony)

            GetExpectedNames(birdreg, scientificName, commonName)
            Dim birdNos(-1) As String
            Dim pos As Int32 = -1
            For Each egg As ClutchEgg In eggs
                index += 1
                If egg.Rings.Length > 0 Then
                    pos += 1
                    ReDim Preserve birdNos(pos)
                    birdNos(pos) = "Bird " & index.ToString
                    AddEgg(returnDS, egg, "BOSchedule4Chicks_Sub2", index, scientificName, commonName)
                End If
            Next
            AddSignatures(returnDS, birdNos.Length, "BOSchedule4InspectorSig", birdNos)

            Return New ReportDataResults(returnDS, reference)
        End Function

        Public Shared Sub GetExpectedNames(ByVal birdreg As BirdRegistration, ByRef scientificName As String, ByRef commonName As String)
            Dim males As Hashtable = BuildHashtable(birdreg.Parents.Father)
            Dim females As Hashtable = BuildHashtable(birdreg.Parents.Mother)
            Dim maleNames() As String = ConcatenateNames(males)
            Dim femaleNames() As String = ConcatenateNames(females)
            If females.Count = 1 Then
                Select Case True
                    Case males.Count = 1
                        If maleNames(0) = femaleNames(0) Then
                            scientificName = femaleNames(0)
                            commonName = femaleNames(1)
                        Else
                            scientificName = femaleNames(0) + " x " + maleNames(0)
                            commonName = femaleNames(1) + " x " + maleNames(1)
                        End If
                    Case males.Count > 0
                        scientificName = femaleNames(0) + " x (" + maleNames(0) + ")"
                        commonName = femaleNames(1) + " x (" + maleNames(1) + ")"
                    Case Else
                        scientificName = femaleNames(0)
                        commonName = femaleNames(1)
                End Select
            End If
        End Sub

        Private Shared Function ConcatenateNames(ByVal table As Hashtable) As String()
            Dim results(1) As String
            results(0) = ""
            results(1) = ""
            For Each specimen As Parent In table.Values
                If (results(0).Length > 0) Then
                    results(0) += " or "
                    results(1) += " or "
                End If
                results(0) += specimen.ScientificName
                results(1) += specimen.CommonName
            Next
            Return results
        End Function

        Private Shared Function BuildHashtable(ByVal specimens() As Parent) As Hashtable
            Dim table As New Hashtable
            For Each specimen As Parent In specimens
                table.Item(specimen.ScientificName) = specimen
            Next
            Return table
        End Function

        Public Shared Function GetCertificatePermitCoverLetterData(ByVal applicationId As Int32, ByVal ssoUserId As Long, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim certificateRejection As New Application.Letter.Reports.CertificatePermitCover(applicationId, ssoUserId)
            Return certificateRejection.GetReportData(returnDS)
        End Function

        Public Shared Function GetCertificateRefusalLetterData(ByVal permitIds() As Int32, ByVal ssoUserId As Long, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim CertificateRefusal As New Application.Letter.Reports.Certificate(permitIds, ssoUserId)
            Return CertificateRefusal.GetReportData(returnDS)
        End Function

        Public Shared Function GetSemiCompleteReminderLetterData(ByVal permitIds() As Int32, ByVal ssoUserId As Long, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim SemiCompleteReminder As New Application.Letter.Reports.SemiCompleteReminder(permitIds, ssoUserId)
            Return SemiCompleteReminder.GetReportData(returnDS)
        End Function


        Public Shared Function GetRegistrationRefusalLetterData(ByVal applicationId As Int32, ByVal ssoUserId As Long, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim registration As New Application.Letter.Reports.Registration(applicationId, ssoUserId)
            Return registration.GetReportData(returnDS)
        End Function

        Public Shared Function GetPermitRefusalLetterData(ByVal permitIds() As Int32, ByVal ssoUserId As Long, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim PermitRefusal As New Application.Letter.Reports.Permit(permitIds, ssoUserId)
            Return PermitRefusal.GetReportData(returnDS)
        End Function

        Private Shared Function GetRegisteredInfo(ByVal partyId As Int32, ByVal currently As Boolean, ByVal previously As Boolean) As RegisteredInfo()
            Dim partySpecimens() As BOPartySpecimen = BOPartySpecimen.GetPartySpecimensByPartyId(partyId)
            Dim reginfos(partySpecimens.Length - 1) As RegisteredInfo
            Dim i As Int32 = 0

            If currently AndAlso Not previously Then
                partySpecimens = FilterSpecimens(partySpecimens, True)
            ElseIf Not currently AndAlso previously Then
                partySpecimens = FilterSpecimens(partySpecimens, False)
            End If
            For Each item As BOPartySpecimen In partySpecimens
                Dim specimen As New BOSpecimen(item.SpecimenId, Nothing)
                Dim speciesName As String = specimen.Description
                Dim fateName As String = ""
                Dim fateDate As New Date
                If Not specimen.Fate Is Nothing Then
                    fateName = specimen.Fate.Description
                    fateDate = specimen.FateDate
                End If
                reginfos(i) = New RegisteredInfo(speciesName, fateName, fateDate)
                i += 1
            Next
            ReDim Preserve reginfos(i - 1)
            Array.Sort(reginfos, New RegisteredInfoComparer)
            Return reginfos
        End Function

        Public Shared Function GetRegisteredBirdsReportData(ByVal partyId As Int32, ByVal currently As Boolean, ByVal previously As Boolean, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim newRow As DataRow = returnDS.Tables("BOKeeper").NewRow()
            Dim party As BOParty = BOParty.PolymorphicCreate(partyId)
            Dim reginfos() As RegisteredInfo = GetRegisteredInfo(partyId, currently, previously)
            Dim primaryAddress As New BOAddress(CType(party.MailingAddressId, Int32), Nothing)
            Dim searchReference As String = ""
            Dim i As Int32 = 0

            With newRow
                ' Address
                .Item("HolderId") = partyId
                .Item("NameAddress") = party.DisplayName & Environment.NewLine & primaryAddress.ReportAddress
            End With
            returnDS.Tables("BOKeeper").Rows.Add(newRow)
            returnDS.AcceptChanges()

            While i < reginfos.Length
                Dim speciesName As String = reginfos(i).SpeciesName
                Dim displayName As String = speciesName
                While i < reginfos.Length AndAlso reginfos(i).SpeciesName = speciesName
                    Dim reginfo1 As RegisteredInfo = reginfos(i)
                    Dim key As String = reginfo1.Key
                    Dim count As Int32 = 0
                    While i < reginfos.Length AndAlso reginfos(i).Key = key
                        count += 1
                        i += 1
                    End While
                    newRow = returnDS.Tables("BORegisteredBirds").NewRow()
                    With newRow
                        .Item("HolderId") = partyId
                        .Item("SpeciesName") = displayName
                        .Item("NumberSpecimens") = count
                        .Item("FateName") = reginfo1.FateName
                        .Item("FateDate") = reginfo1.FateDateDisplay
                    End With
                    displayName = ""
                    returnDS.Tables("BORegisteredBirds").Rows.Add(newRow)
                    returnDS.AcceptChanges()
                End While
            End While

            Return New ReportDataResults(returnDS, searchReference)
        End Function

        Private Shared Function FilterSpecimens(ByVal oldSpecimens() As BOPartySpecimen, ByVal wantCurrent As Boolean) As BOPartySpecimen()
            Dim newSpecimens(oldSpecimens.Length - 1) As BOPartySpecimen
            Dim i As Int32 = 0
            For Each specimen As BOPartySpecimen In oldSpecimens
                Dim current As Boolean = specimen.EndDate.Ticks = 0
                If current = wantCurrent Then
                    newSpecimens(i) = specimen
                    i += 1
                End If
            Next
            ReDim Preserve newSpecimens(i - 1)
            Return newSpecimens
        End Function

        Private Shared Sub OutputAddressGroups(ByRef returnDS As DataSet, ByRef index As Int32, ByVal party As BOParty, ByVal partySpecimens As BOPartySpecimen(), ByVal primary As Boolean, ByVal heading As String)
            Dim newRow As DataRow
            Dim oldAddressId As Int32 = -1
            Dim required As Boolean = False

            For Each item As BOPartySpecimen In partySpecimens
                newRow = returnDS.Tables("BOKeeper").NewRow()
                If item.AddressId <> oldAddressId Then  'change of address id
                    Dim address As New BOAddress(item.AddressId, Nothing)
                    Dim mailing As Boolean = CType(party.MailingAddressId, Int32) = item.AddressId
                    required = primary = mailing
                    oldAddressId = item.AddressId
                    If required Then
                        index += 1
                        With newRow
                            .Item("KeeperIdx") = index
                            .Item("KeeperDetails") = heading & Environment.NewLine & address.ReportAddress
                        End With
                        returnDS.Tables("BOKeeper").Rows.Add(newRow)
                        returnDS.AcceptChanges()
                    End If
                End If
                If required Then
                    OutputSpecimen(returnDS, item.SpecimenId, index)
                End If
            Next
        End Sub

        Private Shared Sub OutputSpecimen(ByRef returnDS As DataSet, ByVal specimenId As Int32, ByVal index As Int32)
            Dim specimen As BOSpecimen = New BOSpecimen(specimenId, Nothing)
            Dim marks() As BOSpecimenMark = specimen.SpecimenMarks
            Dim name As String = specimen.GetSpeciesReportName()
            Dim gender As String = specimen.Gender.ToString().Substring(0, 1)
            Dim hatch As String = ""

            If specimen.DOB <> Nothing Then
                hatch = specimen.DOB().ToString()
            End If
            If marks Is Nothing OrElse marks.Length = 0 Then
                ReDim marks(0)
            End If
            For Each mark As BOSpecimenMark In marks
                Dim newRow As DataRow = returnDS.Tables("BOKeeperBirds").NewRow()
                With newRow
                    .Item("KeeperIdx") = index
                    If Not mark Is Nothing Then
                        .Item("IdMarkType") = mark.IdMarkType.Description
                        .Item("IdMarkNumber") = mark.IdMark
                    End If
                    .Item("Origin") = "N/A"
                    .Item("SpeciesName") = name
                    .Item("Gender") = gender
                    .Item("HatchDate") = hatch
                    name = ""
                    gender = ""
                    hatch = ""
                End With
                returnDS.Tables("BOKeeperBirds").Rows.Add(newRow)
                returnDS.AcceptChanges()
            Next
        End Sub

        Public Shared Function GetKeeperBirdReportData(ByVal partyId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim newRow As DataRow = returnDS.Tables("BOKeeper").NewRow()
            Dim party As BOParty = BOParty.PolymorphicCreate(partyId)
            Dim partySpecimens() As BOPartySpecimen = BOPartySpecimen.GetPartySpecimensByPartyId(partyId)
            Dim primaryAddress As New BOAddress(CType(party.MailingAddressId, Int32), Nothing)
            Dim searchReference As String = ""
            Dim index As Int32 = -1

            OutputAddressGroups(returnDS, index, party, partySpecimens, True, "PRIMARY ADDRESS" & Environment.NewLine & party.DisplayName)
            OutputAddressGroups(returnDS, index, party, partySpecimens, False, "OTHER ADDRESS")

            If Not primaryAddress Is Nothing Then
                Dim otherAddressIds() As Int32 = primaryAddress.GetSimilarAddressIds()
                If otherAddressIds.Length > 0 Then
                    index += 1
                    newRow = returnDS.Tables("BOKeeper").NewRow()
                    With newRow
                        .Item("KeeperIdx") = index
                        .Item("KeeperDetails") = "BIRDS AT PRIMARY ADDRESS NOT REGISTERED TO KEEPER"
                    End With
                    returnDS.Tables("BOKeeper").Rows.Add(newRow)
                    returnDS.AcceptChanges()
                    For Each addressId As Int32 In otherAddressIds
                        Dim addressSpecimens() As BOPartySpecimen = BOPartySpecimen.GetPartySpecimensByAddressId(addressId)
                        For Each addressSpecimen As BOPartySpecimen In addressSpecimens
                            OutputSpecimen(returnDS, addressSpecimen.SpecimenId, index)
                        Next
                    Next
                End If
            End If
            Return New ReportDataResults(returnDS, searchReference)
        End Function

        Private Class RegisteredInfo
            Sub New(ByVal speciesName As String, ByVal fateName As String, ByVal fateDate As Date)
                mSpeciesName = speciesName
                mFateName = fateName
                mFateDate = fateDate
            End Sub

            Private mSpeciesName As String
            Private mFateName As String
            Private mFateDate As Date

            Public ReadOnly Property SpeciesName() As String
                Get
                    Return mSpeciesName
                End Get
            End Property

            Public ReadOnly Property FateName() As String
                Get
                    Return mFateName
                End Get
            End Property

            Public ReadOnly Property FateDate() As Date
                Get
                    Return mFateDate
                End Get
            End Property

            Public ReadOnly Property FateDateDisplay() As String
                Get
                    If mFateDate = Nothing Then
                        Return ""
                    End If
                    Return mFateDate.ToString("dd/MM/yyyy")
                End Get
            End Property

            Public ReadOnly Property Key() As String
                Get
                    If mFateDate = Nothing Then
                        Return mSpeciesName + mFateName
                    End If
                    Return mSpeciesName + mFateName + mFateDate.ToString("yyyyMMdd")
                End Get
            End Property
        End Class

        Private Class RegisteredInfoComparer
            Implements IComparer

            Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim xinfo As RegisteredInfo = CType(x, RegisteredInfo)
                Dim yinfo As RegisteredInfo = CType(y, RegisteredInfo)
                Return String.Compare(xinfo.Key, yinfo.Key)
            End Function
        End Class
    End Class
End Namespace
