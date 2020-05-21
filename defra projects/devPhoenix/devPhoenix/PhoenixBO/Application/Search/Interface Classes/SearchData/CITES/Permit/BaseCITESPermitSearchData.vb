Namespace Application.Search.Data
    Public MustInherit Class BaseCITESPermitSearchData
        Inherits BaseCITESSearchData
        Implements ISearchDataParty

        Public Sub New()
        End Sub

        Public Property PartyId() As String Implements ISearchDataParty.PartyId
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As String)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As String

        Public Property PartyName() As String Implements ISearchDataParty.PartyName
            Get
                Return mPartyName
            End Get
            Set(ByVal Value As String)
                mPartyName = Value
            End Set
        End Property
        Private mPartyName As String

        Public Property PermitType() As String
            Get
                Return mPermitType
            End Get
            Set(ByVal Value As String)
                mPermitType = Value
            End Set
        End Property
        Private mPermitType As String

        Public Property Status() As String
            Get
                Return mStatus
            End Get
            Set(ByVal Value As String)
                mStatus = Value
            End Set
        End Property
        Private mStatus As String

        Public Property PermitTypeId() As Application.ApplicationTypes
            Get
                Return mPermitTypeId
            End Get
            Set(ByVal Value As Application.ApplicationTypes)
                mPermitTypeId = Value
            End Set
        End Property
        Private mPermitTypeId As Application.ApplicationTypes
    End Class
End Namespace