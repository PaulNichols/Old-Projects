<%@ Page Language="vb" AutoEventWireup="false" Codebehind="WebForm1.aspx.vb" Inherits="PITSService.WebForm1"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:dropdownlist id="cboFilters" style="Z-INDEX: 101; LEFT: 32px; POSITION: absolute; TOP: 48px"
				runat="server" Width="214px" AutoPostBack="True"></asp:dropdownlist>
			<asp:DataGrid id="DataGrid1" style="Z-INDEX: 104; LEFT: 32px; POSITION: absolute; TOP: 112px"
				runat="server" Width="960px" Height="280px" AllowPaging="True" AutoGenerateColumns="False">
				<Columns>
					<asp:HyperLinkColumn Target="_blank" DataNavigateUrlField="ProjectId" DataTextField="Project" HeaderText="Project"></asp:HyperLinkColumn>
					<asp:BoundColumn DataField="Issue" ReadOnly="True" HeaderText="Issue"></asp:BoundColumn>
					<asp:BoundColumn DataField="Slide Type" ReadOnly="True" HeaderText="Slide Type"></asp:BoundColumn>
					<asp:BoundColumn DataField="Department" ReadOnly="True" HeaderText="Department"></asp:BoundColumn>
					<asp:BoundColumn DataField="Current Owner" ReadOnly="True" HeaderText="Current Owner"></asp:BoundColumn>
					<asp:BoundColumn DataField="Customer" ReadOnly="True" HeaderText="Customer"></asp:BoundColumn>
				</Columns>
			</asp:DataGrid>
			<asp:DropDownList id="cboLocation" style="Z-INDEX: 103; LEFT: 480px; POSITION: absolute; TOP: 48px"
				runat="server" Width="216px"></asp:DropDownList>
			<asp:DropDownList id="cboFurtherFilter" style="Z-INDEX: 102; LEFT: 256px; POSITION: absolute; TOP: 48px"
				runat="server" Width="216px" AutoPostBack="True"></asp:DropDownList></form>
	</body>
</HTML>
