IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoSalesLocations')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoSalesLocations
	END

GO

CREATE Procedure Discovery_GetOpCoSalesLocations
	(
		@OpCoCode varchar(3) = null
	)
AS

SELECT DISTINCT SalesBranchCode AS Location, -1 AS Id, SalesBranchCode AS Description
FROM Discovery_OpCoShipment
WHERE (OpCoCode = @OpCoCode OR @OpCoCode IS NULL)
ORDER BY Location

GO


