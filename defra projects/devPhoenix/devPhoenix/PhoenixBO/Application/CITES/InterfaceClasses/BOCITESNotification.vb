Namespace Application.CITES
    Public MustInherit Class BOCITESNotification
        Inherits BaseBO
        Implements CITES.IBOCITESNotification




#Region " Prelim code "
        Public Sub New()
            MyBase.New()

            ''init classes
            'mAgent = New Application.BOApplicationPartyDetails

            'mCountryOfOrigin = New ReferenceData.BOCountry
            'mCountryOfExport = New ReferenceData.BOCountry
            'mCountryOfImport = New ReferenceData.BOCountry
            'mActive = True
            'mValidated = False
            'mSpecie = New BOSpecie() {}
            'mParty = New Application.BOApplicationPartyDetails
        End Sub

        Public Sub New(ByVal citesNotificationId As Int32)
            MyClass.New()
            LoadCITESNotification(citesNotificationId)
        End Sub

        Private Function LoadCITESNotification(ByVal id As Int32) As DataObjects.Entity.CITESNotification
            Return LoadCITESNotification(id, Nothing)
        End Function

        Private Function LoadCITESNotification(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.CITESNotification
            Dim NewNotification As DataObjects.Entity.CITESNotification = DataObjects.Entity.CITESNotification.GetById(id)
            If NewNotification Is Nothing Then
                Throw New RecordDoesNotExist("CITESNotification", id)
            Else
                InitialiseCITESNotification(NewNotification, tran)
                Return NewNotification
            End If
        End Function

        Protected Overridable Sub InitialiseCITESNotification(ByVal notification As DataObjects.Entity.CITESNotification, ByVal tran As SqlClient.SqlTransaction)
            With notification
                mCITESNotificationId = .Id
                CheckSum = .CheckSum

                If Not .IsAgentPartyLinkIdNull Then
                    mAgent = New Application.BOApplicationPartyDetails(.AgentPartyLinkId, tran)
                End If
                If Not .IsCountryOfOriginIdNull Then
                    mCountryOfOrigin = New ReferenceData.BOCountry(.CountryOfOriginId, tran)
                End If
                If Not .IsExportedCountryIdNull Then
                    mCountryOfExport = New ReferenceData.BOCountry(.ExportedCountryId, tran)
                End If
                If Not .IsMemberStateOfImportIdNull Then
                    mCountryOfImport = New ReferenceData.BOCountry(.MemberStateOfImportId, tran)
                End If
                mActive = .Active
                mNotificationDate = .DateOfImport
                If Not .IsPartyLinkIdNull Then
                    mParty = New Application.BOApplicationPartyDetails(.PartyLinkId, tran)
                End If
                If Not .IsReferenceNumberNull Then mReference = .ReferenceNumber.Trim.ToString
                If Not .IsDateUnknownNull Then mDateUnknown = .DateUnknown
                If Not .IsReceivedDateNull Then mReceivedDate = .ReceivedDate
                If Not .IsUnknownCountryOfExportNull Then Me.mUnknownCountryOfExport = .UnknownCountryOfExport

                mValidated = .Validated

                'load specie
                Dim SpecNotification As New DataObjects.Service.NotificationSpecieLinkService
                'SpecNotification.GetForCITESNotification(mCITESNotificationId, tran).ConvertSetToEntityArray()
                Dim LinkSet As DataObjects.EntitySet.NotificationSpecieLinkSet = SpecNotification.GetForCITESNotification(mCITESNotificationId, tran)

                'clear and set size
                Erase mSpecie

                If LinkSet.Count > 0 Then
                    Dim Links() As Object = LinkSet.ConvertSetToEntityArray
                    ReDim mSpecie(LinkSet.Count - 1)

                    Dim Index As Int32 = 0
                    Dim Ascii As New System.Text.ASCIIEncoding
                    Dim A As Int32 = Ascii.Convert(System.Text.Encoding.ASCII, System.Text.Encoding.Default, Ascii.GetBytes("A"))(0)
                    For Each LinkItem As DataObjects.Entity.NotificationSpecieLink In Links
                        mSpecie(Index) = CType(AddSpecie(LinkItem.SpecieId, tran), BONotificationSpecie)
                        If TypeOf mSpecie(Index) Is CITES.ImportNotification.BOImportSpecie Then
                            CType(mSpecie(Index), CITES.ImportNotification.BOImportSpecie).Section = Ascii.GetString(New Byte() {CType(A + Index, Byte)})
                        End If
                        Index += 1
                    Next LinkItem
                End If

            End With
        End Sub
#End Region

#Region " Properties "
        Public Property Reference() As String Implements IBOCITESNotification.Reference
            Get
                Return mReference
            End Get
            Set(ByVal Value As String)
                mReference = Value
            End Set
        End Property
        Private mReference As String

        Public Property Active() As Boolean Implements IBOCITESNotification.Active
            Get
                Return mActive
            End Get
            Set(ByVal Value As Boolean)
                mActive = Value
            End Set
        End Property
        Private mActive As Boolean

        Public Property Agent() As BOApplicationPartyDetails Implements IBOCITESNotification.Agent
            Get
                Return mAgent
            End Get
            Set(ByVal Value As BOApplicationPartyDetails)
                mAgent = Value
            End Set
        End Property
        Private mAgent As BOApplicationPartyDetails

        Public Property CountryOfExport() As ReferenceData.BOCountry Implements IBOCITESNotification.CountryOfExport
            Get
                Return mCountryOfExport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfExport = Value
            End Set
        End Property
        Private mCountryOfExport As ReferenceData.BOCountry

        Public Property CountryOfImport() As ReferenceData.BOCountry Implements IBOCITESNotification.CountryOfImport
            Get
                If mCountryOfImport Is Nothing AndAlso TypeOf Me Is BO.Application.CITES.ImportNotification.BOImportNotification Then
                    Dim Config As New BO.BOConfiguration
                    Dim Result As Object = Config.GetValue("DefaultCountry")
                    If Not Result Is Nothing AndAlso _
                        Config.IsInt32(Result) Then
                        mCountryOfImport = New BO.ReferenceData.BOCountry(CType(Result, Int32))
                    Else
                        mCountryOfImport = Nothing
                    End If
                End If
                Return mCountryOfImport
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfImport = Value
            End Set
        End Property
        Private mCountryOfImport As ReferenceData.BOCountry

        Public Property CountryOfOrigin() As ReferenceData.BOCountry Implements IBOCITESNotification.CountryOfOrigin
            Get
                Return mCountryOfOrigin
            End Get
            Set(ByVal Value As ReferenceData.BOCountry)
                mCountryOfOrigin = Value
            End Set
        End Property
        Private mCountryOfOrigin As ReferenceData.BOCountry

        Public Property CITESNotificationId() As Integer Implements IBOCITESNotification.Id
            Get
                Return mCITESNotificationId
            End Get
            Set(ByVal Value As Integer)
                mCITESNotificationId = Value
            End Set
        End Property
        Protected mCITESNotificationId As Int32

        Public Property UnknownCountryOfExport() As Boolean Implements IBOCITESNotification.UnknownCountryOfExport
            Get
                Return mUnknownCountryOfExport
            End Get
            Set(ByVal Value As Boolean)
                mUnknownCountryOfExport = Value
            End Set
        End Property
        Private mUnknownCountryOfExport As Boolean


        Public Property NotificationDate() As Date Implements IBOCITESNotification.NotificationDate
            Get
                Return mNotificationDate
            End Get
            Set(ByVal Value As Date)
                mNotificationDate = Value
            End Set
        End Property
        Private mNotificationDate As Date

        Public Property ReceivedDate() As Object Implements IBOCITESNotification.RecievedDate
            Get
                Return mReceivedDate
            End Get
            Set(ByVal Value As Object)
                mReceivedDate = Value
            End Set
        End Property
        Private mReceivedDate As Object

        Public Property Party() As BOApplicationPartyDetails Implements IBOCITESNotification.Party
            Get
                Return mParty
            End Get
            Set(ByVal Value As BOApplicationPartyDetails)
                mParty = Value
            End Set
        End Property
        Private mParty As BOApplicationPartyDetails

        Public Property Specie() As Application.CITES.BONotificationSpecie() Implements IBOCITESNotification.Specie
            Get
                Return mSpecie
            End Get
            Set(ByVal Value() As Application.CITES.BONotificationSpecie)
                mSpecie = Value
            End Set
        End Property
        Protected mSpecie As Application.CITES.BONotificationSpecie()

        Public Property Validated() As Boolean Implements IBOCITESNotification.Validated
            Get
                Return mValidated
            End Get
            Set(ByVal Value As Boolean)
                mValidated = Value
            End Set
        End Property
        Private mValidated As Boolean

        'Public Property DerivitiveId() As Object Implements IBOCITESNotification.DerivitiveId
        '    Get
        '        Return mDerivitiveId
        '    End Get
        '    Set(ByVal Value As Object)
        '        mDerivitiveName = ""
        '        mDerivitiveId = Value
        '    End Set
        'End Property
        'Private mDerivitiveId As Object

        'Public Property UOM() As BOMeasurement Implements IBOCITESNotification.UOM
        '    Get
        '        Return mUOM
        '    End Get
        '    Set(ByVal Value As BOMeasurement)
        '        mUOM = Value
        '    End Set
        'End Property
        'Private mUOM As BOMeasurement

        Public Property DateUnknown() As Boolean Implements IBOCITESNotification.DateUnknown
            Get
                Return mDateUnknown
            End Get
            Set(ByVal Value As Boolean)
                mDateUnknown = Value
            End Set
        End Property
        Private mDateUnknown As Boolean
#End Region

#Region " Helper Functions "
        Protected Overridable Function AddSpecie(ByVal specieId As Int32, ByVal tran As SqlClient.SqlTransaction) As Object
            Return New BONotificationSpecie(specieId, tran)
        End Function
        Private ReadOnly Property CountryOfOriginId() As Object
            Get
                If mCountryOfOrigin Is Nothing OrElse mCountryOfOrigin.ID = 0 Then
                    Return Nothing
                Else
                    Return mCountryOfOrigin.ID
                End If
            End Get
        End Property

        Private ReadOnly Property CountryOfImportId() As Object
            Get
                If mCountryOfImport Is Nothing OrElse mCountryOfImport.ID = 0 Then
                    Return Nothing
                Else
                    Return mCountryOfImport.ID
                End If
            End Get
        End Property

        Private ReadOnly Property CountryOfExportId() As Object
            Get
                If mCountryOfExport Is Nothing OrElse mCountryOfExport.ID = 0 Then
                    Return Nothing
                Else
                    Return mCountryOfExport.ID
                End If
            End Get
        End Property

        Private ReadOnly Property AgentId() As Object
            Get
                If mAgent Is Nothing OrElse mAgent.LinkId = 0 Then
                    Return Nothing
                Else
                    Return mAgent.LinkId
                End If
            End Get
        End Property

        Private ReadOnly Property PartyId() As Object
            Get
                If mParty Is Nothing OrElse mParty.LinkId = 0 Then
                    Return Nothing
                Else
                    Return mParty.LinkId
                End If
            End Get
        End Property

        'Public Property DerivitiveName() As String Implements IBOCITESNotification.DerivitiveName
        '    Get
        '        If mDerivitiveName Is Nothing OrElse (mDerivitiveName = "" AndAlso Not mDerivitiveId Is Nothing _
        '            AndAlso CType(mDerivitiveId, Int32) > 0) Then

        '            If mDerivitiveId Is Nothing Then
        '                mDerivitiveName = ""
        '            Else
        '                Dim DerivitiveNameData As DataObjects.Entity.CITESDerivative = DataObjects.Entity.CITESDerivative.GetById(CType(mDerivitiveId, Int32))
        '                If Not DerivitiveNameData Is Nothing Then mDerivitiveName = DerivitiveNameData.Description
        '            End If
        '        End If
        '        Return mDerivitiveName
        '    End Get
        '    Set(ByVal Value As String)
        '        mDerivitiveName = Value
        '    End Set
        'End Property
        'Private mDerivitiveName As String

        'Private ReadOnly Property UOMId() As Object
        '    Get
        '        If mUOM Is Nothing Then
        '            Return Convert.DBNull
        '        Else
        '            Return mUOM.UOMId
        '        End If
        '    End Get
        'End Property

        Protected Shared Sub DeleteSpecie(ByVal specieId As Int32, ByVal citesNotificationId As Int32, ByVal checkSum As Int32, ByVal tran As SqlClient.SqlTransaction)
            DataObjects.Entity.NotificationSpecieLink.DeleteById(specieId, citesNotificationId, checkSum, tran)
        End Sub

        Shared Function SetLinks(ByVal citesNotificationId As Int32, ByVal specie As CITES.BONotificationSpecie, ByVal deleteRecordsFirst As Boolean) As Boolean
            Return SetLinks(citesNotificationId, specie, Nothing)
        End Function

        Shared Function SetLinks(ByVal citesNotificationId As Int32, ByVal specie As CITES.BONotificationSpecie, ByVal tran As SqlClient.SqlTransaction, ByVal deleteRecordsFirst As Boolean) As Boolean
            Return SetLinks(citesNotificationId, New BONotificationSpecie() {specie}, Nothing, tran, deleteRecordsFirst)
        End Function

        Shared Function SetLinks(ByVal citesNotificationId As Int32, ByVal specie() As CITES.BONotificationSpecie, ByVal service As EnterpriseObjects.Service, ByVal tran As SqlClient.SqlTransaction, ByVal deleteRecordsFirst As Boolean) As Boolean
            DataObjects.Sprocs.LastError = Nothing
            If deleteRecordsFirst Then
                Dim SpecNotification As New DataObjects.Service.NotificationSpecieLinkService
                SpecNotification.GetForCITESNotification(citesNotificationId, tran).ConvertSetToEntityArray()
                Dim LinkSet As DataObjects.EntitySet.NotificationSpecieLinkSet = SpecNotification.GetForCITESNotification(citesNotificationId, tran)

                '  Dim Links As DataObjects.Entity.NotificationSpecieLink() = CType(SpecNotification.GetForCITESNotification(mCITESNotificationId, tran).ConvertSetToEntityArray, DataObjects.Entity.NotificationSpecieLink())

                'drop the existing
                Dim Index As Int32 = 0
                For Each LinkItem As DataObjects.Entity.NotificationSpecieLink In LinkSet
                    DeleteSpecie(LinkItem.SpecieId, LinkItem.CITESNotificationId, LinkItem.CheckSum, tran)
                Next LinkItem
            End If

            'If specie.Length > 0 Then Me.Specie(0) = New ImportNotification.BOImportSpecie
            'create the new
            Dim NewSpecieItem As New DataObjects.Entity.NotificationSpecieLink
            Dim SpecieItemService As DataObjects.Service.NotificationSpecieLinkService = NewSpecieItem.ServiceObject
            Dim i As Int32
            For Each SpecieItem As BONotificationSpecie In specie

                If SpecieItem.SpecieId = 0 OrElse (Not SpecieItem.UOM Is Nothing AndAlso SpecieItem.UOM.UOMId = 0) Then
                    SpecieItem = SpecieItem.Save(tran)
                    If Not DataObjects.Sprocs.LastError Is Nothing OrElse SpecieItem Is Nothing OrElse _
                       (Not SpecieItem Is Nothing AndAlso Not SpecieItem.ValidationErrors Is Nothing) Then
                        If Not service Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                        Return False
                    End If
                End If

                Dim UOMID As Object = SpecieItem.UOMID
                If Not UOMID Is Nothing Then If CType(UOMID, Int32) = 0 Then UOMID = Nothing

                If SpecieItem.Created Or deleteRecordsFirst Then
                    SpecieItemService.Insert(SpecieItem.SpecieId, citesNotificationId, SpecieItem.DerivativeId, UOMID, tran)
                Else
                    SpecieItemService.Update(SpecieItem.SpecieId, citesNotificationId, SpecieItem.DerivativeId, UOMID, 0, tran)
                    If Not DataObjects.Sprocs.LastError Is Nothing Then
                        DataObjects.Sprocs.LastError = Nothing
                        SpecieItemService.Insert(SpecieItem.SpecieId, citesNotificationId, SpecieItem.DerivativeId, UOMID, tran)
                    End If

                End If
                If Not DataObjects.Sprocs.LastError Is Nothing Then
                    If Not service Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    Return False
                End If
                If Not SpecieItem.DerivativeId Is Nothing Then SpecieItem.Derivative = New ReferenceData.BOCITESDerivative(CType(SpecieItem.DerivativeId, Int32), tran)
                If TypeOf SpecieItem Is CITES.ImportNotification.BOImportSpecie Then
                    Dim Ascii As New System.Text.ASCIIEncoding
                    Dim A As Int32 = Ascii.Convert(System.Text.Encoding.ASCII, System.Text.Encoding.Default, Ascii.GetBytes("A"))(0)
                    CType(SpecieItem, CITES.ImportNotification.BOImportSpecie).Section = Ascii.GetString(New Byte() {CType(A + i, Byte)})
                End If
                i += 1
            Next SpecieItem
            Return True
        End Function
#End Region

#Region " Save "
        Public Overridable Shadows Function Save(ByVal ignoreValidation As Boolean, ByVal tran As SqlClient.SqlTransaction) As BOCITESNotification
            If Not ignoreValidation AndAlso Validated Then
                If Not Me.Validate() Is Nothing Then
                    Return Me
                End If
            End If
            MyBase.Save()

            Dim NewCITESNotification As New DataObjects.Entity.CITESNotification
            Dim service As DataObjects.Service.CITESNotificationService = NewCITESNotification.ServiceObject

            If Not Agent Is Nothing Then
                Dim Agent As BOApplicationPartyDetails = mAgent.Save(tran)
                If Not DataObjects.Sprocs.LastError Is Nothing OrElse Agent Is Nothing Then
                    'TODO: Use errors collection to check to see if the problem was concurrency
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveCITESNotification)
                    Return Me
                End If
            End If

            If Not mParty Is Nothing Then
                Try
                    Dim NewParty As BOApplicationPartyDetails = mParty.Save(tran)
                    If Not DataObjects.Sprocs.LastError Is Nothing OrElse NewParty Is Nothing Then
                        'TODO: Use errors collection to check to see if the problem was concurrency
                        ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveCITESNotification)
                        service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                        Return Me
                    End If
                Catch ex As Exception

                End Try

            End If



            'If Not DataObjects.Sprocs.LastError Is Nothing OrElse NewUOM Is Nothing Then
            '    'TODO: Use errors collection to check to see if the problem was concurrency
            '    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            '    Return Nothing
            'End If

            Created = (mCITESNotificationId = 0)

            If Created Then
                NewCITESNotification = service.Insert(CountryOfImportId, _
                                                       mNotificationDate, _
                                                      CountryOfOriginId, _
                                                      CountryOfExportId, _
                                                      AgentId, _
                                                      mActive, _
                                                      mValidated, _
                                                      PartyId, _
                                                      mReference, _
                                                      mDateUnknown, _
                                                      mReceivedDate, _
                                                      mUnknownCountryOfExport, _
                                                      tran)
            Else
                NewCITESNotification = service.Update(mCITESNotificationId, _
                                                      CountryOfImportId, _
                                                      mNotificationDate, _
                                                      CountryOfOriginId, _
                                                      CountryOfExportId, _
                                                      AgentId, _
                                                      mActive, _
                                                      mValidated, _
                                                      PartyId, _
                                                      mReference, _
                                                      mDateUnknown, _
                                                      mReceivedDate, _
                                                        mUnknownCountryOfExport, _
                                                      CheckSum, _
                                                      tran)
            End If
            'check to see if any SQL errors have occured
            If NewCITESNotification Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveCITESNotification)
            ElseIf NewCITESNotification Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveCITESNotification)
            Else
                If Created And Not NewCITESNotification Is Nothing Then
                    mCITESNotificationId = NewCITESNotification.Id
                End If
                'drop and create the specie links
                If Not mSpecie Is Nothing AndAlso mSpecie.Length > 0 Then
                    If Not SetLinks(mCITESNotificationId, mSpecie, service, tran, True) Then
                        ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveCITESNotification)
                        Return Me
                    End If
                End If

                'no point in initialising unless things have changed
                If NewCITESNotification.CheckSum <> CheckSum Then
                    InitialiseCITESNotification(NewCITESNotification, tran)
                End If
            End If
            Return Me
        End Function
