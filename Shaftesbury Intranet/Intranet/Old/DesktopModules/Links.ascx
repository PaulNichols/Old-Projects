<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.Links" CodeBehind="Links.ascx.vb" AutoEventWireup="false" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="~/DesktopModuleTitle.ascx"%>
<aspnetportal:title editurl="~/DesktopModules/EditLinks.aspx" edittext="Add Link" runat="server" id="Title1" />
<asp:datalist id="myDataList" cellpadding="4" width="100%" runat="server">
	<itemtemplate>
		<span class="Normal">
			<asp:HyperLink id="editLink" ImageUrl="<%# linkImage %>" NavigateUrl='<%# ChooseURL(DataBinder.Eval(Container.DataItem,"ItemID"), ModuleId, DataBinder.Eval(Container.DataItem,"Url")) %>' Target='<%# ChooseTarget() %>' ToolTip='<%# ChooseTip(DataBinder.Eval(Container.DataItem,"Description")) %>' runat="server" />
			<asp:HyperLink Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>' NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"Url") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem,"Description") %>' Target="_new" runat="server" />
		</span>
		<br>
	</itemtemplate>
</asp:datalist>
