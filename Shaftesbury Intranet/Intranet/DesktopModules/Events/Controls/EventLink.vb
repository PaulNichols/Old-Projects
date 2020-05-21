
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports ASPNET.StarterKit.Communities.Events




'*********************************************************************
'
' EventLink Class
'
' Represents an event link to other content
'
'*********************************************************************
<Designer(GetType(ASPNET.StarterKit.Communities.CommunityDesigner))>  _
Public Class EventLink
    Inherits HyperLink
    
    
    
    
    '*********************************************************************
    '
    ' EventLink Constructor
    '
    ' Assign a default css style (the user can override)
    '
    '*********************************************************************
    Public Sub New()
        CssClass = "eventFullDescription"
    End Sub 'New
     
    
    
    
    
    
    '*********************************************************************
    '
    ' Render Method
    '
    ' Don't display anything if event link is empty
    '
    '*********************************************************************
    Protected Overrides Sub Render(writer As HtmlTextWriter)
        If NavigateUrl <> String.Empty Then
            
            ' Get ContentInfo object
            Dim objEventInfo As EventInfo = CType(Context.Items("ContentInfo"), EventInfo)
            If objEventInfo.Link.StartsWith("~") Then
                NavigateUrl = Page.ResolveUrl(objEventInfo.Link)
            Else
                NavigateUrl = objEventInfo.Link
            End If 
            MyBase.Render(writer)
        End If
    End Sub 'Render
End Class 'EventLink 
