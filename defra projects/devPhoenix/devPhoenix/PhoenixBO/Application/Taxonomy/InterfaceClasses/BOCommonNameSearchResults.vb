Namespace Taxonomy
    Public Class BOCommonNameSearchResults
        Implements ICommonNameSearchResults

        Public Property CommonNames() As BOCommonNameResults() Implements ICommonNameSearchResults.CommonNames
            Get
                Return mCommonNames
            End Get
            Set(ByVal Value() As BOCommonNameResults)
                mCommonNames = Value
            End Set
        End Property
        Private mCommonNames() As BOCommonNameResults

        Public Property HasCommonNames() As Boolean Implements ICommonNameSearchResults.HasCommonNames
            Get
                If mCommonNames Is Nothing = False _
                AndAlso mCommonNames.Length > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal Value As Boolean)
                Value = Value
            End Set
        End Property

        Public Property HasMessages() As Boolean Implements ICommonNameSearchResults.HasMessages
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

        Public Property Messages() As String() Implements ICommonNameSearchResults.Messages
            Get
                Return mMessages
            End Get
            Set(ByVal Value() As String)
                mMessages = Value
            End Set
        End Property
        Private mMessages() As String

    End Class
End Namespace