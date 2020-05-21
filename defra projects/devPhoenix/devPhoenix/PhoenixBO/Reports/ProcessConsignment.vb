Imports System
Imports System.data
Imports System.Collections
Imports uk.gov.defra.Phoenix.PhoenixReport
Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do

Namespace RPT

    Public Class Consignment
        Inherits BOReport

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Overrides Function Process(ByVal reportCriteria As ReportCriteria.ReportCriteria, ByVal reportPrintJobId As Int32, ByRef printSequence As Int32, ByVal saveReport As Boolean) As BOReportResults()
            Dim consignmentReportCriteria As reportCriteria.ConsignmentReportCriteria = CType(reportCriteria, reportCriteria.ConsignmentReportCriteria)
            Dim consignmentDataset As New ConsignmentData

            Dim citesImportExportPermit As New Application.CITES.Applications.BOCITESImportExportPermit
            Dim reportDataResults As reportDataResults
            If consignmentReportCriteria.ApplicationId > 0 Then
                reportDataResults = citesImportExportPermit.GetConsignmentReportData(consignmentReportCriteria.ApplicationId, consignmentReportCriteria.Duplicate, False, consignmentDataset.GetXmlSchema)
            Else

                reportDataResults = citesImportExportPermit.GetConsignmentReportTestData(consignmentReportCriteria.ApplicationId, consignmentReportCriteria.Duplicate, False, consignmentDataset.GetXmlSchema)
            End If
            Dim ReportDataset As DataSet = reportDataResults.ReportData

            'Dim consignment_RPT As New CITESConsignment_RPT
            Dim consignment_RPT As New consignment_RPT
            Dim reportResults(0) As BOReportResults

            Dim parameterValues As New Hashtable
            parameterValues.Add("HideDraftCopy", True)

            reportResults(0) = DoReport(reportCriteria.Description, reportDataResults.SearchReference, consignmentReportCriteria.ApplicationId, reportDataResults.ReportData, consignmentReportCriteria, saveReport, _
                consignment_RPT, reportPrintJobId, printSequence, parameterValues)

            Return reportResults

        End Function
    End Class

End Namespace
