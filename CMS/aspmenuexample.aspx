<%@ Register TagPrefix="uc1" TagName="ASPMenuMain" Src="ASPMenuMain.ascx" %>
<%@ Page language="c#" Codebehind="aspmenuexample.aspx.cs" AutoEventWireup="false" Inherits="ASPMenuWeb.Home" %>
<%@ Register TagPrefix="wam" Namespace="WebActive.ASPMenu" Assembly="ASPMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ASPMenu .NET Server Control</title>
		<meta name="description" content="An ASP.NET server control for constructing javascript DHTML menus, using Visual Studio.">
		<meta name="keywords" content="ASPMenu, asp menu, ASP.NET, server control, controls, control, component, control, javascript menu, javascript, DHTML menu, DHTML, ASP, programmer, development, .NET, menu, visual studio">
		<LINK href="aspMenuStyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="768" border="0" style="WIDTH: 768px; HEIGHT: 846px">
				<TR>
					<TD height="7">
						<P>
							<uc1:ASPMenuMain id="ASPMenuMain1" runat="server"></uc1:ASPMenuMain></P>
					</TD>
				</TR>
				<TR>
					<TD>
						<P>
							<TABLE id="Table4" style="BORDER-COLLAPSE: collapse" borderColor="#000000" cellSpacing="0"
								cellPadding="3" width="765" bgColor="#ffffff" border="0">
								<TR>
									<TD vAlign="top" width="514">
										<TABLE id="AutoNumber1" style="BORDER-COLLAPSE: collapse" borderColor="#808080" cellSpacing="0"
											cellPadding="6" width="103%" border="1">
											<TR>
												<TD width="100%" bgColor="#ebf5ff">
													<P class="title">aspMENU.net</P>
													<P class="bodytextnormal">By far the most advanced server control on the 
														market!&nbsp;<BR>
														<BR>
														Allowing Microsoft Visual Studio .NET&nbsp;users to create dynamic JavaScript 
														menus within minutes. Typically this is a task that would take a developer days 
														to program and maintain.</P>
													<P class="bodytextnormal">
														Using <STRONG><FONT color="#009aff">aspMENU.net</FONT></STRONG> server control 
														is a point and click solution, menu content is created from a XML document, 
														database or programmatically. With so many features the possibilities are 
														endless.<BR>
														<BR>
														Please see our <A href="http://aspmenu.net/Demos/default.aspx">online demos for 
															examples</A> of <STRONG><FONT color="#009aff">aspMENU.net</FONT></STRONG>.</P>
												</TD>
											</TR>
										</TABLE>
									</TD>
									<TD vAlign="top" width="208" bgColor="#ffffff">
										<P align="center">&nbsp;</P>
										<P align="center"><IMG height="100" src="images/Themes%20copy.jpg" width="127" border="0">
											<IMG height="40" src="images/ms_net_sm[1].jpg" width="78" border="0"></P>
										<P align="center">&nbsp;</P>
									</TD>
								</TR>
							</TABLE>
							<TABLE id="Table2" style="BORDER-COLLAPSE: collapse" borderColor="#111111" height="287"
								cellSpacing="0" cellPadding="10" width="767" border="0">
								<TBODY>
									<TR>
										<TD class="bodytextnormal" vAlign="top" width="169" height="283">
											<P><BR>
												<IMG height="250" src="images/1.jpg" width="169" border="0">
											</P>
											<P>
												<STRONG><FONT color="#009aff"><a href="http://www.411asp.net"></a>&nbsp;</P>
											</FONT></STRONG>
										</TD>
										<TD vAlign="top" width="304" height="283">
											<TABLE id="AutoNumber4" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
												cellPadding="15" width="107%" border="0">
												<TR>
													<TD width="100%">
														<P class="title">Customer Feedback</P>
														<P class="bodytextnormal"><I>"Using </I>aspMENU.net<I> saved us money and time! Our 
																development time for implementing dynamic menus is now over 250% times faster, 
																not to mention the added flexibility."</I><BR>
															<BR>
															<B>Tony Barltett</B> - Director <A href="http://www.iASP.com.au" target="_blank">www.iASP.com.au</A></P>
														<P class="title">Download</P>
														<P class="bodytextnormal">Download your free copy today of <STRONG><FONT color="#009aff">aspMENU.net</FONT></STRONG>
															for&nbsp;a limited time only.<BR>
															<BR>
															<B><A href="http://aspmenu.net/Download/Trial.aspx">Download Now</A></B></P>
													</TD>
												</TR>
											</TABLE>
										</TD>
										<TD vAlign="top" width="219" bgColor="#ffffff" height="283">
											<TABLE id="AutoNumber3" style="BORDER-COLLAPSE: collapse" borderColor="#808080" cellSpacing="0"
												cellPadding="6" width="100%" border="1">
												<TR>
													<TD width="100%" bgColor="#d1eeb4">
														<P class="title">Features</P>
														<UL>
															<LI>
																<P class="bodytextnormal"><STRONG>XML &amp; CSS </STRONG>
																</P>
															<LI>
																<P class="bodytextnormal"><STRONG>Scrolling </STRONG>
																</P>
															<LI>
																<P class="bodytextnormal"><STRONG>Permissions </STRONG>
																</P>
															<LI>
																<P class="bodytextnormal"><STRONG>Cross Browser Support </STRONG>
																</P>
															<LI>
																<P class="bodytextnormal"><STRONG>Orientations </STRONG>
																</P>
															<LI>
																<P class="bodytextnormal"><STRONG>Expand Effects </STRONG>
																</P>
															<LI>
																<P class="bodytextnormal"><STRONG>OpenOnClick </STRONG>
																</P>
															<LI>
																<P class="bodytextnormal"><STRONG>Multiple Menus </STRONG>
																</P>
															<LI>
																<P class="bodytextnormal"><STRONG>Style Themes </STRONG>
																</P>
															<LI>
																<P class="bodytextnormal"><STRONG>Client \ Server Events </STRONG>
																</P>
															<LI>
																<P class="bodytextnormal"><STRONG> Database</STRONG></P>
															<LI>
																<P class="bodytextnormal"><STRONG>Programmatic</STRONG></P>
															<LI>
																<P class="bodytextnormal"><STRONG>Smart Menus</STRONG>
																</P>
															</LI>
														</UL>
														<P class="bodytextnormal"><a href="http://aspmenu.net/features.aspx">Learn more..</a></P>
													</TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
								</TBODY></TABLE>
							<TABLE id="Table3" width="100%" border="0">
								<TR>
									<TD class="bodytextsmall" align="center" width="100%"><SPAN class="footertext">Web 
												Active Corporation Pty Ltd ACN 086 209 403 - Copyright 1998 - 2003 - ABN 83 086 
												209 403</SPAN></TD>
								</TR>
							</TABLE>
						</P>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
