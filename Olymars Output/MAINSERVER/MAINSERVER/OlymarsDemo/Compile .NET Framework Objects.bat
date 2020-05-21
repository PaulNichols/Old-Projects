@ECHO OFF
ECHO --------------------- CREATING BIN AND PublicPrivateKey DIRECTORIES
IF NOT EXIST "Bin" MD "Bin"
if errorlevel 1 goto ERROR
IF NOT EXIST "PublicPrivateKey" MD "PublicPrivateKey"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- CREATING STRONG NAME
IF NOT EXIST "PublicPrivateKey\OlymarsDemo.snk" "%OLYMARS_LATEST_FXSDK%\sn.exe" -k "PublicPrivateKey\OlymarsDemo.snk"
if errorlevel 1 goto ERROR
IF NOT EXIST "PublicPrivateKey\PublicKey.snk" "%OLYMARS_LATEST_FXSDK%\sn.exe" -p "PublicPrivateKey\OlymarsDemo.snk" "PublicPrivateKey\PublicKey.snk"
if errorlevel 1 goto ERROR
COPY "PublicPrivateKey\PublicKey.snk" "Bin"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.DataClasses.dll
ECHO If you wish to compile the data classes in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /t:library /debug+ /out:"Bin\OlymarsDemo.DataClasses.dll" Version\CS\*.cs DataClasses\CS\*.cs DataClasses\CS\Common\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.DataClasses.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.DataClasses.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.DataClasses.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.AbstractClasses.dll
ECHO If you wish to compile the abstract classes in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /out:"Bin\OlymarsDemo.AbstractClasses.dll" Version\CS\*.cs AbstractClasses\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.AbstractClasses.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.AbstractClasses.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.AbstractClasses.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.SqlDataAdapters.dll
ECHO If you wish to compile the SqlDataAdapters objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /out:"Bin\OlymarsDemo.SqlDataAdapters.dll" Version\CS\*.cs SqlDataAdapters\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.SqlDataAdapters.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.SqlDataAdapters.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.SqlDataAdapters.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Windows.dll
ECHO If you wish to compile the Windows objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinComboBox.bmp,OlymarsDemo.Windows.ComboBoxes.WinComboBox.bmp /res:Images\WinCheckedListBox.bmp,OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox.bmp /res:Images\WinListBox.bmp,OlymarsDemo.Windows.ListBoxes.WinListBox.bmp /res:Images\WinDataGrid.bmp,OlymarsDemo.Windows.DataGrids.WinDataGrid.bmp  /t:library /debug+ /r:"Bin\OlymarsDemo.SqlDataAdapters.dll" /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Windows.dll" Version\CS\*.cs Windows\ListBoxes\CS\*.cs Windows\ComboBoxes\CS\*.cs Windows\CheckedListBoxes\CS\*.cs Windows\DataGrids\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Windows.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Windows.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Windows.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Windows.CheckedListBoxes.dll
ECHO If you wish to compile the Windows CheckedListBoxes objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinCheckedListBox.bmp,OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox.bmp /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Windows.CheckedListBoxes.dll" Version\CS\*.cs Windows\CheckedListBoxes\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Windows.CheckedListBoxes.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Windows.CheckedListBoxes.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Windows.CheckedListBoxes.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Windows.ComboBoxes.dll
ECHO If you wish to compile the Windows ComboBoxes objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinComboBox.bmp,OlymarsDemo.Windows.ComboBoxes.WinComboBox.bmp /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Windows.ComboBoxes.dll" Version\CS\*.cs Windows\ComboBoxes\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Windows.ComboBoxes.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Windows.ComboBoxes.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Windows.ComboBoxes.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Windows.DataGrids.dll
ECHO If you wish to compile the Windows DataGrids objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinDataGrid.bmp,OlymarsDemo.Windows.DataGrids.WinDataGrid.bmp /t:library /debug+ /r:"Bin\OlymarsDemo.SqlDataAdapters.dll" /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.SqlDataAdapters.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Windows.DataGrids.dll" Version\CS\*.cs Windows\DataGrids\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Windows.DataGrids.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Windows.DataGrids.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Windows.DataGrids.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Windows.ListBoxes.dll
ECHO If you wish to compile the Windows ListBoxes objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinListBox.bmp,OlymarsDemo.Windows.ListBoxes.WinListBox.bmp /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Windows.ListBoxes.dll" Version\CS\*.cs Windows\ListBoxes\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Windows.ListBoxes.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Windows.ListBoxes.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Windows.ListBoxes.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Web.dll
ECHO If you wish to compile the Web objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WebDataGrid.bmp,OlymarsDemo.Web.DataGrids.WebDataGrid.bmp /res:Images\WebDataList.bmp,OlymarsDemo.Web.DataLists.WebDataList.bmp /res:Images\WebRepeater.bmp,OlymarsDemo.Web.Repeaters.WebRepeater.bmp /res:Images\WebDropDownList.bmp,OlymarsDemo.Web.DropDownLists.WebDropDownList.bmp /res:Images\WebListBox.bmp,OlymarsDemo.Web.ListBoxes.WebListBox.bmp /res:Images\WebCheckBoxList.bmp,OlymarsDemo.Web.CheckBoxLists.WebCheckBoxList.bmp /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Web.dll" Version\CS\*.cs Web\DropDownLists\CS\*.cs Web\ListBoxes\CS\*.cs Web\CheckBoxLists\CS\*.cs Web\Repeaters\CS\*.cs Web\DataGrids\CS\*.cs Web\DataLists\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Web.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Web.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Web.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Web.CheckBoxLists.dll
ECHO If you wish to compile the Web CheckBoxLists objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WebCheckBoxList.bmp,OlymarsDemo.Web.CheckBoxLists.WebCheckBoxList.bmp /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Web.CheckBoxLists.dll" Version\CS\*.cs Web\CheckBoxLists\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Web.CheckBoxLists.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Web.CheckBoxLists.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Web.CheckBoxLists.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Web.DataGrids.dll
ECHO If you wish to compile the Web DataGrids objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WebDataGrid.bmp,OlymarsDemo.Web.DataGrids.WebDataGrid.bmp /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Web.DataGrids.dll" Version\CS\*.cs Web\DataGrids\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Web.DataGrids.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Web.DataGrids.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Web.DataGrids.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Web.DataLists.dll
ECHO If you wish to compile the Web DataLists objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WebDataList.bmp,OlymarsDemo.Web.DataLists.WebDataList.bmp /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Web.DataLists.dll" Version\CS\*.cs Web\DataLists\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Web.DataLists.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Web.DataLists.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Web.DataLists.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Web.DropDownLists.dll
ECHO If you wish to compile the Web DropDownLists objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WebDropDownList.bmp,OlymarsDemo.Web.DropDownLists.WebDropDownList.bmp /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Web.DropDownLists.dll" Version\CS\*.cs Web\DropDownLists\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Web.DropDownLists.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Web.DropDownLists.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Web.DropDownLists.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Web.ListBoxes.dll
ECHO If you wish to compile the Web ListBoxes objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WebListBox.bmp,OlymarsDemo.Web.ListBoxes.WebListBox.bmp /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Web.ListBoxes.dll" Version\CS\*.cs Web\ListBoxes\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Web.ListBoxes.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Web.ListBoxes.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Web.ListBoxes.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.Web.Repeaters.dll
ECHO If you wish to compile the Web Repeaters objects in a specific DLL, uncomment the next four lines in this batch file.
REM "%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WebRepeater.bmp,OlymarsDemo.Web.Repeaters.WebRepeater.bmp /t:library /debug+ /r:"Bin\OlymarsDemo.DataClasses.dll" /r:"Bin\OlymarsDemo.AbstractClasses.dll" /out:"Bin\OlymarsDemo.Web.Repeaters.dll" Version\CS\*.cs Web\Repeaters\CS\*.cs
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr "Bin\OlymarsDemo.Web.Repeaters.dll"
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXSDK%\sn.exe" -R "Bin\OlymarsDemo.Web.Repeaters.dll" PublicPrivateKey\OlymarsDemo.snk
REM if errorlevel 1 goto ERROR
REM "%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.Web.Repeaters.dll"
REM if errorlevel 1 goto ERROR
ECHO.
ECHO.

