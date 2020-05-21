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
    
    'Service base implementation for table 'Payment'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class PaymentServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.PaymentSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.PaymentSet
            Return CType(MyBase.GetAll("eosp_SelectPayment", GetType(EntitySet.PaymentSet), includeHyphen, includeInactive, orderBy),EntitySet.PaymentSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.PaymentSet
            Return Me.GetAll(includeHyphen, includeInactive, PaymentServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, PaymentServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.PaymentSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal paymentId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectPayment", "PaymentId", paymentId, GetType(EntitySet.PaymentSet), tran),Entity.Payment)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal paymentId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(paymentId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal paymentId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Payment
            Return CType(MyBase.GetById("eosp_SelectPayment", "PaymentId", paymentId, GetType(EntitySet.PaymentSet), tran),Entity.Payment)
        End Function
        
        Public Overloads Function GetById(ByVal paymentId As Integer) As Entity.Payment
            Return Me.GetById(paymentId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal paymentId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(paymentId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal paymentId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeletePayment", "PaymentId", paymentId, checkSum, transaction)
        End Function
        
        'GetForParty - links to the Party table...
        Public Overloads Function GetForParty(ByVal PartyId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Payment where PartyId=" + PartyId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PaymentSet), tran),EntitySet.PaymentSet)
        End Function
        
        'GetForParty - links to the Party table...
        Public Overloads Function GetForParty(ByVal PartyId As Integer) As EntitySet.PaymentSet
            Return Me.GetForParty(PartyId, Nothing)
        End Function
        
        'GetForPaymentMethod - links to the PaymentMethod table...
        Public Overloads Function GetForPaymentMethod(ByVal PaymentMethodID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Payment where PaymentMethodId=" + PaymentMethodID.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PaymentSet), tran),EntitySet.PaymentSet)
        End Function
        
        'GetForPaymentMethod - links to the PaymentMethod table...
        Public Overloads Function GetForPaymentMethod(ByVal PaymentMethodID As Integer) As EntitySet.PaymentSet
            Return Me.GetForPaymentMethod(PaymentMethodID, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal partyId As Integer, ByVal paymentReference As Object, ByVal paymentMethodId As Object, ByVal initiatedByCustomer As Object, ByVal paymentDateTime As Object, ByVal paymentAmount As Object, ByVal availableBalance As Object, ByVal gatewayAuthorisationCode As Object, ByVal gatewayBankReference As Object, ByVal enteredApplicationsCovered As Object, ByVal actualApplicationsCovered As Object, ByVal receiptPrinted As Object, ByVal reportDate As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Payment
            Return Me.GetById(Sprocs.eosp_CreatePayment(partyId, paymentReference, paymentMethodId, initiatedByCustomer, paymentDateTime, paymentAmount, availableBalance, gatewayAuthorisationCode, gatewayBankReference, enteredApplicationsCovered, actualApplicationsCovered, receiptPrinted, reportDate, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal partyId As Integer, ByVal paymentReference As Object, ByVal paymentMethodId As Object, ByVal initiatedByCustomer As Object, ByVal paymentDateTime As Object, ByVal paymentAmount As Object, ByVal availableBalance As Object, ByVal gatewayAuthorisationCode As Object, ByVal gatewayBankReference As Object, ByVal enteredApplicationsCovered As Object, ByVal actualApplicationsCovered As Object, ByVal receiptPrinted As Object, ByVal reportDate As Object) As Entity.Payment
            Return Me.Insert(partyId, paymentReference, paymentMethodId, initiatedByCustomer, paymentDateTime, paymentAmount, availableBalance, gatewayAuthorisationCode, gatewayBankReference, enteredApplicationsCovered, actualApplicationsCovered, receiptPrinted, reportDate, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal payment As Entity.Payment) As Entity.Payment
            Return Me.Insert(payment(1), payment(2), payment(3), payment(4), payment(5), payment(6), payment(7), payment(8), payment(9), payment(10), payment(11), payment(12), payment(13))
        End Function
        
        Public Overloads Function Insert(ByVal payment As Entity.Payment, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Payment
            Return Me.Insert(payment(1), payment(2), payment(3), payment(4), payment(5), payment(6), payment(7), payment(8), payment(9), payment(10), payment(11), payment(12), payment(13), transaction)
        End Function
        
        Public Overloads Function Update( _
                    ByVal id As Integer,  _
                    ByVal partyId As Integer,  _
                    ByVal paymentReference As Object,  _
                    ByVal paymentMethodId As Object,  _
                    ByVal initiatedByCustomer As Object,  _
                    ByVal paymentDateTime As Object,  _
                    ByVal paymentAmount As Object,  _
                    ByVal availableBalance As Object,  _
                    ByVal gatewayAuthorisationCode As Object,  _
                    ByVal gatewayBankReference As Object,  _
                    ByVal enteredApplicationsCovered As Object,  _
                    ByVal actualApplicationsCovered As Object,  _
                    ByVal receiptPrinted As Object,  _
                    ByVal reportDate As Object,  _
                    ByVal checkSum As Integer,  _
                    ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Payment
            Return Sprocs.eosp_UpdatePayment(id, partyId, paymentReference, paymentMethodId, initiatedByCustomer, paymentDateTime, paymentAmount, availableBalance, gatewayAuthorisationCode, gatewayBankReference, enteredApplicationsCovered, actualApplicationsCovered, receiptPrinted, reportDate, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal partyId As Integer, ByVal paymentReference As Object, ByVal paymentMethodId As Object, ByVal initiatedByCustomer As Object, ByVal paymentDateTime As Object, ByVal paymentAmount As Object, ByVal availableBalance As Object, ByVal gatewayAuthorisationCode As Object, ByVal gatewayBankReference As Object, ByVal enteredApplicationsCovered As Object, ByVal actualApplicationsCovered As Object, ByVal receiptPrinted As Object, ByVal reportDate As Object) As Entity.Payment
            Return Me.Update(id, partyId, paymentReference, paymentMethodId, initiatedByCustomer, paymentDateTime, paymentAmount, availableBalance, gatewayAuthorisationCode, gatewayBankReference, enteredApplicationsCovered, actualApplicationsCovered, receiptPrinted, reportDate, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal partyId As Integer, ByVal paymentReference As Object, ByVal paymentMethodId As Object, ByVal initiatedByCustomer As Object, ByVal paymentDateTime As Object, ByVal paymentAmount As Object, ByVal availableBalance As Object, ByVal gatewayAuthorisationCode As Object, ByVal gatewayBankReference As Object, ByVal enteredApplicationsCovered As Object, ByVal actualApplicationsCovered As Object, ByVal receiptPrinted As Object, ByVal reportDate As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Payment
            Return Me.Update(id, partyId, paymentReference, paymentMethodId, initiatedByCustomer, paymentDateTime, paymentAmount, availableBalance, gatewayAuthorisationCode, gatewayBankReference, enteredApplicationsCovered, actualApplicationsCovered, receiptPrinted, reportDate, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal partyId As Integer, ByVal paymentReference As Object, ByVal paymentMethodId As Object, ByVal initiatedByCustomer As Object, ByVal paymentDateTime As Object, ByVal paymentAmount As Object, ByVal availableBalance As Object, ByVal gatewayAuthorisationCode As Object, ByVal gatewayBankReference As Object, ByVal enteredApplicationsCovered As Object, ByVal actualApplicationsCovered As Object, ByVal receiptPrinted As Object, ByVal reportDate As Object, ByVal checkSum As Integer) As Entity.Payment
            Return Me.Update(id, partyId, paymentReference, paymentMethodId, initiatedByCustomer, paymentDateTime, paymentAmount, availableBalance, gatewayAuthorisationCode, gatewayBankReference, enteredApplicationsCovered, actualApplicationsCovered, receiptPrinted, reportDate, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal payment As Entity.Payment) As Entity.Payment
            Return Me.Update(payment.id, payment(1), payment(2), payment(3), payment(4), payment(5), payment(6), payment(7), payment(8), payment(9), payment(10), payment(11), payment(12), payment(13))
        End Function
        
        Public Overloads Function Update(ByVal payment As Entity.Payment, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Payment
            Return Me.Update(payment.id, payment(1), payment(2), payment(3), payment(4), payment(5), payment(6), payment(7), payment(8), payment(9), payment(10), payment(11), payment(12), payment(13), transaction)
        End Function
        
        Public Overloads Function Update(ByVal payment As Entity.Payment, ByVal checkSum As Integer) As Entity.Payment
            Return Me.Update(payment.id, payment(1), payment(2), payment(3), payment(4), payment(5), payment(6), payment(7), payment(8), payment(9), payment(10), payment(11), payment(12), payment(13), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal payment As Entity.Payment, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Payment
            Return Me.Update(payment.id, payment(1), payment(2), payment(3), payment(4), payment(5), payment(6), payment(7), payment(8), payment(9), payment(10), payment(11), payment(12), payment(13), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_Payment(ByVal paymentId As Integer) As EntitySet.PaymentSet
            Return Sprocs.eosp_SelectPayment(paymentId:=Nothing, Index_PaymentId:=[paymentId], Index_PaymentReference:=Nothing, Index_PaymentDateTime:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_Payment(ByVal paymentId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentSet
            Return Sprocs.eosp_SelectPayment(paymentId:=Nothing, Index_PaymentId:=[paymentId], Index_PaymentReference:=Nothing, Index_PaymentDateTime:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_Payment_1(ByVal paymentReference As String) As EntitySet.PaymentSet
            Return Sprocs.eosp_SelectPayment(paymentId:=Nothing, Index_PaymentReference:=[paymentReference], Index_PaymentId:=Nothing, Index_PaymentDateTime:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_Payment_1(ByVal paymentReference As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentSet
            Return Sprocs.eosp_SelectPayment(paymentId:=Nothing, Index_PaymentReference:=[paymentReference], Index_PaymentId:=Nothing, Index_PaymentDateTime:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_Payment_2(ByVal paymentDateTime As Date) As EntitySet.PaymentSet
            Return Sprocs.eosp_SelectPayment(paymentId:=Nothing, Index_PaymentDateTime:=[paymentDateTime], Index_PaymentId:=Nothing, Index_PaymentReference:=Nothing, transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_Payment_2(ByVal paymentDateTime As Date, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.PaymentSet
            Return Sprocs.eosp_SelectPayment(paymentId:=Nothing, Index_PaymentDateTime:=[paymentDateTime], Index_PaymentId:=Nothing, Index_PaymentReference:=Nothing, sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_Payment
            
            IX_Payment_1
            
            IX_Payment_2
            
            
        End Enum
    End Class
End Namespace