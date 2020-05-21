Namespace Application
    Public Class BOApplicationPartyDetails
        Inherits BaseBO
        Implements IBOApplicationPartyDetails

        Public Sub New()
            'Party = New Party.BOParty
            'Address = New Party.BOReadOnlyAddress
            MyBase.New()
        End Sub

        Public Sub New(ByVal partyLinkId As Int32, ByVal tran As SqlClient.SqlTransaction)
            Try
                Dim Link As New DataObjects.Entity.PartyLink(partyLinkId, tran)
                If Not Link Is Nothing Then
                    mParty = New Party.BOParty(Link.PartyId, tran)
                    If mParty.IsBusiness Then
                        mParty = New Party.BOPartyBusiness(Link.PartyId, tran)
                    Else
                        mParty = New Party.BOPartyIndividual(Link.PartyId, tran)
                    End If
                    Dim MailingAddress As BO.Party.BOAddress = mParty.GetMailingAddress(tran)
                    Dim MailingAddressId As Object
                    If Not MailingAddress Is Nothing Then
                        MailingAddressId = MailingAddress.AddressId
                    End If
                    Dim PartyAddress As BO.party.BOAddress = New BO.party.BOAddress(New DataObjects.Entity.Address(Link.AddressId, tran), MailingAddressId, mParty.PartyId)
                    If Not PartyAddress Is Nothing Then
                        mAddress = New BO.Party.BOReadOnlyAddress(PartyAddress, tran)
                    End If
                    mLinkId = partyLinkId
                    CheckSum = Link.CheckSum
                End If
            Catch
                ' the party did not exist!!!
            End Try
        End Sub

        Public Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BOApplicationPartyDetails
            MyBase.Save()
            Dim NewApplicationPartyDetails As New DataObjects.Entity.PartyLink
            Dim service As DataObjects.Service.PartyLinkService = NewApplicationPartyDetails.ServiceObject
            Dim linkset As DataObjects.EntitySet.PartyLinkSet = service.GetByIndex_IX_PartyAddress(mParty.PartyId, mAddress.AddressId, tran)

            If linkset Is Nothing OrElse linkset.Count = 0 Then 'MLD 1/12/4 modified to only insert if doesn't exist
                NewApplicationPartyDetails = service.Insert(mParty.PartyId, _
                                                            mAddress.AddressId, _
                                                            tran)
                Created = True
                'check to see if any SQL errors have occured
                If (NewApplicationPartyDetails Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                    'TODO: Use errors collection to check to see if the problem was concurrency
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveParty)
                ElseIf Created And Not NewApplicationPartyDetails Is Nothing Then
                    mLinkId = NewApplicationPartyDetails.Id
                    CheckSum = NewApplicationPartyDetails.CheckSum
                End If
            ElseIf linkset.Count > 0 Then
                Me.LinkId = linkset.Entities(0).Id 'set the id to the link id found in the db
            End If
            Return Me
        End Function

        Public Property Address() As Party.BOReadOnlyAddress Implements IBOApplicationPartyDetails.Address
            Get
                Return mAddress
            End Get
            Set(ByVal Value As Party.BOReadOnlyAddress)
                mAddress = Value
            End Set
        End Property
        Private mAddress As Party.BOReadOnlyAddress

        Public Property Party() As Party.BOParty Implements IBOApplicationPartyDetails.Party
            Get
                Return mParty
            End Get
            Set(ByVal Value As Party.BOParty)
                mParty = Value
            End Set
        End Property
        Private mParty As Party.BOParty

        Public Property LinkId() As Integer Implements IBOApplicationPartyDetails.LinkId
            Get
                Return mLinkId
            End Get
            Set(ByVal Value As Integer)
                mLinkId = Value
            End Set
        End Property
        Private mLinkId As Int32
    End Class
End Namespace