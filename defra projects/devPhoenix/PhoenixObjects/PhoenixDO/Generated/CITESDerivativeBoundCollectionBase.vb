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


Namespace DataObjects.Base
    
    'EntityBoundCollection base implementation for table 'CITESDerivative'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class CITESDerivativeBoundCollectionBase
        Inherits EnterpriseObjects.EntityBoundCollection
        
        Public Shadows Default ReadOnly Property Item(ByVal index As Integer) As Entity.CITESDerivative
            Get
                Return CType(Me.EntitySet.GetEntity(Me.TableIndex, index),Entity.CITESDerivative)
            End Get
        End Property
    End Class
End Namespace
