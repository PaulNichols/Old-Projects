@ECHO OFF
ECHO --------------------- DELETING FILE
IF EXIST Bob.DataClasses.dll del /Q Bob.DataClasses.*
ECHO.
ECHO.

ECHO --------------------- CREATING BIN AND PublicPrivateKey DIRECTORIES
IF NOT EXIST "..\..\Bin" MD "..\..\Bin"
if errorlevel 1 goto ERROR
IF NOT EXIST "..\..\PublicPrivateKey" MD "..\..\PublicPrivateKey"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- CREATING STRONG NAME
IF NOT EXIST "..\..\PublicPrivateKey\Bob.snk" "%OLYMARS_LATEST_FXSDK%\sn.exe" -k "..\..\PublicPrivateKey\Bob.snk"
if errorlevel 1 goto ERROR
IF NOT EXIST "..\..\PublicPrivateKey\PublicKey.snk" "%OLYMARS_LATEST_FXSDK%\sn.exe" -p "..\..\PublicPrivateKey\Bob.snk" "..\..\PublicPrivateKey\PublicKey.snk"
if errorlevel 1 goto ERROR
COPY "..\..\PublicPrivateKey\PublicKey.snk" "..\..\Bin"
COPY "..\..\PublicPrivateKey\PublicKey.snk" "."
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- COMPILING C# CODE
"%OLYMARS_LATEST_FXBIN%\csc.exe" /debug /doc:..\Bob.DataClasses.XML /t:library /r:System.dll /r:System.Data.dll /r:System.Xml.dll /r:System.EnterpriseServices.dll /out:Bob.DataClasses.dll Common\*.cs *.cs ..\..\Version\CS\*.cs
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- TURNING OFF SIGNATURE VERIFICATION
"%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr Bob.DataClasses.dll
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- RESIGNING ASSEMBLY
ECHO YOU WILL NEED TO SIGN THIS ASSEMBLY USING THE
ECHO FOLLOWING COMMAND LINE:
ECHO "%OLYMARS_LATEST_FXSDK%\sn.exe" -R Bob.DataClasses.dll ..\..\PublicPrivateKey\Bob.snk

ECHO.
ECHO.

GOTO END

:ERROR
ECHO.
ECHO.
ECHO.
ECHO --------------------- WARNING !!!!! AN ERROR HAS OCCURED !
ECHO.


:END
if "%1" == "" pause
