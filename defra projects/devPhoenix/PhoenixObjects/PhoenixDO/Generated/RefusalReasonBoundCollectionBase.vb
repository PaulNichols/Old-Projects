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
    
    'EntityBoundCollection base implementation for table 'RefusalReason'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class RefusalReasonBoundCollectionBase
        Inherits EnterpriseObjects.EntityBoundCollection
        
        Public Shadows Default ReadOnly Property Item(ByVal index As Integer) As Entity.RefusalReason
            Get
                Return CType(Me.EntitySet.GetEntity(Me.TableIndex, index),Entity.RefusalReason)
            End Get
        End Property
    End Class
End Namespace
