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
    
    'Base entity implementation for table 'Specie'
    '*DO NOT* modify this file.
    'Add new properties and methods to Specie instead.
    Public MustInherit Class SpecieBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal specieId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(specieId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal specieId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(specieId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property SpecieId As Integer
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
        Public Property CommonName As String
            Get
                If (Me.IsCommonNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),String)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property ECAnnex As String
            Get
                If (Me.IsECAnnexNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),String)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property CITESAppendix As String
            Get
                If (Me.IsCITESAppendixNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),String)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property ScientificName As String
            Get
                If (Me.IsScientificNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),String)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Description As String
            Get
                If (Me.IsDescriptionNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(5),String)
                End If
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property AppliedForName As String
            Get
                If (Me.IsAppliedForNameNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),String)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Lineage As String
            Get
                If (Me.IsLineageNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),String)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property KeyedByApplicant As Boolean
            Get
                If (Me.IsKeyedByApplicantNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),Boolean)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Property Hybrid As Boolean
            Get
                If (Me.IsHybridNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(9),Boolean)
                End If
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        Public Property CommonNameSource As Integer
            Get
                If (Me.IsCommonNameSourceNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(10),Integer)
                End If
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        Public Property CommonNameID As Integer
            Get
                If (Me.IsCommonNameIDNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(11),Integer)
                End If
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        Public Property IsCoral As Boolean
            Get
                Return CType(Me(12),Boolean)
            End Get
            Set
                Me(12) = value
            End Set
        End Property
        
        Public Property PaymentKingdom As Integer
            Get
                If (Me.IsPaymentKingdomNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(13),Integer)
                End If
            End Get
            Set
                Me(13) = value
            End Set
        End Property
        
        Public Property PaymentTaxonType As Integer
            Get
                If (Me.IsPaymentTaxonTypeNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(14),Integer)
                End If
            End Get
            Set
                Me(14) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(15),Integer)
                End If
            End Get
            Set
                Me(15) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.SpecieService
            Get
                Return CType(GetServiceObject(GetType(Service.SpecieService)),Service.SpecieService)
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
        
        Public Function IsCommonNameNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetCommonNameToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsECAnnexNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetECAnnexToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsCITESAppendixNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetCITESAppendixToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsScientificNameNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetScientificNameToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsDescriptionNull() As Boolean
            Return Me.IsNull(5)
        End Function
        
        Public Sub SetDescriptionToNull()
            Me(5) = System.DBNull.Value
        End Sub
        
        Public Function IsAppliedForNameNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetAppliedForNameToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsLineageNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetLineageToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsKeyedByApplicantNull() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetKeyedByApplicantToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Function IsHybridNull() As Boolean
            Return Me.IsNull(9)
        End Function
        
        Public Sub SetHybridToNull()
            Me(9) = System.DBNull.Value
        End Sub
        
        Public Function IsCommonNameSourceNull() As Boolean
            Return Me.IsNull(10)
        End Function
        
        Public Sub SetCommonNameSourceToNull()
            Me(10) = System.DBNull.Value
        End Sub
        
        Public Function IsCommonNameIDNull() As Boolean
            Return Me.IsNull(11)
        End Function
        
        Public Sub SetCommonNameIDToNull()
            Me(11) = System.DBNull.Value
        End Sub
        
        Public Function IsPaymentKingdomNull() As Boolean
            Return Me.IsNull(13)
        End Function
        
        Public Sub SetPaymentKingdomToNull()
            Me(13) = System.DBNull.Value
        End Sub
        
        Public Function IsPaymentTaxonTypeNull() As Boolean
            Return Me.IsNull(14)
        End Function
        
        Public Sub SetPaymentTaxonTypeToNull()
            Me(14) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(15)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(15) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(16)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.SpecieSet
            Return SpecieBase.GetAll(false, false, SpecieServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.SpecieSet
            Return SpecieBase.GetAll(includeHyphen, false, SpecieServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As SpecieServiceBase.OrderBy) As EntitySet.SpecieSet
            Dim service As Service.SpecieService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As SpecieServiceBase.OrderBy) As EntitySet.SpecieSet
            Return SpecieBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal specieId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.Specie
            Dim service As Service.SpecieService
            service = ServiceObject
            Return service.GetById(SpecieId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal specieId As Integer) As Entity.Specie
            Dim service As Service.SpecieService
            service = ServiceObject
            Return service.GetById(SpecieId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specieId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.SpecieService
            service = ServiceObject
            Return service.DeleteById(specieId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specieId As Integer) As Boolean
            Return SpecieBase.DeleteById(specieId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal specieId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return SpecieBase.DeleteById(specieId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForCommonNameSourceTaxonomyCommonName(ByVal sourceTable As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SpecieSet
            Dim service As Service.SpecieService
            service = ServiceObject
            Return service.GetForCommonNameSourceTaxonomyCommonName(sourceTable, tran)
        End Function
        
        Public Overloads Shared Function GetForCommonNameSourceTaxonomyCommonName(ByVal sourceTable As Integer) As EntitySet.SpecieSet
            Return SpecieBase.GetForCommonNameSourceTaxonomyCommonName(sourceTable, Nothing)
        End Function
        
        Public Overloads Shared Function GetForCommonNameIDTaxonomyCommonName(ByVal sourceTable As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SpecieSet
            Dim service As Service.SpecieService
            service = ServiceObject
            Return service.GetForCommonNameIDTaxonomyCommonName(sourceTable, tran)
        End Function
        
        Public Overloads Shared Function GetForCommonNameIDTaxonomyCommonName(ByVal sourceTable As Integer) As EntitySet.SpecieSet
            Return SpecieBase.GetForCommonNameIDTaxonomyCommonName(sourceTable, Nothing)
        End Function
        
        Public Overloads Function GetRelatedAccommodationAndCare(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.AccommodationAndCareSet
            Return Entity.AccommodationAndCare.GetForSpecie(Me.SpecieId, tran)
        End Function
        
        Public Overloads Function GetRelatedAccommodationAndCare() As EntitySet.AccommodationAndCareSet
            Return Me.GetRelatedAccommodationAndCare(Nothing)
        End Function
        
        Public Overloads Function GetRelatedImportSpecie(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.ImportSpecieSet
            Return Entity.ImportSpecie.GetForSpecie(Me.SpecieId, tran)
        End Function
        
        Public Overloads Function GetRelatedImportSpecie() As EntitySet.ImportSpecieSet
            Return Me.GetRelatedImportSpecie(Nothing)
        End Function
        
        Public Overloads Function GetRelatedNotificationSpecieLink(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.NotificationSpecieLinkSet
            Return Entity.NotificationSpecieLink.GetForSpecie(Me.SpecieId, tran)
        End Function
        
        Public Overloads Function GetRelatedNotificationSpecieLink() As EntitySet.NotificationSpecieLinkSet
            Return Me.GetRelatedNotificationSpecieLink(Nothing)
        End Function
        
        Public Overloads Function GetRelatedPermit(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitSet
            Return Entity.Permit.GetForSpecie(Me.SpecieId, tran)
        End Function
        
        Public Overloads Function GetRelatedPermit() As EntitySet.PermitSet
            Return Me.GetRelatedPermit(Nothing)
        End Function
        
        Public Overloads Function GetRelatedPermitInfo(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitInfoSet
            Return Entity.PermitInfo.GetForSpecie(Me.SpecieId, tran)
        End Function
        
        Public Overloads Function GetRelatedPermitInfo() As EntitySet.PermitInfoSet
            Return Me.GetRelatedPermitInfo(Nothing)
        End Function
        
        Public Overloads Function GetRelatedSpecimenSpecie(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SpecimenSpecieSet
            Return Entity.SpecimenSpecie.GetForSpecie(Me.SpecieId, tran)
        End Function
        
        Public Overloads Function GetRelatedSpecimenSpecie() As EntitySet.SpecimenSpecieSet
            Return Me.GetRelatedSpecimenSpecie(Nothing)
        End Function
        
        Public Overloads Function GetRelatedTaxon(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonSet
            Return Entity.Taxon.GetForSpecie(Me.SpecieId, tran)
        End Function
        
        Public Overloads Function GetRelatedTaxon() As EntitySet.TaxonSet
            Return Me.GetRelatedTaxon(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal commonName As Object, ByVal eCAnnex As Object, ByVal cITESAppendix As Object, ByVal scientificName As Object, ByVal description As Object, ByVal appliedForName As Object, ByVal lineage As Object, ByVal keyedByApplicant As Object, ByVal hybrid As Object, ByVal commonNameSource As Object, ByVal commonNameID As Object, ByVal isCoral As Boolean, ByVal paymentKingdom As Object, ByVal paymentTaxonType As Object) As Entity.Specie
            Return Entity.Specie.ServiceObject.Insert(commonName, eCAnnex, cITESAppendix, scientificName, description, appliedForName, lineage, keyedByApplicant, hybrid, commonNameSource, commonNameID, isCoral, paymentKingdom, paymentTaxonType)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim commonNameParam As Object
            If (Me.IsCommonNameNull = false) Then
                commonNameParam = EnterpriseObjects.Common.ParseSQLText(Me.CommonName)
            Else
                commonNameParam = System.DBNull.Value
            End If
            Dim eCAnnexParam As Object
            If (Me.IsECAnnexNull = false) Then
                eCAnnexParam = EnterpriseObjects.Common.ParseSQLText(Me.ECAnnex)
            Else
                eCAnnexParam = System.DBNull.Value
            End If
            Dim cITESAppendixParam As Object
            If (Me.IsCITESAppendixNull = false) Then
                cITESAppendixParam = EnterpriseObjects.Common.ParseSQLText(Me.CITESAppendix)
            Else
                cITESAppendixParam = System.DBNull.Value
            End If
            Dim scientificNameParam As Object
            If (Me.IsScientificNameNull = false) Then
                scientificNameParam = EnterpriseObjects.Common.ParseSQLText(Me.ScientificName)
            Else
                scientificNameParam = System.DBNull.Value
            End If
            Dim descriptionParam As Object
            If (Me.IsDescriptionNull = false) Then
                descriptionParam = EnterpriseObjects.Common.ParseSQLText(Me.Description)
            Else
                descriptionParam = System.DBNull.Value
            End If
            Dim appliedForNameParam As Object
            If (Me.IsAppliedForNameNull = false) Then
                appliedForNameParam = EnterpriseObjects.Common.ParseSQLText(Me.AppliedForName)
            Else
                appliedForNameParam = System.DBNull.Value
            End If
            Dim lineageParam As Object
            If (Me.IsLineageNull = false) Then
                lineageParam = EnterpriseObjects.Common.ParseSQLText(Me.Lineage)
            Else
                lineageParam = System.DBNull.Value
            End If
            Dim keyedByApplicantParam As Object
            If (Me.IsKeyedByApplicantNull = false) Then
                keyedByApplicantParam = Me.KeyedByApplicant
            Else
                keyedByApplicantParam = System.DBNull.Value
            End If
            Dim hybridParam As Object
            If (Me.IsHybridNull = false) Then
                hybridParam = Me.Hybrid
            Else
                hybridParam = System.DBNull.Value
            End If
            Dim commonNameSourceParam As Object
            If (Me.IsCommonNameSourceNull = false) Then
                commonNameSourceParam = Me.CommonNameSource
            Else
                commonNameSourceParam = System.DBNull.Value
            End If
            Dim commonNameIDParam As Object
            If (Me.IsCommonNameIDNull = false) Then
                commonNameIDParam = Me.CommonNameID
            Else
                commonNameIDParam = System.DBNull.Value
            End If
            Dim isCoralParam As Boolean = Me.IsCoral
            Dim paymentKingdomParam As Object
            If (Me.IsPaymentKingdomNull = false) Then
                paymentKingdomParam = Me.PaymentKingdom
            Else
                paymentKingdomParam = System.DBNull.Value
            End If
            Dim paymentTaxonTypeParam As Object
            If (Me.IsPaymentTaxonTypeNull = false) Then
                paymentTaxonTypeParam = Me.PaymentTaxonType
            Else
                paymentTaxonTypeParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.Specie.ServiceObject.Update(Me.Id, commonNameParam, eCAnnexParam, cITESAppendixParam, scientificNameParam, descriptionParam, appliedForNameParam, lineageParam, keyedByApplicantParam, hybridParam, commonNameSourceParam, commonNameIDParam, isCoralParam, paymentKingdomParam, paymentTaxonTypeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
