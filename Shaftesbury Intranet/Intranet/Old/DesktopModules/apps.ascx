<%@ Control Language="vb" AutoEventWireup="false" Codebehind="apps.ascx.vb" Inherits="apps" %>
<HTML>
	<HEAD>
		<TITLE></TITLE>
		<Script Language="VBScript"> 

Sub MSWord() 

	Set WS = CreateObject("WScript.Shell") 
	'Ws.Run("%windir%\notepad.exe")
	Ws.Run("C:\Program Files\Microsoft Office\Office\winword.exe")
	'Ws.Run("C:\SELECT_Enterprise\SELECT_Enterprise\Exe\ANIMATOR.EXE")
	Set WS = nothing 
End Sub 


		</Script>
	</HEAD>
	<body>
		<p>
			<asp:label id="ModuleTitle" cssclass="Head" EnableViewState="false" runat="server">Local Applications</asp:label> 
		<P>
			<a href="Default.aspx" onclick="MSWord">Microsoft Word</a>
		<p></p>
	</body>
</HTML>
