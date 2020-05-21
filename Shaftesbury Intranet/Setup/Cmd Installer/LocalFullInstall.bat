@ECHO OFF

REM Change the ROOT_PATH to point to the location where PRTL.exe is located  
@SET ROOT_PATH = "C:\Program Files\ASP.NET Starter Kits"

REM %1 Target Directory (local path on this local IIS server)
REM %2 Existing web site on Local IIS Server
REM %3 Name of Virtual Directory to create for Portal Application (existing one will be modified)
REM %4 Database Name on local DB server
REM %5 SQL user name for %4 as db owner
REM %6 SQL password for user specified by %5
REM %7 SQL "sa" password

IF x==%1x GOTO USAGE
IF x==%2x GOTO USAGE
IF x==%3x GOTO USAGE
IF x==%4x GOTO USAGE
IF x==%5x GOTO USAGE
IF x==%6x GOTO USAGE
IF x==%7x GOTO USAGE

@cd  %ROOT_PATH%

@call PRTL.exe INSTALL_OPTION=FULL WEB_FILES_DIR="PortalWebVBVS" DB_FILES_DIR="SQL Scripts" TARGET_DIR=%1 IIS_SERVER=localhost WEB_SITE=%2 VDIR=%3 DB_CONNECTION=SQL DB_SERVER=localhost DB_NAME=%4 DB_OWNER_LOGIN=%5 DB_OWNER_PWD=%6 SQL_ADMIN_USER=SA SQL_ADMIN_PWD=%7 LOG_FILE=installLog.txt
GOTO END

:USAGE
ECHO *******************************************************************************
ECHO * 
ECHO * Please edit the ROOT_PATH variable in LocalFullInstall.bat before running it
ECHO * 
ECHO * Usage:
ECHO *    LocalFullInstall.bat targetpath site webvdir mobilevdir
ECHO *                           dbname dbowner dbownerpwd sapwd
ECHO * 
ECHO *    targetpath   - Path where all the web files will be copied
ECHO *    site         - Existing web site on local IIS server
ECHO *    webvdir      - Virtual directory name for Portal Web
ECHO *    dbname       - Database name
ECHO *    dbowner      - SQL login for dbowner 
ECHO *    dbownerpwd   - dbowner password
ECHO *    sapwd        - SQL "sa" password
ECHO *    
ECHO *******************************************************************************
pause

:END