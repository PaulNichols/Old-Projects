Namespace SearchTaxonomy
    <ServiceMapping(GetType(DataObjects.Service.TaxonomyLevelOfUseService)), _
        EntityMapping(GetType(DataObjects.Entity.TaxonomyLevelOfUse)), _
        CollectionMapping(GetType(DataObjects.Collection.TaxonomyLevelOfUseBoundCollection)), _
        Serializable()> _
    Public Class BOLevelOfUse
        Inherits BaseDataBO
        Implements ILevelOfUse

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal id As Int32)
            MyBase.New(id, Nothing)
        End Sub

        Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyBase.New(id, tran)
        End Sub

        <DOtoBOMapping("LevelOfUseDescription")> _
        Property Description() As String Implements ILevelOfUse.description
            Get
                Description = mDescription
            End Get
            Set(ByVal Value As String)
                mDescription = Value
            End Set
        End Property
        Private mDescription As String

    End Class
End Namespace
