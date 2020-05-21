

Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports ASPNET.StarterKit.Communities

Namespace Events

'*********************************************************************
'
' Event Class
'
' Represents an individual event. 
'
'  _skinFileName = the name of the skin to use for this section
'
'  _getContentItem = the name of the method that retrieves the content item 
'
'*********************************************************************

Public Class [Event]
    Inherits ContentItemPage

    Private _skinFileName As String = "Events_Event.ascx"

    Private _getContentItem As New GetContentItemDelegate(AddressOf EventUtility.GetEventInfo)



    '*********************************************************************
    '
    ' Event Constructor
    '
    ' Assigns skin and contentItems method to base ContentItemPage class
    '
    '*********************************************************************
    Public Sub New()
        SkinFileName = _skinFileName
        GetContentItem = _getContentItem
    End Sub 'New
End Class '[Event] 

End Namespace




