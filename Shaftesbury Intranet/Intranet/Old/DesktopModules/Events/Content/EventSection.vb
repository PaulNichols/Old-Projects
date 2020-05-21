

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

Public Class EventSection
    Inherits ContentListPage

    Private _skinFileName As String = "Events_EventSection.ascx"




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
            TotalRecords = EventUtility.GetTotalRecordsWithInvisible(objSectionInfo.ID)
            GetContentItems = New GetContentItemsDelegate(AddressOf EventUtility.GetInvisibleEvents)
        Else
            TotalRecords = EventUtility.GetTotalRecords(objSectionInfo.ID)
            GetContentItems = New GetContentItemsDelegate(AddressOf EventUtility.GetVisibleEvents)
        End If

        ' call the base method
        MyBase.InitializeSkin(skin)
    End Sub 'InitializeSkin





    '*********************************************************************
    '
    ' EventSection Constructor
    '
    ' Assigns skin and contentItems method to base ContentListPage class
    '
    '*********************************************************************
    Public Sub New()
        SkinFileName = _skinFileName
    End Sub 'New
End Class 'EventSection 


End Namespace