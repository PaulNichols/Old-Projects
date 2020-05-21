Namespace Party
    <Serializable()> _
   Public Class BOReadOnlyAddress
        Inherits BaseBOAddress

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal addressId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New(New BOAddress(addressId, tran))
        End Sub

        Public Sub New(ByVal addressId As Int32)
            MyClass.New(addressId, Nothing)
        End Sub

        Public Sub New(ByVal address As Party.BOAddress)
            InitialiseReadOnlyAddress(address, Nothing)
        End Sub

        Public Sub New(ByVal address As Party.BOAddress, ByVal tran As SqlClient.SqlTransaction)
            InitialiseReadOnlyAddress(address, tran)
        End Sub

        Protected Overridable Sub InitialiseReadOnlyAddress(ByVal address As Party.BOAddress, ByVal tran As SqlClient.SqlTransaction)
            If Not address Is Nothing Then
                With address
                    Active = .Active
                    Address1 = .Address1
                    Address2 = .Address2
                    Address3 = .Address3
                    Address4 = .Address4
                    AddressId = .AddressId
                    ContactName = .ContactName
                    Country = .Country
                    CountryId = .CountryId
                    County = .County
                    DisplayAddress = .DisplayAddress
                    IsMailing = .IsMailing
                    IsTemporary = .IsTemporary
                    PartyId = .PartyId
                    Postcode = .Postcode
                    Region = .Region
                    RegionId = .RegionId
                    HouseNumber = .HouseNumber
                    Town = .Town
                    AddressAndHouseNumber = String.Concat(.HouseNumber, ", ", .Address1)
                    moriginaladdress = .moriginaladdress
                End With
            Else
            End If
        End Sub

     

        Public Property AddressAndHouseNumber() As String
            Get
                Return mAddressAndHouseNumber
            End Get
            Set(ByVal Value As String)
                mAddressAndHouseNumber = Value
            End Set
        End Property
        Private mAddressAndHouseNumber As String

        Public Property HTMLAddress() As String
            Get
                If Not moriginalAddress Is Nothing Then
                    Return Me.moriginalAddress.Replace(CType(Microsoft.VisualBasic.Constants.vbTab, Char), "<br>")
                End If
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Friend ReadOnly Property SingleLineAddress() As String
            Get
                If Not moriginalAddress Is Nothing Then
                    Return Me.moriginalAddress.Replace(CType(Microsoft.VisualBasic.Constants.vbTab, Char), ", ")
                End If
            End Get
        End Property

        Public Property OrdinaryAddress() As String
            Get
                If Not moriginalAddress Is Nothing Then
                    Return Me.moriginalAddress '.Replace(CType(Microsoft.VisualBasic.Constants.vbTab, Char), "<br>")
                End If
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        'Public Overrides Property ReportAddress() As String
        '    Get
        '        String.Concat(Address1, Environment.NewLine)

        '        Const sTab As Char = CType(Microsoft.VisualBasic.Constants.vbTab, Char)
        '        Dim sAddrLines() As String = String.Concat(Address1, sTab, Address2, sTab, Address3, _
        '            sTab, Address4, sTab, Town, sTab, County, sTab, _
        '            Postcode, sTab, Country).Split(sTab)


        '        Dim sAddLine As String
        '        mReportAddress = ""
        '        For Each sAddLine In sAddrLines
        '            If sAddLine.Length <> 0 Then
        '                mReportAddress = String.Concat(mReportAddress, sTab, sAddLine)
        '            End If
        '        Next

        '        mReportAddress = mReportAddress.Trim(sTab)
        '        mReportAddress = mReportAddress.Replace(sTab, Environment.NewLine)

        '        Return mReportAddress
        '    End Get
        '    Set(ByVal Value As String)
        '        '  Throw New NotImplementedException
        '    End Set
        'End Property



        Public Property IsTemporaryString() As String
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(IsTemporary)
            End Get
            Set(ByVal Value As String)
                '  Throw New NotImplementedException
            End Set
        End Property
    End Class
End Namespace