'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.573
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Views.Base
    
    'Service base implementation for table 'vTaxonomyPermittedListingValuesEU'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class TaxonomyPermittedListingValuesEUServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.TaxonomyPermittedListingValuesEUSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.TaxonomyPermittedListingValuesEUSet
            Return CType(MyBase.GetAll("eosp_SelectTaxonomyPermittedListingValuesEU", GetType(EntitySet.TaxonomyPermittedListingValuesEUSet), includeHyphen, includeInactive, orderBy),EntitySet.TaxonomyPermittedListingValuesEUSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.TaxonomyPermittedListingValuesEUSet
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyPermittedListingValuesEUServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyPermittedListingValuesEUServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
        End Enum
    End Class
End Namespace
