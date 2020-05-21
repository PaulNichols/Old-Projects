

Imports System
Imports System.Collections
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports ASPNET.StarterKit.Communities

Namespace Events

'*********************************************************************
'
' EventUtility Class
'
' Contains static utility methods used by the Events section. 
'
'*********************************************************************

Public Class EventUtility



    '*********************************************************************
    '
    ' GetTotalRecords Method
    '
    ' Returns the total number of visible events
    '
    '*********************************************************************
    Public Shared Function GetTotalRecords(ByVal sectionID As Integer) As Integer
        Dim conPortal As New SqlConnection(CommunityGlobals.ConnectionString)
        Dim cmdGetTotal As New SqlCommand("Community_EventsGetTotalRecords", conPortal)
        cmdGetTotal.CommandType = CommandType.StoredProcedure
        cmdGetTotal.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
        cmdGetTotal.Parameters.Add("@sectionID", sectionID)
        conPortal.Open()
        cmdGetTotal.ExecuteNonQuery()
        Dim totalRecords As Integer = Fix(cmdGetTotal.Parameters("@RETURN_VALUE").Value)
        conPortal.Close()

        Return totalRecords
    End Function 'GetTotalRecords




    '*********************************************************************
    '
    ' GetTotalRecordsWithInvisible Method
    '
    ' Returns the total number of visible and invisible events
    '
    '*********************************************************************
    Public Shared Function GetTotalRecordsWithInvisible(ByVal sectionID As Integer) As Integer
        Dim conPortal As New SqlConnection(CommunityGlobals.ConnectionString)
        Dim cmdGetTotal As New SqlCommand("Community_EventsGetTotalRecordsWithInvisible", conPortal)
        cmdGetTotal.CommandType = CommandType.StoredProcedure
        cmdGetTotal.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
        cmdGetTotal.Parameters.Add("@sectionID", sectionID)
        conPortal.Open()
        cmdGetTotal.ExecuteNonQuery()
        Dim totalRecords As Integer = Fix(cmdGetTotal.Parameters("@RETURN_VALUE").Value)
        conPortal.Close()

        Return totalRecords
    End Function 'GetTotalRecordsWithInvisible






    '*********************************************************************
    '
    ' GetTotalPastRecords Method
    '
    ' Returns the total number of visible past events
    '
    '*********************************************************************
    Public Shared Function GetTotalPastRecords(ByVal sectionID As Integer) As Integer
        Dim conPortal As New SqlConnection(CommunityGlobals.ConnectionString)
        Dim cmdGetTotal As New SqlCommand("Community_EventsGetTotalPastRecords", conPortal)
        cmdGetTotal.CommandType = CommandType.StoredProcedure
        cmdGetTotal.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
        cmdGetTotal.Parameters.Add("@sectionID", sectionID)
        conPortal.Open()
        cmdGetTotal.ExecuteNonQuery()
        Dim totalRecords As Integer = Fix(cmdGetTotal.Parameters("@RETURN_VALUE").Value)
        conPortal.Close()

        Return totalRecords
    End Function 'GetTotalPastRecords




    '*********************************************************************
    '
    ' GetTotalPastRecordsWithInvisible Method
    '
    ' Returns the total number of past visible and invisible events
    '
    '*********************************************************************
    Public Shared Function GetTotalPastRecordsWithInvisible(ByVal sectionID As Integer) As Integer
        Dim conPortal As New SqlConnection(CommunityGlobals.ConnectionString)
        Dim cmdGetTotal As New SqlCommand("Community_EventsGetTotalPastRecordsWithInvisible", conPortal)
        cmdGetTotal.CommandType = CommandType.StoredProcedure
        cmdGetTotal.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
        cmdGetTotal.Parameters.Add("@sectionID", sectionID)
        conPortal.Open()
        cmdGetTotal.ExecuteNonQuery()
        Dim totalRecords As Integer = Fix(cmdGetTotal.Parameters("@RETURN_VALUE").Value)
        conPortal.Close()

        Return totalRecords
    End Function 'GetTotalPastRecordsWithInvisible





    '*********************************************************************
    '
    ' AddEvent Method
    '
    ' Adds a new event to the database. 
    '
    '*********************************************************************
    Public Shared Function AddEvent(ByVal sectionID As Integer, ByVal username As String, ByVal eventTitle As String, ByVal eventLink As String, ByVal eventBriefDescription As String, ByVal eventFullDescription As String, ByVal eventLocation As String, ByVal eventSpeaker As String, ByVal eventSpeakerBiography As String, ByVal eventDate As DateTime, ByVal eventDateVisible As DateTime, ByVal topicID As Integer, ByVal moderationStatus As Integer) As Integer
        ' Update Database with new Event
        Dim conPortal As New SqlConnection(CommunityGlobals.ConnectionString)
        Dim cmdAdd As New SqlCommand("Community_EventsAddEvent", conPortal)
        cmdAdd.CommandType = CommandType.StoredProcedure

        cmdAdd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
        cmdAdd.Parameters.Add("@communityID", CommunityGlobals.CommunityID)
        cmdAdd.Parameters.Add("@sectionID", sectionID)
        cmdAdd.Parameters.Add("@username", username)
        cmdAdd.Parameters.Add("@eventTitle", eventTitle)
        cmdAdd.Parameters.Add("@eventLink", eventLink)
        cmdAdd.Parameters.Add("@eventBriefDescription", eventBriefDescription)
        cmdAdd.Parameters.Add("@eventFullDescription", SqlDbType.NText)
        cmdAdd.Parameters("@eventFullDescription").Value = eventFullDescription
        cmdAdd.Parameters.Add("@eventLocation", eventLocation)
        cmdAdd.Parameters.Add("@eventSpeaker", eventSpeaker)
        cmdAdd.Parameters.Add("@eventSpeakerBiography", eventSpeakerBiography)
        cmdAdd.Parameters.Add("@eventMetaDescription", ContentPageUtility.CalculateMetaDescription(eventBriefDescription))
        cmdAdd.Parameters.Add("@eventMetaKeys", ContentPageUtility.CalculateMetaKeys(eventBriefDescription))
        cmdAdd.Parameters.Add("@eventDate", eventDate)
        cmdAdd.Parameters.Add("@eventDateVisible", eventDateVisible)
        cmdAdd.Parameters.Add("@topicID", topicID)
        cmdAdd.Parameters.Add("@moderationStatus", moderationStatus)

        conPortal.Open()
        cmdAdd.ExecuteNonQuery()
        Dim result As Integer = Fix(cmdAdd.Parameters("@RETURN_VALUE").Value)

        ' Add Search Keys
        SearchUtility.AddSearchKeys(conPortal, sectionID, result, eventTitle, eventBriefDescription)


        conPortal.Close()

        Return result
    End Function 'AddEvent



    '*********************************************************************
    '
    ' EditEvent Method
    '
    ' Edits an existing event in the database. 
    '
    '*********************************************************************
    Public Shared Sub EditEvent(ByVal username As String, ByVal sectionID As Integer, ByVal contentPageID As Integer, ByVal eventTitle As String, ByVal eventLink As String, ByVal eventBriefDescription As String, ByVal eventFullDescription As String, ByVal eventLocation As String, ByVal eventSpeaker As String, ByVal eventSpeakerBiography As String, ByVal eventDate As DateTime, ByVal eventDateVisible As DateTime, ByVal topicID As Integer)
        ' Update Database with new Event
        Dim conPortal As New SqlConnection(CommunityGlobals.ConnectionString)
        Dim cmdAdd As New SqlCommand("Community_EventsEditEvent", conPortal)
        cmdAdd.CommandType = CommandType.StoredProcedure

        cmdAdd.Parameters.Add("@communityID", CommunityGlobals.CommunityID)
        cmdAdd.Parameters.Add("@contentPageID", contentPageID)
        cmdAdd.Parameters.Add("@username", username)
        cmdAdd.Parameters.Add("@eventTitle", eventTitle)
        cmdAdd.Parameters.Add("@eventLink", eventLink)
        cmdAdd.Parameters.Add("@eventBriefDescription", eventBriefDescription)
        cmdAdd.Parameters.Add("@eventFullDescription", SqlDbType.NText)
        cmdAdd.Parameters("@eventFullDescription").Value = eventFullDescription
        cmdAdd.Parameters.Add("@eventLocation", eventLocation)
        cmdAdd.Parameters.Add("@eventSpeaker", eventSpeaker)
        cmdAdd.Parameters.Add("@eventSpeakerBiography", eventSpeakerBiography)
        cmdAdd.Parameters.Add("@eventMetaDescription", ContentPageUtility.CalculateMetaDescription(eventBriefDescription))
        cmdAdd.Parameters.Add("@eventMetaKeys", ContentPageUtility.CalculateMetaKeys(eventBriefDescription))
        cmdAdd.Parameters.Add("@eventDate", eventDate)
        cmdAdd.Parameters.Add("@eventDateVisible", eventDateVisible)
        cmdAdd.Parameters.Add("@topicID", topicID)

        conPortal.Open()
        cmdAdd.ExecuteNonQuery()


        ' Edit Search Keys
        SearchUtility.EditSearchKeys(conPortal, sectionID, contentPageID, eventTitle, eventBriefDescription)


        conPortal.Close()
    End Sub 'EditEvent




    '*********************************************************************
    '
    ' GetVisibleEvents Method
    '
    ' Gets a list of visible future events for this section from the database. 
    '
    '*********************************************************************
    Public Shared Function GetVisibleEvents(ByVal username As String, ByVal sectionID As Integer, ByVal pageSize As Integer, ByVal pageIndex As Integer, ByVal sortOrder As String) As ArrayList
        Return GetEvents(False, username, sectionID, pageSize, pageIndex, sortOrder)
    End Function 'GetVisibleEvents



    '*********************************************************************
    '
    ' GetInvisibleEvents Method
    '
    ' Gets a list of visible and invisible future events for this section 
    ' from the database. 
    '
    '*********************************************************************
    Public Shared Function GetInvisibleEvents(ByVal username As String, ByVal sectionID As Integer, ByVal pageSize As Integer, ByVal pageIndex As Integer, ByVal sortOrder As String) As ArrayList
        Return GetEvents(True, username, sectionID, pageSize, pageIndex, sortOrder)
    End Function 'GetInvisibleEvents



    '*********************************************************************
    '
    ' GetEvents Method
    '
    ' Gets a list of future events for this section from the database. 
    '
    '*********************************************************************
    Public Shared Function GetEvents(ByVal showInvisible As Boolean, ByVal username As String, ByVal sectionID As Integer, ByVal pageSize As Integer, ByVal pageIndex As Integer, ByVal sortOrder As String) As ArrayList
        Dim colEvents As New ArrayList()

        Dim conPortal As New SqlConnection(CommunityGlobals.ConnectionString)
        Dim cmdGet As New SqlCommand("Community_EventsGetEvents", conPortal)
        cmdGet.CommandType = CommandType.StoredProcedure
        cmdGet.Parameters.Add("@communityID", CommunityGlobals.CommunityID)
        cmdGet.Parameters.Add("@username", username)
        cmdGet.Parameters.Add("@sectionID", sectionID)
        cmdGet.Parameters.Add("@pageSize", pageSize)
        cmdGet.Parameters.Add("@pageIndex", pageIndex)
        cmdGet.Parameters.Add("@sortOrder", sortOrder)
        cmdGet.Parameters.Add("@showInvisible", showInvisible)

        conPortal.Open()
        Dim dr As SqlDataReader = cmdGet.ExecuteReader()
        While dr.Read()
            colEvents.Add(New EventInfo(dr))
        End While
        conPortal.Close()

        Return colEvents
    End Function 'GetEvents



    '*********************************************************************
    '
    ' GetVisiblePastEvents Method
    '
    ' Gets a list of visible future events for this section from the database. 
    '
    '*********************************************************************
    Public Shared Function GetVisiblePastEvents(ByVal username As String, ByVal sectionID As Integer, ByVal pageSize As Integer, ByVal pageIndex As Integer, ByVal sortOrder As String) As ArrayList
        Return GetPastEvents(False, username, sectionID, pageSize, pageIndex, sortOrder)
    End Function 'GetVisiblePastEvents



    '*********************************************************************
    '
    ' GetInvisiblePastEvents Method
    '
    ' Gets a list of visible and invisible future events for this section 
    ' from the database. 
    '
    '*********************************************************************
    Public Shared Function GetInvisiblePastEvents(ByVal username As String, ByVal sectionID As Integer, ByVal pageSize As Integer, ByVal pageIndex As Integer, ByVal sortOrder As String) As ArrayList
        Return GetPastEvents(True, username, sectionID, pageSize, pageIndex, sortOrder)
    End Function 'GetInvisiblePastEvents




    '*********************************************************************
    '
    ' GetPastEvents Method
    '
    ' Gets a list of past events for this section from the database. 
    '
    '*********************************************************************
    Public Shared Function GetPastEvents(ByVal showInvisible As Boolean, ByVal username As String, ByVal sectionID As Integer, ByVal pageSize As Integer, ByVal pageIndex As Integer, ByVal sortOrder As String) As ArrayList
        Dim colEvents As New ArrayList()

        Dim conPortal As New SqlConnection(CommunityGlobals.ConnectionString)
        Dim cmdGet As New SqlCommand("Community_EventsGetPastEvents", conPortal)
        cmdGet.CommandType = CommandType.StoredProcedure
        cmdGet.Parameters.Add("@communityID", CommunityGlobals.CommunityID)
        cmdGet.Parameters.Add("@username", username)
        cmdGet.Parameters.Add("@sectionID", sectionID)
        cmdGet.Parameters.Add("@pageSize", pageSize)
        cmdGet.Parameters.Add("@pageIndex", pageIndex)
        cmdGet.Parameters.Add("@sortOrder", sortOrder)
        cmdGet.Parameters.Add("@showInvisible", showInvisible)

        conPortal.Open()
        Dim dr As SqlDataReader = cmdGet.ExecuteReader()
        While dr.Read()
            colEvents.Add(New EventInfo(dr))
        End While
        conPortal.Close()
        Return colEvents
    End Function 'GetPastEvents



    '*********************************************************************
    '
    ' GetEventInfo Method
    '
    ' Gets a particular event from the database. 
    '
    '*********************************************************************
    Public Shared Function GetEventInfo(ByVal username As String, ByVal contentPageID As Integer) As ContentInfo
        Dim _eventInfo As EventInfo = Nothing

        Dim conPortal As New SqlConnection(CommunityGlobals.ConnectionString)
        Dim cmdGet As New SqlCommand("Community_EventsGetEvent", conPortal)

        ' Add Parameters
        cmdGet.CommandType = CommandType.StoredProcedure
        cmdGet.Parameters.Add("@communityID", CommunityGlobals.CommunityID)
        cmdGet.Parameters.Add("@username", username)
        cmdGet.Parameters.Add("@contentPageID", contentPageID)

        conPortal.Open()
        Dim dr As SqlDataReader = cmdGet.ExecuteReader()
        If dr.Read() Then
            _eventInfo = New EventInfo(dr)
        End If
        conPortal.Close()
        Return CType(_eventInfo, ContentInfo)
    End Function 'GetEventInfo
End Class 'EventUtility 

End Namespace