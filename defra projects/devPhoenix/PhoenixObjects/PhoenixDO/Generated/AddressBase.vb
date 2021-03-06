'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.2032
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Base
    
    'Base entity implementation for table 'Address'
    '*DO NOT* modify this file.
    'Add new properties and methods to Address instead.
    Public MustInherit Class AddressBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal addressId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(addressId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal addressId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(addressId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        <EnterpriseObjects.Attributes.FieldDescription("Unique ID")>  _
        Public Property AddressId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Unique ID")>  _
        Public Overrides Property Id As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(200),  _
         EnterpriseObjects.Attributes.FieldDescription("Address line 1")>  _
        Public Property Address1 As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(100),  _
         EnterpriseObjects.Attributes.FieldDescription("Address line 2")>  _
        Public Property Address2 As String
            Get
                If (Me.IsAddress2Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),String)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(100),  _
         EnterpriseObjects.Attributes.FieldDescription("Address line 3")>  _
        Public Property Address3 As String
            Get
                If (Me.IsAddress3Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),String)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(100),  _
         EnterpriseObjects.Attributes.FieldDescription("Address line 4")>  _
        Public Property Address4 As String
            Get
                If (Me.IsAddress4Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),String)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50),  _
         EnterpriseObjects.Attributes.FieldDescription("Post town")>  _
        Public Property Town As String
            Get
                Return CType(Me(5),String)
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(30),  _
         EnterpriseObjects.Attributes.FieldDescription("County")>  _
        Public Property County As String
            Get
                If (Me.IsCountyNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),String)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(20),  _
         EnterpriseObjects.Attributes.FieldDescription("Postal code")>  _
        Public Property Postcode As String
            Get
                If (Me.IsPostcodeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),String)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("FK to Country table")>  _
        Public Property CountryId As Integer
            Get
                Return CType(Me(8),Integer)
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Temporary address")>  _
        Public Property IsTemporary As Boolean
            Get
                Return CType(Me(9),Boolean)
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Is row active?")>  _
        Public Property Active As Boolean
            Get
                Return CType(Me(10),Boolean)
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(100)>  _
        Public Property ContactName As String
            Get
                If (Me.IsContactNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(11),String)
                End If
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        Public Property RegionId As Integer
            Get
                If (Me.IsRegionIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(12),Integer)
                End If
            End Get
            Set
                Me(12) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property HouseNumber As String
            Get
                If (Me.IsHouseNumberNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(13),String)
                End If
            End Get
            Set
                Me(13) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(3000)>  _
        Public Property ReportAddress As String
            Get
                If (Me.IsReportAddressNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(14),String)
                End If
            End Get
            Set
                Me(14) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(100)>  _
        Public Property BuildingName As String
            Get
                If (Me.IsBuildingNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(15),String)
                End If
            End Get
            Set
                Me(15) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(100)>  _
        Public Property OrganisationName As String
            Get
                If (Me.IsOrganisationNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(16),String)
                End If
            End Get
            Set
                Me(16) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(3000)>  _
        Public Property GridAddress As String
            Get
                If (Me.IsGridAddressNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(17),String)
                End If
            End Get
            Set
                Me(17) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(18),Integer)
                End If
            End Get
            Set
                Me(18) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.AddressService
            Get
                Return CType(GetServiceObject(GetType(Service.AddressService)),Service.AddressService)
            End Get
        End Property
        
        Public Overridable Property RawDataset As System.Data.DataSet Implements EnterpriseObjects.IUpdatable.RawDataset
            Get
                Return mRawDataset
            End Get
            Set
                mRawDataset = value
            End Set
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsAddress2Null() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetAddress2ToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsAddress3Null() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetAddress3ToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsAddress4Null() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetAddress4ToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsCountyNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetCountyToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsPostcodeNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetPostcodeToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsContactNameNull() As Boolean
            Return Me.IsNull(11)
        End Function
        
        Public Sub SetContactNameToNull()
            Me(11) = System.DBNull.Value
        End Sub
        
        Public Function IsRegionIdNull() As Boolean
            Return Me.IsNull(12)
        End Function
        
        Public Sub SetRegionIdToNull()
            Me(12) = System.DBNull.Value
        End Sub
        
        Public Function IsHouseNumberNull() As Boolean
            Return Me.IsNull(13)
        End Function
        
        Public Sub SetHouseNumberToNull()
            Me(13) = System.DBNull.Value
        End Sub
        
        Public Function IsReportAddressNull() As Boolean
            Return Me.IsNull(14)
        End Function
        
        Public Sub SetReportAddressToNull()
            Me(14) = System.DBNull.Value
        End Sub
        
        Public Function IsBuildingNameNull() As Boolean
            Return Me.IsNull(15)
        End Function
        
        Public Sub SetBuildingNameToNull()
            Me(15) = System.DBNull.Value
        End Sub
        
        Public Function IsOrganisationNameNull() As Boolean
            Return Me.IsNull(16)
        End Function
        
        Public Sub SetOrganisationNameToNull()
            Me(16) = System.DBNull.Value
        End Sub
        
        Public Function IsGridAddressNull() As Boolean
            Return Me.IsNull(17)
        End Function
        
        Public Sub SetGridAddressToNull()
            Me(17) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(18)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(18) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(19)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.AddressSet
            Return AddressBase.GetAll(false, false, AddressServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.AddressSet
            Return AddressBase.GetAll(includeHyphen, false, AddressServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As AddressServiceBase.OrderBy) As EntitySet.AddressSet
            Dim service As Service.AddressService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As AddressServiceBase.OrderBy) As EntitySet.AddressSet
            Return AddressBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal addressId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Address
            Dim service As Service.AddressService
            service = ServiceObject
            Return service.GetById(AddressId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal addressId As Integer) As Entity.Address
            Dim service As Service.AddressService
            service = ServiceObject
            Return service.GetById(AddressId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal addressId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.AddressService
            service = ServiceObject
            Return service.DeleteById(addressId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal addressId As Integer) As Boolean
            Return AddressBase.DeleteById(addressId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal addressId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return AddressBase.DeleteById(addressId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.AddressSet
            Dim service As Service.AddressService
            service = ServiceObject
            Return service.GetForCountry(countryId, tran)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer) As EntitySet.AddressSet
            Return AddressBase.GetForCountry(countryId, Nothing)
        End Function
        
        Public Overloads Function GetRelatedPartyAddresses(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartyAddressSet
            Return Entity.PartyAddress.GetForAddress(Me.AddressId, tran)
        End Function
        
        Public Overloads Function GetRelatedPartyAddresses() As EntitySet.PartyAddressSet
            Return Me.GetRelatedPartyAddresses(Nothing)
        End Function
        
        Public Overloads Function GetRelatedPartyLink(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartyLinkSet
            Return Entity.PartyLink.GetForAddress(Me.AddressId, tran)
        End Function
        
        Public Overloads Function GetRelatedPartyLink() As EntitySet.PartyLinkSet
            Return Me.GetRelatedPartyLink(Nothing)
        End Function
        
        Public Overloads Function GetRelatedPartySpecimen(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PartySpecimenSet
            Return Entity.PartySpecimen.GetForAddress(Me.AddressId, tran)
        End Function
        
        Public Overloads Function GetRelatedPartySpecimen() As EntitySet.PartySpecimenSet
            Return Me.GetRelatedPartySpecimen(Nothing)
        End Function
        
        Public Overloads Function GetRelatedCITESApplication(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Return Entity.CITESApplication.GetForAddress(Me.AddressId, tran)
        End Function
        
        Public Overloads Function GetRelatedCITESApplication() As EntitySet.CITESApplicationSet
            Return Me.GetRelatedCITESApplication(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal address1 As String, ByVal address2 As Object, ByVal address3 As Object, ByVal address4 As Object, ByVal town As String, ByVal county As Object, ByVal postcode As Object, ByVal countryId As Integer, ByVal isTemporary As Boolean, ByVal active As Boolean, ByVal contactName As Object, ByVal regionId As Object, ByVal houseNumber As Object, ByVal buildingName As Object, ByVal organisationName As Object) As Entity.Address
            Return Entity.Address.ServiceObject.Insert(address1, address2, address3, address4, town, county, postcode, countryId, isTemporary, active, contactName, regionId, houseNumber, buildingName, organisationName)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim address1Param As String = Me.Address1
            Dim address2Param As Object
            If (Me.IsAddress2Null = false) Then
                address2Param = EnterpriseObjects.Common.ParseSQLText(Me.Address2)
            Else
                address2Param = System.DBNull.Value
            End If
            Dim address3Param As Object
            If (Me.IsAddress3Null = false) Then
                address3Param = EnterpriseObjects.Common.ParseSQLText(Me.Address3)
            Else
                address3Param = System.DBNull.Value
            End If
            Dim address4Param As Object
            If (Me.IsAddress4Null = false) Then
                address4Param = EnterpriseObjects.Common.ParseSQLText(Me.Address4)
            Else
                address4Param = System.DBNull.Value
            End If
            Dim townParam As String = Me.Town
            Dim countyParam As Object
            If (Me.IsCountyNull = false) Then
                countyParam = EnterpriseObjects.Common.ParseSQLText(Me.County)
            Else
                countyParam = System.DBNull.Value
            End If
            Dim postcodeParam As Object
            If (Me.IsPostcodeNull = false) Then
                postcodeParam = EnterpriseObjects.Common.ParseSQLText(Me.Postcode)
            Else
                postcodeParam = System.DBNull.Value
            End If
            Dim countryIdParam As Integer = Me.CountryId
            Dim isTemporaryParam As Boolean = Me.IsTemporary
            Dim activeParam As Boolean = Me.Active
            Dim contactNameParam As Object
            If (Me.IsContactNameNull = false) Then
                contactNameParam = EnterpriseObjects.Common.ParseSQLText(Me.ContactName)
            Else
                contactNameParam = System.DBNull.Value
            End If
            Dim regionIdParam As Object
            If (Me.IsRegionIdNull = false) Then
                regionIdParam = Me.RegionId
            Else
                regionIdParam = System.DBNull.Value
            End If
            Dim houseNumberParam As Object
            If (Me.IsHouseNumberNull = false) Then
                houseNumberParam = EnterpriseObjects.Common.ParseSQLText(Me.HouseNumber)
            Else
                houseNumberParam = System.DBNull.Value
            End If
            Dim buildingNameParam As Object
            If (Me.IsBuildingNameNull = false) Then
                buildingNameParam = EnterpriseObjects.Common.ParseSQLText(Me.BuildingName)
            Else
                buildingNameParam = System.DBNull.Value
            End If
            Dim organisationNameParam As Object
            If (Me.IsOrganisationNameNull = false) Then
                organisationNameParam = EnterpriseObjects.Common.ParseSQLText(Me.OrganisationName)
            Else
                organisationNameParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.Address.ServiceObject.Update(Me.Id, address1Param, address2Param, address3Param, address4Param, townParam, countyParam, postcodeParam, countryIdParam, isTemporaryParam, activeParam, contactNameParam, regionIdParam, houseNumberParam, buildingNameParam, organisationNameParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
