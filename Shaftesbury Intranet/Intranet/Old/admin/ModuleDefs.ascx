<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<%@ Control Inherits="ASPNET.StarterKit.Portal.ModuleDefs" CodeBehind="ModuleDefs.ascx.vb" language="vb" AutoEventWireup="false" %>
<P>
	<ASPNETPortal:title runat="server" id="Title1" /></P>
<table cellpadding="2" cellspacing="0" border="0">
	<tr valign="top">
		<td>
			<asp:DataList id="defsList" DataKeyField="ModuleDefID" runat="server">
				<ItemTemplate>
					<asp:ImageButton ImageUrl="~/images/edit.gif" AlternateText="Edit this item" runat="server" />
					&nbsp;&nbsp;
					<asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "FriendlyName") %>' CssClass="Normal" runat="server" />
				</ItemTemplate>
			</asp:DataList>
		</td>
	</tr>
	<tr>
		<td>
			<asp:LinkButton cssclass="CommandButton" Text="Add New Module Type" runat="server" id="AddDefBtn" />
		</td>
	</tr>
</table>
