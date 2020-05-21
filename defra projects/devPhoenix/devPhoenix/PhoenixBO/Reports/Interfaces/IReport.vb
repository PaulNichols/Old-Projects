Public Interface IReport
    Property ReportId() As Int32
    Property CreatedDate() As Date
    Property ReportTypeId() As Int32
    Property Version() As Int32
    Property SearchReference() As String
    Property DatabaseId() As Int32
    Property ReportPrintJobId() As Int32
    Property PrintSequence() As Int32
    Property ExpiryDate() As Object
    Property Size() As Int32
    Property ReportPrinterId() As Int32
    Property Staple() As Int32
    Property Description() As String
End Interface
