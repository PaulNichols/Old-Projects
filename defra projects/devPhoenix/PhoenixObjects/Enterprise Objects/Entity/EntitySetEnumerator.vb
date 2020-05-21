'========================================================
'EntitySetEnumerator
'--------------------------------------------------------
'Purpose : To allow the developer to enumerate through 
'the entity sets.
'
'Author : Steven Sartain (x912595)
'
'Notes : 
'--------------------------------------------------------
'Revision History
'--------------------------------------------------------
'20 Nov 2003 x912595 : Documented source.
'========================================================
Public Class EntitySetEnumerator
    Implements IEnumerator

    Public TableIndex As Integer
    Public EntitySet As EntitySet
    Public Position As Integer = -1

    Public Sub New(ByVal entitySet As EntitySet)
        Me.New(0, entitySet)
    End Sub

    Public Sub New(ByVal tableIndex As Integer, ByVal entitySet As EntitySet)
        Me.TableIndex = tableIndex
        Me.EntitySet = entitySet
    End Sub

    Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
        Get
            Return EntitySet.GetEntity(TableIndex, Position)

        End Get
    End Property

    Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
        If Position = EntitySet.Count(TableIndex) - 1 Then
            Return False
        End If
        Position += 1
        Return True
    End Function

    Public Sub Reset() Implements System.Collections.IEnumerator.Reset
        Position = -1
    End Sub

End Class
