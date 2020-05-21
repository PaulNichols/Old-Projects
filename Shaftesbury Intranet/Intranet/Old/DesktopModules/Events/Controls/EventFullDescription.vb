
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports ASPNET.StarterKit.Communities.Events




'*********************************************************************
'
' EventFullDescription Class
'
' Represents an event full description
'
'*********************************************************************
<Designer(GetType(ASPNET.StarterKit.Communities.CommunityDesigner))>  _
Public Class EventFullDescription
    Inherits WebControl
    
    Private _text As String
    
    
    
    '*********************************************************************
    '
    ' EventFullDescription Constructor
    '
    ' Assign a default css style (the user can override)
    '
    '*********************************************************************
    Public Sub New()
        CssClass = "eventFullDescription"
        
        ' Get ContentInfo object
        If Not (Context Is Nothing) Then
            Dim objEventInfo As EventInfo = CType(Context.Items("ContentInfo"), EventInfo)
            _text = objEventInfo.FullDescription
        End If
    End Sub 'New
    
    
    
    
    
    
    '*********************************************************************
    '
    ' RenderContents Method
    '
    ' Display the title
    ' Note: we are going to HTML Encode here to prevent script injections
    '
    '*********************************************************************
    Protected Overrides Sub RenderContents(writer As HtmlTextWriter)
        ' we need the section for the transformations
        Dim objSectionInfo As SectionInfo = CType(Context.Items("SectionInfo"), SectionInfo)
        
        ' display the content
        writer.Write(CommunityGlobals.FormatText(objSectionInfo.AllowHtmlInput, objSectionInfo.ID, _text))
    End Sub 'RenderContents
End Class 'EventFullDescription 
