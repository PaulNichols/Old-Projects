
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports ASPNET.StarterKit.Communities.Events
Imports System.ComponentModel




'*********************************************************************
'
' ItemEventDateVisible Class
'
' Displays date that event will be displayed in a template
'
'*********************************************************************

Public Class ItemEventDateVisible
    Inherits ItemDate
    
    
    
    Public Sub New()
        CssClass = "itemEventDateVisible"
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
        [Date] = CType(content, EventInfo).DateVisible
    End Sub 'AssignContentItem
    
    
    
    
    Protected Overrides ReadOnly Property TagKey() As HtmlTextWriterTag
        Get
            Return HtmlTextWriterTag.Div
        End Get
    End Property
     
    
    Protected Overrides Sub Render(writer As HtmlTextWriter)
        If [Date] > DateTime.Now Then
            MyBase.Render(writer)
        End If
    End Sub 'Render 
End Class 'ItemEventDateVisible 
