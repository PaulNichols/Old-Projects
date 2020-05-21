
Imports System
Imports System.ComponentModel
Imports ASPNET.StarterKit.Communities.Events




'*********************************************************************
'
' DisplayEventImage Class
'
' Displays an image of a book
'
'*********************************************************************
<Designer(GetType(ASPNET.StarterKit.Communities.CommunityDesigner))>  _
Public Class DisplayEventImage
    Inherits DisplayImage
    
    
    
    '*********************************************************************
    '
    ' DisplayEventImage Constructor
    '
    ' Get the book image information from the Context
    '
    '*********************************************************************
    Public Sub New()
        ' Grab event image from context
        If Not (Context Is Nothing) Then
            Dim objEventInfo As EventInfo = CType(Context.Items("ContentInfo"), EventInfo)
            If objEventInfo.ImageID <> - 1 Then
                ImageUrl = ImageUtility.BuildImagePath(objEventInfo.ImageID, objEventInfo.ImageName)
            End If
        End If
    End Sub 'New 
End Class 'DisplayEventImage