
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.GWDCountryService)), _
     EntityMapping(GetType(DataObjects.Entity.GWDCountry)), _
     CollectionMapping(GetType(DataObjects.Collection.GWDCountryBoundCollection)), _
     Serializable()> _
      Public Class BOGWDCountry
        Inherits BO.ReferenceData.BOBaseReferenceTable

#Region " DOToBOMapping "

        <DOtoBOMapping("CountryId")> _
        Public Property CountryId() As Int32
            Get
                Return mCountryId
            End Get
            Set(ByVal Value As Int32)
                mCountryId = Value
            End Set
        End Property
        Private mCountryId As Int32

        Private m_CountryName As String = ""
        <DOtoBOMapping("CountryName")> _
        Public Property CountryName() As String
            Get
                Return m_CountryName
            End Get
            Set(ByVal Value As String)
                ' Read Only - Can't be set
                m_CountryName = Value
            End Set
        End Property
        Private m_CountryCodeDescription As String = ""
        <DOtoBOMapping("CodeDescription")> _
        Public Property CodeDescription() As String
            Get
                Return m_CountryCodeDescription
            End Get
            Set(ByVal Value As String)
                ' Read Only - Can't be set
                m_CountryCodeDescription = Value
            End Set
        End Property

        Private Sub SetCountryDetails(ByVal id As Integer)
            Dim myObj As New BOCountry(id)
            m_CountryName = myObj.ShortName
            m_ISOCountryCode = myObj.ISO2CountryCode
            m_CountryCodeDescription = myObj.CodeDescription
            myObj = Nothing

        End Sub
        Private m_ISOCountryCode As String = ""
        <DOtoBOMapping("ISOCountryCode")> _
        Public Property ISOCountryCode() As String
            Get
                Return m_ISOCountryCode
            End Get
            Set(ByVal Value As String)
                ' Read Only - Can't be set
                m_ISOCountryCode = Value
            End Set
        End Property

        <DOtoBOMapping("CITESSignatory")> _
        Public Property CITESSignatory() As Boolean
            Get
                Return mCITESSignatory
            End Get
            Set(ByVal Value As Boolean)
                mCITESSignatory = Value
                SetCountryDetails(mCountryId)
            End Set
        End Property
        Private mCITESSignatory As Boolean

        <DOtoBOMapping("CITESSignatoryAsText")> _
        Public Property CITESSignatoryAsText() As String
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(mCITESSignatory)
            End Get
            Set(ByVal Value As String)
                mCITESSignatory = Application.Search.ApplicationSearch.ConvertEnglishToBoolean(Value)
            End Set
        End Property

        <DOtoBOMapping("ECMemberState")> _
        Public Property ECMemberState() As Boolean
            Get
                Return mECMemberState
            End Get
            Set(ByVal Value As Boolean)
                mECMemberState = Value
            End Set
        End Property
        Private mECMemberState As Boolean

        <DOtoBOMapping("ECMemberStateAsText")> _
        Public Property ECMemberStateAsText() As String
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(mECMemberState)
            End Get
            Set(ByVal Value As String)
                mECMemberState = Application.Search.ApplicationSearch.ConvertEnglishToBoolean(Value)
            End Set
        End Property

        <DOtoBOMapping("PostCodeValidationId")> _
        Public Property PostCodeValidationId() As Object
            Get
                Return mPostCodeValidationId
            End Get
            Set(ByVal Value As Object)
                mPostCodeValidationId = Value
            End Set
        End Property
        Private mPostCodeValidationId As Object

        <DOtoBOMapping("PhoneNumberValidationId")> _
        Public Property PhoneNumberValidationId() As Object
            Get
                Return mPhoneNumberValidationId
            End Get
            Set(ByVal Value As Object)
                mPhoneNumberValidationId = Value
            End Set
        End Property
        Private mPhoneNumberValidationId As Object


#End Region

        Public Sub New()
        End Sub

        Public Sub New(ByVal gwdCountryId As Int32)
            MyClass.New(gwdCountryId, Nothing)
        End Sub

        Public Sub New(ByVal gwdCountryId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialiseGWDCountry(DataObjects.Entity.GWDCountry.GetById(gwdCountryId, tran))
        End Sub

        Private Sub InitialiseGWDCountry(ByVal gwdCountryId As DataObjects.Entity.GWDCountry)
            ConvertDataObjectTOBO(Me, gwdCountryId)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.GWDCountry.GetAll(includeHyphen, includeInactive, DataObjects.Base.GWDCountryServiceBase.OrderBy.IX_GWDCountry)
        End Function

        Protected Overrides Function IsFiltered() As Boolean  'MLD 11/1/5 added
            Return True
        End Function

        Protected Overrides Function PassFilter(ByVal entity As BaseDataBO) As Boolean  'MLD 11/1/5 added
            Dim country As BOGWDCountry = CType(entity, BOGWDCountry)
            Return Not country.ISOCountryCode Is Nothing
        End Function

        Protected Overrides Function GetComparer() As IComparer  'MLD 21/1/5 added
            Return New CountryComparer
        End Function
        
        Private Class CountryComparer
            Implements IComparer

            Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim xcountry As BOGWDCountry = CType(x, BOGWDCountry)
                Dim ycountry As BOGWDCountry = CType(y, BOGWDCountry)
                Return String.Compare(xcountry.ISOCountryCode, ycountry.ISOCountryCode)
            End Function
        End Class
    End Class
End Namespace


