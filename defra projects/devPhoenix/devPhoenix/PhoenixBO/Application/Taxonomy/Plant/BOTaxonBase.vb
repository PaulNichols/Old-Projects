Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Taxonomy.Plant
    <Serializable()> _
    Public MustInherit Class BOTaxonBase

#Region " Prelim code "
        Private mTaxon As Taxonomy.BOTaxon

        Sub New()
            mTaxon = New Taxonomy.BOTaxon
        End Sub

        Sub New(ByVal taxon As Taxonomy.BOTaxon)
            mTaxon = taxon
            FillHigherTaxa()
            FillLowerTaxa()
        End Sub

        Protected Sub FillHigherTaxa()
            Dim taxa() As Taxonomy.BOTaxon = mTaxon.GetHigherTaxa()
            Dim results(taxa.Length) As BOTaxonSummary
            Dim i As Int32 = 1

            results(0) = New BOTaxonSummary(Taxonomy.TaxonomySearch.GetPlantKingdom())
            For Each taxon As Taxonomy.BOTaxon In taxa
                results(i) = New BOTaxonSummary(taxon)
                i += 1
            Next
            mHigherTaxa = results
        End Sub

        Protected Sub FillLowerTaxa()
            Dim taxa() As Taxonomy.BOTaxon = mTaxon.GetLowerTaxa(1)
            Dim results(taxa.Length - 1) As BOTaxonSummary
            Dim i As Int32 = 0

            For Each taxon As Taxonomy.BOTaxon In taxa
                results(i) = New BOTaxonSummary(taxon)
                i += 1
            Next
            Array.Sort(results, New TaxonSummaryComparer)
            mLowerTaxa = results
        End Sub

#End Region

#Region " Properties "

        Public Property HigherTaxa As BOTaxonSummary()
            Get
                Return mHigherTaxa
            End Get
            Set
                mHigherTaxa = Value
            End Set
        End Property
        Private mHigherTaxa As BOTaxonSummary()

        Public Property LowerTaxa As BOTaxonSummary()
            Get
                Return mLowerTaxa
            End Get
            Set
                mLowerTaxa = Value
            End Set
        End Property
        Private mLowerTaxa As BOTaxonSummary()

        Public Property Id As Int32
            Get
                Return mTaxon.Id
            End Get
            Set
                mTaxon.Id = Value
            End Set
        End Property

        Public Property CheckSum As Int32
            Get
                Return mTaxon.CheckSum
            End Get
            Set
                mTaxon.CheckSum = Value
            End Set
        End Property

        Public Property TaxonId As BOTaxonId
            Get
                Return New BOTaxonId(mTaxon)
            End Get
            Set
                mTaxon.KingdomID   = Value.KingdomId
                mTaxon.TaxonId     = Value.TaxonId
                mTaxon.TaxonTypeID = value.TaxonTypeId
            End Set
        End Property

        Public Property ParentTaxonId As BOTaxonId
            Get
                Dim taxon As New BOTaxonId
                taxon.KingdomID   = mTaxon.ParentKingdomId
                taxon.TaxonId     = mTaxon.ParentTaxonId
                taxon.TaxonTypeID = mTaxon.ParentTaxonTypeId
                Return taxon
            End Get
            Set
                mTaxon.ParentKingdomId   = Value.KingdomId
                mTaxon.ParentTaxonId     = Value.TaxonId
                mTaxon.ParentTaxonTypeId = value.TaxonTypeId
            End Set
        End Property

        Public Property Name As String
            Get
                Return mTaxon.TaxonNameUnformatted
            End Get
            Set
                mTaxon.TaxonNameUnformatted = Value
            End Set
        End Property

        Public Property Type As String
            Get
                Return mTaxon.TaxonTypeDescription
            End Get
            Set
            End Set
        End Property

        Public Property LongName As String
            Get
                Return mTaxon.LongScientificNameHTMLFormatted
            End Get
            Set
            End Set
        End Property

        Public Property Author As String
            Get
                Return mTaxon.TaxonAuthorUnformatted
            End Get
            Set
                mTaxon.TaxonAuthorUnformatted = Value
            End Set
        End Property

        Public Property CitesRef As String
            Get
                Return mTaxon.CITESReference
            End Get
            Set
                mTaxon.CITESReference = Value
            End Set
        End Property

        Public Property EpithetType As String
            Get
                Return mTaxon.TaxonEpithetUnformatted
            End Get
            Set
                mTaxon.TaxonEpithetUnformatted = Value
            End Set
        End Property

        Public Property TaxonStatus As String
            Get
                Return mTaxon.TaxonStatusDescription
            End Get
            Set
                Dim obj as Object = System.Enum.Parse(GetType(Taxonomy.TaxonStatusEnum), Value)
                mTaxon.TaxonStatusId = CType(obj, Taxonomy.TaxonStatusEnum)
            End Set
        End Property

        Public Property DistributionComplete As String
            Get
                Return mTaxon.DistributionComplete
            End Get
            Set
                mTaxon.DistributionComplete = Value
            End Set
        End Property

        Public Property IsDeletable As Boolean
            Get
                Return mIsDeletable
            End Get
            Set
                mIsDeletable = Value
            End Set
        End Property
        Private mIsDeletable As Boolean

        Public Property DisplayType As String
            Get
                Return mDisplayType
            End Get
            Set
                mDisplayType = Value
            End Set
        End Property
        Private mDisplayType As String

        Public Overridable Property AuthorAllowed As Boolean
            Get
                Return False
            End Get
            Set
            End Set
        End Property

        Public Overridable Property DistributionAllowed As Boolean
            Get
                Return False
            End Get
            Set
            End Set
        End Property

        Public Overridable Property LegislationAllowed As Boolean
            Get
                Return False
            End Get
            Set
            End Set
        End Property

        Public Overridable Property DecisionAllowed As Boolean
            Get
                Return False
            End Get
            Set
            End Set
        End Property

        Public Overridable Property CommonNameAllowed As Boolean
            Get
                Return False
            End Get
            Set
            End Set
        End Property

        Public Overridable Property SynonymAllowed As Boolean
            Get
                Return False
            End Get
            Set
            End Set
        End Property

