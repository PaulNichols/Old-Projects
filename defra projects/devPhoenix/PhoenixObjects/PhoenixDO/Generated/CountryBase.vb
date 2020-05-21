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
    
    'Base entity implementation for table 'Country'
    '*DO NOT* modify this file.
    'Add new properties and methods to Country instead.
    Public MustInherit Class CountryBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(countryId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal countryId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(countryId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property CountryId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Overrides Property Id As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(2)>  _
        Public Property ISO2CountryCode As String
            Get
                If (Me.IsISO2CountryCodeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),String)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(3)>  _
        Public Property ISO3CountryCode As String
            Get
                If (Me.IsISO3CountryCodeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),String)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property ShortName As String
            Get
                Return CType(Me(3),String)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(70)>  _
        Public Property LongName As String
            Get
                Return CType(Me(4),String)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(55)>  _
        Public Property CodeDescription As String
            Get
                If (Me.IsCodeDescriptionNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),String)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property Active As Boolean
            Get
                Return CType(Me(6),Boolean)
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Property CountryBRU As Boolean
            Get
                Return CType(Me(7),Boolean)
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property ISO3166 As Boolean
            Get
                Return CType(Me(8),Boolean)
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Property ManagementCountryId As Integer
            Get
                If (Me.IsManagementCountryIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(9),Integer)
                End If
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(10),Integer)
                End If
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.CountryService
            Get
                Return CType(GetServiceObject(GetType(Service.CountryService)),Service.CountryService)
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
        
        Public Function IsISO2CountryCodeNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetISO2CountryCodeToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsISO3CountryCodeNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetISO3CountryCodeToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsCodeDescriptionNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetCodeDescriptionToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsManagementCountryIdNull() As Boolean
            Return Me.IsNull(9)
        End Function
        
        Public Sub SetManagementCountryIdToNull()
            Me(9) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(10)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(10) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(11)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.CountrySet
            Return CountryBase.GetAll(false, false, CountryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.CountrySet
            Return CountryBase.GetAll(includeHyphen, false, CountryServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As CountryServiceBase.OrderBy) As EntitySet.CountrySet
            Dim service As Service.CountryService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As CountryServiceBase.OrderBy) As EntitySet.CountrySet
            Return CountryBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Country
            Dim service As Service.CountryService
            service = ServiceObject
            Return service.GetById(CountryId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal countryId As Integer) As Entity.Country
            Dim service As Service.CountryService
            service = ServiceObject
            Return service.GetById(CountryId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal countryId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.CountryService
            service = ServiceObject
            Return service.DeleteById(countryId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal countryId As Integer) As Boolean
            Return CountryBase.DeleteById(countryId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal countryId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return CountryBase.DeleteById(countryId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CountrySet
            Dim service As Service.CountryService
            service = ServiceObject
            Return service.GetForCountry(countryId, tran)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer) As EntitySet.CountrySet
            Return CountryBase.GetForCountry(countryId, Nothing)
        End Function
        
        Public Overloads Function GetRelatedExportedCountryIdCITESNotification(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Return Entity.CITESNotification.GetForExportedCountryIdCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedExportedCountryIdCITESNotification() As EntitySet.CITESNotificationSet
            Return Me.GetRelatedExportedCountryIdCITESNotification(Nothing)
        End Function
        
        Public Overloads Function GetRelatedMemberStateOfImportIdCITESNotification(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Return Entity.CITESNotification.GetForMemberStateOfImportIdCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedMemberStateOfImportIdCITESNotification() As EntitySet.CITESNotificationSet
            Return Me.GetRelatedMemberStateOfImportIdCITESNotification(Nothing)
        End Function
        
        Public Overloads Function GetRelatedCITESPermit(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESPermitSet
            Return Entity.CITESPermit.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedCITESPermit() As EntitySet.CITESPermitSet
            Return Me.GetRelatedCITESPermit(Nothing)
        End Function
        
        Public Overloads Function GetRelatedCountry(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CountrySet
            Return Entity.Country.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedCountry() As EntitySet.CountrySet
            Return Me.GetRelatedCountry(Nothing)
        End Function
        
        Public Overloads Function GetRelatedExportApplication(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ExportApplicationSet
            Return Entity.ExportApplication.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedExportApplication() As EntitySet.ExportApplicationSet
            Return Me.GetRelatedExportApplication(Nothing)
        End Function
        
        Public Overloads Function GetRelatedGWDCountry(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.GWDCountrySet
            Return Entity.GWDCountry.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedGWDCountry() As EntitySet.GWDCountrySet
            Return Me.GetRelatedGWDCountry(Nothing)
        End Function
        
        Public Overloads Function GetRelatedImportApplication(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportApplicationSet
            Return Entity.ImportApplication.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedImportApplication() As EntitySet.ImportApplicationSet
            Return Me.GetRelatedImportApplication(Nothing)
        End Function
        
        Public Overloads Function GetRelatedPermit(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitSet
            Return Entity.Permit.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedPermit() As EntitySet.PermitSet
            Return Me.GetRelatedPermit(Nothing)
        End Function
        
        Public Overloads Function GetRelatedPermitInfo(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitInfoSet
            Return Entity.PermitInfo.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedPermitInfo() As EntitySet.PermitInfoSet
            Return Me.GetRelatedPermitInfo(Nothing)
        End Function
        
        Public Overloads Function GetRelatedSpecimenReport(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SpecimenReportSet
            Return Entity.SpecimenReport.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedSpecimenReport() As EntitySet.SpecimenReportSet
            Return Me.GetRelatedSpecimenReport(Nothing)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyBRU(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyBRUSet
            Return Entity.TaxonomyBRU.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyBRU() As EntitySet.TaxonomyBRUSet
            Return Me.GetRelatedTaxonomyBRU(Nothing)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyDecision(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyDecisionSet
            Return Entity.TaxonomyDecision.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyDecision() As EntitySet.TaxonomyDecisionSet
            Return Me.GetRelatedTaxonomyDecision(Nothing)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyExportQuota(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyExportQuotaSet
            Return Entity.TaxonomyExportQuota.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyExportQuota() As EntitySet.TaxonomyExportQuotaSet
            Return Me.GetRelatedTaxonomyExportQuota(Nothing)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyLegislation(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyLegislationSet
            Return Entity.TaxonomyLegislation.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyLegislation() As EntitySet.TaxonomyLegislationSet
            Return Me.GetRelatedTaxonomyLegislation(Nothing)
        End Function
        
        Public Overloads Function GetRelatedTaxonomySpeciesCountryDistribution(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomySpeciesCountryDistributionSet
            Return Entity.TaxonomySpeciesCountryDistribution.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedTaxonomySpeciesCountryDistribution() As EntitySet.TaxonomySpeciesCountryDistributionSet
            Return Me.GetRelatedTaxonomySpeciesCountryDistribution(Nothing)
        End Function
        
        Public Overloads Function GetRelatedUKCountry(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.UKCountrySet
            Return Entity.UKCountry.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedUKCountry() As EntitySet.UKCountrySet
            Return Me.GetRelatedUKCountry(Nothing)
        End Function
        
        Public Overloads Function GetRelatedAddress(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.AddressSet
            Return Entity.Address.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedAddress() As EntitySet.AddressSet
            Return Me.GetRelatedAddress(Nothing)
        End Function
        
        Public Overloads Function GetRelatedCITESApplication(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Return Entity.CITESApplication.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedCITESApplication() As EntitySet.CITESApplicationSet
            Return Me.GetRelatedCITESApplication(Nothing)
        End Function
        
        Public Overloads Function GetRelatedCITESImportExportPermit(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESImportExportPermitSet
            Return Entity.CITESImportExportPermit.GetForCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedCITESImportExportPermit() As EntitySet.CITESImportExportPermitSet
            Return Me.GetRelatedCITESImportExportPermit(Nothing)
        End Function
        
        Public Overloads Function GetRelatedCountryOfOriginIdCITESNotification(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Return Entity.CITESNotification.GetForCountryOfOriginIdCountry(Me.CountryId, tran)
        End Function
        
        Public Overloads Function GetRelatedCountryOfOriginIdCITESNotification() As EntitySet.CITESNotificationSet
            Return Me.GetRelatedCountryOfOriginIdCITESNotification(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal iSO2CountryCode As Object, ByVal iSO3CountryCode As Object, ByVal shortName As String, ByVal longName As String, ByVal active As Boolean, ByVal countryBRU As Boolean, ByVal iSO3166 As Boolean, ByVal managementCountryId As Object) As Entity.Country
            Return Entity.Country.ServiceObject.Insert(iSO2CountryCode, iSO3CountryCode, shortName, longName, active, countryBRU, iSO3166, managementCountryId)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim iSO2CountryCodeParam As Object
            If (Me.IsISO2CountryCodeNull = false) Then
                iSO2CountryCodeParam = Me.ISO2CountryCode
            Else
                iSO2CountryCodeParam = System.DBNull.Value
            End If
            Dim iSO3CountryCodeParam As Object
            If (Me.IsISO3CountryCodeNull = false) Then
                iSO3CountryCodeParam = Me.ISO3CountryCode
            Else
                iSO3CountryCodeParam = System.DBNull.Value
            End If
            Dim shortNameParam As String = Me.ShortName
            Dim longNameParam As String = Me.LongName
            Dim activeParam As Boolean = Me.Active
            Dim countryBRUParam As Boolean = Me.CountryBRU
            Dim iSO3166Param As Boolean = Me.ISO3166
            Dim managementCountryIdParam As Object
            If (Me.IsManagementCountryIdNull = false) Then
                managementCountryIdParam = Me.ManagementCountryId
            Else
                managementCountryIdParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.Country.ServiceObject.Update(Me.Id, iSO2CountryCodeParam, iSO3CountryCodeParam, shortNameParam, longNameParam, activeParam, countryBRUParam, iSO3166Param, managementCountryIdParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
