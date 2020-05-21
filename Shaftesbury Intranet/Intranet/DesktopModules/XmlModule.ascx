<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.XmlModule" CodeBehind="XmlModule.ascx.vb" AutoEventWireup="false" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>

<ASPNETPortal:title EditText="Edit" EditUrl="~/DesktopModules/EditXml.aspx" runat="server" id=Title1 />

<span class="Normal">
    <asp:xml id="xml1" runat="server" />
</span>
