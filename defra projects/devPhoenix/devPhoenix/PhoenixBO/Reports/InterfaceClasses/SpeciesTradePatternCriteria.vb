
Namespace ReportCriteria
    <Serializable()> _
    Public Class SpeciesTradePatternCriteria
        Inherits ReportCriteria

        Public Sub New()
            MyBase.new()
        End Sub


        Public FromDate As Date
        Public ToDate As Date
        Public KingdomID As Int32
        Public TaxonId As Int32
        Public TaxonTypeID As Int32
        Public FamilyDesciption As String
        Public GenusDesciption As String
        Public SpeciesDesciption As String
        Public DerivativeId As Int32
        Public SourceId As Int32
        Public PurposeId As Int32
        Public CountryOfOriginId As Int32

        Friend Overrides ReadOnly Property Report() As RPT.BOReport
            Get
                Return New RPT.SpeciesTradePattern
            End Get
        End Property
    End Class

End Namespace