Namespace Party
    Public Class BOGatewayPreferredDetails
        Private m_PartyId As Integer
        Private m_ContactId As Integer
        Private m_DisplayName As String
        Private m_EmailAddress As String
        Private m_Title As String
        Private m_Forename As String
        Private m_Surname As String
        Private m_MailingId As Integer
        Private m_Preferred As Boolean
        Private m_EmailType As String
        Private m_PersonId As Integer

        Public Property MailingAddressId() As Integer
            Get
                Return m_MailingId
            End Get
            Set(ByVal Value As Integer)
                m_MailingId = Value
            End Set
        End Property

        Public Property Preferred() As Boolean
            Get
                Return m_Preferred
            End Get
            Set(ByVal Value As Boolean)
                m_Preferred = Value
            End Set
        End Property

        Public Property EmailType() As String
            Get
                Return m_EmailType
            End Get
            Set(ByVal Value As String)
                m_EmailType = Value
            End Set
        End Property

        Public Property PersonId() As Integer
            Get
                Return m_PersonId
            End Get
            Set(ByVal Value As Integer)
                m_PersonId = Value
            End Set
        End Property

        Public Property DisplayName() As String
            Get
                Return m_DisplayName
            End Get
            Set(ByVal Value As String)
                m_DisplayName = Value
            End Set
        End Property

        Public Property DropdownDisplayName() As String
            Get
                Dim strB As System.Text.StringBuilder = New System.Text.StringBuilder("")
                strB.Append(DisplayName)
                strB.Append(" (")
                strB.Append(EmailAddress & " - ")
                strB.Append(EmailType & ")")
                Return strB.ToString
            End Get
            Set(ByVal Value As String)

            End Set
        End Property
        Public Property EmailAddress() As String
            Get
                Return m_EmailAddress
            End Get
            Set(ByVal Value As String)
                m_EmailAddress = Value
            End Set
        End Property
        Public Property Title() As String
            Get
                Return m_Title
            End Get
            Set(ByVal Value As String)
                m_Title = Value
            End Set
        End Property
        Public Property Forename() As String
            Get
                Return m_Forename
            End Get
            Set(ByVal Value As String)
                m_Forename = Value
            End Set
        End Property
        Public Property Surname() As String
            Get
                Return m_Surname
            End Get
            Set(ByVal Value As String)
                m_Surname = Value
            End Set
        End Property

        Public Property PartyId() As Integer
            Get
                Return m_PartyId
            End Get
            Set(ByVal Value As Integer)
                m_PartyId = Value
            End Set
        End Property
        Public Property ContactId() As Integer
            Get
                Return m_ContactId
            End Get
            Set(ByVal Value As Integer)
                m_ContactId = Value
            End Set
        End Property

        Public Shared Function Load(ByVal id As Int32, ByVal blnPreferred As Boolean, ByVal tran As SqlClient.SqlTransaction) As BOGatewayPreferredDetails()
            ' This can be used to return an array list consisting of all Details, or of the Preferred email contact details (one element in array)
            Dim myDS As DataSet = DataObjects.Sprocs.usp_GatewayPreferredEmailAddressAndContact(id, blnPreferred, tran, GetType(System.Data.DataSet))
            Return InitialiseObj(myDS)

        End Function
        Public Shared Function Load(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As BOGatewayPreferredDetails
            ' This just returns the one Preferred email contact and details.
            Dim myDS As DataSet = DataObjects.Sprocs.usp_GatewayPreferredEmailAddressAndContact(id, True, tran, GetType(System.Data.DataSet))
            Return InitialiseObj(myDS)(0)

        End Function
        Private Shared Function InitialiseObj(ByVal myDS As DataSet) As Party.BOGatewayPreferredDetails()
            Dim myObj(myDS.Tables(0).Rows.Count - 1) As Party.BOGatewayPreferredDetails

            With myDS.Tables(0)
                For myLoop As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Dim MalingAddressId As Int32
                    If Not .Rows(myLoop)("MailingAddressId") Is Convert.DBNull Then
                        MalingAddressId = Integer.Parse(.Rows(myLoop)("MailingAddressId").ToString)
                    Else
                        MalingAddressId = 0
                    End If
                    myObj(myLoop) = InitialiseObj(Integer.Parse(.Rows(myLoop)("ContactId").ToString), _
                                        Integer.Parse(.Rows(myLoop)("PartyId").ToString), _
                                        .Rows(myLoop)("Title").ToString, _
                                        .Rows(myLoop)("Forename").ToString, _
                                        .Rows(myLoop)("Surname").ToString, _
                                        .Rows(myLoop)("DisplayName").ToString, _
                                         MalingAddressId, _
                                        (.Rows(myLoop)("Preferred").ToString = "1" Or .Rows(myLoop)("Preferred").ToString = "True"), _
                                        .Rows(myLoop)("EmailType").ToString, _
                                        Integer.Parse(.Rows(myLoop)("PersonId").ToString), _
                                        .Rows(myLoop)("EmailAddress").ToString)

                Next

            End With
            Return myObj

        End Function
        Private Shared Function InitialiseObj(ByVal pContactID As Integer, ByVal pPartyid As Integer, _
                                    ByVal pTitle As String, ByVal pForename As String, _
                                    ByVal pSurname As String, ByVal pDisplayname As String, _
                                    ByVal pMailingAddressId As Integer, ByVal pPreferred As Boolean, _
                                    ByVal pEmailType As String, ByVal pPersonId As Integer, _
                                    ByVal pEmailAddress As String) As Party.BOGatewayPreferredDetails

            Dim myObj As Party.BOGatewayPreferredDetails = New BOGatewayPreferredDetails
            With myObj
                .ContactId = pContactID
                .DisplayName = pDisplayname
                .EmailAddress = pEmailAddress
                .Forename = pForename
                .Surname = pSurname
                .PartyId = pPartyid
                .Title = pTitle
                .MailingAddressId = pMailingAddressId
                .Preferred = pPreferred
                .EmailType = pEmailType
                .PersonId = pPersonId
            End With
            Return myObj
        End Function

        Public Sub New(ByVal pContactID As Integer, ByVal pPartyid As Integer, ByVal pTitle As String, _
                        ByVal pForename As String, ByVal pSurname As String, ByVal pDisplayname As String, _
                        ByVal pMailingAddressId As Integer, ByVal pPreferred As Boolean, _
                        ByVal pEmailType As String, ByVal pPersonId As Integer, _
                        ByVal pEmailAddress As String)

            MyBase.new()
            InitialiseObj(pContactID, pPartyid, pTitle, pForename, pSurname, pDisplayname, pMailingAddressId, pPreferred, pEmailType, pPersonId, pEmailAddress)

        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal id As Int32, ByVal blnPreferred As Boolean, ByVal tran As SqlClient.SqlTransaction)
            Load(id, blnPreferred, tran)
        End Sub

        Public Sub New(ByVal id As Int32, ByVal blnPreferred As Boolean)

            Load(id, blnPreferred, Nothing)

        End Sub

    End Class
End Namespace
