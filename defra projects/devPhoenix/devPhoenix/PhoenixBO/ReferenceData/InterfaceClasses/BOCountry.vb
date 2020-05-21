
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.CountryService)), _
     EntityMapping(GetType(DataObjects.Entity.Country)), _
     CollectionMapping(GetType(DataObjects.Collection.CountryBoundCollection)), _
     Serializable()> _
    Public Class BOCountry
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Shared Function GetRegions(ByVal countryid As Int32) As Party.BOUKCountry()
            Dim Regions As DataObjects.EntitySet.UKCountrySet = DataObjects.Entity.Country.GetRelatedRegions(countryid)

            If Not Regions Is Nothing AndAlso _
                Regions.Count > 0 Then
                Dim RegionList(Regions.Count - 1) As Party.BOUKCountry
                Dim Index As Int32 = 0
                For Each Region As DataObjects.Entity.UKCountry In Regions
                    RegionList(Index) = New Party.BOUKCountry(Region)
                    Index += 1
                Next Region
                Return RegionList
            Else
                Return Nothing
            End If
        End Function

        Friend Function GetGWDCountry() As ReferenceData.BOGWDCountry
            Return GetGWDCountry(Nothing)
        End Function

        Friend Function GetGWDCountry(ByVal tran As SqlClient.SqlTransaction) As ReferenceData.BOGWDCountry
            Return GetGWDCountry(Me.ID, tran)
        End Function

        Shared Function GetGWDCountry(ByVal countryId As Int32, ByVal tran As SqlClient.SqlTransaction) As ReferenceData.BOGWDCountry
            Dim GWDCountries As DataObjects.EntitySet.GWDCountrySet = DataObjects.Entity.GWDCountry.GetForCountry(countryId, tran)

            If Not GWDCountries Is Nothing AndAlso _
                GWDCountries.Count = 1 Then
                Return New ReferenceData.BOGWDCountry(CType(GWDCountries.GetEntity(0), DataObjects.Entity.GWDCountry).Id)
            Else
                Return Nothing
            End If
        End Function

        '<DOtoBOMapping("ISOCountryCode")> _
        'Public Property ISOCountryCode() As String
        '    Get
        '        If mISOCountryCode Is Nothing Then
        '            Return Nothing
        '        Else
        '            Return mISOCountryCode.ToUpper
        '        End If
        '    End Get
        '    Set(ByVal Value As String)
        '        mISOCountryCode = Value.ToUpper
        '    End Set
        'End Property
        'Private mISOCountryCode As String

        <DOtoBOMapping("CodeDescription")> _
        Public Property CodeDescription() As String
            Get
                If mCodeDescription Is Nothing Then
                    Return Nothing
                Else
                    Return mCodeDescription
                End If
            End Get
            Set(ByVal Value As String)
                mCodeDescription = Value
            End Set
        End Property
        Private mCodeDescription As String

        <DOtoBOMapping("ISO2CountryCode")> _
                Public Property ISO2CountryCode() As String
            Get
                If mISO2CountryCode Is Nothing Then
                    Return Nothing
                Else
                    Return mISO2CountryCode
                End If
            End Get
            Set(ByVal Value As String)
                mISO2CountryCode = Value
            End Set
        End Property
        Private mISO2CountryCode As String

        <DOtoBOMapping("ISO3CountryCode")> _
        Public Property ISO3CountryCode() As String
            Get
                If mISO3CountryCode Is Nothing Then
                    Return Nothing
                Else
                    Return mISO3CountryCode
                End If
            End Get
            Set(ByVal Value As String)
                mISO3CountryCode = Value
            End Set
        End Property
        Private mISO3CountryCode As String

        <DOtoBOMapping("LongName")> _
               Public Property LongName() As String
            Get
                If mLongName Is Nothing Then
                    Return Nothing
                Else
                    Return mLongName
                End If
            End Get
            Set(ByVal Value As String)
                mLongName = Value
            End Set
        End Property
        Private mLongName As String

        <DOtoBOMapping("ShortName")> _
                     Public Property ShortName() As String
            Get
                If mShortName Is Nothing Then
                    Return Nothing
                Else
                    Return mShortName
                End If
            End Get
            Set(ByVal Value As String)
                mShortName = Value
            End Set
        End Property
        Private mShortName As String

        <DOtoBOMapping("ISO3166")> _
        Public Property ISO3166() As Boolean
            Get
                Return mISO3166
            End Get
            Set(ByVal Value As Boolean)
                mISO3166 = Value
            End Set
        End Property
        Private mISO3166 As Boolean

        <DOtoBOMapping("ManagementCountryId")> _
             Public Property ManagementCountryId() As Int32
            Get
                Return mManagementCountryId
            End Get
            Set(ByVal Value As Int32)
                mManagementCountryId = Value
            End Set
        End Property
        Private mManagementCountryId As Int32

        <DOtoBOMapping("CountryBRU")> _
        Public Property CountryBRU() As Boolean
            Get

                Return mCountryBRU

            End Get
            Set(ByVal Value As Boolean)
                mCountryBRU = Value
            End Set
        End Property
        Private mCountryBRU As Boolean


        Public Sub New()

        End Sub

        Public Sub New(ByVal countryId As Int32)
            MyClass.New(countryId, Nothing)
        End Sub

        Public Sub New(ByVal countryId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialiseCountry(DataObjects.Entity.Country.GetById(countryId, tran))
        End Sub

        Private Sub New(ByVal country As DataObjects.Entity.Country)
            InitialiseCountry(country)
        End Sub

        Private Sub InitialiseCountry(ByVal country As DataObjects.Entity.Country)
            ConvertDataObjectTOBO(Me, country)
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
    End Class
End Namespace


