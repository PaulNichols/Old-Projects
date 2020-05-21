Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Taxonomy.Plant
    <Serializable()> _
    Public Class BOClass
        Inherits BOPhylum

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Sub New(ByVal taxon As Taxonomy.BOTaxon)
            MyBase.New(taxon)
        End Sub

#End Region

#Region " Properties "

        Public Overrides Property LegislationAllowed As Boolean
            Get
                Return True
            End Get
            Set
            End Set
        End Property

        Public Overrides Property DecisionAllowed As Boolean
            Get
                Return True
            End Get
            Set
            End Set
        End Property

        Public Overrides Property CommonNameAllowed As Boolean
            Get
                Return True
            End Get
            Set
            End Set
        End Property
#End Region

#Region " Save "
        Public Overrides Overloads Sub Save(ByVal tran As SqlClient.SqlTransaction)
            MyBase.Save(tran, TaxonTypeEnum.Class)
        End Sub

        Public Overrides Function CreateChild() As BOTaxonBase
            Return FillChild(New BOOrder())
        End Function
#End Region


    End Class
End Namespace