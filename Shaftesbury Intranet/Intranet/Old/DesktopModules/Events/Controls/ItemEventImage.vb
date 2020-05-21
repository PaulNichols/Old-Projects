
Imports System
Imports ASPNET.StarterKit.Communities.Events



'*********************************************************************
'
' ItemEventImage Class
'
' Represents an event image (you know, a speark)
'
'*********************************************************************

Public Class ItemEventImage
    Inherits ItemImage
    
    
    
    '*********************************************************************
    '
    ' OnDataBinding Method
    '
    ' When databound, get the image information
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
        If objEventInfo.ImageID <> - 1 Then
            ImageUrl = ImageUtility.BuildImagePath(objEventInfo.ImageID, objEventInfo.ImageName)
            NavigateUrl = CommunityGlobals.CalculatePath(String.Format("{0}.aspx", objEventInfo.ContentPageID))
        End If
    End Sub 'OnDataBinding
End Class 'ItemEventImage 


