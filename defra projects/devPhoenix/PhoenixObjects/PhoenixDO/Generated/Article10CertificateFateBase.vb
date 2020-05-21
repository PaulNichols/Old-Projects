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
    
    'Base entity implementation for table 'Article10CertificateFate'
    '*DO NOT* modify this file.
    'Add new properties and methods to Article10CertificateFate instead.
    Public MustInherit Class Article10CertificateFateBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal article10CertificateFateId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(article10CertificateFateId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal article10CertificateFateId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(article10CertificateFateId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property Article10CertificateFateId As Integer
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
        
        Public Property FateId As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property QtyUsed As Integer
            Get
                If (Me.IsQtyUsedNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property ReturnedToDefra As Boolean
            Get
                Return CType(Me(3),Boolean)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property PermitId As Integer
            Get
                Return CType(Me(4),Integer)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property SpecimenSoldTo As String
            Get
                If (Me.IsSpecimenSoldToNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),String)
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
        
        Public Shared ReadOnly Property ServiceObject As Service.Article10CertificateFateService
            Get
                Return CType(GetServiceObject(GetType(Service.Article10CertificateFateService)),Service.Article10CertificateFateService)
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
        
        Public Function IsQtyUsedNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetQtyUsedToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsSpecimenSoldToNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetSpecimenSoldToToNull()
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
        
        Public Overloads Shared Function GetAll() As EntitySet.Article10CertificateFateSet
            Return Article10CertificateFateBase.GetAll(false, false, Article10CertificateFateServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.Article10CertificateFateSet
            Return Article10CertificateFateBase.GetAll(includeHyphen, false, Article10CertificateFateServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As Article10CertificateFateServiceBase.OrderBy) As EntitySet.Article10CertificateFateSet
            Dim service As Service.Article10CertificateFateService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As Article10CertificateFateServiceBase.OrderBy) As EntitySet.Article10CertificateFateSet
            Return Article10CertificateFateBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal article10CertificateFateId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Article10CertificateFate
            Dim service As Service.Article10CertificateFateService
            service = ServiceObject
            Return service.GetById(Article10CertificateFateId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal article10CertificateFateId As Integer) As Entity.Article10CertificateFate
            Dim service As Service.Article10CertificateFateService
            service = ServiceObject
            Return service.GetById(Article10CertificateFateId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal article10CertificateFateId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.Article10CertificateFateService
            service = ServiceObject
            Return service.DeleteById(article10CertificateFateId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal article10CertificateFateId As Integer) As Boolean
            Return Article10CertificateFateBase.DeleteById(article10CertificateFateId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal article10CertificateFateId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return Article10CertificateFateBase.DeleteById(article10CertificateFateId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForArticle10Fate(ByVal article10FateId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.Article10CertificateFateSet
            Dim service As Service.Article10CertificateFateService
            service = ServiceObject
            Return service.GetForArticle10Fate(article10FateId, tran)
        End Function
        
        Public Overloads Shared Function GetForArticle10Fate(ByVal article10FateId As Integer) As EntitySet.Article10CertificateFateSet
            Return Article10CertificateFateBase.GetForArticle10Fate(article10FateId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForPermit(ByVal permitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.Article10CertificateFateSet
            Dim service As Service.Article10CertificateFateService
            service = ServiceObject
            Return service.GetForPermit(permitId, tran)
        End Function
        
        Public Overloads Shared Function GetForPermit(ByVal permitId As Integer) As EntitySet.Article10CertificateFateSet
            Return Article10CertificateFateBase.GetForPermit(permitId, Nothing)
        End Function
        
        Public Shared Function Insert(ByVal fateId As Integer, ByVal qtyUsed As Object, ByVal returnedToDefra As Boolean, ByVal permitId As Integer, ByVal specimenSoldTo As Object) As Entity.Article10CertificateFate
            Return Entity.Article10CertificateFate.ServiceObject.Insert(fateId, qtyUsed, returnedToDefra, permitId, specimenSoldTo)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim fateIdParam As Integer = Me.FateId
            Dim qtyUsedParam As Object
            If (Me.IsQtyUsedNull = false) Then
                qtyUsedParam = Me.QtyUsed
            Else
                qtyUsedParam = System.DBNull.Value
            End If
            Dim returnedToDefraParam As Boolean = Me.ReturnedToDefra
            Dim permitIdParam As Integer = Me.PermitId
            Dim specimenSoldToParam As Object
            If (Me.IsSpecimenSoldToNull = false) Then
                specimenSoldToParam = Me.SpecimenSoldTo
            Else
                specimenSoldToParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.Article10CertificateFate.ServiceObject.Update(Me.Id, fateIdParam, qtyUsedParam, returnedToDefraParam, permitIdParam, specimenSoldToParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace