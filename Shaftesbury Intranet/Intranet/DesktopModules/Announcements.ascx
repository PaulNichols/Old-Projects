<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.Announcements" CodeBehind="Announcements.ascx.vb" AutoEventWireup="false" %>
<ASPNETPortal:title EditText="Add New Announcement" EditUrl="~/DesktopModules/EditAnnouncements.aspx"
	runat="server" id="Title1" />
<asp:DataList id="myDataList" CellPadding="4" Width="98%" EnableViewState="false" runat="server">
	<ItemTemplate>
		<asp:HyperLink id=editLink runat="server" Visible="<%# IsEditable %>" NavigateUrl='<%# "~/DesktopModules/EditAnnouncements.aspx?ItemID=" &amp; DataBinder.Eval(Container.DataItem,"ItemID") &amp; "&amp;mid=" &amp; ModuleId %>' ImageUrl="~/images/edit.gif">
		</asp:HyperLink><SPAN class="ItemTitle"><%# DataBinder.Eval(Container.DataItem,"Title") %></SPAN><BR>
		<SPAN class="Normal"><%# DataBinder.Eval(Container.DataItem,"Description") %>&nbsp; 
<asp:HyperLink id=moreLink runat="server" Visible='<%# DataBinder.Eval(Container.DataItem,"MoreLink") <> String.Empty %>' NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MoreLink") %>'>
                read more...</asp:HyperLink></SPAN><BR>
	</ItemTemplate>
</asp:DataList>
