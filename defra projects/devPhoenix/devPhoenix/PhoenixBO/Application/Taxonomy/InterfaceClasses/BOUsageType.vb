Namespace SearchTaxonomy
    <ServiceMapping(GetType(DataObjects.Service.TaxonomyUsageTypeService)), _
    EntityMapping(GetType(DataObjects.Entity.TaxonomyUsageType)), _
    CollectionMapping(GetType(DataObjects.Collection.TaxonomyUsageTypeBoundCollection)), _
    Serializable()> _
   Public Class BOUsageType
        Inherits BaseDataBO
        Implements IUsageType

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal id As Int32)
            MyBase.New(id, Nothing)
        End Sub

        Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyBase.New(id, tran)
        End Sub

        '<DOtoBOMapping("Id")> _
        '   Shadows Property ID() As Int32
        '    Get
        '        ID = mID
        '    End Get
        '    Set(ByVal Value As Int32)
        '        mID = Value
        '    End Set
        'End Property
        'Private mID As Int32

        <DOtoBOMapping("UsageTypeDescription")> _
       Property Description() As String Implements IUsageType.description
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