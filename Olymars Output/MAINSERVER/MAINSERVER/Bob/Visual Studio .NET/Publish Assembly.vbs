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
WScript.Echo "References to your various assemblies are now available from Visual Studio.NET!"

Sub DoTheWork (ByVal Response)

	Dim DllPath
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

	On Error Resume Next
	Set fso = CreateObject("Scripting.FileSystemObject")

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.dll"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.dll", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the assembly Bob.dll: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.pdb"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.pdb", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file Bob.pdb: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.xml"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.xml", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file Bob.xml: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.Forms.dll"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.Forms.dll", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the assembly Bob.Forms.dll: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.Forms.pdb"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.Forms.pdb", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file Bob.Forms.pdb: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.Forms.xml"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.Forms.xml", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file Bob.Forms.xml: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.BusinessComponents.dll"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.BusinessComponents.dll", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the assembly Bob.BusinessComponents.dll: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.BusinessComponents.pdb"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.BusinessComponents.pdb", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file Bob.BusinessComponents.pdb: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.BusinessComponents.xml"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.BusinessComponents.xml", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file Bob.BusinessComponents.xml: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.TreeNodeFactory.dll"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.TreeNodeFactory.dll", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the assembly Bob.TreeNodeFactory.dll: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.TreeNodeFactory.pdb"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.TreeNodeFactory.pdb", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file Bob.TreeNodeFactory.pdb: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.TreeNodeFactory.xml"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.TreeNodeFactory.xml", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file Bob.TreeNodeFactory.xml: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.ObjectSpace.dll"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.ObjectSpace.dll", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the assembly Bob.ObjectSpace.dll: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.ObjectSpace.pdb"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.ObjectSpace.pdb", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file Bob.ObjectSpace.pdb: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\Bob.ObjectSpace.xml"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\Bob.ObjectSpace.xml", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file Bob.ObjectSpace.xml: " & Err.Description
			Err.Clear
		End If
	End If

	Set fso = Nothing
	Err.Clear
	On Error Goto 0

End Sub
