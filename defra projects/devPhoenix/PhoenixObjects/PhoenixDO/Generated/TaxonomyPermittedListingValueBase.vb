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
    
    'Base entity implementation for table 'TaxonomyPermittedListingValue'
    '*DO NOT* modify this file.
    'Add new properties and methods to TaxonomyPermittedListingValue instead.
    Public MustInherit Class TaxonomyPermittedListingValueBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal permittedListingID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(permittedListingID, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal permittedListingID As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(permittedListingID).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property PermittedListingID As Integer
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
        
        Public Property LegislationNameID As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(10)>  _
        Public Property ListingValue As String
            Get
                Return CType(Me(2),String)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(10)>  _
        Public Property ListingOrder As String
            Get
                If (Me.IsListingOrderNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),String)
                End If
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
        
        Public Shared ReadOnly Property ServiceObject As Service.TaxonomyPermittedListingValueService
            Get
                Return CType(GetServiceObject(GetType(Service.TaxonomyPermittedListingValueService)),Service.TaxonomyPermittedListingValueService)
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
        
        Public Function IsListingOrderNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetListingOrderToNull()
            Me(3) = System.DBNull.Value
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
        
        Public Overloads Shared Function GetAll() As EntitySet.TaxonomyPermittedListingValueSet
            Return TaxonomyPermittedListingValueBase.GetAll(false, false, TaxonomyPermittedListingValueServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.TaxonomyPermittedListingValueSet
            Return TaxonomyPermittedListingValueBase.GetAll(includeHyphen, false, TaxonomyPermittedListingValueServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As TaxonomyPermittedListingValueServiceBase.OrderBy) As EntitySet.TaxonomyPermittedListingValueSet
            Dim service As Service.TaxonomyPermittedListingValueService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As TaxonomyPermittedListingValueServiceBase.OrderBy) As EntitySet.TaxonomyPermittedListingValueSet
            Return TaxonomyPermittedListingValueBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permittedListingID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyPermittedListingValue
            Dim service As Service.TaxonomyPermittedListingValueService
            service = ServiceObject
            Return service.GetById(PermittedListingID, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permittedListingID As Integer) As Entity.TaxonomyPermittedListingValue
            Dim service As Service.TaxonomyPermittedListingValueService
            service = ServiceObject
            Return service.GetById(PermittedListingID)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permittedListingID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.TaxonomyPermittedListingValueService
            service = ServiceObject
            Return service.DeleteById(permittedListingID, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permittedListingID As Integer) As Boolean
            Return TaxonomyPermittedListingValueBase.DeleteById(permittedListingID, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permittedListingID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return TaxonomyPermittedListingValueBase.DeleteById(permittedListingID, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForTaxonomyLegislationName(ByVal legislationNameID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyPermittedListingValueSet
            Dim service As Service.TaxonomyPermittedListingValueService
            service = ServiceObject
            Return service.GetForTaxonomyLegislationName(legislationNameID, tran)
        End Function
        
        Public Overloads Shared Function GetForTaxonomyLegislationName(ByVal legislationNameID As Integer) As EntitySet.TaxonomyPermittedListingValueSet
            Return TaxonomyPermittedListingValueBase.GetForTaxonomyLegislationName(legislationNameID, Nothing)
        End Function
        
        Public Shared Function Insert(ByVal legislationNameID As Integer, ByVal listingValue As String) As Entity.TaxonomyPermittedListingValue
            Return Entity.TaxonomyPermittedListingValue.ServiceObject.Insert(legislationNameID, listingValue)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim legislationNameIDParam As Integer = Me.LegislationNameID
            Dim listingValueParam As String = Me.ListingValue
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.TaxonomyPermittedListingValue.ServiceObject.Update(Me.Id, legislationNameIDParam, listingValueParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
