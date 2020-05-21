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


Namespace DataObjects.Views.Base
    
    'Base entity implementation for table 'vSearchCompletedCitesDocuments'
    '*DO NOT* modify this file.
    'Add new properties and methods to SearchCompletedCitesDocuments instead.
    Public MustInherit Class SearchCompletedCitesDocumentsBase
        Inherits EnterpriseObjects.Entity
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Property ApplicationId As Integer
            Get
                Return CType(Me(0),Integer)
            End Get
            Set
                Me(0) = value
            End Set
        End Property
        
        Public Property SemiComplete As Boolean
            Get
                Return CType(Me(1),Boolean)
            End Get
            Set
                Me(1) = value
            End Set
        End Property
        
        Public Property NumberOfCopies As Integer
            Get
                If (Me.IsNumberOfCopiesNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(2),Integer)
                End If
            End Get
            Set
                Me(2) = value
            End Set
        End Property
        
        Public Property DateAuthorised As Date
            Get
                If (Me.IsDateAuthorisedNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(3),Date)
                End If
            End Get
            Set
                Me(3) = value
            End Set
        End Property
        
        Public Property DateRefused As Date
            Get
                If (Me.IsDateRefusedNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(4),Date)
                End If
            End Get
            Set
                Me(4) = value
            End Set
        End Property
        
        Public Property ApplicationTypeId As Integer
            Get
                Return CType(Me(5),Integer)
            End Get
            Set
                Me(5) = value
            End Set
        End Property
        
        Public Property GWDClock As Integer
            Get
                If (Me.IsGWDClockNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(6),Integer)
                End If
            End Get
            Set
                Me(6) = value
            End Set
        End Property
        
        Public Property JNCCClock As Integer
            Get
                If (Me.IsJNCCClockNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(7),Integer)
                End If
            End Get
            Set
                Me(7) = value
            End Set
        End Property
        
        Public Property KewClock As Integer
            Get
                If (Me.IsKewClockNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(8),Integer)
                End If
            End Get
            Set
                Me(8) = value
            End Set
        End Property
        
        Public Property InspectorateClock As Integer
            Get
                If (Me.IsInspectorateClockNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(9),Integer)
                End If
            End Get
            Set
                Me(9) = value
            End Set
        End Property
        
        Public Property GWDClockStartDate As Date
            Get
                If (Me.IsGWDClockStartDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(10),Date)
                End If
            End Get
            Set
                Me(10) = value
            End Set
        End Property
        
        Public Property JNCCClockStartDate As Date
            Get
                If (Me.IsJNCCClockStartDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(11),Date)
                End If
            End Get
            Set
                Me(11) = value
            End Set
        End Property
        
        Public Property KewClockStartDate As Date
            Get
                If (Me.IsKewClockStartDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(12),Date)
                End If
            End Get
            Set
                Me(12) = value
            End Set
        End Property
        
        Public Property InspectorateClockStartDate As Date
            Get
                If (Me.IsInspectorateClockStartDateNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(13),Date)
                End If
            End Get
            Set
                Me(13) = value
            End Set
        End Property
        
        Public Property ServiceLevel As Integer
            Get
                If (Me.IsServiceLevelNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(14),Integer)
                End If
            End Get
            Set
                Me(14) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("WLRS Service Level(Days)")>  _
        Public Property WLRSService As Integer
            Get
                If (Me.IsWLRSServiceNull = true) Then
                    Return Nothing
                Else
                    Return CType(Me(15),Integer)
                End If
            End Get
            Set
                Me(15) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("JNCC Service Level1(Days)")>  _
        Public Property JNCCService1 As Integer
            Get
                If (Me.IsJNCCService1Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(16),Integer)
                End If
            End Get
            Set
                Me(16) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("JNCC Service Level2(Days)")>  _
        Public Property JNCCService2 As Integer
            Get
                If (Me.IsJNCCService2Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(17),Integer)
                End If
            End Get
            Set
                Me(17) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("KEW Service Level1(Days)")>  _
        Public Property KewService1 As Integer
            Get
                If (Me.IsKewService1Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(18),Integer)
                End If
            End Get
            Set
                Me(18) = value
            End Set
        End Property
        
        <EnterpriseObjects.Attributes.FieldDescription("KEW Service Level2(Days)")>  _
        Public Property KewService2 As Integer
            Get
                If (Me.IsKewService2Null = true) Then
                    Return Nothing
                Else
                    Return CType(Me(19),Integer)
                End If
            End Get
            Set
                Me(19) = value
            End Set
        End Property
        
        Public Shared ReadOnly Property ServiceObject As Service.SearchCompletedCitesDocumentsService
            Get
                Return CType(GetServiceObject(GetType(Service.SearchCompletedCitesDocumentsService)),Service.SearchCompletedCitesDocumentsService)
            End Get
        End Property
        
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
        
        Public Function IsNumberOfCopiesNull() As Boolean
            Return Me.IsNull(2)
        End Function
        
        Public Sub SetNumberOfCopiesToNull()
            Me(2) = System.DBNull.Value
        End Sub
        
        Public Function IsDateAuthorisedNull() As Boolean
            Return Me.IsNull(3)
        End Function
        
        Public Sub SetDateAuthorisedToNull()
            Me(3) = System.DBNull.Value
        End Sub
        
        Public Function IsDateRefusedNull() As Boolean
            Return Me.IsNull(4)
        End Function
        
        Public Sub SetDateRefusedToNull()
            Me(4) = System.DBNull.Value
        End Sub
        
        Public Function IsGWDClockNull() As Boolean
            Return Me.IsNull(6)
        End Function
        
        Public Sub SetGWDClockToNull()
            Me(6) = System.DBNull.Value
        End Sub
        
        Public Function IsJNCCClockNull() As Boolean
            Return Me.IsNull(7)
        End Function
        
        Public Sub SetJNCCClockToNull()
            Me(7) = System.DBNull.Value
        End Sub
        
        Public Function IsKewClockNull() As Boolean
            Return Me.IsNull(8)
        End Function
        
        Public Sub SetKewClockToNull()
            Me(8) = System.DBNull.Value
        End Sub
        
        Public Function IsInspectorateClockNull() As Boolean
            Return Me.IsNull(9)
        End Function
        
        Public Sub SetInspectorateClockToNull()
            Me(9) = System.DBNull.Value
        End Sub
        
        Public Function IsGWDClockStartDateNull() As Boolean
            Return Me.IsNull(10)
        End Function
        
        Public Sub SetGWDClockStartDateToNull()
            Me(10) = System.DBNull.Value
        End Sub
        
        Public Function IsJNCCClockStartDateNull() As Boolean
            Return Me.IsNull(11)
        End Function
        
        Public Sub SetJNCCClockStartDateToNull()
            Me(11) = System.DBNull.Value
        End Sub
        
        Public Function IsKewClockStartDateNull() As Boolean
            Return Me.IsNull(12)
        End Function
        
        Public Sub SetKewClockStartDateToNull()
            Me(12) = System.DBNull.Value
        End Sub
        
        Public Function IsInspectorateClockStartDateNull() As Boolean
            Return Me.IsNull(13)
        End Function
        
        Public Sub SetInspectorateClockStartDateToNull()
            Me(13) = System.DBNull.Value
        End Sub
        
        Public Function IsServiceLevelNull() As Boolean
            Return Me.IsNull(14)
        End Function
        
        Public Sub SetServiceLevelToNull()
            Me(14) = System.DBNull.Value
        End Sub
        
        Public Function IsWLRSServiceNull() As Boolean
            Return Me.IsNull(15)
        End Function
        
        Public Sub SetWLRSServiceToNull()
            Me(15) = System.DBNull.Value
        End Sub
        
        Public Function IsJNCCService1Null() As Boolean
            Return Me.IsNull(16)
        End Function
        
        Public Sub SetJNCCService1ToNull()
            Me(16) = System.DBNull.Value
        End Sub
        
        Public Function IsJNCCService2Null() As Boolean
            Return Me.IsNull(17)
        End Function
        
        Public Sub SetJNCCService2ToNull()
            Me(17) = System.DBNull.Value
        End Sub
        
        Public Function IsKewService1Null() As Boolean
            Return Me.IsNull(18)
        End Function
        
        Public Sub SetKewService1ToNull()
            Me(18) = System.DBNull.Value
        End Sub
        
        Public Function IsKewService2Null() As Boolean
            Return Me.IsNull(19)
        End Function
        
        Public Sub SetKewService2ToNull()
            Me(19) = System.DBNull.Value
        End Sub
        
        Public Overrides Sub CreateEmptyEntity()
            MyBase.CreateEmpty(20)
        End Sub
        
        Public Overloads Shared Function GetAll() As EntitySet.SearchCompletedCitesDocumentsSet
            Return SearchCompletedCitesDocumentsBase.GetAll(false, false, SearchCompletedCitesDocumentsServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean) As EntitySet.SearchCompletedCitesDocumentsSet
            Return SearchCompletedCitesDocumentsBase.GetAll(includeHyphen, false, SearchCompletedCitesDocumentsServiceBase.OrderBy.DefaultOrder)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal orderBy As SearchCompletedCitesDocumentsServiceBase.OrderBy) As EntitySet.SearchCompletedCitesDocumentsSet
            Dim service As Service.SearchCompletedCitesDocumentsService
            service = ServiceObject
            Return service.GetAll(includeHyphen, includeInactive, orderBy)
        End Function
        
        Public Overloads Shared Function GetAll(ByVal orderBy As SearchCompletedCitesDocumentsServiceBase.OrderBy) As EntitySet.SearchCompletedCitesDocumentsSet
            Return SearchCompletedCitesDocumentsBase.GetAll(false, false, orderBy)
        End Function
    End Class
End Namespace