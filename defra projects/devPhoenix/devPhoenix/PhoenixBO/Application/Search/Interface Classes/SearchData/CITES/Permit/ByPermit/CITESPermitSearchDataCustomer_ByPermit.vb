Namespace Application.Search.Data
    Public Class CITESPermitSearchDataCustomer_ByPermit
        Inherits BaseCITESPermitSearchData_ByPermit
        Implements IDateReceived, IISOCodeDescription, IPD

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

        Public Property PD() As String Implements IPD.PD
            Get
                Return mPD
            End Get
            Set(ByVal Value As String)
                mPD = Value
            End Set
        End Property
        Private mPD As String
    End Class
End Namespace