#End Region

#Region " Validate "

        Public Overloads Function Validate() As ValidationManager Implements IValidate.Validate

        End Function


        Public Overridable Overloads Function Validate(ByVal ignoreWarnings As Boolean) As ValidationManager
            If Not mParty Is Nothing AndAlso _
               Not mParty.Party Is Nothing AndAlso _
               Not mParty.Address Is Nothing Then
                'check that the party has addresses associated to it that is this address
                Dim Addresses As BO.Party.BOAddress() = mParty.Party.GetAddresses(Nothing)
                If Addresses Is Nothing Then
                    'no addresses?
                    MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.ThisPartyDoesNotHaveAssociatedAddress))
                Else
                    Dim FoundAddress As Boolean = False
                    For Each Address As BO.Party.BOAddress In Addresses
                        If Address.AddressId = mParty.Address.AddressId Then
                            FoundAddress = True
                            Exit For
                            'good
                        End If
                    Next Address
                    If Not FoundAddress Then
                        MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.TheSelectedPartyHasMismatchedAddress))
                    End If

                    'is there a specie?
                    If mSpecie Is Nothing Then
                        'nope 
                        MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NoSpecieIdentified))
                    End If

                    'If mUOM Is Nothing Then
                    '    'no unit of measure!
                    '    MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.NoUnitOfMeasure))
                    'Else
                    '    'check that it's ok
                    '    mUOM.Validate(MyBase.ValidationErrors)
                    'End If
                End If
            End If
        End Function
#End Region

    End Class
End Namespace