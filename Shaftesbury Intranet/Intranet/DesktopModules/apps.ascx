<%@ Control Language="vb" AutoEventWireup="false" Codebehind="apps.ascx.vb" Inherits="apps" %>
<HTML>
	<HEAD>
		<TITLE></TITLE>
		<Script Language="VBScript"> 

Sub Dynamics() 

	Set WS = CreateObject("WScript.Shell") 
	Ws.Run("C:\lotus\123\123w.exe")
	
	Set WS = nothing 
End Sub 
Sub Word() 

	Set WS = CreateObject("WScript.Shell") 
'	Ws.Run("C:\Program Files\Microsoft Office 2000\Office\WINWORD.EXE")
Ws.Run("WINWORD.EXE")
	
	Set WS = nothing 
End Sub 
Sub Excel()

	Set WS = CreateObject("WScript.Shell") 
	'Ws.Run("%windir%\notepad.exe")
	Ws.Run("EXCEL.EXE")
	
	Set WS = nothing 
End Sub 

		</Script>
	</HEAD>
	<body>
	<span class="Normal">
		<p>
			<asp:label id="ModuleTitle" Font-Bold=True cssclass="Normal" EnableViewState="false" runat="server">Local Applications</asp:label> 
		<P>
		
			<a href="Default.aspx" onclick="Dynamics">Lotus 123</a><br>
			<a href="Default.aspx" onclick="Word">MS Word</a><br>
			<a href="Default.aspx" onclick="Excel">Excel</a>
		<p></p>
				</span>

	</body>
</HTML>
