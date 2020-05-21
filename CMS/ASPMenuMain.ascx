<%@ Register TagPrefix="wam" Namespace="WebActive.ASPMenu" Assembly="ASPMenu" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ASPMenuMain.ascx.cs" Inherits="ASPMenuWeb1._1.ASPMenuMain" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Main1" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD><IMG src="images/bnr_r1_c1.jpg"></TD>
		<TD><IMG src="images/bnr_r1_c2.jpg"></TD>
		<TD><IMG src="images/bnr_r1_c3.jpg"></TD>
	</TR>
</TABLE>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD>
			<wam:ASPMenu id="ASPMenu1" XmlFile="aspmenu.xml" runat="server" DataSource="XML" MenuItemHeight="25"
				MenuItemWidth="125" Orientation="Horizontal" EffectDuration="35" ExpandEffect="Pixelate"
				CssFile="aspmenu.css" MenuGroupCss="MainGroup" MenuItemCss="MainItem" MenuItemOverCss="MainItemOver"></wam:ASPMenu>
		</TD>
	</TR>
</TABLE>
