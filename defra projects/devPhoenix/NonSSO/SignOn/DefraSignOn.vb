Imports System.Web

Public Class DefraSignOn
    Inherits SignOnBase
    Implements SignOn.ISignOn

    Public Sub CheckAuthentication() Implements SignOn.ISignOn.CheckAuthentication
        uk.gov.defra.CommonCode.SignOn.CheckAuthentication()

        'Dim xdoc As New Xml.XmlDocument
        'xdoc.LoadXml(HttpContext.Current.User.Identity.Name)
        'Dim node As Xml.XmlNode = xdoc.SelectSingleNode("//SPNumber")

        'SetPermissions(node.InnerText.Trim())

    End Sub

    Public Function CurrentUserName() As String Implements SignOn.ISignOn.CurrentUserName
        Return uk.gov.defra.CommonCode.Serialize.ActiveUser_Name
        Dim xdoc As New Xml.XmlDocument
        xdoc.LoadXml(HttpContext.Current.User.Identity.Name)
        Dim node As Xml.XmlNode = xdoc.SelectSingleNode("//SPNumber")

        Return node.InnerText.Trim()

    End Function

    'Public Function UserRegistryMode() As Utils.Configuration.RegistryMode Implements Utils.SignOn.ISignOn.UserRegistryMode
    '    Dim xdoc As New Xml.XmlDocument

    '    xdoc.LoadXml(HttpContext.Current.User.Identity.Name)
    '    Dim node As Xml.XmlNode = xdoc.SelectSingleNode("//SPNumber")

    '    If Not node Is Nothing Then
    '        HttpContext.Current.Session("UserName") = node.InnerText.Trim
    '    End If

    '    Return Utils.Configuration.RegistryMode.Admin
    'End Function


End Class
