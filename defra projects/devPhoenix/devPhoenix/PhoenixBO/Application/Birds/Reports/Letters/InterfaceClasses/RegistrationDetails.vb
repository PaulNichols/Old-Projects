
Namespace Application.Letter.Reports

    <Serializable()> _
    Public Class RegistrationDetails

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal specimenId As Int32)
            MyBase.New()
            LoadRegistrationDetails(specimenId)
        End Sub

        Private Sub LoadRegistrationDetails(ByVal specimenId As Int32)


            Dim eg As String

            mIdMarkTypeText = "<!IdMarkTypeText1!>"

            ' Example IdMarkTypeText1
            eg = "Closed Ring"
            mIdMarkTypeText = mIdMarkTypeText.Replace("<!IdMarkTypeText1!>", eg)

            mIdMarkText = "<!IdMarkText1!>"

            ' Example IdMarkText1
            eg = "12345678"
            mIdMarkText = mIdMarkText.Replace("<!IdMarkText1!>", eg)

            mCommonNameText = "<!CommonNameText1!>"

            ' Example CommonNameText1
            eg = "Peregrine falcon - a big bird of prey with an eagle eye for small fury creatures"
            mCommonNameText = mCommonNameText.Replace("<!CommonNameText1!>", eg)

            mScientificNameText = "<!ScientificNameText1!>"

            ' Example ScientificNameText1
            eg = "(Falco peregrinus)"
            mScientificNameText = mScientificNameText.Replace("<!ScientificNameText1!>", eg)

        End Sub


        Private Sub LoadTestRegistrationDetails(ByVal specimenId As Int32)
            Dim eg As String

            mIdMarkTypeText = "<!IdMarkTypeText1!>"

            ' Example IdMarkTypeText1
            eg = "Closed Ring"
            mIdMarkTypeText = mIdMarkTypeText.Replace("<!IdMarkTypeText1!>", eg)

            mIdMarkText = "<!IdMarkText1!>"

            ' Example IdMarkText1
            eg = "12345678"
            mIdMarkText = mIdMarkText.Replace("<!IdMarkText1!>", eg)

            mCommonNameText = "<!CommonNameText1!>"

            ' Example CommonNameText1
            eg = "Peregrine falcon - a big bird of prey with an eagle eye for small fury creatures"
            mCommonNameText = mCommonNameText.Replace("<!CommonNameText1!>", eg)

            mScientificNameText = "<!ScientificNameText1!>"

            ' Example ScientificNameText1
            eg = "(Falco peregrinus)"
            mScientificNameText = mScientificNameText.Replace("<!ScientificNameText1!>", eg)

        End Sub
        Public ReadOnly Property IdMarkTypeText() As String
            Get
                Return mIdMarkTypeText
            End Get
        End Property
        Private mIdMarkTypeText As String

        Public ReadOnly Property IdMarkText() As String
            Get
                Return mIdMarkText
            End Get
        End Property
        Private mIdMarkText As String

        Public ReadOnly Property CommonNameText() As String
            Get
                Return mCommonNameText
            End Get
        End Property
        Private mCommonNameText As String

        Public ReadOnly Property ScientificNameText() As String
            Get
                Return mScientificNameText
            End Get
        End Property
        Private mScientificNameText As String

    End Class

End Namespace
