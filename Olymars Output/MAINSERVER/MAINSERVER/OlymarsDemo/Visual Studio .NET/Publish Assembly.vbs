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

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.dll"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.dll", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the assembly OlymarsDemo.dll: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.pdb"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.pdb", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file OlymarsDemo.pdb: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.xml"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.xml", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file OlymarsDemo.xml: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.Forms.dll"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.Forms.dll", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the assembly OlymarsDemo.Forms.dll: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.Forms.pdb"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.Forms.pdb", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file OlymarsDemo.Forms.pdb: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.Forms.xml"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.Forms.xml", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file OlymarsDemo.Forms.xml: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.BusinessComponents.dll"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.BusinessComponents.dll", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the assembly OlymarsDemo.BusinessComponents.dll: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.BusinessComponents.pdb"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.BusinessComponents.pdb", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file OlymarsDemo.BusinessComponents.pdb: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.BusinessComponents.xml"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.BusinessComponents.xml", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file OlymarsDemo.BusinessComponents.xml: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.TreeNodeFactory.dll"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.TreeNodeFactory.dll", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the assembly OlymarsDemo.TreeNodeFactory.dll: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.TreeNodeFactory.pdb"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.TreeNodeFactory.pdb", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file OlymarsDemo.TreeNodeFactory.pdb: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.TreeNodeFactory.xml"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.TreeNodeFactory.xml", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file OlymarsDemo.TreeNodeFactory.xml: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.ObjectSpace.dll"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.ObjectSpace.dll", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the assembly OlymarsDemo.ObjectSpace.dll: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.ObjectSpace.pdb"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.ObjectSpace.pdb", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file OlymarsDemo.ObjectSpace.pdb: " & Err.Description
			Err.Clear
		End If
	End If

	DllPath = Left(WScript.ScriptFullName, Len(WScript.ScriptFullName) - Len(WScript.ScriptName) ) + "..\Bin\OlymarsDemo.ObjectSpace.xml"
	If fso.FileExists(DllPath) Then
		fso.CopyFile DllPath, PublicAssembliesPath + "\OlymarsDemo.ObjectSpace.xml", True
		If Err.Number <> 0 Then
			WScript.Echo "Unable to copy the file OlymarsDemo.ObjectSpace.xml: " & Err.Description
			Err.Clear
		End If
	End If

	Set fso = Nothing
	Err.Clear
	On Error Goto 0

End Sub
