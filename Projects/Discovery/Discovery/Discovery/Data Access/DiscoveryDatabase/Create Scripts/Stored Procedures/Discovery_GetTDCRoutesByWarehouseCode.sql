IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCRoutesByWarehouseCode')
	BEGIN
		DROP  Procedure  Discovery_GetTDCRoutesByWarehouseCode
	END

GO

CREATE Procedure Discovery_GetTDCRoutesByWarehouseCode
	(
		@WarehouseCode varchar(10) = null
	)
AS

SELECT DISTINCT Discovery_Route.Id, Discovery_Route.Code, Discovery_Route.Description
FROM
	Discovery_Route INNER JOIN
	Discovery_Warehouse ON Discovery_Route.WarehouseId = Discovery_Warehouse.Id
WHERE     
	(Discovery_Warehouse.Code = @WarehouseCode OR @WarehouseCode is NULL)
ORDER BY Discovery_Route.Code

GO


