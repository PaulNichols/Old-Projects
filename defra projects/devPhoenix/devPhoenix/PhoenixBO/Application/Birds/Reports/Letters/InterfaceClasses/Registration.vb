
Namespace Application.Letter.Reports

    <Serializable()> _
    Public Class Registration
        Inherits Rejection

        Public Sub New()
            MyBase.New()
        End Sub


        Public Sub New(ByVal applicationId As Int32, ByVal ssoUserId As Long)

            MyBase.New(ssoUserId)
            LoadRegistration(applicationId)

        End Sub

        Private Sub LoadRegistration(ByVal applicationId As Int32)


            SetBirdReg(applicationId)
            SetRefusal(applicationId)

            Dim eg As String

            mBeginText = "WILDLIFE AND COUNTRYSIDE ACT 1981: REGISTRATION OF SCHEDULE 4 BIRDS" ' Hard Coded?

            mIntroText = "I refer to your application to register the following bird(s):"

            ' Create array of Details
            ReDim mDetails(0)
            mDetails(0) = New RegistrationDetails(1) ' Test Data
            'Dim idx As Int32 = -1
            'For Each permitId As Int32 In permitIds
            '    idx += 1
            '    mDetails(idx) = New RegistrationDetails(permitId)
            'Next

            mEndText = "I regret to inform you that this bird cannot be registered in your name for the following reason:" & nl & nl

            mEndText = mEndText & RefusalReason & nl & nl

            mEndText = mEndText & "I hope the above is clear but if you need any further information " & _
            "or clarification please do not hesitate to contact me." & nl & nl

            mEndText = mEndText & "Yours sincerely"

            FillStandardElements()

        End Sub

        Public Function GetReportData(ByVal returnDS As DataSet) As ReportDataResults

            Dim newRow As DataRow = returnDS.Tables("BORegistrationRefusalLetter").NewRow()
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
            returnDS.Tables("BORegistrationRefusalLetter").Rows.Add(newRow)
            returnDS.AcceptChanges()


            For Each registrationDetails As registrationDetails In mDetails

                newRow = returnDS.Tables("BORegistrationRefusalLetterDetails").NewRow()
                With newRow

                    .Item("Id") = 1 ' Hard Coded for Crystal Sub Report Purposes
                    .Item("IdMarkTypeText") = registrationDetails.IdMarkTypeText
                    .Item("IdMarkText") = registrationDetails.IdMarkText
                    .Item("CommonNameText") = registrationDetails.CommonNameText
                    .Item("ScientificNameText") = registrationDetails.ScientificNameText

                End With
                returnDS.Tables("BORegistrationRefusalLetterDetails").Rows.Add(newRow)
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

        Public ReadOnly Property Details() As RegistrationDetails()
            Get
                Return mDetails
            End Get
        End Property
        Private mDetails(-1) As RegistrationDetails

        Public ReadOnly Property EndText() As String
            Get
                Return mEndText
            End Get
        End Property
        Private mEndText As String

    End Class

End Namespace
