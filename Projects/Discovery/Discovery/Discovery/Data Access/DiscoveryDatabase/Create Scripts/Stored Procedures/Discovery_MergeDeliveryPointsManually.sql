IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_MergeDeliveryPointsManually')
	BEGIN
		DROP  Procedure  Discovery_MergeDeliveryPointsManually
	END

GO

CREATE Procedure Discovery_MergeDeliveryPointsManually

	(
		@RoutingHistoryId int ,
		@MainCode varchar(50),
		@CodeToUpdate varchar(MAX)
	)

AS
--perform check to ensure that the items all have the same delivery warehouse as you cannot merge across warehouses
if (select count(deliverywarehousecode ) from (select distinct deliverywarehousecode from discovery_shipment 
where locationcode in(@CodeToUpdate) ) as subset)>1
	
	return -1

IF (@@ERROR <> 0) return -2

UPDATE    
	Discovery_Shipment
SET              
	LocationCode = @MainCode
FROM         
	Discovery_Shipment INNER JOIN
	Discovery_ShipmentRoutingHistory ON Discovery_Shipment.Id = Discovery_ShipmentRoutingHistory.ShipmentId
WHERE     
	(Discovery_Shipment.LocationCode = @CodeToUpdate) AND 
	Discovery_ShipmentRoutingHistory.RoutingHistoryId=@RoutingHistoryId

IF (@@ERROR <> 0) return -2	
	
	return 0
GO
