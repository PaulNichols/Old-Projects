IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_ManualMergeReshuffle')
	BEGIN
		DROP  Procedure  Discovery_ManualMergeReshuffle
	END

GO

CREATE Procedure Discovery_ManualMergeReshuffle

	(
		@RoutingHistoryId int
	)

AS


DECLARE  @SiteCode varchar(50)     
DECLARE  @newSiteCode int      

-- declare the cursor

DECLARE MergedSites CURSOR FOR

SELECT     Discovery_Shipment.LocationCode AS SiteCode
FROM         Discovery_Shipment INNER JOIN
                      Discovery_ShipmentRoutingHistory ON Discovery_Shipment.Id = Discovery_ShipmentRoutingHistory.ShipmentId
WHERE RoutingHistoryId=@RoutingHistoryId
GROUP BY Discovery_Shipment.LocationCode
ORDER BY CAST(Discovery_Shipment.LocationCode AS int)

 

OPEN MergedSites

SET @NewSiteCode=0

FETCH MergedSites INTO @SiteCode

-- start the main processing loop.

WHILE @@Fetch_Status = 0

   BEGIN
		
  	UPDATE    Discovery_Shipment
  	SET              LocationCode = @NewSiteCode
  	FROM         Discovery_Shipment INNER JOIN
  	                      Discovery_ShipmentRoutingHistory ON Discovery_Shipment.Id = Discovery_ShipmentRoutingHistory.ShipmentId
  	WHERE     (Discovery_Shipment.LocationCode = @SiteCode) AND
  	RoutingHistoryId=@RoutingHistoryId
			 
   -- Get the next row.

   FETCH MergedSites INTO @SiteCode             

	SET @NewSiteCode=@NewSiteCode+1
	
   END

CLOSE MergedSites

DEALLOCATE MergedSites


GO
