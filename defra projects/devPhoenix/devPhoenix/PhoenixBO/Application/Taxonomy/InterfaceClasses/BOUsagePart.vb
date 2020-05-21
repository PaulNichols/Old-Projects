Namespace SearchTaxonomy
    <ServiceMapping(GetType(DataObjects.Service.TaxonomyPartService)), _
    EntityMapping(GetType(DataObjects.Entity.TaxonomyPart)), _
    CollectionMapping(GetType(DataObjects.Collection.TaxonomyPartBoundCollection)), _
    Serializable()> _
    Public Class BOUsagePart
        Inherits BaseDataBO
        Implements IUsagePart

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal id As Int32)
            MyBase.New(id, Nothing)
        End Sub

        Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyBase.New(id, tran)
        End Sub

        <DOtoBOMapping("PartDescription")> _
       Property Description() As String Implements IUsagePart.description
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
