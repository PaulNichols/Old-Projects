Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions


Namespace ASPNET.StarterKit.Portal

    '*********************************************************************
    '
    ' PortalSecurity Class
    '
    ' The PortalSecurity class encapsulates two helper methods that enable
    ' developers to easily check the role status of the current browser client.
    '
    '*********************************************************************

    Public Class PortalSecurity

        '*********************************************************************
        '
        ' Security.Encrypt() Method
        '
        ' The Encrypt method encryts a clean string into a hashed string

        '
        '*********************************************************************

        Public Shared Function Encrypt(ByVal cleanString As String) As String
            Dim clearBytes As [Byte]()
            clearBytes = New UnicodeEncoding().GetBytes(cleanString)
            Dim hashedBytes As [Byte]() = CType(CryptoConfig.CreateFromName("MD5"), HashAlgorithm).ComputeHash(clearBytes)
            Dim hashedText As String = BitConverter.ToString(hashedBytes)
            Return hashedText
        End Function

        '*********************************************************************
        '
        ' PortalSecurity.IsInRole() Method
        '
        ' The IsInRole method enables developers to easily check the role
        ' status of the current browser client.
        '
        '*********************************************************************

        Public Shared Function IsInRole(ByVal role As String) As Boolean

            Return HttpContext.Current.User.IsInRole(role)

        End Function


        '*********************************************************************
        '
        ' PortalSecurity.IsInRoles() Method
        '
        ' The IsInRoles method enables developers to easily check the role
        ' status of the current browser client against an array of roles
        '
        '*********************************************************************

        Public Shared Function IsInRoles(ByVal roles As String) As Boolean

            Dim context As HttpContext = HttpContext.Current

            Dim role As String
            '  If roles Is Nothing Then Return True
            For Each role In roles.Split(New Char() {";"c})

                If role <> "" And Not role Is Nothing And (role = "All Users" Or context.User.IsInRole(role)) Then
                    Return True
                End If

            Next role

            Return False

        End Function

        '*********************************************************************
        '
        ' PortalSecurity.HasEditPermissions() Method
        '
        ' The HasEditPermissions method enables developers to easily check 
        ' whether the current browser client has access to edit the settings
        ' of a specified portal module
        '
        '*********************************************************************

        Public Shared Function HasEditPermissions(ByVal moduleId As Integer) As Boolean

            Dim accessRoles As String
            Dim editRoles As String

            ' Obtain SiteSettings from Current Context
            Dim siteSettings As SiteConfiguration = CType(HttpContext.Current.Items("SiteSettings"), SiteConfiguration)

            ' Find the appropriate Module in the Module table
            Dim moduleRow As SiteConfiguration._ModuleRow = siteSettings._Module.FindByModuleId(moduleId)

            editRoles = moduleRow.EditRoles
            accessRoles = moduleRow.TabRow.AccessRoles

            If PortalSecurity.IsInRoles(accessRoles) = False Or PortalSecurity.IsInRoles(editRoles) = False Then
                Return False
            Else
                Return True
            End If

        End Function

    End Class


    '*********************************************************************
    '
    ' UsersDB Class
    '
    ' The UsersDB class encapsulates all data logic necessary to add/login/query
    ' users within the Portal Users database.
    '
    ' Important Note: The UsersDB class is only used when forms-based cookie
    ' authentication is enabled within the portal.  When windows based
    ' authentication is used instead, then either the Windows SAM or Active Directory
    ' is used to store and validate all username/password credentials.
    '
    '*********************************************************************

    Public Class UserDetails
        Public Ref As String
        Public FullName As String
        Public AuthorName As String
        Public AuthorTitle As String
        Public AuthorInitials As String
        Public Email As String
        Public Password As String
        Public Id As Int32
        Public username As String

        Friend Sub Update()
            Dim users As New UsersDB
            users.UpdateUser(Id, Email, Password, FullName, Ref, AuthorName, AuthorTitle, AuthorInitials, username)
        End Sub
    End Class

    Public Class UsersDB


        '*********************************************************************
        '
        ' UsersDB.AddUser() Method <a name="AddUser"></a>
        '
        ' The AddUser method inserts a new user record into the "Users" database table.
        '
        ' Other relevant sources:
        '     + <a href="AddUser.htm" style="color:green">AddUser Stored Procedure</a>
        '
        '*********************************************************************

        Public Function AddUser(ByVal username As String, ByVal fullName As String, ByVal email As String, ByVal password As String) As Integer

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_AddUser", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterFullName As New SqlParameter("@Name", SqlDbType.NVarChar, 50)
            parameterFullName.Value = fullName
            myCommand.Parameters.Add(parameterFullName)

            Dim parameterUSerName As New SqlParameter("@username", SqlDbType.NVarChar, 50)
            parameterUSerName.Value = username
            myCommand.Parameters.Add(parameterUSerName)


            Dim parameterEmail As New SqlParameter("@Email", SqlDbType.NVarChar, 100)
            parameterEmail.Value = email
            myCommand.Parameters.Add(parameterEmail)

            Dim parameterPassword As New SqlParameter("@Password", SqlDbType.NVarChar, 50)
            parameterPassword.Value = password
            myCommand.Parameters.Add(parameterPassword)

            Dim parameterUserId As New SqlParameter("@UserID", SqlDbType.Int)
            parameterUserId.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(parameterUserId)

            ' Execute the command in a try/catch to catch duplicate username errors
            Try

                ' Open the connection and execute the Command
                myConnection.Open()
                myCommand.ExecuteNonQuery()

            Catch ex As System.Exception
                ' failed to create a new user
                Return -1

            Finally

                ' Close the Connection
                If myConnection.State = ConnectionState.Open Then
                    myConnection.Close()
                End If

            End Try

            Return CType(parameterUserId.Value, Int32)

        End Function


        '*********************************************************************
        '
        ' UsersDB.DeleteUser() Method <a name="DeleteUser"></a>
        '
        ' The DeleteUser method deleted a  user record from the "Users" database table.
        '
        ' Other relevant sources:
        '     + <a href="DeleteUser.htm" style="color:green">DeleteUser Stored Procedure</a>
        '
        '*********************************************************************

        Public Sub DeleteUser(ByVal userId As Integer)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_DeleteUser", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterUserId As New SqlParameter("@UserID", SqlDbType.Int)
            parameterUserId.Value = userId
            myCommand.Parameters.Add(parameterUserId)

            ' Open the database connection and execute the command
            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        '*********************************************************************
        '
        ' UsersDB.UpdateUser() Method <a name="DeleteUser"></a>
        '
        ' The UpdateUser method deleted a  user record from the "Users" database table.
        '
        ' Other relevant sources:
        '     + <a href="UpdateUser.htm" style="color:green">UpdateUser Stored Procedure</a>
        '
        '*********************************************************************

        Public Sub UpdateUser(ByVal userId As Integer, ByVal email As String, ByVal password As String, _
            ByVal fullname As String, ByVal ref As String, ByVal authorName As String, ByVal authorTitle As String, _
            ByVal authorInitials As String, ByVal username As String)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_UpdateUser", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterUserId As New SqlParameter("@UserID", SqlDbType.Int)
            parameterUserId.Value = userId
            myCommand.Parameters.Add(parameterUserId)

            Dim parameterAuthorInitials As New SqlParameter("@AuthorInitials", SqlDbType.NVarChar, 100)
            parameterAuthorInitials.Value = authorInitials
            myCommand.Parameters.Add(parameterAuthorInitials)

            Dim parameterEmail As New SqlParameter("@Email", SqlDbType.NVarChar, 100)
            parameterEmail.Value = email
            myCommand.Parameters.Add(parameterEmail)

            Dim parameterPassword As New SqlParameter("@Password", SqlDbType.NVarChar, 50)
            parameterPassword.Value = password
            myCommand.Parameters.Add(parameterPassword)

            Dim parameterRef As New SqlParameter("@Ref", SqlDbType.NVarChar, 50)
            parameterRef.Value = ref
            myCommand.Parameters.Add(parameterRef)

            Dim parameterAuthorName As New SqlParameter("@AuthorName", SqlDbType.NVarChar, 50)
            parameterAuthorName.Value = authorName
            myCommand.Parameters.Add(parameterAuthorName)

            Dim parameterAuthorTitle As New SqlParameter("@AuthorTitle ", SqlDbType.NVarChar, 50)
            parameterAuthorTitle.Value = authorTitle
            myCommand.Parameters.Add(parameterAuthorTitle)

            Dim parameterFullName As New SqlParameter("@FullName", SqlDbType.NVarChar, 50)
            parameterFullName.Value = fullname
            myCommand.Parameters.Add(parameterFullName)

            Dim parameterUserName As New SqlParameter("@UserName", SqlDbType.NVarChar, 50)
            parameterUserName.Value = username
            myCommand.Parameters.Add(parameterUserName)

            ' Open the database connection and execute the command
            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        '*********************************************************************
        '
        ' UsersDB.GetRolesByUser() Method <a name="GetRolesByUser"></a>
        '
        ' The DeleteUser method deleted a  user record from the "Users" database table.
        '
        ' Other relevant sources:
        '     + <a href="GetRolesByUser.htm" style="color:green">GetRolesByUser Stored Procedure</a>
        '
        '*********************************************************************

        Public Function GetRolesByUser(ByVal email As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetRolesByUser", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterEmail As New SqlParameter("@Email", SqlDbType.NVarChar, 100)
            parameterEmail.Value = email
            myCommand.Parameters.Add(parameterEmail)

            ' Open the database connection and execute the command
            myConnection.Open()
            Dim dr As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return dr

        End Function



        '*********************************************************************
        '
        ' GetSingleUser Method
        '
        ' The GetSingleUser method returns a SqlDataReader containing details
        ' about a specific user from the Users database table.
        '
        '*********************************************************************

        Public Function GetSingleUserbyUserName(ByVal userName As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetSingleUserByName", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterUserName As New SqlParameter("@UserName", SqlDbType.NVarChar, 100)
            parameterUserName.Value = userName
            myCommand.Parameters.Add(parameterUserName)

            ' Open the database connection and execute the command
            myConnection.Open()
            Dim dr As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return dr

        End Function

        Public Function GetSingleUser(ByVal userName As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetSingleUser", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameteruserName As New SqlParameter("@userName", SqlDbType.NVarChar, 100)
            parameteruserName.Value = userName
            myCommand.Parameters.Add(parameteruserName)

            ' Open the database connection and execute the command
            myConnection.Open()
            Dim dr As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return dr

        End Function

        Public Function GetAllUsers() As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetAllUsers", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure


            ' Open the database connection and execute the command
            myConnection.Open()
            Dim dr As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader
            Return dr

        End Function

        Public Function GetUserName(ByVal email As String) As String

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetSingleUserName", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterEmail As New SqlParameter("@Email", SqlDbType.NVarChar, 100)
            parameterEmail.Value = email
            myCommand.Parameters.Add(parameterEmail)

            ' Open the database connection and execute the command
            myConnection.Open()
            Dim dr As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While dr.Read
                Return CType(dr.GetValue(0), String)
            End While
        End Function

        Public Function GetFullName(ByVal email As String) As String

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetSingleUserName", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterEmail As New SqlParameter("@Email", SqlDbType.NVarChar, 100)
            parameterEmail.Value = email
            myCommand.Parameters.Add(parameterEmail)

            ' Open the database connection and execute the command
            myConnection.Open()
            Dim dr As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While dr.Read
                Return CType(dr.GetValue(1), String)
            End While
        End Function

        '*********************************************************************
        '
        ' GetRoles() Method <a name="GetRoles"></a>
        '
        ' The GetRoles method returns a list of role names for the user.
        '
        ' Other relevant sources:
        '     + <a href="GetRolesByUser.htm" style="color:green">GetRolesByUser Stored Procedure</a>
        '
        '*********************************************************************

        Public Function GetRoles(ByVal email As String) As String()

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetRolesByUser", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterEmail As New SqlParameter("@Email", SqlDbType.NVarChar, 100)
            parameterEmail.Value = email
            myCommand.Parameters.Add(parameterEmail)

            ' Open the database connection and execute the command
            Dim dr As SqlDataReader

            myConnection.Open()
            dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' create a String array from the data
            Dim userRoles As New ArrayList

            While dr.Read()
                userRoles.Add(dr("RoleName"))
            End While

            dr.Close()

            ' Return the String array of roles
            Return CType(userRoles.ToArray(GetType(String)), String())

        End Function

        '*********************************************************************
        '
        ' UsersDB.Login() Method <a name="Login"></a>
        '
        ' The Login method validates a email/password pair against credentials
        ' stored in the users database.  If the email/password pair is valid,
        ' the method returns user's name.
        '
        ' Other relevant sources:
        '     + <a href="UserLogin.htm" style="color:green">UserLogin Stored Procedure</a>
        '
        '*********************************************************************

        Public Function Login(ByVal email As String, ByVal password As String) As String

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_UserLogin", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterEmail As New SqlParameter("@Email", SqlDbType.NVarChar, 100)
            parameterEmail.Value = email
            myCommand.Parameters.Add(parameterEmail)

            Dim parameterPassword As New SqlParameter("@Password", SqlDbType.NVarChar, 50)
            parameterPassword.Value = password
            myCommand.Parameters.Add(parameterPassword)

            Dim parameterUserName As New SqlParameter("@UserName", SqlDbType.NVarChar, 100)
            parameterUserName.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(parameterUserName)

            ' Open the database connection and execute the command
            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

            If Not parameterUserName.Value Is Nothing And Not parameterUserName.Value Is System.DBNull.Value Then
                Return CStr(parameterUserName.Value).Trim()
            Else
                Return String.Empty
            End If

        End Function

    End Class

End Namespace