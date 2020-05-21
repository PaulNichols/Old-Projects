'========================================================
'EntityBoundCollection
'--------------------------------------------------------
'Purpose : A collection wrapper for entities 
'
'Author : Steven Sartain (x912595)
'
'Notes : 
'--------------------------------------------------------
'Revision History
'--------------------------------------------------------
'20 Nov 2003 x912595 : Documented source.
'========================================================
<Serializable()> _
Public Class EntityBoundCollection
    Implements IList

    Public EntitySet As EntitySet
    Public TableIndex As Integer

    Class EntityBoundCollectionCannotBeChangedException
        Inherits ApplicationException

    End Class

    Public Function Add(ByVal value As Object) As Integer Implements System.Collections.IList.Add
        Throw New EntityBoundCollectionCannotBeChangedException
        Return Nothing
    End Function

    Public Sub Clear() Implements System.Collections.IList.Clear
        Throw New EntityBoundCollectionCannotBeChangedException
    End Sub

    Public Function Contains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
        Throw New NotImplementedException
        Return False
    End Function

    Public Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.IList.CopyTo
        For i As Integer = 0 To Count - 1
            array.SetValue(Item(i), index + i)
        Next i
    End Sub

    Public Overridable ReadOnly Property Count() As Integer Implements System.Collections.IList.Count
        Get
            Return CType(EntitySet.Count(TableIndex), Integer)
        End Get
    End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IList.GetEnumerator
        Return EntitySet.GetEnumerator(TableIndex)
    End Function

    Public Function IndexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
        Throw New NotImplementedException
        Return Nothing
    End Function

    Public Sub Insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
        Throw New EntityBoundCollectionCannotBeChangedException
    End Sub

    Public ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.IList.IsReadOnly
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.IList.IsSynchronized
        Get
            Throw New NotImplementedException
            Return False
        End Get
    End Property

    Public Property Item(ByVal index As Integer) As Object Implements System.Collections.IList.Item
        Get
            Return EntitySet.GetEntity(TableIndex, index)
        End Get
        Set(ByVal Value As Object)
            Throw New EntityBoundCollectionCannotBeChangedException
        End Set
    End Property

    Public Sub Remove(ByVal value As Object) Implements System.Collections.IList.Remove
        Throw New EntityBoundCollectionCannotBeChangedException
    End Sub

    Public Sub RemoveAt(ByVal index As Integer) Implements System.Collections.IList.RemoveAt
        Throw New EntityBoundCollectionCannotBeChangedException
    End Sub

    Public ReadOnly Property SyncRoot() As Object Implements System.Collections.IList.SyncRoot
        Get
            'Throw New NotImplementedException()
            Return False
        End Get
    End Property

End Class
