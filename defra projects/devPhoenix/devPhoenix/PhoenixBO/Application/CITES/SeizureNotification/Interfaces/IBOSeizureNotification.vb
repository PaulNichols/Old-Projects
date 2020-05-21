Namespace Application.CITES
    Public Interface IBOSeizureNotification
        Inherits IBOCITESNotification

        Property SeizureNotificationId() As Int32
        Property CustomsReference() As String
        Property PortOfEntryID() As Object
        Property PortOfEntry() As String
        Property Reason() As Object
        Property CheckSum() As Int32
        Property NotificationType() As Application.ApplicationTypes
        Property CITESNotificationId() As Int32
        Property SingleSpecie() As Application.cites.BONotificationSpecie

        Overloads Function LinkToPermit(ByVal permitId As Int32) As Boolean
        Overloads Function LinkToPermit(ByVal permitId() As Int32) As Boolean
        Overloads Function LinkToPermitRetrospectively(ByVal permitId As Int32) As Boolean
        Overloads Function LinkToPermitRetrospectively(ByVal permitId() As Int32) As Boolean
        Overloads Function LinkToPermit(ByVal permitId() As Int32, ByVal setRetrospective As Boolean) As Boolean

        ' Overloads Function Validate(ByVal userid As Int32, ByVal writeFlag As Boolean) As ValidationManager

    End Interface
End Namespace