Public Interface IReportAuthorisedQ

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

End Interface
