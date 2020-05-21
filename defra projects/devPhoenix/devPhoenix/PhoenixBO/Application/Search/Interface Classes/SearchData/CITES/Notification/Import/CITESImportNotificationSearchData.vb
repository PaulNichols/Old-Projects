Namespace Application.Search.Data
    Public MustInherit Class CITESImportNotificationSearchData
        Inherits BaseCITESNotificationSearchData
        Implements INotificationReference

        Public Sub New()
        End Sub

        Public Property NotificationReference() As String Implements INotificationReference.NotificationReference
            Get
                Return mNotificationReference
            End Get
            Set(ByVal Value As String)
                mNotificationReference = Value
            End Set
        End Property
        Private mNotificationReference As String
    End Class
End Namespace