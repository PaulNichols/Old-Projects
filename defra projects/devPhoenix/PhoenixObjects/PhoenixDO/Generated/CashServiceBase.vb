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
    
    'Service base implementation for table 'Cash'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class CashServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.CashSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.CashSet
            Return CType(MyBase.GetAll("eosp_SelectCash", GetType(EntitySet.CashSet), includeHyphen, includeInactive, orderBy),EntitySet.CashSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.CashSet
            Return Me.GetAll(includeHyphen, includeInactive, CashServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, CashServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.CashSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal cashId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectCash", "CashId", cashId, GetType(EntitySet.CashSet), tran),Entity.Cash)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal cashId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(cashId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal cashId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Cash
            Return CType(MyBase.GetById("eosp_SelectCash", "CashId", cashId, GetType(EntitySet.CashSet), tran),Entity.Cash)
        End Function
        
        Public Overloads Function GetById(ByVal cashId As Integer) As Entity.Cash
            Return Me.GetById(cashId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal cashId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(cashId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal cashId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteCash", "CashId", cashId, checkSum, transaction)
        End Function
        
        'GetForPayment - links to the Payment table...
        Public Overloads Function GetForPayment(ByVal PaymentId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CashSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from Cash where PaymentId=" + PaymentId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.CashSet), tran),EntitySet.CashSet)
        End Function
        
        'GetForPayment - links to the Payment table...
        Public Overloads Function GetForPayment(ByVal PaymentId As Integer) As EntitySet.CashSet
            Return Me.GetForPayment(PaymentId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Cash
            Return Me.GetById(Sprocs.eosp_CreateCash(paymentId, amount, serialNumber, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String) As Entity.Cash
            Return Me.Insert(paymentId, amount, serialNumber, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal cash As Entity.Cash) As Entity.Cash
            Return Me.Insert(cash(1), cash(2), cash(3))
        End Function
        
        Public Overloads Function Insert(ByVal cash As Entity.Cash, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Cash
            Return Me.Insert(cash(1), cash(2), cash(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Cash
            Return Sprocs.eosp_UpdateCash(id, paymentId, amount, serialNumber, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String) As Entity.Cash
            Return Me.Update(id, paymentId, amount, serialNumber, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Cash
            Return Me.Update(id, paymentId, amount, serialNumber, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String, ByVal checkSum As Integer) As Entity.Cash
            Return Me.Update(id, paymentId, amount, serialNumber, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal cash As Entity.Cash) As Entity.Cash
            Return Me.Update(cash.id, cash(1), cash(2), cash(3))
        End Function
        
        Public Overloads Function Update(ByVal cash As Entity.Cash, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Cash
            Return Me.Update(cash.id, cash(1), cash(2), cash(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal cash As Entity.Cash, ByVal checkSum As Integer) As Entity.Cash
            Return Me.Update(cash.id, cash(1), cash(2), cash(3), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal cash As Entity.Cash, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.Cash
            Return Me.Update(cash.id, cash(1), cash(2), cash(3), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_Cash(ByVal paymentId As Integer) As EntitySet.CashSet
            Return Sprocs.eosp_SelectCash(cashId:=Nothing, Index_PaymentId:=[paymentId], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_Cash(ByVal paymentId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.CashSet
            Return Sprocs.eosp_SelectCash(cashId:=Nothing, Index_PaymentId:=[paymentId], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_Cash
            
            
        End Enum
    End Class
End Namespace