ECHO --------------------- DELETING PREVIOUS NATIVE IMAGE
"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" /DELETE "Bin\OlymarsDemo.dll"
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- COMPILING C# CODE: OlymarsDemo.dll
"%OLYMARS_LATEST_FXBIN%\csc.exe" /res:Images\WinComboBox.bmp,OlymarsDemo.Windows.ComboBoxes.WinComboBox.bmp /res:Images\WinCheckedListBox.bmp,OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox.bmp /res:Images\WinListBox.bmp,OlymarsDemo.Windows.ListBoxes.WinListBox.bmp /res:Images\WinDataGrid.bmp,OlymarsDemo.Windows.DataGrids.WinDataGrid.bmp /res:Images\WebDataGrid.bmp,OlymarsDemo.Web.DataGrids.WebDataGrid.bmp /res:Images\WebDataList.bmp,OlymarsDemo.Web.DataLists.WebDataList.bmp /res:Images\WebRepeater.bmp,OlymarsDemo.Web.Repeaters.WebRepeater.bmp /res:Images\WebDropDownList.bmp,OlymarsDemo.Web.DropDownLists.WebDropDownList.bmp /res:Images\WebListBox.bmp,OlymarsDemo.Web.ListBoxes.WebListBox.bmp /res:Images\WebCheckBoxList.bmp,OlymarsDemo.Web.CheckBoxLists.WebCheckBoxList.bmp /define:OLYMARS_DEBUG /define:OLYMARS_ATTRIBUTE /t:library /debug+ /out:"Bin\OlymarsDemo.dll" /doc:"Bin\OlymarsDemo.xml" Version\CS\*.cs DataClasses\CS\*.cs DataClasses\CS\Common\*.cs AbstractClasses\CS\*.cs SqlDataAdapters\CS\*.cs Windows\ListBoxes\CS\*.cs Windows\ComboBoxes\CS\*.cs Windows\CheckedListBoxes\CS\*.cs Web\DropDownLists\CS\*.cs Web\ListBoxes\CS\*.cs Web\CheckBoxLists\CS\*.cs Web\Repeaters\CS\*.cs Windows\DataGrids\CS\*.cs Web\DataGrids\CS\*.cs Web\DataLists\CS\*.cs
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- TURNING OFF SIGNATURE VERIFICATION
"%OLYMARS_LATEST_FXSDK%\sn.exe" -Vr Bin\OlymarsDemo.dll
if errorlevel 1 goto ERROR
ECHO.

ECHO --------------------- RESIGNING ASSEMBLY
"%OLYMARS_LATEST_FXSDK%\sn.exe" -R Bin\OlymarsDemo.dll PublicPrivateKey\OlymarsDemo.snk
if errorlevel 1 goto ERROR
ECHO.

"%OLYMARS_LATEST_FXBIN%\NGEN.EXE" "Bin\OlymarsDemo.dll"
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
