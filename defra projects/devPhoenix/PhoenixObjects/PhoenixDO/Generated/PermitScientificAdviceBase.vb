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
    
    'Base entity implementation for table 'PermitScientificAdvice'
    '*DO NOT* modify this file.
    'Add new properties and methods to PermitScientificAdvice instead.
    Public MustInherit Class PermitScientificAdviceBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal permitScientificAdvice As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(permitScientificAdvice, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal permitScientificAdvice As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(permitScientificAdvice).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property PermitScientificAdvice As Integer
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
        
        Public Property DateOfAdvice As Date
            Get
                Return CType(Me(1),Date)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property ScientificAdviceId As Integer
            Get
                Return CType(Me(2),Integer)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property PermitId As Integer
            Get
                Return CType(Me(3),Integer)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property SSOUserId As Decimal
            Get
                Return CType(Me(4),Decimal)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(8000)>  _
        Public Property SpecificAdvice As String
            Get
                If (Me.IsSpecificAdviceNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),String)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property Current As Boolean
            Get
                Return CType(Me(6),Boolean)
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),Integer)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.PermitScientificAdviceService
            Get
                Return CType(GetServiceObject(GetType(Service.PermitScientificAdviceService)),Service.PermitScientificAdviceService)
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
        
        Public Function IsSpecificAdviceNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetSpecificAdviceToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(8)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.PermitScientificAdviceSet
            Return PermitScientificAdviceBase.GetAll(false, false, PermitScientificAdviceServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PermitScientificAdviceSet
            Return PermitScientificAdviceBase.GetAll(includeHyphen, false, PermitScientificAdviceServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PermitScientificAdviceServiceBase.OrderBy) As EntitySet.PermitScientificAdviceSet
            Dim service As Service.PermitScientificAdviceService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PermitScientificAdviceServiceBase.OrderBy) As EntitySet.PermitScientificAdviceSet
            Return PermitScientificAdviceBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permitScientificAdvice As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PermitScientificAdvice
            Dim service As Service.PermitScientificAdviceService
            service = ServiceObject
            Return service.GetById(PermitScientificAdvice, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permitScientificAdvice As Integer) As Entity.PermitScientificAdvice
            Dim service As Service.PermitScientificAdviceService
            service = ServiceObject
            Return service.GetById(PermitScientificAdvice)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitScientificAdvice As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.PermitScientificAdviceService
            service = ServiceObject
            Return service.DeleteById(permitScientificAdvice, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitScientificAdvice As Integer) As Boolean
            Return PermitScientificAdviceBase.DeleteById(permitScientificAdvice, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitScientificAdvice As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return PermitScientificAdviceBase.DeleteById(permitScientificAdvice, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForScientificAdvice(ByVal scientificAdviceId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitScientificAdviceSet
            Dim service As Service.PermitScientificAdviceService
            service = ServiceObject
            Return service.GetForScientificAdvice(scientificAdviceId, tran)
        End Function
        
        Public Overloads Shared Function GetForScientificAdvice(ByVal scientificAdviceId As Integer) As EntitySet.PermitScientificAdviceSet
            Return PermitScientificAdviceBase.GetForScientificAdvice(scientificAdviceId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForPermit(ByVal permitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitScientificAdviceSet
            Dim service As Service.PermitScientificAdviceService
            service = ServiceObject
            Return service.GetForPermit(permitId, tran)
        End Function
        
        Public Overloads Shared Function GetForPermit(ByVal permitId As Integer) As EntitySet.PermitScientificAdviceSet
            Return PermitScientificAdviceBase.GetForPermit(permitId, Nothing)
        End Function
        
        Public Shared Function Insert(ByVal dateOfAdvice As Date, ByVal scientificAdviceId As Integer, ByVal permitId As Integer, ByVal sSOUserId As Decimal, ByVal specificAdvice As Object, ByVal current As Boolean) As Entity.PermitScientificAdvice
            Return Entity.PermitScientificAdvice.ServiceObject.Insert(dateOfAdvice, scientificAdviceId, permitId, sSOUserId, specificAdvice, current)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim dateOfAdviceParam As Date = Me.DateOfAdvice
            Dim scientificAdviceIdParam As Integer = Me.ScientificAdviceId
            Dim permitIdParam As Integer = Me.PermitId
            Dim sSOUserIdParam As Decimal = Me.SSOUserId
            Dim specificAdviceParam As Object
            If (Me.IsSpecificAdviceNull = false) Then
                specificAdviceParam = EnterpriseObjects.Common.ParseSQLText(Me.SpecificAdvice)
            Else
                specificAdviceParam = System.DBNull.Value
            End If
            Dim currentParam As Boolean = Me.Current
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.PermitScientificAdvice.ServiceObject.Update(Me.Id, dateOfAdviceParam, scientificAdviceIdParam, permitIdParam, sSOUserIdParam, specificAdviceParam, currentParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
