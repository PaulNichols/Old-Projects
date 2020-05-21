Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class DraftPermit
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim permitDraftReportCriteria As reportCriteria.PermitDraftReportCriteria = CType(reportCriteria, reportCriteria.PermitDraftReportCriteria)
            Dim permitDataset As New BOCITESPermitData

            'Dim citesImportExportPermit As New Application.CITES.Applications.BOCITESImportExportPermit
            Dim reportDataResults As reportDataResults = Application.CITES.Applications.BOCITESImportExportPermit.GetPermitReportData(permitDraftReportCriteria.PermitInfoId, permitDraftReportCriteria.Duplicate, True, permitDataset.GetXmlSchema)
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            Dim permit_RPT As New CITESPermit_RPT
            Dim reportResults(0) As BOReportResults

            Dim parameterValues As New Hashtable
            parameterValues.Add("HideDraftCopy", False)

            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, permitDraftReportCriteria.PermitInfoId, reportDataResults.ReportData, permitDraftReportCriteria, saveReport, _
                permit_RPT, reportPrintJobId, printSequence, parameterValues)

            Return reportResults

        End Function
    End Class

End Namespace
