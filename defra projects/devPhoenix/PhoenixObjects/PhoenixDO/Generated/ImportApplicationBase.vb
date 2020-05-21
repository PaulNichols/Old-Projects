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
    
    'Base entity implementation for table 'ImportApplication'
    '*DO NOT* modify this file.
    'Add new properties and methods to ImportApplication instead.
    Public MustInherit Class ImportApplicationBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal importApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(importApplicationId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal importApplicationId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(importApplicationId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property ImportApplicationId As Integer
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
        
        Public Property CitesApplicationId As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property CountryOfExportId As Integer
            Get
                If (Me.IsCountryOfExportIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property RegionOfExportId As Integer
            Get
                If (Me.IsRegionOfExportIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Integer)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property RegionOfImportId As Integer
            Get
                If (Me.IsRegionOfImportIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Integer)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.ImportApplicationService
            Get
                Return CType(GetServiceObject(GetType(Service.ImportApplicationService)),Service.ImportApplicationService)
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
        
        Public Function IsCountryOfExportIdNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetCountryOfExportIdToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsRegionOfExportIdNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetRegionOfExportIdToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsRegionOfImportIdNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetRegionOfImportIdToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(6)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ImportApplicationSet
            Return ImportApplicationBase.GetAll(false, false, ImportApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ImportApplicationSet
            Return ImportApplicationBase.GetAll(includeHyphen, false, ImportApplicationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ImportApplicationServiceBase.OrderBy) As EntitySet.ImportApplicationSet
            Dim service As Service.ImportApplicationService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ImportApplicationServiceBase.OrderBy) As EntitySet.ImportApplicationSet
            Return ImportApplicationBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal importApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ImportApplication
            Dim service As Service.ImportApplicationService
            service = ServiceObject
            Return service.GetById(ImportApplicationId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal importApplicationId As Integer) As Entity.ImportApplication
            Dim service As Service.ImportApplicationService
            service = ServiceObject
            Return service.GetById(ImportApplicationId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal importApplicationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ImportApplicationService
            service = ServiceObject
            Return service.DeleteById(importApplicationId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal importApplicationId As Integer) As Boolean
            Return ImportApplicationBase.DeleteById(importApplicationId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal importApplicationId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ImportApplicationBase.DeleteById(importApplicationId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForCITESApplication(ByVal citesApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportApplicationSet
            Dim service As Service.ImportApplicationService
            service = ServiceObject
            Return service.GetForCITESApplication(citesApplicationId, tran)
        End Function
        
        Public Overloads Shared Function GetForCITESApplication(ByVal citesApplicationId As Integer) As EntitySet.ImportApplicationSet
            Return ImportApplicationBase.GetForCITESApplication(citesApplicationId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportApplicationSet
            Dim service As Service.ImportApplicationService
            service = ServiceObject
            Return service.GetForCountry(countryId, tran)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer) As EntitySet.ImportApplicationSet
            Return ImportApplicationBase.GetForCountry(countryId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForRegionOfExportIdUKCountry(ByVal uKCountryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportApplicationSet
            Dim service As Service.ImportApplicationService
            service = ServiceObject
            Return service.GetForRegionOfExportIdUKCountry(uKCountryId, tran)
        End Function
        
        Public Overloads Shared Function GetForRegionOfExportIdUKCountry(ByVal uKCountryId As Integer) As EntitySet.ImportApplicationSet
            Return ImportApplicationBase.GetForRegionOfExportIdUKCountry(uKCountryId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForRegionOfImportIdUKCountry(ByVal uKCountryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportApplicationSet
            Dim service As Service.ImportApplicationService
            service = ServiceObject
            Return service.GetForRegionOfImportIdUKCountry(uKCountryId, tran)
        End Function
        
        Public Overloads Shared Function GetForRegionOfImportIdUKCountry(ByVal uKCountryId As Integer) As EntitySet.ImportApplicationSet
            Return ImportApplicationBase.GetForRegionOfImportIdUKCountry(uKCountryId, Nothing)
        End Function
        
        Public Shared Function Insert(ByVal citesApplicationId As Integer, ByVal countryOfExportId As Object, ByVal regionOfExportId As Object, ByVal regionOfImportId As Object) As Entity.ImportApplication
            Return Entity.ImportApplication.ServiceObject.Insert(citesApplicationId, countryOfExportId, regionOfExportId, regionOfImportId)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim citesApplicationIdParam As Integer = Me.CitesApplicationId
            Dim countryOfExportIdParam As Object
            If (Me.IsCountryOfExportIdNull = false) Then
                countryOfExportIdParam = Me.CountryOfExportId
            Else
                countryOfExportIdParam = System.DBNull.Value
            End If
            Dim regionOfExportIdParam As Object
            If (Me.IsRegionOfExportIdNull = false) Then
                regionOfExportIdParam = Me.RegionOfExportId
            Else
                regionOfExportIdParam = System.DBNull.Value
            End If
            Dim regionOfImportIdParam As Object
            If (Me.IsRegionOfImportIdNull = false) Then
                regionOfImportIdParam = Me.RegionOfImportId
            Else
                regionOfImportIdParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.ImportApplication.ServiceObject.Update(Me.Id, citesApplicationIdParam, countryOfExportIdParam, regionOfExportIdParam, regionOfImportIdParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
