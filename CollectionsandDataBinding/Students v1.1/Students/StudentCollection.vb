Imports System.Collections
Imports System.ComponentModel

Public Class StudentCollection
    Inherits BaseTypedCollection

    Public Sub New()
    End Sub

#Region "CollectionBase Overrides"
    Public Shadows Sub Add(ByVal Student As Student)
        Me.List.Add(Student)    'List.Add() takes type Object
    End Sub

    Public Shadows Function Contains(ByVal Student As Student) As Boolean
        Return Me.List.Contains(Student)
    End Function

    Public Shadows Function IndexOf(ByVal student As Student) As Integer
        Return Me.List.IndexOf(student)
    End Function

    Public Shadows Sub Insert(ByVal index As Integer, ByVal Student As Student)
        Me.List.Insert(index, Student)
    End Sub

    Public Function GetSubSet(ByVal startIndex As Int32, ByVal endIndex As Int32) As StudentCollection
        Dim returnCollection As New StudentCollection
        If endIndex > Count - 1 Then endIndex = Count - 1
        For i As Int32 = startIndex To endIndex
            returnCollection.Add(Me.Item(i))
        Next i
        Return returnCollection
    End Function

    Default Public Shadows Property Item(ByVal index As Integer) As Student
        Get
            Return CType(Me.List.Item(index), Student)
        End Get
        Set(ByVal Value As Student)
            Me.List(index) = Value
        End Set
    End Property

    Public Overloads Sub Remove(ByVal Student As Student)
        Me.List.Remove(Student)
    End Sub
#End Region

#Region "IBindingList Editing Methods"
    Protected Overrides Function AddNew() As Object
        Dim s As New Student(True)
        Me.Add(s)
        AddHandler s.CancelAddNew, AddressOf Me.CancelAddNew
        Return s
    End Function

    Friend Overrides Sub CancelAddNew(ByVal student As Student, ByVal Remove As Boolean)
        RemoveHandler student.CancelAddNew, AddressOf Me.CancelAddNew
        If (Remove) Then
            Me.List.Remove(student)
        End If
    End Sub

#End Region

#Region "IBindingList Other Features"
    Protected Overrides Function Find(ByVal [property] As System.ComponentModel.PropertyDescriptor, ByVal key As Object) As Integer

        Dim idx As Integer
        Dim result As Integer = -1
        For idx = 0 To Me.List.Count - 1
            Dim value As Object = [property].GetValue(Me.List(idx))
            Select Case [property].Name
                Case "FirstName", "LastName"
                    If (value Is key) Then
                        result = idx
                    End If
                Case "Age", "Grade"
                    If (CType(value, Integer) = CType(key, Integer)) Then
                        result = idx
                    End If
                Case Else
                    If (value Is key) Then
                        result = idx
                    End If
            End Select
            If (result <> -1) Then
                Return result
            End If
        Next
    End Function
#End Region

#Region "ITypedList Implementation"
    Protected Overrides Function GetItemProperties(ByVal listAccessors() As PropertyDescriptor) As PropertyDescriptorCollection
        Dim m_OriginalList As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(Student))
        Dim m_SortedList As PropertyDescriptorCollection = _
            m_OriginalList.Sort(New String() {"FirstName", "LastName", "Age", "Grade"})
        Return m_SortedList
    End Function

    Protected Overrides Function GetListName(ByVal listAccessors() As PropertyDescriptor) As String
        Return "StudentCollection"
    End Function
#End Region
End Class



