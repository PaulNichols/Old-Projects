Imports Students.Student
Imports System.ComponentModel

Public Class StudentCollection
    Inherits BindingList(Of Student)
    Implements IComponent

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
        Me.Items.Clear()
        RaiseEvent Disposed(Me, System.EventArgs.Empty)
    End Sub
#End Region

#Region "IBindingList Sorting Features"
    Private m_SupportsSorting As Boolean = True
    Private m_SortProperty As PropertyDescriptor
    Private m_SortDirection As ListSortDirection
    Private m_OriginalList As ArrayList

    Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
        Get
            Return m_SupportsSorting
        End Get
    End Property
    Protected Overrides ReadOnly Property SortDirectionCore() As System.ComponentModel.ListSortDirection
        Get
            Return m_SortDirection
        End Get
    End Property

    Protected Overrides ReadOnly Property SortPropertyCore() As System.ComponentModel.PropertyDescriptor
        Get
            Return m_SortProperty
        End Get
    End Property

    Protected Overrides ReadOnly Property IsSortedCore() As Boolean
        Get
            Return m_SortProperty Is Nothing
        End Get
    End Property

    Private Sub SaveList()
        m_OriginalList = New ArrayList(Me.Items)
    End Sub

    Private Sub ResetList(ByVal NewList As ArrayList)
        Me.Items.Clear()
        For Each m_Student As Student In NewList
            Me.Add(m_Student)
        Next
    End Sub

    Private Sub DoSort()
        Dim m_Comparer As New StudentComparer(m_SortProperty, m_SortDirection)
        Dim m_SortList As New ArrayList(Me.Items)
        m_SortList.Sort(m_Comparer)
        ResetList(m_SortList)
    End Sub

    Protected Overrides Sub ApplySortCore(ByVal prop As System.ComponentModel.PropertyDescriptor, ByVal direction As System.ComponentModel.ListSortDirection)
        m_SortProperty = prop
        m_SortDirection = direction
        If (m_OriginalList Is Nothing) Then
            SaveList()
        End If
        DoSort()
    End Sub

    Protected Overrides Sub RemoveSortCore()
        ResetList(m_OriginalList)
        m_SortDirection = Nothing
        m_SortProperty = Nothing
    End Sub
#End Region
End Class
