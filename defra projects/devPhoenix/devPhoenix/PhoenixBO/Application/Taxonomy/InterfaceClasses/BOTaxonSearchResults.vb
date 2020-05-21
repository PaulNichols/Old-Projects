Namespace Taxonomy
    Public Class BOTaxonSearchResults
        Implements ITaxonSearchResults

        Public Property Messages() As String() Implements ITaxonSearchResults.Messages
            Get
                Return mMessages
            End Get
            Set(ByVal Value() As String)
                mMessages = Value
            End Set
        End Property
        Private mMessages() As String

        Public Property Taxa() As Taxonomy.BOTaxon() Implements ITaxonSearchResults.Taxa
            Get
                Return mTaxa
            End Get
            Set(ByVal Value() As Taxonomy.BOTaxon)
                mTaxa = Value
            End Set
        End Property
        Private mTaxa() As Taxonomy.BOTaxon

        Public Property HasMessages() As Boolean Implements ITaxonSearchResults.HasMessages
            Get
                If mMessages Is Nothing = False _
                AndAlso mMessages.Length > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal Value As Boolean)
                Value = Value
            End Set
        End Property

        Public Property HasTaxa() As Boolean Implements ITaxonSearchResults.HasTaxa
            Get
                If mTaxa Is Nothing = False _
                AndAlso mTaxa.Length > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal Value As Boolean)
                Value = Value
            End Set
        End Property
    End Class
End Namespace