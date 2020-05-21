Namespace Application.Search.Data
    <Serializable()> _
    Public Class BirdChickSearchData_Customer
        Inherits BaseBirdSearchData

        Public Sub New()
        End Sub

        Public Property HatchDate() As String
            Get
                Return mHatchDate
            End Get
            Set(ByVal Value As String)
                mHatchDate = Value
            End Set
        End Property
        Private mHatchDate As String

        Public Property NumberOfEggs() As String
            Get
                Return mNumberOfEggs
            End Get
            Set(ByVal Value As String)
                mNumberOfEggs = Value
            End Set
        End Property
        Private mNumberOfEggs As String
    End Class
End Namespace