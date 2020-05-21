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
    
    'Base entity implementation for table 'TaxonomyDecision'
    '*DO NOT* modify this file.
    'Add new properties and methods to TaxonomyDecision instead.
    Public MustInherit Class TaxonomyDecisionBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal source As Integer, ByVal decisionID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(source, decisionID, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal source As Integer, ByVal decisionID As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(source, decisionID).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property Source As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property DecisionID As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property KingdomID As Integer
            Get
                Return CType(Me(2),Integer)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property TaxonID As Integer
            Get
                Return CType(Me(3),Integer)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property TaxonTypeID As Integer
            Get
                Return CType(Me(4),Integer)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(10)>  _
        Public Property SRGOpinion As String
            Get
                If (Me.IsSRGOpinionNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),String)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property DecisionDate As Date
            Get
                If (Me.IsDecisionDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Date)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(10)>  _
        Public Property Article4Point6ImportRestriction As String
            Get
                If (Me.IsArticle4Point6ImportRestrictionNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),String)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property DecisionLevel As String
            Get
                If (Me.IsDecisionLevelNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),String)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property DecisionMiscellaneous As String
            Get
                If (Me.IsDecisionMiscellaneousNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(9),String)
                End If
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        Public Property CountryID As Integer
            Get
                If (Me.IsCountryIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(10),Integer)
                End If
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(4000)>  _
        Public Property Note As String
            Get
                If (Me.IsNoteNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(11),String)
                End If
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(12),Integer)
                End If
            End Get
            Set
                Me(12) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.TaxonomyDecisionService
            Get
                Return CType(GetServiceObject(GetType(Service.TaxonomyDecisionService)),Service.TaxonomyDecisionService)
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
        
        Public Function IsSRGOpinionNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetSRGOpinionToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsDecisionDateNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetDecisionDateToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsArticle4Point6ImportRestrictionNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetArticle4Point6ImportRestrictionToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsDecisionLevelNull() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetDecisionLevelToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Function IsDecisionMiscellaneousNull() As Boolean
            Return Me.IsNull(9)
        End Function
        
        Public Sub SetDecisionMiscellaneousToNull()
            Me(9) = System.DBNull.Value
        End Sub
        
        Public Function IsCountryIDNull() As Boolean
            Return Me.IsNull(10)
        End Function
        
        Public Sub SetCountryIDToNull()
            Me(10) = System.DBNull.Value
        End Sub
        
        Public Function IsNoteNull() As Boolean
            Return Me.IsNull(11)
        End Function
        
        Public Sub SetNoteToNull()
            Me(11) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(12)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(12) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(13)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.TaxonomyDecisionSet
            Return TaxonomyDecisionBase.GetAll(false, false, TaxonomyDecisionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.TaxonomyDecisionSet
            Return TaxonomyDecisionBase.GetAll(includeHyphen, false, TaxonomyDecisionServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As TaxonomyDecisionServiceBase.OrderBy) As EntitySet.TaxonomyDecisionSet
            Dim service As Service.TaxonomyDecisionService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As TaxonomyDecisionServiceBase.OrderBy) As EntitySet.TaxonomyDecisionSet
            Return TaxonomyDecisionBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal source As Integer, ByVal decisionID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyDecision
            Dim service As Service.TaxonomyDecisionService
            service = ServiceObject
            Return service.GetById(New Integer() {source, decisionID}, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal source As Integer, ByVal decisionID As Integer) As Entity.TaxonomyDecision
            Dim service As Service.TaxonomyDecisionService
            service = ServiceObject
            Return service.GetById(New Integer() {source, decisionID})
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal source As Integer, ByVal decisionID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.TaxonomyDecisionService
            service = ServiceObject
            Return service.DeleteById(New Integer() {source, decisionID}, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal source As Integer, ByVal decisionID As Integer) As Boolean
            Return TaxonomyDecisionBase.DeleteById(source, decisionID, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal source As Integer, ByVal decisionID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return TaxonomyDecisionBase.DeleteById(source, decisionID, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyDecisionSet
            Dim service As Service.TaxonomyDecisionService
            service = ServiceObject
            Return service.GetForCountry(countryId, tran)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer) As EntitySet.TaxonomyDecisionSet
            Return TaxonomyDecisionBase.GetForCountry(countryId, Nothing)
        End Function
        
        Public Shared Sub Insert(ByVal source As Integer, ByVal decisionID As Integer, ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal sRGOpinion As Object, ByVal decisionDate As Object, ByVal article4Point6ImportRestriction As Object, ByVal decisionLevel As Object, ByVal decisionMiscellaneous As Object, ByVal countryID As Object, ByVal note As Object)
            Entity.TaxonomyDecision.ServiceObject.Insert(source, decisionID, kingdomID, taxonID, taxonTypeID, sRGOpinion, decisionDate, article4Point6ImportRestriction, decisionLevel, decisionMiscellaneous, countryID, note)
        End Sub
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim sourceParam As Integer = Me.Source
            Dim decisionIDParam As Integer = Me.DecisionID
            Dim kingdomIDParam As Integer = Me.KingdomID
            Dim taxonIDParam As Integer = Me.TaxonID
            Dim taxonTypeIDParam As Integer = Me.TaxonTypeID
            Dim sRGOpinionParam As Object
            If (Me.IsSRGOpinionNull = false) Then
                sRGOpinionParam = EnterpriseObjects.Common.ParseSQLText(Me.SRGOpinion)
            Else
                sRGOpinionParam = System.DBNull.Value
            End If
            Dim decisionDateParam As Object
            If (Me.IsDecisionDateNull = false) Then
                decisionDateParam = Me.DecisionDate
            Else
                decisionDateParam = System.DBNull.Value
            End If
            Dim article4Point6ImportRestrictionParam As Object
            If (Me.IsArticle4Point6ImportRestrictionNull = false) Then
                article4Point6ImportRestrictionParam = EnterpriseObjects.Common.ParseSQLText(Me.Article4Point6ImportRestriction)
            Else
                article4Point6ImportRestrictionParam = System.DBNull.Value
            End If
            Dim decisionLevelParam As Object
            If (Me.IsDecisionLevelNull = false) Then
                decisionLevelParam = EnterpriseObjects.Common.ParseSQLText(Me.DecisionLevel)
            Else
                decisionLevelParam = System.DBNull.Value
            End If
            Dim decisionMiscellaneousParam As Object
            If (Me.IsDecisionMiscellaneousNull = false) Then
                decisionMiscellaneousParam = EnterpriseObjects.Common.ParseSQLText(Me.DecisionMiscellaneous)
            Else
                decisionMiscellaneousParam = System.DBNull.Value
            End If
            Dim countryIDParam As Object
            If (Me.IsCountryIDNull = false) Then
                countryIDParam = Me.CountryID
            Else
                countryIDParam = System.DBNull.Value
            End If
            Dim noteParam As Object
            If (Me.IsNoteNull = false) Then
                noteParam = Me.Note
            Else
                noteParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.TaxonomyDecision.ServiceObject.Update(sourceParam, decisionIDParam, kingdomIDParam, taxonIDParam, taxonTypeIDParam, sRGOpinionParam, decisionDateParam, article4Point6ImportRestrictionParam, decisionLevelParam, decisionMiscellaneousParam, countryIDParam, noteParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace