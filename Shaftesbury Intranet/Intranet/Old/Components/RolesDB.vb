Imports System
Imports System.Configuration
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections


Namespace ASPNET.StarterKit.Portal

	'*********************************************************************
	'
	' RolesDB Class
	'
	' Class that encapsulates all data logic necessary to add/query/delete
	' Users, Roles and security settings values within the Portal database.
	'
	'*********************************************************************

	Public Class RolesDB

		'
		' ROLES
		'
		'*********************************************************************
		'
		' GetPortalRoles() Method <a name="GetPortalRoles"></a>
		'
		' The GetPortalRoles method returns a list of all role names for the 
		' specified portal.
		'
		' Other relevant sources:
		'     + <a href="GetRolesByUser.htm" style="color:green">GetPortalRoles Stored Procedure</a>
		'
		'*********************************************************************

		Public Function GetPortalRoles(ByVal portalId As Integer) As SqlDataReader

			' Create Instance of Connection and Command Object
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
			Dim myCommand As New SqlCommand("Portal_GetPortalRoles", myConnection)

			' Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure

			' Add Parameters to SPROC
			Dim parameterPortalID As New SqlParameter("@PortalID", SqlDbType.Int, 4)
			parameterPortalID.Value = portalId
			myCommand.Parameters.Add(parameterPortalID)

			' Open the database connection and execute the command
			myConnection.Open()
			Dim dr As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

			' Return the datareader
			Return dr

		End Function


		'*********************************************************************
		'
		' AddRole() Method <a name="AddRole"></a>
		'
		' The AddRole method creates a new security role for the specified portal,
		' and returns the new RoleID value.
		'
		' Other relevant sources:
		'     + <a href="AddRole.htm" style="color:green">AddRole Stored Procedure</a>
		'
		'*********************************************************************

		Public Function AddRole(ByVal portalId As Integer, ByVal roleName As String) As Integer

			' Create Instance of Connection and Command Object
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
			Dim myCommand As New SqlCommand("Portal_AddRole", myConnection)

			' Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure

			' Add Parameters to SPROC
			Dim parameterPortalID As New SqlParameter("@PortalID", SqlDbType.Int, 4)
			parameterPortalID.Value = portalId
			myCommand.Parameters.Add(parameterPortalID)

			Dim parameterRoleName As New SqlParameter("@RoleName", SqlDbType.NVarChar, 50)
			parameterRoleName.Value = roleName
			myCommand.Parameters.Add(parameterRoleName)

			Dim parameterRoleID As New SqlParameter("@RoleID", SqlDbType.Int, 4)
			parameterRoleID.Direction = ParameterDirection.Output
			myCommand.Parameters.Add(parameterRoleID)

			' Open the database connection and execute the command
			myConnection.Open()
			myCommand.ExecuteNonQuery()
			myConnection.Close()

			' return the role id 
			Return CInt(parameterRoleID.Value)

		End Function


		'*********************************************************************
		'
		' DeleteRole() Method <a name="DeleteRole"></a>
		'
		' The DeleteRole deletes the specified role from the portal database.
		'
		' Other relevant sources:
		'     + <a href="DeleteRole.htm" style="color:green">DeleteRole Stored Procedure</a>
		'
		'*********************************************************************

		Public Sub DeleteRole(ByVal roleId As Integer)

			' Create Instance of Connection and Command Object
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
			Dim myCommand As New SqlCommand("Portal_DeleteRole", myConnection)

			' Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure

			' Add Parameters to SPROC
			Dim parameterRoleID As New SqlParameter("@RoleID", SqlDbType.Int, 4)
			parameterRoleID.Value = roleId
			myCommand.Parameters.Add(parameterRoleID)

			' Open the database connection and execute the command
			myConnection.Open()
			myCommand.ExecuteNonQuery()
			myConnection.Close()

		End Sub


		'*********************************************************************
		'
		' UpdateRole() Method <a name="UpdateRole"></a>
		'
		' The UpdateRole method updates the friendly name of the specified role.
		'
		' Other relevant sources:
		'     + <a href="UpdateRole.htm" style="color:green">UpdateRole Stored Procedure</a>
		'
		'*********************************************************************

		Public Sub UpdateRole(ByVal roleId As Integer, ByVal roleName As String)

			' Create Instance of Connection and Command Object
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
			Dim myCommand As New SqlCommand("Portal_UpdateRole", myConnection)

			' Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure

			' Add Parameters to SPROC
			Dim parameterRoleID As New SqlParameter("@RoleID", SqlDbType.Int, 4)
			parameterRoleID.Value = roleId
			myCommand.Parameters.Add(parameterRoleID)

			Dim parameterRoleName As New SqlParameter("@RoleName", SqlDbType.NVarChar, 50)
			parameterRoleName.Value = roleName
			myCommand.Parameters.Add(parameterRoleName)

			' Open the database connection and execute the command
			myConnection.Open()
			myCommand.ExecuteNonQuery()
			myConnection.Close()

		End Sub

		'
		' USER ROLES
		'
		'*********************************************************************
		'
		' GetRoleMembers() Method <a name="GetRoleMembers"></a>
		'
		' The GetRoleMembers method returns a list of all members in the specified
		' security role.
		'
		' Other relevant sources:
		'     + <a href="GetRoleMembers.htm" style="color:green">GetRoleMembers Stored Procedure</a>
		'
		'*********************************************************************

		Public Function GetRoleMembers(ByVal roleId As Integer) As SqlDataReader

			' Create Instance of Connection and Command Object
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
			Dim myCommand As New SqlCommand("Portal_GetRoleMembership", myConnection)

			' Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure

			Dim parameterRoleID As New SqlParameter("@RoleID", SqlDbType.Int, 4)
			parameterRoleID.Value = roleId
			myCommand.Parameters.Add(parameterRoleID)

			' Open the database connection and execute the command
			myConnection.Open()
			Dim dr As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

			' Return the datareader
			Return dr

		End Function


		'*********************************************************************
		'
		' AddUserRole() Method <a name="AddUserRole"></a>
		'
		' The AddUserRole method adds the user to the specified security role.
		'
		' Other relevant sources:
		'     + <a href="AddUserRole.htm" style="color:green">AddUserRole Stored Procedure</a>
		'
		'*********************************************************************

		Public Sub AddUserRole(ByVal roleId As Integer, ByVal userId As Integer)

			' Create Instance of Connection and Command Object
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
			Dim myCommand As New SqlCommand("Portal_AddUserRole", myConnection)

			' Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure

			' Add Parameters to SPROC
			Dim parameterRoleID As New SqlParameter("@RoleID", SqlDbType.Int, 4)
			parameterRoleID.Value = roleId
			myCommand.Parameters.Add(parameterRoleID)

			Dim parameterUserID As New SqlParameter("@UserID", SqlDbType.Int, 4)
			parameterUserID.Value = userId
			myCommand.Parameters.Add(parameterUserID)

			' Open the database connection and execute the command
			myConnection.Open()
			myCommand.ExecuteNonQuery()
			myConnection.Close()

		End Sub


		'*********************************************************************
		'
		' DeleteUserRole() Method <a name="DeleteUserRole"></a>
		'
		' The DeleteUserRole method deletes the user from the specified role.
		'
		' Other relevant sources:
		'     + <a href="DeleteUserRole.htm" style="color:green">DeleteUserRole Stored Procedure</a>
		'
		'*********************************************************************

		Public Sub DeleteUserRole(ByVal roleId As Integer, ByVal userId As Integer)

			' Create Instance of Connection and Command Object
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
			Dim myCommand As New SqlCommand("Portal_DeleteUserRole", myConnection)

			' Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure

			' Add Parameters to SPROC
			Dim parameterRoleID As New SqlParameter("@RoleID", SqlDbType.Int, 4)
			parameterRoleID.Value = roleId
			myCommand.Parameters.Add(parameterRoleID)

			Dim parameterUserID As New SqlParameter("@UserID", SqlDbType.Int, 4)
			parameterUserID.Value = userId
			myCommand.Parameters.Add(parameterUserID)

			' Open the database connection and execute the command
			myConnection.Open()
			myCommand.ExecuteNonQuery()
			myConnection.Close()

		End Sub


		'
		' USERS
		'
		'*********************************************************************
		'
		' GetUsers() Method <a name="GetUsers"></a>
		'
		' The GetUsers method returns returns the UserID, Name and Email for 
		' all registered users.
		'
		' Other relevant sources:
		'     + <a href="GetUsers.htm" style="color:green">GetUsers Stored Procedure</a>
		'
		'*********************************************************************

		Public Function GetUsers() As SqlDataReader

			' Create Instance of Connection and Command Object
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
			Dim myCommand As New SqlCommand("Portal_GetUsers", myConnection)

			' Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure

			' Open the database connection and execute the command
			myConnection.Open()
			Dim dr As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

			' Return the datareader
			Return dr

		End Function

	End Class

End Namespace
