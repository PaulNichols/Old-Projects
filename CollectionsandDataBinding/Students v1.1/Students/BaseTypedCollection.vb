Imports System.ComponentModel

Public MustInherit Class BaseTypedCollection
    Inherits CollectionBase
    Implements IComponent, IBindingList, ITypedList


#Region "CollectionBase Overrides"
    Public Sub Add(ByVal item As Object)
        Me.List.Add(item)    'List.Add() takes type Object
    End Sub

    Public Function Contains(ByVal item As Student) As Boolean
        Return Me.List.Contains(item)
    End Function

    Public Sub Insert(ByVal index As Integer, ByVal item As Object)
        Me.List.Insert(index, item)
    End Sub

    Default Public Property Item(ByVal index As Integer) As Object
        Get
            Return Me.List.Item(index)
        End Get
        Set(ByVal Value As Object)
            Me.List(index) = Value
        End Set
    End Property

    Public Sub Remove(ByVal item As Object)
        Me.List.Remove(item)
    End Sub
#End Region



#Region "IComponent Implementation"
    Private m_Site As ISite = Nothing
    Public Event Disposed(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements System.ComponentModel.IComponent.Disposed

    Protected Property Site() As System.ComponentModel.ISite Implements _
        System.ComponentModel.IComponent.Site
        Get
            Return m_Site
        End Get
        Set(ByVal Value As System.ComponentModel.ISite)
            m_Site = Value
        End Set
    End Property

    Public Sub Dispose() Implements System.IDisposable.Dispose
        Me.List.Clear()
        RaiseEvent Disposed(Me, System.EventArgs.Empty)
    End Sub
#End Region


#Region "IBindingList Implementation"

#Region "IBindingList Editing Properties"
    Public Event ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Implements System.ComponentModel.IBindingList.ListChanged

    Protected m_AllowEdit As Boolean = True
    Protected m_AllowNew As Boolean = True
    Protected m_AllowRemove As Boolean = True

    Protected ReadOnly Property AllowEdit() As Boolean Implements System.ComponentModel.IBindingList.AllowEdit
        Get
            Return m_AllowEdit
        End Get
    End Property

    Protected ReadOnly Property AllowNew() As Boolean Implements System.ComponentModel.IBindingList.AllowNew
        Get
            Return m_AllowNew
        End Get
    End Property

    Protected ReadOnly Property AllowRemove() As Boolean Implements System.ComponentModel.IBindingList.AllowRemove
        Get
            Return m_AllowRemove
        End Get
    End Property
#End Region

#Region "IBindingList Features Properties"
    Private m_SupportsChangeNotification As Boolean = True
    Private m_SupportsSearching As Boolean = True
    Private m_SupportsSorting As Boolean = True

    Protected ReadOnly Property SupportsChangeNotification() As Boolean _
        Implements System.ComponentModel.IBindingList.SupportsChangeNotification
        Get
            Return m_SupportsChangeNotification
        End Get
    End Property

    Protected ReadOnly Property SupportsSearching() As Boolean _
        Implements System.ComponentModel.IBindingList.SupportsSearching
        Get
            Return m_SupportsSearching
        End Get
    End Property

    Protected ReadOnly Property SupportsSorting() As Boolean _
        Implements System.ComponentModel.IBindingList.SupportsSorting
        Get
            Return m_SupportsSorting
        End Get
    End Property
#End Region

#Region "IBindingList Editing Methods"
    Protected Overridable Function AddNew() As Object Implements System.ComponentModel.IBindingList.AddNew
        Throw New NotImplementedException
    End Function

    Friend Overridable Sub CancelAddNew(ByVal student As Student, ByVal Remove As Boolean)
        Throw New NotImplementedException
    End Sub

    Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
        MyBase.OnInsertComplete(index, value)
        RaiseEvent ListChanged(Me, New ListChangedEventArgs(ListChangedType.ItemAdded, index))
    End Sub

    Protected Overrides Sub OnRemoveComplete(ByVal index As Integer, ByVal value As Object)
        MyBase.OnRemoveComplete(index, value)
        RaiseEvent ListChanged(Me, New ListChangedEventArgs(ListChangedType.ItemDeleted, index))
    End Sub
#End Region

#Region "IBindingList Sorting Features"
    Private m_SortProperty As PropertyDescriptor
    Private m_SortDirection As ListSortDirection
    Private m_OriginalList As ArrayList

    Protected ReadOnly Property SortDirection() As System.ComponentModel.ListSortDirection _
        Implements System.ComponentModel.IBindingList.SortDirection
        Get
            Return m_SortDirection
        End Get
    End Property

    Protected ReadOnly Property SortProperty() As System.ComponentModel.PropertyDescriptor _
        Implements System.ComponentModel.IBindingList.SortProperty
        Get
            Return m_SortProperty
        End Get
    End Property

    Protected ReadOnly Property IsSorted() As Boolean _
        Implements System.ComponentModel.IBindingList.IsSorted
        Get
            Return m_SortProperty Is Nothing
        End Get
    End Property

    Private Sub SaveList()
        m_OriginalList = New ArrayList(Me.List)
    End Sub

    Private Sub ResetList(ByVal NewList As ArrayList)
        Me.List.Clear()
        For Each m_item As Object In NewList
            Me.Add(m_item)
        Next
    End Sub

    Public Sub Sort(ByVal propertyName As String, ByVal direction As ListSortDirection)
        If List.Count > 0 Then
            If direction = ListSortDirection.Descending Then
                propertyName = propertyName & " desc"
            End If
            Me.InnerList.Sort(New UniversalComparer(List.Item(0).GetType, propertyName))
        End If
    End Sub

    Protected Sub Sort()
        If List.Count > 0 Then
            Dim sort As String = m_SortProperty.Name
            If m_SortDirection = ListSortDirection.Descending Then
                sort = sort & " desc"
            End If
            Me.InnerList.Sort(New UniversalComparer(List.Item(0).GetType, sort))
        End If
        'Me.InnerList.Sort(New StudentComparer(m_SortProperty, m_SortDirection))
    End Sub

    Protected Sub ApplySort(ByVal [property] As System.ComponentModel.PropertyDescriptor, _
        ByVal direction As System.ComponentModel.ListSortDirection) _
        Implements System.ComponentModel.IBindingList.ApplySort
        m_SortProperty = [property]
        m_SortDirection = direction
        If (m_OriginalList Is Nothing) Then
            SaveList()
        End If
        Sort()

        RaiseEvent ListChanged(Me, New ListChangedEventArgs(ListChangedType.Reset, 0))
    End Sub

    Protected Sub RemoveSort() Implements System.ComponentModel.IBindingList.RemoveSort
        ResetList(m_OriginalList)
        m_SortDirection = Nothing
        m_SortProperty = Nothing
        RaiseEvent ListChanged(Me, New ListChangedEventArgs(ListChangedType.Reset, 0))
    End Sub
#End Region

#Region "IBindingList Other Features"
    Protected Overridable Function Find(ByVal [property] As System.ComponentModel.PropertyDescriptor, _
        ByVal key As Object) As Integer Implements System.ComponentModel.IBindingList.Find
        Throw New NotImplementedException
    End Function

    Protected Sub AddIndex(ByVal [property] As System.ComponentModel.PropertyDescriptor) _
        Implements System.ComponentModel.IBindingList.AddIndex
        Throw New NotImplementedException
    End Sub

    Protected Sub RemoveIndex(ByVal [property] As System.ComponentModel.PropertyDescriptor) _
        Implements System.ComponentModel.IBindingList.RemoveIndex
        Throw New NotImplementedException
    End Sub
#End Region
#End Region

#Region "ITypedList Implementation"
    Protected Overridable Function GetItemProperties(ByVal listAccessors() As PropertyDescriptor) _
        As PropertyDescriptorCollection _
        Implements System.ComponentModel.ITypedList.GetItemProperties
        Throw New NotImplementedException
    End Function

    Protected Overridable Function GetListName(ByVal listAccessors() As PropertyDescriptor) As String _
        Implements System.ComponentModel.ITypedList.GetListName
        Throw New NotImplementedException
    End Function
#End Region
End Class
