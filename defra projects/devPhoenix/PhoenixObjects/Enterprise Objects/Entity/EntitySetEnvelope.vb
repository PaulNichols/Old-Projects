Imports System.Runtime.Serialization
'========================================================
'EntitySetEnvelope
'--------------------------------------------------------
'Purpose : To provide serialisation to an EntitySet
'
'Author : Steven Sartain (x912595)
'
'Notes : The serialised data persists both the EntitySet
'and the type() of the EntitySet (for recast).
'--------------------------------------------------------
'Revision History
'--------------------------------------------------------
'20 Nov 2003 x912595 : Documented source.
'========================================================
<Serializable()> Public Class EntitySetEnvelope
    Implements ISerializable

    Public EntitySet As EntitySet

    Public Sub New(ByVal entitySet As EntitySet)
        Me.EntitySet = entitySet
    End Sub

    Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
        Dim type As type = CType(info.GetValue("EntitySetType", GetType(type)), type)
        EntitySet = CType(info.GetValue("Data", type), EntitySet)
    End Sub

    Public Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext) Implements System.Runtime.Serialization.ISerializable.GetObjectData
        info.AddValue("EntitySetType", EntitySet.GetType)
        info.AddValue("Data", EntitySet)
    End Sub

End Class
