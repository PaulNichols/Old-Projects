Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class SpeciesCaseTypes
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim SpeciesCaseTypesReportCriteria As ReportCriteria.SpeciesCaseTypesReportCriteria = CType(reportCriteria, ReportCriteria.SpeciesCaseTypesReportCriteria)

            Dim reportDatasets() As DataSet
            reportDatasets = GetSpeciesCaseTypesReportDatasets(SpeciesCaseTypesReportCriteria)

            Dim parameterValues As New Hashtable
            Dim dateTimeFormatInfo As New System.Globalization.DateTimeFormatInfo

            Dim fromToMonthYear As String = "From " _
            & dateTimeFormatInfo.GetMonthName(SpeciesCaseTypesReportCriteria.FromMonth) _
            & " " & SpeciesCaseTypesReportCriteria.FromYear.ToString & " To " _
            & dateTimeFormatInfo.GetMonthName(SpeciesCaseTypesReportCriteria.ToMonth) _
            & " " & SpeciesCaseTypesReportCriteria.ToYear.ToString
            parameterValues.Add("FromToMonthYear", fromToMonthYear)

            Dim SpeciesCaseTypes_RPT As SpeciesCaseTypes_RPT

            Dim idx As Int32 = -1
            Dim linkId As String = ""
            For Each dataset As dataset In reportDatasets
                idx += 1
                SpeciesCaseTypes_RPT = New SpeciesCaseTypes_RPT
                reportResults(idx) = DoReport(reportCriteria.Description, linkId, 0, dataset, SpeciesCaseTypesReportCriteria, saveReport, _
                SpeciesCaseTypes_RPT, reportPrintJobId, printSequence, parameterValues)
            Next

            Return reportResults

        End Function


        Public Function GetSpeciesCaseTypesReportDatasets(ByVal SpeciesCaseTypesReportCriteria As ReportCriteria.SpeciesCaseTypesReportCriteria) As SpeciesCaseTypesData()

            Dim SpeciesCaseTypesDataset As New SpeciesCaseTypesData
            Dim lo_Row As SpeciesCaseTypesData.BODetailsRow

            ' Create Details
            lo_Row = SpeciesCaseTypesDataset.BODetails.NewBODetailsRow()
            'lo_Row.AppTypeId = 0
            'lo_Row.Description = "Application Type"
            'lo_Row.InternetApps = "Internet Apps."
            'lo_Row.TelephoneApps = "Telephone Apps."
            'lo_Row.PostalApps = "Postal Apps."
            'lo_Row.EMailApps = "E-Mail Apps."
            'lo_Row.FaxApps = "Fax Apps."
            'lo_Row.PageHeading = True
            'lo_Row.TotalHeading = False
            SpeciesCaseTypesDataset.BODetails.AddBODetailsRow(lo_Row)


            SpeciesCaseTypesDataset.AcceptChanges()

            Dim SpeciesCaseTypesDatasets(0) As SpeciesCaseTypesData
            SpeciesCaseTypesDatasets(0) = SpeciesCaseTypesDataset

            Return SpeciesCaseTypesDatasets

        End Function

    End Class

End Namespace