Namespace Application.Search.Data
    Public Class CITESPermitSearchDataInspectorate_ByPermit
        Inherits BaseCITESPermitSearchData_ByPermit
        Implements IAssignedTo, IDateReferred, IInspectorateAdvice

        Public Sub New()
        End Sub

        Public Property DateReferred() As String Implements IDateReferred.DateReferred
            Get
                Return mDateReferred
            End Get
            Set(ByVal Value As String)
                mDateReferred = Value
            End Set
        End Property
        Private mDateReferred As String

        Public Property AssignedTo() As String Implements IAssignedTo.AssignedTo
            Get
                Return mAssignedTo
            End Get
            Set(ByVal Value As String)
                mAssignedTo = Value
            End Set
        End Property
        Private mAssignedTo As String

        Public Property InspectorateAdvice() As String Implements IInspectorateAdvice.InspectorateAdvice
            Get
                Return mInspectorateAdvice
            End Get
            Set(ByVal Value As String)
                mInspectorateAdvice = Value
            End Set
        End Property
        Private mInspectorateAdvice As String
    End Class
End Namespace