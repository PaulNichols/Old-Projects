<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.Events" CodeBehind="Events.ascx.vb" AutoEventWireup="false" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<ASPNETPortal:title EditText="Add New Event" EditUrl="~/DesktopModules/EditEvents.aspx" runat="server"
	id="Title1" />
<asp:DataList id="myDataList" CellPadding="4" Width="98%" EnableViewState="false" runat="server">
	<ItemTemplate>
		<SPAN class="ItemTitle">
			<asp:HyperLink id=editLink runat="server" Visible="<%# IsEditable %>" NavigateUrl='<%# "~/DesktopModules/EditEvents.aspx?ItemID=" &amp; DataBinder.Eval(Container.DataItem,"ItemID") &amp; "&amp;mid=" &amp; ModuleId %>' ImageUrl="~/images/edit.gif">
			</asp:HyperLink>
			<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'>
			</asp:Label>
		</SPAN><BR>
		<SPAN class="Normal">
			<I>
				<%# DataBinder.Eval(Container.DataItem,"WhereWhen") %>
			</I>
		</SPAN><BR>
		<SPAN class="Normal">
			<%# DataBinder.Eval(Container.DataItem,"Description") %>
		</SPAN>&nbsp;
		<asp:HyperLink id=moreLink runat="server" Visible='<%# DataBinder.Eval(Container.DataItem,"URL") <> String.Empty %>' NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"URL") %>' CssClass="normal">
                read more...</asp:HyperLink><BR>
	</ItemTemplate>
</asp:DataList>
