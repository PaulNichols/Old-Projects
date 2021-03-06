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
    
    'Base entity implementation for table 'PaymentBasket'
    '*DO NOT* modify this file.
    'Add new properties and methods to PaymentBasket instead.
    Public MustInherit Class PaymentBasketBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal paymentBasketId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(paymentBasketId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal paymentBasketId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(paymentBasketId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property PaymentBasketId As Integer
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
        
        Public Property PartyId As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property TotalFeeCharged As Decimal
            Get
                If (Me.IsTotalFeeChargedNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Decimal)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property BasketClosed As Boolean
            Get
                If (Me.IsBasketClosedNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Boolean)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property RemittanceAdvicePrinted As Boolean
            Get
                If (Me.IsRemittanceAdvicePrintedNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Boolean)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property RemittanceAdviceDateTime As Date
            Get
                If (Me.IsRemittanceAdviceDateTimeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Date)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property RemittanceAdvicePaymentId As Integer
            Get
                If (Me.IsRemittanceAdvicePaymentIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
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
        
        Public Shared ReadOnly Property ServiceObject As Service.PaymentBasketService
            Get
                Return CType(GetServiceObject(GetType(Service.PaymentBasketService)),Service.PaymentBasketService)
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
        
        Public Function IsTotalFeeChargedNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetTotalFeeChargedToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsBasketClosedNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetBasketClosedToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsRemittanceAdvicePrintedNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetRemittanceAdvicePrintedToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsRemittanceAdviceDateTimeNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetRemittanceAdviceDateTimeToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsRemittanceAdvicePaymentIdNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetRemittanceAdvicePaymentIdToNull()
            Me(6) = System.DBNull.Value
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
        
        Public Overloads Shared Function GetAll() As EntitySet.PaymentBasketSet
            Return PaymentBasketBase.GetAll(false, false, PaymentBasketServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PaymentBasketSet
            Return PaymentBasketBase.GetAll(includeHyphen, false, PaymentBasketServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PaymentBasketServiceBase.OrderBy) As EntitySet.PaymentBasketSet
            Dim service As Service.PaymentBasketService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PaymentBasketServiceBase.OrderBy) As EntitySet.PaymentBasketSet
            Return PaymentBasketBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal paymentBasketId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PaymentBasket
            Dim service As Service.PaymentBasketService
            service = ServiceObject
            Return service.GetById(PaymentBasketId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal paymentBasketId As Integer) As Entity.PaymentBasket
            Dim service As Service.PaymentBasketService
            service = ServiceObject
            Return service.GetById(PaymentBasketId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal paymentBasketId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.PaymentBasketService
            service = ServiceObject
            Return service.DeleteById(paymentBasketId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal paymentBasketId As Integer) As Boolean
            Return PaymentBasketBase.DeleteById(paymentBasketId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal paymentBasketId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return PaymentBasketBase.DeleteById(paymentBasketId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForParty(ByVal partyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentBasketSet
            Dim service As Service.PaymentBasketService
            service = ServiceObject
            Return service.GetForParty(partyId, tran)
        End Function
        
        Public Overloads Shared Function GetForParty(ByVal partyId As Integer) As EntitySet.PaymentBasketSet
            Return PaymentBasketBase.GetForParty(partyId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForPayment(ByVal paymentId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentBasketSet
            Dim service As Service.PaymentBasketService
            service = ServiceObject
            Return service.GetForPayment(paymentId, tran)
        End Function
        
        Public Overloads Shared Function GetForPayment(ByVal paymentId As Integer) As EntitySet.PaymentBasketSet
            Return PaymentBasketBase.GetForPayment(paymentId, Nothing)
        End Function
        
        Public Overloads Function GetRelatedPaymentBasketPayment(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentBasketPaymentSet
            Return Entity.PaymentBasketPayment.GetForPaymentBasket(Me.PaymentBasketId, tran)
        End Function
        
        Public Overloads Function GetRelatedPaymentBasketPayment() As EntitySet.PaymentBasketPaymentSet
            Return Me.GetRelatedPaymentBasketPayment(Nothing)
        End Function
        
        Public Overloads Function GetRelatedApplication(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ApplicationSet
            Return Entity.Application.GetForPaymentBasket(Me.PaymentBasketId, tran)
        End Function
        
        Public Overloads Function GetRelatedApplication() As EntitySet.ApplicationSet
            Return Me.GetRelatedApplication(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal partyId As Integer, ByVal totalFeeCharged As Object, ByVal basketClosed As Object, ByVal remittanceAdvicePrinted As Object, ByVal remittanceAdviceDateTime As Object, ByVal remittanceAdvicePaymentId As Object) As Entity.PaymentBasket
            Return Entity.PaymentBasket.ServiceObject.Insert(partyId, totalFeeCharged, basketClosed, remittanceAdvicePrinted, remittanceAdviceDateTime, remittanceAdvicePaymentId)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim partyIdParam As Integer = Me.PartyId
            Dim totalFeeChargedParam As Object
            If (Me.IsTotalFeeChargedNull = false) Then
                totalFeeChargedParam = Me.TotalFeeCharged
            Else
                totalFeeChargedParam = System.DBNull.Value
            End If
            Dim basketClosedParam As Object
            If (Me.IsBasketClosedNull = false) Then
                basketClosedParam = Me.BasketClosed
            Else
                basketClosedParam = System.DBNull.Value
            End If
            Dim remittanceAdvicePrintedParam As Object
            If (Me.IsRemittanceAdvicePrintedNull = false) Then
                remittanceAdvicePrintedParam = Me.RemittanceAdvicePrinted
            Else
                remittanceAdvicePrintedParam = System.DBNull.Value
            End If
            Dim remittanceAdviceDateTimeParam As Object
            If (Me.IsRemittanceAdviceDateTimeNull = false) Then
                remittanceAdviceDateTimeParam = Me.RemittanceAdviceDateTime
            Else
                remittanceAdviceDateTimeParam = System.DBNull.Value
            End If
            Dim remittanceAdvicePaymentIdParam As Object
            If (Me.IsRemittanceAdvicePaymentIdNull = false) Then
                remittanceAdvicePaymentIdParam = Me.RemittanceAdvicePaymentId
            Else
                remittanceAdvicePaymentIdParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.PaymentBasket.ServiceObject.Update(Me.Id, partyIdParam, totalFeeChargedParam, basketClosedParam, remittanceAdvicePrintedParam, remittanceAdviceDateTimeParam, remittanceAdvicePaymentIdParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
