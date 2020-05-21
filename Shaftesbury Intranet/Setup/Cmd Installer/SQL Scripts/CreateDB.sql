IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'~~##PRTLStarterKitDB##~~')
BEGIN
	USE master
	
	CREATE DATABASE [~~##PRTLStarterKitDB##~~] 
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'autoclose', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'bulkcopy', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'trunc. log', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'torn page detection', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'read only', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'dbo use', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'single', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'autoshrink', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'ANSI null default', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'recursive triggers', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'ANSI nulls', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'concat null yields null', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'cursor close on commit', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'default to local cursor', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'quoted identifier', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'ANSI warnings', N'false'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'auto create statistics', N'true'
	
	
	exec sp_dboption N'~~##PRTLStarterKitDB##~~', N'auto update statistics', N'true'

END

GO





