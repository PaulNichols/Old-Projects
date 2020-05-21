Namespace Application
    Public Interface IBOSpecimenMark
        Inherits IValidate

        Property IdMarkType() As ReferenceData.BOIDMarkType
        Property IdMark() As String '????
        Property MarkTypeDescription() As String
        Property SpecimenId() As Int32
        Property SpecimenMarkId() As Int32
        Property MarkCheckSum() As Integer
    End Interface
End Namespace