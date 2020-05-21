Namespace Application.Search.Data
    Public MustInherit Class BaseCITESPermitSearchData_ByApplication
        Inherits BaseCITESPermitSearchData
        Implements INumberOfPermits, IApplicationNumber

        Public Sub New()
        End Sub

        Public Property ApplicationNumber() As String Implements IApplicationNumber.ApplicationNumber
            Get
                Return mApplicationNumber
            End Get
            Set(ByVal Value As String)
                mApplicationNumber = Value
            End Set
        End Property
        Private mApplicationNumber As String

        Public Property NumberOfPermits() As String Implements INumberOfPermits.NumberOfPermits
            Get
                Return mNumberOfPermits
            End Get
            Set(ByVal Value As String)
                mNumberOfPermits = Value
            End Set
        End Property
        Private mNumberOfPermits As String
    End Class
End Namespace