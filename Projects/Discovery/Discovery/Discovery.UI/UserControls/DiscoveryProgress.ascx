<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DiscoveryProgress.ascx.cs"
    Inherits="Discovery.UI.Web.UserControls.DiscoveryProgress" %>
<!-- AJAX Update Panel Start -->
<asp:UpdateProgress ID="updateProgressMessage" DisplayAfter="100" runat="server">
    <ProgressTemplate>
        <asp:Panel ID="PanelProgress" runat="server" CssClass="ProgressPanel">
            <table width="100%" border="0">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="imgProgress" runat="server" SkinID="Progress" />
                    </td>
                    <td valign="middle" align="center">
                        <asp:Label ID="lblMessage" runat="server" CssClass="ProgressText"><%= Message %></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ProgressTemplate>
</asp:UpdateProgress>
<!-- AJAX Update Panel End -->
