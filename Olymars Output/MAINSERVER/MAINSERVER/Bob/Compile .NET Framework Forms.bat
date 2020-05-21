@ECHO OFF
ECHO --------------------- DELETING PREVIOUS NATIVE IMAGE
"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" /DELETE "Bin\Bob.Forms.dll"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- COMPILING C# CODE: Bob.Forms.dll
"%OLYMARS_LATEST_FXBIN%\csc.exe" /t:library /debug+ /r:"Bin\Bob.dll" /out:"Bin\Bob.Forms.dll" /doc:"Bin\Bob.Forms.xml" Windows\Forms\CS\*.cs Version\CS\Version.cs
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- TURNING OFF SIGNATURE VERIFICATION
"%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\Bob.Forms.dll"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- RESIGNING ASSEMBLY
"%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\Bob.Forms.dll" "PublicPrivateKey\Bob.snk"
if errorlevel 1 goto ERROR
ECHO.

"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob.Forms.dll"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- DELETING PREVIOUS NATIVE IMAGE
"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" /DELETE "Bin\Bob Sample Application.exe"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- COMPILING C# CODE: Sample Application
"%OLYMARS_LATEST_FXBIN%\csc.exe" /t:winexe /debug+ /main:Bob.Exe.WinForm_Main /r:"Bin\Bob.dll" /r:"Bin\Bob.Forms.dll" /out:"Bin\Bob Sample Application.exe" Exe\CS\*.cs Version\CS\Version.cs
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- TURNING OFF SIGNATURE VERIFICATION
"%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\Bob Sample Application.exe"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- RESIGNING ASSEMBLY
"%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\Bob Sample Application.exe" PublicPrivateKey\Bob.snk
if errorlevel 1 goto ERROR
ECHO.

"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob Sample Application.exe"
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
