Namespace Application.Search.Data
    Public Class BaseCITESNotificationSearchData
        Inherits BaseCITESSearchData
        Implements IScientificName, IQuantity

        Public Sub New()
        End Sub

        Public Property Status() As String
            Get
                Return mStatus
            End Get
            Set(ByVal Value As String)
                mStatus = Value
            End Set
        End Property
        Private mStatus As String

        Public Property ScientificName() As String Implements IScientificName.ScientificName
            Get
                Return mScientificName
            End Get
            Set(ByVal Value As String)
                mScientificName = Value
            End Set
        End Property
        Private mScientificName As String

        Public Property Quantity() As String Implements IQuantity.Quantity
            Get
                Return mQuantity
            End Get
            Set(ByVal Value As String)
                mQuantity = Value
            End Set
        End Property
        Private mQuantity As String

        'Public Property NotificationId() As Int32
        '    Get
        '        Return mNotificationId
        '    End Get
        '    Set(ByVal Value As Int32)
        '        mNotificationId = Value
        '    End Set
        'End Property
        'Private mNotificationId As Int32
    End Class
End Namespace