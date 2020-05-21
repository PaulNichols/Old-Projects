Namespace TaxonomyData
    <Serializable()> Public Class BOTable
        Implements ITable

#Region " Prelim code "

        Public Sub New(ByVal NewXML As Text.StringBuilder)
            Try

            Catch Ex As Exception
                Throw New Exception("Cannot create new BOTable object", Ex)
            End Try
        End Sub
#End Region

#Region " Properties "
        Private Property TheTable() As TaxonomyData.BOTable
            Get
                Return mTheTable
            End Get
            Set(ByVal Value As TaxonomyData.BOTable)
                mTheTable = Value
            End Set
        End Property
        Private mTheTable As TaxonomyData.BOTable

        Friend ReadOnly Property Entries() As IEnumerable
            Get
                'Return TheManifest.Row.Rows

            End Get
        End Property
#End Region

#Region " Helper Functions"



#End Region

    End Class
End Namespace