Namespace Application.CITES.SeizureNotification
    Public Class BOSeizureNotification
        Inherits Application.CITES.BOCITESNotification
        Implements IBOSeizureNotification


#Region " Prelim code "
        Public Sub New()
            MyBase.New()

            'init classes
            'mType = NotificationType.Article10
            'mUOM = New BOMeasurement
        End Sub

        Public Sub New(ByVal notificationId As Int32)
            MyClass.New()
            LoadNotification(notificationId)
        End Sub

        Private Function LoadNotification(ByVal id As Int32) As DataObjects.Entity.SeizureNotification
            Return LoadNotification(id, Nothing)
        End Function

        Private Function LoadNotification(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.SeizureNotification
            Dim NewNotification As DataObjects.Entity.SeizureNotification = DataObjects.Entity.SeizureNotification.GetById(id)
            If NewNotification Is Nothing Then
                Throw New RecordDoesNotExist("SeizureNotification", id)
            Else
                InitialiseNotification(NewNotification, tran)
                Return NewNotification
            End If
        End Function

        Protected Overridable Sub InitialiseNotification(ByVal notification As DataObjects.Entity.SeizureNotification, ByVal tran As SqlClient.SqlTransaction)
            With notification
                'get the base class data
                MyBase.InitialiseCITESNotification(New DataObjects.Entity.CITESNotification(.CITESNotificationId), tran)

                'set myself up
                mSeizureNotificationId = .Id
                mSeizureNotificationCheckSum = .CheckSum

                If Not .IsPortOfEntryIDNull Then mPortOfEntryID = .PortOfEntryID

                '   If Not .IsTypeNull Then mType = CType(.Type, Application.ApplicationTypes)
                'If Not .IsSpecieIdNull Then mSpecie = New Application.BOSpecie(.SpecieId)
                If Not .IsSeizureReasonNull Then mSeizureReason = .SeizureReason
                If Not .IsCustomsReferenceNull Then mCustomsReference = .CustomsReference
            End With
        End Sub
#End Region

#Region " Properties "
        Public Property SeizureNotificationCheckSum() As Int32 Implements IBOSeizureNotification.CheckSum
            Get
                Return mSeizureNotificationCheckSum
            End Get
            Set(ByVal Value As Int32)
                mSeizureNotificationCheckSum = Value
            End Set
        End Property
        Private mSeizureNotificationCheckSum As Int32

        <Xml.Serialization.XmlIgnore()> _
        Public Shadows Property CITESNotificationId() As Int32 Implements IBOSeizureNotification.CITESNotificationId
            Get
                Return MyBase.CITESNotificationId
            End Get
            Set(ByVal Value As Int32)
                MyBase.CITESNotificationId = Value
            End Set
        End Property

        Public Property Type() As Application.ApplicationTypes Implements IBOSeizureNotification.NotificationType
            Get
                Return ApplicationTypes.Seizure
            End Get
            Set(ByVal Value As Application.ApplicationTypes)
                ' mType = Value
            End Set
        End Property
        ' Private mType As Application.ApplicationTypes

        Public Property CustomsReference() As String Implements IBOSeizureNotification.CustomsReference
            Get
                Return mCustomsReference
            End Get
            Set(ByVal Value As String)
                mCustomsReference = Value
            End Set
        End Property
        Private mCustomsReference As String

        Public Property SingleSpecie() As Application.cites.BONotificationSpecie Implements IBOSeizureNotification.SingleSpecie
            Get
                If Not Specie Is Nothing AndAlso Specie.Length = 1 Then
                    Return Specie(0)
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal Value As Application.cites.BONotificationSpecie)
                If Not Specie Is Nothing AndAlso Specie.Length = 1 Then
                    Specie(0) = Value
                ElseIf Specie.Length = 0 AndAlso Not Value Is Nothing Then
                    ReDim Specie(0)
                    Specie(0) = Value
                End If
            End Set
        End Property

        Public Property SeizureNotificationId() As Integer Implements IBOSeizureNotification.SeizureNotificationId
            Get
                Return mSeizureNotificationId
            End Get
            Set(ByVal Value As Integer)
                mSeizureNotificationId = Value
            End Set
        End Property
        Private mSeizureNotificationId As Int32

        Public Property PortOfEntryID() As object Implements IBOSeizureNotification.PortOfEntryID
            Get
                Return mPortOfEntryID
            End Get
            Set(ByVal Value As object)
                mPortOfEntry = ""
                mPortOfEntryID = Value
            End Set
        End Property
        Private mPortOfEntryID As object


#End Region

