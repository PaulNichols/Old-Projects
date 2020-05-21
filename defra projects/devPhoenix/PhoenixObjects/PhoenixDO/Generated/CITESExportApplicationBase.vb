'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.573
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Base
    
    'Base entity implementation for table 'CITESExportApplication'
    '*DO NOT* modify this file.
    'Add new properties and methods to CITESExportApplication instead.
    Public MustInherit Class CITESExportApplicationBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal citesExportApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(citesExportApplicationId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal citesExportApplicationId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(citesExportApplicationId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Overrides Property Id() As Integer
            Get
                Return CType(Me(0), Integer)
            End Get
            Set(ByVal Value As Integer)
                Me(0) = Value
            End Set
        End Property

        Public Property CitesApplicationId() As Integer
            Get
                Return CType(Me(1), Integer)
            End Get
            Set(ByVal Value As Integer)
                Me(1) = Value
            End Set
        End Property

        Public Property ExporterId() As Integer
            Get
                If (Me.IsExporterIdNull = True) Then
                    Return Nothing
                Else
                    Return CType(Me(2), Integer)
                End If
            End Get
            Set(ByVal Value As Integer)
                Me(2) = Value
            End Set
        End Property

        Public Property CountryOfExportId() As Integer
            Get
                If (Me.IsCountryOfExportIdNull = True) Then
                    Return Nothing
                Else
                    Return CType(Me(3), Integer)
                End If
            End Get
            Set(ByVal Value As Integer)
                Me(3) = Value
            End Set
        End Property

        Public Property ImporterId() As Integer
            Get
                Return CType(Me(4), Integer)
            End Get
            Set(ByVal Value As Integer)
                Me(4) = Value
            End Set
        End Property

        Public Overrides Property CheckSum() As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = True) Then
                    Return Nothing
                Else
                    Return CType(Me(5), Integer)
                End If
            End Get
            Set(ByVal Value As Integer)
                Me(5) = Value
            End Set
        End Property

        Public Shared ReadOnly Property ServiceObject() As Service.CITESExportApplicationService
            Get
                Return CType(GetServiceObject(GetType(Service.CITESExportApplicationService)), Service.CITESExportApplicationService)
            End Get
        End Property

        Public Overridable Property RawDataset() As System.Data.DataSet Implements EnterpriseObjects.IUpdatable.RawDataset
            Get
                Return mRawDataset
            End Get
            Set(ByVal Value As System.Data.DataSet)
                mRawDataset = Value
            End Set
        End Property

        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub

        Public Function IsExporterIdNull() As Boolean
            Return Me.IsNull(2)
        End Function

        Public Sub SetExporterIdToNull()
            Me(2) = System.DBNull.Value
        End Sub

        Public Function IsCountryOfExportIdNull() As Boolean
            Return Me.IsNull(3)
        End Function

        Public Sub SetCountryOfExportIdToNull()
            Me(3) = System.DBNull.Value
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

        Public Overloads Shared Function GetAll() As EntitySet.CITESExportApplicationSet
            Return CITESExportApplicationBase.GetAll(False, False, CITESExportApplicationServiceBase.OrderBy.DefaultOrder)
        End Function

        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.CITESExportApplicationSet
            Return CITESExportApplicationBase.GetAll(includeHyphen, False, CITESExportApplicationServiceBase.OrderBy.DefaultOrder)
        End Function

        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As CITESExportApplicationServiceBase.OrderBy) As EntitySet.CITESExportApplicationSet
            Dim service As service.CITESExportApplicationService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function

        Public Overloads Shared Function GetAll(ByVal orderBy As CITESExportApplicationServiceBase.OrderBy) As EntitySet.CITESExportApplicationSet
            Return CITESExportApplicationBase.GetAll(False, False, orderBy)
        End Function

        Public Overloads Shared Function GetById(ByVal citesExportApplicationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.CITESExportApplication
            Dim service As service.CITESExportApplicationService
            service = ServiceObject
            Return service.GetById(citesExportApplicationId, tran)
        End Function

        Public Overloads Shared Function GetById(ByVal citesExportApplicationId As Integer) As Entity.CITESExportApplication
            Dim service As service.CITESExportApplicationService
            service = ServiceObject
            Return service.GetById(citesExportApplicationId)
        End Function

        Public Overloads Shared Function DeleteById(ByVal citesExportApplicationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As service.CITESExportApplicationService
            service = ServiceObject
            Return service.DeleteById(citesExportApplicationId, checkSum, transaction)
        End Function

        Public Overloads Shared Function DeleteById(ByVal citesExportApplicationId As Integer) As Boolean
            Return CITESExportApplicationBase.DeleteById(citesExportApplicationId, 0, Nothing)
        End Function

        Public Overloads Shared Function DeleteById(ByVal citesExportApplicationId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return CITESExportApplicationBase.DeleteById(citesExportApplicationId, 0, transaction)
        End Function

        Public Shared Function Insert(ByVal citesApplicationId As Integer, ByVal exporterId As Object, ByVal countryOfExportId As Object, ByVal importerId As Integer) As Entity.CITESExportApplication
            Return Entity.CITESExportApplication.ServiceObject.Insert(citesApplicationId, exporterId, countryOfExportId, importerId)
        End Function

        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim citesApplicationIdParam As Integer = Me.CitesApplicationId
            Dim exporterIdParam As Object
            If (Me.IsExporterIdNull = False) Then
                exporterIdParam = Me.ExporterId
            Else
                exporterIdParam = System.DBNull.Value
            End If
            Dim countryOfExportIdParam As Object
            If (Me.IsCountryOfExportIdNull = False) Then
                countryOfExportIdParam = Me.CountryOfExportId
            Else
                countryOfExportIdParam = System.DBNull.Value
            End If
            Dim importerIdParam As Integer = Me.ImporterId
            Dim checkSum As Integer
            If (Me.UseConcurrency = True) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As Object = Entity.CITESExportApplication.ServiceObject.Update(Me.Id, citesApplicationIdParam, exporterIdParam, countryOfExportIdParam, importerIdParam, checkSum)
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace