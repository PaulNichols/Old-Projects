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
    
    'Base entity implementation for table 'CITESImportExportPermit'
    '*DO NOT* modify this file.
    'Add new properties and methods to CITESImportExportPermit instead.
    Public MustInherit Class CITESImportExportPermitBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal cITESImportExportPermitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(cITESImportExportPermitId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal cITESImportExportPermitId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(cITESImportExportPermitId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property CITESImportExportPermitId As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(10)>  _
        Public Property CofLExportNumber As String
            Get
                If (Me.IsCofLExportNumberNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),String)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property CofLExportIssueDate As Date
            Get
                If (Me.IsCofLExportIssueDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Date)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property CofLExportPermitExpiryDate As Date
            Get
                If (Me.IsCofLExportPermitExpiryDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Date)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property EULicenseNumber As Integer
            Get
                If (Me.IsEULicenseNumberNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Integer)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property CITESPermitId As Integer
            Get
                Return CType(Me(5),Integer)
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property CofLExportId As Integer
            Get
                If (Me.IsCofLExportIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(10)>  _
        Public Property PreviousCertificateNumber As String
            Get
                If (Me.IsPreviousCertificateNumberNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),String)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property PreviousCertificateIssueDate As Date
            Get
                If (Me.IsPreviousCertificateIssueDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),Date)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Property IsTransactionSpecific As Boolean
            Get
                If (Me.IsIsTransactionSpecificNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(9),Boolean)
                End If
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(10),Integer)
                End If
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.CITESImportExportPermitService
            Get
                Return CType(GetServiceObject(GetType(Service.CITESImportExportPermitService)),Service.CITESImportExportPermitService)
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
        
        Public Function IsCofLExportNumberNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetCofLExportNumberToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsCofLExportIssueDateNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetCofLExportIssueDateToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsCofLExportPermitExpiryDateNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetCofLExportPermitExpiryDateToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsEULicenseNumberNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetEULicenseNumberToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsCofLExportIdNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetCofLExportIdToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsPreviousCertificateNumberNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetPreviousCertificateNumberToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsPreviousCertificateIssueDateNull() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetPreviousCertificateIssueDateToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Function IsIsTransactionSpecificNull() As Boolean
            Return Me.IsNull(9)
        End Function
        
        Public Sub SetIsTransactionSpecificToNull()
            Me(9) = System.DBNull.Value
        End Sub
        
        Public Function IsCheckSumNull() As Boolean
            Return Me.IsNull(10)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(10) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(11)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.CITESImportExportPermitSet
            Return CITESImportExportPermitBase.GetAll(false, false, CITESImportExportPermitServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.CITESImportExportPermitSet
            Return CITESImportExportPermitBase.GetAll(includeHyphen, false, CITESImportExportPermitServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As CITESImportExportPermitServiceBase.OrderBy) As EntitySet.CITESImportExportPermitSet
            Dim service As Service.CITESImportExportPermitService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As CITESImportExportPermitServiceBase.OrderBy) As EntitySet.CITESImportExportPermitSet
            Return CITESImportExportPermitBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal cITESImportExportPermitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.CITESImportExportPermit
            Dim service As Service.CITESImportExportPermitService
            service = ServiceObject
            Return service.GetById(CITESImportExportPermitId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal cITESImportExportPermitId As Integer) As Entity.CITESImportExportPermit
            Dim service As Service.CITESImportExportPermitService
            service = ServiceObject
            Return service.GetById(CITESImportExportPermitId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESImportExportPermitId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.CITESImportExportPermitService
            service = ServiceObject
            Return service.DeleteById(cITESImportExportPermitId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESImportExportPermitId As Integer) As Boolean
            Return CITESImportExportPermitBase.DeleteById(cITESImportExportPermitId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESImportExportPermitId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return CITESImportExportPermitBase.DeleteById(cITESImportExportPermitId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForCITESPermit(ByVal cITESPermitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESImportExportPermitSet
            Dim service As Service.CITESImportExportPermitService
            service = ServiceObject
            Return service.GetForCITESPermit(cITESPermitId, tran)
        End Function
        
        Public Overloads Shared Function GetForCITESPermit(ByVal cITESPermitId As Integer) As EntitySet.CITESImportExportPermitSet
            Return CITESImportExportPermitBase.GetForCITESPermit(cITESPermitId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESImportExportPermitSet
            Dim service As Service.CITESImportExportPermitService
            service = ServiceObject
            Return service.GetForCountry(countryId, tran)
        End Function
        
        Public Overloads Shared Function GetForCountry(ByVal countryId As Integer) As EntitySet.CITESImportExportPermitSet
            Return CITESImportExportPermitBase.GetForCountry(countryId, Nothing)
        End Function
        
        Public Shared Function Insert(ByVal cofLExportNumber As Object, ByVal cofLExportIssueDate As Object, ByVal cofLExportPermitExpiryDate As Object, ByVal eULicenseNumber As Object, ByVal cITESPermitId As Integer, ByVal cofLExportId As Object, ByVal previousCertificateNumber As Object, ByVal previousCertificateIssueDate As Object, ByVal isTransactionSpecific As Object) As Entity.CITESImportExportPermit
            Return Entity.CITESImportExportPermit.ServiceObject.Insert(cofLExportNumber, cofLExportIssueDate, cofLExportPermitExpiryDate, eULicenseNumber, cITESPermitId, cofLExportId, previousCertificateNumber, previousCertificateIssueDate, isTransactionSpecific)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim cofLExportNumberParam As Object
            If (Me.IsCofLExportNumberNull = false) Then
                cofLExportNumberParam = EnterpriseObjects.Common.ParseSQLText(Me.CofLExportNumber)
            Else
                cofLExportNumberParam = System.DBNull.Value
            End If
            Dim cofLExportIssueDateParam As Object
            If (Me.IsCofLExportIssueDateNull = false) Then
                cofLExportIssueDateParam = Me.CofLExportIssueDate
            Else
                cofLExportIssueDateParam = System.DBNull.Value
            End If
            Dim cofLExportPermitExpiryDateParam As Object
            If (Me.IsCofLExportPermitExpiryDateNull = false) Then
                cofLExportPermitExpiryDateParam = Me.CofLExportPermitExpiryDate
            Else
                cofLExportPermitExpiryDateParam = System.DBNull.Value
            End If
            Dim eULicenseNumberParam As Object
            If (Me.IsEULicenseNumberNull = false) Then
                eULicenseNumberParam = Me.EULicenseNumber
            Else
                eULicenseNumberParam = System.DBNull.Value
            End If
            Dim cITESPermitIdParam As Integer = Me.CITESPermitId
            Dim cofLExportIdParam As Object
            If (Me.IsCofLExportIdNull = false) Then
                cofLExportIdParam = Me.CofLExportId
            Else
                cofLExportIdParam = System.DBNull.Value
            End If
            Dim previousCertificateNumberParam As Object
            If (Me.IsPreviousCertificateNumberNull = false) Then
                previousCertificateNumberParam = EnterpriseObjects.Common.ParseSQLText(Me.PreviousCertificateNumber)
            Else
                previousCertificateNumberParam = System.DBNull.Value
            End If
            Dim previousCertificateIssueDateParam As Object
            If (Me.IsPreviousCertificateIssueDateNull = false) Then
                previousCertificateIssueDateParam = Me.PreviousCertificateIssueDate
            Else
                previousCertificateIssueDateParam = System.DBNull.Value
            End If
            Dim isTransactionSpecificParam As Object
            If (Me.IsIsTransactionSpecificNull = false) Then
                isTransactionSpecificParam = Me.IsTransactionSpecific
            Else
                isTransactionSpecificParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.CITESImportExportPermit.ServiceObject.Update(Me.Id, cofLExportNumberParam, cofLExportIssueDateParam, cofLExportPermitExpiryDateParam, eULicenseNumberParam, cITESPermitIdParam, cofLExportIdParam, previousCertificateNumberParam, previousCertificateIssueDateParam, isTransactionSpecificParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
