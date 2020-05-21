Namespace SearchTaxonomy

    <Serializable()> Public Class BOStringCriteria
        Inherits Taxonomy.TaxonomyBaseBO
        Implements IStringCriteria


#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty, but defined for explicitness.
        End Sub

        'Public Sub New(ByVal searchString As String, ByVal kingdomID As Int32, ByVal searchType As SearchableTaxonomyComponentEnum, ByVal restriction As SearchRestrictionEnum) 'NT 25/04/05 - Overload is incorrect.
        Public Sub New(ByVal searchString As String)
            With Me
                .SearchString = searchString
            End With
        End Sub
#End Region

#Region " Properties "
        Public Property SearchString() As String Implements IStringCriteria.SearchString
            Get
                Return mSearchString
            End Get
            Set(ByVal Value As String)
                mSearchString = Value
            End Set
        End Property
        Private mSearchString As String

        Public Property Soundex() As Boolean Implements IStringCriteria.Soundex
            Get
                Return mSoundex
            End Get
            Set(ByVal Value As Boolean)
                mSoundex = Value
            End Set
        End Property
        Private mSoundex As Boolean
#End Region

#Region " Validate "
        Public Shared Function MaxSearchWords(ByVal SearchableTaxonomyComponent As SearchableTaxonomyComponentEnum) As Int32
            Select Case SearchableTaxonomyComponent
                Case SearchableTaxonomyComponentEnum.ClassTaxon
                    Return 1
                Case SearchableTaxonomyComponentEnum.CommonName
                    Return 2
                Case SearchableTaxonomyComponentEnum.FamilyTaxon
                    Return 1
                Case SearchableTaxonomyComponentEnum.GenusTaxon
                    Return 1
                Case SearchableTaxonomyComponentEnum.OrderTaxon
                    Return 1
                Case SearchableTaxonomyComponentEnum.PhylumTaxon
                    Return 1
                Case SearchableTaxonomyComponentEnum.SpeciesTaxon
                    Return 2
                Case SearchableTaxonomyComponentEnum.Usage
                    Return 0
                Case Else
                    Throw New ApplicationException("This SearchableTaxonomyComponent is not catered for in MaxSearchWords")
            End Select
        End Function

        Public Shared Function MinSearchWords(ByVal SearchableTaxonomyComponent As SearchableTaxonomyComponentEnum) As Int32
            Select Case SearchableTaxonomyComponent
                Case SearchableTaxonomyComponentEnum.ClassTaxon
                    Return 0
                Case SearchableTaxonomyComponentEnum.CommonName
                    Return 1
                Case SearchableTaxonomyComponentEnum.FamilyTaxon
                    Return 0
                Case SearchableTaxonomyComponentEnum.GenusTaxon
                    Return 1
                Case SearchableTaxonomyComponentEnum.OrderTaxon
                    Return 0
                Case SearchableTaxonomyComponentEnum.PhylumTaxon
                    Return 0
                Case SearchableTaxonomyComponentEnum.SpeciesTaxon
                    Return 1
                Case SearchableTaxonomyComponentEnum.Usage
                    Return 0
                Case Else
                    Throw New ApplicationException("This SearchableTaxonomyComponent is not catered for in MinSearchWords")
            End Select
        End Function

        Public Overridable Overloads Function Validate(ByVal SearchType As SearchableTaxonomyComponentEnum) As ValidationManager
            Const HigherTaxonMaxWords As Int32 = 1
            Const HigherTaxonMinWords As Int32 = 0
            Const GenusTaxonMaxWords As Int32 = 1
            Const GenusTaxonMinWords As Int32 = 1
            Const OtherMaxWords As Int32 = 2
            Const OtherMinWords As Int32 = 1
            Const ErrTooManyWords As String = "You have supplied too many search words."
            Const ErrTooFewWords As String = "You have not supplied enough search words."

            'Validate the SearchableTaxonomyComponentEnum.
            If System.Enum.IsDefined(GetType(SearchableTaxonomyComponentEnum), SearchType) = False Then
                Throw New ApplicationException("Add the value of " & SearchType.ToString & " to SearchableTaxonomyComponentEnum.")
            Else
                'Validate the search string.
                Dim SearchWords As String()
                SearchWords = Taxonomy.TaxonomySearch.ExtractSearchWords(SearchString)
                Select Case SearchType
                    Case SearchableTaxonomyComponentEnum.PhylumTaxon, _
                    SearchableTaxonomyComponentEnum.ClassTaxon, _
                    SearchableTaxonomyComponentEnum.OrderTaxon, _
                    SearchableTaxonomyComponentEnum.FamilyTaxon
                        Select Case SearchWords.Length
                            Case Is > HigherTaxonMaxWords
                                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.YouHaveSuppliedTooManySearchWords)}, ValidationManager.ValidationTitles.TaxonomySearchString)
                            Case Is < HigherTaxonMinWords
                                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.YouHaveSuppliedTooFewSearchWords)}, ValidationManager.ValidationTitles.TaxonomySearchString)
                        End Select
                    Case SearchableTaxonomyComponentEnum.GenusTaxon
                        Select Case SearchWords.Length
                            Case Is > GenusTaxonMaxWords
                                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.YouHaveSuppliedTooManySearchWords)}, ValidationManager.ValidationTitles.TaxonomySearchString)
                            Case Is < GenusTaxonMinWords
                                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.YouHaveSuppliedTooFewSearchWords)}, ValidationManager.ValidationTitles.TaxonomySearchString)
                        End Select
                    Case SearchableTaxonomyComponentEnum.SpeciesTaxon, _
                    SearchableTaxonomyComponentEnum.CommonName
                        Select Case SearchWords.Length
                            Case Is > OtherMaxWords
                                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.YouHaveSuppliedTooManySearchWords)}, ValidationManager.ValidationTitles.TaxonomySearchString)
                            Case Is < OtherMinWords
                                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.YouHaveSuppliedTooFewSearchWords)}, ValidationManager.ValidationTitles.TaxonomySearchString)
                        End Select
                    Case SearchableTaxonomyComponentEnum.Usage
                        'Do nothing.
                    Case Else
                        Throw New ApplicationException("Add the value of " & SearchType.ToString & " to the verification routine for SearchableTaxonomyComponentEnum.")
                End Select
            End If

            ''Validate the Restriction enum.
            'If System.Enum.IsDefined(GetType(SearchRestrictionEnum), CommonSearchCriteria.SearchRestriction) = False Then
            '    Throw New ApplicationException("Add the value of " & CommonSearchCriteria.SearchRestriction.ToString & " to SearchRestrictionEnum.")
            'End If

            ''Validate the Match enum.
            'If System.Enum.IsDefined(GetType(SearchMatchEnum), CommonSearchCriteria.SearchMatch) = False Then
            '    Throw New ApplicationException("Add the value of " & CommonSearchCriteria.SearchMatch.ToString & " to SearchMatchEnum.")
            'End If

            Return MyBase.ValidationErrors

        End Function
#End Region

    End Class
End Namespace
