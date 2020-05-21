@ECHO OFF

ECHO --------------------- CREATING STRONG NAME
IF NOT EXIST "PublicPrivateKey\Bob.snk" "%OLYMARS_LATEST_FXSDK%\sn.exe" -k "PublicPrivateKey\Bob.snk"
if errorlevel 1 goto ERROR
IF NOT EXIST "PublicPrivateKey\PublicKey.snk" "%OLYMARS_LATEST_FXSDK%\sn.exe" -p "PublicPrivateKey\Bob.snk" "PublicPrivateKey\PublicKey.snk"
if errorlevel 1 goto ERROR
COPY "PublicPrivateKey\PublicKey.snk" "Bin"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- DELETING PREVIOUS NATIVE IMAGE
"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" /delete "Bin\Bob.BusinessComponents.dll"
if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: Bob.BusinessComponents.dll
"%OLYMARS_LATEST_FXBIN%\csc.exe" /define:OLYMARS_DEBUG /define:OLYMARS_ATTRIBUTE /doc:Bin\Bob.BusinessComponents.xml /debug+ /t:library /out:Bin\Bob.BusinessComponents.dll /r:Bin\Bob.dll /recurse:BusinessComponents\CS\*.cs Version\CS\Version.cs
if errorlevel 1 goto ERROR

ECHO --------------------- TURNING OFF SIGNATURE VERIFICATION
"%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr Bin\Bob.BusinessComponents.dll
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- RESIGNING ASSEMBLY
"%OLYMARS_LATEST_FXSDK%\sn.exe" -R Bin\Bob.BusinessComponents.dll PublicPrivateKey\Bob.snk
if errorlevel 1 goto ERROR
ECHO.

"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob.BusinessComponents.dll"
if errorlevel 1 goto ERROR
ECHO.
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
