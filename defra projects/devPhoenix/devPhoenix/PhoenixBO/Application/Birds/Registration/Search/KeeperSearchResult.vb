Namespace Application.Bird.Registration.Search
    <Serializable()> _
    Public Class KeeperSearchResult
        Public Sub New()
        End Sub

        Public Property PartyId() As String
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As String)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As String

        Public Property PartyName() As String
            Get
                Return mPartyName
            End Get
            Set(ByVal Value As String)
                mPartyName = Value
            End Set
        End Property
        Private mPartyName As String

        'i.e the date the specimen was registered!
        Public Property DateRegistered() As String
            Get
                Return mDateRegistered
            End Get
            Set(ByVal Value As String)
                mDateRegistered = Value
            End Set
        End Property
        Private mDateRegistered As String

        'i.e the specimen
        Public Property DateTransferred() As String
            Get
                Return mDateTransferred
            End Get
            Set(ByVal Value As String)
                mDateTransferred = Value
            End Set
        End Property
        Private mDateTransferred As String

        Public Property AddressLine1() As String
            Get
                Return mAddressLine1
            End Get
            Set(ByVal Value As String)
                mAddressLine1 = Value
            End Set
        End Property
        Private mAddressLine1 As String

        Public Property Postcode() As String
            Get
                Return mPostcode
            End Get
            Set(ByVal Value As String)
                mPostcode = Value
            End Set
        End Property
        Private mPostcode As String
    End Class
End Namespace
