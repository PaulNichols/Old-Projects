Namespace Application.CITES.ImportNotification
    Public Class BOImportNotification
        Inherits BOCITESNotification
        Implements IBOImportNotification


#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal notificationId As Int32)
            MyClass.New()
            LoadNotification(notificationId)
        End Sub

        Private Function LoadNotification(ByVal id As Int32) As DataObjects.Entity.ImportNotification
            Return LoadNotification(id, Nothing)
        End Function

        Private Function LoadNotification(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.ImportNotification
            Dim NewNotification As DataObjects.Entity.ImportNotification = DataObjects.Entity.ImportNotification.GetById(id, tran)
            If NewNotification Is Nothing Then
                Throw New RecordDoesNotExist("ImportNotification", id)
            Else
                InitialiseNotification(NewNotification, tran)
                Return NewNotification
            End If
        End Function

        Protected Overridable Sub InitialiseNotification(ByVal notification As DataObjects.Entity.ImportNotification, ByVal tran As SqlClient.SqlTransaction)
            With notification
                'get the base class data
                MyBase.InitialiseCITESNotification(New DataObjects.Entity.CITESNotification(.CITESNotificationId), tran)

                'set myself up
                Me.mImportNotificationId = .Id
                mImportNotificationCheckSum = .CheckSum

                If Not .IsRegionIdNull Then mRegionOfImportId = .RegionId
            End With
        End Sub

#End Region

#Region " Properties "
        Public Property ImportNotificationCheckSum() As Int32 Implements IBOImportNotification.CheckSum
            Get
                Return mImportNotificationCheckSum
            End Get
            Set(ByVal Value As Int32)
                mImportNotificationCheckSum = Value
            End Set
        End Property
        Private mImportNotificationCheckSum As Int32

        <Xml.Serialization.XmlIgnore()> _
        Public Shadows Property CITESNotificationId() As Int32
            Get
                Return MyBase.CITESNotificationId
            End Get
            Set(ByVal Value As Int32)
                MyBase.CITESNotificationId = Value
            End Set
        End Property

        Public Property ImportNotificationId() As Integer Implements IBOImportNotification.ImportNotificationId
            Get
                Return mImportNotificationId
            End Get
            Set(ByVal Value As Integer)
                mImportNotificationId = Value
            End Set
        End Property
        Private mImportNotificationId As Int32

        Public Property RegionOfImportId() As Object Implements IBOImportNotification.RegionOfImportId
            Get
                Return mRegionOfImportId
            End Get
            Set(ByVal Value As Object)
                mRegionOfImportId = Value
            End Set
        End Property
        Private mRegionOfImportId As Object

        Public Property CountryOfOriginRegionId() As Object Implements IBOImportNotification.CountryOfOriginRegionId
            Get
                Return mCountryOfOriginRegionId
            End Get
            Set(ByVal Value As Object)
                mCountryOfOriginRegionId = Value
            End Set
        End Property
        Private mCountryOfOriginRegionId As Object
#End Region

#Region " Helper Functions "
        Public Property RegionOfImport() As String Implements IBOImportNotification.RegionOfImport
            Get
                If mRegionOfImport Is Nothing Then
                    If mRegionOfImportId Is Nothing Then
                        mRegionOfImport = ""
                    Else
                        Dim NameData As DataObjects.Entity.UKCountry = DataObjects.Entity.UKCountry.GetById(CType(mRegionOfImportId, Int32))
                        If Not NameData Is Nothing Then mRegionOfImport = NameData.UKCountryName
                    End If
                End If
                Return mRegionOfImport
            End Get
            Set(ByVal Value As String)
                mRegionOfImport = Value
            End Set
        End Property
        Private mRegionOfImport As String

        Public Property CountryOfOriginRegion() As String Implements IBOImportNotification.CountryOfOriginRegion
            Get
                If mCountryOfOriginRegion Is Nothing Then
                    If mCountryOfOriginRegion Is Nothing Then
                        mCountryOfOriginRegion = ""
                    Else
                        Dim NameData As DataObjects.Entity.UKCountry = DataObjects.Entity.UKCountry.GetById(CType(mCountryOfOriginRegionId, Int32))
                        If Not NameData Is Nothing Then mCountryOfOriginRegion = NameData.UKCountryName
                    End If
                End If
                Return mCountryOfOriginRegion
            End Get
            Set(ByVal Value As String)
                mCountryOfOriginRegion = Value
            End Set
        End Property
        Private mCountryOfOriginRegion As String

        Protected Overrides Function AddSpecie(ByVal specieId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Object
            Dim spec As BOSpecie = CType(MyBase.AddSpecie(specieId, tran), BOSpecie)
            Dim NewImportSpec As New BOImportSpecie
            'load my data
            NewImportSpec.LoadImportSpecie(specieId, tran)
            'load the specie data
            NewImportSpec.InitialiseSpecie(spec, tran)

            Return NewImportSpec
        End Function
#End Region

#Region " Save "
        Public Overridable Shadows Function Save(ByVal ignoreValidation As Boolean) As BOImportNotification
            If Not ignoreValidation AndAlso Validated Then
                'as this record has been considered valid - we must ensure this continues by prevalidating
                If Not Validate(True) Is Nothing Then
                    'validation failed, so bail
                    Return Me
                End If
            End If

            Dim NewImportNotification As New DataObjects.Entity.ImportNotification
            Dim service As DataObjects.Service.ImportNotificationService = NewImportNotification.ServiceObject

            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            Dim Base As Application.CITES.BOCITESNotification = MyBase.Save(ignoreValidation, tran)

            If DataObjects.Sprocs.LastError Is Nothing AndAlso Not Base Is Nothing Then

                Created = (mImportNotificationId = 0)

                If Created Then
                    NewImportNotification = service.Insert(mRegionOfImportId, _
                                                        CITESNotificationId, _
                                                        Me.mCountryOfOriginRegionId, _
                                                        tran)
                Else
                    NewImportNotification = service.Update(mImportNotificationId, _
                                                        mRegionOfImportId, _
                                                        CITESNotificationId, _
                                                        Me.mCountryOfOriginRegionId, _
                                                        mImportNotificationCheckSum, _
                                                        tran)
                End If

                'check to see if any SQL errors have occured
                If (NewImportNotification Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                    'TODO: Use errors collection to check to see if the problem was concurrency
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveImportNotification)
                    Return Me
                ElseIf Created And Not NewImportNotification Is Nothing Then
                    mImportNotificationId = NewImportNotification.Id
                End If

                Try
                    If NewImportNotification.CheckSum <> ImportNotificationCheckSum Then
                        'no point in initialising unless things have changed
                        service.EndTransaction(tran)
                        InitialiseNotification(NewImportNotification, Nothing)
                    Else
                        service.EndTransaction(tran)
                    End If
                    Return Me
                Catch ex As Exception
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                End Try

            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.DoNothing)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveImportNotification)
                Return Me
            End If
        End Function
