Public Interface IBoReportSearchResults
    Property ReportId() As Int32
    Property PrintJobDescription() As String
    Property ReportTypeDescription() As String
    Property ReportViewURL() As String
    Property SearchReference() As String
    Property Description() As String
    Property CreatedDate() As Date
    Property CreatedDate_NavigateUrl() As String

    Function GetReportSearchView(ByVal reportTypeIds() As Int32, ByVal fromDate As Date, ByVal toDate As Date) As ReportData.BoReportSearchResults()
End Interface
