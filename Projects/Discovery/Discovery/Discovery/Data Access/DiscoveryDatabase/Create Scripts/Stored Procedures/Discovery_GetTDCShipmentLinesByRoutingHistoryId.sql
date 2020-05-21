 IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetTDCShipmentLinesByRoutingId')
	BEGIN
		DROP  Procedure  Discovery_GetTDCShipmentLinesByRoutingId
	END

GO

CREATE Procedure Discovery_GetTDCShipmentLinesByRoutingId

	(
		@RoutingHistoryId int
	)

AS


SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum
FROM         Discovery_ShipmentLine INNER JOIN
                      Discovery_ShipmentRoutingHistory ON Discovery_ShipmentLine.ShipmentId = Discovery_ShipmentRoutingHistory.ShipmentId
WHERE     (Discovery_ShipmentRoutingHistory.RoutingHistoryId = @RoutingHistoryId)


GO



