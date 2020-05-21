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
    
    'Service base implementation for table 'PaymentBasketPayment'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class PaymentBasketPaymentServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.PaymentBasketPaymentSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.PaymentBasketPaymentSet
            Return CType(MyBase.GetAll("eosp_SelectPaymentBasketPayment", GetType(EntitySet.PaymentBasketPaymentSet), includeHyphen, includeInactive, orderBy),EntitySet.PaymentBasketPaymentSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.PaymentBasketPaymentSet
            Return Me.GetAll(includeHyphen, includeInactive, PaymentBasketPaymentServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, PaymentBasketPaymentServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.PaymentBasketPaymentSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Function GetById(ByVal idColumns() As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PaymentBasketPayment
            Return CType(MyBase.GetById("eosp_SelectPaymentBasketPayment", New String() {"PaymentId", "PaymentBasketId"}, idColumns, GetType(EntitySet.PaymentBasketPaymentSet), tran),Entity.PaymentBasketPayment)
        End Function
        
        Public Overloads Function GetById(ByVal idColumns() As Integer) As Entity.PaymentBasketPayment
            Return Me.GetById(idColumns, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal idColumns() As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(idColumns, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal idColumns() As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeletePaymentBasketPayment", New String() {"PaymentId", "PaymentBasketId"}, idColumns, checkSum, transaction)
        End Function
        
        'GetForPayment - links to the Payment table...
        Public Overloads Function GetForPayment(ByVal PaymentId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentBasketPaymentSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PaymentBasketPayment where PaymentI"& _ 
"d=" + PaymentId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PaymentBasketPaymentSet), tran),EntitySet.PaymentBasketPaymentSet)
        End Function
        
        'GetForPayment - links to the Payment table...
        Public Overloads Function GetForPayment(ByVal PaymentId As Integer) As EntitySet.PaymentBasketPaymentSet
            Return Me.GetForPayment(PaymentId, Nothing)
        End Function
        
        'GetForPaymentBasket - links to the PaymentBasket table...
        Public Overloads Function GetForPaymentBasket(ByVal PaymentBasketId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentBasketPaymentSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PaymentBasketPayment where PaymentB"& _ 
"asketId=" + PaymentBasketId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PaymentBasketPaymentSet), tran),EntitySet.PaymentBasketPaymentSet)
        End Function
        
        'GetForPaymentBasket - links to the PaymentBasket table...
        Public Overloads Function GetForPaymentBasket(ByVal PaymentBasketId As Integer) As EntitySet.PaymentBasketPaymentSet
            Return Me.GetForPaymentBasket(PaymentBasketId, Nothing)
        End Function
        
        Public Overloads Sub Insert(ByVal paymentId As Integer, ByVal paymentBasketId As Integer, ByVal linkDateTime As Object, ByVal amount As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Sprocs.eosp_CreatePaymentBasketPayment(paymentId, paymentBasketId, linkDateTime, amount, transaction)
        End Sub
        
        Public Overloads Sub Insert(ByVal paymentId As Integer, ByVal paymentBasketId As Integer, ByVal linkDateTime As Object, ByVal amount As Object)
            Me.Insert(paymentId, paymentBasketId, linkDateTime, amount, Nothing)
        End Sub
        
        Public Overloads Sub Insert(ByVal paymentBasketPayment As Entity.PaymentBasketPayment)
            Me.Insert(paymentBasketPayment(0), paymentBasketPayment(1), paymentBasketPayment(2), paymentBasketPayment(3))
        End Sub
        
        Public Overloads Sub Insert(ByVal paymentBasketPayment As Entity.PaymentBasketPayment, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Insert(paymentBasketPayment(0), paymentBasketPayment(1), paymentBasketPayment(2), paymentBasketPayment(3), transaction)
        End Sub
        
        Public Overloads Function Update(ByVal paymentId As Integer, ByVal paymentBasketId As Integer, ByVal linkDateTime As Object, ByVal amount As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PaymentBasketPayment
            Return Sprocs.eosp_UpdatePaymentBasketPayment(paymentId, paymentBasketId, linkDateTime, amount, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal paymentId As Integer, ByVal paymentBasketId As Integer, ByVal linkDateTime As Object, ByVal amount As Object) As Entity.PaymentBasketPayment
            Return Me.Update(paymentId, paymentBasketId, linkDateTime, amount, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal paymentId As Integer, ByVal paymentBasketId As Integer, ByVal linkDateTime As Object, ByVal amount As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PaymentBasketPayment
            Return Me.Update(paymentId, paymentBasketId, linkDateTime, amount, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal paymentId As Integer, ByVal paymentBasketId As Integer, ByVal linkDateTime As Object, ByVal amount As Object, ByVal checkSum As Integer) As Entity.PaymentBasketPayment
            Return Me.Update(paymentId, paymentBasketId, linkDateTime, amount, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal paymentBasketPayment As Entity.PaymentBasketPayment) As Entity.PaymentBasketPayment
            Return Me.Update(paymentBasketPayment(0), paymentBasketPayment(1), paymentBasketPayment(2), paymentBasketPayment(3))
        End Function
        
        Public Overloads Function Update(ByVal paymentBasketPayment As Entity.PaymentBasketPayment, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PaymentBasketPayment
            Return Me.Update(paymentBasketPayment(0), paymentBasketPayment(1), paymentBasketPayment(2), paymentBasketPayment(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal paymentBasketPayment As Entity.PaymentBasketPayment, ByVal checkSum As Integer) As Entity.PaymentBasketPayment
            Return Me.Update(paymentBasketPayment(0), paymentBasketPayment(1), paymentBasketPayment(2), paymentBasketPayment(3), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal paymentBasketPayment As Entity.PaymentBasketPayment, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PaymentBasketPayment
            Return Me.Update(paymentBasketPayment(0), paymentBasketPayment(1), paymentBasketPayment(2), paymentBasketPayment(3), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_PaymentBasketPayment(ByVal paymentId As Integer, ByVal linkDateTime As Date) As EntitySet.PaymentBasketPaymentSet
            Return Sprocs.eosp_SelectPaymentBasketPayment(paymentId:=Nothing, paymentBasketId:=Nothing, Index_PaymentId:=[paymentId], Index_LinkDateTime:=[linkDateTime], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_PaymentBasketPayment(ByVal paymentId As Integer, ByVal linkDateTime As Date, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentBasketPaymentSet
            Return Sprocs.eosp_SelectPaymentBasketPayment(paymentId:=Nothing, paymentBasketId:=Nothing, Index_PaymentId:=[paymentId], Index_LinkDateTime:=[linkDateTime], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_PaymentBasketPayment
            
            
        End Enum
    End Class
End Namespace