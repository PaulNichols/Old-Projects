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
    
    'Base entity implementation for table 'CITESDerivative'
    '*DO NOT* modify this file.
    'Add new properties and methods to CITESDerivative instead.
    <EnterpriseObjects.Attributes.TableDescription("CITES Part or Derivative")>  _
    Public MustInherit Class CITESDerivativeBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal cITESDerivativeId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(cITESDerivativeId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal cITESDerivativeId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(cITESDerivativeId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property CITESDerivativeId As Integer
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
        
        <EnterpriseObjects.Attributes.FieldSize(30)>  _
        Public Property Description As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(3)>  _
        Public Property Code As String
            Get
                Return CType(Me(2),String)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property Active As Boolean
            Get
                Return CType(Me(3),Boolean)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(1000)>  _
        Public Property Explanation As String
            Get
                Return CType(Me(4),String)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property SortSequence As Integer
            Get
                Return CType(Me(5),Integer)
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property PreferredUOMId_01 As Integer
            Get
                If (Me.IsPreferredUOMId_01Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Property PreferredUOMId_02 As Integer
            Get
                If (Me.IsPreferredUOMId_02Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),Integer)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property PreferredUOMId_03 As Integer
            Get
                If (Me.IsPreferredUOMId_03Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),Integer)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Property AlternativeUOMId_01 As Integer
            Get
                If (Me.IsAlternativeUOMId_01Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(9),Integer)
                End If
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        Public Property AlternativeUOMId_02 As Integer
            Get
                If (Me.IsAlternativeUOMId_02Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(10),Integer)
                End If
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        Public Property AlternativeUOMId_03 As Integer
            Get
                If (Me.IsAlternativeUOMId_03Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(11),Integer)
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
        
        Public Shared ReadOnly Property ServiceObject As Service.CITESDerivativeService
            Get
                Return CType(GetServiceObject(GetType(Service.CITESDerivativeService)),Service.CITESDerivativeService)
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
        
        Public Function IsPreferredUOMId_01Null() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetPreferredUOMId_01ToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsPreferredUOMId_02Null() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetPreferredUOMId_02ToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsPreferredUOMId_03Null() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetPreferredUOMId_03ToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Function IsAlternativeUOMId_01Null() As Boolean
            Return Me.IsNull(9)
        End Function
        
        Public Sub SetAlternativeUOMId_01ToNull()
            Me(9) = System.DBNull.Value
        End Sub
        
        Public Function IsAlternativeUOMId_02Null() As Boolean
            Return Me.IsNull(10)
        End Function
        
        Public Sub SetAlternativeUOMId_02ToNull()
            Me(10) = System.DBNull.Value
        End Sub
        
        Public Function IsAlternativeUOMId_03Null() As Boolean
            Return Me.IsNull(11)
        End Function
        
        Public Sub SetAlternativeUOMId_03ToNull()
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
        
        Public Overloads Shared Function GetAll() As EntitySet.CITESDerivativeSet
            Return CITESDerivativeBase.GetAll(false, false, CITESDerivativeServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.CITESDerivativeSet
            Return CITESDerivativeBase.GetAll(includeHyphen, false, CITESDerivativeServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As CITESDerivativeServiceBase.OrderBy) As EntitySet.CITESDerivativeSet
            Dim service As Service.CITESDerivativeService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As CITESDerivativeServiceBase.OrderBy) As EntitySet.CITESDerivativeSet
            Return CITESDerivativeBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal cITESDerivativeId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.CITESDerivative
            Dim service As Service.CITESDerivativeService
            service = ServiceObject
            Return service.GetById(CITESDerivativeId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal cITESDerivativeId As Integer) As Entity.CITESDerivative
            Dim service As Service.CITESDerivativeService
            service = ServiceObject
            Return service.GetById(CITESDerivativeId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESDerivativeId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.CITESDerivativeService
            service = ServiceObject
            Return service.DeleteById(cITESDerivativeId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESDerivativeId As Integer) As Boolean
            Return CITESDerivativeBase.DeleteById(cITESDerivativeId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal cITESDerivativeId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return CITESDerivativeBase.DeleteById(cITESDerivativeId, 0, transaction)
        End Function
        
        Public Overloads Shared Function GetForPreferredUOMId_01CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESDerivativeSet
            Dim service As Service.CITESDerivativeService
            service = ServiceObject
            Return service.GetForPreferredUOMId_01CITESUnitOfMeasurement(cITESUnitOfMeasurementId, tran)
        End Function
        
        Public Overloads Shared Function GetForPreferredUOMId_01CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer) As EntitySet.CITESDerivativeSet
            Return CITESDerivativeBase.GetForPreferredUOMId_01CITESUnitOfMeasurement(cITESUnitOfMeasurementId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForPreferredUOMId_02CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESDerivativeSet
            Dim service As Service.CITESDerivativeService
            service = ServiceObject
            Return service.GetForPreferredUOMId_02CITESUnitOfMeasurement(cITESUnitOfMeasurementId, tran)
        End Function
        
        Public Overloads Shared Function GetForPreferredUOMId_02CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer) As EntitySet.CITESDerivativeSet
            Return CITESDerivativeBase.GetForPreferredUOMId_02CITESUnitOfMeasurement(cITESUnitOfMeasurementId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForPreferredUOMId_03CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESDerivativeSet
            Dim service As Service.CITESDerivativeService
            service = ServiceObject
            Return service.GetForPreferredUOMId_03CITESUnitOfMeasurement(cITESUnitOfMeasurementId, tran)
        End Function
        
        Public Overloads Shared Function GetForPreferredUOMId_03CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer) As EntitySet.CITESDerivativeSet
            Return CITESDerivativeBase.GetForPreferredUOMId_03CITESUnitOfMeasurement(cITESUnitOfMeasurementId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForAlternativeUOMId_01CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESDerivativeSet
            Dim service As Service.CITESDerivativeService
            service = ServiceObject
            Return service.GetForAlternativeUOMId_01CITESUnitOfMeasurement(cITESUnitOfMeasurementId, tran)
        End Function
        
        Public Overloads Shared Function GetForAlternativeUOMId_01CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer) As EntitySet.CITESDerivativeSet
            Return CITESDerivativeBase.GetForAlternativeUOMId_01CITESUnitOfMeasurement(cITESUnitOfMeasurementId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForAlternativeUOMId_02CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESDerivativeSet
            Dim service As Service.CITESDerivativeService
            service = ServiceObject
            Return service.GetForAlternativeUOMId_02CITESUnitOfMeasurement(cITESUnitOfMeasurementId, tran)
        End Function
        
        Public Overloads Shared Function GetForAlternativeUOMId_02CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer) As EntitySet.CITESDerivativeSet
            Return CITESDerivativeBase.GetForAlternativeUOMId_02CITESUnitOfMeasurement(cITESUnitOfMeasurementId, Nothing)
        End Function
        
        Public Overloads Shared Function GetForAlternativeUOMId_03CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESDerivativeSet
            Dim service As Service.CITESDerivativeService
            service = ServiceObject
            Return service.GetForAlternativeUOMId_03CITESUnitOfMeasurement(cITESUnitOfMeasurementId, tran)
        End Function
        
        Public Overloads Shared Function GetForAlternativeUOMId_03CITESUnitOfMeasurement(ByVal cITESUnitOfMeasurementId As Integer) As EntitySet.CITESDerivativeSet
            Return CITESDerivativeBase.GetForAlternativeUOMId_03CITESUnitOfMeasurement(cITESUnitOfMeasurementId, Nothing)
        End Function
        
        Public Overloads Function GetRelatedCITESPermit(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.CITESPermitSet
            Return Entity.CITESPermit.GetForCITESDerivative(Me.CITESDerivativeId, tran)
        End Function
        
        Public Overloads Function GetRelatedCITESPermit() As EntitySet.CITESPermitSet
            Return Me.GetRelatedCITESPermit(Nothing)
        End Function
        
        Public Overloads Function GetRelatedNotificationSpecieLink(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.NotificationSpecieLinkSet
            Return Entity.NotificationSpecieLink.GetForCITESDerivative(Me.CITESDerivativeId, tran)
        End Function
        
        Public Overloads Function GetRelatedNotificationSpecieLink() As EntitySet.NotificationSpecieLinkSet
            Return Me.GetRelatedNotificationSpecieLink(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal description As String, ByVal code As String, ByVal active As Boolean, ByVal explanation As String, ByVal sortSequence As Integer, ByVal preferredUOMId_01 As Object, ByVal preferredUOMId_02 As Object, ByVal preferredUOMId_03 As Object, ByVal alternativeUOMId_01 As Object, ByVal alternativeUOMId_02 As Object, ByVal alternativeUOMId_03 As Object) As Entity.CITESDerivative
            Return Entity.CITESDerivative.ServiceObject.Insert(description, code, active, explanation, sortSequence, preferredUOMId_01, preferredUOMId_02, preferredUOMId_03, alternativeUOMId_01, alternativeUOMId_02, alternativeUOMId_03)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim descriptionParam As String = Me.Description
            Dim codeParam As String = Me.Code
            Dim activeParam As Boolean = Me.Active
            Dim explanationParam As String = Me.Explanation
            Dim sortSequenceParam As Integer = Me.SortSequence
            Dim preferredUOMId_01Param As Object
            If (Me.IsPreferredUOMId_01Null = false) Then
                preferredUOMId_01Param = Me.PreferredUOMId_01
            Else
                preferredUOMId_01Param = System.DBNull.Value
            End If
            Dim preferredUOMId_02Param As Object
            If (Me.IsPreferredUOMId_02Null = false) Then
                preferredUOMId_02Param = Me.PreferredUOMId_02
            Else
                preferredUOMId_02Param = System.DBNull.Value
            End If
            Dim preferredUOMId_03Param As Object
            If (Me.IsPreferredUOMId_03Null = false) Then
                preferredUOMId_03Param = Me.PreferredUOMId_03
            Else
                preferredUOMId_03Param = System.DBNull.Value
            End If
            Dim alternativeUOMId_01Param As Object
            If (Me.IsAlternativeUOMId_01Null = false) Then
                alternativeUOMId_01Param = Me.AlternativeUOMId_01
            Else
                alternativeUOMId_01Param = System.DBNull.Value
            End If
            Dim alternativeUOMId_02Param As Object
            If (Me.IsAlternativeUOMId_02Null = false) Then
                alternativeUOMId_02Param = Me.AlternativeUOMId_02
            Else
                alternativeUOMId_02Param = System.DBNull.Value
            End If
            Dim alternativeUOMId_03Param As Object
            If (Me.IsAlternativeUOMId_03Null = false) Then
                alternativeUOMId_03Param = Me.AlternativeUOMId_03
            Else
                alternativeUOMId_03Param = System.DBNull.Value
            End If
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.CITESDerivative.ServiceObject.Update(Me.Id, descriptionParam, codeParam, activeParam, explanationParam, sortSequenceParam, preferredUOMId_01Param, preferredUOMId_02Param, preferredUOMId_03Param, alternativeUOMId_01Param, alternativeUOMId_02Param, alternativeUOMId_03Param, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
