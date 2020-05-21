Namespace Application
    Public Interface IBOAdditionalDeclaration
        Property AdditionalDeclarationId() As Int32
        Property CITESApplicationId() As Int32

        Property OtherInformation() As String
        Property HadRejectedCITESApplication() As String
        Property FurtherPossesionDetails() As String
        Property DeclarationAcknowledged() As Boolean
        Property ConfirmAddress() As Boolean
        Property FalseStatement() As Boolean
        Property ApplicationDate() As Object

        Property HadRejectedCITESApplication_Boolean() As Boolean

        ReadOnly Property Confirmed() As Boolean
    End Interface
End Namespace