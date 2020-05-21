@ECHO OFF

REM Change the ROOT_PATH to point to the location where PRTL.exe is located  
@SET ROOT_PATH = "C:\Program Files\ASP.NET Starter Kits"

REM %1  Target IIS Server Name
REM %2  Target Directory (local path on the target IIS server)
REM %3  Existing web site on target IIS Server
REM %4  Name of Virtual Directory to create for Portal Web (existing one will be modified)
REM %5  Target DB Server Name
REM %6  Database Name on target DB server
REM %7  SQL user name for %6 as db owner
REM %8  SQL password for user specified by %7
REM %9  SQL password for sa

IF x==%1x GOTO USAGE
IF x==%2x GOTO USAGE
IF x==%3x GOTO USAGE
IF x==%4x GOTO USAGE
IF x==%5x GOTO USAGE
IF x==%6x GOTO USAGE
IF x==%7x GOTO USAGE
IF x==%8x GOTO USAGE
IF x==%9x GOTO USAGE

@cd  %ROOT_PATH%

@call PRTL.exe INSTALL_OPTION=FULL WEB_FILES_DIR="PortalWebVBVS" DB_FILES_DIR="SQL Scripts" IIS_SERVER=%1 TARGET_DIR=%2 WEB_SITE=%3 VDIR=%4 DB_CONNECTION=SQL DB_SERVER=%5 DB_NAME=%6 DB_OWNER_LOGIN=%7 DB_OWNER_PWD=%8 SQL_ADMIN_USER=SA SQL_ADMIN_PWD=%9 LOG_FILE=%1_%5_installLog.txt

GOTO END

:USAGE
ECHO *******************************************************************************
ECHO * 
ECHO * Please edit ROOT_PATH in RemoteFullInstall.bat before running it
ECHO * 
ECHO * Usage:
ECHO *    RemoteFullInstall.bat iisserver targetpath site webvdir mobilevdir 
ECHO *                             dbserver dbname dbowner dbownerpwd 
ECHO * 
ECHO *    iiserver     - IIS server name to install the application
ECHO *    targetpath   - Path where all the web files will be copied
ECHO *    site         - Existing web site on local IIS server
ECHO *    webvdir      - Virtual directory name for Portal Web
ECHO *    dbserver     - Remote Database Server Name
ECHO *    dbname       - Database name
ECHO *    dbowner      - SQL login for dbowner 
ECHO *    dbownerpwd   - dbowner password
ECHO *    sapwd        - sa password
ECHO *    
ECHO *******************************************************************************
pause

:END