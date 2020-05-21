
Imports System.IO
Imports System.Diagnostics
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

'========================================================
'EntitySet
'--------------------------------------------------------
'Purpose : To provide a wrapper around the Microsoft 
'DataSet object in order to extend it's functionality.
'
'Author : Steven Sartain (x912595)
'
'Notes : 
'--------------------------------------------------------
'Revision History
'--------------------------------------------------------
'20 Nov 2003 x912595 : Documented source.
'========================================================
Public Class EntitySet
    Inherits DataSet
    Implements IEnumerable
    Implements ICounterProvider

    Public EntityType As Type = GetType(Entity)
    Private _entityCache As Hashtable
    Public UseCaching As Boolean = True
    Private _boundCollections As Hashtable
    Private Shared _entitySetsPerSecond As PerformanceCounter

    Protected Const EntitySetsPerSecondCounterName As String = "EntitySetsPerSecond"

    Public Sub New()

        ' update...
        If Not _entitySetsPerSecond Is Nothing Then
            _entitySetsPerSecond.Increment()
        End If

    End Sub

    Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
        MyBase.New(info, context)
    End Sub

    Public Overridable Function GetEntity(ByVal index As Integer) As Entity
        Return GetEntity(0, index)
    End Function

    '========================================================
    'GetEntity
    '--------------------------------------------------------
    'Purpose : Create an entity or return one from the cache
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : If caching has been identified and the entity 
    'exists, one is returned. Otherwise one is created, 
    'populated with the relevant data, possibly added to the 
    'cache and then returned.
    '--------------------------------------------------------
    'Parameters
    '----------
    'tableIndex (Integer) : The table with which to poulate 
    'entity with
    'index (Integer) : The row to populate the entity with
    '--------------------------------------------------------
    'Returns : Entity
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================    
    Public Function GetEntity(ByVal tableIndex As Integer, ByVal index As Integer) As Entity

        ' do we have it in the cache?
        Dim newEntity As Entity

        ' caching?
        If UseCaching = True AndAlso tableIndex = 0 Then

            ' create it...
            If _entityCache Is Nothing Then
                _entityCache = New Hashtable
            End If

            ' find it...
            If _entityCache.Contains(index) Then
                Return CType(_entityCache(index), Entity)
            End If

        End If

        ' create it...
        If newEntity Is Nothing Then

            ' create it...
            newEntity = CType(System.Activator.CreateInstance(EntityType), Entity)
            newEntity.Populate(Tables(tableIndex).Rows(index))

            ' add it...
            If UseCaching = True AndAlso tableIndex = 0 Then
                _entityCache.Add(index, newEntity)
            End If

        End If

        ' return...
        Return newEntity

    End Function

    Public Overridable ReadOnly Property Count() As Integer
        Get
            Return Count(0)
        End Get
    End Property

    Public ReadOnly Property Count(ByVal tableIndex As Integer) As Integer
        Get
            Try
                Return Tables(tableIndex).Rows.Count
            Catch
                Return 0
            End Try
        End Get
    End Property

    Public Overridable Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return GetEnumerator(0)
    End Function

    Public Overridable Function GetEnumerator(ByVal tableIndex As Integer) As IEnumerator
        Return New EntitySetEnumerator(tableIndex, Me)
    End Function

    '========================================================
    'GetBoundCollection
    '--------------------------------------------------------
    'Purpose : Create a bound collection
    '
    'Author : Steven Sartain (x912595)
    '
    'Notes : Pulls a collection from the cache if available
    'or creates a new one, sets some members (possibly
    'putting this into the cache) and returns it.
    '--------------------------------------------------------
    'Parameters
    '----------
    'entitySet (EntitySet) : The set that the collection contains 
    'tableIndex (Integer) : The collections table
    'collectionType (Type) : The type of collection to create
    '--------------------------------------------------------
    'Returns : EntityBoundCollection
    '--------------------------------------------------------
    'Revision History
    '--------------------------------------------------------
    '20 Nov 2003 x912595 : Documented source.
    '========================================================  
    Public Function GetBoundCollection(ByVal entitySet As EntitySet, ByVal tableIndex As Integer, ByVal collectionType As Type) As EntityBoundCollection

        ' do we have the collection?
        Dim inCache As Boolean = False
        If Not _boundCollections Is Nothing Then
            inCache = _boundCollections.Contains(tableIndex)
        Else
            _boundCollections = New Hashtable
        End If

        ' in...
        If inCache = False Then

            ' create one...
            Dim collection As EntityBoundCollection = CType(System.Activator.CreateInstance(collectionType), EntityBoundCollection)
            collection.EntitySet = entitySet
            collection.TableIndex = tableIndex
            _boundCollections.Add(tableIndex, collection)

        End If

        ' return
        Return CType(_boundCollections(tableIndex), EntityBoundCollection)

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

    Public Sub CreateCounters(ByVal counters As EnterpriseObjects.EnterpriseCounters) Implements EnterpriseObjects.ICounterProvider.CreateCounters
        'EnterpriseApplication.Application.Counters.Counters.Add(New EnterpriseCounter(EntitySetsPerSecondCounterName, "The number of EntitySets created each second", PerformanceCounterType.RateOfCountsPerSecond32))
    End Sub

    Public Sub CountersCreated(ByVal counters As EnterpriseObjects.EnterpriseCounters) Implements EnterpriseObjects.ICounterProvider.CountersCreated
        _entitySetsPerSecond = counters.Counters.Find(EntitySetsPerSecondCounterName).Counter
    End Sub

    Public Function ConvertSetToEntityArray() As EnterpriseObjects.Entity()
        If Count > 0 Then
            Dim SetList(Count - 1) As EnterpriseObjects.Entity
            Dim Index As Int32 = 0
            For Each entity As EnterpriseObjects.Entity In Me
                SetList(Index) = entity
                Index += 1
            Next entity
            Return SetList
        Else
            Return Nothing
        End If

    End Function
End Class
