<%@ Control Inherits="ASPNET.StarterKit.Portal.Documents" CodeBehind="Documents.ascx.vb" language="vb" AutoEventWireup="false" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<ASPNETPortal:title runat="server" id="Title1" />
<table cellpadding="2" cellspacing="0" border="0">
	<tr valign="top">
		<td class="Normal" width="100">
			&nbsp;
		</td>
		<td>
			<asp:DataList id="documentList" DataKeyField="ItemID" runat="server">
				<ItemTemplate>
					<asp:ImageButton ImageUrl="~/images/edit.gif" CommandName="edit" AlternateText="Edit this item" runat="server" />
					&nbsp;&nbsp;
					<asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "FileFriendlyName") %>' cssclass="Normal" runat="server" />
				</ItemTemplate>
			</asp:DataList>
		</td>
	</tr>
	<tr>
		<td>
			&nbsp;
		</td>
		<td>
			<asp:LinkButton cssclass="CommandButton" Text="Add New Document" runat="server" id="AddDocumentBtn">
                Add New Document</asp:LinkButton>
		</td>
	</tr>
</table>
