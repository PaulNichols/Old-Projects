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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 272)
        Me.Name = "Form1"
        Me.Text = "Form1"

    End Sub

#End Region
    Private mFolderName As String
    Private mUserId As Int32
    Private mContactsAdded As Int32

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mContactsAdded = 0
        Dim OutlookApp As New Outlook.Application
        Dim Folders As Outlook.Folders = OutlookApp.GetNamespace("MAPI").Folders
        mFolderName = InputBox("FolderName", , "Contacts")
        mUserId = CType(InputBox("UserId", , "7"), Int32)
        'Dim Connection As New SqlClient.SqlConnection("Persist Security Info=False;Integrated Security=SSPI;database=Intranet;server=(local)")
        Dim Connection As New SqlClient.SqlConnection("Trusted_Connection=true;database=Intranet;server=shaftfin01")
        Try
            Connection.Open()
        Catch ex As System.Exception
            MessageBox.Show(ex.Message)
        End Try
        RecurseFolders(Folders, Connection)

        Connection.Close()
        Connection.Dispose()
        MessageBox.Show(Me.mContactsAdded, "Contacts Added")
    End Sub

    Private Sub RecurseFolders(ByVal folders As Outlook.Folders, ByVal Connection As SqlClient.SqlConnection)
        With folders
            Dim Folder As Outlook.MAPIFolder
            Dim i As Int32
            For i = 1 To .Count
                Folder = .Item(i)
                If Folder.Name = "Public Folders" OrElse Folder.Name = "All Public Folders" Then
                    RecurseFolders(Folder.Folders, Connection)

                Else
                    '   MessageBox.Show(Folder.Name)
                    If Folder.Name = "Contacts" Then
                        'MessageBox.Show(Folder.DefaultItemType.ToString)
                        'If Folder.DefaultItemType = Outlook.OlItemType.olContactItem Then

                        '  If Folder.Name = mFolderName Then
                        Dim j As Int32
                        MessageBox.Show(Folder.Items.Count)

                        Dim Contact As Outlook.ContactItem

                        For j = 1 To Folder.Items.Count

                            Contact = Folder.Items.Item(j)
                            If Not SavePrivateContact(Contact, mUserId, Connection) Then
                                MessageBox.Show("4")
                            End If
                        Next
                        ' End If
                        'End If
                        'If Folder.Folders.Count > 0 Then
                        '    RecurseFolders(Folder.Folders, Connection)
                        'End If
                        Exit For
                    Else
                        ' MessageBox.Show("2")
                    End If
                End If
            Next i
        End With
    End Sub

    Private Function SavePrivateContact(ByVal contact As Outlook.ContactItem, ByVal userId As Int32, ByVal connection As SqlClient.SqlConnection) As Boolean
        'don't worry about replication until this becomes a service
        'then only worry about replication for this user
        Dim Command As New SqlClient.SqlCommand
        With Command
            .Connection = connection

            .CommandType = CommandType.Text

            .CommandText = "select count(fullname) from Portal_Contacts where FullName='" & ParseSQL(contact.FullName) & "' and email='" & ParseSQL(contact.Email1Address) & "'"
            '  MessageBox.Show("5")
            Try
                If .ExecuteScalar > 0 Then
                    ' MessageBox.Show("FullName='" & ParseSQL(contact.FullName) & "' and email='" & ParseSQL(contact.Email1Address) & "'")
                    Return True
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            .CommandText = "insert into Portal_Contacts (ModuleId,fileas,email,email2,createdbyuser," & _
                "fullname, [company name],mobile,businessphone,BusinessAddress,HomeAddress,Suffix,Title," & _
                "HomePhone,HomeFax,BusinessFax,Salutation,FirstName,LastName,BusinessHomePage,userid) values(35, '" & ParseSQL(contact.FileAs) & _
                    "','" & ParseSQL(contact.Email1Address) & "','" & ParseSQL(contact.Email2Address) & "',7,'" & _
                    ParseSQL(contact.FullName) & "','" & ParseSQL(contact.CompanyName) & "','" & ParseSQL(contact.MobileTelephoneNumber) & _
                    "','" & ParseSQL(contact.BusinessTelephoneNumber) & "','" & ParseSQL(contact.BusinessAddress) & "','" & ParseSQL(contact.HomeAddress) & _
                    "','" & ParseSQL(contact.Suffix) & "','" & ParseSQL(contact.Title) & "','" & ParseSQL(contact.HomeTelephoneNumber) & "','" & _
                    ParseSQL(contact.HomeFaxNumber) & "','" & ParseSQL(contact.BusinessFaxNumber) & "','" & ParseSQL(contact.FullName) & "','" & _
                    ParseSQL(contact.FirstName) & "','" & ParseSQL(contact.LastName) & "','" & ParseSQL(contact.WebPage) & "'," & userId & ")"





            Dim ReturnState As Boolean
            Try

                mContactsAdded += .ExecuteNonQuery
                ReturnState = mContactsAdded > 0
            Catch ex As Exception
                MessageBox.Show(ex.Message, "5")
                Return False
            End Try


            Command.Dispose()
            Return ReturnState
        End With

    End Function

    Private Function ParseSQL(ByVal sql As String) As String
        If Not sql Is Nothing Then
            If sql.IndexOf("'") <> -1 Then sql = sql.Replace("'", "''")
            If sql.IndexOf("""") <> -1 Then sql = sql.Replace("""", """""")
        End If
        Return sql
    End Function

End Class

