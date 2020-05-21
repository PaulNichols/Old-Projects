Option Strict Off

Imports System.Collections
Imports System.Collections.Specialized
Imports System.ComponentModel

Public Class StudentComparer
    Implements IComparer

    Private m_SortList As ListSortDescriptionCollection

    Public Sub New(ByVal SortProperty As PropertyDescriptor, ByVal direction As ListSortDirection)
        'Create a new list every time
        m_SortList = New ListSortDescriptionCollection(New ListSortDescription() {New ListSortDescription(SortProperty, direction)})
    End Sub

    Public Sub New(ByVal SortList As ListSortDescriptionCollection)
        m_SortList = SortList
    End Sub

    Private Function CompareSingleProperty(ByVal x As Student, ByVal y As Student, ByVal prop As PropertyDescriptor, ByVal direction As ListSortDirection) As Integer
        Dim result As Integer = 0
        Dim directionModifier As Integer
        If (direction = ListSortDirection.Ascending) Then
            directionModifier = 1
        Else
            directionModifier = -1
        End If
        If (x Is Nothing) Then
            result = -1 * directionModifier
        ElseIf (y Is Nothing) Then
            result = 1 * directionModifier
        ElseIf (prop.GetValue(x) < prop.GetValue(y)) Then
            result = -1 * directionModifier
        ElseIf (prop.GetValue(x) > prop.GetValue(y)) Then
            result = 1 * directionModifier
        Else
            result = 0
        End If
        Return result
    End Function

    Private Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
        Implements System.Collections.IComparer.Compare
        Dim idx As Integer
        Dim result As Integer
        If (Not TypeOf x Is Student) Then
            Throw New ArgumentException("Unexpected Argument.  Arguments must be of Type Student", "x")
        End If
        If (Not TypeOf y Is Student) Then
            Throw New ArgumentException("Unexpected Argument.  Arguments must be of Type Student", "y")
        End If
        For idx = 0 To m_SortList.Count - 1
            result = CompareSingleProperty(x, y, m_SortList(idx).PropertyDescriptor, m_SortList(idx).SortDirection)
            If (result <> 0) Then
                Exit For
            End If
        Next
        Return result
    End Function
End Class


