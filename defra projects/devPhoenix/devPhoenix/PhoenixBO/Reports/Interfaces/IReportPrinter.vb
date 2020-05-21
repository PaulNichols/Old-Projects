<Serializable()> _
Public Enum QueueStatuses
    Authorised
    Printing
    Printed
    Deleted
End Enum

Public Interface IReportPrinter

    Property ReportPrinterId() As Int32
    Property Name() As String
    Property NetworkPath() As String
    Property PausedDate() As Object
    Property PausedBy() As Object
    Property StatusDescription() As String
    Property Status() As String
    Property QueueStatus() As QueueStatuses

End Interface