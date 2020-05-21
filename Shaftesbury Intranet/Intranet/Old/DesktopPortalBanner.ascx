<%@ Control CodeBehind="DesktopPortalBanner.ascx.vb" language="vb" AutoEventWireup="false" Inherits="ASPNET.StarterKit.Portal.DesktopPortalBanner" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>
<%--

   The DesktopPortalBanner User Control is responsible for displaying the standard Portal
   banner at the top of each .aspx page.

   The DesktopPortalBanner uses the Portal Configuration System to obtain a list of the
   portal's sitename and tab settings. It then render's this content into the page.

--%>
<p>
	<table width="100%" cellspacing="0" class="HeadBg" border="0">
		<tr valign="top">
			<td colspan="3" class="SiteLink" align="right" height="17">
				<P></P>
				<P></P>
				<P><BR>
					<asp:label id="WelcomeMessage" forecolor="#eeeeee" runat="server" />
					<%= LogoffLink %>
					&nbsp;&nbsp;&nbsp;
				</P>
			</td>
		</tr>
		<tr>
			<td width="10" rowspan="2">
				&nbsp;
			</td>
			<td height="40">
				<asp:label id="siteName" CssClass="SiteTitle" EnableViewState="false" runat="server" />
			</td>
			<td align="center" rowspan="2">
				<!--ASP.NET Logo was here//-->
			</td>
		</tr>
		<tr>
			<td>
				<asp:datalist id="tabs" cssclass="OtherTabsBg" repeatdirection="horizontal" ItemStyle-Height="25"
					SelectedItemStyle-CssClass="TabBg" ItemStyle-BorderWidth="1" EnableViewState="false" runat="server">
					<ItemTemplate>
						&nbsp;<a href='<%= Global.GetApplicationPath(Request) %>/DesktopDefault.aspx?tabindex=<%# Container.ItemIndex %>&tabid=<%# Ctype(Container.DataItem, TabStripDetails).TabId %>' class="OtherTabs"><%# Ctype(Container.DataItem, TabStripDetails).TabName %></a>&nbsp;
					</ItemTemplate>
					<SelectedItemTemplate>
						&nbsp;<span class="SelectedTab"><%# Ctype(Container.DataItem, TabStripDetails).TabName %></span>&nbsp;
					</SelectedItemTemplate>
				</asp:datalist>
			</td>
		</tr>
	</table>
</p>
