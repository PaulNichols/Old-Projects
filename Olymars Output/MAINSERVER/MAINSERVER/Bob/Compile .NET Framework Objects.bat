@ECHO OFF
ECHO --------------------- CREATING BIN AND PublicPrivateKey DIRECTORIES
IF NOT EXIST "Bin" MD "Bin"
if errorlevel 1 goto ERROR
IF NOT EXIST "PublicPrivateKey" MD "PublicPrivateKey"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- CREATING STRONG NAME
IF NOT EXIST "PublicPrivateKey\Bob.snk" "%OLYMARS_LATEST_FXSDK%\sn.exe" -k "PublicPrivateKey\Bob.snk"
if errorlevel 1 goto ERROR
IF NOT EXIST "PublicPrivateKey\PublicKey.snk" "%OLYMARS_LATEST_FXSDK%\sn.exe" -p "PublicPrivateKey\Bob.snk" "PublicPrivateKey\PublicKey.snk"
if errorlevel 1 goto ERROR
COPY "PublicPrivateKey\PublicKey.snk" "Bin"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- COMPILING C# CODE: Bob.DataClasses.dll
ECHO If you wish to compile the data classes in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /t:library /debug+ /out:"Bin\Bob.DataClasses.dll" Version\CS\*.cs DataClasses\CS\*.cs DataClasses\CS\Common\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\Bob.DataClasses.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\Bob.DataClasses.dll" PublicPrivateKey\Bob.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob.DataClasses.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: Bob.AbstractClasses.dll
ECHO If you wish to compile the abstract classes in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /t:library /debug+ /r:"Bin\Bob.DataClasses.dll" /out:"Bin\Bob.AbstractClasses.dll" Version\CS\*.cs AbstractClasses\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\Bob.AbstractClasses.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\Bob.AbstractClasses.dll" PublicPrivateKey\Bob.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob.AbstractClasses.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: Bob.SqlDataAdapters.dll
ECHO If you wish to compile the SqlDataAdapters objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /t:library /debug+ /r:"Bin\Bob.DataClasses.dll" /out:"Bin\Bob.SqlDataAdapters.dll" Version\CS\*.cs SqlDataAdapters\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\Bob.SqlDataAdapters.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\Bob.SqlDataAdapters.dll" PublicPrivateKey\Bob.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob.SqlDataAdapters.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: Bob.Windows.dll
ECHO If you wish to compile the Windows objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinComboBox.bmp,Bob.Windows.ComboBoxes.WinComboBox.bmp /res:Images\WinCheckedListBox.bmp,Bob.Windows.CheckedListBoxes.WinCheckedListBox.bmp /res:Images\WinListBox.bmp,Bob.Windows.ListBoxes.WinListBox.bmp /res:Images\WinDataGrid.bmp,Bob.Windows.DataGrids.WinDataGrid.bmp  /t:library /debug+ /r:"Bin\Bob.SqlDataAdapters.dll" /r:"Bin\Bob.DataClasses.dll" /r:"Bin\Bob.AbstractClasses.dll" /out:"Bin\Bob.Windows.dll" Version\CS\*.cs Windows\ListBoxes\CS\*.cs Windows\ComboBoxes\CS\*.cs Windows\CheckedListBoxes\CS\*.cs Windows\DataGrids\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\Bob.Windows.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\Bob.Windows.dll" PublicPrivateKey\Bob.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob.Windows.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: Bob.Windows.CheckedListBoxes.dll
ECHO If you wish to compile the Windows CheckedListBoxes objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinCheckedListBox.bmp,Bob.Windows.CheckedListBoxes.WinCheckedListBox.bmp /t:library /debug+ /r:"Bin\Bob.DataClasses.dll" /r:"Bin\Bob.AbstractClasses.dll" /out:"Bin\Bob.Windows.CheckedListBoxes.dll" Version\CS\*.cs Windows\CheckedListBoxes\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\Bob.Windows.CheckedListBoxes.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\Bob.Windows.CheckedListBoxes.dll" PublicPrivateKey\Bob.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob.Windows.CheckedListBoxes.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: Bob.Windows.ComboBoxes.dll
ECHO If you wish to compile the Windows ComboBoxes objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinComboBox.bmp,Bob.Windows.ComboBoxes.WinComboBox.bmp /t:library /debug+ /r:"Bin\Bob.DataClasses.dll" /r:"Bin\Bob.AbstractClasses.dll" /out:"Bin\Bob.Windows.ComboBoxes.dll" Version\CS\*.cs Windows\ComboBoxes\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\Bob.Windows.ComboBoxes.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\Bob.Windows.ComboBoxes.dll" PublicPrivateKey\Bob.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob.Windows.ComboBoxes.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: Bob.Windows.DataGrids.dll
ECHO If you wish to compile the Windows DataGrids objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinDataGrid.bmp,Bob.Windows.DataGrids.WinDataGrid.bmp /t:library /debug+ /r:"Bin\Bob.SqlDataAdapters.dll" /r:"Bin\Bob.DataClasses.dll" /r:"Bin\Bob.SqlDataAdapters.dll" /r:"Bin\Bob.AbstractClasses.dll" /out:"Bin\Bob.Windows.DataGrids.dll" Version\CS\*.cs Windows\DataGrids\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\Bob.Windows.DataGrids.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\Bob.Windows.DataGrids.dll" PublicPrivateKey\Bob.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob.Windows.DataGrids.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: Bob.Windows.ListBoxes.dll
ECHO If you wish to compile the Windows ListBoxes objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinListBox.bmp,Bob.Windows.ListBoxes.WinListBox.bmp /t:library /debug+ /r:"Bin\Bob.DataClasses.dll" /r:"Bin\Bob.AbstractClasses.dll" /out:"Bin\Bob.Windows.ListBoxes.dll" Version\CS\*.cs Windows\ListBoxes\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\Bob.Windows.ListBoxes.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\Bob.Windows.ListBoxes.dll" PublicPrivateKey\Bob.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob.Windows.ListBoxes.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.



ECHO --------------------- DELETING PREVIOUS NATIVE IMAGE
"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" /DELETE "Bin\Bob.dll"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- COMPILING C# CODE: Bob.dll
"%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinComboBox.bmp,Bob.Windows.ComboBoxes.WinComboBox.bmp /res:Images\WinCheckedListBox.bmp,Bob.Windows.CheckedListBoxes.WinCheckedListBox.bmp /res:Images\WinListBox.bmp,Bob.Windows.ListBoxes.WinListBox.bmp /res:Images\WinDataGrid.bmp,Bob.Windows.DataGrids.WinDataGrid.bmp /define:OLYMARS_DEBUG /define:OLYMARS_ATTRIBUTE /t:library /debug+ /out:"Bin\Bob.dll" /doc:"Bin\Bob.xml" Version\CS\*.cs DataClasses\CS\*.cs DataClasses\CS\Common\*.cs AbstractClasses\CS\*.cs SqlDataAdapters\CS\*.cs Windows\ListBoxes\CS\*.cs Windows\ComboBoxes\CS\*.cs Windows\CheckedListBoxes\CS\*.cs Windows\DataGrids\CS\*.cs 
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- TURNING OFF SIGNATURE VERIFICATION
"%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr Bin\Bob.dll
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- RESIGNING ASSEMBLY
"%OLYMARS_LATEST_FXSDK%\sn.exe" -R Bin\Bob.dll PublicPrivateKey\Bob.snk
if errorlevel 1 goto ERROR
ECHO.

"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\Bob.dll"
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
