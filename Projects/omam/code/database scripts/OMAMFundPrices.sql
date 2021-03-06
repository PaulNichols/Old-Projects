/*
   16 February 200819:56:14
   User: 
   Server: LAPTOP-VISTA
   Database: OMAM
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
CREATE TABLE dbo.Tmp_OMAMFundPrices
	(
	Id int NOT NULL IDENTITY (1, 1),
	FundCode nvarchar(10) NOT NULL,
	Description nvarchar(100) NOT NULL,
	PriceType nvarchar(5) NOT NULL,
	BidPrice float(53) NOT NULL,
	OfferPrice float(53) NOT NULL,
	Yield float(53) NOT NULL,
	UploadDate smalldatetime NOT NULL,
	UploadedBy int NOT NULL
	)  ON [PRIMARY]
GO
SET IDENTITY_INSERT dbo.Tmp_OMAMFundPrices ON
GO
IF EXISTS(SELECT * FROM dbo.OMAMFundPrices)
	 EXEC('INSERT INTO dbo.Tmp_OMAMFundPrices (Id, FundCode, PriceType, BidPrice, OfferPrice, UploadDate, UploadedBy)
		SELECT Id, FundCode, PriceType, BidPrice, OfferPrice, UploadDate, UploadedBy FROM dbo.OMAMFundPrices WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_OMAMFundPrices OFF
GO
DROP TABLE dbo.OMAMFundPrices
GO
EXECUTE sp_rename N'dbo.Tmp_OMAMFundPrices', N'OMAMFundPrices', 'OBJECT' 
GO
ALTER TABLE dbo.OMAMFundPrices ADD CONSTRAINT
	PK_OMAMFundPrices PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
