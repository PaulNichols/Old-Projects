<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.HtmlModule" CodeBehind="HtmlModule.ascx.vb" AutoEventWireup="false" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>

<ASPNETPortal:title EditText="Edit" EditUrl="~/DesktopModules/EditHtml.aspx" runat="server" id=Title1 />

<table id="t1" cellspacing="0" cellpadding="0" runat="server">
    <tr valign="top">
        <td id="HtmlHolder" runat="server">
        </td>
    </tr>
</table>
