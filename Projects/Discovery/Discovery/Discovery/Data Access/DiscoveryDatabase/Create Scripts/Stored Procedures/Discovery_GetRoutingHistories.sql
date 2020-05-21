IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetRoutingHistories')
	BEGIN
		DROP  Procedure  Discovery_GetRoutingHistories
	END

GO

CREATE Procedure Discovery_GetRoutingHistories


AS

SELECT
	*,
	BINARY_CHECKSUM(*) as CheckSum,
	Discovery_Region.Code as RegionCode
FROM         Discovery_RoutingHistory INNER JOIN
                      Discovery_Region ON Discovery_RoutingHistory.RegionId = Discovery_Region.Id
ORDER BY
	ProcessStartedDate desc

GO

