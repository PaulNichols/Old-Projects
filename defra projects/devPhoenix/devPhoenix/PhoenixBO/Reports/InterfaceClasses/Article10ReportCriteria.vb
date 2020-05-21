Namespace ReportCriteria
    <Serializable()> _
    Public Class Article10ReportCriteria
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

        Public Property Duplicate() As Boolean
            Get
                Return mDuplicate
            End Get
            Set(ByVal Value As Boolean)
                mDuplicate = Value
            End Set
        End Property
        Private mDuplicate As Boolean

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.Article10
            End Get
        End Property
    End Class

End Namespace
