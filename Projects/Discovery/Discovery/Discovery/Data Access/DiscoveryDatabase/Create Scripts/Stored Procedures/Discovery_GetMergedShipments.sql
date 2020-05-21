IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Discovery_GetMergedShipments')
	BEGIN
		DROP  Procedure  Discovery_GetMergedShipments
	END

GO

CREATE Procedure Discovery_GetMergedShipments
	(
		@RoutingHistoryId int,
		@sortExpression varchar(1000),
		@startRowIndex int,
		@maximumRows int
	)
AS

Select 
	* 
FROM 
(

	SELECT  
		count(ShipmentNumber) as ShipmentCount,
		Count(distinct CustomerNumber) as CustomerCount,
		LocationCode AS SiteCode,  
		MAX(ShipmentName) as ShipmentName, 
		MAX(PAFAddress1) AS AddressLine1, 
		MAX(PAFPostCode) AS PostCode, 
		MAX(PAFDPS) AS DPSCode ,
		MAX(DeliveryWarehouseCode) as DeliveryWarehouseCode,
		ROW_NUMBER() OVER(
			ORDER BY	
				
			-- ShipmentName
			CASE WHEN @sortExpression = 'ShipmentName' 
				 THEN MAX(ShipmentName) ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'ShipmentName desc' 
				 THEN MAX(ShipmentName) ELSE NULL END DESC,
			CASE WHEN @sortExpression = null or @sortExpression = ''
				THEN cast(LocationCode as int) ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'SiteCode' 
				 THEN cast(LocationCode as int) ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'SiteCode desc' 
				 THEN cast(LocationCode as int) ELSE NULL END DESC,
			CASE WHEN @sortExpression = 'DeliveryWarehouseCode' 
				 THEN MAX(DeliveryWarehouseCode) ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'DeliveryWarehouseCode desc' 
				 THEN MAX(DeliveryWarehouseCode) ELSE NULL END DESC,				 
			CASE WHEN @sortExpression = 'PAFPostCode' 
				 THEN MAX(PAFPostCode) ELSE NULL END ASC,
			CASE WHEN @sortExpression = 'PAFPostCode desc' 
				 THEN MAX(PAFPostCode) ELSE NULL END DESC
 			) as RowNum
	FROM         Discovery_Shipment INNER JOIN
	                      Discovery_ShipmentRoutingHistory ON Discovery_Shipment.Id = Discovery_ShipmentRoutingHistory.ShipmentId
	WHERE     
		(Discovery_ShipmentRoutingHistory.RoutingHistoryId = @RoutingHistoryID)
	GROUP BY 
		Discovery_Shipment.LocationCode	
) as DerivedTableName
WHERE 
@maximumRows=-1 OR 
RowNum 
BETWEEN @startRowIndex AND (@startRowIndex + @maximumRows) - 1
 
		

		
GO

