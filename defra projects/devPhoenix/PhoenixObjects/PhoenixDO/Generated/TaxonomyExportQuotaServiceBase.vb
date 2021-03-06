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
    
    'Service base implementation for table 'TaxonomyExportQuota'
    '*DO NOT* add your modifications to this file
    Public MustInherit Class TaxonomyExportQuotaServiceBase
        Inherits EnterpriseObjects.Service
        Implements EnterpriseObjects.IServiceAll
        
        Protected Overrides ReadOnly Property HasStates As Boolean
            Get
                Return false
            End Get
        End Property
        
        Public Overloads Overridable Function GetAll() As EntitySet.TaxonomyExportQuotaSet
            Return Me.GetAll(false, false, OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As OrderBy) As EntitySet.TaxonomyExportQuotaSet
            Return CType(MyBase.GetAll("eosp_SelectTaxonomyExportQuota", GetType(EntitySet.TaxonomyExportQuotaSet), includeHyphen, includeInactive, orderBy),EntitySet.TaxonomyExportQuotaSet)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EntitySet.TaxonomyExportQuotaSet
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyExportQuotaServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAllInternal(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet Implements EnterpriseObjects.IServiceAll.GetAllInternal
            Return Me.GetAll(includeHyphen, includeInactive, TaxonomyExportQuotaServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Overridable Function GetAll(ByVal orderBy As OrderBy) As EntitySet.TaxonomyExportQuotaSet
            Return Me.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Function GetById(ByVal idColumns() As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyExportQuota
            Return CType(MyBase.GetById("eosp_SelectTaxonomyExportQuota", New String() {"Source", "ID"}, idColumns, GetType(EntitySet.TaxonomyExportQuotaSet), tran),Entity.TaxonomyExportQuota)
        End Function
        
        Public Overloads Function GetById(ByVal idColumns() As Integer) As Entity.TaxonomyExportQuota
            Return Me.GetById(idColumns, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal idColumns() As Integer, ByVal checkSum As Integer) As Boolean
            Return Me.DeleteById(idColumns, checkSum, Nothing)
        End Function
        
        Public Overloads Function DeleteById(ByVal idColumns() As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return MyBase.DeleteById("eosp_DeleteTaxonomyExportQuota", New String() {"Source", "ID"}, idColumns, checkSum, transaction)
        End Function
        
        'GetForTaxonomyCITESNotification - links to the TaxonomyCITESNotification table...
        Public Overloads Function GetForTaxonomyCITESNotification(ByVal CITESNotificationID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyExportQuotaSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from TaxonomyExportQuota where ExportQuo"& _ 
"taNotificationID=" + CITESNotificationID.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.TaxonomyExportQuotaSet), tran),EntitySet.TaxonomyExportQuotaSet)
        End Function
        
        'GetForTaxonomyCITESNotification - links to the TaxonomyCITESNotification table...
        Public Overloads Function GetForTaxonomyCITESNotification(ByVal CITESNotificationID As Integer) As EntitySet.TaxonomyExportQuotaSet
            Return Me.GetForTaxonomyCITESNotification(CITESNotificationID, Nothing)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyExportQuotaSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from TaxonomyExportQuota where CountryID"& _ 
"=" + CountryId.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.TaxonomyExportQuotaSet), tran),EntitySet.TaxonomyExportQuotaSet)
        End Function
        
        'GetForCountry - links to the Country table...
        Public Overloads Function GetForCountry(ByVal CountryId As Integer) As EntitySet.TaxonomyExportQuotaSet
            Return Me.GetForCountry(CountryId, Nothing)
        End Function
        
        'GetForTaxonomyExportQuotaTerm - links to the TaxonomyExportQuotaTerm table...
        Public Overloads Function GetForTaxonomyExportQuotaTerm(ByVal ID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyExportQuotaSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from TaxonomyExportQuota where ExportQuo"& _ 
"taTermID=" + ID.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.TaxonomyExportQuotaSet), tran),EntitySet.TaxonomyExportQuotaSet)
        End Function
        
        'GetForTaxonomyExportQuotaTerm - links to the TaxonomyExportQuotaTerm table...
        Public Overloads Function GetForTaxonomyExportQuotaTerm(ByVal ID As Integer) As EntitySet.TaxonomyExportQuotaSet
            Return Me.GetForTaxonomyExportQuotaTerm(ID, Nothing)
        End Function
        
        'GetForTaxonomyExportQuotaSource - links to the TaxonomyExportQuotaSource table...
        Public Overloads Function GetForTaxonomyExportQuotaSource(ByVal ID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyExportQuotaSet
            Dim sql As String
            sql = ("select *, binary_checksum(*) As checkSum from TaxonomyExportQuota where ExportQuo"& _ 
"taSourceID=" + ID.ToString)
            Return CType(Me.GetEntitySet(sql, GetType(EntitySet.TaxonomyExportQuotaSet), tran),EntitySet.TaxonomyExportQuotaSet)
        End Function
        
        'GetForTaxonomyExportQuotaSource - links to the TaxonomyExportQuotaSource table...
        Public Overloads Function GetForTaxonomyExportQuotaSource(ByVal ID As Integer) As EntitySet.TaxonomyExportQuotaSet
            Return Me.GetForTaxonomyExportQuotaSource(ID, Nothing)
        End Function
        
        Public Overloads Sub Insert(ByVal source As Integer, ByVal iD As Integer, ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal quotaYear As Object, ByVal quotaVolume As Object, ByVal quotaUnit As Object, ByVal exportQuotaNotificationID As Object, ByVal countryID As Object, ByVal exportQuotaTermID As Object, ByVal exportQuotaSourceID As Object, ByVal note As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Sprocs.eosp_CreateTaxonomyExportQuota(source, iD, kingdomID, taxonID, taxonTypeID, quotaYear, quotaVolume, quotaUnit, exportQuotaNotificationID, countryID, exportQuotaTermID, exportQuotaSourceID, note, transaction)
        End Sub
        
        Public Overloads Sub Insert(ByVal source As Integer, ByVal iD As Integer, ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal quotaYear As Object, ByVal quotaVolume As Object, ByVal quotaUnit As Object, ByVal exportQuotaNotificationID As Object, ByVal countryID As Object, ByVal exportQuotaTermID As Object, ByVal exportQuotaSourceID As Object, ByVal note As Object)
            Me.Insert(source, iD, kingdomID, taxonID, taxonTypeID, quotaYear, quotaVolume, quotaUnit, exportQuotaNotificationID, countryID, exportQuotaTermID, exportQuotaSourceID, note, Nothing)
        End Sub
        
        Public Overloads Sub Insert(ByVal taxonomyExportQuota As Entity.TaxonomyExportQuota)
            Me.Insert(taxonomyExportQuota(0), taxonomyExportQuota(1), taxonomyExportQuota(2), taxonomyExportQuota(3), taxonomyExportQuota(4), taxonomyExportQuota(5), taxonomyExportQuota(6), taxonomyExportQuota(7), taxonomyExportQuota(8), taxonomyExportQuota(9), taxonomyExportQuota(10), taxonomyExportQuota(11), taxonomyExportQuota(12))
        End Sub
        
        Public Overloads Sub Insert(ByVal taxonomyExportQuota As Entity.TaxonomyExportQuota, ByVal transaction As System.Data.SqlClient.SqlTransaction)
            Me.Insert(taxonomyExportQuota(0), taxonomyExportQuota(1), taxonomyExportQuota(2), taxonomyExportQuota(3), taxonomyExportQuota(4), taxonomyExportQuota(5), taxonomyExportQuota(6), taxonomyExportQuota(7), taxonomyExportQuota(8), taxonomyExportQuota(9), taxonomyExportQuota(10), taxonomyExportQuota(11), taxonomyExportQuota(12), transaction)
        End Sub
        
        Public Overloads Function Update(ByVal source As Integer, ByVal iD As Integer, ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal quotaYear As Object, ByVal quotaVolume As Object, ByVal quotaUnit As Object, ByVal exportQuotaNotificationID As Object, ByVal countryID As Object, ByVal exportQuotaTermID As Object, ByVal exportQuotaSourceID As Object, ByVal note As Object, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyExportQuota
            Return Sprocs.eosp_UpdateTaxonomyExportQuota(source, iD, kingdomID, taxonID, taxonTypeID, quotaYear, quotaVolume, quotaUnit, exportQuotaNotificationID, countryID, exportQuotaTermID, exportQuotaSourceID, note, checkSum, transaction)
        End Function
        
        Public Overloads Function Update(ByVal source As Integer, ByVal iD As Integer, ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal quotaYear As Object, ByVal quotaVolume As Object, ByVal quotaUnit As Object, ByVal exportQuotaNotificationID As Object, ByVal countryID As Object, ByVal exportQuotaTermID As Object, ByVal exportQuotaSourceID As Object, ByVal note As Object) As Entity.TaxonomyExportQuota
            Return Me.Update(source, iD, kingdomID, taxonID, taxonTypeID, quotaYear, quotaVolume, quotaUnit, exportQuotaNotificationID, countryID, exportQuotaTermID, exportQuotaSourceID, note, 0, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal source As Integer, ByVal iD As Integer, ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal quotaYear As Object, ByVal quotaVolume As Object, ByVal quotaUnit As Object, ByVal exportQuotaNotificationID As Object, ByVal countryID As Object, ByVal exportQuotaTermID As Object, ByVal exportQuotaSourceID As Object, ByVal note As Object, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyExportQuota
            Return Me.Update(source, iD, kingdomID, taxonID, taxonTypeID, quotaYear, quotaVolume, quotaUnit, exportQuotaNotificationID, countryID, exportQuotaTermID, exportQuotaSourceID, note, 0, transaction)
        End Function
        
        Public Overloads Function Update(ByVal source As Integer, ByVal iD As Integer, ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal quotaYear As Object, ByVal quotaVolume As Object, ByVal quotaUnit As Object, ByVal exportQuotaNotificationID As Object, ByVal countryID As Object, ByVal exportQuotaTermID As Object, ByVal exportQuotaSourceID As Object, ByVal note As Object, ByVal checkSum As Integer) As Entity.TaxonomyExportQuota
            Return Me.Update(source, iD, kingdomID, taxonID, taxonTypeID, quotaYear, quotaVolume, quotaUnit, exportQuotaNotificationID, countryID, exportQuotaTermID, exportQuotaSourceID, note, checkSum, Nothing)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyExportQuota As Entity.TaxonomyExportQuota) As Entity.TaxonomyExportQuota
            Return Me.Update(taxonomyExportQuota(0), taxonomyExportQuota(1), taxonomyExportQuota(2), taxonomyExportQuota(3), taxonomyExportQuota(4), taxonomyExportQuota(5), taxonomyExportQuota(6), taxonomyExportQuota(7), taxonomyExportQuota(8), taxonomyExportQuota(9), taxonomyExportQuota(10), taxonomyExportQuota(11), taxonomyExportQuota(12))
        End Function
        
        Public Overloads Function Update(ByVal taxonomyExportQuota As Entity.TaxonomyExportQuota, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyExportQuota
            Return Me.Update(taxonomyExportQuota(0), taxonomyExportQuota(1), taxonomyExportQuota(2), taxonomyExportQuota(3), taxonomyExportQuota(4), taxonomyExportQuota(5), taxonomyExportQuota(6), taxonomyExportQuota(7), taxonomyExportQuota(8), taxonomyExportQuota(9), taxonomyExportQuota(10), taxonomyExportQuota(11), taxonomyExportQuota(12), transaction)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyExportQuota As Entity.TaxonomyExportQuota, ByVal checkSum As Integer) As Entity.TaxonomyExportQuota
            Return Me.Update(taxonomyExportQuota(0), taxonomyExportQuota(1), taxonomyExportQuota(2), taxonomyExportQuota(3), taxonomyExportQuota(4), taxonomyExportQuota(5), taxonomyExportQuota(6), taxonomyExportQuota(7), taxonomyExportQuota(8), taxonomyExportQuota(9), taxonomyExportQuota(10), taxonomyExportQuota(11), taxonomyExportQuota(12), checkSum)
        End Function
        
        Public Overloads Function Update(ByVal taxonomyExportQuota As Entity.TaxonomyExportQuota, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Entity.TaxonomyExportQuota
            Return Me.Update(taxonomyExportQuota(0), taxonomyExportQuota(1), taxonomyExportQuota(2), taxonomyExportQuota(3), taxonomyExportQuota(4), taxonomyExportQuota(5), taxonomyExportQuota(6), taxonomyExportQuota(7), taxonomyExportQuota(8), taxonomyExportQuota(9), taxonomyExportQuota(10), taxonomyExportQuota(11), taxonomyExportQuota(12), checkSum, transaction)
        End Function
        
        Public Overloads Function GetByIndex_IX_TaxonomyExportQuota(ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer) As EntitySet.TaxonomyExportQuotaSet
            Return Sprocs.eosp_SelectTaxonomyExportQuota(source:=Nothing, iD:=Nothing, Index_KingdomID:=[kingdomID], Index_TaxonID:=[taxonID], Index_TaxonTypeID:=[taxonTypeID], transaction:=Nothing, sortOrder:=0)
        End Function
        
        Public Overloads Function GetByIndex_IX_TaxonomyExportQuota(ByVal kingdomID As Integer, ByVal taxonID As Integer, ByVal taxonTypeID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As EntitySet.TaxonomyExportQuotaSet
            Return Sprocs.eosp_SelectTaxonomyExportQuota(source:=Nothing, iD:=Nothing, Index_KingdomID:=[kingdomID], Index_TaxonID:=[taxonID], Index_TaxonTypeID:=[taxonTypeID], sortOrder:=0, transaction:=transaction)
        End Function
        
        Public Enum OrderBy
            
            DefaultOrder
            
            IX_TaxonomyExportQuota
            
            
        End Enum
    End Class
End Namespace