#End Region

        Private Function ReferenceNumberFound() As Boolean
            If Not Reference = "" Then
                Dim CITESNotificationService As New DataObjects.Service.CITESNotificationService
                Dim NotificationSet As DataObjects.EntitySet.CITESNotificationSet = CITESNotificationService.GetByIndex_IX_CITESNotification_1(Me.Reference, False)
                Return (Not NotificationSet Is Nothing AndAlso NotificationSet.Entities.Count > 0 AndAlso NotificationSet.Entities(0).Id <> Me.ImportNotificationId)
            End If
        End Function

#Region " Validate "

        Public Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean, ByVal saveApplication As Boolean) As BaseBO
            Me.Save(True)
            Me.ValidationErrors = Validate(writeFlag, ignoreWarnings)
            Return Me
        End Function

        Public Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean) As ValidationManager
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveImportNotification)

            MyBase.Validate(ignoreWarnings)

            If Me.CountryOfImport Is Nothing OrElse CountryOfImport.ID = 0 Then
                MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NoMemberStateOfImport))
            End If

            If Not Me.UnknownCountryOfExport AndAlso (Me.CountryOfExport Is Nothing OrElse Me.CountryOfExport.ID = 0) Then
                MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NoCountryOfExportNotMarkedUnknown))
            End If

            Dim Config As New BO.BOConfiguration
            Dim Result As Object = Config.GetValue("DefaultCountry")
            If Not Result Is Nothing AndAlso _
                Config.IsInt32(Result) Then
                If Me.CountryOfExport.ID = CType(Result, Int32) Then
                    MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.CountryOfExportCannotBeGB))
                End If
            End If
            Config = Nothing

            If Me.CountryOfOrigin Is Nothing OrElse Me.CountryOfOrigin.ID = 0 Then
                MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NoCountryOfOrigin))
            End If

            If ReferenceNumberFound() Then
                MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.ReferenceNumberUsedBefore))
            End If

            'check to see if we have too many species
            If Not MyBase.Specie Is Nothing Then
                If MyBase.Specie.Length > 6 Then
                    MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.TooManySpecie))
                End If
                For Each SpecieItem As BOSpecie In MyBase.Specie
                    If Not SpecieItem.IsAnnexC AndAlso Not SpecieItem.IsAppendixIII Then
                        If writeFlag Then Validated = False
                        MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.SpecieMustBeAnnexCOrAnnexD))
                        'no point in telling them that more than one has failed, so bail ourt of loop
                        Exit For
                    End If
                Next SpecieItem
            End If

            If MyBase.ValidationErrors.HasErrors Then
                If writeFlag Then Validated = False
            Else
                If writeFlag Then Validated = True
                MyBase.ValidationErrors = Nothing
            End If

            Return MyBase.ValidationErrors
        End Function
#End Region

        Public Shared Function UnlinkSpecie(ByVal specieId As Int32, ByVal cITESNotificationId As Int32) As Boolean
            Dim LinkService As New [DO].DataObjects.Service.NotificationSpecieLinkService
            Dim tran As SqlClient.SqlTransaction = LinkService.BeginTransaction
            Dim NotificationSpecieLink As New [DO].DataObjects.Entity.NotificationSpecieLink
            Dim Sucess As Boolean = True
            If NotificationSpecieLink.DeleteById(specieId, CITESNotificationId, tran) Then
                If [DO].DataObjects.Entity.ImportSpecie.DeleteById(specieId, tran) Then
                    If Not [DO].DataObjects.Entity.Specie.DeleteById(specieId, tran) Then
                        Sucess = False
                    End If
                Else
                    Sucess = False
                End If
            Else
                Sucess = False
            End If
            If Not Sucess Then
                LinkService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                LinkService.EndTransaction(tran)
            End If
            Return Sucess
        End Function
    End Class
End Namespace