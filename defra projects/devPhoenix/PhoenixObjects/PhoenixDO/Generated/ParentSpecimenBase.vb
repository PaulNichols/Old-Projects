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
    
    'Base entity implementation for table 'ParentSpecimen'
    '*DO NOT* modify this file.
    'Add new properties and methods to ParentSpecimen instead.
    <EnterpriseObjects.Attributes.TableDescription("Links specimens to their parents (or possible parents, if colony-bred).")>  _
    Public MustInherit Class ParentSpecimenBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal specimenID As Integer, ByVal parentID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(specimenID, parentID, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal specimenID As Integer, ByVal parentID As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(specimenID, parentID).RawDataset.Tables(0).Rows(0))
        End Sub
        
        <EnterpriseObjects.Attributes.FieldDescription("The specimen")>  _
        Public Property SpecimenID As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("SpecimenID of the parent (or possible parent, if colony-bred)")>  _
        Public Property ParentID As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.ParentSpecimenService
            Get
                Return CType(GetServiceObject(GetType(Service.ParentSpecimenService)),Service.ParentSpecimenService)
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
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(3)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ParentSpecimenSet
            Return ParentSpecimenBase.GetAll(false, false, ParentSpecimenServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ParentSpecimenSet
            Return ParentSpecimenBase.GetAll(includeHyphen, false, ParentSpecimenServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ParentSpecimenServiceBase.OrderBy) As EntitySet.ParentSpecimenSet
            Dim service As Service.ParentSpecimenService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ParentSpecimenServiceBase.OrderBy) As EntitySet.ParentSpecimenSet
            Return ParentSpecimenBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal specimenID As Integer, ByVal parentID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ParentSpecimen
            Dim service As Service.ParentSpecimenService
            service = ServiceObject
            Return service.GetById(New Integer() {specimenID, parentID}, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal specimenID As Integer, ByVal parentID As Integer) As Entity.ParentSpecimen
            Dim service As Service.ParentSpecimenService
            service = ServiceObject
            Return service.GetById(New Integer() {specimenID, parentID})
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specimenID As Integer, ByVal parentID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ParentSpecimenService
            service = ServiceObject
            Return service.DeleteById(New Integer() {specimenID, parentID}, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specimenID As Integer, ByVal parentID As Integer) As Boolean
            Return ParentSpecimenBase.DeleteById(specimenID, parentID, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specimenID As Integer, ByVal parentID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ParentSpecimenBase.DeleteById(specimenID, parentID, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForSpecimenIDSpecimen(ByVal specimenId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ParentSpecimenSet
            Dim service As Service.ParentSpecimenService
            service = ServiceObject
            Return service.GetForSpecimenIDSpecimen(specimenId, tran)
        End Function
        
        Public Overloads Shared Function GetForSpecimenIDSpecimen(ByVal specimenId As Integer) As EntitySet.ParentSpecimenSet
            Return ParentSpecimenBase.GetForSpecimenIDSpecimen(specimenId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForParentIDSpecimen(ByVal specimenId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ParentSpecimenSet
            Dim service As Service.ParentSpecimenService
            service = ServiceObject
            Return service.GetForParentIDSpecimen(specimenId, tran)
        End Function
        
        Public Overloads Shared Function GetForParentIDSpecimen(ByVal specimenId As Integer) As EntitySet.ParentSpecimenSet
            Return ParentSpecimenBase.GetForParentIDSpecimen(specimenId, Nothing)
        End Function
        
        Public Shared Sub Insert(ByVal specimenID As Integer, ByVal parentID As Integer)
            Entity.ParentSpecimen.ServiceObject.Insert(specimenID, parentID)
        End Sub
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim specimenIDParam As Integer = Me.SpecimenID
            Dim parentIDParam As Integer = Me.ParentID
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.ParentSpecimen.ServiceObject.Update(specimenIDParam, parentIDParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
