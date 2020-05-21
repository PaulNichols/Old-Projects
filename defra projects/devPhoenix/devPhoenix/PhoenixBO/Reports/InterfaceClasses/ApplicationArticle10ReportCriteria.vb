Namespace ReportCriteria
    <Serializable()> _
    Public Class ApplicationArticle10ReportCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property PermitInfoId() As Integer
            Get
                Return mPermitInfoId
            End Get
            Set(ByVal Value As Integer)
                mPermitInfoId = Value
            End Set
        End Property
        Private mPermitInfoId As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.ApplicationArticle10
            End Get
        End Property
    End Class

End Namespace
