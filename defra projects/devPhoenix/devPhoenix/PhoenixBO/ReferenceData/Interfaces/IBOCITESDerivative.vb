Namespace ReferenceData
    Public Interface IBOCITESDerivative
        Inherits IBOCode

        Property Explanation() As String
        Property PreferredUOM_01Object() As BOUnitOfMeasurement
        Property PreferredUOM_02Object() As BOUnitOfMeasurement
        Property PreferredUOM_03Object() As BOUnitOfMeasurement
        Property AlternativeUOM_01Object() As BOUnitOfMeasurement
        Property AlternativeUOM_02Object() As BOUnitOfMeasurement
        Property AlternativeUOM_03Object() As BOUnitOfMeasurement
        Property SortSequence() As Int32
    End Interface
End Namespace