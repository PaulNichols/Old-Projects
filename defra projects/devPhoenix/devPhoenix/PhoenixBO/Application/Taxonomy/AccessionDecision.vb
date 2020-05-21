Namespace Taxonomy
    <Serializable()> _
    Public Class AccessionDecision

        Public Sub New()

        End Sub

        Private Article4point6Value As String
        Public Property Article4point6() As String
            Get
                Return Article4point6Value
            End Get
            Set(ByVal Value As String)
                Article4point6Value = Value
            End Set
        End Property

        Private DecisionDateValue As String
        Public Property DecisionDate() As String
            Get
                Return DecisionDateValue
            End Get
            Set(ByVal Value As String)
                DecisionDateValue = Value
            End Set
        End Property

        Private SRGOpinionValue As String
        Public Property SRGOpinion() As String
            Get
                Return SRGOpinionValue
            End Get
            Set(ByVal Value As String)
                SRGOpinionValue = Value
            End Set
        End Property

        Private CountryValue As String
        Public Property Country() As String
            Get
                Return CountryValue
            End Get
            Set(ByVal Value As String)
                CountryValue = Value
            End Set
        End Property


    End Class
End Namespace
