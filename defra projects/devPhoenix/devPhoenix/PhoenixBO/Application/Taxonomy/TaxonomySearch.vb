Imports uk.gov.defra.Phoenix.DO.DataObjects
Imports uk.gov.defra.Phoenix.BO.SearchTaxonomy

Namespace Taxonomy

    Public Enum LegislationTypeEnum
        All = 0
        EUAnnex = 1
        CITESAppendix = 2
    End Enum

    Public Enum KingdomTypeEnum
        Fauna = 0
        Flora = 1
        Fungi = 2
        Bacteria = 3
        Protozoa = 4
    End Enum

    Public Class TaxonomySearch

        Private Sub New()
            'This constructor is intentionally empty, but defined for explicitness.
        End Sub

        Public Shared Function GetMaxHybridConstituentParts() As Int32
            Return 6
        End Function

        Public Shared Function GetSearchableTaxonomyComponents(ByVal SearchRestriction As SearchTaxonomy.SearchRestrictionEnum) As SearchableTaxonomyComponent()
            Dim SearchableTaxonomyComponentsList As New ArrayList
            With SearchableTaxonomyComponentsList
                Select Case SearchRestriction
                    Case SearchTaxonomy.SearchRestrictionEnum.None
                        .Add(New SearchableTaxonomyComponent("Species", SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon))
                        .Add(New SearchableTaxonomyComponent("Genus", SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon))
                        .Add(New SearchableTaxonomyComponent("Family", SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon))
                        .Add(New SearchableTaxonomyComponent("Order", SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon))
                        .Add(New SearchableTaxonomyComponent("Class", SearchTaxonomy.SearchableTaxonomyComponentEnum.ClassTaxon))
                        .Add(New SearchableTaxonomyComponent("Phylum", SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon))
                        .Add(New SearchableTaxonomyComponent("Common Name", SearchTaxonomy.SearchableTaxonomyComponentEnum.CommonName))
                        .Add(New SearchableTaxonomyComponent("Usage", SearchTaxonomy.SearchableTaxonomyComponentEnum.Usage))
                    Case SearchTaxonomy.SearchRestrictionEnum.AvesClass, _
                    SearchTaxonomy.SearchRestrictionEnum.Schedule4
                        .Add(New SearchableTaxonomyComponent("Species", SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon))
                        .Add(New SearchableTaxonomyComponent("Genus", SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon))
                        .Add(New SearchableTaxonomyComponent("Family", SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon))
                        .Add(New SearchableTaxonomyComponent("Order", SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon))
                        .Add(New SearchableTaxonomyComponent("Common Name", SearchTaxonomy.SearchableTaxonomyComponentEnum.CommonName))
                End Select
            End With
            Dim SearchableTaxonomyComponentsArray(SearchableTaxonomyComponentsList.Count - 1) As SearchableTaxonomyComponent
            SearchableTaxonomyComponentsList.CopyTo(SearchableTaxonomyComponentsArray)
            Return SearchableTaxonomyComponentsArray
        End Function

        Public Shared Function GetFungiKingdom() As Taxonomy.BOTaxon

            Dim Result As Entity.TaxonomyTaxon
            Result = DataObjects.Service.TaxonomyTaxonService.GetFungiKingdom
            If Result Is Nothing = False Then
                Return New BOTaxon(Result.Id)
            Else
                Return Nothing
            End If
        End Function

        Public Shared Function GetBacteriaKingdom() As Taxonomy.BOTaxon

            Dim Result As Entity.TaxonomyTaxon
            Result = DataObjects.Service.TaxonomyTaxonService.GetBacteriaKingdom
            If Result Is Nothing = False Then
                Return New BOTaxon(Result.Id)
            Else
                Return Nothing
            End If
        End Function

        Public Shared Function GetProtozoaKingdom() As Taxonomy.BOTaxon

            Dim Result As Entity.TaxonomyTaxon
            Result = DataObjects.Service.TaxonomyTaxonService.GetProtozoaKingdom
            If Result Is Nothing = False Then
                Return New BOTaxon(Result.Id)
            Else
                Return Nothing
            End If
        End Function

        Public Shared Function GetAnimalKingdom() As Taxonomy.BOTaxon

            Dim Result As Entity.TaxonomyTaxon
            Result = DataObjects.Service.TaxonomyTaxonService.GetAnimalKingdom
            If Result Is Nothing = False Then
                Return New BOTaxon(Result.Id)
            Else
                Return Nothing
            End If
        End Function

        Public Shared Function GetPlantKingdom() As Taxonomy.BOTaxon

            Dim Result As Entity.TaxonomyTaxon
            Result = DataObjects.Service.TaxonomyTaxonService.GetPlantKingdom
            If Result Is Nothing = False Then
                Return New BOTaxon(Result.Id)
            Else
                Return Nothing
            End If
        End Function

        Public Shared Function GetAllKingdom() As Taxonomy.BOTaxon()

            Dim Kingdoms As New ArrayList
            With Kingdoms
                .Add(GetAnimalKingdom)
                .Add(GetPlantKingdom)
                .Add(GetBacteriaKingdom)
                .Add(GetFungiKingdom)
                .Add(GetProtozoaKingdom)
            End With
            If Kingdoms.Count > 0 Then
                Dim KingdomArray(Kingdoms.Count - 1) As Taxonomy.BOTaxon
                Kingdoms.CopyTo(KingdomArray)
                Return KingdomArray
            Else
                Return Nothing
            End If
        End Function

        Friend Shared Function ExtractSearchWords(ByVal SearchString As String) As String()
            Const SingleSpace As String = " "
            Const DoubleSpace As String = "  "
            If SearchString Is Nothing = True Then
                SearchString = "*"
            End If
            Dim InternalSearchString As String = SearchString.Trim
            'Remove any double spaces.
            Do Until InternalSearchString.IndexOf(DoubleSpace) <= 0
                InternalSearchString.Replace(DoubleSpace, "")
            Loop
            Dim SearchWords As String() = InternalSearchString.Split(SingleSpace.ToCharArray)
            Return SearchWords
        End Function

        Private Shared Function GetKingdomID(ByVal SearchKingdomType As SearchKingdomTypeEnum) As Int32
            Select Case SearchKingdomType
                Case SearchKingdomTypeEnum.Plant
                    Return Taxonomy.TaxonomySearch.GetPlantKingdom.Id
                Case SearchKingdomTypeEnum.Animal
                    Return Taxonomy.TaxonomySearch.GetAnimalKingdom.Id
                Case SearchKingdomTypeEnum.Bacteria
                    Return Taxonomy.TaxonomySearch.GetBacteriaKingdom.Id
                Case SearchKingdomTypeEnum.Fungi
                    Return Taxonomy.TaxonomySearch.GetFungiKingdom.Id
                Case SearchKingdomTypeEnum.Protozoa
                    Return Taxonomy.TaxonomySearch.GetProtozoaKingdom.Id
                Case SearchKingdomTypeEnum.Any
                    Return 0
            End Select

        End Function

        Public Shared Function SearchUsage(ByVal CommonSearchCriteria As BOCommonCriteria, ByVal UsageCriteria As SearchTaxonomy.BOUsageCriteria) As BOTaxonSearchResults
            Dim Results As Views.Collection.SearchTaxonomyTaxonBoundCollection
            With CommonSearchCriteria
                Select Case .SearchForRestriction
                    Case SearchTaxonomy.SearchRestrictionEnum.None
                        Results = Views.Service.SearchTaxonomyTaxonService.GetTaxaByUsage(GetKingdomID(.SearchForKingdomType), UsageCriteria.LevelOfUseID, UsageCriteria.PartID, UsageCriteria.UsageTypeID)
                    Case SearchTaxonomy.SearchRestrictionEnum.Schedule4
                        Results = Views.Service.SearchTaxonomyTaxonService.GetTaxaByUsageSched4(GetKingdomID(.SearchForKingdomType), UsageCriteria.LevelOfUseID, UsageCriteria.PartID, UsageCriteria.UsageTypeID)
                    Case SearchRestrictionEnum.AvesClass
                        Results = Views.Service.SearchTaxonomyTaxonService.GetTaxaByUsageAvesClass(GetKingdomID(.SearchForKingdomType), UsageCriteria.LevelOfUseID, UsageCriteria.PartID, UsageCriteria.UsageTypeID)
                    Case Else
                        Results = Views.Service.SearchTaxonomyTaxonService.GetTaxaByUsage(GetKingdomID(.SearchForKingdomType), UsageCriteria.LevelOfUseID, UsageCriteria.PartID, UsageCriteria.UsageTypeID)
                End Select
            End With
            Dim ResultsPackage As New BOTaxonSearchResults
            If Results Is Nothing = False Then
                'Check that not too many results have been returned.
                If Results.Count <= 4000 Then
                    Dim BOTaxonArray(Results.Count - 1) As BOTaxon
                    For FoundTaxonIdx As Integer = 0 To Results.Count - 1
                        Dim FoundTaxon As Views.Entity.SearchTaxonomyTaxon = Results(FoundTaxonIdx)
                        Dim NewTaxon As New BOTaxon(FoundTaxon.Id)
                        BOTaxonArray(FoundTaxonIdx) = NewTaxon
                    Next
                    ResultsPackage.Taxa = BOTaxonArray
                Else
                    Dim Message(0) As String
                    Message(0) = "There are too many results. Please refine your search."
                    ResultsPackage.Messages = Message
                End If
            End If
            Return ResultsPackage
        End Function

        Public Shared Function SearchCommonNames(ByVal CommonSearchCriteria As BOCommonCriteria, ByVal StringSearchCriteria As SearchTaxonomy.BOStringCriteria) As BOCommonNameSearchResults

            Try
                StringSearchCriteria.Validate(CommonSearchCriteria.SearchForComponentType)
            Catch ex As Exception
                Throw New ApplicationException("There is a problem with the search criteria. Use ValidateStringSearchCriteria to get more details.", ex)
            End Try

            Dim Results As Collection.TaxonomyCommonNameBoundCollection
            'Extract the 2 search words from the SearchString.
            Dim SearchWords As String()
            With StringSearchCriteria
                SearchWords = ExtractSearchWords(.SearchString)
                Dim SearchWord1 As String = Nothing
                Dim SearchWord2 As String = Nothing
                If SearchWords(0) Is Nothing = False Then
                    SearchWord1 = SearchWords(0)
                End If
                If SearchWords.Length > 1 _
                AndAlso SearchWords(1) Is Nothing = False Then
                    SearchWord2 = SearchWords(1)
                End If
                If SearchWords.Length <= 2 Then
                    Select Case CommonSearchCriteria.SearchForRestriction
                        Case SearchTaxonomy.SearchRestrictionEnum.None
                            Results = Service.TaxonomyCommonNameService.GetCommonNamesByInfo(kingdomid:=GetKingdomID(CommonSearchCriteria.SearchForKingdomType), Searchstring1:=SearchWord1, searchstring2:=SearchWord2, soundex:=.Soundex)
                        Case SearchTaxonomy.SearchRestrictionEnum.Schedule4
                            Results = Service.TaxonomyCommonNameService.GetSched4CommonNamesByInfo(kingdomid:=GetKingdomID(CommonSearchCriteria.SearchForKingdomType), searchstring1:=SearchWord1, searchstring2:=SearchWord2, soundex:=.Soundex)
                        Case SearchRestrictionEnum.AvesClass
                            Results = Service.TaxonomyCommonNameService.GetAvesClassCommonNamesByInfo(kingdomid:=GetKingdomID(CommonSearchCriteria.SearchForKingdomType), searchstring1:=SearchWord1, searchstring2:=SearchWord2, soundex:=.Soundex)
                        Case Else
                            Results = Service.TaxonomyCommonNameService.GetCommonNamesByInfo(kingdomid:=GetKingdomID(CommonSearchCriteria.SearchForKingdomType), searchstring1:=SearchWord1, searchstring2:=SearchWord2, soundex:=.Soundex)
                    End Select
                Else
                    Throw New ArgumentException("Only one or two search words are allowed.")
                End If
            End With


            Dim ResultsPackage As New BOCommonNameSearchResults
            If Results Is Nothing = False Then
                'Check that not too many results have been returned.
                If Results.Count <= 4000 Then
                    Dim BOCommonNameArray(Results.Count - 1) As BOCommonNameResults
                    For FoundCommonNameIdx As Integer = 0 To Results.Count - 1
                        Dim FoundCommonName As Entity.TaxonomyCommonName = Results(FoundCommonNameIdx)
                        Dim NewCommonName As New BOCommonNameResults(FoundCommonName.SourceTable, FoundCommonName.CommonNameID)
                        BOCommonNameArray(FoundCommonNameIdx) = NewCommonName
                    Next
                    ResultsPackage.CommonNames = BOCommonNameArray
                Else
                    Dim Message(0) As String
                    Message(0) = "There are too many results. Please refine your search."
                    ResultsPackage.Messages = Message
                End If
            End If
            Return ResultsPackage
        End Function

        Public Shared Function GetTaxon(ByVal KingdomID As Int32, ByVal TaxonId As Int32, ByVal TaxonTypeID As Int32) As BOTaxon
            Return New BOTaxon(KingdomID, TaxonId, TaxonTypeID)
        End Function

        Public Shared Function SearchTaxa(ByVal CommonSearchCriteria As BOCommonCriteria, ByVal StringSearchCriteria As SearchTaxonomy.BOStringCriteria) As BOTaxonSearchResults
            Try
                StringSearchCriteria.Validate(CommonSearchCriteria.SearchForComponentType)
            Catch ex As Exception
                Throw New ApplicationException("There is a problem with the search criteria. Use ValidateStringSearchCriteria to get more details.", ex)
            End Try

            Dim Results As Views.Collection.SearchTaxonomyTaxonBoundCollection
            With StringSearchCriteria
                Dim DataFunction As Views.Service.SearchTaxonomyTaxonService.GetTaxaByInfoDelegate
                Select Case CommonSearchCriteria.SearchForRestriction
                    Case SearchRestrictionEnum.None
                        Select Case CommonSearchCriteria.SearchForComponentType
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetSpeciesTaxaByInfo
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetGenusTaxaByInfo
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetFamilyTaxaByInfo
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetOrderTaxaByInfo
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.ClassTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetClassTaxaByInfo
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetPhylumTaxaByInfo
                        End Select
                    Case SearchRestrictionEnum.Schedule4
                        Select Case CommonSearchCriteria.SearchForComponentType
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetShed4SpeciesTaxaByInfo
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetShed4GenusTaxaByInfo
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetShed4FamilyTaxaByInfo
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetShed4OrderTaxaByInfo
                        End Select
                    Case SearchRestrictionEnum.AvesClass
                        Select Case CommonSearchCriteria.SearchForComponentType
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetAvesClassSpeciesTaxaByInfo
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetAvesClassGenusTaxaByInfo
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetAvesClassFamilyTaxaByInfo
                            Case SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon
                                DataFunction = AddressOf Views.Service.SearchTaxonomyTaxonService.GetAvesClassOrderTaxaByInfo
                        End Select
                End Select
                'Extract the 2 search words from the SearchString.
                Dim SearchWords As String()
                SearchWords = ExtractSearchWords(.SearchString)
                Dim SearchWord1 As String = Nothing
                Dim SearchWord2 As String = Nothing
                If SearchWords(0) Is Nothing = False _
                AndAlso SearchWords(0).Length > 0 Then
                    SearchWord1 = SearchWords(0)
                Else
                    SearchWord1 = "*"
                End If
                If SearchWords.Length > 1 _
                AndAlso SearchWords(1) Is Nothing = False Then
                    SearchWord2 = SearchWords(1)
                End If
                If SearchWords.Length <= 2 Then
                    Results = Views.Service.SearchTaxonomyTaxonService.GetTaxaByInfo(DataFunction, GetKingdomID(CommonSearchCriteria.SearchForKingdomType), SearchWord1, SearchWord2, .Soundex)
                Else
                    Throw New ArgumentException("Only one or two search words are allowed.")
                End If
            End With

            Dim ResultsPackage As New BOTaxonSearchResults
            If Results Is Nothing = False Then
                'Check that not too many results have been returned.
                If Results.Count <= 4000 Then
                    Dim BOTaxonArray(Results.Count - 1) As BOTaxon
                    For FoundTaxonIdx As Integer = 0 To Results.Count - 1
                        Dim FoundTaxon As Views.Entity.SearchTaxonomyTaxon = Results(FoundTaxonIdx)
                        Dim NewTaxon As New BOTaxon(FoundTaxon.Id)
                        BOTaxonArray(FoundTaxonIdx) = NewTaxon
                    Next
                    ResultsPackage.Taxa = BOTaxonArray
                Else
                    Dim Message(0) As String
                    Message(0) = "There are too many results. Please refine your search."
                    ResultsPackage.Messages = Message
                End If
            End If
            Return ResultsPackage
        End Function

        Public Shared Function ConvertIDToSearchKingdomTypeEnum(ByVal ID As Int32) As SearchKingdomTypeEnum
            Throw New Exception("This function should no longer be used.") 'TODO - delete this function.
            Select Case ID
                Case GetPlantKingdom.Id
                    Return SearchKingdomTypeEnum.Plant
                Case GetAnimalKingdom.Id
                    Return SearchKingdomTypeEnum.Animal
                Case GetBacteriaKingdom.Id
                    Return SearchKingdomTypeEnum.Bacteria
                Case GetProtozoaKingdom.Id
                    Return SearchKingdomTypeEnum.Protozoa
                Case GetFungiKingdom.Id
                    Return SearchKingdomTypeEnum.Fungi
            End Select
        End Function

        Public Shared Function GetCITESLegislationPermittedValues() As String()
            Dim DOPermittedValues As [DO].DataObjects.Views.EntitySet.TaxonomyPermittedListingValuesCITESSet = [DO].DataObjects.Views.Entity.TaxonomyPermittedListingValuesCITES.GetAll()
            If DOPermittedValues Is Nothing = False _
            AndAlso DOPermittedValues.Count > 0 Then
                Dim PermittedValuesArray(DOPermittedValues.Count - 1) As String
                For DOPermittedValuesIdx As Int32 = 0 To DOPermittedValues.Count - 1
                    PermittedValuesArray(DOPermittedValuesIdx) = DOPermittedValues.Entities(DOPermittedValuesIdx).ListingValue
                Next
                Return PermittedValuesArray
            Else
                Return Nothing
            End If
        End Function

        Public Shared Function GetEULegislationPermittedValues() As String()
            Dim DOPermittedValues As [DO].DataObjects.Views.EntitySet.TaxonomyPermittedListingValuesEUSet = _
                            [DO].DataObjects.Views.Entity.TaxonomyPermittedListingValuesEU.GetAll
            If DOPermittedValues Is Nothing = False _
            AndAlso DOPermittedValues.Count > 0 Then
                Dim PermittedValuesArray(DOPermittedValues.Count - 1) As String
                For DOPermittedValuesIdx As Int32 = 0 To DOPermittedValues.Count - 1
                    PermittedValuesArray(DOPermittedValuesIdx) = DOPermittedValues.Entities(DOPermittedValuesIdx).ListingValue
                Next
                Return PermittedValuesArray
            Else
                Return Nothing
            End If
        End Function
    End Class
End Namespace

