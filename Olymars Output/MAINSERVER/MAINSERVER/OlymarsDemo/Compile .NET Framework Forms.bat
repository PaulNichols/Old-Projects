@ECHO OFF
ECHO --------------------- DELETING PREVIOUS NATIVE IMAGE
"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" /DELETE "Bin\OlymarsDemo.Forms.dll"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Forms.dll
"%OLYMARS_LATEST_FXBIN%\csc.exe" /t:library /debug+ /r:"Bin\OlymarsDemo.dll" /out:"Bin\OlymarsDemo.Forms.dll" /doc:"Bin\OlymarsDemo.Forms.xml" Windows\Forms\CS\*.cs Web\Forms\CS\*.cs Version\CS\Version.cs
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- TURNING OFF SIGNATURE VERIFICATION
"%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Forms.dll"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- RESIGNING ASSEMBLY
"%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Forms.dll" "PublicPrivateKey\OlymarsDemo.snk"
if errorlevel 1 goto ERROR
ECHO.

"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Forms.dll"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- DELETING PREVIOUS NATIVE IMAGE
"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" /DELETE "Bin\OlymarsDemo Sample Application.exe"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- COMPILING C# CODE: Sample Application
"%OLYMARS_LATEST_FXBIN%\csc.exe" /t:winexe /debug+ /main:OlymarsDemo.Exe.WinForm_Main /r:"Bin\OlymarsDemo.dll" /r:"Bin\OlymarsDemo.Forms.dll" /out:"Bin\OlymarsDemo Sample Application.exe" Exe\CS\*.cs Version\CS\Version.cs
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- TURNING OFF SIGNATURE VERIFICATION
"%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo Sample Application.exe"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- RESIGNING ASSEMBLY
"%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo Sample Application.exe" PublicPrivateKey\OlymarsDemo.snk
if errorlevel 1 goto ERROR
ECHO.

"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo Sample Application.exe"
if errorlevel 1 goto ERROR
ECHO.

goto END

:ERROR
ECHO.
ECHO.
ECHO.
ECHO --------------------- WARNING !!!!! AN ERROR HAS OCCURED !
ECHO.

:END

if "%1" == "" pause
