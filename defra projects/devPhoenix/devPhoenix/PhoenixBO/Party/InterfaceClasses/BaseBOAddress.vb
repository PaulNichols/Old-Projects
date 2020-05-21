Namespace Party
    Public Class BaseBOAddress
        Inherits BaseBO
        Implements IAddress


        Protected mMailingAddressId As Object

        Public Sub New()
            MyBase.New()
        End Sub

        Public Property ActiveString() As String Implements IAddress.ActiveString
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(Active)
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property IsMailingString() As String Implements IAddress.IsMailingString
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(IsMailing)
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property Active() As Boolean Implements IAddress.Active
            Get
                Return mActive
            End Get
            Set(ByVal Value As Boolean)
                mActive = Value
            End Set
        End Property
        Private mActive As Boolean

        Public Property Address1() As String Implements IAddress.Address1
            Get
                Return mAddress1
            End Get
            Set(ByVal Value As String)
                mAddress1 = Value
            End Set
        End Property
        Private mAddress1 As String

        Public Property Address2() As String Implements IAddress.Address2
            Get
                Return mAddress2
            End Get
            Set(ByVal Value As String)
                mAddress2 = Value
            End Set
        End Property
        Private mAddress2 As String

        Public Property Address3() As String Implements IAddress.Address3
            Get
                Return mAddress3
            End Get
            Set(ByVal Value As String)
                mAddress3 = Value
            End Set
        End Property
        Private mAddress3 As String

        Public Property Address4() As String Implements IAddress.Address4
            Get
                Return mAddress4
            End Get
            Set(ByVal Value As String)
                mAddress4 = Value
            End Set
        End Property
        Private mAddress4 As String

        Public Property AddressId() As Integer Implements IAddress.AddressId
            Get
                Return mAddressId
            End Get
            Set(ByVal Value As Integer)
                mAddressId = Value
            End Set
        End Property
        Private mAddressId As Int32

        Public Property Country() As String Implements IAddress.Country
            Get
                If mCountry Is Nothing Then
                    Dim CountryData As DataObjects.Entity.Country = DataObjects.Entity.Country.GetById(mCountryId)
                    If Not CountryData Is Nothing Then
                        mCountry = CountryData.ShortName
                        CountryData = Nothing
                    End If
                End If
                Return mCountry
            End Get
            Set(ByVal Value As String)
                mCountry = Value
            End Set
        End Property
        Private mCountry As String

        Friend ReadOnly Property ISO2CountryCode() As String
            Get
                Dim ISO2 As String = String.Empty
                If mCountryId > 0 Then
                    Dim CountryData As DataObjects.Entity.Country = DataObjects.Entity.Country.GetById(mCountryId)
                    If Not CountryData Is Nothing AndAlso Not CountryData.IsISO2CountryCodeNull Then
                        ISO2 = CountryData.ISO2CountryCode
                        CountryData = Nothing
                    End If
                End If
                Return ISO2
            End Get
        End Property

        Public Property CountryId() As Integer Implements IAddress.CountryId
            Get
                Return mCountryId
            End Get
            Set(ByVal Value As Integer)
                mCountryId = Value
            End Set
        End Property
        Private mCountryId As Int32

        Public Property Region() As String Implements IAddress.Region
            Get
                'todo
                'If mRegion Is Nothing Then
                '    Dim megionData As DataObjects.Entity.re = DataObjects.Entity.Country.GetById(mCountryId)
                '    If Not CountryData Is Nothing Then
                '        mRegion = CountryData.Description
                '        CountryData = Nothing
                '    End If
                'End If
                'Return mCountry
            End Get
            Set(ByVal Value As String)
                mRegion = Value
            End Set
        End Property
        Private mRegion As String

        Public Property RegionId() As Object Implements IAddress.RegionId
            Get
                Return mRegionId
            End Get
            Set(ByVal Value As Object)
                mRegionId = Value
            End Set
        End Property
        Private mRegionId As Object

        Public Property County() As String Implements IAddress.County
            Get
                Return mCounty
            End Get
            Set(ByVal Value As String)
                mCounty = Value
            End Set
        End Property
        Private mCounty As String

        Public Property HouseNumber() As String Implements IAddress.HouseNumber
            Get
                Return mHouseNumber
            End Get
            Set(ByVal Value As String)
                mHouseNumber = Value
            End Set
        End Property
        Private mHouseNumber As String

        Public Property BuildingName() As String Implements IAddress.BuildingName
            Get
                Return mBuildingName
            End Get
            Set(ByVal Value As String)
                mBuildingName = Value
            End Set
        End Property
        Private mBuildingName As String

        Public Property OrganisationName() As String Implements IAddress.OrganisationName
            Get
                Return mOrganisationName
            End Get
            Set(ByVal Value As String)
                mOrganisationName = Value
            End Set
        End Property
        Private mOrganisationName As String

        Public Property IsTemporary() As Boolean Implements IAddress.IsTemporary
            Get
                Return mIsTemporary
            End Get
            Set(ByVal Value As Boolean)
                mIsTemporary = Value
            End Set
        End Property
        Private mIsTemporary As Boolean

        Public Property Postcode() As String Implements IAddress.Postcode
            Get
                Return mPostcode
            End Get
            Set(ByVal Value As String)
                mPostcode = Value
            End Set
        End Property
        Private mPostcode As String

        Public Property ContactName() As Object Implements IAddress.ContactName
            Get
                Return mContactName
            End Get
            Set(ByVal Value As Object)
                mContactName = Value
            End Set
        End Property
        Private mContactName As Object

        Public Property Town() As String Implements IAddress.Town
            Get
                Return mTown
            End Get
            Set(ByVal Value As String)
                mTown = Value
            End Set
        End Property
        Private mTown As String

        Public Property IsMailing() As Boolean Implements IAddress.IsMailing
            Get
                Return mIsMailing
            End Get
            Set(ByVal Value As Boolean)
                mIsMailing = Value
            End Set
        End Property
        Private mIsMailing As Boolean

        Public Property PartyId() As Integer Implements IAddress.PartyId
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As Integer)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As Int32

        Public Property DisplayAddress() As String Implements IAddress.DisplayAddress
            Get
                If mDisplayAddress Is Nothing Then
                    mDisplayAddress = String.Concat(mAddress1, ", ", mPostcode)
                End If
                Return mDisplayAddress
            End Get
            Set(ByVal Value As String)
                mDisplayAddress = Value
            End Set
        End Property
        Private mDisplayAddress As String

        Public Overridable Property LongGridAddress() As String
            Get
                If Not moriginalAddress Is Nothing Then
                    Return Me.moriginalAddress.Replace(CType(Microsoft.VisualBasic.Constants.vbTab, Char), "")
                End If
            End Get
            Set(ByVal Value As String)
                '  mReportAddress = Value
            End Set
        End Property

        Public Overridable Property ReportAddress() As String Implements IAddress.ReportAddress
            Get
                If Not moriginalAddress Is Nothing Then
                    Return Me.moriginalAddress.Replace(CType(Microsoft.VisualBasic.Constants.vbTab, Char), Environment.NewLine)
                End If
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
        Public moriginalAddress As String

        Public Function Validate() As ValidationManager Implements IValidate.Validate
            Return Nothing
        End Function


    End Class
End Namespace