Imports System.Configuration

Namespace Configuration
    Public Class PlugInConfigSectionHandler
        Inherits ConfigSectionHandlerBase
        Implements System.Configuration.IConfigurationSectionHandler

        Public Function Create(ByVal parent As Object, ByVal configContext As Object, ByVal section As System.Xml.XmlNode) As Object Implements System.Configuration.IConfigurationSectionHandler.Create
            Dim plug As PlugInList

            ' cope with the parent
            If parent Is Nothing Then
                plug = New PlugInList
            Else
                plug = CType(CType(parent, PlugInList).Clone(), PlugInList)
            End If

            ' read the section and set the correct properties in the plugInList object

            Dim signOnProvider As Xml.XmlNode = section.SelectSingleNode("security/signOnProvider")
            If Not signOnProvider Is Nothing Then
                plug.SignOn = ExtractValue(signOnProvider, "type")
            End If

            Return plug

        End Function

        Public Shared Function GetConfig() As uk.gov.defra.Phoenix.NonSSO.Configuration.PlugInList
            Return CType(ConfigurationSettings.GetConfig("phoenix.plugin"), uk.gov.defra.Phoenix.NonSSO.Configuration.PlugInList)
        End Function
    End Class
End Namespace
