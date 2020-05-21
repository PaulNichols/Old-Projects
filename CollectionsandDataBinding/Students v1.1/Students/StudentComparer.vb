Option Strict Off

Imports System.Collections
Imports System.Collections.Specialized
Imports System.ComponentModel

Public Class StudentComparer
    Implements IComparer

    Private m_SortProperty As PropertyDescriptor
    Private m_SortDirection As ListSortDirection

    Public Sub New(ByVal SortProperty As PropertyDescriptor, ByVal direction As ListSortDirection)
        m_SortProperty = SortProperty
        m_SortDirection = direction
    End Sub

    Private Function CompareProperties(ByVal x As Student, ByVal y As Student) As Integer
        Dim result As Integer = 0
        Dim directionModifier As Integer
        If (m_SortDirection = ListSortDirection.Ascending) Then
            directionModifier = 1
        Else
            directionModifier = -1
        End If
        If (x Is Nothing) Then
            result = -1 * directionModifier
        ElseIf (y Is Nothing) Then
            result = 1 * directionModifier
        ElseIf (m_SortProperty.GetValue(x) < m_SortProperty.GetValue(y)) Then
            result = -1 * directionModifier
        ElseIf (m_SortProperty.GetValue(x) > m_SortProperty.GetValue(y)) Then
            result = 1 * directionModifier
        Else
            result = 0
        End If
        Return result
    End Function

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
        Implements System.Collections.IComparer.Compare
        If (Not TypeOf x Is Student) Then
            Throw New ArgumentException("Unexpected Argument.  Arguments must be of Type Student", "x")
        End If
        If (Not TypeOf y Is Student) Then
            Throw New ArgumentException("Unexpected Argument.  Arguments must be of Type Student", "y")
        End If
        Dim result As Integer = CompareProperties(CType(x, Student), CType(y, Student))
        Return result
    End Function
End Class


