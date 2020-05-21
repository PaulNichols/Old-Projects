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


Namespace DataObjects.Views.Base
    
    'Service base implementation for table 'vSearchRefund'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class SearchRefundServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.SearchRefundSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.SearchRefundSet
            Return CType(MyBase.GetAll("eosp_SelectSearchRefund", GetType(EntitySet.SearchRefundSet), includeHyphen, includeInactive, orderBy),EntitySet.SearchRefundSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.SearchRefundSet
            Return Me.GetAll(includeHyphen, includeInactive, SearchRefundServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, SearchRefundServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace