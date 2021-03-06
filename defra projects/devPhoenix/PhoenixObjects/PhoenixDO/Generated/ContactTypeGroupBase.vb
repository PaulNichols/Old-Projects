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
    
    'Base entity implementation for table 'ContactTypeGroup'
    '*DO NOT* modify this file.
    'Add new properties and methods to ContactTypeGroup instead.
    <EnterpriseObjects.Attributes.TableDescription("Contact Type Group")>  _
    Public MustInherit Class ContactTypeGroupBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal contactTypeGroupId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(contactTypeGroupId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal contactTypeGroupId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(contactTypeGroupId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property ContactTypeGroupId As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(500),  _
         EnterpriseObjects.Attributes.FieldDescription("Expression")>  _
        Public Property Expression As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50),  _
         EnterpriseObjects.Attributes.FieldDescription("Group Name")>  _
        Public Property GroupName As String
            Get
                Return CType(Me(2),String)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property Active As Boolean
            Get
                Return CType(Me(3),Boolean)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.ContactTypeGroupService
            Get
                Return CType(GetServiceObject(GetType(Service.ContactTypeGroupService)),Service.ContactTypeGroupService)
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
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(5)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ContactTypeGroupSet
            Return ContactTypeGroupBase.GetAll(false, false, ContactTypeGroupServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ContactTypeGroupSet
            Return ContactTypeGroupBase.GetAll(includeHyphen, false, ContactTypeGroupServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ContactTypeGroupServiceBase.OrderBy) As EntitySet.ContactTypeGroupSet
            Dim service As Service.ContactTypeGroupService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ContactTypeGroupServiceBase.OrderBy) As EntitySet.ContactTypeGroupSet
            Return ContactTypeGroupBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal contactTypeGroupId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ContactTypeGroup
            Dim service As Service.ContactTypeGroupService
            service = ServiceObject
            Return service.GetById(ContactTypeGroupId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal contactTypeGroupId As Integer) As Entity.ContactTypeGroup
            Dim service As Service.ContactTypeGroupService
            service = ServiceObject
            Return service.GetById(ContactTypeGroupId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal contactTypeGroupId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ContactTypeGroupService
            service = ServiceObject
            Return service.DeleteById(contactTypeGroupId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal contactTypeGroupId As Integer) As Boolean
            Return ContactTypeGroupBase.DeleteById(contactTypeGroupId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal contactTypeGroupId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ContactTypeGroupBase.DeleteById(contactTypeGroupId, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedContactType(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ContactTypeSet
            Return Entity.ContactType.GetForContactTypeGroup(Me.ContactTypeGroupId, tran)
        End Function
        
        Public Overloads Function GetRelatedContactType() As EntitySet.ContactTypeSet
            Return Me.GetRelatedContactType(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal expression As String, ByVal groupName As String, ByVal active As Boolean) As Entity.ContactTypeGroup
            Return Entity.ContactTypeGroup.ServiceObject.Insert(expression, groupName, active)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim expressionParam As String = Me.Expression
            Dim groupNameParam As String = Me.GroupName
            Dim activeParam As Boolean = Me.Active
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.ContactTypeGroup.ServiceObject.Update(Me.Id, expressionParam, groupNameParam, activeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
