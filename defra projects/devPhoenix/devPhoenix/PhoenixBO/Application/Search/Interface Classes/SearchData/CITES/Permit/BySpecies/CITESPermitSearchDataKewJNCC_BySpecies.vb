Namespace Application.Search.Data
    Public Class CITESPermitSearchDataKewJNCC_BySpecies
        Inherits BaseCITESPermitSearchData_BySpecies
        Implements IAssignedTo, INumberOfPermits, IDateReferred

        Public Sub New()
        End Sub

        Public Property AssignedTo() As String Implements IAssignedTo.AssignedTo
            Get
                Return mAssignedTo
            End Get
            Set(ByVal Value As String)
                mAssignedTo = Value
            End Set
        End Property
        Private mAssignedTo As String

        Public Property NumberOfPermits() As String Implements INumberOfPermits.NumberOfPermits
            Get
                Return mNumberOfPermits
            End Get
            Set(ByVal Value As String)
                mNumberOfPermits = Value
            End Set
        End Property
        Private mNumberOfPermits As String

        Public Property DateReferred() As String Implements IDateReferred.DateReferred
            Get
                Return mDateReferred
            End Get
            Set(ByVal Value As String)
                mDateReferred = Value
            End Set
        End Property
        Private mDateReferred As String
    End Class
End Namespace