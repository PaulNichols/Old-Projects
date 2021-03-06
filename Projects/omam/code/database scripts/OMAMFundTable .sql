/*
   22 February 200813:46:38
   User: 
   Server: XP4336
   Database: test
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.[OMAMFund] ADD
	OffShore bit NOT NULL CONSTRAINT [DF_OMAMFundTable _OffShore] DEFAULT 0,
	Currency varchar(5) NOT NULL 
GO
COMMIT
