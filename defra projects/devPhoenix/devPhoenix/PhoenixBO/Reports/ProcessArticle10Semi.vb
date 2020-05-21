Imports System
Imports System.data
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class Article10Semi
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim article10SemiReportCriteria As ReportCriteria.Article10SemiReportCriteria = CType(reportCriteria, ReportCriteria.Article10SemiReportCriteria)

            Dim reportDataResults As ReportDataResults = GetReportDataResults(article10SemiReportCriteria.PermitInfoId, article10SemiReportCriteria.Duplicate)

            Dim article10_RPT As New article10_RPT
            Dim reportResults(0) As BOReportResults
            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, article10SemiReportCriteria.PermitInfoId, reportDataResults.ReportData, article10SemiReportCriteria, saveReport, _
                article10_RPT, reportPrintJobId, printSequence)

            Return reportResults

        End Function

        Private Function GetReportDataResults(ByVal permitInfoId As Int32, ByVal duplicate As Boolean) As ReportDataResults

            Dim article10Permit As New Application.CITES.Applications.BOCITESArticle10Permit
            Dim article10Data As New article10Data

            Dim copyDataset As New article10Data
            Dim copyRow As article10Data.BOPermitRow

            Dim reportDataResults As reportDataResults = article10Permit.GetArticle10ReportData(permitInfoId, duplicate, article10Data.GetXmlSchema)
            article10Data.Merge(reportDataResults.ReportData)
            article10Data.AcceptChanges()

            ' Create Permit row in Report Dataset - Sheet 2
            copyDataset = CType(article10Data.Copy(), article10Data)
            copyRow = CType(copyDataset.BOPermit.Rows(0), article10Data.BOPermitRow)
            copyRow.SheetNumber = "2"
            copyRow.SheetDescription = "COPY for the issuing authority"
            article10Data.Merge(copyDataset)
            article10Data.AcceptChanges()

            Return New reportDataResults(article10Data, reportDataResults.SearchReference)

        End Function

    End Class

End Namespace
