Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class PartyDataProtection
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Private Function GetReportTestData(ByVal partyId As Int32, ByVal schema As String) As ReportDataResults

            'get the DS ready
            Dim ReturnDS As New DataSet
            'create a stream to put the info into
            Dim io As New io.StringReader(schema)
            'set-up the DS schema
            ReturnDS.ReadXmlSchema(io)
            'tidy ...
            io.Close()
            io = Nothing

            Dim searchReference As String = "" ' Set this to a search reference if required

            'create a new row using the ds schema
            Dim newRow As DataRow

            ' Create a new BOPartyDataProtection row - Populate with test data
            newRow = ReturnDS.Tables("BOPartyDataProtection").NewRow()
            With newRow
                .Item("LinkId") = 1 ' Hard Coded - Always 1
                .Item("DisplayName") = "John Smith"
                .Item("ExcludeFromMailingList") = "No" ' "Exclude from mailing list: No"
                .Item("ReportPartyId") = "123"
                .Item("PreviousName") = "Jane Smith"
                .Item("MailingContactAddress") = "Permanent Address" _
                & Environment.NewLine & "Active Address: Yes" _
                & Environment.NewLine & "Alias Smith" _
                & Environment.NewLine & "54 St. James House" _
                & Environment.NewLine & "York" _
                & Environment.NewLine & "YK12 T34"
                .Item("PreferredContact") = "E-Mail: johns@hotmail.com"
                .Item("AllowSemCompleteIncomplete") = "Allow Incomplete Import Applications" _
                & Environment.NewLine & "Allow Semicomplete Cites Article10" _
                & Environment.NewLine & "Allow Semicomplete Cites Export" _
                & Environment.NewLine & "Allow Semicomplete Cites Import"
                .Item("Validated") = "Yes"
                .Item("KnownFactIssued") = "Yes"
            End With
            ReturnDS.Tables("BOPartyDataProtection").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            ' Create a new BOOtherAddress row - Populate with test data
            newRow = ReturnDS.Tables("BOOtherAddress").NewRow()
            With newRow
                .Item("LinkId") = 1 ' Hard Coded - Always 1
                .Item("Description") = "Temporary Address" _
                & Environment.NewLine & "Active Address: No" _
                & Environment.NewLine & "Peter Smith" _
                & Environment.NewLine & "36 Dame Street" _
                & Environment.NewLine & "Tamworth" _
                & Environment.NewLine & "Staffs"
            End With
            ReturnDS.Tables("BOOtherAddress").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            ' Create a new BOOtherAddress row - Populate with test data
            newRow = ReturnDS.Tables("BOOtherAddress").NewRow()
            With newRow
                .Item("LinkId") = 1 ' Hard Coded - Always 1
                .Item("Description") = "Temporary Address" _
                & Environment.NewLine & "Active Address: Yes" _
                & Environment.NewLine & "Smith Press" _
                & Environment.NewLine & "21 Main Street" _
                & Environment.NewLine & "Polesworth" _
                & Environment.NewLine & "Warwickshire"
            End With
            ReturnDS.Tables("BOOtherAddress").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            ' Create a new BOOtherContact row - Populate with test data
            newRow = ReturnDS.Tables("BOOtherContact").NewRow()
            With newRow
                .Item("LinkId") = 1 ' Hard Coded - Always 1
                .Item("Description") = "Telephone: 01654 653278"
            End With
            ReturnDS.Tables("BOOtherContact").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            ' Create a new BOOtherContact row - Populate with test data
            newRow = ReturnDS.Tables("BOOtherContact").NewRow()
            With newRow
                .Item("LinkId") = 1 ' Hard Coded - Always 1
                .Item("Description") = "Mobile: 077234242334"
            End With
            ReturnDS.Tables("BOOtherContact").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            ' Create a new BOPartyNote row - Populate with test data
            newRow = ReturnDS.Tables("BOPartyNote").NewRow()
            With newRow
                .Item("LinkId") = 1 ' Hard Coded - Always 1
                .Item("NoteDate") = "01 Dec 2004"
                .Item("NoteSubject") = "Sports"
                .Item("NoteContents") = "Plays Tennis"
            End With
            ReturnDS.Tables("BOPartyNote").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            ' Create a new BOPartyNote row - Populate with test data
            newRow = ReturnDS.Tables("BOPartyNote").NewRow()
            With newRow
                .Item("LinkId") = 1 ' Hard Coded - Always 1
                .Item("NoteDate") = "08 Jan 2005"
                .Item("NoteSubject") = "Drinking Habits"
                .Item("NoteContents") = "Likes Water"
            End With
            ReturnDS.Tables("BOPartyNote").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            ' Create a new BOBankDetails row - Populate with test data
            newRow = ReturnDS.Tables("BOBankDetails").NewRow()
            With newRow
                .Item("LinkId") = 1 ' Hard Coded - Always 1
                .Item("AccountNumber") = "12345678"
                .Item("SortCode") = "40 72 54"
            End With
            ReturnDS.Tables("BOBankDetails").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            ' Create a new BOBankDetails row - Populate with test data
            newRow = ReturnDS.Tables("BOBankDetails").NewRow()
            With newRow
                .Item("LinkId") = 1 ' Hard Coded - Always 1
                .Item("AccountNumber") = "12345678"
                .Item("SortCode") = "40 72 54"
            End With
            ReturnDS.Tables("BOBankDetails").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            Return New ReportDataResults(ReturnDS, searchReference)

        End Function

        Private Function GetReportData(ByVal partyId As Int32, ByVal schema As String) As ReportDataResults

            'get the DS ready
            Dim ReturnDS As New DataSet
            'create a stream to put the info into
            Dim io As New io.StringReader(schema)
            'set-up the DS schema
            ReturnDS.ReadXmlSchema(io)
            'tidy ...
            io.Close()
            io = Nothing

            Dim searchReference As String = "" ' Set this to a search reference if required

            Dim boParty As Party.BOParty = boParty.PolymorphicCreate(partyId)
            Dim boKnownFacts As New Party.BOKnownFacts ' Use this to determine 'Validated for on-line Applications'

            'create a new row using the ds schema
            Dim newRow As DataRow

            ' Create a new BOPartyDataProtection row - Populate with data
            newRow = ReturnDS.Tables("BOPartyDataProtection").NewRow()
            Dim mailingContactAddress As String
            Dim otherContactAddress(-1) As String
            Dim addressActive As String
            Dim permTempAddress As String
            Dim idx As Int32 = -1
            Dim preferredContact As String
            Dim otherContacts(-1) As String
            Dim ExcludeFromMailingList As String
            Dim allowSemCompleteIncomplete As String
            Dim newLine As String = ""
            With newRow
                .Item("LinkId") = 1 ' Hard Coded - Always 1
                .Item("DisplayName") = boParty.DisplayName
                .Item("ExcludeFromMailingList") = "No"
                If boParty.ExcludeFromMailingList Then
                    .Item("ExcludeFromMailingList") = "Yes"
                End If
                If boParty.AuthorisedPartyId = 0 Then .Item("ReportPartyId") = "" Else .Item("ReportPartyId") = boParty.AuthorisedPartyId.ToString
                .Item("PreviousName") = boParty.PreviousName

                ' Contact Mailing Address + Any other Contact Addresses
                For Each boAddress As Party.BOAddress In boParty.GetAddresses(Nothing)

                    ' Is this address active?
                    addressActive = "Active Address: No"
                    If boAddress.Active Then
                        addressActive = "Active Address: Yes"
                    End If

                    ' Is this address Permanent or Tempoary
                    permTempAddress = "Permanent Address"
                    If boAddress.IsTemporary Then
                        permTempAddress = "Temporary Address"
                    End If

                    ' Build Mailing ContactAddress or Other ContactAddress
                    If boAddress.IsMailing Then
                        mailingContactAddress = String.Concat(permTempAddress, Environment.NewLine, addressActive, Environment.NewLine, "Contact Name: ", boAddress.ContactName, Environment.NewLine, boAddress.ReportAddress())
                    Else
                        idx += 1
                        ReDim Preserve otherContactAddress(idx)
                        otherContactAddress(idx) = String.Concat(permTempAddress, Environment.NewLine, addressActive, Environment.NewLine, "Contact Name: ", boAddress.ContactName, Environment.NewLine & boAddress.ReportAddress())
                    End If
                Next boAddress
                .Item("MailingContactAddress") = mailingContactAddress

                ' Get Individual Person preferred contact details + Any other contact details 
                idx = -1
                If Not boParty.IsBusiness Then ' Test if Party is an Individual

                    Dim Person As Party.BOPerson = boParty.GetPersons()(0)
                    For Each contact As Party.BOContact In Person.GetContacts
                        If contact.Ispreferred() Then
                            preferredContact = contact.ContactType & ": " & contact.Detail
                        Else
                            idx += 1
                            ReDim Preserve otherContacts(idx)
                            otherContacts(idx) = contact.ContactType & ": " & contact.Detail
                        End If
                    Next

                    ' Exclude person from mailing list
                    ExcludeFromMailingList = "Exclude from mailing list: No"
                    If boParty.ExcludeFromMailingList Then
                        ExcludeFromMailingList = "Exclude from mailing list: Yes"
                    End If

                    ' Allow SemiComplete/Incomplete indicators
                    newLine = ""
                    If boParty.AllowIncompleteImportApplications Then
                        allowSemCompleteIncomplete = allowSemCompleteIncomplete & newLine & "Allow Incomplete Import Applications"
                        newLine = Environment.NewLine
                    End If
                    If boParty.AllowSemicompleteCitesArticle10 Then
                        allowSemCompleteIncomplete = allowSemCompleteIncomplete & newLine & "Allow Semicomplete Cites Article10"
                        newLine = Environment.NewLine
                    End If
                    If boParty.AllowSemicompleteCitesExport Then
                        allowSemCompleteIncomplete = allowSemCompleteIncomplete & newLine & "Allow Semicomplete Cites Export"
                        newLine = Environment.NewLine
                    End If
                    If boParty.AllowSemicompleteCitesImport Then
                        allowSemCompleteIncomplete = allowSemCompleteIncomplete & newLine & "Allow Semicomplete Cites Import"
                    End If

                End If

                .Item("PreferredContact") = preferredContact
                .Item("AllowSemCompleteIncomplete") = allowSemCompleteIncomplete

                .Item("Validated") = Application.Search.ApplicationSearch.ConvertToEnglishBoolean(boParty.Validated Is Nothing OrElse Not CType(boParty.Validated, Boolean))

                .Item("KnownFactIssued") = Application.Search.ApplicationSearch.ConvertToEnglishBoolean(Not boKnownFacts.SSOKnownFactId Is Nothing)

            End With
            ReturnDS.Tables("BOPartyDataProtection").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            ' Create a new BOOtherContact row - Populate with data
            For Each otherContact As String In otherContacts
                newRow = ReturnDS.Tables("BOOtherContact").NewRow()
                With newRow
                    .Item("LinkId") = 1 ' Hard Coded - Always 1
                    .Item("Description") = otherContact
                End With
                ReturnDS.Tables("BOOtherContact").Rows.Add(newRow)
                ReturnDS.AcceptChanges()
            Next

            ' Create a new BOOtherAddress row - Populate with data
            For Each otherAddress As String In otherContactAddress
                newRow = ReturnDS.Tables("BOOtherAddress").NewRow()
                With newRow
                    .Item("LinkId") = 1 ' Hard Coded - Always 1
                    .Item("Description") = otherAddress
                End With
                ReturnDS.Tables("BOOtherAddress").Rows.Add(newRow)
                ReturnDS.AcceptChanges()
            Next

            ' Get Party Notes
            Dim objNotes As Object = boParty.GetNotes
            If Not objNotes Is Nothing Then

                For Each partynote As Party.Note In CType(objNotes, Party.Note())

                    ' Create a new BOPartyNote row - Populate with data
                    newRow = ReturnDS.Tables("BOPartyNote").NewRow()
                    With newRow
                        .Item("LinkId") = 1 ' Hard Coded - Always 1
                        .Item("NoteDate") = partynote.NoteDate.ToShortDateString
                        .Item("NoteSubject") = partynote.Subject
                        .Item("NoteContents") = partynote.Content
                    End With
                    ReturnDS.Tables("BOPartyNote").Rows.Add(newRow)
                    ReturnDS.AcceptChanges()

                Next partynote

            End If

            ' Get Party Bank Payment Details
            For Each partyBankDetail As Party.PartyBankDetail In boParty.GetPaymentAccountDetails
                newRow = ReturnDS.Tables("BOBankDetails").NewRow()
                With newRow
                    .Item("LinkId") = 1 ' Hard Coded - Always 1
                    .Item("AccountNumber") = partyBankDetail.AccountNumber
                    .Item("SortCode") = partyBankDetail.SortCode
                End With
                ReturnDS.Tables("BOBankDetails").Rows.Add(newRow)
                ReturnDS.AcceptChanges()
            Next

            'Dim validated As String = "Yes"
            'If boParty.Validated Is Nothing OrElse boParty.Validated = False Then
            '    validated = "No"
            'End If

            'Dim knownFactIssued As String
            'If boKnownFacts.SSOKnownFactId Is Nothing Then
            '    knownFactIssued = "No"
            'Else
            '    knownFactIssued = "Yes"
            'End If

            ' Person or Business Name
            'Dim displayName As String = boParty.DisplayName

            ' Party Id
            'Dim reportPartyId As String = boParty.AuthorisedPartyId.ToString
            'If reportPartyId = "0" Then reportPartyId = ""

            ' Previous Application Name
            'Dim previousName As String = boParty.PreviousName

            ' Contact Mailing Address + Any other Contact Addresses
            'Dim mailingContactAddress As String
            'Dim otherContactAddress(-1) As String
            'Dim addressActive As String
            'Dim permTempAddress As String

            'Dim idx As Int32 = -1
            'For Each boAddress As Party.BOAddress In boParty.GetAddresses(Nothing)

            '    ' Is this address active?
            '    addressActive = "Active Address: No"
            '    If boAddress.Active Then
            '        addressActive = "Active Address: Yes"
            '    End If

            '    ' Is this address Permanent or Tempoary
            '    permTempAddress = "Permanent Address"
            '    If boAddress.IsTemporary Then
            '        permTempAddress = "Temporary Address"
            '    End If

            '    ' Build Mailing ContactAddress or Other ContactAddress
            '    If boAddress.IsMailing Then
            '        mailingContactAddress = permTempAddress & Environment.NewLine & addressActive & Environment.NewLine & "Contact Name: " & boAddress.ContactName & Environment.NewLine & boAddress.ReportAddress()
            '    Else
            '        idx += 1
            '        ReDim Preserve otherContactAddress(idx)
            '        otherContactAddress(idx) = permTempAddress & Environment.NewLine & addressActive & Environment.NewLine & "Contact Name: " & boAddress.ContactName & Environment.NewLine & boAddress.ReportAddress()
            '    End If

            'Next boAddress


            ' Get Individual Person preferred contact details + Any other contact details 
            'Dim preferredContact As String
            'Dim otherContacts(-1) As String
            'Dim ExcludeFromMailingList As String
            'Dim allowSemCompleteIncomplete As String
            'Dim newLine As String = ""
            'idx = -1
            'If Not boParty.IsBusiness Then ' Test if Party is an Individual

            '    Dim Person As Party.BOPerson = boParty.GetPersons()(0)
            '    For Each contact As Party.BOContact In Person.GetContacts
            '        If contact.Ispreferred() Then
            '            preferredContact = contact.ContactType & ": " & contact.Detail
            '        Else
            '            idx += 1
            '            ReDim Preserve otherContacts(idx)
            '            otherContacts(idx) = contact.ContactType & ": " & contact.Detail
            '        End If
            '    Next

            '    ' Exclude person from mailing list
            '    ExcludeFromMailingList = "Exclude from mailing list: No"
            '    If boParty.ExcludeFromMailingList Then
            '        ExcludeFromMailingList = "Exclude from mailing list: Yes"
            '    End If

            '    ' Allow SemiComplete/Incomplete indicators
            '    newLine = ""
            '    If boParty.AllowIncompleteImportApplications Then
            '        allowSemCompleteIncomplete = allowSemCompleteIncomplete & newLine & "Allow Incomplete Import Applications"
            '        newLine = Environment.NewLine
            '    End If
            '    If boParty.AllowSemicompleteCitesArticle10 Then
            '        allowSemCompleteIncomplete = allowSemCompleteIncomplete & newLine & "Allow Semicomplete Cites Article10"
            '        newLine = Environment.NewLine
            '    End If
            '    If boParty.AllowSemicompleteCitesExport Then
            '        allowSemCompleteIncomplete = allowSemCompleteIncomplete & newLine & "Allow Semicomplete Cites Export"
            '        newLine = Environment.NewLine
            '    End If
            '    If boParty.AllowSemicompleteCitesImport Then
            '        allowSemCompleteIncomplete = allowSemCompleteIncomplete & newLine & "Allow Semicomplete Cites Import"
            '    End If

            'End If




            Return New ReportDataResults(ReturnDS, searchReference)

        End Function

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim partyDataProtectionCriteria As reportCriteria.PartyDataProtectionCriteria = CType(reportCriteria, reportCriteria.PartyDataProtectionCriteria)
            Dim partyDataProtectionDataset As New PartyDataProtectionData
            Dim reportDataResults As reportDataResults

            If partyDataProtectionCriteria.PartyId < 0 Then
                reportDataResults = GetReportTestData(partyDataProtectionCriteria.PartyId, partyDataProtectionDataset.GetXmlSchema)
            Else
                reportDataResults = GetReportData(partyDataProtectionCriteria.PartyId, partyDataProtectionDataset.GetXmlSchema)
            End If
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim applicationPermit_RPT As New PartyDataProtection_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, partyDataProtectionCriteria.PartyId, reportDataResults.ReportData, partyDataProtectionCriteria, saveReport, _
                applicationPermit_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function
    End Class

End Namespace
