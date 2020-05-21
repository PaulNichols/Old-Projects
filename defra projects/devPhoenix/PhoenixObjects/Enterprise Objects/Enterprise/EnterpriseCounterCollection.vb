Imports System.Reflection
Imports System.Diagnostics
Imports System.Threading

Public Class EnterpriseCounterCollection
    Inherits CollectionBase

    ' members...
    Private _lock As New ReaderWriterLock()
    Private _cache As Hashtable

    Public Sub Add(ByVal counter As EnterpriseCounter)

        ' have we already added it?
        Dim existing As EnterpriseCounter = Find(counter.Name)
        If Not existing Is Nothing Then
            Return
        End If

        ' add it...
        _lock.AcquireWriterLock(-1)
        List.Add(counter)
        ClearCache()
        _lock.ReleaseWriterLock()

    End Sub

    Public Sub Remove(ByVal counter As EnterpriseCounter)
        _lock.AcquireWriterLock(-1)
        list.Remove(counter)
        ClearCache()
        _lock.ReleaseWriterLock()
    End Sub

    Public Shadows Sub Clear()
        _lock.AcquireWriterLock(-1)
        MyBase.Clear()
        ClearCache()
        _lock.ReleaseWriterLock()
    End Sub

    Public Shadows Sub RemoveAt(ByVal index As Integer)
        _lock.AcquireWriterLock(-1)
        MyBase.RemoveAt(index)
        ClearCache()
        _lock.ReleaseWriterLock()
    End Sub

    Public Shadows ReadOnly Property Count() As Integer
        Get
            _lock.AcquireReaderLock(-1)
            Dim theCount As Integer = MyBase.Count
            _lock.ReleaseWriterLock()
            Return theCount
        End Get
    End Property

    Default Public Property Item(ByVal index As Integer) As EnterpriseCounter
        Get
            _lock.AcquireReaderLock(-1)
            Dim theItem As EnterpriseCounter = CType(list.Item(index), EnterpriseCounter)
            _lock.ReleaseReaderLock()
            Return theItem
        End Get
        Set(ByVal Value As EnterpriseCounter)
            _lock.AcquireWriterLock(-1)
            list.Item(index) = Value
            ClearCache()
            _lock.ReleaseWriterLock()
        End Set
    End Property

    Protected Sub ClearCache()
        _lock.AcquireWriterLock(-1)
        _cache = Nothing
        _lock.ReleaseWriterLock()
    End Sub

    Protected Sub CheckCacheExists()

        ' lock...
        _lock.AcquireReaderLock(-1)

        ' do we have it?
        If _cache Is Nothing Then

            ' upgrade...
            _lock.UpgradeToWriterLock(-1)

            ' create it...
            _cache = New Hashtable
            Dim counter As EnterpriseCounter
            For Each counter In Me.InnerList
                _cache.Add(counter.Name.ToLower(), counter)
            Next

        End If

        ' unlock...
        _lock.ReleaseLock()

    End Sub

    Public Function Find(ByVal name As String) As EnterpriseCounter

        ' do we have a cache?
        CheckCacheExists()

        ' loop...
        _lock.AcquireReaderLock(-1)
        Dim found As EnterpriseCounter = CType(_cache(name.ToLower()), EnterpriseCounter)
        _lock.ReleaseReaderLock()

        ' return...
        Return found

    End Function

    Public Function Exists(ByVal name As String) As Boolean

        ' do we have a cache?
        CheckCacheExists()

        ' exists?
        _lock.AcquireReaderLock(-1)
        Dim counterExists As Boolean = _cache.Contains(name.ToLower)
        _lock.ReleaseReaderLock()

        ' return...
        Return counterExists

    End Function

End Class
