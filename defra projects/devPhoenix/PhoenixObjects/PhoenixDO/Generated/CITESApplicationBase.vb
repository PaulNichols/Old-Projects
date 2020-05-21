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
    
    'Base entity implementation for table 'CITESApplication'
    '*DO NOT* modify this file.
    'Add new properties and methods to CITESApplication instead.
    Public MustInherit Class CITESApplicationBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal citesApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(citesApplicationId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal citesApplicationId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(citesApplicationId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property CitesApplicationId As Integer
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
        
        Public Property ApplicationId As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property ManagementAuthorityId As Integer
            Get
                If (Me.IsManagementAuthorityIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property SecondPartyId As Integer
            Get
                If (Me.IsSecondPartyIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Integer)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property ForeignManagementAuthorityId As Integer
            Get
                If (Me.IsForeignManagementAuthorityIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property CountryOfImportId As Integer
            Get
                If (Me.IsCountryOfImportIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Integer)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property LocationAddressId As Integer
            Get
                If (Me.IsLocationAddressIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Property Consignment As Boolean
            Get
                If (Me.IsConsignmentNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),Boolean)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property IsComposite As Boolean
            Get
                Return CType(Me(8),Boolean)
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(9),Integer)
                End If
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.CITESApplicationService
            Get
                Return CType(GetServiceObject(GetType(Service.CITESApplicationService)),Service.CITESApplicationService)
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
        
        Public Function IsManagementAuthorityIdNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetManagementAuthorityIdToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsSecondPartyIdNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetSecondPartyIdToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsForeignManagementAuthorityIdNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetForeignManagementAuthorityIdToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsCountryOfImportIdNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetCountryOfImportIdToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsLocationAddressIdNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetLocationAddressIdToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsConsignmentNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetConsignmentToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(9)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(9) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(10)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.CITESApplicationSet
            Return CITESApplicationBase.GetAll(false, false, CITESApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.CITESApplicationSet
            Return CITESApplicationBase.GetAll(includeHyphen, false, CITESApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As CITESApplicationServiceBase.OrderBy) As EntitySet.CITESApplicationSet
            Dim service As Service.CITESApplicationService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As CITESApplicationServiceBase.OrderBy) As EntitySet.CITESApplicationSet
            Return CITESApplicationBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal citesApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.CITESApplication
            Dim service As Service.CITESApplicationService
            service = ServiceObject
            Return service.GetById(CitesApplicationId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal citesApplicationId As Integer) As Entity.CITESApplication
            Dim service As Service.CITESApplicationService
            service = ServiceObject
            Return service.GetById(CitesApplicationId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal citesApplicationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.CITESApplicationService
            service = ServiceObject
            Return service.DeleteById(citesApplicationId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal citesApplicationId As Integer) As Boolean
            Return CITESApplicationBase.DeleteById(citesApplicationId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal citesApplicationId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return CITESApplicationBase.DeleteById(citesApplicationId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForApplication(ByVal applicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim service As Service.CITESApplicationService
            service = ServiceObject
            Return service.GetForApplication(applicationId, tran)
        End Function
        
        Public Overloads Shared Function GetForApplication(ByVal applicationId As Integer) As EntitySet.CITESApplicationSet
            Return CITESApplicationBase.GetForApplication(applicationId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForManagementAuthorityIdPartyLink(ByVal partyLinkId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim service As Service.CITESApplicationService
            service = ServiceObject
            Return service.GetForManagementAuthorityIdPartyLink(partyLinkId, tran)
        End Function
        
        Public Overloads Shared Function GetForManagementAuthorityIdPartyLink(ByVal partyLinkId As Integer) As EntitySet.CITESApplicationSet
            Return CITESApplicationBase.GetForManagementAuthorityIdPartyLink(partyLinkId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForSecondPartyIdPartyLink(ByVal partyLinkId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim service As Service.CITESApplicationService
            service = ServiceObject
            Return service.GetForSecondPartyIdPartyLink(partyLinkId, tran)
        End Function
        
        Public Overloads Shared Function GetForSecondPartyIdPartyLink(ByVal partyLinkId As Integer) As EntitySet.CITESApplicationSet
            Return CITESApplicationBase.GetForSecondPartyIdPartyLink(partyLinkId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForForeignManagementAuthorityIdPartyLink(ByVal partyLinkId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim service As Service.CITESApplicationService
            service = ServiceObject
            Return service.GetForForeignManagementAuthorityIdPartyLink(partyLinkId, tran)
        End Function
        
        Public Overloads Shared Function GetForForeignManagementAuthorityIdPartyLink(ByVal partyLinkId As Integer) As EntitySet.CITESApplicationSet
            Return CITESApplicationBase.GetForForeignManagementAuthorityIdPartyLink(partyLinkId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim service As Service.CITESApplicationService
            service = ServiceObject
            Return service.GetForCountry(countryId, tran)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer) As EntitySet.CITESApplicationSet
            Return CITESApplicationBase.GetForCountry(countryId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForAddress(ByVal addressId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESApplicationSet
            Dim service As Service.CITESApplicationService
            service = ServiceObject
            Return service.GetForAddress(addressId, tran)
        End Function
        
        Public Overloads Shared Function GetForAddress(ByVal addressId As Integer) As EntitySet.CITESApplicationSet
            Return CITESApplicationBase.GetForAddress(addressId, Nothing)
        End Function
        
        Public Overloads Function GetRelatedExportApplication(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ExportApplicationSet
            Return Entity.ExportApplication.GetForCITESApplication(Me.CitesApplicationId, tran)
        End Function
        
        Public Overloads Function GetRelatedExportApplication() As EntitySet.ExportApplicationSet
            Return Me.GetRelatedExportApplication(Nothing)
        End Function
        
        Public Overloads Function GetRelatedImportApplication(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportApplicationSet
            Return Entity.ImportApplication.GetForCITESApplication(Me.CitesApplicationId, tran)
        End Function
        
        Public Overloads Function GetRelatedImportApplication() As EntitySet.ImportApplicationSet
            Return Me.GetRelatedImportApplication(Nothing)
        End Function
        
        Public Overloads Function GetRelatedAdditionalDeclaration(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.AdditionalDeclarationSet
            Return Entity.AdditionalDeclaration.GetForCITESApplication(Me.CitesApplicationId, tran)
        End Function
        
        Public Overloads Function GetRelatedAdditionalDeclaration() As EntitySet.AdditionalDeclarationSet
            Return Me.GetRelatedAdditionalDeclaration(Nothing)
        End Function
        
        Public Overloads Function GetRelatedApplicationSubmission(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ApplicationSubmissionSet
            Return Entity.ApplicationSubmission.GetForCITESApplication(Me.CitesApplicationId, tran)
        End Function
        
        Public Overloads Function GetRelatedApplicationSubmission() As EntitySet.ApplicationSubmissionSet
            Return Me.GetRelatedApplicationSubmission(Nothing)
        End Function
        
        Public Overloads Function GetRelatedArticle10(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.Article10Set
            Return Entity.Article10.GetForCITESApplication(Me.CitesApplicationId, tran)
        End Function
        
        Public Overloads Function GetRelatedArticle10() As EntitySet.Article10Set
            Return Me.GetRelatedArticle10(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal applicationId As Integer, ByVal managementAuthorityId As Object, ByVal secondPartyId As Object, ByVal foreignManagementAuthorityId As Object, ByVal countryOfImportId As Object, ByVal locationAddressId As Object, ByVal consignment As Object, ByVal isComposite As Boolean) As Entity.CITESApplication
            Return Entity.CITESApplication.ServiceObject.Insert(applicationId, managementAuthorityId, secondPartyId, foreignManagementAuthorityId, countryOfImportId, locationAddressId, consignment, isComposite)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim applicationIdParam As Integer = Me.ApplicationId
            Dim managementAuthorityIdParam As Object
            If (Me.IsManagementAuthorityIdNull = false) Then
                managementAuthorityIdParam = Me.ManagementAuthorityId
            Else
                managementAuthorityIdParam = System.DBNull.Value
            End If
            Dim secondPartyIdParam As Object
            If (Me.IsSecondPartyIdNull = false) Then
                secondPartyIdParam = Me.SecondPartyId
            Else
                secondPartyIdParam = System.DBNull.Value
            End If
            Dim foreignManagementAuthorityIdParam As Object
            If (Me.IsForeignManagementAuthorityIdNull = false) Then
                foreignManagementAuthorityIdParam = Me.ForeignManagementAuthorityId
            Else
                foreignManagementAuthorityIdParam = System.DBNull.Value
            End If
            Dim countryOfImportIdParam As Object
            If (Me.IsCountryOfImportIdNull = false) Then
                countryOfImportIdParam = Me.CountryOfImportId
            Else
                countryOfImportIdParam = System.DBNull.Value
            End If
            Dim locationAddressIdParam As Object
            If (Me.IsLocationAddressIdNull = false) Then
                locationAddressIdParam = Me.LocationAddressId
            Else
                locationAddressIdParam = System.DBNull.Value
            End If
            Dim consignmentParam As Object
            If (Me.IsConsignmentNull = false) Then
                consignmentParam = Me.Consignment
            Else
                consignmentParam = System.DBNull.Value
            End If
            Dim isCompositeParam As Boolean = Me.IsComposite
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.CITESApplication.ServiceObject.Update(Me.Id, applicationIdParam, managementAuthorityIdParam, secondPartyIdParam, foreignManagementAuthorityIdParam, countryOfImportIdParam, locationAddressIdParam, consignmentParam, isCompositeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
