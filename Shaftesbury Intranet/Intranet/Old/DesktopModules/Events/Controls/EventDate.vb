
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports ASPNET.StarterKit.Communities.Events


'*********************************************************************
'
' EventDate Class
'
' Represents the date of the event
'
'*********************************************************************
<Designer(GetType(ASPNET.StarterKit.Communities.CommunityDesigner))>  _
Public Class EventDate
    Inherits DisplayDate
    
    
    Public Sub New()
        CssClass = "eventDate"
    End Sub 'New
    
    
    
    
    Public Overrides ReadOnly Property [Date]() As DateTime
        Get
            Dim objEventInfo As EventInfo = CType(Context.Items("ContentInfo"), EventInfo)
            Return objEventInfo.Date
        End Get
    End Property
End Class 'EventDate 

