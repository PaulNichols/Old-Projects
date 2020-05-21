
Namespace ReportCriteria
    <Serializable()> _
    Public Class SpeciesCaseTypesReportCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub

        Public FromMonth As Int32
        Public FromYear As Int32

        Public ToMonth As Int32
        Public ToYear As Int32

        Public ApplicationTypeId As Int32
        Public PermitStatusId As Int32
        Public SourceId As Int32
        Public PurposeId As Int32
        Public SpeciesId As Int32 ' Use Taxon Search
        Public CountryOfOriginId As Int32
        Public CountryOfExportId As Int32
        Public DestinationCountryId As Int32
        Public PartyId As Int32 ' Applicant Id - Use Party Search

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.SpeciesCaseTypes
            End Get
        End Property
    End Class

End Namespace