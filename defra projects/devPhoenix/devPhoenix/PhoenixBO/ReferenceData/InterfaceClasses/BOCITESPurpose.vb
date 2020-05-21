Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.CITESPurposeService)), _
     EntityMapping(GetType(DataObjects.Entity.CITESPurpose)), _
     CollectionMapping(GetType(DataObjects.Collection.CITESPurposeBoundCollection)), _
     Serializable()> _
    Public Class BOCITESPurpose
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Private _Code As String
        <DOtoBOMapping("Code")> _
        Public Property Code() As String
            Get
                If _Code Is Nothing Then
                    Return Nothing
                Else
                    Return _Code.ToUpper
                End If
            End Get
            Set(ByVal Value As String)
                _Code = Value
            End Set
        End Property

     

        Private _CommercialPurpose As Boolean
        <DOtoBOMapping("CommercialPurpose")> _
        Public Property CommercialPurpose() As Boolean
            Get
                Return _CommercialPurpose
            End Get
            Set(ByVal Value As Boolean)
                _CommercialPurpose = Value
            End Set
        End Property

        Public Property CommercialPurposeText() As String
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(_CommercialPurpose)
            End Get
            Set(ByVal Value As String)
                _CommercialPurpose = Application.Search.ApplicationSearch.ConvertEnglishToBoolean(Value)
            End Set
        End Property

        Public Sub New()

        End Sub

        Public Sub New(ByVal derivativeId As Int32)
            MyClass.New(derivativeId, Nothing)
        End Sub

        Public Sub New(ByVal citesPurposeId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialisePurpose(DataObjects.Entity.CITESPurpose.GetById(citesPurposeId, tran))
        End Sub

        Private Sub InitialisePurpose(ByVal citesPurpose As DataObjects.Entity.CITESPurpose)
            ConvertDataObjectTOBO(Me, citesPurpose)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.CITESPurpose.GetAll(includeHyphen, includeInactive, DataObjects.Base.CITESPurposeServiceBase.OrderBy.IX_CITESPurpose)
        End Function
    End Class

End Namespace

