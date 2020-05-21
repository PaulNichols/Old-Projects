IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetSalesLocationByCode')
	BEGIN
		DROP  Procedure  Discovery_GetSalesLocationByCode
	END

GO

CREATE Procedure Discovery_GetSalesLocationByCode
	(
		@Code VARCHAR(50) = null
	)

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_SalesLocation
WHERE
	Location=@Code

GO

 