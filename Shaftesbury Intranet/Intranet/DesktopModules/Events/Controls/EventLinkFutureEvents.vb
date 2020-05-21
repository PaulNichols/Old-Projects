
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports ASPNET.StarterKit.Communities.Events




'*********************************************************************
'
' EventLinkFutureEvents Class
'
' Represents an event link to other content
'
'*********************************************************************
<Designer(GetType(ASPNET.StarterKit.Communities.CommunityDesigner))>  _
Public Class EventLinkFutureEvents
    Inherits HyperLink
    
    
    
    
    '*********************************************************************
    '
    ' EventLinkFutureEvents Constructor
    '
    ' Assign a default css style (the user can override)
    '
    '*********************************************************************
    Public Sub New()
        CssClass = "eventLinkFutureEvents"
    End Sub 'New
    
    
    
    
    '*********************************************************************
    '
    ' Render Method
    '
    ' Link to the PastEvents page
    '
    '*********************************************************************
    Protected Overrides Sub Render(writer As HtmlTextWriter)
        ' Get ContentInfo object
        If Not (Context Is Nothing) Then
            NavigateUrl = CommunityGlobals.CalculatePath("Default.aspx")
        End If
        MyBase.Render(writer)
    End Sub 'Render
End Class 'EventLinkFutureEvents 
