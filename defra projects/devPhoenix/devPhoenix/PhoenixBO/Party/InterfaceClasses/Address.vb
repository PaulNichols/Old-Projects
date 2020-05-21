Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Party
    Public Class BOAddress
        Inherits BaseBOAddress

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            LoadAddress(id, tran)
        End Sub

        Private Function LoadAddress(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Address
            Dim NewAddress As DataObjects.Entity.Address = DataObjects.Entity.Address.GetById(id, tran)
            If NewAddress Is Nothing Then
                Throw New RecordDoesNotExist("Address", id)
            Else
                Me.InitialiseAddress(NewAddress)
                Return NewAddress
            End If
        End Function

        Public Sub New(ByVal data As DataObjects.Entity.Address, ByVal mailingAddressId As Object, ByVal partyId As Int32)
            MyClass.New()
            mMailingAddressId = mailingAddressId
            MyBase.PartyId = partyId
            InitialiseAddress(data)
        End Sub


        Private Sub InitialiseAddress(ByVal address As DataObjects.Entity.Address)
            With address
                AddressId = .Id
                Address1 = .Address1
                HouseNumber = .HouseNumber
                OrganisationName = .OrganisationName
                BuildingName = .BuildingName
                If Not .IsAddress2Null Then Address2 = .Address2
                If Not .IsAddress3Null Then Address3 = .Address3
                If Not .IsAddress4Null Then Address4 = .Address4
                Town = .Town
                If Not .IsCountyNull Then County = .County
                Region = Nothing
                If Not .IsRegionIdNull Then RegionId = .RegionId
                If Not .IsPostcodeNull Then Postcode = .Postcode
                IsTemporary = .IsTemporary
                'set to GB as default
                Dim ConfigurationSettings As New BOConfiguration
                If .CountryId = 0 Then
                    Dim Result As Object = ConfigurationSettings.GetValue("DefaultCountry")
                    If Not Result Is Nothing AndAlso _
                        ConfigurationSettings.IsInt32(Result) Then
                        'SCS 23/3/05 - Not required as reading the country property causes the application
                        'to go off and load the country description.  Not point in setting the value unless
                        'it's used...especially as reading it later does the same thing...
                        'Dim DefaultCountry As New BO.ReferenceData.BOCountry(CType(Result, Int32))
                        'Country = DefaultCountry.Description
                        CountryId = CType(Result, Int32)
                    End If
                Else
                    CountryId = .CountryId
                    Country = Nothing
                End If

                Active = .Active
                ContactName = .ContactName
                If Not mMailingAddressId Is Nothing AndAlso CType(mMailingAddressId, Int32) > 0 Then
                    IsMailing = (CType(mMailingAddressId, Int32) = AddressId)
                Else
                    IsMailing = False
                End If
                moriginalAddress = .ReportAddress

                CheckSum = .CheckSum
            End With
        End Sub

        Public Shadows Function Save() As BOAddress
            Created = (AddressId = 0)
            Dim NewAddress As New DataObjects.Entity.Address
            Dim service As DataObjects.Service.AddressService = NewAddress.ServiceObject

            Postcode = Postcode.ToUpper
            If Created Then
                NewAddress = service.Insert(Address1, _
                                            Address2, _
                                            Address3, _
                                            Address4, _
                                            Town, _
                                            County, _
                                            Postcode, _
                                            CountryId, _
                                            IsTemporary, _
                                            Active, _
                                            ContactName, _
                                            RegionId, _
                                            HouseNumber, _
                                            BuildingName, _
                                            OrganisationName)
            Else
                NewAddress = service.Update(AddressId, _
                                            Address1, _
                                            Address2, _
                                            Address3, _
                                            Address4, _
                                            Town, _
                                            County, _
                                            Postcode, _
                                            CountryId, _
                                            IsTemporary, _
                                            Active, _
                                            ContactName, _
                                            RegionId, _
                                            HouseNumber, _
                                            BuildingName, _
                                            OrganisationName, _
                                            CheckSum)
            End If
            If NewAddress Is Nothing Then
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveParty)
                Return Me
            Else
                InitialiseAddress(NewAddress)
                If Created AndAlso PartyId > 0 Then
                    Dim AddressJoin As New DataObjects.Entity.PartyAddress
                    Dim JoinService As DataObjects.Service.PartyAddressService = AddressJoin.ServiceObject
                    JoinService.Insert(AddressId, PartyId)
                End If
                Return Me
            End If
        End Function

        'Get an array of address ids for addresses where the postcode and either the building name or
        'house number match this address
        Public Function GetSimilarAddressIds() As Int32()   'MLD 26/11/4 added
            Dim service As New Service.AddressService
            Dim items1 As EntitySet.AddressSet = GetByPostcode(Postcode)
            Dim items2 As EntitySet.AddressSet = GetByPostcode(Postcode.Replace(" ", ""))
            Dim results(items1.Count + items2.Count - 2) As Int32
            Dim index As Int32 = 0
            
            AddIfMatching(results, index, items1)
            AddIfMatching(results, index, items2)
            Redim Preserve results(index - 1)
            Return results
        End Function

        Private Shared Function GetByPostcode(ByVal search As String) As EntitySet.AddressSet
            Dim service As New service.AddressService
            Dim items As EntitySet.AddressSet = service.GetByIndex_PostCode(search, False)
            If items Is Nothing Then Return New EntitySet.AddressSet
            Return items
        End Function

        Private Sub AddIfMatching(ByRef results As Int32(), ByRef index As Int32, ByVal items As EntitySet.AddressSet) 'MLD added 29/11/4
            For Each item As Entity.Address In items
                If item.AddressId <> AddressId AndAlso (item.BuildingName = BuildingName OrElse item.HouseNumber = HouseNumber) Then
                    results(index) = item.AddressId
                    index += 1
                End If
            Next
        End Sub

        Public Class AddressSearchCriteria
            Public Property PostCode() As String
                Get
                    Return mPostCode
                End Get
                Set(ByVal Value As String)
                    mPostCode = Value
                End Set
            End Property
            Private mPostCode As String
        End Class

        Public Shared Function AddressSearch(ByVal criteria As AddressSearchCriteria) As BOAddress()
            Dim AddressSet As EntitySet.AddressSet = GetByPostcode(criteria.PostCode)
            Dim ReturnAddresses As BOAddress()
            If Not AddressSet Is Nothing Then
                ReDim ReturnAddresses(AddressSet.Entities.Count - 1)
                Dim i As Int32
                For Each Address As Entity.Address In AddressSet
                    ReturnAddresses(i) = New BOAddress(Address.AddressId, Nothing)
                    i += 1
                Next
            End If
            Return ReturnAddresses
        End Function
    End Class
End Namespace