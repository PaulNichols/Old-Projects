Namespace Application.Bird.Registration.Search
    <Serializable()> _
    Public Class BirdSearchResult
        Public Sub New()
        End Sub

        Public Property Id() As Int32
            Get
                Return mId
            End Get
            Set(Byval Value As Int32)
                mId = Value
            End Set
        End Property
        Private mId As Int32

        'derived....
        Public Property Species() As String
            Get
                Return mCommonName
            End Get
            Set(Byval Value As String)
                mCommonName = Value
            End Set
        End Property

        Public Property ScientificName() As String
            Get
                Return mScientificName
            End Get
            Set(Byval Value As String)
                mScientificName = Value
            End Set
        End Property
        Private mScientificName As String

        Public Property CommonName() As String
            Get
                Return mCommonName
            End Get
            Set(Byval Value As String)
                mCommonName = Value
            End Set
        End Property
        Private mCommonName As String

        Public Property IDMarkType() As String
            Get
                Return mIDMarkType
            End Get
            Set(Byval Value As String)
                mIDMarkType = Value
            End Set
        End Property
        Private mIDMarkType As String

        Public Property IDMarkNumber() As String
            Get
                Return mIDMarkNumber
            End Get
            Set(ByVal Value As String)
                mIDMarkNumber = Value
            End Set
        End Property
        Private mIDMarkNumber As String

        Public Property Gender() As String
            Get
                Return mGender
            End Get
            Set(ByVal Value As String)
                mGender = Value
            End Set
        End Property
        Private mGender As String

        'HatchDate, DD/MM/YYYY or empty
        Public Property HatchDate() As String
            Get
                Return mHatchDate
            End Get
            Set(ByVal Value As String)
                mHatchDate = Value
            End Set
        End Property
        Private mHatchDate As String

        'HatchDateExact, Y or N or empty
        Public Property HatchDateExact() As String
            Get
                Return mHatchDateExact
            End Get
            Set(ByVal Value As String)
                mHatchDateExact = Value
            End Set
        End Property
        Private mHatchDateExact As String

        Public Property A10Ref() As String
            Get
                Return mA10Ref
            End Get
            Set(Byval Value As String)
                mA10Ref = Value
            End Set
        End Property
        Private mA10Ref As String

        Public Property Fate() As String
            Get
                Return mFate
            End Get
            Set(Byval Value As String)
                mFate = Value
            End Set
        End Property
        Private mFate As String

        'RegDocRef, e.g. "215529/01"
        Public Property RegDocRef() As String
            Get
                Return mRegDocRef
            End Get
            Set(ByVal Value As String)
                mRegDocRef = Value
            End Set
        End Property
        Private mRegDocRef As String

        Public Property FateId() As Int32
            Get
                Return mFateId
            End Get
            Set(ByVal Value As Int32)
                mFateId = Value
            End Set
        End Property
        Private mFateId As Int32

        Public Property DateAcquired() As Date
            Get
                If mDateAcquired.Ticks = 0 Then mDateAcquired = Date.Now
                Return mDateAcquired
            End Get
            Set(ByVal Value As Date)
                mDateAcquired = Value
            End Set
        End Property
        Private mDateAcquired As Date

        Public Property ECAnnex() As String
            Get
                Return mECAnnex
            End Get
            Set(ByVal Value As String)
                mECAnnex = Value
            End Set
        End Property
        Private mECAnnex As String

        Public Property OtherIdMarks() As String
            Get
                Return mOtherIdMarks
            End Get
            Set(ByVal Value As String)
                mOtherIdMarks = Value
            End Set
        End Property
        Private mOtherIdMarks As String

        Public Property OtherIdMarkTypes() As String
            Get
                Return mOtherIdMarkTypes
            End Get
            Set(ByVal Value As String)
                mOtherIdMarkTypes = Value
            End Set
        End Property
        Private mOtherIdMarkTypes As String

        Public Property CITESSourceId() As Int32
            Get
                Return mCITESSourceId
            End Get
            Set(ByVal Value As Int32)
                mCITESSourceId = Value
            End Set
        End Property
        Private mCITESSourceId As Int32

        Public Property CITESSourceId2() As Int32
            Get
                Return mCITESSourceId2
            End Get
            Set(ByVal Value As Int32)
                mCITESSourceId2 = Value
            End Set
        End Property
        Private mCITESSourceId2 As Int32

        Public Property PermitId() As Int32
            Get
                Return mPermitId
            End Get
            Set(ByVal Value As Int32)
                mPermitId = Value
            End Set
        End Property
        Private mPermitId As Int32
    End Class
End Namespace
