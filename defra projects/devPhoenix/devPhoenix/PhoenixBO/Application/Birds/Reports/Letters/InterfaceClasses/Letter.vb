Imports uk.gov.defra.Phoenix.BO.Party
Imports uk.gov.defra.Phoenix.BO.Application.CITES.Applications
Imports uk.gov.defra.Phoenix.BO.Application.Bird.Registration

Namespace Application.Letter.Reports

    <Serializable()> _
    Public MustInherit Class Letter
        'Inherits BaseBO
        Implements ILetter

        Protected nl As String = Environment.NewLine
        Protected mUser As BOAuthorisedUser
        Protected mApp As BOCITESApplication
        Protected mBirdreg As BirdRegistration
        Protected mParty As BOParty
        Protected mAddress As BOAddress

        Public Sub New()
            MyBase.New()
            LoadTestData()
        End Sub

        Public Sub New(ByVal ssoUserId as Long)
            mUser = New BOAuthorisedUser(ssoUserId)
        End Sub

        Protected Sub SetApplication(ByVal applicationId As Int32)
            mApp = CType(BOApplication.PolymorphicCreate(applicationId), BOCITESApplication)
            mParty = GetPartyOrAgent(mApp)
            mAddress = mParty.GetMailingAddress(Nothing)
            FromAddress = mApp.IssuingAuthorityAddress
        End Sub

        Protected Sub SetBirdReg(ByVal applicationId As Int32)
            mBirdreg = New BirdRegistration(applicationId)
            mParty = BOParty.PolymorphicCreate(mBirdreg.PartyId)
            mAddress = mParty.GetMailingAddress(Nothing)

            ' Hard coded From Address
            FromAddress = "Wildlife Licensing and Registration Service" & nl & _
           "Global Wildlife Division" & nl & _
           "FLOOR 1 / ZONE  17" & nl & _
           "TEMPLE QUAY HOUSE" & nl & _
           "2 THE SQUARE, TEMPLE QUAY" & nl & _
           "BRISTOL	BS1 6EB" & nl & nl
        End Sub

        Protected Function GetApplicationId(ByVal permitInfoIds() As Int32) As Int32
            If permitInfoIds Is Nothing OrElse permitInfoIds.Length = 0 Then
                Throw New Exception("No permit info id")
            End If
            Dim info As New BOPermitInfo(permitInfoIds(0))
            Return BOPermit.PolymorphicCreate(info.PermitId).ApplicationId
        End Function

        Protected Sub FillStandardElements()
            ToAddress = mParty.DisplayName & nl & mAddress.ReportAddress
            IssuedDate = Date.Now.ToString("dd/MM/yyyy")
            DirectLineText = mUser.STD
            SignatureText = GetFullName()
            FaxText = mUser.Fax
            EmailText = mUser.Email
            If mParty.IsBusiness Then
                Salutation = "Dear Sir/Madam"
            Else
                Dim person As BOPartyIndividual = CType(mParty, BOPartyIndividual)
                Salutation = "Dear " + person.Title + " " + CType(person.Surname, String)
            End If
        End Sub

        Private Function GetFullName() As String
            Dim split1() As String = mUser.FullName.Split("(".ToCharArray())
            Dim split2() As String = split1(0).Split(",".ToCharArray())
            If split2.Length = 1 Then Return split2(0).Trim() Else Return split2(1).Trim() + " " + split2(0).Trim()
        End Function

        Private Function GetPartyOrAgent(ByVal app As BOApplication) As BOParty
            If app.Party Is Nothing Then
                If app.Agent Is Nothing Then
                    Throw New Exception("No Party or Agent for this Application")
                End If
                Return app.Agent.Party
            End If
            Return app.Party.Party
        End Function

        Private Sub LoadTestData()
            Dim eg As String

            mFromAddress = "<!FromAddress1!>"

            ' Example FromAddress1
            eg = "Wildlife Licensing and Registration Service" & nl & _
           "Global Wildlife Division" & nl & _
           "FLOOR 1 / ZONE  17" & nl & _
           "TEMPLE QUAY HOUSE" & nl & _
           "2 THE SQUARE, TEMPLE QUAY" & nl & _
           "BRISTOL	BS1 6EB" & nl & nl
            mFromAddress = mFromAddress.Replace("<!FromAddress1!>", eg)

            mToAddress = "<!ToAddress1!>"

            ' Example ToAddress1
            eg = "UK Applicant’s surname or Agent’s" & nl & _
            "Contact Line (eg. C/o…)" & nl & _
            "Address" & nl & _
            "Address" & nl & _
            "Address" & nl & _
            "Address" & nl & _
            "Town" & nl & _
            "County" & nl & _
            "Postcode" & nl & _
            "Country  (non UK only)" & nl & nl
            mToAddress = mToAddress.Replace("<!ToAddress1!>", eg)

            mIssuedDate = "<!IssuedDate1!>"

            ' Example IssuedDate1
            eg = "xx/xx/200x"
            mIssuedDate = mIssuedDate.Replace("<!IssuedDate1!>", eg)

            mSalutation = "Dear <!Salutation1!>"

            ' Example Salutation1
            eg = "Sir/Madam"
            mSalutation = mSalutation.Replace("<!Salutation1!>", eg)

            mSignatureText = "<!Signature1!>"

            ' Example Signature1
            eg = "Fred Bloggs"
            mSignatureText = mSignatureText.Replace("<!Signature1!>", eg)

            mDirectLineText = "<!DirectLineText1!>"

            ' Example DirectLineText1
            eg = "0117 372 6072"
            mDirectLineText = mDirectLineText.Replace("<!DirectLineText1!>", eg)

            mFaxText = "<!FaxText1!>" & nl

            ' Example EndText3
            eg = "0117 372 8206"
            mFaxText = mFaxText.Replace("<!FaxText1!>", eg)

            mEmailText = "<!EMailText1!>"

            ' Example EndText4
            eg = "fred.bloggs@defra.gsi.gov.uk"
            mEmailText = mEmailText.Replace("<!EMailText1!>", eg)
        End Sub

        Public Property FromAddress() As String Implements ILetter.FromAddress
            Get
                Return mFromAddress
            End Get
            Set(ByVal Value As String)
                mFromAddress = Value
            End Set
        End Property
        Private mFromAddress As String

        Public Property IssuedDate() As String Implements ILetter.IssuedDate
            Get
                Return mIssuedDate
            End Get
            Set(ByVal Value As String)
                mIssuedDate = Value
            End Set
        End Property
        Private mIssuedDate As String

        Public Property Salutation() As String Implements ILetter.Salutation
            Get
                Return mSalutation
            End Get
            Set(ByVal Value As String)
                mSalutation = Value
            End Set
        End Property
        Private mSalutation As String

        Public Property ToAddress() As String Implements ILetter.ToAddress
            Get
                Return mToAddress
            End Get
            Set(ByVal Value As String)
                mToAddress = Value
            End Set
        End Property
        Private mToAddress As String

        Public Property DirectLineText() As String Implements ILetter.DirectLineText
            Get
                Return mDirectLineText
            End Get
            Set(ByVal Value As String)
                mDirectLineText = Value
            End Set
        End Property
        Private mDirectLineText As String

        Public Property EmailText() As String Implements ILetter.EmailText
            Get
                Return mEmailText
            End Get
            Set(ByVal Value As String)
                mEmailText = Value
            End Set
        End Property
        Private mEmailText As String

        Public Property FaxText() As String Implements ILetter.FaxText
            Get
                Return mFaxText
            End Get
            Set(ByVal Value As String)
                mFaxText = Value
            End Set
        End Property
        Private mFaxText As String

        Public Property SignatureText() As String Implements ILetter.SignatureText
            Get
                Return mSignatureText
            End Get
            Set(ByVal Value As String)
                mSignatureText = Value
            End Set
        End Property
        Private mSignatureText As String

        Protected ReadOnly Property ReceivedOn() As String
            Get
                Return mApp.ReceivedDate.ToString("dd/MM/yyyy")
            End Get
        End Property
    End Class
End Namespace
