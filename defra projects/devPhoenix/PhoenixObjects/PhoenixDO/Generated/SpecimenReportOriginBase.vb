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
    
    'Base entity implementation for table 'SpecimenReportOrigin'
    '*DO NOT* modify this file.
    'Add new properties and methods to SpecimenReportOrigin instead.
    Public MustInherit Class SpecimenReportOriginBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal specimenReportOriginId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(specimenReportOriginId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal specimenReportOriginId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(specimenReportOriginId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property SpecimenReportOriginId As Integer
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
        Public Property Description As String
            Get
                Return CType(Me(1),String)
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
        
        Public Shared ReadOnly Property ServiceObject As Service.SpecimenReportOriginService
            Get
                Return CType(GetServiceObject(GetType(Service.SpecimenReportOriginService)),Service.SpecimenReportOriginService)
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
        
        Public Overloads Shared Function GetAll() As EntitySet.SpecimenReportOriginSet
            Return SpecimenReportOriginBase.GetAll(false, false, SpecimenReportOriginServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.SpecimenReportOriginSet
            Return SpecimenReportOriginBase.GetAll(includeHyphen, false, SpecimenReportOriginServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As SpecimenReportOriginServiceBase.OrderBy) As EntitySet.SpecimenReportOriginSet
            Dim service As Service.SpecimenReportOriginService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As SpecimenReportOriginServiceBase.OrderBy) As EntitySet.SpecimenReportOriginSet
            Return SpecimenReportOriginBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal specimenReportOriginId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.SpecimenReportOrigin
            Dim service As Service.SpecimenReportOriginService
            service = ServiceObject
            Return service.GetById(SpecimenReportOriginId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal specimenReportOriginId As Integer) As Entity.SpecimenReportOrigin
            Dim service As Service.SpecimenReportOriginService
            service = ServiceObject
            Return service.GetById(SpecimenReportOriginId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specimenReportOriginId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.SpecimenReportOriginService
            service = ServiceObject
            Return service.DeleteById(specimenReportOriginId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specimenReportOriginId As Integer) As Boolean
            Return SpecimenReportOriginBase.DeleteById(specimenReportOriginId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specimenReportOriginId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return SpecimenReportOriginBase.DeleteById(specimenReportOriginId, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedSpecimenReport(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SpecimenReportSet
            Return Entity.SpecimenReport.GetForSpecimenReportOrigin(Me.SpecimenReportOriginId, tran)
        End Function
        
        Public Overloads Function GetRelatedSpecimenReport() As EntitySet.SpecimenReportSet
            Return Me.GetRelatedSpecimenReport(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal description As String) As Entity.SpecimenReportOrigin
            Return Entity.SpecimenReportOrigin.ServiceObject.Insert(description)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim descriptionParam As String = Me.Description
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.SpecimenReportOrigin.ServiceObject.Update(Me.Id, descriptionParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
