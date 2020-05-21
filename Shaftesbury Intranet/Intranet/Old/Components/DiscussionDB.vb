Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports ASPNET.StarterKit.Portal


Namespace ASPNET.StarterKit.Portal

    '*********************************************************************
    '
    ' DiscussionDB Class
    '
    ' Class that encapsulates all data logic necessary to add/query/delete
    ' discussions within the Portal database.
    '
    '*********************************************************************

    Public Class DiscussionDB


        '*******************************************************
        '
        ' GetTopLevelMessages Method
        '
        ' Returns details for all of the messages in the discussion specified by ModuleID.
        '
        ' Other relevant sources:
        '     + <a href="GetTopLevelMessages.htm" style="color:green">GetTopLevelMessages Stored Procedure</a>
        '
        '*******************************************************

        Public Function GetTopLevelMessages(ByVal moduleId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetTopLevelMessages", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterModuleId As New SqlParameter("@ModuleID", SqlDbType.Int, 4)
            parameterModuleId.Value = moduleId
            myCommand.Parameters.Add(parameterModuleId)

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        '*******************************************************
        '
        ' GetThreadMessages Method
        '
        ' Returns details for all of the messages the thread, as identified by the Parent id string.
        '
        ' Other relevant sources:
        '     + <a href="GetThreadMessages.htm" style="color:green">GetThreadMessages Stored Procedure</a>
        '
        '*******************************************************

        Public Function GetThreadMessages(ByVal parent As String) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetThreadMessages", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterParent As New SqlParameter("@Parent", SqlDbType.NVarChar, 750)
            parameterParent.Value = parent
            myCommand.Parameters.Add(parameterParent)

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        '*******************************************************
        '
        ' GetSingleMessage Method
        '
        ' The GetSingleMessage method returns the details for the message
        ' specified by the itemId parameter.
        '
        ' Other relevant sources:
        '     + <a href="GetSingleMessage.htm" style="color:green">GetSingleMessage Stored Procedure</a>
        '
        '*******************************************************

        Public Function GetSingleMessage(ByVal itemId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetSingleMessage", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterItemId As New SqlParameter("@ItemID", SqlDbType.Int, 4)
            parameterItemId.Value = itemId
            myCommand.Parameters.Add(parameterItemId)

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        '*********************************************************************
        '
        ' AddMessage Method
        '
        ' The AddMessage method adds a new message within the
        ' Discussions database table, and returns ItemID value as a result.
        '
        ' Other relevant sources:
        '     + <a href="AddMessage.htm" style="color:green">AddMessage Stored Procedure</a>
        '
        '*********************************************************************

        Public Function AddMessage(ByVal moduleId As Integer, ByVal parentId As Integer, ByVal userName As String, ByVal title As String, ByVal body As String) As Integer

            If userName.Length < 1 Then
                userName = "unknown"
            End If

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_AddMessage", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterItemID As New SqlParameter("@ItemID", SqlDbType.Int, 4)
            parameterItemID.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterTitle As New SqlParameter("@Title", SqlDbType.NVarChar, 100)
            parameterTitle.Value = title
            myCommand.Parameters.Add(parameterTitle)

            Dim parameterBody As New SqlParameter("@Body", SqlDbType.NVarChar, 3000)
            parameterBody.Value = body
            myCommand.Parameters.Add(parameterBody)

            Dim parameterParentID As New SqlParameter("@ParentID", SqlDbType.Int, 4)
            parameterParentID.Value = parentId
            myCommand.Parameters.Add(parameterParentID)

            Dim parameterUserName As New SqlParameter("@UserName", SqlDbType.NVarChar, 100)
            parameterUserName.Value = userName
            myCommand.Parameters.Add(parameterUserName)

            Dim parameterModuleID As New SqlParameter("@ModuleID", SqlDbType.Int, 4)
            parameterModuleID.Value = moduleId
            myCommand.Parameters.Add(parameterModuleID)

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

            Return CInt(parameterItemID.Value)

        End Function

    End Class

End Namespace
