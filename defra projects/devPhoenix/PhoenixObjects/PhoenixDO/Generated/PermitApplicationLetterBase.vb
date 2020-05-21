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
    
    'Base entity implementation for table 'PermitApplicationLetter'
    '*DO NOT* modify this file.
    'Add new properties and methods to PermitApplicationLetter instead.
    Public MustInherit Class PermitApplicationLetterBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal permitApplicationLetterId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(permitApplicationLetterId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal permitApplicationLetterId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(permitApplicationLetterId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property PermitApplicationLetterId As Integer
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
        
        Public Property ApplicationLetterId As Integer
            Get
                If (Me.IsApplicationLetterIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(1),Integer)
                End If
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property PermitId As Integer
            Get
                If (Me.IsPermitIdNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
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
        
        Public Shared ReadOnly Property ServiceObject As Service.PermitApplicationLetterService
            Get
                Return CType(GetServiceObject(GetType(Service.PermitApplicationLetterService)),Service.PermitApplicationLetterService)
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
        
        Public Function IsApplicationLetterIdNull() As Boolean
            Return Me.IsNull(1)
        End Function
        
        Public Sub SetApplicationLetterIdToNull()
            Me(1) = System.DBNull.Value
        End Sub
        
        Public Function IsPermitIdNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetPermitIdToNull()
            Me(2) = System.DBNull.Value
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
        
        Public Overloads Shared Function GetAll() As EntitySet.PermitApplicationLetterSet
            Return PermitApplicationLetterBase.GetAll(false, false, PermitApplicationLetterServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.PermitApplicationLetterSet
            Return PermitApplicationLetterBase.GetAll(includeHyphen, false, PermitApplicationLetterServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As PermitApplicationLetterServiceBase.OrderBy) As EntitySet.PermitApplicationLetterSet
            Dim service As Service.PermitApplicationLetterService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As PermitApplicationLetterServiceBase.OrderBy) As EntitySet.PermitApplicationLetterSet
            Return PermitApplicationLetterBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permitApplicationLetterId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.PermitApplicationLetter
            Dim service As Service.PermitApplicationLetterService
            service = ServiceObject
            Return service.GetById(PermitApplicationLetterId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal permitApplicationLetterId As Integer) As Entity.PermitApplicationLetter
            Dim service As Service.PermitApplicationLetterService
            service = ServiceObject
            Return service.GetById(PermitApplicationLetterId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitApplicationLetterId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.PermitApplicationLetterService
            service = ServiceObject
            Return service.DeleteById(permitApplicationLetterId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitApplicationLetterId As Integer) As Boolean
            Return PermitApplicationLetterBase.DeleteById(permitApplicationLetterId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal permitApplicationLetterId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return PermitApplicationLetterBase.DeleteById(permitApplicationLetterId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForApplicationLetter(ByVal applicationLetterId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitApplicationLetterSet
            Dim service As Service.PermitApplicationLetterService
            service = ServiceObject
            Return service.GetForApplicationLetter(applicationLetterId, tran)
        End Function
        
        Public Overloads Shared Function GetForApplicationLetter(ByVal applicationLetterId As Integer) As EntitySet.PermitApplicationLetterSet
            Return PermitApplicationLetterBase.GetForApplicationLetter(applicationLetterId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForPermit(ByVal permitId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.PermitApplicationLetterSet
            Dim service As Service.PermitApplicationLetterService
            service = ServiceObject
            Return service.GetForPermit(permitId, tran)
        End Function
        
        Public Overloads Shared Function GetForPermit(ByVal permitId As Integer) As EntitySet.PermitApplicationLetterSet
            Return PermitApplicationLetterBase.GetForPermit(permitId, Nothing)
        End Function
        
        Public Shared Function Insert(ByVal applicationLetterId As Object, ByVal permitId As Object) As Entity.PermitApplicationLetter
            Return Entity.PermitApplicationLetter.ServiceObject.Insert(applicationLetterId, permitId)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim applicationLetterIdParam As Object
            If (Me.IsApplicationLetterIdNull = false) Then
                applicationLetterIdParam = Me.ApplicationLetterId
            Else
                applicationLetterIdParam = System.DBNull.Value
            End If
            Dim permitIdParam As Object
            If (Me.IsPermitIdNull = false) Then
                permitIdParam = Me.PermitId
            Else
                permitIdParam = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.PermitApplicationLetter.ServiceObject.Update(Me.Id, applicationLetterIdParam, permitIdParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace