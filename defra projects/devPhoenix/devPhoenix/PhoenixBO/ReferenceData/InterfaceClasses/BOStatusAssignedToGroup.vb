
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.StatusAssignedToGroupService)), _
     EntityMapping(GetType(DataObjects.Entity.StatusAssignedToGroup)), _
     CollectionMapping(GetType(DataObjects.Collection.StatusAssignedToGroupBoundCollection)), _
     Serializable()> _
    Public Class BOStatusAssignedToGroup
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Sub New()

        End Sub

        Public Sub New(ByVal statusAssignedToGroupId As Int32)
            MyClass.New(statusAssignedToGroupId, Nothing)
        End Sub

        Public Sub New(ByVal statusAssignedToGroupId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialiseStatusAssignedToGroupId(DataObjects.Entity.StatusAssignedToGroup.GetById(statusAssignedToGroupId, tran))
        End Sub

        <DOtoBOMapping("SSOGroupId")> _
        Public Property SSOGroupId() As Int32
            Get
                Return mSSOGroupId
            End Get
            Set(ByVal Value As Int32)
                mSSOGroupId = Value
            End Set
        End Property
        Private mSSOGroupId As Int32

        Private Sub InitialiseStatusAssignedToGroupId(ByVal statusAssignedToGroup As DataObjects.Entity.StatusAssignedToGroup)
            ConvertDataObjectTOBO(Me, statusAssignedToGroup)
        End Sub

        Public Property AssignedTo() As Common.AssignedToList
            Get
                Return CType(Me.ID, Common.AssignedToList)
            End Get
            Set(ByVal Value As Common.AssignedToList)

            End Set
        End Property

        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeinactve As Boolean, ByVal orderBy As DataObjects.Base.StatusAssignedToGroupServiceBase.OrderBy) As [DO].DataObjects.Collection.StatusAssignedToGroupBoundCollection
            Return DataObjects.Entity.StatusAssignedToGroup.GetAll(includeHyphen, includeinactve, orderBy).Entities
        End Function
    End Class

End Namespace
