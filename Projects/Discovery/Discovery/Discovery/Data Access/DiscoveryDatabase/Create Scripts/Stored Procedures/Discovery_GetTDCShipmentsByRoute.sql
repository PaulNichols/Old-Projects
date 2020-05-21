IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipmentsByRoute')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipmentsByRoute
	END

GO

CREATE Procedure Discovery_GetTDCShipmentsByRoute

	(

		@DeliveryWarehouseId int ,
		@RequiredDeliveryDate datetime,
		@IncludeNonNextDay bit
	)


AS

SELECT     
	Discovery_Shipment.RouteCode, 
	Discovery_Shipment.DeliveryWarehouseCode,
	Discovery_Route.Id as RouteId,
	Discovery_Shipment.RequiredShipmentDate AS RequiredDeliveryDate, 
	Discovery_Warehouse.Description AS DeliveryLocation, 
	SUM(CASE WHEN Discovery_Shipment.DeliveryWarehouseCode<>Discovery_Shipment.StockWarehouseCode THEN 0 ELSE Discovery_ShipmentLine.NetWeight END ) as ExBranch,
	SUM(CASE WHEN Discovery_Shipment.DeliveryWarehouseCode<>Discovery_Shipment.StockWarehouseCode THEN Discovery_ShipmentLine.NetWeight ELSE 0 END ) AS Trunked, 
	SUM(Discovery_ShipmentLine.NetWeight) *1.05 AS Weight, 
	SUM(Discovery_ShipmentLine.Volume) AS Volume
FROM         
	Discovery_Shipment INNER JOIN
	Discovery_ShipmentLine ON Discovery_Shipment.Id = Discovery_ShipmentLine.ShipmentId INNER JOIN
	Discovery_Warehouse ON Discovery_Shipment.DeliveryWarehouseCode = Discovery_Warehouse.Code INNER JOIN
	Discovery_Route ON Discovery_Shipment.RouteCode = Discovery_Route.Code
GROUP BY 
	Discovery_Shipment.RouteCode, 
	Discovery_Shipment.DeliveryWarehouseCode,
	Discovery_Warehouse.Description, 
	Discovery_Shipment.RequiredShipmentDate, 
	Discovery_Warehouse.Id,
	Discovery_Route.IsSameDay,
	Discovery_Route.IsNextDay,
	Discovery_Route.IsCollection,
	Discovery_Route.Id ,
	Discovery_Route.IsSpecial

	HAVING      
	(Discovery_Warehouse.Id = @DeliveryWarehouseId) AND 
	(Discovery_Shipment.RequiredShipmentDate = @RequiredDeliveryDate)  AND
	(@IncludeNonNextDay =1  OR  Discovery_Route.IsNextDay=1  )
	
GO
