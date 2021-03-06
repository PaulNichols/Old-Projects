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
    
    'Base entity implementation for table 'Article10CertificateType'
    '*DO NOT* modify this file.
    'Add new properties and methods to Article10CertificateType instead.
    <EnterpriseObjects.Attributes.TableDescription("Article 10 Type")>  _
    Public MustInherit Class Article10CertificateTypeBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal article10CertificationType As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(article10CertificationType, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal article10CertificationType As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(article10CertificationType).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property Article10CertificationType As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(255),  _
         EnterpriseObjects.Attributes.FieldDescription("Description")>  _
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
        
        Public Shared ReadOnly Property ServiceObject As Service.Article10CertificateTypeService
            Get
                Return CType(GetServiceObject(GetType(Service.Article10CertificateTypeService)),Service.Article10CertificateTypeService)
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
        
        Public Overloads Shared Function GetAll() As EntitySet.Article10CertificateTypeSet
            Return Article10CertificateTypeBase.GetAll(false, false, Article10CertificateTypeServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.Article10CertificateTypeSet
            Return Article10CertificateTypeBase.GetAll(includeHyphen, false, Article10CertificateTypeServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As Article10CertificateTypeServiceBase.OrderBy) As EntitySet.Article10CertificateTypeSet
            Dim service As Service.Article10CertificateTypeService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As Article10CertificateTypeServiceBase.OrderBy) As EntitySet.Article10CertificateTypeSet
            Return Article10CertificateTypeBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal article10CertificationType As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Article10CertificateType
            Dim service As Service.Article10CertificateTypeService
            service = ServiceObject
            Return service.GetById(Article10CertificationType, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal article10CertificationType As Integer) As Entity.Article10CertificateType
            Dim service As Service.Article10CertificateTypeService
            service = ServiceObject
            Return service.GetById(Article10CertificationType)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal article10CertificationType As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.Article10CertificateTypeService
            service = ServiceObject
            Return service.DeleteById(article10CertificationType, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal article10CertificationType As Integer) As Boolean
            Return Article10CertificateTypeBase.DeleteById(article10CertificationType, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal article10CertificationType As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return Article10CertificateTypeBase.DeleteById(article10CertificationType, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedArticle10(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.Article10Set
            Return Entity.Article10.GetForArticle10CertificateType(Me.Article10CertificationType, tran)
        End Function
        
        Public Overloads Function GetRelatedArticle10() As EntitySet.Article10Set
            Return Me.GetRelatedArticle10(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal description As String, ByVal active As Boolean) As Entity.Article10CertificateType
            Return Entity.Article10CertificateType.ServiceObject.Insert(description, active)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim descriptionParam As String = Me.Description
            Dim activeParam As Boolean = Me.Active
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.Article10CertificateType.ServiceObject.Update(Me.Id, descriptionParam, activeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
