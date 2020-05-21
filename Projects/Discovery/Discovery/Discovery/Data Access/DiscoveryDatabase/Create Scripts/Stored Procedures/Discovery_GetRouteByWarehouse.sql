IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetRouteByWarehouse')
	BEGIN
		DROP  Procedure  Discovery_GetRouteByWarehouse
	END

GO

CREATE Procedure Discovery_GetRouteByWarehouse
	(
		@WarehouseCode varchar(20),
		@RouteCode varchar(20)
	)

AS

SELECT
	Discovery_Route.Id,
	Discovery_Route.Code,
	Discovery_Route.Description,
	Discovery_Route.IsSameDay,
	Discovery_Route.IsNextDay,
	Discovery_Route.IsCollection,
	Discovery_Route.IsSpecial,
	Discovery_Route.UpdatedDate,
	Discovery_Route.UpdatedBy,
	Discovery_Route.WarehouseId,
	BINARY_CHECKSUM(
	Discovery_Route.Id,
	Discovery_Route.Code,
	Discovery_Route.Description,
	Discovery_Route.IsSameDay,
	Discovery_Route.IsNextDay,
	Discovery_Route.IsCollection,
	Discovery_Route.IsSpecial,
	Discovery_Route.UpdatedDate,
	Discovery_Route.UpdatedBy,
	Discovery_Route.WarehouseId)	
	as CheckSum
FROM         
	Discovery_Route INNER JOIN
	Discovery_Warehouse ON Discovery_Route.WarehouseId = Discovery_Warehouse.Id
WHERE     
	(Discovery_Route.Code = @RouteCode) AND
	(Discovery_Warehouse.Code = @WarehouseCode)
	
GO

 