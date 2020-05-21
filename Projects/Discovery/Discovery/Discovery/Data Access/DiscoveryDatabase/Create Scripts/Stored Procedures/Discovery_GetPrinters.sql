IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetPrinters')
	BEGIN
		DROP  Procedure  Discovery_GetPrinters
	END

GO

CREATE Procedure Discovery_GetPrinters

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Printer

GO

