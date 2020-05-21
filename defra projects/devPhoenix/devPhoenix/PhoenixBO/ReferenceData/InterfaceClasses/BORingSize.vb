Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.RingSizeService)), _
     EntityMapping(GetType(DataObjects.Entity.RingSize)), _
     CollectionMapping(GetType(DataObjects.Entity.RingSize)), _
     Serializable()> _
    Public Class BORingSize
        Inherits BO.ReferenceData.BOBaseReferenceTable

        <DOtoBOMapping("Code")> _
        Public Property Code() As String
            Get
                If Description Is Nothing Then
                    Return Nothing
                Else
                    Return Description.ToUpper
                End If
            End Get
            Set(ByVal Value As String)
                Description = Value
            End Set
        End Property

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.RingSize.GetAll(includeHyphen, includeInactive, DataObjects.Base.RingSizeServiceBase.OrderBy.IX_RingSize)
        End Function
    End Class

End Namespace




