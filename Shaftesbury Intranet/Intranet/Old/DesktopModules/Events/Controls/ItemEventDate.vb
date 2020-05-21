
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports ASPNET.StarterKit.Communities.Events
Imports System.ComponentModel




'*********************************************************************
'
' ItemEventDate Class
'
' Represents an event date displayed in a template
'
'*********************************************************************

Public Class ItemEventDate
    Inherits ItemDate
    
    
    
    
    Public Sub New()
        CssClass = "itemEventDate"
        EnableViewState = False
    End Sub 'New
    
    
    
    
    '*********************************************************************
    '
    ' AssignContentItem Method
    '
    ' Assigns the correct content item to the date.
    '
    '*********************************************************************
    Protected Overrides Sub AssignContentItem(content As ContentInfo)
        [Date] = CType(content, EventInfo).Date
    End Sub 'AssignContentItem
End Class 'ItemEventDate 


