Namespace Taxonomy
    Public Interface ILegislationName
        Property LegislationNameId As Int32
        Property LegislationShortName As String
        Property LegislationLongName As String
        Property LegislationLevel As Char
        Property LegislationDateAdopted As Date
        Property LegislationDateEnforced As Date
        Property LegislationURL As String
        Property LegislationNameStatus As Char
        Property Note As String
    End Interface
End Namespace