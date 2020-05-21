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
    
    'Base entity implementation for table 'RefusalReason'
    '*DO NOT* modify this file.
    'Add new properties and methods to RefusalReason instead.
    <EnterpriseObjects.Attributes.TableDescription("Refusal Reason")>  _
    Public MustInherit Class RefusalReasonBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal refusalReasonID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(refusalReasonID, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal refusalReasonID As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(refusalReasonID).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property RefusalReasonID As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(1000),  _
         EnterpriseObjects.Attributes.FieldDescription(" Standard Text")>  _
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
        
        <EnterpriseObjects.Attributes.FieldSize(30),  _
         EnterpriseObjects.Attributes.FieldDescription("Subject")>  _
        Public Property Subject As String
            Get
                Return CType(Me(3),String)
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
        
        Public Shared ReadOnly Property ServiceObject As Service.RefusalReasonService
            Get
                Return CType(GetServiceObject(GetType(Service.RefusalReasonService)),Service.RefusalReasonService)
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
        
        Public Overloads Shared Function GetAll() As EntitySet.RefusalReasonSet
            Return RefusalReasonBase.GetAll(false, false, RefusalReasonServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.RefusalReasonSet
            Return RefusalReasonBase.GetAll(includeHyphen, false, RefusalReasonServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As RefusalReasonServiceBase.OrderBy) As EntitySet.RefusalReasonSet
            Dim service As Service.RefusalReasonService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As RefusalReasonServiceBase.OrderBy) As EntitySet.RefusalReasonSet
            Return RefusalReasonBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal refusalReasonID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.RefusalReason
            Dim service As Service.RefusalReasonService
            service = ServiceObject
            Return service.GetById(RefusalReasonID, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal refusalReasonID As Integer) As Entity.RefusalReason
            Dim service As Service.RefusalReasonService
            service = ServiceObject
            Return service.GetById(RefusalReasonID)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal refusalReasonID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.RefusalReasonService
            service = ServiceObject
            Return service.DeleteById(refusalReasonID, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal refusalReasonID As Integer) As Boolean
            Return RefusalReasonBase.DeleteById(refusalReasonID, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal refusalReasonID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return RefusalReasonBase.DeleteById(refusalReasonID, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedPermitInfo(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitInfoSet
            Return Entity.PermitInfo.GetForRefusalReason(Me.RefusalReasonID, tran)
        End Function
        
        Public Overloads Function GetRelatedPermitInfo() As EntitySet.PermitInfoSet
            Return Me.GetRelatedPermitInfo(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal description As String, ByVal active As Boolean, ByVal subject As String) As Entity.RefusalReason
            Return Entity.RefusalReason.ServiceObject.Insert(description, active, subject)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim descriptionParam As String = Me.Description
            Dim activeParam As Boolean = Me.Active
            Dim subjectParam As String = Me.Subject
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.RefusalReason.ServiceObject.Update(Me.Id, descriptionParam, activeParam, subjectParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace