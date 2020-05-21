'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.573
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Base
    
    'Base entity implementation for table 'TaxonomyLevelOfUse'
    '*DO NOT* modify this file.
    'Add new properties and methods to TaxonomyLevelOfUse instead.
    <EnterpriseObjects.Attributes.TableDescription("Taxonomy Level of Use")>  _
    Public MustInherit Class TaxonomyLevelOfUseBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal iD As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(iD, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal iD As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(iD).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Overrides Property Id As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(30),  _
         EnterpriseObjects.Attributes.FieldDescription("Level of Use")>  _
        Public Property LevelOfUseDescription As String
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
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Integer)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.TaxonomyLevelOfUseService
            Get
                Return CType(GetServiceObject(GetType(Service.TaxonomyLevelOfUseService)),Service.TaxonomyLevelOfUseService)
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
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(4)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.TaxonomyLevelOfUseSet
            Return TaxonomyLevelOfUseBase.GetAll(false, false, TaxonomyLevelOfUseServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.TaxonomyLevelOfUseSet
            Return TaxonomyLevelOfUseBase.GetAll(includeHyphen, false, TaxonomyLevelOfUseServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As TaxonomyLevelOfUseServiceBase.OrderBy) As EntitySet.TaxonomyLevelOfUseSet
            Dim service As Service.TaxonomyLevelOfUseService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As TaxonomyLevelOfUseServiceBase.OrderBy) As EntitySet.TaxonomyLevelOfUseSet
            Return TaxonomyLevelOfUseBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal iD As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyLevelOfUse
            Dim service As Service.TaxonomyLevelOfUseService
            service = ServiceObject
            Return service.GetById(ID, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal iD As Integer) As Entity.TaxonomyLevelOfUse
            Dim service As Service.TaxonomyLevelOfUseService
            service = ServiceObject
            Return service.GetById(ID)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal iD As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.TaxonomyLevelOfUseService
            service = ServiceObject
            Return service.DeleteById(iD, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal iD As Integer) As Boolean
            Return TaxonomyLevelOfUseBase.DeleteById(iD, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal iD As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return TaxonomyLevelOfUseBase.DeleteById(iD, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyUsage(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyUsageSet
            Return Entity.TaxonomyUsage.GetForTaxonomyLevelOfUse(Me.ID, tran)
        End Function
        
        Public Overloads Function GetRelatedTaxonomyUsage() As EntitySet.TaxonomyUsageSet
            Return Me.GetRelatedTaxonomyUsage(Nothing)
        End Function
        
        Public Shared Sub Insert(ByVal iD As Integer, ByVal levelOfUseDescription As String, ByVal active As Boolean)
            Entity.TaxonomyLevelOfUse.ServiceObject.Insert(iD, levelOfUseDescription, active)
        End Sub
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim levelOfUseDescriptionParam As String = Me.LevelOfUseDescription
            Dim activeParam As Boolean = Me.Active
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.TaxonomyLevelOfUse.ServiceObject.Update(Me.Id, levelOfUseDescriptionParam, activeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
