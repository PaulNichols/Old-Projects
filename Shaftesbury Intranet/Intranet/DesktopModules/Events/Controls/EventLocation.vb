
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports ASPNET.StarterKit.Communities.Events


'*********************************************************************
'
' EventLocation Class
'
' Represents the location of the event
'
'*********************************************************************
<Designer(GetType(ASPNET.StarterKit.Communities.CommunityDesigner))>  _
Public Class EventLocation
    Inherits WebControl
    
    Private _formatString As String = "location: {0}"
    
    
    Public Sub New()
        CssClass = "eventLocation"
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
        ' Get PageInfo object
        Dim objEventInfo As EventInfo = CType(Context.Items("ContentInfo"), EventInfo)
        
        ' Write location
        If objEventInfo.Location <> String.Empty Then
            writer.Write(String.Format(_formatString, HttpUtility.HtmlEncode(objEventInfo.Location)))
        End If
    End Sub 'RenderContents 
End Class 'EventLocation 