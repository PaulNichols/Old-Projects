Imports System.reflection
Public Class BaseDataBO
    Inherits BaseBO

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal id As Int32)
        MyClass.New(id, Nothing)
    End Sub

    Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
        MyClass.New()
        LoadById(id, tran)
    End Sub

    Protected Overridable Sub LoadById(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
        Dim Service As EnterpriseObjects.Service = GetService()

        If Not Service Is Nothing Then
            Dim Entity As EnterpriseObjects.Entity = CType(Service, EnterpriseObjects.IServiceId).GetByIdInternal(id, tran)
            If Not Entity Is Nothing Then
                ' all is well, so use the entity information to populate this object (me)
                ConvertDataObjectTOBO(Me, Entity)
                Return
            End If
        End If
        'either the service couldn't be instantiated or the record did not exists, throw it...
        Throw New RecordDoesNotExist(Service.ToString, id)
    End Sub

    Private mID As Integer
    <DOtoBOMapping("Id")> _
    Property ID() As Int32
        Get
            ID = mID
        End Get
        Set(ByVal Value As Int32)
            mID = Value
        End Set
    End Property

    Private Function GetService() As EnterpriseObjects.Service
        Dim ServiceTypes As Object() = Me.GetType.GetCustomAttributes(GetType(ServiceMapping), True)
        If Not ServiceTypes Is Nothing Then
            Dim ServiceType As Type = CType(ServiceTypes(0), ServiceMapping).Service
            Return CType(System.Activator.CreateInstance(ServiceType), EnterpriseObjects.Service)
        End If
    End Function

    Private Function GetEntity() As EnterpriseObjects.Entity
        Dim EntityTypes As Object() = Me.GetType.GetCustomAttributes(GetType(EntityMapping), True)
        If Not EntityTypes Is Nothing Then
            Dim EntityType As Type = CType(EntityTypes(0), EntityMapping).Entity
            Return CType(System.Activator.CreateInstance(EntityType), EnterpriseObjects.Entity)
        Else
            Return Nothing
        End If
    End Function

    Private ReadOnly Property GetCollectionType() As Type
        Get
            Dim CollectionTypes As Object() = Me.GetType.GetCustomAttributes(GetType(CollectionMapping), True)
            If Not CollectionTypes Is Nothing Then
                Return CType(CollectionTypes(0), CollectionMapping).Collection
            Else
                Throw New NotImplementedException("You must implement 'CollectionMapping'")
            End If
        End Get
    End Property

    Public Overloads Overrides Function Save() As BaseBO
        Return MyClass.Save(Nothing)
    End Function

    Public Overridable Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO 'MLD 11/1/5 pointless try/catch removed, minor tidy up
        MyBase.Save()
        Dim Entity As EnterpriseObjects.Entity = GetEntity()

        If Entity Is Nothing Then
            Return Nothing
        End If
        Entity.CreateEmptyEntity()
        Dim PIs() As Reflection.PropertyInfo = Me.GetType.GetProperties()
        For Each Pi As Reflection.PropertyInfo In PIs
            Dim Mappings As Object() = Pi.GetCustomAttributes(GetType(DOtoBOMapping), True)

            If Not Mappings Is Nothing Then
                If Mappings.Length > 0 Then
                    Dim FieldName As String = CType(Mappings(0), DOtoBOMapping).DataField
                    Dim DOProperty As Reflection.PropertyInfo = Entity.GetType.GetProperty(FieldName)
                    If Not DOProperty Is Nothing Then                       'MLD 13/1/5 may be Nothing if field does not exist in record, E.g. Editable
                        Dim Result As Object = Pi.GetValue(Me, Nothing)
                        Dim DONullMethod As Reflection.MethodInfo = Entity.GetType.GetMethod("Set" & FieldName & "ToNull")

                        If Not DONullMethod Is Nothing AndAlso (Result Is Nothing OrElse Result Is Convert.DBNull) Then
                            DONullMethod.Invoke(Entity, Nothing)
                        Else
                            If TypeOf Result Is Boolean AndAlso DOProperty.PropertyType.Name = "Int32" Then 'MLD 13/1/5 special case to avoid "widening" exception
                                If CType(Result, Boolean) Then
                                    DOProperty.SetValue(Entity, 1, Nothing)
                                Else
                                    DOProperty.SetValue(Entity, 0, Nothing)
                                End If
                            Else If FieldName = "UserRole" Then
                                'MLD 18/1/5 temporary fix to stop problem where Int64 squashed into Int32 (ScientificAdvice) table
                            Else
                                DOProperty.SetValue(Entity, Result, Nothing)
                            End If
                        End If
                    End If
                End If
            End If
        Next Pi
        'we should have a nicely setup dataobject
        Dim Service As EnterpriseObjects.Service = GetService()
        Dim NewEntity As EnterpriseObjects.Entity = SaveEntity(Entity, Service, tran)
        If NewEntity.CheckSum <> CheckSum Then
            'no point in initialising unless things have changed
            Return ConvertDataObjectTOBO(NewEntity)
        End If
        'as things are the same, return the saved object
        Return Me
    End Function

    Friend Sub PreSave(ByVal oldEntity As EnterpriseObjects.Entity, ByVal tran As SqlClient.SqlTransaction)
    End Sub

    Friend Sub PostSave(ByVal newEntity As EnterpriseObjects.Entity, ByVal tran As SqlClient.SqlTransaction)
    End Sub


    Private Function SaveEntity(ByVal entity As EnterpriseObjects.Entity, ByVal service As EnterpriseObjects.Service) As EnterpriseObjects.Entity
        Return SaveEntity(entity, service, Nothing)
    End Function

    Private Function SaveEntity(ByVal entity As EnterpriseObjects.Entity, ByVal service As EnterpriseObjects.Service, ByVal tran As SqlClient.SqlTransaction) As EnterpriseObjects.Entity
        PreSave(entity, tran)

        Dim ThisEntity As EnterpriseObjects.Entity

        Try
            Created = (entity.Id = 0)
            If Created Then
                ThisEntity = CType(service.GetType.InvokeMember("Insert", BindingFlags.InvokeMethod, Nothing, service, New Object() {entity}), EnterpriseObjects.Entity)
            Else
                ThisEntity = CType(service.GetType.InvokeMember("Update", BindingFlags.InvokeMethod, Nothing, service, New Object() {entity}), EnterpriseObjects.Entity)
            End If
        Catch ex As Exception
            ' An Unexpected Database Error occurred
            Throw New Exception("DBError")
        End Try

        PostSave(ThisEntity, tran)

        If ThisEntity Is Nothing Then   'MLD 11/1/5 refactored
            If Not tran Is Nothing Then service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            If DataObjects.Sprocs.LastError Is Nothing Then
                Throw New Exception("DBDuplicate")
            Else
                Dim msg As String = DataObjects.Sprocs.LastError.ErrorMessage
                If msg.IndexOf("Cannot insert duplicate") >= 0 Then
                    Throw New Exception("DBDuplicate")
                Else
                    Throw New Exception("DBUnknown")
                End If
            End If
        Else
            'Everything Fine.
            If Created Then mID = ThisEntity.Id

            Dim ReturnedError As String
            If CompareEntitiesForUpdates(entity, ThisEntity, ReturnedError) Then
                Return ThisEntity
            Else
                Throw New Exception(ReturnedError)
            End If

        End If

    End Function

    Private Function CompareEntitiesForUpdates(ByVal entity As EnterpriseObjects.Entity, ByVal thisEntity As EnterpriseObjects.Entity, ByRef errorString As String) As Boolean
        errorString = "Unknown"
        Dim Index As Integer = 0 ' 0 is ID
        Dim RC As Boolean = True
        If thisEntity.Id = entity.Id Then ' Insert throws error on failure.  Update doesn't
            Do
                Try
                    If String.Compare(thisEntity.Data(Index).ToString, entity.Data(Index).ToString, True) <> 0 Then
                        ' The data is different!  Means returned different information.
                        ' Excluding ID from retrieval, so can only be that update failed.
                        ' therefore throw duplication error.
                        ' Checksum will be different though
                        ' Could also be boolean - Entity shows False/True, thisEntity shows 0/1
                        Try
                            Select Case Type.GetTypeCode(entity.Data(Index).GetType)
                                Case TypeCode.Boolean
                                    ' Generally Booleans can be 0/1 or True False
                                    ' All else should match regardless.
                                    Dim Check1 As Boolean = entity.Data(Index).ToString = "1" OrElse entity.Data(Index).ToString.ToUpper = "TRUE"
                                    Dim Check2 As Boolean = thisEntity.Data(Index).ToString = "1" OrElse thisEntity.Data(Index).ToString.ToUpper = "TRUE"
                                    If Check1 <> Check2 Then
                                        ' Not the same thing - Database couldn't save it!
                                        errorString = "DBDuplicate"
                                        RC = False
                                        Exit Do
                                    End If
                                Case Else

                                    Dim blnIsNumeric As Boolean = False
                                    Try
                                        Dim decNum As Decimal = Decimal.Parse(entity.Data(Index).ToString)
                                        decNum = Decimal.Parse(thisEntity.Data(Index).ToString)
                                        blnIsNumeric = True
                                    Catch ex As Exception

                                    End Try
                                    If blnIsNumeric Then
                                        If Decimal.Compare(Decimal.Parse(entity.Data(Index).ToString), Decimal.Parse(thisEntity.Data(Index).ToString)) <> 0 Then
                                            ' Data is different
                                            ' Check if it is the Checksum
                                            Try
                                                ' Checksum is last piece of Data - should not be anything else
                                                If (thisEntity.Data(Index + 1).ToString.Length >= 0) Then
                                                    ' There is another record.
                                                    errorString = "DBDuplicate"
                                                    RC = False
                                                    Exit Do
                                                End If
                                            Catch ex As Exception
                                                ' Didn't exist, therefore Checksum
                                                Exit Do ' No Error, as Checksum.
                                            End Try

                                        End If
                                    Else
                                        ' Is it a Date
                                        Dim blnIsDate As Boolean = False
                                        Try
                                            Dim datDate As Date = Date.Parse(entity.Data(Index).ToString)
                                            datDate = Date.Parse(thisEntity.Data(Index).ToString)
                                            blnIsNumeric = True
                                        Catch ex As Exception

                                        End Try
                                        If blnIsDate = True Then
                                            If Date.Compare(Date.Parse(entity.Data(Index).ToString), Date.Parse(thisEntity.Data(Index).ToString)) <> 0 Then
                                                ' Data is different
                                                ' There is another record.
                                                        errorString = "DBDuplicate"
                                                        RC = False
                                                        Exit Do
                                            End If
                                        Else
                                            ' All that is left is the String, and that is different.
                                            errorString = "DBDuplicate"
                                            RC = False
                                            Exit Do
                                        End If
                                    End If

                            End Select
                        Catch ex As Exception

                        End Try
                    End If
                Catch ex As Exception
                    ' Probably just that ran out of data
                    Exit Do
                End Try
                Index += 1
            Loop
        Else
            RC = (thisEntity.Id > 0 AndAlso entity.Id = 0) ' Insert so fine.
        End If
        Return RC
    End Function

    Public Overridable Function GetLookupData(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal ssouserid As Int64, ByVal ssouserrole As Integer) As BaseDataBO()
        Dim Service As EnterpriseObjects.Service = GetService()
        Dim EntitySet As EnterpriseObjects.EntitySet = GetLookupDataEntitySet(Service, includeHyphen, includeInactive)
        Return ConvertDataObjectToArray(EntitySet)
        'MLD 12/1/5 loony try/catch removed
    End Function

    Protected Overridable Function GetComparer() As IComparer  'MLD 21/1/5 added
        Return Nothing
    End Function

    Protected Overridable Function IsFiltered() As Boolean  'MLD 11/1/5 added
        Return False
    End Function

    Protected Overridable Function PassFilter(ByVal entity As BaseDataBO) As Boolean  'MLD 11/1/5 added
        Return True
    End Function

    Protected Overridable Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
        Return CType(service, EnterpriseObjects.IServiceAll).GetAllInternal(includeHyphen, includeInactive)
    End Function

    Public Function GetAll() As EnterpriseObjects.EntityBoundCollection ' [DO].DataObjects.Collection.CountryBoundCollection
        Return GetAll(False)
    End Function

    Public Function GetAll(ByVal includeHyphen As Boolean) As EnterpriseObjects.EntityBoundCollection  ' [DO].DataObjects.Collection.CountryBoundCollection
        Return GetAll(includeHyphen, False)
    End Function

    Public Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntityBoundCollection ' [DO].DataObjects.Collection.CountryBoundCollection
        Try
            Dim Service As EnterpriseObjects.Service = GetService()
            If Not Service Is Nothing Then
                Dim ES As EnterpriseObjects.EntitySet = CType(Service, EnterpriseObjects.IServiceAll).GetAllInternal(includeHyphen, includeInactive)
                Return ES.GetBoundCollection(ES, 0, GetCollectionType)
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetAllBO() As BaseDataBO()
        Return GetAllBO(False)
    End Function

    Public Function GetAllBO(ByVal includeHyphen As Boolean) As BaseDataBO()
        Return GetAllBO(includeHyphen, False)
    End Function

    Public Function GetAllBO(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As BaseDataBO()
        Try
            Dim Service As EnterpriseObjects.Service = GetService()
            If Not Service Is Nothing Then
                Dim ES As EnterpriseObjects.EntitySet = CType(Service, EnterpriseObjects.IServiceAll).GetAllInternal(includeHyphen, includeInactive)
                Return ConvertDataObjectToArray(ES)
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function GetValueFromFieldName(ByVal dataobj As EnterpriseObjects.Entity, ByVal fieldName As String) As Object
        Try
            Dim prop As Reflection.PropertyInfo = dataobj.GetType.GetProperty(fieldName)
            Return prop.GetValue(dataobj, Nothing)
        Catch
            Return Nothing
        End Try
    End Function

    Friend Function ConvertDataObjectTOBO(ByVal dataobj As EnterpriseObjects.Entity) As BO.BaseDataBO
        Return ConvertDataObjectTOBO(CType(System.Activator.CreateInstance(Me.GetType), BaseDataBO), dataobj)
    End Function

    Friend Function ConvertDataObjectTOBO(ByRef bo As BaseDataBO, ByVal dataobj As EnterpriseObjects.Entity) As BO.BaseDataBO
        Dim PIs() As Reflection.PropertyInfo = bo.GetType.GetProperties()
        For Each Pi As Reflection.PropertyInfo In PIs
            Dim Mappings As Object() = Pi.GetCustomAttributes(GetType(DOtoBOMapping), True)
            If Not Mappings Is Nothing Then
                If Mappings.Length > 0 Then
                    Dim FieldName As String = CType(Mappings(0), DOtoBOMapping).DataField
                    Dim Result As Object = GetValueFromFieldName(dataobj, FieldName)
                    Dim DONullMethod As Reflection.MethodInfo = dataobj.GetType.GetMethod("Is" & FieldName & "Null")

                    If Not Result Is Nothing    'MLD 18/1/5 may be nothing if field does not exist in either BO or DO
                        If Not DONullMethod Is Nothing AndAlso CType(DONullMethod.Invoke(dataobj, Nothing), Boolean) Then
                            Pi.SetValue(bo, Nothing, Nothing)   'set to Nothing if Null in the record
                        Else
                            If TypeOf Result Is Integer AndAlso Pi.PropertyType.Name = "Boolean" Then
                                Pi.SetValue(bo, CType(result, Integer) > 0, Nothing)    'MLD 12/1/5 special case to avoid "widening" exception
                            Else
                                Pi.SetValue(bo, Result, Nothing)    'copy result from record to BO
                            End If
                        End If
                    End If
                End If
            End If
        Next Pi
        Return bo
    End Function

    Protected Function ConvertDataObjectToArray(ByVal entityset As EnterpriseObjects.EntitySet) As BaseDataBO() 'MLD 11/1/5 filter added
        Dim results(entityset.Count - 1) As BaseDataBO
        Dim comparer As IComparer = GetComparer()
        Dim index As Int32 = 0
        For Each entity As EnterpriseObjects.Entity In entityset
            Dim bo As BaseDataBO = ConvertDataObjectTOBO(entity)
            If PassFilter(bo) Then
                results(index) = bo
                index += 1
            End If
        Next entity
        If IsFiltered() Then
            Redim Preserve results(index - 1)
        End If
        If Not comparer Is Nothing Then
            Array.Sort(results, comparer)
        End If
        Return results
    End Function

    Public Shared Function GetReferenceDataObject(ByVal ReferenceDataObject As String) As ReferenceData.BOBaseReferenceTable
        Dim a As [Assembly] = [Assembly].Load("uk.gov.defra.Phoenix.BO")
        Dim sTypeString As String = "uk.gov.defra.Phoenix.BO." & ReferenceDataObject
        Dim aType As Type = a.GetType(sTypeString)
        If aType Is Nothing Then
            Throw New Exception(sTypeString + " does not exist in the uk.gov.defra.Phoenix.BO assembly. Has the spelling changed?")
        End If
        Return CType(System.Activator.CreateInstance(aType), ReferenceData.BOBaseReferenceTable)
    End Function

    Public Overridable Function Validate() As Boolean
        MyBase.ValidationErrors = Nothing
        'if you want to raise validation errors:
        'call SetNewValidationManager() and then add messages to MyBase.ValidationErrors.
        Return True
    End Function
End Class
