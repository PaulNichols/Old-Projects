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
    
    'Base entity implementation for table 'Cash'
    '*DO NOT* modify this file.
    'Add new properties and methods to Cash instead.
    Public MustInherit Class CashBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal cashId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(cashId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal cashId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(cashId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property CashId As Integer
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
        
        Public Property PaymentId As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property Amount As Decimal
            Get
                Return CType(Me(2),Decimal)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(10)>  _
        Public Property SerialNumber As String
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
        
        Public Shared ReadOnly Property ServiceObject As Service.CashService
            Get
                Return CType(GetServiceObject(GetType(Service.CashService)),Service.CashService)
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
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(5)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.CashSet
            Return CashBase.GetAll(false, false, CashServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.CashSet
            Return CashBase.GetAll(includeHyphen, false, CashServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As CashServiceBase.OrderBy) As EntitySet.CashSet
            Dim service As Service.CashService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As CashServiceBase.OrderBy) As EntitySet.CashSet
            Return CashBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal cashId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Cash
            Dim service As Service.CashService
            service = ServiceObject
            Return service.GetById(CashId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal cashId As Integer) As Entity.Cash
            Dim service As Service.CashService
            service = ServiceObject
            Return service.GetById(CashId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cashId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.CashService
            service = ServiceObject
            Return service.DeleteById(cashId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cashId As Integer) As Boolean
            Return CashBase.DeleteById(cashId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cashId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return CashBase.DeleteById(cashId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForPayment(ByVal paymentId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CashSet
            Dim service As Service.CashService
            service = ServiceObject
            Return service.GetForPayment(paymentId, tran)
        End Function
        
        Public Overloads Shared Function GetForPayment(ByVal paymentId As Integer) As EntitySet.CashSet
            Return CashBase.GetForPayment(paymentId, Nothing)
        End Function
        
        Public Shared Function Insert(ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String) As Entity.Cash
            Return Entity.Cash.ServiceObject.Insert(paymentId, amount, serialNumber)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim paymentIdParam As Integer = Me.PaymentId
            Dim amountParam As Decimal = Me.Amount
            Dim serialNumberParam As String = Me.SerialNumber
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.Cash.ServiceObject.Update(Me.Id, paymentIdParam, amountParam, serialNumberParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace