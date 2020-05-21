@ECHO OFF
ECHO --------------------- BUILDING DOCUMENTATION
"%OLYMARS_HTMLHELP%\HHC.EXE" "Documentation\Documentation.hhp"
ECHO.
ECHO.

if "%1" == "" pause
