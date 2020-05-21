
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.IDMarkTypeService)), _
     EntityMapping(GetType(DataObjects.Entity.IDMarkType)), _
     CollectionMapping(gettype(DataObjects.Collection.IDMarkTypeBoundCollection)), _
     Serializable()> _
    Public Class BOIDMarkType
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Public Sub New()

        End Sub

        Public Sub New(ByVal idMarkTypeId As Int32)
            MyClass.New(idMarkTypeId, Nothing)
        End Sub

        <DOtoBOMapping("Permanence")> _
        Public Property Permanence() As String
            Get
                If mPermanence Is Nothing Then
                    Return Nothing
                Else
                    Return mPermanence.ToUpper
                End If
            End Get
            Set(ByVal Value As String)
                mPermanence = Value
            End Set
        End Property
        Private mPermanence As String

        Public Sub New(ByVal idMarkTypeId As Int32, ByVal tran As SqlClient.SqlTransaction)
            InitialiseIdMarkType(DataObjects.Entity.IDMarkType.GetById(idMarkTypeId, tran))
        End Sub

        Private Sub InitialiseIdMarkType(ByVal idMarkType As DataObjects.Entity.IDMarkType)
            ConvertDataObjectTOBO(Me, idMarkType)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.IDMarkType.GetAll(includeHyphen, includeInactive, DataObjects.Base.IDMarkTypeServiceBase.OrderBy.IX_IDMarkType)
        End Function
    End Class


End Namespace
