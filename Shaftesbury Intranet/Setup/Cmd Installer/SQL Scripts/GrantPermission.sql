
USE master
IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = '~~##PRTLStarterKitUser##~~')
BEGIN
    declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'us_english'
    if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
        select @logindb = N'master'
    if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
        select @loginlang = @@language
    exec sp_addlogin '~~##PRTLStarterKitUser##~~', '~~##PRTLUserPWD##~~', @logindb, @loginlang
END


USE ~~##PRTLStarterKitDB##~~
if not exists(select * from sysusers where name=N'~~##PRTLStarterKitUser##~~')
	EXEC sp_grantdbaccess N'~~##PRTLStarterKitUser##~~'
	
EXEC sp_addrolemember N'db_owner', N'~~##PRTLStarterKitUser##~~'