#End Region

#Region " Save "
        Public Sub Save()
            Dim service As New Service.TaxonomyTaxonService
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction
            Save(tran)
            Service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
        End Sub

        Public Sub Delete()
            Dim service As New Service.TaxonomyTaxonService
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction
            Entity.TaxonomyTaxon.DeleteById(Id, CheckSum, tran)
            Service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
        End Sub

        Public MustOverride Sub Save(ByVal tran As SqlClient.SqlTransaction)
        
        Protected Sub Save(ByVal tran As SqlClient.SqlTransaction, ByVal taxonType As Taxonomy.TaxonTypeEnum)
            Dim taxon As New Entity.TaxonomyTaxon
            Dim service As service.TaxonomyTaxonService = taxon.ServiceObject
            Dim created As Boolean = mTaxon.Id = 0

            With mTaxon
                If created Then
                    taxon = service.Insert(.KingdomID, _
                                        .TaxonId, _
                                        CType(taxonType, Int32), _
                                        .TaxonEpithetUnformatted, _
                                        .TaxonNameUnformatted, _
                                        .TaxonAuthorUnformatted, _
                                        .TaxonStatusID, _
                                        .ParentKingdomID, _
                                        .ParentTaxonID, _
                                        .ParentTaxonTypeID, _
                                        .DistributionComplete, _
                                        .CITESReference, _
                                        0, _
                                        tran)
                Else
                    taxon = service.Update(.Id, _
                                        .KingdomID, _
                                        .TaxonId, _
                                        .TaxonTypeID, _
                                        .TaxonEpithetUnformatted, _
                                        .TaxonNameUnformatted, _
                                        .TaxonAuthorUnformatted, _
                                        .TaxonStatusID, _
                                        .ParentKingdomID, _
                                        .ParentTaxonID, _
                                        .ParentTaxonTypeID, _
                                        .DistributionComplete, _
                                        .CITESReference, _
                                        0, _
                                        .CheckSum, _
                                        tran)
                End If
            End With
            If taxon Is Nothing Then
                If Not tran Is Nothing Then
                    Service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                End If
                If Sprocs.LastError Is Nothing Then
                    Throw New Exception("Cannot save Taxon: reason unknown")
                Else
                    Throw New Exception("Cannot save Taxon: " + Sprocs.LastError.ErrorMessage)
                End If
            Else
                mTaxon.Id       = taxon.Id
                mTaxon.TaxonId  = taxon.TaxonID
                mTaxon.CheckSum = taxon.CheckSum
            End If
        End Sub

        Public Sub AttachTransferees(ByVal transferees As BOTaxonSummary())
            Dim service As New Service.TaxonomyTaxonService
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction
            For Each transferee As BOTaxonSummary In transferees
                Dim transferTaxon As New BOTaxon(transferee.KingdomId, transferee.TaxonId, transferee.TaxonTypeId)
                Dim transferBase  As BOTaxonBase = BOTaxonFactory.PolymorphicCreate(transferTaxon)
                transferBase.ParentTaxonId = TaxonId
                transferBase.Save(tran)
            Next
            Service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
        End Sub

        Public MustOverride Function CreateChild() As BOTaxonBase

        Protected Function FillChild(ByVal child As BOTaxonBase) As BOTaxonBase
            Dim empty(-1) As BOTaxonSummary
            child.ParentTaxonId  = TaxonId
            mTaxon.TaxonId       = 0
            mTaxon.TaxonTypeId  += 1        
            child.TaxonId        = TaxonId      'contents altered by previous 2 lines
            child.TaxonStatus    = "Accepted"
            child.HigherTaxa     = ExtendTaxa(HigherTaxa)
            child.LowerTaxa      = empty
            Return child
        End Function

        Private Function ExtendTaxa(ByVal taxa As BOTaxonSummary()) As BOTaxonSummary()
            Dim taxaCount As Int32 = taxa.Length
            Redim Preserve taxa(taxaCount)
            taxa(taxaCount)      = New BOTaxonSummary
            taxa(taxaCount).Name = ""
            taxa(taxaCount).Type = ""
            Return taxa
        End Function

#End Region

        Private Class TaxonSummaryComparer
            Implements IComparer

            Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim xtaxon As BOTaxonSummary = CType(x, BOTaxonSummary)
                Dim ytaxon As BOTaxonSummary = CType(y, BOTaxonSummary)
                Return String.Compare(xtaxon.LongName, ytaxon.LongName)
            End Function
        End Class
    End Class
End Namespace