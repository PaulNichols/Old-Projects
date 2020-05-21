Namespace Application.Search.Data
    Public Class CITESPermitSearchDataCaseOfficer_BySpecies
        Inherits BaseCITESPermitSearchData_BySpecies
        Implements IDateReceived, IISOCode, IAssignedTo, IPaid, INumberOfPermits

        Public Sub New()
        End Sub

        Public Property DateReceived() As String Implements IDateReceived.DateReceived
            Get
                Return mDateReceived
            End Get
            Set(ByVal Value As String)
                mDateReceived = Value
            End Set
        End Property
        Private mDateReceived As String

        Public Property ISOCode() As String Implements IISOCode.ISOCode
            Get
                Return mISOCode
            End Get
            Set(ByVal Value As String)
                mISOCode = Value
            End Set
        End Property
        Private mISOCode As String

        Public Property NumberOfPermits() As String Implements INumberOfPermits.NumberOfPermits
            Get
                Return mNumberOfPermits
            End Get
            Set(ByVal Value As String)
                mNumberOfPermits = Value
            End Set
        End Property
        Private mNumberOfPermits As String

        Public Property AssignedTo() As String Implements IAssignedTo.AssignedTo
            Get
                Return mAssignedTo
            End Get
            Set(ByVal Value As String)
                mAssignedTo = Value
            End Set
        End Property
        Private mAssignedTo As String

        Public Property Paid() As String Implements IPaid.Paid
            Get
                Return mPaid
            End Get
            Set(ByVal Value As String)
                mPaid = Value
            End Set
        End Property
        Private mPaid As String
    End Class
End Namespace