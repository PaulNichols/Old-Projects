Imports Students
Imports System.ComponentModel

Public Class Form1

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboGrade.Items.AddRange(New Object() {9, 10, 11, 12})
        If (myStudentCollection.Count = 0) Then
            myStudentCollection.Add(New Student("John", "Doe", 12, 17))
            myStudentCollection.Add(New Student("Jane", "Public", 11, 16))
            myStudentCollection.Add(New Student("Bob", "Smith", 10, 15))
            myStudentCollection.Add(New Student("Mary", "Williams", 9, 14))
        End If
    End Sub
End Class
