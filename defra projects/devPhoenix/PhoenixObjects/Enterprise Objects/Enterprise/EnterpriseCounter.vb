Imports System.Diagnostics

Public Class EnterpriseCounter

    ' members...
    Public Name As String
    Public HelpText As String
    Public Type As PerformanceCounterType
    Public Counter As PerformanceCounter
    Private _instances As New Hashtable()

    Public Sub New()

    End Sub

    Public Sub New(ByVal name As String, ByVal helpText As String, ByVal type As PerformanceCounterType)
        Me.Name = name
        Me.HelpText = helpText
        Me.Type = type
    End Sub

    Public Sub RegisterInstance(ByVal theObject As Object)

        ' do we have this object?
        Dim hashCode As Integer = theObject.GetHashCode()
        If _instances.Contains(hashCode) = False Then
            _instances.Add(hashCode, Nothing)
            Counter.Increment()
        End If

    End Sub

    Public Sub DeregisterInstance(ByVal theObject As Object)

        ' get the hashcode...
        Dim hashCode As Integer = theObject.GetHashCode()
        If _instances.Contains(hashCode) = True Then
            _instances.Remove(hashCode)
            Counter.Decrement()
        End If

    End Sub

End Class
