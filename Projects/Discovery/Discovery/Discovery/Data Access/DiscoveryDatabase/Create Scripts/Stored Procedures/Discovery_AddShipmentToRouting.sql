IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_AddShipmentToRouting')
	BEGIN
		DROP  Procedure  Discovery_AddShipmentToRouting
	END

GO

CREATE Procedure Discovery_AddShipmentToRouting

	(
		@shipmentId int,
		@routingHistoryId int
	)
AS
insert into Discovery_ShipmentRoutingHistory (shipmentId,routingHistoryId) values(@shipmentId,@routingHistoryId)




