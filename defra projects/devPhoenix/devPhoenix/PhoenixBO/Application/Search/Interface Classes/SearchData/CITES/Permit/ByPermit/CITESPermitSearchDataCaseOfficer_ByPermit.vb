Namespace Application.Search.Data
    Public Class CITESPermitSearchDataCaseOfficer_ByPermit
        Inherits BaseCITESPermitSearchData_ByPermit
        Implements IDateReceived, IISOCode, IAssignedTo, IPaid, IPD, ISAAdvice, IInspectorateAdvice, IReferred, IReIssued

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

        Public Property PD() As String Implements IPD.PD
            Get
                Return mPD
            End Get
            Set(ByVal Value As String)
                mPD = Value
            End Set
        End Property
        Private mPD As String

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

        Public Property SAAdvice() As String Implements ISAAdvice.SAAdvice
            Get
                Return mSAAdvice
            End Get
            Set(ByVal Value As String)
                mSAAdvice = Value
            End Set
        End Property
        Private mSAAdvice As String

        Public Property Referred() As String Implements IReferred.Referred
            Get
                Return mReferred
            End Get
            Set(ByVal Value As String)
                mReferred = Value
            End Set
        End Property
        Private mReferred As String

        Public Property InspectorateAdvice() As String Implements IInspectorateAdvice.InspectorateAdvice
            Get
                Return mInspectorateAdvice
            End Get
            Set(ByVal Value As String)
                mInspectorateAdvice = Value
            End Set
        End Property
        Private mInspectorateAdvice As String

        Public Property Reissued() As String Implements IReissued.Reissued
            Get
                Return mReissued
            End Get
            Set(ByVal Value As String)
                mReissued = Value
            End Set
        End Property
        Private mReissued As String
    End Class
End Namespace