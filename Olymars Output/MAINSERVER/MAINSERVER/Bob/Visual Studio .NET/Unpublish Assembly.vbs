Option Explicit

Dim Response
Dim vbYesNoCancel
Dim vbQuestion
Dim vbCancel
Dim vbCrLf

vbYesNoCancel = 3
vbQuestion = 32
vbCancel = 2
vbCrLf = Chr(13) + Chr(10)

Response = MsgBox("Do you want to target the latest version of the .NET Framework and Visual Studio .NET?" & vbCrLf & vbCrLf & "Yes = .NET Framework 1.1 & VS .NET 2003" & vbCrLf & "No = .NET Framework 1.0 & VS .NET 2002" & vbCrLf & "Cancel = No change will be made", vbYesNoCancel + vbQuestion, "Which .NET version you want to use ?")

If Response = vbCancel Then WScript.Quit

DoTheWork Response
WScript.Echo "Your assemblies were successfully unreferenced from Visual Studio.NET!"

Sub DoTheWork (ByVal Response)

	Dim PublicAssembliesPath
	Dim WSHShell
	Dim fso
	Dim vbYes
	Dim vbNo

	vbYes = 6
	vbNo = 7

	If Response = vbYes Then

		PublicAssembliesPath = "HKLM\SOFTWARE\Microsoft\VisualStudio\7.1\AssemblyFolders\PublicAssemblies\"

	ElseIf Response = vbNo Then

		PublicAssembliesPath = "HKLM\SOFTWARE\Microsoft\VisualStudio\7.0\AssemblyFolders\PublicAssemblies\"

	End If

	Set WSHShell = WScript.CreateObject("WScript.Shell")
	On Error Resume Next
	PublicAssembliesPath = WSHShell.RegRead(PublicAssembliesPath)
	Set WSHShell = Nothing

	If Err.Number <> 0 Then

		WScript.Echo "Visual Studio .NET is not installed on this computer."
		WScript.Quit

	End If

	Err.Clear
	On Error Resume Next
	Set fso = CreateObject("Scripting.FileSystemObject")
	fso.DeleteFile PublicAssembliesPath + "\Bob.dll", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.pdb", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.xml", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.Forms.dll", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.Forms.pdb", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.Forms.xml", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.BusinessComponents.dll", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.BusinessComponents.pdb", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.BusinessComponents.xml", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.TreeNodeFactory.dll", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.TreeNodeFactory.pdb", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.TreeNodeFactory.xml", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.ObjectSpace.dll", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.ObjectSpace.pdb", True
	fso.DeleteFile PublicAssembliesPath + "\Bob.ObjectSpace.xml", True
	Set fso = Nothing
	Err.Clear
	On Error Goto 0

End Sub
