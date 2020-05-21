Namespace Taxonomy
    <Serializable()> _
    Public Class BOConservationSummary

        Implements IConservationSummary

        Public Sub New()

        End Sub

        Public Property HasLegislation() As String Implements IConservationSummary.HasLegislation
            Get
                Return mHasLegislation
            End Get
            Set(ByVal Value As String)
                mHasLegislation = Value
            End Set
        End Property
        Private mHasLegislation As String

        Public Property HasDecisions() As String Implements IConservationSummary.HasDecisions
            Get
                Return mHasDecisions
            End Get
            Set(ByVal Value As String)
                mHasDecisions = Value
            End Set
        End Property
        Private mHasDecisions As String

        Public Property HasExportQuotas() As String Implements IConservationSummary.HasExportQuotas
            Get
                Return mHasExportQuotas
            End Get
            Set(ByVal Value As String)
                mHasExportQuotas = Value
            End Set
        End Property
        Private mHasExportQuotas As String
    End Class
End Namespace
