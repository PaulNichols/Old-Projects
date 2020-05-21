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


Namespace DataObjects.Entity
    
    'Entity implementation for table 'Address'
    '*DO* add your modifications to this file
    <System.Serializable()>  _
    Public Class Address
        Inherits Base.AddressBase
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal addressId As Integer)
            MyBase.New(addressId)
        End Sub

        Public Sub New(ByVal addressId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New(addressId, tran)
        End Sub

        Public Shadows Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub

        Shared Function GetForParty(ByVal partyId As Int32, ByVal tran As SqlClient.SqlTransaction) As EntitySet.AddressSet
            Dim service As service.AddressService = ServiceObject
            Return service.GetEntitySet("Exec usp_GetPartyAddresses " & partyId, GetType(EntitySet.AddressSet), tran)
        End Function
    End Class
End Namespace