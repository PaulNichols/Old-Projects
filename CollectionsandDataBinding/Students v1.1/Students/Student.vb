Imports System.ComponentModel

Public MustInherit Class BaseEntity
    Implements IEditableObject

#Region "IEditableObject Interface"
    Protected m_OriginalData As Hashtable
    Protected m_Editing As Boolean = False
    Protected m_IsAddNew As Boolean = False


    Public Sub BeginEdit() Implements System.ComponentModel.IEditableObject.BeginEdit
        If Not m_Editing Then
            m_Editing = True
            m_OriginalData = New Hashtable
            Dim Properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(Me)
            For Each prop As PropertyDescriptor In Properties
                m_OriginalData.Add(prop, prop.GetValue(Me))
            Next
        End If
    End Sub

    Public Sub EndEdit() Implements System.ComponentModel.IEditableObject.EndEdit
        If m_Editing Then
            If (m_IsAddNew) Then
                OnCancelAddNew(False)
            End If
            m_OriginalData = Nothing
            m_Editing = False
        End If
    End Sub

    Public Sub CancelEdit() Implements System.ComponentModel.IEditableObject.CancelEdit
        If m_Editing Then
            If (m_IsAddNew) Then
                OnCancelAddNew(True)
            Else
                Dim prop As PropertyDescriptor
                For Each entry As DictionaryEntry In m_OriginalData
                    prop = CType(entry.Key, PropertyDescriptor)
                    prop.SetValue(Me, entry.Value)
                Next
                m_OriginalData = Nothing
                m_Editing = False
            End If
        End If
    End Sub

    Public Overridable Sub OnCancelAddNew(ByVal remove As Boolean)

    End Sub

#End Region

End Class

Public Class Student
    Inherits BaseEntity

    Private m_FirstName As String
    Private m_LastName As String
    Private m_Grade As GradeClass
    Private m_Age As Integer

    Public Event FirstNameChanged As System.EventHandler
    Public Event LastNameChanged As System.EventHandler
    Public Event GradeChanged As System.EventHandler
    Public Event AgeChanged As System.EventHandler

#Region "IEditableObject Interface"
    Public Event CancelAddNew(ByVal sender As Student, ByVal Remove As Boolean)

    Friend Sub New(ByVal IsAddNew As Boolean)
        Me.New(String.Empty, String.Empty)
        m_IsAddNew = IsAddNew
    End Sub

    Public Overrides Sub OnCancelAddNew(ByVal remove As Boolean)
        RaiseEvent CancelAddNew(Me, remove)
    End Sub
#End Region

    Public Sub New()
        MyBase.New()
    End Sub



    Public Sub New(ByVal FirstName As String, ByVal LastName As String, _
                    Optional ByVal Grade As Integer = 0, Optional ByVal Age As Integer = 0)
        With Me
            .m_FirstName = FirstName
            .m_LastName = LastName
            .m_Grade = New GradeClass(Grade)
            .m_Age = Age
        End With
    End Sub

    Public Property FirstName() As String
        Get
            Return m_FirstName
        End Get
        Set(ByVal Value As String)
            m_FirstName = Value
            RaiseEvent FirstNameChanged(Me, EventArgs.Empty)
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return m_LastName
        End Get
        Set(ByVal Value As String)
            m_LastName = Value
            RaiseEvent LastNameChanged(Me, EventArgs.Empty)
        End Set
    End Property

    Public Class GradeClass

        Public Sub New()

        End Sub
        Public Property Grade() As Int32
            Get
                Return Me.mgrade
            End Get
            Set(ByVal Value As Int32)
                Me.mgrade = Value
            End Set
        End Property

        Private mgrade As Int32


        Public Sub New(ByVal grade As Int32)
            mgrade = grade
        End Sub
    End Class

    Public ReadOnly Property Grade() As Int32
        Get
            Return m_Grade.Grade
        End Get
        'Set(ByVal Value As Int32)
        '    m_Grade = Value
        '    RaiseEvent GradeChanged(Me, EventArgs.Empty)
        'End Set
    End Property

    Public Property Age() As Integer
        Get
            Return m_Age
        End Get
        Set(ByVal Value As Integer)
            m_Age = Value
            RaiseEvent AgeChanged(Me, EventArgs.Empty)
        End Set
    End Property


End Class
