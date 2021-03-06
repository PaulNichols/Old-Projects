Namespace Application.Search.Data
    Public MustInherit Class BaseBirdSearchData
        Inherits BaseSearchData
        Implements IScientificName, IApplicationNumber

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

        Public Property DateRequestSubmitted() As String
            Get
                Return mDateRequestSubmitted
            End Get
            Set(ByVal Value As String)
                mDateRequestSubmitted = Value
            End Set
        End Property
        Private mDateRequestSubmitted As String

        Public Property ScientificName() As String Implements IScientificName.ScientificName
            Get
                Return mScientificName
            End Get
            Set(ByVal Value As String)
                mScientificName = Value
            End Set
        End Property
        Private mScientificName As String

        Public Property Status() As String
            Get
                Return mStatus
            End Get
            Set(ByVal Value As String)
                mStatus = Value
            End Set
        End Property
        Private mStatus As String

    End Class
End Namespace