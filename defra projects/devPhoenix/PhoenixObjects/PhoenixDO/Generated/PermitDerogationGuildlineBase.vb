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
    
    'Base entity implementation for table 'PermitDerogationGuildline'
    '*DO NOT* modify this file.
    'Add new properties and methods to PermitDerogationGuildline instead.
    Public MustInherit Class PermitDerogationGuildlineBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal permitDerogationGuildlineId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(permitDerogationGuildlineId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal permitDerogationGuildlineId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(permitDerogationGuildlineId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property PermitDerogationGuildlineId As Integer
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
        
        Public Property PermitId As Integer
            Get
                If (Me.IsPermitIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),Integer)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property DerogationGuideLineId As Integer
            Get
                If (Me.IsDerogationGuideLineIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(1)>  _
        Public Property Description As String
            Get
                Return CType(Me(3),String)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.PermitDerogationGuildlineService
            Get
                Return CType(GetServiceObject(GetType(Service.PermitDerogationGuildlineService)),Service.PermitDerogationGuildlineService)
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
        
        Public Function IsPermitIdNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetPermitIdToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsDerogationGuideLineIdNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetDerogationGuideLineIdToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(5)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.PermitDerogationGuildlineSet
            Return PermitDerogationGuildlineBase.GetAll(false, false, PermitDerogationGuildlineServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PermitDerogationGuildlineSet
            Return PermitDerogationGuildlineBase.GetAll(includeHyphen, false, PermitDerogationGuildlineServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PermitDerogationGuildlineServiceBase.OrderBy) As EntitySet.PermitDerogationGuildlineSet
            Dim service As Service.PermitDerogationGuildlineService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PermitDerogationGuildlineServiceBase.OrderBy) As EntitySet.PermitDerogationGuildlineSet
            Return PermitDerogationGuildlineBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permitDerogationGuildlineId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PermitDerogationGuildline
            Dim service As Service.PermitDerogationGuildlineService
            service = ServiceObject
            Return service.GetById(PermitDerogationGuildlineId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permitDerogationGuildlineId As Integer) As Entity.PermitDerogationGuildline
            Dim service As Service.PermitDerogationGuildlineService
            service = ServiceObject
            Return service.GetById(PermitDerogationGuildlineId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitDerogationGuildlineId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.PermitDerogationGuildlineService
            service = ServiceObject
            Return service.DeleteById(permitDerogationGuildlineId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitDerogationGuildlineId As Integer) As Boolean
            Return PermitDerogationGuildlineBase.DeleteById(permitDerogationGuildlineId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitDerogationGuildlineId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return PermitDerogationGuildlineBase.DeleteById(permitDerogationGuildlineId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForPermit(ByVal permitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitDerogationGuildlineSet
            Dim service As Service.PermitDerogationGuildlineService
            service = ServiceObject
            Return service.GetForPermit(permitId, tran)
        End Function
        
        Public Overloads Shared Function GetForPermit(ByVal permitId As Integer) As EntitySet.PermitDerogationGuildlineSet
            Return PermitDerogationGuildlineBase.GetForPermit(permitId, Nothing)
        End Function
        
        Public Shared Function Insert(ByVal permitId As Object, ByVal derogationGuideLineId As Object, ByVal description As String) As Entity.PermitDerogationGuildline
            Return Entity.PermitDerogationGuildline.ServiceObject.Insert(permitId, derogationGuideLineId, description)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim permitIdParam As Object
            If (Me.IsPermitIdNull = false) Then
                permitIdParam = Me.PermitId
            Else
                permitIdParam = System.DBNull.Value
            End If
            Dim derogationGuideLineIdParam As Object
            If (Me.IsDerogationGuideLineIdNull = false) Then
                derogationGuideLineIdParam = Me.DerogationGuideLineId
            Else
                derogationGuideLineIdParam = System.DBNull.Value
            End If
            Dim descriptionParam As String = Me.Description
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.PermitDerogationGuildline.ServiceObject.Update(Me.Id, permitIdParam, derogationGuideLineIdParam, descriptionParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace