Namespace Application
    Public Class BOSpecimenMark
        Inherits BaseBO
        Implements IBOSpecimenMark

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal idMarkTypeId As Int32, ByVal specimenId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadSpecimenMark(SpecimenMarkId, tran)
        End Sub

        Protected Overridable Function LoadSpecimenMark(ByVal specimenMarkId As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.SpecimenIDMark
            Dim NewSpecimenMark As DataObjects.Entity.SpecimenIDMark = DataObjects.Entity.SpecimenIDMark.GetById(specimenMarkId, tran)
            If NewSpecimenMark Is Nothing Then
                Throw New RecordDoesNotExist("Specimen Mark", specimenMarkId)
            Else
                InitialiseSpecimenMark(NewSpecimenMark, tran)
                Return NewSpecimenMark
            End If
        End Function

        Friend Overridable Sub InitialiseSpecimenMark(ByVal specimenMark As DataObjects.Entity.SpecimenIDMark, ByVal tran As SqlClient.SqlTransaction)
            With specimenMark
                mSpecimenMarkId = .Id
                mMarkCheckSum = .CheckSum

                mIDMark = .IdMark
                mIdMarkType = New ReferenceData.BOIDMarkType(.IDMarkTypeId, tran)
                mSpecimenId = .SpecimenId
            End With
        End Sub
#End Region

#Region " Properties "

        Public Property MarkCheckSum() As Integer Implements IBOSpecimenMark.MarkCheckSum
            Get
                Return mMarkCheckSum
            End Get
            Set(ByVal Value As Integer)
                mMarkCheckSum = Value
            End Set
        End Property
        Private mMarkCheckSum As Int32

        Public Property SpecimenMarkId() As Integer Implements IBOSpecimenMark.SpecimenMarkId
            Get
                Return mSpecimenMarkId
            End Get
            Set(ByVal Value As Integer)
                mSpecimenMarkId = Value
            End Set
        End Property
        Private mSpecimenMarkId As Int32

        Public Property SpecimenId() As Integer Implements IBOSpecimenMark.SpecimenId
            Get
                Return mSpecimenId
            End Get
            Set(ByVal Value As Integer)
                mSpecimenId = Value
            End Set
        End Property
        Private mSpecimenId As Int32

        Public Property IdMark() As String Implements IBOSpecimenMark.IdMark
            Get
                Return mIDMark
            End Get
            Set(ByVal Value As String)
                mIDMark = Value
            End Set
        End Property
        Private mIDMark As String

        Public Property IdMarkType() As ReferenceData.BOIDMarkType Implements IBOSpecimenMark.IdMarkType
            Get
                Return mIdMarkType
            End Get
            Set(ByVal Value As ReferenceData.BOIDMarkType)
                mIdMarkType = Value
            End Set
        End Property
        Private mIdMarkType As ReferenceData.BOIDMarkType
#End Region

#Region " Helper Functions "

        Public Property MarkTypeDescription() As String Implements IBOSpecimenMark.MarkTypeDescription
            Get
                If Not mIdMarkType Is Nothing AndAlso mIdMarkType.ID <> 0 Then Return mIdMarkType.Description
            End Get
            Set(ByVal Value As String)

            End Set
        End Property
#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Return Save(Nothing)
        End Function

        Public Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO

            MyBase.Save()
            Dim NewMark As New DataObjects.Entity.SpecimenIDMark
            Dim service As DataObjects.Service.SpecimenIDMarkService = NewMark.ServiceObject

            Created = (mSpecimenMarkId = 0)

            If Created Then
                NewMark = service.Insert(mIdMarkType.ID, _
                                         mSpecimenId, _
                                         mIDMark, _
                                         Nothing, _
                                         tran)
            Else
                NewMark = service.Update(mSpecimenMarkId, _
                                         mIdMarkType.ID, _
                                         mSpecimenId, _
                                         mIDMark, _
                                        Nothing, _
                                         mMarkCheckSum, _
                                         tran)
            End If
            'check to see if any SQL errors have occured
            If NewMark Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
                'TODO: Use errors collection to check to see if the problem was concurrency
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSpecimenMark)
            ElseIf NewMark Is Nothing Then
                If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSpecimenMark)
            ElseIf Created And Not NewMark Is Nothing Then
                mSpecimenId = NewMark.Id
            End If

            If Not NewMark Is Nothing Then
                If NewMark.CheckSum <> mMarkCheckSum Then
                    'no point in initialising unless things have changed
                    InitialiseSpecimenMark(NewMark, tran)
                End If
            End If
            Return Me
        End Function
#End Region

#Region " Validate "
        Public Function Validate() As ValidationManager Implements IBOSpecimenMark.Validate
            ' init the errors list
            MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveImportSpecie)

            GetValidationErrors()

            If Not MyBase.ValidationErrors.HasErrors Then
                MyBase.ValidationErrors = Nothing
            End If

            Return MyBase.ValidationErrors
        End Function

        Protected Overridable Sub GetValidationErrors()
        End Sub
#End Region

#Region "Operations"
        Public Shared Function GetRelatedSpecimens(ByVal idNumber As String, ByVal markTypeId As Int32, ByVal tran As SqlClient.SqlTransaction) As BO.Application.BOSpecimen()
            Dim SpecimenIDMarkService As New [DO].DataObjects.Service.SpecimenIDMarkService
            Dim MarkSet As DataObjects.EntitySet.SpecimenIDMarkSet = SpecimenIDMarkService.GetByIndex_IdMarkAndType(idNumber, markTypeId)


            If Not MarkSet Is Nothing AndAlso MarkSet.Entities.Count > 0 Then
                Dim UniqueSpecs As New System.Collections.Hashtable
                For Each DOSpecMark As DataObjects.Entity.SpecimenIDMark In MarkSet.Entities
                    If Not UniqueSpecs.ContainsKey(DOSpecMark.SpecimenId) Then
                        UniqueSpecs.Add(DOSpecMark.SpecimenId, New BO.Application.BOSpecimen(DOSpecMark.SpecimenId, tran))
                    End If
                Next

                Dim ReturnSpecs(UniqueSpecs.Count - 1) As BO.Application.BOSpecimen
                Dim SpecEnum As Collections.IEnumerator = UniqueSpecs.GetEnumerator
                Dim i As Int32 = 0
                Do While (SpecEnum.MoveNext)
                    CType(CType(SpecEnum.Current, System.Collections.DictionaryEntry).Value, BO.Application.BOSpecimen).UOM = New BOMeasurement
                    ReturnSpecs(i) = CType(CType(SpecEnum.Current, System.Collections.DictionaryEntry).Value, BO.Application.BOSpecimen)
                    i += 1
                Loop
                SpecEnum = Nothing
                Return ReturnSpecs

            End If
        End Function

        Public Shared Function DeleteById(ByVal id As Int32) As Boolean
            Return [DO].DataObjects.Entity.SpecimenIDMark.DeleteById(id)
        End Function

        Public Overrides Function Delete() As Object
            Return [DO].DataObjects.Entity.SpecimenIDMark.DeleteById(Me.SpecimenMarkId)
        End Function

#End Region

    End Class
End Namespace