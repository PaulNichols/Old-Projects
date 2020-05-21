Namespace SearchTaxonomy
    Public Class BOCommonCriteria
        Inherits Taxonomy.TaxonomyBaseBO
        Implements ICommonCriteria


#Region " Prelim Code "
        Public Sub New()
            MyClass.New(SearchableTaxonomyComponentEnum.SpeciesTaxon, SearchRestrictionEnum.None)
        End Sub

        Public Sub New(ByVal NewSearchType As SearchableTaxonomyComponentEnum, ByVal NewRestriction As SearchRestrictionEnum)
            With Me
                .SearchForRestriction = NewRestriction
                .SearchForComponentType = NewSearchType
            End With
        End Sub
#End Region

#Region " Properties "

        Public Property SearchDirect() As Boolean
            Get
                Return mDirectSearct
            End Get
            Set(ByVal Value As Boolean)
                mDirectSearct = Value
            End Set
        End Property
        Private mDirectSearct As Boolean

        Public Property SearchForComponentType() As SearchableTaxonomyComponentEnum Implements ICommonCriteria.SearchType
            Get
                Return mSearchType
            End Get
            Set(ByVal Value As SearchableTaxonomyComponentEnum)
                mSearchType = Value
            End Set
        End Property
        Private mSearchType As SearchableTaxonomyComponentEnum

        Public Property SearchForRestriction() As SearchRestrictionEnum Implements ICommonCriteria.SearchRestriction
            Get
                Return mRestriction
            End Get
            Set(ByVal Value As SearchRestrictionEnum)
                mRestriction = Value
            End Set
        End Property
        Private mRestriction As SearchRestrictionEnum

        Public Property PickedObjectType() As PickedObjectTypeEnum Implements ICommonCriteria.SearchResultObject
            Get
                Return mSearchResultObject
            End Get
            Set(ByVal Value As PickedObjectTypeEnum)
                mSearchResultObject = Value
            End Set
        End Property
        Private mSearchResultObject As PickedObjectTypeEnum

        Public Property PickedObjectStatus() As Int32 Implements ICommonCriteria.SearchResultStatus
            Get
                Return mSearchResultStatus
            End Get
            Set(ByVal Value As Integer)
                mSearchResultStatus = Value
            End Set
        End Property
        Private mSearchResultStatus As Int32

        Public Property PickedObjectTaxonType() As Int32 Implements ICommonCriteria.SearchResultType
            Get
                Return mSearchResultType
            End Get
            Set(ByVal Value As Integer)
                mSearchResultType = Value
            End Set
        End Property
        Private mSearchResultType As Int32

        Public Property SearchDisplayCriteriaDisabled() As Int32 Implements ICommonCriteria.SearchCriteriaDisabled
            Get
                Return mSearchCriteriaDisabled
            End Get
            Set(ByVal Value As Integer)
                mSearchCriteriaDisabled = Value
            End Set
        End Property
        Private mSearchCriteriaDisabled As Int32

        Public Property SearchForKingdomType() As SearchKingdomTypeEnum Implements ICommonCriteria.SearchKingdomType
            Get
                Return mSearchKingdomType
            End Get
            Set(ByVal Value As SearchKingdomTypeEnum)
                mSearchKingdomType = Value
            End Set
        End Property
        Private mSearchKingdomType As SearchKingdomTypeEnum

        Public Property SearchForAmount() As SearchForAmountEnum Implements ICommonCriteria.SearchForAmount
            Get
                Return mSearchForAmount
            End Get
            Set(ByVal Value As SearchForAmountEnum)
                mSearchForAmount = Value
            End Set
        End Property
        Private mSearchForAmount As SearchForAmountEnum

#End Region


    End Class
End Namespace