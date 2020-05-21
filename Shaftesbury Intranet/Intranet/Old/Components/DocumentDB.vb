Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports ASPNET.StarterKit.Portal


Namespace ASPNET.StarterKit.Portal

    '*********************************************************************
    '
    ' DocumentDB Class
    '
    ' Class that encapsulates all data logic necessary to add/query/delete
    ' documents within the Portal database.
    '
    '*********************************************************************

    Public Class DocumentDB

        '*********************************************************************
        '
        ' GetDocuments Method
        '
        ' The GetDocuments method returns a SqlDataReader containing all of the
        ' documents for a specific portal module from the documents
        ' database.
        '
        ' Other relevant sources:
        '     + <a href="GetDocuments.htm" style="color:green">GetDocuments Stored Procedure</a>
        '
        '*********************************************************************

        Public Function GetDocuments(ByVal moduleId As Integer, ByVal userid As Int32) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetDocuments", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterModuleId As New SqlParameter("@ModuleID", SqlDbType.Int, 4)
            parameterModuleId.Value = moduleId
            myCommand.Parameters.Add(parameterModuleId)

            Dim parameterUserId As New SqlParameter("@UserID", SqlDbType.Int, 4)
            parameterUserId.Value = userid
            myCommand.Parameters.Add(parameterUserId)
            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        Public Function GetDocumentUsers(ByVal docId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("GetDocumentUsers", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterDocId As New SqlParameter("@DocId", SqlDbType.Int, 4)
            parameterdocId.Value = docId
            myCommand.Parameters.Add(parameterdocId)

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        '*********************************************************************
        '
        ' GetSingleDocument Method
        '
        ' The GetSingleDocument method returns a SqlDataReader containing details
        ' about a specific document from the Documents database table.
        '
        ' Other relevant sources:
        '     + <a href="GetSingleDocument.htm" style="color:green">GetSingleDocument Stored Procedure</a>
        '
        '*********************************************************************

        Public Function GetSingleDocument(ByVal itemId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetSingleDocument", myConnection)

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

        Public Function GetUnassignedUsers(ByVal DocId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetAllUsers", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterDocId As New SqlParameter("@DocId", SqlDbType.Int, 4)
            parameterDocId.Value = DocId
            myCommand.Parameters.Add(parameterDocId)

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function

        '*********************************************************************
        '
        ' GetDocumentContent Method
        '
        ' The GetDocumentContent method returns the contents of the specified
        ' document from the Documents database table.
        '
        ' Other relevant sources:
        '     + <a href="GetDocumentContent.htm" style="color:green">GetDocumentContent</a>
        '
        '*********************************************************************

        Public Function GetDocumentContent(ByVal itemId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetDocumentContent", myConnection)

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
        ' DeleteDocument Method
        '
        ' The DeleteDocument method deletes the specified document from
        ' the Documents database table.
        '
        ' Other relevant sources:
        '     + <a href="DeleteDocument.htm" style="color:green">DeleteDocument Stored Procedure</a>
        '
        '*********************************************************************

        Public Sub DeleteDocument(ByVal itemID As Integer)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_DeleteDocument", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterItemID As New SqlParameter("@ItemID", SqlDbType.Int, 4)
            parameterItemID.Value = itemID
            myCommand.Parameters.Add(parameterItemID)

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        '*********************************************************************
        '
        ' UpdateDocument Method
        '
        ' The UpdateDocument method updates the specified document within
        ' the Documents database table.
        '
        ' Other relevant sources:
        '     + <a href="UpdateDocument.htm" style="color:green">UpdateDocument Stored Procedure</a>
        '
        '*********************************************************************

        Public Sub UpdateDocument(ByVal moduleId As Integer, ByVal itemId As Integer, ByVal userId As Int32, _
            ByVal name As String, ByVal url As String, ByVal category As String, ByVal content() As Byte, _
            ByVal size As Integer, ByVal contentType As String, ByVal description As String, ByVal templatePage As String)

            'If userName.Length < 1 Then
            '    userName = "unknown"
            'End If

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_UpdateDocument", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterItemID As New SqlParameter("@ItemID", SqlDbType.Int, 4)
            parameterItemID.Value = itemId
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterModuleID As New SqlParameter("@ModuleID", SqlDbType.Int, 4)
            parameterModuleID.Value = moduleId
            myCommand.Parameters.Add(parameterModuleID)

            Dim parameterUserId As New SqlParameter("@UserId", SqlDbType.Int, 4)
            parameterUserId.Value = userId
            myCommand.Parameters.Add(parameterUserId)

            Dim parameterName As New SqlParameter("@FileFriendlyName", SqlDbType.NVarChar, 150)
            parameterName.Value = name
            myCommand.Parameters.Add(parameterName)

            Dim parameterFileUrl As New SqlParameter("@FileNameUrl", SqlDbType.NVarChar, 250)
            parameterFileUrl.Value = url
            myCommand.Parameters.Add(parameterFileUrl)

            Dim parameterCategory As New SqlParameter("@Category", SqlDbType.NVarChar, 50)
            parameterCategory.Value = category
            myCommand.Parameters.Add(parameterCategory)

            Dim parameterContent As New SqlParameter("@Content", SqlDbType.Image)
            parameterContent.Value = content
            myCommand.Parameters.Add(parameterContent)

            Dim parameterContentType As New SqlParameter("@ContentType", SqlDbType.NVarChar, 50)
            parameterContentType.Value = contentType
            myCommand.Parameters.Add(parameterContentType)

            Dim parameterContentSize As New SqlParameter("@ContentSize", SqlDbType.Int, 4)
            parameterContentSize.Value = size
            myCommand.Parameters.Add(parameterContentSize)

            Dim parameterDescription As New SqlParameter("@Description", SqlDbType.NVarChar, 100)
            parameterDescription.Value = description
            myCommand.Parameters.Add(parameterDescription)

            Dim parameterTempatePage As New SqlParameter("@TemplatePage", SqlDbType.NVarChar, 100)
            parameterTempatePage.Value = templatePage
            myCommand.Parameters.Add(parameterTempatePage)

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub

        Public Sub RemoveDocumentUser(ByVal docid As Int32, ByVal userid As Int32)

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("RemoveDocUser", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterUserId As New SqlParameter("@UserId", SqlDbType.Int, 4)
            parameterUserId.Value = userid
            myCommand.Parameters.Add(parameterUserId)

            Dim parameterdocid As New SqlParameter("@Docid", SqlDbType.Int, 4)
            parameterdocid.Value = docid
            myCommand.Parameters.Add(parameterdocid)

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub

        Public Sub AddDocumentUser(ByVal docid As Int32, ByVal userid As Int32)

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("AddDocUser", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            Dim parameterUserId As New SqlParameter("@UserId", SqlDbType.Int, 4)
            parameterUserId.Value = userid
            myCommand.Parameters.Add(parameterUserId)

            Dim parameterdocid As New SqlParameter("@Docid", SqlDbType.Int, 4)
            parameterdocid.Value = docid
            myCommand.Parameters.Add(parameterdocid)

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()
        End Sub
    End Class

End Namespace
