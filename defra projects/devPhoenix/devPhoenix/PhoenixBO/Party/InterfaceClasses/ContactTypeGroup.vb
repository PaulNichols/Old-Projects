
Namespace Party
    <Serializable()> _
    Public Class BOContactTypeGroup

        Public Shared Function GetAll() As [DO].DataObjects.Collection.ContactTypeGroupBoundCollection
            Return DataObjects.Entity.ContactTypeGroup.GetAll().Entities
        End Function
    End Class
End Namespace