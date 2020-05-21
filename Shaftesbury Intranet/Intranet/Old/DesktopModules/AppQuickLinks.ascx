<%@ Control language="vb" Inherits="ASPNET.StarterKit.Portal.AppQuickLinks" CodeBehind="AppQuickLinks.ascx.vb" AutoEventWireup="false" %>
<hr noshade size="1pt" width="98%">
<span class="SubSubHead">Applications</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:hyperlink id="EditButton" cssclass="CommandButton" enableviewstate="false" runat="server" />
<asp:datalist id="myDataList" cellpadding="4" width="100%" enableviewstate="false" runat="server">
	<itemtemplate>
		<span class="Normal">
			<asp:hyperlink id="editLink" imageurl="<%# linkImage %>" navigateurl='<%# ChooseURL(DataBinder.Eval(Container.DataItem,"ItemID"), ModuleId, DataBinder.Eval(Container.DataItem,"Url")) %>' runat="server" />
			<asp:hyperlink  id="Hyperlink1" tooltip='<%# DataBinder.Eval(Container.DataItem,"Description") %>' Target="_blank" navigateurl='<%# DataBinder.Eval(Container.DataItem,"Url") %>'  runat="server">
				<%# DataBinder.Eval(Container.DataItem,"Title") %>
			</asp:hyperlink>
		</span>
	</itemtemplate>
</asp:datalist>
<br>
