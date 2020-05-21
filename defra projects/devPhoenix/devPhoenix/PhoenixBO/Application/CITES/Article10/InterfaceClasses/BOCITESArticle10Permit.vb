Imports System.Text
Imports uk.gov.defra.Phoenix.DO.DataObjects.Entity
Imports uk.gov.defra.Phoenix.DO.DataObjects.EntitySet

Namespace Application.CITES.Applications
    Public Class BOCITESArticle10Permit
        Inherits BOCITESImportExportPermit
        Implements IBOCITESArticle10Permit



#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal citesArticle10Id As Int32)
            MyClass.New(citesArticle10Id, Nothing)
        End Sub

        Public Sub New(ByVal citesArticle10Id As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            MyBase.LoadCITESImportExportPermit(citesArticle10Id, tran)
        End Sub

        Friend Overrides Sub InitialiseCITESImportExportPermit(ByVal citesImportExportPermit As DataObjects.Entity.CITESImportExportPermit, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.InitialiseCITESImportExportPermit(citesImportExportPermit, tran)
            If Not citesImportExportPermit.IsPreviousCertificateIssueDateNull Then mPreviousCertificateIssueDate = citesImportExportPermit.PreviousCertificateIssueDate
            If Not citesImportExportPermit.IsPreviousCertificateNumberNull Then Me.mPreviousCertificateNumber = citesImportExportPermit.PreviousCertificateNumber
            If Not citesImportExportPermit.IsIsTransactionSpecificNull Then
                mIsTransactionSpecific = citesImportExportPermit.IsTransactionSpecific
            Else
                mIsTransactionSpecific = Nothing
            End If
        End Sub
#End Region

#Region " Properties "
        Public Property TranOrSpecType() As String Implements IBOCITESArticle10Permit.TranOrSpecType
            Get
                If IsTransactionSpecific Is Nothing Then
                    Return ""
                ElseIf CType(IsTransactionSpecific, Boolean) Then
                    Return "T"
                ElseIf Not CType(IsTransactionSpecific, Boolean) Then
                    Return "S"
                End If
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Property IsTransactionSpecific() As Object Implements IBOCITESArticle10Permit.IsTransactionSpecific
            Get
                Return mIsTransactionSpecific
            End Get
            Set(ByVal Value As Object)
                mIsTransactionSpecific = Value
            End Set
        End Property

        Public Property MemberStateOfImport() As ReferenceData.BOCountry Implements IBOCITESArticle10Permit.MemberStateOfImport
            Get
                Return MyBase.CofLExport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                MyBase.CofLExport = Value
            End Set
        End Property

        Public Property MemberStateOfImportDocumentNumber() As String Implements IBOCITESArticle10Permit.MemberStateOfImportDocumentNumber
            Get
                Return MyBase.CofLExportNumber
            End Get
            Set(ByVal Value As String)
                MyBase.CofLExportNumber = Value
            End Set
        End Property

        Public Property MemberStateOfImportDateOfIssue() As Object Implements IBOCITESArticle10Permit.MemberStateOfImportDateOfIssue
            Get
                Return MyBase.CofLExportIssueDate
            End Get
            Set(ByVal Value As Object)
                MyBase.CofLExportIssueDate = Value
            End Set
        End Property

#End Region

        Public Overrides Function CreatePermitFromPermit(ByVal specimenId As Int32, ByVal typeToCreate As uk.gov.defra.Phoenix.BO.Application.ApplicationTypes, ByVal fromType As uk.gov.defra.Phoenix.BO.Application.ApplicationTypes) As BO.Application.CITES.Applications.BOCITESImportExportPermit
            Dim returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit
            If specimenId = 0 Then
                returnPermit = CType(Me.Clone, BO.Application.CITES.Applications.BOCITESImportExportPermit)
                Select Case typeToCreate
                    Case ApplicationTypes.Article10
                        PopulateArt10PermitFromArticle10Permit(returnPermit)
                    Case ApplicationTypes.Export
                        PopulateExportPermitFromArticle10Permit(returnPermit)
                    Case ApplicationTypes.Import
                        PopulateImportPermitFromArticle10Permit(returnPermit)

                End Select
            Else
                returnPermit = New Application.CITES.Applications.BOCITESArticle10Permit
                PopulateSpecimenDetails(specimenId, returnPermit)
            End If
            Return returnPermit
        End Function

        Private Sub PopulateArt10PermitFromArticle10Permit(ByRef returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit)
            With CType(returnPermit, BO.Application.CITES.Applications.BOCITESArticle10Permit)
                .UnderDerogation = False
                .Description = ""
            End With
        End Sub

        Private Sub PopulateImportPermitFromArticle10Permit(ByRef returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit)
            With CType(returnPermit, BO.Application.CITES.Applications.BOCITESArticle10Permit)
                .UnderDerogation = False
                .CofLExportNumber = ""
                .CofLExportIssueDate = Nothing
                .CofLExportPermitExpiryDate = Nothing
                .CofLExport = Nothing
                .Description = ""
            End With
        End Sub

        Private Sub PopulateExportPermitFromArticle10Permit(ByRef returnPermit As BO.Application.CITES.Applications.BOCITESImportExportPermit)
            With CType(returnPermit, BO.Application.CITES.Applications.BOCITESArticle10Permit)
                .UnderDerogation = False
                .CofLExportNumber = ""
                .PreviousCertificateIssueDate = Nothing
                .PreviousCertificateNumber = Nothing
                .CofLExportIssueDate = Nothing
                .Specimens = Nothing
                .CofLExportPermitExpiryDate = Nothing
                .CofLExport = Nothing
                .Description = ""
            End With
        End Sub

        Public Function GetFate(ByVal trans As SqlClient.SqlTransaction) As BO.Application.CITES.Applications.BOArticle10CertificateFate
            Dim Fate As New BO.Application.CITES.Applications.BOArticle10CertificateFate
            Fate.LoadFateByCertificateId(PermitId, trans)
            If Not Fate Is Nothing AndAlso Fate.Fate Is Nothing Then
                Fate.Fate = New ReferenceData.BOArticle10Fate
            End If
            Return Fate
        End Function

        Private Overloads Shared Function GetPermit(ByVal permitInfoId As Int32, ByRef info As BOPermitInfo) As BOCITESArticle10Permit
            Try
                info = New BOPermitInfo(permitInfoId)
            Catch ex As Exception
                Throw New Exception("Cannot find PermitInfo, id=" + permitInfoId.ToString())
            End Try
            Try
                Return CType(BOPermit.PolymorphicCreate(info.PermitId), BOCITESArticle10Permit)
            Catch ex As Exception
                Throw New Exception("Cannot find Permit, id=" + info.PermitId.ToString())
            End Try
        End Function

        Private Shared Function GetUserName(ByVal user As BOAuthorisedUser) As String
            If user Is Nothing Then Return ""
            Return user.FullName
        End Function

        Public Shared Function GetArticle10ApplicationReportTestData(ByVal PermitInfoId As Int32, ByVal schema As String) As ReportDataResults

            'get the DS ready
            Dim ReturnDS As New DataSet
            'create a stream to put the info into
            Dim io As New io.StringReader(schema)
            'set-up the DS schema
            ReturnDS.ReadXmlSchema(io)
            'tidy ...
            io.Close()
            io = Nothing

            Dim copyDataset As DataSet
            Dim copyDataRow As DataRow

            'create a new row using the ds schema
            Dim newRow As DataRow

            Dim searchReference As String = "GB123456/01" ' Get Article10 Reference 

            ' Create a new row - Populate with test data
            newRow = ReturnDS.Tables("BOPermit").NewRow()

            With newRow

                ' Create new Article10 Application Details
                .Item("Reference") = searchReference
                .Item("BarCode") = "00GB1234501000ADDMMYYYY"

                .Item("SheetDescription") = "APPLICATION" ' Hard Coded
                .Item("SheetNumber") = "3" ' Hard Coded
                .Item("PageMofN") = "Page 1 of 1" ' Need to calculate this when using live data!

                ' Create new IssuingManagementAuthority Details
                .Item("IssuingManagementAuthority_NameAddress") = "Wildlife Licensing and Registration Service" & Environment.NewLine & _
                "Floor 1, Zone 17, Temple Quary House" & Environment.NewLine & _
                "2 The Square, Temple Quary" & Environment.NewLine & _
                "Bristol BS1 6EB  Tel 0044 (0)117 372 8749" & Environment.NewLine & _
                "Department for Enviroment, Food and Rural Affairs"

                ' Create new Specimen Details
                .Item("Specimen_Description") = "microchip number 0005FC1EFS"
                .Item("Specimen_Appendix") = "I"
                .Item("Specimen_CommonName") = "Orang-utan"
                .Item("Specimen_ECAnnex") = "A"
                .Item("Specimen_Mass") = "160"
                .Item("Specimen_OriginCountry") = "INDONESIA"
                .Item("Specimen_OriginPermitDate") = "04/06/2002"
                .Item("Specimen_OriginPermitRef") = "5246A-2002"
                .Item("Specimen_Quantity") = "1"
                .Item("Specimen_ScientificName") = "Pongo pymaeus"
                .Item("Specimen_Source") = "C"
                .Item("Specimen_ImportCountry") = "TAIWAN"
                .Item("Specimen_DocumentRef") = "07/06/2002"
                .Item("Specimen_ImportIssueDate") = "245365/01"

                ' Create new Holder Details
                .Item("Holder_AuthorisedPartyId") = "1"
                .Item("Holder_PartyName") = "PINGTANG RESCUE CENTRE FOR ENDANGERED WILD ANIMALS"
                .Item("Holder_PartyAddress") = "UNIV. OF SCIENCE and TECHNOLOGY" & Environment.NewLine & _
                "1 HSUEH-HE ROAD, CHIUNG" & Environment.NewLine & _
                "PINGTANG 94982" & Environment.NewLine & _
                "TAIWAN"

                ' Create new Location Details
                .Item("Location_Address") = "Location Address1" & Environment.NewLine & _
                "Location Address2" & Environment.NewLine & _
                "Location Address3" & Environment.NewLine & _
                "Location Address4"

                .Item("Box18_1X") = ""
                .Item("Box18_2X") = ""
                .Item("Box18_3X") = ""
                .Item("Box18_4X") = "X"
                .Item("Box18_5X") = ""
                .Item("Box18_6X") = ""
                .Item("Box18_7X") = ""
                .Item("Box18_8X") = ""

                .Item("Box19_1X") = ""
                .Item("Box19_2X") = "X"
                .Item("Box19_3X") = ""

                .Item("AdditionalDetails") = "Additional Details"
            End With

            'add the row to the dataset - Sheet 1
            ReturnDS.Tables("BOPermit").Rows.Add(newRow)
            ReturnDS.AcceptChanges()

            Return New ReportDataResults(ReturnDS, searchReference)
        End Function

        Public Shared Function GetArticle10ApplicationReportData(ByVal permitInfoId As Int32, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim newRow As DataRow = returnDS.Tables("BOPermit").NewRow()
            Dim info As BOPermitInfo
            Dim permit As BOCITESArticle10Permit = GetPermit(permitInfoId, info)
            Dim species As BOSpecie = permit.Specie
            Dim issueDate As DateTime = DateTime.Now
            Dim application As BOCITESArticle10 = permit.GetArticle10Application()
            Dim reference As String = permit.ReportISOCode + permit.ReportApplicationRef

            With newRow
                ' Create new Article10 Application Details
                .Item("Reference") = reference
                .Item("BarCode") = permit.GetBarCode("0", issueDate)
                .Item("SheetDescription") = "APPLICATION" ' Hard Coded
                .Item("SheetNumber") = "3" ' Hard Coded
                .Item("PageMofN") = "Page 1 of 1" ' Hard Coded
                GetBasicSpecimenDetails(newRow, permit, species)
                If Not application Is Nothing Then
                    GetApplicationRelatedDetails(newRow, application)
                    .Item("IssuingManagementAuthority_NameAddress") = GetIssuingAuthority(application)
                    .Item("AdditionalDetails") = Resolve(application.AdditionalDeclaration, "OtherInformation")
                End If
            End With

            'add the row to the dataset - Sheet 1
            returnDS.Tables("BOPermit").Rows.Add(newRow)
            returnDS.AcceptChanges()

            Return New ReportDataResults(returnDS, reference)

        End Function


        Public Shared Function GetArticle10ReportData(ByVal permitInfoId As Int32, ByVal duplicate As Boolean, ByVal schema As String) As ReportDataResults
            Dim returnDS As DataSet = GetSchemaDataSet(schema)
            Dim newRow As DataRow = returnDS.Tables("BOPermit").NewRow()
            Dim info As BOPermitInfo
            Dim permit As BOCITESArticle10Permit = GetPermit(permitInfoId, info)
            Dim species As BOSpecie = permit.Specie
            Dim issueDate As DateTime = DateTime.Now
            Dim issueLetter As String = info.GetNextIssue()
            Dim printStatus As String = ""
            Dim application As BOCITESArticle10 = permit.GetArticle10Application()
            Dim authorityPartyId As String = ""
            Dim authorityPartyName As String = ""
            Dim authorityPartyAddress As String = ""

            If duplicate Then
                printStatus = "DUPLICATE"
            End If
            If Not application Is Nothing AndAlso Not application.ManagementAuthority Is Nothing Then
                authorityPartyId = application.ManagementAuthority.Party.PartyId.ToString()
                authorityPartyName = application.ManagementAuthority.Party.DisplayName
                authorityPartyAddress = application.ManagementAuthority.Address.ReportAddress
            End If

            With newRow
                ' Permit Reference Number = IsoContryCode & ApplicationRef & "/" & PermitNo & "/" & CopyNo
                .Item("PermitReference") = permit.PermitReference(permit.GetCopyNo(info))
                .Item("Barcode") = permit.GetBarCode(issueLetter, issueDate)
                .Item("IssueDate") = issueDate.ToString("dd MMMM yyyy")
                .Item("IssueLetter") = issueLetter
                .Item("IssuePlace") = Resolve(info.PlaceOfIssue)
                .Item("SpecialConditions") = permit.GetReportSpecialConditions(permit.GetSpecialConditions(False))
                .Item("UserName") = GetUserName(info.IssuedBy)
                .Item("SheetDescription") = "ORIGINAL" ' Hard coded here - Changed Later
                .Item("SheetNumber") = "1" ' Hard coded here - Changed Later
                .Item("PageMofN") = "Page 1 of 1" ' Hard coded here - Changed Later
                .Item("PrintStatus") = printStatus

                ' Create new IssuingManagementAuthority Details
                .Item("IssuingManagementAuthority_AuthorisedPartyId") = authorityPartyId
                .Item("IssuingManagementAuthority_PartyAddress") = authorityPartyAddress
                .Item("IssuingManagementAuthority_PartyName") = authorityPartyName

                ' Create new Specimen Details
                GetBasicSpecimenDetails(newRow, permit, species)
                .Item("Specimen_ImportCountry") = Resolve(application.CountryOfImport, "LongName")
                .Item("Specimen_ImportPermitDate") = permit.PermitDate_String
                .Item("Specimen_ImportPermitRef") = permit.ApplicationPermitNumber

                If Not application Is Nothing Then
                    GetApplicationRelatedDetails(newRow, application)
                End If
                If Not permit.IsTransactionSpecific Is Nothing Then
                    .Item("ValidHolderX") = Resolve(CType(permit.IsTransactionSpecific, Boolean))
                End If
            End With

            'add the row to the dataset
            returnDS.Tables("BOPermit").Rows.Add(newRow)

            'return the datset containing the single row
            Return New ReportDataResults(returnDS, "")
        End Function

        Private Shared Sub GetBasicSpecimenDetails(ByRef newRow As DataRow, ByVal permit As BOCITESArticle10Permit, ByVal species As BOSpecie)
            With newRow
                .Item("Specimen_Description") = permit.GetSpecimenDesc()
                .Item("Specimen_Appendix") = species.CITESAppendix
                .Item("Specimen_CommonName") = species.CommonName
                .Item("Specimen_ECAnnex") = species.ECAnnex
                .Item("Specimen_Mass") = permit.NetMassString
                .Item("Specimen_OriginCountry") = permit.GetOriginCountry()
                .Item("Specimen_OriginPermitDate") = permit.CountryOfOriginPermitDate_String
                .Item("Specimen_OriginPermitRef") = permit.CountryOfOriginPermitNo_String
                .Item("Specimen_Quantity") = "1"
                .Item("Specimen_ScientificName") = species.ScientificName
                .Item("Specimen_Source") = species.Source
            End With
        End Sub

        Private Shared Sub GetApplicationRelatedDetails(ByRef newRow As DataRow, ByVal application As BOCITESArticle10)
            With newRow
                If Not application.Holder Is Nothing Then
                    .Item("Holder_AuthorisedPartyId") = application.Holder.Party.PartyId
                    .Item("Holder_PartyName") = application.Holder.Party.DisplayName
                    .Item("Holder_PartyAddress") = application.Holder.Address.ReportAddress
                End If
                .Item("Location_Address") = Resolve(application.LocationAddress, "ReportAddress")
                .Item("Box18_1X") = Resolve(application.Box18_1)
                .Item("Box18_2X") = Resolve(application.Box18_2)
                .Item("Box18_3X") = Resolve(application.Box18_3)
                .Item("Box18_4X") = Resolve(application.Box18_4)
                .Item("Box18_5X") = Resolve(application.Box18_5)
                .Item("Box18_6X") = Resolve(application.Box18_6)
                .Item("Box18_7X") = Resolve(application.Box18_7)
                .Item("Box18_8X") = Resolve(application.Box18_8)
                .Item("Box19_1X") = Resolve(application.Article10Type.ID = 1)
                .Item("Box19_2X") = Resolve(application.Article10Type.ID = 2)
                .Item("Box19_3X") = Resolve(application.Article10Type.ID = 3)
            End With
        End Sub

    End Class
End Namespace
