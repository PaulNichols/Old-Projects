IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetShipmentsForDeliveryPoint')
	BEGIN
		DROP  Procedure  Discovery_GetShipmentsForDeliveryPoint
	END

GO

CREATE Procedure Discovery_GetShipmentsForDeliveryPoint
	(
		@deliveryPoint varchar(50) ,
		@routingHistoryId int 
	)

AS

SELECT     
	*, 
	BINARY_CHECKSUM(*) AS CheckSum
FROM         
	Discovery_Shipment 
WHERE     
	(Discovery_Shipment.LocationCode = @deliveryPoint) AND
	id in 
		(
			select 
				ShipmentId  as id
			from 
				Discovery_ShipmentRoutingHistory 
			where 
				Discovery_ShipmentRoutingHistory.ShipmentId=Discovery_Shipment.Id and
				Discovery_ShipmentRoutingHistory.RoutingHistoryId=@routingHistoryId
		)

GO

