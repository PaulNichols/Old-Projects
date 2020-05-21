Namespace Application.Search.Data
    Public MustInherit Class BaseCITESSearchData
        Inherits BaseSearchData

        Public Sub New()
        End Sub

        Friend Property Count() As Int32
            Get
                Return mCount
            End Get
            Set(ByVal Value As Int32)
                mCount = Value
            End Set
        End Property
        Private mCount As Int32
    End Class
End Namespace