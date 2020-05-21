IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipmentTrunkedStockSummary')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipmentTrunkedStockSummary
	END

GO

CREATE Procedure Discovery_GetTDCShipmentTrunkedStockSummary


	(
		
		@DeliveryWarehouseId int ,
		@RouteId int,
		@RequiredDeliveryDate datetime
	)


AS

SELECT     
	Discovery_Route.Description as RouteDescription,
	Discovery_Route.Code as RouteCode,
	SUM(Discovery_ShipmentLine.NetWeight) AS Weight, 
	SUM(Discovery_ShipmentLine.Volume) AS Volume, 
	StockWarehouse.Code as StockWarehouseCode,
	StockWarehouse.Description as StockWarehouseDescription,
	Discovery_Shipment.DeliveryWarehouseCode
FROM         Discovery_Shipment INNER JOIN
                      Discovery_Route ON Discovery_Shipment.RouteCode = Discovery_Route.Code INNER JOIN
                      Discovery_ShipmentLine ON Discovery_Shipment.Id = Discovery_ShipmentLine.ShipmentId INNER JOIN
                      Discovery_Warehouse AS DeliveryWarehouse ON Discovery_Shipment.DeliveryWarehouseCode = DeliveryWarehouse.Code INNER JOIN
                      Discovery_Warehouse AS StockWarehouse ON Discovery_Shipment.StockWarehouseCode = StockWarehouse.Code
WHERE
	Discovery_Shipment.DeliveryWarehouseCode<>Discovery_Shipment.StockWarehouseCode	and
	(DeliveryWarehouse.Id = @DeliveryWarehouseId) AND 
	(Discovery_Shipment.RequiredShipmentDate = @RequiredDeliveryDate) 	AND 
	(@RouteId=-1 OR Discovery_Route.Id = @RouteId)
GROUP BY 
	Discovery_Route.Description, 
	Discovery_Route.Id,
	StockWarehouse.Description, 
	Discovery_Shipment.StockWarehouseCode, 
	DeliveryWarehouse.Id, 
	Discovery_Shipment.DeliveryWarehouseCode,
	Discovery_Shipment.RequiredShipmentDate,
	StockWarehouse.Code,
	Discovery_Route.Code 

go