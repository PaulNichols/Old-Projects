
Namespace ReportCriteria
    <Serializable()> _
    Public Class ViewCaseTypesSpeciesCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property FromMonth() As Int32
            Get
                Return mFromMonth
            End Get
            Set(ByVal Value As Int32)
                mFromMonth = Value
            End Set
        End Property
        Private mFromMonth As Int32

        Public Property FromYear() As Int32
            Get
                Return mFromYear
            End Get
            Set(ByVal Value As Int32)
                mFromYear = Value
            End Set
        End Property
        Private mFromYear As Int32

        Public Property ToMonth() As Int32
            Get
                Return mToMonth
            End Get
            Set(ByVal Value As Int32)
                mToMonth = Value
            End Set
        End Property
        Private mToMonth As Int32

        Public Property ToYear() As Int32
            Get
                Return mToYear
            End Get
            Set(ByVal Value As Int32)
                mToYear = Value
            End Set
        End Property
        Private mToYear As Int32

        Public Property applicationType() As Object
            Get
                Return mApplicationType
            End Get
            Set(ByVal Value As Object)
                mApplicationType = Value
            End Set
        End Property
        Private mApplicationType As Object

        Public Property permitStatus() As Object
            Get
                Return mPermitStatus
            End Get
            Set(ByVal Value As Object)
                mPermitStatus = Value
            End Set
        End Property
        Private mPermitStatus As Object

        Public Property source() As Object
            Get
                Return mSource

            End Get
            Set(ByVal Value As Object)
                mSource = Value
            End Set
        End Property
        Private mSource As Object

        Public Property origin() As Object
            Get
                Return mOrigin
            End Get
            Set(ByVal Value As Object)
                mOrigin = Value
            End Set
        End Property
        Private mOrigin As Object

        Public Property purpose() As Object
            Get
                Return mPurpose
            End Get
            Set(ByVal Value As Object)
                mPurpose = Value
            End Set
        End Property
        Private mPurpose As Object

        Public Property species() As Object
            Get
                Return mSpecies
            End Get
            Set(ByVal Value As Object)
                mSpecies = Value
            End Set
        End Property
        Private mSpecies As Object

        Public Property countryOfExport() As Object
            Get
                Return mCountryOfExport
            End Get
            Set(ByVal Value As Object)
                mCountryOfExport = Value
            End Set
        End Property
        Private mCountryOfExport As Object

        Public Property countryOfDestination() As Object
            Get
                Return mCountryOfDestination

            End Get
            Set(ByVal Value As Object)
                mCountryOfDestination = Value
            End Set
        End Property
        Private mCountryOfDestination As Object

        Public Property applicantId() As Object
            Get
                Return mApplicantId

            End Get
            Set(ByVal Value As Object)
                mApplicantId = Value
            End Set
        End Property
        Private mApplicantId As Object

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.ViewCaseTypesSpecies
            End Get
        End Property
    End Class

End Namespace
