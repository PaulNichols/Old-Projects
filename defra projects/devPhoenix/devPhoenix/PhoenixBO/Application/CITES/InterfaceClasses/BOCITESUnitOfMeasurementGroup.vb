
Namespace Application.CITES
    Public Class BOCITESUnitOfMeasurementGroup


        Public Shared Function GetGridData(ByVal includeHyphen As Boolean, ByVal bIncludeInActive As Boolean) As [DO].DataObjects.Collection.CITESUnitOfMeasurementBoundCollection
            Try
                Return DataObjects.Entity.CITESUnitOfMeasurement.GetAll(includeHyphen, bIncludeInActive, DataObjects.Base.CITESUnitOfMeasurementServiceBase.OrderBy.DefaultOrder).Entities
            Catch ex As Exception
            End Try
        End Function

        Public Shared Function GetAll(ByVal includeHyphen As Boolean) As [DO].DataObjects.Collection.CITESUnitOfMeasurementBoundCollection
            Return DataObjects.Entity.CITESUnitOfMeasurement.GetAll(includeHyphen).Entities
        End Function

    End Class
End Namespace

