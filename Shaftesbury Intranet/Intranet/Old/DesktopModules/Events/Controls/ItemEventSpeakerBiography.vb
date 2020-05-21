
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports ASPNET.StarterKit.Communities.Events
Imports System.ComponentModel




'*********************************************************************
'
' ItemEventSpeakerBiography Class
'
' Represents an event location displayed in a template
'
'*********************************************************************

Public Class ItemEventSpeakerBiography
    Inherits WebControl
    
    
    
    '*********************************************************************
    '
    ' ItemEventSpeakerBiography Constructor
    '
    ' Assign a default css class
    '
    '*********************************************************************
    Public Sub New()
        CssClass = "itemEventSpeakerBiography"
        EnableViewState = False
    End Sub 'New
    
    
    
    
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
        ViewState("Biography") = objEventInfo.SpeakerBiography
    End Sub 'OnDataBinding
    
    
    
    '*********************************************************************
    '
    ' RenderContents Method
    '
    ' Render the contents
    '
    '*********************************************************************
    Protected Overrides Sub RenderContents(writer As HtmlTextWriter)
        ' we need the section for the transformations
        Dim objSectionInfo As SectionInfo = CType(Context.Items("SectionInfo"), SectionInfo)
        
        ' display the content
        writer.Write(CommunityGlobals.FormatText(objSectionInfo.AllowHtmlInput, objSectionInfo.ID, CStr(ViewState("Biography"))))
    End Sub 'RenderContents
End Class 'ItemEventSpeakerBiography 
