Namespace Application.CITES.Applications
    Public Interface IBOPermitSpecialCondition

        Property PermitSpecialConditionId() As Int32
        Property PermitId() As Int32
        Property BFDate() As Object
        Property BFDateGrid() As String
        Property StatusId() As SpecialConditionStatus
        Property Status() As String
        Property DateApplied() As Date
        Property DateAppliedGrid() As String
        Property SpecialConditionId() As Int32
        Property SpecialCondition() As ReferenceData.BOSpecialCondition
        Property SSOUserId() As Decimal
        Property Condition() As String
        Property Current() As Boolean
        Property PermitSpecialConditionCheckSum() As Int32
        Property Standard() As Boolean
        Property User() As String
        Property StandardString() As String
        Property AddedBySA() As Boolean

    End Interface

    <Serializable()> _
       Public Enum SpecialConditionStatus
        Rcmd_by_SA
        Rcmd_SA_and_App
        Added_By_GWD
    End Enum
End Namespace
