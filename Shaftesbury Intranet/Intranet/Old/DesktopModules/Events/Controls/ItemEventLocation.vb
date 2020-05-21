
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports ASPNET.StarterKit.Communities.Events
Imports System.ComponentModel




'*********************************************************************
'
' ItemEventLocation Class
'
' Represents an event location displayed in a template
'
'*********************************************************************

Public Class ItemEventLocation
    Inherits WebControl
    
    Private _formatString As String = "location: {0}"
    
    
    
    '*********************************************************************
    '
    ' ItemEventLocation Constructor
    '
    ' Assign a default css class
    '
    '*********************************************************************
    Public Sub New()
        CssClass = "itemEventLocation"
        EnableViewState = False
    End Sub 'New
    
    
    '*********************************************************************
    '
    ' FormatString Property
    '
    ' String used for formatting location
    '
    '*********************************************************************
    
    Public Property FormatString() As String
        Get
            Return _formatString
        End Get
        Set
            _formatString = value
        End Set
    End Property
     
    
    '*********************************************************************
    '
    ' OnDataBinding Method
    '
    ' Get the content info from the container
    '
    '*********************************************************************
    Protected Overrides Sub OnDataBinding(e As EventArgs)
        Dim item As ContentItem
        
        If TypeOf NamingContainer Is ContentItem Then
            item = CType(NamingContainer, ContentItem)
        Else
            item = CType(NamingContainer.NamingContainer, ContentItem)
        End If 
        
        Dim objEventInfo As EventInfo = CType(item.DataItem, EventInfo)
        ViewState("EventLocation") = objEventInfo.Location
    End Sub 'OnDataBinding
    
    
    
    '*********************************************************************
    '
    ' Render Method
    '
    ' Render only if there is a value
    '
    '*********************************************************************
    Protected Overrides Sub Render(writer As HtmlTextWriter)
        If CStr(ViewState("EventLocation")) <> String.Empty Then
            MyBase.Render(writer)
        End If
    End Sub 'Render
     
    
    '*********************************************************************
    '
    ' RenderContents Method
    '
    ' Render the contents
    '
    '*********************************************************************
    Protected Overrides Sub RenderContents(writer As HtmlTextWriter)
        writer.Write(String.Format(_formatString, HttpUtility.HtmlEncode(CStr(ViewState("EventLocation")))))
    End Sub 'RenderContents
End Class 'ItemEventLocation 
