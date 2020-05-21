IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetRoutingHistory')
	BEGIN
		DROP  Procedure  Discovery_GetRoutingHistory
	END

GO

CREATE Procedure Discovery_GetRoutingHistory
	(
		@Id int
	)

AS


SELECT
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
                      Discovery_Region ON Discovery_RoutingHistory.RegionId = Discovery_Region.Id
WHERE
	Discovery_RoutingHistory.Id=@Id

GO

