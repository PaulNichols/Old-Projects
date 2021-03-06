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
    
    'Service base implementation for table 'Refund'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class RefundServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.RefundSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.RefundSet
            Return CType(MyBase.GetAll("eosp_SelectRefund", GetType(EntitySet.RefundSet), includeHyphen, includeInactive, orderBy),EntitySet.RefundSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.RefundSet
            Return Me.GetAll(includeHyphen, includeInactive, RefundServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, RefundServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal refundId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectRefund", "RefundId", refundId, GetType(EntitySet.RefundSet), tran),Entity.Refund)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal refundId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(refundId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal refundId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Refund
            Return CType(MyBase.GetById("eosp_SelectRefund", "RefundId", refundId, GetType(EntitySet.RefundSet), tran),Entity.Refund)
        End Function
        
        Public Overloads Function GetById(ByVal refundId As Integer) As Entity.Refund
            Return Me.GetById(refundId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal refundId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(refundId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal refundId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteRefund", "RefundId", refundId, checkSum, transaction)
        End Function
        
        'GetForPayment - links to the Payment table...
        Public Overloads Function GetForPayment(ByVal PaymentId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.RefundSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Refund where PaymentId=" + PaymentId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.RefundSet), tran),EntitySet.RefundSet)
        End Function
        
        'GetForPayment - links to the Payment table...
        Public Overloads Function GetForPayment(ByVal PaymentId As Integer) As EntitySet.RefundSet
            Return Me.GetForPayment(PaymentId, Nothing)
        End Function
        
        'GetForParty - links to the Party table...
        Public Overloads Function GetForParty(ByVal PartyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.RefundSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Refund where OriginalPartyId=" + PartyId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.RefundSet), tran),EntitySet.RefundSet)
        End Function
        
        'GetForParty - links to the Party table...
        Public Overloads Function GetForParty(ByVal PartyId As Integer) As EntitySet.RefundSet
            Return Me.GetForParty(PartyId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal paymentId As Integer, ByVal sSOUserId As Object, ByVal refundType As Object, ByVal dateTime As Object, ByVal amount As Object, ByVal originalPaymentAmount As Object, ByVal originalPartyId As Object, ByVal notesComments As Object, ByVal reportDate As Object, ByVal gatewayAuthorisation As Object, ByVal gatewayReference As Object, ByVal bankReference As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Refund
            Return Me.GetById(Sprocs.eosp_CreateRefund(paymentId, sSOUserId, refundType, dateTime, amount, originalPaymentAmount, originalPartyId, notesComments, reportDate, gatewayAuthorisation, gatewayReference, bankReference, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal paymentId As Integer, ByVal sSOUserId As Object, ByVal refundType As Object, ByVal dateTime As Object, ByVal amount As Object, ByVal originalPaymentAmount As Object, ByVal originalPartyId As Object, ByVal notesComments As Object, ByVal reportDate As Object, ByVal gatewayAuthorisation As Object, ByVal gatewayReference As Object, ByVal bankReference As Object) As Entity.Refund
            Return Me.Insert(paymentId, sSOUserId, refundType, dateTime, amount, originalPaymentAmount, originalPartyId, notesComments, reportDate, gatewayAuthorisation, gatewayReference, bankReference, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal refund As Entity.Refund) As Entity.Refund
            Return Me.Insert(refund(1), refund(2), refund(3), refund(4), refund(5), refund(6), refund(7), refund(8), refund(9), refund(10), refund(11), refund(12))
        End Function
        
        Public Overloads Function Insert(ByVal refund As Entity.Refund, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Refund
            Return Me.Insert(refund(1), refund(2), refund(3), refund(4), refund(5), refund(6), refund(7), refund(8), refund(9), refund(10), refund(11), refund(12), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal sSOUserId As Object, ByVal refundType As Object, ByVal dateTime As Object, ByVal amount As Object, ByVal originalPaymentAmount As Object, ByVal originalPartyId As Object, ByVal notesComments As Object, ByVal reportDate As Object, ByVal gatewayAuthorisation As Object, ByVal gatewayReference As Object, ByVal bankReference As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Refund
            Return Sprocs.eosp_UpdateRefund(id, paymentId, sSOUserId, refundType, dateTime, amount, originalPaymentAmount, originalPartyId, notesComments, reportDate, gatewayAuthorisation, gatewayReference, bankReference, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal sSOUserId As Object, ByVal refundType As Object, ByVal dateTime As Object, ByVal amount As Object, ByVal originalPaymentAmount As Object, ByVal originalPartyId As Object, ByVal notesComments As Object, ByVal reportDate As Object, ByVal gatewayAuthorisation As Object, ByVal gatewayReference As Object, ByVal bankReference As Object) As Entity.Refund
            Return Me.Update(id, paymentId, sSOUserId, refundType, dateTime, amount, originalPaymentAmount, originalPartyId, notesComments, reportDate, gatewayAuthorisation, gatewayReference, bankReference, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal sSOUserId As Object, ByVal refundType As Object, ByVal dateTime As Object, ByVal amount As Object, ByVal originalPaymentAmount As Object, ByVal originalPartyId As Object, ByVal notesComments As Object, ByVal reportDate As Object, ByVal gatewayAuthorisation As Object, ByVal gatewayReference As Object, ByVal bankReference As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Refund
            Return Me.Update(id, paymentId, sSOUserId, refundType, dateTime, amount, originalPaymentAmount, originalPartyId, notesComments, reportDate, gatewayAuthorisation, gatewayReference, bankReference, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal sSOUserId As Object, ByVal refundType As Object, ByVal dateTime As Object, ByVal amount As Object, ByVal originalPaymentAmount As Object, ByVal originalPartyId As Object, ByVal notesComments As Object, ByVal reportDate As Object, ByVal gatewayAuthorisation As Object, ByVal gatewayReference As Object, ByVal bankReference As Object, ByVal checkSum As Integer) As Entity.Refund
            Return Me.Update(id, paymentId, sSOUserId, refundType, dateTime, amount, originalPaymentAmount, originalPartyId, notesComments, reportDate, gatewayAuthorisation, gatewayReference, bankReference, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal refund As Entity.Refund) As Entity.Refund
            Return Me.Update(refund.id, refund(1), refund(2), refund(3), refund(4), refund(5), refund(6), refund(7), refund(8), refund(9), refund(10), refund(11), refund(12))
        End Function
        
        Public Overloads Function Update(ByVal refund As Entity.Refund, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Refund
            Return Me.Update(refund.id, refund(1), refund(2), refund(3), refund(4), refund(5), refund(6), refund(7), refund(8), refund(9), refund(10), refund(11), refund(12), transaction)
        End Function
        
        Public Overloads Function Update(ByVal refund As Entity.Refund, ByVal checkSum As Integer) As Entity.Refund
            Return Me.Update(refund.id, refund(1), refund(2), refund(3), refund(4), refund(5), refund(6), refund(7), refund(8), refund(9), refund(10), refund(11), refund(12), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal refund As Entity.Refund, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Refund
            Return Me.Update(refund.id, refund(1), refund(2), refund(3), refund(4), refund(5), refund(6), refund(7), refund(8), refund(9), refund(10), refund(11), refund(12), checkSum, transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace
