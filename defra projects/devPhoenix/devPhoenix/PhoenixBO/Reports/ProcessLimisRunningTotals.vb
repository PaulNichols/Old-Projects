Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class LimisRunningTotals
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim LimisRunningTotalsReportCriteria As ReportCriteria.LimisRunningTotalsReportCriteria = CType(reportCriteria, ReportCriteria.LimisRunningTotalsReportCriteria)

            Dim reportDatasets() As DataSet
            reportDatasets = GetLimisRunningTotalsReportDatasets(LimisRunningTotalsReportCriteria)

            Dim parameterValues As New Hashtable

            Dim financialYear As String = "Running Totals for " _
            & LimisRunningTotalsReportCriteria.FinancialYear.ToString & "-" _
            & CType(LimisRunningTotalsReportCriteria.FinancialYear + 1, String)
            parameterValues.Add("FinancialYear", financialYear)

            Dim LimisRunningTotals_RPT As LimisRunningTotals_RPT

            Dim idx As Int32 = -1
            Dim linkId As String = LimisRunningTotalsReportCriteria.FinancialYear.ToString
            For Each dataset As DataSet In reportDatasets
                idx += 1
                LimisRunningTotals_RPT = New LimisRunningTotals_RPT
                reportResults(idx) = DoReport(reportCriteria.Description, linkId, 0, dataset, LimisRunningTotalsReportCriteria, saveReport, _
                LimisRunningTotals_RPT, reportPrintJobId, printSequence, parameterValues)
            Next
            Return reportResults
        End Function

        Private Sub CreateHeadings(ByRef returnDS As LimisRunningTotalsData)
            Dim row As LimisRunningTotalsData.BORunningTotalsRow

            ' Create Column Headings 1
            row = returnDS.BORunningTotals.NewBORunningTotalsRow()
            row.MonthIdx = 0
            row.Month = ""
            row.Article10_TotDocs = " "
            row.Article10_DocsInTarget = "Article 10s"
            row.Article10_PercentInTarget = ""
            row.CitesPermits_TotDocs = ""
            row.CitesPermits_DocsInTarget = "CITES Permits"
            row.CitesPermits_PercentInTarget = ""
            row.BirdChicks_TotDocs = " "
            row.BirdChicks_DocsInTarget = "Bird Reg - Chicks"
            row.BirdChicks_PercentInTarget = ""
            row.BirdAdult_TotDocs = " "
            row.BirdAdult_DocsInTarget = "Bird Reg - Adult"
            row.BirdAdult_PercentInTarget = ""
            row.PageHeading = False
            row.TotalHeading = False
            returnDS.BORunningTotals.AddBORunningTotalsRow(row)

            ' Create Column Headings 2
            row = returnDS.BORunningTotals.NewBORunningTotalsRow()
            row.MonthIdx = 1
            row.Month = "Month"
            row.Article10_TotDocs = "Total Docs"
            row.Article10_DocsInTarget = "Docs in Target"
            row.Article10_PercentInTarget = "% in Target"
            row.CitesPermits_TotDocs = "Total Docs"
            row.CitesPermits_DocsInTarget = "Docs in Target"
            row.CitesPermits_PercentInTarget = "% in Target"
            row.BirdChicks_TotDocs = "Total Docs"
            row.BirdChicks_DocsInTarget = "Docs in Target"
            row.BirdChicks_PercentInTarget = "% in Target"
            row.BirdAdult_TotDocs = "Total Docs"
            row.BirdAdult_DocsInTarget = "Docs in Target"
            row.BirdAdult_PercentInTarget = "% in Target"
            row.PageHeading = False
            row.TotalHeading = False
            returnDS.BORunningTotals.AddBORunningTotalsRow(row)
        End Sub

        Private Sub CreateMonthRow(ByRef returnDS As LimisRunningTotalsData, ByVal index As Int32, ByVal name As String, ByVal monthStart As Date, ByRef totals() As ReportLimis.BOLimisMonth.Line)
            Dim monthData As New ReportLimis.BOLimisMonth(monthStart)
            Dim line1 As ReportLimis.BOLimisMonth.Line = monthData.GetSubTotal(ReportLimis.BOLimisMonth.Constant.Article10Type)
            Dim line2 As ReportLimis.BOLimisMonth.Line = monthData.GetSubTotal(ReportLimis.BOLimisMonth.Constant.CitesType)
            Dim line3 As ReportLimis.BOLimisMonth.Line = monthData.GetSubTotal(ReportLimis.BOLimisMonth.Constant.ChicksType)
            Dim line4 As ReportLimis.BOLimisMonth.Line = monthData.GetSubTotal(ReportLimis.BOLimisMonth.Constant.AdultType)

            CreateSubRow(returnDS, index, name, line1, line2, line3, line4, monthStart <= Date.Now)
            totals(0).Add(line1)
            totals(1).Add(line2)
            totals(2).Add(line3)
            totals(3).Add(line4)
        End Sub

        Private Sub CreateTotals(ByRef returnDS As LimisRunningTotalsData, ByVal totals() As ReportLimis.BOLimisMonth.Line)
            CreateSubRow(returnDS, 14, "Total", totals(0), totals(1), totals(2), totals(3), True)
        End Sub

        Private Sub CreateSubRow(ByRef returnDS As LimisRunningTotalsData, ByVal index As Int32, ByVal name As String, ByVal line1 As ReportLimis.BOLimisMonth.Line, ByVal line2 As ReportLimis.BOLimisMonth.Line, ByVal line3 As ReportLimis.BOLimisMonth.Line, ByVal line4 As ReportLimis.BOLimisMonth.Line, ByVal showData As Boolean)
            Dim row As LimisRunningTotalsData.BORunningTotalsRow

            row = returnDS.BORunningTotals.NewBORunningTotalsRow()
            row.MonthIdx = index
            row.Month = name
            If showData Then
                row.Article10_TotDocs = line1.DocTotal.ToString()
                row.Article10_DocsInTarget = line1.DocsInTarget.ToString()
                row.Article10_PercentInTarget = line1.PercentInTarget.ToString("N") + "%"
                row.CitesPermits_TotDocs = line2.DocTotal.ToString()
                row.CitesPermits_DocsInTarget = line2.DocsInTarget.ToString()
                row.CitesPermits_PercentInTarget = line2.PercentInTarget.ToString("N") + "%"
                row.BirdChicks_TotDocs = line3.DocTotal.ToString()
                row.BirdChicks_DocsInTarget = line3.DocsInTarget.ToString()
                row.BirdChicks_PercentInTarget = line3.PercentInTarget.ToString("N") + "%"
                row.BirdAdult_TotDocs = line4.DocTotal.ToString()
                row.BirdAdult_DocsInTarget = line4.DocsInTarget.ToString()
                row.BirdAdult_PercentInTarget = line4.PercentInTarget.ToString("N") + "%"
            End If
            row.PageHeading = False
            row.TotalHeading = False
            returnDS.BORunningTotals.AddBORunningTotalsRow(row)
        End Sub

        Public Function GetLimisRunningTotalsReportDatasets(ByVal criterion As ReportCriteria.LimisRunningTotalsReportCriteria) As LimisRunningTotalsData()
            Dim year1 As Int32 = criterion.FinancialYear
            Dim year2 As Int32 = year1 + 1
            Dim returnDS As New LimisRunningTotalsData
            Dim results(0) As LimisRunningTotalsData
            Dim row As LimisRunningTotalsData.BORunningTotalsRow
            Dim totals(3) As ReportLimis.BOLimisMonth.Line

            totals(0) = New ReportLimis.BOLimisMonth.Line("")
            totals(1) = New ReportLimis.BOLimisMonth.Line("")
            totals(2) = New ReportLimis.BOLimisMonth.Line("")
            totals(3) = New ReportLimis.BOLimisMonth.Line("")
            CreateHeadings(returnDS)
            CreateMonthRow(returnDS, 2, "Apr", New Date(year1, 4, 1), totals)
            CreateMonthRow(returnDS, 3, "May", New Date(year1, 5, 1), totals)
            CreateMonthRow(returnDS, 4, "Jun", New Date(year1, 6, 1), totals)
            CreateMonthRow(returnDS, 5, "Jul", New Date(year1, 7, 1), totals)
            CreateMonthRow(returnDS, 6, "Aug", New Date(year1, 8, 1), totals)
            CreateMonthRow(returnDS, 7, "Sep", New Date(year1, 9, 1), totals)
            CreateMonthRow(returnDS, 8, "Oct", New Date(year1, 10, 1), totals)
            CreateMonthRow(returnDS, 9, "Nov", New Date(year1, 11, 1), totals)
            CreateMonthRow(returnDS, 10, "Dec", New Date(year1, 12, 1), totals)
            CreateMonthRow(returnDS, 11, "Jan", New Date(year2, 1, 1), totals)
            CreateMonthRow(returnDS, 12, "Feb", New Date(year2, 2, 1), totals)
            CreateMonthRow(returnDS, 13, "Mar", New Date(year2, 3, 1), totals)
            CreateTotals(returnDS, totals)
            returnDS.AcceptChanges()
            results(0) = returnDS
            Return results
        End Function

    End Class
End Namespace