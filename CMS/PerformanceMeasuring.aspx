<%@ Page language="c#" Codebehind="PerformanceMeasuring.aspx.cs" AutoEventWireup="false" Inherits="CMS.PerformanceMeasuring" %>
<%@ Register TagPrefix="uc1" TagName="ASPMenuMain" Src="ASPMenuMain.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="CMS.css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="1"
				cellPadding="1" width="100%" border="0">
				<TR>
					<TD align="center">
						<asp:HyperLink id="lnkPage4" runat="server" NavigateUrl="page4.htm">HP Rollout Programme Status Scorecard</asp:HyperLink></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:HyperLink id="lnkPage7" runat="server" NavigateUrl="page7.htm">Future Firecrest Balanced Scorecard Report</asp:HyperLink></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:LinkButton id="lnkTop" runat="server">Top</asp:LinkButton>&nbsp;&nbsp;
						<asp:LinkButton id="lnkUp" runat="server">Up</asp:LinkButton></TD>
				</TR>
				<TR>
					<TD align="left">
						<asp:DataGrid id=grdElements runat="server" AutoGenerateColumns="False" CellPadding="5" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#CCCCCC" DataKeyField="ElementId" DataSource="<%# gridView %>" Width="90%" Height="136px" OnItemDataBound="grdElements_OnItemDataBound" OnItemCommand="grdElements_OnItemCommand" >
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<HeaderStyle CssClass="GridHeader"></HeaderStyle>
							<FooterStyle ForeColor="#000066" BackColor="#CC9900"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Ref" SortExpression="Ref">
									<HeaderStyle Width="1%"></HeaderStyle>
									<ItemStyle CssClass="GridItemBold"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Service Elements">
									<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="29%"></HeaderStyle>
									<ItemStyle CssClass="GridItemBold"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Last Month">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" CssClass="GridItem"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblLastMonth runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastMonth", "{0:P}") %>'>
										</asp:Label>
										<asp:Label id="lblLastMonthTriangle" runat="server" ForeColor="Red">*</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="This Month">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" CssClass="GridItem"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblThisMonthText runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ThisMonth", "{0:P}") %>' Visible="False">
										</asp:Label>
										<asp:LinkButton id=lnkThisMonthText runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ElementId") %>' ForeColor="Black" Text='<%# DataBinder.Eval(Container, "DataItem.ThisMonth", "{0:P}") %>'>
										</asp:LinkButton>
										<asp:Label id="lblThisMonthTriangle" runat="server" ForeColor="Red">*</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Next Month (predicted)">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" CssClass="GridItem"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblNextMonthText runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NextMonth", "{0:P}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Commentary" SortExpression="Commentary" HeaderText="Commentary">
									<HeaderStyle HorizontalAlign="Center" Width="40%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" CssClass="GridItem"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn Visible="False">
									<ItemTemplate>
										<asp:LinkButton id="lnkDetail" runat="server" Visible="False" CommandName="ServiceDetail">Detail...</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="#CC9900" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
