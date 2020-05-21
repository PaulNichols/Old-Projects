IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetSalesLocations')
	BEGIN
		DROP  Procedure  Discovery_GetSalesLocations
	END

GO

CREATE Procedure Discovery_GetSalesLocations

AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM
	Discovery_SalesLocation
ORDER BY [Location]

GO

