IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCDeliveryWarehouseCodes')
	BEGIN
		DROP  Procedure  Discovery_GetTDCDeliveryWarehouseCodes
	END

GO

CREATE Procedure Discovery_GetTDCDeliveryWarehouseCodes
	(
		@OpCoCode varchar(3) = null
	)
AS

SELECT DISTINCT DeliveryWarehouseCode AS Code, -1 AS Id, DeliveryWarehouseCode AS Description
FROM Discovery_Shipment
WHERE (OpCoCode = @OpCoCode OR @OpCoCode IS NULL)
ORDER BY Code

GO


