Namespace Taxonomy

    <Serializable()> _
    Public Class CommonName

        Private NameValue As String
        Public Property Name() As String
            Get
                Return (NameValue)
            End Get
            Set(ByVal Value As String)
                NameValue = Value
            End Set
        End Property

        Private AreaOfUseValue As String
        Public Property AreaOfUse() As String
            Get
                Return AreaOfUseValue
            End Get
            Set(ByVal Value As String)
                AreaOfUseValue = Value
            End Set
        End Property

        Private IsProductIndicatorValue As Boolean
        Public Property IsProductIndicator() As Boolean
            Get
                Return IsProductIndicatorValue
            End Get
            Set(ByVal Value As Boolean)
                IsProductIndicatorValue = Value
            End Set
        End Property
    End Class

End Namespace
