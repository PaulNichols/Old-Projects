
Namespace ReportCriteria
    <Serializable()> _
    Public MustInherit Class PermitBaseCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property Duplicate() As Boolean
            Get
                Return mDuplicate
            End Get
            Set(ByVal Value As Boolean)
                mDuplicate = Value
            End Set
        End Property
        Private mDuplicate As Boolean
    End Class

End Namespace
