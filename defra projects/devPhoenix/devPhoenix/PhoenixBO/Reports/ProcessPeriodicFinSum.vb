Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class PeriodicFinSum
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim reportResults(0) As BOReportResults

            Dim rptCriteria As reportCriteria.PeriodicFinSumCriteria = CType(reportCriteria, reportCriteria.PeriodicFinSumCriteria)

            Dim reportDataset As DataSet

            reportDataset = GetDataset(rptCriteria)

            Dim crystalRPT As New PeriodicFinSum_RPT

            Dim SearchReference As String = ""

            Dim parameterValues As New Hashtable

            parameterValues.Add("FromToDate", "Period Covered From: " & rptCriteria.FromDate.ToShortDateString & " To " & rptCriteria.ToDate.ToShortDateString)

            reportResults(0) = DoReport(reportCriteria.Description, SearchReference, 0, reportDataset, rptCriteria, saveReport, _
            crystalRPT, reportPrintJobId, printSequence, parameterValues)

            Return reportResults

        End Function


        Public Function GetDataset(ByVal rptCriteria As ReportCriteria.PeriodicFinSumCriteria) As PeriodicFinSumData

            Dim periodicFinSumData As New periodicFinSumData

            periodicFinSumData.Merge(BOReportPeriodicFinSum.PeriodicFinSumReportData(rptCriteria.FromDate, rptCriteria.ToDate, periodicFinSumData.GetXmlSchema).ReportData)

            Return periodicFinSumData


        End Function

    End Class

End Namespace