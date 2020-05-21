IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMergedShipmentsCount')
	BEGIN
		DROP  Procedure  Discovery_GetMergedShipmentsCount
	END

GO

CREATE Procedure Discovery_GetMergedShipmentsCount
(
		@RoutingHIstoryId int
	)

AS
Select 
	count(LocationCode) 
FROM 
(
	SELECT     Discovery_Shipment.LocationCode
	FROM         Discovery_Shipment INNER JOIN
	                      Discovery_ShipmentRoutingHistory ON Discovery_Shipment.Id = Discovery_ShipmentRoutingHistory.ShipmentId
	WHERE     (Discovery_ShipmentRoutingHistory.RoutingHistoryId = @RoutingHistoryID)
	GROUP BY Discovery_Shipment.LocationCode
		) as derivedtable

GO
