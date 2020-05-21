Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class ViewCaseTypesSpecies
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim rptCriteria As reportCriteria.ViewCaseTypesSpeciesCriteria = CType(reportCriteria, reportCriteria.ViewCaseTypesSpeciesCriteria)

            Dim reportDataset As DataSet

            reportDataset = GetDataset(rptCriteria)

            Dim crystalRPT As New ViewCaseTypesSpecies_RPT

            Dim SearchReference As String = ""

            reportResults(0) = DoReport(reportCriteria.Description, SearchReference, 0, reportDataset, rptCriteria, saveReport, _
            crystalRPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function


        Public Function GetDataset(ByVal rptCriteria As ReportCriteria.ViewCaseTypesSpeciesCriteria) As ViewCaseTypesSpeciesData

            Dim dateTimeFormatInfo As New System.Globalization.DateTimeFormatInfo

            Dim viewCaseTypesSpeciesData As New viewCaseTypesSpeciesData

            Dim aRow As viewCaseTypesSpeciesData.BOViewCaseTypesSpeciesRow

            ' Add Report Heading Details 
            aRow = CType(viewCaseTypesSpeciesData.BOViewCaseTypesSpecies.NewRow, viewCaseTypesSpeciesData.BOViewCaseTypesSpeciesRow)
            aRow.BoldRow = True
            aRow.ApplicantName = "View Case Types for Species"
            viewCaseTypesSpeciesData.BOViewCaseTypesSpecies.AddBOViewCaseTypesSpeciesRow(aRow)

            ' Add Report Heading Details 
            aRow = CType(viewCaseTypesSpeciesData.BOViewCaseTypesSpecies.NewRow, viewCaseTypesSpeciesData.BOViewCaseTypesSpeciesRow)
            aRow.BoldRow = True
            aRow.ApplicantName = "From: " & dateTimeFormatInfo.GetMonthName(rptCriteria.FromMonth) & " " & rptCriteria.FromYear.ToString
            viewCaseTypesSpeciesData.BOViewCaseTypesSpecies.AddBOViewCaseTypesSpeciesRow(aRow)

            ' Add Report Heading Details 
            aRow = CType(viewCaseTypesSpeciesData.BOViewCaseTypesSpecies.NewRow, viewCaseTypesSpeciesData.BOViewCaseTypesSpeciesRow)
            aRow.BoldRow = True
            aRow.ApplicantName = "To: " & dateTimeFormatInfo.GetMonthName(rptCriteria.ToMonth) & " " & rptCriteria.ToYear.ToString
            viewCaseTypesSpeciesData.BOViewCaseTypesSpecies.AddBOViewCaseTypesSpeciesRow(aRow)

            ' Add Report Heading Details 
            aRow = CType(viewCaseTypesSpeciesData.BOViewCaseTypesSpecies.NewRow, viewCaseTypesSpeciesData.BOViewCaseTypesSpeciesRow)
            aRow.BoldRow = True
            aRow.ApplicantName = "Applicant Name"
            aRow.ApplicantId = "Applicant ID"
            aRow.ApplicantPostcode = "Applicant Postcode"
            aRow.ApplicationType = "Application Type"
            aRow.PermitNo = "Permit Number"
            aRow.ApplicationNo = "Application number"
            aRow.ReceivedDate = "Received Date"
            aRow.Status = "Status"
            aRow.ScientificName = "Scientific Name"
            aRow.CommonName = "Common Name"
            aRow.PartDerivative = "Part Derivative"
            aRow.Purpose = "Purpose"
            aRow.CountryOfOrigin = "Country of Origin"
            aRow.OtherCountry = "Other Country"
            aRow.OtherCountryPermitNo = "Other Country Permit number"
            aRow.Source = "Source"
            aRow.CertificateDeclaration = "Certificate Declaration"
            aRow.Gender = "Gender"
            aRow.Quantity = "Quantity"
            aRow.NetMass = "Net Mass"
            aRow.IssuedDate = "Issued Date"
            aRow.QuantityUsed = "Quantity Used"
            aRow.NetMassUsed = "Net Mass Used"
            aRow.Annex = "Annex"
            aRow.Appendix = "Appendix"
            aRow.Owner = "Owner"
            aRow.OtherPartyId = "Other Party ID"
            aRow.OtherPartyName = "Other Party Name"
            aRow.OtherPartyPostcode = "Other Party Postcode"
            viewCaseTypesSpeciesData.BOViewCaseTypesSpecies.AddBOViewCaseTypesSpeciesRow(aRow)

            viewCaseTypesSpeciesData.Merge(BOReportViewCaseTypesSpecies.GetReportData(rptCriteria.FromMonth, rptCriteria.FromYear, rptCriteria.ToMonth, rptCriteria.ToYear, _
            rptCriteria.applicationType, rptCriteria.permitStatus, rptCriteria.source, rptCriteria.origin, rptCriteria.purpose, rptCriteria.species, rptCriteria.countryOfExport, _
            rptCriteria.countryOfDestination, rptCriteria.applicantId, viewCaseTypesSpeciesData.GetXmlSchema).ReportData)

            Return viewCaseTypesSpeciesData


        End Function
    End Class

End Namespace