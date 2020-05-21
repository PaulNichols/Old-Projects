Namespace Application.Search.Data
    Public Class CITESPermitSearchDataCaseOfficer_ByApplication
        Inherits BaseCITESPermitSearchData_ByApplication
        Implements IDateReceived, IISOCode, IAssignedTo, IPaid

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