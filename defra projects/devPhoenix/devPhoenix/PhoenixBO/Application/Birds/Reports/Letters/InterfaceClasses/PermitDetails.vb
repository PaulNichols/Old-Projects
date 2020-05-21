Imports uk.gov.defra.Phoenix.BO.Party
Imports uk.gov.defra.Phoenix.BO.Application.CITES.Applications

Namespace Application.Letter.Reports

    <Serializable()> _
    Public Class PermitDetails

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal permitInfoId As Int32, ByVal fromTo As String)
            MyBase.New()
            LoadPermitDetails(permitInfoId, fromTo)
        End Sub

        Private Sub LoadPermitDetails(ByVal permitInfoId As Int32, ByVal fromTo As String)
            Dim info As New BOPermitInfo(permitInfoId)
            Dim permit As BOCITESImportExportPermit = CType(BOPermit.PolymorphicCreate(info.PermitId), BOCITESImportExportPermit)
            Dim species As BOSpecie = permit.Specie

            mDescriptionText = permit.GetAllSpecimenDescs()
            mScientificNameText = "(" + species.ScientificName + ")"
            mImportExportFromTo = fromTo
        End Sub


        Public ReadOnly Property DescriptionText() As String
            Get
                Return mDescriptionText
            End Get
        End Property
        Private mDescriptionText As String

        Public ReadOnly Property ScientificNameText() As String
            Get
                Return mScientificNameText
            End Get
        End Property
        Private mScientificNameText As String

        Public ReadOnly Property ImportExportFromTo() As String
            Get
                Return mImportExportFromTo
            End Get
        End Property
        Private mImportExportFromTo As String
    End Class

End Namespace
