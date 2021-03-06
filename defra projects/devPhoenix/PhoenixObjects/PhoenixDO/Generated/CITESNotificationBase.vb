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
    
    'Base entity implementation for table 'CITESNotification'
    '*DO NOT* modify this file.
    'Add new properties and methods to CITESNotification instead.
    Public MustInherit Class CITESNotificationBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal cITESNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(cITESNotificationId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal cITESNotificationId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(cITESNotificationId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property CITESNotificationId As Integer
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
        
        Public Property MemberStateOfImportId As Integer
            Get
                If (Me.IsMemberStateOfImportIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),Integer)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property DateOfImport As Date
            Get
                Return CType(Me(2),Date)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property CountryOfOriginId As Integer
            Get
                If (Me.IsCountryOfOriginIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Integer)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property ExportedCountryId As Integer
            Get
                If (Me.IsExportedCountryIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property AgentPartyLinkId As Integer
            Get
                If (Me.IsAgentPartyLinkIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Integer)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property Active As Boolean
            Get
                Return CType(Me(6),Boolean)
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Property Validated As Boolean
            Get
                Return CType(Me(7),Boolean)
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property PartyLinkId As Integer
            Get
                If (Me.IsPartyLinkIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),Integer)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(4)>  _
        Public Property ReferenceNumber As String
            Get
                If (Me.IsReferenceNumberNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(9),String)
                End If
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        Public Property DateUnknown As Boolean
            Get
                If (Me.IsDateUnknownNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(10),Boolean)
                End If
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        Public Property ReceivedDate As Date
            Get
                If (Me.IsReceivedDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(11),Date)
                End If
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        Public Property UnknownCountryOfExport As Boolean
            Get
                If (Me.IsUnknownCountryOfExportNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(12),Boolean)
                End If
            End Get
            Set
                Me(12) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(13),Integer)
                End If
            End Get
            Set
                Me(13) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.CITESNotificationService
            Get
                Return CType(GetServiceObject(GetType(Service.CITESNotificationService)),Service.CITESNotificationService)
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
        
        Public Function IsMemberStateOfImportIdNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetMemberStateOfImportIdToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsCountryOfOriginIdNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetCountryOfOriginIdToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsExportedCountryIdNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetExportedCountryIdToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsAgentPartyLinkIdNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetAgentPartyLinkIdToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsPartyLinkIdNull() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetPartyLinkIdToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Function IsReferenceNumberNull() As Boolean
            Return Me.IsNull(9)
        End Function
        
        Public Sub SetReferenceNumberToNull()
            Me(9) = System.DBNull.Value
        End Sub
        
        Public Function IsDateUnknownNull() As Boolean
            Return Me.IsNull(10)
        End Function
        
        Public Sub SetDateUnknownToNull()
            Me(10) = System.DBNull.Value
        End Sub
        
        Public Function IsReceivedDateNull() As Boolean
            Return Me.IsNull(11)
        End Function
        
        Public Sub SetReceivedDateToNull()
            Me(11) = System.DBNull.Value
        End Sub
        
        Public Function IsUnknownCountryOfExportNull() As Boolean
            Return Me.IsNull(12)
        End Function
        
        Public Sub SetUnknownCountryOfExportToNull()
            Me(12) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(13)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(13) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(14)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.CITESNotificationSet
            Return CITESNotificationBase.GetAll(false, false, CITESNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.CITESNotificationSet
            Return CITESNotificationBase.GetAll(includeHyphen, false, CITESNotificationServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As CITESNotificationServiceBase.OrderBy) As EntitySet.CITESNotificationSet
            Dim service As Service.CITESNotificationService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As CITESNotificationServiceBase.OrderBy) As EntitySet.CITESNotificationSet
            Return CITESNotificationBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal cITESNotificationId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.CITESNotification
            Dim service As Service.CITESNotificationService
            service = ServiceObject
            Return service.GetById(CITESNotificationId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal cITESNotificationId As Integer) As Entity.CITESNotification
            Dim service As Service.CITESNotificationService
            service = ServiceObject
            Return service.GetById(CITESNotificationId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESNotificationId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.CITESNotificationService
            service = ServiceObject
            Return service.DeleteById(cITESNotificationId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESNotificationId As Integer) As Boolean
            Return CITESNotificationBase.DeleteById(cITESNotificationId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESNotificationId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return CITESNotificationBase.DeleteById(cITESNotificationId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForMemberStateOfImportIdCountry(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Dim service As Service.CITESNotificationService
            service = ServiceObject
            Return service.GetForMemberStateOfImportIdCountry(countryId, tran)
        End Function
        
        Public Overloads Shared Function GetForMemberStateOfImportIdCountry(ByVal countryId As Integer) As EntitySet.CITESNotificationSet
            Return CITESNotificationBase.GetForMemberStateOfImportIdCountry(countryId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForCountryOfOriginIdCountry(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Dim service As Service.CITESNotificationService
            service = ServiceObject
            Return service.GetForCountryOfOriginIdCountry(countryId, tran)
        End Function
        
        Public Overloads Shared Function GetForCountryOfOriginIdCountry(ByVal countryId As Integer) As EntitySet.CITESNotificationSet
            Return CITESNotificationBase.GetForCountryOfOriginIdCountry(countryId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForExportedCountryIdCountry(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Dim service As Service.CITESNotificationService
            service = ServiceObject
            Return service.GetForExportedCountryIdCountry(countryId, tran)
        End Function
        
        Public Overloads Shared Function GetForExportedCountryIdCountry(ByVal countryId As Integer) As EntitySet.CITESNotificationSet
            Return CITESNotificationBase.GetForExportedCountryIdCountry(countryId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForAgentPartyLinkIdPartyLink(ByVal partyLinkId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Dim service As Service.CITESNotificationService
            service = ServiceObject
            Return service.GetForAgentPartyLinkIdPartyLink(partyLinkId, tran)
        End Function
        
        Public Overloads Shared Function GetForAgentPartyLinkIdPartyLink(ByVal partyLinkId As Integer) As EntitySet.CITESNotificationSet
            Return CITESNotificationBase.GetForAgentPartyLinkIdPartyLink(partyLinkId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForPartyLinkIdPartyLink(ByVal partyLinkId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESNotificationSet
            Dim service As Service.CITESNotificationService
            service = ServiceObject
            Return service.GetForPartyLinkIdPartyLink(partyLinkId, tran)
        End Function
        
        Public Overloads Shared Function GetForPartyLinkIdPartyLink(ByVal partyLinkId As Integer) As EntitySet.CITESNotificationSet
            Return CITESNotificationBase.GetForPartyLinkIdPartyLink(partyLinkId, Nothing)
        End Function
        
        Public Overloads Function GetRelatedImportNotification(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportNotificationSet
            Return Entity.ImportNotification.GetForCITESNotification(Me.CITESNotificationId, tran)
        End Function
        
        Public Overloads Function GetRelatedImportNotification() As EntitySet.ImportNotificationSet
            Return Me.GetRelatedImportNotification(Nothing)
        End Function
        
        Public Overloads Function GetRelatedNotificationSpecieLink(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.NotificationSpecieLinkSet
            Return Entity.NotificationSpecieLink.GetForCITESNotification(Me.CITESNotificationId, tran)
        End Function
        
        Public Overloads Function GetRelatedNotificationSpecieLink() As EntitySet.NotificationSpecieLinkSet
            Return Me.GetRelatedNotificationSpecieLink(Nothing)
        End Function
        
        Public Overloads Function GetRelatedSeizureNotification(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SeizureNotificationSet
            Return Entity.SeizureNotification.GetForCITESNotification(Me.CITESNotificationId, tran)
        End Function
        
        Public Overloads Function GetRelatedSeizureNotification() As EntitySet.SeizureNotificationSet
            Return Me.GetRelatedSeizureNotification(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal memberStateOfImportId As Object, ByVal dateOfImport As Date, ByVal countryOfOriginId As Object, ByVal exportedCountryId As Object, ByVal agentPartyLinkId As Object, ByVal active As Boolean, ByVal validated As Boolean, ByVal partyLinkId As Object, ByVal referenceNumber As Object, ByVal dateUnknown As Object, ByVal receivedDate As Object, ByVal unknownCountryOfExport As Object) As Entity.CITESNotification
            Return Entity.CITESNotification.ServiceObject.Insert(memberStateOfImportId, dateOfImport, countryOfOriginId, exportedCountryId, agentPartyLinkId, active, validated, partyLinkId, referenceNumber, dateUnknown, receivedDate, unknownCountryOfExport)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim memberStateOfImportIdParam As Object
            If (Me.IsMemberStateOfImportIdNull = false) Then
                memberStateOfImportIdParam = Me.MemberStateOfImportId
            Else
                memberStateOfImportIdParam = System.DBNull.Value
            End If
            Dim dateOfImportParam As Date = Me.DateOfImport
            Dim countryOfOriginIdParam As Object
            If (Me.IsCountryOfOriginIdNull = false) Then
                countryOfOriginIdParam = Me.CountryOfOriginId
            Else
                countryOfOriginIdParam = System.DBNull.Value
            End If
            Dim exportedCountryIdParam As Object
            If (Me.IsExportedCountryIdNull = false) Then
                exportedCountryIdParam = Me.ExportedCountryId
            Else
                exportedCountryIdParam = System.DBNull.Value
            End If
            Dim agentPartyLinkIdParam As Object
            If (Me.IsAgentPartyLinkIdNull = false) Then
                agentPartyLinkIdParam = Me.AgentPartyLinkId
            Else
                agentPartyLinkIdParam = System.DBNull.Value
            End If
            Dim activeParam As Boolean = Me.Active
            Dim validatedParam As Boolean = Me.Validated
            Dim partyLinkIdParam As Object
            If (Me.IsPartyLinkIdNull = false) Then
                partyLinkIdParam = Me.PartyLinkId
            Else
                partyLinkIdParam = System.DBNull.Value
            End If
            Dim referenceNumberParam As Object
            If (Me.IsReferenceNumberNull = false) Then
                referenceNumberParam = Me.ReferenceNumber
            Else
                referenceNumberParam = System.DBNull.Value
            End If
            Dim dateUnknownParam As Object
            If (Me.IsDateUnknownNull = false) Then
                dateUnknownParam = Me.DateUnknown
            Else
                dateUnknownParam = System.DBNull.Value
            End If
            Dim receivedDateParam As Object
            If (Me.IsReceivedDateNull = false) Then
                receivedDateParam = Me.ReceivedDate
            Else
                receivedDateParam = System.DBNull.Value
            End If
            Dim unknownCountryOfExportParam As Object
            If (Me.IsUnknownCountryOfExportNull = false) Then
                unknownCountryOfExportParam = Me.UnknownCountryOfExport
            Else
                unknownCountryOfExportParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.CITESNotification.ServiceObject.Update(Me.Id, memberStateOfImportIdParam, dateOfImportParam, countryOfOriginIdParam, exportedCountryIdParam, agentPartyLinkIdParam, activeParam, validatedParam, partyLinkIdParam, referenceNumberParam, dateUnknownParam, receivedDateParam, unknownCountryOfExportParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
