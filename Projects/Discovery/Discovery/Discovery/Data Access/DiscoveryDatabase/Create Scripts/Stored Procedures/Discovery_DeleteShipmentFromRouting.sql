IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_DeleteShipmentFromRouting')
	BEGIN
		DROP  Procedure  Discovery_DeleteShipmentFromRouting
	END

GO

CREATE Procedure Discovery_DeleteShipmentFromRouting

	(
		@ShipmentId int,
		@HistoryId int
	)


AS

DELETE 
FROM
	Discovery_ShipmentRoutingHistory
WHERE
	ShipmentId=@ShipmentId AND
	RoutingHistoryId=@HistoryId

