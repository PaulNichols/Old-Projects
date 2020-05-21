Namespace Application.Search.Data
    Public Class CITESPermitSearchDataCustomer_BySpecies
        Inherits BaseCITESPermitSearchData_BySpecies
        Implements IDateReceived, IISOCodeDescription, IPaid, INumberOfPermits

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

        Public Property ISOCodeDescription() As String Implements IISOCodeDescription.ISOCodeDescription
            Get
                Return mISOCodeDescription
            End Get
            Set(ByVal Value As String)
                mISOCodeDescription = Value
            End Set
        End Property
        Private mISOCodeDescription As String

        Public Property NumberOfPermits() As String Implements INumberOfPermits.NumberOfPermits
            Get
                Return mNumberOfPermits
            End Get
            Set(ByVal Value As String)
                mNumberOfPermits = Value
            End Set
        End Property
        Private mNumberOfPermits As String

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