#Region " Helper Functions "

        Public Property PortOfEntry() As String Implements IBOSeizureNotification.PortOfEntry
            Get
                If Not mPortOfEntryID Is Nothing AndAlso (mPortOfEntry Is Nothing OrElse (mPortOfEntry = "" AndAlso CType(mPortOfEntryID, Int32) > 0)) Then
                    Dim PortOfEntryData As DataObjects.Entity.PortOfEntry = DataObjects.Entity.PortOfEntry.GetById(CType(mPortOfEntryID, Int32))
                    If Not PortOfEntryData Is Nothing Then mPortOfEntry = PortOfEntryData.Description
                End If
                Return mPortOfEntry
            End Get
            Set(ByVal Value As String)
                mPortOfEntry = Value
            End Set
        End Property
        Private mPortOfEntry As String

        Public Property SeizureReason() As Object Implements IBOSeizureNotification.Reason
            Get
                Return mSeizureReason
            End Get
            Set(ByVal Value As Object)
                mSeizureReason = Value
            End Set
        End Property
        Private mSeizureReason As Object
#End Region

#Region " Save "
        Public Overridable Shadows Function Save(ByVal ignoreValidation As Boolean) As BOSeizureNotification
            If Validated AndAlso Not ignoreValidation Then
                'as this record has been considered valid - we must ensure this continues by prevalidating
                If Not Validate(True) Is Nothing Then
                    'validation failed, so bail
                    Return Me
                End If
            End If

            Dim NewSeizureNotification As New DataObjects.Entity.SeizureNotification
            Dim service As DataObjects.Service.SeizureNotificationService = NewSeizureNotification.ServiceObject

            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            Dim Base As Application.CITES.BOCITESNotification = MyBase.Save(ignoreValidation, tran)

            If DataObjects.Sprocs.LastError Is Nothing AndAlso Not Base Is Nothing Then

                Created = (mSeizureNotificationId = 0)

                If Created Then
                    NewSeizureNotification = service.Insert(mCustomsReference, _
                                                            mSeizureReason, _
                                                            PortOfEntryID, _
                                                            CITESNotificationId, _
                                                            Type, _
                                                            tran)
                Else
                    NewSeizureNotification = service.Update(mSeizureNotificationId, _
                                                            mCustomsReference, _
                                                            mSeizureReason, _
                                                            PortOfEntryID, _
                                                            CITESNotificationId, _
                                                            Type, _
                                                            mSeizureNotificationCheckSum, _
                                                            tran)
                End If


                'check to see if any SQL errors have occured
                If (NewSeizureNotification Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing) Then
                    'TODO: Use errors collection to check to see if the problem was concurrency
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSeizureNotification)
                    Return Me
                ElseIf Created And Not NewSeizureNotification Is Nothing Then
                    mSeizureNotificationId = NewSeizureNotification.Id
                End If

                If Not mSpecie Is Nothing AndAlso mSpecie.Length > 0 Then
                    If Not SetLinks(mCITESNotificationId, mSpecie, service, tran, True) Then
                        ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveCITESNotification)
                        Return Me
                    End If
                End If

                If NewSeizureNotification.CheckSum <> SeizureNotificationCheckSum Then
                    'no point in initialising unless things have changed
                    service.EndTransaction(tran)
                    InitialiseNotification(NewSeizureNotification, Nothing)
                Else
                    service.EndTransaction(tran)
                End If
                Return Me
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.DoNothing)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSeizureNotification)
                Return Me
            End If
        End Function
#End Region

#Region " Validate "
        Public Overloads Function Validate(ByVal writeFlag As Boolean, ByVal ignoreWarnings As Boolean, ByVal saveApplication As Boolean) As BaseBO
            Me.Save(True)
            Me.ValidationErrors = Validate(writeFlag)
            Return Me
        End Function

        Public Overloads Overrides Function Validate(ByVal writeFlag As Boolean) As ValidationManager
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveSeizureNotification)

            MyBase.Validate(False)
            If MyBase.ValidationErrors.HasErrors Then
                If writeFlag Then Validated = False
            Else
                If writeFlag Then Validated = True
                MyBase.ValidationErrors = Nothing
            End If

            Return MyBase.ValidationErrors
        End Function
#End Region

