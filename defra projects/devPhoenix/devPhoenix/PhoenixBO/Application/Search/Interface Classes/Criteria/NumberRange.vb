Namespace Application.Search
    <Serializable()> _
    Public Enum NumberRangeList
        Greater_Than
        Less_Than
        Equal_To
    End Enum

    <Serializable()> _
    Public Class NumberRange
        Public Sub New()
        End Sub

        Public Property Range() As NumberRangeList
            Get
                Return mRange
            End Get
            Set(ByVal Value As NumberRangeList)
                mRange = Value
            End Set
        End Property
        Private mRange As NumberRangeList

        Public Property Number() As Object
            Get
                Return mNumber
            End Get
            Set(ByVal Value As Object)
                mNumber = Value
            End Set
        End Property
        Private mNumber As Object
    End Class
End Namespace
