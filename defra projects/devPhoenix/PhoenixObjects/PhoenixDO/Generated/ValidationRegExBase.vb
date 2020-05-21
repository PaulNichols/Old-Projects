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
    
    'Base entity implementation for table 'ValidationRegEx'
    '*DO NOT* modify this file.
    'Add new properties and methods to ValidationRegEx instead.
    <EnterpriseObjects.Attributes.TableDescription("Table of validation regular expressions, for strings like post codes, phone numbe"& _ 
"rs, etc.")>  _
    Public MustInherit Class ValidationRegExBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal validationRegExID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(validationRegExID, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal validationRegExID As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(validationRegExID).RawDataset.Tables(0).Rows(0))
        End Sub
        
        <EnterpriseObjects.Attributes.FieldDescription("Unique ID")>  _
        Public Property ValidationRegExID As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Unique ID")>  _
        Public Overrides Property Id As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50),  _
         EnterpriseObjects.Attributes.FieldDescription("Text description, e.g. 'UK format post codes'. Regular expressions aren't very ea"& _ 
"sy to read, so this description is mandatory!")>  _
        Public Property Description As String
            Get
                Return CType(Me(1),String)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(255),  _
         EnterpriseObjects.Attributes.FieldDescription("Regular expression used to validate the data")>  _
        Public Property RegEx As String
            Get
                Return CType(Me(2),String)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("Is this row active, or logically deleted?")>  _
        Public Property Active As Boolean
            Get
                Return CType(Me(3),Boolean)
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
        
        Public Shared ReadOnly Property ServiceObject As Service.ValidationRegExService
            Get
                Return CType(GetServiceObject(GetType(Service.ValidationRegExService)),Service.ValidationRegExService)
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
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(5)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.ValidationRegExSet
            Return ValidationRegExBase.GetAll(false, false, ValidationRegExServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.ValidationRegExSet
            Return ValidationRegExBase.GetAll(includeHyphen, false, ValidationRegExServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As ValidationRegExServiceBase.OrderBy) As EntitySet.ValidationRegExSet
            Dim service As Service.ValidationRegExService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As ValidationRegExServiceBase.OrderBy) As EntitySet.ValidationRegExSet
            Return ValidationRegExBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal validationRegExID As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.ValidationRegEx
            Dim service As Service.ValidationRegExService
            service = ServiceObject
            Return service.GetById(ValidationRegExID, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal validationRegExID As Integer) As Entity.ValidationRegEx
            Dim service As Service.ValidationRegExService
            service = ServiceObject
            Return service.GetById(ValidationRegExID)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal validationRegExID As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.ValidationRegExService
            service = ServiceObject
            Return service.DeleteById(validationRegExID, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal validationRegExID As Integer) As Boolean
            Return ValidationRegExBase.DeleteById(validationRegExID, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal validationRegExID As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return ValidationRegExBase.DeleteById(validationRegExID, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedPhoneNumberValidationIdGWDCountry(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.GWDCountrySet
            Return Entity.GWDCountry.GetForPhoneNumberValidationIdValidationRegEx(Me.ValidationRegExID, tran)
        End Function
        
        Public Overloads Function GetRelatedPhoneNumberValidationIdGWDCountry() As EntitySet.GWDCountrySet
            Return Me.GetRelatedPhoneNumberValidationIdGWDCountry(Nothing)
        End Function
        
        Public Overloads Function GetRelatedPostCodeValidationIdGWDCountry(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.GWDCountrySet
            Return Entity.GWDCountry.GetForPostCodeValidationIdValidationRegEx(Me.ValidationRegExID, tran)
        End Function
        
        Public Overloads Function GetRelatedPostCodeValidationIdGWDCountry() As EntitySet.GWDCountrySet
            Return Me.GetRelatedPostCodeValidationIdGWDCountry(Nothing)
        End Function
        
        Public Overloads Function GetRelatedIDMarkType(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.IDMarkTypeSet
            Return Entity.IDMarkType.GetForValidationRegEx(Me.ValidationRegExID, tran)
        End Function
        
        Public Overloads Function GetRelatedIDMarkType() As EntitySet.IDMarkTypeSet
            Return Me.GetRelatedIDMarkType(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal description As String, ByVal regEx As String, ByVal active As Boolean) As Entity.ValidationRegEx
            Return Entity.ValidationRegEx.ServiceObject.Insert(description, regEx, active)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim descriptionParam As String = Me.Description
            Dim regExParam As String = Me.RegEx
            Dim activeParam As Boolean = Me.Active
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.ValidationRegEx.ServiceObject.Update(Me.Id, descriptionParam, regExParam, activeParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
