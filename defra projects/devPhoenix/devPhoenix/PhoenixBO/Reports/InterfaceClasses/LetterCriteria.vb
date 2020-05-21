Namespace ReportCriteria
    <Serializable()> _
    Public MustInherit Class LetterCriteria
        Inherits ReportCriteria         'MLD added 1/2/5

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property SsoUserId() As Long
            Get
                Return mSsoUserId
            End Get
            Set(ByVal Value As Long)
                mSsoUserId = Value
            End Set
        End Property
        Private mSsoUserId As Long
    End Class
End Namespace
