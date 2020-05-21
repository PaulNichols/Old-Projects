
Imports System.ComponentModel



<Designer(GetType(ASPNET.StarterKit.Communities.CommunityDesigner))>  _
Public Class EventEditContent
    Inherits EditContent
    
    
    Public Sub New()
        If Not (Context Is Nothing) Then
            Dim _pageInfo As PageInfo = CType(Context.Items("PageInfo"), PageInfo)
            Dim contentPageID As Integer = _pageInfo.ID
            
            AddUrl = "Events_AddEvent.aspx"
            EditUrl = String.Format("Events_EditEvent.aspx?id={0}", contentPageID)
            DeleteUrl = String.Format("ContentPages_DeleteContentPage.aspx?id={0}", contentPageID)
            MoveUrl = String.Format("ContentPages_MoveContentPage.aspx?id={0}", contentPageID)
            CommentUrl = String.Format("Comments_AddComment.aspx?id={0}", contentPageID)
            ModerateUrl = "Moderation_ModerateSection.aspx"
        End If
    End Sub 'New
End Class 'EventEditContent