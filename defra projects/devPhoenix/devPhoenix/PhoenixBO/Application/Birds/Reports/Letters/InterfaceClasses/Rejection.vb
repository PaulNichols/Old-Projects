Imports uk.gov.defra.Phoenix.BO.Application.Bird.Registration

Namespace Application.Letter.Reports

    <Serializable()> _
    Public MustInherit Class Rejection
        Inherits Letter
        Implements IRejection

        Public Sub New()
            MyBase.New()
            LoadTestData()
        End Sub

        Public Sub New(ByVal ssoUserId As Long)
            MyBase.New(ssoUserId)
        End Sub

        Protected Sub SetRefusal(ByVal permitInfoIds() As Int32)
            Dim info As New BOPermitInfo(permitInfoIds(0))
            Dim reason As ReferenceData.BORefusalReason = info.RefusalReason
            If reason Is Nothing Then
                Throw New Exception("No refusal reason")
            End If
            mOurReference = mApp.ApplicationId.ToString()
            mRefusalReason = reason.Description
        End Sub

        Protected Sub SetRefusal(ByVal applicationId As Int32)
            mOurReference = mbirdreg.ApplicationId.ToString()
            mRefusalReason = mbirdreg.DeclineReason
        End Sub
        Private Sub LoadTestData()
            Dim eg As String
            mOurReference = "<!OurReference1!>"

            ' Example OurReference1
            eg = "256114/01/01-01/04"
            mOurReference = mOurReference.Replace("<!OurReference1!>", eg)

        End Sub

        Public Property OurReference() As String Implements IRejection.OurReference
            Get
                Return mOurReference
            End Get
            Set(ByVal Value As String)
                mOurReference = Value
            End Set
        End Property
        Private mOurReference As String

        Public Property RefusalReason() As String
            Get
                Return mRefusalReason
            End Get
            Set(ByVal Value As String)
                mRefusalReason = Value
            End Set
        End Property
        Private mRefusalReason As String
    End Class

End Namespace

