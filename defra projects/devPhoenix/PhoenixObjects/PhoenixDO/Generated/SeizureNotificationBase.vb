'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.2032
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Base
    
    'Base entity implementation for table 'SeizureNotification'
    '*DO NOT* modify this file.
    'Add new properties and methods to SeizureNotification instead.
    Public MustInherit Class SeizureNotificationBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal seizureNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(seizureNotificationId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal seizureNotificationId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(seizureNotificationId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property SeizureNotificationId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Overrides Property Id As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property CustomsReference As String
            Get
                If (Me.IsCustomsReferenceNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),String)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(255)>  _
        Public Property SeizureReason As String
            Get
                If (Me.IsSeizureReasonNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),String)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property PortOfEntryID As Integer
            Get
                If (Me.IsPortOfEntryIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Integer)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property CITESNotificationId As Integer
            Get
                Return CType(Me(4),Integer)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property Type As Integer
            Get
                If (Me.IsTypeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Integer)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.SeizureNotificationService
            Get
                Return CType(GetServiceObject(GetType(Service.SeizureNotificationService)),Service.SeizureNotificationService)
            End Get
        End Property
        
        Public Overridable Property RawDataset As System.Data.DataSet Implements EnterpriseObjects.IUpdatable.RawDataset
            Get
                Return mRawDataset
            End Get
            Set
                mRawDataset = value
            End Set
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsCustomsReferenceNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetCustomsReferenceToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsSeizureReasonNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetSeizureReasonToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsPortOfEntryIDNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetPortOfEntryIDToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsTypeNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetTypeToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(7)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.SeizureNotificationSet
            Return SeizureNotificationBase.GetAll(false, false, SeizureNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.SeizureNotificationSet
            Return SeizureNotificationBase.GetAll(includeHyphen, false, SeizureNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As SeizureNotificationServiceBase.OrderBy) As EntitySet.SeizureNotificationSet
            Dim service As Service.SeizureNotificationService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As SeizureNotificationServiceBase.OrderBy) As EntitySet.SeizureNotificationSet
            Return SeizureNotificationBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal seizureNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.SeizureNotification
            Dim service As Service.SeizureNotificationService
            service = ServiceObject
            Return service.GetById(SeizureNotificationId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal seizureNotificationId As Integer) As Entity.SeizureNotification
            Dim service As Service.SeizureNotificationService
            service = ServiceObject
            Return service.GetById(SeizureNotificationId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal seizureNotificationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.SeizureNotificationService
            service = ServiceObject
            Return service.DeleteById(seizureNotificationId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal seizureNotificationId As Integer) As Boolean
            Return SeizureNotificationBase.DeleteById(seizureNotificationId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal seizureNotificationId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return SeizureNotificationBase.DeleteById(seizureNotificationId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForPortOfEntry(ByVal portOfEntryID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SeizureNotificationSet
            Dim service As Service.SeizureNotificationService
            service = ServiceObject
            Return service.GetForPortOfEntry(portOfEntryID, tran)
        End Function
        
        Public Overloads Shared Function GetForPortOfEntry(ByVal portOfEntryID As Integer) As EntitySet.SeizureNotificationSet
            Return SeizureNotificationBase.GetForPortOfEntry(portOfEntryID, Nothing)
        End Function
        
        Public Overloads Shared Function GetForCITESNotification(ByVal cITESNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SeizureNotificationSet
            Dim service As Service.SeizureNotificationService
            service = ServiceObject
            Return service.GetForCITESNotification(cITESNotificationId, tran)
        End Function
        
        Public Overloads Shared Function GetForCITESNotification(ByVal cITESNotificationId As Integer) As EntitySet.SeizureNotificationSet
            Return SeizureNotificationBase.GetForCITESNotification(cITESNotificationId, Nothing)
        End Function
        
        Public Overloads Function GetRelatedSeizureToPermitLink(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SeizureToPermitLinkSet
            Return Entity.SeizureToPermitLink.GetForSeizureNotification(Me.SeizureNotificationId, tran)
        End Function
        
        Public Overloads Function GetRelatedSeizureToPermitLink() As EntitySet.SeizureToPermitLinkSet
            Return Me.GetRelatedSeizureToPermitLink(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal customsReference As Object, ByVal seizureReason As Object, ByVal portOfEntryID As Object, ByVal cITESNotificationId As Integer, ByVal type As Object) As Entity.SeizureNotification
            Return Entity.SeizureNotification.ServiceObject.Insert(customsReference, seizureReason, portOfEntryID, cITESNotificationId, type)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim customsReferenceParam As Object
            If (Me.IsCustomsReferenceNull = false) Then
                customsReferenceParam = EnterpriseObjects.Common.ParseSQLText(Me.CustomsReference)
            Else
                customsReferenceParam = System.DBNull.Value
            End If
            Dim seizureReasonParam As Object
            If (Me.IsSeizureReasonNull = false) Then
                seizureReasonParam = EnterpriseObjects.Common.ParseSQLText(Me.SeizureReason)
            Else
                seizureReasonParam = System.DBNull.Value
            End If
            Dim portOfEntryIDParam As Object
            If (Me.IsPortOfEntryIDNull = false) Then
                portOfEntryIDParam = Me.PortOfEntryID
            Else
                portOfEntryIDParam = System.DBNull.Value
            End If
            Dim cITESNotificationIdParam As Integer = Me.CITESNotificationId
            Dim typeParam As Object
            If (Me.IsTypeNull = false) Then
                typeParam = Me.Type
            Else
                typeParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.SeizureNotification.ServiceObject.Update(Me.Id, customsReferenceParam, seizureReasonParam, portOfEntryIDParam, cITESNotificationIdParam, typeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace