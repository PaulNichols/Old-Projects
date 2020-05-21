Public Interface IReportAuthorisedPrintJob
    Property IsAuthorised() As Boolean
    Property ReportAuthorisedQId() As Int32
    Property ReportId() As Int32
    Property ReportPrinterId() As Int32
    Property PrintSequence() As Int32
    Property PausedBy() As String
    Property PausedDate() As Object
    Property AuthorisedBy() As String
    Property AuthorisedDate() As Date
    Property PrintingDate() As Object
    Property PrintedDate() As Object
    Property DeletedBy() As String
    Property DeletedDate() As Object
    Property LastStatusMessage() As String
    Property StapleOff() As Boolean
    Property BOTypeName() As String
    Property StapleStartPage() As Int32
    Property StapleEndPage() As Int32
    Property StapleBatch() As Int32
    Property SearchReference() As String
    Property ReportOutput() As Byte()

End Interface
