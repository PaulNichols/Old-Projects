Namespace ReportCriteria
    <Serializable()> _
    Public MustInherit Class ReportCriteria
        Implements IReportCriteria

        Public Sub New()
            MyBase.New()
        End Sub

        Public Property Description() As String
            Get
                Return mDescription
            End Get
            Set(ByVal Value As String)
                mDescription = Value
            End Set
        End Property
        Private mDescription As String

        Friend MustOverride ReadOnly Property Report() As RPT.BOReport
    End Class

End Namespace
