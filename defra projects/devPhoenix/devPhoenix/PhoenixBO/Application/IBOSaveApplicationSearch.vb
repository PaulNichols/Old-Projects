Namespace Application
    Public Interface IBOSaveApplicationSearch

        Property Criteria() As String
        Property Modified() As Date
        Property UserId() As Decimal
        Property WorkList() As Boolean
    End Interface

    Public Interface IBOSaveApplicationSearchLite

        Property SavedSearchId() As Int32
        Property Description() As String
    End Interface
end Namespace 