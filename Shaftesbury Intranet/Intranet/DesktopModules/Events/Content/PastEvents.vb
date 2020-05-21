

Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports ASPNET.StarterKit.Communities

Namespace Events

'*********************************************************************
'
' EventSection Class
'
' Represents the default page for the Events section. This class 
' displays a list of event listings.
'
'  _skinFileName = the name of the skin to use for this section
'
'  _getContentItems = the name of the method that retrieves the list of content items 
'
'*********************************************************************

Public Class PastEvents
    Inherits ContentListPage

    Private _skinFileName As String = "Events_PastEvents.ascx"




    '*********************************************************************
    '
    ' InitializeSkin Method
    '
    ' In this case, we need to override the InitializeSkin method since
    ' we need to change the total records handler.
    '
    '*********************************************************************
    Protected Overrides Sub InitializeSkin(ByVal skin As Control)
        ' Change the total records depending on edit ability
        If objUserInfo.MayEdit Then
            TotalRecords = EventUtility.GetTotalPastRecordsWithInvisible(objSectionInfo.ID)
            GetContentItems = New GetContentItemsDelegate(AddressOf EventUtility.GetInvisiblePastEvents)
        Else
            TotalRecords = EventUtility.GetTotalPastRecords(objSectionInfo.ID)
            GetContentItems = New GetContentItemsDelegate(AddressOf EventUtility.GetVisiblePastEvents)
        End If

        ' call the base method
        MyBase.InitializeSkin(skin)
    End Sub 'InitializeSkin





    '*********************************************************************
    '
    ' PastEvents Constructor
    '
    ' Assigns skin and contentItems method to base ContentListPage class
    '
    '*********************************************************************
    Public Sub New()
        SkinFileName = _skinFileName
    End Sub 'New
End Class 'PastEvents 


End Namespace