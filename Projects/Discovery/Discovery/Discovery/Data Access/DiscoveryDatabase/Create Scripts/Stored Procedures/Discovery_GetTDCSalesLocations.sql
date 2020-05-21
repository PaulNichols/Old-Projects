IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCSalesLocations')
	BEGIN
		DROP  Procedure  Discovery_GetTDCSalesLocations
	END

GO

CREATE Procedure Discovery_GetTDCSalesLocations
	(
		@OpCoCode varchar(3) = null
	)
AS

SELECT DISTINCT SalesBranchCode AS Location, -1 AS Id, SalesBranchCode AS Description
FROM Discovery_Shipment
WHERE (OpCoCode = @OpCoCode OR @OpCoCode IS NULL)
ORDER BY Location

GO


