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
    
    'Service base implementation for table 'PostalOrder'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class PostalOrderServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll, EnterpriseObjects.IServiceId
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.PostalOrderSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.PostalOrderSet
            Return CType(MyBase.GetAll("eosp_SelectPostalOrder", GetType(EntitySet.PostalOrderSet), includeHyphen, includeInactive, orderBy),EntitySet.PostalOrderSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.PostalOrderSet
            Return Me.GetAll(includeHyphen, includeInactive, PostalOrderServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, PostalOrderServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.PostalOrderSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal postalOrderId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return CType(MyBase.GetById("eosp_SelectPostalOrder", "PostalOrderId", postalOrderId, GetType(EntitySet.PostalOrderSet), tran),Entity.PostalOrder)
        End Function
        
        Public Overloads Overridable Function GetByIdInternal(ByVal postalOrderId As Integer) As EnterpriseObjects.Entity Implements EnterpriseObjects.IServiceId.GetByIdInternal
            Return Me.GetById(postalOrderId, Nothing)
        End Function
        
        Public Overloads Function GetById(ByVal postalOrderId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PostalOrder
            Return CType(MyBase.GetById("eosp_SelectPostalOrder", "PostalOrderId", postalOrderId, GetType(EntitySet.PostalOrderSet), tran),Entity.PostalOrder)
        End Function
        
        Public Overloads Function GetById(ByVal postalOrderId As Integer) As Entity.PostalOrder
            Return Me.GetById(postalOrderId, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal postalOrderId As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(postalOrderId, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal postalOrderId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeletePostalOrder", "PostalOrderId", postalOrderId, checkSum, transaction)
        End Function
        
        'GetForPayment - links to the Payment table...
        Public Overloads Function GetForPayment(ByVal PaymentId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PostalOrderSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from PostalOrder where PaymentId=" + PaymentId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.PostalOrderSet), tran),EntitySet.PostalOrderSet)
        End Function
        
        'GetForPayment - links to the Payment table...
        Public Overloads Function GetForPayment(ByVal PaymentId As Integer) As EntitySet.PostalOrderSet
            Return Me.GetForPayment(PaymentId, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PostalOrder
            Return Me.GetById(Sprocs.eosp_CreatePostalOrder(paymentId, amount, serialNumber, transaction), transaction)
        End Function
        
        Public Overloads Function Insert(ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String) As Entity.PostalOrder
            Return Me.Insert(paymentId, amount, serialNumber, Nothing)
        End Function
        
        Public Overloads Function Insert(ByVal postalOrder As Entity.PostalOrder) As Entity.PostalOrder
            Return Me.Insert(postalOrder(1), postalOrder(2), postalOrder(3))
        End Function
        
        Public Overloads Function Insert(ByVal postalOrder As Entity.PostalOrder, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PostalOrder
            Return Me.Insert(postalOrder(1), postalOrder(2), postalOrder(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PostalOrder
            Return Sprocs.eosp_UpdatePostalOrder(id, paymentId, amount, serialNumber, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String) As Entity.PostalOrder
            Return Me.Update(id, paymentId, amount, serialNumber, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PostalOrder
            Return Me.Update(id, paymentId, amount, serialNumber, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal id As Integer, ByVal paymentId As Integer, ByVal amount As Decimal, ByVal serialNumber As String, ByVal checkSum As Integer) As Entity.PostalOrder
            Return Me.Update(id, paymentId, amount, serialNumber, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal postalOrder As Entity.PostalOrder) As Entity.PostalOrder
            Return Me.Update(postalOrder.id, postalOrder(1), postalOrder(2), postalOrder(3))
        End Function
        
        Public Overloads Function Update(ByVal postalOrder As Entity.PostalOrder, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PostalOrder
            Return Me.Update(postalOrder.id, postalOrder(1), postalOrder(2), postalOrder(3), transaction)
        End Function
        
        Public Overloads Function Update(ByVal postalOrder As Entity.PostalOrder, ByVal checkSum As Integer) As Entity.PostalOrder
            Return Me.Update(postalOrder.id, postalOrder(1), postalOrder(2), postalOrder(3), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal postalOrder As Entity.PostalOrder, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.PostalOrder
            Return Me.Update(postalOrder.id, postalOrder(1), postalOrder(2), postalOrder(3), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_PostalOrder(ByVal paymentId As Integer) As EntitySet.PostalOrderSet
            Return Sprocs.eosp_SelectPostalOrder(postalOrderId:=Nothing, Index_PaymentId:=[paymentId], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_PostalOrder(ByVal paymentId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.PostalOrderSet
            Return Sprocs.eosp_SelectPostalOrder(postalOrderId:=Nothing, Index_PaymentId:=[paymentId], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_PostalOrder
            
            
        End Enum
    End Class
End Namespace
