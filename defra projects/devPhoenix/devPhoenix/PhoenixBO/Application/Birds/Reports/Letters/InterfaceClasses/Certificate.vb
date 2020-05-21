Imports uk.gov.defra.Phoenix.BO.Party
Imports uk.gov.defra.Phoenix.BO.Application.CITES.Applications

Namespace Application.Letter.Reports

    <Serializable()> _
    Public Class Certificate
        Inherits Rejection

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal permitInfoIds() As Int32, ByVal ssoUserId as Long)
            MyBase.New(ssoUserId)
            LoadCertificate(permitInfoIds)
        End Sub

        Private Sub LoadCertificate(ByVal permitInfoIds() As Int32)
            Dim idx As Int32 = -1

            SetApplication(GetApplicationId(permitInfoIds))
            SetRefusal(permitInfoIds)
            mBeginText = "THE CONVENTION ON INTERNATIONAL TRADE IN ENDANGERED SPECIES OF WILD " & _
                         "FAUNA AND FLORA (CITES) - EC REGULATIONS 338/97 AND 1808/2001" & nl & nl ' Hard Coded?
            mIntroText = "I refer to your application, received on " & ReceivedOn & _
                         " for " & CertificateType & " for:" & nl & nl
            mEndText =   RefusalReason & nl & nl & _
                         "This decision does not prevent you from using the specimen " & _
                         "for non-commercial purposes, gifting the offspring, displaying the specimen for " & _
                         "non-commercial purposes or donating it to a suitable home provided that no payment, " & _
                         "exchange or barter of any kind is involved." & nl & nl & _
                         "Whilst there is no formal right of appeal against a decision, " & _
                         "the Department is prepared to reconsider where further, relevant information is submitted. " & _
                         "Any further correspondence should be directed to your case officer below." & nl & nl & _
                         "Yours sincerely"
            FillStandardElements()
            ' Create array of Details
            ReDim mDetails(permitInfoIds.Length - 1)
            For Each infoId As Int32 In permitInfoIds
                idx += 1
                mDetails(idx) = New CertificateDetails(infoId)
            Next
        End Sub

        Public Function GetReportData(ByVal returnDS As DataSet) As ReportDataResults
            Dim newRow As DataRow = returnDS.Tables("BOCertificateRefusalLetter").NewRow()
            With newRow
                .Item("Id") = 1
                .Item("FromAddress") = Me.FromAddress
                .Item("ToAddress") = Me.ToAddress
                .Item("IssuedDate") = Me.IssuedDate
                .Item("Salutation") = Me.Salutation
                .Item("OurReference") = Me.OurReference
                .Item("BeginText") = Me.BeginText
                .Item("IntroText") = Me.IntroText
                .Item("EndText") = Me.EndText
                .Item("DirectLineText") = Me.DirectLineText
                .Item("SignatureText") = Me.SignatureText
                .Item("FaxText") = Me.FaxText
                .Item("EMailText") = Me.EmailText
            End With
            returnDS.Tables("BOCertificateRefusalLetter").Rows.Add(newRow)
            returnDS.AcceptChanges()

            For Each certificateDetails As CertificateDetails In mDetails
                newRow = returnDS.Tables("BOCertificateRefusalLetterDetails").NewRow()
                With newRow
                    .Item("Id") = 1 ' Hard Coded for Crystal Sub Report Purposes
                    .Item("DescriptionText") = certificateDetails.DescriptionText
                    .Item("ScientificNameText") = certificateDetails.ScientificNameText
                End With
                returnDS.Tables("BOCertificateRefusalLetterDetails").Rows.Add(newRow)
                returnDS.AcceptChanges()
            Next

            Return New ReportDataResults(returnDS, "")
        End Function

        Public ReadOnly Property BeginText() As String
            Get
                Return mBeginText
            End Get
        End Property
        Private mBeginText As String

        Public ReadOnly Property IntroText() As String
            Get
                Return mIntroText
            End Get
        End Property
        Private mIntroText As String

        Public ReadOnly Property Details() As CertificateDetails()
            Get
                Return mDetails
            End Get
        End Property
        Private mDetails(-1) As CertificateDetails

        Public ReadOnly Property EndText() As String
            Get
                Return mEndText
            End Get
        End Property
        Private mEndText As String

        Private Readonly Property CertificateType As String
            Get
                If TypeOf mApp Is BOCITESArticle10 Then
                    Dim app10 As BOCITESArticle10 = CType(mApp, BOCITESArticle10)
                    Return "an Article 10 " + app10.Article10TypeString + " Certificate"
                End If
                If TypeOf mApp Is BOCITESArticle30 Then
                    Return "an Article 30 Certificate"
                End If
                Throw New Exception("Unknown application type")
            End Get
        End Property

    End Class
End Namespace
