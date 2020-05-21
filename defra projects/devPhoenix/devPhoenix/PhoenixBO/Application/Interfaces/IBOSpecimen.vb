Namespace Application
    Public Enum GenderType
        Unknown = 0
        Male = 1
        Female = 2
    End Enum

    Public Interface IBOSpecimen
        Property SpecimenId() As Int32
        'Property ApplicationId() As Int32
        Property SpecimenMarks() As BOSpecimenMark()
        Property Gender() As GenderType
        Property DOB() As Date  'MLD 31/1/5 refactored
        Property Fate() As ReferenceData.BOSpecimenFate
        Property AcquisitionDate() As Date  'MLD 31/1/5 refactored
        Property Description() As String
        Property FatherSpecimen() As BOSpecimen
        Property MotherSpecimen() As BOSpecimen
        Property ExactDOB() As Boolean
        Property SpecimenCheckSum() As Int32
        Property Measurement() As BOMeasurement
        Property FateDate As Date   'MLD 27/1/5 added (NB. no need to use Object)
    End Interface
End Namespace
