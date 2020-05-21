IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetRoutingHistoryByShipmentLineId')
	BEGIN
		DROP  Procedure  Discovery_GetRoutingHistoryByShipmentLineId
	END

GO

CREATE Procedure Discovery_GetRoutingHistoryByShipmentLineId
	(
		@ShipmentLineId int
	)

AS


SELECT top 1
	*,
	BINARY_CHECKSUM(
		Discovery_RoutingHistory.Id
      ,Discovery_RoutingHistory.RegionId
      ,Discovery_RoutingHistory.SentDate
      ,Discovery_RoutingHistory.ProcessStartedDate
      ,Discovery_RoutingHistory.ResetDate
      ,Discovery_RoutingHistory.ProcessedBy
      ,Discovery_RoutingHistory.ResetBy
      ,Discovery_RoutingHistory.TripFileReceivedDate
      ,Discovery_RoutingHistory.DropFileReceivedDate
      ,Discovery_RoutingHistory.TripPartFileReceivedDate
        ) as CheckSum,
		Discovery_Region.Code as RegionCode
FROM         Discovery_RoutingHistory INNER JOIN
                      Discovery_Region ON Discovery_RoutingHistory.RegionId = Discovery_Region.Id INNER JOIN
                      Discovery_ShipmentRoutingHistory ON Discovery_RoutingHistory.Id = Discovery_ShipmentRoutingHistory.RoutingHistoryId INNER JOIN
                      Discovery_ShipmentLine ON Discovery_ShipmentRoutingHistory.ShipmentId = Discovery_ShipmentLine.ShipmentId
WHERE     (Discovery_ShipmentLine.ShipmentId = @ShipmentLineId) and
		Discovery_RoutingHistory.ResetDate is null
GO

