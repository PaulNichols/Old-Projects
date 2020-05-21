Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.RefusalReasonService)), _
     EntityMapping(GetType(DataObjects.Entity.RefusalReason)), _
     CollectionMapping(GetType(DataObjects.Collection.RefusalReasonBoundCollection)), _
     Serializable()> _
    Public Class BORefusalReason
        Inherits BO.ReferenceData.BOBaseReferenceTable

#Region " DO to BO Mapping "

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

#End Region

        Public Sub New()

        End Sub

        Public Sub New(ByVal refusalReasonId As Int32)
            MyClass.New(refusalReasonId, Nothing)
        End Sub

        Public Sub New(ByVal refusalReasonId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialiseRefusalReason(DataObjects.Entity.RefusalReason.GetById(refusalReasonId, tran))
        End Sub

        Private Sub InitialiseRefusalReason(ByVal refusalReason As DataObjects.Entity.RefusalReason)
            ConvertDataObjectTOBO(Me, refusalReason)
            'With country
            '    Me.mCountryBRU = .CountryBRU
            '    Me.mISO2CountryCode = .ISO2CountryCode
            '    Me.mISO3166 = .ISO3166
            '    Me.mISO3CountryCode = .ISO3CountryCode
            '    Me.mLongName = .LongName
            '    ' Me.mManagementCountryId = .ManagementCountryId
            '    Me.mPhoneNumberValidationExpression = .PhoneNumberValidationExpression
            '    Me.mPostCodeValidationExpression = .PostCodeValidationExpression
            '    Me.mShortName = .ShortName
            '    Me.mCodeDescription = .CodeDescription
            '    ' Me.ID = .Id
            '    CheckSum = .CheckSum
            'End With
            'country = Nothing
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.RefusalReason.GetAll(includeHyphen, includeInactive, DataObjects.Base.RefusalReasonServiceBase.OrderBy.IX_RefusalReason_Subject)
        End Function
    End Class
End Namespace

