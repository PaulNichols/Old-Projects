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
    
    'Base entity implementation for table 'Message'
    '*DO NOT* modify this file.
    'Add new properties and methods to Message instead.
    <EnterpriseObjects.Attributes.TableDescription("Message")>  _
    Public MustInherit Class MessageBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal messageID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(messageID, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal messageID As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(messageID).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property MessageID As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(300),  _
         EnterpriseObjects.Attributes.FieldDescription("Message Text")>  _
        Public Property Description As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property Active As Boolean
            Get
                Return CType(Me(2),Boolean)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property IndividualMessageId As Integer
            Get
                If (Me.IsIndividualMessageIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Integer)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property Stage As Integer
            Get
                If (Me.IsStageNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property TitleId As Integer
            Get
                If (Me.IsTitleIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),Integer)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(200),  _
         EnterpriseObjects.Attributes.FieldDescription("Hyperlink for information")>  _
        Public Property URL As String
            Get
                If (Me.IsURLNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),String)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Property IsWarning As Boolean
            Get
                Return CType(Me(7),Boolean)
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),Integer)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.MessageService
            Get
                Return CType(GetServiceObject(GetType(Service.MessageService)),Service.MessageService)
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
        
        Public Function IsIndividualMessageIdNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetIndividualMessageIdToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsStageNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetStageToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsTitleIdNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetTitleIdToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsURLNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetURLToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(9)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.MessageSet
            Return MessageBase.GetAll(false, false, MessageServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.MessageSet
            Return MessageBase.GetAll(includeHyphen, false, MessageServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As MessageServiceBase.OrderBy) As EntitySet.MessageSet
            Dim service As Service.MessageService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As MessageServiceBase.OrderBy) As EntitySet.MessageSet
            Return MessageBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal messageID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Message
            Dim service As Service.MessageService
            service = ServiceObject
            Return service.GetById(MessageID, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal messageID As Integer) As Entity.Message
            Dim service As Service.MessageService
            service = ServiceObject
            Return service.GetById(MessageID)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal messageID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.MessageService
            service = ServiceObject
            Return service.DeleteById(messageID, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal messageID As Integer) As Boolean
            Return MessageBase.DeleteById(messageID, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal messageID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MessageBase.DeleteById(messageID, 0, transaction)
        End Function
        
        Public Shared Function Insert(ByVal description As String, ByVal active As Boolean, ByVal individualMessageId As Object, ByVal stage As Object, ByVal titleId As Object, ByVal uRL As Object, ByVal isWarning As Boolean) As Entity.Message
            Return Entity.Message.ServiceObject.Insert(description, active, individualMessageId, stage, titleId, uRL, isWarning)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim descriptionParam As String = Me.Description
            Dim activeParam As Boolean = Me.Active
            Dim individualMessageIdParam As Object
            If (Me.IsIndividualMessageIdNull = false) Then
                individualMessageIdParam = Me.IndividualMessageId
            Else
                individualMessageIdParam = System.DBNull.Value
            End If
            Dim stageParam As Object
            If (Me.IsStageNull = false) Then
                stageParam = Me.Stage
            Else
                stageParam = System.DBNull.Value
            End If
            Dim titleIdParam As Object
            If (Me.IsTitleIdNull = false) Then
                titleIdParam = Me.TitleId
            Else
                titleIdParam = System.DBNull.Value
            End If
            Dim uRLParam As Object
            If (Me.IsURLNull = false) Then
                uRLParam = EnterpriseObjects.Common.ParseSQLText(Me.URL)
            Else
                uRLParam = System.DBNull.Value
            End If
            Dim isWarningParam As Boolean = Me.IsWarning
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.Message.ServiceObject.Update(Me.Id, descriptionParam, activeParam, individualMessageIdParam, stageParam, titleIdParam, uRLParam, isWarningParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