#Region " Procedures "
        Public Overloads Function UnLinkFromPermit(ByVal permitId As Integer) As Boolean
            Dim LinkService As New DataObjects.Service.SeizureToPermitLinkService
            Return LinkService.DeleteById(New Int32() {mSeizureNotificationId, permitId}, 0)
        End Function

        Public Overloads Function LinkToPermit(ByVal permitId As Integer) As Boolean Implements IBOSeizureNotification.LinkToPermit
            Return LinkToPermit(New Int32() {permitId})
        End Function

        Public Overloads Function LinkToPermit(ByVal permitId() As Integer) As Boolean Implements IBOSeizureNotification.LinkToPermit
            Return LinkToPermit(permitId, False)
        End Function

        Public Overloads Function LinkToPermitRetrospectively(ByVal permitId As Integer) As Boolean Implements IBOSeizureNotification.LinkToPermitRetrospectively
            Return LinkToPermitRetrospectively(New Int32() {permitId})
        End Function

        Public Overloads Function LinkToPermitRetrospectively(ByVal permitId() As Integer) As Boolean Implements IBOSeizureNotification.LinkToPermitRetrospectively
            Return LinkToPermit(permitId, True)
        End Function

        Private Overloads Function LinkToPermit(ByVal permitId() As Integer, ByVal setRetrospective As Boolean) As Boolean Implements IBOSeizureNotification.LinkToPermit
            Dim LinkService As New DataObjects.Service.SeizureToPermitLinkService
            Dim tran As SqlClient.SqlTransaction = LinkService.BeginTransaction()

            Dim DOSeizureNotification As New DataObjects.Entity.SeizureNotification(mSeizureNotificationId)
            Dim SNToPermitSet As [DO].DataObjects.EntitySet.SeizureToPermitLinkSet = DOSeizureNotification.GetRelatedSeizureToPermitLink(tran)

            For Each item As [DO].DataObjects.Entity.SeizureToPermitLink In SNToPermitSet
                LinkService.DeleteById(New Int32() {mSeizureNotificationId, item.PermitId}, 0, tran)
            Next

            For Each id As Int32 In permitId
                LinkService.Insert(mSeizureNotificationId, id, tran)
                If setRetrospective Then
                    ' update the appropriate permit
                    Dim BOPermit As BOCITESPermit = BOCITESPermit.GetByPermitId(id)
                    If Not BOPermit.IsRetrospective Then
                        BOPermit.IsRetrospective = True
                        BOPermit = CType(BOPermit.Save(tran), BOCITESPermit)
                        If Not BOPermit.ValidationErrors Is Nothing Then
                            LinkService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                            LinkService = Nothing
                            Return False
                        End If
                    End If
                End If
            Next id
            LinkService.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            LinkService = Nothing
            Return True
        End Function

        Public Function DeleteLinkToPermit(ByVal permitId As Int32) As Boolean
            Dim LinkedService As New [DO].DataObjects.Service.SeizureToPermitLinkService
            Return LinkedService.DeleteById(New Int32() {Me.mSeizureNotificationId, permitId}, 0)
        End Function

        Public Function GetLinkedPermits() As BO.Application.BOPermit.ProgressPermitGrid()
            Dim Linked As New ArrayList
            Dim SeizureNotification As New [DO].DataObjects.Entity.SeizureNotification(Me.SeizureNotificationId)
            Dim LinkSet As [DO].DataObjects.EntitySet.SeizureToPermitLinkSet = SeizureNotification.GetRelatedSeizureToPermitLink()
            If Not LinkSet Is Nothing AndAlso LinkSet.Entities.Count > 0 Then
                Dim DOPermit As [DO].DataObjects.Entity.Permit
                Dim DOCitesPermitSet As [DO].DataObjects.EntitySet.CITESPermitSet
                Dim BOCitesPermit As BO.Application.CITES.BOCITESPermit
                Dim BOCitesApp As BO.Application.CITES.Applications.BOCITESApplication
                Dim DOApp As [DO].DataObjects.Entity.Application
                Dim DOCitesAppSet As [DO].DataObjects.EntitySet.CITESApplicationSet

                For Each Link As [DO].DataObjects.Entity.SeizureToPermitLink In LinkSet.Entities
                    DOPermit = New [DO].DataObjects.Entity.Permit(Link.PermitId, Nothing)
                    DOCitesPermitSet = DOPermit.GetRelatedCITESPermit
                    If Not DOCitesPermitSet Is Nothing AndAlso DOCitesPermitSet.Entities.Count > 0 Then
                        BOCitesPermit = New BO.Application.cites.BOCITESPermit(DOCitesPermitSet.Entities(0).CITESPermitId)
                        DOApp = New [DO].DataObjects.Entity.Application(DOPermit.ApplicationId)
                        DOCitesAppSet = DOApp.GetRelatedCITESApplication
                        If Not DOCitesAppSet Is Nothing AndAlso DOCitesAppSet.Entities.Count > 0 Then
                            BOCitesApp = New BO.Application.CITES.Applications.BOCITESApplication(DOCitesAppSet.Entities(0).CitesApplicationId)
                            For Each PI As BO.Application.BOPermitInfo In BOCitesPermit.GetPermitInfos(Nothing)
                                Linked.Add(New BO.Application.BOPermit.ProgressPermitGrid(BOCitesApp, True, PI, BOCitesPermit))
                            Next
                        End If
                    End If
                Next
                DOPermit = Nothing
                DOCitesPermitSet = Nothing
                BOCitesPermit = Nothing
                BOCitesApp = Nothing
                DOApp = Nothing
                DOCitesAppSet = Nothing

                Return CType(Linked.ToArray(GetType(BO.Application.BOPermit.ProgressPermitGrid)), BO.Application.BOPermit.ProgressPermitGrid())
            End If
        End Function
#End Region



    End Class
End Namespace