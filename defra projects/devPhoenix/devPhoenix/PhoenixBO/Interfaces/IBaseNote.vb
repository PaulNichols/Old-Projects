Namespace BOCommon
    Public Interface IBaseNote
        Property NoteId() As Int32
        Property NoteDate() As Date
        Property Content() As String
        Property ShortContent() As String
        Property IsReadOnly() As Boolean
        Property Important() As Boolean
        Property Subject() As String
        'Property CreatedInfo() As Stamp
        'Property ModifiedInfo() As Stamp
        Property Active() As Boolean
        Property ModifiedDate() As Object
        Property CreatedDate() As Date

        Property ModifiedById() As Object
        Property ModifiedBy() As String

        Property CreatedById() As Object
        Property CreatedBy() As String

        Property OtherId() As Int32
    End Interface
End Namespace
