'''<copyright>Defra 2004</copyright>
'''<author>Mark Lines-Davies</author>
'''<summary>
''' Class for calculating fees. Unlike the other classes in the Payments BO section, which are
''' entity classes, BOFeeCalculator is a controller class. Its interface, therefore, consists of methods
''' not properties. A separate class has been provided, rather than placing the business logic in
''' an existing entity class, as the calculations span several entities.
'''</summary>
Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Payments
    Public Class BOFeeCalculator
        Inherits PaymentsBaseBO
        Implements IBOFeeCalculator
#Region " Prelim code "
#End Region

#Region " Properties "

#End Region

#Region " Helper Functions "

#End Region

#Region " Save "

#End Region

#Region " Validate "
#End Region

#Region " Operations "

        Public Function CalculateStandAloneFee(ByVal applicationId As Int32) As BOCalculationResult Implements IBOFeeCalculator.CalculateStandAloneFee
            Dim result As New BOCalculationResult
            Dim app As New Entity.Application(applicationId)
            Dim appType As Int32 = app.ApplicationTypeId
            Dim permits As EntitySet.PermitSet = app.GetRelatedPermit()
            Dim commercialAnimalSpecies As New Hashtable
            Dim commercialPlantGenera As New Hashtable
            Dim noncommercialAnimalSpecies As New Hashtable
            Dim noncommercialPlantGenera As New Hashtable
            Dim standardFees As EntitySet.StandardFeeSet = Entity.StandardFee.GetAll()
            Dim fee As New Decimal(0)

            BuildHashtables(permits, commercialAnimalSpecies, commercialPlantGenera, noncommercialAnimalSpecies, noncommercialPlantGenera)
            Try
                fee += CalculateFee(standardFees, appType, True, True, commercialPlantGenera.Count)
                fee += CalculateFee(standardFees, appType, False, True, commercialAnimalSpecies.Count)
                fee += CalculateFee(standardFees, appType, True, False, noncommercialPlantGenera.Count)
                fee += CalculateFee(standardFees, appType, False, False, noncommercialAnimalSpecies.Count)
                result.Amount = fee
                result.PlantGeneraCount = commercialPlantGenera.Count + noncommercialPlantGenera.Count
                result.AnimalSpeciesCount = commercialAnimalSpecies.Count + noncommercialAnimalSpecies.Count
                app.FeeCharged = fee
                app.StandardFee = fee
                If Not app.SaveChanges() Then
                    result.ErrorMessage = "Could not save Application"
                End If
            Catch ex As Exception
                result.ErrorMessage = ex.Message
            End Try
            Return result
        End Function


        Public Sub BuildHashtables(ByVal permits As EntitySet.PermitSet, _
            ByRef commercialAnimalSpecies As Hashtable, ByRef commercialPlantGenera As Hashtable, _
            ByRef noncommercialAnimalSpecies As Hashtable, ByRef noncommercialPlantGenera As Hashtable)

            For Each permit As Entity.Permit In permits
                Dim specieId As Integer = permit.SpecieId
                Dim citesPermit As Application.CITES.BOCITESPermit = Application.CITES.BOCITESPermit.GetByPermitId(permit.Id)
                Dim commercial As Boolean = Not citesPermit.Purpose Is Nothing AndAlso citesPermit.Purpose.Code = "T"
                If specieId > 0 Then
                    Dim specie As New Entity.Specie(specieId)
                    Dim name As String = specie.ScientificName
                    If commercial Then
                        If IsPlant(specie) Then
                            commercialPlantGenera.Item(Genus(name)) = Nothing
                        Else
                            commercialAnimalSpecies.Item(name) = Nothing
                        End If
                    Else
                        If IsPlant(specie) Then
                            noncommercialPlantGenera.Item(Genus(name)) = Nothing
                        Else
                            noncommercialAnimalSpecies.Item(name) = Nothing
                        End If
                    End If
                End If
            Next
        End Sub

        Private Function CalculateFee(ByVal standardFees As EntitySet.StandardFeeSet, ByVal applicationType As Int32, ByVal plant As Boolean, ByVal commercial As Boolean, ByVal count As Int32) As Decimal
            If count = 0 Then
                Return 0
            End If
            For Each standardFee As Entity.StandardFee In standardFees
                With standardFee
                    'If .ApplicationTypeCode = applicationType AndAlso _
                    '   .LinkedApplicationTypeCode = 0 AndAlso _
                    '   (.PlantOrCoral = 1) = plant AndAlso _
                    '   (.CommercialPurpose = 1) = commercial AndAlso _
                    '   .MaximumNumberOfSpecies >= count AndAlso _
                    '   .MinimumNumberOfSpecies <= count Then
                    '    Return .Fee * count
                    'End If
                    If .ApplicationTypeCode = applicationType AndAlso _
                       (.PlantOrCoral = 1) = plant AndAlso _
                       (.CommercialPurpose = 1) = commercial AndAlso _
                       .MaximumNumberOfSpecies >= count AndAlso _
                       .MinimumNumberOfSpecies <= count Then
                        Return .Fee * count
                    End If
                End With
            Next
            Throw New Exception("No fee structure in database for application type=" + applicationType.ToString() + _
                ", plant=" + plant.ToString() + ", commercial=" + commercial.ToString() + ", count=" + count.ToString())
        End Function

        Private Function Genus(ByVal name As String) As String
            Return name.Split(New Char() {" "c})(0)
        End Function

        Private Function IsPlant(ByVal specie As Entity.Specie) As Boolean
            If specie.KeyedByApplicant Then
                Return specie.PaymentKingdom = Application.PaymentKingdomEnum.Plant
            End If
            Dim taxa As EntitySet.TaxonSet = Entity.Taxon.GetForSpecie(specie.Id)
            If taxa Is Nothing OrElse taxa.Count = 0 Then
                Return False 'don't know what it is
            End If
            Return taxa.Entities(0).PaymentKingdom = Application.PaymentKingdomEnum.Plant
        End Function

        Public Sub RecalculateBasketFees(ByVal basketId As Int32) Implements IBOFeeCalculator.RecalculateBasketFees

        End Sub
        
        Public Function CalculateBirdFee(ByVal applicationTypeId As Int32, ByVal specimenIds() As Int32) As BOCalculationResult Implements IBOFeeCalculator.CalculateBirdFee
            Dim result As New BOCalculationResult
            result.Amount = 17      'TO DO
            Return result
        End Function    
#End Region
    End Class
End Namespace

