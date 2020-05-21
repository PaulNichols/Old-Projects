Public Interface IBOReportResults
    Property ReportId() As Int32
    Property CreatedDate() As Date
    Property RPT() As Byte()
    Property ReportOutput() As Byte()
    Property Data() As Object
    Property Criteria() As Object
    Property TypeId() As Int32
    Property ReportTypeDescription() As String
    Property BoTypeName() As String
    Property ContentType() As String
    Property OutputType() As Int32
    Property Version() As Int32
    Property DataBaseId() As Int32
    Property SearchReference() As String
    Property ReportPrintJobId() As Int32
    Property PrintJobDescription() As String
    Property PrintSequence() As Int32
    Property ExpiryDate() As Date
    Property ReportPrinterId() As Int32
    Property StapleStartPage() As Int32
    Property StapleEndPage() As Int32
    Property StapleBatch() As Int32
    Property Size() As Int32
    Property PrinterSelection() As uk.gov.defra.Phoenix.bo.ReportData.BoReportPrinter()
    Property StapleSelection() As uk.gov.defra.Phoenix.bo.ReportData.BoReportStaple()
    Property Staple() As Int32
    Property Description() As String

End Interface

