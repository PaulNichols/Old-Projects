Imports Students
Imports System.Xml.Serialization
Imports System.IO

Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents grdStudents As System.Windows.Forms.DataGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboGrade As System.Windows.Forms.ComboBox
    Friend WithEvents nudAge As System.Windows.Forms.NumericUpDown
    Friend WithEvents myStudentCollection As Students.StudentCollection
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grdStudents = New System.Windows.Forms.DataGrid
        Me.myStudentCollection = New Students.StudentCollection
        Me.txtFirstName = New System.Windows.Forms.TextBox
        Me.txtLastName = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.nudAge = New System.Windows.Forms.NumericUpDown
        Me.cboGrade = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        CType(Me.grdStudents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nudAge, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdStudents
        '
        Me.grdStudents.CaptionText = "Students"
        Me.grdStudents.DataMember = ""
        Me.grdStudents.DataSource = Me.myStudentCollection
        Me.grdStudents.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdStudents.Location = New System.Drawing.Point(16, 16)
        Me.grdStudents.Name = "grdStudents"
        Me.grdStudents.Size = New System.Drawing.Size(400, 168)
        Me.grdStudents.TabIndex = 0
        '
        'txtFirstName
        '
        Me.txtFirstName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.myStudentCollection, "FirstName"))
        Me.txtFirstName.Location = New System.Drawing.Point(112, 24)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.TabIndex = 3
        Me.txtFirstName.Text = ""
        '
        'txtLastName
        '
        Me.txtLastName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.myStudentCollection, "LastName"))
        Me.txtLastName.Location = New System.Drawing.Point(112, 56)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.TabIndex = 4
        Me.txtLastName.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.nudAge)
        Me.GroupBox1.Controls.Add(Me.cboGrade)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtFirstName)
        Me.GroupBox1.Controls.Add(Me.txtLastName)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 192)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(400, 152)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Student Details"
        '
        'nudAge
        '
        Me.nudAge.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.myStudentCollection, "Age"))
        Me.nudAge.Location = New System.Drawing.Point(112, 88)
        Me.nudAge.Name = "nudAge"
        Me.nudAge.Size = New System.Drawing.Size(104, 20)
        Me.nudAge.TabIndex = 14
        '
        'cboGrade
        '
        Me.cboGrade.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.myStudentCollection, "Grade"))
        Me.cboGrade.Location = New System.Drawing.Point(112, 120)
        Me.cboGrade.Name = "cboGrade"
        Me.cboGrade.Size = New System.Drawing.Size(104, 21)
        Me.cboGrade.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 23)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Grade:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 23)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Age:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 23)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Last Name:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 23)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "First Name:"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(256, 352)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(344, 352)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        '
        'Form1
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(432, 382)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grdStudents)
        Me.Name = "Form1"
        Me.Text = "Student List"
        CType(Me.grdStudents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.nudAge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " List Serialization Methods"
    Private serializer As XmlSerializer

    Private Sub LoadStudents(ByVal collection As StudentCollection)
        Dim fStream As FileStream
        Try
            Dim fName As String = CType(System.Configuration.ConfigurationSettings.AppSettings("studentFile"), String)
            serializer = New XmlSerializer(GetType(StudentCollection))
            fStream = New FileStream(fName, FileMode.Open)
            collection = CType(serializer.Deserialize(fStream), StudentCollection)
        Catch ex As Exception
            ' MessageBox.Show(ex.Message)
        Finally
            If (Not fStream Is Nothing) Then
                fStream.Close()
            End If
        End Try
    End Sub

    Private Sub SaveStudents(ByVal collection As StudentCollection)
        Dim fStream As FileStream
        Try
            Dim fName As String = CType(System.Configuration.ConfigurationSettings.AppSettings("studentFile"), String)
            fStream = New FileStream(fName, FileMode.Create)
            serializer = New XmlSerializer(GetType(StudentCollection))
            serializer.Serialize(fStream, collection)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If (Not fStream Is Nothing) Then
                fStream.Close()
            End If
        End Try
    End Sub
#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboGrade.Items.AddRange(New Object() {9, 10, 11, 12})
        LoadStudents(myStudentCollection)
        If (myStudentCollection.Count = 0) Then
            myStudentCollection.Add(New Student("John", "Doe", 12, 17))
            myStudentCollection.Add(New Student("Jane", "Public", 11, 16))
            myStudentCollection.Add(New Student("Bob", "Smith", 10, 15))
            myStudentCollection.Add(New Student("Mary", "Williams", 9, 14))
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveStudents(myStudentCollection)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class
