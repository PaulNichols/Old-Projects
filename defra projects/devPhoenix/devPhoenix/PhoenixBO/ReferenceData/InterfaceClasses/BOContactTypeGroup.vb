Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.ContactTypeGroupService)), _
     EntityMapping(GetType(DataObjects.Entity.ContactTypeGroup)), _
     CollectionMapping(GetType(DataObjects.Collection.ContactTypeGroupBoundCollection)), _
     Serializable()> _
    Public Class BOContactTypeGroup
        Inherits BO.ReferenceData.BOBaseReferenceTable

#Region " DoToBOMapping "
        Private mExpression As String
        <DOtoBOMapping("Expression")> _
        Public Property Expression() As String
            Get
                Return mExpression
            End Get
            Set(ByVal Value As String)
                mExpression = Value
            End Set
        End Property

        Private mGroupName As String

        <DOtoBOMapping("GroupName")> _
        Public Property GroupName() As String
            Get
                Return mGroupName
            End Get
            Set(ByVal Value As String)
                mGroupName = Value
            End Set
        End Property
#End Region

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.ContactTypeGroup.GetAll(includeHyphen, includeInactive, DataObjects.Base.ContactTypeGroupServiceBase.OrderBy.IX_ContactTypeGroup_GroupName)
        End Function
    End Class

End Namespace