Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.SpecialConditionService)), _
     EntityMapping(GetType(DataObjects.entity.SpecialCondition)), _
     CollectionMapping(GetType(DataObjects.Collection.SpecialConditionBoundCollection)), _
     Serializable()> _
    Public Class BOSpecialCondition
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Private _BFDateRequired As Boolean
        <DOtoBOMapping("BFDateRequired")> _
        Public Property BFDateRequired() As Boolean
            Get
                Return _BFDateRequired
            End Get
            Set(ByVal Value As Boolean)
                _BFDateRequired = Value
            End Set
        End Property

        Public Property BFDateRequiredText() As String
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(_BFDateRequired)
            End Get
            Set(ByVal Value As String)
                _BFDateRequired = Application.Search.ApplicationSearch.ConvertEnglishToBoolean(Value)
            End Set
        End Property

        Private mSubject As String
        <DOtoBOMapping("Subject")> _
        Public Property Subject() As String
            Get
                Return mSubject
            End Get
            Set(ByVal Value As String)
                mSubject = Value
            End Set
        End Property

        Private mCode As String
        <DOtoBOMapping("Code")> _
        Public Property Code() As String
            Get
                Return mCode
            End Get
            Set(ByVal Value As String)
                mCode = Value
            End Set
        End Property

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.SpecialCondition.GetAll(includeHyphen, includeInactive, DataObjects.Base.SpecialConditionServiceBase.OrderBy.IX_SpecialCondition)
        End Function

        Public Sub New()

        End Sub

        Public Sub New(ByVal specialConditionId As Int32)
            MyClass.New(specialConditionId, Nothing)
        End Sub

        Public Sub New(ByVal specialConditionId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialiseSpecialCondition(DataObjects.Entity.SpecialCondition.GetById(specialConditionId, tran))
        End Sub

        Private Sub InitialiseSpecialCondition(ByVal specialCondition As DataObjects.Entity.SpecialCondition)
            ConvertDataObjectTOBO(Me, specialCondition)
        End Sub

    End Class
End Namespace

