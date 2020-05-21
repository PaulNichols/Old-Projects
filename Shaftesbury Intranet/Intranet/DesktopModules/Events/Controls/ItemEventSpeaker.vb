
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports ASPNET.StarterKit.Communities.Events
Imports System.ComponentModel




'*********************************************************************
'
' ItemEventSpeaker Class
'
' Represents an event speaker displayed in a template
'
'*********************************************************************

Public Class ItemEventSpeaker
    Inherits WebControl
    
    Private _formatString As String = "speaker: {0}"
    
    
    
    '*********************************************************************
    '
    ' ItemEventSpeaker Constructor
    '
    ' Assign a default css class
    '
    '*********************************************************************
    Public Sub New()
        CssClass = "itemEventSpeaker"
        EnableViewState = False
    End Sub 'New
    
    
    '*********************************************************************
    '
    ' FormatString Property
    '
    ' String used for formatting speaker
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
        ViewState("Speaker") = objEventInfo.Speaker
    End Sub 'OnDataBinding
    
    
    
    '*********************************************************************
    '
    ' Render Method
    '
    ' Render only if there is a value
    '
    '*********************************************************************
    Protected Overrides Sub Render(writer As HtmlTextWriter)
        If CStr(ViewState("Speaker")) <> String.Empty Then
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
        writer.Write(String.Format(_formatString, HttpUtility.HtmlEncode(CStr(ViewState("Speaker")))))
    End Sub 'RenderContents
End Class 'ItemEventSpeaker 
