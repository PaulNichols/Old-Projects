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
    
    'EntitySet base implementation for table 'AcquisitionTransferMethod'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class AcquisitionTransferMethodSetBase
        Inherits EnterpriseObjects.EntitySet
        
        'basic constructor...
        Protected Sub New(ByVal state As Object)
            MyBase.New
            Me.Setup
        End Sub
        
        'remoting constructor...
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
            Me.Setup
        End Sub
        
        Public ReadOnly Property Entities As Collection.AcquisitionTransferMethodBoundCollection
            Get
                Return Me.GetBoundCollection
            End Get
        End Property
        
        'shared object initialization...
        Private Sub Setup()
            Me.EntityType = GetType(Entity.AcquisitionTransferMethod)
        End Sub
        
        Private Overloads Function GetBoundCollection() As Collection.AcquisitionTransferMethodBoundCollection
            Return Me.GetBoundCollection(0)
        End Function
        
        Private Overloads Function GetBoundCollection(ByVal tableIndex As Integer) As Collection.AcquisitionTransferMethodBoundCollection
            Return CType(Me.GetBoundCollection(Me, tableIndex, GetType(Collection.AcquisitionTransferMethodBoundCollection)),Collection.AcquisitionTransferMethodBoundCollection)
        End Function
    End Class
End Namespace