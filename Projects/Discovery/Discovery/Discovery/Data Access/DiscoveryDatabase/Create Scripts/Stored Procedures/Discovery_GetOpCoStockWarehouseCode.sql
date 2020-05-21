IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoStockWarehouseCodes')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoStockWarehouseCodes
	END

GO

CREATE Procedure Discovery_GetOpCoStockWarehouseCodes
	(
		@OpCoCode varchar(3) = null
	)
AS

SELECT DISTINCT StockWarehouseCode AS Code, -1 AS Id, StockWarehouseCode AS Description
FROM Discovery_OpCoShipment
WHERE (OpCoCode = @OpCoCode OR @OpCoCode IS NULL)

GO


