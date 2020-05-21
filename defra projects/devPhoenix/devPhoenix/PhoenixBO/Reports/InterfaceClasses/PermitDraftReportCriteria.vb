Namespace ReportCriteria
    <Serializable()> _
    Public Class PermitDraftReportCriteria
        Inherits ReportCriteria
        Implements IPermitReportCriteria


        Public Sub New()
            MyBase.new()
        End Sub

        Public Property PermitInfoId() As Integer Implements IPermitReportCriteria.PermitInfoId
            Get
                Return mPermitInfoId
            End Get
            Set(ByVal Value As Integer)
                mPermitInfoId = Value
            End Set
        End Property
        Private mPermitInfoId As Int32

        Public Property Duplicate() As Boolean Implements IPermitReportCriteria.Duplicate
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
                Return New RPT.DraftPermit
            End Get
        End Property
    End Class

End Namespace
