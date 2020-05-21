
Namespace Application
    Public Interface IBOApplicationPartyDetails
        Property Party() As Party.BOParty
        Property Address() As Party.BOReadOnlyAddress
        Property LinkId() As Int32
    End Interface
End Namespace