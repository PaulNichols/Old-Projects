Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.UKCountryService)), _
     EntityMapping(GetType(DataObjects.Entity.UKCountry)), _
     CollectionMapping(GetType(DataObjects.Collection.UKCountryBoundCollection)), _
     Serializable()> _
    Public Class BOUKCountry
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Private _ISOCountryCode As String

        <DOtoBOMapping("ISOCountryCode")> _
       Public Property ISOCountryCode() As String
            Get
                Return _ISOCountryCode
            End Get
            Set(ByVal Value As String)
                _ISOCountryCode = Value
            End Set
        End Property

        Private _CountryId As Integer
        <DOtoBOMapping("CountryId")> _
        Public Property CountryId() As Integer
            Get
                Return _CountryId
            End Get
            Set(ByVal Value As Integer)
                _CountryId = Value
            End Set
        End Property

        Private _UKCountryCode As String
        <DOtoBOMapping("UKCountryCode")> _
        Public Property UKCountryCode() As String
            Get
                Return _UKCountryCode
            End Get
            Set(ByVal Value As String)
                _UKCountryCode = Value
            End Set
        End Property
        Private _UKCountryName As String
        <DOtoBOMapping("UKCountryName")> _
        Public Property UKCountryName() As String
            Get
                Return _UKCountryName
            End Get
            Set(ByVal Value As String)
                _UKCountryName = Value
            End Set
        End Property

        Public Sub New()
            _CountryId = Integer.Parse(System.Configuration.ConfigurationSettings.AppSettings("UKCountryIdFromDatabase"))
        End Sub

        Public Sub New(ByVal ukCountryId As Int32)
            MyClass.New(ukCountryId, Nothing)
        End Sub

        Public Sub New(ByVal ukCountryId As Int32, ByVal tran As SqlClient.SqlTransaction)
            InitialiseUKCountry(DataObjects.Entity.UKCountry.GetById(ukCountryId, tran))
        End Sub

        Private Sub InitialiseUKCountry(ByVal ukCountry As DataObjects.Entity.UKCountry)
            ConvertDataObjectTOBO(Me, ukCountry)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.UKCountry.GetAll(includeHyphen, includeInactive, DataObjects.Base.UKCountryServiceBase.OrderBy.IX_UKCountry)
        End Function
    End Class
End Namespace


