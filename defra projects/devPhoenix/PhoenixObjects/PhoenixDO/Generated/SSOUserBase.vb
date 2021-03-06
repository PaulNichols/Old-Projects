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
    
    'Base entity implementation for table 'SSOUser'
    '*DO NOT* modify this file.
    'Add new properties and methods to SSOUser instead.
    Public MustInherit Class SSOUserBase
        Inherits EnterpriseObjects.Entity
        Implements EnterpriseObjects.IUpdatable
        
        Private mRawDataset As System.Data.DataSet
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal userId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New
            MyBase.Populate(Me.GetById(userId, tran).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Sub New(ByVal userId As Integer)
            MyBase.New
            MyBase.Populate(Me.GetById(userId).RawDataset.Tables(0).Rows(0))
        End Sub
        
        Public Property UserId As Integer
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
        
        Public Property SSOUserId As Integer
            Get
                Return CType(Me(1),Integer)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property SPNumber As String
            Get
                Return CType(Me(2),String)
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Fullname As String
            Get
                Return CType(Me(3),String)
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Email As String
            Get
                Return CType(Me(4),String)
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Location As String
            Get
                Return CType(Me(5),String)
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Title As String
            Get
                Return CType(Me(6),String)
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Initials As String
            Get
                Return CType(Me(7),String)
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property FirstName As String
            Get
                Return CType(Me(8),String)
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Surname As String
            Get
                Return CType(Me(9),String)
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Grade As String
            Get
                Return CType(Me(10),String)
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Unit As String
            Get
                Return CType(Me(11),String)
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Room As String
            Get
                Return CType(Me(12),String)
            End Get
            Set
                Me(12) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property Ext As String
            Get
                Return CType(Me(13),String)
            End Get
            Set
                Me(13) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property STD As String
            Get
                Return CType(Me(14),String)
            End Get
            Set
                Me(14) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldSize(50)>  _
        Public Property JobTitle As String
            Get
                Return CType(Me(15),String)
            End Get
            Set
                Me(15) = value
            End Set
        End Property
        
        Public Overrides Property CheckSum As Integer Implements EnterpriseObjects.IUpdatable.CheckSum
            Get
                If (Me.IsCheckSumNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(16),Integer)
                End If
            End Get
            Set
                Me(16) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.SSOUserService
            Get
                Return CType(GetServiceObject(GetType(Service.SSOUserService)),Service.SSOUserService)
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
            Return Me.IsNull(16)
        End Function
        
        Public Sub SetCheckSumToNull()
            Me(16) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(17)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.SSOUserSet
            Return SSOUserBase.GetAll(false, false, SSOUserServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.SSOUserSet
            Return SSOUserBase.GetAll(includeHyphen, false, SSOUserServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As SSOUserServiceBase.OrderBy) As EntitySet.SSOUserSet
            Dim service As Service.SSOUserService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As SSOUserServiceBase.OrderBy) As EntitySet.SSOUserSet
            Return SSOUserBase.GetAll(false, false, orderBy)
        End Function
        
        Public Overloads Shared Function GetById(ByVal userId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction) As Entity.SSOUser
            Dim service As Service.SSOUserService
            service = ServiceObject
            Return service.GetById(UserId, tran)
        End Function
        
        Public Overloads Shared Function GetById(ByVal userId As Integer) As Entity.SSOUser
            Dim service As Service.SSOUserService
            service = ServiceObject
            Return service.GetById(UserId)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal userId As Integer, ByVal checkSum As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Dim service As Service.SSOUserService
            service = ServiceObject
            Return service.DeleteById(userId, checkSum, transaction)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal userId As Integer) As Boolean
            Return SSOUserBase.DeleteById(userId, 0, Nothing)
        End Function
        
        Public Overloads Shared Function DeleteById(ByVal userId As Integer, ByVal transaction As System.Data.SqlClient.SqlTransaction) As Boolean
            Return SSOUserBase.DeleteById(userId, 0, transaction)
        End Function
        
        Public Overloads Function GetRelatedSSOUserRoleLink(ByVal tran As System.Data.SqlClient.SqlTransaction) As EntitySet.SSOUserRoleLinkSet
            Return Entity.SSOUserRoleLink.GetForSSOUser(Me.UserId, tran)
        End Function
        
        Public Overloads Function GetRelatedSSOUserRoleLink() As EntitySet.SSOUserRoleLinkSet
            Return Me.GetRelatedSSOUserRoleLink(Nothing)
        End Function
        
        Public Shared Function Insert(ByVal sSOUserId As Integer, ByVal sPNumber As String, ByVal fullname As String, ByVal email As String, ByVal location As String, ByVal title As String, ByVal initials As String, ByVal firstName As String, ByVal surname As String, ByVal grade As String, ByVal unit As String, ByVal room As String, ByVal ext As String, ByVal sTD As String, ByVal jobTitle As String) As Entity.SSOUser
            Return Entity.SSOUser.ServiceObject.Insert(sSOUserId, sPNumber, fullname, email, location, title, initials, firstName, surname, grade, unit, room, ext, sTD, jobTitle)
        End Function
        
        Public Overridable Function SaveChanges() As Boolean Implements EnterpriseObjects.IUpdatable.SaveChanges
            'line up the update params...
            Dim sSOUserIdParam As Integer = Me.SSOUserId
            Dim sPNumberParam As String = Me.SPNumber
            Dim fullnameParam As String = Me.Fullname
            Dim emailParam As String = Me.Email
            Dim locationParam As String = Me.Location
            Dim titleParam As String = Me.Title
            Dim initialsParam As String = Me.Initials
            Dim firstNameParam As String = Me.FirstName
            Dim surnameParam As String = Me.Surname
            Dim gradeParam As String = Me.Grade
            Dim unitParam As String = Me.Unit
            Dim roomParam As String = Me.Room
            Dim extParam As String = Me.Ext
            Dim sTDParam As String = Me.STD
            Dim jobTitleParam As String = Me.JobTitle
            Dim checkSum As Integer
            If (Me.UseConcurrency = true) Then
                checkSum = Me.CheckSum
            Else
                checkSum = 0
            End If
            Dim Result As uk.gov.defra.EnterpriseObjects.Entity = Entity.SSOUser.ServiceObject.Update(Me.Id, sSOUserIdParam, sPNumberParam, fullnameParam, emailParam, locationParam, titleParam, initialsParam, firstNameParam, surnameParam, gradeParam, unitParam, roomParam, extParam, sTDParam, jobTitleParam, checkSum)
            If (Me.UseConcurrency = true) Then
                Me.CheckSum = Result.checkSum
            End If
            Return (Not (Result) Is Nothing)
        End Function
    End Class
End Namespace
