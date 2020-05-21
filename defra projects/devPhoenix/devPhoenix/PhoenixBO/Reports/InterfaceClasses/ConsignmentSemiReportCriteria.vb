
Namespace ReportCriteria
    <Serializable()> _
    Public Class ConsignmentSemiReportCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public Property ApplicationId() As Integer
            Get
                Return mApplicationId
            End Get
            Set(ByVal Value As Integer)
                mApplicationId = Value
            End Set
        End Property
        Private mApplicationId As Int32

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
                Return New RPT.SemiConsignment
            End Get
        End Property
    End Class

End Namespace
