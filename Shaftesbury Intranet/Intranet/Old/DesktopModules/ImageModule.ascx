<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.ImageModule" CodeBehind="ImageModule.ascx.vb" AutoEventWireup="false" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>

<ASPNETPortal:title EditText="Edit" EditUrl="~/DesktopModules/EditImage.aspx" runat="server" id=Title1 />

<asp:image id="Image1" border="0" runat="server" />
<br>
