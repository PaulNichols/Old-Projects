Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class LimisMonthly
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim LimisMonthlyReportCriteria As ReportCriteria.LimisMonthlyReportCriteria = CType(reportCriteria, ReportCriteria.LimisMonthlyReportCriteria)

            Dim reportDatasets() As DataSet
            reportDatasets = GetLimisMonthlyReportDatasets(LimisMonthlyReportCriteria)

            Dim parameterValues As New Hashtable
            Dim dateTimeFormatInfo As New System.Globalization.DateTimeFormatInfo

            Dim monthYear As String = "Totals sheet for " _
            & dateTimeFormatInfo.GetMonthName(LimisMonthlyReportCriteria.Month) _
            & " " & LimisMonthlyReportCriteria.Year.ToString
            parameterValues.Add("MonthYear", monthYear)

            Dim LimisMonthly_RPT As LimisMonthly_RPT

            Dim idx As Int32 = -1
            Dim linkId As String = LimisMonthlyReportCriteria.Year.ToString.PadLeft(2, CType("0", Char)) & LimisMonthlyReportCriteria.Month.ToString.PadLeft(2)
            For Each dataset As dataset In reportDatasets
                idx += 1
                LimisMonthly_RPT = New LimisMonthly_RPT
                reportResults(idx) = DoReport(reportCriteria.Description, linkId, 0, dataset, LimisMonthlyReportCriteria, saveReport, _
                LimisMonthly_RPT, reportPrintJobId, printSequence, parameterValues)
            Next
            Return reportResults
        End Function

        Private Sub CreateHeading(ByRef returnDS As LimisMonthlyData)
            Dim row As LimisMonthlyData.BOAppTypeDetailRow
            row = returnDS.BOAppTypeDetail.NewBOAppTypeDetailRow
            row.AppTypeId = 0
            row.Description = "App. Type"
            row.ActualNoOfApps = "Actual no. of apps"
            row.DocsInTarget = "Docs. in target"
            row.DocsOutOfTarget = "Docs. out of target"
            row.TotalNoOfDocs = "Total no. of docs"
            row.PercentDocsInTarget = "% of docs. in target"
            row.PageHeading = True
            row.AppTypeHeading = False
            row.SubTotalHeading = False
            returnDS.BOAppTypeDetail.AddBOAppTypeDetailRow(row)
        End Sub

        Private Sub CreateBlankLine(ByRef returnDS As LimisMonthlyData, ByRef count As Int32)
            CreateSubHead(returnDS, count, "")
        End Sub

        Private Sub CreateSubHead(ByRef returnDS As LimisMonthlyData, ByRef count As Int32, ByVal desc As String)
            Dim row As LimisMonthlyData.BOAppTypeDetailRow
            row = returnDS.BOAppTypeDetail.NewBOAppTypeDetailRow()
            count += 1
            row.AppTypeId = count
            row.Description = desc
            row.ActualNoOfApps = " "
            row.DocsInTarget = " "
            row.DocsOutOfTarget = " "
            row.TotalNoOfDocs = " "
            row.PercentDocsInTarget = " "
            row.PageHeading = False
            row.AppTypeHeading = desc > ""
            row.SubTotalHeading = False
            returnDS.BOAppTypeDetail.AddBOAppTypeDetailRow(row)
        End Sub

        Private Sub CreateDataLine(ByRef returnDS As LimisMonthlyData, ByRef count As Int32, ByVal line As ReportLimis.BOLimisMonth.Line)
            CreateLineSub(returnDS, count, line, False)
        End Sub

        Private Sub CreateTotal(ByRef returnDS As LimisMonthlyData, ByRef count As Int32, ByVal line As ReportLimis.BOLimisMonth.Line)
            CreateLineSub(returnDS, count, line, True)
        End Sub

        Private Sub CreateLineSub(ByRef returnDS As LimisMonthlyData, ByRef count As Int32, ByVal line As ReportLimis.BOLimisMonth.Line, ByRef subtotal As Boolean)
            Dim row As LimisMonthlyData.BOAppTypeDetailRow
            row = returnDS.BOAppTypeDetail.NewBOAppTypeDetailRow()
            count += 1
            row.AppTypeId = count
            row.Description = line.Description
            row.ActualNoOfApps = line.AppTotal.ToString()
            row.DocsInTarget = line.DocsInTarget.ToString()
            row.DocsOutOfTarget = line.DocsOutOfTarget.ToString()
            row.TotalNoOfDocs = line.DocTotal.ToString()
            row.PercentDocsInTarget = line.PercentInTarget.ToString("N") + "%"
            row.PageHeading = False
            row.AppTypeHeading = False
            row.SubTotalHeading = subtotal
            returnDS.BOAppTypeDetail.AddBOAppTypeDetailRow(row)
        End Sub

        Public Function GetLimisMonthlyReportDatasets(ByVal criterion As ReportCriteria.LimisMonthlyReportCriteria) As LimisMonthlyData()
            Dim monthStart As New Date(criterion.Year, criterion.Month, 1)
            Dim monthData As New ReportLimis.BOLimisMonth(monthStart)
            Dim returnDS As New LimisMonthlyData
            Dim results(0) As LimisMonthlyData
            Dim row As LimisMonthlyData.BOAppTypeDetailRow
            Dim count As Int32 = 0

            CreateHeading(returnDS)
            For Each appType As ReportLimis.BOLimisMonth.AppType In monthData.AppTypes
                CreateBlankLine(returnDS, count)
                CreateSubHead(returnDS, count, appType.Description)
                For Each line As ReportLimis.BOLimisMonth.Line In appType.Lines
                    CreateDataLine(returnDS, count, line)
                Next
            Next
            CreateBlankLine(returnDS, count)
            CreateTotal(returnDS, count, monthData.Total)
            returnDS.AcceptChanges()
            results(0) = returnDS
            Return results
        End Function

    End Class
End Namespace