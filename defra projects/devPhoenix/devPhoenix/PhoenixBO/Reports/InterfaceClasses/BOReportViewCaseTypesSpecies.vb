Imports System.IO
Imports System.Text
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Collection
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Service
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Entity

Public Class BOReportViewCaseTypesSpecies
    Public Sub New()
        MyBase.New()
    End Sub

    Public Shared Function GetReportData(ByVal fromMonth As Int32, ByVal fromYear As Int32, _
    ByVal toMonth As Int32, ByVal toYear As Int32, _
    ByVal applicationType As Object, ByVal permitStatus As Object, ByVal source As Object, ByVal origin As Object, _
    ByVal purpose As Object, ByVal species As Object, ByVal countryOfExport As Object, ByVal countryOfDestination As Object, _
    ByVal applicantId As Object, ByVal schema As String) As ReportDataResults

        'get the DS ready
        Dim ReturnDS As New DataSet
        'create a stream to put the info into
        Dim io As New io.StringReader(schema)

        'set-up the DS schema
        ReturnDS.ReadXmlSchema(io)
        'tidy ...
        io.Close()
        io = Nothing

        Dim fromDate As New DateTime(fromYear, fromMonth, 1)
        Dim toDate As New DateTime(toYear, toMonth, DateTime.DaysInMonth(toYear, toMonth))
        Dim permitData As ICollection = SearchPermitService.GetPermitsByDateRange(fromDate, toDate)

        For Each permitItem As SearchPermit In permitData
            Dim include As Boolean = _
                Check(applicationType, permitItem.ApplicationTypeId) AndAlso _
                Check(origin, permitItem.CountryOfOriginId) AndAlso _
                Check(applicantId, permitItem.ApplicantId)
            If include Then
                'add the row to the dataset
                ReturnDS.Tables("BOViewCaseTypesSpecies").Rows.Add(AddRow(ReturnDS, permitItem))
            End If
        Next
        Return New ReportDataResults(ReturnDS, "")
    End Function

    Private Shared Function Check(ByVal obj As Object, ByVal test As Int32) As Boolean
        Return obj Is Nothing OrElse CType(obj, Int32) = test
    End Function

    Private Shared Function AddRow(ByRef dataset As DataSet, ByVal permitItem As SearchPermit) As DataRow
        Dim newRow As DataRow = dataset.Tables("BOViewCaseTypesSpecies").NewRow()
        With newRow
            .Item("BoldRow") = False
            .Item("ApplicantId") = permitItem.ApplicantId.ToString()
            .Item("ApplicantName") = GetApplicantName(permitItem)
            .Item("ApplicantPostcode") = permitItem.PostCode
            .Item("ApplicationType") = GetApplicationType(permitItem)
            .Item("PermitNo") = permitItem.PermitNumber
            .Item("ApplicationNo") = permitItem.ApplicationId.ToString()
            .Item("ReceivedDate") = permitItem.RecievedDate
            .Item("Status") = GetPermitStatus(permitItem)
            .Item("ScientificName") = permitItem.ScientificName
            .Item("CommonName") = permitItem.CommonName
            .Item("PartDerivative") = permitItem.Derivative
            .Item("Purpose") = permitItem.PurposeCode
            .Item("CountryOfOrigin") = GetCountry(permitItem)
            .Item("OtherCountry") = "(to do)"
            .Item("OtherCountryPermitNo") = "(to do)"
            .Item("Source") = permitItem.SourceCode
            .Item("CertificateDeclaration") = GetCertificateDeclaration(permitItem)
            .Item("Gender") = "N/A"
            .Item("Quantity") = "N/A"
            .Item("NetMass") = "N/A"
            .Item("IssuedDate") = "N/A"
            .Item("QuantityUsed") = "N/A"
            .Item("NetMassUsed") = "N/A"
            .Item("Annex") = "N/A"
            .Item("Appendix") = "N/A"
            .Item("Owner") = "N/A"
            .Item("OtherPartyId") = "N/A"
            .Item("OtherPartyName") = "N/A"
            .Item("OtherPartyPostcode") = "N/A"
        End With
        Return newRow
    End Function

    Private Shared Function GetApplicantName(ByVal permitItem As SearchPermit) As String
        Dim applicantName As String = permitItem.PersonName
        If applicantName Is Nothing OrElse applicantName.Length = 0 Then
            applicantName = permitItem.BusinessName
        End If
        Return applicantName
    End Function

    Private Shared Function GetApplicationType(ByVal permitItem As SearchPermit) As String
        Select Case permitItem.ApplicationTypeId
            Case 1
                Return "Import Permit"
            Case 2
                Return "Export Permit"
            Case 3
                Return "Article 10"
            Case 4
                Return "Article 30"
        End Select
        Return "Unknown"
    End Function

    Private Shared Function GetPermitStatus(ByVal permitItem As SearchPermit) As String
        Try
            Dim status As New ReferenceData.BOPermitStatus(permitItem.PermitStatusId)
            Return status.Description
        Catch
        End Try
        Return "Unknown"
    End Function

    Private Shared Function GetCountry(ByVal permitItem As SearchPermit) As String
        Try
            Dim country As New ReferenceData.BOCountry(permitItem.CountryOfOriginId)
            Return country.ShortName
        Catch
        End Try
        Return "Unknown"
    End Function

    Private Shared Function GetCertificateDeclaration(ByVal permitItem As SearchPermit) As String
        Try
            Dim builder As New StringBuilder
            builder.Append(GetYN(permitItem.Box18_1))
            builder.Append(GetYN(permitItem.Box18_2))   'MLD 15/11/4 tried simply concatenating
            builder.Append(GetYN(permitItem.Box18_3))   'the GetYN calls, but brain-dead VB
            builder.Append(GetYN(permitItem.Box18_4))   'compiler can't cope, hence this
            builder.Append(GetYN(permitItem.Box18_5))   'long winded stuff
            builder.Append(GetYN(permitItem.Box18_6))
            builder.Append(GetYN(permitItem.Box18_7))
            builder.Append(GetYN(permitItem.Box18_8))
            Return builder.ToString()
        Catch
        End Try
        Return "NNNNNNNN"
    End Function

    Private Shared Function GetYN(ByVal item As Boolean) As String
        If item Then Return "Y"
        Return "N"
    End Function

    Private Shared Function GetValue(ByVal item As Object, ByVal name As String) As Object
        Return item.GetType().GetProperty(name).GetValue(item, Nothing)
    End Function
End Class
