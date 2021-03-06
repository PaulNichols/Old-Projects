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
    
    'Base entity implementation for table 'TaxonomyCITESNotification'
    '*DO NOT* modify this file.
    'Add new properties and methods to TaxonomyCITESNotification instead.
    Public MustInherit Class TaxonomyCITESNotificationBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal cITESNotificationID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(cITESNotificationID, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal cITESNotificationID As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(cITESNotificationID).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property CITESNotificationID As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property CITESNotificationName As String
            Get
                If (Me.IsCITESNotificationNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),String)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.TaxonomyCITESNotificationService
            Get
                Return CType(GetServiceObject(GetType(Service.TaxonomyCITESNotificationService)),Service.TaxonomyCITESNotificationService)
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
        
        Public Function IsCITESNotificationNameNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetCITESNotificationNameToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(3)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.TaxonomyCITESNotificationSet
            Return TaxonomyCITESNotificationBase.GetAll(false, false, TaxonomyCITESNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.TaxonomyCITESNotificationSet
            Return TaxonomyCITESNotificationBase.GetAll(includeHyphen, false, TaxonomyCITESNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As TaxonomyCITESNotificationServiceBase.OrderBy) As EntitySet.TaxonomyCITESNotificationSet
            Dim service As Service.TaxonomyCITESNotificationService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As TaxonomyCITESNotificationServiceBase.OrderBy) As EntitySet.TaxonomyCITESNotificationSet
            Return TaxonomyCITESNotificationBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal cITESNotificationID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyCITESNotification
            Dim service As Service.TaxonomyCITESNotificationService
            service = ServiceObject
            Return service.GetById(CITESNotificationID, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal cITESNotificationID As Integer) As Entity.TaxonomyCITESNotification
            Dim service As Service.TaxonomyCITESNotificationService
            service = ServiceObject
            Return service.GetById(CITESNotificationID)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESNotificationID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.TaxonomyCITESNotificationService
            service = ServiceObject
            Return service.DeleteById(cITESNotificationID, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESNotificationID As Integer) As Boolean
            Return TaxonomyCITESNotificationBase.DeleteById(cITESNotificationID, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESNotificationID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return TaxonomyCITESNotificationBase.DeleteById(cITESNotificationID, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyExportQuota(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyExportQuotaSet
            Return Entity.TaxonomyExportQuota.GetForTaxonomyCITESNotification(Me.CITESNotificationID, tran)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyExportQuota() As EntitySet.TaxonomyExportQuotaSet
            Return Me.GetRelatedTaxonomyExportQuota(Nothing)
        End Function
        
        Public Shared Sub Insert(ByVal cITESNotificationID As Integer, ByVal cITESNotificationName As Object)
            Entity.TaxonomyCITESNotification.ServiceObject.Insert(cITESNotificationID, cITESNotificationName)
        End Sub
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim cITESNotificationNameParam As Object
            If (Me.IsCITESNotificationNameNull = false) Then
                cITESNotificationNameParam = EnterpriseObjects.Common.ParseSQLText(Me.CITESNotificationName)
            Else
                cITESNotificationNameParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.TaxonomyCITESNotification.ServiceObject.Update(Me.Id, cITESNotificationNameParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
