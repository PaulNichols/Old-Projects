Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Party
    <Serializable()> _
    Public Class BOPartyBusiness
        Inherits BOParty
        Implements IBusiness
        'Private mPartyId As Int32

        Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyBase.New(id, tran)
        End Sub

        Public Sub New(ByVal id As Int32)
            MyBase.New(id, Nothing)
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overloads Overrides Sub InitialiseParty(ByVal party As Entity.Party, ByVal userId As Int64, ByVal tran As SqlClient.SqlTransaction)
            MyBase.InitialiseParty(party, userId, tran)
            PartyId = party.Id
            With party.GetRelatedBusiness(tran)
                If .Count = 1 Then
                    InitialiseBusiness(CType(.GetEntity(0), Entity.Business))
                End If
                Me.CITESRegisteredNumber = CType(.GetEntity(0), Entity.Business).CITESRegisteredNumber

            End With

            'With uk.gov.defra.Phoenix.DO.Entity.Business.GetById(party.BusinessId)
            '    BusinessName = .BusinessName
            '    BusinessTypeId = .BusinessTypeId
            'End With
        End Sub

        Public Overrides Property DisplayName() As String Implements IBusiness.DisplayName
            Get
                Return mBusinessName
            End Get
            Set(ByVal Value As String)
                mBusinessName = Value
            End Set
        End Property

        Public Property CITESRegisteredNumber() As String Implements IBusiness.CITESRegisteredNumber
            Get
                Return mCITESRegisteredNumber
            End Get
            Set(ByVal Value As String)
                mCITESRegisteredNumber = Value
            End Set
        End Property
        Private mCITESRegisteredNumber As String

        Public Property DefaultMAForCountry As Boolean Implements IBusiness.DefaultMAForCountry
            Get
                Return mDefaultMAForCountry
            End Get
            Set
                mDefaultMAForCountry = Value
            End Set
        End Property
        Private mDefaultMAForCountry As Boolean


        Public Property BusinessCheckSum() As Int32 Implements IBusiness.CheckSum
            Get
                Return mBusinessCheckSum
            End Get
            Set(ByVal Value As Int32)
                mBusinessCheckSum = Value
            End Set
        End Property
        Private mBusinessCheckSum As Int32

        Private Sub InitialiseBusiness(ByVal data As Entity.Business)
            With data
                BusinessId = .Id
                BusinessName = .BusinessName
                BusinessTypeId = .BusinessTypeId
                BusinessType = Nothing
                DefaultMAForCountry = .DefaultManagementForCountry
                BusinessCheckSum = .CheckSum
            End With
        End Sub

        Public Property BusinessTypeId() As Int32 Implements IBusiness.BusinessTypeId
            Get
                Return mBusinessTypeId
            End Get
            Set(ByVal Value As Int32)
                mBusinessTypeId = Value
            End Set
        End Property
        Private mBusinessTypeId As Int32

        Public Property BusinessId() As Int32 Implements IBusiness.BusinessId
            Get
                Return mBusinessId
            End Get
            Set(ByVal Value As Int32)
                mBusinessId = Value
            End Set
        End Property
        Private mBusinessId As Int32

        Public Property BusinessName() As String Implements IBusiness.BusinessName
            Get
                Return mBusinessName
            End Get
            Set(ByVal Value As String)
                mBusinessName = Value
            End Set
        End Property
        Private mBusinessName As String

        Public Property BusinessType() As String Implements IBusiness.BusinessType
            Get
                If mBusinessType Is Nothing Then
                    Dim BusinessTypeData As Entity.BusinessType = Entity.BusinessType.GetById(mBusinessTypeId)
                    If Not BusinessTypeData Is Nothing Then
                        mBusinessType = BusinessTypeData.Description
                        BusinessTypeData = Nothing
                    End If
                End If
                Return mBusinessType
            End Get
            Set(ByVal Value As String)
                mBusinessType = Value
            End Set
        End Property
        Private mBusinessType As String

        Public Function GetRepresentatives() As BOPerson() Implements IBusiness.GetRepresentatives
            Return GetRepresentatives(PartyId)
        End Function

        Shared Function GetRepresentatives(ByVal partyId As Int32) As BOPerson()
            Try
                Dim Party As New Entity.Party(partyId)
                Return GetRepresentatives(Party)
            Catch
            End Try
        End Function

        Shared Function GetRepresentatives(ByVal party As Entity.Party) As BOPerson()
            Dim Persons As EntitySet.PersonSet = party.GetRelatedPerson

            If Not Persons Is Nothing AndAlso _
               Persons.Count > 0 Then
                Dim PersonsList(Persons.Count - 1) As BOPerson
                Dim Index As Int32 = 0
                For Each person As Entity.Person In Persons
                    PersonsList(Index) = New party.BOPerson(person, party.Id)
                    Index += 1
                Next person
                Return PersonsList
            Else
                Return Nothing
            End If
        End Function

        Private Sub ClearDefaultManagementAuthority(ByVal tran As SqlClient.SqlTransaction)
            'Find all MAs for this businesses country and set the default to 0
            Dim Params As New Party.Search.SearchParameters_Business
            If Not MailingAddressId Is Nothing AndAlso CType(MailingAddressId, Int32) > 0 Then
                Dim address   As New BO.Party.BOAddress(CType(MailingAddressId, Int32), tran)
                Dim biz       As New Entity.Business
                Dim biztypeId As Int32 = CType(BO.Party.BOBusinessType.BusinessTypes.Management_Authority, Int32)

                biz.UnDefaultCountryManagementAuthorities(biztypeId, address.CountryId, tran)   'MLD 6/4/5 order of params changed
            End If
        End Sub

        Protected Overrides Function PreSave(ByVal tran As SqlClient.SqlTransaction) As Boolean
            Created = (BusinessId = 0)
            Dim NewBusiness As New Entity.Business
            Dim service As Service.BusinessService = NewBusiness.ServiceObject

            If DefaultMAForCountry Then                 'if this MA is now the default...
                ClearDefaultManagementAuthority(tran)   'clear any others for this country
            End If

            If Created Then
                NewBusiness = service.Insert(mBusinessName, _
                                             mBusinessTypeId, _
                                             PartyId, _
                                             mCITESRegisteredNumber, _
                                              mDefaultMAForCountry, _
                                             tran)
            Else
                NewBusiness = service.Update(mBusinessId, _
                                             mBusinessName, _
                                             mBusinessTypeId, _
                                             PartyId, _
                                             mCITESRegisteredNumber, _
                                             mDefaultMAForCountry, _
                                             mBusinessCheckSum, _
                                             tran)
            End If
            If NewBusiness Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                Return False
            ElseIf NewBusiness Is Nothing Then
                Return False
            Else
                If NewBusiness.CheckSum <> mBusinessCheckSum Then
                    InitialiseBusiness(NewBusiness)
                End If
                Return True
            End If
        End Function

        'Public Overrides Function Delete() As Object

        '    MyBase.Delete()
        'End Function


    End Class
End Namespace