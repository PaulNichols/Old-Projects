
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports ASPNET.StarterKit.Communities.Events


'*********************************************************************
'
' EventSpeakerBiography Class
'
' Represents the speaker biography for the event
'
'*********************************************************************
<Designer(GetType(ASPNET.StarterKit.Communities.CommunityDesigner))>  _
Public Class EventSpeakerBiography
    Inherits WebControl
    
    
    Public Sub New()
        CssClass = "eventSpeakerBiography"
    End Sub 'New
    
    
    
    
    
    
    '*********************************************************************
    '
    ' RenderContents Method
    '
    ' Display content by retrieving content from context
    '
    '*********************************************************************
    Protected Overrides Sub RenderContents(writer As HtmlTextWriter)
        ' we need the section for the transformations
        Dim objSectionInfo As SectionInfo = CType(Context.Items("SectionInfo"), SectionInfo)
        
        ' Get EventInfo object
        Dim objEventInfo As EventInfo = CType(Context.Items("ContentInfo"), EventInfo)
        
        ' display the content
        writer.Write(CommunityGlobals.FormatText(objSectionInfo.AllowHtmlInput, objSectionInfo.ID, objEventInfo.SpeakerBiography))
    End Sub 'RenderContents
End Class 'EventSpeakerBiography 
