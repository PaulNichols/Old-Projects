IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetOpCoRoutesByWarehouseCode')
	BEGIN
		DROP  Procedure  Discovery_GetOpCoRoutesByWarehouseCode
	END

GO

CREATE Procedure Discovery_GetOpCoRoutesByWarehouseCode
	(
		@WarehouseCode varchar(10) = null
	)
AS

SELECT DISTINCT Discovery_Route.Id, Discovery_Route.Code, Discovery_Route.Description
FROM
	Discovery_Route INNER JOIN
	Discovery_Warehouse ON Discovery_Route.WarehouseId = Discovery_Warehouse.Id
WHERE     
	(Discovery_Warehouse.Code = @WarehouseCode)
ORDER BY Discovery_Route.Code

GO


