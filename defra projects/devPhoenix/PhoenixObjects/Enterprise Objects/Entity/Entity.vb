Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Remoting.Contexts



'========================================================
'Entity
'--------------------------------------------------------
'Purpose : The class that manages a data object 
'representation.
'
'Author : Steven Sartain (x912595)
'
'Notes : 
'--------------------------------------------------------
'Revision History
'--------------------------------------------------------
'20 Nov 2003 x912595 : Documented source.
'========================================================


<Serializable(), Synchronization(SynchronizationAttribute.SUPPORTED)> Public Class Entity
    Implements ISerializable, IEmptyEntity

    Private _data As Object()
    Protected Shared _serviceObjects As Hashtable

    Public Const MaximumNextIdsReturnedPerCall As Integer = 100

    Public Class SaveChangesNotImplementedException
        Inherits NotImplementedException

        Public Sub New()
            MyBase.New("SaveChanges not implemented")
        End Sub

    End Class
  
    Public Sub New()
    End Sub

    Protected Sub CreateEmpty(ByVal emptySize As Int32)
        If emptySize > 0 Then
            ReDim _data(emptySize - 1)
        End If
    End Sub

    Default Property Data(ByVal index As Integer) As Object
        Get
            Return _data(index)
        End Get
        Set(ByVal Value As Object)
            _data(index) = Value
        End Set
    End Property

    Public Sub Populate(ByVal row As DataRow)

        ' store the data...
        ReDim _data(row.ItemArray.Length - 1)
        row.ItemArray.CopyTo(_data, 0)

    End Sub

    '========================================================
    'GetServiceObject
    '--------------------------------------------------------
    'Purpose : Gets this entities service object
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : Create a service object, either from a factory 
    'or from the cache
    '--------------------------------------------------------
    'Parameters
    '----------
    'serviceObjectType (Type) : The table with which to poulate 
    '--------------------------------------------------------
    'Returns : Service
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================    
    Protected Shared Function GetServiceObject(ByVal serviceObjectType As Type) As Service
        If _serviceObjects Is Nothing Then
            _serviceObjects = New Hashtable
        End If
        Dim serviceObject As Service = CType(_serviceObjects(serviceObjectType), Service)
        If serviceObject Is Nothing Then

            ' where do we get the object from?
            Dim factory As ServiceObjectFactory = EnterpriseApplication.Application.ServiceObjectFactory
            serviceObject = factory.Create(serviceObjectType)

            ' add it...
            _serviceObjects.Add(serviceObjectType, serviceObject)

        End If
        Return serviceObject
    End Function

    Public Function SupportsConcurrency() As Boolean
        Dim iface As IConcurrentEntity
        Try
            iface = CType(Me, IConcurrentEntity)
        Catch
        End Try
        If Not iface Is Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub Serialize(ByVal filename As String)
        Dim stream As New FileStream(filename, FileMode.Create)
        Serialize(stream)
        stream.Close()
    End Sub

    Public Sub Serialize(ByVal stream As Stream)

        ' formatter...
        Dim formatter As New BinaryFormatter
        Serialize(stream, formatter)

    End Sub

    Public Sub Serialize(ByVal stream As Stream, ByVal formatter As IFormatter)
        formatter.Serialize(stream, Me)
    End Sub

    Public Sub Serialize(ByVal filename As String, ByVal formatter As IFormatter)
        Dim stream As New FileStream(filename, FileMode.Create)
        Serialize(stream, formatter)
        stream.Close()
    End Sub

    ' Deserialize - retrieve from a stream...
    Public Shared Function Deserialize(ByVal filename As String) As EntitySet
        Dim stream As New FileStream(filename, FileMode.Open)
        Dim newEntitySet As EntitySet = Deserialize(stream)
        stream.Close()
        Return newEntitySet
    End Function

    Public Shared Function Deserialize(ByVal stream As Stream) As EntitySet

        ' formatter...
        Dim formatter As New BinaryFormatter
        Dim newEntitySet As EntitySet = Deserialize(stream, formatter)
        Return newEntitySet

    End Function

    Public Shared Function Deserialize(ByVal filename As String, ByVal formatter As IFormatter) As EntitySet
        Dim stream As New FileStream(filename, FileMode.Open)
        Dim myObject As Object = Deserialize(stream, formatter)
        stream.Close()
        Return CType(myObject, EntitySet)
    End Function

    Public Shared Function Deserialize(ByVal stream As Stream, ByVal formatter As IFormatter) As EntitySet
        Dim myObject As Object = formatter.Deserialize(stream)
        Return CType(myObject, EntitySet)
    End Function

    '========================================================
    'GetObjectData
    '--------------------------------------------------------
    'Purpose : Serialises the data
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Parameters
    '----------
    'info (SerializationInfo) : see MSDN
    'context (StreamingContext) : see MSDN
    '--------------------------------------------------------
    'Implements : ISerializable.GetObjectData
    '--------------------------------------------------------
    'Returns : Service
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================   
    Public Overridable Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext) Implements System.Runtime.Serialization.ISerializable.GetObjectData
        If Not _data Is Nothing Then
            info.AddValue("Row", _data, GetType(Object()))
        Else
            info.AddValue("Row", Nothing, GetType(Object()))
        End If
    End Sub

    Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
        _data = CType(info.GetValue("Row", GetType(Object())), Object())
    End Sub

    '========================================================
    'IsNull
    '--------------------------------------------------------
    'Purpose : Checks the row item to see if the value is null
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Parameters
    '----------
    'index (Integer) : The row index
    '--------------------------------------------------------
    'Returns : Boolean
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================   
    Public Function IsNull(ByVal index As Integer) As Boolean
        If _data(index) Is DBNull.Value Then
            Return True
        Else
            Return False
        End If
    End Function

    '========================================================
    'SetToNull
    '--------------------------------------------------------
    'Purpose : Sets the row item to null
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Parameters
    '----------
    'index (Integer) : The row index
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '======================================================== 
    Public Sub SetToNull(ByVal index As Integer)
        _data(index) = DBNull.Value
    End Sub

    '========================================================
    'UseConcurrency
    '--------------------------------------------------------
    'Purpose : Determines if concurrency management needs to
    'occur
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : 
    '--------------------------------------------------------
    'Returns : Boolean
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '======================================================== 
    Protected ReadOnly Property UseConcurrency() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overridable Sub CreateEmptyEntity()
        Throw New NotImplementedException
    End Sub


    Public Overridable Property Id() As Int32
        Get
            'Throw New NotImplementedException
        End Get
        Set(ByVal Value As Int32)
            ' Throw New NotImplementedException
        End Set
    End Property

    Public Overridable Property CheckSum() As Int32
        Get
        End Get
        Set(ByVal Value As Int32)
        End Set
    End Property
End Class
