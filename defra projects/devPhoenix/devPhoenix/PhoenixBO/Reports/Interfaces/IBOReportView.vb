Public Interface IBOReportView

    Property PrintSequence() As Int32
    Property ReportId() As Int32
    Property PrintJobDescription() As String
    Property ReportTypeDescription() As String
    Property ReportViewURL() As String
    Property ReportPrinterId() As Int32
    Property PrinterSelection() As uk.gov.defra.Phoenix.bo.ReportData.BoReportPrinter()
    Property StapleSelection() As uk.gov.defra.Phoenix.bo.ReportData.BoReportStaple()
    Property Staple() As Int32
    Property StapleBatch() As Int32
    Property SearchReference() As String
    Property Description() As String
    Function GetPrintJobView(ByVal PrintJobId As Int32) As ReportData.BOReportView()
End Interface
