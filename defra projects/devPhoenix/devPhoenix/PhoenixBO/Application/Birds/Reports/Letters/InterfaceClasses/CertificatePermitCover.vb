Imports uk.gov.defra.Phoenix.BO.Party
Imports uk.gov.defra.Phoenix.BO.Application.CITES.Applications

Namespace Application.Letter.Reports

    <Serializable()> _
    Public Class CertificatePermitCover
        Inherits Rejection

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal applicationId As Int32, ByVal ssoUserId as Long)
            MyBase.New(ssoUserId)
            LoadCertificatePermitCover(applicationId)
            Me.OurReference = applicationId.ToString
        End Sub

        Private Sub LoadCertificatePermitCover(ByVal applicationId As Int32)
            SetApplication(applicationId)

            BeginText = "THE CONVENTION ON INTERNATIONAL TRADE IN ENDANGERED SPECIES OF WILD " & _
                        "FAUNA AND FLORA (CITES) - EC REGULATIONS 338/97 AND 1808/2001" & nl & nl ' Hard Coded?
            IntroText = "Please find enclosed the licences for which you recently applied." & nl & nl
            EndText   = "Your licences are only valid if all the details remain the same as those " & _
                        "of the specimen(s) for which you applied.  Any errors may mean that the licence is " & _
                        "invalid and may result in an offence being committed." & nl & nl & _
                        "Please check the details carefully and if there are any errors, " & _
                        "please contact us at the above address. " & nl & nl & _
                        "Yours sincerely" & nl & nl
            FillStandardElements()
        End Sub

        Public Function GetReportData(ByVal returnDS As DataSet) As ReportDataResults

            Dim newRow As DataRow = returnDS.Tables("BOCertificatePermitCoverLetter").NewRow()
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
                .Item("EMailText") = Me.EMailText
            End With
            returnDS.Tables("BOCertificatePermitCoverLetter").Rows.Add(newRow)
            returnDS.AcceptChanges()

            Return New ReportDataResults(returnDS, "")

        End Function

        Public Property BeginText() As String
            Get
                Return mBeginText
            End Get
            Set(ByVal Value As String)
                mBeginText = Value
            End Set
        End Property
        Private mBeginText As String

        Public Property IntroText() As String
            Get
                Return mIntroText
            End Get
            Set(ByVal Value As String)
                mIntroText = Value
            End Set
        End Property
        Private mIntroText As String

        Public Property Details() As CertificateDetails()
            Get
                Return mDetails
            End Get
            Set(ByVal Value As CertificateDetails())
                mDetails = Value
            End Set
        End Property
        Private mDetails(-1) As CertificateDetails

        Public Property EndText() As String
            Get
                Return mEndText
            End Get
            Set(ByVal Value As String)
                mEndText = Value
            End Set
        End Property
        Private mEndText As String
    End Class

End Namespace
