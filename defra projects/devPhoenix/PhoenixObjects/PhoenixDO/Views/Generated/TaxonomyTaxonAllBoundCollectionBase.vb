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
    
    'EntityBoundCollection base implementation for table 'vTaxonomyTaxonAll'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class TaxonomyTaxonAllBoundCollectionBase
        Inherits EnterpriseObjects.EntityBoundCollection
        
        Public Shadows Default ReadOnly Property Item(ByVal index As Integer) As Entity.TaxonomyTaxonAll
            Get
                Return CType(Me.EntitySet.GetEntity(Me.TableIndex, index),Entity.TaxonomyTaxonAll)
            End Get
        End Property
    End Class
End Namespace
