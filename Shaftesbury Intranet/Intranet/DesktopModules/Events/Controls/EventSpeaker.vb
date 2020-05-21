
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports ASPNET.StarterKit.Communities.Events


'*********************************************************************
'
' EventSpeaker Class
'
' Represents the speaker for the event
'
'*********************************************************************
<Designer(GetType(ASPNET.StarterKit.Communities.CommunityDesigner))>  _
Public Class EventSpeaker
    Inherits WebControl
    
    Private _formatString As String = "speaker: {0}"
    
    
    Public Sub New()
        CssClass = "eventSpeaker"
    End Sub 'New
    
    
    
    '*********************************************************************
    '
    ' FormatString Property
    '
    ' Used to format the output of this control
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
    ' RenderContents Method
    '
    ' Display content by retrieving content from context
    '
    '*********************************************************************
    Protected Overrides Sub RenderContents(writer As HtmlTextWriter)
        ' Get EventInfo object
        Dim objEventInfo As EventInfo = CType(Context.Items("ContentInfo"), EventInfo)
        
        ' Write location
        If objEventInfo.Speaker <> String.Empty Then
            writer.Write(String.Format(_formatString, HttpUtility.HtmlEncode(objEventInfo.Speaker)))
        End If
    End Sub 'RenderContents 
End Class 'EventSpeaker 