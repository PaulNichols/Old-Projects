IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetPrinter')
	BEGIN
		DROP  Procedure  Discovery_GetPrinter
	END

GO

CREATE Procedure Discovery_GetPrinter
	(
		@Id int
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_Printer
WHERE
	Id=@Id

GO

 